using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace JasonQuery
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            lblJasonQuery.Text = lblJasonQuery.Tag + @" " + (IntPtr.Size == 8 ? "(x64)" : "(x86)");
            lblVersion.Text = @"v" + MyGlobal.sMyVersion;
            lblVersion.Location = new Point(lblJasonQuery.Left + lblJasonQuery.Width, lblVersion.Top);
            txtSupportInfo.Text = MyGlobal.sSupportInfo;

            MyGlobal.ApplyLanguageInfo(this, false);

            txtTemp.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lnkIcons8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkIcons8.LinkVisited = true;
            Process.Start("https://icons8.com/icons");
        }

        private void lnkFreeFont1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkFreeFont1.LinkVisited = true;
            Process.Start("https://www.facebook.com/groups/549661292148791");
        }

        private void lnkFreeFont2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkFreeFont2.LinkVisited = true;
            Process.Start("https://github.com/jasonhandwriting/JasonHandwriting");
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
