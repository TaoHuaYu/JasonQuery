namespace JasonQuery
{
    sealed partial class frmCheckForUpdates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckForUpdates));
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblInfo2 = new System.Windows.Forms.Label();
            this.lnkCheck1 = new System.Windows.Forms.LinkLabel();
            this.lnkDownloadWithoutSetting1 = new System.Windows.Forms.LinkLabel();
            this.grpDownloadInfo = new System.Windows.Forms.GroupBox();
            this.lblLength = new System.Windows.Forms.Label();
            this.btnHelp_HowToUpdate = new C1.Win.C1Input.C1Button();
            this.btnHelp_000WebHost = new C1.Win.C1Input.C1Button();
            this.lblRecommended = new System.Windows.Forms.Label();
            this.lblWithout2 = new System.Windows.Forms.Label();
            this.lblWithout1 = new System.Windows.Forms.Label();
            this.lnkDownloadWithoutSetting2 = new System.Windows.Forms.LinkLabel();
            this.lblDownloadInfoWithout = new System.Windows.Forms.Label();
            this.grpCheckInfo = new System.Windows.Forms.GroupBox();
            this.btnClose = new C1.Win.C1Input.C1Button();
            this.btnCheckForUpdates = new C1.Win.C1Input.C1Button();
            this.grpUpdateNow = new System.Windows.Forms.GroupBox();
            this.btnUpdateNow = new C1.Win.C1Input.C1Button();
            this.lblUpdateNow = new System.Windows.Forms.Label();
            this.grpDownloadInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_HowToUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_000WebHost)).BeginInit();
            this.grpCheckInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheckForUpdates)).BeginInit();
            this.grpUpdateNow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdateNow)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblInfo.Location = new System.Drawing.Point(54, 21);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(251, 16);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "A new version of JasonQuery is available:";
            this.lblInfo.Visible = false;
            // 
            // lblInfo2
            // 
            this.lblInfo2.AutoSize = true;
            this.lblInfo2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblInfo2.Location = new System.Drawing.Point(54, 43);
            this.lblInfo2.Name = "lblInfo2";
            this.lblInfo2.Size = new System.Drawing.Size(32, 16);
            this.lblInfo2.TabIndex = 2;
            this.lblInfo2.Text = "0.01";
            this.lblInfo2.Visible = false;
            // 
            // lnkCheck1
            // 
            this.lnkCheck1.AutoSize = true;
            this.lnkCheck1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lnkCheck1.Location = new System.Drawing.Point(23, 29);
            this.lnkCheck1.Name = "lnkCheck1";
            this.lnkCheck1.Size = new System.Drawing.Size(267, 16);
            this.lnkCheck1.TabIndex = 3;
            this.lnkCheck1.TabStop = true;
            this.lnkCheck1.Text = "https://jasonquery.000webhostapp.com/jq.txt";
            this.lnkCheck1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCheck1_LinkClicked);
            // 
            // lnkDownloadWithoutSetting1
            // 
            this.lnkDownloadWithoutSetting1.AutoSize = true;
            this.lnkDownloadWithoutSetting1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lnkDownloadWithoutSetting1.Location = new System.Drawing.Point(151, 76);
            this.lnkDownloadWithoutSetting1.Name = "lnkDownloadWithoutSetting1";
            this.lnkDownloadWithoutSetting1.Size = new System.Drawing.Size(108, 16);
            this.lnkDownloadWithoutSetting1.TabIndex = 5;
            this.lnkDownloadWithoutSetting1.TabStop = true;
            this.lnkDownloadWithoutSetting1.Tag = "";
            this.lnkDownloadWithoutSetting1.Text = "JasonQuery86.zip";
            this.lnkDownloadWithoutSetting1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownloadWithoutSetting_LinkClicked);
            // 
            // grpDownloadInfo
            // 
            this.grpDownloadInfo.Controls.Add(this.lblLength);
            this.grpDownloadInfo.Controls.Add(this.btnHelp_HowToUpdate);
            this.grpDownloadInfo.Controls.Add(this.btnHelp_000WebHost);
            this.grpDownloadInfo.Controls.Add(this.lblRecommended);
            this.grpDownloadInfo.Controls.Add(this.lblWithout2);
            this.grpDownloadInfo.Controls.Add(this.lblWithout1);
            this.grpDownloadInfo.Controls.Add(this.lnkDownloadWithoutSetting2);
            this.grpDownloadInfo.Controls.Add(this.lblDownloadInfoWithout);
            this.grpDownloadInfo.Controls.Add(this.lnkDownloadWithoutSetting1);
            this.grpDownloadInfo.Location = new System.Drawing.Point(15, 158);
            this.grpDownloadInfo.Name = "grpDownloadInfo";
            this.grpDownloadInfo.Size = new System.Drawing.Size(430, 107);
            this.grpDownloadInfo.TabIndex = 11;
            this.grpDownloadInfo.TabStop = false;
            this.grpDownloadInfo.Text = "Download latest version manually";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(406, 19);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(0, 16);
            this.lblLength.TabIndex = 86;
            this.lblLength.Visible = false;
            // 
            // btnHelp_HowToUpdate
            // 
            this.btnHelp_HowToUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp_HowToUpdate.Image")));
            this.btnHelp_HowToUpdate.Location = new System.Drawing.Point(266, 0);
            this.btnHelp_HowToUpdate.Name = "btnHelp_HowToUpdate";
            this.btnHelp_HowToUpdate.Size = new System.Drawing.Size(21, 19);
            this.btnHelp_HowToUpdate.TabIndex = 6;
            this.btnHelp_HowToUpdate.UseVisualStyleBackColor = true;
            this.btnHelp_HowToUpdate.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.btnHelp_HowToUpdate.Click += new System.EventHandler(this.btnHelp_HowToUpdate_Click);
            // 
            // btnHelp_000WebHost
            // 
            this.btnHelp_000WebHost.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp_000WebHost.Image")));
            this.btnHelp_000WebHost.Location = new System.Drawing.Point(349, 51);
            this.btnHelp_000WebHost.Name = "btnHelp_000WebHost";
            this.btnHelp_000WebHost.Size = new System.Drawing.Size(21, 21);
            this.btnHelp_000WebHost.TabIndex = 85;
            this.btnHelp_000WebHost.UseVisualStyleBackColor = true;
            this.btnHelp_000WebHost.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnHelp_000WebHost.Click += new System.EventHandler(this.btnHelp_000WebHost_Click);
            // 
            // lblRecommended
            // 
            this.lblRecommended.AutoSize = true;
            this.lblRecommended.Location = new System.Drawing.Point(247, 53);
            this.lblRecommended.Name = "lblRecommended";
            this.lblRecommended.Size = new System.Drawing.Size(100, 16);
            this.lblRecommended.TabIndex = 75;
            this.lblRecommended.Text = "(recommended)";
            // 
            // lblWithout2
            // 
            this.lblWithout2.AutoSize = true;
            this.lblWithout2.Location = new System.Drawing.Point(23, 53);
            this.lblWithout2.Name = "lblWithout2";
            this.lblWithout2.Size = new System.Drawing.Size(85, 16);
            this.lblWithout2.TabIndex = 14;
            this.lblWithout2.Text = "000WebHost:";
            // 
            // lblWithout1
            // 
            this.lblWithout1.AutoSize = true;
            this.lblWithout1.Location = new System.Drawing.Point(23, 76);
            this.lblWithout1.Name = "lblWithout1";
            this.lblWithout1.Size = new System.Drawing.Size(85, 16);
            this.lblWithout1.TabIndex = 13;
            this.lblWithout1.Text = "000WebHost:";
            // 
            // lnkDownloadWithoutSetting2
            // 
            this.lnkDownloadWithoutSetting2.AutoSize = true;
            this.lnkDownloadWithoutSetting2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lnkDownloadWithoutSetting2.Location = new System.Drawing.Point(151, 53);
            this.lnkDownloadWithoutSetting2.Name = "lnkDownloadWithoutSetting2";
            this.lnkDownloadWithoutSetting2.Size = new System.Drawing.Size(108, 16);
            this.lnkDownloadWithoutSetting2.TabIndex = 4;
            this.lnkDownloadWithoutSetting2.TabStop = true;
            this.lnkDownloadWithoutSetting2.Tag = "JasonQuery64.zip";
            this.lnkDownloadWithoutSetting2.Text = "JasonQuery64.zip";
            this.lnkDownloadWithoutSetting2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDownload_LinkClicked);
            // 
            // lblDownloadInfoWithout
            // 
            this.lblDownloadInfoWithout.AutoSize = true;
            this.lblDownloadInfoWithout.Location = new System.Drawing.Point(23, 29);
            this.lblDownloadInfoWithout.Name = "lblDownloadInfoWithout";
            this.lblDownloadInfoWithout.Size = new System.Drawing.Size(200, 16);
            this.lblDownloadInfoWithout.TabIndex = 11;
            this.lblDownloadInfoWithout.Text = "7z package: portable, multilingual";
            // 
            // grpCheckInfo
            // 
            this.grpCheckInfo.Controls.Add(this.lnkCheck1);
            this.grpCheckInfo.Location = new System.Drawing.Point(15, 88);
            this.grpCheckInfo.Name = "grpCheckInfo";
            this.grpCheckInfo.Size = new System.Drawing.Size(430, 63);
            this.grpCheckInfo.TabIndex = 12;
            this.grpCheckInfo.TabStop = false;
            this.grpCheckInfo.Text = "Check for updates manually";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(379, 384);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 29);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnClose.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCheckForUpdates
            // 
            this.btnCheckForUpdates.Location = new System.Drawing.Point(124, 32);
            this.btnCheckForUpdates.Name = "btnCheckForUpdates";
            this.btnCheckForUpdates.Size = new System.Drawing.Size(209, 29);
            this.btnCheckForUpdates.TabIndex = 1;
            this.btnCheckForUpdates.Text = "Check for updates now";
            this.btnCheckForUpdates.UseVisualStyleBackColor = true;
            this.btnCheckForUpdates.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnCheckForUpdates.Click += new System.EventHandler(this.btnCheckForUpdates_Click);
            // 
            // grpUpdateNow
            // 
            this.grpUpdateNow.Controls.Add(this.btnUpdateNow);
            this.grpUpdateNow.Controls.Add(this.lblUpdateNow);
            this.grpUpdateNow.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpUpdateNow.ForeColor = System.Drawing.Color.Black;
            this.grpUpdateNow.Location = new System.Drawing.Point(15, 273);
            this.grpUpdateNow.Name = "grpUpdateNow";
            this.grpUpdateNow.Size = new System.Drawing.Size(430, 100);
            this.grpUpdateNow.TabIndex = 13;
            this.grpUpdateNow.TabStop = false;
            this.grpUpdateNow.Text = "Automatically download and update";
            // 
            // btnUpdateNow
            // 
            this.btnUpdateNow.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnUpdateNow.Location = new System.Drawing.Point(26, 28);
            this.btnUpdateNow.Name = "btnUpdateNow";
            this.btnUpdateNow.Size = new System.Drawing.Size(241, 29);
            this.btnUpdateNow.TabIndex = 15;
            this.btnUpdateNow.Text = "Update Now (One-Click update)";
            this.btnUpdateNow.UseVisualStyleBackColor = true;
            this.btnUpdateNow.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
            this.btnUpdateNow.Click += new System.EventHandler(this.btnUpdateNow_Click);
            // 
            // lblUpdateNow
            // 
            this.lblUpdateNow.AutoSize = true;
            this.lblUpdateNow.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblUpdateNow.ForeColor = System.Drawing.Color.Black;
            this.lblUpdateNow.Location = new System.Drawing.Point(23, 66);
            this.lblUpdateNow.Name = "lblUpdateNow";
            this.lblUpdateNow.Size = new System.Drawing.Size(313, 16);
            this.lblUpdateNow.TabIndex = 11;
            this.lblUpdateNow.Text = "Call Updater.exe to download and update lastest files.";
            // 
            // frmCheckForUpdates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(459, 425);
            this.Controls.Add(this.grpUpdateNow);
            this.Controls.Add(this.btnCheckForUpdates);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpCheckInfo);
            this.Controls.Add(this.grpDownloadInfo);
            this.Controls.Add(this.lblInfo2);
            this.Controls.Add(this.lblInfo);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCheckForUpdates";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check for updates";
            this.Load += new System.EventHandler(this.Form_Load);
            this.grpDownloadInfo.ResumeLayout(false);
            this.grpDownloadInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_HowToUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_000WebHost)).EndInit();
            this.grpCheckInfo.ResumeLayout(false);
            this.grpCheckInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheckForUpdates)).EndInit();
            this.grpUpdateNow.ResumeLayout(false);
            this.grpUpdateNow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdateNow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblInfo2;
        private System.Windows.Forms.LinkLabel lnkCheck1;
        private System.Windows.Forms.LinkLabel lnkDownloadWithoutSetting1;
        private System.Windows.Forms.GroupBox grpDownloadInfo;
        private System.Windows.Forms.Label lblDownloadInfoWithout;
        private System.Windows.Forms.GroupBox grpCheckInfo;
        private System.Windows.Forms.LinkLabel lnkDownloadWithoutSetting2;
        private System.Windows.Forms.Label lblWithout2;
        private System.Windows.Forms.Label lblWithout1;
        private C1.Win.C1Input.C1Button btnHelp_HowToUpdate;
        private System.Windows.Forms.Label lblRecommended;
        private C1.Win.C1Input.C1Button btnClose;
        private C1.Win.C1Input.C1Button btnCheckForUpdates;
        private C1.Win.C1Input.C1Button btnHelp_000WebHost;
        private System.Windows.Forms.GroupBox grpUpdateNow;
        private System.Windows.Forms.Label lblUpdateNow;
        private C1.Win.C1Input.C1Button btnUpdateNow;
        private System.Windows.Forms.Label lblLength;
    }
}