using System;
using System.Collections.Generic;
using System.Linq;
using _20260202_P2_MIDDLE.Communications;
using _20260202_P2_MIDDLE.Forms;

namespace _20260202_P2_MIDDLE.TestSteps
{
    /// <summary>
    /// OPEN / SHORT 검사
    /// SettingForm의 DataGridView1(CON1), DataGridView2(CON2), DataGridView3(CON3)에서
    /// 각 핀의 OpenPin, ShortPin 설정을 읽어 Board에 전송 → 판정
    /// 
    /// OPEN 판정: 응답 결과 0x01 = PASS
    /// SHORT 판정: 응답 결과 0x02 = PASS
    /// </summary>
    public class OpenShortTest : ITestStep
    {
        public const byte RESULT_OPEN = 0x01;
        public const byte RESULT_SHORT = 0x02;
        public const byte RESULT_UNUSED = 0xFF;

        private readonly ControlBoardComm _board;
        private readonly List<PinTestInfo> _pinTests;
        private readonly OpenShortMode _mode;
        private readonly Action<string> _logTx;
        private readonly Action<string> _logRx;

        public string Name { get; }

        /// <summary>
        /// 검사 실행 후 상세 결과 (각 핀별)
        /// </summary>
        public List<PinTestResult> DetailResults { get; private set; } = new List<PinTestResult>();

        /// <param name="mode">OPEN 또는 SHORT</param>
        /// <param name="pinTests">검사할 핀 정보 리스트 (SettingForm 그리드에서 추출)</param>
        public OpenShortTest(string name, ControlBoardComm board, OpenShortMode mode,
            List<PinTestInfo> pinTests,
            Action<string> logTx = null, Action<string> logRx = null)
        {
            Name = name;
            _board = board;
            _mode = mode;
            _pinTests = pinTests;
            _logTx = logTx;
            _logRx = logRx;
        }

        public TestResult Execute()
        {
            try
            {
                DetailResults.Clear();
                byte expectedResult = (_mode == OpenShortMode.Open) ? RESULT_OPEN : RESULT_SHORT;
                string modeStr = (_mode == OpenShortMode.Open) ? "OPEN" : "SHORT";
                bool allPass = true;
                var failMessages = new List<string>();

                foreach (var pinTest in _pinTests)
                {
                    var testPins = pinTest.TestPins
                        .Select(p => (p.Connector, p.PinNum))
                        .ToArray();

                    _logTx?.Invoke($"{modeStr} Test CON{pinTest.Connector} PIN{pinTest.PinNum} → {testPins.Length}개 핀");

                    var result = _board.TestOpenShort(pinTest.Connector, pinTest.PinNum, testPins);

                    if (result == null)
                    {
                        allPass = false;
                        failMessages.Add($"CON{pinTest.Connector} PIN{pinTest.PinNum}: 응답 없음");
                        DetailResults.Add(new PinTestResult
                        {
                            Connector = pinTest.Connector,
                            PinNum = pinTest.PinNum,
                            Pass = false,
                            ErrorMessage = "응답 없음"
                        });
                        continue;
                    }

                    var pinFails = new List<string>();
                    for (int i = 0; i < testPins.Length; i++)
                    {
                        byte actual = result.Results[i];
                        bool pinPass = (actual == expectedResult);

                        if (!pinPass)
                        {
                            string actualStr = actual == RESULT_OPEN ? "OPEN" :
                                               actual == RESULT_SHORT ? "SHORT" :
                                               actual == RESULT_UNUSED ? "UNUSED" : $"0x{actual:X2}";
                            pinFails.Add($"CON{testPins[i].Item1} PIN{testPins[i].Item2}={actualStr}(expected:{modeStr})");
                        }
                    }

                    bool rowPass = pinFails.Count == 0;
                    string resultLog = rowPass ? "ALL PASS" : $"FAIL: {string.Join(", ", pinFails)}";
                    _logRx?.Invoke($"CON{pinTest.Connector} PIN{pinTest.PinNum}: {resultLog}");

                    DetailResults.Add(new PinTestResult
                    {
                        Connector = pinTest.Connector,
                        PinNum = pinTest.PinNum,
                        Pass = rowPass,
                        ErrorMessage = rowPass ? null : string.Join("; ", pinFails)
                    });

                    if (!rowPass)
                    {
                        allPass = false;
                        failMessages.AddRange(pinFails);
                    }
                }

                if (allPass)
                {
                    return new TestResult
                    {
                        ItemName = Name,
                        MeasuredValue = _pinTests.Count,
                        Unit = "pins",
                        SpecMin = 0,
                        SpecMax = 0,
                        Pass = true
                    };
                }
                else
                {
                    return new TestResult
                    {
                        ItemName = Name,
                        MeasuredValue = 0,
                        Unit = "",
                        Pass = false,
                        ErrorMessage = $"{failMessages.Count}개 핀 NG: {string.Join(", ", failMessages.Take(5))}"
                            + (failMessages.Count > 5 ? $" ...외 {failMessages.Count - 5}건" : "")
                    };
                }
            }
            catch (Exception ex)
            {
                return TestResult.Fail(Name, ex.Message);
            }
        }

        #region SettingForm 그리드 데이터 → PinTestInfo 변환 헬퍼

        /// <summary>
        /// SettingForm의 DataGridView 데이터(GridRowData)를 PinTestInfo 리스트로 변환합니다.
        /// </summary>
        /// <param name="conNumber">커넥터 번호 (1=CON1, 2=CON2, 3=CON3)</param>
        /// <param name="gridRows">DataGridView에서 추출한 행 데이터</param>
        /// <param name="mode">OPEN이면 OpenPin 컬럼, SHORT이면 ShortPin 컬럼 사용</param>
        public static List<PinTestInfo> BuildPinTestList(byte conNumber, List<GridRowData> gridRows, OpenShortMode mode)
        {
            var result = new List<PinTestInfo>();
            if (gridRows == null) return result;

            foreach (var row in gridRows)
            {
                if (!row.Check) continue;

                byte mainPin;
                if (!byte.TryParse(row.Num, out mainPin)) continue;

                string pinStr = (mode == OpenShortMode.Open) ? row.OpenPin : row.ShortPin;
                if (string.IsNullOrWhiteSpace(pinStr)) continue;

                var testPins = ParseTestPins(conNumber, pinStr);
                if (testPins.Count == 0) continue;

                result.Add(new PinTestInfo
                {
                    Connector = conNumber,
                    PinNum = mainPin,
                    TestPins = testPins
                });
            }

            return result;
        }

        /// <summary>
        /// "1,6,8,9,11" 형식의 핀 문자열을 파싱합니다.
        /// 같은 CON 내 핀으로 간주합니다.
        /// </summary>
        private static List<TestPin> ParseTestPins(byte conNumber, string pinStr)
        {
            var pins = new List<TestPin>();
            var parts = pinStr.Split(new[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                byte pin;
                if (byte.TryParse(part.Trim(), out pin))
                {
                    pins.Add(new TestPin { Connector = conNumber, PinNum = pin });
                }
            }

            return pins;
        }

        #endregion
    }

    public enum OpenShortMode
    {
        Open,
        Short
    }

    public class PinTestInfo
    {
        public byte Connector { get; set; }
        public byte PinNum { get; set; }
        public List<TestPin> TestPins { get; set; } = new List<TestPin>();
    }

    public class TestPin
    {
        public byte Connector { get; set; }
        public byte PinNum { get; set; }
    }

    public class PinTestResult
    {
        public byte Connector { get; set; }
        public byte PinNum { get; set; }
        public bool Pass { get; set; }
        public string ErrorMessage { get; set; }
    }
}
