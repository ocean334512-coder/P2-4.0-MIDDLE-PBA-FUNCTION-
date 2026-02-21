using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _20260202_P2_MIDDLE.Forms;
using _20260202_P2_MIDDLE.Communications;
using _20260202_P2_MIDDLE.TestSteps;

namespace _20260202_P2_MIDDLE
{
    public partial class Form1 : Form
    {
        // 병합 헤더 그룹 정의: (그룹명, 시작열 인덱스, 끝열 인덱스)
        private readonly (string Name, int Start, int End)[] _headerGroups =
        {
            ("ITEM", 0, 1),
            ("COM", 2, 2),
            ("MAIN CON", 3, 4),
            ("TOP CON", 5, 6),
            ("CART CON", 7, 8),
            ("COMPONENT", 9, 20),
        };

        // 통신 객체
        private ControlBoardComm _boardComm;
        private JigComm _jigComm;
        private LcrMeterComm _lcrComm;

        // 검사 시퀀스 러너
        private TestSequenceRunner _testRunner;

        public Form1()
        {
            InitializeComponent();
            InitializeResultGrid();
        }

        #region 검사결과 DataGridView 초기화

        /// <summary>
        /// 검사결과 DataGridView(dgvTaskList) 초기화:
        /// 병합 헤더 + SPEC MIN/MAX 행 + CH 데이터 행
        /// </summary>
        private void InitializeResultGrid()
        {
            var dgv = dgvTaskList;
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            // ===== 기본 설정 =====
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 50;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Font = new Font("맑은 고딕", 8F);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(180, 210, 180);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 8F, FontStyle.Bold);
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.ScrollBars = ScrollBars.Both;

            // ===== 열 추가 (총 21열) =====
            // FillWeight 비율로 너비 조절 (전체 너비에 꽉 차게)
            // [0-1] ITEM 영역
            AddCol(dgv, "colItem", "", 40);
            AddCol(dgv, "colSubItem", "", 35);
            // [2] COM
            AddCol(dgv, "colPressure", "PRESSURE", 65);
            // [3-4] MAIN CON
            AddCol(dgv, "colMcShort", "SHORT", 45);
            AddCol(dgv, "colMcOpen", "OPEN", 45);
            // [5-6] TOP CON
            AddCol(dgv, "colTcShort", "SHORT", 45);
            AddCol(dgv, "colTcOpen", "OPEN", 45);
            // [7-8] CART CON
            AddCol(dgv, "colCcShort", "SHORT", 45);
            AddCol(dgv, "colCcOpen", "OPEN", 45);
            // [9-20] COMPONENT
            AddCol(dgv, "colR202", "R202", 45);
            AddCol(dgv, "colR203", "R203", 45);
            AddCol(dgv, "colR204a", "R204", 45);
            AddCol(dgv, "colR204b", "R204", 45);
            AddCol(dgv, "colR205", "R205", 45);
            AddCol(dgv, "colR207", "R207", 45);
            AddCol(dgv, "colRT201", "RT201", 50);
            AddCol(dgv, "colC203", "C203", 50);
            AddCol(dgv, "colC204_6", "C204-6", 50);
            AddCol(dgv, "colC207", "C207", 45);
            AddCol(dgv, "colC208", "C208", 45);
            AddCol(dgv, "colL201", "L201", 45);

            // ===== SPEC 행 추가 =====
            int minRow = dgv.Rows.Add(new object[] {
                "SPEC", "MIN",
                10000, 1, 0, 0, 1, 0, 1,
                0, 600, 600, 400, 400, 400, 400, 600, 11000, 9000, 80, 2
            });
            int maxRow = dgv.Rows.Add(new object[] {
                "SPEC", "MAX",
                20000, 1, 0, 1, 0, 1, 0,
                0, 600, 600, 600, 600, 600, 600, 600, 11000, 9000, 110, 4
            });

            // SPEC 행 스타일 (연녹색 배경, 낮은 높이)
            var specStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(220, 235, 220),
                Font = new Font("맑은 고딕", 8F, FontStyle.Bold)
            };
            dgv.Rows[minRow].DefaultCellStyle = specStyle;
            dgv.Rows[maxRow].DefaultCellStyle = specStyle;
            dgv.Rows[minRow].Height = 25;
            dgv.Rows[maxRow].Height = 25;

            // ===== CH 데이터 행 추가 =====
            dgv.Rows.Add("CH1", "");

            // CH1 행: 남은 공간 전부 차지
            int specTotalHeight = dgv.Rows[minRow].Height + dgv.Rows[maxRow].Height;
            int availableForCh = dgv.ClientSize.Height - dgv.ColumnHeadersHeight - specTotalHeight;
            if (availableForCh > 40)
                dgv.Rows[dgv.Rows.Count - 1].Height = availableForCh;
            else
                dgv.Rows[dgv.Rows.Count - 1].Height = 40;

            // ===== 이벤트 등록 =====
            dgv.CellPainting += DgvTaskList_CellPainting;
            dgv.Paint += DgvTaskList_Paint;
            dgv.Scroll += (s, ev) => dgv.Invalidate(
                new Rectangle(0, 0, dgv.Width, dgv.ColumnHeadersHeight));
        }

        /// <summary>
        /// DataGridView에 열을 추가하는 헬퍼
        /// </summary>
        private void AddCol(DataGridView dgv, string name, string headerText, float fillWeight)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = headerText,
                FillWeight = fillWeight,
                MinimumWidth = 30,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            };
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns.Add(col);
        }

        #endregion

        #region 병합 헤더 커스텀 페인팅

        /// <summary>
        /// 컬럼 헤더 셀 커스텀 페인팅: 하단 절반에 서브 헤더 텍스트 표시
        /// </summary>
        private void DgvTaskList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var dgv = dgvTaskList;

            // ===== SPEC 행 colItem 병합 (행 0,1의 첫 번째 열) =====
            if (e.ColumnIndex == 0 && (e.RowIndex == 0 || e.RowIndex == 1))
            {
                e.Handled = true;

                if (e.RowIndex == 0)
                {
                    // MIN 행: "SPEC" 텍스트를 두 행에 걸쳐 그리기
                    var cellRect = e.CellBounds;
                    int row1Height = dgv.Rows[1].Height;
                    var mergedRect = new Rectangle(cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height + row1Height);

                    using (var bgBrush = new SolidBrush(e.CellStyle.BackColor))
                        e.Graphics.FillRectangle(bgBrush, mergedRect);

                    using (var pen = new Pen(dgv.GridColor))
                        e.Graphics.DrawRectangle(pen, mergedRect.X - 1, mergedRect.Y - 1, mergedRect.Width, mergedRect.Height);

                    TextRenderer.DrawText(e.Graphics, "SPEC",
                        new Font("맑은 고딕", 8F, FontStyle.Bold),
                        mergedRect, Color.Black,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
                // MAX 행(row 1): 이미 MIN 행에서 그렸으므로 아무것도 안 함
                return;
            }

            // ===== 컬럼 헤더 커스텀 페인팅 =====
            if (e.RowIndex != -1 || e.ColumnIndex < 0) return;

            // 기본 배경 그리기
            using (var bgBrush = new SolidBrush(e.CellStyle.BackColor))
                e.Graphics.FillRectangle(bgBrush, e.CellBounds);

            // 하단 절반에 서브 헤더 텍스트 그리기
            var rect = e.CellBounds;
            var bottomRect = new Rectangle(rect.X, rect.Y + rect.Height / 2, rect.Width, rect.Height / 2);

            TextRenderer.DrawText(e.Graphics,
                e.Value?.ToString() ?? "",
                e.CellStyle.Font,
                bottomRect,
                e.CellStyle.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // 셀 테두리 그리기
            using (var pen = new Pen(dgv.GridColor))
            {
                // 외곽 테두리
                e.Graphics.DrawRectangle(pen,
                    rect.X - 1, rect.Y - 1, rect.Width, rect.Height);
                // 상단/하단 구분선
                e.Graphics.DrawLine(pen,
                    rect.X, rect.Y + rect.Height / 2,
                    rect.Right - 1, rect.Y + rect.Height / 2);
            }

            e.Handled = true;
        }

        /// <summary>
        /// DataGridView Paint 이벤트: 상단 절반에 병합 그룹 헤더를 표시
        /// </summary>
        private void DgvTaskList_Paint(object sender, PaintEventArgs e)
        {
            var dgv = dgvTaskList;
            if (dgv.Columns.Count == 0) return;

            int topHalf = dgv.ColumnHeadersHeight / 2;
            var headerBg = Color.FromArgb(180, 210, 180);
            var font = new Font("맑은 고딕", 8F, FontStyle.Bold);

            foreach (var group in _headerGroups)
            {
                if (group.Start >= dgv.Columns.Count) continue;
                int endCol = Math.Min(group.End, dgv.Columns.Count - 1);

                // 시작/끝 열의 화면 좌표 계산
                var startRect = dgv.GetColumnDisplayRectangle(group.Start, false);
                var endRect = dgv.GetColumnDisplayRectangle(endCol, false);

                if (startRect.Width == 0 && endRect.Width == 0) continue;

                int x = startRect.X;
                int right = endRect.Right;
                if (right <= x) continue;

                var rect = new Rectangle(x, 0, right - x, topHalf);

                // 배경
                using (var brush = new SolidBrush(headerBg))
                    e.Graphics.FillRectangle(brush, rect);

                // 테두리
                using (var pen = new Pen(dgv.GridColor))
                    e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height);

                // 그룹명 텍스트
                TextRenderer.DrawText(e.Graphics, group.Name,
                    font, rect, Color.Black,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            font.Dispose();
        }

        #endregion

        #region 검사결과 PASS/FAIL 표시 헬퍼

        /// <summary>
        /// 특정 CH 행의 검사 결과를 설정합니다.
        /// PASS → 초록색, FAIL → 적색으로 셀 배경이 바뀝니다.
        /// </summary>
        /// <param name="rowIndex">CH 데이터 행의 인덱스 (SPEC 2행 이후)</param>
        /// <param name="columnName">열 이름 (예: "colPressure", "colR202")</param>
        /// <param name="value">측정값</param>
        /// <param name="pass">PASS 여부</param>
        public void SetTestResult(int rowIndex, string columnName, object value, bool pass)
        {
            var dgv = dgvTaskList;
            if (rowIndex < 0 || rowIndex >= dgv.Rows.Count) return;
            if (!dgv.Columns.Contains(columnName)) return;

            var cell = dgv.Rows[rowIndex].Cells[columnName];
            cell.Value = value;

            if (pass)
            {
                cell.Style.BackColor = Color.LimeGreen;
                cell.Style.ForeColor = Color.White;
            }
            else
            {
                cell.Style.BackColor = Color.Red;
                cell.Style.ForeColor = Color.White;
            }
        }

        #endregion

        #region 통신 로그 (Comm 탭)

        private void InitializeCommLog()
        {
            lblClearJigComm.LinkClicked += (s, ev) => tboxJigComm.Clear();
            lblClearJigLog.LinkClicked += (s, ev) => tboxJigLog.Clear();
            tboxJigComm.ReadOnly = true;
        }

        /// <summary>
        /// 통신 로그를 RichTextBox에 추가 (스레드 안전)
        /// </summary>
        private void AppendCommLog(string tag, string message, Color color)
        {
            if (tboxJigComm.InvokeRequired)
            {
                tboxJigComm.BeginInvoke(new Action(() => AppendCommLog(tag, message, color)));
                return;
            }

            tboxJigComm.SelectionStart = tboxJigComm.TextLength;
            tboxJigComm.SelectionLength = 0;

            tboxJigComm.SelectionColor = Color.Gray;
            tboxJigComm.AppendText($"[{DateTime.Now:HH:mm:ss.fff}] ");

            tboxJigComm.SelectionColor = color;
            tboxJigComm.AppendText($"{tag} : ");

            tboxJigComm.SelectionColor = Color.Black;
            tboxJigComm.AppendText($"{message}\n");

            tboxJigComm.ScrollToCaret();

            if (tboxJigComm.Lines.Length > 5000)
            {
                tboxJigComm.SelectionStart = 0;
                tboxJigComm.SelectionLength = tboxJigComm.GetFirstCharIndexFromLine(1000);
                tboxJigComm.SelectedText = "";
            }
        }

        // ===== JIG =====
        public void LogJigTx(string message) => AppendCommLog("[JIG TX]", message, Color.Blue);
        public void LogJigRx(string message) => AppendCommLog("[JIG RX]", message, Color.DarkGreen);

        // ===== LCR Meter =====
        public void LogLcrTx(string message) => AppendCommLog("[LCR TX]", message, Color.DarkOrange);
        public void LogLcrRx(string message) => AppendCommLog("[LCR RX]", message, Color.Purple);

        // ===== Control Board =====
        public void LogBoardTx(string message) => AppendCommLog("[BOARD TX]", message, Color.DarkRed);
        public void LogBoardRx(string message) => AppendCommLog("[BOARD RX]", message, Color.Teal);

        /// <summary>
        /// 바이트 배열을 HEX 문자열로 변환
        /// </summary>
        public static string ToHexString(byte[] data)
        {
            if (data == null || data.Length == 0) return "(empty)";
            return BitConverter.ToString(data).Replace("-", " ");
        }

        #endregion

        #region 기존 이벤트 핸들러

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnComSettingsOpen_Click(object sender, EventArgs e)
        {
            using (var form = new ComSettingForm())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog(this);
            }
        }

       // private void btnRecipeSettingsOpen_Click(object sender, EventArgs e)
        //{
        //}

        private void tableLayoutPanel2_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void btnComSettingsOpen_Click_1(object sender, EventArgs e)
        {
            using (var form = new ComSettingForm())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog(this);
            }
        }

        private void btnRecipeSettingsOpen_Click_1(object sender, EventArgs e)
        {
        }

        private void btnSettingsOpen_Click_1(object sender, EventArgs e)
        {
            using (var form = new RecipeSettingForm())
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog(this);
            }
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeCommLog();
            button3.Click += BtnStart_Click;
            button4.Click += BtnStop_Click;
        }

        #region START / STOP 검사 실행

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (_testRunner != null && _testRunner.IsRunning)
            {
                MessageBox.Show("검사가 이미 실행 중입니다.", "알림",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // TODO: 실제 통신 객체가 연결되어 있는지 체크
            if (_boardComm == null || _jigComm == null || _lcrComm == null)
            {
                MessageBox.Show("통신 연결을 먼저 확인해 주세요.\n(Com Settings에서 연결)", "통신 오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 설정 로드
            var settings = TestSequenceRunner.LoadSettingsFromFile(RecipeSettingForm.SaveFilePath);

            // 러너 생성
            _testRunner = new TestSequenceRunner(
                _boardComm, _jigComm, _lcrComm, settings,
                logBoardTx: LogBoardTx, logBoardRx: LogBoardRx,
                logJigTx: LogJigTx, logJigRx: LogJigRx,
                logLcrTx: LogLcrTx, logLcrRx: LogLcrRx);

            // 이벤트 등록
            _testRunner.OnStatusChanged += (msg) =>
            {
                if (InvokeRequired)
                    BeginInvoke(new Action(() => UpdateStatus(msg)));
                else
                    UpdateStatus(msg);
            };

            _testRunner.OnStepCompleted += (result, idx, total) =>
            {
                if (InvokeRequired)
                    BeginInvoke(new Action(() => OnTestStepCompleted(result, idx, total)));
                else
                    OnTestStepCompleted(result, idx, total);
            };

            _testRunner.OnSequenceCompleted += (allPass, results) =>
            {
                if (InvokeRequired)
                    BeginInvoke(new Action(() => OnTestSequenceCompleted(allPass, results)));
                else
                    OnTestSequenceCompleted(allPass, results);
            };

            // CH1 행 초기화
            ClearChRow();

            // UI 상태
            button3.Enabled = false;
            button4.Enabled = true;
            lblResult.Text = "검사 중...";
            lblResult.BackColor = Color.Yellow;
            lblResult.ForeColor = Color.Black;

            // 비동기 실행
            _testRunner.RunAsync();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (_testRunner != null && _testRunner.IsRunning)
            {
                _testRunner.Cancel();
                lblResult.Text = "STOP";
                lblResult.BackColor = Color.Orange;
            }
        }

        private void UpdateStatus(string message)
        {
            AppendCommLog("[SYSTEM]", message, Color.DarkMagenta);
        }

        /// <summary>
        /// 검사 항목별 결과를 dgvTaskList에 표시
        /// </summary>
        private void OnTestStepCompleted(TestResult result, int index, int total)
        {
            int chRow = 2; // CH1 행 (SPEC MIN=0, MAX=1, CH1=2)

            var columnMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "PRESSURE",   "colPressure" },
                { "CON1 SHORT", "colMcShort" },
                { "CON1 OPEN",  "colMcOpen" },
                { "CON2 SHORT", "colTcShort" },
                { "CON2 OPEN",  "colTcOpen" },
                { "CON3 SHORT", "colCcShort" },
                { "CON3 OPEN",  "colCcOpen" },
                { "R202",       "colR202" },
                { "R203",       "colR203" },
                { "R204",       "colR204a" },
                { "R205",       "colR205" },
                { "R207",       "colR207" },
                { "RT201",      "colRT201" },
                { "C203",       "colC203" },
                { "C204-C206",  "colC204_6" },
                { "C207",       "colC207" },
                { "C208",       "colC208" },
                { "L201",       "colL201" },
            };

            string colName;
            if (columnMap.TryGetValue(result.ItemName, out colName))
            {
                object displayValue = result.HasError ? "ERR" : (object)Math.Round(result.MeasuredValue, 2);
                SetTestResult(chRow, colName, displayValue, result.Pass);
            }
        }

        /// <summary>
        /// 전체 검사 완료 시 최종 판정
        /// </summary>
        private void OnTestSequenceCompleted(bool allPass, List<TestResult> results)
        {
            button3.Enabled = true;
            button4.Enabled = false;

            if (allPass)
            {
                lblResult.Text = "PASS";
                lblResult.BackColor = Color.LimeGreen;
                lblResult.ForeColor = Color.White;
            }
            else
            {
                lblResult.Text = "FAIL";
                lblResult.BackColor = Color.Red;
                lblResult.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// CH1 행의 검사 결과를 초기화
        /// </summary>
        private void ClearChRow()
        {
            int chRow = 2;
            if (chRow >= dgvTaskList.Rows.Count) return;

            for (int col = 2; col < dgvTaskList.Columns.Count; col++)
            {
                dgvTaskList.Rows[chRow].Cells[col].Value = "";
                dgvTaskList.Rows[chRow].Cells[col].Style.BackColor = Color.White;
                dgvTaskList.Rows[chRow].Cells[col].Style.ForeColor = Color.Black;
            }
        }

        #endregion
    }
}
