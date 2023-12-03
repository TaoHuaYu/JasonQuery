namespace JasonQuery
{
    partial class frmSingleRecordViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSingleRecordViewer));
            this.c1ThemeController1 = new C1.Win.C1Themes.C1ThemeController();
            this.c1Grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.tsViewer = new System.Windows.Forms.ToolStrip();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.btnPrevious = new System.Windows.Forms.ToolStripButton();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnExportToFile = new System.Windows.Forms.ToolStripButton();
            this.chkShowFilterRow = new C1.Win.C1Input.C1CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Grid)).BeginInit();
            this.tsViewer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilterRow)).BeginInit();
            this.SuspendLayout();
            // 
            // c1Grid
            // 
            this.c1Grid.AllowFilter = false;
            this.c1Grid.AllowUpdate = false;
            this.c1Grid.AllowUpdateOnBlur = false;
            this.c1Grid.AlternatingRows = true;
            this.c1Grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1Grid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1Grid.CaptionHeight = 19;
            this.c1Grid.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1Grid.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1Grid.Images.Add(((System.Drawing.Image)(resources.GetObject("c1Grid.Images"))));
            this.c1Grid.Location = new System.Drawing.Point(0, 30);
            this.c1Grid.Name = "c1Grid";
            this.c1Grid.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1Grid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1Grid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1Grid.PreviewInfo.ZoomFactor = 75D;
            this.c1Grid.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1Grid.PrintInfo.MeasurementPrinterName = null;
            this.c1Grid.RowHeight = 19;
            this.c1Grid.Size = new System.Drawing.Size(344, 452);
            this.c1Grid.TabIndex = 0;
            this.c1ThemeController1.SetTheme(this.c1Grid, "(default)");
            this.c1Grid.UseCompatibleTextRendering = false;
            this.c1Grid.ColResize += new C1.Win.C1TrueDBGrid.ColResizeEventHandler(this.c1Grid_ColResize);
            this.c1Grid.Filter += new C1.Win.C1TrueDBGrid.FilterEventHandler(this.c1Grid_Filter);
            this.c1Grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1Grid_KeyDown);
            this.c1Grid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1Grid_MouseClick);
            this.c1Grid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1Grid_MouseDoubleClick);
            this.c1Grid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1Grid_MouseDown);
            this.c1Grid.PropBag = resources.GetString("c1Grid.PropBag");
            // 
            // tsViewer
            // 
            this.tsViewer.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsViewer.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsViewer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFirst,
            this.btnPrevious,
            this.btnNext,
            this.btnLast,
            this.toolStripSeparator1,
            this.btnSelectAll,
            this.btnCopy,
            this.btnExportToFile});
            this.tsViewer.Location = new System.Drawing.Point(0, 0);
            this.tsViewer.Name = "tsViewer";
            this.tsViewer.Size = new System.Drawing.Size(344, 31);
            this.tsViewer.TabIndex = 1;
            this.tsViewer.Text = "toolStrip1";
            this.c1ThemeController1.SetTheme(this.tsViewer, "(default)");
            // 
            // btnFirst
            // 
            this.btnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(28, 28);
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(28, 28);
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(28, 28);
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(28, 28);
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.Image")));
            this.btnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(28, 28);
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(28, 28);
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnExportToFile
            // 
            this.btnExportToFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportToFile.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToFile.Image")));
            this.btnExportToFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportToFile.Name = "btnExportToFile";
            this.btnExportToFile.Size = new System.Drawing.Size(28, 28);
            this.btnExportToFile.Click += new System.EventHandler(this.btnExportToFile_Click);
            // 
            // chkShowFilterRow
            // 
            this.chkShowFilterRow.AutoSize = true;
            this.chkShowFilterRow.BackColor = System.Drawing.Color.Transparent;
            this.chkShowFilterRow.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowFilterRow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowFilterRow.ForeColor = System.Drawing.Color.Black;
            this.chkShowFilterRow.Location = new System.Drawing.Point(206, 4);
            this.chkShowFilterRow.Name = "chkShowFilterRow";
            this.chkShowFilterRow.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowFilterRow.Size = new System.Drawing.Size(118, 22);
            this.chkShowFilterRow.TabIndex = 77;
            this.chkShowFilterRow.Text = "Show Filter Row";
            this.c1ThemeController1.SetTheme(this.chkShowFilterRow, "(default)");
            this.chkShowFilterRow.UseVisualStyleBackColor = true;
            this.chkShowFilterRow.Value = null;
            this.chkShowFilterRow.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShowFilterRow.CheckedChanged += new System.EventHandler(this.chkShowFilterRow_CheckedChanged);
            // 
            // frmSingleRecordViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 482);
            this.Controls.Add(this.c1Grid);
            this.Controls.Add(this.chkShowFilterRow);
            this.Controls.Add(this.tsViewer);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(360, 520);
            this.Name = "frmSingleRecordViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Single Record Viewer";
            this.c1ThemeController1.SetTheme(this, "(default)");
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResizeEnd += new System.EventHandler(this.Form_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1Grid)).EndInit();
            this.tsViewer.ResumeLayout(false);
            this.tsViewer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilterRow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private C1.Win.C1Themes.C1ThemeController c1ThemeController1;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1Grid;
        private System.Windows.Forms.ToolStrip tsViewer;
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnPrevious;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private C1.Win.C1Input.C1CheckBox chkShowFilterRow;
        private System.Windows.Forms.ToolStripButton btnExportToFile;
        private System.Windows.Forms.ToolStripButton btnSelectAll;
        private System.Windows.Forms.ToolStripButton btnCopy;
    }
}