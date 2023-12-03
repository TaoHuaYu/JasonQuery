using System;
using System.Drawing;
using JasonLibrary;
using System.Windows.Forms;
using JasonLibrary.Class;
using JasonLibrary.Stylers;

namespace JasonQuery
{
    public sealed partial class frmSQLStatementViewer : Form
    {
        private ContextMenuStrip _cMenu = new ContextMenuStrip();

        private string _sCellText;

        public string sCellText
        {
            set => _sCellText = value;
        }

        public frmSQLStatementViewer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            MyGlobal.ApplyLanguageInfo(this, false); //frmCellViewer_Load

            if (MyLibrary.bDarkMode)
            {
                C1.Win.C1Themes.C1ThemeController.ApplicationTheme = "VS2013Dark";
            }

            ApplySqlStyler();

            editorCellViewer.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
            editorCellViewer.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editorCellViewer.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));

            editorCellViewer.Text = _sCellText;
            editorCellViewer.ReadOnly = true;

            var sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "menueditor", "SelectAll", "Text");
            _cMenu.Items.Add(sLangText);
            ((ToolStripMenuItem) _cMenu.Items[0]).ShortcutKeys = (Keys.Control | Keys.A);

            _cMenu.Items[0].Click += delegate
            {
                editorCellViewer.SelectionStart = 0;
                editorCellViewer.SelectionEnd = editorCellViewer.Text.Length;
            };

            _cMenu.Items.Add("-");

            sLangText = MyGlobal.GetLanguageString("Copy", "form", Name, "menueditor", "Copy", "Text");
            _cMenu.Items.Add(sLangText);
            ((ToolStripMenuItem) _cMenu.Items[2]).ShortcutKeys = (Keys.Control | Keys.C);

            _cMenu.Items[2].Click += delegate
            {
                editorCellViewer.Copy();
            };

            if (MyLibrary.bWordWrap)
            {
                btnWordWrap.PerformClick();
            }
            else
            {
                btnWordWrap2.PerformClick();
            }

            switch (MyLibrary.sWordWrapIndentMode)
            {
                case "Fixed":
                    editorCellViewer.WrapIndentMode = ScintillaNET.WrapIndentMode.Fixed;
                    break;
                case "Indent":
                    editorCellViewer.WrapIndentMode = ScintillaNET.WrapIndentMode.Indent;
                    break;
                default:
                    editorCellViewer.WrapIndentMode = ScintillaNET.WrapIndentMode.Same;
                    break;
            }

            if (MyLibrary.bShowAllCharacters)
            {
                editorCellViewer.ViewEol = true;
                editorCellViewer.ViewWhitespace = ScintillaNET.WhitespaceMode.VisibleAlways;
                btnShowAllCharacters.Visible = false;
                btnShowAllCharacters2.Visible = true;
            }
            else
            {
                editorCellViewer.ViewEol = false;
                editorCellViewer.ViewWhitespace = ScintillaNET.WhitespaceMode.Invisible;
                btnShowAllCharacters.Visible = true;
                btnShowAllCharacters2.Visible = false;
            }
        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            MyGlobal.UpdateSetting("GlobalConfig", "SQLStatementViewerFormWidth", Size.Width.ToString());
            MyGlobal.UpdateSetting("GlobalConfig", "SQLStatementViewerFormHeight", Size.Height.ToString());
        }

        private void btnWordWrap_Click(object sender, EventArgs e)
        {
            if (editorCellViewer.WrapMode == ScintillaNET.WrapMode.Word)
            {
                btnWordWrap.Visible = true;
                btnWordWrap2.Visible = false;
                editorCellViewer.WrapMode = ScintillaNET.WrapMode.None;
            }
            else
            {
                btnWordWrap.Visible = false;
                btnWordWrap2.Visible = true;
                editorCellViewer.WrapMode = ScintillaNET.WrapMode.Word;
                editorCellViewer.WrapVisualFlags = (MyLibrary.bWordWrapVisualFlags_Start ? ScintillaNET.WrapVisualFlags.Start : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_End ? ScintillaNET.WrapVisualFlags.End : ScintillaNET.WrapVisualFlags.None) | (MyLibrary.bWordWrapVisualFlags_Margin ? ScintillaNET.WrapVisualFlags.Margin : ScintillaNET.WrapVisualFlags.None);
            }

            //加上以下這個指令，取消 Word Wrap 後，Focus 才不會跑到最底部！
            editorCellViewer.ScrollCaret();
        }

        private void btnShowAllCharacters_Click(object sender, EventArgs e)
        {
            if (editorCellViewer.ViewEol)
            {
                btnShowAllCharacters.Visible = true;
                btnShowAllCharacters2.Visible = false;
                editorCellViewer.ViewEol = false;
                editorCellViewer.ViewWhitespace = ScintillaNET.WhitespaceMode.Invisible;
            }
            else
            {
                btnShowAllCharacters.Visible = false;
                btnShowAllCharacters2.Visible = true;
                editorCellViewer.ViewEol = true;
                editorCellViewer.ViewWhitespace = ScintillaNET.WhitespaceMode.VisibleAlways;
            }
        }

        private void editorCellViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            //Select All
            _cMenu.Items[0].Enabled = !string.IsNullOrEmpty(editorCellViewer.Text);

            //Copy
            _cMenu.Items[2].Enabled = !string.IsNullOrEmpty(editorCellViewer.SelectedText); //判斷是否有選取文字，決定功能表項目可不可用

            editorCellViewer.ContextMenuStrip = _cMenu;

            if (MyLibrary.bDarkMode)
            {
                _cMenu.BackColor = ColorTranslator.FromHtml("#2D2D30");
                _cMenu.ForeColor = Color.White;
                _cMenu.RenderMode = ToolStripRenderMode.System;
                //_cMenu.ShowImageMargin = false;
            }

            _cMenu.Show(editorCellViewer, new Point(e.X, e.Y));
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            editorCellViewer.Copy();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            editorCellViewer.SelectionStart = 0;
            editorCellViewer.SelectionEnd = editorCellViewer.Text.Length;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var bHandled = false;

            switch (keyData)
            {
                case Keys.Escape:
                    Close();
                    bHandled = true;
                    break;
            }

            return bHandled;
        }

        private void ApplySqlStyler()
        {
            editorCellViewer.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
            editorCellViewer.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editorCellViewer.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));

            SqlStyler.sColorEditorBackground = MyLibrary.sColorEditorBackground;
            SqlStyler.sColorTextIdentifier = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorComments = MyLibrary.sColorComments;
            SqlStyler.sColorNumber = MyLibrary.sColorNumber;
            SqlStyler.sColorString = MyLibrary.sColorString;
            SqlStyler.sColorCharacter = MyLibrary.sColorCharacter;
            SqlStyler.sColorOperatorSymbol = MyLibrary.sColorOperatorSymbol;
            SqlStyler.sColorUserDefinedTablesViews = MyLibrary.sColorUserDefinedTablesViews;
            SqlStyler.sColorUserDefinedFunctionsTirggers = MyLibrary.sColorUserDefinedFunctionsTirggers;
            SqlStyler.sColorOperatorKeywords = MyLibrary.sColorOperatorKeywords;
            SqlStyler.sColorBuiltInFunctions = MyLibrary.sColorBuiltInFunctions;
            SqlStyler.sColorBuiltInKeywords = MyLibrary.sColorBuiltInKeywords;
            SqlStyler.sColorUserDefinedKeywords = MyLibrary.sColorUserDefinedKeywords;
            SqlStyler.bKeywordFontBold = MyLibrary.bKeywordFontBold;

            SqlStyler.sKeywordsUserDefinedTables = MyLibrary.sKeywordsUserDefinedTables;
            SqlStyler.sKeywordsUserDefinedViews = MyLibrary.sKeywordsUserDefinedViews;
            SqlStyler.sKeywordsUserDefinedFunctions = MyLibrary.sKeywordsUserDefinedFunctions;
            SqlStyler.sKeywordsUserDefinedTriggers = MyLibrary.sKeywordsUserDefinedTriggers;
            SqlStyler.sKeywordsOperatorKeywords = MyLibrary.sKeywordsOperatorKeywords;
            SqlStyler.sKeywordsBuiltInFunctions = MyLibrary.sKeywordsBuiltInFunctions;
            SqlStyler.sKeywordsBuiltInKeywords = MyLibrary.sKeywordsBuiltInKeywords;
            SqlStyler.sKeywordsUserDefinedKeywords = MyLibrary.sKeywordsUserDefinedKeywords;

            editorCellViewer.Styler = new SqlStyler(); //sqlstyler()：變更關鍵字、顏色
        }
    }
}