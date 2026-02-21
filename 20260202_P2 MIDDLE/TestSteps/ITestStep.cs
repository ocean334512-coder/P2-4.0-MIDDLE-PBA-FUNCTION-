namespace _20260202_P2_MIDDLE.TestSteps
{
    /// <summary>
    /// 검사 항목 공통 인터페이스
    /// </summary>
    public interface ITestStep
    {
        string Name { get; }
        TestResult Execute();
    }
}
