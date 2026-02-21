using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace _20260202_P2_MIDDLE.Forms
{
    public partial class RecipeSettingForm : Form
    {
        // 저장 파일 경로 (실행파일 옆에 RecipeSettings.xml)
        private static readonly string SaveFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RecipeSettings.xml");

        public RecipeSettingForm()
        {
            InitializeComponent();

            // DataGridView 열 구성
            InitializeDataGridView();

            // 저장된 데이터 로드
            LoadSettings();

            // 폼 닫을 때 자동 저장
            this.FormClosing += RecipeSettingForm_FormClosing;
        }

        #region DataGridView 초기화 및 행 생성

        /// <summary>
        /// dataGridView1/2/3 열을 재구성합니다.
        /// </summary>
        private void InitializeDataGridView()
        {
            SetupGridColumns(dataGridView1);
            SetupGridColumns(dataGridView2);
            SetupGridColumns(dataGridView3);
            SetupSpecGrid();
        }

        /// <summary>
        /// 지정된 DataGridView에 Check / Num / Short Pin / Open Pin 열을 세팅합니다.
        /// </summary>
        private void SetupGridColumns(DataGridView dgv)
        {
            dgv.Columns.Clear();
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;

            var colCheck = new DataGridViewCheckBoxColumn();
            colCheck.HeaderText = "Check";
            colCheck.Name = "Check";
            colCheck.Width = 60;
            dgv.Columns.Add(colCheck);

            var colNum = new DataGridViewTextBoxColumn();
            colNum.HeaderText = "Num";
            colNum.Name = "Num";
            colNum.Width = 60;
            colNum.ReadOnly = true;
            colNum.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNum.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            dgv.Columns.Add(colNum);

            var colShortPin = new DataGridViewTextBoxColumn();
            colShortPin.HeaderText = "Short Pin";
            colShortPin.Name = "ShortPin";
            colShortPin.Width = 200;
            dgv.Columns.Add(colShortPin);

            var colOpenPin = new DataGridViewTextBoxColumn();
            colOpenPin.HeaderText = "Open Pin";
            colOpenPin.Name = "OpenPin";
            colOpenPin.Width = 200;
            dgv.Columns.Add(colOpenPin);
        }

        /// <summary>
        /// 지정된 DataGridView에 count만큼 행을 동적으로 생성합니다.
        /// </summary>
        private void GenerateRows(DataGridView dgv, int count)
        {
            if (count <= 0)
            {
                MessageBox.Show("1 이상의 숫자를 입력해 주세요.", "입력 오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgv.Rows.Clear();

            for (int i = 1; i <= count; i++)
            {
                int rowIndex = dgv.Rows.Add();
                dgv.Rows[rowIndex].Cells["Check"].Value = false;
                dgv.Rows[rowIndex].Cells["Num"].Value = i;
                dgv.Rows[rowIndex].Cells["ShortPin"].Value = "";
                dgv.Rows[rowIndex].Cells["OpenPin"].Value = "";
            }
        }

        private void SetupSpecGrid()
        {
            var dgv = dataGridView4;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.DefaultCellStyle.Font = new Font("맑은 고딕", 9F);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.RowTemplate.Height = 28;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 32;

            dgv.Columns["No"].FillWeight = 30;
            dgv.Columns["TEST"].FillWeight = 65;
            dgv.Columns["JUDGMENT"].FillWeight = 70;
            dgv.Columns["ITEM"].FillWeight = 90;
            dgv.Columns["SETTING"].FillWeight = 55;
            dgv.Columns["MIN"].FillWeight = 50;
            dgv.Columns["MAX"].FillWeight = 50;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Name == "No" || col.Name == "TEST" || col.Name == "JUDGMENT" || col.Name == "ITEM")
                    col.ReadOnly = true;
            }

            dgv.Rows.Clear();
            object[][] specData = new object[][]
            {
                new object[] { 1,  "PRESSURE",  "SET", "PRESSURE",       "",     "",      "" },
                new object[] { 2,  "R202",      "SET", "R202 [Ω]",       560,    400,     700 },
                new object[] { 3,  "R203",      "SET", "R203 [Ω]",       560,    400,     700 },
                new object[] { 4,  "R204",      "SET", "R204 [Ω]",       560,    400,     700 },
                new object[] { 5,  "R205",      "SET", "R205 [Ω]",       560,    400,     700 },
                new object[] { 6,  "R207",      "SET", "R207 [Ω]",       10000,  600,     100000 },
                new object[] { 7,  "RT201",     "SET", "RT201 [Ω]",      10000,  600,     100000 },
                new object[] { 8,  "C203",      "SET", "C203 [nF]",      100,    50,      100 },
                new object[] { 9,  "C204-C206", "SET", "C204-C206 [uF]", 20.1,   10,      25 },
                new object[] { 10, "C207",      "SET", "C207 [nF]",      100,    60,      100 },
                new object[] { 11, "C208",      "SET", "C208 [uF]",      10,     5,       15 },
                new object[] { 12, "L201",      "SET", "L201 [Ω]",       0.5,    0.1,     0.6 },
                new object[] { 13, "D201",      "SET", "D201 [V]",       1.7,    1,       3 },
                new object[] { 14, "D202",      "SET", "D202 [V]",       1.7,    1,       3 },
            };

            foreach (var row in specData)
            {
                dgv.Rows.Add(row);
            }
        }

        #endregion

        #region 버튼 이벤트 핸들러

        private void BtnCon1Set_Click(object sender, EventArgs e)
        {
            GenerateRows(dataGridView1, (int)NumberCon1.Value);
        }

        private void BtnCon2Set_Click(object sender, EventArgs e)
        {
            GenerateRows(dataGridView2, (int)NumberCon2.Value);
        }

        private void BtnCon3Set_Click(object sender, EventArgs e)
        {
            GenerateRows(dataGridView3, (int)NumberCon3.Value);
        }

        #endregion

        #region 설정 저장 / 로드

        /// <summary>
        /// 폼 닫을 때 자동 저장
        /// </summary>
        private void RecipeSettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        /// <summary>
        /// 모든 설정값을 XML 파일로 저장합니다.
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                var data = new RecipeSettingsData();

                // Con Count 값
                data.Con1Count = (int)NumberCon1.Value;
                data.Con2Count = (int)NumberCon2.Value;
                data.Con3Count = (int)NumberCon3.Value;

                // Grid 행 데이터
                data.Con1Rows = GetGridData(dataGridView1);
                data.Con2Rows = GetGridData(dataGridView2);
                data.Con3Rows = GetGridData(dataGridView3);

                // Test Items 체크박스
                data.PressureUse = checkBox7.Checked;
                data.McOpen = chbMcOpen.Checked;
                data.McShort = chbMcShort.Checked;
                data.TcOpen = chbTcOpen.Checked;
                data.TcShort = chbTcShort.Checked;
                data.CcOpen = chbCcOpen.Checked;
                data.CcShort = chbCcShort.Checked;
                data.CpUse = checkBox1.Checked;

                // SPEC 값 (dataGridView4에서 가져오기)
                data.SpecRows = GetSpecGridData();

                var serializer = new XmlSerializer(typeof(RecipeSettingsData));
                using (var writer = new StreamWriter(SaveFilePath))
                {
                    serializer.Serialize(writer, data);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"설정 저장 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// XML 파일에서 설정을 로드하여 복원합니다.
        /// </summary>
        private void LoadSettings()
        {
            if (!File.Exists(SaveFilePath)) return;

            try
            {
                RecipeSettingsData data;
                var serializer = new XmlSerializer(typeof(RecipeSettingsData));
                using (var reader = new StreamReader(SaveFilePath))
                {
                    data = (RecipeSettingsData)serializer.Deserialize(reader);
                }

                // Con Count 복원
                NumberCon1.Value = data.Con1Count;
                NumberCon2.Value = data.Con2Count;
                NumberCon3.Value = data.Con3Count;

                // Grid 행 데이터 복원
                RestoreGridData(dataGridView1, data.Con1Rows);
                RestoreGridData(dataGridView2, data.Con2Rows);
                RestoreGridData(dataGridView3, data.Con3Rows);

                // Test Items 체크박스 복원
                checkBox7.Checked = data.PressureUse;
                chbMcOpen.Checked = data.McOpen;
                chbMcShort.Checked = data.McShort;
                chbTcOpen.Checked = data.TcOpen;
                chbTcShort.Checked = data.TcShort;
                chbCcOpen.Checked = data.CcOpen;
                chbCcShort.Checked = data.CcShort;
                checkBox1.Checked = data.CpUse;

                // SPEC 값 복원 (dataGridView4에 반영)
                RestoreSpecGridData(data.SpecRows);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"설정 로드 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// DataGridView의 행 데이터를 리스트로 추출합니다.
        /// </summary>
        private List<GridRowData> GetGridData(DataGridView dgv)
        {
            var rows = new List<GridRowData>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                rows.Add(new GridRowData
                {
                    Check = row.Cells["Check"].Value as bool? ?? false,
                    Num = row.Cells["Num"].Value?.ToString() ?? "",
                    ShortPin = row.Cells["ShortPin"].Value?.ToString() ?? "",
                    OpenPin = row.Cells["OpenPin"].Value?.ToString() ?? ""
                });
            }
            return rows;
        }

        /// <summary>
        /// 리스트 데이터를 DataGridView에 복원합니다.
        /// </summary>
        private void RestoreGridData(DataGridView dgv, List<GridRowData> rows)
        {
            if (rows == null || rows.Count == 0) return;

            dgv.Rows.Clear();
            foreach (var rowData in rows)
            {
                int idx = dgv.Rows.Add();
                dgv.Rows[idx].Cells["Check"].Value = rowData.Check;
                dgv.Rows[idx].Cells["Num"].Value = rowData.Num;
                dgv.Rows[idx].Cells["ShortPin"].Value = rowData.ShortPin;
                dgv.Rows[idx].Cells["OpenPin"].Value = rowData.OpenPin;
            }
        }

        private List<SpecRowData> GetSpecGridData()
        {
            var rows = new List<SpecRowData>();
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                rows.Add(new SpecRowData
                {
                    No = row.Cells["No"].Value?.ToString() ?? "",
                    Test = row.Cells["TEST"].Value?.ToString() ?? "",
                    Judgment = row.Cells["JUDGMENT"].Value?.ToString() ?? "",
                    Item = row.Cells["ITEM"].Value?.ToString() ?? "",
                    Setting = row.Cells["SETTING"].Value?.ToString() ?? "",
                    Min = row.Cells["MIN"].Value?.ToString() ?? "",
                    Max = row.Cells["MAX"].Value?.ToString() ?? ""
                });
            }
            return rows;
        }

        private void RestoreSpecGridData(List<SpecRowData> rows)
        {
            if (rows == null || rows.Count == 0) return;

            dataGridView4.Rows.Clear();
            foreach (var r in rows)
            {
                dataGridView4.Rows.Add(r.No, r.Test, r.Judgment, r.Item, r.Setting, r.Min, r.Max);
            }
        }

        #endregion
    }

    #region 저장 데이터 클래스

    [Serializable]
    public class GridRowData
    {
        public bool Check { get; set; }
        public string Num { get; set; }
        public string ShortPin { get; set; }
        public string OpenPin { get; set; }
    }

    [Serializable]
    public class SpecRowData
    {
        public string No { get; set; }
        public string Test { get; set; }
        public string Judgment { get; set; }
        public string Item { get; set; }
        public string Setting { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
    }

    [Serializable]
    public class RecipeSettingsData
    {
        public int Con1Count { get; set; }
        public int Con2Count { get; set; }
        public int Con3Count { get; set; }

        public List<GridRowData> Con1Rows { get; set; } = new List<GridRowData>();
        public List<GridRowData> Con2Rows { get; set; } = new List<GridRowData>();
        public List<GridRowData> Con3Rows { get; set; } = new List<GridRowData>();

        public bool PressureUse { get; set; }
        public bool McOpen { get; set; }
        public bool McShort { get; set; }
        public bool TcOpen { get; set; }
        public bool TcShort { get; set; }
        public bool CcOpen { get; set; }
        public bool CcShort { get; set; }
        public bool CpUse { get; set; }

        public List<SpecRowData> SpecRows { get; set; } = new List<SpecRowData>();
    }

    #endregion
}
