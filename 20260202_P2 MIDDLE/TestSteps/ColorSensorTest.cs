using System;
using System.Threading;
using _20260202_P2_MIDDLE.Communications;

namespace _20260202_P2_MIDDLE.TestSteps
{
    /// <summary>
    /// 컬러센서 검사: Board 스위치 On → 컬러센서 측정 → 스위치 Off → SPEC 판정
    /// 응답: ErrorCode + Red(Lux) + Green(Lux) + Blue(Lux)
    /// </summary>
    public class ColorSensorTest : ITestStep
    {
        private readonly ControlBoardComm _board;
        private readonly double _specMin;
        private readonly double _specMax;
        private readonly byte[] _switchData;
        private readonly Action<string> _logTx;
        private readonly Action<string> _logRx;

        public string Name => "COLOR SENSOR";

        public float Red { get; private set; }
        public float Green { get; private set; }
        public float Blue { get; private set; }

        /// <param name="switchData">Board 스위치 4바이트, null이면 스위치 안 함</param>
        public ColorSensorTest(ControlBoardComm board, double specMin, double specMax,
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
                    _logTx?.Invoke($"COLOR SENSOR SwitchWrite: {BitConverter.ToString(_switchData)}");
                    bool[] switchResult = _board.SwitchWriteAll(DecodeSwitchBits(_switchData));
                    if (switchResult == null)
                    {
                        return TestResult.Fail(Name, "Board 스위치 응답 없음");
                    }
                    _logRx?.Invoke("SwitchWrite ACK OK");
                    Thread.Sleep(200);
                }

                // 2) Board 컬러센서 측정
                _logTx?.Invoke("TestColor (CMD:0xA4 ITEM:0x00)");

                var result = _board.TestColor();

                if (result == null)
                {
                    SwitchReset();
                    return TestResult.Fail(Name, "응답 없음 (Timeout)");
                }

                _logRx?.Invoke($"ErrorCode:0x{result.ErrorCode:X2}, R:{result.Red:F2} G:{result.Green:F2} B:{result.Blue:F2} Lux");

                if (!result.IsOk)
                {
                    SwitchReset();
                    return TestResult.Fail(Name, $"센서 에러 (ErrorCode: 0x{result.ErrorCode:X2})");
                }

                Red = result.Red;
                Green = result.Green;
                Blue = result.Blue;

                double totalLux = result.Red + result.Green + result.Blue;

                // 3) Board 스위치 Off
                SwitchReset();

                // 4) 판정
                return TestResult.Judge(Name, totalLux, "Lux", _specMin, _specMax);
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
