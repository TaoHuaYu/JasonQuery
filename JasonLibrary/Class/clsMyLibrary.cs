using System;
using System.Drawing;
using System.Windows.Forms;
using PoorMansTSqlFormatterLib.Interfaces;

namespace JasonLibrary.Class
{
    public static class MyLibrary
    {
        private static ISqlTokenizer _tokenizer;
        private static ISqlTokenParser _parser;
        private static ISqlTreeFormatter _formatter;

        private static string _sColorToolstripBackground = "";
        private static string _sColorEditorBackground = "";
        private static string _sColorCurrentLineBackground = "";
        private static string _sColorSelectedTextBackground = "";
        private static string _sColorErrorLineBackground = "";
        private static string _sBookmarkStyle = "";
        private static string _sColorBookmarkBackground = "";
        private static string _sColorComments = "";
        private static string _sColorTextIdentifier = "";
        private static string _sColorBuiltInKeywords = "";
        private static string _sColorUserDefinedKeywords = "";
        private static string _sColorNumber = "";
        private static string _sColorOperatorSymbol = "";
        private static string _sColorOperatorKeywords = "";
        private static string _sColorString = "";
        private static string _sColorCharacter = "";
        private static string _sColorBuiltInFunctions = "";
        private static string _sColorWhiteSpace = "";
        private static string _sColorUserDefinedTablesViews = "";
        private static string _sColorUserDefinedFunctionsTriggers = "";
        private static string _sColorOptionsTabActiveForeColor = "";
        private static string _sColorOptionsTabActiveBackColor = "";
        private static string _sColorOptionsTabInactiveForeColor = "";

        private static string _sTabStyle = ""; //IDE, Plain
        private static string _sTabAppearance = ""; //MultiDocument, MultiForm, MultiBox
        private static int _iACMinFragmentLength = 2;
        private static int _iCheckForUpdate = 7;

        private static int _iRecentFilesQty = 20;
        private static int _iMyFavoriteQty = 20;
        private static string _sGridQuotingWith = "";
        private static string _sGridFieldSeparator = "";
        private static string _sDateFormat = "";
        private static string _sGridMaxWidth = "";
        private static string _sGridNullShowAs = "";
        private static string _sGridRowsPerPage = "";
        private static string _sGridNullShowColor = "";
        private static string _sGridVisualStyle = "";
        private static string _sGridZoom = "";
        private static string _sGridFontName = "";
        private static string _sGridFontSize = "";
        private static string _sGridSheetName = "";
        private static string _sGridHeadingForeColor = "";
        private static string _sGridEvenRowForeColor = "";
        private static string _sGridEvenRowBackColor = "";
        private static string _sGridOddRowForeColor = "";
        private static string _sGridOddRowBackColor = "";
        private static string _sGridHighlightForeColor = "";
        private static string _sGridHighlightBackColor = "";
        private static string _sGridSelectedForeColor = "";
        private static string _sGridSelectedBackColor = "";
        private static string _sGridExcelSaveAsType = "";

        //Query Editor 頁籤：Highlight
        private static string _sHighlightColorForeColor = "";
        private static string _sHighlightColorStyle = "";
        private static string _sHighlightColorOutlineAlpha = "";
        private static string _sHighlightColorAlpha = "";

        //Query Editor 頁籤：Preferences
        private static string _sQueryEditorFontName = "";
        private static string _sQueryEditorFontSize = "";
        private static string _sQueryEditorZoom = "";
        private static string _sWordWrapIndentMode = "";

        //SQL To Code 頁籤：
        private static string _sSQLToCodeVariableName = "";

        //SQL Formatter 頁籤：
        private static string _sSQLFormatterIndentString = "";
        private static string _sGenerateSQLConvertCase = "";
        private static int _iGenerateSQLNumbers = 5;

        private static string _sRowSizing = "";
        private static string _sAutoDisconnect = "";

        public static bool bDarkMode { get; set; }

        public static string sTabStyle
        {
            get => _sTabStyle;
            set => _sTabStyle = "`IDE`Plain`".Contains("`" + value + "`") ? value : "IDE";
        }

        public static string sTabAppearance
        {
            get => _sTabAppearance;
            set => _sTabAppearance = "`MultiDocument`MultiForm`MultiBox`".Contains("`" + value + "`") ? value : "MultiForm";
        }

        public static string sColorOptionsTabActiveForeColor
        {
            get => _sColorOptionsTabActiveForeColor;
            set => _sColorOptionsTabActiveForeColor = value.Length == 7 && CheckColorCode(value) ? value : "#000000"; //Black
        }

        public static string sColorOptionsTabActiveBackColor
        {
            get => _sColorOptionsTabActiveBackColor;
            set => _sColorOptionsTabActiveBackColor = value.Length == 7 && CheckColorCode(value) ? value : "#E6FFFF"; //淡藍
        }

        public static string sColorOptionsTabInactiveForeColor
        {
            get => _sColorOptionsTabInactiveForeColor;
            set => _sColorOptionsTabInactiveForeColor = value.Length == 7 && CheckColorCode(value) ? value : "#595959"; //DarkGray
        }

        public static string sHighlightColorForeColor
        {
            get => _sHighlightColorForeColor;
            set => _sHighlightColorForeColor = value.Length == 7 && CheckColorCode(value) ? value : "#0000FF"; //Color.Blue
        }

        public static string sHighlightColorStyle
        {
            get => _sHighlightColorStyle;
            set => _sHighlightColorStyle = "`Box`CompositionThick`Dash`Diagonal`StraightBox`".Contains("`" + value + "`") ? value : "StraightBox";
        }

        public static string sHighlightColorOutlineAlpha
        {
            get => _sHighlightColorOutlineAlpha;
            set => _sHighlightColorOutlineAlpha = "`50`60`70`80`90`100`110`120`130`140`150`160`170`180`190`200`".Contains("`" + value + "`") ? value : "50";
        }

        public static string sHighlightColorAlpha
        {
            get => _sHighlightColorAlpha;
            set => _sHighlightColorAlpha = "`50`60`70`80`90`100`110`120`130`140`150`160`170`180`190`200`".Contains("`" + value + "`") ? value : "50";
        }

        public static string sQueryEditorFontName
        {
            get => _sQueryEditorFontName;
            set => _sQueryEditorFontName = string.IsNullOrWhiteSpace(value) == false ? value : "Consolas";
        }

        public static string sQueryEditorFontSize
        {
            get => _sQueryEditorFontSize;
            set => _sQueryEditorFontSize = "`10`12`14`16`18`".Contains("`" + value + "`") ? value : "12";
        }

        public static string sQueryEditorZoom
        {
            get => _sQueryEditorZoom;
            set => _sQueryEditorZoom = "`-2`-1`0`1`2`".Contains("`" + value + "`") ? value : "1";
        }

        public static string sRecentFilesHistory { get; set; } = ""; //Recent Files History

        public static string sDefaultDirectory { get; set; } = ""; //Default Directory

        public static string sGridQuotingWith //Gird's Visual Style
        {
            get => _sGridQuotingWith;
            set => _sGridQuotingWith = "`None`\"`'`".Contains("`" + value + "`") ? value : "None";
        }

        public static string sGridFieldSeparator
        {
            get => _sGridFieldSeparator;
            set => _sGridFieldSeparator = string.IsNullOrWhiteSpace(value) == false && "`,`;`|`TAB`".Contains("`" + value.ToUpper() + "`") ? value.ToUpper() : ",";
        }

        public static string sDateFormat
        {
            get => _sDateFormat;
            set => _sDateFormat = "`YYYY/MM/DD`YYYY-MM-DD`MM/DD/YYYY`MM-DD-YYYY`DD/MM/YYYY`DD-MM-YYYY`".Contains("`" + value.ToUpper() + "`") ? value : "yyyy/MM/dd";
        }

        public static bool bShowVersion { get; set; } = true; //是否顯示版號？

        public static bool bHideClock { get; set; } = false; //是否顯示時鐘？

        public static bool bGridShowColumnDataType { get; set; } //是否啟用 Show Column's Data Type？

        public static bool bGridShowStreamlinedName { get; set; } //是否啟用 Show Streamlined Name

        public static bool bGridShowFilterRow { get; set; } //是否啟用 Show Filter Row？

        public static bool bGridShowGroupingRow { get; set; } //是否啟用 Show Filter Row？


        public static bool bGridResize { get; set; } //是否啟用 Auto Resize？


        public static string sGridMaxWidth //Column's Max Width
        {
            get => _sGridMaxWidth;
            set => _sGridMaxWidth = "`Unlimited`500`1000`1500`2000`".ToUpper().Contains("`" + value.ToUpper() + "`") ? value : "Unlimited";
        }

        public static bool bGridSort { get; set; } //是否啟用 Auto Sort？


        public static bool bGridRawDataMode { get; set; } //是否啟用 Raw Data Mode？


        public static string sGridNullShowAs //Null Values 顯示方式
        {
            get => _sGridNullShowAs;
            set => _sGridNullShowAs = "`none`<null>`{null}`(null)`".ToUpper().Contains("`" + value.ToUpper() + "`") ? value : "<NULL>";
        }

        public static bool bGridPagingQuery { get; set; } = true; //是否啟用 分頁查詢？


        public static string sGridRowsPerPage //分頁查詢每頁的筆數
        {
            get => _sGridRowsPerPage;
            set => _sGridRowsPerPage = "`100`200`300`400`500`1000`2000`5000`".Contains("`" + value + "`") ? value : "500";
        }

        public static bool bGridAppendingQueries { get; set; } = true; //是否啟用 附加查詢？


        public static bool bGridSetFocusAfterQuery { get; set; } = true; //是否啟用 查詢後切換到 Grid？

        public static string sGridNullShowColor
        {
            get => _sGridNullShowColor;
            set
            {
                if (value.Length == 7 && CheckColorCode(value))
                {
                    _sGridNullShowColor = value;
                }
                else
                {
                    _sGridNullShowColor = "#0000FF"; //標準藍色
                }
            }
        }

        public static string sGridVisualStyle //Gird's Visual Style
        {
            get => _sGridVisualStyle;
            set => _sGridVisualStyle = "`Office 2007 Blue`Office 2007 Silver`Office 2007 Black`Office 2010 Blue`Office 2010 Silver`Office 2010 Black`".Contains("`" + value + "`") ? value : "Office 2010 Blue";
        }

        public static string sGridZoom
        {
            get => _sGridZoom;
            set => _sGridZoom = "`-2`-1`0`1`2`".Contains("`" + value + "`") ? value : "1";
        }

        public static string sGridFontName
        {
            get => _sGridFontName;
            set => _sGridFontName = string.IsNullOrWhiteSpace(value) == false ? value : "Consolas";
        }

        public static string sGridFontSize
        {
            get => _sGridFontSize;
            set => _sGridFontSize = "`9`10`11`12`13`14`15`16`17`18`".Contains("`" + value + "`") ? value : "12";
        }

        public static string sGridSheetName
        {
            get => _sGridSheetName;
            set => _sGridSheetName = string.IsNullOrWhiteSpace(value) == false ? value : "Data";
        }

        public static string sGridHeadingForeColor
        {
            get => _sGridHeadingForeColor;
            set
            {
                if (value.Length == 7 && CheckColorCode(value))
                {
                    _sGridHeadingForeColor = value;
                }
                else
                {
                    _sGridHeadingForeColor = "#000000"; //白色=#FFFFFF, 黑色=#000000
                }
            }
        }

        public static string sGridEvenRowForeColor
        {
            get => _sGridEvenRowForeColor;
            set
            {
                if (value.Length == 7 && CheckColorCode(value))
                {
                    _sGridEvenRowForeColor = value;
                }
                else
                {
                    _sGridEvenRowForeColor = "#000000"; //白色=#FFFFFF, 黑色=#000000
                }
            }
        }

        public static string sGridEvenRowBackColor
        {
            get => _sGridEvenRowBackColor;
            set => _sGridEvenRowBackColor = value.Length == 7 && CheckColorCode(value) ? value : "#FFFFFF"; //白色=#FFFFFF, 黑色=#000000
        }

        public static string sGridOddRowForeColor
        {
            get => _sGridOddRowForeColor;
            set => _sGridOddRowForeColor = value.Length == 7 && CheckColorCode(value) ? value : "#000000"; //白色=#FFFFFF, 黑色=#000000
        }

        public static string sGridOddRowBackColor
        {
            get => _sGridOddRowBackColor;
            set => _sGridOddRowBackColor = value.Length == 7 && CheckColorCode(value) ? value : "#FFFFC1"; //淺藍色=#DBEEF3, 淺黃色=#FFFFC1
        }

        public static string sGridHighlightForeColor
        {
            get => _sGridHighlightForeColor;
            set => _sGridHighlightForeColor = value.Length == 7 && CheckColorCode(value) ? value : "#000000"; //黑色=#000000
        }

        public static string sGridHighlightBackColor
        {
            get => _sGridHighlightBackColor;
            set => _sGridHighlightBackColor = value.Length == 7 && CheckColorCode(value) ? value : "#92D050"; //翠綠色=#92D050
        }

        public static string sGridSelectedForeColor
        {
            get => _sGridSelectedForeColor;
            set => _sGridSelectedForeColor = value.Length == 7 && CheckColorCode(value) ? value : "#000000"; //黑色=#000000
        }

        public static string sGridSelectedBackColor
        {
            get => _sGridSelectedBackColor;
            set => _sGridSelectedBackColor = value.Length == 7 && CheckColorCode(value) ? value : "#C6D9F0"; //淡藍色=#C6D9F0
        }

        public static string sGridExcelFilename { get; set; } = ""; //Excel 匯出的檔名

        public static string sGridCSVDelimiters { get; set; } = ""; //匯出 CSV 的分隔符號

        public static string sGridEncoding { get; set; } = ""; //匯出 CSV 的編碼

        public static string sGridExcelWorksheetName { get; set; } = ""; //Excel 匯出的工作表名稱

        public static string sGridExcelSaveAsType //Excel 匯出的存檔類型
        {
            get => _sGridExcelSaveAsType;
            
            set
            {
                switch (value)
                {
                    case @"Excel 2007 (*.xlsx)":
                    case @"Excel 2003 (*.xls)":
                    case @"XML A (*.xml)":
                    case @"XML B (*.xml)":
                    case @"XML C (*.xml)":
                    case @"XML DataPacket 2.0 (*.xml)":
                    case @"XML Access (*.xml)":
                    case @"JSON (*.json)":
                    case @"HTML (*.html)":
                    case @"PDF (*.pdf)":
                        _sGridExcelSaveAsType = value;
                        break;
                    default:
                        _sGridExcelSaveAsType = @"CSV (*.csv)";
                        break;
                }
            }
        }

        public static bool bGridConvertCRLF { get; set; } = false; //是否置換 CR LF

        public static bool bGridExcelAutoOpen { get; set; } = true; //是否自動開啟 Excel？

        public static bool bGridExcelAutoColumnResize { get; set; } = true; //是否自動調整欄寬？

        public static bool bGridExcelCustom { get; set; } //是否自訂外觀樣式？

        public static string sGridExcelHeadingBackColor { get; set; } = "";

        public static string sGridExcelEvenRowBackColor { get; set; } = "";

        public static string sGridExcelOddRowBackColor { get; set; } = "";

        public static string sGridExcelFontName { get; set; } = "";

        public static string sGridExcelFontSize { get; set; } = "";

        public static string sGridExcelRowHeight { get; set; } = "";

        public static bool bEnableAutoReplace { get; set; } //是否啟用 Auto Replace？

        public static bool bCheckForUpdate { get; set; } = true; //是否啟用 Check For Update？

        public static int iCheckForUpdate //Check For Update 頻率
        {
            get => _iCheckForUpdate;
            set => _iCheckForUpdate = "`0`1`7`".Contains("`" + value + "`") ? value : 7;
        }

        public static int iRecentFilesQty
        {
            get => _iRecentFilesQty;
            set => _iRecentFilesQty = value >= 10 && value <= 99 ? value : 20;
        }

        public static int iMyFavoriteQty
        {
            get => _iMyFavoriteQty;
            set => _iMyFavoriteQty = value >= 10 && value <= 99 ? value : 20;
        }

        public static bool bEnableAutoComplete { get; set; } //是否啟用 Auto Complete？

        public static int iACMinFragmentLength //Auto Complete 最小觸發長度
        {
            get => _iACMinFragmentLength;
            set => _iACMinFragmentLength = "`2`3`4`5`6`7`8`9`".Contains("`" + value + "`") ? value : 2;
        }

        public static bool bACFirstCharChecking { get; set; } //是否啟用 Auto Complete - First Char Checking？

        public static bool bACBuiltInKeywords { get; set; } //是否啟用 Auto Complete - BuiltInKeywords？

        public static bool bACBuiltInFunctions { get; set; } //是否啟用 Auto Complete - BuiltInKeywords？

        public static bool bACUserDefinedKeywords { get; set; } //是否啟用 Auto Complete - UserDefinedKeywords？

        public static bool bACUserDefinedFunctions { get; set; } //是否啟用 Auto Complete - UserDefinedFunctions？

        public static bool bACUserDefinedTables { get; set; } //是否啟用 Auto Complete - UserDefinedTables？

        public static bool bACUserDefinedTriggers { get; set; } //是否啟用 Auto Complete - UserDefinedTriggers？

        public static bool bACUserDefinedViews { get; set; } //是否啟用 Auto Complete - UserDefinedViews？

        public static string sWordWrapIndentMode
        {
            get => _sWordWrapIndentMode;
            set => _sWordWrapIndentMode = "`Fixed`Same`Indent`".Contains("`" + value + "`") ? value : "Same";
        }

        public static string sColorToolstripBackground
        {
            get => _sColorToolstripBackground;
            set => _sColorToolstripBackground = value.Length == 7 && CheckColorCode(value) ? value : "#E3FDCA"; //#E6FFFF=淡藍；#FFFFD0=淡黃；#E3FDCA=淡綠
        }

        public static string sColorEditorBackground
        {
            get => _sColorEditorBackground;
            set => _sColorEditorBackground = value.Length == 7 && CheckColorCode(value) ? value : "#FFFFFF";
        }

        public static string sColorCurrentLineBackground
        {
            get => _sColorCurrentLineBackground;
            set => _sColorCurrentLineBackground = value.Length == 7 && CheckColorCode(value) ? value : "#FFFFE0"; //LightYellow
        }

        public static string sColorSelectedTextBackground
        {
            get => _sColorSelectedTextBackground;
            set => _sColorSelectedTextBackground = value.Length == 7 && CheckColorCode(value) ? value : "#ADD8E6"; //LightBlue
        }

        public static string sColorErrorLineBackground
        {
            get => _sColorErrorLineBackground;
            set => _sColorErrorLineBackground = value.Length == 7 && CheckColorCode(value) ? value : "#FF0000"; //Red
        }

        public static string sBookmarkStyle
        {
            get => _sBookmarkStyle;
            set => _sBookmarkStyle = "`Arrow`Circle`RoundRect`ShortArrow`SmallRect`".Contains("`" + value + "`") ? value : "ShortArrow";
        }

        public static string sColorBookmarkBackground
        {
            get => _sColorBookmarkBackground;
            set => _sColorBookmarkBackground = value.Length == 7 && CheckColorCode(value) ? value : "#00FFFF"; //Cyan 亮青
        }

        public static string sColorComments
        {
            get => _sColorComments;
            set => _sColorComments = value.Length == 7 && CheckColorCode(value) ? value : "#008000"; //Green

        }

        public static string sColorTextIdentifier
        {
            get => _sColorTextIdentifier;
            set => _sColorTextIdentifier = value.Length == 7 && CheckColorCode(value) ? value : "#000000"; //Text, Black
        }

        public static string sColorBuiltInKeywords
        {
            get => _sColorBuiltInKeywords;
            set => _sColorBuiltInKeywords = value.Length == 7 && CheckColorCode(value) ? value : "#0000FF"; //LightSeaGreen
        }

        public static string sColorUserDefinedKeywords
        {
            get => _sColorUserDefinedKeywords;
            set => _sColorUserDefinedKeywords = value.Length == 7 && CheckColorCode(value) ? value : "#0000FF"; //LightSeaGreen
        }

        public static string sColorNumber
        {
            get => _sColorNumber;
            set => _sColorNumber = value.Length == 7 && CheckColorCode(value) ? value : "#800000"; //Maroon
        }

        public static string sColorOperatorSymbol
        {
            get => _sColorOperatorSymbol;
            set => _sColorOperatorSymbol = value.Length == 7 && CheckColorCode(value) ? value : "#800000"; //Maroon
        }

        public static string sColorOperatorKeywords
        {
            get => _sColorOperatorKeywords;
            set => _sColorOperatorKeywords = value.Length == 7 && CheckColorCode(value) ? value : "#366092"; //靛藍色
        }

        public static string sColorString
        {
            get => _sColorString;
            set => _sColorString = value.Length == 7 && CheckColorCode(value) ? value : "#FF0000"; //Red
        }

        public static string sColorCharacter
        {
            get => _sColorCharacter;
            set => _sColorCharacter = value.Length == 7 && CheckColorCode(value) ? value : "#FF0000"; //Red
        }

        public static string sColorBuiltInFunctions
        {
            get => _sColorBuiltInFunctions;
            set => _sColorBuiltInFunctions = value.Length == 7 && CheckColorCode(value) ? value : "#FF00FF"; //Magenta
        }

        public static string sColorWhiteSpace
        {
            get => _sColorWhiteSpace;
            set => _sColorWhiteSpace = value.Length == 7 && CheckColorCode(value) ? value : "#00FFFF"; //Cyan
        }

        public static string sColorUserDefinedTablesViews
        {
            get => _sColorUserDefinedTablesViews;
            set => _sColorUserDefinedTablesViews = value.Length == 7 && CheckColorCode(value) ? value : "#808000"; //Olive (仿 Toad Table Name Color)
        }

        public static string sColorUserDefinedFunctionsTirggers
        {
            get => _sColorUserDefinedFunctionsTriggers;
            set => _sColorUserDefinedFunctionsTriggers = value.Length == 7 && CheckColorCode(value) ? value : "#808000"; //Olive (仿 Toad Table Name Color)
        }

        public static string sKeywordsOperatorKeywords { get; set; } = "";

        public static string sKeywordsBuiltInFunctions { get; set; } = "";

        public static string sKeywordsBuiltInKeywords { get; set; } = "";

        public static string sKeywordsUserDefinedKeywords { get; set; } = "";

        public static string sKeywordsUserDefinedTables { get; set; } = "";

        public static string sKeywordsUserDefinedViews { get; set; } = "";

        public static string sKeywordsUserDefinedFunctions { get; set; } = "";

        public static string sKeywordsUserDefinedTriggers { get; set; } = "";

        public static string sSQLToCodeVariableName
        {
            get => _sSQLToCodeVariableName;
            set => _sSQLToCodeVariableName = !string.IsNullOrWhiteSpace(value) ? value : "sSQL";
        }

        public static string sSQLFormatterIndentString
        {
            get => _sSQLFormatterIndentString;
            set => _sSQLFormatterIndentString = !string.IsNullOrWhiteSpace(value) ? value : "\t";
        }

        public static int iSQLFormatterSpacesPerTab { get; set; } = 4;

        public static int iSQLFormatterMaxLineWidth { get; set; } = 4;

        public static int iSQLFormatterNewStatementLineBreaks { get; set; } = 4;

        public static int iSQLFormatterNewClauseLineBreaks { get; set; } = 4;

        public static bool bSQLFormatterExpandCommaLists { get; set; }

        public static bool bSQLFormatterTrailingCommas { get; set; }

        public static bool bSQLFormatterExpandBooleanExpressions { get; set; }

        public static bool bSQLFormatterExpandCaseStatements { get; set; }

        public static bool bSQLFormatterExpandBetweenConditions { get; set; }

        public static bool bSQLFormatterExpandInLists { get; set; }

        public static bool bSQLFormatterBreakJoinOnSections { get; set; }

        public static bool bSQLFormatterConvertCaseForKeywords { get; set; }

        public static int iSQLFormatterConvertCaseForKeywordsCase { get; set; } = 1;

        public static string sGenerateSQLConvertCase
        {
            get => _sGenerateSQLConvertCase;
            set => _sGenerateSQLConvertCase = "`UpperAll`UpperKeywords`LowerAll`".Contains("`" + value + "`") ? value : "UpperAll";
        }

        public static int iGenerateSQLNumbers //GenerateSQL: 每列 Select 欄位名稱的數量
        {
            get => _iGenerateSQLNumbers;
            set => _iGenerateSQLNumbers = "`1`2`3`4`5`6`7`8`9`10`".Contains("`" + value + "`") ? value : 5;
        }

        public static bool bKeywordFontBold { get; set; }

        public static bool bShowAllCharacters { get; set; }

        public static bool bShowSaveAsButton { get; set; }

        public static bool bShowIndentGuide { get; set; } = true;

        public static bool bEntireBlankRowAsEmptyRow { get; set; } = true;

        public static bool bOpenFileOnCurrentTab { get; set; } //20200822, always false

        public static bool bHighlightSelection { get; set; } = true;

        public static bool bCopyAsHTML { get; set; }

        public static bool bWordWrap { get; set; }

        public static bool bWordWrapVisualFlags_Start { get; set; }

        public static bool bWordWrapVisualFlags_End { get; set; }

        public static bool bWordWrapVisualFlags_Margin { get; set; }

        public static string sRowSizing
        {
            get => _sRowSizing;
            set => _sRowSizing = "`AllRows`IndividualRows`".Contains("`" + value + "`") ? value : "AllRows";
        }

        public static string sAutoDisconnect
        {
            get => _sAutoDisconnect;
            set => _sAutoDisconnect = "`Never`1hr`3hrs`5hrs`7hrs`9hrs`".Contains("`" + value + "`") ? value : "Never";
        }

        public static string GetRgbColorCode(string sHtmlColorCode)
        {
            var color = ColorTranslator.FromHtml(sHtmlColorCode);
            return "(R:" + color.R + ", G:" + color.G + ", B:" + color.B + ")";
        }

        private static bool CheckColorCode(string sColorCode)
        {
            var regexColorCode = new System.Text.RegularExpressions.Regex("^#[a-fA-F0-9]{6}$");

            return regexColorCode.IsMatch(sColorCode.Trim());
        }

        public static string SQLToCode(string sSql, string sVaribleNameFirst, string sLanguage, string sStyle, bool bCopyToClipboard = true)
        {
            var sVaribleNameNext = "";
            var sDoubleQualReplacement = "\"";
            var sSymbolEqualFirst = "=";
            var sSymbolEqualNext = "=";
            var sSymbolEnd = "";
            var sSymbolQuoted = "\"";
            var sConcatenateOperator = "";
            var sNewLineCharacter = "";

            switch (sLanguage)
            {
                case "Delphi6":
                    sSymbolEqualFirst = ":=";
                    sSymbolEqualNext = ":=";
                    sSymbolQuoted = "'";
                    sSymbolEnd = ";";
                    //sNewLineCharacter = "+ #13#10;";
                    sVaribleNameNext = sVaribleNameFirst + " + ";
                    break;
                case "C#":
                    sSymbolEnd = ";";
                    break;
            }

            switch (sStyle)
            {
                case "1":
                    switch (sLanguage)
                    {
                        case "C#":
                            sConcatenateOperator = "+";
                            sDoubleQualReplacement = "\\\"";
                            break;
                        case "Delphi6":
                            sConcatenateOperator = "+";
                            sDoubleQualReplacement = "\"";
                            break;
                        case "VB.Net":
                            sConcatenateOperator = "&";
                            sDoubleQualReplacement = "\"\"";
                            break;
                        default:
                            sConcatenateOperator = "& _";
                            sDoubleQualReplacement = "\"\"";
                            break;
                    }

                    break;
                case "2":
                    switch (sLanguage)
                    {
                        case "C#":
                            sNewLineCharacter = "\r\n;";
                            sSymbolEqualNext = "+=";
                            sDoubleQualReplacement = "\\\"";
                            break;
                        case "VB.Net":
                            sNewLineCharacter = "& VbCrLf";
                            sSymbolEqualNext = "+=";
                            sDoubleQualReplacement = "\"\"";
                            break;
                        case "Delphi6":
                            sNewLineCharacter = "+ #13#10";
                            sDoubleQualReplacement = "\"";
                            break;
                        default:
                            sNewLineCharacter = "& VbCrLf";
                            sVaribleNameNext = sVaribleNameFirst + " & ";
                            sDoubleQualReplacement = "\"\"";
                            break;
                    }

                    break;
                case "3":
                    if (sLanguage == "C#")
                    {
                        sNewLineCharacter = "+ Environment.NewLine";
                        sDoubleQualReplacement = "\\\"";
                    }
                    else //if (sLanguage == "VB.Net")
                    {
                        sNewLineCharacter = "& Environment.NewLine";
                        sDoubleQualReplacement = "\"\"";
                    }

                    sSymbolEqualNext = "+=";

                    break;
            }

            return SQLToCode2(sSql, sDoubleQualReplacement, sVaribleNameFirst, sSymbolEqualFirst, sVaribleNameNext, sSymbolEqualNext, sSymbolQuoted, sConcatenateOperator, sNewLineCharacter, sSymbolEnd, bCopyToClipboard);
        }

        private static string SQLToCode2(string sSQL, string sDoubleQualReplacement, string sVaribleNameFirst, string sSymbolEqualFirst, string sVaribleNameNext, string sSymbolEqualNext, string sSymbolQuoted, string sConcateOperator, string sNewLineCharacter, string sSymbolEnd, bool bCopyToClipboard = true)
        {
            var sResult = "";
            var sLine = "";
            var parts = sSQL.Split(new[] { "\r\n" }, StringSplitOptions.None); //StringSplitOptions.RemoveEmptyEntries

            if (!string.IsNullOrEmpty(sConcateOperator)) //Style 1
            {
                for (var i = 0; i < parts.Length; i++)
                {
                    sLine = parts[i];

                    if (i < parts.Length - 1 && !sLine.EndsWith(" "))
                    {
                        sLine += " ";
                    }

                    if (sSymbolEqualFirst == ":=") //Delphi
                    {
                        sLine = sLine.Replace("'", "''"); //直接將單引號置換成兩個單引號
                    }

                    if (i == 0)
                    {
                        sResult += sVaribleNameFirst + " " + sSymbolEqualNext + " " + sSymbolQuoted + sLine.Replace("\"", sDoubleQualReplacement) + sSymbolQuoted + " " + sConcateOperator;
                    }
                    else
                    {
                        sResult += new string(' ', sVaribleNameFirst.Length) + " " + new string(' ', sSymbolEqualNext.Length) + " " + sSymbolQuoted + sLine.Replace("\"", sDoubleQualReplacement) + sSymbolQuoted + " " + sConcateOperator;
                    }

                    if (i < parts.Length - 1) //不是選取區的最後一列，後面加上換行符號
                    {
                        sResult += "\r\n";
                    }
                }

                try
                {
                    sResult = sResult.Substring(0, sResult.Length - (sConcateOperator.Length + 1)) + sSymbolEnd;
                }
                catch (Exception)
                {
                    //do nothing
                }
            }
            else //Style 2, 3
            {
                for (var i = 0; i < parts.Length; i++)
                {
                    sLine = parts[i];

                    if (sSymbolEqualFirst == ":=") //Delphi
                    {
                        sLine = sLine.Replace("'", "''"); //直接將單引號置換成兩個單引號
                    }

                    if (i == 0)
                    {
                        sResult += sVaribleNameFirst + " " + sSymbolEqualFirst + " " + sSymbolQuoted + sSymbolQuoted + sSymbolEnd + "\r\n";

                        if (sNewLineCharacter == "\r\n;")
                        {
                            sResult += sVaribleNameFirst + " " + sSymbolEqualNext + " " + sVaribleNameNext + sSymbolQuoted + sLine.Replace("\"", sDoubleQualReplacement) + @"\r\n" + sSymbolQuoted + sSymbolEnd;
                        }
                        else
                        {
                            sResult += sVaribleNameFirst + " " + sSymbolEqualNext + " " + sVaribleNameNext + sSymbolQuoted + sLine.Replace("\"", sDoubleQualReplacement) + sSymbolQuoted + " " + sNewLineCharacter + sSymbolEnd;
                        }
                    }
                    else
                    {
                        if (sNewLineCharacter == "\r\n;")
                        {
                            sResult += sVaribleNameFirst + " " + sSymbolEqualNext + " " + sVaribleNameNext + sSymbolQuoted + sLine.Replace("\"", sDoubleQualReplacement) + (i < parts.Length - 1 ? @"\r\n" : "") + sSymbolQuoted + sSymbolEnd;
                        }
                        else
                        {
                            sResult += sVaribleNameFirst + " " + sSymbolEqualNext + " " + sVaribleNameNext + sSymbolQuoted + sLine.Replace("\"", sDoubleQualReplacement) + sSymbolQuoted + (i < parts.Length - 1 ? " " + sNewLineCharacter : "") + sSymbolEnd;
                        }
                    }

                    if (i < parts.Length - 1) //不是選取區的最後一列，後面加上換行符號
                    {
                        sResult += "\r\n";
                    }
                }

                if (sSymbolEnd.Length > 0)
                {
                    try
                    {
                        sResult = sResult.Substring(0, sResult.Length - (sConcateOperator.Length + 1)) + sSymbolEnd;
                    }
                    catch (Exception)
                    {
                        //
                    }
                }
            }

            if (!string.IsNullOrEmpty(sResult) && bCopyToClipboard)
            {
                Clipboard.SetText(sResult);
            }

            return sResult;
        }

        public static string SQLFormatter(string sSQL, string sIndentString, int iSpacesPerTab, int iMaxLineWidth, int iNewStatementLineBreaks, int iNewClauseLineBreaks,
                                          bool bExpandCommaLists, bool bTrailingCommas, bool bSpaceAfterExpandedComma, bool bExpandBooleanExpressions,
                                          bool bExpandCaseStatements, bool bExpandBetweenConditions, bool bExpandInLists, bool bBreakJoinOnSections,
                                          int iUppercaseKeywords = 0, bool bHTMLColoring = false, bool bKeywordStandardization = false)
        {
            _tokenizer = new PoorMansTSqlFormatterLib.Tokenizers.TSqlStandardTokenizer();

            try
            {
                _parser = new PoorMansTSqlFormatterLib.Parsers.TSqlStandardParser();
            }
            catch (Exception)
            {
                //此處會出錯，通常是 LinqBridge.dll 不見了！
            }

            ISqlTreeFormatter innerFormatter = new PoorMansTSqlFormatterLib.Formatters.TSqlStandardFormatter(new PoorMansTSqlFormatterLib.Formatters.TSqlStandardFormatterOptions
            {
                IndentString = sIndentString, //"\\t", //txt_Indent.Text, 沒作用？
                SpacesPerTab = iSpacesPerTab, //4, //int.Parse(txt_IndentWidth.Text), 沒作用？
                MaxLineWidth = iMaxLineWidth, //int.Parse(txt_MaxWidth.Text),
                NewStatementLineBreaks = iNewStatementLineBreaks, //2, //int.Parse(txt_StatementBreaks.Text), 沒作用？
                NewClauseLineBreaks = iNewClauseLineBreaks, //1, //int.Parse(txt_ClauseBreaks.Text), 沒作用？
                ExpandCommaLists = bExpandCommaLists, //chk_ExpandCommaLists.Checked,
                TrailingCommas = bTrailingCommas, //chk_TrailingCommas.Checked,
                SpaceAfterExpandedComma = bSpaceAfterExpandedComma, //true, //chk_SpaceAfterComma.Checked, 沒作用？
                ExpandBooleanExpressions = bExpandBooleanExpressions, //chk_ExpandBooleanExpressions.Checked,
                ExpandCaseStatements = bExpandCaseStatements, //chk_ExpandCaseStatements.Checked,
                ExpandBetweenConditions = bExpandBetweenConditions, //chk_ExpandBetweenConditions.Checked,
                ExpandInLists = bExpandInLists, //chk_ExpandInLists.Checked,
                BreakJoinOnSections = bBreakJoinOnSections, //chk_BreakJoinOnSections.Checked,
                UppercaseKeywords = iUppercaseKeywords, //chk_UppercaseKeywords.Checked, 沒作用！
                HTMLColoring = false, //chk_Coloring.Checked, 沒作用！
                KeywordStandardization = false //chk_EnableKeywordStandardization.Checked, 沒作用！
            });

            //innerFormatter.ErrorOutputPrefix = _generalResourceManager.GetString("ParseErrorWarningPrefix") + Environment.NewLine;
            _formatter = new PoorMansTSqlFormatterLib.Formatters.HtmlPageWrapper(innerFormatter);

            var tokenizedSql = _tokenizer.TokenizeSQL(sSQL, sSQL.Length);
            var parsedSql = _parser.ParseSQL(tokenizedSql);
            var sResult = _formatter.FormatSQLTree(parsedSql);

            //最後面可能會多回傳一個換行符號
            if (sResult.Length > 2 && sResult.Substring(sResult.Length - 2, 2) == "\r\n")
            {
                sResult = sResult.Substring(0, sResult.Length - 2);
            }

            return sResult.Replace("\t", "    ");
        }
    }
}