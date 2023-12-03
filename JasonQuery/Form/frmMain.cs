using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using JasonLibrary;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;
using System.Globalization;
using System.Linq;
using JasonLibrary.Class;
using HookManager = JasonLibrary.Hotkeys.HookManager;
using TabPage = Crownwood.Magic.Controls.TabPage;

namespace JasonQuery
{
    public partial class MainForm : Form
    {
        private frmQuery _f1;
        private frmSchemaBrowser _f2;
        private frmSQLHistory _f3;
        private frmOptions _f4;
        private ContextMenu _cMenu = new ContextMenu();
        private readonly ToolTip _toolTip1 = new ToolTip();
        private string _sTabToolTip = "";
        private string _sMenuItems = "";
        private string _sLangText = "";
        private bool _bMenuEnable = true;
        private int _iChangeFormSizeManually;
        private int _iConnectionFormChangeLocalization = -1;
        private readonly Queue<string> _mruList = new Queue<string>();
        private List<string> _lstGridHeader = new List<string>();
        private int _iMouseMove = -1;
        private DataTable _dtDatabase;

        private enum eCol
        {
            Close = 0,
            Dash1,
            NewSQLEditor,
            Dash2,
            AddToMyFavorite,
            RemoveFromMyFavorite,
            Dash3,
            OpenFolder,
            Dash4,
            CopyFullFilePath,
            CopyFilename,
            CopyCurrentPath
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(IntPtr classname, string title);

        [DllImport("user32.dll")]
        private static extern void MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool rePaint);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect (IntPtr hwnd, out Rectangle rect);

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int keyCode);

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            UpdateKeys(); //MainForm_Activated
        }

        private void ApplyLocalization()
        {
            MessageBoxManager.Unregister();

            _sLangText = MyGlobal.GetLanguageString("&OK", "Global", "Global", "messagebox", "OK", "Text");
            MessageBoxManager.OK = _sLangText;
            _sLangText = MyGlobal.GetLanguageString("&Cancel", "Global", "Global", "messagebox", "Cancel", "Text");
            MessageBoxManager.Cancel = _sLangText;
            _sLangText = MyGlobal.GetLanguageString("&Abort", "Global", "Global", "messagebox", "Abort", "Text");
            MessageBoxManager.Abort = _sLangText;
            _sLangText = MyGlobal.GetLanguageString("&Retry", "Global", "Global", "messagebox", "Retry", "Text");
            MessageBoxManager.Retry = _sLangText;
            _sLangText = MyGlobal.GetLanguageString("&Ignore", "Global", "Global", "messagebox", "Ignore", "Text");
            MessageBoxManager.Ignore = _sLangText;
            _sLangText = MyGlobal.GetLanguageString("&Yes", "Global", "Global", "messagebox", "Yes", "Text");
            MessageBoxManager.Yes = _sLangText;
            _sLangText = MyGlobal.GetLanguageString("&No", "Global", "Global", "messagebox", "No", "Text");
            MessageBoxManager.No = _sLangText;
            MessageBoxManager.Register();

            const string sCheck3DotText = "`mnuNewConnection`mnuOptions`mnuSchemaBrowser`mnuSQLHistory`mnuFileSplitter`mnuCheckForUpdatesManually`mnuReleaseNotes`mnuReportBugs`mnuAbout`"; //後面要自動加上 ... 的功能表項目
            var myItems = GetItems(mnuMainForm);

            foreach (var item in myItems)
            {
                if (_sMenuItems.Contains("`" + item.Name + "`"))
                {
                    item.Enabled = _bMenuEnable;
                }
                else
                {
                    if ("`mnuMyFavorite`mnuRecentFiles`".Contains("`" + item.Name + "`"))
                    {
                        //由 LoadRecentList / LoadMyFavoriteFiles 控制即可
                        //item.Enabled = false;
                    }
                    else
                    {
                        item.Enabled = !_bMenuEnable;
                    }
                }

                if ("`mnuOptions`mnuSchemaBrowser`mnuSQLHistory`".Contains("`" + item.Name + "`"))
                {
                    //item.Enabled = !item.Checked;
                }

                item.Text = MyGlobal.GetLanguageString(item.Text, "form", Name, "menu", item.Name, "Text") + (sCheck3DotText.Contains("`" + item.Name + "`") ? "..." : "");
                item.ToolTipText = MyGlobal.GetLanguageString(item.ToolTipText, "form", Name, "menu", item.Name, "ToolTipText");

                switch (item.Name)
                {
                    case "mnuOptions":
                        MyGlobal.sNameOptions = item.Text.Replace("...", "");
                        break;
                    case "mnuSchemaBrowser":
                    {
                        MyGlobal.sNameSchemaBrowser = item.Text.Replace("...", "");
                        break;
                    }
                    case "mnuSQLHistory":
                        MyGlobal.sNameSQLHistory = item.Text.Replace("...", "");
                        break;
                }
            }

            //mnuInfo.Enabled = false;

            MyGlobal.dicLocalization = new Dictionary<string, string>();
            var sComboBoxString = MyGlobal.sLocalizationList.Split(new[] { "`" }, StringSplitOptions.None);

            foreach (var t in sComboBoxString)
            {
                MyGlobal.dicLocalization.Add(t.Split(';')[0], t.Split(';')[1]);
            }

            MyGlobal.dicWordWrapIndentMode = new Dictionary<string, string>();

            _sLangText = MyGlobal.GetLanguageString("Fixed", "form", "frmOptions", "dropdownlist", "IndentMode_Fixed", "Text");
            MyGlobal.dicWordWrapIndentMode.Add("Fixed", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Same", "form", "frmOptions", "dropdownlist", "IndentMode_Same", "Text");
            MyGlobal.dicWordWrapIndentMode.Add("Same", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Indent", "form", "frmOptions", "dropdownlist", "IndentMode_Indent", "Text");
            MyGlobal.dicWordWrapIndentMode.Add("Indent", _sLangText);

            MyGlobal.dicMaxWidth = new Dictionary<string, string>();

            _sLangText = MyGlobal.GetLanguageString("Unlimited", "form", "frmOptions", "dropdownlist", "cboMaxWidth", "Text");
            MyGlobal.dicMaxWidth.Add("Unlimited", _sLangText);
            MyGlobal.dicMaxWidth.Add("500", "500");
            MyGlobal.dicMaxWidth.Add("1000", "1000");
            MyGlobal.dicMaxWidth.Add("1500", "1500");
            MyGlobal.dicMaxWidth.Add("2000", "2000");

            MyGlobal.dicRowSizing = new Dictionary<string, string>();

            _sLangText = MyGlobal.GetLanguageString("All Rows", "form", "frmOptions", "dropdownlist", "RowHeightResizing_AllRows", "Text");
            MyGlobal.dicRowSizing.Add("AllRows", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Individual Rows", "form", "frmOptions", "dropdownlist", "RowHeightResizing_IndividualRows", "Text");
            MyGlobal.dicRowSizing.Add("IndividualRows", _sLangText);

            MyGlobal.dicAutoDisconnect = new Dictionary<string, string>();

            _sLangText = MyGlobal.GetLanguageString("Never", "form", "frmOptions", "dropdownlist", "AutoDisconnect_Never", "Text");
            MyGlobal.dicAutoDisconnect.Add("Never", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("1 Hour", "form", "frmOptions", "dropdownlist", "AutoDisconnect_1hr", "Text");
            MyGlobal.dicAutoDisconnect.Add("1hr", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("3 Hours", "form", "frmOptions", "dropdownlist", "AutoDisconnect_3hrs", "Text");
            MyGlobal.dicAutoDisconnect.Add("3hrs", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("5 Hours", "form", "frmOptions", "dropdownlist", "AutoDisconnect_5hrs", "Text");
            MyGlobal.dicAutoDisconnect.Add("5hrs", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("7 Hours", "form", "frmOptions", "dropdownlist", "AutoDisconnect_7hrs", "Text");
            MyGlobal.dicAutoDisconnect.Add("7hrs", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("9 Hours", "form", "frmOptions", "dropdownlist", "AutoDisconnect_9hrs", "Text");
            MyGlobal.dicAutoDisconnect.Add("9hrs", _sLangText);

            MyGlobal.dicBookmarkStyle = new Dictionary<string, string>();

            _sLangText = MyGlobal.GetLanguageString("Arrow", "form", "frmOptions", "dropdownlist", "BookmarkStyle_Arrow", "Text");
            MyGlobal.dicBookmarkStyle.Add("Arrow", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Circle", "form", "frmOptions", "dropdownlist", "BookmarkStyle_Circle", "Text");
            MyGlobal.dicBookmarkStyle.Add("Circle", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("RoundRect", "form", "frmOptions", "dropdownlist", "BookmarkStyle_RoundRect", "Text");
            MyGlobal.dicBookmarkStyle.Add("RoundRect", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("ShortArrow", "form", "frmOptions", "dropdownlist", "BookmarkStyle_ShortArrow", "Text");
            MyGlobal.dicBookmarkStyle.Add("ShortArrow", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("SmallRect", "form", "frmOptions", "dropdownlist", "BookmarkStyle_SmallRect", "Text");
            MyGlobal.dicBookmarkStyle.Add("SmallRect", _sLangText);

            MyGlobal.dicCSVDelimiters = new Dictionary<string, string>();

            _sLangText = MyGlobal.GetLanguageString("Tab", "form", "frmExportToFile", "dropdownlist", "Delimiters_Tab", "Text");
            MyGlobal.dicCSVDelimiters.Add("Tab", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Semicolon", "form", "frmExportToFile", "dropdownlist", "Delimiters_Semicolon", "Text");
            MyGlobal.dicCSVDelimiters.Add("Semicolon", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Comma", "form", "frmExportToFile", "dropdownlist", "Delimiters_Comma", "Text");
            MyGlobal.dicCSVDelimiters.Add("Comma", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Space", "form", "frmExportToFile", "dropdownlist", "Delimiters_Space", "Text");
            MyGlobal.dicCSVDelimiters.Add("Space", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Colon", "form", "frmExportToFile", "dropdownlist", "Delimiters_Colon", "Text");
            MyGlobal.dicCSVDelimiters.Add("Colon", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Slash", "form", "frmExportToFile", "dropdownlist", "Delimiters_Slash", "Text");
            MyGlobal.dicCSVDelimiters.Add("Slash", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Backslash", "form", "frmExportToFile", "dropdownlist", "Delimiters_Backslash", "Text");
            MyGlobal.dicCSVDelimiters.Add("Backslash", _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Pipe", "form", "frmExportToFile", "dropdownlist", "Delimiters_Pipe", "Text");
            MyGlobal.dicCSVDelimiters.Add("Pipe", _sLangText);

            _lstGridHeader = new List<string>();

            _sLangText = MyGlobal.GetLanguageString("Close", "form", Name, "menu", "Close", "Text");
            _lstGridHeader.Add(_sLangText);
            _lstGridHeader.Add("-");
            _sLangText = MyGlobal.GetLanguageString("New SQL Editor", "form", Name, "menu", "NewSQLEditor", "Text");
            _lstGridHeader.Add(_sLangText);
            _lstGridHeader.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Add to \"My Favorite\"", "form", Name, "menu", "AddToMyFavorite", "Text");
            _lstGridHeader.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Remove from \"My Favorite\"", "form", Name, "menu", "RemoveFromMyFavorite", "Text");
            _lstGridHeader.Add(_sLangText);
            _lstGridHeader.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Open Containing Folder in Explorer", "form", Name, "menu", "OpenFolder", "Text");
            _lstGridHeader.Add(_sLangText);
            _lstGridHeader.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Copy Full File Path to Clipboard", "form", Name, "menu", "CopyFullFilePath", "Text");
            _lstGridHeader.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Copy Filename to Clipboard", "form", Name, "menu", "CopyFilename", "Text");
            _lstGridHeader.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Copy Current Dir. Path to Clipboard", "form", Name, "menu", "CopyCurrentPath", "Text");
            _lstGridHeader.Add(_sLangText);

            _cMenu = new ContextMenu();
            _cMenu.MenuItems.Add(_lstGridHeader[(int)eCol.Close], CloseIt);
            _cMenu.MenuItems.Add("-");
            _cMenu.MenuItems.Add(_lstGridHeader[(int)eCol.NewSQLEditor], NewSqlEditor);
            _cMenu.MenuItems.Add("-");
            _cMenu.MenuItems.Add(_lstGridHeader[(int)eCol.AddToMyFavorite], AddToMyFavoriteFiles);
            _cMenu.MenuItems.Add(_lstGridHeader[(int)eCol.RemoveFromMyFavorite], RemoveFromMyFavoriteFiles);
            _cMenu.MenuItems.Add("-");
            _cMenu.MenuItems.Add(_lstGridHeader[(int)eCol.OpenFolder], OpenFolder);
            _cMenu.MenuItems.Add("-");
            _cMenu.MenuItems.Add(_lstGridHeader[(int)eCol.CopyFullFilePath], CopyFullFilePath);
            _cMenu.MenuItems.Add(_lstGridHeader[(int)eCol.CopyFilename], CopyFilename);
            _cMenu.MenuItems.Add(_lstGridHeader[(int)eCol.CopyCurrentPath], CopyCurrentPath);

            if (string.IsNullOrEmpty(MyGlobal.sTabBackColor))
            {
                return;
            }

            tabControl1.BackColor = ColorTranslator.FromHtml(MyGlobal.sTabBackColor);
            tabControl1.ForeColor = ColorTranslator.FromHtml(MyGlobal.sTabActiveForeColor);
            tabControl1.TextInactiveColor = ColorTranslator.FromHtml(MyGlobal.sTabInactiveForeColor);
            tabControl1.ShrinkPagesToFit = MyGlobal.bTabShrinkPages;
            tabControl1.ShowArrows = MyGlobal.bTabShowArrows;
            tabControl1.HoverSelect = MyGlobal.bTabHoverSelect;
            tabControl1.Multiline = MyGlobal.bTabMultiLine;
            tabControl1.ShowClose = !mnuNewConnection.Enabled;

            tabControl1.Style = MyLibrary.sTabStyle == "IDE" ? Crownwood.Magic.Common.VisualStyle.IDE : Crownwood.Magic.Common.VisualStyle.Plain;

            switch (MyLibrary.sTabAppearance)
            {
                case "MultiDocument":
                    tabControl1.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiDocument;
                    break;
                case "MultiForm":
                    tabControl1.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiForm;
                    break;
                default:
                    tabControl1.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiBox;
                    break;
            }

            tabControl1.PositionTop = true;
            tabControl1.BoldSelectedPage = true;
            tabControl1.BorderStyle = BorderStyle.None;

            //20220802
            if (MyGlobal.sDataSource == "PostgreSQL")
            {
                btnAutoRollbackOnErrorOn.Text = MyGlobal.GetLanguageString("Auto Rollback on error", "form", Name, "object", "btnAutoRollbackOnError", "Text");
                btnAutoRollbackOnErrorOff.Text = MyGlobal.GetLanguageString("Auto Rollback on error", "form", Name, "object", "btnAutoRollbackOnError", "Text");

                if (MyGlobal.bDBAutoRollback)
                {
                    btnAutoRollbackOnErrorOn.Visible = true;
                }
                else
                {
                    btnAutoRollbackOnErrorOff.Visible = true;
                }

                spAutoRollbackOnError.Visible = true;
            }

            lblAutoCommit.Text = MyGlobal.GetLanguageString("Auto Commit is off", "form", Name, "object", "lblAutoCommit", "Text");
        }

        private static string CheckFileExists()
        {
            var sResult = "";

            if (File.Exists(Application.StartupPath + @"\C1.C1Excel.4.5.2.dll") == false)
            {
                sResult += "C1.C1Excel.4.5.2.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\C1.C1Zip.4.5.2.dll") == false)
            {
                sResult += "C1.C1Zip.4.5.2.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\C1.Win.4.5.2.dll") == false)
            {
                sResult += "C1.Win.4.5.2.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\C1.Win.Bitmap.4.5.2.dll") == false)
            {
                sResult += "C1.Win.Bitmap.4.5.2.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\C1.Win.C1Command.4.5.2.dll") == false)
            {
                sResult += "C1.Win.C1Command.4.5.2.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\C1.Win.C1DX.4.5.2.dll") == false)
            {
                sResult += "C1.Win.C1DX.4.5.2.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\C1.Win.C1Input.4.5.2.dll") == false)
            {
                sResult += "C1.Win.C1Input.4.5.2.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\C1.Win.C1Ribbon.4.5.2.dll") == false)
            {
                sResult += "C1.Win.C1Ribbon.4.5.2.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\C1.Win.C1Themes.4.5.2.dll") == false)
            {
                sResult += "C1.Win.C1Themes.4.5.2.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\C1.Win.C1Themes.Extended.4.5.2.dll") == false)
            {
                sResult += "C1.Win.C1Themes.Extended.4.5.2.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\C1.Win.C1TrueDBGrid.4.5.2.dll") == false)
            {
                sResult += "C1.Win.C1TrueDBGrid.4.5.2.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\C1.Win.C1TrueDBGrid.Excel.4.5.2.dll") == false)
            {
                sResult += "C1.Win.C1TrueDBGrid.Excel.4.5.2.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\Devart.Data.dll") == false)
            {
                sResult += "Devart.Data.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\Devart.Data.Oracle.dll") == false)
            {
                sResult += "Devart.Data.Oracle.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\Devart.Data.PostgreSql.dll") == false)
            {
                sResult += "Devart.Data.PostgreSql.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\Devart.Data.SqlServer.dll") == false)
            {
                sResult += "Devart.Data.SqlServer.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\Interop.IWshRuntimeLibrary.dll") == false)
            {
                sResult += "Interop.IWshRuntimeLibrary.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\JasonLibrary.dll") == false)
            {
                sResult += "JasonLibrary.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\LinqBridge.dll") == false)
            {
                sResult += "LinqBridge.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\MagicLibrary.dll") == false)
            {
                sResult += "MagicLibrary.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\PoorMansTSqlFormatterLib.dll") == false)
            {
                sResult += "PoorMansTSqlFormatterLib.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\ScintillaNET.dll") == false)
            {
                sResult += "ScintillaNET.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\SQLite.Interop.dll") == false)
            {
                sResult += "SQLite.Interop.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\System.Data.SQLite.dll") == false)
            {
                sResult += "System.Data.SQLite.dll\r\n";
            }

            if (File.Exists(Application.StartupPath + @"\System.Threading.Tasks.Extensions.dll") == false)
            {
                sResult += "System.Threading.Tasks.Extensions.dll\r\n";
            }

            return sResult;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var sTemp = " is";
            var bCreateDBFile = false;

            if (File.Exists(Application.StartupPath + @"\JasonQuery.db") == false)
            {
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("JasonQuery.Files.JasonQuery.db");
                var fileStream = new FileStream("JasonQuery.db", FileMode.CreateNew);

                for (var i = 0; i < stream.Length; i++)
                {
                    fileStream.WriteByte((byte)stream.ReadByte());
                }

                fileStream.Close();

                bCreateDBFile = true;
            }

            var sNoneExistFileList = CheckFileExists();

            if (!string.IsNullOrEmpty(sNoneExistFileList))
            {
                if (sNoneExistFileList.Length - sNoneExistFileList.Replace("\r\n", "").Length == 2)
                {
                    sTemp = "s are"; //如果有換行符號，就表示有多個 dll 檔案找不到
                }

                _sLangText = "The program can't start because the following file{0} missing from your computer.\r\n\r\nTry reinstalling the program to fix this problem.";
                _sLangText = string.Format(_sLangText, sTemp);
                MessageBox.Show(_sLangText + "\r\n\r\n" + sNoneExistFileList, @"JasonQuery - System Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(Environment.ExitCode);
            }

            //新增語系時，此處要維護
            MyGlobal.sLocalizationList = "English;english.xml`Chinese (Traditional) - 中文繁體;chinese-cht.xml`Chinese (Simplified) - 中文简体;chinese-chs.xml";
            MyGlobal.sMDBFilename = Application.StartupPath + @"\JasonQuery.db";
            MyGlobal.sMyDBConnectionString = "Data Source=" + MyGlobal.sMDBFilename + ";Version=3;New=False;Compress=True;";

            var versInfo = FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule?.FileName);
            MyGlobal.sMyVersion = versInfo.FileVersion.Replace(".0.0", "");
            Text = Tag + (MyLibrary.bShowVersion == false ? "" : @" " + MyGlobal.sMyVersion) + @" " + (IntPtr.Size == 8 ? @"(x64)" : @"(x86)");

            if (DBCommon.CheckDBPassword(""))
            {
                //do nothing
            }
            else
            {
                MyGlobal.LoadLocalizationXML(); //MainForm_Load
                ApplyLocalization(); //MainForm_Load

                using (var myForm = new frmCustomPassword())
                {
                    myForm.ShowDialog();
                }
            }

            LoadGlobalSetting(); //MainForm_Load

            if (MyGlobal.bMainFormMaximized)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                if (bCreateDBFile)
                {
                    Location = (Point)new Size(400, 150);
                }
                else
                {
                    Location = (Point)new Size(MyGlobal.iMainFormLocationX, MyGlobal.iMainFormLocationY);
                }

                WindowState = FormWindowState.Normal;
                ClientSize = new Size(MyGlobal.iMainFormWidth - 16, MyGlobal.iMainFormHeight - 38);
            }

            MyGlobal.LoadLocalizationXML(); //MainForm_Load
            ApplyLocalization(); //MainForm_Load

            if (bCreateDBFile)
            {                
                using (var myForm = new frmInitialize())
                {
                    myForm.StartPosition = FormStartPosition.CenterScreen;
                    myForm.ShowDialog();
                }
            }

            #region 判斷是否要檢查新版本
            if (MyLibrary.bCheckForUpdate)
            {
                var bCheckUpdate = MyLibrary.iCheckForUpdate == 0 || VerifyCheckForUpdate("Query");

                if (bCheckUpdate)
                {
                    var myForm = new frmCheckForUpdates
                    {
                        bCheckOnStartup = true
                    };

                    myForm.ShowDialog();

                    VerifyCheckForUpdate("Update");
                }
            }
            #endregion

            HookManager.KeyDown += HookManager_KeyDown;

            lblDomainUser.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            btnIP.Text = MyGlobal.GetIPAddress();

            EnableCloseTabMenu(false); //MainForm_Load

            LoadConnectionForm(); //MainForm_Load

            //新增語系時，此處要維護
            switch (MyGlobal.sLocalization)
            {
                case "Chinese (Traditional) - 中文繁體":
                    _sLangText = "zh-TW";
                    break;
                case "Chinese (Simplified) - 中文简体":
                    _sLangText = "zh-CN";
                    break;
                default: //English
                    _sLangText = "en-US";
                    break;
            }

            MyGlobal.sLocalizationCode = _sLangText;

            var cultureInfo = new CultureInfo(_sLangText)
            {
                DateTimeFormat =
                {
                    ShortDatePattern = MyLibrary.sDateFormat,
                    ShortTimePattern = "HH:mm:ss",
                    LongDatePattern = MyLibrary.sDateFormat,
                    LongTimePattern = "HH:mm:ss"
                }
            };

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            MyGlobal.iMainFormLeft = Left;
            MyGlobal.iMainFormTop = Top;
        }

        private void btnAutoRollbackOnError_Click(object sender, EventArgs e)
        {
            btnAutoRollbackOnErrorOn.Visible = !btnAutoRollbackOnErrorOn.Visible;
            btnAutoRollbackOnErrorOff.Visible = !btnAutoRollbackOnErrorOn.Visible;

            MyGlobal.bDBAutoRollback = btnAutoRollbackOnErrorOn.Visible;
        }

        private void LoadGlobalSetting(bool bChangeLocalization = false)
        {
            DataRow[] dtRow;

            #region 載入 Global 設定值
            //檢查是否有此帳號的 Global 設定值，若沒有，則 Insert 預設值
            var sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '{0}' AND AttributeKey = 'GlobalConfig'";
            sSql = string.Format(sSql, MyGlobal.sDomainUser);
            var dtData = DBCommon.ExecQuery(sSql);

            if (bChangeLocalization == false)
            {
                sSql = "AttributeName = 'Localization'"; //取得語系設定
                dtRow = dtData.Select(sSql);
                MyGlobal.sLocalization = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "English";
            }

            //Start: EnableCheckForUpdate
            sSql = "AttributeName = 'EnableCheckForUpdate'";
            dtRow = dtData.Select(sSql);

            if (dtRow.Length == 0)
            {
                //第一次執行 JasonQuery, 新增 "EnableCheckForUpdate", 預設要檢查更新
                sSql = "INSERT INTO SystemConfig (DomainUser, AttributeKey, AttributeName, AttributeValue, AttributeDate) VALUES ('{0}', 'GlobalConfig', 'EnableCheckForUpdate', '1', '{1}')";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                DBCommon.ExecNonQuery(sSql);

                MyLibrary.bCheckForUpdate = false; //第一次執行 JasonQuery, 不檢查！
            }
            else
            {
                MyLibrary.bCheckForUpdate = dtRow.Length <= 0 || (dtRow[0]["AttributeValue"].ToString() != "0");
            }
            //End: EnableCheckForUpdate

            sSql = "AttributeName = 'CheckForUpdateDays'";
            dtRow = dtData.Select(sSql);

            if (dtRow.Length > 0)
            {
                MyLibrary.iCheckForUpdate = Convert.ToInt16(dtRow[0]["AttributeValue"].ToString());
            }
            else
            {
                MyLibrary.iCheckForUpdate = 7;

                //20200715 此處要寫入記錄，否則第一次啟動時找不到 JasonQuery.db，產生 JasonQuery.db 後，馬上就會檢查更新
                sSql = "INSERT INTO SystemConfig (DomainUser, AttributeKey, AttributeName, AttributeValue, AttributeDate) VALUES ('{0}', 'GlobalConfig', 'CheckForUpdateDays', '{1}', '{2}')";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, MyLibrary.iCheckForUpdate, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                DBCommon.ExecNonQuery(sSql);
            }

            sSql = "AttributeName = 'DateFormat'";
            dtRow = dtData.Select(sSql);
            MyLibrary.sDateFormat = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeName = 'ShowVersion'";
            dtRow = dtData.Select(sSql);
            MyLibrary.bShowVersion = dtRow.Length <= 0 || (dtRow[0]["AttributeValue"].ToString() != "0");

            sSql = "AttributeName = 'ShowClock'";
            dtRow = dtData.Select(sSql);
            MyLibrary.bHideClock = dtRow.Length > 0 && (dtRow[0]["AttributeValue"].ToString() == "1");

            lblDateTime.Visible = !MyLibrary.bHideClock;
            spDateTime.Visible = !MyLibrary.bHideClock;
            tmrDateTime.Enabled = !MyLibrary.bHideClock;

            sSql = "AttributeName = 'MainFormMaximized'";
            dtRow = dtData.Select(sSql);
            MyGlobal.bMainFormMaximized = dtRow.Length > 0 && (dtRow[0]["AttributeValue"].ToString() == "1");

            sSql = "AttributeName = 'MainFormLocationX'";
            dtRow = dtData.Select(sSql);
            MyGlobal.iMainFormLocationX = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 1;

            sSql = "AttributeName = 'MainFormLocationY'";
            dtRow = dtData.Select(sSql);
            MyGlobal.iMainFormLocationY = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 1;

            sSql = "AttributeName = 'MainFormWidth'";
            dtRow = dtData.Select(sSql);
            MyGlobal.iMainFormWidth = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 1024;

            sSql = "AttributeName = 'MainFormHeight'";
            dtRow = dtData.Select(sSql);
            MyGlobal.iMainFormHeight = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 768;

            sSql = "AttributeName = 'RecentFilesQty'";
            dtRow = dtData.Select(sSql);
            MyLibrary.iRecentFilesQty = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 20;

            sSql = "AttributeName = 'MyFavoriteQty'";
            dtRow = dtData.Select(sSql);
            MyLibrary.iMyFavoriteQty = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 20;

            sSql = "AttributeName = 'OptionsTabActiveForeColor'";
            dtRow = dtData.Select(sSql);
            MyLibrary.sColorOptionsTabActiveForeColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeName = 'OptionsTabActiveBackColor'";
            dtRow = dtData.Select(sSql);
            MyLibrary.sColorOptionsTabActiveBackColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeName = 'OptionsTabInactiveForeColor'";
            dtRow = dtData.Select(sSql);
            MyLibrary.sColorOptionsTabInactiveForeColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeName = 'TabStyle'";
            dtRow = dtData.Select(sSql);
            MyLibrary.sTabStyle = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeName = 'TabAppearance'";
            dtRow = dtData.Select(sSql);
            MyLibrary.sTabAppearance = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeName = 'TabBold'";
            dtRow = dtData.Select(sSql);
            MyGlobal.bTabBold = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeName = 'TabShrinkPages'";
            dtRow = dtData.Select(sSql);
            MyGlobal.bTabShrinkPages = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeName = 'TabShowArrows'";
            dtRow = dtData.Select(sSql);
            MyGlobal.bTabShowArrows = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeName = 'TabHoverSelect'";
            dtRow = dtData.Select(sSql);
            MyGlobal.bTabHoverSelect = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeName = 'TabMultiLine'";
            dtRow = dtData.Select(sSql);
            MyGlobal.bTabMultiLine = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";
            #endregion
        }

        private void LoadConnectionForm()
        {
            string sTemp;
            var bConnectResult = true;

            using (var f5 = new frmConnect())
            {
                f5.ShowInTaskbar = true;
                f5.StartPosition = FormStartPosition.CenterScreen;
                f5.ValueUpdated += uValueUpdated;
                f5.ShowDialog();

                if (_iConnectionFormChangeLocalization > -1)
                {
                    _iConnectionFormChangeLocalization = -2;
                }
                else if (_iConnectionFormChangeLocalization == -2)
                {
                    return;
                }
            }

            Application.UseWaitCursor = true;

            if (!string.IsNullOrEmpty(MyGlobal.sDataSource))
            {
                var sConnectTo = ConnectToDatabase(); //嘗試連線資料庫

                if (!string.IsNullOrEmpty(sConnectTo))
                {
                    _sLangText = MyGlobal.GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text");
                    sTemp = _sLangText + "\r\n\r\n";
                    _sLangText = MyGlobal.GetLanguageString("Error connecting to the server:", "Global", "Global", "msg", "ErrorConnectingToTheServer", "Text");
                    sTemp += _sLangText + "\r\n\r\n";
                    _sLangText = MyGlobal.GetLanguageString("Connection Name:", "form", "frmConnect", "object", "lblConnectionName", "Text");
                    sTemp += _sLangText + " " + MyGlobal.sDBConnectionName + "\r\n";
                    _sLangText = MyGlobal.GetLanguageString("Server:", "form", "frmConnect", "object", "lblServer", "Text");
                    sTemp += _sLangText + " " + MyGlobal.sDBConnectionServer + "\r\n\r\n" + sConnectTo;

                    MessageBox.Show(sTemp, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bConnectResult = false;
                }
            }

            if (string.IsNullOrEmpty(MyGlobal.sDataSource) || !bConnectResult)
            {
                EnableCloseTabMenu(false); //LoadConnectionForm
                _bMenuEnable = true; //只能使用「基本功能」裡面的項目
                _sMenuItems = "`mnuFile`mnuNewConnection`mnuExit`mnuHelp`mnuCheckForUpdatesManually`mnuReleaseNotes`mnuReportBugs`mnuAbout`";
            }
            else
            {
                switch (MyGlobal.sDataSource)
                {
                    case "Oracle":
                        btnOracle.Visible = true;
                        break;
                    case "PostgreSQL":
                        btnPostgreSQL.Visible = true;
                        break;
                    case "SQL Server":
                        btnSQLServer.Visible = true;
                        break;
                    case "MySQL":
                        btnMySQL.Visible = true;
                        break;
                    case "SQLite":
                    case "SQLCipyer":
                        btnSQLite.Visible = true;
                        break;
                }

                //載入各項預設值
                LoadDefaultSetting(); //LoadConnectForm

                _bMenuEnable = false; //使用者有指定連線
                _sMenuItems = "`mnuNewConnection`mnuOpenConnection`";

                Text = MyGlobal.sDBConnectionTitle + @" - " + Tag + (MyLibrary.bShowVersion == false ? "" : @" " + MyGlobal.sMyVersion) + @" " + (IntPtr.Size == 8 ? @"(x64)" : @"(x86)");

                EnableCloseTabMenu(true); //LoadConnectionForm

                _toolTip1.ForeColor = Color.Blue;
                _toolTip1.BackColor = Color.Gray;
                _toolTip1.UseAnimation = true;
                _toolTip1.AutoPopDelay = 5000;
                _toolTip1.InitialDelay = 2500;
                _toolTip1.ReshowDelay = 30;

                sTemp = "";
                string sSql;
                string sDBVersionShow = "";
                DataTable dtTemp;
                MyGlobal.sDBVersion = "";

                //20220102 針對每個資料庫，視需要取得版號
                //20220309 取得每個資料庫的版號，並顯示在主畫面的狀態列上
                switch (MyGlobal.sDataSource)
                {
                    case "Oracle":
                        sSql = "SELECT * FROM Product_Component_Version"; //"SELECT Banner FROM sys.v_$version";
                        dtTemp = MyGlobal.oOracleReader.ExecuteQueryToDataTable(sSql);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            try
                            {
                                sDBVersionShow = dtTemp.Rows[0]["Version"].ToString();
                            }
                            catch (Exception)
                            {
                                //
                            }
                        }

                        break;
                    case "PostgreSQL":
                        //20220805 取得所有 database name
                        sSql = "SELECT datname FROM pg_database WHERE datname <> 'template0'";
                        _dtDatabase = MyGlobal.oPostgreReader.ExecuteQueryToDataTable(sSql);

                        sSql = "SHOW Server_Version;";
                        dtTemp = MyGlobal.oPostgreReader.ExecuteQueryToDataTable(sSql);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            try
                            {
                                sTemp = dtTemp.Rows[0]["Server_Version"].ToString();
                                sDBVersionShow = dtTemp.Rows[0]["Server_Version"].ToString();
                            }
                            catch (Exception)
                            {
                                //
                            }
                        }

                        if (sTemp.Substring(0, 1) == "2" || sTemp.Substring(0, 1) == "1" && sTemp.Substring(1, 1) != "0")
                        {
                            MyGlobal.sDBVersion = ">=11";
                        }
                        else
                        {
                            MyGlobal.sDBVersion = "<=10";
                        }

                        break;
                    case "SQL Server":
                        //20220808 取得所有 database name
                        sSql = "SELECT Name FROM master.sys.databases ORDER BY Name;";
                        //20220810 此處列出全部的 DB，因為使用者可能會需要切換到原生 DB！
                        //sSql = string.Format(sSql, (MyGlobal.bDBExcludeNativeDatabase ? " WHERE Name NOT IN ('master', 'model', 'msdb', 'tempdb')" : ""));
                        _dtDatabase = MyGlobal.oSQLServerReader.ExecuteQueryToDataTable(sSql);

                        //20210623 取得 SQL Server 版號
                        sSql = "SELECT @@Version AS Version";
                        dtTemp = MyGlobal.oSQLServerReader.ExecuteQueryToDataTable(sSql);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            MyGlobal.sDBVersion = dtTemp.Rows[0]["version"].ToString().ToUpper().Replace(" ", "");

                            if (MyGlobal.sDBVersion.StartsWith("MICROSOFTSQLSERVER2000"))
                            {
                                MyGlobal.sDBVersion = "2000";
                            }
                        }

                        sSql = "SELECT SERVERPROPERTY('ProductVersion') AS ProductVersion";

                        try
                        {
                            dtTemp = MyGlobal.oSQLServerReader.ExecuteQueryToDataTable(sSql);

                            if (dtTemp != null && dtTemp.Rows.Count > 0)
                            {
                                try
                                {
                                    sDBVersionShow = dtTemp.Rows[0]["ProductVersion"].ToString();

                                    if (sDBVersionShow.StartsWith("6") || sDBVersionShow.StartsWith("7"))
                                    {
                                        MyGlobal.sDBVersion = "2000";
                                    }
                                }
                                catch (Exception)
                                {
                                    //
                                }
                            }
                        }
                        catch (Exception)
                        { }

                        #region SQL Server 版本 vs 版號
                        /*
                          SQL Server 2019	15.0.x
                          SQL Server 2017	14.0.x
                          SQL Server 2016	13.0.x
                          SQL Server 2014	12.0.x
                          SQL Server 2012	11.0.x
                          SQL Server 2008 R2	10.5.x
                          SQL Server 2008	10.0.x
                          SQL Server 2005	9.0.x
                          SQL Server 2000	8.0.x
                          SQL Server 7.0	7.0.x
                          SQL Server 6.5	6.5.x
                          SQL Server 6.0	6.0.x
                          */
                        #endregion

                        break;
                    case "MySQL":
                        //20220808 取得所有 database name
                        sSql = "SELECT Schema_Name AS Name FROM information_schema.schemata ORDER BY Schema_Name;";
                        _dtDatabase = MyGlobal.oMySQLReader.ExecuteQueryToDataTable(sSql);

                        sSql = "SELECT Version() AS Version;"; //SELECT @@Version;
                        dtTemp = MyGlobal.oMySQLReader.ExecuteQueryToDataTable(sSql);

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            try
                            {
                                sDBVersionShow = dtTemp.Rows[0]["Version"].ToString();
                            }
                            catch (Exception)
                            {
                                //
                            }
                        }

                        break;
                }

                if (sDBVersionShow.Length > 2)
                {
                    sDBVersionShow = sDBVersionShow.Substring(sDBVersionShow.Length - 2, 2) == ".0" ? sDBVersionShow.Substring(0, sDBVersionShow.Length - 2) : sDBVersionShow;
                }

                spDBVersion.Visible = true;
                lblDBVersion.Text = MyGlobal.sDataSource + " " + sDBVersionShow;

                //20201125 MainForm 在此處中斷連線 (少佔用一個連線數，也省去中斷連線時釋放的問題)
                DisconnectDatabase(true);

                var ss = CheckTabNameExist();

                if (CheckTabNameExist(ss) == false)
                {
                    CreateNewTab("Query", ss);
                }

                if (!string.IsNullOrEmpty(MyGlobal.sSpecifiedSQLFile1))
                {
                    sTemp = CheckTabNameExist(); //LoadConnectionForm, 取得下一個空白編號
                    CreateNewTab("Query", sTemp, MyGlobal.sSpecifiedSQLFile1);
                }

                var startTime = DateTime.Now;

                while (true)
                {
                    Application.DoEvents();

                    if (DateTime.Now.Subtract(startTime).Milliseconds >= 150)
                    {
                        break;
                    }

                    Application.DoEvents();
                }

                if (!string.IsNullOrEmpty(MyGlobal.sSpecifiedSQLFile2) && CheckTabNameExist(MyGlobal.sSpecifiedSQLFile2) == false)
                {
                    sTemp = CheckTabNameExist(); //LoadConnectionForm, 取得下一個空白編號
                    CreateNewTab("Query", sTemp, MyGlobal.sSpecifiedSQLFile2);
                }
            }

            ApplyLocalization(); //LoadConnectForm

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                case "SQLite":
                case "SQLCipher":
                    break;
                case "PostgreSQL":
                    LoadPostgreSQLDatabase(); //Form_Load
                    break;
                case "SQL Server":
                    LoadSQLServerDatabase(); //Form_Load
                    break;
                case "MySQL":
                    LoadMySQLDatabase(); //Form_Load
                    break;
            }

            Application.UseWaitCursor = false;
        }

        private static string ConnectToDatabase()
        {
            var sResult = "";

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    sResult = MyGlobal.oOracleReader.ConnectTo();
                    break;
                case "PostgreSQL":
                    sResult = MyGlobal.oPostgreReader.ConnectTo(MyGlobal.sDBConnectionString);
                    break;
                case "SQL Server":
                    sResult = MyGlobal.oSQLServerReader.ConnectTo(MyGlobal.sDBConnectionString);
                    break;
                case "MySQL":
                    sResult = MyGlobal.oMySQLReader.ConnectTo(MyGlobal.sDBConnectionString);
                    break;
                case "SQLite":
                    sResult = MyGlobal.oSQLiteReader.ConnectTo(MyGlobal.sDBConnectionString);
                    break;
                case "SQLCipher":
                    sResult = MyGlobal.oSQLCipherReader.ConnectTo(MyGlobal.sDBConnectionString);
                    break;
            }

            return sResult;
        }

        private void LoadDefaultSetting()
        {
            Cursor = Cursors.WaitCursor;

            LoadRecentList(); //LoadDefaultValue

            LoadMyFavoriteFiles(); //LoadDefaultValue

            //載入設定值 part 1
            var sSelectCondition = "'GeneralConfig', 'EditorConfig', 'AutoCompleteConfig', 'AutoReplaceConfig', 'GridConfig', 'SQLFormatterConfig', 'SQL2CodeConfig', 'GenerateSQLConfig'";
            
            #region 檢查是否有此帳號的一般設定值，若沒有，則 Insert 預設值
            var sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey IN (" + sSelectCondition + ", 'KeywordsConfig'" + ")";
            var dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count == 0)
            {
                //Insert 預設關鍵字
                string sKeywords;

                switch (MyGlobal.sDataSource)
                {
                    case "PostgreSQL":
                    case "MySQL":
                    case "SQLite":
                    case "SQLCipher":
                        //BuiltInFunctions
                        sKeywords = "abbrev abs acos age area array_agg array_append array_cat array_dims array_fill array_length array_lower array_ndims array_prepend array_to_string array_upper ascii asin atan atan2 avg bit_and bit_length bit_or bool_and bool_or box broadcast btrim cbrt ceil ceiling center char_length chr circle clock_timestamp coalesce col_description concat concat_ws convert convert_from convert_to corr cos cot count covar_pop covar_samp cume_dist current_catalog current_database current_date current_query current_schema current_schemas current_setting current_time current_timestamp current_user currval cursor_to_xml cursor_to_xmlschema database_to_xml database_to_xml_and_xmlschema database_to_xmlschema date_part date_trunc decode degrees dense_rank diameter div encode enum_first enum_last enum_range every exists exp extract family first_value floor format format_type generate_series generate_subscripts get_bit get_byte get_current_ts_config greatest has_any_column_privilege has_column_privilege has_database_privilege has_foreign_data_wrapper_privilege has_function_privilege has_language_privilege has_schema_privilege has_sequence_privilege has_server_privilege has_table_privilege has_tablespace_privilege height host hostmask inet_client_addr inet_client_port inet_server_addr inet_server_port initcap isclosed isfinite isopen justify_days justify_hours justify_interval lag last_value lastval lead least left length ln localtime localtimestamp log lower lpad lseg ltrim masklen max md5 min mod netmask network nextval now npoints nth_value ntile nullif numnode obj_description octet_length overlay path pclose percent_rank pg_advisory_lock pg_advisory_lock_shared pg_advisory_unlock pg_advisory_unlock_all pg_advisory_unlock_shared pg_advisory_xact_lock pg_advisory_xact_lock_shared pg_backend_pid pg_cancel_backend pg_client_encoding pg_collation_is_visible pg_column_size pg_conf_load_time pg_conversion_is_visible pg_create_restore_point pg_current_xlog_insert_location pg_current_xlog_location pg_database_size pg_describe_object pg_function_is_visible pg_get_constraintdef pg_get_expr pg_get_function_arguments pg_get_function_identity_arguments pg_get_function_result pg_get_functiondef pg_get_indexdef pg_get_keywords pg_get_ruledef pg_get_serial_sequence pg_get_triggerdef pg_get_userbyid pg_get_viewdef pg_has_role pg_indexes_size pg_is_in_recovery pg_is_other_temp_schema pg_is_xlog_replay_paused pg_last_xact_replay_timestamp pg_last_xlog_receive_location pg_last_xlog_replay_location pg_listening_channels pg_ls_dir pg_my_temp_schema pg_opclass_is_visible pg_operator_is_visible pg_options_to_table pg_postmaster_start_time pg_read_binary_file pg_read_file pg_relation_filenode pg_relation_filepath pg_relation_size pg_reload_conf pg_rotate_logfile pg_size_pretty pg_sleep pg_start_backup pg_stat_file pg_stop_backup pg_switch_xlog pg_table_is_visible pg_table_size pg_tablespace_databases pg_tablespace_size pg_terminate_backend pg_total_relation_size pg_try_advisory_lock pg_try_advisory_lock_shared pg_try_advisory_xact_lock pg_try_advisory_xact_lock_shared pg_ts_config_is_visible pg_ts_dict_is_visible pg_ts_parser_is_visible pg_ts_template_is_visible pg_type_is_visible pg_typeof pg_xlog_replay_pause pg_xlog_replay_resume pg_xlogfile_name pg_xlogfile_name_offset pi plainto_tsquery point polygon popen position power query_to_xml query_to_xml_and_xmlschema query_to_xmlschema querytree quote_ident quote_literal quote_nullable radians radius random rank regexp_matches regexp_replace regexp_split_to_array regexp_split_to_table regr_avgx regr_avgy regr_count regr_intercept regr_r2 regr_slope regr_sxx regr_sxy regr_syy repeat replace reverse right round row_number rpad rtrim schema_to_xml schema_to_xml_and_xmlschema schema_to_xmlschema session_user set_bit set_byte set_config set_masklen setseed setval setweight shobj_description sign sin split_part sqrt statement_timestamp stddev stddev_pop stddev_samp string_agg string_to_array strip strpos substr substring sum table_to_xml table_to_xml_and_xmlschema table_to_xmlschema tan text timeofday to_ascii to_char to_date to_hex to_number to_timestamp to_tsquery to_tsvector transaction_timestamp translate trim trunc ts_debug ts_headline ts_lexize ts_parse ts_rank ts_rank_cd ts_rewrite ts_stat ts_token_type tsvector_update_trigger tsvector_update_trigger_column txid_current txid_current_snapshot txid_snapshot_xip txid_snapshot_xmax txid_snapshot_xmin txid_visible_in_snapshot unnest upper user var_pop var_samp variance version width width_bucket xip_list xmax xmin xml_is_well_formed xml_is_well_formed_content xml_is_well_formed_document xmlagg xmlcomment xmlconcat xmlelement xmlexists xmlforest xmlpi xmlroot xpath ";
                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeText, AttributeDate) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'KeywordsConfig', 'BuiltInFunctions', '" + sKeywords + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                        DBCommon.ExecNonQuery(sSql);

                        //BuiltInKeywords
                        sKeywords = "abort absent absolute access according action add admin after aggregate all allocate also alter always analyse analyze and any are array array_max_cardinality as asc asensitive assertion assignment asymmetric at atomic attribute attributes authorization backward base64 before begin begin_frame begin_partition bernoulli between bigint binary bit blob blocked bom boolean both breadth by cache call called cardinality cascade cascaded case cast catalog catalog_name chain char character characteristics characters character_length character_set_catalog character_set_name character_set_schema check checkpoint class class_origin clob close cluster cobol collate collation collation_catalog collation_name collation_schema collect column columns column_name command_function command_function_code comment comments commit committed concurrently condition condition_number configuration conflict connect connection connection_name constraint constraints constraint_catalog constraint_name constraint_schema constructor contains content continue control conversion copy corresponding cost create cross csv cube current current_default_transform_group current_path current_role current_row current_transform_group_for_type cursor cursor_name cycle data database datalink date datetime_interval_code datetime_interval_precision day db deallocate dec decimal declare default defaults deferrable deferred defined definer degree delete delimiter delimiters depends depth deref derived desc describe descriptor deterministic diagnostics dictionary disable discard disconnect dispatch distinct dlnewcopy dlpreviouscopy dlurlcomplete dlurlcompleteonly dlurlcompletewrite dlurlpath dlurlpathonly dlurlpathwrite dlurlscheme dlurlserver dlvalue do document domain double drop dynamic dynamic_function dynamic_function_code each element else empty enable encoding encrypted end end-exec end_frame end_partition enforced enum equals escape event except exception exclude excluding exclusive exec execute explain expression extension external false fetch file filter final first flag float following for force foreign fortran forward found frame_row free freeze from fs full function functions fusion general generated get global go goto grant granted group grouping groups handler having header hex hierarchy hold hour id identity if ignore ilike immediate immediately immutable implementation implicit import in including increment indent index indexes indicator inherit inherits initially inline inner inout input insensitive insert instance instantiable instead int integer integrity intersect intersection interval into invoker is isnull isolation join key key_member key_type label language large last lateral leading leakproof level library like like_regex limit link listen load local location locator lock locked logged map mapping match matched materialized maxvalue max_cardinality member merge message_length message_octet_length message_text method minute minvalue mode modifies module month more move multiset mumps name names namespace national natural nchar nclob nesting new next nfc nfd nfkc nfkd nil no none normalize normalized not nothing notify notnull nowait null nullable nulls number numeric object occurrences_regex octets of off offset oids old on only open operator option options or order ordering ordinality others out outer output over overlaps overriding owned owner pad parallel parameter parameter_mode parameter_name parameter_ordinal_position parameter_specific_catalog parameter_specific_name parameter_specific_schema parser partial partition pascal passing passthrough password percent percentile_cont percentile_disc period permission placing plans pli policy portion position_regex precedes preceding precision prepare prepared preserve primary prior privileges procedural procedure program public quote range read reads real reassign recheck recovery recursive ref references referencing refresh reindex relative release rename repeatable replica requiring reset respect restart restore restrict result return returned_cardinality returned_length returned_octet_length returned_sqlstate returning returns revoke role rollback rollup routine routine_catalog routine_name routine_schema row rows row_count rule savepoint scale schema schema_name scope scope_catalog scope_name scope_schema scroll search second section security select selective self sensitive sequence sequences serializable server server_name session set setof sets share show similar simple size skip smallint snapshot some source space specific specifictype specific_name sql sqlcode sqlerror sqlexception sqlstate sqlwarning stable standalone start state statement static statistics stdin stdout storage strict structure style subclass_origin submultiset substring_regex succeeds symmetric sysid system system_time system_user table tables tablesample tablespace table_name temp template temporary then ties time timestamp timezone_hour timezone_minute to token top_level_count trailing transaction transactions_committed transactions_rolled_back transaction_active transform transforms translate_regex translation treat trigger trigger_catalog trigger_name trigger_schema trim_array true truncate trusted type types uescape unbounded uncommitted under unencrypted union unique unknown unlink unlisten unlogged unnamed until untyped update uri usage use user_defined_type_catalog user_defined_type_code user_defined_type_name user_defined_type_schema using vacuum valid validate validator value values value_of varbinary varchar variadic varying verbose versioning view views volatile when whenever where whitespace window with within without work wrapper write xml xmlattributes xmlbinary xmlcast xmldeclaration xmldocument xmliterate xmlnamespaces xmlparse xmlquery xmlschema xmlserialize xmltable xmltext xmlvalidate year yes zone ";
                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeText, AttributeDate) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'KeywordsConfig', 'BuiltInKeywords', '" + sKeywords + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                        DBCommon.ExecNonQuery(sSql);
                        break;
                    case "SQL Server": //20200308 部份關鍵字不一樣，獨立出來，並改為小寫
                        //BuiltInFunctions
                        sKeywords = "abs acos add_months appendchildxml ascii asciistr asin atan atan2 avg bfilename bin_to_num bitand cardinality case cast ceil chartorowid chr cluster_id cluster_probability cluster_set coalesce collect compose concat convert corr corr_k corr_s cos cosh count covar_pop covar_samp cume_dist current_date current_timestamp cv dbtimezone decode decompose deletexml dense_rank depth deref dump empty_blob empty_clob existsnode exp extract extractvalue feature_id feature_set feature_value first first_value floor from_tz greatest group_id grouping grouping_id hextoraw initcap insertchildxml insertxmlbefore instr instr2 instr4 instrb instrc iteration_number lag last last_day last_value lead least len ln lnnvl localtimestamp log lower lpad ltrim make_ref max median min mod months_between nanvl nchr new_time next_day nls_charset_decl_len nls_charset_id nls_charset_name nls_initcap nls_lower nls_upper nlssort nth_value ntile nullif numtodsinterval numtoyminterval nvl nvl2 ora_hash path percent_rank percentile_cont percentile_disc power powermultiset powermultiset_by_cardinality prediction prediction_cost prediction_details prediction_probability prediction_set presentnnv presentv previous rank ratio_to_report rawtohex rawtonhex ref reftohex regexp_count regexp_instr regexp_replace regexp_substr regr_avgx regr_avgy regr_count regr_intercept regr_r2 regr_slope regr_sxx regr_syy remainder replace round row_number rowidtochar rowidtonchar rownum rpad rtrim scn_to_timestamp sessiontimezone sign sin sinh soundex sqrt stats_binomial_test stats_crosstab stats_f_test stats_ks_test stats_mode stats_mw_test stats_one_way_anova stats_t_test_indep stats_t_test_indepu stats_t_test_one stats_t_test_paired stats_wsr_test stddev stddev_pop stddev_samp substring sum sys_connect_by_path sys_context sys_dburigen sys_extract_utc sys_guid sys_typeid sys_xmlagg sys_xmlgen sysdate systimestamp tan tanh timestamp_to_scn to_binary_double to_binary_float to_char to_clob to_date to_dsinterval to_lob to_multi_byte to_nclob to_number to_single_byte to_timestamp to_timestamp_tz to_yminterval translate treat trim trunc tz_offset uid unistr updatexml upper user userenv value var_pop var_samp variance vsize width_bucket xmlagg xmlcdata xmlcolattval xmlcomment xmlconcat xmlforest xmlparse xmlpi xmlquery xmlroot xmlsequence xmlserialize xmltable xmltransform ";
                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeText, AttributeDate) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'KeywordsConfig', 'BuiltInFunctions', '" + sKeywords + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                        DBCommon.ExecNonQuery(sSql);

                        //BuiltInKeywords
                        sKeywords = "abort accept access add admin after all allocate alter analyze and any archive archivelog array arraylen as asc assert assign at audit authorization avg avg backup base_table become before begin between binary_integer block body boolean by cache cancel cascade case change char char_base character check checkpoint close cluster clusters cobol colauth column columns comment commit compile compress connect constant constraint constraints contents continue controlfile count crash create current currval cursor cycle data_base database datafile date dba debugoff debugon dec decimal declare default definition delay delete delta desc digits disable dismount dispose distinct do double drop dump each else elsif enable end entry escape events except exception exception_init exceptions exclusive exec execute exists exit explain extent externally fetch file float flush for force foreign form fortran found freelist freelists from function generic go goto grant group grouping groups having identified if immediate in including increment index indexes indicator initial initrans insert instance int integer intersect into is key language layer level like limited link lists lock logfile long loop manage manual max maxdatafiles maxextents maxinstances maxlogfiles maxloghistory maxlogmembers maxtrans maxvalue min minextents minus minvalue mlslabel mod mode modify module mount natural new next nextval noarchivelog noaudit nocache nocompress nocycle nomaxvalue nominvalue none noorder noresetlogs normal nosort not notfound nowait null number number_base numeric of off offline old on online only open optimal option or order others out own package parallel partition pctfree pctincrease pctused plan pli positive pragma precision primary prior private privileges procedure profile public quota raise range raw read real record recover references referencing release remr rename resetlogs resource restricted return reuse reverse revoke role roles rollback row rowid rowlabel rownum rows rowtype run savepoint schema scn section segment select separate sequence session set share shared size smallint snapshot some sort space sql sqlbuf sqlcode sqlerrm sqlerror sqlstate start statement statement_id statistics stddev stop storage subtype successful sum switch synonym sysdate system tabauth table tables tablespace task temporary terminate then thread time to tracing transaction trigger triggers truncate type uid under union unique unlimited until update use user using validate values varchar varchar2 variance view views when whenever where while with within work write xor false true ";
                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeText, AttributeDate) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'KeywordsConfig', 'BuiltInKeywords', '" + sKeywords + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                        DBCommon.ExecNonQuery(sSql);
                        break;
                    default: //Oracle...
                        //BuiltInFunctions
                        sKeywords = "ABS ACOS ADD_MONTHS APPENDCHILDXML ASCII ASCIISTR ASIN ATAN ATAN2 AVG BFILENAME BIN_TO_NUM BITAND CARDINALITY CASE CAST CEIL CHARTOROWID CHR CLUSTER_ID CLUSTER_PROBABILITY CLUSTER_SET COALESCE COLLECT COMPOSE CONCAT CONVERT CORR CORR_K CORR_S COS COSH COUNT COVAR_POP COVAR_SAMP CUME_DIST CURRENT_DATE CURRENT_TIMESTAMP CV DBTIMEZONE DECODE DECOMPOSE DELETEXML DENSE_RANK DEPTH DEREF DUMP EMPTY_BLOB EMPTY_CLOB EXISTSNODE EXP EXTRACT EXTRACTVALUE FEATURE_ID FEATURE_SET FEATURE_VALUE FIRST FIRST_VALUE FLOOR FROM_TZ GREATEST GROUP_ID GROUPING GROUPING_ID HEXTORAW INITCAP INSERTCHILDXML INSERTXMLBEFORE INSTR INSTR2 INSTR4 INSTRB INSTRC ITERATION_NUMBER LAG LAST LAST_DAY LAST_VALUE LEAD LEAST LENGTH LENGTH2 LENGTH4 LENGTHB LENGTHC LISTAGG LN LNNVL LOCALTIMESTAMP LOG LOWER LPAD LTRIM MAKE_REF MAX MEDIAN MIN MOD MONTHS_BETWEEN NANVL NCHR NEW_TIME NEXT_DAY NLS_CHARSET_DECL_LEN NLS_CHARSET_ID NLS_CHARSET_NAME NLS_INITCAP NLS_LOWER NLS_UPPER NLSSORT NTH_VALUE NTILE NULLIF NUMTODSINTERVAL NUMTOYMINTERVAL NVL NVL2 ORA_HASH PATH PERCENT_RANK PERCENTILE_CONT PERCENTILE_DISC POWER POWERMULTISET POWERMULTISET_BY_CARDINALITY PREDICTION PREDICTION_COST PREDICTION_DETAILS PREDICTION_PROBABILITY PREDICTION_SET PRESENTNNV PRESENTV PREVIOUS RANK RATIO_TO_REPORT RAWTOHEX RAWTONHEX REF REFTOHEX REGEXP_COUNT REGEXP_INSTR REGEXP_REPLACE REGEXP_SUBSTR REGR_AVGX REGR_AVGY REGR_COUNT REGR_INTERCEPT REGR_R2 REGR_SLOPE REGR_SXX REGR_SYY REMAINDER REPLACE ROUND ROW_NUMBER ROWIDTOCHAR ROWIDTONCHAR ROWNUM RPAD RTRIM SCN_TO_TIMESTAMP SESSIONTIMEZONE SIGN SIN SINH SOUNDEX SQRT STATS_BINOMIAL_TEST STATS_CROSSTAB STATS_F_TEST STATS_KS_TEST STATS_MODE STATS_MW_TEST STATS_ONE_WAY_ANOVA STATS_T_TEST_INDEP STATS_T_TEST_INDEPU STATS_T_TEST_ONE STATS_T_TEST_PAIRED STATS_WSR_TEST STDDEV STDDEV_POP STDDEV_SAMP SUBSTR SUM SYS_CONNECT_BY_PATH SYS_CONTEXT SYS_DBURIGEN SYS_EXTRACT_UTC SYS_GUID SYS_TYPEID SYS_XMLAGG SYS_XMLGEN SYSDATE SYSTIMESTAMP TAN TANH TIMESTAMP_TO_SCN TO_BINARY_DOUBLE TO_BINARY_FLOAT TO_CHAR TO_CLOB TO_DATE TO_DSINTERVAL TO_LOB TO_MULTI_BYTE TO_NCLOB TO_NUMBER TO_SINGLE_BYTE TO_TIMESTAMP TO_TIMESTAMP_TZ TO_YMINTERVAL TRANSLATE TREAT TRIM TRUNC TZ_OFFSET UID UNISTR UPDATEXML UPPER USER USERENV VALUE VAR_POP VAR_SAMP VARIANCE VSIZE WIDTH_BUCKET XMLAGG XMLCDATA XMLCOLATTVAL XMLCOMMENT XMLCONCAT XMLFOREST XMLPARSE XMLPI XMLQUERY XMLROOT XMLSEQUENCE XMLSERIALIZE XMLTABLE XMLTRANSFORM ";
                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeText, AttributeDate) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'KeywordsConfig', 'BuiltInFunctions', '" + sKeywords + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                        DBCommon.ExecNonQuery(sSql);

                        //BuiltInKeywords
                        sKeywords = "ABORT ACCEPT ACCESS ADD ADMIN AFTER ALL ALLOCATE ALTER ANALYZE AND ANY ARCHIVE ARCHIVELOG ARRAY ARRAYLEN AS ASC ASSERT ASSIGN AT AUDIT AUTHORIZATION AVG AVG BACKUP BASE_TABLE BECOME BEFORE BEGIN BETWEEN BINARY_INTEGER BLOCK BODY BOOLEAN BY CACHE CANCEL CASCADE CASE CHANGE CHAR CHAR_BASE CHARACTER CHECK CHECKPOINT CLOSE CLUSTER CLUSTERS COBOL COLAUTH COLUMN COLUMNS COMMENT COMMIT COMPILE COMPRESS CONNECT CONSTANT CONSTRAINT CONSTRAINTS CONTENTS CONTINUE CONTROLFILE COUNT CRASH CREATE CURRENT CURRVAL CURSOR CYCLE DATA_BASE DATABASE DATAFILE DATE DBA DEBUGOFF DEBUGON DEC DECIMAL DECLARE DEFAULT DEFINITION DELAY DELETE DELTA DESC DIGITS DISABLE DISMOUNT DISPOSE DISTINCT DO DOUBLE DROP DUMP EACH ELSE ELSIF ENABLE END ENTRY ESCAPE EVENTS EXCEPT EXCEPTION EXCEPTION_INIT EXCEPTIONS EXCLUSIVE EXEC EXECUTE EXISTS EXIT EXPLAIN EXTENT EXTERNALLY FETCH FILE FLOAT FLUSH FOR FORCE FOREIGN FORM FORTRAN FOUND FREELIST FREELISTS FROM FUNCTION GENERIC GO GOTO GRANT GROUP GROUPING GROUPS HAVING IDENTIFIED IF IMMEDIATE IN INCLUDING INCREMENT INDEX INDEXES INDICATOR INITIAL INITRANS INSERT INSTANCE INT INTEGER INTERSECT INTO IS KEY LANGUAGE LAYER LEVEL LIKE LIMITED LINK LISTS LOCK LOGFILE LONG LOOP MANAGE MANUAL MAX MAXDATAFILES MAXEXTENTS MAXINSTANCES MAXLOGFILES MAXLOGHISTORY MAXLOGMEMBERS MAXTRANS MAXVALUE MIN MINEXTENTS MINUS MINVALUE MLSLABEL MOD MODE MODIFY MODULE MOUNT NATURAL NEW NEXT NEXTVAL NOARCHIVELOG NOAUDIT NOCACHE NOCOMPRESS NOCYCLE NOMAXVALUE NOMINVALUE NONE NOORDER NORESETLOGS NORMAL NOSORT NOT NOTFOUND NOWAIT NULL NUMBER NUMBER_BASE NUMERIC OF OFF OFFLINE OLD ON ONLINE ONLY OPEN OPTIMAL OPTION OR ORDER OTHERS OUT OWN PACKAGE PARALLEL PARTITION PCTFREE PCTINCREASE PCTUSED PLAN PLI POSITIVE PRAGMA PRECISION PRIMARY PRIOR PRIVATE PRIVILEGES PROCEDURE PROFILE PUBLIC QUOTA RAISE RANGE RAW READ REAL RECORD RECOVER REFERENCES REFERENCING RELEASE REMR RENAME RESETLOGS RESOURCE RESTRICTED RETURN REUSE REVERSE REVOKE ROLE ROLES ROLLBACK ROW ROWID ROWLABEL ROWNUM ROWS ROWTYPE RUN SAVEPOINT SCHEMA SCN SECTION SEGMENT SELECT SEPARATE SEQUENCE SESSION SET SHARE SHARED SIZE SMALLINT SNAPSHOT SOME SORT SPACE SQL SQLBUF SQLCODE SQLERRM SQLERROR SQLSTATE START STATEMENT STATEMENT_ID STATISTICS STDDEV STOP STORAGE SUBTYPE SUCCESSFUL SUM SWITCH SYNONYM SYSDATE SYSTEM TABAUTH TABLE TABLES TABLESPACE TASK TEMPORARY TERMINATE THEN THREAD TIME TO TRACING TRANSACTION TRIGGER TRIGGERS TRUNCATE TYPE UID UNDER UNION UNIQUE UNLIMITED UNTIL UPDATE USE USER USING VALIDATE VALUES VARCHAR VARCHAR2 VARIANCE VIEW VIEWS WHEN WHENEVER WHERE WHILE WITH WITHIN WORK WRITE XOR FALSE TRUE ";
                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeText, AttributeDate) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'KeywordsConfig', 'BuiltInKeywords', '" + sKeywords + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                        DBCommon.ExecNonQuery(sSql);
                        break;
                }

                if (MyLibrary.bDarkMode)
                {
                    #region for DarkMode Color
                    MyGlobal.UpdateSetting("GridConfig", "HeadingForeColor", "#000000"); //MyLibrary.sGridHeadingForeColor
                    MyGlobal.UpdateSetting("GridConfig", "EvenRowForeColor", "#000000"); //MyLibrary.sGridEvenRowForeColor
                    MyGlobal.UpdateSetting("GridConfig", "EvenRowBackColor", "#FFFFFF"); //MyLibrary.sGridEvenRowBackColor
                    MyGlobal.UpdateSetting("GridConfig", "OddRowForeColor", "#000000"); //MyLibrary.sGridOddRowForeColor
                    MyGlobal.UpdateSetting("GridConfig", "OddRowBackColor", "#FFFFC1"); //MyLibrary.sGridOddRowBackColor
                    MyGlobal.UpdateSetting("GridConfig", "NullShowColor", "#FFFF00"); //MyLibrary.sGridNullShowColor

                    MyGlobal.UpdateSetting("GlobalConfig", "OptionsTabActiveForeColor", "#000000");
                    MyGlobal.UpdateSetting("GlobalConfig", "OptionsTabActiveBackColor", "#E6FFFF");
                    MyGlobal.UpdateSetting("GlobalConfig", "OptionsTabInactiveForeColor", "#A5A5A5"); //20210120 深色模式，這裡調亮一點

                    MyGlobal.UpdateSetting("EditorConfig", "ToolstripBackground", "#E3FDCA"); //Editor Background
                    MyGlobal.UpdateSetting("EditorConfig", "EditorBackground", "#FFFFFF"); //Editor Background
                    MyGlobal.UpdateSetting("EditorConfig", "CurrentLineBackground", "#FFFFE0"); //Current Line Background
                    MyGlobal.UpdateSetting("EditorConfig", "SelectedTextBackground", "#ADD8E6"); //Selected Text Background
                    MyGlobal.UpdateSetting("EditorConfig", "Comments", "#008000"); //Comments
                    MyGlobal.UpdateSetting("EditorConfig", "TextIdentifier", "#000000"); //Text (Identifier)
                    MyGlobal.UpdateSetting("EditorConfig", "Number", "#800000"); //Number
                    MyGlobal.UpdateSetting("EditorConfig", "OperatorSymbol", "#800000"); //Operator (Symbol)
                    MyGlobal.UpdateSetting("EditorConfig", "OperatorKeywords", "#366092"); //Operator (Keywords)
                    MyGlobal.UpdateSetting("EditorConfig", "String", "#FF0000"); //String (Double Quoted)
                    MyGlobal.UpdateSetting("EditorConfig", "Character", "#FF0000"); //Character (Single Quoted)
                    MyGlobal.UpdateSetting("EditorConfig", "BuiltinFunctions", "#FF00FF"); //Built-in Functions
                    MyGlobal.UpdateSetting("EditorConfig", "BuiltInKeywords", "#0000FF"); //Built-in Keywords
                    MyGlobal.UpdateSetting("EditorConfig", "UserDefinedKeywords", "#0000FF"); //User-defined Keywords
                    MyGlobal.UpdateSetting("EditorConfig", "WhiteSpace", "#00FFFF"); //White Space
                    MyGlobal.UpdateSetting("EditorConfig", "HighlightForeColor", "#000000");
                    #endregion
                }

                sKeywords = "all and any between cross exists in inner is join left like not null or outer pivot right some unpivot ( ) *";
                sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeText, AttributeDate) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'KeywordsConfig', 'OperatorKeywords', '" + sKeywords + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                DBCommon.ExecNonQuery(sSql);

                //AR範例
                #region AR範例
                sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'AutoReplaceConfig', 'AutoReplace', 'sf" + MyGlobal.sSeparator3s + "select * from')";
                DBCommon.ExecNonQuery(sSql);
                sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'AutoReplaceConfig', 'AutoReplace', 'sfe" + MyGlobal.sSeparator3s + "select * from employee')";
                DBCommon.ExecNonQuery(sSql);
                sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'AutoReplaceConfig', 'AutoReplace', 'sfew" + MyGlobal.sSeparator3s + "select * from employee\r\nwhere name like ''J^%''')";
                DBCommon.ExecNonQuery(sSql);

                switch (MyGlobal.sDataSource)
                {
                    case "PostgreSQL":
                    case "MySQL":
                    case "SQLite":
                    case "SQLCipher":
                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'AutoReplaceConfig', 'AutoReplace', 'l1" + MyGlobal.sSeparator3s + "limit 1')";
                        DBCommon.ExecNonQuery(sSql);
                        break;
                    case "SQL Server":
                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'AutoReplaceConfig', 'AutoReplace', 'stf" + MyGlobal.sSeparator3s + "select top 100 * from')";
                        DBCommon.ExecNonQuery(sSql);
                        break;
                    default:
                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'AutoReplaceConfig', 'AutoReplace', 'r1" + MyGlobal.sSeparator3s + "rownum<=1')";
                        DBCommon.ExecNonQuery(sSql);

                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'AutoReplaceConfig', 'AutoReplace', 'w1" + MyGlobal.sSeparator3s + "where rownum<=1')";
                        DBCommon.ExecNonQuery(sSql);
                        break;
                }
                #endregion

                //Generate SQL 預設值
                #region Generate SQL 預設值
                switch (MyGlobal.sDataSource)
                {
                    case "PostgreSQL":
                    case "MySQL":
                    case "SQLite":
                    case "SQLCipher":
                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'GenerateSQLConfig', 'ConvertCase', 'UpperKeywords')";
                        DBCommon.ExecNonQuery(sSql);
                        break;
                    default: //Oracle...
                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'GenerateSQLConfig', 'ConvertCase', 'UpperAll')";
                        DBCommon.ExecNonQuery(sSql);
                        break;
                }

                sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'GenerateSQLConfig', 'Numbers', '5')";
                DBCommon.ExecNonQuery(sSql);
                #endregion

                sSelectCondition += ", 'KeywordsConfig'";

                //重新再 Select 一次
                sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey IN (" + sSelectCondition + ")";
                dtTemp = DBCommon.ExecQuery(sSql);
            }
            #endregion

            //載入設定值 part 2
            #region "載入 General Config 設定值"
            sSql = "AttributeKey = 'GeneralConfig' AND AttributeName = 'DarkMode'";
            var dtRow = dtTemp.Select(sSql);
            MyLibrary.bDarkMode = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            if (MyLibrary.bDarkMode)
            {
                C1.Win.C1Themes.C1ThemeController.ApplicationTheme = "VS2013Dark";
                c1ThemeController1.SetTheme(tabControl1, "VS2013Dark");
                c1ThemeController1.SetTheme(mnuMainForm, "ExpressionLight");
                c1ThemeController1.SetTheme(c1StatusBar1, "ExpressionLight");
            }

            sSql = "AttributeKey = 'GeneralConfig' AND AttributeName = 'AutoDisconnect'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sAutoDisconnect = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "Never";
            MyGlobal.sAutoDisconnect = MyGlobal.GetValueFromDictionary(MyGlobal.dicAutoDisconnect, MyLibrary.sAutoDisconnect);

            if (MyLibrary.sAutoDisconnect == "Never")
            {
                tmrCheckIdleTime.Enabled = false;
            }
            else
            {
                MyGlobal.iAutoDisconnect = Convert.ToInt16(MyLibrary.sAutoDisconnect.Substring(0, 1)) * 60 * 60 * 1000;
                tmrCheckIdleTime.Enabled = true;
            }

            sSql = "AttributeKey = 'GeneralConfig' AND AttributeName = 'SpecifiedSQLFile1'";
            dtRow = dtTemp.Select(sSql);
            MyGlobal.sSpecifiedSQLFile1 = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GeneralConfig' AND AttributeName = 'SpecifiedSQLFile2'";
            dtRow = dtTemp.Select(sSql);
            MyGlobal.sSpecifiedSQLFile2 = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";
            #endregion

            #region "載入 Query Editor 設定值"
            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'ToolstripBackground'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorToolstripBackground = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //淡黃

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'EditorBackground'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorEditorBackground = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //White

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'CurrentLineBackground'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorCurrentLineBackground = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //LightYellow

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'SelectedTextBackground'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorSelectedTextBackground = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //LightBlue

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'ErrorLineBackground'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorErrorLineBackground = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Red

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'BookmarkBackground'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorBookmarkBackground = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Cyan

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'BookmarkStyle'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sBookmarkStyle = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            MyGlobal.sBookmarkStyle = MyGlobal.GetValueFromDictionary(MyGlobal.dicBookmarkStyle, MyLibrary.sBookmarkStyle);

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'Comments'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorComments = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Green

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'TextIdentifier'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorTextIdentifier = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Text, Black

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'BuiltInKeywords'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorBuiltInKeywords = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //LightSeaGreen

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'UserDefinedKeywords'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorUserDefinedKeywords = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //LightSeaGreen

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'Number'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorNumber = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Maroon

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'OperatorSymbol'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorOperatorSymbol = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Black

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'OperatorKeywords'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorOperatorKeywords = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Black

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'String'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorString = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Red

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'Character'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorCharacter = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Red

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'BuiltinFunctions'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorBuiltInFunctions = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Magenta

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'WhiteSpace'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorWhiteSpace = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Cyan

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'UserDefinedTables'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorUserDefinedTablesViews = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Olive

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'UserDefinedFunctions'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sColorUserDefinedFunctionsTirggers = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Olive

            //Query Editor 頁籤：載入 Highlight 設定
            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'HighlightForeColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sHighlightColorForeColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : ""; //Color.Blue

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'HighlightStyle'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sHighlightColorStyle = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'HighlightOutlineAlpha'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sHighlightColorOutlineAlpha = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'HighlightAlpha'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sHighlightColorAlpha = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            //Query Editor 頁籤：載入 Preferences 設定
            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'EditorFontName'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sQueryEditorFontName = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'EditorFontSize'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sQueryEditorFontSize = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'EditorZoom'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sQueryEditorZoom = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'WordWrap'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bWordWrap = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'WordWrapVisualFlags_Start'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bWordWrapVisualFlags_Start = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'WordWrapVisualFlags_End'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bWordWrapVisualFlags_End = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'WordWrapVisualFlags_Margin'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bWordWrapVisualFlags_Margin = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'WordWrapIndentMode'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sWordWrapIndentMode = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            MyGlobal.sWordWrapIndentMode = MyGlobal.GetValueFromDictionary(MyGlobal.dicWordWrapIndentMode, MyLibrary.sWordWrapIndentMode);

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'KeywordFontBold'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bKeywordFontBold = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'CopyAsHTML'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bCopyAsHTML = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'ShowAllCharacters'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bShowAllCharacters = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'ShowSaveAsButton'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bShowSaveAsButton = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'ShowIndentGuide'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bShowIndentGuide = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'TabWidth'";
            dtRow = dtTemp.Select(sSql);
            MyGlobal.iTabWidth = dtRow.Length > 0 ? Convert.ToInt32(dtRow[0]["AttributeValue"].ToString()) : 4;

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'EntireBlankRowAsEmptyRow4SelectBlock'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bEntireBlankRowAsEmptyRow = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'HighlightSelection'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bHighlightSelection = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'SortByColumnName'";
            dtRow = dtTemp.Select(sSql);
            MyGlobal.bSortByColumnName = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'DefaultTabSchemaBrowser'";
            dtRow = dtTemp.Select(sSql);
            MyGlobal.bDefaultTabSchemaBrowser = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'AutoListMembers'";
            dtRow = dtTemp.Select(sSql);
            MyGlobal.bAutoListMembers = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'SavePoint'";
            dtRow = dtTemp.Select(sSql);
            MyGlobal.bSavePoint = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            if (MyGlobal.sDataSource != "PostgreSQL")
            {
                MyGlobal.bSavePoint = false;
            }

            sSql = "AttributeKey = 'EditorConfig' AND AttributeName = 'AfterPasteFocusOnQueryEditor'";
            dtRow = dtTemp.Select(sSql);
            MyGlobal.bAfterPasteFocusOnQueryEditor = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";
            #endregion

            #region 載入 Auto Complete 設定值
            //載入 Auto Complete Config
            sSql = "AttributeKey = 'AutoCompleteConfig' AND AttributeName = 'EnableAutoComplete2'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bEnableAutoComplete = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";
            //MyLibrary.bEnableAutoComplete = false; //20220109 停用此功能！(有 BUG，暫時停用；後續要再改寫過)

            sSql = "AttributeKey = 'AutoCompleteConfig' AND AttributeName = 'MinFragmentLength2'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.iACMinFragmentLength = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 2; //最小觸發的長度

            sSql = "AttributeKey = 'AutoCompleteConfig' AND AttributeName = 'FirstCharChecking2'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bACFirstCharChecking = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            //frmMain, 載入時，依此變數判斷要不要載入設定值
            //20201231 Built-In Keywords && Functions, 同一個變數
            sSql = "AttributeKey = 'AutoCompleteConfig' AND AttributeName = 'BuiltInKeywords2'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bACBuiltInKeywords = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'AutoCompleteConfig' AND AttributeName = 'BuiltInFunctions2'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bACBuiltInFunctions = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            //frmMain, 載入時，依此變數判斷要不要載入設定值
            sSql = "AttributeKey = 'AutoCompleteConfig' AND AttributeName = 'UserDefinedKeywords2'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bACUserDefinedKeywords = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            //frmMain, 載入時，依此變數判斷要不要載入設定值
            sSql = "AttributeKey = 'AutoCompleteConfig' AND AttributeName = 'UserDefinedFunctions2'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bACUserDefinedFunctions = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            //frmMain, 載入時，依此變數判斷要不要載入設定值
            sSql = "AttributeKey = 'AutoCompleteConfig' AND AttributeName = 'UserDefinedTables2'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bACUserDefinedTables = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            //frmMain, 載入時，依此變數判斷要不要載入設定值
            sSql = "AttributeKey = 'AutoCompleteConfig' AND AttributeName = 'UserDefinedTriggers2'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bACUserDefinedTriggers = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            //frmMain, 載入時，依此變數判斷要不要載入設定值
            sSql = "AttributeKey = 'AutoCompleteConfig' AND AttributeName = 'UserDefinedViews2'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bACUserDefinedViews = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";
            #endregion

            #region 載入 Auto Replace 設定值 (此處只要載入「是否啟用」即可，其他的在 frmQuery 確認／載入)
            sSql = "AttributeKey = 'AutoReplaceConfig' AND AttributeName = 'EnableAutoReplace'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bEnableAutoReplace = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            //檢查 AutoReplace 的符號
            sSql = "SELECT PID, AttributeValue FROM SystemConfig WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'AutoReplaceConfig' AND AttributeName = 'AutoReplace'";
            sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID);
            var dt = DBCommon.ExecQuery(sSql);

            if (dt.Rows.Count > 0)
            {
                for (var iRow = 0; iRow < dt.Rows.Count; iRow++)
                {
                    var sAttributeValue = dt.Rows[iRow]["AttributeValue"].ToString();

                    //檢查是否有舊的分隔符號，有的話，改為新的分隔符號 (舊的分隔符號會影響 MySQL)
                    if (sAttributeValue.IndexOf(MyGlobal.sSeparator3s, StringComparison.Ordinal) == -1)
                    {
                        sSql = "UPDATE SystemConfig SET AttributeValue = '{0}' WHERE PID = {1}";
                        sSql = string.Format(sSql, sAttributeValue.Replace("`", MyGlobal.sSeparator3s).Replace("'", "''"), dt.Rows[iRow]["PID"]);
                        DBCommon.ExecNonQuery(sSql);
                    }
                }
            }
            #endregion

            #region 載入 Grid 設定值
            //載入 Grid 設定
            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'QuotingWith'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridQuotingWith = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'FieldSeparator'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridFieldSeparator = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ShowColumnDataType'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridShowColumnDataType = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ShowStreamlinedName'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridShowStreamlinedName = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1" && MyGlobal.sDataSource == "PostgreSQL";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ShowFilterRow'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridShowFilterRow = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ShowGroupingRow'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridShowGroupingRow = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'Resize'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridResize = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'MaxWidth'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridMaxWidth = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "500";
            _sLangText = MyGlobal.GetLanguageString("Unlimited", "form", "frmOptions", "dropdownlist", "cboMaxWidth", "Text");

            MyGlobal.sGridMaxWidth = "`500`1000`1500`2000`".Contains("`" + MyLibrary.sGridMaxWidth + "`") == false ? _sLangText : MyLibrary.sGridMaxWidth;

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'Sort'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridSort = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'PreviewCLOBData'";
            dtRow = dtTemp.Select(sSql);
            MyGlobal.bPreviewCLOBData = dtRow.Length == 0 || dtRow[0]["AttributeValue"].ToString() != "0"; //20220705 變更為預設顯示 CLOB data

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'RawDataMode'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridRawDataMode = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'NullShowAs'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridNullShowAs = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'PagingQuery'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridPagingQuery = dtRow.Length == 0 || dtRow[0]["AttributeValue"].ToString() != "0"; //預設啟用「分頁查詢」功能

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'RowsPerPage'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridRowsPerPage = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "500";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'AppendingQueries'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridAppendingQueries = dtRow.Length == 0 || dtRow[0]["AttributeValue"].ToString() != "0"; //預設啟用「附加查詢」功能

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'SetFocusAfterQuery'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridSetFocusAfterQuery = dtRow.Length != 0 && dtRow[0]["AttributeValue"].ToString() == "1"; //預設啟用「查詢後切換至 Grid」功能

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'NullShowColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridNullShowColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'VisualStyle'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridVisualStyle = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'Zoom'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridZoom = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'FontName'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridFontName = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'FontSize'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridFontSize = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'SheetName'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridSheetName = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'HeadingForeColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridHeadingForeColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'EvenRowForeColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridEvenRowForeColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'EvenRowBackColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridEvenRowBackColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'OddRowForeColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridOddRowForeColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'OddRowBackColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridOddRowBackColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'HighlightForeColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridHighlightForeColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'HighlightBackColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridHighlightBackColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'SelectedForeColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridSelectedForeColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'SelectedBackColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridSelectedBackColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'RowResizing'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sRowSizing = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";
            MyGlobal.sRowSizing = MyGlobal.GetValueFromDictionary(MyGlobal.dicRowSizing, MyLibrary.sRowSizing);

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ExcelFilename'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridExcelFilename = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ExcelSaveAsType'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridExcelSaveAsType = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "Excel 2007 (*.xlsx)";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ExcelWorksheetName'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridExcelWorksheetName = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "data";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'CSVDelimiters'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridCSVDelimiters = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "Comma";

            MyGlobal.sCSVDelimiters = MyGlobal.GetValueFromDictionary(MyGlobal.dicCSVDelimiters, MyLibrary.sGridCSVDelimiters);

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'Encoding'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridEncoding = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "UTF-8";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ExcelAutoOpen'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridExcelAutoOpen = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ExcelAutoColumnResize'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridExcelAutoColumnResize = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ExcelCustom'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bGridExcelCustom = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ExcelHeadingBackColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridExcelHeadingBackColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "LightSkyBlue";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ExcelEvenRowBackColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridExcelEvenRowBackColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "White";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ExcelOddRowBackColor'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridExcelOddRowBackColor = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "LightYellow";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ExcelFontName'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridExcelFontName = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "Consolas";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ExcelFontSize'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridExcelFontSize = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "12";

            sSql = "AttributeKey = 'GridConfig' AND AttributeName = 'ExcelRowHeight'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sGridExcelRowHeight = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "20";
            #endregion

            #region 載入 Keywords 設定值
            //載入 Keywords 設定值
            sSql = "AttributeKey = 'KeywordsConfig' AND AttributeName = 'OperatorKeywords'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sKeywordsOperatorKeywords = dtRow.Length > 0 ? dtRow[0]["AttributeText"].ToString() : "";

            sSql = "AttributeKey = 'KeywordsConfig' AND AttributeName = 'UserDefinedKeywords'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sKeywordsUserDefinedKeywords = dtRow.Length > 0 ? dtRow[0]["AttributeText"].ToString() : "";

            sSql = "AttributeKey = 'KeywordsConfig' AND AttributeName = 'BuiltInFunctions'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sKeywordsBuiltInFunctions = dtRow.Length > 0 ? dtRow[0]["AttributeText"].ToString() : "";

            sSql = "AttributeKey = 'KeywordsConfig' AND AttributeName = 'BuiltInKeywords'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sKeywordsBuiltInKeywords = dtRow.Length > 0 ? dtRow[0]["AttributeText"].ToString() : "";
            #endregion

            #region 載入 SQL to Code 設定值
            sSql = "AttributeKey = 'SQL2CodeConfig' AND AttributeName = 'VariableName'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sSQLToCodeVariableName = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "";
            #endregion

            #region 載入 SQL Formatter 設定值
            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'IndentString'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.sSQLFormatterIndentString = dtRow.Length > 0 ? dtRow[0]["AttributeValue"].ToString() : "\t";

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'SpacesPerTab'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.iSQLFormatterSpacesPerTab = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 4;

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'MaxLineWidth'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.iSQLFormatterMaxLineWidth = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 999;

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'NewStatementLineBreaks'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.iSQLFormatterNewStatementLineBreaks = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 2;

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'NewClauseLineBreaks'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.iSQLFormatterNewClauseLineBreaks = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 1;

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'ExpandCommaLists'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bSQLFormatterExpandCommaLists = dtRow.Length > 0 && dtRow[0]["AttributeValue"].ToString() == "1";

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'TrailingCommas'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bSQLFormatterTrailingCommas = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'ExpandBooleanExpressions'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bSQLFormatterExpandBooleanExpressions = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'ExpandCaseStatements'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bSQLFormatterExpandCaseStatements = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'ExpandBetweenConditions'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bSQLFormatterExpandBetweenConditions = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'ExpandInLists'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bSQLFormatterExpandInLists = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'BreakJoinOnSections'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bSQLFormatterBreakJoinOnSections = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'ConvertCaseForKeywords'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.bSQLFormatterConvertCaseForKeywords = dtRow.Length <= 0 || dtRow[0]["AttributeValue"].ToString() != "0";

            sSql = "AttributeKey = 'SQLFormatterConfig' AND AttributeName = 'ConvertCaseForKeywordsCase'";
            dtRow = dtTemp.Select(sSql);
            MyLibrary.iSQLFormatterConvertCaseForKeywordsCase = dtRow.Length > 0 ? Convert.ToInt16(dtRow[0]["AttributeValue"].ToString()) : 1;
            #endregion

            #region Generate SQL 預設值
            sSql = "AttributeKey = 'GenerateSQLConfig' AND AttributeName = 'ConvertCase'";
            dtRow = dtTemp.Select(sSql);

            if (dtRow.Length == 0) //for 舊的使用者
            {
                switch (MyGlobal.sDataSource)
                {
                    case "PostgreSQL":
                    case "MySQL":
                    case "SQLite":
                    case "SQLCipher":
                        MyLibrary.sGenerateSQLConvertCase = "UpperKeywords";

                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('{0}', {1}, 'GenerateSQLConfig', 'ConvertCase', 'UpperKeywords')";
                        sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID);
                        DBCommon.ExecNonQuery(sSql);
                        break;
                    default: //Oracle...
                        MyLibrary.sGenerateSQLConvertCase = "UpperAll";

                        sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('{0}', {1}, 'GenerateSQLConfig', 'ConvertCase', 'UpperAll')";
                        sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID);
                        DBCommon.ExecNonQuery(sSql);
                        break;
                }
            }
            else
            {
                MyLibrary.sGenerateSQLConvertCase = dtRow[0]["AttributeValue"].ToString();
            }

            sSql = "AttributeKey = 'GenerateSQLConfig' AND AttributeName = 'Numbers'";
            dtRow = dtTemp.Select(sSql);

            if (dtRow.Length == 0) //for 舊的使用者
            {
                MyLibrary.iGenerateSQLNumbers = 5;

                sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue) VALUES ('{0}', {1}, 'GenerateSQLConfig', 'Numbers', '5')";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID);
                DBCommon.ExecNonQuery(sSql);
            }
            else
            {
                MyLibrary.iGenerateSQLNumbers = Convert.ToInt16(dtRow[0]["AttributeValue"].ToString());
            }
            #endregion

            Cursor = Cursors.Default;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            HookManager.KeyDown -= HookManager_KeyDown;
            MessageBoxManager.Unregister();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //記錄位置
            if (Location.X > 0)
            {
                MyGlobal.UpdateSetting("GlobalConfig", "MainFormLocationX", Location.X.ToString());
            }

            if (Location.Y > 0)
            {
                MyGlobal.UpdateSetting("GlobalConfig", "MainFormLocationY", Location.Y.ToString());
            }
        }

        private static List<ToolStripMenuItem> GetItems(ToolStrip menuStrip)
        {
            var myItems = new List<ToolStripMenuItem>();

            foreach (ToolStripMenuItem i in menuStrip.Items)
            {
                GetMenuItems(i, myItems);
            }

            return myItems;
        }

        private static void GetMenuItems(ToolStripMenuItem item, ICollection<ToolStripMenuItem> items)
        {
            items.Add(item);

            foreach (ToolStripItem i in item.DropDownItems)
            {
                if (i is ToolStripMenuItem menuItem)
                {
                    GetMenuItems(menuItem, items);
                }
            }
        }

        private void CloseIt(object sender, EventArgs e) //在 Tab 上按右鍵關閉
        {
            var bCancelClose = false;
            var bCloseOptionsForm = false; //要關閉的是否為 Options Form?

            if (tabControl1.TabPages.Count > 0)
            {
                if (tabControl1.SelectedTab.Title.Trim().Substring(0, 1) == "*")
                {
                    //關閉 Query Form !
                    MyGlobal.sGlobalTemp = "closequeryform`" + tabControl1.SelectedTab.AccessibleDescription + ";";

                    while (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp))
                    {
                        if (MyGlobal.sGlobalTemp.Contains(tabControl1.SelectedTab.AccessibleDescription + "|CANCEL;"))
                        {
                            bCancelClose = true;
                            break;
                        }

                        Application.DoEvents();
                    }

                    if (bCancelClose == false)
                    {
                        tabControl1.SelectedTab.Dispose();
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);

                        ClearMemory(); //CloseIt (with *)
                        UpdateMainMenuTitle(); //CloseIt (with *)
                    }
                }
                else
                {
                    if (tabControl1.SelectedTab.Title.Trim() == MyGlobal.sNameSchemaBrowser)
                    {
                        //此處可能需要關閉連線
                        MyGlobal.sGlobalTemp = "NoSplit"; //不要觸發 Split event
                        MyGlobal.sFormSchemaBrowserKey = "";
                        mnuSchemaBrowser.Enabled = true;
                        mnuSchemaBrowser.Checked = false;
                    }
                    else if (tabControl1.SelectedTab.Title.Trim() == MyGlobal.sNameOptions)
                    {
                        mnuOptions.Enabled = true;
                        mnuOptions.Checked = false;

                        bCloseOptionsForm = true;

                        //Options Form 要用特殊方法
                        MyGlobal.sGlobalTemp = "closeoptionstab"; //告訴 Options Form 取消套用，再由 Options Form 回傳關閉 Tab 的指令
                    }
                    else if (tabControl1.SelectedTab.Title.Trim() == MyGlobal.sNameSQLHistory)
                    {
                        mnuSQLHistory.Enabled = true;
                        mnuSQLHistory.Checked = false;
                    }
                    else
                    {
                        //此 Query Form 不需要存檔，直接關閉即可!
                        MyGlobal.sGlobalTemp = "closequeryform`" + tabControl1.SelectedTab.AccessibleDescription + ";";

                        while (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp))
                        {
                            Application.DoEvents();
                        }
                    }

                    if (bCloseOptionsForm == false)
                    {
                        tabControl1.SelectedTab.Dispose();
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);

                        ClearMemory(); //CloseIt
                        UpdateMainMenuTitle(); //CloseIt
                    }

                    if (MyGlobal.sGlobalTemp == "NoSplit")
                    {
                        MyGlobal.sGlobalTemp = "";
                    }
                }
            }

            var iTabCount = tabControl1.TabPages.Cast<TabPage>().Count(theTab => theTab.Title == MyGlobal.sNameOptions || theTab.Title == MyGlobal.sNameSQLHistory || theTab.Title == MyGlobal.sNameSchemaBrowser);

            if (tabControl1.TabPages.Count != 0 && tabControl1.TabPages.Count != iTabCount)
            {
                return;
            }

            //20190928 改為：如果把最後一個 Tab 關閉，自動再開啟一個空白的 Tab
            var ss = CheckTabNameExist();

            if (CheckTabNameExist(ss) == false)
            {
                CreateNewTab("Query", ss);
            }
        }

        private void CreateNewTab(string sFunction, string sTabTitle, string sOptional = "")
        {
            Control controlToAdd = null;
            var sAccessibleDescription = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            switch (sFunction)
            {
                case "Query":
                case "Query1":
                case "Query2":
                case "SQLEditor":
                    _f1 = new frmQuery
                    {
                        MdiParent = this,
                        Tag = sTabTitle,
                        AccessibleDefaultActionDescription = ""
                    };

                    if (sFunction == "SQLEditor")
                    {
                        _f1.AccessibleDefaultActionDescription = "SQL:" + sOptional; //SQL Statement
                    }
                    else if ("`Query`Query1`Query2`".Contains("`" + sFunction +"`"))
                    {
                        if (!string.IsNullOrEmpty(sOptional))
                        {
                            switch (sFunction)
                            {
                                case "Query1":
                                    _f1.AccessibleDefaultActionDescription = "OPEN1:" + sOptional; //Recent File
                                    break;
                                case "Query2":
                                    _f1.AccessibleDefaultActionDescription = "OPEN2:" + sOptional; //My Favorite
                                    break;
                                default:
                                    _f1.AccessibleDefaultActionDescription = "OPEN0:" + sOptional; //一般 Open File
                                    break;
                            }
                        }
                    }

                    _f1.AccessibleDescription = sAccessibleDescription;

                    _f1.ValueUpdated += uValueUpdated;

                    controlToAdd = _f1;
                    break;
                case "":
                    break;
                default:
                    if (sFunction == MyGlobal.sNameSchemaBrowser)
                    {
                        _f2 = new frmSchemaBrowser
                        {
                            MdiParent = this, Tag = sTabTitle, AccessibleDescription = sAccessibleDescription
                        };

                        MyGlobal.sFormSchemaBrowserKey = sAccessibleDescription;

                        _f2.ValueUpdated += uValueUpdated;

                        controlToAdd = _f2;
                    }
                    else if (sFunction == MyGlobal.sNameSQLHistory)
                    {
                        _f3 = new frmSQLHistory {MdiParent = this, AccessibleDescription = sAccessibleDescription};
                        MyGlobal.sFormSQLHistoryKey = sAccessibleDescription;

                        _f3.ValueUpdated += uValueUpdated;

                        controlToAdd = _f3;
                    }
                    else if (sFunction == MyGlobal.sNameOptions)
                    {
                        _f4 = new frmOptions {MdiParent = this, AccessibleDescription = sAccessibleDescription};
                        MyGlobal.sFormOptionsKey = sAccessibleDescription;

                        _f4.ValueUpdated += uValueUpdated;

                        controlToAdd = _f4;
                    }

                    break;
            }

            var page = new TabPage(sTabTitle, controlToAdd, null, 1)
            {
                Selected = true,

                Tag = sTabTitle,
                AccessibleDescription = sAccessibleDescription
            };

            tabControl1.TabPages.Add(page);

            tsMainMenuToolBar.Visible = false;
            EnableCloseTabMenu(true); //CreateNewTab
        }

        private string CheckTabNameExist()
        {
            var sValue = (from TabPage theTab in tabControl1.TabPages where theTab.Title.Trim().StartsWith("SQL Editor") || theTab.Title.Trim().StartsWith(@"*SQL Editor") select theTab.Title.Replace(@"*SQL Editor", "").Trim() into sTemp select sTemp.Replace("SQL Editor", "").Trim()).Aggregate("", (current, sTemp) => current + sTemp + @",");

            if (string.IsNullOrEmpty(sValue))
            {
                return "SQL Editor 1";
            }

            var sInfo = sValue.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(sInfo);

            sValue = sInfo[sInfo.Length - 1].ToLower().Replace(@".sql", ""); //取最大值
            var iValue = Convert.ToInt16(sValue) + 1;
            sValue = Convert.ToString(iValue);

            return "SQL Editor " + sValue;
        }

        private bool CheckTabNameExist(string sTabName)
        {
            foreach (TabPage theTab in tabControl1.TabPages)
            {
                //20190930 改用 full path + filename 判斷是否已存在，因為有可能是同檔名但不同路徑
                if (theTab.Tag.ToString().Replace(@"*", "") != sTabName)
                {
                    continue;
                }

                theTab.Selected = true;
                return true;
            }

            return false;
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (tabControl1.TabPages.Count > 0)
                {
                    EnableCloseTabMenu(true, true); //tabControl1_MouseDown
                }
                else
                {
                    return;
                }

                _cMenu.Show(tabControl1, new Point(e.X, e.Y));
            }
            else
            {
                UpdateMainMenuTitle(); //tabControl1_MouseDown
            }
        }

        private void UpdateMainMenuTitle()
        {
            foreach (TabPage theTab in tabControl1.TabPages)
            {
                if (!theTab.Selected)
                {
                    continue;
                }

                //20190909 點選到的 Tab，檢查檔案是否有被外部程式異動內容，或是被刪除了
                if (("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + theTab.Title + "`") == false)
                {
                    MyGlobal.sCheckFileFromMDIForm = "`" + theTab.AccessibleDescription + "`";
                }
            }
        }

        private void tabControl1_ClosePressed(object sender, EventArgs e)
        {
            var bCancelClose = false;
            var bCloseOptionsForm = false; //要關閉的是否為 Options Form？

            if (tabControl1.TabPages.Count > 0)
            {
                if (tabControl1.SelectedTab.Title.Trim().Substring(0, 1) == "*")
                {
                    //關閉 Query Form !
                    MyGlobal.sGlobalTemp = "closequeryform`" + tabControl1.SelectedTab.AccessibleDescription + ";";

                    while (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp))
                    {
                        if (MyGlobal.sGlobalTemp.Contains(tabControl1.SelectedTab.AccessibleDescription + "|CANCEL;"))
                        {
                            bCancelClose = true;
                            break;
                        }

                        Application.DoEvents();
                    }

                    if (bCancelClose == false)
                    {
                        tabControl1.SelectedTab.Dispose();
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);

                        ClearMemory(); //CloseIt (with *)
                        UpdateMainMenuTitle(); //tabControl1_ClosePressed (with *)
                    }
                }
                else
                {
                    if (tabControl1.SelectedTab.Title.Trim() == MyGlobal.sNameSchemaBrowser)
                    {
                        MyGlobal.sGlobalTemp = "NoSplit"; //不要觸發 Split event
                        MyGlobal.sFormSchemaBrowserKey = "";
                        mnuSchemaBrowser.Enabled = true;
                        mnuSchemaBrowser.Checked = false;
                    }
                    else if (tabControl1.SelectedTab.Title.Trim() == MyGlobal.sNameOptions)
                    {
                        mnuOptions.Enabled = true;
                        mnuOptions.Checked = false;
                        bCloseOptionsForm = true;

                        //Options Form 要用特殊方法關閉
                        MyGlobal.sGlobalTemp = "closeoptionstab"; //告訴 Options Form 取消套用，再由 Options Form 回傳關閉 Tab 的指令
                    }
                    else if (tabControl1.SelectedTab.Title.Trim() == MyGlobal.sNameSQLHistory)
                    {
                        mnuSQLHistory.Enabled = true;
                        mnuSQLHistory.Checked = false;
                    }

                    if (bCloseOptionsForm == false)
                    {
                        tabControl1.SelectedTab.Dispose();
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);

                        ClearMemory(); //CloseIt
                        UpdateMainMenuTitle(); //tabControl1_ClosePressed
                    }

                    if (MyGlobal.sGlobalTemp == "NoSplit") { MyGlobal.sGlobalTemp = ""; }
                }
            }

            if (tabControl1.TabPages.Count != 0)
            {
                return;
            }

            var ss = CheckTabNameExist();

            if (CheckTabNameExist(ss) == false)
            {
                CreateNewTab("Query", ss);
            }
        }

        private void EnableCloseTabMenu(bool bEnable, bool bCheckFavorite = false)
        {
            var bSaved = false;
            var sFullFilename = "";

            _cMenu.MenuItems[(int)eCol.Close].Enabled = bEnable;
            tabControl1.ShowClose = bEnable;

            _cMenu.MenuItems[(int)eCol.NewSQLEditor].Enabled = true;

            if (!bCheckFavorite)
            {
                return;
            }

            foreach (TabPage theTab in tabControl1.TabPages)
            {
                if (!theTab.Selected)
                {
                    continue;
                }

                //兩者不相等，表示有存檔過了
                if (theTab.Title.Replace("*", "") == theTab.Tag.ToString().Replace("*", ""))
                {
                    continue;
                }

                //略過以下幾個，不需要加入我的最愛
                if (("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + theTab.Title + "`") != false)
                {
                    continue;
                }

                bSaved = true;
                sFullFilename = theTab.Tag.ToString();
            }

            var bValue = false;

            if (bSaved)
            {
                bValue = true;
                var bExist = CheckExist4MyFavoriteFiles(sFullFilename);
                _cMenu.MenuItems[(int)eCol.AddToMyFavorite].Enabled = !bExist;
                _cMenu.MenuItems[(int)eCol.RemoveFromMyFavorite].Enabled = bExist;
            }
            else
            {
                _cMenu.MenuItems[(int)eCol.AddToMyFavorite].Enabled = false;
                _cMenu.MenuItems[(int)eCol.RemoveFromMyFavorite].Enabled = false;
            }

            _cMenu.MenuItems[(int)eCol.AddToMyFavorite].Tag = sFullFilename;
            _cMenu.MenuItems[(int)eCol.RemoveFromMyFavorite].Tag = sFullFilename;
            _cMenu.MenuItems[(int)eCol.OpenFolder].Enabled = bValue;
            _cMenu.MenuItems[(int)eCol.OpenFolder].Tag = sFullFilename;
            _cMenu.MenuItems[(int)eCol.CopyFullFilePath].Enabled = bValue;
            _cMenu.MenuItems[(int)eCol.CopyFullFilePath].Tag = sFullFilename;
            _cMenu.MenuItems[(int)eCol.CopyFilename].Enabled = bValue;
            _cMenu.MenuItems[(int)eCol.CopyFilename].Tag = sFullFilename;
            _cMenu.MenuItems[(int)eCol.CopyCurrentPath].Enabled = bValue;
            _cMenu.MenuItems[(int)eCol.CopyCurrentPath].Tag = sFullFilename;
        }

        private void NewSqlEditor(object sender, EventArgs e)
        {
            CreateNewTab("Query", CheckTabNameExist());
        }

        private static bool CheckExist4MyFavoriteFiles(string sFullFilename)
        {
            var bResult = false;
            var sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'MyFavoriteFiles' AND AttributeText = '{2}'";
            sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID, sFullFilename.Replace("'", "''"));
            var dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0)
            {
                bResult = true;
            }

            return bResult;
        }

        private void AddToMyFavoriteFiles(object sender, EventArgs e)
        {
            if (!(sender is MenuItem mnuItem))
            {
                return;
            }

            var sFilename = mnuItem.Tag.ToString();

            //去掉最前面的 * 號
            if (sFilename.Substring(0, 1) == "*")
            {
                sFilename = sFilename.Substring(1, sFilename.Length - 1);
            }

            var sAliasName = "";
            var sTitle = MyGlobal.GetLanguageString("Add to \"My Favorite\"", "form", Name, "menu", "AddToMyFavorite", "Text");
            var sPromptText = MyGlobal.GetLanguageString("alias", "form", Name, "msg", "AliasName", "Text");
            var sPromptText2 = MyGlobal.GetLanguageString("file", "form", Name, "msg", "FileName", "Text");

            if (AliasName(sTitle, sPromptText, sPromptText2, Cursor.Position.X, Cursor.Position.Y, sFilename, ref sAliasName) != DialogResult.OK)
            {
                return;
            }

            SaveMyFavoriteFiles(sFilename, false, false, sAliasName);
        }

        private static DialogResult AliasName(string sTitle, string sPromptText, string sPromptText2, int iX, int iY, string sFilename, ref string sAliasName)
        {
            var form = new Form();
            var lblAliasName = new Label();
            var lblFilename = new Label();
            var txtAliasName = new TextBox();
            var txtFilename = new TextBox();
            var btnOk = new C1.Win.C1Input.C1Button();
            var btnCancel = new C1.Win.C1Input.C1Button();

            form.Text = sTitle;
            form.ClientSize = new Size(400, 215);
            form.Controls.AddRange(new Control[] { lblAliasName, lblFilename, txtAliasName, txtFilename, btnOk, btnCancel });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.Font = new Font("Microsoft JhengHei", 9F, FontStyle.Regular, GraphicsUnit.Point, 136);

            lblAliasName.Text = sPromptText;
            txtAliasName.Text = "";

            lblFilename.Text = sPromptText2;
            txtFilename.Text = sFilename;
            txtFilename.ReadOnly = true;

            var sName = MyGlobal.GetLanguageString("&OK", "Global", "Global", "messagebox", "OK", "Text");
            btnOk.Text = sName;
            sName = MyGlobal.GetLanguageString("&Cancel", "Global", "Global", "messagebox", "Cancel", "Text");
            btnCancel.Text = sName;

            btnOk.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;

            lblAliasName.SetBounds(14, 18, 372, 13);
            lblFilename.SetBounds(14, 85, 372, 13);
            lblAliasName.Font = new Font("Microsoft JhengHei", 9F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lblFilename.Font = new Font("Microsoft JhengHei", 9F, FontStyle.Bold, GraphicsUnit.Point, 136);
            btnOk.SetBounds(215, 160, 75, 37);
            btnCancel.SetBounds(304, 160, 75, 37);

            lblAliasName.AutoSize = true;
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            txtAliasName.SetBounds(16, 39, 150, 20);
            txtFilename.SetBounds(16, 106, 362, 20);

            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(iX - 62, iY - 62);

            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            var dialogResult = form.ShowDialog();
            sAliasName = txtAliasName.Text.Trim();
            return dialogResult;
        }

        private void RemoveFromMyFavoriteFiles(object sender, EventArgs e)
        {
            if (!(sender is MenuItem mnuItem))
            {
                return;
            }

            var ss = mnuItem.Tag.ToString();
            RemoveMyFavoriteFiles(ss); //RemoveFromMyFavoriteFiles
        }

        private static void OpenFolder(object sender, EventArgs e) //20191003
        {
            if (!(sender is MenuItem mnuItem))
            {
                return;
            }

            var ss = mnuItem.Tag.ToString();
            ss = File.Exists(ss) == false ? Path.GetDirectoryName(ss) : "/select, " + ss;
            Process.Start("explorer.exe", ss); //自動移至指定的路徑下的指定檔案
        }

        private static void CopyFullFilePath(object sender, EventArgs e)
        {
            if (sender is MenuItem mnuItem)
            {
                Clipboard.SetText(mnuItem.Tag.ToString());
            }
        }

        private static void CopyFilename(object sender, EventArgs e)
        {
            if (sender is MenuItem mnuItem)
            {
                Clipboard.SetText(Path.GetFileName(mnuItem.Tag.ToString()));
            }
        }

        private static void CopyCurrentPath(object sender, EventArgs e)
        {
            if (sender is MenuItem mnuItem)
            {
                Clipboard.SetText(Path.GetDirectoryName(mnuItem.Tag.ToString()) ?? throw new InvalidOperationException());
            }
        }

        private void MyFavoriteFilesClick(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem mnuItem))
            {
                return;
            }

            if (mnuItem.Tag.ToString() == "Empty My Favorite")
            {
                FindAndMoveMsgBox(Cursor.Position.X + 10, Cursor.Position.Y + 10, true, "JasonQuery");
                var sMsg = "Are you sure you want to empty \"My Favorite\" ?";
                sMsg = MyGlobal.GetLanguageString(sMsg, "form", Name, "msg", "msgEmptyMyFavorite", "Text");

                if (MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                SaveMyFavoriteFiles(mnuItem.Tag.ToString(), false, true); //MyFavoriteFilesClick
                LoadMyFavoriteFiles(); //MyFavoriteFilesClick
            }
            else
            {
                SaveMyFavoriteFiles(mnuItem.Tag.ToString(), true); //MyFavoriteFilesClick
            }
        }

        private void LoadMyFavoriteFiles()
        {
            var i = 0;
            _mruList.Clear();
            mnuMyFavorite.DropDownItems.Clear();

            try
            {
                var sSql = "SELECT AttributeText, AttributeText2 FROM SystemConfig WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'MyFavoriteFiles' ORDER BY AttributeDate DESC";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID);
                var dtMyFavoriteFiles = DBCommon.ExecQuery(sSql);

                for (var iRow = 0; iRow < dtMyFavoriteFiles.Rows.Count; iRow++)
                {
                    if (iRow <= (MyLibrary.iMyFavoriteQty - 1)) //從 0 開始，要減 1
                    {
                        i++;
                        var sTemp = dtMyFavoriteFiles.Rows[iRow]["AttributeText2"].ToString();
                        _mruList.Enqueue(dtMyFavoriteFiles.Rows[iRow]["AttributeText"] + (string.IsNullOrEmpty(sTemp) ? "" : "```" + sTemp));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {
                //ignored
            }

            var k = 0;

            if (i == 0)
            {
                mnuMyFavorite.Enabled = false;
            }
            else
            {
                mnuMyFavorite.Enabled = true;

                _sLangText = MyGlobal.GetLanguageString("Empty My Favorite", "form", Name, "menu", "EmptyMyFavorite", "Text");
                mnuMyFavorite.DropDownItems.Add(_sLangText);
                mnuMyFavorite.DropDownItems[k].Tag = "Empty My Favorite";
                var a = Assembly.GetExecutingAssembly();
                mnuMyFavorite.DropDownItems[k].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.delete 16x16.ico"));
                mnuMyFavorite.DropDownItems[k].Click += MyFavoriteFilesClick;
                mnuMyFavorite.DropDownItems.Add("-");

                k = 2; //因為前面有加上一個 "Manage My Favorite" and "-"，所以這裡要改成從 2 開始，否則不會觸發 menu event

                foreach (var fileMyFavoriteFiles in _mruList.Select(item => new ToolStripMenuItem(item)))
                {
                    var sTemp = fileMyFavoriteFiles.ToString();
                    var sFullFilename = "";
                    var sAliasName = "";

                    if (sTemp.IndexOf("```", StringComparison.Ordinal) == -1)
                    {
                        sFullFilename = sTemp;
                    }
                    else
                    {
                        var sInfo = sTemp.Split(new[] { "```" }, StringSplitOptions.None);
                        sFullFilename = sInfo[0];
                        sAliasName = "(" + sInfo[1] + ") ";
                    }

                    var sFilename = sAliasName + Path.GetFileName(sFullFilename);

                    //獨立下拉功能表
                    mnuMyFavorite.DropDownItems.Add(k - 1 + ": " + sFilename);
                    mnuMyFavorite.DropDownItems[k].ToolTipText = sFullFilename;
                    mnuMyFavorite.DropDownItems[k].Tag = sFullFilename;
                    mnuMyFavorite.DropDownItems[k].Click += MyFavoriteFilesClick;

                    k++;
                }
            }
        }

        private void SaveMyFavoriteFiles(string sFullFilename, bool bInformChildForm = false, bool bEmptyMyFavorite = false, string sAliasName = "")
        {
            string sSql;

            if (bEmptyMyFavorite)
            {
                sSql = "DELETE FROM SystemConfig WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'MyFavoriteFiles'";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID);
                DBCommon.ExecNonQuery(sSql);

                mnuMyFavorite.DropDownItems.Clear();

                return;
            }

            //Save
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'MyFavoriteFiles' AND AttributeText = '{2}'";
            sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID, sFullFilename.Replace("'", "''"));
            var dtMyFavoriteFiles = DBCommon.ExecQuery(sSql);

            if (dtMyFavoriteFiles.Rows.Count > 0)
            {
                sSql = "UPDATE SystemConfig SET AttributeDate = '{0}' WHERE DomainUser = '{1}' AND MPID = {2} AND AttributeKey = 'MyFavoriteFiles' AND AttributeText = '{3}' AND AttributeText2 = '{4}'";
                sSql = string.Format(sSql, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), MyGlobal.sDomainUser, MyGlobal.sDBMotherPID, sFullFilename.Replace("'", "''"), sAliasName);
                DBCommon.ExecNonQuery(sSql);
            }
            else
            {
                sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeText, AttributeText2, AttributeDate) VALUES ('{0}', {1}, 'MyFavoriteFiles', '{2}', '{3}', '{4}')";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID, sFullFilename.Replace("'", "''"), sAliasName, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                DBCommon.ExecNonQuery(sSql);
            }

            //Reload
            LoadMyFavoriteFiles(); //SaveMyFavoriteFiles

            //是否需要傳訊息給子表單
            if (!bInformChildForm)
            {
                return;
            }

            if (CheckTabNameExist(sFullFilename))
            {
                return;
            }

            var sTabName = CheckTabNameExist();
            CreateNewTab("Query2", sTabName, sFullFilename);
            MyGlobal.sCancelOpenAndCloseTab = sTabName;
        }

        private void RemoveMyFavoriteFiles(string sPath)
        {
            var sSql = "DELETE FROM SystemConfig WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'MyFavoriteFiles' AND AttributeText = '{2}'";
            sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID, sPath.Replace("'", "''"));
            DBCommon.ExecNonQuery(sSql);

            //Reload
            LoadMyFavoriteFiles(); //RemoveMyFavoriteFiles
        }

        private static void FindAndMoveMsgBox(int x, int y, bool repaint, string title)
        {
            var thr = new Thread(() =>
            {
                IntPtr msgBox;

                while ((msgBox = FindWindow(IntPtr.Zero, title)) == IntPtr.Zero)
                { }

                GetWindowRect(msgBox, out var r);
                MoveWindow(msgBox /* handle of the message box */, x, y,
                   r.Width - r.X /* width of originally message box */,
                   r.Height - r.Y /* height of originally message box */,
                   repaint /* if true, the message box repaints */);
            });

            thr.Start();
        }

        private void tmrTimeAndKeyStatus_Tick(object sender, EventArgs e)
        {
            var sTemp = "";

            if (MyGlobal.sGlobalTemp3.StartsWith("sqlserverswitchdatabasebyusing`")) //透過 USE 指令切換資料庫
            {
                var sDatabase = MyGlobal.sGlobalTemp3.Replace("sqlserverswitchdatabasebyusing`", "");
                MyGlobal.sGlobalTemp3 = "";
                LoadSQLServerDatabase(sDatabase);
                return;
            }

            if (MyGlobal.sGlobalTemp3.StartsWith("mysqlswitchdatabasebyusing`")) //透過 USE 指令切換資料庫
            {
                var sDatabase = MyGlobal.sGlobalTemp3.Replace("mysqlswitchdatabasebyusing`", "");
                MyGlobal.sGlobalTemp3 = "";
                LoadMySQLDatabase(sDatabase);
                return;
            }

            if (MyGlobal.sGlobalTemp.StartsWith("PasteFromSchemaBrowser`")) //是否為 Schema Browser 要貼上 SQL
            {
                sTemp = MyGlobal.sGlobalTemp.Replace("PasteFromSchemaBrowser`", "");
                MyGlobal.sGlobalTemp = "";
                PasteFromSchemaBrowser(sTemp);
                return;
            }

            if (MyGlobal.sGlobalTemp == "Rename4SchemaBrowser") //是否要修正 Schema Browser's Tab Name?
            {
                foreach (TabPage theTab in tabControl1.TabPages)
                {
                    if (theTab.AccessibleDescription == MyGlobal.sFormSchemaBrowserKey)
                    {
                        theTab.Title = MyGlobal.sNameSchemaBrowser;
                    }

                    if (theTab.Title.Trim() == "*SQL Editor")
                    {
                        theTab.Selected = true;
                    }
                }

                MyGlobal.sGlobalTemp = "";
                return;
            }

            if (tmrCheckIdleTime.Enabled == false && MyLibrary.sAutoDisconnect != "Never" && MyGlobal.sGlobalTemp == "queryagain") //判斷是否曾經「中斷連線」，若有，則由此處控制 timer (中斷後，使用者再次查詢時觸發)
            {
                MyGlobal.sGlobalTemp = "";
                tmrCheckIdleTime.Enabled = true;
                MyGlobal.bMainFormAutoDisconnect = false;
                return;
            }

            //以下程式碼，可以判斷本程式是否在作用中，並只「呼叫」一次！
            if (!MyGlobal.bContainsFocusFormOptionsKey && ContainsFocus)
            {
                foreach (TabPage theTab in tabControl1.TabPages)
                {
                    if (("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + theTab.Title + "`"))
                    {
                        continue;
                    }
                    
                    //20190909 此處改成「從其他程式切換至 JasonQuery 時，只針對 Focused Tab 處理」，因為同時處理多個，可能會有 BUG
                    if (!theTab.Selected)
                    {
                        continue;
                    }

                    sTemp += theTab.AccessibleDescription + "`";
                    break;
                }

                MyGlobal.sCheckFileFromMDIForm = "`" + sTemp;
            }

            MyGlobal.bContainsFocusFormOptionsKey = ContainsFocus;

            UpdateKeys(); //tmrTimeAndKeyStatus_Tick
        }

        #region Utility Methods
        private void UpdateKeys()
        {
            UpdateInsert();
            UpdateNumLock();
            UpdateCapsLock();
        }

        private void UpdateInsert()
        {
            if (ContainsFocus == false)
            {
                return;
            }

            var insLock = (GetKeyState((int)Keys.Insert)) != 0;
            lblINS.Text = !insLock ? "INS" : "OVR";
        }

        private void UpdateNumLock()
        {
            if (ContainsFocus == false)
            {
                return;
            }

            var numLock = GetKeyState((int)Keys.NumLock) != 0;
            lblNUM.Enabled = numLock;
        }

        private void UpdateCapsLock()
        {
            if (ContainsFocus == false)
            {
                return;
            }

            var capsLock = GetKeyState((int)Keys.CapsLock) != 0;
            lblCAPS.Enabled = capsLock;
        }

        private static void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            //
        }
        #endregion

        private void btnMouseEnter(object sender, EventArgs e)
        {
            //
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            //
        }

        private void PasteFromSchemaBrowser(string sInfo)
        {
            var bExistSqlEditor = false;

            for (var i = 0; i < tabControl1.TabPages.Count; i++)
            {
                //檢查是否有 'SQL Editor'？若存在，則傳遞 SQL statement；若不存在，則建立 Tab 後再傳遞 SQL statement
                if (tabControl1.TabPages[i].Title != @"*SQL Editor")
                {
                    continue;
                }

                bExistSqlEditor = true;
                var sTemp = tabControl1.TabPages[i].AccessibleDescription;

                //已存在，要透過全域變數傳值
                MyGlobal.sInfoFromMDIForm = "transferselectsql`" + sTemp + ";" + sInfo;
                break;
            }

            if (!bExistSqlEditor)
            {
                CreateNewTab("SQLEditor", "SQL Editor", sInfo); //開啟 SQL Editor (從 Schema Browser 傳過來，要開啟 Tab)
            }
        }

        private void uValueUpdated(object sender, ValueUpdatedEventArgs e)
        {
            var sInfo = "";

            if (e.NewValue.StartsWith("sqlserverswitchokdatabasebyusecommand`")) //20220818 使用者透過 USE 指令 切換資料庫，清空其他頁籤的訊息、並更新 Editor's SchemaBrowser 資訊
            {
                sInfo = e.NewValue.Replace("sqlserverswitchokdatabasebyusecommand`", "");

                var sTemp = (from TabPage theTab in tabControl1.TabPages where ("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + theTab.Title + "`") == false where sInfo != theTab.AccessibleDescription select theTab).Aggregate("", (current, theTab) => current + theTab.AccessibleDescription + "`");

                if (string.IsNullOrEmpty(sTemp))
                {
                    return;
                }

                MyGlobal.sGlobalTemp4 = "sqlserverswitchokdatabasefrommainform" + MyGlobal.sSeparator + sTemp; //清空其他頁籤的訊息
                MyGlobal.sGlobalTemp5 = "reloadschemainfo`" + sTemp.Replace("`", ";"); //更新 Editor's SchemaBrowser 資訊
            }
            else if (e.NewValue.StartsWith("mysqlswitchokdatabasebyusecommand`")) //20220818 使用者透過 USE 指令 切換資料庫，清空其他頁籤的訊息、並更新 Editor's SchemaBrowser 資訊
            {
                sInfo = e.NewValue.Replace("mysqlswitchokdatabasebyusecommand`", "");

                var sTemp = (from TabPage theTab in tabControl1.TabPages where ("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + theTab.Title + "`") == false where sInfo != theTab.AccessibleDescription select theTab).Aggregate("", (current, theTab) => current + theTab.AccessibleDescription + "`");

                if (string.IsNullOrEmpty(sTemp))
                {
                    return;
                }

                MyGlobal.sGlobalTemp4 = "mysqlswitchokdatabasefrommainform" + MyGlobal.sSeparator + sTemp; //清空其他頁籤的訊息
                MyGlobal.sGlobalTemp5 = "reloadschemainfo`" + sTemp.Replace("`", ";"); //更新 Editor's SchemaBrowser 資訊
            }
            else if (e.NewValue.ToLower().StartsWith("p`")) //查詢完畢，從子表單傳過來的訊息
            {
                sInfo = e.NewValue.Substring(2, e.NewValue.Length - 2);
                lblInfo.Text = sInfo;
            }
            //else if (e.NewValue.ToLower().StartsWith("q:")) //查詢完畢，從子表單傳過來的訊息
            //{
            //    sInfo = e.NewValue.Substring(2, e.NewValue.Length - 2);
            //}
            else if (e.NewValue.ToLower().StartsWith("closequeryformdirectly`"))
            {
                sInfo = e.NewValue.Substring("closequeryformdirectly`".Length, e.NewValue.Length - "closequeryformdirectly`".Length);

                foreach (TabPage theTab in tabControl1.TabPages)
                {
                    if (theTab.AccessibleDescription != sInfo)
                    {
                        continue;
                    }

                    tabControl1.TabPages.Remove(theTab);
                    UpdateMainMenuTitle(); //uValueUpdated
                    break;
                }
            }
            else if (e.NewValue.ToLower().StartsWith("closeoptionstab`"))
            {
                if (tabControl1.SelectedTab.Title.Trim() != MyGlobal.sNameOptions)
                {
                    return;
                }

                tabControl1.TabPages.Remove(tabControl1.SelectedTab);

                mnuOptions.Enabled = true;
                mnuOptions.Checked = false;

                if (tabControl1.TabPages.Count == 0)
                {
                    //20191013 如果把最後一個 Tab 關閉，自動再開啟一個空白的 Tab
                    CreateNewTab("Query", CheckTabNameExist());
                }
            }
            else if (e.NewValue.ToLower().StartsWith("closeemptytab`"))
            {
                //20191014 如果開啟檔案失敗，或是使用者取消開啟檔案，把空白的 Tab 關閉
                if (tabControl1.SelectedTab.Title.Trim() != MyGlobal.sCancelOpenAndCloseTab)
                {
                    return;
                }

                MyGlobal.sCancelOpenAndCloseTab = "";
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);

                if (tabControl1.TabPages.Count == 0)
                {
                    CreateNewTab("Query", CheckTabNameExist());
                }
            }
            else if (e.NewValue.ToLower().StartsWith("removefrommyfavoritelists`"))
            {
                sInfo = e.NewValue.Substring(26, e.NewValue.Length - 26);
                RemoveMyFavoriteFiles(sInfo); //確認要刪除 My Favorite List - Query Form 再透過 MainForm
            }
            else if (e.NewValue.ToLower().StartsWith("removefromrecentfiles`"))
            {
                sInfo = e.NewValue.Substring(22, e.NewValue.Length - 22);
                RemoveRecentFiles(sInfo);
            }
            else if (e.NewValue.ToLower().StartsWith("createnewtab`"))
            {
                if (e.NewValue.Length == 13)
                {
                    CreateNewTab("Query", CheckTabNameExist());
                }
                else
                {
                    //20191005 傳入要開啟的檔名
                    sInfo = e.NewValue.Substring(13, e.NewValue.Length - 13);
                    if (sInfo.ToUpper() == "OPENFILE")
                    {
                        OpenFile(); //uValueUpdated
                    }
                    else if (sInfo.ToUpper().Substring(0, 8) == "OPENFILE")
                    {
                        sInfo = sInfo.Substring(9, sInfo.Length - 9);
                        SaveRecentList(sInfo, true);
                    }
                }
            }
            else if (e.NewValue.ToLower().StartsWith("checkexisttab`"))
            {
                //20190930 改用 full path + filename 判斷是否已存在，因為有可能是同檔名但不同路徑
                sInfo = e.NewValue.Substring(14, e.NewValue.ToString().Length - 14);

                MyGlobal.sCheckExistTabResult = CheckTabNameExist(sInfo) == false ? "FALSE" : "TRUE";
            }
            else if (e.NewValue.ToLower().StartsWith("updatetabinfo`")) //更新 Tab 資訊
            {
                sInfo = e.NewValue.Substring(14, e.NewValue.Length - 14);
                UpdateTabInfo(sInfo); //updatetabinfo
            }
            else if (e.NewValue.ToLower().StartsWith("updatedatabaseinfo`")) //更新 Database 資訊
            {
                sInfo = e.NewValue.Substring(19, e.NewValue.Length - 19);

                if (string.IsNullOrEmpty(sInfo))
                {
                    return;
                }

                btnDatabase.Visible = true;
                btnDatabase.Text = sInfo;
                spDatabase.Visible = true;
                MyGlobal.sDBConnectionDatabase = sInfo;
            }
            else if (e.NewValue.ToLower().StartsWith("updatecanundo`")) //
            {
                sInfo = e.NewValue.Substring(14, e.NewValue.Length - 14);
                UpdateTabInfo(sInfo); //updatecanundo
            }
            else if (e.NewValue.ToLower().StartsWith("updaterecentfiles`")) //更新 Recent Files 清單
            {
                sInfo = e.NewValue.Substring(18, e.NewValue.Length - 18);
                SaveRecentList(sInfo);
            }
            else if (e.NewValue.ToLower().StartsWith("reloadqueryeditorsetting`")) //重新載入 Query Editor 的設定值
            {
                for (var i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    //不用略過 'Schema Browser' & 'SQL History'，因為這兩個也要套用！
                    if (("`" + MyGlobal.sNameOptions + "`").Contains("`" + tabControl1.TabPages[i].Title + "`") == false)
                    {
                        sInfo = sInfo + tabControl1.TabPages[i].AccessibleDescription + ";";
                    }
                }

                MyGlobal.sInfoFromMDIForm = "reloadqueryeditorsetting`" + sInfo;
            }
            else if (e.NewValue.ToLower().StartsWith("executecommitrollback`")) //執行 Commit / Rollback 指令：設定按鈕狀態
            {
                for (var i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    //略過 'Options' & 'Schema Browser' & 'SQL History'
                    if (("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + tabControl1.TabPages[i].Title + "`") == false)
                    {
                        sInfo = sInfo + tabControl1.TabPages[i].AccessibleDescription + ";";
                    }
                }

                MyGlobal.sInfoFromMDIForm = "executecommitrollback`" + sInfo;
            }
            else if (e.NewValue.ToLower().StartsWith("updatecommitrollbackbutton`")) //執行 nonquery 指令：更新按鈕狀態
            {
                for (var i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    //略過 'Options' & 'Schema Browser' & 'SQL History'
                    if (("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + tabControl1.TabPages[i].Title + "`") == false)
                    {
                        sInfo = sInfo + tabControl1.TabPages[i].AccessibleDescription + ";";
                    }
                }

                MyGlobal.sInfoFromMDIForm = "updatecommitrollbackbutton`" + sInfo;
            }
            else if (e.NewValue.ToLower().StartsWith("disconnectafterqueryonly`")) //單純執行 query 指令，且不需要等待 Commit / Rollback：中斷連線
            {
                for (var i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    //略過 'Options' & 'Schema Browser' & 'SQL History'
                    if (("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + tabControl1.TabPages[i].Title + "`") == false)
                    {
                        sInfo = sInfo + tabControl1.TabPages[i].AccessibleDescription + ";";
                    }
                }

                MyGlobal.sInfoFromMDIForm = "disconnectafterqueryonly`" + sInfo;
            }
            else if (e.NewValue.ToLower().StartsWith("disconnectafterexecuteerror`")) //執行指令有錯誤：中斷連線
            {
                for (var i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    //略過 'Options' & 'Schema Browser' & 'SQL History'
                    if (("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + tabControl1.TabPages[i].Title + "`") == false)
                    {
                        sInfo = sInfo + tabControl1.TabPages[i].AccessibleDescription + ";";
                    }
                }

                MyGlobal.sInfoFromMDIForm = "disconnectafterexecuteerror`" + sInfo;
            }
            else if (e.NewValue.ToLower().StartsWith("reloadlocalization`")) //重新載入 Localization 的設定值
            {
                LoadGlobalSetting(true); //uValueUpdated, reloadlocalization
                MyGlobal.LoadLocalizationXML(); //uValueUpdated, reloadlocalization
                ApplyLocalization(); //uValueUpdated, reloadlocalization

                if (e.NewValue.Length > 25)
                {
                    _iConnectionFormChangeLocalization++;
                    LoadConnectionForm(); //uValueUpdated, "reloadlocalization", 重新載入 Localization 的設定值
                }
                else
                {
                    LoadDefaultSetting(); //uValueUpdated, reloadlocalization

                    for (var i = 0; i < tabControl1.TabPages.Count; i++)
                    {
                        sInfo = sInfo + tabControl1.TabPages[i].AccessibleDescription + @";";

                        if (tabControl1.TabPages[i].Title == MyGlobal.sNameOptions_Before)
                        {
                            tabControl1.TabPages[i].Title = MyGlobal.sNameOptions;
                            tabControl1.TabPages[i].Tag = MyGlobal.sNameOptions;
                            MyGlobal.sNameOptions_Before = MyGlobal.sNameOptions;
                        }
                        else if (tabControl1.TabPages[i].Title == MyGlobal.sNameSchemaBrowser_Before)
                        {
                            tabControl1.TabPages[i].Title = MyGlobal.sNameSchemaBrowser;
                            tabControl1.TabPages[i].Tag = MyGlobal.sNameSchemaBrowser;
                            MyGlobal.sNameSchemaBrowser_Before = MyGlobal.sNameSchemaBrowser;
                        }
                        else if (tabControl1.TabPages[i].Title == MyGlobal.sNameSQLHistory_Before)
                        {
                            tabControl1.TabPages[i].Title = MyGlobal.sNameSQLHistory;
                            tabControl1.TabPages[i].Tag = MyGlobal.sNameSQLHistory;
                            MyGlobal.sNameSQLHistory_Before = MyGlobal.sNameSQLHistory;
                        }
                    }

                    MyGlobal.sInfoFromReloadLocalization = "reloadlocalization`" + sInfo;
                }

                //20220826 這裡要再重新載入一次，要把已切換的資料庫的 MenuItem 變成 Disable
                switch (MyGlobal.sDataSource)
                {
                    case "Oracle":
                    case "SQLite":
                    case "SQLCipher":
                        break;
                    case "PostgreSQL":
                        LoadPostgreSQLDatabase(); //uValueUpdated
                        break;
                    case "SQL Server":
                        LoadSQLServerDatabase(); //uValueUpdated
                        break;
                    case "MySQL":
                        LoadMySQLDatabase(); //uValueUpdated
                        break;
                }

                //這裡要再針對頁籤色彩調整一次
                tabControl1.BackColor = ColorTranslator.FromHtml(MyGlobal.sTabBackColor);
                tabControl1.ForeColor = ColorTranslator.FromHtml(MyGlobal.sTabActiveForeColor);
                tabControl1.TextInactiveColor = ColorTranslator.FromHtml(MyGlobal.sTabInactiveForeColor);
            }
            else if (e.NewValue.ToLower().StartsWith("transferselectsql`")) //傳遞 SQL (建立新的 Tab)
            {
                sInfo = e.NewValue.Split('`')[1];

                PasteFromSchemaBrowser(sInfo);
            }
        }

        private void UpdateTabInfo(string sValue)
        {
            var iIndex = 0;
            var bUpdate = true;
            var sTabTitle = "";
            var sTemp = "";

            for (var i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (!tabControl1.TabPages[i].Selected)
                {
                    continue;
                }

                if (sValue.Length - sValue.Replace("`", "").Length == 1)
                {
                    sTemp = sValue.Split('`')[0]; //傳過來的 AccessibleDescription
                    sValue = sValue.Split('`')[1];
                }

                var sAccessibleDescription = tabControl1.TabPages[i].AccessibleDescription;

                //如果有指定 Tab 代號，比對是否吻合，避免改錯
                if (!string.IsNullOrEmpty(sTemp) && sTemp != sAccessibleDescription)
                {
                    bUpdate = false;
                }

                sTabTitle = Path.GetFileName(sValue);

                if (sValue.Substring(0, 1) == "*" && sTabTitle.Substring(0, 1) != "*")
                {
                    sTabTitle = "*" + sTabTitle;
                }

                iIndex = i;
                break;
            }

            if (!bUpdate)
            {
                return;
            }

            tabControl1.TabPages[iIndex].Tag = sValue;
            tabControl1.TabPages[iIndex].Title = sTabTitle;
            _toolTip1.SetToolTip(tabControl1, sValue);
        }

        private void tabControl1_MouseEnter(object sender, EventArgs e)
        {
            //
        }

        private void tabControl1_MouseLeave(object sender, EventArgs e)
        {
            //
        }

        private void tabControl1_MouseHover(object sender, EventArgs e)
        {
            //
        }

        private void tabControl1_MouseMove(object sender, MouseEventArgs e)
        {
            var mouseRect = new Rectangle(e.X, e.Y, 1, 1);

            for (var i = 0; i < tabControl1.TabPages.Count; i++)
            {
                var tabRect = tabControl1.GetTabRect(i);

                if (!tabRect.IntersectsWith(mouseRect))
                {
                    continue;
                }

                //Tab 名稱有異動，才要再次顯示 ToolTip (否則會有閃爍的問題)
                if (!string.IsNullOrEmpty(_sTabToolTip) && _sTabToolTip == tabControl1.TabPages[i].Tag.ToString())
                {
                    continue;
                }

                _sTabToolTip = tabControl1.TabPages[i].Tag.ToString();
                _toolTip1.SetToolTip(tabControl1, tabControl1.TabPages[i].Tag.ToString());

                break;
            }
        }

        private void tabControl1_SelectionChanged(object sender, EventArgs e)
        {
            var sInfo = "";

            for (var i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (!tabControl1.TabPages[i].Selected)
                {
                    continue;
                }

                sInfo = tabControl1.TabPages[i].Tag.ToString();
                break;
            }

            _toolTip1.SetToolTip(tabControl1, sInfo);
        }

        private void btnNewSQLEditor_Click(object sender, EventArgs e)
        {
            CreateNewTab("Query", CheckTabNameExist());
        }

        private void LoadRecentList()
        {
            var i = 0;

            _mruList.Clear();
            mnuRecentFiles.DropDownItems.Clear();

            try
            {
                var sSql = "SELECT AttributeText FROM SystemConfig WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'RecentFile' ORDER BY AttributeDate DESC";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID);
                var dtRecent = DBCommon.ExecQuery(sSql);

                for (var iRow = 0; iRow < dtRecent.Rows.Count; iRow++)
                {
                    if (iRow <= (MyLibrary.iRecentFilesQty - 1)) //從 0 開始，要減 1
                    {
                        i++;
                        _mruList.Enqueue(dtRecent.Rows[iRow]["AttributeText"].ToString());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {
                //throw
            }

            var k = 0;

            if (i == 0)
            {
                mnuRecentFiles.Enabled = false;
            }
            else
            {
                mnuRecentFiles.Enabled = true;

                _sLangText = MyGlobal.GetLanguageString("Empty Recent Files", "form", Name, "menu", "EmptyRecentFiles", "Text");
                mnuRecentFiles.DropDownItems.Add(_sLangText);
                mnuRecentFiles.DropDownItems[k].Tag = "Empty Recent Files";
                var a = Assembly.GetExecutingAssembly();
                mnuRecentFiles.DropDownItems[k].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.delete 16x16.ico"));
                mnuRecentFiles.DropDownItems[k].Click += new EventHandler(RecentFileClick);
                mnuRecentFiles.DropDownItems.Add("-");

                k = 2; //因為前面有加上一個 "Empty Recent Files List" and "-"，所以這裡要改成從 2 開始，否則不會觸發 menu event

                foreach (var fileRecent in _mruList.Select(item => new ToolStripMenuItem(item)))
                {
                    //獨立下拉功能表
                    mnuRecentFiles.DropDownItems.Add((k - 1).ToString() + ": " + Path.GetFileName(fileRecent.ToString())); //add the menu to "recent" menu
                    mnuRecentFiles.DropDownItems[k].ToolTipText = fileRecent.ToString();
                    mnuRecentFiles.DropDownItems[k].Tag = fileRecent.ToString();
                    mnuRecentFiles.DropDownItems[k].Click += RecentFileClick;

                    k++;
                }
            }
        }

        private void SaveRecentList(string sPath, bool bInformChildForm = false, bool bEmptyRecentList = false)
        {
            string sSql;

            if (bEmptyRecentList)
            {
                sSql = "DELETE FROM SystemConfig WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'RecentFile'";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID);
                DBCommon.ExecNonQuery(sSql);

                mnuRecentFiles.DropDownItems.Clear();

                return;
            }

            //Save
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'RecentFile' AND AttributeText = '{2}'";
            sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID, sPath.Replace("'", "''"));
            var dtRecent = DBCommon.ExecQuery(sSql);

            if (dtRecent.Rows.Count > 0)
            {
                sSql = "UPDATE SystemConfig SET AttributeDate = '{0}' WHERE DomainUser = '{1}' AND MPID = {2} AND AttributeKey = 'RecentFile' AND AttributeText = '{3}'";
                sSql = string.Format(sSql, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), MyGlobal.sDomainUser, MyGlobal.sDBMotherPID, sPath.Replace("'", "''"));
                DBCommon.ExecNonQuery(sSql);
            }
            else
            {
                sSql = "INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeText, AttributeDate) VALUES ('{0}', {1}, 'RecentFile', '{2}', '{3}')";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID, sPath.Replace("'", "''"), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                DBCommon.ExecNonQuery(sSql);
            }

            //Reload
            LoadRecentList(); //SaveRecentList

            //是否需要傳訊息給子表單
            if (!bInformChildForm)
            {
                return;
            }

            if (CheckTabNameExist(sPath))
            {
                return;
            }

            var sTabName = CheckTabNameExist();
            CreateNewTab("Query1", sTabName, sPath);
            MyGlobal.sCancelOpenAndCloseTab = sTabName;
        }

        private void RemoveRecentFiles(string sPath)
        {
            var sSql = "DELETE FROM SystemConfig WHERE DomainUser = '{0}' AND MPID = {1} AND AttributeKey = 'RecentFile' AND AttributeText = '{2}'";
            sSql = string.Format(sSql, MyGlobal.sDomainUser, MyGlobal.sDBMotherPID, sPath.Replace("'", "''"));
            DBCommon.ExecNonQuery(sSql);

            LoadRecentList(); //RemoveRecentFiles
        }

        private void RecentFileClick(object sender, EventArgs e)
        {
            var sFilename = "";

            if (sender is ToolStripMenuItem mnuItem)
            {
                sFilename = mnuItem.Tag.ToString();
            }

            if (sFilename == "Empty Recent Files")
            {
                FindAndMoveMsgBox(Cursor.Position.X + 10, Cursor.Position.Y + 10, true, "JasonQuery");
                var sMsg = "Are you sure you want to empty \"Recent Files\" ?";
                sMsg = MyGlobal.GetLanguageString(sMsg, "form", Name, "msg", "msgEmptyRecentFiles", "Text");

                if (MessageBox.Show(sMsg, @"JasonQuery", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                SaveRecentList(sFilename, false, true); //RecentFileClick
                LoadRecentList(); //RecentFileClick
            }
            else
            {
                SaveRecentList(sFilename, true); //RecentFileClick
            }
        }

        private void mnuOptions_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MyGlobal.sRequireToRestart))
            {
                MessageBox.Show(MyGlobal.sRequireToRestart, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var sTitle = tabControl1.TabPages.Cast<TabPage>().Aggregate("", (current, theTab) => current + (theTab.Title.Trim().Replace("*", "") + ";"));

            sTitle += MyGlobal.sNameOptions;
            MyGlobal.sGlobalTemp = sTitle;

            if (CheckTabNameExist(MyGlobal.sNameOptions) == false)
            {
                CreateNewTab(MyGlobal.sNameOptions, MyGlobal.sNameOptions);
            }
            
            //20200825 改成可重覆開啟 (第二次開啟，自動切換到該頁籤)
            mnuOptions.Checked = true;
        }

        private void mnuSchemaBrowser_Click(object sender, EventArgs e)
        {
            if (MyGlobal.sDataSource == "SQL Server" && MyGlobal.sDBVersion == "2000")
            {
                _sLangText = "for SQL Server 2000 (or lower)\r\n" + MyGlobal.GetLanguageString("The function is not yet complete!", "Global", "Global", "msg", "FunctionNotYet", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _toolTip1.AutoPopDelay = 0;

            if (CheckTabNameExist(MyGlobal.sNameSchemaBrowser) == false)
            {
                CreateNewTab(MyGlobal.sNameSchemaBrowser, MyGlobal.sNameSchemaBrowser);
            }
            
            mnuSchemaBrowser.Checked = true;
            _toolTip1.AutoPopDelay = 5000;
        }

        private void mnuSchemaSearch_Click(object sender, EventArgs e)
        {
            using (var myForm = new frmSchemaSearch())
            {
                var iCellViewerFormWidth = 0;
                var iCellViewerFormHeight = 0;

                var sTemp = "SELECT AttributeValue FROM SystemConfig WHERE DomainUser = '{0}' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'SchemaSearchFormWidth'";
                sTemp = string.Format(sTemp, MyGlobal.sDomainUser);
                var dtData = DBCommon.ExecQuery(sTemp);

                if (dtData.Rows.Count > 0)
                {
                    int.TryParse(dtData.Rows[0][0].ToString(), out iCellViewerFormWidth);
                }

                sTemp = "SELECT AttributeValue FROM SystemConfig WHERE DomainUser = '{0}' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'SchemaSearchFormHeight'";
                sTemp = string.Format(sTemp, MyGlobal.sDomainUser);
                dtData = DBCommon.ExecQuery(sTemp);

                if (dtData.Rows.Count > 0)
                {
                    int.TryParse(dtData.Rows[0][0].ToString(), out iCellViewerFormHeight);
                }

                if (iCellViewerFormWidth > 0 && iCellViewerFormHeight > 0)
                {
                    myForm.ClientSize = new Size(iCellViewerFormWidth - 16, iCellViewerFormHeight - 38);
                }

                myForm.ShowDialog();
            }
        }

        private void mnuGenerateSQLStatement_Click(object sender, EventArgs e)
        {
            using (var myForm = new frmGenerateSQL())
            {
                myForm.ShowDialog();
            }
        }

        private void mnuSQLHistory_Click(object sender, EventArgs e)
        {
            if (CheckTabNameExist(MyGlobal.sNameSQLHistory) == false)
            {
                CreateNewTab(MyGlobal.sNameSQLHistory, MyGlobal.sNameSQLHistory);
            }

            mnuSQLHistory.Checked = true;
        }

        protected override void WndProc(ref Message m)
        {
            var WM_SYSCOMMAND = 0x0112;
            var SC_CLOSE = 0xF060;
            var bCancelClose = false;

            //取得螢幕解析度
            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;

            var iXTemp = Cursor.Position.X;
            var iYTemp = Cursor.Position.Y;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE) //使用者將 frmMain 整個關閉
            {
                if (iXTemp < 300)
                {
                    MyGlobal.iCloseDialogX = iXTemp - 25;
                }

                if (iWidth - iXTemp < 300)
                {
                    MyGlobal.iCloseDialogX = 400; //游標很靠近螢幕的右側
                }

                if (iHeight - iYTemp < 300)
                {
                    MyGlobal.iCloseDialogY = -300; //游標很靠近螢幕的下方
                }

                if (iYTemp < 100)
                {
                    MyGlobal.iCloseDialogY = 50; //游標很靠近螢幕的上方
                }

                MyGlobal.iCloseDialogX = Cursor.Position.X - MyGlobal.iCloseDialogX;
                MyGlobal.iCloseDialogY = Cursor.Position.Y + MyGlobal.iCloseDialogY;

                MyGlobal.sGlobalTemp = "closequeryformandcheckcommit`";

                //判斷是否需要 commit，由使用者決定
                foreach (TabPage theTab in tabControl1.TabPages)
                {
                    if (theTab.Title != MyGlobal.sNameOptions && theTab.Title != MyGlobal.sNameSQLHistory && theTab.Title != MyGlobal.sNameSchemaBrowser)
                    {
                        MyGlobal.sGlobalTemp += theTab.AccessibleDescription + ";";
                    }
                }

                if (string.IsNullOrEmpty(MyGlobal.sGlobalTemp) || MyGlobal.sGlobalTemp == "closequeryformandcheckcommit`") //沒有開啟 Editor，所以不需要 commit
                {
                    MyGlobal.iCommitCheck = 0;
                }

                while (MyGlobal.iCommitCheck == -1 && !string.IsNullOrEmpty(MyGlobal.sGlobalTemp))
                {
                    Application.DoEvents();
                }

                if (MyGlobal.iCommitCheck == 2)
                {
                    MyGlobal.iCommitCheck = -1; //恢復成初始值

                    //使用者取消，不用關閉了
                    return; //這裡要用 return，否則程式還是會被整個關閉
                }

                MyGlobal.iCommitCheck = -1; //恢復成初始值，因為後續使用者可能「取消存檔」而「不關閉程式」

                //判斷是否要繼續「關閉」JasonQuery
                MyGlobal.sGlobalTemp = "closequeryform`";

                foreach (TabPage theTab in tabControl1.TabPages)
                {
                    if (theTab.Title.Trim().StartsWith("*"))
                    {
                        MyGlobal.sGlobalTemp += theTab.AccessibleDescription + ";";
                    }
                }

                while (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp))
                {
                    if (tabControl1.TabPages.Count > 0 && MyGlobal.sGlobalTemp.Contains(tabControl1.SelectedTab.AccessibleDescription + "|CANCEL;"))
                    {
                        MyGlobal.iCloseDialogX = 0;
                        MyGlobal.iCloseDialogY = 0;

                        //使用者取消，不用關閉了
                        bCancelClose = true;
                        return; //這裡要用 return 不能用 break，否則程式還是會被整個關閉
                    }

                    if (MyGlobal.sGlobalTemp == "closequeryform`") //沒有任何需要存檔的 case
                    {
                        MyGlobal.sGlobalTemp = "";
                    }

                    Application.DoEvents();
                }

                //關閉整個 JasonQuery
                DisconnectDatabase();
            }

            base.WndProc(ref m);
        }

        private void mnuOpenNewSQLEditor_Click(object sender, EventArgs e)
        {
            CreateNewTab("Query", CheckTabNameExist());
        }

        private void mnuNewConnection_Click(object sender, EventArgs e)
        {
            LoadConnectionForm(); //from Menu
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            MyGlobal.iCommitCheck = -1;

            MyGlobal.sGlobalTemp = "closequeryformandcheckcommit`";

            //判斷是否需要 commit，由使用者決定
            foreach (TabPage theTab in tabControl1.TabPages)
            {
                if (theTab.Title != MyGlobal.sNameOptions && theTab.Title != MyGlobal.sNameSQLHistory && theTab.Title != MyGlobal.sNameSchemaBrowser)
                {
                    MyGlobal.sGlobalTemp += theTab.AccessibleDescription + ";";
                }
            }

            if (string.IsNullOrEmpty(MyGlobal.sGlobalTemp) || MyGlobal.sGlobalTemp == "closequeryformandcheckcommit`") //沒有開啟 Editor，所以不需要 commit
            {
                MyGlobal.iCommitCheck = 0;
            }

            while (MyGlobal.iCommitCheck == -1 && !string.IsNullOrEmpty(MyGlobal.sGlobalTemp))
            {
                Application.DoEvents();
            }

            if (MyGlobal.iCommitCheck == 2)
            {
                MyGlobal.iCommitCheck = -1; //恢復成初始值

                //使用者取消，不用關閉了
                return; //這裡要用 return，否則程式還是會被整個關閉
            }

            MyGlobal.iCommitCheck = -1; //恢復成初始值，因為後續使用者可能「取消存檔」而「不關閉程式」

            MyGlobal.sGlobalTemp = "closequeryform`";

            foreach (TabPage theTab in tabControl1.TabPages)
            {
                if (theTab.Title.Trim().StartsWith("*"))
                {
                    MyGlobal.sGlobalTemp += theTab.AccessibleDescription + ";";
                }
            }

            while (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp))
            {
                if (tabControl1.TabPages.Count > 0 && MyGlobal.sGlobalTemp.Contains(tabControl1.SelectedTab.AccessibleDescription + "|CANCEL;"))
                {
                    //使用者取消，不用關閉了
                    return;
                }

                if (MyGlobal.sGlobalTemp == "closequeryform`") //沒有任何需要存檔的 case
                {
                    MyGlobal.sGlobalTemp = "";
                }

                Application.DoEvents();
            }

            //關閉整個 JasonQuery.exe
            DisconnectDatabase();

            Close();
        }

        private void mnuCompactMDB_Click(object sender, EventArgs e)
        {
            //
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            using (var myForm = new frmAbout())
            {
                myForm.ShowDialog();
            }
        }

        private void mnuCheckForUpdatesManually_Click(object sender, EventArgs e)
        {
            using (var myForm = new frmCheckForUpdates())
            {
                myForm.ShowDialog();
            }
        }

        private void mnuNewSQLEditor_Click(object sender, EventArgs e)
        {
            CreateNewTab("Query", CheckTabNameExist());
        }

        private void mnuOpenQueryFile_Click(object sender, EventArgs e)
        {
            OpenFile(); //mnuOpenQueryFile_Click
        }

        private void OpenFile()
        {
            var of = new OpenFileDialog {Multiselect = true, Filter = @"Query files (*.sql)|*.sql|All files (*.*)|*.*"};

            if (of.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (var sFilename in of.FileNames)
            {
                if (CheckTabNameExist(sFilename) == false)
                {
                    var sTabName = CheckTabNameExist();
                    CreateNewTab("Query", sTabName, sFilename);
                    MyGlobal.sCancelOpenAndCloseTab = sTabName;
                }

                var startTime = DateTime.Now;

                while (true)
                {
                    Application.DoEvents();

                    if (DateTime.Now.Subtract(startTime).Milliseconds >= 150)
                    {
                        break;
                    }

                    Application.DoEvents();
                }
            }
        }

        private void mnuFileSplitter_Click(object sender, EventArgs e)
        {
            using (var myForm = new frmFileSplitter())
            {
                myForm.ShowDialog();
            }
        }

        private void mnuSubEdit_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem mnuItem)
            {
                MyGlobal.sGlobalTemp = "mainformaction@menu1`" + tabControl1.SelectedTab.AccessibleDescription + ";" + mnuItem.Tag;
            }
        }

        private void mnuSubFile_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem mnuItem)
            {
                MyGlobal.sGlobalTemp = "mainformaction@menu0`" + tabControl1.SelectedTab.AccessibleDescription + ";" + mnuItem.Tag;
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            _iChangeFormSizeManually++; //經過這裡，才可以判定是使用者改變 Form 大小，而不是按下「放至最大」「縮至最小」按鈕

            if (Left == -32000 && Top == -32000)
            {
                HideACList(); //MainForm_SizeChanged

                //最小化, 不用特別處理
            }
            else
            {
                MyGlobal.iMainFormLeft = Left;
                MyGlobal.iMainFormTop = Top;
                //if (Left == -8 && Top == -8) //最大化
            }
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            _iChangeFormSizeManually++;
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            if (_iChangeFormSizeManually > 1)
            {
                MyGlobal.UpdateSetting("GlobalConfig", "MainFormWidth", Size.Width.ToString());
                MyGlobal.UpdateSetting("GlobalConfig", "MainFormHeight", Size.Height.ToString());
            }

            _iChangeFormSizeManually = 0;

            MyGlobal.iMainFormLeft = Left;
            MyGlobal.iMainFormTop = Top;
        }

        private static bool VerifyCheckForUpdate(string sMode)
        {
            string sSql;
            var bResult = false;

            if (sMode == "Query")
            {
                //查詢是否需要更新
                sSql = "SELECT AttributeDate FROM SystemConfig WHERE DomainUser = '{0}' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'CheckForUpdateDays'";
                sSql = string.Format(sSql, MyGlobal.sDomainUser);
                var dt = DBCommon.ExecQuery(sSql);

                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        var ts1 = new TimeSpan(Convert.ToDateTime(dt.Rows[0]["AttributeDate"].ToString()).Ticks);
                        var ts2 = new TimeSpan(DateTime.Now.Ticks);
                        var ts = ts1.Subtract(ts2).Duration();

                        if ((ts.Days * 24 + ts.Hours) >= (MyLibrary.iCheckForUpdate * 24))
                        {
                            bResult = true;
                        }
                    }
                    catch (Exception)
                    {
                        //日期格式有錯誤
                        sSql = "UPDATE SystemConfig SET AttributeDate = '{1}' WHERE DomainUser = '{0}' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'CheckForUpdateDays'";
                        sSql = string.Format(sSql, MyGlobal.sDomainUser, DateTime.Now.ToString("yyyy/MM/dd 00:00:00"));
                        DBCommon.ExecNonQuery(sSql);
                        bResult = true;
                    }
                }
                else
                {
                    sSql = "INSERT INTO SystemConfig (DomainUser, AttributeKey, AttributeName, AttributeValue, AttributeDate) VALUES ('{0}', 'GlobalConfig', 'CheckForUpdateDays', '{1}', '{2}')";
                    sSql = string.Format(sSql, MyGlobal.sDomainUser, MyLibrary.iCheckForUpdate, DateTime.Now.ToString("yyyy/MM/dd 00:00:00"));
                    DBCommon.ExecNonQuery(sSql);
                    bResult = true;
                }
            }
            else
            {
                sSql = "UPDATE SystemConfig SET AttributeDate = '{1}' WHERE DomainUser = '{0}' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'CheckForUpdateDays'";
                sSql = string.Format(sSql, MyGlobal.sDomainUser, DateTime.Now.ToString("yyyy/MM/dd 00:00:00"));
                DBCommon.ExecNonQuery(sSql);
            }

            return bResult;
        }

        private void mnuQuery_Click(object sender, EventArgs e)
        {
            UpdateQueryMenuState(); //mnuEdit_Click
        }

        private void mnuQuery_MouseHover(object sender, EventArgs e)
        {
            UpdateQueryMenuState(); //mnuEdit_MouseHover
        }

        private void UpdateQueryMenuState()
        {
            var bEnable = false;
            var myItems = GetItems(mnuMainForm);

            if (tabControl1.SelectedTab.Title != MyGlobal.sNameSchemaBrowser && tabControl1.SelectedTab.Title != MyGlobal.sNameOptions && tabControl1.SelectedTab.Title != MyGlobal.sNameSQLHistory)
            {
                bEnable = true;
            }

            foreach (var item in myItems.Where(item => item.Name.StartsWith("mnuQuery_")))
            {
                item.Enabled = bEnable;
            }

            if (!bEnable)
            {
                return;
            }

            MyGlobal.sGlobalTemp = "mainforminfo@menu2`" + tabControl1.SelectedTab.AccessibleDescription + ";";

            var startTime = DateTime.Now;

            while (true)
            {
                Application.DoEvents();

                if (MyGlobal.sGlobalTemp.StartsWith("childforminfo@menu2`"))
                {
                    var sTemp = MyGlobal.sGlobalTemp.Replace("childforminfo@menu2`", "");
                    MyGlobal.sGlobalTemp = "";

                    foreach (var item in myItems.Where(item => item.Name.StartsWith("mnuQuery_")))
                    {
                        switch (item.Name)
                        {
                            case "mnuQuery_Execute":
                                item.Enabled = sTemp.Split(';')[0] != "0";
                                break;
                            case "mnuQuery_ExecuteCurrentBlock":
                                item.Enabled = sTemp.Split(';')[1] != "0";
                                break;
                            case "mnuQuery_Explain":
                                item.Enabled = sTemp.Split(';')[2] != "0";
                                break;
                            case "mnuQuery_ExplainAnalyze":
                                item.Enabled = sTemp.Split(';')[3] != "0";
                                break;
                            case "mnuQuery_ExplainOptions":
                                item.Enabled = sTemp.Split(';')[4] != "0";
                                break;
                        }
                    }

                    break;
                }

                Application.DoEvents();

                if (DateTime.Now.Subtract(startTime).Seconds >= 2)
                {
                    break;
                }

                Application.DoEvents();
            }
        }

        private void mnuEdit_Click(object sender, EventArgs e)
        {
            UpdateEditMenuState(); //mnuEdit_Click
        }

        private void mnuEdit_MouseHover(object sender, EventArgs e)
        {
            UpdateEditMenuState(); //mnuEdit_MouseHover
        }

        private void UpdateEditMenuState()
        {
            var bEnable = false;
            var myItems = GetItems(mnuMainForm);

            if (tabControl1.SelectedTab.Title != MyGlobal.sNameSchemaBrowser && tabControl1.SelectedTab.Title != MyGlobal.sNameOptions && tabControl1.SelectedTab.Title != MyGlobal.sNameSQLHistory)
            {
                bEnable = true;
            }

            foreach (var item in myItems.Where(item => item.Name.StartsWith("mnuEdit_")))
            {
                item.Enabled = bEnable;
            }

            if (!bEnable)
            {
                return;
            }

            MyGlobal.sGlobalTemp = "mainforminfo@menu1`" + tabControl1.SelectedTab.AccessibleDescription + ";";

            var startTime = DateTime.Now;

            while (true)
            {
                Application.DoEvents();

                if (MyGlobal.sGlobalTemp.StartsWith("childforminfo@menu1`"))
                {
                    var sTemp = MyGlobal.sGlobalTemp.Replace("childforminfo@menu1`", "");
                    MyGlobal.sGlobalTemp = "";

                    foreach (var item in myItems.Where(item => item.Name.StartsWith("mnuEdit_")))
                    {
                        switch (item.Name)
                        {
                            case "mnuEdit_Undo":
                                item.Enabled = sTemp.Split(';')[0] != "0";
                                break;
                            case "mnuEdit_Redo":
                                item.Enabled = sTemp.Split(';')[1] != "0";
                                break;
                            case "mnuEdit_Paste":
                                item.Enabled = sTemp.Split(';')[3] != "0";
                                break;
                            case "mnuEdit_SelectAll":
                            case "mnuEdit_SelectCurrentBlock":
                                item.Enabled = sTemp.Split(';')[4] != "0";
                                break;
                            default:
                                item.Enabled = sTemp.Split(';')[2] != "0";
                                break;
                        }
                    }

                    break;
                }

                Application.DoEvents();

                if (DateTime.Now.Subtract(startTime).Seconds >= 2)
                {
                    break;
                }

                Application.DoEvents();
            }
        }

        private void mnuFile_Click(object sender, EventArgs e)
        {
            UpdateFileMenuState(); //mnuFile_Click
        }

        private void mnuFile_MouseHover(object sender, EventArgs e)
        {
            UpdateFileMenuState(); //mnuFile_MouseHover
        }

        private void UpdateFileMenuState()
        {
            var bEnable = false;
            var myItems = GetItems(mnuMainForm);

            if (tabControl1.SelectedTab != null && tabControl1.SelectedTab.Title.Trim() != MyGlobal.sNameSchemaBrowser && tabControl1.SelectedTab.Title.Trim() != MyGlobal.sNameOptions && tabControl1.SelectedTab.Title.Trim() != MyGlobal.sNameSQLHistory)
            {
                bEnable = true;
            }

            foreach (var item in myItems.Where(item => item.Name.StartsWith("mnuFile_")))
            {
                item.Enabled = bEnable;
            }
        }

        private void tmrCheckIdleTime_Tick(object sender, EventArgs e)
        {
            if (Win32API.GetIdleTime() <= MyGlobal.iAutoDisconnect)
            {
                return;
            }

            //關閉 timer；使用者再次執行 SQL，timer 才會再被開啟
            tmrCheckIdleTime.Enabled = false;

            MyGlobal.bMainFormAutoDisconnect = true;

            var sTemp = tabControl1.TabPages.Cast<TabPage>().Where(theTab => ("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + theTab.Title + "`") == false).Aggregate("autodisconnect`", (current, theTab) => current + (theTab.AccessibleDescription + ";"));
            MyGlobal.sGlobalTemp = sTemp; //哪些 Tab 需要中斷連線
            Thread.Sleep(10);
            Application.DoEvents();
        }

        private void mnuCloseConnection_Click(object sender, EventArgs e)
        {
            var sTemp = tabControl1.TabPages.Cast<TabPage>().Where(theTab => ("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + theTab.Title + "`") == false).Aggregate("autodisconnect`", (current, theTab) => current + (theTab.AccessibleDescription + ";"));

            MyGlobal.sGlobalTemp = sTemp;

            mnuNewConnection.Enabled = false;
            mnuOpenConnection.Enabled = true;
            mnuCloseConnection.Enabled = false;
        }

        private void DisconnectDatabase(bool bStart = false)
        {
            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    MyGlobal.oOracleReader.oDisconnect();
                    break;
                case "PostgreSQL":
                    MyGlobal.oPostgreReader.oDisconnect();
                    break;
                case "SQL Server":
                    MyGlobal.oSQLServerReader.oDisconnect();
                    break;
                case "MySQL":
                    MyGlobal.oMySQLReader.oDisconnect();
                    break;
                case "SQLite":
                    MyGlobal.oSQLiteReader.oDisconnect();
                    break;
                case "SQLCipher":
                    MyGlobal.oSQLCipherReader.oDisconnect();
                    break;
            }

            if (bStart)
            {
                return;
            }

            mnuNewConnection.Enabled = false;
            mnuOpenConnection.Enabled = true;
            mnuCloseConnection.Enabled = false;
        }

        private void mnuOpenConnection_Click(object sender, EventArgs e)
        {
            string sResult;

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    {
                        sResult = MyGlobal.oOracleReader.ConnectTo();

                        if (!string.IsNullOrEmpty(sResult))
                        {
                            MessageBox.Show(sResult, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        break;
                    }
                case "PostgreSQL":
                    {
                        sResult = MyGlobal.oPostgreReader.ConnectTo(MyGlobal.sDBConnectionString);

                        if (!string.IsNullOrEmpty(sResult))
                        {
                            MessageBox.Show(sResult, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        break;
                    }
                case "SQL Server":
                    {
                        sResult = MyGlobal.oSQLServerReader.ConnectTo(MyGlobal.sDBConnectionString);

                        if (!string.IsNullOrEmpty(sResult))
                        {
                            MessageBox.Show(sResult, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        break;
                    }
                case "MySQL":
                    {
                        sResult = MyGlobal.oMySQLReader.ConnectTo(MyGlobal.sDBConnectionString);

                        if (!string.IsNullOrEmpty(sResult))
                        {
                            MessageBox.Show(sResult, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        break;
                    }
                case "SQLite":
                    {
                        sResult = MyGlobal.oSQLiteReader.ConnectTo(MyGlobal.sDBConnectionString);

                        if (!string.IsNullOrEmpty(sResult))
                        {
                            MessageBox.Show(sResult, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        break;
                    }
                case "SQLCipher":
                    {
                        sResult = MyGlobal.oSQLCipherReader.ConnectTo(MyGlobal.sDBConnectionString);

                        if (!string.IsNullOrEmpty(sResult))
                        {
                            MessageBox.Show(sResult, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        break;
                    }
            }

            mnuNewConnection.Enabled = false;
            mnuOpenConnection.Enabled = false;
            mnuCloseConnection.Enabled = true;
        }

        private void mnuReleaseNotes_Click(object sender, EventArgs e)
        {
            Process.Start("https://jasonquery.000webhostapp.com/releasenotes.html");
        }

        private static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        private void mnuReportBugs_Click(object sender, EventArgs e)
        {
            Process.Start("https://jasonquery.000webhostapp.com/reportbugs.html");
        }

        private void tmrDateTime_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString((string.IsNullOrEmpty(MyLibrary.sDateFormat) ? "yyyy/MM/dd" : MyLibrary.sDateFormat) + " HH:mm:ss");
        }

        private void mnuMainForm_MouseEnter(object sender, EventArgs e)
        {
            //20220725 判斷 Editor 是否有顯示下拉清單？
            _iMouseMove = 1;
        }

        private void mnuMainForm_MouseLeave(object sender, EventArgs e)
        {
            _iMouseMove = -1;
        }

        private void mnuMainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_iMouseMove == -1)
            {
                return;
            }

            _iMouseMove = -1;

            HideACList(); //mnuMainForm_MouseMove
        }

        private void HideACList() //視窗最小化、滑鼠移到功能表區域
        {
            MyGlobal.sGlobalTemp = "hideaclist`";

            foreach (TabPage theTab in tabControl1.TabPages)
            {
                if (theTab.Title != MyGlobal.sNameOptions && theTab.Title != MyGlobal.sNameSQLHistory && theTab.Title != MyGlobal.sNameSchemaBrowser)
                {
                    //隱藏 AC 下拉清單
                    MyGlobal.sGlobalTemp += theTab.AccessibleDescription + ";";
                }
            }
        }

        private void LoadPostgreSQLDatabase()
        {
            mnuSwitchDatabase.Visible = true;
            mnuSwitchDatabase.DropDownItems.Clear();

            if (_dtDatabase == null)
            {
                return;
            }

            try
            {
                for (var i = 0; i < _dtDatabase.Rows.Count; i++)
                {
                    var sDbName = _dtDatabase.Rows[i]["datname"].ToString();

                    mnuSwitchDatabase.DropDownItems.Add(sDbName);
                    mnuSwitchDatabase.DropDownItems[i].Tag = sDbName;
                    mnuSwitchDatabase.DropDownItems[i].Enabled = sDbName != MyGlobal.sDBConnectionDatabase;
                    //mnuSwitchDatabase.DropDownItems[i].Name = sDbName;

                    if (sDbName == MyGlobal.sDBConnectionDatabase)
                    {
                        //20220825 指定 icon
                        var a = Assembly.GetExecutingAssembly();
                        mnuSwitchDatabase.DropDownItems[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.PostgreSQL 16x16.png"));
                        ((ToolStripMenuItem)mnuSwitchDatabase.DropDownItems[i]).Checked = true;
                    }

                    mnuSwitchDatabase.DropDownItems[i].Click += SwitchDatabase_PostgreSQL;
                }
            }
            catch (Exception)
            {
                //
            }
        }

        private void SwitchDatabase_PostgreSQL(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem mnuItem))
            {
                return;
            }

            if (string.IsNullOrEmpty(mnuItem.Tag.ToString()))
            {
                return;
            }

            Application.UseWaitCursor = true;
            MyGlobal.iCommitCheck = -1;
            var sTabsInfo = "reloadschemainfo`";
            MyGlobal.sGlobalTemp = "closequeryformandcheckcommit`";

            //判斷是否需要 Commit，由使用者決定
            foreach (TabPage theTab in tabControl1.TabPages)
            {
                if (theTab.Title == MyGlobal.sNameOptions || theTab.Title == MyGlobal.sNameSQLHistory || theTab.Title == MyGlobal.sNameSchemaBrowser)
                {
                    continue;
                }

                MyGlobal.sGlobalTemp += theTab.AccessibleDescription + ";";
                sTabsInfo += theTab.AccessibleDescription + ";";
            }

            if (string.IsNullOrEmpty(MyGlobal.sGlobalTemp) || MyGlobal.sGlobalTemp == "closequeryformandcheckcommit`") //沒有開啟 Editor，所以不需要 commit
            {
                MyGlobal.iCommitCheck = 0;
            }

            while (MyGlobal.iCommitCheck == -1 && !string.IsNullOrEmpty(MyGlobal.sGlobalTemp))
            {
                Application.DoEvents();
            }

            if (MyGlobal.iCommitCheck == 2)
            {
                MyGlobal.iCommitCheck = -1; //恢復成初始值

                //使用者取消，不用關閉了
                return; //這裡要用 return，否則程式還是會被整個關閉
            }

            MyGlobal.iCommitCheck = -1; //恢復成初始值，因為後續使用者可能「取消存檔」而「不關閉程式」

            MyGlobal.sDBConnectionDatabase = mnuItem.Tag.ToString();
            var sDatabase = MyGlobal.GetStringBetween2(MyGlobal.sDBConnectionString, ";Database=", ";", true);

            MyGlobal.sDBConnectionString = MyGlobal.sDBConnectionString.Replace(";Database=" + sDatabase + ";", ";Database=" + mnuItem.Tag + ";");
            btnDatabase.Text = mnuItem.Tag.ToString();

            ConnectToDatabase(); //切換資料庫後，重新連線
            LoadPostgreSQLDatabase(); //SwitchDatabase_PostgreSQL 切換資料庫後，重新產生功能表資訊
            MyGlobal.dtSchema = null;
            var c1Grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            MyGlobal.UpdateSchemaData_PostgreSQL(c1Grid, false); //切換資料庫後，重新撈取 Schema Info

            MyGlobal.sGlobalTemp = sTabsInfo;
            Application.UseWaitCursor = false;

            _sLangText = MyGlobal.GetLanguageString("The database has been switched to {db} successfully!", "form", Name, "msg", "SwitchedDatabase", "Text").Replace("{db}", mnuItem.Tag.ToString());
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadSQLServerDatabase(string sDatabase = "")
        {
            mnuSwitchDatabase.Visible = true;
            mnuSwitchDatabase.DropDownItems.Clear();

            if (_dtDatabase == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(sDatabase))
            {
                sDatabase = MyGlobal.sDBConnectionDatabase;
            }

            try
            {
                for (var i = 0; i < _dtDatabase.Rows.Count; i++)
                {
                    var sDbName = _dtDatabase.Rows[i]["name"].ToString();

                    mnuSwitchDatabase.DropDownItems.Add(sDbName);
                    mnuSwitchDatabase.DropDownItems[i].Tag = sDbName;
                    mnuSwitchDatabase.DropDownItems[i].Enabled = !string.Equals(sDbName, sDatabase, StringComparison.CurrentCultureIgnoreCase);

                    if (string.Equals(sDbName, MyGlobal.sDBConnectionDatabase, StringComparison.CurrentCultureIgnoreCase)) //使用 USE 指令切換，大小寫會有差異
                    {
                        //20220825 指定 icon
                        var a = Assembly.GetExecutingAssembly();
                        mnuSwitchDatabase.DropDownItems[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.SQL Server 16x16.ico"));
                        ((ToolStripMenuItem)mnuSwitchDatabase.DropDownItems[i]).Checked = true;
                    }

                    mnuSwitchDatabase.DropDownItems[i].Click += SwitchDatabase_SQLServer;
                }
            }
            catch (Exception)
            {
                //
            }
        }

        private void SwitchDatabase_SQLServer(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem mnuItem))
            {
                return;
            }

            if (string.IsNullOrEmpty(mnuItem.Tag.ToString()))
            {
                return;
            }

            Application.UseWaitCursor = true;
            var sAccessibleDescription = "";

            if (tabControl1.TabPages.Count > 0)
            {
                if (("`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSQLHistory + "`").IndexOf("`" + tabControl1.SelectedTab.Title.Trim() + "`", StringComparison.Ordinal) != -1)
                {
                    //所在頁籤不是 SQL Editor，找出其中一個 SQL Editor，送出 Use database 指令
                    foreach (TabPage theTab in tabControl1.TabPages)
                    {
                        if (("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + theTab.Title + "`"))
                        {
                            continue;
                        }

                        sAccessibleDescription = theTab.AccessibleDescription;
                        theTab.Selected = true;
                        break;
                    }
                }
                else
                {
                    sAccessibleDescription = tabControl1.SelectedTab.AccessibleDescription;
                }
            }

            var sTemp = (from TabPage theTab in tabControl1.TabPages where ("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + theTab.Title + "`") == false where sAccessibleDescription != theTab.AccessibleDescription select theTab).Aggregate("", (current, theTab) => current + theTab.AccessibleDescription + "`");

            MyGlobal.sGlobalTemp = "sqlserverswitchdatabasefrommainform" + MyGlobal.sSeparator + sAccessibleDescription + ";" + mnuItem.Tag + ";" + sTemp;

            MyGlobal.sDBConnectionDatabase = mnuItem.Tag.ToString();
            btnDatabase.Text = mnuItem.Tag.ToString();

            LoadSQLServerDatabase(); //SwitchDatabase_SQLServer 切換資料庫後，重新產生功能表資訊

            Application.UseWaitCursor = false;
        }

        private void LoadMySQLDatabase(string sDatabase = "")
        {
            mnuSwitchDatabase.Visible = true;
            mnuSwitchDatabase.DropDownItems.Clear();

            if (_dtDatabase == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(sDatabase))
            {
                sDatabase = MyGlobal.sDBConnectionDatabase;
            }

            try
            {
                for (var i = 0; i < _dtDatabase.Rows.Count; i++)
                {
                    var sDbName = _dtDatabase.Rows[i]["name"].ToString();

                    mnuSwitchDatabase.DropDownItems.Add(sDbName);
                    mnuSwitchDatabase.DropDownItems[i].Tag = sDbName;
                    mnuSwitchDatabase.DropDownItems[i].Enabled = !string.Equals(sDbName, sDatabase, StringComparison.CurrentCultureIgnoreCase);

                    if (string.Equals(sDbName, MyGlobal.sDBConnectionDatabase, StringComparison.CurrentCultureIgnoreCase)) //使用 USE 指令切換，大小寫會有差異
                    {
                        //20220825 指定 icon
                        var a = Assembly.GetExecutingAssembly();
                        mnuSwitchDatabase.DropDownItems[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.MySQL 16x16.png"));
                        ((ToolStripMenuItem)mnuSwitchDatabase.DropDownItems[i]).Checked = true;
                    }

                    mnuSwitchDatabase.DropDownItems[i].Click += SwitchDatabase_MySQL;
                }
            }
            catch (Exception)
            {
                //
            }
        }

        private void SwitchDatabase_MySQL(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem mnuItem))
            {
                return;
            }

            if (string.IsNullOrEmpty(mnuItem.Tag.ToString()))
            {
                return;
            }

            Application.UseWaitCursor = true;
            var sAccessibleDescription = "";

            if (tabControl1.TabPages.Count > 0)
            {
                if (("`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSQLHistory + "`").IndexOf("`" + tabControl1.SelectedTab.Title.Trim() + "`", StringComparison.Ordinal) != -1)
                {
                    //所在頁籤不是 SQL Editor，找出其中一個 SQL Editor，送出 Use database 指令
                    foreach (TabPage theTab in tabControl1.TabPages)
                    {
                        //20190909 點選到的 Tab，檢查檔案是否有被外部程式異動內容，或是被刪除了
                        if (("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + theTab.Title + "`"))
                        {
                            continue;
                        }

                        sAccessibleDescription = theTab.AccessibleDescription;
                        theTab.Selected = true;
                        break;
                    }
                }
                else
                {
                    sAccessibleDescription = tabControl1.SelectedTab.AccessibleDescription;
                }
            }

            var sTemp = (from TabPage theTab in tabControl1.TabPages where ("`" + MyGlobal.sNameOptions + "`" + MyGlobal.sNameSchemaBrowser + "`" + MyGlobal.sNameSQLHistory + "`").Contains("`" + theTab.Title + "`") == false where sAccessibleDescription != theTab.AccessibleDescription select theTab).Aggregate("", (current, theTab) => current + theTab.AccessibleDescription + "`");

            MyGlobal.sGlobalTemp = "mysqlswitchdatabasefrommainform" + MyGlobal.sSeparator + sAccessibleDescription + ";" + mnuItem.Tag + ";" + sTemp;

            MyGlobal.sDBConnectionDatabase = mnuItem.Tag.ToString();
            btnDatabase.Text = mnuItem.Tag.ToString();

            LoadMySQLDatabase(); //SwitchDatabase_SQLServer 切換資料庫後，重新產生功能表資訊

            Application.UseWaitCursor = false;
        }
    }
}