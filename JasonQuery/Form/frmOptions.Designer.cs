using JasonLibrary;

namespace JasonQuery
{
    sealed partial class frmOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
            this.grpEditorColors = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblToolstripBackground = new System.Windows.Forms.Label();
            this.pnlString = new System.Windows.Forms.Panel();
            this.lblSelectedTextBackground = new System.Windows.Forms.Label();
            this.lblString = new System.Windows.Forms.Label();
            this.pnlToolstripBackground = new System.Windows.Forms.Panel();
            this.lblCharacter = new System.Windows.Forms.Label();
            this.pnlSelectedTextBackground = new System.Windows.Forms.Panel();
            this.pnlBuiltInKeywords = new System.Windows.Forms.Panel();
            this.lblUserDefinedKeywords = new System.Windows.Forms.Label();
            this.pnlCharacter = new System.Windows.Forms.Panel();
            this.pnlUserDefinedKeywords = new System.Windows.Forms.Panel();
            this.lblBuiltInKeywords = new System.Windows.Forms.Label();
            this.pnlOperatorKeywords = new System.Windows.Forms.Panel();
            this.lblUserTables = new System.Windows.Forms.Label();
            this.lblOperatorKeywords = new System.Windows.Forms.Label();
            this.pnlIdentifier = new System.Windows.Forms.Panel();
            this.pnlUserFunctions = new System.Windows.Forms.Panel();
            this.pnlUserTables = new System.Windows.Forms.Panel();
            this.lblUserFunctions = new System.Windows.Forms.Label();
            this.lblIdentifier = new System.Windows.Forms.Label();
            this.lblWhiteSpace = new System.Windows.Forms.Label();
            this.lblBuiltInFunctions = new System.Windows.Forms.Label();
            this.pnlWhiteSpace = new System.Windows.Forms.Panel();
            this.pnlComments = new System.Windows.Forms.Panel();
            this.pnlOperatorSymbol = new System.Windows.Forms.Panel();
            this.pnlBuiltInFunctions = new System.Windows.Forms.Panel();
            this.lblOperatorSymbol = new System.Windows.Forms.Label();
            this.lblComments = new System.Windows.Forms.Label();
            this.lblEditorBackground = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblCurrentLineBackground = new System.Windows.Forms.Label();
            this.pnlCurrentLineBackground = new System.Windows.Forms.Panel();
            this.pnlEditorBackground = new System.Windows.Forms.Panel();
            this.pnlNumber = new System.Windows.Forms.Panel();
            this.grpPreferences = new System.Windows.Forms.GroupBox();
            this.grpIndent = new System.Windows.Forms.GroupBox();
            this.chkReplaceTabWithSpace = new C1.Win.C1Input.C1CheckBox();
            this.lblStarTabWidth = new System.Windows.Forms.Label();
            this.chkShowIndentGuide = new C1.Win.C1Input.C1CheckBox();
            this.cboTabWidth = new C1.Win.C1Input.C1ComboBox();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblTabWidth = new System.Windows.Forms.Label();
            this.lblIndentMode = new System.Windows.Forms.Label();
            this.cboIndentMode = new C1.Win.C1Input.C1ComboBox();
            this.lblStarShowIndentGuide = new System.Windows.Forms.Label();
            this.grpIndicate = new System.Windows.Forms.GroupBox();
            this.cboBookmarkStyle = new C1.Win.C1Input.C1ComboBox();
            this.editorIndicator = new JasonLibrary.ScintillaEditor();
            this.lblStarIndicate = new System.Windows.Forms.Label();
            this.lblErrorLineBackground = new System.Windows.Forms.Label();
            this.pnlErrorLineBackground = new System.Windows.Forms.Panel();
            this.lblBookmarkBackground = new System.Windows.Forms.Label();
            this.pnlBookmarkBackground = new System.Windows.Forms.Panel();
            this.lblBookmarkStyle = new System.Windows.Forms.Label();
            this.chkCtrlMouseWheel1 = new C1.Win.C1Input.C1CheckBox();
            this.grpWordWrap = new System.Windows.Forms.GroupBox();
            this.chkMargin = new C1.Win.C1Input.C1CheckBox();
            this.chkEnd = new C1.Win.C1Input.C1CheckBox();
            this.chkStart = new C1.Win.C1Input.C1CheckBox();
            this.chkWordWrap = new C1.Win.C1Input.C1CheckBox();
            this.lblStarWordWrap = new System.Windows.Forms.Label();
            this.chkOpenFileOnCurrentTab = new C1.Win.C1Input.C1CheckBox();
            this.chkShowSaveAsButton = new C1.Win.C1Input.C1CheckBox();
            this.cboEditorZoom = new C1.Win.C1Input.C1ComboBox();
            this.cboEditorFontSize = new C1.Win.C1Input.C1ComboBox();
            this.cboSaveAsEncoding = new C1.Win.C1Input.C1ComboBox();
            this.chkEntireBlankRowAsEmptyRow4SelectBlock = new C1.Win.C1Input.C1CheckBox();
            this.chkHighlightSelectedText = new C1.Win.C1Input.C1CheckBox();
            this.chkSaveAsEncoding = new C1.Win.C1Input.C1CheckBox();
            this.chkCopyAsHTML = new C1.Win.C1Input.C1CheckBox();
            this.chkBold = new C1.Win.C1Input.C1CheckBox();
            this.cboEditorFontPicker = new C1.Win.C1Input.C1FontPicker();
            this.grpHighlight = new System.Windows.Forms.GroupBox();
            this.cboHighlightStyle = new C1.Win.C1Input.C1ComboBox();
            this.cboHighlightAlpha = new C1.Win.C1Input.C1ComboBox();
            this.cboHighlightOutlineAlpha = new C1.Win.C1Input.C1ComboBox();
            this.lblStarHighlight = new System.Windows.Forms.Label();
            this.lblHighlightColorAlpha = new System.Windows.Forms.Label();
            this.lblHighlightColorOutlineAlpha = new System.Windows.Forms.Label();
            this.lblHighlightColorStyle = new System.Windows.Forms.Label();
            this.pnlHighlightForeColor = new System.Windows.Forms.Panel();
            this.lblHighlightColorForeColor = new System.Windows.Forms.Label();
            this.lblStarHighlightSelection = new System.Windows.Forms.Label();
            this.lblEditorFontName = new System.Windows.Forms.Label();
            this.lblEditorFontSize = new System.Windows.Forms.Label();
            this.lblEditorZoom = new System.Windows.Forms.Label();
            this.chkShowAllCharacters = new C1.Win.C1Input.C1CheckBox();
            this.chkHighlightSelection = new C1.Win.C1Input.C1CheckBox();
            this.grpColorTheme = new System.Windows.Forms.GroupBox();
            this.btnHelp_DarkMode = new C1.Win.C1Input.C1Button();
            this.chkDarkMode = new C1.Win.C1Input.C1CheckBox();
            this.grpQueryEditorPreview = new System.Windows.Forms.GroupBox();
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
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnComment = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveComment = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnIndent = new System.Windows.Forms.ToolStripButton();
            this.txtIndentWord = new System.Windows.Forms.ToolStripTextBox();
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
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.editor = new JasonLibrary.ScintillaEditor();
            this.grpAutoComplete = new System.Windows.Forms.GroupBox();
            this.chkFirstCharChecking = new C1.Win.C1Input.C1CheckBox();
            this.lblStarAC = new System.Windows.Forms.Label();
            this.nudMinFragmentLength = new System.Windows.Forms.NumericUpDown();
            this.chkEnableAutoComplete = new C1.Win.C1Input.C1CheckBox();
            this.lblMinFragmentLength = new System.Windows.Forms.Label();
            this.grpAutoCompleteFor = new System.Windows.Forms.GroupBox();
            this.chkUserDefinedViews = new C1.Win.C1Input.C1CheckBox();
            this.chkUserDefinedTriggers = new C1.Win.C1Input.C1CheckBox();
            this.chkUserDefinedTables = new C1.Win.C1Input.C1CheckBox();
            this.chkUserDefinedFunctions = new C1.Win.C1Input.C1CheckBox();
            this.chkUserDefinedKeywords = new C1.Win.C1Input.C1CheckBox();
            this.chkBuiltInKeywords = new C1.Win.C1Input.C1CheckBox();
            this.chkBuiltInFunctions = new C1.Win.C1Input.C1CheckBox();
            this.grpAutoReplace = new System.Windows.Forms.GroupBox();
            this.lblARInfo2 = new System.Windows.Forms.Label();
            this.chkShowFilterRowAR = new C1.Win.C1Input.C1CheckBox();
            this.lblARInfo1 = new System.Windows.Forms.Label();
            this.lblStarAR = new System.Windows.Forms.Label();
            this.grpModifyDefinitionAR = new System.Windows.Forms.GroupBox();
            this.editorAR = new JasonLibrary.ScintillaEditor();
            this.lblTips = new System.Windows.Forms.Label();
            this.txtKeyword = new C1.Win.C1Input.C1TextBox();
            this.btnClearAR = new C1.Win.C1Input.C1Button();
            this.btnCancelAR = new C1.Win.C1Input.C1Button();
            this.btnSaveAR = new C1.Win.C1Input.C1Button();
            this.lblReplacement = new System.Windows.Forms.Label();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.grpDefinitionAR = new System.Windows.Forms.GroupBox();
            this.btnDeleteAR = new C1.Win.C1Input.C1Button();
            this.btnEditAR = new C1.Win.C1Input.C1Button();
            this.btnAddAR = new C1.Win.C1Input.C1Button();
            this.c1GridARInfo = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.chkEnableAutoReplace = new C1.Win.C1Input.C1CheckBox();
            this.grpDataGrid = new System.Windows.Forms.GroupBox();
            this.chkShowGroupingRow = new C1.Win.C1Input.C1CheckBox();
            this.btnHelp_AppendingQueries = new C1.Win.C1Input.C1Button();
            this.chkAppendingQueries = new C1.Win.C1Input.C1CheckBox();
            this.chkSetFocusAfterQuery = new C1.Win.C1Input.C1CheckBox();
            this.cboRowsPerPage = new C1.Win.C1Input.C1ComboBox();
            this.lblRowsPerPage = new System.Windows.Forms.Label();
            this.chkPagingQuery = new C1.Win.C1Input.C1CheckBox();
            this.btnHelp_RawDataMode = new C1.Win.C1Input.C1Button();
            this.chkRawDataMode = new C1.Win.C1Input.C1CheckBox();
            this.chkPreviewCLOBData = new C1.Win.C1Input.C1CheckBox();
            this.chkUseReadOnlyQueries = new C1.Win.C1Input.C1CheckBox();
            this.chkCtrlMouseWheel2 = new C1.Win.C1Input.C1CheckBox();
            this.cboGridRowHeightResizing = new C1.Win.C1Input.C1ComboBox();
            this.cboGridFontSize = new C1.Win.C1Input.C1ComboBox();
            this.cboGridZoom = new C1.Win.C1Input.C1ComboBox();
            this.cboGridVisualStyle = new C1.Win.C1Input.C1ComboBox();
            this.cboResultCopyFieldSeparator = new C1.Win.C1Input.C1ComboBox();
            this.cboResultCopyQuotingWith = new C1.Win.C1Input.C1ComboBox();
            this.cboMaxWidth = new C1.Win.C1Input.C1ComboBox();
            this.chkResize = new C1.Win.C1Input.C1CheckBox();
            this.chkSort = new C1.Win.C1Input.C1CheckBox();
            this.chkShowFilterRow = new C1.Win.C1Input.C1CheckBox();
            this.chkShowStreamlinedName = new C1.Win.C1Input.C1CheckBox();
            this.chkShowColumnType = new C1.Win.C1Input.C1CheckBox();
            this.cboGridFontPicker = new C1.Win.C1Input.C1FontPicker();
            this.lblMaxWidth = new System.Windows.Forms.Label();
            this.lblGridVisualStyle = new System.Windows.Forms.Label();
            this.lblGridFontSize = new System.Windows.Forms.Label();
            this.lblGridZoom = new System.Windows.Forms.Label();
            this.lblResultCopyFieldSeparator = new System.Windows.Forms.Label();
            this.lblGridFontName = new System.Windows.Forms.Label();
            this.lblResultCopyQuotingWith = new System.Windows.Forms.Label();
            this.grpNullValueStyle = new System.Windows.Forms.GroupBox();
            this.cboNullShowAs = new C1.Win.C1Input.C1ComboBox();
            this.pnlNullValueForeColor = new System.Windows.Forms.Panel();
            this.lblNullValueForeColor = new System.Windows.Forms.Label();
            this.lblNullValueShowAs = new System.Windows.Forms.Label();
            this.lblGridRowHeightResizing = new System.Windows.Forms.Label();
            this.grpDataGridColor = new System.Windows.Forms.GroupBox();
            this.lblGridHeadingForeColor = new System.Windows.Forms.Label();
            this.lblStarGridColor = new System.Windows.Forms.Label();
            this.pnlGridHeadingForeColor = new System.Windows.Forms.Panel();
            this.pnlGridSelectedBackColor = new System.Windows.Forms.Panel();
            this.lblGridSelectedBackColor = new System.Windows.Forms.Label();
            this.pnlGridSelectedForeColor = new System.Windows.Forms.Panel();
            this.lblGridEvenRowForeColor = new System.Windows.Forms.Label();
            this.lblGridSelectedForeColor = new System.Windows.Forms.Label();
            this.lblGridHighlightForeColor = new System.Windows.Forms.Label();
            this.lblGridOddRowBackColor = new System.Windows.Forms.Label();
            this.pnlGridEvenRowBackColor = new System.Windows.Forms.Panel();
            this.lblGridOddRowForeColor = new System.Windows.Forms.Label();
            this.pnlGridHighlightForeColor = new System.Windows.Forms.Panel();
            this.lblGridEvenRowBackColor = new System.Windows.Forms.Label();
            this.lblGridHighlightBackColor = new System.Windows.Forms.Label();
            this.pnlGridHighlightBackColor = new System.Windows.Forms.Panel();
            this.pnlGridOddRowForeColor = new System.Windows.Forms.Panel();
            this.pnlGridOddRowBackColor = new System.Windows.Forms.Panel();
            this.pnlGridEvenRowForeColor = new System.Windows.Forms.Panel();
            this.grpPreviewGrid = new System.Windows.Forms.GroupBox();
            this.cboFindGrid = new C1.Win.C1Input.C1ComboBox();
            this.tsGrid = new System.Windows.Forms.ToolStrip();
            this.lblFindGrid = new System.Windows.Forms.ToolStripLabel();
            this.cboFindGrid3 = new System.Windows.Forms.ToolStripComboBox();
            this.btnFindNextGrid = new System.Windows.Forms.ToolStripButton();
            this.btnFindPreviousGrid = new System.Windows.Forms.ToolStripButton();
            this.btnCountGrid = new System.Windows.Forms.ToolStripButton();
            this.btnHighlightAllGrid = new System.Windows.Forms.ToolStripButton();
            this.btnClearHighlightsGrid = new System.Windows.Forms.ToolStripButton();
            this.c1GridVisualStyle = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.grpOperatorKeywords = new System.Windows.Forms.GroupBox();
            this.grpFindOperatorKeywords = new System.Windows.Forms.GroupBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.txtFindOperatorKeywords = new System.Windows.Forms.ToolStripTextBox();
            this.btnNextOperatorKeywords = new System.Windows.Forms.ToolStripButton();
            this.btnPreviousOperatorKeywords = new System.Windows.Forms.ToolStripButton();
            this.btnCloseFindOperatorKeywords = new System.Windows.Forms.ToolStripButton();
            this.picOperatorKeywords = new System.Windows.Forms.PictureBox();
            this.txtOperatorKeywords = new JasonLibrary.ScintillaEditor();
            this.grpBuiltInFunctions = new System.Windows.Forms.GroupBox();
            this.grpFindBuiltInFunctions = new System.Windows.Forms.GroupBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.txtFindBuiltInFunctions = new System.Windows.Forms.ToolStripTextBox();
            this.btnNextBuiltInFunctions = new System.Windows.Forms.ToolStripButton();
            this.btnPreviousBuiltInFunctions = new System.Windows.Forms.ToolStripButton();
            this.btnCloseFindBuiltInFunctions = new System.Windows.Forms.ToolStripButton();
            this.picBuiltInFunctions = new System.Windows.Forms.PictureBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.txtBuiltInFunctions = new JasonLibrary.ScintillaEditor();
            this.grpBuiltInKeywords = new System.Windows.Forms.GroupBox();
            this.grpFindBuiltInKeywords = new System.Windows.Forms.GroupBox();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.txtFindBuiltInKeywords = new System.Windows.Forms.ToolStripTextBox();
            this.btnNextBuiltInKeywords = new System.Windows.Forms.ToolStripButton();
            this.btnPreviousBuiltInKeywords = new System.Windows.Forms.ToolStripButton();
            this.btnCloseFindBuiltInKeywords = new System.Windows.Forms.ToolStripButton();
            this.picBuiltInKeywords = new System.Windows.Forms.PictureBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.txtBuiltInKeywords = new JasonLibrary.ScintillaEditor();
            this.grpUserDefinedKeywords = new System.Windows.Forms.GroupBox();
            this.grpFindUserDefinedKeywords = new System.Windows.Forms.GroupBox();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.txtFindUserDefinedKeywords = new System.Windows.Forms.ToolStripTextBox();
            this.btnNextUserDefinedKeywords = new System.Windows.Forms.ToolStripButton();
            this.btnPreviousUserDefinedKeywords = new System.Windows.Forms.ToolStripButton();
            this.btnCloseFindUserDefinedKeywords = new System.Windows.Forms.ToolStripButton();
            this.picUserDefinedKeywords = new System.Windows.Forms.PictureBox();
            this.txtUserDefinedKeywords = new JasonLibrary.ScintillaEditor();
            this.grpSQLToCode = new System.Windows.Forms.GroupBox();
            this.txtVariableName = new C1.Win.C1Input.C1TextBox();
            this.chkStripCode = new C1.Win.C1Input.C1CheckBox();
            this.grpSQLStatementCode = new System.Windows.Forms.GroupBox();
            this.editorSQLToCode = new JasonLibrary.ScintillaEditor();
            this.grpPreviewSQL = new System.Windows.Forms.GroupBox();
            this.editorSQLToCodePreview = new JasonLibrary.ScintillaEditor();
            this.lblSQLVariableName = new System.Windows.Forms.Label();
            this.grpStyle = new System.Windows.Forms.GroupBox();
            this.lblStyle3 = new System.Windows.Forms.Label();
            this.lblStyle2 = new System.Windows.Forms.Label();
            this.lblStyle1 = new System.Windows.Forms.Label();
            this.rdoStyle1 = new System.Windows.Forms.RadioButton();
            this.rdoStyle3 = new System.Windows.Forms.RadioButton();
            this.rdoStyle2 = new System.Windows.Forms.RadioButton();
            this.grpLanguage = new System.Windows.Forms.GroupBox();
            this.lstLanguage = new System.Windows.Forms.ListBox();
            this.grpSQLFormatter = new System.Windows.Forms.GroupBox();
            this.grpPreviewFormatter = new System.Windows.Forms.GroupBox();
            this.editorSQLFormatterPreview = new JasonLibrary.ScintillaEditor();
            this.grpFormattingOptions = new System.Windows.Forms.GroupBox();
            this.txtMaxWidth = new C1.Win.C1Input.C1TextBox();
            this.chkConvertCaseForKeywords = new C1.Win.C1Input.C1CheckBox();
            this.chkBreakJoinOnSections = new C1.Win.C1Input.C1CheckBox();
            this.chkExpandInLists = new C1.Win.C1Input.C1CheckBox();
            this.chkExpandBetweenConditions = new C1.Win.C1Input.C1CheckBox();
            this.chkExpandCaseStatements = new C1.Win.C1Input.C1CheckBox();
            this.chkExpandBooleanExpressions = new C1.Win.C1Input.C1CheckBox();
            this.chkTrailingCommas = new C1.Win.C1Input.C1CheckBox();
            this.chkExpandCommaLists = new C1.Win.C1Input.C1CheckBox();
            this.rdoProperCase = new System.Windows.Forms.RadioButton();
            this.rdoLowerCase = new System.Windows.Forms.RadioButton();
            this.rdoUpperCase = new System.Windows.Forms.RadioButton();
            this.lblMaxWidth2 = new System.Windows.Forms.Label();
            this.grpSQLStatementFormatter = new System.Windows.Forms.GroupBox();
            this.editorSQLFormatter = new JasonLibrary.ScintillaEditor();
            this.grpGlobal = new System.Windows.Forms.GroupBox();
            this.grpMaxEntries = new System.Windows.Forms.GroupBox();
            this.txtMyFavorite = new C1.Win.C1Input.C1TextBox();
            this.txtRecentFiles = new C1.Win.C1Input.C1TextBox();
            this.lblMyFavorite = new System.Windows.Forms.Label();
            this.lblRecentFiles = new System.Windows.Forms.Label();
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.chkHideClock = new C1.Win.C1Input.C1CheckBox();
            this.lblStarShowVersion = new System.Windows.Forms.Label();
            this.chkShowVersion = new C1.Win.C1Input.C1CheckBox();
            this.cboLocalization = new C1.Win.C1Input.C1ComboBox();
            this.cboDateFormat = new C1.Win.C1Input.C1ComboBox();
            this.lblDateFormat = new System.Windows.Forms.Label();
            this.lblLocalization = new System.Windows.Forms.Label();
            this.grpMainFormWindowsState = new System.Windows.Forms.GroupBox();
            this.rdoNormal = new System.Windows.Forms.RadioButton();
            this.rdoMaximized = new System.Windows.Forms.RadioButton();
            this.grpMainFormTabVisualStyle = new System.Windows.Forms.GroupBox();
            this.lblStarMainFormTab = new System.Windows.Forms.Label();
            this.chkMultiLine = new C1.Win.C1Input.C1CheckBox();
            this.chkHoverSelect = new C1.Win.C1Input.C1CheckBox();
            this.chkShowArrows = new C1.Win.C1Input.C1CheckBox();
            this.chkShrinkPages = new C1.Win.C1Input.C1CheckBox();
            this.chkTabBold = new System.Windows.Forms.CheckBox();
            this.tabExample = new Crownwood.Magic.Controls.TabControl();
            this.tabPage1 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage2 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage3 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage4 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage5 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage6 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage7 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage8 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage9 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage10 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage11 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage12 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage13 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage14 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage15 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage16 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage17 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage18 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage19 = new Crownwood.Magic.Controls.TabPage();
            this.tabPage20 = new Crownwood.Magic.Controls.TabPage();
            this.grpAppearance = new System.Windows.Forms.GroupBox();
            this.rdoMultiBox = new System.Windows.Forms.RadioButton();
            this.rdoMultiForm = new System.Windows.Forms.RadioButton();
            this.rdoMultiDocument = new System.Windows.Forms.RadioButton();
            this.grpMainFormTabStyle = new System.Windows.Forms.GroupBox();
            this.rdoPlain = new System.Windows.Forms.RadioButton();
            this.rdoIDE = new System.Windows.Forms.RadioButton();
            this.grpOptionsTab = new System.Windows.Forms.GroupBox();
            this.lblOptionsTabInactiveForeColor = new System.Windows.Forms.Label();
            this.pnlOptionsTabInactiveForeColor = new System.Windows.Forms.Panel();
            this.c1DockingTab1 = new C1.Win.C1Command.C1DockingTab();
            this.tabGlobal2 = new C1.Win.C1Command.C1DockingTabPage();
            this.tabGeneral2 = new C1.Win.C1Command.C1DockingTabPage();
            this.tabQueryEditor2 = new C1.Win.C1Command.C1DockingTabPage();
            this.tabAutoComplete2 = new C1.Win.C1Command.C1DockingTabPage();
            this.tabAutoReplace2 = new C1.Win.C1Command.C1DockingTabPage();
            this.tabDataGrid2 = new C1.Win.C1Command.C1DockingTabPage();
            this.tabKeywords2 = new C1.Win.C1Command.C1DockingTabPage();
            this.tabSQLToCode2 = new C1.Win.C1Command.C1DockingTabPage();
            this.tabSQLFormatter2 = new C1.Win.C1Command.C1DockingTabPage();
            this.lblOptionsTabActiveForeColor = new System.Windows.Forms.Label();
            this.lblOptionsTabActiveBackColor = new System.Windows.Forms.Label();
            this.pnlOptionsTabActiveForeColor = new System.Windows.Forms.Panel();
            this.pnlOptionsTabActiveBackColor = new System.Windows.Forms.Panel();
            this.grpCheckForUpdate = new System.Windows.Forms.GroupBox();
            this.rdoCheckOnly = new System.Windows.Forms.RadioButton();
            this.grpCheckOnly = new System.Windows.Forms.GroupBox();
            this.rdoCheckForUpdates0 = new System.Windows.Forms.RadioButton();
            this.rdoCheckForUpdates1 = new System.Windows.Forms.RadioButton();
            this.rdoCheckForUpdates7 = new System.Windows.Forms.RadioButton();
            this.rdoDonotCheck = new System.Windows.Forms.RadioButton();
            this.txtHeightCode = new System.Windows.Forms.TextBox();
            this.txtHeightFormatter = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblRequireRestart = new System.Windows.Forms.Label();
            this.lblStarRequireToRestart = new System.Windows.Forms.Label();
            this.timerMother2Child = new System.Windows.Forms.Timer(this.components);
            this.c1DockingTab = new C1.Win.C1Command.C1DockingTab();
            this.tabGlobal = new C1.Win.C1Command.C1DockingTabPage();
            this.tabGeneral = new C1.Win.C1Command.C1DockingTabPage();
            this.grpDefaultDirectory = new System.Windows.Forms.GroupBox();
            this.c1Button2 = new C1.Win.C1Input.C1Button();
            this.txtFavoriteDirectory = new C1.Win.C1Input.C1TextBox();
            this.lblStarDefaultDirectory = new System.Windows.Forms.Label();
            this.rdoFavoriteDirectory = new System.Windows.Forms.RadioButton();
            this.rdoDefaultDirectory = new System.Windows.Forms.RadioButton();
            this.grpOpenSQLFile = new System.Windows.Forms.GroupBox();
            this.btnClear2 = new C1.Win.C1Input.C1Button();
            this.btnClear1 = new C1.Win.C1Input.C1Button();
            this.btnSpecifiedSQLFile2 = new C1.Win.C1Input.C1Button();
            this.txtSpecifiedSQLFile2 = new C1.Win.C1Input.C1TextBox();
            this.lblFile2 = new System.Windows.Forms.Label();
            this.btnSpecifiedSQLFile1 = new C1.Win.C1Input.C1Button();
            this.txtSpecifiedSQLFile1 = new C1.Win.C1Input.C1TextBox();
            this.lblFile1 = new System.Windows.Forms.Label();
            this.grpAutoDisconnect = new System.Windows.Forms.GroupBox();
            this.btnHelp_Disconnect = new C1.Win.C1Input.C1Button();
            this.cboAutoDisconnect = new C1.Win.C1Input.C1ComboBox();
            this.tabQueryEditor = new C1.Win.C1Command.C1DockingTabPage();
            this.grpStatementCompletion = new System.Windows.Forms.GroupBox();
            this.btnHelp_SavePoint = new C1.Win.C1Input.C1Button();
            this.chkSavePoint = new System.Windows.Forms.CheckBox();
            this.chkAutoListMembers = new System.Windows.Forms.CheckBox();
            this.grpSchemaBrowser = new System.Windows.Forms.GroupBox();
            this.chkDefaultTabSchemaBrowser = new C1.Win.C1Input.C1CheckBox();
            this.lblStarShowColumnInfo = new System.Windows.Forms.Label();
            this.lblStarSortByColumnName = new System.Windows.Forms.Label();
            this.btnHelp_ShowColumnInfo = new C1.Win.C1Input.C1Button();
            this.chkShowColumnInfo = new C1.Win.C1Input.C1CheckBox();
            this.chkSortByColumnName = new C1.Win.C1Input.C1CheckBox();
            this.tabAutoComplete = new C1.Win.C1Command.C1DockingTabPage();
            this.tabAutoReplace = new C1.Win.C1Command.C1DockingTabPage();
            this.tabDataGrid = new C1.Win.C1Command.C1DockingTabPage();
            this.tabKeywords = new C1.Win.C1Command.C1DockingTabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.tabSQLToCode = new C1.Win.C1Command.C1DockingTabPage();
            this.tabSQLFormatter = new C1.Win.C1Command.C1DockingTabPage();
            this.timerTitle = new System.Windows.Forms.Timer(this.components);
            this.pnlCopySettings = new System.Windows.Forms.Panel();
            this.toolStrip6 = new System.Windows.Forms.ToolStrip();
            this.tsCopyFrom = new System.Windows.Forms.ToolStripDropDownButton();
            this.c1ThemeController1 = new C1.Win.C1Themes.C1ThemeController();
            this.btnCopySettings = new C1.Win.C1Input.C1SplitButton();
            this.btnClose = new C1.Win.C1Input.C1Button();
            this.btnApply = new C1.Win.C1Input.C1Button();
            this.btnRestoreDefaults = new C1.Win.C1Input.C1Button();
            this.grpEditorColors.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpPreferences.SuspendLayout();
            this.grpIndent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkReplaceTabWithSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowIndentGuide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTabWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIndentMode)).BeginInit();
            this.grpIndicate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboBookmarkStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCtrlMouseWheel1)).BeginInit();
            this.grpWordWrap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWordWrap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOpenFileOnCurrentTab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowSaveAsButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEditorZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEditorFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSaveAsEncoding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEntireBlankRowAsEmptyRow4SelectBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHighlightSelectedText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSaveAsEncoding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCopyAsHTML)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEditorFontPicker)).BeginInit();
            this.grpHighlight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboHighlightStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboHighlightAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboHighlightOutlineAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowAllCharacters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHighlightSelection)).BeginInit();
            this.grpColorTheme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_DarkMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDarkMode)).BeginInit();
            this.grpQueryEditorPreview.SuspendLayout();
            this.tsEditor.SuspendLayout();
            this.grpAutoComplete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFirstCharChecking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinFragmentLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableAutoComplete)).BeginInit();
            this.grpAutoCompleteFor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkUserDefinedViews)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUserDefinedTriggers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUserDefinedTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUserDefinedFunctions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUserDefinedKeywords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBuiltInKeywords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBuiltInFunctions)).BeginInit();
            this.grpAutoReplace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilterRowAR)).BeginInit();
            this.grpModifyDefinitionAR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearAR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelAR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAR)).BeginInit();
            this.grpDefinitionAR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteAR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditAR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddAR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridARInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableAutoReplace)).BeginInit();
            this.grpDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowGroupingRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_AppendingQueries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAppendingQueries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSetFocusAfterQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRowsPerPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPagingQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_RawDataMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRawDataMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPreviewCLOBData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseReadOnlyQueries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCtrlMouseWheel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGridRowHeightResizing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGridFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGridZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGridVisualStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResultCopyFieldSeparator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResultCopyQuotingWith)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMaxWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkResize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilterRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowStreamlinedName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowColumnType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGridFontPicker)).BeginInit();
            this.grpNullValueStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboNullShowAs)).BeginInit();
            this.grpDataGridColor.SuspendLayout();
            this.grpPreviewGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboFindGrid)).BeginInit();
            this.tsGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridVisualStyle)).BeginInit();
            this.grpOperatorKeywords.SuspendLayout();
            this.grpFindOperatorKeywords.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOperatorKeywords)).BeginInit();
            this.grpBuiltInFunctions.SuspendLayout();
            this.grpFindBuiltInFunctions.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBuiltInFunctions)).BeginInit();
            this.grpBuiltInKeywords.SuspendLayout();
            this.grpFindBuiltInKeywords.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBuiltInKeywords)).BeginInit();
            this.grpUserDefinedKeywords.SuspendLayout();
            this.grpFindUserDefinedKeywords.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserDefinedKeywords)).BeginInit();
            this.grpSQLToCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVariableName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStripCode)).BeginInit();
            this.grpSQLStatementCode.SuspendLayout();
            this.grpPreviewSQL.SuspendLayout();
            this.grpStyle.SuspendLayout();
            this.grpLanguage.SuspendLayout();
            this.grpSQLFormatter.SuspendLayout();
            this.grpPreviewFormatter.SuspendLayout();
            this.grpFormattingOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkConvertCaseForKeywords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBreakJoinOnSections)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpandInLists)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpandBetweenConditions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpandCaseStatements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpandBooleanExpressions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTrailingCommas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpandCommaLists)).BeginInit();
            this.grpSQLStatementFormatter.SuspendLayout();
            this.grpGlobal.SuspendLayout();
            this.grpMaxEntries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMyFavorite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecentFiles)).BeginInit();
            this.grpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkHideClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLocalization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDateFormat)).BeginInit();
            this.grpMainFormWindowsState.SuspendLayout();
            this.grpMainFormTabVisualStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkMultiLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHoverSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowArrows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShrinkPages)).BeginInit();
            this.tabExample.SuspendLayout();
            this.grpAppearance.SuspendLayout();
            this.grpMainFormTabStyle.SuspendLayout();
            this.grpOptionsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).BeginInit();
            this.c1DockingTab1.SuspendLayout();
            this.grpCheckForUpdate.SuspendLayout();
            this.grpCheckOnly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab)).BeginInit();
            this.c1DockingTab.SuspendLayout();
            this.tabGlobal.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.grpDefaultDirectory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Button2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFavoriteDirectory)).BeginInit();
            this.grpOpenSQLFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSpecifiedSQLFile2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecifiedSQLFile2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSpecifiedSQLFile1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecifiedSQLFile1)).BeginInit();
            this.grpAutoDisconnect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_Disconnect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAutoDisconnect)).BeginInit();
            this.tabQueryEditor.SuspendLayout();
            this.grpStatementCompletion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_SavePoint)).BeginInit();
            this.grpSchemaBrowser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDefaultTabSchemaBrowser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_ShowColumnInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowColumnInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSortByColumnName)).BeginInit();
            this.tabAutoComplete.SuspendLayout();
            this.tabAutoReplace.SuspendLayout();
            this.tabDataGrid.SuspendLayout();
            this.tabKeywords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.tabSQLToCode.SuspendLayout();
            this.tabSQLFormatter.SuspendLayout();
            this.pnlCopySettings.SuspendLayout();
            this.toolStrip6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCopySettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestoreDefaults)).BeginInit();
            this.SuspendLayout();
            // 
            // grpEditorColors
            // 
            this.grpEditorColors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpEditorColors.BackColor = System.Drawing.Color.Transparent;
            this.grpEditorColors.Controls.Add(this.panel1);
            this.grpEditorColors.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpEditorColors.Location = new System.Drawing.Point(12, 200);
            this.grpEditorColors.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpEditorColors.Name = "grpEditorColors";
            this.grpEditorColors.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpEditorColors.Size = new System.Drawing.Size(317, 473);
            this.grpEditorColors.TabIndex = 24;
            this.grpEditorColors.TabStop = false;
            this.grpEditorColors.Text = "Editor Colors";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(300, 545);
            this.panel1.Controls.Add(this.lblToolstripBackground);
            this.panel1.Controls.Add(this.pnlString);
            this.panel1.Controls.Add(this.lblSelectedTextBackground);
            this.panel1.Controls.Add(this.lblString);
            this.panel1.Controls.Add(this.pnlToolstripBackground);
            this.panel1.Controls.Add(this.lblCharacter);
            this.panel1.Controls.Add(this.pnlSelectedTextBackground);
            this.panel1.Controls.Add(this.pnlBuiltInKeywords);
            this.panel1.Controls.Add(this.lblUserDefinedKeywords);
            this.panel1.Controls.Add(this.pnlCharacter);
            this.panel1.Controls.Add(this.pnlUserDefinedKeywords);
            this.panel1.Controls.Add(this.lblBuiltInKeywords);
            this.panel1.Controls.Add(this.pnlOperatorKeywords);
            this.panel1.Controls.Add(this.lblUserTables);
            this.panel1.Controls.Add(this.lblOperatorKeywords);
            this.panel1.Controls.Add(this.pnlIdentifier);
            this.panel1.Controls.Add(this.pnlUserFunctions);
            this.panel1.Controls.Add(this.pnlUserTables);
            this.panel1.Controls.Add(this.lblUserFunctions);
            this.panel1.Controls.Add(this.lblIdentifier);
            this.panel1.Controls.Add(this.lblWhiteSpace);
            this.panel1.Controls.Add(this.lblBuiltInFunctions);
            this.panel1.Controls.Add(this.pnlWhiteSpace);
            this.panel1.Controls.Add(this.pnlComments);
            this.panel1.Controls.Add(this.pnlOperatorSymbol);
            this.panel1.Controls.Add(this.pnlBuiltInFunctions);
            this.panel1.Controls.Add(this.lblOperatorSymbol);
            this.panel1.Controls.Add(this.lblComments);
            this.panel1.Controls.Add(this.lblEditorBackground);
            this.panel1.Controls.Add(this.lblNumber);
            this.panel1.Controls.Add(this.lblCurrentLineBackground);
            this.panel1.Controls.Add(this.pnlCurrentLineBackground);
            this.panel1.Controls.Add(this.pnlEditorBackground);
            this.panel1.Controls.Add(this.pnlNumber);
            this.panel1.Location = new System.Drawing.Point(0, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(317, 458);
            this.panel1.TabIndex = 39;
            this.c1ThemeController1.SetTheme(this.panel1, "(default)");
            // 
            // lblToolstripBackground
            // 
            this.lblToolstripBackground.Location = new System.Drawing.Point(26, 9);
            this.lblToolstripBackground.Name = "lblToolstripBackground";
            this.lblToolstripBackground.Size = new System.Drawing.Size(195, 16);
            this.lblToolstripBackground.TabIndex = 3;
            this.lblToolstripBackground.Text = "Focused Toolstrip Background:";
            this.lblToolstripBackground.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlString
            // 
            this.pnlString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlString.Location = new System.Drawing.Point(222, 277);
            this.pnlString.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlString.Name = "pnlString";
            this.pnlString.Size = new System.Drawing.Size(74, 21);
            this.pnlString.TabIndex = 11;
            this.pnlString.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // lblSelectedTextBackground
            // 
            this.lblSelectedTextBackground.Location = new System.Drawing.Point(46, 99);
            this.lblSelectedTextBackground.Name = "lblSelectedTextBackground";
            this.lblSelectedTextBackground.Size = new System.Drawing.Size(175, 16);
            this.lblSelectedTextBackground.TabIndex = 38;
            this.lblSelectedTextBackground.Text = "Selected Text Background:";
            this.lblSelectedTextBackground.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblString
            // 
            this.lblString.Location = new System.Drawing.Point(46, 279);
            this.lblString.Name = "lblString";
            this.lblString.Size = new System.Drawing.Size(175, 16);
            this.lblString.TabIndex = 10;
            this.lblString.Text = "String (Double Quoted):";
            this.lblString.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlToolstripBackground
            // 
            this.pnlToolstripBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlToolstripBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlToolstripBackground.Location = new System.Drawing.Point(222, 7);
            this.pnlToolstripBackground.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlToolstripBackground.Name = "pnlToolstripBackground";
            this.pnlToolstripBackground.Size = new System.Drawing.Size(74, 21);
            this.pnlToolstripBackground.TabIndex = 4;
            this.pnlToolstripBackground.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // lblCharacter
            // 
            this.lblCharacter.Location = new System.Drawing.Point(24, 309);
            this.lblCharacter.Name = "lblCharacter";
            this.lblCharacter.Size = new System.Drawing.Size(197, 16);
            this.lblCharacter.TabIndex = 12;
            this.lblCharacter.Text = "Character (Single Quoted):";
            this.lblCharacter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlSelectedTextBackground
            // 
            this.pnlSelectedTextBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlSelectedTextBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSelectedTextBackground.Location = new System.Drawing.Point(222, 97);
            this.pnlSelectedTextBackground.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlSelectedTextBackground.Name = "pnlSelectedTextBackground";
            this.pnlSelectedTextBackground.Size = new System.Drawing.Size(74, 21);
            this.pnlSelectedTextBackground.TabIndex = 37;
            this.pnlSelectedTextBackground.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // pnlBuiltInKeywords
            // 
            this.pnlBuiltInKeywords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlBuiltInKeywords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBuiltInKeywords.Location = new System.Drawing.Point(222, 367);
            this.pnlBuiltInKeywords.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlBuiltInKeywords.Name = "pnlBuiltInKeywords";
            this.pnlBuiltInKeywords.Size = new System.Drawing.Size(74, 21);
            this.pnlBuiltInKeywords.TabIndex = 9;
            this.pnlBuiltInKeywords.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // lblUserDefinedKeywords
            // 
            this.lblUserDefinedKeywords.Location = new System.Drawing.Point(46, 399);
            this.lblUserDefinedKeywords.Name = "lblUserDefinedKeywords";
            this.lblUserDefinedKeywords.Size = new System.Drawing.Size(175, 16);
            this.lblUserDefinedKeywords.TabIndex = 35;
            this.lblUserDefinedKeywords.Text = "User-defined Keywords:";
            this.lblUserDefinedKeywords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCharacter
            // 
            this.pnlCharacter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlCharacter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCharacter.Location = new System.Drawing.Point(222, 307);
            this.pnlCharacter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlCharacter.Name = "pnlCharacter";
            this.pnlCharacter.Size = new System.Drawing.Size(74, 21);
            this.pnlCharacter.TabIndex = 13;
            this.pnlCharacter.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // pnlUserDefinedKeywords
            // 
            this.pnlUserDefinedKeywords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlUserDefinedKeywords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUserDefinedKeywords.Location = new System.Drawing.Point(222, 397);
            this.pnlUserDefinedKeywords.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlUserDefinedKeywords.Name = "pnlUserDefinedKeywords";
            this.pnlUserDefinedKeywords.Size = new System.Drawing.Size(74, 21);
            this.pnlUserDefinedKeywords.TabIndex = 36;
            this.pnlUserDefinedKeywords.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // lblBuiltInKeywords
            // 
            this.lblBuiltInKeywords.Location = new System.Drawing.Point(46, 369);
            this.lblBuiltInKeywords.Name = "lblBuiltInKeywords";
            this.lblBuiltInKeywords.Size = new System.Drawing.Size(175, 16);
            this.lblBuiltInKeywords.TabIndex = 8;
            this.lblBuiltInKeywords.Text = "Built-in Keywords:";
            this.lblBuiltInKeywords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlOperatorKeywords
            // 
            this.pnlOperatorKeywords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlOperatorKeywords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOperatorKeywords.Location = new System.Drawing.Point(222, 247);
            this.pnlOperatorKeywords.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlOperatorKeywords.Name = "pnlOperatorKeywords";
            this.pnlOperatorKeywords.Size = new System.Drawing.Size(74, 21);
            this.pnlOperatorKeywords.TabIndex = 34;
            this.pnlOperatorKeywords.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // lblUserTables
            // 
            this.lblUserTables.Location = new System.Drawing.Point(23, 459);
            this.lblUserTables.Name = "lblUserTables";
            this.lblUserTables.Size = new System.Drawing.Size(198, 16);
            this.lblUserTables.TabIndex = 14;
            this.lblUserTables.Text = "User-defined Tables && Views:";
            this.lblUserTables.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOperatorKeywords
            // 
            this.lblOperatorKeywords.Location = new System.Drawing.Point(46, 249);
            this.lblOperatorKeywords.Name = "lblOperatorKeywords";
            this.lblOperatorKeywords.Size = new System.Drawing.Size(175, 16);
            this.lblOperatorKeywords.TabIndex = 33;
            this.lblOperatorKeywords.Text = "Operator Keywords:";
            this.lblOperatorKeywords.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlIdentifier
            // 
            this.pnlIdentifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlIdentifier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlIdentifier.Location = new System.Drawing.Point(222, 157);
            this.pnlIdentifier.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlIdentifier.Name = "pnlIdentifier";
            this.pnlIdentifier.Size = new System.Drawing.Size(74, 21);
            this.pnlIdentifier.TabIndex = 7;
            this.pnlIdentifier.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // pnlUserFunctions
            // 
            this.pnlUserFunctions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlUserFunctions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUserFunctions.Location = new System.Drawing.Point(222, 487);
            this.pnlUserFunctions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlUserFunctions.Name = "pnlUserFunctions";
            this.pnlUserFunctions.Size = new System.Drawing.Size(74, 21);
            this.pnlUserFunctions.TabIndex = 28;
            this.pnlUserFunctions.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // pnlUserTables
            // 
            this.pnlUserTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlUserTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUserTables.Location = new System.Drawing.Point(222, 457);
            this.pnlUserTables.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlUserTables.Name = "pnlUserTables";
            this.pnlUserTables.Size = new System.Drawing.Size(74, 21);
            this.pnlUserTables.TabIndex = 15;
            this.pnlUserTables.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // lblUserFunctions
            // 
            this.lblUserFunctions.Location = new System.Drawing.Point(16, 489);
            this.lblUserFunctions.Name = "lblUserFunctions";
            this.lblUserFunctions.Size = new System.Drawing.Size(205, 16);
            this.lblUserFunctions.TabIndex = 27;
            this.lblUserFunctions.Text = "User-defined Functions && Triggers:";
            this.lblUserFunctions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIdentifier
            // 
            this.lblIdentifier.Location = new System.Drawing.Point(46, 159);
            this.lblIdentifier.Name = "lblIdentifier";
            this.lblIdentifier.Size = new System.Drawing.Size(175, 16);
            this.lblIdentifier.TabIndex = 6;
            this.lblIdentifier.Text = "Text (Identifier):";
            this.lblIdentifier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWhiteSpace
            // 
            this.lblWhiteSpace.Location = new System.Drawing.Point(46, 429);
            this.lblWhiteSpace.Name = "lblWhiteSpace";
            this.lblWhiteSpace.Size = new System.Drawing.Size(175, 16);
            this.lblWhiteSpace.TabIndex = 25;
            this.lblWhiteSpace.Text = "WhiteSpace:";
            this.lblWhiteSpace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBuiltInFunctions
            // 
            this.lblBuiltInFunctions.Location = new System.Drawing.Point(46, 339);
            this.lblBuiltInFunctions.Name = "lblBuiltInFunctions";
            this.lblBuiltInFunctions.Size = new System.Drawing.Size(175, 16);
            this.lblBuiltInFunctions.TabIndex = 16;
            this.lblBuiltInFunctions.Text = "Built-in Functions:";
            this.lblBuiltInFunctions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlWhiteSpace
            // 
            this.pnlWhiteSpace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlWhiteSpace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWhiteSpace.Location = new System.Drawing.Point(222, 427);
            this.pnlWhiteSpace.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlWhiteSpace.Name = "pnlWhiteSpace";
            this.pnlWhiteSpace.Size = new System.Drawing.Size(74, 21);
            this.pnlWhiteSpace.TabIndex = 26;
            this.pnlWhiteSpace.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // pnlComments
            // 
            this.pnlComments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlComments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlComments.Location = new System.Drawing.Point(222, 127);
            this.pnlComments.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlComments.Name = "pnlComments";
            this.pnlComments.Size = new System.Drawing.Size(74, 21);
            this.pnlComments.TabIndex = 5;
            this.pnlComments.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // pnlOperatorSymbol
            // 
            this.pnlOperatorSymbol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlOperatorSymbol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOperatorSymbol.Location = new System.Drawing.Point(222, 217);
            this.pnlOperatorSymbol.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlOperatorSymbol.Name = "pnlOperatorSymbol";
            this.pnlOperatorSymbol.Size = new System.Drawing.Size(74, 21);
            this.pnlOperatorSymbol.TabIndex = 21;
            this.pnlOperatorSymbol.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // pnlBuiltInFunctions
            // 
            this.pnlBuiltInFunctions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlBuiltInFunctions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBuiltInFunctions.Location = new System.Drawing.Point(222, 337);
            this.pnlBuiltInFunctions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlBuiltInFunctions.Name = "pnlBuiltInFunctions";
            this.pnlBuiltInFunctions.Size = new System.Drawing.Size(74, 21);
            this.pnlBuiltInFunctions.TabIndex = 17;
            this.pnlBuiltInFunctions.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // lblOperatorSymbol
            // 
            this.lblOperatorSymbol.Location = new System.Drawing.Point(46, 219);
            this.lblOperatorSymbol.Name = "lblOperatorSymbol";
            this.lblOperatorSymbol.Size = new System.Drawing.Size(175, 16);
            this.lblOperatorSymbol.TabIndex = 20;
            this.lblOperatorSymbol.Text = "Operator Symbol:";
            this.lblOperatorSymbol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblComments
            // 
            this.lblComments.Location = new System.Drawing.Point(46, 129);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(175, 15);
            this.lblComments.TabIndex = 4;
            this.lblComments.Text = "Comments:";
            this.lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEditorBackground
            // 
            this.lblEditorBackground.Location = new System.Drawing.Point(46, 39);
            this.lblEditorBackground.Name = "lblEditorBackground";
            this.lblEditorBackground.Size = new System.Drawing.Size(175, 16);
            this.lblEditorBackground.TabIndex = 0;
            this.lblEditorBackground.Text = "Editor Background:";
            this.lblEditorBackground.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNumber
            // 
            this.lblNumber.Location = new System.Drawing.Point(46, 189);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(175, 16);
            this.lblNumber.TabIndex = 18;
            this.lblNumber.Text = "Number:";
            this.lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrentLineBackground
            // 
            this.lblCurrentLineBackground.Location = new System.Drawing.Point(46, 69);
            this.lblCurrentLineBackground.Name = "lblCurrentLineBackground";
            this.lblCurrentLineBackground.Size = new System.Drawing.Size(175, 16);
            this.lblCurrentLineBackground.TabIndex = 1;
            this.lblCurrentLineBackground.Text = "Current Line Background:";
            this.lblCurrentLineBackground.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlCurrentLineBackground
            // 
            this.pnlCurrentLineBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlCurrentLineBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCurrentLineBackground.Location = new System.Drawing.Point(222, 67);
            this.pnlCurrentLineBackground.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlCurrentLineBackground.Name = "pnlCurrentLineBackground";
            this.pnlCurrentLineBackground.Size = new System.Drawing.Size(74, 21);
            this.pnlCurrentLineBackground.TabIndex = 3;
            this.pnlCurrentLineBackground.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // pnlEditorBackground
            // 
            this.pnlEditorBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlEditorBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEditorBackground.Location = new System.Drawing.Point(222, 37);
            this.pnlEditorBackground.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlEditorBackground.Name = "pnlEditorBackground";
            this.pnlEditorBackground.Size = new System.Drawing.Size(74, 21);
            this.pnlEditorBackground.TabIndex = 2;
            this.pnlEditorBackground.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // pnlNumber
            // 
            this.pnlNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNumber.Location = new System.Drawing.Point(222, 187);
            this.pnlNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlNumber.Name = "pnlNumber";
            this.pnlNumber.Size = new System.Drawing.Size(74, 21);
            this.pnlNumber.TabIndex = 19;
            this.pnlNumber.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // grpPreferences
            // 
            this.grpPreferences.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPreferences.BackColor = System.Drawing.Color.Transparent;
            this.grpPreferences.Controls.Add(this.grpIndent);
            this.grpPreferences.Controls.Add(this.grpIndicate);
            this.grpPreferences.Controls.Add(this.chkCtrlMouseWheel1);
            this.grpPreferences.Controls.Add(this.grpWordWrap);
            this.grpPreferences.Controls.Add(this.chkOpenFileOnCurrentTab);
            this.grpPreferences.Controls.Add(this.chkShowSaveAsButton);
            this.grpPreferences.Controls.Add(this.cboEditorZoom);
            this.grpPreferences.Controls.Add(this.cboEditorFontSize);
            this.grpPreferences.Controls.Add(this.cboSaveAsEncoding);
            this.grpPreferences.Controls.Add(this.chkEntireBlankRowAsEmptyRow4SelectBlock);
            this.grpPreferences.Controls.Add(this.chkHighlightSelectedText);
            this.grpPreferences.Controls.Add(this.chkSaveAsEncoding);
            this.grpPreferences.Controls.Add(this.chkCopyAsHTML);
            this.grpPreferences.Controls.Add(this.chkBold);
            this.grpPreferences.Controls.Add(this.cboEditorFontPicker);
            this.grpPreferences.Controls.Add(this.grpHighlight);
            this.grpPreferences.Controls.Add(this.lblStarHighlightSelection);
            this.grpPreferences.Controls.Add(this.lblEditorFontName);
            this.grpPreferences.Controls.Add(this.lblEditorFontSize);
            this.grpPreferences.Controls.Add(this.lblEditorZoom);
            this.grpPreferences.Controls.Add(this.chkShowAllCharacters);
            this.grpPreferences.Controls.Add(this.chkHighlightSelection);
            this.grpPreferences.Location = new System.Drawing.Point(339, 9);
            this.grpPreferences.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpPreferences.Name = "grpPreferences";
            this.grpPreferences.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpPreferences.Size = new System.Drawing.Size(861, 295);
            this.grpPreferences.TabIndex = 23;
            this.grpPreferences.TabStop = false;
            this.grpPreferences.Text = "Preferences";
            // 
            // grpIndent
            // 
            this.grpIndent.Controls.Add(this.chkReplaceTabWithSpace);
            this.grpIndent.Controls.Add(this.lblStarTabWidth);
            this.grpIndent.Controls.Add(this.chkShowIndentGuide);
            this.grpIndent.Controls.Add(this.cboTabWidth);
            this.grpIndent.Controls.Add(this.lblLength);
            this.grpIndent.Controls.Add(this.lblTabWidth);
            this.grpIndent.Controls.Add(this.lblIndentMode);
            this.grpIndent.Controls.Add(this.cboIndentMode);
            this.grpIndent.Controls.Add(this.lblStarShowIndentGuide);
            this.grpIndent.Location = new System.Drawing.Point(571, 105);
            this.grpIndent.Name = "grpIndent";
            this.grpIndent.Size = new System.Drawing.Size(245, 130);
            this.grpIndent.TabIndex = 85;
            this.grpIndent.TabStop = false;
            this.grpIndent.Text = "Indent";
            this.c1ThemeController1.SetTheme(this.grpIndent, "(default)");
            // 
            // chkReplaceTabWithSpace
            // 
            this.chkReplaceTabWithSpace.AutoSize = true;
            this.chkReplaceTabWithSpace.BackColor = System.Drawing.Color.Transparent;
            this.chkReplaceTabWithSpace.BorderColor = System.Drawing.Color.Transparent;
            this.chkReplaceTabWithSpace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkReplaceTabWithSpace.Checked = true;
            this.chkReplaceTabWithSpace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReplaceTabWithSpace.Enabled = false;
            this.chkReplaceTabWithSpace.ForeColor = System.Drawing.Color.Black;
            this.chkReplaceTabWithSpace.Location = new System.Drawing.Point(25, 103);
            this.chkReplaceTabWithSpace.Name = "chkReplaceTabWithSpace";
            this.chkReplaceTabWithSpace.Padding = new System.Windows.Forms.Padding(1);
            this.chkReplaceTabWithSpace.Size = new System.Drawing.Size(164, 22);
            this.chkReplaceTabWithSpace.TabIndex = 86;
            this.chkReplaceTabWithSpace.Text = "Replace Tab with Space";
            this.c1ThemeController1.SetTheme(this.chkReplaceTabWithSpace, "(default)");
            this.chkReplaceTabWithSpace.UseVisualStyleBackColor = true;
            this.chkReplaceTabWithSpace.Value = true;
            this.chkReplaceTabWithSpace.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblStarTabWidth
            // 
            this.lblStarTabWidth.AutoSize = true;
            this.lblStarTabWidth.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarTabWidth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarTabWidth.Location = new System.Drawing.Point(169, 79);
            this.lblStarTabWidth.Name = "lblStarTabWidth";
            this.lblStarTabWidth.Size = new System.Drawing.Size(14, 15);
            this.lblStarTabWidth.TabIndex = 85;
            this.lblStarTabWidth.Text = "*";
            this.lblStarTabWidth.Visible = false;
            // 
            // chkShowIndentGuide
            // 
            this.chkShowIndentGuide.AutoSize = true;
            this.chkShowIndentGuide.BackColor = System.Drawing.Color.Transparent;
            this.chkShowIndentGuide.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowIndentGuide.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowIndentGuide.Checked = true;
            this.chkShowIndentGuide.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowIndentGuide.ForeColor = System.Drawing.Color.Black;
            this.chkShowIndentGuide.Location = new System.Drawing.Point(25, 22);
            this.chkShowIndentGuide.Name = "chkShowIndentGuide";
            this.chkShowIndentGuide.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowIndentGuide.Size = new System.Drawing.Size(136, 22);
            this.chkShowIndentGuide.TabIndex = 81;
            this.chkShowIndentGuide.Text = "Show Indent Guide";
            this.c1ThemeController1.SetTheme(this.chkShowIndentGuide, "(default)");
            this.chkShowIndentGuide.UseVisualStyleBackColor = true;
            this.chkShowIndentGuide.Value = true;
            this.chkShowIndentGuide.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShowIndentGuide.CheckedChanged += new System.EventHandler(this.chkShowIndentGuide_CheckedChanged);
            // 
            // cboTabWidth
            // 
            this.cboTabWidth.AllowSpinLoop = false;
            this.cboTabWidth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboTabWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboTabWidth.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboTabWidth.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboTabWidth.GapHeight = 0;
            this.cboTabWidth.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboTabWidth.Items.Add("1");
            this.cboTabWidth.Items.Add("2");
            this.cboTabWidth.Items.Add("3");
            this.cboTabWidth.Items.Add("4");
            this.cboTabWidth.Items.Add("5");
            this.cboTabWidth.Items.Add("6");
            this.cboTabWidth.Items.Add("7");
            this.cboTabWidth.Items.Add("8");
            this.cboTabWidth.Items.Add("9");
            this.cboTabWidth.ItemsDisplayMember = "";
            this.cboTabWidth.ItemsValueMember = "";
            this.cboTabWidth.Location = new System.Drawing.Point(117, 76);
            this.cboTabWidth.Name = "cboTabWidth";
            this.cboTabWidth.Size = new System.Drawing.Size(39, 21);
            this.cboTabWidth.TabIndex = 84;
            this.cboTabWidth.Tag = null;
            this.cboTabWidth.Text = "4";
            this.cboTabWidth.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboTabWidth, "(default)");
            this.cboTabWidth.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(111, 16);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(0, 16);
            this.lblLength.TabIndex = 55;
            this.lblLength.Visible = false;
            // 
            // lblTabWidth
            // 
            this.lblTabWidth.AutoSize = true;
            this.lblTabWidth.Location = new System.Drawing.Point(23, 79);
            this.lblTabWidth.Name = "lblTabWidth";
            this.lblTabWidth.Size = new System.Drawing.Size(70, 16);
            this.lblTabWidth.TabIndex = 83;
            this.lblTabWidth.Text = "Tab Width:";
            this.lblTabWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIndentMode
            // 
            this.lblIndentMode.AutoSize = true;
            this.lblIndentMode.Location = new System.Drawing.Point(23, 50);
            this.lblIndentMode.Name = "lblIndentMode";
            this.lblIndentMode.Size = new System.Drawing.Size(85, 16);
            this.lblIndentMode.TabIndex = 41;
            this.lblIndentMode.Text = "Indent Mode:";
            this.lblIndentMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboIndentMode
            // 
            this.cboIndentMode.AllowSpinLoop = false;
            this.cboIndentMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboIndentMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboIndentMode.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboIndentMode.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboIndentMode.GapHeight = 0;
            this.cboIndentMode.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboIndentMode.Items.Add("Fixed");
            this.cboIndentMode.Items.Add("Same");
            this.cboIndentMode.Items.Add("Indent");
            this.cboIndentMode.ItemsDisplayMember = "";
            this.cboIndentMode.ItemsValueMember = "";
            this.cboIndentMode.Location = new System.Drawing.Point(117, 48);
            this.cboIndentMode.Name = "cboIndentMode";
            this.cboIndentMode.Size = new System.Drawing.Size(105, 21);
            this.cboIndentMode.TabIndex = 78;
            this.cboIndentMode.Tag = null;
            this.cboIndentMode.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboIndentMode, "(default)");
            this.cboIndentMode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboIndentMode.SelectedIndexChanged += new System.EventHandler(this.cboWordWrapIndentMode_SelectedIndexChanged);
            // 
            // lblStarShowIndentGuide
            // 
            this.lblStarShowIndentGuide.AutoSize = true;
            this.lblStarShowIndentGuide.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarShowIndentGuide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarShowIndentGuide.Location = new System.Drawing.Point(163, 26);
            this.lblStarShowIndentGuide.Name = "lblStarShowIndentGuide";
            this.lblStarShowIndentGuide.Size = new System.Drawing.Size(14, 15);
            this.lblStarShowIndentGuide.TabIndex = 82;
            this.lblStarShowIndentGuide.Text = "*";
            this.lblStarShowIndentGuide.Visible = false;
            // 
            // grpIndicate
            // 
            this.grpIndicate.Controls.Add(this.cboBookmarkStyle);
            this.grpIndicate.Controls.Add(this.editorIndicator);
            this.grpIndicate.Controls.Add(this.lblStarIndicate);
            this.grpIndicate.Controls.Add(this.lblErrorLineBackground);
            this.grpIndicate.Controls.Add(this.pnlErrorLineBackground);
            this.grpIndicate.Controls.Add(this.lblBookmarkBackground);
            this.grpIndicate.Controls.Add(this.pnlBookmarkBackground);
            this.grpIndicate.Controls.Add(this.lblBookmarkStyle);
            this.grpIndicate.Location = new System.Drawing.Point(283, 15);
            this.grpIndicate.Name = "grpIndicate";
            this.grpIndicate.Size = new System.Drawing.Size(562, 86);
            this.grpIndicate.TabIndex = 52;
            this.grpIndicate.TabStop = false;
            this.grpIndicate.Text = "Indicate (after executing SQL statement)";
            // 
            // cboBookmarkStyle
            // 
            this.cboBookmarkStyle.AllowSpinLoop = false;
            this.cboBookmarkStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboBookmarkStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboBookmarkStyle.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboBookmarkStyle.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboBookmarkStyle.GapHeight = 0;
            this.cboBookmarkStyle.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboBookmarkStyle.Items.Add("Normal");
            this.cboBookmarkStyle.Items.Add("SYSDBA");
            this.cboBookmarkStyle.Items.Add("SYSOPER");
            this.cboBookmarkStyle.ItemsDisplayMember = "";
            this.cboBookmarkStyle.ItemsValueMember = "";
            this.cboBookmarkStyle.Location = new System.Drawing.Point(386, 24);
            this.cboBookmarkStyle.Name = "cboBookmarkStyle";
            this.cboBookmarkStyle.Size = new System.Drawing.Size(105, 21);
            this.cboBookmarkStyle.TabIndex = 79;
            this.cboBookmarkStyle.Tag = null;
            this.cboBookmarkStyle.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboBookmarkStyle, "(default)");
            this.cboBookmarkStyle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboBookmarkStyle.SelectedIndexChanged += new System.EventHandler(this.cboBookmarkStyle_SelectedIndexChanged);
            // 
            // editorIndicator
            // 
            this.editorIndicator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorIndicator.CaretLineBackColor = System.Drawing.Color.LightYellow;
            this.editorIndicator.CaretLineVisible = true;
            this.editorIndicator.Enabled = false;
            this.editorIndicator.EndAtLastLine = false;
            this.editorIndicator.HScrollBar = false;
            this.editorIndicator.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.editorIndicator.Location = new System.Drawing.Point(299, 54);
            this.editorIndicator.Name = "editorIndicator";
            this.editorIndicator.ScrollWidth = 400;
            this.editorIndicator.SelectionEolFilled = true;
            this.editorIndicator.Size = new System.Drawing.Size(234, 21);
            this.editorIndicator.Styler = null;
            this.editorIndicator.TabIndex = 50;
            this.editorIndicator.VScrollBar = false;
            this.editorIndicator.WhitespaceSize = 3;
            this.editorIndicator.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            // 
            // lblStarIndicate
            // 
            this.lblStarIndicate.AutoSize = true;
            this.lblStarIndicate.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarIndicate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarIndicate.Location = new System.Drawing.Point(227, 3);
            this.lblStarIndicate.Name = "lblStarIndicate";
            this.lblStarIndicate.Size = new System.Drawing.Size(14, 15);
            this.lblStarIndicate.TabIndex = 49;
            this.lblStarIndicate.Text = "*";
            // 
            // lblErrorLineBackground
            // 
            this.lblErrorLineBackground.AutoSize = true;
            this.lblErrorLineBackground.Location = new System.Drawing.Point(17, 26);
            this.lblErrorLineBackground.Name = "lblErrorLineBackground";
            this.lblErrorLineBackground.Size = new System.Drawing.Size(161, 16);
            this.lblErrorLineBackground.TabIndex = 44;
            this.lblErrorLineBackground.Text = "Error Keyword Background:";
            this.lblErrorLineBackground.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlErrorLineBackground
            // 
            this.pnlErrorLineBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlErrorLineBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlErrorLineBackground.Location = new System.Drawing.Point(181, 24);
            this.pnlErrorLineBackground.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlErrorLineBackground.Name = "pnlErrorLineBackground";
            this.pnlErrorLineBackground.Size = new System.Drawing.Size(74, 21);
            this.pnlErrorLineBackground.TabIndex = 43;
            this.pnlErrorLineBackground.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // lblBookmarkBackground
            // 
            this.lblBookmarkBackground.AutoSize = true;
            this.lblBookmarkBackground.Location = new System.Drawing.Point(17, 56);
            this.lblBookmarkBackground.Name = "lblBookmarkBackground";
            this.lblBookmarkBackground.Size = new System.Drawing.Size(139, 16);
            this.lblBookmarkBackground.TabIndex = 40;
            this.lblBookmarkBackground.Text = "Bookmark Background:";
            this.lblBookmarkBackground.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlBookmarkBackground
            // 
            this.pnlBookmarkBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlBookmarkBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBookmarkBackground.Location = new System.Drawing.Point(181, 54);
            this.pnlBookmarkBackground.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlBookmarkBackground.Name = "pnlBookmarkBackground";
            this.pnlBookmarkBackground.Size = new System.Drawing.Size(74, 21);
            this.pnlBookmarkBackground.TabIndex = 39;
            this.pnlBookmarkBackground.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // lblBookmarkStyle
            // 
            this.lblBookmarkStyle.AutoSize = true;
            this.lblBookmarkStyle.Location = new System.Drawing.Point(285, 26);
            this.lblBookmarkStyle.Name = "lblBookmarkStyle";
            this.lblBookmarkStyle.Size = new System.Drawing.Size(98, 16);
            this.lblBookmarkStyle.TabIndex = 41;
            this.lblBookmarkStyle.Text = "Bookmark Style:";
            this.lblBookmarkStyle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkCtrlMouseWheel1
            // 
            this.chkCtrlMouseWheel1.AutoSize = true;
            this.chkCtrlMouseWheel1.BackColor = System.Drawing.Color.Transparent;
            this.chkCtrlMouseWheel1.BorderColor = System.Drawing.Color.Transparent;
            this.chkCtrlMouseWheel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkCtrlMouseWheel1.Checked = true;
            this.chkCtrlMouseWheel1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCtrlMouseWheel1.Enabled = false;
            this.chkCtrlMouseWheel1.ForeColor = System.Drawing.Color.Black;
            this.chkCtrlMouseWheel1.Location = new System.Drawing.Point(282, 239);
            this.chkCtrlMouseWheel1.Name = "chkCtrlMouseWheel1";
            this.chkCtrlMouseWheel1.Padding = new System.Windows.Forms.Padding(1);
            this.chkCtrlMouseWheel1.Size = new System.Drawing.Size(304, 22);
            this.chkCtrlMouseWheel1.TabIndex = 80;
            this.chkCtrlMouseWheel1.Text = "Change Font Size (Zoom) with Ctrl+MouseWheel";
            this.c1ThemeController1.SetTheme(this.chkCtrlMouseWheel1, "(default)");
            this.chkCtrlMouseWheel1.UseVisualStyleBackColor = true;
            this.chkCtrlMouseWheel1.Value = true;
            this.chkCtrlMouseWheel1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // grpWordWrap
            // 
            this.grpWordWrap.Controls.Add(this.chkMargin);
            this.grpWordWrap.Controls.Add(this.chkEnd);
            this.grpWordWrap.Controls.Add(this.chkStart);
            this.grpWordWrap.Controls.Add(this.chkWordWrap);
            this.grpWordWrap.Controls.Add(this.lblStarWordWrap);
            this.grpWordWrap.Location = new System.Drawing.Point(12, 232);
            this.grpWordWrap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpWordWrap.Name = "grpWordWrap";
            this.grpWordWrap.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpWordWrap.Size = new System.Drawing.Size(250, 51);
            this.grpWordWrap.TabIndex = 42;
            this.grpWordWrap.TabStop = false;
            // 
            // chkMargin
            // 
            this.chkMargin.AutoSize = true;
            this.chkMargin.BackColor = System.Drawing.Color.Transparent;
            this.chkMargin.BorderColor = System.Drawing.Color.Transparent;
            this.chkMargin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkMargin.ForeColor = System.Drawing.Color.Black;
            this.chkMargin.Location = new System.Drawing.Point(161, 22);
            this.chkMargin.Name = "chkMargin";
            this.chkMargin.Padding = new System.Windows.Forms.Padding(1);
            this.chkMargin.Size = new System.Drawing.Size(70, 22);
            this.chkMargin.TabIndex = 69;
            this.chkMargin.Text = "Margin";
            this.c1ThemeController1.SetTheme(this.chkMargin, "(default)");
            this.chkMargin.UseVisualStyleBackColor = true;
            this.chkMargin.Value = null;
            this.chkMargin.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkMargin.CheckedChanged += new System.EventHandler(this.WordWrapFlags_CheckedChanged);
            // 
            // chkEnd
            // 
            this.chkEnd.AutoSize = true;
            this.chkEnd.BackColor = System.Drawing.Color.Transparent;
            this.chkEnd.BorderColor = System.Drawing.Color.Transparent;
            this.chkEnd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkEnd.ForeColor = System.Drawing.Color.Black;
            this.chkEnd.Location = new System.Drawing.Point(93, 22);
            this.chkEnd.Name = "chkEnd";
            this.chkEnd.Padding = new System.Windows.Forms.Padding(1);
            this.chkEnd.Size = new System.Drawing.Size(51, 22);
            this.chkEnd.TabIndex = 68;
            this.chkEnd.Text = "End";
            this.c1ThemeController1.SetTheme(this.chkEnd, "(default)");
            this.chkEnd.UseVisualStyleBackColor = true;
            this.chkEnd.Value = null;
            this.chkEnd.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkEnd.CheckedChanged += new System.EventHandler(this.WordWrapFlags_CheckedChanged);
            // 
            // chkStart
            // 
            this.chkStart.AutoSize = true;
            this.chkStart.BackColor = System.Drawing.Color.Transparent;
            this.chkStart.BorderColor = System.Drawing.Color.Transparent;
            this.chkStart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkStart.ForeColor = System.Drawing.Color.Black;
            this.chkStart.Location = new System.Drawing.Point(20, 22);
            this.chkStart.Name = "chkStart";
            this.chkStart.Padding = new System.Windows.Forms.Padding(1);
            this.chkStart.Size = new System.Drawing.Size(55, 22);
            this.chkStart.TabIndex = 67;
            this.chkStart.Text = "Start";
            this.c1ThemeController1.SetTheme(this.chkStart, "(default)");
            this.chkStart.UseVisualStyleBackColor = true;
            this.chkStart.Value = null;
            this.chkStart.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkStart.CheckedChanged += new System.EventHandler(this.WordWrapFlags_CheckedChanged);
            // 
            // chkWordWrap
            // 
            this.chkWordWrap.AutoSize = true;
            this.chkWordWrap.BackColor = System.Drawing.Color.Transparent;
            this.chkWordWrap.BorderColor = System.Drawing.Color.Transparent;
            this.chkWordWrap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkWordWrap.ForeColor = System.Drawing.Color.Black;
            this.chkWordWrap.Location = new System.Drawing.Point(12, -1);
            this.chkWordWrap.Name = "chkWordWrap";
            this.chkWordWrap.Padding = new System.Windows.Forms.Padding(1);
            this.chkWordWrap.Size = new System.Drawing.Size(95, 22);
            this.chkWordWrap.TabIndex = 66;
            this.chkWordWrap.Text = "Word Wrap";
            this.c1ThemeController1.SetTheme(this.chkWordWrap, "(default)");
            this.chkWordWrap.UseVisualStyleBackColor = true;
            this.chkWordWrap.Value = null;
            this.chkWordWrap.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkWordWrap.CheckedChanged += new System.EventHandler(this.chkWordWrap_CheckedChanged);
            // 
            // lblStarWordWrap
            // 
            this.lblStarWordWrap.AutoSize = true;
            this.lblStarWordWrap.BackColor = System.Drawing.Color.Transparent;
            this.lblStarWordWrap.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarWordWrap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarWordWrap.Location = new System.Drawing.Point(137, 3);
            this.lblStarWordWrap.Name = "lblStarWordWrap";
            this.lblStarWordWrap.Size = new System.Drawing.Size(14, 15);
            this.lblStarWordWrap.TabIndex = 47;
            this.lblStarWordWrap.Text = "*";
            // 
            // chkOpenFileOnCurrentTab
            // 
            this.chkOpenFileOnCurrentTab.AutoSize = true;
            this.chkOpenFileOnCurrentTab.BackColor = System.Drawing.Color.Transparent;
            this.chkOpenFileOnCurrentTab.BorderColor = System.Drawing.Color.Transparent;
            this.chkOpenFileOnCurrentTab.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkOpenFileOnCurrentTab.Checked = true;
            this.chkOpenFileOnCurrentTab.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpenFileOnCurrentTab.ForeColor = System.Drawing.Color.Black;
            this.chkOpenFileOnCurrentTab.Location = new System.Drawing.Point(479, 64);
            this.chkOpenFileOnCurrentTab.Name = "chkOpenFileOnCurrentTab";
            this.chkOpenFileOnCurrentTab.Padding = new System.Windows.Forms.Padding(1);
            this.chkOpenFileOnCurrentTab.Size = new System.Drawing.Size(360, 22);
            this.chkOpenFileOnCurrentTab.TabIndex = 79;
            this.chkOpenFileOnCurrentTab.Text = "Open File button: Open the specified file on the current tab";
            this.c1ThemeController1.SetTheme(this.chkOpenFileOnCurrentTab, "(default)");
            this.chkOpenFileOnCurrentTab.UseVisualStyleBackColor = true;
            this.chkOpenFileOnCurrentTab.Value = true;
            this.chkOpenFileOnCurrentTab.Visible = false;
            this.chkOpenFileOnCurrentTab.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkShowSaveAsButton
            // 
            this.chkShowSaveAsButton.AutoSize = true;
            this.chkShowSaveAsButton.BackColor = System.Drawing.Color.Transparent;
            this.chkShowSaveAsButton.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowSaveAsButton.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowSaveAsButton.Checked = true;
            this.chkShowSaveAsButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowSaveAsButton.ForeColor = System.Drawing.Color.Black;
            this.chkShowSaveAsButton.Location = new System.Drawing.Point(282, 214);
            this.chkShowSaveAsButton.Name = "chkShowSaveAsButton";
            this.chkShowSaveAsButton.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowSaveAsButton.Size = new System.Drawing.Size(152, 22);
            this.chkShowSaveAsButton.TabIndex = 78;
            this.chkShowSaveAsButton.Text = "Show \'Save As\' Button";
            this.c1ThemeController1.SetTheme(this.chkShowSaveAsButton, "(default)");
            this.chkShowSaveAsButton.UseVisualStyleBackColor = true;
            this.chkShowSaveAsButton.Value = true;
            this.chkShowSaveAsButton.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShowSaveAsButton.CheckedChanged += new System.EventHandler(this.chkShowSaveAsButton_CheckedChanged);
            // 
            // cboEditorZoom
            // 
            this.cboEditorZoom.AllowSpinLoop = false;
            this.cboEditorZoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboEditorZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboEditorZoom.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboEditorZoom.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboEditorZoom.GapHeight = 0;
            this.cboEditorZoom.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboEditorZoom.Items.Add("-2");
            this.cboEditorZoom.Items.Add("-1");
            this.cboEditorZoom.Items.Add("0");
            this.cboEditorZoom.Items.Add("1");
            this.cboEditorZoom.Items.Add("2");
            this.cboEditorZoom.ItemsDisplayMember = "";
            this.cboEditorZoom.ItemsValueMember = "";
            this.cboEditorZoom.Location = new System.Drawing.Point(797, 298);
            this.cboEditorZoom.Name = "cboEditorZoom";
            this.cboEditorZoom.Size = new System.Drawing.Size(40, 21);
            this.cboEditorZoom.TabIndex = 77;
            this.cboEditorZoom.Tag = null;
            this.cboEditorZoom.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboEditorZoom, "(default)");
            this.cboEditorZoom.Visible = false;
            this.cboEditorZoom.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboEditorZoom.SelectedIndexChanged += new System.EventHandler(this.cboEditorZoom_SelectedIndexChanged);
            // 
            // cboEditorFontSize
            // 
            this.cboEditorFontSize.AllowSpinLoop = false;
            this.cboEditorFontSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboEditorFontSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboEditorFontSize.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboEditorFontSize.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboEditorFontSize.GapHeight = 0;
            this.cboEditorFontSize.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboEditorFontSize.Items.Add("10");
            this.cboEditorFontSize.Items.Add("12");
            this.cboEditorFontSize.Items.Add("14");
            this.cboEditorFontSize.Items.Add("16");
            this.cboEditorFontSize.Items.Add("18");
            this.cboEditorFontSize.ItemsDisplayMember = "";
            this.cboEditorFontSize.ItemsValueMember = "";
            this.cboEditorFontSize.Location = new System.Drawing.Point(102, 56);
            this.cboEditorFontSize.Name = "cboEditorFontSize";
            this.cboEditorFontSize.Size = new System.Drawing.Size(40, 21);
            this.cboEditorFontSize.TabIndex = 76;
            this.cboEditorFontSize.Tag = null;
            this.cboEditorFontSize.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboEditorFontSize, "(default)");
            this.cboEditorFontSize.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboEditorFontSize.SelectedIndexChanged += new System.EventHandler(this.cboEditorFontSize_SelectedIndexChanged);
            // 
            // cboSaveAsEncoding
            // 
            this.cboSaveAsEncoding.AllowSpinLoop = false;
            this.cboSaveAsEncoding.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboSaveAsEncoding.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboSaveAsEncoding.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboSaveAsEncoding.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboSaveAsEncoding.Enabled = false;
            this.cboSaveAsEncoding.GapHeight = 0;
            this.cboSaveAsEncoding.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboSaveAsEncoding.InitialSelectedIndex = 0;
            this.cboSaveAsEncoding.Items.Add("UTF-8");
            this.cboSaveAsEncoding.ItemsDisplayMember = "UTF-8";
            this.cboSaveAsEncoding.ItemsValueMember = "";
            this.cboSaveAsEncoding.Location = new System.Drawing.Point(437, 165);
            this.cboSaveAsEncoding.Name = "cboSaveAsEncoding";
            this.cboSaveAsEncoding.Size = new System.Drawing.Size(63, 21);
            this.cboSaveAsEncoding.TabIndex = 74;
            this.cboSaveAsEncoding.Tag = null;
            this.cboSaveAsEncoding.Text = "UTF-8";
            this.cboSaveAsEncoding.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboSaveAsEncoding, "(default)");
            this.cboSaveAsEncoding.Value = "UTF-8";
            this.cboSaveAsEncoding.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkEntireBlankRowAsEmptyRow4SelectBlock
            // 
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.AutoSize = true;
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.BackColor = System.Drawing.Color.Transparent;
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.BorderColor = System.Drawing.Color.Transparent;
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.Checked = true;
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.ForeColor = System.Drawing.Color.Black;
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.Location = new System.Drawing.Point(282, 264);
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.Name = "chkEntireBlankRowAsEmptyRow4SelectBlock";
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.Padding = new System.Windows.Forms.Padding(1);
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.Size = new System.Drawing.Size(442, 22);
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.TabIndex = 72;
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.Text = "Select Current Block: The entire row of blanks is regarded as an empty row";
            this.c1ThemeController1.SetTheme(this.chkEntireBlankRowAsEmptyRow4SelectBlock, "(default)");
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.UseVisualStyleBackColor = true;
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.Value = true;
            this.chkEntireBlankRowAsEmptyRow4SelectBlock.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkHighlightSelectedText
            // 
            this.chkHighlightSelectedText.AutoSize = true;
            this.chkHighlightSelectedText.BackColor = System.Drawing.Color.Transparent;
            this.chkHighlightSelectedText.BorderColor = System.Drawing.Color.Transparent;
            this.chkHighlightSelectedText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkHighlightSelectedText.Checked = true;
            this.chkHighlightSelectedText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHighlightSelectedText.Enabled = false;
            this.chkHighlightSelectedText.ForeColor = System.Drawing.Color.Black;
            this.chkHighlightSelectedText.Location = new System.Drawing.Point(636, 295);
            this.chkHighlightSelectedText.Name = "chkHighlightSelectedText";
            this.chkHighlightSelectedText.Padding = new System.Windows.Forms.Padding(1);
            this.chkHighlightSelectedText.Size = new System.Drawing.Size(312, 22);
            this.chkHighlightSelectedText.TabIndex = 71;
            this.chkHighlightSelectedText.Text = "Highlight Selected Text When Mouse Double Click";
            this.c1ThemeController1.SetTheme(this.chkHighlightSelectedText, "(default)");
            this.chkHighlightSelectedText.UseVisualStyleBackColor = true;
            this.chkHighlightSelectedText.Value = true;
            this.chkHighlightSelectedText.Visible = false;
            this.chkHighlightSelectedText.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkSaveAsEncoding
            // 
            this.chkSaveAsEncoding.AutoSize = true;
            this.chkSaveAsEncoding.BackColor = System.Drawing.Color.Transparent;
            this.chkSaveAsEncoding.BorderColor = System.Drawing.Color.Transparent;
            this.chkSaveAsEncoding.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkSaveAsEncoding.Checked = true;
            this.chkSaveAsEncoding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveAsEncoding.Enabled = false;
            this.chkSaveAsEncoding.ForeColor = System.Drawing.Color.Black;
            this.chkSaveAsEncoding.Location = new System.Drawing.Point(282, 164);
            this.chkSaveAsEncoding.Name = "chkSaveAsEncoding";
            this.chkSaveAsEncoding.Padding = new System.Windows.Forms.Padding(1);
            this.chkSaveAsEncoding.Size = new System.Drawing.Size(131, 22);
            this.chkSaveAsEncoding.TabIndex = 70;
            this.chkSaveAsEncoding.Text = "Save as Encoding:";
            this.c1ThemeController1.SetTheme(this.chkSaveAsEncoding, "(default)");
            this.chkSaveAsEncoding.UseVisualStyleBackColor = true;
            this.chkSaveAsEncoding.Value = true;
            this.chkSaveAsEncoding.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkCopyAsHTML
            // 
            this.chkCopyAsHTML.AutoSize = true;
            this.chkCopyAsHTML.BackColor = System.Drawing.Color.Transparent;
            this.chkCopyAsHTML.BorderColor = System.Drawing.Color.Transparent;
            this.chkCopyAsHTML.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkCopyAsHTML.ForeColor = System.Drawing.Color.Black;
            this.chkCopyAsHTML.Location = new System.Drawing.Point(282, 139);
            this.chkCopyAsHTML.Name = "chkCopyAsHTML";
            this.chkCopyAsHTML.Padding = new System.Windows.Forms.Padding(1);
            this.chkCopyAsHTML.Size = new System.Drawing.Size(198, 22);
            this.chkCopyAsHTML.TabIndex = 68;
            this.chkCopyAsHTML.Text = "Copy SQL Statement as HTML";
            this.c1ThemeController1.SetTheme(this.chkCopyAsHTML, "(default)");
            this.chkCopyAsHTML.UseVisualStyleBackColor = true;
            this.chkCopyAsHTML.Value = null;
            this.chkCopyAsHTML.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkCopyAsHTML.CheckedChanged += new System.EventHandler(this.chkCopyAsHTML_CheckedChanged);
            // 
            // chkBold
            // 
            this.chkBold.AutoSize = true;
            this.chkBold.BackColor = System.Drawing.Color.Transparent;
            this.chkBold.BorderColor = System.Drawing.Color.Transparent;
            this.chkBold.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkBold.ForeColor = System.Drawing.Color.Black;
            this.chkBold.Location = new System.Drawing.Point(282, 114);
            this.chkBold.Name = "chkBold";
            this.chkBold.Padding = new System.Windows.Forms.Padding(1);
            this.chkBold.Size = new System.Drawing.Size(193, 22);
            this.chkBold.TabIndex = 66;
            this.chkBold.Text = "Using Bold Text for Keywords";
            this.c1ThemeController1.SetTheme(this.chkBold, "(default)");
            this.chkBold.UseVisualStyleBackColor = true;
            this.chkBold.Value = null;
            this.chkBold.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkBold.CheckedChanged += new System.EventHandler(this.chkBold_CheckedChanged);
            // 
            // cboEditorFontPicker
            // 
            this.cboEditorFontPicker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.cboEditorFontPicker.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboEditorFontPicker.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboEditorFontPicker.Location = new System.Drawing.Point(102, 24);
            this.cboEditorFontPicker.Name = "cboEditorFontPicker";
            this.cboEditorFontPicker.Size = new System.Drawing.Size(143, 21);
            this.cboEditorFontPicker.TabIndex = 56;
            this.cboEditorFontPicker.Tag = null;
            this.c1ThemeController1.SetTheme(this.cboEditorFontPicker, "(default)");
            this.cboEditorFontPicker.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboEditorFontPicker.TextChanged += new System.EventHandler(this.cboEditorFontPicker_TextChanged);
            // 
            // grpHighlight
            // 
            this.grpHighlight.Controls.Add(this.cboHighlightStyle);
            this.grpHighlight.Controls.Add(this.cboHighlightAlpha);
            this.grpHighlight.Controls.Add(this.cboHighlightOutlineAlpha);
            this.grpHighlight.Controls.Add(this.lblStarHighlight);
            this.grpHighlight.Controls.Add(this.lblHighlightColorAlpha);
            this.grpHighlight.Controls.Add(this.lblHighlightColorOutlineAlpha);
            this.grpHighlight.Controls.Add(this.lblHighlightColorStyle);
            this.grpHighlight.Controls.Add(this.pnlHighlightForeColor);
            this.grpHighlight.Controls.Add(this.lblHighlightColorForeColor);
            this.grpHighlight.Location = new System.Drawing.Point(12, 85);
            this.grpHighlight.Name = "grpHighlight";
            this.grpHighlight.Size = new System.Drawing.Size(250, 138);
            this.grpHighlight.TabIndex = 25;
            this.grpHighlight.TabStop = false;
            this.grpHighlight.Text = "Highlight";
            // 
            // cboHighlightStyle
            // 
            this.cboHighlightStyle.AllowSpinLoop = false;
            this.cboHighlightStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboHighlightStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboHighlightStyle.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboHighlightStyle.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboHighlightStyle.GapHeight = 0;
            this.cboHighlightStyle.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboHighlightStyle.Items.Add("Box");
            this.cboHighlightStyle.Items.Add("CompositionThick");
            this.cboHighlightStyle.Items.Add("Dash");
            this.cboHighlightStyle.Items.Add("Diagonal");
            this.cboHighlightStyle.Items.Add("StraightBox");
            this.cboHighlightStyle.ItemsDisplayMember = "";
            this.cboHighlightStyle.ItemsValueMember = "";
            this.cboHighlightStyle.Location = new System.Drawing.Point(101, 50);
            this.cboHighlightStyle.Name = "cboHighlightStyle";
            this.cboHighlightStyle.Size = new System.Drawing.Size(128, 21);
            this.cboHighlightStyle.TabIndex = 78;
            this.cboHighlightStyle.Tag = null;
            this.cboHighlightStyle.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboHighlightStyle, "(default)");
            this.cboHighlightStyle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboHighlightStyle.SelectedIndexChanged += new System.EventHandler(this.HighlightPreview_SelectedIndexChanged);
            // 
            // cboHighlightAlpha
            // 
            this.cboHighlightAlpha.AllowSpinLoop = false;
            this.cboHighlightAlpha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboHighlightAlpha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboHighlightAlpha.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboHighlightAlpha.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboHighlightAlpha.GapHeight = 0;
            this.cboHighlightAlpha.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboHighlightAlpha.Items.Add("50");
            this.cboHighlightAlpha.Items.Add("60");
            this.cboHighlightAlpha.Items.Add("70");
            this.cboHighlightAlpha.Items.Add("80");
            this.cboHighlightAlpha.Items.Add("90");
            this.cboHighlightAlpha.Items.Add("100");
            this.cboHighlightAlpha.Items.Add("110");
            this.cboHighlightAlpha.Items.Add("120");
            this.cboHighlightAlpha.Items.Add("130");
            this.cboHighlightAlpha.Items.Add("140");
            this.cboHighlightAlpha.Items.Add("150");
            this.cboHighlightAlpha.Items.Add("160");
            this.cboHighlightAlpha.Items.Add("170");
            this.cboHighlightAlpha.Items.Add("180");
            this.cboHighlightAlpha.Items.Add("190");
            this.cboHighlightAlpha.Items.Add("200");
            this.cboHighlightAlpha.ItemsDisplayMember = "";
            this.cboHighlightAlpha.ItemsValueMember = "";
            this.cboHighlightAlpha.Location = new System.Drawing.Point(101, 106);
            this.cboHighlightAlpha.Name = "cboHighlightAlpha";
            this.cboHighlightAlpha.Size = new System.Drawing.Size(54, 21);
            this.cboHighlightAlpha.TabIndex = 80;
            this.cboHighlightAlpha.Tag = null;
            this.cboHighlightAlpha.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboHighlightAlpha, "(default)");
            this.cboHighlightAlpha.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboHighlightAlpha.SelectedIndexChanged += new System.EventHandler(this.HighlightPreview_SelectedIndexChanged);
            // 
            // cboHighlightOutlineAlpha
            // 
            this.cboHighlightOutlineAlpha.AllowSpinLoop = false;
            this.cboHighlightOutlineAlpha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboHighlightOutlineAlpha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboHighlightOutlineAlpha.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboHighlightOutlineAlpha.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboHighlightOutlineAlpha.GapHeight = 0;
            this.cboHighlightOutlineAlpha.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboHighlightOutlineAlpha.Items.Add("50");
            this.cboHighlightOutlineAlpha.Items.Add("60");
            this.cboHighlightOutlineAlpha.Items.Add("70");
            this.cboHighlightOutlineAlpha.Items.Add("80");
            this.cboHighlightOutlineAlpha.Items.Add("90");
            this.cboHighlightOutlineAlpha.Items.Add("100");
            this.cboHighlightOutlineAlpha.Items.Add("110");
            this.cboHighlightOutlineAlpha.Items.Add("120");
            this.cboHighlightOutlineAlpha.Items.Add("130");
            this.cboHighlightOutlineAlpha.Items.Add("140");
            this.cboHighlightOutlineAlpha.Items.Add("150");
            this.cboHighlightOutlineAlpha.Items.Add("160");
            this.cboHighlightOutlineAlpha.Items.Add("170");
            this.cboHighlightOutlineAlpha.Items.Add("180");
            this.cboHighlightOutlineAlpha.Items.Add("190");
            this.cboHighlightOutlineAlpha.Items.Add("200");
            this.cboHighlightOutlineAlpha.ItemsDisplayMember = "";
            this.cboHighlightOutlineAlpha.ItemsValueMember = "";
            this.cboHighlightOutlineAlpha.Location = new System.Drawing.Point(101, 78);
            this.cboHighlightOutlineAlpha.Name = "cboHighlightOutlineAlpha";
            this.cboHighlightOutlineAlpha.Size = new System.Drawing.Size(54, 21);
            this.cboHighlightOutlineAlpha.TabIndex = 79;
            this.cboHighlightOutlineAlpha.Tag = null;
            this.cboHighlightOutlineAlpha.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboHighlightOutlineAlpha, "(default)");
            this.cboHighlightOutlineAlpha.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboHighlightOutlineAlpha.SelectedIndexChanged += new System.EventHandler(this.HighlightPreview_SelectedIndexChanged);
            // 
            // lblStarHighlight
            // 
            this.lblStarHighlight.AutoSize = true;
            this.lblStarHighlight.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarHighlight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarHighlight.Location = new System.Drawing.Point(101, 3);
            this.lblStarHighlight.Name = "lblStarHighlight";
            this.lblStarHighlight.Size = new System.Drawing.Size(14, 15);
            this.lblStarHighlight.TabIndex = 48;
            this.lblStarHighlight.Text = "*";
            // 
            // lblHighlightColorAlpha
            // 
            this.lblHighlightColorAlpha.Location = new System.Drawing.Point(16, 107);
            this.lblHighlightColorAlpha.Name = "lblHighlightColorAlpha";
            this.lblHighlightColorAlpha.Size = new System.Drawing.Size(82, 16);
            this.lblHighlightColorAlpha.TabIndex = 12;
            this.lblHighlightColorAlpha.Text = "Alpha:";
            this.lblHighlightColorAlpha.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHighlightColorOutlineAlpha
            // 
            this.lblHighlightColorOutlineAlpha.Location = new System.Drawing.Point(-6, 79);
            this.lblHighlightColorOutlineAlpha.Name = "lblHighlightColorOutlineAlpha";
            this.lblHighlightColorOutlineAlpha.Size = new System.Drawing.Size(104, 16);
            this.lblHighlightColorOutlineAlpha.TabIndex = 9;
            this.lblHighlightColorOutlineAlpha.Text = "Outline Alpha:";
            this.lblHighlightColorOutlineAlpha.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHighlightColorStyle
            // 
            this.lblHighlightColorStyle.Location = new System.Drawing.Point(23, 51);
            this.lblHighlightColorStyle.Name = "lblHighlightColorStyle";
            this.lblHighlightColorStyle.Size = new System.Drawing.Size(75, 16);
            this.lblHighlightColorStyle.TabIndex = 4;
            this.lblHighlightColorStyle.Text = "Style:";
            this.lblHighlightColorStyle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlHighlightForeColor
            // 
            this.pnlHighlightForeColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlHighlightForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHighlightForeColor.Location = new System.Drawing.Point(101, 22);
            this.pnlHighlightForeColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlHighlightForeColor.Name = "pnlHighlightForeColor";
            this.pnlHighlightForeColor.Size = new System.Drawing.Size(74, 21);
            this.pnlHighlightForeColor.TabIndex = 3;
            this.pnlHighlightForeColor.Click += new System.EventHandler(this.pnlSelectedClick);
            // 
            // lblHighlightColorForeColor
            // 
            this.lblHighlightColorForeColor.Location = new System.Drawing.Point(-2, 23);
            this.lblHighlightColorForeColor.Name = "lblHighlightColorForeColor";
            this.lblHighlightColorForeColor.Size = new System.Drawing.Size(100, 17);
            this.lblHighlightColorForeColor.TabIndex = 2;
            this.lblHighlightColorForeColor.Text = "Fore Color:";
            this.lblHighlightColorForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStarHighlightSelection
            // 
            this.lblStarHighlightSelection.AutoSize = true;
            this.lblStarHighlightSelection.Enabled = false;
            this.lblStarHighlightSelection.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarHighlightSelection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarHighlightSelection.Location = new System.Drawing.Point(794, 90);
            this.lblStarHighlightSelection.Name = "lblStarHighlightSelection";
            this.lblStarHighlightSelection.Size = new System.Drawing.Size(14, 15);
            this.lblStarHighlightSelection.TabIndex = 50;
            this.lblStarHighlightSelection.Text = "*";
            this.lblStarHighlightSelection.Visible = false;
            // 
            // lblEditorFontName
            // 
            this.lblEditorFontName.Location = new System.Drawing.Point(7, 25);
            this.lblEditorFontName.Name = "lblEditorFontName";
            this.lblEditorFontName.Size = new System.Drawing.Size(92, 16);
            this.lblEditorFontName.TabIndex = 43;
            this.lblEditorFontName.Text = "Font Name:";
            this.lblEditorFontName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEditorFontSize
            // 
            this.lblEditorFontSize.Location = new System.Drawing.Point(24, 57);
            this.lblEditorFontSize.Name = "lblEditorFontSize";
            this.lblEditorFontSize.Size = new System.Drawing.Size(75, 16);
            this.lblEditorFontSize.TabIndex = 45;
            this.lblEditorFontSize.Text = "Font Size:";
            this.lblEditorFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEditorZoom
            // 
            this.lblEditorZoom.Location = new System.Drawing.Point(719, 293);
            this.lblEditorZoom.Name = "lblEditorZoom";
            this.lblEditorZoom.Size = new System.Drawing.Size(75, 16);
            this.lblEditorZoom.TabIndex = 14;
            this.lblEditorZoom.Text = "Zoom:";
            this.lblEditorZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEditorZoom.Visible = false;
            // 
            // chkShowAllCharacters
            // 
            this.chkShowAllCharacters.AutoSize = true;
            this.chkShowAllCharacters.BackColor = System.Drawing.Color.Transparent;
            this.chkShowAllCharacters.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowAllCharacters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowAllCharacters.ForeColor = System.Drawing.Color.Black;
            this.chkShowAllCharacters.Location = new System.Drawing.Point(282, 189);
            this.chkShowAllCharacters.Name = "chkShowAllCharacters";
            this.chkShowAllCharacters.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowAllCharacters.Size = new System.Drawing.Size(139, 22);
            this.chkShowAllCharacters.TabIndex = 69;
            this.chkShowAllCharacters.Text = "Show All Characters";
            this.c1ThemeController1.SetTheme(this.chkShowAllCharacters, "(default)");
            this.chkShowAllCharacters.UseVisualStyleBackColor = true;
            this.chkShowAllCharacters.Value = null;
            this.chkShowAllCharacters.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShowAllCharacters.CheckedChanged += new System.EventHandler(this.chkShowAllCharacters_CheckedChanged);
            // 
            // chkHighlightSelection
            // 
            this.chkHighlightSelection.AutoSize = true;
            this.chkHighlightSelection.BackColor = System.Drawing.Color.Transparent;
            this.chkHighlightSelection.BorderColor = System.Drawing.Color.Transparent;
            this.chkHighlightSelection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkHighlightSelection.ForeColor = System.Drawing.Color.Black;
            this.chkHighlightSelection.Location = new System.Drawing.Point(550, 79);
            this.chkHighlightSelection.Name = "chkHighlightSelection";
            this.chkHighlightSelection.Padding = new System.Windows.Forms.Padding(1);
            this.chkHighlightSelection.Size = new System.Drawing.Size(243, 22);
            this.chkHighlightSelection.TabIndex = 73;
            this.chkHighlightSelection.Text = "Highlight Selection When Mouse Click";
            this.c1ThemeController1.SetTheme(this.chkHighlightSelection, "(default)");
            this.chkHighlightSelection.UseVisualStyleBackColor = true;
            this.chkHighlightSelection.Value = null;
            this.chkHighlightSelection.Visible = false;
            this.chkHighlightSelection.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // grpColorTheme
            // 
            this.grpColorTheme.BackColor = System.Drawing.Color.Transparent;
            this.grpColorTheme.Controls.Add(this.btnHelp_DarkMode);
            this.grpColorTheme.Controls.Add(this.chkDarkMode);
            this.grpColorTheme.Location = new System.Drawing.Point(12, 9);
            this.grpColorTheme.Name = "grpColorTheme";
            this.grpColorTheme.Size = new System.Drawing.Size(158, 58);
            this.grpColorTheme.TabIndex = 43;
            this.grpColorTheme.TabStop = false;
            this.grpColorTheme.Text = "Color Theme";
            // 
            // btnHelp_DarkMode
            // 
            this.btnHelp_DarkMode.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp_DarkMode.Image")));
            this.btnHelp_DarkMode.Location = new System.Drawing.Point(115, 23);
            this.btnHelp_DarkMode.Name = "btnHelp_DarkMode";
            this.btnHelp_DarkMode.Size = new System.Drawing.Size(21, 21);
            this.btnHelp_DarkMode.TabIndex = 80;
            this.c1ThemeController1.SetTheme(this.btnHelp_DarkMode, "(default)");
            this.btnHelp_DarkMode.UseVisualStyleBackColor = true;
            this.btnHelp_DarkMode.Visible = false;
            this.btnHelp_DarkMode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnHelp_DarkMode.Click += new System.EventHandler(this.btnHelp_DarkMode_Click);
            // 
            // chkDarkMode
            // 
            this.chkDarkMode.AutoSize = true;
            this.chkDarkMode.BackColor = System.Drawing.Color.Transparent;
            this.chkDarkMode.BorderColor = System.Drawing.Color.Transparent;
            this.chkDarkMode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkDarkMode.ForeColor = System.Drawing.Color.Black;
            this.chkDarkMode.Location = new System.Drawing.Point(17, 23);
            this.chkDarkMode.Name = "chkDarkMode";
            this.chkDarkMode.Padding = new System.Windows.Forms.Padding(1);
            this.chkDarkMode.Size = new System.Drawing.Size(93, 22);
            this.chkDarkMode.TabIndex = 79;
            this.chkDarkMode.Text = "Dark Mode";
            this.c1ThemeController1.SetTheme(this.chkDarkMode, "(default)");
            this.chkDarkMode.UseVisualStyleBackColor = true;
            this.chkDarkMode.Value = null;
            this.chkDarkMode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // grpQueryEditorPreview
            // 
            this.grpQueryEditorPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpQueryEditorPreview.BackColor = System.Drawing.Color.Transparent;
            this.grpQueryEditorPreview.Controls.Add(this.tsEditor);
            this.grpQueryEditorPreview.Controls.Add(this.editor);
            this.grpQueryEditorPreview.Location = new System.Drawing.Point(339, 307);
            this.grpQueryEditorPreview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpQueryEditorPreview.Name = "grpQueryEditorPreview";
            this.grpQueryEditorPreview.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpQueryEditorPreview.Size = new System.Drawing.Size(861, 366);
            this.grpQueryEditorPreview.TabIndex = 20;
            this.grpQueryEditorPreview.TabStop = false;
            this.grpQueryEditorPreview.Text = "Preview";
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
            this.toolStripSeparator4,
            this.btnCode2SQL,
            this.btnSQL2Code,
            this.toolStripSeparator9,
            this.toolStripSeparator3,
            this.btnComment,
            this.btnRemoveComment,
            this.toolStripSeparator6,
            this.btnIndent,
            this.txtIndentWord,
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
            this.toolStripSeparator7});
            this.tsEditor.Location = new System.Drawing.Point(3, 20);
            this.tsEditor.Name = "tsEditor";
            this.tsEditor.Size = new System.Drawing.Size(855, 31);
            this.tsEditor.TabIndex = 41;
            this.c1ThemeController1.SetTheme(this.tsEditor, "(default)");
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Enabled = false;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(28, 28);
            this.btnNew.Tag = "";
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Enabled = false;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(28, 28);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Enabled = false;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(28, 28);
            // 
            // btnSaveRed
            // 
            this.btnSaveRed.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveRed.Enabled = false;
            this.btnSaveRed.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveRed.Image")));
            this.btnSaveRed.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveRed.Name = "btnSaveRed";
            this.btnSaveRed.Size = new System.Drawing.Size(28, 28);
            this.btnSaveRed.Visible = false;
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAs.Enabled = false;
            this.btnSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAs.Image")));
            this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(28, 28);
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
            // 
            // btnSelectCurrentBlock
            // 
            this.btnSelectCurrentBlock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectCurrentBlock.Enabled = false;
            this.btnSelectCurrentBlock.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectCurrentBlock.Image")));
            this.btnSelectCurrentBlock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectCurrentBlock.Name = "btnSelectCurrentBlock";
            this.btnSelectCurrentBlock.Size = new System.Drawing.Size(28, 28);
            // 
            // btnExecuteCurrentBlock
            // 
            this.btnExecuteCurrentBlock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExecuteCurrentBlock.Enabled = false;
            this.btnExecuteCurrentBlock.Image = ((System.Drawing.Image)(resources.GetObject("btnExecuteCurrentBlock.Image")));
            this.btnExecuteCurrentBlock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExecuteCurrentBlock.Name = "btnExecuteCurrentBlock";
            this.btnExecuteCurrentBlock.Size = new System.Drawing.Size(28, 28);
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
            // 
            // mnuCSharp2SQL
            // 
            this.mnuCSharp2SQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuCSharp2SQL.Name = "mnuCSharp2SQL";
            this.mnuCSharp2SQL.Size = new System.Drawing.Size(209, 22);
            this.mnuCSharp2SQL.Text = "C# to SQL";
            // 
            // mnuVB2SQL
            // 
            this.mnuVB2SQL.Name = "mnuVB2SQL";
            this.mnuVB2SQL.Size = new System.Drawing.Size(209, 22);
            this.mnuVB2SQL.Text = "VB.Net/VB6/VBA to SQL";
            // 
            // mnuDephi2SQL
            // 
            this.mnuDephi2SQL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuDephi2SQL.Name = "mnuDephi2SQL";
            this.mnuDephi2SQL.Size = new System.Drawing.Size(209, 22);
            this.mnuDephi2SQL.Text = "Dephi6 to SQL";
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
            this.mnuCSharpStyle1.Tag = "C#";
            this.mnuCSharpStyle1.Text = "Style 1: Using + operator";
            // 
            // mnuCSharpStyle2
            // 
            this.mnuCSharpStyle2.Name = "mnuCSharpStyle2";
            this.mnuCSharpStyle2.Size = new System.Drawing.Size(340, 22);
            this.mnuCSharpStyle2.Tag = "C#";
            this.mnuCSharpStyle2.Text = "Style 2: New line character \\r\\n";
            // 
            // mnuCSharpStyle3
            // 
            this.mnuCSharpStyle3.Name = "mnuCSharpStyle3";
            this.mnuCSharpStyle3.Size = new System.Drawing.Size(340, 22);
            this.mnuCSharpStyle3.Tag = "C#";
            this.mnuCSharpStyle3.Text = "Style 3: New line character Enviroment.NewLine";
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
            this.mnuVBNetStyle1.Tag = "VB.Net";
            this.mnuVBNetStyle1.Text = "Style 1: Using && operator";
            // 
            // mnuVBNetStyle2
            // 
            this.mnuVBNetStyle2.Name = "mnuVBNetStyle2";
            this.mnuVBNetStyle2.Size = new System.Drawing.Size(340, 22);
            this.mnuVBNetStyle2.Tag = "VB.Net";
            this.mnuVBNetStyle2.Text = "Style 2: New line character VbCrLf";
            // 
            // mnuVBNetStyle3
            // 
            this.mnuVBNetStyle3.Name = "mnuVBNetStyle3";
            this.mnuVBNetStyle3.Size = new System.Drawing.Size(340, 22);
            this.mnuVBNetStyle3.Tag = "VB.Net";
            this.mnuVBNetStyle3.Text = "Style 3: New line character Enviroment.NewLine";
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
            this.mnuVB6AStyle1.Tag = "VB6/VBA";
            this.mnuVB6AStyle1.Text = "Style 1: Using && operator";
            // 
            // mnuVB6AStyle2
            // 
            this.mnuVB6AStyle2.Name = "mnuVB6AStyle2";
            this.mnuVB6AStyle2.Size = new System.Drawing.Size(262, 22);
            this.mnuVB6AStyle2.Tag = "VB6/VBA";
            this.mnuVB6AStyle2.Text = "Style 2: New line character VbCrLf";
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
            this.mnuDelphi6Style1.Tag = "Delphi6";
            this.mnuDelphi6Style1.Text = "Style 1: Using + operator";
            // 
            // mnuDelphi6Style2
            // 
            this.mnuDelphi6Style2.Name = "mnuDelphi6Style2";
            this.mnuDelphi6Style2.Size = new System.Drawing.Size(268, 22);
            this.mnuDelphi6Style2.Tag = "Delphi6";
            this.mnuDelphi6Style2.Text = "Style 2: New line character #13#10";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 31);
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
            // 
            // btnRemoveComment
            // 
            this.btnRemoveComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveComment.Enabled = false;
            this.btnRemoveComment.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveComment.Image")));
            this.btnRemoveComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveComment.Name = "btnRemoveComment";
            this.btnRemoveComment.Size = new System.Drawing.Size(28, 28);
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
            // 
            // txtIndentWord
            // 
            this.txtIndentWord.BackColor = System.Drawing.Color.Ivory;
            this.txtIndentWord.Enabled = false;
            this.txtIndentWord.MaxLength = 1;
            this.txtIndentWord.Name = "txtIndentWord";
            this.txtIndentWord.Size = new System.Drawing.Size(15, 31);
            this.txtIndentWord.Text = "4";
            this.txtIndentWord.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 31);
            // 
            // btnHighlightSelection
            // 
            this.btnHighlightSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHighlightSelection.Enabled = false;
            this.btnHighlightSelection.Image = ((System.Drawing.Image)(resources.GetObject("btnHighlightSelection.Image")));
            this.btnHighlightSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHighlightSelection.Name = "btnHighlightSelection";
            this.btnHighlightSelection.Size = new System.Drawing.Size(28, 28);
            this.btnHighlightSelection.Visible = false;
            // 
            // btnHighlightSelection2
            // 
            this.btnHighlightSelection2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHighlightSelection2.Enabled = false;
            this.btnHighlightSelection2.Image = ((System.Drawing.Image)(resources.GetObject("btnHighlightSelection2.Image")));
            this.btnHighlightSelection2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHighlightSelection2.Name = "btnHighlightSelection2";
            this.btnHighlightSelection2.Size = new System.Drawing.Size(28, 28);
            this.btnHighlightSelection2.Visible = false;
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
            this.btnWordWrap.Enabled = false;
            this.btnWordWrap.Image = ((System.Drawing.Image)(resources.GetObject("btnWordWrap.Image")));
            this.btnWordWrap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWordWrap.Name = "btnWordWrap";
            this.btnWordWrap.Size = new System.Drawing.Size(28, 28);
            // 
            // btnWordWrap2
            // 
            this.btnWordWrap2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnWordWrap2.Enabled = false;
            this.btnWordWrap2.Image = ((System.Drawing.Image)(resources.GetObject("btnWordWrap2.Image")));
            this.btnWordWrap2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWordWrap2.Name = "btnWordWrap2";
            this.btnWordWrap2.Size = new System.Drawing.Size(28, 28);
            this.btnWordWrap2.Visible = false;
            // 
            // btnShowAllCharacters
            // 
            this.btnShowAllCharacters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowAllCharacters.Enabled = false;
            this.btnShowAllCharacters.Image = ((System.Drawing.Image)(resources.GetObject("btnShowAllCharacters.Image")));
            this.btnShowAllCharacters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowAllCharacters.Name = "btnShowAllCharacters";
            this.btnShowAllCharacters.Size = new System.Drawing.Size(28, 28);
            // 
            // btnShowAllCharacters2
            // 
            this.btnShowAllCharacters2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowAllCharacters2.Enabled = false;
            this.btnShowAllCharacters2.Image = ((System.Drawing.Image)(resources.GetObject("btnShowAllCharacters2.Image")));
            this.btnShowAllCharacters2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowAllCharacters2.Name = "btnShowAllCharacters2";
            this.btnShowAllCharacters2.Size = new System.Drawing.Size(28, 28);
            this.btnShowAllCharacters2.Visible = false;
            // 
            // btnShowIndentGuide
            // 
            this.btnShowIndentGuide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowIndentGuide.Enabled = false;
            this.btnShowIndentGuide.Image = ((System.Drawing.Image)(resources.GetObject("btnShowIndentGuide.Image")));
            this.btnShowIndentGuide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowIndentGuide.Name = "btnShowIndentGuide";
            this.btnShowIndentGuide.Size = new System.Drawing.Size(28, 28);
            // 
            // btnShowIndentGuide2
            // 
            this.btnShowIndentGuide2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowIndentGuide2.Enabled = false;
            this.btnShowIndentGuide2.Image = ((System.Drawing.Image)(resources.GetObject("btnShowIndentGuide2.Image")));
            this.btnShowIndentGuide2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowIndentGuide2.Name = "btnShowIndentGuide2";
            this.btnShowIndentGuide2.Size = new System.Drawing.Size(28, 28);
            this.btnShowIndentGuide2.Visible = false;
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
            // 
            // editor
            // 
            this.editor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editor.CaretLineVisible = true;
            this.editor.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.editor.Location = new System.Drawing.Point(12, 55);
            this.editor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editor.Name = "editor";
            this.editor.ScrollWidth = 3618;
            this.editor.Size = new System.Drawing.Size(838, 299);
            this.editor.Styler = null;
            this.editor.TabIndex = 40;
            this.editor.Text = resources.GetString("editor.Text");
            this.editor.WhitespaceSize = 3;
            this.editor.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.editor.WrapMode = ScintillaNET.WrapMode.Word;
            this.editor.DoubleClick += new System.EventHandler<ScintillaNET.DoubleClickEventArgs>(this.editor_DoubleClick);
            this.editor.ZoomChanged += new System.EventHandler<System.EventArgs>(this.editor_ZoomChanged);
            this.editor.Enter += new System.EventHandler(this.editor_Enter);
            this.editor.Leave += new System.EventHandler(this.editor_Leave);
            this.editor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.editor_MouseClick);
            this.editor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editor_MouseDown);
            // 
            // grpAutoComplete
            // 
            this.grpAutoComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAutoComplete.Controls.Add(this.chkFirstCharChecking);
            this.grpAutoComplete.Controls.Add(this.lblStarAC);
            this.grpAutoComplete.Controls.Add(this.nudMinFragmentLength);
            this.grpAutoComplete.Controls.Add(this.chkEnableAutoComplete);
            this.grpAutoComplete.Controls.Add(this.lblMinFragmentLength);
            this.grpAutoComplete.Controls.Add(this.grpAutoCompleteFor);
            this.grpAutoComplete.Location = new System.Drawing.Point(12, 9);
            this.grpAutoComplete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpAutoComplete.Name = "grpAutoComplete";
            this.grpAutoComplete.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpAutoComplete.Size = new System.Drawing.Size(1186, 664);
            this.grpAutoComplete.TabIndex = 0;
            this.grpAutoComplete.TabStop = false;
            this.grpAutoComplete.Text = "                                                                                 " +
    "   ";
            this.c1ThemeController1.SetTheme(this.grpAutoComplete, "(default)");
            // 
            // chkFirstCharChecking
            // 
            this.chkFirstCharChecking.AutoSize = true;
            this.chkFirstCharChecking.BackColor = System.Drawing.Color.Transparent;
            this.chkFirstCharChecking.BorderColor = System.Drawing.Color.Transparent;
            this.chkFirstCharChecking.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkFirstCharChecking.ForeColor = System.Drawing.Color.Black;
            this.chkFirstCharChecking.Location = new System.Drawing.Point(21, 55);
            this.chkFirstCharChecking.Name = "chkFirstCharChecking";
            this.chkFirstCharChecking.Padding = new System.Windows.Forms.Padding(1);
            this.chkFirstCharChecking.Size = new System.Drawing.Size(415, 22);
            this.chkFirstCharChecking.TabIndex = 313;
            this.chkFirstCharChecking.Text = "If the first character is not an English letter, it is automatically ignored!";
            this.c1ThemeController1.SetTheme(this.chkFirstCharChecking, "(default)");
            this.chkFirstCharChecking.UseVisualStyleBackColor = true;
            this.chkFirstCharChecking.Value = null;
            this.chkFirstCharChecking.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblStarAC
            // 
            this.lblStarAC.AutoSize = true;
            this.lblStarAC.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarAC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarAC.Location = new System.Drawing.Point(267, 3);
            this.lblStarAC.Name = "lblStarAC";
            this.lblStarAC.Size = new System.Drawing.Size(14, 15);
            this.lblStarAC.TabIndex = 42;
            this.lblStarAC.Text = "*";
            // 
            // nudMinFragmentLength
            // 
            this.nudMinFragmentLength.ForeColor = System.Drawing.Color.Black;
            this.nudMinFragmentLength.Location = new System.Drawing.Point(270, 26);
            this.nudMinFragmentLength.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudMinFragmentLength.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudMinFragmentLength.Name = "nudMinFragmentLength";
            this.nudMinFragmentLength.Size = new System.Drawing.Size(31, 23);
            this.nudMinFragmentLength.TabIndex = 311;
            this.nudMinFragmentLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.c1ThemeController1.SetTheme(this.nudMinFragmentLength, "Office2010Blue");
            this.nudMinFragmentLength.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudMinFragmentLength.Leave += new System.EventHandler(this.nudMinFragmentLength_Leave);
            // 
            // chkEnableAutoComplete
            // 
            this.chkEnableAutoComplete.AutoSize = true;
            this.chkEnableAutoComplete.BackColor = System.Drawing.Color.Transparent;
            this.chkEnableAutoComplete.BorderColor = System.Drawing.Color.Transparent;
            this.chkEnableAutoComplete.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkEnableAutoComplete.ForeColor = System.Drawing.Color.Black;
            this.chkEnableAutoComplete.Location = new System.Drawing.Point(12, -1);
            this.chkEnableAutoComplete.Name = "chkEnableAutoComplete";
            this.chkEnableAutoComplete.Padding = new System.Windows.Forms.Padding(1);
            this.chkEnableAutoComplete.Size = new System.Drawing.Size(237, 22);
            this.chkEnableAutoComplete.TabIndex = 301;
            this.chkEnableAutoComplete.Text = "Enable Auto Complete on each input";
            this.c1ThemeController1.SetTheme(this.chkEnableAutoComplete, "(default)");
            this.chkEnableAutoComplete.UseVisualStyleBackColor = true;
            this.chkEnableAutoComplete.Value = null;
            this.chkEnableAutoComplete.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkEnableAutoComplete.CheckedChanged += new System.EventHandler(this.chkEnableGroupBox_CheckedChanged);
            // 
            // lblMinFragmentLength
            // 
            this.lblMinFragmentLength.AutoSize = true;
            this.lblMinFragmentLength.Location = new System.Drawing.Point(21, 28);
            this.lblMinFragmentLength.Name = "lblMinFragmentLength";
            this.lblMinFragmentLength.Size = new System.Drawing.Size(216, 16);
            this.lblMinFragmentLength.TabIndex = 50;
            this.lblMinFragmentLength.Text = "Mininum fragment length for popup:";
            this.lblMinFragmentLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpAutoCompleteFor
            // 
            this.grpAutoCompleteFor.Controls.Add(this.chkUserDefinedViews);
            this.grpAutoCompleteFor.Controls.Add(this.chkUserDefinedTriggers);
            this.grpAutoCompleteFor.Controls.Add(this.chkUserDefinedTables);
            this.grpAutoCompleteFor.Controls.Add(this.chkUserDefinedFunctions);
            this.grpAutoCompleteFor.Controls.Add(this.chkUserDefinedKeywords);
            this.grpAutoCompleteFor.Controls.Add(this.chkBuiltInKeywords);
            this.grpAutoCompleteFor.Controls.Add(this.chkBuiltInFunctions);
            this.grpAutoCompleteFor.Location = new System.Drawing.Point(13, 91);
            this.grpAutoCompleteFor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpAutoCompleteFor.Name = "grpAutoCompleteFor";
            this.grpAutoCompleteFor.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpAutoCompleteFor.Size = new System.Drawing.Size(446, 225);
            this.grpAutoCompleteFor.TabIndex = 1;
            this.grpAutoCompleteFor.TabStop = false;
            this.grpAutoCompleteFor.Text = "Auto Complete for";
            // 
            // chkUserDefinedViews
            // 
            this.chkUserDefinedViews.AutoSize = true;
            this.chkUserDefinedViews.BackColor = System.Drawing.Color.Transparent;
            this.chkUserDefinedViews.BorderColor = System.Drawing.Color.Transparent;
            this.chkUserDefinedViews.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkUserDefinedViews.ForeColor = System.Drawing.Color.Black;
            this.chkUserDefinedViews.Location = new System.Drawing.Point(16, 189);
            this.chkUserDefinedViews.Name = "chkUserDefinedViews";
            this.chkUserDefinedViews.Padding = new System.Windows.Forms.Padding(1);
            this.chkUserDefinedViews.Size = new System.Drawing.Size(138, 22);
            this.chkUserDefinedViews.TabIndex = 309;
            this.chkUserDefinedViews.Text = "User-defined Views";
            this.c1ThemeController1.SetTheme(this.chkUserDefinedViews, "(default)");
            this.chkUserDefinedViews.UseVisualStyleBackColor = true;
            this.chkUserDefinedViews.Value = null;
            this.chkUserDefinedViews.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkUserDefinedTriggers
            // 
            this.chkUserDefinedTriggers.AutoSize = true;
            this.chkUserDefinedTriggers.BackColor = System.Drawing.Color.Transparent;
            this.chkUserDefinedTriggers.BorderColor = System.Drawing.Color.Transparent;
            this.chkUserDefinedTriggers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkUserDefinedTriggers.ForeColor = System.Drawing.Color.Black;
            this.chkUserDefinedTriggers.Location = new System.Drawing.Point(16, 161);
            this.chkUserDefinedTriggers.Name = "chkUserDefinedTriggers";
            this.chkUserDefinedTriggers.Padding = new System.Windows.Forms.Padding(1);
            this.chkUserDefinedTriggers.Size = new System.Drawing.Size(152, 22);
            this.chkUserDefinedTriggers.TabIndex = 308;
            this.chkUserDefinedTriggers.Text = "User-defined Triggers";
            this.c1ThemeController1.SetTheme(this.chkUserDefinedTriggers, "(default)");
            this.chkUserDefinedTriggers.UseVisualStyleBackColor = true;
            this.chkUserDefinedTriggers.Value = null;
            this.chkUserDefinedTriggers.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkUserDefinedTables
            // 
            this.chkUserDefinedTables.AutoSize = true;
            this.chkUserDefinedTables.BackColor = System.Drawing.Color.Transparent;
            this.chkUserDefinedTables.BorderColor = System.Drawing.Color.Transparent;
            this.chkUserDefinedTables.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkUserDefinedTables.ForeColor = System.Drawing.Color.Black;
            this.chkUserDefinedTables.Location = new System.Drawing.Point(16, 134);
            this.chkUserDefinedTables.Name = "chkUserDefinedTables";
            this.chkUserDefinedTables.Padding = new System.Windows.Forms.Padding(1);
            this.chkUserDefinedTables.Size = new System.Drawing.Size(143, 22);
            this.chkUserDefinedTables.TabIndex = 307;
            this.chkUserDefinedTables.Text = "User-defined Tables";
            this.c1ThemeController1.SetTheme(this.chkUserDefinedTables, "(default)");
            this.chkUserDefinedTables.UseVisualStyleBackColor = true;
            this.chkUserDefinedTables.Value = null;
            this.chkUserDefinedTables.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkUserDefinedFunctions
            // 
            this.chkUserDefinedFunctions.AutoSize = true;
            this.chkUserDefinedFunctions.BackColor = System.Drawing.Color.Transparent;
            this.chkUserDefinedFunctions.BorderColor = System.Drawing.Color.Transparent;
            this.chkUserDefinedFunctions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkUserDefinedFunctions.ForeColor = System.Drawing.Color.Black;
            this.chkUserDefinedFunctions.Location = new System.Drawing.Point(16, 107);
            this.chkUserDefinedFunctions.Name = "chkUserDefinedFunctions";
            this.chkUserDefinedFunctions.Padding = new System.Windows.Forms.Padding(1);
            this.chkUserDefinedFunctions.Size = new System.Drawing.Size(159, 22);
            this.chkUserDefinedFunctions.TabIndex = 306;
            this.chkUserDefinedFunctions.Text = "User-defined Functions";
            this.c1ThemeController1.SetTheme(this.chkUserDefinedFunctions, "(default)");
            this.chkUserDefinedFunctions.UseVisualStyleBackColor = true;
            this.chkUserDefinedFunctions.Value = null;
            this.chkUserDefinedFunctions.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkUserDefinedKeywords
            // 
            this.chkUserDefinedKeywords.AutoSize = true;
            this.chkUserDefinedKeywords.BackColor = System.Drawing.Color.Transparent;
            this.chkUserDefinedKeywords.BorderColor = System.Drawing.Color.Transparent;
            this.chkUserDefinedKeywords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkUserDefinedKeywords.ForeColor = System.Drawing.Color.Black;
            this.chkUserDefinedKeywords.Location = new System.Drawing.Point(16, 80);
            this.chkUserDefinedKeywords.Name = "chkUserDefinedKeywords";
            this.chkUserDefinedKeywords.Padding = new System.Windows.Forms.Padding(1);
            this.chkUserDefinedKeywords.Size = new System.Drawing.Size(160, 22);
            this.chkUserDefinedKeywords.TabIndex = 304;
            this.chkUserDefinedKeywords.Text = "User-defined Keywords";
            this.c1ThemeController1.SetTheme(this.chkUserDefinedKeywords, "(default)");
            this.chkUserDefinedKeywords.UseVisualStyleBackColor = true;
            this.chkUserDefinedKeywords.Value = null;
            this.chkUserDefinedKeywords.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkBuiltInKeywords
            // 
            this.chkBuiltInKeywords.AutoSize = true;
            this.chkBuiltInKeywords.BackColor = System.Drawing.Color.Transparent;
            this.chkBuiltInKeywords.BorderColor = System.Drawing.Color.Transparent;
            this.chkBuiltInKeywords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkBuiltInKeywords.ForeColor = System.Drawing.Color.Black;
            this.chkBuiltInKeywords.Location = new System.Drawing.Point(16, 53);
            this.chkBuiltInKeywords.Name = "chkBuiltInKeywords";
            this.chkBuiltInKeywords.Padding = new System.Windows.Forms.Padding(1);
            this.chkBuiltInKeywords.Size = new System.Drawing.Size(125, 22);
            this.chkBuiltInKeywords.TabIndex = 303;
            this.chkBuiltInKeywords.Text = "Built-in Keywords";
            this.c1ThemeController1.SetTheme(this.chkBuiltInKeywords, "(default)");
            this.chkBuiltInKeywords.UseVisualStyleBackColor = true;
            this.chkBuiltInKeywords.Value = null;
            this.chkBuiltInKeywords.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkBuiltInFunctions
            // 
            this.chkBuiltInFunctions.AutoSize = true;
            this.chkBuiltInFunctions.BackColor = System.Drawing.Color.Transparent;
            this.chkBuiltInFunctions.BorderColor = System.Drawing.Color.Transparent;
            this.chkBuiltInFunctions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkBuiltInFunctions.ForeColor = System.Drawing.Color.Black;
            this.chkBuiltInFunctions.Location = new System.Drawing.Point(16, 26);
            this.chkBuiltInFunctions.Name = "chkBuiltInFunctions";
            this.chkBuiltInFunctions.Padding = new System.Windows.Forms.Padding(1);
            this.chkBuiltInFunctions.Size = new System.Drawing.Size(124, 22);
            this.chkBuiltInFunctions.TabIndex = 302;
            this.chkBuiltInFunctions.Text = "Built-in Functions";
            this.c1ThemeController1.SetTheme(this.chkBuiltInFunctions, "(default)");
            this.chkBuiltInFunctions.UseVisualStyleBackColor = true;
            this.chkBuiltInFunctions.Value = null;
            this.chkBuiltInFunctions.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // grpAutoReplace
            // 
            this.grpAutoReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAutoReplace.BackColor = System.Drawing.Color.Transparent;
            this.grpAutoReplace.Controls.Add(this.lblARInfo2);
            this.grpAutoReplace.Controls.Add(this.chkShowFilterRowAR);
            this.grpAutoReplace.Controls.Add(this.lblARInfo1);
            this.grpAutoReplace.Controls.Add(this.lblStarAR);
            this.grpAutoReplace.Controls.Add(this.grpModifyDefinitionAR);
            this.grpAutoReplace.Controls.Add(this.grpDefinitionAR);
            this.grpAutoReplace.Controls.Add(this.chkEnableAutoReplace);
            this.grpAutoReplace.Location = new System.Drawing.Point(12, 9);
            this.grpAutoReplace.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpAutoReplace.Name = "grpAutoReplace";
            this.grpAutoReplace.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpAutoReplace.Size = new System.Drawing.Size(1186, 664);
            this.grpAutoReplace.TabIndex = 0;
            this.grpAutoReplace.TabStop = false;
            this.grpAutoReplace.Text = "                             ";
            // 
            // lblARInfo2
            // 
            this.lblARInfo2.AutoSize = true;
            this.lblARInfo2.Location = new System.Drawing.Point(20, 51);
            this.lblARInfo2.Name = "lblARInfo2";
            this.lblARInfo2.Size = new System.Drawing.Size(299, 16);
            this.lblARInfo2.TabIndex = 403;
            this.lblARInfo2.Text = "Press Ctrl+Z to restore \"Replacement\" to \"Keyword\".";
            this.c1ThemeController1.SetTheme(this.lblARInfo2, "(default)");
            // 
            // chkShowFilterRowAR
            // 
            this.chkShowFilterRowAR.AutoSize = true;
            this.chkShowFilterRowAR.BackColor = System.Drawing.Color.Transparent;
            this.chkShowFilterRowAR.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowFilterRowAR.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowFilterRowAR.ForeColor = System.Drawing.Color.Black;
            this.chkShowFilterRowAR.Location = new System.Drawing.Point(683, 25);
            this.chkShowFilterRowAR.Name = "chkShowFilterRowAR";
            this.chkShowFilterRowAR.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowFilterRowAR.Size = new System.Drawing.Size(118, 22);
            this.chkShowFilterRowAR.TabIndex = 402;
            this.chkShowFilterRowAR.Text = "Show Filter Row";
            this.c1ThemeController1.SetTheme(this.chkShowFilterRowAR, "(default)");
            this.chkShowFilterRowAR.UseVisualStyleBackColor = true;
            this.chkShowFilterRowAR.Value = null;
            this.chkShowFilterRowAR.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShowFilterRowAR.CheckedChanged += new System.EventHandler(this.chkShowFilterRow_CheckedChanged);
            // 
            // lblARInfo1
            // 
            this.lblARInfo1.AutoSize = true;
            this.lblARInfo1.Location = new System.Drawing.Point(20, 26);
            this.lblARInfo1.Name = "lblARInfo1";
            this.lblARInfo1.Size = new System.Drawing.Size(561, 16);
            this.lblARInfo1.TabIndex = 62;
            this.lblARInfo1.Text = "After entering the \"Keyword\", press the blank key to replace the \"Keyword\" with t" +
    "he \"Replacement\".";
            // 
            // lblStarAR
            // 
            this.lblStarAR.AutoSize = true;
            this.lblStarAR.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarAR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarAR.Location = new System.Drawing.Point(154, 3);
            this.lblStarAR.Name = "lblStarAR";
            this.lblStarAR.Size = new System.Drawing.Size(14, 15);
            this.lblStarAR.TabIndex = 8;
            this.lblStarAR.Text = "*";
            // 
            // grpModifyDefinitionAR
            // 
            this.grpModifyDefinitionAR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpModifyDefinitionAR.Controls.Add(this.editorAR);
            this.grpModifyDefinitionAR.Controls.Add(this.lblTips);
            this.grpModifyDefinitionAR.Controls.Add(this.txtKeyword);
            this.grpModifyDefinitionAR.Controls.Add(this.btnClearAR);
            this.grpModifyDefinitionAR.Controls.Add(this.btnCancelAR);
            this.grpModifyDefinitionAR.Controls.Add(this.btnSaveAR);
            this.grpModifyDefinitionAR.Controls.Add(this.lblReplacement);
            this.grpModifyDefinitionAR.Controls.Add(this.lblKeyword);
            this.grpModifyDefinitionAR.Location = new System.Drawing.Point(726, 77);
            this.grpModifyDefinitionAR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpModifyDefinitionAR.Name = "grpModifyDefinitionAR";
            this.grpModifyDefinitionAR.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpModifyDefinitionAR.Size = new System.Drawing.Size(447, 572);
            this.grpModifyDefinitionAR.TabIndex = 2;
            this.grpModifyDefinitionAR.TabStop = false;
            this.grpModifyDefinitionAR.Tag = "0";
            this.grpModifyDefinitionAR.Text = "Modify definition";
            // 
            // editorAR
            // 
            this.editorAR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorAR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorAR.CaretLineVisible = true;
            this.editorAR.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.editorAR.Location = new System.Drawing.Point(16, 107);
            this.editorAR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editorAR.Name = "editorAR";
            this.editorAR.ReadOnly = true;
            this.editorAR.ScrollWidth = 3618;
            this.editorAR.Size = new System.Drawing.Size(332, 339);
            this.editorAR.Styler = null;
            this.editorAR.TabIndex = 412;
            this.editorAR.WhitespaceSize = 3;
            this.editorAR.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.editorAR.WrapMode = ScintillaNET.WrapMode.Word;
            // 
            // lblTips
            // 
            this.lblTips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTips.AutoSize = true;
            this.lblTips.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTips.Location = new System.Drawing.Point(13, 450);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(249, 16);
            this.lblTips.TabIndex = 79;
            this.lblTips.Text = "Using the symbol ^ to position your cursor.";
            this.c1ThemeController1.SetTheme(this.lblTips, "(default)");
            // 
            // txtKeyword
            // 
            this.txtKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.txtKeyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeyword.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtKeyword.Location = new System.Drawing.Point(16, 44);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.ReadOnly = true;
            this.txtKeyword.Size = new System.Drawing.Size(332, 23);
            this.txtKeyword.TabIndex = 407;
            this.txtKeyword.Tag = null;
            this.txtKeyword.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.txtKeyword, "(default)");
            this.txtKeyword.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // btnClearAR
            // 
            this.btnClearAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAR.Enabled = false;
            this.btnClearAR.Location = new System.Drawing.Point(365, 121);
            this.btnClearAR.Name = "btnClearAR";
            this.btnClearAR.Size = new System.Drawing.Size(70, 31);
            this.btnClearAR.TabIndex = 411;
            this.btnClearAR.Text = "Clear";
            this.c1ThemeController1.SetTheme(this.btnClearAR, "(default)");
            this.btnClearAR.UseVisualStyleBackColor = true;
            this.btnClearAR.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnClearAR.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCancelAR
            // 
            this.btnCancelAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelAR.Enabled = false;
            this.btnCancelAR.Location = new System.Drawing.Point(365, 61);
            this.btnCancelAR.Name = "btnCancelAR";
            this.btnCancelAR.Size = new System.Drawing.Size(70, 31);
            this.btnCancelAR.TabIndex = 410;
            this.btnCancelAR.Text = "Cancel";
            this.c1ThemeController1.SetTheme(this.btnCancelAR, "(default)");
            this.btnCancelAR.UseVisualStyleBackColor = true;
            this.btnCancelAR.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnCancelAR.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveAR
            // 
            this.btnSaveAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAR.Enabled = false;
            this.btnSaveAR.Location = new System.Drawing.Point(365, 21);
            this.btnSaveAR.Name = "btnSaveAR";
            this.btnSaveAR.Size = new System.Drawing.Size(70, 31);
            this.btnSaveAR.TabIndex = 409;
            this.btnSaveAR.Text = "Save";
            this.c1ThemeController1.SetTheme(this.btnSaveAR, "(default)");
            this.btnSaveAR.UseVisualStyleBackColor = true;
            this.btnSaveAR.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnSaveAR.Click += new System.EventHandler(this.btnSaveAR_Click);
            // 
            // lblReplacement
            // 
            this.lblReplacement.AutoSize = true;
            this.lblReplacement.Location = new System.Drawing.Point(13, 88);
            this.lblReplacement.Name = "lblReplacement";
            this.lblReplacement.Size = new System.Drawing.Size(86, 16);
            this.lblReplacement.TabIndex = 13;
            this.lblReplacement.Text = "Replacement:";
            this.lblReplacement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(13, 25);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(60, 16);
            this.lblKeyword.TabIndex = 12;
            this.lblKeyword.Text = "Keyword:";
            this.lblKeyword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpDefinitionAR
            // 
            this.grpDefinitionAR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpDefinitionAR.Controls.Add(this.btnDeleteAR);
            this.grpDefinitionAR.Controls.Add(this.btnEditAR);
            this.grpDefinitionAR.Controls.Add(this.btnAddAR);
            this.grpDefinitionAR.Controls.Add(this.c1GridARInfo);
            this.grpDefinitionAR.Location = new System.Drawing.Point(13, 77);
            this.grpDefinitionAR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpDefinitionAR.Name = "grpDefinitionAR";
            this.grpDefinitionAR.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpDefinitionAR.Size = new System.Drawing.Size(702, 572);
            this.grpDefinitionAR.TabIndex = 1;
            this.grpDefinitionAR.TabStop = false;
            this.grpDefinitionAR.Tag = "";
            this.grpDefinitionAR.Text = "Definition";
            // 
            // btnDeleteAR
            // 
            this.btnDeleteAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAR.Location = new System.Drawing.Point(620, 121);
            this.btnDeleteAR.Name = "btnDeleteAR";
            this.btnDeleteAR.Size = new System.Drawing.Size(70, 31);
            this.btnDeleteAR.TabIndex = 406;
            this.btnDeleteAR.Text = "Delete";
            this.c1ThemeController1.SetTheme(this.btnDeleteAR, "(default)");
            this.btnDeleteAR.UseVisualStyleBackColor = true;
            this.btnDeleteAR.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnDeleteAR.Click += new System.EventHandler(this.btnDeleteAR_Click);
            // 
            // btnEditAR
            // 
            this.btnEditAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditAR.Location = new System.Drawing.Point(620, 61);
            this.btnEditAR.Name = "btnEditAR";
            this.btnEditAR.Size = new System.Drawing.Size(70, 31);
            this.btnEditAR.TabIndex = 405;
            this.btnEditAR.Text = "Edit";
            this.c1ThemeController1.SetTheme(this.btnEditAR, "(default)");
            this.btnEditAR.UseVisualStyleBackColor = true;
            this.btnEditAR.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnEditAR.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAddAR
            // 
            this.btnAddAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAR.Location = new System.Drawing.Point(620, 21);
            this.btnAddAR.Name = "btnAddAR";
            this.btnAddAR.Size = new System.Drawing.Size(70, 31);
            this.btnAddAR.TabIndex = 404;
            this.btnAddAR.Text = "Add";
            this.c1ThemeController1.SetTheme(this.btnAddAR, "(default)");
            this.btnAddAR.UseVisualStyleBackColor = true;
            this.btnAddAR.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnAddAR.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // c1GridARInfo
            // 
            this.c1GridARInfo.AllowUpdate = false;
            this.c1GridARInfo.AllowUpdateOnBlur = false;
            this.c1GridARInfo.AlternatingRows = true;
            this.c1GridARInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1GridARInfo.CaptionHeight = 19;
            this.c1GridARInfo.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1GridARInfo.Images.Add(((System.Drawing.Image)(resources.GetObject("c1GridARInfo.Images"))));
            this.c1GridARInfo.Location = new System.Drawing.Point(10, 21);
            this.c1GridARInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.c1GridARInfo.Name = "c1GridARInfo";
            this.c1GridARInfo.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1GridARInfo.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1GridARInfo.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1GridARInfo.PreviewInfo.ZoomFactor = 75D;
            this.c1GridARInfo.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1GridARInfo.PrintInfo.MeasurementPrinterName = null;
            this.c1GridARInfo.RowHeight = 17;
            this.c1GridARInfo.Size = new System.Drawing.Size(599, 540);
            this.c1GridARInfo.TabIndex = 403;
            this.c1GridARInfo.UseCompatibleTextRendering = false;
            this.c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
            this.c1GridARInfo.RowColChange += new C1.Win.C1TrueDBGrid.RowColChangeEventHandler(this.c1GridARInfo_RowColChange);
            this.c1GridARInfo.PropBag = resources.GetString("c1GridARInfo.PropBag");
            // 
            // chkEnableAutoReplace
            // 
            this.chkEnableAutoReplace.AutoSize = true;
            this.chkEnableAutoReplace.BackColor = System.Drawing.Color.Transparent;
            this.chkEnableAutoReplace.BorderColor = System.Drawing.Color.Transparent;
            this.chkEnableAutoReplace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkEnableAutoReplace.ForeColor = System.Drawing.Color.Black;
            this.chkEnableAutoReplace.Location = new System.Drawing.Point(12, -1);
            this.chkEnableAutoReplace.Name = "chkEnableAutoReplace";
            this.chkEnableAutoReplace.Padding = new System.Windows.Forms.Padding(1);
            this.chkEnableAutoReplace.Size = new System.Drawing.Size(147, 22);
            this.chkEnableAutoReplace.TabIndex = 401;
            this.chkEnableAutoReplace.Text = "Enable Auto Replace";
            this.c1ThemeController1.SetTheme(this.chkEnableAutoReplace, "(default)");
            this.chkEnableAutoReplace.UseVisualStyleBackColor = true;
            this.chkEnableAutoReplace.Value = null;
            this.chkEnableAutoReplace.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkEnableAutoReplace.CheckedChanged += new System.EventHandler(this.chkEnableGroupBox_CheckedChanged);
            // 
            // grpDataGrid
            // 
            this.grpDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDataGrid.BackColor = System.Drawing.Color.Transparent;
            this.grpDataGrid.Controls.Add(this.chkShowGroupingRow);
            this.grpDataGrid.Controls.Add(this.btnHelp_AppendingQueries);
            this.grpDataGrid.Controls.Add(this.chkAppendingQueries);
            this.grpDataGrid.Controls.Add(this.chkSetFocusAfterQuery);
            this.grpDataGrid.Controls.Add(this.cboRowsPerPage);
            this.grpDataGrid.Controls.Add(this.lblRowsPerPage);
            this.grpDataGrid.Controls.Add(this.chkPagingQuery);
            this.grpDataGrid.Controls.Add(this.btnHelp_RawDataMode);
            this.grpDataGrid.Controls.Add(this.chkRawDataMode);
            this.grpDataGrid.Controls.Add(this.chkPreviewCLOBData);
            this.grpDataGrid.Controls.Add(this.chkUseReadOnlyQueries);
            this.grpDataGrid.Controls.Add(this.chkCtrlMouseWheel2);
            this.grpDataGrid.Controls.Add(this.cboGridRowHeightResizing);
            this.grpDataGrid.Controls.Add(this.cboGridFontSize);
            this.grpDataGrid.Controls.Add(this.cboGridZoom);
            this.grpDataGrid.Controls.Add(this.cboGridVisualStyle);
            this.grpDataGrid.Controls.Add(this.cboResultCopyFieldSeparator);
            this.grpDataGrid.Controls.Add(this.cboResultCopyQuotingWith);
            this.grpDataGrid.Controls.Add(this.cboMaxWidth);
            this.grpDataGrid.Controls.Add(this.chkResize);
            this.grpDataGrid.Controls.Add(this.chkSort);
            this.grpDataGrid.Controls.Add(this.chkShowFilterRow);
            this.grpDataGrid.Controls.Add(this.chkShowStreamlinedName);
            this.grpDataGrid.Controls.Add(this.chkShowColumnType);
            this.grpDataGrid.Controls.Add(this.cboGridFontPicker);
            this.grpDataGrid.Controls.Add(this.lblMaxWidth);
            this.grpDataGrid.Controls.Add(this.lblGridVisualStyle);
            this.grpDataGrid.Controls.Add(this.lblGridFontSize);
            this.grpDataGrid.Controls.Add(this.lblGridZoom);
            this.grpDataGrid.Controls.Add(this.lblResultCopyFieldSeparator);
            this.grpDataGrid.Controls.Add(this.lblGridFontName);
            this.grpDataGrid.Controls.Add(this.lblResultCopyQuotingWith);
            this.grpDataGrid.Controls.Add(this.grpNullValueStyle);
            this.grpDataGrid.Controls.Add(this.lblGridRowHeightResizing);
            this.grpDataGrid.Location = new System.Drawing.Point(12, 9);
            this.grpDataGrid.Name = "grpDataGrid";
            this.grpDataGrid.Size = new System.Drawing.Size(1186, 241);
            this.grpDataGrid.TabIndex = 13;
            this.grpDataGrid.TabStop = false;
            this.grpDataGrid.Text = "Data Grid";
            // 
            // chkShowGroupingRow
            // 
            this.chkShowGroupingRow.AutoSize = true;
            this.chkShowGroupingRow.BackColor = System.Drawing.Color.Transparent;
            this.chkShowGroupingRow.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowGroupingRow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowGroupingRow.ForeColor = System.Drawing.Color.Black;
            this.chkShowGroupingRow.Location = new System.Drawing.Point(643, 208);
            this.chkShowGroupingRow.Name = "chkShowGroupingRow";
            this.chkShowGroupingRow.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowGroupingRow.Size = new System.Drawing.Size(145, 22);
            this.chkShowGroupingRow.TabIndex = 94;
            this.chkShowGroupingRow.Text = "Show Grouping Row";
            this.c1ThemeController1.SetTheme(this.chkShowGroupingRow, "(default)");
            this.chkShowGroupingRow.UseVisualStyleBackColor = true;
            this.chkShowGroupingRow.Value = null;
            this.chkShowGroupingRow.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // btnHelp_AppendingQueries
            // 
            this.btnHelp_AppendingQueries.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp_AppendingQueries.Image")));
            this.btnHelp_AppendingQueries.Location = new System.Drawing.Point(193, 132);
            this.btnHelp_AppendingQueries.Name = "btnHelp_AppendingQueries";
            this.btnHelp_AppendingQueries.Size = new System.Drawing.Size(21, 21);
            this.btnHelp_AppendingQueries.TabIndex = 93;
            this.c1ThemeController1.SetTheme(this.btnHelp_AppendingQueries, "(default)");
            this.btnHelp_AppendingQueries.UseVisualStyleBackColor = true;
            this.btnHelp_AppendingQueries.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnHelp_AppendingQueries.Click += new System.EventHandler(this.btnHelp_AppendingQueries_Click);
            // 
            // chkAppendingQueries
            // 
            this.chkAppendingQueries.AutoSize = true;
            this.chkAppendingQueries.BackColor = System.Drawing.Color.Transparent;
            this.chkAppendingQueries.BorderColor = System.Drawing.Color.Transparent;
            this.chkAppendingQueries.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkAppendingQueries.ForeColor = System.Drawing.Color.Black;
            this.chkAppendingQueries.Location = new System.Drawing.Point(48, 132);
            this.chkAppendingQueries.Name = "chkAppendingQueries";
            this.chkAppendingQueries.Padding = new System.Windows.Forms.Padding(1);
            this.chkAppendingQueries.Size = new System.Drawing.Size(139, 22);
            this.chkAppendingQueries.TabIndex = 92;
            this.chkAppendingQueries.Text = "Appending Queries";
            this.c1ThemeController1.SetTheme(this.chkAppendingQueries, "(default)");
            this.chkAppendingQueries.UseVisualStyleBackColor = true;
            this.chkAppendingQueries.Value = null;
            this.chkAppendingQueries.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkSetFocusAfterQuery
            // 
            this.chkSetFocusAfterQuery.AutoSize = true;
            this.chkSetFocusAfterQuery.BackColor = System.Drawing.Color.Transparent;
            this.chkSetFocusAfterQuery.BorderColor = System.Drawing.Color.Transparent;
            this.chkSetFocusAfterQuery.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkSetFocusAfterQuery.ForeColor = System.Drawing.Color.Black;
            this.chkSetFocusAfterQuery.Location = new System.Drawing.Point(643, 181);
            this.chkSetFocusAfterQuery.Name = "chkSetFocusAfterQuery";
            this.chkSetFocusAfterQuery.Padding = new System.Windows.Forms.Padding(1);
            this.chkSetFocusAfterQuery.Size = new System.Drawing.Size(328, 22);
            this.chkSetFocusAfterQuery.TabIndex = 91;
            this.chkSetFocusAfterQuery.Text = "Set Focus to Data Grid after Execute Query Statement";
            this.c1ThemeController1.SetTheme(this.chkSetFocusAfterQuery, "(default)");
            this.chkSetFocusAfterQuery.UseVisualStyleBackColor = true;
            this.chkSetFocusAfterQuery.Value = null;
            this.chkSetFocusAfterQuery.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // cboRowsPerPage
            // 
            this.cboRowsPerPage.AllowSpinLoop = false;
            this.cboRowsPerPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboRowsPerPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboRowsPerPage.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboRowsPerPage.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboRowsPerPage.GapHeight = 0;
            this.cboRowsPerPage.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboRowsPerPage.Items.Add("100");
            this.cboRowsPerPage.Items.Add("200");
            this.cboRowsPerPage.Items.Add("300");
            this.cboRowsPerPage.Items.Add("400");
            this.cboRowsPerPage.Items.Add("500");
            this.cboRowsPerPage.Items.Add("1000");
            this.cboRowsPerPage.Items.Add("2000");
            this.cboRowsPerPage.Items.Add("5000");
            this.cboRowsPerPage.ItemsDisplayMember = "";
            this.cboRowsPerPage.ItemsValueMember = "";
            this.cboRowsPerPage.Location = new System.Drawing.Point(166, 106);
            this.cboRowsPerPage.Name = "cboRowsPerPage";
            this.cboRowsPerPage.Size = new System.Drawing.Size(60, 21);
            this.cboRowsPerPage.TabIndex = 90;
            this.cboRowsPerPage.Tag = null;
            this.cboRowsPerPage.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboRowsPerPage, "(default)");
            this.cboRowsPerPage.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblRowsPerPage
            // 
            this.lblRowsPerPage.AutoSize = true;
            this.lblRowsPerPage.Enabled = false;
            this.lblRowsPerPage.Location = new System.Drawing.Point(45, 108);
            this.lblRowsPerPage.Name = "lblRowsPerPage";
            this.lblRowsPerPage.Size = new System.Drawing.Size(94, 16);
            this.lblRowsPerPage.TabIndex = 89;
            this.lblRowsPerPage.Text = "Rows Per Page:";
            this.lblRowsPerPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkPagingQuery
            // 
            this.chkPagingQuery.AutoSize = true;
            this.chkPagingQuery.BackColor = System.Drawing.Color.Transparent;
            this.chkPagingQuery.BorderColor = System.Drawing.Color.Transparent;
            this.chkPagingQuery.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkPagingQuery.ForeColor = System.Drawing.Color.Black;
            this.chkPagingQuery.Location = new System.Drawing.Point(28, 79);
            this.chkPagingQuery.Name = "chkPagingQuery";
            this.chkPagingQuery.Padding = new System.Windows.Forms.Padding(1);
            this.chkPagingQuery.Size = new System.Drawing.Size(106, 22);
            this.chkPagingQuery.TabIndex = 88;
            this.chkPagingQuery.Text = "Paging Query";
            this.c1ThemeController1.SetTheme(this.chkPagingQuery, "(default)");
            this.chkPagingQuery.UseVisualStyleBackColor = true;
            this.chkPagingQuery.Value = null;
            this.chkPagingQuery.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkPagingQuery.CheckedChanged += new System.EventHandler(this.chkPagingQuery_CheckedChanged);
            // 
            // btnHelp_RawDataMode
            // 
            this.btnHelp_RawDataMode.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp_RawDataMode.Image")));
            this.btnHelp_RawDataMode.Location = new System.Drawing.Point(171, 53);
            this.btnHelp_RawDataMode.Name = "btnHelp_RawDataMode";
            this.btnHelp_RawDataMode.Size = new System.Drawing.Size(21, 21);
            this.btnHelp_RawDataMode.TabIndex = 86;
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
            this.chkRawDataMode.Location = new System.Drawing.Point(28, 53);
            this.chkRawDataMode.Name = "chkRawDataMode";
            this.chkRawDataMode.Padding = new System.Windows.Forms.Padding(1);
            this.chkRawDataMode.Size = new System.Drawing.Size(121, 22);
            this.chkRawDataMode.TabIndex = 85;
            this.chkRawDataMode.Text = "Raw Data Mode";
            this.c1ThemeController1.SetTheme(this.chkRawDataMode, "(default)");
            this.chkRawDataMode.UseVisualStyleBackColor = true;
            this.chkRawDataMode.Value = null;
            this.chkRawDataMode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkPreviewCLOBData
            // 
            this.chkPreviewCLOBData.AutoSize = true;
            this.chkPreviewCLOBData.BackColor = System.Drawing.Color.Transparent;
            this.chkPreviewCLOBData.BorderColor = System.Drawing.Color.Transparent;
            this.chkPreviewCLOBData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkPreviewCLOBData.Checked = true;
            this.chkPreviewCLOBData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreviewCLOBData.ForeColor = System.Drawing.Color.Black;
            this.chkPreviewCLOBData.Location = new System.Drawing.Point(28, 158);
            this.chkPreviewCLOBData.Name = "chkPreviewCLOBData";
            this.chkPreviewCLOBData.Padding = new System.Windows.Forms.Padding(1);
            this.chkPreviewCLOBData.Size = new System.Drawing.Size(136, 22);
            this.chkPreviewCLOBData.TabIndex = 87;
            this.chkPreviewCLOBData.Text = "Preview CLOB Data";
            this.c1ThemeController1.SetTheme(this.chkPreviewCLOBData, "(default)");
            this.chkPreviewCLOBData.UseVisualStyleBackColor = true;
            this.chkPreviewCLOBData.Value = true;
            this.chkPreviewCLOBData.Visible = false;
            this.chkPreviewCLOBData.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkUseReadOnlyQueries
            // 
            this.chkUseReadOnlyQueries.AutoSize = true;
            this.chkUseReadOnlyQueries.BackColor = System.Drawing.Color.Transparent;
            this.chkUseReadOnlyQueries.BorderColor = System.Drawing.Color.Transparent;
            this.chkUseReadOnlyQueries.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkUseReadOnlyQueries.Checked = true;
            this.chkUseReadOnlyQueries.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseReadOnlyQueries.Enabled = false;
            this.chkUseReadOnlyQueries.ForeColor = System.Drawing.Color.Black;
            this.chkUseReadOnlyQueries.Location = new System.Drawing.Point(28, 27);
            this.chkUseReadOnlyQueries.Name = "chkUseReadOnlyQueries";
            this.chkUseReadOnlyQueries.Padding = new System.Windows.Forms.Padding(1);
            this.chkUseReadOnlyQueries.Size = new System.Drawing.Size(160, 22);
            this.chkUseReadOnlyQueries.TabIndex = 86;
            this.chkUseReadOnlyQueries.Text = "Use Read-Only Queries";
            this.c1ThemeController1.SetTheme(this.chkUseReadOnlyQueries, "(default)");
            this.chkUseReadOnlyQueries.UseVisualStyleBackColor = true;
            this.chkUseReadOnlyQueries.Value = true;
            this.chkUseReadOnlyQueries.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkCtrlMouseWheel2
            // 
            this.chkCtrlMouseWheel2.AutoSize = true;
            this.chkCtrlMouseWheel2.BackColor = System.Drawing.Color.Transparent;
            this.chkCtrlMouseWheel2.BorderColor = System.Drawing.Color.Transparent;
            this.chkCtrlMouseWheel2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkCtrlMouseWheel2.Checked = true;
            this.chkCtrlMouseWheel2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCtrlMouseWheel2.Enabled = false;
            this.chkCtrlMouseWheel2.ForeColor = System.Drawing.Color.Black;
            this.chkCtrlMouseWheel2.Location = new System.Drawing.Point(643, 154);
            this.chkCtrlMouseWheel2.Name = "chkCtrlMouseWheel2";
            this.chkCtrlMouseWheel2.Padding = new System.Windows.Forms.Padding(1);
            this.chkCtrlMouseWheel2.Size = new System.Drawing.Size(304, 22);
            this.chkCtrlMouseWheel2.TabIndex = 81;
            this.chkCtrlMouseWheel2.Text = "Change Font Size (Zoom) with Ctrl+MouseWheel";
            this.c1ThemeController1.SetTheme(this.chkCtrlMouseWheel2, "(default)");
            this.chkCtrlMouseWheel2.UseVisualStyleBackColor = true;
            this.chkCtrlMouseWheel2.Value = true;
            this.chkCtrlMouseWheel2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // cboGridRowHeightResizing
            // 
            this.cboGridRowHeightResizing.AllowSpinLoop = false;
            this.cboGridRowHeightResizing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboGridRowHeightResizing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboGridRowHeightResizing.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboGridRowHeightResizing.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboGridRowHeightResizing.GapHeight = 0;
            this.cboGridRowHeightResizing.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboGridRowHeightResizing.Items.Add("AllRows");
            this.cboGridRowHeightResizing.Items.Add("IndividualRows");
            this.cboGridRowHeightResizing.ItemsDisplayMember = "";
            this.cboGridRowHeightResizing.ItemsValueMember = "";
            this.cboGridRowHeightResizing.Location = new System.Drawing.Point(426, 114);
            this.cboGridRowHeightResizing.Name = "cboGridRowHeightResizing";
            this.cboGridRowHeightResizing.Size = new System.Drawing.Size(143, 21);
            this.cboGridRowHeightResizing.TabIndex = 85;
            this.cboGridRowHeightResizing.Tag = null;
            this.cboGridRowHeightResizing.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboGridRowHeightResizing, "(default)");
            this.cboGridRowHeightResizing.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboGridRowHeightResizing.SelectedIndexChanged += new System.EventHandler(this.cboGridRowHeightResizing_SelectedIndexChanged);
            // 
            // cboGridFontSize
            // 
            this.cboGridFontSize.AllowSpinLoop = false;
            this.cboGridFontSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboGridFontSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboGridFontSize.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboGridFontSize.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboGridFontSize.GapHeight = 0;
            this.cboGridFontSize.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboGridFontSize.Items.Add("9");
            this.cboGridFontSize.Items.Add("9.5");
            this.cboGridFontSize.Items.Add("10");
            this.cboGridFontSize.Items.Add("10.5");
            this.cboGridFontSize.Items.Add("11");
            this.cboGridFontSize.Items.Add("11.5");
            this.cboGridFontSize.Items.Add("12");
            this.cboGridFontSize.Items.Add("12.5");
            this.cboGridFontSize.Items.Add("13");
            this.cboGridFontSize.Items.Add("13.5");
            this.cboGridFontSize.Items.Add("14");
            this.cboGridFontSize.Items.Add("14.5");
            this.cboGridFontSize.Items.Add("15");
            this.cboGridFontSize.Items.Add("15.5");
            this.cboGridFontSize.Items.Add("16");
            this.cboGridFontSize.ItemsDisplayMember = "";
            this.cboGridFontSize.ItemsValueMember = "";
            this.cboGridFontSize.Location = new System.Drawing.Point(426, 85);
            this.cboGridFontSize.Name = "cboGridFontSize";
            this.cboGridFontSize.Size = new System.Drawing.Size(51, 21);
            this.cboGridFontSize.TabIndex = 84;
            this.cboGridFontSize.Tag = null;
            this.cboGridFontSize.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboGridFontSize, "(default)");
            this.cboGridFontSize.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboGridFontSize.SelectedIndexChanged += new System.EventHandler(this.cboGridFontSize_SelectedIndexChanged);
            // 
            // cboGridZoom
            // 
            this.cboGridZoom.AllowSpinLoop = false;
            this.cboGridZoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboGridZoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboGridZoom.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboGridZoom.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboGridZoom.GapHeight = 0;
            this.cboGridZoom.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboGridZoom.Items.Add("0.8");
            this.cboGridZoom.Items.Add("0.9");
            this.cboGridZoom.Items.Add("1");
            this.cboGridZoom.Items.Add("1.1");
            this.cboGridZoom.Items.Add("1.2");
            this.cboGridZoom.ItemsDisplayMember = "";
            this.cboGridZoom.ItemsValueMember = "";
            this.cboGridZoom.Location = new System.Drawing.Point(1293, 27);
            this.cboGridZoom.Name = "cboGridZoom";
            this.cboGridZoom.Size = new System.Drawing.Size(45, 21);
            this.cboGridZoom.TabIndex = 83;
            this.cboGridZoom.Tag = null;
            this.cboGridZoom.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboGridZoom, "(default)");
            this.cboGridZoom.Visible = false;
            this.cboGridZoom.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboGridZoom.SelectedIndexChanged += new System.EventHandler(this.cboGridZoom_SelectedIndexChanged);
            // 
            // cboGridVisualStyle
            // 
            this.cboGridVisualStyle.AllowSpinLoop = false;
            this.cboGridVisualStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboGridVisualStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboGridVisualStyle.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboGridVisualStyle.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboGridVisualStyle.GapHeight = 0;
            this.cboGridVisualStyle.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboGridVisualStyle.Items.Add("Office 2007 Blue");
            this.cboGridVisualStyle.Items.Add("Office 2007 Silver");
            this.cboGridVisualStyle.Items.Add("Office 2007 Black");
            this.cboGridVisualStyle.Items.Add("Office 2010 Blue");
            this.cboGridVisualStyle.Items.Add("Office 2010 Silver");
            this.cboGridVisualStyle.Items.Add("Office 2010 Black");
            this.cboGridVisualStyle.ItemsDisplayMember = "";
            this.cboGridVisualStyle.ItemsValueMember = "";
            this.cboGridVisualStyle.Location = new System.Drawing.Point(426, 26);
            this.cboGridVisualStyle.Name = "cboGridVisualStyle";
            this.cboGridVisualStyle.Size = new System.Drawing.Size(143, 21);
            this.cboGridVisualStyle.TabIndex = 82;
            this.cboGridVisualStyle.Tag = null;
            this.cboGridVisualStyle.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboGridVisualStyle, "(default)");
            this.cboGridVisualStyle.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboGridVisualStyle.SelectedIndexChanged += new System.EventHandler(this.cboGridVisualStyle_SelectedIndexChanged);
            // 
            // cboResultCopyFieldSeparator
            // 
            this.cboResultCopyFieldSeparator.AllowSpinLoop = false;
            this.cboResultCopyFieldSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboResultCopyFieldSeparator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboResultCopyFieldSeparator.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboResultCopyFieldSeparator.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboResultCopyFieldSeparator.GapHeight = 0;
            this.cboResultCopyFieldSeparator.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboResultCopyFieldSeparator.Items.Add(",");
            this.cboResultCopyFieldSeparator.Items.Add(";");
            this.cboResultCopyFieldSeparator.Items.Add("|");
            this.cboResultCopyFieldSeparator.ItemsDisplayMember = "";
            this.cboResultCopyFieldSeparator.ItemsValueMember = "";
            this.cboResultCopyFieldSeparator.Location = new System.Drawing.Point(1209, 61);
            this.cboResultCopyFieldSeparator.Name = "cboResultCopyFieldSeparator";
            this.cboResultCopyFieldSeparator.Size = new System.Drawing.Size(48, 21);
            this.cboResultCopyFieldSeparator.TabIndex = 80;
            this.cboResultCopyFieldSeparator.Tag = null;
            this.cboResultCopyFieldSeparator.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboResultCopyFieldSeparator, "(default)");
            this.cboResultCopyFieldSeparator.Visible = false;
            this.cboResultCopyFieldSeparator.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // cboResultCopyQuotingWith
            // 
            this.cboResultCopyQuotingWith.AllowSpinLoop = false;
            this.cboResultCopyQuotingWith.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboResultCopyQuotingWith.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboResultCopyQuotingWith.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboResultCopyQuotingWith.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboResultCopyQuotingWith.GapHeight = 0;
            this.cboResultCopyQuotingWith.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboResultCopyQuotingWith.Items.Add("None");
            this.cboResultCopyQuotingWith.Items.Add("\"");
            this.cboResultCopyQuotingWith.Items.Add("\'");
            this.cboResultCopyQuotingWith.ItemsDisplayMember = "";
            this.cboResultCopyQuotingWith.ItemsValueMember = "";
            this.cboResultCopyQuotingWith.Location = new System.Drawing.Point(1208, 32);
            this.cboResultCopyQuotingWith.Name = "cboResultCopyQuotingWith";
            this.cboResultCopyQuotingWith.Size = new System.Drawing.Size(60, 21);
            this.cboResultCopyQuotingWith.TabIndex = 79;
            this.cboResultCopyQuotingWith.Tag = null;
            this.cboResultCopyQuotingWith.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboResultCopyQuotingWith, "(default)");
            this.cboResultCopyQuotingWith.Visible = false;
            this.cboResultCopyQuotingWith.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // cboMaxWidth
            // 
            this.cboMaxWidth.AllowSpinLoop = false;
            this.cboMaxWidth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboMaxWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboMaxWidth.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboMaxWidth.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboMaxWidth.GapHeight = 0;
            this.cboMaxWidth.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboMaxWidth.Items.Add("Unlimited");
            this.cboMaxWidth.Items.Add("500");
            this.cboMaxWidth.Items.Add("1000");
            this.cboMaxWidth.Items.Add("1500");
            this.cboMaxWidth.Items.Add("2000");
            this.cboMaxWidth.ItemsDisplayMember = "";
            this.cboMaxWidth.ItemsValueMember = "";
            this.cboMaxWidth.Location = new System.Drawing.Point(723, 127);
            this.cboMaxWidth.Name = "cboMaxWidth";
            this.cboMaxWidth.Size = new System.Drawing.Size(60, 21);
            this.cboMaxWidth.TabIndex = 78;
            this.cboMaxWidth.Tag = null;
            this.cboMaxWidth.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboMaxWidth, "(default)");
            this.cboMaxWidth.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboMaxWidth.SelectedIndexChanged += new System.EventHandler(this.cboMaxSize_SelectedIndexChanged);
            // 
            // chkResize
            // 
            this.chkResize.AutoSize = true;
            this.chkResize.BackColor = System.Drawing.Color.Transparent;
            this.chkResize.BorderColor = System.Drawing.Color.Transparent;
            this.chkResize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkResize.ForeColor = System.Drawing.Color.Black;
            this.chkResize.Location = new System.Drawing.Point(643, 101);
            this.chkResize.Name = "chkResize";
            this.chkResize.Padding = new System.Windows.Forms.Padding(1);
            this.chkResize.Size = new System.Drawing.Size(291, 22);
            this.chkResize.TabIndex = 76;
            this.chkResize.Text = "Resize Column Width according to Data Result";
            this.c1ThemeController1.SetTheme(this.chkResize, "(default)");
            this.chkResize.UseVisualStyleBackColor = true;
            this.chkResize.Value = null;
            this.chkResize.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkResize.CheckedChanged += new System.EventHandler(this.chkResize_CheckedChanged);
            // 
            // chkSort
            // 
            this.chkSort.AutoSize = true;
            this.chkSort.BackColor = System.Drawing.Color.Transparent;
            this.chkSort.BorderColor = System.Drawing.Color.Transparent;
            this.chkSort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkSort.ForeColor = System.Drawing.Color.Black;
            this.chkSort.Location = new System.Drawing.Point(1045, 101);
            this.chkSort.Name = "chkSort";
            this.chkSort.Padding = new System.Windows.Forms.Padding(1);
            this.chkSort.Size = new System.Drawing.Size(286, 22);
            this.chkSort.TabIndex = 75;
            this.chkSort.Text = "Sort Data Result according to Column Header";
            this.c1ThemeController1.SetTheme(this.chkSort, "(default)");
            this.chkSort.UseVisualStyleBackColor = true;
            this.chkSort.Value = null;
            this.chkSort.Visible = false;
            this.chkSort.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkSort.CheckedChanged += new System.EventHandler(this.chkSort_CheckedChanged);
            // 
            // chkShowFilterRow
            // 
            this.chkShowFilterRow.AutoSize = true;
            this.chkShowFilterRow.BackColor = System.Drawing.Color.Transparent;
            this.chkShowFilterRow.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowFilterRow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowFilterRow.ForeColor = System.Drawing.Color.Black;
            this.chkShowFilterRow.Location = new System.Drawing.Point(643, 74);
            this.chkShowFilterRow.Name = "chkShowFilterRow";
            this.chkShowFilterRow.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowFilterRow.Size = new System.Drawing.Size(118, 22);
            this.chkShowFilterRow.TabIndex = 74;
            this.chkShowFilterRow.Text = "Show Filter Row";
            this.c1ThemeController1.SetTheme(this.chkShowFilterRow, "(default)");
            this.chkShowFilterRow.UseVisualStyleBackColor = true;
            this.chkShowFilterRow.Value = null;
            this.chkShowFilterRow.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShowFilterRow.CheckedChanged += new System.EventHandler(this.chkShowFilterRow_CheckedChanged);
            // 
            // chkShowStreamlinedName
            // 
            this.chkShowStreamlinedName.AutoSize = true;
            this.chkShowStreamlinedName.BackColor = System.Drawing.Color.Transparent;
            this.chkShowStreamlinedName.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowStreamlinedName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowStreamlinedName.ForeColor = System.Drawing.Color.Black;
            this.chkShowStreamlinedName.Location = new System.Drawing.Point(663, 47);
            this.chkShowStreamlinedName.Name = "chkShowStreamlinedName";
            this.chkShowStreamlinedName.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowStreamlinedName.Size = new System.Drawing.Size(292, 22);
            this.chkShowStreamlinedName.TabIndex = 73;
            this.chkShowStreamlinedName.Text = "Show Streamlined Name (for PostgreSQL only)";
            this.c1ThemeController1.SetTheme(this.chkShowStreamlinedName, "(default)");
            this.chkShowStreamlinedName.UseVisualStyleBackColor = true;
            this.chkShowStreamlinedName.Value = null;
            this.chkShowStreamlinedName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShowStreamlinedName.CheckedChanged += new System.EventHandler(this.chkShowStreamlinedName_CheckedChanged);
            // 
            // chkShowColumnType
            // 
            this.chkShowColumnType.AutoSize = true;
            this.chkShowColumnType.BackColor = System.Drawing.Color.Transparent;
            this.chkShowColumnType.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowColumnType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowColumnType.ForeColor = System.Drawing.Color.Black;
            this.chkShowColumnType.Location = new System.Drawing.Point(643, 23);
            this.chkShowColumnType.Name = "chkShowColumnType";
            this.chkShowColumnType.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowColumnType.Size = new System.Drawing.Size(138, 22);
            this.chkShowColumnType.TabIndex = 72;
            this.chkShowColumnType.Text = "Show Column Type";
            this.c1ThemeController1.SetTheme(this.chkShowColumnType, "(default)");
            this.chkShowColumnType.UseVisualStyleBackColor = true;
            this.chkShowColumnType.Value = null;
            this.chkShowColumnType.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShowColumnType.CheckedChanged += new System.EventHandler(this.chkShowColumnDataType_CheckedChanged);
            // 
            // cboGridFontPicker
            // 
            this.cboGridFontPicker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.cboGridFontPicker.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboGridFontPicker.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboGridFontPicker.Location = new System.Drawing.Point(426, 56);
            this.cboGridFontPicker.Name = "cboGridFontPicker";
            this.cboGridFontPicker.Size = new System.Drawing.Size(143, 21);
            this.cboGridFontPicker.TabIndex = 57;
            this.cboGridFontPicker.Tag = null;
            this.c1ThemeController1.SetTheme(this.cboGridFontPicker, "(default)");
            this.cboGridFontPicker.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboGridFontPicker.TextChanged += new System.EventHandler(this.cboGridFontPicker_TextChanged);
            // 
            // lblMaxWidth
            // 
            this.lblMaxWidth.AutoSize = true;
            this.lblMaxWidth.Enabled = false;
            this.lblMaxWidth.Location = new System.Drawing.Point(660, 129);
            this.lblMaxWidth.Name = "lblMaxWidth";
            this.lblMaxWidth.Size = new System.Drawing.Size(73, 16);
            this.lblMaxWidth.TabIndex = 61;
            this.lblMaxWidth.Text = "Max Wdith:";
            this.lblMaxWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGridVisualStyle
            // 
            this.lblGridVisualStyle.Location = new System.Drawing.Point(328, 27);
            this.lblGridVisualStyle.Name = "lblGridVisualStyle";
            this.lblGridVisualStyle.Size = new System.Drawing.Size(94, 16);
            this.lblGridVisualStyle.TabIndex = 6;
            this.lblGridVisualStyle.Text = "Visual Style:";
            this.lblGridVisualStyle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGridFontSize
            // 
            this.lblGridFontSize.Location = new System.Drawing.Point(347, 86);
            this.lblGridFontSize.Name = "lblGridFontSize";
            this.lblGridFontSize.Size = new System.Drawing.Size(75, 16);
            this.lblGridFontSize.TabIndex = 49;
            this.lblGridFontSize.Text = "Font Size:";
            this.lblGridFontSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGridZoom
            // 
            this.lblGridZoom.Location = new System.Drawing.Point(1214, 28);
            this.lblGridZoom.Name = "lblGridZoom";
            this.lblGridZoom.Size = new System.Drawing.Size(75, 16);
            this.lblGridZoom.TabIndex = 12;
            this.lblGridZoom.Text = "Zoom:";
            this.lblGridZoom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblGridZoom.Visible = false;
            // 
            // lblResultCopyFieldSeparator
            // 
            this.lblResultCopyFieldSeparator.AutoSize = true;
            this.lblResultCopyFieldSeparator.Location = new System.Drawing.Point(1041, 62);
            this.lblResultCopyFieldSeparator.Name = "lblResultCopyFieldSeparator";
            this.lblResultCopyFieldSeparator.Size = new System.Drawing.Size(167, 16);
            this.lblResultCopyFieldSeparator.TabIndex = 1;
            this.lblResultCopyFieldSeparator.Text = "Result Copy Field Separator:";
            this.lblResultCopyFieldSeparator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblResultCopyFieldSeparator.Visible = false;
            // 
            // lblGridFontName
            // 
            this.lblGridFontName.Location = new System.Drawing.Point(331, 57);
            this.lblGridFontName.Name = "lblGridFontName";
            this.lblGridFontName.Size = new System.Drawing.Size(91, 16);
            this.lblGridFontName.TabIndex = 47;
            this.lblGridFontName.Text = "Font Name:";
            this.lblGridFontName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblResultCopyQuotingWith
            // 
            this.lblResultCopyQuotingWith.AutoSize = true;
            this.lblResultCopyQuotingWith.Location = new System.Drawing.Point(1041, 32);
            this.lblResultCopyQuotingWith.Name = "lblResultCopyQuotingWith";
            this.lblResultCopyQuotingWith.Size = new System.Drawing.Size(154, 16);
            this.lblResultCopyQuotingWith.TabIndex = 0;
            this.lblResultCopyQuotingWith.Text = "Result Copy Quoting with:";
            this.lblResultCopyQuotingWith.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblResultCopyQuotingWith.Visible = false;
            // 
            // grpNullValueStyle
            // 
            this.grpNullValueStyle.Controls.Add(this.cboNullShowAs);
            this.grpNullValueStyle.Controls.Add(this.pnlNullValueForeColor);
            this.grpNullValueStyle.Controls.Add(this.lblNullValueForeColor);
            this.grpNullValueStyle.Controls.Add(this.lblNullValueShowAs);
            this.grpNullValueStyle.Location = new System.Drawing.Point(370, 149);
            this.grpNullValueStyle.Name = "grpNullValueStyle";
            this.grpNullValueStyle.Size = new System.Drawing.Size(199, 84);
            this.grpNullValueStyle.TabIndex = 12;
            this.grpNullValueStyle.TabStop = false;
            this.grpNullValueStyle.Text = "NULL Value Style";
            // 
            // cboNullShowAs
            // 
            this.cboNullShowAs.AllowSpinLoop = false;
            this.cboNullShowAs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboNullShowAs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboNullShowAs.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboNullShowAs.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboNullShowAs.GapHeight = 0;
            this.cboNullShowAs.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboNullShowAs.Items.Add("<NULL>");
            this.cboNullShowAs.Items.Add("<null>");
            this.cboNullShowAs.Items.Add("{NULL}");
            this.cboNullShowAs.Items.Add("{null}");
            this.cboNullShowAs.Items.Add("(NULL)");
            this.cboNullShowAs.Items.Add("(null)");
            this.cboNullShowAs.Items.Add("None");
            this.cboNullShowAs.ItemsDisplayMember = "";
            this.cboNullShowAs.ItemsValueMember = "";
            this.cboNullShowAs.Location = new System.Drawing.Point(79, 23);
            this.cboNullShowAs.Name = "cboNullShowAs";
            this.cboNullShowAs.Size = new System.Drawing.Size(79, 21);
            this.cboNullShowAs.TabIndex = 81;
            this.cboNullShowAs.Tag = null;
            this.cboNullShowAs.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboNullShowAs, "(default)");
            this.cboNullShowAs.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboNullShowAs.SelectedIndexChanged += new System.EventHandler(this.cboNullShowAs_SelectedIndexChanged);
            // 
            // pnlNullValueForeColor
            // 
            this.pnlNullValueForeColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlNullValueForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNullValueForeColor.Location = new System.Drawing.Point(109, 52);
            this.pnlNullValueForeColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlNullValueForeColor.Name = "pnlNullValueForeColor";
            this.pnlNullValueForeColor.Size = new System.Drawing.Size(79, 21);
            this.pnlNullValueForeColor.TabIndex = 20;
            this.pnlNullValueForeColor.Click += new System.EventHandler(this.pnlSelectedGridClick);
            // 
            // lblNullValueForeColor
            // 
            this.lblNullValueForeColor.AutoSize = true;
            this.lblNullValueForeColor.Location = new System.Drawing.Point(21, 54);
            this.lblNullValueForeColor.Name = "lblNullValueForeColor";
            this.lblNullValueForeColor.Size = new System.Drawing.Size(70, 16);
            this.lblNullValueForeColor.TabIndex = 22;
            this.lblNullValueForeColor.Text = "Fore Color:";
            this.lblNullValueForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNullValueShowAs
            // 
            this.lblNullValueShowAs.AutoSize = true;
            this.lblNullValueShowAs.Location = new System.Drawing.Point(21, 25);
            this.lblNullValueShowAs.Name = "lblNullValueShowAs";
            this.lblNullValueShowAs.Size = new System.Drawing.Size(57, 16);
            this.lblNullValueShowAs.TabIndex = 19;
            this.lblNullValueShowAs.Text = "Show as:";
            this.lblNullValueShowAs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGridRowHeightResizing
            // 
            this.lblGridRowHeightResizing.Location = new System.Drawing.Point(272, 115);
            this.lblGridRowHeightResizing.Name = "lblGridRowHeightResizing";
            this.lblGridRowHeightResizing.Size = new System.Drawing.Size(150, 16);
            this.lblGridRowHeightResizing.TabIndex = 64;
            this.lblGridRowHeightResizing.Text = "Row (Height) Resizing:";
            this.lblGridRowHeightResizing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpDataGridColor
            // 
            this.grpDataGridColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpDataGridColor.BackColor = System.Drawing.Color.Transparent;
            this.grpDataGridColor.Controls.Add(this.lblGridHeadingForeColor);
            this.grpDataGridColor.Controls.Add(this.lblStarGridColor);
            this.grpDataGridColor.Controls.Add(this.pnlGridHeadingForeColor);
            this.grpDataGridColor.Controls.Add(this.pnlGridSelectedBackColor);
            this.grpDataGridColor.Controls.Add(this.lblGridSelectedBackColor);
            this.grpDataGridColor.Controls.Add(this.pnlGridSelectedForeColor);
            this.grpDataGridColor.Controls.Add(this.lblGridEvenRowForeColor);
            this.grpDataGridColor.Controls.Add(this.lblGridSelectedForeColor);
            this.grpDataGridColor.Controls.Add(this.lblGridHighlightForeColor);
            this.grpDataGridColor.Controls.Add(this.lblGridOddRowBackColor);
            this.grpDataGridColor.Controls.Add(this.pnlGridEvenRowBackColor);
            this.grpDataGridColor.Controls.Add(this.lblGridOddRowForeColor);
            this.grpDataGridColor.Controls.Add(this.pnlGridHighlightForeColor);
            this.grpDataGridColor.Controls.Add(this.lblGridEvenRowBackColor);
            this.grpDataGridColor.Controls.Add(this.lblGridHighlightBackColor);
            this.grpDataGridColor.Controls.Add(this.pnlGridHighlightBackColor);
            this.grpDataGridColor.Controls.Add(this.pnlGridOddRowForeColor);
            this.grpDataGridColor.Controls.Add(this.pnlGridOddRowBackColor);
            this.grpDataGridColor.Controls.Add(this.pnlGridEvenRowForeColor);
            this.grpDataGridColor.Location = new System.Drawing.Point(12, 256);
            this.grpDataGridColor.Name = "grpDataGridColor";
            this.grpDataGridColor.Size = new System.Drawing.Size(267, 419);
            this.grpDataGridColor.TabIndex = 12;
            this.grpDataGridColor.TabStop = false;
            this.grpDataGridColor.Text = "Data Grid Color";
            // 
            // lblGridHeadingForeColor
            // 
            this.lblGridHeadingForeColor.Location = new System.Drawing.Point(18, 24);
            this.lblGridHeadingForeColor.Name = "lblGridHeadingForeColor";
            this.lblGridHeadingForeColor.Size = new System.Drawing.Size(150, 16);
            this.lblGridHeadingForeColor.TabIndex = 27;
            this.lblGridHeadingForeColor.Text = "Heading Fore Color:";
            this.lblGridHeadingForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.c1ThemeController1.SetTheme(this.lblGridHeadingForeColor, "(default)");
            // 
            // lblStarGridColor
            // 
            this.lblStarGridColor.AutoSize = true;
            this.lblStarGridColor.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarGridColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarGridColor.Location = new System.Drawing.Point(100, 3);
            this.lblStarGridColor.Name = "lblStarGridColor";
            this.lblStarGridColor.Size = new System.Drawing.Size(14, 15);
            this.lblStarGridColor.TabIndex = 52;
            this.lblStarGridColor.Text = "*";
            this.lblStarGridColor.Visible = false;
            // 
            // pnlGridHeadingForeColor
            // 
            this.pnlGridHeadingForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGridHeadingForeColor.Location = new System.Drawing.Point(170, 22);
            this.pnlGridHeadingForeColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGridHeadingForeColor.Name = "pnlGridHeadingForeColor";
            this.pnlGridHeadingForeColor.Size = new System.Drawing.Size(74, 21);
            this.pnlGridHeadingForeColor.TabIndex = 26;
            this.c1ThemeController1.SetTheme(this.pnlGridHeadingForeColor, "(default)");
            this.pnlGridHeadingForeColor.Click += new System.EventHandler(this.pnlSelectedGridClick);
            // 
            // pnlGridSelectedBackColor
            // 
            this.pnlGridSelectedBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlGridSelectedBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGridSelectedBackColor.Location = new System.Drawing.Point(170, 262);
            this.pnlGridSelectedBackColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGridSelectedBackColor.Name = "pnlGridSelectedBackColor";
            this.pnlGridSelectedBackColor.Size = new System.Drawing.Size(74, 21);
            this.pnlGridSelectedBackColor.TabIndex = 31;
            this.pnlGridSelectedBackColor.Click += new System.EventHandler(this.pnlSelectedGridClick);
            // 
            // lblGridSelectedBackColor
            // 
            this.lblGridSelectedBackColor.Location = new System.Drawing.Point(18, 264);
            this.lblGridSelectedBackColor.Name = "lblGridSelectedBackColor";
            this.lblGridSelectedBackColor.Size = new System.Drawing.Size(150, 16);
            this.lblGridSelectedBackColor.TabIndex = 32;
            this.lblGridSelectedBackColor.Text = "Selected Back Color:";
            this.lblGridSelectedBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlGridSelectedForeColor
            // 
            this.pnlGridSelectedForeColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlGridSelectedForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGridSelectedForeColor.Location = new System.Drawing.Point(170, 232);
            this.pnlGridSelectedForeColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGridSelectedForeColor.Name = "pnlGridSelectedForeColor";
            this.pnlGridSelectedForeColor.Size = new System.Drawing.Size(74, 21);
            this.pnlGridSelectedForeColor.TabIndex = 30;
            this.pnlGridSelectedForeColor.Click += new System.EventHandler(this.pnlSelectedGridClick);
            // 
            // lblGridEvenRowForeColor
            // 
            this.lblGridEvenRowForeColor.Location = new System.Drawing.Point(18, 54);
            this.lblGridEvenRowForeColor.Name = "lblGridEvenRowForeColor";
            this.lblGridEvenRowForeColor.Size = new System.Drawing.Size(150, 16);
            this.lblGridEvenRowForeColor.TabIndex = 25;
            this.lblGridEvenRowForeColor.Text = "Even Row Fore Color:";
            this.lblGridEvenRowForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGridSelectedForeColor
            // 
            this.lblGridSelectedForeColor.Location = new System.Drawing.Point(18, 234);
            this.lblGridSelectedForeColor.Name = "lblGridSelectedForeColor";
            this.lblGridSelectedForeColor.Size = new System.Drawing.Size(150, 16);
            this.lblGridSelectedForeColor.TabIndex = 29;
            this.lblGridSelectedForeColor.Text = "Selected Fore Color:";
            this.lblGridSelectedForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGridHighlightForeColor
            // 
            this.lblGridHighlightForeColor.Location = new System.Drawing.Point(18, 174);
            this.lblGridHighlightForeColor.Name = "lblGridHighlightForeColor";
            this.lblGridHighlightForeColor.Size = new System.Drawing.Size(150, 16);
            this.lblGridHighlightForeColor.TabIndex = 2;
            this.lblGridHighlightForeColor.Text = "Highlight Fore Color:";
            this.lblGridHighlightForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGridOddRowBackColor
            // 
            this.lblGridOddRowBackColor.Location = new System.Drawing.Point(18, 144);
            this.lblGridOddRowBackColor.Name = "lblGridOddRowBackColor";
            this.lblGridOddRowBackColor.Size = new System.Drawing.Size(150, 16);
            this.lblGridOddRowBackColor.TabIndex = 28;
            this.lblGridOddRowBackColor.Text = "Odd Row Back Color:";
            this.lblGridOddRowBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlGridEvenRowBackColor
            // 
            this.pnlGridEvenRowBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlGridEvenRowBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGridEvenRowBackColor.Location = new System.Drawing.Point(170, 82);
            this.pnlGridEvenRowBackColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGridEvenRowBackColor.Name = "pnlGridEvenRowBackColor";
            this.pnlGridEvenRowBackColor.Size = new System.Drawing.Size(74, 21);
            this.pnlGridEvenRowBackColor.TabIndex = 19;
            this.pnlGridEvenRowBackColor.Click += new System.EventHandler(this.pnlSelectedGridClick);
            // 
            // lblGridOddRowForeColor
            // 
            this.lblGridOddRowForeColor.Location = new System.Drawing.Point(18, 114);
            this.lblGridOddRowForeColor.Name = "lblGridOddRowForeColor";
            this.lblGridOddRowForeColor.Size = new System.Drawing.Size(150, 16);
            this.lblGridOddRowForeColor.TabIndex = 27;
            this.lblGridOddRowForeColor.Text = "Odd Row Fore Color:";
            this.lblGridOddRowForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlGridHighlightForeColor
            // 
            this.pnlGridHighlightForeColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlGridHighlightForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGridHighlightForeColor.Location = new System.Drawing.Point(170, 172);
            this.pnlGridHighlightForeColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGridHighlightForeColor.Name = "pnlGridHighlightForeColor";
            this.pnlGridHighlightForeColor.Size = new System.Drawing.Size(74, 21);
            this.pnlGridHighlightForeColor.TabIndex = 3;
            this.pnlGridHighlightForeColor.Click += new System.EventHandler(this.pnlSelectedGridClick);
            // 
            // lblGridEvenRowBackColor
            // 
            this.lblGridEvenRowBackColor.Location = new System.Drawing.Point(18, 84);
            this.lblGridEvenRowBackColor.Name = "lblGridEvenRowBackColor";
            this.lblGridEvenRowBackColor.Size = new System.Drawing.Size(150, 16);
            this.lblGridEvenRowBackColor.TabIndex = 26;
            this.lblGridEvenRowBackColor.Text = "Even Row Back Color:";
            this.lblGridEvenRowBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGridHighlightBackColor
            // 
            this.lblGridHighlightBackColor.Location = new System.Drawing.Point(18, 204);
            this.lblGridHighlightBackColor.Name = "lblGridHighlightBackColor";
            this.lblGridHighlightBackColor.Size = new System.Drawing.Size(150, 16);
            this.lblGridHighlightBackColor.TabIndex = 4;
            this.lblGridHighlightBackColor.Text = "Highlight Back Color:";
            this.lblGridHighlightBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlGridHighlightBackColor
            // 
            this.pnlGridHighlightBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlGridHighlightBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGridHighlightBackColor.Location = new System.Drawing.Point(170, 202);
            this.pnlGridHighlightBackColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGridHighlightBackColor.Name = "pnlGridHighlightBackColor";
            this.pnlGridHighlightBackColor.Size = new System.Drawing.Size(74, 21);
            this.pnlGridHighlightBackColor.TabIndex = 4;
            this.pnlGridHighlightBackColor.Click += new System.EventHandler(this.pnlSelectedGridClick);
            // 
            // pnlGridOddRowForeColor
            // 
            this.pnlGridOddRowForeColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlGridOddRowForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGridOddRowForeColor.Location = new System.Drawing.Point(170, 112);
            this.pnlGridOddRowForeColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGridOddRowForeColor.Name = "pnlGridOddRowForeColor";
            this.pnlGridOddRowForeColor.Size = new System.Drawing.Size(74, 21);
            this.pnlGridOddRowForeColor.TabIndex = 24;
            this.pnlGridOddRowForeColor.Click += new System.EventHandler(this.pnlSelectedGridClick);
            // 
            // pnlGridOddRowBackColor
            // 
            this.pnlGridOddRowBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlGridOddRowBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGridOddRowBackColor.Location = new System.Drawing.Point(170, 142);
            this.pnlGridOddRowBackColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGridOddRowBackColor.Name = "pnlGridOddRowBackColor";
            this.pnlGridOddRowBackColor.Size = new System.Drawing.Size(74, 21);
            this.pnlGridOddRowBackColor.TabIndex = 22;
            this.pnlGridOddRowBackColor.Click += new System.EventHandler(this.pnlSelectedGridClick);
            // 
            // pnlGridEvenRowForeColor
            // 
            this.pnlGridEvenRowForeColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlGridEvenRowForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGridEvenRowForeColor.Location = new System.Drawing.Point(170, 52);
            this.pnlGridEvenRowForeColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlGridEvenRowForeColor.Name = "pnlGridEvenRowForeColor";
            this.pnlGridEvenRowForeColor.Size = new System.Drawing.Size(74, 21);
            this.pnlGridEvenRowForeColor.TabIndex = 23;
            this.pnlGridEvenRowForeColor.Click += new System.EventHandler(this.pnlSelectedGridClick);
            // 
            // grpPreviewGrid
            // 
            this.grpPreviewGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPreviewGrid.BackColor = System.Drawing.Color.Transparent;
            this.grpPreviewGrid.Controls.Add(this.cboFindGrid);
            this.grpPreviewGrid.Controls.Add(this.tsGrid);
            this.grpPreviewGrid.Controls.Add(this.c1GridVisualStyle);
            this.grpPreviewGrid.Location = new System.Drawing.Point(288, 256);
            this.grpPreviewGrid.Name = "grpPreviewGrid";
            this.grpPreviewGrid.Size = new System.Drawing.Size(910, 419);
            this.grpPreviewGrid.TabIndex = 2;
            this.grpPreviewGrid.TabStop = false;
            this.grpPreviewGrid.Text = "Preview";
            // 
            // cboFindGrid
            // 
            this.cboFindGrid.AllowSpinLoop = false;
            this.cboFindGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.cboFindGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboFindGrid.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cboFindGrid.GapHeight = 0;
            this.cboFindGrid.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboFindGrid.ItemsDisplayMember = "";
            this.cboFindGrid.ItemsValueMember = "";
            this.cboFindGrid.Location = new System.Drawing.Point(38, 24);
            this.cboFindGrid.Name = "cboFindGrid";
            this.cboFindGrid.Size = new System.Drawing.Size(111, 21);
            this.cboFindGrid.TabIndex = 79;
            this.cboFindGrid.Tag = null;
            this.cboFindGrid.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboFindGrid, "(default)");
            this.cboFindGrid.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboFindGrid.SelectedIndexChanged += new System.EventHandler(this.cboFindGrid_SelectedIndexChanged);
            this.cboFindGrid.TextChanged += new System.EventHandler(this.cboFindGrid_TextChanged);
            this.cboFindGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboFindGrid_KeyPress);
            this.cboFindGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboFindGrid_KeyUp);
            // 
            // tsGrid
            // 
            this.tsGrid.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsGrid.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFindGrid,
            this.cboFindGrid3,
            this.btnFindNextGrid,
            this.btnFindPreviousGrid,
            this.btnCountGrid,
            this.btnHighlightAllGrid,
            this.btnClearHighlightsGrid});
            this.tsGrid.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tsGrid.Location = new System.Drawing.Point(3, 19);
            this.tsGrid.Name = "tsGrid";
            this.tsGrid.Size = new System.Drawing.Size(904, 31);
            this.tsGrid.Stretch = true;
            this.tsGrid.TabIndex = 5;
            this.c1ThemeController1.SetTheme(this.tsGrid, "(default)");
            // 
            // lblFindGrid
            // 
            this.lblFindGrid.Name = "lblFindGrid";
            this.lblFindGrid.Size = new System.Drawing.Size(34, 28);
            this.lblFindGrid.Tag = "";
            this.lblFindGrid.Text = "Find:";
            // 
            // cboFindGrid3
            // 
            this.cboFindGrid3.AutoSize = false;
            this.cboFindGrid3.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cboFindGrid3.Font = new System.Drawing.Font("微軟正黑體", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboFindGrid3.Name = "cboFindGrid3";
            this.cboFindGrid3.Size = new System.Drawing.Size(110, 18);
            this.cboFindGrid3.DropDown += new System.EventHandler(this.cboFindGrid_DropDown);
            this.cboFindGrid3.SelectedIndexChanged += new System.EventHandler(this.cboFindGrid_SelectedIndexChanged);
            this.cboFindGrid3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboFindGrid_KeyPress);
            this.cboFindGrid3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboFindGrid_KeyUp);
            this.cboFindGrid3.TextChanged += new System.EventHandler(this.cboFindGrid_TextChanged);
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
            // c1GridVisualStyle
            // 
            this.c1GridVisualStyle.AllowUpdate = false;
            this.c1GridVisualStyle.AllowUpdateOnBlur = false;
            this.c1GridVisualStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1GridVisualStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.c1GridVisualStyle.CaptionHeight = 19;
            this.c1GridVisualStyle.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1GridVisualStyle.GroupByCaption = "将列头拖拽到这里以便按照该列进行分组";
            this.c1GridVisualStyle.Images.Add(((System.Drawing.Image)(resources.GetObject("c1GridVisualStyle.Images"))));
            this.c1GridVisualStyle.Location = new System.Drawing.Point(3, 50);
            this.c1GridVisualStyle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.c1GridVisualStyle.Name = "c1GridVisualStyle";
            this.c1GridVisualStyle.PreviewInfo.Caption = "PrintPreview窗口";
            this.c1GridVisualStyle.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1GridVisualStyle.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1GridVisualStyle.PreviewInfo.ZoomFactor = 75D;
            this.c1GridVisualStyle.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.c1GridVisualStyle.PrintInfo.MeasurementPrinterName = null;
            this.c1GridVisualStyle.RowHeight = 17;
            this.c1GridVisualStyle.Size = new System.Drawing.Size(904, 365);
            this.c1GridVisualStyle.TabIndex = 65;
            this.c1GridVisualStyle.UseCompatibleTextRendering = false;
            this.c1GridVisualStyle.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
            this.c1GridVisualStyle.OwnerDrawCell += new C1.Win.C1TrueDBGrid.OwnerDrawCellEventHandler(this.c1GridVisualStyle_OwnerDrawCell);
            this.c1GridVisualStyle.FetchCellStyle += new C1.Win.C1TrueDBGrid.FetchCellStyleEventHandler(this.c1GridVisualStyle_FetchCellStyle);
            this.c1GridVisualStyle.Enter += new System.EventHandler(this.c1GridVisualStyle_Enter);
            this.c1GridVisualStyle.Leave += new System.EventHandler(this.c1GridVisualStyle_Leave);
            this.c1GridVisualStyle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1GridVisualStyle_MouseDoubleClick);
            this.c1GridVisualStyle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1GridVisualStyle_MouseDown);
            this.c1GridVisualStyle.PropBag = resources.GetString("c1GridVisualStyle.PropBag");
            // 
            // grpOperatorKeywords
            // 
            this.grpOperatorKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOperatorKeywords.Controls.Add(this.grpFindOperatorKeywords);
            this.grpOperatorKeywords.Controls.Add(this.picOperatorKeywords);
            this.grpOperatorKeywords.Controls.Add(this.txtOperatorKeywords);
            this.grpOperatorKeywords.Location = new System.Drawing.Point(12, 3);
            this.grpOperatorKeywords.Name = "grpOperatorKeywords";
            this.grpOperatorKeywords.Size = new System.Drawing.Size(1186, 158);
            this.grpOperatorKeywords.TabIndex = 5;
            this.grpOperatorKeywords.TabStop = false;
            this.grpOperatorKeywords.Text = "Operator Keywords";
            // 
            // grpFindOperatorKeywords
            // 
            this.grpFindOperatorKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFindOperatorKeywords.BackColor = System.Drawing.Color.Transparent;
            this.grpFindOperatorKeywords.Controls.Add(this.toolStrip2);
            this.grpFindOperatorKeywords.Location = new System.Drawing.Point(972, 0);
            this.grpFindOperatorKeywords.Name = "grpFindOperatorKeywords";
            this.grpFindOperatorKeywords.Size = new System.Drawing.Size(212, 55);
            this.grpFindOperatorKeywords.TabIndex = 111;
            this.grpFindOperatorKeywords.TabStop = false;
            this.grpFindOperatorKeywords.Tag = "0";
            this.grpFindOperatorKeywords.Visible = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtFindOperatorKeywords,
            this.btnNextOperatorKeywords,
            this.btnPreviousOperatorKeywords,
            this.btnCloseFindOperatorKeywords});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(3, 19);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(206, 31);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 6;
            this.c1ThemeController1.SetTheme(this.toolStrip2, "(default)");
            // 
            // txtFindOperatorKeywords
            // 
            this.txtFindOperatorKeywords.Name = "txtFindOperatorKeywords";
            this.txtFindOperatorKeywords.Size = new System.Drawing.Size(100, 31);
            this.txtFindOperatorKeywords.Tag = "0";
            this.txtFindOperatorKeywords.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FindNextKeywords_KeyPress);
            this.txtFindOperatorKeywords.TextChanged += new System.EventHandler(this.FindKeywords_TextChanged);
            // 
            // btnNextOperatorKeywords
            // 
            this.btnNextOperatorKeywords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNextOperatorKeywords.Enabled = false;
            this.btnNextOperatorKeywords.Image = ((System.Drawing.Image)(resources.GetObject("btnNextOperatorKeywords.Image")));
            this.btnNextOperatorKeywords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNextOperatorKeywords.Name = "btnNextOperatorKeywords";
            this.btnNextOperatorKeywords.Size = new System.Drawing.Size(28, 28);
            this.btnNextOperatorKeywords.Tag = "0";
            this.btnNextOperatorKeywords.ToolTipText = "Find Next";
            this.btnNextOperatorKeywords.Click += new System.EventHandler(this.FindNextKeywords_Click);
            // 
            // btnPreviousOperatorKeywords
            // 
            this.btnPreviousOperatorKeywords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPreviousOperatorKeywords.Enabled = false;
            this.btnPreviousOperatorKeywords.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviousOperatorKeywords.Image")));
            this.btnPreviousOperatorKeywords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreviousOperatorKeywords.Name = "btnPreviousOperatorKeywords";
            this.btnPreviousOperatorKeywords.Size = new System.Drawing.Size(28, 28);
            this.btnPreviousOperatorKeywords.Tag = "0";
            this.btnPreviousOperatorKeywords.ToolTipText = "Find Previous";
            this.btnPreviousOperatorKeywords.Click += new System.EventHandler(this.FindPreviousKeywords_Click);
            // 
            // btnCloseFindOperatorKeywords
            // 
            this.btnCloseFindOperatorKeywords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseFindOperatorKeywords.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseFindOperatorKeywords.Image")));
            this.btnCloseFindOperatorKeywords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseFindOperatorKeywords.Name = "btnCloseFindOperatorKeywords";
            this.btnCloseFindOperatorKeywords.Size = new System.Drawing.Size(28, 28);
            this.btnCloseFindOperatorKeywords.Tag = "0";
            this.btnCloseFindOperatorKeywords.ToolTipText = "Close";
            this.btnCloseFindOperatorKeywords.Click += new System.EventHandler(this.HideOperatorKeywords);
            // 
            // picOperatorKeywords
            // 
            this.picOperatorKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picOperatorKeywords.Image = ((System.Drawing.Image)(resources.GetObject("picOperatorKeywords.Image")));
            this.picOperatorKeywords.Location = new System.Drawing.Point(1169, 7);
            this.picOperatorKeywords.Name = "picOperatorKeywords";
            this.picOperatorKeywords.Size = new System.Drawing.Size(16, 16);
            this.picOperatorKeywords.TabIndex = 14;
            this.picOperatorKeywords.TabStop = false;
            this.picOperatorKeywords.Tag = "0";
            this.picOperatorKeywords.Click += new System.EventHandler(this.ShowOperatorKeywords);
            // 
            // txtOperatorKeywords
            // 
            this.txtOperatorKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOperatorKeywords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOperatorKeywords.CaretLineBackColor = System.Drawing.Color.LightYellow;
            this.txtOperatorKeywords.CaretLineVisible = true;
            this.txtOperatorKeywords.EndAtLastLine = false;
            this.txtOperatorKeywords.HScrollBar = false;
            this.txtOperatorKeywords.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.txtOperatorKeywords.Location = new System.Drawing.Point(12, 23);
            this.txtOperatorKeywords.Name = "txtOperatorKeywords";
            this.txtOperatorKeywords.ScrollWidth = 400;
            this.txtOperatorKeywords.SelectionEolFilled = true;
            this.txtOperatorKeywords.Size = new System.Drawing.Size(1160, 122);
            this.txtOperatorKeywords.Styler = null;
            this.txtOperatorKeywords.TabIndex = 112;
            this.txtOperatorKeywords.Tag = "0";
            this.txtOperatorKeywords.ViewEol = true;
            this.txtOperatorKeywords.ViewWhitespace = ScintillaNET.WhitespaceMode.VisibleAlways;
            this.txtOperatorKeywords.WhitespaceSize = 3;
            this.txtOperatorKeywords.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.txtOperatorKeywords.WrapMode = ScintillaNET.WrapMode.Word;
            this.txtOperatorKeywords.Leave += new System.EventHandler(this.Keywords_LeaveCheck);
            // 
            // grpBuiltInFunctions
            // 
            this.grpBuiltInFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBuiltInFunctions.Controls.Add(this.grpFindBuiltInFunctions);
            this.grpBuiltInFunctions.Controls.Add(this.picBuiltInFunctions);
            this.grpBuiltInFunctions.Controls.Add(this.lbl1);
            this.grpBuiltInFunctions.Controls.Add(this.txtBuiltInFunctions);
            this.grpBuiltInFunctions.Location = new System.Drawing.Point(12, 3);
            this.grpBuiltInFunctions.Name = "grpBuiltInFunctions";
            this.grpBuiltInFunctions.Size = new System.Drawing.Size(1186, 163);
            this.grpBuiltInFunctions.TabIndex = 6;
            this.grpBuiltInFunctions.TabStop = false;
            this.grpBuiltInFunctions.Text = "Built-in Functions";
            // 
            // grpFindBuiltInFunctions
            // 
            this.grpFindBuiltInFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFindBuiltInFunctions.BackColor = System.Drawing.Color.Transparent;
            this.grpFindBuiltInFunctions.Controls.Add(this.toolStrip3);
            this.grpFindBuiltInFunctions.Location = new System.Drawing.Point(972, 0);
            this.grpFindBuiltInFunctions.Name = "grpFindBuiltInFunctions";
            this.grpFindBuiltInFunctions.Size = new System.Drawing.Size(212, 55);
            this.grpFindBuiltInFunctions.TabIndex = 113;
            this.grpFindBuiltInFunctions.TabStop = false;
            this.grpFindBuiltInFunctions.Tag = "1";
            this.grpFindBuiltInFunctions.Visible = false;
            // 
            // toolStrip3
            // 
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtFindBuiltInFunctions,
            this.btnNextBuiltInFunctions,
            this.btnPreviousBuiltInFunctions,
            this.btnCloseFindBuiltInFunctions});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip3.Location = new System.Drawing.Point(3, 19);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(206, 31);
            this.toolStrip3.Stretch = true;
            this.toolStrip3.TabIndex = 6;
            this.c1ThemeController1.SetTheme(this.toolStrip3, "(default)");
            // 
            // txtFindBuiltInFunctions
            // 
            this.txtFindBuiltInFunctions.Name = "txtFindBuiltInFunctions";
            this.txtFindBuiltInFunctions.Size = new System.Drawing.Size(100, 31);
            this.txtFindBuiltInFunctions.Tag = "1";
            this.txtFindBuiltInFunctions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FindNextKeywords_KeyPress);
            this.txtFindBuiltInFunctions.TextChanged += new System.EventHandler(this.FindKeywords_TextChanged);
            // 
            // btnNextBuiltInFunctions
            // 
            this.btnNextBuiltInFunctions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNextBuiltInFunctions.Enabled = false;
            this.btnNextBuiltInFunctions.Image = ((System.Drawing.Image)(resources.GetObject("btnNextBuiltInFunctions.Image")));
            this.btnNextBuiltInFunctions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNextBuiltInFunctions.Name = "btnNextBuiltInFunctions";
            this.btnNextBuiltInFunctions.Size = new System.Drawing.Size(28, 28);
            this.btnNextBuiltInFunctions.Tag = "1";
            this.btnNextBuiltInFunctions.ToolTipText = "Find Next";
            this.btnNextBuiltInFunctions.Click += new System.EventHandler(this.FindNextKeywords_Click);
            // 
            // btnPreviousBuiltInFunctions
            // 
            this.btnPreviousBuiltInFunctions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPreviousBuiltInFunctions.Enabled = false;
            this.btnPreviousBuiltInFunctions.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviousBuiltInFunctions.Image")));
            this.btnPreviousBuiltInFunctions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreviousBuiltInFunctions.Name = "btnPreviousBuiltInFunctions";
            this.btnPreviousBuiltInFunctions.Size = new System.Drawing.Size(28, 28);
            this.btnPreviousBuiltInFunctions.Tag = "1";
            this.btnPreviousBuiltInFunctions.ToolTipText = "Find Previous";
            this.btnPreviousBuiltInFunctions.Click += new System.EventHandler(this.FindPreviousKeywords_Click);
            // 
            // btnCloseFindBuiltInFunctions
            // 
            this.btnCloseFindBuiltInFunctions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseFindBuiltInFunctions.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseFindBuiltInFunctions.Image")));
            this.btnCloseFindBuiltInFunctions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseFindBuiltInFunctions.Name = "btnCloseFindBuiltInFunctions";
            this.btnCloseFindBuiltInFunctions.Size = new System.Drawing.Size(28, 28);
            this.btnCloseFindBuiltInFunctions.Tag = "1";
            this.btnCloseFindBuiltInFunctions.ToolTipText = "Close";
            this.btnCloseFindBuiltInFunctions.Click += new System.EventHandler(this.HideOperatorKeywords);
            // 
            // picBuiltInFunctions
            // 
            this.picBuiltInFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picBuiltInFunctions.Image = ((System.Drawing.Image)(resources.GetObject("picBuiltInFunctions.Image")));
            this.picBuiltInFunctions.Location = new System.Drawing.Point(1169, 7);
            this.picBuiltInFunctions.Name = "picBuiltInFunctions";
            this.picBuiltInFunctions.Size = new System.Drawing.Size(16, 16);
            this.picBuiltInFunctions.TabIndex = 15;
            this.picBuiltInFunctions.TabStop = false;
            this.picBuiltInFunctions.Tag = "1";
            this.picBuiltInFunctions.Click += new System.EventHandler(this.ShowOperatorKeywords);
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(6, 17);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(0, 16);
            this.lbl1.TabIndex = 13;
            this.lbl1.Visible = false;
            // 
            // txtBuiltInFunctions
            // 
            this.txtBuiltInFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuiltInFunctions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuiltInFunctions.CaretLineBackColor = System.Drawing.Color.LightYellow;
            this.txtBuiltInFunctions.CaretLineVisible = true;
            this.txtBuiltInFunctions.EndAtLastLine = false;
            this.txtBuiltInFunctions.HScrollBar = false;
            this.txtBuiltInFunctions.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.txtBuiltInFunctions.Location = new System.Drawing.Point(12, 23);
            this.txtBuiltInFunctions.Name = "txtBuiltInFunctions";
            this.txtBuiltInFunctions.ScrollWidth = 400;
            this.txtBuiltInFunctions.SelectionEolFilled = true;
            this.txtBuiltInFunctions.Size = new System.Drawing.Size(1160, 127);
            this.txtBuiltInFunctions.Styler = null;
            this.txtBuiltInFunctions.TabIndex = 114;
            this.txtBuiltInFunctions.Tag = "1";
            this.txtBuiltInFunctions.ViewEol = true;
            this.txtBuiltInFunctions.ViewWhitespace = ScintillaNET.WhitespaceMode.VisibleAlways;
            this.txtBuiltInFunctions.WhitespaceSize = 3;
            this.txtBuiltInFunctions.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.txtBuiltInFunctions.WrapMode = ScintillaNET.WrapMode.Word;
            this.txtBuiltInFunctions.Leave += new System.EventHandler(this.Keywords_LeaveCheck);
            // 
            // grpBuiltInKeywords
            // 
            this.grpBuiltInKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBuiltInKeywords.Controls.Add(this.grpFindBuiltInKeywords);
            this.grpBuiltInKeywords.Controls.Add(this.picBuiltInKeywords);
            this.grpBuiltInKeywords.Controls.Add(this.lbl2);
            this.grpBuiltInKeywords.Controls.Add(this.txtBuiltInKeywords);
            this.grpBuiltInKeywords.Location = new System.Drawing.Point(12, 3);
            this.grpBuiltInKeywords.Name = "grpBuiltInKeywords";
            this.grpBuiltInKeywords.Size = new System.Drawing.Size(1186, 160);
            this.grpBuiltInKeywords.TabIndex = 8;
            this.grpBuiltInKeywords.TabStop = false;
            this.grpBuiltInKeywords.Text = "Built-in Keywords";
            // 
            // grpFindBuiltInKeywords
            // 
            this.grpFindBuiltInKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFindBuiltInKeywords.BackColor = System.Drawing.Color.Transparent;
            this.grpFindBuiltInKeywords.Controls.Add(this.toolStrip4);
            this.grpFindBuiltInKeywords.Location = new System.Drawing.Point(974, 0);
            this.grpFindBuiltInKeywords.Name = "grpFindBuiltInKeywords";
            this.grpFindBuiltInKeywords.Size = new System.Drawing.Size(212, 55);
            this.grpFindBuiltInKeywords.TabIndex = 115;
            this.grpFindBuiltInKeywords.TabStop = false;
            this.grpFindBuiltInKeywords.Tag = "2";
            this.grpFindBuiltInKeywords.Visible = false;
            // 
            // toolStrip4
            // 
            this.toolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip4.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtFindBuiltInKeywords,
            this.btnNextBuiltInKeywords,
            this.btnPreviousBuiltInKeywords,
            this.btnCloseFindBuiltInKeywords});
            this.toolStrip4.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip4.Location = new System.Drawing.Point(3, 19);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(206, 31);
            this.toolStrip4.Stretch = true;
            this.toolStrip4.TabIndex = 6;
            this.c1ThemeController1.SetTheme(this.toolStrip4, "(default)");
            // 
            // txtFindBuiltInKeywords
            // 
            this.txtFindBuiltInKeywords.Name = "txtFindBuiltInKeywords";
            this.txtFindBuiltInKeywords.Size = new System.Drawing.Size(100, 31);
            this.txtFindBuiltInKeywords.Tag = "2";
            this.txtFindBuiltInKeywords.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FindNextKeywords_KeyPress);
            this.txtFindBuiltInKeywords.TextChanged += new System.EventHandler(this.FindKeywords_TextChanged);
            // 
            // btnNextBuiltInKeywords
            // 
            this.btnNextBuiltInKeywords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNextBuiltInKeywords.Enabled = false;
            this.btnNextBuiltInKeywords.Image = ((System.Drawing.Image)(resources.GetObject("btnNextBuiltInKeywords.Image")));
            this.btnNextBuiltInKeywords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNextBuiltInKeywords.Name = "btnNextBuiltInKeywords";
            this.btnNextBuiltInKeywords.Size = new System.Drawing.Size(28, 28);
            this.btnNextBuiltInKeywords.Tag = "2";
            this.btnNextBuiltInKeywords.ToolTipText = "Find Next";
            this.btnNextBuiltInKeywords.Click += new System.EventHandler(this.FindNextKeywords_Click);
            // 
            // btnPreviousBuiltInKeywords
            // 
            this.btnPreviousBuiltInKeywords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPreviousBuiltInKeywords.Enabled = false;
            this.btnPreviousBuiltInKeywords.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviousBuiltInKeywords.Image")));
            this.btnPreviousBuiltInKeywords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreviousBuiltInKeywords.Name = "btnPreviousBuiltInKeywords";
            this.btnPreviousBuiltInKeywords.Size = new System.Drawing.Size(28, 28);
            this.btnPreviousBuiltInKeywords.Tag = "2";
            this.btnPreviousBuiltInKeywords.ToolTipText = "Find Previous";
            this.btnPreviousBuiltInKeywords.Click += new System.EventHandler(this.FindPreviousKeywords_Click);
            // 
            // btnCloseFindBuiltInKeywords
            // 
            this.btnCloseFindBuiltInKeywords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseFindBuiltInKeywords.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseFindBuiltInKeywords.Image")));
            this.btnCloseFindBuiltInKeywords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseFindBuiltInKeywords.Name = "btnCloseFindBuiltInKeywords";
            this.btnCloseFindBuiltInKeywords.Size = new System.Drawing.Size(28, 28);
            this.btnCloseFindBuiltInKeywords.Tag = "2";
            this.btnCloseFindBuiltInKeywords.ToolTipText = "Close";
            this.btnCloseFindBuiltInKeywords.Click += new System.EventHandler(this.HideOperatorKeywords);
            // 
            // picBuiltInKeywords
            // 
            this.picBuiltInKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picBuiltInKeywords.Image = ((System.Drawing.Image)(resources.GetObject("picBuiltInKeywords.Image")));
            this.picBuiltInKeywords.Location = new System.Drawing.Point(1169, 7);
            this.picBuiltInKeywords.Name = "picBuiltInKeywords";
            this.picBuiltInKeywords.Size = new System.Drawing.Size(16, 16);
            this.picBuiltInKeywords.TabIndex = 16;
            this.picBuiltInKeywords.TabStop = false;
            this.picBuiltInKeywords.Tag = "2";
            this.picBuiltInKeywords.Click += new System.EventHandler(this.ShowOperatorKeywords);
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(6, 19);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(0, 16);
            this.lbl2.TabIndex = 14;
            this.lbl2.Visible = false;
            // 
            // txtBuiltInKeywords
            // 
            this.txtBuiltInKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuiltInKeywords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuiltInKeywords.CaretLineBackColor = System.Drawing.Color.LightYellow;
            this.txtBuiltInKeywords.CaretLineVisible = true;
            this.txtBuiltInKeywords.EndAtLastLine = false;
            this.txtBuiltInKeywords.HScrollBar = false;
            this.txtBuiltInKeywords.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.txtBuiltInKeywords.Location = new System.Drawing.Point(12, 23);
            this.txtBuiltInKeywords.Name = "txtBuiltInKeywords";
            this.txtBuiltInKeywords.ScrollWidth = 400;
            this.txtBuiltInKeywords.SelectionEolFilled = true;
            this.txtBuiltInKeywords.Size = new System.Drawing.Size(1160, 124);
            this.txtBuiltInKeywords.Styler = null;
            this.txtBuiltInKeywords.TabIndex = 116;
            this.txtBuiltInKeywords.Tag = "2";
            this.txtBuiltInKeywords.ViewEol = true;
            this.txtBuiltInKeywords.ViewWhitespace = ScintillaNET.WhitespaceMode.VisibleAlways;
            this.txtBuiltInKeywords.WhitespaceSize = 3;
            this.txtBuiltInKeywords.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.txtBuiltInKeywords.WrapMode = ScintillaNET.WrapMode.Word;
            this.txtBuiltInKeywords.Leave += new System.EventHandler(this.Keywords_LeaveCheck);
            // 
            // grpUserDefinedKeywords
            // 
            this.grpUserDefinedKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUserDefinedKeywords.Controls.Add(this.grpFindUserDefinedKeywords);
            this.grpUserDefinedKeywords.Controls.Add(this.picUserDefinedKeywords);
            this.grpUserDefinedKeywords.Controls.Add(this.txtUserDefinedKeywords);
            this.grpUserDefinedKeywords.Location = new System.Drawing.Point(12, 3);
            this.grpUserDefinedKeywords.Name = "grpUserDefinedKeywords";
            this.grpUserDefinedKeywords.Size = new System.Drawing.Size(1185, 164);
            this.grpUserDefinedKeywords.TabIndex = 7;
            this.grpUserDefinedKeywords.TabStop = false;
            this.grpUserDefinedKeywords.Text = "User-defined Keywords";
            // 
            // grpFindUserDefinedKeywords
            // 
            this.grpFindUserDefinedKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFindUserDefinedKeywords.BackColor = System.Drawing.Color.Transparent;
            this.grpFindUserDefinedKeywords.Controls.Add(this.toolStrip5);
            this.grpFindUserDefinedKeywords.Location = new System.Drawing.Point(973, 0);
            this.grpFindUserDefinedKeywords.Name = "grpFindUserDefinedKeywords";
            this.grpFindUserDefinedKeywords.Size = new System.Drawing.Size(212, 55);
            this.grpFindUserDefinedKeywords.TabIndex = 117;
            this.grpFindUserDefinedKeywords.TabStop = false;
            this.grpFindUserDefinedKeywords.Tag = "3";
            this.grpFindUserDefinedKeywords.Visible = false;
            // 
            // toolStrip5
            // 
            this.toolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip5.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtFindUserDefinedKeywords,
            this.btnNextUserDefinedKeywords,
            this.btnPreviousUserDefinedKeywords,
            this.btnCloseFindUserDefinedKeywords});
            this.toolStrip5.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip5.Location = new System.Drawing.Point(3, 19);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(206, 31);
            this.toolStrip5.Stretch = true;
            this.toolStrip5.TabIndex = 6;
            this.c1ThemeController1.SetTheme(this.toolStrip5, "(default)");
            // 
            // txtFindUserDefinedKeywords
            // 
            this.txtFindUserDefinedKeywords.Name = "txtFindUserDefinedKeywords";
            this.txtFindUserDefinedKeywords.Size = new System.Drawing.Size(100, 31);
            this.txtFindUserDefinedKeywords.Tag = "3";
            this.txtFindUserDefinedKeywords.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FindNextKeywords_KeyPress);
            this.txtFindUserDefinedKeywords.TextChanged += new System.EventHandler(this.FindKeywords_TextChanged);
            // 
            // btnNextUserDefinedKeywords
            // 
            this.btnNextUserDefinedKeywords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNextUserDefinedKeywords.Enabled = false;
            this.btnNextUserDefinedKeywords.Image = ((System.Drawing.Image)(resources.GetObject("btnNextUserDefinedKeywords.Image")));
            this.btnNextUserDefinedKeywords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNextUserDefinedKeywords.Name = "btnNextUserDefinedKeywords";
            this.btnNextUserDefinedKeywords.Size = new System.Drawing.Size(28, 28);
            this.btnNextUserDefinedKeywords.Tag = "3";
            this.btnNextUserDefinedKeywords.ToolTipText = "Find Next";
            this.btnNextUserDefinedKeywords.Click += new System.EventHandler(this.FindNextKeywords_Click);
            // 
            // btnPreviousUserDefinedKeywords
            // 
            this.btnPreviousUserDefinedKeywords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPreviousUserDefinedKeywords.Enabled = false;
            this.btnPreviousUserDefinedKeywords.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviousUserDefinedKeywords.Image")));
            this.btnPreviousUserDefinedKeywords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreviousUserDefinedKeywords.Name = "btnPreviousUserDefinedKeywords";
            this.btnPreviousUserDefinedKeywords.Size = new System.Drawing.Size(28, 28);
            this.btnPreviousUserDefinedKeywords.Tag = "3";
            this.btnPreviousUserDefinedKeywords.ToolTipText = "Find Previous";
            this.btnPreviousUserDefinedKeywords.Click += new System.EventHandler(this.FindPreviousKeywords_Click);
            // 
            // btnCloseFindUserDefinedKeywords
            // 
            this.btnCloseFindUserDefinedKeywords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseFindUserDefinedKeywords.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseFindUserDefinedKeywords.Image")));
            this.btnCloseFindUserDefinedKeywords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseFindUserDefinedKeywords.Name = "btnCloseFindUserDefinedKeywords";
            this.btnCloseFindUserDefinedKeywords.Size = new System.Drawing.Size(28, 28);
            this.btnCloseFindUserDefinedKeywords.Tag = "3";
            this.btnCloseFindUserDefinedKeywords.ToolTipText = "Close";
            this.btnCloseFindUserDefinedKeywords.Click += new System.EventHandler(this.HideOperatorKeywords);
            // 
            // picUserDefinedKeywords
            // 
            this.picUserDefinedKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picUserDefinedKeywords.Image = ((System.Drawing.Image)(resources.GetObject("picUserDefinedKeywords.Image")));
            this.picUserDefinedKeywords.Location = new System.Drawing.Point(1168, 7);
            this.picUserDefinedKeywords.Name = "picUserDefinedKeywords";
            this.picUserDefinedKeywords.Size = new System.Drawing.Size(16, 16);
            this.picUserDefinedKeywords.TabIndex = 15;
            this.picUserDefinedKeywords.TabStop = false;
            this.picUserDefinedKeywords.Tag = "3";
            this.picUserDefinedKeywords.Click += new System.EventHandler(this.ShowOperatorKeywords);
            // 
            // txtUserDefinedKeywords
            // 
            this.txtUserDefinedKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserDefinedKeywords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserDefinedKeywords.CaretLineBackColor = System.Drawing.Color.LightYellow;
            this.txtUserDefinedKeywords.CaretLineVisible = true;
            this.txtUserDefinedKeywords.EndAtLastLine = false;
            this.txtUserDefinedKeywords.HScrollBar = false;
            this.txtUserDefinedKeywords.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.txtUserDefinedKeywords.Location = new System.Drawing.Point(12, 23);
            this.txtUserDefinedKeywords.Name = "txtUserDefinedKeywords";
            this.txtUserDefinedKeywords.ScrollWidth = 400;
            this.txtUserDefinedKeywords.SelectionEolFilled = true;
            this.txtUserDefinedKeywords.Size = new System.Drawing.Size(1159, 128);
            this.txtUserDefinedKeywords.Styler = null;
            this.txtUserDefinedKeywords.TabIndex = 118;
            this.txtUserDefinedKeywords.Tag = "3";
            this.txtUserDefinedKeywords.ViewEol = true;
            this.txtUserDefinedKeywords.ViewWhitespace = ScintillaNET.WhitespaceMode.VisibleAlways;
            this.txtUserDefinedKeywords.WhitespaceSize = 3;
            this.txtUserDefinedKeywords.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.txtUserDefinedKeywords.WrapMode = ScintillaNET.WrapMode.Word;
            this.txtUserDefinedKeywords.Leave += new System.EventHandler(this.Keywords_LeaveCheck);
            // 
            // grpSQLToCode
            // 
            this.grpSQLToCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSQLToCode.BackColor = System.Drawing.Color.Transparent;
            this.grpSQLToCode.Controls.Add(this.txtVariableName);
            this.grpSQLToCode.Controls.Add(this.chkStripCode);
            this.grpSQLToCode.Controls.Add(this.grpSQLStatementCode);
            this.grpSQLToCode.Controls.Add(this.grpPreviewSQL);
            this.grpSQLToCode.Controls.Add(this.lblSQLVariableName);
            this.grpSQLToCode.Controls.Add(this.grpStyle);
            this.grpSQLToCode.Controls.Add(this.grpLanguage);
            this.grpSQLToCode.Location = new System.Drawing.Point(12, 9);
            this.grpSQLToCode.Name = "grpSQLToCode";
            this.grpSQLToCode.Size = new System.Drawing.Size(1186, 664);
            this.grpSQLToCode.TabIndex = 14;
            this.grpSQLToCode.TabStop = false;
            this.grpSQLToCode.Text = "SQL to Code";
            // 
            // txtVariableName
            // 
            this.txtVariableName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.txtVariableName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVariableName.Location = new System.Drawing.Point(402, 21);
            this.txtVariableName.Name = "txtVariableName";
            this.txtVariableName.Size = new System.Drawing.Size(119, 21);
            this.txtVariableName.TabIndex = 67;
            this.txtVariableName.Tag = null;
            this.txtVariableName.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.txtVariableName, "(default)");
            this.txtVariableName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtVariableName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVariableName_KeyPress);
            this.txtVariableName.Leave += new System.EventHandler(this.txtVariableName_Leave);
            // 
            // chkStripCode
            // 
            this.chkStripCode.AutoSize = true;
            this.chkStripCode.BackColor = System.Drawing.Color.Transparent;
            this.chkStripCode.BorderColor = System.Drawing.Color.Transparent;
            this.chkStripCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkStripCode.Checked = true;
            this.chkStripCode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStripCode.Enabled = false;
            this.chkStripCode.ForeColor = System.Drawing.Color.Black;
            this.chkStripCode.Location = new System.Drawing.Point(557, 22);
            this.chkStripCode.Name = "chkStripCode";
            this.chkStripCode.Padding = new System.Windows.Forms.Padding(1);
            this.chkStripCode.Size = new System.Drawing.Size(202, 22);
            this.chkStripCode.TabIndex = 66;
            this.chkStripCode.Text = "Strip Code copies to clipboard";
            this.c1ThemeController1.SetTheme(this.chkStripCode, "(default)");
            this.chkStripCode.UseVisualStyleBackColor = true;
            this.chkStripCode.Value = true;
            this.chkStripCode.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // grpSQLStatementCode
            // 
            this.grpSQLStatementCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSQLStatementCode.Controls.Add(this.editorSQLToCode);
            this.grpSQLStatementCode.Location = new System.Drawing.Point(299, 49);
            this.grpSQLStatementCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpSQLStatementCode.Name = "grpSQLStatementCode";
            this.grpSQLStatementCode.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpSQLStatementCode.Size = new System.Drawing.Size(875, 366);
            this.grpSQLStatementCode.TabIndex = 20;
            this.grpSQLStatementCode.TabStop = false;
            this.grpSQLStatementCode.Text = "SQL Statement";
            // 
            // editorSQLToCode
            // 
            this.editorSQLToCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorSQLToCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorSQLToCode.CaretLineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.editorSQLToCode.CaretLineVisible = true;
            this.editorSQLToCode.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.editorSQLToCode.Location = new System.Drawing.Point(10, 23);
            this.editorSQLToCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editorSQLToCode.Name = "editorSQLToCode";
            this.editorSQLToCode.Size = new System.Drawing.Size(855, 332);
            this.editorSQLToCode.Styler = null;
            this.editorSQLToCode.TabIndex = 41;
            this.editorSQLToCode.Tag = "";
            this.editorSQLToCode.WhitespaceSize = 3;
            this.editorSQLToCode.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.editorSQLToCode.Leave += new System.EventHandler(this.editorSQLStatement_Leave);
            // 
            // grpPreviewSQL
            // 
            this.grpPreviewSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPreviewSQL.Controls.Add(this.editorSQLToCodePreview);
            this.grpPreviewSQL.Location = new System.Drawing.Point(299, 286);
            this.grpPreviewSQL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpPreviewSQL.Name = "grpPreviewSQL";
            this.grpPreviewSQL.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpPreviewSQL.Size = new System.Drawing.Size(875, 366);
            this.grpPreviewSQL.TabIndex = 19;
            this.grpPreviewSQL.TabStop = false;
            this.grpPreviewSQL.Text = "Preview";
            // 
            // editorSQLToCodePreview
            // 
            this.editorSQLToCodePreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorSQLToCodePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorSQLToCodePreview.CaretLineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.editorSQLToCodePreview.CaretLineVisible = true;
            this.editorSQLToCodePreview.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.editorSQLToCodePreview.Location = new System.Drawing.Point(10, 23);
            this.editorSQLToCodePreview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editorSQLToCodePreview.Name = "editorSQLToCodePreview";
            this.editorSQLToCodePreview.Size = new System.Drawing.Size(855, 332);
            this.editorSQLToCodePreview.Styler = null;
            this.editorSQLToCodePreview.TabIndex = 41;
            this.editorSQLToCodePreview.Tag = "";
            this.editorSQLToCodePreview.WhitespaceSize = 3;
            this.editorSQLToCodePreview.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            // 
            // lblSQLVariableName
            // 
            this.lblSQLVariableName.AutoSize = true;
            this.lblSQLVariableName.Location = new System.Drawing.Point(305, 22);
            this.lblSQLVariableName.Name = "lblSQLVariableName";
            this.lblSQLVariableName.Size = new System.Drawing.Size(96, 16);
            this.lblSQLVariableName.TabIndex = 20;
            this.lblSQLVariableName.Text = "Variable Name:";
            this.lblSQLVariableName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpStyle
            // 
            this.grpStyle.Controls.Add(this.lblStyle3);
            this.grpStyle.Controls.Add(this.lblStyle2);
            this.grpStyle.Controls.Add(this.lblStyle1);
            this.grpStyle.Controls.Add(this.rdoStyle1);
            this.grpStyle.Controls.Add(this.rdoStyle3);
            this.grpStyle.Controls.Add(this.rdoStyle2);
            this.grpStyle.Location = new System.Drawing.Point(13, 150);
            this.grpStyle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpStyle.Name = "grpStyle";
            this.grpStyle.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpStyle.Size = new System.Drawing.Size(274, 184);
            this.grpStyle.TabIndex = 18;
            this.grpStyle.TabStop = false;
            this.grpStyle.Tag = "1";
            // 
            // lblStyle3
            // 
            this.lblStyle3.AutoSize = true;
            this.lblStyle3.Location = new System.Drawing.Point(31, 146);
            this.lblStyle3.Name = "lblStyle3";
            this.lblStyle3.Size = new System.Drawing.Size(0, 16);
            this.lblStyle3.TabIndex = 9;
            this.lblStyle3.Tag = "3";
            this.c1ThemeController1.SetTheme(this.lblStyle3, "(default)");
            this.lblStyle3.Click += new System.EventHandler(this.lblStyle_Click);
            // 
            // lblStyle2
            // 
            this.lblStyle2.AutoSize = true;
            this.lblStyle2.Location = new System.Drawing.Point(31, 96);
            this.lblStyle2.Name = "lblStyle2";
            this.lblStyle2.Size = new System.Drawing.Size(0, 16);
            this.lblStyle2.TabIndex = 8;
            this.lblStyle2.Tag = "2";
            this.c1ThemeController1.SetTheme(this.lblStyle2, "(default)");
            this.lblStyle2.Click += new System.EventHandler(this.lblStyle_Click);
            // 
            // lblStyle1
            // 
            this.lblStyle1.AutoSize = true;
            this.lblStyle1.Location = new System.Drawing.Point(31, 46);
            this.lblStyle1.Name = "lblStyle1";
            this.lblStyle1.Size = new System.Drawing.Size(0, 16);
            this.lblStyle1.TabIndex = 7;
            this.lblStyle1.Tag = "1";
            this.c1ThemeController1.SetTheme(this.lblStyle1, "(default)");
            this.lblStyle1.Click += new System.EventHandler(this.lblStyle_Click);
            // 
            // rdoStyle1
            // 
            this.rdoStyle1.AutoSize = true;
            this.rdoStyle1.Checked = true;
            this.rdoStyle1.Location = new System.Drawing.Point(15, 23);
            this.rdoStyle1.Name = "rdoStyle1";
            this.rdoStyle1.Size = new System.Drawing.Size(63, 20);
            this.rdoStyle1.TabIndex = 6;
            this.rdoStyle1.TabStop = true;
            this.rdoStyle1.Tag = "Style 1";
            this.rdoStyle1.Text = "Style 1";
            this.rdoStyle1.UseVisualStyleBackColor = true;
            this.rdoStyle1.CheckedChanged += new System.EventHandler(this.rdoStyle_CheckedChanged);
            // 
            // rdoStyle3
            // 
            this.rdoStyle3.AutoSize = true;
            this.rdoStyle3.Location = new System.Drawing.Point(15, 123);
            this.rdoStyle3.Name = "rdoStyle3";
            this.rdoStyle3.Size = new System.Drawing.Size(63, 20);
            this.rdoStyle3.TabIndex = 2;
            this.rdoStyle3.Tag = "Style 3";
            this.rdoStyle3.Text = "Style 3";
            this.rdoStyle3.UseVisualStyleBackColor = true;
            this.rdoStyle3.CheckedChanged += new System.EventHandler(this.rdoStyle_CheckedChanged);
            // 
            // rdoStyle2
            // 
            this.rdoStyle2.AutoSize = true;
            this.rdoStyle2.Location = new System.Drawing.Point(15, 73);
            this.rdoStyle2.Name = "rdoStyle2";
            this.rdoStyle2.Size = new System.Drawing.Size(63, 20);
            this.rdoStyle2.TabIndex = 1;
            this.rdoStyle2.Tag = "Style 2";
            this.rdoStyle2.Text = "Style 2";
            this.rdoStyle2.UseVisualStyleBackColor = true;
            this.rdoStyle2.CheckedChanged += new System.EventHandler(this.rdoStyle_CheckedChanged);
            // 
            // grpLanguage
            // 
            this.grpLanguage.Controls.Add(this.lstLanguage);
            this.grpLanguage.Location = new System.Drawing.Point(13, 21);
            this.grpLanguage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpLanguage.Name = "grpLanguage";
            this.grpLanguage.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpLanguage.Size = new System.Drawing.Size(98, 125);
            this.grpLanguage.TabIndex = 17;
            this.grpLanguage.TabStop = false;
            this.grpLanguage.Text = "Language";
            // 
            // lstLanguage
            // 
            this.lstLanguage.FormattingEnabled = true;
            this.lstLanguage.ItemHeight = 16;
            this.lstLanguage.Items.AddRange(new object[] {
            "C#",
            "VB.Net",
            "VB6/VBA",
            "Delphi6"});
            this.lstLanguage.Location = new System.Drawing.Point(15, 26);
            this.lstLanguage.Name = "lstLanguage";
            this.lstLanguage.Size = new System.Drawing.Size(67, 84);
            this.lstLanguage.TabIndex = 0;
            this.c1ThemeController1.SetTheme(this.lstLanguage, "(default)");
            this.lstLanguage.SelectedIndexChanged += new System.EventHandler(this.lstLanguage_SelectedIndexChanged);
            // 
            // grpSQLFormatter
            // 
            this.grpSQLFormatter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSQLFormatter.BackColor = System.Drawing.Color.Transparent;
            this.grpSQLFormatter.Controls.Add(this.grpPreviewFormatter);
            this.grpSQLFormatter.Controls.Add(this.grpFormattingOptions);
            this.grpSQLFormatter.Controls.Add(this.grpSQLStatementFormatter);
            this.grpSQLFormatter.Location = new System.Drawing.Point(12, 9);
            this.grpSQLFormatter.Name = "grpSQLFormatter";
            this.grpSQLFormatter.Size = new System.Drawing.Size(1186, 664);
            this.grpSQLFormatter.TabIndex = 15;
            this.grpSQLFormatter.TabStop = false;
            this.grpSQLFormatter.Text = "SQL Formatter";
            // 
            // grpPreviewFormatter
            // 
            this.grpPreviewFormatter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPreviewFormatter.Controls.Add(this.editorSQLFormatterPreview);
            this.grpPreviewFormatter.Location = new System.Drawing.Point(252, 270);
            this.grpPreviewFormatter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpPreviewFormatter.Name = "grpPreviewFormatter";
            this.grpPreviewFormatter.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpPreviewFormatter.Size = new System.Drawing.Size(923, 383);
            this.grpPreviewFormatter.TabIndex = 19;
            this.grpPreviewFormatter.TabStop = false;
            this.grpPreviewFormatter.Text = "Preview";
            // 
            // editorSQLFormatterPreview
            // 
            this.editorSQLFormatterPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorSQLFormatterPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorSQLFormatterPreview.CaretLineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.editorSQLFormatterPreview.CaretLineVisible = true;
            this.editorSQLFormatterPreview.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.editorSQLFormatterPreview.Location = new System.Drawing.Point(10, 23);
            this.editorSQLFormatterPreview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editorSQLFormatterPreview.Name = "editorSQLFormatterPreview";
            this.editorSQLFormatterPreview.Size = new System.Drawing.Size(903, 349);
            this.editorSQLFormatterPreview.Styler = null;
            this.editorSQLFormatterPreview.TabIndex = 41;
            this.editorSQLFormatterPreview.Tag = "";
            this.editorSQLFormatterPreview.WhitespaceSize = 3;
            this.editorSQLFormatterPreview.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            // 
            // grpFormattingOptions
            // 
            this.grpFormattingOptions.Controls.Add(this.txtMaxWidth);
            this.grpFormattingOptions.Controls.Add(this.chkConvertCaseForKeywords);
            this.grpFormattingOptions.Controls.Add(this.chkBreakJoinOnSections);
            this.grpFormattingOptions.Controls.Add(this.chkExpandInLists);
            this.grpFormattingOptions.Controls.Add(this.chkExpandBetweenConditions);
            this.grpFormattingOptions.Controls.Add(this.chkExpandCaseStatements);
            this.grpFormattingOptions.Controls.Add(this.chkExpandBooleanExpressions);
            this.grpFormattingOptions.Controls.Add(this.chkTrailingCommas);
            this.grpFormattingOptions.Controls.Add(this.chkExpandCommaLists);
            this.grpFormattingOptions.Controls.Add(this.rdoProperCase);
            this.grpFormattingOptions.Controls.Add(this.rdoLowerCase);
            this.grpFormattingOptions.Controls.Add(this.rdoUpperCase);
            this.grpFormattingOptions.Controls.Add(this.lblMaxWidth2);
            this.grpFormattingOptions.Location = new System.Drawing.Point(13, 21);
            this.grpFormattingOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpFormattingOptions.Name = "grpFormattingOptions";
            this.grpFormattingOptions.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpFormattingOptions.Size = new System.Drawing.Size(230, 306);
            this.grpFormattingOptions.TabIndex = 17;
            this.grpFormattingOptions.TabStop = false;
            this.grpFormattingOptions.Text = "Formatting Options";
            // 
            // txtMaxWidth
            // 
            this.txtMaxWidth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.txtMaxWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaxWidth.Location = new System.Drawing.Point(88, 24);
            this.txtMaxWidth.MaxLength = 4;
            this.txtMaxWidth.Name = "txtMaxWidth";
            this.txtMaxWidth.Size = new System.Drawing.Size(34, 21);
            this.txtMaxWidth.TabIndex = 75;
            this.txtMaxWidth.Tag = null;
            this.txtMaxWidth.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.txtMaxWidth, "(default)");
            this.txtMaxWidth.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtMaxWidth.TextChanged += new System.EventHandler(this.SQLFormat_TextChanged);
            this.txtMaxWidth.Enter += new System.EventHandler(this.txtMaxWidth_Enter);
            this.txtMaxWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaxWidth_KeyDown);
            this.txtMaxWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaxWidth_KeyPress);
            this.txtMaxWidth.Leave += new System.EventHandler(this.txtMaxWidth_Leave);
            // 
            // chkConvertCaseForKeywords
            // 
            this.chkConvertCaseForKeywords.AutoSize = true;
            this.chkConvertCaseForKeywords.BackColor = System.Drawing.Color.Transparent;
            this.chkConvertCaseForKeywords.BorderColor = System.Drawing.Color.Transparent;
            this.chkConvertCaseForKeywords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkConvertCaseForKeywords.ForeColor = System.Drawing.Color.Black;
            this.chkConvertCaseForKeywords.Location = new System.Drawing.Point(15, 226);
            this.chkConvertCaseForKeywords.Name = "chkConvertCaseForKeywords";
            this.chkConvertCaseForKeywords.Padding = new System.Windows.Forms.Padding(1);
            this.chkConvertCaseForKeywords.Size = new System.Drawing.Size(179, 22);
            this.chkConvertCaseForKeywords.TabIndex = 74;
            this.chkConvertCaseForKeywords.Text = "Convert Case for Keywords";
            this.c1ThemeController1.SetTheme(this.chkConvertCaseForKeywords, "(default)");
            this.chkConvertCaseForKeywords.UseVisualStyleBackColor = true;
            this.chkConvertCaseForKeywords.Value = null;
            this.chkConvertCaseForKeywords.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkConvertCaseForKeywords.CheckedChanged += new System.EventHandler(this.SQLFormatter_CheckedChanged);
            // 
            // chkBreakJoinOnSections
            // 
            this.chkBreakJoinOnSections.AutoSize = true;
            this.chkBreakJoinOnSections.BackColor = System.Drawing.Color.Transparent;
            this.chkBreakJoinOnSections.BorderColor = System.Drawing.Color.Transparent;
            this.chkBreakJoinOnSections.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkBreakJoinOnSections.ForeColor = System.Drawing.Color.Black;
            this.chkBreakJoinOnSections.Location = new System.Drawing.Point(15, 201);
            this.chkBreakJoinOnSections.Name = "chkBreakJoinOnSections";
            this.chkBreakJoinOnSections.Padding = new System.Windows.Forms.Padding(1);
            this.chkBreakJoinOnSections.Size = new System.Drawing.Size(159, 22);
            this.chkBreakJoinOnSections.TabIndex = 73;
            this.chkBreakJoinOnSections.Text = "Break Join ON Sections";
            this.c1ThemeController1.SetTheme(this.chkBreakJoinOnSections, "(default)");
            this.chkBreakJoinOnSections.UseVisualStyleBackColor = true;
            this.chkBreakJoinOnSections.Value = null;
            this.chkBreakJoinOnSections.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkBreakJoinOnSections.CheckedChanged += new System.EventHandler(this.SQLFormatter_CheckedChanged);
            // 
            // chkExpandInLists
            // 
            this.chkExpandInLists.AutoSize = true;
            this.chkExpandInLists.BackColor = System.Drawing.Color.Transparent;
            this.chkExpandInLists.BorderColor = System.Drawing.Color.Transparent;
            this.chkExpandInLists.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkExpandInLists.ForeColor = System.Drawing.Color.Black;
            this.chkExpandInLists.Location = new System.Drawing.Point(15, 176);
            this.chkExpandInLists.Name = "chkExpandInLists";
            this.chkExpandInLists.Padding = new System.Windows.Forms.Padding(1);
            this.chkExpandInLists.Size = new System.Drawing.Size(114, 22);
            this.chkExpandInLists.TabIndex = 72;
            this.chkExpandInLists.Text = "Expand IN Lists";
            this.c1ThemeController1.SetTheme(this.chkExpandInLists, "(default)");
            this.chkExpandInLists.UseVisualStyleBackColor = true;
            this.chkExpandInLists.Value = null;
            this.chkExpandInLists.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkExpandInLists.CheckedChanged += new System.EventHandler(this.SQLFormatter_CheckedChanged);
            // 
            // chkExpandBetweenConditions
            // 
            this.chkExpandBetweenConditions.AutoSize = true;
            this.chkExpandBetweenConditions.BackColor = System.Drawing.Color.Transparent;
            this.chkExpandBetweenConditions.BorderColor = System.Drawing.Color.Transparent;
            this.chkExpandBetweenConditions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkExpandBetweenConditions.ForeColor = System.Drawing.Color.Black;
            this.chkExpandBetweenConditions.Location = new System.Drawing.Point(15, 151);
            this.chkExpandBetweenConditions.Name = "chkExpandBetweenConditions";
            this.chkExpandBetweenConditions.Padding = new System.Windows.Forms.Padding(1);
            this.chkExpandBetweenConditions.Size = new System.Drawing.Size(196, 22);
            this.chkExpandBetweenConditions.TabIndex = 71;
            this.chkExpandBetweenConditions.Text = "Expand BETWEEN Conditions";
            this.c1ThemeController1.SetTheme(this.chkExpandBetweenConditions, "(default)");
            this.chkExpandBetweenConditions.UseVisualStyleBackColor = true;
            this.chkExpandBetweenConditions.Value = null;
            this.chkExpandBetweenConditions.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkExpandBetweenConditions.CheckedChanged += new System.EventHandler(this.SQLFormatter_CheckedChanged);
            // 
            // chkExpandCaseStatements
            // 
            this.chkExpandCaseStatements.AutoSize = true;
            this.chkExpandCaseStatements.BackColor = System.Drawing.Color.Transparent;
            this.chkExpandCaseStatements.BorderColor = System.Drawing.Color.Transparent;
            this.chkExpandCaseStatements.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkExpandCaseStatements.ForeColor = System.Drawing.Color.Black;
            this.chkExpandCaseStatements.Location = new System.Drawing.Point(15, 126);
            this.chkExpandCaseStatements.Name = "chkExpandCaseStatements";
            this.chkExpandCaseStatements.Padding = new System.Windows.Forms.Padding(1);
            this.chkExpandCaseStatements.Size = new System.Drawing.Size(171, 22);
            this.chkExpandCaseStatements.TabIndex = 70;
            this.chkExpandCaseStatements.Text = "Expand CASE Statements";
            this.c1ThemeController1.SetTheme(this.chkExpandCaseStatements, "(default)");
            this.chkExpandCaseStatements.UseVisualStyleBackColor = true;
            this.chkExpandCaseStatements.Value = null;
            this.chkExpandCaseStatements.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkExpandCaseStatements.CheckedChanged += new System.EventHandler(this.SQLFormatter_CheckedChanged);
            // 
            // chkExpandBooleanExpressions
            // 
            this.chkExpandBooleanExpressions.AutoSize = true;
            this.chkExpandBooleanExpressions.BackColor = System.Drawing.Color.Transparent;
            this.chkExpandBooleanExpressions.BorderColor = System.Drawing.Color.Transparent;
            this.chkExpandBooleanExpressions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkExpandBooleanExpressions.ForeColor = System.Drawing.Color.Black;
            this.chkExpandBooleanExpressions.Location = new System.Drawing.Point(15, 101);
            this.chkExpandBooleanExpressions.Name = "chkExpandBooleanExpressions";
            this.chkExpandBooleanExpressions.Padding = new System.Windows.Forms.Padding(1);
            this.chkExpandBooleanExpressions.Size = new System.Drawing.Size(190, 22);
            this.chkExpandBooleanExpressions.TabIndex = 69;
            this.chkExpandBooleanExpressions.Text = "Expand Boolean Expressions";
            this.c1ThemeController1.SetTheme(this.chkExpandBooleanExpressions, "(default)");
            this.chkExpandBooleanExpressions.UseVisualStyleBackColor = true;
            this.chkExpandBooleanExpressions.Value = null;
            this.chkExpandBooleanExpressions.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkExpandBooleanExpressions.CheckedChanged += new System.EventHandler(this.SQLFormatter_CheckedChanged);
            // 
            // chkTrailingCommas
            // 
            this.chkTrailingCommas.AutoSize = true;
            this.chkTrailingCommas.BackColor = System.Drawing.Color.Transparent;
            this.chkTrailingCommas.BorderColor = System.Drawing.Color.Transparent;
            this.chkTrailingCommas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkTrailingCommas.ForeColor = System.Drawing.Color.Black;
            this.chkTrailingCommas.Location = new System.Drawing.Point(34, 76);
            this.chkTrailingCommas.Name = "chkTrailingCommas";
            this.chkTrailingCommas.Padding = new System.Windows.Forms.Padding(1);
            this.chkTrailingCommas.Size = new System.Drawing.Size(124, 22);
            this.chkTrailingCommas.TabIndex = 68;
            this.chkTrailingCommas.Text = "Trailing Commas";
            this.c1ThemeController1.SetTheme(this.chkTrailingCommas, "(default)");
            this.chkTrailingCommas.UseVisualStyleBackColor = true;
            this.chkTrailingCommas.Value = null;
            this.chkTrailingCommas.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkTrailingCommas.CheckedChanged += new System.EventHandler(this.SQLFormatter_CheckedChanged);
            // 
            // chkExpandCommaLists
            // 
            this.chkExpandCommaLists.AutoSize = true;
            this.chkExpandCommaLists.BackColor = System.Drawing.Color.Transparent;
            this.chkExpandCommaLists.BorderColor = System.Drawing.Color.Transparent;
            this.chkExpandCommaLists.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkExpandCommaLists.ForeColor = System.Drawing.Color.Black;
            this.chkExpandCommaLists.Location = new System.Drawing.Point(15, 51);
            this.chkExpandCommaLists.Name = "chkExpandCommaLists";
            this.chkExpandCommaLists.Padding = new System.Windows.Forms.Padding(1);
            this.chkExpandCommaLists.Size = new System.Drawing.Size(146, 22);
            this.chkExpandCommaLists.TabIndex = 67;
            this.chkExpandCommaLists.Text = "Expand Comma Lists";
            this.c1ThemeController1.SetTheme(this.chkExpandCommaLists, "(default)");
            this.chkExpandCommaLists.UseVisualStyleBackColor = true;
            this.chkExpandCommaLists.Value = null;
            this.chkExpandCommaLists.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkExpandCommaLists.CheckedChanged += new System.EventHandler(this.SQLFormatter_CheckedChanged);
            // 
            // rdoProperCase
            // 
            this.rdoProperCase.AutoSize = true;
            this.rdoProperCase.Location = new System.Drawing.Point(132, 277);
            this.rdoProperCase.Name = "rdoProperCase";
            this.rdoProperCase.Size = new System.Drawing.Size(94, 20);
            this.rdoProperCase.TabIndex = 36;
            this.rdoProperCase.TabStop = true;
            this.rdoProperCase.Text = "Proper Case";
            this.rdoProperCase.UseVisualStyleBackColor = true;
            this.rdoProperCase.Visible = false;
            this.rdoProperCase.CheckedChanged += new System.EventHandler(this.SQLFormatter2_CheckedChanged);
            // 
            // rdoLowerCase
            // 
            this.rdoLowerCase.AutoSize = true;
            this.rdoLowerCase.Location = new System.Drawing.Point(34, 277);
            this.rdoLowerCase.Name = "rdoLowerCase";
            this.rdoLowerCase.Size = new System.Drawing.Size(87, 20);
            this.rdoLowerCase.TabIndex = 35;
            this.rdoLowerCase.TabStop = true;
            this.rdoLowerCase.Text = "lower Case";
            this.rdoLowerCase.UseVisualStyleBackColor = true;
            this.rdoLowerCase.CheckedChanged += new System.EventHandler(this.SQLFormatter2_CheckedChanged);
            // 
            // rdoUpperCase
            // 
            this.rdoUpperCase.AutoSize = true;
            this.rdoUpperCase.Location = new System.Drawing.Point(34, 252);
            this.rdoUpperCase.Name = "rdoUpperCase";
            this.rdoUpperCase.Size = new System.Drawing.Size(94, 20);
            this.rdoUpperCase.TabIndex = 34;
            this.rdoUpperCase.TabStop = true;
            this.rdoUpperCase.Text = "UPPER Case";
            this.rdoUpperCase.UseVisualStyleBackColor = true;
            this.rdoUpperCase.CheckedChanged += new System.EventHandler(this.SQLFormatter2_CheckedChanged);
            // 
            // lblMaxWidth2
            // 
            this.lblMaxWidth2.AutoSize = true;
            this.lblMaxWidth2.Location = new System.Drawing.Point(12, 26);
            this.lblMaxWidth2.Name = "lblMaxWidth2";
            this.lblMaxWidth2.Size = new System.Drawing.Size(73, 16);
            this.lblMaxWidth2.TabIndex = 20;
            this.lblMaxWidth2.Text = "Max Width:";
            this.lblMaxWidth2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpSQLStatementFormatter
            // 
            this.grpSQLStatementFormatter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSQLStatementFormatter.Controls.Add(this.editorSQLFormatter);
            this.grpSQLStatementFormatter.Location = new System.Drawing.Point(252, 21);
            this.grpSQLStatementFormatter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpSQLStatementFormatter.Name = "grpSQLStatementFormatter";
            this.grpSQLStatementFormatter.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpSQLStatementFormatter.Size = new System.Drawing.Size(923, 377);
            this.grpSQLStatementFormatter.TabIndex = 20;
            this.grpSQLStatementFormatter.TabStop = false;
            this.grpSQLStatementFormatter.Text = "SQL Statement";
            // 
            // editorSQLFormatter
            // 
            this.editorSQLFormatter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorSQLFormatter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editorSQLFormatter.CaretLineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.editorSQLFormatter.CaretLineVisible = true;
            this.editorSQLFormatter.IndentationGuides = ScintillaNET.IndentView.LookBoth;
            this.editorSQLFormatter.Location = new System.Drawing.Point(10, 23);
            this.editorSQLFormatter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editorSQLFormatter.Name = "editorSQLFormatter";
            this.editorSQLFormatter.Size = new System.Drawing.Size(903, 343);
            this.editorSQLFormatter.Styler = null;
            this.editorSQLFormatter.TabIndex = 41;
            this.editorSQLFormatter.Tag = "";
            this.editorSQLFormatter.WhitespaceSize = 3;
            this.editorSQLFormatter.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
            this.editorSQLFormatter.TextChanged += new System.EventHandler(this.SQLFormat_TextChanged);
            // 
            // grpGlobal
            // 
            this.grpGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGlobal.BackColor = System.Drawing.Color.Transparent;
            this.grpGlobal.Controls.Add(this.grpMaxEntries);
            this.grpGlobal.Controls.Add(this.grpGeneral);
            this.grpGlobal.Controls.Add(this.grpMainFormWindowsState);
            this.grpGlobal.Controls.Add(this.grpMainFormTabVisualStyle);
            this.grpGlobal.Controls.Add(this.grpOptionsTab);
            this.grpGlobal.Controls.Add(this.grpCheckForUpdate);
            this.grpGlobal.Location = new System.Drawing.Point(12, 9);
            this.grpGlobal.Name = "grpGlobal";
            this.grpGlobal.Size = new System.Drawing.Size(1186, 664);
            this.grpGlobal.TabIndex = 13;
            this.grpGlobal.TabStop = false;
            this.grpGlobal.Text = "Global Setting (for all your database connection)";
            // 
            // grpMaxEntries
            // 
            this.grpMaxEntries.Controls.Add(this.txtMyFavorite);
            this.grpMaxEntries.Controls.Add(this.txtRecentFiles);
            this.grpMaxEntries.Controls.Add(this.lblMyFavorite);
            this.grpMaxEntries.Controls.Add(this.lblRecentFiles);
            this.grpMaxEntries.Location = new System.Drawing.Point(13, 223);
            this.grpMaxEntries.Name = "grpMaxEntries";
            this.grpMaxEntries.Size = new System.Drawing.Size(368, 87);
            this.grpMaxEntries.TabIndex = 41;
            this.grpMaxEntries.TabStop = false;
            this.grpMaxEntries.Text = "Max. number of entries";
            // 
            // txtMyFavorite
            // 
            this.txtMyFavorite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.txtMyFavorite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMyFavorite.Location = new System.Drawing.Point(131, 53);
            this.txtMyFavorite.MaxLength = 2;
            this.txtMyFavorite.Name = "txtMyFavorite";
            this.txtMyFavorite.Size = new System.Drawing.Size(30, 21);
            this.txtMyFavorite.TabIndex = 6;
            this.txtMyFavorite.Tag = null;
            this.txtMyFavorite.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMyFavorite.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.txtMyFavorite, "(default)");
            this.txtMyFavorite.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtMyFavorite.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtMyFavorite_MouseClick);
            this.txtMyFavorite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMyFavorite_KeyDown);
            this.txtMyFavorite.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMyFavorite_KeyPress);
            this.txtMyFavorite.Leave += new System.EventHandler(this.txtMyFavorite_Leave);
            // 
            // txtRecentFiles
            // 
            this.txtRecentFiles.AutoSize = false;
            this.txtRecentFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.txtRecentFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRecentFiles.Location = new System.Drawing.Point(132, 25);
            this.txtRecentFiles.MaxLength = 2;
            this.txtRecentFiles.Name = "txtRecentFiles";
            this.txtRecentFiles.Size = new System.Drawing.Size(30, 21);
            this.txtRecentFiles.TabIndex = 4;
            this.txtRecentFiles.Tag = null;
            this.txtRecentFiles.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRecentFiles.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.txtRecentFiles, "(default)");
            this.txtRecentFiles.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.txtRecentFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtRecentFiles_MouseClick);
            this.txtRecentFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRecentFiles_KeyDown);
            this.txtRecentFiles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRecentFiles_KeyPress);
            this.txtRecentFiles.Leave += new System.EventHandler(this.txtRecentFiles_Leave);
            // 
            // lblMyFavorite
            // 
            this.lblMyFavorite.AutoSize = true;
            this.lblMyFavorite.Location = new System.Drawing.Point(17, 55);
            this.lblMyFavorite.Name = "lblMyFavorite";
            this.lblMyFavorite.Size = new System.Drawing.Size(77, 16);
            this.lblMyFavorite.TabIndex = 1;
            this.lblMyFavorite.Text = "My Favorite:";
            // 
            // lblRecentFiles
            // 
            this.lblRecentFiles.AutoSize = true;
            this.lblRecentFiles.Location = new System.Drawing.Point(17, 27);
            this.lblRecentFiles.Name = "lblRecentFiles";
            this.lblRecentFiles.Size = new System.Drawing.Size(75, 16);
            this.lblRecentFiles.TabIndex = 0;
            this.lblRecentFiles.Text = "Recent files:";
            // 
            // grpGeneral
            // 
            this.grpGeneral.Controls.Add(this.chkHideClock);
            this.grpGeneral.Controls.Add(this.lblStarShowVersion);
            this.grpGeneral.Controls.Add(this.chkShowVersion);
            this.grpGeneral.Controls.Add(this.cboLocalization);
            this.grpGeneral.Controls.Add(this.cboDateFormat);
            this.grpGeneral.Controls.Add(this.lblDateFormat);
            this.grpGeneral.Controls.Add(this.lblLocalization);
            this.grpGeneral.Location = new System.Drawing.Point(13, 21);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(368, 133);
            this.grpGeneral.TabIndex = 41;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // chkHideClock
            // 
            this.chkHideClock.AutoSize = true;
            this.chkHideClock.BorderColor = System.Drawing.Color.Transparent;
            this.chkHideClock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkHideClock.ForeColor = System.Drawing.Color.Black;
            this.chkHideClock.Location = new System.Drawing.Point(19, 103);
            this.chkHideClock.Name = "chkHideClock";
            this.chkHideClock.Padding = new System.Windows.Forms.Padding(1);
            this.chkHideClock.Size = new System.Drawing.Size(325, 22);
            this.chkHideClock.TabIndex = 81;
            this.chkHideClock.Text = "Disable and hide the clock in the bottom right corner";
            this.c1ThemeController1.SetTheme(this.chkHideClock, "(default)");
            this.chkHideClock.UseVisualStyleBackColor = true;
            this.chkHideClock.Value = null;
            this.chkHideClock.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblStarShowVersion
            // 
            this.lblStarShowVersion.AutoSize = true;
            this.lblStarShowVersion.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarShowVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarShowVersion.Location = new System.Drawing.Point(283, 79);
            this.lblStarShowVersion.Name = "lblStarShowVersion";
            this.lblStarShowVersion.Size = new System.Drawing.Size(14, 15);
            this.lblStarShowVersion.TabIndex = 80;
            this.lblStarShowVersion.Text = "*";
            // 
            // chkShowVersion
            // 
            this.chkShowVersion.AutoSize = true;
            this.chkShowVersion.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowVersion.ForeColor = System.Drawing.Color.Black;
            this.chkShowVersion.Location = new System.Drawing.Point(19, 76);
            this.chkShowVersion.Name = "chkShowVersion";
            this.chkShowVersion.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowVersion.Size = new System.Drawing.Size(261, 22);
            this.chkShowVersion.TabIndex = 79;
            this.chkShowVersion.Text = "Display the version number in the title bar";
            this.c1ThemeController1.SetTheme(this.chkShowVersion, "(default)");
            this.chkShowVersion.UseVisualStyleBackColor = true;
            this.chkShowVersion.Value = null;
            this.chkShowVersion.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
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
            this.cboLocalization.Location = new System.Drawing.Point(96, 20);
            this.cboLocalization.Name = "cboLocalization";
            this.cboLocalization.Size = new System.Drawing.Size(215, 21);
            this.cboLocalization.TabIndex = 78;
            this.cboLocalization.Tag = null;
            this.cboLocalization.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboLocalization, "(default)");
            this.cboLocalization.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboLocalization.SelectedIndexChanged += new System.EventHandler(this.cboLocalization_SelectedIndexChanged);
            // 
            // cboDateFormat
            // 
            this.cboDateFormat.AllowSpinLoop = false;
            this.cboDateFormat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboDateFormat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboDateFormat.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboDateFormat.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboDateFormat.GapHeight = 0;
            this.cboDateFormat.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboDateFormat.Items.Add("yyyy/MM/dd");
            this.cboDateFormat.Items.Add("yyyy-MM-dd");
            this.cboDateFormat.Items.Add("MM/dd/yyyy");
            this.cboDateFormat.Items.Add("MM-dd-yyyy");
            this.cboDateFormat.Items.Add("dd/MM/yyyy");
            this.cboDateFormat.Items.Add("dd-MM-yyyy");
            this.cboDateFormat.ItemsDisplayMember = "";
            this.cboDateFormat.ItemsValueMember = "";
            this.cboDateFormat.Location = new System.Drawing.Point(102, 46);
            this.cboDateFormat.Name = "cboDateFormat";
            this.cboDateFormat.Size = new System.Drawing.Size(106, 21);
            this.cboDateFormat.TabIndex = 77;
            this.cboDateFormat.Tag = null;
            this.cboDateFormat.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboDateFormat, "(default)");
            this.cboDateFormat.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.cboDateFormat.SelectedIndexChanged += new System.EventHandler(this.cboDateFormat_SelectedIndexChanged);
            // 
            // lblDateFormat
            // 
            this.lblDateFormat.AutoSize = true;
            this.lblDateFormat.Location = new System.Drawing.Point(17, 49);
            this.lblDateFormat.Name = "lblDateFormat";
            this.lblDateFormat.Size = new System.Drawing.Size(81, 16);
            this.lblDateFormat.TabIndex = 58;
            this.lblDateFormat.Text = "Date Format:";
            this.lblDateFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLocalization
            // 
            this.lblLocalization.AutoSize = true;
            this.lblLocalization.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblLocalization.Location = new System.Drawing.Point(17, 22);
            this.lblLocalization.Name = "lblLocalization";
            this.lblLocalization.Size = new System.Drawing.Size(79, 16);
            this.lblLocalization.TabIndex = 12;
            this.lblLocalization.Text = "Localization:";
            this.lblLocalization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpMainFormWindowsState
            // 
            this.grpMainFormWindowsState.Controls.Add(this.rdoNormal);
            this.grpMainFormWindowsState.Controls.Add(this.rdoMaximized);
            this.grpMainFormWindowsState.Location = new System.Drawing.Point(13, 162);
            this.grpMainFormWindowsState.Name = "grpMainFormWindowsState";
            this.grpMainFormWindowsState.Size = new System.Drawing.Size(368, 54);
            this.grpMainFormWindowsState.TabIndex = 40;
            this.grpMainFormWindowsState.TabStop = false;
            this.grpMainFormWindowsState.Text = "Main Form Windows State (on startup)";
            // 
            // rdoNormal
            // 
            this.rdoNormal.AutoSize = true;
            this.rdoNormal.Location = new System.Drawing.Point(174, 24);
            this.rdoNormal.Name = "rdoNormal";
            this.rdoNormal.Size = new System.Drawing.Size(69, 20);
            this.rdoNormal.TabIndex = 2;
            this.rdoNormal.Text = "Normal";
            this.rdoNormal.UseVisualStyleBackColor = true;
            // 
            // rdoMaximized
            // 
            this.rdoMaximized.AutoSize = true;
            this.rdoMaximized.Checked = true;
            this.rdoMaximized.Location = new System.Drawing.Point(20, 24);
            this.rdoMaximized.Name = "rdoMaximized";
            this.rdoMaximized.Size = new System.Drawing.Size(89, 20);
            this.rdoMaximized.TabIndex = 1;
            this.rdoMaximized.TabStop = true;
            this.rdoMaximized.Text = "Maximized";
            this.rdoMaximized.UseVisualStyleBackColor = true;
            // 
            // grpMainFormTabVisualStyle
            // 
            this.grpMainFormTabVisualStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMainFormTabVisualStyle.Controls.Add(this.lblStarMainFormTab);
            this.grpMainFormTabVisualStyle.Controls.Add(this.chkMultiLine);
            this.grpMainFormTabVisualStyle.Controls.Add(this.chkHoverSelect);
            this.grpMainFormTabVisualStyle.Controls.Add(this.chkShowArrows);
            this.grpMainFormTabVisualStyle.Controls.Add(this.chkShrinkPages);
            this.grpMainFormTabVisualStyle.Controls.Add(this.chkTabBold);
            this.grpMainFormTabVisualStyle.Controls.Add(this.tabExample);
            this.grpMainFormTabVisualStyle.Controls.Add(this.grpAppearance);
            this.grpMainFormTabVisualStyle.Controls.Add(this.grpMainFormTabStyle);
            this.grpMainFormTabVisualStyle.Location = new System.Drawing.Point(13, 317);
            this.grpMainFormTabVisualStyle.Name = "grpMainFormTabVisualStyle";
            this.grpMainFormTabVisualStyle.Size = new System.Drawing.Size(1159, 180);
            this.grpMainFormTabVisualStyle.TabIndex = 39;
            this.grpMainFormTabVisualStyle.TabStop = false;
            this.grpMainFormTabVisualStyle.Text = "Main Form Tab Visual Style";
            // 
            // lblStarMainFormTab
            // 
            this.lblStarMainFormTab.AutoSize = true;
            this.lblStarMainFormTab.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarMainFormTab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarMainFormTab.Location = new System.Drawing.Point(189, 3);
            this.lblStarMainFormTab.Name = "lblStarMainFormTab";
            this.lblStarMainFormTab.Size = new System.Drawing.Size(14, 15);
            this.lblStarMainFormTab.TabIndex = 70;
            this.lblStarMainFormTab.Text = "*";
            // 
            // chkMultiLine
            // 
            this.chkMultiLine.AutoSize = true;
            this.chkMultiLine.BackColor = System.Drawing.Color.Transparent;
            this.chkMultiLine.BorderColor = System.Drawing.Color.Transparent;
            this.chkMultiLine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkMultiLine.ForeColor = System.Drawing.Color.Black;
            this.chkMultiLine.Location = new System.Drawing.Point(650, 58);
            this.chkMultiLine.Name = "chkMultiLine";
            this.chkMultiLine.Padding = new System.Windows.Forms.Padding(1);
            this.chkMultiLine.Size = new System.Drawing.Size(81, 22);
            this.chkMultiLine.TabIndex = 69;
            this.chkMultiLine.Text = "MultiLine";
            this.c1ThemeController1.SetTheme(this.chkMultiLine, "(default)");
            this.chkMultiLine.UseVisualStyleBackColor = true;
            this.chkMultiLine.Value = null;
            this.chkMultiLine.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkMultiLine.CheckedChanged += new System.EventHandler(this.chkTabVisualStyle_CheckedChanged);
            // 
            // chkHoverSelect
            // 
            this.chkHoverSelect.AutoSize = true;
            this.chkHoverSelect.BackColor = System.Drawing.Color.Transparent;
            this.chkHoverSelect.BorderColor = System.Drawing.Color.Transparent;
            this.chkHoverSelect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkHoverSelect.ForeColor = System.Drawing.Color.Black;
            this.chkHoverSelect.Location = new System.Drawing.Point(650, 31);
            this.chkHoverSelect.Name = "chkHoverSelect";
            this.chkHoverSelect.Padding = new System.Windows.Forms.Padding(1);
            this.chkHoverSelect.Size = new System.Drawing.Size(100, 22);
            this.chkHoverSelect.TabIndex = 68;
            this.chkHoverSelect.Text = "Hover Select";
            this.c1ThemeController1.SetTheme(this.chkHoverSelect, "(default)");
            this.chkHoverSelect.UseVisualStyleBackColor = true;
            this.chkHoverSelect.Value = null;
            this.chkHoverSelect.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkHoverSelect.CheckedChanged += new System.EventHandler(this.chkTabVisualStyle_CheckedChanged);
            // 
            // chkShowArrows
            // 
            this.chkShowArrows.AutoSize = true;
            this.chkShowArrows.BackColor = System.Drawing.Color.Transparent;
            this.chkShowArrows.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowArrows.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowArrows.ForeColor = System.Drawing.Color.Black;
            this.chkShowArrows.Location = new System.Drawing.Point(483, 58);
            this.chkShowArrows.Name = "chkShowArrows";
            this.chkShowArrows.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowArrows.Size = new System.Drawing.Size(101, 22);
            this.chkShowArrows.TabIndex = 67;
            this.chkShowArrows.Text = "Show Arrows";
            this.c1ThemeController1.SetTheme(this.chkShowArrows, "(default)");
            this.chkShowArrows.UseVisualStyleBackColor = true;
            this.chkShowArrows.Value = null;
            this.chkShowArrows.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShowArrows.CheckedChanged += new System.EventHandler(this.chkTabVisualStyle_CheckedChanged);
            // 
            // chkShrinkPages
            // 
            this.chkShrinkPages.AutoSize = true;
            this.chkShrinkPages.BackColor = System.Drawing.Color.Transparent;
            this.chkShrinkPages.BorderColor = System.Drawing.Color.Transparent;
            this.chkShrinkPages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShrinkPages.ForeColor = System.Drawing.Color.Black;
            this.chkShrinkPages.Location = new System.Drawing.Point(483, 31);
            this.chkShrinkPages.Name = "chkShrinkPages";
            this.chkShrinkPages.Padding = new System.Windows.Forms.Padding(1);
            this.chkShrinkPages.Size = new System.Drawing.Size(100, 22);
            this.chkShrinkPages.TabIndex = 66;
            this.chkShrinkPages.Text = "Shrink Pages";
            this.c1ThemeController1.SetTheme(this.chkShrinkPages, "(default)");
            this.chkShrinkPages.UseVisualStyleBackColor = true;
            this.chkShrinkPages.Value = null;
            this.chkShrinkPages.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.chkShrinkPages.CheckedChanged += new System.EventHandler(this.chkTabVisualStyle_CheckedChanged);
            // 
            // chkTabBold
            // 
            this.chkTabBold.AutoSize = true;
            this.chkTabBold.Location = new System.Drawing.Point(940, 70);
            this.chkTabBold.Name = "chkTabBold";
            this.chkTabBold.Size = new System.Drawing.Size(179, 20);
            this.chkTabBold.TabIndex = 11;
            this.chkTabBold.Text = "Active Tab is shown in bold";
            this.chkTabBold.UseVisualStyleBackColor = true;
            this.chkTabBold.Visible = false;
            this.chkTabBold.CheckedChanged += new System.EventHandler(this.chkTabVisualStyle_CheckedChanged);
            // 
            // tabExample
            // 
            this.tabExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabExample.BackColor = System.Drawing.Color.Transparent;
            this.tabExample.BoldSelectedPage = true;
            this.tabExample.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabExample.ForeColor = System.Drawing.Color.Empty;
            this.tabExample.IDEPixelArea = false;
            this.tabExample.Location = new System.Drawing.Point(13, 94);
            this.tabExample.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabExample.Name = "tabExample";
            this.tabExample.PositionTop = true;
            this.tabExample.SelectedIndex = 0;
            this.tabExample.SelectedTab = this.tabPage1;
            this.tabExample.ShowClose = true;
            this.tabExample.Size = new System.Drawing.Size(1134, 63);
            this.tabExample.TabIndex = 10;
            this.tabExample.TabPages.AddRange(new Crownwood.Magic.Controls.TabPage[] {
            this.tabPage1,
            this.tabPage2,
            this.tabPage3,
            this.tabPage4,
            this.tabPage5,
            this.tabPage6,
            this.tabPage7,
            this.tabPage8,
            this.tabPage9,
            this.tabPage10,
            this.tabPage11,
            this.tabPage12,
            this.tabPage13,
            this.tabPage14,
            this.tabPage15,
            this.tabPage16,
            this.tabPage17,
            this.tabPage18,
            this.tabPage19,
            this.tabPage20});
            this.tabExample.TextColor = System.Drawing.Color.Empty;
            this.tabExample.TextInactiveColor = System.Drawing.Color.Silver;
            this.c1ThemeController1.SetTheme(this.tabExample, "(default)");
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(0, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1134, 36);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(0, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Selected = false;
            this.tabPage2.Size = new System.Drawing.Size(1134, 36);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(0, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Selected = false;
            this.tabPage3.Size = new System.Drawing.Size(1134, 36);
            this.tabPage3.TabIndex = 5;
            this.tabPage3.Visible = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(0, 27);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Selected = false;
            this.tabPage4.Size = new System.Drawing.Size(1134, 36);
            this.tabPage4.TabIndex = 6;
            this.tabPage4.Visible = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(0, 27);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Selected = false;
            this.tabPage5.Size = new System.Drawing.Size(1134, 36);
            this.tabPage5.TabIndex = 7;
            this.tabPage5.Visible = false;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(0, 27);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Selected = false;
            this.tabPage6.Size = new System.Drawing.Size(1134, 36);
            this.tabPage6.TabIndex = 8;
            this.tabPage6.Visible = false;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(0, 27);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Selected = false;
            this.tabPage7.Size = new System.Drawing.Size(1134, 36);
            this.tabPage7.TabIndex = 9;
            this.tabPage7.Visible = false;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(0, 27);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Selected = false;
            this.tabPage8.Size = new System.Drawing.Size(1134, 36);
            this.tabPage8.TabIndex = 10;
            this.tabPage8.Visible = false;
            // 
            // tabPage9
            // 
            this.tabPage9.Location = new System.Drawing.Point(0, 27);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Selected = false;
            this.tabPage9.Size = new System.Drawing.Size(1134, 36);
            this.tabPage9.TabIndex = 11;
            this.tabPage9.Visible = false;
            // 
            // tabPage10
            // 
            this.tabPage10.Location = new System.Drawing.Point(0, 27);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Selected = false;
            this.tabPage10.Size = new System.Drawing.Size(1134, 36);
            this.tabPage10.TabIndex = 12;
            this.tabPage10.Visible = false;
            // 
            // tabPage11
            // 
            this.tabPage11.Location = new System.Drawing.Point(0, 27);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Selected = false;
            this.tabPage11.Size = new System.Drawing.Size(1134, 36);
            this.tabPage11.TabIndex = 13;
            // 
            // tabPage12
            // 
            this.tabPage12.Location = new System.Drawing.Point(0, 27);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Selected = false;
            this.tabPage12.Size = new System.Drawing.Size(1134, 36);
            this.tabPage12.TabIndex = 14;
            // 
            // tabPage13
            // 
            this.tabPage13.Location = new System.Drawing.Point(0, 27);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Selected = false;
            this.tabPage13.Size = new System.Drawing.Size(1134, 36);
            this.tabPage13.TabIndex = 15;
            // 
            // tabPage14
            // 
            this.tabPage14.Location = new System.Drawing.Point(0, 27);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Selected = false;
            this.tabPage14.Size = new System.Drawing.Size(1134, 36);
            this.tabPage14.TabIndex = 16;
            // 
            // tabPage15
            // 
            this.tabPage15.Location = new System.Drawing.Point(0, 27);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Selected = false;
            this.tabPage15.Size = new System.Drawing.Size(1134, 36);
            this.tabPage15.TabIndex = 17;
            // 
            // tabPage16
            // 
            this.tabPage16.Location = new System.Drawing.Point(0, 27);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Selected = false;
            this.tabPage16.Size = new System.Drawing.Size(1134, 36);
            this.tabPage16.TabIndex = 18;
            // 
            // tabPage17
            // 
            this.tabPage17.Location = new System.Drawing.Point(0, 27);
            this.tabPage17.Name = "tabPage17";
            this.tabPage17.Selected = false;
            this.tabPage17.Size = new System.Drawing.Size(1134, 36);
            this.tabPage17.TabIndex = 19;
            // 
            // tabPage18
            // 
            this.tabPage18.Location = new System.Drawing.Point(0, 27);
            this.tabPage18.Name = "tabPage18";
            this.tabPage18.Selected = false;
            this.tabPage18.Size = new System.Drawing.Size(1134, 36);
            this.tabPage18.TabIndex = 20;
            // 
            // tabPage19
            // 
            this.tabPage19.Location = new System.Drawing.Point(0, 27);
            this.tabPage19.Name = "tabPage19";
            this.tabPage19.Selected = false;
            this.tabPage19.Size = new System.Drawing.Size(1134, 36);
            this.tabPage19.TabIndex = 21;
            // 
            // tabPage20
            // 
            this.tabPage20.Location = new System.Drawing.Point(0, 27);
            this.tabPage20.Name = "tabPage20";
            this.tabPage20.Selected = false;
            this.tabPage20.Size = new System.Drawing.Size(1134, 36);
            this.tabPage20.TabIndex = 22;
            // 
            // grpAppearance
            // 
            this.grpAppearance.Controls.Add(this.rdoMultiBox);
            this.grpAppearance.Controls.Add(this.rdoMultiForm);
            this.grpAppearance.Controls.Add(this.rdoMultiDocument);
            this.grpAppearance.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpAppearance.Location = new System.Drawing.Point(194, 22);
            this.grpAppearance.Name = "grpAppearance";
            this.grpAppearance.Size = new System.Drawing.Size(255, 58);
            this.grpAppearance.TabIndex = 2;
            this.grpAppearance.TabStop = false;
            this.grpAppearance.Text = "Appearance";
            // 
            // rdoMultiBox
            // 
            this.rdoMultiBox.AutoSize = true;
            this.rdoMultiBox.Location = new System.Drawing.Point(143, 23);
            this.rdoMultiBox.Name = "rdoMultiBox";
            this.rdoMultiBox.Size = new System.Drawing.Size(76, 20);
            this.rdoMultiBox.TabIndex = 0;
            this.rdoMultiBox.Text = "MultiBox";
            this.c1ThemeController1.SetTheme(this.rdoMultiBox, "(default)");
            this.rdoMultiBox.CheckedChanged += new System.EventHandler(this.Style_CheckedChanged);
            // 
            // rdoMultiForm
            // 
            this.rdoMultiForm.AutoSize = true;
            this.rdoMultiForm.Location = new System.Drawing.Point(22, 23);
            this.rdoMultiForm.Name = "rdoMultiForm";
            this.rdoMultiForm.Size = new System.Drawing.Size(84, 20);
            this.rdoMultiForm.TabIndex = 0;
            this.rdoMultiForm.Text = "MultiForm";
            this.c1ThemeController1.SetTheme(this.rdoMultiForm, "(default)");
            this.rdoMultiForm.CheckedChanged += new System.EventHandler(this.Style_CheckedChanged);
            // 
            // rdoMultiDocument
            // 
            this.rdoMultiDocument.AutoSize = true;
            this.rdoMultiDocument.Location = new System.Drawing.Point(256, 23);
            this.rdoMultiDocument.Name = "rdoMultiDocument";
            this.rdoMultiDocument.Size = new System.Drawing.Size(114, 20);
            this.rdoMultiDocument.TabIndex = 0;
            this.rdoMultiDocument.Text = "MultiDocument";
            this.c1ThemeController1.SetTheme(this.rdoMultiDocument, "(default)");
            this.rdoMultiDocument.Visible = false;
            this.rdoMultiDocument.CheckedChanged += new System.EventHandler(this.Style_CheckedChanged);
            // 
            // grpMainFormTabStyle
            // 
            this.grpMainFormTabStyle.Controls.Add(this.rdoPlain);
            this.grpMainFormTabStyle.Controls.Add(this.rdoIDE);
            this.grpMainFormTabStyle.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpMainFormTabStyle.Location = new System.Drawing.Point(23, 22);
            this.grpMainFormTabStyle.Name = "grpMainFormTabStyle";
            this.grpMainFormTabStyle.Size = new System.Drawing.Size(161, 58);
            this.grpMainFormTabStyle.TabIndex = 1;
            this.grpMainFormTabStyle.TabStop = false;
            this.grpMainFormTabStyle.Text = "Style";
            // 
            // rdoPlain
            // 
            this.rdoPlain.AutoSize = true;
            this.rdoPlain.Location = new System.Drawing.Point(88, 23);
            this.rdoPlain.Name = "rdoPlain";
            this.rdoPlain.Size = new System.Drawing.Size(53, 20);
            this.rdoPlain.TabIndex = 0;
            this.rdoPlain.Text = "Plain";
            this.c1ThemeController1.SetTheme(this.rdoPlain, "(default)");
            this.rdoPlain.CheckedChanged += new System.EventHandler(this.Style_CheckedChanged);
            // 
            // rdoIDE
            // 
            this.rdoIDE.AutoSize = true;
            this.rdoIDE.Location = new System.Drawing.Point(22, 23);
            this.rdoIDE.Name = "rdoIDE";
            this.rdoIDE.Size = new System.Drawing.Size(45, 20);
            this.rdoIDE.TabIndex = 0;
            this.rdoIDE.Text = "IDE";
            this.c1ThemeController1.SetTheme(this.rdoIDE, "(default)");
            this.rdoIDE.CheckedChanged += new System.EventHandler(this.Style_CheckedChanged);
            // 
            // grpOptionsTab
            // 
            this.grpOptionsTab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOptionsTab.Controls.Add(this.lblOptionsTabInactiveForeColor);
            this.grpOptionsTab.Controls.Add(this.pnlOptionsTabInactiveForeColor);
            this.grpOptionsTab.Controls.Add(this.c1DockingTab1);
            this.grpOptionsTab.Controls.Add(this.lblOptionsTabActiveForeColor);
            this.grpOptionsTab.Controls.Add(this.lblOptionsTabActiveBackColor);
            this.grpOptionsTab.Controls.Add(this.pnlOptionsTabActiveForeColor);
            this.grpOptionsTab.Controls.Add(this.pnlOptionsTabActiveBackColor);
            this.grpOptionsTab.Location = new System.Drawing.Point(390, 149);
            this.grpOptionsTab.Name = "grpOptionsTab";
            this.grpOptionsTab.Size = new System.Drawing.Size(782, 105);
            this.grpOptionsTab.TabIndex = 4;
            this.grpOptionsTab.TabStop = false;
            this.grpOptionsTab.Text = "Options Tab";
            // 
            // lblOptionsTabInactiveForeColor
            // 
            this.lblOptionsTabInactiveForeColor.AutoSize = true;
            this.lblOptionsTabInactiveForeColor.Location = new System.Drawing.Point(548, 26);
            this.lblOptionsTabInactiveForeColor.Name = "lblOptionsTabInactiveForeColor";
            this.lblOptionsTabInactiveForeColor.Size = new System.Drawing.Size(116, 16);
            this.lblOptionsTabInactiveForeColor.TabIndex = 9;
            this.lblOptionsTabInactiveForeColor.Text = "Inactive Fore Color:";
            this.lblOptionsTabInactiveForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlOptionsTabInactiveForeColor
            // 
            this.pnlOptionsTabInactiveForeColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlOptionsTabInactiveForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOptionsTabInactiveForeColor.Location = new System.Drawing.Point(666, 24);
            this.pnlOptionsTabInactiveForeColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlOptionsTabInactiveForeColor.Name = "pnlOptionsTabInactiveForeColor";
            this.pnlOptionsTabInactiveForeColor.Size = new System.Drawing.Size(74, 21);
            this.pnlOptionsTabInactiveForeColor.TabIndex = 10;
            this.pnlOptionsTabInactiveForeColor.Click += new System.EventHandler(this.pnlOptionsTabClick);
            // 
            // c1DockingTab1
            // 
            this.c1DockingTab1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1DockingTab1.Controls.Add(this.tabGlobal2);
            this.c1DockingTab1.Controls.Add(this.tabGeneral2);
            this.c1DockingTab1.Controls.Add(this.tabQueryEditor2);
            this.c1DockingTab1.Controls.Add(this.tabAutoComplete2);
            this.c1DockingTab1.Controls.Add(this.tabAutoReplace2);
            this.c1DockingTab1.Controls.Add(this.tabDataGrid2);
            this.c1DockingTab1.Controls.Add(this.tabKeywords2);
            this.c1DockingTab1.Controls.Add(this.tabSQLToCode2);
            this.c1DockingTab1.Controls.Add(this.tabSQLFormatter2);
            this.c1DockingTab1.Location = new System.Drawing.Point(15, 57);
            this.c1DockingTab1.Name = "c1DockingTab1";
            this.c1DockingTab1.SelectedTabBold = true;
            this.c1DockingTab1.Size = new System.Drawing.Size(755, 35);
            this.c1DockingTab1.TabIndex = 8;
            this.c1DockingTab1.TabsSpacing = -1;
            this.c1ThemeController1.SetTheme(this.c1DockingTab1, "(default)");
            this.c1DockingTab1.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.c1DockingTab1.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            // 
            // tabGlobal2
            // 
            this.tabGlobal2.Location = new System.Drawing.Point(1, 27);
            this.tabGlobal2.Name = "tabGlobal2";
            this.tabGlobal2.Size = new System.Drawing.Size(753, 7);
            this.tabGlobal2.TabIndex = 0;
            this.tabGlobal2.Text = "Global";
            // 
            // tabGeneral2
            // 
            this.tabGeneral2.Location = new System.Drawing.Point(1, 27);
            this.tabGeneral2.Name = "tabGeneral2";
            this.tabGeneral2.Size = new System.Drawing.Size(753, 7);
            this.tabGeneral2.TabIndex = 8;
            this.tabGeneral2.Text = "General";
            // 
            // tabQueryEditor2
            // 
            this.tabQueryEditor2.Location = new System.Drawing.Point(1, 27);
            this.tabQueryEditor2.Name = "tabQueryEditor2";
            this.tabQueryEditor2.Size = new System.Drawing.Size(753, 7);
            this.tabQueryEditor2.TabIndex = 1;
            this.tabQueryEditor2.Text = "Query Editor";
            // 
            // tabAutoComplete2
            // 
            this.tabAutoComplete2.Location = new System.Drawing.Point(1, 27);
            this.tabAutoComplete2.Name = "tabAutoComplete2";
            this.tabAutoComplete2.Size = new System.Drawing.Size(753, 7);
            this.tabAutoComplete2.TabIndex = 2;
            this.tabAutoComplete2.Text = "Auto Complete";
            // 
            // tabAutoReplace2
            // 
            this.tabAutoReplace2.Location = new System.Drawing.Point(1, 27);
            this.tabAutoReplace2.Name = "tabAutoReplace2";
            this.tabAutoReplace2.Size = new System.Drawing.Size(753, 7);
            this.tabAutoReplace2.TabIndex = 3;
            this.tabAutoReplace2.Text = "Auto Replace";
            // 
            // tabDataGrid2
            // 
            this.tabDataGrid2.Location = new System.Drawing.Point(1, 27);
            this.tabDataGrid2.Name = "tabDataGrid2";
            this.tabDataGrid2.Size = new System.Drawing.Size(753, 7);
            this.tabDataGrid2.TabIndex = 4;
            this.tabDataGrid2.Text = "Data Grid";
            // 
            // tabKeywords2
            // 
            this.tabKeywords2.Location = new System.Drawing.Point(1, 27);
            this.tabKeywords2.Name = "tabKeywords2";
            this.tabKeywords2.Size = new System.Drawing.Size(753, 7);
            this.tabKeywords2.TabIndex = 5;
            this.tabKeywords2.Text = "Keywords";
            // 
            // tabSQLToCode2
            // 
            this.tabSQLToCode2.Location = new System.Drawing.Point(1, 27);
            this.tabSQLToCode2.Name = "tabSQLToCode2";
            this.tabSQLToCode2.Size = new System.Drawing.Size(753, 7);
            this.tabSQLToCode2.TabIndex = 6;
            this.tabSQLToCode2.Text = "SQL to Code";
            // 
            // tabSQLFormatter2
            // 
            this.tabSQLFormatter2.Location = new System.Drawing.Point(1, 27);
            this.tabSQLFormatter2.Name = "tabSQLFormatter2";
            this.tabSQLFormatter2.Size = new System.Drawing.Size(753, 7);
            this.tabSQLFormatter2.TabIndex = 7;
            this.tabSQLFormatter2.Text = "SQL Formatter";
            // 
            // lblOptionsTabActiveForeColor
            // 
            this.lblOptionsTabActiveForeColor.AutoSize = true;
            this.lblOptionsTabActiveForeColor.Location = new System.Drawing.Point(15, 26);
            this.lblOptionsTabActiveForeColor.Name = "lblOptionsTabActiveForeColor";
            this.lblOptionsTabActiveForeColor.Size = new System.Drawing.Size(107, 16);
            this.lblOptionsTabActiveForeColor.TabIndex = 4;
            this.lblOptionsTabActiveForeColor.Text = "Active Fore Color:";
            this.lblOptionsTabActiveForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOptionsTabActiveBackColor
            // 
            this.lblOptionsTabActiveBackColor.AutoSize = true;
            this.lblOptionsTabActiveBackColor.Location = new System.Drawing.Point(275, 26);
            this.lblOptionsTabActiveBackColor.Name = "lblOptionsTabActiveBackColor";
            this.lblOptionsTabActiveBackColor.Size = new System.Drawing.Size(108, 16);
            this.lblOptionsTabActiveBackColor.TabIndex = 5;
            this.lblOptionsTabActiveBackColor.Text = "Active Back Color:";
            this.lblOptionsTabActiveBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlOptionsTabActiveForeColor
            // 
            this.pnlOptionsTabActiveForeColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlOptionsTabActiveForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOptionsTabActiveForeColor.Location = new System.Drawing.Point(124, 24);
            this.pnlOptionsTabActiveForeColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlOptionsTabActiveForeColor.Name = "pnlOptionsTabActiveForeColor";
            this.pnlOptionsTabActiveForeColor.Size = new System.Drawing.Size(74, 21);
            this.pnlOptionsTabActiveForeColor.TabIndex = 6;
            this.pnlOptionsTabActiveForeColor.Click += new System.EventHandler(this.pnlOptionsTabClick);
            // 
            // pnlOptionsTabActiveBackColor
            // 
            this.pnlOptionsTabActiveBackColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlOptionsTabActiveBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOptionsTabActiveBackColor.Location = new System.Drawing.Point(385, 24);
            this.pnlOptionsTabActiveBackColor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlOptionsTabActiveBackColor.Name = "pnlOptionsTabActiveBackColor";
            this.pnlOptionsTabActiveBackColor.Size = new System.Drawing.Size(74, 21);
            this.pnlOptionsTabActiveBackColor.TabIndex = 7;
            this.pnlOptionsTabActiveBackColor.Click += new System.EventHandler(this.pnlOptionsTabClick);
            // 
            // grpCheckForUpdate
            // 
            this.grpCheckForUpdate.Controls.Add(this.rdoCheckOnly);
            this.grpCheckForUpdate.Controls.Add(this.grpCheckOnly);
            this.grpCheckForUpdate.Controls.Add(this.rdoDonotCheck);
            this.grpCheckForUpdate.Location = new System.Drawing.Point(390, 21);
            this.grpCheckForUpdate.Name = "grpCheckForUpdate";
            this.grpCheckForUpdate.Size = new System.Drawing.Size(550, 123);
            this.grpCheckForUpdate.TabIndex = 17;
            this.grpCheckForUpdate.TabStop = false;
            this.grpCheckForUpdate.Text = "Check for Updates";
            // 
            // rdoCheckOnly
            // 
            this.rdoCheckOnly.AutoSize = true;
            this.rdoCheckOnly.Location = new System.Drawing.Point(20, 49);
            this.rdoCheckOnly.Name = "rdoCheckOnly";
            this.rdoCheckOnly.Size = new System.Drawing.Size(432, 20);
            this.rdoCheckOnly.TabIndex = 69;
            this.rdoCheckOnly.TabStop = true;
            this.rdoCheckOnly.Text = "Notify me when an update is available, but let me download it manually.";
            this.c1ThemeController1.SetTheme(this.rdoCheckOnly, "(default)");
            this.rdoCheckOnly.UseVisualStyleBackColor = true;
            this.rdoCheckOnly.CheckedChanged += new System.EventHandler(this.CheckForUpdates);
            // 
            // grpCheckOnly
            // 
            this.grpCheckOnly.Controls.Add(this.rdoCheckForUpdates0);
            this.grpCheckOnly.Controls.Add(this.rdoCheckForUpdates1);
            this.grpCheckOnly.Controls.Add(this.rdoCheckForUpdates7);
            this.grpCheckOnly.Location = new System.Drawing.Point(11, 51);
            this.grpCheckOnly.Name = "grpCheckOnly";
            this.grpCheckOnly.Size = new System.Drawing.Size(525, 60);
            this.grpCheckOnly.TabIndex = 68;
            this.grpCheckOnly.TabStop = false;
            this.grpCheckOnly.Text = "  ";
            this.c1ThemeController1.SetTheme(this.grpCheckOnly, "(default)");
            // 
            // rdoCheckForUpdates0
            // 
            this.rdoCheckForUpdates0.AutoSize = true;
            this.rdoCheckForUpdates0.Location = new System.Drawing.Point(24, 27);
            this.rdoCheckForUpdates0.Name = "rdoCheckForUpdates0";
            this.rdoCheckForUpdates0.Size = new System.Drawing.Size(83, 20);
            this.rdoCheckForUpdates0.TabIndex = 11;
            this.rdoCheckForUpdates0.TabStop = true;
            this.rdoCheckForUpdates0.Text = "on startup";
            this.rdoCheckForUpdates0.UseVisualStyleBackColor = true;
            // 
            // rdoCheckForUpdates1
            // 
            this.rdoCheckForUpdates1.AutoSize = true;
            this.rdoCheckForUpdates1.Location = new System.Drawing.Point(152, 27);
            this.rdoCheckForUpdates1.Name = "rdoCheckForUpdates1";
            this.rdoCheckForUpdates1.Size = new System.Drawing.Size(80, 20);
            this.rdoCheckForUpdates1.TabIndex = 12;
            this.rdoCheckForUpdates1.TabStop = true;
            this.rdoCheckForUpdates1.Text = "every day";
            this.rdoCheckForUpdates1.UseVisualStyleBackColor = true;
            // 
            // rdoCheckForUpdates7
            // 
            this.rdoCheckForUpdates7.AutoSize = true;
            this.rdoCheckForUpdates7.Location = new System.Drawing.Point(270, 27);
            this.rdoCheckForUpdates7.Name = "rdoCheckForUpdates7";
            this.rdoCheckForUpdates7.Size = new System.Drawing.Size(95, 20);
            this.rdoCheckForUpdates7.TabIndex = 17;
            this.rdoCheckForUpdates7.TabStop = true;
            this.rdoCheckForUpdates7.Text = "every 7 days";
            this.rdoCheckForUpdates7.UseVisualStyleBackColor = true;
            // 
            // rdoDonotCheck
            // 
            this.rdoDonotCheck.AutoSize = true;
            this.rdoDonotCheck.Location = new System.Drawing.Point(20, 22);
            this.rdoDonotCheck.Name = "rdoDonotCheck";
            this.rdoDonotCheck.Size = new System.Drawing.Size(429, 20);
            this.rdoDonotCheck.TabIndex = 67;
            this.rdoDonotCheck.TabStop = true;
            this.rdoDonotCheck.Text = "Don\'t automatically check for updates. I will check for updates manually.";
            this.c1ThemeController1.SetTheme(this.rdoDonotCheck, "(default)");
            this.rdoDonotCheck.UseVisualStyleBackColor = true;
            this.rdoDonotCheck.CheckedChanged += new System.EventHandler(this.CheckForUpdates);
            // 
            // txtHeightCode
            // 
            this.txtHeightCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtHeightCode.BackColor = System.Drawing.SystemColors.Control;
            this.txtHeightCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHeightCode.Location = new System.Drawing.Point(3, 97);
            this.txtHeightCode.Multiline = true;
            this.txtHeightCode.Name = "txtHeightCode";
            this.txtHeightCode.Size = new System.Drawing.Size(10, 594);
            this.txtHeightCode.TabIndex = 11;
            this.txtHeightCode.Visible = false;
            // 
            // txtHeightFormatter
            // 
            this.txtHeightFormatter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHeightFormatter.BackColor = System.Drawing.SystemColors.Control;
            this.txtHeightFormatter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHeightFormatter.Location = new System.Drawing.Point(1217, 69);
            this.txtHeightFormatter.Multiline = true;
            this.txtHeightFormatter.Name = "txtHeightFormatter";
            this.txtHeightFormatter.Size = new System.Drawing.Size(10, 623);
            this.txtHeightFormatter.TabIndex = 21;
            this.txtHeightFormatter.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(203, 734);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 36);
            this.button1.TabIndex = 4;
            this.button1.Text = "測試用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblRequireRestart
            // 
            this.lblRequireRestart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRequireRestart.AutoSize = true;
            this.lblRequireRestart.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblRequireRestart.Location = new System.Drawing.Point(24, 744);
            this.lblRequireRestart.Name = "lblRequireRestart";
            this.lblRequireRestart.Size = new System.Drawing.Size(174, 16);
            this.lblRequireRestart.TabIndex = 9;
            this.lblRequireRestart.Text = "Require to restart JasonQuery";
            this.lblRequireRestart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStarRequireToRestart
            // 
            this.lblStarRequireToRestart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStarRequireToRestart.AutoSize = true;
            this.lblStarRequireToRestart.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarRequireToRestart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarRequireToRestart.Location = new System.Drawing.Point(12, 746);
            this.lblStarRequireToRestart.Name = "lblStarRequireToRestart";
            this.lblStarRequireToRestart.Size = new System.Drawing.Size(14, 15);
            this.lblStarRequireToRestart.TabIndex = 8;
            this.lblStarRequireToRestart.Text = "*";
            // 
            // timerMother2Child
            // 
            this.timerMother2Child.Enabled = true;
            this.timerMother2Child.Tick += new System.EventHandler(this.timerMother2Child_Tick);
            // 
            // c1DockingTab
            // 
            this.c1DockingTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.c1DockingTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c1DockingTab.Controls.Add(this.tabGlobal);
            this.c1DockingTab.Controls.Add(this.tabGeneral);
            this.c1DockingTab.Controls.Add(this.tabQueryEditor);
            this.c1DockingTab.Controls.Add(this.tabAutoComplete);
            this.c1DockingTab.Controls.Add(this.tabAutoReplace);
            this.c1DockingTab.Controls.Add(this.tabDataGrid);
            this.c1DockingTab.Controls.Add(this.tabKeywords);
            this.c1DockingTab.Controls.Add(this.tabSQLToCode);
            this.c1DockingTab.Controls.Add(this.tabSQLFormatter);
            this.c1DockingTab.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1DockingTab.Location = new System.Drawing.Point(6, 6);
            this.c1DockingTab.Name = "c1DockingTab";
            this.c1DockingTab.SelectedIndex = 8;
            this.c1DockingTab.SelectedTabBold = true;
            this.c1DockingTab.Size = new System.Drawing.Size(1212, 714);
            this.c1DockingTab.TabIndex = 24;
            this.c1DockingTab.TabsSpacing = -1;
            this.c1ThemeController1.SetTheme(this.c1DockingTab, "(default)");
            this.c1DockingTab.VisualStyle = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.c1DockingTab.VisualStyleBase = C1.Win.C1Command.VisualStyle.Office2010Blue;
            this.c1DockingTab.SelectedIndexChanged += new System.EventHandler(this.c1DockingTab_SelectedIndexChanged);
            this.c1DockingTab.SelectedTabChanged += new System.EventHandler(this.c1DockingTab_SelectedTabChanged);
            this.c1DockingTab.SizeChanged += new System.EventHandler(this.c1DockingTab_SizeChanged);
            // 
            // tabGlobal
            // 
            this.tabGlobal.Controls.Add(this.grpGlobal);
            this.tabGlobal.Location = new System.Drawing.Point(1, 27);
            this.tabGlobal.Name = "tabGlobal";
            this.tabGlobal.Size = new System.Drawing.Size(1210, 686);
            this.tabGlobal.TabIndex = 8;
            this.tabGlobal.Text = "Global";
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.grpDefaultDirectory);
            this.tabGeneral.Controls.Add(this.grpOpenSQLFile);
            this.tabGeneral.Controls.Add(this.grpAutoDisconnect);
            this.tabGeneral.Controls.Add(this.grpColorTheme);
            this.tabGeneral.Location = new System.Drawing.Point(1, 27);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(1210, 686);
            this.tabGeneral.TabIndex = 9;
            this.tabGeneral.Text = "General";
            // 
            // grpDefaultDirectory
            // 
            this.grpDefaultDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDefaultDirectory.BackColor = System.Drawing.Color.Transparent;
            this.grpDefaultDirectory.Controls.Add(this.c1Button2);
            this.grpDefaultDirectory.Controls.Add(this.txtFavoriteDirectory);
            this.grpDefaultDirectory.Controls.Add(this.lblStarDefaultDirectory);
            this.grpDefaultDirectory.Controls.Add(this.rdoFavoriteDirectory);
            this.grpDefaultDirectory.Controls.Add(this.rdoDefaultDirectory);
            this.grpDefaultDirectory.Location = new System.Drawing.Point(12, 179);
            this.grpDefaultDirectory.Name = "grpDefaultDirectory";
            this.grpDefaultDirectory.Size = new System.Drawing.Size(547, 88);
            this.grpDefaultDirectory.TabIndex = 70;
            this.grpDefaultDirectory.TabStop = false;
            this.grpDefaultDirectory.Text = "Default Directory (Open/Save)     ";
            this.grpDefaultDirectory.Visible = false;
            // 
            // c1Button2
            // 
            this.c1Button2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.c1Button2.Location = new System.Drawing.Point(490, 52);
            this.c1Button2.Name = "c1Button2";
            this.c1Button2.Size = new System.Drawing.Size(22, 22);
            this.c1Button2.TabIndex = 81;
            this.c1Button2.Text = "...";
            this.c1ThemeController1.SetTheme(this.c1Button2, "(default)");
            this.c1Button2.UseVisualStyleBackColor = true;
            this.c1Button2.Visible = false;
            this.c1Button2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // txtFavoriteDirectory
            // 
            this.txtFavoriteDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFavoriteDirectory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.txtFavoriteDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFavoriteDirectory.Enabled = false;
            this.txtFavoriteDirectory.Location = new System.Drawing.Point(148, 52);
            this.txtFavoriteDirectory.Name = "txtFavoriteDirectory";
            this.txtFavoriteDirectory.Size = new System.Drawing.Size(355, 21);
            this.txtFavoriteDirectory.TabIndex = 10;
            this.txtFavoriteDirectory.Tag = null;
            this.txtFavoriteDirectory.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.txtFavoriteDirectory, "(default)");
            this.txtFavoriteDirectory.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblStarDefaultDirectory
            // 
            this.lblStarDefaultDirectory.AutoSize = true;
            this.lblStarDefaultDirectory.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarDefaultDirectory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarDefaultDirectory.Location = new System.Drawing.Point(182, 3);
            this.lblStarDefaultDirectory.Name = "lblStarDefaultDirectory";
            this.lblStarDefaultDirectory.Size = new System.Drawing.Size(14, 15);
            this.lblStarDefaultDirectory.TabIndex = 9;
            this.lblStarDefaultDirectory.Text = "*";
            // 
            // rdoFavoriteDirectory
            // 
            this.rdoFavoriteDirectory.AutoSize = true;
            this.rdoFavoriteDirectory.Location = new System.Drawing.Point(20, 51);
            this.rdoFavoriteDirectory.Name = "rdoFavoriteDirectory";
            this.rdoFavoriteDirectory.Size = new System.Drawing.Size(127, 20);
            this.rdoFavoriteDirectory.TabIndex = 2;
            this.rdoFavoriteDirectory.Text = "Favarite Directory:";
            this.rdoFavoriteDirectory.UseVisualStyleBackColor = true;
            // 
            // rdoDefaultDirectory
            // 
            this.rdoDefaultDirectory.AutoSize = true;
            this.rdoDefaultDirectory.Checked = true;
            this.rdoDefaultDirectory.Location = new System.Drawing.Point(20, 24);
            this.rdoDefaultDirectory.Name = "rdoDefaultDirectory";
            this.rdoDefaultDirectory.Size = new System.Drawing.Size(164, 20);
            this.rdoDefaultDirectory.TabIndex = 0;
            this.rdoDefaultDirectory.TabStop = true;
            this.rdoDefaultDirectory.Text = "System Default Directory";
            this.rdoDefaultDirectory.UseVisualStyleBackColor = true;
            // 
            // grpOpenSQLFile
            // 
            this.grpOpenSQLFile.BackColor = System.Drawing.Color.Transparent;
            this.grpOpenSQLFile.Controls.Add(this.btnClear2);
            this.grpOpenSQLFile.Controls.Add(this.btnClear1);
            this.grpOpenSQLFile.Controls.Add(this.btnSpecifiedSQLFile2);
            this.grpOpenSQLFile.Controls.Add(this.txtSpecifiedSQLFile2);
            this.grpOpenSQLFile.Controls.Add(this.lblFile2);
            this.grpOpenSQLFile.Controls.Add(this.btnSpecifiedSQLFile1);
            this.grpOpenSQLFile.Controls.Add(this.txtSpecifiedSQLFile1);
            this.grpOpenSQLFile.Controls.Add(this.lblFile1);
            this.grpOpenSQLFile.Location = new System.Drawing.Point(12, 78);
            this.grpOpenSQLFile.Name = "grpOpenSQLFile";
            this.grpOpenSQLFile.Size = new System.Drawing.Size(680, 90);
            this.grpOpenSQLFile.TabIndex = 69;
            this.grpOpenSQLFile.TabStop = false;
            this.grpOpenSQLFile.Text = "Open the specified SQL file on startup";
            // 
            // btnClear2
            // 
            this.btnClear2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClear2.Image = ((System.Drawing.Image)(resources.GetObject("btnClear2.Image")));
            this.btnClear2.Location = new System.Drawing.Point(642, 55);
            this.btnClear2.Name = "btnClear2";
            this.btnClear2.Size = new System.Drawing.Size(22, 21);
            this.btnClear2.TabIndex = 88;
            this.btnClear2.Tag = "2";
            this.c1ThemeController1.SetTheme(this.btnClear2, "(default)");
            this.btnClear2.UseVisualStyleBackColor = true;
            this.btnClear2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnClear2.Click += new System.EventHandler(this.btnClearFile_Click);
            // 
            // btnClear1
            // 
            this.btnClear1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClear1.Image = ((System.Drawing.Image)(resources.GetObject("btnClear1.Image")));
            this.btnClear1.Location = new System.Drawing.Point(642, 24);
            this.btnClear1.Name = "btnClear1";
            this.btnClear1.Size = new System.Drawing.Size(22, 21);
            this.btnClear1.TabIndex = 87;
            this.btnClear1.Tag = "1";
            this.c1ThemeController1.SetTheme(this.btnClear1, "(default)");
            this.btnClear1.UseVisualStyleBackColor = true;
            this.btnClear1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnClear1.Click += new System.EventHandler(this.btnClearFile_Click);
            // 
            // btnSpecifiedSQLFile2
            // 
            this.btnSpecifiedSQLFile2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSpecifiedSQLFile2.Location = new System.Drawing.Point(617, 55);
            this.btnSpecifiedSQLFile2.Name = "btnSpecifiedSQLFile2";
            this.btnSpecifiedSQLFile2.Size = new System.Drawing.Size(22, 21);
            this.btnSpecifiedSQLFile2.TabIndex = 86;
            this.btnSpecifiedSQLFile2.Tag = "2";
            this.btnSpecifiedSQLFile2.Text = "...";
            this.c1ThemeController1.SetTheme(this.btnSpecifiedSQLFile2, "(default)");
            this.btnSpecifiedSQLFile2.UseVisualStyleBackColor = true;
            this.btnSpecifiedSQLFile2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnSpecifiedSQLFile2.Click += new System.EventHandler(this.btnSpecifiedSQLFile_Click);
            // 
            // txtSpecifiedSQLFile2
            // 
            this.txtSpecifiedSQLFile2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.txtSpecifiedSQLFile2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpecifiedSQLFile2.Location = new System.Drawing.Point(63, 55);
            this.txtSpecifiedSQLFile2.Name = "txtSpecifiedSQLFile2";
            this.txtSpecifiedSQLFile2.ReadOnly = true;
            this.txtSpecifiedSQLFile2.Size = new System.Drawing.Size(530, 21);
            this.txtSpecifiedSQLFile2.TabIndex = 85;
            this.txtSpecifiedSQLFile2.Tag = null;
            this.txtSpecifiedSQLFile2.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.txtSpecifiedSQLFile2, "(default)");
            this.txtSpecifiedSQLFile2.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblFile2
            // 
            this.lblFile2.AutoSize = true;
            this.lblFile2.Location = new System.Drawing.Point(14, 57);
            this.lblFile2.Name = "lblFile2";
            this.lblFile2.Size = new System.Drawing.Size(30, 16);
            this.lblFile2.TabIndex = 84;
            this.lblFile2.Text = "File:";
            this.c1ThemeController1.SetTheme(this.lblFile2, "(default)");
            // 
            // btnSpecifiedSQLFile1
            // 
            this.btnSpecifiedSQLFile1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSpecifiedSQLFile1.Location = new System.Drawing.Point(617, 24);
            this.btnSpecifiedSQLFile1.Name = "btnSpecifiedSQLFile1";
            this.btnSpecifiedSQLFile1.Size = new System.Drawing.Size(22, 21);
            this.btnSpecifiedSQLFile1.TabIndex = 83;
            this.btnSpecifiedSQLFile1.Tag = "1";
            this.btnSpecifiedSQLFile1.Text = "...";
            this.c1ThemeController1.SetTheme(this.btnSpecifiedSQLFile1, "(default)");
            this.btnSpecifiedSQLFile1.UseVisualStyleBackColor = true;
            this.btnSpecifiedSQLFile1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnSpecifiedSQLFile1.Click += new System.EventHandler(this.btnSpecifiedSQLFile_Click);
            // 
            // txtSpecifiedSQLFile1
            // 
            this.txtSpecifiedSQLFile1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.txtSpecifiedSQLFile1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpecifiedSQLFile1.Location = new System.Drawing.Point(63, 24);
            this.txtSpecifiedSQLFile1.Name = "txtSpecifiedSQLFile1";
            this.txtSpecifiedSQLFile1.ReadOnly = true;
            this.txtSpecifiedSQLFile1.Size = new System.Drawing.Size(530, 21);
            this.txtSpecifiedSQLFile1.TabIndex = 82;
            this.txtSpecifiedSQLFile1.Tag = null;
            this.txtSpecifiedSQLFile1.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.txtSpecifiedSQLFile1, "(default)");
            this.txtSpecifiedSQLFile1.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblFile1
            // 
            this.lblFile1.AutoSize = true;
            this.lblFile1.Location = new System.Drawing.Point(14, 26);
            this.lblFile1.Name = "lblFile1";
            this.lblFile1.Size = new System.Drawing.Size(30, 16);
            this.lblFile1.TabIndex = 0;
            this.lblFile1.Text = "File:";
            this.c1ThemeController1.SetTheme(this.lblFile1, "(default)");
            // 
            // grpAutoDisconnect
            // 
            this.grpAutoDisconnect.BackColor = System.Drawing.Color.Transparent;
            this.grpAutoDisconnect.Controls.Add(this.btnHelp_Disconnect);
            this.grpAutoDisconnect.Controls.Add(this.cboAutoDisconnect);
            this.grpAutoDisconnect.Location = new System.Drawing.Point(184, 9);
            this.grpAutoDisconnect.Name = "grpAutoDisconnect";
            this.grpAutoDisconnect.Size = new System.Drawing.Size(217, 58);
            this.grpAutoDisconnect.TabIndex = 68;
            this.grpAutoDisconnect.TabStop = false;
            this.grpAutoDisconnect.Text = "Disconnect automatically";
            // 
            // btnHelp_Disconnect
            // 
            this.btnHelp_Disconnect.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp_Disconnect.Image")));
            this.btnHelp_Disconnect.Location = new System.Drawing.Point(174, 24);
            this.btnHelp_Disconnect.Name = "btnHelp_Disconnect";
            this.btnHelp_Disconnect.Size = new System.Drawing.Size(21, 21);
            this.btnHelp_Disconnect.TabIndex = 73;
            this.c1ThemeController1.SetTheme(this.btnHelp_Disconnect, "(default)");
            this.btnHelp_Disconnect.UseVisualStyleBackColor = true;
            this.btnHelp_Disconnect.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnHelp_Disconnect.Click += new System.EventHandler(this.btnHelp_Disconnect_Click);
            // 
            // cboAutoDisconnect
            // 
            this.cboAutoDisconnect.AllowSpinLoop = false;
            this.cboAutoDisconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.cboAutoDisconnect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cboAutoDisconnect.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cboAutoDisconnect.DropDownStyle = C1.Win.C1Input.DropDownStyle.DropDownList;
            this.cboAutoDisconnect.GapHeight = 0;
            this.cboAutoDisconnect.ImagePadding = new System.Windows.Forms.Padding(0);
            this.cboAutoDisconnect.ItemsDisplayMember = "";
            this.cboAutoDisconnect.ItemsValueMember = "";
            this.cboAutoDisconnect.Location = new System.Drawing.Point(22, 24);
            this.cboAutoDisconnect.Name = "cboAutoDisconnect";
            this.cboAutoDisconnect.Size = new System.Drawing.Size(146, 21);
            this.cboAutoDisconnect.TabIndex = 71;
            this.cboAutoDisconnect.Tag = null;
            this.cboAutoDisconnect.TextDetached = true;
            this.c1ThemeController1.SetTheme(this.cboAutoDisconnect, "(default)");
            this.cboAutoDisconnect.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // tabQueryEditor
            // 
            this.tabQueryEditor.CaptionText = "Data Grid";
            this.tabQueryEditor.Controls.Add(this.grpStatementCompletion);
            this.tabQueryEditor.Controls.Add(this.grpSchemaBrowser);
            this.tabQueryEditor.Controls.Add(this.grpEditorColors);
            this.tabQueryEditor.Controls.Add(this.grpPreferences);
            this.tabQueryEditor.Controls.Add(this.grpQueryEditorPreview);
            this.tabQueryEditor.Location = new System.Drawing.Point(1, 27);
            this.tabQueryEditor.Name = "tabQueryEditor";
            this.tabQueryEditor.Size = new System.Drawing.Size(1210, 686);
            this.tabQueryEditor.TabBackColorSelected = System.Drawing.Color.LightCyan;
            this.tabQueryEditor.TabIndex = 1;
            this.tabQueryEditor.Tag = "";
            this.tabQueryEditor.Text = "Query Editor";
            // 
            // grpStatementCompletion
            // 
            this.grpStatementCompletion.BackColor = System.Drawing.Color.Transparent;
            this.grpStatementCompletion.Controls.Add(this.btnHelp_SavePoint);
            this.grpStatementCompletion.Controls.Add(this.chkSavePoint);
            this.grpStatementCompletion.Controls.Add(this.chkAutoListMembers);
            this.grpStatementCompletion.Location = new System.Drawing.Point(12, 114);
            this.grpStatementCompletion.Name = "grpStatementCompletion";
            this.grpStatementCompletion.Size = new System.Drawing.Size(317, 81);
            this.grpStatementCompletion.TabIndex = 26;
            this.grpStatementCompletion.TabStop = false;
            this.grpStatementCompletion.Text = "Statement Completion";
            // 
            // btnHelp_SavePoint
            // 
            this.btnHelp_SavePoint.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp_SavePoint.Image")));
            this.btnHelp_SavePoint.Location = new System.Drawing.Point(249, 49);
            this.btnHelp_SavePoint.Name = "btnHelp_SavePoint";
            this.btnHelp_SavePoint.Size = new System.Drawing.Size(21, 21);
            this.btnHelp_SavePoint.TabIndex = 82;
            this.c1ThemeController1.SetTheme(this.btnHelp_SavePoint, "(default)");
            this.btnHelp_SavePoint.UseVisualStyleBackColor = true;
            this.btnHelp_SavePoint.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnHelp_SavePoint.Click += new System.EventHandler(this.btnHelp_SavePoint_Click);
            // 
            // chkSavePoint
            // 
            this.chkSavePoint.AutoSize = true;
            this.chkSavePoint.Location = new System.Drawing.Point(36, 50);
            this.chkSavePoint.Name = "chkSavePoint";
            this.chkSavePoint.Size = new System.Drawing.Size(206, 20);
            this.chkSavePoint.TabIndex = 1;
            this.chkSavePoint.Text = "SavePoint (for PostgreSQL only)";
            this.c1ThemeController1.SetTheme(this.chkSavePoint, "(default)");
            this.chkSavePoint.UseVisualStyleBackColor = true;
            // 
            // chkAutoListMembers
            // 
            this.chkAutoListMembers.AutoSize = true;
            this.chkAutoListMembers.Location = new System.Drawing.Point(20, 24);
            this.chkAutoListMembers.Name = "chkAutoListMembers";
            this.chkAutoListMembers.Size = new System.Drawing.Size(132, 20);
            this.chkAutoListMembers.TabIndex = 0;
            this.chkAutoListMembers.Text = "Auto List Members";
            this.c1ThemeController1.SetTheme(this.chkAutoListMembers, "(default)");
            this.chkAutoListMembers.UseVisualStyleBackColor = true;
            // 
            // grpSchemaBrowser
            // 
            this.grpSchemaBrowser.BackColor = System.Drawing.Color.Transparent;
            this.grpSchemaBrowser.Controls.Add(this.chkDefaultTabSchemaBrowser);
            this.grpSchemaBrowser.Controls.Add(this.lblStarShowColumnInfo);
            this.grpSchemaBrowser.Controls.Add(this.lblStarSortByColumnName);
            this.grpSchemaBrowser.Controls.Add(this.btnHelp_ShowColumnInfo);
            this.grpSchemaBrowser.Controls.Add(this.chkShowColumnInfo);
            this.grpSchemaBrowser.Controls.Add(this.chkSortByColumnName);
            this.grpSchemaBrowser.Location = new System.Drawing.Point(12, 9);
            this.grpSchemaBrowser.Name = "grpSchemaBrowser";
            this.grpSchemaBrowser.Size = new System.Drawing.Size(317, 100);
            this.grpSchemaBrowser.TabIndex = 25;
            this.grpSchemaBrowser.TabStop = false;
            this.grpSchemaBrowser.Text = "Schema Browser";
            // 
            // chkDefaultTabSchemaBrowser
            // 
            this.chkDefaultTabSchemaBrowser.AutoSize = true;
            this.chkDefaultTabSchemaBrowser.BackColor = System.Drawing.Color.Transparent;
            this.chkDefaultTabSchemaBrowser.BorderColor = System.Drawing.Color.Transparent;
            this.chkDefaultTabSchemaBrowser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkDefaultTabSchemaBrowser.ForeColor = System.Drawing.Color.Black;
            this.chkDefaultTabSchemaBrowser.Location = new System.Drawing.Point(20, 22);
            this.chkDefaultTabSchemaBrowser.Name = "chkDefaultTabSchemaBrowser";
            this.chkDefaultTabSchemaBrowser.Padding = new System.Windows.Forms.Padding(1);
            this.chkDefaultTabSchemaBrowser.Size = new System.Drawing.Size(242, 22);
            this.chkDefaultTabSchemaBrowser.TabIndex = 108;
            this.chkDefaultTabSchemaBrowser.Text = "Make Schema Browser the default tab";
            this.c1ThemeController1.SetTheme(this.chkDefaultTabSchemaBrowser, "(default)");
            this.chkDefaultTabSchemaBrowser.UseVisualStyleBackColor = true;
            this.chkDefaultTabSchemaBrowser.Value = null;
            this.chkDefaultTabSchemaBrowser.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // lblStarShowColumnInfo
            // 
            this.lblStarShowColumnInfo.AutoSize = true;
            this.lblStarShowColumnInfo.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarShowColumnInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarShowColumnInfo.Location = new System.Drawing.Point(229, 76);
            this.lblStarShowColumnInfo.Name = "lblStarShowColumnInfo";
            this.lblStarShowColumnInfo.Size = new System.Drawing.Size(14, 15);
            this.lblStarShowColumnInfo.TabIndex = 83;
            this.lblStarShowColumnInfo.Text = "*";
            // 
            // lblStarSortByColumnName
            // 
            this.lblStarSortByColumnName.AutoSize = true;
            this.lblStarSortByColumnName.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStarSortByColumnName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblStarSortByColumnName.Location = new System.Drawing.Point(171, 51);
            this.lblStarSortByColumnName.Name = "lblStarSortByColumnName";
            this.lblStarSortByColumnName.Size = new System.Drawing.Size(14, 15);
            this.lblStarSortByColumnName.TabIndex = 82;
            this.lblStarSortByColumnName.Text = "*";
            // 
            // btnHelp_ShowColumnInfo
            // 
            this.btnHelp_ShowColumnInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp_ShowColumnInfo.Image")));
            this.btnHelp_ShowColumnInfo.Location = new System.Drawing.Point(200, 72);
            this.btnHelp_ShowColumnInfo.Name = "btnHelp_ShowColumnInfo";
            this.btnHelp_ShowColumnInfo.Size = new System.Drawing.Size(21, 21);
            this.btnHelp_ShowColumnInfo.TabIndex = 81;
            this.c1ThemeController1.SetTheme(this.btnHelp_ShowColumnInfo, "(default)");
            this.btnHelp_ShowColumnInfo.UseVisualStyleBackColor = true;
            this.btnHelp_ShowColumnInfo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnHelp_ShowColumnInfo.Click += new System.EventHandler(this.btnHelp_ShowColumnInfo_Click);
            // 
            // chkShowColumnInfo
            // 
            this.chkShowColumnInfo.AutoSize = true;
            this.chkShowColumnInfo.BackColor = System.Drawing.Color.Transparent;
            this.chkShowColumnInfo.BorderColor = System.Drawing.Color.Transparent;
            this.chkShowColumnInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkShowColumnInfo.ForeColor = System.Drawing.Color.Black;
            this.chkShowColumnInfo.Location = new System.Drawing.Point(20, 72);
            this.chkShowColumnInfo.Name = "chkShowColumnInfo";
            this.chkShowColumnInfo.Padding = new System.Windows.Forms.Padding(1);
            this.chkShowColumnInfo.Size = new System.Drawing.Size(176, 22);
            this.chkShowColumnInfo.TabIndex = 68;
            this.chkShowColumnInfo.Text = "Show Column Information";
            this.c1ThemeController1.SetTheme(this.chkShowColumnInfo, "(default)");
            this.chkShowColumnInfo.UseVisualStyleBackColor = true;
            this.chkShowColumnInfo.Value = null;
            this.chkShowColumnInfo.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // chkSortByColumnName
            // 
            this.chkSortByColumnName.AutoSize = true;
            this.chkSortByColumnName.BackColor = System.Drawing.Color.Transparent;
            this.chkSortByColumnName.BorderColor = System.Drawing.Color.Transparent;
            this.chkSortByColumnName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chkSortByColumnName.ForeColor = System.Drawing.Color.Black;
            this.chkSortByColumnName.Location = new System.Drawing.Point(20, 47);
            this.chkSortByColumnName.Name = "chkSortByColumnName";
            this.chkSortByColumnName.Padding = new System.Windows.Forms.Padding(1);
            this.chkSortByColumnName.Size = new System.Drawing.Size(154, 22);
            this.chkSortByColumnName.TabIndex = 67;
            this.chkSortByColumnName.Text = "Sort by Column Name";
            this.c1ThemeController1.SetTheme(this.chkSortByColumnName, "(default)");
            this.chkSortByColumnName.UseVisualStyleBackColor = true;
            this.chkSortByColumnName.Value = null;
            this.chkSortByColumnName.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            // 
            // tabAutoComplete
            // 
            this.tabAutoComplete.CaptionText = "SQL History";
            this.tabAutoComplete.Controls.Add(this.grpAutoComplete);
            this.tabAutoComplete.Location = new System.Drawing.Point(1, 27);
            this.tabAutoComplete.Name = "tabAutoComplete";
            this.tabAutoComplete.Size = new System.Drawing.Size(1210, 686);
            this.tabAutoComplete.TabBackColorSelected = System.Drawing.Color.LightCyan;
            this.tabAutoComplete.TabIndex = 2;
            this.tabAutoComplete.Tag = "";
            this.tabAutoComplete.Text = "Auto Complete";
            // 
            // tabAutoReplace
            // 
            this.tabAutoReplace.Controls.Add(this.grpAutoReplace);
            this.tabAutoReplace.Location = new System.Drawing.Point(1, 27);
            this.tabAutoReplace.Name = "tabAutoReplace";
            this.tabAutoReplace.Size = new System.Drawing.Size(1210, 686);
            this.tabAutoReplace.TabBackColorSelected = System.Drawing.Color.LightCyan;
            this.tabAutoReplace.TabIndex = 3;
            this.tabAutoReplace.Text = "Auto Replace";
            // 
            // tabDataGrid
            // 
            this.tabDataGrid.Controls.Add(this.grpDataGrid);
            this.tabDataGrid.Controls.Add(this.grpDataGridColor);
            this.tabDataGrid.Controls.Add(this.grpPreviewGrid);
            this.tabDataGrid.Location = new System.Drawing.Point(1, 27);
            this.tabDataGrid.Name = "tabDataGrid";
            this.tabDataGrid.Size = new System.Drawing.Size(1210, 686);
            this.tabDataGrid.TabBackColorSelected = System.Drawing.Color.LightCyan;
            this.tabDataGrid.TabIndex = 4;
            this.tabDataGrid.Text = "Data Grid";
            // 
            // tabKeywords
            // 
            this.tabKeywords.Controls.Add(this.splitContainer4);
            this.tabKeywords.Location = new System.Drawing.Point(1, 27);
            this.tabKeywords.Name = "tabKeywords";
            this.tabKeywords.Size = new System.Drawing.Size(1210, 686);
            this.tabKeywords.TabBackColorSelected = System.Drawing.Color.LightCyan;
            this.tabKeywords.TabIndex = 5;
            this.tabKeywords.Text = "Keywords";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer5);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer6);
            this.splitContainer4.Size = new System.Drawing.Size(1210, 686);
            this.splitContainer4.SplitterDistance = 338;
            this.splitContainer4.TabIndex = 0;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.grpOperatorKeywords);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.grpBuiltInFunctions);
            this.splitContainer5.Size = new System.Drawing.Size(1210, 338);
            this.splitContainer5.SplitterDistance = 162;
            this.splitContainer5.TabIndex = 0;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.grpBuiltInKeywords);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.grpUserDefinedKeywords);
            this.splitContainer6.Size = new System.Drawing.Size(1210, 344);
            this.splitContainer6.SplitterDistance = 166;
            this.splitContainer6.TabIndex = 0;
            // 
            // tabSQLToCode
            // 
            this.tabSQLToCode.Controls.Add(this.grpSQLToCode);
            this.tabSQLToCode.Location = new System.Drawing.Point(1, 27);
            this.tabSQLToCode.Name = "tabSQLToCode";
            this.tabSQLToCode.Size = new System.Drawing.Size(1210, 686);
            this.tabSQLToCode.TabBackColorSelected = System.Drawing.Color.LightCyan;
            this.tabSQLToCode.TabIndex = 6;
            this.tabSQLToCode.Text = "SQL to Code";
            // 
            // tabSQLFormatter
            // 
            this.tabSQLFormatter.Controls.Add(this.grpSQLFormatter);
            this.tabSQLFormatter.Location = new System.Drawing.Point(1, 27);
            this.tabSQLFormatter.Name = "tabSQLFormatter";
            this.tabSQLFormatter.Size = new System.Drawing.Size(1210, 686);
            this.tabSQLFormatter.TabBackColorSelected = System.Drawing.Color.LightCyan;
            this.tabSQLFormatter.TabIndex = 7;
            this.tabSQLFormatter.Text = "SQL Formatter";
            // 
            // timerTitle
            // 
            this.timerTitle.Enabled = true;
            this.timerTitle.Interval = 250;
            this.timerTitle.Tick += new System.EventHandler(this.timerTitle_Tick);
            // 
            // pnlCopySettings
            // 
            this.pnlCopySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlCopySettings.Controls.Add(this.toolStrip6);
            this.pnlCopySettings.Location = new System.Drawing.Point(307, 734);
            this.pnlCopySettings.Name = "pnlCopySettings";
            this.pnlCopySettings.Size = new System.Drawing.Size(171, 31);
            this.pnlCopySettings.TabIndex = 27;
            this.pnlCopySettings.Visible = false;
            // 
            // toolStrip6
            // 
            this.toolStrip6.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip6.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCopyFrom});
            this.toolStrip6.Location = new System.Drawing.Point(0, 0);
            this.toolStrip6.Name = "toolStrip6";
            this.toolStrip6.Size = new System.Drawing.Size(171, 25);
            this.toolStrip6.TabIndex = 0;
            this.toolStrip6.Text = "toolStrip6";
            this.c1ThemeController1.SetTheme(this.toolStrip6, "(default)");
            // 
            // tsCopyFrom
            // 
            this.tsCopyFrom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsCopyFrom.Image = ((System.Drawing.Image)(resources.GetObject("tsCopyFrom.Image")));
            this.tsCopyFrom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCopyFrom.Name = "tsCopyFrom";
            this.tsCopyFrom.Size = new System.Drawing.Size(145, 22);
            this.tsCopyFrom.Text = "Copy Settings && Close";
            // 
            // btnCopySettings
            // 
            this.btnCopySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopySettings.Location = new System.Drawing.Point(532, 732);
            this.btnCopySettings.Name = "btnCopySettings";
            this.btnCopySettings.Size = new System.Drawing.Size(143, 36);
            this.btnCopySettings.TabIndex = 28;
            this.btnCopySettings.Text = "Copy Settings...";
            this.c1ThemeController1.SetTheme(this.btnCopySettings, "(default)");
            this.btnCopySettings.UseVisualStyleBackColor = true;
            this.btnCopySettings.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1082, 731);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(136, 36);
            this.btnClose.TabIndex = 56;
            this.btnClose.Text = "Cancel && &Close";
            this.c1ThemeController1.SetTheme(this.btnClose, "(default)");
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(933, 731);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(126, 36);
            this.btnApply.TabIndex = 57;
            this.btnApply.Text = "&Apply && Close";
            this.c1ThemeController1.SetTheme(this.btnApply, "(default)");
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnRestoreDefaults
            // 
            this.btnRestoreDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestoreDefaults.Location = new System.Drawing.Point(716, 732);
            this.btnRestoreDefaults.Name = "btnRestoreDefaults";
            this.btnRestoreDefaults.Size = new System.Drawing.Size(143, 36);
            this.btnRestoreDefaults.TabIndex = 58;
            this.btnRestoreDefaults.Text = "&Restore Defaults";
            this.c1ThemeController1.SetTheme(this.btnRestoreDefaults, "(default)");
            this.btnRestoreDefaults.UseVisualStyleBackColor = true;
            this.btnRestoreDefaults.VisualStyleBaseStyle = C1.Win.C1Input.VisualStyle.Office2010Blue;
            this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 780);
            this.Controls.Add(this.btnCopySettings);
            this.Controls.Add(this.pnlCopySettings);
            this.Controls.Add(this.c1DockingTab);
            this.Controls.Add(this.lblRequireRestart);
            this.Controls.Add(this.lblStarRequireToRestart);
            this.Controls.Add(this.txtHeightFormatter);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtHeightCode);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnRestoreDefaults);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.c1ThemeController1.SetTheme(this, "(default)");
            this.Load += new System.EventHandler(this.Form_Load);
            this.grpEditorColors.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.grpPreferences.ResumeLayout(false);
            this.grpPreferences.PerformLayout();
            this.grpIndent.ResumeLayout(false);
            this.grpIndent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkReplaceTabWithSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowIndentGuide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTabWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboIndentMode)).EndInit();
            this.grpIndicate.ResumeLayout(false);
            this.grpIndicate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboBookmarkStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCtrlMouseWheel1)).EndInit();
            this.grpWordWrap.ResumeLayout(false);
            this.grpWordWrap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWordWrap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOpenFileOnCurrentTab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowSaveAsButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEditorZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEditorFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboSaveAsEncoding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEntireBlankRowAsEmptyRow4SelectBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHighlightSelectedText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSaveAsEncoding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCopyAsHTML)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboEditorFontPicker)).EndInit();
            this.grpHighlight.ResumeLayout(false);
            this.grpHighlight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboHighlightStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboHighlightAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboHighlightOutlineAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowAllCharacters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHighlightSelection)).EndInit();
            this.grpColorTheme.ResumeLayout(false);
            this.grpColorTheme.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_DarkMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDarkMode)).EndInit();
            this.grpQueryEditorPreview.ResumeLayout(false);
            this.grpQueryEditorPreview.PerformLayout();
            this.tsEditor.ResumeLayout(false);
            this.tsEditor.PerformLayout();
            this.grpAutoComplete.ResumeLayout(false);
            this.grpAutoComplete.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFirstCharChecking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinFragmentLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableAutoComplete)).EndInit();
            this.grpAutoCompleteFor.ResumeLayout(false);
            this.grpAutoCompleteFor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkUserDefinedViews)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUserDefinedTriggers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUserDefinedTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUserDefinedFunctions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUserDefinedKeywords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBuiltInKeywords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBuiltInFunctions)).EndInit();
            this.grpAutoReplace.ResumeLayout(false);
            this.grpAutoReplace.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilterRowAR)).EndInit();
            this.grpModifyDefinitionAR.ResumeLayout(false);
            this.grpModifyDefinitionAR.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearAR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelAR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAR)).EndInit();
            this.grpDefinitionAR.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteAR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditAR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddAR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridARInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableAutoReplace)).EndInit();
            this.grpDataGrid.ResumeLayout(false);
            this.grpDataGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowGroupingRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_AppendingQueries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAppendingQueries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSetFocusAfterQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboRowsPerPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPagingQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_RawDataMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRawDataMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPreviewCLOBData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUseReadOnlyQueries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCtrlMouseWheel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGridRowHeightResizing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGridFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGridZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGridVisualStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResultCopyFieldSeparator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboResultCopyQuotingWith)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboMaxWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkResize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilterRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowStreamlinedName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowColumnType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboGridFontPicker)).EndInit();
            this.grpNullValueStyle.ResumeLayout(false);
            this.grpNullValueStyle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboNullShowAs)).EndInit();
            this.grpDataGridColor.ResumeLayout(false);
            this.grpDataGridColor.PerformLayout();
            this.grpPreviewGrid.ResumeLayout(false);
            this.grpPreviewGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboFindGrid)).EndInit();
            this.tsGrid.ResumeLayout(false);
            this.tsGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1GridVisualStyle)).EndInit();
            this.grpOperatorKeywords.ResumeLayout(false);
            this.grpFindOperatorKeywords.ResumeLayout(false);
            this.grpFindOperatorKeywords.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOperatorKeywords)).EndInit();
            this.grpBuiltInFunctions.ResumeLayout(false);
            this.grpBuiltInFunctions.PerformLayout();
            this.grpFindBuiltInFunctions.ResumeLayout(false);
            this.grpFindBuiltInFunctions.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBuiltInFunctions)).EndInit();
            this.grpBuiltInKeywords.ResumeLayout(false);
            this.grpBuiltInKeywords.PerformLayout();
            this.grpFindBuiltInKeywords.ResumeLayout(false);
            this.grpFindBuiltInKeywords.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBuiltInKeywords)).EndInit();
            this.grpUserDefinedKeywords.ResumeLayout(false);
            this.grpFindUserDefinedKeywords.ResumeLayout(false);
            this.grpFindUserDefinedKeywords.PerformLayout();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserDefinedKeywords)).EndInit();
            this.grpSQLToCode.ResumeLayout(false);
            this.grpSQLToCode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtVariableName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStripCode)).EndInit();
            this.grpSQLStatementCode.ResumeLayout(false);
            this.grpPreviewSQL.ResumeLayout(false);
            this.grpStyle.ResumeLayout(false);
            this.grpStyle.PerformLayout();
            this.grpLanguage.ResumeLayout(false);
            this.grpSQLFormatter.ResumeLayout(false);
            this.grpPreviewFormatter.ResumeLayout(false);
            this.grpFormattingOptions.ResumeLayout(false);
            this.grpFormattingOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkConvertCaseForKeywords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBreakJoinOnSections)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpandInLists)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpandBetweenConditions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpandCaseStatements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpandBooleanExpressions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTrailingCommas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExpandCommaLists)).EndInit();
            this.grpSQLStatementFormatter.ResumeLayout(false);
            this.grpGlobal.ResumeLayout(false);
            this.grpMaxEntries.ResumeLayout(false);
            this.grpMaxEntries.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMyFavorite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRecentFiles)).EndInit();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkHideClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLocalization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDateFormat)).EndInit();
            this.grpMainFormWindowsState.ResumeLayout(false);
            this.grpMainFormWindowsState.PerformLayout();
            this.grpMainFormTabVisualStyle.ResumeLayout(false);
            this.grpMainFormTabVisualStyle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkMultiLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHoverSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowArrows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShrinkPages)).EndInit();
            this.tabExample.ResumeLayout(false);
            this.grpAppearance.ResumeLayout(false);
            this.grpAppearance.PerformLayout();
            this.grpMainFormTabStyle.ResumeLayout(false);
            this.grpMainFormTabStyle.PerformLayout();
            this.grpOptionsTab.ResumeLayout(false);
            this.grpOptionsTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab1)).EndInit();
            this.c1DockingTab1.ResumeLayout(false);
            this.grpCheckForUpdate.ResumeLayout(false);
            this.grpCheckForUpdate.PerformLayout();
            this.grpCheckOnly.ResumeLayout(false);
            this.grpCheckOnly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DockingTab)).EndInit();
            this.c1DockingTab.ResumeLayout(false);
            this.tabGlobal.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.grpDefaultDirectory.ResumeLayout(false);
            this.grpDefaultDirectory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Button2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFavoriteDirectory)).EndInit();
            this.grpOpenSQLFile.ResumeLayout(false);
            this.grpOpenSQLFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSpecifiedSQLFile2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecifiedSQLFile2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSpecifiedSQLFile1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecifiedSQLFile1)).EndInit();
            this.grpAutoDisconnect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_Disconnect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboAutoDisconnect)).EndInit();
            this.tabQueryEditor.ResumeLayout(false);
            this.grpStatementCompletion.ResumeLayout(false);
            this.grpStatementCompletion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_SavePoint)).EndInit();
            this.grpSchemaBrowser.ResumeLayout(false);
            this.grpSchemaBrowser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDefaultTabSchemaBrowser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHelp_ShowColumnInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowColumnInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSortByColumnName)).EndInit();
            this.tabAutoComplete.ResumeLayout(false);
            this.tabAutoReplace.ResumeLayout(false);
            this.tabDataGrid.ResumeLayout(false);
            this.tabKeywords.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.tabSQLToCode.ResumeLayout(false);
            this.tabSQLFormatter.ResumeLayout(false);
            this.pnlCopySettings.ResumeLayout(false);
            this.pnlCopySettings.PerformLayout();
            this.toolStrip6.ResumeLayout(false);
            this.toolStrip6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1ThemeController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCopySettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestoreDefaults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblEditorBackground;
        private System.Windows.Forms.Panel pnlBuiltInFunctions;
        private System.Windows.Forms.Label lblBuiltInFunctions;
        private System.Windows.Forms.Panel pnlUserTables;
        private System.Windows.Forms.Label lblUserTables;
        private System.Windows.Forms.Panel pnlCharacter;
        private System.Windows.Forms.Label lblCharacter;
        private System.Windows.Forms.Panel pnlString;
        private System.Windows.Forms.Label lblString;
        private System.Windows.Forms.Panel pnlBuiltInKeywords;
        private System.Windows.Forms.Label lblBuiltInKeywords;
        private System.Windows.Forms.Panel pnlIdentifier;
        private System.Windows.Forms.Label lblIdentifier;
        private System.Windows.Forms.Panel pnlComments;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.Panel pnlCurrentLineBackground;
        private System.Windows.Forms.Panel pnlEditorBackground;
        private System.Windows.Forms.Label lblCurrentLineBackground;
        private System.Windows.Forms.GroupBox grpQueryEditorPreview;
        private ScintillaEditor editor;
        private System.Windows.Forms.Panel pnlNumber;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.GroupBox grpEditorColors;
        private System.Windows.Forms.GroupBox grpPreferences;
        private System.Windows.Forms.Panel pnlOperatorSymbol;
        private System.Windows.Forms.Label lblOperatorSymbol;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblWhiteSpace;
        private System.Windows.Forms.Panel pnlWhiteSpace;
        private System.Windows.Forms.Label lblIndentMode;
        private System.Windows.Forms.GroupBox grpWordWrap;
        private System.Windows.Forms.Panel pnlUserFunctions;
        private System.Windows.Forms.Label lblUserFunctions;
        private System.Windows.Forms.GroupBox grpAutoComplete;
        private System.Windows.Forms.GroupBox grpAutoCompleteFor;
        private System.Windows.Forms.GroupBox grpAutoReplace;
        private System.Windows.Forms.GroupBox grpModifyDefinitionAR;
        private System.Windows.Forms.GroupBox grpDefinitionAR;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1GridARInfo;
        private System.Windows.Forms.Label lblStarAR;
        private System.Windows.Forms.Label lblReplacement;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.Label lblRequireRestart;
        private System.Windows.Forms.Label lblStarRequireToRestart;
        private System.Windows.Forms.GroupBox grpHighlight;
        private System.Windows.Forms.Panel pnlHighlightForeColor;
        private System.Windows.Forms.Label lblHighlightColorForeColor;
        private System.Windows.Forms.Label lblHighlightColorStyle;
        private System.Windows.Forms.Label lblHighlightColorOutlineAlpha;
        private System.Windows.Forms.Label lblHighlightColorAlpha;
        private System.Windows.Forms.GroupBox grpPreviewGrid;
        private System.Windows.Forms.Label lblGridVisualStyle;
        private System.Windows.Forms.GroupBox grpDataGridColor;
        private System.Windows.Forms.GroupBox grpNullValueStyle;
        private System.Windows.Forms.Label lblResultCopyFieldSeparator;
        private System.Windows.Forms.Label lblResultCopyQuotingWith;
        private System.Windows.Forms.Panel pnlGridOddRowBackColor;
        private System.Windows.Forms.Panel pnlGridEvenRowBackColor;
        private System.Windows.Forms.Label lblGridZoom;
        private System.Windows.Forms.Panel pnlNullValueForeColor;
        private System.Windows.Forms.Label lblNullValueForeColor;
        private System.Windows.Forms.Label lblNullValueShowAs;
        private System.Windows.Forms.Label lblEditorZoom;
        private System.Windows.Forms.Label lblEditorFontName;
        private System.Windows.Forms.Label lblEditorFontSize;
        private System.Windows.Forms.Label lblGridFontSize;
        private System.Windows.Forms.Label lblGridFontName;
        private System.Windows.Forms.GroupBox grpOptionsTab;
        private System.Windows.Forms.Panel pnlOperatorKeywords;
        private System.Windows.Forms.Label lblOperatorKeywords;
        private System.Windows.Forms.Label lblUserDefinedKeywords;
        private System.Windows.Forms.Panel pnlUserDefinedKeywords;
        private System.Windows.Forms.GroupBox grpGlobal;
        private System.Windows.Forms.Label lblStarWordWrap;
        private System.Windows.Forms.Label lblStarHighlight;
        private System.Windows.Forms.Label lblStarAC;
        private System.Windows.Forms.GroupBox grpSQLToCode;
        private System.Windows.Forms.GroupBox grpLanguage;
        private System.Windows.Forms.GroupBox grpPreviewSQL;
        private System.Windows.Forms.GroupBox grpStyle;
        private System.Windows.Forms.Label lblMinFragmentLength;
        private ScintillaEditor editorSQLToCodePreview;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1GridVisualStyle;
        private System.Windows.Forms.Timer timerMother2Child;
        private System.Windows.Forms.GroupBox grpOperatorKeywords;
        private System.Windows.Forms.GroupBox grpBuiltInFunctions;
        private System.Windows.Forms.GroupBox grpBuiltInKeywords;
        private System.Windows.Forms.GroupBox grpUserDefinedKeywords;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.ListBox lstLanguage;
        private System.Windows.Forms.RadioButton rdoStyle3;
        private System.Windows.Forms.RadioButton rdoStyle2;
        private System.Windows.Forms.RadioButton rdoStyle1;
        private System.Windows.Forms.Label lblSQLVariableName;
        private System.Windows.Forms.GroupBox grpSQLStatementCode;
        private ScintillaEditor editorSQLToCode;
        private System.Windows.Forms.GroupBox grpSQLFormatter;
        private System.Windows.Forms.GroupBox grpSQLStatementFormatter;
        private ScintillaEditor editorSQLFormatter;
        private System.Windows.Forms.GroupBox grpPreviewFormatter;
        private ScintillaEditor editorSQLFormatterPreview;
        private System.Windows.Forms.GroupBox grpFormattingOptions;
        private System.Windows.Forms.Label lblMaxWidth2;
        private System.Windows.Forms.RadioButton rdoUpperCase;
        private System.Windows.Forms.RadioButton rdoProperCase;
        private System.Windows.Forms.RadioButton rdoLowerCase;
        private System.Windows.Forms.Label lblStarHighlightSelection;
        private System.Windows.Forms.Panel pnlGridHighlightBackColor;
        private System.Windows.Forms.Label lblGridHighlightBackColor;
        private System.Windows.Forms.Panel pnlGridHighlightForeColor;
        private System.Windows.Forms.Label lblGridHighlightForeColor;
        private System.Windows.Forms.ToolStrip tsGrid;
        private System.Windows.Forms.ToolStripLabel lblFindGrid;
        private System.Windows.Forms.ToolStripComboBox cboFindGrid3;
        private System.Windows.Forms.ToolStripButton btnFindNextGrid;
        private System.Windows.Forms.ToolStripButton btnFindPreviousGrid;
        private System.Windows.Forms.ToolStripButton btnHighlightAllGrid;
        private System.Windows.Forms.ToolStripButton btnClearHighlightsGrid;
        private System.Windows.Forms.ToolStripButton btnCountGrid;
        private ScintillaEditor txtOperatorKeywords;
        private ScintillaEditor txtBuiltInFunctions;
        private ScintillaEditor txtBuiltInKeywords;
        private ScintillaEditor txtUserDefinedKeywords;
        private System.Windows.Forms.GroupBox grpFindOperatorKeywords;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnNextOperatorKeywords;
        private System.Windows.Forms.ToolStripButton btnPreviousOperatorKeywords;
        private System.Windows.Forms.ToolStripButton btnCloseFindOperatorKeywords;
        private System.Windows.Forms.PictureBox picOperatorKeywords;
        private System.Windows.Forms.ToolStripTextBox txtFindOperatorKeywords;
        private System.Windows.Forms.PictureBox picBuiltInFunctions;
        private System.Windows.Forms.PictureBox picBuiltInKeywords;
        private System.Windows.Forms.PictureBox picUserDefinedKeywords;
        private System.Windows.Forms.GroupBox grpFindBuiltInFunctions;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripTextBox txtFindBuiltInFunctions;
        private System.Windows.Forms.ToolStripButton btnNextBuiltInFunctions;
        private System.Windows.Forms.ToolStripButton btnPreviousBuiltInFunctions;
        private System.Windows.Forms.ToolStripButton btnCloseFindBuiltInFunctions;
        private System.Windows.Forms.GroupBox grpFindBuiltInKeywords;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripTextBox txtFindBuiltInKeywords;
        private System.Windows.Forms.ToolStripButton btnNextBuiltInKeywords;
        private System.Windows.Forms.ToolStripButton btnPreviousBuiltInKeywords;
        private System.Windows.Forms.ToolStripButton btnCloseFindBuiltInKeywords;
        private System.Windows.Forms.GroupBox grpFindUserDefinedKeywords;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.ToolStripTextBox txtFindUserDefinedKeywords;
        private System.Windows.Forms.ToolStripButton btnNextUserDefinedKeywords;
        private System.Windows.Forms.ToolStripButton btnPreviousUserDefinedKeywords;
        private System.Windows.Forms.ToolStripButton btnCloseFindUserDefinedKeywords;
        private System.Windows.Forms.Label lblSelectedTextBackground;
        private System.Windows.Forms.Panel pnlSelectedTextBackground;
        private System.Windows.Forms.Label lblGridOddRowBackColor;
        private System.Windows.Forms.Label lblGridOddRowForeColor;
        private System.Windows.Forms.Label lblGridEvenRowBackColor;
        private System.Windows.Forms.Label lblGridEvenRowForeColor;
        private System.Windows.Forms.Panel pnlGridOddRowForeColor;
        private System.Windows.Forms.Panel pnlGridEvenRowForeColor;
        private System.Windows.Forms.GroupBox grpDataGrid;
        private System.Windows.Forms.Panel pnlGridSelectedBackColor;
        private System.Windows.Forms.Label lblGridSelectedBackColor;
        private System.Windows.Forms.Panel pnlGridSelectedForeColor;
        private System.Windows.Forms.Label lblGridSelectedForeColor;
        private System.Windows.Forms.Label lblBookmarkBackground;
        private System.Windows.Forms.Panel pnlBookmarkBackground;
        private System.Windows.Forms.GroupBox grpIndicate;
        private System.Windows.Forms.Label lblBookmarkStyle;
        private System.Windows.Forms.Label lblErrorLineBackground;
        private System.Windows.Forms.Panel pnlErrorLineBackground;
        private System.Windows.Forms.Label lblStarIndicate;
        private ScintillaEditor editorIndicator;
        private System.Windows.Forms.Label lblStarGridColor;
        private System.Windows.Forms.Label lblMaxWidth;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.TextBox txtHeightCode;
        private System.Windows.Forms.TextBox txtHeightFormatter;
        private System.Windows.Forms.Label lblARInfo1;
        private System.Windows.Forms.GroupBox grpCheckForUpdate;
        private System.Windows.Forms.RadioButton rdoCheckForUpdates1;
        private System.Windows.Forms.RadioButton rdoCheckForUpdates0;
        private System.Windows.Forms.Label lblGridRowHeightResizing;
        private C1.Win.C1Command.C1DockingTab c1DockingTab;
        private C1.Win.C1Command.C1DockingTabPage tabQueryEditor;
        private C1.Win.C1Command.C1DockingTabPage tabAutoComplete;
        private C1.Win.C1Command.C1DockingTabPage tabAutoReplace;
        private C1.Win.C1Command.C1DockingTabPage tabDataGrid;
        private C1.Win.C1Command.C1DockingTabPage tabKeywords;
        private C1.Win.C1Command.C1DockingTabPage tabSQLToCode;
        private C1.Win.C1Command.C1DockingTabPage tabSQLFormatter;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.RadioButton rdoCheckForUpdates7;
        private C1.Win.C1Input.C1FontPicker cboEditorFontPicker;
        private C1.Win.C1Input.C1FontPicker cboGridFontPicker;
        private System.Windows.Forms.GroupBox grpMainFormTabVisualStyle;
        private System.Windows.Forms.GroupBox grpAppearance;
        private System.Windows.Forms.RadioButton rdoMultiBox;
        private System.Windows.Forms.RadioButton rdoMultiForm;
        private System.Windows.Forms.RadioButton rdoMultiDocument;
        private System.Windows.Forms.GroupBox grpMainFormTabStyle;
        private System.Windows.Forms.RadioButton rdoPlain;
        private System.Windows.Forms.RadioButton rdoIDE;
        private Crownwood.Magic.Controls.TabControl tabExample;
        private Crownwood.Magic.Controls.TabPage tabPage1;
        private System.Windows.Forms.Timer timerTitle;
        private Crownwood.Magic.Controls.TabPage tabPage2;
        private Crownwood.Magic.Controls.TabPage tabPage3;
        private Crownwood.Magic.Controls.TabPage tabPage4;
        private Crownwood.Magic.Controls.TabPage tabPage5;
        private Crownwood.Magic.Controls.TabPage tabPage6;
        private Crownwood.Magic.Controls.TabPage tabPage7;
        private Crownwood.Magic.Controls.TabPage tabPage8;
        private Crownwood.Magic.Controls.TabPage tabPage9;
        private Crownwood.Magic.Controls.TabPage tabPage10;
        private Crownwood.Magic.Controls.TabPage tabPage11;
        private Crownwood.Magic.Controls.TabPage tabPage12;
        private Crownwood.Magic.Controls.TabPage tabPage13;
        private Crownwood.Magic.Controls.TabPage tabPage14;
        private Crownwood.Magic.Controls.TabPage tabPage15;
        private Crownwood.Magic.Controls.TabPage tabPage16;
        private Crownwood.Magic.Controls.TabPage tabPage17;
        private Crownwood.Magic.Controls.TabPage tabPage18;
        private Crownwood.Magic.Controls.TabPage tabPage19;
        private Crownwood.Magic.Controls.TabPage tabPage20;
        private System.Windows.Forms.Label lblLocalization;
        private System.Windows.Forms.CheckBox chkTabBold;
        private C1.Win.C1Command.C1DockingTab c1DockingTab1;
        private C1.Win.C1Command.C1DockingTabPage tabGlobal2;
        private C1.Win.C1Command.C1DockingTabPage tabQueryEditor2;
        private C1.Win.C1Command.C1DockingTabPage tabAutoComplete2;
        private C1.Win.C1Command.C1DockingTabPage tabAutoReplace2;
        private C1.Win.C1Command.C1DockingTabPage tabDataGrid2;
        private C1.Win.C1Command.C1DockingTabPage tabKeywords2;
        private C1.Win.C1Command.C1DockingTabPage tabSQLToCode2;
        private C1.Win.C1Command.C1DockingTabPage tabSQLFormatter2;
        private System.Windows.Forms.Label lblOptionsTabActiveForeColor;
        private System.Windows.Forms.Label lblOptionsTabActiveBackColor;
        private System.Windows.Forms.Panel pnlOptionsTabActiveForeColor;
        private System.Windows.Forms.Panel pnlOptionsTabActiveBackColor;
        private System.Windows.Forms.Label lblDateFormat;
        private System.Windows.Forms.GroupBox grpMainFormWindowsState;
        private System.Windows.Forms.RadioButton rdoMaximized;
        private System.Windows.Forms.RadioButton rdoNormal;
        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.Panel pnlCopySettings;
        private System.Windows.Forms.ToolStrip toolStrip6;
        private System.Windows.Forms.ToolStripDropDownButton tsCopyFrom;
        private System.Windows.Forms.Label lblToolstripBackground;
        private System.Windows.Forms.Panel pnlToolstripBackground;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnComment;
        private System.Windows.Forms.ToolStripButton btnRemoveComment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnIndent;
        private System.Windows.Forms.ToolStripTextBox txtIndentWord;
        private System.Windows.Forms.ToolStripButton btnUnIndent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnHighlightSelection;
        private System.Windows.Forms.ToolStripButton btnHighlightSelection2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnWordWrap;
        private System.Windows.Forms.ToolStripButton btnWordWrap2;
        private System.Windows.Forms.ToolStripButton btnShowAllCharacters;
        private System.Windows.Forms.ToolStripButton btnShowAllCharacters2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.GroupBox grpMaxEntries;
        private System.Windows.Forms.Label lblMyFavorite;
        private System.Windows.Forms.Label lblRecentFiles;
        private System.Windows.Forms.Label lblOptionsTabInactiveForeColor;
        private System.Windows.Forms.Panel pnlOptionsTabInactiveForeColor;
        private C1.Win.C1Themes.C1ThemeController c1ThemeController1;
        private C1.Win.C1Input.C1SplitButton btnCopySettings;
        private C1.Win.C1Command.C1DockingTabPage tabGlobal;
        private C1.Win.C1Input.C1CheckBox chkShrinkPages;
        private C1.Win.C1Input.C1CheckBox chkWordWrap;
        private C1.Win.C1Input.C1CheckBox chkMultiLine;
        private C1.Win.C1Input.C1CheckBox chkHoverSelect;
        private C1.Win.C1Input.C1CheckBox chkShowArrows;
        private C1.Win.C1Input.C1CheckBox chkHighlightSelection;
        private C1.Win.C1Input.C1CheckBox chkEntireBlankRowAsEmptyRow4SelectBlock;
        private C1.Win.C1Input.C1CheckBox chkHighlightSelectedText;
        private C1.Win.C1Input.C1CheckBox chkSaveAsEncoding;
        private C1.Win.C1Input.C1CheckBox chkCopyAsHTML;
        private C1.Win.C1Input.C1CheckBox chkBold;
        private C1.Win.C1Input.C1CheckBox chkMargin;
        private C1.Win.C1Input.C1CheckBox chkEnd;
        private C1.Win.C1Input.C1CheckBox chkStart;
        private C1.Win.C1Input.C1CheckBox chkShowAllCharacters;
        private C1.Win.C1Input.C1CheckBox chkShowFilterRowAC;
        private C1.Win.C1Input.C1CheckBox chkUserDefinedViews;
        private C1.Win.C1Input.C1CheckBox chkUserDefinedTriggers;
        private C1.Win.C1Input.C1CheckBox chkUserDefinedTables;
        private C1.Win.C1Input.C1CheckBox chkUserDefinedFunctions;
        private C1.Win.C1Input.C1CheckBox chkUserDefinedKeywords;
        private C1.Win.C1Input.C1CheckBox chkBuiltInKeywords;
        private C1.Win.C1Input.C1CheckBox chkBuiltInFunctions;
        private C1.Win.C1Input.C1CheckBox chkEnableAutoComplete;
        private C1.Win.C1Input.C1CheckBox chkShowFilterRowAR;
        private C1.Win.C1Input.C1CheckBox chkEnableAutoReplace;
        private C1.Win.C1Input.C1CheckBox chkResize;
        private C1.Win.C1Input.C1CheckBox chkSort;
        private C1.Win.C1Input.C1CheckBox chkShowFilterRow;
        private C1.Win.C1Input.C1CheckBox chkShowStreamlinedName;
        private C1.Win.C1Input.C1CheckBox chkShowColumnType;
        private C1.Win.C1Input.C1CheckBox chkStripCode;
        private C1.Win.C1Input.C1CheckBox chkConvertCaseForKeywords;
        private C1.Win.C1Input.C1CheckBox chkBreakJoinOnSections;
        private C1.Win.C1Input.C1CheckBox chkExpandInLists;
        private C1.Win.C1Input.C1CheckBox chkExpandBetweenConditions;
        private C1.Win.C1Input.C1CheckBox chkExpandCaseStatements;
        private C1.Win.C1Input.C1CheckBox chkExpandBooleanExpressions;
        private C1.Win.C1Input.C1CheckBox chkTrailingCommas;
        private C1.Win.C1Input.C1CheckBox chkExpandCommaLists;
        private C1.Win.C1Input.C1Button btnClose;
        private C1.Win.C1Input.C1Button btnApply;
        private C1.Win.C1Input.C1Button btnRestoreDefaults;
        private C1.Win.C1Input.C1Button btnClearAR;
        private C1.Win.C1Input.C1Button btnCancelAR;
        private C1.Win.C1Input.C1Button btnSaveAR;
        private C1.Win.C1Input.C1Button btnDeleteAR;
        private C1.Win.C1Input.C1Button btnEditAR;
        private C1.Win.C1Input.C1Button btnAddAR;
        private C1.Win.C1Input.C1ComboBox cboSaveAsEncoding;
        private C1.Win.C1Input.C1ComboBox cboHighlightStyle;
        private C1.Win.C1Input.C1ComboBox cboEditorZoom;
        private C1.Win.C1Input.C1ComboBox cboEditorFontSize;
        private C1.Win.C1Input.C1ComboBox cboHighlightAlpha;
        private C1.Win.C1Input.C1ComboBox cboHighlightOutlineAlpha;
        private C1.Win.C1Input.C1ComboBox cboBookmarkStyle;
        private C1.Win.C1Input.C1ComboBox cboIndentMode;
        private C1.Win.C1Input.C1TextBox txtRecentFiles;
        private C1.Win.C1Input.C1TextBox txtMyFavorite;
        private C1.Win.C1Input.C1TextBox txtKeyword;
        private C1.Win.C1Input.C1TextBox txtVariableName;
        private C1.Win.C1Input.C1TextBox txtMaxWidth;
        private C1.Win.C1Input.C1ComboBox cboMaxWidth;
        private C1.Win.C1Input.C1ComboBox cboDateFormat;
        private C1.Win.C1Input.C1ComboBox cboGridRowHeightResizing;
        private C1.Win.C1Input.C1ComboBox cboGridFontSize;
        private C1.Win.C1Input.C1ComboBox cboGridZoom;
        private C1.Win.C1Input.C1ComboBox cboGridVisualStyle;
        private C1.Win.C1Input.C1ComboBox cboResultCopyFieldSeparator;
        private C1.Win.C1Input.C1ComboBox cboResultCopyQuotingWith;
        private C1.Win.C1Input.C1ComboBox cboNullShowAs;
        private C1.Win.C1Input.C1ComboBox cboFindGrid;
        private System.Windows.Forms.Label lblGridHeadingForeColor;
        private System.Windows.Forms.Panel pnlGridHeadingForeColor;
        private C1.Win.C1Input.C1ComboBox cboLocalization;
        private System.Windows.Forms.Label lblTips;
        private C1.Win.C1Input.C1CheckBox chkShowSaveAsButton;
        private System.Windows.Forms.GroupBox grpColorTheme;
        private C1.Win.C1Input.C1CheckBox chkDarkMode;
        private C1.Win.C1Input.C1Button btnHelp_DarkMode;
        private C1.Win.C1Input.C1CheckBox chkOpenFileOnCurrentTab;
        private C1.Win.C1Command.C1DockingTabPage tabGeneral;
        private System.Windows.Forms.GroupBox grpOpenSQLFile;
        private System.Windows.Forms.GroupBox grpAutoDisconnect;
        private C1.Win.C1Input.C1Button btnHelp_Disconnect;
        private C1.Win.C1Input.C1ComboBox cboAutoDisconnect;
        private System.Windows.Forms.RadioButton rdoCheckOnly;
        private System.Windows.Forms.GroupBox grpCheckOnly;
        private System.Windows.Forms.RadioButton rdoDonotCheck;
        private C1.Win.C1Command.C1DockingTabPage tabGeneral2;
        private System.Windows.Forms.Label lblFile1;
        private C1.Win.C1Input.C1Button btnSpecifiedSQLFile1;
        private C1.Win.C1Input.C1TextBox txtSpecifiedSQLFile1;
        private C1.Win.C1Input.C1Button btnSpecifiedSQLFile2;
        private C1.Win.C1Input.C1TextBox txtSpecifiedSQLFile2;
        private System.Windows.Forms.Label lblFile2;
        private System.Windows.Forms.GroupBox grpDefaultDirectory;
        private C1.Win.C1Input.C1Button c1Button2;
        private C1.Win.C1Input.C1TextBox txtFavoriteDirectory;
        private System.Windows.Forms.Label lblStarDefaultDirectory;
        private System.Windows.Forms.RadioButton rdoFavoriteDirectory;
        private System.Windows.Forms.RadioButton rdoDefaultDirectory;
        private C1.Win.C1Input.C1Button btnClear2;
        private C1.Win.C1Input.C1Button btnClear1;
        private C1.Win.C1Input.C1CheckBox chkShowVersion;
        private C1.Win.C1Input.C1CheckBox chkCtrlMouseWheel1;
        private C1.Win.C1Input.C1CheckBox chkCtrlMouseWheel2;
        private System.Windows.Forms.Label lblStarShowVersion;
        private System.Windows.Forms.Label lblARInfo2;
        private C1.Win.C1Input.C1CheckBox chkShowIndentGuide;
        private System.Windows.Forms.Label lblStarShowIndentGuide;
        private C1.Win.C1Input.C1ComboBox cboTabWidth;
        private System.Windows.Forms.Label lblTabWidth;
        private System.Windows.Forms.GroupBox grpIndent;
        private System.Windows.Forms.Label lblStarTabWidth;
        private C1.Win.C1Input.C1CheckBox chkReplaceTabWithSpace;
        private System.Windows.Forms.ToolStripButton btnShowIndentGuide;
        private System.Windows.Forms.ToolStripButton btnShowIndentGuide2;
        private C1.Win.C1Input.C1CheckBox chkPreviewCLOBData;
        private C1.Win.C1Input.C1CheckBox chkUseReadOnlyQueries;
        private C1.Win.C1Input.C1Button btnHelp_RawDataMode;
        private C1.Win.C1Input.C1CheckBox chkRawDataMode;
        private C1.Win.C1Input.C1ComboBox cboRowsPerPage;
        private System.Windows.Forms.Label lblRowsPerPage;
        private C1.Win.C1Input.C1CheckBox chkPagingQuery;
        private C1.Win.C1Input.C1CheckBox chkSetFocusAfterQuery;
        private System.Windows.Forms.Label lblStyle3;
        private System.Windows.Forms.Label lblStyle2;
        private System.Windows.Forms.Label lblStyle1;
        private System.Windows.Forms.Label lblStarMainFormTab;
        private System.Windows.Forms.GroupBox grpSchemaBrowser;
        private C1.Win.C1Input.C1CheckBox chkShowColumnInfo;
        private C1.Win.C1Input.C1CheckBox chkSortByColumnName;
        private C1.Win.C1Input.C1Button btnHelp_ShowColumnInfo;
        private System.Windows.Forms.Label lblStarShowColumnInfo;
        private System.Windows.Forms.Label lblStarSortByColumnName;
        private C1.Win.C1Input.C1CheckBox chkDefaultTabSchemaBrowser;
        private C1.Win.C1Input.C1CheckBox chkAppendingQueries;
        private C1.Win.C1Input.C1Button btnHelp_AppendingQueries;
        private C1.Win.C1Input.C1CheckBox chkHideClock;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpStatementCompletion;
        private C1.Win.C1Input.C1Button btnHelp_SavePoint;
        private System.Windows.Forms.CheckBox chkSavePoint;
        private System.Windows.Forms.CheckBox chkAutoListMembers;
        private ScintillaEditor editorAR;
        private System.Windows.Forms.NumericUpDown nudMinFragmentLength;
        private C1.Win.C1Input.C1CheckBox chkFirstCharChecking;
        private C1.Win.C1Input.C1CheckBox chkShowGroupingRow;
    }
}

