
namespace _20260202_P2_MIDDLE.Forms
{
    partial class RecipeSettingForm
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
            this.grboxOS = new System.Windows.Forms.GroupBox();
            this.GboxCon1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Check = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberCon1 = new System.Windows.Forms.NumericUpDown();
            this.BtnCon1Set = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grbSpec = new System.Windows.Forms.GroupBox();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JUDGMENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SETTING = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MIN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.grbOSTest = new System.Windows.Forms.GroupBox();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chbTcOpen = new System.Windows.Forms.CheckBox();
            this.chbTcShort = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.grboxOS.SuspendLayout();
            this.GboxCon1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberCon1)).BeginInit();
            this.grbSpec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.groupBox11.SuspendLayout();
            this.grbOSTest.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // grboxOS
            // 
            this.grboxOS.BackColor = System.Drawing.SystemColors.Control;
            this.grboxOS.Controls.Add(this.GboxCon1);
            this.grboxOS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.grboxOS.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grboxOS.Location = new System.Drawing.Point(25, 267);
            this.grboxOS.Name = "grboxOS";
            this.grboxOS.Size = new System.Drawing.Size(2403, 756);
            this.grboxOS.TabIndex = 6;
            this.grboxOS.TabStop = false;
            this.grboxOS.Text = "O/S TEST";
            // 
            // GboxCon1
            // 
            this.GboxCon1.Controls.Add(this.dataGridView1);
            this.GboxCon1.Controls.Add(this.NumberCon1);
            this.GboxCon1.Controls.Add(this.BtnCon1Set);
            this.GboxCon1.Controls.Add(this.label1);
            this.GboxCon1.Location = new System.Drawing.Point(20, 43);
            this.GboxCon1.Margin = new System.Windows.Forms.Padding(4);
            this.GboxCon1.Name = "GboxCon1";
            this.GboxCon1.Padding = new System.Windows.Forms.Padding(4);
            this.GboxCon1.Size = new System.Drawing.Size(706, 706);
            this.GboxCon1.TabIndex = 6;
            this.GboxCon1.TabStop = false;
            this.GboxCon1.Text = "MAIN CON";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Check,
            this.Num,
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(21, 93);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(676, 601);
            this.dataGridView1.TabIndex = 3;
            // 
            // Check
            // 
            this.Check.HeaderText = "Check";
            this.Check.MinimumWidth = 8;
            this.Check.Name = "Check";
            this.Check.Width = 150;
            // 
            // Num
            // 
            this.Num.HeaderText = "Num";
            this.Num.MinimumWidth = 8;
            this.Num.Name = "Num";
            this.Num.Width = 150;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Open Pin";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Short Pin";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // NumberCon1
            // 
            this.NumberCon1.Location = new System.Drawing.Point(171, 52);
            this.NumberCon1.Margin = new System.Windows.Forms.Padding(4);
            this.NumberCon1.Name = "NumberCon1";
            this.NumberCon1.Size = new System.Drawing.Size(171, 34);
            this.NumberCon1.TabIndex = 1;
            // 
            // BtnCon1Set
            // 
            this.BtnCon1Set.Location = new System.Drawing.Point(373, 50);
            this.BtnCon1Set.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCon1Set.Name = "BtnCon1Set";
            this.BtnCon1Set.Size = new System.Drawing.Size(107, 34);
            this.BtnCon1Set.TabIndex = 2;
            this.BtnCon1Set.Text = "SET";
            this.BtnCon1Set.UseVisualStyleBackColor = true;
            this.BtnCon1Set.Click += new System.EventHandler(this.BtnCon1Set_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(48, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "COUNT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grbSpec
            // 
            this.grbSpec.Controls.Add(this.dataGridView4);
            this.grbSpec.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grbSpec.Location = new System.Drawing.Point(25, 1029);
            this.grbSpec.Name = "grbSpec";
            this.grbSpec.Size = new System.Drawing.Size(2403, 354);
            this.grbSpec.TabIndex = 9;
            this.grbSpec.TabStop = false;
            this.grbSpec.Text = "SPEC";
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.TEST,
            this.JUDGMENT,
            this.ITEM,
            this.SETTING,
            this.MIN,
            this.MAX});
            this.dataGridView4.Location = new System.Drawing.Point(6, 33);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersWidth = 62;
            this.dataGridView4.RowTemplate.Height = 30;
            this.dataGridView4.Size = new System.Drawing.Size(1418, 306);
            this.dataGridView4.TabIndex = 4;
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.MinimumWidth = 8;
            this.No.Name = "No";
            this.No.Width = 150;
            // 
            // TEST
            // 
            this.TEST.HeaderText = "TEST";
            this.TEST.MinimumWidth = 8;
            this.TEST.Name = "TEST";
            this.TEST.Width = 150;
            // 
            // JUDGMENT
            // 
            this.JUDGMENT.HeaderText = "JUDGMENT";
            this.JUDGMENT.MinimumWidth = 8;
            this.JUDGMENT.Name = "JUDGMENT";
            this.JUDGMENT.Width = 150;
            // 
            // ITEM
            // 
            this.ITEM.HeaderText = "ITEM";
            this.ITEM.MinimumWidth = 8;
            this.ITEM.Name = "ITEM";
            this.ITEM.Width = 150;
            // 
            // SETTING
            // 
            this.SETTING.HeaderText = "SETTING";
            this.SETTING.MinimumWidth = 8;
            this.SETTING.Name = "SETTING";
            this.SETTING.Width = 150;
            // 
            // MIN
            // 
            this.MIN.HeaderText = "MIN";
            this.MIN.MinimumWidth = 8;
            this.MIN.Name = "MIN";
            this.MIN.Width = 150;
            // 
            // MAX
            // 
            this.MAX.HeaderText = "MAX";
            this.MAX.MinimumWidth = 8;
            this.MAX.Name = "MAX";
            this.MAX.Width = 150;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.checkBox1);
            this.groupBox11.Location = new System.Drawing.Point(2590, 59);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(230, 221);
            this.groupBox11.TabIndex = 14;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "COMPONENT";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox1.Location = new System.Drawing.Point(6, 44);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(73, 32);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "USE";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // grbOSTest
            // 
            this.grbOSTest.Controls.Add(this.groupBox8);
            this.grbOSTest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.grbOSTest.Location = new System.Drawing.Point(6, 35);
            this.grbOSTest.Name = "grbOSTest";
            this.grbOSTest.Size = new System.Drawing.Size(582, 209);
            this.grbOSTest.TabIndex = 15;
            this.grbOSTest.TabStop = false;
            this.grbOSTest.Text = "O/S TEST";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.groupBox17);
            this.groupBox16.Location = new System.Drawing.Point(1164, 35);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(288, 209);
            this.groupBox16.TabIndex = 18;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "COLOR SENSOR";
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.checkBox5);
            this.groupBox17.Location = new System.Drawing.Point(6, 48);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(276, 152);
            this.groupBox17.TabIndex = 14;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "I2C";
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox5.Location = new System.Drawing.Point(6, 44);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(242, 32);
            this.checkBox5.TabIndex = 2;
            this.checkBox5.Text = "COLOR SENSOR DATA";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.groupBox19);
            this.groupBox18.Location = new System.Drawing.Point(1473, 35);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(288, 209);
            this.groupBox18.TabIndex = 19;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "ADC VOLATE";
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.checkBox6);
            this.groupBox19.Location = new System.Drawing.Point(6, 48);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(254, 152);
            this.groupBox19.TabIndex = 14;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "VOLT";
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox6.Location = new System.Drawing.Point(6, 44);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(72, 32);
            this.checkBox6.TabIndex = 2;
            this.checkBox6.Text = "LED";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox18);
            this.groupBox4.Controls.Add(this.groupBox16);
            this.groupBox4.Controls.Add(this.grbOSTest);
            this.groupBox4.Controls.Add(this.groupBox11);
            this.groupBox4.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox4.Location = new System.Drawing.Point(25, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(2403, 249);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Test Items";
            // 
            // chbTcOpen
            // 
            this.chbTcOpen.AutoSize = true;
            this.chbTcOpen.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chbTcOpen.Location = new System.Drawing.Point(6, 82);
            this.chbTcOpen.Name = "chbTcOpen";
            this.chbTcOpen.Size = new System.Drawing.Size(89, 32);
            this.chbTcOpen.TabIndex = 1;
            this.chbTcOpen.Text = "OPEN";
            this.chbTcOpen.UseVisualStyleBackColor = true;
            // 
            // chbTcShort
            // 
            this.chbTcShort.AutoSize = true;
            this.chbTcShort.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chbTcShort.Location = new System.Drawing.Point(6, 44);
            this.chbTcShort.Name = "chbTcShort";
            this.chbTcShort.Size = new System.Drawing.Size(101, 32);
            this.chbTcShort.TabIndex = 2;
            this.chbTcShort.Text = "SHORT";
            this.chbTcShort.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.chbTcShort);
            this.groupBox8.Controls.Add(this.chbTcOpen);
            this.groupBox8.Location = new System.Drawing.Point(165, 48);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(142, 152);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "TOP CON";
            // 
            // RecipeSettingForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2448, 1385);
            this.Controls.Add(this.grbSpec);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.grboxOS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "RecipeSettingForm";
            this.Text = "Setting";
            this.grboxOS.ResumeLayout(false);
            this.GboxCon1.ResumeLayout(false);
            this.GboxCon1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumberCon1)).EndInit();
            this.grbSpec.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.grbOSTest.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grboxOS;
        private System.Windows.Forms.GroupBox GboxCon1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.NumericUpDown NumberCon1;
        private System.Windows.Forms.Button BtnCon1Set;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbSpec;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEST;
        private System.Windows.Forms.DataGridViewTextBoxColumn JUDGMENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM;
        private System.Windows.Forms.DataGridViewTextBoxColumn SETTING;
        private System.Windows.Forms.DataGridViewTextBoxColumn MIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAX;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox grbOSTest;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox chbTcShort;
        private System.Windows.Forms.CheckBox chbTcOpen;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}