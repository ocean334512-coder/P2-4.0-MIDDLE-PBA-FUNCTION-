
namespace _20260202_P2_MIDDLE.Forms
{
    partial class ComSettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.btnOpenFtpSetting = new System.Windows.Forms.Button();
            this.btnOpenMesSetting = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnRescan = new System.Windows.Forms.Button();
            this.tboxPbaRetryCount = new System.Windows.Forms.TextBox();
            this.tboxBoardRetryCount = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tboxBoardReadTimeOut = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.tboxBoardConnectTimeOut = new System.Windows.Forms.TextBox();
            this.gboxTimeOutValue = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel21 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCh1Status = new System.Windows.Forms.Label();
            this.tboxTcpCh1Port = new System.Windows.Forms.TextBox();
            this.tboxTcpCh1Ip = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cboxJigPort = new System.Windows.Forms.ComboBox();
            this.cboxJigBaudRate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.cboxLcrPort = new System.Windows.Forms.ComboBox();
            this.cboxLcrBaudRate = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.EthernetUseCheck = new System.Windows.Forms.CheckBox();
            this.JIGtUseCheck = new System.Windows.Forms.CheckBox();
            this.LcrUseCheck = new System.Windows.Forms.CheckBox();
            this.EternetComeState = new System.Windows.Forms.Label();
            this.JigComeState = new System.Windows.Forms.Label();
            this.LcrComeState = new System.Windows.Forms.Label();
            this.groupBox19.SuspendLayout();
            this.gboxTimeOutValue.SuspendLayout();
            this.tableLayoutPanel21.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.tableLayoutPanel17.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.tableLayoutPanel18.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel19
            // 
            this.tableLayoutPanel19.ColumnCount = 3;
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel19.Location = new System.Drawing.Point(3, 24);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            this.tableLayoutPanel19.RowCount = 4;
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(765, 181);
            this.tableLayoutPanel19.TabIndex = 0;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.tableLayoutPanel19);
            this.groupBox19.Location = new System.Drawing.Point(740, 626);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(771, 208);
            this.groupBox19.TabIndex = 35;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Function Setting";
            // 
            // btnOpenFtpSetting
            // 
            this.btnOpenFtpSetting.Enabled = false;
            this.btnOpenFtpSetting.Location = new System.Drawing.Point(887, 225);
            this.btnOpenFtpSetting.Name = "btnOpenFtpSetting";
            this.btnOpenFtpSetting.Size = new System.Drawing.Size(137, 70);
            this.btnOpenFtpSetting.TabIndex = 34;
            this.btnOpenFtpSetting.Text = "FTP";
            this.btnOpenFtpSetting.UseVisualStyleBackColor = true;
            // 
            // btnOpenMesSetting
            // 
            this.btnOpenMesSetting.Location = new System.Drawing.Point(740, 225);
            this.btnOpenMesSetting.Name = "btnOpenMesSetting";
            this.btnOpenMesSetting.Size = new System.Drawing.Size(129, 70);
            this.btnOpenMesSetting.TabIndex = 33;
            this.btnOpenMesSetting.Text = "MES";
            this.btnOpenMesSetting.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1377, 118);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(137, 70);
            this.btnClose.TabIndex = 32;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(743, 148);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(126, 70);
            this.btnConnect.TabIndex = 31;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnRescan
            // 
            this.btnRescan.Location = new System.Drawing.Point(584, 148);
            this.btnRescan.Name = "btnRescan";
            this.btnRescan.Size = new System.Drawing.Size(129, 70);
            this.btnRescan.TabIndex = 30;
            this.btnRescan.Text = "Rescan";
            this.btnRescan.UseVisualStyleBackColor = true;
            // 
            // tboxPbaRetryCount
            // 
            this.tboxPbaRetryCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tboxPbaRetryCount.Font = new System.Drawing.Font("Calibri", 11F);
            this.tboxPbaRetryCount.Location = new System.Drawing.Point(303, 240);
            this.tboxPbaRetryCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tboxPbaRetryCount.Name = "tboxPbaRetryCount";
            this.tboxPbaRetryCount.Size = new System.Drawing.Size(161, 34);
            this.tboxPbaRetryCount.TabIndex = 25;
            // 
            // tboxBoardRetryCount
            // 
            this.tboxBoardRetryCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tboxBoardRetryCount.Font = new System.Drawing.Font("Calibri", 11F);
            this.tboxBoardRetryCount.Location = new System.Drawing.Point(303, 182);
            this.tboxBoardRetryCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tboxBoardRetryCount.Name = "tboxBoardRetryCount";
            this.tboxBoardRetryCount.Size = new System.Drawing.Size(161, 34);
            this.tboxBoardRetryCount.TabIndex = 24;
            // 
            // label27
            // 
            this.label27.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Calibri", 11F);
            this.label27.Location = new System.Drawing.Point(18, 129);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(220, 27);
            this.label27.TabIndex = 5;
            this.label27.Text = "Board Read Delay (ms)";
            // 
            // tboxBoardReadTimeOut
            // 
            this.tboxBoardReadTimeOut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tboxBoardReadTimeOut.Font = new System.Drawing.Font("Calibri", 11F);
            this.tboxBoardReadTimeOut.Location = new System.Drawing.Point(303, 125);
            this.tboxBoardReadTimeOut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tboxBoardReadTimeOut.Name = "tboxBoardReadTimeOut";
            this.tboxBoardReadTimeOut.Size = new System.Drawing.Size(161, 34);
            this.tboxBoardReadTimeOut.TabIndex = 4;
            // 
            // label29
            // 
            this.label29.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Calibri", 11F);
            this.label29.Location = new System.Drawing.Point(3, 15);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(249, 27);
            this.label29.TabIndex = 1;
            this.label29.Text = "Board Connect Delay (ms)";
            // 
            // tboxBoardConnectTimeOut
            // 
            this.tboxBoardConnectTimeOut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tboxBoardConnectTimeOut.Font = new System.Drawing.Font("Calibri", 11F);
            this.tboxBoardConnectTimeOut.Location = new System.Drawing.Point(303, 11);
            this.tboxBoardConnectTimeOut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tboxBoardConnectTimeOut.Name = "tboxBoardConnectTimeOut";
            this.tboxBoardConnectTimeOut.Size = new System.Drawing.Size(161, 34);
            this.tboxBoardConnectTimeOut.TabIndex = 0;
            // 
            // gboxTimeOutValue
            // 
            this.gboxTimeOutValue.Controls.Add(this.tableLayoutPanel21);
            this.gboxTimeOutValue.Location = new System.Drawing.Point(740, 303);
            this.gboxTimeOutValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gboxTimeOutValue.Name = "gboxTimeOutValue";
            this.gboxTimeOutValue.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gboxTimeOutValue.Size = new System.Drawing.Size(774, 315);
            this.gboxTimeOutValue.TabIndex = 29;
            this.gboxTimeOutValue.TabStop = false;
            this.gboxTimeOutValue.Text = "Value Setting";
            // 
            // tableLayoutPanel21
            // 
            this.tableLayoutPanel21.ColumnCount = 3;
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel21.Controls.Add(this.tboxPbaRetryCount, 1, 4);
            this.tableLayoutPanel21.Controls.Add(this.tboxBoardRetryCount, 1, 3);
            this.tableLayoutPanel21.Controls.Add(this.tboxBoardReadTimeOut, 1, 2);
            this.tableLayoutPanel21.Controls.Add(this.tboxBoardConnectTimeOut, 1, 0);
            this.tableLayoutPanel21.Controls.Add(this.label27, 0, 2);
            this.tableLayoutPanel21.Controls.Add(this.label29, 0, 0);
            this.tableLayoutPanel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel21.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel21.Name = "tableLayoutPanel21";
            this.tableLayoutPanel21.RowCount = 5;
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel21.Size = new System.Drawing.Size(768, 286);
            this.tableLayoutPanel21.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lblCh1Status, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tboxTcpCh1Port, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.tboxTcpCh1Ip, 1, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.95238F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.99813F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(257, 182);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.MintCream;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 135);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 46);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Honeydew;
            this.tableLayoutPanel3.SetColumnSpan(this.panel1, 2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 45);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.MintCream;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 78);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 56);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCh1Status
            // 
            this.lblCh1Status.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.lblCh1Status, 2);
            this.lblCh1Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCh1Status.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCh1Status.Location = new System.Drawing.Point(1, 47);
            this.lblCh1Status.Margin = new System.Windows.Forms.Padding(0);
            this.lblCh1Status.Name = "lblCh1Status";
            this.lblCh1Status.Size = new System.Drawing.Size(255, 30);
            this.lblCh1Status.TabIndex = 3;
            this.lblCh1Status.Text = "-";
            this.lblCh1Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tboxTcpCh1Port
            // 
            this.tboxTcpCh1Port.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tboxTcpCh1Port.Font = new System.Drawing.Font("Calibri", 11F);
            this.tboxTcpCh1Port.Location = new System.Drawing.Point(132, 89);
            this.tboxTcpCh1Port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tboxTcpCh1Port.Name = "tboxTcpCh1Port";
            this.tboxTcpCh1Port.Size = new System.Drawing.Size(120, 34);
            this.tboxTcpCh1Port.TabIndex = 6;
            this.tboxTcpCh1Port.TextChanged += new System.EventHandler(this.tboxTcpCh1Port_TextChanged);
            // 
            // tboxTcpCh1Ip
            // 
            this.tboxTcpCh1Ip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tboxTcpCh1Ip.Font = new System.Drawing.Font("Calibri", 11F);
            this.tboxTcpCh1Ip.Location = new System.Drawing.Point(132, 141);
            this.tboxTcpCh1Ip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tboxTcpCh1Ip.Name = "tboxTcpCh1Ip";
            this.tboxTcpCh1Ip.Size = new System.Drawing.Size(120, 34);
            this.tboxTcpCh1Ip.TabIndex = 5;
            this.tboxTcpCh1Ip.TextChanged += new System.EventHandler(this.tboxTcpCh1Ip_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 210);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ethernet";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.groupBox17, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(10, 32);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.79816F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.68807F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.77982F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.275229F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(269, 656);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 219);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 163);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "JIG ";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.cboxJigPort, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.cboxJigBaudRate, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0025F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.9975F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(257, 135);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // cboxJigPort
            // 
            this.cboxJigPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboxJigPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxJigPort.Font = new System.Drawing.Font("Calibri", 11F);
            this.cboxJigPort.FormattingEnabled = true;
            this.cboxJigPort.Location = new System.Drawing.Point(132, 11);
            this.cboxJigPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxJigPort.Name = "cboxJigPort";
            this.cboxJigPort.Size = new System.Drawing.Size(120, 35);
            this.cboxJigPort.TabIndex = 11;
            // 
            // cboxJigBaudRate
            // 
            this.cboxJigBaudRate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboxJigBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxJigBaudRate.Font = new System.Drawing.Font("Calibri", 11F);
            this.cboxJigBaudRate.FormattingEnabled = true;
            this.cboxJigBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "115200"});
            this.cboxJigBaudRate.Location = new System.Drawing.Point(132, 67);
            this.cboxJigBaudRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxJigBaudRate.Name = "cboxJigBaudRate";
            this.cboxJigBaudRate.Size = new System.Drawing.Size(120, 35);
            this.cboxJigBaudRate.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.MintCream;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 57);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 55);
            this.label3.TabIndex = 4;
            this.label3.Text = "Baud Rate";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.MintCream;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 55);
            this.label4.TabIndex = 1;
            this.label4.Text = "Port";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.tableLayoutPanel17);
            this.groupBox17.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox17.Location = new System.Drawing.Point(3, 388);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.groupBox17.Size = new System.Drawing.Size(263, 225);
            this.groupBox17.TabIndex = 30;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "LCR";
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.ColumnCount = 1;
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Controls.Add(this.groupBox18, 0, 0);
            this.tableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel17.Location = new System.Drawing.Point(10, 32);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 1;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(243, 183);
            this.tableLayoutPanel17.TabIndex = 0;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.tableLayoutPanel18);
            this.groupBox18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox18.Location = new System.Drawing.Point(3, 3);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(237, 177);
            this.groupBox18.TabIndex = 0;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "LCR";
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel18.ColumnCount = 2;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel18.Controls.Add(this.cboxLcrPort, 1, 0);
            this.tableLayoutPanel18.Controls.Add(this.cboxLcrBaudRate, 1, 1);
            this.tableLayoutPanel18.Controls.Add(this.label25, 0, 1);
            this.tableLayoutPanel18.Controls.Add(this.label26, 0, 0);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 2;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0025F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.9975F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(231, 149);
            this.tableLayoutPanel18.TabIndex = 0;
            // 
            // cboxLcrPort
            // 
            this.cboxLcrPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboxLcrPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxLcrPort.Font = new System.Drawing.Font("Calibri", 11F);
            this.cboxLcrPort.FormattingEnabled = true;
            this.cboxLcrPort.Location = new System.Drawing.Point(119, 20);
            this.cboxLcrPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxLcrPort.Name = "cboxLcrPort";
            this.cboxLcrPort.Size = new System.Drawing.Size(107, 35);
            this.cboxLcrPort.TabIndex = 12;
            // 
            // cboxLcrBaudRate
            // 
            this.cboxLcrBaudRate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboxLcrBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxLcrBaudRate.Font = new System.Drawing.Font("Calibri", 11F);
            this.cboxLcrBaudRate.FormattingEnabled = true;
            this.cboxLcrBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "115200"});
            this.cboxLcrBaudRate.Location = new System.Drawing.Point(119, 94);
            this.cboxLcrBaudRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxLcrBaudRate.Name = "cboxLcrBaudRate";
            this.cboxLcrBaudRate.Size = new System.Drawing.Size(107, 35);
            this.cboxLcrBaudRate.TabIndex = 6;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.MintCream;
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(1, 75);
            this.label25.Margin = new System.Windows.Forms.Padding(0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(114, 73);
            this.label25.TabIndex = 4;
            this.label25.Text = "Baud Rate";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.MintCream;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(1, 1);
            this.label26.Margin = new System.Windows.Forms.Padding(0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(114, 73);
            this.label26.TabIndex = 1;
            this.label26.Text = "Port";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel5);
            this.groupBox5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(290, 135);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.groupBox5.Size = new System.Drawing.Size(289, 698);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Com";
            // 
            // EthernetUseCheck
            // 
            this.EthernetUseCheck.AutoSize = true;
            this.EthernetUseCheck.Location = new System.Drawing.Point(143, 294);
            this.EthernetUseCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EthernetUseCheck.Name = "EthernetUseCheck";
            this.EthernetUseCheck.Size = new System.Drawing.Size(140, 22);
            this.EthernetUseCheck.TabIndex = 36;
            this.EthernetUseCheck.Text = "Ethernet USE";
            this.EthernetUseCheck.UseVisualStyleBackColor = true;
            // 
            // JIGtUseCheck
            // 
            this.JIGtUseCheck.AutoSize = true;
            this.JIGtUseCheck.Location = new System.Drawing.Point(143, 460);
            this.JIGtUseCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.JIGtUseCheck.Name = "JIGtUseCheck";
            this.JIGtUseCheck.Size = new System.Drawing.Size(96, 22);
            this.JIGtUseCheck.TabIndex = 37;
            this.JIGtUseCheck.Text = "JIG USE";
            this.JIGtUseCheck.UseVisualStyleBackColor = true;
            // 
            // LcrUseCheck
            // 
            this.LcrUseCheck.AutoSize = true;
            this.LcrUseCheck.Location = new System.Drawing.Point(143, 688);
            this.LcrUseCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LcrUseCheck.Name = "LcrUseCheck";
            this.LcrUseCheck.Size = new System.Drawing.Size(105, 22);
            this.LcrUseCheck.TabIndex = 38;
            this.LcrUseCheck.Text = "LCR USE";
            this.LcrUseCheck.UseVisualStyleBackColor = true;
            // 
            // EternetComeState
            // 
            this.EternetComeState.AutoSize = true;
            this.EternetComeState.BackColor = System.Drawing.Color.Red;
            this.EternetComeState.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EternetComeState.Location = new System.Drawing.Point(580, 280);
            this.EternetComeState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EternetComeState.Name = "EternetComeState";
            this.EternetComeState.Size = new System.Drawing.Size(129, 28);
            this.EternetComeState.TabIndex = 39;
            this.EternetComeState.Text = "Not Connect";
            // 
            // JigComeState
            // 
            this.JigComeState.AutoSize = true;
            this.JigComeState.BackColor = System.Drawing.Color.Red;
            this.JigComeState.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.JigComeState.Location = new System.Drawing.Point(580, 411);
            this.JigComeState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.JigComeState.Name = "JigComeState";
            this.JigComeState.Size = new System.Drawing.Size(129, 28);
            this.JigComeState.TabIndex = 40;
            this.JigComeState.Text = "Not Connect";
            // 
            // LcrComeState
            // 
            this.LcrComeState.AutoSize = true;
            this.LcrComeState.BackColor = System.Drawing.Color.Red;
            this.LcrComeState.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LcrComeState.Location = new System.Drawing.Point(580, 586);
            this.LcrComeState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LcrComeState.Name = "LcrComeState";
            this.LcrComeState.Size = new System.Drawing.Size(129, 28);
            this.LcrComeState.TabIndex = 41;
            this.LcrComeState.Text = "Not Connect";
            // 
            // ComSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1817, 1030);
            this.Controls.Add(this.LcrComeState);
            this.Controls.Add(this.JigComeState);
            this.Controls.Add(this.EternetComeState);
            this.Controls.Add(this.LcrUseCheck);
            this.Controls.Add(this.JIGtUseCheck);
            this.Controls.Add(this.EthernetUseCheck);
            this.Controls.Add(this.groupBox19);
            this.Controls.Add(this.btnOpenFtpSetting);
            this.Controls.Add(this.btnOpenMesSetting);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnRescan);
            this.Controls.Add(this.gboxTimeOutValue);
            this.Controls.Add(this.groupBox5);
            this.Name = "ComSettingForm";
            this.Text = "ComSetting";
            this.groupBox19.ResumeLayout(false);
            this.gboxTimeOutValue.ResumeLayout(false);
            this.tableLayoutPanel21.ResumeLayout(false);
            this.tableLayoutPanel21.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.tableLayoutPanel17.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tableLayoutPanel18.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel19;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.Button btnOpenFtpSetting;
        private System.Windows.Forms.Button btnOpenMesSetting;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnRescan;
        private System.Windows.Forms.TextBox tboxPbaRetryCount;
        private System.Windows.Forms.TextBox tboxBoardRetryCount;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox tboxBoardReadTimeOut;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tboxBoardConnectTimeOut;
        private System.Windows.Forms.GroupBox gboxTimeOutValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel21;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox tboxTcpCh1Port;
        private System.Windows.Forms.TextBox tboxTcpCh1Ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCh1Status;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ComboBox cboxJigPort;
        private System.Windows.Forms.ComboBox cboxJigBaudRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel17;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
        private System.Windows.Forms.ComboBox cboxLcrPort;
        private System.Windows.Forms.ComboBox cboxLcrBaudRate;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox EthernetUseCheck;
        private System.Windows.Forms.CheckBox JIGtUseCheck;
        private System.Windows.Forms.CheckBox LcrUseCheck;
        private System.Windows.Forms.Label EternetComeState;
        private System.Windows.Forms.Label JigComeState;
        private System.Windows.Forms.Label LcrComeState;
    }
}