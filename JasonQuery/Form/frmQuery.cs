using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using C1.Win.C1TrueDBGrid;
using C1.Win.C1Input;
using C1.Win.C1Command;
using JasonLibrary.Class;
using JasonLibrary.Stylers;

/* 重建方案前，再次確認此表單以下兩個物件
   【editor】Width = 755
   【btnExpandCollapse】ButtonClick 事件 → btnExpandCollapse_ButtonClick
*/

namespace JasonQuery
{
    public partial class frmQuery : Form
    {
        //private string sTempFilename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Log_" + DateTime.Now.ToString("HHmmss") + ".txt";
        //TextEngine.WriteContentToFile(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + " editor_KeyUp, bKeyPressCtrlJ = " + bKeyPressCtrlJ.ToString() + ", _bKeyPressCtrlJ = " + _bKeyPressCtrlJ.ToString() + ", e.KeyData = " + e.KeyData.ToString() + "\r\n", sTempFilename, TextEncode.Default, FileMode.Append);
        private int _iQueryIndex;
        private string _sUnfreezeColumnName = "";
        private string _sQueryEditorFontSize = "";
        private string _sPosXY4AC = ""; //觸發 AC 下拉清單時的 X, Y 座標

        private int _iLastRowOffset;
        private int _iLastTimeOffset;

        private bool _bNextPageQuery;
        private int _iNextPageCol;
        private int _iNextPageRow;
        private DataTable _dtNextPage; //for NextPage merge
        private double _dNext = 1;
        private int _SplitsNum = 0;
        private int _iii = 0;
        private string _sSelectedTextDoubleClick = ""; //20221102 記住 Double Click 的字串

        private FindReplace.FindReplace myFindReplace; //20230629
        private Thread _threadQuery;
        private delegate void BindDatagrid();

        private int _iShiftMouseLeftDownX = -1;
        private int _iShiftMouseLeftDownY = -1;

        private Color _cToolstripFocused = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);
        private Color _cToolstripUnfocused = SystemColors.Control;

        private string _sLangText = "";
        private string _sDataGridTabName = "";
        private string _sDataGridTabNameOriginal = "";
        private string _sOriginal3TabName = "";
        private int _iQueryTextParametersStart; //含參數的 SQL，該段落的SQL的起始位置
        private string _sQueryTextParameters = ""; //含參數的 SQL，置換為指定值之後的完整 SQL
        private string _sQueryTextParametersMapping = ""; //此參數要呈現在 SQL Hisotry 的訊息欄
        private string _sQueryTextParametersPositionMapping = ""; //此參數供錯誤定位用的

        private ContextMenuStrip _cMenuQueryEditor = new ContextMenuStrip(); //Query Editor 右鍵選單
        private ContextMenuStrip _cMenuGrid = new ContextMenuStrip(); //Grid 右鍵選單
        private ContextMenuStrip _cMenuGridHeader = new ContextMenuStrip(); //Grid 右鍵選單 (標題列 - 排序用)
        private ContextMenuStrip _cMenuMessageEditor = new ContextMenuStrip(); //Message Editor 右鍵選單
        private ContextMenuStrip _cMenuSQLHistoryEditor = new ContextMenuStrip(); //SQL History Editor 右鍵選單
        private ContextMenuStrip _cMenuSchemaBrowser = new ContextMenuStrip(); //SchemaBrowser Grid 右鍵選單

        private List<string> _lstMenuEditor = new List<string>();
        private List<string> _lstMenuGrid = new List<string>();

        private readonly DataTable _dtNull = new DataTable(); //查詢前的空 Table，讓 Grid 呈現用
        private DataTable _dtSchemaTableExport = new DataTable(); //承接查詢時所儲存的 SchemaTable

        private string _sQueryStatus = ""; //20191212
        private string _sSqlWhenError = ""; //20200220

        private bool _bGridZooming;
        private bool _bBusy;
        private bool _bNeedToSaveSplitter; //判斷是否需要 SaveSplitter 寬／高度值

        private List<Point> _modifiedList = new List<Point>();
        private List<string> _lstGridHeaderAR = new List<string>();

        private int _iPeriodPos; //記住顯示欄位下拉清單時的游標所在位置
        private int _iPeriodPos2; //記住顯示欄位下拉清單時的游標所在位置 (如果 c1GridAC 是顯示狀態，記住最新的游標所在)
        private DataTable _dtAC4Period = new DataTable();
        private bool _bKeyPressCtrlJ; //記住是否按下 Ctrl+J (KeyUp 無法偵測 Ctrl+J)
        private bool _bKeyPressTab; //記住是否按下 Tab 鍵 (KeyUp 可以偵測 Tab，但 Tab 鍵已被提前觸發了，故不在 KeyUp 攔截)
        private int _iSpacePos; //記住顯示欄位下拉清單時的游標所在位置
        private int _iSpacePos2; //記住顯示欄位下拉清單時的游標所在位置 (如果 c1GridAC 是顯示狀態，記住最新的游標所在)
        private DataTable _dtAC4Space = new DataTable();
        private int _iAllPos; //記住顯示欄位下拉清單時的游標所在位置
        private int _iAllPos2; //記住顯示欄位下拉清單時的游標所在位置 (如果 c1GridAC 是顯示狀態，記住最新的游標所在)
        private bool _bKeyUpFromACGrid; //是否由 Grid 按下 Up 鍵回到 Editor？
        private int _iCompoundKeyCtrlShift; //for Ctrl + Shift + ?

        private string _sColKeywordName = "Keyword";
        private string _sColReplacementName = "Replacement";
        private Dictionary<string, string> _dicAutoReplace = new Dictionary<string, string>();

        [DllImport("user32")]
        private static extern bool SetCursorPos(int x, int y);

        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]

        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        private enum eColAR
        {
            PID = 0,
            Keyword,
            Replacement
        }

        private Devart.Data.Oracle.OracleDataReader oDataReader;
        private Devart.Data.PostgreSql.PgSqlDataReader pDataReader;
        private Devart.Data.SqlServer.SqlDataReader sDataReader;
        private Devart.Data.MySql.MySqlDataReader mDataReader;
        private Devart.Data.SQLite.SQLiteDataReader qDataReader; //SQLite
        private Devart.Data.SQLite.SQLiteDataReader cDataReader; //SQLCipher

        private const int _iSquiggleNum = 11; //20191224 波浪底線

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(IntPtr classname, string title); // extern method: FindWindow

        //<--Begin: 20190322 尋找並關閉 MessageBox
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private const int WM_CLOSE = 0x10;

        [DllImport("user32.dll")]
        private static extern void MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool rePaint); // extern method: MoveWindow

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rect); // extern method: GetWindowRect
        //-->End: 20190322

        private bool _bFormLoadFinish; //表單是否載入完畢 (避免觸發事件)
        private int _iQueryCount;
        private DateTime _dtStartTime, _dtEndTime;

        private int _rowHeight; // original row height
        private int _recSelWidth; // oringal record selector width
        private float _fontSize; // original font size

        private DataTable _dtARInfo = new DataTable();
        private DataRow _rowARInfo;

        private string _sEditorLength = "", _sEditorLines = "", _sEditorLn = "", _sEditorCol = "", _sEditorPos = "", _sEditorSel = "", _sExecTime = "", _sQueryTime = "", _sRows = "";
        private bool _ctrlKeyDown;
        private int _totalDelta;

        private const int BOOKMARK_MARGIN = 1; // Conventionally the symbol margin
        private const int BOOKMARK_MARKER = 3; // Arbitrary. Any valid index would work.

        public event ValueUpdatedEventHandler ValueUpdated;

        private static void BuildAutoCompleteMenu(bool bEnable = true)
        {
            if (bEnable == false)
            {
                MyGlobal.dtAC4All = null;
            }
        }

        private enum _eMenuQueryEditor
        {
            Execute = 0,
            ExecuteCurrentBlock,
            Dash2,
            Undo,
            Redo,
            Dash5,
            Cut,
            Copy,
            CopyTo,
            Paste,
            Delete,
            Dash11,
            FindAndReplace,
            Dash12,
            SelectAll,
            SelectCurrentBlock,
            Dash14,
            Comment,
            RemoveComment,
            Dash17,
            Indent,
            Unindent,
            Dash20,
            UpperCase,
            LowerCase,
            Dash23,
            SQLFormatter
        }

        private enum _eMenuGrid
        {
            CellViewer = 0,
            SingleRecordViewer,
            Dash0,
            SelectAll,
            Dash1,
            ExportAllDataToFile,
            ExportAllDataToFileScript,
            Dash3,
            CopyAllDataToClipboard,
            CopyAllDataToClipboardCurrentRow,
            Dash7,
            Copy,
            CopyAsQueryCondition,
            CopyWithColumnNames,
            CopyColumnNames,
            Dash12,
            FreezeColumn,
            UnfreezeColumn
        }

        public frmQuery()
        {
            InitializeComponent();
        }

        private void ApplyLocalizationSetting()
        {
            MyGlobal.ApplyLanguageInfo(this); //ApplyLocalizationSetting

            var toolTip1 = new ToolTip { ForeColor = Color.Blue, BackColor = Color.Gray, AutoPopDelay = 5000 };
            _sLangText = MyGlobal.GetLanguageString("Show Filter Row", "form", Name, "object", "chkShowFilterRow", "ToolTipText");
            toolTip1.SetToolTip(chkShowFilterRow, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Resize Columns Width based on Data Result automatically", "form", Name, "object", "chkSize", "ToolTipText");
            toolTip1.SetToolTip(chkSize, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Sort Data Result based on Column's Header automatically", "form", Name, "object", "chkSort", "ToolTipText");
            toolTip1.SetToolTip(chkSort, _sLangText);

            c1TrueDBGrid1.GroupStyle.Font = new Font("Microsoft JhengHei", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);
            c1TrueDBGrid1.GroupStyle.BackColor = Color.LightYellow;
            c1TrueDBGrid1.GroupByCaption = MyGlobal.GetLanguageString("Drag a column header here to group by that column", "form", Name, "object", "GroupingRowCaption", "Text"); ;

            if (MyLibrary.bDarkMode)
            {
                c1ThemeController1.SetTheme(c1TrueDBGrid1, "VS2013Dark");
                MyGlobal.SetGridVisualStyle(c1TrueDBGrid1, Convert.ToSingle(MyLibrary.sGridFontSize));

                txtSchemaFilter.Location = new Point(134, 1);

                if (MyGlobal.bChangeColorThemeNeedRestart)
                {
                    if (_fontSize == 0)
                    {
                        _fontSize = 10;
                    }

                    c1TrueDBGrid1.Styles["Normal"].Font = new Font(MyLibrary.sGridFontName, _fontSize * 1);
                }

                c1TrueDBGrid1.BackColor = ColorTranslator.FromHtml("#2D2D30");

                c1ThemeController1.SetTheme(c1StatusBar1, "ExpressionDark");
                c1ThemeController1.SetTheme(c1StatusBar2, "ExpressionDark");
                _cToolstripFocused = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);
                _cToolstripUnfocused = ColorTranslator.FromHtml("#2D2D30");

                lblInfoEditor.BackColor = ColorTranslator.FromHtml("#DFE9F5");

                if (MyLibrary.bEnableAutoReplace)
                {
                    c1ThemeController1.SetTheme(c1GridARInfo, "VS2013Dark");
                    c1GridARInfo.BackColor = ColorTranslator.FromHtml("#2D2D30");
                }

                MyGlobal.SetDockingTabColor(c1DockingTab1, ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveBackColor), ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveForeColor), ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabInactiveForeColor));
                MyGlobal.SetDockingTabColor(c1DockingTab2, ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveBackColor), ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveForeColor), ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabInactiveForeColor));
            }
            else
            {
                c1ThemeController1.SetTheme(c1TrueDBGrid1, "(default)");
                MyGlobal.SetGridVisualStyle(c1TrueDBGrid1, Convert.ToSingle(MyLibrary.sGridFontSize));
                c1TrueDBGrid1.BackColor = SystemColors.Control;

                c1ThemeController1.SetTheme(this, "(default)");
                c1ThemeController1.SetTheme(c1DockingTab1, "(default)");
                BackColor = SystemColors.Control;

                c1ThemeController1.SetTheme(c1StatusBar1, "(default)");
                c1ThemeController1.SetTheme(c1StatusBar2, "(default)");
                _cToolstripFocused = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);
                _cToolstripUnfocused = SystemColors.Control;

                lblInfoEditor.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorEditorBackground);

                if (MyLibrary.bEnableAutoReplace)
                {
                    c1ThemeController1.SetTheme(c1GridARInfo, "(default)");
                    c1GridARInfo.BackColor = ColorTranslator.FromHtml("#F0F0F0");
                }

                MyGlobal.SetDockingTabColor(c1DockingTab1, ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveBackColor), ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveForeColor), ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabInactiveForeColor));
                MyGlobal.SetDockingTabColor(c1DockingTab2, ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveBackColor), ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveForeColor), ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabInactiveForeColor));

                txtIndentWord.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
                chkShowFilterRow.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
                chkShowFilterRow.BackColor = Color.Transparent;
                chkSize.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
                chkSize.BackColor = Color.Transparent;
                chkSort.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
                chkSort.BackColor = Color.Transparent;
                cboFindGrid.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
                cboResultCopyFieldSeparator.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
                cboResultCopyQuotingWith.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
                chkShowColumnType.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
                chkShowColumnType.BackColor = Color.Transparent;
                chkRawDataMode.VisualStyle = C1.Win.C1Input.VisualStyle.Office2007Blue;
                chkRawDataMode.BackColor = Color.Transparent;
            }

            _sLangText = MyGlobal.GetLanguageString("Not yet committed or rollbacked!", "form", Name, "msg", "CommitPrompt", "Text");
            lblNotCommitYet.Tag = _sLangText;

            if (lblNotCommitYet.Visible)
            {
                UpdateNotCommitYetInfo(_sLangText); //ApplyLocalizationSetting
            }

            _sLangText = MyGlobal.GetLanguageString("Paging Query", "form", Name, "statusbarobject", "btnPaginationOn", "Text");
            btnPaginationOn.Text = _sLangText;

            _sLangText = MyGlobal.GetLanguageString("{qty} rows per page", "form", Name, "statusbarobject", "btnPaginationOn", "ToolTipText");
            _sLangText = _sLangText.Replace("{qty}", MyLibrary.sGridRowsPerPage);
            btnPaginationOn.ToolTip = _sLangText;

            _sLangText = MyGlobal.GetLanguageString("Paging Query", "form", Name, "statusbarobject", "btnPaginationOff", "Text");
            btnPaginationOff.Text = _sLangText;

            _sLangText = MyGlobal.GetLanguageString("{qty} rows per page", "form", Name, "statusbarobject", "btnPaginationOn", "ToolTipText");
            _sLangText = _sLangText.Replace("{qty}", MyLibrary.sGridRowsPerPage);
            btnPaginationOff.ToolTip = _sLangText;

            _sLangText = MyGlobal.GetLanguageString("Next Page", "form", Name, "statusbarobject", "btnNextPage", "ToolTipText");
            btnNextPage.ToolTip = _sLangText;

            _sLangText = MyGlobal.GetLanguageString("Appending Queries", "form", Name, "statusbarobject", "btnAppendingQueriesOn", "Text");
            btnAppendingQueriesOn.Text = _sLangText;

            _sLangText = MyGlobal.GetLanguageString("Appending Queries", "form", Name, "statusbarobject", "btnAppendingQueriesOn", "ToolTipText");
            btnAppendingQueriesOn.ToolTip = _sLangText;

            _sLangText = MyGlobal.GetLanguageString("Appending Queries", "form", Name, "statusbarobject", "btnAppendingQueriesOff", "Text");
            btnAppendingQueriesOff.Text = _sLangText;

            _sLangText = MyGlobal.GetLanguageString("Appending Queries", "form", Name, "statusbarobject", "btnAppendingQueriesOff", "ToolTipText");
            btnAppendingQueriesOff.ToolTip = _sLangText;

            chkSize.Text = chkSize.Text + @"(" + MyGlobal.sGridMaxWidth + @")";
            chkSize.Location = new Point(chkShowFilterRow.Left + chkShowFilterRow.Width, chkSize.Top);
            chkSort.Location = new Point(chkSize.Left + chkSize.Width, chkSort.Top);
            cboFindGrid.Location = new Point(chkSize.Left + chkSize.Width + lblFindGrid.Width + (chkSort.Visible ? 11 : 6), cboFindGrid.Top);

            lblSchemaFilter0.Text = lblSchemaFilter.Text;
            txtSchemaFilter.Location = new Point(lblSchemaFilter0.Left + lblSchemaFilter0.Width + 3, txtSchemaFilter.Top);

            chkShowColumnType.Location = new Point(cboFindGrid.Left + cboFindGrid.Width + 163, chkShowColumnType.Top);
            chkShowColumnType.Checked = MyLibrary.bGridShowColumnDataType;

            chkShowGroupingRow.Location = new Point(chkShowColumnType.Left + chkShowColumnType.Width + 1, chkShowGroupingRow.Top);

            chkRawDataMode.Location = new Point(chkShowGroupingRow.Left + chkShowGroupingRow.Width + 1, chkRawDataMode.Top);
            btnHelp_RawDataMode.Location = new Point(chkRawDataMode.Left + chkRawDataMode.Width - 3, chkRawDataMode.Top);

            btnSaveAs.Visible = MyLibrary.bShowSaveAsButton;
            txtIndentWord.Location = new Point(Convert.ToInt16(txtIndentWord.Tag.ToString()) + (MyLibrary.bShowSaveAsButton == false ? 0 : btnSaveAs.Width), txtIndentWord.Top);

            for (var i = 0; i < 100; i++)
            {
                lblSpace.Text = "".PadRight(i, ' ');

                if (lblSpace.Width <= chkShowFilterRow.Width + chkSize.Width + (chkSort.Visible ? chkSort.Width : -5))
                {
                    continue;
                }

                lblSpace.Text = "".PadRight(i, ' ');
                break;
            }

            c1TrueDBGrid1.AllowRowSizing = MyGlobal.GetKeyFromDictionary(MyGlobal.dicRowSizing, MyGlobal.sRowSizing) == "AllRows" ? RowSizingEnum.AllRows : RowSizingEnum.IndividualRows;

            c1TrueDBGrid1.HeadingStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridHeadingForeColor);

            _sLangText = MyGlobal.GetLanguageString(tabMessage.Tag.ToString(), "form", Name, "object", tabMessage.Name, "Text");
            tabMessage.Text = _sLangText;
            _sOriginal3TabName = "`" + _sLangText + "`";

            _sDataGridTabName = MyGlobal.GetLanguageString(tabDataGrid.Tag.ToString(), "form", Name, "object", tabDataGrid.Name, "Text");
            tabDataGrid.Text = _sDataGridTabName;
            _sOriginal3TabName = _sOriginal3TabName + _sDataGridTabName + "`";

            if (string.IsNullOrEmpty(_sDataGridTabNameOriginal))
            {
                _sDataGridTabNameOriginal = _sDataGridTabName;
            }

            if (c1DockingTab1.TabPages.Count > 3 && _sDataGridTabNameOriginal != _sDataGridTabName)
            {
                foreach (Control ctrl in c1DockingTab1.TabPages)
                {
                    ctrl.Text = ctrl.Text.Replace(_sDataGridTabNameOriginal, _sDataGridTabName);
                }

                _sDataGridTabNameOriginal = _sDataGridTabName;
            }

            _sLangText = MyGlobal.GetLanguageString(tabSQLHistory.Tag.ToString(), "form", Name, "object", tabSQLHistory.Name, "Text");
            tabSQLHistory.Text = _sLangText;
            _sOriginal3TabName = _sOriginal3TabName + _sLangText + "`";

            _sEditorLength = MyGlobal.GetLanguageString(lblEditorLength.Text, "form", Name, "statusbarobject", "lblEditorLength", "Text");
            _sEditorLines = MyGlobal.GetLanguageString(lblEditorLines.Text, "form", Name, "statusbarobject", "lblEditorLines", "Text");
            _sEditorLn = MyGlobal.GetLanguageString(lblEditorLn.Text, "form", Name, "statusbarobject", "lblEditorLn", "Text");
            _sEditorCol = MyGlobal.GetLanguageString(lblEditorCol.Text, "form", Name, "statusbarobject", "lblEditorCol", "Text");
            _sEditorPos = MyGlobal.GetLanguageString(lblEditorPos.Text, "form", Name, "statusbarobject", "lblEditorPos", "Text");
            _sEditorSel = MyGlobal.GetLanguageString(lblEditorSel.Text, "form", Name, "statusbarobject", "lblEditorSel", "Text");
            _sExecTime = MyGlobal.GetLanguageString(lblExecTime.Text, "form", Name, "statusbarobject", "lblExecTime", "Text");
            lblExecTime.Text = _sExecTime + @" 00:00.000";
            _sQueryTime = MyGlobal.GetLanguageString(lblQueryTime.Text, "form", Name, "statusbarobject", "lblQueryTime", "Text");
            lblQueryTime.Text = _sQueryTime + @" 00:00.000";
            _sRows = MyGlobal.GetLanguageString(lblRows.Text, "form", Name, "statusbarobject", "lblRows", "Text");

            lblRows.Text = @"0 " + _sRows;

            lblAverage.Text = MyGlobal.GetLanguageString(lblAverage.Text, "form", Name, "statusbarobject", "lblAverage", "Text");
            lblCount.Text = MyGlobal.GetLanguageString(lblCount.Text, "form", Name, "statusbarobject", "lblCount", "Text");
            lblSummary.Text = MyGlobal.GetLanguageString(lblSummary.Text, "form", Name, "statusbarobject", "lblSummary", "Text");

            UpdateEditorStatusBarLanguage();

            var a = Assembly.GetExecutingAssembly();

            //Begin:右鍵選單
            _cMenuMessageEditor = new ContextMenuStrip();
            _sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "menueditor", "SelectAll", "Text");
            _cMenuMessageEditor.Items.Add(_sLangText);
            _cMenuMessageEditor.Items[0].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.select all 16x16-2.ico"));
            ((ToolStripMenuItem) _cMenuMessageEditor.Items[0]).ShortcutKeys = Keys.Control | Keys.A;

            _cMenuMessageEditor.Items[0].Click += delegate
            {
                editorMessage.SelectionStart = 0;
                editorMessage.SelectionEnd = editorMessage.Text.Length;
            };

            _cMenuMessageEditor.Items.Add("-");

            _sLangText = MyGlobal.GetLanguageString("Copy", "form", Name, "menueditor", "Copy", "Text");
            _cMenuMessageEditor.Items.Add(_sLangText);
            _cMenuMessageEditor.Items[2].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy 16x16-2.ico"));
            ((ToolStripMenuItem) _cMenuMessageEditor.Items[2]).ShortcutKeys = Keys.Control | Keys.C;

            _cMenuMessageEditor.Items[2].Click += delegate
            {
                if (MyLibrary.bCopyAsHTML)
                {
                    Clipboard.Clear();
                    editorMessage.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                }
                else
                {
                    editorMessage.Copy();
                }
            };
            //End:右鍵選單

            //Begin:右鍵選單
            _cMenuSQLHistoryEditor = new ContextMenuStrip();
            _sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "menueditor", "SelectAll", "Text");
            _cMenuSQLHistoryEditor.Items.Add(_sLangText);
            _cMenuSQLHistoryEditor.Items[0].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.select all 16x16-2.ico"));
            ((ToolStripMenuItem) _cMenuSQLHistoryEditor.Items[0]).ShortcutKeys = Keys.Control | Keys.A;

            _cMenuSQLHistoryEditor.Items[0].Click += delegate
            {
                editorSQLHistory.SelectionStart = 0;
                editorSQLHistory.SelectionEnd = editorSQLHistory.Text.Length;
            };

            _cMenuSQLHistoryEditor.Items.Add("-");

            _sLangText = MyGlobal.GetLanguageString("Copy", "form", Name, "menueditor", "Copy", "Text");
            _cMenuSQLHistoryEditor.Items.Add(_sLangText);
            _cMenuSQLHistoryEditor.Items[2].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy 16x16-2.ico"));
            ((ToolStripMenuItem) _cMenuSQLHistoryEditor.Items[2]).ShortcutKeys = Keys.Control | Keys.C;

            _cMenuSQLHistoryEditor.Items[2].Click += delegate
            {
                if (MyLibrary.bCopyAsHTML)
                {
                    Clipboard.Clear();
                    editorSQLHistory.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                }
                else
                {
                    editorSQLHistory.Copy();
                }
            };
            //End:右鍵選單

            ApplyEditorMenu(); //ApplyLocalizationSetting()
            ApplyGridMenu(); //ApplyLocalizationSetting()
            MyGlobal.SetGridVisualStyle(c1GridSchemaBrowser, 10);
        }

        private void Form_Leave(object sender, EventArgs e)
        {
            HideACGrid();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //20230707
            lblAverage.Visible = false;
            lblAverageValue.Visible = false;
            lblCount.Visible = false;
            lblCountValue.Visible = false;
            lblSummary.Visible = false;
            lblSummaryValue.Visible = false;
            lblSeparator1.Visible = false;
            lblSeparator2.Visible = false;
            lblSeparator3.Visible = false;

            _sQueryEditorFontSize = MyLibrary.sQueryEditorFontSize;
            editor.MouseWheel += editor_MouseWheel;

            myFindReplace = new FindReplace.FindReplace();
            myFindReplace.Scintilla = editor;

            if (MyLibrary.bDarkMode && MyGlobal.dtSchema == null) //如果是深色模式，先置換 Editor 底色 (避免底色反白停留過久)
            {
                SqlStyler.sColorEditorBackground = MyLibrary.sColorEditorBackground;
                editor.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorEditorBackground);
                editor.Styler = new SqlStyler();

                editorMessage.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorEditorBackground);
                editorMessage.Styler = new SqlStyler();
            }

            c1GridAC4Period1.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorEditorBackground);
            c1GridAC4Space1.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorEditorBackground);
            c1GridAC4All.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorEditorBackground);

            var myForm = new frmInfo();

            if (MyGlobal.dtSchema == null)
            {
                var sMsg = MyGlobal.GetLanguageString("Please wait...", "Global", "Global", "msg", "PleaseWait", "Text");
                myForm.sCaption = sMsg;

                if (MyGlobal.bShowColumnInfo)
                {
                    sMsg = MyGlobal.GetLanguageString("Getting schema information (include all column information of Tables) ...", "Global", "Global", "msg", "GetSchemaInfoIncludeColumn", "Text");
                }
                else
                {
                    sMsg = MyGlobal.GetLanguageString("Getting schema information...", "Global", "Global", "msg", "GetSchemaInfo", "Text");
                }

                myForm.sInfo = sMsg;
                myForm.bMovingPosition = true; //20220706 加入此參數，螢幕置中後，再往上調整位置，避免蓋住 MessageBox！
                myForm.StartPosition = FormStartPosition.CenterScreen;
                //myForm.TopMost = true; //TopMost 會在「所有的程式」最上層顯示
                myForm.TopLevel = true; //20220720 改用 TopLevel，只在 JasonQuery 最上層顯示
                myForm.Show(this);
            }

            lblFindGrid.Tag = "";
            btnCancelQuery.Tag = "";
            btnNextPage.Tag = ""; //記錄頁數
            btnPaginationOff.Tag = ""; //記錄「分頁查詢」含變數的 SQL
            btnPaginationOn.Tag = MyLibrary.sGridRowsPerPage; //記錄「分頁查詢」每次查詢的筆數
            editor.Tag = ""; //multi selection 時判斷用的
            lblNotCommitYet.Tag = "";

            mnuFocusOnQueryEditor.Checked = MyGlobal.bAfterPasteFocusOnQueryEditor;
            mnuFocusOnDataGrid.Checked = !MyGlobal.bAfterPasteFocusOnQueryEditor;

            btnPaginationOn.Visible = MyLibrary.bGridPagingQuery;
            btnPaginationOff.Visible = !MyLibrary.bGridPagingQuery;
            btnAppendingQueriesOn.Visible = MyLibrary.bGridAppendingQueries;
            btnAppendingQueriesOff.Visible = !MyLibrary.bGridAppendingQueries;
            btnNextPage.Enabled = false; //未查詢前，一定是 false
            mnuPreviewCLOBData.Visible = MyGlobal.sDataSource == "Oracle";

            txtIndentWord.Tag = txtIndentWord.Left.ToString(); //記住 Left 值

            ApplyLocalizationSetting(); //Form_Load

            MyGlobal.SetGridVisualStyle(c1GridAC4Period1, 10); //Form_Load
            MyGlobal.SetGridVisualStyle(c1GridAC4Period2, 10); //Form_Load
            MyGlobal.SetGridVisualStyle(c1GridAC4Space1, 10); //Form_Load
            MyGlobal.SetGridVisualStyle(c1GridAC4Space2, 10); //Form_Load
            MyGlobal.SetGridVisualStyle(c1GridAC4All, 10); //Form_Load
            btnExpandCollapse.DropDownItemClicked += SplitButton_DropDownItemClicked;
            SetGridToolStripBackColor(false); //Form_Load
            chkRawDataMode.Checked = MyLibrary.bGridRawDataMode;

            switch (MyLibrary.sGridQuotingWith)
            {
                case "\"":
                    mnuResultCopyQuotingWithDoubleQuoting.Checked = true;
                    break;
                case "'":
                    mnuResultCopyQuotingWithSingleQuoting.Checked = true;
                    break;
                default:
                    //if(MyLibrary.sGridQuotingWith == "None")
                    mnuResultCopyQuotingWithNone.Checked = true;
                    break;
            }

            mnuResultCopyQuotingWith.Tag = MyLibrary.sGridQuotingWith;

            switch (MyLibrary.sGridFieldSeparator)
            {
                case ";":
                    mnuResultCopyFieldSeparatorSemicolon.Checked = true;
                    break;
                case "|":
                    mnuResultCopyFieldSeparatorI.Checked = true;
                    break;
                default: //","
                    mnuResultCopyFieldSeparatorComma.Checked = true;
                    break;
            }

            mnuResultCopyFieldSeparator.Tag = MyLibrary.sGridFieldSeparator;
            mnuPreviewCLOBData.Checked = MyGlobal.bPreviewCLOBData;

            InitDragDropFile();

            c1TrueDBGrid1.MouseWheel += c1TrueDBGrid1_MouseWheel;
            c1TrueDBGrid1.KeyDown += Detect_KeyDown;

            LoadSplitterData("L/R"); //讀取並設定：左側的左方區塊的大小
            LoadSplitterData("U/D"); //讀取並設定：右側的上方區塊的大小

            chkShowFilterRow.Checked = MyLibrary.bGridShowFilterRow;
            c1TrueDBGrid1.FilterBar = chkShowFilterRow.Checked;
            chkSort.Checked = MyLibrary.bGridSort;
            chkSize.Checked = MyLibrary.bGridResize;
            chkShowGroupingRow.Checked = MyLibrary.bGridShowGroupingRow;

            _rowHeight = c1TrueDBGrid1.RowHeight;
            _recSelWidth = c1TrueDBGrid1.RecordSelectorWidth;
            float.TryParse(MyLibrary.sGridFontSize, out _fontSize);

            c1DockingTab1.Focus();
            editor.Focus();

            //停用「右鍵選單」
            txtIndentWord.ContextMenuStrip = new ContextMenuStrip();

            //載入搜尋記錄
            //LoadFindList("Editor", cboFind); //Form_Load
            //LoadFindList("Editor", cboFindBox); //Form_Load
            LoadFindList("Grid", cboFindGrid); //Form_Load
            LoadReplaceList(); //Form_Load

            _bFormLoadFinish = true;

            //使用者是否有啟用 Auto Replace
            if (MyLibrary.bEnableAutoReplace)
            {
                CreateAndGetARInfoTable(); //frmQuery_Load
                splitContainer2.SplitterWidth = 2; //這裡不指定的話，它會變成 4
            }
            else
            {
                tabAutoReplace.TabVisible = false;
            }

            lblInfo.Text = "";
            lblInfo.Tag = "";
            c1StatusBar1.Tag = "";

            lblInfoEditor.Text = "";
            lblInfoEditor.Tag = "";
            c1StatusBar2.Tag = "";

            tmrlblInfo.Enabled = true;
            tmrlblInfoEditor.Enabled = true;

            c1TrueDBGrid1.AllowFilter = false;
            c1TrueDBGrid1.Filter += C1TrueDBGrid_Filter;

            //套用 Grid 外觀
            GridVisualStyle(c1TrueDBGrid1); //Form_Load

            GridFontAndBackgroundColor(c1TrueDBGrid1); //Form_Load
            MyGlobal.SetGridVisualStyle(c1TrueDBGrid1, Convert.ToSingle(MyLibrary.sGridFontSize));

            GridZoom(c1TrueDBGrid1); //Form_Load
            CreateNullTable(); //Form_Load
            c1TrueDBGrid1.DataSource = _dtNull;

            SetDockingTabControl(); //Form_Load

            //以下只在 Form_Load 生效
            txtIndentWord.Text = MyGlobal.iTabWidth.ToString();
            editor.TabWidth = MyGlobal.iTabWidth;
            editorMessage.TabWidth = MyGlobal.iTabWidth;
            editorSQLHistory.TabWidth = MyGlobal.iTabWidth;

            //以下只在 Form_Load 生效
            editor.IndentationGuides = MyLibrary.bShowIndentGuide ? ScintillaNET.IndentView.LookBoth : ScintillaNET.IndentView.None;

            if (MyGlobal.bDefaultTabSchemaBrowser)
            {
                c1DockingTab2.SelectedTab = tabSchemaBrowser;
                c1GridSchemaBrowser.Focus(); //方便滑鼠滾輪滾動
            }
            else if (MyLibrary.bEnableAutoReplace)
            {
                c1DockingTab2.SelectedTab = tabAutoReplace;
                c1GridARInfo.Focus(); //方便滑鼠滾輪滾動
            }
            
            editor.Enabled = false;
            c1DockingTab2.Enabled = false;

            MyGlobal.SetDockingTabColor(c1DockingTab2, ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveBackColor), ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveForeColor), ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabInactiveForeColor));

            if (MyGlobal.dtSchema != null)
            {
                MyGlobal.UpdateSchemaData(c1GridSchemaBrowser); //Form_Load 依資料庫類別，進行 Grid's Group、欄位隱藏、相關設定...
            }
            else
            {
                Application.UseWaitCursor = true;

                switch (MyGlobal.sDataSource)
                {
                    case "Oracle":
                        MyGlobal.UpdateSchemaData_Oracle(c1GridSchemaBrowser); //Form_Load, Oracle
                        break;
                    case "PostgreSQL":
                        MyGlobal.UpdateSchemaData_PostgreSQL(c1GridSchemaBrowser); //Form_Load, PostgreSQL
                        break;
                    case "SQL Server":
                        MyGlobal.UpdateSchemaData_SQLServer(c1GridSchemaBrowser, false); //Form_Load, SQL Server
                        break;
                    case "MySQL":
                        MyGlobal.UpdateSchemaData_MySQL(c1GridSchemaBrowser); //Form_Load, MySQL
                        break;
                    case "SQLite":
                        MyGlobal.UpdateSchemaData_SQLite(c1GridSchemaBrowser); //Form_Load, SQLite
                        break;
                    case "SQLCipher":
                        MyGlobal.UpdateSchemaData_SQLCipher(c1GridSchemaBrowser); //Form_Load, SQLCipher
                        break;
                }

                //使用者是否有啟用 Auto Complete
                if (MyLibrary.bEnableAutoComplete)
                {
                    BuildAutoCompleteMenu();
                }

                Application.UseWaitCursor = false;
            }

            switch (MyGlobal.sDataSource)
            {
                case "SQL Server":
                    {
                        if (string.IsNullOrEmpty(MyGlobal.sDBConnectionDatabase))
                        {
                            //20220428 此處直接變更 MyGlobal.sDBConnectionDatabase = "master"
                            //20220711 此處不直接變更 MyGlobal.sDBConnectionDatabase = "master"，因為登入帳號可能沒有 master 權限
                            //TransferValueToMainForm("updatedatabaseinfo`master"); //Form_Load, SQL Server
                            //MyGlobal.sDBConnectionDatabase = "master";

                            //20220803 沒有指定 database，顯示提示訊息
                            _sLangText = MyGlobal.GetLanguageString("Please use the USE statement to select a particular database first.。", "form", Name, "msg", "SelectDatabaseFirst", "Text");
                            SetLabelInfoEditor(_sLangText, Color.Red);
                        }
                        else
                        {
                            //傳遞資訊至 MainForm，更新 Database 資訊
                            TransferValueToMainForm("updatedatabaseinfo`" + MyGlobal.sDBConnectionDatabase); //Form_Load, SQL Server
                        }

                        if (MyGlobal.dtTFVTP == null)
                        {
                            MyGlobal.RefreshDataForSchemaSearch(); //Form_Load, SQL Server
                        }

                        break;
                    }
                case "PostgreSQL":
                    //傳遞資訊至 MainForm，更新 Database 資訊
                    TransferValueToMainForm("updatedatabaseinfo`" + MyGlobal.sDBConnectionDatabase); //Form_Load, PostgreSQL
                    break;
                case "MySQL":
                    {
                        if (string.IsNullOrEmpty(MyGlobal.sDBConnectionDatabase))
                        {
                            //20220428 此處不直接變更 MyGlobal.sDBConnectionDatabase = "master"，避免後續進入 frmSchemaBrowser 只會列出 master data
                            //TransferValueToMainForm("updatedatabaseinfo`"); //Form_Load, MySQL

                            //20220803 沒有指定 database，顯示提示訊息
                            _sLangText = MyGlobal.GetLanguageString("Please use the USE statement to select a particular database first.", "form", Name, "msg", "SelectDatabaseFirst", "Text");
                            SetLabelInfoEditor(_sLangText, Color.Red);
                        }
                        else
                        {
                            //傳遞資訊至 MainForm，更新 Database 資訊
                            TransferValueToMainForm("updatedatabaseinfo`" + MyGlobal.sDBConnectionDatabase); //Form_Load, MySQL
                        }

                        if (MyGlobal.dtTFVTP == null)
                        {
                            MyGlobal.RefreshDataForSchemaSearch(); //Form_Load, MySQL
                        }

                        break;
                    }
            }

            c1GridARInfo.Cursor = Cursors.Default;
            c1GridSchemaBrowser.Cursor = Cursors.Default;

            MyGlobal.SetGridVisualStyle(c1GridSchemaBrowser, 10);

            //不允許調整 Schema Browser 的列高
            c1GridSchemaBrowser.AllowRowSizing = RowSizingEnum.None;

            DisconnectDatabase(); //Form_Load, 載入 Schema 後，再中斷連線

            AutoResizeGridColumnWidth(); //Form_Load, for Schema Browser & Auto Replace

            //套用 Editor 外觀
            ApplyEditorSetting(); //Form_Load

            myForm.Dispose();

            c1DockingTab2.Enabled = true;
            editor.Enabled = true;
            editor.Focus();
        }

        private void SplitButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            switch ((e.ClickedItem.Tag ?? Tag.ToString()).ToString())
            {
                case "mnuExpandAll":
                    {
                        for (var i = 0; i < c1GridSchemaBrowser.Splits[_iii].Rows.Count; i++)
                        {
                            if (c1GridSchemaBrowser.Splits[_iii].Rows[i].RowType != RowTypeEnum.DataRow)
                                c1GridSchemaBrowser.ExpandGroupRow(i);

                            Application.DoEvents();
                        }

                        break;
                    }
                case "mnuCollapseAll":
                    {
                        for (var i = 0; i < c1GridSchemaBrowser.Splits[_iii].Rows.Count; i++)
                        {
                            if (c1GridSchemaBrowser.Splits[_iii].Rows[i].RowType != RowTypeEnum.DataRow)
                                c1GridSchemaBrowser.CollapseGroupRow(i);

                            Application.DoEvents();
                        }

                        //展開指定的節點 (-1:標題列，所以，從 0 開始算，要展開哪一個)
                        c1GridSchemaBrowser.ExpandGroupRow(0); //展開 0 的結點

                        break;
                    }
            }

            Cursor = Cursors.Default;
        }

        private int c1GridAC4Period_Filter(string sCondition)
        {
            var dataView = _dtAC4Period.DefaultView;

            try
            {
                if (dataView.RowFilter == sCondition)
                {
                    return dataView.Count;
                }

                var condition = sCondition;

                if (condition.Length != 0)
                {
                    for (var i = 0; i < c1GridAC4Period1.Splits[0].DisplayColumns.Count; i++)
                    {
                        if (!condition.Contains($"[{c1GridAC4Period1.Columns[i].Caption}]"))
                        {
                            continue;
                        }

                        var paramIndex = condition.IndexOf('\'', condition.IndexOf($"[{c1GridAC4Period1.Columns[i].Caption}]", StringComparison.Ordinal)) + 1;
                        condition = condition.Insert(paramIndex, "*");
                        condition = condition.Replace("**", "*");
                    }
                }

                condition = condition.Replace("LIKE '", "LIKE '*").Replace("**", "*");

                dataView.RowFilter = condition;

                if (dataView.Count == 0)
                {
                    dataView.RowFilter = "[C] LIKE '*'";
                }

                //20220907 將前面幾個字母相符的排在最前面
                #region
                var dtSorted = dataView.ToTable();
                dtSorted.Columns.Add("Sort", typeof(int));
                var j = -1000;
                var sFilterKeyword = MyGlobal.GetStringBetween(sCondition, "'", "'").Replace("*", "").ToUpper();

                for (var i = 0; i < dtSorted.Rows.Count; i++)
                {
                    if (dtSorted.Rows[i]["C"].ToString().ToUpper().Substring(0, sFilterKeyword.Length) == sFilterKeyword)
                    {
                        dtSorted.Rows[i]["Sort"] = j;
                        j++;
                    }
                    else
                    {
                        dtSorted.Rows[i]["Sort"] = i;
                    }
                }

                dataView = dtSorted.DefaultView;
                dataView.Sort = "Sort, C, D";
                dtSorted = dataView.ToTable();
                dtSorted.Columns.Remove("Sort");
                #endregion

                c1GridAC4Period1.DataSource = dtSorted;

                return dtSorted.Rows.Count;
            }
            catch (Exception)
            {
                return 0; //按下 Ctrl+J 可能會進到這個例外錯誤
            }
        }

        private void c1GridAC4Period_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Up || c1GridAC4Period1.Row != 0)
            {
                return;
            }

            _bKeyUpFromACGrid = true;
            editor.Focus();
        }

        private void c1GridAC4Period_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                c1GridAC4Period_PasteColumnName(); //c1GridAC4Period_KeyPress
            }
            else if (e.KeyChar == 8)
            {
                editor.DeleteRange(Math.Min(_iPeriodPos2, editor.CurrentPosition) - 1, 1);
                editor.CurrentPosition = Math.Min(_iPeriodPos2, editor.CurrentPosition) - 1;
                editor.SelectionStart = Math.Min(_iPeriodPos2, editor.CurrentPosition);
                editor.SelectionEnd = editor.SelectionStart;
                editor.Focus();

                if (editor.CurrentPosition < _iPeriodPos)
                {
                    c1GridAC4Period1.Visible = false;
                }
            }
            else if (MyGlobal.IsEngAlphabetOrNumber(e.KeyChar.ToString(), "_"))
            {
                editor.CurrentPosition = Math.Min(_iPeriodPos2, editor.CurrentPosition);
                editor.SelectionStart = editor.CurrentPosition;
                editor.SelectionEnd = editor.SelectionStart;
                editor.ReplaceSelection(e.KeyChar.ToString());
                editor.Focus();
            }
        }

        private void c1GridAC4Period_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            c1GridAC4Period_PasteColumnName(); //c1GridAC4Period_MouseDoubleClick
        }

        private void c1GridAC4Period_PasteColumnName()
        {
            var sCellText = c1GridAC4Period1[c1GridAC4Period1.Row, 0].ToString();
            var iTemp = 0;

            //20220726 editor.Text 最後加上一個空白，避免觸發時游標已是在最尾端而計算有誤
            for (var i = _iPeriodPos; i < (editor.Text + " ").Length; i++)
            {
                if (!MyGlobal.IsEngAlphabetOrNumber((editor.Text + " ").Substring(i, 1), "_"))
                {
                    if (iTemp == 0)
                    {
                        iTemp = Math.Min(_iPeriodPos2, editor.CurrentPosition);
                    }

                    break;
                }

                iTemp = i + 1;
            }

            editor.SelectionStart = _iPeriodPos;
            editor.SelectionEnd = iTemp;
            editor.ReplaceSelection(sCellText);
            c1GridAC4Period1.Visible = false;
            editor.Focus();
        }

        private int c1GridAC4All_Filter(string sCondition)
        {
            var dataView = MyGlobal.dtAC4All.DefaultView;

            try
            {
                if (dataView.RowFilter == sCondition)
                {
                    return dataView.Count;
                }

                var condition = sCondition;

                if (condition.Length != 0)
                {
                    for (var i = 0; i < c1GridAC4All.Splits[0].DisplayColumns.Count; i++)
                    {
                        if (!condition.Contains($"[{c1GridAC4All.Columns[i].Caption}]"))
                        {
                            continue;
                        }

                        var paramIndex = condition.IndexOf('\'', condition.IndexOf($"[{c1GridAC4All.Columns[i].Caption}]", StringComparison.Ordinal)) + 1;
                        condition = condition.Insert(paramIndex, "*");
                        condition = condition.Replace("**", "*");
                    }
                }

                condition = condition.Replace("LIKE '", "LIKE '*").Replace("**", "*");

                dataView.RowFilter = condition;

                if (dataView.Count == 0)
                {
                    dataView.RowFilter = "[ObjectName] LIKE '*'";
                }

                //AC4All 的來源有很多個，故依名稱排序
                dataView.Sort = "ObjectName, ObjectSource";

                //20220907 將前面幾個字母相符的排在最前面
                #region
                var dtSorted = dataView.ToTable();
                dtSorted.Columns.Add("Sort", typeof(int));
                var j = -1000;
                var sFilterKeyword = MyGlobal.GetStringBetween(sCondition, "'", "'").Replace("*", "").ToUpper();

                for (var i = 0; i < dtSorted.Rows.Count; i++)
                {
                    if (dtSorted.Rows[i]["ObjectName"].ToString().ToUpper().Substring(0, sFilterKeyword.Length) == sFilterKeyword)
                    {
                        dtSorted.Rows[i]["Sort"] = j;
                        j++;
                    }
                    else
                    {
                        dtSorted.Rows[i]["Sort"] = i;
                    }
                }

                dataView = dtSorted.DefaultView;
                dataView.Sort = "Sort, ObjectName, ObjectSource";
                dtSorted = dataView.ToTable();
                dtSorted.Columns.Remove("Sort");
                #endregion

                c1GridAC4All.DataSource = dtSorted;

                return dataView.Count;
            }
            catch (Exception)
            {
                return 0; //按下 Ctrl+J 可能會進到這個例外錯誤
            }
        }

        private void c1GridAC4All_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Up || c1GridAC4All.Row != 0)
            {
                return;
            }

            _bKeyUpFromACGrid = true;
            editor.Focus();
        }

        private void c1GridAC4All_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                c1GridAC4All_PasteColumnName(); //c1GridAC4All_KeyPress
            }
            else if (e.KeyChar == 8)
            {
                editor.DeleteRange(Math.Min(_iAllPos2, editor.CurrentPosition) - 1, 1);
                editor.CurrentPosition = Math.Min(_iAllPos2, editor.CurrentPosition) - 1;
                editor.SelectionStart = Math.Min(_iAllPos2, editor.CurrentPosition);
                editor.SelectionEnd = editor.SelectionStart;
                editor.Focus();

                if (editor.CurrentPosition < _iAllPos)
                {
                    c1GridAC4All.Visible = false;
                }
            }
            else if (MyGlobal.IsEngAlphabetOrNumber(e.KeyChar.ToString(), "_"))
            {
                editor.CurrentPosition = Math.Min(_iAllPos2, editor.CurrentPosition);
                editor.SelectionStart = editor.CurrentPosition;
                editor.SelectionEnd = editor.SelectionStart;
                editor.ReplaceSelection(e.KeyChar.ToString());
                editor.Focus();
            }
        }

        private void c1GridAC4All_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            c1GridAC4All_PasteColumnName(); //c1GridAC4All_MouseDoubleClick
        }

        private void c1GridAC4All_PasteColumnName()
        {
            var sCellText = c1GridAC4All[c1GridAC4All.Row, 0].ToString();
            var iTemp = 0;

            //後面加上一個空白，避免已是結尾處而漏算一個字元
            for (var i = _iAllPos; i < (editor.Text + " ").Length; i++)
            {
                if (!MyGlobal.IsEngAlphabetOrNumber((editor.Text + " ").Substring(i, 1), "_"))
                {
                    if (iTemp == 0)
                    {
                        iTemp = Math.Min(_iAllPos2, editor.CurrentPosition);
                    }

                    break;
                }

                iTemp = i + 1;
            }

            var iStart = 0; //往前找起始字母
            var iEnd = 0; //往後找結束字母

            for (var i = _iAllPos - 1; i >= 0; i--)
            {
                if (MyGlobal.IsEngAlphabetOrNumber((editor.Text + " ").Substring(i, 1), "_"))
                {
                    iStart = i;
                }
                else
                {
                    break;
                }
            }

            for (var i = _iAllPos; i < (editor.Text + " ").Length; i++)
            {
                if (MyGlobal.IsEngAlphabetOrNumber((editor.Text + " ").Substring(i, 1), "_"))
                {
                    iEnd = i;
                }
                else
                {
                    break;
                }
            }

            editor.SelectionStart = Math.Min(_iAllPos, iStart);
            editor.SelectionEnd = Math.Max(iTemp, iEnd);
            editor.ReplaceSelection(sCellText);
            c1GridAC4All.Visible = false;
            editor.Focus();
        }

        private int c1GridAC4Space_Filter(string sCondition)
        {
            var dataView = _dtAC4Space.DefaultView;

            try
            {
                if (dataView.RowFilter == sCondition)
                {
                    return dataView.Count;
                }

                var condition = sCondition;

                if (condition.Length != 0)
                {
                    for (var i = 0; i < c1GridAC4Space1.Splits[0].DisplayColumns.Count; i++)
                    {
                        if (!condition.Contains($"[{c1GridAC4Space1.Columns[i].Caption}]"))
                        {
                            continue;
                        }

                        var paramIndex = condition.IndexOf('\'', condition.IndexOf($"[{c1GridAC4Space1.Columns[i].Caption}]", StringComparison.Ordinal)) + 1;
                        condition = condition.Insert(paramIndex, "*");
                        condition = condition.Replace("**", "*");
                    }
                }

                condition = condition.Replace("LIKE '", "LIKE '*").Replace("**", "*");

                dataView.RowFilter = condition;

                if (dataView.Count == 0)
                {
                    dataView.RowFilter = "[C] LIKE '*'";
                }

                //20220907 將前面幾個字母相符的排在最前面
                #region
                var dtSorted = dataView.ToTable();
                dtSorted.Columns.Add("Sort", typeof(int));
                var j = -1000;
                var sFilterKeyword = MyGlobal.GetStringBetween(sCondition, "'", "'").Replace("*", "").ToUpper();

                for (var i = 0; i < dtSorted.Rows.Count; i++)
                {
                    if (dtSorted.Rows[i]["C"].ToString().ToUpper().Substring(0, sFilterKeyword.Length) == sFilterKeyword)
                    {
                        dtSorted.Rows[i]["Sort"] = j;
                        j++;
                    }
                    else
                    {
                        dtSorted.Rows[i]["Sort"] = i;
                    }
                }

                dataView = dtSorted.DefaultView;
                dataView.Sort = "Sort, C, D";
                dtSorted = dataView.ToTable();
                dtSorted.Columns.Remove("Sort");
                #endregion

                c1GridAC4Space1.DataSource = dtSorted;

                return dtSorted.Rows.Count;
            }
            catch (Exception)
            {
                return 0; //按下 Ctrl+J 可能會進到這個例外錯誤
            }
        }

        private void c1GridAC4Space_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Up || c1GridAC4Space1.Row != 0)
            {
                return;
            }

            _bKeyUpFromACGrid = true;
            editor.Focus();
        }

        private void c1GridAC4Space_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                c1GridAC4Space_PasteColumnName(); //c1GridAC4Space_KeyPress
            }
            else if (e.KeyChar == 8)
            {
                editor.DeleteRange(Math.Min(_iSpacePos2, editor.CurrentPosition) - 1, 1);
                editor.CurrentPosition = Math.Min(_iSpacePos2, editor.CurrentPosition) - 1;
                editor.SelectionStart = Math.Min(_iSpacePos2, editor.CurrentPosition);
                editor.SelectionEnd = editor.SelectionStart;
                editor.Focus();

                if (editor.CurrentPosition < _iSpacePos)
                {
                    c1GridAC4Space1.Visible = false;
                }
            }
            else if (MyGlobal.IsEngAlphabetOrNumber(e.KeyChar.ToString(), "_"))
            {
                editor.CurrentPosition = Math.Min(_iSpacePos2, editor.CurrentPosition);
                editor.SelectionStart = editor.CurrentPosition;
                editor.SelectionEnd = editor.SelectionStart;
                editor.ReplaceSelection(e.KeyChar.ToString());
                editor.Focus();
            }
        }

        private void c1GridAC4Space_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            c1GridAC4Space_PasteColumnName(); //c1GridAC4Space_MouseDoubleClick
        }

        private void c1GridAC4Space_PasteColumnName()
        {
            var sCellText = c1GridAC4Space1[c1GridAC4Space1.Row, 0].ToString();
            var iTemp = 0;

            //20220726 後面加上一個空白，避免已是結尾處而漏算一個字元
            for (var i = _iSpacePos; i < (editor.Text + " ").Length; i++)
            {
                if (!MyGlobal.IsEngAlphabetOrNumber((editor.Text + " ").Substring(i, 1), "_"))
                {
                    if (iTemp == 0)
                    {
                        iTemp = Math.Min(_iSpacePos2, editor.CurrentPosition);
                    }

                    break;
                }

                iTemp = i + 1;
            }

            editor.SelectionStart = _iSpacePos;
            editor.SelectionEnd = iTemp;
            editor.ReplaceSelection(sCellText);
            c1GridAC4Space1.Visible = false;
            editor.Focus();
        }

        private void C1TrueDBGrid_Filter(object sender, FilterEventArgs e)
        {
            var c1Grid = GetWhichGrid();
            var dataView = ((DataTable)c1Grid.DataSource).DefaultView;

            if (dataView.RowFilter == e.Condition)
            {
                return;
            }

            var condition = e.Condition;

            if (condition.Length != 0)
            {
                condition = e.Condition;

                for (var i = 0; i < c1Grid.Splits[_SplitsNum].DisplayColumns.Count; i++)
                {
                    if (!condition.Contains($"[{c1Grid.Columns[i].Caption}]"))
                    {
                        continue;
                    }

                    var paramIndex = condition.IndexOf('\'', condition.IndexOf($"[{c1Grid.Columns[i].Caption}]", StringComparison.Ordinal)) + 1;
                    condition = condition.Insert(paramIndex, "*");
                }
            }

            dataView.RowFilter = condition;
        }

        private void CreateAndGetARInfoTable(DataTable dt = null, bool bReserve = false)
        {
            const string sHideFields = "`PID`";

            _lstGridHeaderAR = new List<string>();

            _lstGridHeaderAR.Add("PID"); //Hide
            _sLangText = MyGlobal.GetLanguageString("Keyword", "form", "frmOptions", "gridheader", "Keyword", "Text");
            _lstGridHeaderAR.Add(_sLangText);
            _sColKeywordName = _sLangText;
            _sLangText = MyGlobal.GetLanguageString("Replacement", "form", "frmOptions", "gridheader", "Replacement", "Text");
            _lstGridHeaderAR.Add(_sLangText);
            _sColReplacementName = _sLangText;

            _dtARInfo = new DataTable();

            _dtARInfo.Columns.Add(_lstGridHeaderAR[(int)eColAR.PID]);
            _dtARInfo.Columns.Add(_lstGridHeaderAR[(int)eColAR.Keyword]);
            _dtARInfo.Columns.Add(_lstGridHeaderAR[(int)eColAR.Replacement]);

            if (bReserve)
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dt.Rows.Count; iRow++)
                    {
                        _rowARInfo = _dtARInfo.NewRow();
                        _rowARInfo[_lstGridHeaderAR[(int)eColAR.PID]] = dt.Rows[iRow]["PID"].ToString();

                        _rowARInfo[(int)eColAR.Keyword] = dt.Rows[iRow][(int)eColAR.Keyword].ToString();
                        _rowARInfo[(int)eColAR.Replacement] = dt.Rows[iRow][(int)eColAR.Replacement].ToString();

                        _dtARInfo.Rows.Add(_rowARInfo);
                    }
                }
            }
            else
            {
                dt = DBCommon.ExecQuery("SELECT PID, AttributeValue FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'AutoReplaceConfig' AND AttributeName = 'AutoReplace'");

                if (dt.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dt.Rows.Count; iRow++)
                    {
                        _rowARInfo = _dtARInfo.NewRow();
                        _rowARInfo[_lstGridHeaderAR[(int)eColAR.PID]] = dt.Rows[iRow]["PID"].ToString();

                        var sAttributeValue = dt.Rows[iRow]["AttributeValue"].ToString();

                        string[] sInfo;

                        if (sAttributeValue.IndexOf(MyGlobal.sSeparator3s, StringComparison.Ordinal) != -1)
                        {
                            //20210916 開始改用新的分隔符號
                            sInfo = sAttributeValue.Split(new[] { MyGlobal.sSeparator3s }, StringSplitOptions.None);

                            _rowARInfo[_lstGridHeaderAR[(int)eColAR.Keyword]] = sInfo[0];
                            _rowARInfo[_lstGridHeaderAR[(int)eColAR.Replacement]] = sInfo[1];
                        }
                        else
                        {
                            //支援 20210915 以前的舊寫法
                            sInfo = sAttributeValue.Split(new[] { "`" }, StringSplitOptions.None);

                            _rowARInfo[_lstGridHeaderAR[(int)eColAR.Keyword]] = sInfo[0];
                            _rowARInfo[_lstGridHeaderAR[(int)eColAR.Replacement]] = sInfo[1];
                        }

                        _dtARInfo.Rows.Add(_rowARInfo);
                    }
                }
            }

            if (bReserve == false)
            { 
                //20191001 新增 10 個空白列，使用者可以自訂資料
                for (var iRow = 0; iRow < 10; iRow++)
                {
                    _rowARInfo = _dtARInfo.NewRow();
                    _rowARInfo[_lstGridHeaderAR[(int)eColAR.PID]] = iRow + 101; //1`Sample2`{NAME}`Jason`1

                    _rowARInfo[_lstGridHeaderAR[(int)eColAR.Keyword]] = "";
                    _rowARInfo[_lstGridHeaderAR[(int)eColAR.Replacement]] = "";

                    _dtARInfo.Rows.Add(_rowARInfo);
                }
            }

            c1GridARInfo.DataSource = _dtARInfo;

            foreach (C1DisplayColumn col in c1GridARInfo.Splits[_iii].DisplayColumns)
            {
                if (sHideFields.Contains("`" + col.Name + "`"))
                {
                    col.Visible = false;
                    col.Frozen = true;
                }
                else
                {
                    if (col.Name == _sColKeywordName)
                    {
                        try
                        {
                            col.AutoSize();
                        }
                        catch (Exception)
                        {
                            col.Width = 500;
                        }

                        if (col.Width > 500)
                        {
                            col.Width = 500;
                        }
                    }

                    if (col.Name == _sColReplacementName)
                    {
                        col.Width = 250;
                    }
                }
            }

            MyGlobal.SetGridVisualStyle(c1GridARInfo, 10);

            if (bReserve == false)
            {
                UpdateAutoReplaceDictionary(); //CreateAndGetARInfoTable
            }

            //調整 AR 的列高
            c1GridARInfo.AllowRowSizing = RowSizingEnum.IndividualRows;
            c1GridARInfo.Splits[0].ColumnCaptionHeight = 20;

            for (var r = 0; r < c1GridARInfo.Splits[_iii].Rows.Count; r++)
            {
                c1GridARInfo.Splits[_iii].Rows[r].AutoSize();

                if (c1GridARInfo.Splits[_iii].Rows[r].Height > 48)
                {
                    c1GridARInfo.Splits[_iii].Rows[r].Height = 48; //最多顯示 3 列資料
                }

                if (c1GridARInfo.Splits[_iii].Rows[r].Height <= 15)
                {
                    c1GridARInfo.Splits[_iii].Rows[r].Height = 16;
                }

                c1GridARInfo.Splits[_iii].Rows[r].Height += 1;
            }
        }

        private void ApplyEditorSetting()
        {
            editor.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
            editor.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editor.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));

            editorSQLHistory.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
            editorSQLHistory.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            //editorSQLHistory.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));
            editorMessage.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
            editorMessage.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);

            //設定 WordWrap 按鈕
            var bValue = !MyLibrary.bWordWrap;
            btnWordWrap.Visible = bValue;
            btnWordWrap2.Visible = !bValue;

            editor.WrapMode = (MyLibrary.bWordWrap == false) ? ScintillaNET.WrapMode.None : ScintillaNET.WrapMode.Word;
            editor.WrapVisualFlags = (MyLibrary.bWordWrapVisualFlags_Start ? ScintillaNET.WrapVisualFlags.Start : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_End ? ScintillaNET.WrapVisualFlags.End : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_Margin ? ScintillaNET.WrapVisualFlags.Margin : ScintillaNET.WrapVisualFlags.None);

            //設定 ShowAllCharacters 按鈕
            btnShowAllCharacters.Visible = !MyLibrary.bShowAllCharacters;
            btnShowAllCharacters2.Visible = MyLibrary.bShowAllCharacters;
            editor.ViewEol = MyLibrary.bShowAllCharacters;
            editor.ViewWhitespace = MyLibrary.bShowAllCharacters ? ScintillaNET.WhitespaceMode.VisibleAlways : ScintillaNET.WhitespaceMode.Invisible;

            editor.Zoom = Convert.ToInt16(MyLibrary.sQueryEditorZoom);

            ApplySqlStyler();

            //<--Begin, 設定 Bookmark 樣式
            var margin = editor.Margins[BOOKMARK_MARGIN];
            //editor.Margins[BOOKMARK_MARGIN].Width = 10;
            margin.Width = 15;
            margin.Mask = 0;
            margin.Sensitive = true;
            margin.Type = ScintillaNET.MarginType.Symbol;
            margin.Mask = ScintillaNET.Marker.MaskAll;
            margin.Cursor = ScintillaNET.MarginCursor.Arrow;

            var marker = editor.Markers[BOOKMARK_MARKER];
            var sStyle = MyGlobal.GetKeyFromDictionary(MyGlobal.dicBookmarkStyle, MyGlobal.sBookmarkStyle);

            switch (sStyle)
            {
                case "Arrow":
                    marker.Symbol = ScintillaNET.MarkerSymbol.Arrow; //箭頭
                    break;
                case "Circle":
                    marker.Symbol = ScintillaNET.MarkerSymbol.Circle; //圓形
                    break;
                case "RoundRect":
                    marker.Symbol = ScintillaNET.MarkerSymbol.RoundRect; //圓角矩形
                    break;
                case "SmallRect":
                    marker.Symbol = ScintillaNET.MarkerSymbol.SmallRect; //正方形
                    break;
                default:
                    marker.Symbol = ScintillaNET.MarkerSymbol.ShortArrow; //小箭頭
                    break;
            }

            marker.SetBackColor(ColorTranslator.FromHtml(MyLibrary.sColorBookmarkBackground));
            marker.SetForeColor(Color.Transparent);
            //editor.Lines[editor.CurrentLine].MarkerAdd(BOOKMARK_MARKER);
            //editor.MarginClick += scintilla_MarginClick;
            //-->End, 設定 Bookmark 樣式
        }

        private void AddComment() //加 -- 註解
        {
            var sAllText = editor.Text;
            var sSelectedText = editor.SelectedText;
            var iComment = 0;
            var iNoneSpaceStart = 0; //iNoneSpaceStart: 第一個非空白字元的位置 (以平均來看，不是以第一列來看)

            //取得使用者選取的範圍之起始、結束位置
            var iStart = editor.SelectionStart;
            var iEnd = editor.SelectionEnd;

            // Loop through array.
            var iStart2 = 0;
            var iEnd2 = 0;

            //根據使用者的選取範圍，重新取得第一列的最前、最後一列的最後位置
            GetNewStartAndEnd(sAllText, iStart, iEnd, ref iStart2, ref iEnd2, ref iNoneSpaceStart, "AddComment"); //AddComment

            //將選取範圍重新定義為「第一列的最前，一直到最後一列的最後」
            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2;

            //判斷是否完全沒有選取文字 或是「只有一列」
            if (iStart == iEnd || sSelectedText.Length == sSelectedText.Replace("\r\n", "").Length)
            {
                //沒有選取文字！默認為選取該列，所以直接 +2 (最前面加上 --)
                iComment += 2;
            }

            //重新選取文字
            sSelectedText = editor.SelectedText;

            var parts = sSelectedText.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var sResult = "";

            for (var i = 0; i < parts.Length; i++)
            {
                if (parts[i].Length == iNoneSpaceStart)
                {
                    sResult += parts[i].Substring(0, iNoneSpaceStart) + "--";
                }
                else
                {
                    sResult += parts[i].Substring(0, iNoneSpaceStart) + "--" + parts[i].Substring(iNoneSpaceStart, parts[i].Length - iNoneSpaceStart);
                }

                if (i >= parts.Length - 1)
                {
                    continue; //不是選取區的最後一列，後面加上換行
                }

                sResult += "\r\n";
                iComment += 2; //每一列 +2
            }

            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2;

            editor.ReplaceSelection(sResult);

            sAllText = editor.Text;

            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2 + iComment;

            //加上 Comment 符號後，重新取得「選取範圍」
            GetNewStartAndEnd(sAllText, iStart2, iEnd2 + iComment, ref iStart2, ref iEnd2, ref iNoneSpaceStart); //AddComment

            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2 + 1; //此處要再加 1？不加 1，有時會少選 1；加 1，多按幾次 AddComment，就會多選到 \r 了。(註一)

            //以下的 Select() + SendKeys(按下組合鍵Shift END)，上面的(註一)問題就可以解決了
            editor.Select();
            SendKeys.Send("+{END}");
        }

        private void RemoveComment() //去除 -- 註解
        {
            var sAllText = editor.Text;
            var sSelectedText = editor.SelectedText;
            var iComment = 0;
            var iNoneSpaceStart = 0; //iNoneSpaceStart: 第一個非空白字元的位置 (以平均來看，不是以第一列來看)

            //取得使用者選取的範圍之起始、結束位置
            var iStart = editor.SelectionStart;
            var iEnd = editor.SelectionEnd;

            var iStart2 = 0;
            var iEnd2 = 0;

            //根據使用者的選取範圍，重新取得第一列的最前、最後一列的最後位置
            GetNewStartAndEnd(sAllText, iStart, iEnd, ref iStart2, ref iEnd2, ref iNoneSpaceStart); //RemoveComment

            //將選取範圍重新定義為「第一列的最前，一直到最後一列的最後」
            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2;

            //判斷是否完全沒有選取文字 或是「只有一列」
            if (iStart == iEnd || sSelectedText.Length == sSelectedText.Replace("\r\n", "").Length)
            {
                //沒有選取文字！默認為選取該列，所以直接 -2 (最前面去掉 --)
                iComment -= 2;
            }

            //重新選取文字
            sSelectedText = editor.SelectedText;

            var parts = sSelectedText.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var sResult = "";

            for (var i = 0; i < parts.Length; i++)
            {
                var sTemp = parts[i];

                for (var j = 0; j < sTemp.Length; j++)
                {
                    if (sTemp.Substring(j, 1) == " ")
                    {
                        continue;
                    }

                    switch (sTemp.Substring(j, 1))
                    {
                        case @"-" when sTemp.Substring(j + 1, 1) == "-":
                            sResult += sTemp.Substring(0, j) + sTemp.Substring(j + 2, sTemp.Length - (j + 2));
                            iComment -= 2; //兩個 dash 符號
                            break;
                        case @"-" when sTemp.Substring(j + 1, 1) != "-":
                            sResult += sTemp.Substring(0, j) + sTemp.Substring(j + 1, sTemp.Length - (j + 1));
                            iComment -= 1; //一個 dash 符號
                            break;
                        default:
                        {
                            if (sTemp.Substring(j, 1) != "-")
                            {
                                sResult += sTemp; //第一個非空白，不是 dash 符號，不處理
                            }

                            break;
                        }
                    }

                    break;
                }

                if (i < parts.Length - 1) //不是選取區的最後一列，後面加上換行符號
                {
                    sResult += "\r\n";
                }
            }

            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2;

            editor.ReplaceSelection(sResult);

            sAllText = editor.Text;

            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2 + iComment;

            //加上 Comment 符號後，重新取得「選取範圍」
            GetNewStartAndEnd(sAllText, iStart2, iEnd2 + iComment, ref iStart2, ref iEnd2, ref iNoneSpaceStart); //RemoveComment

            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2 + 1; //此處要再加 1？不加 1，有時會少選 1；加 1，多按幾次 RemoveComment，就會多選到 \r 了。(註一)

            //以下的 Select() + SendKeys(按下組合鍵Shift END)，上面的(註一)問題就可以解決了
            editor.Select();
            SendKeys.Send("+{END}");
        }

        private void GetNewStartAndEnd(string sText, int iStart, int iEnd, ref int iStart2, ref int iEnd2, ref int iNoneSpaceStart, string sMode = "")
        {
            var sNoneSpace = "";
            var array = sText.ToCharArray();
            char letter;

            //往前找結束位置
            for (var i = iStart - 1; i >= 0; i--)
            {
                letter = array[i];

                iStart2 = i;

                if (sMode.ToUpper() == "SELECTBLOCK")
                {
                    if (letter.ToString() != "\n")
                    {
                        continue;
                    }

                    if (i - 3 <= 0 || array[i - 1].ToString() != "\r" || array[i - 2].ToString() != "\n" || array[i - 3].ToString() != "\r")
                    {
                        continue;
                    }

                    iStart2 = i + 1;
                    break;
                }

                if (letter.ToString() != "\n")
                {
                    continue; //往前找到第一個換行符號
                }

                iStart2 = i + 1;
                break;
            }

            //往後找結束位置
            for (var i = iEnd; i < array.Length; i++)
            {
                letter = array[i];

                iEnd2 = i;

                if (sMode.ToUpper() == "SELECTBLOCK")
                {
                    if (letter.ToString() == "\r") //往後找到最後一個換行符號
                    {
                        if (i + 3 >= array.Length)
                        {
                            iEnd2 = array.Length - 1;
                            break;
                        }

                        if (i + 3 >= array.Length || array[i + 1].ToString() != "\n" || array[i + 2].ToString() != "\r" || array[i + 3].ToString() != "\n")
                        {
                            continue;
                        }

                        iEnd2 = i;
                        break;
                    }

                    if (i == array.Length - 1)
                    {
                        iEnd2 = i + 1; //最後一個字元不是換行符號
                    }
                }
                else
                {
                    if (letter.ToString() != "\r" && letter.ToString() != "\n")
                    {
                        continue; //往後找到最後一個換行符號
                    }

                    iEnd2 -= 1;
                    break;
                }
            }

            if (iEnd2 == 0 && iEnd == array.Length || iEnd2 < iEnd)
            {
                iEnd2 = iEnd - 1;
            }

            if (sMode.ToUpper() != "ADDCOMMENT")
            {
                return;
            }

            //將選取範圍重新定義為「第一列的最前，一直到最後一列的最後」
            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2;

            var sSelectedText = editor.SelectedText;

            var parts = sSelectedText.Split(new[] { "\r\n" }, StringSplitOptions.None);

            iNoneSpaceStart = 0;

            foreach (var sLine in parts)
            {
                if (string.IsNullOrEmpty(sLine.Trim())) //如果整列都是空白
                {
                    sNoneSpace = sNoneSpace + sLine.Length + ",";
                }
                else
                {
                    for (var j = 0; j < sLine.Length - 1; j++)
                    {
                        if (sLine.Substring(j, 1) == " ")
                        {
                            continue;
                        }

                        sNoneSpace = sNoneSpace + j + ",";
                        break;
                    }
                }
            }

            sNoneSpace = sNoneSpace.Substring(0, sNoneSpace.Length - 1);
            var aryNoneSpace = sNoneSpace.Split(new[] { "," }, StringSplitOptions.None);
            NumericalSort(aryNoneSpace);

            iNoneSpaceStart = Convert.ToInt32(aryNoneSpace[0]);
        }

        private static void NumericalSort(string[] ar)
        {
            var rgx = new Regex("([^0-9]*)([0-9]+)");

            Array.Sort(ar, (a, b) =>
            {
                var ma = rgx.Matches(a);
                var mb = rgx.Matches(b);

                for (var i = 0; i < ma.Count; ++i)
                {
                    var ret = string.Compare(ma[i].Groups[1].Value, mb[i].Groups[1].Value, StringComparison.Ordinal);

                    if (ret != 0)
                    {
                        return ret;
                    }

                    ret = int.Parse(ma[i].Groups[2].Value) - int.Parse(mb[i].Groups[2].Value);

                    if (ret != 0)
                    {
                        return ret;
                    }
                }

                return 0;
            });
        }

        private void editor_MouseDown(object sender, MouseEventArgs e)
        {
            var bValue = !string.IsNullOrEmpty(editor.SelectedText); //判斷是否有選取文字，決定功能表項目可不可用

            switch (e.Button)
            {
                case MouseButtons.Right:
                    HideACGrid(false); //滑鼠右鍵，一律隱藏 AC 清單！
                    break;
                case MouseButtons.Left:
                    {
                        if (c1GridAC4Period1.Visible)
                        {
                            if (_iPeriodPos != editor.CurrentPosition)
                            {
                                if (editor.CurrentPosition == _iPeriodPos - 1)
                                {
                                    //游標停在句點上面
                                    c1GridAC4Period1.Visible = false;
                                }
                                else if (editor.CurrentPosition < _iPeriodPos)
                                {
                                    c1GridAC4Period1.Visible = false;
                                }
                                else //20220715 還要往右找，游標所在位置是否為下拉清單裡面的相關字串(或關鍵字)
                                {
                                    var iTemp = _iPeriodPos;

                                    for (var i = _iPeriodPos; i < editor.Text.Length; i++)
                                    {
                                        if (!MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_"))
                                        {
                                            break;
                                        }

                                        iTemp = i + 1;
                                    }

                                    if (editor.CurrentPosition > iTemp)
                                    {
                                        c1GridAC4Period1.Visible = false;
                                    }
                                }
                            }
                        }
                        else if (c1GridAC4Space1.Visible)
                        {
                            if (_iSpacePos == editor.CurrentPosition)
                            {
                                return;
                            }

                            if (editor.CurrentPosition == _iSpacePos - 1)
                            {
                                //游標停在空白上面
                                c1GridAC4Space1.Visible = false;
                            }
                            else if (editor.CurrentPosition < _iSpacePos)
                            {
                                c1GridAC4Space1.Visible = false;
                            }
                            else //20220715 還要往右找，游標所在位置是否為下拉清單裡面的相關字串(或關鍵字)
                            {
                                var iTemp = _iSpacePos;

                                for (var i = _iSpacePos; i < editor.Text.Length; i++)
                                {
                                    if (!MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_"))
                                    {
                                        break;
                                    }

                                    iTemp = i + 1;
                                }

                                if (editor.CurrentPosition > iTemp)
                                {
                                    c1GridAC4Space1.Visible = false;
                                }
                            }
                        }
                        else if (c1GridAC4All.Visible)
                        {
                            if (_iAllPos == editor.CurrentPosition)
                            {
                                return;
                            }

                            var iStart = 0; //往前找起始字母
                            var iEnd = 0; //往後找結束字母

                            for (var i = _iAllPos - 1; i >= 0; i--)
                            {
                                if (MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_"))
                                {
                                    iStart = i;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            for (var i = _iAllPos - 1; i < editor.Text.Length; i++)
                            {
                                if (MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_"))
                                {
                                    iEnd = i;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            if (editor.CurrentPosition < iStart || editor.CurrentPosition > iEnd)
                            {
                                c1GridAC4All.Visible = false;
                            }
                        }

                        return;
                    }
            }

            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            var a = Assembly.GetExecutingAssembly();

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Execute].Enabled = btnQuery.Enabled;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Execute].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.execute 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.ExecuteCurrentBlock].Enabled = btnExecuteCurrentBlock.Enabled;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.ExecuteCurrentBlock].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.execute block 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Undo].Enabled = editor.CanUndo;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Undo].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.undo 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Redo].Enabled = editor.CanRedo;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Redo].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.redo 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Cut].Enabled = bValue;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Cut].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.cut 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Copy].Enabled = bValue;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Copy].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy 16x16-2.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.CopyTo].Enabled = false;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.CopyTo].Visible = false;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Paste].Enabled = editor.CanPaste;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Paste].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.paste 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Delete].Enabled = bValue;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Delete].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.delete 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.FindAndReplace].Enabled = !string.IsNullOrEmpty(editor.Text);
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.FindAndReplace].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.replace 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.SelectAll].Enabled = !string.IsNullOrEmpty(editor.Text);
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.SelectAll].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.select all 16x16-2.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.SelectCurrentBlock].Enabled = !string.IsNullOrEmpty(editor.Text);
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.SelectCurrentBlock].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.select block 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Comment].Enabled = bValue;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Comment].Image = MyLibrary.bDarkMode ? new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.comment_b 16x16.ico")) : new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.comment 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.RemoveComment].Enabled = bValue;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.RemoveComment].Image = MyLibrary.bDarkMode ? new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.uncomment_b 16x16.ico")) : new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.uncomment 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Indent].Enabled = bValue;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Indent].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.indent 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Unindent].Enabled = bValue;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Unindent].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.unindent 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.UpperCase].Enabled = bValue;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.UpperCase].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.uppercase 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.LowerCase].Enabled = bValue;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.LowerCase].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.lowercase 16x16.ico"));
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.SQLFormatter].Enabled = bValue;
            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.SQLFormatter].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.format 16x16.ico"));

            editor.ContextMenuStrip = _cMenuQueryEditor;

            if (MyLibrary.bDarkMode)
            {
                _cMenuQueryEditor.BackColor = ColorTranslator.FromHtml("#2D2D30");
                _cMenuQueryEditor.ForeColor = Color.White;
                _cMenuQueryEditor.RenderMode = ToolStripRenderMode.System;
                //_cMenuQueryEditor.ShowImageMargin = false;
            }

            _cMenuQueryEditor.Show(editor, new Point(e.X, e.Y));
        }

        private void ApplyEditorMenu() //套用 Editor 右鍵功能表
        {
            _lstMenuEditor = new List<string>();

            _sLangText = MyGlobal.GetLanguageString("Execute", "form", Name, "menueditor", "Execute", "Text");
            _lstMenuEditor.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Execute Current Block", "form", Name, "menueditor", "ExecuteCurrentBlock", "Text");
            _lstMenuEditor.Add(_sLangText);
            _lstMenuEditor.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Undo", "form", Name, "menueditor", "Undo", "Text");
            _lstMenuEditor.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Redo", "form", Name, "menueditor", "Redo", "Text");
            _lstMenuEditor.Add(_sLangText);
            _lstMenuEditor.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Cut", "form", Name, "menueditor", "Cut", "Text");
            _lstMenuEditor.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Copy", "form", Name, "menueditor", "Copy", "Text");
            _lstMenuEditor.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("CopyTo...", "form", Name, "menueditor", "CopyTo", "Text");
            _lstMenuEditor.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Paste", "form", Name, "menueditor", "Paste", "Text");
            _lstMenuEditor.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Delete", "form", Name, "menueditor", "Delete", "Text");
            _lstMenuEditor.Add(_sLangText);
            _lstMenuEditor.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Find", "form", Name, "menueditor", "FindAndReplace", "Text");
            _lstMenuEditor.Add(_sLangText);
            _lstMenuEditor.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "menueditor", "SelectAll", "Text");
            _lstMenuEditor.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Select Current Block", "form", Name, "menueditor", "SelectCurrentBlock", "Text");
            _lstMenuEditor.Add(_sLangText);
            _lstMenuEditor.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Comment", "form", Name, "menueditor", "Comment", "Text");
            _lstMenuEditor.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Un-Comment", "form", Name, "menueditor", "Uncomment", "Text");
            _lstMenuEditor.Add(_sLangText);
            _lstMenuEditor.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Indent", "form", Name, "menueditor", "Indent", "Text");
            _lstMenuEditor.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Un-Indent", "form", Name, "menueditor", "Unindent", "Text");
            _lstMenuEditor.Add(_sLangText);
            _lstMenuEditor.Add("-");
            _sLangText = MyGlobal.GetLanguageString("UPPER Case", "form", Name, "menueditor", "UpperCase", "Text");
            _lstMenuEditor.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("lower Case", "form", Name, "menueditor", "LowerCase", "Text");
            _lstMenuEditor.Add(_sLangText);
            _lstMenuEditor.Add("-");
            _sLangText = MyGlobal.GetLanguageString("SQL Formatter", "form", Name, "menueditor", "SQLFormatter", "Text");
            _lstMenuEditor.Add(_sLangText);

            _cMenuQueryEditor = new ContextMenuStrip(); //Editor 右鍵選單

            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Execute]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.ExecuteCurrentBlock]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Dash2]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Undo]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Redo]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Dash5]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Cut]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Copy]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.CopyTo]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Paste]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Delete]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Dash11]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.FindAndReplace]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Dash12]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.SelectAll]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.SelectCurrentBlock]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Dash14]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Comment]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.RemoveComment]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Dash17]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Indent]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Unindent]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Dash20]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.UpperCase]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.LowerCase]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.Dash23]);
            _cMenuQueryEditor.Items.Add(_lstMenuEditor[(int)_eMenuQueryEditor.SQLFormatter]);

            ((ToolStripMenuItem) _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Execute]).ShortcutKeys = Keys.F5;
            ((ToolStripMenuItem) _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.ExecuteCurrentBlock]).ShortcutKeys = Keys.Control | Keys.Enter;
            ((ToolStripMenuItem) _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Undo]).ShortcutKeys = Keys.Control | Keys.Z;
            ((ToolStripMenuItem) _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Redo]).ShortcutKeys = Keys.Control | Keys.Y;
            ((ToolStripMenuItem) _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Cut]).ShortcutKeys = Keys.Control | Keys.X;
            ((ToolStripMenuItem) _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Copy]).ShortcutKeys = Keys.Control | Keys.C;
            ((ToolStripMenuItem) _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Paste]).ShortcutKeys = Keys.Control | Keys.V;
            ((ToolStripMenuItem) _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.FindAndReplace]).ShortcutKeys = Keys.Control | Keys.F;
            ((ToolStripMenuItem) _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.SelectAll]).ShortcutKeys = Keys.Control | Keys.A;
            ((ToolStripMenuItem) _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.SelectCurrentBlock]).ShortcutKeys = Keys.Control | Keys.B;
            ((ToolStripMenuItem) _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.UpperCase]).ShortcutKeys = Keys.Control | Keys.Shift | Keys.U;
            ((ToolStripMenuItem) _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.LowerCase]).ShortcutKeys = Keys.Control | Keys.U;

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Execute].Click += delegate
            {
                btnQuery.PerformClick();
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.ExecuteCurrentBlock].Click += delegate
            {
                btnExecuteCurrentBlock.PerformClick();
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.SelectAll].Click += delegate
            {
                editor.SelectionStart = 0;
                editor.SelectionEnd = editor.Text.Length;
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.SelectCurrentBlock].Click += delegate
            {
                SelectBlock(); //右鍵選單
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Undo].Click += delegate
            {
                editor.Undo();
                lblInfo.Text = "";
                CheckEditorContent(); //右鍵選單 Undo
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Redo].Click += delegate
            {
                editor.Redo();
                CheckEditorContent(); //右鍵選單 Redo
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Cut].Click += delegate
            {
                if (!string.IsNullOrEmpty(editor.Tag.ToString()))
                {
                    editor.Cut();
                    CopyTextToClipboard(editor.Tag.ToString(), "Cut");
                }
                else
                {
                    //20221102 判斷是否為「重複按下 Ctrl+C」
                    if (!string.IsNullOrEmpty(_sSelectedTextDoubleClick) && string.IsNullOrEmpty(editor.SelectedText.ToUpper().Replace(_sSelectedTextDoubleClick.ToUpper(), "")))
                    {
                        CopyTextToClipboard(_sSelectedTextDoubleClick, "033");
                    }
                    else
                    {
                        editor.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                    }

                    editor.Clear();
                }

                CheckEditorContent(); //右鍵選單 Cut
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Copy].Click += delegate
            {
                if (MyLibrary.bCopyAsHTML && string.IsNullOrEmpty(editor.Tag.ToString()))
                {
                    try
                    {
                        Clipboard.Clear();

                        //20221102 判斷是否為「重複按下 Ctrl+C」
                        if (!string.IsNullOrEmpty(_sSelectedTextDoubleClick) && string.IsNullOrEmpty(editor.SelectedText.ToUpper().Replace(_sSelectedTextDoubleClick.ToUpper(), "")))
                        {
                            CopyTextToClipboard(_sSelectedTextDoubleClick, "034");
                        }
                        else
                        {
                            editor.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                        }
                    }
                    catch (Exception ex)
                    {
                        _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                        MessageBox.Show(_sLangText + @"-c02", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(editor.Tag.ToString()))
                    {
                        CopyTextToClipboard(editor.Tag.ToString(), "01");
                    }
                    else
                    {
                        try
                        {
                            //20221102 判斷是否為「重複按下 Ctrl+C」
                            if (!string.IsNullOrEmpty(_sSelectedTextDoubleClick) && string.IsNullOrEmpty(editor.SelectedText.ToUpper().Replace(_sSelectedTextDoubleClick.ToUpper(), "")))
                            {
                                CopyTextToClipboard(_sSelectedTextDoubleClick, "035");
                            }
                            else
                            {
                                editor.Copy();
                            }
                        }
                        catch (Exception ex)
                        {
                            _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                            MessageBox.Show(_sLangText + @"-c04", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }

                CheckEditorContent(); //右鍵選單 Copy
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.CopyTo].Click += delegate
            {
                //尚未完成
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Paste].Click += delegate
            {
                try
                {
                    var sOriginalText = Clipboard.GetText();
                    CopyTextToClipboard(sOriginalText.Replace("\t", "    "), "Paste");
                    editor.Paste();
                }
                catch (Exception)
                {
                    //
                }

                CheckEditorContent(); //右鍵選單 Paste
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Delete].Click += delegate
            {
                //DeleteRange → 類似 Cut 方法
                //editor.DeleteRange(editor.SelectionStart, editor.SelectedText.Length);

                editor.ReplaceSelection(""); //置換內容
                editor.SelectionStart = editor.SelectionStart; //重新選取內容
                editor.SelectionEnd = editor.SelectionStart;
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.FindAndReplace].Click += delegate
            {
                HideACGrid(false); //顯示搜尋視窗時，強制隱藏 AC 清單
                myFindReplace.ShowFind(); //右鍵選單 Find And Replace
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Comment].Click += delegate
            {
                AddComment(); //右鍵選單 Comment
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.RemoveComment].Click += delegate
            {
                RemoveComment(); //右鍵選單 Removecomment
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Indent].Click += delegate
            {
                Indent(); //右鍵選單
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.Unindent].Click += delegate
            {
                Unindent(); //右鍵選單
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.UpperCase].Click += delegate
            {
                TransferStringUpperLower(true); //右鍵選單
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.LowerCase].Click += delegate
            {
                TransferStringUpperLower(false); //右鍵選單
            };

            _cMenuQueryEditor.Items[(int)_eMenuQueryEditor.SQLFormatter].Click += delegate
            {
                uSQLFormatter(); //右鍵選單
            };
        }

        private void ApplyGridMenu() //套用 Grid 功能表
        {
            _lstMenuGrid = new List<string>();

            _lstMenuGrid.Add("");
            _lstMenuGrid.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Copy Column Name", "form", Name, "menugrid", "CopyColumnName", "Text");
            _lstMenuGrid.Add(_sLangText);
            _lstMenuGrid.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Sort", "form", Name, "menugrid", "Sort", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Sort by NUMBER", "form", Name, "menugrid", "SortByNumber", "Text");
            _lstMenuGrid.Add(_sLangText);

            _cMenuGridHeader = new ContextMenuStrip();

            _cMenuGridHeader.Items.Add(_lstMenuGrid[0]);
            _cMenuGridHeader.Items.Add(_lstMenuGrid[1]); //分隔線
            _cMenuGridHeader.Items.Add(_lstMenuGrid[2]);
            _cMenuGridHeader.Items.Add(_lstMenuGrid[3]); //分隔線
            _cMenuGridHeader.Items.Add(_lstMenuGrid[4]);
            _cMenuGridHeader.Items.Add(_lstMenuGrid[5]);

            _cMenuGridHeader.Items[2].Click += delegate
            {
                CopyTextToClipboard(_cMenuGridHeader.Items[2].Tag.ToString(), "CCN"); //Grid右鍵選單, Copy Column Name
            };

            _cMenuGridHeader.Items[4].Click += delegate
            {
                SortDataGrid(Convert.ToInt16(_cMenuGridHeader.Items[0].Tag.ToString())); //Grid右鍵選單, Sort
            };

            _cMenuGridHeader.Items[5].Click += delegate
            {
                SortDataGrid(Convert.ToInt16(_cMenuGridHeader.Items[0].Tag.ToString()), true); //Grid右鍵選單, Sort by NUMBER
            };

            _lstMenuGrid = new List<string>();

            _sLangText = MyGlobal.GetLanguageString("Cell Viewer", "form", Name, "menugrid", "CellViewer", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Single Record Viewer", "form", Name, "menugrid", "SingleRecordViewer", "Text");
            _lstMenuGrid.Add(_sLangText);
            _lstMenuGrid.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "menugrid", "SelectAll", "Text");
            _lstMenuGrid.Add(_sLangText);
            _lstMenuGrid.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Export All Data to File (Excel/CSV...)", "form", Name, "menugrid", "ExportAllDataToFile", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Export All Data to File (as \"Insert Into\" Script)", "form", Name, "menugrid", "ExportAllDataToFileScript", "Text");
            _lstMenuGrid.Add(_sLangText);

            _lstMenuGrid.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Export All Data to Clipboard (as \"Insert Into\" Script)", "form", Name, "menugrid", "CopyAllDataToClipboard", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Export Current Row Data to Clipboard (as \"Insert Into\" Script)", "form", Name, "menugrid", "CopyAllDataToClipboardCurrentRow", "Text");
            _lstMenuGrid.Add(_sLangText);

            _lstMenuGrid.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Copy", "form", Name, "menugrid", "Copy", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Copy as Query Condition", "form", Name, "menugrid", "CopyAsQueryCondition", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Copy with Column Name(s)", "form", Name, "menugrid", "CopyWithColumnName", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Copy Column Name(s)", "form", Name, "menugrid", "CopyColumnName", "Text");
            _lstMenuGrid.Add(_sLangText);
            _lstMenuGrid.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Freeze Column", "form", Name, "menugrid", "FreezeColumn", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Unfreeze Column", "form", Name, "menugrid", "UnfreezeColumn", "Text");
            _lstMenuGrid.Add(_sLangText);

            _cMenuGrid = new ContextMenuStrip();
            var a = Assembly.GetExecutingAssembly();

            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.CellViewer]);
            _cMenuGrid.Items[(int)_eMenuGrid.CellViewer].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.cellviewer.ico"));
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.SingleRecordViewer]);
            _cMenuGrid.Items[(int)_eMenuGrid.SingleRecordViewer].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.literature 16x16.ico"));
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.Dash0]);
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.SelectAll]);
            _cMenuGrid.Items[(int)_eMenuGrid.SelectAll].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.select all 16x16-2.ico"));
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.Dash1]);
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.ExportAllDataToFile]);
            _cMenuGrid.Items[(int)_eMenuGrid.ExportAllDataToFile].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.export 16x16.ico"));
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.ExportAllDataToFileScript]);
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.Dash3]);
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.CopyAllDataToClipboard]);
            _cMenuGrid.Items[(int)_eMenuGrid.CopyAllDataToClipboard].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy2script 16x16.ico"));
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.CopyAllDataToClipboardCurrentRow]);
            _cMenuGrid.Items[(int)_eMenuGrid.CopyAllDataToClipboardCurrentRow].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy2script current 16x16.ico"));
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.Dash7]);
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.Copy]);
            _cMenuGrid.Items[(int)_eMenuGrid.Copy].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy 16x16-2.ico"));
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.CopyAsQueryCondition]);
            _cMenuGrid.Items[(int)_eMenuGrid.CopyAsQueryCondition].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copyin 16x16.ico"));
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.CopyWithColumnNames]);
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.CopyColumnNames]);
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.Dash12]);
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.FreezeColumn]);
            _cMenuGrid.Items[(int)_eMenuGrid.FreezeColumn].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.freeze 16x16.ico"));
            _cMenuGrid.Items.Add(_lstMenuGrid[(int)_eMenuGrid.UnfreezeColumn]);
            _cMenuGrid.Items[(int)_eMenuGrid.UnfreezeColumn].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.unfreeze 16x16.ico"));

            _cMenuGrid.Items[(int)_eMenuGrid.CellViewer].Click += delegate
            {
                CellViewer(); //ApplyGridMenu, Grid右鍵選單
            };

            _cMenuGrid.Items[(int)_eMenuGrid.SingleRecordViewer].Click += delegate
            {
                SingleRecordViewer(); //ApplyGridMenu, Grid右鍵選單
            };

            ((ToolStripMenuItem) _cMenuGrid.Items[(int)_eMenuGrid.SelectAll]).ShortcutKeys = Keys.Control | Keys.A;

            _cMenuGrid.Items[(int)_eMenuGrid.SelectAll].Click += delegate
            {
                c1TrueDBGrid1.SelectedRows.Clear();

                for (var i = 0; i < c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count; i++)
                {
                    c1TrueDBGrid1.SelectedRows.Add(i);
                }
            };

            _cMenuGrid.Items[(int)_eMenuGrid.ExportAllDataToFile].Click += delegate
            {
                ExportToFile(); //ApplyGridMenu, 右鍵選單ExportToExcel()
            };

            _sLangText = MyGlobal.GetLanguageString("DateTime field(s) will be converted using TO_DATE", "form", Name, "msg", "DateTime2ToDate", "Text");
            ((ToolStripMenuItem)_cMenuGrid.Items[(int)_eMenuGrid.ExportAllDataToFileScript]).DropDownItems.Add(_sLangText);

            ((ToolStripMenuItem)_cMenuGrid.Items[(int)_eMenuGrid.ExportAllDataToFileScript]).DropDownItems[0].Click += delegate
            {
                ArrangeData4AllData("ExportAllDataToFileScript", true); //ApplyGridMenu, Grid右鍵選單, 日期使用 TO_DATE(), InsertInto Script
            };

            _sLangText = MyGlobal.GetLanguageString("DateTime field(s) will be converted to STRING", "form", Name, "msg", "DateTime2String", "Text");
            ((ToolStripMenuItem)_cMenuGrid.Items[(int)_eMenuGrid.ExportAllDataToFileScript]).DropDownItems.Add(_sLangText);

            ((ToolStripMenuItem)_cMenuGrid.Items[(int)_eMenuGrid.ExportAllDataToFileScript]).DropDownItems[1].Click += delegate
            {
                ArrangeData4AllData("ExportAllDataToFileScript"); //ApplyGridMenu, Grid右鍵選單, 日期使用 string, InsertInto Script
            };

            _sLangText = MyGlobal.GetLanguageString("DateTime field(s) will be converted using TO_DATE", "form", Name, "msg", "DateTime2ToDate", "Text");
            ((ToolStripMenuItem)_cMenuGrid.Items[(int)_eMenuGrid.CopyAllDataToClipboard]).DropDownItems.Add(_sLangText);

            ((ToolStripMenuItem)_cMenuGrid.Items[(int)_eMenuGrid.CopyAllDataToClipboard]).DropDownItems[0].Click += delegate
            {
                ArrangeData4AllData("CopyAllDataToClipboard", true); //ApplyGridMenu, Grid右鍵選單, 日期使用 TO_DATE(), InsertInto Script
            };

            _sLangText = MyGlobal.GetLanguageString("DateTime field(s) will be converted to STRING", "form", Name, "msg", "DateTime2String", "Text");
            ((ToolStripMenuItem)_cMenuGrid.Items[(int)_eMenuGrid.CopyAllDataToClipboard]).DropDownItems.Add(_sLangText);

            ((ToolStripMenuItem)_cMenuGrid.Items[(int)_eMenuGrid.CopyAllDataToClipboard]).DropDownItems[1].Click += delegate
            {
                ArrangeData4AllData("CopyAllDataToClipboard"); //ApplyGridMenu, Grid右鍵選單, 日期使用 string, InsertInto Script
            };

            _sLangText = MyGlobal.GetLanguageString("DateTime field(s) will be converted using TO_DATE", "form", Name, "msg", "DateTime2ToDate", "Text");
            ((ToolStripMenuItem)_cMenuGrid.Items[(int)_eMenuGrid.CopyAllDataToClipboardCurrentRow]).DropDownItems.Add(_sLangText);

            ((ToolStripMenuItem)_cMenuGrid.Items[(int)_eMenuGrid.CopyAllDataToClipboardCurrentRow]).DropDownItems[0].Click += delegate
            {
                ArrangeData4AllData("CopyAllDataToClipboardCurrentRow", true); //ApplyGridMenu, Grid右鍵選單, 日期使用 TO_DATE(), InsertInto Script
            };

            _sLangText = MyGlobal.GetLanguageString("DateTime field(s) will be converted to STRING", "form", Name, "msg", "DateTime2String", "Text");
            ((ToolStripMenuItem)_cMenuGrid.Items[(int)_eMenuGrid.CopyAllDataToClipboardCurrentRow]).DropDownItems.Add(_sLangText);

            ((ToolStripMenuItem)_cMenuGrid.Items[(int)_eMenuGrid.CopyAllDataToClipboardCurrentRow]).DropDownItems[1].Click += delegate
            {
                ArrangeData4AllData("CopyAllDataToClipboardCurrentRow"); //ApplyGridMenu, Grid右鍵選單, 日期使用 string, InsertInto Script
            };

            ((ToolStripMenuItem) _cMenuGrid.Items[(int)_eMenuGrid.Copy]).ShortcutKeys = Keys.Control | Keys.C;

            _cMenuGrid.Items[(int)_eMenuGrid.Copy].Click += delegate
            {
                ArrangeData("Copy"); //ApplyGridMenu, Grid右鍵選單
            };

            _cMenuGrid.Items[(int)_eMenuGrid.CopyAsQueryCondition].Click += delegate
            {
                ArrangeData("CopyAsQueryCondition"); //ApplyGridMenu, Grid右鍵選單
            };

            _cMenuGrid.Items[(int)_eMenuGrid.CopyWithColumnNames].Click += delegate
            {
                ArrangeData("CopyWithColumnNames"); //ApplyGridMenu, Grid右鍵選單
            };

            _cMenuGrid.Items[(int)_eMenuGrid.CopyColumnNames].Click += delegate
            {
                ArrangeData("CopyColumnNames"); //ApplyGridMenu, Grid右鍵選單
            };

            _cMenuGrid.Items[(int)_eMenuGrid.FreezeColumn].Click += delegate
            {
                FrozenColumn(); //ApplyGridMenu, Grid右鍵選單
            };

            _cMenuGrid.Items[(int)_eMenuGrid.UnfreezeColumn].Click += delegate
            {
                FrozenColumn(false); //ApplyGridMenu, Grid右鍵選單
            };

            //初始值為 false: 使用過 Frozen 後再啟用
            _cMenuGrid.Items[(int)_eMenuGrid.UnfreezeColumn].Enabled = false;
        }

        private void SortDataGrid(int iCol, bool bByNumber = false)
        {
            string sSortMode;
            var sTag = "";
            var c1Grid = GetWhichGrid();
            var sNewCol = DateTime.Now.ToString("yyyyMMddHHmmssfff"); //DataTable 最後面新增的暫時欄位名稱
            Thread.Sleep(12);
            var sNewCol0 = DateTime.Now.ToString("yyyyMMddHHmmssfff"); //使用者選定要排序的欄位，其原始欄位名稱(因為有可能包含換行符號或特殊符號，造成排序異常，故需要一個臨時的名稱)
            var dc = c1Grid.Splits[_SplitsNum].DisplayColumns[iCol]; //dc.DataColumn.DataType.Name
            var iR = c1Grid.Row;
            var iC = c1Grid.Col;

            var a = Assembly.GetExecutingAssembly();
            var sortDown = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.SortDn.bmp"));
            var sortUp = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.SortUp.bmp"));
            sortDown.MakeTransparent(Color.Red);
            sortUp.MakeTransparent(Color.Red);

            //clear all sort indicators
            foreach (C1DisplayColumn col in c1Grid.Splits[_SplitsNum].DisplayColumns)
            {
                sTag += col.DataColumn.Tag + ";";
                col.HeadingStyle.ForegroundImage = null;
            }

            switch (dc.DataColumn.FilterWatermark)
            {
                case "":
                case "None":
                case "Desc":
                    sSortMode = "Asc";
                    break;
                default:
                    sSortMode = "Desc";
                    break;
            }

            string sCondition;

            var dt = ((DataTable)c1Grid.DataSource).Copy();
            var sOldCol = dt.Columns[iCol].ColumnName;
            dt.Columns[iCol].ColumnName = sNewCol0;

            if (bByNumber) //以數字排序，轉成 double 後再排序
            {
                dt.Columns.Add(sNewCol, typeof(double));

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    double.TryParse(dt.Rows[i][iCol].ToString(), out var dValue);

                    if (dt.Rows[i][iCol].ToString() == MyLibrary.sGridNullShowAs)
                    {
                        dValue = -1;
                    }

                    dt.Rows[i][dt.Columns.Count - 1] = dValue;
                }

                sCondition = sNewCol + " " + sSortMode + ", " + sNewCol0 + " " + sSortMode; //有些非數字的值，依原值排序
            }
            else //一般排序，為避免欄位標題包含換行符號或特殊字元(尤其是別國的語言)造成 DataTable 排序異常，故先以 sNewCol 排序，後面再置換回來
            {
                sCondition = sNewCol0 + " " + sSortMode;
            }

            var dv = dt.DefaultView;
            dv.Sort = sCondition;
            var dtSorted = dv.ToTable();

            if (bByNumber)
            {
                dtSorted.Columns.Remove(sNewCol);
            }

            dtSorted.Columns[iCol].ColumnName = sOldCol; //不論是一般排序 or 數字排序，都換回原本的名稱
            c1Grid.DataSource = dtSorted;
            dv.Dispose();

            dc = c1Grid.Splits[_SplitsNum].DisplayColumns[iCol];
            dc.DataColumn.FilterWatermark = sSortMode;
            dc.HeadingStyle.ForegroundImage = sSortMode == "Asc" ? sortUp : sortDown;
            dc.HeadingStyle.ForeGroundPicturePosition = ForeGroundPicturePositionEnum.RightOfText;

            if (MyLibrary.sGridNullShowAs.ToUpper() != "NONE")
            {
                var s1 = new Style { ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridNullShowColor) };

                for (var i = 0; i < c1Grid.Columns.Count; i++)
                {
                    //套用「使用者指定的 NULL」顯示格式
                    c1Grid.Splits[_SplitsNum].DisplayColumns[i].AddRegexCellStyle(CellStyleFlag.AllCells, s1, MyLibrary.sGridNullShowAs);
                }
            }

            var x = 0;
            var parts = sTag.Split(new[] { ";" }, StringSplitOptions.None);

            foreach (C1DisplayColumn col in c1Grid.Splits[_SplitsNum].DisplayColumns)
            {
                //還原原本記錄在 Tag 的 DataType，「複製成查詢條件」／「匯出至 Excel」才不會有問題
                col.DataColumn.Tag = parts[x];
                x++;
            }

            //Auto Size
            if (chkSize.Checked)
            {
                foreach (C1DisplayColumn col in c1Grid.Splits[_SplitsNum].DisplayColumns)
                {
                    try
                    {
                        col.AutoSize();
                    }
                    catch (Exception)
                    {
                        col.Width = 2000;
                    }

                    if ("`500`1000`1500`2000`".Contains("`" + MyGlobal.sGridMaxWidth + "`") && col.Width > Convert.ToInt16(MyGlobal.sGridMaxWidth))
                    {
                        col.Width = Convert.ToInt16(MyGlobal.sGridMaxWidth);
                    }
                }

                c1Grid.Refresh();
            }

            c1Grid.Row = iR;
            c1Grid.Col = iC;
            c1Grid.Select();
        }

        private void uSQLFormatter()
        {
            var sSelectedText = editor.SelectedText;

            var iStart = editor.SelectionStart;
            var iUppercaseKeywords = MyLibrary.bSQLFormatterConvertCaseForKeywords == false ? 0 : MyLibrary.iSQLFormatterConvertCaseForKeywordsCase;

            if (string.IsNullOrWhiteSpace(sSelectedText))
            {
                return;
            }

            var sResult = MyLibrary.SQLFormatter(sSelectedText, "\\t", 4, MyLibrary.iSQLFormatterMaxLineWidth,
                2, 1, MyLibrary.bSQLFormatterExpandCommaLists, MyLibrary.bSQLFormatterTrailingCommas,
                true, MyLibrary.bSQLFormatterExpandBooleanExpressions, MyLibrary.bSQLFormatterExpandCaseStatements, MyLibrary.bSQLFormatterExpandBetweenConditions,
                MyLibrary.bSQLFormatterExpandInLists, MyLibrary.bSQLFormatterBreakJoinOnSections, iUppercaseKeywords);

            sResult = sResult.Replace("\r\nunion all\r\n\r\n", "union all\r\n").Replace("\r\nUNION ALL\r\n\r\n", "UNION ALL\r\n").Replace("\r\nunion\r\n\r\n", "union\r\n").Replace("\r\nUNION\r\n\r\n", "UNION\r\n");
            sResult = sResult.Replace("    \r\n    union all\r\n    \r\n", "union all\r\n").Replace("    \r\n    UNION ALL\r\n    \r\n", "UNION ALL\r\n").Replace("    \r\n    union\r\n    \r\n", "union\r\n").Replace("    \r\n    UNION\r\n    \r\n", "UNION\r\n");
            sResult = sResult.Replace("        \r\n        union all\r\n        \r\n", "union all\r\n").Replace("        \r\n        UNION ALL\r\n        \r\n", "UNION ALL\r\n").Replace("        \r\n        union\r\n        \r\n", "union\r\n").Replace("        \r\n        UNION\r\n        \r\n", "UNION\r\n");

            editor.ReplaceSelection(sResult); //置換內容

            editor.SelectionStart = iStart; //重新選取內容
            editor.SelectionEnd = iStart + sResult.Length;
            editor.ScrollCaret();
        }

        /// <summary>
        /// 轉換大小寫
        /// </summary>
        /// <param name="bUpper">是否轉換為大寫：是→轉大寫；否→轉小寫</param>
        private void TransferStringUpperLower(bool bUpper)
        {
            var sResult = "";
            var sSelectedText = string.IsNullOrEmpty(editor.Tag.ToString()) ? editor.SelectedText : editor.Tag.ToString();

            if (string.IsNullOrEmpty(sSelectedText))
            {
                return;
            }

            var iStart = editor.SelectionStart;
            var iEnd = editor.SelectionEnd;

            var bDoubleQuotation = false; //是否為雙引號起始？是則不變更，直到遇到結束的雙引號
            var bSingleQuotation = false; //是否為單引號起始？是則不變更，直到遇到結束的單引號
            var bSingleComment = false; //是否為單列註解？
            var bParagraphComment = false; //是否為段落註解？
            var array = sSelectedText.ToCharArray();

            // Loop through array.
            for (var i = 0; i < array.Length; i++)
            {
                // Get character from array.
                var letter = array[i];

                if (letter.ToString() == "\"" || letter.ToString() == "'")
                {
                    switch (letter.ToString())
                    {
                        case "\"" when bDoubleQuotation:
                            bDoubleQuotation = false;
                            break;
                        case "\"":
                            {
                                if (bSingleQuotation == false)
                                {
                                    bDoubleQuotation = true;
                                }

                                break;
                            }
                        case "'" when bSingleQuotation:
                            bSingleQuotation = false;
                            break;
                        case "'":
                            {
                                if (bDoubleQuotation == false)
                                {
                                    bSingleQuotation = true;
                                }

                                break;
                            }
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && i >= 1 && letter.ToString() == "*" && array[i - 1].ToString() == "/")
                {
                    if (bParagraphComment == false)
                    {
                        bParagraphComment = true;
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && letter.ToString() == "*" && i + 1 <= array.GetUpperBound(0) && array[i + 1].ToString() == "/")
                {
                    if (bParagraphComment)
                    {
                        bParagraphComment = false;
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && i >= 1 && letter.ToString() == "-" && array[i - 1].ToString() == "-")
                {
                    if (bSingleComment == false)
                    {
                        bSingleComment = true;
                    }
                }

                if (bSingleComment && (letter.ToString() == "\r" || letter.ToString() == "\n"))
                {
                    bSingleComment = false;
                }

                if (bParagraphComment && letter.ToString() == "*" && i + 1 < array.Length && array[i + 1].ToString() == "/")
                {
                    bParagraphComment = false;
                }

                if (bDoubleQuotation == false && bSingleQuotation == false && bSingleComment == false && bParagraphComment == false)
                {
                    if (bUpper)
                    {
                        sResult += letter.ToString().ToUpper();
                    }
                    else
                    {
                        sResult += letter.ToString().ToLower();
                    }
                }
                else
                {
                    //單引號或雙引號包起來的字串，不用轉大小寫
                    sResult += letter.ToString();
                }
            }

            editor.SelectionStart = iStart; //重新選取內容
            editor.SelectionEnd = iEnd;

            editor.ReplaceSelection(sResult); //置換內容

            editor.SelectionStart = iStart; //重新選取內容
            editor.SelectionEnd = iEnd;
        }

        /// <summary>
        /// 判斷是否為 With 子句的 SQL
        /// </summary>
        /// <param name="sSql"></param>
        /// <returns></returns>
        private static bool IsWithSql(string sSql) //判斷 With 子句屬於 Select 還是 Update/Delete/Insert？
        {
            var bResult = true;
            var sTempSql = "";
            var sSelectedText = sSql.ToUpper();

            for (var i = 1; i < 20; i++)
            {
                sSelectedText = sSelectedText.Replace("  ", " ");
            }

            var bDoubleQuotation = false; //是否為雙引號起始？
            var bSingleQuotation = false; //是否為單引號起始？
            var bSingleComment = false; //是否為單列註解？
            var bParagraphComment = false; //是否為段落註解？
            var array = sSelectedText.ToCharArray();

            for (var i = 0; i < array.Length; i++)
            {
                var letter = array[i];

                if (letter.ToString() == "\"" || letter.ToString() == "'")
                {
                    switch (letter.ToString())
                    {
                        case "\"" when bDoubleQuotation:
                            bDoubleQuotation = false;
                            break;
                        case "\"":
                            {
                                if (bSingleQuotation == false)
                                {
                                    bDoubleQuotation = true;
                                }

                                break;
                            }
                        case "'" when bSingleQuotation:
                            bSingleQuotation = false;
                            break;
                        case "'":
                            {
                                if (bDoubleQuotation == false)
                                {
                                    bSingleQuotation = true;
                                }

                                break;
                            }
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && i >= 1 && letter.ToString() == "*" && array[i - 1].ToString() == "/")
                {
                    if (bParagraphComment == false)
                    {
                        bParagraphComment = true;
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && letter.ToString() == "*" && i + 1 <= array.GetUpperBound(0) && array[i + 1].ToString() == "/")
                {
                    if (bParagraphComment)
                    {
                        bParagraphComment = false;
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && i >= 1 && letter.ToString() == "-" && array[i - 1].ToString() == "-")
                {
                    if (bSingleComment == false)
                    {
                        bSingleComment = true;
                    }
                }

                if (bSingleComment && (letter.ToString() == "\r" || letter.ToString() == "\n"))
                {
                    bSingleComment = false;
                }

                if (bParagraphComment && letter.ToString() == "*" && i + 1 < array.Length && array[i + 1].ToString() == "/")
                {
                    bParagraphComment = false;
                }

                if (bDoubleQuotation == false && bSingleQuotation == false && bSingleComment == false && bParagraphComment == false)
                {
                    sTempSql += letter.ToString();
                }
            }

            //換行符號置換成空白
            sTempSql = sTempSql.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");

            //檢查剩下來的 SQL，如果有異動指令，就判定為「非查詢」
            if (sTempSql.Contains(" UPDATE ") || sTempSql.Contains(" DELETE ") || sTempSql.Contains(" INSERT "))
            {
                bResult = false;
            }

            return bResult;
        }

        /// <summary>
        /// 將完整 SQL 整理成一個變數，以利後續判斷用 (將註解一併移除！)
        /// </summary>
        /// <param name="sSql">按下句點句點時，該段落的完整 SQL</param>
        /// <returns>整理完畢的 SQL 子句</returns>
        private static string GetFormattedSql(string sSql, bool bToUpper = true)
        {
            //去除「區段註解」
            for (var i = 0; i < 100; i++)
            {
                var iIndexOf1 = sSql.IndexOf("/*", StringComparison.Ordinal);
                var iIndexOf2 = sSql.IndexOf("*/", StringComparison.Ordinal);

                if (iIndexOf1 == -1 || iIndexOf2 == -1)
                {
                    break;
                }

                if (iIndexOf2 > iIndexOf1)
                {
                    sSql = sSql.Substring(0, sSql.IndexOf("/*", StringComparison.Ordinal)) + sSql.Substring(sSql.IndexOf("*/", StringComparison.Ordinal) + 2, sSql.Length - sSql.IndexOf("*/", StringComparison.Ordinal) - 2);
                }
            }

            var parts = sSql.Split(new[] { "\r\n" }, StringSplitOptions.None);

            //去除整列空白，或是包含「單列註解」
            for (var i = 0; i < parts.Length; i++)
            {
                if (parts[i].Length < 2)
                {
                    parts[i] = parts[i].Trim();
                }
                else if (parts[i].Substring(0, 2) == "--")
                {
                    parts[i] = "";
                }
                else if (parts[i].IndexOf("--", StringComparison.Ordinal) != -1)
                {
                    parts[i] = parts[i].Substring(0, parts[i].IndexOf("--", StringComparison.Ordinal));
                }
            }

            //重新組成單獨一列的 SQL 指令
            sSql = parts.TakeWhile((t, i) => i <= 300).Aggregate("", (current, t) => current + (string.IsNullOrEmpty(t) ? "" : t + " "));

            //換行符號置換成空白
            sSql = sSql.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");

            //逗號、括號，右邊多一個空白 (以利識別用)
            sSql = sSql.Replace(",", " , ").Replace(")", " ) ");

            //重複出現的空白，全部置換成一個空白，方便後續判斷
            for (var i = 1; i < 50; i++)
            {
                sSql = sSql.Replace("  ", " ");
            }

            if (bToUpper)
            {
                sSql = sSql.ToUpper();
            }

            return sSql.Trim();
        }

        private static string GetParametersInfo(string sSql, out string sResultAll)
        {
            var sTemp = "";
            var sResultDistinct = "`";
            var sResultDistinct2 = "`";
            var bParameterStart = false;
            var bDoubleQuotation = false; //是否為雙引號起始？是則不變更，直到遇到結束的雙引號
            var bSingleQuotation = false; //是否為單引號起始？是則不變更，直到遇到結束的單引號
            var bSingleComment = false; //是否為單列註解？
            var bParagraphComment = false; //是否為段落註解？
            var array = sSql.ToCharArray();

            sResultAll = "`";

            for (var i = 0; i < array.Length; i++)
            {
                var letter = array[i];

                if (letter.ToString() == "\"" || letter.ToString() == "'")
                {
                    switch (letter.ToString())
                    {
                        case "\"" when bDoubleQuotation:
                            bDoubleQuotation = false;
                            break;
                        case "\"":
                            {
                                if (bSingleQuotation == false)
                                {
                                    bDoubleQuotation = true;
                                }

                                break;
                            }
                        case "'" when bSingleQuotation:
                            bSingleQuotation = false;
                            break;
                        case "'":
                            {
                                if (bDoubleQuotation == false)
                                {
                                    bSingleQuotation = true;
                                }

                                break;
                            }
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && i >= 1 && (letter.ToString() == "*" && array[i - 1].ToString() == "/"))
                {
                    if (bParagraphComment == false)
                    {
                        bParagraphComment = true;
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && letter.ToString() == "*" && i + 1 <= array.GetUpperBound(0) && array[i + 1].ToString() == "/")
                {
                    if (bParagraphComment)
                    {
                        bParagraphComment = false;
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && i >= 1 && letter.ToString() == "-" && array[i - 1].ToString() == "-")
                {
                    if (bSingleComment == false)
                    {
                        bSingleComment = true;
                    }
                }

                if (bSingleComment && (letter.ToString() == "\r" || letter.ToString() == "\n"))
                {
                    bSingleComment = false;
                }

                if (bParagraphComment && letter.ToString() == "*" && i + 1 < array.Length && array[i + 1].ToString() == "/")
                {
                    bParagraphComment = false;
                }

                if (bDoubleQuotation || bSingleQuotation || bSingleComment || bParagraphComment)
                {
                    continue;
                }

                if (letter.ToString() == ":" && i >= 2
                                             && (array[i - 1].ToString() == "=" || array[i - 1].ToString() == " " || array[i - 1].ToString() == "(" || array[i - 1].ToString() == ")" || array[i - 1].ToString() == "<" || array[i - 1].ToString() == ">" || array[i - 1].ToString() == "\n")
                                             && array[i - 1].ToString() != ":" && i < array.Length && array[i + 1].ToString() != ":")
                {
                    //冒號前面的字元為 ( ) = 或空白或 \n 換行符號，表示它是參數
                    //避掉連續兩個 ::
                    bParameterStart = true;
                }

                if (!bParameterStart)
                {
                    continue;
                }

                //下一個字元是否為空白或 \r 或 - 或 / 或 ; 或 ( 或 ) 或 , 等符號
                if (i + 1 < array.Length && array[i + 1].ToString() != " " && array[i + 1].ToString() != "\r" && array[i + 1].ToString() != "-" && array[i + 1].ToString() != "/" && array[i + 1].ToString() != ";" && array[i + 1].ToString() != "," && array[i + 1].ToString() != ")")
                {
                    sTemp += letter.ToString();
                }
                else
                {
                    sTemp += letter.ToString();

                    if (sResultDistinct.IndexOf("`" + sTemp.ToUpper() + "`", StringComparison.Ordinal) == -1)
                    {
                        sResultDistinct += sTemp.ToUpper() + "`";
                        sResultDistinct2 += sTemp + "`";
                    }

                    sResultAll += sTemp + "|" + (i - sTemp.Length + 1) + "`";

                    sTemp = "";

                    bParameterStart = false;
                }
            }

            sResultDistinct2 = sResultDistinct2 == "`" ? "" : sResultDistinct2.Substring(1, sResultDistinct2.Length - 2);

            return sResultDistinct2;
        }

        private void editor_Leave(object sender, EventArgs e)
        {
            tsEditor.BackColor = _cToolstripUnfocused;

            if (c1GridAC4Period1.Visible)
            {
                //焦點離開 Editor，不能直接將 c1GridAC4Period1 隱藏！
                _iPeriodPos2 = editor.CurrentPosition;
            }
            else if (c1GridAC4Space1.Visible)
            {
                //焦點離開 Editor，不能直接將 c1GridAC4Space1 隱藏！
                _iSpacePos2 = editor.CurrentPosition;
            }
            else if (c1GridAC4All.Visible)
            {
                //焦點離開 Editor，不能直接將 c1GridAC4All 隱藏！
                _iAllPos2 = editor.CurrentPosition;
            }
        }

        private void editor_Enter(object sender, EventArgs e)
        {
            tsEditor.BackColor = _cToolstripFocused;
            myFindReplace.Scintilla = (JasonLibrary.ScintillaEditor)sender; //20230629
            MyGlobal.sGlobalTemp5 = "CanPasteY"; //20230704

            //CheckFileDataTimeAndExist(); //此處，會造成 Reload 之後，editor 的選取區域會因根據滑鼠游標位置亂跑
        }

        private void editorMessage_Enter(object sender, EventArgs e)
        {
            ChangeBackColor(_cToolstripFocused); //20230629
            myFindReplace.Scintilla = (JasonLibrary.ScintillaEditor)sender; //20230629
            MyGlobal.sGlobalTemp5 = "CanPasteN"; //20230704
        }

        private void editorSQLHistory_Enter(object sender, EventArgs e)
        {
            ChangeBackColor(_cToolstripFocused); //20230629
            myFindReplace.Scintilla = (JasonLibrary.ScintillaEditor)sender; //20230629
            MyGlobal.sGlobalTemp5 = "CanPasteN"; //20230704
        }

        private void CheckFileDateTimeAndExist()
        {
            var sTemp = "";
            string sTemp2;
            var sSavedDateTime = "";
            string sFilename;

            try
            {
                sTemp = btnSave.Tag.ToString(); //判斷是否已存檔
                sSavedDateTime = btnSaveRed.Tag.ToString();
            }
            catch (Exception)
            {
                //
            }

            //檢查檔案是否存在 (有可能被外部程式刪除了)
            if (!string.IsNullOrEmpty(sTemp) && File.Exists(sTemp) == false)
            {
                sFilename = Path.GetFileName(sTemp);

                //如果有同樣的 MessageBox，先刪除
                _sLangText = MyGlobal.GetLanguageString("Keep non existing file", "form", Name, "msg", "KeepNonExistingFile", "Text");
                FindMessageBox(_sLangText, true);

                sTemp2 = MyGlobal.GetLanguageString("This file doesn't exist anymore.", "form", Name, "msg", "ThisFileDoesntExist", "Text");
                var sTemp3 = MyGlobal.GetLanguageString("Keep this file in editor?", "form", Name, "msg", "KeepThisFileInEditor", "Text");

                if (MessageBox.Show(sTemp2 + "\r\n" + sFilename + "\r\n\r\n" + sTemp3, _sLangText, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    WriteFile(sTemp); //以指定路徑+檔名存檔
                    editor.Focus();
                }
                else
                {
                    //使用者不要保留，直接關閉它！
                    TransferValueToMainForm("closequeryformdirectly`" + AccessibleDescription);
                }
            }
            //檢查是否有外部編輯器編輯此檔案
            else if (!string.IsNullOrEmpty(sTemp) && !string.IsNullOrEmpty(sSavedDateTime)) //sTemp=""，表示尚未存檔，不用處理
            {
                try
                {
                    var sCheckDataTime = File.GetLastWriteTime(sTemp).ToString("yyyy/MM/dd HH:mm:ss");

                    if (sSavedDateTime == sCheckDataTime)
                    {
                        return;
                    }

                    string sMsg;

                    if (editor.CanUndo)
                    {
                        sMsg = "Do you want to reload it and lose the changes made in JasonQuery?";
                        sMsg = MyGlobal.GetLanguageString(sMsg, "form", Name, "msg", "Reload1", "Text");
                    }
                    else
                    {
                        sMsg = "Do you want to reload it?";
                        sMsg = MyGlobal.GetLanguageString(sMsg, "form", Name, "msg", "Reload2", "Text");
                    }

                    //如果有同樣的 MessageBox，先刪除
                    _sLangText = MyGlobal.GetLanguageString("Reload", "form", Name, "msg", "Reload", "Text");
                    FindMessageBox(_sLangText, true);

                    sTemp2 = MyGlobal.GetLanguageString("This file has been modified by another program.", "form", Name, "msg", "ModifiedByAnotherProgram", "Text");

                    if (MessageBox.Show(sTemp + "\r\n\r\n" + sTemp2 + "\r\n" + sMsg, _sLangText, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        LoadFile(sTemp, out _); //CheckFileDateTimeAndExist

                        //傳資訊到母表單，更新 Tab 資訊
                        TransferValueToMainForm("updatetabinfo`" + sTemp);

                        //儲存「存檔點」
                        editor.EmptyUndoBuffer();

                        btnSaveRed.Visible = false;
                        btnSave.Visible = true;
                    }
                    else
                    {
                        //傳資訊到母表單，更新 Tab 資訊
                        TransferValueToMainForm("updatetabinfo`" + "*" + sTemp);

                        btnSaveRed.Visible = true;
                        btnSave.Visible = false;
                    }

                    //無論使用者選「要或不要更新」，一律更新時間戳記，下次判斷時才不會「沒再次更新卻又再詢問一次」
                    btnSaveRed.Tag = File.GetLastWriteTime(sTemp).ToString("yyyy/MM/dd HH:mm:ss");

                    editor.Focus();
                }
                catch (Exception)
                {
                    //
                }
            }
        }

        private void SetGridToolStripBackColor(bool bColor)
        {
            ChangeBackColor(bColor ? _cToolstripFocused : _cToolstripUnfocused);
        }

        private void Detect_KeyDown(object sender, KeyEventArgs e)
        {
            _ctrlKeyDown = e.Control;
        }

        private void ZoomGrid(float pcnt)
        {
            var c1Grid = GetWhichGrid();

            if (_fontSize == 0)
            {
                _fontSize = 12;
            }

            c1Grid.RowHeight = (int)(_rowHeight * pcnt) + 5;
            c1Grid.Splits[_SplitsNum].ColumnCaptionHeight = chkShowColumnType.Checked ? (int)(_rowHeight * pcnt) * 2 + 11 : (int)(_rowHeight * pcnt) + 12;
            c1Grid.RecordSelectorWidth = (int)(_recSelWidth * pcnt);

            c1Grid.Styles["Normal"].Font = new Font(c1Grid.Styles["Normal"].Font.FontFamily, _fontSize * pcnt);

            if (!chkSize.Checked)
            {
                return;
            }

            foreach (C1DisplayColumn col in c1Grid.Splits[_SplitsNum].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 2000;
                }
            }
        }

        private void editor_ZoomChanged(object sender, EventArgs e)
        {
            //20191016 內容空白時，不用往下執行；否則會被視為「檔案內容有異動」
            if (string.IsNullOrEmpty(editor.Text))
            {
                return;
            }

            var bCanUndo = editor.CanUndo; //記住一開始是否可以 CanUndo

            //以下，當放大縮小後，即時調整 line number 的寬度，避免因為放大時，line number 最左側的數字會看不見
            var iStart = editor.SelectionStart;
            editor.Text = editor.Text + "\r\n";
            editor.Text = editor.Text.Substring(0, editor.Text.Length - 2);
            editor.SelectionStart = iStart;
            editor.ScrollCaret();

            if (bCanUndo)
            {
                return;
            }

            editor.EmptyUndoBuffer(); //20191016 如果原本就沒異動過內容，此處要還原狀態
            CheckEditorContent(); //editor_ZoomChanged() 事件
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            NextPage();
        }

        private void NextPage(int iCol = -1, int iRow = -1)
        {
            if (btnCancelQuery.Enabled)
            {
                return; //「正在執行查詢指令」，忽略！
            }

            Application.UseWaitCursor = true;
            c1DockingTab1.Enabled = false;

            _iNextPageCol = iCol == -1 ? c1TrueDBGrid1.Col : iCol;
            _iNextPageRow = iRow == -1 ? c1TrueDBGrid1.Row : iRow;

            _bNextPageQuery = true; //分頁查詢！
            btnCancelQuery.Tag = "";
            MyGlobal.sExecuteNonQuerySQLHistoryScript = "";

            var c1Grid = GetWhichGrid();
            var sSql = c1Grid.AccessibleDescription;
            _dtNextPage = (DataTable)c1Grid.DataSource;

            if (string.IsNullOrEmpty(sSql))
            {
                return;
            }

            _dNext += 0.5;
            ExecuteQuery(sSql, _bNextPageQuery);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            _bNextPageQuery = false; //正常查詢！
            _iLastTimeOffset = 0;

            lblRows.Text = @"0 " + _sRows;
            lblSummaryValue.Text = "0";
            lblCountValue.Text = "0";
            lblAverageValue.Text = "0";
            c1TrueDBGrid1.DataSource = _dtNull;

            c1GridARInfo.Cursor = Cursors.Default;

            //正常查詢，從 第0頁 開始
            btnNextPage.Tag = "0";

            //正常查詢，ToolTip 恢復為「下一頁」
            btnNextPage.ToolTip = MyGlobal.GetLanguageString("Next Page", "form", Name, "object", "btnNextPage", "ToolTipText");

            _dtNextPage = null;

            _dNext = 1;
            btnCancelQuery.Tag = "";
            MyGlobal.sExecuteNonQuerySQLHistoryScript = "";
            ExecuteQuery();
        }

        private void ExecuteQuery(string sSql = "", bool bNextPage = false, bool bIndicator = true)
        {
            string sResult; //sQueryTextFinal 是最終要執行的 SQL
            var iStart = editor.SelectionStart; //SQL Statement 選取區段的起始位置 (後續要計算錯誤定位點的依據)
            string sCRLF; //iStart 前面有幾個換行符號 (for SQL Server only)

            btnQuery.AccessibleDescription = ""; //判斷 SQL 是否為查詢指令
            var sQueryText = string.IsNullOrEmpty(sSql) ? editor.SelectedText : sSql;

            if (MyLibrary.sAutoDisconnect != "Never" && MyGlobal.bMainFormAutoDisconnect)
            {
                //MainForm: 切換 timer，繼續偵測是否需要「自動中斷連線」
                MyGlobal.sGlobalTemp = "queryagain";
            }
            else
            {
                MyGlobal.sGlobalTemp = "";
            }

            if (string.IsNullOrEmpty(sQueryText))
            {
                //使用者沒有選取任何文字，由程式自動全選
                iStart = 0;
                sCRLF = MyGlobal.sSeparator5;
                sQueryText = editor.Text;
                editor.SelectionStart = 0;
                editor.SelectionEnd = editor.Text.Length;
            }

            sQueryText = sQueryText.Replace(MyGlobal.sSeparator, " ");

            var sTemp1 = editor.Text.Substring(0, iStart);
            sCRLF = MyGlobal.sSeparator5 + ((sTemp1.Length - sTemp1.Replace("\r\n", "").Length) / 2); //要執行的 SQL，前方有幾個換行符號

            if (MyGlobal.sDataSource != @"SQL Server" && MyGlobal.sDataSource != @"MySQL") //SQL Server/MySQL 在執行 SQL 有錯誤時，不會回傳定位點的數值
            {
                sCRLF = "";
            }

            var sParametersDistinct = GetParametersInfo(sQueryText, out string sParametersAll);

            _sQueryTextParameters = "";
            _sQueryTextParametersMapping = "";
            _sQueryTextParametersPositionMapping = "";
            _iQueryTextParametersStart = iStart;

            if (!string.IsNullOrEmpty(sParametersDistinct))
            {
                if (bNextPage)
                {
                    sQueryText = btnPaginationOff.Tag.ToString();
                }
                else
                {
                    btnPaginationOff.Tag = "";

                    using (var myForm = new frmParameters())
                    {
                        myForm.sSqlText = sQueryText;
                        myForm.sParameters = sParametersDistinct;
                        myForm.sParametersWithPosition = sParametersAll;
                        myForm.sAccessibleDescription = AccessibleDescription;

                        var iParametersFormWidth = 0;
                        var iParametersFormHeight = 0;

                        var sTemp = "SELECT AttributeValue FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'ParametersFormWidth'";
                        var dtData = DBCommon.ExecQuery(sTemp);

                        if (dtData.Rows.Count > 0)
                        {
                            int.TryParse(dtData.Rows[0][0].ToString(), out iParametersFormWidth);
                        }

                        sTemp = "SELECT AttributeValue FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'ParametersFormHeight'";
                        dtData = DBCommon.ExecQuery(sTemp);

                        if (dtData.Rows.Count > 0)
                        {
                            int.TryParse(dtData.Rows[0][0].ToString(), out iParametersFormHeight);
                        }

                        if (iParametersFormWidth > 0 && iParametersFormHeight > 0)
                        {
                            myForm.ClientSize = new Size(iParametersFormWidth - 16, iParametersFormHeight - 38);
                        }

                        myForm.ShowDialog();
                    }

                    var startTime = DateTime.Now;

                    while (true)
                    {
                        Application.DoEvents();

                        if (DateTime.Now.Subtract(startTime).Seconds >= 1) //暫停 1 秒, 取得 frmParameters 傳回的資訊
                        {
                            break;
                        }

                        Application.DoEvents();
                    }

                    //如果使用者在 frmParameters 按下「Cancel」鈕，_sQueryTextParameters 會是空值，不會執行任何 SQL 指令
                    if (!string.IsNullOrEmpty(_sQueryTextParameters))
                    {
                        btnPaginationOff.Tag = _sQueryTextParameters;
                    }

                    sQueryText = _sQueryTextParameters;
                    _sSqlWhenError = editor.SelectedText;
                }
            }
            else
            {
                _sSqlWhenError = sQueryText;
            }

            if (string.IsNullOrWhiteSpace(sQueryText))
            {
                return;
            }

            //20220808 待優化
            if (1 == 2)
            {
                var bSavePoint = false;
                var bRollbackPoint = false;
                var bReleasePoint = false;
                var sPointName = "";
                var sPointResult = "";
                var sSqlBlock = GetFormattedSql(sQueryText) + " "; //整理成一句 SQL

                switch (MyGlobal.sDataSource)
                {
                    case "Oracle": //不支援 Release 指令
                        {
                            if (sSqlBlock.StartsWith("SAVEPOINT "))
                            {
                                bSavePoint = true;
                                sPointName = sSqlBlock.Replace("SAVEPOINT", "").Trim();
                                sPointResult = MyGlobal.oOracleReader.oSavePoint(sPointName);

                                return;
                            }
                            else if (sSqlBlock.StartsWith("ROLLBACK TO SAVEPOINT "))
                            {
                                bSavePoint = true;
                                sPointName = sSqlBlock.Replace("ROLLBACK TO SAVEPOINT", "").Trim();
                                sPointResult = MyGlobal.oOracleReader.oRollbackPoint(sPointName);

                                return;
                            }

                            break;
                        }
                    case "PostgreSQL":
                        {
                            if (sSqlBlock.StartsWith("SAVEPOINT "))
                            {
                                bSavePoint = true;
                                sPointName = sSqlBlock.Replace("SAVEPOINT", "").Trim();
                                sPointResult = MyGlobal.oPostgreReader.oSavePoint(sPointName);

                                return;
                            }
                            else if (sSqlBlock.StartsWith("ROLLBACK "))
                            {
                                bRollbackPoint = true;

                                if (sSqlBlock.StartsWith("ROLLBACK TO SAVEPOINT "))
                                {
                                    sPointName = sSqlBlock.Replace("ROLLBACK TO SAVEPOINT", "").Trim();
                                }
                                else if (sSqlBlock.StartsWith("ROLLBACK TO "))
                                {
                                    sPointName = sSqlBlock.Replace("ROLLBACK TO ", "").Trim();
                                }
                                else if (sSqlBlock.StartsWith("ROLLBACK WORK TO SAVEPOINT "))
                                {
                                    sPointName = sSqlBlock.Replace("ROLLBACK WORK TO SAVEPOINT", "").Trim();
                                }
                                else if (sSqlBlock.StartsWith("ROLLBACK WORK TO "))
                                {
                                    sPointName = sSqlBlock.Replace("ROLLBACK WORK TO", "").Trim();
                                }
                                else if (sSqlBlock.StartsWith("ROLLBACK TRANSACTION TO SAVEPOINT "))
                                {
                                    sPointName = sSqlBlock.Replace("ROLLBACK TRANSACTION TO SAVEPOINT", "").Trim();
                                }
                                else if (sSqlBlock.StartsWith("ROLLBACK TRANSACTION TO "))
                                {
                                    sPointName = sSqlBlock.Replace("ROLLBACK TRANSACTION TO ", "").Trim();
                                }

                                sPointResult = MyGlobal.oPostgreReader.oRollbackPoint(sPointName);
                                return;
                            }
                            else if (sSqlBlock.StartsWith("RELEASE "))
                            {
                                bReleasePoint = true;

                                if (sSqlBlock.StartsWith("RELEASE SAVEPOINT "))
                                {
                                    sPointName = sSqlBlock.Replace("RELEASE SAVEPOINT", "").Trim();
                                }
                                else if (sSqlBlock.StartsWith("RELEASE "))
                                {
                                    sPointName = sSqlBlock.Replace("RELEASE ", "").Trim();
                                }

                                sPointResult = MyGlobal.oPostgreReader.oReleasePoint(sPointName);
                                return;
                            }

                            break;
                        }
                    case "SQL Server": //不支援 Release 指令
                        {
                            if (sSqlBlock.StartsWith("SAVE "))
                            {
                                if (sSqlBlock.StartsWith("SAVE TRANSACTION "))
                                {
                                    bSavePoint = true;
                                    sPointName = sSqlBlock.Replace("SAVE TRANSACTION", "").Trim();
                                    sPointResult = MyGlobal.oSQLServerReader.oSavePoint(sPointName);

                                    return;
                                }
                                else if (sSqlBlock.StartsWith("SAVE TRAN "))
                                {
                                    bSavePoint = true;
                                    sPointName = sSqlBlock.Replace("SAVE TRAN", "").Trim();
                                    sPointResult = MyGlobal.oSQLServerReader.oSavePoint(sPointName);

                                    return;
                                }
                                else if (sSqlBlock.StartsWith("SAVE "))
                                {
                                    bSavePoint = true;
                                    sPointName = sSqlBlock.Replace("SAVE", "").Trim();
                                    sPointResult = MyGlobal.oSQLServerReader.oSavePoint(sPointName);

                                    return;
                                }
                            }
                            else if (sSqlBlock.StartsWith("ROLLBACK "))
                            {
                                if (sSqlBlock.StartsWith("ROLLBACK TRANSACTION "))
                                {
                                    bSavePoint = true;
                                    sPointName = sSqlBlock.Replace("ROLLBACK TRANSACTION", "").Trim();
                                    sPointResult = MyGlobal.oSQLServerReader.oRollbackPoint(sPointName);

                                    return;
                                }
                                else if (sSqlBlock.StartsWith("ROLLBACK TRAN "))
                                {
                                    bSavePoint = true;
                                    sPointName = sSqlBlock.Replace("ROLLBACK TRAN", "").Trim();
                                    sPointResult = MyGlobal.oSQLServerReader.oRollbackPoint(sPointName);

                                    return;
                                }
                                else if (sSqlBlock.StartsWith("ROLLBACK "))
                                {
                                    bSavePoint = true;
                                    sPointName = sSqlBlock.Replace("ROLLBACK", "").Trim();
                                    sPointResult = MyGlobal.oSQLServerReader.oRollbackPoint(sPointName);

                                    return;
                                }
                            }

                            break;
                        }
                    case "MySQL":
                        {
                            if (sSqlBlock.StartsWith("SAVEPOINT "))
                            {
                                bSavePoint = true;
                                sPointName = sSqlBlock.Replace("SAVEPOINT", "").Trim();
                                sPointResult = MyGlobal.oMySQLReader.oSavePoint(sPointName);

                                return;
                            }
                            else if (sSqlBlock.StartsWith("ROLLBACK "))
                            {
                                bRollbackPoint = true;

                                if (sSqlBlock.StartsWith("ROLLBACK TO SAVEPOINT "))
                                {
                                    sPointName = sSqlBlock.Replace("ROLLBACK TO SAVEPOINT", "").Trim();
                                }
                                else if (sSqlBlock.StartsWith("ROLLBACK TO "))
                                {
                                    sPointName = sSqlBlock.Replace("ROLLBACK TO ", "").Trim();
                                }
                                else if (sSqlBlock.StartsWith("ROLLBACK WORK TO SAVEPOINT "))
                                {
                                    sPointName = sSqlBlock.Replace("ROLLBACK WORK TO SAVEPOINT", "").Trim();
                                }
                                else if (sSqlBlock.StartsWith("ROLLBACK WORK TO "))
                                {
                                    sPointName = sSqlBlock.Replace("ROLLBACK WORK TO", "").Trim();
                                }

                                sPointResult = MyGlobal.oMySQLReader.oRollbackPoint(sPointName);
                                return;
                            }
                            else if (sSqlBlock.StartsWith("RELEASE "))
                            {
                                bReleasePoint = true;

                                if (sSqlBlock.StartsWith("RELEASE SAVEPOINT "))
                                {
                                    sPointName = sSqlBlock.Replace("RELEASE SAVEPOINT", "").Trim();
                                }
                                else if (sSqlBlock.StartsWith("RELEASE "))
                                {
                                    sPointName = sSqlBlock.Replace("RELEASE ", "").Trim();
                                }

                                sPointResult = MyGlobal.oMySQLReader.oReleasePoint(sPointName);
                                return;
                            }

                            break;
                        }
                    case "SQLite":
                        {


                            break;
                        }
                    case "SQLCipher":
                        {


                            break;
                        }
                }

                if (bSavePoint == false && bRollbackPoint == false && bReleasePoint == false)
                {
                    //一般指令
                }
                else
                {
                    if (bSavePoint)
                    {
                        if (string.IsNullOrEmpty(sPointResult))
                        {
                            sPointResult = "SavePoint successfully!";
                        }
                        else
                        {

                        }
                    }
                    else if (bRollbackPoint)
                    {
                        if (string.IsNullOrEmpty(sPointResult))
                        {

                        }
                        else
                        {

                        }
                    }
                    else if (bReleasePoint)
                    {
                        if (string.IsNullOrEmpty(sPointResult))
                        {

                        }
                        else
                        {

                        }
                    }

                    return;
                }
            }

            btnNextPage.Enabled = false;

            if (bNextPage == false)
            {
                lblRows.Text = @"0 " + _sRows;
            }

            btnQuery.Tag = sQueryText;

            lblQueryTime.Tag = "00:00:00";

            var bQuery = IsQuery(sQueryText, out var iQueryCount, out var bCommitRollbackScript, out var bExtended, out var bIgnoreCommitRollback);

            if (iQueryCount == 0) //沒有 SQL 指令 (例如全部是註解)
            {
                return;
            }

            _dtStartTime = DateTime.Now;
            tmrExecTime.Enabled = true;
            tmrQueryTime.Enabled = true;
            _bBusy = true;

            if (bQuery == false)
            {
                //20201108 非查詢，Data Grid 清空！
                c1TrueDBGrid1.DataSource = _dtNull;

                if (bCommitRollbackScript) //按下 Commit / Rollback 按鈕；或是執行 commit / rollback 指令
                {
                    btnQuery.AccessibleDescription = "query"; //執行 Commit or Rollback 後，會「中斷連線」，故以「query 狀態」來處理

                    //傳遞資訊至 MainForm，更新 Commit/Rollbak 狀態
                    TransferValueToMainForm("executecommitrollback`"); //ExecuteQuery
                }
                else
                {
                    btnQuery.AccessibleDescription = bIgnoreCommitRollback ? "query" : "nonquery";

                    //UpdateNotCommitYetInfo(lblNotCommitYet.Tag.ToString()); //20210601 修正：非查詢，執行無錯誤才要變更！(故不在此變更)
                }

                //20210601 修正：非查詢，執行無錯誤才要變更！(故不在此變更)
                //btnCommit.Enabled = true;
                //btnRollback.Enabled = true;
            }
            else
            {
                btnQuery.AccessibleDescription = "query";

                //20201124 針對所有查詢，先切換到 tabDataGrid (查詢結束後，頁籤就會停留在 tabDataGrid，Focus 是在 Edior)
                c1DockingTab1.SelectedTab = tabDataGrid;

                var sTableName = GetTableNameFromSQL(sQueryText); //判斷 table name

                for (var i = 1; i <= 20; i++)
                {
                    if (c1DockingTab1.TabPages.Count <= 3)
                    {
                        break; //基本的 3 個頁籤，不用再往下判斷！
                    }

                    foreach (Control ctrl in c1DockingTab1.TabPages)
                    {
                        if (_sOriginal3TabName.Contains("`" + ctrl.Text + "`") == false)
                        {
                            c1DockingTab1.TabPages.Remove(ctrl);
                        }
                    }
                }

                if (iQueryCount == 1) //iQueryIndex
                {
                    _iQueryIndex = 1;
                    c1TrueDBGrid1.Enabled = false;
                    c1TrueDBGrid1.Tag = sTableName;
                    c1TrueDBGrid1.AccessibleDescription = sQueryText;
                }
            }

            btnQuery.Enabled = false;
            btnExecuteCurrentBlock.Enabled = false;
            btnCancelQuery.Enabled = true;

            lblInfo.Text = "";
            lblInfo.Tag = "";

            //清除波浪底線！
            SetSquiggle(true); //ExecuteQuery

            //清除訊息
            UpdateMessage(""); //ExecuteQuery
            editorMessage.Tag = "";

            //清除置換字串，避免殘留，之後被誤置換了
            editorSQLHistory.Text = editorSQLHistory.Text.Replace(MyGlobal.sSeparator3, "");

            editorSQLHistory.ReadOnly = false;
            editorSQLHistory.Text = @"--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "\r\n" + sQueryText + MyGlobal.sSeparator3 + (string.IsNullOrEmpty(editorSQLHistory.Text) ? "" : "\r\n\r\n" + editorSQLHistory.Text);
            editorSQLHistory.ReadOnly = true;

            sQueryText = AccessibleDescription + MyGlobal.sSeparator5 + iStart + sCRLF + MyGlobal.sSeparator5 + sQueryText;

            //刪除所有的 Bookmark
            editor.MarkerDeleteAll(-1);

            //Current Line 加上 Bookmark
            if (bIndicator) //20220808 判斷要不要加上「執行 SQL 的指示箭頭」符號 (透過功能表切換資料庫，就不用顯示箭頭指示)
            {
                editor.Lines[editor.CurrentLine].MarkerAdd(BOOKMARK_MARKER);
            }

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    {
                        if (clsOracleReader.GetState() == ConnectionState.Closed)
                        {
                            sResult = MyGlobal.oOracleReader.ConnectTo();

                            if (!string.IsNullOrEmpty(sResult))
                            {
                                //20191212
                                MessageBox.Show(sResult, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        MyGlobal.oOracleReader.QueryCompleted += QueryCompleted;
                        break;
                    }
                case "PostgreSQL":
                    {
                        if (clsPostgreSQLReader.GetState() == ConnectionState.Closed)
                        {
                            sResult = MyGlobal.oPostgreReader.ConnectTo(MyGlobal.sDBConnectionString);

                            if (!string.IsNullOrEmpty(sResult))
                            {
                                //20191212
                                MessageBox.Show(sResult, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        MyGlobal.oPostgreReader.QueryCompleted += QueryCompleted;
                        break;
                    }
                case "SQL Server":
                    {
                        if (clsSQLServerReader.GetState() == ConnectionState.Closed)
                        {
                            sResult = MyGlobal.oSQLServerReader.ConnectTo(MyGlobal.sDBConnectionString);

                            if (!string.IsNullOrEmpty(sResult))
                            {
                                //20191212
                                MessageBox.Show(sResult, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        MyGlobal.oSQLServerReader.QueryCompleted += QueryCompleted;
                        break;
                    }
                case "MySQL":
                    {
                        if (clsMySQLReader.GetState() == ConnectionState.Closed)
                        {
                            sResult = MyGlobal.oMySQLReader.ConnectTo(MyGlobal.sDBConnectionString);

                            if (!string.IsNullOrEmpty(sResult))
                            {
                                MessageBox.Show(sResult, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        MyGlobal.oMySQLReader.QueryCompleted += QueryCompleted;
                        break;
                    }
                case "SQLite":
                    {
                        if (clsSQLiteReader.GetState() == ConnectionState.Closed)
                        {
                            sResult = MyGlobal.oSQLiteReader.ConnectTo(MyGlobal.sDBConnectionString);

                            if (!string.IsNullOrEmpty(sResult))
                            {
                                MessageBox.Show(sResult, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        MyGlobal.oSQLiteReader.QueryCompleted += QueryCompleted;
                        break;
                    }
                case "SQLCipher":
                    {
                        if (clsSQLCipherReader.GetState() == ConnectionState.Closed)
                        {
                            sResult = MyGlobal.oSQLCipherReader.ConnectTo(MyGlobal.sDBConnectionString);

                            if (!string.IsNullOrEmpty(sResult))
                            {
                                MessageBox.Show(sResult, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        MyGlobal.oSQLCipherReader.QueryCompleted += QueryCompleted;
                        break;
                    }
                default:
                    return;
            }

            if (string.IsNullOrEmpty(sQueryText))
            {
                return;
            }

            if (bQuery && btnPaginationOff.Visible == false && bExtended == false)
            {
                sQueryText += MyGlobal.sSeparator5 + btnNextPage.Tag + MyGlobal.sSeparator5 + btnPaginationOn.Tag;
            }

            switch (MyGlobal.sDataSource)
            {
                case "Oracle" when bQuery:
                    _threadQuery = btnPaginationOff.Visible ? new Thread(MyGlobal.oOracleReader.ExecuteQuery2) : new Thread(MyGlobal.oOracleReader.ExecuteQueryPaged);
                    break;
                case "Oracle":
                    _threadQuery = new Thread(MyGlobal.oOracleReader.ExecuteNonQuery);
                    break;
                case "PostgreSQL" when bQuery:
                    _threadQuery = (bExtended || btnPaginationOff.Visible) ? new Thread(MyGlobal.oPostgreReader.ExecuteQuery2) : new Thread(MyGlobal.oPostgreReader.ExecuteQueryPaged);
                    break;
                case "PostgreSQL":
                    _threadQuery = new Thread(MyGlobal.oPostgreReader.ExecuteNonQuery);
                    break;
                case "SQL Server" when bQuery:
                    _threadQuery = (bExtended || btnPaginationOff.Visible) ? new Thread(MyGlobal.oSQLServerReader.ExecuteQuery2) : new Thread(MyGlobal.oSQLServerReader.ExecuteQueryPaged);
                    break;
                case "SQL Server":
                    _threadQuery = new Thread(MyGlobal.oSQLServerReader.ExecuteNonQuery);
                    break;
                case "MySQL" when bQuery:
                    _threadQuery = (bExtended || btnPaginationOff.Visible) ? new Thread(MyGlobal.oMySQLReader.ExecuteQuery2) : new Thread(MyGlobal.oMySQLReader.ExecuteQueryPaged);
                    break;
                case "MySQL":
                    _threadQuery = new Thread(MyGlobal.oMySQLReader.ExecuteNonQuery);
                    break;
                case "SQLite" when bQuery:
                    _threadQuery = (bExtended || btnPaginationOff.Visible) ? new Thread(MyGlobal.oSQLiteReader.ExecuteQuery2) : new Thread(MyGlobal.oSQLiteReader.ExecuteQueryPaged);
                    break;
                case "SQLite":
                    _threadQuery = new Thread(MyGlobal.oSQLiteReader.ExecuteNonQuery);
                    break;
                case "SQLCipher" when bQuery:
                    _threadQuery = (bExtended || btnPaginationOff.Visible) ? new Thread(MyGlobal.oSQLCipherReader.ExecuteQuery2) : new Thread(MyGlobal.oSQLCipherReader.ExecuteQueryPaged);
                    break;
                case "SQLCipher":
                    _threadQuery = new Thread(MyGlobal.oSQLCipherReader.ExecuteNonQuery);
                    break;
            }

            _threadQuery.IsBackground = true;
            _threadQuery.Start(sQueryText);
        }

        private static bool IsQuery(string sSql, out int iQueryCount, out bool bCommitRollback, out bool bExtended, out bool bIgnoreCommitRollback)
        {
            var bResult = false;
            iQueryCount = 0;
            bCommitRollback = false;
            bExtended = false;
            bIgnoreCommitRollback = false;

            var script = new Devart.Data.Oracle.OracleScript(sSql);

            if (!string.IsNullOrWhiteSpace(sSql) && script.Statements.Count > 0)
            {
                if ("`SELECT`".Contains("`" + script.Statements[0].StatementType.ToString().ToUpper() + "`"))
                {
                    bResult = true;
                }
                else if ("`WITH`".Contains("`" + script.Statements[0].StatementType.ToString().ToUpper() + "`"))
                {
                    bResult = IsWithSql(sSql); //IsQuery
                }
                else if (script.Statements[0].Text.ToUpper().StartsWith("DECLARE") && "`BATCH`".Contains("`" + script.Statements[0].StatementType.ToString().ToUpper() + "`"))
                {
                    bResult = IsWithSql(sSql); //IsQuery
                    bExtended = true && bResult;
                }
                else if (MyGlobal.sDataSource == "SQL Server" && (script.Statements[0].Text.ToUpper().StartsWith("USE") || script.Statements[0].Text.ToUpper().StartsWith("GO")))
                {
                    bIgnoreCommitRollback = true;
                }
                else if (MyGlobal.sDataSource == "SQL Server" && script.Statements[0].Text.ToUpper().StartsWith("EXEC"))
                {
                    //20220711 EXEC 指令，視為查詢，且不能以分頁方式查詢，否則會出錯
                    bResult = false; //20220912 改為非查詢
                    bExtended = true;

                    //20221012 EXEC 指令，例外處理
                    if (script.Statements[0].Text.ToUpper().Replace("\r\n", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").StartsWith("EXEC XP_MSVER"))
                    {
                        bResult = true;
                        bExtended = true; //20221012 此處必須是 true
                    }
                }
                else if (MyGlobal.sDataSource == "MySQL" && script.Statements[0].Text.ToUpper().StartsWith("USE"))
                {
                    bIgnoreCommitRollback = true;
                }
            }

            if ((MyGlobal.sDataSource == "MySQL" || MyGlobal.sDataSource == "PostgreSQL") && script.Statements[0].Text.ToUpper().StartsWith("SHOW"))
            {
                bResult = true;
                bExtended = true;
            }

            if (MyGlobal.sDataSource == "PostgreSQL" && (script.Statements[0].Text.ToUpper().StartsWith("EXPLAIN") || script.Statements[0].Text.ToUpper().StartsWith("SAVEPOINT")))
            {
                bResult = true;
                bExtended = true;
            }

            iQueryCount = script.Statements.Count;

            if (!string.IsNullOrWhiteSpace(sSql) && script.Statements.Count > 0 && "`COMMIT`ROLLBACK`".Contains("`" + script.Statements[0].StatementType.ToString().ToUpper() + "`"))
            {
                bCommitRollback = true;
            }

            script.Dispose();

            return bResult;
        }

        private void btnCancelQuery_Click(object sender, EventArgs e)
        {
            var sMsg = "";
            _sQueryStatus = "Cancel";

            btnQuery.Tag = "";
            btnCancelQuery.Tag = "Cancel";

            lblInfo.Text = MyGlobal.GetLanguageString("Execution aborted...", "form", Name, "msg", "ExecutionAborted", "Text");

            if (tmrQueryTime.Enabled == false)
            {
                return;
            }

            try
            {
                switch (MyGlobal.sDataSource)
                {
                    case "Oracle":
                        sMsg = MyGlobal.oOracleReader.InterruptQuery();
                        sMsg = string.IsNullOrEmpty(sMsg) ? "" : "InterruptQueryMsg: " + sMsg;
                        break;
                    case "PostgreSQL":
                        sMsg = MyGlobal.oPostgreReader.InterruptQuery();
                        sMsg = string.IsNullOrEmpty(sMsg) ? "" : "InterruptQueryMsg: " + sMsg;
                        break;
                    case "SQL Server":
                        sMsg = MyGlobal.oSQLServerReader.InterruptQuery();
                        sMsg = string.IsNullOrEmpty(sMsg) ? "" : "InterruptQueryMsg: " + sMsg;
                        break;
                    case "MySQL":
                        sMsg = MyGlobal.oMySQLReader.InterruptQuery();
                        sMsg = string.IsNullOrEmpty(sMsg) ? "" : "InterruptQueryMsg: " + sMsg;
                        break;
                    case "SQLite":
                        sMsg = MyGlobal.oSQLiteReader.InterruptQuery();
                        sMsg = string.IsNullOrEmpty(sMsg) ? "" : "InterruptQueryMsg: " + sMsg;
                        break;
                    case "SQLCipher":
                        sMsg = MyGlobal.oSQLCipherReader.InterruptQuery();
                        sMsg = string.IsNullOrEmpty(sMsg) ? "" : "InterruptQueryMsg: " + sMsg;
                        break;
                }

                if (_threadQuery != null)
                {
                    try
                    {
                        Application.DoEvents();
                        _threadQuery.Abort();
                    }
                    catch (Exception ex)
                    {
                        sMsg += (string.IsNullOrEmpty(sMsg) ? "" : "\r\n") + "AbortMsg: " + ex.Message;
                    }
                }

                _threadQuery = null;
            }
            catch (Exception ex)
            {
                sMsg += (string.IsNullOrEmpty(sMsg) ? "" : "\r\n") + "FinalMsg: " + ex.Message;

                var iLine = new StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                MessageBox.Show(_sLangText + (iLine == 0 ? "" : "\r\n\r\nErrorLine: " + iLine.ToString() + sMsg), @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void QueryCompleted()
        {
            if (InvokeRequired)
            {
                try
                {
                    var d = new BindDatagrid(BindDataGridHandler);
                    Invoke(d);
                }
                catch (Exception)
                {
                    //
                }
            }
            else
            {
                BindDataGridHandler();
            }

            _threadQuery = null;

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    MyGlobal.oOracleReader.Clear();
                    MyGlobal.oOracleReader.QueryCompleted -= QueryCompleted;
                    break;
                case "PostgreSQL":
                    MyGlobal.oPostgreReader.Clear();
                    MyGlobal.oPostgreReader.QueryCompleted -= QueryCompleted;
                    break;
                case "SQL Server":
                    MyGlobal.oSQLServerReader.Clear();
                    MyGlobal.oSQLServerReader.QueryCompleted -= QueryCompleted;
                    break;
                case "MySQL":
                    MyGlobal.oMySQLReader.Clear();
                    MyGlobal.oMySQLReader.QueryCompleted -= QueryCompleted;
                    break;
                case "SQLite":
                    MyGlobal.oSQLiteReader.Clear();
                    MyGlobal.oSQLiteReader.QueryCompleted -= QueryCompleted;
                    break;
                case "SQLCipher":
                    MyGlobal.oSQLCipherReader.Clear();
                    MyGlobal.oSQLCipherReader.QueryCompleted -= QueryCompleted;
                    break;
            }

            _sQueryStatus = "Complete"; //20191212
        }

        private void BindDataGridHandler()
        {
            var bBreak = false;
            var dtData = new DataTable();
            var dtSchemaTable = new DataTable();

            switch (MyGlobal.sDataSource)
            {
                case "Oracle" when MyGlobal.sGlobalTemp.IndexOf("sqlexecuteerrorpos", StringComparison.Ordinal) == -1:
                    oDataReader = MyGlobal.oOracleReader.Datareader;

                    try
                    {
                        if (oDataReader != null)
                        {
                            dtSchemaTable = oDataReader.GetSchemaTable();
                            dtData.Load(oDataReader);
                        }
                    }
                    catch (Devart.Data.Oracle.OracleException ex)
                    {
                        var sSql = editor.SelectedText;
                        var sErrCode = string.IsNullOrEmpty(ex.Code.ToString()) ? "" : "ErrorCode: " + ex.Code + "\r\n";

                        //顯示錯誤訊息
                        MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + AccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + sErrCode + MyGlobal.sSeparator5 + ex.Message + MyGlobal.sSeparator5 + "" + MyGlobal.sSeparator5 + (ex.Offset + _iQueryTextParametersStart) + MyGlobal.sSeparator5 + sSql;
                    }
                    finally
                    {
                        tmrQueryTime.Enabled = false;
                    }

                    break;
                case "PostgreSQL" when MyGlobal.sGlobalTemp.IndexOf("sqlexecuteerrorpos", StringComparison.Ordinal) == -1:
                    pDataReader = MyGlobal.oPostgreReader.Datareader;

                    try
                    {
                        if (pDataReader != null)
                        {
                            dtSchemaTable = pDataReader.GetSchemaTable();
                            dtData.Load(pDataReader);
                        }
                    }
                    catch (Devart.Data.PostgreSql.PgSqlException ex)
                    {
                        var sSql = editor.SelectedText;

                        //顯示錯誤訊息
                        MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + AccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + "" + MyGlobal.sSeparator5 + ex.Message + MyGlobal.sSeparator5 + "" + MyGlobal.sSeparator5 + (ex.Position + (_bNextPageQuery ? -15 : 0) + _iQueryTextParametersStart) + MyGlobal.sSeparator5 + sSql;
                    }
                    finally
                    {
                        tmrQueryTime.Enabled = false;
                    }

                    break;
                case "SQL Server" when MyGlobal.sGlobalTemp.IndexOf("sqlexecuteerrorpos", StringComparison.Ordinal) == -1:
                    sDataReader = MyGlobal.oSQLServerReader.Datareader;

                    try
                    {
                        if (sDataReader != null)
                        {
                            dtSchemaTable = sDataReader.GetSchemaTable();
                            dtData.Load(sDataReader);
                        }
                    }
                    catch (Devart.Data.SqlServer.SqlException ex)
                    {
                        var iPosition = 0;
                        var sErrMsg = ex.Message;
                        var sSql = editor.SelectedText;
                        var iFrom = sErrMsg.IndexOf("'", StringComparison.Ordinal) + 1;
                        var iTo = sErrMsg.LastIndexOf("'", StringComparison.Ordinal);

                        if (iFrom != -1 && iTo != -1)
                        {
                            //可能情況：出現兩個單字，前後都有雙引號；故從第一個雙引號之後繼續找
                            iTo = sErrMsg.Substring(iFrom).IndexOf("'", StringComparison.Ordinal) + iFrom;

                            //錯誤訊息有明確指出哪個字串
                            var sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                            //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                            if (sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) != -1)
                            {
                                iPosition = sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal);
                            }
                        }

                        //顯示錯誤訊息
                        MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + AccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + "" + MyGlobal.sSeparator5 + sErrMsg + MyGlobal.sSeparator5 + "" + MyGlobal.sSeparator5 + iPosition + MyGlobal.sSeparator5 + sSql;
                    }
                    finally
                    {
                        tmrQueryTime.Enabled = false;
                    }

                    break;
                case "MySQL" when MyGlobal.sGlobalTemp.IndexOf("sqlexecuteerrorpos", StringComparison.Ordinal) == -1:
                    mDataReader = MyGlobal.oMySQLReader.Datareader;

                    try
                    {
                        if (mDataReader != null)
                        {
                            dtSchemaTable = mDataReader.GetSchemaTable();
                            dtData.Load(mDataReader);
                        }
                    }
                    catch (ConstraintException)
                    {
                        //某些 SQL 會引發 ConstraintException 錯誤，例如以下這個
                        //SELECT * FROM information_schema.triggers cc WHERE trigger_schema = 'sakila';
                    }
                    catch (Devart.Data.MySql.MySqlException ex)
                    {
                        var iPosition = 0;
                        var sErrMsg = ex.Message;
                        var sSql = editor.SelectedText;
                        var iFrom = sErrMsg.IndexOf("'", StringComparison.Ordinal) + 1;
                        var iTo = sErrMsg.LastIndexOf("'", StringComparison.Ordinal);

                        if (iFrom != -1 && iTo != -1)
                        {
                            //可能情況：出現兩個單字，前後都有雙引號；故從第一個雙引號之後繼續找
                            iTo = sErrMsg.Substring(iFrom).IndexOf("'", StringComparison.Ordinal) + iFrom;

                            //錯誤訊息有明確指出哪個字串
                            var sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                            //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                            if (sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) != -1)
                            {
                                iPosition = sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal);
                            }
                        }

                        //顯示錯誤訊息
                        MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + AccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + "" + MyGlobal.sSeparator5 + sErrMsg + MyGlobal.sSeparator5 + "" + MyGlobal.sSeparator5 + iPosition + MyGlobal.sSeparator5 + sSql;
                    }
                    finally
                    {
                        tmrQueryTime.Enabled = false;
                    }

                    break;
                case "SQLite" when MyGlobal.sGlobalTemp.IndexOf("sqlexecuteerrorpos", StringComparison.Ordinal) == -1:
                    //qDataReader = MyGlobal.oSQLiteReader.Datareader;

                    try
                    {
                        if (mDataReader != null)
                        {
                            dtSchemaTable = mDataReader.GetSchemaTable();
                            dtData.Load(mDataReader);
                        }
                    }
                    catch (ConstraintException)
                    {
                        //
                    }
                    catch (Devart.Data.SQLite.SQLiteException ex)
                    {
                        var iPosition = 0;
                        var sErrMsg = ex.Message;
                        var sSql = editor.SelectedText;
                        var iFrom = sErrMsg.IndexOf("'", StringComparison.Ordinal) + 1;
                        var iTo = sErrMsg.LastIndexOf("'", StringComparison.Ordinal);

                        if (iFrom != -1 && iTo != -1)
                        {
                            //可能情況：出現兩個單字，前後都有雙引號；故從第一個雙引號之後繼續找
                            iTo = sErrMsg.Substring(iFrom).IndexOf("'", StringComparison.Ordinal) + iFrom;

                            //錯誤訊息有明確指出哪個字串
                            var sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                            //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                            if (sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) != -1)
                            {
                                iPosition = sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal);
                            }
                        }

                        //顯示錯誤訊息
                        MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + AccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + "" + MyGlobal.sSeparator5 + sErrMsg + MyGlobal.sSeparator5 + "" + MyGlobal.sSeparator5 + iPosition + MyGlobal.sSeparator5 + sSql;
                    }
                    finally
                    {
                        tmrQueryTime.Enabled = false;
                    }

                    break;
                case "SQLCipher" when MyGlobal.sGlobalTemp.IndexOf("sqlexecuteerrorpos", StringComparison.Ordinal) == -1:
                    cDataReader = MyGlobal.oSQLCipherReader.Datareader;

                    try
                    {
                        if (mDataReader != null)
                        {
                            dtSchemaTable = mDataReader.GetSchemaTable();
                            dtData.Load(mDataReader);
                        }
                    }
                    catch (ConstraintException)
                    {
                        //
                    }
                    catch (Devart.Data.SQLite.SQLiteException ex)
                    {
                        var iPosition = 0;
                        var sErrMsg = ex.Message;
                        var sSql = editor.SelectedText;
                        var iFrom = sErrMsg.IndexOf("'", StringComparison.Ordinal) + 1;
                        var iTo = sErrMsg.LastIndexOf("'", StringComparison.Ordinal);

                        if (iFrom != -1 && iTo != -1)
                        {
                            //可能情況：出現兩個單字，前後都有雙引號；故從第一個雙引號之後繼續找
                            iTo = sErrMsg.Substring(iFrom).IndexOf("'", StringComparison.Ordinal) + iFrom;

                            //錯誤訊息有明確指出哪個字串
                            var sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                            //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                            if (sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) != -1)
                            {
                                iPosition = sSql.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal);
                            }
                        }

                        //顯示錯誤訊息
                        MyGlobal.sGlobalTemp = "sqlexecuteerrorpos" + MyGlobal.sSeparator + AccessibleDescription + ";" + "" + MyGlobal.sSeparator5 + "" + MyGlobal.sSeparator5 + sErrMsg + MyGlobal.sSeparator5 + "" + MyGlobal.sSeparator5 + iPosition + MyGlobal.sSeparator5 + sSql;
                    }
                    finally
                    {
                        tmrQueryTime.Enabled = false;
                    }

                    break;
            }

            lblQueryTime.Tag = MyGlobal.DateDiff(_dtStartTime, DateTime.Now);
            lblQueryTime.Text = _sQueryTime + @" " + lblQueryTime.Tag;

            tmrQueryTime.Enabled = false;

            //if (btnCancelQuery.Enabled)
            //{
            //    //20201105 此處不變更「取消按鈕」的 Enabled 狀態
            //    //btnCancelQuery.Enabled = false;
            //}

            if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
            {
                if (_iQueryIndex == 1)
                {
                    ArrangeDataTable2(c1TrueDBGrid1, dtData, dtSchemaTable, chkSort.Checked); //BindDataGridHandler, 單一查詢
                }
                else
                {
                    foreach (Control tab in c1DockingTab1.TabPages)
                    {
                        var tabPage = (C1DockingTabPage)tab;

                        foreach (var ctrlTab in tabPage.Controls)
                        {
                            if (ctrlTab.GetType().Name != "C1TrueDBGrid")
                            {
                                continue;
                            }

                            var sTemp = ((C1TrueDBGrid)ctrlTab).Name;

                            if (sTemp != "c1TrueDBGrid" + _iQueryIndex)
                            {
                                continue;
                            }

                            ArrangeDataTable2((C1TrueDBGrid)ctrlTab, dtData, dtSchemaTable, chkSort.Checked); //BindDataGridHandler, 多個查詢
                            c1ThemeController1.SetTheme((C1TrueDBGrid)ctrlTab, "(default)");
                            MyGlobal.SetGridVisualStyle((C1TrueDBGrid)ctrlTab, Convert.ToSingle(MyLibrary.sGridFontSize));

                            ((C1TrueDBGrid)ctrlTab).AllowFilter = false;
                            ((C1TrueDBGrid)ctrlTab).FilterBar = chkShowFilterRow.Checked;
                            ((C1TrueDBGrid)ctrlTab).Filter += C1TrueDBGrid_Filter;
                            ((C1TrueDBGrid)ctrlTab).MouseWheel += c1TrueDBGrid1_MouseWheel;
                            ((C1TrueDBGrid)ctrlTab).KeyDown += Detect_KeyDown;
                            ((C1TrueDBGrid)ctrlTab).OwnerDrawCell += c1TrueDBGrid1_OwnerDrawCell;
                            ((C1TrueDBGrid)ctrlTab).DataView = chkShowGroupingRow.Checked ? DataViewEnum.GroupBy : DataViewEnum.Normal;
                            ((C1TrueDBGrid)ctrlTab).GroupStyle.BackColor = Color.LightYellow;
                            ((C1TrueDBGrid)ctrlTab).GroupStyle.Font = new Font("Microsoft JhengHei", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);

                            //套用 Grid 外觀
                            GridVisualStyle((C1TrueDBGrid)ctrlTab); //BindDatagridHandler
                            GridFontAndBackgroundColor((C1TrueDBGrid)ctrlTab); //BindDatagridHandler
                            GridZoom((C1TrueDBGrid)ctrlTab); //BindDatagridHandler

                            bBreak = true;
                            break;
                        }

                        if (bBreak)
                            break;
                    }
                }

                if (((DataTable)c1TrueDBGrid1.DataSource).Rows.Count > 0)
                { 
                    if (MyLibrary.sGridNullShowAs.ToUpper() != "NONE")
                    {
                        var s1 = new Style {ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridNullShowColor)};

                        for (var i = 0; i < c1TrueDBGrid1.Columns.Count; i++)
                        {
                            //套用「使用者指定的 NULL」顯示格式
                            c1TrueDBGrid1.Splits[_SplitsNum].DisplayColumns[i].AddRegexCellStyle(CellStyleFlag.AllCells, s1, MyLibrary.sGridNullShowAs);
                        }
                    }

                    cboFindGrid.Enabled = true;
                }
            }

            SetDockingTabControl(); //BindDataGridHandler

            if (!MyGlobal.bProgressCancel)
            {
                return;
            }

            _sLangText = MyGlobal.GetLanguageString("This operation has been cancelled.", "Global", "Global", "msg", "CancelByUser", "Text");
            UpdateStatusBarInfo(_sLangText);
            Cursor = Cursors.Default;
        }

        private static DataTable ArrangeSchemaTable(DataTable dt) //如果欄位名稱重覆，欄位名稱後面加上數字流水號，以免 SchemaTable 與 DataTable 對不起來，引發例外錯誤
        {
            var sDistinct = "`";
            var dtData = new DataTable();

            for (var i = 0; i < dt.Columns.Count; i++)
            {
                dtData.Columns.Add(dt.Columns[i].ColumnName, typeof(string));
            }

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rowData = dtData.NewRow();

                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == 0)
                    {
                        var sColumnName = dt.Rows[i][dt.Columns[j].ColumnName].ToString();

                        for (var k = 0; k < 20; k++)
                        {
                            if (sDistinct.IndexOf("`" + sColumnName + "`", StringComparison.Ordinal) == -1)
                            {
                                sDistinct += sColumnName + "`";
                                break;
                            }

                            if (sDistinct.IndexOf("`" + sColumnName + (k + 1) + "`", StringComparison.Ordinal) != -1)
                                continue;

                            sDistinct += sColumnName + (k + 1) + "`";
                            sColumnName += (k + 1);
                            break;
                        }

                        rowData[dt.Columns[j].ColumnName] = sColumnName;
                    }
                    else
                    {
                        rowData[dt.Columns[j].ColumnName] = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                    }
                }

                dtData.Rows.Add(rowData);
            }

            return dtData;
        }

        private void ArrangeDataTable2(C1TrueDBGrid c1Grid, DataTable dtData, DataTable dtSchemaTable, bool bSort = true, bool bForceResize = false)
        {
            if (btnPaginationOn.Visible)
            {
                btnNextPage.Enabled = dtData.Rows.Count != 0 && dtData.Rows.Count >= Convert.ToInt32(btnPaginationOn.Tag.ToString());

                if (btnNextPage.Enabled == false)
                {
                    //已經是最後一頁了，變更 ToolTip 為「已經是最後一頁了！」
                    btnNextPage.ToolTip = MyGlobal.GetLanguageString("It's the last page!", "form", Name, "object", "btnNextPage_LastPage", "ToolTipText");
                }
            }

            if (chkRawDataMode.Checked)
            {
                if (_bNextPageQuery == false || btnAppendingQueriesOff.Visible)
                {
                    c1Grid.DataSource = dtData;
                }
                else
                {
                    if (dtData.Rows.Count > 0)
                    {
                        _dtNextPage.Merge(dtData, true);
                    }

                    dtData.Dispose();
                    c1Grid.DataSource = _dtNextPage;
                }

                c1Grid.Show();
            }
            else
            {
                switch (MyGlobal.sDataSource)
                {
                    case "Oracle":
                        ArrangeDataTable_Oracle(c1Grid, dtData, dtSchemaTable, bSort, bForceResize);
                        break;
                    case "PostgreSQL":
                        ArrangeDataTable_PostgreSQL(c1Grid, dtData, dtSchemaTable, bSort, bForceResize);
                        break;
                    case "SQL Server":
                        ArrangeDataTable_SQLServer(c1Grid, dtData, dtSchemaTable, bSort, bForceResize);
                        break;
                    case "MySQL":
                        ArrangeDataTable_MySQL(c1Grid, dtData, dtSchemaTable, bSort, bForceResize);
                        break;
                    case "SQLite":
                    case "SQLCipher":
                        ArrangeDataTable_SQLite(c1Grid, dtData, dtSchemaTable, bSort, bForceResize);
                        break;
                }
            }
        }

        private void ArrangeDataTable_Oracle(C1TrueDBGrid c1Grid, DataTable dtData, DataTable dtSchemaTable, bool bSort = true, bool bForceResize = false)
        {
            string sTemp;
            var sHeader = "";
            var sDataType = "";
            string[] sArraySeparators = { "`" };
            var dtSortedData = new DataTable();
            DataRow[] dtRow;
            var sFormatDataType = "";

            if (chkShowColumnType.Checked)
            {
                dtSchemaTable = ArrangeSchemaTable(dtSchemaTable); //針對重覆的欄位名稱，後面加上流水號 (這樣才會跟 dtData 相符)
            }

            MyGlobal.dtSchemaTable = dtSchemaTable;
            _dtSchemaTableExport = dtSchemaTable;

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sTemp = dtData.Columns[i].DataType.ToString().Replace("System.", "");
                sFormatDataType = sTemp;

                if (chkShowColumnType.Checked)
                {
                    if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                    {
                        dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[i].ColumnName.Replace("'", "''") + "'");

                        if (dtRow.Length > 0)
                        {
                            sFormatDataType = MyGlobal.GetDataTypeFormat_Oracle(dtRow, out sDataType); //ArrangeDataTable, Part 1：取得 ColumnType, 顯示在 header
                        }
                    }

                    sHeader += dtData.Columns[i].ColumnName + "\r\n" + sFormatDataType + "`";
                }
                else
                {
                    sHeader += dtData.Columns[i].ColumnName + "`";
                }
            }

            var sArrayHeader = sHeader.Split(sArraySeparators, StringSplitOptions.RemoveEmptyEntries);

            if (bSort)
            {
                Array.Sort(sArrayHeader, string.CompareOrdinal); //ASCII排序
            }

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sFormatDataType = "string";
                sTemp = sArrayHeader[i].Replace("'", "''");
                sTemp = sTemp.IndexOf("\r\n", StringComparison.Ordinal) == -1 ? sTemp : sTemp.Substring(0, sTemp.IndexOf("\r\n", StringComparison.Ordinal));
                dtRow = dtSchemaTable.Select("ColumnName = '" + sTemp.Replace("'", "''") + "'");

                if (dtRow.Length > 0)
                {
                    sFormatDataType = MyGlobal.GetDataTypeFormat_Oracle(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                }

                switch (sFormatDataType.ToLower())
                {
                    //這裡要全部使用 string，否則一遇到 null 就會出錯，因為 null 可能會被填入 <NULL> 之類的字串
                    default:
                        dtSortedData.Columns.Add(sArrayHeader[i], typeof(string));
                        break;
                }
            }

            try
            {
                for (var i = 0; i < dtData.Rows.Count; i++)
                {
                    var rowData = dtSortedData.NewRow();

                    for (var j = 0; j < dtData.Columns.Count; j++)
                    {
                        Application.DoEvents();

                        if (Convert.IsDBNull(dtData.Rows[i][dtData.Columns[j].ColumnName]))
                        {
                            sTemp = MyLibrary.sGridNullShowAs.ToUpper() == "NONE" ? "" : MyLibrary.sGridNullShowAs;
                        }
                        else
                        {
                            sTemp = dtData.Rows[i][dtData.Columns[j].ColumnName].ToString();
                        }

                        switch (dtData.Columns[j].DataType.ToString().Replace("System.", "").ToUpper())
                        {
                            case "DATETIME":
                                if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                {
                                    sTemp = Convert.ToDateTime(sTemp).ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                                }

                                break;
                            case "TIMESPAN":
                                if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                {
                                    if (sTemp.IndexOf(".", 0, StringComparison.Ordinal) == -1)
                                    {
                                        sTemp = "+00 " + Convert.ToDateTime(sTemp).ToString("HH:mm:ss");
                                    }
                                    else
                                    {
                                        int.TryParse(sTemp.Substring(0, sTemp.IndexOf(".", 0, StringComparison.Ordinal)), out int iTemp);
                                        sTemp = "+" + iTemp.ToString("00") + " " + Convert.ToDateTime(sTemp.Substring(sTemp.IndexOf(".", 0, StringComparison.Ordinal) + 1)).ToString("HH:mm:ss");
                                    }
                                }

                                break;
                        }

                        if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                        {
                            dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[j].ColumnName.Replace("'", "''") + "'");

                            if (dtRow.Length > 0)
                            {
                                sFormatDataType = MyGlobal.GetDataTypeFormat_Oracle(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                            }

                            if ("`CLOB`NCLOB`".Contains("`" + sFormatDataType.ToUpper() + "`"))
                            {
                                rowData[dtData.Columns[j].ColumnName + (chkShowColumnType.Checked == false ? "" : "\r\n" + sFormatDataType)] = mnuPreviewCLOBData.Checked == false ? sTemp == MyLibrary.sGridNullShowAs ? sTemp : "(CLOB)" : sTemp;
                            }
                            else if ("`BLOB`".Contains("`" + sFormatDataType.ToUpper() + "`"))
                            {
                                //BLOB: 如果不是 NULL，則直接顯示 "(BLOB)"
                                rowData[dtData.Columns[j].ColumnName + (chkShowColumnType.Checked == false ? "" : "\r\n" + sFormatDataType)] = sTemp == MyLibrary.sGridNullShowAs ? sTemp : "(BLOB)";
                            }
                            else
                            {
                                rowData[dtData.Columns[j].ColumnName + (chkShowColumnType.Checked == false ? "" : "\r\n" + sFormatDataType)] = sTemp;
                            }
                        }
                        else
                        {
                            rowData[dtData.Columns[j].ColumnName + (chkShowColumnType.Checked == false ? "" : "\r\n" + dtData.Columns[j].DataType.ToString().Replace("System.", "").ToUpper())] = sTemp;
                        }
                    }

                    dtSortedData.Rows.Add(rowData);

                    if (btnCancelQuery.Tag.ToString() == "Cancel")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (btnCancelQuery.Tag.ToString() == "Cancel")
            {
                return;
            }

            MyGlobal.dtSchemaTable = dtSortedData;

            int iTotalRows;

            if (_bNextPageQuery == false || btnAppendingQueriesOff.Visible)
            {
                iTotalRows = dtSortedData.Rows.Count;
                c1Grid.DataSource = dtSortedData;
            }
            else
            {
                if (dtData.Rows.Count > 0)
                {
                    _dtNextPage.Merge(dtSortedData, true);
                }

                iTotalRows = _dtNextPage.Rows.Count;

                dtSortedData.Dispose();
                c1Grid.DataSource = _dtNextPage;
            }

            c1Grid.Show();

            foreach (C1DataColumn col1 in c1Grid.Columns)
            {
                if (col1.Caption.IndexOf("\r\n", 0, StringComparison.Ordinal) != -1)
                {
                    sHeader = col1.Caption.Replace("\r\n", "\n").Split('\n')[0];
                }
                else if (col1.Caption.IndexOf("\r", 0, StringComparison.Ordinal) != -1)
                {
                    sHeader = col1.Caption.Split('\r')[0];
                }
                else
                {
                    sHeader = col1.Caption;
                }

                sDataType = "string";
                dtRow = dtSchemaTable.Select("ColumnName = '" + sHeader.Replace("'", "''") + "'");

                if (dtRow.Length > 0)
                {
                    MyGlobal.GetDataTypeFormat_Oracle(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                }

                switch (sDataType.ToLower())
                {
                    case "datetime":
                        col1.Tag = "datetime";
                        break;
                    case "int": //20220715 int 改歸在 number，避免誤判為 int 造成匯出 Excel 時「型別」錯誤
                    case "number":
                        col1.Tag = "number";
                        c1Grid.Splits[0].DisplayColumns[col1.Caption].Style.HorizontalAlignment = AlignHorzEnum.Far; //20220823 數值欄位，靠右顯示
                        break;
                    default:
                        col1.Tag = "string";
                        break;
                }
            }

            Application.DoEvents();

            //Auto Size
            if (iTotalRows <= 0 || !chkSize.Checked && !bForceResize)
            {
                return;
            }

            foreach (C1DisplayColumn col in c1Grid.Splits[_SplitsNum].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 2000;
                }

                if ("`500`1000`1500`2000`".Contains("`" + MyGlobal.sGridMaxWidth + "`") && col.Width > Convert.ToInt16(MyGlobal.sGridMaxWidth))
                {
                    col.Width = Convert.ToInt16(MyGlobal.sGridMaxWidth);
                }
            }

            c1Grid.Refresh();
        }

        private void ArrangeDataTable_PostgreSQL(C1TrueDBGrid c1Grid, DataTable dtData, DataTable dtSchemaTable, bool bSort = true, bool bForceResize = false)
        {
            string sTemp;
            var sHeader = "";
            string sDataType;
            string[] sArraySeparators = { "`" };
            var dtSortedData = new DataTable();
            DataRow[] dtRow;
            var sFormatDataType = "";

            if (chkShowColumnType.Checked)
            {
                dtSchemaTable = ArrangeSchemaTable(dtSchemaTable); //針對重覆的欄位名稱，後面加上流水號 (這樣才會跟 dtData 相符)
            }

            MyGlobal.dtSchemaTable = dtSchemaTable;
            _dtSchemaTableExport = dtSchemaTable;

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sTemp = dtData.Columns[i].DataType.ToString().Replace("System.", "");
                sFormatDataType = sTemp;

                if (chkShowColumnType.Checked)
                {
                    if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                    {
                        dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[i].ColumnName.Replace("'", "''") + "'");

                        if (dtRow.Length > 0)
                        {
                            sFormatDataType = MyGlobal.GetDataTypeFormat_PostgreSQL(dtRow, out sDataType); //ArrangeDataTable, Part 1：取得 ColumnType, 顯示在 header
                        }
                    }

                    sHeader += dtData.Columns[i].ColumnName + "\r\n" + sFormatDataType + "`";
                }
                else
                {
                    sHeader += dtData.Columns[i].ColumnName + "`";
                }
            }

            var sArrayHeader = sHeader.Split(sArraySeparators, StringSplitOptions.RemoveEmptyEntries);

            if (bSort)
            {
                Array.Sort(sArrayHeader, string.CompareOrdinal); //ASCII排序
            }

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sFormatDataType = "string";
                sTemp = sArrayHeader[i].Replace("'", "''");
                sTemp = sTemp.IndexOf("\r\n", StringComparison.Ordinal) == -1 ? sTemp : sTemp.Substring(0, sTemp.IndexOf("\r\n", StringComparison.Ordinal));
                dtRow = dtSchemaTable.Select("ColumnName = '" + sTemp.Replace("'", "''") + "'");

                if (dtRow.Length > 0)
                {
                    sFormatDataType = MyGlobal.GetDataTypeFormat_PostgreSQL(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                }

                switch (sFormatDataType.ToLower())
                {
                    //這裡要全部使用 string，否則一遇到 null 就會出錯，因為 null 可能會被填入 <NULL> 之類的字串
                    default:
                        dtSortedData.Columns.Add(sArrayHeader[i], typeof(string));
                        break;
                }
            }

            try
            {
                for (var i = 0; i < dtData.Rows.Count; i++)
                {
                    var rowData = dtSortedData.NewRow();

                    for (var j = 0; j < dtData.Columns.Count; j++)
                    {
                        Application.DoEvents();

                        if (Convert.IsDBNull(dtData.Rows[i][dtData.Columns[j].ColumnName]))
                        {
                            sTemp = MyLibrary.sGridNullShowAs.ToUpper() == "NONE" ? "" : MyLibrary.sGridNullShowAs;
                        }
                        else
                        {
                            sTemp = dtData.Rows[i][dtData.Columns[j].ColumnName].ToString();
                        }

                        switch (dtData.Columns[j].DataType.ToString().Replace("System.", "").ToUpper())
                        {
                            case "DATETIME":
                                if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                {
                                    sTemp = Convert.ToDateTime(sTemp).ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                                }

                                break;
                            case "TIMESPAN":
                                if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                {
                                    sTemp = Convert.ToDateTime(sTemp).ToString("HH:mm:ss");
                                }

                                break;
                        }

                        if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                        {
                            dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[j].ColumnName.Replace("'", "''") + "'");

                            if (dtRow.Length > 0)
                            {
                                sFormatDataType = MyGlobal.GetDataTypeFormat_PostgreSQL(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                            }

                            //20220722 針對 "date"/"time with time zone" 兩個型態，調整顯示格式，使其與 paAdmin 一致
                            switch (sFormatDataType)
                            {
                                case "date":
                                {
                                    if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                    {
                                        sTemp = Convert.ToDateTime(sTemp).ToString(MyLibrary.sDateFormat);
                                    }

                                    break;
                                }
                                case "time with time zone":
                                {
                                    if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                    {
                                        sTemp = Convert.ToDateTime(sTemp).ToString("HH:mm:sszzz"); //zzz 是 time zone
                                    }

                                    break;
                                }
                            }

                            rowData[dtData.Columns[j].ColumnName + (chkShowColumnType.Checked == false ? "" : "\r\n" + sFormatDataType)] = (("`CLOB`NCLOB`".Contains("`" + sFormatDataType.ToUpper() + "`") && mnuPreviewCLOBData.Checked) ? "(CLOB)" : sTemp);
                        }
                        else
                        {
                            rowData[dtData.Columns[j].ColumnName + (chkShowColumnType.Checked == false ? "" : "\r\n" + dtData.Columns[j].DataType.ToString().Replace("System.", "").ToLower())] = sTemp;
                        }
                    }

                    dtSortedData.Rows.Add(rowData);

                    if (btnCancelQuery.Tag.ToString() == "Cancel")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (btnCancelQuery.Tag.ToString() == "Cancel")
            {
                return;
            }

            MyGlobal.dtSchemaTable = dtSortedData;

            int iTotalRows;

            if (_bNextPageQuery == false || btnAppendingQueriesOff.Visible)
            {
                iTotalRows = dtSortedData.Rows.Count;
                c1Grid.DataSource = dtSortedData;
            }
            else
            {
                if (dtData.Rows.Count > 0)
                {
                    _dtNextPage.Merge(dtSortedData, true);
                }

                iTotalRows = _dtNextPage.Rows.Count;

                dtSortedData.Dispose();
                c1Grid.DataSource = _dtNextPage;
            }

            c1Grid.Show();

            foreach (C1DataColumn col1 in c1Grid.Columns)
            {
                if (col1.Caption.IndexOf("\r\n", 0, StringComparison.Ordinal) != -1)
                {
                    sHeader = col1.Caption.Replace("\r\n", "\n").Split('\n')[0];
                }
                else if (col1.Caption.IndexOf("\r", 0, StringComparison.Ordinal) != -1)
                {
                    sHeader = col1.Caption.Split('\r')[0];
                }
                else
                {
                    sHeader = col1.Caption;
                }

                sDataType = "string";
                dtRow = dtSchemaTable.Select("ColumnName = '" + sHeader.Replace("'", "''") + "'");

                if (dtRow.Length > 0)
                {
                    MyGlobal.GetDataTypeFormat_PostgreSQL(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                }

                switch (sDataType.ToLower())
                {
                    case "datetime":
                        col1.Tag = "datetime";
                        break;
                    case "int":
                    case "number":
                        col1.Tag = "number";
                        c1Grid.Splits[0].DisplayColumns[col1.Caption].Style.HorizontalAlignment = AlignHorzEnum.Far; //20220823 數值欄位，靠右顯示
                        break;
                    default:
                        col1.Tag = "string";
                        break;
                }
            }

            Application.DoEvents();

            //Auto Size
            if (iTotalRows <= 0 || (!chkSize.Checked && !bForceResize))
            {
                return;
            }

            foreach (C1DisplayColumn col in c1Grid.Splits[_SplitsNum].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 2000;
                }

                if ("`500`1000`1500`2000`".Contains("`" + MyGlobal.sGridMaxWidth + "`") && col.Width > Convert.ToInt16(MyGlobal.sGridMaxWidth))
                {
                    col.Width = Convert.ToInt16(MyGlobal.sGridMaxWidth);
                }
            }

            c1Grid.Refresh();
        }

        private void ArrangeDataTable_SQLServer(C1TrueDBGrid c1Grid, DataTable dtData, DataTable dtSchemaTable, bool bSort = true, bool bForceResize = false)
        {
            string sTemp;
            var sHeader = "";
            var sDataType = "";
            string[] sArraySeparators = { "`" };
            var dtSortedData = new DataTable();
            DataRow[] dtRow;
            var sFormatDataType = "";

            if (chkShowColumnType.Checked)
            {
                dtSchemaTable = ArrangeSchemaTable(dtSchemaTable); //針對重覆的欄位名稱，後面加上流水號 (這樣才會跟 dtData 相符)
            }

            MyGlobal.dtSchemaTable = dtSchemaTable;
            _dtSchemaTableExport = dtSchemaTable;

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sTemp = dtData.Columns[i].DataType.ToString().Replace("System.", "");
                sFormatDataType = sTemp;

                if (chkShowColumnType.Checked)
                {
                    if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                    {
                        dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[i].ColumnName.Replace("'", "''") + "'");

                        if (dtRow.Length > 0)
                        {
                            sFormatDataType = MyGlobal.GetDataTypeFormat_SQLServer(dtRow, out sDataType); //ArrangeDataTable, Part 1：取得 ColumnType, 顯示在 header
                        }
                    }

                    sHeader += dtData.Columns[i].ColumnName + "\r\n" + sFormatDataType + "`";
                }
                else
                {
                    sHeader += dtData.Columns[i].ColumnName + "`";
                }
            }

            var sArrayHeader = sHeader.Split(sArraySeparators, StringSplitOptions.RemoveEmptyEntries);

            if (bSort)
            {
                Array.Sort(sArrayHeader, string.CompareOrdinal); //ASCII排序
            }

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sFormatDataType = "string";
                sTemp = sArrayHeader[i].Replace("'", "''");
                sTemp = sTemp.IndexOf("\r\n", StringComparison.Ordinal) == -1 ? sTemp : sTemp.Substring(0, sTemp.IndexOf("\r\n", StringComparison.Ordinal));
                dtRow = dtSchemaTable.Select("ColumnName = '" + sTemp.Replace("'", "''") + "'");

                if (dtRow.Length > 0)
                {
                    sFormatDataType = MyGlobal.GetDataTypeFormat_SQLServer(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                }

                switch (sFormatDataType.ToLower())
                {
                    //這裡要全部使用 string，否則一遇到 null 就會出錯，因為 null 可能會被填入 <NULL> 之類的字串
                    default:
                        dtSortedData.Columns.Add(sArrayHeader[i], typeof(string));
                        break;
                }
            }

            try
            {
                for (var i = 0; i < dtData.Rows.Count; i++)
                {
                    var rowData = dtSortedData.NewRow();

                    for (var j = 0; j < dtData.Columns.Count; j++)
                    {
                        Application.DoEvents();

                        if (Convert.IsDBNull(dtData.Rows[i][dtData.Columns[j].ColumnName]))
                        {
                            sTemp = MyLibrary.sGridNullShowAs.ToUpper() == "NONE" ? "" : MyLibrary.sGridNullShowAs;
                        }
                        else
                        {
                            sTemp = dtData.Rows[i][dtData.Columns[j].ColumnName].ToString();
                        }

                        var sDataTypeNew = dtData.Columns[j].DataType.ToString().Replace("System.", "").ToUpper();

                        if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                        {
                            dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[j].ColumnName.Replace("'", "''") + "'");

                            if (dtRow.Length > 0)
                            {
                                sDataTypeNew = dtRow[0]["DataTypeName"].ToString().ToUpper();
                            }
                        }

                        switch (sDataTypeNew)
                        {
                            case "DATE":
                                if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                {
                                    sTemp = Convert.ToDateTime(sTemp).ToString(MyLibrary.sDateFormat);
                                }

                                break;
                            case "DATETIME":
                                if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                {
                                    sTemp = Convert.ToDateTime(sTemp).ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                                }

                                break;
                            case "TIMESPAN":
                                if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                {
                                    sTemp = Convert.ToDateTime(sTemp).ToString("HH:mm:ss");
                                }

                                break;
                            case "BIT":
                                sTemp = sTemp.ToUpper() == "FALSE" ? "0" : "1";
                                break;
                        }

                        if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                        {
                            dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[j].ColumnName.Replace("'", "''") + "'");

                            if (dtRow.Length > 0)
                            {
                                sFormatDataType = MyGlobal.GetDataTypeFormat_SQLServer(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                            }

                            rowData[dtData.Columns[j].ColumnName + (chkShowColumnType.Checked == false ? "" : "\r\n" + sFormatDataType)] = "`CLOB`NCLOB`".Contains("`" + sFormatDataType.ToUpper() + "`") && mnuPreviewCLOBData.Checked ? "(CLOB)" : sTemp;
                        }
                        else
                        {
                            rowData[dtData.Columns[j].ColumnName + (chkShowColumnType.Checked == false ? "" : "\r\n" + dtData.Columns[j].DataType.ToString().Replace("System.", "").ToLower())] = sTemp;
                        }
                    }

                    dtSortedData.Rows.Add(rowData);

                    if (btnCancelQuery.Tag.ToString() == "Cancel")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                var sTemp2 = ex.Message;

                if (sTemp2.IndexOf("does not belong to table", 0, StringComparison.Ordinal) != -1)
                {
                    sTemp2 = sTemp2.Replace("does not belong to table .", "does not belong to table.");
                    sTemp2 += "\r\n\r\n" + MyGlobal.GetLanguageString("", "form", Name, "msg", "ColumnAliasName", "Text");
                }

                _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", sTemp2, true);
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (btnCancelQuery.Tag.ToString() == "Cancel")
            {
                return;
            }

            MyGlobal.dtSchemaTable = dtSortedData;

            int iTotalRows;

            if (_bNextPageQuery == false || btnAppendingQueriesOff.Visible)
            {
                iTotalRows = dtSortedData.Rows.Count;
                c1Grid.DataSource = dtSortedData;
            }
            else
            {
                if (dtData.Rows.Count > 0)
                {
                    _dtNextPage.Merge(dtSortedData, true);
                }

                iTotalRows = _dtNextPage.Rows.Count;

                dtSortedData.Dispose();
                c1Grid.DataSource = _dtNextPage;
            }

            c1Grid.Show();

            //匯出至 Excel，要依 col.Tag 判斷它的型態
            foreach (C1DataColumn col1 in c1Grid.Columns)
            {
                if (col1.Caption.IndexOf("\r\n", 0, StringComparison.Ordinal) != -1)
                {
                    sHeader = col1.Caption.Replace("\r\n", "\n").Split('\n')[0];
                }
                else if (col1.Caption.IndexOf("\r", 0, StringComparison.Ordinal) != -1)
                {
                    sHeader = col1.Caption.Split('\r')[0];
                }
                else
                {
                    sHeader = col1.Caption;
                }

                sFormatDataType = "string";
                dtRow = dtSchemaTable.Select("ColumnName = '" + sHeader.Replace("'", "''") + "'");

                if (dtRow.Length > 0)
                {
                    sFormatDataType = MyGlobal.GetDataTypeFormat_SQLServer(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                }

                switch (sDataType.ToLower())
                {
                    case "datetime":
                        col1.Tag = "datetime";
                        break;
                    case "int": //20220715 int 改歸在 number，避免誤判為 int 造成匯出 Excel 時「型別」錯誤
                    case "number":
                        col1.Tag = "number";
                        c1Grid.Splits[0].DisplayColumns[col1.Caption].Style.HorizontalAlignment = AlignHorzEnum.Far; //20220823 數值欄位，靠右顯示
                        break;
                    default:
                        //20220421 這幾個欄位要特別處理！
                        if (sFormatDataType.StartsWith("nchar") || sFormatDataType.StartsWith("nvarchar") || sFormatDataType.StartsWith("ntext"))
                        {
                            col1.Tag = "nstring";
                        }
                        else
                        {
                            col1.Tag = "string";
                        }

                        break;
                }
            }

            Application.DoEvents();

            //Auto Size
            if (iTotalRows <= 0 || !chkSize.Checked && !bForceResize)
            {
                return;
            }

            foreach (C1DisplayColumn col in c1Grid.Splits[_SplitsNum].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 2000;
                }

                if ("`500`1000`1500`2000`".Contains("`" + MyGlobal.sGridMaxWidth + "`") && col.Width > Convert.ToInt16(MyGlobal.sGridMaxWidth))
                {
                    col.Width = Convert.ToInt16(MyGlobal.sGridMaxWidth);
                }
            }

            c1Grid.Refresh();
        }

        private void ArrangeDataTable_MySQL(C1TrueDBGrid c1Grid, DataTable dtData, DataTable dtSchemaTable, bool bSort = true, bool bForceResize = false)
        {
            string sTemp;
            var sHeader = "";
            var sDataType = "";
            string[] sArraySeparators = { "`" };
            var dtSortedData = new DataTable();
            DataRow[] dtRow;
            var sFormatDataType = "";

            if (chkShowColumnType.Checked)
            {
                dtSchemaTable = ArrangeSchemaTable(dtSchemaTable); //針對重覆的欄位名稱，後面加上流水號 (這樣才會跟 dtData 相符)
            }

            MyGlobal.dtSchemaTable = dtSchemaTable;
            _dtSchemaTableExport = dtSchemaTable;

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sTemp = dtData.Columns[i].DataType.ToString().Replace("System.", "");
                sFormatDataType = sTemp;

                if (chkShowColumnType.Checked)
                {
                    if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                    {
                        dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[i].ColumnName.Replace("'", "''") + "'");

                        if (dtRow.Length > 0)
                        {
                            sFormatDataType = MyGlobal.GetDataTypeFormat_MySQL(dtRow, out sDataType); //ArrangeDataTable, Part 1：取得 ColumnType, 顯示在 header
                        }
                    }

                    sHeader += dtData.Columns[i].ColumnName + "\r\n" + sFormatDataType + "`";
                }
                else
                {
                    sHeader += dtData.Columns[i].ColumnName + "`";
                }
            }

            var sArrayHeader = sHeader.Split(sArraySeparators, StringSplitOptions.RemoveEmptyEntries);

            if (bSort)
            {
                Array.Sort(sArrayHeader, string.CompareOrdinal); //ASCII排序
            }

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sFormatDataType = "string";
                sTemp = sArrayHeader[i].Replace("'", "''");
                sTemp = sTemp.IndexOf("\r\n", StringComparison.Ordinal) == -1 ? sTemp : sTemp.Substring(0, sTemp.IndexOf("\r\n", StringComparison.Ordinal));
                dtRow = dtSchemaTable.Select("ColumnName = '" + sTemp.Replace("'", "''") + "'");

                if (dtRow.Length > 0)
                {
                    sFormatDataType = MyGlobal.GetDataTypeFormat_MySQL(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                }

                switch (sFormatDataType.ToLower())
                {
                    //這裡要全部使用 string，否則一遇到 null 就會出錯，因為 null 可能會被填入 <NULL> 之類的字串
                    default:
                        dtSortedData.Columns.Add(sArrayHeader[i], typeof(string));
                        break;
                }
            }

            try
            {
                for (var i = 0; i < dtData.Rows.Count; i++)
                {
                    var rowData = dtSortedData.NewRow();

                    for (var j = 0; j < dtData.Columns.Count; j++)
                    {
                        Application.DoEvents();

                        if (Convert.IsDBNull(dtData.Rows[i][dtData.Columns[j].ColumnName]))
                        {
                            sTemp = MyLibrary.sGridNullShowAs.ToUpper() == "NONE" ? "" : MyLibrary.sGridNullShowAs;
                        }
                        else
                        {
                            sTemp = dtData.Rows[i][dtData.Columns[j].ColumnName].ToString();
                        }

                        var sDataTypeNew = dtData.Columns[j].DataType.ToString().Replace("System.", "").ToUpper();

                        if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                        {
                            dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[j].ColumnName.Replace("'", "''") + "'");

                            if (dtRow.Length > 0)
                            {
                                sDataTypeNew = dtRow[0]["DataType"].ToString().ToUpper();
                            }
                        }

                        switch (sDataTypeNew)
                        {
                            case "DATETIME":
                                if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                {
                                    sTemp = Convert.ToDateTime(sTemp).ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                                }

                                break;
                            case "TIMESPAN":
                                if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                {
                                    sTemp = Convert.ToDateTime(sTemp).ToString("HH:mm:ss");
                                }

                                break;
                            case "BIT":
                                sTemp = sTemp.ToUpper() == "FALSE" ? "0" : "1";
                                break;
                        }

                        if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                        {
                            dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[j].ColumnName.Replace("'", "''") + "'");

                            if (dtRow.Length > 0)
                            {
                                sFormatDataType = MyGlobal.GetDataTypeFormat_MySQL(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                            }

                            rowData[dtData.Columns[j].ColumnName + (chkShowColumnType.Checked == false ? "" : "\r\n" + sFormatDataType)] = "`CLOB`NCLOB`".Contains("`" + sFormatDataType.ToUpper() + "`") && mnuPreviewCLOBData.Checked ? "(CLOB)" : sTemp;
                        }
                        else
                        {
                            rowData[dtData.Columns[j].ColumnName + (chkShowColumnType.Checked == false ? "" : "\r\n" + dtData.Columns[j].DataType.ToString().Replace("System.", "").ToLower())] = sTemp;
                        }
                    }

                    dtSortedData.Rows.Add(rowData);

                    if (btnCancelQuery.Tag.ToString() == "Cancel")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (btnCancelQuery.Tag.ToString() == "Cancel")
            {
                return;
            }

            MyGlobal.dtSchemaTable = dtSortedData;

            int iTotalRows;

            if (_bNextPageQuery == false || btnAppendingQueriesOff.Visible)
            {
                iTotalRows = dtSortedData.Rows.Count;
                c1Grid.DataSource = dtSortedData;
            }
            else
            {
                if (dtData.Rows.Count > 0)
                {
                    _dtNextPage.Merge(dtSortedData, true);
                }

                iTotalRows = _dtNextPage.Rows.Count;

                dtSortedData.Dispose();
                c1Grid.DataSource = _dtNextPage;
            }

            c1Grid.Show();

            //匯出至 Excel，要依 col.Tag 判斷它的型態
            foreach (C1DataColumn col1 in c1Grid.Columns)
            {
                if (col1.Caption.IndexOf("\r\n", 0, StringComparison.Ordinal) != -1)
                {
                    sHeader = col1.Caption.Replace("\r\n", "\n").Split('\n')[0];
                }
                else if (col1.Caption.IndexOf("\r", 0, StringComparison.Ordinal) != -1)
                {
                    sHeader = col1.Caption.Split('\r')[0];
                }
                else
                {
                    sHeader = col1.Caption;
                }

                sDataType = "string";
                dtRow = dtSchemaTable.Select("ColumnName = '" + sHeader.Replace("'", "''") + "'");

                if (dtRow.Length > 0)
                {
                    MyGlobal.GetDataTypeFormat_MySQL(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                }

                switch (sDataType.ToLower())
                {
                    case "datetime":
                        col1.Tag = "datetime";
                        break;
                    case "int": //20220715 int 改歸在 number，避免誤判為 int 造成匯出 Excel 時「型別」錯誤
                    case "number":
                        col1.Tag = "number";
                        c1Grid.Splits[0].DisplayColumns[col1.Caption].Style.HorizontalAlignment = AlignHorzEnum.Far; //20220823 數值欄位，靠右顯示
                        break;
                    default:
                        col1.Tag = "string";
                        break;
                }
            }

            Application.DoEvents();

            //Auto Size
            if (iTotalRows <= 0 || !chkSize.Checked && !bForceResize)
            {
                return;
            }

            foreach (C1DisplayColumn col in c1Grid.Splits[_SplitsNum].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 2000;
                }

                if ("`500`1000`1500`2000`".Contains("`" + MyGlobal.sGridMaxWidth + "`") && col.Width > Convert.ToInt16(MyGlobal.sGridMaxWidth))
                {
                    col.Width = Convert.ToInt16(MyGlobal.sGridMaxWidth);
                }
            }

            c1Grid.Refresh();
        }

        private void ArrangeDataTable_SQLite(C1TrueDBGrid c1Grid, DataTable dtData, DataTable dtSchemaTable, bool bSort = true, bool bForceResize = false)
        {
            string sTemp;
            var sHeader = "";
            var sDataType = "";
            string[] sArraySeparators = { "`" };
            var dtSortedData = new DataTable();
            DataRow[] dtRow;
            var sFormatDataType = "";

            if (chkShowColumnType.Checked)
            {
                dtSchemaTable = ArrangeSchemaTable(dtSchemaTable); //針對重覆的欄位名稱，後面加上流水號 (這樣才會跟 dtData 相符)
            }

            MyGlobal.dtSchemaTable = dtSchemaTable;
            _dtSchemaTableExport = dtSchemaTable;

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sTemp = dtData.Columns[i].DataType.ToString().Replace("System.", "");
                sFormatDataType = sTemp;

                if (chkShowColumnType.Checked)
                {
                    if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                    {
                        dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[i].ColumnName.Replace("'", "''") + "'");

                        if (dtRow.Length > 0)
                        {
                            sFormatDataType = MyGlobal.GetDataTypeFormat_SQLite(dtRow, out sDataType); //ArrangeDataTable, Part 1：取得 ColumnType, 顯示在 header
                        }
                    }

                    sHeader += dtData.Columns[i].ColumnName + "\r\n" + sFormatDataType + "`";
                }
                else
                {
                    sHeader += dtData.Columns[i].ColumnName + "`";
                }
            }

            var sArrayHeader = sHeader.Split(sArraySeparators, StringSplitOptions.RemoveEmptyEntries);

            if (bSort)
            {
                Array.Sort(sArrayHeader, string.CompareOrdinal); //ASCII排序
            }

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sFormatDataType = "string";
                sTemp = sArrayHeader[i].Replace("'", "''");
                sTemp = sTemp.IndexOf("\r\n", StringComparison.Ordinal) == -1 ? sTemp : sTemp.Substring(0, sTemp.IndexOf("\r\n", StringComparison.Ordinal));
                dtRow = dtSchemaTable.Select("ColumnName = '" + sTemp.Replace("'", "''") + "'");

                if (dtRow.Length > 0)
                {
                    sFormatDataType = MyGlobal.GetDataTypeFormat_SQLite(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                }

                switch (sFormatDataType.ToLower())
                {
                    //這裡要全部使用 string，否則一遇到 null 就會出錯，因為 null 可能會被填入 <NULL> 之類的字串
                    default:
                        dtSortedData.Columns.Add(sArrayHeader[i], typeof(string));
                        break;
                }
            }

            try
            {
                for (var i = 0; i < dtData.Rows.Count; i++)
                {
                    var rowData = dtSortedData.NewRow();

                    for (var j = 0; j < dtData.Columns.Count; j++)
                    {
                        Application.DoEvents();

                        if (Convert.IsDBNull(dtData.Rows[i][dtData.Columns[j].ColumnName]))
                        {
                            sTemp = MyLibrary.sGridNullShowAs.ToUpper() == "NONE" ? "" : MyLibrary.sGridNullShowAs;
                        }
                        else
                        {
                            sTemp = dtData.Rows[i][dtData.Columns[j].ColumnName].ToString();
                        }

                        var sDataTypeNew = dtData.Columns[j].DataType.ToString().Replace("System.", "").ToUpper();

                        if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                        {
                            dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[j].ColumnName.Replace("'", "''") + "'");

                            if (dtRow.Length > 0)
                            {
                                sDataTypeNew = dtRow[0]["DataType"].ToString().ToUpper();
                            }
                        }

                        switch (sDataTypeNew)
                        {
                            case "DATETIME":
                                if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                {
                                    sTemp = Convert.ToDateTime(sTemp).ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                                }

                                break;
                            case "TIMESPAN":
                                if (!string.IsNullOrEmpty(sTemp.Trim()) && "`<null>`{null}`(null)`".ToUpper().Contains("`" + sTemp.ToUpper() + "`") == false)
                                {
                                    sTemp = Convert.ToDateTime(sTemp).ToString("HH:mm:ss");
                                }

                                break;
                            case "BIT":
                                sTemp = sTemp.ToUpper() == "FALSE" ? "0" : "1";
                                break;
                        }

                        if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                        {
                            dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[j].ColumnName.Replace("'", "''") + "'");

                            if (dtRow.Length > 0)
                            {
                                sFormatDataType = MyGlobal.GetDataTypeFormat_SQLite(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                            }

                            rowData[dtData.Columns[j].ColumnName + (chkShowColumnType.Checked == false ? "" : "\r\n" + sFormatDataType)] = "`CLOB`NCLOB`".Contains("`" + sFormatDataType.ToUpper() + "`") && mnuPreviewCLOBData.Checked ? "(CLOB)" : sTemp;
                        }
                        else
                        {
                            rowData[dtData.Columns[j].ColumnName + (chkShowColumnType.Checked == false ? "" : "\r\n" + dtData.Columns[j].DataType.ToString().Replace("System.", "").ToLower())] = sTemp;
                        }
                    }

                    dtSortedData.Rows.Add(rowData);

                    if (btnCancelQuery.Tag.ToString() == "Cancel")
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (btnCancelQuery.Tag.ToString() == "Cancel")
            {
                return;
            }

            MyGlobal.dtSchemaTable = dtSortedData;

            int iTotalRows;

            if (_bNextPageQuery == false || btnAppendingQueriesOff.Visible)
            {
                iTotalRows = dtSortedData.Rows.Count;
                c1Grid.DataSource = dtSortedData;
            }
            else
            {
                if (dtData.Rows.Count > 0)
                {
                    _dtNextPage.Merge(dtSortedData, true);
                }

                iTotalRows = _dtNextPage.Rows.Count;

                dtSortedData.Dispose();
                c1Grid.DataSource = _dtNextPage;
            }

            c1Grid.Show();

            //匯出至 Excel，要依 col.Tag 判斷它的型態
            foreach (C1DataColumn col1 in c1Grid.Columns)
            {
                if (col1.Caption.IndexOf("\r\n", 0, StringComparison.Ordinal) != -1)
                {
                    sHeader = col1.Caption.Replace("\r\n", "\n").Split('\n')[0];
                }
                else if (col1.Caption.IndexOf("\r", 0, StringComparison.Ordinal) != -1)
                {
                    sHeader = col1.Caption.Split('\r')[0];
                }
                else
                {
                    sHeader = col1.Caption;
                }

                sDataType = "string";
                dtRow = dtSchemaTable.Select("ColumnName = '" + sHeader.Replace("'", "''") + "'");

                if (dtRow.Length > 0)
                {
                    MyGlobal.GetDataTypeFormat_SQLite(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                }

                switch (sDataType.ToLower())
                {
                    case "datetime":
                        col1.Tag = "datetime";
                        break;
                    case "int": //20220715 int 改歸在 number，避免誤判為 int 造成匯出 Excel 時「型別」錯誤
                    case "number":
                        col1.Tag = "number";
                        c1Grid.Splits[0].DisplayColumns[col1.Caption].Style.HorizontalAlignment = AlignHorzEnum.Far; //20220823 數值欄位，靠右顯示
                        break;
                    default:
                        col1.Tag = "string";
                        break;
                }
            }

            Application.DoEvents();

            //Auto Size
            if (iTotalRows <= 0 || !chkSize.Checked && !bForceResize)
            {
                return;
            }

            foreach (C1DisplayColumn col in c1Grid.Splits[_SplitsNum].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 2000;
                }

                if ("`500`1000`1500`2000`".Contains("`" + MyGlobal.sGridMaxWidth + "`") && col.Width > Convert.ToInt16(MyGlobal.sGridMaxWidth))
                {
                    col.Width = Convert.ToInt16(MyGlobal.sGridMaxWidth);
                }
            }

            c1Grid.Refresh();
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            var sValue = "";

            if (btnCancelQuery.Enabled)
            {
                //如果「正在執行查詢指令」，忽略 Commit / Rollback 按鈕
                return;
            }

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    sValue = MyGlobal.oOracleReader.oCommit();
                    sValue += (string.IsNullOrEmpty(sValue) ? "Commit executed at " : " ") + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    MyGlobal.oOracleReader.oDisconnect();
                    break;
                case "PostgreSQL":
                    sValue = MyGlobal.oPostgreReader.oCommit();
                    sValue += (string.IsNullOrEmpty(sValue) ? "Commit executed at " : " ") + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    MyGlobal.oPostgreReader.oDisconnect();
                    break;
                case "SQL Server":
                    sValue = MyGlobal.oSQLServerReader.oCommit();
                    sValue += (string.IsNullOrEmpty(sValue) ? "Commit executed at " : " ") + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    MyGlobal.oSQLServerReader.oDisconnect();
                    break;
                case "MySQL":
                    sValue = MyGlobal.oMySQLReader.oCommit();
                    sValue += (string.IsNullOrEmpty(sValue) ? "Commit executed at " : " ") + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    MyGlobal.oMySQLReader.oDisconnect();
                    break;
                case "SQLite":
                    sValue = MyGlobal.oSQLiteReader.oCommit();
                    sValue += (string.IsNullOrEmpty(sValue) ? "Commit executed at " : " ") + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    MyGlobal.oSQLiteReader.oDisconnect();
                    break;
                case "SQLCipher":
                    sValue = MyGlobal.oSQLCipherReader.oCommit();
                    sValue += (string.IsNullOrEmpty(sValue) ? "Commit executed at " : " ") + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    MyGlobal.oSQLCipherReader.oDisconnect();
                    break;
            }

            c1DockingTab1.SelectedTab = tabMessage;

            UpdateMessage(sValue); //btnCommit_Click
            editorSQLHistory.ReadOnly = false;
            editorSQLHistory.Text = "--" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + @"--Commit Command (Click Commit Button):" + "\r\n" + editorMessage.Text + "\r\n\r\n" + editorSQLHistory.Text;
            editorSQLHistory.ReadOnly = true;
            UpdateSqlHistory(0, "Complete", sValue, ""); //btnCommit_Click

            //傳遞資訊至 MainForm，更新所有 QueryForm 的 Commit/Rollbak 按鈕狀態
            TransferValueToMainForm("executecommitrollback`"); //btnCommit_Click
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            var sValue = "";

            if (btnCancelQuery.Enabled)
            {
                //如果「正在執行查詢指令」，忽略 Commit / Rollback 按鈕
                return;
            }

            switch (MyGlobal.sDataSource)
            {
                case "Oracle" when clsOracleReader.GetState() == ConnectionState.Closed:
                    return;
                case "Oracle":
                    sValue = MyGlobal.oOracleReader.oRollback();
                    sValue += (string.IsNullOrEmpty(sValue) ? "Rollback executed at " : " ") + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    MyGlobal.oOracleReader.oDisconnect();
                    break;
                case "PostgreSQL" when clsPostgreSQLReader.GetState() == ConnectionState.Closed:
                    return;
                case "PostgreSQL":
                    sValue = MyGlobal.oPostgreReader.oRollback();
                    sValue += (string.IsNullOrEmpty(sValue) ? "Rollback executed at " : " ") + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    MyGlobal.oPostgreReader.oDisconnect();
                    break;
                case "SQL Server" when clsSQLServerReader.GetState() == ConnectionState.Closed:
                    return;
                case "SQL Server":
                    sValue = MyGlobal.oSQLServerReader.oRollback();
                    sValue += (string.IsNullOrEmpty(sValue) ? "Rollback executed at " : " ") + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    MyGlobal.oSQLServerReader.oDisconnect();
                    break;
                case "MySQL" when clsMySQLReader.GetState() == ConnectionState.Closed:
                    return;
                case "MySQL":
                    sValue = MyGlobal.oMySQLReader.oRollback();
                    sValue += (string.IsNullOrEmpty(sValue) ? "Rollback executed at " : " ") + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    MyGlobal.oMySQLReader.oDisconnect();
                    break;
                case "SQLite" when clsSQLiteReader.GetState() == ConnectionState.Closed:
                    return;
                case "SQLite":
                    sValue = MyGlobal.oSQLiteReader.oRollback();
                    sValue += (string.IsNullOrEmpty(sValue) ? "Rollback executed at " : " ") + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    MyGlobal.oSQLiteReader.oDisconnect();
                    break;
                case "SQLCipher" when clsSQLCipherReader.GetState() == ConnectionState.Closed:
                    return;
                case "SQLCipher":
                    sValue = MyGlobal.oSQLCipherReader.oRollback();
                    sValue += (string.IsNullOrEmpty(sValue) ? "Rollback executed at " : " ") + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    MyGlobal.oSQLCipherReader.oDisconnect();
                    break;
            }

            c1DockingTab1.SelectedTab = tabMessage;

            UpdateMessage(sValue); //btnRollback_Click
            editorSQLHistory.ReadOnly = false;
            editorSQLHistory.Text = "--" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + @"--Rollback Command (Click Rollback Button):" + "\r\n" + editorMessage.Text + "\r\n\r\n" + editorSQLHistory.Text;
            editorSQLHistory.ReadOnly = true;
            UpdateSqlHistory(0, "Complete", sValue, ""); //btnRollback_Click

            //傳遞資訊至 MainForm，更新所有 QueryForm 的 Commit/Rollbak 按鈕狀態
            TransferValueToMainForm("executecommitrollback`"); //btnRollback_Click
        }

        private void TransferValueToMainForm(string sValue)
        {
            var valueArgs = new ValueUpdatedEventArgs(sValue);
            ValueUpdated(this, valueArgs);
        }

        private void CheckEditorContent()
        {
            var bValue = false;
            var bCanUndo = false;

            if (_bBusy)
            {
                return; //查詢尚未結束，不需要處理
            }

            if (!string.IsNullOrEmpty(editor.Text.Trim()))
            {
                bValue = true;
            }

            btnIndent.Enabled = bValue;
            btnUnIndent.Enabled = bValue;
            btnComment.Enabled = bValue;
            btnRemoveComment.Enabled = bValue;
            //btnClearHighlights.Enabled = bValue;
            btnQuery.Enabled = bValue;
            btnCode2SQL.Enabled = bValue;
            btnSQL2Code.Enabled = bValue;
            btnSelectCurrentBlock.Enabled = bValue;
            btnExecuteCurrentBlock.Enabled = bValue;

            if (editor.CanUndo)
            {
                bCanUndo = true;
            }

            btnSave.Visible = !bCanUndo;
            btnSaveRed.Visible = bCanUndo;

            var sTemp = (btnSave.Tag ?? Tag.ToString()).ToString();

            if (bCanUndo)
            {
                sTemp = "*" + sTemp;
            }

            TransferValueToMainForm("updatecanundo`" + AccessibleDescription + "`" + sTemp);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            TransferValueToMainForm("createnewtab`");
        }

        private static void FindAndMoveMsgBox(int x, int y, bool repaint, string title)
        {
            var thr = new Thread(() =>
            {
                IntPtr msgBox;
                
                while ((msgBox = FindWindow(IntPtr.Zero, title)) == IntPtr.Zero)
                { }

                GetWindowRect(msgBox, out var r);
                MoveWindow(msgBox /* handle of the message box */, x, y,
                   r.Width - r.X /* width of originally message box */,
                   r.Height - r.Y /* height of originally message box */,
                   repaint /* if true, the message box repaints */);
            });

            thr.Start();
        }

        private void UpdateHighlight(string sStyle, int num)
        {
            switch (sStyle)
            {
                case "Box":
                    editor.Indicators[num].Style = ScintillaNET.IndicatorStyle.Box;
                    break;
                case "CompositionThick":
                    editor.Indicators[num].Style = ScintillaNET.IndicatorStyle.CompositionThick;
                    break;
                case "Dash":
                    editor.Indicators[num].Style = ScintillaNET.IndicatorStyle.Dash;
                    break;
                case "Diagonal":
                    editor.Indicators[num].Style = ScintillaNET.IndicatorStyle.Diagonal;
                    break;
                //case "Squiggle": //波浪底線：for SQL 錯誤時使用！
                //    editorHighlight.Indicators[NUM].Style = ScintillaNET.IndicatorStyle.Squiggle;
                //    break;
                case "StraightBox":
                    editor.Indicators[num].Style = ScintillaNET.IndicatorStyle.StraightBox;
                    break;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFile(MyLibrary.bOpenFileOnCurrentTab);
        }

        private void OpenFile(bool bOpenFileOnCurrentTab, string sFilename = "")
        {
            //判斷是要在新頁籤開啟，還是在原頁籤開啟？
            if (bOpenFileOnCurrentTab == false)
            {
                //在新頁籤開啟！
                if (string.IsNullOrEmpty(sFilename))
                {
                    TransferValueToMainForm("createnewtab`OPENFILE");
                }
                else
                {
                    TransferValueToMainForm("createnewtab`OPENFILE`" + sFilename);
                }
            }
            else
            {
                //在原頁籤開啟
                //editor.CanUndo == true：內容被使用者異動了！
                //btnSaveRed.Visible == true：檔案被外面程式更新了，偵測點詢問時，使用者回答「不要 reload」
                if (editor.CanUndo || btnSaveRed.Visible)
                {
                    var sTemp = (btnSave.Tag ?? Tag.ToString()).ToString(); //, sEncode = "", sEndOfLineStyle = ""; //sTempFilename 暫存檔案
                    var sTemp1 = MyGlobal.GetLanguageString("Save", "form", Name, "msg", "Save", "Text");
                    var sTemp2 = MyGlobal.GetLanguageString("Save file?", "form", Name, "msg", "SaveFile", "Text");

                    var result = MessageBox.Show(sTemp2 + "\r\n\r\n\"" + sTemp + "\"", sTemp1, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (result)
                    {
                        case DialogResult.Yes:
                            Save(); //確認是否存檔，使用者選擇「是」，要存檔！
                            break;
                        case DialogResult.No:
                            //使用者不要儲存，直接開啟指定檔案
                            break;
                        default:
                            return;
                    }
                }

                if (string.IsNullOrEmpty(sFilename))
                {
                    var of = new OpenFileDialog
                    {
                        Multiselect = false, Filter = @"Query files (*.sql)|*.sql|All files (*.*)|*.*"
                    };

                    if (of.ShowDialog() == DialogResult.OK)
                    {
                        sFilename = of.FileName; //這裡會取得完整的路徑+檔名
                    }
                    else
                    {
                        return;
                    }
                }

                //檢查檔案是否存在，不存在，則詢問是否要建立 (從母表單傳遞過來的 Recent 檔名，有可能不存在/被刪除)
                if (File.Exists(sFilename) == false)
                {
                    var sTemp3 = MyGlobal.GetLanguageString("Create new file", "form", Name, "msg", "CreateNewFile", "Text");
                    var sTemp4 = MyGlobal.GetLanguageString("Create it?", "form", Name, "msg", "CreateIt", "Text");
                    var sTemp5 = MyGlobal.GetLanguageString("\"{filename}\" doesn't exist.", "form", Name, "msg", "FileDoesntExist", "Text").Replace("{filename}", sFilename);

                    if (MessageBox.Show(sTemp5 + "\r\n\r\n" + sTemp4, sTemp3, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        editor.Text = "";
                        WriteFile(sFilename); //以指定路徑+檔名存檔，內容是空白的
                    }
                    else
                    {
                        //20191030, 透過 MainForm 關閉空白的 Tab
                        TransferValueToMainForm("closeemptytab`");
                    }
                }
                else
                {
                    MyGlobal.sCheckExistTabResult = "";

                    //傳資訊到母表單，檢查 Tab 資訊，此檔案是否已被開啟了
                    TransferValueToMainForm("checkexisttab`" + sFilename);

                    while (!string.IsNullOrEmpty(MyGlobal.sCheckExistTabResult))
                    {
                        Application.DoEvents();
                        break;
                    }

                    if (MyGlobal.sCheckExistTabResult == "TRUE")
                    {
                        MyGlobal.sCheckExistTabResult = "";
                        return;
                    }

                    if (LoadFile(sFilename, out var bLargeFile) == false) //OpenFile
                    {
                        return;
                    }

                    //傳資訊到母表單，更新 Tab 資訊
                    TransferValueToMainForm("updatetabinfo`" + sFilename);

                    //傳資訊到母表單，更新 Recent Files 資訊
                    TransferValueToMainForm("updaterecentfiles`" + sFilename);

                    btnSave.Tag = sFilename;
                    btnSaveRed.Tag = File.GetLastWriteTime(sFilename).ToString("yyyy/MM/dd HH:mm:ss");
                    btnSave.Enabled = true;
                    btnSaveAs.Enabled = true;

                    editor.EmptyUndoBuffer();

                    if (bLargeFile)
                    {
                        editor.AppendText("\r\n"); //20190909 加一個換行符號，CanUnDo 就會是 true，(TabControl)檔名前面就會自動加上一個星號 (檔案未存檔)
                    }

                    CheckEditorContent(); //OpenFile() 事件
                }
            }
        }

        private bool LoadFile(string sFilename, out bool bLargeFile)
        {
            int iFileLimit;
            int iFileLimitAlarm;
            var lFileLength = new FileInfo(sFilename).Length;
            var sEndOfLineStyle = "";
            string sTemp;
            bLargeFile = false;
            
            if (Environment.Is64BitProcess)
            {
                iFileLimitAlarm = 300;
                iFileLimit = 303000000;
            }
            else
            {
                iFileLimitAlarm = 50;
                iFileLimit = 53000000; //32bit
            }

            if (lFileLength > iFileLimit)
            {
                sTemp = MyGlobal.GetLanguageString("JasonQuery may not open this file because it is larger than {qty} MB.", "form", Name, "msg", "OpenLargeFile1", "Text") + "\r\n\r\n" + sFilename + "\r\n\r\n";
                sTemp = sTemp.Replace("{qty}", iFileLimitAlarm.ToString());
                _sLangText = sTemp + MyGlobal.GetLanguageString("Do you still want to open this file?", "form", Name, "msg", "OpenLargeFile2", "Text") + "\r\n" + MyGlobal.GetLanguageString("(You may get the exception error of 'System.OutOfMemoryException'.)", "form", Name, "msg", "OpenLargeFile3", "Text");
                sTemp = MyGlobal.GetLanguageString("Open File", "form", Name, "msg", "OpenFile", "Text");

                if (MessageBox.Show(_sLangText, sTemp, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return false;
                }
            }

            var sEncode = MyTextEncode.GetTextEncode(sFilename, ref sEndOfLineStyle);

            lblEncode.Text = sEncode;
            lblEndOfLineStyle.Text = sEndOfLineStyle;

            if (sEncode == "ERROR") //指定的檔案開啟失敗
            {
                return false;
            }

            if (sEncode.StartsWith("Binary"))
            {
                sTemp = MyGlobal.GetLanguageString("It seems to be a binary file.", "form", Name, "msg", "OpenFileFailed1", "Text") + "\r\n\r\n";

                if (sEncode.Length > 6) //可辨識的 binnary file
                {
                    sTemp = MyGlobal.GetLanguageString("It seems to be a " + sEncode.Substring(7, sEncode.Length - 7) + " file.", "form", Name, "msg", "OpenFileFailed2", "Text") + "\r\n\r\n";
                    sTemp = sTemp.Replace("{0}", sEncode.Substring(7, sEncode.Length - 7));
                }

                _sLangText = MyGlobal.GetLanguageString("The following file could not be opened because it contains characters that could not be interpreted.", "form", Name, "msg", "OpenFileFailed3", "Text") + "\r\n\r\n" + sTemp + sFilename;
                sTemp = MyGlobal.GetLanguageString("Open File", "form", Name, "msg", "OpenFile", "Text");
                MessageBox.Show(_sLangText, sTemp, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                if (!File.Exists(sFilename))
                {
                    var sTemp1 = MyGlobal.GetLanguageString("Open File", "form", Name, "msg", "OpenFile", "Text");
                    var sTemp2 = MyGlobal.GetLanguageString("This file doesn't exist anymore.", "form", Name, "msg", "ThisFileDoesntExist", "Text");

                    MessageBox.Show(sTemp2 + "\r\n" + sFilename, sTemp1, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    Application.UseWaitCursor = true;

                    //以下是新寫法，檔案被鎖定時，讀取不會出錯！
                    using (var stream = new FileStream(sFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        editor.Text = "";

                        string sContents;

                        if (sEncode == "GB2312 (Simplified)")
                        {
                            using (var reader = new StreamReader(stream, Encoding.GetEncoding("GB2312"), true))
                            {
                                Application.DoEvents();
                                sContents = reader.ReadToEnd();

                                switch (sEndOfLineStyle)
                                {
                                    case "Unix (LF)":
                                        sContents = sContents.Replace("\n", "\r\n").Replace("\r\r", "\r");
                                        break;
                                    case "MacOs (CR)":
                                        sContents = sContents.Replace("\r", "\r\n").Replace("\n\n", "\n");
                                        break;
                                }

                                editor.Text = sContents.Replace("\t", "    ");
                            }
                        }
                        else
                        {
                            using (var reader = new StreamReader(stream, sEncode == "UTF-8" ? Encoding.UTF8 : Encoding.Default))
                            {
                                Application.DoEvents();

                                sContents = reader.ReadToEnd();

                                switch (sEndOfLineStyle)
                                {
                                    case "Unix (LF)":
                                        sContents = sContents.Replace("\n", "\r\n").Replace("\r\r", "\r");
                                        break;
                                    case "MacOs (CR)":
                                        sContents = sContents.Replace("\r", "\r\n").Replace("\n\n", "\n");
                                        break;
                                }

                                editor.Text = sContents.Replace("\t", "    ");
                            }
                        }
                    }

                    editor.SelectionStart = 0;
                    editor.SelectionEnd = 0;
                    editor.ScrollCaret();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                lblEncode.Text = sEncode;
                lblEndOfLineStyle.Text = sEndOfLineStyle;
                lblExecTime.Text = _sExecTime + @" 00:00.000";
                lblQueryTime.Text = _sQueryTime + @" 00:00.000";
                lblRows.Text = @"0 " + _sRows;

                Application.UseWaitCursor = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save(); //btnSave_Click
        }

        private bool SaveAs()
        {
            var sf = new SaveFileDialog();
            _sLangText = MyGlobal.GetLanguageString("Save As", "Global", "Global", "msg", "SaveAs", "Text");
            sf.Title = _sLangText;
            sf.FileName = Path.GetFileName((btnSave.Tag ?? Tag.ToString()).ToString());
            sf.Filter = @"Query files (*.sql)|*.sql|All files (*.*)|*.*";
            sf.RestoreDirectory = true;

            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;
            var iXTemp = Cursor.Position.X;
            var iYTemp = Cursor.Position.Y;
            var iX = 0;
            var iY = 0;

            if (iWidth - iXTemp < 570)
            {
                iX = 570 + (iWidth - iXTemp < 150 ? iWidth - iXTemp : 0); //游標很靠近螢幕的右側
            }

            if (iHeight - iYTemp < 420)
            {
                iY = -420 - (iHeight - iYTemp < 170 ? iHeight - iYTemp : 0); //游標很靠近螢幕的下方
            }

            MoveDialogWhenOpened(sf.Title, iXTemp - iX, iYTemp + iY); //SaveAs

            if (sf.ShowDialog() != DialogResult.OK)
            {
                return false; //無論檔案是否存在，只要不是按「取消」或「否」，都會回傳 OK
            }

            try
            {
                MyGlobal.sCheckExistTabResult = "";

                if (string.IsNullOrEmpty(Path.GetExtension(sf.FileName)))
                {
                    sf.FileName += ".sql";
                }

                //傳資訊到母表單，檢查 Tab 資訊，此檔案是否已被 JasonQuery 開啟了
                TransferValueToMainForm("checkexisttab`" + sf.FileName);

                while (!string.IsNullOrEmpty(MyGlobal.sCheckExistTabResult))
                {
                    Application.DoEvents();
                    break;
                }

                if (MyGlobal.sCheckExistTabResult == "TRUE")
                {
                    MyGlobal.sCheckExistTabResult = "";

                    MessageBox.Show("SaveAs()\r\n\r\n" + sf.FileName, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                WriteFile(sf.FileName);

                TransferValueToMainForm("updatetabinfo`" + sf.FileName);

                btnSave.Tag = sf.FileName;
                btnSaveRed.Tag = File.GetLastWriteTime(sf.FileName).ToString("yyyy/MM/dd HH:mm:ss");

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SaveAs()\r\n\r\n" + ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void WriteFile(string sFilename)
        {
            try
            {
                File.WriteAllText(sFilename, editor.Text, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                var sTemp = MyGlobal.GetLanguageString("Please check if this file is opened in another program.", "form", Name, "msg", "SaveFailed1", "Text");
                MessageBox.Show(sTemp + "\r\n\r\n" + sFilename + "\r\n\r\n" + ex.Message, "JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                //傳資訊到母表單，更新 Tab 資訊
                TransferValueToMainForm("updatetabinfo`" + sFilename);

                btnSave.Tag = sFilename;
                btnSaveRed.Tag = File.GetLastWriteTime(sFilename).ToString("yyyy/MM/dd HH:mm:ss");

                editor.EmptyUndoBuffer();
                CheckEditorContent(); //WriteFile() 事件

                //傳資訊到母表單，更新 Recent Files 資訊
                TransferValueToMainForm("updaterecentfiles`" + sFilename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e) //按下 SaveAs 按鈕
        {
            if (SaveAs()) //btnSaveAs_Click
            {

            }
        }

        private void btnSaveRed_Click(object sender, EventArgs e) //按下工具列上的紅色 Save 按鈕
        {
            if (!Save())
            {
                return;
            }

            btnSave.Visible = true;
            btnSaveRed.Visible = false;
        }

        private bool Save()
        {
            var sFullFilename = (btnSave.Tag ?? string.Empty).ToString();

            if (string.IsNullOrEmpty(sFullFilename)) //尚未存檔過！
            {
                if (SaveAs() == false) //從 Save() 呼叫的
                {
                    return false;
                }
            }
            else
            {
                WriteFile(sFullFilename);
            }

            return true;
        }

        private void editor_Insert(object sender, ScintillaNET.ModificationEventArgs e)
        {
            CheckEditorContent(); //editor_Insert 事件
        }

        private void editor_KeyDown(object sender, KeyEventArgs e)
        {
            //會先經過 ProcessCmdKey()，且 ProcessCmdKey() 回傳 false，才會再進到 editor_KeyDown()

            switch (e.Control)
            {
                default:
                    {
                        //if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                        //{
                        //    e.Handled = false;
                        //    //MultipleSelections(); //上、下、左、右鍵：有bug, 停用!
                        //}

                        if (c1GridAC4Period1.Visible)
                        {
                            switch (e.KeyCode)
                            {
                                case Keys.Escape:
                                case Keys.Space:
                                case Keys.Tab:
                                    c1GridAC4Period1.Visible = false;
                                    break;

                                case Keys.Back:
                                {
                                    if (editor.CurrentPosition <= _iPeriodPos)
                                    {
                                        c1GridAC4Period1.Visible = false;
                                    }

                                    break;
                                }
                                case Keys.Down:
                                    _iPeriodPos2 = editor.CurrentPosition;
                                    e.SuppressKeyPress = true;
                                    c1GridAC4Period1.Focus();
                                    break;

                                case Keys.Enter:
                                    e.SuppressKeyPress = true;
                                    c1GridAC4Period1.Visible = false;
                                    break;

                                case Keys.Left:
                                case Keys.Right:
                                {
                                    var iTemp = 0;

                                    for (var i = _iPeriodPos; i < editor.Text.Length; i++)
                                    {
                                        if (!MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_"))
                                        {
                                            if (iTemp == 0)
                                            {
                                                iTemp = editor.CurrentPosition;
                                            }

                                            break;
                                        }

                                        iTemp = i + 1;
                                    }

                                    if (editor.CurrentPosition + (e.KeyCode == Keys.Left ? -1 : 1) > iTemp)
                                    {
                                        c1GridAC4Period1.Visible = false;
                                    }

                                    break;
                                }
                            }
                        }
                        else if (c1GridAC4Space1.Visible)
                        {
                            switch (e.KeyCode)
                            {
                                case Keys.Escape:
                                case Keys.Space:
                                case Keys.Tab:
                                    c1GridAC4Space1.Visible = false;
                                    break;

                                case Keys.Back:
                                {
                                    if (editor.CurrentPosition <= _iSpacePos)
                                    {
                                        c1GridAC4Space1.Visible = false;
                                    }

                                    break;
                                }
                                case Keys.Down:
                                    _iSpacePos2 = editor.CurrentPosition;
                                    e.SuppressKeyPress = true;
                                    c1GridAC4Space1.Focus();
                                    break;

                                case Keys.Enter:
                                    e.SuppressKeyPress = true;
                                    c1GridAC4Space1.Visible = false;
                                    break;

                                case Keys.Left:
                                case Keys.Right:
                                {
                                    var iTemp = 0;

                                    for (var i = _iSpacePos; i < editor.Text.Length; i++)
                                    {
                                        if (!MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_"))
                                        {
                                            if (iTemp == 0)
                                            {
                                                iTemp = editor.CurrentPosition;
                                            }

                                            break;
                                        }

                                        iTemp = i + 1;
                                    }

                                    if (editor.CurrentPosition + (e.KeyCode == Keys.Left ? -1 : 1) > iTemp)
                                    {
                                        c1GridAC4Space1.Visible = false;
                                    }

                                    break;
                                }
                            }
                        }
                        else if (c1GridAC4All.Visible)
                        {
                            switch (e.KeyCode)
                            {
                                case Keys.Escape:
                                case Keys.Space:
                                case Keys.Tab:
                                    c1GridAC4All.Visible = false;
                                    break;

                                case Keys.Back:
                                {
                                    if (editor.CurrentPosition <= _iAllPos)
                                    {
                                        c1GridAC4All.Visible = false;
                                    }

                                    break;
                                }
                                case Keys.Up:
                                    c1GridAC4All.Visible = false;
                                    break;

                                case Keys.Down:
                                    _iAllPos2 = editor.CurrentPosition;
                                    e.SuppressKeyPress = true;
                                    c1GridAC4All.Focus();
                                    break;

                                case Keys.Enter:
                                    e.SuppressKeyPress = true;
                                    c1GridAC4All.Visible = false;
                                    break;

                                case Keys.Left:
                                case Keys.Right:
                                {
                                    var iStart = 0; //往前找起始字母

                                    for (var i = _iAllPos - 1; i >= 0; i--)
                                    {
                                        if (MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_"))
                                        {
                                            iStart = i;
                                            continue;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    var iTemp = 0;

                                    for (var i = _iAllPos; i < editor.Text.Length; i++)
                                    {
                                        if (!MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_"))
                                        {
                                            if (iTemp == 0)
                                            {
                                                iTemp = editor.CurrentPosition;
                                            }

                                            break;
                                        }

                                        iTemp = i + 1;
                                    }

                                    //左鍵
                                    if (editor.CurrentPosition + (e.KeyCode == Keys.Left ? -1 : 0) < iStart)
                                    {
                                        c1GridAC4All.Visible = false;
                                    }

                                    //右鍵
                                    if (editor.CurrentPosition + (e.KeyCode == Keys.Right ? 1 : 0) > iTemp)
                                    {
                                        c1GridAC4All.Visible = false;
                                    }

                                    break;
                                }
                            }
                        }

                        break;
                    }
            }
        }

        private void editor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < 32)
            {
                //Prevent control characters from getting inserted into the text buffer
                e.Handled = true;
            }
        }

        private static DataTable DetectSchemaAndPeriod(string sAliasName)
        {
            var dt = new DataTable();

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    //應該沒有需要處理
                    break;

                case "PostgreSQL":
                    {
                        DataRow row;
                        var dtRow = MyGlobal.dtTableAndViewName.Select("SchemaNode = '" + sAliasName + "'");
                        dt.Columns.Add("SchemaName");
                        dt.Columns.Add("SchemaType");

                        if (dtRow.Length > 0)
                        {
                            foreach (var t in dtRow)
                            {
                                row = dt.NewRow();
                                row["SchemaName"] = t["SchemaName"];
                                row["SchemaType"] = t["SchemaType"];
                                dt.Rows.Add(row);
                            }

                            var dv = dt.DefaultView;
                            dv.Sort = "SchemaName";
                            dt = dv.ToTable();
                        }

                        break;
                    }
                case "SQL Server":
                    {
                        if (sAliasName.IndexOf(".", StringComparison.Ordinal) == -1) //DB or Node
                        {
                            DataRow row;
                            var dtRow = MyGlobal.dtTableAndViewName.Select("DB = '" + sAliasName + "'");
                            dt.Columns.Add("SchemaName");
                            dt.Columns.Add("SchemaType");

                            if (dtRow.Length > 0) //DB
                            {
                                var sDistinct = ",";

                                foreach (var t in dtRow)
                                {
                                    if (sDistinct.IndexOf("," + t["SchemaNode"] + ",", StringComparison.Ordinal) != -1)
                                        continue;

                                    sDistinct += t["SchemaNode"] + ",";

                                    row = dt.NewRow();
                                    row["SchemaName"] = t["SchemaNode"];
                                    row["SchemaType"] = "[" + t["DB"] + "]";
                                    dt.Rows.Add(row);
                                }

                                var dv = dt.DefaultView;
                                dv.Sort = "SchemaName";
                                dt = dv.ToTable();
                            }
                            else //Node
                            {
                                dtRow = MyGlobal.dtTableAndViewName.Select("SchemaNode = '" + sAliasName + "'");

                                if (dtRow.Length > 0)
                                {
                                    foreach (var t in dtRow)
                                    {
                                        row = dt.NewRow();
                                        row["SchemaName"] = t["SchemaName"];
                                        row["SchemaType"] = t["SchemaType"]; //20220825 ", [" + dtRow[i]["DB"] + "]" 不顯示 DB (因為可能會有 N 個 DB，故只要顯示當下的 DB 即可)
                                        dt.Rows.Add(row);
                                    }

                                    var dv = dt.DefaultView;
                                    dv.Sort = "SchemaName";
                                    dt = dv.ToTable();
                                }
                            }
                        }
                        else //DB + Node
                        {
                            var dtRow = MyGlobal.dtTableAndViewName.Select("DBAndNode = '" + sAliasName + "'");
                            dt.Columns.Add("SchemaName");
                            dt.Columns.Add("SchemaType");

                            if (dtRow.Length > 0)
                            {
                                foreach (var t in dtRow)
                                {
                                    var row = dt.NewRow();
                                    row["SchemaName"] = t["SchemaName"];
                                    row["SchemaType"] = t["SchemaType"];
                                    dt.Rows.Add(row);
                                }

                                var dv = dt.DefaultView;
                                dv.Sort = "SchemaName";
                                dt = dv.ToTable();
                            }
                        }

                        break;
                    }
                case "MySQL":
                    {
                        var dtRow = MyGlobal.dtTableAndViewName.Select("SchemaNode = '" + sAliasName + "'");
                        dt.Columns.Add("SchemaName");
                        dt.Columns.Add("SchemaType");

                        if (dtRow.Length > 0)
                        {
                            foreach (var t in dtRow)
                            {
                                var row = dt.NewRow();
                                row["SchemaName"] = t["SchemaName"];
                                row["SchemaType"] = t["SchemaType"];
                                dt.Rows.Add(row);
                            }

                            var dv = dt.DefaultView;
                            dv.Sort = "SchemaName";
                            dt = dv.ToTable();
                        }

                        break;
                    }
                case "SQLite":

                    break;
                case "SQLCipher":

                    break;
            }

            return dt;
        }

        private void HandleAC4Space()
        {
            if (editor.CurrentPosition < 4 || MyGlobal.dtTableAndViewName == null)
            {
                return; //長度不夠構成一個 Select 子句或是 Use 指令，直接忽略
            }

            var sSqlBlock = SelectBlock(false); //該段落的完整 SQL
            sSqlBlock = " " + GetFormattedSql(sSqlBlock) + " "; //整理成一句 SQL (前後加上空白，省去判斷是否為「起始」或「結束」的狀況)

            if ((sSqlBlock.IndexOf(" USE ", StringComparison.Ordinal) == -1) && (sSqlBlock.IndexOf(" UPDATE ", StringComparison.Ordinal) == -1) && (sSqlBlock.IndexOf(" INSERT INTO ", StringComparison.Ordinal) == -1) && (sSqlBlock.IndexOf(" FROM ", StringComparison.Ordinal) == -1))
            {
                return; //該段落 SQL 沒有找到 USE、FROM、UPDATE、INTO 關鍵字 (Use/Update/Delete From/From/Insert Into)
            }

            sSqlBlock = sSqlBlock.Trim();
            _iSpacePos = editor.CurrentPosition;

            //後面接 table (Update/Delete From/Insert Into)
            var bTableOnly = sSqlBlock == "UPDATE" || sSqlBlock == "INSERT INTO" || sSqlBlock == "DELETE FROM";

            //後面接 table/view
            var bFrom = bTableOnly == false && sSqlBlock.Length > 4 && sSqlBlock.Substring(sSqlBlock.Length - 4, 4) == "FROM";

            //20221012 加判 bFrom 是否為「把 table/view name 去掉再重新輸入」(例如 select * from abc where a='xyz'，把 abc 消掉，再按下空白鍵)
            if (bFrom == false)
            {
                try
                {
                    for (var i = editor.CurrentPosition - 1; i > 0; i--)
                    {
                        if (MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), " "))
                        {
                            //if (sSqlBlock2.Substring(i, 1) == " ")
                            //{
                            //    //空白 OK！
                            //}
                        }
                        else
                        {
                            var sObjectName = editor.Text.Substring(i + 1, editor.CurrentPosition - i - 1).Trim().ToUpper();

                            if (sObjectName == "FROM")
                            {
                                bFrom = true;
                            }

                            break;
                        }
                    }
                }
                catch (Exception)
                { }
            }

            //20221012 table/view 可能是沒有別名的情況：判斷是是否為 where/and/or/by，如果是，後面帶出該 table/view 的所有欄位
            var bWhere = bTableOnly == false && sSqlBlock.Length > 5 && sSqlBlock.Substring(sSqlBlock.Length - 5, 5) == "WHERE";
            var bAnd = bTableOnly == false && sSqlBlock.Length > 4 && sSqlBlock.Substring(sSqlBlock.Length - 3, 3) == "AND";
            var bOr = bTableOnly == false && sSqlBlock.Length > 4 && sSqlBlock.Substring(sSqlBlock.Length - 2, 2) == "OR";
            var bBy = bTableOnly == false && sSqlBlock.Length > 4 && sSqlBlock.Substring(sSqlBlock.Length - 2, 2) == "BY";
            var sTableViewName = ""; //找出 table/view 的名稱

            //後面接 database
            var bUse = sSqlBlock == "USE";

            if (bFrom == false && bUse == false && bWhere == false && bAnd == false && bOr == false && bBy == false)
            {
                //再次確認是否為 Where || And || Or || By，有可能最後的關鍵字不是這幾個，因而誤判
                for (var i = editor.CurrentPosition - 1; i > 0; i--)
                {
                    if (MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), " "))
                    {
                        //
                    }
                    else
                    {
                        var sKeyWord = editor.Text.Substring(i, editor.CurrentPosition - i).Trim().ToUpper();

                        switch (sKeyWord)
                        {
                            case "WHERE":
                                bWhere = true;
                                break;
                            case "AND":
                                bAnd = true;
                                break;
                            case "OR":
                                bOr = true;
                                break;
                            case "BY":
                                bBy = true;
                                break;
                        }

                        break;
                    }
                }
            }

            if (bTableOnly == false && bFrom == false && bUse == false && bWhere == false && bAnd == false && bOr == false && bBy == false)
            {
                return; //按下空白鍵，往前找，沒有找到關鍵字，忽略！
            }

            var sCondition = "(SchemaType = '[Table]'{0})";
            sCondition = string.Format(sCondition, bTableOnly ? "" : " OR SchemaType = '[View]'");

            //20220722 切換 DB 後，更新 AC 內容
            if (MyGlobal.sDataSource == "SQL Server" || MyGlobal.sDataSource == "MySQL")
            {
                if (bUse)
                {
                    sCondition = "Memo = 'DB'";
                }
                else if (bTableOnly == false && bFrom == false && bWhere == false && bAnd == false && bOr == false && bBy == false)
                {
                    return;
                }
                else if ((bFrom || bTableOnly) && !string.IsNullOrEmpty(MyGlobal.sDBConnectionDatabase))
                {
                    sCondition += " AND DB = '" + MyGlobal.sDBConnectionDatabase.ToUpper() + "'";
                }
            }
            else
            {
                if (bUse)
                {
                    return; //其他 DB 應該不需要使用 USE 指令
                }

                if (bTableOnly == false && bFrom == false && bWhere == false && bAnd == false && bOr == false && bBy == false)
                {
                    return;
                }
            }

            if (bWhere || bAnd || bOr || bBy) //20221013
            {
                //此處要重取 SQL，且不可轉大寫，避免 SQL Server 定序問題而找不到該 table/view
                sSqlBlock = SelectBlock(false); //該段落的完整 SQL
                sSqlBlock = GetFormattedSql(sSqlBlock, false);
                var j = sSqlBlock.ToUpper().IndexOf(" FROM ", StringComparison.Ordinal) + " FROM ".Length;

                for (var i = j; i < sSqlBlock.Length; i++)
                {
                    if (MyGlobal.IsEngAlphabetOrNumber(sSqlBlock.Substring(i, 1), ".", "_")) //table/view name 可以包含底線，也有可能是 dbo.xxxx
                    {
                        //
                    }
                    else
                    {
                        sTableViewName = sSqlBlock.Substring(j, i - j);
                        break;
                    }
                }
            }

            _sPosXY4AC = Cursor.Position.X + ", " + Cursor.Position.Y;

            var bShow = false;
            var dt = new DataTable();
            dt.Columns.Add("SchemaName");
            dt.Columns.Add("SchemaType");
            
            if (bFrom == false && bUse == false && (bWhere || bAnd || bOr || bBy))
            {
                if (!string.IsNullOrEmpty(sTableViewName))
                {
                    var sSql = "SELECT * FROM " + sTableViewName + " WHERE 1 = 2";

                    switch (MyGlobal.sDataSource)
                    {
                        case "Oracle":
                            {
                                if (clsOracleReader.GetState() == ConnectionState.Closed)
                                {
                                    MyGlobal.oOracleReader.ConnectTo();
                                }

                                dt = MyGlobal.oOracleReader.ExecuteQueryToDataTable(sSql, false);

                                if (dt != null && dt.Rows.Count >= 0)
                                {
                                    bShow = true;
                                }

                                break;
                            }
                        case "PostgreSQL":
                            {
                                if (clsPostgreSQLReader.GetState() == ConnectionState.Closed)
                                {
                                    MyGlobal.oPostgreReader.ConnectTo(MyGlobal.sDBConnectionString);
                                }

                                if (MyGlobal.bSavePoint)
                                {
                                    MyGlobal.oPostgreReader.oSavePoint("jqcc1688ccqj");
                                }

                                dt = MyGlobal.oPostgreReader.ExecuteQueryToDataTable(sSql, false);

                                if (dt != null && dt.Rows.Count >= 0)
                                {
                                    bShow = true;

                                    if (MyGlobal.bSavePoint)
                                    {
                                        MyGlobal.oPostgreReader.oReleasePoint("jqcc1688ccqj");
                                    }
                                }
                                else
                                {
                                    if (MyGlobal.bSavePoint)
                                    {
                                        MyGlobal.oPostgreReader.oRollbackPoint("jqcc1688ccqj");
                                    }
                                }

                                break;
                            }
                        case "SQL Server":
                            {
                                if (clsSQLServerReader.GetState() == ConnectionState.Closed)
                                {
                                    MyGlobal.oSQLServerReader.ConnectTo(MyGlobal.sDBConnectionString);
                                }

                                dt = MyGlobal.oSQLServerReader.ExecuteQueryToDataTable(sSql, false);

                                if (dt != null && dt.Rows.Count >= 0)
                                {
                                    bShow = true;
                                }

                                break;
                            }
                        case "MySQL":
                            {
                                if (clsMySQLReader.GetState() == ConnectionState.Closed)
                                {
                                    MyGlobal.oMySQLReader.ConnectTo(MyGlobal.sDBConnectionString);
                                }

                                dt = MyGlobal.oMySQLReader.ExecuteQueryToDataTable(sSql, false);

                                if (dt != null && dt.Rows.Count >= 0)
                                {
                                    bShow = true;
                                }

                                break;
                            }
                        case "SQLite":

                            break;
                        case "SQLCipher":

                            break;
                    }

                    if (bShow)
                    {
                        var dt2 = new DataTable();
                        dt2.Columns.Add("SchemaName");
                        dt2.Columns.Add("SchemaType");

                        for (var i = 0; i < dt.Columns.Count; i++)
                        {
                            var row = dt2.NewRow();
                            row["SchemaName"] = dt.Columns[i].ColumnName.ToString();
                            row["SchemaType"] = dt.Columns[i].DataType.ToString().Replace("System.", "");
                            dt2.Rows.Add(row);
                        }

                        dt = dt2.Copy();
                    }
                }
            }
            else
            {
                bShow = true;
                var dtRow = MyGlobal.dtTableAndViewName.Select(sCondition);

                if (dtRow.Length > 0)
                {
                    if (bUse)
                    {
                        //整理出不重複的 DB 值
                        foreach (var t in dtRow)
                        {
                            var row = dt.NewRow();
                            row["SchemaName"] = t["DB"];
                            row["SchemaType"] = "[Database]";
                            dt.Rows.Add(row);
                        }
                    }
                    else
                    {
                        foreach (var t in dtRow)
                        {
                            var sDB = "";

                            switch (MyGlobal.sDataSource)
                            {
                                case "SQL Server":
                                    //20220825 不顯示 DB (因為可能會有 N 個 DB，故只要顯示當下的 DB 即可)
                                    //sDB = ", [" + dtRow[i]["DB"] + "]";
                                    break;
                                case "PostgreSQL":
                                    sDB = ", [" + t["SchemaNode"] + "]";
                                    break;
                            }

                            var row = dt.NewRow();
                            row["SchemaName"] = t["SchemaName"];
                            row["SchemaType"] = t["SchemaType"] + sDB;
                            dt.Rows.Add(row);
                        }
                    }

                    var dv = dt.DefaultView;
                    dv.Sort = "SchemaName";
                    dt = dv.ToTable();
                }
            }

            if (bShow == false || dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            var p = PointToScreen(new Point(editor.PointXFromPosition(editor.CurrentPosition), editor.PointYFromPosition(editor.CurrentPosition)));
            var iLeft = p.X - MyGlobal.iMainFormLeft + splitContainer2.SplitterDistance - 6;
            var iYShift = GetYShift();
            var iTop = p.Y - MyGlobal.iMainFormTop - iYShift;

            _dtAC4Space = new DataTable();
            _dtAC4Space.Columns.Add("C"); //ColumnName
            _dtAC4Space.Columns.Add("D"); //DataType

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var row = _dtAC4Space.NewRow();
                row["C"] = dt.Rows[i]["SchemaName"].ToString();
                row["D"] = dt.Rows[i]["SchemaType"].ToString();
                _dtAC4Space.Rows.Add(row);
            }

            c1GridAC4Space1.Splits[0].RecordSelectors = false;
            c1GridAC4Space1.DataSource = _dtAC4Space;
            c1GridAC4Space2.Splits[0].RecordSelectors = false;
            c1GridAC4Space2.DataSource = _dtAC4Space;
            c1GridAC4Space1.Location = new Point(iLeft, iTop);
            c1GridAC4Space1.Visible = true;
            c1GridAC4Space2.Location = new Point(-200, -200);
            c1GridAC4Space2.Visible = true;

            var iWidth = MyGlobal.ResizeGridColumnWidth(c1GridAC4Space1);
            ResizeACGrid(c1GridAC4Space1, c1GridAC4Space2, _dtAC4Space.Rows.Count, iWidth); //HandleAC4Space
        }

        /// <summary>
        /// 搜尋 With xxx AS 子查詢
        /// </summary>
        /// <param name="sSql">傳入要搜尋的 SQL</param>
        /// <param name="iStart">從哪一個位置開始往後搜尋</param>
        /// <returns></returns>
        private static string GetACSql4Subquery(string sSql, int iStart)
        {
            var iEnd = 0;
            var bDoubleQuotation = false; //是否為雙引號起始？
            var bSingleQuotation = false; //是否為單引號起始？
            var bSingleComment = false; //是否為單列註解？
            var bParagraphComment = false; //是否為段落註解？
            var bRoundBrackets1 = false; //是否為第 1 組 ) ？
            var bRoundBrackets2 = false; //是否為第 2 組 ) ？
            var bRoundBrackets3 = false; //是否為第 3 組 ) ？
            var bRoundBrackets4 = false; //是否為第 4 組 ) ？
            var bRoundBrackets5 = false; //是否為第 5 組 ) ？
            var bRoundBrackets6 = false; //是否為第 6 組 ) ？
            var bRoundBrackets7 = false; //是否為第 7 組 ) ？
            var bRoundBrackets8 = false; //是否為第 8 組 ) ？
            var bRoundBrackets9 = false; //是否為第 9 組 ) ？
            var array = (sSql + " ").ToCharArray();

            for (var i = iStart; i > 0; i--)
            {
                var letter = array[i];

                if (letter.ToString() == "\"" || letter.ToString() == "'")
                {
                    switch (letter.ToString())
                    {
                        case "\"" when bDoubleQuotation:
                            bDoubleQuotation = false;
                            break;
                        case "\"":
                            {
                                if (bSingleQuotation == false)
                                {
                                    bDoubleQuotation = true;
                                }

                                break;
                            }
                        case "'" when bSingleQuotation:
                            bSingleQuotation = false;
                            break;
                        case "'":
                            {
                                if (bDoubleQuotation == false)
                                {
                                    bSingleQuotation = true;
                                }

                                break;
                            }
                    }
                }
                else if (letter.ToString() == ")")
                {
                    if (bSingleQuotation || bDoubleQuotation)
                    {
                        //括號落在單引號或雙引號之間，忽略！
                    }
                    else if (bRoundBrackets8)
                    {
                        //已偵測到第 8 組括號
                        bRoundBrackets9 = true;
                    }
                    else if (bRoundBrackets7)
                    {
                        //已偵測到第 7 組括號
                        bRoundBrackets8 = true;
                    }
                    else if (bRoundBrackets6)
                    {
                        //已偵測到第 6 組括號
                        bRoundBrackets7 = true;
                    }
                    else if (bRoundBrackets5)
                    {
                        //已偵測到第 5 組括號
                        bRoundBrackets6 = true;
                    }
                    else if (bRoundBrackets4)
                    {
                        //已偵測到第 4 組括號
                        bRoundBrackets5 = true;
                    }
                    else if (bRoundBrackets3)
                    {
                        //已偵測到第 3 組括號
                        bRoundBrackets4 = true;
                    }
                    else if (bRoundBrackets2)
                    {
                        //已偵測到第 2 組括號
                        bRoundBrackets3 = true;
                    }
                    else if (bRoundBrackets1)
                    {
                        //已偵測到第 1 組括號
                        bRoundBrackets2 = true;
                    }
                    else
                    {
                        bRoundBrackets1 = true;
                    }
                }
                else if (letter.ToString() == "(")
                {
                    if (bSingleQuotation || bDoubleQuotation)
                    {
                        //括號落在單引號或雙引號之間，忽略！
                    }
                    else if (bRoundBrackets9)
                    {
                        bRoundBrackets9 = false;
                    }
                    else if (bRoundBrackets8)
                    {
                        bRoundBrackets8 = false;
                    }
                    else if (bRoundBrackets7)
                    {
                        bRoundBrackets7 = false;
                    }
                    else if (bRoundBrackets6)
                    {
                        bRoundBrackets6 = false;
                    }
                    else if (bRoundBrackets5)
                    {
                        bRoundBrackets5 = false;
                    }
                    else if (bRoundBrackets4)
                    {
                        bRoundBrackets4 = false;
                    }
                    else if (bRoundBrackets3)
                    {
                        bRoundBrackets3 = false;
                    }
                    else if (bRoundBrackets2)
                    {
                        bRoundBrackets2 = false;
                    }
                    else
                    {
                        bRoundBrackets1 = false;
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && i >= 1 && letter.ToString() == "*" && array[i - 1].ToString() == "/")
                {
                    if (bParagraphComment == false)
                    {
                        bParagraphComment = true;
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && letter.ToString() == "*" && i + 1 <= array.GetUpperBound(0) && array[i + 1].ToString() == "/")
                {
                    if (bParagraphComment)
                    {
                        bParagraphComment = false;
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && i >= 1 && letter.ToString() == "-" && array[i - 1].ToString() == "-")
                {
                    if (bSingleComment == false)
                    {
                        bSingleComment = true;
                    }
                }

                if (bSingleComment && (letter.ToString() == "\r" || letter.ToString() == "\n"))
                {
                    bSingleComment = false;
                }

                if (bParagraphComment && letter.ToString() == "*" && i + 1 < array.Length && array[i + 1].ToString() == "/")
                {
                    bParagraphComment = false;
                }

                if (letter.ToString() == "(" && bDoubleQuotation == false && bSingleQuotation == false && bSingleComment == false && bParagraphComment == false && bRoundBrackets1 == false && bRoundBrackets2 == false && bRoundBrackets3 == false && bRoundBrackets4 == false && bRoundBrackets5 == false && bRoundBrackets6 == false && bRoundBrackets7 == false && bRoundBrackets8 == false && bRoundBrackets9 == false)
                {
                    iEnd = i;
                    break;
                }
            }

            var sResult = "";

            if (iEnd > 0)
            {
                if (iStart - iEnd - 1 > 0)
                {
                    sResult = sSql.Substring(iEnd + 1, iStart - iEnd - 1).Trim();
                }
            }

            return sResult.Trim();
        }

        /// <summary>
        /// 搜尋 With xxx AS 子查詢
        /// 第一組才會帶 With，所以，搜尋時要以 xxx AS 為主
        /// </summary>
        /// <param name="sSql">傳入要搜尋的 SQL</param>
        /// <param name="iStart">從哪一個位置開始往後搜尋</param>
        /// <returns></returns>
        private static string GetACSql4WithAs(string sSql, int iStart)
        {
            var iEnd = 0;
            var bDoubleQuotation = false; //是否為雙引號起始？
            var bSingleQuotation = false; //是否為單引號起始？
            var bSingleComment = false; //是否為單列註解？
            var bParagraphComment = false; //是否為段落註解？
            var bRoundBrackets1 = false; //是否為第 1 組 ( ？
            var bRoundBrackets2 = false; //是否為第 2 組 ( ？
            var bRoundBrackets3 = false; //是否為第 3 組 ( ？
            var bRoundBrackets4 = false; //是否為第 4 組 ( ？
            var bRoundBrackets5 = false; //是否為第 5 組 ( ？
            var bRoundBrackets6 = false; //是否為第 6 組 ( ？
            var bRoundBrackets7 = false; //是否為第 7 組 ( ？
            var bRoundBrackets8 = false; //是否為第 8 組 ( ？
            var bRoundBrackets9 = false; //是否為第 9 組 ( ？
            var array = (sSql + " ").ToCharArray();

            for (var i = iStart; i < array.Length; i++)
            {
                var letter = array[i];

                if (letter.ToString() == "\"" || letter.ToString() == "'")
                {
                    switch (letter.ToString())
                    {
                        case "\"" when bDoubleQuotation:
                            bDoubleQuotation = false;
                            break;
                        case "\"":
                            {
                                if (bSingleQuotation == false)
                                {
                                    bDoubleQuotation = true;
                                }

                                break;
                            }
                        case "'" when bSingleQuotation:
                            bSingleQuotation = false;
                            break;
                        case "'":
                            {
                                if (bDoubleQuotation == false)
                                {
                                    bSingleQuotation = true;
                                }

                                break;
                            }
                    }
                }
                else if (letter.ToString() == "(")
                {
                    if (bSingleQuotation || bDoubleQuotation)
                    {
                        //括號落在單引號或雙引號之間，忽略！
                    }
                    else if (bRoundBrackets8)
                    {
                        //已偵測到第 8 組括號
                        bRoundBrackets9 = true;
                    }
                    else if (bRoundBrackets7)
                    {
                        //已偵測到第 7 組括號
                        bRoundBrackets8 = true;
                    }
                    else if (bRoundBrackets6)
                    {
                        //已偵測到第 6 組括號
                        bRoundBrackets7 = true;
                    }
                    else if (bRoundBrackets5)
                    {
                        //已偵測到第 5 組括號
                        bRoundBrackets6 = true;
                    }
                    else if (bRoundBrackets4)
                    {
                        //已偵測到第 4 組括號
                        bRoundBrackets5 = true;
                    }
                    else if (bRoundBrackets3)
                    {
                        //已偵測到第 3 組括號
                        bRoundBrackets4 = true;
                    }
                    else if (bRoundBrackets2)
                    {
                        //已偵測到第 2 組括號
                        bRoundBrackets3 = true;
                    }
                    else if (bRoundBrackets1)
                    {
                        //已偵測到第 1 組括號
                        bRoundBrackets2 = true;
                    }
                    else
                    {
                        bRoundBrackets1 = true;
                    }
                }
                else if (letter.ToString() == ")")
                {
                    if (bSingleQuotation || bDoubleQuotation)
                    {
                        //括號落在單引號或雙引號之間，忽略！
                    }
                    else if (bRoundBrackets9)
                    {
                        bRoundBrackets9 = false;
                    }
                    else if (bRoundBrackets8)
                    {
                        bRoundBrackets8 = false;
                    }
                    else if (bRoundBrackets7)
                    {
                        bRoundBrackets7 = false;
                    }
                    else if (bRoundBrackets6)
                    {
                        bRoundBrackets6 = false;
                    }
                    else if (bRoundBrackets5)
                    {
                        bRoundBrackets5 = false;
                    }
                    else if (bRoundBrackets4)
                    {
                        bRoundBrackets4 = false;
                    }
                    else if (bRoundBrackets3)
                    {
                        bRoundBrackets3 = false;
                    }
                    else if (bRoundBrackets2)
                    {
                        bRoundBrackets2 = false;
                    }
                    else
                    {
                        bRoundBrackets1 = false;
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && i >= 1 && letter.ToString() == "*" && array[i - 1].ToString() == "/")
                {
                    if (bParagraphComment == false)
                    {
                        bParagraphComment = true;
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && letter.ToString() == "*" && i + 1 <= array.GetUpperBound(0) && array[i + 1].ToString() == "/")
                {
                    if (bParagraphComment)
                    {
                        bParagraphComment = false;
                    }
                }

                if (bSingleQuotation == false && bDoubleQuotation == false && i >= 1 && letter.ToString() == "-" && array[i - 1].ToString() == "-")
                {
                    if (bSingleComment == false)
                    {
                        bSingleComment = true;
                    }
                }

                if (bSingleComment && (letter.ToString() == "\r" || letter.ToString() == "\n"))
                {
                    bSingleComment = false;
                }

                if (bParagraphComment && letter.ToString() == "*" && i + 1 < array.Length && array[i + 1].ToString() == "/")
                {
                    bParagraphComment = false;
                }

                if (letter.ToString() == ")" && bDoubleQuotation == false && bSingleQuotation == false && bSingleComment == false && bParagraphComment == false && bRoundBrackets1 == false && bRoundBrackets2 == false && bRoundBrackets3 == false && bRoundBrackets4 == false && bRoundBrackets5 == false && bRoundBrackets6 == false && bRoundBrackets7 == false && bRoundBrackets8 == false && bRoundBrackets9 == false)
                {
                    iEnd = i;
                    break;
                }
            }

            var sResult = "";

            if (iEnd > 0)
            {
                if (iEnd - iStart > 0)
                {
                    sResult = sSql.Substring(iStart + 1, iEnd - iStart - 1).Trim();
                }
            }

            return sResult.Trim();
        }

        private void HandleAC4Period(int iPosNew = 0) //iPosNew: 透過 Ctrl+J 傳過來的變數
        {
            _iPeriodPos = iPosNew == 0 ? editor.CurrentPosition : iPosNew;
            _iPeriodPos2 = _iPeriodPos;

            if (_iPeriodPos < 9)
            {
                return; //長度不夠構成一個 select 子句，直接忽略
            }

            var iPeriodPos = iPosNew == 0 ? editor.CurrentPosition - 2 : iPosNew - 2; //從句點左邊開始，找出 Alias Name
            var sAliasName = "";
            var sKeyWord = MyGlobal.sDataSource == "SQL Server" ? "." : "";

            for (var i = iPeriodPos; i > 0; i--)
            {
                if (MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_", sKeyWord))
                {
                    //
                }
                else
                {
                    if (i == iPeriodPos && (!MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1))))
                    {
                        //20221005 句點左邊第一個字元不是英文字母或數字，直接忽略
                        break;
                    }
                    else if (iPeriodPos - i < 1)
                    {
                        //不是合法的 Alias Name
                    }
                    else
                    {
                        sAliasName = editor.Text.Substring(i + 1, iPeriodPos - i).ToUpper();

                        //20221005 避開單引號或雙引號開頭的字串，例如 LIKE 'xxx. 或是 = "xxx.，這些都不需要處理
                        if (editor.Text.Substring(i, 1) == "'" || editor.Text.Substring(i, 1) == "\"")
                        {
                            sAliasName = "";
                        }

                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(sAliasName))
            {
                return; //找不到 AliasName，忽略！
            }

            var sSqlBlock = SelectBlock(false); //該段落的完整 SQL
            sSqlBlock = GetFormattedSql(sSqlBlock) + " "; //整理成一句 SQL (這裡最要再加上一個空白，因為使用者所輸入的 SQL，最後可能是結尾或是換行符號，透過 GetFormattedSql() 整理出來的 SQL，會有誤判的問題)

            var dt = new DataTable();
            var sSql = "";
            var sObjectName = "";
            var iPos = sSqlBlock.ToUpper().IndexOf(" " + sAliasName + " ", StringComparison.Ordinal);

            _sPosXY4AC = Cursor.Position.X + ", " + Cursor.Position.Y;

            if (iPos == -1) //該段落 SQL 沒有找到 Alias Name
            {
                //判斷是否為 Schema + 句點 (例如 dbo.)
                dt = DetectSchemaAndPeriod(sAliasName);
            }
            else
            {
                if (sSqlBlock.ToUpper().IndexOf(sAliasName + " AS (", StringComparison.Ordinal) != -1) //是否為 With 子句
                {
                    sSqlBlock = SelectBlock(false); //該段落的完整 SQL
                    sSqlBlock = GetFormattedSql(sSqlBlock, false); //整理成一句 SQL，不強制轉成大寫 (後續執行此 SQL，條件可能有區分大小寫)

                    //往後找出括號包起來的整段SQL
                    sSql = GetACSql4WithAs(sSqlBlock, sSqlBlock.ToUpper().IndexOf(sAliasName + " AS (", StringComparison.Ordinal) + (sAliasName + " AS (").Length - 1);
                }
                else if (sSqlBlock.ToUpper().IndexOf("WITH RECURSIVE " + sAliasName, StringComparison.Ordinal) != -1 && sSqlBlock.IndexOf(" AS (", StringComparison.Ordinal) != -1) //是否為 With 子句 (PostgreSQL 遞迴查詢)
                {
                    //PostgreSQL 的 recursive 會指向自己，所以，就算取得完整 SQL 也是無法順利執行，故，忽略它！
                    //sSqlBlock = SelectBlock(false); //該段落的完整 SQL
                    //sSqlBlock = GetFormattedSql(sSqlBlock, false); //整理成一句 SQL，不強制轉成大寫 (後續執行此 SQL，條件可能有區分大小寫)

                    //var sAliasNameNew = MyGlobal.GetStringBetween2(sSqlBlock, "WITH RECURSIVE", "AS (", false);
                    //var sSearch = "WITH RECURSIVE" + sAliasNameNew.ToUpper() + "AS (";

                    ////往後找出括號包起來的整段SQL
                    //sSql = GetACSql4WithAs(sSqlBlock, sSqlBlock.ToUpper().IndexOf(sSearch, StringComparison.Ordinal) + sSearch.Length - 1);
                }
                else if (sSqlBlock.ToUpper().IndexOf(") " + sAliasName, StringComparison.Ordinal) != -1 || sSqlBlock.ToUpper().IndexOf(") AS " + sAliasName, StringComparison.Ordinal) != -1) //是否為 (...) 子查詢 (是否有 AS 關鍵字？)
                {
                    sSqlBlock = SelectBlock(false); //該段落的完整 SQL
                    sSqlBlock = GetFormattedSql(sSqlBlock, false); //整理成一句 SQL，不強制轉成大寫 (後續執行此 SQL，條件可能有區分大小寫)

                    //往前找出起始的右括號
                    if (sSqlBlock.ToUpper().IndexOf(") AS " + sAliasName, StringComparison.Ordinal) != -1)
                    {
                        sSql = GetACSql4Subquery(sSqlBlock, sSqlBlock.ToUpper().IndexOf(") AS " + sAliasName, StringComparison.Ordinal));
                    }
                    else
                    {
                        sSql = GetACSql4Subquery(sSqlBlock, sSqlBlock.ToUpper().IndexOf(") " + sAliasName, StringComparison.Ordinal));
                    }

                    if (!string.IsNullOrEmpty(sSql))
                    {
                        if (!sSql.ToUpper().StartsWith("SELECT"))
                        {
                            sSql = ""; //不是 Select SQL，忽略！
                        }
                        else
                        {
                            var sTemp = DateTime.Now.ToString("fff");
                            //加上 LIMIT，避免撈出海量資料，佔用資源
                            sSql = "SELECT sJasonQuerys{0}.* FROM (\r\n" + sSql + ") sJasonQuerys{0} WHERE 1 = 2";
                            sSql = string.Format(sSql, sTemp);
                        }
                    }
                }
                else //取得 Alias Name 前面的名稱 (可能是 Table Name or View Name)
                {
                    sSqlBlock = SelectBlock(false); //該段落的完整 SQL
                    sSqlBlock = GetFormattedSql(sSqlBlock, false); //整理成一句 SQL，不強制轉成大寫 (後續執行此 SQL，條件可能有區分大小寫)
                    var j = sSqlBlock.ToUpper().IndexOf(" " + sAliasName + " ", StringComparison.Ordinal) - 1;

                    for (var i = j; i > 0; i--)
                    {
                        if (MyGlobal.IsEngAlphabetOrNumber(sSqlBlock.Substring(i, 1), ".", "_"))
                        {
                            //
                        }
                        else
                        {
                            if (j - i < 1)
                            {
                                //不是合法的 Object
                            }
                            else
                            {
                                sObjectName = sSqlBlock.Substring(i + 1, j - i);
                                sSql = "SELECT * FROM " + sObjectName + " WHERE 1 = 2";
                                break;
                            }
                        }
                    }

                    if (sObjectName == "FROM") //可能是以完整的 Table Name or View Name 當成 Alias Name，所以取到的是 FROM 關鍵字
                    {
                        if (sSqlBlock.IndexOf(" " + sAliasName + ".", StringComparison.Ordinal) != -1)
                        {
                            //以完整的 Table Name or View Name 當成 Alias Name
                            sSql = "SELECT * FROM " + sAliasName + " WHERE 1 = 2";
                        }
                    }
                    else if (sObjectName =="AS") //Table Name or View Name 的 Alias Name 可能有加上 AS 關鍵字
                    {
                        var k = sSqlBlock.IndexOf(" AS " + sAliasName + " ", StringComparison.Ordinal) - 1;

                        for (var i = k; i > 0; i--)
                        {
                            if (MyGlobal.IsEngAlphabetOrNumber(sSqlBlock.Substring(i, 1), ".", "_"))
                            {
                                //
                            }
                            else
                            {
                                if (k - i < 1)
                                {
                                    //不是合法的 Object
                                }
                                else
                                {
                                    sObjectName = sSqlBlock.Substring(i + 1, k - i);
                                    sSql = "SELECT * FROM " + sObjectName + " WHERE 1 = 2";
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            var bShow = false;
            var bSchemaData = false;

            if (string.IsNullOrEmpty(sSql))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    //Schema 相關 (單純 Table/View Name 或是 Node + 句點)
                    bShow = true;
                    bSchemaData = true;
                }
                else
                {
                    return;
                }
            }
            else
            {
                switch (MyGlobal.sDataSource)
                {
                    case "Oracle":
                        {
                            if (clsOracleReader.GetState() == ConnectionState.Closed)
                            {
                                MyGlobal.oOracleReader.ConnectTo();
                            }

                            dt = MyGlobal.oOracleReader.ExecuteQueryToDataTable(sSql, false);

                            if (dt != null && dt.Rows.Count >= 0)
                            {
                                bShow = true;
                            }

                            break;
                        }
                    case "PostgreSQL":
                        {
                            if (clsPostgreSQLReader.GetState() == ConnectionState.Closed)
                            {
                                MyGlobal.oPostgreReader.ConnectTo(MyGlobal.sDBConnectionString);
                            }

                            if (MyGlobal.bSavePoint)
                            {
                                MyGlobal.oPostgreReader.oSavePoint("jqcc1688ccqj");
                            }

                            dt = MyGlobal.oPostgreReader.ExecuteQueryToDataTable(sSql, false);

                            if (dt != null && dt.Rows.Count >= 0)
                            {
                                bShow = true;

                                if (MyGlobal.bSavePoint)
                                {
                                    MyGlobal.oPostgreReader.oReleasePoint("jqcc1688ccqj");
                                }
                            }
                            else
                            {
                                if (MyGlobal.bSavePoint)
                                {
                                    MyGlobal.oPostgreReader.oRollbackPoint("jqcc1688ccqj");
                                }
                            }

                            break;
                        }
                    case "SQL Server":
                        {
                            if (clsSQLServerReader.GetState() == ConnectionState.Closed)
                            {
                                MyGlobal.oSQLServerReader.ConnectTo(MyGlobal.sDBConnectionString);
                            }

                            dt = MyGlobal.oSQLServerReader.ExecuteQueryToDataTable(sSql, false);

                            if (dt != null && dt.Rows.Count >= 0)
                            {
                                bShow = true;
                            }

                            break;
                        }
                    case "MySQL":
                        {
                            if (clsMySQLReader.GetState() == ConnectionState.Closed)
                            {
                                MyGlobal.oMySQLReader.ConnectTo(MyGlobal.sDBConnectionString);
                            }

                            dt = MyGlobal.oMySQLReader.ExecuteQueryToDataTable(sSql, false);

                            if (dt != null && dt.Rows.Count >= 0)
                            {
                                bShow = true;
                            }

                            break;
                        }
                    case "SQLite":

                        break;
                    case "SQLCipher":

                        break;
                }
            }

            if (!bShow) return;

            var iPos4Point = iPosNew == 0 ? editor.CurrentPosition : iPosNew;
            var p = PointToScreen(new Point(editor.PointXFromPosition(iPos4Point), editor.PointYFromPosition(iPos4Point)));
            var iLeft = p.X - MyGlobal.iMainFormLeft + splitContainer2.SplitterDistance - 6;
            var iYShift = GetYShift();
            var iTop = p.Y - MyGlobal.iMainFormTop - iYShift;

            _dtAC4Period = new DataTable();
            _dtAC4Period.Columns.Add("C"); //ColumnName
            _dtAC4Period.Columns.Add("D"); //DataType

            if (bSchemaData)
            {
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var row = _dtAC4Period.NewRow();
                    row["C"] = dt.Rows[i]["SchemaName"].ToString();
                    row["D"] = dt.Rows[i]["SchemaType"].ToString();
                    _dtAC4Period.Rows.Add(row);
                }
            }
            else
            {
                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    var row = _dtAC4Period.NewRow();
                    row["C"] = dt.Columns[i].ColumnName.ToString();
                    row["D"] = dt.Columns[i].DataType.ToString().Replace("System.", "");
                    _dtAC4Period.Rows.Add(row);
                }
            }

            c1GridAC4Period1.Splits[0].RecordSelectors = false;
            c1GridAC4Period1.DataSource = _dtAC4Period;
            c1GridAC4Period2.Splits[0].RecordSelectors = false;
            c1GridAC4Period2.DataSource = _dtAC4Period;
            c1GridAC4Period1.Location = new Point(iLeft, iTop);
            c1GridAC4Period1.Visible = true;
            c1GridAC4Period2.Location = new Point(-200, -200);
            c1GridAC4Period2.Visible = true;

            var iWidth = MyGlobal.ResizeGridColumnWidth(c1GridAC4Period1);
            ResizeACGrid(c1GridAC4Period1, c1GridAC4Period2, _dtAC4Period.Rows.Count, iWidth); //HandleAC4Period
        }

        private int GetYShift()
        {
            var iYShift = 0;

            switch (_sQueryEditorFontSize)
            {
                case @"10":
                    {
                        switch (editor.Zoom)
                        {
                            case 20:
                                iYShift = 2;
                                break;
                            case 19:
                                iYShift = 3;
                                break;
                            case 18:
                                iYShift = 6;
                                break;
                            case 17:
                                iYShift = 7;
                                break;
                            case 16:
                                iYShift = 8;
                                break;
                            case 15:
                                iYShift = 11;
                                break;
                            case 14:
                                iYShift = 12;
                                break;
                            case 13:
                                iYShift = 12;
                                break;
                            case 12:
                                iYShift = 15;
                                break;
                            case 11:
                                iYShift = 16;
                                break;
                            case 10:
                                iYShift = 17;
                                break;
                            case 9:
                                iYShift = 21;
                                break;
                            case 8:
                                iYShift = 21;
                                break;
                            case 7:
                                iYShift = 22;
                                break;
                            case 6:
                                iYShift = 25;
                                break;
                            case 5:
                                iYShift = 26;
                                break;
                            case 4:
                                iYShift = 27;
                                break;
                            case 3:
                                iYShift = 29;
                                break;
                            case 2:
                                iYShift = 30;
                                break;
                            case 1:
                                iYShift = 31;
                                break;
                            case 0:
                                iYShift = 35;
                                break;
                            case -1:
                                iYShift = 36;
                                break;
                            case -2:
                                iYShift = 37;
                                break;
                            case -3:
                                iYShift = 40;
                                break;
                            case -4:
                                iYShift = 41;
                                break;
                            case -5:
                                iYShift = 42;
                                break;
                            case -6:
                                iYShift = 44;
                                break;
                            case -7:
                                iYShift = 45;
                                break;
                            case -8:
                                iYShift = 46;
                                break;
                            case -9:
                                iYShift = 46;
                                break;
                            case -10:
                                iYShift = 46;
                                break;
                        }

                        break;
                    }
                case @"12":
                    {
                        switch (editor.Zoom)
                        {
                            case 20:
                                iYShift = -2;
                                break;
                            case 19:
                                iYShift = 1;
                                break;
                            case 18:
                                iYShift = 2;
                                break;
                            case 17:
                                iYShift = 3;
                                break;
                            case 16:
                                iYShift = 6;
                                break;
                            case 15:
                                iYShift = 7;
                                break;
                            case 14:
                                iYShift = 8;
                                break;
                            case 13:
                                iYShift = 11;
                                break;
                            case 12:
                                iYShift = 12;
                                break;
                            case 11:
                                iYShift = 12;
                                break;
                            case 10:
                                iYShift = 15;
                                break;
                            case 9:
                                iYShift = 16;
                                break;
                            case 8:
                                iYShift = 17;
                                break;
                            case 7:
                                iYShift = 20;
                                break;
                            case 6:
                                iYShift = 21;
                                break;
                            case 5:
                                iYShift = 22;
                                break;
                            case 4:
                                iYShift = 25;
                                break;
                            case 3:
                                iYShift = 26;
                                break;
                            case 2:
                                iYShift = 28;
                                break;
                            case 1:
                                iYShift = 30;
                                break;
                            case 0:
                                iYShift = 31;
                                break;
                            case -1:
                                iYShift = 32;
                                break;
                            case -2:
                                iYShift = 35;
                                break;
                            case -3:
                                iYShift = 36;
                                break;
                            case -4:
                                iYShift = 37;
                                break;
                            case -5:
                                iYShift = 40;
                                break;
                            case -6:
                                iYShift = 41;
                                break;
                            case -7:
                                iYShift = 42;
                                break;
                            case -8:
                                iYShift = 44;
                                break;
                            case -9:
                                iYShift = 45;
                                break;
                            case -10:
                                iYShift = 47;
                                break;
                        }

                        break;
                    }
                case @"14":
                    {
                        switch (editor.Zoom)
                        {
                            case 20:
                                iYShift = -3;
                                break;
                            case 19:
                                iYShift = -2;
                                break;
                            case 18:
                                iYShift = -2;
                                break;
                            case 17:
                                iYShift = 1;
                                break;
                            case 16:
                                iYShift = 2;
                                break;
                            case 15:
                                iYShift = 3;
                                break;
                            case 14:
                                iYShift = 6;
                                break;
                            case 13:
                                iYShift = 7;
                                break;
                            case 12:
                                iYShift = 8;
                                break;
                            case 11:
                                iYShift = 11;
                                break;
                            case 10:
                                iYShift = 12;
                                break;
                            case 9:
                                iYShift = 12;
                                break;
                            case 8:
                                iYShift = 15;
                                break;
                            case 7:
                                iYShift = 16;
                                break;
                            case 6:
                                iYShift = 17;
                                break;
                            case 5:
                                iYShift = 20;
                                break;
                            case 4:
                                iYShift = 21;
                                break;
                            case 3:
                                iYShift = 22;
                                break;
                            case 2:
                                iYShift = 25;
                                break;
                            case 1:
                                iYShift = 26;
                                break;
                            case 0:
                                iYShift = 28;
                                break;
                            case -1:
                                iYShift = 30;
                                break;
                            case -2:
                                iYShift = 30;
                                break;
                            case -3:
                                iYShift = 32;
                                break;
                            case -4:
                                iYShift = 34;
                                break;
                            case -5:
                                iYShift = 36;
                                break;
                            case -6:
                                iYShift = 37;
                                break;
                            case -7:
                                iYShift = 40;
                                break;
                            case -8:
                                iYShift = 41;
                                break;
                            case -9:
                                iYShift = 42;
                                break;
                            case -10:
                                iYShift = 44;
                                break;
                        }

                        break;
                    }
                case @"16":
                    {
                        switch (editor.Zoom)
                        {
                            case 20:
                                iYShift = -7;
                                break;
                            case 19:
                                iYShift = -6;
                                break;
                            case 18:
                                iYShift = -3;
                                break;
                            case 17:
                                iYShift = -2;
                                break;
                            case 16:
                                iYShift = -2;
                                break;
                            case 15:
                                iYShift = 0;
                                break;
                            case 14:
                                iYShift = 1;
                                break;
                            case 13:
                                iYShift = 3;
                                break;
                            case 12:
                                iYShift = 6;
                                break;
                            case 11:
                                iYShift = 7;
                                break;
                            case 10:
                                iYShift = 8;
                                break;
                            case 9:
                                iYShift = 11;
                                break;
                            case 8:
                                iYShift = 12;
                                break;
                            case 7:
                                iYShift = 12;
                                break;
                            case 6:
                                iYShift = 15;
                                break;
                            case 5:
                                iYShift = 16;
                                break;
                            case 4:
                                iYShift = 17;
                                break;
                            case 3:
                                iYShift = 20;
                                break;
                            case 2:
                                iYShift = 21;
                                break;
                            case 1:
                                iYShift = 22;
                                break;
                            case 0:
                                iYShift = 25;
                                break;
                            case -1:
                                iYShift = 26;
                                break;
                            case -2:
                                iYShift = 27;
                                break;
                            case -3:
                                iYShift = 29;
                                break;
                            case -4:
                                iYShift = 30;
                                break;
                            case -5:
                                iYShift = 31;
                                break;
                            case -6:
                                iYShift = 34;
                                break;
                            case -7:
                                iYShift = 35;
                                break;
                            case -8:
                                iYShift = 36;
                                break;
                            case -9:
                                iYShift = 40;
                                break;
                            case -10:
                                iYShift = 41;
                                break;
                        }

                        break;
                    }
                case @"18":
                    {
                        switch (editor.Zoom)
                        {
                            case 20:
                                iYShift = -12;
                                break;
                            case 19:
                                iYShift = -8;
                                break;
                            case 18:
                                iYShift = -7;
                                break;
                            case 17:
                                iYShift = -6;
                                break;
                            case 16:
                                iYShift = -4;
                                break;
                            case 15:
                                iYShift = -3;
                                break;
                            case 14:
                                iYShift = -2;
                                break;
                            case 13:
                                iYShift = 1;
                                break;
                            case 12:
                                iYShift = 2;
                                break;
                            case 11:
                                iYShift = 3;
                                break;
                            case 10:
                                iYShift = 6;
                                break;
                            case 9:
                                iYShift = 7;
                                break;
                            case 8:
                                iYShift = 8;
                                break;
                            case 7:
                                iYShift = 11;
                                break;
                            case 6:
                                iYShift = 12;
                                break;
                            case 5:
                                iYShift = 12;
                                break;
                            case 4:
                                iYShift = 15;
                                break;
                            case 3:
                                iYShift = 16;
                                break;
                            case 2:
                                iYShift = 17;
                                break;
                            case 1:
                                iYShift = 20;
                                break;
                            case 0:
                                iYShift = 21;
                                break;
                            case -1:
                                iYShift = 22;
                                break;
                            case -2:
                                iYShift = 25;
                                break;
                            case -3:
                                iYShift = 26;
                                break;
                            case -4:
                                iYShift = 27;
                                break;
                            case -5:
                                iYShift = 30;
                                break;
                            case -6:
                                iYShift = 31;
                                break;
                            case -7:
                                iYShift = 32;
                                break;
                            case -8:
                                iYShift = 35;
                                break;
                            case -9:
                                iYShift = 36;
                                break;
                            case -10:
                                iYShift = 37;
                                break;
                        }

                        break;
                    }
            }
            
            return iYShift;
        }

        private void editor_KeyUp(object sender, KeyEventArgs e)
        {
            var bKeyPressCtrlJ = _bKeyPressCtrlJ;
            var bKeyPressTab = _bKeyPressTab;
            int iPosStart = 0, iPosEnd = 0;

            editor.Tag = "";

            if (_iCompoundKeyCtrlShift > 0)
            {
                _iCompoundKeyCtrlShift--; //Ctrl+Shift+? 組合鍵，會額外經過 KeyUp 3 次
                return;
            }

            //AutoReplace
            if (e.KeyCode == Keys.Space && editor.Text.Trim().Length >= 2 && editor.CurrentPosition > 2 && editor.Text.Substring(editor.CurrentPosition - 1, 1) == " ")
            {
                if (e.Control && e.KeyCode == Keys.Space)
                {
                    return;
                }

                var sTemp = "";

                for (var i = editor.CurrentPosition - 2; i >= 0; i--) //-2, 要扣掉按下的空白鍵
                {
                    sTemp = editor.Text.Substring(i, 1);

                    if (sTemp != " " && sTemp != "\n" && i != 0)
                    {
                        continue;
                    }

                    iPosStart = i == 0 ? i : i + 1;
                    iPosEnd = editor.CurrentPosition - 1;
                    break;
                }

                editor.SelectionStart = iPosStart;
                editor.SelectionEnd = iPosEnd + 1; //20200329 選取時，多取一個空白

                //尋找對應的 Key
                sTemp = MyGlobal.GetValueFromDictionary(_dicAutoReplace, editor.SelectedText.Trim()); //20200329 判斷時，忽略最後的空白

                if (string.IsNullOrEmpty(sTemp))
                {
                    //找不到，直接定位！
                    editor.SelectionStart = editor.CurrentPosition;
                }
                else
                {
                    //有找到！
                    iPosStart = sTemp.IndexOf("^", StringComparison.Ordinal); //是否有 ^ 符號

                    if (iPosStart != -1)
                    {
                        iPosStart += (editor.CurrentPosition - editor.SelectedText.Length);
                        sTemp = sTemp.Replace("^", "");
                    }

                    editor.ReplaceSelection(sTemp + " "); //20200329 置換時，多一個空白；讓置換後的游標停在空白後面

                    if (iPosStart != -1)
                    {
                        editor.SelectionStart = iPosStart;
                        editor.SelectionEnd = iPosStart;
                    }
                    else
                    {
                        editor.SelectionStart = editor.CurrentPosition;
                        editor.SelectionEnd = editor.CurrentPosition;
                    }
                }
            }

            //Auto List Members (偵測逗號旁邊的句點 Keys.OemPeriod；或是數字九宮格區塊的句點 Keys.Decimal)
            if (MyGlobal.bAutoListMembers)
            {
                //重點：
                //0. 句點的所在位置，前面應該要有完整的子句
                //1. 是否為 Table Name or View Name
                //2. 是否為 With ... AS
                //3. 是否為 (...) 子查詢
                //4. 是否為多個 Table JOIN，例如 tblABC c, tblDEF f

                if (c1GridAC4All.Visible && (e.KeyData == Keys.OemPeriod || e.KeyData == Keys.Decimal))
                {
                    //c1GridAC4All 已顯示，但此時按下句點，就要改判是否要顯示 c1GridAC4Space1
                    c1GridAC4All.Visible = false;
                }

                if (c1GridAC4All.Visible)
                {
                    //不處理
                }
                else if (e.KeyCode == Keys.Up || (e.KeyCode == Keys.V && e.Modifiers == Keys.Control) || (e.KeyCode == Keys.X && e.Modifiers == Keys.Control) || (e.KeyCode == Keys.U && e.Modifiers == Keys.Control) || (e.KeyCode == Keys.U && e.Control & e.Shift))
                {
                    if (_bKeyUpFromACGrid)
                    {
                        _bKeyUpFromACGrid = false;
                        return;
                    }

                    //Ctrl + V 或 Ctrl + X
                    HideACGrid(false); //強制隱藏 AC 清單
                }
                else if ((e.KeyCode == Keys.Multiply || e.KeyCode == Keys.D8) && c1GridAC4Period1.Visible)
                {
                    //c1GridAC4Period1 已顯示，此時又按下 * 鍵
                    HideACGrid(false); //強制隱藏 AC 清單
                }
                else if (e.KeyData == Keys.Space)
                {
                    if (c1GridAC4Period1.Visible)
                    {
                        c1GridAC4Period1.Visible = false; //Table name or View name 不應該有 空白，故輸入 空白 則要隱藏下拉清單
                    }
                    
                    if (c1GridAC4All.Visible)
                    {
                        c1GridAC4All.Visible = false; //Auto Complete 不應該有 空白，故輸入 空白 則要隱藏下拉清單
                    }

                    HandleAC4Space(); //editor_KeyUp
                }
                else if (e.KeyData == Keys.OemPeriod || e.KeyData == Keys.Decimal)
                {
                    //按下句點，如果 c1GridAC4Space1 顯示中，隱藏 c1GridAC4Space1
                    if (c1GridAC4Space1.Visible)
                    {
                        c1GridAC4Space1.Visible = false; //Table name or View name 不應該有 .，故輸入 . 則要隱藏下拉清單
                    }

                    if (c1GridAC4All.Visible)
                    {
                        c1GridAC4All.Visible = false;
                    }

                    //此處要單獨判斷，不可以接上面的 else if，否則，像 PostgreSQL 的 public. 就會被忽略了！ 
                    if (c1GridAC4Space1.Visible == false)
                    {
                        HandleAC4Period(); //editor_KeyUp
                    }
                }
                else
                {
                    if (_bKeyPressCtrlJ || bKeyPressCtrlJ)
                    {
                        //if (e.KeyData.ToString() == "ControlKey") //第一次 KeyUp 是 Ctrl+J，但還會再觸發一次 ControlKey，推測應該是因為沒有「同時放開 Ctrl、J」兩個按鍵
                        //{
                            _bKeyPressCtrlJ = false; //Ctrl+J 會回到這裡，不重複處理！
                        //}
                    }
                    else if (_bKeyPressTab || bKeyPressTab)
                    {
                        _bKeyPressTab = false; //Tab 會回到這裡，不重複處理！
                    }
                    else if (c1GridAC4Period1.Visible) //句點觸發
                    {
                        switch (e.KeyCode)
                        {
                            case Keys.Home:
                            case Keys.PageUp:
                            case Keys.PageDown:
                            //還落在句點上面，顯示！超出句點才要隱藏
                            case Keys.End when editor.CurrentPosition < _iPeriodPos - 1:
                                c1GridAC4Period1.Visible = false;
                                break;
                            case Keys.End:
                            {
                                var bHide = true;

                                for (var i = _iPeriodPos; i < editor.CurrentPosition; i++)
                                {
                                    if (editor.Text.Substring(i, 1) == " ")
                                    {
                                        bHide = false;
                                        break;
                                    }
                                    else if (editor.Text.Substring(i, 1) == "\r")
                                    {
                                        break;
                                    }
                                }

                                c1GridAC4Period1.Visible = bHide;
                                break;
                            }
                            default:
                            {
                                if (editor.CurrentPosition <= _iPeriodPos - 1) //可能是按下倒退鍵或左鍵
                                {
                                    //若是落在句點上面，也要隱藏！(←仿照 SSMS)
                                    c1GridAC4Period1.Visible = false; //超出句點隱藏！
                                }
                                else
                                {
                                    var iTemp = 0;

                                    for (var i = _iPeriodPos; i < editor.Text.Length; i++)
                                    {
                                        if (!MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_"))
                                        {
                                            //20220728 判斷輸入的字元後面接著的符號
                                            if ("` `\r`=`>`<`!`-`+`*`/`".IndexOf("`" + editor.Text.Substring(i, 1) + "`", StringComparison.Ordinal) == -1)
                                            {
                                                //20220825 後面可以是 = 或 > 或 < 或 ! 或 - 或 + 或 * 或 / (加減乘除運算類的)
                                                c1GridAC4Period1.Visible = false; //輸入了 Table name or View name 不該有的符號，直接隱藏
                                            }
                                            //else
                                            //{
                                                //允許是換行符號或是空白，表示後面接續的是其他 SQL
                                            //}

                                            break;
                                        }

                                        iTemp = i + 1;
                                    }

                                    var ss = editor.GetTextRange(_iPeriodPos, iTemp - _iPeriodPos);
                                    var iRowCount = c1GridAC4Period_Filter("[C] LIKE '" + ss + "*'");
                                    var iWidth = MyGlobal.ResizeGridColumnWidth(c1GridAC4Period1);
                                    ResizeACGrid(c1GridAC4Period1, c1GridAC4Period2, iRowCount, iWidth); //editor_KeyUp (Period)
                                }

                                break;
                            }
                        }
                    }
                    else if (c1GridAC4Space1.Visible) //空白觸發
                    {
                        switch (e.KeyCode)
                        {
                            case Keys.Home:
                            case Keys.PageUp:
                            case Keys.PageDown:
                            //還落在空白上面，顯示！超出空白才要隱藏
                            case Keys.End when editor.CurrentPosition < _iSpacePos - 1:
                                c1GridAC4Space1.Visible = false;
                                break;
                            case Keys.End:
                            {
                                var bHide = true;

                                for (var i = _iSpacePos; i < editor.CurrentPosition; i++)
                                {
                                    if (editor.Text.Substring(i, 1) == " ")
                                    {
                                        bHide = false;
                                        break;
                                    }
                                    else if (editor.Text.Substring(i, 1) == "\r")
                                    {
                                        break;
                                    }
                                }

                                c1GridAC4Space1.Visible = bHide;
                                break;
                            }
                            default:
                            {
                                if (editor.CurrentPosition <= _iSpacePos - 1) //可能是按下倒退鍵或左鍵
                                {
                                    //若是落在空白上面，也要隱藏！(←仿照 SSMS)
                                    c1GridAC4Space1.Visible = false; //超出空白隱藏！
                                }
                                else
                                {
                                    var iTemp = 0;

                                    for (var i = _iSpacePos; i < editor.Text.Length; i++)
                                    {
                                        if (!MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_"))
                                        {
                                            //20220728 判斷輸入的字元後面接著的符號
                                            if ("` `\r`=`>`<`!`-`+`*`/`".IndexOf("`" + editor.Text.Substring(i, 1) + "`", StringComparison.Ordinal) == -1)
                                            {
                                                //20220825 後面可以是 = 或 > 或 < 或 ! 或 - 或 + 或 * 或 / (加減乘除運算類的)
                                                c1GridAC4Space1.Visible = false; //輸入了 Table name or View name 不該有的符號，直接隱藏
                                            }
                                            //else
                                            //{
                                                //允許是換行符號或是空白，表示後面接續的是其他 SQL
                                            //}

                                            break;
                                        }

                                        iTemp = i + 1;
                                    }

                                    var ss = editor.GetTextRange(_iSpacePos, iTemp - _iSpacePos);
                                    var iRowCount = c1GridAC4Space_Filter("[C] LIKE '" + ss + "*'");
                                    var iWidth = MyGlobal.ResizeGridColumnWidth(c1GridAC4Space1);
                                    ResizeACGrid(c1GridAC4Space1, c1GridAC4Space2, iRowCount, iWidth); //editor_KeyUp (Space)
                                }

                                break;
                            }
                        }
                    }
                }
            }

            //20220822 for Auto Complete，確認沒有顯示 c1GridAC4Period1 / c1GridAC4Space1 才要處理
            if (MyLibrary.bEnableAutoComplete && c1GridAC4Period1.Visible == false && c1GridAC4Space1.Visible == false)
            {
                var iPos = editor.CurrentPosition - 1;
                //var sKey = iPos < 0 ? "" : editor.Text.Substring(iPos, 1); //此次按下的字母

                if (_bKeyPressCtrlJ || bKeyPressCtrlJ) //避免被「Auto List Members」變更 _bKeyPressCtrlJ 的值，故加判 bKeyPressCtrlJ
                {
                    if (e.KeyData.ToString() == "ControlKey") //第一次 KeyUp 是 Ctrl+J，但還會再觸發一次 ControlKey，推測應該是因為沒有「同時放開 Ctrl、J」兩個按鍵
                    {
                        _bKeyPressCtrlJ = false; //Ctrl+J 會回到這裡，不重複處理！
                    }
                }
                else if (_bKeyPressTab || bKeyPressTab) //避免被「Auto List Members」變更 _bKeyPressCtrlJ 的值，故加判 bKeyPressTab
                {
                    _bKeyPressTab = false; //Tab 會回到這裡，不重複處理！
                }
                else if (iPos < 0)
                {
                    //可能是按下 Home 或是 倒退鍵
                    c1GridAC4All.Visible = false;
                }
                //else if (e.KeyData.ToString() == "ControlKey" || (e.KeyData.ToString() == "ShiftKey") || (e.KeyData.ToString() == "ShiftKey, Control") || (e.KeyData.ToString() == "ControlKey, Shift"))
                //{
                //    //按下 Ctrl / Shift 複合鍵
                //    c1GridAC4All.Visible = false;
                //}
                else if (c1GridAC4Space1.Visible == false && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
                {
                    //不需處理
                }
                else if ((e.KeyCode == Keys.V && e.Modifiers == Keys.Control) || (e.KeyCode == Keys.X && e.Modifiers == Keys.Control) || (e.KeyCode == Keys.U && e.Modifiers == Keys.Control) || (e.KeyCode == Keys.U && e.Shift && e.Control) || e.KeyCode == Keys.PrintScreen || (e.KeyCode == Keys.PrintScreen && e.Modifiers == Keys.Control) || (e.KeyCode == Keys.PrintScreen && e.Modifiers == Keys.Alt))
                {
                    //Ctrl + V 或 Ctrl + X
                    HideACGrid(false); //強制隱藏 AC 清單

                    if (e.KeyCode == Keys.PrintScreen && e.Modifiers == Keys.Alt)
                    {
                        e.SuppressKeyPress = true;
                    }
                }
                else if (e.KeyData == Keys.Menu)
                {
                    HideACGrid(false); //強制隱藏 AC 清單
                }
                else if (e.KeyData == Keys.Space || e.KeyData == Keys.OemPeriod || e.KeyData == Keys.Decimal || !MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(iPos, 1), "_"))
                {
                    //空白、句點、非英數、非底線，忽略！
                    c1GridAC4All.Visible = false;
                }
                else if (e.KeyData == Keys.Enter || e.KeyData == Keys.F | e.Modifiers == Keys.Control)
                {
                    //20230608 Ctrl+F，忽略，避免畫面閃爍
                }
                else if (c1GridAC4All.Visible == false && (e.KeyData == Keys.Up || e.KeyData == Keys.Down || e.KeyData == Keys.Left || e.KeyData == Keys.Right || e.KeyData == Keys.Delete || e.KeyData == Keys.Escape || e.KeyData == Keys.Tab)) //e.KeyData == Keys.Back
                {
                    //不需處理
                }
                else if (e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown || e.KeyCode == Keys.ControlKey)
                {
                    c1GridAC4All.Visible = false;
                }
                else
                {
                    var iStart = 0; //往前找起始字母
                    var iEnd = 0; //往後找結束字母
                    //var bPeriod = false;

                    for (var i = iPos; i >= 0; i--)
                    {
                        if (MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_"))
                        {
                            iStart = i;
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (var i = iPos; i < editor.Text.Length; i++)
                    {
                        if (MyGlobal.IsEngAlphabetOrNumber(editor.Text.Substring(i, 1), "_"))
                        {
                            iEnd = i;
                        }
                        else
                        {
                            break;
                        }
                    }

                    _iAllPos = editor.CurrentPosition;
                    var sAllWord = editor.Text.Substring(iStart, iEnd - iStart + 1);

                    #region 檢查單字的前後一個字元，是否為允許的字元
                    var sPreStart = "";
                    var sNextEnd = "";

                    if (iStart > 1)
                    {
                        sPreStart = editor.Text.Substring(iStart - 1, 1);
                    }

                    if (iEnd + 1 < editor.Text.Length)
                    {
                        sNextEnd = editor.Text.Substring(iEnd + 1, 1);
                    }

                    var bCharChecking = (string.IsNullOrEmpty(sPreStart) || sPreStart == " " || sPreStart == "\n" || sPreStart == "," || sPreStart == ")") && sNextEnd != "'" && sNextEnd != "\"";
                    #endregion

                    //判斷是否為數值
                    int.TryParse(sAllWord, out var bNumeric);

                    if (bNumeric == 0 && !string.IsNullOrEmpty(sAllWord.Replace("0", "")) && bCharChecking && sAllWord.Length >= MyLibrary.iACMinFragmentLength)
                    {
                        //20220824 檢查 sAllWord 第一個字元是否為英文字母？
                        if (MyLibrary.bACFirstCharChecking == false || (MyLibrary.bACFirstCharChecking && MyGlobal.IsEngAlphabet(sAllWord.Substring(0, 1))))
                        {
                            var p = PointToScreen(new Point(editor.PointXFromPosition(iStart), editor.PointYFromPosition(iStart)));
                            var iLeft = p.X - MyGlobal.iMainFormLeft + splitContainer2.SplitterDistance - 6;
                            var iYShift = GetYShift();
                            var iTop = p.Y - MyGlobal.iMainFormTop - iYShift;
                            var iRowCount = c1GridAC4All_Filter("[ObjectName] LIKE '" + sAllWord + "*'");

                            if (iRowCount > 0)
                            {
                                var iWidth = MyGlobal.ResizeGridColumnWidth(c1GridAC4All);
                                ResizeACGrid(c1GridAC4All, c1GridAC4Period2, iRowCount, iWidth); //editor_KeyUp (AC4All)

                                c1GridAC4All.Splits[0].RecordSelectors = false;
                                c1GridAC4All.Location = new Point(iLeft, iTop);
                                c1GridAC4All.Visible = true;
                            }
                        }
                        else
                        {
                            HideACGrid(false);
                        }
                    }
                    else
                    {
                        HideACGrid(false);
                    }
                }
            }

            #region 使用者輸入「單引號/雙引號」，是否要自動產生成對的「單引號/雙引號」？
            if (e.KeyData == Keys.Oem7) //Keys.Oem7：單引號
            {

            }

            if (e.KeyData == Keys.Oem7 | e.Modifiers == Keys.Shift) //e.KeyData == Keys.Oem7 | e.Modifiers == Keys.Shift：雙引號
            {
                //var sPreChar = "";
                //var sNextChar = "";
                //var iPos = editor.CurrentPosition - 1;

                //if (iPos > 1)
                //{
                //    sPreChar = editor.Text.Substring(iPos - 1, 1);
                //}

                //if (iPos + 1 < editor.Text.Length)
                //{
                //    sNextChar = editor.Text.Substring(iPos + 1, 1);
                //}

                ////editor.InsertText(iPos + 1, "\"");
                //editor.DeleteRange(iPos + 1, 1);
            }
            #endregion

            CheckEditorContent(); //editor_KeyUp() 事件
        }

        //調整下拉清單的大小
        private static void ResizeACGrid(C1TrueDBGrid c1Grid1, C1TrueDBGrid c1Grid2, int iRowCount, int iWidth)
        {
            if (iRowCount <= 9)
            {
                iWidth = MyGlobal.ResizeGridColumnWidth(c1Grid1) + 3;
            }
            else
            {
                iWidth = MyGlobal.ResizeGridColumnWidth(c1Grid1) + c1Grid1.VScrollBar.Width + 5;
            }

            const int iHeight = 181;

            c1Grid1.Size = new Size(iWidth, iHeight);
            c1Grid2.Size = new Size(iWidth, iHeight);
        }

        private void txtIndentWord_Enter(object sender, EventArgs e)
        {
            tsEditor.BackColor = _cToolstripFocused;
        }

        private void txtIndentWord_Leave(object sender, EventArgs e)
        {
            tsEditor.BackColor = _cToolstripUnfocused;

            if (string.IsNullOrEmpty(txtIndentWord.Text) || txtIndentWord.Text == @"0")
            {
                txtIndentWord.Text = @"4";
            }
        }

        private void txtIndentWord_KeyUp(object sender, KeyEventArgs e)
        {
            //Editor 上面的垂直虛線，是根據 editor.TabWidth 來呈現的
            if (string.IsNullOrEmpty(txtIndentWord.Text.Trim()))
            {
                return;
            }

            editor.TabWidth = Convert.ToInt32(txtIndentWord.Text);
            editor.Focus();
        }

        private void txtIndentWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtIndentWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtIndentWord_MouseClick(object sender, MouseEventArgs e)
        {
            txtIndentWord.SelectionStart = 0;
            txtIndentWord.SelectionLength = 1;
            //SendKeys.Send("^(a)");
        }

        private void btnComment_Click(object sender, EventArgs e)
        {
            AddComment(); //按鈕
        }

        private void btnRemoveComment_Click(object sender, EventArgs e)
        {
            RemoveComment(); //按鈕
        }

        //從母表單傳遞資訊至指定的子表單
        private void timerMother2Child_Tick(object sender, EventArgs e)
        {
            string sFilename;
            var sTemp = "";
            string sTemp2;
            string sTemp3;
            var iX = 0;
            var iY = 0;

            //取得螢幕解析度
            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;

            var iXTemp = Cursor.Position.X;
            var iYTemp = Cursor.Position.Y;

            if (iXTemp < 300)
            {
                iX = iXTemp - 25;
            }

            if (iWidth - iXTemp < 300)
            {
                iX = 300; //游標很靠近螢幕的右側
            }

            if (iHeight - iYTemp < 300)
            {
                iY = -300; //游標很靠近螢幕的下方
            }

            if (!string.IsNullOrEmpty(MyGlobal.sCheckFileFromMDIForm) && !string.IsNullOrEmpty(AccessibleDescription) && (MyGlobal.sCheckFileFromMDIForm.Contains("`" + AccessibleDescription + "`")))
            {
                //<--Begin:CheckFileDataTimeAndExist() 之前，要先執行 MyGlobal.sCheckFileFromMDIForm 變數取代
                //判斷是否有相同的 MessageBox 正在顯示中...避免重覆顯示
                sTemp = (btnSave.Tag ?? string.Empty).ToString();
                MyGlobal.sCheckFileFromMDIForm = MyGlobal.sCheckFileFromMDIForm.Replace("`" + AccessibleDescription, "");

                if (MyGlobal.sCheckFileFromMDIForm == "`")
                {
                    MyGlobal.sCheckFileFromMDIForm = "";
                }

                if (!string.IsNullOrEmpty(sTemp)) //有存檔過
                {
                    CheckFileDateTimeAndExist(); //timerMother2Child_Tick
                }
                //-->End:CheckFileDataTimeAndExist 之前，要先執行 MyGlobal.sCheckFileFromMDIForm 變數取代

                //這裡要透過全域變數控制「本程式正在作用中」，否則按下「確定」後，從 MessageBox 切換到 Main Form，又會被 Main Form 誤判為 ContainsFocus = true
                MyGlobal.bContainsFocusFormOptionsKey = true;
            }

            //判斷是否為指定的子表單
            if (!string.IsNullOrEmpty(MyGlobal.sOpenFileFromMDIForm) && !string.IsNullOrEmpty(AccessibleDescription) && MyGlobal.sOpenFileFromMDIForm.Substring(0, 17) == AccessibleDescription)
            {
                sFilename = MyGlobal.sOpenFileFromMDIForm.Substring(15);
                MyGlobal.sOpenFileFromMDIForm = "";

                OpenFile(MyLibrary.bOpenFileOnCurrentTab, sFilename);
                return;
            }

            if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("singlerecordviewer" + MyGlobal.sSeparator)) //SingleRecordViewer表單傳來的要求？
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("singlerecordviewer" + MyGlobal.sSeparator, "");
                var sTempRow = sTemp.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1];
                sTemp = sTemp.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];

                if (sTemp.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0] == AccessibleDescription) //確認是否為指定的 Tab
                {
                    int.TryParse(sTempRow, out var iCurrentRow);

                    var c1Grid = GetWhichGrid();
                    var dtData = (DataTable)c1Grid.DataSource;
                    var iTotalRows = dtData.Rows.Count;

                    MyGlobal.dtSingleRecordViewer = GetDataRowForSingleRecordViewer(c1Grid, dtData, iCurrentRow, iTotalRows);
                    MyGlobal.sGlobalTemp = "";
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sInfoFromMDIForm) && MyGlobal.sInfoFromMDIForm.StartsWith("reloadqueryeditorsetting`")) //是否為 Editor Setting 套用？
            {
                sTemp = MyGlobal.sInfoFromMDIForm.Replace("reloadqueryeditorsetting`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sInfoFromMDIForm = MyGlobal.sInfoFromMDIForm.Replace(AccessibleDescription + ";", "");

                    if (MyGlobal.sInfoFromMDIForm == "reloadqueryeditorsetting`")
                    {
                        MyGlobal.sInfoFromMDIForm = "";
                    }

                    ApplyEditorSetting(); //timerMother2Child_Tick, 重新載入 Editor Settings

                    if (MyLibrary.bEnableAutoReplace)
                    {
                        CreateAndGetARInfoTable(_dtARInfo); //timerMother2Child_Tick, Reload Query Editor Setting
                    }

                    //20220816
                    //BuildAutoCompleteMenu(MyLibrary.bEnableAutoComplete);
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sInfoFromMDIForm) && MyGlobal.sInfoFromMDIForm.StartsWith("executecommitrollback`")) //是否為「按下 Commit / Rollback 按鈕」，或「執行 commit / rollback 指令」？
            {
                sTemp = MyGlobal.sInfoFromMDIForm.Replace("executecommitrollback`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sInfoFromMDIForm = MyGlobal.sInfoFromMDIForm.Replace(AccessibleDescription + ";", "");

                    if (MyGlobal.sInfoFromMDIForm == "executecommitrollback`")
                    {
                        MyGlobal.sInfoFromMDIForm = "";
                    }

                    btnCommit.Enabled = false;
                    btnRollback.Enabled = false;

                    DisconnectDatabase(); //共用同一個 Connection，所以，Commit/Rollback 之後，每一個 Query Editor 的 lblNotCommitYet.Text 都要清空
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sInfoFromMDIForm) && MyGlobal.sInfoFromMDIForm.StartsWith("updatecommitrollbackbutton`")) //如果是 nonquery, 執行無錯誤, 更新 Commit / Rollback 按鈕
            {
                sTemp = MyGlobal.sInfoFromMDIForm.Replace("updatecommitrollbackbutton`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sInfoFromMDIForm = MyGlobal.sInfoFromMDIForm.Replace(AccessibleDescription + ";", "");

                    if (MyGlobal.sInfoFromMDIForm == "updatecommitrollbackbutton`")
                    {
                        MyGlobal.sInfoFromMDIForm = "";
                    }

                    btnCommit.Enabled = true;
                    btnRollback.Enabled = true;
                    UpdateNotCommitYetInfo(lblNotCommitYet.Tag.ToString()); //timerMother2Child_Tick, updatecommitrollbackbutton
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sInfoFromMDIForm) && MyGlobal.sInfoFromMDIForm.StartsWith("disconnectafterqueryonly`")) //如果是單純查詢，且不需要等待 Commit / Rollback，全部「中斷連線」
            {
                sTemp = MyGlobal.sInfoFromMDIForm.Replace("disconnectafterqueryonly`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sInfoFromMDIForm = MyGlobal.sInfoFromMDIForm.Replace(AccessibleDescription + ";", "");

                    if (MyGlobal.sInfoFromMDIForm == "disconnectafterqueryonly`")
                    {
                        MyGlobal.sInfoFromMDIForm = "";
                    }

                    btnCommit.Enabled = false;
                    btnRollback.Enabled = false;

                    DisconnectDatabase(); //共用同一個 Connection，所以，Commit/Rollback 之後，每一個 Query Editor 的 lblNotCommitYet.Text 都要清空
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sInfoFromMDIForm) && MyGlobal.sInfoFromMDIForm.StartsWith("disconnectafterexecuteerror`")) //如果是 PostgreSQL，執行指令有錯誤：全部中斷連線
            {
                sTemp = MyGlobal.sInfoFromMDIForm.Replace("disconnectafterexecuteerror`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sInfoFromMDIForm = MyGlobal.sInfoFromMDIForm.Replace(AccessibleDescription + ";", "");

                    if (MyGlobal.sInfoFromMDIForm == "disconnectafterexecuteerror`")
                    {
                        MyGlobal.sInfoFromMDIForm = "";
                    }

                    btnCommit.Enabled = false;
                    btnRollback.Enabled = false;
                    UpdateNotCommitYetInfo(""); //timerMother2Child_Tick, disconnectafterexecuteerror

                    MyGlobal.oPostgreReader.oRollback();
                    MyGlobal.oPostgreReader.oDisconnect();
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("parameters" + MyGlobal.sSeparator)) //變數表單回傳的 SQL？
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("parameters" + MyGlobal.sSeparator, "").Replace(MyGlobal.sSeparator7, MyGlobal.sSeparator);

                if (sTemp.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0] == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp = "";

                    _sQueryTextParameters = sTemp.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1];
                    _sQueryTextParametersMapping = sTemp.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2];
                    _sQueryTextParametersPositionMapping = sTemp.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[3].Replace("!!!!!!!", MyGlobal.sSeparator);
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("autodisconnect`")) //是否為主表單通知要自動中斷連線？
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("autodisconnect`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp = MyGlobal.sGlobalTemp.Replace(AccessibleDescription + ";", "");

                    if (MyGlobal.sGlobalTemp == "autodisconnect`")
                    {
                        MyGlobal.sGlobalTemp = "";
                    }

                    DisconnectDatabase(); //timerMother2Child_Tick, Auto Disconnect
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("mainforminfo@menu2`")) //是否為主表單詢問 Query 功能表 《Enable/Disable》？
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("mainforminfo@menu2`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp = ""; //只作用在一個 QueryForm

                    //判斷是否有選取文字，決定功能表項目可不可用
                    MyGlobal.sGlobalTemp = "childforminfo@menu2`" + (editor.CanUndo ? "1" : "0") + ";" + (editor.CanRedo ? "1" : "0") + ";" + (string.IsNullOrEmpty(editor.SelectedText) ? "0" : "1") + ";" + (editor.CanPaste ? "1" : "0") + ";" + (string.IsNullOrEmpty(editor.Text) ? "0" : "1");
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("mainforminfo@menu1`")) //是否為主表單詢問 Edit 功能表 《Enable/Disable》？
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("mainforminfo@menu1`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp = ""; //只作用在一個 QueryForm

                    //判斷是否有選取文字，決定功能表項目可不可用
                    MyGlobal.sGlobalTemp = "childforminfo@menu1`" + (editor.CanUndo ? "1" : "0") + ";" + (editor.CanRedo ? "1" : "0") + ";" + (string.IsNullOrEmpty(editor.SelectedText) ? "0" : "1") + ";" + (editor.CanPaste ? "1" : "0") + ";" + (string.IsNullOrEmpty(editor.Text) ? "0" : "1");
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("mainformaction@menu1`")) //是否為主表單傳來的 Edit 功能表 《指令》？
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("mainformaction@menu1`", "");

                if (sTemp.Split(';')[0] == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp = ""; //只作用在一個 QueryForm

                    switch (sTemp.Split(';')[1])
                    {
                        case "Undo":
                            editor.Undo();
                            break;
                        case "Redo":
                            editor.Redo();
                            break;
                        case "Cut":
                            editor.Cut();
                            break;
                        case "Copy":
                            if (MyLibrary.bCopyAsHTML && string.IsNullOrEmpty(editor.Tag.ToString()))
                            {
                                try
                                {
                                    Clipboard.Clear();
                                    editor.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                                }
                                catch (Exception ex)
                                {
                                    _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                                    MessageBox.Show(_sLangText + @"-c06", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(editor.Tag.ToString()))
                                {
                                    CopyTextToClipboard(editor.Tag.ToString(), "02");
                                }
                                else
                                {
                                    try
                                    {
                                        editor.Copy();
                                    }
                                    catch (Exception ex)
                                    {
                                        _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                                        MessageBox.Show(_sLangText + @"-c08", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                }
                            }
                            break;
                        case "Paste":
                            try
                            {
                                editor.Paste();
                            }
                            catch (Exception ex)
                            {
                                _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Clipboard.Clear();
                            }

                            break;
                        case "Delete":
                            editor.ReplaceSelection(""); //置換內容
                            editor.SelectionStart = editor.SelectionStart; //重新選取內容
                            editor.SelectionEnd = editor.SelectionStart;
                            break;
                        case "SelectAll":
                            editor.SelectionStart = 0;
                            editor.SelectionEnd = editor.Text.Length;
                            break;
                        case "SelectCurrentBlock":
                            SelectBlock(); //timerMother2Child_Tick, "mainformaction@menu1", MainForm 下拉式功能表
                            break;
                        case "Comment":
                            AddComment(); //timerMother2Child_Tick, "mainformaction@menu1", MainForm 下拉式功能表
                            break;
                        case "Un-Comment":
                            RemoveComment(); //timerMother2Child_Tick, "mainformaction@menu1", MainForm 下拉式功能表
                            break;
                        case "Indent":
                            Indent(); //timerMother2Child_Tick, "mainformaction@menu1", MainForm 下拉式功能表
                            break;
                        case "Un-Indent":
                            Unindent(); //timerMother2Child_Tick, "mainformaction@menu1", MainForm 下拉式功能表
                            break;
                        case "UpperCase":
                            TransferStringUpperLower(true); //timerMother2Child_Tick, "mainformaction@menu1", MainForm 下拉式功能表
                            break;
                        case "LowerCase":
                            TransferStringUpperLower(false); //timerMother2Child_Tick, "mainformaction@menu1", MainForm 下拉式功能表
                            break;
                        case "SQLFormatter":
                            uSQLFormatter(); //timerMother2Child_Tick, "mainformaction@menu1", MainForm 下拉式功能表
                            break;
                    }

                    CheckEditorContent(); //timerMother2Child_Tick, "mainformaction@menu1", MainForm 下拉式功能表
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("mainformaction@menu0`")) //是否為主表單傳來的 File 功能表 指令？
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("mainformaction@menu0`", "");

                if (sTemp.Split(';')[0] == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp = ""; //只作用在一個 QueryForm

                    switch (sTemp.Split(';')[1])
                    {
                        case "Save":
                            if (btnSaveRed.Visible)
                            {
                                btnSaveRed.PerformClick();
                            }
                            else
                            {
                                btnSave.PerformClick();
                            }

                            break;
                        case "SaveAs":
                            SaveAs();
                            break;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sInfoFromReloadLocalization) && MyGlobal.sInfoFromReloadLocalization.StartsWith("reloadlocalization`")) //是否為 Reload Localization 套用？
            {
                sTemp = MyGlobal.sInfoFromReloadLocalization.Replace("reloadlocalization`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sInfoFromReloadLocalization = MyGlobal.sInfoFromReloadLocalization.Replace(AccessibleDescription + ";", "");

                    if (MyGlobal.sInfoFromReloadLocalization == "reloadlocalization`")
                    {
                        MyGlobal.sInfoFromReloadLocalization = "";
                    }

                    ApplyLocalizationSetting(); //timerMother2Child_Tick

                    //在 reloadqueryeditorsetting 區段執行即可，此處不重複處理
                    //if (MyLibrary.bEnableAutoReplace)
                    //{
                    //    CreateAndGetARInfoTable(_dtARInfo); //timerMother2Child_Tick, Reload Localization
                    //}
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("sqlexecuteerrorpos" + MyGlobal.sSeparator)) //執行 SQL Statement 出錯了：取得波浪底線要呈現的位置
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("sqlexecuteerrorpos" + MyGlobal.sSeparator, "");
                sTemp2 = sTemp.Split(';')[0]; //sAccessibleDescription
                sTemp3 = sTemp.Substring(sTemp2.Length + 1); //ex.Message + MyGlobal.sSpace5 + ex.Data["Position"].ToString() + MyGlobal.sSpace5 + SQL;

                if (sTemp2 == AccessibleDescription) //確認是否為指定的 Tab
                {
                    //把變數清空，以免重覆觸發！
                    MyGlobal.sGlobalTemp = "";

                    sTemp3 = sTemp3.Replace(MyGlobal.sSeparator5, MyGlobal.sSeparator);

                    switch (MyGlobal.sDataSource)
                    {
                        case "Oracle":
                            //定位：從 1 開始，iPos = 0
                            HandleSqlExecuteErrorPosition_Oracle(sTemp3, 0); //timerMother2Child_Tick
                            break;
                        case "PostgreSQL":
                            //定位：從 0 開始，iPos = -1
                            HandleSqlExecuteErrorPosition_PostgreSQL(sTemp3, -1); //timerMother2Child_Tick
                            break;
                        case "SQL Server":
                            //定位：從 0 開始，iPos = -1
                            HandleSqlExecuteErrorPosition_SQLServer(sTemp3, -1); //timerMother2Child_Tick
                            break;
                        case "MySQL":
                            //定位：從 1 開始，iPos = 0
                            HandleSqlExecuteErrorPosition_MySQL(sTemp3, 0); //timerMother2Child_Tick
                            break;
                        case "SQLite":
                        case "SQLCipher":
                            //定位：從 1 開始，iPos = 0
                            HandleSqlExecuteErrorPosition_SQLite(sTemp3, 0); //timerMother2Child_Tick
                            break;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("generatesqlpaste2queryeditor" + MyGlobal.sSeparator)) //產生 SQL 指令：貼至 Query Editor
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("generatesqlpaste2queryeditor" + MyGlobal.sSeparator, "");
                sTemp2 = sTemp.Split(new[] { MyGlobal.sSeparatorPlus1 }, StringSplitOptions.None)[0]; //sAccessibleDescription
                sTemp3 = sTemp.Substring(sTemp2.Length + MyGlobal.sSeparatorPlus1.Length);

                if (sTemp2 == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp = "";

                    Clipboard.SetDataObject(sTemp3, false);
                    editor.Paste();
                    editor.Focus();
                }
            }

            if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sDataSource == "SQL Server" && MyGlobal.sGlobalTemp.StartsWith("sqlserverswitchdatabasefrommainform" + MyGlobal.sSeparator)) //20220808 SQL Server，使用者透過功能表切換資料庫
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("sqlserverswitchdatabasefrommainform" + MyGlobal.sSeparator, "");
                sTemp2 = sTemp.Split(';')[0]; //sAccessibleDescription, 指定哪一個 QueryEditor
                sTemp3 = sTemp.Split(';')[1]; //切換哪一個資料庫

                if (sTemp2 == AccessibleDescription) //確認是否為指定的 Tab
                {
                    //20220808 此處只要直接執行 use 指令即可，接著會再呼叫 sqlserverswitchdatabase 執行後續動作
                    ExecuteQuery("USE " + sTemp3 + "; --Switch database by JasonQuery", false, false);
                }
            }

            if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp4) && MyGlobal.sDataSource == "SQL Server" && MyGlobal.sGlobalTemp4.StartsWith("sqlserverswitchokdatabasefrommainform" + MyGlobal.sSeparator)) //20220808 SQL Server，使用者透過功能表切換資料庫完畢，將其他頁籤的「切換資料庫」提示訊息清空
            {
                sTemp = MyGlobal.sGlobalTemp4.Replace("sqlserverswitchokdatabasefrommainform" + MyGlobal.sSeparator, "");
                sTemp2 = sTemp.Split('`')[0]; //sAccessibleDescription, 指定哪一個 QueryEditor

                if (sTemp2 == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp4 = MyGlobal.sGlobalTemp4.Replace(AccessibleDescription + "`", "");

                    SetLabelInfoEditor("", Color.Black);

                    if (MyGlobal.sGlobalTemp4 == "sqlserverswitchokdatabasefrommainform" + MyGlobal.sSeparator)
                    {
                        MyGlobal.sGlobalTemp4 = "";
                    }
                }
            }

            if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp4) && MyGlobal.sDataSource == "MySQL" && MyGlobal.sGlobalTemp4.StartsWith("mysqlswitchokdatabasefrommainform" + MyGlobal.sSeparator)) //20220818 MySQL，使用者透過功能表切換資料庫完畢，將其他頁籤的「切換資料庫」提示訊息清空
            {
                sTemp = MyGlobal.sGlobalTemp4.Replace("mysqlswitchokdatabasefrommainform" + MyGlobal.sSeparator, "");
                sTemp2 = sTemp.Split('`')[0]; //sAccessibleDescription, 指定哪一個 QueryEditor

                if (sTemp2 == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp4 = MyGlobal.sGlobalTemp4.Replace(AccessibleDescription + "`", "");

                    SetLabelInfoEditor("", Color.Black);

                    if (MyGlobal.sGlobalTemp4 == "mysqlswitchokdatabasefrommainform" + MyGlobal.sSeparator)
                    {
                        MyGlobal.sGlobalTemp4 = "";
                    }
                }
            }

            if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp2) && MyGlobal.sDataSource == "SQL Server" && MyGlobal.sGlobalTemp2.StartsWith("sqlserverswitchdatabase" + MyGlobal.sSeparator)) //20220423 SQL Server 使用 USE 指令切換資料庫
            {
                sTemp = MyGlobal.sGlobalTemp2.Replace("sqlserverswitchdatabase" + MyGlobal.sSeparator, "");
                sTemp2 = sTemp.Split(';')[0]; //sAccessibleDescription, 識別從哪一個 QueryEditor 傳過來的 SQL
                sTemp3 = sTemp.Split(';')[1]; //切換哪一個資料庫

                if (sTemp2 == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp2 = "";

                    if (MyGlobal.sDBConnectionString.IndexOf("Initial Catalog", StringComparison.Ordinal) == -1)
                    {
                        MyGlobal.sDBConnectionString = MyGlobal.sDBConnectionString.Replace("Integrated Security", "Initial Catalog=" + sTemp3 + ";Integrated Security");
                    }
                    else
                    {
                        var sTemp4 = MyGlobal.GetStringBetween2(MyGlobal.sDBConnectionString, "Initial Catalog=", ";", false);

                        if (!string.IsNullOrEmpty(sTemp4))
                        {
                            MyGlobal.sDBConnectionString = MyGlobal.sDBConnectionString.Replace("Initial Catalog=" + sTemp4 + ";", "Initial Catalog=" + sTemp3 + ";");
                        }
                    }

                    MyGlobal.sDBConnectionDatabase = sTemp3;

                    //傳遞資訊至 MainForm，更新 Database 資訊
                    TransferValueToMainForm("updatedatabaseinfo`" + MyGlobal.sDBConnectionDatabase); //timerMother2Child_Tick

                    //20220423 不能重新連線，否則 transaction 會跟之前的不同，導致之前未 Commit 的異動會消失！
                    //////////ConnectToDatabase(); //重新連線！

                    //20220803 切換資料庫，清空 Label 訊息
                    SetLabelInfoEditor("", Color.Black);

                    MyGlobal.RefreshDataForSchemaSearch(); //timerMother2Child_Tick

                    var myForm = new frmInfo();
                    var sMsg = MyGlobal.GetLanguageString("Please wait...", "Global", "Global", "msg", "PleaseWait", "Text");
                    myForm.sCaption = sMsg;

                    if (MyGlobal.bShowColumnInfo)
                    {
                        sMsg = MyGlobal.GetLanguageString("Getting schema information (include all column information of Tables) ...", "Global", "Global", "msg", "GetSchemaInfoIncludeColumn", "Text");
                    }
                    else
                    {
                        sMsg = MyGlobal.GetLanguageString("Getting schema information...", "Global", "Global", "msg", "GetSchemaInfo", "Text");
                    }

                    myForm.sInfo = sMsg;
                    myForm.bMovingPosition = true;
                    myForm.StartPosition = FormStartPosition.CenterScreen;
                    myForm.TopLevel = true; //只在 JasonQuery 最上層顯示
                    myForm.Show(this);

                    //依切換的資料庫，重新載入一次最新的 Schema Data for SchemaBrowser
                    Application.UseWaitCursor = true;
                    MyGlobal.UpdateSchemaData_SQLServer(c1GridSchemaBrowser, false); //timerMother2Child_Tick, SQL Server
                    MyGlobal.UpdateTableAndViewInfo4AC_SQLServer(MyGlobal.sDBConnectionDatabase); //透過 USE 指令 切換資料庫
                    MyGlobal.dtTableAndViewName.Merge(MyGlobal.dtDatabaseName); //切換資料庫後，要再合併所有的資料庫名稱，供 USE 指令使用
                    Application.UseWaitCursor = false;

                    myForm.Dispose();

                    AutoResizeGridColumnWidth(); //timerMother2Child_Tick, sqlserverswitchdatabase

                    TransferValueToMainForm("sqlserverswitchokdatabasebyusecommand`" + AccessibleDescription);

                    //20220805 切換資料庫，Tag = "nonquery"，最後會直接切換到「tabMessage」頁籤
                    editorMessage.Tag = "nonquery";
                }
            }

            if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sDataSource == "MySQL" && MyGlobal.sGlobalTemp.StartsWith("mysqlswitchdatabasefrommainform" + MyGlobal.sSeparator)) //20220808 MySQL，使用者透過功能表切換資料庫
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("mysqlswitchdatabasefrommainform" + MyGlobal.sSeparator, "");
                sTemp2 = sTemp.Split(';')[0]; //sAccessibleDescription, 指定哪一個 QueryEditor
                sTemp3 = sTemp.Split(';')[1]; //切換哪一個資料庫
                var sTemp4 = sTemp.Split(';')[2]; //是否有其他頁籤要清空「切換資料庫」的提示訊息？

                if (sTemp2 == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp = "";

                    //20220808 此處只要直接執行 use 指令即可，接著會再呼叫 mysqlswitchdatabase 執行後續動作
                    ExecuteQuery("USE " + sTemp3 + ";  /*Switch database by JasonQuery*/", false, false);

                    if (!string.IsNullOrEmpty(sTemp4))
                    {
                        MyGlobal.sGlobalTemp4 = "mysqlswitchokdatabasefrommainform" + MyGlobal.sSeparator + sTemp4;
                    }

                    var myForm = new frmInfo();
                    var sMsg = MyGlobal.GetLanguageString("Please wait...", "Global", "Global", "msg", "PleaseWait", "Text");
                    myForm.sCaption = sMsg;

                    if (MyGlobal.bShowColumnInfo)
                    {
                        sMsg = MyGlobal.GetLanguageString("Getting schema information (include all column information of Tables) ...", "Global", "Global", "msg", "GetSchemaInfoIncludeColumn", "Text");
                    }
                    else
                    {
                        sMsg = MyGlobal.GetLanguageString("Getting schema information...", "Global", "Global", "msg", "GetSchemaInfo", "Text");
                    }

                    myForm.sInfo = sMsg;
                    myForm.bMovingPosition = true;
                    myForm.StartPosition = FormStartPosition.CenterScreen;
                    myForm.TopLevel = true; //只在 JasonQuery 最上層顯示
                    myForm.Show(this);

                    //依切換的資料庫，重新載入一次最新的 Schema Data for SchemaBrowser
                    Application.UseWaitCursor = true;
                    MyGlobal.UpdateSchemaData_MySQL(c1GridSchemaBrowser); //timerMother2Child_Tick, MySQL
                    Application.UseWaitCursor = false;

                    myForm.Dispose();

                    AutoResizeGridColumnWidth(); //timerMother2Child_Tick, mysqlswitchdatabasefrommainform

                    //20220817 此處不能使用 MyGlobal.sGlobalTemp，會跟切換的變數搞混
                    MyGlobal.sGlobalTemp5 = "reloadschemainfo`" + AccessibleDescription + ";" + sTemp4.Replace("`", ";");
                }
            }

            if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp4) && MyGlobal.sDataSource == "MySQL" && MyGlobal.sGlobalTemp4.StartsWith("mysqlswitchokdatabasefrommainform" + MyGlobal.sSeparator)) //20220808 MySQL，使用者透過功能表切換資料庫完畢，將其他頁籤的「切換資料庫」提示訊息清空
            {
                sTemp = MyGlobal.sGlobalTemp4.Replace("mysqlswitchokdatabasefrommainform" + MyGlobal.sSeparator, "");
                sTemp2 = sTemp.Split('`')[0]; //sAccessibleDescription, 指定哪一個 QueryEditor

                if (sTemp2 == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp4 = MyGlobal.sGlobalTemp4.Replace(AccessibleDescription + "`", "");

                    SetLabelInfoEditor("", Color.Black);

                    if (MyGlobal.sGlobalTemp4 == "mysqlswitchokdatabasefrommainform" + MyGlobal.sSeparator)
                    {
                        MyGlobal.sGlobalTemp4 = "";
                    }
                }
            }

            if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp2) && MyGlobal.sDataSource == "MySQL" && MyGlobal.sGlobalTemp2.StartsWith("mysqlswitchdatabase" + MyGlobal.sSeparator)) //20220515 MySQL 使用 USE 指令切換資料庫，重新連線！
            {
                sTemp = MyGlobal.sGlobalTemp2.Replace("mysqlswitchdatabase" + MyGlobal.sSeparator, "");
                sTemp2 = sTemp.Split(';')[0]; //sAccessibleDescription, 識別從哪一個 QueryEditor 傳過來的 SQL
                sTemp3 = sTemp.Split(';')[1]; //切換哪一個資料庫

                if (sTemp2 == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp2 = "";

                    if (MyGlobal.sDBConnectionString.IndexOf("Database=", StringComparison.Ordinal) == -1)
                    {
                        MyGlobal.sDBConnectionString = MyGlobal.sDBConnectionString.Replace("Unicode", "Database=" + sTemp3 + ";Unicode");
                    }
                    else
                    {
                        var sTemp4 = MyGlobal.GetStringBetween2(MyGlobal.sDBConnectionString, "Database=", ";", false);

                        if (!string.IsNullOrEmpty(sTemp4))
                        {
                            MyGlobal.sDBConnectionString = MyGlobal.sDBConnectionString.Replace("Database=" + sTemp4 + ";", "Database=" + sTemp3 + ";");
                        }
                    }

                    MyGlobal.sDBConnectionDatabase = sTemp3;

                    //傳遞資訊至 MainForm，更新 Database 資訊
                    TransferValueToMainForm("updatedatabaseinfo`" + MyGlobal.sDBConnectionDatabase); //timerMother2Child_Tick

                    //20220803 切換資料庫，清空 Label 訊息
                    SetLabelInfoEditor("", Color.Black);

                    MyGlobal.RefreshDataForSchemaSearch(); //timerMother2Child_Tick

                    var myForm = new frmInfo();
                    var sMsg = MyGlobal.GetLanguageString("Please wait...", "Global", "Global", "msg", "PleaseWait", "Text");
                    myForm.sCaption = sMsg;

                    if (MyGlobal.bShowColumnInfo)
                    {
                        sMsg = MyGlobal.GetLanguageString("Getting schema information (include all column information of Tables) ...", "Global", "Global", "msg", "GetSchemaInfoIncludeColumn", "Text");
                    }
                    else
                    {
                        sMsg = MyGlobal.GetLanguageString("Getting schema information...", "Global", "Global", "msg", "GetSchemaInfo", "Text");
                    }

                    myForm.sInfo = sMsg;
                    myForm.bMovingPosition = true;
                    myForm.StartPosition = FormStartPosition.CenterScreen;
                    myForm.TopLevel = true; //只在 JasonQuery 最上層顯示
                    myForm.Show(this);

                    //依切換的資料庫，重新載入一次最新的 Schema Data for SchemaBrowser
                    Application.UseWaitCursor = true;
                    MyGlobal.UpdateSchemaData_MySQL(c1GridSchemaBrowser); //timerMother2Child_Tick, MySQL
                    MyGlobal.UpdateTableAndViewInfo4AC_MySQL(MyGlobal.sDBConnectionDatabase); //透過 USE 指令 切換資料庫
                    MyGlobal.dtTableAndViewName.Merge(MyGlobal.dtDatabaseName); //切換資料庫後，要再合併所有的資料庫名稱，供 USE 指令使用
                    Application.UseWaitCursor = false;

                    myForm.Dispose();

                    AutoResizeGridColumnWidth(); //timerMother2Child_Tick, mysqlswitchdatabase

                    TransferValueToMainForm("mysqlswitchokdatabasebyusecommand`" + AccessibleDescription);

                    //20220805 切換資料庫，Tag = "nonquery"，最後會直接切換到「tabMessage」頁籤
                    editorMessage.Tag = "nonquery";
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("sqlexecuteaffected" + MyGlobal.sSeparator)) //執行「異動的 SQL」，取得異動的筆數
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("sqlexecuteaffected" + MyGlobal.sSeparator, "");
                sTemp2 = sTemp.Split(';')[0]; //sAccessibleDescription, 識別從哪一個 QueryEditor 傳過來的 SQL
                sTemp3 = sTemp.Split(';')[1]; //異動的筆數

                if (sTemp2 == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.sGlobalTemp = "";

                    UpdateMessage(sTemp3); //timerMother2Chile_Tick //sqlexecuteaffected
                    editorMessage.Tag = "nonquery";
                    editorMessage.LineScroll(editorMessage.Lines.Count, 0);
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("hideaclist`")) //隱藏 AC 下拉清單
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("hideaclist`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    HideACGrid();

                    //將關鍵字移除，以免重覆觸發！
                    MyGlobal.sGlobalTemp = MyGlobal.sGlobalTemp.Replace(AccessibleDescription + ";", "");

                    if (MyGlobal.sGlobalTemp == "hideaclist`")
                    {
                        MyGlobal.sGlobalTemp = "";
                    }
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("reloadschemainfo`")) //切換資料庫，重新載入 Schema Info
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("reloadschemainfo`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    MyGlobal.UpdateSchemaData(c1GridSchemaBrowser, true); //timerMother2Child_Tick, reloadschemainfo
                    AutoResizeGridColumnWidth(); //timerMother2Child_Tick, reloadschemainfo

                    //將關鍵字移除，以免重覆觸發！
                    MyGlobal.sGlobalTemp = MyGlobal.sGlobalTemp.Replace(AccessibleDescription + ";", "");

                    if (MyGlobal.sGlobalTemp == "reloadschemainfo`")
                    {
                        MyGlobal.sGlobalTemp = "";
                    }
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp5) && MyGlobal.sGlobalTemp5.StartsWith("reloadschemainfo`")) //切換資料庫，重新載入 Schema Info
            {
                sTemp = MyGlobal.sGlobalTemp5.Replace("reloadschemainfo`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    //將關鍵字移除，以免重覆觸發！
                    MyGlobal.sGlobalTemp5 = MyGlobal.sGlobalTemp5.Replace(AccessibleDescription + ";", "");

                    if (MyGlobal.sGlobalTemp5 == "reloadschemainfo`")
                    {
                        MyGlobal.sGlobalTemp5 = "";
                    }

                    MyGlobal.UpdateSchemaData(c1GridSchemaBrowser, true); //timerMother2Child_Tick, reloadschemainfo
                    AutoResizeGridColumnWidth(); //timerMother2Child_Tick, reloadschemainfo
                }
            }

            if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("closequeryformandcheckcommit`")) //檢查是否需要 commit
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("closequeryformandcheckcommit`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp == AccessibleDescription) //確認是否為指定的 Tab
                {
                    //把關鍵字串重新命名，以免重覆觸發！
                    MyGlobal.sGlobalTemp = MyGlobal.sGlobalTemp.Replace(AccessibleDescription + ";", "+" + AccessibleDescription + "+;");

                    if (!string.IsNullOrEmpty(lblNotCommitYet.Text) && MyGlobal.iCommitCheck == -1)
                    {
                        _sLangText = "JasonQuery - Commit / Rollback";

                        //修正 MsgBox 顯示的位置
                        FindAndMoveMsgBox(Cursor.Position.X - iX - 200, Cursor.Position.Y + iY, true, _sLangText);

                        sTemp2 = MyGlobal.GetLanguageString("Your connection to {DBType} is closing.", "form", Name, "msg", "CommitPrompt1", "Text");
                        sTemp2 = sTemp2.Replace("{DBType}", MyGlobal.sDataSource);
                        sTemp2 = sTemp2 + "\r\n" + MyGlobal.GetLanguageString("JasonQuery has detected that you have uncommitted changes in that session.", "form", Name, "msg", "CommitPrompt2", "Text");
                        sTemp2 = sTemp2 + "\r\n\r\n" + MyGlobal.GetLanguageString("Yes: Commit!", "form", Name, "msg", "CommitPrompt_Yes", "Text");
                        sTemp2 = sTemp2 + "\r\n" + MyGlobal.GetLanguageString("No: Rollback!", "form", Name, "msg", "CommitPrompt_No", "Text");
                        sTemp2 = sTemp2 + "\r\n" + MyGlobal.GetLanguageString("Cancel: Do nothing!", "form", Name, "msg", "CommitPrompt_Cancel", "Text");

                        var result = MessageBox.Show(sTemp2, _sLangText, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        switch (result)
                        {
                            case DialogResult.Yes:
                                MyGlobal.iCommitCheck = 1;
                                btnCommit.PerformClick();
                                TransferValueToMainForm("executecommitrollback`"); //timerMother2Child_Tick
                                break;
                            case DialogResult.No:
                                //使用者選擇「不要 commit」！
                                MyGlobal.iCommitCheck = 0;
                                TransferValueToMainForm("executecommitrollback`"); //timerMother2Child_Tick
                                break;
                            default:
                                //使用者按下取消！
                                MyGlobal.iCommitCheck = 2; //此處設定為 2(取消)
                                MyGlobal.sGlobalTemp = ""; //後續不用再判斷了
                                MyGlobal.iCloseDialogX = 0;
                                MyGlobal.iCloseDialogY = 0;
                                return;
                        }
                    }

                    //將關鍵字移除
                    MyGlobal.sGlobalTemp = MyGlobal.sGlobalTemp.Replace("+" + AccessibleDescription + "+;", "");

                    if (MyGlobal.sGlobalTemp == "closequeryformandcheckcommit`")
                    {
                        MyGlobal.sGlobalTemp = "";
                    }
                }
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("closequeryform`")) //由 TabControl 關閉 Query Form (關閉單一 Form)
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("closequeryform`", "");
                sTemp = sTemp.Split(';')[0];

                if (sTemp != AccessibleDescription)
                {
                    return;
                }

                //處理過程中：先把關鍵字串重新命名，以免重覆觸發！
                MyGlobal.sGlobalTemp = MyGlobal.sGlobalTemp.Replace(AccessibleDescription + ";", "+" + AccessibleDescription + "+;");

                //檢查檔案是否需要存檔？
                if (btnSaveRed.Visible || editor.CanUndo)
                {
                    sTemp = (btnSave.Tag ?? Tag.ToString()).ToString();

                    _sLangText = MyGlobal.GetLanguageString("Save", "form", Name, "msg", "Save", "Text");

                    //修正 MsgBox 顯示的位置
                    if (MyGlobal.iCloseDialogX == 0 && MyGlobal.iCloseDialogY == 0)
                    {
                        FindAndMoveMsgBox(Cursor.Position.X - iX, Cursor.Position.Y + iY, true, _sLangText);
                    }
                    else
                    {
                        FindAndMoveMsgBox(MyGlobal.iCloseDialogX, MyGlobal.iCloseDialogY, true, _sLangText);
                    }

                    sTemp2 = MyGlobal.GetLanguageString("Save file?", "form", Name, "msg", "SaveFile", "Text");
                    var result = MessageBox.Show(sTemp2 + "\r\n\r\n\"" + sTemp + "\"", _sLangText, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (result)
                    {
                        case DialogResult.Yes:
                        {
                            if (Save() == false) //在 MainForm 關閉 Tab，詢問是否存檔，使用者選擇「是」要存檔！
                            {
                                //存檔出現狀況
                                return;
                            }

                            break;
                        }
                        case DialogResult.No:
                            //使用者選擇「不要存檔」！
                            break;
                        default:
                            MyGlobal.sGlobalTemp = MyGlobal.sGlobalTemp.Replace("+" + AccessibleDescription + "+;", AccessibleDescription + "|CANCEL;");
                            MyGlobal.iCloseDialogX = 0;
                            MyGlobal.iCloseDialogY = 0;
                            return; //使用者按下取消，告訴 MainForm，此 Tab 不要關閉了！
                    }
                }

                //沒異常或檔案不需要存檔，將關鍵字移除
                MyGlobal.sGlobalTemp = MyGlobal.sGlobalTemp.Replace("+" + AccessibleDescription + "+;", "");

                DisconnectDatabase(); //timerMother2Child_Tick, 關閉單一 Query Form

                if (MyGlobal.sGlobalTemp == "closequeryform`")
                {
                    MyGlobal.sGlobalTemp = "";
                }

                return;
            }
            else if (!string.IsNullOrEmpty(MyGlobal.sInfoFromMDIForm) && MyGlobal.sInfoFromMDIForm.StartsWith("transferselectsql`")) //「不是」第一次開啟 Query Form，要透過全域變數來判斷
            {
                sTemp = MyGlobal.sInfoFromMDIForm.Replace("transferselectsql`", "");

                if (sTemp.Split(';')[0] == AccessibleDescription) //確認是否為指定的 Tab
                {
                    sTemp = sTemp.Split(';')[1];

                    var i = editor.Text.Length + (string.IsNullOrEmpty(editor.Text) ? 0 : 2);
                    editor.Text = (string.IsNullOrEmpty(editor.Text) ? sTemp : editor.Text + "\r\n" + sTemp);
                    editor.SelectionStart = i;
                    editor.SelectionEnd = i + sTemp.Length + 2;
                    editor.ScrollCaret();

                    MyGlobal.sInfoFromMDIForm = "";
                    MyGlobal.sGlobalTemp = "Rename4SchemaBrowser"; //貼上 SQL 後，要再呼叫主表單變更 Schema Browser's Tab Name (會同時自動切換至 *SQL Editor)
                }
            }

            //此處的 AccessibleDescription，指的是 frmMain.cs 裡面的 f1.AccessibleDescription
            //此處的 AccessibleDefaultActionDescription，指的是 frmMain.cs 裡面的 f1.AccessibleDefaultActionDescription
            //第一次開啟 Query Form 時可以判斷 AccessibleDefaultActionDescription，之後的就要透過全域變數才行
            if (!string.IsNullOrEmpty(AccessibleDefaultActionDescription) && AccessibleDefaultActionDescription.Length > 0) // && (AccessibleDescription.Length - AccessibleDescription.Replace("`", "").Length == 1))
            {
                if (AccessibleDefaultActionDescription.StartsWith("SQL:"))
                {
                    sTemp = AccessibleDefaultActionDescription.Substring(4, AccessibleDefaultActionDescription.Length - 4);
                    AccessibleDefaultActionDescription = "";
                    editor.Text = sTemp + "\r\n";
                    editor.SelectionStart = 0;
                    editor.SelectionEnd = sTemp.Length + 2;
                }
                else if (AccessibleDefaultActionDescription.StartsWith("OPEN0:") || AccessibleDefaultActionDescription.StartsWith("OPEN1:") || AccessibleDefaultActionDescription.StartsWith("OPEN2:"))
                {
                    sFilename = AccessibleDefaultActionDescription.Substring(6, AccessibleDefaultActionDescription.Length - 6);

                    sTemp2 = MyGlobal.GetLanguageString("File not found.", "form", Name, "msg", "FileNotFound", "Text");
                    sTemp3 = MyGlobal.GetLanguageString("Yes: Create it!", "form", Name, "msg", "YesCreateIt", "Text");
                    var sTemp5 = MyGlobal.GetLanguageString("Cancel: Do nothing!", "form", Name, "msg", "DoNothing", "Text");
                    string sTemp4;
                    string sMsgText;
                    string sMsgCaption;

                    if (AccessibleDefaultActionDescription.StartsWith("OPEN1:")) //OPEN1: Recent Files
                    {
                        sTemp = "1";
                        _sLangText = MyGlobal.GetLanguageString("Recent Files", "form", Name, "msg", "RecentFiles", "Text");
                        sMsgCaption = _sLangText;
                        sTemp4 = MyGlobal.GetLanguageString("No: Remove the file from Recent Files!", "form", Name, "msg", "RemoveFromRecentFiles", "Text");
                        sMsgText = sTemp2 + "\r\n\r\n\"" + sFilename + "\"\r\n\r\n" + sTemp3 + "\r\n" + sTemp4 + "\r\n" + sTemp5;
                    }
                    else if (AccessibleDefaultActionDescription.StartsWith("OPEN2:")) //OPEN2: My Favorite
                    {
                        sTemp = "2";
                        _sLangText = MyGlobal.GetLanguageString("My Favorite", "form", Name, "msg", "MyFavorite", "Text");
                        sMsgCaption = _sLangText;
                        sTemp4 = MyGlobal.GetLanguageString("No: Remove the file from My Favorite", "form", Name, "msg", "RemoveFromMyFavorite", "Text");
                        sMsgText = sTemp2 + "\r\n\r\n\"" + sFilename + "\"\r\n\r\n" + sTemp3 + "\r\n" + sTemp4 + "\r\n" + sTemp5;
                    }
                    else //OPEN0: 一般開檔
                    {
                        sTemp = "0";
                        _sLangText = MyGlobal.GetLanguageString("Create new file", "form", Name, "msg", "CreateNewFile", "Text");
                        sMsgCaption = _sLangText;
                        sTemp4 = MyGlobal.GetLanguageString("OK: Create it?", "form", Name, "msg", "OKCreateIt", "Text");
                        sMsgText = sTemp2 + "\r\n\r\n\"" + sFilename + "\"\r\n\r\n" + sTemp4 + "\r\n" + sTemp5;
                    }

                    AccessibleDefaultActionDescription = "";

                    //以下程式碼，是從「Open File」按鈕複製來的！
                    //檢查檔案是否存在，不存在，則詢問是否要建立 (從母表單傳遞過來的 Recent 檔名，有可能不存在/被刪除)
                    if (File.Exists(sFilename) == false)
                    {
                        FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, sMsgCaption);

                        if (sTemp == "0")
                        {
                            if (MessageBox.Show(sMsgText, sMsgCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                editor.Text = "";
                                WriteFile(sFilename); //以指定路徑 + 檔名存檔，內容是空白的
                            }
                            else
                            {
                                //20191029 透過 MainForm 關閉空白的 Tab
                                TransferValueToMainForm("closeemptytab`");
                                return;
                            }
                        }
                        else
                        {
                            var myResult = MessageBox.Show(sMsgText, sMsgCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                            switch (myResult)
                            {
                                case DialogResult.Yes:
                                    editor.Text = "";
                                    WriteFile(sFilename); //以指定路徑+檔名存檔，內容是空白的
                                    break;
                                case DialogResult.No:
                                {
                                    if (sTemp == "1")
                                    {
                                        //將此檔案從 Recent Files 移除: 透過 MainForm 移除
                                        TransferValueToMainForm("removefromrecentfiles`" + sFilename);
                                    }
                                    else
                                    {
                                        //將此檔案從 My Favorite Lists 移除: 透過 MainForm 移除
                                        TransferValueToMainForm("removefrommyfavoritelists`" + sFilename);
                                    }

                                    TransferValueToMainForm("closeemptytab`");
                                    return;
                                }
                                default:
                                    //20191029 透過 MainForm 關閉空白的 Tab
                                    TransferValueToMainForm("closeemptytab`");
                                    return;
                            }
                        }
                    }
                    else
                    {
                        MyGlobal.sCheckExistTabResult = "";

                        //傳資訊到母表單，檢查 Tab 資訊，此檔案是否已被開啟了
                        TransferValueToMainForm("checkexisttab`" + sFilename);

                        while (!string.IsNullOrEmpty(MyGlobal.sCheckExistTabResult))
                        {
                            Application.DoEvents();
                            break;
                        }

                        if (MyGlobal.sCheckExistTabResult == "TRUE")
                        {
                            MyGlobal.sCheckExistTabResult = "";
                            return;
                        }

                        if (LoadFile(sFilename, out var bLargeFile) == false) //timerMother2Child_Tick
                        {
                            if (!string.IsNullOrEmpty(MyGlobal.sCancelOpenAndCloseTab))
                            {
                                //20191013 關閉 - 要透過 MainForm 才行
                                TransferValueToMainForm("closeemptytab`");
                            }

                            return;
                        }

                        //傳資訊到母表單，更新 Tab 資訊
                        TransferValueToMainForm("updatetabinfo`" + sFilename);

                        //傳資訊到母表單，更新 Recent Files 資訊
                        TransferValueToMainForm("updaterecentfiles`" + sFilename);

                        btnSave.Tag = sFilename;
                        btnSaveRed.Tag = File.GetLastWriteTime(sFilename).ToString("yyyy/MM/dd HH:mm:ss");
                        btnSave.Enabled = true;
                        btnSaveAs.Enabled = true;

                        editor.EmptyUndoBuffer();

                        if (bLargeFile)
                        {
                            editor.AppendText("\r\n"); //20190909 加一個換行符號，CanUnDo 就會是 true，(TabControl)檔名前面就會自動加上一個星號 (檔案未存檔)
                        }

                        CheckEditorContent(); //timerMother2Child_Tick() 事件
                    }
                }
            }

            //判斷是否有從外部傳進來要開啟的檔案
            if (!string.IsNullOrEmpty(MyGlobal.sOpenFileFromExternal))
            {
                sTemp = MyGlobal.sOpenFileFromExternal;
                MyGlobal.sOpenFileFromExternal = "";

                OpenFile(MyLibrary.bOpenFileOnCurrentTab, sTemp);
            }

            if (string.IsNullOrEmpty(_sQueryStatus))
            {
                return;
            }

            //20210529 SQL 指令執行結束, _sQueryStatus == "Complete"
            //可能是沒錯誤正常結束，也可能是有錯誤；都會是 "Complete"

            _dtEndTime = DateTime.Now;
            lblExecTime.Tag = MyGlobal.DateDiff(_dtStartTime, _dtEndTime);
            lblExecTime.Text = _sExecTime + @" " + lblExecTime.Tag;
            lblRows.Text = c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count.ToString("N0") + " " + _sRows;
            //UpdateLabelInfo(false, "Processing OK!"); //timerMother2Child_Tick(), _sQueryStatus
            tmrExecTime.Enabled = false;

            c1TrueDBGrid1.Row = 0;
            c1TrueDBGrid1.Col = 0;
            c1TrueDBGrid1.Select();
            c1TrueDBGrid1.Enabled = true;

            foreach (Control tab in c1DockingTab1.TabPages)
            {
                var tabPage = (C1DockingTabPage)tab;

                foreach (var ctrlTab in tabPage.Controls)
                {
                    if (ctrlTab.GetType().Name != "C1TrueDBGrid")
                    {
                        continue;
                    }

                    if (sTemp != "c1TrueDBGrid" + _iQueryIndex)
                    {
                        continue;
                    }

                    GridFontAndBackgroundColor((C1TrueDBGrid)ctrlTab); //timerMother2Child_Tick
                    GridZoom((C1TrueDBGrid)ctrlTab); //timerMother2Child_Tick
                }
            }

            var sResult = _sQueryStatus;
            var sMessage = "";

            editorSQLHistory.ReadOnly = false;

            switch (editorMessage.Tag.ToString())
            {
                case "error":
                    {
                        sResult = "Error";
                        editorSQLHistory.Text = editorSQLHistory.Text.Replace(MyGlobal.sSeparator3, "\r\n--Status: \"Error\", " + lblExecTime.Text + "\r\n--" + editorMessage.Text.Replace("\r\n", "\r\n--"));
                        sMessage = editorMessage.Text;

                        if (MyGlobal.sDataSource == "PostgreSQL" && MyGlobal.bDBAutoRollback == false)
                        {
                            btnRollback.Enabled = true;
                        }

                        break;
                    }
                //非查詢模式，且沒有發生錯誤
                case "nonquery":
                    editorSQLHistory.Text = editorSQLHistory.Text.Replace(MyGlobal.sSeparator3, "\r\n--Status: \"" + _sQueryStatus + "\", " + lblExecTime.Text + ", " + editorMessage.Text.Replace("\r\n", "\r\n--"));
                    sMessage = editorMessage.Text;
                    break;
                default:
                    editorSQLHistory.Text = editorSQLHistory.Text.Replace(MyGlobal.sSeparator3, "\r\n--Status: \"" + _sQueryStatus + "\", " + lblExecTime.Text + ", Total Records: " + c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count.ToString("N0") + " row(s)");
                    break;
            }

            editorSQLHistory.ReadOnly = true;

            //Begin:更新 SQL History
            sTemp = sResult == "Error" ? _sSqlWhenError : editor.SelectedText;

            if (string.IsNullOrEmpty(sTemp))
            {
                sTemp = editor.Text;
            }

            if (!string.IsNullOrEmpty(MyGlobal.sExecuteNonQuerySQLHistoryScript))
            {
                //20201124 不在此處寫入 JasonQuery.db，因為效率太差了 (改在執行當下逐筆寫入)
                MyGlobal.sExecuteNonQuerySQLHistoryScript = "";
            }
            else
            {
                string sNewMessage;

                if (sResult == "Error")
                {
                    sNewMessage = sMessage.Replace("'", "''");
                }
                else
                {
                    sNewMessage = string.IsNullOrEmpty(sMessage) ? _sQueryTextParametersMapping.Replace("'", "''") : sMessage.Replace("'", "''") + (string.IsNullOrEmpty(_sQueryTextParametersMapping) ? "" : "\r\n\r\n" + _sQueryTextParametersMapping.Replace("'", "''"));
                }

                UpdateSqlHistory(c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count, sResult, sNewMessage, sTemp.Replace("'", "''"));
            }
            //End:更新 SQL History

            _sQueryStatus = "";

            //控制按鈕程式碼，要寫在後面
            var bValue = c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count != 0;

            cboFindGrid.Enabled = bValue;
            btnExportToFile.Enabled = bValue;
            btnShowSQL.Enabled = bValue;
            btnFreezeColumn.Enabled = bValue;
            btnAutoSize.Enabled = bValue && (chkRawDataMode.Checked || !chkSize.Checked);
            btnAutoSort.Enabled = !chkSort.Checked && bValue;

            //傳訊息至母表單
            //uTransferValueToMainForm("p`Processing OK!"); //Total Records: " + c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count.ToString());

            if (editorMessage.Tag.ToString() == "nonquery" || editorMessage.Tag.ToString() == "cancel")
            {
                c1DockingTab1.SelectedTab = tabMessage;
            }
            else if (btnCancelQuery.Tag.ToString() == "Cancel")
            {
                switch (MyGlobal.sDataSource)
                {
                    case "Oracle":
                    case "PostgreSQL":
                    case "SQL Server":
                    case "MySQL":
                    case "SQLite":
                    case "SQLCipher":
                        UpdateMessage("canceling statement due to user request");
                        break;
                }

                c1TrueDBGrid1.DataSource = _dtNull;
                c1DockingTab1.SelectedTab = tabMessage;
            }
            else if (editorMessage.Tag.ToString() != "error")
            {
                //如果查無任何資料，判斷是否要顯示 Schema Info
                if (((DataTable)c1TrueDBGrid1.DataSource).Rows.Count == 0) // && MyGlobal.dtSchemaTable != null)
                {
                    //查無資料，還是要移到 DataGrid 頁籤
                    c1DockingTab1.SelectedTab = tabDataGrid;

                    foreach (C1DisplayColumn col in c1TrueDBGrid1.Splits[_SplitsNum].DisplayColumns)
                    {
                        try
                        {
                            col.AutoSize();
                        }
                        catch (Exception)
                        {
                            col.Width = 2000;
                        }

                        if ("`500`1000`1500`2000`".Contains("`" + MyGlobal.sGridMaxWidth + "`") != true)
                        {
                            continue;
                        }

                        if (col.Width > Convert.ToInt16(MyGlobal.sGridMaxWidth))
                        {
                            col.Width = Convert.ToInt16(MyGlobal.sGridMaxWidth);
                        }
                    }

                    c1TrueDBGrid1.Refresh();
                }

                MyGlobal.SetGridVisualStyle(c1TrueDBGrid1, Convert.ToSingle(MyLibrary.sGridFontSize));

                if (MyGlobal.bChangeColorThemeNeedRestart)
                {
                    if (_fontSize == 0)
                    {
                        _fontSize = 10;
                    }

                    c1TrueDBGrid1.Styles["Normal"].Font = new Font(MyLibrary.sGridFontName, _fontSize * 1);
                }

                if (string.IsNullOrEmpty(btnNextPage.Tag.ToString()))
                {
                    btnNextPage.Tag = "0";
                }

                btnNextPage.Tag = (Convert.ToInt16(btnNextPage.Tag.ToString()) + Convert.ToInt16(btnPaginationOn.Tag.ToString())).ToString();

            }

            if (string.IsNullOrEmpty(editorMessage.Tag.ToString()) || editorMessage.Tag.ToString() != @"error")
            {
                switch (btnQuery.AccessibleDescription)
                {
                    case "query" when btnCommit.Enabled == false:
                        //單純的查詢、且沒有錯誤、且不需要 Commit / Rollback，全部「中斷連線」
                        //傳遞資訊至 MainForm，每個 QueryForm 全部中斷連線
                        TransferValueToMainForm("disconnectafterqueryonly`"); //timerMother2child_Tick
                        break;
                    case "nonquery":
                        //傳遞資訊至 MainForm，更新每個 QueryForm 的 Commit/Rollbak 按鈕狀態
                        TransferValueToMainForm("updatecommitrollbackbutton`"); //timerMother2child_Tick
                        break;
                }
            }
            else if (editorMessage.Tag.ToString() == "error" && MyGlobal.sDataSource == "PostgreSQL") //&& MyGlobal.bDBAutoRollback)
            {
                //PostgreSQL 執行有錯誤：傳遞資訊至 MainForm，每個 QueryForm 全部中斷連線
                //20220804 PostgreSQL 如果使用者有設定「Auto Rollback on error」，才需要中斷連線！
                if (MyGlobal.bDBAutoRollback)
                {
                    TransferValueToMainForm("disconnectafterexecuteerror`"); //timerMother2child_Tick
                }
            }

            _bBusy = false;

            btnQuery.Enabled = true;
            btnExecuteCurrentBlock.Enabled = true;
            btnCancelQuery.Enabled = false;

            if (_bNextPageQuery)
            {
                c1TrueDBGrid1.ScrollGrid(0, c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count);
                _iLastRowOffset = c1TrueDBGrid1.Splits[_SplitsNum].VerticalOffset;

                c1TrueDBGrid1.Row = _iNextPageRow;
                c1TrueDBGrid1.Col = _iNextPageCol;
                c1TrueDBGrid1.Select();

                //記錄目前 scroll bar 的位置
                c1TrueDBGrid1.ScrollGrid(0, _iNextPageRow);
                _iLastTimeOffset = c1TrueDBGrid1.Splits[_SplitsNum].VerticalOffset;

                //重新再移動一次
                c1TrueDBGrid1.Row = _iNextPageRow;
                c1TrueDBGrid1.Col = _iNextPageCol;
                c1TrueDBGrid1.Select();

                Application.UseWaitCursor = false;
                c1DockingTab1.Enabled = true;
            }
            else
            {
                c1TrueDBGrid1.ScrollGrid(0, c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count);
                _iLastRowOffset = c1TrueDBGrid1.Splits[_SplitsNum].VerticalOffset;
                c1TrueDBGrid1.Row = 0;
                c1TrueDBGrid1.Col = 0;
                c1TrueDBGrid1.Select();
            }

            //20200622 不在此處控制
            //btnCommit.Enabled = true;
            //btnRollback.Enabled = true;

            if (MyLibrary.bGridSetFocusAfterQuery == false && _bNextPageQuery == false)
            {
                editor.Focus();
            }
            else
            {
                c1TrueDBGrid1.Focus(); //20201210
            }

            //20210601 查詢結束後，移動一下滑鼠，讓滑鼠游標不再是 busy
            SetCursorPos(Cursor.Position.X - 1, Cursor.Position.Y - 1);
            SetCursorPos(Cursor.Position.X + 1, Cursor.Position.Y + 1);
        }

        private void UpdateSqlHistory(int iRows, string sResult, string sMessage, string sSQL)
        {
            var sExecTime = (lblExecTime.Tag ?? string.Empty).ToString();
            var sQueryTime = (lblQueryTime.Tag ?? string.Empty).ToString();
            DBCommon.ExecNonQuery("Insert Into SQLHistory (MPID, ExecutionDate, ExecutionTime, QueryTime, Rows, Result, Message, SQL) Values (" + MyGlobal.sDBMotherPID + ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', '" + sExecTime + "', '" + sQueryTime + "', " + iRows + ", '" + sResult + "', '" + sMessage + "', '" + sSQL + "')");
        }

        private static void LoadFindList(string sFun, C1ComboBox objComboBox)
        {
            var i = 0;
            objComboBox.Items.Clear();

            try
            {
                var dtRecent = DBCommon.ExecQuery("Select AttributeValue From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='FindList_" + sFun + "' Order By AttributeDate Desc");

                if (dtRecent.Rows.Count <= 0)
                {
                    return;
                }

                for (var iRow = 0; iRow < dtRecent.Rows.Count; iRow++)
                {
                    if (i > 20)
                    {
                        break;
                    }

                    objComboBox.Items.Add(dtRecent.Rows[iRow]["AttributeValue"].ToString());

                    i++;
                }
            }
            catch (Exception)
            {
                //throw
            }
        }

        private void SaveFindList(string sFun, string sFindText)
        {
            //Save
            var dtRecent = DBCommon.ExecQuery("Select * From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='FindList_" + sFun + "' And AttributeValue='" + sFindText.Replace("'", "''") + "'");

            if (dtRecent.Rows.Count > 0)
            {
                DBCommon.ExecNonQuery("Update SystemConfig Set AttributeDate='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='FindList_" + sFun + "' And AttributeValue='" + sFindText.Replace("'", "''") + "'");
            }
            else
            {
                DBCommon.ExecNonQuery("Insert Into SystemConfig (DomainUser, MPID, AttributeKey, AttributeValue, AttributeDate) Values ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'FindList_" + sFun + "', '" + sFindText.Replace("'", "''") + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')");
            }

            //Reload
            //LoadFindList(sFun, cboFind); //SaveFindList
            //LoadFindList(sFun, cboFindBox); //SaveFindList
            LoadFindList(sFun, cboFindGrid); //SaveFindList
        }

        private void LoadReplaceList()
        {
            //var i = 0;

            //cboReplaceBox.Items.Clear();

            //try
            //{
            //    var dtReplace = DBCommon.ExecQuery("Select AttributeValue From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='ReplaceList_Editor" + "' Order By AttributeDate Desc");

            //    if (dtReplace.Rows.Count <= 0)
            //    {
            //        return;
            //    }

            //    for (var iRow = 0; iRow < dtReplace.Rows.Count; iRow++)
            //    {
            //        if (i > 20)
            //        {
            //            break;
            //        }

            //        cboReplaceBox.Items.Add(dtReplace.Rows[iRow]["AttributeValue"].ToString());

            //        i++;
            //    }
            //}
            //catch (Exception)
            //{
            //    //throw
            //}
        }

        private void SaveReplaceList(string sFun, string sReplaceText)
        {
            //Save
            var dtRecent = DBCommon.ExecQuery("Select * From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='ReplaceList_" + sFun + "' And AttributeValue='" + sReplaceText.Replace("'", "''") + "'");

            if (dtRecent.Rows.Count > 0)
            {
                DBCommon.ExecNonQuery("Update SystemConfig Set AttributeDate='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='ReplaceList_" + sFun + "' And AttributeValue='" + sReplaceText.Replace("'", "''") + "'");
            }
            else
            {
                DBCommon.ExecNonQuery("Insert Into SystemConfig (DomainUser, MPID, AttributeKey, AttributeValue, AttributeDate) Values ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'ReplaceList_" + sFun + "', '" + sReplaceText.Replace("'", "''") + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')");
            }

            //Reload
            LoadReplaceList(); //SaveReplaceList
        }

        private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            _bNeedToSaveSplitter = true; //使用者手動調整，才要儲存 (此處 Form Size 變動，並不會觸發 SplitterMoving 事件)
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!_bNeedToSaveSplitter)
            {
                return;
            }

            SaveSplitterData("U/D", splitContainer1.SplitterDistance);
            _bNeedToSaveSplitter = false;
            editor.Focus();
        }

        private void splitContainer2_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            _bNeedToSaveSplitter = true; //使用者手動調整，才要儲存 (此處 Form Size 變動，並不會觸發 SplitterMoving 事件)
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            HideACGrid();

            if (!_bNeedToSaveSplitter)
            {
                return;
            }

            AutoResizeGridColumnWidth(); //splitContainer2_SplitterMoved
            SaveSplitterData("L/R", splitContainer2.SplitterDistance);
            _bNeedToSaveSplitter = false;
            editor.Focus();
        }

        private void LoadSplitterData(string sSplitterKey)
        {
            try
            {
                var dtSplitterData = DBCommon.ExecQuery("Select AttributeValue From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='SplitterConfig' And AttributeName='QueryEditor_" + sSplitterKey + "'");

                if (dtSplitterData.Rows.Count <= 0)
                {
                    return;
                }

                switch (sSplitterKey)
                {
                    case "L/R":
                        splitContainer2.SplitterDistance = Convert.ToInt32(dtSplitterData.Rows[0]["AttributeValue"].ToString());
                        break;
                    case "U/D":
                        splitContainer1.SplitterDistance = Convert.ToInt32(dtSplitterData.Rows[0]["AttributeValue"].ToString());
                        break;
                }
            }
            catch (Exception)
            {
                //throw
            }
        }

        private void SaveSplitterData(string sSplitterKey, int iSplitterValue) //sSplitterKey="U/D" or "L/R"
        {
            if (sSplitterKey != "U/D" && sSplitterKey != "L/R")
            {
                return;
            }

            //Save
            var dtSplitterData = DBCommon.ExecQuery("Select * From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='SplitterConfig' And AttributeName='QueryEditor_" + sSplitterKey + "'");

            if (dtSplitterData.Rows.Count > 0)
            {
                DBCommon.ExecNonQuery("Update SystemConfig Set AttributeValue='" + iSplitterValue + "' Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='SplitterConfig' And AttributeName='QueryEditor_" + sSplitterKey + "'");
            }
            else
            {
                DBCommon.ExecNonQuery("Insert Into SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) Values ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'SplitterConfig', 'QueryEditor_" + sSplitterKey + "', '" + iSplitterValue + "')");
            }
        }

        private void btnIndent_Click(object sender, EventArgs e)
        {
            Indent(); //按鈕
        }

        private void btnUnIndent_Click(object sender, EventArgs e)
        {
            Unindent(); //按鈕
        }

        private void Indent(int iShift = 1) //加上 -- 註解
        {
            var sAllText = editor.Text;
            var sSelectedText = editor.SelectedText;
            var iIndentCount = 0;
            var iNoneSpaceStart = 0;
            var iIndent = Convert.ToInt32(txtIndentWord.Text);

            //取得使用者選取的範圍之起始、結束位置
            var iStart = editor.SelectionStart;
            var iEnd = editor.SelectionEnd;

            var iStart2 = 0;
            var iEnd2 = 0;

            //根據使用者的選取範圍，重新取得第一列的最前、最後一列的最後位置
            GetNewStartAndEnd(sAllText, iStart, iEnd, ref iStart2, ref iEnd2, ref iNoneSpaceStart); //Indent

            //如果最後一個位置是換行符號，往前一個位置
            if (editor.Text.Substring(iEnd2, 1) == "\r")
            {
                iEnd2--;
            }

            //將選取範圍重新定義為「第一列的最前，一直到最後一列的最後」
            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2;

            //20201211 重新選取文字
            sSelectedText = editor.SelectedText;

            var parts = sSelectedText.Split(new[] { "\r\n" }, StringSplitOptions.None);

            var sResult = "";

            for (var i = 0; i < parts.Length; i++)
            {
                var sTemp = "";

                for (var j = 0; j < iIndent; j++)
                {
                    sTemp += " "; //第一個非空白，不是 dash 符號，不處理
                    iIndentCount++;
                }

                if (!string.IsNullOrEmpty(parts[i])) //如果該列是完全空的，就不在最前面加上空白了！
                {
                    sResult = sResult + sTemp + parts[i];
                }

                if (i < parts.Length - 1) //不是選取區的最後一列，後面加上換行符號
                {
                    sResult += "\r\n";
                }
            }

            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2;

            editor.ReplaceSelection(sResult);

            sAllText = editor.Text;

            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2 + iIndentCount;

            //加上 Indent 空白符號後，重新取得「選取範圍」
            GetNewStartAndEnd(sAllText, iStart2, iEnd2 + iIndentCount, ref iStart2, ref iEnd2, ref iNoneSpaceStart); //Indent

            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2 + iShift; //此處要再加 1？不加 1，有時會少選 1；加 1，多按幾次 Un-Indent，就會多選到 \r 了。(註一)

            //以下的 Select() + SendKeys(按下組合鍵Shift END)，上面的(註一)問題就可以解決了
            editor.Select();

            if (iShift == 1)
            {
                SendKeys.Send("+{END}");
            }
        }

        private void Unindent(int iShift = 1) //去除 -- 註解
        {
            var sAllText = editor.Text;
            var sSelectedText = editor.SelectedText;
            var iUnindentCount = 0;
            var iNoneSpaceStart = 0;
            var iIndent = Convert.ToInt32(txtIndentWord.Text);

            //取得使用者選取的範圍之起始、結束位置
            var iStart = editor.SelectionStart;
            var iEnd = editor.SelectionEnd;

            var iStart2 = 0;
            var iEnd2 = 0;

            //根據使用者的選取範圍，重新取得第一列的最前、最後一列的最後位置
            GetNewStartAndEnd(sAllText, iStart, iEnd, ref iStart2, ref iEnd2, ref iNoneSpaceStart); //Unindent

            //將選取範圍重新定義為「第一列的最前，一直到最後一列的最後」
            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2;

            //20201211 重新選取文字
            sSelectedText = editor.SelectedText;

            var parts = sSelectedText.Split(new[] { "\r\n" }, StringSplitOptions.None);

            var sResult = "";

            for (var i = 0; i < parts.Length; i++)
            {
                var sTemp = parts[i];

                if (!string.IsNullOrEmpty(sTemp))
                {
                    for (var j = 0; j < iIndent; j++)
                    {
                        if (sTemp.Substring(0, 1) != " ")
                        {
                            continue;
                        }

                        sTemp = sTemp.Substring(1, sTemp.Length - 1);
                        iUnindentCount++;
                    }
                }

                sResult += sTemp;

                if (i < parts.Length - 1) //不是選取區的最後一列，後面加上換行符號
                {
                    sResult += "\r\n";
                }
            }

            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2;

            editor.ReplaceSelection(sResult);

            sAllText = editor.Text;

            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2 - iUnindentCount;

            //加上 Indent 空白符號後，重新取得「選取範圍」
            GetNewStartAndEnd(sAllText, iStart2, iEnd2 - iUnindentCount, ref iStart2, ref iEnd2, ref iNoneSpaceStart); //Unindent

            editor.SelectionStart = iStart2;
            editor.SelectionEnd = iEnd2 + iShift; //此處要再加 1？不加 1，有時會少選 1；加 1，多按幾次 Indent，就會多選到 \r 了。(註一)

            //以下的 Select() + SendKeys(按下組合鍵Shift END)，上面的(註一)問題就可以解決了
            editor.Select();

            if (iShift == 1)
            {
                SendKeys.Send("+{END}");
            }
        }

        private void btnSelectCurrentBlock_Click(object sender, EventArgs e)
        {
            SelectBlock(); //btnSelectCurrentBlock_Click
        }

        private string SelectBlock(bool bSelect = true)
        {
            var sAllText = editor.Text;
            var sSelectedText = editor.SelectedText;
            var iNoneSpaceStart = 0; //iNoneSpaceStart: 第一個非空白字元的位置 (以平均來看，不是以第一列來看)

            if (string.IsNullOrEmpty(sAllText))
            {
                return "";
            }

            //如果是全選，改為以 SelectionStart 重新判斷 Block 區段
            if (sAllText == sSelectedText)
            {
                editor.SelectionEnd = editor.SelectionStart;
            }

            //取得使用者選取的範圍之起始、結束位置
            var iStart = editor.SelectionStart;
            var iEnd = editor.SelectionEnd;

            var iStart2 = 0;
            var iEnd2 = 0;

            //如果有啟用 HighlightSelection，停用 editor.MultipleSelection，避免選取文字包含 MultipleSelection
            //if (btnHighlightSelection.Visible)
            //{
            //    editor.MultipleSelection = false;
            //}

            //根據使用者的選取範圍，重新取得第一列的最前、最後一列的最後位置
            if (MyLibrary.bEntireBlankRowAsEmptyRow == false)
            {
                GetNewStartAndEnd(sAllText, iStart, iEnd, ref iStart2, ref iEnd2, ref iNoneSpaceStart, "selectblock"); //SelectBlock
            }
            else
            {
                GetNewStartAndEnd_EntireBlankRowAsEmptyRow(iStart, iEnd, ref iStart2, ref iEnd2);
            }

            if (!bSelect) return editor.GetTextRange(iStart2, iEnd2 - iStart2);

            //將選取範圍重新定義為「第一列的最前，一直到最後一列的最後」
            editor.SelectionStart = iEnd2;
            //editor.SelectionEnd = iStart2;
            editor.CurrentPosition = iStart2;
            editor.ScrollCaret();

            return "";

            //////以下寫法會有問題：游標會出現在最後面(NG)，就算把 iStart2/iEnd2 交換也不行
            ////editor.SelectionStart = iStart2;
            ////editor.SelectionEnd = iEnd2;
        }

        private void GetNewStartAndEnd_EntireBlankRowAsEmptyRow(int iStart, int iEnd, ref int iStart2, ref int iEnd2)
        {
            var iLineStart = 0;
            var iLineEnd = 0;
            var iPos = 0;
            var sLine = "";
            var sText = editor.Text;
            var parts = sText.Split(new[] { "\r\n" }, StringSplitOptions.None); //StringSplitOptions.RemoveEmptyEntries

            var iCurrentLine = editor.CurrentLine;

            for (var i = iCurrentLine; i > 0; i--)
            {
                sLine = parts[i];

                if (!string.IsNullOrEmpty(sLine.Trim()) || i == iCurrentLine)
                {
                    continue; //如果整列都是空白
                }

                iLineStart = i + 1;
                break;
            }

            if (iCurrentLine == parts.Length - 1 || string.IsNullOrEmpty(parts[iCurrentLine]) && iCurrentLine + 1 <= parts.Length - 1 && string.IsNullOrEmpty(parts[iCurrentLine + 1]))
            {
                //【CurrentLine 位於最後一列】，【或是 CurrentLine 為空白列，且 CurrentLine 下一列也是空白列】
                iLineEnd = iCurrentLine;
            }
            else
            {
                for (var i = iCurrentLine; i < parts.Length; i++)
                {
                    sLine = parts[i];

                    if (string.IsNullOrEmpty(sLine.Trim()) && i != iCurrentLine) //如果整列都是空白
                    {
                        iLineEnd = i - 1;
                        break;
                    }

                    if (string.IsNullOrEmpty(sLine.Trim()) && i == iCurrentLine)
                    {
                        iLineEnd = i;
                        break;
                    }

                    iLineEnd = i;
                }

                if (iLineStart == 0 && iLineEnd == 0)
                {
                    iLineEnd = 0;
                }
                else if (iLineStart >= 0 && iLineEnd == 0)
                {
                    iLineEnd = parts.Length - 1;
                }
            }

            for (var i = 0; i < parts.Length; i++)
            {
                iPos += parts[i].Length + 2;

                if (i == iLineStart - 1)
                {
                    iStart2 = iPos;
                }
                else if (i == iLineEnd)
                {
                    iEnd2 = iPos - 2;
                }
            }
        }

        private void editor_DoubleClick(object sender, ScintillaNET.DoubleClickEventArgs e)
        {
            HighlightSelection(true); //editor_DoubleClick
        }

        private void editor_UpdateUI(object sender, ScintillaNET.UpdateUIEventArgs e)
        {
            UpdateEditorStatusBarLanguage();

            if (e.Change == ScintillaNET.UpdateChange.VScroll || e.Change == ScintillaNET.UpdateChange.HScroll)
            {
                //20220805 移動 ScrollBar，隱藏 AC Grid
                HideACGrid();
            }
        }

        private void UpdateEditorStatusBarLanguage()
        {
            var iSel = 0;
            var lines = editor.Text.Split(new[] { "\r\n" }, StringSplitOptions.None);

            if (editor.SelectionStart != editor.SelectionEnd)
            {
                iSel = editor.SelectionEnd - editor.SelectionStart;
            }

            lblEditorLines.Text = _sEditorLines + @" " + lines.Length.ToString("###,##0");
            lblEditorLength.Text = _sEditorLength + @" " + editor.TextLength.ToString("###,##0");
            lblEditorLn.Text = _sEditorLn + @" " + (editor.CurrentLine + 1).ToString("###,##0");
            lblEditorCol.Text = _sEditorCol + @" " + (editor.GetColumn(editor.CurrentPosition) + 1).ToString("###,##0");
            lblEditorPos.Text = _sEditorPos + @" " + (editor.CurrentPosition + 1).ToString("###,##0");
            lblEditorSel.Text = _sEditorSel + @" " + iSel.ToString("###,##0");
            //MultipleSelections(); //此處針對「方向鍵」處理有BUG，會影響「使用者輸入」，因為游標會亂跑
        }

        private void CreateNullTable()
        {
            _sLangText = "data"; //依資料庫不同而變化?
            _dtNull.Columns.Add(_sLangText);
        }

        private static void MoveDialogThread(object sender, DoWorkEventArgs e)
        {
            const string dialogWindowClass = "#32770";

            var windowCaption = (string)((object[])e.Argument)[0];
            var location = (Point)((object[])e.Argument)[1];

            const int maxAttempts = 400;

            for (var i = 0; i < maxAttempts; ++i)
            {
                var handle = Win32Api.FindWindow(dialogWindowClass, windowCaption);

                if ((int)handle > 0 && Win32Api.IsWindowVisible(handle) > 0)
                {
                    // move it
                    Win32Api.SetWindowPos(handle, (IntPtr)0, location.X, location.Y, 0, 0, Win32Api.SWP_NOSIZE | Win32Api.SWP_NOZORDER);
                    break;
                }

                Thread.Sleep(10);
            }
        }

        private void ApplySqlStyler()
        {
            SqlStyler.sColorEditorBackground = MyLibrary.sColorEditorBackground;
            SqlStyler.sColorTextIdentifier = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorComments = MyLibrary.sColorComments;
            SqlStyler.sColorNumber = MyLibrary.sColorNumber;
            SqlStyler.sColorString = MyLibrary.sColorString;
            SqlStyler.sColorCharacter = MyLibrary.sColorCharacter;
            SqlStyler.sColorOperatorSymbol = MyLibrary.sColorOperatorSymbol;
            SqlStyler.sColorUserDefinedTablesViews = MyLibrary.sColorUserDefinedTablesViews;
            SqlStyler.sColorUserDefinedFunctionsTirggers = MyLibrary.sColorUserDefinedFunctionsTirggers;
            SqlStyler.sColorOperatorKeywords = MyLibrary.sColorOperatorKeywords;
            SqlStyler.sColorBuiltInFunctions = MyLibrary.sColorBuiltInFunctions;
            SqlStyler.sColorBuiltInKeywords = MyLibrary.sColorBuiltInKeywords;
            SqlStyler.sColorUserDefinedKeywords = MyLibrary.sColorUserDefinedKeywords;
            SqlStyler.bKeywordFontBold = MyLibrary.bKeywordFontBold;

            SqlStyler.sKeywordsUserDefinedTables = MyLibrary.sKeywordsUserDefinedTables;
            SqlStyler.sKeywordsUserDefinedViews = MyLibrary.sKeywordsUserDefinedViews;
            SqlStyler.sKeywordsUserDefinedFunctions = MyLibrary.sKeywordsUserDefinedFunctions;
            SqlStyler.sKeywordsUserDefinedTriggers = MyLibrary.sKeywordsUserDefinedTriggers;
            SqlStyler.sKeywordsOperatorKeywords = MyLibrary.sKeywordsOperatorKeywords;
            SqlStyler.sKeywordsBuiltInFunctions = MyLibrary.sKeywordsBuiltInFunctions;
            SqlStyler.sKeywordsBuiltInKeywords = MyLibrary.sKeywordsBuiltInKeywords;
            SqlStyler.sKeywordsUserDefinedKeywords = MyLibrary.sKeywordsUserDefinedKeywords;

            editor.Styler = new SqlStyler(); //變更關鍵字、顏色
            editorSQLHistory.Styler = new SqlStyler(); //變更關鍵字、顏色
            editorMessage.Styler = new SqlStyler(); //變更關鍵字、顏色
        }

        private static string GetStringFromUnicode(string sCode) //GetStringFromUnicode
        {
            var bytes = new byte[2];

            bytes[1] = Convert.ToByte(sCode.Substring(0, 2), 16);
            bytes[0] = Convert.ToByte(sCode.Substring(2, sCode.Length - 2), 16);

            return Encoding.Unicode.GetString(bytes);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var bHandled = false;
            var bValue = true;

            switch (keyData)
            {
                case Keys.F12:
                    SaveAs();
                    bHandled = true;
                    break;

                case Keys.Control | Keys.Space:
                    if (editor.Focused)
                    {
                        bHandled = true;
                    }

                    break;

                case Keys.F5: //偵測 F5 按鍵 (放在 frmQuery.cs)，可以取得每個 TabControl 各自的 editor.Text 的值！
                    btnQuery.PerformClick();
                    bHandled = true;
                    break;

                case Keys.Control | Keys.S:
                    Save();
                    bHandled = true;
                    break;

                case Keys.Control | Keys.Home:
                    if (c1TrueDBGrid1.Focused)
                    {
                        c1TrueDBGrid1.Row = 0;
                        //c1TrueDBGrid1.Col = 0;
                        c1TrueDBGrid1.Select(); //Focus 切換到指定的 Cell
                        bHandled = true;
                    }

                    break;

                case Keys.Control | Keys.End:
                    if (c1TrueDBGrid1.Focused)
                    {
                        c1TrueDBGrid1.Row = c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count - 1;
                        c1TrueDBGrid1.Select(); //Focus 切換到指定的 Cell

                        if (btnNextPage.Enabled)
                        {
                            NextPage();
                        }

                        _ctrlKeyDown = false; //20201210 強制改為 false，因為  Ctrl+End 組合鍵，並不會觸發 _ctrlKeyDown 變成 false (不改為 false，滾動捲軸時會變成 Grid 放大、縮小)
                        bHandled = true; //此處必須為 true，因為不要觸發 Grid 移至最底
                    }

                    break;

                case Keys.Down:
                    if (c1TrueDBGrid1.Focused)
                    {
                        if (btnNextPage.Enabled && c1TrueDBGrid1.Row == c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count - 1)
                        {
                            NextPage(c1TrueDBGrid1.Col, c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count - 1);
                            bHandled = true;
                        }
                    }

                    break;

                case Keys.PageDown:
                    if (c1TrueDBGrid1.Focused)
                    {
                        if (btnNextPage.Enabled && c1TrueDBGrid1.Row == c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count - 1)
                        {
                            NextPage(c1TrueDBGrid1.Col, c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count - 1);
                            bHandled = true;
                        }
                    }

                    break;

                case Keys.Control | Keys.X: //Ctrl+X
                    if (editor.Focused)
                    {
                        if (!string.IsNullOrEmpty(editor.Tag.ToString()))
                        {
                            editor.Cut();
                            CopyTextToClipboard(editor.Tag.ToString(), "CtrlX");
                        }
                        else
                        {
                            editor.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                            editor.Clear();
                        }

                        CheckEditorContent(); //ProcessCmdKey, Ctrl+X
                    }

                    bHandled = bValue;
                    break;

                case Keys.Control | Keys.Alt | Keys.V: //Ctrl+Alt+V
                    if (c1GridSchemaBrowser.Focused) //20220415
                    {
                        ArrangeSchemaData4CopyPaste("Paste3"); //ProcessCmdKey
                    }

                    break;

                case Keys.Control | Keys.Shift | Keys.V: //Ctrl+Shift+V
                    if (c1GridSchemaBrowser.Focused) //20220415
                    {
                        ArrangeSchemaData4CopyPaste("Paste4"); //ProcessCmdKey
                    }

                    break;

                case Keys.Alt | Keys.Shift | Keys.V: //Alt+Shift+V
                    if (c1GridSchemaBrowser.Focused) //20220415
                    {
                        ArrangeSchemaData4CopyPaste("Paste5"); //ProcessCmdKey
                    }

                    break;

                case Keys.Control | Keys.Alt | Keys.Shift | Keys.V: //Ctrl+Alt+Shift+V
                    if (c1GridSchemaBrowser.Focused) //20220415
                    {
                        ArrangeSchemaData4CopyPaste("Paste6"); //ProcessCmdKey
                    }

                    break;

                case Keys.Control | Keys.C: //Ctrl+C
                    if (c1TrueDBGrid1.Focused)
                    {
                        ArrangeData("Copy"); //ProcessCmdKey
                    }
                    else if (c1GridSchemaBrowser.Focused)
                    {
                        //20220415
                        ArrangeSchemaData4CopyPaste("Copy"); //ProcessCmdKey
                    }
                    else if (MyLibrary.bCopyAsHTML && editorSQLHistory.Focused)
                    {
                        try
                        {
                            Clipboard.Clear();
                            editorSQLHistory.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                        }
                        catch (Exception ex)
                        {
                            _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                            MessageBox.Show(_sLangText + @"-c10", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        }
                    }
                    else if (MyLibrary.bCopyAsHTML && editor.Focused && string.IsNullOrEmpty(editor.Tag.ToString()))
                    {
                        try
                        {
                            Clipboard.Clear();

                            //20221102 判斷是否為「重複按下 Ctrl+C」
                            if (!string.IsNullOrEmpty(_sSelectedTextDoubleClick) && string.IsNullOrEmpty(editor.SelectedText.ToUpper().Replace(_sSelectedTextDoubleClick.ToUpper(), "")))
                            {
                                CopyTextToClipboard(_sSelectedTextDoubleClick, "031");
                            }
                            else
                            {
                                editor.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                            }
                        }
                        catch (Exception ex)
                        {
                            _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                            MessageBox.Show(_sLangText + @"-c12", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        }
                    }
                    else
                    {
                        if (editor.Focused)
                        {
                            if (!string.IsNullOrEmpty(editor.Tag.ToString()))
                            {
                                Clipboard.Clear();
                                CopyTextToClipboard(editor.Tag.ToString(), "03");
                            }
                            else
                            {
                                try
                                {
                                    //20221102 判斷是否為「重複按下 Ctrl+C」
                                    if (!string.IsNullOrEmpty(_sSelectedTextDoubleClick) && string.IsNullOrEmpty(editor.SelectedText.ToUpper().Replace(_sSelectedTextDoubleClick.ToUpper(), "")))
                                    {
                                        CopyTextToClipboard(_sSelectedTextDoubleClick, "032");
                                    }
                                    else
                                    {
                                        editor.Copy();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                                    MessageBox.Show(_sLangText + @"-c14", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    break;
                                }
                            }
                        }
                        else if (editorSQLHistory.Focused)
                        {
                            try
                            {
                                editorSQLHistory.Copy();
                            }
                            catch (Exception ex)
                            {
                                _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                                MessageBox.Show(_sLangText + @"-c15", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                break;
                            }
                        }
                        else
                        {
                            bValue = false;
                        }
                    }

                    bHandled = bValue;
                    break;

                case Keys.Control | Keys.A: //Ctrl+A
                    if (c1TrueDBGrid1.Focused)
                    {
                        //清除所有已選取的 Row
                        c1TrueDBGrid1.SelectedRows.Clear();

                        for (var i = 0; i < c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count; i++)
                        {
                            c1TrueDBGrid1.SelectedRows.Add(i);
                        }
                    }

                    break;

                case Keys.Control | Keys.B: //Ctrl+B
                    if (editor.Focused)
                    {
                        SelectBlock(); //ProcessCmdKey() Ctrl+B
                        bHandled = true;
                    }

                    break;

                case Keys.Control | Keys.V: //Ctrl+V
                    if (editor.Focused && editor.CanPaste)
                    {
                        try
                        {
                            var sOriginalText = Clipboard.GetText();
                            CopyTextToClipboard(sOriginalText.Replace("\t", "    "), "CtrlV");
                            editor.Paste();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                var sOriginalText = Clipboard.GetText();
                                CopyTextToClipboard(sOriginalText.Replace("\t", "    "), "CtrlV");
                                editor.Paste();
                            }
                            catch (Exception)
                            {
                                //
                            }
                        }

                        bHandled = true;
                    }

                    break;

                case Keys.Control | Keys.N: //Ctrl+N
                    TransferValueToMainForm("createnewtab`");
                    bHandled = true;
                    break;

                case Keys.Control | Keys.O: //Ctrl+O
                    //20191005 按快速鍵，改為「在新的頁籤開啟」
                    TransferValueToMainForm("createnewtab`OPENFILE");

                    bHandled = true;
                    break;

                case Keys.Control | Keys.Enter: //Ctrl+Enter
                    if (editor.Focused)
                    {
                        if (c1GridAC4Period1.Visible)
                        {
                            c1GridAC4Period1.Visible = false;
                        }

                        if (c1GridAC4Space1.Visible)
                        {
                            c1GridAC4Space1.Visible = false;
                        }

                        if (c1GridAC4All.Visible)
                        {
                            c1GridAC4All.Visible = false;
                        }

                        SelectBlock(); //ProcessCmdKey, Ctrl+Enter
                        btnQuery.PerformClick();
                        bHandled = true;
                    }

                    break;

                case Keys.Tab: //Tab
                    _bKeyPressTab = true;

                    if (MyGlobal.bAutoListMembers && editor.Focused)
                    {
                        if (c1GridAC4Period1.Visible)
                        {
                            _iPeriodPos2 = editor.CurrentPosition;
                            c1GridAC4Period_PasteColumnName(); //ProcessCmdKey, Tab
                            c1GridAC4Period1.Visible = false;

                            bHandled = true;
                        }
                        else if (c1GridAC4Space1.Visible)
                        {
                            _iSpacePos2 = editor.CurrentPosition;
                            c1GridAC4Space_PasteColumnName(); //ProcessCmdKey, Tab
                            c1GridAC4Space1.Visible = false;

                            bHandled = true;
                        }
                    }

                    if (bHandled == false && MyLibrary.bEnableAutoComplete && editor.Focused && c1GridAC4All.Visible)
                    {
                        _iAllPos2 = editor.CurrentPosition;
                        c1GridAC4All_PasteColumnName(); //ProcessCmdKey, Tab
                        c1GridAC4All.Visible = false;

                        bHandled = true;
                    }

                    break;

                case Keys.Control | Keys.J: //Ctrl+J
                    _bKeyPressCtrlJ = true;

                    if (MyGlobal.bAutoListMembers && editor.Focused && c1GridAC4Period1.Visible == false)
                    {
                        var iTemp = 0;
                        var iPeriodPos = 0;
                        var eText = editor.Text.Length == editor.CurrentPosition ? editor.Text + " " : editor.Text; //游標可能在最後的位置，加上一個空白，以避掉 Substring() 的錯誤

                        if (eText.Substring(editor.CurrentPosition, 1) == ".") //所在位置剛好是句點
                        {
                            iPeriodPos = editor.CurrentPosition + 1;
                            _iPeriodPos = editor.CurrentPosition + 1;
                            _iPeriodPos2 = _iPeriodPos;
                            iTemp++;
                        }
                        else //if (MyGlobal.IsEngAlphabetOrNumber(eText.Substring(editor.CurrentPosition, 1), "_")) //所在位置為字母或數字或底線，往前找出句點的位置
                        {
                            for (var i = editor.CurrentPosition; i > 5; i--)
                            {
                                //20220825 待確認：if (eText.Substring(i, 1) == " " && eText.Substring(i - 1, 1) == " ")
                                if (eText.Substring(i - 1, 1) == " ")
                                {
                                    break; //還沒找到句點，先找到空白，直接忽略
                                }
                                else if (eText.Substring(i, 1) == ".")
                                {
                                    //找到句點的位置
                                    iPeriodPos = i + 1;
                                    _iPeriodPos = i + 1;
                                    _iPeriodPos2 = _iPeriodPos;
                                    iTemp++;
                                    break;
                                }
                            }
                        }

                        if (iTemp > 0) //從句點往後找出搜尋關鍵字
                        {
                            iTemp = 0;

                            for (var i = _iPeriodPos + 1; i < eText.Length; i++)
                            {
                                if (!MyGlobal.IsEngAlphabetOrNumber(eText.Substring(i, 1), "_"))
                                {
                                    //20220728 判斷輸入的字元後面接著的符號
                                    if ("` `\r`=`>`<`!`-`+`*`/`".IndexOf("`" + eText.Substring(i, 1) + "`", StringComparison.Ordinal) == -1)
                                    {
                                        //20220825 後面可以是 = 或 > 或 < 或 ! 或 - 或 + 或 * 或 / (加減乘除運算類的)
                                        c1GridAC4Period1.Visible = false; //輸入了 Table name or View name 不該有的符號，直接隱藏
                                    }
                                    else
                                    {
                                        //允許是換行符號或是空白，表示後面接續的是其他 SQL
                                        iTemp = i;
                                    }

                                    break;
                                }

                                iTemp = i + 1;
                            }
                        }

                        if (iPeriodPos > 0)
                        {
                            HandleAC4Period(_iPeriodPos); //ProcessCmdKey, for Ctrl+J

                            if (iTemp > 0) //有找到「搜尋關鍵字」
                            {
                                var ss = editor.GetTextRange(_iPeriodPos, iTemp - _iPeriodPos); //搜尋關鍵字
                                var iRowCount = c1GridAC4Period_Filter("[C] LIKE '" + ss + "*'");
                                var iWidth = MyGlobal.ResizeGridColumnWidth(c1GridAC4Period1);
                                ResizeACGrid(c1GridAC4Period1, c1GridAC4Period2, iRowCount, iWidth); //ProcessCmdKey (Ctrl+J)
                            }

                            bHandled = true;
                        }
                    }

                    break;

                case Keys.Control | Keys.Shift | Keys.U: //Ctrl+Shift+U
                    _iCompoundKeyCtrlShift = 3;

                    if (editor.Focused)
                    {
                        TransferStringUpperLower(true); //Ctrl+Shift+U, ProcessCmdKey, 大寫
                        bHandled = true;
                    }

                    break;

                case Keys.Control | Keys.U: //Ctrl+U
                    if (editor.Focused)
                    {
                        TransferStringUpperLower(false); //Ctrl+U, ProcessCmdKey, 小寫
                        bHandled = true;
                    }

                    break;

                case Keys.Control | Keys.Y: //Ctrl+Y
                    if (editor.Focused && editor.CanRedo)
                    {
                        editor.Redo();
                        CheckEditorContent(); //Ctrl+Y, ProcessCmdKey
                        bHandled = true;
                    }

                    break;

                case Keys.Control | Keys.F: //Ctrl+F 尋找
                    HideACGrid(false); //顯示搜尋視窗時，強制隱藏 AC 清單
                    myFindReplace.ShowFind();
                    bHandled = true;
                    break;

                case Keys.Control | Keys.H: //Ctrl+H 取代
                    HideACGrid(false); //顯示搜尋視窗時，強制隱藏 AC 清單
                    myFindReplace.ShowReplace();
                    bHandled = true;
                    break;

                case Keys.F3:
                    if (editor.Focused || editorMessage.Focused || editorSQLHistory.Focused)
                    {
                        //myFindReplace.FindPrevious();
                        myFindReplace.Window.FindNext(true);
                        bHandled = true;
                    }
                    else if (c1TrueDBGrid1.Focused)
                    {
                        btnFindNextGrid.PerformClick();
                        bHandled = true;
                    }

                    break;

                case Keys.Shift | Keys.F3:
                    if (editor.Focused || editorMessage.Focused || editorSQLHistory.Focused)
                    {
                        myFindReplace.Window.FindNext(false);
                        bHandled = true;
                    }
                    else if (c1TrueDBGrid1.Focused)
                    {
                        btnFindPreviousGrid.PerformClick();
                        bHandled = true;
                    }

                    break;

                case Keys.Escape:
                    if (c1GridAC4Period1.Visible)
                    {
                        c1GridAC4Period1.Visible = false;
                        editor.CurrentPosition = _iPeriodPos;
                    }
                    else if (c1GridAC4Space1.Visible)
                    {
                        c1GridAC4Space1.Visible = false;
                        editor.CurrentPosition = _iSpacePos;
                    }
                    else if (c1GridAC4All.Visible)
                    {
                        c1GridAC4All.Visible = false;
                        editor.CurrentPosition = _iAllPos;
                    }

                    editor.Focus();
                    break;
            }

            return bHandled; //此處如果回傳 false，表示在其他的元件，此按鍵是會發生作用的
        }

        private void btnExecuteCurrentBlock_Click(object sender, EventArgs e)
        {
            SelectBlock(); //btnExecuteCurrentBlock_Click
            btnQuery.PerformClick();
        }

        private void btnWordWrap_Click(object sender, EventArgs e)
        {
            btnWordWrap.Visible = editor.WrapMode == ScintillaNET.WrapMode.Word;
            btnWordWrap2.Visible = !btnWordWrap.Visible;
            editor.WrapMode = editor.WrapMode == ScintillaNET.WrapMode.Word ? ScintillaNET.WrapMode.None : ScintillaNET.WrapMode.Word;
            editor.WrapVisualFlags = (MyLibrary.bWordWrapVisualFlags_Start ? ScintillaNET.WrapVisualFlags.Start : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_End ? ScintillaNET.WrapVisualFlags.End : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_Margin ? ScintillaNET.WrapVisualFlags.Margin : ScintillaNET.WrapVisualFlags.None);

            //加上以下這個指令，取消 Word Wrap 後，Focus 才不會跑到最底部！
            editor.ScrollCaret();
        }

        private void btnShowAllCharacters_Click(object sender, EventArgs e)
        {
            btnShowAllCharacters.Visible = editor.ViewEol;
            btnShowAllCharacters2.Visible = !btnShowAllCharacters.Visible;
            editor.ViewEol = !editor.ViewEol;
            editor.ViewWhitespace = (btnShowAllCharacters.Visible) ? ScintillaNET.WhitespaceMode.Invisible : ScintillaNET.WhitespaceMode.VisibleAlways;
        }

        private void btnShowIndentGuide_Click(object sender, EventArgs e)
        {
            btnShowIndentGuide.Visible = editor.IndentationGuides == ScintillaNET.IndentView.LookBoth;
            btnShowIndentGuide2.Visible = !btnShowIndentGuide.Visible;
            editor.IndentationGuides = editor.IndentationGuides == ScintillaNET.IndentView.LookBoth ? ScintillaNET.IndentView.None : ScintillaNET.IndentView.LookBoth;
        }

        private void HighlightSelection(bool bMouseClick = false)
        {
            var pos = editor.CurrentPosition;
            var wordStart = editor.WordStartPosition(pos, true);
            var wordEnd = editor.WordEndPosition(pos, true);
            var sUpperText = editor.Text.ToUpper();
            var word = editor.GetTextRange(wordStart, wordEnd - wordStart);

            var iMainSelection = 0;
            var bValue = true;
            //editor.AdditionalCaretsBlink = false; //選取的字串，最前面會不會閃爍
            //editor.AdditionalCaretForeColor = Color.Red; //選取的字串，最前面的 | 的顏色
            editor.AdditionalCaretsVisible = false; //選取的字串，最前面的 | 要不要顯示
            
            //包含換行符號？不用 multi-select！
            if (editor.SelectedText.Length - editor.SelectedText.Replace("\r", "").Length > 0 ||
                editor.SelectedText.Length - editor.SelectedText.Replace("\n", "").Length > 0)
            {
                bValue = false;
            }

            //包含空白？不用 multi-select！
            if (bValue && editor.SelectedText.Length - editor.SelectedText.Replace(" ", "").Length > 0)
            {
                bValue = false;
            }

            //for Mouse Click，如果沒有選取文字，需要重新處理 multi selection
            //20190320 此處還有bug 需要調整：左右鍵移動時，在單字移動，例如 word，游標在 d 按左鍵移動，此時不會選取 (忽略 bMouseClick，會造成左右鍵失效)
            if (string.IsNullOrEmpty(editor.SelectedText) && bMouseClick)
            {
                editor.Tag = "";
            }

            if (string.IsNullOrEmpty(word) || !bValue || editor.Tag.ToString() == word)
            {
                return;
            }

            editor.Tag = word;
            _sSelectedTextDoubleClick = word;

            var matches = Regex.Matches(sUpperText, word.ToUpper());

            editor.MultipleSelection = true;

            foreach (Match m in matches)
            {
                if (pos >= m.Index && pos - m.Index <= word.Length)
                {
                    //記住游標所在處的單字
                    iMainSelection = m.Index;
                }
                else
                {
                    editor.AddSelection(m.Index, m.Index + word.Length);
                }
            }

            //游標所在處的單字，最後再選取，游標就會停留在此單字的最前面
            editor.AddSelection(iMainSelection, iMainSelection + word.Length);
        }

        private static void FindMessageBox(string sTitle, bool bKill = false)
        {
            //依MessageBox的標題,找出MessageBox的視窗
            var ptr = FindWindow(null, sTitle);

            if (ptr == IntPtr.Zero)
            {
                return;
            }

            if (bKill)
            {
                //找到則關閉MessageBox視窗
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }

        //editor 允許從外部拖曳檔案，並直接開啟它
        //20190930 如果拖曳的檔案已被 JasonQuery 開啟了，會自動切換至該 Tab！
        private void InitDragDropFile()
        {
            editor.AllowDrop = true;

            editor.DragEnter += delegate (object sender, DragEventArgs e)
            {
                e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
            };

            editor.DragDrop += delegate (object sender, DragEventArgs e)
            {
                if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    return;
                }

                var a = (Array)e.Data.GetData(DataFormats.FileDrop);

                if (a == null)
                {
                    return;
                }

                for (var i = 0; i < a.Length; i++)
                {
                    OpenFile(false, a.GetValue(i).ToString());

                    var startTime = DateTime.Now;

                    while (true)
                    {
                        Application.DoEvents();

                        if (DateTime.Now.Subtract(startTime).Milliseconds >= 150)
                        {
                            break;
                        }

                        Application.DoEvents();
                    }
                }
            };
        }

        //public static bool FilePathHasInvalidChars(string path)
        //{
        //    //判斷檔名、路徑是否有不合法的字元
        //    return (!string.IsNullOrEmpty(path) && path.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0);
        //}

        private void btnCode2SQL_Click(object sender, EventArgs e)
        {
            var bValue = !string.IsNullOrEmpty(editor.SelectedText.Trim());

            mnuCSharp2SQL.Enabled = bValue;
            mnuVB2SQL.Enabled = bValue;
            mnuDephi2SQL.Enabled = bValue;
        }

        private void mnuCSharp2SQL_Click(object sender, EventArgs e)
        {
            if (editor.CanPaste)
            {
                CodeToSql("C#", "\"", "//");
            }
        }

        private void mnuVB2SQL_Click(object sender, EventArgs e)
        {
            if (editor.CanPaste)
            {
                CodeToSql("VB", "\"", "'");
            }
        }

        private void mnuDelphi2SQL_Click(object sender, EventArgs e)
        {
            if (editor.CanPaste)
            {
                CodeToSql("Delphi", "'", "//");
            }
        }

        private void CodeToSql(string sMode, string sKey, string sCommentKeyword)
        {
            var sResult = "";

            //來源：選取的字串 or 剪貼簿內容
            var sOriginalText = editor.SelectedText; //Clipboard.GetText();
            var parts = sOriginalText.Split(new[] { "\r\n" }, StringSplitOptions.None);

            for (var iPos = 0; iPos < parts.Length; iPos++)
            {
                var bCRLF = true;
                var sLine = parts[iPos];

                int iFrom;

                if (sLine.Trim().Length >= 2 && sLine.Trim().Substring(0, 2) == sCommentKeyword)
                {
                    sLine = "--" + sLine.Trim().Substring(2);
                }
                else
                {
                    int iCommentPos;
                    var sComment = "";

                    if (sLine.Length - sLine.Replace(sKey, "").Length >= 2) //至少有兩個指定符號：取中間的字串
                    {
                        iFrom = sLine.IndexOf(sKey, StringComparison.Ordinal) + sKey.Length;
                        var iTo = sLine.LastIndexOf(sKey, StringComparison.Ordinal);
                        iCommentPos = sLine.LastIndexOf(sCommentKeyword, StringComparison.Ordinal);

                        sComment = "";

                        if (iCommentPos > 0 && iTo < iCommentPos) //此列有註解
                        {
                            sComment = " --" + sLine.Substring(iCommentPos + sCommentKeyword.Length);
                        }

                        sLine = sLine.Substring(iFrom, iTo - iFrom) + sComment;
                    }
                    else if (sLine.Length - sLine.Replace(sKey, "").Length == 1) //只有一個指定符號：取第一個指定符號後面的字串
                    {
                        iFrom = sLine.IndexOf(sKey, StringComparison.Ordinal) + sKey.Length;
                        iCommentPos = sLine.LastIndexOf(sCommentKeyword, StringComparison.Ordinal);

                        sComment = "";

                        if (iCommentPos > 0 && iFrom < iCommentPos) //此列有註解
                        {
                            sComment = " --" + sLine.Substring(iCommentPos + sCommentKeyword.Length);
                        }

                        sLine = sLine.Substring(iFrom, sLine.Length - iFrom) + sComment;
                    }
                }

                if (!string.IsNullOrEmpty(sLine) && sMode == "Delphi" && sKey == "'")
                {
                    sLine = sLine.Replace("''", "'");
                }

                if (!string.IsNullOrEmpty(sLine) && "`C#`Delphi`".Contains("`" + sMode + "`") && sLine.Trim().Length > sCommentKeyword.Length && sLine.Trim().Substring(0, sCommentKeyword.Length) == sCommentKeyword)
                {
                    iFrom = sLine.IndexOf("//", StringComparison.Ordinal);
                    sLine = sLine.Substring(0, iFrom) + "--" + sLine.Substring(iFrom + sCommentKeyword.Length);
                }

                if (!string.IsNullOrEmpty(sLine) && sMode == "VB" && sLine.Trim().Length > sCommentKeyword.Length && sLine.Trim().Substring(0, sCommentKeyword.Length) == sCommentKeyword)
                {
                    iFrom = sLine.IndexOf("'", StringComparison.Ordinal);
                    sLine = sLine.Substring(0, iFrom) + "--" + sLine.Substring(iFrom + sCommentKeyword.Length);
                }

                if (sMode == "C#" && (sLine.Trim() == "{" || sLine.Trim() == "}" || sLine.Trim().StartsWith("if") || sLine.Trim().StartsWith("else")))
                {
                    bCRLF = false;
                    sLine = "";
                }

                sLine = sLine.TrimEnd();
                sResult += sLine;

                if (iPos < parts.Length - 1 && bCRLF) //不是選取區的最後一列，後面加上換行符號
                {
                    sResult += "\r\n";
                }
            }

            if (!string.IsNullOrEmpty(sResult))
            {
                sResult = sResult.Replace("\\r\\n", ""); //C#

                switch (sMode)
                {
                    case "C#":
                        sResult = sResult.Replace("\\\"", "\"");
                        break;
                    case "Delphi":
                        sResult = sResult.Replace("'#$D#$A'", "\r\n");
                        break;
                }
            }

            //判斷使用者的來源是「選取的字串」or「剪貼簿內容」
            try
            {
                sOriginalText = Clipboard.GetText();
                CopyTextToClipboard(sResult, "04");
                editor.Paste();
                CopyTextToClipboard(sOriginalText, "05");
            }
            catch (Exception ex)
            {
                _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Clipboard.Clear();
            }
        }

        private void btnSQL2Code_Click(object sender, EventArgs e)
        {
            var bValue = !string.IsNullOrEmpty(editor.SelectedText.Trim());

            mnuSQL2CSharp.Enabled = bValue;
            mnuSQL2VBNet.Enabled = bValue;
            mnuSQL2VB6A.Enabled = bValue;
            mnuSQL2Delphi.Enabled = bValue;
        }

        private void mnuSQLToCode_Click(object sender, EventArgs e)
        {
            var mnuItem = sender as ToolStripMenuItem;
            var sSqlStatement = editor.SelectedText;
            var sLanguage = mnuItem?.Tag.ToString().Split('`')[0];
            var sStyle = mnuItem?.Tag.ToString().Split('`')[1];

            MyLibrary.SQLToCode(sSqlStatement, MyLibrary.sSQLToCodeVariableName, sLanguage, sStyle);
        }

        private void btnHighlightSelection_Click(object sender, EventArgs e)
        {
            //
        }

        private void GridVisualStyle(C1TrueDBGrid c1Grid)
        {
            if (MyLibrary.bDarkMode == false)
            {
                switch (MyLibrary.sGridVisualStyle)
                {
                    case "Office 2007 Blue":
                        c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;
                        c1GridSchemaBrowser.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;
                        break;
                    case "Office 2007 Silver":
                        c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
                        c1GridSchemaBrowser.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
                        break;
                    case "Office 2007 Black":
                        c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Black;
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Black;
                        c1GridSchemaBrowser.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Black;
                        break;
                    case "Office 2010 Blue":
                        c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
                        c1GridSchemaBrowser.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
                        break;
                    case "Office 2010 Silver":
                        c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Silver;
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Silver;
                        c1GridSchemaBrowser.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Silver;
                        break;
                    case "Office 2010 Black":
                        c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Black;
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Black;
                        c1GridSchemaBrowser.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Black;
                        break;
                    default:
                        c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
                        c1GridSchemaBrowser.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
                        break;
                }
            }

            c1Grid.Splits[_SplitsNum].ColumnCaptionHeight = chkShowColumnType.Checked ? 45 : 25;
        }

        private void GridZoom(C1TrueDBGrid c1Grid)
        {
            float.TryParse(MyLibrary.sGridZoom, out var pcnt);

            if (_fontSize == 0)
            {
                _fontSize = 12;
            }

            c1Grid.RowHeight = (int)(_rowHeight * pcnt) + 5;

            if (chkShowColumnType.Checked)
            {
                c1Grid.Splits[_SplitsNum].ColumnCaptionHeight = (int)(_rowHeight * pcnt) * 2 + 11;
            }
            else
            {
                c1Grid.Splits[_SplitsNum].ColumnCaptionHeight = (int)(_rowHeight * pcnt) + 12;
            }

            c1Grid.RecordSelectorWidth = (int)(_recSelWidth * pcnt);

            c1Grid.Styles["Normal"].Font = new Font(c1Grid.Styles["Normal"].Font.FontFamily, _fontSize * pcnt);
        }

        private void GridFontAndBackgroundColor(C1TrueDBGrid c1Grid)
        {
            if (_fontSize == 0)
            {
                _fontSize = 12;
            }

            //字型 + 字體大小
            c1Grid.Font = new Font(MyLibrary.sGridFontName, _fontSize, FontStyle.Regular, GraphicsUnit.Point);
            c1Grid.HeadingStyle.Font = new Font(MyLibrary.sGridFontName, _fontSize, FontStyle.Regular, GraphicsUnit.Point);

            c1Grid.OddRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowForeColor);
            c1Grid.OddRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowBackColor);
            c1Grid.EvenRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowForeColor);
            c1Grid.EvenRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);

            //Grid's 選取顏色
            c1Grid.SelectedStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedForeColor);
            c1Grid.SelectedStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedBackColor);

            //AR: 字型 + 字體大小
            c1GridARInfo.Font = new Font(MyLibrary.sGridFontName, 10, FontStyle.Regular, GraphicsUnit.Point);
            c1GridARInfo.HeadingStyle.Font = new Font(MyLibrary.sGridFontName, 10, FontStyle.Regular, GraphicsUnit.Point);

            c1GridARInfo.OddRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowForeColor);
            c1GridARInfo.OddRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowBackColor);
            c1GridARInfo.EvenRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowForeColor);
            c1GridARInfo.EvenRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);

            //Grid's 選取顏色
            c1GridARInfo.SelectedStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedForeColor);
            c1GridARInfo.SelectedStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedBackColor);

            //Schema Browser: 字型 + 字體大小
            c1GridSchemaBrowser.Font = new Font(MyLibrary.sGridFontName, 10, FontStyle.Regular, GraphicsUnit.Point);
            c1GridSchemaBrowser.HeadingStyle.Font = new Font(MyLibrary.sGridFontName, 10, FontStyle.Regular, GraphicsUnit.Point);

            c1GridSchemaBrowser.SelectedStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedForeColor);
            c1GridSchemaBrowser.SelectedStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedBackColor);
        }

        private void cboFindGrid_Enter(object sender, EventArgs e)
        {
            SetGridToolStripBackColor(true); //cboFindGrid_Enter
        }

        private void cboFindGrid_Leave(object sender, EventArgs e)
        {
            SetGridToolStripBackColor(false); //cboFindGrid_Leave
        }

        private void cboFindGrid_KeyUp(object sender, KeyEventArgs e)
        {
            var bValue = !string.IsNullOrEmpty(cboFindGrid.Text.Trim());

            btnFindNextGrid.Enabled = bValue;
            btnFindPreviousGrid.Enabled = bValue;
            btnCountGrid.Enabled = bValue;
            btnHighlightAllGrid.Enabled = bValue;
            btnClearHighlightsGrid.Enabled = bValue;
        }

        private void cboFindGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13 || string.IsNullOrEmpty(cboFindGrid.Text.Trim()))
            {
                return;
            }

            UpdateFindGridList(); //cboFindGrid_KeyPress

            cboFindGrid.Tag = CountGrid().ToString(); //cboFindGrid_KeyPress 統計出現次數，但不顯示
            btnFindNextGrid.PerformClick();
        }

        private void cboFindGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboFindGrid.Text.Trim()))
            {
                return;
            }

            btnFindNextGrid.Enabled = true;
            btnFindPreviousGrid.Enabled = true;
            btnCountGrid.Enabled = true;
            btnHighlightAllGrid.Enabled = true;
            btnClearHighlightsGrid.Enabled = true;

            cboFindGrid.Tag = CountGrid().ToString(); //cboFindGrid_SelectedIndexChanged 統計出現次數，但不顯示
        }

        private void cboFindGrid_TextChanged(object sender, EventArgs e)
        {
            var bValue = false;
            //var iCount = 0;

            if (!string.IsNullOrEmpty(cboFindGrid.Text.Trim()))
            {
                bValue = true;
                cboFindGrid.Tag = ""; //改成每次輸入都清空，按下「Next」/「Previous」/「Highlight」按鈕時再去統計

                //20201209 每輸入一個字元就統計一次，如果SQL內容很多，就會嚴重 lag
                //iCount = CountGrid(); //cboFindGrid_TextChanged, 統計出現次數，但不顯示
            }

            btnFindNextGrid.Enabled = bValue;
            btnFindPreviousGrid.Enabled = bValue;
            btnCountGrid.Enabled = bValue;
            btnHighlightAllGrid.Enabled = bValue; //(iCount > 0);
            btnClearHighlightsGrid.Enabled = bValue;
        }

        private int CountGrid(bool bShowMsg = false)
        {
            var iCount = 0;
            var c1Grid = GetWhichGrid();

            for (var iRow = 0; iRow < c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count; iRow++)
            {
                var vr = c1Grid.Splits[_SplitsNum].Rows[iRow];

                iCount += c1TrueDBGrid1.Columns.Cast<C1DataColumn>().Count(col1 => col1.CellText(vr.DataRowIndex).Length != col1.CellText(vr.DataRowIndex).ToUpper().Replace(cboFindGrid.Text.ToUpper(), "").Length);
            }

            if (!bShowMsg)
            {
                return iCount;
            }

            FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "JasonQuery");
            var sTemp1 = MyGlobal.GetLanguageString("Find What:", "form", Name, "msg", "FindWhat", "Text");
            var sTemp2 = MyGlobal.GetLanguageString("Count:", "form", Name, "msg", "Count", "Text");
            var sTemp3 = MyGlobal.GetLanguageString("matches.", "form", Name, "msg", "matches", "Text");
            MessageBox.Show(sTemp1 + @" " + cboFindGrid.Text + "\r\n\r\n" + sTemp2 + " " + iCount + @" " + sTemp3, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return iCount;
        }

        private void btnFindNextGrid_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboFindGrid.Text.Trim()))
            {
                cboFindGrid.Text = "";
                return;
            }

            if (string.IsNullOrEmpty(cboFindGrid.Tag.ToString()))
            {
                cboFindGrid.Tag = CountGrid().ToString(); //btnFindNextGrid_Click 統計出現次數，但不顯示
            }

            if (cboFindGrid.Tag.ToString() == "0")
            {
                FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "JasonQuery");

                _sLangText = MyGlobal.GetLanguageString("Can't find the text", "form", Name, "msg", "CantFindText", "Text");
                MessageBox.Show(_sLangText + " \"" + cboFindGrid.Text + "\"", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            UpdateFindGridList(); //btnFindNextGrid_Click

            if (!FindNextGrid()) //btnFindNextGrid_Click
            {
                return;
            }

            //往下找不到資料，而且不是在第一格位置，再從頭開始找一次
            if (!(c1TrueDBGrid1.Row == 0 && c1TrueDBGrid1.Col == 0))
            {
                FindNextGrid(true); //btnFindNextGrid_Click
            }
        }

        private bool FindNextGrid(bool bFindAgain = false)
        {
            var sSearchText = cboFindGrid.Text;
            var iFindRow = 0;
            var iFindCol = 0;
            var bFind = false;
            bool bResult;

            var c1Grid = GetWhichGrid();

            var iStartCol = c1Grid.Col;
            var iStartRow = c1Grid.Row;

            if (bFindAgain)
            {
                iStartRow = 0;
                iStartCol = 0;
            }

            if (string.IsNullOrEmpty(sSearchText))
            {
                return false;
            }

            if (cboFindGrid.Items.Count > 0 && cboFindGrid.Text == cboFindGrid.Items[0].ToString())
            {
                //搜尋的字串是第一個，不用更新
            }
            else
            {
                SaveFindList("Grid", cboFindGrid.Text); //FindNextGrid
            }

            for (var iRow = iStartRow; iRow < c1Grid.Splits[_SplitsNum].Rows.Count; iRow++)
            {
                var vr = c1Grid.Splits[_SplitsNum].Rows[iRow];
                var iCol = 0;

                foreach (C1DataColumn col1 in c1Grid.Columns)
                {
                    if (col1.CellText(vr.DataRowIndex).Length != col1.CellText(vr.DataRowIndex).ToUpper().Replace(cboFindGrid.Text.ToUpper(), "").Length)
                    {
                        if (iRow == iStartRow) //游標所在列，尋找下一個，要略過此格！
                        {
                            if (iCol > iStartCol)
                            {
                                iFindRow = iRow;
                                iFindCol = iCol;

                                bFind = true;
                                break;
                            }
                        }
                        else
                        {
                            iFindRow = iRow;
                            iFindCol = iCol;

                            bFind = true;
                            break;
                        }
                    }

                    iCol++;
                }

                if (bFind)
                {
                    break;
                }
            }

            if (bFind)
            {
                c1Grid.Row = iFindRow;
                c1Grid.Col = iFindCol;
                c1Grid.Select(); //Focus 切換到指定的 Cell
                bResult = false;
            }
            else
            {
                bResult = true;
            }

            return bResult;
        }

        private void btnFindPreviousGrid_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboFindGrid.Text.Trim()))
            {
                cboFindGrid.Text = "";
                return;
            }

            if (string.IsNullOrEmpty(cboFindGrid.Tag.ToString()))
            {
                cboFindGrid.Tag = CountGrid().ToString(); //btnFindPreviousGrid_Click 統計出現次數，但不顯示
            }

            if (cboFindGrid.Tag.ToString() == "0")
            {
                FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "JasonQuery");

                _sLangText = MyGlobal.GetLanguageString("Can't find the text", "form", Name, "msg", "CantFindText", "Text");
                MessageBox.Show(_sLangText + " \"" + cboFindGrid.Text + "\"", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            UpdateFindGridList(); //btnFindPreviousGrid_Click

            if (FindPreviousGrid()) //btnFindPreviousGrid_Click: 1st
            {
                //往上找不到資料，再從底部開始找一次
                FindPreviousGrid(true); //btnFindPreviousGrid_Click: 2nd
            }
        }

        private bool FindPreviousGrid(bool bFindAgain = false)
        {
            var sSearchText = cboFindGrid.Text;
            var iStartRow = 0;
            var iFindRow = 0;
            var iStartCol = 0;
            var iFindCol = 0;
            var bFind = false;
            bool bResult;

            var c1Grid = GetWhichGrid();

            iStartCol = c1Grid.Col;
            iStartRow = c1Grid.Row;

            if (bFindAgain)
            {
                iStartRow = c1Grid.Splits[_SplitsNum].Rows.Count - 1;
                iStartCol = c1Grid.Splits[_SplitsNum].DisplayColumns.Count - 1;
            }

            if (string.IsNullOrEmpty(sSearchText))
            {
                return false;
            }

            if (cboFindGrid.Items.Count > 0 && cboFindGrid.Text == cboFindGrid.Items[0].ToString())
            {
                //搜尋的字串是第一個，不用更新
            }
            else
            {
                SaveFindList("Grid", cboFindGrid.Text); //FindPreviousGrid
            }

            for (var iRow = iStartRow; iRow >= 0; iRow--)
            {
                for (var jj = c1Grid.Splits[_SplitsNum].DisplayColumns.Count - 1; jj >= 0; jj--)
                {
                    if (c1Grid.Columns[jj].CellValue(iRow).ToString().Length == c1Grid.Columns[jj].CellValue(iRow).ToString().ToUpper().Replace(cboFindGrid.Text.ToUpper(), "").Length)
                    {
                        continue;
                    }

                    if (iRow == iStartRow) //游標所在列
                    {
                        if ((bFindAgain || jj >= iStartCol) && (!bFindAgain || jj > iStartCol))
                        {
                            continue;
                        }

                        iFindRow = iRow;
                        iFindCol = jj;

                        bFind = true;
                        break;
                    }

                    iFindRow = iRow;
                    iFindCol = jj;

                    bFind = true;
                    break;
                }

                if (bFind)
                {
                    break;
                }
            }

            if (bFind)
            {
                c1Grid.Row = iFindRow;
                c1Grid.Col = iFindCol;
                c1Grid.Select(); //Focus 切換到指定的 Cell
                bResult = false;
            }
            else
            {
                bResult = true;
            }

            return bResult;
        }

        private void btnClearHighlightsGrid_Click(object sender, EventArgs e)
        {
            var c1Grid = GetWhichGrid();

            foreach (C1DisplayColumn cd in c1Grid.Splits[_SplitsNum].DisplayColumns)
            {
                cd.OwnerDraw = false;
            }

            _modifiedList.Clear();
            c1Grid.ClearCellStyle(CellStyleFlag.AllCells);
        }

        private void btnCountGrid_Click(object sender, EventArgs e)
        {
            HighlightGridCount(true);
        }

        private void HighlightGridCount(bool bShowMsg = false)
        {
            var iCount = 0;
            var c1Grid = GetWhichGrid();

            for (var iRow = 0; iRow < c1Grid.Splits[_SplitsNum].Rows.Count; iRow++)
            {
                var vr = c1Grid.Splits[_SplitsNum].Rows[iRow];

                iCount += c1Grid.Columns.Cast<C1DataColumn>().Count(col1 => col1.CellText(vr.DataRowIndex).Length != col1.CellText(vr.DataRowIndex).ToUpper().Replace(cboFindGrid.Text.ToUpper(), "").Length);
            }

            if (!bShowMsg)
            {
                return;
            }

            FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "JasonQuery");

            var sTemp1 = MyGlobal.GetLanguageString("Find What:", "form", Name, "msg", "FindWhat", "Text");
            var sTemp2 = MyGlobal.GetLanguageString("Count:", "form", Name, "msg", "Count", "Text");
            var sTemp3 = MyGlobal.GetLanguageString("matches.", "form", Name, "msg", "matches", "Text");
            MessageBox.Show(sTemp1 + @" " + cboFindGrid.Text + "\r\n\r\n" + sTemp2 + @" " + iCount + @" " + sTemp3, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHighlightAllGrid_Click(object sender, EventArgs e)
        {
            var bFind = false;

            btnClearHighlightsGrid.PerformClick();

            var c1Grid = GetWhichGrid();

            if (string.IsNullOrEmpty(cboFindGrid.Tag.ToString()))
            {
                cboFindGrid.Tag = CountGrid().ToString(); //btnHighlightAllGrid_Click 統計出現次數，但不顯示
            }

            if (cboFindGrid.Tag.ToString() == "0")
            {
                FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "JasonQuery");

                _sLangText = MyGlobal.GetLanguageString("Can't find the text", "form", Name, "msg", "CantFindText", "Text");
                MessageBox.Show(_sLangText + " \"" + cboFindGrid.Text + "\"", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            UpdateFindGridList(); //btnHighlightAllGrid_Click

            for (var iRow = 0; iRow < c1Grid.Splits[_SplitsNum].Rows.Count; iRow++)
            {
                var iCol = 0;
                var vr = c1Grid.Splits[_SplitsNum].Rows[iRow];

                foreach (C1DataColumn col1 in c1Grid.Columns)
                {
                    if (col1.CellText(vr.DataRowIndex).Length != col1.CellText(vr.DataRowIndex).ToUpper().Replace(cboFindGrid.Text.ToUpper(), "").Length)
                    {
                        bFind = true;
                        _modifiedList.Add(new Point(iRow, iCol));
                    }

                    iCol++;
                }
            }

            if (!bFind)
            {
                return;
            }

            foreach (C1DisplayColumn cd in c1Grid.Splits[_SplitsNum].DisplayColumns)
            {
                cd.OwnerDraw = true;
            }
        }

        private void c1TrueDBGrid1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            var mc = new Point(e.Row, e.Col);

            if (!_modifiedList.Contains(mc))
            {
                return;
            }

            e.Style.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridHighlightForeColor); //Color.Green;
            e.Style.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridHighlightBackColor); //Color.LightPink;
        }

        private void c1TrueDBGrid1_MouseDown(object sender, MouseEventArgs e)
        {
            var c1Grid = GetWhichGrid();
            c1Grid.ContextMenuStrip = null;

            //取得滑鼠所在列的儲存格行列位置
            var iRow = c1Grid.RowContaining(e.Y);
            var iCol = c1Grid.ColContaining(e.X);
            var bCornerSelected = false;

            if (iRow == -1 && iCol == -1)
            {
                bCornerSelected = chkShowFilterRow.Checked == false || e.Y <= c1Grid.Splits[_SplitsNum].ColumnCaptionHeight;
            }

            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    if (iCol != -1)
                    {
                        _iShiftMouseLeftDownX = iCol;
                    }

                    if (iRow != -1)
                    {
                        _iShiftMouseLeftDownY = iRow;
                    }

                    break;
                }
                case MouseButtons.Right:
                    {
                        if (bCornerSelected) //按下 Grid's 左上角
                        {
                            return;
                        }

                        if (iRow == -1)
                        {
                            if (iCol == -1)
                            {
                                //20220124 使用者點到最左側，略過！
                                //20220319 使用者點 DataGrid 的灰色地帶，略過！
                                return;
                            }

                            var sColumnName2 = c1Grid.Splits[_SplitsNum].DisplayColumns[iCol].ToString(); //20210905 c1Grid.Col => iCol, 修正右鍵顯示的欄位不正確問題
                            
                            if (sColumnName2.IndexOf("\r", StringComparison.Ordinal) != -1)
                            {
                                sColumnName2 = sColumnName2.Substring(0, sColumnName2.IndexOf("\r", StringComparison.Ordinal));
                            }

                            _cMenuGridHeader.Items[0].Text = sColumnName2;
                            _cMenuGridHeader.Items[0].Tag = iCol.ToString();
                            _cMenuGridHeader.Items[0].Enabled = false;

                            if (c1Grid.SelectedCols.Count <= 1) //直接在 Column Name 上按右鍵，會是等於0
                            {
                                _cMenuGridHeader.Items[2].Tag = sColumnName2; //Copy Column Name
                            }
                            else
                            {
                                sColumnName2 = "";

                                for (var i = 0; i < c1Grid.SelectedCols.Count; i++)
                                {
                                    sColumnName2 += c1Grid.SelectedCols[i].Caption + ", ";
                                }

                                _cMenuGridHeader.Items[2].Tag = sColumnName2.Substring(0, sColumnName2.Length - 2); //Copy Column Name
                            }

                            _cMenuGridHeader.Items[4].Enabled = ((DataTable)c1Grid.DataSource).Rows.Count > 0;
                            _cMenuGridHeader.Items[5].Enabled = ((DataTable)c1Grid.DataSource).Rows.Count > 0;

                            c1Grid.ContextMenuStrip = _cMenuGridHeader;

                            if (MyLibrary.bDarkMode)
                            {
                                _cMenuGridHeader.BackColor = ColorTranslator.FromHtml("#2D2D30");
                                _cMenuGridHeader.ForeColor = Color.White;
                                _cMenuGridHeader.RenderMode = ToolStripRenderMode.System;
                                //_cMenuGridHeader.ShowImageMargin = false;
                            }

                            _cMenuGridHeader.Show(c1Grid, new Point(e.X, e.Y));

                            return;
                        }

                        GetGridSelectedXY(iCol, iRow);
                        var sColumnName = c1Grid.Splits[_SplitsNum].DisplayColumns[iCol].ToString(); //20210905 c1Grid.Col => iCol, 修正右鍵顯示的欄位不正確問題

                        if (sColumnName.IndexOf("\r", StringComparison.Ordinal) != -1)
                        {
                            sColumnName = sColumnName.Substring(0, sColumnName.IndexOf("\r", StringComparison.Ordinal));
                        }

                        var bValue = !(sColumnName.ToUpper() == "DATA" && c1Grid.Columns.Count == 1 && c1Grid.Splits[_SplitsNum].Rows.Count == 0 || c1Grid.Columns.Count >= 1 && c1Grid.Splits[_SplitsNum].Rows.Count == 0);
                        var bValueUnfreeze = !string.IsNullOrEmpty(_sUnfreezeColumnName);

                        _cMenuGrid.Items[(int)_eMenuGrid.CellViewer].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.SingleRecordViewer].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.Dash0].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.SelectAll].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.Dash1].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.ExportAllDataToFile].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.ExportAllDataToFileScript].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.Dash3].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.CopyAllDataToClipboard].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.CopyAllDataToClipboardCurrentRow].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.Dash7].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.Copy].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.CopyAsQueryCondition].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.CopyWithColumnNames].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.CopyColumnNames].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.Dash12].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.FreezeColumn].Enabled = bValue;
                        _cMenuGrid.Items[(int)_eMenuGrid.UnfreezeColumn].Enabled = bValueUnfreeze;

                        _sLangText = MyGlobal.GetLanguageString("Cell Viewer", "form", Name, "menugrid", "CellViewer", "Text");
                        _cMenuGrid.Items[(int)_eMenuGrid.CellViewer].Text = _sLangText + (bValue == false ? "" : " (" + sColumnName + ")");

                        _sLangText = MyGlobal.GetLanguageString("Freeze Column", "form", Name, "menugrid", "FreezeColumn", "Text");
                        _cMenuGrid.Items[(int)_eMenuGrid.FreezeColumn].Text = _sLangText + (bValue == false ? "" : " (" + sColumnName + ")");

                        _sLangText = MyGlobal.GetLanguageString("Unfreeze Column", "form", Name, "menugrid", "UnfreezeColumn", "Text");
                        _cMenuGrid.Items[(int)_eMenuGrid.UnfreezeColumn].Text = _sLangText + (string.IsNullOrEmpty(_sUnfreezeColumnName) ? "" : " (" + _sUnfreezeColumnName + ")");

                        c1Grid.ContextMenuStrip = _cMenuGrid;

                        if (MyLibrary.bDarkMode)
                        {
                            _cMenuGrid.BackColor = ColorTranslator.FromHtml("#2D2D30");
                            _cMenuGrid.ForeColor = Color.White;
                            _cMenuGrid.RenderMode = ToolStripRenderMode.System;
                            //_cMenuGrid.ShowImageMargin = false;
                        }

                        _cMenuGrid.Show(c1Grid, new Point(e.X, e.Y));

                        break;
                }
            }
        }

        private string GetGridSelectedXY(int iCol = -1, int iRow = -1)
        {
            var sCells = ";";
            var c1Grid = GetWhichGrid();
            var selCol = c1Grid.SelectedCols.Count;

            foreach (int row in c1Grid.SelectedRows)
            {
                var i = 0;

                if (selCol == 0) //整列選取
                {
                    i = 0;

                    foreach (C1DataColumn col1 in c1Grid.Columns)
                    {
                        sCells += i + "," + row + ";";
                        i++;
                    }
                }
                else //非整列選取 (選取區塊)
                {
                    foreach (C1DataColumn col1 in c1Grid.SelectedCols)
                    {
                        i = c1Grid.Columns.Cast<C1DataColumn>().TakeWhile(col11 => col1.Caption != col11.Caption).Count(); //for old code => 20210310-1

                        sCells += i + "," + row + ";";
                    }
                }
            }

            if (iRow == -1 || iCol == -1)
            {
                return sCells;
            }

            return sCells.IndexOf(";" + iCol + "," + iRow + ";", 0, StringComparison.Ordinal) != -1 ? "true" : "false";
        }

        private void c1TrueDBGrid1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_bGridZooming)
            {
                return;
            }

            if (!_ctrlKeyDown)
            {
                return;
            }

            _ctrlKeyDown = !_ctrlKeyDown; //20230710 此處強制變更 _ctrlKeyDown，避免後續使用者沒有按下 Ctrl，只是單純滾動捲軸時會變成 Grid 放大、縮小
            _bBusy = true;
            _bGridZooming = true;
            _totalDelta += e.Delta;
            var fValue = 1 + (float)(SystemInformation.MouseWheelScrollLines * _totalDelta) / 3600;

            if (fValue > 1.7 || fValue < 0.5)
            {
                //
            }
            else
            {
                ZoomGrid(fValue);
            }

            _bBusy = false;
            _bGridZooming = false;
        }

        private void FrozenColumn(bool bFrozen = true)
        {
            var c1Grid = GetWhichGrid();

            for (var i = 0; i < c1Grid.Splits[_SplitsNum].DisplayColumns.Count; i++)
            {
                c1Grid.Splits[_SplitsNum].DisplayColumns[i].Frozen = false;
            }

            if (bFrozen == false)
            {
                _sUnfreezeColumnName = "";
                return;
            }

            c1Grid.Splits[_SplitsNum].DisplayColumns[c1Grid.Col].Frozen = true;
            _sUnfreezeColumnName = c1Grid.Splits[_SplitsNum].DisplayColumns[c1Grid.Col].ToString();
            _cMenuGrid.Items[(int)_eMenuGrid.UnfreezeColumn].Enabled = true;
        }

        private void ArrangeData(string sMode)
        {
            var i = 0;
            var sData = "";
            var sColumnName = "";
            var sDataType = "";
            var bCopy = false;
            var bActiveCell = true; //是否為「只點到某一個 cell，並沒有『選取』cell」?
            string sColumnDataType;

            var c1Grid = GetWhichGrid();

            var selCol = c1Grid.SelectedCols.Count;

            var bSelectedWholeColumn = c1Grid.SelectedRows.Count == 0 && c1Grid.SelectedCols.Count > 0;

            var sQuotingWith = mnuResultCopyQuotingWith.Tag.ToString() == "None" ? "" : mnuResultCopyQuotingWith.Tag.ToString();

            if (sMode.ToUpper() == "COPYASQUERYCONDITION")
            {
                sQuotingWith = "'";
            }

            var sFieldSeparator = mnuResultCopyFieldSeparator.Tag.ToString();

            if (sMode.ToUpper() == "COPYASQUERYCONDITION")
            {
                sFieldSeparator = ",";
            }

            if (bSelectedWholeColumn) //整欄選取
            {
                var sDistinct = "```";
                bActiveCell = false;

                for (var row = 0; row < c1Grid.Splits[_SplitsNum].Rows.Count; row++)
                {
                    foreach (var col in c1Grid.SelectedCols)
                    {
                        if (selCol > 1)
                        {
                            if (sDistinct.IndexOf("```" + col.ToString() + "```", StringComparison.Ordinal) != -1)
                            {
                                continue;
                            }
                            else
                            {
                                sDistinct += col.ToString() + "```";
                            }
                        }

                        string sTemp1;
                        string sTemp2;

                        if (c1Grid.Columns[col.ToString()].Caption.IndexOf("\n", 0, StringComparison.Ordinal) != -1)
                        {
                            sTemp1 = c1Grid.Columns[col.ToString()].Caption.Replace("\r\n", "\n").Split('\n')[0];
                            sTemp2 = c1Grid.Columns[col.ToString()].Caption.Replace("\r\n", "\n").Split('\n')[1];
                        }
                        else
                        {
                            sTemp1 = c1Grid.Columns[col.ToString()].Caption;
                            sTemp2 = "";
                        }

                        if (row == 0)
                        {
                            //收集 Column Name & Data Type
                            sColumnName += sTemp1 + sFieldSeparator;
                            sDataType += string.IsNullOrEmpty(sTemp2) ? "" : sTemp2 + sFieldSeparator;
                        }

                        sColumnDataType = (c1Grid.Columns[col.ToString()].Tag ?? string.Empty).ToString();

                        switch (sColumnDataType.ToUpper())
                        {
                            case "NSTRING": //20220711 這個欄位要特別處理！
                                sData += "N" + sQuotingWith + c1Grid.Columns[col.ToString()].CellText(row) + sQuotingWith + sFieldSeparator;
                                break;
                            case "STRING":
                            case "DATETIME":
                                sData += sQuotingWith + c1Grid.Columns[col.ToString()].CellText(row) + sQuotingWith + sFieldSeparator;
                                break;
                            default:
                                if (c1Grid.Columns[col.ToString()].CellText(row) == MyLibrary.sGridNullShowAs)
                                {
                                    sData += "null" + sFieldSeparator; //數字，but it's null
                                }
                                else
                                {
                                    sData += c1Grid.Columns[col.ToString()].CellText(row) + sFieldSeparator; //數字
                                }
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(sData))
                    {
                        sData = sData.Substring(0, sData.Length - sFieldSeparator.Length) + "\r\n";
                    }

                    sDistinct = "```";
                }
            }
            else //非整欄選取
            {
                foreach (int row in c1Grid.SelectedRows)
                {
                    var vr = c1Grid.Splits[_SplitsNum].Rows[row];

                    string sTemp1;
                    string sTemp2;

                    if (selCol == 0) //整列選取
                    {
                        bActiveCell = false;

                        foreach (C1DataColumn col1 in c1Grid.Columns)
                        {
                            if (col1.Caption.IndexOf("\n", 0, StringComparison.Ordinal) != -1)
                            {
                                sTemp1 = col1.Caption.Replace("\r\n", "\n").Split('\n')[0];
                                sTemp2 = col1.Caption.Replace("\r\n", "\n").Split('\n')[1];
                            }
                            else
                            {
                                sTemp1 = col1.Caption;
                                sTemp2 = "";
                            }

                            if (i == 0)
                            {
                                //收集 Column Name & Data Type
                                sColumnName += sTemp1 + sFieldSeparator;
                                sDataType += string.IsNullOrEmpty(sTemp2) ? "" : sTemp2 + sFieldSeparator;
                            }

                            sColumnDataType = (col1.Tag ?? string.Empty).ToString();

                            switch (sColumnDataType.ToUpper())
                            {
                                case "NSTRING": //20220711 這個欄位要特別處理！
                                    sData += "N" + sQuotingWith + col1.CellText(vr.DataRowIndex) + sQuotingWith + sFieldSeparator;
                                    break;
                                case "STRING":
                                case "DATETIME":
                                    sData += sQuotingWith + col1.CellText(vr.DataRowIndex) + sQuotingWith + sFieldSeparator;
                                    break;
                                default:
                                    if (col1.CellText(vr.DataRowIndex) == MyLibrary.sGridNullShowAs)
                                    {
                                        sData += "null" + sFieldSeparator; //數字，but it's null
                                    }
                                    else
                                    {
                                        sData += col1.CellText(vr.DataRowIndex) + sFieldSeparator; //數字
                                    }
                                    break;
                            }
                        }

                        i++;
                    }
                    else //非整列選取 (選取區塊)
                    {
                        foreach (C1DataColumn col1 in c1Grid.SelectedCols)
                        {
                            bActiveCell = false;

                            if (col1.Caption.IndexOf("\r\n", 0, StringComparison.Ordinal) != -1)
                            {
                                sTemp1 = col1.Caption.Replace("\r\n", "\n").Split('\n')[0];
                                sTemp2 = col1.Caption.Replace("\r\n", "\n").Split('\n')[1];
                            }
                            else
                            {
                                sTemp1 = col1.Caption;
                                sTemp2 = "";
                            }

                            if (i == 0)
                            {
                                //收集 Column Name & Data Type
                                sColumnName += sTemp1 + sFieldSeparator;
                                sDataType += string.IsNullOrEmpty(sTemp2) ? "" : sTemp2 + sFieldSeparator;
                            }

                            sColumnDataType = (col1.Tag ?? string.Empty).ToString();

                            switch (sColumnDataType.ToUpper())
                            {
                                case "NSTRING":  //20220711 這個欄位要特別處理！
                                    sData += "N" + sQuotingWith + col1.CellText(vr.DataRowIndex) + sQuotingWith + sFieldSeparator;
                                    break;
                                case "STRING":
                                case "DATETIME":
                                    sData += sQuotingWith + col1.CellText(vr.DataRowIndex) + sQuotingWith + sFieldSeparator;
                                    break;
                                default:
                                    if (col1.CellText(vr.DataRowIndex) == MyLibrary.sGridNullShowAs)
                                    {
                                        sData += "null" + sFieldSeparator; //數字，but it's null
                                    }
                                    else
                                    {
                                        sData += col1.CellText(vr.DataRowIndex) + sFieldSeparator; //數字
                                    }
                                    break;
                            }
                        }

                        i++;
                    }

                    if (!string.IsNullOrEmpty(sData))
                    {
                        sData = sData.Substring(0, sData.Length - sFieldSeparator.Length) + "\r\n";
                    }
                }
            }

            if (!string.IsNullOrEmpty(sColumnName))
            {
                sColumnName = sColumnName.Substring(0, sColumnName.Length - sFieldSeparator.Length);
            }

            if (!string.IsNullOrEmpty(sDataType))
            {
                sDataType = sDataType.Substring(0, sDataType.Length - sFieldSeparator.Length);
            }

            switch (sMode.ToUpper())
            {
                case "COPY":
                case "COPYASQUERYCONDITION":
                    bCopy = true;

                    if (bActiveCell)
                    {
                        i = 0;

                        foreach (C1DataColumn col1 in c1Grid.Columns)
                        {
                            if (i == c1Grid.Col)
                            {
                                sColumnDataType = (col1.Tag ?? string.Empty).ToString();

                                switch (sColumnDataType.ToUpper())
                                {
                                    case "STRING":
                                    case "DATETIME":
                                        sData = sQuotingWith + c1Grid[c1Grid.Splits[_SplitsNum].Rows[c1Grid.Row].DataRowIndex, c1Grid.Col] + sQuotingWith;
                                        break;
                                    case "NSTRING" when string.IsNullOrEmpty(sQuotingWith): //20220731 如果 sQuotingWith 是空值，前面就不用加 "N"
                                        sData = c1Grid[c1Grid.Splits[_SplitsNum].Rows[c1Grid.Row].DataRowIndex, c1Grid.Col].ToString();
                                        break;
                                    case "NSTRING": //20220711 這個欄位要特別處理！
                                        sData = "N" + sQuotingWith + c1Grid[c1Grid.Splits[_SplitsNum].Rows[c1Grid.Row].DataRowIndex, c1Grid.Col] + sQuotingWith;
                                        break;
                                    default:
                                        sData = c1Grid[c1Grid.Splits[_SplitsNum].Rows[c1Grid.Row].DataRowIndex, c1Grid.Col].ToString();
                                        break;
                                }

                                break;
                            }

                            i++;
                        }
                    }

                    if (sData.Length >= 2 && sData.Substring(sData.Length - 2, 2) == "\r\n")
                    {
                        sData = sData.Substring(0, sData.Length - 2);
                    }

                    if (sMode.ToUpper() == "COPYASQUERYCONDITION")
                    {
                        var parts = sData.Split(new[] { "\r\n" }, StringSplitOptions.None);
                        var sDistinct = "```";

                        foreach (var t in parts)
                        {
                            if (sDistinct.IndexOf("```" + t + "```", StringComparison.Ordinal) == -1)
                            {
                                sDistinct += t + "```";
                            }
                        }

                        if (sDistinct.StartsWith("```"))
                        {
                            sDistinct = sDistinct.Substring(3, sDistinct.Length - 3);
                        }

                        if (sDistinct.EndsWith("```"))
                        {
                            sDistinct = sDistinct.Substring(0, sDistinct.Length - 3);
                        }

                        sData = sDistinct.Replace("```", "\r\n");

                        if ((selCol > 1 || selCol == 0) && sColumnName.IndexOf(sFieldSeparator, 0, StringComparison.Ordinal) != -1) //selCol == 0：整列選取或是單一Cell
                        {
                            if (!bActiveCell) //如果是單一Cell，前後不用加括號
                            {
                                sData = "((" + sData.Replace("\r\n", "),(") + "))";
                            }
                        }
                        else
                        {
                            sData = "(" + sData.Replace("\r\n", ",") + ")";
                        }
                    }

                    break;
                case "COPYWITHCOLUMNNAMES":
                    bCopy = true;
                    sData = sColumnName + (string.IsNullOrEmpty(sDataType) ? "" : "\r\n" + sDataType) + "\r\n" + sData;
                    break;
                case "COPYCOLUMNNAMES":
                    bCopy = true;
                    sData = sColumnName + (string.IsNullOrEmpty(sDataType) ? "" : "\r\n" + sDataType);
                    break;
            }

            if (bCopy)
            {
                CopyTextToClipboard(sData, "06");
            }
            else
            {
                var sf = new SaveFileDialog();

                _sLangText = MyGlobal.GetLanguageString("Save As", "Global", "Global", "msg", "SaveAs", "Text");

                sf.Title = _sLangText;
                sf.FileName = MyLibrary.sGridSheetName;

                sf.Filter = sMode.ToUpper() == "EXPORTALLDATATOCSV" ? @"Query files (*.csv)|*.csv|All files (*.*)|*.*" : @"Query files (*.sql)|*.sql|All files (*.*)|*.*";

                if (sf.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (sMode.ToUpper() == "EXPORTALLDATATOCSV")
                {
                    if (string.IsNullOrEmpty(Path.GetExtension(sf.FileName)))
                    {
                        sf.FileName += ".csv";
                    }

                    TextEngine.WriteContentToFile(sData, sf.FileName, TextEncode.Default);
                }
                else
                {
                    if (string.IsNullOrEmpty(Path.GetExtension(sf.FileName)))
                    {
                        sf.FileName += ".sql";
                    }

                    TextEngine.WriteContentToFile(sData, sf.FileName, TextEncode.UTF8);
                }
            }
        }

        private void ArrangeData4AllData(string sMode, bool bToDate = false)
        {
            var i = 0;
            var sCsvData = "";
            var sInsertFiled = "";
            var sInsertDataResult = "";
            var sColumnName = "";
            var sDataType = "";
            string sTemp1;
            string sTemp2;
            var sTableName = "";
            var sSaveAsFilename = "";
            var sTitleName = "";
            var bAutoOpenFile = false;
            var vUpperCase = MyLibrary.iSQLFormatterConvertCaseForKeywordsCase == 1;

            var c1Grid = GetWhichGrid();

            if (sMode == "ExportAllDataToFileScript")
            {
                sTableName = (c1Grid.Tag ?? "table_name").ToString();
                sTemp1 = MyGlobal.GetLanguageString("Table name", "form", Name, "msg", "TableName", "Text") + " - " + MyGlobal.GetLanguageString("Export all data to File (as \"Insert Into\" script)", "form", Name, "menugrid", "ExportAllDataToFile", "Text");
                sTemp2 = MyGlobal.GetLanguageString("Please input table name:", "form", Name, "msg", "InputTableName", "Text");
                var sTemp3 = MyGlobal.GetLanguageString("After the file is generated, the file will be opened automatically in a new window.", "form", Name, "msg", "AutoOpenExportedFile", "Text");

                if (InputBox(sTemp1, sTemp2, sTemp3, Cursor.Position.X, Cursor.Position.Y, ref sTableName, ref bAutoOpenFile) != DialogResult.OK)
                {
                    return;
                }
            }

            if (sMode.StartsWith("CopyAllDataToClipboard"))
            {
                sTableName = (c1Grid.Tag ?? "table_name").ToString();
            }
            else
            {
                var sf = new SaveFileDialog();
                _sLangText = MyGlobal.GetLanguageString("Save As", "Global", "Global", "msg", "SaveAs", "Text");

                switch (sMode)
                {
                    case "ExportAllDataToCSV":
                        sTitleName = MyGlobal.GetLanguageString("Export all data to CSV", "form", Name, "menugrid", "ExportAllDataToCSV", "Text");
                        _sLangText += " - " + sTitleName;
                        sf.FileName = MyLibrary.sGridSheetName;
                        break;
                    case "ExportAllDataToFileScript":
                        sTitleName = MyGlobal.GetLanguageString("Export all data to File (as Insert script)", "form", Name, "menugrid", "ExportAllDataToFileScript", "Text");
                        _sLangText += " - " + sTitleName;
                        sf.FileName = "InsertScript_" + sTableName;
                        break;
                }

                sf.Title = _sLangText;
                sf.Filter = sMode.ToUpper() == "EXPORTALLDATATOCSV" ? @"Query files (*.csv)|*.csv|All files (*.*)|*.*" : @"Query files (*.sql)|*.sql|All files (*.*)|*.*";

                var iXTemp = Cursor.Position.X + 20;
                var iYTemp = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height * 0.25);

                MoveDialogWhenOpened(sf.Title, iXTemp, iYTemp); //c1ArrangeData4AllData

                if (sf.ShowDialog() == DialogResult.OK) //無論檔案是否存在，只要不是按「取消」或「否」，都會回傳 OK
                {
                    sSaveAsFilename = sf.FileName;
                }
                else
                {
                    return;
                }
            }

            Cursor = Cursors.WaitCursor;
            var sQuotingWith = "";

            if (mnuResultCopyQuotingWithDoubleQuoting.Checked)
            {
                sQuotingWith = "\"";
            }
            else if (mnuResultCopyQuotingWithSingleQuoting.Checked)
            {
                sQuotingWith = "'";
            }

            string sFieldSeparator;

            if (mnuResultCopyFieldSeparatorSemicolon.Checked)
            {
                sFieldSeparator = ";";
            }
            else if (mnuResultCopyFieldSeparatorI.Checked)
            {
                sFieldSeparator = "|";
            }
            else
            {
                sFieldSeparator = ",";
            }

            var iStartRow = 0;
            var iEndRow = c1Grid.Splits[_SplitsNum].Rows.Count;

            if (sMode == "CopyAllDataToClipboardCurrentRow")
            {
                iStartRow = c1Grid.Row;
                iEndRow = c1Grid.Row + 1;
            }
            else
            {
                MyGlobal.iProgressInsertInto = 0;
                MyGlobal.bProgressCancel = false;

                var myForm = new frmProgressInfo
                {
                    sTitleName = sTitleName,
                    iTotalQty = c1Grid.Splits[_SplitsNum].Rows.Count,
                    TopMost = false,
                    FormBorderStyle = FormBorderStyle.None,
                    ShowInTaskbar = false,
                    Owner = this,
                    StartPosition = FormStartPosition.CenterScreen
                };

                myForm.Show();
            }

            for (var iRow = iStartRow; iRow < iEndRow; iRow++)
            {
                var sInsertData = "";
                var vr = c1Grid.Splits[_SplitsNum].Rows[iRow];

                foreach (C1DataColumn col1 in c1Grid.Columns)
                {
                    if (col1.Caption.IndexOf("\r\n", 0, StringComparison.Ordinal) != -1)
                    {
                        sTemp1 = col1.Caption.Replace("\r\n", "\n").Split('\n')[0];
                        sTemp2 = col1.Caption.Replace("\r\n", "\n").Split('\n')[1];
                    }
                    else if (col1.Caption.IndexOf("\r", 0, StringComparison.Ordinal) != -1)
                    {
                        sTemp1 = col1.Caption.Split('\r')[0];
                        sTemp2 = col1.Caption.Split('\r')[1];
                    }
                    else
                    {
                        sTemp1 = col1.Caption;
                        sTemp2 = "";
                    }

                    if (i == 0)
                    {
                        //收集 Column Name & Data Type
                        sColumnName += sTemp1 + sFieldSeparator;
                        sDataType += string.IsNullOrEmpty(sTemp2) ? "" : sTemp2 + sFieldSeparator;

                        sInsertFiled += sTemp1 + ", ";
                    }

                    var sColumnDataType = (col1.Tag ?? string.Empty).ToString();

                    if ((sMode == "ExportAllDataToFileScript" || sMode.StartsWith("CopyAllDataToClipboard")) && col1.CellText(vr.DataRowIndex) == MyLibrary.sGridNullShowAs)
                    {
                        if (vUpperCase)
                        {
                            sInsertData += "NULL, ";
                        }
                        else
                        {
                            sInsertData += "null, ";
                        }
                    }
                    else switch (sColumnDataType.ToUpper())
                    {
                        case "STRING":
                            sCsvData += sQuotingWith + col1.CellText(vr.DataRowIndex) + sQuotingWith + sFieldSeparator;
                            sInsertData += "'" + col1.CellText(vr.DataRowIndex).Replace("'", "''") + "', ";
                            break;
                        case "NCHAR": //20220421 N型態，這幾個欄位型態要特別處理！
                        case "NSTRING":
                        case "NTEXT":
                            sCsvData += sQuotingWith + col1.CellText(vr.DataRowIndex) + sQuotingWith + sFieldSeparator;
                            sInsertData += "N'" + col1.CellText(vr.DataRowIndex).Replace("'", "''") + "', ";
                            break;
                        case "DATETIME":
                        {
                            sCsvData += sQuotingWith + col1.CellText(vr.DataRowIndex) + sQuotingWith + sFieldSeparator;

                            if (bToDate == false)
                            {
                                sInsertData += "'" + col1.CellText(vr.DataRowIndex) + "', ";
                            }
                            else
                            {
                                if (vUpperCase)
                                {
                                    sInsertData += "TO_DATE('" + col1.CellText(vr.DataRowIndex) + "','" + MyLibrary.sDateFormat + " HH24:MI:SS'), ";
                                }
                                else
                                {
                                    sInsertData += "to_date('" + col1.CellText(vr.DataRowIndex) + "','" + MyLibrary.sDateFormat + " HH24:MI:SS'), ";
                                }
                            }

                            break;
                        }
                        default:
                            sCsvData += col1.CellText(vr.DataRowIndex) + sFieldSeparator; //數字相關的
                            sInsertData += col1.CellText(vr.DataRowIndex) + ", ";
                            break;
                    }

                    if (MyGlobal.bProgressCancel)
                    {
                        break;
                    }
                }

                i++;
                Application.DoEvents();
                MyGlobal.iProgressInsertInto = i;

                if (!string.IsNullOrEmpty(sCsvData))
                {
                    sCsvData = sCsvData.Substring(0, sCsvData.Length - sFieldSeparator.Length) + "\r\n";
                }

                if (vUpperCase)
                {
                    sInsertDataResult += "INSERT INTO " + sTableName + " (" + sInsertFiled.Substring(0, sInsertFiled.Length - 2) + ")\r\nVALUES (" + sInsertData.Substring(0, sInsertData.Length - 2) + ");\r\n\r\n";
                }
                else
                {
                    sInsertDataResult += "insert into " + sTableName + " (" + sInsertFiled.Substring(0, sInsertFiled.Length - 2) + ")\r\nvalues (" + sInsertData.Substring(0, sInsertData.Length - 2) + ");\r\n\r\n";
                }

                if (MyGlobal.bProgressCancel)
                {
                    break;
                }
            }

            if (!string.IsNullOrEmpty(sCsvData))
            {
                sCsvData = sCsvData.Substring(0, sCsvData.Length - 2);
            }

            if (!string.IsNullOrEmpty(sColumnName))
            {
                sColumnName = sColumnName.Substring(0, sColumnName.Length - sFieldSeparator.Length);
            }

            if (chkShowColumnType.Checked && !string.IsNullOrEmpty(sDataType))
            {
                sDataType = sDataType.Substring(0, sDataType.Length - sFieldSeparator.Length);
            }

            if (sMode == "ExportAllDataToCSV")
            {
                sCsvData = sColumnName + (string.IsNullOrEmpty(sDataType) ? "" : ("\r\n" + sDataType)) + "\r\n" + sCsvData;
            }

            _sLangText = MyGlobal.GetLanguageString("has been exported!", "form", Name, "msg", "ExportOK", "Text");

            if (string.IsNullOrEmpty(Path.GetExtension(sSaveAsFilename)))
            {
                sSaveAsFilename += ((sMode.ToUpper() == "EXPORTALLDATATOCSV") ? ".csv" : ".sql");
            }

            if (MyGlobal.bProgressCancel)
            {
                _sLangText = MyGlobal.GetLanguageString("This operation has been cancelled.", "Global", "Global", "msg", "CancelByUser", "Text");
                UpdateStatusBarInfo(_sLangText);
                Cursor = Cursors.Default;
                return;
            }

            if (sMode.ToUpper() == "EXPORTALLDATATOCSV")
            {
                TextEngine.WriteContentToFile(sCsvData, sSaveAsFilename, TextEncode.Default); //此處用 TextEncode.Default 存檔，Excel 比較不會出現異常 (比如分割位置錯誤)
                UpdateStatusBarInfo(sSaveAsFilename + " " + _sLangText);
            }
            else
            {
                if (sInsertDataResult.EndsWith("\r\n\r\n"))
                {
                    sInsertDataResult = sInsertDataResult.Substring(0, sInsertDataResult.Length - 2);
                }

                if (sMode.StartsWith("CopyAllDataToClipboard"))
                {
                    CopyTextToClipboard(sInsertDataResult, "07");
                    
                    _sLangText = MyGlobal.GetLanguageString("Data has been copied to the clipboard!", "form", Name, "msg", "CopyOK", "Text"); //資料已被複製到剪貼簿。
                    UpdateStatusBarInfo(_sLangText);
                }
                else if (sMode == "ExportAllDataToFileScript")
                {
                    TextEngine.WriteContentToFile(sInsertDataResult, sSaveAsFilename, TextEncode.UTF8);
                    UpdateStatusBarInfo(sSaveAsFilename + " " + _sLangText);

                    if (bAutoOpenFile)
                    {
                        TransferValueToMainForm("createnewtab`OPENFILE`" + sSaveAsFilename);
                    }
                }
            }

            Cursor = Cursors.Default;
        }

        private void SetSquiggle(bool bClearOnly, int iPos = 0, int iLength = 0)
        {
            editor.IndicatorCurrent = _iSquiggleNum;
            editor.IndicatorClearRange(0, editor.TextLength);

            if (bClearOnly)
            {
                return;
            }

            // Update indicator appearance
            //UpdateHighlight(MyLibrary.sHighlightColorStyle, iSquiggleNum);
            editor.Indicators[_iSquiggleNum].Style = ScintillaNET.IndicatorStyle.Squiggle;

            editor.Indicators[_iSquiggleNum].ForeColor = ColorTranslator.FromHtml(MyLibrary.sColorErrorLineBackground);
            editor.Indicators[_iSquiggleNum].Style = ScintillaNET.IndicatorStyle.Squiggle;
            editor.IndicatorFillRange(iPos, iLength);
        }

        private void SetSquiggles(string sSqlExecuted, string sPos, string sErrMsg, out int iCursorPostion, out int iLength)
        {
            var bFirst = true;
            iCursorPostion = 0;
            iLength = 0;
            editor.IndicatorCurrent = _iSquiggleNum;
            editor.IndicatorClearRange(0, editor.TextLength);
            editor.Indicators[_iSquiggleNum].Style = ScintillaNET.IndicatorStyle.Squiggle;
            editor.Indicators[_iSquiggleNum].ForeColor = ColorTranslator.FromHtml(MyLibrary.sColorErrorLineBackground);
            editor.Indicators[_iSquiggleNum].Style = ScintillaNET.IndicatorStyle.Squiggle;

            var iPos = Convert.ToInt32(sPos) - 1;
            var parts = sSqlExecuted.Split(new[] { "\r\n" }, StringSplitOptions.None);
            var parts2 = sErrMsg.Split(new[] { MyGlobal.sSeparatorPlus1 }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var t in parts2)
            {
                var sLine = t.Split(new[] { MyGlobal.sSeparatorPlus2 }, StringSplitOptions.None)[0];
                var sWord = t.Split(new[] { MyGlobal.sSeparatorPlus2 }, StringSplitOptions.None)[1];
                var sPosition = t.Split(new[] { MyGlobal.sSeparatorPlus2 }, StringSplitOptions.None)[2];

                int.TryParse(sLine, out var iErrLine);
                int.TryParse(sPosition, out var iPosition);

                if (iErrLine <= 0 || iErrLine > parts.Length || parts[iErrLine - 1].IndexOf(sWord, StringComparison.Ordinal) == -1)
                {
                    continue;
                }

                var iTemp0 = 0;
                var iTemp9 = 0;
                var iPosPerErrLine = parts[iErrLine - 1].IndexOf(sWord, iPosition, StringComparison.Ordinal);

                foreach (var y in parts)
                {
                    if (iErrLine - 1 == iTemp0)
                    {
                        iTemp9 += iPosPerErrLine;
                        editor.IndicatorFillRange(iPos + iTemp9, sWord.Length);

                        if (bFirst)
                        {
                            bFirst = false;
                            iCursorPostion = iPos + iTemp9;
                            iLength = sWord.Length;
                        }
                    }
                    else
                    {
                        iTemp9 += y.Length + 2;
                    }

                    iTemp0++;
                }

                //sPos = (iTemp9 + 1).ToString();
            }
        }

        private void CellViewer()
        {
            string sColumnName;
            var sColumnType = "";
            var c1Grid = GetWhichGrid();
            var sCellText = c1Grid[c1Grid.Row, c1Grid.Col].ToString();
            var sTemp = c1Grid.Splits[_SplitsNum].DisplayColumns[c1Grid.Col].ToString();

            if (sTemp.IndexOf("\r\n", StringComparison.Ordinal) != -1)
            {
                sColumnName = sTemp.Substring(0, sTemp.IndexOf("\r\n", StringComparison.Ordinal));
                sColumnType = sTemp.Substring(sTemp.IndexOf("\r\n", StringComparison.Ordinal) + 2);
            }
            else if (sTemp.IndexOf("\r", StringComparison.Ordinal) != -1)
            {
                sColumnName = sTemp.Substring(0, sTemp.IndexOf("\r", StringComparison.Ordinal));
                sColumnType = sTemp.Substring(sTemp.IndexOf("\r", StringComparison.Ordinal) + 1);
            }
            else
            {
                sColumnName = sTemp;
            }

            using (var myForm = new frmCellViewer())
            {
                myForm.sColumnName = sColumnName;
                myForm.sColumnType = sColumnType;
                myForm.sCellText = sCellText;

                var iCellViewerFormWidth = 0;
                var iCellViewerFormHeight = 0;

                sTemp = "SELECT AttributeValue FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'CellViewerFormWidth'";
                var dtData = DBCommon.ExecQuery(sTemp);

                if (dtData.Rows.Count > 0)
                {
                    int.TryParse(dtData.Rows[0][0].ToString(), out iCellViewerFormWidth);
                }

                sTemp = "SELECT AttributeValue FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'CellViewerFormHeight'";
                dtData = DBCommon.ExecQuery(sTemp);

                if (dtData.Rows.Count > 0)
                {
                    int.TryParse(dtData.Rows[0][0].ToString(), out iCellViewerFormHeight);
                }

                if (iCellViewerFormWidth > 0 && iCellViewerFormHeight > 0)
                {
                    myForm.ClientSize = new Size(iCellViewerFormWidth - 16, iCellViewerFormHeight - 38);
                }

                myForm.ShowDialog();
            }
        }

        private DataTable GetDataRowForSingleRecordViewer(C1TrueDBGrid c1Grid, DataTable dtOld, int iCurrentRow, int iTotalRows)
        {
            var dtNew = new DataTable();

            dtNew.Columns.Add("Column Name");
            dtNew.Columns.Add("Value");

            var vr = c1Grid.Splits[_SplitsNum].Rows[iCurrentRow];

            foreach (C1DataColumn col1 in c1Grid.Columns)
            {
                var _rowTemp = dtNew.NewRow();
                var sTemp = col1.Caption.IndexOf("\n", 0, StringComparison.Ordinal) != -1 ? col1.Caption.Replace("\r\n", "\n").Split('\n')[0] : col1.Caption;

                _rowTemp["Column Name"] = sTemp;
                _rowTemp["Value"] = col1.CellText(vr.DataRowIndex);

                dtNew.Rows.Add(_rowTemp);
            }

            return dtNew;
        }

        private void SingleRecordViewer()
        {
            var c1Grid = GetWhichGrid();
            var dtData = (DataTable)c1Grid.DataSource;

            var iTotalRows = dtData.Rows.Count;
            var iCurrentRow = c1TrueDBGrid1.Row;

            dtData = GetDataRowForSingleRecordViewer(c1Grid, dtData, iCurrentRow, iTotalRows);

            int iFormWidth;
            int iFormHeight;

            var sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND AttributeKey = 'GlobalConfig' AND (AttributeName = 'SingleRecordViewerFormWidth' OR AttributeName = 'SingleRecordViewerFormHeight')";
            var dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp == null || dtTemp.Rows.Count == 0)
            {
                iFormWidth = 360;
                iFormHeight = 520;
            }
            else
            {
                sSql = "AttributeName = 'SingleRecordViewerFormWidth'";
                var dtRow = dtTemp.Select(sSql);
                iFormWidth = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 360;

                sSql = "AttributeName = 'SingleRecordViewerFormHeight'";
                dtRow = dtTemp.Select(sSql);
                iFormHeight = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 520;
            }

            using (var myForm = new frmSingleRecordViewer())
            {
                myForm.iTotalQty = iTotalRows;
                myForm.iCurrent = iCurrentRow;
                myForm.dtData = dtData;
                myForm.sAccessibleDescription = AccessibleDescription;
                myForm.ClientSize = new Size(iFormWidth - 16, iFormHeight - 38);
                myForm.ShowDialog();
            }
        }

        private void UpdateFindGridList() //判斷是否要更新「搜尋清單」
        {
            if (lblFindGrid.Tag.ToString() == cboFindGrid.Text)
            {
                return;
            }

            if (cboFindGrid.Items.Count > 0 && cboFindGrid.Text == cboFindGrid.Items[0].ToString())
            {
                //搜尋的字串是第一個，不用更新
            }
            else
            {
                SaveFindList("Grid", cboFindGrid.Text); //UpdateFindGridList
            }

            lblFindGrid.Tag = cboFindGrid.Text;
        }

        private void chkShowFilterRow_Click(object sender, EventArgs e)
        {
            var c1Grid = GetWhichGrid();
            c1Grid.FilterBar = chkShowFilterRow.Checked;

            SetGridToolStripBackColor(true); //chkShowFilterRow_Click
        }

        private void chkSize_Click(object sender, EventArgs e)
        {
            if (btnAutoSize.Enabled)
            {
                btnAutoSize.PerformClick();
            }

            btnAutoSize.Enabled = !chkSize.Checked;

            SetGridToolStripBackColor(true); //chkSize_Click
        }

        private void chkSort_Click(object sender, EventArgs e)
        {
            if (btnAutoSort.Enabled)
            {
                btnAutoSort.PerformClick();
            }

            btnAutoSort.Enabled = !chkSort.Checked;

            SetGridToolStripBackColor(true); //chkSort_Click
        }

        private void chkShowColumnDataType_Click(object sender, EventArgs e)
        {
            var c1Grid = GetWhichGrid();

            if (c1Grid == null)
            {
                c1DockingTab1.SelectedTab = tabDataGrid;
                c1Grid = GetWhichGrid();
            }

            GridZoom(c1Grid); //chkShowColumnDataType_Click
            SetGridToolStripBackColor(true); //chkShowColumnDataType_Click
            ChangeBackColor(_cToolstripFocused);
        }

        private void btnFreezeColumn_Click(object sender, EventArgs e)
        {
            FrozenColumn(); //按鈕
        }

        private void btnAutoSize_Click(object sender, EventArgs e)
        {
            var c1Grid = GetWhichGrid();

            foreach (C1DisplayColumn col in c1Grid.Splits[_SplitsNum].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 2000;
                }

                if (!"`500`1000`1500`2000`".Contains("`" + MyGlobal.sGridMaxWidth + "`"))
                {
                    continue;
                }

                if (col.Width > Convert.ToInt16(MyGlobal.sGridMaxWidth))
                {
                    col.Width = Convert.ToInt16(MyGlobal.sGridMaxWidth);
                }
            }

            c1Grid.Refresh();
        }

        private void btnAutoSort_Click(object sender, EventArgs e)
        {
            var sHeader = "";
            string[] sArraySeparators = { "`" };
            var dtSortedData = new DataTable();

            Cursor = Cursors.WaitCursor;

            var c1Grid = GetWhichGrid();
            var dt = (DataTable)c1Grid.DataSource;

            for (var i = 0; i < dt.Columns.Count; i++)
            {
                sHeader += dt.Columns[i].ColumnName + "`";
            }

            //ASCII排序, 20220514 修正字母大小寫被分別排序的 bug
            var sArrayHeader = sHeader.Split(sArraySeparators, StringSplitOptions.RemoveEmptyEntries);
            //Array.Sort(sArrayHeader, string.CompareOrdinal);

            if (btnAutoSort.Tag?.ToString() == "1")
            {
                btnAutoSort.Tag = "0";
                sArrayHeader = sArrayHeader.OrderByDescending(c => c).ToArray();
            }
            else
            {
                btnAutoSort.Tag = "1";
                sArrayHeader = sArrayHeader.OrderBy(c => c).ToArray();
            }

            for (var i = 0; i < dt.Columns.Count; i++)
            {
                dtSortedData.Columns.Add(sArrayHeader[i], typeof(string));
            }

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var rowData = dtSortedData.NewRow();

                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    rowData[dt.Columns[j].ColumnName] = dt.Rows[i][dt.Columns[j].ColumnName].ToString();
                }

                dtSortedData.Rows.Add(rowData);
            }

            c1Grid.DataSource = dtSortedData;
            c1Grid.Show();

            if (MyLibrary.sGridNullShowAs.ToUpper() != "NONE")
            {
                var s1 = new Style {ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridNullShowColor)};

                for (var i = 0; i < c1Grid.Columns.Count; i++)
                {
                    //套用「使用者指定的 NULL」顯示格式
                    c1Grid.Splits[_SplitsNum].DisplayColumns[i].AddRegexCellStyle(CellStyleFlag.AllCells, s1, MyLibrary.sGridNullShowAs);
                }
            }

            Cursor = Cursors.Default;
            c1Grid.Cursor = Cursors.Default;

            if (!chkSize.Checked)
            {
                return;
            }

            foreach (C1DisplayColumn col in c1Grid.Splits[_SplitsNum].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 2000;
                }

                if ("`500`1000`1500`2000`".Contains("`" + MyGlobal.sGridMaxWidth + "`") != true)
                {
                    continue;
                }

                if (col.Width > Convert.ToInt16(MyGlobal.sGridMaxWidth))
                {
                    col.Width = Convert.ToInt16(MyGlobal.sGridMaxWidth);
                }
            }

            c1Grid.Refresh();
        }

        private void c1DockingTab1_Enter(object sender, EventArgs e)
        {
            HideACGrid();
            ChangeBackColor(_cToolstripFocused);
        }

        private void c1DockingTab1_Leave(object sender, EventArgs e)
        {
            ChangeBackColor(_cToolstripUnfocused);
        }

        private void tabMessage_Enter(object sender, EventArgs e)
        {
            ChangeBackColor(_cToolstripFocused);
        }

        private void tabMessage_Leave(object sender, EventArgs e)
        {
            tsDataGrid.BackColor = _cToolstripUnfocused;
        }

        private void tabSQLHistory_Enter(object sender, EventArgs e)
        {
            ChangeBackColor(_cToolstripFocused);
        }

        private void tabSQLHistory_Leave(object sender, EventArgs e)
        {
            ChangeBackColor(_cToolstripUnfocused);
        }

        private void tabDataGrid_Enter(object sender, EventArgs e)
        {
            ChangeBackColor(_cToolstripFocused);
        }

        private void tabDataGrid_Leave(object sender, EventArgs e)
        {
            ChangeBackColor(_cToolstripUnfocused);
        }

        private void btnExportToCSV_Click(object sender, EventArgs e)
        {
            ArrangeData4AllData("ExportAllDataToCSV"); //按鈕
        }

        private void editor_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0 && c1GridAC4Period1.Visible)
            {
                c1GridAC4Period1.Visible = false;
            }
            else if (e.Delta != 0 && c1GridAC4Space1.Visible)
            {
                c1GridAC4Space1.Visible = false;
            }
        }

        private void editor_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCursor(); //editor_MouseMove
        }

        private void c1GridARInfo_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCursor("AR"); //c1GridARInfo_MouseMove
        }

        private void c1GridSchemaBrowser_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCursor("SchemaBrowser"); //c1GridSchemaBrowser_MouseMove
        }

        private void editorMessage_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCursor(); //editorMessage_MouseMove
        }

        private void c1TrueDBGrid1_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCursor(); //c1TrueDBGrid1_MouseMove
        }

        private void editorSQLHistory_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCursor(); //editorSQLHistory_MouseMove
        }

        private void tsEditor_MouseMove(object sender, MouseEventArgs e)
        {
            HideACGrid();
            UpdateCursor(); //tsEditor_MouseMove
        }

        private void tsDataGrid_MouseMove(object sender, MouseEventArgs e)
        {
            HideACGrid();
            UpdateCursor(); //tsDataGrid_MouseMove
        }

        private void splitContainer2_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCursor(); //splitContainer2_MouseMove
        }

        private void UpdateCursor(string sControl = "")
        {
            if (_bBusy)
            {
                Cursor = Cursors.WaitCursor;
            }
            else
            {
                if (Cursor != Cursors.Default)
                {
                    Cursor = Cursors.Default;
                    Application.UseWaitCursor = false;
                    c1TrueDBGrid1.Cursor = Cursors.Default;
                }

                switch (sControl)
                {
                    case "AR":
                        c1GridARInfo.Cursor = Cursors.Default;
                        break;
                    case "SchemaBrowser":
                        c1GridSchemaBrowser.Cursor = Cursors.Default;
                        break;
                }
            }
        }

        private void btnExportToFile_Click(object sender, EventArgs e)
        {
            ExportToFile(); //btnExportToFile_Click
        }

        private void tmrExecTime_Tick(object sender, EventArgs e)
        {
            var sExecTime = MyGlobal.DateDiff(_dtStartTime, DateTime.Now);
            lblExecTime.Text = _sExecTime + " " + sExecTime;
        }

        private void tmrQueryTime_Tick(object sender, EventArgs e)
        {
            var sQueryTime = MyGlobal.DateDiff(_dtStartTime, DateTime.Now);
            lblQueryTime.Text = _sQueryTime + " " + sQueryTime;
        }

        private void c1GridARInfo_AfterUpdate(object sender, EventArgs e)
        {
            UpdateAutoReplaceDictionary(); //c1GridARInfo_AfterUpdate
        }

        private void UpdateAutoReplaceDictionary()
        {
            _dicAutoReplace = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < c1GridARInfo.Splits[_iii].Rows.Count; i++)
            {
                var sKeyword = c1GridARInfo[c1GridARInfo.Splits[_iii].Rows[i].DataRowIndex, (int)eColAR.Keyword].ToString();
                var sReplacement = c1GridARInfo[c1GridARInfo.Splits[_iii].Rows[i].DataRowIndex, (int)eColAR.Replacement].ToString();

                if (!string.IsNullOrEmpty(sKeyword.Trim()) && !string.IsNullOrEmpty(sReplacement.Trim()))
                {
                    _dicAutoReplace.Add(sKeyword, sReplacement);
                }
            }
        }

        private void c1TrueDBGrid1_Click(object sender, EventArgs e)
        {
            var c1Grid = GetWhichGrid();

            if ((ModifierKeys & Keys.Shift) == 0)
            {
                return;
            }

            if (_iShiftMouseLeftDownX == -1 || _iShiftMouseLeftDownY == -1)
            {
                return;
            }

            int iTemp;
            int iY;

            if (_iShiftMouseLeftDownY < c1Grid.Row)
            {
                iTemp = c1Grid.Row;
                c1Grid.Row = _iShiftMouseLeftDownY;
                _iShiftMouseLeftDownY = iTemp;
                iY = _iShiftMouseLeftDownY;
            }
            else
            {
                iY = c1Grid.Row;
            }

            var iRow = c1Grid.Row;
            var iRowCount = _iShiftMouseLeftDownY - c1Grid.Row + 1;

            int iX;

            if (_iShiftMouseLeftDownX < c1Grid.Col)
            {
                iTemp = c1Grid.Col;
                c1Grid.Col = _iShiftMouseLeftDownX;
                _iShiftMouseLeftDownX = iTemp;
                iX = _iShiftMouseLeftDownX;
            }
            else
            {
                iX = c1Grid.Col;
            }

            var iCol = c1Grid.Col;
            var iColCount = _iShiftMouseLeftDownX - c1Grid.Col + 1;

            c1Grid.Select(iRow, iCol, iRowCount, iColCount, false);
            c1Grid.SetActiveCell(iY, iX);

            _iShiftMouseLeftDownX = -1;
            _iShiftMouseLeftDownY = -1;
        }

        private void ExportToFile()
        {
            using (var myForm = new frmExportToFile())
            {
                Cursor = Cursors.WaitCursor;
                var c1Grid = GetWhichGrid();
                string sHeader;
                var dtData = new DataTable();
                var dt = (DataTable)c1Grid.DataSource;

                foreach (C1DataColumn col1 in c1Grid.Columns)
                {
                    sHeader = col1.Caption;

                    switch ((col1.Tag ?? string.Empty).ToString())
                    {
                        case "int":
                            dtData.Columns.Add(sHeader, typeof(int));
                            break;
                        case "number":
                            dtData.Columns.Add(sHeader, typeof(decimal));
                            break;
                        default:
                            dtData.Columns.Add(sHeader, typeof(string));
                            break;
                    }
                }

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rowData = dtData.NewRow();

                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        sHeader = dt.Columns[j].ColumnName;

                        if (dt.Rows[i][dt.Columns[j].ColumnName].ToString() == MyLibrary.sGridNullShowAs)
                        {
                            rowData[sHeader] = DBNull.Value;
                        }
                        else
                        {
                            rowData[sHeader] = dt.Rows[i][dt.Columns[j].ColumnName];
                        }
                    }

                    dtData.Rows.Add(rowData);
                    Application.DoEvents();
                }

                Cursor = Cursors.Default;
                myForm.dtData = dtData;
                myForm.dtSchemaTable = _dtSchemaTableExport;
                myForm.sFontName = c1Grid.Font.Name;
                myForm.fFontSize = c1Grid.Font.Size;
                myForm.ShowDialog();
            }
        }

        private static void MoveDialogWhenOpened(string windowCaption, int iX, int iY)
        {
            //取得螢幕解析度
            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;

            if (iWidth - iX < 300)
            {
                MyGlobal.iCloseDialogX = 400; //游標很靠近螢幕的右側
            }

            if (iHeight - iY < 300)
            {
                MyGlobal.iCloseDialogY = -300; //游標很靠近螢幕的下方
            }

            var location = new Point();
            location.Offset(iX, iY);

            var argument = new object[] { windowCaption, location };

            //using System.ComponentModel;
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += MoveDialogThread;
            backgroundWorker.RunWorkerAsync(argument);
        }

        private void tmrlblInfo_Tick(object sender, EventArgs e)
        {
            //20201105
            if (btnCancelQuery.Enabled && tmrExecTime.Enabled == false && tmrQueryTime.Enabled == false)
            {
                btnCancelQuery.Enabled = false; //20200427
            }

            if (lblInfo.Text != (lblInfo.Tag ?? string.Empty).ToString())
            {
                lblInfo.Tag = lblInfo.Text;
                c1StatusBar1.Tag = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }

            //1分鐘後，將 lblInfo.Text 清空！
            if (string.IsNullOrEmpty((c1StatusBar1.Tag ?? string.Empty).ToString()))
            {
                return;
            }

            if (MyGlobal.DateDiff(Convert.ToDateTime(c1StatusBar1.Tag.ToString()),
                Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")), "Minutes") != "01")
            {
                return;
            }

            lblInfo.Text = "";
            lblInfo.Tag = "";
            c1StatusBar1.Tag = "";
        }

        private void editorMessage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            _cMenuMessageEditor.Items[0].Enabled = !string.IsNullOrEmpty(editorMessage.Text);

            //Copy: 判斷是否有選取文字，決定功能表項目可不可用
            _cMenuMessageEditor.Items[2].Enabled = !string.IsNullOrEmpty(editorMessage.SelectedText);

            editorMessage.ContextMenuStrip = _cMenuMessageEditor;

            if (MyLibrary.bDarkMode)
            {
                _cMenuMessageEditor.BackColor = ColorTranslator.FromHtml("#2D2D30");
                _cMenuMessageEditor.ForeColor = Color.White;
                _cMenuMessageEditor.RenderMode = ToolStripRenderMode.System;
                //_cMenuMessageEditor.ShowImageMargin = false;
            }

            _cMenuMessageEditor.Show(editorMessage, new Point(e.X, e.Y));
        }

        private void editorSQLHistory_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            _cMenuSQLHistoryEditor.Items[0].Enabled = !string.IsNullOrEmpty(editorSQLHistory.Text);

            //Copy: 判斷是否有選取文字，決定功能表項目可不可用
            _cMenuSQLHistoryEditor.Items[2].Enabled = !string.IsNullOrEmpty(editorSQLHistory.SelectedText);

            editorSQLHistory.ContextMenuStrip = _cMenuSQLHistoryEditor;

            if (MyLibrary.bDarkMode)
            {
                _cMenuSQLHistoryEditor.BackColor = ColorTranslator.FromHtml("#2D2D30");
                _cMenuSQLHistoryEditor.ForeColor = Color.White;
                _cMenuSQLHistoryEditor.RenderMode = ToolStripRenderMode.System;
                //_cMenuSQLHistoryEditor.ShowImageMargin = false;
            }

            _cMenuSQLHistoryEditor.Show(editorSQLHistory, new Point(e.X, e.Y));
        }

        private void c1DockingTab1_SelectedTabChanged(object sender, EventArgs e)
        {
            //20201203 此處改成不要帶入 true，因為偶爾會出現「執行異動指令後，觸發 {TAB}，造成指令變成 {TAB}」
            SetDockingTabControl(); //c1DockingTab1_SelectedTabChanged
        }

        private void SetDockingTabControl(bool bSendTabKey = false)
        {
            var bValue = false;

            if ("`tabMessage`tabSQLHistory`".Contains("`" + c1DockingTab1.SelectedTab.Name + "`") == false)
            {
                foreach (var ctl in c1DockingTab1.SelectedTab.Controls)
                {
                    if (!(ctl is C1TrueDBGrid c1Grid))
                    {
                        continue;
                    }

                    var dt = (DataTable)c1Grid.DataSource;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        bValue = true;
                    }

                    c1Grid.Focus();
                }
            }

            //tsDataGrid.Enabled = bValue; //因為要單獨控制 btnOptions，所以每個要獨立設定
            btnExportToFile.Enabled = bValue;
            btnFreezeColumn.Enabled = bValue;
            btnShowSQL.Enabled = bValue;
            btnAutoSort.Enabled = bValue;
            lblFindGrid.Enabled = bValue;
            btnFindNextGrid.Enabled = !string.IsNullOrEmpty(cboFindGrid.Text);
            btnFindPreviousGrid.Enabled = !string.IsNullOrEmpty(cboFindGrid.Text);
            btnCountGrid.Enabled = !string.IsNullOrEmpty(cboFindGrid.Text);
            btnHighlightAllGrid.Enabled = !string.IsNullOrEmpty(cboFindGrid.Text);
            btnClearHighlightsGrid.Enabled = !string.IsNullOrEmpty(cboFindGrid.Text);
            btnOptions.Enabled = true;

            //變更工具列按鈕的 Enable 狀態
            chkShowFilterRow.Enabled = bValue;
            chkSize.Enabled = c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count != 0 && !chkRawDataMode.Checked;
            btnAutoSize.Enabled = c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count != 0 && !chkSize.Enabled;
            chkSort.Enabled = bValue;
            cboFindGrid.Enabled = bValue;
            cboResultCopyQuotingWith.Enabled = bValue;
            cboResultCopyFieldSeparator.Enabled = bValue;
        }

        private void tmrlblInfoEditor_Tick(object sender, EventArgs e)
        {
            if (lblInfoEditor.Text != (lblInfoEditor.Tag ?? string.Empty).ToString())
            {
                lblInfoEditor.Tag = lblInfoEditor.Text;
                c1StatusBar2.Tag = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }

            //1分鐘後，將 lblInfo.Text 清空！
            if ((c1StatusBar2.Tag ?? string.Empty).ToString() == string.Empty)
            {
                return;
            }

            if (MyGlobal.DateDiff(Convert.ToDateTime(c1StatusBar2.Tag.ToString()),
                Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")), "Minutes") != "01")
            {
                return;
            }

            SetLabelInfoEditor("", Color.Black);
            lblInfoEditor.Tag = "";
            c1StatusBar2.Tag = "";
        }

        private void SetLabelInfoEditor(string sMsg, Color color)
        {
            var sColor = "#DFE9F5";

            if (MyLibrary.bDarkMode)
            {
                sColor = "#333333";
            }

            lblInfoEditor.Text = sMsg;
            lblInfoEditor.ForeColor = color;
            lblInfoEditor.BackColor = ColorTranslator.FromHtml(sColor);
        }

        private void UpdateStatusBarInfo(string sText = "")
        {
            lblInfo.Tag = sText;
            lblInfo.Text = sText;
        }

        private void c1TrueDBGrid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var c1Grid = GetWhichGrid();

            var iRow = c1Grid.RowContaining(e.Y);

            if (iRow != -1)
            {
                CellViewer();
            }
        }

        private void c1TrueDBGrid1_MouseUp(object sender, MouseEventArgs e)
        {
            var iDisplayColIndex = c1TrueDBGrid1.ColContaining(e.X);
            var iDisplayRowIndex = c1TrueDBGrid1.RowContaining(e.Y);

            if (iDisplayColIndex == -1 && iDisplayRowIndex == -1)
            {
                return;
            }

            c1TrueDBGrid1.Row = iDisplayRowIndex;
            c1TrueDBGrid1.Col = iDisplayColIndex;

            CalculateCells(); //c1TrueDBGrid1_MouseUp
        }

        private void CalculateCells()
        {
            double dSummary = 0;
            var c1Grid = GetWhichGrid();
            var selCol = c1Grid.SelectedCols.Count;
            var bSelectedWholeColumn = c1Grid.SelectedRows.Count == 0 && c1Grid.SelectedCols.Count > 0;
            int iCount = 0, iCountAverage = 0;
            bool bTryParse;

            if (c1Grid.DataSource == null)
            {
                return;
            }

            if (bSelectedWholeColumn) //整欄選取
            {
                for (var row = 0; row < c1Grid.Splits[_SplitsNum].Rows.Count; row++)
                {
                    foreach (var col in c1Grid.SelectedCols)
                    {
                        bTryParse = double.TryParse(c1Grid.Columns[col.ToString()].CellText(row), out var fValue);
                        dSummary += fValue;

                        if (!string.IsNullOrEmpty(c1Grid.Columns[col.ToString()].CellText(row)))
                        {
                            iCount++;
                        }

                        iCountAverage = bTryParse ? iCountAverage + 1 : iCountAverage;
                    }
                }
            }
            else //非整欄選取
            {
                foreach (int row in c1Grid.SelectedRows)
                {
                    var vr = c1Grid.Splits[_SplitsNum].Rows[row];

                    if (selCol == 0) //整列選取
                    {
                        foreach (C1DataColumn col1 in c1Grid.Columns)
                        {
                            bTryParse = double.TryParse(col1.CellText(vr.DataRowIndex), out var fValue);
                            dSummary += fValue;

                            if (!string.IsNullOrEmpty(col1.CellText(vr.DataRowIndex)))
                            {
                                iCount++;
                            }

                            iCountAverage = bTryParse ? iCountAverage + 1 : iCountAverage;
                        }
                    }
                    else
                    {
                        foreach (C1DataColumn col1 in c1Grid.SelectedCols)
                        {
                            bTryParse = double.TryParse(col1.CellText(vr.DataRowIndex), out var fValue);
                            dSummary += fValue;

                            if (!string.IsNullOrEmpty(col1.CellText(vr.DataRowIndex)))
                            {
                                iCount++;
                            }

                            iCountAverage = bTryParse ? iCountAverage + 1 : iCountAverage;
                        }
                    }
                }
            }

            var bShow13 = true;
            var bShow2 = true;

            if (iCount == 0 && ((DataTable)c1Grid.DataSource).Rows.Count > 0) //單一儲存格，且有資料
            {
                //double.TryParse(c1Grid.Columns[c1Grid.Col].CellText(c1Grid.Row), out var fValue);
                //dSummary += fValue;
                //iCount++;
                //iCountAverage++;

                //使用者只有點到某一儲存格
                bShow2 = false;
                bShow13 = false;
            }

            if (bShow13 && dSummary.Equals(0) && iCountAverage.Equals(0))
            {
                bShow13 = false;
            }

            lblSummaryValue.Text = dSummary.ToString("###,##0.####"); //###,##0.################
            lblCountValue.Text = iCount.ToString();
            lblAverageValue.Text = dSummary == 0 ? "0" : (dSummary / iCountAverage).ToString("###,##0.####"); //###,##0.#####

            //20230707 判斷是否要顯示以下物件
            lblAverage.Visible = bShow13;
            lblAverageValue.Visible = bShow13;
            lblCount.Visible = bShow2;
            lblCountValue.Visible = bShow2;
            lblSummary.Visible = bShow13;
            lblSummaryValue.Visible = bShow13;
            lblSeparator1.Visible = bShow13;
            lblSeparator2.Visible = bShow13;
            lblSeparator3.Visible = bShow13 || bShow2;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            //取得螢幕解析度
            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;
            var iXTemp = Cursor.Position.X;
            var iYTemp = Cursor.Position.Y;
            var iX = 0;
            var iY = 0;

            if (iXTemp < 300)
            {
                iX = iXTemp - 25;
            }

            if (iWidth - iXTemp < 300)
            {
                iX = 320; //游標很靠近螢幕的右側
            }

            if (iHeight - iYTemp < 300)
            {
                iY = -300; //游標很靠近螢幕的下方
            }

            _sLangText = MyGlobal.GetLanguageString("\"Auto Replace\": Keyword binding for favorite queries.", "form", Name, "msg", "Help_AutoReplace1", "Text");
            _sLangText += "\r\n\r\n" + MyGlobal.GetLanguageString("After entering the \"Keyword\", press the blank key to replace the \"Keyword\" with the \"Replacement\".", "form", Name, "msg", "Help_AutoReplace2", "Text");
            _sLangText += "\r\n\r\n" + MyGlobal.GetLanguageString("Press Ctrl+Z to restore \"Replacement\" to \"Keyword\".", "form", Name, "msg", "Help_AutoReplace3", "Text");

            FindAndMoveMsgBox(Cursor.Position.X - iX - 30, Cursor.Position.Y + iY + 30, true, "JasonQuery");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void splitContainer1_KeyPress(object sender, KeyPressEventArgs e)
        {
            editor.Focus(); //Focus 移回 SQL Editor (Enter 鍵)
        }

        private void splitContainer1_KeyDown(object sender, KeyEventArgs e)
        {
            editor.Focus(); //Focus 移回 SQL Editor (上/下/左/右 鍵)
        }

        private void UpdateMessage(string sMsg)
        {
            editorMessage.ReadOnly = false;
            editorMessage.Text = sMsg;
            editorMessage.ReadOnly = true;
        }

        private void ChangeBackColor(Color cColor)
        {
            tsDataGrid.BackColor = cColor;
            chkShowFilterRow.BackColor = cColor;
            chkSize.BackColor = cColor;
            chkSort.BackColor = cColor;
            chkShowColumnType.BackColor = cColor;
            chkRawDataMode.BackColor = cColor;
            chkShowGroupingRow.BackColor = cColor;
        }

        private static bool IsRealSymbol(string sChar)
        {
            sChar = sChar.Substring(0, 1);
            var cChar = Convert.ToChar(sChar);

            var bResult = char.IsSymbol(cChar) || char.IsPunctuation(cChar) || char.IsControl(cChar) || char.IsSeparator(cChar) || char.IsWhiteSpace(cChar);

            return bResult;
        }

        private C1TrueDBGrid GetWhichGrid()
        {
            var bBreak = false;
            const int iQueryIndex = 1;
            C1TrueDBGrid c1Grid = null;

            foreach (var ctl in c1DockingTab1.SelectedTab.Controls)
            {
                if (ctl is C1TrueDBGrid grid)
                {
                    c1Grid = grid;
                }
            }

            if (c1Grid != null)
            {
                return c1Grid;
            }

            foreach (Control tab in c1DockingTab1.TabPages)
            {
                var tabPage = (C1DockingTabPage)tab;

                foreach (var ctrlTab in tabPage.Controls)
                {
                    if (ctrlTab.GetType().Name != "C1TrueDBGrid")
                    {
                        continue;
                    }

                    var sTemp = ((C1TrueDBGrid)ctrlTab).Name;

                    if (sTemp != "c1TrueDBGrid" + iQueryIndex)
                    {
                        continue;
                    }

                    c1Grid = (C1TrueDBGrid)ctrlTab;
                    bBreak = true;
                    break;
                }

                if (bBreak)
                    break;
            }

            return c1Grid;
        }

        private void btnHelp_RawDataMode_Click(object sender, EventArgs e)
        {
            //取得螢幕解析度
            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;

            var iXTemp = Cursor.Position.X;
            var iYTemp = Cursor.Position.Y;
            var iX = 0;
            var iY = 0;

            if (iXTemp < 300)
            {
                iX = iXTemp - 25;
            }

            if (iWidth - iXTemp < 300)
            {
                iX = 320; //游標很靠近螢幕的右側
            }

            if (iHeight - iYTemp < 300)
            {
                iY = -300; //游標很靠近螢幕的下方
            }

            _sLangText = MyGlobal.GetLanguageString("\"Raw Data Mode\": It can speed up data display greatly.", "Global", "Global", "msg", "Help_RawDataMode1", "Text");
            _sLangText += "\r\n\r\n" + MyGlobal.GetLanguageString("When you enable \"Raw Data Mode\", the following settings will not be applied to the presentation of the data table:", "Global", "Global", "msg", "Help_RawDataMode2", "Text");
            _sLangText += "\r\n" + "1. " + MyGlobal.GetLanguageString("Date Format", "Global", "Global", "msg", "Help_RawDataMode3", "Text");
            _sLangText += "\r\n" + "2. " + MyGlobal.GetLanguageString("Null Value Style", "Global", "Global", "msg", "Help_RawDataMode4", "Text");
            _sLangText += "\r\n" + "3. " + MyGlobal.GetLanguageString("Show Column Type", "form", Name, "object", "chkShowColumnType", "Text");
            _sLangText += "\r\n" + "4. " + MyGlobal.GetLanguageString("Automatically Resize Column Width according to Data Result", "form", Name, "object", "chkSize", "ToolTipText");

            if (chkSort.Visible)
            {
                _sLangText += "\r\n" + "5. " + MyGlobal.GetLanguageString("Automatically Sort Data Result according to Column Header", "form", Name, "object", "chkSort", "ToolTipText");
            }

            FindAndMoveMsgBox(Cursor.Position.X - iX - 30, Cursor.Position.Y + iY + 30, true, "JasonQuery");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkRawDataMode_Click(object sender, EventArgs e)
        {
            ChangeBackColor(_cToolstripFocused);
        }

        private void chkRawDataMode_CheckedChanged(object sender, EventArgs e)
        {
            chkShowColumnType.Enabled = !chkRawDataMode.Checked; //20220421 取消 c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count != 0 條件
            chkSize.Enabled = c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count != 0 && !chkRawDataMode.Checked;
            btnAutoSize.Enabled = c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count != 0 && !chkSize.Enabled;
        }

        private void chkShowGroupingRow_CheckedChanged(object sender, EventArgs e)
        {
            c1TrueDBGrid1.DataView = chkShowGroupingRow.Checked ? DataViewEnum.GroupBy : DataViewEnum.Normal;
        }

        private void chkShowGroupingRow_Click(object sender, EventArgs e)
        {
            ChangeBackColor(_cToolstripFocused);
        }

        private void btnPagination_Click(object sender, EventArgs e)
        {
            btnPaginationOn.Visible = !btnPaginationOn.Visible;
            btnPaginationOff.Visible = !btnPaginationOn.Visible;

            //下一頁按鈕不在此處控制！
            //btnNextPage.Enabled = btnPaginationOn.Visible;
        }

        private void btnAppendingQueries_Click(object sender, EventArgs e)
        {
            btnAppendingQueriesOn.Visible = !btnAppendingQueriesOn.Visible;
            btnAppendingQueriesOff.Visible = !btnAppendingQueriesOn.Visible;
        }

        private void btnShowSQL_Click(object sender, EventArgs e)
        {
            var c1Grid = GetWhichGrid();

            var sCellText = c1Grid.AccessibleDescription;

            using (var myForm = new frmSQLStatementViewer())
            {
                myForm.sCellText = sCellText;

                var iSQLStatementViewerFormWidth = 0;
                var iSQLStatementViewerFormHeight = 0;

                var sTemp = "SELECT AttributeValue FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'SQLStatementViewerFormWidth'";
                var dtData = DBCommon.ExecQuery(sTemp);

                if (dtData.Rows.Count > 0)
                {
                    int.TryParse(dtData.Rows[0][0].ToString(), out iSQLStatementViewerFormWidth);
                }

                sTemp = "SELECT AttributeValue FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'SQLStatementViewerFormHeight'";
                dtData = DBCommon.ExecQuery(sTemp);

                if (dtData.Rows.Count > 0)
                {
                    int.TryParse(dtData.Rows[0][0].ToString(), out iSQLStatementViewerFormHeight);
                }

                if (iSQLStatementViewerFormWidth > 0 && iSQLStatementViewerFormHeight > 0)
                {
                    myForm.ClientSize = new Size(iSQLStatementViewerFormWidth - 16, iSQLStatementViewerFormHeight - 38);
                }

                myForm.ShowDialog();
            }
        }

        private void c1TrueDBGrid1_Scroll(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            var oldoffset = c1TrueDBGrid1.Splits[_SplitsNum].VerticalOffset;

            if (btnNextPage.Enabled == false || _iLastTimeOffset == oldoffset)
            {
                //水平移動
                return;
            }

            if ((oldoffset + (300 * _dNext)) >= _iLastRowOffset) //if (oldoffset == _iLastRowOffset)
            {
                NextPage(c1TrueDBGrid1.Col, c1TrueDBGrid1.Splits[_SplitsNum].Rows.Count - 1);
            }
            
            //已滾動到底了
            //c1TrueDBGrid1.Splits[_SplitsNum].HorizontalOffset = oldoffset;            

            _iLastTimeOffset = oldoffset;
        }

        private static DialogResult InputBox(string sTitle, string sPromptText, string sPromptText2, int iX, int iY, ref string sValue, ref bool bAutoOpenFile)
        {
            var form = new Form();
            var lblPromptText = new Label();
            var lblTemp = new Label();
            var lblTemp1 = new Label();
            var txtInputBox = new TextBox();
            var btnOK = new C1Button();
            var btnCancel = new C1Button();
            var chkAutoOpenFile = new CheckBox();

            form.Text = sTitle;
            form.ClientSize = new Size(396, 152);
            form.Controls.AddRange(new Control[] { lblPromptText, lblTemp, txtInputBox, btnOK, btnCancel, chkAutoOpenFile });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.Font = new Font("Microsoft JhengHei", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);

            lblPromptText.Text = sPromptText;
            txtInputBox.Text = sValue;
            chkAutoOpenFile.Text = sPromptText2;

            var sName = MyGlobal.GetLanguageString("&OK", "Global", "Global", "messagebox", "OK", "Text");
            btnOK.Text = sName;
            sName = MyGlobal.GetLanguageString("&Cancel", "Global", "Global", "messagebox", "Cancel", "Text");
            btnCancel.Text = sName;

            btnOK.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;

            lblPromptText.SetBounds(14, 15, 372, 13);
            chkAutoOpenFile.SetBounds(17, 65, 372, 20);
            btnOK.SetBounds(215, 100, 75, 37);
            btnCancel.SetBounds(304, 100, 74, 37);

            lblTemp.SetBounds(12, 65, 372, 13);
            lblTemp.AutoSize = true;
            lblTemp.Text = sValue;
            lblTemp.Visible = false;

            lblTemp1.SetBounds(0, 0, 372, 13);
            lblTemp1.AutoSize = true;
            lblTemp1.Text = sTitle;
            lblTemp1.Visible = false;

            lblPromptText.AutoSize = true;
            chkAutoOpenFile.AutoSize = true;
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(Math.Max(lblPromptText.Right + 20, Math.Max(chkAutoOpenFile.Right + 20, lblTemp1.Width + 20)), form.ClientSize.Height);
            txtInputBox.SetBounds(15, 36, form.Width - 50, 20);

            txtInputBox.TextChanged += delegate
            {
                btnOK.Enabled = !string.IsNullOrEmpty(txtInputBox.Text.Trim());
            };

            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(iX - lblTemp.Width - 25, iY - 76);

            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = btnOK;
            form.CancelButton = btnCancel;

            var dialogResult = form.ShowDialog();
            sValue = txtInputBox.Text;
            bAutoOpenFile = chkAutoOpenFile.Checked;
            return dialogResult;
        }

        private string GetTableNameFromSQL(string sSQL_Original)
        {
            var sTableName = "";
            var sSql = sSQL_Original;

            //去除「區段註解」
            for (var i = 0; i < 100; i++)
            {
                var iIndexOf1 = sSql.IndexOf("/*", StringComparison.Ordinal);
                var iIndexOf2 = sSql.IndexOf("*/", StringComparison.Ordinal);

                if (iIndexOf1 == -1 || iIndexOf2 == -1)
                {
                    break;
                }

                if (iIndexOf2 > iIndexOf1)
                {
                    sSql = sSql.Substring(0, sSql.IndexOf("/*", StringComparison.Ordinal)) + sSql.Substring(sSql.IndexOf("*/", StringComparison.Ordinal) + 2, sSql.Length - sSql.IndexOf("*/", StringComparison.Ordinal) - 2);
                }
            }

            var parts = sSql.Split(new[] { "\r\n" }, StringSplitOptions.None);

            //去除整列空白，或是包含「單列註解」
            for (var i = 0; i < parts.Length; i++)
            {
                if (parts[i].Length < 2)
                {
                    parts[i] = parts[i].Trim();
                }
                else if (parts[i].Substring(0, 2) == "--")
                {
                    parts[i] = "";
                }
                else if (parts[i].IndexOf("--", StringComparison.Ordinal) != -1)
                {
                    parts[i] = parts[i].Substring(0, parts[i].IndexOf("--", StringComparison.Ordinal));
                }
            }

            //重新組成單獨一列的 SQL 指令
            sSql = parts.TakeWhile((t, i) => i <= 300).Aggregate("", (current, t) => current + (string.IsNullOrEmpty(t) ? "" : t + " "));

            //去除多餘空白，方便等一下定位 " FROM "
            for (var i = 1; i <= 20; i++)
            {
                sSql = sSql.ToUpper().Replace("  ", " ");
            }

            var iIndexOf = sSql.IndexOf(" FROM ", StringComparison.Ordinal);

            if (iIndexOf == -1)
            {
                sTableName = "unknow_table_name"; //找不到 TableName
            }
            else
            {
                for (var k = iIndexOf + 6; k < sSql.Length; k++)
                {
                    if (sSql.Substring(k, 1) != " ")
                    {
                        sTableName += sSql.Substring(k, 1);
                    }
                    else
                    {
                        break;
                    }
                }

                if (string.IsNullOrEmpty(sTableName))
                {
                    return sTableName;
                }

                //還原大小寫
                iIndexOf = sSQL_Original.ToUpper().IndexOf(sTableName, StringComparison.Ordinal);
                sTableName = sSQL_Original.Substring(iIndexOf, sTableName.Length);
            }

            return sTableName;
        }

        private void DisconnectDatabase()
        {
            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    MyGlobal.oOracleReader.oDisconnect();
                    break;
                case "PostgreSQL":
                    MyGlobal.oPostgreReader.oDisconnect();
                    break;
                case "SQL Server":
                    MyGlobal.oSQLServerReader.oDisconnect();
                    break;
                case "MySQL":
                    MyGlobal.oMySQLReader.oDisconnect();
                    break;
                case "SQLite":
                    MyGlobal.oSQLiteReader.oDisconnect();
                    break;
                case "SQLCipher":
                    MyGlobal.oSQLCipherReader.oDisconnect();
                    break;
            }

            UpdateNotCommitYetInfo(""); //DisconnectDatabase, 清除「尚未 Commit/Rollback」訊息
        }

        private void mnuResultCopyQuotingWith_Click(object sender, EventArgs e)
        {
            var mnu = sender as ToolStripMenuItem;

            mnuResultCopyQuotingWithNone.Checked = false;
            mnuResultCopyQuotingWithDoubleQuoting.Checked = false;
            mnuResultCopyQuotingWithSingleQuoting.Checked = false;
            mnu.Checked = true;

            switch (mnu.Tag.ToString())
            {
                case "None":
                    mnuResultCopyQuotingWith.Tag = "None";
                    break;
                case "\"":
                    mnuResultCopyQuotingWith.Tag = "\"";
                    break;
                case "'":
                    mnuResultCopyQuotingWith.Tag = "'";
                    break;
            }
        }

        private void mnuResultCopyFieldSeparator_Click(object sender, EventArgs e)
        {
            var mnu = sender as ToolStripMenuItem;

            mnuResultCopyFieldSeparatorComma.Checked = false;
            mnuResultCopyFieldSeparatorSemicolon.Checked = false;
            mnuResultCopyFieldSeparatorI.Checked = false;
            mnu.Checked = true;

            switch (mnu.Tag.ToString())
            {
                case ",":
                    mnuResultCopyFieldSeparator.Tag = ",";
                    break;
                case ";":
                    mnuResultCopyFieldSeparator.Tag = ";";
                    break;
                case "|":
                    mnuResultCopyFieldSeparator.Tag = "|";
                    break;
            }
        }

        private void c1TrueDBGrid1_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateCells(); //c1TrueDBGrid1_KeyUp
        }

        private void c1TrueDBGrid1_MouseClick(object sender, MouseEventArgs e)
        {
            var bCornerSelected = false;
            var c1Grid = GetWhichGrid();
            var iRow = c1Grid.RowContaining(e.Y);
            var iCol = c1Grid.ColContaining(e.X);

            if (iRow == -1 && iCol == -1)
            {
                bCornerSelected = chkShowFilterRow.Checked == false || e.Y <= c1Grid.Splits[_SplitsNum].ColumnCaptionHeight;
            }

            if (!bCornerSelected || e.Button != MouseButtons.Left || e.X >= 20)
            {
                return;
            }

            c1Grid.SelectedRows.Clear();

            for (var i = 0; i < c1Grid.Splits[_SplitsNum].Rows.Count; i++)
            {
                c1Grid.SelectedRows.Add(i);
            }
        }

        private void mnuPreviewCLOBData_Click(object sender, EventArgs e)
        {
            mnuPreviewCLOBData.Checked = !mnuPreviewCLOBData.Checked;
        }

        private void HandleSqlExecuteErrorPosition_Oracle(string sSqlInfo, int iPos)
        {
            string sTemp;
            var sTempWord = "";
            var sTempWordVariables = "";
            var bKeywordNotAppear = false;
            var bHasUnicodeWord = false;
            var sExecutedResult = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];
            var sErrCode = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1];
            var sErrMsg = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2];
            var sErrHint = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[3];
            var sPos = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[4];
            var sSqlExecuted = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[5];
            var sSql = editor.Text;
            
            if (_sQueryStatus == "Cancel")
            {
                sTemp = sErrCode + "ErrorMsg: " + sErrMsg;
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_Oracle
                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "cancel";
                return;
            }

            if (sSqlExecuted.Length != Encoding.GetEncoding("UTF-8").GetBytes(sSqlExecuted).Length)
            {
                bHasUnicodeWord = true;

                //20201218 這裡先乘以 1，因為還沒抓到規則
                sPos = (Convert.ToInt16(sPos) - (Encoding.GetEncoding("UTF-8").GetBytes(sSqlExecuted).Length - sSqlExecuted.Length) * 1).ToString();
            }

            if (string.IsNullOrEmpty(sSqlExecuted)) //sSQLExecuted 為空，表示錯誤不是在 ExecuteQuery() 攔截到的，不需要處理
            {
                sTemp = "ErrorMsg: " + sErrMsg;
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_Oracle
                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "error";
            }
            else
            {
                //其中一種情況是，"aa"."name"→aa.name
                sErrMsg = sErrMsg.Replace("\".\"", ".");

                var iFrom = sErrMsg.IndexOf("\"", StringComparison.Ordinal) + 1;
                var iTo = sErrMsg.LastIndexOf("\"", StringComparison.Ordinal);

                if (iFrom != -1 && iTo != -1 && iTo > iFrom) //Oracle
                {
                    //可能情況：出現兩個單字，前後都有雙引號；故從第一個雙引號之後繼續找
                    iTo = sErrMsg.Substring(iFrom).IndexOf("\"", StringComparison.Ordinal) + iFrom;

                    //錯誤訊息有明確指出哪個字串
                    sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                    //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                    if (sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) == -1)
                    {
                        bKeywordNotAppear = true;
                    }

                    if (bHasUnicodeWord)
                    {
                        sPos = sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal).ToString();
                    }
                }
                else
                {
                    //檢查是否有用單引號指出字串？
                    iFrom = sErrMsg.IndexOf("'", StringComparison.Ordinal) + 1;
                    iTo = sErrMsg.LastIndexOf("'", StringComparison.Ordinal);

                    if (iFrom != -1 && iTo != -1 && iTo > iFrom) //Oracle
                    {
                        //錯誤訊息有明確指出哪個字串
                        sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                        //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                        if (sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) == -1)
                        {
                            bKeywordNotAppear = true;
                        }
                    }
                    else
                    {
                        //錯誤訊息並未明確指出哪個字串，所以從「OffSet」指示的「位置」找出這個字串
                        var iTemp = Convert.ToInt32(sPos) + iPos;

                        if (iTemp >= 0)
                        {
                            for (var ii = Convert.ToInt32(sPos) + iPos; ii < sSql.Length; ii++)
                            {
                                //如果下一個字元符合以下條件，則不再往下找
                                if (sSql.Substring(ii, 1) == " " || sSql.Substring(ii, 1) == "\r" || sSql.Substring(ii, 1) == "'" || sSql.Substring(ii, 1) == "\"" || sSql.Substring(ii, 1) == "(" || sSql.Substring(ii, 1) == ")" || sSql.Substring(ii, 1) == ":" || (!string.IsNullOrEmpty(sTempWord) && sSql.Substring(ii, 1) == "="))
                                {
                                    break;
                                }

                                sTempWord += sSql.Substring(ii, 1);
                            }

                            if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && !string.IsNullOrEmpty(sTempWord))
                            {
                                sTempWordVariables = sTempWord;
                                sTempWord = "";
                            }
                        }
                    }
                }

                //editor.IndicatorFillRange(Convert.ToInt32(sPos) - 1, sTemp3.Length - 1);

                if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping))
                {
                    var sParameter = _sQueryTextParametersPositionMapping.Split(new[] { "`" }, StringSplitOptions.RemoveEmptyEntries);

                    var iPosition0 = Convert.ToInt32(sPos);
                    var iPosition1 = iPosition0 - _iQueryTextParametersStart;
                    var iPosition2_Adjust = 0;

                    for (var i = 0; i < sParameter.Length; i++)
                    {
                        var sName = sParameter[i].Split('|')[0];
                        var iPosition2 = Convert.ToInt16(sParameter[i].Split('|')[1]) + _iQueryTextParametersStart;
                        var sValue = sParameter[i].Split('|')[2];

                        if (i == 0 && iPosition0 + iPos == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += iPos;
                            sTempWord = sName;
                            break;
                        }
                        if (i == 0 && iPosition0 == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += 1;
                            sTempWord = sName;
                            break;
                        }

                        if (i > 0 && iPosition0 == iPosition2 + Math.Abs(iPosition2_Adjust))
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += iPosition2_Adjust;
                            sTempWord = sName;
                            break;
                        }

                        if (i > 0 && iPosition0 + iPos == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            sTempWord = sName;
                            break;
                        }

                        if (!(iPosition1 > iPosition2 + Math.Abs(iPosition2_Adjust) && iPosition1 > iPosition2 + Math.Abs(iPosition2_Adjust) + sValue.Length))
                        {
                            if (iPosition2 > iPosition0)
                            {
                                iPosition0 += iPosition2_Adjust;
                                break; //DB 回報的定位點超過此變數的位置了，不再往下處理
                            }

                            //判斷新的定位點
                            if (_iQueryTextParametersStart > 0)
                            {
                                iPosition2_Adjust += (short)(sName.Length - sValue.Length);
                            }
                        }
                        else
                        {
                            //根據「變數、值」調整位置
                            iPosition0 += (short)(sName.Length - sValue.Length);
                        }
                    }

                    sPos = iPosition0.ToString();
                }

                if (bKeywordNotAppear == false)
                {
                    if (Convert.ToInt32(sPos) + iPos >= 0)
                    {
                        if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && string.IsNullOrEmpty(sTempWord))
                        {
                            sTempWord = sTempWordVariables;
                        }

                        SetSquiggle(false, Convert.ToInt32(sPos) + iPos, sTempWord.Length); //HandleSqlExecuteErrorPosition_Oracle

                        editor.SelectionStart = Convert.ToInt32(sPos) + iPos + sTempWord.Length;
                        editor.CurrentPosition = Convert.ToInt32(sPos) + iPos;
                    }
                }
                else
                {
                    editor.CurrentPosition = Convert.ToInt32(sPos);
                }

                editor.ScrollCaret();

                sTemp = "";

                #region 取得 Message 要呈現的文字
                if (Convert.ToInt32(sPos) > 0 && !string.IsNullOrEmpty(sTempWord) && sSql.Length >= 2)
                {
                    iFrom = 0;

                    for (var ii = Convert.ToInt32(sPos); ii > 0; ii--)
                    {
                        if (sSql.Substring(ii, 1) != "\n")
                        {
                            continue;
                        }

                        iFrom = ii + 1;
                        break;
                    }

                    iTo = sSql.Length;

                    for (var ii = Convert.ToInt32(sPos); ii < sSql.Length; ii++)
                    {
                        if (sSql.Substring(ii, 1) != "\r")
                        {
                            continue;
                        }

                        iTo = ii; //這裡要不要減 1，可能需要再確認
                        break;
                    }

                    if (Convert.ToInt32(sPos) - iFrom + iPos > 0)
                    {
                        var sMessageSQL = sSql.Substring(iFrom, iTo - iFrom);
                        var iLen = Encoding.Default.GetByteCount(sMessageSQL);

                        if (sMessageSQL.Length == iLen)
                        {
                            //整串文字「沒有」包含中文字
                            sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos, ' ') + "".PadRight(sTempWord.Length, '^');
                        }
                        else
                        {
                            sMessageSQL = sSql.Substring(iFrom, Convert.ToInt32(sPos) - iFrom + iPos);
                            iLen = Encoding.Default.GetByteCount(sMessageSQL);

                            if (sMessageSQL.Length == iLen)
                            {
                                var iLen2 = Encoding.Default.GetByteCount(sTempWord);

                                //要指引的文字，前面「沒有」包含中文字
                                sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos, ' ') + "".PadRight(iLen2, '^');
                            }
                            else
                            {
                                var iLen3 = Encoding.Default.GetByteCount(sTempWord);

                                //要指引的文字，前面「有」包含中文字
                                sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos + iLen - sMessageSQL.Length, ' ') + "".PadRight(iLen3, '^');
                            }
                        }
                    }
                }
                #endregion

                sTemp = (string.IsNullOrEmpty(sExecutedResult) ? "" : sExecutedResult + "\r\n") + sErrCode + "ErrorMsg: " + sErrMsg + sErrHint + sTemp + (string.IsNullOrEmpty(_sQueryTextParametersMapping) ? "" : "\r\n\r\n" + _sQueryTextParametersMapping);
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_Oracle

                if (string.IsNullOrEmpty(sErrMsg))
                {
                    return;
                }

                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "error";
            }
        }

        private void HandleSqlExecuteErrorPosition_PostgreSQL(string sSqlInfo, int iPos)
        {
            string sTemp;
            var sTempWord = "";
            var sTempWordVariables = "";
            var bKeywordNotAppear = false;

            var sExecutedResult = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];
            var sErrCode = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1];
            var sErrMsg = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2];
            var sErrHint = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[3];
            var sPos = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[4];
            var sSqlExecuted = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[5];
            var sSql = editor.Text;
            var iSpecialLength = 0;

            if (_sQueryStatus == "Cancel")
            {
                sTemp = sErrCode + "ErrorMsg: " + sErrMsg;
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_PostgreSQL
                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "cancel";
                return;
            }

            //錯誤的定位點若往前抓，就要調整這個地方
            if (sErrMsg.StartsWith("permission denied for relation") || sErrMsg.StartsWith("current transaction is aborted, commands ignored"))
            {
                iPos = 0;
            }

            if (string.IsNullOrEmpty(sSqlExecuted)) //sSQLExecuted 為空，表示錯誤不是在 ExecuteQuery() 攔截到的，不需要處理
            {
                sTemp = "ErrorMsg: " + sErrMsg;
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_PostgreSQL
                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "error";
            }
            else
            {
                if (sErrCode.IndexOf("22P02", StringComparison.Ordinal) != -1)
                {
                    //前後有單引號，所以 +2
                    iSpecialLength = 2;
                }

                //其中一種情況是，"aa"."name"→aa.name
                sErrMsg = sErrMsg.Replace("\".\"", ".");

                var iFrom = sErrMsg.IndexOf("\"", StringComparison.Ordinal) + 1;
                var iTo = sErrMsg.LastIndexOf("\"", StringComparison.Ordinal);

                if (iFrom != -1 && iTo != -1 && iTo > iFrom) //PostgreSQL
                {
                    //可能情況：出現兩個單字，前後都有雙引號；故從第一個雙引號之後繼續找
                    iTo = sErrMsg.Substring(iFrom).IndexOf("\"", StringComparison.Ordinal) + iFrom;

                    //錯誤訊息有明確指出哪個字串
                    sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                    //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                    if (sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) == -1)
                    {
                        bKeywordNotAppear = true;
                    }
                }
                else
                {
                    //檢查是否有用單引號指出字串？
                    iFrom = sErrMsg.IndexOf("'", StringComparison.Ordinal) + 1;
                    iTo = sErrMsg.LastIndexOf("'", StringComparison.Ordinal);

                    //iTo 如果小於 iFrom，表示只有起始的單引號，卻沒有結束的單引號
                    if (iFrom != -1 && iTo != -1 && iTo > iFrom) //PostgreSQL
                    {
                        //錯誤訊息有明確指出哪個字串
                        sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                        if (sErrMsg.ToUpper().Replace("PARAMETER '", "").Replace("' IS MISSING", "") == sTempWord.ToUpper())
                        {
                            sTempWord = ":" + sTempWord;
                        }

                        //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                        if (sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) == -1)
                        {
                            bKeywordNotAppear = true;
                        }
                    }
                    else if (sErrMsg.StartsWith("current transaction is aborted, commands ignored"))
                    {
                        sTempWordVariables = "";
                        sTempWord = "";
                    }
                    else
                    {
                        bKeywordNotAppear = true;

                        //錯誤訊息並未明確指出哪個字串，所以從「OffSet」指示的「位置」找出這個字串
                        var iTemp = Convert.ToInt32(sPos) + iPos;

                        if (iTemp >= 0)
                        {
                            for (var ii = Convert.ToInt32(sPos) + iPos; ii < sSql.Length; ii++)
                            {
                                //如果下一個字元符合以下條件，則不再往下找
                                if (sSql.Substring(ii, 1) == " " || sSql.Substring(ii, 1) == "\r" || sSql.Substring(ii, 1) == "'" || sSql.Substring(ii, 1) == "\"" || sSql.Substring(ii, 1) == ")" || sSql.Substring(ii, 1) == ":" || (!string.IsNullOrEmpty(sTempWord) && sSql.Substring(ii, 1) == "="))
                                {
                                    break;
                                }

                                sTempWord += sSql.Substring(ii, 1);
                            }

                            if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && !string.IsNullOrEmpty(sTempWord))
                            {
                                sTempWordVariables = sTempWord;
                                sTempWord = "";
                            }
                        }
                    }
                }

                //editor.IndicatorFillRange(Convert.ToInt32(sPos) - 1, sTemp3.Length - 1);

                if (Convert.ToInt32(sPos) > 0 && sTempWord.Length > 0 && (sErrMsg.StartsWith("ErrorCode: 25") || sErrMsg.StartsWith("ErrorCode: 22")))
                {
                    sPos = editor.CurrentPosition.ToString();
                    sTempWord = " ";
                }

                if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping))
                {
                    iSpecialLength = 0;
                    var sParameter = _sQueryTextParametersPositionMapping.Split(new[] { "`" }, StringSplitOptions.RemoveEmptyEntries);

                    var iPosition0 = Convert.ToInt32(sPos);
                    var iPosition1 = iPosition0 - _iQueryTextParametersStart;
                    var iPosition2_Adjust = 0;

                    for (var i = 0; i < sParameter.Length; i++)
                    {
                        var sName = sParameter[i].Split('|')[0];
                        var iPosition2 = Convert.ToInt16(sParameter[i].Split('|')[1]) + _iQueryTextParametersStart;
                        var sValue = sParameter[i].Split('|')[2];

                        if (i == 0 && iPosition0 + iPos == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 = iPosition0 + 1 + iPos;
                            sTempWord = sName;
                            break;
                        }
                        if (i == 0 && iPosition0 == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += 1;
                            sTempWord = sName;
                            break;
                        }

                        if (i > 0 && iPosition0 == iPosition2 + Math.Abs(iPosition2_Adjust))
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 = iPosition0 + 1 + iPosition2_Adjust;
                            sTempWord = sName;
                            break;
                        }

                        if (i > 0 && iPosition0 + iPos == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            sTempWord = sName;
                            break;
                        }

                        if (!(iPosition1 > iPosition2 + Math.Abs(iPosition2_Adjust) && iPosition1 > iPosition2 + Math.Abs(iPosition2_Adjust) + sValue.Length))
                        {
                            if (iPosition2 > iPosition0)
                            {
                                iPosition0 += iPosition2_Adjust;
                                break; //DB 回報的定位點超過此變數的位置了，不再往下處理
                            }

                            //判斷新的定位點
                            if (_iQueryTextParametersStart > 0)
                            {
                                iPosition2_Adjust += (short)(sName.Length - sValue.Length);
                            }
                        }
                        else
                        {
                            //根據「變數、值」調整位置
                            iPosition0 += (short)(sName.Length - sValue.Length);
                        }
                    }

                    sPos = iPosition0.ToString();
                }

                if (bKeywordNotAppear == false)
                {
                    if (Convert.ToInt32(sPos) + iPos >= 0)
                    {
                        if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && string.IsNullOrEmpty(sTempWord))
                        {
                            sTempWord = sTempWordVariables;
                        }

                        if (sSql.Substring(Convert.ToInt32(sPos) + iPos, sTempWord.Length + iSpecialLength) == sTempWord) //錯誤定位OK
                        {
                            SetSquiggle(false, Convert.ToInt32(sPos) + iPos, sTempWord.Length + iSpecialLength); //HandleSqlExecuteErrorPosition_PostgreSQL
                        }
                        else //有找到錯誤關鍵字，但 PostgreSQL 回報的定位 sPos 有問題，導致兩者不一致
                        {
                            if (sSql.Substring(Convert.ToInt32(sPos) + iPos, 1) == "\n" && iPos == -1)
                            {
                                iPos = 0;
                            }

                            sTempWord = ""; //讓錯誤定位在 SQL 的最前面位置
                        }
                        
                        editor.SelectionStart = Convert.ToInt32(sPos) + iPos + sTempWord.Length + iSpecialLength;
                        editor.CurrentPosition = Convert.ToInt32(sPos) + iPos;
                    }
                }
                else
                {
                    editor.CurrentPosition = Convert.ToInt32(sPos);
                }

                editor.ScrollCaret();

                sTemp = "";

                #region 取得 Message 要呈現的文字
                if (Convert.ToInt32(sPos) > 0 && !string.IsNullOrEmpty(sTempWord) && sSql.Length >= 2)
                {
                    iFrom = 0;

                    for (var ii = Convert.ToInt32(sPos); ii > 0; ii--)
                    {
                        if (sSql.Substring(ii, 1) != "\n")
                        {
                            continue;
                        }

                        iFrom = ii + 1;
                        break;
                    }

                    iTo = sSql.Length;

                    for (var ii = Convert.ToInt32(sPos); ii < sSql.Length; ii++)
                    {
                        if (sSql.Substring(ii, 1) != "\r")
                        {
                            continue;
                        }

                        iTo = ii; //這裡要不要減 1，可能需要再確認
                        break;
                    }

                    if (Convert.ToInt32(sPos) - iFrom + iPos > 0)
                    {
                        var sMessageSQL = sSql.Substring(iFrom, iTo - iFrom);
                        //byte[] byteStr = Encoding.GetEncoding("BIG5").GetBytes(sSql.Substring(iFrom, iTo - iFrom)); //把string轉為byte 
                        //var iLen = byteStr.Length; //取byte的長度, 中文字就會算2碼了

                        var iLen = Encoding.Default.GetByteCount(sMessageSQL);

                        if (sMessageSQL.Length == iLen)
                        {
                            //整串文字「沒有」包含中文字
                            sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos, ' ') + "".PadRight(sTempWord.Length + iSpecialLength, '^');
                        }
                        else
                        {
                            sMessageSQL = sSql.Substring(iFrom, Convert.ToInt32(sPos) - iFrom + iPos);
                            //byteStr = Encoding.GetEncoding("BIG5").GetBytes(sSql.Substring(iFrom, Convert.ToInt32(sPos) - iFrom + iPos));
                            //iLen = byteStr.Length;
                            iLen = Encoding.Default.GetByteCount(sMessageSQL);

                            if (sMessageSQL.Length == iLen)
                            {
                                var iLen2 = Encoding.Default.GetByteCount(sTempWord);

                                //要指引的文字，前面「沒有」包含中文字
                                sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos, ' ') + "".PadRight(iLen2 + iSpecialLength, '^');
                            }
                            else
                            {
                                var iLen3 = Encoding.Default.GetByteCount(sTempWord);

                                //要指引的文字，前面「有」包含中文字
                                sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos + iLen - sMessageSQL.Length, ' ') + "".PadRight(iLen3 + iSpecialLength, '^');
                            }
                        }
                    }
                }
                #endregion

                sTemp = (string.IsNullOrEmpty(sExecutedResult) ? "" : sExecutedResult + "\r\n") + sErrCode + "ErrorMsg: " + sErrMsg + sErrHint + sTemp + (string.IsNullOrEmpty(_sQueryTextParametersMapping) ? "" : "\r\n\r\n" + _sQueryTextParametersMapping);
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_PostgreSQL

                if (!string.IsNullOrEmpty(sErrMsg))
                {
                    c1DockingTab1.SelectedTab = tabMessage;
                    editorMessage.Tag = "error";
                }
            }

            if (!MyGlobal.bDBAutoRollback)
            {
                return;
            }

            try
            {
                MyGlobal.oPostgreReader.oRollback();
                //20210531 待確認：UpdateNotCommitYetInfo("");
            }
            catch (Exception)
            {
                //if (ex.Message == "This PgSqlTransaction has completed; it is no longer usable.")
                //{ }
            }
        }

        private void c1DockingTab2_TabClick(object sender, EventArgs e)
        {
            switch (c1DockingTab2.SelectedTab.Text)
            {
                case "Auto Replace":
                    c1GridARInfo.Focus();
                    break;
                case "Schema Browser":
                    c1GridSchemaBrowser.Focus();
                    break;
            }
        }

        private void HandleSqlExecuteErrorPosition_SQLServer(string sSqlInfo, int iPos)
        {
            string sTemp;
            var sTempWord = "";
            var sTempWordVariables = "";
            var bKeywordNotAppear = false;

            var sExecutedResult = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];
            var sErrCode = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1];
            var sErrMsg = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2];
            var sErrHint = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[3];
            var sPos = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[4];
            var sSqlExecuted = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[5];
            var sErrMsg2 = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[6];
            var sSql = editor.Text;

            var iErrLine = -1;
            var iTemp2 = sErrCode.IndexOf("ErrorLine:", StringComparison.Ordinal);

            if (iTemp2 != -1)
            {
                sTemp = sErrCode.Substring(iTemp2).Replace("ErrorLine:", "").Replace(" ", "").Replace("\r\n", "");
                int.TryParse(sTemp, out iErrLine);
            }

            if (_sQueryStatus == "Cancel")
            {
                sTemp = sErrCode + "ErrorMsg: " + sErrMsg;
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_SQLServer
                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "cancel";
            }
            else if (string.IsNullOrEmpty(sSqlExecuted)) //sSQLExecuted 為空，表示錯誤不是在 ExecuteQuery() 攔截到的，不需要處理
            {
                sTemp = "ErrorMsg: " + sErrMsg;
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_SQLServer
                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "error";
            }
            else
            {
                if (string.IsNullOrEmpty(sErrMsg2))
                {
                    //其中一種情況是，"aa"."name"→aa.name
                    sErrMsg = sErrMsg.Replace("\".\"", ".");

                    var iFrom = sErrMsg.IndexOf("\"", StringComparison.Ordinal) + 1;
                    var iTo = sErrMsg.LastIndexOf("\"", StringComparison.Ordinal);

                    if (iFrom != -1 && iTo != -1 && iTo > iFrom) //SQL Server
                    {
                        //可能情況：出現兩個單字，前後都有雙引號；故從第一個雙引號之後繼續找
                        iTo = sErrMsg.Substring(iFrom).IndexOf("\"", StringComparison.Ordinal) + iFrom;

                        //錯誤訊息有明確指出哪個字串
                        sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                        //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                        if (sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) == -1)
                        {
                            bKeywordNotAppear = true;
                        }

                        if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && !string.IsNullOrEmpty(sTempWord))
                        {
                            sTempWordVariables = sTempWord;
                            sTempWord = "";

                            if (sPos == "1")
                            {
                                sPos = "0";
                            }
                        }
                    }
                    else
                    {
                        //檢查是否有用單引號指出字串？
                        iFrom = sErrMsg.IndexOf("'", 0, StringComparison.Ordinal) + 1;
                        iTo = sErrMsg.IndexOf("'", iFrom, StringComparison.Ordinal);

                        if (iFrom != -1 && iTo != -1 && iTo > iFrom) //SQL Server
                        {
                            //錯誤訊息有明確指出哪個字串
                            sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                            //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                            if (sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) == -1)
                            {
                                bKeywordNotAppear = true;
                            }

                            if (bKeywordNotAppear == false)
                            {
                                var parts = sSqlExecuted.Split(new[] { "\r\n" }, StringSplitOptions.None);

                                if (iErrLine > 0 && iErrLine <= parts.Length && parts[iErrLine - 1].IndexOf(sTempWord, StringComparison.Ordinal) != -1)
                                {
                                    var iTemp0 = 0;
                                    var iTemp9 = Convert.ToInt32(sPos);
                                    var iPosPerErrLine = parts[iErrLine - 1].IndexOf(sTempWord, StringComparison.Ordinal);

                                    foreach (var t in parts)
                                    {
                                        if (iErrLine - 1 == iTemp0)
                                        {
                                            iTemp9 += iPosPerErrLine;
                                            break;
                                        }

                                        iTemp9 += t.Length + 2;
                                        iTemp0++;
                                    }

                                    sPos = (iTemp9 + 1).ToString();
                                }
                                else
                                {
                                    var iTemp = Convert.ToInt32(sPos) + iPos;

                                    if (iTemp >= 0)
                                    {
                                        for (var ii = Convert.ToInt32(sPos) + iPos; ii < sSql.Length; ii++)
                                        {
                                            if (sSql.Substring(ii, 1) != sTempWord.Substring(0, 1) || sSql.Substring(ii, sTempWord.Length) != sTempWord)
                                            {
                                                continue;
                                            }

                                            sPos = (ii + 1).ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //檢查是否有用雙引號指出字串？
                            iFrom = sErrMsg.IndexOf("\"", 0, StringComparison.Ordinal) + 1;
                            iTo = sErrMsg.IndexOf("\"", iFrom, StringComparison.Ordinal);

                            if (iFrom != -1 && iTo != -1 && iTo > iFrom) //SQL Server
                            {
                                //錯誤訊息有明確指出哪個字串
                                sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                                //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                                if (sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) == -1)
                                {
                                    bKeywordNotAppear = true;
                                }

                                if (bKeywordNotAppear == false)
                                {
                                    var parts = sSqlExecuted.Split(new[] { "\r\n" }, StringSplitOptions.None);

                                    if (iErrLine > 0 && iErrLine <= parts.Length && parts[iErrLine - 1].IndexOf(sTempWord, StringComparison.Ordinal) != -1)
                                    {
                                        var iTemp0 = 0;
                                        var iTemp9 = Convert.ToInt32(sPos);
                                        var iPosPerErrLine = parts[iErrLine - 1].IndexOf(sTempWord, StringComparison.Ordinal);

                                        foreach (var t in parts)
                                        {
                                            if (iErrLine - 1 == iTemp0)
                                            {
                                                iTemp9 += iPosPerErrLine;
                                                break;
                                            }

                                            iTemp9 += t.Length + 2;
                                            iTemp0++;
                                        }

                                        sPos = (iTemp9 + 1).ToString();
                                    }
                                    else
                                    {
                                        var iTemp = Convert.ToInt32(sPos) + iPos;

                                        if (iTemp >= 0)
                                        {
                                            for (var ii = Convert.ToInt32(sPos) + iPos; ii < sSql.Length; ii++)
                                            {
                                                if (sSql.Substring(ii, 1) != sTempWord.Substring(0, 1) || sSql.Substring(ii, sTempWord.Length) != sTempWord)
                                                {
                                                    continue;
                                                }

                                                sPos = (ii + 1).ToString();
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            //else
                            //{
                                //錯誤訊息並未明確指出哪個字串，所以從「OffSet」指示的「位置」找出這個字串
                                //20220402 沒有明確指出錯誤字串，游標停在起始位置(避免誤選)
                                //var iTemp = Convert.ToInt32(sPos) + iPos;

                                //if (iTemp >= 0)
                                //{
                                //    for (var ii = Convert.ToInt32(sPos) + iPos; ii < sSql.Length; ii++)
                                //    {
                                //        //如果下一個字元符合以下條件，則不再往下找
                                //        if (sSql.Substring(ii, 1) == " " || sSql.Substring(ii, 1) == "," || sSql.Substring(ii, 1) == "\r" || sSql.Substring(ii, 1) == "'" || sSql.Substring(ii, 1) == "\"" || sSql.Substring(ii, 1) == ")" || sSql.Substring(ii, 1) == ":" || !string.IsNullOrEmpty(sTempWord) && sSql.Substring(ii, 1) == "=")
                                //        {
                                //            break;
                                //        }

                                //        sTempWord += sSql.Substring(ii, 1);
                                //    }

                                //    if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && !string.IsNullOrEmpty(sTempWord))
                                //    {
                                //        sTempWordVariables = sTempWord;
                                //        sTempWord = "";
                                //    }
                                //}
                            //}
                        }
                    }
                }

                //editor.IndicatorFillRange(Convert.ToInt32(sPos) - 1, sTemp3.Length - 1);

                if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping))
                {
                    var sParameter = _sQueryTextParametersPositionMapping.Split(new[] { "`" }, StringSplitOptions.RemoveEmptyEntries);

                    var iPosition0 = Convert.ToInt32(sPos);
                    var iPosition1 = iPosition0 - _iQueryTextParametersStart;
                    var iPosition2_Adjust = 0;

                    for (var i = 0; i < sParameter.Length; i++)
                    {
                        var sName = sParameter[i].Split('|')[0];
                        var iPosition2 = Convert.ToInt16(sParameter[i].Split('|')[1]) + _iQueryTextParametersStart;
                        var sValue = sParameter[i].Split('|')[2];

                        if (i == 0 && iPosition0 + iPos == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += iPos;
                            sTempWord = sName;
                            break;
                        }

                        if (i == 0 && iPosition0 == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += 1;
                            sTempWord = sName;
                            break;
                        }

                        if (i > 0 && iPosition0 == iPosition2 + Math.Abs(iPosition2_Adjust))
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += iPosition2_Adjust;
                            sTempWord = sName;
                            break;
                        }

                        if (i > 0 && iPosition0 + iPos == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            sTempWord = sName;
                            break;
                        }

                        if (!(iPosition1 > iPosition2 + Math.Abs(iPosition2_Adjust) && iPosition1 > iPosition2 + Math.Abs(iPosition2_Adjust) + sValue.Length))
                        {
                            if (iPosition2 > iPosition0)
                            {
                                iPosition0 += iPosition2_Adjust;
                                break; //DB 回報的定位點超過此變數的位置了，不再往下處理
                            }

                            //判斷新的定位點
                            if (_iQueryTextParametersStart > 0)
                            {
                                iPosition2_Adjust += (short)(sName.Length - sValue.Length);
                            }
                        }
                        else
                        {
                            //根據「變數、值」調整位置
                            iPosition0 += (short)(sName.Length - sValue.Length);
                        }
                    }

                    sPos = iPosition0.ToString();
                }

                if (!string.IsNullOrEmpty(sErrMsg2))
                {
                    SetSquiggles(sSqlExecuted, sPos, sErrMsg2, out int iCursorPostion, out int iLength); //HandleSqlExecuteErrorPosition_SQLServer

                    editor.SelectionStart = iCursorPostion + iLength;
                    editor.CurrentPosition = iCursorPostion;
                }
                else
                {
                    if (bKeywordNotAppear == false)
                    {
                        if (Convert.ToInt32(sPos) + iPos >= 0)
                        {
                            if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && string.IsNullOrEmpty(sTempWord))
                            {
                                sTempWord = sTempWordVariables;
                            }

                            if (btnCancelQuery.Tag.ToString() != @"Cancel")
                            {
                                if (string.IsNullOrEmpty(sTempWord))
                                {
                                    editor.SelectionStart = Convert.ToInt32(sPos);
                                    editor.CurrentPosition = Convert.ToInt32(sPos);
                                }
                                else
                                {
                                    SetSquiggle(false, Convert.ToInt32(sPos) + iPos, sTempWord.Length); //HandleSqlExecuteErrorPosition_SQLServer

                                    editor.SelectionStart = Convert.ToInt32(sPos) + iPos + sTempWord.Length;
                                    editor.CurrentPosition = Convert.ToInt32(sPos) + iPos;
                                }
                            }
                            else
                            {
                                editor.SelectionStart = Convert.ToInt32(sPos) + iPos;
                                editor.CurrentPosition = Convert.ToInt32(sPos) + iPos;
                            }
                        }
                    }
                    else
                    {
                        editor.SelectionStart = Convert.ToInt32(sPos) + iPos;
                        editor.CurrentPosition = Convert.ToInt32(sPos) + iPos;
                    }
                }

                editor.ScrollCaret();

                sTemp = "";

                #region 取得 Message 要呈現的文字
                if (Convert.ToInt32(sPos) > 0 && !string.IsNullOrEmpty(sTempWord) && sSql.Length >= 2)
                {
                    var iFrom = 0;

                    for (var ii = Convert.ToInt32(sPos); ii > 0; ii--)
                    {
                        if (sSql.Substring(ii, 1) != "\n")
                        {
                            continue;
                        }

                        iFrom = ii + 1;
                        break;
                    }

                    var iTo = sSql.Length;

                    for (var ii = Convert.ToInt32(sPos); ii < sSql.Length; ii++)
                    {
                        if (sSql.Substring(ii, 1) != "\r")
                        {
                            continue;
                        }

                        iTo = ii; //這裡要不要減 1，可能需要再確認
                        break;
                    }

                    if (Convert.ToInt32(sPos) - iFrom + iPos > 0)
                    {
                        var sMessageSQL = sSql.Substring(iFrom, iTo - iFrom);
                        var iLen = Encoding.Default.GetByteCount(sMessageSQL);

                        if (sMessageSQL.Length == iLen)
                        {
                            //整串文字「沒有」包含中文字
                            sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos, ' ') + "".PadRight(sTempWord.Length, '^');
                        }
                        else
                        {
                            sMessageSQL = sSql.Substring(iFrom, Convert.ToInt32(sPos) - iFrom + iPos);
                            iLen = Encoding.Default.GetByteCount(sMessageSQL);

                            if (sMessageSQL.Length == iLen)
                            {
                                var iLen2 = Encoding.Default.GetByteCount(sTempWord);

                                //要指引的文字，前面「沒有」包含中文字
                                sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos, ' ') + "".PadRight(iLen2, '^');
                            }
                            else
                            {
                                var iLen3 = Encoding.Default.GetByteCount(sTempWord);

                                //要指引的文字，前面「有」包含中文字
                                sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos + iLen - sMessageSQL.Length, ' ') + "".PadRight(iLen3, '^');
                            }
                        }
                    }
                }
                #endregion

                sTemp = (string.IsNullOrEmpty(sExecutedResult) ? "" : sExecutedResult + "\r\n") + sErrCode + (!string.IsNullOrEmpty(sErrMsg2) ? "" : "ErrorMsg: ") + sErrMsg + sErrHint + sTemp + (string.IsNullOrEmpty(_sQueryTextParametersMapping) ? "" : "\r\n\r\n" + _sQueryTextParametersMapping);
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_SQLServer

                if (string.IsNullOrEmpty(sErrMsg))
                {
                    return;
                }

                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "error";
            }
        }

        private void HandleSqlExecuteErrorPosition_MySQL(string sSqlInfo, int iPos)
        {
            string sTemp;
            var sTempWord = "";
            var sTempWordVariables = "";
            var bKeywordNotAppear = false;

            var sExecutedResult = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];
            var sErrCode = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1];
            var sErrMsg = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2];
            var sErrHint = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[3];
            var sPos = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[4];
            var sSqlExecuted = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[5];
            var sSql = editor.Text;
            var sPosOriginal = sPos;

            if (_sQueryStatus == "Cancel")
            {
                sTemp = sErrCode + "ErrorMsg: " + sErrMsg;
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_MySQL
                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "cancel";
                return;
            }

            if (string.IsNullOrEmpty(sSqlExecuted)) //sSQLExecuted 為空，表示錯誤不是在 ExecuteQuery() 攔截到的，不需要處理
            {
                sTemp = "ErrorMsg: " + sErrMsg;
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_MySQL
                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "error";
            }
            else
            {
                //其中一種情況是，"aa"."name"→aa.name
                sErrMsg = sErrMsg.Replace("\".\"", ".");

                var iFrom = sErrMsg.IndexOf("\"", StringComparison.Ordinal) + 1;
                var iTo = sErrMsg.LastIndexOf("\"", StringComparison.Ordinal);

                if (sErrMsg.StartsWith("Unknown column '") && sErrMsg.IndexOf(" in 'where clause'", StringComparison.Ordinal) != -1)
                {
                    iFrom = sErrMsg.Replace(" in 'where clause'", "").IndexOf("\"", StringComparison.Ordinal) + 1;
                    iTo = sErrMsg.Replace(" in 'where clause'", "").LastIndexOf("\"", StringComparison.Ordinal);
                }

                if (iFrom != -1 && iTo != -1 && iTo > iFrom) //MySQL
                {
                    //可能情況：出現兩個單字，前後都有雙引號；故從第一個雙引號之後繼續找
                    iTo = sErrMsg.Substring(iFrom).IndexOf("\"", StringComparison.Ordinal) + iFrom;

                    //錯誤訊息有明確指出哪個字串
                    sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                    //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                    if (sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) == -1)
                    {
                        bKeywordNotAppear = true;
                    }

                    if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && !string.IsNullOrEmpty(sTempWord))
                    {
                        sTempWordVariables = sTempWord;
                        sTempWord = "";

                        if (sPos == "1")
                        {
                            sPos = "0";
                        }
                    }
                }
                else
                {
                    //檢查是否有用單引號指出字串？
                    iFrom = sErrMsg.IndexOf("'", StringComparison.Ordinal) + 1;
                    iTo = sErrMsg.LastIndexOf("'", StringComparison.Ordinal);

                    if (sErrMsg.StartsWith("Unknown column '") && sErrMsg.IndexOf(" in 'where clause'", StringComparison.Ordinal) != -1)
                    {
                        iFrom = sErrMsg.Replace(" in 'where clause'", "").IndexOf("'", StringComparison.Ordinal) + 1;
                        iTo = sErrMsg.Replace(" in 'where clause'", "").LastIndexOf("'", StringComparison.Ordinal);
                    }
                    else if (sErrMsg.StartsWith("Table") && sErrMsg.IndexOf("doesn't exist", StringComparison.Ordinal) != -1)
                    {
                        iFrom = sErrMsg.Replace("doesn't exist", "").IndexOf("'", StringComparison.Ordinal) + 1;
                        iTo = sErrMsg.Replace("doesn't exist", "").LastIndexOf("'", StringComparison.Ordinal);
                    }

                    if (iFrom != -1 && iTo != -1 && iTo > iFrom) //MySQL
                    {
                        //錯誤訊息有明確指出哪個字串
                        sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                        //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                        if (sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) == -1)
                        {
                            bKeywordNotAppear = true;
                        }
                        else
                        {
                            if (sPos == "0")
                            {
                                sPos = sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal).ToString();
                            }
                        }
                    }
                    else
                    {
                        //錯誤訊息並未明確指出哪個字串，所以從「OffSet」指示的「位置」找出這個字串
                        var iTemp = Convert.ToInt32(sPos) + iPos;

                        if (iTemp >= 0)
                        {
                            for (var ii = Convert.ToInt32(sPos) + iPos; ii < sSql.Length; ii++)
                            {
                                //如果下一個字元符合以下條件，則不再往下找
                                if (sSql.Substring(ii, 1) == " " || sSql.Substring(ii, 1) == "\r" || sSql.Substring(ii, 1) == "'" || sSql.Substring(ii, 1) == "\"" || sSql.Substring(ii, 1) == ")" || sSql.Substring(ii, 1) == ":" || (!string.IsNullOrEmpty(sTempWord) && sSql.Substring(ii, 1) == "="))
                                {
                                    break;
                                }

                                sTempWord += sSql.Substring(ii, 1);
                            }

                            if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && !string.IsNullOrEmpty(sTempWord))
                            {
                                sTempWordVariables = sTempWord;
                                sTempWord = "";
                            }
                        }
                    }
                }

                //editor.IndicatorFillRange(Convert.ToInt32(sPos) - 1, sTemp3.Length - 1);

                if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping))
                {
                    var sParameter = _sQueryTextParametersPositionMapping.Split(new[] { "`" }, StringSplitOptions.RemoveEmptyEntries);

                    var iPosition0 = Convert.ToInt32(sPos);
                    var iPosition1 = iPosition0 - _iQueryTextParametersStart;
                    var iPosition2_Adjust = 0;

                    for (var i = 0; i < sParameter.Length; i++)
                    {
                        var sName = sParameter[i].Split('|')[0];
                        var iPosition2 = Convert.ToInt16(sParameter[i].Split('|')[1]) + _iQueryTextParametersStart;
                        var sValue = sParameter[i].Split('|')[2];

                        if (i == 0 && iPosition0 + iPos == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += iPos;
                            sTempWord = sName;
                            break;
                        }
                        if (i == 0 && iPosition0 == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += 1;
                            sTempWord = sName;
                            break;
                        }

                        if (i > 0 && iPosition0 == iPosition2 + Math.Abs(iPosition2_Adjust))
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += iPosition2_Adjust;
                            sTempWord = sName;
                            break;
                        }

                        if (i > 0 && iPosition0 + iPos == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            sTempWord = sName;
                            break;
                        }

                        if (!(iPosition1 > iPosition2 + Math.Abs(iPosition2_Adjust) && iPosition1 > iPosition2 + Math.Abs(iPosition2_Adjust) + sValue.Length))
                        {
                            if (iPosition2 > iPosition0)
                            {
                                iPosition0 += iPosition2_Adjust;
                                break; //DB 回報的定位點超過此變數的位置了，不再往下處理
                            }

                            //判斷新的定位點
                            if (_iQueryTextParametersStart > 0)
                            {
                                iPosition2_Adjust += (short)(sName.Length - sValue.Length);
                            }
                        }
                        else
                        {
                            //根據「變數、值」調整位置
                            iPosition0 += (short)(sName.Length - sValue.Length);
                        }
                    }

                    sPos = iPosition0.ToString();
                }

                var bContinue = true;

                if (sErrMsg == "No database selected")
                {
                    sPos = sPosOriginal;
                    bContinue = false;
                }

                if (bKeywordNotAppear == false && bContinue)
                {
                    if (Convert.ToInt32(sPos) + iPos >= 0)
                    {
                        if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && string.IsNullOrEmpty(sTempWord))
                        {
                            sTempWord = sTempWordVariables;
                        }

                        SetSquiggle(false, Convert.ToInt32(sPos) + iPos, sTempWord.Length); //HandleSqlExecuteErrorPosition_MySQL

                        editor.SelectionStart = Convert.ToInt32(sPos) + iPos + sTempWord.Length;
                        editor.CurrentPosition = Convert.ToInt32(sPos) + iPos;
                    }
                }
                else
                {
                    editor.CurrentPosition = Convert.ToInt32(sPos);
                }

                editor.ScrollCaret();

                sTemp = "";

                #region 取得 Message 要呈現的文字
                if (Convert.ToInt32(sPos) > 0 && !string.IsNullOrEmpty(sTempWord) && sSql.Length >= 2)
                {
                    iFrom = 0;

                    for (var ii = Convert.ToInt32(sPos); ii > 0; ii--)
                    {
                        if (sSql.Substring(ii, 1) != "\n")
                        {
                            continue;
                        }

                        iFrom = ii + 1;
                        break;
                    }

                    iTo = sSql.Length;

                    for (var ii = Convert.ToInt32(sPos); ii < sSql.Length; ii++)
                    {
                        if (sSql.Substring(ii, 1) != "\r")
                        {
                            continue;
                        }

                        iTo = ii; //這裡要不要減 1，可能需要再確認
                        break;
                    }

                    if (Convert.ToInt32(sPos) - iFrom + iPos > 0)
                    {
                        var sMessageSQL = sSql.Substring(iFrom, iTo - iFrom);
                        var iLen = Encoding.Default.GetByteCount(sMessageSQL);

                        if (sMessageSQL.Length == iLen)
                        {
                            //整串文字「沒有」包含中文字
                            sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos, ' ') + "".PadRight(Math.Min(sTempWord.Length, sSql.Substring(iFrom, iTo - iFrom).Length - "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos, ' ').Length), '^');
                        }
                        else
                        {
                            sMessageSQL = sSql.Substring(iFrom, Convert.ToInt32(sPos) - iFrom + iPos);
                            iLen = Encoding.Default.GetByteCount(sMessageSQL);

                            if (sMessageSQL.Length == iLen)
                            {
                                var iLen2 = Encoding.Default.GetByteCount(sTempWord);

                                //要指引的文字，前面「沒有」包含中文字
                                sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos, ' ') + "".PadRight(iLen2, '^');
                            }
                            else
                            {
                                var iLen3 = Encoding.Default.GetByteCount(sTempWord);

                                //要指引的文字，前面「有」包含中文字
                                sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos + iLen - sMessageSQL.Length, ' ') + "".PadRight(iLen3, '^');
                            }
                        }
                    }
                }
                #endregion

                sTemp = (string.IsNullOrEmpty(sExecutedResult) ? "" : sExecutedResult + "\r\n") + sErrCode + (string.IsNullOrEmpty(sErrCode) ? "ErrorMsg:" : "") + " " + sErrMsg + sErrHint + sTemp + (string.IsNullOrEmpty(_sQueryTextParametersMapping) ? "" : "\r\n\r\n" + _sQueryTextParametersMapping);
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_MySQL

                if (string.IsNullOrEmpty(sErrMsg))
                {
                    return;
                }

                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "error";
            }
        }

        private void HandleSqlExecuteErrorPosition_SQLite(string sSqlInfo, int iPos)
        {
            string sTemp;
            var sTempWord = "";
            var sTempWordVariables = "";
            var bKeywordNotAppear = false;

            var sExecutedResult = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[0];
            var sErrCode = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[1];
            var sErrMsg = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[2];
            var sErrHint = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[3];
            var sPos = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[4];
            var sSqlExecuted = sSqlInfo.Split(new[] { MyGlobal.sSeparator }, StringSplitOptions.None)[5];
            var sSql = editor.Text;
            var sPosOriginal = sPos;

            if (_sQueryStatus == "Cancel")
            {
                sTemp = sErrCode + "ErrorMsg: " + sErrMsg;
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_SQLite
                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "cancel";
                return;
            }

            if (string.IsNullOrEmpty(sSqlExecuted)) //sSQLExecuted 為空，表示錯誤不是在 ExecuteQuery() 攔截到的，不需要處理
            {
                sTemp = "ErrorMsg: " + sErrMsg;
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_SQLite
                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "error";
            }
            else
            {
                //其中一種情況是，"aa"."name"→aa.name
                sErrMsg = sErrMsg.Replace("\".\"", ".");

                var iFrom = sErrMsg.IndexOf("\"", StringComparison.Ordinal) + 1;
                var iTo = sErrMsg.LastIndexOf("\"", StringComparison.Ordinal);

                if (sErrMsg.StartsWith("Unknown column '") && sErrMsg.IndexOf(" in 'where clause'", StringComparison.Ordinal) != -1)
                {
                    iFrom = sErrMsg.Replace(" in 'where clause'", "").IndexOf("\"", StringComparison.Ordinal) + 1;
                    iTo = sErrMsg.Replace(" in 'where clause'", "").LastIndexOf("\"", StringComparison.Ordinal);
                }

                if (iFrom != -1 && iTo != -1 && iTo > iFrom) //SQLite
                {
                    //可能情況：出現兩個單字，前後都有雙引號；故從第一個雙引號之後繼續找
                    iTo = sErrMsg.Substring(iFrom).IndexOf("\"", StringComparison.Ordinal) + iFrom;

                    //錯誤訊息有明確指出哪個字串
                    sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                    //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                    if (sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) == -1)
                    {
                        bKeywordNotAppear = true;
                    }

                    if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && !string.IsNullOrEmpty(sTempWord))
                    {
                        sTempWordVariables = sTempWord;
                        sTempWord = "";

                        if (sPos == "1")
                        {
                            sPos = "0";
                        }
                    }
                }
                else
                {
                    //檢查是否有用單引號指出字串？
                    iFrom = sErrMsg.IndexOf("'", StringComparison.Ordinal) + 1;
                    iTo = sErrMsg.LastIndexOf("'", StringComparison.Ordinal);

                    if (sErrMsg.StartsWith("Unknown column '") && sErrMsg.IndexOf(" in 'where clause'", StringComparison.Ordinal) != -1)
                    {
                        iFrom = sErrMsg.Replace(" in 'where clause'", "").IndexOf("'", StringComparison.Ordinal) + 1;
                        iTo = sErrMsg.Replace(" in 'where clause'", "").LastIndexOf("'", StringComparison.Ordinal);
                    }
                    else if (sErrMsg.StartsWith("Table") && sErrMsg.IndexOf("doesn't exist", StringComparison.Ordinal) != -1)
                    {
                        iFrom = sErrMsg.Replace("doesn't exist", "").IndexOf("'", StringComparison.Ordinal) + 1;
                        iTo = sErrMsg.Replace("doesn't exist", "").LastIndexOf("'", StringComparison.Ordinal);
                    }

                    if (iFrom != -1 && iTo != -1 && iTo > iFrom) //SQLite
                    {
                        //錯誤訊息有明確指出哪個字串
                        sTempWord = sErrMsg.Substring(iFrom, iTo - iFrom);

                        //判斷 sTempWord 是否有出現在執行的 SQL 裡面
                        if (sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal) == -1)
                        {
                            bKeywordNotAppear = true;
                        }
                        else
                        {
                            if (sPos == "0")
                            {
                                sPos = sSqlExecuted.ToUpper().IndexOf(sTempWord.ToUpper(), StringComparison.Ordinal).ToString();
                            }
                        }
                    }
                    else
                    {
                        //錯誤訊息並未明確指出哪個字串，所以從「OffSet」指示的「位置」找出這個字串
                        var iTemp = Convert.ToInt32(sPos) + iPos;

                        if (iTemp >= 0)
                        {
                            for (var ii = Convert.ToInt32(sPos) + iPos; ii < sSql.Length; ii++)
                            {
                                //如果下一個字元符合以下條件，則不再往下找
                                if (sSql.Substring(ii, 1) == " " || sSql.Substring(ii, 1) == "\r" || sSql.Substring(ii, 1) == "'" || sSql.Substring(ii, 1) == "\"" || sSql.Substring(ii, 1) == ")" || sSql.Substring(ii, 1) == ":" || (!string.IsNullOrEmpty(sTempWord) && sSql.Substring(ii, 1) == "="))
                                {
                                    break;
                                }

                                sTempWord += sSql.Substring(ii, 1);
                            }

                            if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && !string.IsNullOrEmpty(sTempWord))
                            {
                                sTempWordVariables = sTempWord;
                                sTempWord = "";
                            }
                        }
                    }
                }

                //editor.IndicatorFillRange(Convert.ToInt32(sPos) - 1, sTemp3.Length - 1);

                if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping))
                {
                    var sParameter = _sQueryTextParametersPositionMapping.Split(new[] { "`" }, StringSplitOptions.RemoveEmptyEntries);

                    var iPosition0 = Convert.ToInt32(sPos);
                    var iPosition1 = iPosition0 - _iQueryTextParametersStart;
                    var iPosition2_Adjust = 0;

                    for (var i = 0; i < sParameter.Length; i++)
                    {
                        var sName = sParameter[i].Split('|')[0];
                        var iPosition2 = Convert.ToInt16(sParameter[i].Split('|')[1]) + _iQueryTextParametersStart;
                        var sValue = sParameter[i].Split('|')[2];

                        if (i == 0 && iPosition0 + iPos == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += iPos;
                            sTempWord = sName;
                            break;
                        }
                        if (i == 0 && iPosition0 == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += 1;
                            sTempWord = sName;
                            break;
                        }

                        if (i > 0 && iPosition0 == iPosition2 + Math.Abs(iPosition2_Adjust))
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            iPosition0 += iPosition2_Adjust;
                            sTempWord = sName;
                            break;
                        }

                        if (i > 0 && iPosition0 + iPos == iPosition2)
                        {
                            //新的定位點剛好在變數的位置 (變數的值有錯誤)
                            sTempWord = sName;
                            break;
                        }

                        if (!(iPosition1 > iPosition2 + Math.Abs(iPosition2_Adjust) && iPosition1 > iPosition2 + Math.Abs(iPosition2_Adjust) + sValue.Length))
                        {
                            if (iPosition2 > iPosition0)
                            {
                                iPosition0 += iPosition2_Adjust;
                                break; //DB 回報的定位點超過此變數的位置了，不再往下處理
                            }

                            //判斷新的定位點
                            if (_iQueryTextParametersStart > 0)
                            {
                                iPosition2_Adjust += (short)(sName.Length - sValue.Length);
                            }
                        }
                        else
                        {
                            //根據「變數、值」調整位置
                            iPosition0 += (short)(sName.Length - sValue.Length);
                        }
                    }

                    sPos = iPosition0.ToString();
                }

                var bContinue = true;

                if (sErrMsg == "No database selected")
                {
                    sPos = sPosOriginal;
                    bContinue = false;
                }

                if (bKeywordNotAppear == false && bContinue)
                {
                    if (Convert.ToInt32(sPos) + iPos >= 0)
                    {
                        if (!string.IsNullOrEmpty(_sQueryTextParametersPositionMapping) && string.IsNullOrEmpty(sTempWord))
                        {
                            sTempWord = sTempWordVariables;
                        }

                        SetSquiggle(false, Convert.ToInt32(sPos) + iPos, sTempWord.Length); //HandleSqlExecuteErrorPosition_SQLite

                        editor.SelectionStart = Convert.ToInt32(sPos) + iPos + sTempWord.Length;
                        editor.CurrentPosition = Convert.ToInt32(sPos) + iPos;
                    }
                }
                else
                {
                    editor.CurrentPosition = Convert.ToInt32(sPos);
                }

                editor.ScrollCaret();

                sTemp = "";

                #region 取得 Message 要呈現的文字
                if (Convert.ToInt32(sPos) > 0 && !string.IsNullOrEmpty(sTempWord) && sSql.Length >= 2)
                {
                    iFrom = 0;

                    for (var ii = Convert.ToInt32(sPos); ii > 0; ii--)
                    {
                        if (sSql.Substring(ii, 1) != "\n")
                        {
                            continue;
                        }

                        iFrom = ii + 1;
                        break;
                    }

                    iTo = sSql.Length;

                    for (var ii = Convert.ToInt32(sPos); ii < sSql.Length; ii++)
                    {
                        if (sSql.Substring(ii, 1) != "\r")
                        {
                            continue;
                        }

                        iTo = ii; //這裡要不要減 1，可能需要再確認
                        break;
                    }

                    if (Convert.ToInt32(sPos) - iFrom + iPos > 0)
                    {
                        var sMessageSQL = sSql.Substring(iFrom, iTo - iFrom);
                        var iLen = Encoding.Default.GetByteCount(sMessageSQL);

                        if (sMessageSQL.Length == iLen)
                        {
                            //整串文字「沒有」包含中文字
                            sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos, ' ') + "".PadRight(Math.Min(sTempWord.Length, sSql.Substring(iFrom, iTo - iFrom).Length - "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos, ' ').Length), '^');
                        }
                        else
                        {
                            sMessageSQL = sSql.Substring(iFrom, Convert.ToInt32(sPos) - iFrom + iPos);
                            iLen = Encoding.Default.GetByteCount(sMessageSQL);

                            if (sMessageSQL.Length == iLen)
                            {
                                var iLen2 = Encoding.Default.GetByteCount(sTempWord);

                                //要指引的文字，前面「沒有」包含中文字
                                sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos, ' ') + "".PadRight(iLen2, '^');
                            }
                            else
                            {
                                var iLen3 = Encoding.Default.GetByteCount(sTempWord);

                                //要指引的文字，前面「有」包含中文字
                                sTemp = "\r\n\r\n" + sSql.Substring(iFrom, iTo - iFrom) + "\r\n" + "".PadRight(Convert.ToInt32(sPos) - iFrom + iPos + iLen - sMessageSQL.Length, ' ') + "".PadRight(iLen3, '^');
                            }
                        }
                    }
                }
                #endregion

                sTemp = (string.IsNullOrEmpty(sExecutedResult) ? "" : sExecutedResult + "\r\n") + sErrCode + (string.IsNullOrEmpty(sErrCode) ? "ErrorMsg:" : "") + " " + sErrMsg + sErrHint + sTemp + (string.IsNullOrEmpty(_sQueryTextParametersMapping) ? "" : "\r\n\r\n" + _sQueryTextParametersMapping);
                UpdateMessage(sTemp); //HandleSqlExecuteErrorPosition_SQLite

                if (string.IsNullOrEmpty(sErrMsg))
                {
                    return;
                }

                c1DockingTab1.SelectedTab = tabMessage;
                editorMessage.Tag = "error";
            }
        }

        private void UpdateNotCommitYetInfo(string sText)
        {
            var bValue = !string.IsNullOrEmpty(sText);

            lblNotCommitYet.Visible = bValue;
            sepNotCommitYet.Visible = bValue;

            lblNotCommitYet.Text = sText;
        }

        private void CopyTextToClipboard(string sText, string sKey)
        {
            try
            {
                Clipboard.SetDataObject(sText, true, 10, 100);
            }
            catch (Exception ex)
            {
                try
                {
                    if (Clipboard.GetText() != sText)
                    {
                        _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                        MessageBox.Show(_sLangText, @"JasonQuery - CopyTextToClipboard " + sKey, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception)
                {
                    //
                }
            }
        }

        private void c1GridSchemaBrowser_FetchRowStyle(object sender, FetchRowStyleEventArgs e)
        {
            try
            {
                var data = c1GridSchemaBrowser.GetDataBoundItem(e.Row);
                var name = ((DataRowView)data).Row["SchemaObject"].ToString();

                if (name == MyGlobal.sDBConnectionName)
                {
                    e.CellStyle.ForeColor = MyLibrary.bDarkMode ? Color.Yellow : Color.Blue;
                }
            }
            catch (Exception ex)
            {
                _sLangText = MyGlobal.GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text");
                MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message, @"JasonQuery (c1TrueDBGrid_FetchRowStyle)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void c1GridSchemaBrowser_MouseUp(object sender, MouseEventArgs e)
        {
            var iDisplayRowIndex = c1GridSchemaBrowser.RowContaining(e.Y);

            if (iDisplayRowIndex == -1)
            {
                return;
            }

            //切換：當滑鼠點到 Grid 最右邊的空白處時，一樣會有點選的效果，但 Focus 列並不會同時切換，所以此處要強制切換
            c1GridSchemaBrowser.Row = iDisplayRowIndex;
        }

        private void txtSchemaFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                QuerySchema();
            }
        }

        private void QuerySchema()
        {
            var sFilter = "";
            var dtSchema = (DataTable)c1GridSchemaBrowser.DataSource;
            txtSchemaFilter.Text = txtSchemaFilter.Text.Replace("%", "*");

            if (string.IsNullOrEmpty(txtSchemaFilter.Text.Trim()))
            {
                txtSchemaFilter.Text = @"*";
            }

            if (!txtSchemaFilter.Text.EndsWith("*"))
            {
                txtSchemaFilter.Text += @"*";
            }

            try
            {
                if (MyGlobal.bShowColumnInfo)
                {
                    sFilter = "(SchemaName LIKE '" + txtSchemaFilter.Text.Trim().Replace("*", "%") + "%' OR Schema_Browser LIKE '" + txtSchemaFilter.Text.Trim().Replace("*", "%") + "%')";
                }
                else
                {
                    sFilter = "Schema_Browser LIKE '" + txtSchemaFilter.Text.Trim().Replace("*", "%") + "'";
                }

                dtSchema.DefaultView.RowFilter = sFilter.Replace("%%", "%");
            }
            catch (Exception ex)
            {
                _sLangText = MyGlobal.GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text");
                MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                //展開指定的節點 (-1:標題列，所以，從 0 開始算，要展開哪一個)
                c1GridSchemaBrowser.ExpandGroupRow(0);
                c1GridSchemaBrowser.Focus();
            }
        }

        private void btnExpandCollapse_ButtonClick(object sender, EventArgs e)
        {
            btnExpandCollapse.Enabled = false;

            if (btnExpandCollapse.Tag?.ToString() == "0")
            {
                CollapseAll();
                btnExpandCollapse.Tag = "1";
            }
            else
            {
                ExpandAll();
                btnExpandCollapse.Tag = "0";
            }

            btnExpandCollapse.Enabled = true;
        }

        private void mnuExpandAll_Click(object sender, EventArgs e)
        {
            ExpandAll();
        }

        private void mnuCollapseAll_Click(object sender, EventArgs e)
        {
            CollapseAll();
        }

        private void ExpandAll()
        {
            Application.UseWaitCursor = true;

            for (var i = 0; i < c1GridSchemaBrowser.Splits[_iii].Rows.Count; i++)
            {
                if (c1GridSchemaBrowser.Splits[_iii].Rows[i].RowType != RowTypeEnum.DataRow)
                    c1GridSchemaBrowser.ExpandGroupRow(i);

                Application.DoEvents();
            }

            c1GridSchemaBrowser.Focus();
            c1GridSchemaBrowser.Cursor = Cursors.Default;
            Application.UseWaitCursor = false;
        }

        private void CollapseAll()
        {
            Application.UseWaitCursor = true;

            for (var i = 0; i < c1GridSchemaBrowser.Splits[_iii].Rows.Count; i++)
            {
                if (c1GridSchemaBrowser.Splits[_iii].Rows[i].RowType != RowTypeEnum.DataRow)
                    c1GridSchemaBrowser.CollapseGroupRow(i);

                Application.DoEvents();
            }

            c1GridSchemaBrowser.Focus();
            c1GridSchemaBrowser.Cursor = Cursors.Default;
            Application.UseWaitCursor = false;

            //展開指定的節點 (-1:標題列，所以，從 0 開始算，要展開哪一個)
            //參考 MyGlobal.UpdateSchemaData()
            c1GridSchemaBrowser.ExpandGroupRow(0); //展開 0 的結點
        }

        private void c1GridSchemaBrowser_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                    e.Handled = true; //e.Handled = true：表示取消原本的左右鍵，不發揮作用 (不會右移或左移)，避免動作重複
                    CheckNodeThenCollapsedOrExpanded();
                    break;
            }
        }

        private void CheckNodeThenCollapsedOrExpanded()
        {
            var row = c1GridSchemaBrowser.Row;

            switch (c1GridSchemaBrowser.Splits[_iii].Rows[row].RowType)
            {
                //判斷是不是節點
                case RowTypeEnum.CollapsedGroupRow:
                    c1GridSchemaBrowser.ExpandGroupRow(row);
                    AutoResizeGridColumnWidth(); //CheckNodeThenCollapsedOrExpanded
                    break;
                case RowTypeEnum.ExpandedGroupRow:
                    c1GridSchemaBrowser.CollapseGroupRow(row);
                    break;
            }
        }

        private void c1GridSchemaBrowser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CheckNodeThenCollapsedOrExpanded();
        }

        private void btnSettingOfFocus_ButtonClick(object sender, EventArgs e)
        {
            mnuFocusOnQueryEditor.Checked = MyGlobal.bAfterPasteFocusOnQueryEditor;
            mnuFocusOnDataGrid.Checked = !MyGlobal.bAfterPasteFocusOnQueryEditor;
        }

        private void mnuFocusOnDataGrid_Click(object sender, EventArgs e)
        {
            mnuFocusOnDataGrid.Checked = !mnuFocusOnDataGrid.Checked;
            mnuFocusOnQueryEditor.Checked = !mnuFocusOnDataGrid.Checked;

            MyGlobal.bAfterPasteFocusOnQueryEditor = mnuFocusOnQueryEditor.Checked;
            MyGlobal.UpdateSetting("EditorConfig", "AfterPasteFocusOnQueryEditor", mnuFocusOnQueryEditor.Checked ? "1" : "0");
        }

        private void mnuFocusOnQueryEditor_Click(object sender, EventArgs e)
        {
            mnuFocusOnQueryEditor.Checked = !mnuFocusOnQueryEditor.Checked;
            mnuFocusOnDataGrid.Checked = !mnuFocusOnQueryEditor.Checked;

            MyGlobal.bAfterPasteFocusOnQueryEditor = mnuFocusOnQueryEditor.Checked;
            MyGlobal.UpdateSetting("EditorConfig", "AfterPasteFocusOnQueryEditor", mnuFocusOnQueryEditor.Checked ? "1" : "0");
        }

        private void ArrangeSchemaData4CopyPaste(string sMode)
        {
            var iRow = c1GridSchemaBrowser.Row;
            var sTemp = "";
            //var selCol = c1GridSchemaBrowser.SelectedCols.Count;
            var vr = c1GridSchemaBrowser.Splits[0].Rows[iRow];
            //var sSchemaType = "";

            //判斷是不是節點
            if (c1GridSchemaBrowser.Splits[0].Rows[iRow].RowType == RowTypeEnum.CollapsedGroupRow || c1GridSchemaBrowser.Splits[0].Rows[iRow].RowType == RowTypeEnum.ExpandedGroupRow)
            {
                var iLevel = ((GroupRow)vr).Level;
                //sSchemaType = c1GridSchemaBrowser.Columns["SchemaType"].CellValue(((GroupRow)vr).StartIndex).ToString();

                switch (MyGlobal.sDataSource)
                {
                    case "Oracle" when iLevel <= 1:
                    case "PostgreSQL" when iLevel <= 2:
                    case "SQL Server" when iLevel <= 2:
                    case "MySQL" when iLevel <= 2:
                    case "SQLite" when iLevel <= 2:
                    case "SQLCipher" when iLevel <= 2:
                        return; //針對主要節點(例如 AliasName/Tables/Functions/View/Triggers)，不處理
                    default:
                        sTemp = ((GroupRow)vr).GroupedText;
                        break;
                }
            }
            else
            {
                vr = c1GridSchemaBrowser.Splits[0].Rows[iRow];
                //var sSchemaType = c1GridSchemaBrowser.Columns["SchemaType"].CellValue(vr.DataRowIndex).ToString();
                var sSchemaType = (c1GridSchemaBrowser.GetDataBoundItem(iRow) as DataRowView).Row["SchemaType"].ToString();

                if (sSchemaType.StartsWith("Tables"))
                {
                    //NG: sTemp = c1GridSchemaBrowser[vr.DataRowIndex, "Schema_Browser"].ToString();

                    //原廠給的建議寫法：use GetDataBoundItem to get the DataRowView associated with the current row and then fetch data from its underlying row
                    sTemp = (c1GridSchemaBrowser.GetDataBoundItem(iRow) as DataRowView).Row["Schema_Browser"].ToString();
                }
                else
                {
                    //NG: sTemp = c1GridSchemaBrowser[vr.DataRowIndex, "Schema_Browser"].ToString();

                    //原廠給的建議寫法：use GetDataBoundItem to get the DataRowView associated with the current row and then fetch data from its underlying row
                    sTemp = (c1GridSchemaBrowser.GetDataBoundItem(iRow) as DataRowView).Row["Schema_Browser"].ToString();
                }

                if (sTemp.IndexOf(", ", StringComparison.Ordinal) != -1)
                {
                    sTemp = sTemp.Split(new[] { ", " }, StringSplitOptions.None)[0];
                }

                if (sTemp.IndexOf(", ", StringComparison.Ordinal) != -1)
                {
                    sTemp = sTemp.Split(new[] { ", " }, StringSplitOptions.None)[0];
                }
            }

            if (string.IsNullOrEmpty(sTemp))
            {
                return;
            }

            if (sTemp.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
            {
                sTemp = sTemp.Substring(0, sTemp.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
            }

            CopyTextToClipboard(sTemp, "ArrangeSchemaData4CopyPaste");

            if (!sMode.StartsWith("Paste"))
            {
                return;
            }

            switch (sMode)
            {
                case "Paste3":
                    CopyTextToClipboard(sTemp, "ArrangeSchemaData4CopyPaste3");
                    break;
                case "Paste4":
                    CopyTextToClipboard(sTemp + ", ", "ArrangeSchemaData4CopyPaste4");
                    break;
                case "Paste5":
                    CopyTextToClipboard(sTemp + ", \r\n", "ArrangeSchemaData4CopyPaste5");
                    break;
                case "Paste6":
                    CopyTextToClipboard(sTemp + "\r\n", "ArrangeSchemaData4CopyPaste6");
                    break;
            }

            editor.Paste();

            if (MyGlobal.bAfterPasteFocusOnQueryEditor)
            {
                editor.Focus();
            }
        }

        private void c1GridARInfo_Leave(object sender, EventArgs e)
        {
            tsAutoReplace.BackColor = _cToolstripUnfocused;
            UpdateAutoReplaceDictionary(); //c1GridARInfo_Leave
        }

        private void c1GridARInfo_Enter(object sender, EventArgs e)
        {
            tsAutoReplace.BackColor = _cToolstripFocused;
        }

        private void c1GridSchemaBrowser_Leave(object sender, EventArgs e)
        {
            tsSchemaBrowser.BackColor = _cToolstripUnfocused;
        }

        private void c1GridSchemaBrowser_Enter(object sender, EventArgs e)
        {
            tsSchemaBrowser.BackColor = _cToolstripFocused;
        }

        private void c1DockingTab2_Enter(object sender, EventArgs e)
        {
            HideACGrid();
            tsEditor.BackColor = _cToolstripFocused;
            tsAutoReplace.BackColor = _cToolstripFocused;
        }

        private void c1DockingTab2_Leave(object sender, EventArgs e)
        {
            tsEditor.BackColor = _cToolstripUnfocused;
            tsAutoReplace.BackColor = _cToolstripUnfocused;
        }

        private void tsDataGrid_Enter(object sender, EventArgs e)
        {
            HideACGrid();
        }

        private void c1StatusBar1_MouseMove(object sender, MouseEventArgs e)
        {
            HideACGrid();
        }

        private void c1StatusBar2_MouseMove(object sender, MouseEventArgs e)
        {
            HideACGrid();
        }

        private void c1GridSchemaBrowser_MouseDown(object sender, MouseEventArgs e)
        {
            string sSchemaName;

            var iDisplayRowIndex = c1GridSchemaBrowser.RowContaining(e.Y);

            if (iDisplayRowIndex == -1)
            {
                return; //Exclude row headers
            }

            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            _cMenuSchemaBrowser = new ContextMenuStrip();
            c1GridSchemaBrowser.ContextMenuStrip = _cMenuSchemaBrowser;

            var vr = c1GridSchemaBrowser.Splits[_iii].Rows[iDisplayRowIndex];

            c1GridSchemaBrowser.Row = iDisplayRowIndex;

            string sSchemaType;
            var sSchemaType2 = "";
            var sSchemaNode = "";
            var sSchemaDbo = "";
            var sObjectID = "";
            int iLevel;
            var sTitle1 = MyGlobal.GetLanguageString("Copy to Clipboard", "form", "frmGenerateSQL", "object", "btnCopyToClipboard", "Text");
            var sTitle2 = MyGlobal.GetLanguageString("Paste to Query Editor", "form", "frmGenerateSQL", "object", "btnPasteToQueryEditor", "Text");

            if (c1GridSchemaBrowser.Splits[_iii].Rows[iDisplayRowIndex].RowType == RowTypeEnum.CollapsedGroupRow || c1GridSchemaBrowser.Splits[_iii].Rows[iDisplayRowIndex].RowType == RowTypeEnum.ExpandedGroupRow)
            {
                iLevel = ((GroupRow)vr).Level;

                //此寫法可能有問題：原廠的 BUG，有時會取到錯誤的值
                sSchemaType = c1GridSchemaBrowser.Columns["SchemaType"].CellValue(((GroupRow)vr).StartIndex).ToString();

                var ii = 2;

                if (MyGlobal.sDataSource == "Oracle")
                {
                    ii = 1;
                }

                //原廠給的暫時性解法
                for (var i = iDisplayRowIndex - 1; i >= 0; i--)
                {
                    //if above row is Group Row
                    if (!(c1GridSchemaBrowser.Splits[0].Rows[i] is GroupRow groupRow))
                    {
                        continue;
                    }

                    //if level is 2, meaning that this Group Row is for SchemaType
                    if (groupRow.Level != ii)
                    {
                        continue;
                    }

                    //copy its GroupedText and the break the loop
                    sSchemaType2 = groupRow.GroupedText;
                    break;
                }

                var dtSchema0 = (DataTable)c1GridSchemaBrowser.DataSource;

                //20220516 判斷 sSchemaType 哪一個是正確的
                if (sSchemaType != sSchemaType2 && !string.IsNullOrEmpty(sSchemaType2))
                {
                    switch (MyGlobal.sDataSource)
                    {
                        case "Oracle" when iLevel > 1:
                        {
                            var sTempSchemaObject1 = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();
                            var sTempSchemaObject2 = dtSchema0.Select($"SchemaType = '{sSchemaType2}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();

                            if (string.IsNullOrEmpty(sTempSchemaObject1) && !string.IsNullOrEmpty(sTempSchemaObject2))
                            {
                                sSchemaType = sSchemaType2;
                            }

                            break;
                        }
                        case "PostgreSQL" when iLevel > 2:
                        {
                            var sTempSchemaObject1 = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();
                            var sTempSchemaObject2 = dtSchema0.Select($"SchemaType = '{sSchemaType2}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();

                            if (string.IsNullOrEmpty(sTempSchemaObject1) && !string.IsNullOrEmpty(sTempSchemaObject2))
                            {
                                sSchemaType = sSchemaType2;
                            }

                            break;
                        }
                        case "SQL Server" when iLevel > 2:
                        {
                            var sTempSchemaObject1 = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();
                            var sTempSchemaObject2 = dtSchema0.Select($"SchemaType = '{sSchemaType2}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();

                            if (string.IsNullOrEmpty(sTempSchemaObject1) && !string.IsNullOrEmpty(sTempSchemaObject2))
                            {
                                sSchemaType = sSchemaType2;
                            }

                            break;
                        }
                        case "MySQL" when iLevel > 2:
                        {
                            var sTempSchemaObject1 = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();
                            var sTempSchemaObject2 = dtSchema0.Select($"SchemaType = '{sSchemaType2}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();

                            if (string.IsNullOrEmpty(sTempSchemaObject1) && !string.IsNullOrEmpty(sTempSchemaObject2))
                            {
                                sSchemaType = sSchemaType2;
                            }

                            break;
                        }
                        case "SQLite" when iLevel > 2:
                        {
                            var sTempSchemaObject1 = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();
                            var sTempSchemaObject2 = dtSchema0.Select($"SchemaType = '{sSchemaType2}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();

                            if (string.IsNullOrEmpty(sTempSchemaObject1) && !string.IsNullOrEmpty(sTempSchemaObject2))
                            {
                                sSchemaType = sSchemaType2;
                            }

                            break;
                        }
                        case "SQLCipher" when iLevel > 2:
                        {
                            var sTempSchemaObject1 = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();
                            var sTempSchemaObject2 = dtSchema0.Select($"SchemaType = '{sSchemaType2}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();

                            if (string.IsNullOrEmpty(sTempSchemaObject1) && !string.IsNullOrEmpty(sTempSchemaObject2))
                            {
                                sSchemaType = sSchemaType2;
                            }

                            break;
                        }
                    }
                }

                switch (MyGlobal.sDataSource)
                {
                    case "PostgreSQL" when iLevel > 2:
                    {
                        var sTemp = ((GroupRow)vr).GroupedText;

                        sSchemaNode = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '{sTemp}'").FirstOrDefault()?["SchemaNode"].ToString();
                        break;
                    }
                    case "SQL Server" when iLevel > 2:
                    {
                        var sTemp = ((GroupRow)vr).GroupedText;

                        sSchemaNode = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '{sTemp}'").FirstOrDefault()?["SchemaNode"].ToString();
                        sSchemaDbo = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '{sTemp}'").FirstOrDefault()?["SchemaDbo"].ToString();
                        sObjectID = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '{sTemp}'").FirstOrDefault()?["ObjectID"].ToString();
                        break;
                    }
                    case "MySQL" when iLevel > 2:
                    {
                        var sTemp = ((GroupRow)vr).GroupedText;

                        sSchemaNode = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '{sTemp}'").FirstOrDefault()?["SchemaNode"].ToString();
                        break;
                    }
                }

                if (sSchemaType.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
                {
                    sSchemaType = sSchemaType.Substring(0, sSchemaType.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
                }

                sSchemaName = ((GroupRow)vr).GroupedText;

                if (sSchemaName.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
                {
                    sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
                }

                switch (MyGlobal.sDataSource)
                {
                    case "SQL Server" when iLevel == 1:
                        MyGlobal.GenerateRightMenu4CopyOnly(false, _cMenuSchemaBrowser, c1GridSchemaBrowser, editor, sTitle1, sTitle2, "USE " + sSchemaName + ";", e.X, e.Y, false); //針對 "USE Database" 切換 DB，不出現額外幾個貼上的選項
                        break;
                    case "MySQL" when iLevel == 1:
                        MyGlobal.GenerateRightMenu4CopyOnly(false, _cMenuSchemaBrowser, c1GridSchemaBrowser, editor, sTitle1, sTitle2, "USE " + sSchemaName + ";", e.X, e.Y, false); //針對 "USE Database" 切換 DB，不出現額外幾個貼上的選項
                        break;
                    case "Oracle" when iLevel <= 1:
                    case "PostgreSQL" when iLevel <= 2:
                    case "SQL Server" when iLevel <= 2:
                    case "MySQL" when iLevel <= 2:
                        return; //針對主要節點(例如 AliasName/Tables/Functions/View/Triggers)，右鍵不處理
                    case "Oracle":
                        {
                            if (sSchemaType.StartsWith("Function") || sSchemaType.StartsWith("Trigger"))
                            {
                                MyGlobal.GenerateRightMenu4CopyOnly(false, _cMenuSchemaBrowser, c1GridSchemaBrowser, editor, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }
                            else
                            {
                                MyGlobal.GenerateRightMenu4Copy_Oracle(false, c1GridSchemaBrowser, _cMenuSchemaBrowser, editor, AccessibleDescription, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }

                            break;
                        }
                    case "PostgreSQL":
                        {
                            if (sSchemaType.StartsWith("Function"))
                            {
                                if (sSchemaName.IndexOf("(", StringComparison.Ordinal) != -1 && sSchemaName.IndexOf(")", StringComparison.Ordinal) != -1)
                                {
                                    sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf("(", StringComparison.Ordinal));
                                }
                                
                                MyGlobal.GenerateRightMenu4CopyOnly(false, _cMenuSchemaBrowser, c1GridSchemaBrowser, editor, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }
                            else if (sSchemaType.StartsWith("Trigger"))
                            {
                                MyGlobal.GenerateRightMenu4CopyOnly(false, _cMenuSchemaBrowser, c1GridSchemaBrowser, editor, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }
                            else
                            {
                                MyGlobal.GenerateRightMenu4Copy_PostgreSQL(false, c1GridSchemaBrowser, _cMenuSchemaBrowser, editor, AccessibleDescription, sSchemaNode, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }

                            break;
                        }
                    case "SQL Server":
                        {
                            if (sSchemaType.StartsWith("Functions") || sSchemaType.StartsWith("Triggers") || sSchemaType.StartsWith("Indices") || sSchemaType.StartsWith("Procedures"))
                            {
                                MyGlobal.GenerateRightMenu4CopyOnly(false, _cMenuSchemaBrowser, c1GridSchemaBrowser, editor, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }
                            else
                            {
                                MyGlobal.GenerateRightMenu4Copy_SQLServer(false, c1GridSchemaBrowser, _cMenuSchemaBrowser, editor, AccessibleDescription, sSchemaNode, sSchemaDbo, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y, sObjectID);
                            }

                            break;
                        }
                    case "MySQL":
                        {
                            if (sSchemaType.StartsWith("Functions") || sSchemaType.StartsWith("Triggers") || sSchemaType.StartsWith("Indices") || sSchemaType.StartsWith("Procedures"))
                            {
                                MyGlobal.GenerateRightMenu4CopyOnly(false, _cMenuSchemaBrowser, c1GridSchemaBrowser, editor, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }
                            else
                            {
                                MyGlobal.GenerateRightMenu4Copy_MySQL(false, c1GridSchemaBrowser, _cMenuSchemaBrowser, editor, AccessibleDescription, sSchemaNode, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }

                            break;
                        }
                    case "SQLite":
                    case "SQLCipher":
                        {
                            if (sSchemaType.StartsWith("Functions") || sSchemaType.StartsWith("Triggers") || sSchemaType.StartsWith("Indices") || sSchemaType.StartsWith("Procedures"))
                            {
                                MyGlobal.GenerateRightMenu4CopyOnly(false, _cMenuSchemaBrowser, c1GridSchemaBrowser, editor, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }
                            else
                            {
                                MyGlobal.GenerateRightMenu4Copy_SQLite(false, c1GridSchemaBrowser, _cMenuSchemaBrowser, editor, AccessibleDescription, sSchemaNode, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }

                            break;
                        }
                }
            }
            else //滑鼠所在處並不是節點！
            {
                //20220418：原廠給的改善寫法
                //NG: sSchemaType = c1GridSchemaBrowser.Columns["SchemaType"].CellValue(vr.DataRowIndex).ToString();
                sSchemaType = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["SchemaType"].ToString();

                if (sSchemaType.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
                {
                    sSchemaType = sSchemaType.Substring(0, sSchemaType.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
                }

                //NG: sSchemaName = c1GridSchemaBrowser.Columns["Schema_Browser"].CellValue(vr.DataRowIndex).ToString();
                sSchemaName = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["Schema_Browser"].ToString();

                if (sSchemaName.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
                {
                    sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
                }
                else if (sSchemaName.IndexOf(", ", StringComparison.Ordinal) != -1) //欄位名稱
                {
                    sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf(", ", StringComparison.Ordinal));
                }

                if (string.IsNullOrEmpty(sSchemaName))
                {
                    //如果使用者有開啟「顯示欄位名稱」，除了「Table」外，其餘的項目，sSchemaName 都會是空值
                    return;
                }

                var bContinue = true;

                switch (MyGlobal.sDataSource)
                {
                    case "Oracle" when MyGlobal.bShowColumnInfo:
                    case "PostgreSQL" when MyGlobal.bShowColumnInfo:
                    case "SQL Server" when MyGlobal.bShowColumnInfo:
                    case "MySQL" when MyGlobal.bShowColumnInfo:
                    case "SQLite" when MyGlobal.bShowColumnInfo:
                    case "SQLCipher" when MyGlobal.bShowColumnInfo:
                        break; //不是節點，有顯示 Column Info，如果 sSchemaName 不是空值，就顯示右鍵功能表
                    case "Oracle":
                        {
                            if (sSchemaType == "Views" || sSchemaType == "Tables")
                            {
                                bContinue = false;
                                MyGlobal.GenerateRightMenu4Copy_Oracle(false, c1GridSchemaBrowser, _cMenuSchemaBrowser, editor, AccessibleDescription, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }

                            break;
                        }
                    case "PostgreSQL":
                        {
                            sSchemaNode = c1GridSchemaBrowser.Columns["SchemaNode"].CellValue(vr.DataRowIndex).ToString();

                            if (sSchemaType == "Views" || sSchemaType == "Tables")
                            {
                                bContinue = false;
                                MyGlobal.GenerateRightMenu4Copy_PostgreSQL(false, c1GridSchemaBrowser, _cMenuSchemaBrowser, editor, AccessibleDescription, sSchemaNode, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }

                            break;
                        }
                    case "SQL Server":
                        {
                            if (sSchemaType == "Views" || sSchemaType == "Tables")
                            {
                                bContinue = false;
                                //var dtSchema2 = (DataTable)c1GridSchemaBrowser.DataSource;
                                //改寫、修正
                                sSchemaNode = c1GridSchemaBrowser.Columns["SchemaNode"].CellValue(vr.DataRowIndex).ToString();
                                sSchemaDbo = c1GridSchemaBrowser.Columns["SchemaDbo"].CellValue(vr.DataRowIndex).ToString();
                                sObjectID = c1GridSchemaBrowser.Columns["ObjectID"].CellValue(vr.DataRowIndex).ToString();

                                MyGlobal.GenerateRightMenu4Copy_SQLServer(false, c1GridSchemaBrowser, _cMenuSchemaBrowser, editor, AccessibleDescription, sSchemaNode, sSchemaDbo, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y, sObjectID);
                            }

                            break;
                        }
                    case "MySQL":
                        {
                            sSchemaNode = c1GridSchemaBrowser.Columns["SchemaNode"].CellValue(vr.DataRowIndex).ToString();

                            if (sSchemaType == "Views" || sSchemaType == "Tables")
                            {
                                bContinue = false;
                                MyGlobal.GenerateRightMenu4Copy_MySQL(false, c1GridSchemaBrowser, _cMenuSchemaBrowser, editor, AccessibleDescription, sSchemaNode, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }

                            break;
                        }
                    case "SQLite":
                    case "SQLCipher":
                        {
                            sSchemaNode = c1GridSchemaBrowser.Columns["SchemaNode"].CellValue(vr.DataRowIndex).ToString();

                            if (sSchemaType == "Views" || sSchemaType == "Tables")
                            {
                                bContinue = false;
                                MyGlobal.GenerateRightMenu4Copy_SQLite(false, c1GridSchemaBrowser, _cMenuSchemaBrowser, editor, AccessibleDescription, sSchemaNode, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }

                            break;
                        }
                }

                if (bContinue)
                {
                    MyGlobal.GenerateRightMenu4CopyOnly(false, _cMenuSchemaBrowser, c1GridSchemaBrowser, editor, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                }
            }
        }

        private void cboFindGrid_BeforeDropDownOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoadFindList("Grid", cboFindGrid); //cboFindGrid_BeforeDropDownOpen
        }

        private void AutoResizeGridColumnWidth()
        {
            var iWidth = 0;

            if ((DataTable)c1GridSchemaBrowser.DataSource == null)
            {
                return;
            }

            var i = 3 + (MyGlobal.bShowColumnInfo ? 1 : 0);

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    i--;
                    iWidth = 96 + (MyGlobal.bShowColumnInfo ? 10 : 0);
                    break;
                case "PostgreSQL":
                    iWidth = 110 + (MyGlobal.bShowColumnInfo ? 10 : 0);
                    break;
                case "SQL Server":
                    iWidth = 110 + (MyGlobal.bShowColumnInfo ? 10 : 0);
                    break;
                case "MySQL":
                    iWidth = 110 + (MyGlobal.bShowColumnInfo ? 10 : 0);
                    break;
                case "SQLite":
                case "SQLCipher":
                    iWidth = 75;
                    break;
            }

            c1GridSchemaBrowser.Splits[_iii].DisplayColumns[i].Width = splitContainer2.Panel1.Width - iWidth;

            if (c1GridSchemaBrowser.Splits[_iii].DisplayColumns[i].Width < 60)
            {
                c1GridSchemaBrowser.Splits[_iii].DisplayColumns[i].Width = 60;
            }

            c1GridSchemaBrowser.Refresh();

            if (!MyLibrary.bEnableAutoReplace)
            {
                return;
            }

            c1GridARInfo.Splits[_iii].DisplayColumns[2].Width = splitContainer2.Panel1.Width - 136;

            if (c1GridARInfo.Splits[_iii].DisplayColumns[2].Width < 60)
            {
                c1GridARInfo.Splits[_iii].DisplayColumns[2].Width = 60;
            }

            c1GridARInfo.Refresh();
        }

        private void HideACGrid(bool bValue = true)
        {
            if (bValue && _sPosXY4AC == Cursor.Position.X + ", " + Cursor.Position.Y) //輸入過程中，若滑鼠沒移動，則忽略
            {
                return;
            }

            c1GridAC4Period1.Visible = false;
            c1GridAC4Space1.Visible = false;
            c1GridAC4All.Visible = false;
        }
        //end of frmQuery, 1231
    }

    internal static class Win32Api
    {
        public const int SWP_NOSIZE = 0x1;
        public const int SWP_NOZORDER = 0x4;

        [DllImport("user32")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32")]
        public static extern int IsWindowVisible(IntPtr hwnd);

        [DllImport("user32")]
        public static extern int SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int cx, int cy, int wFlags);
    }
}