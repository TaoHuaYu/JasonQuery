using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using Devart.Data.SqlServer;
using Devart.Data.MySql;
using Devart.Data.SQLite;
using Devart.Data.PostgreSql;
using JasonLibrary.Class;
using JasonLibrary.ColorPicker;

namespace JasonQuery
{
    public partial class frmConnect : Form
    {
        private DataTable _dtDbInfo;
        private DataTable _dtDatabaseListInfo_PostgreSQL; //database 欄位下拉清單 (各自獨立，避免相互干擾)
        private DataTable _dtDatabaseListInfo_SQLServer; //database 欄位下拉清單 (各自獨立，避免相互干擾)
        private DataTable _dtDatabaseListInfo_MySQL; //database 欄位下拉清單 (各自獨立，避免相互干擾)
        private DataRow _rowDbInfo;
        private List<Control> _lstPicLogo;
        private List<string> _lstSupportInfo = new List<string>();
        private List<string> _lstGridHeader = new List<string>();
        private bool _bFormLoadFinish; //表單是否載入完畢 (避免觸發事件)
        private ToolTip _toolTip1 = new ToolTip();
        private string _sPanelColorSelectedName = "";
        private List<Control> _lstPanelTabColor;
        private string _sLangText = "";
        public event ValueUpdatedEventHandler ValueUpdated;
        private string _sDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private Color _cEssentialField = Color.LightYellow;
        private Color _cOptionalField = ColorTranslator.FromHtml("#EAF2FF");
        private string sPassword1;
        private string sPassword2;
        private bool _bComboBoxKeypress4Database = false;
        private bool _bKeyPressTab = false; //記住是否按下 Tab 鍵 (KeyUp 可以偵測 Tab，但 Tab 鍵已被提前觸發了，故不在 KeyUp 攔截)
        private bool _bKeyPressESC = false; //記住是否按下 ESC 鍵
        private bool _bKeyPressDelete = false; //記住是否按下 Delete 鍵

        private enum eMenu
        {
            ePID = 0,
            eDomainUser,
            eDataSource,
            eConnectionName,
            eServer,
            eSID,
            eDirectMode,
            eDatabase,
            eConnectAs,
            ePort,
            eUserID,
            ePassword,
            eLastConnect,
            eTabBackColor,
            eTabActiveForeColor,
            eTabInactiveForeColor,
            eUnicode,
            eAutoRollback,
            ePooling,
            eExcludeNativeDatabases,
            eRemarks,
            eDatabaseFile,
            eDatabaseType,
            eWithPassword
        }

        [DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int WNetGetConnection([MarshalAs(UnmanagedType.LPTStr)] string localName,
                                                   [MarshalAs(UnmanagedType.LPTStr)] StringBuilder remoteName,
                                                   ref int length);

        //for MessageBox's Position
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(IntPtr classname, string title); // extern method: FindWindow

        [DllImport("user32.dll")]
        private static extern void MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool rePaint); // extern method: MoveWindow

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rect); // extern method: GetWindowRect

        public frmConnect()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            pnlOracle.Location = new Point(8, 46);
            pnlPostgreSQL.Location = new Point(8, 46);
            pnlSQLServer.Location = new Point(8, 46);
            pnlMySQL.Location = new Point(8, 46);
            pnlSQLite.Location = new Point(8, 46);

            c1GridPostgreSQL.Splits[0].RecordSelectors = false;
            c1GridPostgreSQL.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);
            MyGlobal.SetGridVisualStyle(c1GridPostgreSQL, 10); //Form_Load

            c1GridSQLServer.Splits[0].RecordSelectors = false;
            c1GridSQLServer.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);
            MyGlobal.SetGridVisualStyle(c1GridSQLServer, 10); //Form_Load

            c1GridMySQL.Splits[0].RecordSelectors = false;
            c1GridMySQL.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);
            MyGlobal.SetGridVisualStyle(c1GridMySQL, 10); //Form_Load

            if (IntPtr.Size == 8)
            {
                cboDatabaseType_SQLite.Items.Add("SQLCipher(Community)");
            }

            cboDatabaseType_SQLite.Items.Add("System.Data.SQLite RC4");

            sPassword1 = "";

            cboDataSource.BackColor = _cEssentialField;

            txtConnectionName_Oracle.BackColor = _cEssentialField;
            txtServer_Oracle.BackColor = _cEssentialField;
            txtPort_Oracle.BackColor = _cEssentialField;
            txtUserID_Oracle.BackColor = _cEssentialField;
            txtPassword_Oracle.BackColor = _cEssentialField;
            cboConnectAs_Oracle.BackColor = _cEssentialField;

            txtConnectionName_PostgreSQL.BackColor = _cEssentialField;
            txtServer_PostgreSQL.BackColor = _cEssentialField;
            cboDatabase_PostgreSQL.BackColor = _cEssentialField;
            txtPort_PostgreSQL.BackColor = _cEssentialField;
            txtUserID_PostgreSQL.BackColor = _cEssentialField;
            txtPassword_PostgreSQL.BackColor = _cEssentialField;

            txtConnectionName_SQLServer.BackColor = _cEssentialField;
            txtServer_SQLServer.BackColor = _cEssentialField;
            //cboDatabase_SQLServer.BackColor = _cEssentialField;
            txtUserID_SQLServer.BackColor = _cEssentialField;
            txtPassword_SQLServer.BackColor = _cEssentialField;

            txtConnectionName_MySQL.BackColor = _cEssentialField;
            txtServer_MySQL.BackColor = _cEssentialField;
            txtUserID_MySQL.BackColor = _cEssentialField;
            txtPassword_MySQL.BackColor = _cEssentialField;

            txtConnectionName_SQLite.BackColor = _cEssentialField;
            txtFile_SQLite.BackColor = _cEssentialField;

            MyGlobal.SetC1ComboBoxItemsFromDictionary(cboLocalization, MyGlobal.dicLocalization, true);
            cboLocalization.Text = MyGlobal.sLocalization;
            cboLocalization.Tag = MyGlobal.sLocalization;

            _lstPanelTabColor = new List<Control>
            {
                pnlBackColor,
                pnlActiveForeColor,
                pnlInactiveForeColor
            };

            btnCreateShortcut.Enabled = !File.Exists(_sDesktopPath + @"\JasonQuery.lnk");

            _lstPicLogo = new List<Control>
            {
                picOracle,
                picPostgreSQL,
                picSQLServer,
                picMySQL,
                picSQLite
            };

            ApplyLocalizationSetting(); //Form_Load

            lblDomainUser.Text = MyGlobal.sDomainUser;

            //判斷是否透過檔案總管呼叫 JasonQuery.exe 並開啟指定的 sql 檔案
            if (!string.IsNullOrEmpty(MyGlobal.sOpenFileFromExternal))
            {
                txtOpenSpecifiedFile.Text = MyGlobal.sOpenFileFromExternal;

                grpOpenFile.Visible = true;
                c1GridDBInfo.Location = new Point(10, 350);
                c1GridDBInfo.Size = new Size(773, 125);
            }

            _bFormLoadFinish = true;

            if (c1GridDBInfo.RowCount > 0)
            {
                btnConnect.Focus();
            }
            else
            {
                btnNew.PerformClick();
            }

            //Form_Load 還原以下初始值，避免連線有誤時，再次開啟連線視窗又馬上關閉時，又再嘗試連線而引發錯誤
            MyGlobal.sDataSource = "";
            MyGlobal.sTabBackColor = "";
            MyGlobal.sTabActiveForeColor = "";
            MyGlobal.sTabInactiveForeColor = "";
        }

        private int CreateAndGetDbInfoTable()
        {
            const string sHideFields = "`PID`DirectMode`SID`ConnectAs`Port`Database`DomainUser`Password`TabActiveForeColor`TabInactiveForeColor`Unicode`AutoRollback`Pooling`ExcludeNativeDatabases`DatabaseFile`DatabaseType`WithPassword`";

            _lstGridHeader = new List<string>
            {
                "PID",
                "DomainUser",
                "DataSource",
                "ConnectionName",
                "Server",
                "SID",
                "DirectMode",
                "Database",
                "ConnectAs",
                "Port",
                "UserID",
                "Password",
                "LastConnect",
                "TabBackColor",
                "TabActiveForeColor",
                "TabInactiveForeColor",
                "Unicode",
                "AutoRollback",
                "Pooling",
                "ExcludeNativeDatabases",
                "Remarks",
                "DatabaseFile",
                "DatabaseType",
                "WithPassword"
            };

            _dtDbInfo = new DataTable();

            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.ePID]); //Hide
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eDomainUser]); //Hide
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eDataSource]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eConnectionName]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eServer]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eSID]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eDirectMode]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eDatabase]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eConnectAs]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.ePort]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eUserID]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.ePassword]); //Hide
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eLastConnect]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eTabBackColor]); //Hide
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eTabActiveForeColor]); //Hide
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eTabInactiveForeColor]); //Hide
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eUnicode]); //Hide
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eAutoRollback]); //Hide
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.ePooling]); //Hide
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eExcludeNativeDatabases]); //Hide
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eRemarks]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eDatabaseFile]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eDatabaseType]);
            _dtDbInfo.Columns.Add(_lstGridHeader[(int)eMenu.eWithPassword]);

            var dt = DBCommon.ExecQuery("SELECT * FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' ORDER BY LastConnect DESC");

            btnDelete.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                for (var iRow = 0; iRow < dt.Rows.Count; iRow++)
                {
                    _rowDbInfo = _dtDbInfo.NewRow();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.ePID]] = dt.Rows[iRow]["PID"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eDomainUser]] = dt.Rows[iRow]["DomainUser"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eConnectionName]] = dt.Rows[iRow]["ConnectionName"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eDataSource]] = dt.Rows[iRow]["DataSource"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eServer]] = dt.Rows[iRow]["Server"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eSID]] = dt.Rows[iRow]["SID"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eDirectMode]] = dt.Rows[iRow]["DirectMode"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eDatabase]] = dt.Rows[iRow]["Database"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eConnectAs]] = dt.Rows[iRow]["ConnectAs"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.ePort]] = dt.Rows[iRow]["Port"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eUserID]] = dt.Rows[iRow]["User"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.ePassword]] = dt.Rows[iRow]["Password"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eLastConnect]] = dt.Rows[iRow]["LastConnect"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eTabBackColor]] = dt.Rows[iRow]["TabBackColor"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eTabActiveForeColor]] = dt.Rows[iRow]["TabActiveForeColor"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eTabInactiveForeColor]] = dt.Rows[iRow]["TabInactiveForeColor"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eUnicode]] = dt.Rows[iRow]["Unicode"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eAutoRollback]] = dt.Rows[iRow]["AutoRollback"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.ePooling]] = dt.Rows[iRow]["o1"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eExcludeNativeDatabases]] = dt.Rows[iRow]["o2"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eRemarks]] = dt.Rows[iRow]["Remarks"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eDatabaseFile]] = dt.Rows[iRow]["o3"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eDatabaseType]] = dt.Rows[iRow]["o4"].ToString();
                    _rowDbInfo[_lstGridHeader[(int)eMenu.eWithPassword]] = dt.Rows[iRow]["o5"].ToString();

                    _dtDbInfo.Rows.Add(_rowDbInfo);
                }

                btnDelete.Enabled = true;
            }

            //---------------------------------------------------------------------------------------------------------------

            c1GridDBInfo.DataSource = _dtDbInfo;

            foreach (C1DisplayColumn col in c1GridDBInfo.Splits[0].DisplayColumns)
            {
                if (sHideFields.Contains("`" + col.Name + "`"))
                {
                    col.Visible = false;
                    col.Frozen = true;
                }
                else if (col.Name == "Remarks")
                {
                    col.Width = 150;
                }
                else
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

                col.Style.VerticalAlignment = AlignVertEnum.Center;
            }

            MyGlobal.ReplaceColumnNameByLanguageInfo(c1GridDBInfo, Name, true); //CreateAndGetDbInfoTable

            c1GridDBInfo.RowHeight = 20;
            c1GridDBInfo.Splits[0].ColumnCaptionHeight = 20;
            c1GridDBInfo.Refresh();

            return dt.Rows.Count;
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            txtDecode.Text = TextEngine.Encode(TextEngine.Encrypt(txtEncode.Text, MyGlobal.sDomainUser));
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            txtEncode.Text = TextEngine.Decrypt(TextEngine.Decode(txtDecode.Text), MyGlobal.sDomainUser);
        }

        private void c1GridDBInfo_RowColChange(object sender, RowColChangeEventArgs e)
        {
            if (c1GridDBInfo.RowCount == 0)
            {
                return;
            }

            var iRow = c1GridDBInfo.Row;

            cboDataSource.Enabled = true;

            if (cboDataSource.Items.Contains(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDataSource]].CellValue(iRow).ToString()))
            {
                MyGlobal.SetC1ComboBoxTextAndTriggerSelectedIndex(cboDataSource, c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDataSource]].CellValue(iRow).ToString());
            }
            else
            {
                cboDataSource.SelectedIndex = -1;
            }

            cboDataSource.Enabled = false;

            switch (cboDataSource.Text)
            {
                case "Oracle":
                    Grid_RowColChange_Oracle();
                    break;
                case "PostgreSQL":
                    _dtDatabaseListInfo_PostgreSQL = null; //切換時要清空！
                    Grid_RowColChange_PostgreSQL();
                    break;
                case "SQL Server":
                    _dtDatabaseListInfo_SQLServer = null; //切換時要清空！
                    Grid_RowColChange_SQLServer();
                    break;
                case "MySQL/MariaDB":
                    _dtDatabaseListInfo_MySQL = null; //切換時要清空！
                    Grid_RowColChange_MySQL();
                    break;
                case "SQLite":
                    Grid_RowColChange_SQLite();
                    break;
            }
        }

        private void Grid_RowColChange_Oracle()
        {
            var iRow = c1GridDBInfo.Row;
            var sPasswordResult = "";

            lblPID.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePID]].CellValue(iRow).ToString();
            lblDomainUser.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDomainUser]].CellValue(iRow).ToString();

            //Text & Tag：當 Update Mode 時，如果 Connection Name 有變更，檢查是否已經有重覆的 Connection Name 存在了
            txtConnectionName_Oracle.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eConnectionName]].CellValue(iRow).ToString();
            txtConnectionName_Oracle.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eConnectionName]].CellValue(iRow).ToString();

            txtServer_Oracle.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eServer]].CellValue(iRow).ToString();
            MyGlobal.SetC1TextBoxText(txtSID_Oracle, c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eSID]].CellValue(iRow).ToString());
            chkDirectMode_Oracle.Checked = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDirectMode]].CellValue(iRow).ToString() == "1";
            txtPort_Oracle.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePort]].CellValue(iRow).ToString();
            
            pnlBackColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabBackColor]].CellValue(iRow).ToString());
            pnlBackColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabBackColor]].CellValue(iRow).ToString();
            pnlActiveForeColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabActiveForeColor]].CellValue(iRow).ToString());
            pnlActiveForeColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabActiveForeColor]].CellValue(iRow).ToString();
            pnlInactiveForeColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabInactiveForeColor]].CellValue(iRow).ToString());
            pnlInactiveForeColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabInactiveForeColor]].CellValue(iRow).ToString();

            _toolTip1.SetToolTip(pnlBackColor, pnlBackColor.Tag + " " + "(R:" + pnlBackColor.BackColor.R + ", G:" + pnlBackColor.BackColor.G + ", B:" + pnlBackColor.BackColor.B + ")");
            _toolTip1.SetToolTip(pnlActiveForeColor, pnlActiveForeColor.Tag + " " + "(R:" + pnlActiveForeColor.BackColor.R + ", G:" + pnlActiveForeColor.BackColor.G + ", B:" + pnlActiveForeColor.BackColor.B + ")");
            _toolTip1.SetToolTip(pnlInactiveForeColor, pnlInactiveForeColor.Tag + " " + "(R:" + pnlInactiveForeColor.BackColor.R + ", G:" + pnlInactiveForeColor.BackColor.G + ", B:" + pnlInactiveForeColor.BackColor.B + ")");

            tabExample.BackColor = pnlBackColor.BackColor;
            tabExample.ForeColor = pnlActiveForeColor.BackColor; //Color.Black;
            tabExample.TextInactiveColor = pnlInactiveForeColor.BackColor;

            txtUserID_Oracle.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eUserID]].CellValue(iRow).ToString();

            if (cboConnectAs_Oracle.Items.Contains(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eConnectAs]].CellValue(iRow).ToString()))
            {
                MyGlobal.SetC1ComboboxTextIgnoreVisible(cboConnectAs_Oracle, c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eConnectAs]].CellValue(iRow).ToString());
            }
            else
            {
                cboConnectAs_Oracle.SelectedIndex = -1; //Visible=false, 不會引發錯誤
            }

            txtRemark_Oracle.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eRemarks]].CellValue(iRow).ToString();

            btnCopy.Enabled = true;
            btnDelete.Enabled = true;

            var bValue = false;
            var sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + lblPID.Text + " AND AttributeKey = 'GeneralConfig' AND AttributeName = 'DarkMode'";
            var dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkDarkMode.Checked = bValue;

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + lblPID.Text + " AND AttributeKey = 'EditorConfig' AND AttributeName = 'ShowColumnInfo'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkShowColumnInfo.Checked = bValue;

            chkPooling_Oracle.Checked = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePooling]].CellValue(iRow).ToString() == "1";
            chkUnicode_Oracle.Checked = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eUnicode]].CellValue(iRow).ToString() == "1";

            //這裡改為個別判斷，才不會出現「先勾再取消」的「特殊狀況」
            chkSavePasswords.Checked = !string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString());

            if (!string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int) eMenu.ePassword]].CellValue(iRow).ToString()))
            {
                sPasswordResult = TextEngine.Decrypt(TextEngine.Decode(c1GridDBInfo.Columns[_lstGridHeader[(int) eMenu.ePassword]].CellValue(iRow).ToString()), MyGlobal.sDomainUser);
            }

            if (string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()) && string.IsNullOrEmpty(txtPassword_Oracle.Tag?.ToString()))
            {
                txtPassword_Oracle.Text = "";
                txtPassword_Oracle.Focus();
            }
            else if (string.IsNullOrEmpty(sPasswordResult) && !string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()))
            {
                txtPassword_Oracle.Text = "";
                _sLangText = MyGlobal.GetLanguageString("Wrong password!", "form", Name, "msg", "WrongPassword", "Text");
                var sMsg = _sLangText + "\r\n\r\n";
                _sLangText = MyGlobal.GetLanguageString("Could not resolve your password correctly.", "form", Name, "msg", "Resolve", "Text");
                sMsg += _sLangText + "\r\n";
                _sLangText = MyGlobal.GetLanguageString("You must re-enter your password.", "form", Name, "msg", "ReEnter", "Text");
                sMsg += _sLangText;
                MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword_Oracle.Focus();
            }
            else
            {
                txtPassword_Oracle.Text = string.IsNullOrEmpty(sPasswordResult) ? txtPassword_Oracle.Tag?.ToString() : sPasswordResult;
            }

            cboDataSource.Focus();
            txtConnectionName_Oracle.Focus();
        }

        private void Grid_RowColChange_PostgreSQL()
        {
            var iRow = c1GridDBInfo.Row;

            lblPID.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePID]].CellValue(iRow).ToString();
            lblDomainUser.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDomainUser]].CellValue(iRow).ToString();

            //Text & Tag：當 Update Mode 時，如果 Connection Name 有變更，檢查是否已經有重覆的 Connection Name 存在了
            txtConnectionName_PostgreSQL.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eConnectionName]].CellValue(iRow).ToString();
            txtConnectionName_PostgreSQL.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eConnectionName]].CellValue(iRow).ToString();

            txtServer_PostgreSQL.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eServer]].CellValue(iRow).ToString();
            txtPort_PostgreSQL.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePort]].CellValue(iRow).ToString();
            cboDatabase_PostgreSQL.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDatabase]].CellValue(iRow).ToString();
            cboDatabase_PostgreSQL.BackColor = _cEssentialField;

            chkPooling_PostgreSQL.Checked = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePooling]].CellValue(iRow).ToString() == "1";
            chkUnicode_PostgreSQL.Checked = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eUnicode]].CellValue(iRow).ToString() == "1";
            chkAutoRollback_PostgreSQL.Checked = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eAutoRollback]].CellValue(iRow).ToString() == "1";

            pnlBackColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabBackColor]].CellValue(iRow).ToString());
            pnlBackColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabBackColor]].CellValue(iRow).ToString();
            pnlActiveForeColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabActiveForeColor]].CellValue(iRow).ToString());
            pnlActiveForeColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabActiveForeColor]].CellValue(iRow).ToString();
            pnlInactiveForeColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabInactiveForeColor]].CellValue(iRow).ToString());
            pnlInactiveForeColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabInactiveForeColor]].CellValue(iRow).ToString();

            _toolTip1.SetToolTip(pnlBackColor, pnlBackColor.Tag + " " + "(R:" + pnlBackColor.BackColor.R + ", G:" + pnlBackColor.BackColor.G + ", B:" + pnlBackColor.BackColor.B + ")");
            _toolTip1.SetToolTip(pnlActiveForeColor, pnlActiveForeColor.Tag + " " + "(R:" + pnlActiveForeColor.BackColor.R + ", G:" + pnlActiveForeColor.BackColor.G + ", B:" + pnlActiveForeColor.BackColor.B + ")");
            _toolTip1.SetToolTip(pnlInactiveForeColor, pnlInactiveForeColor.Tag + " " + "(R:" + pnlInactiveForeColor.BackColor.R + ", G:" + pnlInactiveForeColor.BackColor.G + ", B:" + pnlInactiveForeColor.BackColor.B + ")");

            tabExample.BackColor = pnlBackColor.BackColor;
            tabExample.ForeColor = pnlActiveForeColor.BackColor; //Color.Black;
            tabExample.TextInactiveColor = pnlInactiveForeColor.BackColor;

            txtUserID_PostgreSQL.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eUserID]].CellValue(iRow).ToString();
            txtRemark_PostgreSQL.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eRemarks]].CellValue(iRow).ToString();

            var bValue = false;
            var sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + lblPID.Text + " AND AttributeKey = 'GeneralConfig' AND AttributeName = 'DarkMode'";
            var dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            btnDelete.Enabled = true;
            btnCopy.Enabled = true;
            chkDarkMode.Checked = bValue;

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + lblPID.Text + " AND AttributeKey = 'EditorConfig' AND AttributeName = 'ShowColumnInfo'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkShowColumnInfo.Checked = bValue;

            var sPasswordResult = TextEngine.Decrypt(TextEngine.Decode(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()), MyGlobal.sDomainUser);

            //這裡改為各別判斷，才不會出現「先勾再取消」的「特殊狀況」
            chkSavePasswords.Checked = !string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString());

            if (string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()) && string.IsNullOrEmpty((txtPassword_PostgreSQL.Tag ?? string.Empty).ToString()))
            {
                txtPassword_PostgreSQL.Text = "";
                txtPassword_PostgreSQL.Focus();
            }
            else if (string.IsNullOrEmpty(sPasswordResult) && !string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()))
            {
                txtPassword_PostgreSQL.Text = "";
                _sLangText = MyGlobal.GetLanguageString("Wrong password!", "form", Name, "msg", "WrongPassword", "Text");
                var sMsg = _sLangText + "\r\n\r\n";
                _sLangText = MyGlobal.GetLanguageString("Could not resolve your password correctly.", "form", Name, "msg", "Resolve", "Text");
                sMsg += _sLangText + "\r\n";
                _sLangText = MyGlobal.GetLanguageString("You must re-enter your password.", "form", Name, "msg", "ReEnter", "Text");
                sMsg += _sLangText;
                MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword_PostgreSQL.Focus();
            }
            else
            {
                txtPassword_PostgreSQL.Text = string.IsNullOrEmpty(sPasswordResult) ? txtPassword_PostgreSQL.Tag?.ToString() : sPasswordResult;
            }

            txtConnectionName_PostgreSQL.Focus();
        }

        private void Grid_RowColChange_SQLServer()
        {
            var iRow = c1GridDBInfo.Row;

            lblPID.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePID]].CellValue(iRow).ToString();
            lblDomainUser.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDomainUser]].CellValue(iRow).ToString();

            //Text & Tag：當 Update Mode 時，如果 Connection Name 有變更，檢查是否已經有重覆的 Connection Name 存在了
            txtConnectionName_SQLServer.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eConnectionName]].CellValue(iRow).ToString();
            txtConnectionName_SQLServer.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eConnectionName]].CellValue(iRow).ToString();

            txtServer_SQLServer.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eServer]].CellValue(iRow).ToString();
            cboDatabase_SQLServer.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDatabase]].CellValue(iRow).ToString();
            txtPort_SQLServer.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePort]].CellValue(iRow).ToString();
            txtPort_SQLServer.Text = txtPort_SQLServer.Text == "0" ? @"1433" : txtPort_SQLServer.Text;

            pnlBackColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabBackColor]].CellValue(iRow).ToString());
            pnlBackColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabBackColor]].CellValue(iRow).ToString();
            pnlActiveForeColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabActiveForeColor]].CellValue(iRow).ToString());
            pnlActiveForeColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabActiveForeColor]].CellValue(iRow).ToString();
            pnlInactiveForeColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabInactiveForeColor]].CellValue(iRow).ToString());
            pnlInactiveForeColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabInactiveForeColor]].CellValue(iRow).ToString();

            _toolTip1.SetToolTip(pnlBackColor, pnlBackColor.Tag + " " + "(R:" + pnlBackColor.BackColor.R + ", G:" + pnlBackColor.BackColor.G + ", B:" + pnlBackColor.BackColor.B + ")");
            _toolTip1.SetToolTip(pnlActiveForeColor, pnlActiveForeColor.Tag + " " + "(R:" + pnlActiveForeColor.BackColor.R + ", G:" + pnlActiveForeColor.BackColor.G + ", B:" + pnlActiveForeColor.BackColor.B + ")");
            _toolTip1.SetToolTip(pnlInactiveForeColor, pnlInactiveForeColor.Tag + " " + "(R:" + pnlInactiveForeColor.BackColor.R + ", G:" + pnlInactiveForeColor.BackColor.G + ", B:" + pnlInactiveForeColor.BackColor.B + ")");

            tabExample.BackColor = pnlBackColor.BackColor;
            tabExample.ForeColor = pnlActiveForeColor.BackColor; //Color.Black;
            tabExample.TextInactiveColor = pnlInactiveForeColor.BackColor;

            txtUserID_SQLServer.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eUserID]].CellValue(iRow).ToString();
            txtRemark_SQLServer.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eRemarks]].CellValue(iRow).ToString();

            var bValue = false;
            var sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + lblPID.Text + " AND AttributeKey = 'GeneralConfig' AND AttributeName = 'DarkMode'";
            var dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            btnDelete.Enabled = true;
            btnCopy.Enabled = true;
            chkDarkMode.Checked = bValue;

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + lblPID.Text + " AND AttributeKey = 'EditorConfig' AND AttributeName = 'ShowColumnInfo'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkShowColumnInfo.Checked = bValue;

            var sPasswordResult = TextEngine.Decrypt(TextEngine.Decode(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()), MyGlobal.sDomainUser);

            chkPooling_SQLServer.Checked = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePooling]].CellValue(iRow).ToString() == "1";

            var sTemp = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eExcludeNativeDatabases]].CellValue(iRow).ToString();
            chkExcludeNativeObject_SQLServer.Checked = sTemp == "1" || string.IsNullOrEmpty(sTemp);

            //這裡改為各別判斷，才不會出現「先勾再取消」的「特殊狀況」
            chkSavePasswords.Checked = !string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString());

            if (string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()) && string.IsNullOrEmpty((txtPassword_SQLServer.Tag ?? string.Empty).ToString()))
            {
                txtPassword_SQLServer.Text = "";
                txtPassword_SQLServer.Focus();
            }
            else if (string.IsNullOrEmpty(sPasswordResult) && !string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()))
            {
                txtPassword_SQLServer.Text = "";
                _sLangText = MyGlobal.GetLanguageString("Wrong password!", "form", Name, "msg", "WrongPassword", "Text");
                var sMsg = _sLangText + "\r\n\r\n";
                _sLangText = MyGlobal.GetLanguageString("Could not resolve your password correctly.", "form", Name, "msg", "Resolve", "Text");
                sMsg += _sLangText + "\r\n";
                _sLangText = MyGlobal.GetLanguageString("You must re-enter your password.", "form", Name, "msg", "ReEnter", "Text");
                sMsg += _sLangText;
                MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword_SQLServer.Focus();
            }
            else
            {
                txtPassword_SQLServer.Text = string.IsNullOrEmpty(sPasswordResult) ? txtPassword_SQLServer.Tag?.ToString() : sPasswordResult;
            }

            txtConnectionName_SQLServer.Focus();
        }

        private void Grid_RowColChange_MySQL()
        {
            var iRow = c1GridDBInfo.Row;

            lblPID.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePID]].CellValue(iRow).ToString();
            lblDomainUser.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDomainUser]].CellValue(iRow).ToString();

            //Text & Tag：當 Update Mode 時，如果 Connection Name 有變更，檢查是否已經有重覆的 Connection Name 存在了
            //txtConnectionName_MySQL.TextDetached = true; //偶爾莫名會自己變成 false；故此處強制改為 true
            txtConnectionName_MySQL.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eConnectionName]].CellValue(iRow).ToString();
            txtConnectionName_MySQL.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eConnectionName]].CellValue(iRow).ToString();

            txtServer_MySQL.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eServer]].CellValue(iRow).ToString();
            cboDatabase_MySQL.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDatabase]].CellValue(iRow).ToString();
            txtPort_MySQL.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePort]].CellValue(iRow).ToString();
            txtPort_MySQL.Text = txtPort_MySQL.Text == "0" ? @"3306" : txtPort_MySQL.Text;

            pnlBackColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabBackColor]].CellValue(iRow).ToString());
            pnlBackColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabBackColor]].CellValue(iRow).ToString();
            pnlActiveForeColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabActiveForeColor]].CellValue(iRow).ToString());
            pnlActiveForeColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabActiveForeColor]].CellValue(iRow).ToString();
            pnlInactiveForeColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabInactiveForeColor]].CellValue(iRow).ToString());
            pnlInactiveForeColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabInactiveForeColor]].CellValue(iRow).ToString();

            _toolTip1.SetToolTip(pnlBackColor, pnlBackColor.Tag + " " + "(R:" + pnlBackColor.BackColor.R + ", G:" + pnlBackColor.BackColor.G + ", B:" + pnlBackColor.BackColor.B + ")");
            _toolTip1.SetToolTip(pnlActiveForeColor, pnlActiveForeColor.Tag + " " + "(R:" + pnlActiveForeColor.BackColor.R + ", G:" + pnlActiveForeColor.BackColor.G + ", B:" + pnlActiveForeColor.BackColor.B + ")");
            _toolTip1.SetToolTip(pnlInactiveForeColor, pnlInactiveForeColor.Tag + " " + "(R:" + pnlInactiveForeColor.BackColor.R + ", G:" + pnlInactiveForeColor.BackColor.G + ", B:" + pnlInactiveForeColor.BackColor.B + ")");

            tabExample.BackColor = pnlBackColor.BackColor;
            tabExample.ForeColor = pnlActiveForeColor.BackColor; //Color.Black;
            tabExample.TextInactiveColor = pnlInactiveForeColor.BackColor;

            txtUserID_MySQL.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eUserID]].CellValue(iRow).ToString();
            txtRemark_MySQL.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eRemarks]].CellValue(iRow).ToString();

            var bValue = false;
            var sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + lblPID.Text + " AND AttributeKey = 'GeneralConfig' AND AttributeName = 'DarkMode'";
            var dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            btnDelete.Enabled = true;
            btnCopy.Enabled = true;
            chkDarkMode.Checked = bValue;

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + lblPID.Text + " AND AttributeKey = 'EditorConfig' AND AttributeName = 'ShowColumnInfo'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkShowColumnInfo.Checked = bValue;

            var sPasswordResult = TextEngine.Decrypt(TextEngine.Decode(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()), MyGlobal.sDomainUser);

            chkPooling_MySQL.Checked = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePooling]].CellValue(iRow).ToString() == "1";
            chkUnicode_MySQL.Checked = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eUnicode]].CellValue(iRow).ToString() == "1";

            //這裡改為各別判斷，才不會出現「先勾再取消」的「特殊狀況」
            chkSavePasswords.Checked = !string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString());

            if (string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()) && string.IsNullOrEmpty((txtPassword_MySQL.Tag ?? string.Empty).ToString()))
            {
                txtPassword_MySQL.Text = "";
                txtPassword_MySQL.Focus();
            }
            else if (string.IsNullOrEmpty(sPasswordResult) && !string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()))
            {
                txtPassword_MySQL.Text = "";
                _sLangText = MyGlobal.GetLanguageString("Wrong password!", "form", Name, "msg", "WrongPassword", "Text");
                var sMsg = _sLangText + "\r\n\r\n";
                _sLangText = MyGlobal.GetLanguageString("Could not resolve your password correctly.", "form", Name, "msg", "Resolve", "Text");
                sMsg += _sLangText + "\r\n";
                _sLangText = MyGlobal.GetLanguageString("You must re-enter your password.", "form", Name, "msg", "ReEnter", "Text");
                sMsg += _sLangText;
                MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword_MySQL.Focus();
            }
            else
            {
                txtPassword_MySQL.Text = string.IsNullOrEmpty(sPasswordResult) ? txtPassword_MySQL.Tag?.ToString() : sPasswordResult;
            }

            txtConnectionName_MySQL.Focus();
        }

        private void Grid_RowColChange_SQLite()
        {
            var iRow = c1GridDBInfo.Row;

            lblPID.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePID]].CellValue(iRow).ToString();
            lblDomainUser.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDomainUser]].CellValue(iRow).ToString();

            //Text & Tag：當 Update Mode 時，如果 Connection Name 有變更，檢查是否已經有重覆的 Connection Name 存在了
            txtConnectionName_SQLite.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eConnectionName]].CellValue(iRow).ToString();
            txtConnectionName_SQLite.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eConnectionName]].CellValue(iRow).ToString();

            //rdoOpen_SQLite.Checked = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDatabaseOpenExistingFile]].CellValue(iRow).ToString() == "1";
            rdoOpen_SQLite.Checked = true;
            txtFile_SQLite.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDatabaseFile]].CellValue(iRow).ToString();

            txtFile_SQLite.ForeColor = File.Exists(txtFile_SQLite.Text) ? SystemColors.WindowText : Color.Red;

            cboDatabaseType_SQLite.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eDatabaseType]].CellValue(iRow).ToString();
            txtRemark_SQLite.Text = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eRemarks]].CellValue(iRow).ToString();

            var bValue = false;
            var sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + lblPID.Text + " AND AttributeKey = 'GeneralConfig' AND AttributeName = 'DarkMode'";
            var dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            btnCopy.Enabled = true;
            btnDelete.Enabled = true;
            chkDarkMode.Checked = bValue;

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + lblPID.Text + " AND AttributeKey = 'EditorConfig' AND AttributeName = 'ShowColumnInfo'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkShowColumnInfo.Checked = bValue;

            pnlBackColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabBackColor]].CellValue(iRow).ToString());
            pnlBackColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabBackColor]].CellValue(iRow).ToString();
            pnlActiveForeColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabActiveForeColor]].CellValue(iRow).ToString());
            pnlActiveForeColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabActiveForeColor]].CellValue(iRow).ToString();
            pnlInactiveForeColor.BackColor = ColorTranslator.FromHtml(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabInactiveForeColor]].CellValue(iRow).ToString());
            pnlInactiveForeColor.Tag = c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eTabInactiveForeColor]].CellValue(iRow).ToString();

            _toolTip1.SetToolTip(pnlBackColor, pnlBackColor.Tag + " " + "(R:" + pnlBackColor.BackColor.R + ", G:" + pnlBackColor.BackColor.G + ", B:" + pnlBackColor.BackColor.B + ")");
            _toolTip1.SetToolTip(pnlActiveForeColor, pnlActiveForeColor.Tag + " " + "(R:" + pnlActiveForeColor.BackColor.R + ", G:" + pnlActiveForeColor.BackColor.G + ", B:" + pnlActiveForeColor.BackColor.B + ")");
            _toolTip1.SetToolTip(pnlInactiveForeColor, pnlInactiveForeColor.Tag + " " + "(R:" + pnlInactiveForeColor.BackColor.R + ", G:" + pnlInactiveForeColor.BackColor.G + ", B:" + pnlInactiveForeColor.BackColor.B + ")");

            tabExample.BackColor = pnlBackColor.BackColor;
            tabExample.ForeColor = pnlActiveForeColor.BackColor; //Color.Black;
            tabExample.TextInactiveColor = pnlInactiveForeColor.BackColor;

            var sPasswordResult = TextEngine.Decrypt(TextEngine.Decode(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()), MyGlobal.sDomainUser);

            //這裡改為各別判斷，才不會出現「先勾再取消」的「特殊狀況」
            chkSavePasswords.Checked = !string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString());

            if (string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()) && string.IsNullOrEmpty((txtPassword_SQLite.Tag ?? string.Empty).ToString()))
            {
                txtPassword_SQLite.Text = "";
                txtPassword_SQLite.Focus();
            }
            else if (string.IsNullOrEmpty(sPasswordResult) && !string.IsNullOrEmpty(c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.ePassword]].CellValue(iRow).ToString()))
            {
                txtPassword_SQLite.Text = "";
                _sLangText = MyGlobal.GetLanguageString("Wrong password!", "form", Name, "msg", "WrongPassword", "Text");
                var sMsg = _sLangText + "\r\n\r\n";
                _sLangText = MyGlobal.GetLanguageString("Could not resolve your password correctly.", "form", Name, "msg", "Resolve", "Text");
                sMsg += _sLangText + "\r\n";
                _sLangText = MyGlobal.GetLanguageString("You must re-enter your password.", "form", Name, "msg", "ReEnter", "Text");
                sMsg += _sLangText;
                MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword_SQLite.Focus();
            }
            else
            {
                txtPassword_SQLite.Text = string.IsNullOrEmpty(sPasswordResult) ? txtPassword_SQLite.Tag?.ToString() : sPasswordResult;
            }

            rdoCreate_SQLite.Enabled = false;
            rdoWithPassword.Visible = false;
            rdoWithoutPassword.Visible = false;
            btnRemovePassword.Visible = false;
            btnEncryptWithPassword.Visible = false;

            //調整位置
            if (c1GridDBInfo.Columns[_lstGridHeader[(int)eMenu.eWithPassword]].CellValue(iRow).ToString() == "1") //有密碼
            {
                rdoWithPassword.Visible = true;
                btnRemovePassword.Visible = true;
                rdoWithPassword.Checked = true;
                btnRemovePassword.Location = new Point(rdoWithPassword.Left + rdoWithPassword.Width + 15, btnRemovePassword.Top);
            }
            else
            {
                rdoWithoutPassword.Visible = true;
                btnEncryptWithPassword.Visible = true;
                rdoWithoutPassword.Checked = true;
                rdoWithoutPassword.Location = new Point(rdoWithPassword.Left, rdoWithoutPassword.Top);
                btnEncryptWithPassword.Location = new Point(rdoWithoutPassword.Left + rdoWithoutPassword.Width + 15, btnEncryptWithPassword.Top);
            }

            txtConnectionName_SQLite.Focus();
        }

        private void cboDataSource_TextChanged(object sender, EventArgs e)
        {
            pnlOracle.Visible = cboDataSource.Text == "Oracle";
            pnlPostgreSQL.Visible = cboDataSource.Text == "PostgreSQL";
            pnlSQLServer.Visible = cboDataSource.Text == "SQL Server";
            pnlMySQL.Visible = cboDataSource.Text.StartsWith("MySQL");
            pnlSQLite.Visible = cboDataSource.Text.StartsWith("SQLite");
            btnTest.Enabled = true;

            if (string.IsNullOrEmpty(cboDataSource.Text))
            {
                return;
            }

            txtSupportInfo.Text = "";

            switch (cboDataSource.Text)
            {
                case "Oracle":
                    InitialDataSource_Oracle(); //cboDataSource_TextChanged
                    break;
                case "PostgreSQL":
                    InitialDataSource_PostgreSQL(); //cboDataSource_TextChanged
                    break;
                case "SQL Server":
                    InitialDataSource_SQLServer(); //cboDataSource_TextChanged
                    break;
                case "MySQL/MariaDB":
                    InitialDataSource_MySQL(); //cboDataSource_TextChanged
                    break;
                case "SQLite":
                    InitialDataSource_SQLite(); //cboDataSource_TextChanged
                    break;
            }
        }

        private void InitialDataSource_Oracle()
        {
            txtConnectionName_Oracle.Text = "";
            txtServer_Oracle.Text = "";
            txtPort_Oracle.Text = @"1521";
            txtRemark_Oracle.Text = "";
            txtUserID_Oracle.Text = "";
            txtPassword_Oracle.Text = "";
            cboConnectAs_Oracle.SelectedIndex = 0;
            chkDirectMode_Oracle.Checked = false;            
            lblSID_Oracle.Visible = chkDirectMode_Oracle.Checked;
            txtSID_Oracle.Visible = true;
            txtSID_Oracle.Text = "";
            txtSID_Oracle.Visible = chkDirectMode_Oracle.Checked;
            btnHelp_SID_Oracle.Visible = chkDirectMode_Oracle.Checked;
            chkUnicode_Oracle.Checked = true;

            foreach (var t in _lstPicLogo)
            {
                t.Visible = false;
            }

            if (cboDataSource.SelectedIndex >= 0)
            {
                _lstPicLogo[cboDataSource.SelectedIndex].Visible = true;
                txtSupportInfo.Text = _lstSupportInfo[cboDataSource.SelectedIndex];
            }

            chkPooling_Oracle.Checked = false;

            cboDataSource.Focus();
            txtConnectionName_Oracle.Focus();
        }

        private void InitialDataSource_PostgreSQL()
        {
            foreach (var t in _lstPicLogo)
            {
                t.Visible = false;
            }

            if (cboDataSource.SelectedIndex >= 0)
            {
                _lstPicLogo[cboDataSource.SelectedIndex].Visible = true;
                txtSupportInfo.Text = _lstSupportInfo[cboDataSource.SelectedIndex];
            }

            txtConnectionName_PostgreSQL.Text = "";
            txtServer_PostgreSQL.Text = "";
            cboDatabase_PostgreSQL.Items.Clear();
            cboDatabase_PostgreSQL.Text = "";
            txtPort_PostgreSQL.Text = @"5432";
            txtRemark_PostgreSQL.Text = "";
            txtUserID_PostgreSQL.Text = "";
            txtPassword_PostgreSQL.Text = "";
            chkUnicode_PostgreSQL.Checked = true;
            chkAutoRollback_PostgreSQL.Checked = true;
            chkPooling_PostgreSQL.Checked = false;

            cboDataSource.Focus();
            txtConnectionName_PostgreSQL.Focus();
        }

        private void InitialDataSource_SQLServer()
        {
            foreach (var t in _lstPicLogo)
            {
                t.Visible = false;
            }

            if (cboDataSource.SelectedIndex >= 0)
            {
                _lstPicLogo[cboDataSource.SelectedIndex].Visible = true;
                txtSupportInfo.Text = _lstSupportInfo[cboDataSource.SelectedIndex];
            }

            txtConnectionName_SQLServer.Text = "";
            txtServer_SQLServer.Text = "";
            cboDatabase_SQLServer.Items.Clear();
            cboDatabase_SQLServer.Text = "";
            txtRemark_SQLServer.Text = "";
            txtUserID_SQLServer.Text = "";
            txtPassword_SQLServer.Text = "";
            txtPort_SQLServer.Text = @"1433";
            chkPooling_SQLServer.Checked = false;
            chkExcludeNativeObject_SQLServer.Checked = true;

            cboDataSource.Focus();
            txtConnectionName_SQLServer.Focus();
        }

        private void InitialDataSource_MySQL()
        {
            foreach (var t in _lstPicLogo)
            {
                t.Visible = false;
            }

            if (cboDataSource.SelectedIndex >= 0)
            {
                _lstPicLogo[cboDataSource.SelectedIndex].Visible = true;
                txtSupportInfo.Text = _lstSupportInfo[cboDataSource.SelectedIndex];
            }

            txtConnectionName_MySQL.Text = "";
            txtServer_MySQL.Text = "";
            cboDatabase_MySQL.Items.Clear();
            cboDatabase_MySQL.Text = "";
            txtRemark_MySQL.Text = "";
            txtUserID_MySQL.Text = "";
            txtPassword_MySQL.Text = "";
            txtPort_MySQL.Text = @"3306";
            chkPooling_MySQL.Checked = false;
            chkUnicode_MySQL.Checked = true;

            cboDataSource.Focus();
            txtConnectionName_MySQL.Focus();
        }

        private void InitialDataSource_SQLite()
        {
            foreach (var t in _lstPicLogo)
            {
                t.Visible = false;
            }

            if (cboDataSource.SelectedIndex >= 0)
            {
                _lstPicLogo[cboDataSource.SelectedIndex].Visible = true;
                txtSupportInfo.Text = _lstSupportInfo[cboDataSource.SelectedIndex];
            }

            txtConnectionName_SQLite.Text = "";
            rdoOpen_SQLite.Checked = true;
            txtFile_SQLite.Text = "";
            cboDatabaseType_SQLite.SelectedIndex = 0;
            txtPassword_SQLite.Text = "";
            txtRemark_SQLite.Text = "";
            ChangeSQLitePasswordState();

            cboDataSource.Focus();
            txtConnectionName_SQLite.Focus();
        }

        private bool CheckData(bool bSaveOnly, bool bShowMessage = true)
        {
            var bValue = false;

            switch (cboDataSource.Text)
            {
                case "Oracle":
                    bValue = CheckData_Oracle(bSaveOnly);
                    break;
                case "PostgreSQL":
                    bValue = CheckData_PostgreSQL(bSaveOnly, bShowMessage);
                    break;
                case "SQL Server":
                    bValue = CheckData_SQLServer(bSaveOnly, bShowMessage);
                    break;
                case "MySQL/MariaDB":
                    bValue = CheckData_MySQL(bSaveOnly, bShowMessage);
                    break;
                case "SQLite":
                    bValue = CheckData_SQLite(bSaveOnly, bShowMessage);
                    break;
            }

            return bValue;
        }

        private bool CheckData_Oracle(bool bSaveOnly)
        {
            var bValue = false;

            if (string.IsNullOrEmpty(cboDataSource.Text))
            {
                _sLangText = MyGlobal.GetLanguageString("Please select data source.", "form", Name, "msg", "InfoCheckDataSource", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboDataSource.Focus();
            }
            else if (string.IsNullOrEmpty(txtConnectionName_Oracle.Text.Trim()))
            {
                _sLangText = MyGlobal.GetLanguageString("Please enter connection name.", "form", Name, "msg", "InfoCheckConnection", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConnectionName_Oracle.Focus();
            }
            else if (string.IsNullOrEmpty(txtServer_Oracle.Text.Trim()))
            {
                _sLangText = MyGlobal.GetLanguageString("Please enter server name.", "form", Name, "msg", "InfoCheckServer", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtServer_Oracle.Focus();
            }
            else if (chkDirectMode_Oracle.Checked && string.IsNullOrEmpty(txtSID_Oracle.Text.Trim()))
            {
                _sLangText = MyGlobal.GetLanguageString("Please enter SID.", "form", Name, "msg", "InfoCheckSID", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSID_Oracle.Focus();
            }
            else if (string.IsNullOrEmpty(txtPort_Oracle.Text.Trim()))
            {
                _sLangText = MyGlobal.GetLanguageString("Please enter port number.", "form", Name, "msg", "InfoCheckPort", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPort_Oracle.Focus();
            }
            else if (string.IsNullOrEmpty(txtUserID_Oracle.Text.Trim()))
            {
                _sLangText = MyGlobal.GetLanguageString("Please enter user name.", "form", Name, "msg", "InfoCheckUser", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserID_Oracle.Focus();
            }
            else if (bSaveOnly == false && string.IsNullOrEmpty(txtPassword_Oracle.Text.Trim()))
            {
                _sLangText = MyGlobal.GetLanguageString("Please enter password.", "form", Name, "msg", "InfoCheckPassword", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword_Oracle.Focus();
            }
            else
            {
                bValue = true;
            }

            return bValue;
        }

        private bool CheckData_PostgreSQL(bool bSaveOnly, bool bShowMessage)
        {
            var bValue = false;

            if (string.IsNullOrEmpty(cboDataSource.Text))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please select data source.", "form", Name, "msg", "InfoCheckDataSource", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboDataSource.Focus();
            }
            else if (string.IsNullOrEmpty(txtConnectionName_PostgreSQL.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter connection name.", "form", Name, "msg", "InfoCheckConnection", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConnectionName_PostgreSQL.Focus();
            }
            else if (string.IsNullOrEmpty(txtServer_PostgreSQL.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter server name.", "form", Name, "msg", "InfoCheckServer", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtServer_PostgreSQL.Focus();
            }
            else if (bShowMessage && string.IsNullOrEmpty(cboDatabase_PostgreSQL.Text.Trim()))
            {
                _sLangText = MyGlobal.GetLanguageString("Please enter database name.", "form", Name, "msg", "InfoCheckDatabase", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboDatabase_PostgreSQL.Focus();
            }
            else if (string.IsNullOrEmpty(txtPort_PostgreSQL.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter port number.", "form", Name, "msg", "InfoCheckPort", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPort_PostgreSQL.Focus();
            }
            else if (string.IsNullOrEmpty(txtUserID_PostgreSQL.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter user name.", "form", Name, "msg", "InfoCheckUser", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserID_PostgreSQL.Focus();
            }
            else if (bSaveOnly == false && string.IsNullOrEmpty(txtPassword_PostgreSQL.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter password.", "form", Name, "msg", "InfoCheckPassword", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword_PostgreSQL.Focus();
            }
            else
            {
                bValue = true;
            }

            return bValue;
        }

        private bool CheckData_SQLServer(bool bSaveOnly, bool bShowMessage)
        {
            var bValue = false;

            if (string.IsNullOrEmpty(cboDataSource.Text))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please select data source.", "form", Name, "msg", "InfoCheckDataSource", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboDataSource.Focus();
            }
            else if (string.IsNullOrEmpty(txtConnectionName_SQLServer.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter connection name.", "form", Name, "msg", "InfoCheckConnection", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConnectionName_SQLServer.Focus();
            }
            else if (string.IsNullOrEmpty(txtServer_SQLServer.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter server name.", "form", Name, "msg", "InfoCheckServer", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtServer_SQLServer.Focus();
            }
            //20220423 不檢查是否有指定資料庫 (使用者透過 USE DatabaseName 切換資料庫時，同時變更 ConnectionString)
            //else if (bShowMessage && string.IsNullOrEmpty(cboDatabase_SQLServer.Text.Trim())) //SQL Server 連線時必須指定資料庫，否則用 Use 指令切換資料庫，執行一次 select 指令並中斷連線後，就無法再正常 select 了！(違反整體架構的一致性)
            //{
            //    _sLangText = MyGlobal.GetLanguageString("Please enter database name.", "form", Name, "msg", "InfoCheckDatabase", "Text");
            //    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cboDatabase_SQLServer.Focus();
            //}
            else if (string.IsNullOrEmpty(txtUserID_SQLServer.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter user name.", "form", Name, "msg", "InfoCheckUser", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserID_SQLServer.Focus();
            }
            else if (bSaveOnly == false && string.IsNullOrEmpty(txtPassword_SQLServer.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter password.", "form", Name, "msg", "InfoCheckPassword", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword_SQLServer.Focus();
            }
            else
            {
                bValue = true;
            }

            return bValue;
        }

        private bool CheckData_MySQL(bool bSaveOnly, bool bShowMessage)
        {
            var bValue = false;

            if (string.IsNullOrEmpty(cboDataSource.Text))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please select data source.", "form", Name, "msg", "InfoCheckDataSource", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboDataSource.Focus();
            }
            else if (string.IsNullOrEmpty(txtConnectionName_MySQL.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter connection name.", "form", Name, "msg", "InfoCheckConnection", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConnectionName_MySQL.Focus();
            }
            else if (string.IsNullOrEmpty(txtServer_MySQL.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter server name.", "form", Name, "msg", "InfoCheckServer", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtServer_MySQL.Focus();
            }
            //else if (string.IsNullOrEmpty(cboDatabase_MySQL.Text.Trim()))
            //{
            //    _sName = MyGlobal.GetLanguageString("Please enter database name.", "form", Name, "msg", "InfoCheckDatabase", "Text");
            //    MessageBox.Show(_sName, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cboDatabase_MySQL.Focus();
            //}
            else if (string.IsNullOrEmpty(txtUserID_MySQL.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter user name.", "form", Name, "msg", "InfoCheckUser", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserID_MySQL.Focus();
            }
            else if (bSaveOnly == false && string.IsNullOrEmpty(txtPassword_MySQL.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter password.", "form", Name, "msg", "InfoCheckPassword", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword_MySQL.Focus();
            }
            else
            {
                bValue = true;
            }

            return bValue;
        }

        private bool CheckData_SQLite(bool bSaveOnly, bool bShowMessage)
        {
            var bValue = false;

            if (string.IsNullOrEmpty(cboDataSource.Text))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please select data source.", "form", Name, "msg", "InfoCheckDataSource", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboDataSource.Focus();
            }
            else if (string.IsNullOrEmpty(txtConnectionName_SQLite.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter connection name.", "form", Name, "msg", "InfoCheckConnection", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConnectionName_SQLite.Focus();
            }
            else if (string.IsNullOrEmpty(txtFile_SQLite.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please select a SQLite file.", "form", Name, "msg", "InfoCheckDatabaseFile", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnBrowseFile_SQLite.Focus();
            }
            else if (cboDatabaseType_SQLite.Text == "System.Data.SQLite RC4" && rdoWithPassword.Checked && bSaveOnly == false && string.IsNullOrEmpty(txtPassword_SQLite.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter password.", "form", Name, "msg", "InfoCheckPassword", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword_SQLite.Focus();
            }
            else if (cboDatabaseType_SQLite.Text == "System.Data.SQLite RC4" && rdoOpen_SQLite.Checked && !File.Exists(txtFile_SQLite.Text))
            {
                if (!bShowMessage) return false;

                txtFile_SQLite.ForeColor = Color.Red;
                _sLangText = MyGlobal.GetLanguageString("SQLite file not found!", "Global", "Global", "msg", "SQLiteFileNotFound", "Text");
                MessageBox.Show(_sLangText + "\r\n\r\n" + txtFile_SQLite.Text, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnBrowseFile_SQLite.Focus();
            }
            else if (cboDatabaseType_SQLite.Text.StartsWith("SQLCipher") && rdoWithPassword.Checked && bSaveOnly == false && string.IsNullOrEmpty(txtPassword_SQLite.Text.Trim()))
            {
                if (!bShowMessage) return false;

                _sLangText = MyGlobal.GetLanguageString("Please enter password.", "form", Name, "msg", "InfoCheckPassword", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword_SQLite.Focus();
            }
            else if (cboDatabaseType_SQLite.Text.StartsWith("SQLCipher") && rdoOpen_SQLite.Checked && !File.Exists(txtFile_SQLite.Text))
            {
                if (!bShowMessage) return false;

                txtFile_SQLite.ForeColor = Color.Red;
                _sLangText = MyGlobal.GetLanguageString("SQLite file not found!", "Global", "Global", "msg", "SQLiteFileNotFound", "Text");
                MessageBox.Show(_sLangText + "\r\n\r\n" + txtFile_SQLite.Text, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnBrowseFile_SQLite.Focus();
            }
            else
            {
                bValue = true;
                txtFile_SQLite.ForeColor = SystemColors.WindowText;
            }

            return bValue;
        }

        private bool SaveConnectData(bool bSaveOnly = false)
        {
            var bResult = false;

            switch (cboDataSource.Text)
            {
                case "Oracle":
                    bResult = SaveConnectData_Oracle(bSaveOnly);
                    break;
                case "PostgreSQL":
                    bResult = SaveConnectData_PostgreSQL(bSaveOnly);
                    break;
                case "SQL Server":
                    bResult = SaveConnectData_SQLServer(bSaveOnly);
                    break;
                case "MySQL/MariaDB":
                    bResult = SaveConnectData_MySQL(bSaveOnly);
                    break;
                case "SQLite":
                    if (rdoWithoutPassword.Checked)
                    {
                        //沒有密碼，密碼強制清空、不儲存，避免下次會要求輸入密碼才能運作(卡住！)
                        txtPassword_SQLite.Text = "";
                        chkSavePasswords.Checked = false;
                    }

                    if (!string.IsNullOrEmpty(txtPassword_SQLite.Text))
                    {
                        txtPassword_SQLite.Tag = txtPassword_SQLite.Text;
                    }

                    bResult = SaveConnectData_SQLite(bSaveOnly);
                    txtPassword_SQLite.Tag = "";
                    break;
            }

            //指定哪一個 Column 要套用 FetchCellStyle (這裡要重新指定一次，並 Refresh，才會顯示顏色，而不是顏色代碼)
            c1GridDBInfo.Splits[0].DisplayColumns[_lstGridHeader[(int)eMenu.eTabBackColor]].FetchStyle = true;
            c1GridDBInfo.Refresh();

            return bResult;
        }

        private bool SaveConnectData_Oracle(bool bSaveOnly = false)
        {
            var bResult = false;
            string sSql;
            var sEncryptedPassword = "";
            int iRow = 0, iRowNumber = 0;

            if (!CheckData(bSaveOnly))
            {
                return false;
            }

            if (chkSavePasswords.Checked)
            {
                sEncryptedPassword = TextEngine.Encode(TextEngine.Encrypt(txtPassword_Oracle.Text, MyGlobal.sDomainUser));
            }

            string sTemp;
            string sTemp2;
            DataTable dt;

            if (!string.IsNullOrEmpty(lblPID.Text)) //Update Mode
            {
                //Update Mode 時，如果 Connection Name 有變更，檢查是否已經有重覆的 Connection Name 存在了
                if (txtConnectionName_Oracle.Text != txtConnectionName_Oracle.Tag.ToString())
                {
                    dt = DBCommon.ExecQuery("SELECT * FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND ConnectionName = '" + txtConnectionName_Oracle.Text + "' ORDER BY LastConnect DESC");

                    if (dt.Rows.Count > 0)
                    {
                        _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", Name, "object", "lblConnectionName", "Text");
                        sTemp = MyGlobal.GetLanguageString("already exists!", "form", Name, "msg", "AlreadyExists", "Text");
                        sTemp2 = MyGlobal.GetLanguageString("Please enter another connection name.", "form", Name, "msg", "EnterAnterConnectionName", "Text");
                        MessageBox.Show(_sLangText + @" " + txtConnectionName_Oracle.Text + @" " + sTemp + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtConnectionName_Oracle.Focus();
                        return false;
                    }
                }

                iRow = c1GridDBInfo.Row; //記住目前在第幾列

                sSql = "UPDATE DBInfo\r\n";
                sSql += "   SET DataSource = '" + cboDataSource.Text + "', DomainUser = '" + lblDomainUser.Text + "', ConnectionName = '" + txtConnectionName_Oracle.Text + "',\r\n";
                sSql += "       Server = '" + txtServer_Oracle.Text + "', SID = '" + txtSID_Oracle.Text + "', DirectMode = '" + (chkDirectMode_Oracle.Checked ? "1" : "0") + "', Database = '', Port = '" + txtPort_Oracle.Text + "',\r\n";
                sSql += "       TabBackColor = '" + pnlBackColor.Tag + "', TabActiveForeColor = '" + pnlActiveForeColor.Tag + "', TabInactiveForeColor = '" + pnlInactiveForeColor.Tag + "', User = '" + txtUserID_Oracle.Text + "', ConnectAs = '" + cboConnectAs_Oracle.Text + "', \r\n";
                sSql += "       Password = '" + sEncryptedPassword + "',\r\n";
                sSql += "       AutoRollback = '0', Unicode = '" + (chkUnicode_Oracle.Checked ? "1" : "0") + "', Remarks = '" + txtRemark_Oracle.Text.Replace("'", "''") + "', o1 = '" + (chkPooling_Oracle.Checked ? "1" : "0") + "'";

                if (bSaveOnly == false)
                {
                    sSql += ", LastConnect = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'\r\n";
                }

                sSql += " WHERE PID = " + lblPID.Text;
                DBCommon.ExecNonQuery(sSql);

                //重新載入 DBInfo 資料
                CreateAndGetDbInfoTable(); //SaveConnectData_Oracle

                c1GridDBInfo.Row = bSaveOnly == false ? 0 : iRow;
                bResult = true;
            }
            else //Add Mode
            {
                //檢查此 Connection Name 是否已存在
                dt = DBCommon.ExecQuery("SELECT * FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND ConnectionName = '" + txtConnectionName_Oracle.Text + "'");

                if (dt.Rows.Count > 0)
                {
                    _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", Name, "object", "lblConnectionName", "Text");
                    sTemp = MyGlobal.GetLanguageString("already exists!", "form", Name, "msg", "AlreadyExists", "Text");
                    sTemp2 = MyGlobal.GetLanguageString("Please enter another connection name.", "form", Name, "msg", "EnterAnterConnectionName", "Text");
                    MessageBox.Show(_sLangText + @" " + txtConnectionName_Oracle.Text + @" " + sTemp + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConnectionName_Oracle.Focus();
                }
                else
                {
                    sSql = "INSERT INTO DBInfo\r\n";
                    sSql += "       (DataSource, DomainUser, ConnectionName,\r\n";
                    sSql += "        Server, SID, DirectMode, Database, Port,\r\n";
                    sSql += "        TabBackColor, TabActiveForeColor, TabInactiveForeColor, User, ConnectAs,\r\n";
                    sSql += "        Password,\r\n";
                    sSql += "        AutoRollback, Unicode, Remarks, o1";

                    if (bSaveOnly == false)
                    {
                        sSql += ", LastConnect";
                    }

                    sSql += ")\r\n";

                    sSql += "VALUES ('" + cboDataSource.Text + "', '" + lblDomainUser.Text + "', '" + txtConnectionName_Oracle.Text + "',\r\n";
                    sSql += "        '" + txtServer_Oracle.Text + "', '" + txtSID_Oracle.Text + "', '" + (chkDirectMode_Oracle.Checked ? "1" : "0") + "', '', '" + txtPort_Oracle.Text + "',\r\n";
                    sSql += "        '" + pnlBackColor.Tag + "', '" + pnlActiveForeColor.Tag + "', '" + pnlInactiveForeColor.Tag + "', '" + txtUserID_Oracle.Text + "', '" + cboConnectAs_Oracle.Text + "',\r\n";
                    sSql += "        '" + sEncryptedPassword + "',\r\n";
                    sSql += "        '0', '" + (chkUnicode_Oracle.Checked ? "1" : "0") + "', '" + txtRemark_Oracle.Text + "', '" + (chkPooling_Oracle.Checked ? "1" : "0") + "'";

                    if (bSaveOnly == false)
                    {
                        sSql += ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'";
                    }

                    sSql += ")";

                    DBCommon.ExecNonQuery(sSql);

                    //重新載入 DBInfo 資料
                    CreateAndGetDbInfoTable(); //SaveConnectData_Oracle

                    //找到 PID 數值最大的，就是剛剛新增的資料
                    for (var i = 0; i < _dtDbInfo.Rows.Count; i++)
                    {
                        if (iRowNumber == 0)
                        {
                            iRowNumber = Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString());
                            iRow = i;
                        }
                        else
                        {
                            if (iRowNumber >= Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString()))
                            {
                                continue;
                            }

                            iRowNumber = Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString());
                            iRow = i;
                        }
                    }

                    lblPID.Text = iRowNumber.ToString();
                    c1GridDBInfo.Row = iRow; //指標切換到剛剛新增的那一筆的列數
                    bResult = true;
                }
            }

            return bResult;
        }

        private bool SaveConnectData_PostgreSQL(bool bSaveOnly = false)
        {
            var bResult = false;
            string sSql;
            var sEncryptedPassword = "";
            int iRow = 0, iRowNumber = 0;

            if (!CheckData(bSaveOnly))
            {
                return false;
            }

            if (chkSavePasswords.Checked)
            {
                sEncryptedPassword = TextEngine.Encode(TextEngine.Encrypt(txtPassword_PostgreSQL.Text, MyGlobal.sDomainUser));
            }

            string sTemp;
            string sTemp2;
            DataTable dt;

            if (!string.IsNullOrEmpty(lblPID.Text)) //Update Mode
            {
                //Update Mode 時，如果 Connection Name 有變更，檢查是否已經有重覆的 Connection Name 存在了
                if (txtConnectionName_PostgreSQL.Text != txtConnectionName_PostgreSQL.Tag.ToString())
                {
                    dt = DBCommon.ExecQuery("SELECT * FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND ConnectionName = '" + txtConnectionName_PostgreSQL.Text + "' ORDER BY LastConnect DESC");

                    if (dt.Rows.Count > 0)
                    {
                        _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", Name, "object", "lblConnectionName", "Text");
                        sTemp = MyGlobal.GetLanguageString("already exists!", "form", Name, "msg", "AlreadyExists", "Text");
                        sTemp2 = MyGlobal.GetLanguageString("Please enter another connection name.", "form", Name, "msg", "EnterAnotherConnectionName", "Text");
                        MessageBox.Show(_sLangText + @" " + txtConnectionName_PostgreSQL.Text + @" " + sTemp + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtConnectionName_PostgreSQL.Focus();
                        return false;
                    }
                }

                iRow = c1GridDBInfo.Row; //記住目前在第幾列

                sSql = "UPDATE DBInfo\r\n";
                sSql += "   SET DataSource = '" + cboDataSource.Text + "', DomainUser = '" + lblDomainUser.Text + "', ConnectionName = '" + txtConnectionName_PostgreSQL.Text + "',\r\n";
                sSql += "       Server = '" + txtServer_PostgreSQL.Text + "', SID = '', DirectMode = '0', Database = '" + cboDatabase_PostgreSQL.Text + "', Port = '" + txtPort_PostgreSQL.Text + "',\r\n";
                sSql += "       TabBackColor = '" + pnlBackColor.Tag + "', TabActiveForeColor = '" + pnlActiveForeColor.Tag + "', TabInactiveForeColor = '" + pnlInactiveForeColor.Tag + "', User = '" + txtUserID_PostgreSQL.Text + "', ConnectAs = '',\r\n";
                sSql += "       Password = '" + sEncryptedPassword + "',\r\n";
                sSql += "       AutoRollback = '" + (chkAutoRollback_PostgreSQL.Checked ? "1" : "0") + "', Unicode = '" + (chkUnicode_PostgreSQL.Checked ? "1" : "0") + "', Remarks = '" + txtRemark_PostgreSQL.Text.Replace("'", "''") + "', o1 = '" + (chkPooling_PostgreSQL.Checked ? "1" : "0") + "'";

                if (bSaveOnly == false)
                {
                    sSql += ", LastConnect = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' \r\n";
                }

                sSql += " WHERE PID = " + lblPID.Text;
                DBCommon.ExecNonQuery(sSql);

                //重新載入 DBInfo 資料
                CreateAndGetDbInfoTable(); //SaveConnectData_PostgreSQL

                c1GridDBInfo.Row = bSaveOnly == false ? 0 : iRow;
                bResult = true;
            }
            else //Add Mode
            {
                //檢查此 Connection Name 是否已存在
                dt = DBCommon.ExecQuery("SELECT * FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND ConnectionName = '" + txtConnectionName_PostgreSQL.Text + "'");

                if (dt.Rows.Count > 0)
                {
                    _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", Name, "object", "lblConnectionName", "Text");
                    sTemp = MyGlobal.GetLanguageString("already exists!", "form", Name, "msg", "AlreadyExists", "Text");
                    sTemp2 = MyGlobal.GetLanguageString("Please enter another connection name.", "form", Name, "msg", "EnterAnotherConnectionName", "Text");
                    MessageBox.Show(_sLangText + @" " + txtConnectionName_PostgreSQL.Text + @" " + sTemp + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConnectionName_PostgreSQL.Focus();
                }
                else
                {
                    sSql = "INSERT INTO DBInfo\r\n";
                    sSql += "       (DataSource, DomainUser, ConnectionName,\r\n";
                    sSql += "        Server, SID, DirectMode, Database, Port,\r\n";
                    sSql += "        TabBackColor, TabActiveForeColor, TabInactiveForeColor, User, ConnectAs,\r\n";
                    sSql += "        Password,\r\n";
                    sSql += "        AutoRollback, Unicode, Remarks, o1";

                    if (bSaveOnly == false)
                    {
                        sSql += ", LastConnect";
                    }

                    sSql += ")\r\n";

                    sSql += "VALUES ('" + cboDataSource.Text + "', '" + lblDomainUser.Text + "', '" + txtConnectionName_PostgreSQL.Text + "',\r\n";
                    sSql += "        '" + txtServer_PostgreSQL.Text + "', '', '0', '" + cboDatabase_PostgreSQL.Text + "', '" + txtPort_PostgreSQL.Text + "',\r\n";
                    sSql += "        '" + pnlBackColor.Tag + "', '" + pnlActiveForeColor.Tag + "', '" + pnlInactiveForeColor.Tag + "', '" + txtUserID_PostgreSQL.Text + "', '',\r\n";
                    sSql += "        '" + sEncryptedPassword + "',\r\n";
                    sSql += "        '" + (chkAutoRollback_PostgreSQL.Checked ? "1" : "0") + "', '" + (chkUnicode_PostgreSQL.Checked ? "1" : "0") + "', '" + txtRemark_PostgreSQL.Text + "', '" + (chkPooling_PostgreSQL.Checked ? "1" : "0") + "'";

                    if (bSaveOnly == false)
                    {
                        sSql += ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'";
                    }

                    sSql += ")";

                    DBCommon.ExecNonQuery(sSql);

                    //重新載入 DBInfo 資料
                    CreateAndGetDbInfoTable(); //SaveConnectData_PostgreSQL

                    //找到 PID 數值最大的，就是剛剛新增的資料
                    for (var i = 0; i < _dtDbInfo.Rows.Count; i++)
                    {
                        if (iRowNumber == 0)
                        {
                            iRowNumber = Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString());
                            iRow = i;
                        }
                        else
                        {
                            if (iRowNumber >= Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString()))
                            {
                                continue;
                            }

                            iRowNumber = Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString());
                            iRow = i;
                        }
                    }

                    lblPID.Text = iRowNumber.ToString();
                    c1GridDBInfo.Row = iRow; //指標切換到剛剛新增的那一筆的列數
                    bResult = true;
                }
            }

            return bResult;
        }

        private bool SaveConnectData_SQLServer(bool bSaveOnly = false)
        {
            var bResult = false;
            string sSql;
            var sEncryptedPassword = "";
            int iRow = 0, iRowNumber = 0;

            if (!CheckData(bSaveOnly))
            {
                return false;
            }

            if (chkSavePasswords.Checked)
            {
                sEncryptedPassword = TextEngine.Encode(TextEngine.Encrypt(txtPassword_SQLServer.Text, MyGlobal.sDomainUser));
            }

            string sTemp;
            string sTemp2;
            DataTable dt;

            if (!string.IsNullOrEmpty(lblPID.Text)) //Update Mode
            {
                //Update Mode 時，如果 Connection Name 有變更，檢查是否已經有重覆的 Connection Name 存在了
                if (txtConnectionName_SQLServer.Text != txtConnectionName_SQLServer.Tag.ToString())
                {
                    dt = DBCommon.ExecQuery("SELECT * FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND ConnectionName = '" + txtConnectionName_SQLServer.Text + "' ORDER BY LastConnect DESC");

                    if (dt.Rows.Count > 0)
                    {
                        _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", Name, "object", "lblConnectionName", "Text");
                        sTemp = MyGlobal.GetLanguageString("already exists!", "form", Name, "msg", "AlreadyExists", "Text");
                        sTemp2 = MyGlobal.GetLanguageString("Please enter another connection name.", "form", Name, "msg", "EnterAnotherConnectionName", "Text");
                        MessageBox.Show(_sLangText + @" " + txtConnectionName_SQLServer.Text + @" " + sTemp + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtConnectionName_SQLServer.Focus();
                        return false;
                    }
                }

                iRow = c1GridDBInfo.Row; //記住目前在第幾列

                sSql = "UPDATE DBInfo\r\n";
                sSql += "   SET DataSource = '" + cboDataSource.Text + "', DomainUser = '" + lblDomainUser.Text + "', ConnectionName = '" + txtConnectionName_SQLServer.Text + "',\r\n";
                sSql += "       Server = '" + txtServer_SQLServer.Text + "', SID = '', DirectMode = '0', Database = '" + cboDatabase_SQLServer.Text + "', Port = '" + txtPort_SQLServer.Text + "',\r\n";
                sSql += "       TabBackColor = '" + pnlBackColor.Tag + "', TabActiveForeColor = '" + pnlActiveForeColor.Tag + "', TabInactiveForeColor = '" + pnlInactiveForeColor.Tag + "', User = '" + txtUserID_SQLServer.Text + "', ConnectAs = '',\r\n";
                sSql += "       Password = '" + sEncryptedPassword + "',\r\n";
                sSql += "       AutoRollback = '0', Unicode = '0', Remarks = '" + txtRemark_SQLServer.Text.Replace("'", "''") + "', o1 = '" + (chkPooling_SQLServer.Checked ? "1" : "0") + "' , o2 = '" + (chkExcludeNativeObject_SQLServer.Checked ? "1" : "0") + "'";

                if (bSaveOnly == false)
                {
                    sSql += ", LastConnect = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'\r\n";
                }

                sSql += " WHERE PID = " + lblPID.Text;
                DBCommon.ExecNonQuery(sSql);

                //重新載入 DBInfo 資料
                CreateAndGetDbInfoTable(); //SaveConnectData_SQLServer

                c1GridDBInfo.Row = bSaveOnly == false ? 0 : iRow;
                bResult = true;
            }
            else //Add Mode
            {
                //檢查此 Connection Name 是否已存在
                dt = DBCommon.ExecQuery("SELECT * FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND ConnectionName = '" + txtConnectionName_SQLServer.Text + "'");

                if (dt.Rows.Count > 0)
                {
                    _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", Name, "object", "lblConnectionName", "Text");
                    sTemp = MyGlobal.GetLanguageString("already exists!", "form", Name, "msg", "AlreadyExists", "Text");
                    sTemp2 = MyGlobal.GetLanguageString("Please enter another connection name.", "form", Name, "msg", "EnterAnterConnectionName", "Text");
                    MessageBox.Show(_sLangText + @" " + txtConnectionName_SQLServer.Text + @" " + sTemp + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConnectionName_SQLServer.Focus();
                }
                else
                {
                    sSql = "INSERT INTO DBInfo\r\n";
                    sSql += "       (DataSource, DomainUser, ConnectionName,\r\n";
                    sSql += "        Server, SID, DirectMode, Database, Port,\r\n";
                    sSql += "        TabBackColor, TabActiveForeColor, TabInactiveForeColor, User, ConnectAs,\r\n";
                    sSql += "        Password,\r\n";
                    sSql += "        AutoRollback, Unicode, Remarks, o1, o2";

                    if (bSaveOnly == false)
                    {
                        sSql += ", LastConnect";
                    }

                    sSql += ")\r\n";

                    sSql += "VALUES ('" + cboDataSource.Text + "', '" + lblDomainUser.Text + "', '" + txtConnectionName_SQLServer.Text + "', \r\n";
                    sSql += "        '" + txtServer_SQLServer.Text + "', '', '0', '" + cboDatabase_SQLServer.Text + "', '" + txtPort_SQLServer.Text + "', \r\n";
                    sSql += "        '" + pnlBackColor.Tag + "', '" + pnlActiveForeColor.Tag + "', '" + pnlInactiveForeColor.Tag + "', '" + txtUserID_SQLServer.Text + "', '', \r\n";
                    sSql += "        '" + sEncryptedPassword + "', \r\n";
                    sSql += "        '0', '0', '" + txtRemark_SQLServer.Text + "', '" + (chkPooling_SQLServer.Checked ? "1" : "0") + "', '" + (chkExcludeNativeObject_SQLServer.Checked ? "1" : "0") + "'";

                    if (bSaveOnly == false)
                    {
                        sSql += ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'";
                    }

                    sSql += ")";

                    DBCommon.ExecNonQuery(sSql);

                    //重新載入 DBInfo 資料
                    CreateAndGetDbInfoTable(); //SaveConnectData_SQLServer

                    //找到 PID 數值最大的，就是剛剛新增的資料
                    for (var i = 0; i < _dtDbInfo.Rows.Count; i++)
                    {
                        if (iRowNumber == 0)
                        {
                            iRowNumber = Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString());
                            iRow = i;
                        }
                        else
                        {
                            if (iRowNumber >= Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString()))
                            {
                                continue;
                            }

                            iRowNumber = Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString());
                            iRow = i;
                        }
                    }

                    lblPID.Text = iRowNumber.ToString();
                    c1GridDBInfo.Row = iRow; //指標切換到剛剛新增的那一筆的列數
                    bResult = true;
                }
            }

            return bResult;
        }

        private bool SaveConnectData_MySQL(bool bSaveOnly = false)
        {
            var bResult = false;
            string sSql;
            var sEncryptedPassword = "";
            int iRow = 0, iRowNumber = 0;

            if (!CheckData(bSaveOnly))
            {
                return false;
            }

            if (chkSavePasswords.Checked)
            {
                sEncryptedPassword = TextEngine.Encode(TextEngine.Encrypt(txtPassword_MySQL.Text, MyGlobal.sDomainUser));
            }

            string sTemp;
            string sTemp2;
            DataTable dt;

            if (!string.IsNullOrEmpty(lblPID.Text)) //Update Mode
            {
                //Update Mode 時，如果 Connection Name 有變更，檢查是否已經有重覆的 Connection Name 存在了
                if (txtConnectionName_MySQL.Text != txtConnectionName_MySQL.Tag.ToString())
                {
                    dt = DBCommon.ExecQuery("SELECT * FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND ConnectionName = '" + txtConnectionName_MySQL.Text + "' ORDER BY LastConnect DESC");

                    if (dt.Rows.Count > 0)
                    {
                        _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", Name, "object", "lblConnectionName", "Text");
                        sTemp = MyGlobal.GetLanguageString("already exists!", "form", Name, "msg", "AlreadyExists", "Text");
                        sTemp2 = MyGlobal.GetLanguageString("Please enter another connection name.", "form", Name, "msg", "EnterAnotherConnectionName", "Text");
                        MessageBox.Show(_sLangText + @" " + txtConnectionName_MySQL.Text + @" " + sTemp + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtConnectionName_MySQL.Focus();
                        return false;
                    }
                }

                iRow = c1GridDBInfo.Row; //記住目前在第幾列

                sSql = "UPDATE DBInfo\r\n";
                sSql += "   SET DataSource = '" + cboDataSource.Text + "', DomainUser = '" + lblDomainUser.Text + "', ConnectionName = '" + txtConnectionName_MySQL.Text + "',\r\n";
                sSql += "       Server = '" + txtServer_MySQL.Text + "', SID = '', DirectMode = '0', Database = '" + cboDatabase_MySQL.Text + "', Port = '" + txtPort_MySQL.Text + "',\r\n";
                sSql += "       TabBackColor = '" + pnlBackColor.Tag + "', TabActiveForeColor = '" + pnlActiveForeColor.Tag + "', TabInactiveForeColor = '" + pnlInactiveForeColor.Tag + "', User = '" + txtUserID_MySQL.Text + "', ConnectAs = '',\r\n";
                sSql += "       Password = '" + sEncryptedPassword + "',\r\n";
                sSql += "       AutoRollback = '0', Unicode = '" + (chkUnicode_MySQL.Checked ? "1" : "0") + "', Remarks = '" + txtRemark_MySQL.Text.Replace("'", "''") + "', o1 = '" + (chkPooling_MySQL.Checked ? "1" : "0") + "'";

                if (bSaveOnly == false)
                {
                    sSql += ", LastConnect = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'\r\n";
                }

                sSql += " WHERE PID = " + lblPID.Text;
                DBCommon.ExecNonQuery(sSql);

                //重新載入 DBInfo 資料
                CreateAndGetDbInfoTable(); //SaveConnectData_MySQL

                c1GridDBInfo.Row = bSaveOnly == false ? 0 : iRow;
                bResult = true;
            }
            else //Add Mode
            {
                //檢查此 Connection Name 是否已存在
                dt = DBCommon.ExecQuery("SELECT * FROM DBInfo Where DomainUser = '" + MyGlobal.sDomainUser + "' AND ConnectionName = '" + txtConnectionName_MySQL.Text + "'");

                if (dt.Rows.Count > 0)
                {
                    _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", Name, "object", "lblConnectionName", "Text");
                    sTemp = MyGlobal.GetLanguageString("already exists!", "form", Name, "msg", "AlreadyExists", "Text");
                    sTemp2 = MyGlobal.GetLanguageString("Please enter another connection name.", "form", Name, "msg", "EnterAnterConnectionName", "Text");
                    MessageBox.Show(_sLangText + @" " + txtConnectionName_MySQL.Text + @" " + sTemp + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConnectionName_MySQL.Focus();
                }
                else
                {
                    sSql = "INSERT INTO DBInfo\r\n";
                    sSql += "       (DataSource, DomainUser, ConnectionName,\r\n";
                    sSql += "        Server, SID, DirectMode, Database, Port,\r\n";
                    sSql += "        TabBackColor, TabActiveForeColor, TabInactiveForeColor, User, ConnectAs,\r\n";
                    sSql += "        Password,\r\n";
                    sSql += "        AutoRollback, Unicode, Remarks, o1";

                    if (bSaveOnly == false)
                    {
                        sSql += ", LastConnect";
                    }

                    sSql += ")\r\n";

                    sSql += "VALUES ('" + cboDataSource.Text + "', '" + lblDomainUser.Text + "', '" + txtConnectionName_MySQL.Text + "',\r\n";
                    sSql += "        '" + txtServer_MySQL.Text + "', '', '0', '" + cboDatabase_MySQL.Text + "', '" + txtPort_MySQL.Text + "',\r\n";
                    sSql += "        '" + pnlBackColor.Tag + "', '" + pnlActiveForeColor.Tag + "', '" + pnlInactiveForeColor.Tag + "', '" + txtUserID_MySQL.Text + "', '',\r\n";
                    sSql += "        '" + sEncryptedPassword + "',\r\n";
                    sSql += "        '0', '" + (chkUnicode_MySQL.Checked ? "1" : "0") + "', '" + txtRemark_MySQL.Text + "', '" + (chkPooling_MySQL.Checked ? "1" : "0") + "'";

                    if (bSaveOnly == false)
                    {
                        sSql += ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'";
                    }

                    sSql += ")";

                    DBCommon.ExecNonQuery(sSql);

                    //重新載入 DBInfo 資料
                    CreateAndGetDbInfoTable(); //SaveConnectData_MySQL

                    //找到 PID 數值最大的，就是剛剛新增的資料
                    for (var i = 0; i < _dtDbInfo.Rows.Count; i++)
                    {
                        if (iRowNumber == 0)
                        {
                            iRowNumber = Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString());
                            iRow = i;
                        }
                        else
                        {
                            if (iRowNumber >= Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString()))
                            {
                                continue;
                            }

                            iRowNumber = Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString());
                            iRow = i;
                        }
                    }

                    lblPID.Text = iRowNumber.ToString();
                    c1GridDBInfo.Row = iRow; //指標切換到剛剛新增的那一筆的列數
                    bResult = true;
                }
            }

            return bResult;
        }

        private bool SaveConnectData_SQLite(bool bSaveOnly = false)
        {
            var bResult = false;
            string sSql;
            var sEncryptedPassword = "";
            int iRow = 0, iRowNumber = 0;

            if (!CheckData(bSaveOnly))
            {
                return false;
            }

            if (chkSavePasswords.Checked && !string.IsNullOrEmpty(txtPassword_SQLite.Text.Trim()))
            {
                sEncryptedPassword = TextEngine.Encode(TextEngine.Encrypt(txtPassword_SQLite.Text, MyGlobal.sDomainUser));
            }

            string sTemp;
            string sTemp2;
            DataTable dt;

            if (!string.IsNullOrEmpty(lblPID.Text)) //Update Mode
            {
                //Update Mode 時，如果 Connection Name 有變更，檢查是否已經有重覆的 Connection Name 存在了
                if (txtConnectionName_SQLite.Text != txtConnectionName_SQLite.Tag.ToString())
                {
                    dt = DBCommon.ExecQuery("SELECT * FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND ConnectionName = '" + txtConnectionName_SQLite.Text + "' ORDER BY LastConnect DESC");

                    if (dt.Rows.Count > 0)
                    {
                        _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", Name, "object", "lblConnectionName", "Text");
                        sTemp = MyGlobal.GetLanguageString("already exists!", "form", Name, "msg", "AlreadyExists", "Text");
                        sTemp2 = MyGlobal.GetLanguageString("Please enter another connection name.", "form", Name, "msg", "EnterAnotherConnectionName", "Text");
                        MessageBox.Show(_sLangText + @" " + txtConnectionName_SQLite.Text + @" " + sTemp + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtConnectionName_SQLite.Focus();
                        return false;
                    }
                }

                iRow = c1GridDBInfo.Row; //記住目前在第幾列

                sSql = "UPDATE DBInfo\r\n";
                sSql += "   SET DataSource = '" + cboDataSource.Text + "', DomainUser = '" + lblDomainUser.Text + "', ConnectionName = '" + txtConnectionName_SQLite.Text + "',\r\n";
                sSql += "       Server = '" + Path.GetFileName(txtFile_SQLite.Text) + "', SID = '', DirectMode = '0', Database = '', Port = '',\r\n";
                sSql += "       TabBackColor = '" + pnlBackColor.Tag + "', TabActiveForeColor = '" + pnlActiveForeColor.Tag + "', TabInactiveForeColor = '" + pnlInactiveForeColor.Tag + "', User = '', ConnectAs = '',\r\n";
                sSql += "       Password = '" + sEncryptedPassword + "',\r\n";
                sSql += "       AutoRollback = '0', Unicode = '0', Remarks = '" + txtRemark_SQLite.Text.Replace("'", "''") + "', o3 = '" + txtFile_SQLite.Text + "', o4 = '" + cboDatabaseType_SQLite.Text + "', o5 = '" + (rdoWithPassword.Checked ? "1" : "0") + "'";

                if (bSaveOnly == false)
                {
                    sSql += ", LastConnect = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'\r\n";
                }

                sSql += " WHERE PID = " + lblPID.Text;
                DBCommon.ExecNonQuery(sSql);

                //重新載入 DBInfo 資料
                CreateAndGetDbInfoTable(); //SaveConnectData_SQLite

                c1GridDBInfo.Row = bSaveOnly == false ? 0 : iRow;
                bResult = true;
            }
            else //Add Mode
            {
                //檢查此 Connection Name 是否已存在
                dt = DBCommon.ExecQuery("SELECT * FROM DBInfo Where DomainUser = '" + MyGlobal.sDomainUser + "' AND ConnectionName = '" + txtConnectionName_SQLite.Text + "'");

                if (dt.Rows.Count > 0)
                {
                    _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", Name, "object", "lblConnectionName", "Text");
                    sTemp = MyGlobal.GetLanguageString("already exists!", "form", Name, "msg", "AlreadyExists", "Text");
                    sTemp2 = MyGlobal.GetLanguageString("Please enter another connection name.", "form", Name, "msg", "EnterAnterConnectionName", "Text");
                    MessageBox.Show(_sLangText + @" " + txtConnectionName_SQLite.Text + @" " + sTemp + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConnectionName_SQLite.Focus();
                }
                else
                {
                    sSql = "INSERT INTO DBInfo\r\n";
                    sSql += "       (DataSource, DomainUser, ConnectionName,\r\n";
                    sSql += "        Server, SID, DirectMode, Database, Port,\r\n";
                    sSql += "        TabBackColor, TabActiveForeColor, TabInactiveForeColor, User, ConnectAs,\r\n";
                    sSql += "        Password,\r\n";
                    sSql += "        AutoRollback, Unicode, Remarks, o3, o4, o5";

                    if (bSaveOnly == false)
                    {
                        sSql += ", LastConnect";
                    }

                    sSql += ")\r\n";

                    sSql += "VALUES ('" + cboDataSource.Text + "', '" + lblDomainUser.Text + "', '" + txtConnectionName_SQLite.Text + "',\r\n";
                    sSql += "        '" + Path.GetFileName(txtFile_SQLite.Text) + "', '', '0', '', '',\r\n";
                    sSql += "        '" + pnlBackColor.Tag + "', '" + pnlActiveForeColor.Tag + "', '" + pnlInactiveForeColor.Tag + "', '', '',\r\n";
                    sSql += "        '" + sEncryptedPassword + "',\r\n";
                    sSql += "        '0', '0', '" + txtRemark_SQLite.Text + "', '" + txtFile_SQLite.Text + "', '" + cboDatabaseType_SQLite.Text + "', '" + (rdoWithPassword.Checked ? "1" : "0") + "'";

                    if (bSaveOnly == false)
                    {
                        sSql += ", '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "'";
                    }

                    sSql += ")";

                    DBCommon.ExecNonQuery(sSql);

                    //重新載入 DBInfo 資料
                    CreateAndGetDbInfoTable(); //SaveConnectData_SQLite

                    //找到 PID 數值最大的，就是剛剛新增的資料
                    for (var i = 0; i < _dtDbInfo.Rows.Count; i++)
                    {
                        if (iRowNumber == 0)
                        {
                            iRowNumber = Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString());
                            iRow = i;
                        }
                        else
                        {
                            if (iRowNumber >= Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString()))
                            {
                                continue;
                            }

                            iRowNumber = Convert.ToInt32(c1GridDBInfo.Columns["PID"].CellValue(i).ToString());
                            iRow = i;
                        }
                    }

                    lblPID.Text = iRowNumber.ToString();
                    c1GridDBInfo.Row = iRow; //指標切換到剛剛新增的那一筆的列數
                    var bCreateDBFile = false;

                    //20220617 產生指定的新的 SQLite file
                    try
                    {
                        if (cboDatabaseType_SQLite.Text.StartsWith("SQLCipher"))
                        {
                            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("JasonQuery.Files.newSQLCipher.db");
                            var fileStream = new FileStream(txtFile_SQLite.Text, FileMode.CreateNew);

                            for (var i = 0; i < stream.Length; i++)
                            {
                                fileStream.WriteByte((byte)stream.ReadByte());
                            }

                            fileStream.Close();

                            if (File.Exists(txtFile_SQLite.Text))
                            {
                                txtFile_SQLite.ForeColor = SystemColors.WindowText;

                                if (rdoWithPassword.Checked)
                                {
                                    string sFilename = Path.GetTempFileName();
                                    File.Delete(sFilename);

                                    var builder = new SQLiteConnectionStringBuilder
                                    {
                                        DataSource = txtFile_SQLite.Text,
                                        Encryption = EncryptionMode.SQLCipher, //EncryptionLicenseKey = sEncryptionLicenseKey
                                        FailIfMissing = false,
                                        Password = "",
                                        Pooling = false,
                                        WritableSchema = true
                                    };

                                    MyGlobal.oSQLCipherReader.ConnectTo(builder.ConnectionString, "NEW", txtPassword_SQLite.Tag?.ToString(), sFilename);
                                    MyGlobal.oSQLCipherReader.oDisconnect();

                                    File.Copy(sFilename, txtFile_SQLite.Text, true);
                                }
                            }

                            bCreateDBFile = true;
                        }
                        else
                        {
                            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("JasonQuery.Files.newSQLite3.db");
                            var fileStream = new FileStream(txtFile_SQLite.Text, FileMode.CreateNew);

                            for (var i = 0; i < stream.Length; i++)
                            {
                                fileStream.WriteByte((byte)stream.ReadByte());
                            }

                            fileStream.Close();

                            if (File.Exists(txtFile_SQLite.Text))
                            {
                                txtFile_SQLite.ForeColor = SystemColors.WindowText;

                                if (rdoWithPassword.Checked)
                                {
                                    string sFilename = Path.GetTempFileName();
                                    File.Delete(sFilename);

                                    var builder = new System.Data.SQLite.SQLiteConnectionStringBuilder
                                    {
                                        DataSource = txtFile_SQLite.Text,
                                        FailIfMissing = false,
                                        Password = "",
                                        Pooling = false,
                                        Version = 3
                                    };

                                    MyGlobal.oSQLiteReader.ConnectTo(builder.ConnectionString, "NEW", txtPassword_SQLite.Tag?.ToString(), sFilename);
                                    MyGlobal.oSQLiteReader.oDisconnect();
                                }
                            }

                            bCreateDBFile = true;
                        }
                    }
                    catch (Exception)
                    {
                        //
                    }

                    bResult = bCreateDBFile;
                }
            }

            return bResult;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            foreach (var t in _lstPicLogo)
            {
                t.Visible = false;
            }

            lblPID.Text = "";
            lblDomainUser.Text = MyGlobal.sDomainUser;
            cboDataSource.Enabled = true;
            cboDataSource.SelectedIndex = -1;
            cboDataSource.Focus();
            chkSavePasswords.Checked = false;
            chkShowColumnInfo.Checked = false;
            txtSupportInfo.Text = "";
            btnDelete.Enabled = false;
            btnCopy.Enabled = false;

            var rng = new Random(Guid.NewGuid().GetHashCode());
            var i = rng.Next(0, 8);

            switch (i)
            {
                case 0:
                    pnlBackColor.BackColor = Color.YellowGreen;
                    break;
                case 1:
                    pnlBackColor.BackColor = Color.LightBlue;
                    break;
                case 2:
                    pnlBackColor.BackColor = Color.LightPink;
                    break;
                case 3:
                    pnlBackColor.BackColor = Color.LightSeaGreen;
                    break;
                case 4:
                    pnlBackColor.BackColor = Color.LightSalmon;
                    break;
                case 5:
                    pnlBackColor.BackColor = Color.Plum;
                    break;
                case 6:
                    pnlBackColor.BackColor = Color.Orange;
                    break;
                default:
                    pnlBackColor.BackColor = Color.Gold;
                    break;
            }

            pnlBackColor.Tag = ColorTranslator.ToHtml(Color.FromArgb(pnlBackColor.BackColor.ToArgb())); //"#9ACD32"
            pnlActiveForeColor.BackColor = Color.Black;
            pnlActiveForeColor.Tag = ColorTranslator.ToHtml(Color.FromArgb(pnlActiveForeColor.BackColor.ToArgb())); //"#000000"
            pnlInactiveForeColor.BackColor = ColorTranslator.FromHtml("#7F7F7F");
            pnlInactiveForeColor.Tag = ColorTranslator.ToHtml(Color.FromArgb(pnlInactiveForeColor.BackColor.ToArgb())); //"#7F7F7F"

            _toolTip1.SetToolTip(pnlBackColor, pnlBackColor.Tag + " " + "(R:" + pnlBackColor.BackColor.R + ", G:" + pnlBackColor.BackColor.G + ", B:" + pnlBackColor.BackColor.B + ")");
            _toolTip1.SetToolTip(pnlActiveForeColor, pnlActiveForeColor.Tag + " " + "(R:" + pnlActiveForeColor.BackColor.R + ", G:" + pnlActiveForeColor.BackColor.G + ", B:" + pnlActiveForeColor.BackColor.B + ")");
            _toolTip1.SetToolTip(pnlInactiveForeColor, pnlInactiveForeColor.Tag + " " + "(R:" + pnlInactiveForeColor.BackColor.R + ", G:" + pnlInactiveForeColor.BackColor.G + ", B:" + pnlInactiveForeColor.BackColor.B + ")");

            tabExample.BackColor = pnlBackColor.BackColor;
            tabExample.ForeColor = pnlActiveForeColor.BackColor; //Color.Black;
            tabExample.TextInactiveColor = pnlInactiveForeColor.BackColor;
            pnlPostgreSQL.Visible = false;
            pnlOracle.Visible = false;
            pnlSQLServer.Visible = false;
            pnlMySQL.Visible = false;
            pnlSQLite.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            sPassword1 = sPassword2;

            if (SaveConnectData(true)) //btnSave_Click
            {
                btnCopy.Enabled = true;
            }

            sPassword2 = sPassword1;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            MyGlobal.bShowColumnInfo = chkShowColumnInfo.Checked; //先記住使用者是否有勾選

            //針對「不儲存密碼」，先記住密碼內容
            switch (cboDataSource.Text)
            {
                case "Oracle":
                    sPassword1 = txtPassword_Oracle.Text;
                    break;
                case "PostgreSQL":
                    sPassword1 = txtPassword_PostgreSQL.Text;
                    break;
                case "SQL Server":
                    sPassword1 = txtPassword_SQLServer.Text;
                    break;
                case "MySQL/MariaDB":
                    sPassword1 = txtPassword_MySQL.Text;
                    break;
                case "SQLite":
                    sPassword1 = txtPassword_SQLite.Text;
                    break;
                default:
                    sPassword1 = "";
                    break;
            }

            if (!SaveConnectData())
            {
                return;
            }

            //針對「不儲存密碼」，此處還原密碼內容
            switch (cboDataSource.Text)
            {
                case "Oracle":
                    sPassword2 = sPassword1;
                    txtPassword_Oracle.Text = sPassword2;
                    break;
                case "PostgreSQL":
                    sPassword2 = sPassword1;
                    txtPassword_PostgreSQL.Text = sPassword2;
                    break;
                case "SQL Server":
                    sPassword2 = sPassword1;
                    txtPassword_SQLServer.Text = sPassword2;
                    break;
                case "MySQL/MariaDB":
                    sPassword2 = sPassword1;
                    txtPassword_MySQL.Text = sPassword2;
                    break;
                case "SQLite":
                    sPassword2 = sPassword1;
                    txtPassword_SQLite.Text = sPassword2;
                    break;
            }

            //20230511 允許在連線時選擇「Dark Mode」
            //20230511 不能在此表單切換「Dark Mode」，因為顏色的部份是在選項表單才會儲存的！
            //MyGlobal.sDBMotherPID = lblPID.Text;
            //MyGlobal.UpdateSetting("GeneralConfig", "DarkMode", chkDarkMode.Checked ? "1" : "0");
            //MyLibrary.bDarkMode = chkDarkMode.Checked;

            chkShowColumnInfo.Checked = MyGlobal.bShowColumnInfo; //還原使用者是否有勾選
            ConnectToDatabase(); //btnConnect_Click
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var sConnectionName = "";
            var sServer = "";
            var sDatabase = "";

            _sLangText = MyGlobal.GetLanguageString("Database:", "form", Name, "object", "lblDatabase", "Text");

            switch (cboDataSource.Text)
            {
                case "Oracle":
                    sConnectionName = txtConnectionName_Oracle.Text;
                    sServer = txtServer_Oracle.Text;
                    break;
                case "PostgreSQL":
                    sConnectionName = txtConnectionName_PostgreSQL.Text;
                    sServer = txtServer_PostgreSQL.Text;

                    sDatabase = _sLangText + " " + cboDatabase_PostgreSQL.Text;
                    break;
                case "SQL Server":
                    sConnectionName = txtConnectionName_SQLServer.Text;
                    sServer = txtServer_SQLServer.Text;

                    sDatabase = _sLangText + " " + cboDatabase_SQLServer.Text;
                    break;
                case "MySQL/MariaDB":
                    sConnectionName = txtConnectionName_MySQL.Text;
                    sServer = txtServer_MySQL.Text;

                    sDatabase = _sLangText + " " + cboDatabase_MySQL.Text;
                    break;
                case "SQLite":
                    sConnectionName = txtConnectionName_SQLite.Text;
                    sServer = txtFile_SQLite.Text;

                    sDatabase = "";
                    break;
            }

            var sTemp = MyGlobal.GetLanguageString("Are you sure you want to delete the record below?", "form", Name, "msg", "DeleteRecord", "Text");
            sTemp += "\r\n" + MyGlobal.GetLanguageString("All related records will be deleted.", "form", Name, "msg", "AllRelatedRecords", "Text");
            _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", Name, "object", "lblConnectionName", "Text");
            sTemp += "\r\n\r\n" + _sLangText + " " + sConnectionName + "\r\n";

            if (cboDataSource.Text == "SQLite")
            {
                _sLangText = MyGlobal.GetLanguageString("Database File:", "form", Name, "object", "lblDatabaseFile_SQLite", "Text") + "\r\n";
            }
            else
            {
                _sLangText = MyGlobal.GetLanguageString("Server:", "form", Name, "object", "lblServer", "Text");
            }

            sTemp += _sLangText + " " + sServer + "\r\n";
            sTemp += sDatabase;

            if (MessageBox.Show(sTemp, @"JasonQuery", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                return;
            }

            DBCommon.ExecNonQuery("DELETE FROM SQLHistory WHERE MPID = " + lblPID.Text);
            DBCommon.ExecNonQuery("DELETE FROM SystemConfig WHERE MPID = " + lblPID.Text);
            DBCommon.ExecNonQuery("DELETE FROM DBInfo WHERE PID = " + lblPID.Text);

            //重新載入 DBInfo 資料
            CreateAndGetDbInfoTable(); //btnDelete_Click

            //指定哪一個 Column 要套用 FetchCellStyle (這裡要重新指定一次，並 Refresh，才會顯示顏色，而不是顏色代碼)
            c1GridDBInfo.Splits[0].DisplayColumns[_lstGridHeader[(int)eMenu.eTabBackColor]].FetchStyle = true;
            c1GridDBInfo.Refresh();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (!CheckData(false))
            {
                return;
            }

            ConnectToDatabase(true);  //btnTest_Click

            //以下還原，才不會引發錯誤 & 變更 Tab Color
            MyGlobal.sDataSource = "";
            MyGlobal.sTabBackColor = "";
            MyGlobal.sTabActiveForeColor = "";
            MyGlobal.sTabInactiveForeColor = "";
        }

        private void ConnectToDatabase(bool bTesting = false)
        {
            switch (cboDataSource.Text)
            {
                case "Oracle":
                    ConnectToDatabase_Oracle(bTesting); //ConnectToDatabase
                    break;
                case "PostgreSQL":
                    ConnectToDatabase_PostgreSQL(bTesting); //ConnectToDatabase
                    break;
                case "SQL Server":
                    ConnectToDatabase_SQLServer(bTesting); //ConnectToDatabase
                    break;
                case "MySQL/MariaDB":
                    ConnectToDatabase_MySQL(bTesting); //ConnectToDatabase
                    break;
                case "SQLite":
                    if (rdoOpen_SQLite.Checked && !File.Exists(txtFile_SQLite.Text))
                    {
                        _sLangText = MyGlobal.GetLanguageString("The following file could not be found.", "form", Name, "msg", "FollowingFileNotFound", "Text") + "\r\n";
                        _sLangText += MyGlobal.GetLanguageString("Please check the file name and try again.", "form", Name, "msg", "CheckFileAndTry", "Text") + "\r\n\r\n";
                        MessageBox.Show(_sLangText + txtFile_SQLite.Text, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (cboDatabaseType_SQLite.Text.StartsWith("SQLCipher"))
                    {
                        ConnectToDatabase_SQLCipher(bTesting); //ConnectToDatabase
                    }
                    else
                    {
                        ConnectToDatabase_SQLite(bTesting); //ConnectToDatabase
                    }

                    break;
            }
        }

        private void ConnectToDatabase_Oracle(bool bTesting = false)
        {
            MyGlobal.sDBMotherPID = lblPID.Text;
            MyGlobal.sDataSource = cboDataSource.Text;
            MyGlobal.sDBConnectionTitle = "(" + cboDataSource.Text + ") (" + txtConnectionName_Oracle.Text + ") " + txtUserID_Oracle.Text + "@" + txtServer_Oracle.Text + ":" + txtPort_Oracle.Text;
            MyGlobal.sDBConnectionName = txtConnectionName_Oracle.Text;
            //MyLibrary.sTabControlColor = cboTabBackColor.SelectedValue.ToString();
            MyGlobal.sTabBackColor = pnlBackColor.Tag.ToString();
            MyGlobal.sTabActiveForeColor = pnlActiveForeColor.Tag.ToString();
            MyGlobal.sTabInactiveForeColor = pnlInactiveForeColor.Tag.ToString();
            MyGlobal.sDBConnectionServer = txtServer_Oracle.Text;
            MyGlobal.bDBAutoRollback = false;
            MyGlobal.bDBDirectMode = chkDirectMode_Oracle.Checked;
            MyGlobal.bDBPooling = chkPooling_Oracle.Checked;
            MyGlobal.sSupportInfo = txtSupportInfo.Text;
            MyGlobal.sDBConnectionDatabase = "";

            //MyGlobal.sDBConnectionString = "User Id=" + txtUser.Text + ";Password=" + txtPassword.Text + ";Data Source=(ADDRESS=(PROTOCOL=TCP)(HOST=" + txtServer.Text + ")(PORT=" + txtPort.Text + "))(CONNECT_DATA=(SERVICE_NAME=" + txtDatabase.Text + "));";
            MyGlobal.sDBUser = txtUserID_Oracle.Text;
            MyGlobal.sDBPW = txtPassword_Oracle.Text;
            MyGlobal.iDBConnectionPort = Convert.ToInt32(txtPort_Oracle.Text);
            MyGlobal.sDBConnectionSid = txtSID_Oracle.Text;
            MyGlobal.bDBUnicode = chkUnicode_Oracle.Checked;
            MyGlobal.sDBConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + txtServer_Oracle.Text + ")(PORT=" + txtPort_Oracle.Text + ")))(CONNECT_DATA=(SERVICE_NAME=)));Persist Security Info=True;User ID=" + txtUserID_Oracle.Text + ";Password=" + txtPassword_Oracle.Text + ";";

            switch (cboConnectAs_Oracle.Text.ToUpper())
            {
                case "SYSDBA":
                    MyGlobal.sDBConnectionConnectAs = @"SysDba";
                    MyGlobal.sDBConnectionString = string.Concat(MyGlobal.sDBConnectionString, "DBA Privilege=SYSDBA;");
                    //MyGlobal.sDBConnectionString = "User Id=sys;Password=84193721;Data Source=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE));DBA Privilege=SYSDBA;";
                    break;
                case "SYSOPER":
                    //MyGlobal.sDBConnectionString = MyGlobal.sDBConnectionString + "DBA Privilege=SYSOPER;";
                    MyGlobal.sDBConnectionConnectAs = @"SysOper";
                    MyGlobal.sDBConnectionString = string.Concat(MyGlobal.sDBConnectionString, "DBA Privilege=SYSOPER;");
                    break;
            }

            if (bTesting == false)
            {
                Close();
            }
            else
            {
                var sConnectTo = ConnectToDatabase2(); //Oracle

                if (!string.IsNullOrEmpty(sConnectTo))
                {
                    _sLangText = MyGlobal.GetLanguageString("Test connection failed", "Global", "Global", "msg", "TestNG", "Text");
                    var sLangText = _sLangText + "\r\n\r\n";
                    _sLangText = MyGlobal.GetLanguageString("Error connecting to the server:", "Global", "Global", "msg", "ErrorConnectingToTheServer", "Text");
                    sLangText = sLangText + _sLangText + "\r\n\r\n";
                    _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", "frmConnect", "object", "lblConnectionName", "Text");
                    sLangText = sLangText + _sLangText + " " + MyGlobal.sDBConnectionName + "\r\n";
                    _sLangText = MyGlobal.GetLanguageString("Server:", "form", "frmConnect", "object", "lblServer", "Text");
                    sLangText = sLangText + _sLangText + " " + MyGlobal.sDBConnectionServer + "\r\n\r\n" + sConnectTo;
                    MessageBox.Show(sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _sLangText = MyGlobal.GetLanguageString("Test connection succeeded.", "Global", "Global", "msg", "TestOK", "Text");
                    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ConnectToDatabase_PostgreSQL(bool bTesting = false, bool bUpdateDatabaseList = false)
        {
            MyGlobal.sDBMotherPID = lblPID.Text;
            MyGlobal.sDataSource = cboDataSource.Text;
            MyGlobal.sDBConnectionTitle = "(" + cboDataSource.Text + ") (" + txtConnectionName_PostgreSQL.Text + ") " + cboDatabase_PostgreSQL.Text + " on " + txtUserID_PostgreSQL.Text + "@" + txtServer_PostgreSQL.Text + ":" + txtPort_PostgreSQL.Text;
            MyGlobal.sDBConnectionName = txtConnectionName_PostgreSQL.Text;
            MyGlobal.sTabBackColor = pnlBackColor.Tag.ToString();
            MyGlobal.sTabActiveForeColor = pnlActiveForeColor.Tag.ToString();
            MyGlobal.sTabInactiveForeColor = pnlInactiveForeColor.Tag.ToString();
            MyGlobal.sDBConnectionServer = txtServer_PostgreSQL.Text;
            MyGlobal.bDBAutoRollback = chkAutoRollback_PostgreSQL.Checked;
            MyGlobal.bDBPooling = chkPooling_PostgreSQL.Checked;
            MyGlobal.sSupportInfo = txtSupportInfo.Text;
            MyGlobal.sDBConnectionDatabase = cboDatabase_PostgreSQL.Text;

            var builder = new PgSqlConnectionStringBuilder
            {
                UserId = txtUserID_PostgreSQL.Text,
                Password = txtPassword_PostgreSQL.Text,
                PersistSecurityInfo = chkSavePasswords.Checked,
                Host = txtServer_PostgreSQL.Text,
                Port = Convert.ToInt16(txtPort_PostgreSQL.Text),
                Pooling = chkPooling_PostgreSQL.Checked,
                Database = cboDatabase_PostgreSQL.Text,
                ConnectionTimeout = 15,
                DefaultCommandTimeout = 0,
                Unicode = chkUnicode_PostgreSQL.Checked
            };

            MyGlobal.sDBConnectionString = "Server=" + txtServer_PostgreSQL.Text + ";Port=" + txtPort_PostgreSQL.Text + ";User Id=" + txtUserID_PostgreSQL.Text + ";Password=" + txtPassword_PostgreSQL.Text + (bUpdateDatabaseList ? "" : ";Database=" + cboDatabase_PostgreSQL.Text) + ";" + (chkUnicode_PostgreSQL.Checked == false ? "" : "Unicode=true;") + (chkPooling_PostgreSQL.Checked == false ? "Pooling=false;" : "Pooling=true;") + "Connection Timeout=15;Default Command Timeout=0;";

            MyGlobal.sDBConnectionString = builder.ConnectionString;

            if (bTesting == false)
            {
                Close();
            }
            else
            {
                var sConnectTo = ConnectToDatabase2(bUpdateDatabaseList); //PostgreSQL

                if (!string.IsNullOrEmpty(sConnectTo))
                {
                    _sLangText = MyGlobal.GetLanguageString("Test connection failed", "Global", "Global", "msg", "TestNG", "Text");
                    var sLangText = "";

                    if (bUpdateDatabaseList == false)
                    {
                        sLangText = _sLangText + "\r\n\r\n";
                    }

                    _sLangText = MyGlobal.GetLanguageString("Error connecting to the server:", "Global", "Global", "msg", "ErrorConnectingToTheServer", "Text");
                    sLangText = sLangText + _sLangText + "\r\n\r\n";
                    _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", "frmConnect", "object", "lblConnectionName", "Text");
                    sLangText = sLangText + _sLangText + " " + MyGlobal.sDBConnectionName + "\r\n";
                    _sLangText = MyGlobal.GetLanguageString("Server:", "form", "frmConnect", "object", "lblServer", "Text");
                    sLangText = sLangText + _sLangText + " " + MyGlobal.sDBConnectionServer + "\r\n\r\n" + sConnectTo;
                    MessageBox.Show(sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (bUpdateDatabaseList)
                    {
                        return;
                    }

                    _sLangText = MyGlobal.GetLanguageString("Test connection succeeded.", "Global", "Global", "msg", "TestOK", "Text");
                    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ConnectToDatabase_SQLServer(bool bTesting = false, bool bUpdateDatabaseList = false)
        {
            MyGlobal.sDBMotherPID = lblPID.Text;
            MyGlobal.sDataSource = cboDataSource.Text;
            MyGlobal.sDBConnectionTitle = "(" + cboDataSource.Text + ") (" + txtConnectionName_SQLServer.Text + ") " + (string.IsNullOrEmpty(cboDatabase_SQLServer.Text) ? "" : cboDatabase_SQLServer.Text + " ") + "on " + txtUserID_SQLServer.Text + "@" + txtServer_SQLServer.Text;
            MyGlobal.sDBConnectionName = txtConnectionName_SQLServer.Text;
            MyGlobal.sTabBackColor = pnlBackColor.Tag.ToString();
            MyGlobal.sTabActiveForeColor = pnlActiveForeColor.Tag.ToString();
            MyGlobal.sTabInactiveForeColor = pnlInactiveForeColor.Tag.ToString();
            MyGlobal.sDBConnectionServer = txtServer_SQLServer.Text;
            MyGlobal.sSupportInfo = txtSupportInfo.Text;
            MyGlobal.sDBConnectionDatabase = cboDatabase_SQLServer.Text;
            MyGlobal.bDBPooling = chkPooling_SQLServer.Checked;
            MyGlobal.bDBExcludeNativeDatabase = chkExcludeNativeObject_SQLServer.Checked;

            var builder = new SqlConnectionStringBuilder();
            {
                builder.IntegratedSecurity = false;//, //false = 採用「SQL Server Authentication」
                builder.UserID = txtUserID_SQLServer.Text;
                builder.Password = txtPassword_SQLServer.Text;
                builder.PersistSecurityInfo = chkSavePasswords.Checked;
                builder.DataSource = txtServer_SQLServer.Text + (string.IsNullOrEmpty(txtPort_SQLServer.Text) || txtPort_SQLServer.Text == "0" ? "" : "," + txtPort_SQLServer.Text);
                builder.ConnectTimeout = 60;
                if (string.IsNullOrEmpty(cboDatabase_SQLServer.Text))
                {
                    builder.InitialCatalog = "";
                }
                else
                {
                    builder.InitialCatalog = bUpdateDatabaseList ? "" : cboDatabase_SQLServer.Text; //20220824 針對「撈取所有資料庫」，改為不帶入 database，避免 database 是錯誤的，造成連線錯誤，而取不到「資料庫清單」
                }

                builder.Pooling = chkPooling_SQLServer.Checked;
            };

            MyGlobal.sDBConnectionString = builder.ConnectionString;

            if (bTesting == false)
            {
                Close();
            }
            else
            {
                var sConnectTo = ConnectToDatabase2(bUpdateDatabaseList); //SQL Server

                if (!string.IsNullOrEmpty(sConnectTo))
                {
                    _sLangText = MyGlobal.GetLanguageString("Test connection failed", "Global", "Global", "msg", "TestNG", "Text");
                    var sLangText = "";

                    if (bUpdateDatabaseList == false)
                    {
                        sLangText = _sLangText + "\r\n\r\n";
                    }

                    _sLangText = MyGlobal.GetLanguageString("Error connecting to the server:", "Global", "Global", "msg", "ErrorConnectingToTheServer", "Text");
                    sLangText = sLangText + _sLangText + "\r\n\r\n";
                    _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", "frmConnect", "object", "lblConnectionName", "Text");
                    sLangText = sLangText + _sLangText + " " + MyGlobal.sDBConnectionName + "\r\n";
                    _sLangText = MyGlobal.GetLanguageString("Server:", "form", "frmConnect", "object", "lblServer", "Text");
                    sLangText = sLangText + _sLangText + " " + MyGlobal.sDBConnectionServer + (string.IsNullOrEmpty(txtPort_SQLServer.Text) || txtPort_SQLServer.Text == "0" ? "" : "," + txtPort_SQLServer.Text) + "\r\n\r\n" + sConnectTo;

                    if (sConnectTo.IndexOf("(provider: TCP Provider, error: 0", StringComparison.Ordinal) != -1)
                    {
                        sLangText += "\r\n" + MyGlobal.GetLanguageString("Please double check that the TCP/IP protocol is enabled and the port number is correct.", "form", "frmConnect", "msg", "ServerTCPIPPortIssue", "Text");
                    }

                    MessageBox.Show(sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (bUpdateDatabaseList)
                    {
                        return;
                    }

                    _sLangText = MyGlobal.GetLanguageString("Test connection succeeded.", "Global", "Global", "msg", "TestOK", "Text");
                    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ConnectToDatabase_MySQL(bool bTesting = false, bool bUpdateDatabaseList = false)
        {
            MyGlobal.sDBMotherPID = lblPID.Text;
            MyGlobal.sDataSource = cboDataSource.Text.Replace("/MariaDB", "");
            MyGlobal.sDBConnectionTitle = "(" + cboDataSource.Text + ") (" + txtConnectionName_MySQL.Text + ") " + (string.IsNullOrEmpty(cboDatabase_MySQL.Text) ? "" : cboDatabase_MySQL.Text + " ") + "on " + txtUserID_MySQL.Text + "@" + txtServer_MySQL.Text;
            MyGlobal.sDBConnectionName = txtConnectionName_MySQL.Text;
            MyGlobal.sTabBackColor = pnlBackColor.Tag.ToString();
            MyGlobal.sTabActiveForeColor = pnlActiveForeColor.Tag.ToString();
            MyGlobal.sTabInactiveForeColor = pnlInactiveForeColor.Tag.ToString();
            MyGlobal.sDBConnectionServer = txtServer_MySQL.Text;
            MyGlobal.sSupportInfo = txtSupportInfo.Text;
            MyGlobal.sDBConnectionDatabase = cboDatabase_MySQL.Text;
            MyGlobal.bDBPooling = chkPooling_MySQL.Checked;

            var builder = new MySqlConnectionStringBuilder
            {
                UserId = txtUserID_MySQL.Text,
                Password = txtPassword_MySQL.Text,
                PersistSecurityInfo = chkSavePasswords.Checked,
                Host = txtServer_MySQL.Text,
                Port = Convert.ToInt16(txtPort_MySQL.Text),
                Pooling = chkPooling_MySQL.Checked,
                Database = bUpdateDatabaseList ? "" : cboDatabase_MySQL.Text, //20220824 針對「撈取所有資料庫」，改為不帶入 database，避免 database 是錯誤的，造成連線錯誤，而取不到「資料庫清單」
                Unicode = chkUnicode_MySQL.Checked,
                Protocol = MySqlProtocol.Tcp
            };

            MyGlobal.sDBConnectionString = "User Id=" + txtUserID_MySQL.Text + ";Password=" + txtPassword_MySQL.Text + ";Host=" + txtServer_MySQL.Text + (string.IsNullOrEmpty(cboDatabase_MySQL.Text) ? "" : (bUpdateDatabaseList ? "" : ";Database=" + cboDatabase_MySQL.Text)) + ";" + (chkUnicode_MySQL.Checked == false ? "" : "Unicode=true;") + (chkPooling_MySQL.Checked == false ? "Pooling=false;" : "Pooling=true;") + "Protocol=Tcp;" + (string.IsNullOrEmpty(txtPort_MySQL.Text) || txtPort_MySQL.Text == "0" ? "" : "Port=" + txtPort_MySQL.Text + ";");

            MyGlobal.sDBConnectionString = builder.ConnectionString;

            if (bTesting == false)
            {
                Close();
            }
            else
            {
                var sConnectTo = ConnectToDatabase2(bUpdateDatabaseList); //MySQL

                if (!string.IsNullOrEmpty(sConnectTo))
                {
                    _sLangText = MyGlobal.GetLanguageString("Test connection failed", "Global", "Global", "msg", "TestNG", "Text");
                    var sLangText = "";

                    if (bUpdateDatabaseList == false)
                    {
                        sLangText = _sLangText + "\r\n\r\n";
                    }

                    _sLangText = MyGlobal.GetLanguageString("Error connecting to the server:", "Global", "Global", "msg", "ErrorConnectingToTheServer", "Text");
                    sLangText = sLangText + _sLangText + "\r\n\r\n";
                    _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", "frmConnect", "object", "lblConnectionName", "Text");
                    sLangText = sLangText + _sLangText + " " + MyGlobal.sDBConnectionName + "\r\n";
                    _sLangText = MyGlobal.GetLanguageString("Server:", "form", "frmConnect", "object", "lblServer", "Text");
                    sLangText = sLangText + _sLangText + " " + MyGlobal.sDBConnectionServer + "\r\n\r\n" + sConnectTo;
                    MessageBox.Show(sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (bUpdateDatabaseList)
                    {
                        return;
                    }

                    _sLangText = MyGlobal.GetLanguageString("Test connection succeeded.", "Global", "Global", "msg", "TestOK", "Text");
                    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ConnectToDatabase_SQLite(bool bTesting = false, bool bUpdateDatabaseList = false)
        {
            MyGlobal.sDBMotherPID = lblPID.Text;
            MyGlobal.sDataSource = cboDataSource.Text;
            MyGlobal.sDBConnectionTitle = "(" + cboDataSource.Text + ") (" + txtConnectionName_SQLite.Text + ") " + "on " + Path.GetFileName(txtFile_SQLite.Text);
            MyGlobal.sDBConnectionName = txtConnectionName_SQLite.Text;
            MyGlobal.sTabBackColor = pnlBackColor.Tag.ToString();
            MyGlobal.sTabActiveForeColor = pnlActiveForeColor.Tag.ToString();
            MyGlobal.sTabInactiveForeColor = pnlInactiveForeColor.Tag.ToString();
            MyGlobal.sDBConnectionServer = "";
            MyGlobal.sSupportInfo = txtSupportInfo.Text;
            MyGlobal.sDBConnectionDatabase = "";
            MyGlobal.bDBPooling = false;

            var builder = new System.Data.SQLite.SQLiteConnectionStringBuilder
            {
                DataSource = txtFile_SQLite.Text,
                FailIfMissing = false,
                //Password = txtPassword_SQLite.Text, //不在此處指定密碼
                Pooling = false,
                Version = 3
            };

            MyGlobal.sDBConnectionString = builder.ConnectionString;

            if (bTesting == false)
            {
                Close();
            }
            else
            {
                var sConnectTo = ConnectToDatabase2(bUpdateDatabaseList); //SQLite

                if (!string.IsNullOrEmpty(sConnectTo))
                {
                    _sLangText = MyGlobal.GetLanguageString("Test connection failed", "Global", "Global", "msg", "TestNG", "Text");
                    var sLangText = "";

                    if (bUpdateDatabaseList == false)
                    {
                        sLangText = _sLangText + "\r\n\r\n";
                    }

                    _sLangText = MyGlobal.GetLanguageString("Error connecting to the SQLite file:", "Global", "Global", "msg", "ErrorConnectingToSQLiteFile", "Text");
                    sLangText += _sLangText + "\r\n\r\n";
                    sLangText += txtFile_SQLite.Text + "\r\n\r\n";

                    if (sConnectTo == "file is not a database\r\nfile is not a database")
                    {
                        _sLangText = MyGlobal.GetLanguageString("Wrong password or not a database file!", "form", Name, "object", "WrongPasswordOrNotDBFile", "Text");
                        sLangText += _sLangText;
                    }
                    else
                    {
                        sLangText += sConnectTo;
                    }

                    MessageBox.Show(sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (bUpdateDatabaseList)
                    {
                        return;
                    }

                    _sLangText = MyGlobal.GetLanguageString("Test connection succeeded.", "Global", "Global", "msg", "TestOK", "Text");
                    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ConnectToDatabase_SQLCipher(bool bTesting = false, bool bUpdateDatabaseList = false)
        {
            MyGlobal.sDBMotherPID = lblPID.Text;
            MyGlobal.sDataSource = "SQLCipher";
            MyGlobal.sDBConnectionTitle = "(" + cboDataSource.Text + ") (" + txtConnectionName_SQLite.Text + ") " + "on " + Path.GetFileName(txtFile_SQLite.Text);
            MyGlobal.sDBConnectionName = txtConnectionName_SQLite.Text;
            MyGlobal.sTabBackColor = pnlBackColor.Tag.ToString();
            MyGlobal.sTabActiveForeColor = pnlActiveForeColor.Tag.ToString();
            MyGlobal.sTabInactiveForeColor = pnlInactiveForeColor.Tag.ToString();
            MyGlobal.sDBConnectionServer = "";
            MyGlobal.sSupportInfo = txtSupportInfo.Text;
            MyGlobal.sDBConnectionDatabase = "";
            MyGlobal.bDBPooling = false;

            var builder = new SQLiteConnectionStringBuilder
            {
                DataSource = txtFile_SQLite.Text,
                Encryption = EncryptionMode.SQLCipher, //EncryptionLicenseKey = sEncryptionLicenseKey
                FailIfMissing = false,
                Password = rdoWithoutPassword.Checked ? "" : txtPassword_SQLite.Text,
                Pooling = false,
                WritableSchema = true
            };

            MyGlobal.sDBConnectionString = builder.ConnectionString;

            if (bTesting == false)
            {
                Close();
            }
            else
            {
                var sConnectTo = ConnectToDatabase2(bUpdateDatabaseList); //SQLite

                if (!string.IsNullOrEmpty(sConnectTo))
                {
                    _sLangText = MyGlobal.GetLanguageString("Test connection failed", "Global", "Global", "msg", "TestNG", "Text");
                    var sLangText = "";

                    if (bUpdateDatabaseList == false)
                    {
                        sLangText = _sLangText + "\r\n\r\n";
                    }

                    _sLangText = MyGlobal.GetLanguageString("Error connecting to the SQLite file:", "Global", "Global", "msg", "ErrorConnectingToSQLiteFile", "Text");
                    sLangText += _sLangText + "\r\n\r\n";
                    sLangText += txtFile_SQLite.Text + "\r\n\r\n";

                    if (sConnectTo == "File opened that is not a database file\r\nfile is not a database")
                    {
                        _sLangText = MyGlobal.GetLanguageString("Wrong password or not a database file!", "form", Name, "object", "WrongPasswordOrNotDBFile", "Text");
                        sLangText += _sLangText;
                    }
                    else
                    {
                        sLangText += sConnectTo;
                    }

                    MessageBox.Show(sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (bUpdateDatabaseList)
                    {
                        return;
                    }

                    _sLangText = MyGlobal.GetLanguageString("Test connection succeeded.", "Global", "Global", "msg", "TestOK", "Text");
                    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private string ConnectToDatabase2(bool bUpdateDatabaseList = false)
        {
            var sResult = "";

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    sResult = MyGlobal.oOracleReader.ConnectTo();

                    if (string.IsNullOrEmpty(sResult))
                    {
                        MyGlobal.oOracleReader.oDisconnect();
                    }

                    break;
                case "PostgreSQL":
                    sResult = MyGlobal.oPostgreReader.ConnectTo(MyGlobal.sDBConnectionString);

                    if (string.IsNullOrEmpty(sResult))
                    {
                        if (bUpdateDatabaseList)
                        {
                            const string sSql = "SELECT datname AS Name FROM pg_database WHERE datistemplate = false ORDER BY datname";
                            var dt = MyGlobal.oPostgreReader.ExecuteQueryToDataTable(sSql);

                            if (dt != null)
                            {
                                _dtDatabaseListInfo_PostgreSQL = dt.Copy();
                                UpdateComboBox(dt, cboDatabase_PostgreSQL);
                            }
                            else
                            {
                                cboDatabase_PostgreSQL.Items.Clear();
                                _dtDatabaseListInfo_PostgreSQL = null;
                            }
                        }

                        MyGlobal.oPostgreReader.oDisconnect();
                    }

                    break;
                case "SQL Server":
                    sResult = MyGlobal.oSQLServerReader.ConnectTo(MyGlobal.sDBConnectionString);

                    if (string.IsNullOrEmpty(sResult))
                    {
                        if (bUpdateDatabaseList)
                        {
                            const string sSql = "SELECT Name FROM master.sys.databases ORDER BY Name";
                            var dt = MyGlobal.oSQLServerReader.ExecuteQueryToDataTable(sSql, false);

                            if (dt != null)
                            {
                                //20220819
                                _dtDatabaseListInfo_SQLServer = dt.Copy();
                                UpdateComboBox(dt, cboDatabase_SQLServer);
                            }
                            else
                            {
                                cboDatabase_SQLServer.Items.Clear();
                                _dtDatabaseListInfo_SQLServer = null;
                            }
                        }

                        MyGlobal.oSQLServerReader.oDisconnect();
                    }

                    break;
                case "MySQL":
                    sResult = MyGlobal.oMySQLReader.ConnectTo(MyGlobal.sDBConnectionString);

                    if (string.IsNullOrEmpty(sResult))
                    {
                        if (bUpdateDatabaseList)
                        {
                            const string sSql = "SELECT Schema_Name AS Name FROM information_schema.schemata ORDER BY Schema_Name";
                            var dt = MyGlobal.oMySQLReader.ExecuteQueryToDataTable(sSql, false);

                            if (dt != null)
                            {
                                _dtDatabaseListInfo_MySQL = dt.Copy();
                                UpdateComboBox(dt, cboDatabase_MySQL);
                            }
                            else
                            {
                                cboDatabase_MySQL.Items.Clear();
                                _dtDatabaseListInfo_MySQL = null;
                            }
                        }

                        MyGlobal.oMySQLReader.oDisconnect();
                    }

                    break;
                case "SQLite":
                    sResult = MyGlobal.oSQLiteReader.ConnectTo(MyGlobal.sDBConnectionString, "", txtPassword_SQLite.Text);

                    if (string.IsNullOrEmpty(sResult))
                    {
                        MyGlobal.oSQLiteReader.oDisconnect();
                    }

                    break;
                case "SQLCipher":
                    sResult = MyGlobal.oSQLCipherReader.ConnectTo(MyGlobal.sDBConnectionString);

                    if (string.IsNullOrEmpty(sResult))
                    {
                        MyGlobal.oSQLCipherReader.oDisconnect();
                    }

                    break;
            }

            return sResult;
        }

        private void UpdateComboBox(DataTable dt, C1ComboBox cbo)
        {
            cbo.Items.Clear();

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                cbo.Items.Add(dt.Rows[i]["Name"].ToString());
            }
        }

        private void btnHelp_ConnectionName_Click(object sender, EventArgs e)
        {
            _sLangText = MyGlobal.GetLanguageString("It's an alias or nickname for easy identification.", "form", Name, "msg", "Help_ConnectionName1", "Text");
            _sLangText += "\r\n\r\n" + MyGlobal.GetLanguageString("If necessary, you can rename it next time you connect.", "form", Name, "msg", "Help_ConnectionName2", "Text");

            GetXY4MessageBox(out int iX, out int iY);
            FindAndMoveMsgBox(Cursor.Position.X - iX, Cursor.Position.Y + iY, true, "JasonQuery");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHelp_DirectMode_Click(object sender, EventArgs e)
        {
            _sLangText = MyGlobal.GetLanguageString("In the Direct mode, JasonQuery connects to an Oracle server directly, with third party libraries - Devart.Data.Oracle.", "form", Name, "msg", "DirectMode1", "Text");
            var sMsg = _sLangText + "\r\n\r\n";
            _sLangText = MyGlobal.GetLanguageString("It allows JasonQuery to work with Oracle directly through TCP/IP protocol without involving the Oracle Client software.", "form", Name, "msg", "DirectMode2", "Text");
            sMsg += _sLangText + "\r\n\r\n";
            _sLangText = MyGlobal.GetLanguageString("You only need to have operating system with TCP/IP protocol support.", "form", Name, "msg", "DirectMode3", "Text");

            GetXY4MessageBox(out int iX, out int iY);
            FindAndMoveMsgBox(Cursor.Position.X - iX, Cursor.Position.Y + iY, true, "JasonQuery");
            MessageBox.Show(sMsg + _sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHelp_SID_Click(object sender, EventArgs e)
        {
            _sLangText = MyGlobal.GetLanguageString("You can get the SID using one of the following SQL statements.", "form", Name, "msg", "InfoGetSID", "Text");

            GetXY4MessageBox(out int iX, out int iY);
            FindAndMoveMsgBox(Cursor.Position.X - iX, Cursor.Position.Y + iY, true, "JasonQuery");
            MessageBox.Show(_sLangText + "\r\n\r\nSELECT Instance_Name AS \"SID\" FROM v$instance;\r\n\r\nSELECT SYS_CONTEXT('userenv', 'instance_name') AS \"SID\" FROM DUAL;", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHelp_Unicode_Click(object sender, EventArgs e)
        {
            _sLangText = MyGlobal.GetLanguageString("If you get the error message below when you execute SQL statement:", "form", Name, "msg", "InfoEnableUnicode1", "Text");
            var sMsg = _sLangText + "\r\n\r\n";
            sMsg += "character with byte sequence 0xe6 0xb8 0xa9 in encoding \"UTF8\" has no equivalent in encoding \"BIG5\"\r\n\r\n";
            _sLangText = MyGlobal.GetLanguageString("You can enable Unicode function and try again.", "form", Name, "msg", "InfoEnableUnicode2", "Text");
            sMsg += _sLangText;

            GetXY4MessageBox(out int iX, out int iY);
            FindAndMoveMsgBox(Cursor.Position.X - iX, Cursor.Position.Y + iY, true, "JasonQuery");
            MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHelp_AutoRollback_Click(object sender, EventArgs e)
        {
            var sMsg = "";
            _sLangText = MyGlobal.GetLanguageString("This feature is enabled by default.", "form", Name, "msg", "AutoRollback0", "Text");
            sMsg = _sLangText + "\r\n\r\n";
            _sLangText = MyGlobal.GetLanguageString("If it is disabled, by executing that wrong SQL statement, postgreSQL will not terminate the execution by itself.", "form", Name, "msg", "AutoRollback1", "Text");
            sMsg += _sLangText + "\r\n";
            _sLangText = MyGlobal.GetLanguageString("At this point, you must manually execute the \"rollback\" command before you can continue to execute other SQL statements.", "form", Name, "msg", "AutoRollback2", "Text");
            sMsg += _sLangText;

            GetXY4MessageBox(out int iX, out int iY);
            FindAndMoveMsgBox(Cursor.Position.X - iX, Cursor.Position.Y + iY, true, "JasonQuery");
            MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SelectedTabClick(object sender, EventArgs e)
        {
            const int iX = 23;
            const int iY = 265;

            var pnlSelected = sender as Panel;
            _sPanelColorSelectedName = pnlSelected.Name;
            var pt = PointToScreen(pnlSelected.Location);

            pt = new Point(pt.X + iX, pt.Y + pnlSelected.Height + iY);  //因為在 GroupBox 內，所以要再微調 (X, Y)

            var f = new ThemeColorPickerWindow(pt, FormBorderStyle.FixedToolWindow, ThemeColorPickerWindow.Action.CloseWindow, ThemeColorPickerWindow.Action.CloseWindow)
            {
                FormBorderStyle = FormBorderStyle.None,
                ActionAfterColorSelected = ThemeColorPickerWindow.Action.CloseWindow
            };

            f.ColorSelected += f_ColorSelectedTab;
            f.Show();
        }

        private void f_ColorSelectedTab(object sender, ColorSelectedArg e)
        {
            foreach (var t in _lstPanelTabColor.Where(t => ((Panel)t).Name == _sPanelColorSelectedName))
            {
                ((Panel)t).BackColor = e.Color;
                ((Panel)t).Tag = e.HexColor;
                _toolTip1.SetToolTip(((Panel)t), e.HexColor + " " + "(R:" + e.R + ", G:" + e.G + ", B:" + e.B + ")");

                switch (((Panel)t).Name)
                {
                    case "pnlBackColor":
                        tabExample.BackColor = e.Color;
                        break;
                    case "pnlActiveForeColor":
                        tabExample.ForeColor = e.Color;
                        break;
                    case "pnlInactiveForeColor":
                        tabExample.TextInactiveColor = e.Color;
                        break;
                }

                break;
            }
        }

        private void chkDirectMode_CheckedChanged(object sender, EventArgs e)
        {
            lblSID_Oracle.Visible = chkDirectMode_Oracle.Checked;
            txtSID_Oracle.Visible = chkDirectMode_Oracle.Checked;
            btnHelp_SID_Oracle.Visible = chkDirectMode_Oracle.Checked;
            txtSID_Oracle.BackColor = chkDirectMode_Oracle.Checked ? _cEssentialField : _cOptionalField;
            txtSID_Oracle.Focus();
        }

        private void TransferValueToMainForm(string sValue)
        {
            var valueArgs = new ValueUpdatedEventArgs(sValue);
            ValueUpdated?.Invoke(this, valueArgs);
        }

        private void cboLocalization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyGlobal.sLocalization == cboLocalization.Text || !CheckLocalizationFileExist(true))
            {
                return;
            }

            MyGlobal.UpdateSetting("GlobalConfig", "Localization", cboLocalization.Text);

            MyGlobal.sLocalization = cboLocalization.Text;
            MyGlobal.LoadLocalizationXML(); //cboLocalization_SelectedIndexChanged
            ApplyLocalizationSetting(); //cboLocalization_SelectedIndexChanged
        }

        private bool CheckLocalizationFileExist(bool bShowMsg = false)
        {
            var bResult = true;
            var sFilename = Application.StartupPath + @"\localization\" + MyGlobal.GetValueFromDictionary(MyGlobal.dicLocalization, cboLocalization.Text);

            if (File.Exists(sFilename) == false)
            {
                bResult = false;
            }

            if (!bShowMsg || bResult)
            {
                return bResult;
            }

            _sLangText = MyGlobal.GetLanguageString("Localization file not found!", "Global", "Global", "msg", "LocalizationNotFound", "Text");
            MessageBox.Show(_sLangText + "\r\n\r\n" + sFilename, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return false;
        }

        private void btnHelp_DarkMode_Click(object sender, EventArgs e)
        {
            _sLangText = MyGlobal.GetLanguageString("You can change the color theme from [Tools] > [Options] > [General].", "form", Name, "msg", "ColorTheme", "Text");

            GetXY4MessageBox(out int iX, out int iY);
            FindAndMoveMsgBox(Cursor.Position.X - iX, Cursor.Position.Y + iY, true, "JasonQuery");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void c1GridDBInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var iRow = c1GridDBInfo.RowContaining(e.Y);

            if (iRow != -1)
            {
                c1GridDBInfo.Enabled = false;
                btnConnect.PerformClick();
            }
        }

        private void btnCreateShortcut_Click(object sender, EventArgs e)
        {
            CreateShortcut("JasonQuery.exe", "JasonQuery", Application.StartupPath);

            if (!File.Exists(_sDesktopPath + @"\JasonQuery.lnk"))
            {
                return;
            }

            btnCreateShortcut.Enabled = false;
            _sLangText = MyGlobal.GetLanguageString("A shortcut has been created on the desktop!", "form", Name, "msg", "Shortcut", "Text");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CreateShortcut(string sExeFilename, string sSystemName, string sTargetPath)
        {
            var wsh = new IWshRuntimeLibrary.WshShell();
            var shortcut = wsh.CreateShortcut(_sDesktopPath + @"\" + sSystemName + ".lnk") as IWshRuntimeLibrary.IWshShortcut;
            shortcut.Arguments = "";
            shortcut.TargetPath = sTargetPath + @"\" + sExeFilename;
            shortcut.WindowStyle = 1;
            shortcut.Description = sSystemName;
            shortcut.WorkingDirectory = sTargetPath;
            shortcut.IconLocation = sTargetPath + @"\" + sExeFilename;
            shortcut.Save();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!CheckData(false))
            {
                return;
            }

            switch (cboDataSource.Text)
            {
                case "Oracle":
                    CopyOracle();
                    _lstPicLogo[0].Visible = true;
                    txtSupportInfo.Text = _lstSupportInfo[0];
                    break;
                case "PostgreSQL":
                    CopyPostgreSQL();
                    _lstPicLogo[1].Visible = true;
                    txtSupportInfo.Text = _lstSupportInfo[1];
                    break;
                case "SQL Server":
                    CopySQLServer();
                    _lstPicLogo[2].Visible = true;
                    txtSupportInfo.Text = _lstSupportInfo[2];
                    break;
                case "MySQL/MariaDB":
                    CopyMySQL();
                    _lstPicLogo[3].Visible = true;
                    txtSupportInfo.Text = _lstSupportInfo[3];
                    break;
                case "SQLite":
                    CopySQLite();
                    _lstPicLogo[4].Visible = true;
                    txtSupportInfo.Text = _lstSupportInfo[4];
                    break;
            }
        }

        private void CopyOracle()
        {
            cboDataSource.Tag = cboDataSource.Text;
            txtConnectionName_Oracle.Tag = txtConnectionName_Oracle.Text + @"(Copied)";
            txtServer_Oracle.Tag = txtServer_Oracle.Text;
            //txtDatabase.Tag = txtDatabase.Text;
            txtPort_Oracle.Tag = txtPort_Oracle.Text;
            txtRemark_Oracle.Tag = txtRemark_Oracle.Text;
            txtUserID_Oracle.Tag = txtUserID_Oracle.Text;
            txtPassword_Oracle.Tag = txtPassword_Oracle.Text;
            cboConnectAs_Oracle.Tag = cboConnectAs_Oracle.Text;
            txtSID_Oracle.Tag = txtSID_Oracle.Text;
            chkDirectMode_Oracle.Tag = chkDirectMode_Oracle.Checked.ToString();
            chkSavePasswords.Tag = chkSavePasswords.Checked.ToString();

            btnNew.PerformClick();

            cboDataSource.Text = cboDataSource.Tag.ToString();
            txtConnectionName_Oracle.Text = txtConnectionName_Oracle.Tag.ToString();
            txtServer_Oracle.Text = txtServer_Oracle.Tag.ToString();
            //txtDatabase.Text = txtDatabase.Tag.ToString();
            txtPort_Oracle.Text = txtPort_Oracle.Tag.ToString();
            txtRemark_Oracle.Text = txtRemark_Oracle.Tag.ToString();
            txtUserID_Oracle.Text = txtUserID_Oracle.Tag.ToString();
            txtPassword_Oracle.Text = txtPassword_Oracle.Tag.ToString();
            cboConnectAs_Oracle.Text = cboConnectAs_Oracle.Tag.ToString();

            chkDirectMode_Oracle.Checked = chkDirectMode_Oracle.Tag.ToString() == "True";
            chkSavePasswords.Checked = chkSavePasswords.Tag.ToString() == "True";

            txtSID_Oracle.Text = (chkDirectMode_Oracle.Checked) ? txtSID_Oracle.Tag.ToString() : "";

            txtConnectionName_Oracle.Focus();
        }

        private void CopyPostgreSQL()
        {
            cboDataSource.Tag = cboDataSource.Text;
            txtConnectionName_PostgreSQL.Tag = txtConnectionName_PostgreSQL.Text + @"(Copied)";
            txtServer_PostgreSQL.Tag = txtServer_PostgreSQL.Text;
            cboDatabase_PostgreSQL.Tag = cboDatabase_PostgreSQL.Text;
            txtPort_PostgreSQL.Tag = txtPort_PostgreSQL.Text;
            txtRemark_PostgreSQL.Tag = txtRemark_PostgreSQL.Text;
            txtUserID_PostgreSQL.Tag = txtUserID_PostgreSQL.Text;
            txtPassword_PostgreSQL.Tag = txtPassword_PostgreSQL.Text;
            chkUnicode_PostgreSQL.Tag = chkUnicode_PostgreSQL.Checked.ToString();
            chkAutoRollback_PostgreSQL.Tag = chkAutoRollback_PostgreSQL.Checked.ToString();
            chkSavePasswords.Tag = chkSavePasswords.Checked.ToString();

            btnNew.PerformClick();

            cboDataSource.Text = cboDataSource.Tag.ToString();
            txtConnectionName_PostgreSQL.Text = txtConnectionName_PostgreSQL.Tag.ToString();
            txtServer_PostgreSQL.Text = txtServer_PostgreSQL.Tag.ToString();
            cboDatabase_PostgreSQL.Text = cboDatabase_PostgreSQL.Tag.ToString();
            txtPort_PostgreSQL.Text = txtPort_PostgreSQL.Tag.ToString();
            txtRemark_PostgreSQL.Text = txtRemark_PostgreSQL.Tag.ToString();
            txtUserID_PostgreSQL.Text = txtUserID_PostgreSQL.Tag.ToString();
            txtPassword_PostgreSQL.Text = txtPassword_PostgreSQL.Tag.ToString();
            chkUnicode_PostgreSQL.Checked = chkUnicode_PostgreSQL.Tag.ToString() == "True";
            chkAutoRollback_PostgreSQL.Checked = chkAutoRollback_PostgreSQL.Tag.ToString() == "True";
            chkSavePasswords.Checked = chkSavePasswords.Tag.ToString() == "True";

            txtConnectionName_PostgreSQL.Focus();
        }

        private void CopySQLServer()
        {
            cboDataSource.Tag = cboDataSource.Text;
            txtConnectionName_SQLServer.Tag = txtConnectionName_SQLServer.Text + @"(Copied)";
            txtServer_SQLServer.Tag = txtServer_SQLServer.Text;
            cboDatabase_SQLServer.Tag = cboDatabase_SQLServer.Text;
            txtRemark_SQLServer.Tag = txtRemark_SQLServer.Text;
            txtUserID_SQLServer.Tag = txtUserID_SQLServer.Text;
            txtPassword_SQLServer.Tag = txtPassword_SQLServer.Text;
            chkSavePasswords.Tag = chkSavePasswords.Checked.ToString();

            btnNew.PerformClick();

            cboDataSource.Text = cboDataSource.Tag.ToString();
            txtConnectionName_SQLServer.Text = txtConnectionName_SQLServer.Tag.ToString();
            txtServer_SQLServer.Text = txtServer_SQLServer.Tag.ToString();
            cboDatabase_SQLServer.Text = cboDatabase_SQLServer.Tag.ToString();
            txtRemark_SQLServer.Text = txtRemark_SQLServer.Tag.ToString();
            txtUserID_SQLServer.Text = txtUserID_SQLServer.Tag.ToString();
            txtPassword_SQLServer.Text = txtPassword_SQLServer.Tag.ToString();
            chkSavePasswords.Checked = chkSavePasswords.Tag.ToString() == "True";

            txtConnectionName_SQLServer.Focus();
        }

        private void CopyMySQL()
        {
            cboDataSource.Tag = cboDataSource.Text;
            txtConnectionName_MySQL.Tag = txtConnectionName_MySQL.Text + @"(Copied)";
            txtServer_MySQL.Tag = txtServer_MySQL.Text;
            cboDatabase_MySQL.Tag = cboDatabase_MySQL.Text;
            txtRemark_MySQL.Tag = txtRemark_MySQL.Text;
            txtUserID_MySQL.Tag = txtUserID_MySQL.Text;
            txtPassword_MySQL.Tag = txtPassword_MySQL.Text;
            chkSavePasswords.Tag = chkSavePasswords.Checked.ToString();

            btnNew.PerformClick();

            cboDataSource.Text = cboDataSource.Tag.ToString();
            txtConnectionName_MySQL.Text = txtConnectionName_MySQL.Tag.ToString();
            txtServer_MySQL.Text = txtServer_MySQL.Tag.ToString();
            cboDatabase_MySQL.Text = cboDatabase_MySQL.Tag.ToString();
            txtRemark_MySQL.Text = txtRemark_MySQL.Tag.ToString();
            txtUserID_MySQL.Text = txtUserID_MySQL.Tag.ToString();
            txtPassword_MySQL.Text = txtPassword_MySQL.Tag.ToString();
            chkSavePasswords.Checked = chkSavePasswords.Tag.ToString() == "True";

            txtConnectionName_MySQL.Focus();
        }

        private void CopySQLite()
        {
            cboDataSource.Tag = cboDataSource.Text;
            txtConnectionName_SQLite.Tag = txtConnectionName_SQLite.Text + @"(Copied)";
            txtFile_SQLite.Tag = txtFile_SQLite.Text;
            cboDatabaseType_SQLite.Tag = cboDatabaseType_SQLite.Text;
            txtRemark_SQLite.Tag = txtRemark_SQLite.Text;
            txtPassword_SQLite.Tag = txtPassword_SQLite.Text;
            chkSavePasswords.Tag = chkSavePasswords.Checked.ToString();

            btnNew.PerformClick();

            cboDataSource.Text = cboDataSource.Tag.ToString();
            txtConnectionName_SQLite.Text = txtConnectionName_SQLite.Tag.ToString();
            txtFile_SQLite.Text = txtFile_SQLite.Tag.ToString();
            cboDatabaseType_SQLite.Text = cboDatabaseType_SQLite.Tag.ToString();
            txtRemark_SQLite.Text = txtRemark_SQLite.Tag.ToString();
            txtPassword_SQLite.Text = txtPassword_SQLite.Tag.ToString();
            chkSavePasswords.Checked = chkSavePasswords.Tag.ToString() == "True";

            txtConnectionName_SQLite.Focus();
        }

        private void btnHelp_Password_Click(object sender, EventArgs e)
        {
            _sLangText = MyGlobal.GetLanguageString("Password will be encrypted with your domain username.", "form", Name, "msg", "PasswordEncrypt", "Text");
            var sMsg = _sLangText + "\r\n\r\n";
            _sLangText = MyGlobal.GetLanguageString("If you do not want your password to be stored in \"JasonQuery.db\", please uncheck \"Save Passwords\".", "form", Name, "msg", "SavePassword", "Text");
            sMsg += _sLangText;

            GetXY4MessageBox(out int iX, out int iY);
            FindAndMoveMsgBox(Cursor.Position.X - iX, Cursor.Position.Y + iY, true, "JasonQuery");
            MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHelp_Pooling_Click(object sender, EventArgs e)
        {
            _sLangText = "";
            _sLangText = MyGlobal.GetLanguageString("Connection pooling enables JasonQuery to use a connection from a pool of connections that do not need to be reestablished for each use. Once a connection has been created and placed in a pool, JasonQuery can reuse that connection without performing the complete connection process.", "form", Name, "msg", "Pooling", "Text") + "\r\n\r\n";

            var sTemp1 = MyGlobal.GetLanguageString("If it is enabled, when you disconnect the database connection, the actual connection is not closed in order to be used later by JasonQuery. This boosts performance greatly.", "form", Name, "msg", "Pooling1", "Text");
            var sTemp2 = MyGlobal.GetLanguageString("If it is disabled, when you disconnect the database connection, the database connection will not be seen from the server when the connection is disconnected.", "form", Name, "msg", "Pooling2", "Text");
            var sTemp3 = MyGlobal.GetLanguageString("When you execute any SQL statement again, JasonQuery will automatically connect to the database.", "form", "frmOptions", "msg", "Help_Reconnect", "Text");

            GetXY4MessageBox(out int iX, out int iY);
            FindAndMoveMsgBox(Cursor.Position.X - iX, Cursor.Position.Y + iY, true, "JasonQuery");
            MessageBox.Show(_sLangText + sTemp1 + "\r\n\r\n" + sTemp2 + "\r\n\r\n" + sTemp3, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void c1GridDBInfo_FetchCellStyle(object sender, FetchCellStyleEventArgs e)
        {
            var sColor = c1GridDBInfo.Columns[(int)eMenu.eTabBackColor].CellText(e.Row);
            e.CellStyle.ForeColor = ColorTranslator.FromHtml(sColor);
            e.CellStyle.BackColor = ColorTranslator.FromHtml(sColor);
        }

        private void btnHelp_ExcludeNativeDatabase_SQLServer_Click(object sender, EventArgs e)
        {
            _sLangText = MyGlobal.GetLanguageString("If it is enabled, JasonQuery will exclude native object shipped or created by SQL Server.", "form", Name, "msg", "Help_ExcludeNativeObject", "Text");

            GetXY4MessageBox(out int iX, out int iY);
            FindAndMoveMsgBox(Cursor.Position.X - iX, Cursor.Position.Y + iY, true, "JasonQuery");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHelp_ShowColumnInfo_Click(object sender, EventArgs e)
        {
            _sLangText = MyGlobal.GetLanguageString("Show column information in the Query Editor's Schema Browser.", "Global", "Global", "msg", "InfoShowColumnInfo1", "Text") + "\r\n\r\n";
            _sLangText += MyGlobal.GetLanguageString("If it is enabled, it may take a long time to get all the column information.", "Global", "Global", "msg", "InfoShowColumnInfo2", "Text") + "\r\n\r\n";
            _sLangText += MyGlobal.GetLanguageString("You can change the setting from [Tools] > [Options] > [Query Editor].", "Global", "Global", "msg", "InfoShowColumnInfo3", "Text");

            GetXY4MessageBox(out int iX, out int iY);
            FindAndMoveMsgBox(Cursor.Position.X - iX, Cursor.Position.Y + iY, true, "JasonQuery");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHelp_Unicode_MySQL_Click(object sender, EventArgs e)
        {
            _sLangText = "If it is enabled, it may set MySQL client charset to UTF8 and converts client data according to this charset.\r\n";
            _sLangText += "MySQL server version 4.1 or higher is required to use this option.";

            GetXY4MessageBox(out int iX, out int iY);
            FindAndMoveMsgBox(Cursor.Position.X - iX, Cursor.Position.Y + iY, true, "JasonQuery");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ApplyLocalizationSetting()
        {
            var iConnectDataRowCount = 0;

            MyGlobal.ApplyLanguageInfo(this); //ApplyLocalizationSetting

            _sLangText = MyGlobal.GetLanguageString("Create a desktop shortcut", "form", Name, "Object", "btnCreateShortcut", "ToolTipText");
            _toolTip1.SetToolTip(btnCreateShortcut, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Export password encrypted all connection information", "form", Name, "Object", "btnExport", "ToolTipText");
            _toolTip1.SetToolTip(btnExport, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Import all connection information encrypted with password", "form", Name, "Object", "btnImport", "ToolTipText");
            _toolTip1.SetToolTip(btnImport, _sLangText);

            _sLangText = MyGlobal.GetLanguageString("Browse existing database file on local computer", "form", Name, "Object", "btnBrowse", "ToolTipText");
            _toolTip1.SetToolTip(btnBrowseFile_SQLite, _sLangText);

            _sLangText = MyGlobal.GetLanguageString("JasonQuery for Oracle supports Oracle servers 21c, 19c, 18c, 12c, 11g, 10g, 9i, 8i, 8.0 and 7.3, including Personal and Express editions. JasonQuery for Oracle supports both x86 and x64 versions of the following Oracle clients: 21c, 19c, 18c, 12c, 11g, 10g, 9i, 8i and 8.0. JasonQuery for Oracle also supports Oracle TimesTen 11g Release 1 and Oracle TimesTen 11g Release 2.", "form", Name, "msg", "OracleSupportInfo", "Text");
            _lstSupportInfo.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("JasonQuery for PostgreSQL supports PostgreSQL server since 7.1 version to 14, EnterpriseDB/EnterpriseDB Advanced Server/Postgres Plus Advanced Server/EDB Postgres Advanced Server, Pervasive Postgres SQL servers, Heroku Postgres.", "form", Name, "msg", "PostgreSQLSupportInfo", "Text");
            _lstSupportInfo.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("JasonQuery for SQL Server supports SQL Azure, SQL Server 2019, 2017, 2016, 2014, 2012, 2008, 2005 (including Express editions), SQL Server 2000 and MSDE.", "form", Name, "msg", "SQLServerSupportInfo", "Text");
            _lstSupportInfo.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("JasonQuery for MySQL supports MySQL server versions 8.0, 5.7, 5.6, 5.5, 5.4, 5.1, 5.0, 4.1, 4.0 and 3.23 including Embedded servers (starting with 4.1), 64-bit MySQL servers, Percona, and MariaDB. JasonQuery for MySQL supports Amazon RDS for MySQL, Amazon RDS for MariaDB, and Amazon RDS for Aurora.", "form", Name, "msg", "MySQLSupportInfo", "Text");
            _lstSupportInfo.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("JasonQuery for SQLite supports SQLite engine version 3 and higher.\r\nSQLCipher (Community) is an Open Source SQLite extension that provides transparent 256-bit AES full database encryption.\r\nSystem.Data.SQLite RC4 is an ADO.NET provider for SQLite. It provides a 128 bit RC4 encryption.", "form", Name, "msg", "SQLiteSupportInfo", "Text");
            _lstSupportInfo.Add(_sLangText);

            chkPooling_SQLServer.Location = new Point(btnHelp_Pooling_SQLServer.Left - chkPooling_SQLServer.Width, chkPooling_SQLServer.Top);
            chkExcludeNativeObject_SQLServer.Location = new Point(btnHelp_ExcludeNativeDatabase_SQLServer.Left - chkExcludeNativeObject_SQLServer.Width, chkExcludeNativeObject_SQLServer.Top);

            chkPooling_PostgreSQL.Location = new Point(btnHelp_Pooling_PostgreSQL.Left - chkPooling_PostgreSQL.Width, chkPooling_PostgreSQL.Top);
            chkUnicode_PostgreSQL.Location = new Point(btnHelp_Unicode_PostgreSQL.Left - chkUnicode_PostgreSQL.Width, chkUnicode_PostgreSQL.Top);
            chkAutoRollback_PostgreSQL.Location = new Point(btnHelp_AutoRollback_PostgreSQL.Left - chkAutoRollback_PostgreSQL.Width, chkAutoRollback_PostgreSQL.Top);

            chkPooling_MySQL.Location = new Point(btnHelp_Pooling_MySQL.Left - chkPooling_MySQL.Width, chkPooling_MySQL.Top);
            chkUnicode_MySQL.Location = new Point(btnHelp_Unicode_MySQL.Left - chkUnicode_MySQL.Width, chkUnicode_MySQL.Top);

            chkUnicode_Oracle.Location = new Point(btnHelp_DirectMode.Left - chkUnicode_Oracle.Width, chkUnicode_Oracle.Top);
            chkDirectMode_Oracle.Location = new Point(btnHelp_DirectMode.Left - chkDirectMode_Oracle.Width, chkDirectMode_Oracle.Top);
            chkPooling_Oracle.Location = new Point(btnHelp_Pooling_Oracle.Left - chkPooling_Oracle.Width, chkPooling_Oracle.Top);

            btnHelp_Password.Location = new Point(chkSavePasswords.Left + chkSavePasswords.Width - 3, btnHelp_Password.Top);
            btnHelp_DarkMode.Location = new Point(btnConnect.Left + btnConnect.Width - btnHelp_DarkMode.Width, btnHelp_DarkMode.Top);
            chkDarkMode.Location = new Point(btnHelp_DarkMode.Left - btnHelp_DarkMode.Width - chkDarkMode.Width + 23, chkDarkMode.Top);
            btnHelp_ShowColumnInfo.Location = new Point(chkShowColumnInfo.Left + chkShowColumnInfo.Width - 3, btnHelp_ShowColumnInfo.Top);

            c1GridDBInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;

            //取得 DBInfo 資料
            iConnectDataRowCount = CreateAndGetDbInfoTable(); //ApplyLocalizationSetting

            if (iConnectDataRowCount == 0)
            {
                //cboConnectAs.SelectedIndex = 0;
            }
            else
            {
                //指定哪一個 Column 要套用 FetchCellStyle
                c1GridDBInfo.Splits[0].DisplayColumns[_lstGridHeader[(int)eMenu.eTabBackColor]].FetchStyle = true;
            }

            pnlBackColor.Location = new Point(lblBackColor.Left + lblBackColor.Width + 3, pnlBackColor.Top);
            pnlActiveForeColor.Location = new Point(lblActiveForeColor.Left + lblActiveForeColor.Width + 3, pnlActiveForeColor.Top);
            pnlInactiveForeColor.Location = new Point(lblInactiveForeColor.Left + lblInactiveForeColor.Width + 3, pnlInactiveForeColor.Top);

            Refresh();
        }

        private void txtPort_SQLServer_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPort_SQLServer.Text) || txtPort_SQLServer.Text == "0")
            {
                txtPort_SQLServer.Text = @"1433";
            }
        }

        private void txtPort_MySQL_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPort_MySQL.Text) || txtPort_MySQL.Text == "0")
            {
                txtPort_MySQL.Text = "3306";
            }
        }

        private void TextBox_Numeric_KeyPress(object sender, KeyPressEventArgs e)
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

        private string GetUNCPath(string sPath)
        {
            var bResult = true;
            var root = Path.GetPathRoot(sPath).Substring(0, 2);
            var allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (root != d.ToString().Substring(0, 2))
                {
                    continue;
                }

                if (d.DriveType.ToString() == "Fixed")
                {
                    bResult = false;
                }
            }

            if (!bResult) return sPath;

            var sb = new StringBuilder(512);
            var size = sb.Capacity;

            if (sPath.Length <= 2 || sPath[1] != ':') return sPath;

            var c = sPath[0];

            if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z')) return sPath;

            var error = WNetGetConnection(sPath.Substring(0, 2), sb, ref size);

            if (error != 0) return sPath;

            var path = Path.GetFullPath(sPath).Substring(Path.GetPathRoot(sPath).Length);

            return Path.Combine(sb.ToString().TrimEnd(), path);

        }

        private void btnBrowseFile_SQLite_Click(object sender, EventArgs e)
        {
            string sFilename;

            if (rdoOpen_SQLite.Checked)
            {
                var of = new OpenFileDialog { Multiselect = false };

                _sLangText = MyGlobal.GetLanguageString("Open an Existing SQLite File...", "form", Name, "msg", "OpenExistingSQLiteDatabaseFile", "Text");
                of.Title = _sLangText;
                //of.InitialDirectory = "C:\\Users\\Administrators\\Desktop";
                of.Filter = @"SQLite database files (*.db;*.sqlite;*.sqlite3;*.db3;*.s3db)|*.db;*.sqlite;*.sqltie3;*.db3;*.s3db|All files (*.*)|*.*";

                if (of.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                sFilename = of.FileName;
                txtFile_SQLite.ForeColor = SystemColors.WindowText;

                sFilename = GetUNCPath(sFilename);

                //嘗試以 SQLCipher 模式開啟，判斷是否有密碼
                var bResult = TestSQLCipherConnect(sFilename);

                if (bResult == false)
                {
                    bResult = TestSQLiteConnect(sFilename);
                }

                rdoWithPassword.Visible = false;
                rdoWithoutPassword.Visible = false;
                btnRemovePassword.Visible = false;
                btnEncryptWithPassword.Visible = false;

                //調整位置
                if (bResult) //無加密！
                {
                    rdoWithoutPassword.Checked = true;
                    rdoWithoutPassword.Visible = true;
                    btnEncryptWithPassword.Visible = true;

                    rdoWithoutPassword.Location = new Point(rdoWithPassword.Left, rdoWithoutPassword.Top);
                    btnEncryptWithPassword.Location = new Point(rdoWithoutPassword.Left + rdoWithoutPassword.Width + 15, btnEncryptWithPassword.Top);
                }
                else //有加密！
                {
                    rdoWithPassword.Checked = true;
                    rdoWithPassword.Visible = true;
                    btnRemovePassword.Visible = true;

                    btnRemovePassword.Location = new Point(rdoWithPassword.Left + rdoWithPassword.Width + 15, btnRemovePassword.Top);
                }
            }
            else
            {
                var of = new SaveFileDialog();

                _sLangText = MyGlobal.GetLanguageString("Save as a New SQLite File...", "form", Name, "msg", "SaveAsNewDatabaseFile", "Text");
                of.Title = _sLangText;
                //of.InitialDirectory = "C:\\Users\\Administrators\\Desktop";
                of.Filter = @"SQLite database files (*.db;*.sqlite;*.sqlite3;*.db3;*.s3db)|*.db;*.sqlite;*.sqltie3;*.db3;*.s3db|All files (*.*)|*.*";

                if (of.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                rdoWithPassword.Visible = false;
                rdoWithoutPassword.Visible = false;
                btnRemovePassword.Visible = false;
                btnEncryptWithPassword.Visible = false;

                rdoWithPassword.Checked = true;
                rdoWithPassword.Visible = true;
                rdoWithoutPassword.Visible = true;

                sFilename = of.FileName;
            }

            txtFile_SQLite.Text = sFilename;
        }

        private void Create_Or_New_CheckedChanged(object sender, EventArgs e)
        {
            ChangeSQLitePasswordState();
        }

        private void ChangeSQLitePasswordState()
        {
            rdoWithPassword.Visible = false;
            rdoWithoutPassword.Visible = false;
            btnRemovePassword.Visible = false;
            btnEncryptWithPassword.Visible = false;
            btnTest.Enabled = rdoOpen_SQLite.Checked;
            rdoCreate_SQLite.Enabled = true;
            rdoWithPassword.Checked = true;

            if (rdoOpen_SQLite.Checked && !string.IsNullOrEmpty(txtFile_SQLite.Text) && File.Exists(txtFile_SQLite.Text))
            {
                //嘗試以 SQLCipher 模式開啟，判斷是否有密碼
                var bResult = TestSQLCipherConnect(txtFile_SQLite.Text);

                if (bResult == false)
                {
                    bResult = TestSQLiteConnect(txtFile_SQLite.Text);
                }

                //調整位置
                if (bResult) //無加密！
                {
                    rdoWithoutPassword.Visible = true;
                    btnEncryptWithPassword.Visible = true;

                    rdoWithoutPassword.Location = new Point(rdoWithPassword.Left, rdoWithoutPassword.Top);
                    btnEncryptWithPassword.Location = new Point(rdoWithoutPassword.Left + rdoWithoutPassword.Width + 15, btnEncryptWithPassword.Top);
                }
                else //有加密！
                {
                    rdoWithPassword.Visible = true;
                    btnRemovePassword.Visible = true;

                    btnRemovePassword.Location = new Point(rdoWithPassword.Left + rdoWithPassword.Width + 15, btnRemovePassword.Top);
                }
            }
            else
            {
                rdoWithPassword.Visible = true;
                rdoWithoutPassword.Visible = true;

                //調整位置
                rdoWithoutPassword.Location = new Point(rdoWithPassword.Left + rdoWithPassword.Width + 15, rdoWithoutPassword.Top);
            }
        }

        private bool TestSQLCipherConnect(string sFilename)
        {
            var bResult = false;
            var oCommand = new SQLiteCommand();
            var conn = new SQLiteConnection();

            try
            {
                //var sFilename = Application.StartupPath + "\\0413SQLCipherTest20220604.db";
                //var sPassword = "12311204"; //密碼包含「單引號」或「雙引號」都會引發錯誤

                var builder = new SQLiteConnectionStringBuilder
                {
                    DataSource = sFilename,
                    Password = "",
                    Encryption = EncryptionMode.SQLCipher,
                    FailIfMissing = false,
                    Pooling = false,
                    Version = 3
                };

                conn = new SQLiteConnection(builder.ConnectionString);
                conn.Open();

                bResult = true;
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                conn.Close();
            }

            return bResult;
        }

        private bool TestSQLiteConnect(string sFilename)
        {
            var bResult = false;
            var oCommand = new SQLiteCommand();
            var conn = new SQLiteConnection();

            try
            {
                //var sFilename = Application.StartupPath + "\\0413SQLCipherTest20220604.db";
                //var sPassword = "12311204"; //密碼包含「單引號」或「雙引號」都會引發錯誤

                var builder = new SQLiteConnectionStringBuilder
                {
                    DataSource = sFilename,
                    Password = "",
                    Encryption = EncryptionMode.SQLCipher,
                    FailIfMissing = false,
                    Pooling = false,
                    Version = 3
                };

                conn = new SQLiteConnection(builder.ConnectionString);
                conn.Open();

                bResult = true;
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                conn.Close();
            }

            return bResult;
        }

        private void ExtractSQLiteNewDBFile(string sType, string sFullFilename)
        {
            var sFilename = sType == "SQLite3" ? "newSQLite3.db" : "newSQLCipher.db";
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("JasonQuery.Files." + sFilename);

            try
            {
                File.Delete(sFullFilename);

                var fileStream = new FileStream(sFullFilename, FileMode.CreateNew);

                for (var i = 0; i < stream.Length; i++)
                {
                    fileStream.WriteByte((byte)stream.ReadByte());
                }

                fileStream.Close();
            }
            catch (Exception)
            {
                //
            }
        }

        private void btnRemoveOrEncrypt_Click(object sender, EventArgs e)
        {
            var btn = sender as C1Button;
            var sMode = btn.Name;

            sMode = sMode.IndexOf("RemovePassword", StringComparison.Ordinal) != -1 ? "REMOVE" : "ENCRYPT";

            if (!File.Exists(txtFile_SQLite.Text))
            {
                txtFile_SQLite.ForeColor = Color.Red;
                _sLangText = MyGlobal.GetLanguageString("SQLite file not found!", "Global", "Global", "msg", "SQLiteFileNotFound", "Text");
                MessageBox.Show(_sLangText + "\r\n\r\n" + txtFile_SQLite.Text, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnBrowseFile_SQLite.Focus();
            }
            else
            {
                if (!CheckData(false))
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword_SQLite.Text))
                {
                    _sLangText = MyGlobal.GetLanguageString("Please enter password.", "form", Name, "msg", "InfoCheckPassword", "Text");
                    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword_SQLite.Focus();
                    return;
                }

                txtFile_SQLite.ForeColor = SystemColors.WindowText;

                try
                {
                    if (cboDatabaseType_SQLite.Text.StartsWith("SQLCipher"))
                    {
                        var sFilename = Path.GetTempFileName();
                        File.Delete(sFilename);

                        var builder = new SQLiteConnectionStringBuilder
                        {
                            DataSource = txtFile_SQLite.Text,
                            Encryption = EncryptionMode.SQLCipher, //EncryptionLicenseKey = sEncryptionLicenseKey
                            FailIfMissing = false,
                            Password = sMode == "ENCRYPT" ? "" : txtPassword_SQLite.Text,
                            Pooling = false,
                            WritableSchema = true
                        };

                        MyGlobal.oSQLCipherReader.ConnectTo(builder.ConnectionString, sMode, sMode == "REMOVE" ? "" : txtPassword_SQLite.Text, sFilename);
                        MyGlobal.oSQLCipherReader.oDisconnect();

                        File.Copy(sFilename, txtFile_SQLite.Text, true);
                        File.Delete(sFilename);
                    }
                    else
                    {
                        var builder = new System.Data.SQLite.SQLiteConnectionStringBuilder
                        {
                            DataSource = txtFile_SQLite.Text,
                            FailIfMissing = false,
                            Password = sMode == "ENCRYPT" ? "" : txtPassword_SQLite.Text,
                            Pooling = false,
                            Version = 3
                        };

                        var sMsg = MyGlobal.oSQLiteReader.ConnectTo(builder.ConnectionString, sMode, sMode == "REMOVE" ? "" : txtPassword_SQLite.Text, "");
                        MyGlobal.oSQLiteReader.oDisconnect();

                        if (sMsg.StartsWith("file is not a database"))
                        {
                            _sLangText = MyGlobal.GetLanguageString("Wrong password or not a database file!", "form", Name, "object", "WrongPasswordOrNotDBFile", "Text");
                            MessageBox.Show(txtFile_SQLite.Text + "\r\n\r\n" + _sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                catch (Exception)
                {
                    //
                }

                rdoWithPassword.Visible = false;
                rdoWithoutPassword.Visible = false;
                btnRemovePassword.Visible = false;
                btnEncryptWithPassword.Visible = false;

                if (sMode == "ENCRYPT")
                {
                    rdoWithPassword.Visible = true;
                    btnRemovePassword.Visible = true;
                    rdoWithPassword.Checked = true;
                    btnRemovePassword.Location = new Point(rdoWithPassword.Left + rdoWithPassword.Width + 15, btnRemovePassword.Top);
                }
                else //REMOVE
                {
                    txtPassword_SQLite.Text = "";
                    rdoWithoutPassword.Visible = true;
                    btnEncryptWithPassword.Visible = true;
                    rdoWithoutPassword.Checked = true;
                    rdoWithoutPassword.Location = new Point(rdoWithPassword.Left, rdoWithoutPassword.Top);
                    btnEncryptWithPassword.Location = new Point(rdoWithoutPassword.Left + rdoWithoutPassword.Width + 15, btnEncryptWithPassword.Top);
                }

                btnSave.PerformClick();
            }
        }

        private static void FindAndMoveMsgBox(int x, int y, bool repaint, string title)
        {
            var thr = new Thread(() =>
            {
                IntPtr msgBox;

                while ((msgBox = FindWindow(IntPtr.Zero, title)) == IntPtr.Zero) ;

                GetWindowRect(msgBox, out var r);
                MoveWindow(msgBox /* handle of the message box */, x, y,
                   r.Width - r.X /* width of originally message box */,
                   r.Height - r.Y /* height of originally message box */,
                   repaint /* if true, the message box repaints */);
            });

            thr.Start();
        }

        private void GetXY4MessageBox(out int iX, out int iY)
        {
            //取得螢幕解析度
            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;
            var iXTemp = Cursor.Position.X;
            var iYTemp = Cursor.Position.Y;
            iX = 12;
            iY = 15;

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
        }

        private void cboDatabase_SQLServer_BeforeDropDownOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            c1GridSQLServer.Visible = false;

            if (_bComboBoxKeypress4Database)
            {
                _bComboBoxKeypress4Database = false;
                return;
            }

            UpdateSQLServerDatabaseList();
        }

        private void cboDatabase_SQLServer_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            if (_bKeyPressTab)
            {
                _bKeyPressTab = false;
                c1GridSQLServer.Visible = false;
                return;
            }
        }

        private void cboDatabase_SQLServer_DropDownOpened(object sender, EventArgs e)
        {
            c1GridSQLServer.Visible = false;
        }

        private void UpdateSQLServerDatabaseList()
        {
            cboDatabase_SQLServer.Items.Clear();

            Cursor = Cursors.WaitCursor;

            if (!CheckData(false, false))
            {
                Cursor = Cursors.Default;
                return;
            }

            ConnectToDatabase_SQLServer(true, true); //UpdateSQLServerDatabaseList

            //以下還原，才不會引發錯誤 & 變更 Tab Color
            MyGlobal.sDataSource = "";
            MyGlobal.sTabBackColor = "";
            MyGlobal.sTabActiveForeColor = "";
            MyGlobal.sTabInactiveForeColor = "";

            Cursor = Cursors.Default;
        }

        private int c1GridSQLServer_Filter(string sCondition)
        {
            if (_dtDatabaseListInfo_SQLServer == null)
            {
                return 0;
            }

            var dataView = _dtDatabaseListInfo_SQLServer.DefaultView;

            try
            {
                var condition = "[Name] LIKE '*" + sCondition + "*'";
                dataView.RowFilter = condition;

                if (dataView.Count == 0)
                {
                    dataView.RowFilter = "[Name] LIKE '*'";
                }

                //20220915 將前面幾個字母相符的排在最前面
                #region
                dataView.Sort = "Name";
                var dtSorted = dataView.ToTable();
                dtSorted.Columns.Add("Sort", typeof(int));
                var j = -1000;
                var sFilterKeyword = MyGlobal.GetStringBetween(condition, "'", "'").Replace("*", "").ToUpper();

                for (var i = 0; i < dtSorted.Rows.Count; i++)
                {
                    if (dtSorted.Rows[i]["Name"].ToString().ToUpper().Substring(0, sFilterKeyword.Length) == sFilterKeyword)
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
                dataView.Sort = "Sort, Name";
                dtSorted = dataView.ToTable();
                dtSorted.Columns.Remove("Sort");
                #endregion

                c1GridSQLServer.DataSource = dtSorted;

                return dtSorted.Rows.Count;
            }
            catch (Exception)
            {
                return 0; //按下 Ctrl+J 可能會進到這個例外錯誤
            }
        }

        private void c1GridSQLServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13 && e.KeyChar != 9)
            {
                return;
            }

            _bKeyPressTab = true;
            c1GridSQLServer_PasteColumnName(); //c1GridSQLServer_KeyPress
            _bKeyPressTab = true;
        }

        private void c1GridSQLServer_PasteColumnName()
        {
            var sCellText = c1GridSQLServer[c1GridSQLServer.Row, 0].ToString();

            cboDatabase_SQLServer.Text = sCellText;
            c1GridSQLServer.Visible = false;
            cboDatabase_SQLServer.Focus();
            _bKeyPressTab = true;
        }

        private void c1GridSQLServer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _bKeyPressTab = true;
            c1GridSQLServer_PasteColumnName(); //c1GridSQLServer_MouseDoubleClick
            _bKeyPressTab = true;
        }

        private void cboDatabase_SQLServer_Enter(object sender, EventArgs e)
        {
            cboDatabase_SQLServer_EnterOrFocus();
        }

        private void cboDatabase_SQLServer_EnterOrFocus()
        {
            if (cboDatabase_SQLServer.Items.Count == 0 && CheckData(false, false))
            {
                UpdateSQLServerDatabaseList();
            }

            if (_bKeyPressTab)
            {
                _bKeyPressTab = false;
                return;
            }

            var iRowCount = c1GridSQLServer_Filter(cboDatabase_SQLServer.Text);

            if (iRowCount > 0)
            {
                ResizeACGrid(c1GridSQLServer, iRowCount, 0);
                c1GridSQLServer.Visible = true;
            }
            else
            {
                c1GridSQLServer.Visible = false;
            }
        }

        private void cboDatabase_SQLServer_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    c1GridSQLServer.Focus();
                    e.SuppressKeyPress = false;
                    break;
                case Keys.Delete:
                    _bKeyPressDelete = true; //不能在此處理 Delete 按鍵，因為此時的 cboDatabase_SQLServer.Text 的值是按下 Delete 鍵之前的！
                    break;
            }
        }

        private void cboDatabase_SQLServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            _bComboBoxKeypress4Database = true;
        }

        private void cboDatabase_SQLServer_KeyUp(object sender, KeyEventArgs e)
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
                var iRowCount = c1GridSQLServer_Filter(cboDatabase_SQLServer.Text);

                if (iRowCount > 0)
                {
                    ResizeACGrid(c1GridSQLServer, iRowCount, 0);
                    c1GridSQLServer.Visible = true;
                }
                else
                {
                    c1GridSQLServer.Visible = false;
                }
            }
        }

        private void cboDatabase_SQLServer_Leave(object sender, EventArgs e)
        {
            if (!c1GridSQLServer.Focused)
            {
                c1GridSQLServer.Visible = false;
            }
        }

        private void cboDatabase_MySQL_BeforeDropDownOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            c1GridMySQL.Visible = false;

            if (_bComboBoxKeypress4Database)
            {
                _bComboBoxKeypress4Database = false;
                return;
            }

            UpdateMySQLDatabaseList();
        }

        private void cboDatabase_MySQL_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            if (_bKeyPressTab)
            {
                _bKeyPressTab = false;
                c1GridMySQL.Visible = false;
                return;
            }
        }

        private void cboDatabase_MySQL_DropDownOpened(object sender, EventArgs e)
        {
            c1GridMySQL.Visible = false;
        }

        private void UpdateMySQLDatabaseList()
        {
            cboDatabase_MySQL.Items.Clear();

            Cursor = Cursors.WaitCursor;

            if (!CheckData(false, false))
            {
                Cursor = Cursors.Default;
                return;
            }

            ConnectToDatabase_MySQL(true, true); //UpdateMySQLDatabaseList

            //以下還原，才不會引發錯誤 & 變更 Tab Color
            MyGlobal.sDataSource = "";
            MyGlobal.sTabBackColor = "";
            MyGlobal.sTabActiveForeColor = "";
            MyGlobal.sTabInactiveForeColor = "";

            Cursor = Cursors.Default;
        }

        private void cboDatabase_MySQL_KeyUp(object sender, KeyEventArgs e)
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
                var iRowCount = c1GridMySQL_Filter(cboDatabase_MySQL.Text);

                if (iRowCount > 0)
                {
                    ResizeACGrid(c1GridMySQL, iRowCount, 0);
                    c1GridMySQL.Visible = true;
                }
                else
                {
                    c1GridMySQL.Visible = false;
                }
            }
        }

        private void cboDatabase_MySQL_Leave(object sender, EventArgs e)
        {
            if (!c1GridMySQL.Focused)
            {
                c1GridMySQL.Visible = false;
            }
        }

        private void cboDatabase_MySQL_KeyPress(object sender, KeyPressEventArgs e)
        {
            _bComboBoxKeypress4Database = true;
        }

        private void cboDatabase_MySQL_Enter(object sender, EventArgs e)
        {
            cboDatabase_MySQL_EnterOrFocus();
        }

        private void cboDatabase_MySQL_EnterOrFocus()
        {
            if (cboDatabase_MySQL.Items.Count == 0 && CheckData(false, false))
            {
                UpdateMySQLDatabaseList();
            }

            if (_bKeyPressTab)
            {
                _bKeyPressTab = false;
                return;
            }

            var iRowCount = c1GridMySQL_Filter(cboDatabase_MySQL.Text);

            if (iRowCount > 0)
            {
                ResizeACGrid(c1GridMySQL, iRowCount, 0);
                c1GridMySQL.Visible = true;
            }
            else
            {
                c1GridMySQL.Visible = false;
            }
        }

        private int c1GridMySQL_Filter(string sCondition)
        {
            if (_dtDatabaseListInfo_MySQL == null)
            {
                return 0;
            }

            var dataView = _dtDatabaseListInfo_MySQL.DefaultView;

            try
            {
                var condition = "[Name] LIKE '*" + sCondition + "*'";
                dataView.RowFilter = condition;

                if (dataView.Count == 0)
                {
                    dataView.RowFilter = "[Name] LIKE '*'";
                }

                //20220915 將前面幾個字母相符的排在最前面
                #region
                dataView.Sort = "Name";
                var dtSorted = dataView.ToTable();
                dtSorted.Columns.Add("Sort", typeof(int));
                var j = -1000;
                var sFilterKeyword = MyGlobal.GetStringBetween(condition, "'", "'").Replace("*", "").ToUpper();

                for (var i = 0; i < dtSorted.Rows.Count; i++)
                {
                    if (dtSorted.Rows[i]["Name"].ToString().ToUpper().Substring(0, sFilterKeyword.Length) == sFilterKeyword)
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
                dataView.Sort = "Sort, Name";
                dtSorted = dataView.ToTable();
                dtSorted.Columns.Remove("Sort");
                #endregion

                c1GridMySQL.DataSource = dtSorted;

                return dtSorted.Rows.Count;
            }
            catch (Exception)
            {
                return 0; //按下 Ctrl+J 可能會進到這個例外錯誤
            }
        }

        private void c1GridMySQL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13 && e.KeyChar != 9)
            {
                return;
            }

            _bKeyPressTab = true;
            c1GridMySQL_PasteColumnName(); //c1GridMySQL_KeyPress
            _bKeyPressTab = true;
        }

        private void c1GridMySQL_PasteColumnName()
        {
            var sCellText = c1GridMySQL[c1GridMySQL.Row, 0].ToString();

            cboDatabase_MySQL.Text = sCellText;
            c1GridMySQL.Visible = false;
            cboDatabase_MySQL.Focus();
            _bKeyPressTab = true;
        }

        private void c1GridMySQL_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _bKeyPressTab = true;
            c1GridMySQL_PasteColumnName(); //c1GridMySQL_MouseDoubleClick
            _bKeyPressTab = true;
        }

        private void cboDatabase_MySQL_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    c1GridMySQL.Focus();
                    e.SuppressKeyPress = false;
                    break;
                case Keys.Delete:
                    _bKeyPressDelete = true; //不能在此處理 Delete 按鍵，因為此時的 cboDatabase_MySQL.Text 的值是按下 Delete 鍵之前的！
                    break;
            }
        }

        private void cboDatabase_PostgreSQL_BeforeDropDownOpen(object sender, System.ComponentModel.CancelEventArgs e)
        {
            c1GridPostgreSQL.Visible = false;

            if (_bComboBoxKeypress4Database)
            {
                _bComboBoxKeypress4Database = false;
                return;
            }

            UpdatePostgreSQLDatabaseList();
        }

        private void cboDatabase_PostgreSQL_DropDownClosed(object sender, DropDownClosedEventArgs e)
        {
            if (_bKeyPressTab)
            {
                _bKeyPressTab = false;
                c1GridPostgreSQL.Visible = false;
                return;
            }
        }

        private void cboDatabase_PostgreSQL_DropDownOpened(object sender, EventArgs e)
        {
            c1GridPostgreSQL.Visible = false;
        }

        private void UpdatePostgreSQLDatabaseList()
        {
            cboDatabase_PostgreSQL.Items.Clear();

            Cursor = Cursors.WaitCursor;

            if (!CheckData(false, false))
            {
                Cursor = Cursors.Default;
                return;
            }

            ConnectToDatabase_PostgreSQL(true, true); //UpdatePostgreSQLDatabaseList

            //以下還原，才不會引發錯誤 & 變更 Tab Color
            MyGlobal.sDataSource = "";
            MyGlobal.sTabBackColor = "";
            MyGlobal.sTabActiveForeColor = "";
            MyGlobal.sTabInactiveForeColor = "";

            Cursor = Cursors.Default;
        }

        private void cboDatabase_PostgreSQL_KeyUp(object sender, KeyEventArgs e)
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
                var iRowCount = c1GridPostgreSQL_Filter(cboDatabase_PostgreSQL.Text);

                if (iRowCount > 0)
                {
                    ResizeACGrid(c1GridPostgreSQL, iRowCount, 0);
                    c1GridPostgreSQL.Visible = true;
                }
                else
                {
                    c1GridPostgreSQL.Visible = false;
                }
            }
        }

        private void cboDatabase_PostgreSQL_Leave(object sender, EventArgs e)
        {
            if (!c1GridPostgreSQL.Focused)
            {
                c1GridPostgreSQL.Visible = false;
            }
        }

        private void cboDatabase_PostgreSQL_KeyPress(object sender, KeyPressEventArgs e)
        {
            _bComboBoxKeypress4Database = true;
        }

        private void cboDatabase_PostgreSQL_Enter(object sender, EventArgs e)
        {
            cboDatabase_PostgreSQL_EnterOrFocus();
        }

        private void cboDatabase_PostgreSQL_EnterOrFocus()
        {
            if (cboDatabase_PostgreSQL.Items.Count == 0 && CheckData(false, false))
            {
                UpdatePostgreSQLDatabaseList();
            }

            if (_bKeyPressTab)
            {
                _bKeyPressTab = false;
                return;
            }

            var iRowCount = c1GridPostgreSQL_Filter(cboDatabase_PostgreSQL.Text);

            if (iRowCount > 0)
            {
                ResizeACGrid(c1GridPostgreSQL, iRowCount, 0);
                c1GridPostgreSQL.Visible = true;
            }
            else
            {
                c1GridPostgreSQL.Visible = false;
            }
        }

        private int c1GridPostgreSQL_Filter(string sCondition)
        {
            if (_dtDatabaseListInfo_PostgreSQL == null)
            {
                return 0;
            }

            var dataView = _dtDatabaseListInfo_PostgreSQL.DefaultView;

            try
            {
                var condition = "[Name] LIKE '*" + sCondition + "*'";
                dataView.RowFilter = condition;

                if (dataView.Count == 0)
                {
                    dataView.RowFilter = "[Name] LIKE '*'";
                }

                //20220915 將前面幾個字母相符的排在最前面
                #region
                dataView.Sort = "Name";
                var dtSorted = dataView.ToTable();
                dtSorted.Columns.Add("Sort", typeof(int));
                var j = -1000;
                var sFilterKeyword = MyGlobal.GetStringBetween(condition, "'", "'").Replace("*", "").ToUpper();

                for (var i = 0; i < dtSorted.Rows.Count; i++)
                {
                    if (dtSorted.Rows[i]["Name"].ToString().ToUpper().Substring(0, sFilterKeyword.Length) == sFilterKeyword)
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
                dataView.Sort = "Sort, Name";
                dtSorted = dataView.ToTable();
                dtSorted.Columns.Remove("Sort");
                #endregion

                c1GridPostgreSQL.DataSource = dtSorted;

                return dtSorted.Rows.Count;
            }
            catch (Exception)
            {
                return 0; //按下 Ctrl+J 可能會進到這個例外錯誤
            }
        }

        private void c1GridPostgreSQL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13 && e.KeyChar != 9)
            {
                return;
            }

            _bKeyPressTab = true;
            c1GridPostgreSQL_PasteColumnName(); //c1GridPostgreSQL_KeyPress
            _bKeyPressTab = true;
        }

        private void c1GridPostgreSQL_PasteColumnName()
        {
            var sCellText = c1GridPostgreSQL[c1GridPostgreSQL.Row, 0].ToString();

            cboDatabase_PostgreSQL.Text = sCellText;
            c1GridPostgreSQL.Visible = false;
            cboDatabase_PostgreSQL.Focus();
            _bKeyPressTab = true;
        }

        private void c1GridPostgreSQL_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _bKeyPressTab = true;
            c1GridPostgreSQL_PasteColumnName(); //c1GridPostgreSQL_MouseDoubleClick
            _bKeyPressTab = true;
        }

        private void cboDatabase_PostgreSQL_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    c1GridPostgreSQL.Focus();
                    e.SuppressKeyPress = false;
                    break;
                case Keys.Delete:
                    _bKeyPressDelete = true; //不能在此處理 Delete 按鍵，因為此時的 cboDatabase_PostgreSQL.Text 的值是按下 Delete 鍵之前的！
                    break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var bHandled = false;

            switch (keyData)
            {
                case Keys.Down:
                    if (c1GridPostgreSQL.Visible && cboDatabase_PostgreSQL.Focused)
                    {
                        c1GridPostgreSQL.Focus();
                        bHandled = true;
                    }
                    else if (c1GridSQLServer.Visible && cboDatabase_SQLServer.Focused)
                    {
                        c1GridSQLServer.Focus();
                        bHandled = true;
                    }
                    else if (c1GridMySQL.Visible && cboDatabase_MySQL.Focused)
                    {
                        c1GridMySQL.Focus();
                        bHandled = true;
                    }

                    break;
                case Keys.Tab: //Tab
                case Keys.Enter: //Enter
                    _bKeyPressTab = true;

                    if (c1GridPostgreSQL.Visible)
                    {
                        c1GridPostgreSQL_PasteColumnName(); //ProcessCmdKey, Tab
                        c1GridPostgreSQL.Visible = false;
                        bHandled = true;
                    }
                    else if (c1GridSQLServer.Visible)
                    {
                        c1GridSQLServer_PasteColumnName(); //ProcessCmdKey, Tab
                        c1GridSQLServer.Visible = false;
                        bHandled = true;
                    }
                    else if (c1GridMySQL.Visible)
                    {
                        c1GridMySQL_PasteColumnName(); //ProcessCmdKey, Tab
                        c1GridMySQL.Visible = false;
                        bHandled = true;
                    }

                    break;
                case Keys.Escape:
                    _bKeyPressESC = true;

                    if (c1GridPostgreSQL.Visible)
                    {
                        c1GridPostgreSQL.Visible = false;
                        cboDatabase_PostgreSQL.Focus();
                        bHandled = true;
                    }
                    else if (c1GridSQLServer.Visible)
                    {
                        c1GridSQLServer.Visible = false;
                        cboDatabase_SQLServer.Focus();
                        bHandled = true;
                    }
                    else if (c1GridMySQL.Visible)
                    {
                        c1GridMySQL.Visible = false;
                        cboDatabase_MySQL.Focus();
                        bHandled = true;
                    }

                    break;
            }

            return bHandled; //此處如果回傳 false，表示在其他的元件，此按鍵是會發生作用的
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

        private void cboDatabase_PostgreSQL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bComboBoxKeypress4Database)
            {
                return; //鍵盤輸入，忽略！
            }

            _bKeyPressTab = true;
        }

        private void cboDatabase_SQLServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bComboBoxKeypress4Database)
            {
                return; //鍵盤輸入，忽略！
            }

            _bKeyPressTab = true;
        }

        private void cboDatabase_MySQL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bComboBoxKeypress4Database)
            {
                return; //鍵盤輸入，忽略！
            }

            _bKeyPressTab = true;
        }

        private void cboDatabase_SQLServer_TextChanged(object sender, EventArgs e)
        {
            if (cboDatabase_SQLServer.Items.Count == 0)
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

            var iRowCount = c1GridSQLServer_Filter(cboDatabase_SQLServer.Text);

            if (iRowCount > 0)
            {
                ResizeACGrid(c1GridSQLServer, iRowCount, 0);
                c1GridSQLServer.Visible = true;
            }
            else
            {
                c1GridSQLServer.Visible = false;
            }
        }

        private void cboDatabase_MySQL_TextChanged(object sender, EventArgs e)
        {
            if (cboDatabase_MySQL.Items.Count == 0)
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

            var iRowCount = c1GridMySQL_Filter(cboDatabase_MySQL.Text);

            if (iRowCount > 0)
            {
                ResizeACGrid(c1GridMySQL, iRowCount, 0);
                c1GridMySQL.Visible = true;
            }
            else
            {
                c1GridMySQL.Visible = false;
            }
        }

        private void cboDatabase_PostgreSQL_TextChanged(object sender, EventArgs e)
        {
            if (cboDatabase_PostgreSQL.Items.Count == 0)
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

            var iRowCount = c1GridPostgreSQL_Filter(cboDatabase_PostgreSQL.Text);

            if (iRowCount > 0)
            {
                ResizeACGrid(c1GridPostgreSQL, iRowCount, 0);
                c1GridPostgreSQL.Visible = true;
            }
            else
            {
                c1GridPostgreSQL.Visible = false;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (var myForm = new frmConnectExport())
            {
                myForm.ShowDialog();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (var myForm = new frmConnectImport())
            {
                myForm.ShowDialog();
            }

            //重新載入 DBInfo 資料
            CreateAndGetDbInfoTable(); //btnImport_Click

            //指定哪一個 Column 要套用 FetchCellStyle (這裡要重新指定一次，並 Refresh，才會顯示顏色，而不是顏色代碼)
            c1GridDBInfo.Splits[0].DisplayColumns[_lstGridHeader[(int)eMenu.eTabBackColor]].FetchStyle = true;
            c1GridDBInfo.Refresh();
        }

        private void chkEncryptWithCustomPW_Click(object sender, EventArgs e)
        {
            using (var myForm = new frmEncryptionMothed())
            {
                myForm.ShowDialog();
            }
        }
    }
}