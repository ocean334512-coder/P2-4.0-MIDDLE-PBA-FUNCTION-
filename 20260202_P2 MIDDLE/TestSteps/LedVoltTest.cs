using System;
using System.Threading;
using _20260202_P2_MIDDLE.Communications;

namespace _20260202_P2_MIDDLE.TestSteps
{
    /// <summary>
    /// LED/다이오드 전압 검사 (D201, D202, LED VOLTAGE)
    /// 순서: Board 스위치 On → Board 전압 측정 → 스위치 Off → 판정
    /// </summary>
    public class LedVoltTest : ITestStep
    {
        private readonly ControlBoardComm _board;
        private readonly double _specMin;
        private readonly double _specMax;
        private readonly bool _isDiode;
        private readonly byte[] _switchData;
        private readonly Action<string> _logTx;
        private readonly Action<string> _logRx;

        public string Name { get; }

        /// <param name="isDiode">true=다이오드(ITEM 0x01), false=LED(ITEM 0x00)</param>
        /// <param name="switchData">Board 스위치 4바이트 [Num4, Num3, Num2, Num1], null이면 스위치 안 함</param>
        public LedVoltTest(string name, ControlBoardComm board, double specMin, double specMax,
            bool isDiode, byte[] switchData = null,
            Action<string> logTx = null, Action<string> logRx = null)
        {
            Name = name;
            _board = board;
            _specMin = specMin;
            _specMax = specMax;
            _isDiode = isDiode;
            _switchData = switchData;
            _logTx = logTx;
            _logRx = logRx;
        }

        public TestResult Execute()
        {
            try
            {
                // 1) Board 스위치 On
                if (_switchData != null)
                {
                    _logTx?.Invoke($"{Name} SwitchWrite: {BitConverter.ToString(_switchData)}");
                    bool[] switchResult = _board.SwitchWriteAll(DecodeSwitchBits(_switchData));
                    if (switchResult == null)
                    {
                        return TestResult.Fail(Name, "Board 스위치 응답 없음");
                    }
                    _logRx?.Invoke("SwitchWrite ACK OK");
                    Thread.Sleep(200);
                }

                // 2) Board 전압 측정
                string itemDesc = _isDiode ? "DiodeVoltage (ITEM:0x01)" : "LedVoltage (ITEM:0x00)";
                _logTx?.Invoke($"TestLedVolt {itemDesc}");

                var result = _isDiode ? _board.TestDiodeVoltage() : _board.TestLedVoltage();

                if (result == null)
                {
                    SwitchReset();
                    return TestResult.Fail(Name, "응답 없음 (Timeout)");
                }

                double voltV = result.VoltageV;
                _logRx?.Invoke($"{result.VoltageMv} mV ({voltV:F3} V)");

                // 3) Board 스위치 Off
                SwitchReset();

                // 4) 판정
                return TestResult.Judge(Name, voltV, "V", _specMin, _specMax);
            }
            catch (Exception ex)
            {
                SwitchReset();
                return TestResult.Fail(Name, ex.Message);
            }
        }

        private void SwitchReset()
        {
            if (_switchData == null) return;
            try
            {
                _logTx?.Invoke("SwitchWrite Reset (All Off)");
                _board.SwitchWriteAll(new bool[27]);
                _logRx?.Invoke("SwitchReset ACK OK");
            }
            catch { }
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
    /// D201/D202/LED VOLTAGE 스위치 데이터
    /// </summary>
    public static class VoltSwitchMap
    {
        public static readonly byte[] D201        = { 0x00, 0xA0, 0x00, 0x00 };
        public static readonly byte[] D202        = { 0x00, 0x0A, 0x00, 0x00 };
        // LED VOLTAGE 스위치 데이터는 확인 후 추가
        // public static readonly byte[] LED_VOLTAGE = { ... };
    }
}
