using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using C1.Win.C1Input;
using C1.Win.C1Themes;
using C1.Win.C1TrueDBGrid;
using JasonLibrary.Class;
using JasonLibrary.Stylers;
using ScintillaNET;
using VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle;

/*
 以下 3個元件，重建方案前要檢查一下 Height 值
 editorSQLPane: 699
 c1GridStructure: 698
 c1Grid100RowsTop: 698
 c1Grid100RowsLast: 698
*/

namespace JasonQuery
{
    public partial class frmSchemaBrowser : Form
    {
        public event ValueUpdatedEventHandler ValueUpdated;
        private ContextMenuStrip eMenu = new ContextMenuStrip(); //Editor's menu
        private ContextMenuStrip gMenu1 = new ContextMenuStrip(); //C1TrueDBGrid1's menu
        private ContextMenuStrip gMenu2 = new ContextMenuStrip(); //C1TrueDBGrid2's menu
        private ContextMenuStrip _cMenuSchemaBrowser = new ContextMenuStrip(); //SchemaBrowser Grid 右鍵選單
        private bool _bFormLoadFinish = false;
        private bool _bColResize; //是否正在執行 c1TrueDBGrid1_ColResize 事件
        private bool _bColAutoResize; //是否正在「自動調整欄寬」？
        private bool _bMouseDoubleClick = true; //是否由 MouseDoubleClick 觸發？  初始值要設為 true，因為 Form_Load 會觸發一次
        private bool _bExpandCollapseAction; //是否正在執行 Expand / Collapse ?
        private bool _bFilterAction; //是否正在執行 Filter ?
        private DataTable dtSchema, dtTableSchema; //dtFilter = null;
        private bool _bSaveSplitter; //判斷是否需要 SaveSplitter 寬／高度值
        private string _sLangText = "";
        private string _sViewName = "";
        private string _sTableName = "";

        private FindReplace.FindReplace myFindReplace; //20230629
        private DataTable dtData = new DataTable();
        private DataTable dtSchemaTable = new DataTable();

        private Devart.Data.Oracle.OracleDataReader drOracle { get; set; }
        private Devart.Data.PostgreSql.PgSqlDataReader drPostgreSQL { get; set; }
        private Devart.Data.SqlServer.SqlDataReader drSQLServer { get; set; }
        private Devart.Data.MySql.MySqlDataReader drMySQL { get; set; }

        public frmSchemaBrowser()
        {
            InitializeComponent();
        }

        private void ApplyLocalizationSetting()
        {
            //以下顏色要重新指定，否則 CheckBox 底色會不一樣
            toolStrip1.BackColor = SystemColors.Control;
            chkCopyAsHTML.BackColor = toolStrip1.BackColor;

            toolStrip2.BackColor = SystemColors.Control;
            lblFilter.BackColor = toolStrip2.BackColor;

            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            MyGlobal.ApplyLanguageInfo(this); //ApplyLocalizationSetting

            lblFilter0.Text = lblFilter.Text;
            txtFilter.Location = new Point(lblFilter0.Left + lblFilter0.Width + 1, txtFilter.Top);

            _sLangText = MyGlobal.GetLanguageString("Table Name:", "form", Name, "object", "lblTableName", "Text");
            _sTableName = _sLangText;
            _sLangText = MyGlobal.GetLanguageString("View Name:", "form", Name, "object", "lblViewName", "Text");
            _sViewName = _sLangText;

            if (!MyLibrary.bDarkMode)
            {
                lblFilter.ForeColor = ColorTranslator.FromHtml("#000000");
            }
            else
            {
                C1ThemeController.ApplicationTheme = "VS2013Dark";
                c1ThemeController1.SetTheme(c1GridSchemaBrowser, "VS2013Dark");
                c1GridSchemaBrowser.BackColor = ColorTranslator.FromHtml("#2D2D30"); //Office 2010 Black
                MyGlobal.SetGridVisualStyle(c1GridSchemaBrowser, 9);

                c1ThemeController1.SetTheme(c1GridStructure, "VS2013Dark");
                c1GridStructure.BackColor = ColorTranslator.FromHtml("#2D2D30"); //Office 2010 Black
                MyGlobal.SetGridVisualStyle(c1GridStructure, 9);

                c1ThemeController1.SetTheme(c1Grid100RowsTop, "VS2013Dark");
                c1Grid100RowsTop.BackColor = ColorTranslator.FromHtml("#2D2D30"); //Office 2010 Black
                MyGlobal.SetGridVisualStyle(c1Grid100RowsTop, 9);

                lblFilter.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
                lblFilter.BackColor = ColorTranslator.FromHtml("#3F3F3f");

                //以下顏色要重新指定
                toolStrip1.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);
                chkCopyAsHTML.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);
                toolStrip2.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);

                txtFilter.BackColor = ColorTranslator.FromHtml("#2D2D30");

                lblTableName01.ForeColor = Color.Yellow;
                lblTableName02.ForeColor = Color.Yellow;

                txtFilter.Location = new Point(131, 1);
                lblFilter.Location = new Point(90, 4);
            }

            eMenu = new ContextMenuStrip();

            _sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "menueditor", "SelectAll", "Text");
            eMenu.Items.Add(_sLangText);
            ((ToolStripMenuItem) eMenu.Items[0]).ShortcutKeys = Keys.Control | Keys.A;

            eMenu.Items[0].Click += delegate
            {
                editorSQLPane.SelectionStart = 0;
                editorSQLPane.SelectionEnd = editorSQLPane.Text.Length;
            };

            eMenu.Items.Add("-");

            _sLangText = MyGlobal.GetLanguageString("Copy", "form", Name, "menueditor", "Copy", "Text");
            eMenu.Items.Add(_sLangText);
            ((ToolStripMenuItem) eMenu.Items[2]).ShortcutKeys = Keys.Control | Keys.C;

            eMenu.Items[2].Click += delegate
            {
                if (chkCopyAsHTML.Checked)
                {
                    Clipboard.SetDataObject("", false);
                    editorSQLPane.Copy(CopyFormat.Text | CopyFormat.Rtf | CopyFormat.Html);
                }
                else
                {
                    editorSQLPane.Copy();
                }
            };

            var toolTip1 = new ToolTip();
            _sLangText = MyGlobal.GetLanguageString(@"Expand / Collapse", "form", Name, "object", "btnExpandCollapse", "ToolTipText");
            toolTip1.SetToolTip(btnExpandCollapse, _sLangText);

            chkCopyAsHTML.Checked = MyLibrary.bCopyAsHTML;

            Refresh();

            editorSQLPane.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editorSQLPane.WrapVisualFlags = (MyLibrary.bWordWrapVisualFlags_Start ? WrapVisualFlags.Start : WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_End ? WrapVisualFlags.End : WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_Margin ? WrapVisualFlags.Margin : WrapVisualFlags.None);

            if (MyLibrary.bWordWrap)
            {
                btnWordWrap.Visible = false;
                btnWordWrap2.Visible = true;
                editorSQLPane.WrapMode = WrapMode.Word;
            }
            else
            {
                btnWordWrap.Visible = true;
                btnWordWrap2.Visible = false;
                editorSQLPane.WrapMode = WrapMode.None;
            }

            editorSQLPane.ViewWhitespace = WhitespaceMode.Invisible;

            ApplySqlStyler(); //ApplyLocalizationSetting
            MyGlobal.SetGridVisualStyle(c1GridStructure, 10);
            MyGlobal.SetGridVisualStyle(c1Grid100RowsTop, 10);

            GridVisualStyle(); //ApplyLocalizationSetting
            GridFontAndBackgroundColor(); //ApplyLocalizationSetting
            GridZoom(); //ApplyLocalizationSetting

            Cursor = Cursors.Default;
        }

        private void c1SplitButton_DropDownItemClicked(object sender, DropDownItemClickedEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            _bExpandCollapseAction = true;

            switch ((e.ClickedItem.Tag ?? Tag.ToString()).ToString())
            {
                case "mnuExpandAll":
                    {
                        for (var i = 0; i < c1GridSchemaBrowser.Splits[0].Rows.Count; i++)
                        {
                            if (c1GridSchemaBrowser.Splits[0].Rows[i].RowType != RowTypeEnum.DataRow)
                                c1GridSchemaBrowser.ExpandGroupRow(i);

                            Application.DoEvents();
                        }

                        AutoResizeGridColumnWidth(); //mnuExpandAll_Click
                        break;
                    }
                case "mnuCollapseAll":
                    {
                        for (var i = 0; i < c1GridSchemaBrowser.Splits[0].Rows.Count; i++)
                        {
                            if (c1GridSchemaBrowser.Splits[0].Rows[i].RowType != RowTypeEnum.DataRow)
                                c1GridSchemaBrowser.CollapseGroupRow(i);

                            Application.DoEvents();
                        }

                        AutoResizeGridColumnWidth(); //mnuCollapseAll_Click

                        //展開指定的節點 (-1:標題列，所以，從 0 開始算，要展開哪一個)
                        c1GridSchemaBrowser.ExpandGroupRow(0); //展開 0 的結點

                        break;
                    }
            }

            _bExpandCollapseAction = false;
            Cursor = Cursors.Default;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            tabTableStructure.Tag = "";
            tabView100RowsTop.Tag = "";
            tabView100RowsLast.Tag = "";

            myFindReplace = new FindReplace.FindReplace();
            myFindReplace.Scintilla = editorSQLPane;

            MyGlobal.SetDockingTabColor(c1DockingTab1, ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveBackColor), ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveForeColor), ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabInactiveForeColor));

            if (MyLibrary.bDarkMode)
            {
                c1ThemeController1.SetTheme(c1DockingTab1, "VS2013Dark");
            }

            lblFilter.Font = new Font("Microsoft JhengHei", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);

            ApplyLocalizationSetting(); //Form_Load

            btnExpandCollapse.DropDownItemClicked += c1SplitButton_DropDownItemClicked;

            //載入左右分割的百分比
            LoadSplitterData("L/R"); //Form1_Load

            btnRefresh.PerformClick();

            _bFormLoadFinish = true;
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

            editorSQLPane.Styler = new SqlStyler(); //sqlstyler()：變更關鍵字、顏色
        }

        private void CreateTableSchemaTable() //點選到 Tables 時，顯示在右側的 Grid
        {
            dtTableSchema = new DataTable();
            dtTableSchema.Columns.Add("ColumnName");
            dtTableSchema.Columns.Add("ID");
            dtTableSchema.Columns.Add("DataType"); //包含長度，ex.Varchar2(50), Numeric(5, 3)
            dtTableSchema.Columns.Add("ConstraintInfo");
            dtTableSchema.Columns.Add("Nullable");

            if (MyGlobal.sDataSource == "MySQL")
            {
                dtTableSchema.Columns.Add("AutoInc");
            }

            dtTableSchema.Columns.Add("Default");
            dtTableSchema.Columns.Add("Comments");
        }

        private static void ConnectToDatabase()
        {
            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    MyGlobal.oOracleReader.ConnectTo();
                    break;
                case "PostgreSQL":
                    MyGlobal.oPostgreReader.ConnectTo(MyGlobal.sDBConnectionString);
                    break;
                case "SQL Server":
                    MyGlobal.oSQLServerReader.ConnectTo(MyGlobal.sDBConnectionString);
                    break;
                case "MySQL":
                    MyGlobal.oMySQLReader.ConnectTo(MyGlobal.sDBConnectionString);
                    break;
            }
        }

        private void editorSQLPane_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            //Select All
            eMenu.Items[0].Enabled = !string.IsNullOrEmpty(editorSQLPane.Text);

            //Copy
            eMenu.Items[2].Enabled = !string.IsNullOrEmpty(editorSQLPane.SelectedText); //判斷是否有選取文字，決定功能表項目可不可用

            editorSQLPane.ContextMenuStrip = eMenu;

            if (MyLibrary.bDarkMode)
            {
                eMenu.BackColor = ColorTranslator.FromHtml("#2D2D30");
                eMenu.ForeColor = Color.White;
                eMenu.RenderMode = ToolStripRenderMode.System;
                //eMenu.ShowImageMargin = false;
            }

            eMenu.Show(editorSQLPane, new Point(e.X, e.Y));
        }

        private void editorSQLPane_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; //其他按鍵，忽略！
        }

        private void editorSQLPane_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                //停用 Enter, Ctrl+V, Ctrl+X, Delete, Back
                case Keys.Enter:
                case Keys.Control | Keys.V:
                case Keys.Control | Keys.X:
                case Keys.Delete:
                case Keys.Back:
                    e.Handled = true;
                    return;
                case Keys.Control | Keys.A:
                    editorSQLPane.SelectionStart = 0;
                    editorSQLPane.SelectionEnd = editorSQLPane.Text.Length;
                    e.Handled = false;
                    return;
            }

            if (e.KeyData == (Keys.Control | Keys.Home) || e.KeyData == (Keys.Control | Keys.End) || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown)
            {
                //以下，當放大縮小後，即時調整 line number 的寬度，避免因為放大時，line number 最左側的數字會看不見
                var iStart = editorSQLPane.SelectionStart;
                editorSQLPane.ReadOnly = false;
                editorSQLPane.Text += "\r\n";
                editorSQLPane.Text = editorSQLPane.Text.Substring(0, editorSQLPane.Text.Length - 2);
                editorSQLPane.SelectionStart = iStart;
                editorSQLPane.ReadOnly = true;
                editorSQLPane.ScrollCaret();
                return;
            }

            switch (e.KeyCode)
            {
                //原本的上下左右鍵，可以發揮作用；加上 Shift Key，一樣可以發揮作用！
                case Keys.Left:
                case Keys.Shift | Keys.Left:
                case Keys.Right:
                case Keys.Shift | Keys.Right:
                case Keys.Up:
                case Keys.Shift | Keys.Up:
                case Keys.Down:
                case Keys.Shift | Keys.Down:
                case Keys.Home:
                case Keys.End:
                    e.Handled = false;
                    break;
                //Ctrl + C, 在 ProcessCMDKey() 控制即可
                default:
                    e.Handled = true; //其他按鍵，忽略！
                    break;
            }
        }

        private void editorSQLPane_Enter(object sender, EventArgs e)
        {
            myFindReplace.Scintilla = (JasonLibrary.ScintillaEditor)sender; //20230629
            MyGlobal.sGlobalTemp5 = "CanPasteN"; //20230704
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var bHandled = false;
            var bActiveCell = true; //是否為「只點到某一個 cell，並沒有『選取』cell」?
            var sTemp = "";

            switch (keyData)
            {
                case Keys.Control | Keys.C: //Ctrl+C
                    //判斷要針對哪一個元件進行「複製」
                    if (c1GridStructure.Focused || c1Grid100RowsTop.Focused)
                    {
                        CopyDataFromDataGrid();
                    }
                    else if (c1GridSchemaBrowser.Focused)
                    {
                        C1TrueDBGrid c1Grid = c1GridSchemaBrowser;

                        var row1 = c1Grid.Row;
                        var i = 0;
                        var selCol = c1Grid.SelectedCols.Count;
                        var selRow = c1Grid.SelectedRows.Count;
                        var vr = c1Grid.Splits[0].Rows[row1];

                        //判斷是不是節點
                        if ((selRow == 0) && ((c1Grid.Splits[0].Rows[row1].RowType == RowTypeEnum.CollapsedGroupRow) || (c1Grid.Splits[0].Rows[row1].RowType == RowTypeEnum.ExpandedGroupRow)))
                        {
                            bActiveCell = false;
                            sTemp = ((GroupRow)vr).GroupedText;

                            if (sTemp.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
                            {
                                sTemp = sTemp.Substring(0, sTemp.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
                            }
                        }
                        else
                        {
                            foreach (int row in c1Grid.SelectedRows)
                            {
                                if (selCol == 0) //整列選取
                                {
                                    bActiveCell = false;

                                    foreach (C1DataColumn col1 in c1Grid.Columns)
                                    {
                                        vr = c1Grid.Splits[0].Rows[row];

                                        switch (i)
                                        {
                                            case 0 when col1.Caption == "SchemaType":
                                                break;
                                            case 0 when col1.Caption == "SchemaName":
                                                break;
                                            default:
                                                {
                                                    if (col1.Caption != "SchemaObject" && col1.Caption != "SchemaNode" && col1.Caption != "SchemaType" && col1.Caption != "SchemaName")
                                                    {
                                                        sTemp += col1.CellText(vr.DataRowIndex) + ", ";
                                                    }

                                                    break;
                                                }
                                        }
                                    }

                                    i++;

                                    //▲整列選取, 參考 lblInfo.Text 的顯示方式
                                    //string[] valuesInRow = c1Grid.SelectedRows[ //wsInClipboard[iRow].Split(columnSplitter);

                                    //for (int iCol = 0; iCol < c1Grid.Columns.Count; iCol++)
                                    //{
                                    //    sTemp += c1Grid[c1Grid.Row, iCol] + ", ";
                                    //}
                                }
                                else
                                {
                                    vr = c1Grid.Splits[0].Rows[row];

                                    //使用者可能會同時選取「節點」、「非節點」
                                    if (c1Grid.Splits[0].Rows[row].RowType == RowTypeEnum.CollapsedGroupRow || c1Grid.Splits[0].Rows[row].RowType == RowTypeEnum.ExpandedGroupRow)
                                    {
                                        sTemp = ((GroupRow)vr).GroupedText + ", ";
                                    }
                                    else
                                    {
                                        foreach (C1DataColumn col in c1Grid.SelectedCols)
                                        {
                                            bActiveCell = false;

                                            if (string.IsNullOrEmpty(col.ToString().Trim()))
                                            {
                                                sTemp += c1Grid[vr.DataRowIndex, col.DataField] + ", ";
                                            }
                                            else
                                            {
                                                sTemp += c1Grid[vr.DataRowIndex, col.ToString()] + ", ";
                                            }
                                        }
                                    }
                                }

                                if (!string.IsNullOrEmpty(sTemp))
                                {
                                    sTemp = sTemp.Substring(0, sTemp.Length - 2) + "\r\n";
                                }
                            }
                        }

                        if (bActiveCell)
                        {
                            sTemp = c1Grid[c1Grid.Splits[0].Rows[c1Grid.Row].DataRowIndex, c1Grid.Col].ToString();

                            if (c1GridSchemaBrowser.Focused)
                            {
                                sTemp = lblSchemaName2.Text;
                            }

                            if (c1Grid == c1GridSchemaBrowser && sTemp.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
                            {
                                sTemp = sTemp.Substring(0, sTemp.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
                            }
                        }

                        Clipboard.SetDataObject(sTemp, false);
                    }
                    else if (editorSQLPane.Focused)
                    {
                        if (chkCopyAsHTML.Checked)
                        {
                            editorSQLPane.Copy(CopyFormat.Text | CopyFormat.Rtf | CopyFormat.Html);
                        }
                        else
                        {
                            editorSQLPane.Copy();
                        }
                    }

                    bHandled = true;
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
                    if (editorSQLPane.Focused)
                    {
                        //myFindReplace.FindPrevious();
                        myFindReplace.Window.FindNext(true);
                        bHandled = true;
                    }

                    break;

                case Keys.Shift | Keys.F3:
                    if (editorSQLPane.Focused)
                    {
                        myFindReplace.Window.FindNext(false);
                        bHandled = true;
                    }

                    break;
            }

            return bHandled;
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (splitContainer1.Panel1.Width > 700)
            {
                splitContainer1.SplitterDistance = 700;
            }

            //控制「工具列」是兩行的狀態
            var i = Math.Round((double)(splitContainer1.SplitterDistance - 330) / 8, 0);

            AutoResizeGridColumnWidth();

            if (MyGlobal.sGlobalTemp == "NoSplit") //避免觸發 splitContainer1_SplitterMoved()
            {
                MyGlobal.sGlobalTemp = "";
                return;
            }

            if (!_bSaveSplitter)
            {
                return;
            }

            SaveSplitterData("L/R", splitContainer1.SplitterDistance);
            editorSQLPane.Focus();
            _bSaveSplitter = false;
        }

        private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            _bSaveSplitter = true;
        }

        private void LoadSplitterData(string sSplitterKey)
        {
            try
            {
                var sTemp = "SELECT AttributeValue FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' And MPID = " + MyGlobal.sDBMotherPID + " And AttributeKey = 'SplitterConfig' And AttributeName = 'SchemaBrowser_" + sSplitterKey + "'";
                var dtSplitterData = DBCommon.ExecQuery(sTemp);

                if (dtSplitterData.Rows.Count <= 0)
                {
                    return;
                }

                sTemp = "AttributeValue";
                splitContainer1.SplitterDistance = Convert.ToInt32(dtSplitterData.Rows[0][sTemp].ToString());
            }
            catch (Exception)
            {
                //throw
            }
        }

        private static void SaveSplitterData(string sSplitterKey, int iSplitterValue) //sSplitterKey="L/R"
        {
            if (sSplitterKey != "L/R")
            {
                return;
            }

            //Save
            var sTemp = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SplitterConfig' AND AttributeName = 'SchemaBrowser_" + sSplitterKey + "'";
            var dtSplitterData = DBCommon.ExecQuery(sTemp);

            if (dtSplitterData.Rows.Count > 0)
            {
                sTemp = "UPDATE SystemConfig SET AttributeValue = '" + iSplitterValue + "' WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SplitterConfig' AND AttributeName = 'SchemaBrowser_" + sSplitterKey + "'";
                DBCommon.ExecNonQuery(sTemp);
            }
            else
            {
                sTemp = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) Values ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'SplitterConfig', 'SchemaBrowser_" + sSplitterKey + "', '" + iSplitterValue + "')";
                DBCommon.ExecNonQuery(sTemp);
            }
        }

        private void AutoResizeGridColumnWidth()
        {
            _bColAutoResize = true;

            if ((DataTable)c1GridSchemaBrowser.DataSource == null)
            {
                return;
            }

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
                    iWidth = 70 + (MyGlobal.bShowColumnInfo ? 10 : 0);
                    break;
                case "PostgreSQL":
                    iWidth = 100 + (MyGlobal.bShowColumnInfo ? 0 : -20);
                    break;
                case "SQL Server":
                    iWidth = 75 + (MyGlobal.bShowColumnInfo ? 20 : -10);
                    break;
                case "MySQL":
                    iWidth = 75;
                    break;
            }

            c1GridSchemaBrowser.Splits[0].DisplayColumns[i].Width = splitContainer1.Panel1.Width - iWidth;

            if (c1GridSchemaBrowser.Splits[0].DisplayColumns[i].Width < 60)
            {
                c1GridSchemaBrowser.Splits[0].DisplayColumns[i].Width = 60;
            }

            c1GridSchemaBrowser.Refresh();

            _bColAutoResize = false;
        }

        private void QuerySchema()
        {
            var dtSchema = (DataTable)c1GridSchemaBrowser.DataSource;
            txtFilter.Text = txtFilter.Text.Replace("%", "*");
            var sFilter = "";

            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                txtFilter.Text = @"*";
            }

            if (!txtFilter.Text.EndsWith("*"))
            {
                txtFilter.Text += @"*";
            }

            try
            {
                if (MyGlobal.bShowColumnInfo)
                {
                    dtSchema.DefaultView.RowFilter = sFilter + "(SchemaName LIKE '" + txtFilter.Text.Trim().Replace("*", "%") + "%' OR Schema_Browser LIKE '" + txtFilter.Text.Trim().Replace("*", "%") + "%')";
                }
                else
                {
                    dtSchema.DefaultView.RowFilter = sFilter + "Schema_Browser LIKE '" + txtFilter.Text.Trim().Replace("*", "%") + "'";
                }
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

        private void DisplayInfo_Oracle(int iDisplayRowIndex)
        {
            string sSchemaType;
            var sSchemaType2 = "";
            string sSchemaName;
            int iLevel;
            var sTemp = "";
            var dtTemp = new DataTable();
            var bNode = false; //是否為節點

            Application.DoEvents();

            tabTableStructure.TabVisible = false;
            tabView100RowsTop.TabVisible = false;
            tabView100RowsLast.TabVisible = false;

            var vr = c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex];

            if (c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex].RowType == RowTypeEnum.CollapsedGroupRow || c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex].RowType == RowTypeEnum.ExpandedGroupRow)
            {
                bNode = true;

                //以下可以取得游標所在列的節點的「正確的值」
                iLevel = ((GroupRow)vr).Level;

                if (iLevel <= 1)
                {
                    return;
                }

                //此寫法可能有問題：原廠的 BUG，有時會取到錯誤的值
                sSchemaType = c1GridSchemaBrowser.Columns["SchemaType"].CellValue(((GroupRow)vr).StartIndex).ToString();

                //原廠給的暫時性解法
                for (var i = iDisplayRowIndex - 1; i >= 0; i--)
                {
                    //if above row is Group Row
                    if (c1GridSchemaBrowser.Splits[0].Rows[i] is GroupRow groupRow)
                    {
                        //if level is 2, meaning that this Group Row is for SchemaType
                        if (groupRow.Level == 1)
                        {
                            //copy its GroupedText and the break the loop
                            sSchemaType2 = groupRow.GroupedText;
                            break;
                        }
                    }
                }

                var dtSchema0 = (DataTable)c1GridSchemaBrowser.DataSource;

                //20220516 判斷 sSchemaType 哪一個是正確的
                if (sSchemaType != sSchemaType2 && !string.IsNullOrEmpty(sSchemaType2))
                {
                    var sTempSchemaObject1 = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();
                    var sTempSchemaObject2 = dtSchema0.Select($"SchemaType = '{sSchemaType2}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();

                    if (string.IsNullOrEmpty(sTempSchemaObject1) && !string.IsNullOrEmpty(sTempSchemaObject2))
                    {
                        sSchemaType = sSchemaType2;
                    }
                }

                sSchemaName = ((GroupRow)vr).GroupedText;
            }
            else
            {
                iLevel = 3;

                //以下可以取得游標所在列的「正確的值」，用原廠給的建議寫法
                sSchemaType = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["SchemaType"].ToString();
                sSchemaName = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["Schema_Browser"].ToString();
            }

            if (sSchemaType.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
            {
                sSchemaType = sSchemaType.Substring(0, sSchemaType.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
            }

            if (sSchemaName.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
            {
                sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
            }

            if (sSchemaName.IndexOf(", ", StringComparison.Ordinal) != -1) //欄位名稱
            {
                sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf(", ", StringComparison.Ordinal));
            }

            if (string.IsNullOrEmpty(sSchemaName))
            {
                //如果使用者有開啟「顯示欄位名稱」，除了「Table」外，其餘的項目，sSchemaName 都會是空值
                return;
            }

            //切換：當滑鼠點到 Grid 最右邊的空白處時，一樣會有點選的效果，但 Focus 列並不會同時切換，所以此處要強制切換
            c1GridSchemaBrowser.Row = iDisplayRowIndex;

            if (!bNode && MyGlobal.bShowColumnInfo)
            {
                return; //有顯示 Column Info，進入的不是空值就是欄位名稱，不用處理
            }

            editorSQLPane.ReadOnly = false;
            editorSQLPane.Text = "";
            editorSQLPane.ReadOnly = true;

            string sSql;

            if (sSchemaType == "Tables")
            {
                tabTableStructure.Tag = sSchemaName;
                tabTableStructure.TabVisible = true;
                tabView100RowsTop.Tag = sSchemaName;
                tabView100RowsTop.TabVisible = true;
                //tabView100RowsLast.Tag = sSchemaName;
                //tabView100RowsLast.TabVisible = true;
                lblTableName1.Text = _sTableName;
                lblTableName2.Text = _sTableName;
                lblTableName3.Text = _sTableName;
                lblTableName01.Text = sSchemaName;
                lblTableName02.Text = sSchemaName;
                lblTableName03.Text = sSchemaName;

                CreateTableSchemaTable(); //DisplayInfo_Oracle

                #region 取得指定的 Table's 欄位資訊
                //Constraint資訊
                sSql = "SELECT cols.Table_Name AS TableName, cols.Column_Name AS ColumnName, cons.Constraint_Type AS ConstraintType, cons.Constraint_Name AS ConstraintName FROM all_constraints cons, all_cons_columns cols WHERE cons.Constraint_Name = cols.Constraint_Name AND UPPER(cols.Owner) = '{0}' ORDER BY cons.Constraint_Type DESC";
                sSql = string.Format(sSql, MyGlobal.sDBUser.ToUpper());
                var dtConstraint = new DataTable();
                ExecuteQueryToDataTable(sSql, ref dtConstraint);

                sSql = "";
                sSql += "SELECT 'TABLE' AS Schema_Type, ss.Table_Name AS TableName, ss.Column_Name AS ColumnName, ss.Column_ID AS ColumnID, ss.Data_Type AS DataType, ss.Data_Length AS DataLength, ss.Nullable, ss.Data_Default AS DefaultValue, ss.Data_Precision || ',' || ss.Data_Scale as Scale, cc.Comments\r\n";
                sSql += " FROM user_tab_columns ss LEFT JOIN user_col_comments cc on (ss.Column_Name = cc.Column_Name AND ss.Table_Name = cc.Table_Name)\r\n";
                sSql += " WHERE ss.Table_Name IN (SELECT Object_Name FROM all_objects WHERE Object_Type IN ('TABLE') AND UPPER(Owner) = '{0}') AND ss.Table_Name = '{1}' ORDER BY TO_NUMBER(ss.Column_ID)";
                sSql = string.Format(sSql, MyGlobal.sDBUser.ToUpper(), sSchemaName);
                ExecuteQueryToDataTable(sSql, ref dtTemp);

                if (!(dtTemp != null && dtTemp.Rows.Count > 0))
                {
                    c1GridStructure.DataSource = null;
                    c1Grid100RowsTop.DataSource = null;
                    c1Grid100RowsLast.DataSource = null;
                }
                else
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        var row = dtTableSchema.NewRow();
                        row["ColumnName"] = dtTemp.Rows[iRow]["ColumnName"].ToString();
                        row["ID"] = dtTemp.Rows[iRow]["ColumnID"].ToString();

                        switch (dtTemp.Rows[iRow]["DataType"].ToString().ToUpper())
                        {
                            case "DATE":
                                row["DataType"] = "DATE";
                                break;
                            case "VARCHAR":
                            case "VARCHAR2":
                                row["DataType"] = dtTemp.Rows[iRow]["DataType"].ToString().ToUpper() + " (" + dtTemp.Rows[iRow]["DataLength"] + ")";
                                break;
                            case "TIMESTAMP(6)":
                                row["DataType"] = "TIMESTAMP (6)";
                                break;
                            case "TIMESTAMP(0) WITH TIME ZONE":
                                row["DataType"] = "TIMESTAMP (0) WITH TIME ZONE";
                                break;
                            case "TIMESTAMP(6) WITH TIME ZONE":
                                row["DataType"] = "TIMESTAMP (6) WITH TIME ZONE";
                                break;
                            case "NUMBER":
                                if (dtTemp.Rows[iRow]["Scale"].ToString() == ",")
                                {
                                    row["DataType"] = dtTemp.Rows[iRow]["DataType"].ToString().ToUpper();
                                }
                                else
                                {
                                    row["DataType"] = dtTemp.Rows[iRow]["DataType"].ToString().ToUpper() + " (" + dtTemp.Rows[iRow]["Scale"] + ")";
                                }
                                break;
                            default:
                                row["DataType"] = dtTemp.Rows[iRow]["DataType"].ToString().ToUpper();
                                break;
                        }

                        var sConstraintInfo = "";

                        if (dtConstraint != null)
                        {
                            var dtRow = dtConstraint.Select("TableName='" + dtTemp.Rows[iRow]["TableName"].ToString().Replace("'", "''") + "' and ColumnName='" + dtTemp.Rows[iRow]["ColumnName"].ToString().Replace("'", "''") + "'");

                            if (dtRow.Length > 0)
                            {
                                sConstraintInfo = dtRow[0]["ConstraintType"] + ", " + dtRow[0]["ConstraintName"];
                            }
                        }

                        row["ConstraintInfo"] = sConstraintInfo;
                        row["Default"] = dtTemp.Rows[iRow]["DefaultValue"].ToString();
                        row["Nullable"] = dtTemp.Rows[iRow]["Nullable"].ToString();
                        row["Comments"] = dtTemp.Rows[iRow]["Comments"].ToString();
                        dtTableSchema.Rows.Add(row);
                    }

                    c1GridStructure.DataSource = dtTableSchema;

                    foreach (C1DisplayColumn col in c1GridStructure.Splits[0].DisplayColumns)
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

                    //取得指定 Table 的前 100 筆資料
                    sSql = "SELECT * FROM {0}";
                    sSql = string.Format(sSql, sSchemaName);
                    ExecuteQuery100Rows(sSql, 0, 100);
                }
                #endregion
            }

            DateTime dtDateTime;

            //取得 Table/Function/Trigger/View 的「名稱、狀態、建立日期」
            switch (sSchemaType)
            {
                case "Tables":
                    {
                        tabTableStructure.Tag = sSchemaName;
                        tabTableStructure.TabVisible = true;
                        tabView100RowsTop.Tag = sSchemaName;
                        tabView100RowsTop.TabVisible = true;
                        //tabView100RowsLast.Tag = sSchemaName;
                        //tabView100RowsLast.TabVisible = true;
                        lblTableName1.Text = _sTableName;
                        lblTableName2.Text = _sTableName;
                        lblTableName3.Text = _sTableName;
                        lblTableName01.Text = sSchemaName;
                        lblTableName02.Text = sSchemaName;
                        lblTableName03.Text = sSchemaName;

                        sSql = "SELECT Object_Name AS Function_Name, Status, Created FROM all_objects WHERE Object_Type = 'TABLE' AND UPPER(Owner) = '{0}' AND Object_Name = '{1}'";
                        sSql = string.Format(sSql, MyGlobal.sDBUser.ToUpper(), sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Created"].ToString());
                            sTemp = "--Table: \"" + MyGlobal.sDBUser.ToUpper() + "\".\"" + sSchemaName + "\"\r\n--Status: " + dtTemp.Rows[0]["Status"] + "\r\n--Created: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "\r\n\r\n";
                        }

                        break;
                    }
                case "Functions":
                    {
                        sSql = "SELECT Object_Name AS Function_Name, Status, Created FROM all_objects WHERE Object_Type = 'FUNCTION' AND UPPER(Owner) = '{0}' AND Object_Name = '{1}'";
                        sSql = string.Format(sSql, MyGlobal.sDBUser.ToUpper(), sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Created"].ToString());
                            sTemp = "--Function: \"" + MyGlobal.sDBUser.ToUpper() + "\".\"" + sSchemaName + "\"\r\n--Status: " + dtTemp.Rows[0]["Status"] + "\r\n--Created: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "\r\n\r\n";
                        }

                        break;
                    }
                case "Triggers":
                    {
                        sSql = "SELECT Object_Name AS Trigger_Name, Status, Created FROM all_objects WHERE Object_Type = 'TRIGGER' AND UPPER(Owner) = '{0}' AND Object_Name = '{1}'";
                        sSql = string.Format(sSql, MyGlobal.sDBUser.ToUpper(), sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Created"].ToString());
                            sTemp = "--Trigger: \"" + MyGlobal.sDBUser.ToUpper() + "\".\"" + sSchemaName + "\"\r\n--Status: " + dtTemp.Rows[0]["Status"] + "\r\n--Created: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "\r\n\r\n";
                        }

                        break;
                    }
                case "Views":
                    {
                        lblTableName1.Text = _sViewName;
                        lblTableName2.Text = _sViewName;
                        lblTableName3.Text = _sViewName;
                        lblTableName01.Text = sSchemaName;
                        lblTableName02.Text = sSchemaName;
                        lblTableName03.Text = sSchemaName;

                        sSql = "SELECT os.Object_Name AS View_Name, vs.Text_Length, os.Status, os.Created FROM all_objects os, all_views vs WHERE os.Object_Type = 'VIEW' AND os.Object_Name = vs.View_Name AND UPPER(os.Owner) = '{0}' AND UPPER(vs.Owner) = '{0}' AND os.Object_Name = '{1}'";
                        sSql = string.Format(sSql, MyGlobal.sDBUser.ToUpper(), sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (!(dtTemp != null && dtTemp.Rows.Count > 0))
                        {
                            c1GridStructure.DataSource = null;
                            c1Grid100RowsTop.DataSource = null;
                            c1Grid100RowsLast.DataSource = null;
                        }
                        else
                        {
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Created"].ToString());
                            sTemp = "--View: \"" + MyGlobal.sDBUser.ToUpper() + "\".\"" + sSchemaName + "\"\r\n--Status: " + dtTemp.Rows[0]["Status"] + "\r\n--Created: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "\r\n\r\n";

                            //取得指定 View 的前 100 筆資料
                            sSql = "SELECT * FROM {0}";
                            sSql = string.Format(sSql, sSchemaName);
                            ExecuteQuery100Rows(sSql, 0, 100);

                            tabView100RowsTop.Tag = sSchemaName;
                            tabView100RowsTop.TabVisible = true;
                        }

                        break;
                    }
            }

            //以下可以取得指定 Function/Table/Trigger/View 的 Created SQL
            sSql = "SELECT dbms_metadata.GET_DDL('{2}', '{1}', '{0}') AS ScriptText FROM DUAL";
            sSql = string.Format(sSql, MyGlobal.sDBUser.ToUpper(), sSchemaName, sSchemaType.Substring(0, sSchemaType.Length - 1).ToUpper());
            ExecuteQueryToDataTable(sSql, ref dtTemp);

            if (dtTemp == null || dtTemp.Rows.Count == 0)
            {
                return;
            }

            editorSQLPane.ReadOnly = false;
            editorSQLPane.Text = dtTemp.Rows[0]["ScriptText"].ToString();

            if (editorSQLPane.Text.Substring(0, 1) == "\n")
            {
                editorSQLPane.Text = editorSQLPane.Text.Substring(1, editorSQLPane.Text.Length - 1);
            }

            editorSQLPane.Text = sTemp + editorSQLPane.Text;
            editorSQLPane.ReadOnly = true;
            editorSQLPane.Focus();
        }

        private void DisplayInfo_PostgreSQL(int iDisplayRowIndex)
        {
            string sSchemaNode;
            string sSchemaType;
            var sSchemaType2 = "";
            string sSchemaName;
            var sConstraintName = "";
            int iLevel;
            var dtTemp = new DataTable();
            var bNode = false; //是否為節點

            Application.DoEvents();

            tabTableStructure.TabVisible = false;
            tabView100RowsTop.TabVisible = false;
            tabView100RowsLast.TabVisible = false;

            var vr = c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex];

            if (c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex].RowType == RowTypeEnum.CollapsedGroupRow || c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex].RowType == RowTypeEnum.ExpandedGroupRow)
            {
                bNode = true;

                //以下可以取得游標所在列的節點的「正確的值」
                iLevel = ((GroupRow)vr).Level;

                if (iLevel <= 2)
                {
                    return;
                }

                //此寫法可能有問題：原廠的 BUG，有時會取到錯誤的值
                sSchemaType = c1GridSchemaBrowser.Columns["SchemaType"].CellValue(((GroupRow)vr).StartIndex).ToString();

                //原廠給的暫時性解法
                for (var i = iDisplayRowIndex - 1; i >= 0; i--)
                {
                    //if above row is Group Row
                    if (!(c1GridSchemaBrowser.Splits[0].Rows[i] is GroupRow groupRow))
                    {
                        continue;
                    }

                    //if level is 2, meaning that this Group Row is for SchemaType
                    if (groupRow.Level != 2)
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
                    var sTempSchemaObject1 = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();
                    var sTempSchemaObject2 = dtSchema0.Select($"SchemaType = '{sSchemaType2}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();

                    if (string.IsNullOrEmpty(sTempSchemaObject1) && !string.IsNullOrEmpty(sTempSchemaObject2))
                    {
                        sSchemaType = sSchemaType2;
                    }
                }

                sSchemaName = ((GroupRow)vr).GroupedText;
                sSchemaNode = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '{sSchemaName}'").FirstOrDefault()?["SchemaNode"].ToString();
            }
            else
            {
                iLevel = 4;

                //以下可以取得游標所在列的「正確的值」，用原廠給的建議寫法
                sSchemaNode = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["SchemaNode"].ToString();
                sSchemaType = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["SchemaType"].ToString();
                sSchemaName = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["Schema_Browser"].ToString();
            }

            if (sSchemaType.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
            {
                sSchemaType = sSchemaType.Substring(0, sSchemaType.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
            }

            if (sSchemaName.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
            {
                sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
            }

            if (sSchemaName.IndexOf(", ", StringComparison.Ordinal) != -1) //欄位名稱
            {
                sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf(", ", StringComparison.Ordinal));
            }

            if (string.IsNullOrEmpty(sSchemaName))
            {
                //如果使用者有開啟「顯示欄位名稱」，除了「Table」外，其餘的項目，sSchemaName 都會是空值
                return;
            }

            //切換：當滑鼠點到 Grid 最右邊的空白處時，一樣會有點選的效果，但 Focus 列並不會同時切換，所以此處要強制切換
            c1GridSchemaBrowser.Row = iDisplayRowIndex;

            if (!bNode && MyGlobal.bShowColumnInfo)
            {
                return; //有顯示 Column Info，進入的不是空值就是欄位名稱，不用處理
            }

            editorSQLPane.ReadOnly = false;
            editorSQLPane.Text = "";
            editorSQLPane.ReadOnly = true;

            var sScripts = "";
            string sTemp;
            string sSql;

            if (sSchemaType != "Tables")
            {
                switch (sSchemaType)
                {
                    case "Views":
                        lblTableName1.Text = _sViewName;
                        lblTableName2.Text = _sViewName;
                        lblTableName3.Text = _sViewName;
                        lblTableName01.Text = sSchemaName;
                        lblTableName02.Text = sSchemaName;
                        lblTableName03.Text = sSchemaName;

                        sSql = "SELECT Definition AS Scripts FROM pg_catalog.pg_views WHERE SchemaName NOT IN ('pg_catalog', 'information_schema') AND SchemaName = '" + sSchemaNode + "' AND ViewName = '" + sSchemaName + "'";
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sScripts = dtTemp.Rows[0][0].ToString();
                        }

                        editorSQLPane.ReadOnly = false;
                        editorSQLPane.Text = "-- View: {0}\r\n\r\n-- DROP VIEW {0};\r\n\r\nCREATE OR REPLACE VIEW {0} AS \r\n{1}";
                        editorSQLPane.Text = string.Format(editorSQLPane.Text, sSchemaName, sScripts);
                        editorSQLPane.ReadOnly = true;

                        //取得指定 View 的前 100 筆資料
                        sSql = "SELECT * FROM {0}";
                        sSql = string.Format(sSql, sSchemaName);
                        ExecuteQuery100Rows(sSql, 0, 100);

                        tabView100RowsTop.Tag = sSchemaName;
                        tabView100RowsTop.TabVisible = true;

                        break;
                    case "Functions":
                        var sSchemaNameLong = sSchemaName;

                        if (sSchemaName.IndexOf("(", StringComparison.Ordinal) != -1)
                        {
                            sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf("(", StringComparison.Ordinal));
                        }

                        if (MyGlobal.sDBVersion == ">=11")
                        {
                            sSql = "SELECT PG_GET_FUNCTIONDEF(p.oid) AS Scripts FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE p.prokind <> 'p' and n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname <> 'trigger' AND n.nspname = '" + sSchemaNode + "' AND p.proname = '" + sSchemaName + "'";
                        }
                        else
                        {
                            sSql = "SELECT PG_GET_FUNCTIONDEF(p.oid) AS Scripts FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE NOT p.proisagg and n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname <> 'trigger' AND n.nspname = '" + sSchemaNode + "' AND p.proname = '" + sSchemaName + "'";
                        }

                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            if (dtTemp.Rows.Count == 1)
                            {
                                sScripts = dtTemp.Rows[0][0].ToString();
                            }
                            else
                            {
                                for (var i = 0; i < dtTemp.Rows.Count; i++)
                                {
                                    if (dtTemp.Rows[i][0].ToString().IndexOf(sSchemaNameLong, StringComparison.Ordinal) != -1)
                                    {
                                        sScripts = dtTemp.Rows[i][0].ToString();
                                        break;
                                    }
                                }
                            }
                        }

                        sTemp = sScripts.Substring(sScripts.IndexOf("(", StringComparison.Ordinal), sScripts.IndexOf(")", StringComparison.Ordinal) - sScripts.IndexOf("(", StringComparison.Ordinal) + 1);

                        editorSQLPane.ReadOnly = false;
                        editorSQLPane.Text = "-- Function: {0}{2}\r\n\r\n-- DROP FUNCTION {0}{2};\r\n\r\n{1}";
                        editorSQLPane.Text = string.Format(editorSQLPane.Text, sSchemaName, sScripts, sTemp);
                        editorSQLPane.ReadOnly = true;
                        break;
                    case "Triggers":
                        if (MyGlobal.sDBVersion == ">=11")
                        {
                            sSql = "SELECT PG_GET_FUNCTIONDEF(p.oid) AS Scripts FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE p.prokind <> 'p' AND n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname = 'trigger' AND n.nspname = '" + sSchemaNode + "' AND p.proname = '" + sSchemaName + "'";
                        }
                        else
                        {
                            sSql = "SELECT PG_GET_FUNCTIONDEF(p.oid) AS Scripts FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE NOT p.proisagg AND n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname = 'trigger' AND n.nspname = '" + sSchemaNode + "' AND p.proname = '" + sSchemaName + "'";
                        }

                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sScripts = dtTemp.Rows[0][0].ToString();
                        }

                        //pgAdmin 顯示 Trigger 為 Function，故此處比照辦理
                        editorSQLPane.ReadOnly = false;
                        editorSQLPane.Text = "-- Function: {0}()\r\n\r\n-- DROP FUNCTION {0}();\r\n\r\n{1}"; //pgAdmin 顯示為 Function (完整名稱為 Trigger Functions)
                        editorSQLPane.Text = string.Format(editorSQLPane.Text, sSchemaName, sScripts);
                        editorSQLPane.ReadOnly = true;
                        break;
                }
            }
            else
            {
                tabTableStructure.Tag = sSchemaName;
                tabTableStructure.TabVisible = true;
                tabView100RowsTop.Tag = sSchemaName;
                tabView100RowsTop.TabVisible = true;
                //tabView100RowsLast.Tag = sSchemaName;
                //tabView100RowsLast.TabVisible = true;
                lblTableName1.Text = _sTableName;
                lblTableName2.Text = _sTableName;
                lblTableName3.Text = _sTableName;
                lblTableName01.Text = sSchemaName;
                lblTableName02.Text = sSchemaName;
                lblTableName03.Text = sSchemaName;

                CreateTableSchemaTable(); //DisplayInfo_PostgreSQL

                #region 取得指定的 Table's 欄位資訊
                //Constraint資訊
                sSql = "SELECT ee.Constraint_Schema AS SchemaName, ee.Table_Name AS TableName, ee.Column_Name AS ColumnName, LOWER(SUBSTR(ss.Constraint_Type, 1, 1)) || ', ' || ss.Constraint_Name AS ConstraintInfo FROM information_schema.key_column_usage ee, information_schema.table_constraints ss WHERE ee.Constraint_Catalog = ss.Constraint_Catalog AND ee.Constraint_Schema = ss.Constraint_Schema AND ee.Table_Schema = ss.Table_Schema AND ee.Table_Name = ss.Table_Name AND ee.Constraint_Name = ss.Constraint_Name AND ss.Constraint_Type <> 'CHECK' AND ee.Constraint_Schema = '{0}' AND ee.Table_Name = '{1}' ORDER BY ee.Ordinal_Position";
                sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                var dtConstraint = new DataTable();
                ExecuteQueryToDataTable(sSql, ref dtConstraint);

                //Comment資訊
                //20230315 0.81 版(含) 之前所用的 SQL，有些會抓不到
                //sSql = "SELECT cols.Table_Schema AS SchemaName, cols.Table_Name AS TableName, cols.Column_Name AS ColumnName, pg_catalog.COL_DESCRIPTION(c.oid, cols.Ordinal_Position::int) AS Comments FROM pg_catalog.pg_class c, information_schema.columns cols WHERE cols.Table_Name = c.relname AND cols.Table_Schema = '{0}' AND cols.Table_Name = '{1}'";
                //sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                //20230321 改用這段 SQL，但「閔」還是反應抓不到
                //sSql = "SELECT a.attname AS ColumnName, des.description AS Comments FROM pg_attribute AS a, pg_description AS des, pg_class AS pgc WHERE pgc.oid = a.attrelid AND des.objoid = pgc.oid AND pg_table_is_visible(pgc.oid) AND pgc.relname = '{0}' AND a.attnum = des.objsubid;";
                //sSql = string.Format(sSql, sSchemaName);
                //20230322 改用「閔」公司 DBA 所提供的 SQL
                sSql = "SELECT cols.Table_Schema AS SchemaName, cols.Table_Name AS TableName, cols.Column_Name AS ColumnName, (pg_catalog.COL_DESCRIPTION((table_schema || '.' || table_name)::regclass::oid, ordinal_position::int)) AS Comments FROM information_schema.columns cols WHERE Table_Schema = '{0}' AND Table_Name = '{1}';";
                sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                var dtComment = new DataTable();
                ExecuteQueryToDataTable(sSql, ref dtComment);

                sSql = "SELECT ts.SchemaName, 'Tables' AS SchemaType, ts.TableName, cs.Column_Name AS ColumnName, cs.Ordinal_Position AS ColumnID, SUBSTR(cs.Is_Nullable, 1, 1) AS Nullable, cs.Column_Default AS DefaultValue, LOWER(cs.Data_Type) AS DataType, cs.Character_Maximum_Length AS DataLength, cs.Numeric_Precision || ',' || cs.Numeric_Scale AS Scale FROM pg_catalog.pg_tables ts, information_schema.columns cs WHERE ts.SchemaName != 'pg_catalog' AND ts.SchemaName != 'information_schema' AND ts.SchemaName = cs.Table_Schema AND ts.TableName = cs.Table_Name AND ts.SchemaName = '{0}' AND ts.TableName = '{1}' ORDER BY cs.Ordinal_Position";
                sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                ExecuteQueryToDataTable(sSql, ref dtTemp);

                if (!(dtTemp != null && dtTemp.Rows.Count > 0))
                {
                    c1GridStructure.DataSource = null;
                    c1Grid100RowsTop.DataSource = null;
                    c1Grid100RowsLast.DataSource = null;
                }
                else
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        var row = dtTableSchema.NewRow();
                        row["ColumnName"] = dtTemp.Rows[iRow]["ColumnName"].ToString();
                        row["ID"] = dtTemp.Rows[iRow]["ColumnID"].ToString();

                        switch (dtTemp.Rows[iRow]["DataType"].ToString())
                        {
                            case "character":
                                row["DataType"] = dtTemp.Rows[iRow]["DataType"] + " (1)";
                                break;
                            case "character varying":
                                row["DataType"] = dtTemp.Rows[iRow]["DataType"] + " (" + dtTemp.Rows[iRow]["DataLength"] + ")";
                                break;
                            case "bigint":
                                row["DataType"] = dtTemp.Rows[iRow]["DataType"] + " (64)";
                                break;
                            case "integer":
                                row["DataType"] = dtTemp.Rows[iRow]["DataType"] + " (32)";
                                break;
                            case "smallint":
                                row["DataType"] = dtTemp.Rows[iRow]["DataType"] + " (16)";
                                break;
                            case "numeric":
                                if (string.IsNullOrEmpty(dtTemp.Rows[iRow]["Scale"].ToString()))
                                {
                                    row["DataType"] = dtTemp.Rows[iRow]["DataType"].ToString();
                                }
                                else
                                {
                                    row["DataType"] = dtTemp.Rows[iRow]["DataType"] + " (" + dtTemp.Rows[iRow]["Scale"] + ")";
                                }
                                break;
                            default:
                                row["DataType"] = dtTemp.Rows[iRow]["DataType"].ToString();
                                break;
                        }

                        var sConstraintInfo = "";
                        var dtRow = dtConstraint.Select("SchemaName = '" + dtTemp.Rows[iRow]["SchemaName"].ToString().Replace("'", "''") + "' AND TableName = '" + dtTemp.Rows[iRow]["TableName"] + "' AND ColumnName = '" + dtTemp.Rows[iRow]["ColumnName"].ToString().Replace("'", "''") + "'");

                        if (dtRow.Length > 0)
                        {
                            sConstraintInfo = dtRow[0]["ConstraintInfo"].ToString();
                        }

                        row["ConstraintInfo"] = sConstraintInfo;

                        sTemp = dtTemp.Rows[iRow]["DefaultValue"].ToString();
                        sTemp = sTemp.Replace("::text", "");
                        sTemp = sTemp.Replace("::timestamp without time zone", "");
                        sTemp = sTemp.Replace("::character varying", "");
                        sTemp = sTemp.Replace("::bpchar", "");
                        sTemp = sTemp.Replace("::regclass", "");
                        row["Default"] = sTemp;

                        row["Nullable"] = dtTemp.Rows[iRow]["Nullable"].ToString();

                        sTemp = "";
                        //dtRow = dtComment.Select("ColumnName = '" + dtTemp.Rows[iRow]["ColumnName"].ToString().Replace("'", "''") + "'");
                        dtRow = dtComment.Select("SchemaName = '" + dtTemp.Rows[iRow]["SchemaName"].ToString().Replace("'", "''") + "' and TableName = '" + dtTemp.Rows[iRow]["TableName"].ToString().Replace("'", "''") + "' and ColumnName = '" + dtTemp.Rows[iRow]["ColumnName"].ToString().Replace("'", "''") + "'");

                        if (dtRow.Length > 0)
                        {
                            sTemp = dtRow[0]["Comments"].ToString().Trim();
                        }

                        row["Comments"] = sTemp;

                        dtTableSchema.Rows.Add(row);
                    }

                    c1GridStructure.DataSource = dtTableSchema;

                    foreach (C1DisplayColumn col in c1GridStructure.Splits[0].DisplayColumns)
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

                    //取得指定 Table 的前 100 筆資料
                    sSql = "SELECT * FROM {0}.{1}";
                    sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                    ExecuteQuery100Rows(sSql, 0, 100);
                }
                #endregion 取得指定的 Table's 欄位資訊

                //取得指定的 Table's create script
                var sValue = MyGlobal.GetCreateScript_PostgreSQL(sSchemaNode, sSchemaType, sSchemaName);
                editorSQLPane.ReadOnly = false;
                editorSQLPane.Text = sValue[0];
                editorSQLPane.ReadOnly = true;
            }

            editorSQLPane.Focus();
        }

        private void DisplayInfo_SQLServer(int iDisplayRowIndex)
        {
            string sSchemaNode;
            string sSchemaType;
            var sSchemaType2 = "";
            string sSchemaName;
            string sSchemaDbo;
            string sObjectID;
            string sCreateDate;
            string sModifyDate;
            int iLevel;
            var dtTemp = new DataTable();
            var bNode = false; //是否為節點

            Application.DoEvents();

            tabTableStructure.TabVisible = false;
            tabView100RowsTop.TabVisible = false;
            tabView100RowsLast.TabVisible = false;

            var vr = c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex];

            //NG: sSchemaType = c1GridSchemaBrowser.Columns["SchemaType"].CellValue(vr.DataRowIndex).ToString();
            //sSchemaType = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["SchemaType"].ToString();

            if (c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex].RowType == RowTypeEnum.CollapsedGroupRow || c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex].RowType == RowTypeEnum.ExpandedGroupRow)
            {
                bNode = true;

                //以下可以取得游標所在列的節點的「正確的值」
                iLevel = ((GroupRow)vr).Level;

                if (iLevel <= 2)
                {
                    return;
                }

                //此寫法可能有問題：原廠的 BUG，有時會取到錯誤的值
                sSchemaType = c1GridSchemaBrowser.Columns["SchemaType"].CellValue(((GroupRow)vr).StartIndex).ToString();

                //原廠給的暫時性解法
                for (var i = iDisplayRowIndex - 1; i >= 0; i--)
                {
                    //if above row is Group Row
                    if (!(c1GridSchemaBrowser.Splits[0].Rows[i] is GroupRow groupRow))
                    {
                        continue;
                    }

                    //if level is 2, meaning that this Group Row is for SchemaType
                    if (groupRow.Level != 2)
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
                    var sTempSchemaObject1 = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();
                    var sTempSchemaObject2 = dtSchema0.Select($"SchemaType = '{sSchemaType2}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();

                    if (string.IsNullOrEmpty(sTempSchemaObject1) && !string.IsNullOrEmpty(sTempSchemaObject2))
                    {
                        sSchemaType = sSchemaType2;
                    }
                }

                sSchemaName = ((GroupRow)vr).GroupedText;
                sSchemaNode = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '{sSchemaName}'").FirstOrDefault()?["SchemaNode"].ToString();
                sSchemaDbo = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '{sSchemaName}'").FirstOrDefault()?["SchemaDbo"].ToString();
                sObjectID = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '{sSchemaName}'").FirstOrDefault()?["ObjectID"].ToString();
                sCreateDate = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '{sSchemaName}'").FirstOrDefault()?["CreateDate"].ToString();
                sModifyDate = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '{sSchemaName}'").FirstOrDefault()?["ModifyDate"].ToString();
            }
            else
            {
                iLevel = 4;

                //以下可以取得游標所在列的「正確的值」，用原廠給的建議寫法
                sSchemaNode = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["SchemaNode"].ToString();
                sSchemaType = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["SchemaType"].ToString();
                sSchemaName = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["Schema_Browser"].ToString();
                sSchemaDbo = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["SchemaDbo"].ToString();
                sObjectID = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["ObjectID"].ToString();
                sCreateDate = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["CreateDate"].ToString();
                sModifyDate = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["ModifyDate"].ToString();
            }

            if (sSchemaType.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
            {
                sSchemaType = sSchemaType.Substring(0, sSchemaType.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
            }

            if (sSchemaName.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
            {
                sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
            }

            if (sSchemaName.IndexOf(", ", StringComparison.Ordinal) != -1) //欄位名稱
            {
                sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf(", ", StringComparison.Ordinal));
            }

            if (string.IsNullOrEmpty(sSchemaName))
            {
                //如果使用者有開啟「顯示欄位名稱」，除了「Table」外，其餘的項目，sSchemaName 都會是空值
                return;
            }

            //切換：當滑鼠點到 Grid 最右邊的空白處時，一樣會有點選的效果，但 Focus 列並不會同時切換，所以此處要強制切換
            c1GridSchemaBrowser.Row = iDisplayRowIndex;

            if (!bNode && MyGlobal.bShowColumnInfo)
            {
                return; //有顯示 Column Info，進入的不是空值就是欄位名稱，不用處理
            }

            editorSQLPane.ReadOnly = false;
            editorSQLPane.Text = "";
            editorSQLPane.ReadOnly = true;

            string sSql;
            string sSqlPane;
            DateTime dtDateTime;

            //取得 Table/Function/Trigger/View 的「名稱、狀態、建立日期」
            string sTemp;

            switch (sSchemaType)
            {
                case "Tables":
                    {
                        tabTableStructure.Tag = sSchemaName;
                        tabTableStructure.TabVisible = true;
                        tabView100RowsTop.Tag = sSchemaName;
                        tabView100RowsTop.TabVisible = true;
                        //tabView100RowsLast.Tag = sSchemaName;
                        //tabView100RowsLast.TabVisible = true;
                        lblTableName1.Text = _sTableName;
                        lblTableName2.Text = _sTableName;
                        lblTableName3.Text = _sTableName;
                        lblTableName01.Text = sSchemaName;
                        lblTableName02.Text = sSchemaName;
                        lblTableName03.Text = sSchemaName;

                        CreateTableSchemaTable(); //DisplayInfo_SQLServer

                        var sValue = MyGlobal.GetCreateScript_SQLServer(sSchemaType, sSchemaNode, sSchemaDbo, sSchemaName.Replace(sSchemaDbo + ".", ""));
                        editorSQLPane.ReadOnly = false;
                        editorSQLPane.Text = sValue[0];
                        editorSQLPane.ReadOnly = true;

                        #region 取得指定的 Table's 欄位資訊
                        //取得Constraint資訊
                        sSql = "SELECT col.Column_Name, col.Ordinal_Position, con.Constraint_Name, con.Constraint_Type FROM {0}.INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS col, {0}.INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS con WHERE col.Table_Schema = '{1}' AND col.Table_Name = '{2}' AND col.Table_Schema = con.Table_Schema AND col.Table_Name = con.Table_Name AND col.Constraint_Name = con.Constraint_Name ORDER BY col.Ordinal_Position;"; //排序是為後面要取 Create Table 的 Constraint 資料
                        sSql = string.Format(sSql, sSchemaNode, sSchemaDbo, sSchemaName.Replace(sSchemaDbo + ".", ""));
                        var dtConstraint = new DataTable();
                        ExecuteQueryToDataTable(sSql, ref dtConstraint);

                        //取得Comment資訊
                        sSql = "SELECT c.Name AS \"Column_Name\", prop.Value AS \"comment\" FROM {0}.sys.extended_properties AS prop INNER JOIN {0}.sys.all_objects o ON prop.Major_ID = o.Object_ID INNER JOIN {0}.sys.schemas s ON o.Schema_ID = s.Schema_ID INNER JOIN {0}.sys.columns AS c ON prop.Major_ID = c.Object_ID AND prop.Minor_ID = c.Column_ID WHERE prop.Name = 'MS_Description' AND s.Name = '{1}' AND o.Name = '{2}';";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaDbo, sSchemaName.Replace(sSchemaDbo + ".", ""));
                        var dtComment = new DataTable();
                        ExecuteQueryToDataTable(sSql, ref dtComment);

                        //取得所有欄位資訊
                        sSql = "SELECT * FROM {0}.INFORMATION_SCHEMA.COLUMNS WHERE Table_Schema = '{1}' AND Table_Name = '{2}' ORDER BY Ordinal_Position;";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaDbo, sSchemaName.Replace(sSchemaDbo + ".", ""));
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            var sConstraintName = "";

                            for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                            {
                                var row = dtTableSchema.NewRow();
                                row["ColumnName"] = dtTemp.Rows[iRow]["Column_Name"].ToString();
                                row["ID"] = dtTemp.Rows[iRow]["Ordinal_Position"].ToString();

                                var sDataType = dtTemp.Rows[iRow]["Data_Type"].ToString();
                                var sCollation = string.IsNullOrEmpty(dtTemp.Rows[iRow]["Collation_Name"].ToString()) ? "" : " COLLATE " + dtTemp.Rows[iRow]["Collation_Name"];
                                var sIsNullable = sCollation + "```DEFAULT```" + (dtTemp.Rows[iRow]["Is_Nullable"].ToString() == "NO" ? " NOT NULL" : " NULL");

                                switch (sDataType.ToUpper())
                                {
                                    case "NCHAR":
                                    case "BINARY":
                                    case "CHAR":
                                        row["DataType"] = sDataType + "(" + dtTemp.Rows[iRow]["Character_Maximum_Length"] + ")";
                                        break;
                                    case "NVARCHAR":
                                    case "VBINARY":
                                    case "VARCHAR":
                                    case "VARBINARY":
                                        if (dtTemp.Rows[iRow]["Character_Maximum_Length"].ToString() == "-1")
                                        {
                                            row["DataType"] = sDataType + "(max)";
                                        }
                                        else
                                        {
                                            row["DataType"] = sDataType + "(" + dtTemp.Rows[iRow]["Character_Maximum_Length"] + ")";
                                        }

                                        break;
                                    case "DECIMAL":
                                    case "NUMERIC":
                                        row["DataType"] = sDataType + "(" + dtTemp.Rows[iRow]["Numeric_Precision"] + ", " + dtTemp.Rows[iRow]["Numeric_Scale"] + ")";
                                        break;
                                    case "TIME":
                                    case "DATETIME2":
                                    case "DATETIMEOFFSET":
                                        row["DataType"] = sDataType + "(" + dtTemp.Rows[iRow]["DateTime_Precision"] + ")";
                                        break;
                                    default:
                                        row["DataType"] = sDataType;
                                        break;
                                }

                                DataRow[] dtRow;
                                var sConstraintInfo = "";

                                if (dtConstraint != null && dtConstraint.Rows.Count > 0)
                                {
                                    dtRow = dtConstraint.Select("Column_Name='" + dtTemp.Rows[iRow]["Column_Name"].ToString().Replace("'", "''") + "'");

                                    if (dtRow.Length > 0)
                                    {
                                        sConstraintName = dtRow[0]["Constraint_Name"].ToString();
                                        sConstraintInfo = dtRow[0]["Constraint_Type"].ToString();
                                        sConstraintInfo = dtRow[0]["Constraint_Name"] + (string.IsNullOrEmpty(sConstraintInfo) ? "" : ", " + sConstraintInfo);
                                    }
                                }

                                row["ConstraintInfo"] = sConstraintInfo;

                                var sColumnDefault = dtTemp.Rows[iRow]["Column_Default"].ToString();

                                if (sColumnDefault.Length > 2 && sColumnDefault.Substring(0, 1) == "(" && sColumnDefault.Substring(sColumnDefault.Length - 1, 1) == ")")
                                {
                                    sColumnDefault = sColumnDefault.Substring(1, sColumnDefault.Length - 2);

                                    if (sColumnDefault.Length > 2 && sColumnDefault.Substring(0, 1) == "(" && sColumnDefault.Substring(sColumnDefault.Length - 1, 1) == ")")
                                    {
                                        sColumnDefault = sColumnDefault.Substring(1, sColumnDefault.Length - 2);
                                    }
                                }

                                row["Default"] = sColumnDefault;
                                row["Nullable"] = dtTemp.Rows[iRow]["Is_Nullable"].ToString();

                                var sComment = "";

                                if (dtComment != null && dtComment.Rows.Count > 0)
                                {
                                    dtRow = dtComment.Select("Column_Name='" + dtTemp.Rows[iRow]["Column_Name"].ToString().Replace("'", "''") + "'");

                                    if (dtRow.Length > 0)
                                    {
                                        sComment = dtRow[0]["Comment"].ToString();
                                    }
                                }

                                row["Comments"] = sComment;

                                dtTableSchema.Rows.Add(row);
                            }

                            c1GridStructure.DataSource = dtTableSchema;

                            foreach (C1DisplayColumn col in c1GridStructure.Splits[0].DisplayColumns)
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

                            //取得指定 Table 的前 100 筆資料
                            sSql = "SELECT * FROM {0}";
                            sSql = string.Format(sSql, sSchemaName.Replace(sSchemaDbo + ".", ""));
                            ExecuteQuery100Rows(sSql, 0, 100);
                        }
                        #endregion

                        break;
                    }
                case "Functions":
                    {
                        sSql = "SELECT m.Definition, o.* FROM {0}.sys.objects o INNER JOIN {0}.sys.sql_modules m ON o.Object_ID = m.Object_ID WHERE o.Type = 'FN' AND o.Is_Ms_Shipped <> 1 AND o.Name = '{1}';";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName.Replace(sSchemaDbo + ".", ""));
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Create_Date"].ToString());
                            sTemp = "-- Function: " + sSchemaDbo + "." + sSchemaName + "\r\n-- Created: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "{0}\r\n\r\n";
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Modify_Date"].ToString());
                            sTemp = string.Format(sTemp, "\r\n-- Modified: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss"));

                            editorSQLPane.ReadOnly = false;
                            editorSQLPane.Text = sTemp + dtTemp.Rows[0]["Definition"] + "\r\nGO";
                            editorSQLPane.ReadOnly = true;
                        }

                        break;
                    }
                case "Triggers":
                    {
                        sSql = "SELECT m.Definition, o.* FROM {0}.sys.objects o INNER JOIN {0}.sys.sql_modules m ON o.Object_ID = m.Object_ID WHERE o.Type = 'TR' AND o.Is_Ms_Shipped <> 1 AND o.Name = '{1}';";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName.Replace(sSchemaDbo + ".", ""));
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Create_Date"].ToString());
                            sTemp = "-- Trigger: " + sSchemaDbo + "." + sSchemaName + "\r\n-- Created: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "{0}\r\n\r\n";
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Modify_Date"].ToString());
                            sTemp = string.Format(sTemp, "\r\n-- Modified: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss"));

                            editorSQLPane.ReadOnly = false;
                            editorSQLPane.Text = sTemp + dtTemp.Rows[0]["Definition"] + "\r\nGO";
                            editorSQLPane.ReadOnly = true;
                        }

                        break;
                    }
                case "Procedures":
                    {
                        sSql = "SELECT m.Definition, o.* FROM {0}.sys.objects o INNER JOIN {0}.sys.sql_modules m ON o.Object_ID = m.Object_ID WHERE o.Type = 'P' AND o.Is_Ms_Shipped <> 1 AND o.Name = '{1}';";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName.Replace(sSchemaDbo + ".", ""));
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Create_Date"].ToString());
                            sTemp = "-- Procedure: " + sSchemaDbo + "." + sSchemaName + "\r\n-- Created: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "{0}\r\n\r\n";
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Modify_Date"].ToString());
                            sTemp = string.Format(sTemp, "\r\n-- Modified: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss"));

                            editorSQLPane.ReadOnly = false;
                            editorSQLPane.Text = sTemp + dtTemp.Rows[0]["Definition"] + "\r\nGO";
                            editorSQLPane.ReadOnly = true;
                        }

                        break;
                    }
                case "Views":
                    {
                        lblTableName1.Text = _sViewName;
                        lblTableName2.Text = _sViewName;
                        lblTableName3.Text = _sViewName;
                        lblTableName01.Text = sSchemaName;
                        lblTableName02.Text = sSchemaName;
                        lblTableName03.Text = sSchemaName;

                        sSql = "SELECT m.Definition, o.* FROM {0}.sys.objects o INNER JOIN {0}.sys.sql_modules m ON o.Object_ID = m.Object_ID WHERE o.Type = 'V' AND o.Is_Ms_Shipped <> 1 AND o.Name = '{1}';";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName.Replace(sSchemaDbo + ".", ""));
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (!(dtTemp != null && dtTemp.Rows.Count > 0))
                        {
                            c1GridStructure.DataSource = null;
                            c1Grid100RowsTop.DataSource = null;
                            c1Grid100RowsLast.DataSource = null;
                        }
                        else
                        {
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Create_Date"].ToString());
                            sTemp = "-- View: " + sSchemaDbo + "." + sSchemaName + "\r\n-- Created: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "{0}\r\n\r\n";
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Modify_Date"].ToString());
                            sTemp = string.Format(sTemp, "\r\n-- Modified: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss"));

                            editorSQLPane.ReadOnly = false;
                            editorSQLPane.Text = sTemp + dtTemp.Rows[0]["Definition"] + "\r\nGO";
                            editorSQLPane.ReadOnly = true;

                            //取得指定 View 的前 100 筆資料
                            sSql = "SELECT * FROM {0}";
                            sSql = string.Format(sSql, sSchemaName.Replace(sSchemaDbo + ".", ""));
                            ExecuteQuery100Rows(sSql, 0, 100);

                            tabView100RowsTop.Tag = sSchemaName;
                            tabView100RowsTop.TabVisible = true;
                        }

                        break;
                    }
                case "Indices":
                    {
                        sSqlPane = "";
                        editorSQLPane.ReadOnly = false;
                        editorSQLPane.Text = "";
                        editorSQLPane.ReadOnly = true;

                        if (sSchemaName.IndexOf(" on ", StringComparison.Ordinal) == -1)
                            return;

                        var sTableName = sSchemaName.Replace(sSchemaDbo + ".", "").Split(new string[] { " on " }, StringSplitOptions.RemoveEmptyEntries)[1];
                        var sIndexName = sSchemaName.Replace(sSchemaDbo + ".", "").Split(new string[] { " on " }, StringSplitOptions.RemoveEmptyEntries)[0];

                        sSql = "";
                        sSql += "SELECT i.Name, t.Name AS tblname, i.Index_ID, i.Type_Desc, u.Name AS uname, i.Object_ID, g.Name AS GroupName, i.Is_Padded, s.No_Recompute, i.Ignore_Dup_Key, i.Allow_Row_Locks, i.Allow_Page_Locks, t.Create_Date, t.Modify_Date\r\n";
                        sSql += "FROM {0}.sys.indexes i\r\n";
                        sSql += "LEFT OUTER JOIN {0}.sys.data_spaces g ON i.Data_Space_ID = g.Data_Space_ID\r\n";
                        sSql += "INNER JOIN {0}.sys.objects t ON t.Object_ID = i.Object_ID\r\n";
                        sSql += "INNER JOIN {0}.sys.schemas u ON t.Schema_ID = u.Schema_ID\r\n";
                        sSql += "LEFT OUTER JOIN {0}.sys.stats s ON s.Object_ID = i.Object_ID AND s.Stats_ID = i.Index_ID\r\n";
                        sSql += "WHERE i.Index_ID > 0 AND t.Type = 'U' AND u.Name = '{1}' AND t.Name = '{2}' AND i.Name = '{3}'";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaDbo, sTableName, sIndexName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sSql = "";
                            sSql += "SELECT tab.Name Table_Name,\r\n";
                            sSql += "       ix.Name Index_Name,\r\n";
                            sSql += "       col.Name Index_Column_Name,\r\n";
                            sSql += "       ixc.Index_Column_ID\r\n";
                            sSql += "  FROM {0}.sys.indexes ix\r\n";
                            sSql += "       INNER JOIN {0}.sys.index_columns ixc ON ix.Object_ID = ixc.Object_ID AND ix.Index_ID = ixc.Index_ID\r\n";
                            sSql += "       INNER JOIN {0}.sys.columns col ON ix.Object_ID = col.Object_ID AND ixc.Column_ID = col.Column_ID\r\n";
                            sSql += "       INNER JOIN {0}.sys.tables tab ON ix.Object_ID = tab.Object_ID\r\n";
                            sSql += " WHERE ix.Object_ID = {1} AND ix.Name = '{2}' ORDER BY ixc.Index_ID, ixc.Index_Column_ID";
                            sSql = string.Format(sSql, sSchemaNode, sObjectID, sIndexName);
                            var dtTemp2 = new DataTable();
                            ExecuteQueryToDataTable(sSql, ref dtTemp2);

                            if (dtTemp2 != null && dtTemp2.Rows.Count > 0)
                            {
                                for (var i = 0; i < dtTemp2.Rows.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Create_Date"].ToString());
                                        sSqlPane = "-- Index: " + sIndexName + "\r\n-- Created: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "{0}\r\n\r\n";
                                        dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Modify_Date"].ToString());
                                        sSqlPane = string.Format(sSqlPane, "\r\n-- Modified: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss"));

                                        if (dtTemp.Rows[0]["Type_Desc"].ToString() == "NONCLUSTERED")
                                        {
                                            sSqlPane += "CREATE NONCLUSTERED INDEX {0} ON {1}.{2}\r\n(\r\n    [{3}]";
                                            sSqlPane = string.Format(sSqlPane, sIndexName, sSchemaDbo, sTableName, dtTemp2.Rows[i]["Index_Column_Name"]);
                                        }
                                        else
                                        {
                                            sSqlPane += "ALTER TABLE {0}.{1}\r\nADD CONSTRAINT {2}\r\n";
                                            sSqlPane += "PRIMARY KEY CLUSTERED\r\n(\r\n    [{3}]";
                                            sSqlPane = string.Format(sSqlPane, sSchemaDbo, sTableName, sIndexName, dtTemp2.Rows[i]["Index_Column_Name"]);
                                        }
                                    }
                                    else
                                    {
                                        sSqlPane += ",\r\n    [{0}]";
                                        sSqlPane = string.Format(sSqlPane, dtTemp2.Rows[i]["Index_Column_Name"]);
                                    }

                                    //取得欄位的排序方式
                                    sSql = "SELECT i.Name, ic.Index_Column_ID, ic.Is_Descending_Key FROM {0}.sys.indexes i JOIN {0}.sys.index_columns ic ON i.Index_ID = ic.Index_ID AND i.Object_ID = ic.Object_ID AND i.Name = '{1}' AND ic.Index_Column_ID = {2}";
                                    sSql = string.Format(sSql, sSchemaNode, sIndexName, dtTemp2.Rows[i]["Index_Column_ID"]);
                                    var dtTemp0 = new DataTable();
                                    ExecuteQueryToDataTable(sSql, ref dtTemp0);

                                    if (dtTemp0 != null && dtTemp0.Rows.Count > 0)
                                    {
                                        sSqlPane += dtTemp0.Rows[0]["Is_Descending_Key"].ToString() == "True" ? " DESC" : " ASC";
                                    }

                                    if (dtTemp2.Rows.Count == 1 || i == dtTemp2.Rows.Count - 1) //最後一筆
                                    {
                                        sSqlPane += "\r\n)\r\n";
                                    }
                                }

                                sSqlPane += "WITH\r\n(\r\n";

                                if (dtTemp.Rows[0]["Type_Desc"].ToString() == "NONCLUSTERED")
                                {
                                    sSqlPane += "    PAD_INDEX = {0},\r\n";
                                    sSqlPane += "    DROP_EXISTING = OFF,\r\n";
                                    sSqlPane += "    STATISTICS_NORECOMPUTE = {1},\r\n";
                                    sSqlPane += "    SORT_IN_TEMPDB = OFF,\r\n";
                                    sSqlPane += "    ONLINE = OFF,\r\n";
                                    sSqlPane += "    ALLOW_ROW_LOCKS = {2},\r\n";
                                    sSqlPane += "    ALLOW_PAGE_LOCKS = {3}\r\n)\r\n";
                                    sSqlPane += "ON [{4}]\r\n";
                                    sSqlPane = string.Format(sSqlPane, dtTemp.Rows[0]["Is_Padded"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[0]["No_Recompute"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[0]["Allow_Row_Locks"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[0]["Allow_Page_Locks"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[0]["GroupName"]);
                                }
                                else
                                {
                                    sSqlPane += "    PAD_INDEX = {0},\r\n";
                                    sSqlPane += "    IGNORE_DUP_KEY = {1},\r\n";
                                    sSqlPane += "    STATISTICS_NORECOMPUTE = {2},\r\n";
                                    sSqlPane += "    ALLOW_ROW_LOCKS = {3},\r\n";
                                    sSqlPane += "    ALLOW_PAGE_LOCKS = {4}\r\n)\r\n";
                                    sSqlPane += "ON [{5}]\r\n";
                                    sSqlPane = string.Format(sSqlPane, dtTemp.Rows[0]["Is_Padded"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[0]["Ignore_Dup_Key"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[0]["No_Recompute"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[0]["Allow_Row_Locks"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[0]["Allow_Page_Locks"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[0]["GroupName"]);
                                }

                                sSqlPane += "\r\nGO\r\n\r\n";
                            }
                        }

                        if (sSqlPane.EndsWith("\r\n\r\n"))
                        {
                            sSqlPane = sSqlPane.Substring(0, sSqlPane.Length - 4);
                        }

                        editorSQLPane.ReadOnly = false;
                        editorSQLPane.Text = sSqlPane;
                        editorSQLPane.ReadOnly = true;

                        break;
                    }
            }

            editorSQLPane.Focus();
        }

        private void DisplayInfo_MySQL(int iDisplayRowIndex)
        {
            string sSchemaNode;
            string sSchemaType;
            var sSchemaType2 = "";
            string sSchemaName;
            int iLevel;
            var dtTemp = new DataTable();
            var bNode = false; //是否為節點

            Application.DoEvents();

            tabTableStructure.TabVisible = false;
            tabView100RowsTop.TabVisible = false;
            tabView100RowsLast.TabVisible = false;

            var vr = c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex];

            if (c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex].RowType == RowTypeEnum.CollapsedGroupRow || c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex].RowType == RowTypeEnum.ExpandedGroupRow)
            {
                bNode = true;

                //以下可以取得游標所在列的節點的「正確的值」
                iLevel = ((GroupRow)vr).Level;

                if (iLevel <= 2)
                {
                    return;
                }

                //此寫法可能有問題：原廠的 BUG，有時會取到錯誤的值
                sSchemaType = c1GridSchemaBrowser.Columns["SchemaType"].CellValue(((GroupRow)vr).StartIndex).ToString();

                //原廠給的暫時性解法
                for (var i = iDisplayRowIndex - 1; i >= 0; i--)
                {
                    //if above row is Group Row
                    if (!(c1GridSchemaBrowser.Splits[0].Rows[i] is GroupRow groupRow))
                    {
                        continue;
                    }

                    //if level is 2, meaning that this Group Row is for SchemaType
                    if (groupRow.Level != 2)
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
                    var sTempSchemaObject1 = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();
                    var sTempSchemaObject2 = dtSchema0.Select($"SchemaType = '{sSchemaType2}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();

                    if (string.IsNullOrEmpty(sTempSchemaObject1) && !string.IsNullOrEmpty(sTempSchemaObject2))
                    {
                        sSchemaType = sSchemaType2;
                    }
                }

                sSchemaName = ((GroupRow)vr).GroupedText;
                sSchemaNode = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '{sSchemaName}'").FirstOrDefault()?["SchemaNode"].ToString();
            }
            else
            {
                iLevel = 4;

                //以下可以取得游標所在列的「正確的值」，用原廠給的建議寫法
                sSchemaNode = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["SchemaNode"].ToString();
                sSchemaType = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["SchemaType"].ToString();
                sSchemaName = (c1GridSchemaBrowser.GetDataBoundItem(iDisplayRowIndex) as DataRowView).Row["Schema_Browser"].ToString();
            }

            if (sSchemaType.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
            {
                sSchemaType = sSchemaType.Substring(0, sSchemaType.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
            }

            if (sSchemaName.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal) != -1)
            {
                sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf(MyGlobal.sSeparator, StringComparison.Ordinal));
            }

            if (sSchemaName.IndexOf(", ", StringComparison.Ordinal) != -1) //欄位名稱
            {
                sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf(", ", StringComparison.Ordinal));
            }

            if (string.IsNullOrEmpty(sSchemaName))
            {
                //如果使用者有開啟「顯示欄位名稱」，除了「Table」外，其餘的項目，sSchemaName 都會是空值
                return;
            }

            //切換：當滑鼠點到 Grid 最右邊的空白處時，一樣會有點選的效果，但 Focus 列並不會同時切換，所以此處要強制切換
            c1GridSchemaBrowser.Row = iDisplayRowIndex;

            if (!bNode && MyGlobal.bShowColumnInfo)
            {
                return; //有顯示 Column Info，進入的不是空值就是欄位名稱，不用處理
            }

            editorSQLPane.ReadOnly = false;
            editorSQLPane.Text = "";
            editorSQLPane.ReadOnly = true;

            var sScripts = "";

            if (sSchemaType.IndexOf(" (", StringComparison.Ordinal) != -1 && sSchemaType.EndsWith(")"))
            {
                sSchemaType = sSchemaType.Substring(0, sSchemaType.IndexOf(" (", StringComparison.Ordinal));
            }

            string sSql;

            if (sSchemaType != "Tables")
            {
                switch (sSchemaType)
                {
                    case "Views":
                        lblTableName1.Text = _sViewName;
                        lblTableName2.Text = _sViewName;
                        lblTableName3.Text = _sViewName;
                        lblTableName01.Text = sSchemaName;
                        lblTableName02.Text = sSchemaName;
                        lblTableName03.Text = sSchemaName;

                        sSql = "SHOW CREATE VIEW `{0}`.`{1}`;";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (!(dtTemp != null && dtTemp.Rows.Count > 0))
                        {
                            c1GridStructure.DataSource = null;
                            c1Grid100RowsTop.DataSource = null;
                            c1Grid100RowsLast.DataSource = null;
                        }
                        else
                        {
                            sScripts = dtTemp.Rows[0]["Create View"].ToString();

                            //取得指定 View 的前 100 筆資料
                            sSql = "SELECT * FROM `{0}`.`{1}`;";
                            sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                            ExecuteQuery100Rows(sSql, 0, 100);

                            tabView100RowsTop.Tag = sSchemaName;
                            tabView100RowsTop.TabVisible = true;
                        }

                        editorSQLPane.ReadOnly = false;
                        editorSQLPane.Text = sScripts.Replace(" DEFINER=", "\r\n    DEFINER=").Replace(" SQL SECURITY ", "\r\nSQL SECURITY ").Replace(" VIEW `" + sSchemaNode + "`.`" + sSchemaName + "` AS ", "\r\nVIEW `" + sSchemaNode + "`.`" + sSchemaName + "`\r\nAS\r\n");
                        editorSQLPane.ReadOnly = true;
                        break;
                    case "Functions":
                        sSql = "SHOW CREATE FUNCTION `{0}`.`{1}`;";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sScripts = dtTemp.Rows[0]["Create Function"].ToString();
                        }

                        editorSQLPane.ReadOnly = false;
                        editorSQLPane.Text = sScripts.Replace("CREATE DEFINER", "CREATE\r\n    DEFINER").Replace(" FUNCTION ", "\r\nFUNCTION ").Replace("`" + sSchemaName + "`(", "`" + sSchemaName + "`\r\n    (").Replace(" RETURNS ", "\r\n    RETURNS ");
                        editorSQLPane.ReadOnly = true;
                        break;
                    case "Triggers":
                        sSql = "SHOW CREATE TRIGGER `{0}`.`{1}`;";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sScripts = dtTemp.Rows[0]["SQL Original Statement"].ToString();
                        }

                        editorSQLPane.ReadOnly = false;
                        editorSQLPane.Text = sScripts.Replace("CREATE DEFINER", "CREATE\r\n    DEFINER").Replace(" TRIGGER ", "\r\nTRIGGER ").Replace(" BEFORE INSERT ON ", "\r\n    BEFORE INSERT ON ").Replace(" AFTER INSERT ON ", "\r\n    AFTER INSERT ON ").Replace(" BEFORE UPDATE ON ", "\r\n    BEFORE UPDATE ON ").Replace(" AFTER UPDATE ON ", "\r\n    AFTER UPDATE ON ").Replace(" BEFORE DELETE ON ", "\r\n    BEFORE DELETE ON ").Replace(" AFTER DELETE ON ", "\r\n    AFTER DELETE ON ").Replace(" FOR EACH ROW BEGIN", "\r\n    FOR EACH ROW\r\n\r\n  BEGIN").Replace(" FOR EACH ROW SET", "\r\n    FOR EACH ROW\r\nSET");
                        editorSQLPane.ReadOnly = true;
                        break;
                    case "Procedures":
                        sSql = "SHOW CREATE PROCEDURE `{0}`.`{1}`;";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sScripts = dtTemp.Rows[0]["Create Procedure"].ToString();
                        }

                        editorSQLPane.ReadOnly = false;
                        editorSQLPane.Text = sScripts.Replace("CREATE DEFINER", "CREATE\r\n    DEFINER").Replace(" PROCEDURE ", "\r\nPROCEDURE ").Replace("`" + sSchemaName + "`(", "`" + sSchemaName + "`\r\n    (");
                        editorSQLPane.ReadOnly = true;
                        break;
                }
            }
            else
            {
                if (sSchemaName.IndexOf(MyGlobal.sSeparator + "(", StringComparison.Ordinal) != -1 && sSchemaName.EndsWith(")"))
                {
                    sSchemaName = sSchemaName.Substring(0, sSchemaName.IndexOf(MyGlobal.sSeparator + "(", StringComparison.Ordinal));
                }

                tabTableStructure.Tag = sSchemaName;
                tabTableStructure.TabVisible = true;
                tabView100RowsTop.Tag = sSchemaName;
                tabView100RowsTop.TabVisible = true;
                //tabView100RowsLast.Tag = sSchemaName;
                //tabView100RowsLast.TabVisible = true;
                lblTableName1.Text = _sTableName;
                lblTableName2.Text = _sTableName;
                lblTableName3.Text = _sTableName;
                lblTableName01.Text = sSchemaName;
                lblTableName02.Text = sSchemaName;
                lblTableName03.Text = sSchemaName;

                CreateTableSchemaTable(); //DisplayInfo_MySQL

                #region 取得指定的 Table's 欄位資訊
                //Constraint資訊
                sSql = "SELECT Column_Name AS ColumnName, Constraint_Name AS ConstraintInfo FROM `information_schema`.`key_column_usage` WHERE Table_Schema = '{0}' AND Table_Name = '{1}' AND Referenced_Table_Name IS NOT NULL;";
                sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                var dtConstraint = new DataTable();
                ExecuteQueryToDataTable(sSql, ref dtConstraint);

                sSql = "SELECT Table_Schema AS SchemaName, 'Tables' AS SchemaType, Table_Name AS TableName, Column_Name AS ColumnName, Ordinal_Position AS ColumnID, SUBSTR(Is_Nullable, 1, 1) AS Nullable, Column_Default AS DefaultValue, Column_Type AS DataType, Column_Key AS ColumnKey, Column_Type AS ColumnType, Extra, Column_Comment AS Comments FROM `information_schema`.`columns` WHERE Table_Schema = '{0}' AND Table_Name = '{1}' ORDER BY Ordinal_Position;";
                sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                ExecuteQueryToDataTable(sSql, ref dtTemp);

                if (!(dtTemp != null && dtTemp.Rows.Count > 0))
                {
                    c1GridStructure.DataSource = null;
                    c1Grid100RowsTop.DataSource = null;
                    c1Grid100RowsLast.DataSource = null;
                }
                else
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        var row = dtTableSchema.NewRow();
                        row["ColumnName"] = dtTemp.Rows[iRow]["ColumnName"].ToString();
                        row["ID"] = dtTemp.Rows[iRow]["ColumnID"].ToString();
                        row["DataType"] = dtTemp.Rows[iRow]["DataType"].ToString();

                        var sConstraintInfo = dtTemp.Rows[iRow]["ColumnKey"].ToString() == "PRI" ? "primary" : "";
                        var dtRow = dtConstraint.Select("ColumnName = '" + dtTemp.Rows[iRow]["ColumnName"].ToString().Replace("'", "''") + "'");

                        if (dtRow.Length > 0 && !string.IsNullOrEmpty(dtRow[0]["ConstraintInfo"].ToString()))
                        {
                            if (string.IsNullOrEmpty(sConstraintInfo))
                            {
                                sConstraintInfo = dtRow[0]["ConstraintInfo"].ToString();
                            }
                            else
                            {
                                sConstraintInfo += ", " + dtRow[0]["ConstraintInfo"];
                            }
                        }

                        row["ConstraintInfo"] = sConstraintInfo;

                        var sTemp = dtTemp.Rows[iRow]["DefaultValue"].ToString();

                        if (Convert.IsDBNull(dtTemp.Rows[iRow]["DefaultValue"]))
                        {
                            sTemp = MyLibrary.sGridNullShowAs;
                        }

                        row["Default"] = sTemp;

                        row["Nullable"] = dtTemp.Rows[iRow]["Nullable"].ToString();
                        row["AutoInc"] = dtTemp.Rows[iRow]["Extra"].ToString() == "auto_increment" ? "Y" : "N";
                        row["Comments"] = dtTemp.Rows[iRow]["Comments"].ToString();

                        dtTableSchema.Rows.Add(row);
                    }

                    c1GridStructure.DataSource = dtTableSchema;

                    foreach (C1DisplayColumn col in c1GridStructure.Splits[0].DisplayColumns)
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

                    //取得指定 Table 的前 100 筆資料
                    sSql = "SELECT * FROM {0}{1}";
                    sSql = string.Format(sSql, string.IsNullOrEmpty(sSchemaNode) ? "" : sSchemaNode + ".", sSchemaName);
                    ExecuteQuery100Rows(sSql, 0, 100);
                }
                #endregion 取得指定的 Table's 欄位資訊

                #region 取得指定的 Table's create script
                sSql = "SHOW CREATE TABLE `{0}`.`{1}`;";
                sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                ExecuteQueryToDataTable(sSql, ref dtTemp);

                if (dtTemp == null || dtTemp.Rows.Count <= 0)
                {
                    return;
                }

                editorSQLPane.ReadOnly = false;
                editorSQLPane.Text = dtTemp.Rows[0]["Create Table"].ToString();
                editorSQLPane.ReadOnly = true;
                #endregion 取得指定的 Table's create script
            }

            editorSQLPane.Focus();
        }

        private void tmrMouseDoubleClick_Tick(object sender, EventArgs e)
        {
            tmrMouseDoubleClick.Enabled = false;

            if (!_bMouseDoubleClick)
            {
                return; //如果是 Form_Load 或是 MouseDoubleClick 事件，不處理
            }

            _bMouseDoubleClick = false;
        }

        private void c1Grid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            C1TrueDBGrid c1Grid = null;

            if (c1GridStructure.Focused)
            {
                c1Grid = c1GridStructure;
            }
            else if (c1Grid100RowsTop.Focused)
            {
                c1Grid = c1Grid100RowsTop;
            }
            else if (c1Grid100RowsLast.Focused)
            {
                c1Grid = c1Grid100RowsLast;
            }

            gMenu2 = new ContextMenuStrip();

            _sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "menugrid2", "SelectAll", "Text");
            gMenu2.Items.Add(_sLangText);

            gMenu2.Items[0].Click += delegate
            {
                SelectAll();
            };

            _sLangText = MyGlobal.GetLanguageString("Copy", "form", Name, "menugrid2", "Copy", "Text");
            gMenu2.Items.Add(_sLangText);

            gMenu2.Items[1].Click += delegate
            {
                CopyDataFromDataGrid();
            };

            gMenu2.Items.Add("-");

            _sLangText = MyGlobal.GetLanguageString("Export to File", "form", Name, "menugrid2", "ExportToFile", "Text");
            gMenu2.Items.Add(_sLangText);

            gMenu2.Items[3].Click += delegate
            {
                ExportToFile();
            };

            if (MyLibrary.bDarkMode)
            {
                gMenu2.BackColor = ColorTranslator.FromHtml("#2D2D30");
                gMenu2.ForeColor = Color.White;
                gMenu2.RenderMode = ToolStripRenderMode.System;
                //gMenu2.ShowImageMargin = false;
            }

            c1Grid.ContextMenuStrip = gMenu2;
            gMenu2.Show(c1Grid, new Point(e.X, e.Y));
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tabTableStructure.TabVisible = false;
            tabView100RowsTop.TabVisible = false;
            tabView100RowsLast.TabVisible = false;
            editorSQLPane.ReadOnly = false;
            editorSQLPane.Text = "";
            editorSQLPane.ReadOnly = true;
            c1GridSchemaBrowser.Focus();

            var myForm = new frmInfo();
            myForm.TopMost = true;

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
            myForm.StartPosition = FormStartPosition.CenterScreen;
            myForm.Show();

            ConnectToDatabase();

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    MyGlobal.UpdateSchemaData_Oracle(c1GridSchemaBrowser); //Form_Load
                    break;
                case "PostgreSQL":
                    MyGlobal.UpdateSchemaData_PostgreSQL(c1GridSchemaBrowser); //Form_Load
                    break;
                case "SQL Server":
                    MyGlobal.UpdateSchemaData_SQLServer(c1GridSchemaBrowser); //Form_Load
                    break;
                case "MySQL":
                    MyGlobal.UpdateSchemaData_MySQL(c1GridSchemaBrowser); //Form_Load
                    break;
            }

            DisconnectDatabase();
            myForm.Dispose();
        }

        private void btnWordWrap_Click(object sender, EventArgs e)
        {
            if (editorSQLPane.WrapMode == WrapMode.Word)
            {
                btnWordWrap.Visible = true;
                btnWordWrap2.Visible = false;
                editorSQLPane.WrapMode = WrapMode.None;
            }
            else
            {
                btnWordWrap.Visible = false;
                btnWordWrap2.Visible = true;
                editorSQLPane.WrapMode = WrapMode.Word;
            }

            //加上以下這個指令，取消 Word Wrap 後，Focus 才不會跑到最底部！
            editorSQLPane.ScrollCaret();
        }

        private void CheckNodeThenShowInfo(int ii)
        {
            var row = c1GridSchemaBrowser.Row + ii;

            if (_bColResize)
            {
                _bColResize = false;
                return;
            }

            if (row == -1)
            {
                return; //Exclude row headers
            }

            var vr = c1GridSchemaBrowser.Splits[0].Rows[row];

            //判斷是不是節點
            //if ((c1TrueDBGrid1.Splits[0].Rows[row].RowType == RowTypeEnum.CollapsedGroupRow) || (c1TrueDBGrid1.Splits[0].Rows[row].RowType == RowTypeEnum.ExpandedGroupRow))
            //{
            //    //lblInfo.Text = row + @", " + c1TrueDBGrid1.Columns["SchemaType"].CellValue(((GroupRow)vr).StartIndex) + @" Lv:" + ((GroupRow)vr).Level + @", " + ((GroupRow)vr).GroupedText;
            //}
            //else
            //{
            //    //lblInfo.Text = row + @", " + c1TrueDBGrid1.Columns["SchemaType"].CellValue(vr.DataRowIndex) + @", " + c1TrueDBGrid1.Columns["SchemaName"].CellValue(vr.DataRowIndex) + @", " + c1TrueDBGrid1.Columns["ColumnName"].CellValue(vr.DataRowIndex);
            //}
        }

        private void btnLeftAndRight_Click(object sender, EventArgs e)
        {
            SaveSplitterData("L/R", splitContainer1.SplitterDistance);
            editorSQLPane.Focus();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            editorSQLPane.SelectionStart = 0;
            editorSQLPane.SelectionEnd = editorSQLPane.Text.Length;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (chkCopyAsHTML.Checked)
            {
                Clipboard.SetDataObject("", false);
                editorSQLPane.Copy(CopyFormat.Text | CopyFormat.Rtf | CopyFormat.Html);
            }
            else
            {
                editorSQLPane.Copy();
            }
        }

        private void CheckNodeThenCollapsedOrExpanded()
        {
            var row = c1GridSchemaBrowser.Row;

            if (_bColResize)
            {
                _bColResize = false;
                return;
            }

            switch (c1GridSchemaBrowser.Splits[0].Rows[row].RowType)
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

        private void TransferValueToMainForm(string sValue)
        {
            var valueArgs = new ValueUpdatedEventArgs(sValue);
            ValueUpdated(this, valueArgs);
        }

        private void tmrMother2Child_Tick(object sender, EventArgs e)
        {
            var sTemp = "";

            //是否為主表單通知要自動中斷連線？
            if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp.StartsWith("autodisconnect`"))
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

            //是否為 Reload Localization 套用？
            if (string.IsNullOrEmpty(MyGlobal.sInfoFromReloadLocalization) || !MyGlobal.sInfoFromReloadLocalization.StartsWith("reloadlocalization`"))
            {
                return;
            }

            sTemp = MyGlobal.sInfoFromReloadLocalization.Replace("reloadlocalization`", "");
            sTemp = sTemp.Split(';')[0];

            if (sTemp != AccessibleDescription)
            {
                return;
            }

            MyGlobal.sInfoFromReloadLocalization = MyGlobal.sInfoFromReloadLocalization.Replace(AccessibleDescription + ";", "");

            if (MyGlobal.sInfoFromReloadLocalization == "reloadlocalization`")
            {
                MyGlobal.sInfoFromReloadLocalization = "";
            }

            ApplyLocalizationSetting(); //timerMother2Child_Tick()
        }

        private void GridVisualStyle()
        {
            var sStyle = MyLibrary.bDarkMode ? "Office 2010 Black" : MyLibrary.sGridVisualStyle;

            switch (sStyle)
            {
                case "Office 2007 Blue":
                    c1GridSchemaBrowser.VisualStyle = VisualStyle.Office2007Blue;
                    c1GridStructure.VisualStyle = VisualStyle.Office2007Blue;
                    c1Grid100RowsTop.VisualStyle = VisualStyle.Office2007Blue;
                    break;
                case "Office 2007 Silver":
                    c1GridSchemaBrowser.VisualStyle = VisualStyle.Office2007Silver;
                    c1GridStructure.VisualStyle = VisualStyle.Office2007Silver;
                    c1Grid100RowsTop.VisualStyle = VisualStyle.Office2007Silver;
                    break;
                case "Office 2007 Black":
                    c1GridSchemaBrowser.VisualStyle = VisualStyle.Office2007Black;
                    c1GridStructure.VisualStyle = VisualStyle.Office2007Black;
                    c1Grid100RowsTop.VisualStyle = VisualStyle.Office2007Black;
                    break;
                case "Office 2010 Blue":
                    c1GridSchemaBrowser.VisualStyle = VisualStyle.Office2010Blue;
                    c1GridStructure.VisualStyle = VisualStyle.Office2010Blue;
                    c1Grid100RowsTop.VisualStyle = VisualStyle.Office2010Blue;
                    break;
                case "Office 2010 Silver":
                    c1GridSchemaBrowser.VisualStyle = VisualStyle.Office2010Silver;
                    c1GridStructure.VisualStyle = VisualStyle.Office2010Silver;
                    c1Grid100RowsTop.VisualStyle = VisualStyle.Office2010Silver;
                    break;
                case "Office 2010 Black":
                    c1GridSchemaBrowser.VisualStyle = VisualStyle.Office2010Black;
                    c1GridStructure.VisualStyle = VisualStyle.Office2010Black;
                    c1Grid100RowsTop.VisualStyle = VisualStyle.Office2010Black;
                    break;
                default:
                    c1GridSchemaBrowser.VisualStyle = VisualStyle.Office2010Blue;
                    c1GridStructure.VisualStyle = VisualStyle.Office2010Blue;
                    c1Grid100RowsTop.VisualStyle = VisualStyle.Office2010Blue;
                    break;
            }
        }

        private void c1Grid_MouseClick(object sender, MouseEventArgs e)
        {
            C1TrueDBGrid c1Grid = null;

            switch (c1DockingTab1.SelectedTab.Name)
            {
                case "tabTableStructure":
                    c1Grid = c1GridStructure;
                    break;
                case "tabView100RowsTop":
                    c1Grid = c1Grid100RowsTop;
                    break;
                case "tabView100RowsLast":
                    c1Grid = c1Grid100RowsLast;
                    break;
            }

            var iRow = c1Grid.RowContaining(e.Y);
            var iCol = c1Grid.ColContaining(e.X);

            var bCornerSelected = iRow == -1 && iCol == -1;

            if (!bCornerSelected)
            {
                return;
            }

            c1Grid.SelectedRows.Clear();

            for (var i = 0; i < c1Grid.Splits[0].Rows.Count; i++)
            {
                c1Grid.SelectedRows.Add(i);
            }
        }

        private void btnGridExportToFile_Click(object sender, EventArgs e)
        {
            ExportToFile();
        }

        private void ExportToFile()
        {
            var sSheetName = "";
            C1TrueDBGrid c1Grid = null;

            switch (c1DockingTab1.SelectedTab.Name)
            {
                case "tabTableStructure":
                    sSheetName = lblTableName01.Text;
                    c1Grid = c1GridStructure;
                    break;
                case "tabView100RowsTop":
                    sSheetName = lblTableName02.Text;
                    c1Grid = c1Grid100RowsTop;
                    break;
                case "tabView100RowsLast":
                    sSheetName = lblTableName03.Text;
                    c1Grid = c1Grid100RowsLast;
                    break;
            }

            using (var myForm = new frmExportToFile())
            {
                string sHeader;
                var dtData = new DataTable();
                var dt = (DataTable)c1Grid.DataSource;

                foreach (C1DataColumn col1 in c1Grid.Columns)
                {
                    sHeader = col1.Caption;

                    switch (sHeader)
                    {
                        //case "ID":
                        //    dtData.Columns.Add(sHeader, typeof(int));
                        //    break;
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
                        rowData[sHeader] = dt.Rows[i][dt.Columns[j].ColumnName];
                    }

                    dtData.Rows.Add(rowData);
                    Application.DoEvents();
                }

                myForm.sTitle = c1DockingTab1.SelectedTab.Text;
                myForm.dtData = dtData;
                myForm.sSheetName = sSheetName;
                myForm.sFontName = c1Grid.Font.Name;
                myForm.fFontSize = c1Grid.Font.Size;
                myForm.ShowDialog();
            }
        }

        private void btnGridSelectAll_Click(object sender, EventArgs e)
        {
            C1TrueDBGrid c1Grid = null;

            switch (c1DockingTab1.SelectedTab.Name)
            {
                case "tabTableStructure":
                    c1Grid = c1GridStructure;
                    break;
                case "tabView100RowsTop":
                    c1Grid = c1Grid100RowsTop;
                    break;
                case "tabView100RowsLast":
                    c1Grid = c1Grid100RowsLast;
                    break;
            }

            c1Grid.SelectedRows.Clear();

            for (var i = 0; i < c1Grid.Splits[0].Rows.Count; i++)
            {
                c1Grid.SelectedRows.Add(i);
            }
        }

        private void btnGridCopy_Click(object sender, EventArgs e)
        {
            CopyDataFromDataGrid();
        }

        private void GridFontAndBackgroundColor()
        {
            const int fontSize = 12;

            //字型 + 字體大小
            c1GridSchemaBrowser.Font = new Font(MyLibrary.sGridFontName, fontSize, FontStyle.Regular, GraphicsUnit.Point);
            c1GridStructure.Font = new Font(MyLibrary.sGridFontName, fontSize, FontStyle.Regular, GraphicsUnit.Point);
            c1Grid100RowsTop.Font = new Font(MyLibrary.sGridFontName, fontSize, FontStyle.Regular, GraphicsUnit.Point);

            c1GridSchemaBrowser.OddRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowForeColor);
            c1GridSchemaBrowser.OddRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowBackColor);
            c1GridSchemaBrowser.EvenRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowForeColor);
            c1GridSchemaBrowser.EvenRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);
            c1GridStructure.OddRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowForeColor);
            c1GridStructure.OddRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowBackColor);
            c1GridStructure.EvenRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowForeColor);
            c1GridStructure.EvenRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);

            //Grid's 選取顏色
            c1GridSchemaBrowser.SelectedStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedForeColor);
            c1GridSchemaBrowser.SelectedStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedBackColor);
            c1GridStructure.SelectedStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedForeColor);
            c1GridStructure.SelectedStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedBackColor);
        }

        private void GridZoom()
        {
            const int fontSize = 11;
            const float pcnt = 0.9F;
            var rowHeight = c1GridSchemaBrowser.RowHeight;
            var recSelWidth = c1GridSchemaBrowser.RecordSelectorWidth;

            //adjust row height
            c1GridSchemaBrowser.RowHeight = (int)(rowHeight * pcnt) + 5;
            c1GridStructure.RowHeight = (int)(rowHeight * pcnt) + 5;
            c1Grid100RowsTop.RowHeight = (int)(rowHeight * pcnt) + 5;

            //標題列的高度
            c1GridSchemaBrowser.Splits[0].ColumnCaptionHeight = (int)(rowHeight * pcnt) + 5;
            c1GridStructure.Splits[0].ColumnCaptionHeight = (int)(rowHeight * pcnt) + 5;
            c1Grid100RowsTop.Splits[0].ColumnCaptionHeight = (int)(rowHeight * pcnt) * 2 + 3;

            //and recordselector width
            c1GridSchemaBrowser.RecordSelectorWidth = (int)(recSelWidth * pcnt);
            c1GridStructure.RecordSelectorWidth = (int)(recSelWidth * pcnt);
            c1Grid100RowsTop.RecordSelectorWidth = (int)(recSelWidth * pcnt);

            //adjust font sizes.  Normal is the root style so changing its sizes adjust all other styles
            c1GridSchemaBrowser.Styles["Normal"].Font = new Font(c1GridSchemaBrowser.Styles["Normal"].Font.FontFamily, fontSize * pcnt);
            c1GridStructure.Styles["Normal"].Font = new Font(c1GridStructure.Styles["Normal"].Font.FontFamily, fontSize * pcnt);
            c1Grid100RowsTop.Styles["Normal"].Font = new Font(c1Grid100RowsTop.Styles["Normal"].Font.FontFamily, fontSize * pcnt);
        }

        private static void DisconnectDatabase()
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
            }
        }

        private void ExecuteQueryToDataTable(string sSql, ref DataTable dt)
        {
            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    {
                        if (clsOracleReader.GetState() == ConnectionState.Closed)
                        {
                            MyGlobal.oOracleReader.ConnectTo();
                        }

                        dt = MyGlobal.oOracleReader.ExecuteQueryToDataTable(sSql);
                        break;
                    }
                case "PostgreSQL":
                    {
                        if (clsPostgreSQLReader.GetState() == ConnectionState.Closed)
                        {
                            MyGlobal.oPostgreReader.ConnectTo(MyGlobal.sDBConnectionString);
                        }

                        dt = MyGlobal.oPostgreReader.ExecuteQueryToDataTable(sSql);
                        break;
                    }
                case "SQL Server":
                    {
                        if (clsSQLServerReader.GetState() == ConnectionState.Closed)
                        {
                            MyGlobal.oSQLServerReader.ConnectTo(MyGlobal.sDBConnectionString);
                        }

                        dt = MyGlobal.oSQLServerReader.ExecuteQueryToDataTable(sSql, false);
                        break;
                    }
                case "MySQL":
                    {
                        if (clsMySQLReader.GetState() == ConnectionState.Closed)
                        {
                            MyGlobal.oMySQLReader.ConnectTo(MyGlobal.sDBConnectionString);
                        }

                        dt = MyGlobal.oMySQLReader.ExecuteQueryToDataTable(sSql);
                        break;
                    }
            }

            if (_bFormLoadFinish) //Form_Load 完成之後，每次查詢結束後才要中斷連線
            {
                DisconnectDatabase();
            }
        }

        private void ExecuteQuery100Rows(string sSql, int iStartRow, int iPageLength, bool bTop = true)
        {
            bool bPermissionDenied;

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    {
                        if (clsOracleReader.GetState() == ConnectionState.Closed)
                        {
                            MyGlobal.oOracleReader.ConnectTo();
                        }

                        dtData = new DataTable();
                        drOracle = MyGlobal.oOracleReader.ExecuteQueryPaged100Rows(sSql, iStartRow, iPageLength, out bPermissionDenied);

                        if (drOracle != null)
                        {
                            dtSchemaTable = drOracle.GetSchemaTable();
                            dtData.Load(drOracle);

                            ArrangeDataTable2(bTop ? c1Grid100RowsTop : c1Grid100RowsLast, dtData, dtSchemaTable);
                        }

                        break;
                    }
                case "PostgreSQL":
                    {
                        if (clsPostgreSQLReader.GetState() == ConnectionState.Closed)
                        {
                            MyGlobal.oPostgreReader.ConnectTo(MyGlobal.sDBConnectionString);
                        }

                        dtData = new DataTable();
                        drPostgreSQL = MyGlobal.oPostgreReader.ExecuteQueryPaged100Rows(sSql, iStartRow, iPageLength, out var bRollback, out bPermissionDenied);

                        if (bRollback)
                        {
                            drPostgreSQL = MyGlobal.oPostgreReader.ExecuteQueryPaged100Rows(sSql, iStartRow, iPageLength, out bRollback, out bPermissionDenied);
                        }

                        if (drPostgreSQL != null)
                        {
                            dtSchemaTable = drPostgreSQL.GetSchemaTable();
                            dtData.Load(drPostgreSQL);

                            if (bPermissionDenied)
                            {
                                ArrangeDataTable2(c1Grid100RowsTop, dtSchemaTable, dtSchemaTable);
                            }
                            else
                            {
                                ArrangeDataTable2(bTop ? c1Grid100RowsTop : c1Grid100RowsLast, dtData, dtSchemaTable);
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

                        dtData = new DataTable();
                        drSQLServer = MyGlobal.oSQLServerReader.ExecuteQueryPaged100Rows(sSql, iStartRow, iPageLength);

                        if (drSQLServer != null && drSQLServer.IsClosed == false)
                        {
                            dtSchemaTable = drSQLServer.GetSchemaTable();
                            dtData.Load(drSQLServer);

                            ArrangeDataTable2(bTop ? c1Grid100RowsTop : c1Grid100RowsLast, dtData, dtSchemaTable);
                        }

                        break;
                    }
                case "MySQL":
                    {
                        if (clsMySQLReader.GetState() == ConnectionState.Closed)
                        {
                            MyGlobal.oMySQLReader.ConnectTo(MyGlobal.sDBConnectionString);
                        }

                        dtData = new DataTable();
                        drMySQL = MyGlobal.oMySQLReader.ExecuteQueryPaged100Rows(sSql, iStartRow, iPageLength);

                        if (drMySQL != null)
                        {
                            dtSchemaTable = drMySQL.GetSchemaTable();
                            dtData.Load(drMySQL);

                            ArrangeDataTable2(bTop ? c1Grid100RowsTop : c1Grid100RowsLast, dtData, dtSchemaTable);
                        }

                        break;
                    }
            }

            if (_bFormLoadFinish) //Form_Load 完成之後，每次查詢結束後才要中斷連線
            {
                DisconnectDatabase();
            }
        }

        private void CopyDataFromDataGrid()
        {
            var i = 0;
            var sData = "";
            var sColumnName = "";
            var sDataType = "";
            var bActiveCell = true; //是否為「只點到某一個 cell，並沒有『選取』cell」?

            C1TrueDBGrid c1Grid = null;

            switch (c1DockingTab1.SelectedTab.Name)
            {
                case "tabTableStructure":
                    c1Grid = c1GridStructure;
                    break;
                case "tabView100RowsTop":
                    c1Grid = c1Grid100RowsTop;
                    break;
                case "tabView100RowsLast":
                    c1Grid = c1Grid100RowsLast;
                    break;
            }

            var selCol = c1Grid.SelectedCols.Count;
            var sQuotingWith = "";
            var sFieldSeparator = ",";
            var bSelectedWholeColumn = c1Grid.SelectedRows.Count == 0 && c1Grid.SelectedCols.Count > 0;

            if (bSelectedWholeColumn) //整欄選取
            {
                var sDistinct = "```";
                bActiveCell = false;

                for (var row = 0; row < c1Grid.Splits[0].Rows.Count; row++)
                {
                    foreach (var col in c1Grid.SelectedCols)
                    {
                        if (selCol > 1)
                        {
                            if (sDistinct.IndexOf("```" + col + "```", StringComparison.Ordinal) != -1)
                            {
                                continue;
                            }

                            sDistinct += col + "```";
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

                        sData += sQuotingWith + c1Grid.Columns[col.ToString()].CellText(row) + sQuotingWith + sFieldSeparator;
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
                    var vr = c1Grid.Splits[0].Rows[row];

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

                            sData += col1.CellText(vr.DataRowIndex) + sFieldSeparator;
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

                            sData += col1.CellText(vr.DataRowIndex) + sFieldSeparator;
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
                sColumnName = sColumnName.Substring(0, sColumnName.Length - sFieldSeparator.Length) + "\r\n";
            }

            if (!string.IsNullOrEmpty(sDataType))
            {
                sDataType = sDataType.Substring(0, sDataType.Length - sFieldSeparator.Length) + "\r\n";
            }

            if (bActiveCell)
            {
                sData = c1Grid[c1Grid.Splits[0].Rows[c1Grid.Row].DataRowIndex, c1Grid.Col].ToString();
            }

            CopyTextToClipboard(sColumnName + sDataType + sData);
        }

        private void CopyTextToClipboard(string sText)
        {
            try
            {
                Clipboard.Clear();
                Clipboard.SetDataObject(sText, true, 5, 200);
            }
            catch (Exception ex)
            {
                _sLangText = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        /// <summary>
         /// 如果有重覆，後面加上數字流水號，以免 SchemaTable 與 DataTable 對不起來
         /// (當 select 的欄位名稱有重覆的情況，SchemaTable 不會加數字流水號，但 DataTable)
         /// </summary>
         /// <param name="dt"></param>
         /// <returns></returns>
        private static DataTable ArrangeSchemaTable(DataTable dt)
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

        private static void ArrangeDataTable2(C1TrueDBGrid c1Grid, DataTable dtData, DataTable dtSchemaTable)
        {
            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    ArrangeDataTable_Oracle(c1Grid, dtData, dtSchemaTable);
                    break;
                case "PostgreSQL":
                    ArrangeDataTable_PostgreSQL(c1Grid, dtData, dtSchemaTable);
                    break;
                case "SQL Server":
                    ArrangeDataTable_SQLServer(c1Grid, dtData, dtSchemaTable);
                    break;
                case "MySQL":
                    ArrangeDataTable_MySQL(c1Grid, dtData, dtSchemaTable);
                    break;
            }
        }

        private void c1Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                SelectAll();
            }
        }

        private void SelectAll()
        {
            C1TrueDBGrid c1Grid = null;

            if (c1GridStructure.Focused)
            {
                c1Grid = c1GridStructure;
            }
            else if (c1Grid100RowsTop.Focused)
            {
                c1Grid = c1Grid100RowsTop;
            }
            else if (c1Grid100RowsLast.Focused)
            {
                c1Grid = c1Grid100RowsLast;
            }

            c1Grid.SelectedRows.Clear();

            for (var i = 0; i < c1Grid.Splits[0].Rows.Count; i++)
            {
                c1Grid.SelectedRows.Add(i);
            }
        }

        private void c1DockingTab1_TabClick(object sender, EventArgs e)
        {
            switch (c1DockingTab1.SelectedTab.Name)
            {
                case "tabSQLPane":
                    editorSQLPane.Focus();
                    break;
                case "tabTableStructure":
                    c1GridStructure.Focus();
                    break;
                case "tabView100RowsTop":
                    c1Grid100RowsTop.Focus();
                    break;
                case "tabView100RowsLast":
                    c1Grid100RowsLast.Focus();
                    break;
            }
        }

        private static void ArrangeDataTable_Oracle(C1TrueDBGrid c1Grid, DataTable dtData, DataTable dtSchemaTable)
        {
            string sTemp;
            var sHeader = "";
            var sDataType = "";
            string[] sArraySeparators = { "`" };
            var dtSortedData = new DataTable();
            DataRow[] dtRow;
            var sFormatDataType = "";

            dtSchemaTable = ArrangeSchemaTable(dtSchemaTable); //針對重覆的欄位名稱，後面加上流水號 (這樣才會跟 dtData 相符)

            MyGlobal.dtSchemaTable = dtSchemaTable;

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sTemp = dtData.Columns[i].DataType.ToString().Replace("System.", "");
                sFormatDataType = sTemp;

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

            var sArrayHeader = sHeader.Split(sArraySeparators, StringSplitOptions.RemoveEmptyEntries);

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

                //這裡要全部使用 string，否則一遇到 null 就會出錯，因為 null 可能會被填入 <NULL> 之類的字串
                dtSortedData.Columns.Add(sArrayHeader[i], typeof(string));
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
                                sDataTypeNew = dtRow[0]["DataType"].ToString().Replace("System.", "").ToUpper();
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
                                rowData[dtData.Columns[j].ColumnName + "\r\n" + sFormatDataType] = sTemp == MyLibrary.sGridNullShowAs ? sTemp : "(CLOB)";
                            }
                            else if ("`BLOB`".Contains("`" + sFormatDataType.ToUpper() + "`"))
                            {
                                //BLOB: 如果不是 NULL，則直接顯示 "(BLOB)"
                                rowData[dtData.Columns[j].ColumnName + "\r\n" + sFormatDataType] = (sTemp == MyLibrary.sGridNullShowAs ? sTemp : "(BLOB)");
                            }
                            else
                            {
                                rowData[dtData.Columns[j].ColumnName + "\r\n" + sFormatDataType] = sTemp;
                            }
                        }
                        else
                        {
                            rowData[dtData.Columns[j].ColumnName + "\r\n" + dtData.Columns[j].DataType.ToString().Replace("System.", "").ToUpper()] = sTemp;
                        }
                    }

                    dtSortedData.Rows.Add(rowData);
                }
            }
            catch (Exception)
            {
                //sTemp = ex.Message;
            }

            c1Grid.DataSource = dtSortedData;
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
                    MyGlobal.GetDataTypeFormat_Oracle(dtRow, out sDataType); //ArrangeDataTable_Oracle
                }

                switch (sDataType.ToLower())
                {
                    case "datetime":
                        col1.Tag = "datetime";
                        break;
                    case "int":
                    case "number":
                        col1.Tag = "number";
                        break;
                    default:
                        col1.Tag = "string";
                        break;
                }
            }

            Application.DoEvents();

            //Auto Size
            foreach (C1DisplayColumn col in c1Grid.Splits[0].DisplayColumns)
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

            if (MyLibrary.sGridNullShowAs.ToUpper() != "NONE")
            {
                var s1 = new C1.Win.C1TrueDBGrid.Style { ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridNullShowColor) };

                for (var i = 0; i < c1Grid.Columns.Count; i++)
                {
                    //套用「使用者指定的 NULL」顯示格式
                    c1Grid.Splits[0].DisplayColumns[i].AddRegexCellStyle(CellStyleFlag.AllCells, s1, MyLibrary.sGridNullShowAs);
                }
            }

            c1Grid.Refresh();
        }

        private static void ArrangeDataTable_PostgreSQL(C1TrueDBGrid c1Grid, DataTable dtData, DataTable dtSchemaTable)
        {
            string sTemp;
            var sHeader = "";
            var sDataType = "";
            string[] sArraySeparators = { "`" };
            var dtSortedData = new DataTable();
            DataRow[] dtRow;
            var sFormatDataType = "";

            dtSchemaTable = ArrangeSchemaTable(dtSchemaTable); //針對重覆的欄位名稱，後面加上流水號 (這樣才會跟 dtData 相符)

            MyGlobal.dtSchemaTable = dtSchemaTable;

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sTemp = dtData.Columns[i].DataType.ToString().Replace("System.", "");
                sFormatDataType = sTemp;

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

            var sArrayHeader = sHeader.Split(sArraySeparators, StringSplitOptions.RemoveEmptyEntries);

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

                //這裡要全部使用 string，否則一遇到 null 就會出錯，因為 null 可能會被填入 <NULL> 之類的字串
                dtSortedData.Columns.Add(sArrayHeader[i], typeof(string));
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
                                sDataTypeNew = dtRow[0]["DataType"].ToString().Replace("System.", "").ToUpper();
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
                        }

                        if (dtSchemaTable != null && dtSchemaTable.Rows.Count > 0)
                        {
                            dtRow = dtSchemaTable.Select("ColumnName = '" + dtData.Columns[j].ColumnName.Replace("'", "''") + "'");

                            if (dtRow.Length > 0)
                            {
                                sFormatDataType = MyGlobal.GetDataTypeFormat_PostgreSQL(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                            }

                            rowData[dtData.Columns[j].ColumnName + "\r\n" + sFormatDataType] = (("`CLOB`NCLOB`".Contains("`" + sFormatDataType.ToUpper() + "`")) ? "(CLOB)" : sTemp);
                        }
                        else
                        {
                            rowData[dtData.Columns[j].ColumnName + "\r\n" + dtData.Columns[j].DataType.ToString().Replace("System.", "").ToLower()] = sTemp;
                        }
                    }

                    dtSortedData.Rows.Add(rowData);
                }
            }
            catch (Exception)
            {
                //sTemp = ex.Message;
            }

            c1Grid.DataSource = dtSortedData;
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
                    MyGlobal.GetDataTypeFormat_PostgreSQL(dtRow, out sDataType); //ArrangeDataTable_PostgreSQL
                }

                switch (sDataType.ToLower())
                {
                    case "datetime":
                        col1.Tag = "datetime";
                        break;
                    case "int":
                    case "number":
                        col1.Tag = "number";
                        break;
                    default:
                        col1.Tag = "string";
                        break;
                }
            }

            Application.DoEvents();

            //Auto Size
            foreach (C1DisplayColumn col in c1Grid.Splits[0].DisplayColumns)
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

            if (MyLibrary.sGridNullShowAs.ToUpper() != "NONE")
            {
                var s1 = new C1.Win.C1TrueDBGrid.Style { ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridNullShowColor) };

                for (var i = 0; i < c1Grid.Columns.Count; i++)
                {
                    //套用「使用者指定的 NULL」顯示格式
                    c1Grid.Splits[0].DisplayColumns[i].AddRegexCellStyle(CellStyleFlag.AllCells, s1, MyLibrary.sGridNullShowAs);
                }
            }

            c1Grid.Refresh();
        }

        private static void ArrangeDataTable_SQLServer(C1TrueDBGrid c1Grid, DataTable dtData, DataTable dtSchemaTable)
        {
            string sTemp;
            var sHeader = "";
            var sDataType = "";
            string[] sArraySeparators = { "`" };
            var dtSortedData = new DataTable();
            DataRow[] dtRow;
            var sFormatDataType = "";

            dtSchemaTable = ArrangeSchemaTable(dtSchemaTable); //針對重覆的欄位名稱，後面加上流水號 (這樣才會跟 dtData 相符)

            MyGlobal.dtSchemaTable = dtSchemaTable;

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sTemp = dtData.Columns[i].DataType.ToString().Replace("System.", "");
                sFormatDataType = sTemp;

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

            var sArrayHeader = sHeader.Split(sArraySeparators, StringSplitOptions.RemoveEmptyEntries);

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

                //這裡要全部使用 string，否則一遇到 null 就會出錯，因為 null 可能會被填入 <NULL> 之類的字串
                dtSortedData.Columns.Add(sArrayHeader[i], typeof(string));
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
                                sDataTypeNew = dtRow[0]["DataTypeName"].ToString().Replace("System.", "").ToUpper();
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
                                sFormatDataType = MyGlobal.GetDataTypeFormat_SQLServer(dtRow, out sDataType); //ArrangeDataTable, Part 2：for rowData[]
                            }

                            rowData[dtData.Columns[j].ColumnName + "\r\n" + sFormatDataType] = "`CLOB`NCLOB`".Contains("`" + sFormatDataType.ToUpper() + "`") ? "(CLOB)" : sTemp;
                        }
                        else
                        {
                            rowData[dtData.Columns[j].ColumnName + "\r\n" + dtData.Columns[j].DataType.ToString().Replace("System.", "").ToLower()] = sTemp;
                        }
                    }

                    dtSortedData.Rows.Add(rowData);
                }
            }
            catch (Exception)
            {
                //sTemp = ex.Message;
            }

            c1Grid.DataSource = dtSortedData;
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
                    MyGlobal.GetDataTypeFormat_SQLServer(dtRow, out sDataType); //ArrangeDataTable_SQLServer
                }

                switch (sDataType.ToLower())
                {
                    case "datetime":
                        col1.Tag = "datetime";
                        break;
                    case "int":
                    case "number":
                        col1.Tag = "number";
                        break;
                    default:
                        col1.Tag = "string";
                        break;
                }
            }

            Application.DoEvents();

            //Auto Size
            foreach (C1DisplayColumn col in c1Grid.Splits[0].DisplayColumns)
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

            if (MyLibrary.sGridNullShowAs.ToUpper() != "NONE")
            {
                var s1 = new C1.Win.C1TrueDBGrid.Style { ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridNullShowColor) };

                for (var i = 0; i < c1Grid.Columns.Count; i++)
                {
                    //套用「使用者指定的 NULL」顯示格式
                    c1Grid.Splits[0].DisplayColumns[i].AddRegexCellStyle(CellStyleFlag.AllCells, s1, MyLibrary.sGridNullShowAs);
                }
            }

            c1Grid.Refresh();
        }

        private static void ArrangeDataTable_MySQL(C1TrueDBGrid c1Grid, DataTable dtData, DataTable dtSchemaTable)
        {
            string sTemp;
            var sHeader = "";
            var sDataType = "";
            string[] sArraySeparators = { "`" };
            var dtSortedData = new DataTable();
            DataRow[] dtRow;
            var sFormatDataType = "";

            dtSchemaTable = ArrangeSchemaTable(dtSchemaTable); //針對重覆的欄位名稱，後面加上流水號 (這樣才會跟 dtData 相符)

            MyGlobal.dtSchemaTable = dtSchemaTable;

            for (var i = 0; i < dtData.Columns.Count; i++)
            {
                Application.DoEvents();

                sTemp = dtData.Columns[i].DataType.ToString().Replace("System.", "");
                sFormatDataType = sTemp;

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

            var sArrayHeader = sHeader.Split(sArraySeparators, StringSplitOptions.RemoveEmptyEntries);

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

                //這裡要全部使用 string，否則一遇到 null 就會出錯，因為 null 可能會被填入 <NULL> 之類的字串
                dtSortedData.Columns.Add(sArrayHeader[i], typeof(string));
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
                                sDataTypeNew = dtRow[0]["DataType"].ToString().Replace("System.", "").ToUpper();
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

                            rowData[dtData.Columns[j].ColumnName + "\r\n" + sFormatDataType] = "`CLOB`NCLOB`".Contains("`" + sFormatDataType.ToUpper() + "`") ? "(CLOB)" : sTemp;
                        }
                        else
                        {
                            rowData[dtData.Columns[j].ColumnName + "\r\n" + dtData.Columns[j].DataType.ToString().Replace("System.", "").ToLower()] = sTemp;
                        }
                    }

                    dtSortedData.Rows.Add(rowData);
                }
            }
            catch (Exception)
            {
                //sTemp = ex.Message;
            }

            c1Grid.DataSource = dtSortedData;
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
                    MyGlobal.GetDataTypeFormat_MySQL(dtRow, out sDataType); //ArrangeDataTable_MySQL
                }

                switch (sDataType.ToLower())
                {
                    case "datetime":
                        col1.Tag = "datetime";
                        break;
                    case "int":
                    case "number":
                        col1.Tag = "number";
                        break;
                    default:
                        col1.Tag = "string";
                        break;
                }
            }

            Application.DoEvents();

            //Auto Size
            foreach (C1DisplayColumn col in c1Grid.Splits[0].DisplayColumns)
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

            if (MyLibrary.sGridNullShowAs.ToUpper() != "NONE")
            {
                var s1 = new C1.Win.C1TrueDBGrid.Style { ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridNullShowColor) };

                for (var i = 0; i < c1Grid.Columns.Count; i++)
                {
                    //套用「使用者指定的 NULL」顯示格式
                    c1Grid.Splits[0].DisplayColumns[i].AddRegexCellStyle(CellStyleFlag.AllCells, s1, MyLibrary.sGridNullShowAs);
                }
            }

            c1Grid.Refresh();
        }

        private void c1GridSchemaBrowser_ColResize(object sender, ColResizeEventArgs e)
        {
            if (_bColAutoResize)
            {
                return;
            }

            _bColResize = true;
        }

        private void c1GridSchemaBrowser_Expand(object sender, BandEventArgs e)
        {
            if (_bExpandCollapseAction)
            {
                return;
            }

            AutoResizeGridColumnWidth(); //c1TrueDBGrid1_Expand

            Application.DoEvents();
            Thread.Sleep(50);

            tmrMouseDoubleClick.Enabled = true;
        }

        private void c1GridSchemaBrowser_FetchRowStyle(object sender, FetchRowStyleEventArgs e)
        {
            if (_bFilterAction)
            {
                return;
            }

            try
            {
                var data = c1GridSchemaBrowser.GetDataBoundItem(e.Row);
                var name = ((DataRowView)data).Row["SchemaObject"].ToString();

                if (name == MyGlobal.sDBConnectionName)
                {
                    e.CellStyle.ForeColor = (MyLibrary.bDarkMode ? Color.Yellow : Color.Blue);
                }
            }
            catch (Exception ex)
            {
                _sLangText = MyGlobal.GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text");
                MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message, @"JasonQuery (c1TrueDBGrid_FetchRowStyle)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
                case Keys.Up:
                case Keys.Down:
                    e.Handled = true; //e.Handled = true：表示取消原本的上下鍵，不發揮作用 (不會右移或左移)，避免動作重複

                    var iDisplayRowIndex = c1GridSchemaBrowser.Row;

                    if (e.KeyCode == Keys.Down)
                        iDisplayRowIndex++;

                    if (e.KeyCode == Keys.Up)
                        iDisplayRowIndex--;

                    CheckNodeThenShowInfo(iDisplayRowIndex);

                    DisplaySchemaInfo(iDisplayRowIndex);

                    break;
            }
        }

        private void DisplaySchemaInfo(int iDisplayRowIndex)
        {
            Application.DoEvents();
            Cursor = Cursors.WaitCursor;

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    DisplayInfo_Oracle(iDisplayRowIndex); //c1TrueDBGrid1_KeyDown, case Keys.Up/Down
                    break;
                case "PostgreSQL":
                    DisplayInfo_PostgreSQL(iDisplayRowIndex); //c1TrueDBGrid1_KeyDown, case Keys.Up/Down
                    break;
                case "SQL Server":
                    DisplayInfo_SQLServer(iDisplayRowIndex); //c1TrueDBGrid1_KeyDown, case Keys.Up/Down
                    break;
                case "MySQL":
                    DisplayInfo_MySQL(iDisplayRowIndex); //c1TrueDBGrid1_KeyDown, case Keys.Up/Down
                    break;
            }

            Application.DoEvents();
            Cursor = Cursors.Default;
            c1GridSchemaBrowser.Cursor = Cursors.Default;
        }

        private void c1GridStructure_FetchRowStyle(object sender, FetchRowStyleEventArgs e)
        {
            //var dt = (DataTable)c1GridStructure.DataSource;

            var s = c1GridStructure.Columns["ConstraintInfo"].CellText(e.Row).ToString();

            if (string.IsNullOrEmpty(s))
            {
                return;
            }

            var myfont = new Font(e.CellStyle.Font, FontStyle.Bold);
            e.CellStyle.Font = myfont;
            //e.CellStyle.BackColor = ColorTranslator.FromHtml(MyGlobal.sDataGridOddRowBackColor);
        }

        private void c1GridSchemaBrowser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _bMouseDoubleClick = true;
            CheckNodeThenCollapsedOrExpanded();
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
                return; //左鍵寫在 MouseUp()；如果寫在 MouseDown() 會有問題
            }

            _cMenuSchemaBrowser = new ContextMenuStrip();
            c1GridSchemaBrowser.ContextMenuStrip = _cMenuSchemaBrowser;

            var vr = c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex];

            c1GridSchemaBrowser.Row = iDisplayRowIndex;

            var selCol = c1GridSchemaBrowser.SelectedCols.Count;
            var selRow = c1GridSchemaBrowser.SelectedRows.Count;
            string sSchemaType;
            var sSchemaType2 = "";
            var sSchemaNode = "";
            var sSchemaDbo = "";
            var sObjectID = "";
            var sTitle1 = MyGlobal.GetLanguageString("Copy to Clipboard", "form", "frmGenerateSQL", "object", "btnCopyToClipboard", "Text");
            var sTitle2 = MyGlobal.GetLanguageString("Paste to Query Editor", "form", "frmGenerateSQL", "object", "btnPasteToQueryEditor", "Text");

            if (c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex].RowType == RowTypeEnum.CollapsedGroupRow || c1GridSchemaBrowser.Splits[0].Rows[iDisplayRowIndex].RowType == RowTypeEnum.ExpandedGroupRow)
            {
                var iLevel = ((GroupRow)vr).Level;
                
                //此寫法可能有問題：原廠的 BUG，有時會取到錯誤的值
                sSchemaType = c1GridSchemaBrowser.Columns["SchemaType"].CellValue(((GroupRow)vr).StartIndex).ToString();

                //原廠給的暫時性解法
                for (var i = iDisplayRowIndex - 1; i >= 0; i--)
                {
                    //if above row is Group Row
                    if (!(c1GridSchemaBrowser.Splits[0].Rows[i] is GroupRow groupRow))
                    {
                        continue;
                    }

                    //if level is 2, meaning that this Group Row is for SchemaType
                    if (groupRow.Level != 2)
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
                    if ((iLevel > 1 && MyGlobal.sDataSource == "Oracle") || (iLevel > 2 && (MyGlobal.sDataSource == "PostgreSQL" || MyGlobal.sDataSource == "SQL Server" || MyGlobal.sDataSource == "MySQL")))
                    {
                        var sTempSchemaObject1 = dtSchema0.Select($"SchemaType = '{sSchemaType}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();
                        var sTempSchemaObject2 = dtSchema0.Select($"SchemaType = '{sSchemaType2}' And SchemaName = '" + ((GroupRow)vr).GroupedText + "'").FirstOrDefault()?["SchemaObject"].ToString();

                        if (string.IsNullOrEmpty(sTempSchemaObject1) && !string.IsNullOrEmpty(sTempSchemaObject2))
                        {
                            sSchemaType = sSchemaType2;
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
                        MyGlobal.GenerateRightMenu4CopyOnly_SchemaBrowser(_cMenuSchemaBrowser, c1GridSchemaBrowser, sTitle1, sTitle2, "USE " + sSchemaName + ";", e.X, e.Y);
                        break;
                    case "MySQL" when iLevel == 1:
                        MyGlobal.GenerateRightMenu4CopyOnly_SchemaBrowser(_cMenuSchemaBrowser, c1GridSchemaBrowser, sTitle1, sTitle2, "USE " + sSchemaName + ";", e.X, e.Y); //針對 "USE Database" 切換 DB，不出現額外幾個貼上的選項
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
                                MyGlobal.GenerateRightMenu4CopyOnly(true, _cMenuSchemaBrowser, c1GridSchemaBrowser, editorSQLPane, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }
                            else
                            {
                                MyGlobal.GenerateRightMenu4Copy_Oracle(true, c1GridSchemaBrowser, _cMenuSchemaBrowser, editorSQLPane, AccessibleDescription, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
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

                                MyGlobal.GenerateRightMenu4CopyOnly(true, _cMenuSchemaBrowser, c1GridSchemaBrowser, editorSQLPane, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }
                            else if (sSchemaType.StartsWith("Trigger"))
                            {
                                MyGlobal.GenerateRightMenu4CopyOnly(true, _cMenuSchemaBrowser, c1GridSchemaBrowser, editorSQLPane, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }
                            else
                            {
                                MyGlobal.GenerateRightMenu4Copy_PostgreSQL(true, c1GridSchemaBrowser, _cMenuSchemaBrowser, editorSQLPane, AccessibleDescription, sSchemaNode, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }

                            break;
                        }
                    case "SQL Server":
                        {
                            if (sSchemaType.StartsWith("Functions") || sSchemaType.StartsWith("Triggers") || sSchemaType.StartsWith("Indices") || sSchemaType.StartsWith("Procedures"))
                            {
                                MyGlobal.GenerateRightMenu4CopyOnly(true, _cMenuSchemaBrowser, c1GridSchemaBrowser, editorSQLPane, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }
                            else
                            {
                                MyGlobal.GenerateRightMenu4Copy_SQLServer(true, c1GridSchemaBrowser, _cMenuSchemaBrowser, editorSQLPane, AccessibleDescription, sSchemaNode, sSchemaDbo, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y, sObjectID);
                            }

                            break;
                        }
                    case "MySQL":
                        {
                            if (sSchemaType.StartsWith("Functions") || sSchemaType.StartsWith("Triggers") || sSchemaType.StartsWith("Indices") || sSchemaType.StartsWith("Procedures"))
                            {
                                MyGlobal.GenerateRightMenu4CopyOnly(true, _cMenuSchemaBrowser, c1GridSchemaBrowser, editorSQLPane, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }
                            else
                            {
                                MyGlobal.GenerateRightMenu4Copy_MySQL(true, c1GridSchemaBrowser, _cMenuSchemaBrowser, editorSQLPane, AccessibleDescription, sSchemaNode, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
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
                        break;
                    case "Oracle":
                        {
                            if (sSchemaType == "Views" || sSchemaType == "Tables")
                            {
                                bContinue = false;
                                MyGlobal.GenerateRightMenu4Copy_Oracle(true, c1GridSchemaBrowser, _cMenuSchemaBrowser, editorSQLPane, AccessibleDescription, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }

                            break;
                        }
                    case "PostgreSQL":
                        {
                            sSchemaNode = c1GridSchemaBrowser.Columns["SchemaNode"].CellValue(vr.DataRowIndex).ToString();

                            if (sSchemaType == "Views" || sSchemaType == "Tables")
                            {
                                bContinue = false;
                                MyGlobal.GenerateRightMenu4Copy_PostgreSQL(true, c1GridSchemaBrowser, _cMenuSchemaBrowser, editorSQLPane, AccessibleDescription, sSchemaNode, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
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

                                MyGlobal.GenerateRightMenu4Copy_SQLServer(true, c1GridSchemaBrowser, _cMenuSchemaBrowser, editorSQLPane, AccessibleDescription, sSchemaNode, sSchemaDbo, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y, sObjectID);
                            }

                            break;
                        }
                    case "MySQL":
                        {
                            sSchemaNode = c1GridSchemaBrowser.Columns["SchemaNode"].CellValue(vr.DataRowIndex).ToString();

                            if (sSchemaType == "Views" || sSchemaType == "Tables")
                            {
                                bContinue = false;
                                MyGlobal.GenerateRightMenu4Copy_MySQL(true, c1GridSchemaBrowser, _cMenuSchemaBrowser, editorSQLPane, AccessibleDescription, sSchemaNode, sSchemaType, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                            }

                            break;
                        }
                }

                if (bContinue)
                {
                    MyGlobal.GenerateRightMenu4CopyOnly(true, _cMenuSchemaBrowser, c1GridSchemaBrowser, editorSQLPane, sTitle1, sTitle2, sSchemaName, e.X, e.Y);
                }
            }
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                QuerySchema();
            }
        }

        private void c1GridSchemaBrowser_MouseUp(object sender, MouseEventArgs e)
        {
            var iDisplayRowIndex = c1GridSchemaBrowser.RowContaining(e.Y);

            if (iDisplayRowIndex == -1) //Exclude row headers
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            DisplaySchemaInfo(iDisplayRowIndex);
        }
    }
}