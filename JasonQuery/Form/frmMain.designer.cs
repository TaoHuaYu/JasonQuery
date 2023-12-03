namespace JasonQuery
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mnuMainForm = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuNewSQLEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFile_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_Undo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_Redo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdit_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdit_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_SelectCurrentBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdit_Comment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_UnComment = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdit_Indent = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_UnIndent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdit_UpperCase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_LowerCase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdit_SQLFormatter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewSQLEditor2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuQuery_Execute = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuery_ExecuteCurrentBlock = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuQuery_Explain = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuery_ExplainAnalyze = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuery_ExplainOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAutoRollback = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAutoCommit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuQuery_Commit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuery_Rollback = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecentFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMyFavorite = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSwitchDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSQLHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSchemaBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSchemaSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenerateSQLStatement = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCompactMDB = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSplitter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCheckForUpdatesManually = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReleaseNotes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportBugs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrTimeAndKeyStatus = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConnect = new System.Windows.Forms.ToolStripButton();
            this.btnDisconnect = new System.Windows.Forms.ToolStripButton();
            this.btnNewSQLEditor = new System.Windows.Forms.ToolStripButton();
            this.btnSetting = new System.Windows.Forms.ToolStripButton();
            this.tsMainMenuToolBar = new System.Windows.Forms.ToolStrip();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new Crownwood.Magic.Controls.TabControl();
            this.c1ThemeController1 = new C1.Win.C1Themes.C1ThemeController();
            this.c1StatusBar1 = new C1.Win.C1Ribbon.C1StatusBar();
            this.ribbonLabel1 = new C1.Win.C1Ribbon.RibbonLabel();
            this.lblInfo = new C1.Win.C1Ribbon.RibbonLabel();
            this.btnSQLServer = new C1.Win.C1Ribbon.RibbonButton();
            this.btnOracle = new C1.Win.C1Ribbon.RibbonButton();
            this.btnPostgreSQL = new C1.Win.C1Ribbon.RibbonButton();
            this.btnMySQL = new C1.Win.C1Ribbon.RibbonButton();
            this.btnSQLite = new C1.Win.C1Ribbon.RibbonButton();
            this.lblDBVersion = new C1.Win.C1Ribbon.RibbonLabel();
            this.spDBVersion = new C1.Win.C1Ribbon.RibbonSeparator();
            this.btnDatabase = new C1.Win.C1Ribbon.RibbonButton();
            this.spDatabase = new C1.Win.C1Ribbon.RibbonSeparator();
            this.btnIP = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonSeparator1 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblDomainUser = new C1.Win.C1Ribbon.RibbonLabel();
            this.spAutoRollbackOnError = new C1.Win.C1Ribbon.RibbonSeparator();
            this.btnAutoRollbackOnErrorOn = new C1.Win.C1Ribbon.RibbonButton();
            this.btnAutoRollbackOnErrorOff = new C1.Win.C1Ribbon.RibbonButton();
            this.ribbonSeparator2 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblAutoCommit = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonSeparator3 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblINS = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonSeparator4 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblNUM = new C1.Win.C1Ribbon.RibbonLabel();
            this.ribbonSeparator5 = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblCAPS = new C1.Win.C1Ribbon.RibbonLabel();
            this.spDateTime = new C1.Win.C1Ribbon.RibbonSeparator();
            this.lblDateTime = new C1.Win.C1Ribbon.RibbonLabel();
            this.tmrCheckIdleTime = new System.Windows.Forms.Timer(this.components);
            this.tmrDateTime = new System.Windows.Forms.Timer(this.components);
            this.mnuMainForm.SuspendLayout();
            this.tsMainMenuToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMainForm
            // 
            this.mnuMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuQuery,
            this.mnuRecentFiles,
            this.mnuMyFavorite,
            this.mnuSwitchDatabase,
            this.mnuTools,
            this.mnuHelp,
            this.mnuInfo});
            this.mnuMainForm.Location = new System.Drawing.Point(0, 0);
            this.mnuMainForm.Name = "mnuMainForm";
            this.mnuMainForm.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.mnuMainForm.ShowItemToolTips = true;
            this.mnuMainForm.Size = new System.Drawing.Size(1008, 26);
            this.mnuMainForm.TabIndex = 9;
            this.mnuMainForm.Text = "menuStrip1";
            this.c1ThemeController1.SetTheme(this.mnuMainForm, "(default)");
            this.mnuMainForm.MouseEnter += new System.EventHandler(this.mnuMainForm_MouseEnter);
            this.mnuMainForm.MouseLeave += new System.EventHandler(this.mnuMainForm_MouseLeave);
            this.mnuMainForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mnuMainForm_MouseMove);
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewConnection,
            this.mnuOpenConnection,
            this.mnuCloseConnection,
            this.toolStripSeparator2,
            this.mnuNewSQLEditor,
            this.mnuOpenFiles,
            this.toolStripMenuItem2,
            this.mnuFile_Save,
            this.mnuFile_SaveAs,
            this.toolStripMenuItem9,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(39, 20);
            this.mnuFile.Text = "&File";
            this.mnuFile.Click += new System.EventHandler(this.mnuFile_Click);
            this.mnuFile.MouseHover += new System.EventHandler(this.mnuFile_MouseHover);
            // 
            // mnuNewConnection
            // 
            this.mnuNewConnection.Image = ((System.Drawing.Image)(resources.GetObject("mnuNewConnection.Image")));
            this.mnuNewConnection.Name = "mnuNewConnection";
            this.mnuNewConnection.Size = new System.Drawing.Size(211, 22);
            this.mnuNewConnection.Text = "New Connection";
            this.mnuNewConnection.Click += new System.EventHandler(this.mnuNewConnection_Click);
            // 
            // mnuOpenConnection
            // 
            this.mnuOpenConnection.Name = "mnuOpenConnection";
            this.mnuOpenConnection.Size = new System.Drawing.Size(211, 22);
            this.mnuOpenConnection.Text = "Open Connection";
            this.mnuOpenConnection.Visible = false;
            this.mnuOpenConnection.Click += new System.EventHandler(this.mnuOpenConnection_Click);
            // 
            // mnuCloseConnection
            // 
            this.mnuCloseConnection.Name = "mnuCloseConnection";
            this.mnuCloseConnection.Size = new System.Drawing.Size(211, 22);
            this.mnuCloseConnection.Text = "End Connection";
            this.mnuCloseConnection.Visible = false;
            this.mnuCloseConnection.Click += new System.EventHandler(this.mnuCloseConnection_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(208, 6);
            // 
            // mnuNewSQLEditor
            // 
            this.mnuNewSQLEditor.Image = ((System.Drawing.Image)(resources.GetObject("mnuNewSQLEditor.Image")));
            this.mnuNewSQLEditor.Name = "mnuNewSQLEditor";
            this.mnuNewSQLEditor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNewSQLEditor.Size = new System.Drawing.Size(211, 22);
            this.mnuNewSQLEditor.Text = "&New SQL Editor";
            this.mnuNewSQLEditor.Click += new System.EventHandler(this.mnuNewSQLEditor_Click);
            // 
            // mnuOpenFiles
            // 
            this.mnuOpenFiles.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpenFiles.Image")));
            this.mnuOpenFiles.Name = "mnuOpenFiles";
            this.mnuOpenFiles.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpenFiles.Size = new System.Drawing.Size(211, 22);
            this.mnuOpenFiles.Text = "&Open File(s)";
            this.mnuOpenFiles.Click += new System.EventHandler(this.mnuOpenQueryFile_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(208, 6);
            // 
            // mnuFile_Save
            // 
            this.mnuFile_Save.Image = ((System.Drawing.Image)(resources.GetObject("mnuFile_Save.Image")));
            this.mnuFile_Save.Name = "mnuFile_Save";
            this.mnuFile_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuFile_Save.Size = new System.Drawing.Size(211, 22);
            this.mnuFile_Save.Tag = "Save";
            this.mnuFile_Save.Text = "&Save";
            this.mnuFile_Save.Click += new System.EventHandler(this.mnuSubFile_Click);
            // 
            // mnuFile_SaveAs
            // 
            this.mnuFile_SaveAs.Image = ((System.Drawing.Image)(resources.GetObject("mnuFile_SaveAs.Image")));
            this.mnuFile_SaveAs.Name = "mnuFile_SaveAs";
            this.mnuFile_SaveAs.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.mnuFile_SaveAs.Size = new System.Drawing.Size(211, 22);
            this.mnuFile_SaveAs.Tag = "SaveAs";
            this.mnuFile_SaveAs.Text = "Save &As";
            this.mnuFile_SaveAs.Click += new System.EventHandler(this.mnuSubFile_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(208, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Image = ((System.Drawing.Image)(resources.GetObject("mnuExit.Image")));
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuExit.Size = new System.Drawing.Size(211, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEdit_Undo,
            this.mnuEdit_Redo,
            this.toolStripMenuItem3,
            this.mnuEdit_Cut,
            this.mnuEdit_Copy,
            this.mnuEdit_Paste,
            this.mnuEdit_Delete,
            this.toolStripMenuItem4,
            this.mnuEdit_SelectAll,
            this.mnuEdit_SelectCurrentBlock,
            this.toolStripMenuItem5,
            this.mnuEdit_Comment,
            this.mnuEdit_UnComment,
            this.toolStripMenuItem6,
            this.mnuEdit_Indent,
            this.mnuEdit_UnIndent,
            this.toolStripMenuItem7,
            this.mnuEdit_UpperCase,
            this.mnuEdit_LowerCase,
            this.toolStripMenuItem8,
            this.mnuEdit_SQLFormatter});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(42, 20);
            this.mnuEdit.Text = "&Edit";
            this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
            this.mnuEdit.MouseHover += new System.EventHandler(this.mnuEdit_MouseHover);
            // 
            // mnuEdit_Undo
            // 
            this.mnuEdit_Undo.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_Undo.Image")));
            this.mnuEdit_Undo.Name = "mnuEdit_Undo";
            this.mnuEdit_Undo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuEdit_Undo.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_Undo.Tag = "Undo";
            this.mnuEdit_Undo.Text = "&Undo";
            this.mnuEdit_Undo.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // mnuEdit_Redo
            // 
            this.mnuEdit_Redo.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_Redo.Image")));
            this.mnuEdit_Redo.Name = "mnuEdit_Redo";
            this.mnuEdit_Redo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.mnuEdit_Redo.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_Redo.Tag = "Redo";
            this.mnuEdit_Redo.Text = "&Redo";
            this.mnuEdit_Redo.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(227, 6);
            // 
            // mnuEdit_Cut
            // 
            this.mnuEdit_Cut.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_Cut.Image")));
            this.mnuEdit_Cut.Name = "mnuEdit_Cut";
            this.mnuEdit_Cut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuEdit_Cut.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_Cut.Tag = "Cut";
            this.mnuEdit_Cut.Text = "Cu&t";
            this.mnuEdit_Cut.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // mnuEdit_Copy
            // 
            this.mnuEdit_Copy.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_Copy.Image")));
            this.mnuEdit_Copy.Name = "mnuEdit_Copy";
            this.mnuEdit_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuEdit_Copy.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_Copy.Tag = "Copy";
            this.mnuEdit_Copy.Text = "&Copy";
            this.mnuEdit_Copy.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // mnuEdit_Paste
            // 
            this.mnuEdit_Paste.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_Paste.Image")));
            this.mnuEdit_Paste.Name = "mnuEdit_Paste";
            this.mnuEdit_Paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuEdit_Paste.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_Paste.Tag = "Paste";
            this.mnuEdit_Paste.Text = "&Paste";
            this.mnuEdit_Paste.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // mnuEdit_Delete
            // 
            this.mnuEdit_Delete.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_Delete.Image")));
            this.mnuEdit_Delete.Name = "mnuEdit_Delete";
            this.mnuEdit_Delete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuEdit_Delete.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_Delete.Tag = "Delete";
            this.mnuEdit_Delete.Text = "&Delete";
            this.mnuEdit_Delete.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(227, 6);
            // 
            // mnuEdit_SelectAll
            // 
            this.mnuEdit_SelectAll.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_SelectAll.Image")));
            this.mnuEdit_SelectAll.Name = "mnuEdit_SelectAll";
            this.mnuEdit_SelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuEdit_SelectAll.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_SelectAll.Tag = "SelectAll";
            this.mnuEdit_SelectAll.Text = "Select &All";
            this.mnuEdit_SelectAll.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // mnuEdit_SelectCurrentBlock
            // 
            this.mnuEdit_SelectCurrentBlock.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_SelectCurrentBlock.Image")));
            this.mnuEdit_SelectCurrentBlock.Name = "mnuEdit_SelectCurrentBlock";
            this.mnuEdit_SelectCurrentBlock.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.mnuEdit_SelectCurrentBlock.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_SelectCurrentBlock.Tag = "SelectCurrentBlock";
            this.mnuEdit_SelectCurrentBlock.Text = "Select Current &Block";
            this.mnuEdit_SelectCurrentBlock.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(227, 6);
            // 
            // mnuEdit_Comment
            // 
            this.mnuEdit_Comment.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_Comment.Image")));
            this.mnuEdit_Comment.Name = "mnuEdit_Comment";
            this.mnuEdit_Comment.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_Comment.Tag = "Comment";
            this.mnuEdit_Comment.Text = "Comment";
            this.mnuEdit_Comment.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // mnuEdit_UnComment
            // 
            this.mnuEdit_UnComment.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_UnComment.Image")));
            this.mnuEdit_UnComment.Name = "mnuEdit_UnComment";
            this.mnuEdit_UnComment.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_UnComment.Tag = "Un-Comment";
            this.mnuEdit_UnComment.Text = "Un-Comment";
            this.mnuEdit_UnComment.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(227, 6);
            // 
            // mnuEdit_Indent
            // 
            this.mnuEdit_Indent.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_Indent.Image")));
            this.mnuEdit_Indent.Name = "mnuEdit_Indent";
            this.mnuEdit_Indent.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_Indent.Tag = "Indent";
            this.mnuEdit_Indent.Text = "Indent";
            this.mnuEdit_Indent.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // mnuEdit_UnIndent
            // 
            this.mnuEdit_UnIndent.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_UnIndent.Image")));
            this.mnuEdit_UnIndent.Name = "mnuEdit_UnIndent";
            this.mnuEdit_UnIndent.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_UnIndent.Tag = "Un-Indent";
            this.mnuEdit_UnIndent.Text = "Un-Indent";
            this.mnuEdit_UnIndent.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(227, 6);
            // 
            // mnuEdit_UpperCase
            // 
            this.mnuEdit_UpperCase.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_UpperCase.Image")));
            this.mnuEdit_UpperCase.Name = "mnuEdit_UpperCase";
            this.mnuEdit_UpperCase.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.U)));
            this.mnuEdit_UpperCase.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_UpperCase.Tag = "UpperCase";
            this.mnuEdit_UpperCase.Text = "UPPER Case";
            this.mnuEdit_UpperCase.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // mnuEdit_LowerCase
            // 
            this.mnuEdit_LowerCase.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_LowerCase.Image")));
            this.mnuEdit_LowerCase.Name = "mnuEdit_LowerCase";
            this.mnuEdit_LowerCase.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.mnuEdit_LowerCase.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_LowerCase.Tag = "LowerCase";
            this.mnuEdit_LowerCase.Text = "lower Case";
            this.mnuEdit_LowerCase.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(227, 6);
            // 
            // mnuEdit_SQLFormatter
            // 
            this.mnuEdit_SQLFormatter.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_SQLFormatter.Image")));
            this.mnuEdit_SQLFormatter.Name = "mnuEdit_SQLFormatter";
            this.mnuEdit_SQLFormatter.Size = new System.Drawing.Size(230, 22);
            this.mnuEdit_SQLFormatter.Tag = "SQLFormatter";
            this.mnuEdit_SQLFormatter.Text = "SQL Formatter";
            this.mnuEdit_SQLFormatter.Click += new System.EventHandler(this.mnuSubEdit_Click);
            // 
            // mnuQuery
            // 
            this.mnuQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewSQLEditor2,
            this.toolStripMenuItem10,
            this.mnuQuery_Execute,
            this.mnuQuery_ExecuteCurrentBlock,
            this.toolStripMenuItem11,
            this.mnuQuery_Explain,
            this.mnuQuery_ExplainAnalyze,
            this.mnuQuery_ExplainOptions,
            this.toolStripMenuItem12,
            this.mnuAutoRollback,
            this.mnuAutoCommit,
            this.toolStripMenuItem13,
            this.mnuQuery_Commit,
            this.mnuQuery_Rollback});
            this.mnuQuery.Name = "mnuQuery";
            this.mnuQuery.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.mnuQuery.Size = new System.Drawing.Size(54, 20);
            this.mnuQuery.Text = "&Query";
            this.mnuQuery.Visible = false;
            this.mnuQuery.Click += new System.EventHandler(this.mnuQuery_Click);
            this.mnuQuery.MouseHover += new System.EventHandler(this.mnuQuery_MouseHover);
            // 
            // mnuNewSQLEditor2
            // 
            this.mnuNewSQLEditor2.Image = ((System.Drawing.Image)(resources.GetObject("mnuNewSQLEditor2.Image")));
            this.mnuNewSQLEditor2.Name = "mnuNewSQLEditor2";
            this.mnuNewSQLEditor2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNewSQLEditor2.Size = new System.Drawing.Size(219, 22);
            this.mnuNewSQLEditor2.Text = "&New SQL Editor";
            this.mnuNewSQLEditor2.Click += new System.EventHandler(this.mnuNewSQLEditor_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(216, 6);
            // 
            // mnuQuery_Execute
            // 
            this.mnuQuery_Execute.Image = ((System.Drawing.Image)(resources.GetObject("mnuQuery_Execute.Image")));
            this.mnuQuery_Execute.Name = "mnuQuery_Execute";
            this.mnuQuery_Execute.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuQuery_Execute.Size = new System.Drawing.Size(219, 22);
            this.mnuQuery_Execute.Text = "Execute";
            // 
            // mnuQuery_ExecuteCurrentBlock
            // 
            this.mnuQuery_ExecuteCurrentBlock.Image = ((System.Drawing.Image)(resources.GetObject("mnuQuery_ExecuteCurrentBlock.Image")));
            this.mnuQuery_ExecuteCurrentBlock.Name = "mnuQuery_ExecuteCurrentBlock";
            this.mnuQuery_ExecuteCurrentBlock.Size = new System.Drawing.Size(219, 22);
            this.mnuQuery_ExecuteCurrentBlock.Text = "Execute Current Block";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(216, 6);
            // 
            // mnuQuery_Explain
            // 
            this.mnuQuery_Explain.Name = "mnuQuery_Explain";
            this.mnuQuery_Explain.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.mnuQuery_Explain.Size = new System.Drawing.Size(219, 22);
            this.mnuQuery_Explain.Text = "Explain";
            this.mnuQuery_Explain.Visible = false;
            // 
            // mnuQuery_ExplainAnalyze
            // 
            this.mnuQuery_ExplainAnalyze.Name = "mnuQuery_ExplainAnalyze";
            this.mnuQuery_ExplainAnalyze.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F7)));
            this.mnuQuery_ExplainAnalyze.Size = new System.Drawing.Size(219, 22);
            this.mnuQuery_ExplainAnalyze.Text = "Explain Analyze";
            this.mnuQuery_ExplainAnalyze.Visible = false;
            // 
            // mnuQuery_ExplainOptions
            // 
            this.mnuQuery_ExplainOptions.Name = "mnuQuery_ExplainOptions";
            this.mnuQuery_ExplainOptions.Size = new System.Drawing.Size(219, 22);
            this.mnuQuery_ExplainOptions.Text = "Explain Options";
            this.mnuQuery_ExplainOptions.Visible = false;
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(216, 6);
            this.toolStripMenuItem12.Visible = false;
            // 
            // mnuAutoRollback
            // 
            this.mnuAutoRollback.Name = "mnuAutoRollback";
            this.mnuAutoRollback.Size = new System.Drawing.Size(219, 22);
            this.mnuAutoRollback.Text = "Auto-Rollback";
            // 
            // mnuAutoCommit
            // 
            this.mnuAutoCommit.Enabled = false;
            this.mnuAutoCommit.Name = "mnuAutoCommit";
            this.mnuAutoCommit.Size = new System.Drawing.Size(219, 22);
            this.mnuAutoCommit.Text = "Auto-Commit";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(216, 6);
            // 
            // mnuQuery_Commit
            // 
            this.mnuQuery_Commit.Name = "mnuQuery_Commit";
            this.mnuQuery_Commit.Size = new System.Drawing.Size(219, 22);
            this.mnuQuery_Commit.Text = "Commit";
            // 
            // mnuQuery_Rollback
            // 
            this.mnuQuery_Rollback.Name = "mnuQuery_Rollback";
            this.mnuQuery_Rollback.Size = new System.Drawing.Size(219, 22);
            this.mnuQuery_Rollback.Text = "Rollback";
            // 
            // mnuRecentFiles
            // 
            this.mnuRecentFiles.Enabled = false;
            this.mnuRecentFiles.Name = "mnuRecentFiles";
            this.mnuRecentFiles.Size = new System.Drawing.Size(86, 20);
            this.mnuRecentFiles.Text = "&Recent Files";
            // 
            // mnuMyFavorite
            // 
            this.mnuMyFavorite.Enabled = false;
            this.mnuMyFavorite.Name = "mnuMyFavorite";
            this.mnuMyFavorite.Size = new System.Drawing.Size(86, 20);
            this.mnuMyFavorite.Text = "&My Favorite";
            // 
            // mnuSwitchDatabase
            // 
            this.mnuSwitchDatabase.Name = "mnuSwitchDatabase";
            this.mnuSwitchDatabase.Size = new System.Drawing.Size(113, 20);
            this.mnuSwitchDatabase.Text = "&Switch Database";
            this.mnuSwitchDatabase.Visible = false;
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptions,
            this.toolStripMenuItem1,
            this.mnuSQLHistory,
            this.mnuSchemaBrowser,
            this.mnuSchemaSearch,
            this.mnuGenerateSQLStatement,
            this.mnuCompactMDB,
            this.mnuFileSplitter});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(51, 20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuOptions
            // 
            this.mnuOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuOptions.Image")));
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(235, 22);
            this.mnuOptions.Text = "Options";
            this.mnuOptions.Click += new System.EventHandler(this.mnuOptions_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(232, 6);
            // 
            // mnuSQLHistory
            // 
            this.mnuSQLHistory.Image = ((System.Drawing.Image)(resources.GetObject("mnuSQLHistory.Image")));
            this.mnuSQLHistory.Name = "mnuSQLHistory";
            this.mnuSQLHistory.Size = new System.Drawing.Size(235, 22);
            this.mnuSQLHistory.Text = "SQL History";
            this.mnuSQLHistory.Click += new System.EventHandler(this.mnuSQLHistory_Click);
            // 
            // mnuSchemaBrowser
            // 
            this.mnuSchemaBrowser.Image = ((System.Drawing.Image)(resources.GetObject("mnuSchemaBrowser.Image")));
            this.mnuSchemaBrowser.Name = "mnuSchemaBrowser";
            this.mnuSchemaBrowser.Size = new System.Drawing.Size(235, 22);
            this.mnuSchemaBrowser.Text = "Schema Browser";
            this.mnuSchemaBrowser.Click += new System.EventHandler(this.mnuSchemaBrowser_Click);
            // 
            // mnuSchemaSearch
            // 
            this.mnuSchemaSearch.Image = ((System.Drawing.Image)(resources.GetObject("mnuSchemaSearch.Image")));
            this.mnuSchemaSearch.Name = "mnuSchemaSearch";
            this.mnuSchemaSearch.Size = new System.Drawing.Size(235, 22);
            this.mnuSchemaSearch.Text = "Advanced Search in Schema";
            this.mnuSchemaSearch.Click += new System.EventHandler(this.mnuSchemaSearch_Click);
            // 
            // mnuGenerateSQLStatement
            // 
            this.mnuGenerateSQLStatement.Image = ((System.Drawing.Image)(resources.GetObject("mnuGenerateSQLStatement.Image")));
            this.mnuGenerateSQLStatement.Name = "mnuGenerateSQLStatement";
            this.mnuGenerateSQLStatement.Size = new System.Drawing.Size(235, 22);
            this.mnuGenerateSQLStatement.Text = "Generate SQL Statement";
            this.mnuGenerateSQLStatement.Click += new System.EventHandler(this.mnuGenerateSQLStatement_Click);
            // 
            // mnuCompactMDB
            // 
            this.mnuCompactMDB.Name = "mnuCompactMDB";
            this.mnuCompactMDB.Size = new System.Drawing.Size(235, 22);
            this.mnuCompactMDB.Text = "Compact \'JasonQuery.db\'";
            this.mnuCompactMDB.Visible = false;
            this.mnuCompactMDB.Click += new System.EventHandler(this.mnuCompactMDB_Click);
            // 
            // mnuFileSplitter
            // 
            this.mnuFileSplitter.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileSplitter.Image")));
            this.mnuFileSplitter.Name = "mnuFileSplitter";
            this.mnuFileSplitter.Size = new System.Drawing.Size(235, 22);
            this.mnuFileSplitter.Text = "Large Text File Splitter";
            this.mnuFileSplitter.Click += new System.EventHandler(this.mnuFileSplitter_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCheckForUpdatesManually,
            this.mnuReleaseNotes,
            this.mnuReportBugs,
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(47, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuCheckForUpdatesManually
            // 
            this.mnuCheckForUpdatesManually.Image = ((System.Drawing.Image)(resources.GetObject("mnuCheckForUpdatesManually.Image")));
            this.mnuCheckForUpdatesManually.Name = "mnuCheckForUpdatesManually";
            this.mnuCheckForUpdatesManually.Size = new System.Drawing.Size(178, 22);
            this.mnuCheckForUpdatesManually.Text = "Check for updates";
            this.mnuCheckForUpdatesManually.Click += new System.EventHandler(this.mnuCheckForUpdatesManually_Click);
            // 
            // mnuReleaseNotes
            // 
            this.mnuReleaseNotes.Image = ((System.Drawing.Image)(resources.GetObject("mnuReleaseNotes.Image")));
            this.mnuReleaseNotes.Name = "mnuReleaseNotes";
            this.mnuReleaseNotes.Size = new System.Drawing.Size(178, 22);
            this.mnuReleaseNotes.Text = "Release Notes";
            this.mnuReleaseNotes.ToolTipText = "Open the \"releasenotes.html\" with your default browser";
            this.mnuReleaseNotes.Click += new System.EventHandler(this.mnuReleaseNotes_Click);
            // 
            // mnuReportBugs
            // 
            this.mnuReportBugs.Image = ((System.Drawing.Image)(resources.GetObject("mnuReportBugs.Image")));
            this.mnuReportBugs.Name = "mnuReportBugs";
            this.mnuReportBugs.Size = new System.Drawing.Size(178, 22);
            this.mnuReportBugs.Text = "Report Bugs";
            this.mnuReportBugs.ToolTipText = "Open the \"reportbugs.html\" with your default browser";
            this.mnuReportBugs.Click += new System.EventHandler(this.mnuReportBugs_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Image = ((System.Drawing.Image)(resources.GetObject("mnuAbout.Image")));
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(178, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuInfo
            // 
            this.mnuInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.mnuInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuInfo.Enabled = false;
            this.mnuInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mnuInfo.Name = "mnuInfo";
            this.mnuInfo.Size = new System.Drawing.Size(12, 20);
            // 
            // tmrTimeAndKeyStatus
            // 
            this.tmrTimeAndKeyStatus.Enabled = true;
            this.tmrTimeAndKeyStatus.Tick += new System.EventHandler(this.tmrTimeAndKeyStatus_Tick);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 212);
            this.panel1.TabIndex = 3;
            this.c1ThemeController1.SetTheme(this.panel1, "(default)");
            // 
            // btnConnect
            // 
            this.btnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConnect.Image = ((System.Drawing.Image)(resources.GetObject("btnConnect.Image")));
            this.btnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(28, 28);
            this.btnConnect.Text = "toolStripButton2";
            this.btnConnect.ToolTipText = "New Connection";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("btnDisconnect.Image")));
            this.btnDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(28, 28);
            this.btnDisconnect.Text = "toolStripButton1";
            this.btnDisconnect.ToolTipText = "End Connection";
            // 
            // btnNewSQLEditor
            // 
            this.btnNewSQLEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewSQLEditor.Image = ((System.Drawing.Image)(resources.GetObject("btnNewSQLEditor.Image")));
            this.btnNewSQLEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewSQLEditor.Name = "btnNewSQLEditor";
            this.btnNewSQLEditor.Size = new System.Drawing.Size(28, 28);
            this.btnNewSQLEditor.Tag = "Open New SQL Editor";
            this.btnNewSQLEditor.ToolTipText = "Open New SQL Editor";
            this.btnNewSQLEditor.Click += new System.EventHandler(this.btnNewSQLEditor_Click);
            this.btnNewSQLEditor.MouseEnter += new System.EventHandler(this.btnMouseEnter);
            this.btnNewSQLEditor.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            // 
            // btnSetting
            // 
            this.btnSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSetting.Image = ((System.Drawing.Image)(resources.GetObject("btnSetting.Image")));
            this.btnSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(28, 28);
            this.btnSetting.Tag = "Open Setting Form";
            this.btnSetting.ToolTipText = "Open Setting Form";
            this.btnSetting.MouseEnter += new System.EventHandler(this.btnMouseEnter);
            this.btnSetting.MouseLeave += new System.EventHandler(this.btnMouseLeave);
            // 
            // tsMainMenuToolBar
            // 
            this.tsMainMenuToolBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsMainMenuToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnect,
            this.btnDisconnect,
            this.btnNewSQLEditor,
            this.btnSetting});
            this.tsMainMenuToolBar.Location = new System.Drawing.Point(0, 26);
            this.tsMainMenuToolBar.Name = "tsMainMenuToolBar";
            this.tsMainMenuToolBar.Size = new System.Drawing.Size(1008, 31);
            this.tsMainMenuToolBar.TabIndex = 13;
            this.tsMainMenuToolBar.Text = "toolStrip1";
            this.c1ThemeController1.SetTheme(this.tsMainMenuToolBar, "(default)");
            this.tsMainMenuToolBar.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(9, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 3;
            this.c1ThemeController1.SetTheme(this.panel2, "(default)");
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(17, 76);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(472, 335);
            this.panel3.TabIndex = 15;
            this.c1ThemeController1.SetTheme(this.panel3, "(default)");
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(330, 193);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.c1ThemeController1.SetTheme(this.groupBox1, "(default)");
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = false;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.BoldSelectedPage = true;
            this.tabControl1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControl1.ForeColor = System.Drawing.Color.Empty;
            this.tabControl1.IDEPixelArea = false;
            this.tabControl1.Location = new System.Drawing.Point(0, 26);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.PositionTop = true;
            this.tabControl1.ShowArrows = true;
            this.tabControl1.Size = new System.Drawing.Size(1008, 681);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.TextColor = System.Drawing.Color.Empty;
            this.tabControl1.TextInactiveColor = System.Drawing.Color.Silver;
            this.tabControl1.ClosePressed += new System.EventHandler(this.tabControl1_ClosePressed);
            this.tabControl1.SelectionChanged += new System.EventHandler(this.tabControl1_SelectionChanged);
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            this.tabControl1.MouseEnter += new System.EventHandler(this.tabControl1_MouseEnter);
            this.tabControl1.MouseLeave += new System.EventHandler(this.tabControl1_MouseLeave);
            this.tabControl1.MouseHover += new System.EventHandler(this.tabControl1_MouseHover);
            this.tabControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseMove);
            // 
            // c1StatusBar1
            // 
            this.c1StatusBar1.LeftPaneItems.Add(this.ribbonLabel1);
            this.c1StatusBar1.LeftPaneItems.Add(this.lblInfo);
            this.c1StatusBar1.Location = new System.Drawing.Point(0, 707);
            this.c1StatusBar1.Name = "c1StatusBar1";
            this.c1StatusBar1.RightPaneItems.Add(this.btnSQLServer);
            this.c1StatusBar1.RightPaneItems.Add(this.btnOracle);
            this.c1StatusBar1.RightPaneItems.Add(this.btnPostgreSQL);
            this.c1StatusBar1.RightPaneItems.Add(this.btnMySQL);
            this.c1StatusBar1.RightPaneItems.Add(this.btnSQLite);
            this.c1StatusBar1.RightPaneItems.Add(this.lblDBVersion);
            this.c1StatusBar1.RightPaneItems.Add(this.spDBVersion);
            this.c1StatusBar1.RightPaneItems.Add(this.btnDatabase);
            this.c1StatusBar1.RightPaneItems.Add(this.spDatabase);
            this.c1StatusBar1.RightPaneItems.Add(this.btnIP);
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonSeparator1);
            this.c1StatusBar1.RightPaneItems.Add(this.lblDomainUser);
            this.c1StatusBar1.RightPaneItems.Add(this.spAutoRollbackOnError);
            this.c1StatusBar1.RightPaneItems.Add(this.btnAutoRollbackOnErrorOn);
            this.c1StatusBar1.RightPaneItems.Add(this.btnAutoRollbackOnErrorOff);
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonSeparator2);
            this.c1StatusBar1.RightPaneItems.Add(this.lblAutoCommit);
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonSeparator3);
            this.c1StatusBar1.RightPaneItems.Add(this.lblINS);
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonSeparator4);
            this.c1StatusBar1.RightPaneItems.Add(this.lblNUM);
            this.c1StatusBar1.RightPaneItems.Add(this.ribbonSeparator5);
            this.c1StatusBar1.RightPaneItems.Add(this.lblCAPS);
            this.c1StatusBar1.RightPaneItems.Add(this.spDateTime);
            this.c1StatusBar1.RightPaneItems.Add(this.lblDateTime);
            this.c1StatusBar1.Size = new System.Drawing.Size(1008, 23);
            this.c1ThemeController1.SetTheme(this.c1StatusBar1, "(default)");
            this.c1StatusBar1.VisualStyle = C1.Win.C1Ribbon.VisualStyle.Office2010Silver;
            // 
            // ribbonLabel1
            // 
            this.ribbonLabel1.Name = "ribbonLabel1";
            this.ribbonLabel1.Text = "      ";
            // 
            // lblInfo
            // 
            this.lblInfo.Name = "lblInfo";
            // 
            // btnSQLServer
            // 
            this.btnSQLServer.Name = "btnSQLServer";
            this.btnSQLServer.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSQLServer.SmallImage")));
            this.btnSQLServer.Visible = false;
            // 
            // btnOracle
            // 
            this.btnOracle.Name = "btnOracle";
            this.btnOracle.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnOracle.SmallImage")));
            this.btnOracle.Visible = false;
            // 
            // btnPostgreSQL
            // 
            this.btnPostgreSQL.Name = "btnPostgreSQL";
            this.btnPostgreSQL.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnPostgreSQL.SmallImage")));
            this.btnPostgreSQL.Visible = false;
            // 
            // btnMySQL
            // 
            this.btnMySQL.Name = "btnMySQL";
            this.btnMySQL.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnMySQL.SmallImage")));
            this.btnMySQL.Visible = false;
            // 
            // btnSQLite
            // 
            this.btnSQLite.Name = "btnSQLite";
            this.btnSQLite.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSQLite.SmallImage")));
            this.btnSQLite.Visible = false;
            // 
            // lblDBVersion
            // 
            this.lblDBVersion.Name = "lblDBVersion";
            // 
            // spDBVersion
            // 
            this.spDBVersion.Name = "spDBVersion";
            this.spDBVersion.Visible = false;
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
            // btnIP
            // 
            this.btnIP.Name = "btnIP";
            this.btnIP.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnIP.SmallImage")));
            // 
            // ribbonSeparator1
            // 
            this.ribbonSeparator1.Name = "ribbonSeparator1";
            // 
            // lblDomainUser
            // 
            this.lblDomainUser.Name = "lblDomainUser";
            this.lblDomainUser.Text = "DomainUser";
            // 
            // spAutoRollbackOnError
            // 
            this.spAutoRollbackOnError.Name = "spAutoRollbackOnError";
            this.spAutoRollbackOnError.Visible = false;
            // 
            // btnAutoRollbackOnErrorOn
            // 
            this.btnAutoRollbackOnErrorOn.Name = "btnAutoRollbackOnErrorOn";
            this.btnAutoRollbackOnErrorOn.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnAutoRollbackOnErrorOn.SmallImage")));
            this.btnAutoRollbackOnErrorOn.Visible = false;
            this.btnAutoRollbackOnErrorOn.Click += new System.EventHandler(this.btnAutoRollbackOnError_Click);
            // 
            // btnAutoRollbackOnErrorOff
            // 
            this.btnAutoRollbackOnErrorOff.Name = "btnAutoRollbackOnErrorOff";
            this.btnAutoRollbackOnErrorOff.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnAutoRollbackOnErrorOff.SmallImage")));
            this.btnAutoRollbackOnErrorOff.Visible = false;
            this.btnAutoRollbackOnErrorOff.Click += new System.EventHandler(this.btnAutoRollbackOnError_Click);
            // 
            // ribbonSeparator2
            // 
            this.ribbonSeparator2.Name = "ribbonSeparator2";
            // 
            // lblAutoCommit
            // 
            this.lblAutoCommit.Name = "lblAutoCommit";
            this.lblAutoCommit.Text = "Auto Commit is off";
            // 
            // ribbonSeparator3
            // 
            this.ribbonSeparator3.Name = "ribbonSeparator3";
            // 
            // lblINS
            // 
            this.lblINS.Name = "lblINS";
            this.lblINS.Text = "INS";
            // 
            // ribbonSeparator4
            // 
            this.ribbonSeparator4.Name = "ribbonSeparator4";
            // 
            // lblNUM
            // 
            this.lblNUM.Name = "lblNUM";
            this.lblNUM.Text = "NUM";
            // 
            // ribbonSeparator5
            // 
            this.ribbonSeparator5.Name = "ribbonSeparator5";
            // 
            // lblCAPS
            // 
            this.lblCAPS.Name = "lblCAPS";
            this.lblCAPS.Text = "CAPS";
            // 
            // spDateTime
            // 
            this.spDateTime.Name = "spDateTime";
            // 
            // lblDateTime
            // 
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Text = "2018/01/21 15:01:20";
            // 
            // tmrCheckIdleTime
            // 
            this.tmrCheckIdleTime.Tick += new System.EventHandler(this.tmrCheckIdleTime_Tick);
            // 
            // tmrDateTime
            // 
            this.tmrDateTime.Enabled = true;
            this.tmrDateTime.Tick += new System.EventHandler(this.tmrDateTime_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.c1StatusBar1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tsMainMenuToolBar);
            this.Controls.Add(this.mnuMainForm);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "JasonQuery";
            this.Text = "JasonQuery";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.mnuMainForm.ResumeLayout(false);
            this.mnuMainForm.PerformLayout();
            this.tsMainMenuToolBar.ResumeLayout(false);
            this.tsMainMenuToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1StatusBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mnuMainForm;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNewConnection;
        private System.Windows.Forms.Timer tmrTimeAndKeyStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton btnConnect;
        private System.Windows.Forms.ToolStripButton btnDisconnect;
        private System.Windows.Forms.ToolStripButton btnNewSQLEditor;
        private System.Windows.Forms.ToolStripButton btnSetting;
        private System.Windows.Forms.ToolStrip tsMainMenuToolBar;
        private System.Windows.Forms.Panel panel2;
        private Crownwood.Magic.Controls.TabControl tabControl1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuSQLHistory;
        private System.Windows.Forms.ToolStripMenuItem mnuSchemaBrowser;
        private System.Windows.Forms.ToolStripMenuItem mnuCheckForUpdatesManually;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuQuery;
        private System.Windows.Forms.ToolStripMenuItem mnuNewSQLEditor2;
        private System.Windows.Forms.ToolStripMenuItem mnuRecentFiles;
        private System.Windows.Forms.ToolStripMenuItem mnuMyFavorite;
        private System.Windows.Forms.ToolStripMenuItem mnuCompactMDB;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuNewSQLEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenFiles;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSplitter;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Undo;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Redo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Cut;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Copy;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Paste;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_SelectAll;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_SelectCurrentBlock;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Comment;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_UnComment;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Indent;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_UnIndent;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_UpperCase;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_LowerCase;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_SQLFormatter;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Delete;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Save;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_SaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem mnuQuery_Execute;
        private System.Windows.Forms.ToolStripMenuItem mnuQuery_ExecuteCurrentBlock;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem mnuQuery_Explain;
        private System.Windows.Forms.ToolStripMenuItem mnuQuery_ExplainAnalyze;
        private System.Windows.Forms.ToolStripMenuItem mnuQuery_ExplainOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem mnuAutoRollback;
        private System.Windows.Forms.ToolStripMenuItem mnuAutoCommit;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem mnuQuery_Commit;
        private System.Windows.Forms.ToolStripMenuItem mnuQuery_Rollback;
        private C1.Win.C1Themes.C1ThemeController c1ThemeController1;
        private System.Windows.Forms.Timer tmrCheckIdleTime;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenConnection;
        private System.Windows.Forms.ToolStripMenuItem mnuReleaseNotes;
        private System.Windows.Forms.ToolStripMenuItem mnuInfo;
        private System.Windows.Forms.ToolStripMenuItem mnuReportBugs;
        private C1.Win.C1Ribbon.RibbonLabel lblDateTime;
        private C1.Win.C1Ribbon.RibbonSeparator spDateTime;
        private C1.Win.C1Ribbon.RibbonLabel lblCAPS;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator5;
        private C1.Win.C1Ribbon.RibbonLabel lblNUM;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator4;
        private C1.Win.C1Ribbon.RibbonLabel lblINS;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator3;
        private C1.Win.C1Ribbon.RibbonLabel lblAutoCommit;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator2;
        private C1.Win.C1Ribbon.RibbonSeparator spAutoRollbackOnError;
        private C1.Win.C1Ribbon.RibbonLabel lblDomainUser;
        private C1.Win.C1Ribbon.RibbonSeparator ribbonSeparator1;
        private C1.Win.C1Ribbon.RibbonLabel lblInfo;
        private C1.Win.C1Ribbon.RibbonLabel ribbonLabel1;
        private C1.Win.C1Ribbon.C1StatusBar c1StatusBar1;
        private C1.Win.C1Ribbon.RibbonButton btnSQLServer;
        private C1.Win.C1Ribbon.RibbonButton btnOracle;
        private C1.Win.C1Ribbon.RibbonButton btnPostgreSQL;
        private C1.Win.C1Ribbon.RibbonButton btnMySQL;
        private C1.Win.C1Ribbon.RibbonLabel lblDBVersion;
        private C1.Win.C1Ribbon.RibbonSeparator spDBVersion;
        private C1.Win.C1Ribbon.RibbonButton btnIP;
        private System.Windows.Forms.Timer tmrDateTime;
        private C1.Win.C1Ribbon.RibbonButton btnDatabase;
        private C1.Win.C1Ribbon.RibbonSeparator spDatabase;
        private C1.Win.C1Ribbon.RibbonButton btnSQLite;
        private System.Windows.Forms.ToolStripMenuItem mnuSchemaSearch;
        private C1.Win.C1Ribbon.RibbonButton btnAutoRollbackOnErrorOn;
        private C1.Win.C1Ribbon.RibbonButton btnAutoRollbackOnErrorOff;
        private System.Windows.Forms.ToolStripMenuItem mnuSwitchDatabase;
        private System.Windows.Forms.ToolStripMenuItem mnuGenerateSQLStatement;
    }
}

