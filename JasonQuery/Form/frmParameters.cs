using System;
using System.Data;
using JasonLibrary;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using System.Drawing;
using System.Collections.Generic;
using JasonLibrary.Class;
using JasonLibrary.Stylers;
using ScintillaNET;

namespace JasonQuery
{
    public partial class frmParameters : Form
    {
        private List<string> _lstGridHeaderParameters = new List<string>();
        private string _sLangText = "";
        private DataTable _dtParameters = new DataTable();
        private string _sColValue = "Value";
        private string _sParametersMapping = ""; //此變數要呈現在 SQL Hisotry 的訊息欄
        private string _sParametersPositionMapping = ""; //此變數要回傳給 frmQuery，供錯誤定位用的

        private enum eParameters
        {
            NameOriginal = 0,
            Name,
            Value,
            Type,
            Help
        }

        private enum eType
        {
            String = 0,
            Number,
            Custom
        }

        public string sSqlText { get; set; }
        public string sParameters { get; set; }
        public string sParametersWithPosition { get; set; }
        public string sAccessibleDescription { get; set; }

        public frmParameters()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MyGlobal.ApplyLanguageInfo(this, false);

            SetSqlText(sSqlText); //將 SQL 寫入 editor 中
            editorSql.Tag = sSqlText;

            _lstGridHeaderParameters = new List<string>();

            _lstGridHeaderParameters.Add("Name_Original");
            _sLangText = MyGlobal.GetLanguageString("Name", "form", "frmParameters", "gridheader", "Name", "Text");
            _lstGridHeaderParameters.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Value", "form", "frmParameters", "gridheader", "Value", "Text");
            _lstGridHeaderParameters.Add(_sLangText);
            _sColValue = _sLangText;
            _sLangText = MyGlobal.GetLanguageString("Type", "form", Name, "gridheader", "Type", "Text");
            _lstGridHeaderParameters.Add(_sLangText);
            _lstGridHeaderParameters.Add("?"); //固定顯示 ？

            _dtParameters = new DataTable();

            _dtParameters.Columns.Add(_lstGridHeaderParameters[(int)eParameters.NameOriginal]);
            _dtParameters.Columns.Add(_lstGridHeaderParameters[(int)eParameters.Name]);
            _dtParameters.Columns.Add(_lstGridHeaderParameters[(int)eParameters.Value]);
            _dtParameters.Columns.Add(_lstGridHeaderParameters[(int)eParameters.Type]);
            _dtParameters.Columns.Add(_lstGridHeaderParameters[(int)eParameters.Help]);

            var sParameter = sParameters.Split(new[] { "`" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var t in sParameter)
            {
                var rowParameters = _dtParameters.NewRow();
                rowParameters[(int)eParameters.NameOriginal] = t;
                rowParameters[(int)eParameters.Name] = t;

                var sValue = "";
                var sType = "0";
                var dtTemp = DBCommon.ExecQuery("SELECT AttributeValue, AttributeText FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'ParametersList' AND AttributeName = '" + t.Replace("'", "''") + "' ORDER BY AttributeDate DESC");

                if (dtTemp.Rows.Count > 0)
                {
                    //取得同一個變數，前一次的設定值
                    sValue = dtTemp.Rows[0]["AttributeValue"].ToString();
                    sType = dtTemp.Rows[0]["AttributeText"].ToString();

                    if ("`0`1`2`".Contains("`" + sType + "`") == false)
                    {
                        sType = "0"; //預設為字串
                    }
                }

                rowParameters[(int)eParameters.Value] = sValue;
                rowParameters[(int)eParameters.Type] = sType;
                rowParameters[(int)eParameters.Help] = "";

                _dtParameters.Rows.Add(rowParameters);
            }

            c1GridParameters.DataSource = _dtParameters;
            c1GridParameters.Splits[0].DisplayColumns[(int)eParameters.Help].FilterButton = false;
            c1GridParameters.Splits[0].DisplayColumns[(int)eParameters.Help].Button = true;
            c1GridParameters.Splits[0].DisplayColumns[(int)eParameters.Help].ButtonAlways = true;
            c1GridParameters.ButtonClick += c1GridParameters_ButtonClick;
            c1GridParameters.Columns[(int)eParameters.Help].ButtonPicture = picHelp.Image; //Image.FromFile(@"D:\Downloads\JasonQuery\Image\16x16\icons8-help-16.png");

            SetComboBox(); //Form_Load
            c1GridParameters.Refresh(); //Form_Load
            GridVisualStyle(); //Form_Load, Normal
            GridFontAndBackgroundColor(); //Form_Load

            foreach (C1DisplayColumn col in c1GridParameters.Splits[0].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 2000;
                }

                if (col.Name == "Name_Original")
                {
                    col.Visible = false;
                    col.Frozen = true;
                }
                else if (col.Name == _sColValue)
                {
                    col.Width = 250;
                }
                else if (col.Name == "?") //說明欄，只顯示問號按鈕
                {
                    col.Width = 17;
                }
                else
                {
                    col.Width += 10;
                }
            }

            ApplySqlStyler();

            panel1.Location = new Point(lblPreview.Left + lblPreview.Width, panel1.Top);

            c1GridParameters.Col = (int)eParameters.Value;
            c1GridParameters.Row = 0;
        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            MyGlobal.UpdateSetting("GlobalConfig", "ParametersFormWidth", Size.Width.ToString());
            MyGlobal.UpdateSetting("GlobalConfig", "ParametersFormHeight", Size.Height.ToString());
        }

        private void SetComboBox()
        {
            //Type 欄位以 ComboBox 動態呈現
            var items = c1GridParameters.Columns[_lstGridHeaderParameters[(int)eParameters.Type]].ValueItems;

            items.Translate = true;
            items.Presentation = PresentationEnum.ComboBox;
            items.Validate = true;

            items.Values.Clear();
            _sLangText = MyGlobal.GetLanguageString("String", "form", "frmParameters", "object", "String", "Text");
            items.Values.Add(new ValueItem("0", _sLangText));
            _sLangText = MyGlobal.GetLanguageString("Number", "form", "frmParameters", "object", "Number", "Text");
            items.Values.Add(new ValueItem("1", _sLangText));
            _sLangText = MyGlobal.GetLanguageString("Custom", "form", "frmParameters", "object", "Custom", "Text");
            items.Values.Add(new ValueItem("2", _sLangText));

            //指定哪一個 Column 要套用 FetchCellStyle
            c1GridParameters.Splits[0].DisplayColumns[(int)eParameters.Name].FetchStyle = true;
            c1GridParameters.Splits[0].DisplayColumns[_lstGridHeaderParameters[(int)eParameters.Value]].FetchStyle = true;
            c1GridParameters.Splits[0].DisplayColumns[_lstGridHeaderParameters[(int)eParameters.Help]].FetchStyle = true;
        }

        private static void UpdateParametersList(string sName, string sValue, string sType)
        {
            var dtParameters = DBCommon.ExecQuery("SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'ParametersList' AND AttributeName = '" + sName + "'");

            if (dtParameters.Rows.Count > 0)
            {
                DBCommon.ExecNonQuery("UPDATE SystemConfig SET AttributeValue = '" + sValue + "', AttributeText = '" + sType + "', AttributeDate = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'ParametersList' AND AttributeName = '" + sName + "'");
            }
            else
            {
                DBCommon.ExecNonQuery("INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue, AttributeText, AttributeDate) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'ParametersList', '" + sName + "', '" + sValue + "', '" + sType + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')");
            }
        }

        private void c1GridParameters_FetchCellStyle(object sender, FetchCellStyleEventArgs e)
        {
            switch (e.Col)
            {
                case (int)eParameters.Name:
                    e.CellStyle.Locked = true; //變數名稱，此欄 Lock！
                    break;
                case (int)eParameters.Help:
                    e.CellStyle.Locked = true; //Help，此欄 Lock！
                    break;
                case (int)eParameters.Value:
                {
                    e.CellStyle.BackColor = Color.LightYellow;

                    if (MyLibrary.bDarkMode)
                    {
                        e.CellStyle.BackColor = ColorTranslator.FromHtml("#0F243E");
                        e.CellStyle.ForeColor = Color.White;
                    }

                    break;
                }
            }
        }

        private void GridVisualStyle()
        {
            var sStyle = MyLibrary.bDarkMode ? "Office 2010 Black" : MyLibrary.sGridVisualStyle;

            switch (sStyle)
            {
                case "Office 2007 Blue":
                    c1GridParameters.VisualStyle = VisualStyle.Office2007Blue;

                    break;
                case "Office 2007 Silver":
                    c1GridParameters.VisualStyle = VisualStyle.Office2007Silver;

                    break;
                case "Office 2007 Black":
                    c1GridParameters.VisualStyle = VisualStyle.Office2007Black;

                    break;
                case "Office 2010 Blue":
                    c1GridParameters.VisualStyle = VisualStyle.Office2010Blue;

                    break;
                case "Office 2010 Silver":
                    c1GridParameters.VisualStyle = VisualStyle.Office2010Silver;

                    break;
                case "Office 2010 Black":
                    c1GridParameters.VisualStyle = VisualStyle.Office2010Black;

                    break;
                default:
                    c1GridParameters.VisualStyle = VisualStyle.Office2010Blue;

                    break;
            }

            c1GridParameters.Splits[0].ColumnCaptionHeight = 20;
        }

        private void GridFontAndBackgroundColor()
        {
            const float fontSize = 10;

            //字型 + 字體大小
            c1GridParameters.Font = new Font(MyLibrary.sGridFontName, fontSize, FontStyle.Regular, GraphicsUnit.Point);

            c1GridParameters.OddRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowForeColor);
            c1GridParameters.OddRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowBackColor);
            c1GridParameters.EvenRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowForeColor);
            c1GridParameters.EvenRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);

            //Grid's 選取顏色
            c1GridParameters.SelectedStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedForeColor);
            c1GridParameters.SelectedStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedBackColor);

            if (!MyLibrary.bDarkMode)
            {
                return;
            }

            MyGlobal.SetGridVisualStyle(c1GridParameters, fontSize);
            c1GridParameters.BackColor = ColorTranslator.FromHtml("#2D2D30"); //Office 2010 Black

            c1GridParameters.BorderColor = Color.White;
            c1GridParameters.HeadingStyle.Borders.Color = Color.White;
            c1GridParameters.HeadingStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridHeadingForeColor);
            c1GridParameters.RowDivider.Color = Color.White;
            c1GridParameters.Font = new Font(MyLibrary.sGridFontName, Convert.ToSingle(MyLibrary.sGridFontSize), FontStyle.Regular, GraphicsUnit.Point);
            c1GridParameters.HeadingStyle.Font = new Font(MyLibrary.sGridFontName, Convert.ToSingle(MyLibrary.sGridFontSize), FontStyle.Regular, GraphicsUnit.Point);
            c1GridParameters.MarqueeStyle = MarqueeEnum.FloatingEditor;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var dt = (DataTable)c1GridParameters.DataSource;
            var parts = sParametersWithPosition.Split(new[] { "`" }, StringSplitOptions.RemoveEmptyEntries);

            editorSql.ReadOnly = false;
            editorSql.Text = editorSql.Tag.ToString();

            _sParametersMapping = "";
            _sParametersPositionMapping = sParametersWithPosition;

            //從最後一個變數開始置換指定值 (位置才不會因為「變數的長度 vs 指定值的長度」而改變)
            for (var i = parts.Length - 1; i >= 0; i--)
            {
                var sName = parts[i].Split('|')[0];
                int iPosition = Convert.ToInt32(parts[i].Split('|')[1]);

                var rows = dt.Select(_lstGridHeaderParameters[(int)eParameters.NameOriginal] + " = '" + sName + "'");

                if (rows.Length <= 0)
                {
                    continue;
                }

                var sType = rows[0][(int)eParameters.Type].ToString();
                var sValue = rows[0][(int)eParameters.Value].ToString();

                if (string.IsNullOrEmpty(sValue))
                {
                    sValue = sType == ((int)eType.Number).ToString() ? "0" : "''";
                }
                else if (sType == ((int)eType.String).ToString())
                {
                    sValue = "'" + sValue.Replace("'", "''") + "'";
                }
                else if (sType == ((int)eType.Number).ToString())
                {
                    sValue = MyGlobal.IsNumeric(sValue) ? sValue : "0";
                }

                _sParametersMapping += sName + " => " + sValue + "\r\n";
                _sParametersPositionMapping = _sParametersPositionMapping.Replace("`" + sName + "|" + iPosition + "`", "`" + sName + "|" + iPosition + "|" + sValue + "`");

                editorSql.SelectionStart = iPosition; //重新選取內容
                editorSql.SelectionEnd = iPosition + sName.Length;

                editorSql.ReplaceSelection(sValue); //置換內容
            }

            //重新取得對應的顯示順序
            parts = _sParametersMapping.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            _sParametersMapping = "";

            for (var i = parts.Length - 1; i >= 0; i--)
            {
                _sParametersMapping += parts[i] + "\r\n";
            }

            if (!string.IsNullOrEmpty(_sParametersMapping))
            {
                _sParametersMapping = _sParametersMapping.Substring(0, _sParametersMapping.Length - 2);
            }

            editorSql.SelectionStart = 0;
            editorSql.SelectionEnd = 0;
            editorSql.ScrollCaret();

            editorSql.ReadOnly = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //儲存使用者的變數定義
            var dt = (DataTable)c1GridParameters.DataSource;

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var sName = dt.Rows[i][(int)eParameters.Name].ToString();
                var sValue = dt.Rows[i][(int)eParameters.Value].ToString();
                var sType = dt.Rows[i][(int)eParameters.Type].ToString();

                if (!string.IsNullOrEmpty(sValue) && sName != sValue)
                {
                    UpdateParametersList(sName.Replace("'", "''"), sValue.Replace("'", "''"), sType);
                }
            }

            //預覽並傳回要執行的 SQL 指令
            btnPreview.PerformClick();
            MyGlobal.sGlobalTemp = "parameters" + MyGlobal.sSeparator + sAccessibleDescription + MyGlobal.sSeparator7 + editorSql.Text + MyGlobal.sSeparator7 + _sParametersMapping + MyGlobal.sSeparator7 + _sParametersPositionMapping.Replace(MyGlobal.sSeparator, "!!!!!!!");
            Close();
        }

        private void ApplySqlStyler()
        {
            editorSql.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
            editorSql.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editorSql.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));

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

            editorSql.Styler = new SqlStyler(); //sqlstyler()：變更關鍵字、顏色
        }

        private void SetSqlText(string sText)
        {
            editorSql.ReadOnly = false;
            editorSql.Text = sText;
            editorSql.ReadOnly = true;
        }

        private static void c1GridParameters_ButtonClick(object sender, ColEventArgs e)
        {
            if (e.ColIndex != (int) eParameters.Help)
            {
                return;
            }

            using (var myForm = new frmHelpParametersType())
            {
                myForm.ShowDialog();
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            editorSql.SelectionStart = 0;
            editorSql.SelectionEnd = editorSql.Text.Length;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject("", false);
            editorSql.Copy(CopyFormat.Text | CopyFormat.Rtf | CopyFormat.Html);
        }

        private void btnWordWrap_Click(object sender, EventArgs e)
        {
            if (editorSql.WrapMode == WrapMode.Word)
            {
                btnWordWrap.Visible = true;
                btnWordWrap2.Visible = false;
                editorSql.WrapMode = WrapMode.None;
            }
            else
            {
                btnWordWrap.Visible = false;
                btnWordWrap2.Visible = true;
                editorSql.WrapMode = WrapMode.Word;
                editorSql.WrapVisualFlags = (WrapVisualFlags.Start) | (WrapVisualFlags.End) | (WrapVisualFlags.Margin);
            }

            //加上以下這個指令，取消 Word Wrap 後，Focus 才不會跑到最底部！
            editorSql.ScrollCaret();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var bHandled = false;

            switch (keyData)
            {
                case Keys.Escape:
                    Close();
                    bHandled = true;
                    break;
            }

            return bHandled;
        }
    }
}