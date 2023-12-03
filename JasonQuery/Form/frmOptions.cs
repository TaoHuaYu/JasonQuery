using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using JasonLibrary;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using C1.Win.C1TrueDBGrid;
using JasonLibrary.Class;
using JasonLibrary.ColorPicker;
using JasonLibrary.Stylers;
using VisualStyle = Crownwood.Magic.Common.VisualStyle;

namespace JasonQuery
{
    public sealed partial class frmOptions : Form
    {
        private int _iPID; //for Auto Complete, PID
        private string _sPanelColorSelectedName = "";
        private List<object> _lstPanelTabColor; //Tab
        private List<object> _lstPanelQueryEditorColor; //Query Editor
        private List<object> _lstPanelACColor; //Auto Complete Info
        private List<object> _lstPanelGridColor; //Grid
        private List<object> _lstc1Grid;
        private List<object> _lstFindTextBox;
        private List<object> _lstFindNextButton;
        private List<object> _lstFindPreviousButton;
        private List<object> _lstFindEditor;
        //private List<object> _lstFindPicture;
        private List<object> _lstFindGroup;
        private ContextMenuStrip _cMenu = new ContextMenuStrip();
        private ContextMenuStrip _gMenu = new ContextMenuStrip();
        private readonly ContextMenu _nullMenu = new ContextMenu();
        private readonly ToolTip _toolTip1 = new ToolTip();
        public event ValueUpdatedEventHandler ValueUpdated;
        private DataTable _dtARInfo;
        private DataRow _rowARInfo;
        private DataTable _dtVisualStyle;
        private string _sLangText = "";
        //private string _sColKeywordName = "Keyword";
        //private string _sColReplacementName = "Replacement";

        private List<string> _lstGridHeaderAr = new List<string>();

        private Color _cEditorFocused = Color.LightGoldenrodYellow;
        private Color _cEditorUnfocused = SystemColors.Control;

        private enum eColAR
        {
            PID = 0,
            Keyword,
            Replacement
        }

        private const int BOOKMARK_MARGIN = 1; // Conventionally the symbol margin
        private const int BOOKMARK_MARKER = 3; // Arbitrary. Any valid index would work.

        private bool _bFormLoadFinish; //表單是否載入完畢 (避免觸發事件)
        private bool _bApplyAndClose; //使用是否按下「套用&關閉」？

        //private List<string> _lstMenuEditor = new List<string>();
        private readonly List<string> _lstMenuGrid = new List<string>();

        private bool _ctrlKeyDown;
        private int _totalDelta;

        //////for MessageBox's Position
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(IntPtr classname, string title); // extern method: FindWindow

        [DllImport("user32.dll")]
        private static extern void MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool rePaint); // extern method: MoveWindow

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rect); // extern method: GetWindowRect

        private enum c1GridID
        {
            //AcInfo = 0,
            //ArInfo = 1,
            VisualStyle = 0
        }

        private int _rowHeight; // original row height
        private int _recSelWidth; // orignal record selector width
        private float _fontSize; // original font size

        private enum uMenu
        {
            CellViewer = 0,
            Dash0,
            SelectAll,
            Dash1,
            ExportToExcel,
            ExportToCsv,
            ExportToFile,
            Dash2,
            Copy,
            CopyWithColumnNames,
            CopyColumnNames,
            Dash3,
            FreezeColumn,
            UnfreezeColumn
        }

        private readonly List<Point> _modifiedList = new List<Point>();

        public frmOptions()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (MyGlobal.sDataSource == "Oracle")
            {
                chkPreviewCLOBData.Visible = true;
            }

            ApplyLocalizationSetting(); //Form_Load

            MyGlobal.SetGridVisualStyle(c1GridARInfo, 10);

            txtSpecifiedSQLFile1.Text = MyGlobal.sSpecifiedSQLFile1;
            txtSpecifiedSQLFile2.Text = MyGlobal.sSpecifiedSQLFile2;

            var bValue = false;
            var sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'EditorConfig' AND AttributeName = 'SortByColumnName'";
            var dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkSortByColumnName.Checked = bValue; //重啟後才會生效，故此處要直接撈取 DB 的值

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'EditorConfig' AND AttributeName = 'ShowColumnInfo'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkShowColumnInfo.Checked = bValue; //重啟後才會生效，故此處要直接撈取 DB 的值

            bValue = false;
            sSql = "SELECT * FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'EditorConfig' AND AttributeName = 'DefaultTabSchemaBrowser'";
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0 && dtTemp.Rows[0]["AttributeValue"].ToString() == "1")
            {
                bValue = true;
            }

            chkDefaultTabSchemaBrowser.Checked = bValue; //重啟後才會生效，故此處要直接撈取 DB 的值

            chkAutoListMembers.Checked = MyGlobal.bAutoListMembers;
            chkSavePoint.Checked = MyGlobal.bSavePoint;

            if (MyGlobal.sDataSource != "PostgreSQL")
            {
                chkSavePoint.Enabled = false;
            }

            #region Copy Settings...
            btnCopySettings.Visible = false;
            btnCopySettings.Items.Clear();

            sSql = "SELECT * FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND PID <> " + MyGlobal.sDBMotherPID;
            dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count > 0)
            {
                btnCopySettings.Visible = true;

                var sFrom = MyGlobal.GetLanguageString("From", "form", Name, "msg", "From", "Text");
                var sTo = MyGlobal.GetLanguageString("To", "form", Name, "msg", "To", "Text");

                for (var i = 0; i < dtTemp.Rows.Count; i++)
                {
                    var dropDownItem = new C1.Win.C1Input.DropDownItem();
                    btnCopySettings.Items.Add(dropDownItem);
                    btnCopySettings.Items[i].Tag = dtTemp.Rows[i]["PID"].ToString();
                    btnCopySettings.Items[i].Text = sFrom + " <" + dtTemp.Rows[i]["DataSource"] + ", " + dtTemp.Rows[i]["ConnectionName"] + ">  " + sTo + " <" + MyGlobal.sDataSource + ", " + MyGlobal.sDBConnectionName + ">";

                    if (i == 0) //只註冊一次即可！
                    {
                        btnCopySettings.DropDownItemClicked += btnCopySettings_DropDownItemClicked;
                    }
                }
            }
            #endregion

            if (MyLibrary.bDarkMode)
            {
                C1.Win.C1Themes.C1ThemeController.ApplicationTheme = "VS2013Dark";

                c1ThemeController1.SetTheme(c1GridARInfo, "VS2013Dark");
                MyGlobal.SetGridVisualStyle(c1GridARInfo, 10);
                c1GridARInfo.BackColor = ColorTranslator.FromHtml("#2D2D30"); //Office 2010 Black
                c1ThemeController1.SetTheme(c1GridVisualStyle, "VS2013Dark");
                MyGlobal.SetGridVisualStyle(c1GridVisualStyle, 10);
                c1GridVisualStyle.BackColor = ColorTranslator.FromHtml("#2D2D30"); //Office 2010 Black

                ApplyDarkStyler();

                c1GridVisualStyle.BorderColor = Color.White;
                c1GridVisualStyle.HeadingStyle.Borders.Color = Color.White;
                c1GridVisualStyle.HeadingStyle.ForeColor = ColorTranslator.FromHtml(MyLibrary.sGridHeadingForeColor);
                c1GridVisualStyle.RowDivider.Color = Color.White;
                c1GridVisualStyle.Font = new Font(MyLibrary.sGridFontName, Convert.ToSingle(MyLibrary.sGridFontSize), FontStyle.Regular, GraphicsUnit.Point);
                c1GridVisualStyle.HeadingStyle.Font = new Font(MyLibrary.sGridFontName, Convert.ToSingle(MyLibrary.sGridFontSize), FontStyle.Regular, GraphicsUnit.Point);
                c1GridVisualStyle.MarqueeStyle = MarqueeEnum.HighlightCell;

                MyLibrary.sGridVisualStyle = "Office 2010 Black";
                cboGridVisualStyle.Text = "Office 2010 Black";
                cboGridVisualStyle.Enabled = false;

                _cEditorUnfocused = ColorTranslator.FromHtml("#2D2D30");
            }

            MyGlobal.SetC1ComboBoxItemsFromDictionary(cboLocalization, MyGlobal.dicLocalization, true);
            cboLocalization.Text = MyGlobal.sLocalization;
            cboLocalization.Tag = MyGlobal.sLocalization;

            cboDateFormat.Text = MyLibrary.sDateFormat;
            chkShowVersion.Checked = MyLibrary.bShowVersion;
            chkHideClock.Checked = MyLibrary.bHideClock;

            //記住原值，如果變更語系時，才能正確處理
            MyGlobal.sNameOptions_Before = MyGlobal.sNameOptions;
            MyGlobal.sNameSchemaBrowser_Before = MyGlobal.sNameSchemaBrowser;
            MyGlobal.sNameSQLHistory_Before = MyGlobal.sNameSQLHistory;

            editor.Tag = ""; //multi selection 時判斷用的

            editorSQLToCodePreview.Tag = editor.Text;
            editorSQLToCode.Text = editor.Text;
            editorSQLFormatter.Text = editor.Text;

            _rowHeight = c1GridVisualStyle.RowHeight;
            _recSelWidth = c1GridVisualStyle.RecordSelectorWidth;
            _fontSize = c1GridVisualStyle.Styles["Normal"].Font.Size;

            _toolTip1.ForeColor = Color.Blue;
            _toolTip1.BackColor = Color.Gray;

            _toolTip1.UseAnimation = true;
            _toolTip1.AutoPopDelay = 5000;
            _toolTip1.InitialDelay = 50;
            _toolTip1.ReshowDelay = 30;

            Cursor = Cursors.WaitCursor;

            grpBuiltInFunctions.Text += " (" + MyGlobal.sDataSource + ")";
            grpBuiltInKeywords.Text += " (" + MyGlobal.sDataSource + ")";

            _toolTip1.SetToolTip(nudMinFragmentLength, "2 ~ 9");
            _toolTip1.SetToolTip(txtRecentFiles, "10 ~ 60");
            _toolTip1.SetToolTip(txtMyFavorite, "10 ~ 60");

            nudMinFragmentLength.ContextMenu = _nullMenu;
            txtRecentFiles.ContextMenu = _nullMenu;
            txtMyFavorite.ContextMenu = _nullMenu;

            //英文字母強制大寫
            //e.KeyChar = Char.ToUpper(e.KeyChar);
            //txtReplacement.CharacterCasing = CharacterCasing.Upper;

            _lstPanelTabColor = new List<object>
            {
                pnlOptionsTabActiveForeColor,
                pnlOptionsTabActiveBackColor,
                pnlOptionsTabInactiveForeColor
            };

            _lstPanelQueryEditorColor = new List<object>
            {
                pnlToolstripBackground,
                pnlEditorBackground,
                pnlCurrentLineBackground,
                pnlSelectedTextBackground,
                pnlErrorLineBackground,
                pnlBookmarkBackground,
                pnlComments,
                pnlIdentifier,
                pnlNumber,
                pnlOperatorSymbol,
                pnlOperatorKeywords,
                pnlString,
                pnlCharacter,
                pnlBuiltInFunctions,
                pnlBuiltInKeywords,
                pnlUserDefinedKeywords,
                pnlWhiteSpace,
                pnlUserTables,
                pnlUserFunctions,
                pnlHighlightForeColor
            };

            _lstPanelGridColor = new List<object>
            {
                pnlNullValueForeColor,
                pnlGridHeadingForeColor,
                pnlGridEvenRowForeColor,
                pnlGridEvenRowBackColor,
                pnlGridOddRowForeColor,
                pnlGridOddRowBackColor,
                pnlGridHighlightForeColor,
                pnlGridHighlightBackColor,
                pnlGridSelectedForeColor,
                pnlGridSelectedBackColor
            };

            _lstc1Grid = new List<object>
            {
                c1GridARInfo,
                c1GridVisualStyle
            };

            _lstFindTextBox = new List<object>
            {
                txtFindOperatorKeywords,
                txtFindBuiltInFunctions,
                txtFindBuiltInKeywords,
                txtFindUserDefinedKeywords
            };

            _lstFindNextButton = new List<object>
            {
                btnNextOperatorKeywords,
                btnNextBuiltInFunctions,
                btnNextBuiltInKeywords,
                btnNextUserDefinedKeywords
            };

            _lstFindPreviousButton = new List<object>
            {
                btnPreviousOperatorKeywords,
                btnPreviousBuiltInFunctions,
                btnPreviousBuiltInKeywords,
                btnPreviousUserDefinedKeywords
            };

            _lstFindEditor = new List<object>
            {
                txtOperatorKeywords,
                txtBuiltInFunctions,
                txtBuiltInKeywords,
                txtUserDefinedKeywords
            };

            _lstFindGroup = new List<object>
            {
                grpFindOperatorKeywords,
                grpFindBuiltInFunctions,
                grpFindBuiltInKeywords,
                grpFindUserDefinedKeywords
            };

            //將空白顯示成符號
            //editor.SetWhitespaceForeColor(true, Color.Red);
            //editor.WhitespaceSize = 3; //3:剛好

            //editor.Styles[ScintillaNET.Style.Default].BackColor = Color.Red;
            //panel1.BackColor = ColorTranslator.FromHtml("#FFC000");

            c1GridVisualStyle.MouseWheel += c1GridVisualStyle_MouseWheel;

            c1GridVisualStyle.KeyDown += Detect_KeyDown;
            c1GridVisualStyle.KeyUp += Detect_KeyUp;

            //Query Editor Color 套用
            #region Query Editor Color 套用，並將 Color 的值存入 Tag
            //Label.Tag：用意 - 當使用者按下 Close 時，用 Label.Tag 的顏色來還原
            pnlToolstripBackground.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorToolstripBackground);
            pnlToolstripBackground.Tag = MyLibrary.sColorToolstripBackground;
            lblEditorBackground.Tag = MyLibrary.sColorToolstripBackground;
            _toolTip1.SetToolTip(pnlToolstripBackground, MyLibrary.sColorToolstripBackground + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorToolstripBackground));

            _cEditorFocused = pnlToolstripBackground.BackColor;

            pnlEditorBackground.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorEditorBackground);
            pnlEditorBackground.Tag = MyLibrary.sColorEditorBackground;
            lblEditorBackground.Tag = MyLibrary.sColorEditorBackground;
            _toolTip1.SetToolTip(pnlEditorBackground, MyLibrary.sColorEditorBackground + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorEditorBackground));

            pnlCurrentLineBackground.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            pnlCurrentLineBackground.Tag = MyLibrary.sColorCurrentLineBackground;
            lblCurrentLineBackground.Tag = MyLibrary.sColorCurrentLineBackground;
            _toolTip1.SetToolTip(pnlCurrentLineBackground, MyLibrary.sColorCurrentLineBackground + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorCurrentLineBackground));

            pnlSelectedTextBackground.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground);
            pnlSelectedTextBackground.Tag = MyLibrary.sColorSelectedTextBackground;
            lblSelectedTextBackground.Tag = MyLibrary.sColorSelectedTextBackground;
            _toolTip1.SetToolTip(pnlSelectedTextBackground, MyLibrary.sColorSelectedTextBackground + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorSelectedTextBackground));

            pnlErrorLineBackground.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorErrorLineBackground);
            pnlErrorLineBackground.Tag = MyLibrary.sColorErrorLineBackground;
            lblErrorLineBackground.Tag = MyLibrary.sColorErrorLineBackground;
            _toolTip1.SetToolTip(pnlErrorLineBackground, MyLibrary.sColorErrorLineBackground + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorErrorLineBackground));

            pnlBookmarkBackground.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorBookmarkBackground);
            pnlBookmarkBackground.Tag = MyLibrary.sColorBookmarkBackground;
            lblBookmarkBackground.Tag = MyLibrary.sColorBookmarkBackground;
            _toolTip1.SetToolTip(pnlBookmarkBackground, MyLibrary.sColorBookmarkBackground + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorBookmarkBackground));

            pnlComments.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorComments);
            pnlComments.Tag = MyLibrary.sColorComments;
            lblComments.Tag = MyLibrary.sColorComments;
            _toolTip1.SetToolTip(pnlComments, MyLibrary.sColorComments + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorComments));

            pnlIdentifier.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorTextIdentifier);
            pnlIdentifier.Tag = MyLibrary.sColorTextIdentifier;
            lblIdentifier.Tag = MyLibrary.sColorTextIdentifier;
            _toolTip1.SetToolTip(pnlIdentifier, MyLibrary.sColorTextIdentifier + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorTextIdentifier));

            pnlBuiltInKeywords.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorBuiltInKeywords);
            pnlBuiltInKeywords.Tag = MyLibrary.sColorBuiltInKeywords;
            lblBuiltInKeywords.Tag = MyLibrary.sColorBuiltInKeywords;
            _toolTip1.SetToolTip(pnlBuiltInKeywords, MyLibrary.sColorBuiltInKeywords + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorBuiltInKeywords));

            pnlUserDefinedKeywords.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorUserDefinedKeywords);
            pnlUserDefinedKeywords.Tag = MyLibrary.sColorUserDefinedKeywords;
            lblUserDefinedKeywords.Tag = MyLibrary.sColorUserDefinedKeywords;
            _toolTip1.SetToolTip(pnlUserDefinedKeywords, MyLibrary.sColorUserDefinedKeywords + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorUserDefinedKeywords));

            pnlNumber.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorNumber);
            pnlNumber.Tag = MyLibrary.sColorNumber;
            lblNumber.Tag = MyLibrary.sColorNumber;
            _toolTip1.SetToolTip(pnlNumber, MyLibrary.sColorNumber + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorNumber));

            pnlOperatorSymbol.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorOperatorSymbol);
            pnlOperatorSymbol.Tag = MyLibrary.sColorOperatorSymbol;
            lblOperatorSymbol.Tag = MyLibrary.sColorOperatorSymbol;
            _toolTip1.SetToolTip(pnlOperatorSymbol, MyLibrary.sColorOperatorSymbol + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorOperatorSymbol));

            pnlOperatorKeywords.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorOperatorKeywords);
            pnlOperatorKeywords.Tag = MyLibrary.sColorOperatorKeywords;
            lblOperatorKeywords.Tag = MyLibrary.sColorOperatorKeywords;
            _toolTip1.SetToolTip(pnlOperatorKeywords, MyLibrary.sColorOperatorKeywords + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorOperatorKeywords));

            pnlString.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorString);
            pnlString.Tag = MyLibrary.sColorString;
            lblString.Tag = MyLibrary.sColorString;
            _toolTip1.SetToolTip(pnlString, MyLibrary.sColorString + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorString));

            pnlCharacter.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorCharacter);
            pnlCharacter.Tag = MyLibrary.sColorCharacter;
            lblCharacter.Tag = MyLibrary.sColorCharacter;
            _toolTip1.SetToolTip(pnlCharacter, MyLibrary.sColorCharacter + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorCharacter));

            pnlBuiltInFunctions.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorBuiltInFunctions);
            pnlBuiltInFunctions.Tag = MyLibrary.sColorBuiltInFunctions;
            lblBuiltInFunctions.Tag = MyLibrary.sColorBuiltInFunctions;
            _toolTip1.SetToolTip(pnlBuiltInFunctions, MyLibrary.sColorBuiltInFunctions + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorBuiltInFunctions));

            pnlWhiteSpace.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace);
            pnlWhiteSpace.Tag = MyLibrary.sColorWhiteSpace;
            lblWhiteSpace.Tag = MyLibrary.sColorWhiteSpace;
            _toolTip1.SetToolTip(pnlWhiteSpace, MyLibrary.sColorWhiteSpace + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorWhiteSpace));

            pnlUserFunctions.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorUserDefinedFunctionsTirggers);
            pnlUserFunctions.Tag = MyLibrary.sColorUserDefinedFunctionsTirggers;
            lblUserFunctions.Tag = MyLibrary.sColorUserDefinedFunctionsTirggers;
            _toolTip1.SetToolTip(pnlUserFunctions, MyLibrary.sColorUserDefinedFunctionsTirggers + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorUserDefinedFunctionsTirggers));

            pnlUserTables.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorUserDefinedTablesViews);
            pnlUserTables.Tag = MyLibrary.sColorUserDefinedTablesViews;
            lblUserTables.Tag = MyLibrary.sColorUserDefinedTablesViews;
            _toolTip1.SetToolTip(pnlUserTables, MyLibrary.sColorUserDefinedTablesViews + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorUserDefinedTablesViews));

            //Query Editor 頁籤：Highlight
            pnlHighlightForeColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sHighlightColorForeColor);
            pnlHighlightForeColor.Tag = MyLibrary.sHighlightColorForeColor;
            lblHighlightColorForeColor.Tag = MyLibrary.sHighlightColorForeColor;
            _toolTip1.SetToolTip(pnlHighlightForeColor, MyLibrary.sHighlightColorForeColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sHighlightColorForeColor));

            MyGlobal.SetC1ComboBoxTextAndTriggerSelectedIndex(cboHighlightStyle, MyLibrary.sHighlightColorStyle);
            lblHighlightColorStyle.Tag = MyLibrary.sHighlightColorStyle;
            MyGlobal.SetC1ComboBoxTextAndTriggerSelectedIndex(cboHighlightOutlineAlpha, MyLibrary.sHighlightColorOutlineAlpha);
            lblHighlightColorOutlineAlpha.Tag = MyLibrary.sHighlightColorOutlineAlpha;
            MyGlobal.SetC1ComboBoxTextAndTriggerSelectedIndex(cboHighlightAlpha, MyLibrary.sHighlightColorAlpha);
            lblHighlightColorAlpha.Tag = MyLibrary.sHighlightColorAlpha;

            //Query Editor 頁籤：Preferences
            cboEditorFontPicker.Value = MyLibrary.sQueryEditorFontName; //指定的字型若之後被使用者移除了，也不會出現錯誤

            lblEditorFontName.Tag = MyLibrary.sQueryEditorFontName;
            MyGlobal.SetC1ComboBoxTextAndTriggerSelectedIndex(cboEditorFontSize, MyLibrary.sQueryEditorFontSize);
            lblEditorFontSize.Tag = MyLibrary.sQueryEditorFontSize;
            MyGlobal.SetC1ComboBoxTextAndTriggerSelectedIndex(cboEditorZoom, MyLibrary.sQueryEditorZoom);
            lblEditorZoom.Tag = MyLibrary.sQueryEditorZoom;

            chkWordWrap.Checked = MyLibrary.bWordWrap;
            chkWordWrap.Tag = chkWordWrap.Checked ? "1" : "0";
            btnWordWrap.Visible = !chkWordWrap.Checked;
            btnWordWrap2.Visible = chkWordWrap.Checked;

            chkStart.Checked = MyLibrary.bWordWrapVisualFlags_Start;
            chkStart.Tag = chkStart.Checked ? "1" : "0";
            chkEnd.Checked = MyLibrary.bWordWrapVisualFlags_End;
            chkEnd.Tag = chkEnd.Checked ? "1" : "0";
            chkMargin.Checked = MyLibrary.bWordWrapVisualFlags_Margin;
            chkMargin.Tag = chkMargin.Checked ? "1" : "0";
            cboIndentMode.Text = MyGlobal.sWordWrapIndentMode;
            cboIndentMode.Tag = MyGlobal.sWordWrapIndentMode;
            chkBold.Checked = MyLibrary.bKeywordFontBold;
            chkBold.Tag = chkBold.Checked ? "1" : "0";
            chkCopyAsHTML.Checked = MyLibrary.bCopyAsHTML;
            chkCopyAsHTML.Tag = chkCopyAsHTML.Checked ? "1" : "0";

            chkShowAllCharacters.Checked = MyLibrary.bShowAllCharacters;
            chkShowAllCharacters.Tag = chkShowAllCharacters.Checked ? "1" : "0";
            btnShowAllCharacters.Visible = !chkShowAllCharacters.Checked;
            btnShowAllCharacters2.Visible = chkShowAllCharacters.Checked;

            chkShowSaveAsButton.Checked = MyLibrary.bShowSaveAsButton;
            chkShowSaveAsButton.Tag = chkShowSaveAsButton.Checked ? "1" : "0";
            btnSaveAs.Visible = chkShowSaveAsButton.Checked;

            chkShowIndentGuide.Checked = MyLibrary.bShowIndentGuide;
            chkShowIndentGuide.Tag = chkShowIndentGuide.Checked ? "1" : "0";
            btnShowIndentGuide.Visible = !chkShowIndentGuide.Checked;
            btnShowIndentGuide2.Visible = chkShowIndentGuide.Checked;

            chkEntireBlankRowAsEmptyRow4SelectBlock.Checked = MyLibrary.bEntireBlankRowAsEmptyRow;
            chkEntireBlankRowAsEmptyRow4SelectBlock.Tag = chkEntireBlankRowAsEmptyRow4SelectBlock.Checked ? "1" : "0";
            chkHighlightSelection.Checked = MyLibrary.bHighlightSelection;
            chkHighlightSelection.Tag = chkHighlightSelection.Checked ? "1" : "0";
            #endregion

            editor.ViewEol = MyLibrary.bShowAllCharacters;
            editor.ViewWhitespace = MyLibrary.bShowAllCharacters ? ScintillaNET.WhitespaceMode.VisibleAlways : ScintillaNET.WhitespaceMode.Invisible;
            editor.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editor.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
            editor.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));
            editor.WrapMode = MyLibrary.bWordWrap ? ScintillaNET.WrapMode.Word : ScintillaNET.WrapMode.None;
            editor.WrapVisualFlags = (MyLibrary.bWordWrapVisualFlags_Start ? ScintillaNET.WrapVisualFlags.Start : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_End ? ScintillaNET.WrapVisualFlags.End : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_Margin ? ScintillaNET.WrapVisualFlags.Margin : ScintillaNET.WrapVisualFlags.None);

            editorAR.ViewEol = MyLibrary.bShowAllCharacters;
            editorAR.ViewWhitespace = MyLibrary.bShowAllCharacters ? ScintillaNET.WhitespaceMode.VisibleAlways : ScintillaNET.WhitespaceMode.Invisible;
            editorAR.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editorAR.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
            editorAR.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));
            editorAR.WrapMode = MyLibrary.bWordWrap ? ScintillaNET.WrapMode.Word : ScintillaNET.WrapMode.None;
            editorAR.WrapVisualFlags = (MyLibrary.bWordWrapVisualFlags_Start ? ScintillaNET.WrapVisualFlags.Start : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_End ? ScintillaNET.WrapVisualFlags.End : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_Margin ? ScintillaNET.WrapVisualFlags.Margin : ScintillaNET.WrapVisualFlags.None);

            editorSQLToCode.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editorSQLToCode.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色

            editorSQLFormatter.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editorSQLFormatter.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色

            editorSQLFormatterPreview.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editorSQLFormatterPreview.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色

            //tabControl1.SelectedIndex = 1; //可以正常運作
            //tabControl1.SelectedTab = tabPage2; //可以正常運作
            //tabControl1.SelectTab("tabPage" + textBox1.Text); //可以正常運作

            //載入 Auto Complete 設定
            #region 載入 Auto Complete 設定
            chkEnableAutoComplete.Checked = MyLibrary.bEnableAutoComplete;
            nudMinFragmentLength.Value = MyLibrary.iACMinFragmentLength;
            chkFirstCharChecking.Value = MyLibrary.bACFirstCharChecking;
            chkBuiltInKeywords.Checked = MyLibrary.bACBuiltInKeywords;
            chkBuiltInFunctions.Checked = MyLibrary.bACBuiltInFunctions;
            chkUserDefinedKeywords.Checked = MyLibrary.bACUserDefinedKeywords;
            chkUserDefinedFunctions.Checked = MyLibrary.bACUserDefinedFunctions;
            chkUserDefinedTables.Checked = MyLibrary.bACUserDefinedTables;
            chkUserDefinedTriggers.Checked = MyLibrary.bACUserDefinedTriggers;
            chkUserDefinedViews.Checked = MyLibrary.bACUserDefinedViews;
            #endregion

            //lblTips2.Text = "Tips #2: The first " + txtMinFragmentLength.Text + " characters must be the following conditions";

            //載入 Auto Replace 設定
            #region 載入 Auto Replace 設定
            chkEnableAutoReplace.Checked = MyLibrary.bEnableAutoReplace;
            #endregion

            //載入 Data Grid 設定            
            #region 載入 Data Grid 設定
            cboResultCopyQuotingWith.Text = MyLibrary.sGridQuotingWith;
            cboResultCopyFieldSeparator.Text = MyLibrary.sGridFieldSeparator;
            rdoMaximized.Checked = MyGlobal.bMainFormMaximized;
            rdoNormal.Checked = !MyGlobal.bMainFormMaximized;

            chkShowColumnType.Checked = MyLibrary.bGridShowColumnDataType;

            if (MyGlobal.sDataSource == "PostgreSQL")
            {
                chkShowStreamlinedName.Enabled = chkShowColumnType.Checked;
            }
            else
            {
                chkShowStreamlinedName.Enabled = false;
                chkShowStreamlinedName.Checked = false;
            }

            c1DockingTab.SelectedTab = tabDataGrid;
            chkShowFilterRow.Checked = MyLibrary.bGridShowFilterRow;
            c1DockingTab.SelectedTab = tabGlobal;

            chkShowGroupingRow.Checked = MyLibrary.bGridShowGroupingRow;

            chkSort.Checked = MyLibrary.bGridSort;

            chkResize.Checked = MyLibrary.bGridResize;
            lblMaxWidth.Enabled = chkResize.Checked;
            cboMaxWidth.Enabled = chkResize.Checked;

            pnlNullValueForeColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridNullShowColor);
            pnlNullValueForeColor.Tag = MyLibrary.sGridNullShowColor;
            lblNullValueForeColor.Tag = MyLibrary.sGridNullShowColor;
            _toolTip1.SetToolTip(pnlNullValueForeColor, MyLibrary.sGridNullShowColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sGridNullShowColor));

            chkRawDataMode.Checked = MyLibrary.bGridRawDataMode;
            cboNullShowAs.Text = MyLibrary.sGridNullShowAs;

            chkPagingQuery.Checked = MyLibrary.bGridPagingQuery;
            cboRowsPerPage.Text = MyLibrary.sGridRowsPerPage;
            chkAppendingQueries.Checked = MyLibrary.bGridAppendingQueries;

            chkSetFocusAfterQuery.Checked = MyLibrary.bGridSetFocusAfterQuery;

            cboGridVisualStyle.Text = MyLibrary.sGridVisualStyle;
            cboGridZoom.Text = MyLibrary.sGridZoom;
            cboGridFontPicker.Value = MyLibrary.sGridFontName;
            cboGridFontSize.Text = MyLibrary.sGridFontSize;

            pnlGridHeadingForeColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridHeadingForeColor);
            pnlGridHeadingForeColor.Tag = MyLibrary.sGridHeadingForeColor;
            _toolTip1.SetToolTip(pnlGridHeadingForeColor, MyLibrary.sGridHeadingForeColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sGridHeadingForeColor));

            pnlGridEvenRowForeColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowForeColor);
            pnlGridEvenRowForeColor.Tag = MyLibrary.sGridEvenRowForeColor;
            _toolTip1.SetToolTip(pnlGridEvenRowForeColor, MyLibrary.sGridEvenRowForeColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sGridEvenRowForeColor));

            pnlGridEvenRowBackColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridEvenRowBackColor);
            pnlGridEvenRowBackColor.Tag = MyLibrary.sGridEvenRowBackColor;
            _toolTip1.SetToolTip(pnlGridEvenRowBackColor, MyLibrary.sGridEvenRowBackColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sGridEvenRowBackColor));

            pnlGridOddRowForeColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowForeColor);
            pnlGridOddRowForeColor.Tag = MyLibrary.sGridOddRowForeColor;
            _toolTip1.SetToolTip(pnlGridOddRowForeColor, MyLibrary.sGridOddRowForeColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sGridOddRowForeColor));

            pnlGridOddRowBackColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridOddRowBackColor);
            pnlGridOddRowBackColor.Tag = MyLibrary.sGridOddRowBackColor;
            _toolTip1.SetToolTip(pnlGridOddRowBackColor, MyLibrary.sGridOddRowBackColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sGridOddRowBackColor));

            AlternatingRowColorSetting(); //Form_Load

            pnlGridHighlightForeColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridHighlightForeColor);
            pnlGridHighlightForeColor.Tag = MyLibrary.sGridHighlightForeColor;
            _toolTip1.SetToolTip(pnlGridHighlightForeColor, MyLibrary.sGridHighlightForeColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sGridHighlightForeColor));

            pnlGridHighlightBackColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridHighlightBackColor);
            pnlGridHighlightBackColor.Tag = MyLibrary.sGridHighlightBackColor;
            _toolTip1.SetToolTip(pnlGridHighlightBackColor, MyLibrary.sGridHighlightBackColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sGridHighlightBackColor));

            pnlGridSelectedForeColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedForeColor);
            pnlGridSelectedForeColor.Tag = MyLibrary.sGridSelectedForeColor;
            _toolTip1.SetToolTip(pnlGridSelectedForeColor, MyLibrary.sGridSelectedForeColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sGridSelectedForeColor));

            pnlGridSelectedBackColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sGridSelectedBackColor);
            pnlGridSelectedBackColor.Tag = MyLibrary.sGridSelectedBackColor;
            _toolTip1.SetToolTip(pnlGridSelectedBackColor, MyLibrary.sGridSelectedBackColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sGridSelectedBackColor));
            #endregion

            //載入 Keywords 設定
            #region 載入 Keywords 設定
            txtOperatorKeywords.Text = MyLibrary.sKeywordsOperatorKeywords;
            txtOperatorKeywords.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));
            txtBuiltInFunctions.Text = MyLibrary.sKeywordsBuiltInFunctions;
            txtBuiltInFunctions.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));
            txtBuiltInKeywords.Text = MyLibrary.sKeywordsBuiltInKeywords;
            txtBuiltInKeywords.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));
            txtUserDefinedKeywords.Text = MyLibrary.sKeywordsUserDefinedKeywords;
            txtUserDefinedKeywords.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));
            #endregion

            //載入 SQL To Code 設定
            #region 載入 SQL To Code 設定
            txtVariableName.Text = MyLibrary.sSQLToCodeVariableName;
            txtVariableName.Tag = MyLibrary.sSQLToCodeVariableName;
            #endregion

            //載入 SQL Formatter 設定
            #region 載入 SQL Formatter 設定
            txtMaxWidth.Text = MyLibrary.iSQLFormatterMaxLineWidth.ToString();
            chkExpandCommaLists.Checked = MyLibrary.bSQLFormatterExpandCommaLists;
            chkTrailingCommas.Checked = MyLibrary.bSQLFormatterTrailingCommas;
            chkExpandBooleanExpressions.Checked = MyLibrary.bSQLFormatterExpandBooleanExpressions;
            chkExpandCaseStatements.Checked = MyLibrary.bSQLFormatterExpandCaseStatements;
            chkExpandBetweenConditions.Checked = MyLibrary.bSQLFormatterExpandBetweenConditions;
            chkExpandInLists.Checked = MyLibrary.bSQLFormatterExpandInLists;
            chkBreakJoinOnSections.Checked = MyLibrary.bSQLFormatterBreakJoinOnSections;
            chkConvertCaseForKeywords.Checked = MyLibrary.bSQLFormatterConvertCaseForKeywords;

            switch (MyLibrary.iSQLFormatterConvertCaseForKeywordsCase)
            {
                case 2:
                    rdoLowerCase.Checked = true;
                    break;
                case 3:
                    rdoProperCase.Checked = true;
                    break;
                default:
                    rdoUpperCase.Checked = true;
                    break;
            }
            #endregion

            //載入 Global 設定
            #region 載入 Global 設定
            rdoCheckOnly.Checked = MyLibrary.bCheckForUpdate;

            switch (MyLibrary.iCheckForUpdate)
            {
                case 0:
                    rdoCheckForUpdates0.Checked = true;
                    break;
                case 1:
                    rdoCheckForUpdates1.Checked = true;
                    break;
                default:
                    rdoCheckForUpdates7.Checked = true;
                    break;
            }

            chkDarkMode.Checked = MyLibrary.bDarkMode;
            chkDarkMode.Tag = MyLibrary.bDarkMode ? "1" : "0";

            if (MyGlobal.bChangeColorThemeNeedRestart)
            {
                chkDarkMode.Enabled = false; //避免連續切換色彩佈景主題
                btnHelp_DarkMode.Visible = true;
            }

            try
            {
                if (MyLibrary.sTabStyle == "IDE")
                {
                    rdoIDE.Checked = true;
                }
                else
                {
                    rdoPlain.Checked = true;
                }
            }
            catch (Exception)
            {
                //
            }

            switch (MyLibrary.sTabAppearance)
            {
                case "MultiDocument":
                    rdoMultiDocument.Checked = true;
                    break;
                case "MultiForm":
                    rdoMultiForm.Checked = true;
                    break;
                default:
                    rdoMultiBox.Checked = true;
                    break;
            }

            chkTabBold.Checked = MyGlobal.bTabBold;
            chkShrinkPages.Checked = MyGlobal.bTabShrinkPages;
            chkShowArrows.Checked = MyGlobal.bTabShowArrows;
            chkHoverSelect.Checked = MyGlobal.bTabHoverSelect;
            chkMultiLine.Checked = MyGlobal.bTabMultiLine;

            txtRecentFiles.Text = MyLibrary.iRecentFilesQty.ToString();
            txtMyFavorite.Text = MyLibrary.iMyFavoriteQty.ToString();

            pnlOptionsTabActiveForeColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveForeColor);
            pnlOptionsTabActiveForeColor.Tag = MyLibrary.sColorOptionsTabActiveForeColor;
            lblOptionsTabActiveForeColor.Tag = MyLibrary.sColorOptionsTabActiveForeColor;
            _toolTip1.SetToolTip(pnlOptionsTabActiveForeColor, MyLibrary.sColorOptionsTabActiveForeColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorOptionsTabActiveForeColor));

            pnlOptionsTabActiveBackColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabActiveBackColor);
            pnlOptionsTabActiveBackColor.Tag = MyLibrary.sColorOptionsTabActiveBackColor;
            lblOptionsTabActiveBackColor.Tag = MyLibrary.sColorOptionsTabActiveBackColor;
            _toolTip1.SetToolTip(pnlOptionsTabActiveBackColor, MyLibrary.sColorOptionsTabActiveBackColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorOptionsTabActiveBackColor));

            pnlOptionsTabInactiveForeColor.BackColor = ColorTranslator.FromHtml(MyLibrary.sColorOptionsTabInactiveForeColor);
            pnlOptionsTabInactiveForeColor.Tag = MyLibrary.sColorOptionsTabInactiveForeColor;
            lblOptionsTabInactiveForeColor.Tag = MyLibrary.sColorOptionsTabInactiveForeColor;
            _toolTip1.SetToolTip(pnlOptionsTabInactiveForeColor, MyLibrary.sColorOptionsTabInactiveForeColor + " " + MyLibrary.GetRgbColorCode(MyLibrary.sColorOptionsTabInactiveForeColor));

            MyGlobal.SetDockingTabColor(c1DockingTab, pnlOptionsTabActiveBackColor.BackColor, pnlOptionsTabActiveForeColor.BackColor, pnlOptionsTabInactiveForeColor.BackColor);
            MyGlobal.SetDockingTabColor(c1DockingTab1, pnlOptionsTabActiveBackColor.BackColor, pnlOptionsTabActiveForeColor.BackColor, pnlOptionsTabInactiveForeColor.BackColor);

            if (MyLibrary.bDarkMode)
            {
                c1ThemeController1.SetTheme(c1DockingTab, "VS2013Dark");
            }
            #endregion

            #region 套用 tabExmaple 外觀
            tabExample.BackColor = ColorTranslator.FromHtml(MyGlobal.sTabBackColor);
            tabExample.ForeColor = ColorTranslator.FromHtml(MyGlobal.sTabActiveForeColor);
            tabExample.TextInactiveColor = ColorTranslator.FromHtml(MyGlobal.sTabInactiveForeColor);
            tabExample.BoldSelectedPage = true;
            tabExample.ShrinkPagesToFit = chkShrinkPages.Checked;
            tabExample.ShowArrows = chkShowArrows.Checked;
            tabExample.HoverSelect = chkHoverSelect.Checked;
            tabExample.Multiline = chkMultiLine.Checked;
            tabExample.PositionTop = true;
            tabExample.ShowClose = true;

            tabExample.Style = MyLibrary.sTabStyle == "IDE" ? VisualStyle.IDE : VisualStyle.Plain;

            switch (MyLibrary.sTabAppearance)
            {
                case "MultiDocument":
                    tabExample.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiDocument;
                    break;
                case "MultiForm":
                    tabExample.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiForm;
                    break;
                default:
                    tabExample.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiBox;
                    break;
            }
            #endregion

            //載入雜項設定
            if (!string.IsNullOrEmpty(MyLibrary.sDefaultDirectory) && Directory.Exists(MyLibrary.sDefaultDirectory)) //Default Directory
            {
                rdoFavoriteDirectory.Checked = true;
                txtFavoriteDirectory.Text = MyLibrary.sDefaultDirectory;
            }
            else
            {
                rdoDefaultDirectory.Checked = true;
            }

            cboGridVisualStyle.Text = MyLibrary.sGridVisualStyle;

            if (MyLibrary.bDarkMode == false)
            {
                ChangeVisualStyle(false); //bPreview=true, 表示只要針對「c1GridVisualStyle」作用即可
            }

            _rowHeight = c1GridVisualStyle.RowHeight;
            lstLanguage.SelectedIndex = 0;

            HighlightPreview(); //Form_Load
            btnClearHighlightsGrid.Enabled = false;

            //外觀套用
            ApplySqlStyler(); //Form1_Load
            editorIndicator.ReadOnly = false;
            editorIndicator.Text = "SELECT * FROM Empoyee";
            editorIndicator.ReadOnly = true;
            SetSquiggle(false, "", 14, 7); //Form_Load
            ApplyIndicatorAppearance(MyLibrary.sColorBookmarkBackground); //Form_Load

            _bFormLoadFinish = true;

            PreviewSQLToCode(); //Form1_load
            PreviewSqlFormatter(); //Form1_load

            CreateVisualStyleInfo(); //Form_Load

            UpdateMainFormTabVisualStyle(); //Form_Load

            Cursor = Cursors.Default;
        }

        private void ApplyLocalizationSetting()
        {
            MyGlobal.ApplyLanguageInfo(this); //ApplyLocalizationSetting

            lblStarShowVersion.Location = new Point(chkShowVersion.Left + chkShowVersion.Width - 5, lblStarShowVersion.Top);
            btnHelp_RawDataMode.Location = new Point(chkRawDataMode.Left + chkRawDataMode.Width - 3, btnHelp_RawDataMode.Top);
            btnHelp_AppendingQueries.Location = new Point(chkAppendingQueries.Left + chkAppendingQueries.Width - 3, btnHelp_AppendingQueries.Top);
            rdoNormal.Location = new Point(rdoMaximized.Left + rdoMaximized.Width + 20, rdoNormal.Top);
            lblStarSortByColumnName.Location = new Point(chkSortByColumnName.Left + chkSortByColumnName.Width - 5, lblStarSortByColumnName.Top);
            lblStarShowColumnInfo.Location = new Point(chkShowColumnInfo.Left + chkShowColumnInfo.Width - 5, lblStarShowColumnInfo.Top);
            btnHelp_ShowColumnInfo.Location = new Point(lblStarShowColumnInfo.Left + lblStarShowColumnInfo.Width + 3, btnHelp_ShowColumnInfo.Top);
            btnHelp_SavePoint.Location = new Point(chkSavePoint.Left + chkSavePoint.Width + 3, btnHelp_SavePoint.Top);

            //定義最大寬度的設定值
            MyGlobal.SetC1ComboBoxItemsFromDictionary(cboMaxWidth, MyGlobal.dicMaxWidth);
            cboMaxWidth.Text = MyGlobal.sGridMaxWidth;

            chkPreviewCLOBData.Checked = MyGlobal.bPreviewCLOBData;

            //定義縮排模式的設定值
            MyGlobal.SetC1ComboBoxItemsFromDictionary(cboIndentMode, MyGlobal.dicWordWrapIndentMode);
            cboIndentMode.Text = MyGlobal.sWordWrapIndentMode;

            cboTabWidth.Text = MyGlobal.iTabWidth.ToString();
            txtIndentWord.Text = MyGlobal.iTabWidth.ToString();

            //定義書籤樣式的設定值
            MyGlobal.SetC1ComboBoxItemsFromDictionary(cboBookmarkStyle, MyGlobal.dicBookmarkStyle);
            cboBookmarkStyle.Text = MyGlobal.sBookmarkStyle;

            //定義「自動中斷連線」的設定值
            MyGlobal.SetC1ComboBoxItemsFromDictionary(cboAutoDisconnect, MyGlobal.dicAutoDisconnect);
            cboAutoDisconnect.Text = MyGlobal.sAutoDisconnect;

            //Begin:定義調整列高方式的設定值
            MyGlobal.SetC1ComboBoxItemsFromDictionary(cboGridRowHeightResizing, MyGlobal.dicRowSizing);
            cboGridRowHeightResizing.Text = MyGlobal.sRowSizing;

            if (MyGlobal.GetKeyFromDictionary(MyGlobal.dicRowSizing, MyGlobal.sRowSizing) == "AllRows")
            {
                c1GridARInfo.AllowRowSizing = RowSizingEnum.AllRows;
                c1GridVisualStyle.AllowRowSizing = RowSizingEnum.AllRows;
            }
            else
            {
                c1GridARInfo.AllowRowSizing = RowSizingEnum.IndividualRows;
                c1GridVisualStyle.AllowRowSizing = RowSizingEnum.IndividualRows;
            }
            //End:定義調整列高方式的設定值

            lblLength.Text = grpMainFormTabVisualStyle.Text;
            grpMainFormTabVisualStyle.Text += @"    ";
            lblStarMainFormTab.Location = new Point(grpMainFormTabVisualStyle.Left + lblLength.Width - 10, lblStarMainFormTab.Top);

            cboFindGrid.Location = new Point(lblFindGrid.Width + 3, cboFindGrid.Top);
            cboSaveAsEncoding.Location = new Point(chkSaveAsEncoding.Left + chkSaveAsEncoding.Width, cboSaveAsEncoding.Top);
            cboIndentMode.Location = new Point(lblIndentMode.Left + lblIndentMode.Width + 3, cboIndentMode.Top);

            lblLength.Text = grpHighlight.Text;
            lblStarHighlight.Location = new Point(grpHighlight.Left + lblLength.Width - 10, lblStarHighlight.Top);
            lblStarHighlight.Visible = false;

            lblLength.Text = grpIndicate.Text;
            lblStarIndicate.Location = new Point(lblLength.Width + 2, lblStarIndicate.Top);
            lblStarIndicate.Visible = false;

            lblLength.Text = grpDataGridColor.Text;
            lblStarGridColor.Location = new Point(grpDataGridColor.Left + lblLength.Width - 10, lblStarGridColor.Top);

            btnHelp_DarkMode.Location = new Point(chkDarkMode.Left + chkDarkMode.Width, btnHelp_DarkMode.Top);

            //Begin:chkWordWrap
            lblLength.Text = chkWordWrap.Text;

            for (var i = 1; i < 100; i++)
            {
                lblLength.Text = "".PadRight(i, ' ');

                if (lblLength.Width <= chkWordWrap.Width)
                {
                    continue;
                }

                grpWordWrap.Text = "".PadRight(i + 0, ' ');
                break;
            }

            lblStarWordWrap.Location = new Point(chkWordWrap.Left + chkWordWrap.Width - 5, lblStarWordWrap.Top);
            lblStarWordWrap.Visible = false;
            //End:chkWordWrap

            //Begin:chkEnableAutoComplete
            lblLength.Text = chkEnableAutoComplete.Text;

            for (var i = 1; i < 500; i++)
            {
                lblLength.Text = "".PadRight(i, ' ');

                if (lblLength.Width <= chkEnableAutoComplete.Width)
                {
                    continue;
                }

                grpAutoComplete.Text = "".PadRight(i + 4, ' ');
                break;
            }

            lblStarAC.Location = new Point(chkEnableAutoComplete.Left + chkEnableAutoComplete.Width - 7, lblStarAC.Top);
            //lblStarAC.Visible = false;
            //End:chkEnableAutoComplete

            //Begin:chkEnableAutoReplace
            lblLength.Text = chkEnableAutoReplace.Text;

            for (var i = 1; i < 500; i++)
            {
                lblLength.Text = "".PadRight(i, ' ');

                if (lblLength.Width <= chkEnableAutoReplace.Width)
                {
                    continue;
                }

                grpAutoReplace.Text = "".PadRight(i + 1, ' ');
                break;
            }

            lblStarAR.Location = new Point(chkEnableAutoReplace.Left + chkEnableAutoReplace.Width - 5, lblStarAR.Top);
            lblStarAR.Visible = false;
            //End:chkEnableAutoReplace

            lblOptionsTabActiveBackColor.Location = new Point(pnlOptionsTabActiveForeColor.Left + pnlOptionsTabActiveForeColor.Width + 30, lblOptionsTabActiveBackColor.Top);
            pnlOptionsTabActiveBackColor.Location = new Point(lblOptionsTabActiveBackColor.Left + lblOptionsTabActiveBackColor.Width + 5, pnlOptionsTabActiveBackColor.Top);

            lblOptionsTabInactiveForeColor.Location = new Point(pnlOptionsTabActiveBackColor.Left + pnlOptionsTabActiveBackColor.Width + 30, lblOptionsTabInactiveForeColor.Top);
            pnlOptionsTabInactiveForeColor.Location = new Point(lblOptionsTabInactiveForeColor.Left + lblOptionsTabInactiveForeColor.Width + 5, pnlOptionsTabInactiveForeColor.Top);

            txtSpecifiedSQLFile1.Size = new Size(grpOpenSQLFile.Width - lblFile1.Width - btnSpecifiedSQLFile1.Width - btnClear1.Width - 40, 21);
            txtSpecifiedSQLFile1.Location = new Point(lblFile1.Left + lblFile1.Width + 2, txtSpecifiedSQLFile1.Top);
            btnSpecifiedSQLFile1.Location = new Point(txtSpecifiedSQLFile1.Left + txtSpecifiedSQLFile1.Width + 5, btnSpecifiedSQLFile1.Top);
            btnClear1.Location = new Point(btnSpecifiedSQLFile1.Left + btnSpecifiedSQLFile1.Width + 5, btnClear1.Top);

            txtSpecifiedSQLFile2.Size = new Size(grpOpenSQLFile.Width - lblFile2.Width - btnSpecifiedSQLFile2.Width - btnClear2.Width - 40, 21);
            txtSpecifiedSQLFile2.Location = new Point(lblFile2.Left + lblFile2.Width + 2, txtSpecifiedSQLFile2.Top);
            btnSpecifiedSQLFile2.Location = new Point(txtSpecifiedSQLFile2.Left + txtSpecifiedSQLFile2.Width + 5, btnSpecifiedSQLFile2.Top);
            btnClear2.Location = new Point(btnSpecifiedSQLFile2.Left + btnSpecifiedSQLFile2.Width + 5, btnClear2.Top);

            nudMinFragmentLength.Left = lblMinFragmentLength.Left + lblMinFragmentLength.Width;
            chkShowFilterRowAR.Location = new Point(lblARInfo1.Left + lblARInfo1.Width + 30, chkShowFilterRowAR.Top);
            cboMaxWidth.Location = new Point(lblMaxWidth.Left + lblMaxWidth.Width + 1, cboMaxWidth.Top);
            txtVariableName.Location = new Point(lblSQLVariableName.Left + lblSQLVariableName.Width + 1, txtVariableName.Top);
            txtMaxWidth.Location = new Point(lblMaxWidth2.Left + lblMaxWidth2.Width + 1, txtMaxWidth.Top);
            cboResultCopyQuotingWith.Location = new Point(lblResultCopyQuotingWith.Left + lblResultCopyQuotingWith.Width + 1, cboResultCopyQuotingWith.Top);
            cboResultCopyFieldSeparator.Location = new Point(lblResultCopyFieldSeparator.Left + lblResultCopyFieldSeparator.Width + 1, cboResultCopyFieldSeparator.Top);
            cboDateFormat.Location = new Point(lblDateFormat.Left + lblDateFormat.Width + 1, cboDateFormat.Top);
            cboLocalization.Location = new Point(lblLocalization.Left + lblLocalization.Width + 1, cboLocalization.Top);
            cboRowsPerPage.Location = new Point(lblRowsPerPage.Left + lblRowsPerPage.Width + 1, cboRowsPerPage.Top);

            cboNullShowAs.Location = new Point(lblNullValueShowAs.Left + lblNullValueShowAs.Width + 1, cboNullShowAs.Top);
            pnlNullValueForeColor.Location = new Point(lblNullValueForeColor.Left + lblNullValueForeColor.Width + 3, pnlNullValueForeColor.Top);

            pnlOptionsTabActiveForeColor.Location = new Point(lblOptionsTabActiveForeColor.Left + lblOptionsTabActiveForeColor.Width + 3, pnlOptionsTabActiveForeColor.Top);
            pnlOptionsTabActiveBackColor.Location = new Point(lblOptionsTabActiveBackColor.Left + lblOptionsTabActiveBackColor.Width + 3, pnlOptionsTabActiveBackColor.Top);
            pnlOptionsTabInactiveForeColor.Location = new Point(lblOptionsTabInactiveForeColor.Left + lblOptionsTabInactiveForeColor.Width + 3, pnlOptionsTabInactiveForeColor.Top);

            txtRecentFiles.Location = new Point(lblRecentFiles.Left + lblRecentFiles.Width + 3, txtRecentFiles.Top);
            txtMyFavorite.Location = new Point(lblMyFavorite.Left + lblMyFavorite.Width + 3, txtMyFavorite.Top);

            pnlErrorLineBackground.Location = new Point(lblErrorLineBackground.Left + lblErrorLineBackground.Width + 3, pnlErrorLineBackground.Top);
            pnlBookmarkBackground.Location = new Point(lblBookmarkBackground.Left + lblBookmarkBackground.Width + 3, pnlBookmarkBackground.Top);
            lblBookmarkStyle.Location = new Point(pnlErrorLineBackground.Left + pnlErrorLineBackground.Width + 33, lblBookmarkStyle.Top);
            cboBookmarkStyle.Location = new Point(lblBookmarkStyle.Left + lblBookmarkStyle.Width + 3, cboBookmarkStyle.Top);
            editorIndicator.Location = new Point(lblBookmarkStyle.Left + 3, editorIndicator.Top);
            cboTabWidth.Location = new Point(lblTabWidth.Left + lblTabWidth.Width + 3, cboTabWidth.Top);
            lblStarShowIndentGuide.Location = new Point(chkShowIndentGuide.Left + chkShowIndentGuide.Width - 4, lblStarShowIndentGuide.Top);
            lblStarTabWidth.Location = new Point(cboTabWidth.Left + cboTabWidth.Width + 3, lblStarTabWidth.Top);
            //End:調整位置

            _sLangText = MyGlobal.GetLanguageString("Display 'Start' flag on a wrapped line", "form", Name, "object", "chkStart", "ToolTipText");
            _toolTip1.SetToolTip(chkStart, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Display 'End' flag on a wrapped line", "form", Name, "object", "chkEnd", "ToolTipText");
            _toolTip1.SetToolTip(chkEnd, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Display 'Margin' flag on a wrapped line", "form", Name, "object", "chkMargin", "ToolTipText");
            _toolTip1.SetToolTip(chkMargin, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Determines how wrapped sublines are indented", "form", Name, "object", "cboIndentMode", "ToolTipText");
            _toolTip1.SetToolTip(cboIndentMode, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Find Operator Keywords (Ctrl+F)", "form", Name, "object", "picOperatorKeywords", "ToolTipText");
            _toolTip1.SetToolTip(picOperatorKeywords, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Find Built-In Functions (Ctrl+F)", "form", Name, "object", "picBuiltInFunctions", "ToolTipText");
            _toolTip1.SetToolTip(picBuiltInFunctions, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Find Built-In Keywords (Ctrl+F)", "form", Name, "object", "picBuiltInKeywords", "ToolTipText");
            _toolTip1.SetToolTip(picBuiltInKeywords, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Find User-Defined Keywords (Ctrl+F)", "form", Name, "object", "picUserDefinedKeywords", "ToolTipText");
            _toolTip1.SetToolTip(picUserDefinedKeywords, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Operator Keywords, Built-in Keywords, User-defined Keywords", "form", Name, "object", "chkBold", "ToolTipText");
            _toolTip1.SetToolTip(chkBold, _sLangText);

            _sLangText = MyGlobal.GetLanguageString("Browse File", "form", Name, "object", "btnSpecifiedSQLFile1", "ToolTipText");
            _toolTip1.SetToolTip(btnSpecifiedSQLFile1, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Browse File", "form", Name, "object", "btnSpecifiedSQLFile2", "ToolTipText");
            _toolTip1.SetToolTip(btnSpecifiedSQLFile2, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Clear", "form", Name, "object", "btnClear1", "ToolTipText");
            _toolTip1.SetToolTip(btnClear1, _sLangText);
            _sLangText = MyGlobal.GetLanguageString("Clear", "form", Name, "object", "btnClear2", "ToolTipText");
            _toolTip1.SetToolTip(btnClear2, _sLangText);

            _cMenu = new ContextMenuStrip();
            _gMenu = new ContextMenuStrip();

            _cMenu.Items.Add("Copy");
            ((ToolStripMenuItem) _cMenu.Items[0]).ShortcutKeys = Keys.Control | Keys.C;

            _cMenu.Items[0].Click += delegate
            {
                if (chkCopyAsHTML.Checked)
                {
                    editor.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                }
                else
                {
                    editor.Copy();
                }
            };

            //設定 icon
            //cMenu.Items[0].Image = _menuIcons.Images[1];

            _sLangText = MyGlobal.GetLanguageString("Cell Viewer", "form", Name, "menugrid", "CellViewer", "Text");
            _lstMenuGrid.Add(_sLangText);
            _lstMenuGrid.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "menugrid", "SelectAll", "Text");
            _lstMenuGrid.Add(_sLangText);
            _lstMenuGrid.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Export all data to Excel", "form", Name, "menugrid", "ExportAllDataToExcel", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Export all data to Excel without color", "form", Name, "menugrid", "ExportAllDataToExcelWithoutColor", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Export all data to CSV", "form", Name, "menugrid", "ExportAllDataToCSV", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Export all data to File (as Insert script)", "form", Name, "menugrid", "ExportAllDataToFile", "Text");
            _lstMenuGrid.Add(_sLangText);
            _lstMenuGrid.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Copy", "form", Name, "menugrid", "Copy", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Copy with Column Name(s)", "form", Name, "menugrid", "CopyWithColumnName", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Copy Column Name(s)", "form", Name, "menugrid", "CopyColumnName", "Text");
            _lstMenuGrid.Add(_sLangText);
            _lstMenuGrid.Add("-");
            _sLangText = MyGlobal.GetLanguageString("Freeze Column", "form", Name, "menugrid", "FreezeColumn", "Text");
            _lstMenuGrid.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Unfreeze Column", "form", Name, "menugrid", "UnfreezeColumn", "Text");
            _lstMenuGrid.Add(_sLangText);

            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.CellViewer]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.Dash0]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.SelectAll]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.Dash1]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.ExportToExcel]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.ExportToCsv]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.ExportToFile]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.Dash2]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.Copy]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.CopyWithColumnNames]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.CopyColumnNames]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.Dash3]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.FreezeColumn]);
            _gMenu.Items.Add(_lstMenuGrid[(int)uMenu.UnfreezeColumn]);

            _gMenu.Items[(int)uMenu.CellViewer].Click += delegate
            {
                CellViewer(); //右鍵選單
            };

            ((ToolStripMenuItem) _gMenu.Items[(int)uMenu.SelectAll]).ShortcutKeys = Keys.Control | Keys.A;

            _gMenu.Items[(int)uMenu.SelectAll].Click += delegate
            {
                for (var i = 0; i < c1GridVisualStyle.Splits[0].Rows.Count; i++)
                {
                    c1GridVisualStyle.SelectedRows.Add(i);
                }
            };

            _gMenu.Items[(int)uMenu.ExportToExcel].Click += delegate
            {
                ExportToExcel();
            };

            _gMenu.Items[(int)uMenu.ExportToCsv].Click += delegate
            {
                ArrangeData4AllData("ExportToCSV");
            };

            _gMenu.Items[(int)uMenu.ExportToFile].Click += delegate
            {
                ArrangeData("ExportToFile");
            };

            _gMenu.Items[(int)uMenu.Copy].Click += delegate
            {
                ArrangeData("Copy");
            };

            _gMenu.Items[(int)uMenu.CopyWithColumnNames].Click += delegate
            {
                ArrangeData("CopyWithColumnNames");
            };

            _gMenu.Items[(int)uMenu.CopyColumnNames].Click += delegate
            {
                ArrangeData("CopyColumnNames");
            };

            _gMenu.Items[(int)uMenu.FreezeColumn].Click += delegate
            {
                FrozenColumn();
            };

            _gMenu.Items[(int)uMenu.UnfreezeColumn].Click += delegate
            {
                FrozenColumn(false);
            };

            CreateAndGetARInfoTable();
        }

        private void ApplySqlStyler(string sControl = "all")
        {
            SqlStyler.sColorEditorBackground = pnlEditorBackground.Tag.ToString();
            SqlStyler.sColorTextIdentifier = pnlIdentifier.Tag.ToString();
            SqlStyler.sColorComments = pnlComments.Tag.ToString();
            SqlStyler.sColorNumber = pnlNumber.Tag.ToString();
            SqlStyler.sColorString = pnlString.Tag.ToString();
            SqlStyler.sColorCharacter = pnlCharacter.Tag.ToString();
            SqlStyler.sColorOperatorSymbol = pnlOperatorSymbol.Tag.ToString();
            SqlStyler.sColorUserDefinedTablesViews = pnlUserTables.Tag.ToString();
            SqlStyler.sColorUserDefinedFunctionsTirggers = pnlUserFunctions.Tag.ToString();
            SqlStyler.sColorOperatorKeywords = pnlOperatorKeywords.Tag.ToString();
            SqlStyler.sColorBuiltInFunctions = pnlBuiltInFunctions.Tag.ToString();
            SqlStyler.sColorBuiltInKeywords = pnlBuiltInKeywords.Tag.ToString();
            SqlStyler.sColorUserDefinedKeywords = pnlUserDefinedKeywords.Tag.ToString();
            SqlStyler.bKeywordFontBold = chkBold.Checked;

            SqlStyler.sKeywordsUserDefinedTables = MyLibrary.sKeywordsUserDefinedTables;
            SqlStyler.sKeywordsUserDefinedViews = MyLibrary.sKeywordsUserDefinedViews;
            SqlStyler.sKeywordsUserDefinedFunctions = MyLibrary.sKeywordsUserDefinedFunctions;
            SqlStyler.sKeywordsUserDefinedTriggers = MyLibrary.sKeywordsUserDefinedTriggers;
            SqlStyler.sKeywordsOperatorKeywords = txtOperatorKeywords.Text;
            SqlStyler.sKeywordsBuiltInFunctions = txtBuiltInFunctions.Text;
            SqlStyler.sKeywordsBuiltInKeywords = txtBuiltInKeywords.Text;
            SqlStyler.sKeywordsUserDefinedKeywords = txtUserDefinedKeywords.Text;

            if (sControl == "all" || sControl == "editor")
            {
                editor.Styler = new SqlStyler(); //sqlstyler()：變更關鍵字、顏色
            }

            if (sControl == "all" || sControl == "editorAR")
            {
                editorAR.Styler = new SqlStyler(); //sqlstyler()：變更關鍵字、顏色
            }

            if (sControl == "all" || sControl == "editorSQLToCode")
            {
                editorSQLToCode.Styler = new SqlStyler();
            }

            if (sControl == "all" || sControl == "editorSQLFormatter")
            {
                editorSQLFormatter.Styler = new SqlStyler();
            }

            if (sControl == "all" || sControl == "editorSQLFormatterPreview")
            {
                editorSQLFormatterPreview.Styler = new SqlStyler();
            }

            if (sControl == "all" || sControl == "editorIndicator")
            {
                editorIndicator.Styler = new SqlStyler();
            }
        }

        private void ApplyDarkStyler()
        {
            SqlStyler.sColorEditorBackground = "#2D2D30";
            SqlStyler.sColorTextIdentifier = "#FFFFFF";
            SqlStyler.sColorComments = "#FFFFFF";
            SqlStyler.sColorNumber = "#FFFFFF";
            SqlStyler.sColorString = "#FFFFFF";
            SqlStyler.sColorCharacter = "#FFFFFF";
            SqlStyler.sColorOperatorSymbol = "#FFFFFF";
            SqlStyler.sColorUserDefinedTablesViews = "#FFFFFF";
            SqlStyler.sColorUserDefinedFunctionsTirggers = "#FFFFFF";
            SqlStyler.sColorOperatorKeywords = "#FFFFFF";
            SqlStyler.sColorBuiltInFunctions = "#FFFFFF";
            SqlStyler.sColorBuiltInKeywords = "#FFFFFF";
            SqlStyler.sColorUserDefinedKeywords = "#FFFFFF";
            SqlStyler.bKeywordFontBold = false;

            SqlStyler.sKeywordsUserDefinedTables = "";
            SqlStyler.sKeywordsUserDefinedViews = "";
            SqlStyler.sKeywordsUserDefinedFunctions = "";
            SqlStyler.sKeywordsUserDefinedTriggers = "";
            SqlStyler.sKeywordsOperatorKeywords = "";
            SqlStyler.sKeywordsBuiltInFunctions = "";
            SqlStyler.sKeywordsBuiltInKeywords = "";
            SqlStyler.sKeywordsUserDefinedKeywords = "";

            editorAR.Styler = new SqlStyler();
            editorSQLToCodePreview.Styler = new SqlStyler();
            txtOperatorKeywords.Styler = new SqlStyler();
            txtBuiltInFunctions.Styler = new SqlStyler();
            txtBuiltInKeywords.Styler = new SqlStyler();
            txtUserDefinedKeywords.Styler = new SqlStyler();
        }

        private void chkWordWrap_CheckedChanged(object sender, EventArgs e)
        {
            bool bValue;

            if (chkWordWrap.Checked == false)
            {
                bValue = false;
                editor.WrapMode = ScintillaNET.WrapMode.None;
            }
            else
            {
                bValue = true;
                editor.WrapMode = ScintillaNET.WrapMode.Word;
                editor.WrapVisualFlags = (chkStart.Checked ? ScintillaNET.WrapVisualFlags.Start : ScintillaNET.WrapVisualFlags.None) | (chkEnd.Checked ? ScintillaNET.WrapVisualFlags.End : ScintillaNET.WrapVisualFlags.None) | (chkMargin.Checked ? ScintillaNET.WrapVisualFlags.Margin : ScintillaNET.WrapVisualFlags.None);
            }

            chkStart.Enabled = bValue;
            chkEnd.Enabled = bValue;
            chkMargin.Enabled = bValue;

            btnWordWrap.Visible = !chkWordWrap.Checked;
            btnWordWrap2.Visible = chkWordWrap.Checked;
        }

        private void WordWrapFlags_CheckedChanged(object sender, EventArgs e)
        {
            editor.WrapVisualFlags = (chkStart.Checked ? ScintillaNET.WrapVisualFlags.Start : ScintillaNET.WrapVisualFlags.None) | (chkEnd.Checked ? ScintillaNET.WrapVisualFlags.End : ScintillaNET.WrapVisualFlags.None) | (chkMargin.Checked ? ScintillaNET.WrapVisualFlags.Margin : ScintillaNET.WrapVisualFlags.None);
        }

        private void chkBold_CheckedChanged(object sender, EventArgs e)
        {
            ApplySqlStyler("editor"); //chkBold_CheckedChanged
        }

        private void chkCopyAsHTML_CheckedChanged(object sender, EventArgs e)
        {
            //do nothing (透過程式判斷)
        }

        private void chkShowAllCharacters_CheckedChanged(object sender, EventArgs e)
        {
            editor.ViewEol = chkShowAllCharacters.Checked;
            editor.ViewWhitespace = chkShowAllCharacters.Checked ? ScintillaNET.WhitespaceMode.VisibleAlways : ScintillaNET.WhitespaceMode.Invisible;

            btnShowAllCharacters.Visible = !chkShowAllCharacters.Checked;
            btnShowAllCharacters2.Visible = chkShowAllCharacters.Checked;
        }

        private void pnlSelectedClick(object sender, EventArgs e)
        {
            var iX = 19;
            var iY = 43;

            var pnlSelected = sender as Panel;
            _sPanelColorSelectedName = pnlSelected.Name;
            var pt = PointToScreen(pnlSelected.Location);

            switch (_sPanelColorSelectedName)
            {
                case "pnlHighlightForeColor":
                    iX = grpHighlight.Left + 346;
                    iY = grpHighlight.Top + 43;
                    break;
                case "pnlErrorLineBackground":
                case "pnlBookmarkBackground":
                    iX = grpIndicate.Left + 346;
                    iY = grpIndicate.Top + 43;
                    break;
                default:
                    iX = 19;
                    iY = 244;
                    break;
            }

            pt = new Point(pt.X + iX, pt.Y + pnlSelected.Height + iY);  //因為在 GroupBox 內，所以要再微調 (X, Y)

            var f = new ThemeColorPickerWindow(pt, FormBorderStyle.FixedToolWindow, ThemeColorPickerWindow.Action.CloseWindow, ThemeColorPickerWindow.Action.CloseWindow)
            {
                FormBorderStyle = FormBorderStyle.None,
                ActionAfterColorSelected = ThemeColorPickerWindow.Action.CloseWindow
            };

            f.ColorSelected += f_ColorSelected;
            f.Show();
        }

        private void f_ColorSelected(object sender, ColorSelectedArg e)
        {
            for (var i = 0; i < _lstPanelQueryEditorColor.Count; i++)
            {
                if (((Panel)_lstPanelQueryEditorColor[i]).Name != _sPanelColorSelectedName)
                {
                    continue;
                }

                ((Panel)_lstPanelQueryEditorColor[i]).BackColor = e.Color;
                ((Panel)_lstPanelQueryEditorColor[i]).Tag = e.HexColor;
                _toolTip1.SetToolTip((Panel)_lstPanelQueryEditorColor[i], e.HexColor + " " + "(R:" + e.R + ", G:" + e.G + ", B:" + e.B + ")");

                switch (((Panel)_lstPanelQueryEditorColor[i]).Name)
                {
                    case "pnlToolstripBackground":
                        pnlToolstripBackground.Tag = e.HexColor;
                        _cEditorFocused = pnlToolstripBackground.BackColor;
                        break;
                    case "pnlEditorBackground":
                        pnlEditorBackground.Tag = e.HexColor;
                        break;
                    case "pnlCurrentLineBackground":
                        editor.CaretLineBackColor = e.Color;
                        pnlCurrentLineBackground.Tag = e.HexColor;
                        break;
                    case "pnlSelectedTextBackground":
                        editor.SetSelectionBackColor(true, e.Color);
                        pnlSelectedTextBackground.Tag = e.HexColor;
                        break;
                    case "pnlErrorLineBackground":
                        SetSquiggle(false, e.HexColor, 14, 7); //變更 Error Line 顏色時
                        pnlErrorLineBackground.Tag = e.HexColor;
                        break;
                    case "pnlBookmarkBackground":
                        ApplyIndicatorAppearance(e.HexColor); //變更 Bookmark 顏色時
                        pnlBookmarkBackground.Tag = e.HexColor;
                        break;
                    case "pnlComments":
                        pnlComments.Tag = e.HexColor;
                        break;
                    case "pnlIdentifier":
                        pnlIdentifier.Tag = e.HexColor;
                        break;
                    case "pnlNumber":
                        pnlNumber.Tag = e.HexColor;
                        break;
                    case "pnlOperatorSymbol":
                        pnlOperatorSymbol.Tag = e.HexColor;
                        break;
                    case "pnlOperatorKeywords":
                        pnlOperatorKeywords.Tag = e.HexColor;
                        break;
                    case "pnlString":
                        pnlString.Tag = e.HexColor;
                        break;
                    case "pnlCharacter":
                        pnlCharacter.Tag = e.HexColor;
                        break;
                    case "pnlBuiltInFunctions":
                        pnlBuiltInFunctions.Tag = e.HexColor;
                        break;
                    case "pnlBuiltInKeywords":
                        pnlBuiltInKeywords.Tag = e.HexColor;
                        break;
                    case "pnlUserDefinedKeywords":
                        pnlUserDefinedKeywords.Tag = e.HexColor;
                        break;
                    case "pnlWhiteSpace":
                        editor.SetWhitespaceForeColor(true, e.Color);
                        pnlWhiteSpace.Tag = e.HexColor;
                        break;
                    case "pnlUserTables":
                        pnlUserTables.Tag = e.HexColor;
                        break;
                    case "pnlUserFunctions":
                        pnlUserFunctions.Tag = e.HexColor;
                        break;
                    case "pnlHighlightForeColor":
                        pnlHighlightForeColor.Tag = e.HexColor;
                        HighlightPreview(); //選顏色
                        break;
                }

                ApplySqlStyler("editor"); //f_ColorSelected

                break;
            }
        }

        private void HighlightPreview()
        {
            if (string.IsNullOrEmpty(editor.Text) || string.IsNullOrEmpty(cboHighlightStyle.Text) || string.IsNullOrEmpty(cboHighlightOutlineAlpha.Text) || string.IsNullOrEmpty(cboHighlightAlpha.Text))
            {
                return;
            }

            //Indicators 0-7 could be in use by a lexer, so we'll use indicator 8 to highlight words.
            const int num = 8;

            //Remove all uses of our indicator
            editor.IndicatorCurrent = num;
            editor.IndicatorClearRange(0, editor.TextLength);

            //Update indicator appearance
            UpdateHighlightPreview(cboHighlightStyle.Text, num);

            editor.Indicators[num].Under = true;
            editor.Indicators[num].ForeColor = ColorTranslator.FromHtml((pnlHighlightForeColor.Tag ?? String.Empty).ToString());
            editor.Indicators[num].OutlineAlpha = Convert.ToInt16(cboHighlightOutlineAlpha.Text);
            editor.Indicators[num].Alpha = Convert.ToInt16(cboHighlightAlpha.Text);

            //Search the document
            editor.TargetStart = 0;
            editor.TargetEnd = editor.TextLength;
            editor.SearchFlags = ScintillaNET.SearchFlags.None;

            var matches = Regex.Matches(editor.Text, "Employee");

            foreach (Match m in matches)
            {
                // Mark the search results with the current indicator
                editor.IndicatorFillRange(m.Index, "Employee".Length);
            }
        }

        private void UpdateHighlightPreview(string sStyle, int num)
        {
            //Update indicator appearance
            switch (sStyle)
            {
                case "Box":
                    editor.Indicators[num].Style = ScintillaNET.IndicatorStyle.Box;
                    break;
                case "CompositionThick":
                    editor.Indicators[num].Style = ScintillaNET.IndicatorStyle.CompositionThick;
                    break;
                case "Dash":
                    editor.Indicators[num].Style = ScintillaNET.IndicatorStyle.Dash;
                    break;
                case "Diagonal":
                    editor.Indicators[num].Style = ScintillaNET.IndicatorStyle.Diagonal;
                    break;
                //case "Squiggle": //波浪底線：針對 SQL 錯誤時使用！
                //    editor.Indicators[NUM].Style = ScintillaNET.IndicatorStyle.Squiggle;
                //    break;
                case "StraightBox":
                    editor.Indicators[num].Style = ScintillaNET.IndicatorStyle.StraightBox;
                    break;
            }
        }

        private void pnlSelectedGridClick(object sender, EventArgs e)
        {
            int iX;
            int iY;

            var pnlSelected = sender as Panel;
            _sPanelColorSelectedName = pnlSelected.Name;
            var pt = PointToScreen(pnlSelected.Location);

            if (_sPanelColorSelectedName == "pnlNullValueForeColor")
            {
                iX = grpNullValueStyle.Left + 19; iY = grpNullValueStyle.Top + 43;
            }
            else //if ((sPanelColorSelectedName == "pnlGridHighlightForeColor") || (sPanelColorSelectedName == "pnlGridHighlightBackColor"))
            {
                iX = grpDataGridColor.Left + 7; iY = grpDataGridColor.Top + 34;
            }

            pt = new Point(pt.X + iX, pt.Y + pnlSelected.Height + iY);  //因為在 GroupBox 內，所以要再微調 (X, Y)

            var f = new ThemeColorPickerWindow(pt, FormBorderStyle.FixedToolWindow, ThemeColorPickerWindow.Action.CloseWindow, ThemeColorPickerWindow.Action.CloseWindow)
            {
                FormBorderStyle = FormBorderStyle.None,
                ActionAfterColorSelected = ThemeColorPickerWindow.Action.CloseWindow
            };

            f.ColorSelected += f_ColorSelectedGrid;
            f.Show();
        }

        private void f_ColorSelectedGrid(object sender, ColorSelectedArg e)
        {
            for (var i = 0; i < _lstPanelGridColor.Count; i++)
            {
                if (((Panel) _lstPanelGridColor[i]).Name != _sPanelColorSelectedName)
                {
                    continue;
                }

                ((Panel)_lstPanelGridColor[i]).BackColor = e.Color;
                ((Panel)_lstPanelGridColor[i]).Tag = e.HexColor;
                _toolTip1.SetToolTip(((Panel)_lstPanelGridColor[i]), e.HexColor + " " + "(R:" + e.R + ", G:" + e.G + ", B:" + e.B + ")");

                switch (((Panel)_lstPanelGridColor[i]).Name)
                {
                    case "pnlNullValueForeColor":
                        pnlNullValueForeColor.Tag = e.HexColor;
                        CreateVisualStyleInfo(); //f_ColorSelectedGrid, Change Null Value Color
                        break;
                    case "pnlGridHeadingForeColor":
                        pnlGridHeadingForeColor.Tag = e.HexColor;
                        c1GridVisualStyle.HeadingStyle.ForeColor = e.Color;
                        break;
                    case "pnlGridEvenRowForeColor":
                        pnlGridEvenRowForeColor.Tag = e.HexColor;
                        AlternatingRowColorSetting(); //f_ColorSelectedGrid, pnlGridEvenRowForeColor
                        break;
                    case "pnlGridEvenRowBackColor":
                        pnlGridEvenRowBackColor.Tag = e.HexColor;
                        AlternatingRowColorSetting(); //f_ColorSelectedGrid, pnlGridEvenRowBackColor
                        break;
                    case "pnlGridOddRowForeColor":
                        pnlGridOddRowForeColor.Tag = e.HexColor;
                        AlternatingRowColorSetting(); //f_ColorSelectedGrid, pnlGridOddRowForeColor
                        break;
                    case "pnlGridOddRowBackColor":
                        pnlGridOddRowBackColor.Tag = e.HexColor;
                        AlternatingRowColorSetting(); //f_ColorSelectedGrid, pnlGridOddRowBackColor
                        break;
                    case "pnlGridHighlightForeColor":
                        pnlGridHighlightForeColor.Tag = e.HexColor;
                        if (!string.IsNullOrEmpty(cboFindGrid.Text.Trim()))
                        { btnHighlightAllGrid.PerformClick(); }
                        break;
                    case "pnlGridHighlightBackColor":
                        pnlGridHighlightBackColor.Tag = e.HexColor;
                        if (!string.IsNullOrEmpty(cboFindGrid.Text.Trim()))
                        { btnHighlightAllGrid.PerformClick(); }
                        break;
                    case "pnlGridSelectedForeColor":
                        pnlGridSelectedForeColor.Tag = e.HexColor;
                        c1GridVisualStyle.SelectedStyle.ForeColor = e.Color;
                        break;
                    case "pnlGridSelectedBackColor":
                        pnlGridSelectedBackColor.Tag = e.HexColor;
                        c1GridVisualStyle.SelectedStyle.BackColor = e.Color;
                        break;
                }

                break;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CancelApplyAndCloseOptionsForm();

            //20191013 關閉 - 要透過 MainForm 才行, 否則 SubForm 關了, TabControl 沒關！
            //Dispose vs Close 的比較 → https://www.alwaysgetbetter.com/blog/2008/04/04/c-formclose-vs-formdispose/
            //this.Dispose();
            //this.Close();
            TransferValueToMainForm("closeoptionstab`");
        }

        private void CancelApplyAndCloseOptionsForm()
        {
            //使用者可能有調整顏色，但直接按 Close 關閉 (不儲存)，以免新開啟的 SQL Editor 會套用到不相干的設定值

            //Query Editor 頁籤：Editor Color
            MyLibrary.sColorEditorBackground = lblEditorBackground.Tag.ToString();
            MyLibrary.sColorCurrentLineBackground = lblCurrentLineBackground.Tag.ToString();
            MyLibrary.sColorSelectedTextBackground = lblSelectedTextBackground.Tag.ToString();
            MyLibrary.sColorComments = lblComments.Tag.ToString();
            MyLibrary.sColorTextIdentifier = lblIdentifier.Tag.ToString();
            MyLibrary.sColorBuiltInKeywords = lblBuiltInKeywords.Tag.ToString();
            MyLibrary.sColorUserDefinedKeywords = lblUserDefinedKeywords.Tag.ToString();
            MyLibrary.sColorNumber = lblNumber.Tag.ToString();
            MyLibrary.sColorOperatorSymbol = lblOperatorSymbol.Tag.ToString();
            MyLibrary.sColorOperatorKeywords = lblOperatorKeywords.Tag.ToString();
            MyLibrary.sColorString = lblString.Tag.ToString();
            MyLibrary.sColorCharacter = lblCharacter.Tag.ToString();
            MyLibrary.sColorBuiltInFunctions = lblBuiltInFunctions.Tag.ToString();
            MyLibrary.sColorWhiteSpace = lblWhiteSpace.Tag.ToString();
            MyLibrary.sColorUserDefinedTablesViews = lblUserTables.Tag.ToString();
            MyLibrary.sColorUserDefinedFunctionsTirggers = lblUserFunctions.Tag.ToString();

            //Query Editor 頁籤：Highlight (Apply & Close 不變更全域變數的值，故此處不用還原)
            //MyLibrary.sHighlightColorForeColor = lblHighlightColorForeColor.Tag.ToString();
            //MyLibrary.sHighlightColorStyle = lblHighlightColorStyle.Tag.ToString();
            //MyLibrary.sHighlightColorOutlineAlpha = lblHighlightColorOutlineAlpha.Tag.ToString();
            //MyLibrary.sHighlightColorAlpha = lblHighlightColorAlpha.Tag.ToString();

            //Query Editor 頁籤：Preferences
            MyLibrary.sQueryEditorFontName = lblEditorFontName.Tag.ToString();
            MyLibrary.sQueryEditorFontSize = lblEditorFontSize.Tag.ToString();
            MyLibrary.sQueryEditorZoom = lblEditorZoom.Tag.ToString();
            MyLibrary.bKeywordFontBold = chkBold.Tag.ToString() == "1";
            MyLibrary.bCopyAsHTML = chkCopyAsHTML.Tag.ToString() == "1";
            MyLibrary.bShowAllCharacters = chkShowAllCharacters.Tag.ToString() == "1";
            MyLibrary.bEntireBlankRowAsEmptyRow = chkEntireBlankRowAsEmptyRow4SelectBlock.Tag.ToString() == "1";
            MyLibrary.bHighlightSelection = chkHighlightSelection.Tag.ToString() == "1";

            //Auto Replace 頁籤：直接關閉，不需任何動作 (重啟程式才會生效)

            //Data Grid 頁籤：直接關閉，不需任何動作 (重啟程式才會生效)

            //Keywords 頁籤：直接關閉，不需任何動作 (重啟程式才會生效)

            //SQL To Code 頁籤：
            MyLibrary.sSQLToCodeVariableName = txtVariableName.Tag.ToString();

            if (cboLocalization.Tag.ToString() == cboLocalization.Text)
            {
                return;
            }

            MyGlobal.sLocalization = cboLocalization.Tag.ToString();
            TransferValueToMainForm("reloadlocalization`");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //取得顏色的 HTML 代碼
            var mycolor = Color.YellowGreen; //白色=#FFFFFF, 黑色=#000000, DarkGray=#A9A9A9, SlateGray=#708090, GreenYellow=#ADFF2F, YellowGreen=#9ACD32
            //Color mycolor = SystemColors.Window;
            var strcol = ColorTranslator.ToHtml(Color.FromArgb(mycolor.ToArgb()));

            #region demo
            //////ToolStripMenuItem tsMenuItem40 = new ToolStripMenuItem();

            //////tsMenuItem40.Name = "tsMenuItem40";
            //////tsMenuItem40.Size = new Size(152, 22);
            //////tsMenuItem40.Text = "Test40";
            //////tsMenuItem40.Click += new EventHandler(tsMenuItem40_Click);

            //////ToolStripMenuItem tsProdMenuItem11 = new ToolStripMenuItem();

            //////tsProdMenuItem11.Name = "tsProdMenuItem11";
            //////tsProdMenuItem11.Size = new Size(152, 22);
            //////tsProdMenuItem11.Text = "Prod11";
            //////tsProdMenuItem11.Click += new EventHandler(tsProdMenuItem11_Click);

            //////tsCopyFrom.DropDownItems.AddRange(new ToolStripItem[] { tsMenuItem40, tsProdMenuItem11 });
            #endregion

            btnCopySettings.Items.Clear();

            var sSql = "SELECT * FROM DBInfo WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND PID <> " + MyGlobal.sDBMotherPID;
            var dtTemp = DBCommon.ExecQuery(sSql);

            if (dtTemp.Rows.Count <= 0)
            {
                return;
            }

            pnlCopySettings.Visible = true;

            for (var i = 0; i < dtTemp.Rows.Count; i++)
            {
                //dtTemp.Rows[i][""].ToString();
                var dropDownItem = new C1.Win.C1Input.DropDownItem();
                btnCopySettings.Items.Add(dropDownItem);
                btnCopySettings.Items[i].Tag = "123";
                btnCopySettings.Items[i].Text = "From \"(" + dtTemp.Rows[i]["DataSource"] + @")" + dtTemp.Rows[i]["ConnectionName"] + "\" To \"(" + MyGlobal.sDataSource + @")" + MyGlobal.sDBConnectionName + "\"";
                btnCopySettings.DropDownItemClicked += btnCopySettings_DropDownItemClicked;

                //以下是 VS 的 DropDownItems 使用範例
                //tsCopyFrom.DropDownItems.Add("tsMenuItem" + i.ToString());
                //tsCopyFrom.DropDownItems[i].AccessibleName = "from";
                //tsCopyFrom.DropDownItems[i].AccessibleDescription = "to";
                //tsCopyFrom.DropDownItems[i].AccessibleDefaultActionDescription = "";
                //tsCopyFrom.DropDownItems[i].Text = "Copy settings from \"" + dtTemp.Rows[i]["ConnectionName"].ToString() + "\" to \"" + MyGlobal.sDBConnectionName + "\"";
                //tsCopyFrom.DropDownItems[i].Click += new EventHandler(tsMenuItem_Click);
            }

            //tsCopyFrom.DropDownItems.Add("tsProdMenuItem11");
            ////tsCopyFrom.DropDownItems[1].Size = new Size(152, 22);
            //tsCopyFrom.DropDownItems[1].Text = "Copy settings from \"Prod11\" to \"那個\"";
            //tsCopyFrom.DropDownItems[1].Click += new EventHandler(tsMenuItem_Click);

            //editor.WrapVisualFlags = (chkStart.Checked ? ScintillaNET.WrapVisualFlags.Start : ScintillaNET.WrapVisualFlags.None) | (chkEnd.Checked ? ScintillaNET.WrapVisualFlags.End : ScintillaNET.WrapVisualFlags.None) | (chkMargin.Checked ? ScintillaNET.WrapVisualFlags.Margin : ScintillaNET.WrapVisualFlags.None);
        }

        private void btnCopySettings_DropDownItemClicked(object sender, C1.Win.C1Input.DropDownItemClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.ClickedItem.Text))
            {
                return;
            }

            _sLangText = MyGlobal.GetLanguageString("Are you sure you want to copy all settings?", "Global", "Global", "msg", "CopySettingsFrom", "Text");

            var parts = e.ClickedItem.Text.Split(new[] { " " }, StringSplitOptions.None);
            var sTemp = parts.Aggregate("", (current, t) => current + t + "\r\n");

            _sLangText += "\r\n\r\n" + sTemp;
            var result = MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            //刪除本身的設定
            DBCommon.ExecNonQuery("Delete From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey<>'GlobalConfig'");

            //複製選定的 Profile 設定
            var sSql = "Insert Into SystemConfig (MPID, DomainUser, AttributeKey, AttributeName, AttributeValue, AttributeDate, AttributeText, AttributeText2)\r\n" +
                       "Select " + MyGlobal.sDBMotherPID + " as MPID, DomainUser, AttributeKey, AttributeName, AttributeValue, AttributeDate, AttributeText, AttributeText2 From SystemConfig Where DomainUser = '" + MyGlobal.sDomainUser + "' And MPID = " + e.ClickedItem.Tag.ToString() + " And[AttributeKey] <> 'GlobalConfig';";
            DBCommon.ExecNonQuery(sSql);

            _sLangText = MyGlobal.GetLanguageString("The settings have been copied. Please restart JasonQuery!", "Global", "Global", "msg", "CopyOK", "Text");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MyGlobal.sRequireToRestart = MyGlobal.GetLanguageString("All settings have been changed.", "Global", "Global", "msg", "SettingsChanged", "Text") + "\r\n" + MyGlobal.GetLanguageString("You need to restart JasonQuery!", "Global", "Global", "msg", "RequireToRestart", "Text");
            btnApply.Enabled = false;
            btnRestoreDefaults.Enabled = false;
            btnCopySettings.Enabled = false;
        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            _sLangText = MyGlobal.GetLanguageString("Are you sure you want to restore all default settings except \"Global\"? ", "Global", "Global", "msg", "RestoreDefault", "Text");
            var result = MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            DBCommon.ExecNonQuery("Delete From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey<>'GlobalConfig'");
            btnApply.Enabled = false;
            pnlCopySettings.Enabled = false;

            _sLangText = MyGlobal.GetLanguageString("The settings have been restored. Please restart JasonQuery!", "Global", "Global", "msg", "RestoreOK", "Text");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MyGlobal.sRequireToRestart = MyGlobal.GetLanguageString("All settings have been changed.", "Global", "Global", "msg", "SettingsChanged", "Text") + "\r\n" + MyGlobal.GetLanguageString("You need to restart JasonQuery!", "Global", "Global", "msg", "RequireToRestart", "Text");
            btnApply.Enabled = false;
            btnRestoreDefaults.Enabled = false;
            btnCopySettings.Enabled = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var bHandled = false;
            var iKey = 0;

            switch (keyData)
            {
                case Keys.Control | Keys.F: //Ctrl+F
                    if (c1DockingTab.SelectedTab == tabKeywords)
                    {
                        if (txtOperatorKeywords.Focused || grpFindOperatorKeywords.Visible)
                        {
                            iKey = 0;
                        }
                        else if (txtBuiltInFunctions.Focused || grpFindBuiltInFunctions.Visible)
                        {
                            iKey = 1;
                        }
                        else if (txtBuiltInKeywords.Focused || grpFindBuiltInKeywords.Visible)
                        {
                            iKey = 2;
                        }
                        else if (txtUserDefinedKeywords.Focused || grpFindUserDefinedKeywords.Visible)
                        {
                            iKey = 3;
                        }

                        FindKeywords(iKey, true);
                    }

                    bHandled = true;
                    break;

                case Keys.Control | Keys.M: //Ctrl+M
                    if (c1DockingTab.SelectedTab == tabDataGrid && c1GridVisualStyle.Focused)
                    {
                        c1GridVisualStyle.Row = 0;
                        c1GridVisualStyle.Col = 6;
                        c1GridVisualStyle.Select(); //Focus 切換到指定的 Cell
                    }

                    break;

                case Keys.Control | Keys.A: //Ctrl+A
                    if (c1DockingTab.SelectedTab == tabDataGrid && c1GridVisualStyle.Focused)
                    {
                        c1GridVisualStyle.SelectedRows.Clear();

                        for (var i = 0; i < c1GridVisualStyle.Splits[0].Rows.Count; i++)
                        {
                            c1GridVisualStyle.SelectedRows.Add(i);
                        }
                    }

                    break;

                case Keys.Control | Keys.C: //Ctrl+C
                    if (c1DockingTab.SelectedTab == tabAutoReplace)
                    {
                        if (txtKeyword.Focused)
                        {
                            txtKeyword.Copy();
                        }

                        if (editorAR.Focused)
                        {
                            editorAR.Copy();
                        }
                    }
                    else if (c1DockingTab.SelectedTab == tabQueryEditor && editor.Focused)
                    {
                        if (chkCopyAsHTML.Checked)
                        {
                            editor.Copy(ScintillaNET.CopyFormat.Text | ScintillaNET.CopyFormat.Rtf | ScintillaNET.CopyFormat.Html);
                        }
                        else
                        {
                            editor.Copy();
                        }
                    }
                    else if (c1DockingTab.SelectedTab == tabDataGrid)
                    {
                        if (c1GridVisualStyle.Focused)
                        {
                            ArrangeData("COPY");
                        }
                    }

                    break;

                case Keys.Control | Keys.N: //Ctrl+N
                    TransferValueToMainForm("createnewtab`");
                    bHandled = true;
                    break;

                case Keys.Control | Keys.O: //Ctrl+O
                    //20191005 按快速鍵，改為「在新的頁籤開啟」
                    TransferValueToMainForm("createnewtab`OPENFILE");
                    bHandled = true;
                    break;
            }

            return bHandled;
        }

        private void editor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            editor.ContextMenuStrip = _cMenu;

            if (MyLibrary.bDarkMode)
            {
                _cMenu.BackColor = ColorTranslator.FromHtml("#2D2D30");
                _cMenu.ForeColor = Color.White;
                _cMenu.RenderMode = ToolStripRenderMode.System;
                //_cMenu.ShowImageMargin = false;
            }

            _cMenu.Show(editor, new Point(e.X, e.Y));
        }

        private void cboWordWrapIndentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bFormLoadFinish == false)
            {
                return;
            }

            var sMode = MyGlobal.GetKeyFromDictionary(MyGlobal.dicWordWrapIndentMode, cboIndentMode.Text);

            switch (sMode)
            {
                case "Fixed":
                    editor.WrapIndentMode = ScintillaNET.WrapIndentMode.Fixed;
                    break;
                case "Indent":
                    editor.WrapIndentMode = ScintillaNET.WrapIndentMode.Indent;
                    break;
                default:
                    editor.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
                    break;
            }
        }

        private void editor_ZoomChanged(object sender, EventArgs e)
        {
            //以下，當放大縮小後，即時調整 line number 的寬度，避免因為放大時，line number 最左側的數字會看不見
            var iStart = editor.SelectionStart;
            editor.Text += "\r\n";
            editor.Text = editor.Text.Substring(0, editor.Text.Length - 2);
            editor.SelectionStart = iStart;
            editor.ScrollCaret();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            btnRestoreDefaults.Enabled = false;
            btnCopySettings.Enabled = false;
            btnApply.Enabled = false;
            btnClose.Enabled = false;
            
            if (MyLibrary.bDarkMode == false && chkDarkMode.Checked)
            {
                pnlGridHeadingForeColor.Tag = "#FFFFFF"; //White
                pnlGridEvenRowForeColor.Tag = "#FFFFFF"; //White
                pnlGridEvenRowBackColor.Tag = "#0F243E"; //Dark
                pnlGridOddRowForeColor.Tag = "#FFFFFF"; //White
                pnlGridOddRowBackColor.Tag = "#262626"; //Dark
                pnlNullValueForeColor.Tag = "#FFFF00"; //FFFF00=Yellow, 006CBA

                pnlOptionsTabActiveForeColor.Tag = "#000000";
                pnlOptionsTabActiveBackColor.Tag = "#E6FFFF";
                pnlOptionsTabInactiveForeColor.Tag = "#A5A5A5"; //深色模式，這個調亮一點點

                pnlToolstripBackground.Tag = "#3F3F3F";
                pnlEditorBackground.Tag = "#2D2D30";
                pnlCurrentLineBackground.Tag = "#7F7F7F"; //舊的是 #3F3F3F
                pnlSelectedTextBackground.Tag = "#639ACE";
                pnlComments.Tag = "#00FF00";
                pnlIdentifier.Tag = "#FFFFFF";
                pnlNumber.Tag = "#FF00FF";
                pnlOperatorSymbol.Tag = "#FF00FF";
                pnlOperatorKeywords.Tag = "#00FFFF";
                pnlString.Tag = "#FFFF00";
                pnlCharacter.Tag = "#FFFF00";
                pnlBuiltInFunctions.Tag = "#FFC000";
                pnlBuiltInKeywords.Tag = "#00FFFF";
                pnlUserDefinedKeywords.Tag = "#00FFFF";
                pnlWhiteSpace.Tag = "#7030A0";
                pnlHighlightForeColor.Tag = "#FFFF00";
                pnlUserTables.Tag = "#9BBB59";
                pnlUserFunctions.Tag = "#9BBB59";

                cboGridVisualStyle.Text = @"Office 2010 Black";
            }
            else if (MyLibrary.bDarkMode && chkDarkMode.Checked == false)
            {
                pnlGridHeadingForeColor.Tag = "#000000";
                pnlGridEvenRowForeColor.Tag = "#000000";
                pnlGridEvenRowBackColor.Tag = "#FFFFFF";
                pnlGridOddRowForeColor.Tag = "#000000";
                pnlGridOddRowBackColor.Tag = "#FFFFC1";
                pnlNullValueForeColor.Tag = "#0000FF";

                pnlOptionsTabActiveForeColor.Tag = "#000000";
                pnlOptionsTabActiveBackColor.Tag = "#E6FFFF";
                pnlOptionsTabInactiveForeColor.Tag = "#595959";

                pnlToolstripBackground.Tag = "#E3FDCA";
                pnlEditorBackground.Tag = "#FFFFFF";
                pnlCurrentLineBackground.Tag = "#FFFFE0";
                pnlSelectedTextBackground.Tag = "#ADD8E6";
                pnlComments.Tag = "#008000";
                pnlIdentifier.Tag = "#000000";
                pnlNumber.Tag = "#800000";
                pnlOperatorSymbol.Tag = "#800000";
                pnlOperatorKeywords.Tag = "#366092";
                pnlString.Tag = "#FF0000";
                pnlCharacter.Tag = "#FF0000";
                pnlBuiltInFunctions.Tag = "#FF00FF";
                pnlBuiltInKeywords.Tag = "#0000FF";
                pnlUserDefinedKeywords.Tag = "#0000FF";
                pnlWhiteSpace.Tag = "#00FFFF";
                pnlHighlightForeColor.Tag = "#000000";
                pnlUserTables.Tag = "#808000";
                pnlUserFunctions.Tag = "#808000";

                cboGridVisualStyle.Text = @"Office 2010 Blue";
            }

            Application.DoEvents();
            _bApplyAndClose = true;

            //儲存設定：General
            #region 儲存設定：General
            MyGlobal.UpdateSetting("GeneralConfig", "DarkMode", chkDarkMode.Checked ? "1" : "0");
            MyLibrary.bDarkMode = chkDarkMode.Checked;

            var sTemp = MyGlobal.GetKeyFromDictionary(MyGlobal.dicAutoDisconnect, cboAutoDisconnect.Text);
            MyGlobal.UpdateSetting("GeneralConfig", "AutoDisconnect", sTemp);
            MyLibrary.sAutoDisconnect = sTemp;
            MyGlobal.sAutoDisconnect = MyGlobal.GetValueFromDictionary(MyGlobal.dicAutoDisconnect, MyLibrary.sAutoDisconnect);

            if (MyLibrary.sAutoDisconnect == "Never")
            {
                MyGlobal.iAutoDisconnect = 360 * 60 * 60 * 1000; //從選項變更，改成 360 小時不自動「中斷連線」(正確作法應該是要通知 MainForm disable timer)
            }
            else
            {
                MyGlobal.iAutoDisconnect = Convert.ToInt16(MyLibrary.sAutoDisconnect.Substring(0, 1)) * 60 * 60 * 1000;
            }

            MyGlobal.UpdateSetting("GeneralConfig", "SpecifiedSQLFile1", txtSpecifiedSQLFile1.Text);
            MyGlobal.UpdateSetting("GeneralConfig", "SpecifiedSQLFile2", txtSpecifiedSQLFile2.Text);
            #endregion

            //儲存設定：Query Editor
            #region 儲存設定：Query Editor
            MyGlobal.UpdateSetting("EditorConfig", "ToolstripBackground", pnlToolstripBackground.Tag.ToString()); //Editor Background
            MyLibrary.sColorToolstripBackground = pnlToolstripBackground.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "EditorBackground", pnlEditorBackground.Tag.ToString()); //Editor Background
            MyLibrary.sColorEditorBackground = pnlEditorBackground.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "CurrentLineBackground", pnlCurrentLineBackground.Tag.ToString()); //Current Line Background
            MyLibrary.sColorCurrentLineBackground = pnlCurrentLineBackground.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "SelectedTextBackground", pnlSelectedTextBackground.Tag.ToString()); //Selected Text Background
            MyLibrary.sColorSelectedTextBackground = pnlSelectedTextBackground.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "ErrorLineBackground", pnlErrorLineBackground.Tag.ToString()); //Error Line Background
            MyGlobal.UpdateSetting("EditorConfig", "BookmarkBackground", pnlBookmarkBackground.Tag.ToString()); //Bookmark Background

            MyGlobal.UpdateSetting("EditorConfig", "BookmarkStyle", MyGlobal.GetKeyFromDictionary(MyGlobal.dicWordWrapIndentMode, cboBookmarkStyle.Text)); //Bookmark Style

            MyGlobal.UpdateSetting("EditorConfig", "Comments", pnlComments.Tag.ToString()); //Comments
            MyLibrary.sColorComments = pnlComments.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "TextIdentifier", pnlIdentifier.Tag.ToString()); //Text (Identifier)
            MyLibrary.sColorTextIdentifier = pnlIdentifier.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "BuiltInKeywords", pnlBuiltInKeywords.Tag.ToString()); //Built-in Keywords
            MyLibrary.sColorBuiltInKeywords = pnlBuiltInKeywords.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "UserDefinedKeywords", pnlUserDefinedKeywords.Tag.ToString()); //User-defined Keywords
            MyLibrary.sColorUserDefinedKeywords = pnlUserDefinedKeywords.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "Number", pnlNumber.Tag.ToString()); //Number
            MyLibrary.sColorNumber = pnlNumber.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "OperatorSymbol", pnlOperatorSymbol.Tag.ToString()); //Operator (Symbol)
            MyLibrary.sColorOperatorSymbol = pnlOperatorSymbol.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "OperatorKeywords", pnlOperatorKeywords.Tag.ToString()); //Operator (Keywords)
            MyLibrary.sColorOperatorKeywords = pnlOperatorKeywords.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "String", pnlString.Tag.ToString()); //String (Double Quoted)
            MyLibrary.sColorString = pnlString.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "Character", pnlCharacter.Tag.ToString()); //Character (Single Quoted)
            MyLibrary.sColorCharacter = pnlCharacter.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "BuiltinFunctions", pnlBuiltInFunctions.Tag.ToString()); //Built-in Functions
            MyLibrary.sColorBuiltInFunctions = pnlBuiltInFunctions.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "BuiltInKeywords", pnlBuiltInKeywords.Tag.ToString()); //Built-in Keywords
            MyLibrary.sColorBuiltInKeywords = pnlBuiltInKeywords.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "WhiteSpace", pnlWhiteSpace.Tag.ToString()); //White Space
            MyLibrary.sColorWhiteSpace = pnlWhiteSpace.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "UserDefinedTables", pnlUserTables.Tag.ToString()); //User-defined Table
            MyLibrary.sColorUserDefinedTablesViews = pnlUserTables.Tag.ToString();

            MyGlobal.UpdateSetting("EditorConfig", "UserDefinedFunctions", pnlUserFunctions.Tag.ToString()); //User-defined Function
            MyLibrary.sColorUserDefinedFunctionsTirggers = pnlUserFunctions.Tag.ToString();

            //Query Editor 頁籤：Highlight
            //20191016 Highlight 不要動態變更，故不變更全域變數的值
            MyGlobal.UpdateSetting("EditorConfig", "HighlightForeColor", pnlHighlightForeColor.Tag.ToString());
            MyGlobal.UpdateSetting("EditorConfig", "HighlightStyle", cboHighlightStyle.Text);
            MyGlobal.UpdateSetting("EditorConfig", "HighlightOutlineAlpha", cboHighlightOutlineAlpha.Text);
            MyGlobal.UpdateSetting("EditorConfig", "HighlightAlpha", cboHighlightAlpha.Text);

            //Query Editor 頁籤：Preferences
            MyGlobal.UpdateSetting("EditorConfig", "EditorFontName", cboEditorFontPicker.Text);
            MyLibrary.sQueryEditorFontName = cboEditorFontPicker.Text;

            MyGlobal.UpdateSetting("EditorConfig", "EditorFontSize", cboEditorFontSize.Text);
            MyLibrary.sQueryEditorFontSize = cboEditorFontSize.Text;

            MyGlobal.UpdateSetting("EditorConfig", "EditorZoom", cboEditorZoom.Text);
            MyLibrary.sQueryEditorZoom = cboEditorZoom.Text;

            MyGlobal.UpdateSetting("EditorConfig", "WordWrap", chkWordWrap.Checked ? "1" : "0");
            MyLibrary.bWordWrap = chkWordWrap.Checked;

            MyGlobal.UpdateSetting("EditorConfig", "WordWrapVisualFlags_Start", chkStart.Checked ? "1" : "0");
            MyLibrary.bWordWrapVisualFlags_Start = chkStart.Checked;

            MyGlobal.UpdateSetting("EditorConfig", "WordWrapVisualFlags_End", chkEnd.Checked ? "1" : "0");
            MyLibrary.bWordWrapVisualFlags_End = chkEnd.Checked;

            MyGlobal.UpdateSetting("EditorConfig", "WordWrapVisualFlags_Margin", chkMargin.Checked ? "1" : "0");
            MyLibrary.bWordWrapVisualFlags_Margin = chkMargin.Checked;

            MyGlobal.UpdateSetting("EditorConfig", "WordWrapIndentMode", cboIndentMode.Text);
            MyGlobal.sWordWrapIndentMode = MyGlobal.GetKeyFromDictionary(MyGlobal.dicWordWrapIndentMode, cboIndentMode.Text);

            MyGlobal.UpdateSetting("EditorConfig", "TabWidth", cboTabWidth.Text);
            MyGlobal.iTabWidth = Convert.ToInt16(cboTabWidth.Text);

            MyGlobal.UpdateSetting("EditorConfig", "KeywordFontBold", chkBold.Checked ? "1" : "0");
            MyLibrary.bKeywordFontBold = chkBold.Checked;

            MyGlobal.UpdateSetting("EditorConfig", "CopyAsHTML", chkCopyAsHTML.Checked ? "1" : "0");
            MyLibrary.bCopyAsHTML = chkCopyAsHTML.Checked;

            MyGlobal.UpdateSetting("EditorConfig", "ShowAllCharacters", chkShowAllCharacters.Checked ? "1" : "0");
            MyLibrary.bShowAllCharacters = chkShowAllCharacters.Checked;

            MyGlobal.UpdateSetting("EditorConfig", "ShowSaveAsButton", chkShowSaveAsButton.Checked ? "1" : "0");
            MyLibrary.bShowSaveAsButton = chkShowSaveAsButton.Checked;

            MyGlobal.UpdateSetting("EditorConfig", "ShowIndentGuide", chkShowIndentGuide.Checked ? "1" : "0");
            MyLibrary.bShowSaveAsButton = chkShowIndentGuide.Checked;

            MyGlobal.UpdateSetting("EditorConfig", "EntireBlankRowAsEmptyRow4SelectBlock", chkEntireBlankRowAsEmptyRow4SelectBlock.Checked ? "1" : "0");
            MyLibrary.bEntireBlankRowAsEmptyRow = chkEntireBlankRowAsEmptyRow4SelectBlock.Checked;

            MyGlobal.UpdateSetting("EditorConfig", "HighlightSelection", chkHighlightSelection.Checked ? "1" : "0");
            MyLibrary.bHighlightSelection = chkHighlightSelection.Checked;

            MyGlobal.UpdateSetting("EditorConfig", "SortByColumnName", chkSortByColumnName.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("EditorConfig", "ShowColumnInfo", chkShowColumnInfo.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("EditorConfig", "DefaultTabSchemaBrowser", chkDefaultTabSchemaBrowser.Checked ? "1" : "0");

            MyGlobal.UpdateSetting("EditorConfig", "AutoListMembers", chkAutoListMembers.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("EditorConfig", "SavePoint", chkSavePoint.Checked ? "1" : "0");
            #endregion

            Application.DoEvents();

            //儲存設定：Auto Complete
            #region 儲存設定：Auto Complete
            MyGlobal.UpdateSetting("AutoCompleteConfig", "EnableAutoComplete2", chkEnableAutoComplete.Checked ? "1" : "0");
            MyLibrary.bEnableAutoComplete = chkEnableAutoComplete.Checked;

            MyGlobal.UpdateSetting("AutoCompleteConfig", "MinFragmentLength2", nudMinFragmentLength.Text);
            MyLibrary.iACMinFragmentLength = (int)nudMinFragmentLength.Value;

            MyGlobal.UpdateSetting("AutoCompleteConfig", "FirstCharChecking2", chkFirstCharChecking.Checked ? "1" : "0");
            MyLibrary.bACFirstCharChecking = chkFirstCharChecking.Checked;

            //Built-In Keywords && Functions
            MyGlobal.UpdateSetting("AutoCompleteConfig", "BuiltInKeywords2", chkBuiltInKeywords.Checked ? "1" : "0");
            MyLibrary.bACBuiltInKeywords = chkBuiltInKeywords.Checked;

            MyGlobal.UpdateSetting("AutoCompleteConfig", "BuiltInFunctions2", chkBuiltInFunctions.Checked ? "1" : "0");
            MyLibrary.bACBuiltInFunctions = chkBuiltInFunctions.Checked;

            MyGlobal.UpdateSetting("AutoCompleteConfig", "UserDefinedKeywords2", chkUserDefinedKeywords.Checked ? "1" : "0");
            MyLibrary.bACUserDefinedKeywords = chkUserDefinedKeywords.Checked;

            MyGlobal.UpdateSetting("AutoCompleteConfig", "UserDefinedFunctions2", chkUserDefinedFunctions.Checked ? "1" : "0");
            MyLibrary.bACUserDefinedFunctions = chkUserDefinedFunctions.Checked;

            MyGlobal.UpdateSetting("AutoCompleteConfig", "UserDefinedTables2", chkUserDefinedTables.Checked ? "1" : "0");
            MyLibrary.bACUserDefinedTables = chkUserDefinedTables.Checked;

            MyGlobal.UpdateSetting("AutoCompleteConfig", "UserDefinedTriggers2", chkUserDefinedTriggers.Checked ? "1" : "0");
            MyLibrary.bACUserDefinedTriggers = chkUserDefinedTriggers.Checked;

            MyGlobal.UpdateSetting("AutoCompleteConfig", "UserDefinedViews2", chkUserDefinedViews.Checked ? "1" : "0");
            MyLibrary.bACUserDefinedViews = chkUserDefinedViews.Checked;
            #endregion

            Application.DoEvents();

            //儲存設定：Auto Replace
            #region 儲存設定：Auto Replace
            MyGlobal.UpdateSetting("AutoReplaceConfig", "EnableAutoReplace", chkEnableAutoReplace.Checked ? "1" : "0");
            MyLibrary.bEnableAutoReplace = chkEnableAutoReplace.Checked;

            //刪除所有的 Auto Replace，再重新 Insert/Update
            DBCommon.ExecNonQuery("DELETE FROM SystemConfig WHERE DomainUser = '" + MyGlobal.sDomainUser + "' AND MPID = " + MyGlobal.sDBMotherPID + " AND AttributeKey = 'AutoReplaceConfig' AND AttributeName = 'AutoReplace'");

            foreach (DataRow oRow in _dtARInfo.Rows)
            {
                sTemp = oRow[_lstGridHeaderAr[(int)eColAR.Keyword]] + MyGlobal.sSeparator3s + oRow[_lstGridHeaderAr[(int)eColAR.Replacement]];
                MyGlobal.UpdateSetting("AutoReplaceConfig", "AutoReplace", sTemp, true);
            }
            #endregion

            Application.DoEvents();

            //儲存設定：Data Grid (重新啟動，設定才會生效)
            #region 儲存設定：Data Grid
            MyGlobal.UpdateSetting("GridConfig", "QuotingWith", cboResultCopyQuotingWith.Text); //MyLibrary.sGridQuotingWith
            MyGlobal.UpdateSetting("GridConfig", "FieldSeparator", cboResultCopyFieldSeparator.Text); //MyLibrary.sGridFieldSeparator
            MyGlobal.UpdateSetting("GridConfig", "ShowColumnDataType", chkShowColumnType.Checked ? "1" : "0"); //MyLibrary.bGridShowColumnDataType
            MyGlobal.UpdateSetting("GridConfig", "ShowStreamlinedName", (chkShowStreamlinedName.Checked && MyGlobal.sDataSource == "PostgreSQL") ? "1" : "0"); //MyLibrary.bGridShowStreamlinedName
            MyGlobal.UpdateSetting("GridConfig", "ShowFilterRow", chkShowFilterRow.Checked ? "1" : "0"); //MyLibrary.bGridShowFilterRow
            MyGlobal.UpdateSetting("GridConfig", "ShowGroupingRow", chkShowGroupingRow.Checked ? "1" : "0"); //MyLibrary.bGridShowGroupingRow
            MyGlobal.UpdateSetting("GridConfig", "Resize", chkResize.Checked ? "1" : "0"); //MyLibrary.bGridResize
            MyGlobal.UpdateSetting("GridConfig", "MaxWidth", MyGlobal.GetKeyFromDictionary(MyGlobal.dicMaxWidth, cboMaxWidth.Text)); //MyGlobal.sGridMaxWidth
            MyGlobal.UpdateSetting("GridConfig", "PreviewCLOBData", chkPreviewCLOBData.Checked ? "1" : "0"); //MyGlobal.bPreviewCLOBData
            MyGlobal.UpdateSetting("GridConfig", "Sort", chkSort.Checked ? "1" : "0"); //MyLibrary.bGridSort
            MyGlobal.UpdateSetting("GridConfig", "RawDataMode", chkRawDataMode.Checked ? "1" : "0"); //MyLibrary.bGridRawDataMode
            MyGlobal.UpdateSetting("GridConfig", "NullShowAs", cboNullShowAs.Text); //MyLibrary.sGridNullShowAs
            MyGlobal.UpdateSetting("GridConfig", "PagingQuery", chkPagingQuery.Checked ? "1" : "0"); //MyLibrary.bGridPagingQuery
            MyGlobal.UpdateSetting("GridConfig", "RowsPerPage", cboRowsPerPage.Text); //MyLibrary.sGridRowsPerPage
            MyGlobal.UpdateSetting("GridConfig", "AppendingQueries", chkAppendingQueries.Checked ? "1" : "0"); //MyLibrary.bGridAppendingQueries
            MyGlobal.UpdateSetting("GridConfig", "SetFocusAfterQuery", chkSetFocusAfterQuery.Checked ? "1" : "0"); //MyLibrary.bGridSetFocusAfterQuery
            MyGlobal.UpdateSetting("GridConfig", "NullShowColor", pnlNullValueForeColor.Tag.ToString()); //MyLibrary.sGridNullShowColor
            MyGlobal.UpdateSetting("GridConfig", "VisualStyle", cboGridVisualStyle.Text); //MyLibrary.sGridVisualStyle
            MyGlobal.UpdateSetting("GridConfig", "Zoom", cboGridZoom.Text); //MyLibrary.sGridZoom
            MyGlobal.UpdateSetting("GridConfig", "FontName", cboGridFontPicker.Text); //MyLibrary.sGridFontName
            MyGlobal.UpdateSetting("GridConfig", "FontSize", cboGridFontSize.Text); //MyLibrary.sGridFontSize
            MyGlobal.UpdateSetting("GridConfig", "RowResizing", MyGlobal.GetKeyFromDictionary(MyGlobal.dicRowSizing, cboGridRowHeightResizing.Text));
            MyGlobal.UpdateSetting("GridConfig", "HeadingForeColor", pnlGridHeadingForeColor.Tag.ToString()); //MyLibrary.sGridHeadingForeColor
            MyGlobal.UpdateSetting("GridConfig", "EvenRowForeColor", pnlGridEvenRowForeColor.Tag.ToString()); //MyLibrary.sGridEvenRowForeColor
            MyGlobal.UpdateSetting("GridConfig", "EvenRowBackColor", pnlGridEvenRowBackColor.Tag.ToString()); //MyLibrary.sGridEvenRowBackColor
            MyGlobal.UpdateSetting("GridConfig", "OddRowForeColor", pnlGridOddRowForeColor.Tag.ToString()); //MyLibrary.sGridOddRowForeColor
            MyGlobal.UpdateSetting("GridConfig", "OddRowBackColor", pnlGridOddRowBackColor.Tag.ToString()); //MyLibrary.sGridOddRowBackColor
            MyGlobal.UpdateSetting("GridConfig", "HighlightForeColor", pnlGridHighlightForeColor.Tag.ToString()); //MyLibrary.sGridHighlightForeColor
            MyGlobal.UpdateSetting("GridConfig", "HighlightBackColor", pnlGridHighlightBackColor.Tag.ToString()); //MyLibrary.sGridHighlightBackColor
            MyGlobal.UpdateSetting("GridConfig", "SelectedForeColor", pnlGridSelectedForeColor.Tag.ToString()); //MyLibrary.sGridSelectedForeColor
            MyGlobal.UpdateSetting("GridConfig", "SelectedBackColor", pnlGridSelectedBackColor.Tag.ToString()); //MyLibrary.sGridSelectedBackColor
            #endregion

            Application.DoEvents();

            //儲存設定：Keywords
            #region 儲存設定：Keywords
            sTemp = string.IsNullOrEmpty(txtOperatorKeywords.Text.Trim()) ? "" : txtOperatorKeywords.Text.Trim().ToLower() + " ";
            MyGlobal.UpdateSetting("KeywordsConfig", "OperatorKeywords", sTemp, false, true);
            MyLibrary.sKeywordsOperatorKeywords = txtOperatorKeywords.Text;

            sTemp = string.IsNullOrEmpty(txtBuiltInFunctions.Text.Trim()) ? "" : txtBuiltInFunctions.Text.Trim().ToLower() + " ";
            MyGlobal.UpdateSetting("KeywordsConfig", "BuiltInFunctions", sTemp, false, true);
            MyLibrary.sKeywordsBuiltInFunctions = txtBuiltInFunctions.Text;

            sTemp = string.IsNullOrEmpty(txtBuiltInKeywords.Text.Trim()) ? "" : txtBuiltInKeywords.Text.Trim().ToLower() + " ";
            MyGlobal.UpdateSetting("KeywordsConfig", "BuiltInKeywords", sTemp, false, true);
            MyLibrary.sKeywordsBuiltInKeywords = txtBuiltInKeywords.Text;

            sTemp = string.IsNullOrEmpty(txtUserDefinedKeywords.Text.Trim()) ? "" : txtUserDefinedKeywords.Text.Trim().ToLower() + " ";
            MyGlobal.UpdateSetting("KeywordsConfig", "UserDefinedKeywords", sTemp, false, true);
            MyLibrary.sKeywordsUserDefinedKeywords = txtUserDefinedKeywords.Text;
            #endregion

            Application.DoEvents();

            //儲存設定：SQL to Code
            #region 儲存設定：SQL to Code
            MyGlobal.UpdateSetting("SQL2CodeConfig", "VariableName", txtVariableName.Text);
            MyLibrary.sSQLToCodeVariableName = txtVariableName.Text;
            #endregion

            Application.DoEvents();

            //儲存設定：SQL Formatter
            #region 儲存設定：SQL Formatter
            MyGlobal.UpdateSetting("SQLFormatterConfig", "IndentString", "\t");
            MyLibrary.sSQLFormatterIndentString = "\t";

            MyGlobal.UpdateSetting("SQLFormatterConfig", "SpacesPerTab", "4");
            MyLibrary.iSQLFormatterSpacesPerTab = 4;

            MyGlobal.UpdateSetting("SQLFormatterConfig", "MaxLineWidth", txtMaxWidth.Text);
            MyLibrary.iSQLFormatterMaxLineWidth = Convert.ToInt16(txtMaxWidth.Text);

            MyGlobal.UpdateSetting("SQLFormatterConfig", "SpacesPerTab", "2");
            MyLibrary.iSQLFormatterNewStatementLineBreaks = 2;

            MyGlobal.UpdateSetting("SQLFormatterConfig", "NewClauseLineBreaks", "1");
            MyLibrary.iSQLFormatterNewClauseLineBreaks = 1;

            MyGlobal.UpdateSetting("SQLFormatterConfig", "ExpandCommaLists", chkExpandCommaLists.Checked ? "1" : "0");
            MyLibrary.bSQLFormatterExpandCommaLists = chkExpandCommaLists.Checked;

            MyGlobal.UpdateSetting("SQLFormatterConfig", "TrailingCommas", chkTrailingCommas.Checked ? "1" : "0");
            MyLibrary.bSQLFormatterTrailingCommas = chkTrailingCommas.Checked;

            MyGlobal.UpdateSetting("SQLFormatterConfig", "ExpandBooleanExpressions", chkExpandBooleanExpressions.Checked ? "1" : "0");
            MyLibrary.bSQLFormatterExpandBooleanExpressions = chkExpandBooleanExpressions.Checked;

            MyGlobal.UpdateSetting("SQLFormatterConfig", "ExpandCaseStatements", chkExpandCaseStatements.Checked ? "1" : "0");
            MyLibrary.bSQLFormatterExpandCaseStatements = chkExpandCaseStatements.Checked;

            MyGlobal.UpdateSetting("SQLFormatterConfig", "ExpandBetweenConditions", chkExpandBetweenConditions.Checked ? "1" : "0");
            MyLibrary.bSQLFormatterExpandBetweenConditions = chkExpandBetweenConditions.Checked;

            MyGlobal.UpdateSetting("SQLFormatterConfig", "ExpandInLists", chkExpandInLists.Checked ? "1" : "0");
            MyLibrary.bSQLFormatterExpandInLists = chkExpandInLists.Checked;

            MyGlobal.UpdateSetting("SQLFormatterConfig", "BreakJoinOnSections", chkBreakJoinOnSections.Checked ? "1" : "0");
            MyLibrary.bSQLFormatterBreakJoinOnSections = chkBreakJoinOnSections.Checked;

            MyGlobal.UpdateSetting("SQLFormatterConfig", "ConvertCaseForKeywords", chkConvertCaseForKeywords.Checked ? "1" : "0");
            MyLibrary.bSQLFormatterConvertCaseForKeywords = chkConvertCaseForKeywords.Checked;

            var iCase = 1;

            if (rdoLowerCase.Checked)
            {
                iCase = 2;
            }
            else if (rdoProperCase.Checked)
            {
                iCase = 3;
            }

            MyGlobal.UpdateSetting("SQLFormatterConfig", "ConvertCaseForKeywordsCase", iCase.ToString());
            MyLibrary.iSQLFormatterConvertCaseForKeywordsCase = iCase;
            #endregion

            Application.DoEvents();

            //儲存設定：Global
            #region 儲存設定：Global
            //檢查更新
            MyGlobal.UpdateSetting("GlobalConfig", "EnableCheckForUpdate", rdoCheckOnly.Checked ? "1" : "0");

            if (rdoCheckForUpdates0.Checked)
            {
                iCase = 0;
            }
            else if (rdoCheckForUpdates1.Checked)
            {
                iCase = 1;
            }
            else
            {
                iCase = 7;
            }

            MyGlobal.UpdateSetting("GlobalConfig", "CheckForUpdateDays", iCase.ToString());

            if (rdoMultiDocument.Checked)
            {
                sTemp = "MultiDocument";
            }
            else if (rdoMultiForm.Checked)
            {
                sTemp = "MultiForm";
            }
            else
            {
                sTemp = "MultiBox";
            }

            MyGlobal.UpdateSetting("GlobalConfig", "TabAppearance", sTemp);

            MyGlobal.UpdateSetting("GlobalConfig", "RecentFilesQty", txtRecentFiles.Text);
            MyLibrary.iRecentFilesQty = Convert.ToInt16(txtRecentFiles.Text);
            MyGlobal.UpdateSetting("GlobalConfig", "MyFavoriteQty", txtMyFavorite.Text);
            MyLibrary.iMyFavoriteQty = Convert.ToInt16(txtMyFavorite.Text);

            MyGlobal.UpdateSetting("GlobalConfig", "DateFormat", cboDateFormat.Text); //MyLibrary.sDateFormat
            MyLibrary.sDateFormat = cboDateFormat.Text;
            MyGlobal.UpdateSetting("GlobalConfig", "ShowVersion", chkShowVersion.Checked ? "1" : "0"); //MyLibrary.bShowVersion
            MyGlobal.UpdateSetting("GlobalConfig", "ShowClock", chkHideClock.Checked ? "1" : "0"); //MyLibrary.bShowClock
            MyGlobal.UpdateSetting("GlobalConfig", "MainFormMaximized", rdoMaximized.Checked ? "1" : "0");

            MyGlobal.UpdateSetting("GlobalConfig", "OptionsTabActiveForeColor", pnlOptionsTabActiveForeColor.Tag.ToString());
            MyLibrary.sColorOptionsTabActiveForeColor = pnlOptionsTabActiveForeColor.Tag.ToString();
            MyGlobal.UpdateSetting("GlobalConfig", "OptionsTabActiveBackColor", pnlOptionsTabActiveBackColor.Tag.ToString());
            MyLibrary.sColorOptionsTabActiveBackColor = pnlOptionsTabActiveBackColor.Tag.ToString();
            MyGlobal.UpdateSetting("GlobalConfig", "OptionsTabInactiveForeColor", pnlOptionsTabInactiveForeColor.Tag.ToString());
            MyLibrary.sColorOptionsTabInactiveForeColor = pnlOptionsTabInactiveForeColor.Tag.ToString();

            MyGlobal.UpdateSetting("GlobalConfig", "TabStyle", rdoIDE.Checked ? "IDE" : "Plain");
            MyGlobal.UpdateSetting("GlobalConfig", "TabBold", chkTabBold.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("GlobalConfig", "TabShrinkPages", chkShrinkPages.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("GlobalConfig", "TabShowArrows", chkShowArrows.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("GlobalConfig", "TabHoverSelect", chkHoverSelect.Checked ? "1" : "0");
            MyGlobal.UpdateSetting("GlobalConfig", "TabMultiLine", chkMultiLine.Checked ? "1" : "0");

            if (MyLibrary.bDarkMode && chkDarkMode.Tag.ToString() == "0" || MyLibrary.bDarkMode == false && chkDarkMode.Tag.ToString() == "1")
            {
                MyGlobal.bChangeColorThemeNeedRestart = true;
            }

            if (MyLibrary.bDarkMode == false && chkDarkMode.Tag.ToString() == "1")
            {
                _sLangText = MyGlobal.GetLanguageString("Changing the color theme from dark to normal requires restarting JasonQuery for the best display.", "form", Name, "msg", "ChangeColorTheme", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();
            TransferValueToMainForm("reloadqueryeditorsetting`");

            //判斷語系檔案是否存在
            if (CheckLocalizationFileExist()) //btnApply_Click
            {
                //判斷是否有變更語系
                if (cboLocalization.Text != cboLocalization.Tag.ToString())
                {
                    MyGlobal.UpdateSetting("GlobalConfig", "Localization", cboLocalization.Text);

                    MyGlobal.sLocalization = cboLocalization.Text;
                }
            }

            TransferValueToMainForm("reloadlocalization`");
            #endregion

            Application.DoEvents();
            Cursor = Cursors.Default;

            //20191013 關閉Tab - 要透過 MainForm 關閉才行, 否則 SubForm 關了, TabControl 沒關！
            TransferValueToMainForm("closeoptionstab`");
        }

        private void TransferValueToMainForm(string sValue)
        {
            var valueArgs = new ValueUpdatedEventArgs(sValue);
            ValueUpdated(this, valueArgs);
        }

        private void chkEnableGroupBox_CheckedChanged(object sender, EventArgs e)
        {
            //讓 CheckBox 可以完美控制 GroupBox 的 Enable/Disable
            if (c1DockingTab.SelectedTab == tabAutoComplete)
            {
                ManageCheckGroupBox(chkEnableAutoComplete, grpAutoComplete);
            }
            else if (c1DockingTab.SelectedTab == tabAutoReplace)
            {
                ManageCheckGroupBox(chkEnableAutoReplace, grpAutoReplace);
            }
        }

        private static void ManageCheckGroupBox(CheckBox chk, Control grp)
        {
            if (chk.Parent == grp)
            {
                grp.Parent.Controls.Add(chk);

                chk.Location = new Point(chk.Left + grp.Left, chk.Top + grp.Top);

                chk.BringToFront();
            }

            grp.Enabled = chk.Checked;
        }

        private void c1GridARInfo_RowColChange(object sender, RowColChangeEventArgs e)
        {
            var iRow = c1GridARInfo.Row;

            btnSaveAR.Tag = c1GridARInfo.Columns["PID"].CellValue(iRow).ToString();
            txtKeyword.ReadOnly = false;
            editorAR.ReadOnly = false;
            txtKeyword.Text = c1GridARInfo.Columns[_lstGridHeaderAr[(int)eColAR.Keyword]].CellValue(iRow).ToString();
            txtKeyword.Tag = c1GridARInfo.Columns[_lstGridHeaderAr[(int)eColAR.Keyword]].CellValue(iRow).ToString();
            editorAR.Text = c1GridARInfo.Columns[_lstGridHeaderAr[(int)eColAR.Replacement]].CellValue(iRow).ToString();
            editorAR.Tag = c1GridARInfo.Columns[_lstGridHeaderAr[(int)eColAR.Replacement]].CellValue(iRow).ToString();
            txtKeyword.ReadOnly = true;
            editorAR.ReadOnly = true;

            button_EnableControl(true); //顯示內容，但不可編輯 (除非按下「Add/Edit」)
        }

        private void CreateVisualStyleInfo()
        {
            var bShowColumnsDataType = chkShowColumnType.Checked;
            var sShowAs = cboNullShowAs.Text.ToUpper() == "NONE" ? "" : cboNullShowAs.Text;

            if (_bFormLoadFinish == false)
            {
                return;
            }

            _sLangText = MyGlobal.GetLanguageString("name", "form", Name, "gridheader", "name", "Text");
            var sGender = MyGlobal.GetLanguageString("gender", "form", Name, "gridheader", "gender", "Text");
            var sAge = MyGlobal.GetLanguageString("age", "form", Name, "gridheader", "age", "Text");
            var sBirthday = MyGlobal.GetLanguageString("birthday", "form", Name, "gridheader", "birthday", "Text");
            var sWeight = MyGlobal.GetLanguageString("weight", "form", Name, "gridheader", "weight", "Text");
            var sHeight = MyGlobal.GetLanguageString("height", "form", Name, "gridheader", "height", "Text");
            var sBlood = MyGlobal.GetLanguageString("blood group", "form", Name, "gridheader", "bloodgroup", "Text");
            var sEMail = MyGlobal.GetLanguageString("e-mail", "form", Name, "gridheader", "e-mail", "Text");
            var sEducation = MyGlobal.GetLanguageString("education", "form", Name, "gridheader", "education", "Text");
            var sAddress = MyGlobal.GetLanguageString("address", "form", Name, "gridheader", "address", "Text");

            switch (MyGlobal.sDataSource)
            {
                case "Oracle":
                    _sLangText = _sLangText.ToUpper() + (bShowColumnsDataType ? "\nVARCHAR(20)" : "");
                    sGender = sGender.ToUpper() + (bShowColumnsDataType ? "\nCHAR(1)" : "");
                    sAge = sAge.ToUpper() + (bShowColumnsDataType ? "\nINTEGER" : "");
                    sBirthday = sBirthday.ToUpper() + (bShowColumnsDataType ? "\nDATE" : "");
                    sWeight = sWeight.ToUpper() + (bShowColumnsDataType ? "\nVARCHAR(20)" : "");
                    sHeight = sHeight.ToUpper() + (bShowColumnsDataType ? "\nVARCHAR(20)" : "");
                    sBlood = sBlood.ToUpper() + (bShowColumnsDataType ? "\nCHAR(2)" : "");
                    sEMail = sEMail.ToUpper() + (bShowColumnsDataType ? "\nVARCHAR(30)" : "");
                    sEducation = sEducation.ToUpper() + (bShowColumnsDataType ? "\nVARCHAR(15)" : "");
                    sAddress = sAddress.ToUpper() + (bShowColumnsDataType ? "\nVARCHAR(100)" : "");
                    break;
                
                case "PostgreSQL" when chkShowStreamlinedName.Checked: //MyLibrary.bGridShowStreamlinedName
                    _sLangText += bShowColumnsDataType ? "\nvarchar(15)" : "";
                    sGender += bShowColumnsDataType ? "\nchar(1)" : "";
                    sAge += bShowColumnsDataType ? "\nint" : "";
                    sBirthday += bShowColumnsDataType ? "\ndate" : "";
                    sWeight += bShowColumnsDataType ? "\nvarchar(20)" : "";
                    sHeight += bShowColumnsDataType ? "\nvarchar(20)" : "";
                    sBlood += bShowColumnsDataType ? "\nchar(2)" : "";
                    sEMail += bShowColumnsDataType ? "\nvarchar(30)" : "";
                    sEducation += bShowColumnsDataType ? "\nvarchar(15)" : "";
                    sAddress += bShowColumnsDataType ? "\nvarchar(100)" : "";
                    break;

                case "PostgreSQL":
                    _sLangText += bShowColumnsDataType ? "\ncharacter varying(15)" : "";
                    sGender += bShowColumnsDataType ? "\ncharacter(1)" : "";
                    sAge += bShowColumnsDataType ? "\ninteger" : "";
                    sBirthday += bShowColumnsDataType ? "\ntime with time zone" : "";
                    sWeight += bShowColumnsDataType ? "\ncharacter varying(20)" : "";
                    sHeight += bShowColumnsDataType ? "\ncharacter varying(20)" : "";
                    sBlood += bShowColumnsDataType ? "\ncharacter(2)" : "";
                    sEMail += bShowColumnsDataType ? "\ncharacter varying(30)" : "";
                    sEducation += bShowColumnsDataType ? "\ncharacter varying(15)" : "";
                    sAddress += bShowColumnsDataType ? "\ncharacter varying(100)" : "";
                    break;
                default:
                    _sLangText += bShowColumnsDataType ? "\nvarchar(20)" : "";
                    sGender += bShowColumnsDataType ? "\nchar(1)" : "";
                    sAge += bShowColumnsDataType ? "\ninteger" : "";
                    sBirthday += bShowColumnsDataType ? "\ndate" : "";
                    sWeight += bShowColumnsDataType ? "\nvarchar(20)" : "";
                    sHeight += bShowColumnsDataType ? "\nVarchar(20)" : "";
                    sBlood += bShowColumnsDataType ? "\nchar(2)" : "";
                    sEMail += bShowColumnsDataType ? "\nvarchar(30)" : "";
                    sEducation += bShowColumnsDataType ? "\nvarchar(15)" : "";
                    sAddress += bShowColumnsDataType ? "\nvarchar(100)" : "";
                    break;
            }

            _dtVisualStyle = new DataTable();

            if (chkSort.Checked)
            {
                _dtVisualStyle.Columns.Add(sAddress);
                _dtVisualStyle.Columns.Add(sAge, typeof(int));
                _dtVisualStyle.Columns.Add(sBirthday);
                _dtVisualStyle.Columns.Add(sBlood);
                _dtVisualStyle.Columns.Add(sEducation);
                _dtVisualStyle.Columns.Add(sEMail);
                _dtVisualStyle.Columns.Add(sGender);
                _dtVisualStyle.Columns.Add(sHeight);
                _dtVisualStyle.Columns.Add(_sLangText);
                _dtVisualStyle.Columns.Add(sWeight);
            }
            else
            {
                _dtVisualStyle.Columns.Add(_sLangText);
                _dtVisualStyle.Columns.Add(sGender);
                _dtVisualStyle.Columns.Add(sAge, typeof(int));
                _dtVisualStyle.Columns.Add(sBirthday);
                _dtVisualStyle.Columns.Add(sWeight);
                _dtVisualStyle.Columns.Add(sHeight);
                _dtVisualStyle.Columns.Add(sBlood);
                _dtVisualStyle.Columns.Add(sEMail);
                _dtVisualStyle.Columns.Add(sEducation);
                _dtVisualStyle.Columns.Add(sAddress);
            }

            var rowVSInfo = _dtVisualStyle.NewRow();
            rowVSInfo[_sLangText] = "Mary";
            rowVSInfo[sGender] = "F";
            rowVSInfo[sAge] = "21";
            rowVSInfo[sBirthday] = Convert.ToDateTime("1991/12/31").ToString(cboDateFormat.Text);
            rowVSInfo[sWeight] = "49kg";
            rowVSInfo[sHeight] = "162cm";
            rowVSInfo[sBlood] = "B";
            rowVSInfo[sEMail] = "mary@gmail.com";
            rowVSInfo[sEducation] = "Master of Law";
            rowVSInfo[sAddress] = "505 N. Brand Blvd Suite 1450 Glendale CA 91203 USA";
            _dtVisualStyle.Rows.Add(rowVSInfo);

            rowVSInfo = _dtVisualStyle.NewRow();
            rowVSInfo[_sLangText] = "Lucky";
            rowVSInfo[sGender] = "M";
            rowVSInfo[sAge] = "18";
            rowVSInfo[sBirthday] = Convert.ToDateTime("1988/5/25").ToString(cboDateFormat.Text);
            rowVSInfo[sWeight] = "64kg";
            rowVSInfo[sHeight] = "159cm";
            rowVSInfo[sBlood] = sShowAs;
            rowVSInfo[sEMail] = "lucky.huang@pchome.com.tw";
            rowVSInfo[sEducation] = "Doctor of Philosophy";
            rowVSInfo[sAddress] = "Rm. 50705, 15F.-2, No. 155-12, Aly. 2022, Ln. 1155, Qingda Dongyuan, Sec. 25, Guangfu Rd., East Dist., Hsinchu City 30035, Taiwan (R.O.C.)";
            _dtVisualStyle.Rows.Add(rowVSInfo);

            rowVSInfo = _dtVisualStyle.NewRow();
            rowVSInfo[_sLangText] = "Jack";
            rowVSInfo[sGender] = "M";
            rowVSInfo[sAge] = "28";
            rowVSInfo[sBirthday] = Convert.ToDateTime("1997/10/10").ToString(cboDateFormat.Text);
            rowVSInfo[sWeight] = "69kg";
            rowVSInfo[sHeight] = "166cm";
            rowVSInfo[sBlood] = "AB";
            rowVSInfo[sEMail] = "jack@ibm.com";
            rowVSInfo[sEducation] = "Bachelor of Engineering";
            rowVSInfo[sAddress] = sShowAs;
            _dtVisualStyle.Rows.Add(rowVSInfo);

            rowVSInfo = _dtVisualStyle.NewRow();
            rowVSInfo[_sLangText] = "Peter";
            rowVSInfo[sGender] = "M";
            rowVSInfo[sAge] = "24";
            rowVSInfo[sBirthday] = Convert.ToDateTime("1998/3/24").ToString(cboDateFormat.Text);
            rowVSInfo[sWeight] = "87kg";
            rowVSInfo[sHeight] = "182cm";
            rowVSInfo[sBlood] = "B";
            rowVSInfo[sEMail] = "peter@dell.com";
            rowVSInfo[sEducation] = "Master of Business Administration";
            rowVSInfo[sAddress] = "15 Grand Rue 11800 Laure Minervois France";
            _dtVisualStyle.Rows.Add(rowVSInfo);

            rowVSInfo = _dtVisualStyle.NewRow();
            rowVSInfo[_sLangText] = "Jelic";
            rowVSInfo[sGender] = "M";
            rowVSInfo[sAge] = "31";
            rowVSInfo[sBirthday] = Convert.ToDateTime("1992/1/2").ToString(cboDateFormat.Text);
            rowVSInfo[sWeight] = "51kg";
            rowVSInfo[sHeight] = "162cm";
            rowVSInfo[sBlood] = "A";
            rowVSInfo[sEMail] = "jelic@hotmail.com";
            rowVSInfo[sEducation] = sShowAs;
            rowVSInfo[sAddress] = "3723 HR Bilthoven The Netherlands";
            _dtVisualStyle.Rows.Add(rowVSInfo);

            rowVSInfo = _dtVisualStyle.NewRow();
            rowVSInfo[_sLangText] = "Sue";
            rowVSInfo[sGender] = "F";
            rowVSInfo[sAge] = "19";
            rowVSInfo[sBirthday] = Convert.ToDateTime("1994/12/13").ToString(cboDateFormat.Text);
            rowVSInfo[sWeight] = sShowAs;
            rowVSInfo[sHeight] = "173cm";
            rowVSInfo[sBlood] = "O";
            rowVSInfo[sEMail] = "sue@test.com";
            rowVSInfo[sEducation] = "Doctor of Engineering";
            rowVSInfo[sAddress] = "Box 179, Millersville, SI 21108 Japan";
            _dtVisualStyle.Rows.Add(rowVSInfo);

            rowVSInfo = _dtVisualStyle.NewRow();
            rowVSInfo[_sLangText] = "Moly";
            rowVSInfo[sGender] = "F";
            rowVSInfo[sAge] = "27";
            rowVSInfo[sBirthday] = Convert.ToDateTime("1997/4/7").ToString(cboDateFormat.Text);
            rowVSInfo[sWeight] = "55kg";
            rowVSInfo[sHeight] = "167cm";
            rowVSInfo[sBlood] = "O";
            rowVSInfo[sEMail] = "moly@yahoo.com";
            rowVSInfo[sEducation] = "Master of Fine Arts";
            rowVSInfo[sAddress] = "7700 Gateway Blvd. Newark, CC 94560 Vietnam";
            _dtVisualStyle.Rows.Add(rowVSInfo);

            rowVSInfo = _dtVisualStyle.NewRow();
            rowVSInfo[_sLangText] = "Jenie";
            rowVSInfo[sGender] = "F";
            rowVSInfo[sAge] = "38";
            rowVSInfo[sBirthday] = Convert.ToDateTime("1992/8/31").ToString(cboDateFormat.Text);
            rowVSInfo[sWeight] = "60kg";
            rowVSInfo[sHeight] = sShowAs;
            rowVSInfo[sBlood] = "AB";
            rowVSInfo[sEMail] = "jenie@hp.com";
            rowVSInfo[sEducation] = "Bachelor of Arts in Music";
            rowVSInfo[sAddress] = "4700 NW Camas Meadows Drive Camas, WA 98607 USA";
            _dtVisualStyle.Rows.Add(rowVSInfo);

            c1GridVisualStyle.DataSource = _dtVisualStyle;
            c1GridVisualStyle.Splits[0].ColumnCaptionHeight = bShowColumnsDataType ? 45 : 25;

            //Grid's 選取顏色
            c1GridVisualStyle.SelectedStyle.ForeColor = ColorTranslator.FromHtml(pnlGridSelectedForeColor.Tag.ToString());
            c1GridVisualStyle.SelectedStyle.BackColor = ColorTranslator.FromHtml(pnlGridSelectedBackColor.Tag.ToString());

            c1GridVisualStyle.HeadingStyle.ForeColor = ColorTranslator.FromHtml(pnlGridHeadingForeColor.Tag.ToString());

            if (chkResize.Checked)
            {
                foreach (C1DisplayColumn col in c1GridVisualStyle.Splits[0].DisplayColumns)
                {
                    try
                    {
                        col.AutoSize();
                    }
                    catch (Exception)
                    {
                        col.Width = 2000;
                    }

                    if (!"`500`1000`1500`2000`".Contains("`" + cboMaxWidth.Text + "`"))
                    {
                        continue;
                    }

                    if (col.Width > Convert.ToInt16(cboMaxWidth.Text))
                    {
                        col.Width = Convert.ToInt16(cboMaxWidth.Text);
                    }
                }
            }

            //變更 Cell = NULL 的前景顏色 (不能使用 FetchCellStyle 事件，因為會變成整列都變色)
            if (string.IsNullOrWhiteSpace(sShowAs))
            {
                return;
            }

            var s1 = new Style
            {
                ForeColor = ColorTranslator.FromHtml(pnlNullValueForeColor.Tag.ToString())
            };

            for (var i = 0; i < c1GridVisualStyle.Columns.Count; i++)
            {
                c1GridVisualStyle.Splits[0].DisplayColumns[i].AddRegexCellStyle(CellStyleFlag.AllCells, s1, sShowAs);
            }
        }

        private void CreateAndGetARInfoTable()
        {
            const string sHideFields = "`PID`";

            _lstGridHeaderAr = new List<string>();

            _lstGridHeaderAr.Add("PID"); //Hide
            _sLangText = MyGlobal.GetLanguageString("Keyword", "form", Name, "gridheader", "Keyword", "Text");
            _lstGridHeaderAr.Add(_sLangText);
            _sLangText = MyGlobal.GetLanguageString("Replacement", "form", Name, "gridheader", "Replacement", "Text");
            _lstGridHeaderAr.Add(_sLangText);

            _dtARInfo = new DataTable();

            _dtARInfo.Columns.Add(_lstGridHeaderAr[(int)eColAR.PID]);
            _dtARInfo.Columns.Add(_lstGridHeaderAr[(int)eColAR.Keyword]);
            _dtARInfo.Columns.Add(_lstGridHeaderAr[(int)eColAR.Replacement]);

            var dt = DBCommon.ExecQuery("Select PID, AttributeValue From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='AutoReplaceConfig' And AttributeName='AutoReplace'");

            btnEditAR.Enabled = false;
            btnDeleteAR.Enabled = false;

            if (dt.Rows.Count > 0)
            {
                for (var iRow = 0; iRow < dt.Rows.Count; iRow++)
                {
                    _rowARInfo = _dtARInfo.NewRow();
                    _rowARInfo[_lstGridHeaderAr[(int)eColAR.PID]] = dt.Rows[iRow]["PID"].ToString();

                    var sAttributeValue = dt.Rows[iRow]["AttributeValue"].ToString();
                    var sInfo = sAttributeValue.Split(new[] { MyGlobal.sSeparator3s }, StringSplitOptions.None);

                    _rowARInfo[_lstGridHeaderAr[(int)eColAR.Keyword]] = sInfo[0];
                    _rowARInfo[_lstGridHeaderAr[(int)eColAR.Replacement]] = sInfo[1];

                    if (_iPID < Convert.ToInt32(dt.Rows[iRow]["PID"].ToString()))
                    {
                        _iPID = Convert.ToInt32(dt.Rows[iRow]["PID"].ToString());
                    }

                    _dtARInfo.Rows.Add(_rowARInfo);
                }

                btnEditAR.Enabled = true;
                btnDeleteAR.Enabled = true;
            }

            c1GridARInfo.DataSource = _dtARInfo;

            foreach (C1DisplayColumn col in c1GridARInfo.Splits[0].DisplayColumns)
            {
                if (sHideFields.Contains("`" + col.Name + "`"))
                {
                    col.Visible = false;
                    col.Frozen = true;
                }
                else if (col.Name == _lstGridHeaderAr[(int)eColAR.Keyword])
                {
                    try
                    {
                        col.AutoSize();
                    }
                    catch (Exception)
                    {
                        col.Width = 500;
                    }

                    if (col.Width > 500)
                    {
                        col.Width = 500;
                    }
                }
                else
                {
                    col.Width = 500;
                }
            }

            c1GridARInfo.AllowRowSizing = RowSizingEnum.IndividualRows;
            c1GridARInfo.Splits[0].ColumnCaptionHeight = 25;

            for (var r = 0; r < c1GridARInfo.Splits[0].Rows.Count; r++)
            {
                c1GridARInfo.Splits[0].Rows[r].AutoSize();

                if (c1GridARInfo.Splits[0].Rows[r].Height > 48)
                {
                    c1GridARInfo.Splits[0].Rows[r].Height = 48; //最多顯示 3 列資料
                }

                c1GridARInfo.Splits[0].Rows[r].Height += 4;
            }
        }

        private bool CheckARInfoExist()
        {
            return _dtARInfo.Rows.Cast<DataRow>().Any(oRow => oRow[_lstGridHeaderAr[(int) eColAR.Keyword]].ToString().Equals(txtKeyword.Text));
        }

        private void chkShowFilterRow_CheckedChanged(object sender, EventArgs e)
        {
            if (c1DockingTab.SelectedTab == tabAutoReplace)
            {
                c1GridARInfo.FilterBar = chkShowFilterRowAR.Checked;
            }
            else if (c1DockingTab.SelectedTab == tabDataGrid)
            {
                c1GridVisualStyle.FilterBar = chkShowFilterRow.Checked;
                CreateVisualStyleInfo(); //chkShowFilterRow_CheckedChanged
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var sTemp = "";

            _iPID++;

            if (c1DockingTab.SelectedTab == tabAutoReplace)
            {
                sTemp = "AR";
                grpModifyDefinitionAR.Tag = "ADD";
                txtKeyword.ReadOnly = false;
                txtKeyword.Text = "";
                txtKeyword.Tag = "";
                editorAR.ReadOnly = false;
                editorAR.Text = "";
                editorAR.Tag = "";
                txtKeyword.Focus();
            }

            var btnBox = Controls.Find("btnSave" + sTemp, true).FirstOrDefault() as Button;
            btnBox.Tag = _iPID.ToString();

            button_EnableControl(false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (c1DockingTab.SelectedTab == tabAutoReplace)
            {
                grpModifyDefinitionAR.Tag = "EDIT";
                txtKeyword.ReadOnly = false;
                editorAR.ReadOnly = false;
                txtKeyword.Focus();
            }

            button_EnableControl(false);
        }

        private void btnDeleteAR_Click(object sender, EventArgs e)
        {
            var sAlias = txtKeyword.Tag.ToString();

            foreach (DataRow oRow in _dtARInfo.Rows)
            {
                if (!oRow[_lstGridHeaderAr[(int)eColAR.Keyword]].ToString().Equals(sAlias))
                {
                    continue;
                }

                _dtARInfo.Rows.Remove(oRow);
                txtKeyword.ReadOnly = false;
                editorAR.ReadOnly = false;

                if (_dtARInfo.Rows.Count > 0)
                {
                    var iRow = c1GridARInfo.Row;

                    btnSaveAR.Tag = c1GridARInfo.Columns[_lstGridHeaderAr[(int)eColAR.PID]].CellValue(iRow).ToString();
                    txtKeyword.Text = c1GridARInfo.Columns[_lstGridHeaderAr[(int)eColAR.Keyword]].CellValue(iRow).ToString();
                    txtKeyword.Tag = c1GridARInfo.Columns[_lstGridHeaderAr[(int)eColAR.Keyword]].CellValue(iRow).ToString();
                    editorAR.Text = c1GridARInfo.Columns[_lstGridHeaderAr[(int)eColAR.Replacement]].CellValue(iRow).ToString();
                    editorAR.Tag = c1GridARInfo.Columns[_lstGridHeaderAr[(int)eColAR.Replacement]].CellValue(iRow).ToString();
                    txtKeyword.ReadOnly = true;
                    editorAR.ReadOnly = true;
                }
                else
                {
                    txtKeyword.Text = "";
                    editorAR.Text = "";
                    txtKeyword.ReadOnly = true;
                    editorAR.ReadOnly = true;
                    btnEditAR.Enabled = false;
                    btnDeleteAR.Enabled = false;
                }

                break;
            }
        }

        private void btnSaveAR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKeyword.Text.Trim()))
            {
                _sLangText = MyGlobal.GetLanguageString("Please input \"Keyword\"!", "form", Name, "msg", "InputKeyword", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtKeyword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(editorAR.Text.Trim()))
            {
                _sLangText = MyGlobal.GetLanguageString("Please input \"Replacement\"!", "form", Name, "msg", "InputReplacement", "Text");
                MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                editorAR.Focus();
                return;
            }

            if (grpModifyDefinitionAR.Tag.ToString() == "ADD")
            {
                if (CheckARInfoExist() == false)
                {
                    _rowARInfo = _dtARInfo.NewRow();
                    _rowARInfo[_lstGridHeaderAr[(int)eColAR.PID]] = btnSaveAR.Tag.ToString();
                    _rowARInfo[_lstGridHeaderAr[(int)eColAR.Keyword]] = txtKeyword.Text.Trim();
                    _rowARInfo[_lstGridHeaderAr[(int)eColAR.Replacement]] = editorAR.Text.Trim();

                    _dtARInfo.Rows.Add(_rowARInfo);

                    var iRow = _dtARInfo.Rows.Count - 1;
                    c1GridARInfo.Row = iRow;
                }
                else
                {
                    _sLangText = MyGlobal.GetLanguageString("The Keyword already exists!", "form", Name, "msg", "ReplacementKeywordExist", "Text");
                    MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (grpModifyDefinitionAR.Tag.ToString() == "EDIT")
            {
                foreach (DataRow oRow in _dtARInfo.Rows)
                {
                    if (!oRow[_lstGridHeaderAr[(int) eColAR.PID]].ToString().Equals(btnSaveAR.Tag.ToString()))
                    {
                        continue;
                    }

                    oRow[_lstGridHeaderAr[(int)eColAR.Keyword]] = txtKeyword.Text.Trim();
                    oRow[_lstGridHeaderAr[(int)eColAR.Replacement]] = editorAR.Text.Trim();

                    break;
                }
            }

            txtKeyword.Tag = txtKeyword.Text.Trim();
            editorAR.Tag = editorAR.Text.Trim();

            button_EnableControl(true);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (c1DockingTab.SelectedTab != tabAutoReplace)
            {
                return;
            }

            txtKeyword.Text = "";
            editorAR.Text = "";
            txtKeyword.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (c1DockingTab.SelectedTab == tabAutoReplace)
            {
                if (grpModifyDefinitionAR.Tag.ToString() == "EDIT")
                {
                    txtKeyword.Text = txtKeyword.Tag.ToString();
                    editorAR.Text = editorAR.Tag.ToString();
                }
                else
                {
                    txtKeyword.Text = "";
                    editorAR.Text = "";
                }
            }

            button_EnableControl(true);

            editorAR.Focus();
            btnAddAR.Focus();
        }

        private void button_EnableControl(bool bValue)
        {
            if (c1DockingTab.SelectedTab == tabAutoReplace)
            {
                txtKeyword.ReadOnly = bValue;
                editorAR.ReadOnly = bValue;
                btnAddAR.Enabled = bValue;
                btnEditAR.Enabled = bValue;
                btnDeleteAR.Enabled = bValue;
                btnSaveAR.Enabled = !bValue;
                btnCancelAR.Enabled = !bValue;
                btnClearAR.Enabled = !bValue;
                c1GridARInfo.Enabled = bValue;
            }

            btnRestoreDefaults.Enabled = bValue;
            btnCopySettings.Enabled = bValue;
            btnApply.Enabled = bValue;
            btnClose.Enabled = bValue;
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void nudMinFragmentLength_Leave(object sender, EventArgs e)
        {
            //數字如果被使用者用 Delete or Backspace 刪除了，數值還是等於原數值，但畫面上是顯示空值；透過以下方式，將 numMinFragmentLength 顯示成原數值
            if (nudMinFragmentLength.Value == 9)
            {
                nudMinFragmentLength.Value = 8;
                nudMinFragmentLength.UpButton();
            }
            else if (nudMinFragmentLength.Value >= 2)
            {
                nudMinFragmentLength.Value += 1;
                nudMinFragmentLength.DownButton();
            }
        }

        private void HighlightPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bFormLoadFinish == false)
            {
                return;
            }

            HighlightPreview(); //Query Editor 頁籤：Highlight, 3 個下拉選單觸發
        }

        private void ZoomGrid() //bPreview=true, 表示只要針對「c1GridVisualStyle」作用即可
        {
            float.TryParse(cboGridZoom.Text, out var pcnt);

            if (_fontSize == 0)
            {
                _fontSize = 12;
            }

            ((C1TrueDBGrid)_lstc1Grid[(int)c1GridID.VisualStyle]).RowHeight = (int)(_rowHeight * pcnt) + 5;
            ((C1TrueDBGrid)_lstc1Grid[(int)c1GridID.VisualStyle]).Splits[0].ColumnCaptionHeight = (int)(_rowHeight * pcnt) + 12; //標題列的高度            
            ((C1TrueDBGrid)_lstc1Grid[(int)c1GridID.VisualStyle]).RecordSelectorWidth = (int)(_recSelWidth * pcnt);
            ((C1TrueDBGrid)_lstc1Grid[(int)c1GridID.VisualStyle]).Styles["Normal"].Font = new Font(c1GridVisualStyle.Styles["Normal"].Font.FontFamily, _fontSize * pcnt);

            foreach (C1DisplayColumn col in ((C1TrueDBGrid)_lstc1Grid[(int)c1GridID.VisualStyle]).Splits[0].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 500;
                }

                if (col.Width > 500)
                {
                    col.Width = 500;
                }
            }
        }

        private void AlternatingRowColorSetting()
        {
            c1GridVisualStyle.AlternatingRows = true;
            c1GridVisualStyle.OddRowStyle.ForeColor = ColorTranslator.FromHtml((pnlGridOddRowForeColor.Tag ?? string.Empty).ToString());
            c1GridVisualStyle.OddRowStyle.BackColor = ColorTranslator.FromHtml((pnlGridOddRowBackColor.Tag ?? string.Empty).ToString());
            c1GridVisualStyle.EvenRowStyle.ForeColor = ColorTranslator.FromHtml((pnlGridEvenRowForeColor.Tag ?? string.Empty).ToString());
            c1GridVisualStyle.EvenRowStyle.BackColor = ColorTranslator.FromHtml((pnlGridEvenRowBackColor.Tag ?? string.Empty).ToString());
        }

        private void cboEditorFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bFormLoadFinish == false)
            {
                return;
            }

            MyLibrary.sQueryEditorFontSize = cboEditorFontSize.Text;
            ApplySqlStyler("editor"); //cboEditorFontSize_SelectedIndexChanged
        }

        private void cboEditorZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bFormLoadFinish == false)
            {
                return;
            }

            MyLibrary.sQueryEditorZoom = cboEditorZoom.Text;
            editor.Zoom = Convert.ToInt16(cboEditorZoom.Text);
        }

        private void cboGridFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            float.TryParse(cboGridFontSize.Text, out var iSize);

            if (iSize == 0)
            {
                iSize = 12;
            }

            c1GridVisualStyle.Font = new Font(cboGridFontPicker.Text, iSize, FontStyle.Regular, GraphicsUnit.Point);
            AutoSizeGrid();
            c1GridVisualStyle.Refresh();
        }

        private void cboGridZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            ZoomGrid();
            AutoSizeGrid();
        }

        private void AutoSizeGrid()
        {
            if (_bFormLoadFinish == false)
            {
                return;
            }

            foreach (C1DisplayColumn col in c1GridVisualStyle.Splits[0].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 500;
                }

                if (col.Width > 500)
                {
                    col.Width = 500;
                }
            }

            c1GridVisualStyle.Refresh();
        }

        private void ChangeVisualStyle(bool bPreview = true) //bPreview=true, 表示只要針對「c1GridVisualStyle」作用即可
        {
            var sStyle = MyLibrary.bDarkMode ? "Office 2010 Black" : cboGridVisualStyle.Text;

            switch (sStyle)
            {
                case "Office 2007 Blue":
                    c1GridVisualStyle.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;

                    if (bPreview == false)
                    {
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Blue;
                    }

                    break;
                case "Office 2007 Silver":
                    c1GridVisualStyle.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;

                    if (bPreview == false)
                    {
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Silver;
                    }

                    break;
                case "Office 2007 Black":
                    c1GridVisualStyle.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Black;

                    if (bPreview == false)
                    {
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2007Black;
                    }

                    break;
                case "Office 2010 Blue":
                    c1GridVisualStyle.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;

                    if (bPreview == false)
                    {
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
                    }

                    break;
                case "Office 2010 Silver":
                    c1GridVisualStyle.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Silver;

                    if (bPreview == false)
                    {
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Silver;
                    }

                    break;
                case "Office 2010 Black":
                    c1GridVisualStyle.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Black;

                    if (bPreview == false)
                    {
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Black;
                    }

                    break;
                default:
                    c1GridVisualStyle.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;

                    if (bPreview == false)
                    {
                        c1GridARInfo.VisualStyle = C1.Win.C1TrueDBGrid.VisualStyle.Office2010Blue;
                    }

                    break;
            }
        }

        private void cboNullShowAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateVisualStyleInfo(); //cboNullShowAs_SelectedIndexChanged
        }

        private void chkShowColumnDataType_CheckedChanged(object sender, EventArgs e)
        {
            if (MyGlobal.sDataSource == "PostgreSQL")
            {
                chkShowStreamlinedName.Enabled = chkShowColumnType.Checked;
            }

            CreateVisualStyleInfo(); //chkShowColumnDataType_CheckedChanged
        }

        private void c1GridVisualStyle_MouseDown(object sender, MouseEventArgs e)
        {
            //20220218 取消右鍵選單 (此處的右鍵選單，功能與主畫面差異太大，故直接取消)
        }


        private void Detect_KeyUp(object sender, KeyEventArgs e)
        {
            _ctrlKeyDown = e.Control;
        }

        private void Detect_KeyDown(object sender, KeyEventArgs e)
        {
            _ctrlKeyDown = e.Control;
        }

        private void c1GridVisualStyle_MouseWheel(object sender, MouseEventArgs e)
        {
            const float scalePerDelta = 10f / 120;

            if (!_ctrlKeyDown)
            {
                return;
            }

            _totalDelta += e.Delta;
            var fValue = 1 + (float)(SystemInformation.MouseWheelScrollLines * _totalDelta) / 3600;

            if (fValue > 1.7 || fValue < 0.5)
            { }
            else
            {
                Zoom(fValue);
            }
        }

        private void Zoom(float pcnt)
        {
            if (_fontSize == 0)
            {
                _fontSize = 12;
            }

            c1GridVisualStyle.RowHeight = (int)(_rowHeight * pcnt) - 5;
            c1GridVisualStyle.Splits[0].ColumnCaptionHeight = (int)(_rowHeight * pcnt) + 12; //標題列的高度
            c1GridVisualStyle.RecordSelectorWidth = (int)(_recSelWidth * pcnt);
            c1GridVisualStyle.Styles["Normal"].Font = new Font(c1GridVisualStyle.Styles["Normal"].Font.FontFamily, _fontSize * pcnt);

            foreach (C1DisplayColumn col in c1GridVisualStyle.Splits[0].DisplayColumns)
            {
                try
                {
                    col.AutoSize();
                }
                catch (Exception)
                {
                    col.Width = 500;
                }

                if (col.Width > 500)
                {
                    col.Width = 500;
                }
            }
        }

        private void FrozenColumn(bool bFrozen = true)
        {
            for (var i = 0; i < c1GridVisualStyle.Splits[0].DisplayColumns.Count; i++)
            {
                c1GridVisualStyle.Splits[0].DisplayColumns[i].Frozen = false;
            }

            if (bFrozen == false)
            {
                return;
            }

            c1GridVisualStyle.Splits[0].DisplayColumns[c1GridVisualStyle.Col].Frozen = true;

            _gMenu.Items[(int)uMenu.UnfreezeColumn].Enabled = true;
        }

        private void ArrangeData(string sMode)
        {
            var i = 0;
            var selCol = c1GridVisualStyle.SelectedCols.Count;
            var sData = "";
            var sColumnName = "";
            var sDataType = "";
            var bCopy = false;
            var bActiveCell = true; //是否為「只點到某一個 cell，並沒有『選取』cell」?

            var sQuotingWith = cboResultCopyQuotingWith.Text == @"None" ? "" : cboResultCopyQuotingWith.Text;

            var sFieldSeparator = cboResultCopyFieldSeparator.Text; //MyLibrary.sGridFieldSeparator

            foreach (int row in c1GridVisualStyle.SelectedRows)
            {
                var vr = c1GridVisualStyle.Splits[0].Rows[row];
                string sTemp;
                string sTemp1;
                string sTemp2;

                if (selCol == 0) //整列選取
                {
                    bActiveCell = false;

                    foreach (C1DataColumn col1 in c1GridVisualStyle.Columns)
                    {
                        if (col1.Caption.IndexOf("\n", 0, StringComparison.Ordinal) != -1)
                        {
                            sTemp1 = col1.Caption.Split('\n')[0];
                            sTemp2 = col1.Caption.Split('\n')[1];
                        }
                        else
                        {
                            sTemp1 = col1.Caption;
                            sTemp2 = "";
                        }

                        if (i == 0)
                        {
                            //收集 Column Name & Data Type
                            sColumnName += sTemp1 + sFieldSeparator;
                            sDataType += string.IsNullOrEmpty(sTemp2) ? "" : sTemp2 + sFieldSeparator;
                        }

                        switch (col1.DataType.Name.ToUpper())
                        {
                            case "STRING":
                                sData += sQuotingWith + col1.CellText(vr.DataRowIndex) + sQuotingWith + sFieldSeparator;
                                break;
                            case "DATETIME":
                                sTemp = Convert.ToDateTime(col1.CellText(vr.DataRowIndex)).ToString(cboDateFormat.Text);
                                sData += sQuotingWith + sTemp + sQuotingWith + sFieldSeparator;
                                break;
                            default:
                                sData += col1.CellText(vr.DataRowIndex) + sFieldSeparator; //數字
                                break;
                        }
                    }

                    i++;
                }
                else //非整列選取 (選取區塊)
                {
                    foreach (C1DataColumn col1 in c1GridVisualStyle.SelectedCols)
                    {
                        bActiveCell = false;

                        if (col1.Caption.IndexOf("\n", 0, StringComparison.Ordinal) != -1)
                        {
                            sTemp1 = col1.Caption.Split('\n')[0];
                            sTemp2 = col1.Caption.Split('\n')[1];
                        }
                        else
                        {
                            sTemp1 = col1.Caption;
                            sTemp2 = "";
                        }

                        if (i == 0)
                        {
                            //收集 Column Name & Data Type
                            sColumnName += sTemp1 + sFieldSeparator;
                            sDataType += string.IsNullOrEmpty(sTemp2) ? "" : sTemp2 + sFieldSeparator;
                        }

                        switch (col1.DataType.Name.ToUpper())
                        {
                            case "STRING":
                                sData += sQuotingWith + col1.CellText(vr.DataRowIndex) + sQuotingWith + sFieldSeparator; //col1.Value
                                break;
                            case "DATETIME":
                                sTemp = Convert.ToDateTime(col1.CellText(vr.DataRowIndex)).ToString(cboDateFormat.Text);
                                sData += sQuotingWith + sTemp + sQuotingWith + sFieldSeparator;
                                break;
                            default:
                                sData += col1.CellText(vr.DataRowIndex) + sFieldSeparator; //數字
                                break;
                        }
                    }

                    i++;
                }

                if (!string.IsNullOrEmpty(sData) && sData.Length > sFieldSeparator.Length)
                {
                    sData = sData.Substring(0, sData.Length - sFieldSeparator.Length) + "\r\n";
                }
            }

            if (!string.IsNullOrEmpty(sColumnName) && sColumnName.Length > sFieldSeparator.Length)
            {
                sColumnName = sColumnName.Substring(0, sColumnName.Length - sFieldSeparator.Length);
            }

            if (!string.IsNullOrEmpty(sDataType) && sDataType.Length > sFieldSeparator.Length) //MyLibrary.bGridShowColumnDataType
            {
                sDataType = sDataType.Substring(0, sDataType.Length - sFieldSeparator.Length);
            }

            switch (sMode.ToUpper())
            {
                case "COPY":
                    bCopy = true;

                    if (bActiveCell)
                    {
                        i = 0;

                        foreach (C1DataColumn col1 in c1GridVisualStyle.Columns)
                        {
                            if (i == c1GridVisualStyle.Col)
                            {
                                if (col1.DataType.Name.ToUpper() == "STRING" || col1.DataType.Name.ToUpper() == "DATETIME")
                                {
                                    sData = sQuotingWith + c1GridVisualStyle[c1GridVisualStyle.Splits[0].Rows[c1GridVisualStyle.Row].DataRowIndex, c1GridVisualStyle.Col] + sQuotingWith;
                                }
                                else
                                {
                                    sData = c1GridVisualStyle[c1GridVisualStyle.Splits[0].Rows[c1GridVisualStyle.Row].DataRowIndex, c1GridVisualStyle.Col].ToString();
                                }

                                break;
                            }

                            i++;
                        }
                    }

                    if (sData.Length >=2 && sData.Substring(sData.Length - 2, 2) == "\r\n")
                    {
                        sData = sData.Substring(0, sData.Length - 2);
                    }

                    break;
                case "COPYWITHCOLUMNNAMES":
                    bCopy = true;
                    sData = sColumnName + (string.IsNullOrEmpty(sDataType) ? "" : ("\r\n" + sDataType)) + "\r\n" + sData;
                    break;
                case "COPYCOLUMNNAMES":
                    bCopy = true;
                    sData = sColumnName + (string.IsNullOrEmpty(sDataType) ? "" : ("\r\n" + sDataType));
                    break;
                case "EXPORTTOCSV":
                    sData = sColumnName + (string.IsNullOrEmpty(sDataType) ? "" : ("\r\n" + sDataType)) + "\r\n" + sData;
                    break;
            }

            if (bCopy)
            {
                Clipboard.SetDataObject(sData, false);
            }
            else
            {
                var sf = new SaveFileDialog();

                if (sMode.ToUpper() == "EXPORTTOCSV")
                {
                    sf.Title = MyGlobal.GetLanguageString("Save As", "Global", "Global", "msg", "SaveAs", "Text");
                    sf.Filter = @"Query files (*.csv)|*.csv|All files (*.*)|*.*";
                }
                else
                {
                    sf.Filter = @"Query files (*.sql)|*.sql|All files (*.*)|*.*";
                }

                if (sf.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (sMode.ToUpper() == "EXPORTTOCSV")
                {
                    if (string.IsNullOrEmpty(Path.GetExtension(sf.FileName)))
                    {
                        sf.FileName += ".csv";
                    }

                    TextEngine.WriteContentToFile(sData, sf.FileName, TextEncode.Default);
                }
                else
                {
                    if (string.IsNullOrEmpty(Path.GetExtension(sf.FileName)))
                    {
                        sf.FileName += ".sql";
                    }

                    TextEngine.WriteContentToFile(sData, sf.FileName, TextEncode.UTF8);
                }
            }
        }

        private void ArrangeData4AllData(string sMode)
        {
            var i = 0;
            var sData = "";
            var sColumnName = "";
            var sDataType = "";

            var sQuotingWith = cboResultCopyQuotingWith.Text == @"None" ? "" : cboResultCopyQuotingWith.Text;
            var sFieldSeparator = cboResultCopyFieldSeparator.Text; //MyLibrary.sGridFieldSeparator
            
            for (var iRow = 0; iRow < c1GridVisualStyle.Splits[0].Rows.Count; iRow++)
            {
                var vr = c1GridVisualStyle.Splits[0].Rows[iRow];

                foreach (C1DataColumn col1 in c1GridVisualStyle.Columns)
                {
                    string sTemp1;
                    string sTemp2;

                    if (col1.Caption.IndexOf("\n", 0, StringComparison.Ordinal) != -1)
                    {
                        sTemp1 = col1.Caption.Split('\n')[0];
                        sTemp2 = col1.Caption.Split('\n')[1];
                    }
                    else
                    {
                        sTemp1 = col1.Caption;
                        sTemp2 = "";
                    }

                    if (i == 0)
                    {
                        //收集 Column Name & Data Type
                        sColumnName += sTemp1 + sFieldSeparator;
                        sDataType += string.IsNullOrEmpty(sTemp2) ? "" : sTemp2 + sFieldSeparator;
                    }

                    switch (col1.DataType.Name.ToUpper())
                    {
                        case "STRING":
                        case "DATETIME":
                            sData += sQuotingWith + col1.CellText(vr.DataRowIndex) + sQuotingWith + sFieldSeparator;
                            break;
                        default:
                            sData += col1.CellText(vr.DataRowIndex) + sFieldSeparator; //數字
                            break;
                    }
                }

                i++;

                if (!string.IsNullOrEmpty(sData))
                {
                    sData = sData.Substring(0, sData.Length - sFieldSeparator.Length) + "\r\n";
                }
            }

            if (!string.IsNullOrEmpty(sData) && sData.Length > 2)
            {
                sData = sData.Substring(0, sData.Length - 2);
            }

            if (!string.IsNullOrEmpty(sColumnName) && sColumnName.Length > sFieldSeparator.Length)
            {
                sColumnName = sColumnName.Substring(0, sColumnName.Length - sFieldSeparator.Length);
            }

            if (chkShowColumnType.Checked && !string.IsNullOrEmpty(sDataType) && sDataType.Length > sFieldSeparator.Length) //MyLibrary.bGridShowColumnDataType
            {
                sDataType = sDataType.Substring(0, sDataType.Length - sFieldSeparator.Length);
            }

            switch (sMode.ToUpper())
            {
                case "EXPORTTOCSV":
                    sData = sColumnName + (string.IsNullOrEmpty(sDataType) ? "" : ("\r\n" + sDataType)) + "\r\n" + sData;
                    break;
            }

            var sf = new SaveFileDialog
            {
                Title = MyGlobal.GetLanguageString("Save As", "Global", "Global", "msg", "SaveAs", "Text"),
                Filter = sMode.ToUpper() == "EXPORTTOCSV"
                    ? @"Query files (*.csv)|*.csv|All files (*.*)|*.*"
                    : @"Query files (*.sql)|*.sql|All files (*.*)|*.*"
            };


            if (sf.ShowDialog() != DialogResult.OK)
            {
                return; //無論檔案是否存在，只要不是按「取消」或「否」，都會回傳 OK
            }

            if (sMode.ToUpper() == "EXPORTTOCSV")
            {
                if (string.IsNullOrEmpty(Path.GetExtension(sf.FileName)))
                {
                    sf.FileName += ".csv";
                }

                TextEngine.WriteContentToFile(sData, sf.FileName, TextEncode.Default);
            }
            else
            {
                if (string.IsNullOrEmpty(Path.GetExtension(sf.FileName)))
                {
                    sf.FileName += ".sql";
                }

                TextEngine.WriteContentToFile(sData, sf.FileName, TextEncode.UTF8);
            }
        }

        private void timerMother2Child_Tick(object sender, EventArgs e)
        {
            //由 TabControl 關閉 Options Form
            if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp) && MyGlobal.sGlobalTemp == "closeoptionstab")
            {
                MyGlobal.sGlobalTemp = ""; //避免重覆觸發！

                //從 MainForm 傳來的關閉指令，要先「取消套用，再告訴 MainForm 關閉 Options Form」，否則全域變數會被套用到
                CancelApplyAndCloseOptionsForm();

                //20191013 關閉 - 要透過 MainForm 才行, 否則 SubForm 關了, TabControl 沒關！
                TransferValueToMainForm("closeoptionstab`");
            }

            //是否為 Reload Localization 套用？
            if (string.IsNullOrEmpty(MyGlobal.sInfoFromReloadLocalization) || !MyGlobal.sInfoFromReloadLocalization.StartsWith("reloadlocalization`"))
            {
                return;
            }

            var sTemp = MyGlobal.sInfoFromReloadLocalization.Replace("reloadlocalization`", "");
            sTemp = sTemp.Split(';')[0];

            if (sTemp != AccessibleDescription)
            {
                return; //確認是否為指定的 Tab
            }

            MyGlobal.sInfoFromReloadLocalization = MyGlobal.sInfoFromReloadLocalization.Replace(AccessibleDescription + ";", "");

            if (MyGlobal.sInfoFromReloadLocalization == "reloadlocalization`")
            {
                MyGlobal.sInfoFromReloadLocalization = "";
            }

            if (_bApplyAndClose == false)
            {
                ApplyLocalizationSetting(); //timerMother2Child_Tick()
            }
        }

        private void txtVariableName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVariableName.Text))
            {
                txtVariableName.Text = "sSQL";
            }

            PreviewSQLToCode(); //txtVariableName_Leave
        }

        private void lstLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sTemp = "";
            rdoStyle3.Enabled = false;

            switch (lstLanguage.SelectedItem.ToString())
            {
                case "C#":
                case "VB.Net":
                    rdoStyle3.Enabled = true;
                    break;
                default:
                    if (rdoStyle3.Checked)
                    {
                        rdoStyle1.Checked = true;
                    }

                    break;
            }

            switch (lstLanguage.SelectedItem.ToString())
            {
                case "C#":
                    sTemp = MyGlobal.GetLanguageString("Using + operator", "form", Name, "msg", "C#Style1", "Text");
                    lblStyle1.Text = sTemp;
                    sTemp = MyGlobal.GetLanguageString("New line character \r\n", "form", Name, "msg", "C#Style2", "Text");
                    lblStyle2.Text = sTemp;
                    sTemp = MyGlobal.GetLanguageString("New line character Enviroment.NewLine", "form", Name, "msg", "C#Style3", "Text");
                    lblStyle3.Text = sTemp;
                    break;
                case "VB.Net":
                    sTemp = MyGlobal.GetLanguageString("Using & operator", "form", Name, "msg", "VB.NetStyle1", "Text");
                    lblStyle1.Text = sTemp;
                    sTemp = MyGlobal.GetLanguageString("New line character VbCrLf", "form", Name, "msg", "VB.NetStyle2", "Text");
                    lblStyle2.Text = sTemp;
                    sTemp = MyGlobal.GetLanguageString("New line character Enviroment.NewLine", "form", Name, "msg", "VB.NetStyle3", "Text");
                    lblStyle3.Text = sTemp;
                    break;
                case "VB6/VBA":
                    sTemp = MyGlobal.GetLanguageString("Using & operator", "form", Name, "msg", "VB6/VBAStyle1", "Text");
                    lblStyle1.Text = sTemp;
                    sTemp = MyGlobal.GetLanguageString("New line character VbCrLf", "form", Name, "msg", "VB6/VBAStyle2", "Text");
                    lblStyle2.Text = sTemp;
                    lblStyle3.Text = "";
                    break;
                case "Delphi6":
                    sTemp = MyGlobal.GetLanguageString("Using + operator", "form", Name, "msg", "Delphi6Style1", "Text");
                    lblStyle1.Text = sTemp;
                    sTemp = MyGlobal.GetLanguageString("New line character #13#10", "form", Name, "msg", "Delphi6Style2", "Text");
                    lblStyle2.Text = sTemp;
                    lblStyle3.Text = "";
                    break;
            }

            PreviewSQLToCode(); //lstLanguage_SelectedIndexChanged
        }

        private void rdoStyle_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rdo)
            {
                grpStyle.Tag = rdo.Tag.ToString().Substring(6, 1);
            }

            PreviewSQLToCode(); //rdoStyle_CheckedChanged
        }

        private void editorSQLStatement_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(editorSQLToCode.Text))
            {
                editorSQLToCode.Text = editorSQLToCodePreview.Tag.ToString();
            }
        }

        private void txtVariableName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtVariableName.Text))
            {
                txtVariableName.Text = @"sSQL";
            }

            PreviewSQLToCode(); //txtVariableName_KeyPress
        }

        private void PreviewSQLToCode()
        {
            if (!_bFormLoadFinish)
            {
                return;
            }

            editorSQLToCodePreview.ReadOnly = false;
            editorSQLToCodePreview.Text = MyLibrary.SQLToCode(editorSQLToCode.Text, txtVariableName.Text, lstLanguage.SelectedItem.ToString(), grpStyle.Tag.ToString(), false);
            editorSQLToCodePreview.ReadOnly = true;
        }

        private void txtMaxWidth_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtMaxWidth_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaxWidth.Text) || int.Parse(txtMaxWidth.Text) < 100)
            {
                txtMaxWidth.Text = @"99";
            }
        }

        private void txtMaxWidth_Enter(object sender, EventArgs e)
        {
            txtMaxWidth.SelectionStart = 0;
            txtMaxWidth.SelectionLength = txtMaxWidth.TextLength;
        }

        private void PreviewSqlFormatter()
        {
            var iUppercaseKeywords = 0;
            var iPos = editorSQLFormatterPreview.SelectionStart + 1;

            if (chkConvertCaseForKeywords.Checked)
            {
                if (rdoUpperCase.Checked)
                {
                    iUppercaseKeywords = 1;
                }
                else if (rdoLowerCase.Checked)
                {
                    iUppercaseKeywords = 2;
                }
                else if (rdoProperCase.Checked)
                {
                    iUppercaseKeywords = 3;
                }
            }

            if (!_bFormLoadFinish || string.IsNullOrWhiteSpace(editorSQLFormatter.Text))
            {
                return;
            }

            if (string.IsNullOrEmpty(txtMaxWidth.Text) || int.Parse(txtMaxWidth.Text) < 100)
            {
                txtMaxWidth.Text = @"99";
            }

            editorSQLFormatterPreview.ReadOnly = false;
            editorSQLFormatterPreview.Text = MyLibrary.SQLFormatter(editorSQLFormatter.Text, "\\t", 4, int.Parse(txtMaxWidth.Text), 2, 1, chkExpandCommaLists.Checked, chkTrailingCommas.Checked,
                true, chkExpandBooleanExpressions.Checked, chkExpandCaseStatements.Checked, chkExpandBetweenConditions.Checked,
                chkExpandInLists.Checked, chkBreakJoinOnSections.Checked, iUppercaseKeywords);
            editorSQLFormatterPreview.ReadOnly = true;
            editorSQLFormatterPreview.SelectionStart = iPos;
            editorSQLFormatterPreview.ScrollCaret();
        }

        private void SQLFormat_TextChanged(object sender, EventArgs e)
        {
            PreviewSqlFormatter(); //SQLFormat_TextChanged
        }

        private void SQLFormatter_CheckedChanged(object sender, EventArgs e)
        {
            PreviewSqlFormatter(); //SQLFormatter_CheckedChanged
        }

        private void SQLFormatter2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConvertCaseForKeywords.Checked)
            {
                PreviewSqlFormatter(); //SQLFormatter2_CheckedChanged
            }
        }

        private void editor_MouseClick(object sender, MouseEventArgs e)
        {
            //停用：會造成輸入文字時的誤刪困擾 (加上區塊選取+執行，會造成選取文字的 SQL 指令包含了其他的選取文字)
            //if (chkHighlightSelection.Checked)
            //    HighlightSelection(true);
        }

        private void HighlightSelection(bool bMouseClick = false)
        {
            var pos = editor.CurrentPosition;
            var wordStart = editor.WordStartPosition(pos, true);
            var wordEnd = editor.WordEndPosition(pos, true);
            var sUpperText = editor.Text.ToUpper();
            var word = editor.GetTextRange(wordStart, wordEnd - wordStart);

            var iMainSelection = 0;
            var bValue = true;
            editor.AdditionalCaretsBlink = false; //選取的字串，最前面會不會閃爍
            editor.AdditionalCaretsVisible = false; //選取的字串，最前面的 | 要不要顯示

            //包含換行符號？不用 multi-select！
            if (editor.SelectedText.Length - editor.SelectedText.Replace("\r", "").Length > 0 ||
                editor.SelectedText.Length - editor.SelectedText.Replace("\n", "").Length > 0)
            {
                bValue = false;
            }

            //包含空白？不用 multi-select！
            if (bValue && editor.SelectedText.Length - editor.SelectedText.Replace(" ", "").Length > 0)
            {
                bValue = false;
            }

            //for Mouse Click，如果沒有選取文字，需要重新處理 multi selection
            //20190320 此處還有 BUG 需要調整：左右鍵移動時，在單字移動，例如 word，游標在 d 按左鍵移動，此時不會選取 (忽略 bMouseClick，會造成左右鍵失效)
            if (string.IsNullOrEmpty(editor.SelectedText) && bMouseClick)
            {
                editor.Tag = "";
            }

            if (string.IsNullOrEmpty(word) || !bValue || editor.Tag.ToString() == word)
            {
                return;
            }

            editor.Tag = word;

            var matches = Regex.Matches(sUpperText, word.ToUpper());

            editor.MultipleSelection = true;

            foreach (Match m in matches)
            {
                if (pos >= m.Index && pos - m.Index <= word.Length)
                {
                    //記住游標所在處的單字
                    iMainSelection = m.Index;
                }
                else
                {
                    editor.AddSelection(m.Index, m.Index + word.Length);
                }
            }

            //游標所在處的單字，最後再選取，游標就會停留在此單字的最前面
            editor.AddSelection(iMainSelection, iMainSelection + word.Length);
        }

        private void cboFindGrid_KeyUp(object sender, KeyEventArgs e)
        {
            var bValue = !string.IsNullOrEmpty(cboFindGrid.Text.Trim());

            btnFindNextGrid.Enabled = bValue;
            btnFindPreviousGrid.Enabled = bValue;
            btnCountGrid.Enabled = bValue;
            btnHighlightAllGrid.Enabled = bValue;
            btnClearHighlightsGrid.Enabled = bValue;
        }

        private void cboFindGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13 || string.IsNullOrEmpty(cboFindGrid.Text.Trim()))
            {
                return;
            }

            btnFindNextGrid.PerformClick();
        }

        private void cboFindGrid_DropDown(object sender, EventArgs e)
        {
            //LoadFindListGrid();
        }

        private void cboFindGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboFindGrid.Text.Trim()))
            {
                return;
            }

            btnFindNextGrid.Enabled = true;
            btnFindPreviousGrid.Enabled = true;
            btnCountGrid.Enabled = true;
            btnHighlightAllGrid.Enabled = true;
            btnClearHighlightsGrid.Enabled = true;

            cboFindGrid.SelectedIndex = 0;
            cboFindGrid.Tag = HighlightCount().ToString(); //統計出現次數，但不顯示
        }

        private void cboFindGrid_TextChanged(object sender, EventArgs e)
        {
            var bValue = !string.IsNullOrEmpty(cboFindGrid.Text.Trim());

            foreach (C1DisplayColumn cd in c1GridVisualStyle.Splits[0].DisplayColumns)
            {
                cd.OwnerDraw = bValue;
            }

            btnFindNextGrid.Enabled = bValue;
            btnFindPreviousGrid.Enabled = bValue;
            btnCountGrid.Enabled = bValue;
            btnHighlightAllGrid.Enabled = bValue;
            btnClearHighlightsGrid.Enabled = bValue;

            if (bValue)
            {
                cboFindGrid.Tag = HighlightCount().ToString();
            } //統計出現次數，但不顯示
        }

        private void btnFindNextGrid_Click(object sender, EventArgs e)
        {
            if (cboFindGrid.Tag.ToString() == "0")
            {
                FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "JasonQuery");
                MessageBox.Show("Can't find the text \"" + cboFindGrid.Text + "\"", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!FindNextGrid())
            {
                return;
            }

            //往下找不到資料，而且不是在第一格位置，再從頭開始找一次
            if (!(c1GridVisualStyle.Row == 0 && c1GridVisualStyle.Col == 0))
            {
                FindNextGrid(true);
            }
        }

        private bool FindNextGrid(bool bFindAgain = false)
        {
            var sSearchText = cboFindGrid.Text;
            var iStartRow = c1GridVisualStyle.Row;
            var iFindRow = 0;
            var iStartCol = c1GridVisualStyle.Col;
            var iFindCol = 0;
            var bFind = false;
            bool bResult;

            if (bFindAgain)
            {
                iStartRow = 0;
                iStartCol = 0;
            }

            if (string.IsNullOrEmpty(sSearchText))
            {
                return false;
            }

            for (var iRow = iStartRow; iRow < c1GridVisualStyle.Splits[0].Rows.Count; iRow++)
            {
                var vr = c1GridVisualStyle.Splits[0].Rows[iRow];
                var iCol = 0;

                foreach (C1DataColumn col1 in c1GridVisualStyle.Columns)
                {
                    if (col1.CellText(vr.DataRowIndex).Length != col1.CellText(vr.DataRowIndex).ToUpper().Replace(cboFindGrid.Text.ToUpper(), "").Length)
                    {
                        if (iRow == iStartRow) //游標所在列，尋找下一個，要略過此格！
                        {
                            if (iCol > iStartCol)
                            {
                                iFindRow = iRow;
                                iFindCol = iCol;

                                bFind = true;
                                break;
                            }
                        }
                        else
                        {
                            iFindRow = iRow;
                            iFindCol = iCol;

                            bFind = true;
                            break;
                        }
                    }

                    iCol++;
                }

                if (bFind)
                {
                    break;
                }
            }

            if (bFind)
            {
                c1GridVisualStyle.Row = iFindRow;
                c1GridVisualStyle.Col = iFindCol;
                c1GridVisualStyle.Select(); //Focus 切換到指定的 Cell
                bResult = false;
            }
            else
            {
                bResult = true;
            }

            return bResult;
        }

        private void btnFindPreviousGrid_Click(object sender, EventArgs e)
        {
            if (cboFindGrid.Tag.ToString() == "0")
            {
                FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "JasonQuery");
                MessageBox.Show("Can't find the text \"" + cboFindGrid.Text + "\"", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (FindPreviousGrid())
            {
                //往上找不到資料，再從底部開始找一次
                FindPreviousGrid(true);
            }
        }

        private bool FindPreviousGrid(bool bFindAgain = false)
        {
            var sSearchText = cboFindGrid.Text;
            var iStartRow = c1GridVisualStyle.Row;
            var iFindRow = 0;
            var iStartCol = c1GridVisualStyle.Col;
            var iFindCol = 0;
            var bFind = false;
            bool bResult;

            if (bFindAgain)
            {
                iStartRow = c1GridVisualStyle.Splits[0].Rows.Count - 1;
                iStartCol = c1GridVisualStyle.Splits[0].DisplayColumns.Count - 1;
            }

            if (string.IsNullOrEmpty(sSearchText))
            {
                return false;
            }

            for (var iRow = iStartRow; iRow >= 0; iRow--)
            {
                var vr = c1GridVisualStyle.Splits[0].Rows[iRow];
                var iCol = 0;

                for (var jj = c1GridVisualStyle.Splits[0].DisplayColumns.Count - 1; jj >= 0; jj--)
                {
                    if (c1GridVisualStyle.Columns[jj].CellValue(iRow).ToString().Length != c1GridVisualStyle.Columns[jj].CellValue(iRow).ToString().ToUpper().Replace(cboFindGrid.Text.ToUpper(), "").Length)
                    {
                        if (iRow == iStartRow) //游標所在列
                        {
                            if (bFindAgain == false && jj < iStartCol || bFindAgain && jj <= iStartCol)
                            {
                                iFindRow = iRow;
                                iFindCol = jj;

                                bFind = true;
                                break;
                            }
                        }
                        else
                        {
                            iFindRow = iRow;
                            iFindCol = jj;

                            bFind = true;
                            break;
                        }
                    }

                    iCol++;
                }

                if (bFind)
                {
                    break;
                }
            }

            if (bFind)
            {
                c1GridVisualStyle.Row = iFindRow;
                c1GridVisualStyle.Col = iFindCol;
                c1GridVisualStyle.Select(); //Focus 切換到指定的 Cell
                bResult = false;
            }
            else
            {
                bResult = true;
            }

            return bResult;
        }

        private void btnClearHighlightsGrid_Click(object sender, EventArgs e)
        {
            //Disable OwnerDraw property for each column
            foreach (C1DisplayColumn cd in c1GridVisualStyle.Splits[0].DisplayColumns)
            {
                cd.OwnerDraw = false;
            }

            _modifiedList.Clear();
            c1GridVisualStyle.ClearCellStyle(CellStyleFlag.AllCells);
        }

        private void c1GridVisualStyle_FetchCellStyle(object sender, FetchCellStyleEventArgs e)
        {
            //var dr = (DataRowView)c1GridVisualStyle[c1GridVisualStyle.RowBookmark(e.Row)];

            //if ((int)dr[1] <= 20)
            //{
            //    //e.CellStyle.BackColor = Color.Blue; 
            //}
            //else if (dr[5].ToString() == "O")
            //{
            //    //e.CellStyle.BackColor = Color.Green;
            //}
        }

        private void btnHighlightAllGrid_Click(object sender, EventArgs e)
        {
            var bFind = false;

            btnClearHighlightsGrid.PerformClick();

            if (cboFindGrid.Tag.ToString() == "0")
            {
                FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "JasonQuery");
                MessageBox.Show("Can't find the text \"" + cboFindGrid.Text + "\"", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            for (var iRow = 0; iRow < c1GridVisualStyle.Splits[0].Rows.Count; iRow++)
            {
                var iCol = 0;
                var vr = c1GridVisualStyle.Splits[0].Rows[iRow];

                foreach (C1DataColumn col1 in c1GridVisualStyle.Columns)
                {
                    if (col1.CellText(vr.DataRowIndex).Length != col1.CellText(vr.DataRowIndex).ToUpper().Replace(cboFindGrid.Text.ToUpper(), "").Length)
                    {
                        bFind = true;
                        _modifiedList.Add(new Point(iRow, iCol));
                    }

                    iCol++;
                }
            }

            //Enable OwnerDraw property for each column
            if (!bFind)
            {
                return;
            }

            foreach (C1DisplayColumn cd in c1GridVisualStyle.Splits[0].DisplayColumns)
            {
                cd.OwnerDraw = true;
            }
        }

        private void c1GridVisualStyle_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            var mc = new Point(e.Row, e.Col);

            if (!_modifiedList.Contains(mc))
            {
                return;
            }

            e.Style.ForeColor = ColorTranslator.FromHtml(pnlGridHighlightForeColor.Tag.ToString()); //Color.Green;
            e.Style.BackColor = ColorTranslator.FromHtml(pnlGridHighlightBackColor.Tag.ToString()); //Color.LightPink;
        }

        private void btnCountGrid_Click(object sender, EventArgs e)
        {
            HighlightCount(true);
        }

        private int HighlightCount(bool bShowMsg = false)
        {
            var iCount = 0;

            for (var iRow = 0; iRow < c1GridVisualStyle.Splits[0].Rows.Count; iRow++)
            {
                var vr = c1GridVisualStyle.Splits[0].Rows[iRow];

                iCount += c1GridVisualStyle.Columns.Cast<C1DataColumn>().Count(col1 => col1.CellText(vr.DataRowIndex).Length != col1.CellText(vr.DataRowIndex).ToUpper().Replace(cboFindGrid.Text.ToUpper(), "").Length);
            }

            if (!bShowMsg)
            {
                return iCount;
            }

            FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 30, true, "JasonQuery");

            var sTemp1 = MyGlobal.GetLanguageString("Find What:", "form", Name, "msg", "FindWhat", "Text");
            var sTemp2 = MyGlobal.GetLanguageString("Count:", "form", Name, "msg", "Count", "Text");
            var sTemp3 = MyGlobal.GetLanguageString("matches.", "form", Name, "msg", "matches", "Text");
            MessageBox.Show(sTemp1 + @" " + cboFindGrid.Text + "\r\n\r\n" + sTemp2 + @" " + iCount + @" " + sTemp3, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return iCount;
        }

        private static void FindAndMoveMsgBox(int x, int y, bool repaint, string title)
        {
            var thr = new Thread(() =>
            {
                IntPtr msgBox;
                
                while ((msgBox = FindWindow(IntPtr.Zero, title)) == IntPtr.Zero) ;
                
                GetWindowRect(msgBox, out var r);
                MoveWindow(msgBox /* handle of the message box */, x, y,
                   r.Width - r.X /* width of originally message box */,
                   r.Height - r.Y /* height of originally message box */,
                   repaint /* if true, the message box repaints */);
            });

            thr.Start();
        }

        private void Keywords_LeaveCheck(object sender, EventArgs e)
        {
            if (!(sender is ScintillaEditor keywords))
            {
                return;
            }

            keywords.Text = keywords.Text.Replace("\r\n", " ");
            keywords.Text = keywords.Text.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ")
                .Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
        }

        private void ShowOperatorKeywords(object sender, EventArgs e)
        {
            if (sender is PictureBox pic)
            {
                FindKeywords(Convert.ToInt16(pic.Tag.ToString()), true);
            }
        }

        private void HideOperatorKeywords(object sender, EventArgs e)
        {
            if (sender is ToolStripButton btn)
            {
                FindKeywords(Convert.ToInt16(btn.Tag.ToString()));
            }
        }

        private void FindKeywords(int iKey, bool bFormButton = false)
        {
            var iFocus = -1; //目前 Focus 在哪一個區塊？

            for (var i = 0; i <=3; i++)
            {
                if (!((GroupBox) _lstFindGroup[i]).Visible)
                {
                    continue;
                }

                iFocus = i;
                break;
            }

            if (iFocus != -1) // && iFocus != iKey)
            {
                FindKeywords2(iFocus); //先 Show 再 Hide
            }

            if (!(iKey >= 0 && iKey <= 3))
            {
                return;
            }

            if (iFocus != -1 && bFormButton == false)
            {
                return;
            }

            if (iFocus != iKey)
            {
                FindKeywords2(iKey);
            }
        }

        private void FindKeywords2(int iKey)
        {
            int iWidth;
            var iHeight = ((ScintillaEditor)_lstFindEditor[iKey]).Height;

            if (((GroupBox)_lstFindGroup[iKey]).Visible)
            {
                iWidth = ((ScintillaEditor)_lstFindEditor[iKey]).Width + 205;
                ((ScintillaEditor)_lstFindEditor[iKey]).Size = new Size(iWidth, iHeight);
                ((GroupBox)_lstFindGroup[iKey]).Visible = false;
                ((ScintillaEditor)_lstFindEditor[iKey]).Focus();
            }
            else
            {
                iWidth = ((ScintillaEditor)_lstFindEditor[iKey]).Width - 205;
                ((ScintillaEditor)_lstFindEditor[iKey]).Size = new Size(iWidth, iHeight);
                ((GroupBox)_lstFindGroup[iKey]).Visible = true;
                ((ToolStripTextBox)_lstFindTextBox[iKey]).Focus();
            }
        }

        private void FindKeywords_TextChanged(object sender, EventArgs e)
        {
            if (!(sender is ToolStripTextBox txt))
            {
                return;
            }

            int i = Convert.ToInt16(txt.Tag.ToString());
            var bValue = !string.IsNullOrEmpty(((ToolStripTextBox)_lstFindTextBox[i]).Text.Trim());

            ((ToolStripButton)_lstFindNextButton[i]).Enabled = bValue;
            ((ToolStripButton)_lstFindPreviousButton[i]).Enabled = bValue;
        }

        private void FindNextKeywords_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripButton btn))
            {
                return;
            }

            int i = Convert.ToInt16(btn.Tag.ToString());

            var sText = ((ScintillaEditor)_lstFindEditor[i]).Text.Trim();
            var sSearchText = ((ToolStripTextBox)_lstFindTextBox[i]).Text.Trim();

            if (string.IsNullOrEmpty(sText) || string.IsNullOrEmpty(sSearchText))
            {
                return;
            }

            if (FindCount(sText, sSearchText) == 0)
            {
                FindAndMoveMsgBox(Cursor.Position.X - 150, Cursor.Position.Y + 30, true, "JasonQuery");
                MessageBox.Show("Can't find the text \"" + sSearchText + "\"", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (FindNext(sText, sSearchText, i) == false)
            {
                if (FindNext(sText, sSearchText, i, true))
                {
                    ((ScintillaEditor)_lstFindEditor[i]).Focus();
                }
            }
            else
            {
                ((ScintillaEditor)_lstFindEditor[i]).Focus();
            }
        }

        private bool FindNext(string sAllText, string sSearchText, int ii, bool bFindAgain = false)
        {
            bool bResult;

            var iStartOriginal = ((ScintillaEditor)_lstFindEditor[ii]).SelectionStart;
            var iEndOriginal = ((ScintillaEditor)_lstFindEditor[ii]).SelectionEnd;

            if (bFindAgain)
            {
                iStartOriginal = 0;
                iEndOriginal = 0;
            }

            var iStart = 0;
            var iEnd = 0;

            var bMatch = false;

            var array = sAllText.ToCharArray();

            for (var i = iEndOriginal; i < array.Length; i++)
            {
                var letter = array[i];

                if (!string.Equals(letter.ToString(), sSearchText.Substring(0, 1), StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                iStart = i;
                bMatch = true;

                for (var j = 1; j < sSearchText.Length; j++)
                {
                    i++;

                    try
                    {
                        letter = array[i];

                        if (j < sSearchText.Length && string.Equals(letter.ToString(), sSearchText.Substring(j, 1), StringComparison.CurrentCultureIgnoreCase))
                        {
                            //
                        }
                        else
                        {
                            bMatch = false;
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        bMatch = false;
                        break;
                    }
                }

                if (!bMatch)
                {
                    continue;
                }

                iEnd = i;
                break;
            }

            if (iStart >= 0 && (bMatch && iEnd >=0 || iEnd > 0))
            {
                ((ScintillaEditor)_lstFindEditor[ii]).SelectionStart = iStart;
                ((ScintillaEditor)_lstFindEditor[ii]).SelectionEnd = iEnd + 1;
                ((ScintillaEditor)_lstFindEditor[ii]).ScrollCaret();
                bResult = true;
            }
            else
            {
                ((ScintillaEditor)_lstFindEditor[ii]).SelectionStart = iStartOriginal;
                ((ScintillaEditor)_lstFindEditor[ii]).SelectionEnd = iEndOriginal;
                bResult = false;
            }

            return bResult;
        }

        private void FindPreviousKeywords_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripButton btn))
            {
                return;
            }

            int i = Convert.ToInt16(btn.Tag.ToString());

            var sText = ((ScintillaEditor)_lstFindEditor[i]).Text.Trim();
            var sSearchText = ((ToolStripTextBox)_lstFindTextBox[i]).Text.Trim();

            if (string.IsNullOrEmpty(sText) || string.IsNullOrEmpty(sSearchText))
            {
                return;
            }

            if (FindCount(sText, sSearchText) == 0)
            {
                FindAndMoveMsgBox(Cursor.Position.X - 150, Cursor.Position.Y + 30, true, "JasonQuery");
                MessageBox.Show("Can't find the text \"" + sSearchText + "\"", @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (FindPrevious(sText, sSearchText, i) == false)
            {
                if (FindPrevious(sText, sSearchText, i, true))
                {
                    ((ScintillaEditor)_lstFindEditor[i]).Focus();
                }
            }
            else
            {
                ((ScintillaEditor)_lstFindEditor[i]).Focus();
            }
        }

        private bool FindPrevious(string sAllText, string sSearchText, int ii, bool bFindAgain = false)
        {
            bool bResult;

            if (string.IsNullOrEmpty(sSearchText))
            {
                return false;
            }

            var iStartOriginal = ((ScintillaEditor)_lstFindEditor[ii]).SelectionStart;
            var iEndOriginal = ((ScintillaEditor)_lstFindEditor[ii]).SelectionEnd;

            if (bFindAgain)
            {
                iStartOriginal = sAllText.Length;
                iEndOriginal = sAllText.Length;
            }

            var iStart = 0;
            var iEnd = 0;
            var bMatch = false;

            var array = sAllText.ToCharArray();

            if (iStartOriginal - sSearchText.Length <= 0)
            {
                return false;
            }

            for (var i = iStartOriginal; i >= 0; i--)
            {
                if (i - 1 < 0)
                {
                    return false;
                }

                var letter = array[i - 1];

                //最後一個字母符合
                if (!string.Equals(letter.ToString(), sSearchText.Substring(sSearchText.Length - 1, 1), StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }

                iEnd = i - 1;
                bMatch = true;

                for (var j = sSearchText.Length - 1; j > 0; j--)
                {
                    i--;

                    try
                    {
                        letter = array[i - 1];

                        if (j < sSearchText.Length && string.Equals(letter.ToString(), sSearchText.Substring(j - 1, 1), StringComparison.CurrentCultureIgnoreCase))
                        {
                            //bMatch = true;
                        }
                        else
                        {
                            bMatch = false;
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        //
                    }
                }

                if (!bMatch)
                {
                    continue;
                }

                iStart = i - 1;
                break;
            }

            if (iStart >= 0 && ((bMatch && iEnd >= 0) || (iEnd > 0)))
            {
                ((ScintillaEditor)_lstFindEditor[ii]).SelectionStart = iStart;
                ((ScintillaEditor)_lstFindEditor[ii]).SelectionEnd = iEnd + 1;
                ((ScintillaEditor)_lstFindEditor[ii]).ScrollCaret();
                bResult = true;
            }
            else
            {
                ((ScintillaEditor)_lstFindEditor[ii]).SelectionStart = iStartOriginal;
                ((ScintillaEditor)_lstFindEditor[ii]).SelectionEnd = iEndOriginal;
                bResult = false;
            }

            return bResult;
        }

        private static int FindCount(string sText, string sFind)
        {
            var i = 0;

            try
            {
                var matches = Regex.Matches(sText.ToUpper(), sFind.ToUpper());

                i += matches.Cast<Match>().Count();
            }
            catch (Exception)
            {
                //
            } //搜尋 "(" 會出現例外錯誤

            return i;
        }

        private void FindNextKeywords_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(sender is ToolStripTextBox txt))
            {
                return;
            }

            int i = Convert.ToInt16(txt.Tag.ToString());

            if (e.KeyChar == 13 && !string.IsNullOrEmpty(((ToolStripTextBox)_lstFindTextBox[i]).Text.Trim()))
            {
                ((ToolStripButton)_lstFindNextButton[i]).PerformClick();
            }
        }

        private void editor_DoubleClick(object sender, ScintillaNET.DoubleClickEventArgs e)
        {
            HighlightSelection(true);
        }

        private void ApplyIndicatorAppearance(string sColor) //設定 Bookmark 樣式
        {
            var sStyle = "";
            var margin = editorIndicator.Margins[BOOKMARK_MARGIN];
            margin.Width = 15;
            margin.Mask = 0;
            margin.Sensitive = true;
            margin.Type = ScintillaNET.MarginType.Symbol;
            margin.Mask = ScintillaNET.Marker.MaskAll;
            margin.Cursor = ScintillaNET.MarginCursor.Arrow;

            var marker = editorIndicator.Markers[BOOKMARK_MARKER];

            sStyle = MyGlobal.GetKeyFromDictionary(MyGlobal.dicBookmarkStyle, cboBookmarkStyle.Text);

            switch (sStyle)
            {
                case "Arrow":
                    marker.Symbol = ScintillaNET.MarkerSymbol.Arrow; //箭頭
                    break;
                case "Circle":
                    marker.Symbol = ScintillaNET.MarkerSymbol.Circle; //圓形
                    break;
                case "RoundRect":
                    marker.Symbol = ScintillaNET.MarkerSymbol.RoundRect; //圓角矩形
                    break;
                case "SmallRect":
                    marker.Symbol = ScintillaNET.MarkerSymbol.SmallRect; //正方形
                    break;
                default:
                    marker.Symbol = ScintillaNET.MarkerSymbol.ShortArrow; //小箭頭
                    break;
            }

            marker.SetBackColor(ColorTranslator.FromHtml(sColor));
            marker.SetForeColor(Color.Transparent);
            editorIndicator.Lines[editorIndicator.CurrentLine].MarkerAdd(BOOKMARK_MARKER);
            editorIndicator.Margins[0].Width = 0;
        }

        private void SetSquiggle(bool bClearOnly, string sColorErrorLineBackground, int iPos = 0, int iLength = 0)
        {
            //波浪底線
            const int iSquiggleNum = 11;

            editorIndicator.IndicatorCurrent = iSquiggleNum;
            editorIndicator.IndicatorClearRange(0, editorIndicator.TextLength);

            if (bClearOnly)
            {
                return;
            }

            if (string.IsNullOrEmpty(sColorErrorLineBackground))
            {
                sColorErrorLineBackground = MyLibrary.sColorErrorLineBackground;
            }

            editorIndicator.Indicators[iSquiggleNum].ForeColor = ColorTranslator.FromHtml(sColorErrorLineBackground);
            editorIndicator.Indicators[iSquiggleNum].Style = ScintillaNET.IndicatorStyle.Squiggle;
            editorIndicator.IndicatorFillRange(iPos, iLength);
        }

        private void cboBookmarkStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bFormLoadFinish)
            {
                ApplyIndicatorAppearance(pnlBookmarkBackground.Tag.ToString()); //變更 Bookmark Style
            }
        }

        private void cboDateFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bFormLoadFinish)
            {
                CreateVisualStyleInfo(); //cboDateFormat_SelectedIndexChanged
            }
        }

        private void chkResize_CheckedChanged(object sender, EventArgs e)
        {
            lblMaxWidth.Enabled = chkResize.Checked;
            cboMaxWidth.Enabled = chkResize.Checked;
            CreateVisualStyleInfo(); //chkResize_CheckedChanged
        }

        private void chkPagingQuery_CheckedChanged(object sender, EventArgs e)
        {
            lblRowsPerPage.Enabled = chkPagingQuery.Checked;
            cboRowsPerPage.Enabled = chkPagingQuery.Checked;
        }

        private void chkSort_CheckedChanged(object sender, EventArgs e)
        {
            CreateVisualStyleInfo(); //chkSort_CheckedChanged
        }

        private void cboMaxSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateVisualStyleInfo(); //cboMaxSize_SelectedIndexChanged
        }

        private void chkShowStreamlinedName_CheckedChanged(object sender, EventArgs e)
        {
            CreateVisualStyleInfo(); //chkShowStreamlinedName_CheckedChanged
        }

        private void ExportToExcel()
        {
            using (var myForm = new frmExportToFile())
            {
                var sHeader = "";
                var sAge = MyGlobal.GetLanguageString("age", "form", Name, "gridheader", "age", "Text");
                var dtData = new DataTable();
                var dt = (DataTable)c1GridVisualStyle.DataSource;

                foreach (C1DataColumn col1 in c1GridVisualStyle.Columns)
                {
                    sHeader = col1.Caption;
                    dtData.Columns.Add(sHeader, sHeader == sAge ? typeof(int) : typeof(string));
                }

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var rowData = dtData.NewRow();

                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        sHeader = dt.Columns[j].ColumnName;

                        if (dt.Rows[i][dt.Columns[j].ColumnName].ToString() == MyLibrary.sGridNullShowAs)
                        {
                            rowData[sHeader] = DBNull.Value;
                        }
                        else
                        {
                            rowData[sHeader] = dt.Rows[i][dt.Columns[j].ColumnName];
                        }
                    }

                    dtData.Rows.Add(rowData);
                    Application.DoEvents();
                }

                myForm.dtData = dtData;
                myForm.sFontName = c1GridVisualStyle.Font.Name;
                myForm.fFontSize = c1GridVisualStyle.Font.Size;
                myForm.ShowDialog();
            }
        }

        private void CellViewer()
        {
            string sColumnName;
            var sColumnType = "";
            var sCellText = c1GridVisualStyle[c1GridVisualStyle.Row, c1GridVisualStyle.Col].ToString();

            var sTemp = c1GridVisualStyle.Splits[0].DisplayColumns[c1GridVisualStyle.Col].ToString();

            if (sTemp.IndexOf("\n", StringComparison.Ordinal) != -1)
            {
                sColumnName = sTemp.Substring(0, sTemp.IndexOf("\n", StringComparison.Ordinal));
                sColumnType = sTemp.Substring(sTemp.IndexOf("\n", StringComparison.Ordinal) + 1);
            }
            else
            {
                sColumnName = sTemp;
            }

            using (var myForm = new frmCellViewer())
            {
                myForm.sColumnName = sColumnName;
                myForm.sColumnType = sColumnType;
                myForm.sCellText = sCellText;
                myForm.ShowDialog();
            }
        }

        private void c1GridVisualStyle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var iRow = c1GridVisualStyle.RowContaining(e.Y);

            if (iRow != -1)
            {
                CellViewer();
            }
        }

        private void LocationObject()
        {
            int iHeight = Convert.ToInt16(Math.Floor((double)txtHeightCode.Height / 2));

            grpSQLStatementCode.Size = new Size(grpSQLStatementCode.Width, iHeight);
            grpPreviewSQL.Size = new Size(grpSQLStatementCode.Width, iHeight);
            grpPreviewSQL.Location = new Point(grpPreviewSQL.Left, grpSQLStatementCode.Top + grpSQLStatementCode.Height + 4);

            iHeight = Convert.ToInt16(Math.Floor((double)txtHeightFormatter.Height / 2));

            grpSQLStatementFormatter.Size = new Size(grpSQLStatementFormatter.Width, iHeight);
            grpPreviewFormatter.Size = new Size(grpSQLStatementFormatter.Width, iHeight);
            grpPreviewFormatter.Location = new Point(grpPreviewFormatter.Left, grpSQLStatementFormatter.Top + grpSQLStatementFormatter.Height + 4);
        }

        private void cboLocalization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bFormLoadFinish)
            {
                return;
            }

            if (!CheckLocalizationFileExist(true)) //cboLocalization_SelectedIndexChanged
            {
                return;
            }

            if (MyGlobal.sLocalization == cboLocalization.Text)
            {
                return;
            }

            MyGlobal.sLocalization = cboLocalization.Text;
            TransferValueToMainForm("reloadlocalization`");
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

        private void cboGridRowHeightResizing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bFormLoadFinish == false)
            {
                return;
            }

            var sRowSizing = MyGlobal.GetKeyFromDictionary(MyGlobal.dicRowSizing, cboGridRowHeightResizing.Text);

            c1GridVisualStyle.AllowRowSizing = sRowSizing == "AllRows" ? RowSizingEnum.AllRows : RowSizingEnum.IndividualRows;
        }

        private void c1DockingTab_SizeChanged(object sender, EventArgs e)
        {
            LocationObject(); //c1DockingTab_SizeChanged
        }

        private void c1DockingTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            LocationObject(); //c1DockingTab_SelectedIndexChanged
        }

        private void c1DockingTab_SelectedTabChanged(object sender, EventArgs e)
        {
            LocationObject(); //c1DockingTab_SelectedTabChanged
        }

        private void timerTitle_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MyGlobal.sGlobalTemp))
            {
                return;
            }

            timerTitle.Enabled = false;

            var sTitle = MyGlobal.sGlobalTemp.Split(new[] { ";" }, StringSplitOptions.None);
            MyGlobal.sGlobalTemp = "";

            for (var i = 0; i < sTitle.Length; i++)
            {
                if (i > 19)
                {
                    break;
                }

                tabExample.TabPages[i].Title = sTitle[i];                    
            }

            if (sTitle.Length < 20)
            {
                for (var i = 19; i >= sTitle.Length; i--)
                {
                    //tabExample.TabPages.RemoveAt(i);
                    tabExample.TabPages[i].Title = @"Sample Page No." + (i + 1);
                }
            }

            tabExample.TabPages[0].Selected = true;
        }

        private void UpdateMainFormTabVisualStyle()
        {
            try
            {
                if (rdoMultiDocument.Checked)
                {
                    tabExample.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiDocument;
                }
                else if (rdoMultiForm.Checked)
                {
                    tabExample.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiForm;
                }
                else
                {
                    tabExample.Appearance = Crownwood.Magic.Controls.TabControl.VisualAppearance.MultiBox;
                }

                tabExample.Style = rdoIDE.Checked ? VisualStyle.IDE : VisualStyle.Plain;

                tabExample.BoldSelectedPage = chkBold.Checked;
                tabExample.ShrinkPagesToFit = chkShrinkPages.Checked;
                tabExample.ShowArrows = chkShowArrows.Checked;
                tabExample.HoverSelect = chkHoverSelect.Checked;
                tabExample.Multiline = chkMultiLine.Checked;
                tabExample.PositionTop = true;
                tabExample.ShowClose = true;
                tabExample.BorderStyle = BorderStyle.None;
            }
            catch (Exception)
            {
                //
            }
        }

        private void chkTabVisualStyle_CheckedChanged(object sender, EventArgs e)
        {
            if (_bFormLoadFinish == false)
            {
                return;
            }

            if (sender is CheckBox chk)
            {    switch (chk.Name)
                {
                    case "chkShrinkPages":
                    {
                        if (chkShowArrows.Checked)
                            chkShowArrows.Checked = !chkShrinkPages.Checked;
                        break;
                    }
                    case "chkShowArrows":
                    {
                        if (chkShrinkPages.Checked)
                            chkShrinkPages.Checked = !chkShowArrows.Checked;
                        break;
                    }
                    case "chkHoverSelect":
                    {
                        if (chkMultiLine.Checked)
                            chkMultiLine.Checked = !chkHoverSelect.Checked;
                        break;
                    }
                    case "chkMultiLine":
                    {
                        if (chkHoverSelect.Checked)
                            chkHoverSelect.Checked = !chkMultiLine.Checked;
                        break;
                    }
                }
            }

            UpdateMainFormTabVisualStyle(); //chkTabVisualStyle_CheckedChanged
        }

        private void cboEditorFontPicker_TextChanged(object sender, EventArgs e)
        {
            MyLibrary.sQueryEditorFontName = cboEditorFontPicker.Text;
            ApplySqlStyler("editor"); //cboEditorFontName_SelectedIndexChanged
        }

        private void cboGridFontPicker_TextChanged(object sender, EventArgs e)
        {
            float.TryParse(cboGridFontSize.Text, out var iSize);

            if (iSize == 0)
            {
                iSize = 12;
            }

            c1GridVisualStyle.Font = new Font(cboGridFontPicker.Text, iSize, FontStyle.Regular, GraphicsUnit.Point);
            AutoSizeGrid();
            c1GridVisualStyle.Refresh();
        }

        private void pnlOptionsTabClick(object sender, EventArgs e)
        {
            var iX = grpOptionsTab.Left + 19;
            var iY = grpOptionsTab.Top + 43;

            if (!(sender is Panel pnlSelected))
            {
                return;
            }

            _sPanelColorSelectedName = pnlSelected.Name;
            var pt = PointToScreen(pnlSelected.Location);

            pt = new Point(pt.X + iX, pt.Y + pnlSelected.Height + iY);

            var f = new ThemeColorPickerWindow(pt, FormBorderStyle.FixedToolWindow,
                ThemeColorPickerWindow.Action.CloseWindow, ThemeColorPickerWindow.Action.CloseWindow)
            {
                FormBorderStyle = FormBorderStyle.None,
                ActionAfterColorSelected = ThemeColorPickerWindow.Action.CloseWindow
            };

            f.ColorSelected += f_ColorOptionsTab;
            f.Show();
        }

        private void f_ColorOptionsTab(object sender, ColorSelectedArg e)
        {
            for (var i = 0; i < _lstPanelTabColor.Count; i++)
            {
                if (((Panel) _lstPanelTabColor[i]).Name != _sPanelColorSelectedName)
                {
                    continue;
                }

                ((Panel)_lstPanelTabColor[i]).BackColor = e.Color;
                ((Panel)_lstPanelTabColor[i]).Tag = e.HexColor;
                _toolTip1.SetToolTip(((Panel)_lstPanelTabColor[i]), e.HexColor + " " + "(R:" + e.R + ", G:" + e.G + ", B:" + e.B + ")");

                MyGlobal.SetDockingTabColor(c1DockingTab1, pnlOptionsTabActiveBackColor.BackColor, pnlOptionsTabActiveForeColor.BackColor, pnlOptionsTabInactiveForeColor.BackColor);

                break;
            }
        }

        private void txtRecentFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtRecentFiles_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtRecentFiles_Leave(object sender, EventArgs e)
        {
            int.TryParse(txtRecentFiles.Text, out var i);

            if (i < 10 || i > 60)
            {
                txtRecentFiles.Text = @"20";
            }
        }

        private void txtMyFavorite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtMyFavorite_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtMyFavorite_Leave(object sender, EventArgs e)
        {
            int.TryParse(txtMyFavorite.Text, out var i);

            if (i < 10 || i > 60)
            {
                txtMyFavorite.Text = @"20";
            }
        }

        private void editor_Enter(object sender, EventArgs e)
        {
            tsEditor.BackColor = _cEditorFocused;
        }

        private void editor_Leave(object sender, EventArgs e)
        {
            tsEditor.BackColor = _cEditorUnfocused;
        }

        private void cboGridVisualStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyGlobal.ChangeC1TrueDbGridVisualStyle(c1GridVisualStyle, cboGridVisualStyle.Text);
        }

        private void txtRecentFiles_MouseClick(object sender, MouseEventArgs e)
        {
            txtRecentFiles.SelectionStart = 0;
            txtRecentFiles.SelectionLength = 2;
        }

        private void txtMyFavorite_MouseClick(object sender, MouseEventArgs e)
        {
            txtMyFavorite.SelectionStart = 0;
            txtMyFavorite.SelectionLength = 2;
        }

        private void chkShowSaveAsButton_CheckedChanged(object sender, EventArgs e)
        {
            btnSaveAs.Visible = chkShowSaveAsButton.Checked;
        }

        private void Style_CheckedChanged(object sender, EventArgs e)
        {
            UpdateMainFormTabVisualStyle();
        }

        private void CheckForUpdates(object sender, EventArgs e)
        {
            var bValue = !rdoDonotCheck.Checked;
            grpCheckOnly.Enabled = bValue;
        }

        private void btnSpecifiedSQLFile_Click(object sender, EventArgs e)
        {
            var btn = sender as C1.Win.C1Input.C1Button;
            var sTag = btn.Tag.ToString();

            var of = new OpenFileDialog
            {
                Multiselect = false, Filter = @"Query files (*.sql)|*.sql|All files (*.*)|*.*"
            };

            if (of.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            switch (sTag)
            {
                case "1":
                    txtSpecifiedSQLFile1.Text = of.FileName;
                    break;
                case "2":
                    txtSpecifiedSQLFile2.Text = of.FileName;
                    break;
            }
        }

        private void btnHelp_Disconnect_Click(object sender, EventArgs e)
        {
            //取得螢幕解析度
            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;
            var iXTemp = Cursor.Position.X;
            var iYTemp = Cursor.Position.Y;
            var iX = 0;
            var iY = 0;

            if (iXTemp < 300)
            {
                iX = iXTemp - 25;
            }

            if (iWidth - iXTemp < 300)
            {
                iX = 320; //游標很靠近螢幕的右側
            }

            if (iHeight - iYTemp < 300)
            {
                iY = -300; //游標很靠近螢幕的下方
            }

            _sLangText = MyGlobal.GetLanguageString("If your mouse or keyboard have no activity within 1~9 hour(s), JasonQuery will automatically close the database connection.", "form", Name, "msg", "Help_Disconnect", "Text") + "\r\n\r\n";
            _sLangText += MyGlobal.GetLanguageString("When you execute any SQL statement again, JasonQuery will automatically connect to the database.", "form", Name, "msg", "Help_Reconnect", "Text");

            var sTemp1 = MyGlobal.GetLanguageString("If the 'Pooling' function is enabled when the database is connected, the actual connection will not be closed after the connection is disconnected, so that JasonQuery can continue to use it later. This greatly improves performance.", "form", Name, "msg", "Pooling1", "Text");
            var sTemp2 = MyGlobal.GetLanguageString("If the 'Pooling' function is disabled when the database is connected, the database connection will not be seen from the server when the connection is disconnected.", "form", Name, "msg", "Pooling2", "Text");

            FindAndMoveMsgBox(Cursor.Position.X - iX - 30, Cursor.Position.Y + iY + 30, true, "JasonQuery");
            MessageBox.Show(_sLangText + "\r\n\r\n" + sTemp1 + "\r\n\r\n" + sTemp2, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClearFile_Click(object sender, EventArgs e)
        {
            var btn = sender as C1.Win.C1Input.C1Button;
            var sTag = btn.Tag.ToString();

            switch (sTag)
            {
                case "1":
                    txtSpecifiedSQLFile1.Text = "";
                    break;
                case "2":
                    txtSpecifiedSQLFile2.Text = "";
                    break;
            }
        }

        private void chkShowIndentGuide_CheckedChanged(object sender, EventArgs e)
        {
            editor.IndentationGuides = chkShowIndentGuide.Checked ? ScintillaNET.IndentView.LookBoth : ScintillaNET.IndentView.None;

            btnShowIndentGuide.Visible = !chkShowIndentGuide.Checked;
            btnShowIndentGuide2.Visible = chkShowIndentGuide.Checked;
        }

        private void c1GridVisualStyle_Enter(object sender, EventArgs e)
        {
            tsGrid.BackColor = _cEditorFocused;
        }

        private void c1GridVisualStyle_Leave(object sender, EventArgs e)
        {
            tsGrid.BackColor = _cEditorUnfocused;
        }

        private void btnHelp_RawDataMode_Click(object sender, EventArgs e)
        {
            //取得螢幕解析度
            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;

            var iXTemp = Cursor.Position.X;
            var iYTemp = Cursor.Position.Y;
            var iX = 0;
            var iY = 0;

            if (iXTemp < 300)
            {
                iX = iXTemp - 25;
            }

            if (iWidth - iXTemp < 300)
            {
                iX = 320; //游標很靠近螢幕的右側
            }

            if (iHeight - iYTemp < 300)
            {
                iY = -300; //游標很靠近螢幕的下方
            }

            _sLangText = MyGlobal.GetLanguageString("\"Raw Data Mode\": It can speed up data display greatly.", "Global", "Global", "msg", "Help_RawDataMode1", "Text");
            _sLangText += "\r\n\r\n" + MyGlobal.GetLanguageString("When you enable \"Raw Data Mode\", the following settings will not be applied to the presentation of the data table:", "Global", "Global", "msg", "Help_RawDataMode2", "Text");
            _sLangText += "\r\n" + "1. " + MyGlobal.GetLanguageString("Date Format", "Global", "Global", "msg", "Help_RawDataMode3", "Text");
            _sLangText += "\r\n" + "2. " + MyGlobal.GetLanguageString("Null Value Style", "Global", "Global", "msg", "Help_RawDataMode4", "Text");
            _sLangText += "\r\n" + "3. " + MyGlobal.GetLanguageString("Show Column Type", "form", Name, "object", "chkShowColumnType", "Text");
            _sLangText += "\r\n" + "4. " + MyGlobal.GetLanguageString("Automatically Resize Column Width according to Data Result", "form", "frmQuery", "object", "chkSize", "ToolTipText");
            _sLangText += "\r\n" + "5. " + MyGlobal.GetLanguageString("Automatically Sort Data Result according to Column Header", "form", "frmQuery", "object", "chkSort", "ToolTipText");

            FindAndMoveMsgBox(Cursor.Position.X - iX - 30, Cursor.Position.Y + iY + 30, true, "JasonQuery");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHelp_AppendingQueries_Click(object sender, EventArgs e)
        {
            //取得螢幕解析度
            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;

            var iXTemp = Cursor.Position.X;
            var iYTemp = Cursor.Position.Y;
            var iX = 0;
            var iY = 0;

            if (iXTemp < 300)
            {
                iX = iXTemp - 25;
            }

            if (iWidth - iXTemp < 300)
            {
                iX = 320; //游標很靠近螢幕的右側
            }

            if (iHeight - iYTemp < 300)
            {
                iY = -300; //游標很靠近螢幕的下方
            }

            _sLangText = MyGlobal.GetLanguageString("This feature is enabled by default. When you press \"Next Page\", the previous query results will be automatically merged.", "form", Name, "msg", "AppendingQueries", "Text");

            FindAndMoveMsgBox(Cursor.Position.X - iX - 30, Cursor.Position.Y + iY + 30, true, "JasonQuery");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHelp_DarkMode_Click(object sender, EventArgs e)
        {
            _sLangText = MyGlobal.GetLanguageString("You cannot change this setting unless you restart JasonQuery.", "Global", "Global", "msg", "RequestToRestart", "Text");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblStyle_Click(object sender, EventArgs e)
        {
            var lbl = sender as Label;
            var sTag = lbl.Tag.ToString();

            switch (sTag)
            {
                case "1":
                    rdoStyle1.Checked = true;
                    break;
                case "2":
                    rdoStyle2.Checked = true;
                    break;
                case "3":
                    rdoStyle3.Checked = true;
                    break;
            }
        }

        private void btnHelp_ShowColumnInfo_Click(object sender, EventArgs e)
        {
            //取得螢幕解析度
            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;
            var iXTemp = Cursor.Position.X;
            var iYTemp = Cursor.Position.Y;
            var iX = 0;
            var iY = 0;

            if (iXTemp < 300)
            {
                iX = iXTemp - 25;
            }

            if (iWidth - iXTemp < 300)
            {
                iX = 320; //游標很靠近螢幕的右側
            }

            if (iHeight - iYTemp < 300)
            {
                iY = -300; //游標很靠近螢幕的下方
            }

            _sLangText = MyGlobal.GetLanguageString("Show column information in the Query Editor's Schema Browser.", "Global", "Global", "msg", "InfoShowColumnInfo1", "Text") + "\r\n\r\n";
            _sLangText += MyGlobal.GetLanguageString("If it is enabled, it may take a long time to get all the column information.", "Global", "Global", "msg", "InfoShowColumnInfo2", "Text");

            FindAndMoveMsgBox(Cursor.Position.X - iX - 30, Cursor.Position.Y + iY + 30, true, "JasonQuery");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHelp_SavePoint_Click(object sender, EventArgs e)
        {
            //取得螢幕解析度
            var iWidth = Screen.PrimaryScreen.Bounds.Width;
            var iHeight = Screen.PrimaryScreen.Bounds.Height;
            var iXTemp = Cursor.Position.X;
            var iYTemp = Cursor.Position.Y;
            var iX = 0;
            var iY = 0;

            if (iXTemp < 300)
            {
                iX = iXTemp - 25;
            }

            if (iWidth - iXTemp < 300)
            {
                iX = 320; //游標很靠近螢幕的右側
            }

            if (iHeight - iYTemp < 300)
            {
                iY = -300; //游標很靠近螢幕的下方
            }

            _sLangText = MyGlobal.GetLanguageString("JasonQuery automatically creates a savepoint before executing the \"Auto List Members\" subquery.", "form", Name, "msg", "Help_SavePoint1", "Text") + "\r\n";
            _sLangText += MyGlobal.GetLanguageString("If there is an error in the execution of the subquery, the savepoint is automatically restored.", "form", Name, "msg", "Help_SavePoint2", "Text") + "\r\n\r\n";
            _sLangText += MyGlobal.GetLanguageString("The above only applies to PostgreSQL.", "form", Name, "msg", "Help_SavePoint3", "Text");

            FindAndMoveMsgBox(Cursor.Position.X - iX - 30, Cursor.Position.Y + iY + 30, true, "JasonQuery");
            MessageBox.Show(_sLangText, @"JasonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtMaxWidth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Back)
            {
                return;
            }

            if (string.IsNullOrEmpty(txtMaxWidth.Text) || int.Parse(txtMaxWidth.Text) < 100)
            {
                txtMaxWidth.Text = @"99";
            }
        }
    }
}