using System;
using System.Drawing;
using System.Windows.Forms;

namespace JasonQuery
{
    public sealed partial class frmInfo : Form
    {
        private string _sInfo = "", _sCaption = "";
        private bool _bMovingPosition = false;

        public string sInfo
        {
            set => _sInfo = value;
        }

        public string sCaption
        {
            set => _sCaption = value;
        }

        public bool bMovingPosition
        {
            set => _bMovingPosition = value;
        }

        public frmInfo()
        {
            InitializeComponent();
        }

        private void frmInfo_Load(object sender, EventArgs e)
        {
            Text = _sCaption;
            lblInfo.Text = _sInfo;
            lblInfo.Refresh();
            Application.DoEvents();

            if (_bMovingPosition)
            {
                Location = new Point(Left, Top - 250);
            }
        }

        private void tmrInfo_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MyGlobal.sSendMsgToInfo))
            {
                return;
            }

            lblInfo.Text = MyGlobal.sSendMsgToInfo;
            MyGlobal.sSendMsgToInfo = "";
            lblInfo.Refresh();
            Application.DoEvents();
        }
    }
}