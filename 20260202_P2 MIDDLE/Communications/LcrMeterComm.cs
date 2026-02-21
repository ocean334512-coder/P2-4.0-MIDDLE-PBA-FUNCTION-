using System;
using System.Globalization;
using System.IO.Ports;
using System.Threading;

namespace _20260202_P2_MIDDLE.Communications
{
    /// <summary>
    /// GW Instek LCR-6100 시리즈 RS232 통신 클래스
    /// SCPI 텍스트 기반 프로토콜
    /// </summary>
    public class LcrMeterComm : IDeviceComm, IDisposable
    {
        private SerialPort _serial;
        private readonly object _lock = new object();

        private const int DEFAULT_TIMEOUT_MS = 5000;
        private const string TERMINATOR = "\n";

        #region 측정 모드 상수

        public const string MODE_DCR = "DCR";
        public const string MODE_CP_D = "Cp-D";

        #endregion

        #region 측정 결과 클래스

        /// <summary>
        /// DCR 모드 측정 결과
        /// </summary>
        public class DcrResult
        {
            public double Resistance { get; set; }
            public string RangeStatus { get; set; }
            public string Judgment { get; set; }
            public string RawResponse { get; set; }
        }

        /// <summary>
        /// Cp-D 모드 측정 결과
        /// </summary>
        public class CpDResult
        {
            public double Capacitance { get; set; }
            public double DissipationFactor { get; set; }
            public string RangeStatus { get; set; }
            public string AuxJudgment { get; set; }
            public string Judgment { get; set; }
            public string RawResponse { get; set; }

            /// <summary>
            /// 커패시턴스를 pF 단위로 반환
            /// </summary>
            public double CapacitancePF => Capacitance * 1e12;

            /// <summary>
            /// 커패시턴스를 nF 단위로 반환
            /// </summary>
            public double CapacitanceNF => Capacitance * 1e9;

            /// <summary>
            /// 커패시턴스를 uF 단위로 반환
            /// </summary>
            public double CapacitanceUF => Capacitance * 1e6;
        }

        #endregion

        public bool IsConnected => _serial?.IsOpen ?? false;

        public LcrMeterComm(string portName, int baudRate = 115200)
        {
            _serial = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One)
            {
                ReadTimeout = DEFAULT_TIMEOUT_MS,
                WriteTimeout = DEFAULT_TIMEOUT_MS,
                NewLine = "\n"
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

        #region 기본 송수신

        /// <summary>
        /// 명령 전송 (응답 없는 설정 명령용)
        /// </summary>
        private void SendCommand(string command)
        {
            lock (_lock)
            {
                _serial.DiscardInBuffer();
                _serial.Write(command + TERMINATOR);
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// 쿼리 전송 후 응답 수신 (? 포함 조회 명령용)
        /// </summary>
        private string SendQuery(string command)
        {
            lock (_lock)
            {
                _serial.DiscardInBuffer();
                _serial.Write(command + TERMINATOR);
                try
                {
                    string response = _serial.ReadLine().Trim();
                    return response;
                }
                catch (TimeoutException)
                {
                    return null;
                }
            }
        }

        #endregion

        #region 1. 기기 정보 확인

        /// <summary>
        /// 기기 정보 조회 (*IDN?)
        /// 응답 예: "LCR-6100,REV E8.13,GEZ883880,Good Will Instrument Co., Ltd."
        /// </summary>
        public string GetIdentification()
        {
            return SendQuery("*IDN?");
        }

        #endregion

        #region 2-3. 주파수 설정/확인

        /// <summary>
        /// 측정 주파수 설정 (Hz 단위)
        /// </summary>
        public void SetFrequency(double frequencyHz)
        {
            SendCommand($"FREQ {frequencyHz.ToString(CultureInfo.InvariantCulture)}");
        }

        /// <summary>
        /// 현재 주파수 조회 (Hz 단위로 반환)
        /// </summary>
        public double? GetFrequency()
        {
            string resp = SendQuery("FREQ?");
            if (resp != null && double.TryParse(resp, NumberStyles.Float, CultureInfo.InvariantCulture, out double freq))
                return freq;
            return null;
        }

        #endregion

        #region 4-5. 측정 모드 설정/확인

        /// <summary>
        /// 측정 모드 설정 (예: "DCR", "Cp-D")
        /// </summary>
        public void SetFunction(string mode)
        {
            SendCommand($"FUNC {mode}");
        }

        /// <summary>
        /// 현재 측정 모드 조회
        /// </summary>
        public string GetFunction()
        {
            return SendQuery("FUNC?");
        }

        /// <summary>
        /// DCR 모드로 설정
        /// </summary>
        public void SetModeDCR()
        {
            SetFunction(MODE_DCR);
        }

        /// <summary>
        /// Cp-D 모드로 설정
        /// </summary>
        public void SetModeCpD()
        {
            SetFunction(MODE_CP_D);
        }

        #endregion

        #region 6, 9. 트리거 소스 설정/확인

        /// <summary>
        /// 트리거 소스를 BUS 모드로 설정 (PC에서 *TRG로 측정 제어)
        /// </summary>
        public void SetTriggerSourceBus()
        {
            SendCommand("TRIG:SOUR BUS");
        }

        /// <summary>
        /// 현재 트리거 소스 조회
        /// </summary>
        public string GetTriggerSource()
        {
            return SendQuery("TRIG:SOUR?");
        }

        #endregion

        #region 10. 측정 실행 (*TRG)

        /// <summary>
        /// 측정 트리거 실행 후 원본 응답 문자열 반환
        /// </summary>
        public string Trigger()
        {
            return SendQuery("*TRG");
        }

        /// <summary>
        /// DCR 모드 측정 실행
        /// 응답 예: "+4.92312e+03,OUT ,OK"
        /// </summary>
        public DcrResult MeasureDCR()
        {
            string resp = SendQuery("*TRG");
            if (string.IsNullOrEmpty(resp)) return null;

            try
            {
                string[] parts = resp.Split(',');
                if (parts.Length < 3) return null;

                double.TryParse(parts[0].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double resistance);

                return new DcrResult
                {
                    Resistance = resistance,
                    RangeStatus = parts[1].Trim(),
                    Judgment = parts[2].Trim(),
                    RawResponse = resp
                };
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Cp-D 모드 측정 실행
        /// 응답 예: "+4.64627e-10,+3.88413e-03,OUT ,AUX-NG,NG"
        /// </summary>
        public CpDResult MeasureCpD()
        {
            string resp = SendQuery("*TRG");
            if (string.IsNullOrEmpty(resp)) return null;

            try
            {
                string[] parts = resp.Split(',');
                if (parts.Length < 5) return null;

                double.TryParse(parts[0].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double capacitance);
                double.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double dissipation);

                return new CpDResult
                {
                    Capacitance = capacitance,
                    DissipationFactor = dissipation,
                    RangeStatus = parts[2].Trim(),
                    AuxJudgment = parts[3].Trim(),
                    Judgment = parts[4].Trim(),
                    RawResponse = resp
                };
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 편의 메서드 (모드 변경 + 측정 한번에)

        /// <summary>
        /// DCR 모드로 변경 후 저항 측정 (Ω 단위)
        /// </summary>
        public DcrResult MeasureResistance(double? frequencyHz = null)
        {
            SetModeDCR();
            if (frequencyHz.HasValue)
                SetFrequency(frequencyHz.Value);
            Thread.Sleep(100);
            return MeasureDCR();
        }

        /// <summary>
        /// Cp-D 모드로 변경 후 커패시턴스 측정
        /// </summary>
        public CpDResult MeasureCapacitance(double? frequencyHz = null)
        {
            SetModeCpD();
            if (frequencyHz.HasValue)
                SetFrequency(frequencyHz.Value);
            Thread.Sleep(100);
            return MeasureCpD();
        }

        #endregion

        #region 초기화 (검사 시작 전 세팅)

        /// <summary>
        /// LCR Meter 초기 설정 (트리거 BUS 모드)
        /// </summary>
        public bool Initialize()
        {
            try
            {
                SetTriggerSourceBus();
                string src = GetTriggerSource();
                return src != null && src.Contains("BUS");
            }
            catch
            {
                return false;
            }
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
