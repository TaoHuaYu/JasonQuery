using JasonLibrary;

namespace JasonQuery
{
    partial class frmQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuery));
            this.c1XLBook1 = new C1.C1Excel.C1XLBook();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtIndentWord = new C1.Win.C1Input.C1TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.c1DockingTab2 = new C1.Win.C1Command.C1DockingTab();
            this.tabAutoReplace = new C1.Win.C1Command.C1DockingTabPage();
            this.c1GridARInfo = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.tsAutoReplace = new System.Windows.Forms.ToolStrip();
            this.lblAutoReplace = new System.Windows.Forms.ToolStripLabel();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.tabSchemaBrowser = new C1.Win.C1Command.C1DockingTabPage();
            this.lblSchemaFilter0 = new System.Windows.Forms.Label();
            this.txtSchemaFilter = new C1.Win.C1Input.C1TextBox();
            this.tsSchemaBrowser = new System.Windows.Forms.ToolStrip();
            this.btnExpandCollapse = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSettingOfFocus = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuFocusOnDataGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFocusOnQueryEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.lblSchemaFilter = new System.Windows.Forms.ToolStripLabel();
            this.c1GridSchemaBrowser = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.editor = new JasonLibrary.ScintillaEditor();
            this.lblInfoEditor = new System.Windows.Forms.Label();
            this.c1StatusBar2 = new C1.Win.C1Ribbon.C1StatusBar();
            this.btnDatabase = new C1.Win.C1Ribbon.RibbonButton();
            this.spDatabase = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblEditorLength = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonLabel2 = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblEditorLines = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonSeparator1 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblEditorLn = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonLabel3 = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblEditorCol = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonLabel4 = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblEditorPos = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonLabel5 = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblEditorSel = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonSeparator7 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblEndOfLineStyle = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonSeparator8 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblEncode = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblTemp = new C1.Win.C1Ribbon.RibbonLabel();
            this.tsEditor = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnSaveRed = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnQuery = new System.Windows.Forms.ToolStripButton();
            this.btnSelectCurrentBlock = new System.Windows.Forms.ToolStripButton();
            this.btnExecuteCurrentBlock = new System.Windows.Forms.ToolStripButton();
            this.btnCancelQuery = new System.Windows.Forms.ToolStripButton();
            this.btnCommit = new System.Windows.Forms.ToolStripButton();
            this.btnRollback = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCode2SQL = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuCSharp2SQL = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVB2SQL = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDephi2SQL = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSQL2Code = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuSQL2CSharp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCSharpStyle1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCSharpStyle2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCSharpStyle3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSQL2VBNet = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVBNetStyle1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVBNetStyle2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVBNetStyle3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSQL2VB6A = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVB6AStyle1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVB6AStyle2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSQL2Delphi = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelphi6Style1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelphi6Style2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLeftAndRight = new System.Windows.Forms.ToolStripButton();
            this.btnUpAndDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnComment = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveComment = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIndent = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.txtIndentWord2 = new System.Windows.Forms.ToolStripTextBox();
            this.btnUnIndent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnHighlightSelection = new System.Windows.Forms.ToolStripButton();
            this.btnHighlightSelection2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnWordWrap = new System.Windows.Forms.ToolStripButton();
            this.btnWordWrap2 = new System.Windows.Forms.ToolStripButton();
            this.btnShowAllCharacters = new System.Windows.Forms.ToolStripButton();
            this.btnShowAllCharacters2 = new System.Windows.Forms.ToolStripButton();
            this.btnShowIndentGuide = new System.Windows.Forms.ToolStripButton();
            this.btnShowIndentGuide2 = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.chkShowGroupingRow = new C1.Win.C1Input.C1CheckBox();
            this.btnHelp_RawDataMode = new C1.Win.C1Input.C1Button();
            this.chkRawDataMode = new C1.Win.C1Input.C1CheckBox();
            this.chkShowColumnType = new C1.Win.C1Input.C1CheckBox();
            this.cboResultCopyFieldSeparator = new C1.Win.C1Input.C1ComboBox();
            this.cboResultCopyQuotingWith = new C1.Win.C1Input.C1ComboBox();
            this.cboFindGrid = new C1.Win.C1Input.C1ComboBox();
            this.chkSort = new C1.Win.C1Input.C1CheckBox();
            this.chkSize = new C1.Win.C1Input.C1CheckBox();
            this.chkShowFilterRow = new C1.Win.C1Input.C1CheckBox();
            this.c1DockingTab1 = new C1.Win.C1Command.C1DockingTab();
            this.tabMessage = new C1.Win.C1Command.C1DockingTabPage();
            this.editorMessage = new JasonLibrary.ScintillaEditor();
            this.tabSQLHistory = new C1.Win.C1Command.C1DockingTabPage();
            this.editorSQLHistory = new JasonLibrary.ScintillaEditor();
            this.tabDataGrid = new C1.Win.C1Command.C1DockingTabPage();
            this.c1TrueDBGrid1 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.lblInfo = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblAverage = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblAverageValue = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblSeparator1 = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblCount = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblCountValue = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblSeparator2 = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblSummary = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblSummaryValue = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblSeparator3 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblNotCommitYet = new C1.Win.C1Ribbon.RibbonLabel();
            this.sepNotCommitYet = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblExecTime = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonSeparator2 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblQueryTime = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonSeparator5 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblRows = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonSeparator3 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.btnPaginationOn = new C1.Win.C1Ribbon.RibbonButton();
            this.btnPaginationOff = new C1.Win.C1Ribbon.RibbonButton();
            this.btnAppendingQueriesOn = new C1.Win.C1Ribbon.RibbonButton();
            this.btnAppendingQueriesOff = new C1.Win.C1Ribbon.RibbonButton();
            this.btnNextPage = new C1.Win.C1Ribbon.RibbonButton();
            this.tsDataGrid = new System.Windows.Forms.ToolStrip();
            this.btnExportToFile = new System.Windows.Forms.ToolStripButton();
            this.btnExportToCSV = new System.Windows.Forms.ToolStripButton();
            this.btnFreezeColumn = new System.Windows.Forms.ToolStripButton();
            this.btnShowSQL = new System.Windows.Forms.ToolStripButton();
            this.btnAutoSize = new System.Windows.Forms.ToolStripButton();
            this.btnAutoSort = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.lblSpace = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.lblFindGrid = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.btnFindNextGrid = new System.Windows.Forms.ToolStripButton();
            this.btnFindPreviousGrid = new System.Windows.Forms.ToolStripButton();
            this.btnCountGrid = new System.Windows.Forms.ToolStripButton();
            this.btnHighlightAllGrid = new System.Windows.Forms.ToolStripButton();
            this.btnClearHighlightsGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnOptions = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuResultCopyQuotingWith = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuResultCopyQuotingWithNone = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuResultCopyQuotingWithDoubleQuoting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuResultCopyQuotingWithSingleQuoting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuResultCopyFieldSeparator = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuResultCopyFieldSeparatorComma = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuResultCopyFieldSeparatorSemicolon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuResultCopyFieldSeparatorI = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPreviewCLOBData = new System.Windows.Forms.ToolStripMenuItem();
            this.lblResultCopyQuotingWith = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.lblResultCopyFieldSeparator = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.c1GridAC4Space2 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1GridAC4Space1 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1GridAC4Period2 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.tmrlblInfo = new System.Windows.Forms.Timer(this.components);
            this.tmrMother2Child = new System.Windows.Forms.Timer(this.components);
            this.c1ThemeController1 = new C1.Win.C1Themes.C1ThemeController();
            this.c1GridAC4All = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1GridAC4Period1 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.tmrExecTime = new System.Windows.Forms.Timer(this.components);
            this.tmrlblInfoEditor = new System.Windows.Forms.Timer(this.components);
            this.tmrQueryTime = new System.Windows.Forms.Timer(this.components);
            this.c1CommandHolder1 = new C1.Win.C1Command.C1CommandHolder();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtIndentWord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab2)).BeginInit();
            this.c1DockingTab2.SuspendLayout();
            this.tabAutoReplace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridARInfo)).BeginInit();
            this.tsAutoReplace.SuspendLayout();
            this.tabSchemaBrowser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSchemaFilter)).BeginInit();
            this.tsSchemaBrowser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridSchemaBrowser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar2)).BeginInit();
            this.tsEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowGroupingRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_RawDataMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRawDataMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowColumnType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResultCopyFieldSeparator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResultCopyQuotingWith)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFindGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilterRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.tabMessage.SuspendLayout();
            this.tabSQLHistory.SuspendLayout();
            this.tabDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            this.tsDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridAC4Space2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridAC4Space1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridAC4Period2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridAC4All)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridAC4Period1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtIndentWord);
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Controls.Add(this.tsEditor);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chkShowGroupingRow);
            this.splitContainer1.Panel2.Controls.Add(this.btnHelp_RawDataMode);
            this.splitContainer1.Panel2.Controls.Add(this.chkRawDataMode);
            this.splitContainer1.Panel2.Controls.Add(this.chkShowColumnType);
            this.splitContainer1.Panel2.Controls.Add(this.cboResultCopyFieldSeparator);
            this.splitContainer1.Panel2.Controls.Add(this.cboResultCopyQuotingWith);
            this.splitContainer1.Panel2.Controls.Add(this.cboFindGrid);
            this.splitContainer1.Panel2.Controls.Add(this.chkSort);
            this.splitContainer1.Panel2.Controls.Add(this.chkSize);
            this.splitContainer1.Panel2.Controls.Add(this.chkShowFilterRow);
            this.splitContainer1.Panel2.Controls.Add(this.c1DockingTab1);
            this.splitContainer1.Panel2.Controls.Add(this.c1StatusBar1);
            this.splitContainer1.Panel2.Controls.Add(this.tsDataGrid);
            this.splitContainer1.Size = new System.Drawing.Size(1159, 557);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 0;
            this.c1ThemeController1.SetTheme(this.splitContainer1, "(default)");
            this.splitContainer1.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.splitContainer1_SplitterMoving);
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            this.splitContainer1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.splitContainer1_KeyDown);
            this.splitContainer1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.splitContainer1_KeyPress);
            // 
            // txtIndentWord
            // 
            this.txtIndentWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.txtIndentWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIndentWord.Location = new System.Drawing.Point(436, 5);
            this.txtIndentWord.MaxLength = 1;
            this.txtIndentWord.Name = "txtIndentWord";
            this.txtIndentWord.Size = new System.Drawing.Size(15, 21);
            this.txtIndentWord.TabIndex = 5;
            this.txtIndentWord.Tag = null;
            this.txtIndentWord.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIndentWord.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.txtIndentWord, "(default)");
            this.txtIndentWord.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtIndentWord.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtIndentWord_MouseClick);
            this.txtIndentWord.Enter += new System.EventHandler(this.txtIndentWord_Enter);
            this.txtIndentWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIndentWord_KeyDown);
            this.txtIndentWord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIndentWord_KeyPress);
            this.txtIndentWord.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtIndentWord_KeyUp);
            this.txtIndentWord.Leave += new System.EventHandler(this.txtIndentWord_Leave);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 31);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.c1DockingTab2);
            this.splitContainer2.Panel1MinSize = 49;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.editor);
            this.splitContainer2.Panel2.Controls.Add(this.lblInfoEditor);
            this.splitContainer2.Panel2.Controls.Add(this.c1StatusBar2);
            this.splitContainer2.Panel2MinSize = 200;
            this.splitContainer2.Size = new System.Drawing.Size(1159, 204);
            this.splitContainer2.SplitterDistance = 400;
            this.splitContainer2.SplitterWidth = 2;
            this.splitContainer2.TabIndex = 3;
            this.c1ThemeController1.SetTheme(this.splitContainer2, "(default)");
            this.splitContainer2.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.splitContainer2_SplitterMoving);
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            this.splitContainer2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitContainer2_MouseMove);
            // 
            // c1DockingTab2
            // 
            this.c1DockingTab2.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.c1DockingTab2.Controls.Add(this.tabAutoReplace);
            this.c1DockingTab2.Controls.Add(this.tabSchemaBrowser);
            this.c1DockingTab2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1DockingTab2.Font = new System.Drawing.Font("微軟正黑體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1DockingTab2.Location = new System.Drawing.Point(0, 0);
            this.c1DockingTab2.MultiLine = true;
            this.c1DockingTab2.Name = "c1DockingTab2";
            this.c1DockingTab2.SelectedIndex = 3;
            this.c1DockingTab2.ShowToolTips = true;
            this.c1DockingTab2.Size = new System.Drawing.Size(398, 202);
            this.c1DockingTab2.TabIndex = 67;
            this.c1DockingTab2.TabsSpacing = 0;
            this.c1DockingTab2.TextDirection = C1.Win.C1Command.TabTextDirectionEnum.VerticalLeft;
            this.c1ThemeController1.SetTheme(this.c1DockingTab2, "(default)");
            this.c1DockingTab2.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.c1DockingTab2.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.c1DockingTab2.TabClick += new System.EventHandler(this.c1DockingTab2_TabClick);
            this.c1DockingTab2.Enter += new System.EventHandler(this.c1DockingTab2_Enter);
            this.c1DockingTab2.Leave += new System.EventHandler(this.c1DockingTab2_Leave);
            // 
            // tabAutoReplace
            // 
            this.tabAutoReplace.Controls.Add(this.c1GridARInfo);
            this.tabAutoReplace.Controls.Add(this.tsAutoReplace);
            this.tabAutoReplace.Location = new System.Drawing.Point(26, 1);
            this.tabAutoReplace.Name = "tabAutoReplace";
            this.tabAutoReplace.Size = new System.Drawing.Size(371, 200);
            this.tabAutoReplace.TabIndex = 0;
            this.tabAutoReplace.Text = "Auto Replace";
            // 
            // c1GridARInfo
            // 
            this.c1GridARInfo.AllowUpdateOnBlur = false;
            this.c1GridARInfo.AlternatingRows = true;
            this.c1GridARInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1GridARInfo.CaptionHeight = 19;
            this.c1GridARInfo.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1GridARInfo.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1GridARInfo.Images.Add(((System.Drawing.Image)(resources.GetObject("c1GridARInfo.Images"))));
            this.c1GridARInfo.Location = new System.Drawing.Point(1, 24);
            this.c1GridARInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.c1GridARInfo.Name = "c1GridARInfo";
            this.c1GridARInfo.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1GridARInfo.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1GridARInfo.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1GridARInfo.PreviewInfo.ZoomFactor = 75D;
            this.c1GridARInfo.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1GridARInfo.PrintInfo.MeasurementPrinterName = null;
            this.c1GridARInfo.RowHeight = 17;
            this.c1GridARInfo.Size = new System.Drawing.Size(369, 175);
            this.c1GridARInfo.TabIndex = 65;
            this.c1GridARInfo.UseCompatibleTextRendering = false;
            this.c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
            this.c1GridARInfo.AfterUpdate += new System.EventHandler(this.c1GridARInfo_AfterUpdate);
            this.c1GridARInfo.Enter += new System.EventHandler(this.c1GridARInfo_Enter);
            this.c1GridARInfo.Leave += new System.EventHandler(this.c1GridARInfo_Leave);
            this.c1GridARInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1GridARInfo_MouseMove);
            this.c1GridARInfo.PropBag = resources.GetString("c1GridARInfo.PropBag");
            // 
            // tsAutoReplace
            // 
            this.tsAutoReplace.BackColor = System.Drawing.Color.Transparent;
            this.tsAutoReplace.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsAutoReplace.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblAutoReplace,
            this.btnHelp});
            this.tsAutoReplace.Location = new System.Drawing.Point(0, 0);
            this.tsAutoReplace.Name = "tsAutoReplace";
            this.tsAutoReplace.Size = new System.Drawing.Size(371, 25);
            this.tsAutoReplace.TabIndex = 67;
            this.tsAutoReplace.Text = "toolStrip1";
            // 
            // lblAutoReplace
            // 
            this.lblAutoReplace.Name = "lblAutoReplace";
            this.lblAutoReplace.Size = new System.Drawing.Size(83, 22);
            this.lblAutoReplace.Text = "Auto Replace";
            // 
            // btnHelp
            // 
            this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(23, 22);
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // tabSchemaBrowser
            // 
            this.tabSchemaBrowser.Controls.Add(this.lblSchemaFilter0);
            this.tabSchemaBrowser.Controls.Add(this.txtSchemaFilter);
            this.tabSchemaBrowser.Controls.Add(this.tsSchemaBrowser);
            this.tabSchemaBrowser.Controls.Add(this.c1GridSchemaBrowser);
            this.tabSchemaBrowser.Location = new System.Drawing.Point(26, 1);
            this.tabSchemaBrowser.Name = "tabSchemaBrowser";
            this.tabSchemaBrowser.Size = new System.Drawing.Size(371, 200);
            this.tabSchemaBrowser.TabIndex = 1;
            this.tabSchemaBrowser.Text = "Schema Browser";
            // 
            // lblSchemaFilter0
            // 
            this.lblSchemaFilter0.AutoSize = true;
            this.lblSchemaFilter0.BackColor = System.Drawing.Color.Transparent;
            this.lblSchemaFilter0.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSchemaFilter0.Location = new System.Drawing.Point(70, 14);
            this.lblSchemaFilter0.Name = "lblSchemaFilter0";
            this.lblSchemaFilter0.Size = new System.Drawing.Size(14, 16);
            this.lblSchemaFilter0.TabIndex = 95;
            this.lblSchemaFilter0.Text = "F";
            this.lblSchemaFilter0.Visible = false;
            // 
            // txtSchemaFilter
            // 
            this.txtSchemaFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.txtSchemaFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSchemaFilter.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtSchemaFilter.Location = new System.Drawing.Point(134, 2);
            this.txtSchemaFilter.Name = "txtSchemaFilter";
            this.txtSchemaFilter.Size = new System.Drawing.Size(121, 20);
            this.txtSchemaFilter.TabIndex = 89;
            this.txtSchemaFilter.Tag = null;
            this.txtSchemaFilter.Text = "*";
            this.txtSchemaFilter.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.txtSchemaFilter, "(default)");
            this.txtSchemaFilter.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtSchemaFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSchemaFilter_KeyDown);
            // 
            // tsSchemaBrowser
            // 
            this.tsSchemaBrowser.BackColor = System.Drawing.Color.Transparent;
            this.tsSchemaBrowser.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsSchemaBrowser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExpandCollapse,
            this.toolStripSeparator15,
            this.btnSettingOfFocus,
            this.lblSchemaFilter});
            this.tsSchemaBrowser.Location = new System.Drawing.Point(0, 0);
            this.tsSchemaBrowser.Name = "tsSchemaBrowser";
            this.tsSchemaBrowser.Size = new System.Drawing.Size(371, 25);
            this.tsSchemaBrowser.TabIndex = 7;
            this.tsSchemaBrowser.Text = "toolStrip1";
            // 
            // btnExpandCollapse
            // 
            this.btnExpandCollapse.BackColor = System.Drawing.Color.Transparent;
            this.btnExpandCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExpandCollapse.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExpandAll,
            this.mnuCollapseAll});
            this.btnExpandCollapse.Image = ((System.Drawing.Image)(resources.GetObject("btnExpandCollapse.Image")));
            this.btnExpandCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExpandCollapse.Name = "btnExpandCollapse";
            this.btnExpandCollapse.Size = new System.Drawing.Size(32, 22);
            this.btnExpandCollapse.ButtonClick += new System.EventHandler(this.btnExpandCollapse_ButtonClick);
            // 
            // mnuExpandAll
            // 
            this.mnuExpandAll.Name = "mnuExpandAll";
            this.mnuExpandAll.Size = new System.Drawing.Size(140, 22);
            this.mnuExpandAll.Tag = "mnuExpandAll";
            this.mnuExpandAll.Text = "Expand All";
            this.mnuExpandAll.Click += new System.EventHandler(this.mnuExpandAll_Click);
            // 
            // mnuCollapseAll
            // 
            this.mnuCollapseAll.Name = "mnuCollapseAll";
            this.mnuCollapseAll.Size = new System.Drawing.Size(140, 22);
            this.mnuCollapseAll.Tag = "mnuCollapseAll";
            this.mnuCollapseAll.Text = "Collapse All";
            this.mnuCollapseAll.Click += new System.EventHandler(this.mnuCollapseAll_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSettingOfFocus
            // 
            this.btnSettingOfFocus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSettingOfFocus.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFocusOnDataGrid,
            this.mnuFocusOnQueryEditor});
            this.btnSettingOfFocus.Image = ((System.Drawing.Image)(resources.GetObject("btnSettingOfFocus.Image")));
            this.btnSettingOfFocus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettingOfFocus.Name = "btnSettingOfFocus";
            this.btnSettingOfFocus.Size = new System.Drawing.Size(32, 22);
            this.btnSettingOfFocus.Text = "toolStripSplitButton1";
            this.btnSettingOfFocus.ButtonClick += new System.EventHandler(this.btnSettingOfFocus_ButtonClick);
            // 
            // mnuFocusOnDataGrid
            // 
            this.mnuFocusOnDataGrid.Name = "mnuFocusOnDataGrid";
            this.mnuFocusOnDataGrid.Size = new System.Drawing.Size(364, 22);
            this.mnuFocusOnDataGrid.Text = "After Pasting to Query Editor, Focus on Data Grid";
            this.mnuFocusOnDataGrid.Click += new System.EventHandler(this.mnuFocusOnDataGrid_Click);
            // 
            // mnuFocusOnQueryEditor
            // 
            this.mnuFocusOnQueryEditor.Name = "mnuFocusOnQueryEditor";
            this.mnuFocusOnQueryEditor.Size = new System.Drawing.Size(364, 22);
            this.mnuFocusOnQueryEditor.Text = "After Pasting to Query Editor, Focus on Query Editor";
            this.mnuFocusOnQueryEditor.Click += new System.EventHandler(this.mnuFocusOnQueryEditor_Click);
            // 
            // lblSchemaFilter
            // 
            this.lblSchemaFilter.Name = "lblSchemaFilter";
            this.lblSchemaFilter.Size = new System.Drawing.Size(37, 22);
            this.lblSchemaFilter.Text = "Filter:";
            // 
            // c1GridSchemaBrowser
            // 
            this.c1GridSchemaBrowser.AllowUpdate = false;
            this.c1GridSchemaBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1GridSchemaBrowser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1GridSchemaBrowser.DataView = C1.Win.C1TrueDBGrid.DataViewEnum.GroupBy;
            this.c1GridSchemaBrowser.FetchRowStyles = true;
            this.c1GridSchemaBrowser.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1GridSchemaBrowser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1GridSchemaBrowser.GroupByAreaVisible = false;
            this.c1GridSchemaBrowser.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1GridSchemaBrowser.Images.Add(((System.Drawing.Image)(resources.GetObject("c1GridSchemaBrowser.Images"))));
            this.c1GridSchemaBrowser.Location = new System.Drawing.Point(1, 24);
            this.c1GridSchemaBrowser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.c1GridSchemaBrowser.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.None;
            this.c1GridSchemaBrowser.Name = "c1GridSchemaBrowser";
            this.c1GridSchemaBrowser.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1GridSchemaBrowser.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1GridSchemaBrowser.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1GridSchemaBrowser.PreviewInfo.ZoomFactor = 75D;
            this.c1GridSchemaBrowser.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1GridSchemaBrowser.PrintInfo.MeasurementPrinterName = null;
            this.c1GridSchemaBrowser.RowHeight = 19;
            this.c1GridSchemaBrowser.Size = new System.Drawing.Size(369, 175);
            this.c1GridSchemaBrowser.TabIndex = 6;
            this.c1ThemeController1.SetTheme(this.c1GridSchemaBrowser, "(default)");
            this.c1GridSchemaBrowser.UseCompatibleTextRendering = false;
            this.c1GridSchemaBrowser.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.c1GridSchemaBrowser_FetchRowStyle);
            this.c1GridSchemaBrowser.Enter += new System.EventHandler(this.c1GridSchemaBrowser_Enter);
            this.c1GridSchemaBrowser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1GridSchemaBrowser_KeyDown);
            this.c1GridSchemaBrowser.Leave += new System.EventHandler(this.c1GridSchemaBrowser_Leave);
            this.c1GridSchemaBrowser.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1GridSchemaBrowser_MouseDoubleClick);
            this.c1GridSchemaBrowser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1GridSchemaBrowser_MouseDown);
            this.c1GridSchemaBrowser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1GridSchemaBrowser_MouseMove);
            this.c1GridSchemaBrowser.MouseUp += new System.Windows.Forms.MouseEventHandler(this.c1GridSchemaBrowser_MouseUp);
            this.c1GridSchemaBrowser.PropBag = resources.GetString("c1GridSchemaBrowser.PropBag");
            // 
            // editor
            // 
            this.editor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editor.CaretLineBackColor = System.Drawing.Color.LightYellow;
            this.editor.CaretLineVisible = true;
            this.editor.EndAtLastLine = false;
            this.editor.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.editor.Location = new System.Drawing.Point(-1, -1);
            this.editor.Name = "editor";
            this.editor.ScrollWidth = 400;
            this.editor.SelectionEolFilled = true;
            this.editor.Size = new System.Drawing.Size(755, 181);
            this.editor.Styler = null;
            this.editor.TabIndex = 1;
            this.editor.WhitespaceSize = 3;
            this.editor.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.editor.DoubleClick += new System.EventHandler<ScintillaNET.DoubleClickEventArgs>(this.editor_DoubleClick);
            this.editor.Insert += new System.EventHandler<ScintillaNET.ModificationEventArgs>(this.editor_Insert);
            this.editor.UpdateUI += new System.EventHandler<ScintillaNET.UpdateUIEventArgs>(this.editor_UpdateUI);
            this.editor.ZoomChanged += new System.EventHandler<System.EventArgs>(this.editor_ZoomChanged);
            this.editor.Enter += new System.EventHandler(this.editor_Enter);
            this.editor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editor_KeyDown);
            this.editor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editor_KeyPress);
            this.editor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.editor_KeyUp);
            this.editor.Leave += new System.EventHandler(this.editor_Leave);
            this.editor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.editor_MouseDown);
            this.editor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editor_MouseDown);
            this.editor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.editor_MouseMove);
            // 
            // lblInfoEditor
            // 
            this.lblInfoEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfoEditor.AutoSize = true;
            this.lblInfoEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInfoEditor.Location = new System.Drawing.Point(2, 184);
            this.lblInfoEditor.Margin = new System.Windows.Forms.Padding(0);
            this.lblInfoEditor.Name = "lblInfoEditor";
            this.lblInfoEditor.Size = new System.Drawing.Size(0, 16);
            this.lblInfoEditor.TabIndex = 82;
            this.c1ThemeController1.SetTheme(this.lblInfoEditor, "(default)");
            // 
            // c1StatusBar2
            // 
            this.c1StatusBar2.Location = new System.Drawing.Point(0, 180);
            this.c1StatusBar2.Name = "c1StatusBar2";
            this.c1StatusBar2.RightPaneItems.Add(this.btnDatabase);
            this.c1StatusBar2.RightPaneItems.Add(this.spDatabase);
            this.c1StatusBar2.RightPaneItems.Add(this.lblEditorLength);
            this.c1StatusBar2.RightPaneItems.Add(this.ribbonLabel2);
            this.c1StatusBar2.RightPaneItems.Add(this.lblEditorLines);
            this.c1StatusBar2.RightPaneItems.Add(this.ribbonSeparator1);
            this.c1StatusBar2.RightPaneItems.Add(this.lblEditorLn);
            this.c1StatusBar2.RightPaneItems.Add(this.ribbonLabel3);
            this.c1StatusBar2.RightPaneItems.Add(this.lblEditorCol);
            this.c1StatusBar2.RightPaneItems.Add(this.ribbonLabel4);
            this.c1StatusBar2.RightPaneItems.Add(this.lblEditorPos);
            this.c1StatusBar2.RightPaneItems.Add(this.ribbonLabel5);
            this.c1StatusBar2.RightPaneItems.Add(this.lblEditorSel);
            this.c1StatusBar2.RightPaneItems.Add(this.ribbonSeparator7);
            this.c1StatusBar2.RightPaneItems.Add(this.lblEndOfLineStyle);
            this.c1StatusBar2.RightPaneItems.Add(this.ribbonSeparator8);
            this.c1StatusBar2.RightPaneItems.Add(this.lblEncode);
            this.c1StatusBar2.RightPaneItems.Add(this.lblTemp);
            this.c1StatusBar2.ShowDropDownsOnTop = false;
            this.c1StatusBar2.Size = new System.Drawing.Size(755, 22);
            this.c1StatusBar2.SizingGrip = false;
            this.c1ThemeController1.SetTheme(this.c1StatusBar2, "(default)");
            this.c1StatusBar2.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Windows7;
            this.c1StatusBar2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1StatusBar2_MouseMove);
            // 
            // btnDatabase
            // 
            this.btnDatabase.Name = "btnDatabase";
            this.btnDatabase.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnDatabase.SmallImage")));
            this.btnDatabase.Visible = false;
            // 
            // spDatabase
            // 
            this.spDatabase.Name = "spDatabase";
            this.spDatabase.Visible = false;
            // 
            // lblEditorLength
            // 
            this.lblEditorLength.Name = "lblEditorLength";
            this.lblEditorLength.Text = "Length:";
            // 
            // ribbonLabel2
            // 
            this.ribbonLabel2.Name = "ribbonLabel2";
            // 
            // lblEditorLines
            // 
            this.lblEditorLines.Name = "lblEditorLines";
            this.lblEditorLines.Text = "Lines:";
            // 
            // ribbonSeparator1
            // 
            this.ribbonSeparator1.Name = "ribbonSeparator1";
            // 
            // lblEditorLn
            // 
            this.lblEditorLn.Name = "lblEditorLn";
            this.lblEditorLn.Text = "Ln:";
            // 
            // ribbonLabel3
            // 
            this.ribbonLabel3.Name = "ribbonLabel3";
            // 
            // lblEditorCol
            // 
            this.lblEditorCol.Name = "lblEditorCol";
            this.lblEditorCol.Text = "Col:";
            // 
            // ribbonLabel4
            // 
            this.ribbonLabel4.Name = "ribbonLabel4";
            // 
            // lblEditorPos
            // 
            this.lblEditorPos.Name = "lblEditorPos";
            this.lblEditorPos.Text = "Pos:";
            // 
            // ribbonLabel5
            // 
            this.ribbonLabel5.Name = "ribbonLabel5";
            // 
            // lblEditorSel
            // 
            this.lblEditorSel.Name = "lblEditorSel";
            this.lblEditorSel.Text = "Sel:";
            // 
            // ribbonSeparator7
            // 
            this.ribbonSeparator7.Name = "ribbonSeparator7";
            // 
            // lblEndOfLineStyle
            // 
            this.lblEndOfLineStyle.Name = "lblEndOfLineStyle";
            this.lblEndOfLineStyle.Text = "Windows (CR LF)";
            // 
            // ribbonSeparator8
            // 
            this.ribbonSeparator8.Name = "ribbonSeparator8";
            // 
            // lblEncode
            // 
            this.lblEncode.Name = "lblEncode";
            this.lblEncode.Text = "UTF-8";
            // 
            // lblTemp
            // 
            this.lblTemp.Name = "lblTemp";
            // 
            // tsEditor
            // 
            this.tsEditor.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsEditor.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.btnSaveRed,
            this.btnSaveAs,
            this.toolStripSeparator5,
            this.btnQuery,
            this.btnSelectCurrentBlock,
            this.btnExecuteCurrentBlock,
            this.btnCancelQuery,
            this.btnCommit,
            this.btnRollback,
            this.toolStripSeparator4,
            this.btnCode2SQL,
            this.btnSQL2Code,
            this.toolStripSeparator9,
            this.btnLeftAndRight,
            this.btnUpAndDown,
            this.toolStripSeparator3,
            this.btnComment,
            this.btnRemoveComment,
            this.toolStripSeparator6,
            this.btnIndent,
            this.toolStripLabel4,
            this.txtIndentWord2,
            this.btnUnIndent,
            this.toolStripSeparator8,
            this.btnHighlightSelection,
            this.btnHighlightSelection2,
            this.toolStripSeparator1,
            this.btnWordWrap,
            this.btnWordWrap2,
            this.btnShowAllCharacters,
            this.btnShowAllCharacters2,
            this.btnShowIndentGuide,
            this.btnShowIndentGuide2,
            this.tsSeparator});
            this.tsEditor.Location = new System.Drawing.Point(0, 0);
            this.tsEditor.Name = "tsEditor";
            this.tsEditor.Size = new System.Drawing.Size(1159, 31);
            this.tsEditor.TabIndex = 2;
            this.c1ThemeController1.SetTheme(this.tsEditor, "(default)");
            this.tsEditor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tsEditor_MouseMove);
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(28, 28);
            this.btnNew.Tag = "";
            this.btnNew.ToolTipText = "Open New SQL Editor";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(28, 28);
            this.btnOpen.ToolTipText = "Open Query file";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(28, 28);
            this.btnSave.ToolTipText = "Save (Ctrl+S)\r\nSave as Encoding: UTF8";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveRed
            // 
            this.btnSaveRed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveRed.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveRed.Image")));
            this.btnSaveRed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveRed.Name = "btnSaveRed";
            this.btnSaveRed.Size = new System.Drawing.Size(28, 28);
            this.btnSaveRed.ToolTipText = "Save (Ctrl+S)\r\nSave as Encoding: UTF8";
            this.btnSaveRed.Visible = false;
            this.btnSaveRed.Click += new System.EventHandler(this.btnSaveRed_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAs.Image")));
            this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(28, 28);
            this.btnSaveAs.ToolTipText = "Save As (F12)\r\nSave as Encoding: UTF8";
            this.btnSaveAs.Visible = false;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // btnQuery
            // 
            this.btnQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnQuery.Enabled = false;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(28, 28);
            this.btnQuery.Tag = "";
            this.btnQuery.ToolTipText = "Execute Statement (F5)\r\n(Selected or All)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnSelectCurrentBlock
            // 
            this.btnSelectCurrentBlock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectCurrentBlock.Enabled = false;
            this.btnSelectCurrentBlock.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectCurrentBlock.Image")));
            this.btnSelectCurrentBlock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectCurrentBlock.Name = "btnSelectCurrentBlock";
            this.btnSelectCurrentBlock.Size = new System.Drawing.Size(28, 28);
            this.btnSelectCurrentBlock.ToolTipText = "Select Current Block (Ctrl+B)";
            this.btnSelectCurrentBlock.Click += new System.EventHandler(this.btnSelectCurrentBlock_Click);
            // 
            // btnExecuteCurrentBlock
            // 
            this.btnExecuteCurrentBlock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExecuteCurrentBlock.Enabled = false;
            this.btnExecuteCurrentBlock.Image = ((System.Drawing.Image)(resources.GetObject("btnExecuteCurrentBlock.Image")));
            this.btnExecuteCurrentBlock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExecuteCurrentBlock.Name = "btnExecuteCurrentBlock";
            this.btnExecuteCurrentBlock.Size = new System.Drawing.Size(28, 28);
            this.btnExecuteCurrentBlock.ToolTipText = "Execute Current Block (Ctrl+Enter)";
            this.btnExecuteCurrentBlock.Click += new System.EventHandler(this.btnExecuteCurrentBlock_Click);
            // 
            // btnCancelQuery
            // 
            this.btnCancelQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelQuery.Enabled = false;
            this.btnCancelQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelQuery.Image")));
            this.btnCancelQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelQuery.Name = "btnCancelQuery";
            this.btnCancelQuery.Size = new System.Drawing.Size(28, 28);
            this.btnCancelQuery.Tag = "";
            this.btnCancelQuery.ToolTipText = "Cancel Query";
            this.btnCancelQuery.Click += new System.EventHandler(this.btnCancelQuery_Click);
            // 
            // btnCommit
            // 
            this.btnCommit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCommit.Enabled = false;
            this.btnCommit.Image = ((System.Drawing.Image)(resources.GetObject("btnCommit.Image")));
            this.btnCommit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(28, 28);
            this.btnCommit.Tag = "N";
            this.btnCommit.Text = "toolStripButton1";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // btnRollback
            // 
            this.btnRollback.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRollback.Enabled = false;
            this.btnRollback.Image = ((System.Drawing.Image)(resources.GetObject("btnRollback.Image")));
            this.btnRollback.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Size = new System.Drawing.Size(28, 28);
            this.btnRollback.Tag = "N";
            this.btnRollback.Text = "toolStripButton2";
            this.btnRollback.Click += new System.EventHandler(this.btnRollback_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // btnCode2SQL
            // 
            this.btnCode2SQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCode2SQL.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCSharp2SQL,
            this.mnuVB2SQL,
            this.mnuDephi2SQL});
            this.btnCode2SQL.Enabled = false;
            this.btnCode2SQL.Image = ((System.Drawing.Image)(resources.GetObject("btnCode2SQL.Image")));
            this.btnCode2SQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCode2SQL.Name = "btnCode2SQL";
            this.btnCode2SQL.Size = new System.Drawing.Size(37, 28);
            this.btnCode2SQL.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCode2SQL.ToolTipText = "Code to SQL\r\n(Strip SQL from non-SQL code)";
            this.btnCode2SQL.Click += new System.EventHandler(this.btnCode2SQL_Click);
            // 
            // mnuCSharp2SQL
            // 
            this.mnuCSharp2SQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuCSharp2SQL.Name = "mnuCSharp2SQL";
            this.mnuCSharp2SQL.Size = new System.Drawing.Size(209, 22);
            this.mnuCSharp2SQL.Text = "C# to SQL";
            this.mnuCSharp2SQL.Click += new System.EventHandler(this.mnuCSharp2SQL_Click);
            // 
            // mnuVB2SQL
            // 
            this.mnuVB2SQL.Name = "mnuVB2SQL";
            this.mnuVB2SQL.Size = new System.Drawing.Size(209, 22);
            this.mnuVB2SQL.Text = "VB.Net/VB6/VBA to SQL";
            this.mnuVB2SQL.Click += new System.EventHandler(this.mnuVB2SQL_Click);
            // 
            // mnuDephi2SQL
            // 
            this.mnuDephi2SQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuDephi2SQL.Name = "mnuDephi2SQL";
            this.mnuDephi2SQL.Size = new System.Drawing.Size(209, 22);
            this.mnuDephi2SQL.Text = "Dephi6 to SQL";
            this.mnuDephi2SQL.Click += new System.EventHandler(this.mnuDelphi2SQL_Click);
            // 
            // btnSQL2Code
            // 
            this.btnSQL2Code.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSQL2Code.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSQL2CSharp,
            this.mnuSQL2VBNet,
            this.mnuSQL2VB6A,
            this.mnuSQL2Delphi});
            this.btnSQL2Code.Enabled = false;
            this.btnSQL2Code.Image = ((System.Drawing.Image)(resources.GetObject("btnSQL2Code.Image")));
            this.btnSQL2Code.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSQL2Code.Name = "btnSQL2Code";
            this.btnSQL2Code.Size = new System.Drawing.Size(37, 28);
            this.btnSQL2Code.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSQL2Code.ToolTipText = "SQL to Code\r\n(Make non-SQL code statement from SQL)";
            this.btnSQL2Code.Click += new System.EventHandler(this.btnSQL2Code_Click);
            // 
            // mnuSQL2CSharp
            // 
            this.mnuSQL2CSharp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuSQL2CSharp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCSharpStyle1,
            this.mnuCSharpStyle2,
            this.mnuCSharpStyle3});
            this.mnuSQL2CSharp.Name = "mnuSQL2CSharp";
            this.mnuSQL2CSharp.Size = new System.Drawing.Size(165, 22);
            this.mnuSQL2CSharp.Text = "SQL to C#";
            // 
            // mnuCSharpStyle1
            // 
            this.mnuCSharpStyle1.Name = "mnuCSharpStyle1";
            this.mnuCSharpStyle1.Size = new System.Drawing.Size(340, 22);
            this.mnuCSharpStyle1.Tag = "C#`1";
            this.mnuCSharpStyle1.Text = "Style 1: Using + operator";
            this.mnuCSharpStyle1.Click += new System.EventHandler(this.mnuSQLToCode_Click);
            // 
            // mnuCSharpStyle2
            // 
            this.mnuCSharpStyle2.Name = "mnuCSharpStyle2";
            this.mnuCSharpStyle2.Size = new System.Drawing.Size(340, 22);
            this.mnuCSharpStyle2.Tag = "C#`2";
            this.mnuCSharpStyle2.Text = "Style 2: New line character \\r\\n";
            this.mnuCSharpStyle2.Click += new System.EventHandler(this.mnuSQLToCode_Click);
            // 
            // mnuCSharpStyle3
            // 
            this.mnuCSharpStyle3.Name = "mnuCSharpStyle3";
            this.mnuCSharpStyle3.Size = new System.Drawing.Size(340, 22);
            this.mnuCSharpStyle3.Tag = "C#`3";
            this.mnuCSharpStyle3.Text = "Style 3: New line character Enviroment.NewLine";
            this.mnuCSharpStyle3.Click += new System.EventHandler(this.mnuSQLToCode_Click);
            // 
            // mnuSQL2VBNet
            // 
            this.mnuSQL2VBNet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuVBNetStyle1,
            this.mnuVBNetStyle2,
            this.mnuVBNetStyle3});
            this.mnuSQL2VBNet.Name = "mnuSQL2VBNet";
            this.mnuSQL2VBNet.Size = new System.Drawing.Size(165, 22);
            this.mnuSQL2VBNet.Text = "SQL to VB.Net";
            // 
            // mnuVBNetStyle1
            // 
            this.mnuVBNetStyle1.Name = "mnuVBNetStyle1";
            this.mnuVBNetStyle1.Size = new System.Drawing.Size(340, 22);
            this.mnuVBNetStyle1.Tag = "VB.Net`1";
            this.mnuVBNetStyle1.Text = "Style 1: Using && operator";
            this.mnuVBNetStyle1.Click += new System.EventHandler(this.mnuSQLToCode_Click);
            // 
            // mnuVBNetStyle2
            // 
            this.mnuVBNetStyle2.Name = "mnuVBNetStyle2";
            this.mnuVBNetStyle2.Size = new System.Drawing.Size(340, 22);
            this.mnuVBNetStyle2.Tag = "VB.Net`2";
            this.mnuVBNetStyle2.Text = "Style 2: New line character VbCrLf";
            this.mnuVBNetStyle2.Click += new System.EventHandler(this.mnuSQLToCode_Click);
            // 
            // mnuVBNetStyle3
            // 
            this.mnuVBNetStyle3.Name = "mnuVBNetStyle3";
            this.mnuVBNetStyle3.Size = new System.Drawing.Size(340, 22);
            this.mnuVBNetStyle3.Tag = "VB.Net`3";
            this.mnuVBNetStyle3.Text = "Style 3: New line character Enviroment.NewLine";
            this.mnuVBNetStyle3.Click += new System.EventHandler(this.mnuSQLToCode_Click);
            // 
            // mnuSQL2VB6A
            // 
            this.mnuSQL2VB6A.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuSQL2VB6A.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuVB6AStyle1,
            this.mnuVB6AStyle2});
            this.mnuSQL2VB6A.Name = "mnuSQL2VB6A";
            this.mnuSQL2VB6A.Size = new System.Drawing.Size(165, 22);
            this.mnuSQL2VB6A.Text = "SQL to VB6/VBA";
            // 
            // mnuVB6AStyle1
            // 
            this.mnuVB6AStyle1.Name = "mnuVB6AStyle1";
            this.mnuVB6AStyle1.Size = new System.Drawing.Size(262, 22);
            this.mnuVB6AStyle1.Tag = "VB6/VBA`1";
            this.mnuVB6AStyle1.Text = "Style 1: Using && operator";
            this.mnuVB6AStyle1.Click += new System.EventHandler(this.mnuSQLToCode_Click);
            // 
            // mnuVB6AStyle2
            // 
            this.mnuVB6AStyle2.Name = "mnuVB6AStyle2";
            this.mnuVB6AStyle2.Size = new System.Drawing.Size(262, 22);
            this.mnuVB6AStyle2.Tag = "VB6/VBA`2";
            this.mnuVB6AStyle2.Text = "Style 2: New line character VbCrLf";
            this.mnuVB6AStyle2.Click += new System.EventHandler(this.mnuSQLToCode_Click);
            // 
            // mnuSQL2Delphi
            // 
            this.mnuSQL2Delphi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDelphi6Style1,
            this.mnuDelphi6Style2});
            this.mnuSQL2Delphi.Name = "mnuSQL2Delphi";
            this.mnuSQL2Delphi.Size = new System.Drawing.Size(165, 22);
            this.mnuSQL2Delphi.Text = "SQL to Delphi6";
            // 
            // mnuDelphi6Style1
            // 
            this.mnuDelphi6Style1.Name = "mnuDelphi6Style1";
            this.mnuDelphi6Style1.Size = new System.Drawing.Size(268, 22);
            this.mnuDelphi6Style1.Tag = "Delphi6`1";
            this.mnuDelphi6Style1.Text = "Style 1: Using + operator";
            this.mnuDelphi6Style1.Click += new System.EventHandler(this.mnuSQLToCode_Click);
            // 
            // mnuDelphi6Style2
            // 
            this.mnuDelphi6Style2.Name = "mnuDelphi6Style2";
            this.mnuDelphi6Style2.Size = new System.Drawing.Size(268, 22);
            this.mnuDelphi6Style2.Tag = "Delphi6`2";
            this.mnuDelphi6Style2.Text = "Style 2: New line character #13#10";
            this.mnuDelphi6Style2.Click += new System.EventHandler(this.mnuSQLToCode_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 31);
            // 
            // btnLeftAndRight
            // 
            this.btnLeftAndRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLeftAndRight.Image = ((System.Drawing.Image)(resources.GetObject("btnLeftAndRight.Image")));
            this.btnLeftAndRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLeftAndRight.Name = "btnLeftAndRight";
            this.btnLeftAndRight.Size = new System.Drawing.Size(28, 28);
            this.btnLeftAndRight.Text = "LeftAndRight";
            this.btnLeftAndRight.ToolTipText = "Save SplitContainer\'s horizontal width";
            this.btnLeftAndRight.Visible = false;
            // 
            // btnUpAndDown
            // 
            this.btnUpAndDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUpAndDown.Image = ((System.Drawing.Image)(resources.GetObject("btnUpAndDown.Image")));
            this.btnUpAndDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpAndDown.Name = "btnUpAndDown";
            this.btnUpAndDown.Size = new System.Drawing.Size(28, 28);
            this.btnUpAndDown.Text = "UpAndDown";
            this.btnUpAndDown.ToolTipText = "Save SplitContainer\'s vertical height";
            this.btnUpAndDown.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            this.toolStripSeparator3.Visible = false;
            // 
            // btnComment
            // 
            this.btnComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnComment.Enabled = false;
            this.btnComment.Image = ((System.Drawing.Image)(resources.GetObject("btnComment.Image")));
            this.btnComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnComment.Name = "btnComment";
            this.btnComment.Size = new System.Drawing.Size(28, 28);
            this.btnComment.ToolTipText = "Comment";
            this.btnComment.Click += new System.EventHandler(this.btnComment_Click);
            // 
            // btnRemoveComment
            // 
            this.btnRemoveComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveComment.Enabled = false;
            this.btnRemoveComment.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveComment.Image")));
            this.btnRemoveComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveComment.Name = "btnRemoveComment";
            this.btnRemoveComment.Size = new System.Drawing.Size(28, 28);
            this.btnRemoveComment.ToolTipText = "Un-Comment";
            this.btnRemoveComment.Click += new System.EventHandler(this.btnRemoveComment_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // btnIndent
            // 
            this.btnIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIndent.Enabled = false;
            this.btnIndent.Image = ((System.Drawing.Image)(resources.GetObject("btnIndent.Image")));
            this.btnIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIndent.Name = "btnIndent";
            this.btnIndent.Size = new System.Drawing.Size(28, 28);
            this.btnIndent.ToolTipText = "Indent";
            this.btnIndent.Click += new System.EventHandler(this.btnIndent_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(19, 28);
            this.toolStripLabel4.Text = "    ";
            // 
            // txtIndentWord2
            // 
            this.txtIndentWord2.BackColor = System.Drawing.Color.Ivory;
            this.txtIndentWord2.MaxLength = 1;
            this.txtIndentWord2.Name = "txtIndentWord2";
            this.txtIndentWord2.Size = new System.Drawing.Size(15, 31);
            this.txtIndentWord2.Text = "4";
            this.txtIndentWord2.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIndentWord2.ToolTipText = "Indent / Un-Indent Width";
            this.txtIndentWord2.Visible = false;
            this.txtIndentWord2.Enter += new System.EventHandler(this.txtIndentWord_Enter);
            this.txtIndentWord2.Leave += new System.EventHandler(this.txtIndentWord_Leave);
            this.txtIndentWord2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIndentWord_KeyDown);
            this.txtIndentWord2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIndentWord_KeyPress);
            // 
            // btnUnIndent
            // 
            this.btnUnIndent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUnIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUnIndent.Enabled = false;
            this.btnUnIndent.Image = ((System.Drawing.Image)(resources.GetObject("btnUnIndent.Image")));
            this.btnUnIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnIndent.Name = "btnUnIndent";
            this.btnUnIndent.Size = new System.Drawing.Size(28, 28);
            this.btnUnIndent.ToolTipText = "Un-Indent";
            this.btnUnIndent.Click += new System.EventHandler(this.btnUnIndent_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 31);
            // 
            // btnHighlightSelection
            // 
            this.btnHighlightSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHighlightSelection.Image = ((System.Drawing.Image)(resources.GetObject("btnHighlightSelection.Image")));
            this.btnHighlightSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHighlightSelection.Name = "btnHighlightSelection";
            this.btnHighlightSelection.Size = new System.Drawing.Size(28, 28);
            this.btnHighlightSelection.ToolTipText = "Disable Highlight Selection When Mouse Click";
            this.btnHighlightSelection.Visible = false;
            this.btnHighlightSelection.Click += new System.EventHandler(this.btnHighlightSelection_Click);
            // 
            // btnHighlightSelection2
            // 
            this.btnHighlightSelection2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHighlightSelection2.Image = ((System.Drawing.Image)(resources.GetObject("btnHighlightSelection2.Image")));
            this.btnHighlightSelection2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHighlightSelection2.Name = "btnHighlightSelection2";
            this.btnHighlightSelection2.Size = new System.Drawing.Size(28, 28);
            this.btnHighlightSelection2.ToolTipText = "Enable Highlight Selection When Mouse Click";
            this.btnHighlightSelection2.Visible = false;
            this.btnHighlightSelection2.Click += new System.EventHandler(this.btnHighlightSelection_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            this.toolStripSeparator1.Visible = false;
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
            // btnShowIndentGuide
            // 
            this.btnShowIndentGuide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowIndentGuide.Image = ((System.Drawing.Image)(resources.GetObject("btnShowIndentGuide.Image")));
            this.btnShowIndentGuide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowIndentGuide.Name = "btnShowIndentGuide";
            this.btnShowIndentGuide.Size = new System.Drawing.Size(28, 28);
            this.btnShowIndentGuide.Text = "Show Indent Guide";
            this.btnShowIndentGuide.Visible = false;
            this.btnShowIndentGuide.Click += new System.EventHandler(this.btnShowIndentGuide_Click);
            // 
            // btnShowIndentGuide2
            // 
            this.btnShowIndentGuide2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowIndentGuide2.Image = ((System.Drawing.Image)(resources.GetObject("btnShowIndentGuide2.Image")));
            this.btnShowIndentGuide2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowIndentGuide2.Name = "btnShowIndentGuide2";
            this.btnShowIndentGuide2.Size = new System.Drawing.Size(28, 28);
            this.btnShowIndentGuide2.Text = "Show Indent Guide";
            this.btnShowIndentGuide2.Click += new System.EventHandler(this.btnShowIndentGuide_Click);
            // 
            // tsSeparator
            // 
            this.tsSeparator.Name = "tsSeparator";
            this.tsSeparator.Size = new System.Drawing.Size(6, 31);
            // 
            // chkShowGroupingRow
            // 
            this.chkShowGroupingRow.AutoSize = true;
            this.chkShowGroupingRow.BackColor = System.Drawing.Color.Transparent;
            this.chkShowGroupingRow.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowGroupingRow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowGroupingRow.ForeColor = System.Drawing.Color.Black;
            this.chkShowGroupingRow.Location = new System.Drawing.Point(857, 5);
            this.chkShowGroupingRow.Name = "chkShowGroupingRow";
            this.chkShowGroupingRow.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowGroupingRow.Size = new System.Drawing.Size(145, 22);
            this.chkShowGroupingRow.TabIndex = 86;
            this.chkShowGroupingRow.Text = "Show Grouping Row";
            this.c1ThemeController1.SetTheme(this.chkShowGroupingRow, "(default)");
            this.chkShowGroupingRow.UseVisualStyleBackColor = true;
            this.chkShowGroupingRow.Value = null;
            this.chkShowGroupingRow.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShowGroupingRow.CheckedChanged += new System.EventHandler(this.chkShowGroupingRow_CheckedChanged);
            this.chkShowGroupingRow.Click += new System.EventHandler(this.chkShowGroupingRow_Click);
            // 
            // btnHelp_RawDataMode
            // 
            this.btnHelp_RawDataMode.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp_RawDataMode.Image")));
            this.btnHelp_RawDataMode.Location = new System.Drawing.Point(1127, 4);
            this.btnHelp_RawDataMode.Name = "btnHelp_RawDataMode";
            this.btnHelp_RawDataMode.Size = new System.Drawing.Size(21, 21);
            this.btnHelp_RawDataMode.TabIndex = 84;
            this.c1ThemeController1.SetTheme(this.btnHelp_RawDataMode, "(default)");
            this.btnHelp_RawDataMode.UseVisualStyleBackColor = true;
            this.btnHelp_RawDataMode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnHelp_RawDataMode.Click += new System.EventHandler(this.btnHelp_RawDataMode_Click);
            // 
            // chkRawDataMode
            // 
            this.chkRawDataMode.AutoSize = true;
            this.chkRawDataMode.BackColor = System.Drawing.Color.Transparent;
            this.chkRawDataMode.BorderColor = System.Drawing.Color.Transparent;
            this.chkRawDataMode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkRawDataMode.ForeColor = System.Drawing.Color.Black;
            this.chkRawDataMode.Location = new System.Drawing.Point(1004, 5);
            this.chkRawDataMode.Name = "chkRawDataMode";
            this.chkRawDataMode.Padding = new System.Windows.Forms.Padding(1);
            this.chkRawDataMode.Size = new System.Drawing.Size(121, 22);
            this.chkRawDataMode.TabIndex = 82;
            this.chkRawDataMode.Text = "Raw Data Mode";
            this.c1ThemeController1.SetTheme(this.chkRawDataMode, "(default)");
            this.chkRawDataMode.UseVisualStyleBackColor = true;
            this.chkRawDataMode.Value = null;
            this.chkRawDataMode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkRawDataMode.CheckedChanged += new System.EventHandler(this.chkRawDataMode_CheckedChanged);
            this.chkRawDataMode.Click += new System.EventHandler(this.chkRawDataMode_Click);
            // 
            // chkShowColumnType
            // 
            this.chkShowColumnType.AutoSize = true;
            this.chkShowColumnType.BackColor = System.Drawing.Color.Transparent;
            this.chkShowColumnType.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowColumnType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowColumnType.ForeColor = System.Drawing.Color.Black;
            this.chkShowColumnType.Location = new System.Drawing.Point(786, 5);
            this.chkShowColumnType.Name = "chkShowColumnType";
            this.chkShowColumnType.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowColumnType.Size = new System.Drawing.Size(138, 22);
            this.chkShowColumnType.TabIndex = 80;
            this.chkShowColumnType.Text = "Show Column Type";
            this.c1ThemeController1.SetTheme(this.chkShowColumnType, "(default)");
            this.chkShowColumnType.UseVisualStyleBackColor = true;
            this.chkShowColumnType.Value = null;
            this.chkShowColumnType.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShowColumnType.Click += new System.EventHandler(this.chkShowColumnDataType_Click);
            // 
            // cboResultCopyFieldSeparator
            // 
            this.cboResultCopyFieldSeparator.AllowSpinLoop = false;
            this.cboResultCopyFieldSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.cboResultCopyFieldSeparator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboResultCopyFieldSeparator.GapHeight = 0;
            this.cboResultCopyFieldSeparator.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboResultCopyFieldSeparator.Items.Add(",");
            this.cboResultCopyFieldSeparator.Items.Add(";");
            this.cboResultCopyFieldSeparator.Items.Add("|");
            this.cboResultCopyFieldSeparator.ItemsDisplayMember = "";
            this.cboResultCopyFieldSeparator.ItemsValueMember = "";
            this.cboResultCopyFieldSeparator.Location = new System.Drawing.Point(1161, 5);
            this.cboResultCopyFieldSeparator.Name = "cboResultCopyFieldSeparator";
            this.cboResultCopyFieldSeparator.Size = new System.Drawing.Size(36, 21);
            this.cboResultCopyFieldSeparator.TabIndex = 78;
            this.cboResultCopyFieldSeparator.Tag = null;
            this.cboResultCopyFieldSeparator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cboResultCopyFieldSeparator.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboResultCopyFieldSeparator, "(default)");
            this.cboResultCopyFieldSeparator.Visible = false;
            this.cboResultCopyFieldSeparator.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // cboResultCopyQuotingWith
            // 
            this.cboResultCopyQuotingWith.AllowSpinLoop = false;
            this.cboResultCopyQuotingWith.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.cboResultCopyQuotingWith.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboResultCopyQuotingWith.GapHeight = 0;
            this.cboResultCopyQuotingWith.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboResultCopyQuotingWith.Items.Add("None");
            this.cboResultCopyQuotingWith.Items.Add("\"");
            this.cboResultCopyQuotingWith.Items.Add("\'");
            this.cboResultCopyQuotingWith.ItemsDisplayMember = "";
            this.cboResultCopyQuotingWith.ItemsValueMember = "";
            this.cboResultCopyQuotingWith.Location = new System.Drawing.Point(836, 5);
            this.cboResultCopyQuotingWith.Name = "cboResultCopyQuotingWith";
            this.cboResultCopyQuotingWith.Size = new System.Drawing.Size(59, 21);
            this.cboResultCopyQuotingWith.TabIndex = 77;
            this.cboResultCopyQuotingWith.Tag = null;
            this.cboResultCopyQuotingWith.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cboResultCopyQuotingWith.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboResultCopyQuotingWith, "(default)");
            this.cboResultCopyQuotingWith.Visible = false;
            this.cboResultCopyQuotingWith.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // cboFindGrid
            // 
            this.cboFindGrid.AllowSpinLoop = false;
            this.cboFindGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.cboFindGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboFindGrid.Enabled = false;
            this.cboFindGrid.GapHeight = 0;
            this.cboFindGrid.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboFindGrid.ItemsDisplayMember = "";
            this.cboFindGrid.ItemsValueMember = "";
            this.cboFindGrid.Location = new System.Drawing.Point(418, 5);
            this.cboFindGrid.Name = "cboFindGrid";
            this.cboFindGrid.Size = new System.Drawing.Size(110, 21);
            this.cboFindGrid.TabIndex = 75;
            this.cboFindGrid.Tag = null;
            this.cboFindGrid.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboFindGrid, "(default)");
            this.cboFindGrid.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboFindGrid.SelectedIndexChanged += new System.EventHandler(this.cboFindGrid_SelectedIndexChanged);
            this.cboFindGrid.BeforeDropDownOpen += new System.ComponentModel.CancelEventHandler(this.cboFindGrid_BeforeDropDownOpen);
            this.cboFindGrid.TextChanged += new System.EventHandler(this.cboFindGrid_TextChanged);
            this.cboFindGrid.Enter += new System.EventHandler(this.cboFindGrid_Enter);
            this.cboFindGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboFindGrid_KeyPress);
            this.cboFindGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboFindGrid_KeyUp);
            this.cboFindGrid.Leave += new System.EventHandler(this.cboFindGrid_Leave);
            // 
            // chkSort
            // 
            this.chkSort.AutoSize = true;
            this.chkSort.BackColor = System.Drawing.Color.Transparent;
            this.chkSort.BorderColor = System.Drawing.Color.Transparent;
            this.chkSort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkSort.ForeColor = System.Drawing.Color.Black;
            this.chkSort.Location = new System.Drawing.Point(292, 5);
            this.chkSort.Name = "chkSort";
            this.chkSort.Padding = new System.Windows.Forms.Padding(1);
            this.chkSort.Size = new System.Drawing.Size(52, 22);
            this.chkSort.TabIndex = 73;
            this.chkSort.Text = "Sort";
            this.c1ThemeController1.SetTheme(this.chkSort, "(default)");
            this.chkSort.UseVisualStyleBackColor = true;
            this.chkSort.Value = null;
            this.chkSort.Visible = false;
            this.chkSort.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkSort.Click += new System.EventHandler(this.chkSort_Click);
            // 
            // chkSize
            // 
            this.chkSize.AutoSize = true;
            this.chkSize.BackColor = System.Drawing.Color.Transparent;
            this.chkSize.BorderColor = System.Drawing.Color.Transparent;
            this.chkSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkSize.ForeColor = System.Drawing.Color.Black;
            this.chkSize.Location = new System.Drawing.Point(250, 5);
            this.chkSize.Name = "chkSize";
            this.chkSize.Padding = new System.Windows.Forms.Padding(1);
            this.chkSize.Size = new System.Drawing.Size(65, 22);
            this.chkSize.TabIndex = 72;
            this.chkSize.Text = "Resize";
            this.c1ThemeController1.SetTheme(this.chkSize, "(default)");
            this.chkSize.UseVisualStyleBackColor = true;
            this.chkSize.Value = null;
            this.chkSize.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkSize.Click += new System.EventHandler(this.chkSize_Click);
            // 
            // chkShowFilterRow
            // 
            this.chkShowFilterRow.AutoSize = true;
            this.chkShowFilterRow.BackColor = System.Drawing.Color.Transparent;
            this.chkShowFilterRow.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowFilterRow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowFilterRow.ForeColor = System.Drawing.Color.Black;
            this.chkShowFilterRow.Location = new System.Drawing.Point(148, 5);
            this.chkShowFilterRow.Name = "chkShowFilterRow";
            this.chkShowFilterRow.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowFilterRow.Size = new System.Drawing.Size(118, 22);
            this.chkShowFilterRow.TabIndex = 70;
            this.chkShowFilterRow.Text = "Show Filter Row";
            this.c1ThemeController1.SetTheme(this.chkShowFilterRow, "(default)");
            this.chkShowFilterRow.UseVisualStyleBackColor = true;
            this.chkShowFilterRow.Value = null;
            this.chkShowFilterRow.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShowFilterRow.Click += new System.EventHandler(this.chkShowFilterRow_Click);
            // 
            // c1DockingTab1
            // 
            this.c1DockingTab1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.c1DockingTab1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1DockingTab1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1DockingTab1.Controls.Add(this.tabMessage);
            this.c1DockingTab1.Controls.Add(this.tabSQLHistory);
            this.c1DockingTab1.Controls.Add(this.tabDataGrid);
            this.c1DockingTab1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1DockingTab1.Location = new System.Drawing.Point(0, 31);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.SelectedTabBold = true;
            this.c1DockingTab1.Size = new System.Drawing.Size(1159, 266);
            this.c1DockingTab1.TabIndex = 5;
            this.c1DockingTab1.TabsSpacing = 5;
            this.c1ThemeController1.SetTheme(this.c1DockingTab1, "(default)");
            this.c1DockingTab1.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.c1DockingTab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.c1DockingTab1.SelectedTabChanged += new System.EventHandler(this.c1DockingTab1_SelectedTabChanged);
            this.c1DockingTab1.Enter += new System.EventHandler(this.c1DockingTab1_Enter);
            this.c1DockingTab1.Leave += new System.EventHandler(this.c1DockingTab1_Leave);
            // 
            // tabMessage
            // 
            this.tabMessage.Controls.Add(this.editorMessage);
            this.tabMessage.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabMessage.Location = new System.Drawing.Point(0, 0);
            this.tabMessage.Name = "tabMessage";
            this.tabMessage.Size = new System.Drawing.Size(1159, 239);
            this.tabMessage.TabIndex = 0;
            this.tabMessage.Tag = "Message";
            this.tabMessage.Text = "Message";
            this.tabMessage.Enter += new System.EventHandler(this.tabMessage_Enter);
            this.tabMessage.Leave += new System.EventHandler(this.tabMessage_Leave);
            // 
            // editorMessage
            // 
            this.editorMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorMessage.CaretLineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.editorMessage.CaretLineVisible = true;
            this.editorMessage.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.editorMessage.Location = new System.Drawing.Point(0, 0);
            this.editorMessage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editorMessage.Name = "editorMessage";
            this.editorMessage.ReadOnly = true;
            this.editorMessage.Size = new System.Drawing.Size(1159, 240);
            this.editorMessage.Styler = null;
            this.editorMessage.TabIndex = 42;
            this.editorMessage.Tag = "";
            this.editorMessage.WhitespaceSize = 3;
            this.editorMessage.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.editorMessage.Enter += new System.EventHandler(this.editorMessage_Enter);
            this.editorMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editorMessage_MouseDown);
            this.editorMessage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.editorMessage_MouseMove);
            // 
            // tabSQLHistory
            // 
            this.tabSQLHistory.CaptionText = "SQL History";
            this.tabSQLHistory.Controls.Add(this.editorSQLHistory);
            this.tabSQLHistory.Location = new System.Drawing.Point(0, 0);
            this.tabSQLHistory.Name = "tabSQLHistory";
            this.tabSQLHistory.Size = new System.Drawing.Size(1159, 239);
            this.tabSQLHistory.TabIndex = 2;
            this.tabSQLHistory.Tag = "SQL History";
            this.tabSQLHistory.Text = "SQL History";
            this.tabSQLHistory.Enter += new System.EventHandler(this.tabSQLHistory_Enter);
            this.tabSQLHistory.Leave += new System.EventHandler(this.tabSQLHistory_Leave);
            // 
            // editorSQLHistory
            // 
            this.editorSQLHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorSQLHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorSQLHistory.CaretLineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.editorSQLHistory.CaretLineVisible = true;
            this.editorSQLHistory.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.editorSQLHistory.Location = new System.Drawing.Point(0, 0);
            this.editorSQLHistory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editorSQLHistory.Name = "editorSQLHistory";
            this.editorSQLHistory.ReadOnly = true;
            this.editorSQLHistory.Size = new System.Drawing.Size(1159, 240);
            this.editorSQLHistory.Styler = null;
            this.editorSQLHistory.TabIndex = 42;
            this.editorSQLHistory.Tag = "";
            this.editorSQLHistory.WhitespaceSize = 3;
            this.editorSQLHistory.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.editorSQLHistory.Enter += new System.EventHandler(this.editorSQLHistory_Enter);
            this.editorSQLHistory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editorSQLHistory_MouseDown);
            this.editorSQLHistory.MouseMove += new System.Windows.Forms.MouseEventHandler(this.editorSQLHistory_MouseMove);
            // 
            // tabDataGrid
            // 
            this.tabDataGrid.CaptionText = "Data Grid";
            this.tabDataGrid.Controls.Add(this.c1TrueDBGrid1);
            this.tabDataGrid.Location = new System.Drawing.Point(0, 0);
            this.tabDataGrid.Name = "tabDataGrid";
            this.tabDataGrid.Size = new System.Drawing.Size(1159, 239);
            this.tabDataGrid.TabIndex = 1;
            this.tabDataGrid.Tag = "Data Grid";
            this.tabDataGrid.Text = "Data Grid";
            this.tabDataGrid.Enter += new System.EventHandler(this.tabDataGrid_Enter);
            this.tabDataGrid.Leave += new System.EventHandler(this.tabDataGrid_Leave);
            // 
            // c1TrueDBGrid1
            // 
            this.c1TrueDBGrid1.AllowSort = false;
            this.c1TrueDBGrid1.AllowUpdate = false;
            this.c1TrueDBGrid1.AllowUpdateOnBlur = false;
            this.c1TrueDBGrid1.AlternatingRows = true;
            this.c1TrueDBGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1TrueDBGrid1.CaptionHeight = 19;
            this.c1TrueDBGrid1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1TrueDBGrid1.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1TrueDBGrid1.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGrid1.Images"))));
            this.c1TrueDBGrid1.Location = new System.Drawing.Point(0, 0);
            this.c1TrueDBGrid1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.c1TrueDBGrid1.Name = "c1TrueDBGrid1";
            this.c1TrueDBGrid1.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1TrueDBGrid1.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1TrueDBGrid1.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1TrueDBGrid1.PreviewInfo.ZoomFactor = 75D;
            this.c1TrueDBGrid1.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1TrueDBGrid1.PrintInfo.MeasurementPrinterName = null;
            this.c1TrueDBGrid1.RowHeight = 17;
            this.c1TrueDBGrid1.Size = new System.Drawing.Size(1159, 240);
            this.c1TrueDBGrid1.TabIndex = 66;
            this.c1TrueDBGrid1.UseCompatibleTextRendering = false;
            this.c1TrueDBGrid1.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
            this.c1TrueDBGrid1.OwnerDrawCell += new C1.Win.C1TrueDBGrid.OwnerDrawCellEventHandler(this.c1TrueDBGrid1_OwnerDrawCell);
            this.c1TrueDBGrid1.Scroll += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.c1TrueDBGrid1_Scroll);
            this.c1TrueDBGrid1.Click += new System.EventHandler(this.c1TrueDBGrid1_Click);
            this.c1TrueDBGrid1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.c1TrueDBGrid1_KeyUp);
            this.c1TrueDBGrid1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.c1TrueDBGrid1_MouseClick);
            this.c1TrueDBGrid1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1TrueDBGrid1_MouseDoubleClick);
            this.c1TrueDBGrid1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1TrueDBGrid1_MouseDown);
            this.c1TrueDBGrid1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1TrueDBGrid1_MouseMove);
            this.c1TrueDBGrid1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.c1TrueDBGrid1_MouseUp);
            this.c1TrueDBGrid1.PropBag = resources.GetString("c1TrueDBGrid1.PropBag");
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1StatusBar1.LeftPaneItems.Add(this.lblInfo);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 296);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.lblAverage);
            this.c1StatusBar1.RightPaneItems.Add(this.lblAverageValue);
            this.c1StatusBar1.RightPaneItems.Add(this.lblSeparator1);
            this.c1StatusBar1.RightPaneItems.Add(this.lblCount);
            this.c1StatusBar1.RightPaneItems.Add(this.lblCountValue);
            this.c1StatusBar1.RightPaneItems.Add(this.lblSeparator2);
            this.c1StatusBar1.RightPaneItems.Add(this.lblSummary);
            this.c1StatusBar1.RightPaneItems.Add(this.lblSummaryValue);
            this.c1StatusBar1.RightPaneItems.Add(this.lblSeparator3);
            this.c1StatusBar1.RightPaneItems.Add(this.lblNotCommitYet);
            this.c1StatusBar1.RightPaneItems.Add(this.sepNotCommitYet);
            this.c1StatusBar1.RightPaneItems.Add(this.lblExecTime);
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonSeparator2);
            this.c1StatusBar1.RightPaneItems.Add(this.lblQueryTime);
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonSeparator5);
            this.c1StatusBar1.RightPaneItems.Add(this.lblRows);
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonSeparator3);
            this.c1StatusBar1.RightPaneItems.Add(this.btnPaginationOn);
            this.c1StatusBar1.RightPaneItems.Add(this.btnPaginationOff);
            this.c1StatusBar1.RightPaneItems.Add(this.btnAppendingQueriesOn);
            this.c1StatusBar1.RightPaneItems.Add(this.btnAppendingQueriesOff);
            this.c1StatusBar1.RightPaneItems.Add(this.btnNextPage);
            this.c1StatusBar1.Size = new System.Drawing.Size(1159, 22);
            this.c1StatusBar1.SizingGrip = false;
            this.c1ThemeController1.SetTheme(this.c1StatusBar1, "(default)");
            this.c1StatusBar1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Windows7;
            this.c1StatusBar1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1StatusBar1_MouseMove);
            // 
            // lblInfo
            // 
            this.lblInfo.Name = "lblInfo";
            // 
            // lblAverage
            // 
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Text = "Average:";
            // 
            // lblAverageValue
            // 
            this.lblAverageValue.Name = "lblAverageValue";
            this.lblAverageValue.Text = "0";
            // 
            // lblSeparator1
            // 
            this.lblSeparator1.Name = "lblSeparator1";
            this.lblSeparator1.Text = "  ";
            // 
            // lblCount
            // 
            this.lblCount.Name = "lblCount";
            this.lblCount.Text = "Count:";
            // 
            // lblCountValue
            // 
            this.lblCountValue.Name = "lblCountValue";
            this.lblCountValue.Text = "0";
            // 
            // lblSeparator2
            // 
            this.lblSeparator2.Name = "lblSeparator2";
            this.lblSeparator2.Text = "  ";
            // 
            // lblSummary
            // 
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Text = "Sum:";
            // 
            // lblSummaryValue
            // 
            this.lblSummaryValue.Name = "lblSummaryValue";
            this.lblSummaryValue.Text = "0";
            // 
            // lblSeparator3
            // 
            this.lblSeparator3.Name = "lblSeparator3";
            // 
            // lblNotCommitYet
            // 
            this.lblNotCommitYet.Name = "lblNotCommitYet";
            this.lblNotCommitYet.SmallImage = ((System.Drawing.Image)(resources.GetObject("lblNotCommitYet.SmallImage")));
            this.lblNotCommitYet.Visible = false;
            // 
            // sepNotCommitYet
            // 
            this.sepNotCommitYet.Name = "sepNotCommitYet";
            this.sepNotCommitYet.Visible = false;
            // 
            // lblExecTime
            // 
            this.lblExecTime.Name = "lblExecTime";
            this.lblExecTime.Text = "Exec Time: 00:00.000";
            // 
            // ribbonSeparator2
            // 
            this.ribbonSeparator2.Name = "ribbonSeparator2";
            // 
            // lblQueryTime
            // 
            this.lblQueryTime.Name = "lblQueryTime";
            this.lblQueryTime.Text = "Query Time: 00:00.000";
            // 
            // ribbonSeparator5
            // 
            this.ribbonSeparator5.Name = "ribbonSeparator5";
            // 
            // lblRows
            // 
            this.lblRows.Name = "lblRows";
            this.lblRows.Text = "0 row(s)";
            // 
            // ribbonSeparator3
            // 
            this.ribbonSeparator3.Name = "ribbonSeparator3";
            // 
            // btnPaginationOn
            // 
            this.btnPaginationOn.Name = "btnPaginationOn";
            this.btnPaginationOn.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnPaginationOn.SmallImage")));
            this.btnPaginationOn.Text = "Paging Query";
            this.btnPaginationOn.Click += new System.EventHandler(this.btnPagination_Click);
            // 
            // btnPaginationOff
            // 
            this.btnPaginationOff.Name = "btnPaginationOff";
            this.btnPaginationOff.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnPaginationOff.SmallImage")));
            this.btnPaginationOff.Text = "Paging Query";
            this.btnPaginationOff.Click += new System.EventHandler(this.btnPagination_Click);
            // 
            // btnAppendingQueriesOn
            // 
            this.btnAppendingQueriesOn.Name = "btnAppendingQueriesOn";
            this.btnAppendingQueriesOn.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnAppendingQueriesOn.SmallImage")));
            this.btnAppendingQueriesOn.Text = "Appending Queries";
            this.btnAppendingQueriesOn.Click += new System.EventHandler(this.btnAppendingQueries_Click);
            // 
            // btnAppendingQueriesOff
            // 
            this.btnAppendingQueriesOff.Name = "btnAppendingQueriesOff";
            this.btnAppendingQueriesOff.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnAppendingQueriesOff.SmallImage")));
            this.btnAppendingQueriesOff.Text = "Appending Queries";
            this.btnAppendingQueriesOff.Click += new System.EventHandler(this.btnAppendingQueries_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNextPage.SmallImage")));
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // tsDataGrid
            // 
            this.tsDataGrid.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsDataGrid.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportToFile,
            this.btnExportToCSV,
            this.btnFreezeColumn,
            this.btnShowSQL,
            this.btnAutoSize,
            this.btnAutoSort,
            this.toolStripSeparator10,
            this.toolStripLabel2,
            this.lblSpace,
            this.toolStripSeparator11,
            this.lblFindGrid,
            this.toolStripLabel6,
            this.btnFindNextGrid,
            this.btnFindPreviousGrid,
            this.btnCountGrid,
            this.btnHighlightAllGrid,
            this.btnClearHighlightsGrid,
            this.toolStripSeparator12,
            this.toolStripLabel1,
            this.btnOptions,
            this.lblResultCopyQuotingWith,
            this.toolStripLabel7,
            this.toolStripSeparator13,
            this.toolStripLabel3,
            this.lblResultCopyFieldSeparator,
            this.toolStripLabel8,
            this.toolStripSeparator14});
            this.tsDataGrid.Location = new System.Drawing.Point(0, 0);
            this.tsDataGrid.Name = "tsDataGrid";
            this.tsDataGrid.Size = new System.Drawing.Size(1159, 31);
            this.tsDataGrid.TabIndex = 2;
            this.c1ThemeController1.SetTheme(this.tsDataGrid, "(default)");
            this.tsDataGrid.Enter += new System.EventHandler(this.tsDataGrid_Enter);
            this.tsDataGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tsDataGrid_MouseMove);
            // 
            // btnExportToFile
            // 
            this.btnExportToFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportToFile.Enabled = false;
            this.btnExportToFile.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToFile.Image")));
            this.btnExportToFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportToFile.Name = "btnExportToFile";
            this.btnExportToFile.Size = new System.Drawing.Size(28, 28);
            this.btnExportToFile.ToolTipText = "Export all data to Excel";
            this.btnExportToFile.Click += new System.EventHandler(this.btnExportToFile_Click);
            // 
            // btnExportToCSV
            // 
            this.btnExportToCSV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportToCSV.Enabled = false;
            this.btnExportToCSV.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToCSV.Image")));
            this.btnExportToCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportToCSV.Name = "btnExportToCSV";
            this.btnExportToCSV.Size = new System.Drawing.Size(28, 28);
            this.btnExportToCSV.ToolTipText = "Export all data to CSV";
            this.btnExportToCSV.Visible = false;
            this.btnExportToCSV.Click += new System.EventHandler(this.btnExportToCSV_Click);
            // 
            // btnFreezeColumn
            // 
            this.btnFreezeColumn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFreezeColumn.Enabled = false;
            this.btnFreezeColumn.Image = ((System.Drawing.Image)(resources.GetObject("btnFreezeColumn.Image")));
            this.btnFreezeColumn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFreezeColumn.Name = "btnFreezeColumn";
            this.btnFreezeColumn.Size = new System.Drawing.Size(28, 28);
            this.btnFreezeColumn.ToolTipText = "Freeze Column";
            this.btnFreezeColumn.Click += new System.EventHandler(this.btnFreezeColumn_Click);
            // 
            // btnShowSQL
            // 
            this.btnShowSQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowSQL.Enabled = false;
            this.btnShowSQL.Image = ((System.Drawing.Image)(resources.GetObject("btnShowSQL.Image")));
            this.btnShowSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowSQL.Name = "btnShowSQL";
            this.btnShowSQL.Size = new System.Drawing.Size(28, 28);
            this.btnShowSQL.ToolTipText = "SQL Statement Viewer";
            this.btnShowSQL.Click += new System.EventHandler(this.btnShowSQL_Click);
            // 
            // btnAutoSize
            // 
            this.btnAutoSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAutoSize.Enabled = false;
            this.btnAutoSize.Image = ((System.Drawing.Image)(resources.GetObject("btnAutoSize.Image")));
            this.btnAutoSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAutoSize.Name = "btnAutoSize";
            this.btnAutoSize.Size = new System.Drawing.Size(28, 28);
            this.btnAutoSize.ToolTipText = "Auto Resize Column\'s Width by Data Result";
            this.btnAutoSize.Click += new System.EventHandler(this.btnAutoSize_Click);
            // 
            // btnAutoSort
            // 
            this.btnAutoSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAutoSort.Enabled = false;
            this.btnAutoSort.Image = ((System.Drawing.Image)(resources.GetObject("btnAutoSort.Image")));
            this.btnAutoSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAutoSort.Name = "btnAutoSort";
            this.btnAutoSort.Size = new System.Drawing.Size(28, 28);
            this.btnAutoSort.ToolTipText = "Auto Sort Data Result based on Column\'s Header";
            this.btnAutoSort.Click += new System.EventHandler(this.btnAutoSort_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Consolas", 2.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(5, 28);
            this.toolStripLabel2.Text = " ";
            // 
            // lblSpace
            // 
            this.lblSpace.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpace.Name = "lblSpace";
            this.lblSpace.Size = new System.Drawing.Size(224, 28);
            this.lblSpace.Text = "                               ";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 31);
            // 
            // lblFindGrid
            // 
            this.lblFindGrid.Name = "lblFindGrid";
            this.lblFindGrid.Size = new System.Drawing.Size(34, 28);
            this.lblFindGrid.Tag = "";
            this.lblFindGrid.Text = "Find:";
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripLabel6.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(119, 28);
            this.toolStripLabel6.Text = "                ";
            // 
            // btnFindNextGrid
            // 
            this.btnFindNextGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFindNextGrid.Enabled = false;
            this.btnFindNextGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnFindNextGrid.Image")));
            this.btnFindNextGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFindNextGrid.Name = "btnFindNextGrid";
            this.btnFindNextGrid.Size = new System.Drawing.Size(28, 28);
            this.btnFindNextGrid.Tag = "";
            this.btnFindNextGrid.ToolTipText = "Find Next";
            this.btnFindNextGrid.Click += new System.EventHandler(this.btnFindNextGrid_Click);
            // 
            // btnFindPreviousGrid
            // 
            this.btnFindPreviousGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFindPreviousGrid.Enabled = false;
            this.btnFindPreviousGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnFindPreviousGrid.Image")));
            this.btnFindPreviousGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFindPreviousGrid.Name = "btnFindPreviousGrid";
            this.btnFindPreviousGrid.Size = new System.Drawing.Size(28, 28);
            this.btnFindPreviousGrid.Tag = "";
            this.btnFindPreviousGrid.ToolTipText = "Find Previous";
            this.btnFindPreviousGrid.Click += new System.EventHandler(this.btnFindPreviousGrid_Click);
            // 
            // btnCountGrid
            // 
            this.btnCountGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCountGrid.Enabled = false;
            this.btnCountGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnCountGrid.Image")));
            this.btnCountGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCountGrid.Name = "btnCountGrid";
            this.btnCountGrid.Size = new System.Drawing.Size(28, 28);
            this.btnCountGrid.ToolTipText = "Count";
            this.btnCountGrid.Click += new System.EventHandler(this.btnCountGrid_Click);
            // 
            // btnHighlightAllGrid
            // 
            this.btnHighlightAllGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHighlightAllGrid.Enabled = false;
            this.btnHighlightAllGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnHighlightAllGrid.Image")));
            this.btnHighlightAllGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHighlightAllGrid.Name = "btnHighlightAllGrid";
            this.btnHighlightAllGrid.Size = new System.Drawing.Size(28, 28);
            this.btnHighlightAllGrid.ToolTipText = "Highlight All";
            this.btnHighlightAllGrid.Click += new System.EventHandler(this.btnHighlightAllGrid_Click);
            // 
            // btnClearHighlightsGrid
            // 
            this.btnClearHighlightsGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearHighlightsGrid.Enabled = false;
            this.btnClearHighlightsGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnClearHighlightsGrid.Image")));
            this.btnClearHighlightsGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearHighlightsGrid.Name = "btnClearHighlightsGrid";
            this.btnClearHighlightsGrid.Size = new System.Drawing.Size(28, 28);
            this.btnClearHighlightsGrid.ToolTipText = "Clear Highlights";
            this.btnClearHighlightsGrid.Click += new System.EventHandler(this.btnClearHighlightsGrid_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("微軟正黑體", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(4, 28);
            this.toolStripLabel1.Text = " ";
            // 
            // btnOptions
            // 
            this.btnOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuResultCopyQuotingWith,
            this.mnuResultCopyFieldSeparator,
            this.mnuPreviewCLOBData});
            this.btnOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOptions.Image")));
            this.btnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(37, 28);
            this.btnOptions.Visible = false;
            // 
            // mnuResultCopyQuotingWith
            // 
            this.mnuResultCopyQuotingWith.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuResultCopyQuotingWithNone,
            this.mnuResultCopyQuotingWithDoubleQuoting,
            this.mnuResultCopyQuotingWithSingleQuoting});
            this.mnuResultCopyQuotingWith.Name = "mnuResultCopyQuotingWith";
            this.mnuResultCopyQuotingWith.Size = new System.Drawing.Size(230, 22);
            this.mnuResultCopyQuotingWith.Tag = "None";
            this.mnuResultCopyQuotingWith.Text = "Result Copy Quoting With";
            // 
            // mnuResultCopyQuotingWithNone
            // 
            this.mnuResultCopyQuotingWithNone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuResultCopyQuotingWithNone.Name = "mnuResultCopyQuotingWithNone";
            this.mnuResultCopyQuotingWithNone.Size = new System.Drawing.Size(106, 22);
            this.mnuResultCopyQuotingWithNone.Tag = "None";
            this.mnuResultCopyQuotingWithNone.Text = "None";
            this.mnuResultCopyQuotingWithNone.Click += new System.EventHandler(this.mnuResultCopyQuotingWith_Click);
            // 
            // mnuResultCopyQuotingWithDoubleQuoting
            // 
            this.mnuResultCopyQuotingWithDoubleQuoting.Name = "mnuResultCopyQuotingWithDoubleQuoting";
            this.mnuResultCopyQuotingWithDoubleQuoting.Size = new System.Drawing.Size(106, 22);
            this.mnuResultCopyQuotingWithDoubleQuoting.Tag = "\"";
            this.mnuResultCopyQuotingWithDoubleQuoting.Text = "\"";
            this.mnuResultCopyQuotingWithDoubleQuoting.Click += new System.EventHandler(this.mnuResultCopyQuotingWith_Click);
            // 
            // mnuResultCopyQuotingWithSingleQuoting
            // 
            this.mnuResultCopyQuotingWithSingleQuoting.Name = "mnuResultCopyQuotingWithSingleQuoting";
            this.mnuResultCopyQuotingWithSingleQuoting.Size = new System.Drawing.Size(106, 22);
            this.mnuResultCopyQuotingWithSingleQuoting.Tag = "\'";
            this.mnuResultCopyQuotingWithSingleQuoting.Text = "\'";
            this.mnuResultCopyQuotingWithSingleQuoting.Click += new System.EventHandler(this.mnuResultCopyQuotingWith_Click);
            // 
            // mnuResultCopyFieldSeparator
            // 
            this.mnuResultCopyFieldSeparator.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuResultCopyFieldSeparatorComma,
            this.mnuResultCopyFieldSeparatorSemicolon,
            this.mnuResultCopyFieldSeparatorI});
            this.mnuResultCopyFieldSeparator.Name = "mnuResultCopyFieldSeparator";
            this.mnuResultCopyFieldSeparator.Size = new System.Drawing.Size(230, 22);
            this.mnuResultCopyFieldSeparator.Tag = ",";
            this.mnuResultCopyFieldSeparator.Text = "Result Copy Field Separator";
            // 
            // mnuResultCopyFieldSeparatorComma
            // 
            this.mnuResultCopyFieldSeparatorComma.Name = "mnuResultCopyFieldSeparatorComma";
            this.mnuResultCopyFieldSeparatorComma.Size = new System.Drawing.Size(77, 22);
            this.mnuResultCopyFieldSeparatorComma.Tag = ",";
            this.mnuResultCopyFieldSeparatorComma.Text = ",";
            this.mnuResultCopyFieldSeparatorComma.Click += new System.EventHandler(this.mnuResultCopyFieldSeparator_Click);
            // 
            // mnuResultCopyFieldSeparatorSemicolon
            // 
            this.mnuResultCopyFieldSeparatorSemicolon.Name = "mnuResultCopyFieldSeparatorSemicolon";
            this.mnuResultCopyFieldSeparatorSemicolon.Size = new System.Drawing.Size(77, 22);
            this.mnuResultCopyFieldSeparatorSemicolon.Tag = ";";
            this.mnuResultCopyFieldSeparatorSemicolon.Text = ";";
            this.mnuResultCopyFieldSeparatorSemicolon.Click += new System.EventHandler(this.mnuResultCopyFieldSeparator_Click);
            // 
            // mnuResultCopyFieldSeparatorI
            // 
            this.mnuResultCopyFieldSeparatorI.Name = "mnuResultCopyFieldSeparatorI";
            this.mnuResultCopyFieldSeparatorI.Size = new System.Drawing.Size(77, 22);
            this.mnuResultCopyFieldSeparatorI.Tag = "|";
            this.mnuResultCopyFieldSeparatorI.Text = "|";
            this.mnuResultCopyFieldSeparatorI.Click += new System.EventHandler(this.mnuResultCopyFieldSeparator_Click);
            // 
            // mnuPreviewCLOBData
            // 
            this.mnuPreviewCLOBData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuPreviewCLOBData.Name = "mnuPreviewCLOBData";
            this.mnuPreviewCLOBData.Size = new System.Drawing.Size(230, 22);
            this.mnuPreviewCLOBData.Text = "Preview CLOB Data";
            this.mnuPreviewCLOBData.Visible = false;
            this.mnuPreviewCLOBData.Click += new System.EventHandler(this.mnuPreviewCLOBData_Click);
            // 
            // lblResultCopyQuotingWith
            // 
            this.lblResultCopyQuotingWith.Name = "lblResultCopyQuotingWith";
            this.lblResultCopyQuotingWith.Size = new System.Drawing.Size(153, 28);
            this.lblResultCopyQuotingWith.Text = "Result Copy Quoting with:";
            this.lblResultCopyQuotingWith.Visible = false;
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(64, 28);
            this.toolStripLabel7.Text = "                   ";
            this.toolStripLabel7.Visible = false;
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 31);
            this.toolStripSeparator13.Visible = false;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Font = new System.Drawing.Font("微軟正黑體", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(4, 28);
            this.toolStripLabel3.Text = " ";
            // 
            // lblResultCopyFieldSeparator
            // 
            this.lblResultCopyFieldSeparator.Name = "lblResultCopyFieldSeparator";
            this.lblResultCopyFieldSeparator.Size = new System.Drawing.Size(166, 28);
            this.lblResultCopyFieldSeparator.Text = "Result Copy Field Separator:";
            this.lblResultCopyFieldSeparator.Visible = false;
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(43, 28);
            this.toolStripLabel8.Text = "            ";
            this.toolStripLabel8.Visible = false;
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 31);
            this.toolStripSeparator14.Visible = false;
            // 
            // c1GridAC4Space2
            // 
            this.c1GridAC4Space2.AllowFilter = false;
            this.c1GridAC4Space2.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.IndividualRows;
            this.c1GridAC4Space2.AllowUpdate = false;
            this.c1GridAC4Space2.AlternatingRows = true;
            this.c1GridAC4Space2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1GridAC4Space2.CaptionHeight = 19;
            this.c1GridAC4Space2.ColumnHeaders = false;
            this.c1GridAC4Space2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1GridAC4Space2.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1GridAC4Space2.Images.Add(((System.Drawing.Image)(resources.GetObject("c1GridAC4Space2.Images"))));
            this.c1GridAC4Space2.Location = new System.Drawing.Point(971, 5);
            this.c1GridAC4Space2.Name = "c1GridAC4Space2";
            this.c1GridAC4Space2.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1GridAC4Space2.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1GridAC4Space2.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1GridAC4Space2.PreviewInfo.ZoomFactor = 75D;
            this.c1GridAC4Space2.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1GridAC4Space2.PrintInfo.MeasurementPrinterName = null;
            this.c1GridAC4Space2.RowHeight = 19;
            this.c1GridAC4Space2.Size = new System.Drawing.Size(270, 183);
            this.c1GridAC4Space2.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.c1GridAC4Space2.TabIndex = 115;
            this.c1ThemeController1.SetTheme(this.c1GridAC4Space2, "(default)");
            this.c1GridAC4Space2.UseCompatibleTextRendering = false;
            this.c1GridAC4Space2.Visible = false;
            this.c1GridAC4Space2.PropBag = resources.GetString("c1GridAC4Space2.PropBag");
            // 
            // c1GridAC4Space1
            // 
            this.c1GridAC4Space1.AllowFilter = false;
            this.c1GridAC4Space1.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.IndividualRows;
            this.c1GridAC4Space1.AllowUpdate = false;
            this.c1GridAC4Space1.AlternatingRows = true;
            this.c1GridAC4Space1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1GridAC4Space1.CaptionHeight = 19;
            this.c1GridAC4Space1.ColumnHeaders = false;
            this.c1GridAC4Space1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1GridAC4Space1.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1GridAC4Space1.Images.Add(((System.Drawing.Image)(resources.GetObject("c1GridAC4Space1.Images"))));
            this.c1GridAC4Space1.Location = new System.Drawing.Point(929, 5);
            this.c1GridAC4Space1.Name = "c1GridAC4Space1";
            this.c1GridAC4Space1.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1GridAC4Space1.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1GridAC4Space1.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1GridAC4Space1.PreviewInfo.ZoomFactor = 75D;
            this.c1GridAC4Space1.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1GridAC4Space1.PrintInfo.MeasurementPrinterName = null;
            this.c1GridAC4Space1.RowHeight = 19;
            this.c1GridAC4Space1.Size = new System.Drawing.Size(270, 183);
            this.c1GridAC4Space1.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.c1GridAC4Space1.TabIndex = 114;
            this.c1ThemeController1.SetTheme(this.c1GridAC4Space1, "(default)");
            this.c1GridAC4Space1.UseCompatibleTextRendering = false;
            this.c1GridAC4Space1.Visible = false;
            this.c1GridAC4Space1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1GridAC4Space_KeyDown);
            this.c1GridAC4Space1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1GridAC4Space_KeyPress);
            this.c1GridAC4Space1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1GridAC4Space_MouseDoubleClick);
            this.c1GridAC4Space1.PropBag = resources.GetString("c1GridAC4Space1.PropBag");
            // 
            // c1GridAC4Period2
            // 
            this.c1GridAC4Period2.AllowFilter = false;
            this.c1GridAC4Period2.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.IndividualRows;
            this.c1GridAC4Period2.AllowUpdate = false;
            this.c1GridAC4Period2.AlternatingRows = true;
            this.c1GridAC4Period2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1GridAC4Period2.CaptionHeight = 19;
            this.c1GridAC4Period2.ColumnHeaders = false;
            this.c1GridAC4Period2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1GridAC4Period2.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1GridAC4Period2.Images.Add(((System.Drawing.Image)(resources.GetObject("c1GridAC4Period2.Images"))));
            this.c1GridAC4Period2.Location = new System.Drawing.Point(888, 5);
            this.c1GridAC4Period2.Name = "c1GridAC4Period2";
            this.c1GridAC4Period2.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1GridAC4Period2.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1GridAC4Period2.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1GridAC4Period2.PreviewInfo.ZoomFactor = 75D;
            this.c1GridAC4Period2.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1GridAC4Period2.PrintInfo.MeasurementPrinterName = null;
            this.c1GridAC4Period2.RowHeight = 19;
            this.c1GridAC4Period2.Size = new System.Drawing.Size(270, 183);
            this.c1GridAC4Period2.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.c1GridAC4Period2.TabIndex = 114;
            this.c1ThemeController1.SetTheme(this.c1GridAC4Period2, "(default)");
            this.c1GridAC4Period2.UseCompatibleTextRendering = false;
            this.c1GridAC4Period2.Visible = false;
            this.c1GridAC4Period2.PropBag = resources.GetString("c1GridAC4Period2.PropBag");
            // 
            // tmrlblInfo
            // 
            this.tmrlblInfo.Enabled = true;
            this.tmrlblInfo.Tick += new System.EventHandler(this.tmrlblInfo_Tick);
            // 
            // tmrMother2Child
            // 
            this.tmrMother2Child.Enabled = true;
            this.tmrMother2Child.Tick += new System.EventHandler(this.timerMother2Child_Tick);
            // 
            // c1GridAC4All
            // 
            this.c1GridAC4All.AllowFilter = false;
            this.c1GridAC4All.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.IndividualRows;
            this.c1GridAC4All.AllowUpdate = false;
            this.c1GridAC4All.AlternatingRows = true;
            this.c1GridAC4All.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1GridAC4All.CaptionHeight = 19;
            this.c1GridAC4All.ColumnHeaders = false;
            this.c1GridAC4All.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1GridAC4All.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1GridAC4All.Images.Add(((System.Drawing.Image)(resources.GetObject("c1GridAC4All.Images"))));
            this.c1GridAC4All.Location = new System.Drawing.Point(886, 20);
            this.c1GridAC4All.Name = "c1GridAC4All";
            this.c1GridAC4All.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1GridAC4All.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1GridAC4All.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1GridAC4All.PreviewInfo.ZoomFactor = 75D;
            this.c1GridAC4All.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1GridAC4All.PrintInfo.MeasurementPrinterName = null;
            this.c1GridAC4All.RowHeight = 19;
            this.c1GridAC4All.Size = new System.Drawing.Size(270, 183);
            this.c1GridAC4All.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.c1GridAC4All.TabIndex = 116;
            this.c1ThemeController1.SetTheme(this.c1GridAC4All, "(default)");
            this.c1GridAC4All.UseCompatibleTextRendering = false;
            this.c1GridAC4All.Visible = false;
            this.c1GridAC4All.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1GridAC4All_KeyDown);
            this.c1GridAC4All.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1GridAC4All_KeyPress);
            this.c1GridAC4All.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1GridAC4All_MouseDoubleClick);
            this.c1GridAC4All.PropBag = resources.GetString("c1GridAC4All.PropBag");
            // 
            // c1GridAC4Period1
            // 
            this.c1GridAC4Period1.AllowFilter = false;
            this.c1GridAC4Period1.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.IndividualRows;
            this.c1GridAC4Period1.AllowUpdate = false;
            this.c1GridAC4Period1.AlternatingRows = true;
            this.c1GridAC4Period1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.c1GridAC4Period1.CaptionHeight = 19;
            this.c1GridAC4Period1.ColumnHeaders = false;
            this.c1GridAC4Period1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.c1GridAC4Period1.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1GridAC4Period1.Images.Add(((System.Drawing.Image)(resources.GetObject("c1GridAC4Period1.Images"))));
            this.c1GridAC4Period1.Location = new System.Drawing.Point(857, 15);
            this.c1GridAC4Period1.Name = "c1GridAC4Period1";
            this.c1GridAC4Period1.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1GridAC4Period1.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1GridAC4Period1.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1GridAC4Period1.PreviewInfo.ZoomFactor = 75D;
            this.c1GridAC4Period1.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1GridAC4Period1.PrintInfo.MeasurementPrinterName = null;
            this.c1GridAC4Period1.RowHeight = 19;
            this.c1GridAC4Period1.Size = new System.Drawing.Size(270, 183);
            this.c1GridAC4Period1.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.GridNavigation;
            this.c1GridAC4Period1.TabIndex = 84;
            this.c1GridAC4Period1.UseCompatibleTextRendering = false;
            this.c1GridAC4Period1.Visible = false;
            this.c1GridAC4Period1.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Custom;
            this.c1GridAC4Period1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.c1GridAC4Period_KeyDown);
            this.c1GridAC4Period1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.c1GridAC4Period_KeyPress);
            this.c1GridAC4Period1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1GridAC4Period_MouseDoubleClick);
            this.c1GridAC4Period1.PropBag = resources.GetString("c1GridAC4Period1.PropBag");
            // 
            // tmrExecTime
            // 
            this.tmrExecTime.Tick += new System.EventHandler(this.tmrExecTime_Tick);
            // 
            // tmrlblInfoEditor
            // 
            this.tmrlblInfoEditor.Enabled = true;
            this.tmrlblInfoEditor.Tick += new System.EventHandler(this.tmrlblInfoEditor_Tick);
            // 
            // tmrQueryTime
            // 
            this.tmrQueryTime.Interval = 50;
            this.tmrQueryTime.Tick += new System.EventHandler(this.tmrQueryTime_Tick);
            // 
            // c1CommandHolder1
            // 
            this.c1CommandHolder1.Owner = this;
            // 
            // frmQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 557);
            this.Controls.Add(this.c1GridAC4All);
            this.Controls.Add(this.c1GridAC4Period1);
            this.Controls.Add(this.c1GridAC4Space2);
            this.Controls.Add(this.c1GridAC4Space1);
            this.Controls.Add(this.c1GridAC4Period2);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Query";
            this.c1ThemeController1.SetTheme(this, "(default)");
            this.Load += new System.EventHandler(this.Form_Load);
            this.Leave += new System.EventHandler(this.Form_Leave);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtIndentWord)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab2)).EndInit();
            this.c1DockingTab2.ResumeLayout(false);
            this.tabAutoReplace.ResumeLayout(false);
            this.tabAutoReplace.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridARInfo)).EndInit();
            this.tsAutoReplace.ResumeLayout(false);
            this.tsAutoReplace.PerformLayout();
            this.tabSchemaBrowser.ResumeLayout(false);
            this.tabSchemaBrowser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSchemaFilter)).EndInit();
            this.tsSchemaBrowser.ResumeLayout(false);
            this.tsSchemaBrowser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridSchemaBrowser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar2)).EndInit();
            this.tsEditor.ResumeLayout(false);
            this.tsEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowGroupingRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_RawDataMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRawDataMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowColumnType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResultCopyFieldSeparator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResultCopyQuotingWith)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFindGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilterRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).EndInit();
            this.c1DockingTab1.ResumeLayout(false);
            this.tabMessage.ResumeLayout(false);
            this.tabSQLHistory.ResumeLayout(false);
            this.tabDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            this.tsDataGrid.ResumeLayout(false);
            this.tsDataGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridAC4Space2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridAC4Space1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridAC4Period2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridAC4All)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridAC4Period1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1CommandHolder1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStrip tsEditor;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnSaveRed;
        private System.Windows.Forms.ToolStripButton btnSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnQuery;
        private System.Windows.Forms.ToolStripButton btnSelectCurrentBlock;
        private System.Windows.Forms.ToolStripButton btnExecuteCurrentBlock;
        private System.Windows.Forms.ToolStripButton btnCancelQuery;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton btnCode2SQL;
        private System.Windows.Forms.ToolStripMenuItem mnuCSharp2SQL;
        private System.Windows.Forms.ToolStripMenuItem mnuVB2SQL;
        private System.Windows.Forms.ToolStripMenuItem mnuDephi2SQL;
        private System.Windows.Forms.ToolStripDropDownButton btnSQL2Code;
        private System.Windows.Forms.ToolStripMenuItem mnuSQL2CSharp;
        private System.Windows.Forms.ToolStripMenuItem mnuCSharpStyle1;
        private System.Windows.Forms.ToolStripMenuItem mnuCSharpStyle2;
        private System.Windows.Forms.ToolStripMenuItem mnuCSharpStyle3;
        private System.Windows.Forms.ToolStripMenuItem mnuSQL2VBNet;
        private System.Windows.Forms.ToolStripMenuItem mnuVBNetStyle1;
        private System.Windows.Forms.ToolStripMenuItem mnuVBNetStyle2;
        private System.Windows.Forms.ToolStripMenuItem mnuVBNetStyle3;
        private System.Windows.Forms.ToolStripMenuItem mnuSQL2VB6A;
        private System.Windows.Forms.ToolStripMenuItem mnuVB6AStyle1;
        private System.Windows.Forms.ToolStripMenuItem mnuVB6AStyle2;
        private System.Windows.Forms.ToolStripMenuItem mnuSQL2Delphi;
        private System.Windows.Forms.ToolStripMenuItem mnuDelphi6Style1;
        private System.Windows.Forms.ToolStripMenuItem mnuDelphi6Style2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton btnLeftAndRight;
        private System.Windows.Forms.ToolStripButton btnUpAndDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnComment;
        private System.Windows.Forms.ToolStripButton btnRemoveComment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnIndent;
        private System.Windows.Forms.ToolStripTextBox txtIndentWord2;
        private System.Windows.Forms.ToolStripButton btnUnIndent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnHighlightSelection;
        private System.Windows.Forms.ToolStripButton btnHighlightSelection2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnWordWrap;
        private System.Windows.Forms.ToolStripButton btnWordWrap2;
        private System.Windows.Forms.ToolStripButton btnShowAllCharacters;
        private System.Windows.Forms.ToolStripButton btnShowAllCharacters2;
        private System.Windows.Forms.ToolStripSeparator tsSeparator;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1GridARInfo;
        private ScintillaEditor editor;
        private System.Windows.Forms.ToolStrip tsDataGrid;
        private System.Windows.Forms.ToolStripButton btnExportToFile;
        private System.Windows.Forms.ToolStripButton btnExportToCSV;
        private System.Windows.Forms.ToolStripButton btnFreezeColumn;
        private System.Windows.Forms.ToolStripButton btnAutoSize;
        private System.Windows.Forms.ToolStripButton btnAutoSort;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel lblFindGrid;
        private System.Windows.Forms.ToolStripButton btnFindNextGrid;
        private System.Windows.Forms.ToolStripButton btnFindPreviousGrid;
        private System.Windows.Forms.ToolStripButton btnCountGrid;
        private System.Windows.Forms.ToolStripButton btnHighlightAllGrid;
        private System.Windows.Forms.ToolStripButton btnClearHighlightsGrid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel lblResultCopyQuotingWith;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel lblResultCopyFieldSeparator;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonLabel lblInfo;
        private C1.Win.C1Ribbon.RibbonLabel lblExecTime;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator5;
        private C1.Win.C1Ribbon.RibbonLabel lblRows;
        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage tabMessage;
        private ScintillaEditor editorMessage;
        private C1.Win.C1Command.C1DockingTabPage tabDataGrid;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1TrueDBGrid1;
        private C1.Win.C1Command.C1DockingTabPage tabSQLHistory;
        private ScintillaEditor editorSQLHistory;
        private System.Windows.Forms.Timer tmrlblInfo;
        private System.Windows.Forms.Timer tmrMother2Child;
        private C1.Win.C1Themes.C1ThemeController c1ThemeController1;
        private C1.Win.C1Input.C1TextBox txtIndentWord;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel lblSpace;
        private C1.Win.C1Input.C1CheckBox chkShowFilterRow;
        private System.Windows.Forms.ToolStripButton btnCommit;
        private System.Windows.Forms.ToolStripButton btnRollback;
        private C1.Win.C1Input.C1CheckBox chkSort;
        private C1.Win.C1Input.C1CheckBox chkSize;
        private C1.Win.C1Input.C1ComboBox cboFindGrid;
        private C1.Win.C1Input.C1ComboBox cboResultCopyFieldSeparator;
        private C1.Win.C1Input.C1ComboBox cboResultCopyQuotingWith;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.Timer tmrExecTime;
        private System.Windows.Forms.ToolStripButton btnShowSQL;
        private C1.Win.C1Input.C1CheckBox chkShowColumnType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripLabel toolStripLabel8;
        private System.Windows.Forms.Label lblInfoEditor;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar2;
        private C1.Win.C1Ribbon.RibbonLabel lblEditorLength;
        private C1.Win.C1Ribbon.RibbonLabel lblEditorLines;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator1;
        private C1.Win.C1Ribbon.RibbonLabel lblEditorLn;
        private C1.Win.C1Ribbon.RibbonLabel lblEditorCol;
        private C1.Win.C1Ribbon.RibbonLabel lblEditorPos;
        private C1.Win.C1Ribbon.RibbonLabel lblEditorSel;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator7;
        private C1.Win.C1Ribbon.RibbonLabel lblEndOfLineStyle;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator8;
        private C1.Win.C1Ribbon.RibbonLabel lblEncode;
        private System.Windows.Forms.Timer tmrlblInfoEditor;
        private C1.Win.C1Ribbon.RibbonLabel lblTemp;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel2;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel3;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel4;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel5;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator2;
        private C1.Win.C1Ribbon.RibbonLabel lblQueryTime;
        private System.Windows.Forms.Timer tmrQueryTime;
        private C1.Win.C1Ribbon.RibbonLabel lblNotCommitYet;
        private C1.Win.C1Ribbon.RibbonSeparator sepNotCommitYet;
        private System.Windows.Forms.ToolStripButton btnShowIndentGuide;
        private System.Windows.Forms.ToolStripButton btnShowIndentGuide2;
        private System.Windows.Forms.ToolStripDropDownButton btnOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuResultCopyQuotingWith;
        private System.Windows.Forms.ToolStripMenuItem mnuResultCopyQuotingWithNone;
        private System.Windows.Forms.ToolStripMenuItem mnuResultCopyQuotingWithDoubleQuoting;
        private System.Windows.Forms.ToolStripMenuItem mnuResultCopyQuotingWithSingleQuoting;
        private System.Windows.Forms.ToolStripMenuItem mnuResultCopyFieldSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuResultCopyFieldSeparatorComma;
        private System.Windows.Forms.ToolStripMenuItem mnuResultCopyFieldSeparatorSemicolon;
        private System.Windows.Forms.ToolStripMenuItem mnuResultCopyFieldSeparatorI;
        private System.Windows.Forms.ToolStripMenuItem mnuPreviewCLOBData;
        private C1.Win.C1Input.C1CheckBox chkRawDataMode;
        private C1.Win.C1Input.C1Button btnHelp_RawDataMode;
        private C1.C1Excel.C1XLBook c1XLBook1;
        private C1.Win.C1Ribbon.RibbonButton btnPaginationOn;
        private C1.Win.C1Ribbon.RibbonButton btnPaginationOff;
        private C1.Win.C1Ribbon.RibbonButton btnNextPage;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private C1.Win.C1Ribbon.RibbonLabel lblAverage;
        private C1.Win.C1Ribbon.RibbonLabel lblAverageValue;
        private C1.Win.C1Ribbon.RibbonLabel lblSeparator1;
        private C1.Win.C1Ribbon.RibbonLabel lblCount;
        private C1.Win.C1Ribbon.RibbonLabel lblCountValue;
        private C1.Win.C1Ribbon.RibbonLabel lblSeparator2;
        private C1.Win.C1Ribbon.RibbonLabel lblSummary;
        private C1.Win.C1Ribbon.RibbonLabel lblSummaryValue;
        private C1.Win.C1Ribbon.RibbonSeparator lblSeparator3;
        private C1.Win.C1Command.C1DockingTab c1DockingTab2;
        private C1.Win.C1Command.C1DockingTabPage tabAutoReplace;
        private System.Windows.Forms.ToolStrip tsAutoReplace;
        private System.Windows.Forms.ToolStripLabel lblAutoReplace;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private C1.Win.C1Command.C1DockingTabPage tabSchemaBrowser;
        private System.Windows.Forms.ToolStrip tsSchemaBrowser;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1GridSchemaBrowser;
        private C1.Win.C1Command.C1CommandHolder c1CommandHolder1;
        private System.Windows.Forms.ToolStripSplitButton btnExpandCollapse;
        private C1.Win.C1Input.C1TextBox txtSchemaFilter;
        private System.Windows.Forms.ToolStripMenuItem mnuCollapseAll;
        private System.Windows.Forms.ToolStripMenuItem mnuExpandAll;
        private System.Windows.Forms.Label lblSchemaFilter0;
        private System.Windows.Forms.ToolStripLabel lblSchemaFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private C1.Win.C1Ribbon.RibbonButton btnAppendingQueriesOn;
        private C1.Win.C1Ribbon.RibbonButton btnAppendingQueriesOff;
        private C1.Win.C1Ribbon.RibbonSeparator spDatabase;
        private C1.Win.C1Ribbon.RibbonButton btnDatabase;
        private System.Windows.Forms.ToolStripSplitButton btnSettingOfFocus;
        private System.Windows.Forms.ToolStripMenuItem mnuFocusOnDataGrid;
        private System.Windows.Forms.ToolStripMenuItem mnuFocusOnQueryEditor;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1GridAC4Period1;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1GridAC4Period2;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1GridAC4Space2;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1GridAC4Space1;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1GridAC4All;
        private C1.Win.C1Input.C1CheckBox chkShowGroupingRow;
    }
}