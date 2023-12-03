namespace JasonQuery
{
    sealed partial class frmConnectExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnectExport));
            this.txtEncryptPassword = new C1.Win.C1Input.C1TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnBrowseFile = new C1.Win.C1Input.C1Button();
            this.txtFilename = new C1.Win.C1Input.C1TextBox();
            this.lblFilename = new System.Windows.Forms.Label();
            this.chkIncludeDBPassword = new C1.Win.C1Input.C1CheckBox();
            this.grp = new System.Windows.Forms.GroupBox();
            this.btnHelp_Password = new C1.Win.C1Input.C1Button();
            this.chkEncrypt = new C1.Win.C1Input.C1CheckBox();
            this.lblRemember = new System.Windows.Forms.Label();
            this.lblCaution = new System.Windows.Forms.Label();
            this.btnExport = new C1.Win.C1Input.C1Button();
            this.btnClose = new C1.Win.C1Input.C1Button();
            this.grpExportTo = new System.Windows.Forms.GroupBox();
            this.btnEncryptPasswordView = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtEncryptPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBrowseFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilename)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeDBPassword)).BeginInit();
            this.grp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_Password)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEncrypt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.grpExportTo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEncryptPasswordView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtEncryptPassword
            // 
            this.txtEncryptPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.txtEncryptPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEncryptPassword.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtEncryptPassword.Location = new System.Drawing.Point(82, 26);
            this.txtEncryptPassword.MaxLength = 25;
            this.txtEncryptPassword.Name = "txtEncryptPassword";
            this.txtEncryptPassword.PasswordChar = '*';
            this.txtEncryptPassword.ShortcutsEnabled = false;
            this.txtEncryptPassword.ShowContextMenu = false;
            this.txtEncryptPassword.Size = new System.Drawing.Size(180, 21);
            this.txtEncryptPassword.TabIndex = 88;
            this.txtEncryptPassword.Tag = null;
            this.txtEncryptPassword.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtEncryptPassword.TextChanged += new System.EventHandler(this.txtEncryptPassword_TextChanged);
            this.txtEncryptPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEncryptPassword_KeyDown);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPassword.Location = new System.Drawing.Point(16, 28);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(64, 16);
            this.lblPassword.TabIndex = 87;
            this.lblPassword.Text = "Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(540, 25);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(21, 21);
            this.btnBrowseFile.TabIndex = 111;
            this.btnBrowseFile.Text = "...";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.txtFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilename.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtFilename.Location = new System.Drawing.Point(145, 25);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = true;
            this.txtFilename.Size = new System.Drawing.Size(396, 21);
            this.txtFilename.TabIndex = 109;
            this.txtFilename.Tag = null;
            this.txtFilename.TextDetached = true;
            this.txtFilename.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblFilename.Location = new System.Drawing.Point(12, 27);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(68, 16);
            this.lblFilename.TabIndex = 110;
            this.lblFilename.Text = "File Name:";
            this.lblFilename.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkIncludeDBPassword
            // 
            this.chkIncludeDBPassword.AutoSize = true;
            this.chkIncludeDBPassword.BackColor = System.Drawing.SystemColors.Control;
            this.chkIncludeDBPassword.BorderColor = System.Drawing.Color.Transparent;
            this.chkIncludeDBPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkIncludeDBPassword.ForeColor = System.Drawing.Color.Black;
            this.chkIncludeDBPassword.Location = new System.Drawing.Point(239, -1);
            this.chkIncludeDBPassword.Name = "chkIncludeDBPassword";
            this.chkIncludeDBPassword.Padding = new System.Windows.Forms.Padding(1);
            this.chkIncludeDBPassword.Size = new System.Drawing.Size(194, 22);
            this.chkIncludeDBPassword.TabIndex = 112;
            this.chkIncludeDBPassword.Text = "Include Connection Password";
            this.chkIncludeDBPassword.UseVisualStyleBackColor = false;
            this.chkIncludeDBPassword.Value = null;
            this.chkIncludeDBPassword.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            // 
            // grp
            // 
            this.grp.BackColor = System.Drawing.SystemColors.Control;
            this.grp.Controls.Add(this.btnEncryptPasswordView);
            this.grp.Controls.Add(this.btnHelp_Password);
            this.grp.Controls.Add(this.chkEncrypt);
            this.grp.Controls.Add(this.lblRemember);
            this.grp.Controls.Add(this.lblCaution);
            this.grp.Controls.Add(this.chkIncludeDBPassword);
            this.grp.Controls.Add(this.txtEncryptPassword);
            this.grp.Controls.Add(this.lblPassword);
            this.grp.Location = new System.Drawing.Point(14, 57);
            this.grp.Name = "grp";
            this.grp.Size = new System.Drawing.Size(556, 100);
            this.grp.TabIndex = 114;
            this.grp.TabStop = false;
            // 
            // btnHelp_Password
            // 
            this.btnHelp_Password.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp_Password.Image")));
            this.btnHelp_Password.Location = new System.Drawing.Point(282, 26);
            this.btnHelp_Password.Name = "btnHelp_Password";
            this.btnHelp_Password.Size = new System.Drawing.Size(21, 21);
            this.btnHelp_Password.TabIndex = 118;
            this.btnHelp_Password.Tag = "Available characters for password";
            this.btnHelp_Password.UseVisualStyleBackColor = true;
            this.btnHelp_Password.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnHelp_Password.Click += new System.EventHandler(this.btnHelp_Password_Click);
            // 
            // chkEncrypt
            // 
            this.chkEncrypt.AutoSize = true;
            this.chkEncrypt.BackColor = System.Drawing.SystemColors.Control;
            this.chkEncrypt.BorderColor = System.Drawing.Color.Transparent;
            this.chkEncrypt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkEncrypt.Checked = true;
            this.chkEncrypt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEncrypt.ForeColor = System.Drawing.Color.Black;
            this.chkEncrypt.Location = new System.Drawing.Point(11, -1);
            this.chkEncrypt.Name = "chkEncrypt";
            this.chkEncrypt.Padding = new System.Windows.Forms.Padding(1);
            this.chkEncrypt.Size = new System.Drawing.Size(200, 22);
            this.chkEncrypt.TabIndex = 117;
            this.chkEncrypt.Text = "Encrypt the contents of this file";
            this.chkEncrypt.UseVisualStyleBackColor = false;
            this.chkEncrypt.Value = true;
            this.chkEncrypt.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkEncrypt.CheckedChanged += new System.EventHandler(this.chkEncrypt_CheckedChanged);
            // 
            // lblRemember
            // 
            this.lblRemember.AutoSize = true;
            this.lblRemember.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblRemember.Location = new System.Drawing.Point(16, 74);
            this.lblRemember.Name = "lblRemember";
            this.lblRemember.Size = new System.Drawing.Size(270, 16);
            this.lblRemember.TabIndex = 90;
            this.lblRemember.Text = "(Remember that passwords are case-sensitive.)";
            this.lblRemember.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCaution
            // 
            this.lblCaution.AutoSize = true;
            this.lblCaution.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblCaution.Location = new System.Drawing.Point(16, 53);
            this.lblCaution.Name = "lblCaution";
            this.lblCaution.Size = new System.Drawing.Size(382, 16);
            this.lblCaution.TabIndex = 89;
            this.lblCaution.Text = "Caution: If you lose or forget the password, it cannot be recovered.";
            this.lblCaution.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExport.Location = new System.Drawing.Point(613, 21);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(73, 54);
            this.btnExport.TabIndex = 115;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.btnExport.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClose.Location = new System.Drawing.Point(613, 94);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 54);
            this.btnClose.TabIndex = 116;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.btnClose.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpExportTo
            // 
            this.grpExportTo.Controls.Add(this.lblFilename);
            this.grpExportTo.Controls.Add(this.txtFilename);
            this.grpExportTo.Controls.Add(this.btnBrowseFile);
            this.grpExportTo.Controls.Add(this.grp);
            this.grpExportTo.Location = new System.Drawing.Point(12, 12);
            this.grpExportTo.Name = "grpExportTo";
            this.grpExportTo.Size = new System.Drawing.Size(586, 170);
            this.grpExportTo.TabIndex = 117;
            this.grpExportTo.TabStop = false;
            this.grpExportTo.Text = "Export To";
            // 
            // btnEncryptPasswordView
            // 
            this.btnEncryptPasswordView.Image = ((System.Drawing.Image)(resources.GetObject("btnEncryptPasswordView.Image")));
            this.btnEncryptPasswordView.Location = new System.Drawing.Point(255, 26);
            this.btnEncryptPasswordView.Name = "btnEncryptPasswordView";
            this.btnEncryptPasswordView.Size = new System.Drawing.Size(21, 21);
            this.btnEncryptPasswordView.TabIndex = 141;
            this.btnEncryptPasswordView.UseVisualStyleBackColor = true;
            this.btnEncryptPasswordView.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnEncryptPasswordView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnEncryptPasswordView_MouseDown);
            this.btnEncryptPasswordView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEncryptPasswordView_MouseUp);
            // 
            // frmConnectExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 196);
            this.Controls.Add(this.grpExportTo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExport);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConnectExport";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Export Connection Information";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtEncryptPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBrowseFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilename)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeDBPassword)).EndInit();
            this.grp.ResumeLayout(false);
            this.grp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_Password)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEncrypt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.grpExportTo.ResumeLayout(false);
            this.grpExportTo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEncryptPasswordView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1Input.C1TextBox txtEncryptPassword;
        private System.Windows.Forms.Label lblPassword;
        private C1.Win.C1Input.C1Button btnBrowseFile;
        private C1.Win.C1Input.C1TextBox txtFilename;
        private System.Windows.Forms.Label lblFilename;
        private C1.Win.C1Input.C1CheckBox chkIncludeDBPassword;
        private System.Windows.Forms.GroupBox grp;
        private System.Windows.Forms.Label lblRemember;
        private System.Windows.Forms.Label lblCaution;
        private C1.Win.C1Input.C1Button btnExport;
        private C1.Win.C1Input.C1Button btnClose;
        private C1.Win.C1Input.C1CheckBox chkEncrypt;
        private C1.Win.C1Input.C1Button btnHelp_Password;
        private System.Windows.Forms.GroupBox grpExportTo;
        private C1.Win.C1Input.C1Button btnEncryptPasswordView;
    }
}