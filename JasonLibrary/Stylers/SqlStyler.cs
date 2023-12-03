using System.Drawing;
using JasonLibrary.Class;
using ScintillaNET;

namespace JasonLibrary.Stylers
{
    public class SqlStyler : ScintillaStyler
    {
        private const int NUMBER_MARGIN = 1;
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        public static string sColorEditorBackground = ""; //SqlStyler.sColorEditorBackground
        public static string sColorTextIdentifier = "";
        public static string sColorComments = "";
        public static string sColorNumber = "";
        public static string sColorString = "";
        public static string sColorCharacter = "";
        public static string sColorOperatorSymbol = "";
        public static string sColorUserDefinedTablesViews = "";
        public static string sColorUserDefinedFunctionsTirggers = "";
        public static string sColorOperatorKeywords = "";
        public static string sColorBuiltInFunctions = "";
        public static string sColorBuiltInKeywords = "";
        public static string sColorUserDefinedKeywords = "";
        public static bool bKeywordFontBold = false;

        public static string sKeywordsUserDefinedTables = "";
        public static string sKeywordsUserDefinedViews = "";
        public static string sKeywordsUserDefinedFunctions = "";
        public static string sKeywordsUserDefinedTriggers = "";
        public static string sKeywordsOperatorKeywords = "";
        public static string sKeywordsBuiltInFunctions = "";
        public static string sKeywordsBuiltInKeywords = "";
        public static string sKeywordsUserDefinedKeywords = "";

        public SqlStyler() : base(Lexer.Sql, lineNumbers: true, codeFolding: true, braceMatching: true, autoIndent: true)
        { }

        public override void ApplyStyle(Scintilla scintilla)
        {
            // Set the Styles
            scintilla.Styles[Style.LineNumber].ForeColor = Color.FromArgb(255, 128, 128, 128); //Dark Gray
            scintilla.Styles[Style.LineNumber].BackColor = Color.FromArgb(255, 228, 228, 228); //Light Gray
            //scintilla.Styles[Style.Default].BackColor = Color.Black;
            //scintilla.Styles[Style.IndentGuide].ForeColor = Color.DarkGreen;
            //scintilla.Styles[Style.IndentGuide].BackColor = Color.Red;

            var nums = scintilla.Margins[NUMBER_MARGIN];
            nums.Width = 2;
            //nums.Mask = 11;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            //var marker = scintilla.Markers[BOOKMARK_MARKER];
            //marker.Symbol = MarkerSymbol.Bookmark;
            //marker.SetBackColor(Color.DeepSkyBlue);
            //marker.SetForeColor(Color.Black);

            var marker = scintilla.Markers[BOOKMARK_MARGIN];
            marker.Symbol = MarkerSymbol.Bookmark;
            marker.SetBackColor(Color.DeepSkyBlue);
            marker.SetForeColor(Color.Black);

            // Set colors for all folding markers
            //for (int i = 0; i <= 31; i++)
            //{
            //    scintilla.Markers[i].SetForeColor(Color.Blue); // styles for [+] and [-]
            //    scintilla.Markers[i].SetBackColor(Color.Maroon); // styles for [+] and [-]
            //}

            if (MyLibrary.bDarkMode)
            {
                foreach (var style in scintilla.Styles)
                    style.BackColor = ColorTranslator.FromHtml("#2D2D30"); //Color.FromArgb(30, 30, 30);
            }

            var margin = scintilla.Margins[BOOKMARK_MARGIN];
            margin.Width = 12;
            //margin.Sensitive = true;
            //margin.Type = MarginType.Symbol;
            //margin.Mask = (1 << BOOKMARK_MARKER);

            scintilla.Styles[Style.Default].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);
            scintilla.Styles[Style.Sql.Default].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            scintilla.Styles[Style.Sql.Identifier].ForeColor = ColorTranslator.FromHtml(sColorTextIdentifier);
            scintilla.Styles[Style.Sql.Identifier].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            scintilla.Styles[Style.Sql.QOperator].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);
            scintilla.Styles[Style.Sql.QuotedIdentifier].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);
            scintilla.Styles[Style.Sql.SqlPlus].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);
            scintilla.Styles[Style.Sql.SqlPlusPrompt].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);
            scintilla.Styles[Style.Sql.SqlPlusComment].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            scintilla.Styles[Style.Sql.Comment].ForeColor = ColorTranslator.FromHtml(sColorComments); //Color.Green;
            scintilla.Styles[Style.Sql.Comment].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);
            scintilla.Styles[Style.Sql.CommentLine].ForeColor = ColorTranslator.FromHtml(sColorComments); //Color.Green;
            scintilla.Styles[Style.Sql.CommentLine].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);
            scintilla.Styles[Style.Sql.CommentDoc].ForeColor = ColorTranslator.FromHtml(sColorComments); //Color.Green;
            scintilla.Styles[Style.Sql.CommentDoc].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);
            scintilla.Styles[Style.Sql.CommentLineDoc].ForeColor = ColorTranslator.FromHtml(sColorComments); //Color.Green;
            scintilla.Styles[Style.Sql.CommentLineDoc].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            scintilla.Styles[Style.Sql.Number].ForeColor = ColorTranslator.FromHtml(sColorNumber); //Color.Maroon;
            scintilla.Styles[Style.Sql.Number].BackColor = ColorTranslator.FromHtml(sColorEditorBackground); //Color.Maroon;

            //雙引號中間的字串
            scintilla.Styles[Style.Sql.String].ForeColor = ColorTranslator.FromHtml(sColorString); //Color.Red;
            scintilla.Styles[Style.Sql.String].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            //單引號中間的字串
            scintilla.Styles[Style.Sql.Character].ForeColor = ColorTranslator.FromHtml(sColorCharacter); //Color.LightBlue;
            scintilla.Styles[Style.Sql.Character].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            //Operator Symbol
            scintilla.Styles[Style.Sql.Operator].ForeColor = ColorTranslator.FromHtml(sColorOperatorSymbol); //Color.LightGreen;
            scintilla.Styles[Style.Sql.Operator].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            scintilla.Styles[Style.Sql.Word].ForeColor = ColorTranslator.FromHtml(sColorUserDefinedTablesViews); //Color.Blue;
            scintilla.Styles[Style.Sql.Word].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            scintilla.Styles[Style.Sql.Word2].ForeColor = ColorTranslator.FromHtml(sColorUserDefinedFunctionsTirggers);
            scintilla.Styles[Style.Sql.Word2].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            //Operator Keywords
            scintilla.Styles[Style.Sql.User1].ForeColor = ColorTranslator.FromHtml(sColorOperatorKeywords); //Color.LightGreen;
            scintilla.Styles[Style.Sql.User1].Bold = bKeywordFontBold;
            scintilla.Styles[Style.Sql.User1].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            scintilla.Styles[Style.Sql.User2].ForeColor = ColorTranslator.FromHtml(sColorBuiltInFunctions); //Color.FromArgb(255, 00, 128, 192); //Medium Blue-Green
            scintilla.Styles[Style.Sql.User2].Bold = bKeywordFontBold;
            scintilla.Styles[Style.Sql.User2].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            scintilla.Styles[Style.Sql.User3].ForeColor = ColorTranslator.FromHtml(sColorBuiltInKeywords); //Color.FromArgb(255, 128, 128, 0); //橄欖色
            scintilla.Styles[Style.Sql.User3].Bold = bKeywordFontBold;
            scintilla.Styles[Style.Sql.User3].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            //仿 Toad 的 table name color
            scintilla.Styles[Style.Sql.User4].ForeColor = ColorTranslator.FromHtml(sColorUserDefinedKeywords); //Color.FromArgb(255, 128, 128, 0); //橄欖色
            scintilla.Styles[Style.Sql.User4].Bold = bKeywordFontBold;
            scintilla.Styles[Style.Sql.User4].BackColor = ColorTranslator.FromHtml(sColorEditorBackground);

            if (MyLibrary.bDarkMode)
            {
                scintilla.SetFoldMarginHighlightColor(true, ColorTranslator.FromHtml("#2D2D30")); //Color.FromArgb(30, 30, 30));
                scintilla.SetFoldMarginColor(true, ColorTranslator.FromHtml("#2D2D30"));
                //scintilla.SetAdditionalSelBack(Color.FromArgb(30, 30, 30));

                scintilla.Styles[Style.Default].ForeColor = ColorTranslator.FromHtml(sColorTextIdentifier);
                scintilla.Styles[Style.Sql.Default].ForeColor = ColorTranslator.FromHtml(sColorTextIdentifier);

                scintilla.Styles[Style.Sql.SqlPlus].ForeColor = ColorTranslator.FromHtml(sColorTextIdentifier);
                scintilla.Styles[Style.Sql.SqlPlusPrompt].ForeColor = ColorTranslator.FromHtml(sColorTextIdentifier);
                scintilla.Styles[Style.Sql.SqlPlusComment].ForeColor = ColorTranslator.FromHtml(sColorTextIdentifier);
            }
        }

        public override void RemoveStyle(Scintilla scintilla)
        {

        }

        public override void SetKeywords(Scintilla scintilla)
        {
            // Set keyword lists
            // Word = 0, User-defined Tables & Views
            scintilla.SetKeywords(0, MyLibrary.sKeywordsUserDefinedTables.ToLower() + MyLibrary.sKeywordsUserDefinedViews.ToLower());

            // Word2 = 1, User-defined Functions & Triggers
            scintilla.SetKeywords(1, MyLibrary.sKeywordsUserDefinedFunctions.ToLower() + MyLibrary.sKeywordsUserDefinedTriggers.ToLower());

            // User1 = 4, Operator (Keywords)
            //scintilla.SetKeywords(4, "all and any between cross exists in inner is join left like not null or outer pivot right some unpivot ( ) * yyy");
            scintilla.SetKeywords(4, MyLibrary.sKeywordsOperatorKeywords.ToLower());

            // User2 = 5, Built-in Functions 內建 Function, 依 Oracle / PostgreSQL 而有不同
            //.SetKeywords(5, "sys objects sysobjects "); //可以挪為它用
            scintilla.SetKeywords(5, MyLibrary.sKeywordsBuiltInFunctions.ToLower());

            // User3 = 6, Built-in Keywords 內建 Function, 依 Oracle / PostgreSQL 而有不同 (Color 定義在 SqlStyler.cs 的 ApplyStyle() 裡面)
            //scintilla.SetKeywords(6, JasonLibrary.MyLibrary.sKeyWord6);
            scintilla.SetKeywords(6, MyLibrary.sKeywordsBuiltInKeywords.ToLower());

            // User4 = 7, User-defined Keywords 使用者自定的關鍵字 (Color 定義在 SqlStyler.cs 的 ApplyStyle() 裡面)，此為最後一組使用者自定義的顏色
            scintilla.SetKeywords(7, MyLibrary.sKeywordsUserDefinedKeywords.ToLower());
        }
    }
}