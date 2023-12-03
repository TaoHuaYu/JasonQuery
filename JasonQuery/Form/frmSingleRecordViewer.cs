using System;
using System.Data;
using JasonLibrary;
using System.Windows.Forms;
using C1.Win.C1Themes;
using System.Drawing;
using C1.Win.C1TrueDBGrid;
using JasonLibrary.Class;
using VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle;

namespace JasonQuery
{
    public partial class frmSingleRecordViewer : Form
    {
        private string _sAccessibleDescription;
        private int _iTotalQty, _iCurrent;
        private DataTable _dtData;
        private int _iCol1Width;
        private int _iCol2Width;
        private string _sLangText = "";

        public string sAccessibleDescription
        {
            set => _sAccessibleDescription = value;
        }

        public int iTotalQty
        {
            set => _iTotalQty = value;
        }

        public int iCurrent
        {
            set => _iCurrent = value;
        }

        public DataTable dtData
        {
            set => _dtData = value;
        }

        public frmSingleRecordViewer()
        {
            InitializeComponent();
        }

        private void CheckButtonStatus()
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = true;
            btnLast.Enabled = true;

            if (_iTotalQty == 1)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            else if (_iCurrent == 0)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
            }
            else if (_iCurrent == _iTotalQty - 1)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            var i = 0;
            c1Grid.Tag = _dtData.Rows.Count.ToString();

            CheckButtonStatus(); //決定 4 顆按鈕的狀態

            c1Grid.DataSource = _dtData;
            ApplyLocalizationSetting();

            MyGlobal.ApplyLanguageInfo(this, false);

            foreach (C1DisplayColumn col in c1Grid.Splits[0].DisplayColumns)
            {
                if (i == 0)
                {
                    try
                    {
                        col.AutoSize();
                    }
                    catch (Exception)
                    {
                        col.Width = 200;
                    }
                }
                else
                {
                    col.Width = 200;
                }

                i++;
            }

            if (MyLibrary.sGridNullShowAs.ToUpper() != "NONE")
            {
                var s1 = new Style { ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridNullShowColor) };

                for (var j = 0; j < c1Grid.Columns.Count; j++)
                {
                    //套用「使用者指定的 NULL」顯示格式
                    c1Grid.Splits[0].DisplayColumns[j].AddRegexCellStyle(CellStyleFlag.AllCells, s1, MyLibrary.sGridNullShowAs);
                }
            }

            c1Grid.Refresh();
        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            MyGlobal.UpdateSetting("GlobalConfig", "SingleRecordViewerFormWidth", Size.Width.ToString());
            MyGlobal.UpdateSetting("GlobalConfig", "SingleRecordViewerFormHeight", Size.Height.ToString());
        }

        private void ApplyLocalizationSetting()
        {
            if (MyLibrary.bDarkMode)
            {
                C1ThemeController.ApplicationTheme = "VS2013Dark";
            }

            chkShowFilterRow.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);
            tsViewer.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);

            GridVisualStyle(); //ApplyLocalizationSetting
            GridFontAndBackgroundColor();
            GridZoom();

            MyGlobal.SetGridVisualStyle(c1Grid, 11);

            Cursor = Cursors.Default;
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
            const int fontSize = 12;

            //字型 + 字體大小
            c1Grid.Font = new Font(MyLibrary.sGridFontName, fontSize, FontStyle.Regular, GraphicsUnit.Point);

            c1Grid.OddRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowForeColor);
            c1Grid.OddRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowBackColor);
            c1Grid.EvenRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowForeColor);
            c1Grid.EvenRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);

            //Grid's 選取顏色
            c1Grid.SelectedStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedForeColor);
            c1Grid.SelectedStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedBackColor);
        }

        private void chkShowFilterRow_CheckedChanged(object sender, EventArgs e)
        {
            c1Grid.FilterBar = chkShowFilterRow.Checked;
        }

        private void GridZoom()
        {
            const int fontSize = 11;
            const float pcnt = 0.9F;
            var rowHeight = c1Grid.RowHeight;
            var recSelWidth = c1Grid.RecordSelectorWidth;

            c1Grid.RowHeight = (int)(rowHeight * pcnt) + 5;
            c1Grid.Splits[0].ColumnCaptionHeight = (int)(rowHeight * pcnt) + 5;
            c1Grid.Styles["Normal"].Font = new Font(c1Grid.Styles["Normal"].Font.FontFamily, fontSize * pcnt);
        }

        private void c1Grid_Filter(object sender, FilterEventArgs e)
        {
            var dataView = (c1Grid.DataSource as DataTable)?.DefaultView;

            if (dataView == null || dataView.RowFilter == e.Condition)
            {
                return;
            }

            var condition = e.Condition;

            if (condition.Length != 0)
            {
                condition = e.Condition;

                for (var i = 0; i < c1Grid.Splits[0].DisplayColumns.Count; i++)
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var bHandled = false;

            switch (keyData)
            {
                case Keys.Escape:
                    Close();
                    bHandled = true;
                    break;
                case Keys.Control | Keys.C:
                    if (c1Grid.Focused)
                    {
                        CopyDataFromDataGrid();
                        bHandled = true; //此處必須為 true，否則會「使用 Grid 原生的 Copy 功能」
                    }
                    break;
            }

            return bHandled;
        }

        private void c1Grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var iRow = c1Grid.RowContaining(e.Y);

            if (iRow != -1)
            {
                CellViewer();
            }
        }

        private void CellViewer()
        {
            string sColumnName;
            var sColumnType = "";
            var sCellText = c1Grid[c1Grid.Row, c1Grid.Col].ToString();
            var sTemp = c1Grid.Splits[0].DisplayColumns[c1Grid.Col].ToString();

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
                myForm.ShowDialog();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            _iCurrent = 0;
            CheckButtonStatus();
            GetRecord();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _iCurrent--;
            CheckButtonStatus();
            GetRecord();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _iCurrent++;
            CheckButtonStatus();
            GetRecord();
        }

        private void c1Grid_ColResize(object sender, ColResizeEventArgs e)
        {
            var i = 0;

            foreach (C1DisplayColumn col in c1Grid.Splits[0].DisplayColumns)
            {
                if (i == 0)
                {
                    _iCol1Width = col.Width;
                }
                else
                {
                    _iCol2Width = col.Width;
                }

                i++;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void SelectAll()
        {
            c1Grid.SelectedRows.Clear();

            for (var i = 0; i < c1Grid.Splits[0].Rows.Count; i++)
            {
                c1Grid.SelectedRows.Add(i);
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            _iCurrent = _iTotalQty - 1;
            CheckButtonStatus();
            GetRecord();
        }

        private void GetRecord()
        {
            MyGlobal.sGlobalTemp = "singlerecordviewer" + MyGlobal.sSeparator + _sAccessibleDescription + MyGlobal.sSeparator + _iCurrent;

            var startTime = DateTime.Now;

            while (true)
            {
                Application.DoEvents();

                if (DateTime.Now.Subtract(startTime).Milliseconds >= 300)
                {
                    break;
                }

                Application.DoEvents();
            }

            c1Grid.DataSource = MyGlobal.dtSingleRecordViewer;

            var i = 0;

            foreach (C1DisplayColumn col in c1Grid.Splits[0].DisplayColumns)
            {
                col.Width = i == 0 ? _iCol1Width : _iCol2Width;

                i++;
            }

            if (!chkShowFilterRow.Checked)
            {
                return;
            }

            c1Grid.Row = 1;
            c1Grid.Col = 0;
            c1Grid.Select();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyDataFromDataGrid();
        }

        private void btnExportToFile_Click(object sender, EventArgs e)
        {
            const string sSheetName = "Single Record";

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

                myForm.sTitle = Text;
                myForm.dtData = dtData;
                myForm.sSheetName = sSheetName;
                myForm.sFontName = c1Grid.Font.Name;
                myForm.fFontSize = c1Grid.Font.Size;
                myForm.ShowDialog();
            }
        }

        private void c1Grid_MouseClick(object sender, MouseEventArgs e)
        {
            var bCornerSelected = false;
            var iRow = c1Grid.RowContaining(e.Y);
            var iCol = c1Grid.ColContaining(e.X);

            if (iRow == -1 && iCol == -1)
            {
                bCornerSelected = !chkShowFilterRow.Checked || e.Y <= c1Grid.Splits[0].ColumnCaptionHeight;
            }

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

        private void c1Grid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            var gMenu2 = new ContextMenuStrip();

            _sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "menugrid", "SelectAll", "Text");
            gMenu2.Items.Add(_sLangText);

            gMenu2.Items[0].Click += delegate
            {
                SelectAll();
            };

            _sLangText = MyGlobal.GetLanguageString("Copy", "form", Name, "menugrid", "Copy", "Text");
            gMenu2.Items.Add(_sLangText);

            gMenu2.Items[1].Click += delegate
            {
                CopyDataFromDataGrid();
            };

            gMenu2.Items.Add("-");

            _sLangText = MyGlobal.GetLanguageString("Export to File", "form", Name, "menugrid", "ExportToFile", "Text");
            gMenu2.Items.Add(_sLangText);

            gMenu2.Items[3].Click += delegate
            {
                btnExportToFile.PerformClick();
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

        private void c1Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                SelectAll();
            }
        }

        private void CopyDataFromDataGrid()
        {
            var i = 0;
            var sData = "";
            var sColumnName = "";
            var sDataType = "";
            var bActiveCell = true; //是否為「只點到某一個 cell，並沒有『選取』cell」?
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
                            else
                            {
                                sDistinct += col + "```";
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
    }
}