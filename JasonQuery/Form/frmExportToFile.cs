using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using JasonLibrary;
using System.Diagnostics;
using C1.C1Excel;
using C1.Win.C1TrueDBGrid;
using System.Reflection;
using System.Runtime.InteropServices;
using JasonLibrary.Class;

namespace JasonQuery
{
    public partial class frmExportToFile : Form
    {
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        private bool _bBusy = false;
        private string _sLangText = "";
        private ToolTip _toolTip1 = new ToolTip();
        private bool _bProgressCancel = false;
        public DataTable dtData { get; set; }
        public DataTable dtSchemaTable { get; set; }
        public string sFontName { get; set; } = "";
        public float fFontSize { get; set; } = 11;
        public string sSheetName { get; set; } = "";
        public string sTitle { get; set; } = "";

        public frmExportToFile()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (MyLibrary.bDarkMode)
            {
                editorPreview.SetSelectionBackColor(true, ColorTranslator.FromHtml("#ADD8E6")); //LightBlue
                editorPreview.CaretLineBackColor = ColorTranslator.FromHtml("#FFFFE0"); //LightYellow
            }
            else
            {
                editorPreview.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
                editorPreview.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            }

            //Begin: 設定 ComboBox Color
            var colorType = typeof(Color);
            var propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);

            foreach (var c in propInfoList)
            {
                if ((!c.Name.StartsWith("Light") || "`LightBlue`LightGreen`LightSalmon`Plum`LightSteelBlue`LightGoldenrodYellow`LightSlateGray`".Contains("`" + c.Name + "`")) && "`YellowGreen`Yellow`Gold`Orange`Cyan`White`Blue`Green`".Contains("`" + c.Name + "`") != true)
                {
                    continue;
                }

                cboHeadingBackColor.Items.Add(c.Name);
                cboEvenRowBackColor.Items.Add(c.Name);
                cboOddRowBackColor.Items.Add(c.Name);
            }
            //End: 設定 ComboBox Color

            MyGlobal.ApplyLanguageInfo(this, false, false);

            Text += string.IsNullOrEmpty(sTitle) ? "" : " - " + sTitle;

            CreateVisualStyleInfo(); //Form_Load

            sFontName = sFontName;
            fFontSize = fFontSize;

            cboFilename.Location = new Point(lblFilename.Left + lblFilename.Width, cboFilename.Top);
            btnBrowseFile.Location = new Point(cboFilename.Left + cboFilename.Width + 5, btnBrowseFile.Top);
            cboSaveAsType.Location = new Point(lblSaveAsType.Left + lblSaveAsType.Width, cboSaveAsType.Top);
            txtWorksheetName.Location = new Point(lblWorksheetName.Left + lblWorksheetName.Width, txtWorksheetName.Top);
            lblEncoding.Location = new Point(cboSaveAsType.Left + cboSaveAsType.Width + 50, lblEncoding.Top);
            cboEncoding.Location = new Point(lblEncoding.Left + lblEncoding.Width, cboEncoding.Top);
            lblCSVDelimiters.Location = new Point(cboEncoding.Left + cboEncoding.Width + 50, lblCSVDelimiters.Top);
            cboCSVDelimiters.Location = new Point(lblCSVDelimiters.Left + lblCSVDelimiters.Width, cboCSVDelimiters.Top);
            chkConvertCRLF.Location = new Point(cboEncoding.Left + cboEncoding.Width + 50, chkConvertCRLF.Top);
            rdoCustom.Location = new Point(rdoDefault.Left + rdoDefault.Width + 20, rdoCustom.Top);
            chkColumnResize.Location = new Point(chkAutoOpenExportedFile.Left + chkAutoOpenExportedFile.Width + 30, chkColumnResize.Top);
            cboHeadingBackColor.Location = new Point(lblHeadingBackColor.Left + lblHeadingBackColor.Width, cboHeadingBackColor.Top);
            cboEvenRowBackColor.Location = new Point(lblEvenRowBackColor.Left + lblEvenRowBackColor.Width, cboEvenRowBackColor.Top);
            cboOddRowBackColor.Location = new Point(lblOddRowBackColor.Left + lblOddRowBackColor.Width, cboOddRowBackColor.Top);

            cboGridFontName.Location = new Point(lblFontName.Left + lblFontName.Width, cboGridFontName.Top);
            cboGridFontSize.Location = new Point(lblFontSize.Left + lblFontSize.Width, cboGridFontSize.Top);
            cboGridRowHeight.Location = new Point(lblRowHeight.Left + lblRowHeight.Width, cboGridRowHeight.Top);

            LoadFilenameList();

            cboSaveAsType.Text = MyLibrary.sGridExcelSaveAsType;
            txtWorksheetName.Text = string.IsNullOrEmpty(sSheetName) ? MyLibrary.sGridExcelWorksheetName : sSheetName;
            chkConvertCRLF.Checked = MyLibrary.bGridConvertCRLF;
            chkAutoOpenExportedFile.Checked = MyLibrary.bGridExcelAutoOpen;
            chkColumnResize.Checked = MyLibrary.bGridExcelAutoColumnResize;

            MyGlobal.SetC1ComboBoxItemsFromDictionary(cboCSVDelimiters, MyGlobal.dicCSVDelimiters);
            cboCSVDelimiters.Text = MyGlobal.sCSVDelimiters; //MyLibrary.sGridCSVDelimiters; //MyGlobal.GetValueFromDictionary(MyGlobal.dicCSVDelimiters, MyLibrary.sGridCSVDelimiters);

            cboEncoding.Text = MyLibrary.sGridEncoding;

            if (cboSaveAsType.Text.StartsWith("Excel") && dtData.Rows.Count > 65535)
            {
                cboSaveAsType.SelectedIndex = 1;
            }

            cboFilename.Text = MyLibrary.sGridExcelFilename;
            cboFilename.Tag = cboFilename.Text;

            rdoCustom.Checked = true;

            cboHeadingBackColor.Invalidate();
            cboEvenRowBackColor.Invalidate();
            cboOddRowBackColor.Invalidate();

            cboHeadingBackColor.Text = MyLibrary.sGridExcelHeadingBackColor;
            cboEvenRowBackColor.Text = MyLibrary.sGridExcelEvenRowBackColor;
            cboOddRowBackColor.Text = MyLibrary.sGridExcelOddRowBackColor;
            
            cboGridFontName.Text = MyLibrary.sGridExcelFontName;
            cboGridFontSize.Text = MyLibrary.sGridExcelFontSize;
            cboGridRowHeight.Text = MyLibrary.sGridExcelRowHeight;

            ApplyVisualStyle(); //Form_Load
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_bBusy)
            {
                return;
            }

            Close();
        }

        private void txtWorksheetName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWorksheetName.Text.Trim()))
            {
                txtWorksheetName.Text = @"data";
            }
        }

        private void CreateVisualStyleInfo()
        {
            var sShowAs = MyLibrary.sGridNullShowAs.ToUpper() == "NONE" ? "" : MyLibrary.sGridNullShowAs;

            c1GridVisualStyle.DataSource = dtData;
            c1GridVisualStyle.Splits[0].ColumnCaptionHeight = 25;
            c1GridVisualStyle.RowHeight = 25;

            //Grid's 選取顏色
            c1GridVisualStyle.SelectedStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedForeColor);
            c1GridVisualStyle.SelectedStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedBackColor);

            c1GridVisualStyle.HeadingStyle.ForeColor = ColorTranslator.FromHtml("#000000");

            foreach (C1DisplayColumn col in c1GridVisualStyle.Splits[0].DisplayColumns)
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

            //變更 Cell = NULL 的前景顏色 (不能使用 FetchCellStyle 事件，因為會變成整列都變色)
            if (string.IsNullOrWhiteSpace(sShowAs))
            {
                return;
            }

            var s1 = new Style {ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridNullShowColor)};

            for (var i = 0; i < c1GridVisualStyle.Columns.Count; i++)
            {
                c1GridVisualStyle.Splits[0].DisplayColumns[i].AddRegexCellStyle(CellStyleFlag.AllCells, s1, sShowAs);
            }
        }

        private void btnExportAndClose_Click(object sender, EventArgs e)
        {
            if (_bBusy)
            {
                return;
            }

            if (string.IsNullOrEmpty(cboFilename.Text.Trim()))
            {
                _sLangText = MyGlobal.GetLanguageString("Please select the name of the exported file!", "form", Name, "msg", "SelectSavedFilename", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnBrowseFile.Focus();
                return;
            }

            btnExport.PerformClick();
            Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_bBusy)
            {
                return;
            }

            ExportToFile(false);
        }

        private void ExportToFile(bool bPreview = false)
        {
            if (bPreview)
            {
                if (cboSaveAsType.Text.StartsWith("Excel"))
                {
                    c1GridVisualStyle.Visible = true;
                    editorPreview.Visible = false;
                    return;
                }

                c1GridVisualStyle.Visible = false;
                editorPreview.Visible = true;
            }

            var iPreview = 5;
            var sPreviewResult = "";
            _bProgressCancel = false;

            var i = 0;
            var sStep = "";
            var bExportResult = true;
            var sPath = "";
            var sTryAgain = MyGlobal.GetLanguageString("Please try again!", "Global", "Global", "msg", "PleaseTryAgain", "Text");
            var sSaveAsType = cboSaveAsType.Text.Substring(0, cboSaveAsType.Text.IndexOf(" ", 0, StringComparison.Ordinal));

            lblInfo.Text = "";
            lblInfo.ForeColor = Color.Green;

            switch (bPreview)
            {
                case false when string.IsNullOrEmpty(cboFilename.Text.Trim()):
                    _sLangText = MyGlobal.GetLanguageString("Please select the name of the exported file!", "form", Name, "msg", "SelectSavedFilename", "Text");
                    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBrowseFile.Focus();
                    return;
                case false:
                    try
                    {
                        sPath = Path.GetDirectoryName(cboFilename.Text); //路徑不存在不會有錯誤；路徑不合法才會引發 catch 錯誤
                    }
                    catch (Exception ex)
                    {
                        _sLangText = MyGlobal.GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text");
                        MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnBrowseFile.Focus();
                        return;
                    }

                    break;
            }

            switch (bPreview)
            {
                case false when string.IsNullOrEmpty(sPath):
                    {
                        _sLangText = MyGlobal.GetLanguageString("Invalid path!", "form", Name, "msg", "InvalidPath", "Text");
                        MessageBox.Show(_sLangText + "\r\n\r\n" + cboFilename.Text, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnBrowseFile.Focus();
                        return;
                    }
                case false when File.Exists(cboFilename.Text):
                    {
                        var sTemp = MyGlobal.GetLanguageString("Confirm Save As", "Global", "Global", "msg", "ConfirmSaveAs", "Text");
                        _sLangText = MyGlobal.GetLanguageString("The file you are trying to save already exists!", "Global", "Global", "msg", "TheFileExists", "Text");
                        _sLangText += "\r\n" + cboFilename.Text + "\r\n\r\n" + MyGlobal.GetLanguageString("Do you want to replace it?", "Global", "Global", "msg", "ReplaceFile", "Text");

                        if (MessageBox.Show(_sLangText, sTemp, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        break;
                    }
            }

            if (bPreview == false)
            {
                try
                {
                    sStep = "CreateFolder";
                    Directory.CreateDirectory(sPath ?? string.Empty);
                    sStep = "SaveFile";
                    TextEngine.WriteContentToFile("", cboFilename.Text, TextEncode.Default);
                    File.Delete(cboFilename.Text);
                }
                catch (Exception ex)
                {
                    _sLangText = MyGlobal.GetLanguageString("An error occurred while saving {FileType} file.", "Global", "Global", "msg", "ErrorToSaveFile", "Text");
                    _sLangText = _sLangText.Replace("{FileType}", sSaveAsType);
                    MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message + (sStep == "CreateFolder" ? "" : "\r\n\r\n" + sTryAgain), @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            Application.UseWaitCursor = true;
            _bBusy = true;

            //以上都沒有錯誤，更新 ComboBox 下拉清單
            if (bPreview == false)
            {
                UpdateFilenameList();
            }

            if (!cboSaveAsType.Text.StartsWith("Excel"))
            {
                if (bPreview == false)
                {
                    btnCancel.Visible = true;

                    progressBar1.Visible = true;
                    progressBar1.Value = 0;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = dtData.Rows.Count;
                }

                try
                {
                    string sTemp1;

                    if (cboSaveAsType.Text.StartsWith("CSV"))
                    {
                        string sCsvData = "", sColumnName = "", sFieldSeparator = cboCSVDelimiters.Tag.ToString();
                        
                        #region //產出 CSV 檔案
                        iPreview = bPreview ? Math.Min(iPreview, dtData.Rows.Count) : dtData.Rows.Count;

                        for (var iRow = 0; iRow < iPreview; iRow++)
                        {
                            var vr = c1GridVisualStyle.Splits[0].Rows[iRow];

                            foreach (C1DataColumn col1 in c1GridVisualStyle.Columns)
                            {
                                if (col1.Caption.IndexOf("\r\n", 0, StringComparison.Ordinal) != -1)
                                {
                                    sTemp1 = col1.Caption.Replace("\r\n", "\n").Split('\n')[0];
                                }
                                else if (col1.Caption.IndexOf("\r", 0, StringComparison.Ordinal) != -1)
                                {
                                    sTemp1 = col1.Caption.Split('\r')[0];
                                }
                                else
                                {
                                    sTemp1 = col1.Caption;
                                }

                                if (i == 0)
                                {
                                    //收集 Column Name
                                    sColumnName += sTemp1 + sFieldSeparator;
                                }

                                if (c1GridVisualStyle.Columns[col1.Caption].CellValue(iRow) == DBNull.Value)
                                {
                                    sCsvData += "null" + sFieldSeparator;
                                }
                                else
                                {
                                    sCsvData += col1.CellText(vr.DataRowIndex) + sFieldSeparator;
                                }

                                if (_bProgressCancel)
                                {
                                    break;
                                }

                                Application.DoEvents();
                            }

                            if (!string.IsNullOrEmpty(sCsvData))
                            {
                                sCsvData = sCsvData.Substring(0, sCsvData.Length - sFieldSeparator.Length) + "\r\n";
                            }

                            progressBar1.Value = iRow;

                            if (bPreview == false)
                            {
                                TextEngine.WriteContentToFile(i > 0 ? sCsvData : sColumnName.Substring(0, sColumnName.Length - sFieldSeparator.Length) + "\r\n" + sCsvData, cboFilename.Text, cboEncoding.Text, FileMode.Append); //此處用 TextEncode.Default 存檔，Excel 比較不會出現異常 (比如分割位置錯誤)
                            }
                            else
                            {
                                sPreviewResult += i > 0 ? sCsvData : sColumnName.Substring(0, sColumnName.Length - sFieldSeparator.Length) + "\r\n" + sCsvData;
                            }

                            sCsvData = "";
                            i++;

                            if (_bProgressCancel)
                            {
                                break;
                            }
                        }

                        btnCancel.Visible = false;
                        progressBar1.Visible = false;

                        if (_bProgressCancel)
                        {
                            _sLangText = MyGlobal.GetLanguageString("This operation has been cancelled.", "Global", "Global", "msg", "CancelByUser", "Text");
                            lblInfo.Text = _sLangText;
                            _bBusy = false;
                            Application.UseWaitCursor = false;
                            return;
                        }

                        if (bPreview)
                        {
                            editorPreview.Text = sPreviewResult;
                        }

                        #endregion
                    }
                    else if (cboSaveAsType.Text.StartsWith("PDF"))
                    {
                        c1GridVisualStyle.ExportToPDF(cboFilename.Text);
                    }
                    else if (cboSaveAsType.Text.StartsWith("JSON"))
                    {
                        string sJsonData = "";

                        #region //產出 JSON 檔案
                        iPreview = bPreview ? Math.Min(iPreview, dtData.Rows.Count) : dtData.Rows.Count;

                        for (var iRow = 0; iRow < iPreview; iRow++)
                        {
                            var vr = c1GridVisualStyle.Splits[0].Rows[iRow];

                            if (iRow == 0)
                            {
                                sJsonData = "[\r\n";

                                if (bPreview)
                                {
                                    sPreviewResult += sJsonData;
                                }
                                else
                                {
                                    TextEngine.WriteContentToFile(sJsonData, cboFilename.Text, cboEncoding.Text, FileMode.Append);
                                }
                            }

                            sJsonData = "  {\r\n";
                            var j = 0;

                            foreach (C1DataColumn col1 in c1GridVisualStyle.Columns)
                            {
                                j++;
                                var sDataType = "";

                                if (col1.Caption.IndexOf("\r\n", 0, StringComparison.Ordinal) != -1)
                                {
                                    sTemp1 = col1.Caption.Replace("\r\n", "\n").Split('\n')[0];
                                }
                                else if (col1.Caption.IndexOf("\r", 0, StringComparison.Ordinal) != -1)
                                {
                                    sTemp1 = col1.Caption.Split('\r')[0];
                                }
                                else
                                {
                                    sTemp1 = col1.Caption;
                                }

                                var sColumnName = sTemp1;
                                var sColumnDataType = (col1.Tag ?? string.Empty).ToString();

                                if (string.IsNullOrEmpty(sColumnDataType))
                                {
                                    sColumnDataType = dtData.Columns[col1.Caption].DataType.ToString().Replace("System.", "");
                                }

                                try
                                {
                                    if (dtSchemaTable.Rows.Count == 0) //Raw Data Mode
                                    {
                                        sDataType = sColumnDataType;
                                    }
                                    else
                                    {
                                        var dtRow = dtSchemaTable.Select("ColumnName = '" + sColumnName.Replace("'", "''") + "'");

                                        if (dtRow.Length > 0)
                                        {
                                            switch (MyGlobal.sDataSource)
                                            {
                                                case "Oracle":
                                                    sColumnDataType = MyGlobal.GetDataTypeFormat_Oracle(dtRow, out sDataType).ToLower();
                                                    break;
                                                case "PostgreSQL":
                                                    sColumnDataType = MyGlobal.GetDataTypeFormat_PostgreSQL(dtRow, out sDataType).ToLower();
                                                    break;
                                                case "SQL Server":
                                                    sColumnDataType = MyGlobal.GetDataTypeFormat_SQLServer(dtRow, out sDataType).ToLower();
                                                    break;
                                                case "MySQL":
                                                    sColumnDataType = MyGlobal.GetDataTypeFormat_MySQL(dtRow, out sDataType).ToLower();
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            sDataType = sColumnDataType;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    sDataType = sColumnDataType;
                                }

                                string sJsonDataValue = "    \"" + sColumnName + "\": ";

                                if (c1GridVisualStyle.Columns[col1.Caption].CellValue(iRow) == DBNull.Value)
                                {
                                    sJsonDataValue += "null,\r\n";
                                }
                                else
                                {
                                    switch (sColumnDataType)
                                    {
                                        case "int":
                                        case "integer":
                                        case "long":
                                        case "number":
                                        case "bigint":
                                        case "smallint":
                                        case "real":
                                        case "double":
                                        case "double precision":
                                        case "numeric":
                                        case "decimal":
                                        case "bit":
                                        case "tinyint":
                                        case "unsigned tinyint":
                                        case "mediumint":
                                        case "unsigned smallint":
                                        case "float":
                                        case "boolean":
                                        case "money":
                                        case "int4range":
                                        case "int8range":
                                        case "numrange":
                                        case "int32":
                                        case "oid":
                                            sJsonDataValue += col1.CellText(vr.DataRowIndex) + ",\r\n";
                                            break;
                                        case "integer[]":
                                        case "int32[]":
                                        case "double precision[]":
                                        case "money[]":
                                        case "numeric[]":
                                        case "nummultirange[]":
                                        case "int4range[]":
                                        case "int8range[]":
                                        case "int4multirange[]":
                                        case "int8multirange[]":
                                        case "numrange[]":
                                        case "oid[]":
                                        case "real[]":
                                        case "smallint[]":
                                            sJsonDataValue += "{ " + col1.CellText(vr.DataRowIndex) + " },\r\n";
                                            break;
                                        case "boolean[]":
                                            sJsonDataValue += col1.CellText(vr.DataRowIndex) + ",\r\n";
                                            break;
                                        case "char[]":
                                        case "text[]":
                                        case "character varying[]":
                                        case "bit varying[]":
                                        case "uuid[]":
                                        case "xml[]":
                                        case "xid8[]":
                                        case "aclitem[]":
                                        case "name[]":
                                        case "oidvector[]":
                                        case "pg_lsn[]":
                                        case "path[]":
                                        case "pg_snapshot[]":
                                        case "point[]":
                                        case "polygon[]":
                                        case "refcursor[]":
                                        case "regclass[]":
                                        case "regcollation[]":
                                        case "regconfig[]":
                                        case "regdictionary[]":
                                        case "regnamespace[]":
                                        case "regoper[]":
                                        case "regoperator[]":
                                        case "regproc[]":
                                        case "regprocedure[]":
                                        case "regrole[]":
                                        case "regtype[]":
                                        case "tid[]":
                                        case "tsmultirange[]":
                                        case "tsquery[]":
                                        case "tsrange[]":
                                        case "tstzmultirange[]":
                                        case "tstzrange[]":
                                        case "tsvector[]":
                                        case "txid_snapshot[]":
                                            sJsonDataValue += col1.CellText(vr.DataRowIndex).Replace("\"", "\\\"").Replace("{", "[\"").Replace("}", "\"]").Replace("\t", "\\t").Replace("\r", "\\r").Replace("\n", "\\n").Replace(",", "\", \"") + ",\r\n";
                                            break;
                                        default:
                                            if (sColumnDataType.StartsWith("numeric") && sColumnDataType.EndsWith("[]"))
                                            {
                                                sJsonDataValue += "{ " + col1.CellText(vr.DataRowIndex) + " },\r\n";
                                            }
                                            else
                                            {
                                                sJsonDataValue += "\"" + col1.CellText(vr.DataRowIndex).Replace("\"", "\\\"").Replace("\t", "\\t").Replace("\r", "\\r").Replace("\n", "\\n") + "\",\r\n";
                                            }

                                            break;
                                    }
                                }

                                sJsonData += sJsonDataValue;
                                Application.DoEvents();
                            }

                            sJsonData = sJsonData.Substring(0, sJsonData.Length - 3) + "\r\n  }" + (iRow + 1 == iPreview ?  "" : ",\r\n");

                            progressBar1.Value = iRow;

                            if (bPreview)
                            {
                                sPreviewResult += sJsonData;
                            }
                            else
                            {
                                TextEngine.WriteContentToFile(sJsonData, cboFilename.Text, cboEncoding.Text, FileMode.Append);
                            }

                            sJsonData = "";

                            if (_bProgressCancel)
                            {
                                break;
                            }
                        }

                        if (bPreview)
                        {
                            sPreviewResult += "\r\n]\r\n";
                        }
                        else
                        {
                            TextEngine.WriteContentToFile("\r\n]\r\n", cboFilename.Text, cboEncoding.Text, FileMode.Append);
                        }

                        btnCancel.Visible = false;
                        progressBar1.Visible = false;

                        if (_bProgressCancel)
                        {
                            _sLangText = MyGlobal.GetLanguageString("This operation has been cancelled.", "Global", "Global", "msg", "CancelByUser", "Text");
                            lblInfo.Text = _sLangText;
                            _bBusy = false;
                            Application.UseWaitCursor = false;
                            return;
                        }

                        if (bPreview)
                        {
                            editorPreview.Text = sPreviewResult;
                        }
                        #endregion
                    }
                    else if (cboSaveAsType.Text.StartsWith("XML"))
                    {
                        string sXmlData0 = "", sXmlData = "";

                        #region //產出 XML 檔案
                        iPreview = bPreview ? Math.Min(iPreview, dtData.Rows.Count) : dtData.Rows.Count;

                        for (var iRow = 0; iRow < iPreview; iRow++)
                        {
                            var vr = c1GridVisualStyle.Splits[0].Rows[iRow];

                            if (iRow == 0)
                            {
                                switch (cboSaveAsType.Text)
                                {
                                    case "XML A (*.xml)": //FlySpeed, DataGrip
                                    case "XML B (*.xml)": //HeidiSQL, ToadEdge
                                    case "XML C (*.xml)": //SqlDbx, dbForge
                                        sXmlData = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n";
                                        sXmlData += "<dataroot>\r\n";
                                        break;
                                    case "XML DataPacket 2.0 (*.xml)": //EMS SQL Manager
                                        sXmlData = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n";
                                        sXmlData += "<DATAPACKET Version=\"2.0\">\r\n";
                                        sXmlData += "  <METADATA>\r\n    <FIELDS>\r\n";
                                        break;
                                    case "XML Access (*.xml)": //EMS SQL Manager
                                        sXmlData = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>\r\n";
                                        sXmlData += "<dataroot xmlns:od=\"urn:schemas-microsoft-com:officedata\">\r\n";
                                        break;
                                }

                                if (bPreview)
                                {
                                    sPreviewResult += sXmlData;
                                }
                                else
                                {
                                    TextEngine.WriteContentToFile(sXmlData, cboFilename.Text, cboEncoding.Text, FileMode.Append);
                                }
                            }

                            switch (cboSaveAsType.Text)
                            {
                                case "XML A (*.xml)": //FlySpeed, DataGrip
                                case "XML B (*.xml)": //HeidiSQL, ToadEdge
                                    sXmlData = "  <row>\r\n";
                                    break;
                                case "XML C (*.xml)": //SqlDbx, dbForge
                                    sXmlData = "  <row ";
                                    break;
                                case "XML DataPacket 2.0 (*.xml)": //EMS SQL Manager
                                    sXmlData = "    <ROW ";
                                    break;
                                case "XML Access (*.xml)": //EMS SQL Manager
                                    sXmlData = "  <row>";
                                    break;
                            }

                            foreach (C1DataColumn col1 in c1GridVisualStyle.Columns)
                            {
                                string sTemp2;

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

                                var sColumnName = sTemp1;
                                var sDataType = string.IsNullOrEmpty(sTemp2) ? "" : sTemp2;

                                var sColumnDataType = (col1.Tag ?? string.Empty).ToString();

                                if (string.IsNullOrEmpty(sColumnDataType))
                                {
                                    sColumnDataType = dtData.Columns[col1.Caption].DataType.ToString().Replace("System.", "");
                                }

                                try
                                {
                                    if (dtSchemaTable.Rows.Count == 0) //Raw Data Mode
                                    {
                                        sDataType = sColumnDataType;
                                    }
                                    else
                                    {
                                        var dtRow = dtSchemaTable.Select("ColumnName = '" + sColumnName.Replace("'", "''") + "'");

                                        if (dtRow.Length > 0)
                                        {
                                            switch (MyGlobal.sDataSource)
                                            {
                                                case "Oracle":
                                                    sColumnDataType = MyGlobal.GetDataTypeFormat_Oracle(dtRow, out sDataType);
                                                    break;
                                                case "PostgreSQL":
                                                    sColumnDataType = MyGlobal.GetDataTypeFormat_PostgreSQL(dtRow, out sDataType);
                                                    break;
                                                case "SQL Server":
                                                    sColumnDataType = MyGlobal.GetDataTypeFormat_SQLServer(dtRow, out sDataType);
                                                    break;
                                                case "MySQL":
                                                    sColumnDataType = MyGlobal.GetDataTypeFormat_MySQL(dtRow, out sDataType);
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            sDataType = sColumnDataType;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    sDataType = sColumnDataType;
                                }

                                string sXmlDataValue;

                                if (c1GridVisualStyle.Columns[col1.Caption].CellValue(iRow) == DBNull.Value)
                                {
                                    sXmlDataValue = "null";
                                }
                                else
                                {
                                    sXmlDataValue = col1.CellText(vr.DataRowIndex).Replace("&", "&amp;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;").Replace(" ", "&#160;"); //文字
                                    sXmlDataValue = chkConvertCRLF.Checked ? sXmlDataValue.Replace("\r", "&#xD;").Replace("\n", "&#xA;") : sXmlDataValue;
                                }

                                if (iRow == 0 && cboSaveAsType.Text == "XML DataPacket 2.0 (*.xml)")
                                {
                                    sXmlData0 += "      <FIELD FieldName=\"" + sColumnName + "\" DisplayLabel=\"" + sColumnName + "\" FieldType=\"" + sDataType + "\" FieldClass=\"TField\"/>\r\n";
                                }

                                switch (cboSaveAsType.Text)
                                {
                                    case "XML A (*.xml)": //FlySpeed, DataGrip
                                        sXmlData += "    <" + sColumnName + ">" + sXmlDataValue + "</" + sColumnName + ">\r\n";
                                        break;
                                    case "XML B (*.xml)": //HeidiSQL, ToadEdge
                                        sXmlData += "    <column name=\"" + sColumnName + "\">" + sXmlDataValue + "</column>\r\n";
                                        break;
                                    case "XML Access (*.xml)": //EMS SQL Manager
                                        sXmlData += "<" + sColumnName + ">" + sXmlDataValue + "</" + sColumnName + ">";
                                        break;
                                    case "XML C (*.xml)": //SqlDbx, dbForge
                                    case "XML DataPacket 2.0 (*.xml)": //EMS SQL Manager
                                        sXmlData += "" + sColumnName + "=\"" + sXmlDataValue + "\" ";
                                        break;
                                }

                                Application.DoEvents();
                            }

                            if (iRow == 0 && cboSaveAsType.Text == "XML DataPacket 2.0 (*.xml)")
                            {
                                sXmlData0 += "    </FIELDS>\r\n  </METADATA>\r\n  <ROWDATA>\r\n";

                                if (bPreview)
                                {
                                    sPreviewResult += sXmlData0;
                                }
                                else
                                {
                                    TextEngine.WriteContentToFile(sXmlData0, cboFilename.Text, cboEncoding.Text, FileMode.Append);
                                }
                            }

                            switch (cboSaveAsType.Text)
                            {
                                case "XML A (*.xml)": //FlySpeed, DataGrip
                                case "XML B (*.xml)": //HeidiSQL, ToadEdge
                                    sXmlData += "  </row>\r\n";
                                    break;
                                case "XML C (*.xml)": //SqlDbx, dbForge
                                case "XML DataPacket 2.0 (*.xml)": //EMS SQL Manager
                                    sXmlData = sXmlData.TrimEnd() + "/>\r\n";
                                    break;
                                case "XML Access (*.xml)": //EMS SQL Manager
                                    sXmlData += "</row>\r\n";
                                    break;
                            }

                            progressBar1.Value = iRow;

                            if (bPreview)
                            {
                                sPreviewResult += sXmlData;
                            }
                            else
                            {
                                TextEngine.WriteContentToFile(sXmlData, cboFilename.Text, cboEncoding.Text, FileMode.Append);
                            }

                            sXmlData = "";

                            if (_bProgressCancel)
                            {
                                break;
                            }
                        }

                        //最後結束符號
                        switch (cboSaveAsType.Text)
                        {
                            case "XML A (*.xml)": //FlySpeed, DataGrip
                            case "XML B (*.xml)": //HeidiSQL, ToadEdge
                            case "XML C (*.xml)": //SqlDbx, dbForge
                            case "XML Access (*.xml)": //EMS SQL Manager
                                sXmlData += "</dataroot>\r\n";
                                break;
                            case "XML DataPacket 2.0 (*.xml)": //EMS SQL Manager
                                sXmlData = "  </ROWDATA>\r\n</DATAPACKET>\r\n";
                                break;
                        }

                        if (bPreview)
                        {
                            sPreviewResult += sXmlData;
                        }
                        else
                        {
                            TextEngine.WriteContentToFile(sXmlData, cboFilename.Text, cboEncoding.Text, FileMode.Append);
                        }

                        btnCancel.Visible = false;
                        progressBar1.Visible = false;

                        if (_bProgressCancel)
                        {
                            _sLangText = MyGlobal.GetLanguageString("This operation has been cancelled.", "Global", "Global", "msg", "CancelByUser", "Text");
                            lblInfo.Text = _sLangText;
                            _bBusy = false;
                            Application.UseWaitCursor = false;
                            return;
                        }

                        if (bPreview)
                        {
                            editorPreview.Text = sPreviewResult;
                        }
                        #endregion
                    }
                    else if (cboSaveAsType.Text.StartsWith("HTML"))
                    {
                        c1GridVisualStyle.ExportToHTML(cboFilename.Text);
                    }
                    else if (cboSaveAsType.Text.StartsWith("RTF"))
                    {
                        c1GridVisualStyle.ExportToRTF(cboFilename.Text);
                    }

                    _bBusy = false;
                }
                catch (Exception ex)
                {
                    _sLangText = MyGlobal.GetLanguageString("An error occurred while saving {FileType} file.", "Global", "Global", "msg", "ErrorToSaveFile", "Text");
                    _sLangText = _sLangText.Replace("{FileType}", sSaveAsType);
                    MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message + "\r\n\r\n" + sTryAgain, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    Application.UseWaitCursor = false;
                    _bBusy = false;
                    progressBar1.Visible = false;
                    btnCancel.Visible = false;
                }

                if (bPreview)
                {
                    lblInfo.Text = "";
                    return;
                }

                _sLangText = MyGlobal.GetLanguageString("All data has been exported.", "form", Name, "msg", "ExportOK", "Text");

                lblInfo.Tag = _sLangText;
                lblInfo.Text = _sLangText;

                if (chkAutoOpenExportedFile.Checked == false)
                {
                    return;
                }

                try
                {
                    Process.Start(cboFilename.Text);
                }
                catch (Exception ex)
                {
                    _sLangText = MyGlobal.GetLanguageString("An error occurred while opening {FileType} file.", "Global", "Global", "msg", "ErrorToOpenFile", "Text");
                    _sLangText = _sLangText.Replace("{FileType}", sSaveAsType);
                    MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message + "\r\n\r\n" + sTryAgain, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                return;
            }

            try
            {
                ExportToExcel(cboSaveAsType.Text == "Excel 2007 (*.xlsx)" ? "2007" : "2003", dtData, cboFilename.Text);
                _bBusy = false;
            }
            catch (Exception ex)
            {
                bExportResult = false;
                _sLangText = MyGlobal.GetLanguageString("An error occurred while saving {FileType} file.", "Global", "Global", "msg", "ErrorToSaveFile", "Text");
                _sLangText = _sLangText.Replace("{FileType}", sSaveAsType);
                MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message + "\r\n\r\n" + sTryAgain, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Application.UseWaitCursor = false;
            }

            btnCancel.Visible = false;
            progressBar1.Visible = false;
            _bBusy = false;

            if (_bProgressCancel)
            {
                return;
            }

            if (!bExportResult)
            {
                return;
            }

            _sLangText = chkAutoOpenExportedFile.Checked ? MyGlobal.GetLanguageString("All data has been exported and displayed in an Excel spreadsheet.", "form", Name, "msg", "ExportAndOpenByExcel", "Text") : MyGlobal.GetLanguageString("All data has been exported to Excel.", "form", Name, "msg", "ExportOK", "Text");

            lblInfo.Tag = _sLangText;
            lblInfo.Text = _sLangText;

            if (!chkAutoOpenExportedFile.Checked || _bProgressCancel)
            {
                return;
            }

            try
            {
                tmrExcelDetect.Enabled = true;
                lblExcelDetect.Text = " ";

                Process.Start(cboFilename.Text);

                //5 秒後 lblExcelDetect.Text 沒有變成空值，表示該 Excel 檔並沒有被成功地開啟 (可能狀況：1. Excel 處於「儲存格編輯模式」；2. Excel 跳出錯誤小視窗，但使用者未按下「確定」；3. Excel 檔案太大，開啟超過 5 秒)
                lblExcelDetect.Text = "";
                tmrExcelDetect.Enabled = false;
                lblInfo.ForeColor = Color.Green;
                lblInfo.Text = lblInfo.Tag.ToString();
            }
            catch (Exception ex)
            {
                _sLangText = MyGlobal.GetLanguageString("An error occurred while opening {FileType} file.", "Global", "Global", "msg", "ErrorToOpenFile", "Text");
                _sLangText = _sLangText.Replace("{FileType}", sSaveAsType);
                MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message + "\r\n\r\n" + sTryAgain, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Enabled = true;
            }
        }

        private void VisualStyleChanged(object sender, EventArgs e)
        {
            ApplyVisualStyle(); //VisualStyleChanged
        }

        private void ApplyVisualStyle()
        {
            if (rdoDefault.Checked)
            {
                c1GridVisualStyle.HeadingStyle.BackColor = Color.FromName("LightSkyBlue");
                c1GridVisualStyle.OddRowStyle.BackColor = Color.FromName("White");
                c1GridVisualStyle.EvenRowStyle.BackColor = Color.FromName("LightYellow");
            }
            else //custom
            {
                c1GridVisualStyle.HeadingStyle.BackColor = Color.FromName(cboHeadingBackColor.Text);
                c1GridVisualStyle.OddRowStyle.BackColor = Color.FromName(cboOddRowBackColor.Text);
                c1GridVisualStyle.EvenRowStyle.BackColor = Color.FromName(cboEvenRowBackColor.Text);
            }
        }

        private void btnSaveAllTheSettings_Click(object sender, EventArgs e)
        {
            if (_bBusy)
            {
                return;
            }

            lblInfo.Text = "";

            MyGlobal.UpdateSetting("GridConfig", "ExcelFilename", cboFilename.Text);
            MyLibrary.sGridExcelFilename = cboFilename.Text;

            MyGlobal.UpdateSetting("GridConfig", "ExcelSaveAsType", cboSaveAsType.Text);
            MyLibrary.sGridExcelSaveAsType = cboSaveAsType.Text;

            MyGlobal.UpdateSetting("GridConfig", "CSVDelimiters", MyGlobal.GetKeyFromDictionary(MyGlobal.dicCSVDelimiters, cboCSVDelimiters.Text));

            MyGlobal.UpdateSetting("GridConfig", "ConvertCRLF", chkConvertCRLF.Checked == false ? "0" : "1");
            MyLibrary.bGridConvertCRLF = chkConvertCRLF.Checked;

            MyGlobal.UpdateSetting("GridConfig", "Encoding", cboEncoding.Text);
            MyLibrary.sGridEncoding = cboEncoding.Text;

            MyGlobal.UpdateSetting("GridConfig", "ExcelWorksheetName", txtWorksheetName.Text);
            MyLibrary.sGridExcelWorksheetName = txtWorksheetName.Text;

            MyGlobal.UpdateSetting("GridConfig", "ExcelAutoOpen", chkAutoOpenExportedFile.Checked == false ? "0" : "1");
            MyLibrary.bGridExcelAutoOpen = chkAutoOpenExportedFile.Checked;

            MyGlobal.UpdateSetting("GridConfig", "ExcelAutoColumnResize", chkColumnResize.Checked == false ? "0" : "1");
            MyLibrary.bGridExcelAutoColumnResize = chkColumnResize.Checked;

            MyGlobal.UpdateSetting("GridConfig", "ExcelCustom", rdoCustom.Checked ? "1" : "0");
            MyLibrary.bGridExcelCustom = rdoCustom.Checked;

            MyGlobal.UpdateSetting("GridConfig", "ExcelHeadingBackColor", cboHeadingBackColor.Text);
            MyLibrary.sGridExcelHeadingBackColor = cboHeadingBackColor.Text;

            MyGlobal.UpdateSetting("GridConfig", "ExcelEvenRowBackColor", cboEvenRowBackColor.Text);
            MyLibrary.sGridExcelEvenRowBackColor = cboEvenRowBackColor.Text;

            MyGlobal.UpdateSetting("GridConfig", "ExcelOddRowBackColor", cboOddRowBackColor.Text);
            MyLibrary.sGridExcelOddRowBackColor = cboOddRowBackColor.Text;

            MyGlobal.UpdateSetting("GridConfig", "ExcelFontName", cboGridFontName.Text);
            MyLibrary.sGridExcelFontName = cboGridFontName.Text;
            MyGlobal.UpdateSetting("GridConfig", "ExcelFontSize", cboGridFontSize.Text);
            MyLibrary.sGridExcelFontSize = cboGridFontSize.Text;
            MyGlobal.UpdateSetting("GridConfig", "ExcelRowHeight", cboGridRowHeight.Text);
            MyLibrary.sGridExcelRowHeight = cboGridRowHeight.Text;

            _sLangText = MyGlobal.GetLanguageString("All the settings have been saved!", "form", Name, "msg", "SaveAllTheSettings", "Text");
            lblInfo.Text = _sLangText;
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            if (_bBusy)
            {
                return;
            }

            string sExt;
            var sf = new SaveFileDialog();
            
            _sLangText = MyGlobal.GetLanguageString("Save As", "Global", "Global", "msg", "SaveAs", "Text");
            _sLangText += @" - " + MyGlobal.GetLanguageString("Export All Data to File", "form", Name, "menugrid", "ExportAllDataToFile", "Text");

            sf.Title = _sLangText;

            switch (cboSaveAsType.Text)
            {
                case "Excel 2007 (*.xlsx)":
                    sExt = ".xlsx";
                    sf.Filter = @"Excel files (*.xlsx)|*.xlsx";
                    break;
                case "Excel 2003 (*.xls)":
                    sExt = ".xls";
                    sf.Filter = @"Excel files (*.xls)|*.xls";
                    break;
                case "CSV (*.csv)":
                    sExt = ".csv";
                    sf.Filter = @"CSV files (*.csv)|*.csv";
                    break;
                case "JSON (*.json)":
                    sExt = ".json";
                    sf.Filter = @"JSON files (*.json)|*.json";
                    break;
                case "PDF (*.pdf)":
                    sExt = ".pdf";
                    sf.Filter = @"PDF files (*.pdf)|*.pdf";
                    break;
                case "HTML (*.html)":
                    sExt = ".html";
                    sf.Filter = @"HTML files (*.html)|*.html";
                    break;
                default:
                    sExt = ".xml";
                    sf.Filter = @"XML files (*.xml)|*.xml";
                    break;
            }

            if (sf.ShowDialog() != DialogResult.OK)
            {
                return; //無論檔案是否存在，只要不是按「取消」或「否」，都會回傳 OK
            }

            if (string.IsNullOrEmpty(Path.GetExtension(sf.FileName)))
            {
                sf.FileName += sExt;
            }

            cboFilename.Text = sf.FileName;
        }

        private void ExportToExcel(string sVersion, DataTable dt, string sFilename)
        {
            int iCols;
            int iRows;
            var iPos = 0;
            string sColorHeading;
            string sColorOdd;
            string sColorEven;

            if (rdoCustom.Checked)
            {
                sColorHeading = cboHeadingBackColor.Text;
                sColorOdd = cboOddRowBackColor.Text;
                sColorEven = cboEvenRowBackColor.Text;
            }
            else
            {
                sColorHeading = "LightSkyBlue";
                sColorOdd = "LightYellow";
                sColorEven = "White";
            }

            var ds = new DataSet();
            ds.Tables.Add(dt.Copy());

            var xlsStyleHeader = new XLStyle(c1XLBook1);
            var xlsStyleOdd = new XLStyle(c1XLBook1);
            var xlsStyleEven = new XLStyle(c1XLBook1);
            var xlsStyleOddDecimal = new XLStyle(c1XLBook1);
            var xlsStyleEvenDecimal = new XLStyle(c1XLBook1);

            sFontName = string.IsNullOrEmpty(cboGridFontName.Text) ? sFontName : cboGridFontName.Text;
            float.TryParse(cboGridFontSize.Text, out var fNewFontSize);

            if (fNewFontSize == 0)
            {
                fNewFontSize = 12;
            }

            xlsStyleHeader.AlignHorz = XLAlignHorzEnum.Left;
            xlsStyleHeader.AlignVert = XLAlignVertEnum.Center;
            //xlstStyleHeader.WordWrap = true;
            xlsStyleHeader.Font = new Font(sFontName, fNewFontSize, FontStyle.Regular);
            xlsStyleHeader.SetBorderColor(Color.Gray);
            xlsStyleHeader.BorderBottom = XLLineStyleEnum.Thin;
            xlsStyleHeader.BorderTop = XLLineStyleEnum.Hair;
            xlsStyleHeader.BorderLeft = XLLineStyleEnum.Hair;
            xlsStyleHeader.BorderRight = XLLineStyleEnum.Hair;
            xlsStyleHeader.ForeColor = Color.Black;
            xlsStyleHeader.BackColor = Color.FromName(sColorHeading); //Color.LightBlue;// ColorTranslator.FromHtml(sColorHeading);

            xlsStyleEven.AlignHorz = XLAlignHorzEnum.Left;
            xlsStyleEven.AlignVert = XLAlignVertEnum.Center;
            //xlstStyleEven.WordWrap = true;
            xlsStyleEven.Font = new Font(sFontName, fNewFontSize, FontStyle.Regular);
            xlsStyleEven.SetBorderColor(Color.Gray);
            xlsStyleEven.BorderBottom = XLLineStyleEnum.Hair;
            xlsStyleEven.BorderTop = XLLineStyleEnum.Hair;
            xlsStyleEven.BorderLeft = XLLineStyleEnum.Hair;
            xlsStyleEven.BorderRight = XLLineStyleEnum.Hair;
            xlsStyleEven.ForeColor = Color.Black;
            xlsStyleEven.BackColor = Color.FromName(sColorEven);

            xlsStyleOdd.AlignHorz = XLAlignHorzEnum.Left;
            xlsStyleOdd.AlignVert = XLAlignVertEnum.Center;
            //xlstStyleOdd.WordWrap = true;
            xlsStyleOdd.Font = new Font(sFontName, fNewFontSize, FontStyle.Regular);
            xlsStyleOdd.SetBorderColor(Color.Gray);
            xlsStyleOdd.BorderBottom = XLLineStyleEnum.Hair;
            xlsStyleOdd.BorderTop = XLLineStyleEnum.Hair;
            xlsStyleOdd.BorderLeft = XLLineStyleEnum.Hair;
            xlsStyleOdd.BorderRight = XLLineStyleEnum.Hair;
            xlsStyleOdd.ForeColor = Color.Black;
            xlsStyleOdd.BackColor = Color.FromName(sColorOdd);

            xlsStyleEvenDecimal.AlignHorz = XLAlignHorzEnum.Right;
            xlsStyleEvenDecimal.AlignVert = XLAlignVertEnum.Center;
            //xlstStyleEvenDecimal.WordWrap = true;
            xlsStyleEvenDecimal.Font = new Font(sFontName, fNewFontSize, FontStyle.Regular);
            xlsStyleEvenDecimal.SetBorderColor(Color.Gray);
            xlsStyleEvenDecimal.BorderBottom = XLLineStyleEnum.Hair;
            xlsStyleEvenDecimal.BorderTop = XLLineStyleEnum.Hair;
            xlsStyleEvenDecimal.BorderLeft = XLLineStyleEnum.Hair;
            xlsStyleEvenDecimal.BorderRight = XLLineStyleEnum.Hair;
            xlsStyleEvenDecimal.ForeColor = Color.Black;
            xlsStyleEvenDecimal.BackColor = Color.FromName(sColorEven);

            xlsStyleOddDecimal.AlignHorz = XLAlignHorzEnum.Right;
            xlsStyleOddDecimal.AlignVert = XLAlignVertEnum.Center;
            //xlstStyleOddDecimal.WordWrap = true;
            xlsStyleOddDecimal.Font = new Font(sFontName, fNewFontSize, FontStyle.Regular);
            xlsStyleOddDecimal.SetBorderColor(Color.Gray);
            xlsStyleOddDecimal.BorderBottom = XLLineStyleEnum.Hair;
            xlsStyleOddDecimal.BorderTop = XLLineStyleEnum.Hair;
            xlsStyleOddDecimal.BorderLeft = XLLineStyleEnum.Hair;
            xlsStyleOddDecimal.BorderRight = XLLineStyleEnum.Hair;
            xlsStyleOddDecimal.ForeColor = Color.Black;
            xlsStyleOddDecimal.BackColor = Color.FromName(sColorOdd);

            c1XLBook1.Author = "JasonQuery";
            c1XLBook1.CompatibilityMode = sVersion == "2003" ? CompatibilityMode.Excel2003 : CompatibilityMode.Excel2007;

            var sheet = c1XLBook1.Sheets[0];
            sheet.Name = txtWorksheetName.Text;

            if (c1XLBook1.CompatibilityMode == CompatibilityMode.Excel2003)
            {
                iCols = ds.Tables[0].Columns.Count >= 255 ? 255 : ds.Tables[0].Columns.Count;
                iRows = ds.Tables[0].Rows.Count >= 65536 ? 65536 : ds.Tables[0].Rows.Count;
            }
            else
            {
                iCols = ds.Tables[0].Columns.Count;
                iRows = ds.Tables[0].Rows.Count;
            }

            //處理標題列
            for (var j = 0; j < ds.Tables[0].Columns.Count; j++)
            {
                var sHeader = ds.Tables[0].Columns[j].ToString();

                if (sHeader.IndexOf("\r\n", 0, StringComparison.Ordinal) != -1)
                {
                    sHeader = sHeader.Replace("\r\n", "\r");
                }

                if (sHeader.IndexOf("\r", 0, StringComparison.Ordinal) != -1)
                {
                    iPos = 2;
                    sheet[0, j].Value = sHeader.Split('\r')[0];
                    sheet[0, j].Style = xlsStyleHeader;
                    sheet[1, j].Value = sHeader.Split('\r')[1];
                    sheet[1, j].Style = xlsStyleHeader;
                }
                else
                {
                    iPos = 1;
                    sheet[0, j].Value = ds.Tables[0].Columns[j].ToString();
                    sheet[0, j].Style = xlsStyleHeader;
                }

                Application.DoEvents();
            }

            btnCancel.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = iRows;

            for (var t = 0; t < ds.Tables.Count; t++)
            {
                for (var r = 0; r < iRows; r++)
                {
                    for (var c = 0; c < iCols; c++)
                    {
                        sheet[r + iPos, c].Value = ds.Tables[t].Rows[r][c];

                        if (ds.Tables[t].Columns[c].DataType.ToString() == "System.Decimal")
                        {
                            sheet[r + iPos, c].Style = (r + 1) % 2 == 0 ? xlsStyleOddDecimal : xlsStyleEvenDecimal;
                        }
                        else
                        {
                            sheet[r + iPos, c].Style = (r + 1) % 2 == 0 ? xlsStyleOdd : xlsStyleEven;
                        }

                        progressBar1.Value = r + 1;
                        Application.DoEvents();

                        if (_bProgressCancel)
                        {
                            break;
                        }
                    }

                    if (_bProgressCancel)
                    {
                        break;
                    }
                }

                if (_bProgressCancel)
                {
                    break;
                }
            }

            btnCancel.Visible = false;
            progressBar1.Visible = false;

            if (_bProgressCancel)
            {
                _sLangText = MyGlobal.GetLanguageString("This operation has been cancelled.", "Global", "Global", "msg", "CancelByUser", "Text");
                lblInfo.Text = _sLangText;
            }
            else
            {
                sheet.Rows.Frozen = iPos;
                progressBar1.Value = iRows;

                _sLangText = MyGlobal.GetLanguageString("Saving file...", "Global", "Global", "msg", "SavingFile", "Text");
                lblInfo.Text = _sLangText;
                Application.DoEvents();
                lblInfo.Visible = true;
                lblInfo.Refresh();

                if (chkColumnResize.Checked)
                {
                    AutoSizeColumns(sheet);
                }

                try
                {
                    c1XLBook1.Save(sFilename);
                }
                catch (Exception ex)
                {
                    _sLangText = MyGlobal.GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text");
                    MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            ds.Dispose();
        }

        private void BackColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bBusy)
            {
                return;
            }

            rdoCustom.Checked = true;
            ApplyVisualStyle(); //BackColor_SelectedIndexChanged
        }

        private void BackColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            var g = e.Graphics;
            var rect = e.Bounds;

            if (e.Index < 0)
            {
                return;
            }

            var n = ((ComboBox)sender).Items[e.Index].ToString();
            var c = Color.FromName(n);
            Brush b = new SolidBrush(c);
            g.FillRectangle(b, rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
        }

        private void cboFilename_Leave(object sender, EventArgs e)
        {
            var sExt = MyGlobal.GetStringBetween2(cboSaveAsType.Text, "*", ")", false);

            if (cboFilename.Text.Length <= 5)
            {
                return;
            }

            if (string.IsNullOrEmpty(Path.GetExtension(cboFilename.Text))) // && Path.GetExtension(cboFilename.Text).ToLower() != sExt)
            {
                if (cboFilename.Text.Substring(cboFilename.Text.Length - 1, 1) == ".")
                {
                    cboFilename.Text += sExt.Replace(".", "");
                }
                else
                {
                    var sExtTemp = Path.GetExtension(cboFilename.Text);
                    var sTemp = cboFilename.Text.Substring(0, cboFilename.Text.Length - sExtTemp.Length);
                    cboFilename.Text = sTemp + sExt;
                }

                return;
            }
        }

        private void AutoSizeColumns(XLSheet sheet)
        {
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                for (var c = 0; c < sheet.Columns.Count; c++)
                {
                    var colWidth = -1;

                    for (var r = 0; r < sheet.Rows.Count; r++)
                    {
                        var value = sheet[r, c].Value;

                        if (value == null)
                        {
                            continue;
                        }

                        var text = value.ToString();

                        var s = sheet[r, c].Style;

                        if (s != null && s.Format.Length > 0 && value is IFormattable)
                        {
                            var fmt = XLStyle.FormatXLToDotNet(s.Format);
                            text = ((IFormattable)value).ToString(fmt, System.Globalization.CultureInfo.CurrentCulture);
                        }

                        var font = c1XLBook1.DefaultFont;

                        if (s?.Font != null)
                        {
                            font = s.Font;
                        }

                        var sz = Size.Ceiling(g.MeasureString(text + "XX", font));

                        if (sz.Width > colWidth)
                        {
                            colWidth = sz.Width;
                        }
                    }

                    if (colWidth > -1)
                    {
                        sheet.Columns[c].Width = C1XLBook.PixelsToTwips(colWidth);
                    }
                }
            }
        }

        private void UpdateFilenameList() //判斷是否要更新「檔案清單」
        {
            if (cboFilename.Items.Count > 0 && cboFilename.Text == cboFilename.Items[0].ToString())
            {
                //檔名的是下拉清單的第一個，不用更新
            }
            else
            {
                SaveFilenameList(cboFilename.Text); //UpdateFilenameList
            }

            cboFilename.Tag = cboFilename.Text;
        }

        private void SaveFilenameList(string sFilename)
        {
            //Save
            var sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'FilenameList' AND AttributeValue = '{2}'";
            sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID, sFilename.Replace("'", "''"));
            var dtFilename = DBCommon.ExecQuery(sSql);

            if (dtFilename.Rows.Count > 0)
            {
                sSql = "UPDATE SystemConfig SET AttributeDate = '{2}' WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'FilenameList' AND AttributeValue = '{3}'";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), sFilename.Replace("'", "''"));
                DBCommon.ExecNonQuery(sSql);
            }
            else
            {
                sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeValue, AttributeDate) VALUES ('{0}', {1}, 'FilenameList', '{2}', '{3}')";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID, sFilename.Replace("'", "''"), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                DBCommon.ExecNonQuery(sSql);
            }

            //Reload
            LoadFilenameList(); //SaveFilenameList
        }

        private void LoadFilenameList()
        {
            var i = 0;

            try
            {
                var sSql = "SELECT AttributeValue FROM SystemConfig WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'FilenameList' ORDER BY AttributeDate DESC";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID);
                var dtFilename = DBCommon.ExecQuery(sSql);

                if (dtFilename.Rows.Count <= 0)
                {
                    return;
                }

                for (var iRow = 0; iRow < dtFilename.Rows.Count; iRow++)
                {
                    if (i > 20)
                    {
                        break;
                    }

                    cboFilename.Items.Add(dtFilename.Rows[iRow]["AttributeValue"].ToString());

                    i++;
                }
            }
            catch (Exception)
            {
                //throw
            }
        }

        private void tmrExcelDetect_Tick(object sender, EventArgs e)
        {
            if (lblExcelDetect.Text != " ")
            {
                return;
            }

            lblInfo.ForeColor = Color.DarkRed;
            _sLangText = MyGlobal.GetLanguageString("Your Excel may be in \"cell-edit\" mode, so the file cannot be opened normally!", "form", Name, "msg", "CellEditMode", "Text");
            lblInfo.Text = _sLangText;
        }

        private void cboGridFontName_TextChanged(object sender, EventArgs e)
        {
            float.TryParse(cboGridFontSize.Text, out var iSize);

            if (iSize == 0)
            {
                iSize = 12;
            }

            c1GridVisualStyle.Font = new Font(cboGridFontName.Text, iSize, FontStyle.Regular, GraphicsUnit.Point);

            if (chkColumnResize.Checked)
            {
                AutoSizeGrid(); //cboGridFontName_TextChanged
            }

            c1GridVisualStyle.Refresh();
        }

        private void cboGridFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            float.TryParse(cboGridFontSize.Text, out var fSize);

            if (fSize == 0)
            {
                fSize = 12;
            }

            c1GridVisualStyle.Font = new Font(cboGridFontName.Text, fSize, FontStyle.Regular, GraphicsUnit.Point);

            if (chkColumnResize.Checked)
            {
                AutoSizeGrid(); //cboGridFontSize_SelectedIndexChanged
            }

            c1GridVisualStyle.Refresh();
        }

        private void cboGridRowHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(cboGridRowHeight.Text, out var iSize);

            if (iSize == 0)
            {
                iSize = 12;
            }

            c1GridVisualStyle.Splits[0].ColumnCaptionHeight = iSize;
            c1GridVisualStyle.RowHeight = iSize;
        }

        private void AutoSizeGrid()
        {
            foreach (C1DisplayColumn col in c1GridVisualStyle.Splits[0].DisplayColumns)
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

            c1GridVisualStyle.Refresh();
        }

        private void cboCSVDelimiters_TextChanged(object sender, EventArgs e)
        {
            var sStyle = MyGlobal.GetKeyFromDictionary(MyGlobal.dicCSVDelimiters, cboCSVDelimiters.Text);

            switch (sStyle)
            {
                case "Tab":
                    cboCSVDelimiters.Tag = "\t";
                    break;
                case "Semicolon":
                    cboCSVDelimiters.Tag = ";";
                    break;
                case "Comma":
                    cboCSVDelimiters.Tag = ",";
                    break;
                case "Space":
                    cboCSVDelimiters.Tag = " ";
                    break;
                case "Colon":
                    cboCSVDelimiters.Tag = ":";
                    break;
                case "Slash":
                    cboCSVDelimiters.Tag = "/";
                    break;
                case "Backslash":
                    cboCSVDelimiters.Tag = "\\";
                    break;
                case "Pipe":
                    cboCSVDelimiters.Tag = "|";
                    break;
                default:
                    cboCSVDelimiters.Tag = ",";
                    break;
            }

            ExportToFile(true);
        }

        private void SetSaveAsType()
        {
            var bValue = false;
            var sFileExt = Path.GetExtension(cboFilename.Text);
            var sSaveAsExt = MyGlobal.GetStringBetween2(cboSaveAsType.Text, "*", ")", false);

            lblCSVDelimiters.Visible = false;
            cboCSVDelimiters.Visible = false;
            lblEncoding.Visible = false;
            cboEncoding.Visible = false;
            lblWorksheetName.Visible = false;
            txtWorksheetName.Visible = false;
            cboEncoding.Enabled = true;
            chkConvertCRLF.Visible = false;
            c1GridVisualStyle.Visible = false;
            editorPreview.Visible = false;

            if (cboSaveAsType.Text.StartsWith("XML"))
            {
                cboEncoding.Text = "UTF-8";
                cboEncoding.Enabled = false;
                lblEncoding.Visible = true;
                cboEncoding.Visible = true;
                chkConvertCRLF.Visible = true;
                editorPreview.Visible = true;
            }
            else if (cboSaveAsType.Text.StartsWith("JSON"))
            {
                cboEncoding.Text = "UTF-8";
                cboEncoding.Enabled = false;
                lblEncoding.Visible = true;
                cboEncoding.Visible = true;
                editorPreview.Visible = true;
            }
            else if (cboSaveAsType.Text == "CSV (*.csv)")
            {
                if (string.IsNullOrEmpty(cboCSVDelimiters.Text))
                {
                    cboCSVDelimiters.SelectedIndex = 0;
                }

                if (string.IsNullOrEmpty(cboEncoding.Text))
                {
                    cboEncoding.SelectedIndex = 0;
                }

                lblCSVDelimiters.Visible = true;
                cboCSVDelimiters.Visible = true;
                lblEncoding.Visible = true;
                cboEncoding.Visible = true;
                editorPreview.Visible = true;
            }
            else
            {
                if (cboSaveAsType.Text.StartsWith("Excel"))
                {
                    lblWorksheetName.Visible = true;
                    txtWorksheetName.Visible = true;
                }

                bValue = true;
                c1GridVisualStyle.Visible = true;
            }

            grpDataGrid.Enabled = bValue;
            chkColumnResize.Visible = bValue;

            ExportToFile(true);
            cboSaveAsType.Focus(); //先 Focus 到 ComBoBox，後面再切換到 Grid or Editor，避免切換失效
            Application.DoEvents();

            if (cboSaveAsType.Text.StartsWith("Excel"))
            {
                c1GridVisualStyle.Focus();
            }
            else
            {
                editorPreview.Focus();
            }

            if (string.IsNullOrEmpty(cboFilename.Text.Trim()))
            {
                return;
            }

            cboFilename.Text = cboFilename.Text.Replace(sFileExt, sSaveAsExt);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _bProgressCancel = true;
        }

        private void frmExportToFile_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (dtData.Rows.Count <= 10000)
            {
                return;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        private void chkConvertCRLF_CheckedChanged(object sender, EventArgs e)
        {
            ExportToFile(true);
        }

        private void chkColumnResize_CheckedChanged(object sender, EventArgs e)
        {
            if (chkColumnResize.Checked)
            {
                AutoSizeGrid(); //cboGridFontSize_SelectedIndexChanged()
            }
        }

        private void cboSaveAsType_TextChanged(object sender, EventArgs e)
        {
            if (_bBusy)
            {
                return;
            }

            btnBrowseFile.Focus();
            SetSaveAsType(); //cboSaveAsType_TextChanged
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