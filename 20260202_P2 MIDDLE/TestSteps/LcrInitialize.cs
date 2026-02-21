using System;
using _20260202_P2_MIDDLE.Communications;

namespace _20260202_P2_MIDDLE.TestSteps
{
    /// <summary>
    /// LCR Meter 초기화 (검사 시작 전 1회 실행)
    /// 1) 계측기 정보 확인 (*IDN?)
    /// 2) 주파수 설정 10kHz
    /// 3) 트리거 BUS 모드 설정
    /// </summary>
    public class LcrInitialize : ITestStep
    {
        private readonly LcrMeterComm _lcr;
        private readonly Action<string> _logTx;
        private readonly Action<string> _logRx;

        public string Name => "LCR Initialize";

        public string Identification { get; private set; }

        public LcrInitialize(LcrMeterComm lcr,
            Action<string> logTx = null, Action<string> logRx = null)
        {
            _lcr = lcr;
            _logTx = logTx;
            _logRx = logRx;
        }

        public TestResult Execute()
        {
            try
            {
                // 1) 계측기 정보 확인
                _logTx?.Invoke("*IDN?");
                Identification = _lcr.GetIdentification();
                if (string.IsNullOrEmpty(Identification))
                    return TestResult.Fail(Name, "LCR 계측기 응답 없음");
                _logRx?.Invoke(Identification);

                // 2) 주파수 설정 10kHz
                _logTx?.Invoke("FREQ 10000");
                _lcr.SetFrequency(10000);
                var freq = _lcr.GetFrequency();
                _logRx?.Invoke($"FREQ = {freq}");

                // 3) 트리거 BUS 모드 설정
                _logTx?.Invoke("TRIG:SOUR BUS");
                _lcr.SetTriggerSourceBus();
                var src = _lcr.GetTriggerSource();
                _logRx?.Invoke($"TRIG:SOUR = {src}");

                bool success = src != null && src.Contains("BUS");

                if (success)
                {
                    return new TestResult
                    {
                        ItemName = Name,
                        MeasuredValue = 1,
                        Unit = "",
                        Pass = true
                    };
                }
                else
                {
                    return TestResult.Fail(Name, "트리거 모드 설정 실패");
                }
            }
            catch (Exception ex)
            {
                _logRx?.Invoke($"초기화 실패: {ex.Message}");
                return TestResult.Fail(Name, ex.Message);
            }
        }
    }
}
