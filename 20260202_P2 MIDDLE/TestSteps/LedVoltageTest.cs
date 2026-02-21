using System;
using System.Threading;
using _20260202_P2_MIDDLE.Communications;

namespace _20260202_P2_MIDDLE.TestSteps
{
    /// <summary>
    /// LED 전압 검사 (ControlBoard ITEM 0x00 - LED Voltage ADC)
    /// 순서: Board 스위치 On → Board LED 전압 측정 → 스위치 Off → 판정
    /// </summary>
    public class LedVoltageTest : ITestStep
    {
        private readonly ControlBoardComm _board;
        private readonly double _specMin;
        private readonly double _specMax;
        private readonly byte[] _switchData;
        private readonly Action<string> _logTx;
        private readonly Action<string> _logRx;

        public string Name => "LED VOLTAGE";

        /// <param name="switchData">Board 스위치 4바이트, null이면 스위치 안 함</param>
        public LedVoltageTest(ControlBoardComm board, double specMin, double specMax,
            byte[] switchData = null,
            Action<string> logTx = null, Action<string> logRx = null)
        {
            _board = board;
            _specMin = specMin;
            _specMax = specMax;
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
                    _logTx?.Invoke($"LED VOLTAGE SwitchWrite: {BitConverter.ToString(_switchData)}");
                    bool[] switchResult = _board.SwitchWriteAll(DecodeSwitchBits(_switchData));
                    if (switchResult == null)
                    {
                        return TestResult.Fail(Name, "Board 스위치 응답 없음");
                    }
                    _logRx?.Invoke("SwitchWrite ACK OK");
                    Thread.Sleep(200);
                }

                // 2) Board LED 전압 측정
                _logTx?.Invoke("TestLedVoltage (ITEM:0x00)");

                var result = _board.TestLedVoltage();

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
}
