using System;

namespace _20260202_P2_MIDDLE.TestSteps
{
    /// <summary>
    /// 개별 검사 항목의 결과
    /// </summary>
    public class TestResult
    {
        public string ItemName { get; set; }
        public double MeasuredValue { get; set; }
        public string Unit { get; set; }
        public double SpecMin { get; set; }
        public double SpecMax { get; set; }
        public bool Pass { get; set; }
        public string ErrorMessage { get; set; }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        /// <summary>
        /// 측정값과 SPEC MIN/MAX로 판정
        /// </summary>
        public static TestResult Judge(string itemName, double measuredValue, string unit, double specMin, double specMax)
        {
            return new TestResult
            {
                ItemName = itemName,
                MeasuredValue = measuredValue,
                Unit = unit,
                SpecMin = specMin,
                SpecMax = specMax,
                Pass = measuredValue >= specMin && measuredValue <= specMax
            };
        }

        /// <summary>
        /// 에러 발생 시 FAIL 결과 생성
        /// </summary>
        public static TestResult Fail(string itemName, string errorMessage)
        {
            return new TestResult
            {
                ItemName = itemName,
                Pass = false,
                ErrorMessage = errorMessage
            };
        }

        public override string ToString()
        {
            if (HasError)
                return $"{ItemName}: ERROR - {ErrorMessage}";
            return $"{ItemName}: {MeasuredValue} {Unit} ({(Pass ? "PASS" : "FAIL")}) [{SpecMin}~{SpecMax}]";
        }
    }
}
