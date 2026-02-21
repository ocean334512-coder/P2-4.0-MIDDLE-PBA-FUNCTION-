using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace _20260202_P2_MIDDLE.Communications
{
    /// <summary>
    /// JIG 디바이스 RS232 통신 클래스
    /// Protocol: Header(ITM) + LEN(2byte BE) + CMD + ITEM + DATA... + CRC
    /// </summary>
    public class JigComm : IDeviceComm, IDisposable
    {
        private SerialPort _serial;
        private readonly object _lock = new object();

        private const int DEFAULT_TIMEOUT_MS = 3000;

        #region Header / CRC 상수

        private static readonly byte[] HEADER = { 0x49, 0x54, 0x4D }; // 'I','T','M'
        private const byte CRC_XOR_KEY = 0x55;

        #endregion

        #region CMD 상수

        public const byte CMD_JIG_INFO    = 0x00;
        public const byte CMD_JIG_SIGNAL  = 0x01;
        public const byte CMD_CONTROL     = 0x11;

        #endregion

        #region ITEM 상수 - Jig Information (CMD 0x00)

        public const byte ITEM_FIRMWARE_VERSION = 0x00;

        #endregion

        #region ITEM 상수 - Jig Signal (CMD 0x01)

        public const byte ITEM_JIG_INITIALIZE = 0x00;
        public const byte ITEM_TEST_START     = 0x01;
        public const byte ITEM_TEST_STOP      = 0x02;
        public const byte ITEM_JIG_START      = 0x11;
        public const byte ITEM_JIG_STOP       = 0x12;
        public const byte ITEM_OK_BUTTON      = 0xF1;
        public const byte ITEM_NG_BUTTON      = 0xF2;
        public const byte ITEM_QR_READ        = 0xA1;

        #endregion

        #region ITEM 상수 - Control List (CMD 0x11)

        public const byte ITEM_PRODUCT_DETECT     = 0x01;
        public const byte ITEM_RESULT_LED_DISPLAY  = 0x02;
        public const byte ITEM_RELAY_CONTROL       = 0x03;
        public const byte ITEM_VCC_CONNECT         = 0x04;
        public const byte ITEM_DP_DM_CONNECT       = 0x05;

        #endregion

        #region 이벤트 (JIG→PC 수신 알림)

        public event Action OnJigStartRequested;
        public event Action OnJigStopRequested;
        public event Action OnOkButtonPressed;
        public event Action OnNgButtonPressed;
        public event Action OnQrReadRequested;

        #endregion

        public bool IsConnected => _serial?.IsOpen ?? false;

        public JigComm(string portName, int baudRate = 115200)
        {
            _serial = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One)
            {
                ReadTimeout = DEFAULT_TIMEOUT_MS,
                WriteTimeout = DEFAULT_TIMEOUT_MS
            };
        }

        #region 연결 / 해제

        public bool Connect()
        {
            try
            {
                if (_serial.IsOpen) return true;
                _serial.Open();
                _serial.DiscardInBuffer();
                _serial.DiscardOutBuffer();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                if (_serial != null && _serial.IsOpen)
                    _serial.Close();
            }
            catch { }
        }

        #endregion

        #region 패킷 빌드 / 파싱

        /// <summary>
        /// 송신 패킷 생성: Header(3) + LEN(2 BE) + CMD + ITEM + DATA... + CRC
        /// </summary>
        private byte[] BuildPacket(byte cmd, byte item, params byte[] data)
        {
            int payloadLen = 2 + (data?.Length ?? 0); // CMD + ITEM + DATA
            int packetLen = HEADER.Length + 2 + payloadLen + 1; // Header + LEN + payload + CRC

            byte[] packet = new byte[packetLen];
            int idx = 0;

            Array.Copy(HEADER, 0, packet, idx, HEADER.Length);
            idx += HEADER.Length;

            packet[idx++] = (byte)((payloadLen >> 8) & 0xFF); // LEN High
            packet[idx++] = (byte)(payloadLen & 0xFF);        // LEN Low

            packet[idx++] = cmd;
            packet[idx++] = item;

            if (data != null && data.Length > 0)
            {
                Array.Copy(data, 0, packet, idx, data.Length);
                idx += data.Length;
            }

            // CRC: (CMD + ITEM + Data...) 합산 후 XOR 0x55
            int sum = cmd + item;
            if (data != null)
            {
                for (int i = 0; i < data.Length; i++)
                    sum += data[i];
            }
            packet[idx] = (byte)((sum & 0xFF) ^ CRC_XOR_KEY);

            return packet;
        }

        /// <summary>
        /// 응답 수신 및 파싱. DATA 부분만 반환 (CMD, ITEM 제외).
        /// expectedCmd/expectedItem과 일치하지 않으면 null.
        /// </summary>
        private byte[] SendAndReceive(byte cmd, byte item, byte[] data = null, int timeoutMs = DEFAULT_TIMEOUT_MS)
        {
            lock (_lock)
            {
                byte[] packet = BuildPacket(cmd, item, data);
                _serial.DiscardInBuffer();
                _serial.Write(packet, 0, packet.Length);

                return ReadResponse(cmd, item, timeoutMs);
            }
        }

        /// <summary>
        /// 응답 패킷 읽기. Header 동기화 후 LEN/CMD/ITEM/DATA/CRC 파싱.
        /// </summary>
        private byte[] ReadResponse(byte expectedCmd, byte expectedItem, int timeoutMs)
        {
            _serial.ReadTimeout = timeoutMs;
            DateTime deadline = DateTime.Now.AddMilliseconds(timeoutMs);

            // Header 'I','T','M' 동기화
            int headerIdx = 0;
            while (DateTime.Now < deadline)
            {
                int b = _serial.ReadByte();
                if (b == HEADER[headerIdx])
                {
                    headerIdx++;
                    if (headerIdx == HEADER.Length) break;
                }
                else
                {
                    headerIdx = (b == HEADER[0]) ? 1 : 0;
                }
            }

            if (headerIdx != HEADER.Length)
                return null;

            // LEN (2 bytes, Big-Endian)
            int lenHigh = _serial.ReadByte();
            int lenLow = _serial.ReadByte();
            int payloadLen = (lenHigh << 8) | lenLow;

            if (payloadLen < 2) return null;

            // Payload (CMD + ITEM + DATA)
            byte[] payload = new byte[payloadLen];
            int read = 0;
            while (read < payloadLen && DateTime.Now < deadline)
            {
                int r = _serial.Read(payload, read, payloadLen - read);
                read += r;
            }

            // CRC
            int crc = _serial.ReadByte();

            // CRC 검증
            int sum = 0;
            for (int i = 0; i < payloadLen; i++)
                sum += payload[i];
            byte expectedCrc = (byte)((sum & 0xFF) ^ CRC_XOR_KEY);

            if (crc != expectedCrc) return null;

            byte respCmd = payload[0];
            byte respItem = payload[1];
            if (respCmd != expectedCmd || respItem != expectedItem) return null;

            // DATA 부분만 추출
            byte[] respData = new byte[payloadLen - 2];
            if (respData.Length > 0)
                Array.Copy(payload, 2, respData, 0, respData.Length);

            return respData;
        }

        /// <summary>
        /// 에코 응답만 보내기 (JIG→PC 수신 후 PC→JIG 응답용)
        /// </summary>
        private void SendEcho(byte cmd, byte item)
        {
            lock (_lock)
            {
                byte[] packet = BuildPacket(cmd, item);
                _serial.Write(packet, 0, packet.Length);
            }
        }

        #endregion

        #region 1. Jig Information (CMD 0x00)

        public class FirmwareVersionInfo
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int Day { get; set; }
            public string Version { get; set; }
        }

        /// <summary>
        /// 1.1 펌웨어 버전 요청
        /// </summary>
        public FirmwareVersionInfo GetFirmwareVersion()
        {
            byte[] resp = SendAndReceive(CMD_JIG_INFO, ITEM_FIRMWARE_VERSION);
            if (resp == null || resp.Length < 5) return null;

            return new FirmwareVersionInfo
            {
                Year = 2000 + resp[0],
                Month = resp[1],
                Day = resp[2],
                Version = $"{resp[3]}.{resp[4]}"
            };
        }

        #endregion

        #region 2. Jig Signal (CMD 0x01) - PC→JIG

        /// <summary>
        /// 2.1 지그 초기화
        /// </summary>
        public bool JigInitialize()
        {
            byte[] resp = SendAndReceive(CMD_JIG_SIGNAL, ITEM_JIG_INITIALIZE);
            return resp != null;
        }

        /// <summary>
        /// 2.2 테스트 시작
        /// </summary>
        public bool TestStart()
        {
            byte[] resp = SendAndReceive(CMD_JIG_SIGNAL, ITEM_TEST_START);
            return resp != null;
        }

        /// <summary>
        /// 2.3 테스트 종료
        /// </summary>
        public bool TestStop()
        {
            byte[] resp = SendAndReceive(CMD_JIG_SIGNAL, ITEM_TEST_STOP);
            return resp != null;
        }

        #endregion

        #region 3. Control List (CMD 0x11) - PC→JIG

        /// <summary>
        /// 3.1 제품 안착 감지 (1CH~4CH, 0=없음/1=있음)
        /// </summary>
        public bool[] ProductDetect()
        {
            byte[] resp = SendAndReceive(CMD_CONTROL, ITEM_PRODUCT_DETECT);
            if (resp == null || resp.Length < 4) return null;

            return new bool[]
            {
                resp[0] == 1,
                resp[1] == 1,
                resp[2] == 1,
                resp[3] == 1
            };
        }

        /// <summary>
        /// 3.2 검사 결과 LED 표시 (1CH~4CH, 0=불량/1=양품)
        /// </summary>
        public bool ResultLedDisplay(bool ch1Pass, bool ch2Pass, bool ch3Pass, bool ch4Pass)
        {
            byte[] data = new byte[]
            {
                (byte)(ch1Pass ? 1 : 0),
                (byte)(ch2Pass ? 1 : 0),
                (byte)(ch3Pass ? 1 : 0),
                (byte)(ch4Pass ? 1 : 0)
            };
            byte[] resp = SendAndReceive(CMD_CONTROL, ITEM_RESULT_LED_DISPLAY, data);
            return resp != null;
        }

        /// <summary>
        /// 3.3 릴레이 제어 (Mode 값은 추후 정의)
        /// </summary>
        public bool RelayControl(byte mode)
        {
            byte[] resp = SendAndReceive(CMD_CONTROL, ITEM_RELAY_CONTROL, new byte[] { mode });
            return resp != null;
        }

        /// <summary>
        /// 3.4 Vcc 스위치 제어 (0=Off, 1=On)
        /// </summary>
        public bool VccConnect(bool on)
        {
            byte[] data = new byte[] { (byte)(on ? 1 : 0) };
            byte[] resp = SendAndReceive(CMD_CONTROL, ITEM_VCC_CONNECT, data);
            return resp != null;
        }

        /// <summary>
        /// 3.5 D+/D- 스위치 제어 (0=Off, 1=On)
        /// </summary>
        public bool DpDmConnect(bool on)
        {
            byte[] data = new byte[] { (byte)(on ? 1 : 0) };
            byte[] resp = SendAndReceive(CMD_CONTROL, ITEM_DP_DM_CONNECT, data);
            return resp != null;
        }

        #endregion

        #region JIG→PC 수신 처리

        /// <summary>
        /// 수신 데이터를 처리합니다. 별도 스레드에서 폴링하거나,
        /// SerialPort.DataReceived 이벤트에서 호출하세요.
        /// </summary>
        public void ProcessReceivedData()
        {
            if (!_serial.IsOpen || _serial.BytesToRead < HEADER.Length + 2 + 2 + 1)
                return;

            try
            {
                byte[] resp = ReadIncomingPacket();
                if (resp == null) return;

                byte cmd = resp[0];
                byte item = resp[1];

                // JIG Signal 수신 처리 + 에코 응답
                if (cmd == CMD_JIG_SIGNAL)
                {
                    switch (item)
                    {
                        case ITEM_JIG_START:
                            SendEcho(cmd, item);
                            OnJigStartRequested?.Invoke();
                            break;
                        case ITEM_JIG_STOP:
                            SendEcho(cmd, item);
                            OnJigStopRequested?.Invoke();
                            break;
                        case ITEM_OK_BUTTON:
                            SendEcho(cmd, item);
                            OnOkButtonPressed?.Invoke();
                            break;
                        case ITEM_NG_BUTTON:
                            SendEcho(cmd, item);
                            OnNgButtonPressed?.Invoke();
                            break;
                        case ITEM_QR_READ:
                            SendEcho(cmd, item);
                            OnQrReadRequested?.Invoke();
                            break;
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 수신 패킷 읽기 (CMD+ITEM 포함한 payload 반환)
        /// </summary>
        private byte[] ReadIncomingPacket()
        {
            int headerIdx = 0;
            while (_serial.BytesToRead > 0)
            {
                int b = _serial.ReadByte();
                if (b == HEADER[headerIdx])
                {
                    headerIdx++;
                    if (headerIdx == HEADER.Length) break;
                }
                else
                {
                    headerIdx = (b == HEADER[0]) ? 1 : 0;
                }
            }

            if (headerIdx != HEADER.Length) return null;

            int lenHigh = _serial.ReadByte();
            int lenLow = _serial.ReadByte();
            int payloadLen = (lenHigh << 8) | lenLow;

            if (payloadLen < 2) return null;

            byte[] payload = new byte[payloadLen];
            int read = 0;
            while (read < payloadLen)
            {
                int r = _serial.Read(payload, read, payloadLen - read);
                read += r;
            }

            int crc = _serial.ReadByte();

            int sum = 0;
            for (int i = 0; i < payloadLen; i++)
                sum += payload[i];
            byte expectedCrc = (byte)((sum & 0xFF) ^ CRC_XOR_KEY);

            if (crc != expectedCrc) return null;

            return payload;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Disconnect();
            if (_serial != null)
            {
                _serial.Dispose();
                _serial = null;
            }
        }

        #endregion
    }
}
