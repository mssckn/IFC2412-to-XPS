namespace Test_MicroEpsilon
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txt_SensorType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeviceName = new System.Windows.Forms.TextBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.txt_Msg = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Lbl_index0_Scale = new System.Windows.Forms.Label();
            this.Lbl_index0_Raw = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Lbl_index1_Scale = new System.Windows.Forms.Label();
            this.Lbl_index1_Raw = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Lbl_index2_Scale = new System.Windows.Forms.Label();
            this.Lbl_index2_Raw = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_CreateMaster = new System.Windows.Forms.Button();
            this.btn_Master = new System.Windows.Forms.Button();
            this.Btn_ResetMaster = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.btn_Connect);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 248);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "連接";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(320, 49);
            this.button2.TabIndex = 6;
            this.button2.Text = "(2)     開啟自動讀取";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(320, 49);
            this.button1.TabIndex = 5;
            this.button1.Text = "讀取一次";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.txt_SensorType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtDeviceName, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(323, 52);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // txt_SensorType
            // 
            this.txt_SensorType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_SensorType.Location = new System.Drawing.Point(164, 3);
            this.txt_SensorType.Name = "txt_SensorType";
            this.txt_SensorType.Size = new System.Drawing.Size(156, 22);
            this.txt_SensorType.TabIndex = 4;
            this.txt_SensorType.Text = "IFC2412";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sensor Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDeviceName
            // 
            this.txtDeviceName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeviceName.Location = new System.Drawing.Point(164, 29);
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.Size = new System.Drawing.Size(156, 22);
            this.txtDeviceName.TabIndex = 0;
            this.txtDeviceName.Text = "169.254.168.150";
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(7, 78);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(320, 49);
            this.btn_Connect.TabIndex = 1;
            this.btn_Connect.Text = "(1)      連線";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // txt_Msg
            // 
            this.txt_Msg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Msg.Location = new System.Drawing.Point(3, 18);
            this.txt_Msg.Multiline = true;
            this.txt_Msg.Name = "txt_Msg";
            this.txt_Msg.Size = new System.Drawing.Size(330, 277);
            this.txt_Msg.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_Msg);
            this.groupBox2.Location = new System.Drawing.Point(12, 266);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 298);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "訊息";
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Lbl_index0_Scale
            // 
            this.Lbl_index0_Scale.BackColor = System.Drawing.SystemColors.Window;
            this.Lbl_index0_Scale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_index0_Scale.ForeColor = System.Drawing.Color.Red;
            this.Lbl_index0_Scale.Location = new System.Drawing.Point(605, 166);
            this.Lbl_index0_Scale.Name = "Lbl_index0_Scale";
            this.Lbl_index0_Scale.Size = new System.Drawing.Size(123, 21);
            this.Lbl_index0_Scale.TabIndex = 54;
            this.Lbl_index0_Scale.Text = "0";
            this.Lbl_index0_Scale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_index0_Raw
            // 
            this.Lbl_index0_Raw.BackColor = System.Drawing.SystemColors.Window;
            this.Lbl_index0_Raw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_index0_Raw.ForeColor = System.Drawing.Color.Red;
            this.Lbl_index0_Raw.Location = new System.Drawing.Point(482, 166);
            this.Lbl_index0_Raw.Name = "Lbl_index0_Raw";
            this.Lbl_index0_Raw.Size = new System.Drawing.Size(123, 21);
            this.Lbl_index0_Raw.TabIndex = 53;
            this.Lbl_index0_Raw.Text = "0";
            this.Lbl_index0_Raw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Silver;
            this.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label6.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label6.Location = new System.Drawing.Point(359, 145);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(123, 21);
            this.Label6.TabIndex = 52;
            this.Label6.Text = "Data contents";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Gray;
            this.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label5.ForeColor = System.Drawing.Color.White;
            this.Label5.Location = new System.Drawing.Point(359, 166);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(123, 21);
            this.Label5.TabIndex = 51;
            this.Label5.Text = "CH1_Dis1";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(605, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 21);
            this.label3.TabIndex = 50;
            this.label3.Text = "scaledData";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Silver;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(482, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 21);
            this.label4.TabIndex = 49;
            this.label4.Text = "RawData";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(355, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 24);
            this.label9.TabIndex = 57;
            this.label9.Text = "裝置資訊";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Yellow;
            this.textBox1.ForeColor = System.Drawing.Color.Blue;
            this.textBox1.Location = new System.Drawing.Point(359, 40);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(373, 99);
            this.textBox1.TabIndex = 58;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Window;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(728, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 21);
            this.label7.TabIndex = 62;
            this.label7.Text = "0";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Silver;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(728, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 21);
            this.label8.TabIndex = 63;
            this.label8.Text = "DataAvial";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_index1_Scale
            // 
            this.Lbl_index1_Scale.BackColor = System.Drawing.SystemColors.Window;
            this.Lbl_index1_Scale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_index1_Scale.ForeColor = System.Drawing.Color.Red;
            this.Lbl_index1_Scale.Location = new System.Drawing.Point(605, 187);
            this.Lbl_index1_Scale.Name = "Lbl_index1_Scale";
            this.Lbl_index1_Scale.Size = new System.Drawing.Size(123, 21);
            this.Lbl_index1_Scale.TabIndex = 66;
            this.Lbl_index1_Scale.Text = "0";
            this.Lbl_index1_Scale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_index1_Raw
            // 
            this.Lbl_index1_Raw.BackColor = System.Drawing.SystemColors.Window;
            this.Lbl_index1_Raw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_index1_Raw.ForeColor = System.Drawing.Color.Red;
            this.Lbl_index1_Raw.Location = new System.Drawing.Point(482, 187);
            this.Lbl_index1_Raw.Name = "Lbl_index1_Raw";
            this.Lbl_index1_Raw.Size = new System.Drawing.Size(123, 21);
            this.Lbl_index1_Raw.TabIndex = 65;
            this.Lbl_index1_Raw.Text = "0";
            this.Lbl_index1_Raw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Gray;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(359, 187);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(123, 21);
            this.label12.TabIndex = 64;
            this.label12.Text = "CH2__Dis1";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_index2_Scale
            // 
            this.Lbl_index2_Scale.BackColor = System.Drawing.SystemColors.Window;
            this.Lbl_index2_Scale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_index2_Scale.ForeColor = System.Drawing.Color.Red;
            this.Lbl_index2_Scale.Location = new System.Drawing.Point(605, 208);
            this.Lbl_index2_Scale.Name = "Lbl_index2_Scale";
            this.Lbl_index2_Scale.Size = new System.Drawing.Size(123, 21);
            this.Lbl_index2_Scale.TabIndex = 69;
            this.Lbl_index2_Scale.Text = "0";
            this.Lbl_index2_Scale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lbl_index2_Raw
            // 
            this.Lbl_index2_Raw.BackColor = System.Drawing.SystemColors.Window;
            this.Lbl_index2_Raw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lbl_index2_Raw.ForeColor = System.Drawing.Color.Red;
            this.Lbl_index2_Raw.Location = new System.Drawing.Point(482, 208);
            this.Lbl_index2_Raw.Name = "Lbl_index2_Raw";
            this.Lbl_index2_Raw.Size = new System.Drawing.Size(123, 21);
            this.Lbl_index2_Raw.TabIndex = 68;
            this.Lbl_index2_Raw.Text = "0";
            this.Lbl_index2_Raw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Gray;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(359, 208);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 21);
            this.label13.TabIndex = 67;
            this.label13.Text = "Thickness";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_CreateMaster
            // 
            this.btn_CreateMaster.Location = new System.Drawing.Point(359, 266);
            this.btn_CreateMaster.Name = "btn_CreateMaster";
            this.btn_CreateMaster.Size = new System.Drawing.Size(123, 46);
            this.btn_CreateMaster.TabIndex = 70;
            this.btn_CreateMaster.Text = "Create Master";
            this.btn_CreateMaster.UseVisualStyleBackColor = true;
            this.btn_CreateMaster.Click += new System.EventHandler(this.btn_CreateMaster_Click);
            // 
            // btn_Master
            // 
            this.btn_Master.Location = new System.Drawing.Point(488, 266);
            this.btn_Master.Name = "btn_Master";
            this.btn_Master.Size = new System.Drawing.Size(123, 46);
            this.btn_Master.TabIndex = 70;
            this.btn_Master.Text = "Master";
            this.btn_Master.UseVisualStyleBackColor = true;
            this.btn_Master.Click += new System.EventHandler(this.btn_Master_Click);
            // 
            // Btn_ResetMaster
            // 
            this.Btn_ResetMaster.Location = new System.Drawing.Point(617, 266);
            this.Btn_ResetMaster.Name = "Btn_ResetMaster";
            this.Btn_ResetMaster.Size = new System.Drawing.Size(123, 46);
            this.Btn_ResetMaster.TabIndex = 71;
            this.Btn_ResetMaster.Text = "Reset Master";
            this.Btn_ResetMaster.UseVisualStyleBackColor = true;
            this.Btn_ResetMaster.Click += new System.EventHandler(this.Btn_ResetMaster_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 576);
            this.Controls.Add(this.Btn_ResetMaster);
            this.Controls.Add(this.btn_Master);
            this.Controls.Add(this.btn_CreateMaster);
            this.Controls.Add(this.Lbl_index2_Scale);
            this.Controls.Add(this.Lbl_index2_Raw);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Lbl_index1_Scale);
            this.Controls.Add(this.Lbl_index1_Raw);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Lbl_index0_Scale);
            this.Controls.Add(this.Lbl_index0_Raw);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "IFC2412 ETH thick Example";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TextBox txtDeviceName;
        private System.Windows.Forms.TextBox txt_Msg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txt_SensorType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.Label Lbl_index0_Scale;
        internal System.Windows.Forms.Label Lbl_index0_Raw;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label Lbl_index1_Scale;
        internal System.Windows.Forms.Label Lbl_index1_Raw;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label Lbl_index2_Scale;
        internal System.Windows.Forms.Label Lbl_index2_Raw;
        internal System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn_CreateMaster;
        private System.Windows.Forms.Button btn_Master;
        private System.Windows.Forms.Button Btn_ResetMaster;
    }
}

