using System;
using System.IO;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using JasonLibrary.Class;

//c1Grid: Height=216, c1Grid2: Height=261

namespace JasonQuery
{
    public sealed partial class frmSchemaSearch : Form
    {
        private string _sLangText = "";
        private DataTable _dtResult;
        private DataTable _dtResult2;

        public frmSchemaSearch()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MyGlobal.ApplyLanguageInfo(this, false);
            MyGlobal.SetGridVisualStyle(c1Grid, 10);
            MyGlobal.SetGridVisualStyle(c1Grid2, 10);
            InitialGrid();

            if ((MyGlobal.sDataSource == "SQL Server" || MyGlobal.sDataSource == "MySQL") && string.IsNullOrEmpty(MyGlobal.sDBConnectionDatabase))
            {
                _sLangText = MyGlobal.GetLanguageString("Please use the USE statement to select a database first.", "form", Name, "msg", "SelectDatabaseFirst", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnSearch.Enabled = false;
                btnSearch2.Enabled = false;
            }

            txtKeyword.Location = new Point(lblKeyword.Left + lblKeyword.Width, txtKeyword.Top);
            txtKeyword2.Location = new Point(lblKeyword2.Left + lblKeyword2.Width, txtKeyword2.Top);
            chkMatchCase.Location = new Point(txtKeyword.Left + txtKeyword.Width + 10, chkMatchCase.Top);
            chkMatchCase2.Location = new Point(txtKeyword2.Left + txtKeyword2.Width + 10, chkMatchCase2.Top);
            txtMillisecond.Location = new Point(chkIntervalDelay.Left + chkIntervalDelay.Width - 4, txtMillisecond.Top);
            lblMillisecond.Location = new Point(txtMillisecond.Left + txtMillisecond.Width, lblMillisecond.Top);
            txtMillisecond2.Location = new Point(chkIntervalDelay2.Left + chkIntervalDelay2.Width - 4, txtMillisecond2.Top);
            lblMillisecond2.Location = new Point(txtMillisecond2.Left + txtMillisecond2.Width, lblMillisecond2.Top);
            txtDatabase.Location = new Point(lblDatabase.Left + lblDatabase.Width - 2, txtDatabase.Top);
            txtDatabase.Size = new Size(btnBlank.Width - lblDatabase.Width + 5, 21);
            txtDatabase.Text = MyGlobal.sDBConnectionDatabase;
            txtDatabase.ReadOnly = true;
            txtDatabase2.Location = new Point(lblDatabase.Left + lblDatabase.Width - 2, txtDatabase2.Top);
            txtDatabase2.Size = new Size(txtDatabase.Width, 21);
            txtDatabase2.Text = MyGlobal.sDBConnectionDatabase;
            txtDatabase2.ReadOnly = true;
            txtSaveExecutedScripts.Size = new Size(grpTables.Width - chkSaveExecutedScripts.Width - btnBrowseFile.Width - 22, 21);
            txtSaveExecutedScripts.Location = new Point(chkSaveExecutedScripts.Left + chkSaveExecutedScripts.Width - 4, txtSaveExecutedScripts.Top);
            btnBrowseFile.Location = new Point(txtSaveExecutedScripts.Left + txtSaveExecutedScripts.Width + 4, btnBrowseFile.Top);
            txtSaveExecutedScripts2.Size = new Size(grpTables.Width - chkSaveExecutedScripts2.Width - btnBrowseFile2.Width - 22, 21);
            txtSaveExecutedScripts2.Location = new Point(chkSaveExecutedScripts2.Left + chkSaveExecutedScripts2.Width - 4, txtSaveExecutedScripts2.Top);
            btnBrowseFile2.Location = new Point(txtSaveExecutedScripts2.Left + txtSaveExecutedScripts2.Width + 4, btnBrowseFile2.Top);
            txtLimit.Location = new Point(chkLimit.Left + chkLimit.Width + 1, txtLimit.Top);
            chkExcludeNonText.Location = new Point(txtLimit.Left + txtLimit.Width + 50, chkExcludeNonText.Top);
            chkWithNoLock.Location = new Point(chkExcludeNonText.Left + chkExcludeNonText.Width + 50, chkWithNoLock.Top);
            lblSearchResultValue.Location = new Point(lblSearchResult.Left + lblSearchResult.Width - 4, lblSearchResultValue.Top);
            lblSearchResultValue.Text = "";
            lblSearchResultValue2.Location = new Point(lblSearchResult2.Left + lblSearchResult2.Width - 4, lblSearchResultValue2.Top);
            lblSearchResultValue2.Text = "";
            chkFunctions.Location = new Point(chkTables.Left + chkTables.Width + 30, chkFunctions.Top);
            chkViews.Location = new Point(chkFunctions.Left + chkFunctions.Width + 30, chkViews.Top);
            chkTriggers.Location = new Point(chkViews.Left + chkViews.Width + 30, chkTriggers.Top);
            chkStoredProcedures.Location = new Point(chkTriggers.Left + chkTriggers.Width + 30, chkStoredProcedures.Top);

            var bDatabaseVisible = false;
            var bWithNolockVisible = false;

            switch (MyGlobal.sDataSource)
            {
                case "SQL Server":
                    bWithNolockVisible = true;
                    bDatabaseVisible = true;
                    break;
                case "MySQL":
                    bDatabaseVisible = true;
                    break;
            }

            lblDatabase.Visible = bDatabaseVisible;
            txtDatabase.Visible = bDatabaseVisible;
            lblDatabase2.Visible = bDatabaseVisible;
            txtDatabase2.Visible = bDatabaseVisible;
            chkWithNoLock.Visible = bWithNolockVisible;

            #region 讀取設定值
            var bValue = false;
            var sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'MatchCase'";
            var dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkMatchCase.Checked = bValue;

            //if (MyGlobal.sDataSource == "SQL Server")
            //{
            //    chkMatchCase.Checked = true; //SQL Server 會因為定序設定而影響到「區分大小寫！」，故最後還是保留此功能
            //}

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'IntervalDelay'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkIntervalDelay.Checked = bValue;

            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'IntervalDelayMillisecond'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0)
            {
                txtMillisecond.Text = dtTemp.Rows[0]["AttributeValue"].ToString();
            }

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'StopWhenFound'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkStop.Checked = bValue;

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'Limit'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkLimit.Checked = bValue;

            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'LimitQty'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0)
            {
                txtLimit.Text = dtTemp.Rows[0]["AttributeValue"].ToString();
            }

            bValue = true;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'ExcludeNonText'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "0")
            {
                bValue = false;
            }

            chkExcludeNonText.Checked = bValue; //預設打勾

            if (MyGlobal.sDataSource == "SQL Server")
            {
                bValue = false;
                sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'WithNoLock'";
                dtTemp = DBCommon.ExecQuery(sSql);

                if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
                {
                    bValue = true;
                }

                chkWithNoLock.Checked = bValue;
            }

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'SaveExecutedScripts'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkSaveExecutedScripts.Checked = bValue;

            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'SaveExecutedScriptsPath'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0)
            {
                txtSaveExecutedScripts.Text = dtTemp.Rows[0]["AttributeValue"].ToString();
            }

            bValue = true;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'Tables'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "0")
            {
                bValue = false;
            }

            chkTables.Checked = bValue; //預設打勾

            bValue = true;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'Functions'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "0")
            {
                bValue = false;
            }

            chkFunctions.Checked = bValue; //預設打勾

            bValue = true;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'Views'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "0")
            {
                bValue = false;
            }

            chkViews.Checked = bValue; //預設打勾

            bValue = true;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'Triggers'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "0")
            {
                bValue = false;
            }

            chkTriggers.Checked = bValue; //預設打勾

            bValue = true;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'StoredProcedures'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "0")
            {
                bValue = false;
            }

            chkStoredProcedures.Checked = bValue; //預設打勾

            bValue = true;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'StoredProcedures'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "0")
            {
                bValue = false;
            }

            chkStoredProcedures.Checked = bValue; //預設打勾

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'MatchCase2'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkMatchCase2.Checked = bValue;

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'IntervalDelay2'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkIntervalDelay2.Checked = bValue;

            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'IntervalDelayMillisecond2'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0)
            {
                txtMillisecond2.Text = dtTemp.Rows[0]["AttributeValue"].ToString();
            }

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'SaveExecutedScripts2'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkSaveExecutedScripts2.Checked = bValue;

            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'SchemaSearchConfig' AND AttributeName = 'SaveExecutedScriptsPath2'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0)
            {
                txtSaveExecutedScripts2.Text = dtTemp.Rows[0]["AttributeValue"].ToString();
            }
            #endregion

            PreviewSQL();

            txtKeyword.Focus();
        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            MyGlobal.UpdateSetting("GlobalConfig", "SchemaSearchFormWidth", Size.Width.ToString());
            MyGlobal.UpdateSetting("GlobalConfig", "SchemaSearchFormHeight", Size.Height.ToString());
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

        private void txtMillisecond_Leave(object sender, EventArgs e)
        {
            int.TryParse(txtMillisecond.Text, out int iResult);
            txtMillisecond.Text = iResult.ToString();

            if (string.IsNullOrEmpty(txtMillisecond.Text) || txtMillisecond.Text == @"0")
            {
                txtMillisecond.Text = @"150";
            }

            PreviewSQL();
        }

        private void txtMillisecond2_Leave(object sender, EventArgs e)
        {
            int.TryParse(txtMillisecond2.Text, out var iResult);
            txtMillisecond2.Text = iResult.ToString();

            if (string.IsNullOrEmpty(txtMillisecond2.Text) || txtMillisecond2.Text == @"0")
            {
                txtMillisecond2.Text = @"150";
            }
        }

        private void txtLimit_Leave(object sender, EventArgs e)
        {
            int.TryParse(txtLimit.Text, out var iResult);
            txtLimit.Text = iResult.ToString();

            if (string.IsNullOrEmpty(txtLimit.Text) || txtLimit.Text == @"0")
            {
                txtLimit.Text = @"10";
            }

            PreviewSQL();
        }

        private void txtNumber_MouseClick(object sender, MouseEventArgs e)
        {
            var txt = sender as C1.Win.C1Input.C1TextBox;

            txt.SelectionStart = 0;
            txt.SelectionLength = txt.TextLength;
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void chkMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            PreviewSQL();
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            PreviewSQL();
        }

        private void btnSaveAllTheSettings_Click(object sender, EventArgs e)
        {
            MyGlobal.UpdateSetting("SchemaSearchConfig", "MatchCase", chkMatchCase.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "IntervalDelay", chkIntervalDelay.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "IntervalDelayMillisecond", txtMillisecond.Text);
            MyGlobal.UpdateSetting("SchemaSearchConfig", "StopWhenFound", chkStop.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "Limit", chkLimit.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "LimitQty", txtLimit.Text);
            MyGlobal.UpdateSetting("SchemaSearchConfig", "ExcludeNonText", chkExcludeNonText.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "WithNoLock", (MyGlobal.sDataSource == "SQL Server" && chkWithNoLock.Checked) ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "SaveExecutedScripts", chkSaveExecutedScripts.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "SaveExecutedScriptsPath", txtSaveExecutedScripts.Text);

            _sLangText = MyGlobal.GetLanguageString("All the settings have been saved!", "form", Name, "msg", "SaveAllTheSettings", "Text");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSaveAllTheSettings2_Click(object sender, EventArgs e)
        {
            MyGlobal.UpdateSetting("SchemaSearchConfig", "Tables", chkTables.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "Functions", chkFunctions.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "Views", chkViews.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "Triggers", chkTriggers.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "StoredProcedures", chkStoredProcedures.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "MatchCase2", chkMatchCase2.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "IntervalDelay2", chkIntervalDelay2.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "IntervalDelayMillisecond2", txtMillisecond2.Text);
            MyGlobal.UpdateSetting("SchemaSearchConfig", "SaveExecutedScripts2", chkSaveExecutedScripts2.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("SchemaSearchConfig", "SaveExecutedScriptsPath2", txtSaveExecutedScripts2.Text);

            _sLangText = MyGlobal.GetLanguageString("All the settings have been saved!", "form", Name, "msg", "SaveAllTheSettings", "Text");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtKeyword_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtKeyword.Text))
            {
                PreviewSQL();
            }
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            var of = new SaveFileDialog();

            _sLangText = MyGlobal.GetLanguageString("Save as a New Script File...", "form", Name, "msg", "SaveAsNewScriptFile", "Text");
            of.Title = _sLangText;
            of.Filter = @"Query files (*.sql)|*.sql|All files (*.*)|*.*";

            if (of.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txtSaveExecutedScripts.Text = of.FileName;
        }

        private void btnBrowseFile2_Click(object sender, EventArgs e)
        {
            var of = new SaveFileDialog();

            _sLangText = MyGlobal.GetLanguageString("Save as a New Script File...", "form", Name, "msg", "SaveAsNewScriptFile", "Text");
            of.Title = _sLangText;
            of.Filter = @"Query files (*.sql)|*.sql|All files (*.*)|*.*";

            if (of.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txtSaveExecutedScripts2.Text = of.FileName;
        }

        private string PreviewSQL(bool bGetPreviewSQL = false)
        {
            var sSampleSQL = lblSampleSQL.Tag.ToString();

            if (bGetPreviewSQL == false)
            {
                sSampleSQL = sSampleSQL.Replace("$TABLE$", "Table"); //.Replace("$COLUMN$", sColumnName)
            }

            sSampleSQL = sSampleSQL.Replace("$KEYWORD$", chkMatchCase.Checked ? txtKeyword.Text : txtKeyword.Text.ToUpper());

            if (bGetPreviewSQL)
            {
                sSampleSQL = sSampleSQL.Replace("$COLUMN$", chkMatchCase.Checked ? "$COLUMN$" : MyGlobal.sDataSource == "Access" ? "UCASE($COLUMN$)" : "UPPER($COLUMN$)");
            }
            else
            {
                if (MyGlobal.sDataSource == "PostgreSQL")
                {
                    sSampleSQL = sSampleSQL.Replace("$COLUMN$", chkMatchCase.Checked ? "Column::TEXT" : "UPPER(Column::TEXT)");
                }
                else
                {
                    sSampleSQL = sSampleSQL.Replace("$COLUMN$", chkMatchCase.Checked ? "Column" : MyGlobal.sDataSource == "Access" ? "UCASE(Column)" : "UPPER(Column)");
                }
            }

            if (MyGlobal.sDataSource == "SQL Server" && chkWithNoLock.Checked)
            {
                sSampleSQL = sSampleSQL.Replace("$NOLOCK$", " WITH (NOLOCK)");
            }
            else
            {
                sSampleSQL = sSampleSQL.Replace("$NOLOCK$", "");
            }

            if (chkLimit.Checked == false)
            {
                sSampleSQL = sSampleSQL.Replace("$LIMIT1$", "").Replace(" $LIMIT2$", "");
            }
            else
            {
                switch (MyGlobal.sDataSource)
                {
                    case "Oracle":
                        sSampleSQL = sSampleSQL.Replace("$LIMIT1$", "").Replace(" $LIMIT2$", " AND ROWRUM <= " + txtLimit.Text);
                        break;
                    case "PostgreSQL":
                    case "MySQL":
                    case "SQLite":
                    case "SQLCipher":
                        sSampleSQL = sSampleSQL.Replace("$LIMIT1$", "").Replace(" $LIMIT2$", " LIMIT " + txtLimit.Text);
                        break;
                    case "SQL Server":
                    case "Access":
                        sSampleSQL = sSampleSQL.Replace("$LIMIT1$", " TOP " + txtLimit.Text).Replace(" $LIMIT2$", "");
                        break;
                }
            }

            if (bGetPreviewSQL == false)
            {
                lblSampleSQL.Text = sSampleSQL;
            }

            return sSampleSQL;
        }

        private void InitialGrid(int i = 0)
        {
            if (i == 0 || i == 1)
            {
                _dtResult = new DataTable();
                _dtResult.Columns.Add("TableName");
                _dtResult.Columns.Add("ColumnName");
                _dtResult.Columns.Add("Content");

                c1Grid.DataSource = _dtResult;
                MyGlobal.ReplaceColumnNameByLanguageInfo(c1Grid, Name); //InitialGrid
                MyGlobal.ResizeGridColumnWidth(c1Grid); //InitialGrid
            }

            if (i != 0 && i != 2)
            {
                return;
            }

            _dtResult2 = new DataTable();
            _dtResult2.Columns.Add("SchemaType");
            _dtResult2.Columns.Add("SchemaName");
            _dtResult2.Columns.Add("Content");

            c1Grid2.DataSource = _dtResult2;
            MyGlobal.ReplaceColumnNameByLanguageInfo(c1Grid2, Name); //InitialGrid
            MyGlobal.ResizeGridColumnWidth(c1Grid2); //InitialGrid
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (CheckData() == false)
            {
                return;
            }

            var iResult = 0;
            btnCancel.Tag = "0";
            lblSearchResultValue.Text = "";
            InitialGrid(1);

            if (MyGlobal.dtTableColumn == null)
            {
                return;
            }

            var dt = MyGlobal.dtTableColumn;
            var sPreviewSQL = PreviewSQL(true);
            var iDelay = Convert.ToInt16(txtMillisecond.Text);
            var sLangTableName = MyGlobal.GetLanguageString("Table Name", "form", Name, "gridheader", "TableName", "Text") + ": ";
            var sLangColumnName = MyGlobal.GetLanguageString("Column Name", "form", Name, "gridheader", "ColumnName", "Text") + ": ";
            var sLangDataTypeName = MyGlobal.GetLanguageString("Data Type", "form", Name, "gridheader", "DataType", "Text") + ": ";
            var sResult = "";
            var sKeyword = txtKeyword.Text; //要搜尋的關鍵字

            Application.UseWaitCursor = true;

            grpTables.Enabled = false;
            btnSearch.Enabled = false;
            btnClose.Enabled = false;
            btnCancel.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = dt.Rows.Count;
            btnCancel.Focus();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var sTableName = dt.Rows[i]["TableName"].ToString();
                var sColumnName = dt.Rows[i]["ColumnName"].ToString();
                var sDataType = dt.Rows[i]["DataType"].ToString();

                var bExclude = false;

                if (chkExcludeNonText.Checked)
                {
                    bExclude = CheckIsNonTextColumn(sDataType);
                }

                if (bExclude)
                {
                    sResult += "--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss.fff") + ", " + chkExcludeNonText.Text + ", " + sLangTableName + sTableName + ", " + sLangColumnName + sColumnName + ", " + sLangDataTypeName + sDataType + "\r\n";
                    progressBar1.Value++;
                    continue;
                }

                var sSql = sPreviewSQL.Replace("$TABLE$", sTableName).Replace("$COLUMN$", sColumnName).Replace("*", sColumnName);// "SELECT * FROM " + sTableName + " WHERE " + sColumnName + " LIKE '%" + sKeyword + "%'";
                var dtTemp = new DataTable();

                switch (MyGlobal.sDataSource)
                {
                    case "SQL Server" when ",nchar,ntext,nvarchar,".IndexOf("," + sDataType + ",", StringComparison.Ordinal) != -1:
                        sSql = sSql.Replace("LIKE '", "LIKE N'");
                        break;
                    case "SQL Server" when sDataType.StartsWith("date") && DateTime.TryParse(txtKeyword.Text, out DateTime date): //20220915 針對 SQL Server 的日期欄位，修正搜尋方法
                        if (chkMatchCase.Checked)
                        {
                            //if (txtKeyword.Text.IndexOf(":", StringComparison.Ordinal) == -1) //沒有分鐘
                            {
                                sSql = sSql.Replace("WHERE " + sColumnName + " LIKE", "WHERE CONVERT(VARCHAR, " + sColumnName + ", 111) LIKE");
                            }
                            //else //有分鐘
                            //{
                            //    sSql = sSql.Replace("WHERE " + sColumnName + " LIKE", "WHERE CONVERT(VARCHAR, " + sColumnName + ", 120) LIKE").Replace(txtKeyword.Text, txtKeyword.Text.Replace("/", "-"));
                            //}
                        }
                        else
                        {
                            sSql = sSql.Replace("UPPER(" + sColumnName + ")", "CONVERT(VARCHAR, " + sColumnName + ", 111)");
                        }
                        
                        break;
                    case "PostgreSQL":
                        sSql = sPreviewSQL.Replace("$TABLE$", sTableName).Replace("$COLUMN$", sColumnName + "::TEXT").Replace("*", sColumnName);
                        break;
                }

                try
                {
                    sResult += "--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss.fff") + "\r\n" + sSql +
                               "\r\n";
                    MyGlobal.ExecuteQueryToDataTable(sSql, ref dtTemp, false); //btnSearch_Click
                }
                catch (Exception)
                {
                    //
                }

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    iResult++;
                    sResult += "--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss.fff") + ", " + MyGlobal.GetLanguageString("Keyword found!", "form", Name, "msg", "KeywordFound", "Text") + " " + sLangTableName + sTableName + ", " + sLangColumnName + sColumnName + "\r\n";

                    var row = _dtResult.NewRow();
                    row["TableName"] = sTableName;
                    row["ColumnName"] = sColumnName;
                    row["Content"] = dtTemp.Rows[0][sColumnName].ToString();
                    _dtResult.Rows.Add(row);

                    if (chkStop.Checked)
                    {
                        sResult += "--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss.fff") + ", " + chkStop.Text + "\r\n";
                        break; //找到第一筆記錄即停止搜尋
                    }
                }

                if (chkIntervalDelay.Checked)
                {
                    sResult += "--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss.fff") + ", " + MyGlobal.GetLanguageString("Delay", "form", Name, "msg", "Delay", "Text") + " " + iDelay + " " + lblMillisecond.Text + "\r\n";
                    Thread.Sleep(iDelay); //每筆查詢的間隔延遲 (毫秒)
                }

                if (btnCancel.Tag?.ToString() == "1")
                {
                    sResult += "--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss.fff") + ", " + MyGlobal.GetLanguageString("This operation has been cancelled.", "Global", "Global", "msg", "CancelByUser", "Text") + "\r\n";
                    break;
                }

                Application.DoEvents();
                progressBar1.Value++;
            }

            Application.UseWaitCursor = false;
            c1Grid.Cursor = Cursors.Default;
            btnCancel.Visible = false;
            progressBar1.Visible = false;

            grpTables.Enabled = true;
            btnSearch.Enabled = true;
            btnClose.Enabled = true;

            c1Grid.DataSource = _dtResult;

            lblSearchResultValue.Text = MyGlobal.GetLanguageString("{qty} hit(s)", "form", Name, "object", "lblSearchResultValue", "Text").Replace("{qty}", iResult.ToString());

            MyGlobal.ReplaceColumnNameByLanguageInfo(c1Grid, Name); //btnSearch_Click
            MyGlobal.ResizeGridColumnWidth(c1Grid); //btnSearch_Click

            if (chkSaveExecutedScripts.Checked)
            {
                TextEngine.WriteContentToFile(sResult, txtSaveExecutedScripts.Text, TextEncode.UTF8);
            }

            btnSearch.Focus();
        }

        private bool CheckData()
        {
            var bValue = false;

            if (string.IsNullOrEmpty(txtKeyword.Text.Trim()))
            {
                _sLangText = MyGlobal.GetLanguageString("Please enter keyword.", "form", Name, "msg", "InfoCheckKeyword", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtKeyword.Focus();
            }
            else if (chkSaveExecutedScripts.Checked)
            {
                if (string.IsNullOrEmpty(txtSaveExecutedScripts.Text))
                {
                    _sLangText = MyGlobal.GetLanguageString("Please select a script file.", "form", Name, "msg", "InfoCheckSaveFile", "Text");
                    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBrowseFile.Focus();
                }
                else if (File.Exists(txtSaveExecutedScripts.Text))
                {
                    try
                    {
                        File.Delete(txtSaveExecutedScripts.Text);
                        bValue = true;
                    }
                    catch (Exception ex)
                    {
                        _sLangText = MyGlobal.GetLanguageString("An error occurred while saving {FileType} file.", "Global", "Global", "msg", "ErrorToSaveFile", "Text");
                        _sLangText = _sLangText.Replace("{FileType}", "script");
                        MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        btnBrowseFile.Focus();
                    }
                }
                else
                {
                    bValue = true;
                }
            }
            else
            {
                bValue = true;
            }

            return bValue;
        }

        private bool CheckData2()
        {
            var bValue = false;

            if (string.IsNullOrEmpty(txtKeyword2.Text.Trim()))
            {
                _sLangText = MyGlobal.GetLanguageString("Please enter keyword.", "form", Name, "msg", "InfoCheckKeyword", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtKeyword2.Focus();
            }
            else if (chkSaveExecutedScripts2.Checked)
            {
                if (string.IsNullOrEmpty(txtSaveExecutedScripts2.Text))
                {
                    _sLangText = MyGlobal.GetLanguageString("Please select a script file.", "form", Name, "msg", "InfoCheckSaveFile", "Text");
                    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBrowseFile2.Focus();
                }
                else if (File.Exists(txtSaveExecutedScripts2.Text))
                {
                    try
                    {
                        File.Delete(txtSaveExecutedScripts2.Text);
                        bValue = true;
                    }
                    catch (Exception ex)
                    {
                        _sLangText = MyGlobal.GetLanguageString("An error occurred while saving {FileType} file.", "Global", "Global", "msg", "ErrorToSaveFile", "Text");
                        _sLangText = _sLangText.Replace("{FileType}", "script");
                        MessageBox.Show(_sLangText + "\r\n\r\n" + ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        btnBrowseFile2.Focus();
                    }
                }
                else
                {
                    bValue = true;
                }
            }
            else
            {
                bValue = true;
            }

            return bValue;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Tag = "1";
        }

        private void frmSchemaSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnCancel.Tag?.ToString() != "1")
            {
                return;
            }

            btnCancel.Tag = "0";
            e.Cancel = true;
        }

        private static bool CheckIsNonTextColumn(string sType)
        {
            var bValue = false;

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    switch (sType)
                    {
                        case "TIMESTAMP":
                        case "NUMBER":
                        case "DATE":
                        case "INTEGER":
                        case "LONG":
                            bValue = true;
                            break;
                    }

                    break;
                case "PostgreSQL":
                    switch (sType)
                    {
                        case "boolean":
                        case "int":
                        case "smallint":
                        case "bigint":
                        case "date":
                        case "datetime":
                        case "daterange":
                        case "double":
                        case "double precision":
                        case "int4range":
                        case "int8range":
                        case "integer":
                        case "numeric":
                        case "numerange":
                        case "number":
                        case "int4multirange":
                        case "int8multirange":
                        case "datemultirange":
                        case "real":
                        case "time without time zone":
                        case "timestamp with time zone":
                        case "timestamp without time zone":
                            bValue = true;
                            break;
                    }

                    break;
                case "SQL Server":
                    switch (sType.ToUpper())
                    {
                        case "TIME":
                        case "TIMESTAMP":
                        case "DECIMAL":
                        case "DATE":
                        case "DATETIME":
                        case "DATETIME2":
                        case "DATETIMEOFFSET":
                        case "BIGINT":
                        case "INT":
                        case "INTEGER":
                        case "BIT":
                        case "LONG":
                        case "FLOAT":
                        case "SMALLINT":
                        case "TINYINT":
                            bValue = true;
                            break;
                    }

                    break;
                case "MySQL":
                    switch (sType)
                    {
                        case "datetime":
                        case "decimal":
                        case "enum":
                        case "int":
                        case "bigint":
                        case "float":
                        case "double":
                        case "date":
                        case "time":
                        case "mediumint":
                        case "smallint":
                        case "timestamp":
                        case "tinyint":
                        case "year":
                        case "unsigned tinyint":
                        case "unsigned smallint":
                            bValue = true;
                            break;
                    }

                    break;
                case "SQLite":

                    break;
                case "SQLCipher":

                    break;
            }

            return bValue;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            if (CheckData2() == false)
            {
                return;
            }

            var iResult = 0;
            btnCancel.Tag = "0";
            lblSearchResultValue2.Text = "";
            InitialGrid(2);

            if (MyGlobal.dtTFVTP == null)
            {
                return;
            }

            var dt = MyGlobal.dtTFVTP;
            var iDelay = Convert.ToInt16(txtMillisecond2.Text);
            var sLangSchemaType = MyGlobal.GetLanguageString("Schema Type", "form", Name, "gridheader", "SchemaType", "Text") + ": ";
            var sLangSchemaName = MyGlobal.GetLanguageString("Schema Name", "form", Name, "gridheader", "SchemaName", "Text") + ": ";
            var sResult = "";
            var sSql2 = ""; //儲存執行過的 SQL 指令

            Application.UseWaitCursor = true;

            grpScript.Enabled = false;
            btnSearch2.Enabled = false;
            btnClose2.Enabled = false;
            btnCancel2.Visible = true;
            progressBar2.Visible = true;
            progressBar2.Value = 0;
            progressBar2.Minimum = 0;
            progressBar2.Maximum = dt.Rows.Count;
            btnCancel2.Focus();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var sSql = "";
                var sScripts = "";
                var sSchemaNode = dt.Rows[i]["SchemaNode"].ToString();
                var sSchemaType = dt.Rows[i]["SchemaType"].ToString();
                var sSchemaName = dt.Rows[i]["SchemaName"].ToString();

                switch (MyGlobal.sDataSource)
                {
                    case "Oracle":
                    {
                        sSql = "";
                        sSql += "SELECT Definition FROM (\r\n";
                        sSql += "SELECT dbms_metadata.GET_DDL('{0}', '{1}', '{2}') AS Definition FROM DUAL) s\r\n";
                        sSql += " WHERE {3} LIKE '%{4}%'";
                        sSql = string.Format(sSql, sSchemaType, sSchemaName, MyGlobal.sDBUser.ToUpper(), chkMatchCase2.Checked ? "Definition" : "UPPER(Definition)", chkMatchCase2.Checked ? txtKeyword2.Text : txtKeyword2.Text.ToUpper());

                        break;
                    }
                    case "PostgreSQL":
                    {
                        switch (sSchemaType)
                        {
                            case "TABLE":
                                if (chkTables.Checked)
                                {
                                    var sValue = MyGlobal.GetCreateScript_PostgreSQL(sSchemaNode, "Tables", sSchemaName);
                                    sScripts = sValue[0];
                                    sSql2 = sValue[1];
                                }
                                break;
                            case "FUNCTION":
                                if (chkFunctions.Checked)
                                {
                                    if (MyGlobal.sDBVersion == ">=11")
                                    {
                                        sSql = "SELECT s.Definition FROM (SELECT PG_GET_FUNCTIONDEF(p.oid) AS Definition FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE p.prokind <> 'p' and n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname <> 'trigger' AND n.nspname = '{0}' AND p.proname = '{1}') s WHERE {2} LIKE '%{3}%'";
                                    }
                                    else
                                    {
                                        sSql = "SELECT s.Definition FROM (SELECT PG_GET_FUNCTIONDEF(p.oid) AS Definition FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE NOT p.proisagg and n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname <> 'trigger' AND n.nspname = '{0}' AND p.proname = '{1}') s WHERE {2} LIKE '%{3}%'";
                                    }

                                    sSql = string.Format(sSql, sSchemaNode, sSchemaName, chkMatchCase2.Checked ? "Definition" : "UPPER(Definition)", chkMatchCase2.Checked ? txtKeyword2.Text : txtKeyword2.Text.ToUpper());
                                }
                                break;
                            case "TRIGGER":
                                if (chkTriggers.Checked)
                                {
                                    if (MyGlobal.sDBVersion == ">=11")
                                    {
                                        sSql = "SELECT s.Definition FROM (SELECT PG_GET_FUNCTIONDEF(p.oid) AS Definition FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE p.prokind <> 'p' AND n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname = 'trigger' AND n.nspname = '{0}' AND p.proname = '{1}') s WHERE {2} LIKE '%{3}%'";
                                    }
                                    else
                                    {
                                        sSql = "SELECT s.Definition FROM (SELECT PG_GET_FUNCTIONDEF(p.oid) AS Definition FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE NOT p.proisagg AND n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname = 'trigger' AND n.nspname = '{0}' AND p.proname = '{1}') s WHERE {2} LIKE '%{3}%'";
                                    }

                                    sSql = string.Format(sSql, sSchemaNode, sSchemaName, chkMatchCase2.Checked ? "Definition" : "UPPER(Definition)", chkMatchCase2.Checked ? txtKeyword2.Text : txtKeyword2.Text.ToUpper());
                                }
                                break;
                            case "VIEW":
                                if (chkViews.Checked)
                                {
                                    sSql = "SELECT s.Definition FROM (SELECT Definition FROM pg_catalog.pg_views WHERE SchemaName NOT IN ('pg_catalog', 'information_schema') AND SchemaName = '{0}' AND ViewName = '{1}') s WHERE {2} LIKE '%{3}%'";
                                    sSql = string.Format(sSql, sSchemaNode, sSchemaName, chkMatchCase2.Checked ? "Definition" : "UPPER(Definition)", chkMatchCase2.Checked ? txtKeyword2.Text : txtKeyword2.Text.ToUpper());
                                }
                                break;
                            case "PROCEDURE":
                                if (chkStoredProcedures.Checked)
                                {
                                    sSql = "SELECT s.Definition FROM (SELECT PG_GET_FUNCTIONDEF((SELECT oid FROM pg_proc WHERE Proname = '{0}')) AS Definition) s WHERE {1} LIKE '%{2}%'";
                                    sSql = string.Format(sSql, sSchemaName, chkMatchCase2.Checked ? "Definition" : "UPPER(Definition)", chkMatchCase2.Checked ? txtKeyword2.Text : txtKeyword2.Text.ToUpper());
                                }
                                break;
                        }

                        break;
                    }
                    case "SQL Server":
                    {
                        switch (sSchemaType)
                        {
                            case "TABLE":
                                if (chkTables.Checked)
                                {
                                    string[] sValue = MyGlobal.GetCreateScript_SQLServer("Tables", MyGlobal.sDBConnectionDatabase, sSchemaNode, sSchemaName);

                                    sScripts = sValue[0];
                                    sSql2 = sValue[1];
                                }
                                break;
                            case "FUNCTION":
                                if (chkFunctions.Checked)
                                {
                                    sSql = "SELECT m.Definition FROM {0}.sys.objects o INNER JOIN {0}.sys.sql_modules m ON o.Object_ID = m.Object_ID WHERE o.Type = 'FN'{2} AND o.Name = '{1}' WHERE {3} LIKE '%{4}%';";
                                    sSql = string.Format(sSql, MyGlobal.sDBConnectionDatabase, sSchemaName, MyGlobal.bDBExcludeNativeDatabase ? " AND o.Is_Ms_Shipped <> 1" : "", chkMatchCase2.Checked ? "m.Definition" : "UPPER(m.Definition)", chkMatchCase2.Checked ? txtKeyword2.Text : txtKeyword2.Text.ToUpper());
                                }
                                break;
                            case "TRIGGER":
                                if (chkTriggers.Checked)
                                {
                                    sSql = "SELECT m.Definition FROM {0}.sys.objects o INNER JOIN {0}.sys.sql_modules m ON o.Object_ID = m.Object_ID WHERE o.Type = 'TR'{2} AND o.Name = '{1}' WHERE {3} LIKE '%{4}%';";
                                    sSql = string.Format(sSql, MyGlobal.sDBConnectionDatabase, sSchemaName, MyGlobal.bDBExcludeNativeDatabase ? " AND o.Is_Ms_Shipped <> 1" : "", chkMatchCase2.Checked ? "m.Definition" : "UPPER(m.Definition)", chkMatchCase2.Checked ? txtKeyword2.Text : txtKeyword2.Text.ToUpper());
                                }
                                break;
                            case "VIEW":
                                if (chkViews.Checked)
                                {
                                    sSql = "SELECT m.Definition FROM {0}.sys.objects o INNER JOIN {0}.sys.sql_modules m ON o.Object_ID = m.Object_ID WHERE o.Type = 'V'{2} AND o.Name = '{1}' WHERE {3} LIKE '%{4}%';";
                                    sSql = string.Format(sSql, MyGlobal.sDBConnectionDatabase, sSchemaName, MyGlobal.bDBExcludeNativeDatabase ? " AND o.Is_Ms_Shipped <> 1" : "", chkMatchCase2.Checked ? "m.Definition" : "UPPER(m.Definition)", chkMatchCase2.Checked ? txtKeyword2.Text : txtKeyword2.Text.ToUpper());
                                }
                                break;
                            case "PROCEDURE":
                                if (chkStoredProcedures.Checked)
                                {
                                    sSql = "SELECT m.Definition FROM {0}.sys.objects o INNER JOIN {0}.sys.sql_modules m ON o.Object_ID = m.Object_ID WHERE o.Type = 'P'{2} AND o.Name = '{1}' WHERE {3} LIKE '%{4}%';";
                                    sSql = string.Format(sSql, MyGlobal.sDBConnectionDatabase, sSchemaName, MyGlobal.bDBExcludeNativeDatabase ? " AND o.Is_Ms_Shipped <> 1" : "", chkMatchCase2.Checked ? "m.Definition" : "UPPER(m.Definition)", chkMatchCase2.Checked ? txtKeyword2.Text : txtKeyword2.Text.ToUpper());
                                }
                                break;
                        }

                        break;
                    }
                    case "MySQL":
                    {
                        var dtTemp2 = new DataTable();

                        switch (sSchemaType)
                        {
                            case "TABLE":
                                sSql2 = "SHOW CREATE TABLE `{0}`.`{1}`;";
                                sSql2 = string.Format(sSql2, sSchemaNode, sSchemaName);
                                MyGlobal.ExecuteQueryToDataTable(sSql2, ref dtTemp2, false); //btnSearch2_Click

                                if (dtTemp2 != null && dtTemp2.Rows.Count > 0)
                                {
                                    sScripts = dtTemp2.Rows[0]["Create Table"].ToString();
                                }

                                break;
                            case "FUNCTION":
                                sSql2 = "SHOW CREATE FUNCTION `{0}`.`{1}`;";
                                sSql2 = string.Format(sSql2, sSchemaNode, sSchemaName);
                                MyGlobal.ExecuteQueryToDataTable(sSql2, ref dtTemp2, false); //btnSearch2_Click

                                if (dtTemp2 != null && dtTemp2.Rows.Count > 0)
                                {
                                    sScripts = dtTemp2.Rows[0]["Create Function"].ToString();
                                }

                                break;
                            case "TRIGGER":
                                sSql2 = "SHOW CREATE TRIGGER `{0}`.`{1}`;";
                                sSql2 = string.Format(sSql2, sSchemaNode, sSchemaName);
                                MyGlobal.ExecuteQueryToDataTable(sSql2, ref dtTemp2, false); //btnSearch2_Click

                                if (dtTemp2 != null && dtTemp2.Rows.Count > 0)
                                {
                                    sScripts = dtTemp2.Rows[0]["SQL Original Statement"].ToString();
                                }

                                break;
                            case "VIEW":
                                sSql2 = "SHOW CREATE VIEW `{0}`.`{1}`;";
                                sSql2 = string.Format(sSql2, sSchemaNode, sSchemaName);
                                MyGlobal.ExecuteQueryToDataTable(sSql2, ref dtTemp2, false); //btnSearch2_Click

                                if (dtTemp2 != null && dtTemp2.Rows.Count > 0)
                                {
                                    sScripts = dtTemp2.Rows[0]["Create View"].ToString();
                                }

                                break;
                            case "PROCEDURE":
                                sSql2 = "SHOW CREATE PROCEDURE `{0}`.`{1}`;";
                                sSql2 = string.Format(sSql2, sSchemaNode, sSchemaName);
                                MyGlobal.ExecuteQueryToDataTable(sSql2, ref dtTemp2, false); //btnSearch2_Click

                                if (dtTemp2 != null && dtTemp2.Rows.Count > 0)
                                {
                                    sScripts = dtTemp2.Rows[0]["Create Procedure"].ToString();
                                }

                                break;
                        }

                        break;
                    }
                    case "SQLite":
                    case "SQLCipher":
                    {
                        switch (sSchemaType)
                        {
                            case "TABLE":

                                break;
                            case "FUNCTION":

                                break;
                            case "TRIGGER":

                                break;
                            case "VIEW":

                                break;
                            case "PROCEDURE":

                                break;
                        }

                        break;
                    }
                }

                var dtTemp = new DataTable();

                try
                {
                    if (!string.IsNullOrEmpty(sSql))
                    {
                        sResult += "--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss.fff") + "\r\n" +
                                   sSql + "\r\n";
                        MyGlobal.ExecuteQueryToDataTable(sSql, ref dtTemp, false); //btnSearch2_Click
                    }
                    else
                    {
                        sSql = sSql2;
                        sResult += "--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss.fff") + "\r\n" +
                                   sSql + "\r\n";
                    }
                }
                catch (Exception)
                {
                    // ignored
                }

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    iResult++;
                    sResult += "--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss.fff") + ", " + MyGlobal.GetLanguageString("Keyword found!", "form", Name, "msg", "KeywordFound", "Text") + " " + sLangSchemaType + sSchemaType + ", " + sLangSchemaName + sSchemaName + "\r\n";

                    var row = _dtResult2.NewRow();
                    row["SchemaType"] = sSchemaType;
                    row["SchemaName"] = sSchemaName;
                    row["Content"] = dtTemp.Rows[0]["Definition"].ToString();
                    _dtResult2.Rows.Add(row);
                }
                else if (!string.IsNullOrEmpty(sScripts)) //SQL Server's Table Scripts
                {
                    var bMatch = false;

                    if (chkMatchCase2.Checked)
                    {
                        if (sScripts.IndexOf(txtKeyword2.Text, StringComparison.Ordinal) != -1)
                        {
                            bMatch = true;
                        }
                    }
                    else
                    {
                        if (sScripts.ToUpper().IndexOf(txtKeyword2.Text.ToUpper(), StringComparison.Ordinal) != -1)
                        {
                            bMatch = true;
                        }
                    }

                    if (bMatch)
                    {
                        iResult++;
                        sResult += "--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss.fff") + ", " + MyGlobal.GetLanguageString("Keyword found!", "form", Name, "msg", "KeywordFound", "Text") + " " + sLangSchemaType + sSchemaType + ", " + sLangSchemaName + sSchemaName + "\r\n";

                        var row = _dtResult2.NewRow();
                        row["SchemaType"] = sSchemaType;
                        row["SchemaName"] = sSchemaName;
                        row["Content"] = sScripts;
                        _dtResult2.Rows.Add(row);
                    }
                }

                if (chkIntervalDelay2.Checked)
                {
                    sResult += "--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss.fff") + ", " + MyGlobal.GetLanguageString("Delay", "form", Name, "msg", "Delay", "Text") + " " + iDelay + " " + lblMillisecond2.Text + "\r\n";
                    Thread.Sleep(iDelay); //每筆查詢的間隔延遲 (毫秒)
                }

                if (btnCancel.Tag?.ToString() == "1")
                {
                    sResult += "--" + DateTime.Now.ToString(MyLibrary.sDateFormat + " HH:mm:ss.fff") + ", " + MyGlobal.GetLanguageString("This operation has been cancelled.", "Global", "Global", "msg", "CancelByUser", "Text") + "\r\n";
                    break;
                }

                Application.DoEvents();
                progressBar2.Value++;
            }

            Application.UseWaitCursor = false;
            c1Grid2.Cursor = Cursors.Default;
            btnCancel2.Visible = false;
            progressBar2.Visible = false;

            grpScript.Enabled = true;
            btnSearch2.Enabled = true;
            btnClose2.Enabled = true;

            c1Grid2.DataSource = _dtResult2;

            lblSearchResultValue2.Text = MyGlobal.GetLanguageString("{qty} hit(s)", "form", Name, "object", "lblSearchResultValue", "Text").Replace("{qty}", iResult.ToString());

            MyGlobal.ReplaceColumnNameByLanguageInfo(c1Grid2, Name); //btnSearch2_Click
            MyGlobal.ResizeGridColumnWidth(c1Grid2); //btnSearch2_Click

            if (chkSaveExecutedScripts2.Checked)
            {
                TextEngine.WriteContentToFile(sResult, txtSaveExecutedScripts2.Text, TextEncode.UTF8);
            }

            btnSearch2.Focus();
        }

        private void Scripts_CheckedChanged(object sender, EventArgs e)
        {
            var chk = sender as CheckBox;

            if (chk.Checked)
            {
                return; //只針對取消勾選才要檢查
            }

            var i = 0;

            if (grpScript.Controls.Cast<Control>().Any(c => c.Tag?.ToString() == "s" && ((CheckBox)c).Checked))
            {
                i++;
            }

            if (i == 0)
            {
                chkTables.Checked = true;
            }
        }

        private void c1Grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var iRow = c1Grid.RowContaining(e.Y);

            if (iRow != -1)
            {
                CellViewer(c1Grid);
            }
        }

        private void c1Grid_MouseDown(object sender, MouseEventArgs e)
        {
            var iRow = c1Grid.RowContaining(e.Y);
            var iCol = c1Grid.ColContaining(e.X);

            if (e.Button == MouseButtons.Right)
            {
                if (iRow != -1)
                {
                    var cMenuGrid = new ContextMenuStrip();
                    MyGlobal.MouseDownDataGridExportDataToFile(c1Grid, e.X, e.Y, cMenuGrid);
                }
            }
        }

        private void c1Grid2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var iRow = c1Grid2.RowContaining(e.Y);

            if (iRow != -1)
            {
                CellViewer(c1Grid2);
            }
        }

        private void c1Grid2_MouseDown(object sender, MouseEventArgs e)
        {
            var iRow = c1Grid2.RowContaining(e.Y);
            var iCol = c1Grid2.ColContaining(e.X);

            if (e.Button == MouseButtons.Right)
            {
                if (iRow != -1)
                {
                    var cMenuGrid = new ContextMenuStrip();
                    MyGlobal.MouseDownDataGridExportDataToFile(c1Grid2, e.X, e.Y, cMenuGrid);
                }
            }
        }

        private void CellViewer(C1.Win.C1TrueDBGrid.C1TrueDBGrid c1Grid)
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
    }
}