using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace _20260202_P2_MIDDLE.Communications
{
    /// <summary>
    /// Control PCB Board TCP/IP 통신 클래스 (채널별 1개 인스턴스)
    /// Protocol: Header(ITM) + LEN(2byte BE) + CMD + ITEM + DATA... + CRC
    /// Float: IEEE-754 single precision, little-endian
    /// </summary>
    public class ControlBoardComm : IDeviceComm, IDisposable
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private readonly object _lock = new object();

        private readonly string _ip;
        private readonly int _port;
        private const int DEFAULT_TIMEOUT_MS = 5000;

        #region Header / CRC 상수

        private static readonly byte[] HEADER = { 0x49, 0x54, 0x4D }; // 'I','T','M'
        private const byte CRC_XOR_KEY = 0x55;

        #endregion

        #region CMD 상수

        public const byte CMD_TESTER_INFO       = 0x00;
        public const byte CMD_TESTER_INIT       = 0x01;
        public const byte CMD_TEST_OPEN_SHORT   = 0xA1;
        public const byte CMD_SWITCH_CONTROL    = 0xA2;
        public const byte CMD_TEST_PRESSURE     = 0xA3;
        public const byte CMD_TEST_COLOR        = 0xA4;
        public const byte CMD_TEST_LED_VOLT     = 0xA5;

        #endregion

        #region ITEM 상수

        public const byte ITEM_FIRMWARE_VERSION = 0x00;

        public const byte ITEM_TESTER_INIT      = 0x00;
        public const byte ITEM_TESTER_RESTART   = 0xFF;

        public const byte ITEM_OPEN_SHORT       = 0x00;

        public const byte ITEM_SWITCH_READ_ALL  = 0x00;
        public const byte ITEM_SWITCH_WRITE_ALL = 0x01;

        public const byte ITEM_PRESSURE         = 0x00;

        public const byte ITEM_COLOR            = 0x00;

        public const byte ITEM_LED_VOLTAGE      = 0x00;
        public const byte ITEM_DIODE_VOLTAGE    = 0x01;

        #endregion

        #region 채널 기본 설정

        public static readonly (string Ip, int Port)[] DefaultChannels = new[]
        {
            ("192.168.0.101", 5001),
            ("192.168.0.102", 5002),
            ("192.168.0.103", 5003),
            ("192.168.0.104", 5004)
        };

        #endregion

        #region 결과 클래스

        public class FirmwareVersionInfo
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int Day { get; set; }
            public string Version { get; set; }
        }

        public class OpenShortResult
        {
            public byte Connector { get; set; }
            public byte PinNum { get; set; }
            public byte[] Results { get; set; }
        }

        public const byte RESULT_OPEN = 0x01;
        public const byte RESULT_SHORT = 0x02;
        public const byte RESULT_UNUSED = 0xFF;

        public class PressureResult
        {
            public byte ErrorCode { get; set; }
            public float Pressure { get; set; }
            public bool IsOk => ErrorCode == 0x00;
        }

        public class ColorResult
        {
            public byte ErrorCode { get; set; }
            public float Red { get; set; }
            public float Green { get; set; }
            public float Blue { get; set; }
            public bool IsOk => ErrorCode == 0x00;
        }

        public class LedVoltResult
        {
            public uint VoltageMv { get; set; }
            public double VoltageV => VoltageMv / 1000.0;
        }

        #endregion

        public bool IsConnected => _client?.Connected ?? false;

        public ControlBoardComm(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        /// <summary>
        /// 채널 번호로 생성 (1~4)
        /// </summary>
        public ControlBoardComm(int channel)
        {
            if (channel < 1 || channel > 4)
                throw new ArgumentOutOfRangeException(nameof(channel), "채널 번호는 1~4");
            _ip = DefaultChannels[channel - 1].Ip;
            _port = DefaultChannels[channel - 1].Port;
        }

        #region 연결 / 해제

        public bool Connect()
        {
            try
            {
                if (_client != null && _client.Connected) return true;
                _client = new TcpClient();
                var result = _client.BeginConnect(_ip, _port, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(DEFAULT_TIMEOUT_MS);
                if (!success)
                {
                    _client.Close();
                    _client = null;
                    return false;
                }
                _client.EndConnect(result);
                _stream = _client.GetStream();
                _stream.ReadTimeout = DEFAULT_TIMEOUT_MS;
                _stream.WriteTimeout = DEFAULT_TIMEOUT_MS;
                return true;
            }
            catch
            {
                _client = null;
                _stream = null;
                return false;
            }
        }

        public void Disconnect()
        {
            try
            {
                _stream?.Close();
                _client?.Close();
            }
            catch { }
            finally
            {
                _stream = null;
                _client = null;
            }
        }

        #endregion

        #region 패킷 빌드 / 파싱

        private byte[] BuildPacket(byte cmd, byte item, params byte[] data)
        {
            int payloadLen = 2 + (data?.Length ?? 0);
            int packetLen = HEADER.Length + 2 + payloadLen + 1;

            byte[] packet = new byte[packetLen];
            int idx = 0;

            Array.Copy(HEADER, 0, packet, idx, HEADER.Length);
            idx += HEADER.Length;

            packet[idx++] = (byte)((payloadLen >> 8) & 0xFF);
            packet[idx++] = (byte)(payloadLen & 0xFF);

            packet[idx++] = cmd;
            packet[idx++] = item;

            if (data != null && data.Length > 0)
            {
                Array.Copy(data, 0, packet, idx, data.Length);
                idx += data.Length;
            }

            int sum = cmd + item;
            if (data != null)
                for (int i = 0; i < data.Length; i++)
                    sum += data[i];
            packet[idx] = (byte)((sum & 0xFF) ^ CRC_XOR_KEY);

            return packet;
        }

        private byte[] SendAndReceive(byte cmd, byte item, byte[] data = null, int timeoutMs = DEFAULT_TIMEOUT_MS)
        {
            lock (_lock)
            {
                byte[] packet = BuildPacket(cmd, item, data);
                _stream.Write(packet, 0, packet.Length);
                _stream.Flush();

                return ReadResponse(cmd, item, timeoutMs);
            }
        }

        private byte[] ReadResponse(byte expectedCmd, byte expectedItem, int timeoutMs)
        {
            _stream.ReadTimeout = timeoutMs;
            DateTime deadline = DateTime.Now.AddMilliseconds(timeoutMs);

            int headerIdx = 0;
            while (DateTime.Now < deadline)
            {
                int b = _stream.ReadByte();
                if (b < 0) return null;
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

            int lenHigh = _stream.ReadByte();
            int lenLow = _stream.ReadByte();
            int payloadLen = (lenHigh << 8) | lenLow;

            if (payloadLen < 2) return null;

            byte[] payload = new byte[payloadLen];
            int read = 0;
            while (read < payloadLen && DateTime.Now < deadline)
            {
                int r = _stream.Read(payload, read, payloadLen - read);
                if (r <= 0) return null;
                read += r;
            }

            int crc = _stream.ReadByte();

            int sum = 0;
            for (int i = 0; i < payloadLen; i++)
                sum += payload[i];
            byte expectedCrc = (byte)((sum & 0xFF) ^ CRC_XOR_KEY);

            if (crc != expectedCrc) return null;

            if (payload[0] != expectedCmd || payload[1] != expectedItem) return null;

            byte[] respData = new byte[payloadLen - 2];
            if (respData.Length > 0)
                Array.Copy(payload, 2, respData, 0, respData.Length);

            return respData;
        }

        /// <summary>
        /// float (IEEE-754 LE) 파싱
        /// </summary>
        private float ReadFloatLE(byte[] data, int offset)
        {
            if (BitConverter.IsLittleEndian)
                return BitConverter.ToSingle(data, offset);

            byte[] tmp = new byte[4];
            Array.Copy(data, offset, tmp, 0, 4);
            Array.Reverse(tmp);
            return BitConverter.ToSingle(tmp, 0);
        }

        /// <summary>
        /// uint32 (big-endian) 파싱
        /// </summary>
        private uint ReadUint32BE(byte[] data, int offset)
        {
            return (uint)((data[offset] << 24) | (data[offset + 1] << 16) |
                          (data[offset + 2] << 8) | data[offset + 3]);
        }

        #endregion

        #region 1. Tester Information (CMD 0x00)

        /// <summary>
        /// 펌웨어 버전 요청
        /// </summary>
        public FirmwareVersionInfo GetFirmwareVersion()
        {
            byte[] resp = SendAndReceive(CMD_TESTER_INFO, ITEM_FIRMWARE_VERSION);
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

        #region 2. Tester Initialize (CMD 0x01)

        /// <summary>
        /// 테스터 초기화
        /// </summary>
        public bool TesterInitialize()
        {
            byte[] resp = SendAndReceive(CMD_TESTER_INIT, ITEM_TESTER_INIT);
            return resp != null;
        }

        /// <summary>
        /// 테스터 리셋 (치명적 오류시에만 수동 사용, ACK 미수신 가능)
        /// </summary>
        public void TesterRestart()
        {
            lock (_lock)
            {
                byte[] packet = BuildPacket(CMD_TESTER_INIT, ITEM_TESTER_RESTART);
                _stream.Write(packet, 0, packet.Length);
                _stream.Flush();
            }
        }

        #endregion

        #region 3. Test Open/Short (CMD 0xA1)

        /// <summary>
        /// Open/Short 테스트
        /// </summary>
        /// <param name="connector">주 핀 커넥터 번호 (CON1~CON3)</param>
        /// <param name="pinNum">테스트할 주 핀 번호</param>
        /// <param name="testPins">비교할 핀 배열 (connector, pinNum 쌍). 최대 30개, 미사용은 자동 0xFF 채움</param>
        public OpenShortResult TestOpenShort(byte connector, byte pinNum, (byte con, byte pin)[] testPins)
        {
            byte[] data = new byte[2 + 30 * 2]; // CON+PIN + 30 pairs
            for (int i = 0; i < data.Length; i++) data[i] = 0xFF;

            data[0] = connector;
            data[1] = pinNum;

            for (int i = 0; i < testPins.Length && i < 30; i++)
            {
                data[2 + i * 2] = testPins[i].con;
                data[2 + i * 2 + 1] = testPins[i].pin;
            }

            byte[] resp = SendAndReceive(CMD_TEST_OPEN_SHORT, ITEM_OPEN_SHORT, data);
            if (resp == null) return null;

            // 응답: CON + PIN NUM + (CON + RESULT) * 30
            byte[] results = new byte[30];
            for (int i = 0; i < 30 && (2 + i * 2 + 1) < resp.Length; i++)
            {
                results[i] = resp[2 + i * 2 + 1]; // RESULT 값만 추출
            }

            return new OpenShortResult
            {
                Connector = resp.Length > 0 ? resp[0] : (byte)0,
                PinNum = resp.Length > 1 ? resp[1] : (byte)0,
                Results = results
            };
        }

        #endregion

        #region 4. Switch Control (CMD 0xA2)

        /// <summary>
        /// 전체 스위치 상태 읽기 (스위치 1~27)
        /// </summary>
        public bool[] SwitchReadAll()
        {
            byte[] resp = SendAndReceive(CMD_SWITCH_CONTROL, ITEM_SWITCH_READ_ALL);
            if (resp == null || resp.Length < 4) return null;

            return DecodeSwitchBits(resp);
        }

        /// <summary>
        /// 전체 스위치 상태 쓰기 (스위치 1~27)
        /// </summary>
        public bool[] SwitchWriteAll(bool[] switches)
        {
            byte[] data = EncodeSwitchBits(switches);
            byte[] resp = SendAndReceive(CMD_SWITCH_CONTROL, ITEM_SWITCH_WRITE_ALL, data);
            if (resp == null || resp.Length < 4) return null;

            return DecodeSwitchBits(resp);
        }

        /// <summary>
        /// 특정 스위치 하나만 On/Off (현재 상태를 읽고 변경 후 쓰기)
        /// </summary>
        public bool[] SetSwitch(int switchNum, bool on)
        {
            bool[] current = SwitchReadAll();
            if (current == null) return null;

            if (switchNum >= 1 && switchNum <= current.Length)
                current[switchNum - 1] = on;

            return SwitchWriteAll(current);
        }

        private bool[] DecodeSwitchBits(byte[] data)
        {
            bool[] switches = new bool[27];
            // Num1: bits 1~8, Num2: bits 9~16, Num3: bits 17~24, Num4: bits 25~27
            uint bits = (uint)((data[0] << 24) | (data[1] << 16) | (data[2] << 8) | data[3]);

            for (int i = 0; i < 27; i++)
            {
                int bitPos = i; // bit 0 = switch 1
                int byteIdx = 3 - (i / 8);   // Num1(idx3) → Num4(idx0)
                int bitInByte = i % 8;
                switches[i] = (data[byteIdx] & (1 << bitInByte)) != 0;
            }

            return switches;
        }

        private byte[] EncodeSwitchBits(bool[] switches)
        {
            byte[] data = new byte[4];
            for (int i = 0; i < switches.Length && i < 27; i++)
            {
                if (!switches[i]) continue;
                int byteIdx = 3 - (i / 8);
                int bitInByte = i % 8;
                data[byteIdx] |= (byte)(1 << bitInByte);
            }
            return data;
        }

        #endregion

        #region 5. Test Pressure Sensor (CMD 0xA3)

        /// <summary>
        /// 압력센서 테스트 (Pa 단위 float)
        /// </summary>
        public PressureResult TestPressure()
        {
            byte[] resp = SendAndReceive(CMD_TEST_PRESSURE, ITEM_PRESSURE);
            if (resp == null || resp.Length < 5) return null;

            return new PressureResult
            {
                ErrorCode = resp[0],
                Pressure = ReadFloatLE(resp, 1)
            };
        }

        #endregion

        #region 6. Test Color Sensor (CMD 0xA4)

        /// <summary>
        /// 컬러센서 테스트 (R/G/B Lux, float)
        /// </summary>
        public ColorResult TestColor()
        {
            byte[] resp = SendAndReceive(CMD_TEST_COLOR, ITEM_COLOR);
            if (resp == null || resp.Length < 13) return null;

            return new ColorResult
            {
                ErrorCode = resp[0],
                Red = ReadFloatLE(resp, 1),
                Green = ReadFloatLE(resp, 5),
                Blue = ReadFloatLE(resp, 9)
            };
        }

        #endregion

        #region 7. Test LED Volt (CMD 0xA5)

        /// <summary>
        /// LED 전압 측정 (mV, uint32 BE)
        /// </summary>
        public LedVoltResult TestLedVoltage()
        {
            byte[] resp = SendAndReceive(CMD_TEST_LED_VOLT, ITEM_LED_VOLTAGE);
            if (resp == null || resp.Length < 4) return null;

            return new LedVoltResult { VoltageMv = ReadUint32BE(resp, 0) };
        }

        /// <summary>
        /// 다이오드 전압 측정 (mV, uint32 BE)
        /// </summary>
        public LedVoltResult TestDiodeVoltage()
        {
            byte[] resp = SendAndReceive(CMD_TEST_LED_VOLT, ITEM_DIODE_VOLTAGE);
            if (resp == null || resp.Length < 4) return null;

            return new LedVoltResult { VoltageMv = ReadUint32BE(resp, 0) };
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Disconnect();
        }

        #endregion
    }
}
