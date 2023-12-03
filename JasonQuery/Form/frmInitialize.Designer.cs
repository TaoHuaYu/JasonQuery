namespace JasonQuery
{
    partial class frmInitialize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInitialize));
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpLocalization = new System.Windows.Forms.GroupBox();
            this.cboLocalization = new C1.Win.C1Input.C1ComboBox();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.btnOK = new C1.Win.C1Input.C1Button();
            this.lblGuarantee = new System.Windows.Forms.Label();
            this.lblGuarantee1 = new System.Windows.Forms.Label();
            this.lblGuarantee3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblGuarantee2 = new System.Windows.Forms.Label();
            this.lblGuarantee21 = new System.Windows.Forms.Label();
            this.txtTemp = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblGuarantee22 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFeature11 = new System.Windows.Forms.Label();
            this.lblFeature4 = new System.Windows.Forms.Label();
            this.lblFeature10 = new System.Windows.Forms.Label();
            this.lblFeature9 = new System.Windows.Forms.Label();
            this.lblFeature8 = new System.Windows.Forms.Label();
            this.lblFeature7 = new System.Windows.Forms.Label();
            this.lblFeature6 = new System.Windows.Forms.Label();
            this.lblFeature5 = new System.Windows.Forms.Label();
            this.lblFeature3 = new System.Windows.Forms.Label();
            this.lblFeature2 = new System.Windows.Forms.Label();
            this.lblFeature1 = new System.Windows.Forms.Label();
            this.lblFeature = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.grpLocalization.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboLocalization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTitle1.Location = new System.Drawing.Point(54, 93);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(242, 16);
            this.lblTitle1.TabIndex = 1;
            this.lblTitle1.Text = "Thank you for your interest in JasonQuery.";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTitle.Location = new System.Drawing.Point(24, 66);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(157, 17);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Welcome to JasonQuery";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grpLocalization);
            this.groupBox1.Controls.Add(this.lblTitle2);
            this.groupBox1.Controls.Add(this.lblTitle);
            this.groupBox1.Controls.Add(this.lblTitle1);
            this.groupBox1.Location = new System.Drawing.Point(-14, -54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(764, 145);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            // 
            // grpLocalization
            // 
            this.grpLocalization.Controls.Add(this.cboLocalization);
            this.grpLocalization.Location = new System.Drawing.Point(407, 65);
            this.grpLocalization.Name = "grpLocalization";
            this.grpLocalization.Size = new System.Drawing.Size(260, 53);
            this.grpLocalization.TabIndex = 69;
            this.grpLocalization.TabStop = false;
            this.grpLocalization.Text = "Localization";
            // 
            // cboLocalization
            // 
            this.cboLocalization.AllowSpinLoop = false;
            this.cboLocalization.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboLocalization.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboLocalization.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboLocalization.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboLocalization.GapHeight = 0;
            this.cboLocalization.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboLocalization.Items.Add("yyyy/MM/dd");
            this.cboLocalization.Items.Add("yyyy-MM-dd");
            this.cboLocalization.Items.Add("MM/dd/yyyy");
            this.cboLocalization.Items.Add("MM-dd-yyyy");
            this.cboLocalization.Items.Add("dd/MM/yyyy");
            this.cboLocalization.Items.Add("dd-MM-yyyy");
            this.cboLocalization.ItemsDisplayMember = "";
            this.cboLocalization.ItemsValueMember = "";
            this.cboLocalization.Location = new System.Drawing.Point(29, 20);
            this.cboLocalization.Name = "cboLocalization";
            this.cboLocalization.Size = new System.Drawing.Size(216, 21);
            this.cboLocalization.TabIndex = 79;
            this.cboLocalization.Tag = null;
            this.cboLocalization.TextDetached = true;
            this.cboLocalization.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboLocalization.SelectedIndexChanged += new System.EventHandler(this.cboLocalization_SelectedIndexChanged);
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTitle2.Location = new System.Drawing.Point(54, 115);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(309, 16);
            this.lblTitle2.TabIndex = 2;
            this.lblTitle2.Text = "This program is free for commercial and personal use.";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(567, 613);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(83, 35);
            this.btnOK.TabIndex = 59;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblGuarantee
            // 
            this.lblGuarantee.AutoSize = true;
            this.lblGuarantee.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblGuarantee.Location = new System.Drawing.Point(18, 103);
            this.lblGuarantee.Name = "lblGuarantee";
            this.lblGuarantee.Size = new System.Drawing.Size(466, 16);
            this.lblGuarantee.TabIndex = 61;
            this.lblGuarantee.Text = "The following three guarantees, you can use JasonQuery with peace of mind: ";
            // 
            // lblGuarantee1
            // 
            this.lblGuarantee1.AutoSize = true;
            this.lblGuarantee1.Location = new System.Drawing.Point(31, 130);
            this.lblGuarantee1.Name = "lblGuarantee1";
            this.lblGuarantee1.Size = new System.Drawing.Size(317, 16);
            this.lblGuarantee1.TabIndex = 62;
            this.lblGuarantee1.Text = "1. JasonQuery will not read from or write to the registry.";
            // 
            // lblGuarantee3
            // 
            this.lblGuarantee3.AutoSize = true;
            this.lblGuarantee3.Location = new System.Drawing.Point(31, 224);
            this.lblGuarantee3.Name = "lblGuarantee3";
            this.lblGuarantee3.Size = new System.Drawing.Size(316, 16);
            this.lblGuarantee3.TabIndex = 63;
            this.lblGuarantee3.Text = "3. All settings or data will be stored in the following file:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 16);
            this.label6.TabIndex = 64;
            this.label6.Text = "JasonQuery.db";
            // 
            // lblGuarantee2
            // 
            this.lblGuarantee2.AutoSize = true;
            this.lblGuarantee2.Location = new System.Drawing.Point(31, 157);
            this.lblGuarantee2.Name = "lblGuarantee2";
            this.lblGuarantee2.Size = new System.Drawing.Size(517, 16);
            this.lblGuarantee2.TabIndex = 65;
            this.lblGuarantee2.Text = "2. JasonQuery will not modify your database unless you execute DDL/DML SQL statem" +
    "ents.";
            // 
            // lblGuarantee21
            // 
            this.lblGuarantee21.AutoSize = true;
            this.lblGuarantee21.Location = new System.Drawing.Point(44, 177);
            this.lblGuarantee21.Name = "lblGuarantee21";
            this.lblGuarantee21.Size = new System.Drawing.Size(278, 16);
            this.lblGuarantee21.TabIndex = 66;
            this.lblGuarantee21.Text = "2-1. such as Create/Alter/Insert/Update/Delete...";
            // 
            // txtTemp
            // 
            this.txtTemp.Location = new System.Drawing.Point(-57, 258);
            this.txtTemp.Name = "txtTemp";
            this.txtTemp.Size = new System.Drawing.Size(34, 23);
            this.txtTemp.TabIndex = 1;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblNote.Location = new System.Drawing.Point(12, 624);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(319, 16);
            this.lblNote.TabIndex = 67;
            this.lblNote.Text = "Now click \"OK\" to get started to work with you data.";
            // 
            // lblGuarantee22
            // 
            this.lblGuarantee22.AutoSize = true;
            this.lblGuarantee22.Location = new System.Drawing.Point(44, 197);
            this.lblGuarantee22.Name = "lblGuarantee22";
            this.lblGuarantee22.Size = new System.Drawing.Size(501, 16);
            this.lblGuarantee22.TabIndex = 68;
            this.lblGuarantee22.Text = "2-2. You need to press the \"Commit\" button or manually execute the Commit stateme" +
    "nt.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblFeature11);
            this.groupBox2.Controls.Add(this.lblFeature4);
            this.groupBox2.Controls.Add(this.lblFeature10);
            this.groupBox2.Controls.Add(this.lblFeature9);
            this.groupBox2.Controls.Add(this.lblFeature8);
            this.groupBox2.Controls.Add(this.lblFeature7);
            this.groupBox2.Controls.Add(this.lblFeature6);
            this.groupBox2.Controls.Add(this.lblFeature5);
            this.groupBox2.Controls.Add(this.lblFeature3);
            this.groupBox2.Controls.Add(this.lblFeature2);
            this.groupBox2.Controls.Add(this.lblFeature1);
            this.groupBox2.Controls.Add(this.lblFeature);
            this.groupBox2.Location = new System.Drawing.Point(-50, 264);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(782, 337);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            // 
            // lblFeature11
            // 
            this.lblFeature11.AutoSize = true;
            this.lblFeature11.Location = new System.Drawing.Point(75, 304);
            this.lblFeature11.Name = "lblFeature11";
            this.lblFeature11.Size = new System.Drawing.Size(304, 16);
            this.lblFeature11.TabIndex = 74;
            this.lblFeature11.Text = "11. Fixed bugs and added features from time to time";
            // 
            // lblFeature4
            // 
            this.lblFeature4.AutoSize = true;
            this.lblFeature4.Location = new System.Drawing.Point(81, 129);
            this.lblFeature4.Name = "lblFeature4";
            this.lblFeature4.Size = new System.Drawing.Size(502, 16);
            this.lblFeature4.TabIndex = 73;
            this.lblFeature4.Text = "4. Connect to Oracle directly through TCP/IP protocol without involving the Oracl" +
    "e Client";
            // 
            // lblFeature10
            // 
            this.lblFeature10.AutoSize = true;
            this.lblFeature10.Location = new System.Drawing.Point(75, 279);
            this.lblFeature10.Name = "lblFeature10";
            this.lblFeature10.Size = new System.Drawing.Size(376, 16);
            this.lblFeature10.TabIndex = 72;
            this.lblFeature10.Text = "10. Schema Browser, SQL History and many other useful features...";
            // 
            // lblFeature9
            // 
            this.lblFeature9.AutoSize = true;
            this.lblFeature9.Location = new System.Drawing.Point(81, 254);
            this.lblFeature9.Name = "lblFeature9";
            this.lblFeature9.Size = new System.Drawing.Size(490, 16);
            this.lblFeature9.TabIndex = 71;
            this.lblFeature9.Text = "9. Automatically direct to wrong location of SQL statement and display error mess" +
    "age ";
            // 
            // lblFeature8
            // 
            this.lblFeature8.AutoSize = true;
            this.lblFeature8.Location = new System.Drawing.Point(81, 229);
            this.lblFeature8.Name = "lblFeature8";
            this.lblFeature8.Size = new System.Drawing.Size(450, 16);
            this.lblFeature8.TabIndex = 70;
            this.lblFeature8.Text = "8. Automatically detect variables, such as \"WHERE id = :id AND name = :name\"";
            // 
            // lblFeature7
            // 
            this.lblFeature7.AutoSize = true;
            this.lblFeature7.Location = new System.Drawing.Point(81, 204);
            this.lblFeature7.Name = "lblFeature7";
            this.lblFeature7.Size = new System.Drawing.Size(357, 16);
            this.lblFeature7.TabIndex = 69;
            this.lblFeature7.Text = "7. Support Auto Replace: Keyword binding for favorite queries";
            // 
            // lblFeature6
            // 
            this.lblFeature6.AutoSize = true;
            this.lblFeature6.Location = new System.Drawing.Point(81, 179);
            this.lblFeature6.Name = "lblFeature6";
            this.lblFeature6.Size = new System.Drawing.Size(274, 16);
            this.lblFeature6.TabIndex = 68;
            this.lblFeature6.Text = "6. Support Paging Query: 500 records per page";
            // 
            // lblFeature5
            // 
            this.lblFeature5.AutoSize = true;
            this.lblFeature5.Location = new System.Drawing.Point(81, 154);
            this.lblFeature5.Name = "lblFeature5";
            this.lblFeature5.Size = new System.Drawing.Size(276, 16);
            this.lblFeature5.TabIndex = 67;
            this.lblFeature5.Text = "5. Advanced SQL editor with syntax highlighting";
            // 
            // lblFeature3
            // 
            this.lblFeature3.AutoSize = true;
            this.lblFeature3.Location = new System.Drawing.Point(81, 104);
            this.lblFeature3.Name = "lblFeature3";
            this.lblFeature3.Size = new System.Drawing.Size(344, 16);
            this.lblFeature3.TabIndex = 66;
            this.lblFeature3.Text = "3. Support Oracle/PostgreSQL/SQL Server/MySQL/MariaDB";
            // 
            // lblFeature2
            // 
            this.lblFeature2.AutoSize = true;
            this.lblFeature2.Location = new System.Drawing.Point(81, 79);
            this.lblFeature2.Name = "lblFeature2";
            this.lblFeature2.Size = new System.Drawing.Size(286, 16);
            this.lblFeature2.TabIndex = 65;
            this.lblFeature2.Text = "2. Support version checking and one-click update";
            // 
            // lblFeature1
            // 
            this.lblFeature1.AutoSize = true;
            this.lblFeature1.Location = new System.Drawing.Point(81, 54);
            this.lblFeature1.Name = "lblFeature1";
            this.lblFeature1.Size = new System.Drawing.Size(322, 16);
            this.lblFeature1.TabIndex = 64;
            this.lblFeature1.Text = "1. It is portable, multilingual, lightweight and easy to use";
            // 
            // lblFeature
            // 
            this.lblFeature.AutoSize = true;
            this.lblFeature.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblFeature.Location = new System.Drawing.Point(68, 24);
            this.lblFeature.Name = "lblFeature";
            this.lblFeature.Size = new System.Drawing.Size(62, 16);
            this.lblFeature.TabIndex = 63;
            this.lblFeature.Text = "Features:";
            // 
            // frmInitialize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 662);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblGuarantee22);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.txtTemp);
            this.Controls.Add(this.lblGuarantee21);
            this.Controls.Add(this.lblGuarantee2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblGuarantee3);
            this.Controls.Add(this.lblGuarantee1);
            this.Controls.Add(this.lblGuarantee);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInitialize";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Using JasonQuery for the first time ";
            this.Load += new System.EventHandler(this.Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpLocalization.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboLocalization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private C1.Win.C1Input.C1Button btnOK;
        private System.Windows.Forms.Label lblGuarantee;
        private System.Windows.Forms.Label lblGuarantee1;
        private System.Windows.Forms.Label lblGuarantee3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblGuarantee2;
        private System.Windows.Forms.Label lblGuarantee21;
        private System.Windows.Forms.TextBox txtTemp;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblGuarantee22;
        private System.Windows.Forms.GroupBox grpLocalization;
        private C1.Win.C1Input.C1ComboBox cboLocalization;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblFeature10;
        private System.Windows.Forms.Label lblFeature9;
        private System.Windows.Forms.Label lblFeature8;
        private System.Windows.Forms.Label lblFeature7;
        private System.Windows.Forms.Label lblFeature6;
        private System.Windows.Forms.Label lblFeature5;
        private System.Windows.Forms.Label lblFeature3;
        private System.Windows.Forms.Label lblFeature2;
        private System.Windows.Forms.Label lblFeature1;
        private System.Windows.Forms.Label lblFeature;
        private System.Windows.Forms.Label lblFeature4;
        private System.Windows.Forms.Label lblFeature11;
    }
}