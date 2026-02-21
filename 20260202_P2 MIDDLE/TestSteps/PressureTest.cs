using System;
using _20260202_P2_MIDDLE.Communications;

namespace _20260202_P2_MIDDLE.TestSteps
{
    /// <summary>
    /// PRESSURE 검사: Control Board 압력센서 측정 후 SPEC 판정
    /// </summary>
    public class PressureTest : ITestStep
    {
        private readonly ControlBoardComm _board;
        private readonly double _specMin;
        private readonly double _specMax;
        private readonly Action<string> _logTx;
        private readonly Action<string> _logRx;

        public string Name => "PRESSURE";

        public PressureTest(ControlBoardComm board, double specMin, double specMax,
            Action<string> logTx = null, Action<string> logRx = null)
        {
            _board = board;
            _specMin = specMin;
            _specMax = specMax;
            _logTx = logTx;
            _logRx = logRx;
        }

        public TestResult Execute()
        {
            try
            {
                _logTx?.Invoke("TestPressure (CMD:0xA3 ITEM:0x00)");

                var result = _board.TestPressure();

                if (result == null)
                    return TestResult.Fail(Name, "응답 없음 (Timeout)");

                _logRx?.Invoke($"ErrorCode:0x{result.ErrorCode:X2}, Pressure:{result.Pressure:F2} Pa");

                if (!result.IsOk)
                    return TestResult.Fail(Name, $"센서 에러 (ErrorCode: 0x{result.ErrorCode:X2})");

                return TestResult.Judge(Name, result.Pressure, "Pa", _specMin, _specMax);
            }
            catch (Exception ex)
            {
                return TestResult.Fail(Name, ex.Message);
            }
        }
    }
}
