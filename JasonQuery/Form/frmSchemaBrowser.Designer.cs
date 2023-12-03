using JasonLibrary;

namespace JasonQuery
{
    partial class frmSchemaBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSchemaBrowser));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblFilter0 = new System.Windows.Forms.Label();
            this.lblFilter = new System.Windows.Forms.Label();
            this.txtFilter = new C1.Win.C1Input.C1TextBox();
            this.btnExpandCollapse = new C1.Win.C1Input.C1SplitButton();
            this.mnuExpandAll0 = new C1.Win.C1Input.DropDownItem();
            this.mnuCollapseAll0 = new C1.Win.C1Input.DropDownItem();
            this.lblSchemaType0 = new System.Windows.Forms.Label();
            this.c1GridSchemaBrowser = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.c1DockingTab1 = new C1.Win.C1Command.C1DockingTab();
            this.tabSQLPane = new C1.Win.C1Command.C1DockingTabPage();
            this.chkCopyAsHTML = new C1.Win.C1Input.C1CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnLeftAndRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnWordWrap = new System.Windows.Forms.ToolStripButton();
            this.btnWordWrap2 = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.lblInfo = new System.Windows.Forms.ToolStripLabel();
            this.editorSQLPane = new JasonLibrary.ScintillaEditor();
            this.tabTableStructure = new C1.Win.C1Command.C1DockingTabPage();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.lblTableName1 = new System.Windows.Forms.ToolStripLabel();
            this.lblTableName01 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTableSelectAll = new System.Windows.Forms.ToolStripButton();
            this.btnTableCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTableExportToFile = new System.Windows.Forms.ToolStripButton();
            this.c1GridStructure = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.tabView100RowsTop = new C1.Win.C1Command.C1DockingTabPage();
            this.c1Grid100RowsTop = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.lblTableName2 = new System.Windows.Forms.ToolStripLabel();
            this.lblTableName02 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTopSelectAll = new System.Windows.Forms.ToolStripButton();
            this.btnTopCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnTopExportToFile = new System.Windows.Forms.ToolStripButton();
            this.tabView100RowsLast = new C1.Win.C1Command.C1DockingTabPage();
            this.c1Grid100RowsLast = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.lblTableName3 = new System.Windows.Forms.ToolStripLabel();
            this.lblTableName03 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLastSelectAll = new System.Windows.Forms.ToolStripButton();
            this.btnLastCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLastExportToFile = new System.Windows.Forms.ToolStripButton();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblSchemaName2 = new System.Windows.Forms.Label();
            this.lblSchemaType2 = new System.Windows.Forms.Label();
            this.tmrMouseDoubleClick = new System.Windows.Forms.Timer(this.components);
            this.tmrMother2Child = new System.Windows.Forms.Timer(this.components);
            this.c1ThemeController1 = new C1.Win.C1Themes.C1ThemeController();
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExpandCollapse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridSchemaBrowser)).BeginInit();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.tabSQLPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCopyAsHTML)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabTableStructure.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridStructure)).BeginInit();
            this.tabView100RowsTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Grid100RowsTop)).BeginInit();
            this.toolStrip4.SuspendLayout();
            this.tabView100RowsLast.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Grid100RowsLast)).BeginInit();
            this.toolStrip5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblFilter0);
            this.splitContainer1.Panel1.Controls.Add(this.lblFilter);
            this.splitContainer1.Panel1.Controls.Add(this.txtFilter);
            this.splitContainer1.Panel1.Controls.Add(this.btnExpandCollapse);
            this.splitContainer1.Panel1.Controls.Add(this.lblSchemaType0);
            this.splitContainer1.Panel1.Controls.Add(this.c1GridSchemaBrowser);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            this.splitContainer1.Panel1MinSize = 333;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.c1DockingTab1);
            this.splitContainer1.Panel2.Controls.Add(this.lblLevel);
            this.splitContainer1.Panel2.Controls.Add(this.lblSchemaName2);
            this.splitContainer1.Panel2.Controls.Add(this.lblSchemaType2);
            this.splitContainer1.Panel2MinSize = 432;
            this.splitContainer1.Size = new System.Drawing.Size(1436, 755);
            this.splitContainer1.SplitterDistance = 333;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            this.c1ThemeController1.SetTheme(this.splitContainer1, "(default)");
            this.splitContainer1.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.splitContainer1_SplitterMoving);
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // lblFilter0
            // 
            this.lblFilter0.AutoSize = true;
            this.lblFilter0.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblFilter0.Location = new System.Drawing.Point(90, 15);
            this.lblFilter0.Name = "lblFilter0";
            this.lblFilter0.Size = new System.Drawing.Size(38, 16);
            this.lblFilter0.TabIndex = 43;
            this.lblFilter0.Text = "Filter:";
            this.lblFilter0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.c1ThemeController1.SetTheme(this.lblFilter0, "(default)");
            this.lblFilter0.Visible = false;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblFilter.Location = new System.Drawing.Point(90, 5);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(38, 16);
            this.lblFilter.TabIndex = 91;
            this.lblFilter.Text = "Filter:";
            this.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilter.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtFilter.Location = new System.Drawing.Point(131, 2);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(121, 21);
            this.txtFilter.TabIndex = 90;
            this.txtFilter.Tag = null;
            this.txtFilter.Text = "*";
            this.txtFilter.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.txtFilter, "(default)");
            this.txtFilter.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
            // 
            // btnExpandCollapse
            // 
            this.btnExpandCollapse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExpandCollapse.Image = ((System.Drawing.Image)(resources.GetObject("btnExpandCollapse.Image")));
            this.btnExpandCollapse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExpandCollapse.Items.Add(this.mnuExpandAll0);
            this.btnExpandCollapse.Items.Add(this.mnuCollapseAll0);
            this.btnExpandCollapse.Location = new System.Drawing.Point(32, 0);
            this.btnExpandCollapse.Name = "btnExpandCollapse";
            this.btnExpandCollapse.Size = new System.Drawing.Size(45, 24);
            this.btnExpandCollapse.TabIndex = 45;
            this.btnExpandCollapse.UseVisualStyleBackColor = true;
            // 
            // mnuExpandAll0
            // 
            this.mnuExpandAll0.Tag = "mnuExpandAll";
            this.mnuExpandAll0.Text = "Expand All";
            // 
            // mnuCollapseAll0
            // 
            this.mnuCollapseAll0.Tag = "mnuCollapseAll";
            this.mnuCollapseAll0.Text = "Collapse All";
            // 
            // lblSchemaType0
            // 
            this.lblSchemaType0.AutoSize = true;
            this.lblSchemaType0.Location = new System.Drawing.Point(67, 4);
            this.lblSchemaType0.Name = "lblSchemaType0";
            this.lblSchemaType0.Size = new System.Drawing.Size(0, 16);
            this.lblSchemaType0.TabIndex = 43;
            // 
            // c1GridSchemaBrowser
            // 
            this.c1GridSchemaBrowser.AllowUpdate = false;
            this.c1GridSchemaBrowser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1GridSchemaBrowser.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.GroupBy;
            this.c1GridSchemaBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1GridSchemaBrowser.FetchRowStyles = true;
            this.c1GridSchemaBrowser.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1GridSchemaBrowser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1GridSchemaBrowser.GroupByAreaVisible = false;
            this.c1GridSchemaBrowser.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1GridSchemaBrowser.Images.Add(((System.Drawing.Image)(resources.GetObject("c1GridSchemaBrowser.Images"))));
            this.c1GridSchemaBrowser.Location = new System.Drawing.Point(0, 25);
            this.c1GridSchemaBrowser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.c1GridSchemaBrowser.Name = "c1GridSchemaBrowser";
            this.c1GridSchemaBrowser.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1GridSchemaBrowser.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1GridSchemaBrowser.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1GridSchemaBrowser.PreviewInfo.ZoomFactor = 75D;
            this.c1GridSchemaBrowser.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1GridSchemaBrowser.PrintInfo.MeasurementPrinterName = null;
            this.c1GridSchemaBrowser.RowHeight = 19;
            this.c1GridSchemaBrowser.Size = new System.Drawing.Size(333, 730);
            this.c1GridSchemaBrowser.TabIndex = 5;
            this.c1GridSchemaBrowser.UseCompatibleTextRendering = false;
            this.c1GridSchemaBrowser.ColResize += new C1.Win.C1TrueDBGrid.ColResizeEventHandler(this.c1GridSchemaBrowser_ColResize);
            this.c1GridSchemaBrowser.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.c1GridSchemaBrowser_FetchRowStyle);
            this.c1GridSchemaBrowser.Expand += new C1.Win.C1TrueDBGrid.BandEventHandler(this.c1GridSchemaBrowser_Expand);
            this.c1GridSchemaBrowser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1GridSchemaBrowser_KeyDown);
            this.c1GridSchemaBrowser.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1GridSchemaBrowser_MouseDoubleClick);
            this.c1GridSchemaBrowser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1GridSchemaBrowser_MouseDown);
            this.c1GridSchemaBrowser.MouseUp += new System.Windows.Forms.MouseEventHandler(this.c1GridSchemaBrowser_MouseUp);
            this.c1GridSchemaBrowser.PropBag = resources.GetString("c1GridSchemaBrowser.PropBag");
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.tsSeparator1,
            this.toolStripLabel6,
            this.toolStripSeparator1,
            this.toolStripLabel4,
            this.toolStripLabel5});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(333, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 20);
            this.btnRefresh.ToolTipText = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(49, 15);
            this.toolStripLabel6.Text = "              ";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Font = new System.Drawing.Font("微軟正黑體", 3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(4, 5);
            this.toolStripLabel4.Text = " ";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Font = new System.Drawing.Font("微軟正黑體", 3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(4, 5);
            this.toolStripLabel5.Text = " ";
            // 
            // c1DockingTab1
            // 
            this.c1DockingTab1.Controls.Add(this.tabSQLPane);
            this.c1DockingTab1.Controls.Add(this.tabTableStructure);
            this.c1DockingTab1.Controls.Add(this.tabView100RowsTop);
            this.c1DockingTab1.Controls.Add(this.tabView100RowsLast);
            this.c1DockingTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1DockingTab1.Location = new System.Drawing.Point(0, 0);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.Size = new System.Drawing.Size(1098, 755);
            this.c1DockingTab1.TabIndex = 41;
            this.c1DockingTab1.TabsSpacing = 5;
            this.c1ThemeController1.SetTheme(this.c1DockingTab1, "(default)");
            this.c1DockingTab1.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.c1DockingTab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.c1DockingTab1.TabClick += new System.EventHandler(this.c1DockingTab1_TabClick);
            // 
            // tabSQLPane
            // 
            this.tabSQLPane.Controls.Add(this.chkCopyAsHTML);
            this.tabSQLPane.Controls.Add(this.toolStrip1);
            this.tabSQLPane.Controls.Add(this.editorSQLPane);
            this.tabSQLPane.Location = new System.Drawing.Point(1, 27);
            this.tabSQLPane.Name = "tabSQLPane";
            this.tabSQLPane.Size = new System.Drawing.Size(1096, 727);
            this.tabSQLPane.TabIndex = 0;
            this.tabSQLPane.Tag = "SQL Pane";
            this.tabSQLPane.Text = "SQL Pane";
            // 
            // chkCopyAsHTML
            // 
            this.chkCopyAsHTML.AutoSize = true;
            this.chkCopyAsHTML.BackColor = System.Drawing.Color.Transparent;
            this.chkCopyAsHTML.BorderColor = System.Drawing.Color.Transparent;
            this.chkCopyAsHTML.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkCopyAsHTML.ForeColor = System.Drawing.Color.Black;
            this.chkCopyAsHTML.Location = new System.Drawing.Point(81, 2);
            this.chkCopyAsHTML.Name = "chkCopyAsHTML";
            this.chkCopyAsHTML.Padding = new System.Windows.Forms.Padding(1);
            this.chkCopyAsHTML.Size = new System.Drawing.Size(198, 22);
            this.chkCopyAsHTML.TabIndex = 40;
            this.chkCopyAsHTML.Text = "Copy SQL Statement as HTML";
            this.c1ThemeController1.SetTheme(this.chkCopyAsHTML, "(default)");
            this.chkCopyAsHTML.UseVisualStyleBackColor = true;
            this.chkCopyAsHTML.Value = null;
            this.chkCopyAsHTML.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLeftAndRight,
            this.toolStripSeparator5,
            this.btnSelectAll,
            this.btnCopy,
            this.btnWordWrap,
            this.btnWordWrap2,
            this.tsSeparator4,
            this.toolStripLabel2,
            this.lblInfo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1096, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.c1ThemeController1.SetTheme(this.toolStrip1, "(default)");
            // 
            // btnLeftAndRight
            // 
            this.btnLeftAndRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLeftAndRight.Image = ((System.Drawing.Image)(resources.GetObject("btnLeftAndRight.Image")));
            this.btnLeftAndRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLeftAndRight.Name = "btnLeftAndRight";
            this.btnLeftAndRight.Size = new System.Drawing.Size(23, 22);
            this.btnLeftAndRight.Text = "LeftAndRight";
            this.btnLeftAndRight.ToolTipText = "Save SplitContainer\'s horizontal width";
            this.btnLeftAndRight.Visible = false;
            this.btnLeftAndRight.Click += new System.EventHandler(this.btnLeftAndRight_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator5.Visible = false;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.Image")));
            this.btnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(23, 22);
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnWordWrap
            // 
            this.btnWordWrap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnWordWrap.Image = ((System.Drawing.Image)(resources.GetObject("btnWordWrap.Image")));
            this.btnWordWrap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWordWrap.Name = "btnWordWrap";
            this.btnWordWrap.Size = new System.Drawing.Size(23, 22);
            this.btnWordWrap.Text = "Word Wrap";
            this.btnWordWrap.Click += new System.EventHandler(this.btnWordWrap_Click);
            // 
            // btnWordWrap2
            // 
            this.btnWordWrap2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnWordWrap2.Image = ((System.Drawing.Image)(resources.GetObject("btnWordWrap2.Image")));
            this.btnWordWrap2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWordWrap2.Name = "btnWordWrap2";
            this.btnWordWrap2.Size = new System.Drawing.Size(23, 22);
            this.btnWordWrap2.Text = "Word Wrap";
            this.btnWordWrap2.Click += new System.EventHandler(this.btnWordWrap_Click);
            // 
            // tsSeparator4
            // 
            this.tsSeparator4.Name = "tsSeparator4";
            this.tsSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("微軟正黑體", 3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(4, 22);
            this.toolStripLabel2.Text = " ";
            // 
            // lblInfo
            // 
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(43, 22);
            this.lblInfo.Text = "lblInfo";
            this.lblInfo.Visible = false;
            // 
            // editorSQLPane
            // 
            this.editorSQLPane.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorSQLPane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorSQLPane.CaretLineVisible = true;
            this.editorSQLPane.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.editorSQLPane.Location = new System.Drawing.Point(3, 25);
            this.editorSQLPane.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editorSQLPane.Name = "editorSQLPane";
            this.editorSQLPane.ReadOnly = true;
            this.editorSQLPane.Size = new System.Drawing.Size(1090, 698);
            this.editorSQLPane.Styler = null;
            this.editorSQLPane.TabIndex = 1;
            this.editorSQLPane.WhitespaceSize = 3;
            this.editorSQLPane.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.editorSQLPane.WrapMode = ScintillaNET.WrapMode.Word;
            this.editorSQLPane.Enter += new System.EventHandler(this.editorSQLPane_Enter);
            this.editorSQLPane.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editorSQLPane_KeyDown);
            this.editorSQLPane.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editorSQLPane_KeyPress);
            this.editorSQLPane.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editorSQLPane_MouseDown);
            // 
            // tabTableStructure
            // 
            this.tabTableStructure.Controls.Add(this.toolStrip3);
            this.tabTableStructure.Controls.Add(this.c1GridStructure);
            this.tabTableStructure.Location = new System.Drawing.Point(1, 27);
            this.tabTableStructure.Name = "tabTableStructure";
            this.tabTableStructure.Size = new System.Drawing.Size(1096, 727);
            this.tabTableStructure.TabIndex = 1;
            this.tabTableStructure.TabVisible = false;
            this.tabTableStructure.Tag = "Table Structure";
            this.tabTableStructure.Text = "Table Structure";
            // 
            // toolStrip3
            // 
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTableName1,
            this.lblTableName01,
            this.toolStripSeparator3,
            this.btnTableSelectAll,
            this.btnTableCopy,
            this.toolStripSeparator2,
            this.btnTableExportToFile});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1096, 25);
            this.toolStrip3.TabIndex = 67;
            this.toolStrip3.Text = "toolStrip3";
            this.c1ThemeController1.SetTheme(this.toolStrip3, "(default)");
            // 
            // lblTableName1
            // 
            this.lblTableName1.Name = "lblTableName1";
            this.lblTableName1.Size = new System.Drawing.Size(80, 22);
            this.lblTableName1.Text = "Table Name:";
            // 
            // lblTableName01
            // 
            this.lblTableName01.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTableName01.ForeColor = System.Drawing.Color.Blue;
            this.lblTableName01.Name = "lblTableName01";
            this.lblTableName01.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnTableSelectAll
            // 
            this.btnTableSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTableSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnTableSelectAll.Image")));
            this.btnTableSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTableSelectAll.Name = "btnTableSelectAll";
            this.btnTableSelectAll.Size = new System.Drawing.Size(23, 22);
            this.btnTableSelectAll.Click += new System.EventHandler(this.btnGridSelectAll_Click);
            // 
            // btnTableCopy
            // 
            this.btnTableCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTableCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnTableCopy.Image")));
            this.btnTableCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTableCopy.Name = "btnTableCopy";
            this.btnTableCopy.Size = new System.Drawing.Size(23, 22);
            this.btnTableCopy.Click += new System.EventHandler(this.btnGridCopy_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnTableExportToFile
            // 
            this.btnTableExportToFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTableExportToFile.Image = ((System.Drawing.Image)(resources.GetObject("btnTableExportToFile.Image")));
            this.btnTableExportToFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTableExportToFile.Name = "btnTableExportToFile";
            this.btnTableExportToFile.Size = new System.Drawing.Size(23, 22);
            this.btnTableExportToFile.Click += new System.EventHandler(this.btnGridExportToFile_Click);
            // 
            // c1GridStructure
            // 
            this.c1GridStructure.AllowUpdate = false;
            this.c1GridStructure.AllowUpdateOnBlur = false;
            this.c1GridStructure.AlternatingRows = true;
            this.c1GridStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1GridStructure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1GridStructure.FetchRowStyles = true;
            this.c1GridStructure.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1GridStructure.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1GridStructure.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1GridStructure.Images.Add(((System.Drawing.Image)(resources.GetObject("c1GridStructure.Images"))));
            this.c1GridStructure.Location = new System.Drawing.Point(2, 26);
            this.c1GridStructure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.c1GridStructure.Name = "c1GridStructure";
            this.c1GridStructure.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1GridStructure.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1GridStructure.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1GridStructure.PreviewInfo.ZoomFactor = 75D;
            this.c1GridStructure.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1GridStructure.PrintInfo.MeasurementPrinterName = null;
            this.c1GridStructure.RowHeight = 19;
            this.c1GridStructure.Size = new System.Drawing.Size(1091, 699);
            this.c1GridStructure.TabIndex = 66;
            this.c1ThemeController1.SetTheme(this.c1GridStructure, "(default)");
            this.c1GridStructure.UseCompatibleTextRendering = false;
            this.c1GridStructure.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.c1GridStructure_FetchRowStyle);
            this.c1GridStructure.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1Grid_KeyDown);
            this.c1GridStructure.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1Grid_MouseClick);
            this.c1GridStructure.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1Grid_MouseDown);
            this.c1GridStructure.PropBag = resources.GetString("c1GridStructure.PropBag");
            // 
            // tabView100RowsTop
            // 
            this.tabView100RowsTop.Controls.Add(this.c1Grid100RowsTop);
            this.tabView100RowsTop.Controls.Add(this.toolStrip4);
            this.tabView100RowsTop.Location = new System.Drawing.Point(1, 27);
            this.tabView100RowsTop.Name = "tabView100RowsTop";
            this.tabView100RowsTop.Size = new System.Drawing.Size(1096, 727);
            this.tabView100RowsTop.TabIndex = 2;
            this.tabView100RowsTop.TabVisible = false;
            this.tabView100RowsTop.Tag = "View Top 100 Rows";
            this.tabView100RowsTop.Text = "View Top 100 Rows";
            // 
            // c1Grid100RowsTop
            // 
            this.c1Grid100RowsTop.AllowUpdate = false;
            this.c1Grid100RowsTop.AllowUpdateOnBlur = false;
            this.c1Grid100RowsTop.AlternatingRows = true;
            this.c1Grid100RowsTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1Grid100RowsTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1Grid100RowsTop.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1Grid100RowsTop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1Grid100RowsTop.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1Grid100RowsTop.Images.Add(((System.Drawing.Image)(resources.GetObject("c1Grid100RowsTop.Images"))));
            this.c1Grid100RowsTop.Location = new System.Drawing.Point(2, 26);
            this.c1Grid100RowsTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.c1Grid100RowsTop.Name = "c1Grid100RowsTop";
            this.c1Grid100RowsTop.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1Grid100RowsTop.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1Grid100RowsTop.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1Grid100RowsTop.PreviewInfo.ZoomFactor = 75D;
            this.c1Grid100RowsTop.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1Grid100RowsTop.PrintInfo.MeasurementPrinterName = null;
            this.c1Grid100RowsTop.RowHeight = 19;
            this.c1Grid100RowsTop.Size = new System.Drawing.Size(1091, 699);
            this.c1Grid100RowsTop.TabIndex = 69;
            this.c1ThemeController1.SetTheme(this.c1Grid100RowsTop, "(default)");
            this.c1Grid100RowsTop.UseCompatibleTextRendering = false;
            this.c1Grid100RowsTop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1Grid_KeyDown);
            this.c1Grid100RowsTop.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1Grid_MouseClick);
            this.c1Grid100RowsTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1Grid_MouseDown);
            this.c1Grid100RowsTop.PropBag = resources.GetString("c1Grid100RowsTop.PropBag");
            // 
            // toolStrip4
            // 
            this.toolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTableName2,
            this.lblTableName02,
            this.toolStripSeparator4,
            this.btnTopSelectAll,
            this.btnTopCopy,
            this.toolStripSeparator6,
            this.btnTopExportToFile});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(1096, 25);
            this.toolStrip4.TabIndex = 68;
            this.toolStrip4.Text = "toolStrip4";
            this.c1ThemeController1.SetTheme(this.toolStrip4, "(default)");
            // 
            // lblTableName2
            // 
            this.lblTableName2.Name = "lblTableName2";
            this.lblTableName2.Size = new System.Drawing.Size(80, 22);
            this.lblTableName2.Text = "Table Name:";
            // 
            // lblTableName02
            // 
            this.lblTableName02.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTableName02.ForeColor = System.Drawing.Color.Blue;
            this.lblTableName02.Name = "lblTableName02";
            this.lblTableName02.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnTopSelectAll
            // 
            this.btnTopSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTopSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnTopSelectAll.Image")));
            this.btnTopSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTopSelectAll.Name = "btnTopSelectAll";
            this.btnTopSelectAll.Size = new System.Drawing.Size(23, 22);
            this.btnTopSelectAll.Click += new System.EventHandler(this.btnGridSelectAll_Click);
            // 
            // btnTopCopy
            // 
            this.btnTopCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTopCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnTopCopy.Image")));
            this.btnTopCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTopCopy.Name = "btnTopCopy";
            this.btnTopCopy.Size = new System.Drawing.Size(23, 22);
            this.btnTopCopy.Click += new System.EventHandler(this.btnGridCopy_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // btnTopExportToFile
            // 
            this.btnTopExportToFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTopExportToFile.Image = ((System.Drawing.Image)(resources.GetObject("btnTopExportToFile.Image")));
            this.btnTopExportToFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTopExportToFile.Name = "btnTopExportToFile";
            this.btnTopExportToFile.Size = new System.Drawing.Size(23, 22);
            this.btnTopExportToFile.Click += new System.EventHandler(this.btnGridExportToFile_Click);
            // 
            // tabView100RowsLast
            // 
            this.tabView100RowsLast.Controls.Add(this.c1Grid100RowsLast);
            this.tabView100RowsLast.Controls.Add(this.toolStrip5);
            this.tabView100RowsLast.Location = new System.Drawing.Point(1, 27);
            this.tabView100RowsLast.Name = "tabView100RowsLast";
            this.tabView100RowsLast.Size = new System.Drawing.Size(1096, 727);
            this.tabView100RowsLast.TabIndex = 3;
            this.tabView100RowsLast.TabVisible = false;
            this.tabView100RowsLast.Tag = "View Last 100 Rows";
            this.tabView100RowsLast.Text = "View Last 100 Rows";
            // 
            // c1Grid100RowsLast
            // 
            this.c1Grid100RowsLast.AllowUpdate = false;
            this.c1Grid100RowsLast.AllowUpdateOnBlur = false;
            this.c1Grid100RowsLast.AlternatingRows = true;
            this.c1Grid100RowsLast.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1Grid100RowsLast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1Grid100RowsLast.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1Grid100RowsLast.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1Grid100RowsLast.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1Grid100RowsLast.Images.Add(((System.Drawing.Image)(resources.GetObject("c1Grid100RowsLast.Images"))));
            this.c1Grid100RowsLast.Location = new System.Drawing.Point(2, 26);
            this.c1Grid100RowsLast.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.c1Grid100RowsLast.Name = "c1Grid100RowsLast";
            this.c1Grid100RowsLast.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1Grid100RowsLast.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1Grid100RowsLast.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1Grid100RowsLast.PreviewInfo.ZoomFactor = 75D;
            this.c1Grid100RowsLast.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1Grid100RowsLast.PrintInfo.MeasurementPrinterName = null;
            this.c1Grid100RowsLast.RowHeight = 19;
            this.c1Grid100RowsLast.Size = new System.Drawing.Size(1091, 699);
            this.c1Grid100RowsLast.TabIndex = 70;
            this.c1ThemeController1.SetTheme(this.c1Grid100RowsLast, "(default)");
            this.c1Grid100RowsLast.UseCompatibleTextRendering = false;
            this.c1Grid100RowsLast.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1Grid_KeyDown);
            this.c1Grid100RowsLast.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1Grid_MouseClick);
            this.c1Grid100RowsLast.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1Grid_MouseDown);
            this.c1Grid100RowsLast.PropBag = resources.GetString("c1Grid100RowsLast.PropBag");
            // 
            // toolStrip5
            // 
            this.toolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTableName3,
            this.lblTableName03,
            this.toolStripSeparator7,
            this.btnLastSelectAll,
            this.btnLastCopy,
            this.toolStripSeparator8,
            this.btnLastExportToFile});
            this.toolStrip5.Location = new System.Drawing.Point(0, 0);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(1096, 25);
            this.toolStrip5.TabIndex = 69;
            this.toolStrip5.Text = "toolStrip5";
            this.c1ThemeController1.SetTheme(this.toolStrip5, "(default)");
            // 
            // lblTableName3
            // 
            this.lblTableName3.Name = "lblTableName3";
            this.lblTableName3.Size = new System.Drawing.Size(80, 22);
            this.lblTableName3.Text = "Table Name:";
            // 
            // lblTableName03
            // 
            this.lblTableName03.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTableName03.ForeColor = System.Drawing.Color.Blue;
            this.lblTableName03.Name = "lblTableName03";
            this.lblTableName03.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btnLastSelectAll
            // 
            this.btnLastSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLastSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnLastSelectAll.Image")));
            this.btnLastSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLastSelectAll.Name = "btnLastSelectAll";
            this.btnLastSelectAll.Size = new System.Drawing.Size(23, 22);
            this.btnLastSelectAll.Click += new System.EventHandler(this.btnGridSelectAll_Click);
            // 
            // btnLastCopy
            // 
            this.btnLastCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLastCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnLastCopy.Image")));
            this.btnLastCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLastCopy.Name = "btnLastCopy";
            this.btnLastCopy.Size = new System.Drawing.Size(23, 22);
            this.btnLastCopy.Click += new System.EventHandler(this.btnGridCopy_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // btnLastExportToFile
            // 
            this.btnLastExportToFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLastExportToFile.Image = ((System.Drawing.Image)(resources.GetObject("btnLastExportToFile.Image")));
            this.btnLastExportToFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLastExportToFile.Name = "btnLastExportToFile";
            this.btnLastExportToFile.Size = new System.Drawing.Size(23, 22);
            this.btnLastExportToFile.Click += new System.EventHandler(this.btnGridExportToFile_Click);
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLevel.Location = new System.Drawing.Point(628, 8);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(51, 16);
            this.lblLevel.TabIndex = 39;
            this.lblLevel.Text = "lblLevel";
            this.lblLevel.Visible = false;
            // 
            // lblSchemaName2
            // 
            this.lblSchemaName2.AutoSize = true;
            this.lblSchemaName2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSchemaName2.Location = new System.Drawing.Point(805, 8);
            this.lblSchemaName2.Name = "lblSchemaName2";
            this.lblSchemaName2.Size = new System.Drawing.Size(102, 16);
            this.lblSchemaName2.TabIndex = 38;
            this.lblSchemaName2.Text = "lblSchemaName";
            this.lblSchemaName2.Visible = false;
            // 
            // lblSchemaType2
            // 
            this.lblSchemaType2.AutoSize = true;
            this.lblSchemaType2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSchemaType2.Location = new System.Drawing.Point(694, 8);
            this.lblSchemaType2.Name = "lblSchemaType2";
            this.lblSchemaType2.Size = new System.Drawing.Size(95, 16);
            this.lblSchemaType2.TabIndex = 37;
            this.lblSchemaType2.Text = "lblSchemaType";
            this.lblSchemaType2.Visible = false;
            // 
            // tmrMouseDoubleClick
            // 
            this.tmrMouseDoubleClick.Tick += new System.EventHandler(this.tmrMouseDoubleClick_Tick);
            // 
            // tmrMother2Child
            // 
            this.tmrMother2Child.Enabled = true;
            this.tmrMother2Child.Tick += new System.EventHandler(this.tmrMother2Child_Tick);
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Owner = this;
            // 
            // frmSchemaBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 755);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSchemaBrowser";
            this.Text = "Schema Browser";
            this.c1ThemeController1.SetTheme(this, "(default)");
            this.Load += new System.EventHandler(this.Form_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExpandCollapse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridSchemaBrowser)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).EndInit();
            this.c1DockingTab1.ResumeLayout(false);
            this.tabSQLPane.ResumeLayout(false);
            this.tabSQLPane.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCopyAsHTML)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabTableStructure.ResumeLayout(false);
            this.tabTableStructure.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridStructure)).EndInit();
            this.tabView100RowsTop.ResumeLayout(false);
            this.tabView100RowsTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Grid100RowsTop)).EndInit();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.tabView100RowsLast.ResumeLayout(false);
            this.tabView100RowsLast.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Grid100RowsLast)).EndInit();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ScintillaEditor editorSQLPane;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1GridSchemaBrowser;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripSeparator tsSeparator1;
        private System.Windows.Forms.Timer tmrMouseDoubleClick;
        private System.Windows.Forms.Label lblSchemaName2;
        private System.Windows.Forms.Label lblSchemaType2;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Timer tmrMother2Child;
        private C1.Win.C1Themes.C1ThemeController c1ThemeController1;
        private C1.Win.C1Input.C1CheckBox chkCopyAsHTML;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private System.Windows.Forms.Label lblSchemaType0;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private C1.Win.C1Input.C1SplitButton btnExpandCollapse;
        private C1.Win.C1Input.DropDownItem mnuExpandAll0;
        private C1.Win.C1Input.DropDownItem mnuCollapseAll0;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage tabSQLPane;
        private C1.Win.C1Command.C1DockingTabPage tabTableStructure;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnLeftAndRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnSelectAll;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnWordWrap;
        private System.Windows.Forms.ToolStripButton btnWordWrap2;
        private System.Windows.Forms.ToolStripSeparator tsSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel lblInfo;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1GridStructure;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton btnTableSelectAll;
        private System.Windows.Forms.ToolStripButton btnTableCopy;
        private System.Windows.Forms.ToolStripButton btnTableExportToFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblTableName1;
        private System.Windows.Forms.ToolStripLabel lblTableName01;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private C1.Win.C1Command.C1DockingTabPage tabView100RowsTop;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1Grid100RowsTop;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripLabel lblTableName2;
        private System.Windows.Forms.ToolStripLabel lblTableName02;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnTopSelectAll;
        private System.Windows.Forms.ToolStripButton btnTopCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnTopExportToFile;
        private C1.Win.C1Command.C1DockingTabPage tabView100RowsLast;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.ToolStripLabel lblTableName3;
        private System.Windows.Forms.ToolStripLabel lblTableName03;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btnLastSelectAll;
        private System.Windows.Forms.ToolStripButton btnLastCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnLastExportToFile;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1Grid100RowsLast;
        private C1.Win.C1Input.C1TextBox txtFilter;
        private System.Windows.Forms.Label lblFilter0;
        private System.Windows.Forms.Label lblFilter;
    }
}

