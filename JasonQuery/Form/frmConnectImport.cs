using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using C1.C1Zip;
using C1.C1Excel;

namespace JasonQuery
{
    public sealed partial class frmConnectImport : Form
    {
        public frmConnectImport()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MyGlobal.ApplyLanguageInfo(this); //Form_Load

            txtFilename.Location = new Point(lblFilename.Left + lblFilename.Width, txtFilename.Top);
            btnBrowseFile.Location = new Point(txtFilename.Left + txtFilename.Width + 5, btnBrowseFile.Top);
            txtEncryptPassword.Location = new Point(lblPassword.Left + lblPassword.Width, txtEncryptPassword.Top);
            btnEncryptPasswordView.Location = new Point(txtEncryptPassword.Left + txtEncryptPassword.Width + 5, btnEncryptPasswordView.Top);
        }

        private void txtEncryptPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Control && e.KeyCode == Keys.Space || e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtEncryptPassword_TextChanged(object sender, EventArgs e)
        {
            for (var i = txtEncryptPassword.Text.Length - 1; i >= 0 ; i--)
            {
                if (!MyGlobal.IsEngAlphabetOrNumberOrSpecialCharacters(txtEncryptPassword.Text.Substring(i, 1), " "))
                {
                    txtEncryptPassword.Text = txtEncryptPassword.Text.Replace(txtEncryptPassword.Text.Substring(i, 1), "");
                    txtEncryptPassword.SelectionStart = i;
                    break;
                }
            }
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            var sf = new OpenFileDialog
            {
                Title = MyGlobal.GetLanguageString("Open file", "Global", "Global", "msg", "OpenFile", "Text"),
                Filter = @"JasonQuery files (*.jqc)|*.jqc"
            };

            if (sf.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txtFilename.Text = sf.FileName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var sMsg = "";

            if (string.IsNullOrEmpty(txtFilename.Text))
            {
                sMsg = MyGlobal.GetLanguageString("Please select the file name to import!", "form", Name, "msg", "NoneImportFromFilename", "Text");
                MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnBrowseFile.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtEncryptPassword.Text))
            {
                sMsg = MyGlobal.GetLanguageString("Please enter password.", "form", Name, "msg", "NonePassword", "Text");
                MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEncryptPassword.Focus();
                return;
            }

            byte[] InData = null;
            TextEngine.BinRead(txtFilename.Text, ref InData);
            InData[0] = 80; //50
            InData[1] = 75; //4B
            InData[2] = 3; //03
            InData[3] = 4; //04
            InData[4] = 20; //14
            var sFilenameZip = Path.GetTempFileName(); //Path.GetTempPath() + Path.GetFileName(txtFilename.Text);
            var sFilenameXls = Path.GetTempFileName();
            TextEngine.WriteBinaryFile(sFilenameZip, InData);

            var zip = new C1ZipFile
            {
                UseUtf8Encoding = true,
                Password = MyGlobal.sMyDBConnectionPasswordPrefix + txtEncryptPassword.Text + MyGlobal.sMyDBConnectionExportPasswordSuffix
            };

            var bOpenNG = false;

            try
            {
                zip.Open(sFilenameZip);
                zip.Entries.Extract(0, sFilenameXls);
            }
            catch (Exception)
            {
                //ex.InnerException.Message = "Invalid password: can't decrypt data."
                sMsg = MyGlobal.GetLanguageString("Wrong password!", "form", "frmConnect", "msg", "WrongPassword", "Text") + "\r\n";
                sMsg += MyGlobal.GetLanguageString("You must re-enter your password.", "form", "frmConnect", "msg", "ReEnter", "Text");
                MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEncryptPassword.Focus();
                bOpenNG = false;
            }
            finally
            {
                zip.Close();
            }

            if (bOpenNG)
            {
                return;
            }

            if (!File.Exists(sFilenameXls))
            {
                return;
            }

            var book = new C1XLBook();

            try
            {
                book.Load(sFilenameXls);
                var iCount = 0;
                var sSql = "";

                for (var i = 0; i < 2000; i++) //從 A1 儲存格開始讀取資料
                {
                    if (book.Sheets[0][i, 0].Value == null || string.IsNullOrEmpty(book.Sheets[0][i, 0].Value.ToString()))
                    {
                        break;
                    }

                    var iCol = 0;
                    var sConnectionName = book.Sheets[0][i, iCol].Value.ToString();

                    var sSql0 = "SELECT * FROM DBInfo WHERE DomainUser = '{0}' AND ConnectionName = '{1}'";
                    sSql0 = string.Format(sSql0, MyGlobal.sDomainUser, sConnectionName);
                    var dt = DBCommon.ExecQuery(sSql0);

                    if (dt.Rows.Count > 0)
                    {
                        sConnectionName += DateTime.Now.ToString("_MMddHHmmss");
                    }

                    iCol++;
                    var sDataSource = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sServer = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sSID = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sDirectMode = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sDatabase = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sConnectAs = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sPort = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sUser = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sPassword = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();

                    if (!string.IsNullOrEmpty(sPassword))
                    {
                        sPassword = TextEngine.Encode(TextEngine.Encrypt(sPassword, MyGlobal.sDomainUser));
                    }

                    iCol++;
                    var sAutoRollback = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sUnicode = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sTabBackColor = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sTabActiveForeColor = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sTabInactiveForeColor = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var sRemarks = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s1 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s2 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s3 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s4 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s5 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s6 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s7 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s8 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s9 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s10 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s11 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s12 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s13 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s14 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();
                    iCol++;
                    var s15 = book.Sheets[0][i, iCol].Value == null ? "" : book.Sheets[0][i, iCol].Value.ToString();

                    sSql += "INSERT INTO DBInfo (DomainUser, ConnectionName, DataSource, Server, SID,\r\n";
                    sSql += "       DirectMode, Database, ConnectAs, Port, User, Password,\r\n";
                    sSql += "       AutoRollback, Unicode, TabBackColor, TabActiveForeColor,\r\n";
                    sSql += "       TabInactiveForeColor, Remarks, o1, o2, o3, o4, o5, o6, o7, o8,\r\n";
                    sSql += "       o9, o10, o11, o12, o13, o14, o15)\r\n";
                    sSql += "VALUES ('" + MyGlobal.sDomainUser + "', '" + sConnectionName + "', '" + sDataSource + "', '" + sServer + "', '" + sSID + "',\r\n";
                    sSql += "        '" + sDirectMode + "', '" + sDatabase + "', '" + sConnectAs + "', '" + sPort + "', '" + sUser + "', '" + sPassword + "',\r\n";
                    sSql += "        '" + sAutoRollback + "', '" + sUnicode + "', '" + sTabBackColor + "', '" + sTabActiveForeColor + "',\r\n";
                    sSql += "        '" + sTabInactiveForeColor + "', '" + sRemarks + "', '" + s1 + "', '" + s2 + "', '" + s3 + "', '" + s4 + "', '" + s5 + "', '" + s6 + "', '" + s7 + "', '" + s8 + "',\r\n";
                    sSql += "        '" + s9 + "', '" + s10 + "', '" + s11 + "', '" + s12 + "', '" + s13 + "', '" + s14 + "', '" + s15 + "');\r\n";
                    iCount++;
                }

                DBCommon.ExecNonQuery(sSql);

                sMsg = MyGlobal.GetLanguageString("{qty} connection information imported successfully!", "form", Name, "msg", "ImportOK", "Text").Replace("{qty}", iCount.ToString());
                MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                sMsg = MyGlobal.GetMessageBoxErrorMsg("AnUnexpectedErrorHasOccurred", ex.Message, true);
                MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                book.Dispose();
            }
        }

        private void btnEncryptPasswordView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                txtEncryptPassword.PasswordChar = '\0';
            }
        }

        private void btnEncryptPasswordView_MouseUp(object sender, MouseEventArgs e)
        {
            txtEncryptPassword.PasswordChar = '*';
            txtEncryptPassword.Focus();
        }
    }
}