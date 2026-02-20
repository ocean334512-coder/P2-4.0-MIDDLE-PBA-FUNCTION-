using System;
using System.Drawing;
using System.Windows.Forms;

namespace _20260202_P2_MIDDLE.Forms
{
    public class IpSettingsDialog : Form
    {
        private TextBox txtIpAddress;
        private TextBox txtPort;
        private CheckBox chkEnable;
        private Button btnOk;
        private Button btnCancel;

        public string IpAddress
        {
            get => txtIpAddress.Text.Trim();
            set => txtIpAddress.Text = value;
        }

        public string PortNumber
        {
            get => txtPort.Text.Trim();
            set => txtPort.Text = value;
        }

        public bool Enable
        {
            get => chkEnable.Checked;
            set => chkEnable.Checked = value;
        }

        public IpSettingsDialog()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = "IP Settings";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(340, 220);
            Font = new Font("맑은 고딕", 9F);

            var grp = new GroupBox
            {
                Text = "IP Settings",
                Location = new Point(15, 10),
                Size = new Size(310, 145),
                Font = new Font("맑은 고딕", 9F)
            };

            var lblIp = new Label
            {
                Text = "IP Address",
                Location = new Point(15, 30),
                Size = new Size(90, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            txtIpAddress = new TextBox
            {
                Location = new Point(110, 30),
                Size = new Size(180, 25)
            };

            var lblPort = new Label
            {
                Text = "Port",
                Location = new Point(15, 65),
                Size = new Size(90, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            txtPort = new TextBox
            {
                Location = new Point(110, 65),
                Size = new Size(180, 25)
            };

            chkEnable = new CheckBox
            {
                Text = "Enable",
                Location = new Point(15, 105),
                Size = new Size(120, 25),
                Checked = false
            };

            grp.Controls.AddRange(new Control[] { lblIp, txtIpAddress, lblPort, txtPort, chkEnable });

            btnOk = new Button
            {
                Text = "OK",
                Location = new Point(15, 168),
                Size = new Size(145, 40),
                DialogResult = DialogResult.OK,
                Font = new Font("맑은 고딕", 9F, FontStyle.Bold)
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(180, 168),
                Size = new Size(145, 40),
                DialogResult = DialogResult.Cancel,
                Font = new Font("맑은 고딕", 9F, FontStyle.Bold)
            };

            AcceptButton = btnOk;
            CancelButton = btnCancel;

            Controls.AddRange(new Control[] { grp, btnOk, btnCancel });
        }
    }
}
