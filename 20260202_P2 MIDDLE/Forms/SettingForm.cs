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
                data.PressureUse = chbPressueUse.Checked;
                data.McOpen = chbMcOpen.Checked;
                data.McShort = chbMcShort.Checked;
                data.TcOpen = chbTcOpen.Checked;
                data.TcShort = chbTcShort.Checked;
                data.CcOpen = chbCcOpen.Checked;
                data.CcShort = chbCcShort.Checked;
                data.CpUse = chbCpUse.Checked;

                // SPEC 값
                data.PressureMin = (int)nudPressureMin.Value;
                data.PressureMax = (int)nudPressureMax.Value;
                data.R202Min = (int)nudR202Min.Value;
                data.R202Max = (int)nudR202Max.Value;
                data.R203Min = (int)nudR203Min.Value;
                data.R203Max = (int)nudR203Max.Value;
                data.R204Min = (int)nudR204Min.Value;
                data.R204Max = (int)nudR204Max.Value;
                data.R205Min = (int)nudR205Min.Value;
                data.R205Max = (int)nudR205Max.Value;
                data.R207Min = (int)nudR207Min.Value;
                data.R207Max = (int)nudR207Max.Value;
                data.RT201Min = (int)nudRT201Min.Value;
                data.RT201Max = (int)nudRT201Max.Value;
                data.C203Min = (int)nudC203Min.Value;
                data.C203Max = (int)nudC203Max.Value;
                data.C204Min = (int)nudC204Min.Value;
                data.C204Max = (int)nudC204Max.Value;
                data.C207Min = (int)nudC207Min.Value;
                data.C207Max = (int)nudC207Max.Value;
                data.L201Min = (int)nudL201Min.Value;
                data.L201Max = (int)nudL201Max.Value;

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
                chbPressueUse.Checked = data.PressureUse;
                chbMcOpen.Checked = data.McOpen;
                chbMcShort.Checked = data.McShort;
                chbTcOpen.Checked = data.TcOpen;
                chbTcShort.Checked = data.TcShort;
                chbCcOpen.Checked = data.CcOpen;
                chbCcShort.Checked = data.CcShort;
                chbCpUse.Checked = data.CpUse;

                // SPEC 값 복원
                nudPressureMin.Value = data.PressureMin;
                nudPressureMax.Value = data.PressureMax;
                nudR202Min.Value = data.R202Min;
                nudR202Max.Value = data.R202Max;
                nudR203Min.Value = data.R203Min;
                nudR203Max.Value = data.R203Max;
                nudR204Min.Value = data.R204Min;
                nudR204Max.Value = data.R204Max;
                nudR205Min.Value = data.R205Min;
                nudR205Max.Value = data.R205Max;
                nudR207Min.Value = data.R207Min;
                nudR207Max.Value = data.R207Max;
                nudRT201Min.Value = data.RT201Min;
                nudRT201Max.Value = data.RT201Max;
                nudC203Min.Value = data.C203Min;
                nudC203Max.Value = data.C203Max;
                nudC204Min.Value = data.C204Min;
                nudC204Max.Value = data.C204Max;
                nudC207Min.Value = data.C207Min;
                nudC207Max.Value = data.C207Max;
                nudL201Min.Value = data.L201Min;
                nudL201Max.Value = data.L201Max;
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

        #endregion
    }

    #region 저장 데이터 클래스

    /// <summary>
    /// DataGridView 한 행의 데이터
    /// </summary>
    [Serializable]
    public class GridRowData
    {
        public bool Check { get; set; }
        public string Num { get; set; }
        public string ShortPin { get; set; }
        public string OpenPin { get; set; }
    }

    /// <summary>
    /// RecipeSettingForm 전체 설정 데이터
    /// </summary>
    [Serializable]
    public class RecipeSettingsData
    {
        // Grid Count
        public int Con1Count { get; set; }
        public int Con2Count { get; set; }
        public int Con3Count { get; set; }

        // Grid Row 데이터
        public List<GridRowData> Con1Rows { get; set; } = new List<GridRowData>();
        public List<GridRowData> Con2Rows { get; set; } = new List<GridRowData>();
        public List<GridRowData> Con3Rows { get; set; } = new List<GridRowData>();

        // Test Items 체크박스
        public bool PressureUse { get; set; }
        public bool McOpen { get; set; }
        public bool McShort { get; set; }
        public bool TcOpen { get; set; }
        public bool TcShort { get; set; }
        public bool CcOpen { get; set; }
        public bool CcShort { get; set; }
        public bool CpUse { get; set; }

        // SPEC 값 (Min/Max)
        public int PressureMin { get; set; }
        public int PressureMax { get; set; }
        public int R202Min { get; set; }
        public int R202Max { get; set; }
        public int R203Min { get; set; }
        public int R203Max { get; set; }
        public int R204Min { get; set; }
        public int R204Max { get; set; }
        public int R205Min { get; set; }
        public int R205Max { get; set; }
        public int R207Min { get; set; }
        public int R207Max { get; set; }
        public int RT201Min { get; set; }
        public int RT201Max { get; set; }
        public int C203Min { get; set; }
        public int C203Max { get; set; }
        public int C204Min { get; set; }
        public int C204Max { get; set; }
        public int C207Min { get; set; }
        public int C207Max { get; set; }
        public int L201Min { get; set; }
        public int L201Max { get; set; }
    }

    #endregion
}
