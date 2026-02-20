
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvIpsetting = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.En = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Check = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Board = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnConnectCheck = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDBsetting = new System.Windows.Forms.Button();
            this.btnComSave = new System.Windows.Forms.Button();
            this.btnComCancel = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvSerialsetting = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIpsetting)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerialsetting)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvIpsetting);
            this.groupBox1.Font = new System.Drawing.Font("¸¼Àº °íµñ", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(945, 323);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP Address";
            // 
            // dgvIpsetting
            // 
            this.dgvIpsetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIpsetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.En,
            this.Check,
            this.Board,
            this.IPAddress,
            this.Port});
            this.dgvIpsetting.Location = new System.Drawing.Point(3, 30);
            this.dgvIpsetting.Name = "dgvIpsetting";
            this.dgvIpsetting.RowHeadersWidth = 62;
            this.dgvIpsetting.RowTemplate.Height = 30;
            this.dgvIpsetting.Size = new System.Drawing.Size(936, 273);
            this.dgvIpsetting.TabIndex = 0;
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.MinimumWidth = 8;
            this.No.Name = "No";
            this.No.Width = 150;
            // 
            // En
            // 
            this.En.HeaderText = "En";
            this.En.MinimumWidth = 8;
            this.En.Name = "En";
            this.En.Width = 150;
            // 
            // Check
            // 
            this.Check.HeaderText = "Check";
            this.Check.MinimumWidth = 8;
            this.Check.Name = "Check";
            this.Check.Width = 150;
            // 
            // Board
            // 
            this.Board.HeaderText = "Board";
            this.Board.MinimumWidth = 8;
            this.Board.Name = "Board";
            this.Board.Width = 150;
            // 
            // IPAddress
            // 
            this.IPAddress.HeaderText = "IP Address";
            this.IPAddress.MinimumWidth = 8;
            this.IPAddress.Name = "IPAddress";
            this.IPAddress.Width = 150;
            // 
            // Port
            // 
            this.Port.HeaderText = "Port";
            this.Port.MinimumWidth = 8;
            this.Port.Name = "Port";
            this.Port.Width = 150;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnConnectCheck);
            this.groupBox2.Font = new System.Drawing.Font("¸¼Àº °íµñ", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(975, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(351, 126);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conection Check";
            // 
            // btnConnectCheck
            // 
            this.btnConnectCheck.Location = new System.Drawing.Point(6, 33);
            this.btnConnectCheck.Name = "btnConnectCheck";
            this.btnConnectCheck.Size = new System.Drawing.Size(336, 83);
            this.btnConnectCheck.TabIndex = 0;
            this.btnConnectCheck.Text = "Check";
            this.btnConnectCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDBsetting);
            this.groupBox3.Font = new System.Drawing.Font("¸¼Àº °íµñ", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox3.Location = new System.Drawing.Point(975, 159);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(351, 515);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "MES";
            // 
            // btnDBsetting
            // 
            this.btnDBsetting.Location = new System.Drawing.Point(6, 426);
            this.btnDBsetting.Name = "btnDBsetting";
            this.btnDBsetting.Size = new System.Drawing.Size(336, 83);
            this.btnDBsetting.TabIndex = 3;
            this.btnDBsetting.Text = "DB Setting";
            this.btnDBsetting.UseVisualStyleBackColor = true;
            // 
            // btnComSave
            // 
            this.btnComSave.Font = new System.Drawing.Font("¸¼Àº °íµñ", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnComSave.Location = new System.Drawing.Point(12, 680);
            this.btnComSave.Name = "btnComSave";
            this.btnComSave.Size = new System.Drawing.Size(649, 83);
            this.btnComSave.TabIndex = 4;
            this.btnComSave.Text = "Save";
            this.btnComSave.UseVisualStyleBackColor = true;
            // 
            // btnComCancel
            // 
            this.btnComCancel.Font = new System.Drawing.Font("¸¼Àº °íµñ", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnComCancel.Location = new System.Drawing.Point(677, 680);
            this.btnComCancel.Name = "btnComCancel";
            this.btnComCancel.Size = new System.Drawing.Size(649, 83);
            this.btnComCancel.TabIndex = 5;
            this.btnComCancel.Text = "Cancel";
            this.btnComCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvSerialsetting);
            this.groupBox4.Font = new System.Drawing.Font("¸¼Àº °íµñ", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox4.Location = new System.Drawing.Point(15, 356);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(945, 296);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Serial Port";
            // 
            // dgvSerialsetting
            // 
            this.dgvSerialsetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSerialsetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dgvSerialsetting.Location = new System.Drawing.Point(3, 30);
            this.dgvSerialsetting.Name = "dgvSerialsetting";
            this.dgvSerialsetting.RowHeadersWidth = 62;
            this.dgvSerialsetting.RowTemplate.Height = 30;
            this.dgvSerialsetting.Size = new System.Drawing.Size(936, 273);
            this.dgvSerialsetting.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "No";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "En";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Check";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Dev";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Port";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Baud";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // ComSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1345, 777);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnComCancel);
            this.Controls.Add(this.btnComSave);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ComSettingForm";
            this.Text = "ComSetting";
            this.Load += new System.EventHandler(this.ComSettingForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIpsetting)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSerialsetting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnConnectCheck;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDBsetting;
        private System.Windows.Forms.Button btnComSave;
        private System.Windows.Forms.Button btnComCancel;
        private System.Windows.Forms.DataGridView dgvIpsetting;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn En;
        private System.Windows.Forms.DataGridViewTextBoxColumn Check;
        private System.Windows.Forms.DataGridViewTextBoxColumn Board;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn Port;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvSerialsetting;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}
