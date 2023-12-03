using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JasonQuery
{
    public partial class frmEncryptionMothed : Form
    {
        private int _iWidth = 0;
        private bool _bClose = false; //20230128 按下 btnApply 會觸發 Form_Close，找不出原因，故暫時用此變數解決此異常

        public frmEncryptionMothed()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MyGlobal.ApplyLanguageInfo(this, false);

            _iWidth = Math.Max(Math.Max(500, lblChangeToDefaultPassword.Width + 70), lblCaution0.Width + 45); //700
            lblOldPassword2.Location = new Point(rdoDefaultPassword.Left + rdoDefaultPassword.Width + 25, lblOldPassword2.Top);
            txtOldPassword2.Location = new Point(lblOldPassword2.Left + lblOldPassword2.Width, txtOldPassword2.Top);
            txtCustomPassword.Location = new Point(rdoCustomPassword.Left + rdoCustomPassword.Width - 2, txtCustomPassword.Top);
            btnCustomPasswordView.Location = new Point(txtCustomPassword.Left + txtCustomPassword.Width + 6, btnCustomPasswordView.Top);
            btnHelp_Password.Location = new Point(btnCustomPasswordView.Left + btnCustomPasswordView.Width + 5, btnHelp_Password.Top);
            txtOldPassword.Location = new Point(lblOldPassword.Left + lblOldPassword.Width, txtOldPassword.Top);
            txtNewPassword.Location = new Point(lblNewPassword.Left + lblNewPassword.Width, txtNewPassword.Top);
            txtConfirmNewPassword.Location = new Point(lblConfirmNewPassword.Left + lblConfirmNewPassword.Width, txtConfirmNewPassword.Top);
            lblEncryptionMothed.Location = new Point(lblTitle.Left + lblTitle.Width - 5, lblEncryptionMothed.Top);
            //lblChangeToDefaultPassword.Location = new Point(rdoDefaultPassword.Left + rdoDefaultPassword.Width + 2, lblChangeToDefaultPassword.Top);

            if (MyGlobal.sMyDBConnectionPassword.Equals("ytec1688"))
            {
                rdoChangeCustomPassword.Enabled = false;
                lblOldPassword.Enabled = false;
                txtOldPassword.Enabled = false;
                lblNewPassword.Enabled = false;
                txtNewPassword.Enabled = false;
                lblConfirmNewPassword.Enabled = false;
                txtConfirmNewPassword.Enabled = false;
                btnApply.Enabled = false;
                rdoDefaultPassword.Enabled = false;
                lblOldPassword2.Enabled = false;
                txtOldPassword2.Enabled = false;
                txtCustomPassword.Focus();
                lblEncryptionMothed.Text = MyGlobal.GetLanguageString("Default Password", "form", Name, "msg", "DefaultPassword", "Text");
                Size = new Size(_iWidth, 280);
            }
            else
            {
                rdoCustomPassword.Enabled = false;
                txtCustomPassword.Text = MyGlobal.sMyDBConnectionPassword;
                txtCustomPassword.Enabled = false;
                btnCustomPasswordView.Visible = false;
                txtOldPassword.Text = ""; //MyGlobal.sMyDBConnectionPassword;
                txtOldPassword.Tag = MyGlobal.sMyDBConnectionPassword.Substring(MyGlobal.sMyDBConnectionPasswordPrefix.Length, MyGlobal.sMyDBConnectionPassword.Length - 27);
                txtOldPassword.Enabled = true;
                txtOldPassword2.Text = "";
                txtOldPassword2.Tag = MyGlobal.sMyDBConnectionPassword.Substring(MyGlobal.sMyDBConnectionPasswordPrefix.Length, MyGlobal.sMyDBConnectionPassword.Length - 27);
                lblChangeToDefaultPassword.Visible = true;
                Size = new Size(_iWidth, 355);
                lblEncryptionMothed.Text = MyGlobal.GetLanguageString("Custom Password", "form", Name, "msg", "CustomPassword", "Text");
                rdoChangeCustomPassword.Checked = true;
                //btnApply.Enabled = false;
                //Application.DoEvents();
                //System.Threading.Thread.Sleep(100);
                //Application.DoEvents();
                txtNewPassword.Focus();
                btnApply.Enabled = true;
                txtOldPassword.Focus();
                //txtConfirmNewPassword.Focus();
                //tmrNewPassword.Enabled = true;
            }
        }

        private void rdoPassword_CheckedChanged(object sender, EventArgs e)
        {
            var rdo = sender as RadioButton;

            if (rdo.Checked == false)
            {
                return; //避免執行兩次 (第一次會是「前一次」的選項，且為「未核取」，忽略它！)
            }

            var bEnable = true;

            switch (rdo.Name)
            {
                case "rdoDefaultPassword":
                    Size = new Size(_iWidth, 280);

                    if (MyGlobal.sMyDBConnectionPassword.Equals("ytec1688"))
                    {
                        bEnable = false;
                    }
                    else
                    {
                        txtOldPassword2.Focus();
                    }

                    break;
                case "rdoCustomPassword":
                case "rdoChangeCustomPassword":
                    lblCaution0.Visible = true;
                    lblCaution1.Visible = true;
                    lblCaution2.Visible = true;
                    Size = new Size(_iWidth, 355);

                    if (rdo.Name.Equals("rdoCustomPassword"))
                    {
                        txtCustomPassword.Focus();
                    }
                    else
                    {
                        txtOldPassword.Focus();
                    }

                    break;
            }

            btnApply.Enabled = bEnable;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V || e.Control && e.KeyCode == Keys.Space || e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            var txt = sender as C1.Win.C1Input.C1TextBox;

            for (var i = txt.Text.Length - 1; i >= 0; i--)
            {
                if (!MyGlobal.IsEngAlphabetOrNumberOrSpecialCharacters(txt.Text.Substring(i, 1), " "))
                {
                    txt.Text = txt.Text.Replace(txt.Text.Substring(i, 1), "");
                    txt.SelectionStart = i;
                    break;
                }
            }
        }

        private void txtCustomPassword_Enter(object sender, EventArgs e)
        {
            rdoCustomPassword.Checked = true;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            rdoChangeCustomPassword.Checked = true;
        }

        private void txtOldPassword2_Enter(object sender, EventArgs e)
        {
            rdoDefaultPassword.Checked = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _bClose = true;
            Close();
        }

        private void frmEncryptionMothed_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_bClose == false)
            {
                e.Cancel = true;
                return;
            }
        }

        private void frmEncryptionMothed_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_bClose == false)
            {
                return;
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

        private void btnApply_Click(object sender, EventArgs e)
        {
            var sMsg = "";

            if (rdoCustomPassword.Checked && string.IsNullOrEmpty(txtCustomPassword.Text))
            {
                sMsg = MyGlobal.GetLanguageString("Please enter your password.", "form", Name, "msg", "NonePassword", "Text");
                MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCustomPassword.Focus();
                return;
            }
            else if (rdoChangeCustomPassword.Checked)
            {
                if (string.IsNullOrEmpty(txtOldPassword.Text))
                {
                    sMsg = MyGlobal.GetLanguageString("Please enter your old custom password.", "form", Name, "msg", "NoneOldPassword", "Text");
                    MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOldPassword.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtNewPassword.Text))
                {
                    sMsg = MyGlobal.GetLanguageString("Please enter your new password.", "form", Name, "msg", "NoneNewPassword", "Text");
                    MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewPassword.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtConfirmNewPassword.Text))
                {
                    sMsg = MyGlobal.GetLanguageString("Please enter your confirm password.", "form", Name, "msg", "NoneConfirmNewPassword", "Text");
                    MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtConfirmNewPassword.Focus();
                    return;
                }
                else if (!txtNewPassword.Text.Equals(txtConfirmNewPassword.Text))
                {
                    sMsg = MyGlobal.GetLanguageString("Please make sure your passwords match.", "form", Name, "msg", "NoneMatchConfirmNewPassword", "Text");
                    MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNewPassword.Focus();
                    return;
                }
            }

            if (rdoDefaultPassword.Checked && MyGlobal.sMyDBConnectionPassword.Equals("ytec1688"))
            {
                //do nothing and close form
                _bClose = true;
                Close();
            }
            else if (rdoDefaultPassword.Checked && !MyGlobal.sMyDBConnectionPassword.Equals("ytec1688"))
            {
                //檢查是否有輸入舊密碼
                if (string.IsNullOrEmpty(txtOldPassword2.Text))
                {
                    sMsg = MyGlobal.GetLanguageString("Please enter your old custom password.", "form", Name, "msg", "NoneOldPassword", "Text");
                    MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOldPassword2.Focus();
                    return;
                }

                //檢查舊密碼是否輸入正確
                if (!txtOldPassword2.Text.Equals(txtOldPassword2.Tag.ToString()))
                {
                    //舊密碼不正確
                    sMsg = MyGlobal.GetLanguageString("The old password is incorrect. Try again.", "form", Name, "msg", "ErrorOldPassword", "Text");
                    MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOldPassword2.Focus();
                }
                else
                {
                    //密碼改成預設值
                    var sResult = DBCommon.ResetDBPassword(MyGlobal.sMyDBConnectionPassword, "", true);

                    if (string.IsNullOrEmpty(sResult))
                    {
                        //密碼變更成功
                        sMsg = MyGlobal.GetLanguageString("Your password has been changed.", "form", Name, "msg", "OKPasswordChanged1", "Text");
                        MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _bClose = true;
                        Close();
                    }
                    else
                    {
                        //密碼變更失敗
                        sMsg = MyGlobal.GetLanguageString("Password change failed!", "form", Name, "msg", "ErrorPasswordChanged", "Text") + "\r\n\r\n" + sResult;
                        MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else if (rdoCustomPassword.Checked)
            {
                //密碼由預設值改成自訂密碼
                var sResult = DBCommon.ResetDBPassword(MyGlobal.sMyDBConnectionPassword, txtCustomPassword.Text, false);

                if (string.IsNullOrEmpty(sResult))
                {
                    //密碼變更成功
                    sMsg = MyGlobal.GetLanguageString("Your password has been changed.", "form", Name, "msg", "OKPasswordChanged1", "Text") + "\r\n\r\n" + MyGlobal.GetLanguageString("Please use the new password to start JasonQuery next time.", "form", Name, "msg", "OKPasswordChanged2", "Text");
                    MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _bClose = true;
                    Close();
                }
                else
                {
                    //密碼變更失敗
                    sMsg = MyGlobal.GetLanguageString("Password change failed!", "form", Name, "msg", "ErrorPasswordChanged", "Text") + "\r\n\r\n" + sResult;
                    MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (rdoChangeCustomPassword.Checked)
            {
                //if (DBCommon.CheckDBPassword(txtOldPassword.Tag.ToString()))
                if (txtOldPassword.Text.Equals(txtOldPassword.Tag.ToString()))
                {
                    //變更自訂密碼
                    var sResult = DBCommon.ResetDBPassword(MyGlobal.sMyDBConnectionPassword, txtNewPassword.Text, false);

                    if (string.IsNullOrEmpty(sResult))
                    {
                        //密碼變更成功
                        sMsg = MyGlobal.GetLanguageString("Your password has been changed.", "form", Name, "msg", "OKPasswordChanged1", "Text") + "\r\n\r\n" + MyGlobal.GetLanguageString("Please use the new password to start JasonQuery next time.", "form", Name, "msg", "OKPasswordChanged2", "Text");
                        MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _bClose = true;
                        Close();
                    }
                    else
                    {
                        //密碼變更失敗
                        sMsg = MyGlobal.GetLanguageString("Password change failed!", "form", Name, "msg", "ErrorPasswordChanged", "Text") + "\r\n\r\n" + sResult;
                        MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    //舊密碼不正確
                    sMsg = MyGlobal.GetLanguageString("The old password is incorrect. Try again.", "form", Name, "msg", "ErrorOldPassword", "Text");
                    MessageBox.Show(sMsg, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOldPassword.Focus();
                }
            }
        }

        private void tmrNewPassword_Tick(object sender, EventArgs e)
        {
            txtNewPassword.Focus();
            tmrNewPassword.Enabled = false;
        }

        private void btnCustomPasswordView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                txtCustomPassword.PasswordChar = '\0';
            }
        }

        private void btnCustomPasswordView_MouseUp(object sender, MouseEventArgs e)
        {
            txtCustomPassword.PasswordChar = '*';
            txtCustomPassword.Focus();
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
    }
}