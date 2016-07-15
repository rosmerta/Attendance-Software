namespace Attendance_Master
{
    partial class PunchMaster
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.FirstFinger = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEmployeeName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.digitalDisplayControl1 = new Owf.Controls.DigitalDisplayControl();
            this.lblEmployeeId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FirstFinger)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(412, 19);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(327, 252);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(327, 252);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(327, 252);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FirstFinger
            // 
            this.FirstFinger.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FirstFinger.Location = new System.Drawing.Point(412, 19);
            this.FirstFinger.Name = "FirstFinger";
            this.FirstFinger.Size = new System.Drawing.Size(210, 199);
            this.FirstFinger.TabIndex = 1;
            this.FirstFinger.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblEmployeeId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblEmployeeName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.LblName);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1028, 389);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Personal Information";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(17, 340);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(179, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "Employee ID";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEmployeeName
            // 
            this.lblEmployeeName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblEmployeeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeName.ForeColor = System.Drawing.Color.White;
            this.lblEmployeeName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEmployeeName.Location = new System.Drawing.Point(411, 287);
            this.lblEmployeeName.Name = "lblEmployeeName";
            this.lblEmployeeName.Size = new System.Drawing.Size(328, 29);
            this.lblEmployeeName.TabIndex = 4;
            this.lblEmployeeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEmployeeName.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Employee Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblName
            // 
            this.LblName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.LblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblName.ForeColor = System.Drawing.Color.White;
            this.LblName.Location = new System.Drawing.Point(17, 30);
            this.LblName.Name = "LblName";
            this.LblName.Size = new System.Drawing.Size(179, 29);
            this.LblName.TabIndex = 2;
            this.LblName.Text = "Image";
            this.LblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.FirstFinger);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 482);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1028, 237);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Action";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(20, 31);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(179, 29);
            this.label2.TabIndex = 6;
            this.label2.Text = "Press Your finger";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblStatus);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.ForestGreen;
            this.groupBox3.Location = new System.Drawing.Point(12, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(728, 86);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(58, 13);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(664, 70);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // digitalDisplayControl1
            // 
            this.digitalDisplayControl1.BackColor = System.Drawing.Color.Transparent;
            this.digitalDisplayControl1.DigitColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.digitalDisplayControl1.DigitText = "00:00:00";
            this.digitalDisplayControl1.ForeColor = System.Drawing.Color.White;
            this.digitalDisplayControl1.Location = new System.Drawing.Point(783, 11);
            this.digitalDisplayControl1.Name = "digitalDisplayControl1";
            this.digitalDisplayControl1.Size = new System.Drawing.Size(257, 70);
            this.digitalDisplayControl1.TabIndex = 7;
            // 
            // lblEmployeeId
            // 
            this.lblEmployeeId.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblEmployeeId.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeId.ForeColor = System.Drawing.Color.White;
            this.lblEmployeeId.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEmployeeId.Location = new System.Drawing.Point(411, 340);
            this.lblEmployeeId.Name = "lblEmployeeId";
            this.lblEmployeeId.Size = new System.Drawing.Size(328, 29);
            this.lblEmployeeId.TabIndex = 6;
            this.lblEmployeeId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEmployeeId.Visible = false;
            // 
            // PunchMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 731);
            this.Controls.Add(this.digitalDisplayControl1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PunchMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Punch Master";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PunchMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FirstFinger)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox FirstFinger;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblEmployeeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timer1;
        private Owf.Controls.DigitalDisplayControl digitalDisplayControl1;
        private System.Windows.Forms.Label lblEmployeeId;
    }
}