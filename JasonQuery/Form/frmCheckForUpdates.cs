using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.ComponentModel;
using C1.Win.C1Input;

namespace JasonQuery
{
    public sealed partial class frmCheckForUpdates : Form
    {
        private readonly frmInfo myForm = new frmInfo();
        private int _iDownloadProgressValue;
        private int _iDownloadStatus = -2; //-1=Continue, 0=Cancel, 1=OK
        private readonly C1.Win.C1Themes.C1ThemeController c1ThemeController1 = new C1.Win.C1Themes.C1ThemeController();

        public bool bCheckOnStartup { get; set; }

        public frmCheckForUpdates()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Hide();

            MyGlobal.ApplyLanguageInfo(this, false); //frmCheckForUpdates_Load

            var toolTip1 = new ToolTip {AutoPopDelay = 5000};
            var sLangText = MyGlobal.GetLanguageString("Open the url with your default browser", "Global", "Global", "msg", "OpenWithBrowser", "Text");
            toolTip1.SetToolTip(lnkDownloadWithoutSetting1, sLangText);

            sLangText = MyGlobal.GetLanguageString("Use JasonQuery to download the file directly", "Global", "Global", "msg", "DownloadWithJasonQuery", "Text");
            toolTip1.SetToolTip(lnkDownloadWithoutSetting2, sLangText);

            sLangText = MyGlobal.GetLanguageString("How to update manually", "Global", "Global", "msg", "btnHelp_HowToUpdate", "ToolTipText");
            toolTip1.SetToolTip(btnHelp_HowToUpdate, sLangText);

            lblLength.Text = grpDownloadInfo.Text;
            grpDownloadInfo.Text += @"   ";
            btnHelp_HowToUpdate.Location = new Point(grpDownloadInfo.Left + lblLength.Width - 8, btnHelp_HowToUpdate.Top);

            //btnHelp.Location = new Point(lblDownloadInfoWithout.Left + lblDownloadInfoWithout.Width + 3, btnHelp.Top);
            lnkDownloadWithoutSetting1.Location = new Point(lblWithout1.Left + lblWithout1.Width + 1, lnkDownloadWithoutSetting1.Top);
            lnkDownloadWithoutSetting2.Location = new Point(lblWithout2.Left + lblWithout2.Width + 1, lnkDownloadWithoutSetting2.Top);

            lblRecommended.Location = new Point(lnkDownloadWithoutSetting2.Left + lnkDownloadWithoutSetting2.Width + 1, lblRecommended.Top);
            btnHelp_000WebHost.Location = new Point(lblRecommended.Left + lblRecommended.Width + 3, btnHelp_000WebHost.Top);

            if (bCheckOnStartup)
            {
                //顯示 frmInfo
                myForm.TopMost = true;

                var sMsg = MyGlobal.GetLanguageString("Check for Updates", "form", Name, "msg", "CheckforUpdatesTitle", "Text");
                myForm.sCaption = sMsg;
                sMsg = MyGlobal.GetLanguageString("check for updates on startup...", "form", Name, "msg", "CheckforUpdatesInfo", "Text");
                myForm.sInfo = sMsg;
                myForm.StartPosition = FormStartPosition.CenterScreen;
                myForm.Show();

                CheckForUpdates1(); //frmCheckForUpdates_Load
            }
            else
            {
                Show();
            }
        }

        private void btnCheckForUpdates_Click(object sender, EventArgs e)
        {
            CheckForUpdates1(); //btnCheckForUpdates_Click
        }

        private void CheckForUpdates1()
        {
            string sLangText;

            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            var sTargetFilename = Path.GetFileName(Path.GetTempFileName()).Replace(".", "") + "_jq_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"; //Path.Combine(Path.GetTempPath(), Path.GetFileName(Path.GetTempFileName()).Replace(".", "")) + "_jq.txt";
            const string sAddress = "https://jasonquery.000webhostapp.com/";
            const string sFilename = "jq.txt";
            var sResult = CheckForUpdates2(MyGlobal.sMyVersion, sAddress, sFilename, sTargetFilename);

            btnCheckForUpdates.Visible = false;
            lblInfo.Visible = true;
            lblInfo2.Visible = true;

            myForm.Hide();
            myForm.Close();

            if (!string.IsNullOrEmpty(sResult))
            {
                sLangText = MyGlobal.GetLanguageString("A new version of JasonQuery is available:", "form", Name, "object", "lblInfoHasNewVersion", "Text");
                lblInfo.Text = sLangText;
                lblInfo2.Text = sResult;

                lblInfo.ForeColor = Color.DarkGreen;
                lblInfo2.ForeColor = Color.DarkGreen;

                if (bCheckOnStartup)
                {
                    Show();
                }
            }
            else
            {
                if (bCheckOnStartup)
                {
                    Cursor = Cursors.Default;

                    //先 Hide() 再 Dispose()，frmCheckForUpdates 不會有感覺，很順！
                    Hide();
                    Dispose();
                }

                sLangText = MyGlobal.GetLanguageString("You are using the latest version of JasonQuery.", "form", Name, "object", "lblInfoLatest", "Text");
                lblInfo.Text = sLangText;
                lblInfo2.Text = MyGlobal.sMyVersion;
            }

            Cursor = Cursors.Default;
        }

        private static string CheckForUpdates2(string myVers, string sAddress, string sFilename, string sTargetFilename)
        {
            var info = CheckForUpdates.GetUpdateInfo(sAddress, sFilename, Path.GetTempPath(), sTargetFilename);
            double.TryParse(info, out var dInfo);

            try
            {
                File.Delete(Path.GetTempPath() + sTargetFilename);
            }
            catch (Exception)
            {
                //
            }

            double.TryParse(myVers, out var dExeInfo);

            return dExeInfo < dInfo ? info : "";
        }

        private void lnkCheck1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkCheck1.LinkVisited = true;
            Process.Start("https://jasonquery.000webhostapp.com/jq.txt");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lnkDownloadWithoutSetting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkDownloadWithoutSetting1.LinkVisited = true;
            Process.Start("https://jasonquery.000webhostapp.com/JasonQuery/JasonQuery86.zip");
        }

        private void linkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var lnk = sender as LinkLabel;

            lnk.LinkVisited = true;

            const string sURL = "https://jasonquery.000webhostapp.com/JasonQuery/";
            var sFilename = lnk.Tag.ToString();

            var sf = new SaveFileDialog();
            var sTemp = MyGlobal.GetLanguageString("Save As", "Global", "Global", "msg", "SaveAs", "Text");
            sf.Title = sTemp;
            sf.FileName = sFilename;
            sf.Filter = @"All files (*.*)|*.*";

            if (sf.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Refresh();

            if (string.IsNullOrEmpty(Path.GetExtension(sf.FileName)))
            {
                sf.FileName += ".7z";
            }

            var sTargetFilename = Path.GetFileName(sf.FileName);
            var sTargetFolder = Path.GetDirectoryName(sf.FileName);

            sTargetFolder = sTargetFolder + (sTargetFolder.Substring(sTargetFolder.Length - 1, 1) == "\\" ? "" : "\\");

            sTemp = MyGlobal.GetLanguageString("Downloading update, please wait!", "form", Name, "msg", "DownloadUpdate2", "Text");
            ShowDownloadInfo(sTemp, sURL, sFilename, sTargetFolder, sTargetFilename);
        }

        private void ShowDownloadInfo(string sPromptText, string sURL, string sFilename, string sTargetFolder, string sTargetFilename)
        {
            var wc = new WebClient();
            var form = new Form();
            var lblPromptText = new Label();
            var txtTemp = new TextBox();
            var btnCancel = new C1Button();
            var pbDownloadStatus = new ProgressBar();

            form.ControlBox = false;
            form.Text = MyGlobal.GetLanguageString("Downloading Update", "form", Name, "msg", "DownloadUpdate1", "Text"); ;
            form.ClientSize = new Size(396, 162);
            form.Controls.AddRange(new Control[] { lblPromptText, btnCancel, txtTemp, pbDownloadStatus });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.Font = new Font("Microsoft JhengHei", 9F, FontStyle.Regular, GraphicsUnit.Point, 136); //微軟正黑體

            var sTemp = MyGlobal.GetLanguageString("From", "form", Name, "msg", "From", "Text");
            var sTemp2 = MyGlobal.GetLanguageString("To", "form", Name, "msg", "To", "Text");
            lblPromptText.Text = sPromptText + "\r\n\r\n" + sTemp + @": " + sURL + sFilename + "\r\n" + sTemp2 + @": " + sTargetFolder + sTargetFilename;
            lblPromptText.SetBounds(14, 15, 372, 13);
            lblPromptText.AutoSize = true;

            sTemp = MyGlobal.GetLanguageString("&Cancel", "Global", "Global", "messagebox", "Cancel", "Text");
            btnCancel.Text = sTemp;            
            btnCancel.Click += delegate
            {
                wc.CancelAsync();
            };

            c1ThemeController1.SetTheme(btnCancel, "(default)");
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.VisualStyleBaseStyle = VisualStyle.Office2010Blue;

            txtTemp.SetBounds(-50, 0, 30, 30);

            form.ClientSize = new Size(lblPromptText.Right + 20, form.ClientSize.Height);
            form.StartPosition = FormStartPosition.CenterScreen;

            pbDownloadStatus.Location = new Point(17, 93);
            pbDownloadStatus.Size = new Size(form.Width - 52, 16);

            btnCancel.SetBounds(form.Width - btnCancel.Width - 33, 120, 74, 30);

            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Show();
            form.Refresh();
            txtTemp.Focus(); //讓取消鈕不要是唯一的 Focus

            _iDownloadProgressValue = 0;
            _iDownloadStatus = -1;
            Cursor = Cursors.WaitCursor;

            using (wc)
            {
                wc.DownloadFileCompleted += wc_Completed;
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;

                try
                {
                    wc.DownloadFileAsync(new Uri(sURL + sFilename), sTargetFolder + sTargetFilename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            while (true)
            {
                Application.DoEvents();

                pbDownloadStatus.Value = _iDownloadProgressValue;

                if (_iDownloadStatus != -1)
                {
                    break;
                }
            }

            Cursor = Cursors.Default;

            if (_iDownloadStatus == 0)
            {
                //取消下載，刪除檔案
                try
                {
                    if (File.Exists(sTargetFolder + sTargetFilename))
                    {
                        File.Delete(sTargetFolder + sTargetFilename);
                    }
                }
                catch (Exception)
                {
                    //
                }
            }
            else
            {
                if (File.Exists(sTargetFolder + sTargetFilename) == false)
                {
                    sTemp = Path.GetDirectoryName(sTargetFolder + sTargetFilename);
                }
                else
                {
                    sTemp = "/select, " + sTargetFolder + sTargetFilename;
                }

                Process.Start("explorer.exe", sTemp); //自動移至指定的路徑下的指定檔案

                System.Threading.Thread.Sleep(1000);
                btnHelp_HowToUpdate.PerformClick();
            }

            form.Close();
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            _iDownloadProgressValue = e.ProgressPercentage;
        }

        private void wc_Completed(object sender, AsyncCompletedEventArgs e)
        {
            _iDownloadStatus = e.Cancelled ? 0 : 1;
        }

        private void btnHelp_HowToUpdate_Click(object sender, EventArgs e)
        {
            var sTemp0 = MyGlobal.GetLanguageString("How to manually update JasonQuery to the latest version:", "form", Name, "msg", "HowToUpdate0", "Text") + "\r\n\r\n";
            var sTemp1 = MyGlobal.GetLanguageString("1. Download the latest JasonQuery.7z", "form", Name, "msg", "HowToUpdate1", "Text") + "\r\n";
            var sTemp2 = MyGlobal.GetLanguageString("2. Close all open JasonQuery", "form", Name, "msg", "HowToUpdate2", "Text") + "\r\n";
            var sTemp3 = MyGlobal.GetLanguageString("3. Unzip \"JasonQuery x64\" or \"JasonQuery x86\" to the folder where JasonQuery is currently located (overwrite all, JasonQuery.7z does not include JasonQuery.db)", "form", Name, "msg", "HowToUpdate3", "Text") + "\r\n";
            var sTemp4 = MyGlobal.GetLanguageString("4. Run JasonQuery", "form", Name, "msg", "HowToUpdate4", "Text");
            MessageBox.Show(sTemp0 + sTemp1 + sTemp2 + sTemp3 + sTemp4, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHelp_000WebHost_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The download link of \"000WebHost\"\r\n\r\n" + "https://jasonquery.000webhostapp.com/JasonQuery/JasonQuery64.zip", "JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdateNow_Click(object sender, EventArgs e)
        {
            var sExecuteName = Application.StartupPath + @"\Updater.exe";

            if (File.Exists(sExecuteName))
            {
                var infoExe = new ProcessStartInfo
                {
                    FileName = sExecuteName,
                    WorkingDirectory = Application.StartupPath + @"\",
                    Arguments = MyGlobal.sLocalizationCode + "|" + MyGlobal.sXmlFilename,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };

                Process.Start(infoExe);

                Close();
            }
            else
            {
                var sTemp = MyGlobal.GetLanguageString("File not found:", "form", Name, "msg", "UpdaterNotFound", "Text");
                MessageBox.Show(sTemp + "\r\n\r\n" + sExecuteName, "JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}