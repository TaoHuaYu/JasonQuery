using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace JasonQuery
{
    public sealed partial class frmFileSplitter : Form
    {
        private string _sLangText = "";
        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public frmFileSplitter()
        {
            InitializeComponent();
        }

        private void frmFileSplitter_Load(object sender, EventArgs e)
        {
            cboSizeLimit.SelectedIndex = 0;

            MyGlobal.ApplyLanguageInfo(this, false);

            //20190925 針對 TextBox, 顯示 Hint 功能 (只是顯示，不會改變 .Text 值)
            var sOriginalFile = MyGlobal.GetLanguageString("drag file here!", "form", Name, "object", "txtOriginalFile", "ToolTipText");
            var sDestinationFolder = MyGlobal.GetLanguageString("drag folder here!", "form", Name, "object", "txtDestinationFolder", "ToolTipText");
            SendMessage(txtOriginalFile.Handle, EM_SETCUEBANNER, 0, sOriginalFile);
            SendMessage(txtDestinationFolder.Handle, EM_SETCUEBANNER, 0, sDestinationFolder);
        }

        private void txtSourceFile_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            var a = (Array)e.Data.GetData(DataFormats.FileDrop);

            if (a == null)
            {
                return;
            }

            if (File.Exists(a.GetValue(0).ToString()))
            {
                txtOriginalFile.Text = a.GetValue(0).ToString();
                var fi = new FileInfo(a.GetValue(0).ToString());
                lblFileSize.Text = fi.Length.ToString("N0") + @" Bytes";
            }
            else
            {
                txtOriginalFile.Text = "";
                lblFileSize0.Text = "";
                lblFileSize.Text = "";
            }
        }

        private void txtSourceFile_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            var of = new OpenFileDialog {Multiselect = false};

            _sLangText = MyGlobal.GetLanguageString("Select a large text file...", "form", Name, "msg", "SelectLargeFile", "Text");
            of.Title = _sLangText;
            //of.InitialDirectory = "C:\\Users\\Administrators\\Desktop";
            of.Filter = @"Query files (*.sql)|*.sql|All files (*.*)|*.*";

            if (of.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var sFilename = of.FileName;

            if (File.Exists(sFilename))
            {
                txtOriginalFile.Text = sFilename;
                var fi = new FileInfo(sFilename);
                lblFileSize.Text = fi.Length.ToString("N0") + @" Bytes";
            }
            else
            {
                txtOriginalFile.Text = "";
                lblFileSize0.Text = "";
                lblFileSize.Text = "";
            }
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var vBrowseFolder = new FolderBrowserDialog())
            {
                vBrowseFolder.ShowNewFolderButton = true;
                _sLangText = MyGlobal.GetLanguageString("Select Destination Folder...", "form", Name, "msg", "SelectDestinationFolder", "Text");
                vBrowseFolder.Description = _sLangText;
                var result = vBrowseFolder.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(vBrowseFolder.SelectedPath))
                {
                    txtDestinationFolder.Text = vBrowseFolder.SelectedPath + (vBrowseFolder.SelectedPath.Substring(vBrowseFolder.SelectedPath.Length - 1, 1) == @"\" ? "" : @"\");
                }
            }
        }

        private void txtDestinationFolder_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            var a = (Array)e.Data.GetData(DataFormats.FileDrop);

            if (a == null)
            {
                return;
            }

            //判斷是否為目錄
            if (Directory.Exists(a.GetValue(0).ToString()))
            {
                txtDestinationFolder.Text = a.GetValue(0) + (a.GetValue(0).ToString().Substring(a.GetValue(0).ToString().Length - 1, 1) == @"\" ? "" : @"\");
            }
            else
            {
                txtDestinationFolder.Text = "";
            }
        }

        private void txtDestinationFolder_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSplitSize_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSplitSize_Leave(object sender, EventArgs e)
        {
            int.TryParse(txtSplitSize.Text, out var iSize);

            txtSplitSize.Text = iSize > 0 ? iSize.ToString() : "";
        }

        private string CheckLargeFileContent(string sFilename)
        {
            var sEndOfLineStyle = "";

            var sEncode = MyTextEncode.GetTextEncode(sFilename, ref sEndOfLineStyle);

            if (sEncode == "ERROR") //指定的檔案被鎖定，開啟失敗
            {
                return "ERROR";
            }

            if (!sEncode.StartsWith("Binary"))
            {
                return sEncode;
            }

            _sLangText = MyGlobal.GetLanguageString("It seems to be a binary file.", "form", Name, "msg", "SeemsBinaryFile", "Text");
            var sTemp = _sLangText + "\r\n\r\n";

            if (sEncode.Length > 6) //可辨識的 binary file
            {
                var sTemp2 = MyGlobal.GetLanguageString("It seems to be a", "form", Name, "msg", "SeemsBeA", "Text");
                var sTemp3 = MyGlobal.GetLanguageString("file.", "form", Name, "msg", "File.", "Text");
                sTemp = sTemp2 + " " + sEncode.Substring(7, sEncode.Length - 7) + " " + sTemp3 + "\r\n\r\n";
            }

            _sLangText = MyGlobal.GetLanguageString("The following file could not be opened because it contains characters that could not be interpreted.", "form", Name, "msg", "FileContainsBinaryData", "Text");
            MessageBox.Show(_sLangText + "\r\n\r\n" + sTemp + sFilename, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return "ERROR";
        }

        private bool CheckSplitCondition()
        {
            var sFileSizeLimit = "000000";

            if (string.IsNullOrEmpty(txtOriginalFile.Text) || !File.Exists(txtOriginalFile.Text))
            {
                _sLangText = MyGlobal.GetLanguageString("Please select an existing source file.", "form", Name, "msg", "SelectSource", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnBrowseFile.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDestinationFolder.Text))
            {
                _sLangText = MyGlobal.GetLanguageString("You must indicate a destination folder for your piece files.", "form", Name, "msg", "SelectFolder", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnBrowseFolder.Focus();
                return false;
            }

            if (!Directory.Exists(txtDestinationFolder.Text))
            {
                _sLangText = MyGlobal.GetLanguageString("Please select an existing destination folder.", "form", Name, "msg", "SelectExistingFolder", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnBrowseFolder.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtSplitSize.Text))
            {
                _sLangText = MyGlobal.GetLanguageString("Please define the size of a piece file.", "form", Name, "msg", "DefineSize", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSplitSize.Focus();
                return false;
            }

            if (cboSizeLimit.Text == @"KBytes")
            {
                sFileSizeLimit = "000";
            }

            if (!(Convert.ToDouble(lblFileSize.Text.Replace(",", "").Replace(" Bytes", "")) < Convert.ToDouble(txtSplitSize.Text + sFileSizeLimit))) return true;

            _sLangText = MyGlobal.GetLanguageString("Please decrease the size of blocked pieces so it is lower than the size of the original file.", "form", Name, "msg", "DecreaseSize", "Text");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            txtSplitSize.Focus();
            return false;
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            var lFileSize = 0;
            var iFileIndex = 1;
            var iFileSizeLimit = Convert.ToInt32(txtSplitSize.Text + "000000");
            var list = new List<string>();
            var sFilename = txtOriginalFile.Text;

            if (!CheckSplitCondition())
            {
                return;
            }

            //確認檔案格式、編碼
            var sEncode = CheckLargeFileContent(sFilename);

            //檔案大小
            if (cboSizeLimit.Text == @"KBytes")
            {
                iFileSizeLimit = Convert.ToInt32(txtSplitSize.Text + "000");
            }

            //檔名
            var sFilenameWithoutExtension = Path.GetFileNameWithoutExtension(sFilename);
            var sFilenameExtension = sFilename.Replace(Path.GetDirectoryName(sFilename) ?? string.Empty, "").Replace(sFilenameWithoutExtension, "").Replace(@"\", "");

            //計算分割檔的預計數量 Math.Ceiling(target);
            var iFileMaxIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lblFileSize.Text.Replace(",", "").Replace(" Bytes", "")) / Convert.ToDouble(iFileSizeLimit.ToString())).ToString());
            
            if (sEncode == "ERROR")
            {
                return;
            }

            //確認是否已經有存在已分割過的檔案名稱 (是否要繼續？)
            string sTemp;
            string sTemp2;

            foreach (var file in Directory.GetFiles(txtDestinationFolder.Text))
            {
                sTemp = file;
                int.TryParse(Path.GetFileNameWithoutExtension(sTemp).Replace(sFilenameWithoutExtension, "").Replace(".", ""), out var iTemp);

                if (iTemp <= 0 || iTemp >= iFileMaxIndex)
                {
                    continue;
                }

                _sLangText = MyGlobal.GetLanguageString("Parts of the file have been found in the destination folder.", "form", Name, "msg", "FileBeenFound", "Text");
                sTemp2 = MyGlobal.GetLanguageString("Some of the them will be deleted!", "form", Name, "msg", "SomeWillBeDelete", "Text");
                var sTemp3 = MyGlobal.GetLanguageString("Continue anyway?", "form", Name, "msg", "ContinueAnyway", "Text");
                var result0 = MessageBox.Show(_sLangText + "\r\n" + sTemp2 + "\r\n\r\n" + sTemp3, @"JasonQuery", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result0 == DialogResult.No)
                {
                    return;
                }

                break;
            }

            Cursor = Cursors.WaitCursor;
            progressBar1.Visible = true;
            progressBar1.Value = 0;

            try
            {
                using (var streamReader = new FileStream(sFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    string[] lines;
                    string sContents;
                    var iBase = 1;

                    int.TryParse(streamReader.Length.ToString(), out var iFileLength);

                    if (iFileLength == 0) //超過 int 限制
                    {
                        iBase = 500;
                        int.TryParse((streamReader.Length / iBase).ToString(), out iFileLength);
                    }

                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = iFileLength;

                    iFileLength = 0;

                    using (var reader = new StreamReader(streamReader))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            list.Add(line);

                            lFileSize += line.Length;
                            iFileLength += (int)Math.Truncate((double)(line.Length / iBase));

                            progressBar1.Value = iFileLength;

                            if (lFileSize <= iFileSizeLimit)
                            {
                                continue;
                            }

                            lines = list.ToArray();
                            sContents = string.Join("\r\n", lines);

                            sTemp = txtDestinationFolder.Text + sFilenameWithoutExtension + "." + iFileIndex.ToString("D" + iFileMaxIndex.ToString().Length) + sFilenameExtension;

                            try
                            {
                                if (File.Exists(sTemp))
                                {
                                    File.Delete(sTemp);
                                }

                                TextEngine.WriteContentToFile(sContents, sTemp, TextEncode.UTF8);
                            }
                            catch (Exception)
                            {
                                _sLangText = MyGlobal.GetLanguageString("Cannot access the file below because it is being used by another process.", "form", Name, "msg", "UsedByAnother", "Text");
                                sTemp2 = MyGlobal.GetLanguageString("Skip this file and continue anyway?", "form", Name, "msg", "SkipAndContinue", "Text");
                                var result1 = MessageBox.Show(_sLangText + "\r\n\r\n" + sTemp + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (result1 == DialogResult.No)
                                {
                                    return;
                                }
                            }

                            //還原初始值，重新產生下一個檔案
                            lFileSize = 0;
                            iFileIndex++;
                            list = new List<string>();
                        }
                    }

                    //最後未超過 iFileLimit 的部份，要再寫入檔案
                    lines = list.ToArray();
                    sContents = string.Join("\r\n", lines);

                    sTemp = txtDestinationFolder.Text + sFilenameWithoutExtension + "." + iFileIndex.ToString("D" + iFileMaxIndex.ToString().Length) + sFilenameExtension;

                    try
                    {
                        if (File.Exists(sTemp))
                        {
                            File.Delete(sTemp);
                        }

                        TextEngine.WriteContentToFile(sContents, sTemp, TextEncode.UTF8);
                    }
                    catch (Exception)
                    {
                        _sLangText = MyGlobal.GetLanguageString("Cannot access the file below because it is being used by another process.", "form", Name, "msg", "UsedByAnother", "Text");
                        sTemp2 = MyGlobal.GetLanguageString("Skip this file and continue anyway?", "form", Name, "msg", "SkipAndContinue", "Text");
                        var result2 = MessageBox.Show(_sLangText + "\r\n\r\n" + sTemp + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result2 == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
            }
            finally
            {
                progressBar1.Visible = false;
            }

            Cursor = Cursors.Default;

            _sLangText = MyGlobal.GetLanguageString("Splitting completed.", "form", Name, "msg", "SplittingCompleted", "Text");
            sTemp2 = MyGlobal.GetLanguageString("Would you like to open the destination folder?", "form", Name, "msg", "OpenDestinationFolder", "Text");
            var result = MessageBox.Show(_sLangText + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result != DialogResult.Yes)
            {
                return;
            }

            sTemp = txtDestinationFolder.Text + sFilenameWithoutExtension + "." + 1.ToString("D" + iFileMaxIndex.ToString().Length) + sFilenameExtension;

            if (File.Exists(sTemp))
            {
                sTemp = "/select, " + sTemp;
                Process.Start("explorer.exe", sTemp); //自動移至指定的路徑下的指定檔案
            }
            else
            {
                _sLangText = MyGlobal.GetLanguageString("The following file could not be found.", "form", Name, "msg", "FollowingFileNotFound", "Text");
                sTemp2 = MyGlobal.GetLanguageString("Please check the file name and try again.", "form", Name, "msg", "CheckFileAndTry", "Text");
                MessageBox.Show(_sLangText + "\r\n" + sTemp2 + "\r\n\r\n" + sTemp, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}