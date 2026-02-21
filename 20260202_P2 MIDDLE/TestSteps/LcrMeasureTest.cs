using System;
using System.Threading;
using _20260202_P2_MIDDLE.Communications;

namespace _20260202_P2_MIDDLE.TestSteps
{
    /// <summary>
    /// LCR 측정 모드
    /// </summary>
    public enum LcrMode
    {
        DCR,
        CpD
    }

    /// <summary>
    /// LCR Meter 개별 항목 검사
    /// 순서: Board 스위치 On → LCR 모드변경 + 측정 → Board 스위치 Off → 판정
    /// </summary>
    public class LcrMeasureTest : ITestStep
    {
        private readonly LcrMeterComm _lcr;
        private readonly ControlBoardComm _board;
        private readonly LcrMode _mode;
        private readonly byte[] _switchData;
        private readonly double _specMin;
        private readonly double _specMax;
        private readonly string _unit;
        private readonly Action<string> _logLcrTx;
        private readonly Action<string> _logLcrRx;
        private readonly Action<string> _logBoardTx;
        private readonly Action<string> _logBoardRx;

        public string Name { get; }

        /// <summary>
        /// LCR 측정 검사 항목 생성
        /// </summary>
        /// <param name="switchData">Board 스위치 4바이트 [Num4, Num3, Num2, Num1]</param>
        public LcrMeasureTest(
            string name,
            LcrMeterComm lcr,
            ControlBoardComm board,
            LcrMode mode,
            byte[] switchData,
            double specMin,
            double specMax,
            string unit,
            Action<string> logLcrTx = null, Action<string> logLcrRx = null,
            Action<string> logBoardTx = null, Action<string> logBoardRx = null)
        {
            Name = name;
            _lcr = lcr;
            _board = board;
            _mode = mode;
            _switchData = switchData;
            _specMin = specMin;
            _specMax = specMax;
            _unit = unit;
            _logLcrTx = logLcrTx;
            _logLcrRx = logLcrRx;
            _logBoardTx = logBoardTx;
            _logBoardRx = logBoardRx;
        }

        public TestResult Execute()
        {
            try
            {
                // 1) Board 스위치 On
                _logBoardTx?.Invoke($"{Name} SwitchWrite: {BitConverter.ToString(_switchData)}");
                bool[] switchResult = _board.SwitchWriteAll(DecodeSwitchBits(_switchData));
                if (switchResult == null)
                    return TestResult.Fail(Name, "Board 스위치 응답 없음");
                _logBoardRx?.Invoke("SwitchWrite ACK OK");

                Thread.Sleep(200);

                // 2) LCR 모드 변경 + 측정
                double measuredValue;
                if (_mode == LcrMode.DCR)
                {
                    _logLcrTx?.Invoke("FUNC DCR");
                    _lcr.SetModeDCR();
                    Thread.Sleep(100);

                    _logLcrTx?.Invoke("*TRG");
                    var dcr = _lcr.MeasureDCR();
                    if (dcr == null)
                    {
                        SwitchReset();
                        return TestResult.Fail(Name, "LCR DCR 측정 응답 없음");
                    }
                    measuredValue = dcr.Resistance;
                    _logLcrRx?.Invoke($"{dcr.RawResponse}");
                }
                else
                {
                    _logLcrTx?.Invoke("FUNC Cp-D");
                    _lcr.SetModeCpD();
                    Thread.Sleep(100);

                    _logLcrTx?.Invoke("*TRG");
                    var cpd = _lcr.MeasureCpD();
                    if (cpd == null)
                    {
                        SwitchReset();
                        return TestResult.Fail(Name, "LCR Cp-D 측정 응답 없음");
                    }

                    measuredValue = ConvertCapacitance(cpd.Capacitance);
                    _logLcrRx?.Invoke($"{cpd.RawResponse}");
                }

                // 3) Board 스위치 초기화 (전부 Off)
                SwitchReset();

                // 4) 판정
                return TestResult.Judge(Name, measuredValue, _unit, _specMin, _specMax);
            }
            catch (Exception ex)
            {
                SwitchReset();
                return TestResult.Fail(Name, ex.Message);
            }
        }

        private void SwitchReset()
        {
            try
            {
                _logBoardTx?.Invoke("SwitchWrite Reset (All Off)");
                _board.SwitchWriteAll(new bool[27]);
                _logBoardRx?.Invoke("SwitchReset ACK OK");
            }
            catch { }
        }

        /// <summary>
        /// 단위에 맞게 커패시턴스 변환 (F → nF 또는 uF)
        /// </summary>
        private double ConvertCapacitance(double farad)
        {
            if (_unit == "nF") return farad * 1e9;
            if (_unit == "uF") return farad * 1e6;
            if (_unit == "pF") return farad * 1e12;
            return farad;
        }

        private bool[] DecodeSwitchBits(byte[] data)
        {
            bool[] switches = new bool[27];
            for (int i = 0; i < 27; i++)
            {
                int byteIdx = 3 - (i / 8);
                int bitInByte = i % 8;
                switches[i] = (data[byteIdx] & (1 << bitInByte)) != 0;
            }
            return switches;
        }
    }

    /// <summary>
    /// 항목별 스위치 데이터 정의
    /// [Num4, Num3, Num2, Num1]
    /// </summary>
    public static class LcrSwitchMap
    {
        public static readonly byte[] R202     = { 0x00, 0x00, 0x08, 0x01 };
        public static readonly byte[] R203     = { 0x00, 0x00, 0x10, 0x02 };
        public static readonly byte[] R204     = { 0x00, 0x00, 0x20, 0x04 };
        public static readonly byte[] R205     = { 0x00, 0x00, 0x40, 0x08 };
        public static readonly byte[] C203     = { 0x00, 0x00, 0x04, 0x10 };
        public static readonly byte[] C207     = { 0x00, 0x00, 0x04, 0x20 };
        public static readonly byte[] RT201    = { 0x00, 0x00, 0x04, 0x20 };
        public static readonly byte[] R207     = { 0x00, 0x00, 0x02, 0x20 };
        public static readonly byte[] C204_206 = { 0x00, 0x00, 0x04, 0x40 };
        public static readonly byte[] C208     = { 0x00, 0x80, 0x00, 0x80 };
        public static readonly byte[] L201     = { 0x00, 0x80, 0x01, 0x00 };
        public static readonly byte[] R206     = { 0x00, 0xA0, 0x00, 0x00 };
    }
}
