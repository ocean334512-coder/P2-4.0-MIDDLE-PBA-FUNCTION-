using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO.Ports;

namespace _20260202_P2_MIDDLE.Forms
{
    public partial class ComSettingForm : Form
    {
        private const int IP_BOARD_COUNT = 9;

        public ComSettingForm()
        {
            InitializeComponent();
        }

        private void ComSettingForm_Load(object sender, EventArgs e)
        {
            SetupIpGrid();
            SetupSerialGrid();

            dgvIpsetting.CellDoubleClick += DgvIpsetting_CellDoubleClick;
            dgvSerialsetting.CellDoubleClick += DgvSerialsetting_CellDoubleClick;
            btnConnectCheck.Click += BtnCheck_Click;
            btnComSave.Click += BtnSave_Click;
            btnComCancel.Click += BtnCancel_Click;
        }

        #region IP Address DataGridView 설정

        private void SetupIpGrid()
        {
            var dgv = dgvIpsetting;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.DefaultCellStyle.Font = new Font("맑은 고딕", 9F);

            dgv.Columns["No"].FillWeight = 30;
            dgv.Columns["En"].FillWeight = 30;
            dgv.Columns["Check"].FillWeight = 40;
            dgv.Columns["Board"].FillWeight = 80;
            dgv.Columns["IPAddress"].FillWeight = 100;
            dgv.Columns["Port"].FillWeight = 50;

            dgv.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["En"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Check"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Board"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["IPAddress"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["Port"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;

            dgv.Rows.Clear();
            for (int i = 1; i <= IP_BOARD_COUNT; i++)
            {
                dgv.Rows.Add(i, "-", "", $"Board {i}", $"192.168.0.{100 + i}", "5000");
            }

            HighlightSelectedRow(dgv);
        }

        private void DgvIpsetting_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvIpsetting.Rows[e.RowIndex];

            using (var dlg = new IpSettingsDialog())
            {
                dlg.IpAddress = row.Cells["IPAddress"].Value?.ToString() ?? "";
                dlg.PortNumber = row.Cells["Port"].Value?.ToString() ?? "";
                dlg.Enable = (row.Cells["En"].Value?.ToString() == "√");

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    row.Cells["IPAddress"].Value = dlg.IpAddress;
                    row.Cells["Port"].Value = dlg.PortNumber;
                    row.Cells["En"].Value = dlg.Enable ? "√" : "-";
                }
            }
        }

        #endregion

        #region Connection Check

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            CheckTcpConnections();
            CheckSerialConnections();
        }

        private void CheckTcpConnections()
        {
            var dgv = dgvIpsetting;
            int successCount = 0;
            int failCount = 0;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                string en = row.Cells["En"].Value?.ToString() ?? "";
                if (en != "√")
                {
                    row.Cells["Check"].Value = "-";
                    row.DefaultCellStyle.BackColor = Color.White;
                    continue;
                }

                string ip = row.Cells["IPAddress"].Value?.ToString() ?? "";
                string portStr = row.Cells["Port"].Value?.ToString() ?? "";

                int portNum;
                if (!System.Net.IPAddress.TryParse(ip, out _) ||
                    !int.TryParse(portStr, out portNum) || portNum <= 0 || portNum > 65535)
                {
                    row.Cells["Check"].Value = "FAIL";
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    failCount++;
                    continue;
                }

                bool connected = TryTcpConnect(ip, portNum, timeoutMs: 1000);
                if (connected)
                {
                    row.Cells["Check"].Value = "OK";
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    successCount++;
                }
                else
                {
                    row.Cells["Check"].Value = "FAIL";
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    failCount++;
                }
            }

            MessageBox.Show(
                $"TCP 연결 체크 완료\n\n성공: {successCount}개\n실패: {failCount}개",
                "TCP Connection Check",
                MessageBoxButtons.OK,
                successCount > 0 && failCount == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private bool TryTcpConnect(string ip, int port, int timeoutMs)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(ip, port, null, null);
                    bool success = result.AsyncWaitHandle.WaitOne(timeoutMs);
                    if (success && client.Connected)
                    {
                        client.EndConnect(result);
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Serial Port DataGridView 설정

        private void SetupSerialGrid()
        {
            var dgv = dgvSerialsetting;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.DefaultCellStyle.Font = new Font("맑은 고딕", 9F);

            dgv.Columns["dataGridViewTextBoxColumn1"].FillWeight = 30;
            dgv.Columns["dataGridViewTextBoxColumn2"].FillWeight = 30;
            dgv.Columns["dataGridViewTextBoxColumn3"].FillWeight = 40;
            dgv.Columns["dataGridViewTextBoxColumn4"].FillWeight = 80;
            dgv.Columns["dataGridViewTextBoxColumn5"].FillWeight = 60;
            dgv.Columns["dataGridViewTextBoxColumn6"].FillWeight = 60;

            foreach (DataGridViewColumn col in dgv.Columns)
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;

            dgv.Rows.Clear();
            string[] devNames = { "JIG", "LCR" };
            var ports = SerialPort.GetPortNames().OrderBy(p => p).ToArray();

            for (int i = 0; i < devNames.Length; i++)
            {
                string comPort = (i < ports.Length) ? ports[i] : "COM1";
                dgv.Rows.Add(i + 1, "-", "", devNames[i], comPort, "115200");
            }

            HighlightSelectedRow(dgv);
        }

        private void DgvSerialsetting_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvSerialsetting.Rows[e.RowIndex];

            using (var dlg = new SerialSettingsDialog())
            {
                dlg.DeviceName = row.Cells["dataGridViewTextBoxColumn4"].Value?.ToString() ?? "";
                dlg.CommPort = row.Cells["dataGridViewTextBoxColumn5"].Value?.ToString() ?? "";
                dlg.Baudrate = row.Cells["dataGridViewTextBoxColumn6"].Value?.ToString() ?? "";
                dlg.Enable = (row.Cells["dataGridViewTextBoxColumn2"].Value?.ToString() == "√");

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    row.Cells["dataGridViewTextBoxColumn5"].Value = dlg.CommPort;
                    row.Cells["dataGridViewTextBoxColumn6"].Value = dlg.Baudrate;
                    row.Cells["dataGridViewTextBoxColumn2"].Value = dlg.Enable ? "√" : "-";
                }
            }
        }

        #endregion

        #region Serial Connection Check

        private void CheckSerialConnections()
        {
            var dgv = dgvSerialsetting;
            int successCount = 0;
            int failCount = 0;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                string en = row.Cells["dataGridViewTextBoxColumn2"].Value?.ToString() ?? "";
                if (en != "√")
                {
                    row.Cells["dataGridViewTextBoxColumn3"].Value = "-";
                    row.DefaultCellStyle.BackColor = Color.White;
                    continue;
                }

                string portName = row.Cells["dataGridViewTextBoxColumn5"].Value?.ToString() ?? "";
                string baudStr = row.Cells["dataGridViewTextBoxColumn6"].Value?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(portName) ||
                    !int.TryParse(baudStr, out int baud) || baud <= 0)
                {
                    row.Cells["dataGridViewTextBoxColumn3"].Value = "FAIL";
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    failCount++;
                    continue;
                }

                bool connected = TrySerialConnect(portName, baud);
                if (connected)
                {
                    row.Cells["dataGridViewTextBoxColumn3"].Value = "OK";
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    successCount++;
                }
                else
                {
                    row.Cells["dataGridViewTextBoxColumn3"].Value = "FAIL";
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    failCount++;
                }
            }

            MessageBox.Show(
                $"시리얼 연결 체크 완료\n\n성공: {successCount}개\n실패: {failCount}개",
                "Serial Connection Check",
                MessageBoxButtons.OK,
                successCount > 0 && failCount == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private bool TrySerialConnect(string portName, int baudRate)
        {
            try
            {
                using (var sp = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One))
                {
                    sp.ReadTimeout = 500;
                    sp.WriteTimeout = 500;
                    sp.Open();
                    bool isOpen = sp.IsOpen;
                    sp.Close();
                    return isOpen;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 공통 유틸

        private void HighlightSelectedRow(DataGridView dgv)
        {
            dgv.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            if (dgv.Rows.Count > 0)
                dgv.Rows[0].Selected = true;
        }

        #endregion

        #region Save / Cancel

        private void BtnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("설정이 저장되었습니다.", "Save",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion
    }
}
