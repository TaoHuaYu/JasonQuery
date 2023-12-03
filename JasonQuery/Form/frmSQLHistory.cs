using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using C1.Win.C1TrueDBGrid.Excel;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Diagnostics;
using JasonLibrary.Class;
using JasonLibrary.Stylers;

namespace JasonQuery
{
    public partial class frmSQLHistory : Form
    {
        private DataTable _dtSqlHistoryInfo;
        private DataRow rowDbInfo;
        private DataTable dt;
        public event ValueUpdatedEventHandler ValueUpdated;
        private ContextMenuStrip _gMenu = new ContextMenuStrip(); //Grid
        private ContextMenuStrip _fMenu = new ContextMenuStrip(); //Editor
        private ContextMenuStrip _eMenu = new ContextMenuStrip(); //Editor
        private List<string> _lstGridHeader = new List<string>();

        private FindReplace.FindReplace myFindReplace; //20230629
        private int _rowHeight; // original row height
        private int _recSelWidth; // oringal record selector width
        private float _fontSize; // original font size

        private bool ctrlKeyDown;
        private int _totalDelta;
        private bool _bFormLoadFinish; //表單是否載入完畢 (避免觸發事件)
        private int _iSelectedCount;

        private Dictionary<string, string> dicHistoryPeriod = new Dictionary<string, string>();

        private enum eColumn
        {
            Select = 0,
            PID,
            MPID,
            DataSource,
            ConnectionName,
            ExecutionDate,
            ExecutionTime,
            QueryTime,
            Rows,
            Result,
            Message,
            SQLStatement
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(IntPtr classname, string title); // extern method: FindWindow

        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public const int WM_CLOSE = 0x10;

        [DllImport("user32.dll")]
        private static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool rePaint); // extern method: MoveWindow

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rect); // extern method: GetWindowRect

        public frmSQLHistory()
        {
            InitializeComponent();
        }

        private void ApplyLocalizationSetting()
        {
            MyGlobal.ApplyLanguageInfo(this); //ApplyLocalizationSetting

            var toolTip1 = new ToolTip { ForeColor = Color.Blue, BackColor = Color.Gray, AutoPopDelay = 5000 };
            var sLangText = MyGlobal.GetLanguageString("Search SQL History", "form", Name, "object", "btnSearch", "ToolTipText");
            toolTip1.SetToolTip(btnSearch, sLangText);
            sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "object", "btnSelectAll", "ToolTipText");
            toolTip1.SetToolTip(btnSelectAll, sLangText);
            sLangText = MyGlobal.GetLanguageString("Unselect All", "form", Name, "object", "btnUnselectAll", "ToolTipText");
            toolTip1.SetToolTip(btnUnselectAll, sLangText);
            sLangText = MyGlobal.GetLanguageString("Delete Selected Records", "form", Name, "object", "btnDelete", "ToolTipText");
            toolTip1.SetToolTip(btnDelete, sLangText);

            _bFormLoadFinish = false;

            //以下顏色要重新指定，否則 CheckBox 底色會不一樣
            toolStrip1.BackColor = SystemColors.Control;
            chkShowFilterRow.BackColor = toolStrip1.BackColor;
            toolStrip2.BackColor = SystemColors.Control;
            chkCopyAsHTML.BackColor = toolStrip2.BackColor;
            pnlHideCheckBox.BackColor = Color.White;

            if (MyLibrary.bDarkMode)
            {
                C1.Win.C1Themes.C1ThemeController.ApplicationTheme = "VS2013Dark";

                chkShowFilterRow.Visible = !MyLibrary.bDarkMode;
                chkShowFilterRow.Visible = MyLibrary.bDarkMode;
                chkCopyAsHTML.Visible = !MyLibrary.bDarkMode;
                chkCopyAsHTML.Visible = MyLibrary.bDarkMode;

                toolStrip1.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);
                chkCopyAsHTML.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);
                chkShowFilterRow.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);
                toolStrip2.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);

                cboDataSource.BackColor = ColorTranslator.FromHtml("#707070"); //#2D2D30=極深黑
                cboConnectionName.BackColor = ColorTranslator.FromHtml("#707070"); //#707070=深黑
                cboHistoryPeriod.BackColor = ColorTranslator.FromHtml("#707070");

                c1GridSQLHistory.FilterBarStyle.BackColor = ColorTranslator.FromHtml("#2D2D30"); //#2D2D30=極深黑
                c1GridSQLHistory.FilterBarStyle.ForeColor = Color.White;
                pnlHideCheckBox.BackColor = ColorTranslator.FromHtml("#2D2D30"); //#2D2D30=極深黑
            }

            _gMenu = new ContextMenuStrip();

            //右鍵選單
            sLangText = MyGlobal.GetLanguageString("Export All Data to File", "form", Name, "menugrid", "ExportAllDataToFile", "Text");
            _gMenu.Items.Add(sLangText);

            _gMenu.Items[0].Click += delegate
            {
                ExportToFile();
            };

            _eMenu = new ContextMenuStrip();

            //Begin:右鍵選單
            sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "menueditor", "SelectAll", "Text");
            _eMenu.Items.Add(sLangText);
            ((ToolStripMenuItem) _eMenu.Items[0]).ShortcutKeys = (Keys.Control | Keys.A);

            _eMenu.Items[0].Click += delegate
            {
                editorSQL.SelectionStart = 0;
                editorSQL.SelectionEnd = editorSQL.Text.Length;
                //editorCellViewer.SelectAll();
            };

            _eMenu.Items.Add("-");

            sLangText = MyGlobal.GetLanguageString("Copy", "form", Name, "menueditor", "Copy", "Text");
            _eMenu.Items.Add(sLangText);
            ((ToolStripMenuItem) _eMenu.Items[2]).ShortcutKeys = (Keys.Control | Keys.C);

            _eMenu.Items[2].Click += delegate
            {
                if (chkCopyAsHTML.Checked)
                {
                    Clipboard.SetDataObject(" ", false);
                    editorSQL.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                }
                else
                {
                    editorSQL.Copy();
                }
            };
            //End:右鍵選單

            //Begin:右鍵選單
            sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "menueditor", "SelectAll", "Text");
            _fMenu.Items.Add(sLangText);
            ((ToolStripMenuItem) _fMenu.Items[0]).ShortcutKeys = (Keys.Control | Keys.A);

            _fMenu.Items[0].Click += delegate
            {
                editorMessage.SelectionStart = 0;
                editorMessage.SelectionEnd = editorMessage.Text.Length;
            };

            _fMenu.Items.Add("-");

            sLangText = MyGlobal.GetLanguageString("Copy", "form", Name, "menueditor", "Copy", "Text");
            _fMenu.Items.Add(sLangText);
            ((ToolStripMenuItem) _fMenu.Items[2]).ShortcutKeys = (Keys.Control | Keys.C);

            _fMenu.Items[2].Click += delegate
            {
                if (chkCopyAsHTML.Checked)
                {
                    Clipboard.SetDataObject(" ", false);
                    editorMessage.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                }
                else
                {
                    editorMessage.Copy();
                }
            };
            //End:右鍵選單

            dicHistoryPeriod = new Dictionary<string, string>();

            //Begin:定義區間的設定值
            var s1 = MyGlobal.GetLanguageString("within 7 days", "form", Name, "dropdownlist", "HistoryPeriod_within7days", "Text");
            dicHistoryPeriod.Add("within 7 days", s1);

            sLangText = MyGlobal.GetLanguageString("within 14 days", "form", Name, "dropdownlist", "HistoryPeriod_within14days", "Text");
            dicHistoryPeriod.Add("within 14 days", sLangText);

            sLangText = MyGlobal.GetLanguageString("within 30 days", "form", Name, "dropdownlist", "HistoryPeriod_within30days", "Text");
            dicHistoryPeriod.Add("within 30 days", sLangText);

            sLangText = MyGlobal.GetLanguageString("before 30 days", "form", Name, "dropdownlist", "HistoryPeriod_before30days", "Text");
            dicHistoryPeriod.Add("before 30 days", sLangText);

            sLangText = MyGlobal.GetLanguageString("All", "form", Name, "dropdownlist", "HistoryPeriod_All", "Text");
            dicHistoryPeriod.Add("All", sLangText);

            MyGlobal.SetC1ComboBoxItemsFromDictionary(cboHistoryPeriod, dicHistoryPeriod);
            cboHistoryPeriod.Text = s1;
            //End:定義區間的設定值

            if (_bFormLoadFinish)
            {
                CreateAndGetSqlHistoryInfoTable(); //ApplyLocalizationSetting
            }

            c1GridSQLHistory.AllowRowSizing = MyGlobal.GetKeyFromDictionary(MyGlobal.dicRowSizing, MyGlobal.sRowSizing) == "AllRows" ? RowSizingEnum.AllRows : RowSizingEnum.IndividualRows;

            //套用 Editor 外觀
            ApplyEditorSetting();
            ApplySqlStyler();

            //套用 Grid 外觀
            //20211030 以下兩個 17 必須是固定值，否則每次「寬、高」都會變動，造成「CheckBox 遮蓋失效」
            _rowHeight = 17; //c1GridSQLHistory.RowHeight;
            _recSelWidth = 17; //c1GridSQLHistory.RecordSelectorWidth;
            float.TryParse(MyLibrary.sGridFontSize, out _fontSize);

            GridVisualStyle(); //ApplyLocalizationSetting
            GridFontAndBackgroundColor(); //ApplyLocalizationSetting
            GridZoom(); //ApplyLocalizationSetting

            c1GridSQLHistory.AllowFilter = false;
            c1GridSQLHistory.Filter += C1TrueDBGrid_Filter;

            cboConnectionName.Text = MyGlobal.sDBConnectionName;
            //End:取得此使用者所有的 Connection Name 設定值

            cboDataSource.Location = new Point(lblDataSource.Left + lblDataSource.Width, cboDataSource.Top);
            lblConnectionName.Location = new Point(cboDataSource.Left + cboDataSource.Width + 20, lblConnectionName.Top);
            cboConnectionName.Location = new Point(lblConnectionName.Left + lblConnectionName.Width, cboConnectionName.Top);
            lblHistoryPeriod.Location = new Point(cboConnectionName.Left + cboConnectionName.Width + 20, lblHistoryPeriod.Top);
            cboHistoryPeriod.Location = new Point(lblHistoryPeriod.Left + lblHistoryPeriod.Width, cboHistoryPeriod.Top);
            picSeparator1.Location = new Point(cboHistoryPeriod.Left + cboHistoryPeriod.Width + 10, picSeparator1.Top);
            btnSearch.Location = new Point(picSeparator1.Left + picSeparator1.Width - 2, btnSearch.Top);
            btnSelectAll.Location = new Point(btnSearch.Left + btnSearch.Width + 10, btnSelectAll.Top);
            btnUnselectAll.Location = new Point(btnSelectAll.Left + btnSelectAll.Width + 10, btnUnselectAll.Top);
            btnDelete.Location = new Point(btnUnselectAll.Left + btnUnselectAll.Width + 10, btnDelete.Top);
            picSeparator2.Location = new Point(btnDelete.Left + btnDelete.Width + 10, picSeparator2.Top);
            chkShowFilterRow.Location = new Point(picSeparator2.Left + picSeparator2.Width -2, chkShowFilterRow.Top);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ApplyLocalizationSetting(); //Form_Load

            myFindReplace = new FindReplace.FindReplace();
            myFindReplace.Scintilla = editorSQL;

            //Start:取得下拉選單項目
            cboDataSource.Items.Add("*");

            var sSql = "SELECT DISTINCT DataSource FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' ORDER BY DataSource";
            var dtData = DBCommon.ExecQuery(sSql);

            for (var i = 0; i < dtData.Rows.Count; i++)
            {
                cboDataSource.Items.Add(dtData.Rows[i]["DataSource"].ToString());
            }

            cboDataSource.SelectedIndex = 0; //預設 *
            //End:取得下拉選單項目

            chkCopyAsHTML.Checked = MyLibrary.bCopyAsHTML;

            btnWordWrap.Visible = (MyLibrary.bWordWrap == false);
            btnWordWrap2.Visible = !btnWordWrap.Visible;
            editorSQL.WrapMode = (MyLibrary.bWordWrap == false) ? ScintillaNET.WrapMode.None : ScintillaNET.WrapMode.Word;
            editorSQL.WrapVisualFlags = (MyLibrary.bWordWrapVisualFlags_Start ? ScintillaNET.WrapVisualFlags.Start : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_End ? ScintillaNET.WrapVisualFlags.End : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_Margin ? ScintillaNET.WrapVisualFlags.Margin : ScintillaNET.WrapVisualFlags.None);

            //Message 不要折行，因為有可能包含「錯誤定位點」
            //editorMessage.WrapMode = (MyLibrary.bWordWrap == false) ? ScintillaNET.WrapMode.None : ScintillaNET.WrapMode.Word;
            //editorMessage.WrapVisualFlags = (MyLibrary.bWordWrapVisualFlags_Start ? ScintillaNET.WrapVisualFlags.Start : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_End ? ScintillaNET.WrapVisualFlags.End : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_Margin ? ScintillaNET.WrapVisualFlags.Margin : ScintillaNET.WrapVisualFlags.None);

            //Begin:取得此使用者所有的 Connection Name 設定值
            cboConnectionName.Items.Add("*");
            sSql = "SELECT ConnectionName AS DisplayName, PID AS DisplayValue FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' ORDER BY ConnectionName";
            dtData = DBCommon.ExecQuery(sSql);
            //MyGlobal.GetC1ComboBoxWithValueAndID(cboConnectionName, dt, true, true); //SQLHistory, Form_Load

            for (var i = 0; i < dtData.Rows.Count; i++)
            {
                cboConnectionName.Items.Add(dtData.Rows[i]["DisplayName"].ToString());
            }

            CreateAndGetSqlHistoryInfoTable(); //frmSQLHistory_Load, 預設先取 7天內的資料, " and [ExecutionDate] > Date() - 7"

            //設定放大縮小功能
            c1GridSQLHistory.MouseWheel += c1GridSQLHistory_MouseWheel;

            _bFormLoadFinish = true;

            btnSearch.PerformClick();
        }

        private void C1TrueDBGrid_Filter(object sender, FilterEventArgs e)
        {
            var dataView = (c1GridSQLHistory.DataSource as DataTable)?.DefaultView;

            if (dataView == null || dataView.RowFilter == e.Condition)
            {
                return;
            }

            var condition = e.Condition;

            if (condition.Length != 0)
            {
                condition = e.Condition;

                for (var i = 0; i < c1GridSQLHistory.Splits[0].DisplayColumns.Count; i++)
                {
                    if (!condition.Contains($"[{c1GridSQLHistory.Columns[i].Caption}]"))
                    {
                        continue;
                    }

                    var paramIndex = condition.IndexOf('\'', condition.IndexOf($"[{c1GridSQLHistory.Columns[i].Caption}]", StringComparison.Ordinal)) + 1;
                    condition = condition.Insert(paramIndex, "*");
                }
            }

            dataView.RowFilter = condition;
        }

        private void btnWordWrap_Click(object sender, EventArgs e)
        {
            btnWordWrap.Visible = editorSQL.WrapMode == ScintillaNET.WrapMode.Word;
            btnWordWrap2.Visible = !btnWordWrap.Visible;
            editorSQL.WrapMode = editorSQL.WrapMode == ScintillaNET.WrapMode.Word ? ScintillaNET.WrapMode.None : ScintillaNET.WrapMode.Word;
            editorSQL.WrapVisualFlags = (MyLibrary.bWordWrapVisualFlags_Start ? ScintillaNET.WrapVisualFlags.Start : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_End ? ScintillaNET.WrapVisualFlags.End : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_Margin ? ScintillaNET.WrapVisualFlags.Margin : ScintillaNET.WrapVisualFlags.None);
           
            //Message 不要折行，因為有可能包含「錯誤定位點」
            //editorMessage.WrapMode = editorSQL.WrapMode;
            //editorMessage.WrapVisualFlags = (MyLibrary.bWordWrapVisualFlags_Start ? ScintillaNET.WrapVisualFlags.Start : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_End ? ScintillaNET.WrapVisualFlags.End : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_Margin ? ScintillaNET.WrapVisualFlags.Margin : ScintillaNET.WrapVisualFlags.None);

            //加上以下這個指令，取消 Word Wrap 後，Focus 才不會跑到最底部！
            editorSQL.ScrollCaret();
        }

        private void btnShowAllCharacters_Click(object sender, EventArgs e)
        {
            btnShowAllCharacters.Visible = !btnShowAllCharacters.Visible;
            btnShowAllCharacters2.Visible = !btnShowAllCharacters.Visible;
            editorSQL.ViewEol = !btnShowAllCharacters.Visible;
            editorSQL.ViewWhitespace = btnShowAllCharacters.Visible ? ScintillaNET.WhitespaceMode.Invisible : ScintillaNET.WhitespaceMode.VisibleAlways;
            editorMessage.ViewEol = !btnShowAllCharacters.Visible;
            editorMessage.ViewWhitespace = btnShowAllCharacters.Visible ? ScintillaNET.WhitespaceMode.Invisible : ScintillaNET.WhitespaceMode.VisibleAlways;
        }

        private void CreateAndGetSqlHistoryInfoTable()
        {
            var sDateCondition = "";
            var sDbTypeCondition2 = "";
            var sConnectionNameCondition = "";
            const string sHideFields = "`PID`MPID`";
            var bValue = false;

            Cursor = Cursors.WaitCursor;

            dt = new DataTable();

            switch (MyGlobal.GetKeyFromDictionary(dicHistoryPeriod, cboHistoryPeriod.Text))
            {
                case "within 7 days":
                    sDateCondition = " AND yy.ExecutionDate > '" + DateTime.Today.AddDays(-7).ToString("yyyy/MM/dd") + "'";
                    break;
                case "within 14 days":
                    sDateCondition = " AND yy.ExecutionDate > '" + DateTime.Today.AddDays(-14).ToString("yyyy/MM/dd") + "'";
                    break;
                case "within 30 days":
                    sDateCondition = " AND yy.ExecutionDate > '" + DateTime.Today.AddDays(-30).ToString("yyyy/MM/dd") + "'";
                    break;
                case "before 30 days":
                    sDateCondition = " AND yy.ExecutionDate < '" + DateTime.Today.AddDays(-30).ToString("yyyy/MM/dd") + "'";
                    break;
            }

            _lstGridHeader = new List<string> {" ", "PID", "MPID"};

            var sLangText = MyGlobal.GetLanguageString("Data Source", "form", Name, "gridheader", "DataSource", "Text");
            _lstGridHeader.Add(sLangText);
            sLangText = MyGlobal.GetLanguageString("Connection Name", "form", Name, "gridheader", "ConnectionName", "Text");
            _lstGridHeader.Add(sLangText);
            sLangText = MyGlobal.GetLanguageString("Execute Date", "form", Name, "gridheader", "ExecutionDate", "Text");
            _lstGridHeader.Add(sLangText);
            sLangText = MyGlobal.GetLanguageString("Execute Time", "form", Name, "gridheader", "ExecutionTime", "Text");
            _lstGridHeader.Add(sLangText);
            sLangText = MyGlobal.GetLanguageString("Query Time", "form", Name, "gridheader", "QueryTime", "Text");
            _lstGridHeader.Add(sLangText);
            sLangText = MyGlobal.GetLanguageString("Rows", "form", Name, "gridheader", "Rows", "Text");
            _lstGridHeader.Add(sLangText);
            sLangText = MyGlobal.GetLanguageString("Result", "form", Name, "gridheader", "Result", "Text");
            _lstGridHeader.Add(sLangText);
            sLangText = MyGlobal.GetLanguageString("Message", "form", Name, "gridheader", "Message", "Text");
            _lstGridHeader.Add(sLangText);
            sLangText = MyGlobal.GetLanguageString("SQL Statement", "form", Name, "gridheader", "SQLStatement", "Text");
            _lstGridHeader.Add(sLangText);

            _dtSqlHistoryInfo = new DataTable();

            _dtSqlHistoryInfo.Columns.Add(_lstGridHeader[(int)eColumn.Select]);
            _dtSqlHistoryInfo.Columns.Add(_lstGridHeader[(int)eColumn.PID]);
            _dtSqlHistoryInfo.Columns.Add(_lstGridHeader[(int)eColumn.MPID]);
            _dtSqlHistoryInfo.Columns.Add(_lstGridHeader[(int)eColumn.DataSource]);
            _dtSqlHistoryInfo.Columns.Add(_lstGridHeader[(int)eColumn.ConnectionName]);
            _dtSqlHistoryInfo.Columns.Add(_lstGridHeader[(int)eColumn.ExecutionDate]);
            _dtSqlHistoryInfo.Columns.Add(_lstGridHeader[(int)eColumn.ExecutionTime]);
            _dtSqlHistoryInfo.Columns.Add(_lstGridHeader[(int)eColumn.QueryTime]);
            _dtSqlHistoryInfo.Columns.Add(_lstGridHeader[(int)eColumn.Rows]);
            _dtSqlHistoryInfo.Columns.Add(_lstGridHeader[(int)eColumn.Result]);
            _dtSqlHistoryInfo.Columns.Add(_lstGridHeader[(int)eColumn.Message]);
            _dtSqlHistoryInfo.Columns.Add(_lstGridHeader[(int)eColumn.SQLStatement]);

            //判斷 DB Type
            if (cboDataSource.Text != @"*")
            {
                sDbTypeCondition2 = " AND oo.DataSource='" + cboDataSource.Text + "'";
            }

            //判斷 Connection Name
            if (cboConnectionName.Text != @"*")
            {
                sConnectionNameCondition = " AND oo.ConnectionName='" + cboConnectionName.Text + "'";
            }

            //預設，取 7 天內的資料
            var sSql = "SELECT yy.PID, oo.PID AS MPID, oo.DataSource, oo.ConnectionName, yy.ExecutionDate, yy.ExecutionTime, yy.QueryTime, yy.Rows, yy.Result, yy.Message, yy.SQL FROM SQLHistory yy, DBInfo oo WHERE oo.PID = yy.MPID" + sDbTypeCondition2 + " AND oo.DomainUser='" + MyGlobal.sDomainUser + "'" + sConnectionNameCondition + sDateCondition + " ORDER BY yy.ExecutionDate DESC";
            dt = DBCommon.ExecQuery(sSql);

            if (dt.Rows.Count > 0)
            {
                for (var iRow = 0; iRow < dt.Rows.Count; iRow++)
                {
                    rowDbInfo = _dtSqlHistoryInfo.NewRow();
                    rowDbInfo[_lstGridHeader[(int)eColumn.Select]] = "0";
                    rowDbInfo[_lstGridHeader[(int)eColumn.PID]] = dt.Rows[iRow]["PID"].ToString();
                    rowDbInfo[_lstGridHeader[(int)eColumn.MPID]] = dt.Rows[iRow]["MPID"].ToString();
                    rowDbInfo[_lstGridHeader[(int)eColumn.DataSource]] = dt.Rows[iRow]["DataSource"].ToString();
                    rowDbInfo[_lstGridHeader[(int)eColumn.ConnectionName]] = dt.Rows[iRow]["ConnectionName"].ToString();
                    rowDbInfo[_lstGridHeader[(int)eColumn.ExecutionDate]] = Convert.ToDateTime(dt.Rows[iRow]["ExecutionDate"].ToString()).ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                    rowDbInfo[_lstGridHeader[(int)eColumn.ExecutionTime]] = dt.Rows[iRow]["ExecutionTime"].ToString();
                    rowDbInfo[_lstGridHeader[(int)eColumn.QueryTime]] = dt.Rows[iRow]["QueryTime"].ToString();
                    rowDbInfo[_lstGridHeader[(int)eColumn.Rows]] = dt.Rows[iRow]["Rows"].ToString();
                    rowDbInfo[_lstGridHeader[(int)eColumn.Result]] = dt.Rows[iRow]["Result"].ToString();
                    rowDbInfo[_lstGridHeader[(int)eColumn.Message]] = dt.Rows[iRow]["Message"].ToString();
                    rowDbInfo[_lstGridHeader[(int)eColumn.SQLStatement]] = dt.Rows[iRow]["SQL"].ToString();

                    _dtSqlHistoryInfo.Rows.Add(rowDbInfo);
                }
            }

            c1GridSQLHistory.DataSource = _dtSqlHistoryInfo;

            foreach (C1DisplayColumn col in c1GridSQLHistory.Splits[0].DisplayColumns)
            {
                if (sHideFields.Contains("`" + col.Name + "`"))
                {
                    col.Visible = false;
                    col.Frozen = true;
                }
                else
                {
                    try
                    {
                        col.AutoSize();
                    }
                    catch (Exception)
                    {
                        col.Width = 2000;
                    }

                    if (col.Name == _lstGridHeader[(int)eColumn.Select])
                    {
                        col.AllowSizing = false;
                        col.Frozen = true;
                    }
                    else if (col.Name == _lstGridHeader[(int)eColumn.Message])
                    {
                        col.Width = 200;
                    }
                    else if (col.Name == _lstGridHeader[(int)eColumn.SQLStatement])
                    {
                        col.Width = 600;
                    }
                }
            }

            SetCheckBox(" ");
            c1GridSQLHistory.Splits[0].DisplayColumns[" "].Style.HorizontalAlignment = AlignHorzEnum.Center;

            if (c1GridSQLHistory.RowCount > 0)
            {
                bValue = true;
            }

            btnSelectAll.Enabled = bValue;
            btnUnselectAll.Enabled = bValue;
            btnDelete.Enabled = false;

            Cursor = Cursors.Default;
        }

        private void TransferValueToMainForm(string sValue)
        {
            //使用時機：
            //選定某一個SQL，按下右鍵，傳送至「SQL Editor」
            //使用方式如下範例：
            //uTransferValueToMainForm("transferselectsql`" + "要傳送的 SQL 內容");

            var valueArgs = new ValueUpdatedEventArgs(sValue);
            ValueUpdated(this, valueArgs);
        }

        private void chkShowFilterRow_Click(object sender, EventArgs e)
        {
            c1GridSQLHistory.FilterBar = chkShowFilterRow.Checked;
            pnlHideCheckBox.Visible = chkShowFilterRow.Checked;
        }

        private void c1GridSQLHistory_RowColChange(object sender, RowColChangeEventArgs e)
        {
            editorMessage.ReadOnly = false;
            editorMessage.Text = c1GridSQLHistory.Columns[_lstGridHeader[(int)eColumn.Message]].CellValue(c1GridSQLHistory.Row).ToString();
            editorMessage.ReadOnly = true;
            editorMessage.SelectionStart = 0;
            editorMessage.ScrollCaret();

            editorSQL.ReadOnly = false;
            editorSQL.Text = c1GridSQLHistory.Columns[_lstGridHeader[(int)eColumn.SQLStatement]].CellValue(c1GridSQLHistory.Row).ToString();
            editorSQL.ReadOnly = true;
            editorSQL.SelectionStart = 0;
            editorSQL.ScrollCaret();
        }

        private void ApplyEditorSetting()
        {
            editorSQL.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
            editorSQL.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editorSQL.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));

            editorSQL.Zoom = Convert.ToInt16(MyLibrary.sQueryEditorZoom);

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

            editorSQL.Styler = new SqlStyler(); //sqlstyler()：變更關鍵字、顏色
        }

        private void GridVisualStyle()
        {
            var sStyle = MyLibrary.bDarkMode ? "Office 2010 Black" : MyLibrary.sGridVisualStyle;

            switch (sStyle)
            {
                case "Office 2007 Blue":
                    c1GridSQLHistory.VisualStyle = VisualStyle.Office2007Blue;

                    break;
                case "Office 2007 Silver":
                    c1GridSQLHistory.VisualStyle = VisualStyle.Office2007Silver;

                    break;
                case "Office 2007 Black":
                    c1GridSQLHistory.VisualStyle = VisualStyle.Office2007Black;

                    break;
                case "Office 2010 Blue":
                    c1GridSQLHistory.VisualStyle = VisualStyle.Office2010Blue;

                    break;
                case "Office 2010 Silver":
                    c1GridSQLHistory.VisualStyle = VisualStyle.Office2010Silver;

                    break;
                case "Office 2010 Black":
                    c1GridSQLHistory.VisualStyle = VisualStyle.Office2010Black;

                    break;
                default:
                    c1GridSQLHistory.VisualStyle = VisualStyle.Office2010Blue;

                    break;
            }

            c1GridSQLHistory.Splits[0].ColumnCaptionHeight = 25;
        }

        private void GridFontAndBackgroundColor()
        {
            _fontSize = 12;

            //字型 + 字體大小
            c1GridSQLHistory.Font = new Font(MyLibrary.sGridFontName, _fontSize, FontStyle.Regular, GraphicsUnit.Point);

            c1GridSQLHistory.OddRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowForeColor);
            c1GridSQLHistory.OddRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowBackColor);
            c1GridSQLHistory.EvenRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowForeColor);
            c1GridSQLHistory.EvenRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);

            //Grid's 選取顏色
            c1GridSQLHistory.SelectedStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedForeColor);
            c1GridSQLHistory.SelectedStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedBackColor);
        }

        private void GridZoom()
        {
            float.TryParse("0.9", out var pcnt); //MyLibrary.sGridZoom

            if (_fontSize == 0)
            {
                _fontSize = 12;
            }

            //adjust row height
            c1GridSQLHistory.RowHeight = (int)(_rowHeight * pcnt) + 5;

            //標題列的高度
            c1GridSQLHistory.Splits[0].ColumnCaptionHeight = (int)(_rowHeight * pcnt) + 7;

            //and recordselector width
            c1GridSQLHistory.RecordSelectorWidth = (int)(_recSelWidth * pcnt);

            //adjust font sizes.  Normal is the root style so changing its sizes adjust all other styles
            c1GridSQLHistory.Styles["Normal"].Font = new Font(c1GridSQLHistory.Styles["Normal"].Font.FontFamily, _fontSize * pcnt);
        }

        private void c1GridSQLHistory_MouseWheel(object sender, MouseEventArgs e)
        {
            //The amount by which we adjust scale per wheel click.
            //const float scalePerDelta = 10f / 120;

            //Update the drawing based upon the mouse wheel scrolling.
            //float imageScale = e.Delta * scalePerDelta;

            if (!ctrlKeyDown)
            {
                return;
            }

            _totalDelta += e.Delta;
            var fValue = 1 + ((float)(SystemInformation.MouseWheelScrollLines * _totalDelta) / 3600);

            if (fValue > 1.7 || fValue < 0.5)
            {
                //
            }
            else
            {
                zoom(fValue);
            }
        }

        private void zoom(float pcnt)
        {
            if (_fontSize == 0)
            {
                _fontSize = 12;
            }

            c1GridSQLHistory.RowHeight = (int)(_rowHeight * pcnt) + 5;
            c1GridSQLHistory.Splits[0].ColumnCaptionHeight = (int)(_rowHeight * pcnt) + 12;
            c1GridSQLHistory.RecordSelectorWidth = (int)(_recSelWidth * pcnt);
            c1GridSQLHistory.Styles["Normal"].Font = new Font(c1GridSQLHistory.Styles["Normal"].Font.FontFamily, _fontSize * pcnt);

            foreach (C1DisplayColumn col in c1GridSQLHistory.Splits[0].DisplayColumns)
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
        }

        private void Query_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bFormLoadFinish)
            {
                return;
            }

            //切換 Focus，避免滑鼠滾輪滾動，又再次觸發查詢
            chkShowFilterRow.Focus();
            CreateAndGetSqlHistoryInfoTable(); //Query_SelectedIndexChanged
            c1GridSQLHistory.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CreateAndGetSqlHistoryInfoTable(); //btnSearch_Click
        }

        private void SetCheckBox(string sColumn)
        {
            //該欄位以 CheckBox 動態呈現
            var items = c1GridSQLHistory.Columns[sColumn].ValueItems;
            items.Translate = true;
            items.Presentation = PresentationEnum.CheckBox;

            // now associate underlying db values with the checked state
            items.Values.Clear();
            items.Values.Add(new ValueItem("0", false)); // unchecked
            items.Values.Add(new ValueItem("1", true));  // checked

            //指定哪一個 Column 要套用 FetchCellStyle
            c1GridSQLHistory.Splits[0].DisplayColumns[_lstGridHeader[(int)eColumn.ConnectionName]].FetchStyle = true;
            c1GridSQLHistory.Splits[0].DisplayColumns[_lstGridHeader[(int)eColumn.DataSource]].FetchStyle = true;
            c1GridSQLHistory.Splits[0].DisplayColumns[_lstGridHeader[(int)eColumn.ExecutionDate]].FetchStyle = true;
            c1GridSQLHistory.Splits[0].DisplayColumns[_lstGridHeader[(int)eColumn.ExecutionTime]].FetchStyle = true;
            c1GridSQLHistory.Splits[0].DisplayColumns[_lstGridHeader[(int)eColumn.Message]].FetchStyle = true;
            c1GridSQLHistory.Splits[0].DisplayColumns[_lstGridHeader[(int)eColumn.QueryTime]].FetchStyle = true;
            c1GridSQLHistory.Splits[0].DisplayColumns[_lstGridHeader[(int)eColumn.Rows]].FetchStyle = true;
            c1GridSQLHistory.Splits[0].DisplayColumns[_lstGridHeader[(int)eColumn.Result]].FetchStyle = true;
            c1GridSQLHistory.Splits[0].DisplayColumns[_lstGridHeader[(int)eColumn.SQLStatement]].FetchStyle = true;
        }

        private void c1GridSQLHistory_FetchCellStyle(object sender, FetchCellStyleEventArgs e)
        {
            if (e.Col > 0) //除了 CheckBox，其餘皆 Lock
            {
                e.CellStyle.Locked = true;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            var sValue = sender is ToolStripButton btn && btn.Tag.ToString() == "SelectAll" ? "1" : "0";

            var iRow = c1GridSQLHistory.Row;
            var iCol = c1GridSQLHistory.Col;

            try
            {
                for (var j = c1GridSQLHistory.RowCount - 1; j >= 0; j--)
                {
                    c1GridSQLHistory[j, 0] = sValue;
                }

                CheckAndCountSelected(); //btnSelectAll_Click (全選、全不選)

                c1GridSQLHistory.Row = iRow;
                c1GridSQLHistory.Col = iCol;
                c1GridSQLHistory.Select(); //Focus 切換到指定的 Cell
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var sSql = "";
            var sTemp = "";
            string sMsg;

            try
            {
                FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "JasonQuery");

                var sTemp1 = MyGlobal.GetLanguageString("All selected records will be deleted!", "form", Name, "msg", "AllWillBeDeleted", "Text");
                var sTemp2 = MyGlobal.GetLanguageString("Are you sure you want to continue?", "form", Name, "msg", "WantToContinue", "Text");

                if (MessageBox.Show(sTemp1 + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor = Cursors.Default;

                    for (var j = c1GridSQLHistory.RowCount - 1; j >= 0; j--)
                    {
                        Application.DoEvents();

                        if (c1GridSQLHistory[j, (int)eColumn.Select].ToString() == "1")
                        {
                            sTemp += c1GridSQLHistory[j, (int)eColumn.PID] + ",";
                        }
                    }

                    if (sTemp.Substring(sTemp.Length - 1, 1) == ",")
                    {
                        sTemp = sTemp.Substring(0, sTemp.Length - 1);
                    }

                    sSql = "DELETE FROM SQLHistory WHERE PID IN ({0});";
                    sSql = string.Format(sSql, sTemp);

                    //Batch Delete SQL History
                    sMsg = DBCommon.BatchDeleteRecord(sSql);

                    btnSearch.PerformClick();

                    FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "JasonQuery");

                    if (string.IsNullOrEmpty(sMsg))
                    {
                        sTemp1 = MyGlobal.GetLanguageString("All selected records have been deleted!", "form", Name, "msg", "AllHaveBeenDeleted", "Text");
                        MessageBox.Show(sTemp1, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                DBCommon.ExecNonQuery("VACUUM");
            }
            catch (Exception ex)
            {
                FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "JasonQuery");

                sMsg = MyGlobal.GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text");
                MessageBox.Show(sMsg + "\r\n\r\n" + ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSearch.Focus();
            }

            Cursor = Cursors.Default;
        }

        private void CheckAndCountSelected()
        {
            var iRow = c1GridSQLHistory.Row;
            var iCol = c1GridSQLHistory.Col;

            _iSelectedCount = 0;

            for (var j = c1GridSQLHistory.RowCount - 1; j >= 0; j--)
            {
                if (c1GridSQLHistory[j, 0].ToString() == "1")
                {
                    _iSelectedCount++;
                }
            }

            c1GridSQLHistory.Row = iRow;
            c1GridSQLHistory.Col = iCol;
            c1GridSQLHistory.Select(); //Focus 切換到指定的 Cell

            btnDelete.Enabled = _iSelectedCount > 0;
        }

        private void c1GridSQLHistory_AfterColUpdate(object sender, ColEventArgs e)
        {
            //使用者手動勾選
            CheckAndCountSelected(); //c1GridSQLHistory_AfterColUpdate
        }

        private void editorSQL_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            //Select All
            _eMenu.Items[0].Enabled = !string.IsNullOrEmpty(editorSQL.Text);

            //Copy: 判斷是否有選取文字，決定功能表項目可不可用
            _eMenu.Items[2].Enabled = !string.IsNullOrEmpty(editorSQL.SelectedText);

            editorSQL.ContextMenuStrip = _eMenu;

            if (MyLibrary.bDarkMode)
            {
                _eMenu.BackColor = ColorTranslator.FromHtml("#2D2D30");
                _eMenu.ForeColor = Color.White;
                _eMenu.RenderMode = ToolStripRenderMode.System;
                //_eMenu.ShowImageMargin = false;
            }

            _eMenu.Show(editorSQL, new Point(e.X, e.Y));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var bHandled = false;

            switch (keyData)
            {
                case Keys.Control | Keys.C: //Ctrl+C
                    if (editorSQL.Focused)
                    {
                        if (chkCopyAsHTML.Checked)
                        {
                            Clipboard.SetDataObject(" ", false);
                            editorSQL.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                        }
                        else
                        {
                            editorSQL.Copy();
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

                case Keys.Control | Keys.F: //Ctrl+F 尋找
                    myFindReplace.ShowFind();
                    bHandled = true;
                    break;

                case Keys.Control | Keys.H: //Ctrl+H 取代
                    myFindReplace.ShowReplace();
                    bHandled = true;
                    break;

                case Keys.F3:
                    if (editorSQL.Focused || editorMessage.Focused)
                    {
                        //myFindReplace.FindPrevious();
                        myFindReplace.Window.FindNext(true);
                        bHandled = true;
                    }

                    break;

                case Keys.Shift | Keys.F3:
                    if (editorSQL.Focused || editorMessage.Focused)
                    {
                        myFindReplace.Window.FindNext(false);
                        bHandled = true;
                    }

                    break;
            }

            return bHandled;
        }

        private void chkCopyAsHTML_Click(object sender, EventArgs e)
        {
            editorSQL.Focus();
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

        private void btnSelectAll2_Click(object sender, EventArgs e)
        {
            if (editorSQL.Focused)
            {
                editorSQL.SelectionStart = 0;
                editorSQL.SelectionEnd = editorSQL.Text.Length;
            }
            else
            {
                editorMessage.SelectionStart = 0;
                editorMessage.SelectionEnd = editorSQL.Text.Length;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (editorSQL.Focused)
            {
                if (chkCopyAsHTML.Checked)
                {
                    Clipboard.SetDataObject("", false);
                    editorSQL.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                }
                else
                {
                    editorSQL.Copy();
                }
            }
            else
            {
                if (chkCopyAsHTML.Checked)
                {
                    Clipboard.SetDataObject("", false);
                    editorMessage.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                }
                else
                {
                    editorMessage.Copy();
                }
            }
        }

        private void tmrMother2Child_Tick(object sender, EventArgs e)
        {
            //是否為 Reload Localization 套用？
            if (string.IsNullOrEmpty(MyGlobal.sInfoFromReloadLocalization) || !MyGlobal.sInfoFromReloadLocalization.StartsWith("reloadlocalization`"))
            {
                return;
            }

            var sTemp = MyGlobal.sInfoFromReloadLocalization.Replace("reloadlocalization`", "");
            sTemp = sTemp.Split(';')[0];

            if (sTemp != AccessibleDescription)
            {
                return;
            }

            MyGlobal.sInfoFromReloadLocalization = MyGlobal.sInfoFromReloadLocalization.Replace(AccessibleDescription + @";", "");

            if (MyGlobal.sInfoFromReloadLocalization == "reloadlocalization`")
            {
                MyGlobal.sInfoFromReloadLocalization = "";
            }

            ApplyLocalizationSetting(); //timerMother2Child_Tick
            CreateAndGetSqlHistoryInfoTable(); //timerMother2Child_Tick
            _bFormLoadFinish = true;
        }

        private void c1GridSQLHistory_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            _gMenu.Items[0].Enabled = c1GridSQLHistory.RowCount > 0;

            c1GridSQLHistory.ContextMenuStrip = _gMenu;

            if (MyLibrary.bDarkMode)
            {
                _gMenu.BackColor = ColorTranslator.FromHtml("#2D2D30");
                _gMenu.ForeColor = Color.White;
                _gMenu.RenderMode = ToolStripRenderMode.System;
                //_gMenu.ShowImageMargin = false;
            }

            _gMenu.Show(c1GridSQLHistory, new Point(e.X, e.Y));
        }

        private void ExportToFile()
        {
            var sf = new SaveFileDialog {Title = @"Save As", Filter = @"Excel files (*.xlsx)|*.xlsx"};

            var iXTemp = Cursor.Position.X + 20;
            var iYTemp = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height * 0.25);

            var myNewLocation = Location;
            myNewLocation.Offset(iXTemp, iYTemp);

            MoveDialogWhenOpened(sf.Title, myNewLocation);

            if (sf.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (string.IsNullOrEmpty(Path.GetExtension(sf.FileName)))
            {
                sf.FileName += ".xlsx";
            }

            try
            {
                c1GridSQLHistory.SaveExcel(sf.FileName, MyLibrary.sGridSheetName);
                Process.Start(sf.FileName);
            }
            catch (Exception ex)
            {
                var sLangText = MyGlobal.GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text");
                var sTryAgain = MyGlobal.GetLanguageString("Please try again!", "Global", "Global", "msg", "PleaseTryAgain", "Text");

                MessageBox.Show(sLangText + "\r\n\r\n" + ex.Message + "\r\n\r\n" + sTryAgain, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private static void MoveDialogWhenOpened(string windowCaption, Point location)
        {
            var argument = new object[] { windowCaption, location };

            //using System.ComponentModel;
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += MoveDialogThread;
            backgroundWorker.RunWorkerAsync(argument);
        }

        private static void MoveDialogThread(object sender, DoWorkEventArgs e)
        {
            const string dialogWindowClass = "#32770";

            var windowCaption = (string)((object[])e.Argument)[0];
            var location = (Point)((object[])e.Argument)[1];

            // try for a maximum of 4 seconds (sleepTime * maxAttempts)
            const int sleepTime = 10; // milliseconds
            const int maxAttempts = 400;

            for (var i = 0; i < maxAttempts; ++i)
            {
                // find the handle to the dialog
                var handle = Win32Api.FindWindow(dialogWindowClass, windowCaption);

                // if the handle was found and the dialog is visible
                if ((int)handle > 0 && Win32Api.IsWindowVisible(handle) > 0)
                {
                    // move it
                    Win32Api.SetWindowPos(handle, (IntPtr)0, location.X, location.Y, 0, 0, Win32Api.SWP_NOSIZE | Win32Api.SWP_NOZORDER);
                    break;
                }

                // if not found wait a brief sec and try again
                Thread.Sleep(sleepTime);
            }
        }

        private void editorMessage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            //Select All
            _fMenu.Items[0].Enabled = !string.IsNullOrEmpty(editorMessage.Text);

            //Copy: 判斷是否有選取文字，決定功能表項目可不可用
            _fMenu.Items[2].Enabled = !string.IsNullOrEmpty(editorMessage.SelectedText);

            editorMessage.ContextMenuStrip = _fMenu;

            if (MyLibrary.bDarkMode)
            {
                _fMenu.BackColor = ColorTranslator.FromHtml("#2D2D30");
                _fMenu.ForeColor = Color.White;
                _fMenu.RenderMode = ToolStripRenderMode.System;
                //_fMenu.ShowImageMargin = false;
            }

            _fMenu.Show(editorMessage, new Point(e.X, e.Y));
        }

        private void ApplySqlStyler()
        {
            editorMessage.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
            editorMessage.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editorMessage.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));

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

            editorMessage.Styler = new SqlStyler(); //sqlstyler()：變更關鍵字、顏色
        }

        private void btnCompact_Click(object sender, EventArgs e)
        {
            string sMsg;
            Cursor = Cursors.WaitCursor;
            var lOld = new FileInfo(MyGlobal.sMDBFilename).Length;
            long lNew;

            try
            {
                DBCommon.ExecNonQuery("VACUUM");
                lNew = new FileInfo(MyGlobal.sMDBFilename).Length;
            }
            catch (Exception)
            {
                sMsg = "Cannot access the file '{0}' because it is being used by another process.";
                sMsg = string.Format(sMsg, MyGlobal.sMDBFilename);

                FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "Error compacting file...");
                Cursor = Cursors.Default;
                MessageBox.Show(sMsg, @"Error compacting file...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sMsg = "'JasonQuery.db' compacted successfully!\r\n\r\nSize before compacting: {0}\r\nSize after compacting: {1}";
            sMsg = string.Format(sMsg, $"{lOld:#,##0}", $"{lNew:#,##0}");

            FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "Compact OK!");
            MessageBox.Show(sMsg, @"Compact OK!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            btnCompact.Enabled = false;
            Cursor = Cursors.Default;
        }

        private void editorSQL_Enter(object sender, EventArgs e)
        {
            myFindReplace.Scintilla = (JasonLibrary.ScintillaEditor)sender; //20230629
            MyGlobal.sGlobalTemp5 = "CanPasteN"; //20230704
        }

        private void editorMessage_Enter(object sender, EventArgs e)
        {
            myFindReplace.Scintilla = (JasonLibrary.ScintillaEditor)sender; //20230629
            MyGlobal.sGlobalTemp5 = "CanPasteN"; //20230704
        }
    }
}