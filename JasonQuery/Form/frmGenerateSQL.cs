using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1Themes;
using C1.Win.C1TrueDBGrid;
using System.Text.RegularExpressions;
using JasonLibrary.Class;
using JasonLibrary.Stylers;
using VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle;

namespace JasonQuery
{
    public partial class frmGenerateSQL : Form
    {
        public DataTable dtColumnName { get; set; }
        public string sSchemaType { get; set; } = ""; //Tables or Views
        public string sSchemaNode { get; set; } = "";
        public string sSchemaDbo { get; set; } = "";
        public string sSqlType { get; set; } = "";
        public string sPK { get; set; } = "";
        public string sSchemaName { get; set; } = "";
        public string sSqlOfGetColumnInfo { get; set; } = "";
        public string sAccessibleDescription { get; set; } = "";
        private string _sPK = "";
        private string _sLangText;
        private DataTable _dtTable;
        private DataTable _dtView;
        private bool _bClose = false; //20220914 按第二次 btnSeletct 會觸發 Form_Close，找不出原因，故暫時用此變數解決此異常
        private bool _bKeypressComboBox = false;
        private bool _bKeyPressTab = false; //記住是否按下 Tab 鍵 (KeyUp 可以偵測 Tab，但 Tab 鍵已被提前觸發了，故不在 KeyUp 攔截)
        private bool _bKeyPressESC = false; //記住是否按下 ESC 鍵
        private bool _bKeyPressDelete = false; //記住是否按下 Delete 鍵

        //private string sTempFilename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Log_" + DateTime.Now.ToString("HHmmss") + ".txt";
        //TextEngine.WriteContentToFile(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + " editor_KeyUp, bKeyPressCtrlJ = " + bKeyPressCtrlJ.ToString() + ", _bKeyPressCtrlJ = " + _bKeyPressCtrlJ.ToString() + ", e.KeyData = " + e.KeyData.ToString() + "\r\n", sTempFilename, TextEncode.Default, System.IO.FileMode.Append);

        public frmGenerateSQL()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ApplyLocalizationSetting();
            MyGlobal.ApplyLanguageInfo(this, false, false);

            Text += " - " + MyGlobal.sDataSource;

            if (MyLibrary.bDarkMode)
            {
                C1ThemeController.ApplicationTheme = "VS2013Dark";
                c1ThemeController1.SetTheme(lblNumbers, "VS2013Dark");
            }

            MyGlobal.SetGridVisualStyle(c1GridTable, 10); //Form_Load
            c1GridTable.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);
            MyGlobal.SetGridVisualStyle(c1GridView, 10); //Form_Load
            c1GridView.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);

            txtDatabase.Location = new Point(lblDatabase.Left + lblDatabase.Width, txtDatabase.Top);
            cboTable.Location = new Point(rdoTable.Left + rdoTable.Width, cboTable.Top);
            c1GridTable.Location = new Point(cboTable.Left, c1GridTable.Top);
            cboView.Location = new Point(rdoView.Left + rdoView.Width, cboView.Top);
            c1GridView.Location = new Point(cboView.Left, c1GridView.Top);
            txtAliasName.Location = new Point(chkAliasName.Left + chkAliasName.Width, txtAliasName.Top);
            cboNumbers.Location = new Point(lblNumbers.Left + lblNumbers.Width, cboNumbers.Top);
            cboSchema.Location = new Point(lblSchema.Left + lblSchema.Width, cboSchema.Top);
            txtDatabase.Text = string.IsNullOrEmpty(MyGlobal.sDBConnectionDatabase) ? "" : MyGlobal.sDBConnectionDatabase;
            txtDatabase.Enabled = false;

            var bValue = false;
            var dtSchema = new DataTable();

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    lblDatabase.Visible = false;
                    txtDatabase.Visible = false;
                    rdoTable.Location = new Point(rdoTable.Left - 306, rdoTable.Top);
                    cboTable.Location = new Point(cboTable.Left - 306, cboTable.Top);
                    c1GridTable.Location = new Point(cboTable.Left, c1GridTable.Top);
                    rdoView.Location = new Point(rdoView.Left - 306, rdoView.Top);
                    cboView.Location = new Point(cboView.Left - 306, cboView.Top);
                    c1GridView.Location = new Point(cboView.Left, c1GridView.Top);
                    btnSelect.Location = new Point(btnSelect.Left - 306, btnSelect.Top);

                    MyGlobal.GetTableInfo_Oracle(sSchemaNode, out _dtTable);
                    MyGlobal.GetViewInfo_Oracle(sSchemaNode, out _dtView);

                    break;
                case "PostgreSQL":
                    MyGlobal.GetSchemaInfo_PostgreSQL(out dtSchema);
                    MyGlobal.SetC1ComboBoxItemsFromDataTable(cboSchema, dtSchema);
                    lblSchema.Visible = true;
                    cboSchema.Visible = true;
                    cboSchema.Text = sSchemaNode;

                    if (!string.IsNullOrEmpty(sAccessibleDescription))
                    {
                        MyGlobal.GetTableInfo_PostgreSQL(out _dtTable, cboSchema.Text);
                        MyGlobal.GetViewInfo_PostgreSQL(out _dtView, cboSchema.Text);
                    }

                    break;
                case "SQL Server":
                    bValue = true;

                    if (string.IsNullOrEmpty(MyGlobal.sDBConnectionDatabase) && string.IsNullOrEmpty(sSchemaName))
                    {
                        return;
                    }

                    if (string.IsNullOrEmpty(sSchemaNode))
                    {
                        sSchemaNode = txtDatabase.Text;
                    }

                    if (!string.IsNullOrEmpty(sSchemaNode))
                    {
                        MyGlobal.GetTableInfo_SQLServer(sSchemaNode, out _dtTable);
                        MyGlobal.GetViewInfo_SQLServer(sSchemaNode, out _dtView);
                    }

                    break;
                case "MySQL":
                    bValue = true;

                    sSchemaNode = string.IsNullOrEmpty(sSchemaNode) ? txtDatabase.Text : sSchemaNode;

                    MyGlobal.GetTableInfo_MySQL(sSchemaNode, out _dtTable);
                    MyGlobal.GetViewInfo_MySQL(sSchemaNode, out _dtView);

                    break;

                case "SQLite":
                case "SQLCipher":

                    break;
            }

            MyGlobal.SetC1ComboBoxItemsFromDataTable(cboTable, _dtTable);
            MyGlobal.SetC1ComboBoxItemsFromDataTable(cboView, _dtView);

            txtDatabase.Enabled = bValue;

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                case "PostgreSQL":
                    chkEncloseGraveAccent.Visible = false;
                    chkEncloseBrackets.Visible = true;
                    chkEncloseBrackets.Enabled = false;
                    break;
                case "SQL Server":
                    chkEncloseGraveAccent.Visible = false;
                    chkEncloseBrackets.Visible = true;
                    break;
                case "MySQL":
                    chkEncloseGraveAccent.Visible = true;
                    chkEncloseBrackets.Visible = false;
                    break;
            }

            txtDatabase.ReadOnly = true;

            if (string.IsNullOrEmpty(sAccessibleDescription))
            {
                btnPreview.Enabled = false;
                btnPasteToQueryEditor.Enabled = false;
                return;
            }
            else
            {
                if (sSchemaType == "Views")
                {
                    rdoView.Checked = true;
                    cboView.Text = sSchemaName;
                }
                else
                {
                    rdoTable.Checked = true;
                    cboTable.Text = sSchemaName;
                }
            }

            Initial();
        }

        private void Initial()
        {
            if (string.IsNullOrEmpty(sPK))
            {
                chkPKInfo.Checked = false;
                chkPKInfo.Enabled = false;
            }
            else
            {
                chkPKInfo.Checked = true;
                chkPKInfo.Enabled = true;
            }

            cboNumbers.Text = "5";
            c1Grid.DataSource = CreateColumnTable(dtColumnName, "", "1"); //不能直接指定 dtColumnName，會造成 CheckBox 唯讀！

            MyGlobal.ReplaceColumnNameByLanguageInfo(c1Grid, Name, true); //Form_Load
            SetCheckBox(" ");

            if (sSchemaType == "Views")
            {
                rdoDelete.Enabled = false;
                rdoInsert.Enabled = false;
                rdoUpdate.Enabled = false;
                rdoAlter.Enabled = false;
                rdoCreate.Enabled = false;
                rdoDrop.Enabled = false;
                rdoRename.Enabled = false;
                rdoTruncate.Enabled = false;
                rdoView.Checked = true;
            }
            else
            {
                rdoDelete.Enabled = true;
                rdoInsert.Enabled = true;
                rdoUpdate.Enabled = true;
                rdoAlter.Enabled = true;
                rdoCreate.Enabled = true;
                rdoDrop.Enabled = true;
                rdoRename.Enabled = true;
                rdoTruncate.Enabled = true;
                rdoTable.Checked = true;
            }

            rdoSelectStar.Checked = false;
            rdoSelectStar.Checked = true;
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_bClose == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_bClose == false)
            {
                return;
            }

            DisconnectDatabase();
        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            MyGlobal.UpdateSetting("GlobalConfig", "GenerateSQLFormWidth", Size.Width.ToString());
            MyGlobal.UpdateSetting("GlobalConfig", "GenerateSQLFormHeight", Size.Height.ToString());
        }

        private void SetCheckBox(string sColumn)
        {
            var items = c1Grid.Columns[sColumn].ValueItems;
            items.Translate = true;
            items.Presentation = PresentationEnum.CheckBox;

            items.Values.Clear();
            items.Values.Add(new ValueItem("0", false)); //unchecked
            items.Values.Add(new ValueItem("1", true));  //checked

            //指定哪一個 Column 要套用 FetchCellStyle
            c1Grid.Splits[0].DisplayColumns[1].FetchStyle = true;
            c1Grid.Splits[0].DisplayColumns[2].FetchStyle = true;
            c1Grid.Splits[0].DisplayColumns[3].FetchStyle = true;
            c1Grid.Splits[0].DisplayColumns[4].FetchStyle = true;
            c1Grid.Splits[0].DisplayColumns[5].FetchStyle = true;
            c1Grid.Splits[0].DisplayColumns[6].FetchStyle = true;
            c1Grid.Splits[0].DisplayColumns[7].FetchStyle = true;

            c1Grid.Splits[0].DisplayColumns[" "].Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center;

            foreach (C1DisplayColumn col in c1Grid.Splits[0].DisplayColumns)
            {
                col.AutoSize();

                if (col.Name == " ")
                {
                    col.AllowSizing = false;
                    col.Locked = false;
                }
            }

            c1Grid.Splits[0].DisplayColumns["TypeName"].Visible = false;
            c1Grid.Splits[0].DisplayColumns["DataTypeName"].Visible = false;
        }

        private void ApplyLocalizationSetting()
        {
            if (MyLibrary.bDarkMode)
            {
                C1ThemeController.ApplicationTheme = "VS2013Dark";
            }

            GridVisualStyle(); //ApplyLocalizationSetting
            GridFontAndBackgroundColor(); //ApplyLocalizationSetting
            GridZoom(); //ApplyLocalizationSetting

            MyGlobal.SetGridVisualStyle(c1Grid, 11); //ApplyLocalizationSetting
            ApplyEditorSetting(); //ApplyLocalizationSetting
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _bClose = true;
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData != Keys.Escape) return false;

            _bClose = true;
            Close();

            return true;
        }

        private void GridVisualStyle()
        {
            var sStyle = MyLibrary.bDarkMode ? "Office 2010 Black" : MyLibrary.sGridVisualStyle;

            switch (sStyle)
            {
                case "Office 2007 Blue":
                    c1Grid.VisualStyle = VisualStyle.Office2007Blue;
                    break;
                case "Office 2007 Silver":
                    c1Grid.VisualStyle = VisualStyle.Office2007Silver;
                    break;
                case "Office 2007 Black":
                    c1Grid.VisualStyle = VisualStyle.Office2007Black;
                    break;
                case "Office 2010 Blue":
                    c1Grid.VisualStyle = VisualStyle.Office2010Blue;
                    break;
                case "Office 2010 Silver":
                    c1Grid.VisualStyle = VisualStyle.Office2010Silver;
                    break;
                case "Office 2010 Black":
                    c1Grid.VisualStyle = VisualStyle.Office2010Black;
                    break;
                default:
                    c1Grid.VisualStyle = VisualStyle.Office2010Blue;
                    break;
            }
        }

        private void GridFontAndBackgroundColor()
        {
            const int fontSize = 10;

            c1Grid.Font = new Font(MyLibrary.sGridFontName, fontSize, FontStyle.Regular, GraphicsUnit.Point);

            c1Grid.OddRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowForeColor);
            c1Grid.OddRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowBackColor);
            c1Grid.EvenRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowForeColor);
            c1Grid.EvenRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);
            c1Grid.SelectedStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedForeColor);
            c1Grid.SelectedStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedBackColor);
        }

        private void GridZoom()
        {
            const int fontSize = 10;
            const float pcnt = 0.9F;
            var rowHeight = c1Grid.RowHeight;
            var recSelWidth = c1Grid.RecordSelectorWidth;

            c1Grid.RowHeight = (int)(rowHeight * pcnt) + 5;
            c1Grid.Splits[0].ColumnCaptionHeight = (int)(rowHeight * pcnt) + 5;
            c1Grid.Styles["Normal"].Font = new Font(c1Grid.Styles["Normal"].Font.FontFamily, fontSize * pcnt);
        }

        private void c1Grid_FetchCellStyle(object sender, FetchCellStyleEventArgs e)
        {
            if (e.Col > 0) //除了 CheckBox，其餘皆 Lock
            {
                e.CellStyle.Locked = true;
            }
        }

        private void ApplyEditorSetting()
        {
            editor.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
            editor.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editor.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));

            editor.Zoom = Convert.ToInt16(MyLibrary.sQueryEditorZoom);

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

            editor.Styler = new SqlStyler(); //sqlstyler()：變更關鍵字、顏色
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            editor.Text = "";

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    GenerateSQL_Oracle();
                    break;
                case "PostgreSQL":
                    GenerateSQL_PostgreSQL();
                    break;
                case "SQL Server":
                    GenerateSQL_SQLServer();
                    break;
                case "MySQL":
                    GenerateSQL_MySQL();
                    break;
            }
        }

        private void GenerateSQL_Oracle()
        {
            var sSchema = "";
            var sDataType = "";
            var sColumnInfo = "";
            var j = 0;
            var sColumnValue = "";
            var sSelectedColumnName = "";
            var bPK = true;
            var sText = "";
            var sAliasName = "";
            var bToLower = rdoLowerKeywords.Checked;

            Cursor = Cursors.WaitCursor;

            if (txtAliasName.Enabled && !string.IsNullOrEmpty(txtAliasName.Text))
            {
                if (rdoUpperAll.Checked)
                {
                    sAliasName = txtAliasName.Text.ToUpper();
                }
                else if (rdoLowerAll.Checked)
                {
                    sAliasName = txtAliasName.Text.ToLower();
                }
                else
                {
                    sAliasName = txtAliasName.Text;
                }
            }

            try
            {
                switch (grpFunction.AccessibleDescription?.ToString())
                {
                    case "rdoAlter":
                        sText = "--add a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("ADD ", bToLower) + "New_Column_Name " + MyGlobal.TransferWordCase("VARCHAR2", bToLower) + "(50);\r\n\r\n" +
                                "--add multiple columns\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("ADD ", bToLower) + "(New_Column_Name1 " + MyGlobal.TransferWordCase("INT", bToLower) + ",\r\n       New_Column_Name2 " + MyGlobal.TransferWordCase("VARCHAR2", bToLower) + "(100));\r\n\r\n" +
                                "--modify a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("MODIFY ", bToLower) + "Old_Column_Name " + MyGlobal.TransferWordCase("INT", bToLower) + ";\r\n\r\n" +
                                "--modify multiple columns\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("MODIFY ", bToLower) + "(Old_Column_Name1 " + MyGlobal.TransferWordCase("VARCHAR2", bToLower) + "(100) " + MyGlobal.TransferWordCase("NOT NULL", bToLower) + ",\r\n          Old_Column_Name2 " + MyGlobal.TransferWordCase("VARCHAR2", bToLower) + "(75));";
                        break;
                    case "rdoCreate":
                        sText = MyGlobal.GetCreateScript_Oracle(sSchemaType, sSchemaName);
                        break;
                    case "rdoDrop":
                        sText = "--drop a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("DROP COLUMN ", bToLower) + "Old_Column_Name;\r\n\r\n" +
                                "--drop a table\r\n" + MyGlobal.TransferWordCase("DROP TABLE ", bToLower) + sSchemaName + ";";
                        break;
                    case "rdoRename":
                        sText = "--rename a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("RENAME COLUMN ", bToLower) + "Old_Column_Name " + MyGlobal.TransferWordCase("TO ", bToLower) + "New_Column_Name;\r\n\r\n" +
                                "--rename a table\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("RENAME TO ", bToLower) + "New_Table_Name;";
                        break;
                    case "rdoTruncate":
                        sText = MyGlobal.TransferWordCase("TRUNCATE TABLE ", bToLower) + sSchemaName + ";";
                        break;
                    case "rdoSelectStar":
                    case "rdoSelect":
                    case "rdoInsert":
                    case "rdoDelete":
                    case "rdoUpdate":
                        {
                            if (bPK)
                            {
                                _sPK = "\r\n--Primary Key" + sPK;
                            }

                            for (var i = 0; i < c1Grid.RowCount; i++)
                            {
                                Application.DoEvents();

                                if (chkPKInfo.Checked && bPK && sPK.Contains("`" + c1Grid[i, @"Column_Name"] + "`"))
                                {
                                    var dtRow = dtColumnName.Select("Column_Name = '" + c1Grid[i, @"Column_Name"].ToString().Replace("'", "''") + "'");
                                    sSchema = ", " + MyGlobal.GetDataTypeFormat_Oracle(dtRow, out sDataType); //GenerateSQL_Oracle
                                    _sPK = _sPK.Replace("`" + c1Grid[i, @"Column_Name"] + "`", "`" + (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + c1Grid[i, @"Column_Name"] + sSchema + "`");
                                }

                                if (c1Grid[i, " "].ToString() != "1")
                                {
                                    continue;
                                }

                                var dtRow0 = dtColumnName.Select("Column_Name = '" + c1Grid[i, @"Column_Name"].ToString().Replace("'", "''") + "'");

                                if (dtRow0.Length > 0)
                                {
                                    sSchema = " --" + MyGlobal.GetDataTypeFormat_Oracle(dtRow0, out sDataType); //GenerateSQL_Oracle
                                    sSchema = chkColumnTypeInfo.Checked ? sSchema : "";
                                }

                                string sTemp;

                                switch (sDataType.ToLower())
                                {
                                    case "int":
                                    case "number":
                                    case "long":
                                        sTemp = "0";
                                        break;
                                    case "datetime":
                                        sTemp = MyGlobal.TransferWordCase("TO_DATE(SYSDATE, '", bToLower) + MyLibrary.sDateFormat.ToLower() + " hh24:mi:ss')";
                                        break;
                                    default:
                                        sTemp = "''";
                                        break;
                                }

                                sTemp = (grpFunction.AccessibleDescription == "rdoInsert" && chkDisplayAsParameter.Checked) ? ":" + c1Grid[i, @"Column_Name"] : sTemp;

                                if (j < Convert.ToInt16(cboNumbers.Text == @"All" ? "9999" : cboNumbers.Text) - 1)
                                {
                                    j++;
                                    sSelectedColumnName += (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + c1Grid[i, @"Column_Name"] + ", ";
                                    sColumnValue += sTemp + (i == c1Grid.RowCount - 1 ? "," : ", ");
                                }
                                else
                                {
                                    j = 0;
                                    sSelectedColumnName += (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + c1Grid[i, @"Column_Name"] + ", \r\n       " + (rdoInsert.Checked ? " " : "");
                                    sColumnValue += sTemp + (i == c1Grid.RowCount - 1 ? "," : ", \r\n        ");
                                }

                                sColumnInfo = sColumnInfo + (i == 0 ? "" : "   " + MyGlobal.sSeparator3 + " ") + c1Grid[i, @"Column_Name"] + " = " + sTemp + (i == c1Grid.RowCount - 1 ? "" : MyGlobal.sSeparator) + sSchema + "\r\n";
                            }

                            if (bPK)
                            {
                                _sPK = _sPK.Replace("`", "\r\n--");
                                _sPK = _sPK.Substring(0, _sPK.Length - 4);
                            }

                            sColumnInfo = sColumnInfo.Trim();
                            sColumnValue = string.IsNullOrEmpty(sSelectedColumnName) ? "" : sColumnValue.Trim().Substring(0, sColumnValue.Trim().Length - 1);

                            if (rdoDelete.Checked)
                            {
                                sText = MyGlobal.TransferWordCase("DELETE FROM ", bToLower) + sSchemaName + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bToLower) + (string.IsNullOrEmpty(sSelectedColumnName) ? "/*Your Conditions*/;" : sColumnInfo.Replace(MyGlobal.sSeparator3, MyGlobal.TransferWordCase("AND", bToLower)).Replace(MyGlobal.sSeparator, "") + (chkPKInfo.Checked ? _sPK : ""));
                            }
                            else if (rdoSelect.Checked)
                            {
                                sText = string.IsNullOrEmpty(sSelectedColumnName) ? "" : MyGlobal.TransferWordCase("SELECT ", bToLower) + sSelectedColumnName.Trim().Substring(0, sSelectedColumnName.Trim().Length - 1) + "\r\n  " + MyGlobal.TransferWordCase("FROM ", bToLower) + sSchemaName + (string.IsNullOrEmpty(sAliasName) ? "" : " " + sAliasName) + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bToLower) + "/*Your Conditions*/" + (chkLimitInfo.Checked ? MyGlobal.TransferWordCase("\r\n   AND ROWNUM <= 10", bToLower) : "") + ";" + (chkPKInfo.Checked ? _sPK : "");
                            }
                            else if (rdoInsert.Checked)
                            {
                                sText = string.IsNullOrEmpty(sSelectedColumnName) ? "" : MyGlobal.TransferWordCase("INSERT INTO ", bToLower) + sSchemaName + "\r\n       (" + sSelectedColumnName.Trim().Substring(0, sSelectedColumnName.Trim().Length - 1) + ")\r\n" + MyGlobal.TransferWordCase("VALUES (", bToLower) + sColumnValue + ");" + (chkPKInfo.Checked ? _sPK : "");

                                if (!string.IsNullOrEmpty(sAliasName))
                                {
                                    sText = sText.Replace((sAliasName + "."), "");
                                }
                            }
                            else if (rdoUpdate.Checked)
                            {
                                sText = MyGlobal.TransferWordCase("UPDATE ", bToLower) + sSchemaName + "\r\n   " + MyGlobal.TransferWordCase("SET ", bToLower) + sColumnInfo.Replace(MyGlobal.sSeparator3, "   ").Replace(MyGlobal.sSeparator, ",") + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bToLower) + "/*Your Conditions*/;" + (chkPKInfo.Checked ? _sPK : "");
                            }
                            else if (rdoSelectStar.Checked)
                            {
                                sText = MyGlobal.TransferWordCase("SELECT " + (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + "* FROM ", bToLower) + sSchemaName + (string.IsNullOrEmpty(sAliasName) ? "" : " " + sAliasName) + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bToLower) + "/*Your Conditions*/" + (chkLimitInfo.Checked ? MyGlobal.TransferWordCase("\r\n   AND ROWNUM <= 10", bToLower) : "") + ";" + (chkPKInfo.Checked ? _sPK : "");
                            }
                        }

                        break;
                }

                if (rdoUpperAll.Checked)
                {
                    sText = sText.ToUpper().Replace(" \r\n", "\r\n");
                }
                else if (rdoLowerAll.Checked)
                {
                    sText = sText.ToLower().Replace(" \r\n", "\r\n");
                }

                UpdateEditorText(sText);
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void GenerateSQL_PostgreSQL()
        {
            var sSchema = "";
            var sColumnInfo = "";
            var j = 0;
            var sColumnValue = "";
            var sSelectedColumnName = "";
            var bPK = true;
            var sText = "";
            var sAliasName = "";
            var bKeywordsToLower = rdoLowerKeywords.Checked;

            Cursor = Cursors.WaitCursor;

            if (txtAliasName.Enabled && !string.IsNullOrEmpty(txtAliasName.Text))
            {
                if (rdoUpperAll.Checked)
                {
                    sAliasName = txtAliasName.Text.ToUpper();
                }
                else if (rdoLowerAll.Checked)
                {
                    sAliasName = txtAliasName.Text.ToLower();
                }
                else
                {
                    sAliasName = txtAliasName.Text;
                }
            }

            try
            {
                switch (grpFunction.AccessibleDescription?.ToString())
                {
                    case "rdoAlter":
                        sText = "--add a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bKeywordsToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("ADD ", bKeywordsToLower) + "New_Column_Name " + MyGlobal.TransferWordCase("VARCHAR2", bKeywordsToLower) + "(50);\r\n\r\n" +
                                "--add multiple columns\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bKeywordsToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("ADD ", bKeywordsToLower) + "(New_Column_Name1 " + MyGlobal.TransferWordCase("INT", bKeywordsToLower) + ",\r\n       New_Column_Name2 " + MyGlobal.TransferWordCase("VARCHAR2", bKeywordsToLower) + "(100));\r\n\r\n" +
                                "--modify a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bKeywordsToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("MODIFY ", bKeywordsToLower) + "Old_Column_Name " + MyGlobal.TransferWordCase("INT", bKeywordsToLower) + ";\r\n\r\n" +
                                "--modify multiple columns\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bKeywordsToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("MODIFY ", bKeywordsToLower) + "(Old_Column_Name1 " + MyGlobal.TransferWordCase("VARCHAR2", bKeywordsToLower) + "(100) " + MyGlobal.TransferWordCase("NOT NULL", bKeywordsToLower) + ",\r\n          Old_Column_Name2 " + MyGlobal.TransferWordCase("VARCHAR2", bKeywordsToLower) + "(75));";
                        break;
                    case "rdoCreate":
                        var sValue = MyGlobal.GetCreateScript_PostgreSQL(sSchemaNode, sSchemaType, sSchemaName);
                        sText = sValue[0];
                        break;
                    case "rdoDrop":
                        sText = "--drop a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bKeywordsToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("DROP COLUMN ", bKeywordsToLower) + "Old_Column_Name;\r\n\r\n" +
                                "--drop a table\r\n" + MyGlobal.TransferWordCase("DROP TABLE ", bKeywordsToLower) + sSchemaName + ";";
                        break;
                    case "rdoRename":
                        sText = "--rename a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bKeywordsToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("RENAME COLUMN ", bKeywordsToLower) + "Old_Column_Name " + MyGlobal.TransferWordCase("TO ", bKeywordsToLower) + "New_Column_Name;\r\n\r\n" +
                                "--rename a table\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bKeywordsToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("RENAME TO ", bKeywordsToLower) + "New_Table_Name;";
                        break;
                    case "rdoTruncate":
                        sText = MyGlobal.TransferWordCase("TRUNCATE TABLE ", bKeywordsToLower) + sSchemaName + ";";
                        break;
                    case "rdoSelectStar":
                    case "rdoSelect":
                    case "rdoInsert":
                    case "rdoDelete":
                    case "rdoUpdate":
                        {
                            if (bPK)
                            {
                                _sPK = "\r\n--Primary Key" + sPK;
                            }

                            for (var i = 0; i < c1Grid.RowCount; i++)
                            {
                                Application.DoEvents();

                                if (chkPKInfo.Checked && bPK && sPK.Contains("`" + c1Grid[i, @"Column_Name"] + "`"))
                                {
                                    var dtRow = dtColumnName.Select("Column_Name = '" + c1Grid[i, @"Column_Name"].ToString().Replace("'", "''") + "'");
                                    sSchema = ", " + c1Grid[i, @"TypeName"];
                                    _sPK = _sPK.Replace("`" + c1Grid[i, "Column_Name"] + "`", "`" + c1Grid[i, @"Column_Name"] + sSchema + "`");
                                }

                                if (c1Grid[i, " "].ToString() != "1")
                                {
                                    continue;
                                }

                                var dtRow0 = dtColumnName.Select("Column_Name = '" + c1Grid[i, @"Column_Name"].ToString().Replace("'", "''") + "'");

                                if (dtRow0.Length > 0)
                                {
                                    sSchema = " --" + c1Grid[i, @"TypeName"];
                                    sSchema = chkColumnTypeInfo.Checked ? sSchema : "";
                                }

                                var sDataType = c1Grid[i, @"TypeName"].ToString().ToLower();
                                string sTemp;

                                switch (sDataType)
                                {
                                    case "integer":
                                    case "numeric":
                                    case "bigint":
                                    case "bit":
                                    case "double precision":
                                    case "smallint":
                                        sTemp = "0";
                                        break;
                                    case "datetime":
                                        sTemp = MyGlobal.TransferWordCase("TO_DATE(SYSDATE, '", bKeywordsToLower) + MyLibrary.sDateFormat.ToLower() + " hh24:mi:ss')";
                                        break;
                                    default:
                                        sTemp = "''";
                                        break;
                                }

                                sTemp = (grpFunction.AccessibleDescription == "rdoInsert" && chkDisplayAsParameter.Checked) ? ":" + c1Grid[i, @"Column_Name"] : sTemp;

                                if (j < Convert.ToInt16(cboNumbers.Text == @"All" ? "9999" : cboNumbers.Text) - 1)
                                {
                                    j++;
                                    sSelectedColumnName += (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + c1Grid[i, @"Column_Name"] + ", ";
                                    sColumnValue += sTemp + (i == c1Grid.RowCount - 1 ? "," : ", ");
                                }
                                else
                                {
                                    j = 0;
                                    sSelectedColumnName += (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + c1Grid[i, @"Column_Name"] + ", \r\n       " + (rdoInsert.Checked ? " " : "");
                                    sColumnValue += sTemp + (i == c1Grid.RowCount - 1 ? "," : ", \r\n        ");
                                }

                                sColumnInfo = sColumnInfo + (i == 0 ? "" : "   " + MyGlobal.sSeparator3 + " ") + c1Grid[i, @"Column_Name"] + " = " + sTemp + (i == c1Grid.RowCount - 1 ? "" : MyGlobal.sSeparator) + sSchema + "\r\n";
                            }

                            if (bPK)
                            {
                                _sPK = _sPK.Replace("`", "\r\n--");
                                _sPK = _sPK.Substring(0, _sPK.Length - 4);
                            }

                            sColumnInfo = sColumnInfo.Trim();
                            sColumnValue = string.IsNullOrEmpty(sSelectedColumnName) ? "" : sColumnValue.Trim().Substring(0, sColumnValue.Trim().Length - 1);

                            if (rdoDelete.Checked)
                            {
                                sText = MyGlobal.TransferWordCase("DELETE FROM ", bKeywordsToLower) + sSchemaName + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bKeywordsToLower) + (string.IsNullOrEmpty(sSelectedColumnName) ? "/*Your Conditions*/;" : sColumnInfo.Replace(MyGlobal.sSeparator3, MyGlobal.TransferWordCase("AND", bKeywordsToLower)).Replace(MyGlobal.sSeparator, "") + (chkPKInfo.Checked ? _sPK : ""));
                            }
                            else if (rdoSelect.Checked)
                            {
                                sText = string.IsNullOrEmpty(sSelectedColumnName) ? "" : MyGlobal.TransferWordCase("SELECT ", bKeywordsToLower) + sSelectedColumnName.Trim().Substring(0, sSelectedColumnName.Trim().Length - 1) + "\r\n  " + MyGlobal.TransferWordCase("FROM ", bKeywordsToLower) + sSchemaName + (string.IsNullOrEmpty(sAliasName) ? "" : " " + sAliasName) + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bKeywordsToLower) + "/*Your Conditions*/" + (chkLimitInfo.Checked ? MyGlobal.TransferWordCase(" LIMIT 10", bKeywordsToLower) : "") + ";" + (chkPKInfo.Checked ? _sPK : "");
                            }
                            else if (rdoInsert.Checked)
                            {
                                sText = string.IsNullOrEmpty(sSelectedColumnName) ? "" : MyGlobal.TransferWordCase("INSERT INTO ", bKeywordsToLower) + sSchemaName + "\r\n       (" + sSelectedColumnName.Trim().Substring(0, sSelectedColumnName.Trim().Length - 1) + ")\r\n" + MyGlobal.TransferWordCase("VALUES (", bKeywordsToLower) + sColumnValue + ");" + (chkPKInfo.Checked ? _sPK : "");

                                if (!string.IsNullOrEmpty(sAliasName))
                                {
                                    sText = sText.Replace((sAliasName + "."), "");
                                }
                            }
                            else if (rdoUpdate.Checked)
                            {
                                sText = MyGlobal.TransferWordCase("UPDATE ", bKeywordsToLower) + sSchemaName + "\r\n   " + MyGlobal.TransferWordCase("SET ", bKeywordsToLower) + sColumnInfo.Replace(MyGlobal.sSeparator3, "   ").Replace(MyGlobal.sSeparator, ",") + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bKeywordsToLower) + "/*Your Conditions*/;" + (chkPKInfo.Checked ? _sPK : "");
                            }
                            else if (rdoSelectStar.Checked)
                            {
                                sText = MyGlobal.TransferWordCase("SELECT " + (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + "* FROM ", bKeywordsToLower) + sSchemaName + (string.IsNullOrEmpty(sAliasName) ? "" : " " + sAliasName) + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bKeywordsToLower) + "/*Your Conditions*/" + (chkLimitInfo.Checked ? MyGlobal.TransferWordCase(" LIMIT 10", bKeywordsToLower) : "") + ";" + (chkPKInfo.Checked ? _sPK : "");
                            }
                        }

                        break;
                }

                if (rdoUpperAll.Checked)
                {
                    sText = sText.ToUpper().Replace(" \r\n", "\r\n");
                }
                else if (rdoLowerAll.Checked)
                {
                    sText = sText.ToLower().Replace(" \r\n", "\r\n");
                }

                UpdateEditorText(sText);
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void GenerateSQL_SQLServer()
        {
            var sSchema = "";
            var sDataType = "";
            var sColumnInfo = "";
            var j = 0;
            var sColumnValue = "";
            var sSelectedColumnName = "";
            var bPK = true;
            var sText = "";
            var sAliasName = "";
            var bToLower = rdoLowerKeywords.Checked;

            Cursor = Cursors.WaitCursor;

            if (txtAliasName.Enabled && !string.IsNullOrEmpty(txtAliasName.Text))
            {
                if (rdoUpperAll.Checked)
                {
                    sAliasName = txtAliasName.Text.ToUpper();
                }
                else if (rdoLowerAll.Checked)
                {
                    sAliasName = txtAliasName.Text.ToLower();
                }
                else
                {
                    sAliasName = txtAliasName.Text;
                }
            }

            try
            {
                switch (grpFunction.AccessibleDescription?.ToString())
                {
                    case "rdoAlter":
                        sText = "--add a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n " + MyGlobal.TransferWordCase("ADD ", bToLower) + "New_Column_Name " + MyGlobal.TransferWordCase("VARCHAR", bToLower) + "(50);\r\n\r\n" +
                                "--modify a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("ALTER COLUMN ", bToLower) + "Old_Column_Name " + MyGlobal.TransferWordCase("VARCHAR", bToLower) + "(100) " + MyGlobal.TransferWordCase("NOT NULL;", bToLower);
                        break;
                    case "rdoCreate":
                        var sValue = MyGlobal.GetCreateScript_SQLServer(sSchemaType, sSchemaNode, sSchemaDbo, sSchemaName.Replace(sSchemaDbo + ".", ""));
                        sText = sValue[0];
                        break;
                    case "rdoDrop":
                        sText = "--drop a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("DROP COLUMN ", bToLower) + "Old_Column_Name;\r\n\r\n" +
                                "--drop a table\r\n" + MyGlobal.TransferWordCase("DROP TABLE ", bToLower) + sSchemaName + "; ";
                        break;
                    case "rdoRename":
                        sText = "--rename a column\r\n" + MyGlobal.TransferWordCase("EXEC ", bToLower) + "sp_rename '" + sSchemaName + ".Old_Column_Name', 'New_Column_Name', 'COLUMN';\r\n\r\n" +
                                "--rename a table\r\n" + MyGlobal.TransferWordCase("EXEC ", bToLower) + "sp_rename '" + sSchemaName + "', 'New_Table_Name';";
                        break;
                    case "rdoTruncate":
                        sText = MyGlobal.TransferWordCase("TRUNCATE TABLE ", bToLower) + sSchemaName + ";";
                        break;
                    case "rdoSelectStar":
                    case "rdoSelect":
                    case "rdoInsert":
                    case "rdoDelete":
                    case "rdoUpdate":
                        {
                            if (bPK)
                            {
                                _sPK = "\r\n--Primary Key" + sPK;
                            }

                            for (var i = 0; i < c1Grid.RowCount; i++)
                            {
                                Application.DoEvents();

                                if (chkPKInfo.Checked && bPK && sPK.Contains("`" + c1Grid[i, @"Column_Name"] + "`"))
                                {
                                    var dtRow = dtColumnName.Select("Column_Name = '" + c1Grid[i, @"Column_Name"].ToString().Replace("'", "''") + "'");
                                    sSchema = ", " + MyGlobal.GetDataTypeFormat_SQLServer(dtRow, out sDataType); //GenerateSQL_SQLServer
                                    _sPK = _sPK.Replace("`" + c1Grid[i, @"Column_Name"] + "`", "`" + (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + c1Grid[i, @"Column_Name"] + sSchema + "`");
                                }

                                if (c1Grid[i, " "].ToString() != "1")
                                {
                                    continue;
                                }

                                var sDataTypeN = "";
                                var dtRow0 = dtColumnName.Select("Column_Name = '" + c1Grid[i, @"Column_Name"].ToString().Replace("'", "''") + "'");

                                if (dtRow0.Length > 0)
                                {
                                    sDataTypeN = dtRow0[0]["DataTypeName"].ToString().ToLower();
                                    sSchema = " --" + MyGlobal.GetDataTypeFormat_SQLServer(dtRow0, out sDataType); //GenerateSQL_SQLServer
                                    sSchema = chkColumnTypeInfo.Checked ? sSchema : "";
                                }

                                string sTemp;

                                switch (sDataType.ToLower())
                                {
                                    case "int":
                                    case "number":
                                    case "bigint":
                                    case "integer":
                                    case "decimal":
                                    case "numeric":
                                    case "long":
                                    case "float":
                                    case "smallint":
                                    case "tinyint":
                                        sTemp = "0";
                                        break;
                                    case "time":
                                    case "timestamp":
                                    case "date":
                                    case "datetime":
                                    case "datetime2":
                                    case "datetimeoffset":
                                        //sTemp = MyGlobal.TransferWordCase("CONVERT(CHAR(19), GETDATE(), 120)", bToLower); //YYYY-MM-DD HH:mm:ss
                                        sTemp = MyGlobal.TransferWordCase("GETDATE()", bToLower); //YYYY-MM-DD HH:mm:ss
                                        break;
                                    default:
                                        sTemp = "''";
                                        break;
                                }

                                switch (sDataTypeN)
                                {
                                    case "nchar":
                                    case "nvarchar":
                                    case "ntext":
                                        sTemp = "N''";
                                        break;
                                    case "uniqueidentifier":
                                        sTemp = grpFunction.AccessibleDescription == @"rdoInsert" ? "NEWID()" : "N''";
                                        break;
                                    case "date":
                                        //sTemp = MyGlobal.TransferWordCase("CONVERT(CHAR(10), GETDATE(), 120)", bToLower); //YYYY-MM-DD
                                        sTemp = MyGlobal.TransferWordCase("GETDATE()", bToLower);
                                        break;
                                    case "timestamp":
                                        sTemp = MyGlobal.TransferWordCase("DEFAULT", bToLower); //YYYY-MM-DD HH:mm:ss
                                        break;
                                    case "datetime": //YYYY-MM-DD HH:mm:ss.fff 精確度到後 3 位
                                        sTemp = MyGlobal.TransferWordCase("GETDATE()", bToLower);
                                        break;
                                    case "datetime2": //YYYY-MM-DD HH:mm:ss.fffffff 精確度到後 7 位
                                        sTemp = MyGlobal.TransferWordCase("SYSDATETIME()", bToLower);
                                        break;
                                    case "datetimeoffset": //YYYY-MM-DD HH:mm:ss.fffffff 精確度到後 7 位再加上時區
                                        sTemp = MyGlobal.TransferWordCase("SYSDATETIMEOFFSET()", bToLower);
                                        break;
                                }

                                sTemp = (grpFunction.AccessibleDescription == "rdoInsert" && chkDisplayAsParameter.Checked) ? (sTemp == "N''" ? "N:" : ":") + c1Grid[i, @"Column_Name"] : sTemp;

                                if (j < Convert.ToInt16(cboNumbers.Text == @"All" ? "9999" : cboNumbers.Text) - 1)
                                {
                                    j++;
                                    sSelectedColumnName += (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + (chkEncloseBrackets.Checked ? "[" : "") + c1Grid[i, @"Column_Name"] + (chkEncloseBrackets.Checked ? "]" : "") + ", ";
                                    sColumnValue += sTemp + (i == c1Grid.RowCount - 1 ? "," : ", ");
                                }
                                else
                                {
                                    j = 0;
                                    sSelectedColumnName += (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + (chkEncloseBrackets.Checked ? "[" : "") + c1Grid[i, @"Column_Name"] + (chkEncloseBrackets.Checked ? "]" : "") + ", \r\n       " + (rdoInsert.Checked ? " " : "");
                                    sColumnValue += sTemp + (i == c1Grid.RowCount - 1 ? "," : ", \r\n        ");
                                }

                                sColumnInfo = sColumnInfo + (i == 0 ? "" : "   " + MyGlobal.sSeparator3 + " ") + (chkEncloseBrackets.Checked ? "[" : "") + c1Grid[i, @"Column_Name"] + (chkEncloseBrackets.Checked ? "]" : "") + " = " + sTemp + (i == c1Grid.RowCount - 1 ? "" : MyGlobal.sSeparator) + sSchema + "\r\n";
                            }

                            if (bPK)
                            {
                                _sPK = _sPK.Replace("`", "\r\n--");
                                _sPK = _sPK.Substring(0, _sPK.Length - 4);
                            }

                            sColumnInfo = sColumnInfo.Trim();
                            sColumnValue = string.IsNullOrEmpty(sSelectedColumnName) ? "" : sColumnValue.Trim().Substring(0, sColumnValue.Trim().Length - 1);

                            if (rdoDelete.Checked)
                            {
                                sText = MyGlobal.TransferWordCase("DELETE FROM ", bToLower) + sSchemaName + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bToLower) + (string.IsNullOrEmpty(sSelectedColumnName) ? "/*Your Conditions*/;" : sColumnInfo.Replace(MyGlobal.sSeparator3, MyGlobal.TransferWordCase("AND", bToLower)).Replace(MyGlobal.sSeparator, "") + (chkPKInfo.Checked ? _sPK : ""));
                            }
                            else if (rdoSelect.Checked)
                            {
                                sText = string.IsNullOrEmpty(sSelectedColumnName) ? "" : MyGlobal.TransferWordCase("SELECT ", bToLower) + (chkLimitInfo.Checked ? MyGlobal.TransferWordCase(MyGlobal.TransferWordCase("TOP (100) ", bToLower), bToLower) : "") + sSelectedColumnName.Trim().Substring(0, sSelectedColumnName.Trim().Length - 1) + "\r\n  " + MyGlobal.TransferWordCase("FROM ", bToLower) + sSchemaName + (string.IsNullOrEmpty(sAliasName) ? "" : " " + sAliasName) + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bToLower) + "/*Your Conditions*/;" + (chkPKInfo.Checked ? _sPK : "");
                            }
                            else if (rdoInsert.Checked)
                            {
                                sText = string.IsNullOrEmpty(sSelectedColumnName) ? "" : MyGlobal.TransferWordCase("INSERT INTO ", bToLower) + sSchemaName + "\r\n       (" + sSelectedColumnName.Trim().Substring(0, sSelectedColumnName.Trim().Length - 1) + ")\r\n" + MyGlobal.TransferWordCase("VALUES (", bToLower) + sColumnValue + ");" + (chkPKInfo.Checked ? _sPK : "");

                                if (!string.IsNullOrEmpty(sAliasName))
                                {
                                    sText = sText.Replace((sAliasName + "."), "");
                                }
                            }
                            else if (rdoUpdate.Checked)
                            {
                                sText = MyGlobal.TransferWordCase("UPDATE ", bToLower) + sSchemaName + "\r\n   " + MyGlobal.TransferWordCase("SET ", bToLower) + sColumnInfo.Replace(MyGlobal.sSeparator3, "   ").Replace(MyGlobal.sSeparator, ",") + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bToLower) + "/*Your Conditions*/;" + (chkPKInfo.Checked ? _sPK : "");
                            }
                            else if (rdoSelectStar.Checked)
                            {
                                sText = MyGlobal.TransferWordCase("SELECT ", bToLower) + (chkLimitInfo.Checked ? MyGlobal.TransferWordCase("TOP (100) ", bToLower) : "") + MyGlobal.TransferWordCase((string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + "* FROM ", bToLower) + sSchemaName + (string.IsNullOrEmpty(sAliasName) ? "" : " " + sAliasName) + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bToLower) + "/*Your Conditions*/;" + (chkPKInfo.Checked ? _sPK : "");
                            }
                        }

                        break;
                }

                if (rdoUpperAll.Checked)
                {
                    sText = sText.ToUpper().Replace(" \r\n", "\r\n");
                }
                else if (rdoLowerAll.Checked)
                {
                    sText = sText.ToLower().Replace(" \r\n", "\r\n");
                }

                UpdateEditorText(sText);
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void GenerateSQL_MySQL()
        {
            var sSchema = "";
            var sColumnInfo = "";
            var j = 0;
            var sColumnValue = "";
            var sSelectedColumnName = "";
            var bPK = true;
            var sText = "";
            var sAliasName = "";
            var bToLower = rdoLowerKeywords.Checked;

            Cursor = Cursors.WaitCursor;

            if (txtAliasName.Enabled && !string.IsNullOrEmpty(txtAliasName.Text))
            {
                if (rdoUpperAll.Checked)
                {
                    sAliasName = txtAliasName.Text.ToUpper();
                }
                else if (rdoLowerAll.Checked)
                {
                    sAliasName = txtAliasName.Text.ToLower();
                }
                else
                {
                    sAliasName = txtAliasName.Text;
                }
            }

            try
            {
                switch (grpFunction.AccessibleDescription?.ToString())
                {
                    case "rdoAlter":
                        sText = "--add a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("ADD ", bToLower) + "New_Column_Name " + MyGlobal.TransferWordCase("VARCHAR2", bToLower) + "(50);\r\n\r\n" +
                                "--add multiple columns\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("ADD ", bToLower) + "(New_Column_Name1 " + MyGlobal.TransferWordCase("INT", bToLower) + ",\r\n       New_Column_Name2 " + MyGlobal.TransferWordCase("VARCHAR2", bToLower) + "(100));\r\n\r\n" +
                                "--modify a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("MODIFY ", bToLower) + "Old_Column_Name " + MyGlobal.TransferWordCase("INT", bToLower) + ";\r\n\r\n" +
                                "--modify multiple columns\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("MODIFY ", bToLower) + "(Old_Column_Name1 " + MyGlobal.TransferWordCase("VARCHAR2", bToLower) + "(100) " + MyGlobal.TransferWordCase("NOT NULL", bToLower) + ",\r\n          Old_Column_Name2 " + MyGlobal.TransferWordCase("VARCHAR2", bToLower) + "(75));";
                        break;
                    case "rdoCreate":
                        sText = MyGlobal.GetCreateScript_MySQL(sSchemaType, sSchemaNode, sSchemaName);
                        break;
                    case "rdoDrop":
                        sText = "--drop a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("DROP COLUMN ", bToLower) + "Old_Column_Name;\r\n\r\n" +
                                "--drop a table\r\n" + MyGlobal.TransferWordCase("DROP TABLE ", bToLower) + sSchemaName + ";";
                        break;
                    case "rdoRename":
                        sText = "--rename a column\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("RENAME COLUMN ", bToLower) + "Old_Column_Name " + MyGlobal.TransferWordCase("TO ", bToLower) + "New_Column_Name;\r\n\r\n" +
                                "--rename a table\r\n" + MyGlobal.TransferWordCase("ALTER TABLE ", bToLower) + sSchemaName + "\r\n  " + MyGlobal.TransferWordCase("RENAME TO ", bToLower) + "New_Table_Name;";
                        break;
                    case "rdoTruncate":
                        sText = MyGlobal.TransferWordCase("TRUNCATE TABLE ", bToLower) + sSchemaName + ";";
                        break;
                    case "rdoSelectStar":
                    case "rdoSelect":
                    case "rdoInsert":
                    case "rdoDelete":
                    case "rdoUpdate":
                        {
                            if (bPK)
                            {
                                _sPK = "\r\n--Primary Key" + sPK;
                            }

                            for (var i = 0; i < c1Grid.RowCount; i++)
                            {
                                Application.DoEvents();

                                if (chkPKInfo.Checked && bPK && sPK.Contains("`" + c1Grid[i, @"Column_Name"] + "`"))
                                {
                                    sSchema = ", " + c1Grid[i, @"DataType"];
                                    _sPK = _sPK.Replace("`" + c1Grid[i, @"Column_Name"] + "`", "`" + c1Grid[i, @"Column_Name"] + sSchema + "`");
                                }

                                if (c1Grid[i, " "].ToString() != "1")
                                {
                                    continue;
                                }

                                var dtRow = dtColumnName.Select("Column_Name = '" + c1Grid[i, @"Column_Name"].ToString().Replace("'", "''") + "'");

                                if (dtRow.Length > 0)
                                {
                                    sSchema = " --" + c1Grid[i, @"DataType"];
                                    sSchema = chkColumnTypeInfo.Checked ? sSchema : "";
                                }

                                var sDataType = c1Grid[i, @"TypeName"].ToString().ToLower();

                                if (sDataType.IndexOf("(", StringComparison.Ordinal) != -1)
                                {
                                    sDataType = sDataType.Substring(0, sDataType.IndexOf("(", StringComparison.Ordinal));
                                }

                                string sTemp;

                                switch (sDataType.ToLower())
                                {
                                    case "int":
                                    case "number":
                                    case "bigint":
                                    case "integer":
                                    case "decimal":
                                    case "numeric":
                                    case "long":
                                    case "float":
                                    case "smallint":
                                    case "tinyint":
                                        sTemp = "0";
                                        break;
                                    case "time":
                                    case "timestamp":
                                    case "date":
                                    case "datetime":
                                    case "datetime2":
                                    case "datetimeoffset":
                                        sTemp = "CONVERT(CHAR(19), GETDATE(), 120)"; //YYYY-MM-DD HH:mm:ss
                                        break;
                                    default:
                                        sTemp = "''";
                                        break;
                                }

                                sTemp = (grpFunction.AccessibleDescription == "rdoInsert" && chkDisplayAsParameter.Checked) ? ":" + c1Grid[i, @"Column_Name"] : sTemp;

                                if (j < Convert.ToInt16(cboNumbers.Text == @"All" ? "9999" : cboNumbers.Text) - 1)
                                {
                                    j++;
                                    sSelectedColumnName += (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + c1Grid[i, @"Column_Name"] + ", ";
                                    sColumnValue += sTemp + (i == c1Grid.RowCount - 1 ? "," : ", ");
                                }
                                else
                                {
                                    j = 0;
                                    sSelectedColumnName += (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + c1Grid[i, @"Column_Name"] + ", \r\n       " + (rdoInsert.Checked ? " " : "");
                                    sColumnValue += sTemp + (i == c1Grid.RowCount - 1 ? "," : ", \r\n        ");
                                }

                                sColumnInfo = sColumnInfo + (i == 0 ? "" : "   " + MyGlobal.sSeparator3 + " ") + c1Grid[i, @"Column_Name"] + " = " + sTemp + (i == c1Grid.RowCount - 1 ? "" : MyGlobal.sSeparator) + sSchema + "\r\n";
                            }

                            if (bPK)
                            {
                                _sPK = _sPK.Replace("`", "\r\n--");
                                _sPK = _sPK.Substring(0, _sPK.Length - 4);
                            }

                            sColumnInfo = sColumnInfo.Trim();
                            sColumnValue = string.IsNullOrEmpty(sSelectedColumnName) ? "" : sColumnValue.Trim().Substring(0, sColumnValue.Trim().Length - 1);

                            if (rdoDelete.Checked)
                            {
                                sText = MyGlobal.TransferWordCase("DELETE FROM ", bToLower) + sSchemaName + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bToLower) + (string.IsNullOrEmpty(sSelectedColumnName) ? "/*Your Conditions*/;" : sColumnInfo.Replace(MyGlobal.sSeparator3, MyGlobal.TransferWordCase("AND", bToLower)).Replace(MyGlobal.sSeparator, "") + (chkPKInfo.Checked ? _sPK : ""));
                            }
                            else if (rdoSelect.Checked)
                            {
                                sText = string.IsNullOrEmpty(sSelectedColumnName) ? "" : MyGlobal.TransferWordCase("SELECT ", bToLower) + sSelectedColumnName.Trim().Substring(0, sSelectedColumnName.Trim().Length - 1) + "\r\n  " + MyGlobal.TransferWordCase("FROM ", bToLower) + sSchemaName + (string.IsNullOrEmpty(sAliasName) ? "" : " " + sAliasName) + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bToLower) + "/*Your Conditions*/" + (chkLimitInfo.Checked ? MyGlobal.TransferWordCase(" LIMIT 10", bToLower) : "") + ";" + (chkPKInfo.Checked ? _sPK : "");
                            }
                            else if (rdoInsert.Checked)
                            {
                                sText = string.IsNullOrEmpty(sSelectedColumnName) ? "" : MyGlobal.TransferWordCase("INSERT INTO ", bToLower) + sSchemaName + "\r\n       (" + sSelectedColumnName.Trim().Substring(0, sSelectedColumnName.Trim().Length - 1) + ")\r\n" + MyGlobal.TransferWordCase("VALUES (", bToLower) + sColumnValue + ");" + (chkPKInfo.Checked ? _sPK : "");

                                if (!string.IsNullOrEmpty(sAliasName))
                                {
                                    sText = sText.Replace((sAliasName + "."), "");
                                }
                            }
                            else if (rdoUpdate.Checked)
                            {
                                sText = MyGlobal.TransferWordCase("UPDATE ", bToLower) + sSchemaName + "\r\n   " + MyGlobal.TransferWordCase("SET ", bToLower) + sColumnInfo.Replace(MyGlobal.sSeparator3, "   ").Replace(MyGlobal.sSeparator, ",") + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bToLower) + "/*Your Conditions*/;" + (chkPKInfo.Checked ? _sPK : "");
                            }
                            else if (rdoSelectStar.Checked)
                            {
                                sText = MyGlobal.TransferWordCase("SELECT " + (string.IsNullOrEmpty(sAliasName) ? "" : sAliasName + ".") + "* FROM ", bToLower) + sSchemaName + (string.IsNullOrEmpty(sAliasName) ? "" : " " + sAliasName) + "\r\n " + MyGlobal.TransferWordCase("WHERE ", bToLower) + "/*Your Conditions*/" + (chkLimitInfo.Checked ? MyGlobal.TransferWordCase(" LIMIT 10", bToLower) : "") + ";" + (chkPKInfo.Checked ? _sPK : "");
                            }
                        }

                        break;
                }

                if (rdoUpperAll.Checked)
                {
                    sText = sText.ToUpper().Replace(" \r\n", "\r\n");
                }
                else if (rdoLowerAll.Checked)
                {
                    sText = sText.ToLower().Replace(" \r\n", "\r\n");
                }

                UpdateEditorText(sText);
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void UpdateEditorText(string sText)
        {
            editor.ReadOnly = false;
            editor.Text = sText;
            editor.ReadOnly = true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            var sValue = sender is C1.Win.C1Input.C1Button btn && btn.Tag.ToString() == "SelectAll" ? "1" : "0";

            var iRow = c1Grid.Row;
            var iCol = c1Grid.Col;

            try
            {
                for (var j = c1Grid.RowCount - 1; j >= 0; j--)
                {
                    c1Grid[j, 0] = sValue;
                }

                c1Grid.Row = iRow;
                c1Grid.Col = iCol;
                c1Grid.Select(); //Focus 切換到指定的 Cell
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Function_CheckedChanged(object sender, EventArgs e)
        {
            var bNumbersValue = false;
            var bColumnTypeInfoValue = false;
            var bPKValue = false;
            var bLimitValue = false;
            var bAliasNameValue = false;
            var bBracketsValue = false;
            var bKeywordsValue = true;
            var bDisplayAsParameterValue = false;
            var rdo = sender as RadioButton;

            if (rdo.Checked == false)
            {
                return; //避免執行兩次 (第一次會是「前一次」的選項，且為「未核取」，忽略它！)
            }

            grpFunction.AccessibleDescription = rdo.Name;

            rdoDoNothing.Visible = false;

            switch (rdo.Name)
            {
                case "rdoSelect":
                    bNumbersValue = true;
                    bPKValue = !string.IsNullOrEmpty(sPK);
                    bLimitValue = true;
                    bAliasNameValue = true;
                    bBracketsValue = MyGlobal.sDataSource == "SQL Server" || MyGlobal.sDataSource == "MySQL";
                    break;
                case "rdoInsert":
                    bNumbersValue = true;
                    bPKValue = !string.IsNullOrEmpty(sPK);
                    bDisplayAsParameterValue = true;
                    bBracketsValue = MyGlobal.sDataSource == "SQL Server" || MyGlobal.sDataSource == "MySQL";
                    break;
                case "rdoDelete":
                case "rdoUpdate":
                    bColumnTypeInfoValue = true;
                    bPKValue = !string.IsNullOrEmpty(sPK);
                    bBracketsValue = MyGlobal.sDataSource == "SQL Server" || MyGlobal.sDataSource == "MySQL";
                    break;
                case "rdoSelectStar":
                    bPKValue = !string.IsNullOrEmpty(sPK);
                    bLimitValue = true;
                    bAliasNameValue = true;
                    break;
                case "rdoCreate":
                    rdoUpperAll.CheckedChanged -= ChangeCase_CheckedChanged;
                    rdoUpperAll.Checked = true;
                    rdoUpperAll.CheckedChanged += ChangeCase_CheckedChanged;

                    bKeywordsValue = false;
                    rdoDoNothing.Visible = true;

                    rdoDoNothing.CheckedChanged -= ChangeCase_CheckedChanged;
                    rdoDoNothing.Checked = true;
                    rdoDoNothing.CheckedChanged += ChangeCase_CheckedChanged;

                    break;
            }

            if (rdo.Name != "rdoCreate" && rdoDoNothing.Checked)
            {
                rdoUpperAll.CheckedChanged -= ChangeCase_CheckedChanged;
                rdoUpperAll.Checked = true;
                rdoUpperAll.CheckedChanged += ChangeCase_CheckedChanged;
            }

            lblNumbers.Enabled = bNumbersValue;
            cboNumbers.Enabled = bNumbersValue;
            chkColumnTypeInfo.Enabled = bColumnTypeInfoValue;
            chkPKInfo.Enabled = bPKValue;
            chkLimitInfo.Enabled = bLimitValue;
            chkAliasName.Enabled = bAliasNameValue;
            txtAliasName.Enabled = bAliasNameValue;
            chkEncloseBrackets.Enabled = bBracketsValue;
            chkEncloseGraveAccent.Enabled = bBracketsValue;
            rdoUpperKeywords.Enabled = bKeywordsValue;
            rdoLowerKeywords.Enabled = bKeywordsValue;
            chkDisplayAsParameter.Visible = bDisplayAsParameterValue;

            btnPreview.PerformClick();
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
            }
        }

        private static DataTable CreateColumnTable(DataTable dtTemp, string sColumnName, string sValue)
        {
            var dtColumn = new DataTable();

            dtColumn.Columns.Add(" ");
            dtColumn.Columns.Add("Column_Name");
            dtColumn.Columns.Add("Column_ID", typeof(int));
            dtColumn.Columns.Add("TypeName");
            dtColumn.Columns.Add("DataTypeName");
            dtColumn.Columns.Add("DataType");
            dtColumn.Columns.Add("ColumnSize");
            dtColumn.Columns.Add("NumericScale");
            dtColumn.Columns.Add("NumericPrecision");

            if (dtTemp == null)
            {
                return dtColumn;
            }

            for (var i = 0; i < dtTemp.Rows.Count; i++)
            {
                var row = dtColumn.NewRow();
                row[" "] = string.IsNullOrEmpty(sValue) ? dtTemp.Rows[i][" "] : sValue;
                row["Column_Name"] = dtTemp.Rows[i]["Column_Name"];
                row["Column_ID"] = dtTemp.Rows[i]["Column_ID"];
                row["TypeName"] = dtTemp.Rows[i]["TypeName"];
                row["DataTypeName"] = dtTemp.Rows[i]["DataTypeName"];
                row["DataType"] = dtTemp.Rows[i]["DataType"];
                row["ColumnSize"] = dtTemp.Rows[i]["ColumnSize"];
                row["NumericScale"] = dtTemp.Rows[i]["NumericScale"];
                row["NumericPrecision"] = dtTemp.Rows[i]["NumericPrecision"];

                dtColumn.Rows.Add(row);
            }

            return dtColumn;
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            btnPreview.PerformClick();
            Clipboard.SetDataObject(editor.Text, false);
            _bClose = true;
            Close();
        }

        private void btnPasteToQueryEditor_Click(object sender, EventArgs e)
        {
            btnPreview.PerformClick();
            MyGlobal.sGlobalTemp = "generatesqlpaste2queryeditor" + MyGlobal.sSeparator + sAccessibleDescription + MyGlobal.sSeparatorPlus1 + "\r\n" + editor.Text;
            _bClose = true;
            Close();
        }

        private void txtAliasName_KeyPress(object sender, KeyPressEventArgs e)
        {
            var ch = e.KeyChar;
            var regex = new Regex(@"[^a-zA-Z0-9]");

            if (ch == 46 || (regex.IsMatch(e.KeyChar.ToString()) && ch != 8)) //8：倒退建; 46：句點
            {
                e.Handled = true;
            }

            CheckAliasName();
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CheckAliasName();

            if (!string.IsNullOrEmpty(txtAliasName.Text))
            {
                btnPreview.PerformClick();
            }
        }

        private void CheckAliasName()
        {
            chkAliasName.Checked = !string.IsNullOrEmpty(txtAliasName.Text.Trim());
        }

        private void chkAliasName_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAliasName.Checked)
            {
                txtAliasName.Text = "";
            }
            else
            {
                txtAliasName.Focus();
            }
        }

        private void ChangeCase_CheckedChanged(object sender, EventArgs e)
        {
            var rdo = sender as RadioButton;

            if (rdo.Checked == false)
            {
                return; //避免執行兩次 (第一次會是「前一次」的選項，忽略它！)
            }

            btnPreview.PerformClick();
        }

        private void cboNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPreview.PerformClick();
        }

        private void chkColumnTypeInfo_CheckedChanged(object sender, EventArgs e)
        {
            btnPreview.PerformClick();
        }

        private void chkPKInfo_CheckedChanged(object sender, EventArgs e)
        {
            btnPreview.PerformClick();
        }

        private void chkLimitInfo_CheckedChanged(object sender, EventArgs e)
        {
            btnPreview.PerformClick();
        }

        private void chkEncloseBrackets_CheckedChanged(object sender, EventArgs e)
        {
            btnPreview.PerformClick();
        }

        private void chkEncloseGraveAccent_CheckedChanged(object sender, EventArgs e)
        {
            btnPreview.PerformClick();
        }

        private void Options_CheckedChanged(object sender, EventArgs e)
        {
            btnPreview.PerformClick();
        }

        private void btnShowSql_Click(object sender, EventArgs e)
        {
            MessageBox.Show(sSqlOfGetColumnInfo, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cboSchema_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboSchema.Text))
            {
                return;
            }

            MyGlobal.GetTableInfo_PostgreSQL(out _dtTable, cboSchema.Text);
            MyGlobal.GetViewInfo_PostgreSQL(out _dtView, cboSchema.Text);
            MyGlobal.SetC1ComboBoxItemsFromDataTable(cboTable, _dtTable);
            MyGlobal.SetC1ComboBoxItemsFromDataTable(cboView, _dtView);
        }

        private void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bKeypressComboBox)
            {
                return; //鍵盤輸入，忽略！
            }

            _bKeyPressTab = true;

            if (!string.IsNullOrEmpty(cboTable.Text))
            {
                sSchemaType = "Tables";
                rdoTable.Checked = true;
                btnSelect.PerformClick();
            }
        }

        private void cboView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bKeypressComboBox)
            {
                return; //鍵盤輸入，忽略！
            }

            _bKeyPressTab = true;

            if (!string.IsNullOrEmpty(cboView.Text))
            {
                sSchemaType = "Views";
                rdoView.Checked = true;
                btnSelect.PerformClick();
            }
        }

        protected override void WndProc(ref Message m)
        {
            var WM_SYSCOMMAND = 0x0112;
            var SC_CLOSE = 0xF060;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE) //使用者按下右上角 X，將 Form 整個關閉
            {
                _bClose = true;
            }

            base.WndProc(ref m);
        }

        private void btnSelectObject_Click(object sender, EventArgs e)
        {
            var sSql = "";
            var dtTemp = new DataTable();
            sSchemaType = rdoTable.Checked ? "Tables" : (rdoView.Checked ? "Views" : "");
            sSchemaName = rdoTable.Checked ? cboTable.Text : (rdoView.Checked ? cboView.Text : "");

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    {
                        sSql = "SELECT cols.Column_Name FROM all_constraints cons, all_cons_columns cols WHERE cols.Table_Name = '{0}' AND cons.Constraint_Type = 'P' AND cons.Constraint_Name = cols.Constraint_Name AND cons.Owner = cols.Owner AND UPPER(cons.Owner) = '{1}' ORDER BY cols.Table_Name, cols.Position";
                        sSql = string.Format(sSql, sSchemaName, MyGlobal.sDBUser.ToUpper());
                        MyGlobal.ExecuteQueryToDataTable(sSql, ref dtTemp); //btnSelectObject_Click

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sPK = "`";

                            for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                            {
                                sPK += dtTemp.Rows[iRow]["Column_Name"] + "`";
                            }
                        }

                        //取得 Table/View 的所有欄位 (按照原始順序)
                        sSql = "SELECT '1' AS \" \", Column_Name, Column_ID, Data_Type AS TypeName, Data_Type AS DataTypeName, Data_Type AS DataType, Data_Length AS ColumnSize, Data_Scale AS NumericScale, Data_Precision AS NumericPrecision FROM user_tab_columns WHERE Table_Name = '{0}' ORDER BY Column_ID";
                        sSql = string.Format(sSql, sSchemaName);
                        MyGlobal.ExecuteQueryToDataTable(sSql, ref dtTemp); //btnSelectObject_Click

                        if (dtTemp == null)
                        {
                            dtColumnName = null;
                        }
                        else
                        {
                            dtColumnName = dtTemp.Copy();
                        }

                        break;
                    }
                case "PostgreSQL":
                    {
                        sSchemaNode = cboSchema.Text;

                        if (sSchemaType == "Tables")
                        {
                            sSql = "SELECT ee.Constraint_Schema AS SchemaName, ee.Table_Name AS TableName, ee.Column_Name, LOWER(SUBSTR(ss.Constraint_Type, 1, 1)) || ', ' || ss.Constraint_Name AS ConstraintInfo FROM information_schema.key_column_usage ee, information_schema.table_constraints ss WHERE ee.Constraint_Catalog = ss.Constraint_Catalog AND ee.Constraint_Schema = ss.Constraint_Schema AND ee.Table_Schema = ss.Table_Schema AND ee.Table_Name = ss.Table_Name AND ee.Constraint_Name = ss.Constraint_Name AND ss.Constraint_Type <> 'CHECK' AND ee.Constraint_Schema = '{0}' AND ee.Table_Name = '{1}' ORDER BY ee.Ordinal_Position";
                            sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                            MyGlobal.ExecuteQueryToDataTable(sSql, ref dtTemp); //btnSelectObject_Click

                            if (dtTemp != null && dtTemp.Rows.Count > 0)
                            {
                                sPK = "`";

                                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                                {
                                    sPK += dtTemp.Rows[iRow]["Column_Name"] + "`";
                                }
                            }

                            //取得 Table 的所有指定欄位, 按照原始順序
                            sSql = "SELECT '1' AS \" \", cs.Column_Name AS Column_Name, cs.Ordinal_Position AS Column_ID, LOWER(cs.Data_Type) AS TypeName, LOWER(cs.Data_Type) AS DataTypeName, LOWER(cs.Data_Type) AS DataType, cs.Character_Maximum_Length AS ColumnSize, cs.Numeric_Scale AS NumericScale, cs.Numeric_Precision AS NumericPrecision FROM pg_catalog.pg_tables ts, information_schema.columns cs WHERE ts.SchemaName != 'pg_catalog' AND ts.SchemaName != 'information_schema' AND ts.SchemaName = cs.Table_Schema AND ts.TableName = cs.Table_Name AND ts.SchemaName = '{0}' AND ts.TableName = '{1}' ORDER BY cs.Ordinal_Position";
                            sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                            MyGlobal.ExecuteQueryToDataTable(sSql, ref dtTemp); //btnSelectObject_Click

                            if (dtTemp == null)
                            {
                                dtColumnName = null;
                            }
                            else
                            {
                                dtColumnName = dtTemp.Copy();
                            }
                        }
                        else
                        {
                            var drPostgreSql = MyGlobal.oPostgreReader.ExecuteQueryPaged100Rows("SELECT * FROM " + sSchemaNode + "." + sSchemaName + " LIMIT 1", 1, 1, out bool bRollback, out bool bPermissionDenied);

                            if (drPostgreSql != null)
                            {
                                var dtSchemaTable = drPostgreSql.GetSchemaTable();

                                var dv = dtSchemaTable.DefaultView;
                                dv.Sort = "ColumnOrdinal";
                                dtTemp = dv.ToTable();

                                dtColumnName = new DataTable();

                                dtColumnName.Columns.Add(" ");
                                dtColumnName.Columns.Add("Column_Name");
                                dtColumnName.Columns.Add("Column_ID", typeof(int));
                                dtColumnName.Columns.Add("TypeName");
                                dtColumnName.Columns.Add("DataTypeName");
                                dtColumnName.Columns.Add("DataType");
                                dtColumnName.Columns.Add("ColumnSize");
                                dtColumnName.Columns.Add("NumericScale");
                                dtColumnName.Columns.Add("NumericPrecision");

                                for (var j = 0; j < dtTemp.Rows.Count; j++)
                                {
                                    DataRow row = dtColumnName.NewRow();
                                    row[" "] = "1";

                                    var dtRow = dtTemp.Select("ColumnName = '" + dtTemp.Rows[j]["ColumnName"].ToString().Replace("'", "''") + "'");
                                    var sColumnDataType = MyGlobal.GetDataTypeFormat_PostgreSQL(dtRow, out var sDataType);

                                    if (sColumnDataType.IndexOf("(", StringComparison.Ordinal) != -1)
                                    {
                                        sColumnDataType = sColumnDataType.Substring(0, sColumnDataType.IndexOf("(", StringComparison.Ordinal));
                                    }

                                    row["Column_Name"] = dtTemp.Rows[j]["ColumnName"];
                                    row["Column_ID"] = dtTemp.Rows[j]["ColumnOrdinal"];
                                    row["TypeName"] = sColumnDataType;
                                    row["DataTypeName"] = sColumnDataType;
                                    row["DataType"] = sColumnDataType;
                                    row["ColumnSize"] = dtTemp.Rows[j]["ColumnSize"];
                                    row["NumericScale"] = dtTemp.Rows[j]["NumericScale"];
                                    row["NumericPrecision"] = dtTemp.Rows[j]["NumericPrecision"];

                                    dtColumnName.Rows.Add(row);
                                }
                            }
                        }

                        break;
                    }
                case "SQL Server":
                    {
                        sSchemaNode = txtDatabase.Text;

                        //取得 Table 的所有指定欄位, 按照原始順序
                        if (sSchemaType == "Tables")
                        {
                            sSql = "SELECT '1' AS \" \", Column_Name, Ordinal_Position AS Column_ID, Data_Type AS TypeName, Data_Type AS DataTypeName, Data_Type AS DataType, Character_Maximum_Length AS ColumnSize, Numeric_Scale AS NumericScale, Numeric_Precision AS NumericPrecision FROM {0}.INFORMATION_SCHEMA.COLUMNS WHERE Table_Name = '{1}' ORDER BY Ordinal_Position";
                            sSql = string.Format(sSql, sSchemaNode, sSchemaName.Replace(sSchemaDbo + ".", ""));
                        }
                        else
                        {
                            var sObjectID = "";

                            sSql = "SELECT o.* FROM {0}.sys.all_objects o WHERE Type = 'V' AND Name = '{1}';";
                            sSql = string.Format(sSql, sSchemaNode, cboView.Text);
                            MyGlobal.ExecuteQueryToDataTable(sSql, ref dtTemp); //btnSelectObject_Click

                            if (dtTemp != null && dtTemp.Rows.Count > 0)
                            {
                                sObjectID = dtTemp.Rows[0]["Object_ID"].ToString();
                            }

                            //View
                            sSql = "";
                            sSql += "SELECT '1' AS \" \", c.Name AS Column_Name,\r\n";
                            sSql += "       c.Column_ID,\r\n";
                            sSql += "       TYPE_NAME(User_Type_ID) AS TypeName,\r\n";
                            sSql += "       TYPE_NAME(User_Type_ID) AS DataTypeName,\r\n";
                            sSql += "       TYPE_NAME(User_Type_ID) AS DataType,\r\n";
                            sSql += "       c.Max_Length AS ColumnSize,\r\n";
                            sSql += "       c.Scale AS NumericScale,\r\n";
                            sSql += "       c.Precision AS NumericPrecision\r\n";
                            sSql += "FROM {0}.sys.Columns c\r\n";
                            sSql += "JOIN {0}.sys.Views v ON v.Object_ID = c.Object_ID\r\n";
                            sSql += "WHERE c.Object_ID = {1}";
                            sSql = string.Format(sSql, sSchemaNode, sObjectID);
                        }

                        MyGlobal.ExecuteQueryToDataTable(sSql, ref dtTemp); //btnSelectObject_Click

                        if (dtTemp == null)
                        {
                            dtColumnName = null;
                        }
                        else
                        {
                            dtColumnName = dtTemp.Copy();
                        }

                        break;
                    }
                case "MySQL":
                    {
                        sSchemaNode = txtDatabase.Text;

                        if (sSchemaType == "Tables")
                        {
                            sSql = "SELECT Column_Name, Constraint_Name AS ConstraintInfo FROM `information_schema`.`key_column_usage` WHERE Table_Schema = '{0}' AND Table_Name = '{1}' AND Referenced_Table_Name IS NOT NULL;";
                            sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                            MyGlobal.ExecuteQueryToDataTable(sSql, ref dtTemp); //btnSelectObject_Click

                            if (dtTemp != null && dtTemp.Rows.Count > 0)
                            {
                                sPK = "`";

                                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                                {
                                    sPK += dtTemp.Rows[iRow]["Column_Name"] + "`";
                                }
                            }
                        }

                        //取得 Table/View 的所有指定欄位, 按照原始順序
                        sSql = "SELECT '1' AS \" \", Column_Name, Ordinal_Position AS Column_ID, Column_Type AS TypeName, Column_Type AS DataTypeName, Column_Type AS DataType, Character_Maximum_Length AS ColumnSize, Numeric_Scale AS NumericScale, Numeric_Precision AS NumericPrecision FROM `information_schema`.`columns` WHERE Table_Schema = '{0}' AND Table_Name = '{1}' ORDER BY Ordinal_Position;";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        MyGlobal.ExecuteQueryToDataTable(sSql, ref dtTemp); //btnSelectObject_Click

                        if (dtTemp == null)
                        {
                            dtColumnName = null;
                        }
                        else
                        {
                            dtColumnName = dtTemp.Copy();
                        }

                        break;
                    }
                case "SQLite":
                case "SQLCipher":

                    break;
            }

            btnPreview.Enabled = true;
            Initial();
        }

        //調整下拉清單的大小
        private void ResizeACGrid(C1TrueDBGrid c1Grid1, int RowCount, int iWidth)
        {
            var iHeight = 0;

            if (RowCount <= 6)
            {
                iWidth = MyGlobal.ResizeGridColumnWidth(c1Grid1) + 4;
            }
            else
            {
                iWidth = MyGlobal.ResizeGridColumnWidth(c1Grid1) + c1Grid1.VScrollBar.Width + 5;
            }

            iHeight = 124;

            c1Grid1.Size = new Size(iWidth, iHeight);
        }

        private void cboTable_BeforeDropDownOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _bKeypressComboBox = false;

            if (MyGlobal.sDataSource == "PostgreSQL" && string.IsNullOrEmpty(cboSchema.Text))
            {
                e.Cancel = true;
                _sLangText = MyGlobal.GetLanguageString("Please select a schema first.", "form", Name, "msg", "SelectSchemaFirst", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboSchema.Focus();
            }
            else if ((MyGlobal.sDataSource == "SQL Server" || MyGlobal.sDataSource == "MySQL") && string.IsNullOrEmpty(txtDatabase.Text))
            {
                e.Cancel = true;
                _sLangText = MyGlobal.GetLanguageString("Please select a specific database first.", "form", Name, "msg", "SelectDatabaseFirst", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboSchema.Focus();
            }
        }

        private void cboTable_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            if (_bKeyPressTab)
            {
                _bKeyPressTab = false;
                c1GridTable.Visible = false;
                return;
            }
        }

        private void cboTable_DropDownOpened(object sender, EventArgs e)
        {
            c1GridTable.Visible = false;
        }

        private void cboTable_EnterOrFocus()
        {
            if (_bKeyPressTab)
            {
                _bKeyPressTab = false;
                return;
            }

            var iRowCount = c1GridTable_Filter(cboTable.Text);

            if (iRowCount > 0)
            {
                ResizeACGrid(c1GridTable, iRowCount, 0);
                c1GridTable.Visible = true;
            }
            else
            {
                c1GridTable.Visible = false;
            }
        }

        private int c1GridTable_Filter(string sCondition)
        {
            if (_dtTable == null)
            {
                return 0;
            }

            var dataView = _dtTable.DefaultView;

            try
            {
                var condition = "[SchemaName] LIKE '*" + sCondition + "*'";
                dataView.RowFilter = condition;

                if (dataView.Count == 0)
                {
                    dataView.RowFilter = "[SchemaName] LIKE '*'";
                }

                //20220915 將前面幾個字母相符的排在最前面
                #region
                dataView.Sort = "SchemaName";
                var dtSorted = dataView.ToTable();
                dtSorted.Columns.Add("Sort", typeof(int));
                var j = -1000;
                var sFilterKeyword = MyGlobal.GetStringBetween(condition, "'", "'").Replace("*", "").ToUpper();

                for (var i = 0; i < dtSorted.Rows.Count; i++)
                {
                    if (dtSorted.Rows[i]["SchemaName"].ToString().ToUpper().Substring(0, sFilterKeyword.Length) == sFilterKeyword)
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
                dataView.Sort = "Sort, SchemaName";
                dtSorted = dataView.ToTable();
                dtSorted.Columns.Remove("Sort");
                #endregion

                c1GridTable.Splits[0].RecordSelectors = false;
                c1GridTable.DataSource = dtSorted;

                return dtSorted.Rows.Count;
            }
            catch (Exception)
            {
                return 0; //按下 Ctrl+J 可能會進到這個例外錯誤
            }
        }

        private void cboTable_Enter(object sender, EventArgs e)
        {
            cboTable_EnterOrFocus();
        }

        private void cboTable_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    c1GridTable.Focus();
                    e.SuppressKeyPress = false;
                    break;
                case Keys.Delete:
                    _bKeyPressDelete = true; //不能在此處理 Delete 按鍵，因為此時的 cboTable.Text 的值是按下 Delete 鍵之前的！
                    break;
            }
        }

        private void cboTable_KeyPress(object sender, KeyPressEventArgs e)
        {
            _bKeypressComboBox = true;
        }

        private void cboTable_KeyUp(object sender, KeyEventArgs e)
        {
            if (_bKeyPressTab)
            {
                _bKeyPressTab = false; //Tab 會回到這裡，不重複處理！
            }
            else if (_bKeyPressESC)
            {
                _bKeyPressESC = false; //ESC 會回到這裡，不重複處理！
            }
            else
            {
                var iRowCount = c1GridTable_Filter(cboTable.Text);

                if (iRowCount > 0)
                {
                    ResizeACGrid(c1GridTable, iRowCount, 0);
                    c1GridTable.Visible = true;
                }
                else
                {
                    c1GridTable.Visible = false;
                }
            }
        }

        private void cboTable_Leave(object sender, EventArgs e)
        {
            if (!c1GridTable.Focused)
            {
                c1GridTable.Visible = false;
            }
        }

        private void cboTable_TextChanged(object sender, EventArgs e)
        {
            if (cboTable.Items.Count == 0)
            {
                return;
            }

            if (_bKeyPressDelete)
            {
                _bKeyPressDelete = false;
            }
            else
            {
                return;
            }

            var iRowCount = c1GridTable_Filter(cboTable.Text);

            if (iRowCount > 0)
            {
                ResizeACGrid(c1GridTable, iRowCount, 0);
                c1GridTable.Visible = true;
            }
            else
            {
                c1GridTable.Visible = false;
            }
        } //1231

        private void c1GridTable_Leave(object sender, EventArgs e)
        {
            c1GridTable.Visible = false;
        }

        private void c1GridTable_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13 && e.KeyChar != 9)
            {
                return;
            }

            _bKeyPressTab = true;
            c1GridTable_PasteColumnName(); //c1GridTable_KeyPress
            _bKeyPressTab = true;
        }

        private void c1GridTable_PasteColumnName()
        {
            var sCellText = c1GridTable[c1GridTable.Row, 0].ToString();

            cboTable.Text = sCellText;
            c1GridTable.Visible = false;
            cboTable.Focus();
            _bKeyPressTab = true;
            rdoTable.Checked = true;

            if (!string.IsNullOrEmpty(cboTable.Text))
            {
                sSchemaType = "Tables";
                btnSelect.PerformClick();
            }
        }

        private void c1GridTable_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _bKeyPressTab = true;
            c1GridTable_PasteColumnName(); //c1GridTable_MouseDoubleClick
            _bKeyPressTab = false;
        }

        private void cboView_BeforeDropDownOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _bKeypressComboBox = false;

            if (MyGlobal.sDataSource == "PostgreSQL" && string.IsNullOrEmpty(cboSchema.Text))
            {
                e.Cancel = true;
                _sLangText = MyGlobal.GetLanguageString("Please select a schema first.", "form", Name, "msg", "SelectSchemaFirst", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboSchema.Focus();
            }
            else if ((MyGlobal.sDataSource == "SQL Server" || MyGlobal.sDataSource == "MySQL") && string.IsNullOrEmpty(txtDatabase.Text))
            {
                e.Cancel = true;
                _sLangText = MyGlobal.GetLanguageString("Please select a specific database first.", "form", Name, "msg", "SelectDatabaseFirst", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboSchema.Focus();
            }
        }

        private void cboView_DropDownClosed(object sender, C1.Win.C1Input.DropDownClosedEventArgs e)
        {
            if (_bKeyPressTab)
            {
                _bKeyPressTab = false;
                c1GridView.Visible = false;
                return;
            }
        }

        private void cboView_DropDownOpened(object sender, EventArgs e)
        {
            c1GridView.Visible = false;
        }

        private void cboView_EnterOrFocus()
        {
            if (_bKeyPressTab)
            {
                _bKeyPressTab = false;
                return;
            }

            var iRowCount = c1GridView_Filter(cboView.Text);

            if (iRowCount > 0)
            {
                ResizeACGrid(c1GridView, iRowCount, 0);
                c1GridView.Visible = true;
            }
            else
            {
                c1GridView.Visible = false;
            }
        }

        private int c1GridView_Filter(string sCondition)
        {
            if (_dtView == null)
            {
                return 0;
            }

            var dataView = _dtView.DefaultView;

            try
            {
                var condition = "[SchemaName] LIKE '*" + sCondition + "*'";
                dataView.RowFilter = condition;

                if (dataView.Count == 0)
                {
                    dataView.RowFilter = "[SchemaName] LIKE '*'";
                }

                //20220915 將前面幾個字母相符的排在最前面
                #region
                dataView.Sort = "SchemaName";
                var dtSorted = dataView.ToTable();
                dtSorted.Columns.Add("Sort", typeof(int));
                var j = -1000;
                var sFilterKeyword = MyGlobal.GetStringBetween(condition, "'", "'").Replace("*", "").ToUpper();

                for (var i = 0; i < dtSorted.Rows.Count; i++)
                {
                    if (dtSorted.Rows[i]["SchemaName"].ToString().ToUpper().Substring(0, sFilterKeyword.Length) == sFilterKeyword)
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
                dataView.Sort = "Sort, SchemaName";
                dtSorted = dataView.ToTable();
                dtSorted.Columns.Remove("Sort");
                #endregion

                c1GridView.Splits[0].RecordSelectors = false;
                c1GridView.DataSource = dtSorted;

                return dtSorted.Rows.Count;
            }
            catch (Exception)
            {
                return 0; //按下 Ctrl+J 可能會進到這個例外錯誤
            }
        }

        private void cboView_Enter(object sender, EventArgs e)
        {
            cboView_EnterOrFocus();
        }

        private void cboView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    c1GridView.Focus();
                    e.SuppressKeyPress = false;
                    break;
                case Keys.Delete:
                    _bKeyPressDelete = true; //不能在此處理 Delete 按鍵，因為此時的 cboView.Text 的值是按下 Delete 鍵之前的！
                    break;
            }
        }

        private void cboView_KeyPress(object sender, KeyPressEventArgs e)
        {
            _bKeypressComboBox = true;
        }

        private void cboView_KeyUp(object sender, KeyEventArgs e)
        {
            if (_bKeyPressTab)
            {
                _bKeyPressTab = false; //Tab 會回到這裡，不重複處理！
            }
            else if (_bKeyPressESC)
            {
                _bKeyPressESC = false; //ESC 會回到這裡，不重複處理！
            }
            else
            {
                var iRowCount = c1GridView_Filter(cboView.Text);

                if (iRowCount > 0)
                {
                    ResizeACGrid(c1GridView, iRowCount, 0);
                    c1GridView.Visible = true;
                }
                else
                {
                    c1GridView.Visible = false;
                }
            }
        }

        private void cboView_Leave(object sender, EventArgs e)
        {
            if (!c1GridView.Focused)
            {
                c1GridView.Visible = false;
            }
        }

        private void cboView_TextChanged(object sender, EventArgs e)
        {
            if (cboView.Items.Count == 0)
            {
                return;
            }

            if (_bKeyPressDelete)
            {
                _bKeyPressDelete = false;
            }
            else
            {
                return;
            }

            var iRowCount = c1GridView_Filter(cboView.Text);

            if (iRowCount > 0)
            {
                ResizeACGrid(c1GridView, iRowCount, 0);
                c1GridView.Visible = true;
            }
            else
            {
                c1GridView.Visible = false;
            }
        }

        private void c1GridView_Leave(object sender, EventArgs e)
        {
            c1GridView.Visible = false;
        }

        private void c1GridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13 && e.KeyChar != 9)
            {
                return;
            }

            _bKeyPressTab = true;
            c1GridView_PasteColumnName(); //c1GridView_KeyPress
            _bKeyPressTab = true;
        }

        private void c1GridView_PasteColumnName()
        {
            var sCellText = c1GridView[c1GridView.Row, 0].ToString();

            cboView.Text = sCellText;
            c1GridView.Visible = false;
            cboView.Focus();
            _bKeyPressTab = true;
            rdoView.Checked = true;

            if (!string.IsNullOrEmpty(cboTable.Text))
            {
                sSchemaType = "Views";
                btnSelect.PerformClick();
            }
        }

        private void c1GridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _bKeyPressTab = true;
            c1GridView_PasteColumnName(); //c1GridView_MouseDoubleClick
            _bKeyPressTab = false;
        }

        private void frmGenerateSQL_MouseClick(object sender, MouseEventArgs e)
        {
            c1GridTable.Visible = false;
            c1GridView.Visible = false;
        }

        private void GroupBoxe_FocusedOrClick(object sender, EventArgs e)
        {
            c1GridTable.Visible = false;
            c1GridView.Visible = false;
        }
    }
}