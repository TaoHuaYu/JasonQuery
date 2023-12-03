using System;
using System.Drawing;
using JasonLibrary;
using System.Windows.Forms;
using JasonLibrary.Class;
using JasonLibrary.Stylers;

namespace JasonQuery
{
    public sealed partial class frmCellViewer : Form
    {
        private ContextMenuStrip _cMenu = new ContextMenuStrip();

        private string _sColumnName, _sColumnType, _sCellText;

        public string sColumnName
        {
            set => _sColumnName = value;
        }

        public string sColumnType
        {
            set => _sColumnType = value;
        }

        public string sCellText
        {
            set => _sCellText = value;
        }

        public frmCellViewer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            var sLangText = "";

            MyGlobal.ApplyLanguageInfo(this, false);

            if (MyLibrary.bDarkMode)
            {
                C1.Win.C1Themes.C1ThemeController.ApplicationTheme = "VS2013Dark";

                ApplySqlStyler();

                lblColumnName2.ForeColor = Color.Yellow;
                lblColumnType2.ForeColor = Color.Yellow;
            }
            else
            {
                lblColumnName2.ForeColor = Color.Blue;
                lblColumnType2.ForeColor = Color.Blue;
            }

            lblColumnName2.Text = _sColumnName;

            if (string.IsNullOrEmpty(_sColumnType) == false)
            {
                lblColumnType2.Text = _sColumnType;
            }
            else
            {
                lblColumnType.Visible = false;
                lblColumnType2.Visible = false;
            }

            if (string.IsNullOrEmpty(_sColumnName) && string.IsNullOrEmpty(_sColumnType))
            {
                lblColumnName.Visible = false;
                lblColumnName2.Visible = false;
                lblColumnType.Visible = false;
                lblColumnType2.Visible = false;
                toolStripSeparator1.Visible = false;
            }

            editorCellViewer.SetSelectionBackColor(true, ColorTranslator.FromHtml(MyLibrary.sColorSelectedTextBackground)); //變更選取區塊的底色
            editorCellViewer.CaretLineBackColor = ColorTranslator.FromHtml(MyLibrary.sColorCurrentLineBackground);
            editorCellViewer.SetWhitespaceForeColor(true, ColorTranslator.FromHtml(MyLibrary.sColorWhiteSpace));

            editorCellViewer.Text = _sCellText;
            editorCellViewer.ReadOnly = true;

            sLangText = MyGlobal.GetLanguageString("Select All", "form", Name, "menueditor", "SelectAll", "Text");
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
            MyGlobal.UpdateSetting("GlobalConfig", "CellViewerFormWidth", Size.Width.ToString());
            MyGlobal.UpdateSetting("GlobalConfig", "CellViewerFormHeight", Size.Height.ToString());
        }

        private void ApplySqlStyler()
        {
            SqlStyler.sColorEditorBackground = MyLibrary.sColorEditorBackground;
            SqlStyler.sColorTextIdentifier = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorComments = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorNumber = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorString = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorCharacter = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorOperatorSymbol = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorUserDefinedTablesViews = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorUserDefinedFunctionsTirggers = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorOperatorKeywords = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorBuiltInFunctions = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorBuiltInKeywords = MyLibrary.sColorTextIdentifier;
            SqlStyler.sColorUserDefinedKeywords = MyLibrary.sColorTextIdentifier;
            SqlStyler.bKeywordFontBold = false;

            SqlStyler.sKeywordsUserDefinedTables = MyLibrary.sColorTextIdentifier;
            SqlStyler.sKeywordsUserDefinedViews = MyLibrary.sColorTextIdentifier;
            SqlStyler.sKeywordsUserDefinedFunctions = MyLibrary.sColorTextIdentifier;
            SqlStyler.sKeywordsUserDefinedTriggers = MyLibrary.sColorTextIdentifier;
            SqlStyler.sKeywordsOperatorKeywords = MyLibrary.sColorTextIdentifier;
            SqlStyler.sKeywordsBuiltInFunctions = MyLibrary.sColorTextIdentifier;
            SqlStyler.sKeywordsBuiltInKeywords = MyLibrary.sColorTextIdentifier;
            SqlStyler.sKeywordsUserDefinedKeywords = MyLibrary.sColorTextIdentifier;

            editorCellViewer.Styler = new SqlStyler(); //變更關鍵字、顏色
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
    }
}