using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using C1.C1Zip;
using C1.C1Excel;

namespace JasonQuery
{
    public sealed partial class frmConnectExport : Form
    {
        public frmConnectExport()
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
            btnHelp_Password.Location = new Point(btnEncryptPasswordView.Left + btnEncryptPasswordView.Width + 5, btnHelp_Password.Top);
            chkIncludeDBPassword.Location = new Point(chkEncrypt.Left + chkEncrypt.Width + 20, chkIncludeDBPassword.Top);
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
            var sf = new SaveFileDialog
            {
                Title = MyGlobal.GetLanguageString("Save As", "Global", "Global", "msg", "SaveAs", "Text"),
                Filter = @"JasonQuery files (*.jqc)|*.jqc"
            };

            if (sf.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (string.IsNullOrEmpty(Path.GetExtension(sf.FileName)))
            {
                sf.FileName += ".jqc";
            }
            else if (Path.GetExtension(sf.FileName) != ".jqc")
            {
                sf.FileName = Path.GetDirectoryName(sf.FileName) + "\\" + Path.GetFileNameWithoutExtension(sf.FileName) + ".jqc";
            }

            txtFilename.Text = sf.FileName;
        }

        private void chkEncrypt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEncrypt.Checked == false)
            {
                chkEncrypt.Checked = true;
            }

            txtEncryptPassword.Focus();
        }

        private void btnHelp_Password_Click(object sender, EventArgs e)
        {
            //Available characters for password.
            var sMsg = MyGlobal.GetLanguageString("A valid password can contain the following characters:", "form", Name, "msg", "PasswordHelp1", "Text") + "\r\n\r\n";
            sMsg += MyGlobal.GetLanguageString("Lowercase characters a-z", "form", Name, "msg", "PasswordHelp2", "Text") + "\r\n";
            sMsg += MyGlobal.GetLanguageString("Uppercase characters A-Z", "form", Name, "msg", "PasswordHelp3", "Text") + "\r\n";
            sMsg += MyGlobal.GetLanguageString("Numbers 0-9", "form", Name, "msg", "PasswordHelp4", "Text") + "\r\n";
            //sMsg += MyGlobal.GetLanguageString("Space", "form", Name, "msg", "PasswordHelp5", "Text") + "\r\n";
            sMsg += MyGlobal.GetLanguageString("Special characters", "form", Name, "msg", "PasswordHelp5", "Text").Trim() + " `~!@#$%^&*()_-+=[{]}|\\;:'\",<.>/?";
            MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var sMsg = "";

            if (string.IsNullOrEmpty(txtFilename.Text))
            {
                sMsg = MyGlobal.GetLanguageString("Please select the file name to export!", "form", Name, "msg", "NoneExportToFilename", "Text");
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

            var book = new C1XLBook();
            var sheet = book.Sheets[0];
            var dt = DBCommon.ExecQuery("SELECT * FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' ORDER BY LastConnect DESC");

            for (var iRow = 0; iRow < dt.Rows.Count; iRow++)
            {
                var iCol = 0;
                //sheet[iRow, iCol].Value = "DomainUser";
                //iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["ConnectionName"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["DataSource"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["Server"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["SID"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["DirectMode"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["Database"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["ConnectAs"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["Port"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["User"].ToString();

                iCol++;

                if (chkIncludeDBPassword.Checked)
                {
                    var sPasswordResult = TextEngine.Decrypt(TextEngine.Decode(dt.Rows[iRow]["Password"].ToString()), MyGlobal.sDomainUser);
                    sheet[iRow, iCol].Value = sPasswordResult;
                }
                else
                {
                    sheet[iRow, iCol].Value = "";
                }

                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["AutoRollback"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["Unicode"].ToString();
                iCol++;
                //sheet[iRow, iCol].Value = dt.Rows[iRow]["LastConnect"].ToString();
                //iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["TabBackColor"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["TabActiveForeColor"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["TabInactiveForeColor"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["Remarks"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o1"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o2"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o3"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o4"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o5"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o6"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o7"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o8"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o9"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o10"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o11"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o12"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o13"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o14"].ToString();
                iCol++;
                sheet[iRow, iCol].Value = dt.Rows[iRow]["o15"].ToString();
            }

            var sFilename = Path.GetTempFileName().Replace(".tmp", ".xls");
            book.Save(sFilename);

            var zip = new C1ZipFile();
            zip.Create(txtFilename.Text);
            zip.UseUtf8Encoding = true;
            zip.Password = MyGlobal.sMyDBConnectionPasswordPrefix + txtEncryptPassword.Text + MyGlobal.sMyDBConnectionExportPasswordSuffix;
            zip.CompressionLevel = CompressionLevelEnum.BestCompression;
            zip.Comment = "Connection Information - Exported by JasonQuery";
            zip.Entries.Add(sFilename);
            zip.Close();
            File.Delete(sFilename);
            File.Delete(sFilename.Replace(".xls", ".tmp"));

            //var ss = "jasonquery" + txtEncryptPassword.Text + "exportdbinfo";
            byte[] InData = null;
            TextEngine.BinRead(txtFilename.Text, ref InData);
            InData[0] = 8; //50
            InData[1] = 7; //4B
            InData[2] = 0; //03
            InData[3] = 1; //04
            InData[4] = 1; //14
            TextEngine.WriteBinaryFile(txtFilename.Text, InData);

            sMsg = MyGlobal.GetLanguageString("All the connection information has been exported to", "form", Name, "msg", "ExportOK", "Text") + "\r\n" + txtFilename.Text;
            MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
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