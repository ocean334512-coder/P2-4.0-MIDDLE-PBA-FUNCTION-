using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace _20260202_P2_MIDDLE.Forms
{
    public class SerialSettingsDialog : Form
    {
        private ComboBox cboxCommPort;
        private ComboBox cboxBaudrate;
        private CheckBox chkEnable;
        private Button btnOk;
        private Button btnCancel;
        private Label lblDeviceName;

        public string DeviceName
        {
            get => lblDeviceName.Text;
            set => lblDeviceName.Text = $"[{value}] Port Setting";
        }

        public string CommPort
        {
            get => cboxCommPort.SelectedItem?.ToString() ?? cboxCommPort.Text;
            set
            {
                int idx = cboxCommPort.Items.IndexOf(value);
                if (idx >= 0) cboxCommPort.SelectedIndex = idx;
                else cboxCommPort.Text = value;
            }
        }

        public string Baudrate
        {
            get => cboxBaudrate.SelectedItem?.ToString() ?? cboxBaudrate.Text;
            set
            {
                int idx = cboxBaudrate.Items.IndexOf(value);
                if (idx >= 0) cboxBaudrate.SelectedIndex = idx;
                else cboxBaudrate.Text = value;
            }
        }

        public bool Enable
        {
            get => chkEnable.Checked;
            set => chkEnable.Checked = value;
        }

        public SerialSettingsDialog()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = "Serial Port Setting";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(310, 250);
            Font = new Font("맑은 고딕", 9F);

            lblDeviceName = new Label
            {
                Text = "[Device] Port Setting",
                Location = new Point(20, 15),
                Size = new Size(270, 25),
                Font = new Font("맑은 고딕", 9F, FontStyle.Bold)
            };

            var lblComm = new Label
            {
                Text = "Comm Port",
                Location = new Point(20, 50),
                Size = new Size(100, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            cboxCommPort = new ComboBox
            {
                Location = new Point(130, 50),
                Size = new Size(155, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var ports = SerialPort.GetPortNames().OrderBy(p => p).ToArray();
            cboxCommPort.Items.AddRange(ports);
            if (ports.Length > 0) cboxCommPort.SelectedIndex = 0;

            var lblBaud = new Label
            {
                Text = "Baudrate",
                Location = new Point(20, 90),
                Size = new Size(100, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            cboxBaudrate = new ComboBox
            {
                Location = new Point(130, 90),
                Size = new Size(155, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboxBaudrate.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200", "230400" });
            cboxBaudrate.SelectedItem = "115200";

            var grpUse = new GroupBox
            {
                Text = "Use Comm Port",
                Location = new Point(20, 125),
                Size = new Size(270, 55)
            };

            chkEnable = new CheckBox
            {
                Text = "Enable this Comm Port",
                Location = new Point(15, 22),
                Size = new Size(240, 25),
                Checked = false
            };
            grpUse.Controls.Add(chkEnable);

            btnOk = new Button
            {
                Text = "OK",
                Location = new Point(20, 195),
                Size = new Size(125, 40),
                DialogResult = DialogResult.OK,
                Font = new Font("맑은 고딕", 9F, FontStyle.Bold)
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(165, 195),
                Size = new Size(125, 40),
                DialogResult = DialogResult.Cancel,
                Font = new Font("맑은 고딕", 9F, FontStyle.Bold)
            };

            AcceptButton = btnOk;
            CancelButton = btnCancel;

            Controls.AddRange(new Control[] { lblDeviceName, lblComm, cboxCommPort, lblBaud, cboxBaudrate, grpUse, btnOk, btnCancel });
        }
    }
}
