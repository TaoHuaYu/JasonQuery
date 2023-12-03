using System;
using System.Windows.Forms;

namespace JasonQuery
{
    public partial class frmProgressInfo : Form
    {
        private ContextMenuStrip cMenu = new ContextMenuStrip();
        public string sTitleName { get; set; }
        public int iTotalQty { get; set; }
        public int iInterval { get; set; }

        public frmProgressInfo()
        {
            InitializeComponent();
        }

        private void frmProgressInfo_Load(object sender, EventArgs e)
        {
            MyGlobal.ApplyLanguageInfo(this, false);

            if (iInterval == 0)
            {
                iInterval = 200;
            }

            tmrProgress.Interval = iInterval;
            tmrProgress.Enabled = true;

            Application.UseWaitCursor = true;

            Text = sTitleName;
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = iTotalQty;

            Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MyGlobal.bProgressCancel = true;
            Close();
        }

        private void tmrSaveAsInsertInto_Tick(object sender, EventArgs e)
        {
            lblProgress.Text = MyGlobal.iProgressInsertInto.ToString() + @" / " + iTotalQty.ToString();
            progressBar1.Value = MyGlobal.iProgressInsertInto;
            Application.DoEvents();
            Refresh();

            if (MyGlobal.iProgressInsertInto >= iTotalQty)
            {
                Close();
            }
        }

        private void frmProgressInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.UseWaitCursor = false;
        }
    }
}