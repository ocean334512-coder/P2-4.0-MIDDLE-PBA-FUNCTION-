using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using _20260202_P2_MIDDLE.Communications;
using _20260202_P2_MIDDLE.Forms;

namespace _20260202_P2_MIDDLE.TestSteps
{
    /// <summary>
    /// 전체 검사 시퀀스 관리자
    /// START 버튼 → Run() → 검사 항목 순서대로 실행 → 전체 PASS/FAIL 판정
    /// BackgroundWorker로 별도 쓰레드에서 실행
    /// </summary>
    public class TestSequenceRunner
    {
        private readonly ControlBoardComm _board;
        private readonly JigComm _jig;
        private readonly LcrMeterComm _lcr;
        private readonly RecipeSettingsData _settings;

        private readonly Action<string> _logBoardTx;
        private readonly Action<string> _logBoardRx;
        private readonly Action<string> _logJigTx;
        private readonly Action<string> _logJigRx;
        private readonly Action<string> _logLcrTx;
        private readonly Action<string> _logLcrRx;

        /// <summary>
        /// 각 항목 검사 완료 시 호출 (항목명, 결과, 현재 진행 인덱스)
        /// </summary>
        public event Action<TestResult, int, int> OnStepCompleted;

        /// <summary>
        /// 전체 검사 완료 시 호출 (전체 PASS 여부, 전체 결과 리스트)
        /// </summary>
        public event Action<bool, List<TestResult>> OnSequenceCompleted;

        /// <summary>
        /// 검사 진행 상태 메시지
        /// </summary>
        public event Action<string> OnStatusChanged;

        /// <summary>
        /// 마지막 실행 결과
        /// </summary>
        public List<TestResult> Results { get; private set; } = new List<TestResult>();

        /// <summary>
        /// 전체 PASS 여부
        /// </summary>
        public bool AllPass { get; private set; }

        /// <summary>
        /// 실행 중 여부
        /// </summary>
        public bool IsRunning { get; private set; }

        private BackgroundWorker _worker;

        public TestSequenceRunner(
            ControlBoardComm board,
            JigComm jig,
            LcrMeterComm lcr,
            RecipeSettingsData settings,
            Action<string> logBoardTx = null, Action<string> logBoardRx = null,
            Action<string> logJigTx = null, Action<string> logJigRx = null,
            Action<string> logLcrTx = null, Action<string> logLcrRx = null)
        {
            _board = board;
            _jig = jig;
            _lcr = lcr;
            _settings = settings;
            _logBoardTx = logBoardTx;
            _logBoardRx = logBoardRx;
            _logJigTx = logJigTx;
            _logJigRx = logJigRx;
            _logLcrTx = logLcrTx;
            _logLcrRx = logLcrRx;
        }

        #region SPEC 데이터 헬퍼

        private double GetSpecMin(string testName)
        {
            var row = FindSpecRow(testName);
            if (row != null && double.TryParse(row.Min, out double v)) return v;
            return 0;
        }

        private double GetSpecMax(string testName)
        {
            var row = FindSpecRow(testName);
            if (row != null && double.TryParse(row.Max, out double v)) return v;
            return 0;
        }

        private SpecRowData FindSpecRow(string testName)
        {
            if (_settings?.SpecRows == null) return null;
            return _settings.SpecRows.Find(r =>
                r.Test != null && r.Test.Equals(testName, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region 검사 시퀀스 빌드

        /// <summary>
        /// 설정에 따라 실행할 검사 항목 리스트를 구성합니다.
        /// </summary>
        public List<ITestStep> BuildSequence()
        {
            var steps = new List<ITestStep>();

            // 1. Pressure
            if (_settings.PressureUse)
            {
                steps.Add(new PressureTest(_board,
                    GetSpecMin("PRESSURE"), GetSpecMax("PRESSURE"),
                    _logBoardTx, _logBoardRx));
            }

            // 2. Open/Short - CON1 (Main Connector)
            if (_settings.McOpen)
            {
                var pins = OpenShortTest.BuildPinTestList(0x01, _settings.Con1Rows, OpenShortMode.Open);
                if (pins.Count > 0)
                    steps.Add(new OpenShortTest("CON1 OPEN", _board, OpenShortMode.Open, pins,
                        _logBoardTx, _logBoardRx));
            }
            if (_settings.McShort)
            {
                var pins = OpenShortTest.BuildPinTestList(0x01, _settings.Con1Rows, OpenShortMode.Short);
                if (pins.Count > 0)
                    steps.Add(new OpenShortTest("CON1 SHORT", _board, OpenShortMode.Short, pins,
                        _logBoardTx, _logBoardRx));
            }

            // 3. Open/Short - CON2 (Top Connector)
            if (_settings.TcOpen)
            {
                var pins = OpenShortTest.BuildPinTestList(0x02, _settings.Con2Rows, OpenShortMode.Open);
                if (pins.Count > 0)
                    steps.Add(new OpenShortTest("CON2 OPEN", _board, OpenShortMode.Open, pins,
                        _logBoardTx, _logBoardRx));
            }
            if (_settings.TcShort)
            {
                var pins = OpenShortTest.BuildPinTestList(0x02, _settings.Con2Rows, OpenShortMode.Short);
                if (pins.Count > 0)
                    steps.Add(new OpenShortTest("CON2 SHORT", _board, OpenShortMode.Short, pins,
                        _logBoardTx, _logBoardRx));
            }

            // 4. Open/Short - CON3 (Cart Connector)
            if (_settings.CcOpen)
            {
                var pins = OpenShortTest.BuildPinTestList(0x03, _settings.Con3Rows, OpenShortMode.Open);
                if (pins.Count > 0)
                    steps.Add(new OpenShortTest("CON3 OPEN", _board, OpenShortMode.Open, pins,
                        _logBoardTx, _logBoardRx));
            }
            if (_settings.CcShort)
            {
                var pins = OpenShortTest.BuildPinTestList(0x03, _settings.Con3Rows, OpenShortMode.Short);
                if (pins.Count > 0)
                    steps.Add(new OpenShortTest("CON3 SHORT", _board, OpenShortMode.Short, pins,
                        _logBoardTx, _logBoardRx));
            }

            // 5. LCR 검사 항목들
            if (_settings.CpUse)
            {
                // LCR Initialize
                steps.Add(new LcrInitialize(_lcr, _logLcrTx, _logLcrRx));

                // R202 ~ R205: DCR 모드
                AddLcrStep(steps, "R202", LcrMode.DCR, LcrSwitchMap.R202, "Ω");
                AddLcrStep(steps, "R203", LcrMode.DCR, LcrSwitchMap.R203, "Ω");
                AddLcrStep(steps, "R204", LcrMode.DCR, LcrSwitchMap.R204, "Ω");
                AddLcrStep(steps, "R205", LcrMode.DCR, LcrSwitchMap.R205, "Ω");
                AddLcrStep(steps, "R207", LcrMode.DCR, LcrSwitchMap.R207, "Ω");
                AddLcrStep(steps, "RT201", LcrMode.DCR, LcrSwitchMap.RT201, "Ω");
                AddLcrStep(steps, "L201", LcrMode.DCR, LcrSwitchMap.L201, "Ω");

                // C203, C207: Cp-D 모드 (nF)
                AddLcrStep(steps, "C203", LcrMode.CpD, LcrSwitchMap.C203, "nF");
                AddLcrStep(steps, "C207", LcrMode.CpD, LcrSwitchMap.C207, "nF");

                // C204-C206: Cp-D 모드 (uF)
                AddLcrStep(steps, "C204-C206", LcrMode.CpD, LcrSwitchMap.C204_206, "uF");

                // C208: Cp-D 모드 (uF)
                AddLcrStep(steps, "C208", LcrMode.CpD, LcrSwitchMap.C208, "uF");

                // R206: DCR 모드
                AddLcrStep(steps, "R206", LcrMode.DCR, LcrSwitchMap.R206, "Ω");
            }

            // 6. D201 (다이오드 - 스위치 있음)
            steps.Add(new LedVoltTest("D201", _board,
                GetSpecMin("D201"), GetSpecMax("D201"),
                isDiode: true, switchData: VoltSwitchMap.D201,
                _logBoardTx, _logBoardRx));

            // 7. D202 (다이오드 - 스위치 있음)
            steps.Add(new LedVoltTest("D202", _board,
                GetSpecMin("D202"), GetSpecMax("D202"),
                isDiode: true, switchData: VoltSwitchMap.D202,
                _logBoardTx, _logBoardRx));

            // 8. LED VOLTAGE (스위치 없음)
            steps.Add(new LedVoltageTest(_board,
                GetSpecMin("LED VOLTAGE"), GetSpecMax("LED VOLTAGE"),
                switchData: null,
                _logBoardTx, _logBoardRx));

            // 9. COLOR SENSOR (스위치 없음)
            steps.Add(new ColorSensorTest(_board,
                GetSpecMin("COLOR SENSOR"), GetSpecMax("COLOR SENSOR"),
                switchData: null,
                _logBoardTx, _logBoardRx));

            return steps;
        }

        private void AddLcrStep(List<ITestStep> steps, string name, LcrMode mode, byte[] switchData, string unit)
        {
            steps.Add(new LcrMeasureTest(name, _lcr, _board, mode, switchData,
                GetSpecMin(name), GetSpecMax(name), unit,
                _logLcrTx, _logLcrRx, _logBoardTx, _logBoardRx));
        }

        #endregion

        #region 전체 시퀀스 실행 (비동기)

        /// <summary>
        /// 전체 검사 시퀀스를 백그라운드 쓰레드에서 실행합니다.
        /// </summary>
        public void RunAsync()
        {
            if (IsRunning) return;

            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_Completed;
            _worker.RunWorkerAsync();
        }

        /// <summary>
        /// 실행 중인 검사를 취소합니다.
        /// </summary>
        public void Cancel()
        {
            if (_worker != null && _worker.IsBusy)
                _worker.CancelAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            IsRunning = true;
            Results.Clear();
            AllPass = true;

            var steps = BuildSequence();
            OnStatusChanged?.Invoke($"검사 시작 ({steps.Count}개 항목)");

            for (int i = 0; i < steps.Count; i++)
            {
                if (_worker.CancellationPending)
                {
                    e.Cancel = true;
                    OnStatusChanged?.Invoke("검사 취소됨");
                    return;
                }

                var step = steps[i];
                OnStatusChanged?.Invoke($"[{i + 1}/{steps.Count}] {step.Name} 검사 중...");

                TestResult result = step.Execute();
                Results.Add(result);

                if (!result.Pass)
                    AllPass = false;

                OnStepCompleted?.Invoke(result, i, steps.Count);
            }
        }

        private void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            IsRunning = false;

            if (e.Cancelled)
            {
                OnStatusChanged?.Invoke("검사가 취소되었습니다.");
                OnSequenceCompleted?.Invoke(false, Results);
            }
            else if (e.Error != null)
            {
                OnStatusChanged?.Invoke($"검사 오류: {e.Error.Message}");
                OnSequenceCompleted?.Invoke(false, Results);
            }
            else
            {
                string finalResult = AllPass ? "전체 PASS" : "FAIL";
                OnStatusChanged?.Invoke($"검사 완료 - {finalResult} ({Results.Count}개 항목)");
                OnSequenceCompleted?.Invoke(AllPass, Results);
            }
        }

        #endregion

        #region 개별 검사 실행 (동기)

        /// <summary>
        /// 단일 검사 항목을 동기로 실행합니다 (매뉴얼 테스트용)
        /// </summary>
        public TestResult RunSingle(ITestStep step)
        {
            OnStatusChanged?.Invoke($"{step.Name} 단일 검사 중...");
            var result = step.Execute();
            OnStatusChanged?.Invoke($"{step.Name}: {(result.Pass ? "PASS" : "FAIL")}");
            return result;
        }

        /// <summary>
        /// 이름으로 검사 항목을 찾아 단일 실행
        /// </summary>
        public TestResult RunSingleByName(string testName)
        {
            var steps = BuildSequence();
            var step = steps.Find(s => s.Name.Equals(testName, StringComparison.OrdinalIgnoreCase));
            if (step == null)
                return TestResult.Fail(testName, "검사 항목을 찾을 수 없습니다.");
            return RunSingle(step);
        }

        #endregion

        #region 설정 데이터 로드 헬퍼

        /// <summary>
        /// XML 파일에서 RecipeSettingsData를 로드합니다.
        /// </summary>
        public static RecipeSettingsData LoadSettingsFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return new RecipeSettingsData();

            try
            {
                var serializer = new XmlSerializer(typeof(RecipeSettingsData));
                using (var reader = new StreamReader(filePath))
                {
                    return (RecipeSettingsData)serializer.Deserialize(reader);
                }
            }
            catch
            {
                return new RecipeSettingsData();
            }
        }

        #endregion
    }
}
