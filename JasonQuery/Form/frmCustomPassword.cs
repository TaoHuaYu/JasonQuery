using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JasonQuery
{
    public partial class frmCustomPassword : Form
    {
        private bool _bClose = false; //20230128 按下 btnOK 會觸發 Form_Close，找不出原因，故暫時用此變數解決此異常

        public frmCustomPassword()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MyGlobal.ApplyLanguageInfo(this); //Form_Load
            MyGlobal.SetC1ComboBoxItemsFromDictionary(cboLocalization, MyGlobal.dicLocalization, true);
            cboLocalization.Text = MyGlobal.sLocalization;
            cboLocalization.Tag = MyGlobal.sLocalization;
            lblInfo.ForeColor = Color.Maroon;
            Text = "JasonQuery " + MyGlobal.sMyVersion + " - " + Text;
        }

        private void txtEncryptPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Control && e.KeyCode == Keys.Space || e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txtEncryptPassword.Text))
            {
                btnOK.PerformClick();
            }
        }

        private void txtEncryptPassword_TextChanged(object sender, EventArgs e)
        {
            for (var i = txtEncryptPassword.Text.Length - 1; i >= 0; i--)
            {
                if (!MyGlobal.IsEngAlphabetOrNumberOrSpecialCharacters(txtEncryptPassword.Text.Substring(i, 1), " "))
                {
                    txtEncryptPassword.Text = txtEncryptPassword.Text.Replace(txtEncryptPassword.Text.Substring(i, 1), "");
                    txtEncryptPassword.SelectionStart = i;
                    break;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEncryptPassword.Text))
            {
                var sMsg = MyGlobal.GetLanguageString("Please enter password.", "form", Name, "msg", "NonePassword", "Text");
                MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEncryptPassword.Focus();
                return;
            }

            if (DBCommon.CheckDBPassword(txtEncryptPassword.Text))
            {
                MyGlobal.sMyDBConnectionPassword = MyGlobal.sMyDBConnectionPasswordPrefix + txtEncryptPassword.Text + MyGlobal.sMyDBConnectionPasswordSuffix;
                _bClose = true;
                Close();
            }
            else
            {
                var sMsg = MyGlobal.GetLanguageString("Wrong password!", "form", Name, "msg", "WrongPassword", "Text") + "\r\n";
                sMsg += MyGlobal.GetLanguageString("You must re-enter your password.", "form", Name, "msg", "ReEnter", "Text");
                MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEncryptPassword.Focus();
            }
        }

        private void frmCustomPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_bClose == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void frmCustomPassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_bClose == false)
            {
                return;
            }
        }

        private void cboLocalization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyGlobal.sLocalization == cboLocalization.Text || !CheckLocalizationFileExist(true))
            {
                return;
            }

            MyGlobal.sLocalization = cboLocalization.Text;
            MyGlobal.LoadLocalizationXML(); //cboLocalization_SelectedIndexChanged
            MyGlobal.ApplyLanguageInfo(this); //cboLocalization_SelectedIndexChanged
            lblInfo.ForeColor = Color.Maroon;
            Text = "JasonQuery " + MyGlobal.sMyVersion + " - " + Text;
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

            var sLangText = MyGlobal.GetLanguageString("Localization file not found!", "Global", "Global", "msg", "LocalizationNotFound", "Text");
            MessageBox.Show(sLangText + "\r\n\r\n" + sFilename, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return false;
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