using System;
using System.IO;
using System.Windows.Forms;

namespace JasonQuery
{
    public partial class frmInitialize : Form
    {
        private string _sLangText = "";

        public frmInitialize()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            txtTemp.Focus();

            MyGlobal.SetC1ComboBoxItemsFromDictionary(cboLocalization, MyGlobal.dicLocalization, true);
            cboLocalization.Text = MyGlobal.sLocalization;
            cboLocalization.Tag = MyGlobal.sLocalization;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
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
            MyGlobal.ApplyLanguageInfo(this); //cboLocalization_SelectedIndexChanged
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
    }
}