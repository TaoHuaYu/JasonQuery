using System;
using System.Data;
using System.Xml;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JasonLibrary;
using C1.Win.C1Input;
using C1.Win.C1TrueDBGrid;
using C1.Win.C1Ribbon;
using JasonLibrary.Class;

namespace JasonQuery
{
    public static class MyGlobal
    {
        public static string sSeparator = " ";
        public static string sSeparator3 = "   ";
        public static string sSeparator3s = " ` ";
        public static string sSeparator5 = "     ";
        public static string sSeparator7 = "       ";
        public static string sSeparatorPlus1 = "!~|||~!";
        public static string sSeparatorPlus2 = "|~!!!~|";

        public static string sExecuteNonQuerySQLHistoryScript = ""; //執行「非查詢SQL」，逐筆寫入 SQL History
        public static string sCheckExistTabResult = "";
        public static string sTabBackColor = ""; //Main Form Tab Color
        public static string sTabActiveForeColor = ""; //Main Form Tab Color
        public static string sTabInactiveForeColor = ""; //Main Form Tab Color
        public static bool bTabShrinkPages = true;
        public static bool bTabShowArrows = true;
        public static bool bTabHoverSelect = false;
        public static bool bTabMultiLine = false;
        public static bool bTabBold = true;
        public static string sMDBFilename = "";
        public static string sMyVersion = ""; //JasonQuery.exe 的版本號碼
        public static string sMyDBConnectionString = "";
        public static string sMyDBConnectionPassword = "ytec1688"; //20221107 連線至 JasonQuery.db 的密碼
        public static string sMyDBConnectionPasswordPrefix = "jasonquery1231"; //20221107 連線至 JasonQuery.db 的密碼
        public static string sMyDBConnectionPasswordSuffix = "encryptDB!nf0"; //20221107 連線至 JasonQuery.db 的密碼
        public static string sMyDBConnectionExportPasswordSuffix = "exportDB!nf0"; //20221107 連線至 JasonQuery.db 的密碼
        public static string sDBMotherPID = "";
        public static string sDataSource = ""; //Oracle, Postgre, SQL Server, MySQL
        public static string sDBUser = ""; //記錄 User Name，取得 Table 相關資訊時會用到
        public static string sDBPW = ""; //記錄 User PW
        public static string sDBVersion = ""; //記錄 DB Server 版號 (SQL Server 會用到)
        public static bool bDBAutoRollback = false;
        public static bool bDBDirectMode = false;
        public static bool bDBPooling = false;
        public static bool bDBExcludeNativeDatabase = true;
        public static bool bDBUnicode = false;
        public static int iDBConnectionPort = 0;
        public static string sDBConnectionSid = ""; //記錄 Sid, for Oracle
        public static string sDBConnectionString = "";
        public static string sDBConnectionTitle = "";
        public static string sDBConnectionName = "";
        public static string sDBConnectionServer = "";
        public static string sDBConnectionConnectAs = "";
        public static string sDBConnectionDatabase = ""; //fetch schema info
        public static string sSupportInfo = "";
        public static string sDomainUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name; //範例：(公司)YTEC\jason.yu1204, (家裡)YU\Administrator
        public static string sOpenFileFromMDIForm = ""; //由主表單觸發，要開啟指定的檔案
        public static string sInfoFromMDIForm = ""; //由主表單觸發，傳送特定訊息至指定的子表單
        public static string sInfoFromReloadLocalization = ""; //由主表單觸發，傳送特定訊息至所有已開啟的子表單
        public static string sCheckFileFromMDIForm = ""; //由主表單觸發，傳送特定訊息至指定的子表單
        public static string sFormSchemaBrowserKey = ""; //開啟 Schema Browser，記錄它的 Key
        public static string sFormSQLHistoryKey = ""; //開啟 SQL History，記錄它的 Key
        public static string sFormOptionsKey = ""; //開啟 Options，記錄它的 Key
        public static bool bContainsFocusFormOptionsKey = true; //記錄「本程式是否作用中？」
        public static string sOpenFileFromExternal = ""; //從外面呼叫，開啟指定的檔案
        public static string sCancelOpenAndCloseTab = ""; //開啟檔案失敗或使用者取消開啟檔案，關閉空白的 Tab
        public static string sSendMsgToInfo = "";
        public static int iCommitCheck = -1; //-1:未檢查；0:不需要commit；1:要commit；2:取消
        public static string sBookmarkStyle = "";
        public static string sCSVDelimiters = ""; //匯出至 CSV 的分隔符號
        public static string sRequireToRestart = "";
        public static bool bAfterPasteFocusOnQueryEditor = true;

        public static DataTable dtSchema;
        public static DataTable dtTableColumn; //取得所有的 Table + Column (Search in Schema 會用到)
        public static DataTable dtTFVTP; //取得所有的 Table + Functions + Views + Triggers + Procedures (Search in Schema 會用到)
        public static DataTable dtTableAndViewName; //取得所有的 Table + View Name (Query Editor AC 會用到)
        public static DataTable dtDatabaseName; //取得所有的 Database Name (Query Editor AC 會用到, for SQL Server & MySQL)
        public static DataTable dtAC4All; //for Query Editor AutoComplete，取得指定的關鍵字

        public static bool bMainFormAutoDisconnect = false; //是否有「自動中斷連線」？如果 JasonQuery 有中斷連線過，此變數會變成 true

        public static bool bMainFormMaximized = true;
        public static int iMainFormWidth = 1024;
        public static int iMainFormHeight = 768;
        public static int iMainFormLocationX = 100;
        public static int iMainFormLocationY = 100;
        public static int iMainFormLeft = 0; //主畫面 Left 值
        public static int iMainFormTop = 0; //主畫面 Top 值

        public static int iCloseDialogX = 0;
        public static int iCloseDialogY = 0;

        public static bool bChangeColorThemeNeedRestart = false;

        public static Dictionary<string, string> dicLocalization = new Dictionary<string, string>();
        public static Dictionary<string, string> dicBookmarkStyle = new Dictionary<string, string>();
        public static Dictionary<string, string> dicWordWrapIndentMode = new Dictionary<string, string>();
        public static Dictionary<string, string> dicMaxWidth = new Dictionary<string, string>();
        public static Dictionary<string, string> dicRowSizing = new Dictionary<string, string>();
        public static Dictionary<string, string> dicAutoDisconnect = new Dictionary<string, string>();
        public static Dictionary<string, string> dicCSVDelimiters = new Dictionary<string, string>();

        public static Dictionary<string, Dictionary<string, string>> dicAll = new Dictionary<string, Dictionary<string, string>>();

        public static string sRowSizing = "";
        public static string sAutoDisconnect = "";
        public static int iAutoDisconnect = 0;
        public static string sSpecifiedSQLFile1 = "";
        public static string sSpecifiedSQLFile2 = "";

        public static bool bShowColumnInfo = false;
        public static bool bSortByColumnName = false;
        public static bool bDefaultTabSchemaBrowser = false;
        public static bool bAutoListMembers = false;
        public static bool bSavePoint = false;
        public static int iProgressInsertInto = 0;
        public static bool bProgressCancel = false;

        public static string sGridMaxWidth = "";
        public static string sWordWrapIndentMode = "";
        public static int iTabWidth = 4;
        public static bool bPreviewCLOBData = false;

        public static string sGlobalTemp = ""; //暫存變數，判斷用！
        public static string sGlobalTemp2 = ""; //暫存變數，判斷用！
        public static string sGlobalTemp3 = ""; //暫存變數，判斷用！
        public static string sGlobalTemp4 = ""; //暫存變數，判斷用！
        public static string sGlobalTemp5 = ""; //暫存變數，判斷用！

        public static clsOracleReader oOracleReader = new clsOracleReader();
        public static clsPostgreSQLReader oPostgreReader = new clsPostgreSQLReader();
        public static clsSQLServerReader oSQLServerReader = new clsSQLServerReader();
        public static clsMySQLReader oMySQLReader = new clsMySQLReader();
        public static clsSQLiteReader oSQLiteReader = new clsSQLiteReader();
        public static clsSQLCipherReader oSQLCipherReader = new clsSQLCipherReader();

        public static DataTable dtSchemaTable = new DataTable();
        public static string sSchemaAccessibleDescription = "";

        public static DataTable dtLocalization = new DataTable();
        public static string sLocalizationList = ""; //語系清單
        public static string sLocalization = "English"; //預設語系
        public static string sLocalizationCode = "en-US";
        public static string sXmlFilename = "english.xml";

        public static string sNameOptions = "";
        public static string sNameSchemaBrowser = "";
        public static string sNameSQLHistory = "";
        public static string sNameOptions_Before = "";
        public static string sNameSchemaBrowser_Before = "";
        public static string sNameSQLHistory_Before = "";

        public static DataTable dtSingleRecordViewer = new DataTable();

        public static string GetIPAddress()
        {
            var sIPs = "";
            var strHostName = Dns.GetHostName().ToUpper();

            try
            {
                var iphostentry = Dns.GetHostEntry(strHostName);

                //取得所有 IP 位址 (只取 IP V4 的 Address)
                sIPs = iphostentry.AddressList.Where(ipaddress => ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Aggregate(sIPs, (current, ipaddress) => current + ipaddress + ";");

                //20230129 忽略 169.254 網段的 IP
                var parts = sIPs.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                sIPs = "";

                for (var i = 0; i < parts.Length; i++)
                {
                    if (!parts[i].StartsWith("169.254."))
                    {
                        sIPs += parts[i] + ";";
                    }
                }

                if (sIPs.Substring(sIPs.Length - 1, 1) == ";")
                {
                    sIPs = sIPs.Substring(0, sIPs.Length - 1);
                }
            }
            catch (Exception)
            {
                //
            }

            if (string.IsNullOrEmpty(sIPs))
            {
                sIPs = strHostName;
            }

            return sIPs;
        }

        public static DataTable XmlToDataTable(string sFilename)
        {
            var dt = new DataTable();

            try
            {
                var sXml = File.ReadAllText(sFilename);
                var xmldoc = new XmlDocument();
                xmldoc.LoadXml(sXml);
                var xmlreader = XmlReader.Create(new StringReader(xmldoc.OuterXml));
                var ds = new DataSet();
                ds.ReadXml(xmlreader);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"An error has occurred while loading localization file:" + "\r\n\r\n" + sFilename + "\r\n\r\n" + ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return dt;
        }

        public static string GetLanguageString(string sOriginalText, string sCategory, string sClass, string sType, string sID, string sAttribute)
        {
            sID = sID.Replace("'", "''"); //20230603 預防 ID 有單引號
            var sResult = sOriginalText;

            switch (sClass)
            {
                case "frmConnect": //20200922 針對 Connection Form 特別處理
                    sID = sID.Replace("_PostgreSQL", "");
                    sID = sID.Replace("_Oracle", "");
                    sID = sID.Replace("_SQLServer", "");
                    sID = sID.Replace("_MySQL", "");
                    sID = sID.Replace("_SQLite", "");
                    break;
                
                case "frmSchemaSearch" when sID == "chkLimit": //20220702 針對 SchemaSearch Form 特別處理
                    sID += "_" + sDataSource;
                    break;
            }

            try
            {
                if (dtLocalization != null && dtLocalization.Rows.Count > 0 && dtLocalization.Columns.Count == 6)
                {
                    var sSql = "Category = '" + sCategory + "' AND Class = '" + sClass + "' AND Type = '" + sType + "' AND ID = '" + sID + "' AND Attribute = '" + sAttribute + "'";
                    var dtRow = dtLocalization.Select(sSql);

                    if (dtRow.Length > 0 && !string.IsNullOrEmpty(dtRow[0]["name"].ToString().Trim()))
                    {
                        sResult = dtRow[0]["name"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                var sLangText = GetLanguageString("An error has occurred.", "Global", "Global", "msg", "AnErrorHasOccurred", "Text");
                MessageBox.Show(sLangText + "\r\n\r\n" + ex.Message, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return sResult;
        }

        public static string GetReplacementString(DataTable dt, string sKeywordName, string sReplacementName, string sKeyword)
        {
            var sResult = "";

            try
            {
                var sSql = sKeywordName + " = '" + sKeyword + "'";
                var dtRow = dt.Select(sSql);

                if (dtRow.Length > 0 && !string.IsNullOrEmpty(dtRow[0][sReplacementName].ToString().Trim()))
                {
                    sResult = dtRow[0][sReplacementName].ToString();
                }
            }
            catch (Exception)
            {
                //
            }

            return sResult;
        }

        public static void ApplyLanguageInfo(Form frmForm, bool bColor = true, bool bDarkMode = true)
        {
            Color colorNormal = Color.Black, colorRed = Color.Red;
            var sonControls = frmForm.Controls;

            if (MyLibrary.bDarkMode && bDarkMode)
            {
                colorNormal = Color.White;
            }

            //更新 Form's Title
            frmForm.Text = GetLanguageString(frmForm.Text, "form", frmForm.Name, "object", "this", "Text");

            try
            {
                foreach (Control control in sonControls)
                {
                    bool bEnable;

                    if ("`Label`LinkLabel`Button`C1Button`C1SplitButton`CheckBox`C1CheckBox`RadioButton`".Contains("`" + control.GetType().Name + "`"))
                    {
                        control.Text = GetLanguageString(control.Text, "form", frmForm.Name, "object", control.Name, "Text");

                        bEnable = control.Enabled;
                        control.Enabled = true;

                        if (bColor)
                        {
                            if (control.GetType().Name == "Label" && control.Name.StartsWith("lblStar"))
                            { control.ForeColor = colorRed; }
                            else
                            { control.ForeColor = colorNormal; }
                        }

                        control.Enabled = bEnable;
                    }
                    else if (control.GetType().Name == "C1StatusBar")
                    {

                    }
                    else
                    {
                        switch (control.GetType().Name)
                        {
                            case "Panel":
                                {
                                    foreach (Control obj in ((Panel)control).Controls)
                                    {
                                        if ("`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + obj.GetType().Name + "`"))
                                        {
                                            //pnlSQLite 落在這~
                                            obj.Text = GetLanguageString(obj.Text, "form", frmForm.Name, "object", obj.Name, "Text");
                                        }

                                        if (obj.GetType().Name != "ToolStrip")
                                        {
                                            continue;
                                        }

                                        foreach (ToolStripItem item in ((ToolStrip)obj).Items)
                                        {
                                            item.ToolTipText = GetLanguageString(item.ToolTipText, "form", frmForm.Name, "object", item.Name, "ToolTipText");
                                        }
                                    }

                                    break;
                                }
                            case "GroupBox":
                                {
                                    control.Text = GetLanguageString(control.Text, "form", frmForm.Name, "object", control.Name, "Text");

                                    if (bColor)
                                    {
                                        control.ForeColor = colorNormal;
                                    }

                                    foreach (Control ctrlInGroupBox in ((GroupBox)control).Controls)
                                    {
                                        if ("`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + ctrlInGroupBox.GetType().Name + "`"))
                                        {
                                            ctrlInGroupBox.Text = GetLanguageString(ctrlInGroupBox.Text, "form", frmForm.Name, "object", ctrlInGroupBox.Name, "Text");

                                            bEnable = ctrlInGroupBox.Enabled;
                                            ctrlInGroupBox.Enabled = true;

                                            if (bColor)
                                            {
                                                if (ctrlInGroupBox.GetType().Name == "Label" && ctrlInGroupBox.Name.StartsWith("lblStar"))
                                                { ctrlInGroupBox.ForeColor = colorRed; }
                                                else
                                                { ctrlInGroupBox.ForeColor = colorNormal; }
                                            }

                                            ctrlInGroupBox.Enabled = bEnable;
                                        }
                                        else switch (ctrlInGroupBox.GetType().Name)
                                            {
                                                case "GroupBox":
                                                    {
                                                        ctrlInGroupBox.Text = GetLanguageString(ctrlInGroupBox.Text, "form", frmForm.Name, "object", ctrlInGroupBox.Name, "Text");
                                                        ctrlInGroupBox.ForeColor = colorNormal;

                                                        foreach (Control ctrlInGroupBoxItem in ((GroupBox)ctrlInGroupBox).Controls)
                                                        {
                                                            if (!"`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + ctrlInGroupBoxItem.GetType().Name + "`"))
                                                            {
                                                                continue;
                                                            }

                                                            ctrlInGroupBoxItem.Text = GetLanguageString(ctrlInGroupBoxItem.Text, "form", frmForm.Name, "object", ctrlInGroupBoxItem.Name, "Text");

                                                            bEnable = ctrlInGroupBox.Enabled;
                                                            ctrlInGroupBox.Enabled = true;

                                                            if (bColor)
                                                            {
                                                                if (ctrlInGroupBox.GetType().Name == "Label" && ctrlInGroupBox.Name.StartsWith("lblStar"))
                                                                { ctrlInGroupBox.ForeColor = colorRed; }
                                                                else
                                                                { ctrlInGroupBox.ForeColor = colorNormal; }
                                                            }

                                                            ctrlInGroupBox.Enabled = bEnable;
                                                        }

                                                        break;
                                                    }
                                                case "Panel":
                                                    {
                                                        foreach (Control obj in ((Panel)ctrlInGroupBox).Controls)
                                                        {
                                                            if ("`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + obj.GetType().Name + "`"))
                                                            {
                                                                //pnlOracle 這幾個是落在這~
                                                                obj.Text = GetLanguageString(obj.Text, "form", frmForm.Name, "object", obj.Name, "Text");
                                                            }
                                                        }

                                                        break;
                                                    }
                                            }
                                    }

                                    break;
                                }
                            case "C1DockingTab":
                                {
                                    foreach (Control tab in control.Controls)
                                    {
                                        tab.Text = GetLanguageString(tab.Text, "form", frmForm.Name, "object", tab.Name, "Text");

                                        var tabPage = (C1.Win.C1Command.C1DockingTabPage)tab;

                                        foreach (Control ctrlTab in tabPage.Controls)
                                        {
                                            if ("`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + ctrlTab.GetType().Name + "`"))
                                            {
                                                //20220701
                                                ctrlTab.Text = GetLanguageString(ctrlTab.Text, "form", frmForm.Name, "object", ctrlTab.Name, "Text");
                                            }

                                            switch (ctrlTab.GetType().Name)
                                            {
                                                case "SplitContainer":
                                                    {
                                                        foreach (Control child11 in ((SplitContainer)ctrlTab).Panel1.Controls)
                                                        {
                                                            switch (child11.GetType().Name)
                                                            {
                                                                case "SplitContainer":
                                                                    {
                                                                        foreach (Control child11InChild11 in ((SplitContainer)child11).Panel1.Controls)
                                                                        {
                                                                            if (child11InChild11.GetType().Name != "GroupBox")
                                                                            {
                                                                                continue;
                                                                            }

                                                                            child11InChild11.Text = GetLanguageString(child11InChild11.Text, "form", frmForm.Name, "object", child11InChild11.Name, "Text");
                                                                            child11InChild11.ForeColor = colorNormal;

                                                                            foreach (Control itemInGroupBox in ((GroupBox)child11InChild11).Controls)
                                                                            {
                                                                                if ("`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + itemInGroupBox.GetType().Name + "`"))
                                                                                {
                                                                                    itemInGroupBox.Text = GetLanguageString(itemInGroupBox.Text, "form", frmForm.Name, "object", itemInGroupBox.Name, "Text");

                                                                                    bEnable = itemInGroupBox.Enabled;
                                                                                    itemInGroupBox.Enabled = true;

                                                                                    if (bColor)
                                                                                    {
                                                                                        if (itemInGroupBox.GetType().Name == "Label" && itemInGroupBox.Name.StartsWith("lblStar"))
                                                                                        { itemInGroupBox.ForeColor = colorRed; }
                                                                                        else
                                                                                        { itemInGroupBox.ForeColor = colorNormal; }
                                                                                    }

                                                                                    itemInGroupBox.Enabled = bEnable;
                                                                                }
                                                                                else if (itemInGroupBox.GetType().Name == "GroupBox")
                                                                                {
                                                                                    foreach (Control itemInGroupBoxInGroupBox in ((GroupBox)itemInGroupBox).Controls)
                                                                                    {
                                                                                        itemInGroupBox.ForeColor = colorNormal;

                                                                                        if ("`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + itemInGroupBox.GetType().Name + "`"))
                                                                                        {
                                                                                            itemInGroupBoxInGroupBox.Text = GetLanguageString(itemInGroupBoxInGroupBox.Text, "form", frmForm.Name, "object", itemInGroupBoxInGroupBox.Name, "Text");

                                                                                            bEnable = itemInGroupBoxInGroupBox.Enabled;
                                                                                            itemInGroupBoxInGroupBox.Enabled = true;

                                                                                            if (bColor)
                                                                                            {
                                                                                                if (itemInGroupBoxInGroupBox.GetType().Name == "Label" && itemInGroupBoxInGroupBox.Name.StartsWith("lblStar"))
                                                                                                { itemInGroupBoxInGroupBox.ForeColor = colorRed; }
                                                                                                else
                                                                                                { itemInGroupBoxInGroupBox.ForeColor = colorNormal; }
                                                                                            }

                                                                                            itemInGroupBoxInGroupBox.Enabled = bEnable;
                                                                                        }
                                                                                        else if (itemInGroupBoxInGroupBox.GetType().Name == "ToolStrip")
                                                                                        {
                                                                                            foreach (ToolStripItem item123 in ((ToolStrip)itemInGroupBoxInGroupBox).Items)
                                                                                            {
                                                                                                item123.Text = GetLanguageString(item123.Text, "form", frmForm.Name, "object", item123.Name, "Text");
                                                                                                item123.ToolTipText = GetLanguageString(item123.ToolTipText, "form", frmForm.Name, "object", item123.Name, "ToolTipText");

                                                                                                bEnable = item123.Enabled;
                                                                                                item123.Enabled = true;
                                                                                                item123.ForeColor = colorNormal;
                                                                                                item123.Enabled = bEnable;
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }

                                                                        foreach (Control child11InChild11 in ((SplitContainer)child11).Panel2.Controls)
                                                                        {
                                                                            if (child11InChild11.GetType().Name != "GroupBox")
                                                                            {
                                                                                continue;
                                                                            }

                                                                            child11InChild11.ForeColor = colorNormal;
                                                                            child11InChild11.Text = GetLanguageString(child11InChild11.Text, "form", frmForm.Name, "object", child11InChild11.Name, "Text");

                                                                            foreach (Control itemInGroupBox in ((GroupBox)child11InChild11).Controls)
                                                                            {
                                                                                if (!"`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + itemInGroupBox.GetType().Name + "`"))
                                                                                {
                                                                                    continue;
                                                                                }

                                                                                itemInGroupBox.Text = GetLanguageString(itemInGroupBox.Text, "form", frmForm.Name, "object", itemInGroupBox.Name, "Text");

                                                                                bEnable = itemInGroupBox.Enabled;
                                                                                itemInGroupBox.Enabled = true;

                                                                                if (bColor)
                                                                                {
                                                                                    if (itemInGroupBox.GetType().Name == "Label" && itemInGroupBox.Name.StartsWith("lblStar"))
                                                                                    { itemInGroupBox.ForeColor = colorRed; }
                                                                                    else
                                                                                    { itemInGroupBox.ForeColor = colorNormal; }
                                                                                }

                                                                                itemInGroupBox.Enabled = bEnable;
                                                                            }
                                                                        }

                                                                        break;
                                                                    }
                                                                case "ToolStrip":
                                                                    {
                                                                        foreach (ToolStripItem item in ((ToolStrip)child11).Items)
                                                                        {
                                                                            item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Name, "Text");
                                                                            item.ToolTipText = GetLanguageString(item.ToolTipText, "form", frmForm.Name, "object", item.Name, "ToolTipText");

                                                                            bEnable = item.Enabled;
                                                                            item.Enabled = true;
                                                                            item.ForeColor = colorNormal;
                                                                            item.Enabled = bEnable;
                                                                        }

                                                                        break;
                                                                    }
                                                            }
                                                        }

                                                        foreach (Control child11 in ((SplitContainer)ctrlTab).Panel2.Controls)
                                                        {
                                                            if (child11.GetType().Name == "SplitContainer")
                                                            {
                                                                foreach (Control child11InChild11 in ((SplitContainer)child11).Panel1.Controls)
                                                                {
                                                                    if (child11InChild11.GetType().Name != "GroupBox")
                                                                    {
                                                                        continue;
                                                                    }

                                                                    child11InChild11.ForeColor = colorNormal;
                                                                    child11InChild11.Text = GetLanguageString(child11InChild11.Text, "form", frmForm.Name, "object", child11InChild11.Name, "Text");

                                                                    foreach (Control itemInGroupBox in ((GroupBox)child11InChild11).Controls)
                                                                    {
                                                                        if (!"`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + itemInGroupBox.GetType().Name + "`"))
                                                                        {
                                                                            continue;
                                                                        }

                                                                        itemInGroupBox.Text = GetLanguageString(itemInGroupBox.Text, "form", frmForm.Name, "object", itemInGroupBox.Name, "Text");

                                                                        bEnable = itemInGroupBox.Enabled;
                                                                        itemInGroupBox.Enabled = true;

                                                                        if (bColor)
                                                                        {
                                                                            if (itemInGroupBox.GetType().Name == "Label" && itemInGroupBox.Name.StartsWith("lblStar"))
                                                                            { itemInGroupBox.ForeColor = colorRed; }
                                                                            else
                                                                            { itemInGroupBox.ForeColor = colorNormal; }
                                                                        }

                                                                        itemInGroupBox.Enabled = bEnable;
                                                                    }
                                                                }

                                                                foreach (Control child11InChild11 in ((SplitContainer)child11).Panel2.Controls)
                                                                {
                                                                    if (child11InChild11.GetType().Name != "GroupBox")
                                                                    {
                                                                        continue;
                                                                    }

                                                                    child11InChild11.ForeColor = colorNormal;
                                                                    child11InChild11.Text = GetLanguageString(child11InChild11.Text, "form", frmForm.Name, "object", child11InChild11.Name, "Text");

                                                                    foreach (Control itemInGroupBox in ((GroupBox)child11InChild11).Controls)
                                                                    {
                                                                        if (!"`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + itemInGroupBox.GetType().Name + "`"))
                                                                        {
                                                                            continue;
                                                                        }

                                                                        itemInGroupBox.Text = GetLanguageString(itemInGroupBox.Text, "form", frmForm.Name, "object", itemInGroupBox.Name, "Text");

                                                                        bEnable = itemInGroupBox.Enabled;
                                                                        itemInGroupBox.Enabled = true;

                                                                        if (bColor)
                                                                        {
                                                                            if (itemInGroupBox.GetType().Name == "Label" && itemInGroupBox.Name.StartsWith("lblStar"))
                                                                            { itemInGroupBox.ForeColor = colorRed; }
                                                                            else
                                                                            { itemInGroupBox.ForeColor = colorNormal; }
                                                                        }

                                                                        itemInGroupBox.Enabled = bEnable;
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                foreach (ToolStripItem item in ((ToolStrip)child11).Items)
                                                                {
                                                                    item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Name, "Text");
                                                                    item.ToolTipText = GetLanguageString(item.ToolTipText, "form", frmForm.Name, "object", item.Name, "ToolTipText");

                                                                    bEnable = item.Enabled;
                                                                    item.Enabled = true;
                                                                    item.ForeColor = colorNormal;
                                                                    item.Enabled = bEnable;
                                                                }
                                                            }
                                                        }

                                                        break;
                                                    }
                                                case "GroupBox":
                                                    {
                                                        ctrlTab.ForeColor = colorNormal;
                                                        ctrlTab.BackColor = Color.Transparent;
                                                        ctrlTab.Text = GetLanguageString(ctrlTab.Text, "form", frmForm.Name, "object", ctrlTab.Name, "Text");

                                                        foreach (Control item in ((GroupBox)ctrlTab).Controls)
                                                        {
                                                            if ("`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + item.GetType().Name + "`"))
                                                            {
                                                                item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Name, "Text");

                                                                bEnable = item.Enabled;
                                                                item.Enabled = true;

                                                                if (bColor)
                                                                {
                                                                    if (item.GetType().Name == "Label" && item.Name.StartsWith("lblStar"))
                                                                    {
                                                                        item.ForeColor = colorRed;
                                                                    }
                                                                    else
                                                                    {
                                                                        item.ForeColor = colorNormal;
                                                                    }
                                                                }

                                                                item.Enabled = bEnable;
                                                            }
                                                            else if (item.GetType().Name == "NumericUpDown")
                                                            {
                                                                //
                                                            }
                                                            else switch (item.GetType().Name)
                                                                {
                                                                    case "GroupBox":
                                                                        {
                                                                            item.ForeColor = colorNormal;
                                                                            item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Name, "Text");

                                                                            foreach (Control itemInGroupBox in ((GroupBox)item).Controls)
                                                                            {
                                                                                if ("`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + itemInGroupBox.GetType().Name + "`"))
                                                                                {
                                                                                    itemInGroupBox.Text = GetLanguageString(itemInGroupBox.Text, "form", frmForm.Name, "object", itemInGroupBox.Name, "Text");

                                                                                    bEnable = itemInGroupBox.Enabled;
                                                                                    itemInGroupBox.Enabled = true;

                                                                                    if (bColor)
                                                                                    {
                                                                                        if (itemInGroupBox.GetType().Name == "Label" && itemInGroupBox.Name.StartsWith("lblStar"))
                                                                                        { itemInGroupBox.ForeColor = colorRed; }
                                                                                        else
                                                                                        { itemInGroupBox.ForeColor = colorNormal; }
                                                                                    }

                                                                                    itemInGroupBox.Enabled = bEnable;
                                                                                }
                                                                                else switch (itemInGroupBox.GetType().Name)
                                                                                    {
                                                                                        case "GroupBox":
                                                                                            {
                                                                                                itemInGroupBox.ForeColor = colorNormal;
                                                                                                itemInGroupBox.Text = GetLanguageString(itemInGroupBox.Text, "form", frmForm.Name, "object", itemInGroupBox.Name, "Text");

                                                                                                foreach (Control itemInGroupBoxInGroupBox in ((GroupBox)itemInGroupBox).Controls)
                                                                                                {
                                                                                                    if (!"`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + itemInGroupBoxInGroupBox.GetType().Name + "`"))
                                                                                                    {
                                                                                                        continue;
                                                                                                    }

                                                                                                    itemInGroupBoxInGroupBox.Text = GetLanguageString(itemInGroupBoxInGroupBox.Text, "form", frmForm.Name, "object", itemInGroupBoxInGroupBox.Name, "Text");

                                                                                                    bEnable = itemInGroupBoxInGroupBox.Enabled;
                                                                                                    itemInGroupBoxInGroupBox.Enabled = true;

                                                                                                    if (bColor)
                                                                                                    {
                                                                                                        if (itemInGroupBoxInGroupBox.GetType().Name == "Label" && itemInGroupBoxInGroupBox.Name.StartsWith("lblStar"))
                                                                                                        {
                                                                                                            itemInGroupBoxInGroupBox.ForeColor = colorRed;
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            itemInGroupBoxInGroupBox.ForeColor = colorNormal;
                                                                                                        }
                                                                                                    }

                                                                                                    itemInGroupBoxInGroupBox.Enabled = bEnable;
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case "C1DockingTab":
                                                                                            {
                                                                                                foreach (Control tab2 in itemInGroupBox.Controls)
                                                                                                {
                                                                                                    tab2.Text = GetLanguageString(tab2.Text, "form", frmForm.Name, "object", tab2.Name, "Text");
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case "SplitContainer":
                                                                                            {
                                                                                                foreach (Control childInItem in ((Panel)itemInGroupBox).Controls)
                                                                                                {
                                                                                                    childInItem.Text = GetLanguageString(childInItem.Text, "form", frmForm.Name, "object", childInItem.Name, "Text");
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        default:
                                                                                            { }
                                                                                            break;
                                                                                    }
                                                                            }

                                                                            break;
                                                                        }
                                                                    case "ToolStrip":
                                                                        {
                                                                            foreach (ToolStripItem childInItem in ((ToolStrip)item).Items)
                                                                            {
                                                                                childInItem.Text = GetLanguageString(childInItem.Text, "form", frmForm.Name, "object", childInItem.Name, "Text");
                                                                                childInItem.ToolTipText = GetLanguageString(childInItem.ToolTipText, "form", frmForm.Name, "object", childInItem.Name, "ToolTipText");

                                                                                bEnable = childInItem.Enabled;
                                                                                childInItem.Enabled = true;
                                                                                childInItem.ForeColor = colorNormal;
                                                                                childInItem.Enabled = bEnable;
                                                                            }

                                                                            break;
                                                                        }
                                                                    default:
                                                                        {
                                                                            if ("`Panel`ComboBox`C1ComboBox`ScintillaEditor`PictureBox`TextBox`C1TextBox`C1TrueDBGrid`ListBox`LinkLabel`C1FontPicker`".Contains("`" + item.GetType().Name + "`") == false)
                                                                            {
                                                                                foreach (Control child11 in ((SplitContainer)item).Panel2.Controls)
                                                                                {
                                                                                    if (child11.GetType().Name == "SplitContainer")
                                                                                    {
                                                                                        foreach (Control child11InChild11 in ((SplitContainer)child11).Panel1.Controls)
                                                                                        {
                                                                                            if (child11InChild11.GetType().Name !=
                                                                                             "GroupBox")
                                                                                            {
                                                                                                continue;
                                                                                            }

                                                                                            child11InChild11.ForeColor = colorNormal;
                                                                                            child11InChild11.Text = GetLanguageString(child11InChild11.Text, "form", frmForm.Name, "object", child11InChild11.Name, "Text");

                                                                                            foreach (Control itemInGroupBox in ((GroupBox)child11InChild11).Controls)
                                                                                            {
                                                                                                if (!"`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + itemInGroupBox.GetType().Name + "`"))
                                                                                                {
                                                                                                    continue;
                                                                                                }

                                                                                                itemInGroupBox.Text = GetLanguageString(itemInGroupBox.Text, "form", frmForm.Name, "object", itemInGroupBox.Name, "Text");

                                                                                                bEnable = itemInGroupBox.Enabled;
                                                                                                itemInGroupBox.Enabled = true;

                                                                                                if (bColor)
                                                                                                {
                                                                                                    if (itemInGroupBox.GetType().Name == "Label" && itemInGroupBox.Name.StartsWith("lblStar"))
                                                                                                    {
                                                                                                        itemInGroupBox.ForeColor = colorRed;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        itemInGroupBox.ForeColor = colorNormal;
                                                                                                    }
                                                                                                }

                                                                                                itemInGroupBox.Enabled = bEnable;
                                                                                            }
                                                                                        }

                                                                                        foreach (Control child11InChild11 in ((SplitContainer)child11).Panel2.Controls)
                                                                                        {
                                                                                            if (child11InChild11.GetType().Name != "GroupBox")
                                                                                            {
                                                                                                continue;
                                                                                            }

                                                                                            child11InChild11.ForeColor = colorNormal;
                                                                                            child11InChild11.Text = GetLanguageString(child11InChild11.Text, "form", frmForm.Name, "object", child11InChild11.Name, "Text");

                                                                                            foreach (Control itemInGroupBox in ((GroupBox)child11InChild11).Controls)
                                                                                            {
                                                                                                if (!"`Label`Button`C1Button`CheckBox`C1CheckBox`RadioButton`".Contains("`" + itemInGroupBox.GetType().Name + "`"))
                                                                                                {
                                                                                                    continue;
                                                                                                }

                                                                                                itemInGroupBox.Text = GetLanguageString(itemInGroupBox.Text, "form", frmForm.Name, "object", itemInGroupBox.Name, "Text");

                                                                                                bEnable = itemInGroupBox.Enabled;
                                                                                                itemInGroupBox.Enabled = true;

                                                                                                if (bColor)
                                                                                                {
                                                                                                    if (itemInGroupBox.GetType().Name == "Label" && itemInGroupBox.Name.StartsWith("lblStar"))
                                                                                                    {
                                                                                                        itemInGroupBox.ForeColor = colorRed;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        itemInGroupBox.ForeColor = colorNormal;
                                                                                                    }
                                                                                                }

                                                                                                itemInGroupBox.Enabled = bEnable;
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        foreach (ToolStripItem item11 in ((ToolStrip)child11).Items)
                                                                                        {
                                                                                            item11.Text = GetLanguageString(item11.Text, "form", frmForm.Name, "object", item11.Name, "Text");
                                                                                            item11.ToolTipText = GetLanguageString(item11.ToolTipText, "form", frmForm.Name, "object", item11.Name, "ToolTipText");

                                                                                            bEnable = item11.Enabled;
                                                                                            item11.Enabled = true;
                                                                                            item11.ForeColor = colorNormal;
                                                                                            item11.Enabled = bEnable;
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }

                                                                            if (item.GetType().Name == "Panel")
                                                                            {
                                                                                foreach (Control obj in ((Panel)item).Controls)
                                                                                {
                                                                                    switch (obj.GetType().Name)
                                                                                    {
                                                                                        case "C1Label":
                                                                                        case "C1Button":
                                                                                        case "Label":
                                                                                        case "Button":
                                                                                            obj.Text = GetLanguageString(obj.Text, "form", frmForm.Name, "object", obj.Name, "Text");
                                                                                            break;
                                                                                        case "ToolStrip":
                                                                                            {
                                                                                                foreach (ToolStripItem objItem in ((ToolStrip)obj).Items)
                                                                                                {
                                                                                                    objItem.ToolTipText = GetLanguageString(objItem.ToolTipText, "form", frmForm.Name, "object", objItem.Name, "ToolTipText");
                                                                                                }

                                                                                                break;
                                                                                            }
                                                                                        case "C1ComboBox":
                                                                                        case "CheckBox": //20230430
                                                                                            {
                                                                                                bEnable = obj.Enabled;
                                                                                                obj.Enabled = true;
                                                                                                obj.Text = GetLanguageString(obj.Text, "form", frmForm.Name, "object", obj.Name, "Text");
                                                                                                obj.Enabled = bEnable;
                                                                                                break;
                                                                                            }
                                                                                    }
                                                                                }
                                                                            }

                                                                            break;
                                                                        }
                                                                }
                                                        }

                                                        break;
                                                    }
                                            }
                                        }
                                    }

                                    break;
                                }
                            case "SplitContainer" when frmForm.Name == "frmQuery":
                                {
                                    foreach (Control child3 in ((SplitContainer)control).Panel1.Controls)
                                    {
                                        switch (child3.Name)
                                        {
                                            case "splitContainer1":
                                                {
                                                    foreach (Control childEditor in ((SplitContainer)child3).Panel1.Controls)
                                                    {
                                                        if (childEditor.Name != "tsEditor")
                                                        {
                                                            continue;
                                                        }

                                                        foreach (ToolStripItem item in ((ToolStrip)childEditor).Items)
                                                        {
                                                            item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Name, "Text");
                                                            item.ToolTipText = GetLanguageString(item.ToolTipText, "form", frmForm.Name, "object", item.Name, "ToolTipText");

                                                            bEnable = item.Enabled;
                                                            item.Enabled = true;
                                                            item.ForeColor = colorNormal;
                                                            item.Enabled = bEnable;
                                                        }
                                                    }

                                                    break;
                                                }
                                            case "splitContainer2":
                                                {
                                                    foreach (Control childEditor in ((SplitContainer)child3).Panel1.Controls)
                                                    {
                                                        switch (childEditor.GetType().Name)
                                                        {
                                                            case "C1DockingTab": //20211031 加入 Schema 頁籤
                                                                {
                                                                    foreach (Control tab in childEditor.Controls)
                                                                    {
                                                                        var tabPage = (C1.Win.C1Command.C1DockingTabPage)tab;

                                                                        foreach (Control ctrlTab in tabPage.Controls)
                                                                        {
                                                                            switch (ctrlTab.GetType().Name)
                                                                            {
                                                                                case "ToolStrip":
                                                                                    {
                                                                                        foreach (ToolStripItem item in ((ToolStrip)ctrlTab).Items)
                                                                                        {
                                                                                            item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Name, "Text");
                                                                                            item.ToolTipText = GetLanguageString(item.ToolTipText, "form", frmForm.Name, "object", item.Name, "ToolTipText");

                                                                                            if (item.GetType().Name == "ToolStripSplitButton")
                                                                                            {
                                                                                                foreach (ToolStripItem itemChild in ((ToolStripSplitButton)item).DropDownItems)
                                                                                                {
                                                                                                    itemChild.Text = GetLanguageString(itemChild.Text, "form", frmForm.Name, "object", itemChild.Name, "Text");
                                                                                                    itemChild.ToolTipText = GetLanguageString(itemChild.ToolTipText, "form", frmForm.Name, "object", itemChild.Name, "ToolTipText");

                                                                                                    foreach (ToolStripMenuItem itemMenuChild in ((ToolStripMenuItem)itemChild).DropDownItems)
                                                                                                    {
                                                                                                        itemMenuChild.Text = GetLanguageString(itemMenuChild.Text, "form", frmForm.Name, "object", itemMenuChild.Name, "Text");
                                                                                                    }
                                                                                                }
                                                                                            }

                                                                                            bEnable = item.Enabled;
                                                                                            item.Enabled = true;
                                                                                            item.ForeColor = colorNormal;
                                                                                            item.Enabled = bEnable;
                                                                                        }

                                                                                        break;
                                                                                    }
                                                                            }
                                                                        }
                                                                    }

                                                                    break;
                                                                }
                                                            case "ToolStrip":
                                                                {
                                                                    foreach (ToolStripItem item in ((ToolStrip)childEditor).Items)
                                                                    {
                                                                        item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Name, "Text");
                                                                        item.ToolTipText = GetLanguageString(item.ToolTipText, "form", frmForm.Name, "object", item.Name, "ToolTipText");

                                                                        bEnable = item.Enabled;
                                                                        item.Enabled = true;
                                                                        item.ForeColor = colorNormal;
                                                                        item.Enabled = bEnable;
                                                                    }

                                                                    break;
                                                                }
                                                            case "C1CheckBox":
                                                                childEditor.Text = GetLanguageString(childEditor.Text, "form", frmForm.Name, "object", childEditor.Name, "Text");

                                                                bEnable = childEditor.Enabled;
                                                                childEditor.Enabled = true;
                                                                childEditor.ForeColor = colorNormal;
                                                                childEditor.Enabled = bEnable;
                                                                break;
                                                        }
                                                    }

                                                    foreach (Control childGrid in ((SplitContainer)child3).Panel2.Controls)
                                                    {
                                                        switch (childGrid.Name)
                                                        {
                                                            case "tsDataGrid":
                                                                {
                                                                    foreach (ToolStripItem item in ((ToolStrip)childGrid).Items)
                                                                    {
                                                                        item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Name, "Text");
                                                                        item.ToolTipText = GetLanguageString(item.ToolTipText, "form", frmForm.Name, "object", item.Name, "ToolTipText");

                                                                        bEnable = item.Enabled;
                                                                        item.Enabled = true;
                                                                        item.ForeColor = colorNormal;
                                                                        item.Enabled = bEnable;
                                                                    }

                                                                    break;
                                                                }
                                                            case "tabControl1":
                                                                {
                                                                    foreach (Control tab in childGrid.Controls)
                                                                    {
                                                                        tab.Text = GetLanguageString(tab.Text, "form", frmForm.Name, "object", tab.Name, "Text");
                                                                    }

                                                                    break;
                                                                }
                                                            default:
                                                                {
                                                                    if (childGrid.GetType().Name == "Panel")
                                                                    {
                                                                        foreach (Control obj in ((Panel)childGrid).Controls)
                                                                        {
                                                                            switch (obj.GetType().Name)
                                                                            {
                                                                                case "C1Button":
                                                                                case "Label":
                                                                                    obj.Text = GetLanguageString(obj.Text, "form", frmForm.Name, "object", obj.Name, "Text");
                                                                                    break;
                                                                                case "ToolStrip":
                                                                                    {
                                                                                        foreach (ToolStripItem objItem in ((ToolStrip)obj).Items)
                                                                                        {
                                                                                            objItem.ToolTipText = GetLanguageString(objItem.ToolTipText, "form", frmForm.Name, "object", objItem.Name, "ToolTipText");
                                                                                        }

                                                                                        break;
                                                                                    }
                                                                                case "C1ComboBox":
                                                                                    bEnable = obj.Enabled;
                                                                                    obj.Enabled = true;
                                                                                    obj.Text = GetLanguageString(obj.Text, "form", frmForm.Name, "object", obj.Name, "Text");
                                                                                    obj.Enabled = bEnable;
                                                                                    break;
                                                                            }
                                                                        }
                                                                    }
                                                                    else if (childGrid.GetType().Name == "C1StatusBar")
                                                                    {
                                                                        //20220703 editor 下方的狀態列
                                                                        //var objControls = ((C1StatusBar)childGrid).RightPaneItems;

                                                                        //foreach (RibbonItem items in objControls)
                                                                        //{
                                                                        //    if (items.GetType().Name == "RibbonLabel")
                                                                        //    {
                                                                        //        ((RibbonLabel)items).Text = GetLanguageString(((RibbonLabel)items).Text, "form", frmForm.Name, "statusbarobject", ((RibbonLabel)items).Name, "Text");
                                                                        //    }
                                                                        //    else if (items.GetType().Name == "RibbonButton")
                                                                        //    {
                                                                        //        ((RibbonButton)items).Text = GetLanguageString(((RibbonButton)items).Text, "form", frmForm.Name, "statusbarobject", ((RibbonButton)items).Name, "Text");
                                                                        //        ((RibbonButton)items).ToolTip = GetLanguageString(((RibbonButton)items).Text, "form", frmForm.Name, "statusbarobject", ((RibbonButton)items).Name, "ToolTipText");
                                                                        //    }
                                                                        //}
                                                                    }

                                                                    break;
                                                                }
                                                        }
                                                    }

                                                    break;
                                                }
                                            case "tsEditor":
                                                {
                                                    foreach (ToolStripItem item in ((ToolStrip)child3).Items)
                                                    {
                                                        item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Name, "Text");
                                                        item.ToolTipText = GetLanguageString(item.ToolTipText, "form", frmForm.Name, "object", item.Name, "ToolTipText");

                                                        if (item.GetType().Name == "ToolStripDropDownButton")
                                                        {
                                                            foreach (ToolStripItem itemChild in ((ToolStripDropDownButton)item).DropDownItems)
                                                            {
                                                                itemChild.Text = GetLanguageString(itemChild.Text, "form", frmForm.Name, "object", itemChild.Name, "Text");
                                                                itemChild.ToolTipText = GetLanguageString(itemChild.ToolTipText, "form", frmForm.Name, "object", itemChild.Name, "ToolTipText");

                                                                foreach (ToolStripMenuItem itemMenuChild in ((ToolStripMenuItem)itemChild).DropDownItems)
                                                                {
                                                                    itemMenuChild.Text = GetLanguageString(itemMenuChild.Text, "form", frmForm.Name, "object", itemMenuChild.Name, "Text");
                                                                }
                                                            }
                                                        }

                                                        bEnable = item.Enabled;
                                                        item.Enabled = true;
                                                        item.ForeColor = colorNormal;
                                                        item.Enabled = bEnable;
                                                    }

                                                    break;
                                                }
                                        }
                                    }

                                    foreach (Control child3 in ((SplitContainer)control).Panel2.Controls)
                                    {
                                        if (child3.GetType().Name == "C1CheckBox")
                                        {
                                            child3.Text = GetLanguageString(child3.Text, "form", frmForm.Name, "object", child3.Name, "Text");
                                        }
                                        else if (child3.Name == "tsDataGrid")
                                        {
                                            foreach (ToolStripItem item in ((ToolStrip)child3).Items)
                                            {
                                                item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Name, "Text");
                                                item.ToolTipText = GetLanguageString(item.ToolTipText, "form", frmForm.Name, "object", item.Name, "ToolTipText");

                                                if (item.GetType().Name == "ToolStripDropDownButton")
                                                {
                                                    foreach (ToolStripItem itemChild in ((ToolStripDropDownButton)item).DropDownItems)
                                                    {
                                                        itemChild.Text = GetLanguageString(itemChild.Text, "form", frmForm.Name, "object", itemChild.Name, "Text");
                                                        itemChild.ToolTipText = GetLanguageString(itemChild.ToolTipText, "form", frmForm.Name, "object", itemChild.Name, "ToolTipText");
                                                    }
                                                }

                                                bEnable = item.Enabled;
                                                item.Enabled = true;
                                                item.ForeColor = colorNormal;
                                                item.Enabled = bEnable;
                                            }
                                        }
                                    }

                                    break;
                                }
                            case "SplitContainer":
                                {
                                    foreach (Control child11 in ((SplitContainer)control).Panel1.Controls)
                                    {
                                        switch (child11.GetType().Name)
                                        {
                                            case "ToolStrip":
                                                {
                                                    foreach (ToolStripItem items in ((ToolStrip)child11).Items)
                                                    {
                                                        items.Text = GetLanguageString(items.Text, "form", frmForm.Name, "object", items.Name, "Text");
                                                        items.ToolTipText = GetLanguageString(items.ToolTipText, "form", frmForm.Name, "object", items.Name, "ToolTipText");

                                                        if (items.GetType().Name == "ToolStripDropDownButton")
                                                        {
                                                            foreach (ToolStripItem itemChild in ((ToolStripDropDownButton)items).DropDownItems)
                                                            {
                                                                itemChild.Text = GetLanguageString(itemChild.Text, "form", frmForm.Name, "object", itemChild.Name, "Text");
                                                                itemChild.ToolTipText = GetLanguageString(itemChild.ToolTipText, "form", frmForm.Name, "object", itemChild.Name, "ToolTipText");
                                                            }
                                                        }

                                                        bEnable = items.Enabled;
                                                        items.Enabled = true;
                                                        items.ForeColor = colorNormal;
                                                        items.Enabled = bEnable;
                                                    }

                                                    break;
                                                }
                                            case "C1CheckBox":
                                            case "C1Label":
                                            case "Label":
                                                child11.Text = GetLanguageString(child11.Text, "form", frmForm.Name, "object", child11.Name, "Text");
                                                break;
                                            case "C1SplitButton":
                                                {
                                                    foreach (DropDownItem dropDownItems in ((C1SplitButton)child11).Items)
                                                    {
                                                        foreach (DropDownItem item in dropDownItems.SplitButton.Items)
                                                        {
                                                            item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Tag.ToString(), "Text");
                                                        }
                                                    }

                                                    break;
                                                }
                                        }
                                    }

                                    foreach (Control child22 in ((SplitContainer)control).Panel2.Controls)
                                    {
                                        switch (child22.GetType().Name)
                                        {
                                            case "ToolStrip":
                                                {
                                                    foreach (ToolStripItem item in ((ToolStrip)child22).Items)
                                                    {
                                                        item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Name, "Text");
                                                        item.ToolTipText = GetLanguageString(item.ToolTipText, "form", frmForm.Name, "object", item.Name, "ToolTipText");

                                                        bEnable = item.Enabled;
                                                        item.Enabled = true;
                                                        item.ForeColor = colorNormal;
                                                        item.Enabled = bEnable;
                                                    }

                                                    break;
                                                }
                                            case "C1CheckBox":
                                                child22.Text = GetLanguageString(child22.Text, "form", frmForm.Name, "object", child22.Name, "Text");
                                                break;
                                            case "C1DockingTab":
                                                {
                                                    foreach (Control tab in child22.Controls)
                                                    {
                                                        tab.Text = GetLanguageString(tab.Text, "form", frmForm.Name, "object", tab.Name, "Text");

                                                        var tabPage = (C1.Win.C1Command.C1DockingTabPage)tab;

                                                        foreach (Control ctrlTab in tabPage.Controls)
                                                        {
                                                            switch (ctrlTab.GetType().Name)
                                                            {
                                                                case "C1CheckBox":
                                                                    ctrlTab.Text = GetLanguageString(ctrlTab.Text, "form", frmForm.Name, "object", ctrlTab.Name, "Text");
                                                                    break;
                                                                case "ToolStrip":
                                                                    {
                                                                        foreach (ToolStripItem childInItem in ((ToolStrip)ctrlTab).Items)
                                                                        {
                                                                            childInItem.Text = GetLanguageString(childInItem.Text, "form", frmForm.Name, "object", childInItem.Name, "Text");
                                                                            childInItem.ToolTipText = GetLanguageString(childInItem.ToolTipText, "form", frmForm.Name, "object", childInItem.Name, "ToolTipText");
                                                                        }

                                                                        break;
                                                                    }
                                                            }
                                                        }
                                                    }

                                                    break;
                                                }
                                        }
                                    }

                                    break;
                                }
                            case "ToolStrip":
                                {
                                    foreach (ToolStripItem item in ((ToolStrip)control).Items)
                                    {
                                        item.Text = GetLanguageString(item.Text, "form", frmForm.Name, "object", item.Name, "Text");
                                        item.ToolTipText = GetLanguageString(item.ToolTipText, "form", frmForm.Name, "object", item.Name, "ToolTipText");

                                        bEnable = item.Enabled;
                                        item.Enabled = true;
                                        item.ForeColor = colorNormal;
                                        item.Enabled = bEnable;
                                    }

                                    break;
                                }
                            case "C1CheckBox":
                                control.Text = GetLanguageString(control.Text, "form", frmForm.Name, "object", control.Name, "Text");
                                break;
                            case "C1StatusBar":
                                {
                                    var objControls = ((C1StatusBar)control).RightPaneItems;

                                    foreach (RibbonItem items in objControls)
                                    {
                                        switch (items.GetType().Name)
                                        {
                                            case "RibbonLabel":
                                                ((RibbonLabel)items).Text = GetLanguageString(((RibbonLabel)items).Text, "form", frmForm.Name, "statusbarobject", ((RibbonLabel)items).Name, "Text");
                                                break;
                                            case "RibbonButton":
                                                ((RibbonButton)items).Text = GetLanguageString(((RibbonButton)items).Text, "form", frmForm.Name, "statusbarobject", ((RibbonButton)items).Name, "Text");
                                                ((RibbonButton)items).ToolTip = GetLanguageString(((RibbonButton)items).Text, "form", frmForm.Name, "statusbarobject", ((RibbonButton)items).Name, "ToolTipText");
                                                break;
                                        }
                                    }

                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //
            }
        }

        public static void UpdateSetting(string sAttributeKey, string sAttributeName, string sAttributeValue, bool bInsertDirectly = false, bool bAttributeText = false)
        {
            var sFieldName = "AttributeValue";
            var sMPID = string.IsNullOrEmpty(sDBMotherPID) ? "0" : sDBMotherPID;
            var sDateTimeNow = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            if (sAttributeName == "CheckForUpdateDays")
            {
                sDateTimeNow = DateTime.Now.ToString("yyyy/MM/dd 00:00:00");
            }

            if (sAttributeKey == "GlobalConfig")
            {
                sMPID = "0";
            }

            sAttributeValue = sAttributeValue.Replace("'", "''");

            if (bInsertDirectly) //Auto Replace & Auto Complete 必須先刪除後再 Insert，才不會殘留舊資料
            {
                DBCommon.ExecNonQuery("INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, AttributeValue, AttributeDate) VALUES ('" + sDomainUser + "', " + sMPID + ", '" + sAttributeKey + "', '" + sAttributeName + "', '" + sAttributeValue + "', '" + sDateTimeNow + "')");
            }
            else
            {
                if (bAttributeText)
                {
                    sFieldName = "AttributeText";
                }

                //依據資料是否存在，進行 Update / Insert
                var dtTemp = DBCommon.ExecQuery("SELECT * FROM SystemConfig WHERE DomainUser = '" + sDomainUser + "' AND MPID = " + sMPID + " AND AttributeKey = '" + sAttributeKey + "' AND AttributeName = '" + sAttributeName + "'");

                if (dtTemp.Rows.Count > 0)
                {
                    if (sAttributeName == "CheckForUpdateDays") //不更新日期欄位，避免異常，每次都影響檢查更新的判斷
                    {
                        DBCommon.ExecNonQuery("UPDATE SystemConfig SET " + sFieldName + " = '" + sAttributeValue + "' WHERE DomainUser = '" + sDomainUser + "' AND MPID = " + sMPID + " AND AttributeKey = '" + sAttributeKey + "' AND AttributeName = '" + sAttributeName + "'");
                    }
                    else
                    {
                        DBCommon.ExecNonQuery("UPDATE SystemConfig SET " + sFieldName + " = '" + sAttributeValue + "', AttributeDate = '" + sDateTimeNow + "' WHERE DomainUser = '" + sDomainUser + "' AND MPID = " + sMPID + " AND AttributeKey = '" + sAttributeKey + "' AND AttributeName = '" + sAttributeName + "'");
                    }
                }
                else
                {
                    DBCommon.ExecNonQuery("INSERT INTO SystemConfig (DomainUser, MPID, AttributeKey, AttributeName, " + sFieldName + ", AttributeDate) VALUES ('" + sDomainUser + "', " + sMPID + ", '" + sAttributeKey + "', '" + sAttributeName + "', '" + sAttributeValue + "', '" + sDateTimeNow + "')");
                }
            }
        }

        public static string GetValueFromDictionary(Dictionary<string, string> dic, string sKey)
        {
            var sValue = "";

            if (dic.ContainsKey(sKey))
            {
                sValue = dic[sKey];
            }

            return sValue;
        }

        public static string GetKeyFromDictionary(Dictionary<string, string> dic, string sValue)
        {
            var sKey = "";

            foreach (var item in dic.Where(item => sValue == item.Value))
            {
                sKey = item.Key;
                break;
            }

            return sKey;
        }

        public static void SetC1ComboBoxItemsFromDictionary(C1ComboBox cBox, Dictionary<string, string> dic, bool bKey = false)
        {
            cBox.Items.Clear();

            foreach (var OneItem in dic)
            {
                cBox.Items.Add(bKey ? OneItem.Key : OneItem.Value);
            }
        }

        public static void SetC1ComboBoxItemsFromDataTable(C1ComboBox cBox, DataTable dt, int iColInex = 0)
        {
            cBox.Items.Clear();

            if (dt == null)
            {
                return;
            }

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                cBox.Items.Add(dt.Rows[i][iColInex].ToString());
            }
        }

        public static void SetC1TextBoxText(C1TextBox c1Text, string sText)
        {
            var bVisible = c1Text.Visible;

            try
            {
                c1Text.Visible = true;
                c1Text.Text = sText;
                c1Text.Visible = bVisible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"SetC1TextBoxText" + "\r\n\r\n" + ex.Message);
            }
        }

        public static void SetC1ComboboxTextIgnoreVisible(C1ComboBox c1Text, string sText)
        {
            var bEnable = c1Text.Enabled;
            var bVisible = c1Text.Visible;
            var dStyle = c1Text.DropDownStyle;

            try
            {
                c1Text.Visible = true;
                c1Text.Enabled = true;

                try
                {
                    c1Text.DropDownStyle = DropDownStyle.Default;
                    c1Text.Text = sText;
                }
                catch (Exception)
                {
                    c1Text.DropDownStyle = DropDownStyle.DropDownList;
                    c1Text.Text = sText;
                }

                c1Text.DropDownStyle = dStyle;
                c1Text.Enabled = bEnable;
                c1Text.Visible = bVisible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"SetC1ComboboxText" + "\r\n\r\n" + ex.Message);
            }
        }

        public static void SetC1ComboBoxTextAndTriggerSelectedIndex(C1ComboBox cBox, string sText)
        {
            for (var i = 0; i < cBox.Items.Count; i++)
            {
                if (cBox.Items[i].ToString() != sText)
                {
                    continue;
                }

                try
                {
                    cBox.SelectedIndex = i;
                }
                catch (Exception)
                {
                    //
                }

                break;
            }
        }

        public static void ChangeC1TrueDbGridVisualStyle(C1TrueDBGrid c1Grid, string sStyleText) //bPreview=true, 表示只要針對「c1GridVisualStyle」作用即可
        {
            switch (sStyleText)
            {
                case "Office 2007 Blue":
                    c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;
                    break;
                case "Office 2007 Silver":
                    c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
                    break;
                case "Office 2007 Black":
                    c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Black;
                    break;
                case "Office 2010 Blue":
                    c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
                    break;
                case "Office 2010 Silver":
                    c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Silver;
                    break;
                case "Office 2010 Black":
                    c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Black;
                    break;
                default:
                    c1Grid.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
                    break;
            }
        }

        public static void SetGridVisualStyle(C1TrueDBGrid c1Grid, float fFontSize)
        {
            if (fFontSize == 0)
            {
                fFontSize = 12;
            }

            #region Color
            if (string.IsNullOrEmpty(MyLibrary.sGridOddRowForeColor))
            {
                MyLibrary.sGridOddRowForeColor = MyLibrary.bDarkMode ? "#FFFFFF" : ""; //default
            }

            if (string.IsNullOrEmpty(MyLibrary.sGridOddRowBackColor))
            {
                MyLibrary.sGridOddRowBackColor = MyLibrary.bDarkMode ? "#262626" : ""; //default
            }

            if (string.IsNullOrEmpty(MyLibrary.sGridEvenRowForeColor))
            {
                MyLibrary.sGridEvenRowForeColor = MyLibrary.bDarkMode ? "#FFFFFF" : ""; //default
            }

            if (string.IsNullOrEmpty(MyLibrary.sGridEvenRowBackColor))
            {
                MyLibrary.sGridEvenRowBackColor = MyLibrary.bDarkMode ? "#0F243E" : ""; //default
            }

            if (string.IsNullOrEmpty(MyLibrary.sGridSelectedForeColor))
            {
                MyLibrary.sGridSelectedForeColor = ""; //default
            }

            if (string.IsNullOrEmpty(MyLibrary.sGridSelectedBackColor))
            {
                MyLibrary.sGridSelectedBackColor = ""; //default
            }
            #endregion

            //字型 + 字體大小
            c1Grid.Font = new Font(MyLibrary.sGridFontName, fFontSize, FontStyle.Regular, GraphicsUnit.Point);
            c1Grid.HeadingStyle.Font = new Font(MyLibrary.sGridFontName, fFontSize, FontStyle.Regular, GraphicsUnit.Point);

            c1Grid.OddRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowForeColor);
            c1Grid.OddRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowBackColor);
            c1Grid.EvenRowStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowForeColor);
            c1Grid.EvenRowStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);

            //Grid's 選取顏色
            c1Grid.SelectedStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedForeColor);
            c1Grid.SelectedStyle.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedBackColor);

            if (MyLibrary.bDarkMode)
            {
                #region Get Default Value
                if (string.IsNullOrEmpty(MyLibrary.sGridHeadingForeColor))
                {
                    MyLibrary.sGridHeadingForeColor = "#FFFFFF"; //White
                }

                if (string.IsNullOrEmpty(MyLibrary.sGridFontName))
                {
                    MyLibrary.sGridFontName = ""; //default
                }

                if (string.IsNullOrEmpty(MyLibrary.sGridFontSize))
                {
                    MyLibrary.sGridFontSize = "10";
                }
                #endregion

                c1Grid.BorderColor = Color.White;
                c1Grid.HeadingStyle.Borders.Color = Color.White;
                c1Grid.HeadingStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridHeadingForeColor);
                c1Grid.RowDivider.Color = Color.White;
                c1Grid.Font = new Font(MyLibrary.sGridFontName, Convert.ToSingle(MyLibrary.sGridFontSize), FontStyle.Regular, GraphicsUnit.Point);
                c1Grid.HeadingStyle.Font = new Font(MyLibrary.sGridFontName, Convert.ToSingle(MyLibrary.sGridFontSize), FontStyle.Regular, GraphicsUnit.Point);
                c1Grid.MarqueeStyle = MarqueeEnum.HighlightCell;
            }
        }

        public static void SetDockingTabColor(C1.Win.C1Command.C1DockingTab c1Tab, Color cBackColorSelected, Color cForeColorSelected, Color cForeColor)
        {
            foreach (Control tab in c1Tab.Controls)
            {
                var tabPage = (C1.Win.C1Command.C1DockingTabPage)tab;
                tabPage.TabBackColorSelected = cBackColorSelected;
                tabPage.TabForeColorSelected = cForeColorSelected;
                tabPage.TabForeColor = cForeColor;
            }
        }

        public static string GetMessageBoxErrorMsg(string sID, string sExceptionMsg, bool bTryAgain)
        {
            var sMsg = GetLanguageString("An unexpected error has occurred.", "Global", "Global", "msg", sID, "Text") + "\r\n\r\n" + sExceptionMsg;

            if (bTryAgain)
            {
                sMsg += "\r\n\r\n" + GetLanguageString("Please try again!", "Global", "Global", "msg", "PleaseTryAgain", "Text");
            }

            return sMsg;
        }

        public static string GetStringBetween(string sSource, string sStart, string sEnd)
        {
            if (!sSource.Contains(sStart) || !sSource.Contains(sEnd) || sStart != sEnd ||
                sSource.Length - sStart.Length - sEnd.Length != sSource.Replace(sStart, "").Length) return "";

            var iStart = sSource.IndexOf(sStart, 0, StringComparison.Ordinal) + sStart.Length;
            var iEnd = sSource.IndexOf(sEnd, iStart, StringComparison.Ordinal);

            var sResult = "";

            try
            {
                sResult = sSource.Substring(iStart, iEnd - iStart);
            }
            catch (Exception)
            {
                //
            }

            return sResult;
        }

        public static string GetStringBetween2(string sSource, string sStart, string sEnd, bool bContainsStart)
        {
            var i = bContainsStart ? sStart.Length : 0;

            if (!sSource.Contains(sStart))
            {
                return "";
            }

            if (!string.IsNullOrEmpty(sEnd) && !sSource.Contains(sEnd))
            {
                return "";
            }

            var iStart = sSource.IndexOf(sStart, 0, StringComparison.Ordinal) + sStart.Length;
            int iEnd;

            if (string.IsNullOrEmpty(sEnd))
            {
                iEnd = sSource.Length - iStart;
            }
            else
            {
                iEnd = sSource.IndexOf(sEnd, iStart, StringComparison.Ordinal);
            }

            var sResult = "";

            try
            {
                sResult = string.IsNullOrEmpty(sEnd) ? sSource.Substring(iStart - i, iEnd + i) : sSource.Substring(iStart, iEnd - iStart);
            }
            catch (Exception)
            {
                //
            }

            return sResult;
        }

        public static string DateDiff(DateTime DateTime1, DateTime DateTime2, string sMinutes = "")
        {
            var ts1 = new TimeSpan(DateTime1.Ticks);
            var ts2 = new TimeSpan(DateTime2.Ticks);
            var ts = ts1.Subtract(ts2).Duration();

            var sDateDiff = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00") + "." + ts.Milliseconds.ToString("000");

            return string.IsNullOrEmpty(sMinutes) ? sDateDiff : ts.Minutes.ToString("00");
        }

        public static bool IsNumeric(string sExpression)
        {
            var isNum = double.TryParse(sExpression, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out _);

            return isNum;
        }

        public static string GetDataTypeFormat_Oracle(IReadOnlyList<DataRow> dtRow, out string sDataType)
        {
            var sSchema = "";
            var sDataTypeResult = "";
            var sType = dtRow[0]["TypeName"].ToString().ToUpper();

            switch (sType)
            {
                case "RAW":
                case "CHAR":
                case "NCHAR":
                case "VARCHAR":
                case "VARCHAR2":
                case "NVARCHAR2":
                    sDataTypeResult = "string";
                    sSchema = "(" + dtRow[0]["ColumnSize"] + ")";
                    break;
                case "TIMESTAMP":
                    sDataTypeResult = "datetime";
                    sSchema = "(" + dtRow[0]["NumericScale"] + ")";
                    break;
                case "NUMBER":
                    if (dtRow[0]["DataType"].ToString() == "System.Decimal" && dtRow[0]["NumericPrecision"].ToString() == "38" && dtRow[0]["NumericScale"].ToString() == "0")
                    {
                        sDataTypeResult = "int";
                        sType = "INT";
                    }
                    else
                    {
                        int.TryParse(dtRow[0]["NumericScale"].ToString(), out var iValue);

                        if (iValue == 0 || iValue < 0)
                        {
                            sDataTypeResult = "int";
                            sSchema = "";
                        }
                        else
                        {
                            sDataTypeResult = "number"; //有小數點
                            sSchema = "(" + dtRow[0]["NumericPrecision"] + "," + dtRow[0]["NumericScale"] + ")";
                        }
                    }
                    break;
                case "DATE":
                    sDataTypeResult = "datetime";
                    break;
                case "INTEGER":
                case "LONG":
                    sDataTypeResult = "int";
                    sSchema = "";
                    break;
            }

            sDataType = sDataTypeResult;
            return sType + sSchema;
        }

        public static string GetDataTypeFormat_PostgreSQL(IReadOnlyList<DataRow> dtRow, out string sDataType)
        {
            string sSchema;
            string sDataTypeResult;
            var sType = dtRow[0]["ProviderType"].ToString();
            var sSystemDataType = dtRow[0]["DataType"].ToString();

            switch (sType)
            {
                case "16":
                    sDataTypeResult = "boolean";
                    sSchema = "boolean";
                    break;
                case "1000":
                    sDataTypeResult = "boolean";
                    sSchema = "boolean[]";
                    break;
                case "17":
                    sDataTypeResult = "string";
                    sSchema = "bytea";
                    break;
                case "1001":
                    sDataTypeResult = "string";
                    sSchema = "bytea[]";
                    break;
                case "20": //bigint, bigserial
                    sDataTypeResult = "int";
                    sSchema = "bigint";
                    break;
                case "1016":
                    sDataTypeResult = "int";
                    sSchema = "bigint[]";
                    break;
                case "21": //SmallInt (int16), smallserial
                    sDataTypeResult = "int";
                    sSchema = MyLibrary.bGridShowStreamlinedName ? "int" : "smallint";
                    break;
                case "1005": //SmallInt (int16)
                    sDataTypeResult = "int";
                    sSchema = "smallint[]";
                    break;
                case "23": //signed four-byte integer (int32), oid, serial
                    sDataTypeResult = "int";
                    sSchema = MyLibrary.bGridShowStreamlinedName ? "int" : "integer";
                    break;
                case "25":
                    sDataTypeResult = "string";
                    sSchema = "text";
                    break;
                case "1009":
                    sDataTypeResult = "string";
                    sSchema = "text[]";
                    break;
                case "700":
                    sDataTypeResult = "number";
                    sSchema = "real";
                    break;
                case "1021":
                    sDataTypeResult = "number";
                    sSchema = "real[]";
                    break;
                case "701":
                    sDataTypeResult = "number";
                    sSchema = "double precision";
                    break;
                case "1022":
                    sDataTypeResult = "number";
                    sSchema = "double precision[]";
                    break;
                case "1002":
                    sDataTypeResult = "string";
                    sSchema = "\"char\"[]";
                    break;
                case "1007":
                    sDataTypeResult = "string";
                    sSchema = MyLibrary.bGridShowStreamlinedName ? "int[]" : "integer[]";
                    break;
                case "1034":
                    sDataTypeResult = "string";
                    sSchema = "aclitem[]";
                    break;
                case "1042": //character
                    if (dtRow[0]["ColumnSize"].ToString() == "-1")
                    {
                        sSchema = MyLibrary.bGridShowStreamlinedName ? "char" : "character";
                    }
                    else
                    {
                        sSchema = (MyLibrary.bGridShowStreamlinedName ? "char" : "character") + "(" + dtRow[0]["ColumnSize"] + ")";
                    }

                    sDataTypeResult = "string";
                    break;
                case "1015": //character varying 或是「使用者自定字串」, ex:'e_name' as "EngName"
                case "1043": //string array, xid, xid8, cid, gtsvector, jsonpath, name, pg_lsn, pg_dependencies, pg_brin_minmax_multi_summary, pg_brin_bloom_summary, pg_mcv_list, pg_ndistinct, pg_node_tree, pg_snapshot, regclass, regcollation, regconfig, regdictionary, regnamespace, regoper, regoperator, regproc, regprocedure, regrole, regtype, tid, tsquery, tsvector, txid_snapshot
                    if (dtRow[0]["ColumnSize"].ToString() == "-1")
                    {
                        sSchema = MyLibrary.bGridShowStreamlinedName ? "varchar" : "character varying";
                    }
                    else
                    {
                        sSchema = (MyLibrary.bGridShowStreamlinedName ? "character" : "character varying") + "(" + dtRow[0]["ColumnSize"] + ")";
                    }

                    sSchema += (sType == "1015" ? "[]" : "");
                    sDataTypeResult = "string";
                    break;
                case "1012":
                    sDataTypeResult = "string";
                    sSchema = "cid[]";
                    break;
                case "1028": //int32[] or oid[]
                case "10001": //int32[] or oidvector
                    sDataTypeResult = "number";
                    sSchema = "int32[]";
                    break;
                case "1231":
                    if (dtRow[0]["NumericScale"].ToString() == "0")
                    {
                        sSchema = "numeric(" + dtRow[0]["NumericPrecision"] + ")";
                    }
                    else
                    {
                        //有小數點
                        sSchema = "numeric(" + dtRow[0]["NumericPrecision"] + "," + dtRow[0]["NumericScale"] + ")";
                    }

                    sDataTypeResult = "number";
                    sSchema = sSchema + "[]";
                    break;
                case "1700":
                    if (dtRow[0]["NumericScale"].ToString() == "0")
                    {
                        sDataTypeResult = "int";
                        sSchema = "numeric(" + dtRow[0]["NumericPrecision"] + ")";
                    }
                    else
                    {
                        sDataTypeResult = "number"; //有小數點
                        sSchema = "numeric(" + dtRow[0]["NumericPrecision"] + "," + dtRow[0]["NumericScale"] + ")";
                    }

                    sSchema = sSchema + (sType == "1231" ? "[]" : "");
                    break;
                case "1082":
                    sDataTypeResult = "datetime";
                    sSchema = "date";
                    break;
                case "1182":
                    sDataTypeResult = "datetime";
                    sSchema = "date[]";
                    break;
                case "1266":
                    sDataTypeResult = "datetime";
                    sSchema = MyLibrary.bGridShowStreamlinedName ? "date" : "time with time zone";
                    break;
                case "1270":
                    sDataTypeResult = "datetime";
                    sSchema = MyLibrary.bGridShowStreamlinedName ? "date[]" : "time with time zone[]";
                    break;
                case "1083":
                    sDataTypeResult = "datetime";
                    sSchema = MyLibrary.bGridShowStreamlinedName ? "date" : "time without time zone";
                    break;
                case "1183":
                    sDataTypeResult = "datetime";
                    sSchema = MyLibrary.bGridShowStreamlinedName ? "date[]" : "time without time zone[]";
                    break;
                case "1184":
                    sDataTypeResult = "datetime";
                    sSchema = MyLibrary.bGridShowStreamlinedName ? "date" : "timestamp with time zone";
                    break;
                case "1185":
                    sDataTypeResult = "datetime";
                    sSchema = MyLibrary.bGridShowStreamlinedName ? "date[]" : "timestamp with time zone[]";
                    break;
                case "1114":
                    sDataTypeResult = "datetime";
                    sSchema = MyLibrary.bGridShowStreamlinedName ? "date" : "timestamp without time zone";
                    break;
                case "1115":
                    sDataTypeResult = "datetime";
                    sSchema = MyLibrary.bGridShowStreamlinedName ? "date[]" : "timestamp without time zone[]";
                    break;
                case "1562":
                    sDataTypeResult = "string";
                    sSchema = "bit varying";
                    break;
                case "1563":
                    sDataTypeResult = "string";
                    sSchema = "bit varying[]";
                    break;
                case "2950":
                    sDataTypeResult = "string";
                    sSchema = "uuid";
                    break;
                case "2951":
                    sDataTypeResult = "string";
                    sSchema = "uuid[]";
                    break;
                case "142":
                    sDataTypeResult = "string";
                    sSchema = "xml";
                    break;
                case "143":
                    sDataTypeResult = "string";
                    sSchema = "xml[]";
                    break;
                case "1011":
                    sDataTypeResult = "string";
                    sSchema = "xid[]";
                    break;
                case "271":
                    sDataTypeResult = "string";
                    sSchema = "xid8[]";
                    break;
                case "1560":
                    sDataTypeResult = "string";
                    sSchema = "bit";
                    break;
                case "1561":
                    sDataTypeResult = "string";
                    sSchema = "bit[]";
                    break;
                case "603":
                    sDataTypeResult = "string";
                    sSchema = "box";
                    break;
                case "1020":
                    sDataTypeResult = "string";
                    sSchema = "box[]";
                    break;
                case "650":
                    sDataTypeResult = "string";
                    sSchema = "cidr";
                    break;
                case "651":
                    sDataTypeResult = "string";
                    sSchema = "cidr[]";
                    break;
                case "718":
                    sDataTypeResult = "string";
                    sSchema = "circle";
                    break;
                case "719":
                    sDataTypeResult = "string";
                    sSchema = "circle[]";
                    break;
                case "3912":
                    sDataTypeResult = "datetime";
                    sSchema = "daterange";
                    break;
                case "3913":
                    sDataTypeResult = "datetime";
                    sSchema = "daterange[]";
                    break;
                case "6155":
                    sDataTypeResult = "datetime";
                    sSchema = "datemultirange";
                    break;
                case "3644":
                    sDataTypeResult = "string";
                    sSchema = "gtsvector[]";
                    break;
                case "869":
                    sDataTypeResult = "string";
                    sSchema = "inet";
                    break;
                case "1041":
                    sDataTypeResult = "string";
                    sSchema = "inet[]";
                    break;
                case "22":
                    sDataTypeResult = "string";
                    sSchema = "int2vector";
                    break;
                case "1006":
                    sDataTypeResult = "string";
                    sSchema = "int2vector[]";
                    break;
                case "6150":
                    sDataTypeResult = "int";
                    sSchema = "int4multirange[]";
                    break;
                case "6157":
                    sDataTypeResult = "int";
                    sSchema = "int8multirange[]";
                    break;
                case "3904":
                    sDataTypeResult = "int";
                    sSchema = "int4range";
                    break;
                case "3905":
                    sDataTypeResult = "int";
                    sSchema = "int4range[]";
                    break;
                case "3926":
                    sDataTypeResult = "int";
                    sSchema = "int8range";
                    break;
                case "3927":
                    sDataTypeResult = "int";
                    sSchema = "int8range[]";
                    break;
                case "1186":
                    sDataTypeResult = "string";
                    sSchema = "interval";
                    break;
                case "1187":
                    sDataTypeResult = "string";
                    sSchema = "interval[]";
                    break;
                case "114":
                    sDataTypeResult = "string";
                    sSchema = "json";
                    break;
                case "199":
                    sDataTypeResult = "string";
                    sSchema = "json[]";
                    break;
                case "3802":
                    sDataTypeResult = "string";
                    sSchema = "jsonb";
                    break;
                case "3807":
                    sDataTypeResult = "string";
                    sSchema = "jsonb[]";
                    break;
                case "4073":
                    sDataTypeResult = "string";
                    sSchema = "jsonpath[]";
                    break;
                case "628":
                    sDataTypeResult = "string";
                    sSchema = "line";
                    break;
                case "629":
                    sDataTypeResult = "string";
                    sSchema = "line[]";
                    break;
                case "601":
                    sDataTypeResult = "string";
                    sSchema = "lseg";
                    break;
                case "1018":
                    sDataTypeResult = "string";
                    sSchema = "lseg[]";
                    break;
                case "829":
                    sDataTypeResult = "string";
                    sSchema = "macaddr";
                    break;
                case "1040":
                    sDataTypeResult = "string";
                    sSchema = "macaddr[]";
                    break;
                case "774":
                    sDataTypeResult = "string";
                    sSchema = "macaddr8";
                    break;
                case "775":
                    sDataTypeResult = "string";
                    sSchema = "macaddr8[]";
                    break;
                case "790":
                    sDataTypeResult = "number";
                    sSchema = "money";
                    break;
                case "791":
                    sDataTypeResult = "number";
                    sSchema = "money[]";
                    break;
                case "1003":
                    sDataTypeResult = "string";
                    sSchema = "name[]";
                    break;
                case "6151":
                    sDataTypeResult = "number";
                    sSchema = "nummultirange[]";
                    break;
                case "3906":
                    sDataTypeResult = "number";
                    sSchema = "numrange";
                    break;
                case "3907":
                    sDataTypeResult = "number";
                    sSchema = "numrange[]";
                    break;
                case "1013":
                    sDataTypeResult = "string";
                    sSchema = "oidvector[]";
                    break;
                case "3221":
                    sDataTypeResult = "string";
                    sSchema = "pg_lsn[]";
                    break;
                case "602":
                    sDataTypeResult = "string";
                    sSchema = "path";
                    break;
                case "1019":
                    sDataTypeResult = "string";
                    sSchema = "path[]";
                    break;
                case "5039":
                    sDataTypeResult = "string";
                    sSchema = "pg_snapshot[]";
                    break;
                case "600":
                    sDataTypeResult = "string";
                    sSchema = "point";
                    break;
                case "1017":
                    sDataTypeResult = "string";
                    sSchema = "point[]";
                    break;
                case "604":
                    sDataTypeResult = "string";
                    sSchema = "polygon";
                    break;
                case "1027":
                    sDataTypeResult = "string";
                    sSchema = "polygon[]";
                    break;
                case "1790":
                    sDataTypeResult = "string";
                    sSchema = "refcursor";
                    break;
                case "2201":
                    sDataTypeResult = "string";
                    sSchema = "refcursor[]";
                    break;
                case "2210":
                    sDataTypeResult = "string";
                    sSchema = "regclass[]";
                    break;
                case "4192":
                    sDataTypeResult = "string";
                    sSchema = "regcollation[]";
                    break;
                case "3735":
                    sDataTypeResult = "string";
                    sSchema = "regconfig[]";
                    break;
                case "3770":
                    sDataTypeResult = "string";
                    sSchema = "regdictionary[]";
                    break;
                case "4090":
                    sDataTypeResult = "string";
                    sSchema = "regnamespace[]";
                    break;
                case "2208":
                    sDataTypeResult = "string";
                    sSchema = "regoper[]";
                    break;
                case "2209":
                    sDataTypeResult = "string";
                    sSchema = "regoperator[]";
                    break;
                case "1008":
                    sDataTypeResult = "string";
                    sSchema = "regproc[]";
                    break;
                case "2207":
                    sDataTypeResult = "string";
                    sSchema = "regprocedure[]";
                    break;
                case "4097":
                    sDataTypeResult = "string";
                    sSchema = "regrole[]";
                    break;
                case "2211":
                    sDataTypeResult = "string";
                    sSchema = "regtype[]";
                    break;
                case "1010":
                    sDataTypeResult = "string";
                    sSchema = "tid[]";
                    break;
                case "3645":
                    sDataTypeResult = "string";
                    sSchema = "tsquery[]";
                    break;
                case "3908":
                    sDataTypeResult = "string";
                    sSchema = "tsrange";
                    break;
                case "3909":
                    sDataTypeResult = "string";
                    sSchema = "tsrange[]";
                    break;
                case "3910":
                    sDataTypeResult = "string";
                    sSchema = "tstzrange";
                    break;
                case "3911":
                    sDataTypeResult = "string";
                    sSchema = "tstzrange[]";
                    break;
                case "6153":
                    sDataTypeResult = "string";
                    sSchema = "tstzmultirange[]";
                    break;
                case "3643":
                    sDataTypeResult = "string";
                    sSchema = "tsvector[]";
                    break;
                case "2949":
                    sDataTypeResult = "string";
                    sSchema = "txid_snapshot[]";
                    break;
                case "6152":
                    sDataTypeResult = "string";
                    sSchema = "tsmultirange[]";
                    break;
                default:
                    sDataTypeResult = "string";
                    sSchema = sSystemDataType + "(" + dtRow[0]["ProviderType"] + ")";
                    break;
            }

            sDataType = sDataTypeResult;

            return sSchema;
        }

        public static string GetDataTypeFormat_SQLServer(IReadOnlyList<DataRow> dtRow, out string sDataType)
        {
            var sSchema = "";
            var sDataTypeResult = "string";
            var sType = dtRow[0]["DataTypeName"].ToString();

            switch (sType.ToUpper())
            {
                case "MASTER.SYS.GEOGRAPHY":
                    sType = "geography";
                    break;
                case "MASTER.SYS.GEOMETRY":
                    sType = "geometry";
                    break;
                case "MASTER.SYS.HIERARCHYID":
                    sType = "hierarchyid";
                    break;
                case "UNIQUEIDENTIFIER":
                case "IMAGE":
                case "NTEXT":
                case "XML":
                case "SQL_VARIANT":
                    break; //維持原 Type
                case "BINARY":
                case "CHAR":
                case "NCHAR":
                    sSchema = "(" + dtRow[0]["ColumnSize"] + ")";
                    break;
                case "NVARCHAR":
                case "VBINARY":
                case "VARCHAR":
                case "VARBINARY":
                    if (dtRow[0]["ColumnSize"].ToString() == "2147483647")
                    {
                        sSchema = "(max)";
                    }
                    else
                    {
                        sSchema = "(" + dtRow[0]["ColumnSize"] + ")";
                    }

                    break;
                case "TIME":
                    sDataTypeResult = "datetime";
                    sSchema = "(" + dtRow[0]["NumericScale"] + ")";
                    break;
                case "TIMESTAMP":
                    sDataTypeResult = "datetime";
                    break;
                case "DECIMAL":
                    int.TryParse(dtRow[0]["NumericScale"].ToString(), out var iValue);

                    if (iValue == 0 || iValue < 0)
                    {
                        sDataTypeResult = "int";
                        sSchema = "";
                    }
                    else
                    {
                        sDataTypeResult = "number"; //有小數點
                        sSchema = "(" + dtRow[0]["NumericPrecision"] + "," + dtRow[0]["NumericScale"] + ")";
                    }

                    break;
                case "DATE":
                case "DATETIME":
                    sDataTypeResult = "datetime";
                    break;
                case "DATETIME2":
                case "DATETIMEOFFSET":
                    sDataTypeResult = "datetime";
                    sSchema = "(" + dtRow[0]["NumericScale"] + ")";
                    break;
                case "BIGINT":
                case "INT":
                case "INTEGER":
                case "BIT":
                case "LONG":
                case "FLOAT":
                case "SMALLINT":
                case "TINYINT":
                case "NUMERIC":
                    sDataTypeResult = "int";
                    sSchema = "";
                    break;
            }

            sDataType = sDataTypeResult;
            return sType + sSchema;
        }

        public static string GetDataTypeFormat_MySQL(IReadOnlyList<DataRow> dtRow, out string sDataType)
        {
            var sSchema = "";
            var sDataTypeResult = "string";
            var sType = "";
            var sProviderType = dtRow[0]["ProviderType"].ToString();
            var sColumnSize = dtRow[0]["ColumnSize"].ToString();

            switch (sProviderType)
            {
                case "16":
                    sType = "tinyint";
                    sDataTypeResult = "int";
                    break;
                case "12" when sColumnSize == "3":
                    sType = "unsigned tinyint";
                    sDataTypeResult = "int";
                    break;
                case "12":
                    sType = "smallint";
                    sDataTypeResult = "int";
                    break;
                case "11" when sColumnSize == "9":
                    sType = "mediumint";
                    sDataTypeResult = "int";
                    break;
                case "11" when sColumnSize == "5":
                    sType = "unsigned smallint";
                    sDataTypeResult = "int";
                    break;
                case "11":
                    sType = "int";
                    sDataTypeResult = "int";
                    break;
                case "1":
                    sType = "bigint";
                    sDataTypeResult = "int";
                    break;
                case "10":
                    sType = "float";
                    sDataTypeResult = "number";
                    break;
                case "9":
                    sType = "double";
                    sDataTypeResult = "number";
                    break;
                case "8":
                    sType = "decimal";
                    sSchema = "(" + dtRow[0]["NumericPrecision"] + ", " + dtRow[0]["NumericScale"] + ")";
                    sDataTypeResult = "number";
                    break;
                case "6":
                    sType = "date";
                    break;
                case "7":
                    sType = "datetime";
                    sDataTypeResult = "datetime";
                    break;
                case "15":
                    sType = "timestamp";
                    sDataTypeResult = "datetime";
                    break;
                case "14":
                    sType = "time";
                    break;
                case "19":
                    sType = "year";
                    break;
                case "5" when sColumnSize == "1" && dtRow[0]["IsEnum"].ToString() == "True":
                    sType = "eum";
                    break;
                case "5" when sColumnSize == "1":
                    sType = "char";
                    sSchema = "(" + sColumnSize + ")";
                    break;
                case "5": //when sColumnSize == "2";
                    sType = "set";
                    break;
                case "18":
                    sType = "varchar";
                    sSchema = "(" + sColumnSize + ")";
                    break;
                case "4" when sColumnSize == "255":
                    sType = "tinyblob";
                    break;
                case "4" when sColumnSize == "16777215":
                    sType = "mediumblob";
                    break;
                case "4" when sColumnSize == "2147483647":
                    sType = "longblob";
                    break;
                case "4": //when sColumnSize == "65535"
                    sType = "blob";
                    sSchema = "(" + sColumnSize + ")";
                    break;
                case "13" when sColumnSize == "255":
                    sType = "tinytext";
                    break;
                case "13" when sColumnSize == "16777215":
                    sType = "mediumtext";
                    break;
                case "13" when sColumnSize == "2147483647":
                    sType = "longtext";
                    break;
                case "13": //when sColumnSize == "65535"
                    sType = "text";
                    break;
                case "2":
                    sType = "binary";
                    sSchema = "(" + sColumnSize + ")";
                    break;
                case "17":
                    sType = "varbinary";
                    sSchema = "(" + sColumnSize + ")";
                    break;
                case "3":
                    sType = "bit";
                    sSchema = "(" + sColumnSize + ")";
                    break;
                case "22":
                    sType = "json";
                    break;
                case "21":
                    sType = "geometry";
                    break;
            }

            sDataType = sDataTypeResult;
            return sType + sSchema;
        }

        public static string GetDataTypeFormat_SQLite(IReadOnlyList<DataRow> dtRow, out string sDataType)
        {
            var sSchema = "";
            var sDataTypeResult = "string";
            var sType = "";
            var sProviderType = dtRow[0]["ProviderType"].ToString();
            var sColumnSize = dtRow[0]["ColumnSize"].ToString();

            switch (sProviderType)
            {
                case "16":
                    sType = "tinyint";
                    sDataTypeResult = "int";
                    break;
                case "12" when sColumnSize == "3":
                    sType = "unsigned tinyint";
                    sDataTypeResult = "int";
                    break;
                case "12":
                    sType = "smallint";
                    sDataTypeResult = "int";
                    break;
                case "11" when sColumnSize == "9":
                    sType = "mediumint";
                    sDataTypeResult = "int";
                    break;
                case "11" when sColumnSize == "5":
                    sType = "unsigned smallint";
                    sDataTypeResult = "int";
                    break;
                case "11":
                    sType = "int";
                    sDataTypeResult = "int";
                    break;
                case "1":
                    sType = "bigint";
                    sDataTypeResult = "int";
                    break;
                case "10":
                    sType = "float";
                    sDataTypeResult = "number";
                    break;
                case "9":
                    sType = "double";
                    sDataTypeResult = "number";
                    break;
                case "8":
                    sType = "decimal";
                    sSchema = "(" + dtRow[0]["NumericPrecision"] + ", " + dtRow[0]["NumericScale"] + ")";
                    sDataTypeResult = "number";
                    break;
                case "6":
                    sType = "date";
                    break;
                case "7":
                    sType = "datetime";
                    sDataTypeResult = "datetime";
                    break;
                case "15":
                    sType = "timestamp";
                    sDataTypeResult = "datetime";
                    break;
                case "14":
                    sType = "time";
                    break;
                case "19":
                    sType = "year";
                    break;
                case "5" when sColumnSize == "1" && dtRow[0]["IsEnum"].ToString() == "True":
                    sType = "eum";
                    break;
                case "5" when sColumnSize == "1":
                    sType = "char";
                    sSchema = "(" + sColumnSize + ")";
                    break;
                case "5": //when sColumnSize == "2";
                    sType = "set";
                    break;
                case "18":
                    sType = "varchar";
                    sSchema = "(" + sColumnSize + ")";
                    break;
                case "4" when sColumnSize == "255":
                    sType = "tinyblob";
                    break;
                case "4" when sColumnSize == "16777215":
                    sType = "mediumblob";
                    break;
                case "4" when sColumnSize == "2147483647":
                    sType = "longblob";
                    break;
                case "4": //when sColumnSize == "65535"
                    sType = "blob";
                    sSchema = "(" + sColumnSize + ")";
                    break;
                case "13" when sColumnSize == "255":
                    sType = "tinytext";
                    break;
                case "13" when sColumnSize == "16777215":
                    sType = "mediumtext";
                    break;
                case "13" when sColumnSize == "2147483647":
                    sType = "longtext";
                    break;
                case "13": //when sColumnSize == "65535"
                    sType = "text";
                    break;
                case "2":
                    sType = "binary";
                    sSchema = "(" + sColumnSize + ")";
                    break;
                case "17":
                    sType = "varbinary";
                    sSchema = "(" + sColumnSize + ")";
                    break;
                case "3":
                    sType = "bit";
                    sSchema = "(" + sColumnSize + ")";
                    break;
                case "22":
                    sType = "json";
                    break;
                case "21":
                    sType = "geometry";
                    break;
            }

            sDataType = sDataTypeResult;
            return sType + sSchema;
        }

        public static void LoadLocalizationXML()
        {
            var sComboBoxString = sLocalizationList.Split(new[] { "`" }, StringSplitOptions.None);

            foreach (var t in sComboBoxString)
            {
                if (sLocalization != t.Split(';')[0])
                {
                    continue;
                }

                sXmlFilename = t.Split(';')[1];
                break;
            }

            var sXmlFullFilename = Application.StartupPath + @"\localization\" + sXmlFilename;

            if (File.Exists(sXmlFullFilename))
            {
                dtLocalization = XmlToDataTable(sXmlFullFilename);
            }
        }

        public static void ReplaceColumnNameByLanguageInfo(C1TrueDBGrid c1Grid, string sFormName, bool bTag = false)
        {
            foreach (C1DataColumn col1 in c1Grid.Columns)
            {
                //根據語系置換表頭欄位名稱
                if (bTag)
                {
                    col1.Tag = col1.Tag?.ToString() == "" ? col1.Tag : col1.Caption;
                    col1.Caption = GetLanguageString(col1.Caption, "form", sFormName, "gridheader", col1.Tag.ToString(), "Text");
                }
                else
                {
                    col1.Caption = GetLanguageString(col1.Caption, "form", sFormName, "gridheader", col1.Caption, "Text");
                }
            }
        }

        public static int ResizeGridColumnWidth(C1TrueDBGrid c1Grid)
        {
            var iTotalWidth = 0;

            foreach (C1DisplayColumn col in c1Grid.Splits[0].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 2000;
                }

                if ("`500`1000`1500`2000`".Contains("`" + sGridMaxWidth + "`") && col.Width > Convert.ToInt16(sGridMaxWidth))
                {
                    col.Width = Convert.ToInt16(sGridMaxWidth);
                }

                iTotalWidth += col.Width;
            }

            return iTotalWidth;
        }

        public static string TransferWordCase(string sWord, bool bToLower)
        {
            sWord = bToLower ? sWord.ToLower() : sWord;

            return sWord;
        }

        public static string GetCreateScript_Oracle(string sSchemaType, string sSchemaName)
        {
            if (clsOracleReader.GetState() == ConnectionState.Closed)
            {
                oOracleReader.ConnectTo();
            }

            string sSql;
            var sTemp = "";
            DateTime dtDateTime;
            DataTable dtTemp;

            //取得 Table/View 的「名稱、狀態、建立日期」
            switch (sSchemaType)
            {
                case "Tables":
                    {
                        sSql = "SELECT Object_Name AS Function_Name, Status, Created FROM all_objects WHERE Object_Type = 'TABLE' AND UPPER(Owner) = '{0}' AND Object_Name = '{1}'";
                        sSql = string.Format(sSql, sDBUser.ToUpper(), sSchemaName);
                        dtTemp = oOracleReader.ExecuteQueryToDataTable(sSql); //GetCreateScript_Oracle

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Created"].ToString());
                            sTemp = "--Table: \"" + sDBUser.ToUpper() + "\".\"" + sSchemaName + "\"\r\n--Status: " + dtTemp.Rows[0]["Status"] + "\r\n--Created: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "\r\n\r\n";
                        }

                        break;
                    }
                case "Views":
                    {
                        sSql = "SELECT os.Object_Name AS View_Name, vs.Text_Length, os.Status, os.Created FROM all_objects os, all_views vs WHERE os.Object_Type = 'VIEW' AND os.Object_Name = vs.View_Name AND UPPER(os.Owner) = '{0}' AND UPPER(vs.Owner) = '{0}' AND os.Object_Name = '{1}'";
                        sSql = string.Format(sSql, sDBUser.ToUpper(), sSchemaName);
                        dtTemp = oOracleReader.ExecuteQueryToDataTable(sSql); //GetCreateScript_Oracle

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Created"].ToString());
                            sTemp = "--View: \"" + sDBUser.ToUpper() + "\".\"" + sSchemaName + "\"\r\n--Status: " + dtTemp.Rows[0]["Status"] + "\r\n--Created: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "\r\n\r\n";
                        }

                        break;
                    }
            }

            //以下可以取得指定 Table/View 的 Created SQL
            sSql = "SELECT dbms_metadata.GET_DDL('{2}', '{1}', '{0}') AS ScriptText FROM DUAL";
            sSql = string.Format(sSql, sDBUser.ToUpper(), sSchemaName, sSchemaType.Substring(0, sSchemaType.Length - 1).ToUpper());
            dtTemp = oOracleReader.ExecuteQueryToDataTable(sSql); //GetCreateScript_Oracle

            var sSqlPane = dtTemp.Rows[0]["ScriptText"].ToString();

            if (sSqlPane.Substring(0, 1) == "\n")
            {
                sSqlPane = sSqlPane.Substring(1, sSqlPane.Length - 1);
            }

            sSqlPane = sTemp + sSqlPane;

            return sSqlPane;
        }

        public static string[] GetCreateScript_PostgreSQL(string sSchemaNode, string sSchemaType, string sSchemaName)
        {
            var sValue = new[] { "", "" }; //[0]: Scripts, [1]: 所有執行的 SQL 指令

            if (clsPostgreSQLReader.GetState() == ConnectionState.Closed)
            {
                oPostgreReader.ConnectTo(sDBConnectionString);
            }

            var sSql = "";
            var sScripts = "";
            var dtTemp = new DataTable();

            //取得 Table/View 的「名稱、狀態、建立日期」
            switch (sSchemaType)
            {
                case "Tables":
                    {
                        //取得指定的 Table's Create Script
                        sSql = "";
                        sSql += "SELECT FORMAT(E'CREATE %sTABLE IF NOT EXISTS %s%I\n(\n%s#$@', CASE c.relpersistence WHEN 't' THEN 'temporary ' ELSE '' END, CASE c.relpersistence WHEN 't' THEN '' ELSE n.nspname || '.' END, c.relname,\r\n";
                        sSql += "STRING_AGG(FORMAT(E'    %I %s%s', a.AttName, pg_catalog.FORMAT_TYPE(a.atttypid, a.atttypmod), CASE WHEN a.attnotnull THEN ' not null' ELSE '' end), E',\n' order by a.attnum)) AS sql, c.oid FROM pg_catalog.pg_class c JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace JOIN pg_catalog.pg_attribute a ON a.attrelid = c.oid AND a.attnum > 0 JOIN pg_catalog.pg_type t ON a.atttypid = t.oid\r\n";
                        sSql += "WHERE n.nspname = '{0}' and c.relname = '{1}' GROUP BY c.oid, c.relname, c.relpersistence, n.nspname;";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp); //GetCreateScript_PostgreSQL
                        sValue[1] += sSql + "\r\n";

                        if (dtTemp == null || dtTemp.Rows.Count <= 0)
                        {
                            return sValue;
                        }

                        var sTableScripts = "-- TABLE: {1}.{0}\r\n\r\n-- DROP TABLE IF EXISTS {1}.{0};\r\n\r\n" + dtTemp.Rows[0]["sql"];
                        sTableScripts = string.Format(sTableScripts, sSchemaName, sSchemaNode) + "\r\n";
                        var sOID = dtTemp.Rows[0]["oid"].ToString();

                        //取得 constraint info
                        #region 取得 constraint info
                        sSql = "";
                        sSql += "SELECT conrelid::regclass AS Table_From, conname, PG_GET_CONSTRAINTDEF(oid) AS \"info\"\r\n";
                        sSql += "FROM pg_constraint pp\r\n";
                        sSql += "WHERE pp.contype IN ('f', 'p', 'u')\r\n";
                        sSql += "AND pp.conrelid = {0} ORDER BY CASE pp.contype WHEN 'p' THEN 1 WHEN 'f' THEN 2 WHEN 'u' THEN 3 END";
                        sSql = string.Format(sSql, sOID);
                        ExecuteQueryToDataTable(sSql, ref dtTemp); //GetCreateScript_PostgreSQL
                        sValue[1] += sSql + "\r\n";

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                            {
                                sTableScripts = sTableScripts.Replace("#$@\r\n", ",\r\n    CONSTRAINT " + dtTemp.Rows[iRow]["conname"] + " " + dtTemp.Rows[iRow]["info"]) + "#$@";
                            }
                        }

                        sTableScripts = sTableScripts.Replace("#$@", "\r\n);");
                        #endregion

                        //20220726 取得 Table's Owner Info
                        sScripts = "";
                        sSql = "SELECT TableOwner FROM pg_tables WHERE SchemaName = '{0}' AND TableName = '{1}';";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp); //GetCreateScript_PostgreSQL
                        sValue[1] += sSql + "\r\n";

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sScripts += "\r\nALTER TABLE IF EXISTS " + sSchemaNode + "." + sSchemaName + "\r\n";
                            sScripts += "    OWNER to " + dtTemp.Rows[0]["TableOwner"] + ";";
                        }

                        sTableScripts += sScripts;

                        //取得 Table's Grant Info
                        #region 取得 Table's Grant Info
                        sScripts = "";
                        sSql = "SELECT REPLACE(grant_info, 'DELETE, INSERT, REFERENCES, SELECT, TRIGGER, TRUNCATE, UPDATE', 'ALL') AS Grant_Info, Owner_Info FROM (SELECT FORMAT('GRANT %s ON TABLE %I.%I TO %I%s;', STRING_AGG(Privilege_Type, ', '), Table_Schema, Table_Name, Grantee, CASE WHEN Is_Grantable = 'YES' then ' WITH GRANT OPTION' ELSE '' END) AS Grant_Info, Owner_Info FROM (SELECT tg.Table_Schema, tg.Table_Name, tg.Grantee, tg.Privilege_Type, tg.Is_Grantable, t.TableOwner AS Owner_Info FROM information_schema.role_table_grants tg JOIN pg_tables t ON t.SchemaName = tg.Table_Schema AND t.TableName = tg.Table_Name WHERE tg.Table_Schema = '{0}' AND tg.table_name = '{1}' /*AND t.TableOwner <> tg.Grantee*/ ORDER BY tg.Grantee, tg.Privilege_Type) ss GROUP BY Table_Schema, Table_Name, Grantee, Is_Grantable, Owner_Info ORDER BY Grant_Info) tt;";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp); //GetCreateScript_PostgreSQL
                        sValue[1] += sSql + "\r\n";

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sScripts = "\r\n\r\n-- GRANT INFORMATION\r\n\r\n";

                            for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                            {
                                sScripts += dtTemp.Rows[iRow]["Grant_Info"] + "\r\n";
                            }
                        }

                        sTableScripts += sScripts;
                        #endregion

                        //取得 Table's Index Info
                        #region 取得 Table's Index Info
                        sScripts = "";
                        sSql = "SELECT IndexName, IndexDef FROM pg_indexes WHERE SchemaName = '{0}' AND TableName = '{1}' ORDER BY IndexName;";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp); //GetCreateScript_PostgreSQL
                        sValue[1] += sSql + "\r\n";

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sScripts = "\r\n-- INDEX INFORMATION\r\n\r\n";

                            for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                            {
                                sScripts += dtTemp.Rows[iRow]["IndexDef"] + "\r\n";
                            }
                        }

                        sTableScripts += sScripts;
                        #endregion

                        //20220726 取得 Table's Comment Info
                        sScripts = "";
                        sSql = "SELECT OBJ_DESCRIPTION('{0}.{1}'::regclass);";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp); //GetCreateScript_PostgreSQL
                        sValue[1] += sSql + "\r\n";

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sScripts = "\r\n-- TABLE COMMENT INFORMATION\r\n\r\n";
                            sScripts += "COMMENT ON TABLE " + sSchemaNode + "." + sSchemaName + "\r\n";
                            sScripts += "    IS '" + dtTemp.Rows[0]["obj_description"].ToString().Replace("'", "''") + "';\r\n";
                        }

                        sTableScripts += sScripts;

                        //20220726 取得 Column's Comment Info
                        sScripts = "";
                        sSql = "";
                        sSql += "SELECT c.column_name, d.description\r\n";
                        sSql += "  FROM information_schema.columns c\r\n";
                        sSql += " INNER JOIN pg_class c1\r\n";
                        sSql += "    ON c.table_name = c1.relname\r\n";
                        sSql += " INNER JOIN pg_catalog.pg_namespace n\r\n";
                        sSql += "    ON c.table_schema = n.nspname AND c1.relnamespace = n.oid\r\n";
                        sSql += "  LEFT JOIN pg_catalog.pg_description d\r\n";
                        sSql += "    ON d.objsubid = c.ordinal_position AND d.objoid = c1.oid\r\n";
                        sSql += " WHERE c.table_catalog = '{0}'\r\n";
                        sSql += "   AND c.table_name = '{2}'\r\n";
                        sSql += "   AND c.table_schema = '{1}'\r\n";
                        sSql += "   AND d.description IS NOT NULL";
                        sSql = string.Format(sSql, sDBConnectionDatabase, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp); //GetCreateScript_PostgreSQL
                        sValue[1] += sSql + "\r\n";

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sScripts = "\r\n-- COLUMN COMMENT INFORMATION\r\n\r\n";

                            for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                            {
                                sScripts += "COMMENT ON COLUMN " + sSchemaNode + "." + sSchemaName + "." + dtTemp.Rows[iRow]["column_name"] + "\r\n";
                                sScripts += "    IS '" + dtTemp.Rows[iRow]["description"].ToString().Replace("'", "''") + "';\r\n\r\n";
                            }

                            if (sScripts.EndsWith("\r\n"))
                            {
                                sScripts = sScripts.Substring(0, sScripts.Length - 2);
                            }
                        }

                        if (sScripts.EndsWith("\r\n"))
                        {
                            sScripts = sScripts.Substring(0, sScripts.Length - 2);
                        }

                        sTableScripts += sScripts;

                        //取得 Table's Trigger Info
                        #region 取得 Table's Trigger Info
                        sScripts = "";
                        sSql = "SELECT Trigger_Name, Action_Timing, STRING_AGG(Event_Manipulation,' OR ') AS Event_Manipulation, Action_Orientation, Action_Statement FROM information_schema.triggers ss WHERE Trigger_Catalog = '{0}' AND Trigger_Schema = '{1}' AND Event_Object_Table = '{2}' GROUP BY Trigger_Name, Action_Timing, Action_Orientation, Action_Statement ORDER BY Trigger_Name, Event_Manipulation";
                        sSql = string.Format(sSql, sDBConnectionDatabase, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp); //GetCreateScript_PostgreSQL
                        sValue[1] += sSql + "\r\n";

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                            {
                                sScripts += "\r\n"; //Trigger 會有多筆，換行符號要放在這裡！

                                sScripts += "-- TRIGGER:" + dtTemp.Rows[iRow]["Trigger_Name"] + " ON " + sSchemaNode + "." + sSchemaName + "\r\n";
                                sScripts += "-- DROP TRIGGER " + dtTemp.Rows[iRow]["Trigger_Name"] + " ON " + sSchemaNode + "." + sSchemaName + "\r\n\r\n";
                                sScripts += "CREATE TRIGGER " + dtTemp.Rows[iRow]["Trigger_Name"] + "\r\n";
                                sScripts += "  " + dtTemp.Rows[iRow]["Action_Timing"] + " " + dtTemp.Rows[iRow]["Event_Manipulation"] + "\r\n";
                                sScripts += "  ON " + sSchemaNode + "." + sSchemaName + "\r\n";
                                sScripts += "  FOR EACH " + dtTemp.Rows[iRow]["Action_Orientation"] + "\r\n";
                                sScripts += "  " + dtTemp.Rows[iRow]["Action_Statement"] + ";\r\n";
                            }
                        }

                        sTableScripts += sScripts;
                        #endregion

                        if (sTableScripts.EndsWith("\r\n"))
                        {
                            sTableScripts = sTableScripts.Substring(0, sTableScripts.Length - 2);
                        }

                        sScripts = sTableScripts;

                        break;
                    }
                case "Views":
                    {
                        sSql = "SELECT Definition AS Scripts FROM pg_catalog.pg_views WHERE SchemaName NOT IN ('pg_catalog', 'information_schema') AND SchemaName = '{0}' AND ViewName = '{1}'";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        dtTemp = oPostgreReader.ExecuteQueryToDataTable(sSql); //GetCreateScript_PostgreSQL
                        var sTemp = "";

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sTemp = dtTemp.Rows[0][0].ToString();
                        }

                        sScripts = "-- VIEW: {0}\r\n\r\n-- " + "DROP VIEW" + " {0};\r\n\r\n" + "CREATE OR REPLACE VIEW" + " {0} AS \r\n{1}";
                        sScripts = string.Format(sScripts, sSchemaName, sTemp);

                        break;
                    }
            }

            sValue[0] = sScripts;

            return sValue;
        }

        public static string[] GetCreateScript_SQLServer(string sSchemaType, string sSchemaNode, string sSchemaDbo, string sSchemaName)
        {
            var sValue = new[] { "", "" }; //[0]: Scripts, [1]: 所有執行的 SQL 指令

            if (clsSQLServerReader.GetState() == ConnectionState.Closed)
            {
                oSQLServerReader.ConnectTo(sDBConnectionString);
            }

            var sSql = "";
            var sSqlPane = "";
            var dtTemp = new DataTable();

            //取得 Table/View 的「名稱、狀態、建立日期」
            switch (sSchemaType)
            {
                case "Tables":
                    {
                        #region 取得指定的 Table's 欄位資訊
                        sSql = "SELECT o.* FROM {0}.sys.objects o WHERE Type = 'U' AND Is_Ms_Shipped = 0 AND SCHEMA_NAME(Schema_ID) = '{1}' AND name = '{2}';";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaDbo, sSchemaName);
                        dtTemp = oSQLServerReader.ExecuteQueryToDataTable(sSql, false); //GetCreateScript_SQLServer
                        sValue[1] += sSql + "\r\n";

                        var sCreateDate = "";
                        var sModifyDate = "";
                        var sObjectID = "";

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            sObjectID = dtTemp.Rows[0]["Object_ID"].ToString();
                            var dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Create_Date"]);
                            sCreateDate = dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Modify_Date"]);
                            sModifyDate = dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                        }

                        sSqlPane = "-- Table: {0}.{1}\r\n-- Created: {2}\r\n-- Modified:{3}\r\n\r\n-- DROP TABLE [{0}].[{1}];\r\n\r\nCREATE TABLE [{0}].[{1}]\r\n(\r\n";
                        sSqlPane = string.Format(sSqlPane, sSchemaDbo, sSchemaName, sCreateDate, sModifyDate);

                        //取得Constraint資訊
                        sSql = "SELECT col.Column_Name, col.Ordinal_Position, con.Constraint_Name, con.Constraint_Type FROM {0}.INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS col, {0}.INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS con WHERE col.Table_Schema = '{1}' AND col.Table_Name = '{2}' AND col.Table_Schema = con.Table_Schema AND col.Table_Name = con.Table_Name AND col.Constraint_Name = con.Constraint_Name ORDER BY col.Ordinal_Position;"; //排序是為後面要取 Create Table 的 Constraint 資料
                        sSql = string.Format(sSql, sSchemaNode, sSchemaDbo, sSchemaName);
                        var dtConstraint = oSQLServerReader.ExecuteQueryToDataTable(sSql, false); //GetCreateScript_SQLServer
                        sValue[1] += sSql + "\r\n";

                        //取得Comment資訊
                        sSql = "SELECT c.Name AS \"Column_Name\", prop.Value AS \"comment\" FROM {0}.sys.extended_properties AS prop INNER JOIN {0}.sys.all_objects o ON prop.Major_ID = o.Object_ID INNER JOIN {0}.sys.schemas s ON o.Schema_ID = s.Schema_ID INNER JOIN {0}.sys.columns AS c ON prop.Major_ID = c.Object_ID AND prop.Minor_ID = c.Column_ID WHERE prop.Name = 'MS_Description' AND s.Name = '{1}' AND o.Name = '{2}';";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaDbo, sSchemaName);
                        var dtComment = oSQLServerReader.ExecuteQueryToDataTable(sSql, false); //GetCreateScript_SQLServer
                        sValue[1] += sSql + "\r\n";

                        //取得所有欄位資訊
                        sSql = "SELECT * FROM {0}.INFORMATION_SCHEMA.COLUMNS WHERE Table_Schema = '{1}' AND Table_Name = '{2}' ORDER BY Ordinal_Position;";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaDbo, sSchemaName);
                        dtTemp = oSQLServerReader.ExecuteQueryToDataTable(sSql, false); //GetCreateScript_SQLServer
                        sValue[1] += sSql + "\r\n";

                        if (dtTemp != null && dtTemp.Rows.Count > 0)
                        {
                            var sConstraintName = "";

                            for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                            {
                                var sDataType = dtTemp.Rows[iRow]["Data_Type"].ToString();
                                var sCollation = ""; //20220823 仿 SSMS，暫不顯示 → string.IsNullOrEmpty(dtTemp.Rows[iRow]["Collation_Name"].ToString()) ? "" : " COLLATE " + dtTemp.Rows[iRow]["Collation_Name"];
                                var sIsNullable = sCollation + "```DEFAULT```" + (dtTemp.Rows[iRow]["Is_Nullable"].ToString() == "NO" ? " NOT NULL" : " NULL");

                                switch (sDataType.ToUpper())
                                {
                                    case "NCHAR":
                                    case "BINARY":
                                    case "CHAR":
                                        sSqlPane += "    [" + dtTemp.Rows[iRow]["Column_Name"] + "] [" + sDataType + "](" + dtTemp.Rows[iRow]["Character_Maximum_Length"] + ")" + sIsNullable + (iRow == dtTemp.Rows.Count - 1 ? "" : ",\r\n");
                                        break;
                                    case "NVARCHAR":
                                    case "VBINARY":
                                    case "VARCHAR":
                                    case "VARBINARY":
                                        if (dtTemp.Rows[iRow]["Character_Maximum_Length"].ToString() == "-1")
                                        {
                                            sSqlPane += "    [" + dtTemp.Rows[iRow]["Column_Name"] + "] [" + sDataType + "](max)" + sIsNullable + (iRow == dtTemp.Rows.Count - 1 ? "" : ",\r\n");
                                        }
                                        else
                                        {
                                            sSqlPane += "    [" + dtTemp.Rows[iRow]["Column_Name"] + "] [" + sDataType + "](" + dtTemp.Rows[iRow]["Character_Maximum_Length"] + ")" + sIsNullable + (iRow == dtTemp.Rows.Count - 1 ? "" : ",\r\n");
                                        }

                                        break;
                                    case "DECIMAL":
                                    case "NUMERIC":
                                        sSqlPane += "    [" + dtTemp.Rows[iRow]["Column_Name"] + "] [" + sDataType + "](" + dtTemp.Rows[iRow]["Numeric_Precision"] + ", " + dtTemp.Rows[iRow]["Numeric_Scale"] + ")" + sIsNullable + (iRow == dtTemp.Rows.Count - 1 ? "" : ",\r\n");
                                        break;
                                    case "TIME":
                                    case "DATETIME2":
                                    case "DATETIMEOFFSET":
                                        sSqlPane += "    [" + dtTemp.Rows[iRow]["Column_Name"] + "] [" + sDataType + "](" + dtTemp.Rows[iRow]["DateTime_Precision"] + ")" + sIsNullable + (iRow == dtTemp.Rows.Count - 1 ? "" : ",\r\n");
                                        break;
                                    default:
                                        sSqlPane += "    [" + dtTemp.Rows[iRow]["Column_Name"] + "] [" + sDataType + "]" + sIsNullable + (iRow == dtTemp.Rows.Count - 1 ? "" : ",\r\n");
                                        break;
                                }

                                DataRow[] dtRow;
                                var sConstraintInfo = "";

                                if (dtConstraint != null && dtConstraint.Rows.Count > 0)
                                {
                                    dtRow = dtConstraint.Select("Column_Name='" + dtTemp.Rows[iRow]["Column_Name"].ToString().Replace("'", "''") + "'");

                                    if (dtRow.Length > 0)
                                    {
                                        sConstraintName = dtRow[0]["Constraint_Name"].ToString();
                                        sConstraintInfo = dtRow[0]["Constraint_Type"].ToString();
                                        sConstraintInfo = dtRow[0]["Constraint_Name"] + (string.IsNullOrEmpty(sConstraintInfo) ? "" : ", " + sConstraintInfo);
                                    }
                                }

                                var sColumnDefault = dtTemp.Rows[iRow]["Column_Default"].ToString();

                                if (sColumnDefault.Length > 2 && sColumnDefault.Substring(0, 1) == "(" && sColumnDefault.Substring(sColumnDefault.Length - 1, 1) == ")")
                                {
                                    sColumnDefault = sColumnDefault.Substring(1, sColumnDefault.Length - 2);

                                    if (sColumnDefault.Length > 2 && sColumnDefault.Substring(0, 1) == "(" && sColumnDefault.Substring(sColumnDefault.Length - 1, 1) == ")")
                                    {
                                        sColumnDefault = sColumnDefault.Substring(1, sColumnDefault.Length - 2);
                                    }
                                }

                                sSqlPane = sSqlPane.Replace("```DEFAULT```", string.IsNullOrEmpty(sColumnDefault) ? "" : " DEFAULT " + sColumnDefault);
                                var sComment = "";

                                if (dtComment != null && dtComment.Rows.Count > 0)
                                {
                                    dtRow = dtComment.Select("Column_Name='" + dtTemp.Rows[iRow]["Column_Name"].ToString().Replace("'", "''") + "'");

                                    if (dtRow.Length > 0)
                                    {
                                        sComment = dtRow[0]["Comment"].ToString();
                                    }
                                }
                            }

                            //取得 CONSTRAINT 最後 WITH 子句的條件
                            sSql = "";
                            sSql += "SELECT i.Name, t.Name AS tblname, u.Name AS uname, i.Object_ID, g.Name AS GroupName, i.Is_Padded, s.No_Recompute, i.Ignore_Dup_Key, i.Allow_Row_Locks, i.Allow_Page_Locks\r\n";
                            sSql += "FROM {0}.sys.indexes i\r\n";
                            sSql += "LEFT OUTER JOIN {0}.sys.data_spaces g\r\n";
                            sSql += "    ON i.Data_Space_ID = g.Data_Space_ID\r\n";
                            sSql += "INNER JOIN {0}.sys.objects t\r\n";
                            sSql += "    ON t.Object_ID = i.Object_ID\r\n";
                            sSql += "INNER JOIN {0}.sys.schemas u\r\n";
                            sSql += "    ON t.Schema_ID = u.Schema_ID\r\n";
                            sSql += "LEFT OUTER JOIN {0}.sys.stats s\r\n";
                            sSql += "    ON s.Object_ID = i.Object_ID\r\n";
                            sSql += "        AND s.Stats_ID = i.Index_ID\r\n";
                            sSql += "WHERE i.Index_ID > 0 AND t.Type = 'U' AND i.Name = '{1}'";
                            sSql = string.Format(sSql, sSchemaNode, sConstraintName);
                            var dtConstraint2 = oSQLServerReader.ExecuteQueryToDataTable(sSql, false); //GetCreateScript_SQLServer
                            sValue[1] += sSql + "\r\n";

                            if (dtConstraint2 != null && dtConstraint2.Rows.Count > 0)
                            {
                                sSqlPane += ",\r\n    CONSTRAINT [" + sConstraintName + "] PRIMARY KEY CLUSTERED\r\n    (\r\n";

                                //取得排序定義
                                sSql = "SELECT i.Name, ic.Column_ID, ic.Is_Descending_Key FROM {0}.sys.indexes i JOIN {0}.sys.index_columns ic ON i.Index_ID = ic.Index_ID AND i.Object_ID = ic.Object_ID WHERE i.Name = '{1}' ORDER BY ic.Key_Ordinal";
                                sSql = string.Format(sSql, sSchemaNode, sConstraintName);
                                var dtConstraint3 = oSQLServerReader.ExecuteQueryToDataTable(sSql, false); //GetCreateScript_SQLServer
                                sValue[1] += sSql + "\r\n";

                                if (dtConstraint3 != null && dtConstraint3.Rows.Count > 0)
                                {
                                    for (var iRow = 0; iRow < dtConstraint3.Rows.Count; iRow++)
                                    {
                                        var sColumn_Name = "";
                                        var dtRow = dtTemp.Select("Ordinal_Position = '" + dtConstraint3.Rows[iRow]["Column_ID"].ToString().Replace("'", "''") + "'");

                                        if (dtRow.Length > 0)
                                        {
                                            sColumn_Name = dtRow[0]["Column_Name"].ToString();
                                        }

                                        sSqlPane += "        [" + sColumn_Name + "] " + (dtConstraint3.Rows[iRow]["Is_Descending_Key"].ToString() == "True" ? "DESC" : "ASC") + (iRow == dtConstraint.Rows.Count - 1 ? "\r\n" : ",\r\n");
                                    }
                                }

                                sSqlPane += "    )\r\n    WITH\r\n    (\r\n";
                                sSqlPane += "        PAD_INDEX = " + (dtConstraint2.Rows[0]["Is_Padded"].ToString() == "True" ? "ON" : "OFF") + ", STATISTICS_NORECOMPUTE = " + (dtConstraint2.Rows[0]["No_Recompute"].ToString() == "True" ? "ON" : "OFF") + ", IGNORE_DUP_KEY = " + (dtConstraint2.Rows[0]["Ignore_Dup_Key"].ToString() == "True" ? "ON" : "OFF") + ",\r\n";
                                sSqlPane += "        ALLOW_ROW_LOCKS = " + (dtConstraint2.Rows[0]["Allow_Row_Locks"].ToString() == "True" ? "ON" : "OFF") + ", ALLOW_PAGE_LOCKS = " + (dtConstraint2.Rows[0]["Allow_Page_Locks"].ToString() == "True" ? "ON" : "OFF") + "\r\n";
                                sSqlPane += "    ) ON [" + dtConstraint2.Rows[0]["GroupName"] + "]\r\n";
                                sSqlPane += ") ON [" + dtConstraint2.Rows[0]["GroupName"] + "] TEXTIMAGE_ON [" + dtConstraint2.Rows[0]["GroupName"] + "]\r\nGO\r\n\r\n";
                            }
                            else
                            {
                                sSqlPane += "\r\n)\r\nGO\r\n\r\n";
                            }

                            //產生註解語法
                            if (dtComment != null && dtComment.Rows.Count > 0)
                            {
                                for (var iRow = 0; iRow < dtComment.Rows.Count; iRow++)
                                {
                                    sSqlPane += "EXEC " + sSchemaNode + ".sys.sp_addextendedproperty @name=N'MS_Description', @value=N'" + dtComment.Rows[iRow]["Comment"].ToString().Replace("'", "''") + "', @level0type=N'SCHEMA', @level0name=N'" + sSchemaDbo + "', @level1type=N'TABLE', @level1name=N'" + sSchemaName + "', @level2type=N'COLUMN', @level2name=N'" + dtComment.Rows[iRow]["Column_Name"] + "'\r\nGO\r\n\r\n";
                                }
                            }

                            //產生 indices 語法
                            sSql = "";
                            sSql += "SELECT i.Name, t.Name AS tblname, i.Index_ID, u.Name AS uname, i.Object_ID, g.Name AS GroupName, i.Is_Padded, s.No_Recompute, i.Ignore_Dup_Key, i.Allow_Row_Locks, i.Allow_Page_Locks\r\n";
                            sSql += "FROM {0}.sys.indexes i\r\n";
                            sSql += "LEFT OUTER JOIN {0}.sys.data_spaces g ON i.Data_Space_ID = g.Data_Space_ID\r\n";
                            sSql += "INNER JOIN {0}.sys.objects t ON t.Object_ID = i.Object_ID\r\n";
                            sSql += "INNER JOIN {0}.sys.schemas u ON t.Schema_ID = u.Schema_ID\r\n";
                            sSql += "LEFT OUTER JOIN {0}.sys.stats s ON s.Object_ID = i.Object_ID AND s.Stats_ID = i.Index_ID\r\n";
                            sSql += "WHERE i.Index_ID > 0 AND t.Type = 'U' AND i.Type_Desc = 'NONCLUSTERED' AND u.Name='{1}' AND t.Name = '{2}' ORDER BY i.Index_ID";
                            sSql = string.Format(sSql, sSchemaNode, sSchemaDbo, sSchemaName);
                            dtTemp = oSQLServerReader.ExecuteQueryToDataTable(sSql, false); //GetCreateScript_SQLServer
                            sValue[1] += sSql + "\r\n";

                            if (dtTemp != null && dtTemp.Rows.Count > 0)
                            {
                                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                                {
                                    sSql = "";
                                    sSql += "SELECT tab.Name Table_Name,\r\n";
                                    sSql += "       ix.Name Index_Name,\r\n";
                                    sSql += "       col.Name Index_Column_Name,\r\n";
                                    sSql += "       ixc.Index_Column_ID\r\n";
                                    sSql += "  FROM {0}.sys.indexes ix \r\n";
                                    sSql += "       INNER JOIN {0}.sys.index_columns ixc ON ix.Object_ID = ixc.Object_ID AND ix.Index_ID = ixc.Index_ID\r\n";
                                    sSql += "       INNER JOIN {0}.sys.columns col ON ix.Object_ID = col.Object_ID AND ixc.Column_ID = col.Column_ID\r\n";
                                    sSql += "       INNER JOIN {0}.sys.tables tab ON ix.Object_ID = tab.Object_ID\r\n";
                                    sSql += "WHERE ix.Object_ID = {1} AND ix.Type_Desc = 'NONCLUSTERED' AND ix.Name = '{2}' ORDER BY ixc.Index_ID, ixc.Index_Column_ID";
                                    sSql = string.Format(sSql, sSchemaNode, sObjectID, dtTemp.Rows[iRow]["Name"]);
                                    var dtTemp2 = oSQLServerReader.ExecuteQueryToDataTable(sSql, false); //GetCreateScript_SQLServer
                                    sValue[1] += sSql + "\r\n";

                                    //如果沒權限，dtTemp2 會是 null
                                    if (dtTemp2 != null && dtTemp2.Rows.Count > 0)
                                    {
                                        for (var i = 0; i < dtTemp2.Rows.Count; i++)
                                        {
                                            if (i == 0)
                                            {
                                                sSqlPane += "CREATE NONCLUSTERED INDEX {0} ON {1}.{2}\r\n(\r\n    [{3}]";
                                                sSqlPane = string.Format(sSqlPane, dtTemp.Rows[iRow]["Name"], sSchemaDbo, sSchemaName, dtTemp2.Rows[i]["index_column_name"]);
                                            }
                                            else
                                            {
                                                sSqlPane += ",\r\n    [{0}]";
                                                sSqlPane = string.Format(sSqlPane, dtTemp2.Rows[i]["Index_Column_Name"]);
                                            }

                                            //取得欄位的排序方式
                                            sSql = "SELECT i.Name, ic.Index_Column_ID, ic.Is_Descending_Key FROM {0}.sys.indexes i JOIN {0}.sys.index_columns ic ON i.Index_ID = ic.Index_ID AND i.Object_ID = ic.Object_ID AND i.Name = '{1}' AND ic.Index_Column_ID = {2}";
                                            sSql = string.Format(sSql, sSchemaNode, dtTemp.Rows[iRow]["Name"], dtTemp2.Rows[i]["Index_Column_ID"]);
                                            var dtTemp0 = oSQLServerReader.ExecuteQueryToDataTable(sSql, false); //GetCreateScript_SQLServer
                                            sValue[1] += sSql + "\r\n";

                                            if (dtTemp0 != null && dtTemp0.Rows.Count > 0)
                                            {
                                                sSqlPane += dtTemp0.Rows[0]["Is_Descending_Key"].ToString() == "True" ? " DESC" : " ASC";
                                            }

                                            if (dtTemp2.Rows.Count == 1 || i == dtTemp2.Rows.Count - 1) //最後一筆
                                            {
                                                sSqlPane += "\r\n)\r\n";
                                            }
                                        }

                                        sSqlPane += "";
                                        sSqlPane += "WITH\r\n(\r\n";
                                        sSqlPane += "    PAD_INDEX = {0},\r\n";
                                        sSqlPane += "    DROP_EXISTING = OFF,\r\n";
                                        sSqlPane += "    STATISTICS_NORECOMPUTE = {1},\r\n";
                                        sSqlPane += "    SORT_IN_TEMPDB = OFF,\r\n";
                                        sSqlPane += "    ONLINE = OFF,\r\n";
                                        sSqlPane += "    ALLOW_ROW_LOCKS = {2},\r\n";
                                        sSqlPane += "    ALLOW_PAGE_LOCKS = {3}\r\n)\r\n";
                                        sSqlPane += "ON [{4}]\r\n";
                                        sSqlPane += "GO\r\n\r\n";
                                        sSqlPane = string.Format(sSqlPane, dtTemp.Rows[iRow]["Is_Padded"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[iRow]["No_Recompute"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[iRow]["Allow_Row_Locks"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[iRow]["Allow_Page_Locks"].ToString() == "False" ? "OFF" : "ON", dtTemp.Rows[iRow]["GroupName"]);
                                    }
                                }
                            }

                            if (sSqlPane.EndsWith("\r\n\r\n"))
                            {
                                sSqlPane = sSqlPane.Substring(0, sSqlPane.Length - 4);
                            }
                        }
                        #endregion

                        break;
                    }
                case "Views":
                    {
                        sSql = "SELECT m.Definition, o.* FROM {0}.sys.objects o INNER JOIN {0}.sys.sql_modules m ON o.Object_ID = m.Object_ID WHERE o.Type = 'V' AND o.Is_Ms_Shipped <> 1 AND o.Name = '{1}';";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName.Replace(sSchemaDbo + ".", ""));
                        ExecuteQueryToDataTable(sSql, ref dtTemp); //GetCreateScript_SQLServer

                        if (!(dtTemp != null && dtTemp.Rows.Count > 0))
                        {
                            //
                        }
                        else
                        {
                            var dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Create_Date"].ToString());
                            var sTemp = "-- View: " + sSchemaDbo + "." + sSchemaName + "\r\n-- Created: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss") + "{0}\r\n\r\n";
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[0]["Modify_Date"].ToString());
                            sTemp = string.Format(sTemp, "\r\n-- Modified: " + dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss"));

                            sSqlPane = sTemp + dtTemp.Rows[0]["Definition"] + "\r\nGO";
                        }

                        break;
                    }
            }

            sValue[0] = sSqlPane;

            return sValue;
        }

        public static string GetCreateScript_MySQL(string sSchemaType, string sSchemaNode, string sSchemaName)
        {
            if (clsMySQLReader.GetState() == ConnectionState.Closed)
            {
                oMySQLReader.ConnectTo(sDBConnectionString);
            }

            var sSql = "";
            var sSqlPane = "";
            var dtTemp = new DataTable();

            //取得 Table/View 的「名稱、狀態、建立日期」
            switch (sSchemaType)
            {
                case "Tables":
                    {
                        sSql = "SHOW CREATE TABLE `{0}`.`{1}`;";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp); //GetCreateScript_MySQL

                        if (dtTemp == null || dtTemp.Rows.Count <= 0)
                        {
                            //
                        }
                        else
                        {
                            sSqlPane = dtTemp.Rows[0]["Create Table"].ToString();
                        }

                        break;
                    }
                case "Views":
                    {
                        sSql = "SHOW CREATE VIEW `{0}`.`{1}`;";
                        sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                        ExecuteQueryToDataTable(sSql, ref dtTemp); //GetCreateScript_MySQL

                        if (!(dtTemp != null && dtTemp.Rows.Count > 0))
                        {
                            //
                        }
                        else
                        {
                            sSqlPane = dtTemp.Rows[0]["Create View"].ToString();
                        }

                        sSqlPane = sSqlPane.Replace(" DEFINER=", "\r\n    DEFINER=").Replace(" SQL SECURITY ", "\r\nSQL SECURITY ").Replace(" VIEW `" + sSchemaNode + "`.`" + sSchemaName + "` AS ", "\r\nVIEW `" + sSchemaNode + "`.`" + sSchemaName + "`\r\nAS\r\n");

                        break;
                    }
            }

            return sSqlPane;
        }

        public static void GenerateRightMenu4CopyOnly(bool bFromSchemaBrowser, ContextMenuStrip cMenuSchemaBrowser, C1TrueDBGrid c1Grid, ScintillaEditor editor, string sTitle1, string sTitle2, string sName, int iX, int iY, bool bExtraPaste = true)
        {
            var i = 0;
            var a = Assembly.GetExecutingAssembly();

            cMenuSchemaBrowser.Items.Add(sName);
            cMenuSchemaBrowser.Items[i].Enabled = false;

            i++;
            cMenuSchemaBrowser.Items.Add("-");

            cMenuSchemaBrowser.Items.Add(sTitle1); //Copy to Clipboard

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                Clipboard.SetDataObject(sName, false);
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy 16x16-2.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.C;

            cMenuSchemaBrowser.Items.Add(sTitle2); //Paste to Query Editor

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sName;
                }
                else
                {
                    Clipboard.SetDataObject(sName, false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.paste2editor 16x16.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.V;

            if (bExtraPaste)
            {
                cMenuSchemaBrowser.Items.Add(sTitle2 + " + \", \""); //Paste to Query Editor + ", "

                i++;
                cMenuSchemaBrowser.Items[i].Click += delegate
                {
                    if (bFromSchemaBrowser)
                    {
                        sGlobalTemp = "PasteFromSchemaBrowser`" + sName + ", ";
                    }
                    else
                    {
                        Clipboard.SetDataObject(sName + ", ", false);
                        editor.Paste();

                        if (bAfterPasteFocusOnQueryEditor)
                        {
                            editor.Focus();
                        }
                    }
                };

                ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Shift | Keys.V;

                cMenuSchemaBrowser.Items.Add(sTitle2 + " + \", \\r\\n\""); //Paste to Query Editor + ", \r\n"

                i++;
                cMenuSchemaBrowser.Items[i].Click += delegate
                {
                    if (bFromSchemaBrowser)
                    {
                        sGlobalTemp = "PasteFromSchemaBrowser`" + sName + ", \r\n";
                    }
                    else
                    {
                        Clipboard.SetDataObject(sName + ", \r\n", false);
                        editor.Paste();

                        if (bAfterPasteFocusOnQueryEditor)
                        {
                            editor.Focus();
                        }
                    }
                };

                ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Alt | Keys.Shift | Keys.V;

                cMenuSchemaBrowser.Items.Add(sTitle2 + " + \"\\r\\n\""); //Paste to Query Editor + "\r\n"

                i++;
                cMenuSchemaBrowser.Items[i].Click += delegate
                {
                    if (bFromSchemaBrowser)
                    {
                        sGlobalTemp = "PasteFromSchemaBrowser`" + sName + "\r\n";
                    }
                    else
                    {
                        Clipboard.SetDataObject(sName + "\r\n", false);
                        editor.Paste();

                        if (bAfterPasteFocusOnQueryEditor)
                        {
                            editor.Focus();
                        }
                    }
                };

                ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.Shift | Keys.V;
            }

            c1Grid.ContextMenuStrip = cMenuSchemaBrowser;

            if (MyLibrary.bDarkMode)
            {
                cMenuSchemaBrowser.BackColor = ColorTranslator.FromHtml("#2D2D30");
                cMenuSchemaBrowser.ForeColor = Color.White;
                cMenuSchemaBrowser.RenderMode = ToolStripRenderMode.System;
                //cMenuSchemaBrowser.ShowImageMargin = false;
            }

            cMenuSchemaBrowser.Show(c1Grid, new Point(iX, iY));
        }

        public static void GenerateRightMenu4CopyOnly_SchemaBrowser(ContextMenuStrip cMenuSchemaBrowser, C1TrueDBGrid c1Grid, string sTitle1, string sTitle2, string sName, int iX, int iY)
        {
            var i = 0;
            var a = Assembly.GetExecutingAssembly();

            cMenuSchemaBrowser.Items.Add(sName);
            cMenuSchemaBrowser.Items[i].Enabled = false;

            i++;
            cMenuSchemaBrowser.Items.Add("-");

            cMenuSchemaBrowser.Items.Add(sTitle1); //Copy to Clipboard

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                Clipboard.SetDataObject(sName, false);
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy 16x16-2.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[2]).ShortcutKeys = Keys.Control | Keys.C;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \"\\r\\n\""); //Paste to Query Editor + "\r\n"

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                Clipboard.SetDataObject(sName + "\r\n", false);
                sGlobalTemp = "PasteFromSchemaBrowser`" + sName;

                //if (bAfterPasteFocusOnQueryEditor)
                //{
                //    editor.Focus();
                //}
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.paste2editor 16x16.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.Shift | Keys.V;

            c1Grid.ContextMenuStrip = cMenuSchemaBrowser;

            if (MyLibrary.bDarkMode)
            {
                cMenuSchemaBrowser.BackColor = ColorTranslator.FromHtml("#2D2D30");
                cMenuSchemaBrowser.ForeColor = Color.White;
                cMenuSchemaBrowser.RenderMode = ToolStripRenderMode.System;
                //cMenuSchemaBrowser.ShowImageMargin = false;
            }

            cMenuSchemaBrowser.Show(c1Grid, new Point(iX, iY));
        }

        public static void GenerateRightMenu4Copy_Oracle(bool bFromSchemaBrowser, C1TrueDBGrid c1GridSchemaBrowser, ContextMenuStrip cMenuSchemaBrowser, ScintillaEditor editor, string sAccessibleDescription, string sSchemaType, string sTitle1, string sTitle2, string sSchemaName, int iX, int iY)
        {
            var i = 0;
            var sPK = "";
            var a = Assembly.GetExecutingAssembly();

            cMenuSchemaBrowser.Items.Add(sSchemaName);
            cMenuSchemaBrowser.Items[i].Enabled = false;

            i++;
            cMenuSchemaBrowser.Items.Add("-");

            cMenuSchemaBrowser.Items.Add(sTitle1);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                Clipboard.SetDataObject(sSchemaName, false);
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy 16x16-2.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.C;

            cMenuSchemaBrowser.Items.Add(sTitle2);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName;
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName, false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.paste2editor 16x16.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \", \""); //Paste to Query Editor + ", "

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + ", ";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + ", ", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Shift | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \", \\r\\n\""); //Paste to Query Editor + ", \r\n"

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + ", \r\n";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + ", \r\n", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Alt | Keys.Shift | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \"\\r\\n\""); //Paste to Query Editor + "\r\n"

            i++;

            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + "\r\n";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + "\r\n", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.Shift | Keys.V;

            var sSql = "SELECT cols.Column_Name FROM all_constraints cons, all_cons_columns cols WHERE cols.Table_Name = '{0}' AND cons.Constraint_Type = 'P' AND cons.Constraint_Name = cols.Constraint_Name AND cons.Owner = cols.Owner AND UPPER(cons.Owner) = '{1}' ORDER BY cols.Table_Name, cols.Position";
            sSql = string.Format(sSql, sSchemaName, sDBUser.ToUpper());
            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp); //GenerateRightMenu4Copy_Oracle

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                sPK = "`";

                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    sPK += dtTemp.Rows[iRow]["Column_Name"] + "`";
                }
            }

            //取得 Table/View 的所有欄位 (按照原始順序)
            sSql = "SELECT '1' AS \" \", Column_Name, Column_ID, Data_Type AS TypeName, Data_Type AS DataTypeName, Data_Type AS DataType, Data_Length AS ColumnSize, Data_Scale AS NumericScale, Data_Precision AS NumericPrecision FROM user_tab_columns WHERE Table_Name = '{0}' ORDER BY Column_ID";
            sSql = string.Format(sSql, sSchemaName);
            var dtColumnName = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtColumnName); //GenerateRightMenu4Copy_Oracle

            i++;
            cMenuSchemaBrowser.Items.Add("-");

            var sLangText = GetLanguageString("Generate SQL Statement", "form", "frmGenerateSQL", "object", "this", "Text") + "...";
            cMenuSchemaBrowser.Items.Add(sLangText);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                using (var myForm = new frmGenerateSQL()) //GenerateRightMenu4Copy_Oracle
                {
                    myForm.sSchemaName = sSchemaName;
                    myForm.dtColumnName = dtColumnName;
                    myForm.sSchemaType = sSchemaType;
                    myForm.sSqlType = "S";
                    myForm.sPK = sPK;
                    myForm.sAccessibleDescription = sAccessibleDescription;

                    var iGenerateSqlFormWidth = 0;
                    var iGenerateSqlFormHeight = 0;

                    GetFormWidthAndHeight4GenerateSQL(ref iGenerateSqlFormWidth, ref iGenerateSqlFormHeight);

                    if (iGenerateSqlFormWidth > 0 && iGenerateSqlFormHeight > 0)
                    {
                        myForm.ClientSize = new Size(iGenerateSqlFormWidth - 16, iGenerateSqlFormHeight - 38);
                    }

                    myForm.ShowDialog();
                }
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.generate 16x16.ico"));
            c1GridSchemaBrowser.ContextMenuStrip = cMenuSchemaBrowser;

            if (MyLibrary.bDarkMode)
            {
                cMenuSchemaBrowser.BackColor = ColorTranslator.FromHtml("#2D2D30");
                cMenuSchemaBrowser.ForeColor = Color.White;
                cMenuSchemaBrowser.RenderMode = ToolStripRenderMode.System;
                //cMenuSchemaBrowser.ShowImageMargin = false;
            }

            cMenuSchemaBrowser.Show(c1GridSchemaBrowser, new Point(iX, iY));
        }

        public static void GenerateRightMenu4Copy_PostgreSQL(bool bFromSchemaBrowser, C1TrueDBGrid c1GridSchemaBrowser, ContextMenuStrip cMenuSchemaBrowser, ScintillaEditor editor, string sAccessibleDescription, string sSchemaNode, string sSchemaType, string sTitle1, string sTitle2, string sSchemaName, int iX, int iY)
        {
            var i = 0;
            var sPK = "";
            var a = Assembly.GetExecutingAssembly();

            cMenuSchemaBrowser.Items.Add(sSchemaName);
            cMenuSchemaBrowser.Items[i].Enabled = false;

            i++;
            cMenuSchemaBrowser.Items.Add("-");

            cMenuSchemaBrowser.Items.Add(sTitle1);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                Clipboard.SetDataObject(sSchemaName, false);
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy 16x16-2.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.C;

            cMenuSchemaBrowser.Items.Add(sTitle2);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName;
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName, false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.paste2editor 16x16.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \", \""); //Paste to Query Editor + ", "

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + ", ";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + ", ", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Shift | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \", \\r\\n\""); //Paste to Query Editor + ", \r\n"

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + ", \r\n";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + ", \r\n", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Alt | Keys.Shift | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \"\\r\\n\""); //Paste to Query Editor + "\r\n"

            i++;

            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + "\r\n";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + "\r\n", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.Shift | Keys.V;

            var dtTemp = new DataTable();
            var dtColumnName = new DataTable();

            if (sSchemaType == "Tables")
            {
                var sSql = "SELECT ee.Constraint_Schema AS SchemaName, ee.Table_Name AS TableName, ee.Column_Name, LOWER(SUBSTR(ss.Constraint_Type, 1, 1)) || ', ' || ss.Constraint_Name AS ConstraintInfo FROM information_schema.key_column_usage ee, information_schema.table_constraints ss WHERE ee.Constraint_Catalog = ss.Constraint_Catalog AND ee.Constraint_Schema = ss.Constraint_Schema AND ee.Table_Schema = ss.Table_Schema AND ee.Table_Name = ss.Table_Name AND ee.Constraint_Name = ss.Constraint_Name AND ss.Constraint_Type <> 'CHECK' AND ee.Constraint_Schema = '{0}' AND ee.Table_Name = '{1}' ORDER BY ee.Ordinal_Position";
                sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //GenerateRightMenu4Copy_PostgreSQL

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    sPK = "`";

                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        sPK += dtTemp.Rows[iRow]["Column_Name"] + "`";
                    }
                }

                //取得 Table 的所有指定欄位, 按照原始順序
                sSql = "SELECT '1' AS \" \", cs.Column_Name AS Column_Name, cs.Ordinal_Position AS Column_ID, LOWER(cs.Data_Type) AS TypeName, LOWER(cs.Data_Type) AS DataTypeName, LOWER(cs.Data_Type) AS DataType, cs.Character_Maximum_Length AS ColumnSize, cs.Numeric_Scale AS NumericScale, cs.Numeric_Precision AS NumericPrecision FROM pg_catalog.pg_tables ts, information_schema.columns cs WHERE ts.SchemaName != 'pg_catalog' AND ts.SchemaName != 'information_schema' AND ts.SchemaName = cs.Table_Schema AND ts.TableName = cs.Table_Name AND ts.SchemaName = '{0}' AND ts.TableName = '{1}' ORDER BY cs.Ordinal_Position";
                sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                ExecuteQueryToDataTable(sSql, ref dtColumnName); //GenerateRightMenu4Copy_PostgreSQL
            }
            else
            {
                var drPostgreSql = oPostgreReader.ExecuteQueryPaged100Rows("SELECT * FROM " + sSchemaNode + "." + sSchemaName + " LIMIT 1", 1, 1, out bool bRollback, out bool bPermissionDenied);

                if (drPostgreSql != null)
                {
                    dtSchemaTable = drPostgreSql.GetSchemaTable();

                    var dv = dtSchemaTable.DefaultView;
                    dv.Sort = "ColumnOrdinal";
                    dtTemp = dv.ToTable();

                    dtColumnName = new DataTable();

                    dtColumnName.Columns.Add(" ");
                    dtColumnName.Columns.Add("Column_Name");
                    dtColumnName.Columns.Add("Column_ID", typeof(int));
                    dtColumnName.Columns.Add("TypeName");
                    dtColumnName.Columns.Add("DataTypeName");
                    dtColumnName.Columns.Add("DataType");
                    dtColumnName.Columns.Add("ColumnSize");
                    dtColumnName.Columns.Add("NumericScale");
                    dtColumnName.Columns.Add("NumericPrecision");

                    for (var j = 0; j < dtTemp.Rows.Count; j++)
                    {
                        DataRow row = dtColumnName.NewRow();
                        row[" "] = "1";

                        var dtRow = dtTemp.Select("ColumnName = '" + dtTemp.Rows[j]["ColumnName"].ToString().Replace("'", "''") + "'");
                        var sColumnDataType = GetDataTypeFormat_PostgreSQL(dtRow, out var sDataType);

                        if (sColumnDataType.IndexOf("(", StringComparison.Ordinal) != -1)
                        {
                            sColumnDataType = sColumnDataType.Substring(0, sColumnDataType.IndexOf("(", StringComparison.Ordinal));
                        }

                        row["Column_Name"] = dtTemp.Rows[j]["ColumnName"];
                        row["Column_ID"] = dtTemp.Rows[j]["ColumnOrdinal"];
                        row["TypeName"] = sColumnDataType;
                        row["DataTypeName"] = sColumnDataType;
                        row["DataType"] = sColumnDataType;
                        row["ColumnSize"] = dtTemp.Rows[j]["ColumnSize"];
                        row["NumericScale"] = dtTemp.Rows[j]["NumericScale"];
                        row["NumericPrecision"] = dtTemp.Rows[j]["NumericPrecision"];

                        dtColumnName.Rows.Add(row);
                    }
                }
            }

            i++;
            cMenuSchemaBrowser.Items.Add("-");

            var sLangText = GetLanguageString("Generate SQL Statement", "form", "frmGenerateSQL", "object", "this", "Text") + "...";
            cMenuSchemaBrowser.Items.Add(sLangText);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                using (var myForm = new frmGenerateSQL()) //GenerateRightMenu4Copy_PostgreSQL
                {
                    myForm.sSchemaNode = sSchemaNode;
                    myForm.sSchemaName = sSchemaName;
                    myForm.dtColumnName = dtColumnName;
                    myForm.sSchemaType = sSchemaType;
                    myForm.sSqlType = "S";
                    myForm.sPK = sPK;
                    myForm.sAccessibleDescription = sAccessibleDescription;

                    var iGenerateSqlFormWidth = 0;
                    var iGenerateSqlFormHeight = 0;

                    GetFormWidthAndHeight4GenerateSQL(ref iGenerateSqlFormWidth, ref iGenerateSqlFormHeight);

                    if (iGenerateSqlFormWidth > 0 && iGenerateSqlFormHeight > 0)
                    {
                        myForm.ClientSize = new Size(iGenerateSqlFormWidth - 16, iGenerateSqlFormHeight - 38);
                    }

                    myForm.ShowDialog();
                }
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.generate 16x16.ico"));
            c1GridSchemaBrowser.ContextMenuStrip = cMenuSchemaBrowser;

            if (MyLibrary.bDarkMode)
            {
                cMenuSchemaBrowser.BackColor = ColorTranslator.FromHtml("#2D2D30");
                cMenuSchemaBrowser.ForeColor = Color.White;
                cMenuSchemaBrowser.RenderMode = ToolStripRenderMode.System;
                //cMenuSchemaBrowser.ShowImageMargin = false;
            }

            cMenuSchemaBrowser.Show(c1GridSchemaBrowser, new Point(iX, iY));
        }

        public static void GenerateRightMenu4Copy_SQLServer(bool bFromSchemaBrowser, C1TrueDBGrid c1GridSchemaBrowser, ContextMenuStrip cMenuSchemaBrowser, ScintillaEditor editor, string sAccessibleDescription, string sSchemaNode, string sSchemaDbo, string sSchemaType, string sTitle1, string sTitle2, string sSchemaName, int iX, int iY, string sObjectID)
        {
            var i = 0;
            var sPK = "";
            var a = Assembly.GetExecutingAssembly();

            cMenuSchemaBrowser.Items.Add(sSchemaName);
            cMenuSchemaBrowser.Items[i].Enabled = false;

            i++;
            cMenuSchemaBrowser.Items.Add("-");

            cMenuSchemaBrowser.Items.Add(sTitle1);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                Clipboard.SetDataObject(sSchemaName, false);
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy 16x16-2.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.C;

            cMenuSchemaBrowser.Items.Add(sTitle2);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName;
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName, false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.paste2editor 16x16.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \", \""); //Paste to Query Editor + ", "

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + ", ";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + ", ", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Shift | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \", \\r\\n\""); //Paste to Query Editor + ", \r\n"

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + ", \r\n";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + ", \r\n", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Alt | Keys.Shift | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \"\\r\\n\""); //Paste to Query Editor + "\r\n"

            i++;

            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + "\r\n";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + "\r\n", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.Shift | Keys.V;

            var sSql = "SELECT col.Column_Name, col.Ordinal_Position, con.Constraint_Name, con.Constraint_Type FROM {0}.INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS col, {0}.INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS con WHERE col.Table_Schema = '{1}' AND col.Table_Name = '{2}' AND col.Table_Schema = con.Table_Schema AND col.Table_Name = con.Table_Name AND col.Constraint_Name = con.Constraint_Name AND con.Constraint_Type = 'PRIMARY KEY' ORDER BY col.Ordinal_Position;";
            sSql = string.Format(sSql, sSchemaNode, sSchemaDbo, sSchemaName.Replace(sSchemaDbo + ".", ""));
            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp); //GenerateRightMenu4Copy_SQLServer

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                sPK = "`";

                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    sPK += dtTemp.Rows[iRow]["Column_Name"] + "`";
                }
            }

            var dtColumnName = new DataTable();

            //取得 Table 的所有指定欄位, 按照原始順序
            if (sSchemaType == "Tables")
            {
                sSql = "SELECT '1' AS \" \", Column_Name, Ordinal_Position AS Column_ID, Data_Type AS TypeName, Data_Type AS DataTypeName, Data_Type AS DataType, Character_Maximum_Length AS ColumnSize, Numeric_Scale AS NumericScale, Numeric_Precision AS NumericPrecision FROM {0}.INFORMATION_SCHEMA.COLUMNS WHERE Table_Name = '{1}' ORDER BY Ordinal_Position";
                sSql = string.Format(sSql, sSchemaNode, sSchemaName.Replace(sSchemaDbo + ".", ""));
            }
            else
            {
                //View
                sSql = "";
                sSql += "SELECT '1' AS \" \", c.Name AS Column_Name,\r\n";
                sSql += "       c.Column_ID,\r\n";
                sSql += "       TYPE_NAME(User_Type_ID) AS TypeName,\r\n";
                sSql += "       TYPE_NAME(User_Type_ID) AS DataTypeName,\r\n";
                sSql += "       TYPE_NAME(User_Type_ID) AS DataType,\r\n";
                sSql += "       c.Max_Length AS ColumnSize,\r\n";
                sSql += "       c.Scale AS NumericScale,\r\n";
                sSql += "       c.Precision AS NumericPrecision\r\n";
                sSql += "FROM {0}.sys.Columns c\r\n";
                sSql += "JOIN {0}.sys.Views v ON v.Object_ID = c.Object_ID\r\n";
                sSql += "WHERE c.Object_ID = {1}";
                sSql = string.Format(sSql, sSchemaNode, sObjectID);
            }

            ExecuteQueryToDataTable(sSql, ref dtColumnName); //GenerateRightMenu4Copy_SQLServer

            i++;
            cMenuSchemaBrowser.Items.Add("-");

            var sLangText = GetLanguageString("Generate SQL Statement", "form", "frmGenerateSQL", "object", "this", "Text") + "...";
            cMenuSchemaBrowser.Items.Add(sLangText);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                using (var myForm = new frmGenerateSQL()) //GenerateRightMenu4Copy_SQLServer
                {
                    myForm.sSchemaNode = sSchemaNode;
                    myForm.sSchemaDbo = sSchemaDbo;
                    myForm.sSchemaName = sSchemaName;
                    myForm.dtColumnName = dtColumnName;
                    myForm.sSchemaType = sSchemaType;
                    myForm.sSqlOfGetColumnInfo = sSql;
                    myForm.sSqlType = "S";
                    myForm.sPK = sPK;
                    myForm.sAccessibleDescription = sAccessibleDescription;

                    var iGenerateSqlFormWidth = 0;
                    var iGenerateSqlFormHeight = 0;

                    GetFormWidthAndHeight4GenerateSQL(ref iGenerateSqlFormWidth, ref iGenerateSqlFormHeight);

                    if (iGenerateSqlFormWidth > 0 && iGenerateSqlFormHeight > 0)
                    {
                        myForm.ClientSize = new Size(iGenerateSqlFormWidth - 16, iGenerateSqlFormHeight - 38);
                    }

                    myForm.ShowDialog();
                }
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.generate 16x16.ico"));
            c1GridSchemaBrowser.ContextMenuStrip = cMenuSchemaBrowser;

            if (MyLibrary.bDarkMode)
            {
                cMenuSchemaBrowser.BackColor = ColorTranslator.FromHtml("#2D2D30");
                cMenuSchemaBrowser.ForeColor = Color.White;
                cMenuSchemaBrowser.RenderMode = ToolStripRenderMode.System;
                //cMenuSchemaBrowser.ShowImageMargin = false;
            }

            cMenuSchemaBrowser.Show(c1GridSchemaBrowser, new Point(iX, iY));
        }

        public static void GenerateRightMenu4Copy_MySQL(bool bFromSchemaBrowser, C1TrueDBGrid c1GridSchemaBrowser, ContextMenuStrip cMenuSchemaBrowser, ScintillaEditor editor, string sAccessibleDescription, string sSchemaNode, string sSchemaType, string sTitle1, string sTitle2, string sSchemaName, int iX, int iY)
        {
            var i = 0;
            var sPK = "";
            var a = Assembly.GetExecutingAssembly();

            cMenuSchemaBrowser.Items.Add(sSchemaName);
            cMenuSchemaBrowser.Items[i].Enabled = false;

            i++;
            cMenuSchemaBrowser.Items.Add("-");

            cMenuSchemaBrowser.Items.Add(sTitle1);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                Clipboard.SetDataObject(sSchemaName, false);
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy 16x16-2.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.C;

            cMenuSchemaBrowser.Items.Add(sTitle2);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName;
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName, false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.paste2editor 16x16.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \", \""); //Paste to Query Editor + ", "

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + ", ";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + ", ", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Shift | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \", \\r\\n\""); //Paste to Query Editor + ", \r\n"

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + ", \r\n";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + ", \r\n", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Alt | Keys.Shift | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \"\\r\\n\""); //Paste to Query Editor + "\r\n"

            i++;

            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + "\r\n";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + "\r\n", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.Shift | Keys.V;

            var sSql = "";

            if (sSchemaType == "Tables")
            {
                sSql = "SELECT Column_Name, Constraint_Name AS ConstraintInfo FROM `information_schema`.`key_column_usage` WHERE Table_Schema = '{0}' AND Table_Name = '{1}' AND Referenced_Table_Name IS NOT NULL;";
                sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                var dtTemp = new DataTable();
                ExecuteQueryToDataTable(sSql, ref dtTemp); //GenerateRightMenu4Copy_MySQL

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    sPK = "`";

                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        sPK += dtTemp.Rows[iRow]["Column_Name"] + "`";
                    }
                }
            }

            //取得 Table/View 的所有指定欄位, 按照原始順序
            sSql = "SELECT '1' AS \" \", Column_Name, Ordinal_Position AS Column_ID, Column_Type AS TypeName, Column_Type AS DataTypeName, Column_Type AS DataType, Character_Maximum_Length AS ColumnSize, Numeric_Scale AS NumericScale, Numeric_Precision AS NumericPrecision FROM `information_schema`.`columns` WHERE Table_Schema = '{0}' AND Table_Name = '{1}' ORDER BY Ordinal_Position;";
            sSql = string.Format(sSql, sSchemaNode, sSchemaName);
            var dtColumnName = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtColumnName); //GenerateRightMenu4Copy_MySQL

            i++;
            cMenuSchemaBrowser.Items.Add("-");

            var sLangText = GetLanguageString("Generate SQL Statement", "form", "frmGenerateSQL", "object", "this", "Text") + "...";
            cMenuSchemaBrowser.Items.Add(sLangText);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                using (var myForm = new frmGenerateSQL()) //GenerateRightMenu4Copy_MySQL
                {
                    myForm.sSchemaName = sSchemaName;
                    myForm.dtColumnName = dtColumnName;
                    myForm.sSchemaType = sSchemaType;
                    myForm.sSchemaNode = sSchemaNode;
                    myForm.sSqlType = "S";
                    myForm.sPK = sPK;
                    myForm.sAccessibleDescription = sAccessibleDescription;

                    var iGenerateSqlFormWidth = 0;
                    var iGenerateSqlFormHeight = 0;

                    GetFormWidthAndHeight4GenerateSQL(ref iGenerateSqlFormWidth, ref iGenerateSqlFormHeight);

                    if (iGenerateSqlFormWidth > 0 && iGenerateSqlFormHeight > 0)
                    {
                        myForm.ClientSize = new Size(iGenerateSqlFormWidth - 16, iGenerateSqlFormHeight - 38);
                    }

                    myForm.ShowDialog();
                }
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.generate 16x16.ico"));
            c1GridSchemaBrowser.ContextMenuStrip = cMenuSchemaBrowser;

            if (MyLibrary.bDarkMode)
            {
                cMenuSchemaBrowser.BackColor = ColorTranslator.FromHtml("#2D2D30");
                cMenuSchemaBrowser.ForeColor = Color.White;
                cMenuSchemaBrowser.RenderMode = ToolStripRenderMode.System;
                //cMenuSchemaBrowser.ShowImageMargin = false;
            }

            cMenuSchemaBrowser.Show(c1GridSchemaBrowser, new Point(iX, iY));
        }

        public static void GenerateRightMenu4Copy_SQLite(bool bFromSchemaBrowser, C1TrueDBGrid c1GridSchemaBrowser, ContextMenuStrip cMenuSchemaBrowser, ScintillaEditor editor, string sAccessibleDescription, string sSchemaNode, string sSchemaType, string sTitle1, string sTitle2, string sSchemaName, int iX, int iY)
        {
            var i = 0;
            var sPK = "";
            var a = Assembly.GetExecutingAssembly();

            cMenuSchemaBrowser.Items.Add(sSchemaName);
            cMenuSchemaBrowser.Items[i].Enabled = false;

            i++;
            cMenuSchemaBrowser.Items.Add("-");

            cMenuSchemaBrowser.Items.Add(sTitle1);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                Clipboard.SetDataObject(sSchemaName, false);
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.copy 16x16-2.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.C;

            cMenuSchemaBrowser.Items.Add(sTitle2);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName;
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName, false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.paste2editor 16x16.ico"));
            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \", \""); //Paste to Query Editor + ", "

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + ", ";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + ", ", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Shift | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \", \\r\\n\""); //Paste to Query Editor + ", \r\n"

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + ", \r\n";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + ", \r\n", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Alt | Keys.Shift | Keys.V;

            cMenuSchemaBrowser.Items.Add(sTitle2 + " + \"\\r\\n\""); //Paste to Query Editor + "\r\n"

            i++;

            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                if (bFromSchemaBrowser)
                {
                    sGlobalTemp = "PasteFromSchemaBrowser`" + sSchemaName + "\r\n";
                }
                else
                {
                    Clipboard.SetDataObject(sSchemaName + "\r\n", false);
                    editor.Paste();

                    if (bAfterPasteFocusOnQueryEditor)
                    {
                        editor.Focus();
                    }
                }
            };

            ((ToolStripMenuItem)cMenuSchemaBrowser.Items[i]).ShortcutKeys = Keys.Control | Keys.Alt | Keys.Shift | Keys.V;

            var sSql = "";

            if (sSchemaType == "Tables")
            {
                sSql = "SELECT Column_Name, Constraint_Name AS ConstraintInfo FROM `information_schema`.`key_column_usage` WHERE Table_Schema = '{0}' AND Table_Name = '{1}' AND Referenced_Table_Name IS NOT NULL;";
                sSql = string.Format(sSql, sSchemaNode, sSchemaName);
                var dtTemp = new DataTable();
                ExecuteQueryToDataTable(sSql, ref dtTemp); //GenerateRightMenu4Copy_SQLite

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    sPK = "`";

                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        sPK += dtTemp.Rows[iRow]["Column_Name"] + "`";
                    }
                }
            }

            //取得 Table 的所有指定欄位, 按照原始順序
            sSql = "SELECT '1' AS \" \", Column_Name, Ordinal_Position AS Column_ID, Column_Type AS TypeName, Column_Type AS DataTypeName, Column_Type AS DataType, Character_Maximum_Length AS ColumnSize, Numeric_Scale AS NumericScale, Numeric_Precision AS NumericPrecision FROM `information_schema`.`columns` WHERE Table_Schema = '{0}' AND Table_Name = '{1}' ORDER BY Ordinal_Position;";
            sSql = string.Format(sSql, sSchemaNode, sSchemaName);
            var dtColumnName = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtColumnName); //GenerateRightMenu4Copy_SQLite

            i++;
            cMenuSchemaBrowser.Items.Add("-");

            var sLangText = GetLanguageString("Generate SQL Statement", "form", "frmGenerateSQL", "object", "this", "Text") + "...";
            cMenuSchemaBrowser.Items.Add(sLangText);

            i++;
            cMenuSchemaBrowser.Items[i].Click += delegate
            {
                using (var myForm = new frmGenerateSQL()) //GenerateRightMenu4Copy_SQLite
                {
                    myForm.sSchemaName = sSchemaName;
                    myForm.dtColumnName = dtColumnName;
                    myForm.sSchemaType = sSchemaType;
                    myForm.sSchemaNode = sSchemaNode;
                    myForm.sSqlType = "S";
                    myForm.sPK = sPK;
                    myForm.sAccessibleDescription = sAccessibleDescription;

                    var iGenerateSqlFormWidth = 0;
                    var iGenerateSqlFormHeight = 0;

                    GetFormWidthAndHeight4GenerateSQL(ref iGenerateSqlFormWidth, ref iGenerateSqlFormHeight);

                    if (iGenerateSqlFormWidth > 0 && iGenerateSqlFormHeight > 0)
                    {
                        myForm.ClientSize = new Size(iGenerateSqlFormWidth - 16, iGenerateSqlFormHeight - 38);
                    }

                    myForm.ShowDialog();
                }
            };

            cMenuSchemaBrowser.Items[i].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.generate 16x16.ico"));
            c1GridSchemaBrowser.ContextMenuStrip = cMenuSchemaBrowser;

            if (MyLibrary.bDarkMode)
            {
                cMenuSchemaBrowser.BackColor = ColorTranslator.FromHtml("#2D2D30");
                cMenuSchemaBrowser.ForeColor = Color.White;
                cMenuSchemaBrowser.RenderMode = ToolStripRenderMode.System;
                //cMenuSchemaBrowser.ShowImageMargin = false;
            }

            cMenuSchemaBrowser.Show(c1GridSchemaBrowser, new Point(iX, iY));
        }

        private static void GetFormWidthAndHeight4GenerateSQL(ref int iGenerateSQLFormWidth, ref int iGenerateSQLFormHeight)
        {
            var sTemp2 = "SELECT AttributeValue FROM SystemConfig WHERE DomainUser = '" + sDomainUser + "' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'GenerateSQLFormWidth'";
            var dtData = DBCommon.ExecQuery(sTemp2);

            if (dtData.Rows.Count > 0)
            {
                int.TryParse(dtData.Rows[0][0].ToString(), out iGenerateSQLFormWidth);
            }

            sTemp2 = "SELECT AttributeValue FROM SystemConfig WHERE DomainUser = '" + sDomainUser + "' AND AttributeKey = 'GlobalConfig' AND AttributeName = 'GenerateSQLFormHeight'";
            dtData = DBCommon.ExecQuery(sTemp2);

            if (dtData.Rows.Count > 0)
            {
                int.TryParse(dtData.Rows[0][0].ToString(), out iGenerateSQLFormHeight);
            }
        }

        public static void ExecuteQueryToDataTable(string sSql, ref DataTable dt, bool bShowErrMsg = true)
        {
            switch (sDataSource)
            {
                case "Oracle":
                    {
                        if (clsOracleReader.GetState() == ConnectionState.Closed)
                        {
                            oOracleReader.ConnectTo();
                        }

                        dt = oOracleReader.ExecuteQueryToDataTable(sSql, bShowErrMsg);
                        break;
                    }
                case "PostgreSQL":
                    {
                        if (clsPostgreSQLReader.GetState() == ConnectionState.Closed)
                        {
                            oPostgreReader.ConnectTo(sDBConnectionString);
                        }

                        dt = oPostgreReader.ExecuteQueryToDataTable(sSql, bShowErrMsg);
                        break;
                    }
                case "SQL Server":
                    {
                        if (clsSQLServerReader.GetState() == ConnectionState.Closed)
                        {
                            oSQLServerReader.ConnectTo(sDBConnectionString);
                        }

                        dt = oSQLServerReader.ExecuteQueryToDataTable(sSql, false);
                        break;
                    }
                case "MySQL":
                    {
                        if (clsMySQLReader.GetState() == ConnectionState.Closed)
                        {
                            oMySQLReader.ConnectTo(sDBConnectionString);
                        }

                        dt = oMySQLReader.ExecuteQueryToDataTable(sSql, bShowErrMsg);
                        break;
                    }
            }
        }

        public static void UpdateSchemaData_Oracle(C1TrueDBGrid c1Grid)
        {
            DataRow row;
            string sQty;
            var sTempDistinct = ",";

            dtAC4All = new DataTable();
            dtAC4All.Columns.Add("ObjectName");
            dtAC4All.Columns.Add("ObjectSource");

            CreateSchemaTable();

            //for frmSchemaSearch, 取得所有 Table + Functions + Views + Triggers + Procedure
            var sSql = "";
            sSql += "SELECT '' AS SchemaNode, 'TABLE' AS SchemaType, Object_Name AS SchemaName FROM all_objects\r\n";
            sSql += " WHERE object_type = 'TABLE' AND UPPER(Owner) = '{0}'\r\n";
            sSql += " UNION ALL\r\n";
            sSql += "SELECT '' AS SchemaNode, 'FUNCTION' AS SchemaType, Object_Name AS SchemaName FROM all_objects\r\n";
            sSql += " WHERE Object_Type = 'FUNCTION' AND UPPER(Owner) = '{0}'\r\n";
            sSql += " UNION ALL\r\n";
            sSql += "SELECT '' AS SchemaNode, 'VIEW' AS SchemaType, Object_Name AS SchemaName FROM all_objects\r\n";
            sSql += " WHERE Object_Type = 'VIEW' AND UPPER(Owner) = '{0}'\r\n";
            sSql += " UNION ALL\r\n";
            sSql += "SELECT '' AS SchemaNode, 'PROCEDURE' AS SchemaType, Object_Name AS SchemaName FROM all_objects\r\n";
            sSql += " WHERE Object_Type = 'PROCEDURE' AND UPPER(Owner) = '{0}'\r\n";
            sSql += " UNION ALL\r\n";
            sSql += "SELECT '' AS SchemaNode, 'TRIGGER' AS SchemaType, Object_Name AS SchemaName FROM all_objects\r\n";
            sSql += " WHERE Object_Type = 'TRIGGER' AND UPPER(Owner) = '{0}'";
            sSql = string.Format(sSql, sDBUser.ToUpper());
            ExecuteQueryToDataTable(sSql, ref dtTFVTP); //UpdateSchemaData_Oracle

            //for frmSchemaSearch, 取得所有 Table + 所有 Column
            sSql = "SELECT ss.Table_Name AS TableName, ss.Column_Name AS ColumnName, ss.Data_Type AS DataType" +
                   "  FROM user_tab_columns ss" +
                   " WHERE ss.Table_Name IN (SELECT object_name FROM all_objects WHERE object_type = 'TABLE' AND UPPER(Owner) = '{0}') ORDER BY ss.Table_Name, To_Number(ss.Column_ID)";
            sSql = string.Format(sSql, sDBUser.ToUpper());
            ExecuteQueryToDataTable(sSql, ref dtTableColumn); //UpdateSchemaData_Oracle

            //for Query Editor, 取得所有 Table + Views Name
            sSql = "";
            sSql += "SELECT '' AS SchemaNode, '[Table]' AS SchemaType, Object_Name AS SchemaName FROM all_objects\r\n";
            sSql += " WHERE object_type = 'TABLE' AND UPPER(Owner) = '{0}'\r\n";
            sSql += " UNION ALL\r\n";
            sSql += "SELECT '' AS SchemaNode, '[View]' AS SchemaType, Object_Name AS SchemaName FROM all_objects\r\n";
            sSql += " WHERE Object_Type = 'VIEW' AND UPPER(Owner) = '{0}'";
            sSql = string.Format(sSql, sDBUser.ToUpper());
            ExecuteQueryToDataTable(sSql, ref dtTableAndViewName); //UpdateSchemaData_Oracle

            //Functions
            #region Get info of Functions
            sSql = "SELECT Object_Name AS Function_Name, Status, Created FROM all_objects WHERE Object_Type = 'FUNCTION' AND UPPER(Owner) = '{0}' ORDER BY Object_Name";
            sSql = string.Format(sSql, sDBUser.ToUpper());
            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp);

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    row = dtSchema.NewRow();
                    row["SchemaObject"] = sDBConnectionName;
                    row["SchemaType"] = "Functions" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                    row["SchemaName"] = dtTemp.Rows[iRow]["Function_Name"].ToString();

                    if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Function_Name"] + ",", StringComparison.Ordinal) == -1)
                    {
                        sTempDistinct += dtTemp.Rows[iRow]["Function_Name"] + ",";

                        if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedFunctions)
                        {
                            AddACData(dtTemp.Rows[iRow]["Function_Name"].ToString(), "U-Functions");
                        }
                    }

                    Application.DoEvents();
                    dtSchema.Rows.Add(row);
                }

                MyLibrary.sKeywordsUserDefinedFunctions += sTempDistinct.Replace(",", " ").Trim() + " ";
            }
            #endregion

            sTempDistinct = ",";

            //Tables
            #region Get info of Tables
            if (bShowColumnInfo)
            {
                sSql = "SELECT 'TABLE' AS Schema_Type, ss.Table_Name AS TableName, ss.Column_Name AS ColumnName, ss.Column_ID AS ColumnID, ss.Data_Type AS DataType, ss.Data_Length AS DataLength, ss.Data_Precision || ',' || ss.Data_Scale AS Scale" +
                       "  FROM user_tab_columns ss" +
                       " WHERE ss.Table_Name IN (SELECT object_name FROM all_objects WHERE object_type = 'TABLE' AND UPPER(Owner) = '{0}') ORDER BY ss.Table_Name, " + (bSortByColumnName ? "ss.Column_Name" : "To_Number(ss.Column_ID)");
                sSql = string.Format(sSql, sDBUser.ToUpper());
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_Oracle

                sSql = "SELECT ts.Table_Name AS TableName, ts.Num_Rows AS NumRows FROM all_tables ts WHERE ts.Owner = '{0}' ORDER BY Table_Name";
                sSql = string.Format(sSql, sDBUser.ToUpper());
                var dtTableQty = new DataTable();
                ExecuteQueryToDataTable(sSql, ref dtTableQty); //UpdateSchemaData_Oracle

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaType"] = "Tables" + sSeparator + "(" + dtTableQty.Rows.Count + ")";

                        var dtQty = dtTableQty.Select("TableName = '" + dtTemp.Rows[iRow]["TableName"] + "'");
                        double.TryParse(dtQty.Length == 0 ? "0" : dtQty[0]["NumRows"].ToString(), out var dRowCount);
                        sQty = sSeparator + "(" + dRowCount.ToString("###,##0") + ")";

                        row["SchemaName"] = dtTemp.Rows[iRow]["TableName"] + sQty;

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["TableName"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["TableName"] + ",";

                            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedTables)
                            {
                                AddACData(dtTemp.Rows[iRow]["TableName"].ToString(), "Tables");
                            }
                        }

                        var sColumnInfo = dtTemp.Rows[iRow]["ColumnName"].ToString();

                        string sDataType;

                        switch (dtTemp.Rows[iRow]["DataType"].ToString())
                        {
                            case "RAW":
                            case "CHAR":
                            case "NCHAR":
                            case "VARCHAR":
                            case "VARCHAR2":
                            case "NVARCHAR2":
                                sDataType = dtTemp.Rows[iRow]["DataType"] + "(" + dtTemp.Rows[iRow]["DataLength"] + ")";
                                break;
                            case "NUMBER":
                                switch (dtTemp.Rows[iRow]["Scale"].ToString())
                                {
                                    case ",":
                                        sDataType = dtTemp.Rows[iRow]["DataType"] + "";
                                        break;
                                    case ",0":
                                        sDataType = dtTemp.Rows[iRow]["DataType"] + "(*,0)";
                                        break;
                                    default:
                                        sDataType = dtTemp.Rows[iRow]["DataType"] + "(" + dtTemp.Rows[iRow]["Scale"] + ")";
                                        break;
                                }
                                break;
                            default:
                                sDataType = dtTemp.Rows[iRow]["DataType"].ToString();
                                break;
                        }

                        row["ColumnInfo"] = sColumnInfo + ", " + sDataType;
                        Application.DoEvents();
                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedTables += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
            }
            else
            {
                sSql = "SELECT Object_Name AS TableName FROM all_objects WHERE Object_Type='TABLE' AND UPPER(Owner) = '{0}' ORDER BY Object_Name";
                sSql = string.Format(sSql, sDBUser.ToUpper());
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_Oracle

                sSql = "SELECT ts.Table_Name AS TableName, ts.Num_Rows AS NumRows FROM all_tables ts WHERE ts.Owner = '{0}' ORDER BY Table_Name";
                sSql = string.Format(sSql, sDBUser.ToUpper());
                var dtTableQty = new DataTable();
                ExecuteQueryToDataTable(sSql, ref dtTableQty); //UpdateSchemaData_Oracle

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaType"] = "Tables" + sSeparator + "(" + dtTemp.Rows.Count + ")";

                        var dtQty = dtTableQty.Select("TableName = '" + dtTemp.Rows[iRow]["TableName"] + "'");
                        double.TryParse(dtQty.Length == 0 ? "0" : dtQty[0]["NumRows"].ToString(), out var dRowCount);
                        sQty = sSeparator + "(" + dRowCount.ToString("###,##0") + ")";

                        row["SchemaName"] = dtTemp.Rows[iRow]["TableName"] + sQty;

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["TableName"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["TableName"] + ",";
                        }

                        Application.DoEvents();
                        dtSchema.Rows.Add(row);

                        if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedTables)
                        {
                            AddACData(dtTemp.Rows[iRow]["TableName"].ToString(), "Tables");
                        }
                    }

                    MyLibrary.sKeywordsUserDefinedTables += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
            }
            #endregion

            sTempDistinct = ",";

            //Triggers
            #region Get info of Triggers
            sSql = "SELECT Object_Name AS Trigger_Name, Status, Created FROM all_objects WHERE Object_Type = 'TRIGGER' AND UPPER(owner) = '{0}' ORDER BY Object_Name";
            sSql = string.Format(sSql, sDBUser.ToUpper());
            ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_Oracle

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    row = dtSchema.NewRow();
                    row["SchemaObject"] = sDBConnectionName;
                    row["SchemaType"] = "Triggers" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                    row["SchemaName"] = dtTemp.Rows[iRow]["Trigger_Name"].ToString();

                    if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Trigger_Name"] + ",", StringComparison.Ordinal) == -1)
                    {
                        sTempDistinct += dtTemp.Rows[iRow]["Trigger_Name"] + ",";

                        if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedTriggers)
                        {
                            AddACData(dtTemp.Rows[iRow]["Trigger_Name"].ToString(), "Triggers");
                        }
                    }

                    Application.DoEvents();
                    dtSchema.Rows.Add(row);
                }

                MyLibrary.sKeywordsUserDefinedTriggers += sTempDistinct.Replace(",", " ").Trim() + " ";
            }
            #endregion

            sTempDistinct = ",";

            //Views
            #region Get info of Views
            sSql = "SELECT os.Object_Name AS View_Name, vs.Text_Length, os.Status, os.Created FROM all_objects os, all_views vs WHERE os.Object_Type = 'VIEW' AND os.Object_Name = vs.View_Name AND UPPER(os.Owner) = '{0}' ORDER BY os.Object_Name";
            sSql = string.Format(sSql, sDBUser.ToUpper());
            ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_Oracle

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    row = dtSchema.NewRow();
                    row["SchemaObject"] = sDBConnectionName;
                    row["SchemaType"] = "Views" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                    row["SchemaName"] = dtTemp.Rows[iRow]["View_Name"].ToString();

                    if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["View_Name"] + ",", StringComparison.Ordinal) == -1)
                    {
                        sTempDistinct += dtTemp.Rows[iRow]["View_Name"] + ",";

                        if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedViews)
                        {
                            AddACData(dtTemp.Rows[iRow]["View_Name"].ToString(), "Views");
                        }
                    }

                    Application.DoEvents();
                    dtSchema.Rows.Add(row);
                }

                MyLibrary.sKeywordsUserDefinedViews += sTempDistinct.Replace(",", " ").Trim() + " ";
            }
            #endregion

            #region 處理 Auto Complete 資訊
            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACBuiltInKeywords)
            {
                var part1 = MyLibrary.sKeywordsBuiltInKeywords.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in part1)
                {
                    AddACData(t, "B-Keywords");
                }
            }

            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACBuiltInFunctions)
            {
                var part1 = MyLibrary.sKeywordsBuiltInFunctions.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in part1)
                {
                    AddACData(t, "B-Functions");
                }
            }

            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedKeywords)
            {
                var part1 = MyLibrary.sKeywordsUserDefinedKeywords.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in part1)
                {
                    AddACData(t, "U-Keywords");
                }
            }
            #endregion

            UpdateSchemaData(c1Grid, true); //UpdateSchemaData_Oracle
        }

        public static void GetTableInfo_Oracle(string sSchemaName, out DataTable dtTable)
        {
            var sSql = "";
            sSql += "SELECT Object_Name AS SchemaName FROM all_objects\r\n";
            sSql += " WHERE object_type = 'TABLE' AND UPPER(Owner) = '{0}' ORDER BY Object_Name";
            sSql = string.Format(sSql, sDBUser.ToUpper());
            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp); //GetTableInfo_Oracle
            dtTable = dtTemp.Copy();
        }

        public static void GetViewInfo_Oracle(string sSchemaName, out DataTable dtTable)
        {
            var sSql = "";
            sSql += "SELECT Object_Name AS SchemaName FROM all_objects\r\n";
            sSql += " WHERE Object_Type = 'VIEW' AND UPPER(Owner) = '{0}' ORDER BY Object_Name";
            sSql = string.Format(sSql, sDBUser.ToUpper());
            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp); //GetViewInfo_Oracle
            dtTable = dtTemp.Copy();
        }

        public static void UpdateSchemaData_PostgreSQL(C1TrueDBGrid c1Grid, bool bUpdate = true)
        {
            DataRow row;
            string sQty;
            var sTempDistinct = ",";

            dtAC4All = new DataTable();
            dtAC4All.Columns.Add("ObjectName");
            dtAC4All.Columns.Add("ObjectSource");

            CreateSchemaTable();

            //for frmSchemaSearch, 取得所有 Table + Functions + Views + Triggers + Procedure
            var sSql = "";
            sSql += "SELECT Table_Schema AS SchemaNode, 'TABLE' AS SchemaType, table_name AS SchemaName FROM information_schema.tables\r\n";
            sSql += " WHERE Table_Type = 'BASE TABLE' AND Table_Schema NOT IN ('pg_catalog', 'information_schema')\r\n";
            sSql += " UNION ALL\r\n";

            if (sDBVersion == ">=11")
            {
                //20230127 加判 prokind：f for a normal function, p for a procedure, a for an aggregate function, or w for a window function
                sSql += "SELECT n.nspname AS SchemaNode, 'FUNCTION' AS SchemaType, p.proname AS SchemaName FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE p.prokind <> 'p' AND n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname <> 'trigger' AND p.prokind = 'f'\r\n";
            }
            else
            {
                sSql += "SELECT n.nspname AS SchemaNode, 'FUNCTION' AS SchemaType, p.proname AS SchemaName FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE NOT p.proisagg AND n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname <> 'trigger'\r\n";
            }

            sSql += " UNION ALL\r\n";

            if (sDBVersion == ">=11")
            {
                sSql += "SELECT n.nspname AS SchemaNode, 'TRIGGER' AS SchemaType, p.proname AS SchemaName FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE p.prokind <> 'p' AND n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname = 'trigger'\r\n";
            }
            else
            {
                sSql += "SELECT n.nspname AS SchemaNode, 'TRIGGER' AS SchemaType, p.proname AS SchemaName FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE NOT p.proisagg AND n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname = 'trigger'\r\n";
            }

            sSql += " UNION ALL\r\n";
            sSql += "SELECT SchemaName AS SchemaNode, 'VIEW' AS SchemaType, ViewName AS SchemaName FROM pg_catalog.pg_views WHERE SchemaName NOT IN ('pg_catalog', 'information_schema')\r\n";
            sSql += " UNION ALL\r\n";
            sSql += "SELECT Routine_Schema AS SchemaNode, 'PROCEDURE' AS SchemaType, Routine_Name AS SchemaName FROM information_schema.routines WHERE routine_type = 'PROCEDURE'";
            ExecuteQueryToDataTable(sSql, ref dtTFVTP); //UpdateSchemaData_PostgreSQL

            //for frmSchemaSearch, 取得所有 Table + 所有 Column
            sSql = "SELECT ts.SchemaName || '.' || ts.TableName AS TableName, cs.column_name AS ColumnName, lower(cs.data_type) AS DataType\r\n" +
                   "  FROM pg_catalog.pg_tables ts, information_schema.columns cs\r\n" +
                   " WHERE ts.schemaname != 'pg_catalog'\r\n" +
                   "   AND ts.schemaname != 'information_schema'\r\n" +
                   "   AND ts.schemaname = cs.table_schema\r\n" +
                   "   AND ts.tablename = cs.table_name\r\n" +
                   " ORDER BY ts.SchemaName, ts.TableName, cs.ordinal_position;";
            ExecuteQueryToDataTable(sSql, ref dtTableColumn); //UpdateSchemaData_PostgreSQL

            //for Query Editor, 取得所有 Table + Views Name
            sSql = "";
            sSql += "SELECT Table_Schema AS SchemaNode, '[Table]' AS SchemaType, Table_Name AS SchemaName FROM information_schema.tables\r\n";
            sSql += " WHERE Table_Type = 'BASE TABLE' AND Table_Schema NOT IN ('pg_catalog', 'information_schema')\r\n";
            sSql += " UNION ALL\r\n";
            sSql += "SELECT SchemaName AS SchemaNode, '[View]' AS SchemaType, ViewName AS SchemaName FROM pg_catalog.pg_views WHERE SchemaName NOT IN ('pg_catalog', 'information_schema')";
            sSql = string.Format(sSql, sDBUser.ToUpper());
            ExecuteQueryToDataTable(sSql, ref dtTableAndViewName); //UpdateSchemaData_PostgreSQL

            //Functions
            #region Get Info of Functions
            if (sDBVersion == ">=11")
            {
                //20230127 加判 prokind：f for a normal function, p for a procedure, a for an aggregate function, or w for a window function
                sSql = "SELECT n.nspname AS SchemaName, 'Functions' AS Functions, p.proname AS FunctionName FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE p.prokind <> 'p' AND n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname <> 'trigger' AND p.prokind = 'f' ORDER BY n.nspname, proname;";
            }
            else
            {
                sSql = "SELECT n.nspname AS SchemaName, 'Functions' AS Functions, p.proname AS FunctionName FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE NOT p.proisagg AND n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname <> 'trigger' ORDER BY n.nspname, proname;";
            }

            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_PostgreSQL

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                var iFunQty = 0;
                var sPreviousFunctionName = "";

                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    row = dtSchema.NewRow();
                    row["SchemaObject"] = sDBConnectionName;
                    row["SchemaNode"] = dtTemp.Rows[iRow]["SchemaName"].ToString();

                    var dtQty = dtTemp.Select("SchemaName = '" + dtTemp.Rows[iRow]["SchemaName"] + "'");
                    row["SchemaType"] = "Functions" + sSeparator + "(" + dtQty.Length + ")";

                    var sFunctionName = dtTemp.Rows[iRow]["FunctionName"].ToString();
                    dtQty = dtTemp.Select("FunctionName = '" + sFunctionName + "'");

                    if (sPreviousFunctionName != sFunctionName)
                    {
                        iFunQty = -1; //連續遇到多形的函數
                    }

                    bool bOnlyOneRecord;

                    if (dtQty.Length == 1)
                    {
                        iFunQty = 0;
                        bOnlyOneRecord = true;
                    }
                    else
                    {
                        iFunQty++;
                        bOnlyOneRecord = false;
                    }

                    var sScripts = "";
                    string sSqlFunction;

                    if (sDBVersion == ">=11")
                    {
                        sSqlFunction = "SELECT PG_GET_FUNCTIONDEF(p.oid) AS Scripts FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE p.prokind <> 'p' and n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname <> 'trigger' AND n.nspname = '" + dtTemp.Rows[iRow]["SchemaName"] + "' AND p.proname = '" + dtTemp.Rows[iRow]["FunctionName"] + "'";
                    }
                    else
                    {
                        sSqlFunction = "SELECT PG_GET_FUNCTIONDEF(p.oid) AS Scripts FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE NOT p.proisagg and n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname <> 'trigger' AND n.nspname = '" + dtTemp.Rows[iRow]["SchemaName"] + "' AND p.proname = '" + dtTemp.Rows[iRow]["FunctionName"] + "'";
                    }

                    var dtTempFunction = new DataTable();
                    ExecuteQueryToDataTable(sSqlFunction, ref dtTempFunction); //UpdateSchemaData_PostgreSQL

                    if (dtTempFunction != null && dtTempFunction.Rows.Count > 0)
                    {
                        sScripts = dtTempFunction.Rows[iFunQty]["Scripts"].ToString();
                        sScripts = sScripts.Substring(sScripts.IndexOf("(", StringComparison.Ordinal), sScripts.IndexOf(")", StringComparison.Ordinal) - sScripts.IndexOf("(", StringComparison.Ordinal) + 1);
                    }

                    row["SchemaName"] = sFunctionName + sScripts;

                    //PostgreSQL 的 Function 會有「同名稱但不同參數」的情況
                    if (sTempDistinct.IndexOf("," + sFunctionName + ",", StringComparison.Ordinal) == -1)
                    {
                        sTempDistinct += sFunctionName + ",";

                        if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedFunctions && !string.IsNullOrEmpty(sDBConnectionDatabase))
                        {
                            AddACData(dtTemp.Rows[iRow]["FunctionName"].ToString(), "U-Functions");
                        }
                    }

                    dtSchema.Rows.Add(row);

                    if (bOnlyOneRecord) //每一個單筆，恢復為 -1
                    {
                        iFunQty = -1; //遇到多筆的時候，才會從 0 開始！
                    }

                    sPreviousFunctionName = sFunctionName;
                    Application.DoEvents();
                }

                MyLibrary.sKeywordsUserDefinedFunctions += sTempDistinct.Replace(",", " ").Trim() + " ";
            }
            #endregion

            sTempDistinct = ",";

            //Tables
            #region Get Info of Tables
            sSql = "SELECT SchemaName, Count(*) AS TableQty FROM (\r\n" +
                   "SELECT n.nspname AS SchemaName, c.relname AS TableName, c.reltuples AS RowCount\r\n" +
                   "  FROM pg_class c LEFT JOIN pg_namespace n ON (n.oid = c.relnamespace)\r\n" +
                   " WHERE n.nspname NOT IN ('pg_catalog', 'information_schema') AND c.relkind='r') ss GROUP BY SchemaName;";
            var dtTableQty = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTableQty); //UpdateSchemaData_PostgreSQL

            //20220515 以下 SQL 會取到 -1 的數量
            sSql = "SELECT n.nspname AS SchemaName, c.relname AS TableName, c.reltuples AS RowCount\r\n" +
                   "  FROM pg_class c LEFT JOIN pg_namespace n ON (n.oid = c.relnamespace)\r\n" +
                   " WHERE n.nspname NOT IN ('pg_catalog', 'information_schema') AND c.relkind='r'\r\n" +
                   " ORDER BY n.nspname, c.relname;";
            //20220714 以下 SQL 在不同版本有可能引發錯誤，故暫不使用
            //sSql = "WITH tbl AS\r\n" +
            //       "  (SELECT Table_Schema,\r\n" +
            //       "          Table_Name\r\n" +
            //       "   FROM information_schema.Tables\r\n" +
            //       "   WHERE Table_Name NOT LIKE 'pg_%' AND Table_Schema NOT IN ('pg_catalog', 'information_schema'))\r\n" +
            //       "SELECT Table_Schema AS SchemaName,\r\n" +
            //       "       Table_Name AS TableName,\r\n" +
            //       "       (XPATH('/row/c/text()', QUERY_TO_XML(FORMAT('SELECT COUNT(*) AS c FROM %I.%I', Table_Schema, Table_Name), FALSE, TRUE, '')))[1]::TEXT::INT AS RowCount\r\n" +
            //       "FROM tbl\r\n" +
            //       "ORDER BY Table_Schema, Table_Name;";
            var dtRowCount = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtRowCount); //UpdateSchemaData_PostgreSQL

            if (bShowColumnInfo)
            {
                sSql = "SELECT ts.SchemaName, 'Tables' AS SchemaType, ts.TableName, cs.column_name AS ColumnName, cs.ordinal_position AS ColumnID, substr(cs.is_nullable, 1, 1) AS Nullable, cs.column_default AS DefaultValue, lower(cs.data_type) AS DataType, cs.character_maximum_length AS DataLength, cs.numeric_precision || ',' || cs.numeric_scale AS Scale\r\n" +
                       "  FROM pg_catalog.pg_tables ts, information_schema.columns cs\r\n" +
                       " WHERE ts.schemaname != 'pg_catalog'\r\n" +
                       "   AND ts.schemaname != 'information_schema'\r\n" +
                       "   AND ts.schemaname = cs.table_schema\r\n" +
                       "   AND ts.tablename = cs.table_name\r\n" +
                       " ORDER BY ts.SchemaName, ts.TableName, " + (bSortByColumnName ? "cs.column_name;" : "cs.ordinal_position;");
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_PostgreSQL

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        var dtQty = dtTableQty.Select("SchemaName = '" + dtTemp.Rows[iRow]["SchemaName"] + "'");
                        sQty = (dtQty.Length == 0 || string.IsNullOrEmpty(dtQty[0]["TableQty"].ToString())) ? "" : sSeparator + "(" + dtQty[0]["TableQty"] + ")";

                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = dtTemp.Rows[iRow]["SchemaName"].ToString();
                        row["SchemaType"] = "Tables" + sQty;

                        var dtRowQty = dtRowCount.Select("SchemaName = '" + dtTemp.Rows[iRow]["SchemaName"] + "' AND TableName = '" + dtTemp.Rows[iRow]["TableName"] + "'");
                        double.TryParse(dtRowQty.Length == 0 ? "0" : dtRowQty[0]["RowCount"].ToString(), out var dRowCount);
                        var sRowQty = sSeparator + "(" + dRowCount.ToString("###,##0") + ")";

                        if (sRowQty == sSeparator + "(-1)")
                        {
                            sRowQty = sSeparator + "(0)";
                        }

                        row["SchemaName"] = dtTemp.Rows[iRow]["TableName"] + sRowQty;

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["TableName"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["TableName"] + ",";

                            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedTables && !string.IsNullOrEmpty(sDBConnectionDatabase))
                            {
                                AddACData(dtTemp.Rows[iRow]["TableName"].ToString(), "Tables");
                            }
                        }

                        var sColumnInfo = dtTemp.Rows[iRow]["ColumnName"].ToString();
                        string sDataType;

                        switch (dtTemp.Rows[iRow]["DataType"].ToString())
                        {
                            case "character":
                            case "character varying":
                                sDataType = dtTemp.Rows[iRow]["DataType"] + "(" + dtTemp.Rows[iRow]["DataLength"] + ")";
                                break;
                            case "bigint":
                                sDataType = dtTemp.Rows[iRow]["DataType"] + "(64)";
                                break;
                            case "integer":
                                sDataType = dtTemp.Rows[iRow]["DataType"] + "(32)";
                                break;
                            case "smallint":
                                sDataType = dtTemp.Rows[iRow]["DataType"] + "(16)";
                                break;
                            case "numeric":
                                if (string.IsNullOrEmpty(dtTemp.Rows[iRow]["Scale"].ToString()))
                                {
                                    sDataType = dtTemp.Rows[iRow]["DataType"].ToString();
                                }
                                else
                                {
                                    sDataType = dtTemp.Rows[iRow]["DataType"] + "(" + dtTemp.Rows[iRow]["Scale"] + ")";
                                }

                                break;
                            default:
                                sDataType = dtTemp.Rows[iRow]["DataType"].ToString();
                                break;
                        }

                        row["ColumnInfo"] = sColumnInfo + ", " + sDataType;
                        Application.DoEvents();
                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedTables += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
            }
            else
            {
                if (dtRowCount != null && dtRowCount.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtRowCount.Rows.Count; iRow++)
                    {
                        var dtQty = dtTableQty.Select("SchemaName = '" + dtRowCount.Rows[iRow]["SchemaName"] + "'");
                        sQty = dtQty.Length == 0 || string.IsNullOrEmpty(dtQty[0]["TableQty"].ToString()) ? "" : sSeparator + "(" + dtQty[0]["TableQty"] + ")";

                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = dtRowCount.Rows[iRow]["SchemaName"].ToString();
                        row["SchemaType"] = "Tables" + sQty;
                        double.TryParse(dtRowCount.Rows[iRow]["RowCount"].ToString(), out var dRowCount);

                        var sRowQty = sSeparator + "(" + dRowCount.ToString("###,##0") + ")";

                        if (sRowQty == sSeparator + "(-1)")
                        {
                            sRowQty = sSeparator + "(0)";
                        }

                        row["SchemaName"] = dtRowCount.Rows[iRow]["TableName"] + sRowQty;

                        if (sTempDistinct.IndexOf("," + dtRowCount.Rows[iRow]["TableName"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtRowCount.Rows[iRow]["TableName"] + ",";

                            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedTables && !string.IsNullOrEmpty(sDBConnectionDatabase))
                            {
                                AddACData(dtRowCount.Rows[iRow]["TableName"].ToString(), "Tables");
                            }
                        }

                        Application.DoEvents();
                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedTables += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
            }
            #endregion

            sTempDistinct = ",";

            //Triggers
            #region Get Info of Triggers

            if (sDBVersion == ">=11")
            {
                sSql = "SELECT n.nspname AS SchemaName, 'Triggers' AS Triggers, p.proname AS TriggerName FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE p.prokind <> 'p' AND n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname = 'trigger' ORDER BY n.nspname, proname";
            }
            else
            {
                sSql = "SELECT n.nspname AS SchemaName, 'Triggers' AS Triggers, p.proname AS TriggerName FROM pg_proc p JOIN pg_type t ON p.prorettype = t.oid LEFT OUTER JOIN pg_description d ON p.oid = d.objoid LEFT OUTER JOIN pg_namespace n ON n.oid = p.pronamespace WHERE NOT p.proisagg AND n.nspname NOT IN ('pg_catalog', 'information_schema') AND t.typname = 'trigger' ORDER BY n.nspname, proname";
            }

            ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_PostgreSQL

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    row = dtSchema.NewRow();
                    row["SchemaObject"] = sDBConnectionName;
                    row["SchemaNode"] = dtTemp.Rows[iRow]["SchemaName"].ToString();

                    var dtQty = dtTemp.Select("SchemaName = '" + dtTemp.Rows[iRow]["SchemaName"] + "'");

                    row["SchemaType"] = "Triggers" + sSeparator + "(" + dtQty.Length + ")";
                    row["SchemaName"] = dtTemp.Rows[iRow]["TriggerName"].ToString();

                    if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["TriggerName"] + ",", StringComparison.Ordinal) == -1)
                    {
                        sTempDistinct += dtTemp.Rows[iRow]["TriggerName"] + ",";

                        if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedTriggers && !string.IsNullOrEmpty(sDBConnectionDatabase))
                        {
                            AddACData(dtTemp.Rows[iRow]["TriggerName"].ToString(), "Triggers");
                        }
                    }

                    Application.DoEvents();
                    dtSchema.Rows.Add(row);
                }

                MyLibrary.sKeywordsUserDefinedTriggers += sTempDistinct.Replace(",", " ").Trim() + " ";
            }
            #endregion

            sTempDistinct = ",";

            //Views
            #region Get info of Views
            sSql = "SELECT SchemaName, 'Views' AS Views, ViewName FROM pg_catalog.pg_views WHERE SchemaName NOT IN ('pg_catalog', 'information_schema') ORDER BY SchemaName, ViewName";
            ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_PostgreSQL

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    var dtQty = dtTemp.Select("SchemaName = '" + dtTemp.Rows[iRow]["SchemaName"] + "'");
                    sQty = (dtQty.Length == 0) ? "" : sSeparator + "(" + dtQty.Length + ")";

                    row = dtSchema.NewRow();
                    row["SchemaObject"] = sDBConnectionName;
                    row["SchemaNode"] = dtTemp.Rows[iRow]["SchemaName"].ToString();
                    row["SchemaType"] = "Views" + sQty;
                    row["SchemaName"] = dtTemp.Rows[iRow]["ViewName"].ToString();

                    if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["ViewName"] + ",", StringComparison.Ordinal) == -1)
                    {
                        sTempDistinct += dtTemp.Rows[iRow]["ViewName"] + ",";

                        if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedViews && !string.IsNullOrEmpty(sDBConnectionDatabase))
                        {
                            AddACData(dtTemp.Rows[iRow]["ViewName"].ToString(), "Views");
                        }
                    }

                    Application.DoEvents();
                    dtSchema.Rows.Add(row);
                }

                MyLibrary.sKeywordsUserDefinedViews += sTempDistinct.Replace(",", " ").Trim() + " ";
            }
            #endregion

            #region 處理 Auto Complete 資訊
            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACBuiltInKeywords && !string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                var part1 = MyLibrary.sKeywordsBuiltInKeywords.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in part1)
                {
                    AddACData(t, "B-Keywords");
                }
            }

            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACBuiltInFunctions && !string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                var part1 = MyLibrary.sKeywordsBuiltInFunctions.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in part1)
                {
                    AddACData(t, "B-Functions");
                }
            }

            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedKeywords && !string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                var part1 = MyLibrary.sKeywordsUserDefinedKeywords.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in part1)
                {
                    AddACData(t, "U-Keywords");
                }
            }
            #endregion

            if (bUpdate)
            {
                UpdateSchemaData(c1Grid, true); //UpdateSchemaData_PostgreSQL
            }
        }

        public static void GetSchemaInfo_PostgreSQL(out DataTable dtTable)
        {
            var sSql = "";
            sSql += "SELECT DISTINCT SchemaNode FROM (\r\n";
            sSql += "SELECT Table_Schema AS SchemaNode FROM information_schema.tables\r\n";
            sSql += " WHERE Table_Type = 'BASE TABLE' AND Table_Schema NOT IN ('pg_catalog', 'information_schema')\r\n";
            sSql += " UNION ALL\r\n";
            sSql += "SELECT SchemaName AS SchemaNode FROM pg_catalog.pg_views WHERE SchemaName NOT IN ('pg_catalog', 'information_schema')) ss\r\n";
            sSql += " ORDER BY SchemaNode";
            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp); //GetSchemaInfo_PostgreSQL
            dtTable = dtTemp.Copy();
        }

        public static void GetTableInfo_PostgreSQL(out DataTable dtTable, string sSchemaNode = "")
        {
            var sSql = "SELECT Table_Name AS SchemaName FROM information_schema.tables\r\n";
            sSql += " WHERE Table_Type = 'BASE TABLE' AND Table_Schema NOT IN ('pg_catalog', 'information_schema'){0} ORDER BY Table_Name";
            sSql = string.Format(sSql, string.IsNullOrEmpty(sSchemaNode) ? "" : " AND Table_Schema = '" + sSchemaNode + "'");

            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp); //GetTableInfo_PostgreSQL
            dtTable = dtTemp.Copy();
        }

        public static void GetViewInfo_PostgreSQL(out DataTable dtTable, string sSchemaNode = "")
        {
            var sSql = "SELECT ViewName AS SchemaName FROM pg_catalog.pg_views WHERE SchemaName NOT IN ('pg_catalog', 'information_schema') ORDER BY ViewName";
            sSql = string.Format(sSql, string.IsNullOrEmpty(sSchemaNode) ? "" : " AND Table_Schema = '" + sSchemaNode + "'");
            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp); //GetViewInfo_PostgreSQL
            dtTable = dtTemp.Copy();
        }

        private static void AddACData(string sObjectName, string sObjectSource)
        {
            var row = dtAC4All.NewRow();
            row["ObjectName"] = sObjectName;
            row["ObjectSource"] = "[" + sObjectSource + "]";
            dtAC4All.Rows.Add(row);
        }

        public static void UpdateSchemaData_SQLServer(C1TrueDBGrid c1Grid, bool bSchemaBrowserForm = true)
        {
            string sSql;
            var dtTemp = new DataTable();
            var sSchemaName = sDBConnectionDatabase;

            dtAC4All = new DataTable();
            dtAC4All.Columns.Add("ObjectName");
            dtAC4All.Columns.Add("ObjectSource");

            CreateSchemaTable();

            //is_ms_shipped = 1 (indicates this object was shipped or created by Microsoft)
            //is_ms_shipped = 0 (indicates this object was created by user)

            if (string.IsNullOrEmpty(sSchemaName)) //沒有指定，則撈取全部
            {
                sSql = "SELECT Name FROM master.sys.databases{0} ORDER BY Name;";
                sSql = string.Format(sSql, (bDBExcludeNativeDatabase ? " WHERE Name NOT IN ('master', 'model', 'msdb', 'tempdb')" : ""));
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLServer

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        sSchemaName += dtTemp.Rows[iRow]["Name"] + sSeparator;
                    }
                }
            }

            var parts = sSchemaName.Split(new[] { sSeparator }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var t in parts)
            {
                var sTempDistinct = ",";

                //Functions
                #region Get info of Functions
                sSql = "SELECT SCHEMA_NAME(Schema_ID) AS Schema_Dbo, o.* FROM {0}.sys.all_objects o WHERE Type = 'FN'{1} ORDER BY Name;";
                sSql = string.Format(sSql, t, bDBExcludeNativeDatabase ? " AND Is_Ms_Shipped <> 1" : "");
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLServer

                DataRow row;

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaDbo"] = dtTemp.Rows[iRow]["Schema_Dbo"].ToString();
                        row["SchemaType"] = "Functions" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                        row["SchemaName"] = dtTemp.Rows[iRow]["Schema_Dbo"] + "." + dtTemp.Rows[iRow]["Name"];
                        row["ObjectID"] = dtTemp.Rows[iRow]["Object_ID"].ToString();

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";

                            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedFunctions && !string.IsNullOrEmpty(sDBConnectionDatabase))
                            {
                                AddACData(dtTemp.Rows[iRow]["Name"].ToString(), "U-Functions");
                            }
                        }

                        Application.DoEvents();
                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedFunctions += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                sTempDistinct = ",";

                //Tables //SELECT 'master' As DB, SCHEMA_NAME(Schema_ID) AS Schema_Dbo, Name AS SchemaName FROM master.sys.objects o WHERE Type = 'U' AND Is_Ms_Shipped <> 1 ORDER BY Name;
                #region Get info of Tables
                sSql = "SELECT o.name AS TableName, i.Rows, o.* FROM {0}.sys.sysobjects o INNER JOIN {0}.sys.sysindexes i ON o.ID = i.ID WHERE i.indid <= 1 AND xtype = 'U';";
                sSql = string.Format(sSql, t);
                var dtRowCount = new DataTable();
                ExecuteQueryToDataTable(sSql, ref dtRowCount); //UpdateSchemaData_SQLServer

                sSql = "SELECT SCHEMA_NAME(Schema_ID) AS Schema_Dbo, o.* FROM {0}.sys.objects o WHERE Type = 'U'{1} ORDER BY Name;";
                sSql = string.Format(sSql, t, (bDBExcludeNativeDatabase ? " AND Is_Ms_Shipped <> 1" : ""));
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLServer

                sSql = "SELECT * FROM {0}.INFORMATION_SCHEMA.COLUMNS WHERE Table_Catalog = '{0}' ORDER BY Table_Name, " + (bSortByColumnName ? "Column_Name;" : "Ordinal_Position;");
                sSql = string.Format(sSql, t);
                var dtColumnInfo = new DataTable();

                if (bShowColumnInfo)
                {
                    ExecuteQueryToDataTable(sSql, ref dtColumnInfo); //UpdateSchemaData_SQLServer
                }

                var ss = t;

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        var sRowCount = "";

                        if (dtRowCount != null)
                        {
                            var dtQty = dtRowCount.Select("TableName = '" + dtTemp.Rows[iRow]["Name"].ToString().Replace("'", "''") + "'");
                            double.TryParse(dtQty.Length == 0 ? "0" : dtQty[0]["Rows"].ToString(), out var dRowCount);
                            sRowCount = sSeparator + "(" + dRowCount.ToString("###,##0") + ")";
                        }

                        if (bShowColumnInfo)
                        {
                            var drColumnInfo = dtColumnInfo.Select("Table_Name = '" + dtTemp.Rows[iRow]["Name"] + "'");

                            foreach (var t1 in drColumnInfo)
                            {
                                row = dtSchema.NewRow();
                                row["SchemaObject"] = sDBConnectionName;
                                row["SchemaNode"] = t;
                                row["SchemaDbo"] = dtTemp.Rows[iRow]["Schema_Dbo"].ToString();
                                row["SchemaType"] = "Tables" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                                row["SchemaName"] = dtTemp.Rows[iRow]["Schema_Dbo"] + "." + dtTemp.Rows[iRow]["Name"] + sRowCount;
                                row["ObjectID"] = dtTemp.Rows[iRow]["Object_ID"].ToString();

                                var dtDateTime = Convert.ToDateTime(dtTemp.Rows[iRow]["Create_Date"]);
                                row["CreateDate"] = dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                                dtDateTime = Convert.ToDateTime(dtTemp.Rows[iRow]["Modify_Date"]);
                                row["ModifyDate"] = dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss");

                                if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                                {
                                    sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";

                                    if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedTables && !string.IsNullOrEmpty(sDBConnectionDatabase))
                                    {
                                        AddACData(dtTemp.Rows[iRow]["Name"].ToString(), "Tables");
                                    }
                                }

                                var sColumnInfo = t1["Column_Name"].ToString();
                                string sDataType;

                                switch (t1["Data_Type"].ToString())
                                {
                                    case "char":
                                    case "nchar":
                                    case "varchar":
                                    case "nvarchar":
                                        sDataType = t1["Data_Type"] + "(" + t1["Character_Maximum_Length"] + ")";
                                        break;
                                    default:
                                        sDataType = t1["Data_Type"].ToString();
                                        break;
                                }

                                var columns = dtSchema.Columns;

                                if (columns.Contains("ColumnInfo"))
                                {
                                    row["ColumnInfo"] = sColumnInfo + ", " + sDataType;
                                }
                                else
                                {
                                    row["SchemaName"] = sColumnInfo + ", " + sDataType;
                                }

                                dtSchema.Rows.Add(row);
                            }
                        }
                        else
                        {
                            row = dtSchema.NewRow();
                            row["SchemaObject"] = sDBConnectionName;
                            row["SchemaNode"] = t;
                            row["SchemaDbo"] = dtTemp.Rows[iRow]["Schema_Dbo"].ToString();
                            row["SchemaType"] = "Tables" + sSeparator + "(" + dtTemp.Rows.Count + ")";

                            var columns = dtSchema.Columns;

                            if (columns.Contains("Schema_Browser"))
                            {
                                row["Schema_Browser"] = dtTemp.Rows[iRow]["Schema_Dbo"] + "." + dtTemp.Rows[iRow]["Name"] + sRowCount;
                            }
                            else
                            {
                                row["SchemaName"] = dtTemp.Rows[iRow]["Schema_Dbo"] + "." + dtTemp.Rows[iRow]["Name"] + sRowCount;
                            }

                            row["ObjectID"] = dtTemp.Rows[iRow]["Object_ID"].ToString();

                            var dtDateTime = Convert.ToDateTime(dtTemp.Rows[iRow]["Create_Date"]);
                            row["CreateDate"] = dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                            dtDateTime = Convert.ToDateTime(dtTemp.Rows[iRow]["Modify_Date"]);
                            row["ModifyDate"] = dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss");

                            if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                            {
                                sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";

                                if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedTables && !string.IsNullOrEmpty(sDBConnectionDatabase))
                                {
                                    AddACData(dtTemp.Rows[iRow]["Name"].ToString(), "Tables");
                                }
                            }

                            dtSchema.Rows.Add(row);
                        }

                        Application.DoEvents();
                    }

                    MyLibrary.sKeywordsUserDefinedTables += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                sTempDistinct = ",";

                //Triggers
                #region Get info of Triggers
                sSql = "SELECT SCHEMA_NAME(Schema_ID) AS Schema_Dbo, o.* FROM {0}.sys.all_objects o WHERE Type = 'TR'{1} ORDER BY Name;";
                sSql = string.Format(sSql, t, (bDBExcludeNativeDatabase ? " AND Is_Ms_Shipped <> 1" : ""));
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLServer

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaDbo"] = dtTemp.Rows[iRow]["Schema_Dbo"].ToString();
                        row["SchemaType"] = "Triggers" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                        row["SchemaName"] = dtTemp.Rows[iRow]["Schema_Dbo"] + "." + dtTemp.Rows[iRow]["Name"];
                        row["ObjectID"] = dtTemp.Rows[iRow]["Object_ID"].ToString();

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";

                            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedTriggers && !string.IsNullOrEmpty(sDBConnectionDatabase))
                            {
                                AddACData(dtTemp.Rows[iRow]["Name"].ToString(), "Triggers");
                            }
                        }

                        Application.DoEvents();
                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedTriggers += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                sTempDistinct = ",";

                //Views
                #region Get info of Views
                sSql = "SELECT SCHEMA_NAME(Schema_ID) AS Schema_Dbo, o.* FROM {0}.sys.all_objects o WHERE Type = 'V'{1} ORDER BY Name;";
                sSql = string.Format(sSql, t, (bDBExcludeNativeDatabase ? " AND Is_Ms_Shipped <> 1" : ""));
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLServer

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaDbo"] = dtTemp.Rows[iRow]["Schema_Dbo"].ToString();
                        row["SchemaType"] = "Views" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                        row["SchemaName"] = dtTemp.Rows[iRow]["Schema_Dbo"] + "." + dtTemp.Rows[iRow]["Name"];
                        row["ObjectID"] = dtTemp.Rows[iRow]["Object_ID"].ToString();

                        var dtDateTime = Convert.ToDateTime(dtTemp.Rows[iRow]["Create_Date"]);
                        row["CreateDate"] = dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss");
                        dtDateTime = Convert.ToDateTime(dtTemp.Rows[iRow]["Modify_Date"]);
                        row["ModifyDate"] = dtDateTime.ToString(MyLibrary.sDateFormat + " HH:mm:ss");

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";

                            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedViews && !string.IsNullOrEmpty(sDBConnectionDatabase))
                            {
                                AddACData(dtTemp.Rows[iRow]["Name"].ToString(), "Views");
                            }
                        }

                        Application.DoEvents();
                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedViews += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                Application.DoEvents();

                //Procedures
                #region Get info of Procedures
                if (bSchemaBrowserForm)
                {
                    sSql = "SELECT SCHEMA_NAME(Schema_ID) AS Schema_Dbo, o.* FROM {0}.sys.all_objects o WHERE Type = 'P'{1} ORDER BY Name;";
                    sSql = string.Format(sSql, t, (bDBExcludeNativeDatabase ? " AND Is_Ms_Shipped <> 1" : ""));
                    ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLServer

                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                        {
                            row = dtSchema.NewRow();
                            row["SchemaObject"] = sDBConnectionName;
                            row["SchemaNode"] = t;
                            row["SchemaDbo"] = dtTemp.Rows[iRow]["Schema_Dbo"].ToString();
                            row["SchemaType"] = "Procedures" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                            row["SchemaName"] = dtTemp.Rows[iRow]["Schema_Dbo"] + "." + dtTemp.Rows[iRow]["Name"];
                            row["ObjectID"] = dtTemp.Rows[iRow]["Object_ID"].ToString();
                            dtSchema.Rows.Add(row);
                        }
                    }
                }
                #endregion

                //Indices
                #region Get info of Indices
                if (bSchemaBrowserForm)
                {
                    sSql = "";
                    sSql += "SELECT SCHEMA_NAME(t.Schema_ID) AS Schema_Dbo, i.Name, t.Name AS tblname, u.Name AS uname, i.Object_ID, g.Name AS GroupName, i.Is_Padded, s.No_Recompute, i.Ignore_Dup_Key, i.Allow_Row_Locks, i.Allow_Page_Locks, t.Create_Date, t.Modify_Date\r\n";
                    sSql += "FROM {0}.sys.indexes i\r\n";
                    sSql += "LEFT OUTER JOIN {0}.sys.data_spaces g ON i.Data_Space_ID = g.Data_Space_ID\r\n";
                    sSql += "INNER JOIN {0}.sys.objects t ON t.Object_ID = i.Object_ID\r\n";
                    sSql += "INNER JOIN {0}.sys.schemas u ON t.Schema_ID = u.Schema_ID\r\n";
                    sSql += "LEFT OUTER JOIN {0}.sys.stats s ON s.Object_ID = i.Object_ID AND s.Stats_ID = i.Index_ID\r\n";
                    sSql += "WHERE i.Index_ID > 0 AND t.Type = 'U' AND u.Name = 'dbo'{1} ORDER BY i.Name, i.Index_ID;";
                    sSql = string.Format(sSql, t, (bDBExcludeNativeDatabase ? " AND t.Is_Ms_Shipped <> 1" : ""));
                    ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLServer

                    if (dtTemp == null || dtTemp.Rows.Count <= 0)
                    {
                        continue;
                    }

                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaDbo"] = dtTemp.Rows[iRow]["Schema_Dbo"].ToString();
                        row["SchemaType"] = "Indices" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                        row["SchemaName"] = dtTemp.Rows[iRow]["Schema_Dbo"] + "." + dtTemp.Rows[iRow]["Name"] + " on " + dtTemp.Rows[iRow]["tblname"];
                        row["ObjectID"] = dtTemp.Rows[iRow]["Object_ID"].ToString();
                        dtSchema.Rows.Add(row);
                    }
                }
                #endregion
            }

            #region 處理 Auto Complete 資訊
            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACBuiltInKeywords && !string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                var part1 = MyLibrary.sKeywordsBuiltInKeywords.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in part1)
                {
                    AddACData(t, "B-Keywords");
                }
            }

            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACBuiltInFunctions && !string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                var part1 = MyLibrary.sKeywordsBuiltInFunctions.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in part1)
                {
                    AddACData(t, "B-Functions");
                }
            }

            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedKeywords && !string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                var part1 = MyLibrary.sKeywordsUserDefinedKeywords.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in part1)
                {
                    AddACData(t, "U-Keywords");
                }
            }
            #endregion

            UpdateSchemaData(c1Grid, true); //UpdateSchemaData_SQLServer
            UpdateDatabaseInfo4AC_SQLServer();

            if (!string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                UpdateTableAndViewInfo4AC_SQLServer(sDBConnectionDatabase);
                dtTableAndViewName.Merge(dtDatabaseName);
            }
            else
            {
                dtTableAndViewName = dtDatabaseName.Copy();
            }

            //將 database name 納入 AC
            if (MyLibrary.bEnableAutoComplete && !string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                for (var iRow = 0; iRow < dtDatabaseName.Rows.Count; iRow++)
                {
                    AddACData(dtDatabaseName.Rows[iRow]["DB"].ToString(), "Database");
                }
            }
        }

        public static void UpdateDatabaseInfo4AC_SQLServer() //一開始沒有指定資料庫，只要取得所有的資料庫名稱 (USE 指令會用到)
        {
            dtDatabaseName = new DataTable();
            dtDatabaseName.Columns.Add("DB"); //Database
            dtDatabaseName.Columns.Add("DBAndNode"); //Database+Node
            dtDatabaseName.Columns.Add("SchemaNode");
            dtDatabaseName.Columns.Add("SchemaName");
            dtDatabaseName.Columns.Add("SchemaType");
            dtDatabaseName.Columns.Add("Memo");

            var dtTemp = new DataTable();
            var sSql = "SELECT Name FROM master.sys.databases{0} ORDER BY Name;";
            sSql = string.Format(sSql, (bDBExcludeNativeDatabase ? " WHERE Name NOT IN ('master', 'model', 'msdb', 'tempdb')" : ""));
            ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateDatabaseInfo4AC_SQLServer

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    var row = dtDatabaseName.NewRow();
                    row["DB"] = dtTemp.Rows[iRow]["Name"].ToString(); //方便比對，此處要轉大寫
                    row["Memo"] = "DB";
                    dtDatabaseName.Rows.Add(row);
                }
            }
        }

        public static void UpdateTableAndViewInfo4AC_SQLServer(string sSchemaName) //切換資料庫時，也要一併更新 Table & View Info
        {
            //for Query Editor AC, 取得指定資料庫的所有 Table + View Name
            dtTableAndViewName = new DataTable();
            dtTableAndViewName.Columns.Add("DB"); //Database
            dtTableAndViewName.Columns.Add("DBAndNode"); //Database+Node
            dtTableAndViewName.Columns.Add("SchemaNode");
            dtTableAndViewName.Columns.Add("SchemaName");
            dtTableAndViewName.Columns.Add("SchemaType");
            dtTableAndViewName.Columns.Add("Memo");

            var sSql = "";
            sSql += "SELECT '{0}' AS DB, SCHEMA_NAME(Schema_ID) AS SchemaNode, o.Name AS SchemaName, '[Table]' AS SchemaType FROM {0}.sys.objects o WHERE Type = 'U' AND Is_Ms_Shipped <> 1\r\n";
            sSql += "UNION ALL\r\n";
            sSql += "SELECT '{0}' AS DB, SCHEMA_NAME(Schema_ID) AS SchemaNode, o.Name AS SchemaName, '[View]' AS SchemaType FROM {0}.sys.all_objects o WHERE Type = 'V' AND Is_Ms_Shipped <> 1";
            sSql = string.Format(sSql, sSchemaName);

            if (bDBExcludeNativeDatabase == false)
            {
                sSql = sSql.Replace(" AND Is_Ms_Shipped <> 1", "");
            }

            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLServer

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                for (var i = 0; i < dtTemp.Rows.Count; i++)
                {
                    var row = dtTableAndViewName.NewRow();
                    row["DB"] = dtTemp.Rows[i]["DB"].ToString(); //方便比對，此處要轉大寫
                    row["DBAndNode"] = dtTemp.Rows[i]["DB"] + "." + dtTemp.Rows[i]["SchemaNode"];
                    row["SchemaNode"] = dtTemp.Rows[i]["SchemaNode"].ToString().ToUpper();
                    row["SchemaName"] = dtTemp.Rows[i]["SchemaName"].ToString();
                    row["SchemaType"] = dtTemp.Rows[i]["SchemaType"].ToString();
                    dtTableAndViewName.Rows.Add(row);
                }
            }
        }

        public static void GetTableInfo_SQLServer(string sSchemaName, out DataTable dtTable) //切換資料庫時，也要一併更新 Table & View Info
        {
            var dtTemp = new DataTable();
            var sSql = "SELECT o.Name AS SchemaName FROM {0}.sys.objects o WHERE Type = 'U' AND Is_Ms_Shipped <> 1 ORDER BY o.Name;";
            sSql = string.Format(sSql, sSchemaName);

            if (bDBExcludeNativeDatabase == false)
            {
                sSql = sSql.Replace(" AND Is_Ms_Shipped <> 1", "");
            }

            ExecuteQueryToDataTable(sSql, ref dtTemp); //GetTableInfo_SQLServer
            dtTable = dtTemp.Copy();
        }

        public static void GetViewInfo_SQLServer(string sSchemaName, out DataTable dtTable) //切換資料庫時，也要一併更新 Table & View Info
        {
            var dtTemp = new DataTable();
            var sSql = "SELECT o.Name AS SchemaName FROM {0}.sys.all_objects o WHERE Type = 'V' AND Is_Ms_Shipped <> 1 ORDER BY o.Name;";
            sSql = string.Format(sSql, sSchemaName);

            if (bDBExcludeNativeDatabase == false)
            {
                sSql = sSql.Replace(" AND Is_Ms_Shipped <> 1", "");
            }

            ExecuteQueryToDataTable(sSql, ref dtTemp); //GetViewInfo_SQLServer
            dtTable = dtTemp.Copy();
        }

        public static void UpdateSchemaData_MySQL(C1TrueDBGrid c1Grid)
        {
            string sSql;
            var dtTemp = new DataTable();

            dtAC4All = new DataTable();
            dtAC4All.Columns.Add("ObjectName");
            dtAC4All.Columns.Add("ObjectSource");

            CreateSchemaTable();

            var sSchemaName = sDBConnectionDatabase;

            if (string.IsNullOrEmpty(sSchemaName)) //沒有指定，則撈取全部
            {
                sSql = "SELECT Schema_Name AS Name FROM information_schema.schemata ORDER BY Schema_Name;";
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_MySQL

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        sSchemaName += dtTemp.Rows[iRow]["Name"] + "`";
                    }
                }

                sSql = "SELECT Table_Schema, Table_Name, Column_Name, Data_Type, Character_Maximum_Length, Numeric_Precision, Numeric_Scale, Column_Type\r\n" +
                       "  FROM information_schema.Columns\r\n" +
                       " ORDER BY Table_Name, " + (bSortByColumnName ? "Column_Name;" : "Ordinal_Position;");
            }
            else
            {
                sSql = "SELECT Table_Schema, Table_Name, Column_Name, Data_Type, Character_Maximum_Length, Numeric_Precision, Numeric_Scale, Column_Type\r\n" +
                       "  FROM information_schema.Columns\r\n" +
                       " WHERE Table_Schema = '{0}'\r\n" +
                       " ORDER BY Table_Name, " + (bSortByColumnName ? "Column_Name;" : "Ordinal_Position;");
                sSql = string.Format(sSql, sSchemaName);
            }

            var dtColumnInfo = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtColumnInfo); //UpdateSchemaData_MySQL

            var parts = sSchemaName.Split(new[] { "`" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var t in parts)
            {
                var sTempDistinct = ",";

                //Functions
                #region Get info of Functions
                sSql = "";
                sSql += "SELECT Routine_Schema AS Database_Name,\r\n";
                sSql += "       Routine_Name AS Name,\r\n";
                sSql += "       Routine_Type AS Type,\r\n";
                sSql += "       Data_Type AS Return_Type,\r\n";
                sSql += "       Routine_Definition AS Definition\r\n";
                sSql += "  FROM information_schema.routines\r\n";
                sSql += " WHERE Routine_Schema = '{0}' AND Routine_Type = 'FUNCTION'\r\n";
                sSql += " ORDER BY Routine_Schema, Routine_Name;";
                sSql = string.Format(sSql, t);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_MySQL

                DataRow row;

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaType"] = "Functions" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                        row["SchemaName"] = dtTemp.Rows[iRow]["Name"].ToString();

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";

                            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedFunctions && !string.IsNullOrEmpty(sDBConnectionDatabase))
                            {
                                AddACData(dtTemp.Rows[iRow]["Name"].ToString(), "U-Functions");
                            }
                        }

                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedFunctions += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                Application.DoEvents();
                sTempDistinct = ",";

                //Tables
                #region Get info of Tables
                sSql = "SELECT Table_Name AS TableName, Table_Rows AS RowCount, Create_Time AS Create_Date, Update_Time AS Modify_Date FROM information_schema.tables WHERE Table_Schema = '{0}' AND Table_Type = '{1}' ORDER BY Table_Name";
                sSql = string.Format(sSql, t, "BASE TABLE");
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_MySQL

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        double dRowCount;

                        if (bShowColumnInfo)
                        {
                            var drColumnInfo = dtColumnInfo.Select("Table_Name = '" + dtTemp.Rows[iRow]["TableName"] + "'");

                            foreach (var t1 in drColumnInfo)
                            {
                                row = dtSchema.NewRow();
                                row["SchemaObject"] = sDBConnectionName;
                                row["SchemaNode"] = t;
                                row["SchemaType"] = "Tables" + sSeparator + "(" + dtTemp.Rows.Count + ")";

                                double.TryParse(dtTemp.Rows[iRow]["RowCount"].ToString(), out dRowCount);
                                row["SchemaName"] = dtTemp.Rows[iRow]["TableName"] + sSeparator + "(" + dRowCount.ToString("###,##0") + ")";

                                if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["TableName"] + ",", StringComparison.Ordinal) == -1)
                                {
                                    sTempDistinct += dtTemp.Rows[iRow]["TableName"] + ",";

                                    if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedTables && !string.IsNullOrEmpty(sDBConnectionDatabase))
                                    {
                                        AddACData(dtTemp.Rows[iRow]["TableName"].ToString(), "Tables");
                                    }
                                }

                                var sColumnInfo = t1["Column_Name"].ToString();
                                var sDataType = t1["Column_Type"].ToString().Replace("(", " (");

                                var columns = dtSchema.Columns;

                                if (columns.Contains("ColumnInfo"))
                                {
                                    row["ColumnInfo"] = sColumnInfo + ", " + sDataType;
                                }
                                else
                                {
                                    row["SchemaName"] = sColumnInfo + ", " + sDataType;
                                }

                                dtSchema.Rows.Add(row);
                            }
                        }
                        else
                        {
                            row = dtSchema.NewRow();
                            row["SchemaObject"] = sDBConnectionName;
                            row["SchemaNode"] = t;
                            row["SchemaType"] = "Tables" + sSeparator + "(" + dtTemp.Rows.Count + ")";

                            double.TryParse(dtTemp.Rows[iRow]["RowCount"].ToString(), out dRowCount);

                            var columns = dtSchema.Columns;

                            if (columns.Contains("Schema_Browser"))
                            {
                                row["Schema_Browser"] = dtTemp.Rows[iRow]["TableName"] + sSeparator + "(" + dRowCount.ToString("###,##0") + ")";
                            }
                            else
                            {
                                row["SchemaName"] = dtTemp.Rows[iRow]["TableName"] + sSeparator + "(" + dRowCount.ToString("###,##0") + ")";
                            }

                            if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["TableName"] + ",", StringComparison.Ordinal) == -1)
                            {
                                sTempDistinct += dtTemp.Rows[iRow]["TableName"] + ",";

                                if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedTables && !string.IsNullOrEmpty(sDBConnectionDatabase))
                                {
                                    AddACData(dtTemp.Rows[iRow]["TableName"].ToString(), "Tables");
                                }
                            }

                            dtSchema.Rows.Add(row);
                        }

                        Application.DoEvents();
                    }

                    MyLibrary.sKeywordsUserDefinedTables += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                sTempDistinct = ",";

                //Triggers
                #region Get info of Triggers
                sSql = "SELECT Trigger_Schema AS SchemaName, Trigger_Name AS Name, Created AS CreateDate FROM information_schema.triggers cc WHERE Trigger_Schema = '{0}' ORDER BY Trigger_Name;";
                sSql = string.Format(sSql, t);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_MySQL

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaType"] = "Triggers" + sSeparator + "(" + dtTemp.Rows.Count + ")";

                        var columns = dtSchema.Columns;

                        if (columns.Contains("Schema_Browser"))
                        {
                            row["Schema_Browser"] = dtTemp.Rows[iRow]["Name"].ToString();
                        }
                        else
                        {
                            row["SchemaName"] = dtTemp.Rows[iRow]["Name"].ToString();
                        }

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";

                            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedTriggers && !string.IsNullOrEmpty(sDBConnectionDatabase))
                            {
                                AddACData(dtTemp.Rows[iRow]["Name"].ToString(), "Triggers");
                            }
                        }

                        Application.DoEvents();
                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedTriggers += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                sTempDistinct = ",";

                //Views
                #region Get info of Views
                sSql = "SELECT Table_Name AS NAME, View_Definition FROM information_schema.views cc WHERE Table_Schema = '{0}' ORDER BY Table_Name;";
                sSql = string.Format(sSql, t);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_MySQL

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaType"] = "Views" + sSeparator + "(" + dtTemp.Rows.Count + ")";

                        var columns = dtSchema.Columns;

                        if (columns.Contains("Schema_Browser"))
                        {
                            row["Schema_Browser"] = dtTemp.Rows[iRow]["Name"].ToString();
                        }
                        else
                        {
                            row["SchemaName"] = dtTemp.Rows[iRow]["Name"].ToString();
                        }

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";

                            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedViews && !string.IsNullOrEmpty(sDBConnectionDatabase))
                            {
                                AddACData(dtTemp.Rows[iRow]["Name"].ToString(), "Views");
                            }
                        }

                        Application.DoEvents();
                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedViews += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                //Procedures
                #region Get info of Procedures
                sSql = "";
                sSql += "SELECT Routine_Schema AS Database_Name,\r\n";
                sSql += "       Routine_Name AS Name,\r\n";
                sSql += "       Routine_Type AS Type,\r\n";
                sSql += "       Routine_Definition AS Definition\r\n";
                sSql += "FROM information_schema.routines\r\n";
                sSql += "WHERE Routine_Schema = '{0}' AND Routine_Type = 'PROCEDURE'\r\n";
                sSql += "ORDER BY Routine_Schema, Routine_Name;";
                sSql = string.Format(sSql, t);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_MySQL

                if (dtTemp == null || dtTemp.Rows.Count <= 0)
                {
                    continue;
                }

                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    row = dtSchema.NewRow();
                    row["SchemaObject"] = sDBConnectionName;
                    row["SchemaNode"] = t;
                    row["SchemaType"] = "Procedures" + sSeparator + "(" + dtTemp.Rows.Count + ")";

                    var columns = dtSchema.Columns;

                    if (columns.Contains("Schema_Browser"))
                    {
                        row["Schema_Browser"] = dtTemp.Rows[iRow]["Name"].ToString();
                    }
                    else
                    {
                        row["SchemaName"] = dtTemp.Rows[iRow]["Name"].ToString();
                    }

                    dtSchema.Rows.Add(row);
                }
                #endregion
            }

            #region 處理 Auto Complete 資訊
            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACBuiltInKeywords && !string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                var part1 = MyLibrary.sKeywordsBuiltInKeywords.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in part1)
                {
                    AddACData(t, "B-Keywords");
                }
            }

            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACBuiltInFunctions && !string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                var part1 = MyLibrary.sKeywordsBuiltInFunctions.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in part1)
                {
                    AddACData(t, "B-Functions");
                }
            }

            if (MyLibrary.bEnableAutoComplete && MyLibrary.bACUserDefinedKeywords && !string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                var part1 = MyLibrary.sKeywordsUserDefinedKeywords.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var t in part1)
                {
                    AddACData(t, "U-Keywords");
                }
            }
            #endregion

            UpdateSchemaData(c1Grid, true); //UpdateSchemaData_MySQL
            UpdateDatabaseInfo4AC_MySQL();

            if (!string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                UpdateTableAndViewInfo4AC_MySQL(sDBConnectionDatabase);
                dtTableAndViewName.Merge(dtDatabaseName);
            }
            else
            {
                dtTableAndViewName = dtDatabaseName.Copy();
            }

            //將 database name 納入 AC
            if (MyLibrary.bEnableAutoComplete && !string.IsNullOrEmpty(sDBConnectionDatabase))
            {
                for (var iRow = 0; iRow < dtDatabaseName.Rows.Count; iRow++)
                {
                    AddACData(dtDatabaseName.Rows[iRow]["DB"].ToString(), "Database");
                }
            }
        }

        public static void UpdateDatabaseInfo4AC_MySQL() //一開始沒有指定資料庫，只要取得所有的資料庫名稱 (USE 指令會用到)
        {
            dtDatabaseName = new DataTable();
            dtDatabaseName.Columns.Add("DB"); //Database
            dtDatabaseName.Columns.Add("DBAndNode"); //Database+Node
            dtDatabaseName.Columns.Add("SchemaNode");
            dtDatabaseName.Columns.Add("SchemaName");
            dtDatabaseName.Columns.Add("SchemaType");
            dtDatabaseName.Columns.Add("Memo");

            var dtTemp = new DataTable();
            var sSql = "";
            sSql = "SELECT Schema_Name AS Name FROM information_schema.schemata ORDER BY Schema_Name;";
            ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateDatabaseInfo4AC_MySQL

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    var row = dtDatabaseName.NewRow();
                    row["DB"] = dtTemp.Rows[iRow]["Name"].ToString(); //方便比對，此處要轉大寫
                    row["Memo"] = "DB"; //這個給 USE 使用的
                    dtDatabaseName.Rows.Add(row);
                }
            }
        }

        public static void UpdateTableAndViewInfo4AC_MySQL(string sSchemaName) //切換資料庫時，也要一併更新 Table & View Info
        {
            //for Query Editor AC, 取得指定資料庫的所有 Table + View Name
            dtTableAndViewName = new DataTable();
            dtTableAndViewName.Columns.Add("DB"); //Database
            dtTableAndViewName.Columns.Add("DBAndNode"); //Database+Node
            dtTableAndViewName.Columns.Add("SchemaNode");
            dtTableAndViewName.Columns.Add("SchemaName");
            dtTableAndViewName.Columns.Add("SchemaType");
            dtTableAndViewName.Columns.Add("Memo");

            var sSql = "SELECT '{0}' AS DB, Table_Name AS SchemaName, '[Table]' AS SchemaType FROM information_schema.tables WHERE Table_Schema = '{0}' AND Table_Type = '{1}'\r\n";
            sSql += "UNION ALL\r\n";
            sSql += "SELECT '{0}' AS DB, Table_Name AS SchemaName, '[View]' AS SchemaType FROM information_schema.views cc WHERE Table_Schema = '{0}'"; // ORDER BY Table_Name;";
            sSql = string.Format(sSql, sSchemaName, "BASE TABLE");

            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLServer

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                for (var i = 0; i < dtTemp.Rows.Count; i++)
                {
                    var row = dtTableAndViewName.NewRow();
                    row["DB"] = dtTemp.Rows[i]["DB"].ToString(); //方便比對，此處要轉大寫
                                                                 //row["DBAndNode"] = dtTemp.Rows[i]["DB"].ToString();
                    row["SchemaName"] = dtTemp.Rows[i]["SchemaName"].ToString();
                    row["SchemaType"] = dtTemp.Rows[i]["SchemaType"].ToString();
                    dtTableAndViewName.Rows.Add(row);
                }
            }
        }

        public static void GetTableInfo_MySQL(string sSchemaName, out DataTable dtTable) //切換資料庫時，也要一併更新 Table & View Info
        {
            var sSql = "SELECT Table_Name AS SchemaName FROM information_schema.tables WHERE Table_Schema = '{0}' AND Table_Type = '{1}' ORDER BY Table_Name";
            sSql = string.Format(sSql, sSchemaName, "BASE TABLE");

            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp); //GetTableInfo_MySQL
            dtTable = dtTemp.Copy();
        }

        public static void GetViewInfo_MySQL(string sSchemaName, out DataTable dtTable) //切換資料庫時，也要一併更新 Table & View Info
        {
            var sSql = "SELECT Table_Name AS SchemaName FROM information_schema.views cc WHERE Table_Schema = '{0}' ORDER BY Table_Name"; // ORDER BY Table_Name;";
            sSql = string.Format(sSql, sSchemaName);

            var dtTemp = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtTemp); //GetViewInfo_MySQL
            dtTable = dtTemp.Copy();
        }

        public static void UpdateSchemaData_SQLite(C1TrueDBGrid c1Grid)
        {
            string sSql;
            var dtTemp = new DataTable();

            CreateSchemaTable();

            var sSchemaName = sDBConnectionDatabase;

            if (string.IsNullOrEmpty(sSchemaName)) //沒有指定，則撈取全部
            {
                sSql = "SELECT Schema_Name AS Name FROM information_schema.schemata ORDER BY Schema_Name;";
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLite

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        sSchemaName += dtTemp.Rows[iRow]["Name"] + "`";
                    }
                }

                sSql = "SELECT Table_Schema, Table_Name, Column_Name, Data_Type, Character_Maximum_Length, Numeric_Precision, Numeric_Scale, Column_Type\r\n" +
                       "  FROM information_schema.Columns\r\n" +
                       " ORDER BY Table_Name, " + (bSortByColumnName ? "Column_Name;" : "Ordinal_Position;");
            }
            else
            {
                sSql = "SELECT Table_Schema, Table_Name, Column_Name, Data_Type, Character_Maximum_Length, Numeric_Precision, Numeric_Scale, Column_Type\r\n" +
                       "  FROM information_schema.Columns\r\n" +
                       " WHERE Table_Schema = '{0}'\r\n" +
                       " ORDER BY Table_Name, " + (bSortByColumnName ? "Column_Name;" : "Ordinal_Position;");
                sSql = string.Format(sSql, sSchemaName);
            }

            var dtColumnInfo = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtColumnInfo); //UpdateSchemaData_SQLite

            var parts = sSchemaName.Split(new[] { "`" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var t in parts)
            {
                var sTempDistinct = ",";

                //Functions
                #region Get info of Functions
                sSql = "";
                sSql += "SELECT Routine_Schema AS Database_Name,\r\n";
                sSql += "       Routine_Name AS Name,\r\n";
                sSql += "       Routine_Type AS Type,\r\n";
                sSql += "       Data_Type AS Return_Type,\r\n";
                sSql += "       Routine_Definition AS Definition\r\n";
                sSql += "  FROM information_schema.routines\r\n";
                sSql += " WHERE Routine_Schema = '{0}' AND Routine_Type = 'FUNCTION'\r\n";
                sSql += " ORDER BY Routine_Schema, Routine_Name;";
                sSql = string.Format(sSql, t);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLite

                DataRow row;

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaType"] = "Functions" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                        row["SchemaName"] = dtTemp.Rows[iRow]["Name"].ToString();

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";
                        }

                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedFunctions += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                Application.DoEvents();
                sTempDistinct = ",";

                //Tables
                #region Get info of Tables
                sSql = "SELECT Table_Name AS TableName, Table_Rows AS RowCount, Create_Time AS Create_Date, Update_Time AS Modify_Date FROM information_schema.tables WHERE Table_Schema = '{0}' AND Table_Type = '{1}' ORDER BY Table_Name";
                sSql = string.Format(sSql, t, "BASE TABLE");
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLite

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        double dRowCount;

                        if (bShowColumnInfo)
                        {
                            var drColumnInfo = dtColumnInfo.Select("Table_Name = '" + dtTemp.Rows[iRow]["TableName"] + "'");

                            foreach (var t1 in drColumnInfo)
                            {
                                row = dtSchema.NewRow();
                                row["SchemaObject"] = sDBConnectionName;
                                row["SchemaNode"] = t;
                                row["SchemaType"] = "Tables" + sSeparator + "(" + dtTemp.Rows.Count + ")";

                                double.TryParse(dtTemp.Rows[iRow]["RowCount"].ToString(), out dRowCount);
                                row["SchemaName"] = dtTemp.Rows[iRow]["TableName"] + sSeparator + "(" + dRowCount.ToString("###,##0") + ")";

                                if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["TableName"] + ",", StringComparison.Ordinal) == -1)
                                {
                                    sTempDistinct += dtTemp.Rows[iRow]["TableName"] + ",";
                                }

                                var sColumnInfo = t1["Column_Name"].ToString();
                                var sDataType = t1["Column_Type"].ToString().Replace("(", " (");
                                row["ColumnInfo"] = sColumnInfo + ", " + sDataType;
                                dtSchema.Rows.Add(row);
                            }
                        }
                        else
                        {
                            row = dtSchema.NewRow();
                            row["SchemaObject"] = sDBConnectionName;
                            row["SchemaNode"] = t;
                            row["SchemaType"] = "Tables" + sSeparator + "(" + dtTemp.Rows.Count + ")";

                            double.TryParse(dtTemp.Rows[iRow]["RowCount"].ToString(), out dRowCount);
                            row["SchemaName"] = dtTemp.Rows[iRow]["TableName"] + sSeparator + "(" + dRowCount.ToString("###,##0") + ")";

                            if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["TableName"] + ",", StringComparison.Ordinal) == -1)
                            {
                                sTempDistinct += dtTemp.Rows[iRow]["TableName"] + ",";
                            }

                            dtSchema.Rows.Add(row);
                        }

                        Application.DoEvents();
                    }

                    MyLibrary.sKeywordsUserDefinedTables += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                sTempDistinct = ",";

                //Triggers
                #region Get info of Triggers
                sSql = "SELECT Trigger_Schema AS SchemaName, Trigger_Name AS Name, Created AS CreateDate FROM information_schema.triggers cc WHERE Trigger_Schema = '{0}' ORDER BY Trigger_Name;";
                sSql = string.Format(sSql, t);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLite

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaType"] = "Triggers" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                        row["SchemaName"] = dtTemp.Rows[iRow]["Name"].ToString();

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";
                        }

                        Application.DoEvents();
                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedTriggers += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                sTempDistinct = ",";

                //Views
                #region Get info of Views
                sSql = "SELECT Table_Name AS NAME, View_Definition FROM information_schema.views cc WHERE Table_Schema = '{0}' ORDER BY Table_Name;";
                sSql = string.Format(sSql, t);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLite

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaType"] = "Views" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                        row["SchemaName"] = dtTemp.Rows[iRow]["Name"].ToString();

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";
                        }

                        Application.DoEvents();
                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedViews += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                //Procedures
                #region Get info of Procedures
                sSql = "";
                sSql += "SELECT Routine_Schema AS Database_Name,\r\n";
                sSql += "       Routine_Name AS Name,\r\n";
                sSql += "       Routine_Type AS Type,\r\n";
                sSql += "       Routine_Definition AS Definition\r\n";
                sSql += "FROM information_schema.routines\r\n";
                sSql += "WHERE Routine_Schema = '{0}' AND Routine_Type = 'PROCEDURE'\r\n";
                sSql += "ORDER BY Routine_Schema, Routine_Name;";
                sSql = string.Format(sSql, t);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLite

                if (dtTemp == null || dtTemp.Rows.Count <= 0)
                {
                    continue;
                }

                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    row = dtSchema.NewRow();
                    row["SchemaObject"] = sDBConnectionName;
                    row["SchemaNode"] = t;
                    row["SchemaType"] = "Procedures" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                    row["SchemaName"] = dtTemp.Rows[iRow]["Name"].ToString();
                    dtSchema.Rows.Add(row);
                }
                #endregion
            }

            UpdateSchemaData(c1Grid, true); //UpdateSchemaData_SQLite
        }

        public static void UpdateSchemaData_SQLCipher(C1TrueDBGrid c1Grid)
        {
            string sSql;
            var dtTemp = new DataTable();

            CreateSchemaTable();

            var sSchemaName = sDBConnectionDatabase;

            if (string.IsNullOrEmpty(sSchemaName)) //沒有指定，則撈取全部
            {
                sSql = "SELECT Schema_Name AS Name FROM information_schema.schemata ORDER BY Schema_Name;";
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLCipher

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        sSchemaName += dtTemp.Rows[iRow]["Name"] + "`";
                    }
                }

                sSql = "SELECT Table_Schema, Table_Name, Column_Name, Data_Type, Character_Maximum_Length, Numeric_Precision, Numeric_Scale, Column_Type\r\n" +
                       "  FROM information_schema.Columns\r\n" +
                       " ORDER BY Table_Name, " + (bSortByColumnName ? "Column_Name;" : "Ordinal_Position;");
            }
            else
            {
                sSql = "SELECT Table_Schema, Table_Name, Column_Name, Data_Type, Character_Maximum_Length, Numeric_Precision, Numeric_Scale, Column_Type\r\n" +
                       "  FROM information_schema.Columns\r\n" +
                       " WHERE Table_Schema = '{0}'\r\n" +
                       " ORDER BY Table_Name, " + (bSortByColumnName ? "Column_Name;" : "Ordinal_Position;");
                sSql = string.Format(sSql, sSchemaName);
            }

            var dtColumnInfo = new DataTable();
            ExecuteQueryToDataTable(sSql, ref dtColumnInfo); //UpdateSchemaData_SQLCipher

            var parts = sSchemaName.Split(new[] { "`" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var t in parts)
            {
                var sTempDistinct = ",";

                //Functions
                #region Get info of Functions
                sSql = "";
                sSql += "SELECT Routine_Schema AS Database_Name,\r\n";
                sSql += "       Routine_Name AS Name,\r\n";
                sSql += "       Routine_Type AS Type,\r\n";
                sSql += "       Data_Type AS Return_Type,\r\n";
                sSql += "       Routine_Definition AS Definition\r\n";
                sSql += "  FROM information_schema.routines\r\n";
                sSql += " WHERE Routine_Schema = '{0}' AND Routine_Type = 'FUNCTION'\r\n";
                sSql += " ORDER BY Routine_Schema, Routine_Name;";
                sSql = string.Format(sSql, t);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLCipher

                DataRow row;

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaType"] = "Functions" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                        row["SchemaName"] = dtTemp.Rows[iRow]["Name"].ToString();

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";
                        }

                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedFunctions += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                Application.DoEvents();
                sTempDistinct = ",";

                //Tables
                #region Get info of Tables
                sSql = "SELECT Table_Name AS TableName, Table_Rows AS RowCount, Create_Time AS Create_Date, Update_Time AS Modify_Date FROM information_schema.tables WHERE Table_Schema = '{0}' AND Table_Type = '{1}' ORDER BY Table_Name";
                sSql = string.Format(sSql, t, "BASE TABLE");
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLCipher

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        double dRowCount;

                        if (bShowColumnInfo)
                        {
                            var drColumnInfo = dtColumnInfo.Select("Table_Name = '" + dtTemp.Rows[iRow]["TableName"] + "'");

                            foreach (var t1 in drColumnInfo)
                            {
                                row = dtSchema.NewRow();
                                row["SchemaObject"] = sDBConnectionName;
                                row["SchemaNode"] = t;
                                row["SchemaType"] = "Tables" + sSeparator + "(" + dtTemp.Rows.Count + ")";

                                double.TryParse(dtTemp.Rows[iRow]["RowCount"].ToString(), out dRowCount);
                                row["SchemaName"] = dtTemp.Rows[iRow]["TableName"] + sSeparator + "(" + dRowCount.ToString("###,##0") + ")";

                                if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["TableName"] + ",", StringComparison.Ordinal) == -1)
                                {
                                    sTempDistinct += dtTemp.Rows[iRow]["TableName"] + ",";
                                }

                                var sColumnInfo = t1["Column_Name"].ToString();
                                var sDataType = t1["Column_Type"].ToString().Replace("(", " (");
                                row["ColumnInfo"] = sColumnInfo + ", " + sDataType;
                                dtSchema.Rows.Add(row);
                            }
                        }
                        else
                        {
                            row = dtSchema.NewRow();
                            row["SchemaObject"] = sDBConnectionName;
                            row["SchemaNode"] = t;
                            row["SchemaType"] = "Tables" + sSeparator + "(" + dtTemp.Rows.Count + ")";

                            double.TryParse(dtTemp.Rows[iRow]["RowCount"].ToString(), out dRowCount);
                            row["SchemaName"] = dtTemp.Rows[iRow]["TableName"] + sSeparator + "(" + dRowCount.ToString("###,##0") + ")";

                            if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["TableName"] + ",", StringComparison.Ordinal) == -1)
                            {
                                sTempDistinct += dtTemp.Rows[iRow]["TableName"] + ",";
                            }

                            dtSchema.Rows.Add(row);
                        }

                        Application.DoEvents();
                    }

                    MyLibrary.sKeywordsUserDefinedTables += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                sTempDistinct = ",";

                //Triggers
                #region Get info of Triggers
                sSql = "SELECT Trigger_Schema AS SchemaName, Trigger_Name AS Name, Created AS CreateDate FROM information_schema.triggers cc WHERE Trigger_Schema = '{0}' ORDER BY Trigger_Name;";
                sSql = string.Format(sSql, t);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLCipher

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaType"] = "Triggers" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                        row["SchemaName"] = dtTemp.Rows[iRow]["Name"].ToString();

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";
                        }

                        Application.DoEvents();
                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedTriggers += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                sTempDistinct = ",";

                //Views
                #region Get info of Views
                sSql = "SELECT Table_Name AS NAME, View_Definition FROM information_schema.views cc WHERE Table_Schema = '{0}' ORDER BY Table_Name;";
                sSql = string.Format(sSql, t);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLCipher

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                    {
                        row = dtSchema.NewRow();
                        row["SchemaObject"] = sDBConnectionName;
                        row["SchemaNode"] = t;
                        row["SchemaType"] = "Views" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                        row["SchemaName"] = dtTemp.Rows[iRow]["Name"].ToString();

                        if (sTempDistinct.IndexOf("," + dtTemp.Rows[iRow]["Name"] + ",", StringComparison.Ordinal) == -1)
                        {
                            sTempDistinct += dtTemp.Rows[iRow]["Name"] + ",";
                        }

                        Application.DoEvents();
                        dtSchema.Rows.Add(row);
                    }

                    MyLibrary.sKeywordsUserDefinedViews += sTempDistinct.Replace(",", " ").Trim() + " ";
                }
                #endregion

                //Procedures
                #region Get info of Procedures
                sSql = "";
                sSql += "SELECT Routine_Schema AS Database_Name,\r\n";
                sSql += "       Routine_Name AS Name,\r\n";
                sSql += "       Routine_Type AS Type,\r\n";
                sSql += "       Routine_Definition AS Definition\r\n";
                sSql += "FROM information_schema.routines\r\n";
                sSql += "WHERE Routine_Schema = '{0}' AND Routine_Type = 'PROCEDURE'\r\n";
                sSql += "ORDER BY Routine_Schema, Routine_Name;";
                sSql = string.Format(sSql, t);
                ExecuteQueryToDataTable(sSql, ref dtTemp); //UpdateSchemaData_SQLCipher

                if (dtTemp == null || dtTemp.Rows.Count <= 0)
                {
                    continue;
                }

                for (var iRow = 0; iRow < dtTemp.Rows.Count; iRow++)
                {
                    row = dtSchema.NewRow();
                    row["SchemaObject"] = sDBConnectionName;
                    row["SchemaNode"] = t;
                    row["SchemaType"] = "Procedures" + sSeparator + "(" + dtTemp.Rows.Count + ")";
                    row["SchemaName"] = dtTemp.Rows[iRow]["Name"].ToString();
                    dtSchema.Rows.Add(row);
                }
                #endregion
            }

            UpdateSchemaData(c1Grid, true); //UpdateSchemaData_SQLCipher
        }

        private static void CreateSchemaTable()
        {
            dtSchema = new DataTable();
            dtSchema.Columns.Add("SchemaObject");

            if (sDataSource == "PostgreSQL" || sDataSource == "SQL Server" || sDataSource == "MySQL")
            {
                dtSchema.Columns.Add("SchemaNode");
            }

            dtSchema.Columns.Add("SchemaType");
            dtSchema.Columns.Add("SchemaName");

            if (bShowColumnInfo)
            {
                dtSchema.Columns.Add("ColumnInfo");
            }

            switch (sDataSource)
            {
                case "SQL Server":
                    dtSchema.Columns.Add("SchemaDbo");
                    dtSchema.Columns.Add("ObjectID");
                    dtSchema.Columns.Add("CreateDate");
                    dtSchema.Columns.Add("ModifyDate");
                    break;
                case "MySQL":
                    dtSchema.Columns.Add("CreateDate");
                    dtSchema.Columns.Add("ModifyDate");
                    break;
            }
        }

        public static void UpdateSchemaData(C1TrueDBGrid c1Grid, bool bRename = false)
        {
            var i = 0;

            var columns = dtSchema.Columns;

            switch (bRename)
            {
                case true when bShowColumnInfo:
                    {
                        if (columns.Contains("ColumnInfo"))
                        {
                            dtSchema.Columns["ColumnInfo"].ColumnName = "Schema_Browser"; //sNameSchemaBrowser
                        }

                        break;
                    }
                case true:
                    {
                        if (columns.Contains("SchemaName"))
                        {
                            dtSchema.Columns["SchemaName"].ColumnName = "Schema_Browser"; //sNameSchemaBrowser
                        }

                        break;
                    }
            }

            var _dtSchemaTable = dtSchema.Copy();
            c1Grid.DataSource = _dtSchemaTable;

            switch (sDataSource)
            {
                case "Oracle":
                    c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaObject"]);
                    c1Grid.GroupedColumns[0].GroupInfo.HeaderText = "{0}";
                    c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaType"]);
                    c1Grid.GroupedColumns[1].GroupInfo.HeaderText = "{0}";

                    if (bShowColumnInfo)
                    {
                        c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaName"]);
                        c1Grid.GroupedColumns[2].GroupInfo.HeaderText = "{0}";
                    }

                    break;
                case "PostgreSQL":
                    c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaObject"]);
                    c1Grid.GroupedColumns[0].GroupInfo.HeaderText = "{0}";
                    c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaNode"]);
                    c1Grid.GroupedColumns[1].GroupInfo.HeaderText = "{0}";
                    c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaType"]);
                    c1Grid.GroupedColumns[2].GroupInfo.HeaderText = "{0}";

                    if (bShowColumnInfo)
                    {
                        c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaName"]);
                        c1Grid.GroupedColumns[3].GroupInfo.HeaderText = "{0}";
                    }

                    break;
                case "SQL Server":
                    c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaObject"]);
                    c1Grid.GroupedColumns[0].GroupInfo.HeaderText = "{0}";
                    c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaNode"]);
                    c1Grid.GroupedColumns[1].GroupInfo.HeaderText = "{0}";
                    c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaType"]);
                    c1Grid.GroupedColumns[2].GroupInfo.HeaderText = "{0}";

                    if (bShowColumnInfo)
                    {
                        c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaName"]);
                        c1Grid.GroupedColumns[3].GroupInfo.HeaderText = "{0}";
                    }

                    i = 1;
                    c1Grid.Splits[0].DisplayColumns["SchemaDbo"].Visible = false;
                    c1Grid.Splits[0].DisplayColumns["ObjectID"].Visible = false;
                    c1Grid.Splits[0].DisplayColumns["CreateDate"].Visible = false;
                    c1Grid.Splits[0].DisplayColumns["ModifyDate"].Visible = false;

                    break;
                case "MySQL":
                    c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaObject"]);
                    c1Grid.GroupedColumns[0].GroupInfo.HeaderText = "{0}";
                    c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaNode"]);
                    c1Grid.GroupedColumns[1].GroupInfo.HeaderText = "{0}";
                    c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaType"]);
                    c1Grid.GroupedColumns[2].GroupInfo.HeaderText = "{0}";

                    if (bShowColumnInfo)
                    {
                        c1Grid.GroupedColumns.Add(c1Grid.Columns["SchemaName"]);
                        c1Grid.GroupedColumns[3].GroupInfo.HeaderText = "{0}";
                    }

                    i = 1;
                    c1Grid.Splits[0].DisplayColumns["CreateDate"].Visible = false;
                    c1Grid.Splits[0].DisplayColumns["ModifyDate"].Visible = false;

                    break;
            }

            //展開指定的節點 (-1:標題列，所以，從 0 開始算，要展開哪一個)
            for (var j = 0; j <= i; j++)
            {
                c1Grid.ExpandGroupRow(j);
            }
        }

        public static string GetFirstWord(string sText)
        {
            var sResult = "";

            for (var i = 0; i < sText.Length; i++)
            {
                if (sText.Substring(i, 1) == "\r" || sText.Substring(i, 1) == "\n" || sText.Substring(i, 1) == " " || sText.Substring(i, 1) == sSeparator || sText.Substring(i, 1) == "-" || sText.Substring(i, 1) == "/" || sText.Substring(i, 1) == ";")
                {
                    break;
                }
                else
                {
                    sResult += sText.Substring(i, 1);
                }
            }

            return sResult;
        }

        public static void RefreshDataForSchemaSearch() //20220709
        {
            var sSql = "";
            var dtTemp = new DataTable();

            switch (sDataSource)
            {
                case "SQL Server" when !string.IsNullOrEmpty(sDBConnectionDatabase):
                {
                    //for frmSchemaSearch, 取得所有 Table + 所有 Column
                    sSql = "SELECT Table_Name AS TableName, Column_Name AS ColumnName, Data_Type AS DataType FROM {0}.INFORMATION_SCHEMA.COLUMNS WHERE Table_Catalog = '{0}' ORDER BY Table_Name, Ordinal_Position;";
                    sSql = string.Format(sSql, sDBConnectionDatabase);
                    ExecuteQueryToDataTable(sSql, ref dtTableColumn); //RefreshDataForSchemaSearch, SQL Server

                    //for frmSchemaSearch, 取得所有 Table + Functions + Views + Triggers + Procedure
                    sSql = "";
                    sSql += "SELECT SCHEMA_NAME(Schema_ID) AS SchemaNode, 'FUNCTION' AS SchemaType, o.Name AS SchemaName FROM {0}.sys.all_objects o WHERE Type = 'FN' AND Is_Ms_Shipped <> 1\r\n";
                    sSql += "UNION ALL\r\n";
                    sSql += "SELECT SCHEMA_NAME(Schema_ID) AS SchemaNode, 'TABLE' AS SchemaType, o.Name AS SchemaName FROM {0}.sys.objects o WHERE Type = 'U' AND Is_Ms_Shipped <> 1\r\n";
                    sSql += "UNION ALL\r\n";
                    sSql += "SELECT SCHEMA_NAME(Schema_ID) AS SchemaNode, 'TRIGGER' AS SchemaType, o.Name AS SchemaName FROM {0}.sys.all_objects o WHERE Type = 'TR' AND Is_Ms_Shipped <> 1\r\n";
                    sSql += "UNION ALL\r\n";
                    sSql += "SELECT SCHEMA_NAME(Schema_ID) AS SchemaNode, 'VIEW' AS SchemaType, o.Name AS SchemaName FROM {0}.sys.all_objects o WHERE Type = 'V' AND Is_Ms_Shipped <> 1\r\n";
                    sSql += "UNION ALL\r\n";
                    sSql += "SELECT SCHEMA_NAME(Schema_ID) AS SchemaNode, 'PROCEDURE' AS SchemaType, o.Name AS SchemaName FROM {0}.sys.all_objects o WHERE Type = 'P' AND Is_Ms_Shipped <> 1";
                    sSql = string.Format(sSql, sDBConnectionDatabase);

                    if (bDBExcludeNativeDatabase == false)
                    {
                        sSql = sSql.Replace(" AND Is_Ms_Shipped <> 1", "");
                    }

                    ExecuteQueryToDataTable(sSql, ref dtTFVTP); //RefreshDataForSchemaSearch, SQL Server
                    break;
                }
                case "MySQL" when !string.IsNullOrEmpty(sDBConnectionDatabase):
                    //for frmSchemaSearch, 取得所有 Table + 所有 Column
                    sSql = "SELECT Table_Name AS TableName, Column_Name AS ColumnName, Data_Type AS DataType\r\n" +
                           "  FROM information_schema.Columns\r\n" +
                           " WHERE Table_Schema = '{0}'\r\n" +
                           " ORDER BY Table_Name, Ordinal_Position;";
                    sSql = string.Format(sSql, sDBConnectionDatabase);
                    ExecuteQueryToDataTable(sSql, ref dtTableColumn); //RefreshDataForSchemaSearch, MySQL

                    //for frmSchemaSearch, 取得所有 Table + Functions + Views + Triggers + Procedure
                    sSql = "";
                    sSql += "SELECT Routine_Schema AS SchemaNode, 'FUNCTION' AS SchemaType, Routine_Name AS SchemaName FROM information_schema.routines WHERE Routine_Schema = '{0}' AND Routine_Type = 'FUNCTION'\r\n";
                    sSql += " UNION ALL\r\n";
                    sSql += "SELECT Table_Schema AS SchemaNode, 'TABLE' AS SchemaType, Table_Name AS SchemaName FROM information_schema.tables WHERE Table_Schema = '{0}' AND Table_Type = 'BASE TABLE'\r\n";
                    sSql += " UNION ALL\r\n";
                    sSql += "SELECT Trigger_Schema AS SchemaNode, 'TRIGGER' AS SchemaType, Trigger_Name AS SchemaName FROM information_schema.triggers cc WHERE Trigger_Schema = '{0}'\r\n";
                    sSql += " UNION ALL\r\n";
                    sSql += "SELECT Table_Schema AS SchemaNode, 'VIEW' AS SchemaType, Table_Name AS SchemaName FROM information_schema.views WHERE Table_Schema = '{0}'\r\n";
                    sSql += " UNION ALL\r\n";
                    sSql += "SELECT Routine_Schema AS SchemaNode, 'PROCEDURE' AS SchemaType, Routine_Name AS SchemaName FROM information_schema.routines WHERE Routine_Schema = '{0}' AND Routine_Type = 'PROCEDURE'";
                    sSql = string.Format(sSql, sDBConnectionDatabase);
                    ExecuteQueryToDataTable(sSql, ref dtTFVTP); //RefreshDataForSchemaSearch, MySQL
                    break;
            }
        }

        public static bool IsEngAlphabetOrNumberOrSpecialCharacters(string sChar, string sChar1 = "0", string sChar2 = "0", string sChar3 = "0")
        {
            var reg = new Regex(@"^[A-Za-z0-9]+$");
            var sSymbol = "a`a~a!a@a#a$a%a^a&a*a(a)a_a-a+a=a[a{a]a}a|a\\a;a:a'a\"a,a<a.a>a/a?a".Replace("a", sSeparator);
            return reg.IsMatch(sChar) || sChar == sChar1 || sChar == sChar2 || sChar == sChar3 || sSymbol.IndexOf(sSeparator + sChar + sSeparator, StringComparison.Ordinal) != -1;
        }

        public static bool IsEngAlphabetOrNumber(string sChar, string sChar1 = "0", string sChar2 = "0", string sChar3 = "0")
        {
            var reg = new Regex(@"^[A-Za-z0-9]+$");
            return reg.IsMatch(sChar) || sChar == sChar1 || sChar == sChar2 || sChar == sChar3;
        }

        public static bool IsEngAlphabet(string sChar)
        {
            var reg = new Regex(@"^[A-Za-z]+$");
            return reg.IsMatch(sChar);
        }

        public static void MouseDownDataGridExportDataToFile(C1TrueDBGrid c1TrueDBGrid1, int iX, int iY, ContextMenuStrip cMenuGrid)
        {
            var bCornerSelected = false;
            c1TrueDBGrid1.ContextMenuStrip = null;

            //取得滑鼠所在列的儲存格行列位置
            var iRow = c1TrueDBGrid1.RowContaining(iY);
            var iCol = c1TrueDBGrid1.ColContaining(iX);

            if (iRow == -1 && iCol == -1)
            {
                bCornerSelected = iY <= c1TrueDBGrid1.Splits[0].ColumnCaptionHeight;
            }

            if (bCornerSelected) //按下 Grid's 左上角
            {
                return;
            }

            if (iRow == -1)
            {
                return;
            }

            if (iCol == -1) //使用者點到最左側，略過！
            {
                return;
            }

            var sColumnName = c1TrueDBGrid1.Splits[0].DisplayColumns[iCol].ToString();
            var bValue = ((DataTable)c1TrueDBGrid1.DataSource).Rows.Count > 0;

            var a = Assembly.GetExecutingAssembly();
            var sTemp = GetLanguageString("Export All Data to File (Excel/CSV...)", "form", "frmQuery", "menugrid", "ExportAllDataToFile", "Text");
            cMenuGrid.Items.Add(sTemp);
            cMenuGrid.Items[0].Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.export 16x16.ico"));

            cMenuGrid.Items[0].Click += delegate
            {
                using (var myForm = new frmExportToFile())
                {
                    var dt = (DataTable)c1TrueDBGrid1.DataSource;
                    myForm.dtData = dt;
                    myForm.dtSchemaTable = dt;
                    myForm.sFontName = c1TrueDBGrid1.Font.Name;
                    myForm.fFontSize = c1TrueDBGrid1.Font.Size;
                    myForm.ShowDialog();
                }
            };

            c1TrueDBGrid1.ContextMenuStrip = cMenuGrid;

            cMenuGrid.BackColor = ColorTranslator.FromHtml("#FAFAD2"); //淺黃

            cMenuGrid.Show(c1TrueDBGrid1, new Point(iX, iY));

            c1TrueDBGrid1.SetActiveCell(iRow, iCol);
        }
    }
}