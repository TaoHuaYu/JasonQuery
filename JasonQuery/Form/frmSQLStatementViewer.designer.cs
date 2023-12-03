﻿using JasonLibrary;

namespace JasonQuery
{
    sealed partial class frmSQLStatementViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSQLStatementViewer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnWordWrap = new System.Windows.Forms.ToolStripButton();
            this.btnWordWrap2 = new System.Windows.Forms.ToolStripButton();
            this.btnShowAllCharacters = new System.Windows.Forms.ToolStripButton();
            this.btnShowAllCharacters2 = new System.Windows.Forms.ToolStripButton();
            this.editorCellViewer = new ScintillaEditor();
            this.c1ThemeController1 = new C1.Win.C1Themes.C1ThemeController();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelectAll,
            this.btnCopy,
            this.btnWordWrap,
            this.btnWordWrap2,
            this.btnShowAllCharacters,
            this.btnShowAllCharacters2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(747, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.c1ThemeController1.SetTheme(this.toolStrip1, "(default)");
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.Image")));
            this.btnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(28, 28);
            this.btnSelectAll.ToolTipText = "Select All";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(28, 28);
            this.btnCopy.ToolTipText = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnWordWrap
            // 
            this.btnWordWrap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnWordWrap.Image = ((System.Drawing.Image)(resources.GetObject("btnWordWrap.Image")));
            this.btnWordWrap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWordWrap.Name = "btnWordWrap";
            this.btnWordWrap.Size = new System.Drawing.Size(28, 28);
            this.btnWordWrap.ToolTipText = "Word Wrap";
            this.btnWordWrap.Click += new System.EventHandler(this.btnWordWrap_Click);
            // 
            // btnWordWrap2
            // 
            this.btnWordWrap2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnWordWrap2.Image = ((System.Drawing.Image)(resources.GetObject("btnWordWrap2.Image")));
            this.btnWordWrap2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWordWrap2.Name = "btnWordWrap2";
            this.btnWordWrap2.Size = new System.Drawing.Size(28, 28);
            this.btnWordWrap2.ToolTipText = "Word Wrap";
            this.btnWordWrap2.Visible = false;
            this.btnWordWrap2.Click += new System.EventHandler(this.btnWordWrap_Click);
            // 
            // btnShowAllCharacters
            // 
            this.btnShowAllCharacters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowAllCharacters.Image = ((System.Drawing.Image)(resources.GetObject("btnShowAllCharacters.Image")));
            this.btnShowAllCharacters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowAllCharacters.Name = "btnShowAllCharacters";
            this.btnShowAllCharacters.Size = new System.Drawing.Size(28, 28);
            this.btnShowAllCharacters.ToolTipText = "Show All Characters";
            this.btnShowAllCharacters.Click += new System.EventHandler(this.btnShowAllCharacters_Click);
            // 
            // btnShowAllCharacters2
            // 
            this.btnShowAllCharacters2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowAllCharacters2.Image = ((System.Drawing.Image)(resources.GetObject("btnShowAllCharacters2.Image")));
            this.btnShowAllCharacters2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowAllCharacters2.Name = "btnShowAllCharacters2";
            this.btnShowAllCharacters2.Size = new System.Drawing.Size(28, 28);
            this.btnShowAllCharacters2.ToolTipText = "Show All Characters";
            this.btnShowAllCharacters2.Visible = false;
            this.btnShowAllCharacters2.Click += new System.EventHandler(this.btnShowAllCharacters_Click);
            // 
            // editorCellViewer
            // 
            this.editorCellViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorCellViewer.CaretLineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.editorCellViewer.CaretLineVisible = true;
            this.editorCellViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorCellViewer.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.editorCellViewer.Location = new System.Drawing.Point(0, 31);
            this.editorCellViewer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editorCellViewer.Name = "editorCellViewer";
            this.editorCellViewer.Size = new System.Drawing.Size(747, 291);
            this.editorCellViewer.Styler = null;
            this.editorCellViewer.TabIndex = 42;
            this.editorCellViewer.Tag = "";
            this.editorCellViewer.WhitespaceSize = 3;
            this.editorCellViewer.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.editorCellViewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editorCellViewer_MouseDown);
            // 
            // frmSQLStatementViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 322);
            this.Controls.Add(this.editorCellViewer);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(763, 360);
            this.Name = "frmSQLStatementViewer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SQL Statement Viewer";
            this.c1ThemeController1.SetTheme(this, "(default)");
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResizeEnd += new System.EventHandler(this.Form_ResizeEnd);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private ScintillaEditor editorCellViewer;
        private System.Windows.Forms.ToolStripButton btnWordWrap;
        private System.Windows.Forms.ToolStripButton btnWordWrap2;
        private System.Windows.Forms.ToolStripButton btnShowAllCharacters;
        private System.Windows.Forms.ToolStripButton btnShowAllCharacters2;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnSelectAll;
        private C1.Win.C1Themes.C1ThemeController c1ThemeController1;
    }
}