using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ScintillaNET;
using System.Runtime.InteropServices;
using JasonLibrary.Class;
using System.Reflection;

namespace JasonQuery.FindReplace
{
    public partial class frmFindReplace : Form
    {
        private bool _autoPosition;
        private CharacterRange _searchRange;
        private Scintilla _scintilla;

        //20230705 Color
        private Color _colorRed;
        private Color _colorGreen;
        private Color _colorBlue;
        private Color _colorNormal;

        //20230628 語系訊息
        private string _sErrorRegularExpression;
        private string _sFindNoMatch;
        private string _sFindFirstMatchInFile;
        private string _sFindFirstMatchInSelection;
        private string _sCountQtyMatchInFile;
        private string _sCountQtyMatchInSelection;
        private string _sCountQtyMarkInFile;
        private string _sCountQtyMarkInSelection;
        private string _sReplaceNoMatch;
        private string _sReplaceAllQtyReplacedInFile;
        private string _sReplaceAllQtyReplacedInSelection;

        public event KeyPressedHandler KeyPressed;
        public delegate void KeyPressedHandler(object sender, KeyEventArgs e);

        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        //<--Begin: 20230628 尋找並關閉 MessageBox
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(IntPtr classname, string title); // extern method: FindWindow

        [DllImport("user32.dll")]
        private static extern void MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, bool rePaint); // extern method: MoveWindow

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rectangle rect); // extern method: GetWindowRect
        //-->End: 20230628

        #region Properties/// <summary>
        /// Gets or sets whether the dialog should automatically move away from the current
        /// selection to prevent obscuring it.
        /// </summary>
        /// <returns>true to automatically move away from the current selection; otherwise, false.</returns>
        public bool AutoPosition
        {
            get
            {
                return _autoPosition;
            }
            set
            {
                _autoPosition = value;
            }
        }

        public Scintilla Scintilla
        {
            get
            {
                return _scintilla;
            }
            set
            {
                _scintilla = value;
            }
        }

        public FindReplace FindReplace { get; set; }
        #endregion Properties

        public frmFindReplace()
        {
            InitializeComponent();
            _autoPosition = true;
        }

        private void frmFindReplace_Load(object sender, EventArgs e)
        {
            ApplyLocalizationSetting(); //frmFindReplace_Load

            _colorGreen = MyLibrary.bDarkMode ? Color.FromArgb(0, 255, 0) : Color.Green;
            _colorRed = MyLibrary.bDarkMode ? Color.FromArgb(162, 12, 12) : Color.Red;
            _colorBlue = MyLibrary.bDarkMode ? Color.Cyan : Color.Blue;
            _colorNormal = MyLibrary.bDarkMode ? Color.White : Color.Blue;
        }

        private void SearchMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoStandard.Checked)
            {
                pnlStandardOptions.BringToFront();
            }
            else
            {
                pnlRegexpOptions.BringToFront();
            }
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;
            lblStatus.ForeColor = _colorNormal;

            if (cboFind.Text == string.Empty)
            {
                return;
            }

            UpdateFindList(); //btnFindNext_Click

            CharacterRange foundRange;

            try
            {
                foundRange = FindNext(false);
            }
            catch (ArgumentException ex)
            {
                lblStatus.Text = _sErrorRegularExpression + ex.Message;
                lblStatus.ForeColor = _colorRed;
                return;
            }

            if (foundRange.cpMin == foundRange.cpMax)
            {
                lblStatus.Text = _sFindNoMatch.Replace("{text}", cboFind.Text);
                lblStatus.ForeColor = _colorRed;
            }
            else
            {
                if (foundRange.cpMin < Scintilla.AnchorPosition)
                {
                    if (chkInSelection.Checked)
                    {
                        lblStatus.Text = _sFindFirstMatchInSelection; //Search match wrapped to the beginning of the selection
                    }
                    else
                    {
                        lblStatus.Text = _sFindFirstMatchInFile; //Search match wrapped to the beginning of the document
                    }
                }

                lblStatus.ForeColor = _colorGreen;
                Scintilla.SetSel(foundRange.cpMin, foundRange.cpMax);
                MoveFormAwayFromSelection();
            }
        }

        private void btnFindPrevious_Click(object sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;
            lblStatus.ForeColor = _colorNormal;

            if (cboFind.Text == string.Empty)
            {
                return;
            }

            UpdateFindList(); //btnFindPrevious_Click
            CharacterRange foundRange;

            try
            {
                foundRange = FindNext(true);
            }
            catch (ArgumentException ex)
            {
                lblStatus.Text = _sErrorRegularExpression + ex.Message;
                lblStatus.ForeColor = _colorRed;
                return;
            }

            if (foundRange.cpMin == foundRange.cpMax)
            {
                lblStatus.Text = _sFindNoMatch.Replace("{text}", cboFind.Text);
                lblStatus.ForeColor = _colorRed;
            }
            else
            {
                if (foundRange.cpMin > Scintilla.CurrentPosition)
                {
                    if (chkInSelection.Checked)
                    {
                        lblStatus.Text = _sFindFirstMatchInSelection; //Search match wrapped to the end of the selection
                    }
                    else
                    {
                        lblStatus.Text = _sFindFirstMatchInFile; //Search match wrapped to the end of the document
                    }
                }

                lblStatus.ForeColor = _colorGreen;
                Scintilla.SetSel(foundRange.cpMin, foundRange.cpMax);
                MoveFormAwayFromSelection();
            }
        }

        public CharacterRange FindNext(bool searchUp)
        {
            CharacterRange foundRange;

            if (rdoRegularExpression.Checked)
            {
                var rr = new Regex(cboFind.Text, GetRegexOptions());

                if (chkInSelection.Checked)
                {
                    if (_searchRange.cpMin == _searchRange.cpMax)
                    {
                        _searchRange = new CharacterRange(_scintilla.Selections[0].Start, _scintilla.Selections[0].End);
                    }

                    if (searchUp)
                    {
                        foundRange = FindReplace.FindPrevious(rr, chkWrapAround.Checked, _searchRange);
                    }
                    else
                    {
                        foundRange = FindReplace.FindNext(rr, chkWrapAround.Checked, _searchRange);
                    }
                }
                else
                {
                    _searchRange = new CharacterRange();

                    if (searchUp)
                    {
                        foundRange = FindReplace.FindPrevious(rr, chkWrapAround.Checked);
                    }
                    else
                    {
                        foundRange = FindReplace.FindNext(rr, chkWrapAround.Checked);
                    }
                }
            }
            else
            {
                if (chkInSelection.Checked)
                {
                    if (_searchRange.cpMin == _searchRange.cpMax)
                    {
                        _searchRange = new CharacterRange(_scintilla.Selections[0].Start, _scintilla.Selections[0].End);
                    }

                    if (searchUp)
                    {
                        var textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;
                        foundRange = FindReplace.FindPrevious(textToFind, chkWrapAround.Checked, GetSearchFlags(), _searchRange);
                    }
                    else
                    {
                        var textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;
                        foundRange = FindReplace.FindNext(textToFind, chkWrapAround.Checked, GetSearchFlags(), _searchRange);
                    }
                }
                else
                {
                    _searchRange = new CharacterRange();

                    if (searchUp)
                    {
                        var textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;
                        foundRange = FindReplace.FindPrevious(textToFind, chkWrapAround.Checked, GetSearchFlags());
                    }
                    else
                    {
                        var textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;
                        foundRange = FindReplace.FindNext(textToFind, chkWrapAround.Checked, GetSearchFlags());
                    }
                }
            }

            return foundRange;
        }

        public virtual void MoveFormAwayFromSelection()
        {
            if (!Visible)
            {
                return;
            }

            if (!AutoPosition)
            {
                return;
            }

            int pos = Scintilla.CurrentPosition;
            int x = Scintilla.PointXFromPosition(pos);
            int y = Scintilla.PointYFromPosition(pos);

            var cursorPoint = Scintilla.PointToScreen(new Point(x, y));
            var r = new Rectangle(Location, Size);

            if (r.Contains(cursorPoint))
            {
                Point newLocation;

                if (cursorPoint.Y < (Screen.PrimaryScreen.Bounds.Height / 2))
                {
                    //TODO - replace lineheight with ScintillaNET command, when added
                    int SCI_TEXTHEIGHT = 2279;
                    int lineHeight = Scintilla.DirectMessage(SCI_TEXTHEIGHT, IntPtr.Zero, IntPtr.Zero).ToInt32();
                    // int lineHeight = Scintilla.Lines[Scintilla.LineFromPosition(pos)].Height;

                    // Top half of the screen
                    newLocation = Scintilla.PointToClient(new Point(Location.X, cursorPoint.Y + lineHeight * 2));
                }
                else
                {
                    //TODO - replace lineheight with ScintillaNET command, when added
                    int SCI_TEXTHEIGHT = 2279;
                    int lineHeight = Scintilla.DirectMessage(SCI_TEXTHEIGHT, IntPtr.Zero, IntPtr.Zero).ToInt32();
                    // int lineHeight = Scintilla.Lines[Scintilla.LineFromPosition(pos)].Height;

                    // Bottom half of the screen
                    newLocation = Scintilla.PointToClient(new Point(Location.X, cursorPoint.Y - Height - (lineHeight * 2)));
                }

                newLocation = Scintilla.PointToScreen(newLocation);
                Location = newLocation;
            }
        }

        private CharacterRange FindNext(bool searchUp, ref Regex rr)
        {
            CharacterRange foundRange;

            if (rdoRegularExpression.Checked)
            {
                if (rr == null)
                {
                    rr = new Regex(cboFind.Text, GetRegexOptions());
                }

                if (chkInSelection.Checked)
                {
                    if (_searchRange.cpMin == _searchRange.cpMax)
                    {
                        _searchRange = new CharacterRange(_scintilla.Selections[0].Start, _scintilla.Selections[0].End);
                    }

                    if (searchUp)
                    {
                        foundRange = FindReplace.FindPrevious(rr, chkWrapAround.Checked, _searchRange);
                    }
                    else
                    {
                        foundRange = FindReplace.FindNext(rr, chkWrapAround.Checked, _searchRange);
                    }
                }
                else
                {
                    _searchRange = new CharacterRange();

                    if (searchUp)
                    {
                        foundRange = FindReplace.FindPrevious(rr, chkWrapAround.Checked);
                    }
                    else
                    {
                        foundRange = FindReplace.FindNext(rr, chkWrapAround.Checked);
                    }
                }
            }
            else
            {
                if (chkInSelection.Checked)
                {
                    if (_searchRange.cpMin == _searchRange.cpMax)
                    {
                        _searchRange = new CharacterRange(_scintilla.Selections[0].Start, _scintilla.Selections[0].End);
                    }

                    if (searchUp)
                    {
                        var textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;
                        foundRange = FindReplace.FindPrevious(textToFind, chkWrapAround.Checked, GetSearchFlags(), _searchRange);
                    }
                    else
                    {
                        string textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;
                        foundRange = FindReplace.FindNext(textToFind, chkWrapAround.Checked, GetSearchFlags(), _searchRange);
                    }
                }
                else
                {
                    _searchRange = new CharacterRange();

                    if (searchUp)
                    {
                        string textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;
                        foundRange = FindReplace.FindPrevious(textToFind, chkWrapAround.Checked, GetSearchFlags());
                    }
                    else
                    {
                        string textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;
                        foundRange = FindReplace.FindNext(textToFind, chkWrapAround.Checked, GetSearchFlags());
                    }
                }
            }

            return foundRange;
        }

        public RegexOptions GetRegexOptions()
        {
            RegexOptions ro = RegexOptions.None;

            if (chkCompiled.Checked)
            {
                ro |= RegexOptions.Compiled;
            }

            if (chkCultureInvariant.Checked)
            {
                ro |= RegexOptions.Compiled;
            }

            if (chkEcmaScript.Checked)
            {
                ro |= RegexOptions.ECMAScript;
            }

            if (chkExplicitCapture.Checked)
            {
                ro |= RegexOptions.ExplicitCapture;
            }

            if (chkIgnoreCase.Checked)
            {
                ro |= RegexOptions.IgnoreCase;
            }

            if (chkIgnorePatternWhitespace.Checked)
            {
                ro |= RegexOptions.IgnorePatternWhitespace;
            }

            if (chkMultiline.Checked)
            {
                ro |= RegexOptions.Multiline;
            }

            if (chkRightToLeft.Checked)
            {
                ro |= RegexOptions.RightToLeft;
            }

            if (chkSingleLine.Checked)
            {
                ro |= RegexOptions.Singleline;
            }

            return ro;
        }

        public SearchFlags GetSearchFlags()
        {
            var sf = SearchFlags.None;

            if (chkMatchCase.Checked)
            {
                sf |= SearchFlags.MatchCase;
            }

            if (chkWholeWord.Checked)
            {
                sf |= SearchFlags.WholeWord;
            }

            if (chkWordStart.Checked)
            {
                sf |= SearchFlags.WordStart;
            }

            return sf;
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;
            lblStatus.ForeColor = _colorBlue;

            if (cboFind.Text == string.Empty)
            {
                return;
            }

            UpdateFindList(); //btnReplaceAll_Click
            UpdateReplaceList(); //btnReplaceAll_Click
            var foundCount = 0;

            #region RegEx
            if (rdoRegularExpression.Checked)
            {
                Regex rr = null;

                try
                {
                    rr = new Regex(cboFind.Text, GetRegexOptions());
                }
                catch (ArgumentException ex)
                {
                    lblStatus.Text = _sErrorRegularExpression + ex.Message;
                    lblStatus.ForeColor = _colorRed;
                    return;
                }

                if (chkInSelection.Checked)
                {
                    if (_searchRange.cpMin == _searchRange.cpMax)
                    {
                        _searchRange = new CharacterRange(_scintilla.Selections[0].Start, _scintilla.Selections[0].End);
                    }

                    foundCount = FindReplace.ReplaceAll(_searchRange, rr, cboReplace.Text, false, false);
                }
                else
                {
                    _searchRange = new CharacterRange();
                    foundCount = FindReplace.ReplaceAll(rr, cboReplace.Text, false, false);
                }
            }
            #endregion

            #region Non-RegEx
            if (!rdoRegularExpression.Checked)
            {
                if (chkInSelection.Checked)
                {
                    if (_searchRange.cpMin == _searchRange.cpMax)
                    {
                        _searchRange = new CharacterRange(_scintilla.Selections[0].Start, _scintilla.Selections[0].End);
                    }

                    var textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;
                    var textToReplace = rdoExtended.Checked ? FindReplace.Transform(cboReplace.Text) : cboReplace.Text;
                    foundCount = FindReplace.ReplaceAll(_searchRange, textToFind, textToReplace, GetSearchFlags(), false, false);
                }
                else
                {
                    _searchRange = new CharacterRange();
                    var textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;
                    var textToReplace = rdoExtended.Checked ? FindReplace.Transform(cboReplace.Text) : cboReplace.Text;
                    foundCount = FindReplace.ReplaceAll(textToFind, textToReplace, GetSearchFlags(), false, false);
                }
            }
            #endregion

            if (chkInSelection.Checked)
            {
                lblStatus.Text = _sReplaceAllQtyReplacedInSelection.Replace("{qty}", foundCount.ToString());
            }
            else
            {
                lblStatus.Text = _sReplaceAllQtyReplacedInFile.Replace("{qty}", foundCount.ToString());
            }

            lblStatus.ForeColor = foundCount.Equals(0) ? _colorRed : _colorBlue;
        }

        private void frmFindReplace_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void btnClearHighlights_Click(object sender, EventArgs e)
        {
            FindReplace.ClearAllHighlights();
            lblStatus.Text = string.Empty;
        }

        private void chkEcmaScript_CheckedChanged(object sender, EventArgs e)
        {
            if (((C1.Win.C1Input.C1CheckBox)sender).Checked)
            {
                chkExplicitCapture.Checked = false;
                chkExplicitCapture.Enabled = false;
                chkIgnorePatternWhitespace.Checked = false;
                chkIgnorePatternWhitespace.Enabled = false;
                chkRightToLeft.Checked = false;
                chkRightToLeft.Enabled = false;
                chkSingleLine.Checked = false;
            }
            else
            {
                chkExplicitCapture.Enabled = true;
                chkIgnorePatternWhitespace.Enabled = true;
                chkRightToLeft.Enabled = true;
                chkSingleLine.Enabled = true;
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Scintilla.SelectedText))
            {
                chkInSelection.Enabled = true;
            }
            else
            {
                chkInSelection.Enabled = false;
                chkInSelection.Checked = false;
            }

            //var bValue = Scintilla.CanPaste;
            //btnReplaceAll.Enabled = bValue;
            //btnReplaceNext.Enabled = bValue;
            //btnReplacePrevious.Enabled = bValue;

            //if they leave the dialog and come back any "Search Selection" range they might have had is invalidated
            _searchRange = new CharacterRange();

            MoveFormAwayFromSelection();

            lblStatus.Text = string.Empty;
            base.OnActivated(e);

            //20230703 檢查目前具有焦點的控制項是否為按鈕
            //if (ActiveControl is ToolStripButton clickedButton)
            //{
            //    // 在這裡處理按鈕被按下的邏輯
            //    MessageBox.Show($"Button {clickedButton.Name} was clicked!");
            //}
        }

        private void btnReplaceNext_Click(object sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;
            lblStatus.ForeColor = _colorNormal;

            if (cboFind.Text == string.Empty)
            {
                return;
            }

            UpdateFindList(); //btnReplaceNext_Click
            UpdateReplaceList(); //btnReplaceNext_Click

            CharacterRange nextRange;

            try
            {
                nextRange = ReplaceNext(false);
            }
            catch (ArgumentException ex)
            {
                lblStatus.Text = _sErrorRegularExpression + ex.Message;
                lblStatus.ForeColor = _colorRed;
                return;
            }

            if (nextRange.cpMin == nextRange.cpMax)
            {
                lblStatus.Text = _sFindNoMatch.Replace("{text}", cboFind.Text);
                lblStatus.ForeColor = _colorRed;
            }
            else
            {
                if (nextRange.cpMin < Scintilla.AnchorPosition)
                {
                    if (chkInSelection.Checked)
                    {
                        lblStatus.Text = _sFindFirstMatchInSelection; //Search match wrapped to the beginning of the selection
                    }
                    else
                    {
                        lblStatus.Text = _sFindFirstMatchInFile; //Search match wrapped to the beginning of the document
                    }
                }

                lblStatus.ForeColor = _colorGreen;
                Scintilla.SetSel(nextRange.cpMin, nextRange.cpMax);
                MoveFormAwayFromSelection();
            }
        }

        private CharacterRange ReplaceNext(bool searchUp)
        {
            Regex rr = null;
            var selRange = new CharacterRange(_scintilla.Selections[0].Start, _scintilla.Selections[0].End);

            if (selRange.cpMax - selRange.cpMin > 0)
            {
                if (rdoRegularExpression.Checked)
                {
                    rr = new Regex(cboFind.Text, GetRegexOptions());
                    var selRangeText = Scintilla.GetTextRange(selRange.cpMin, selRange.cpMax - selRange.cpMin + 1);

                    if (selRange.Equals(FindReplace.Find(selRange, rr, false)))
                    {
                        if (searchUp)
                        {
                            _scintilla.SelectionStart = selRange.cpMin;
                            _scintilla.SelectionEnd = selRange.cpMax;
                            _scintilla.ReplaceSelection(rr.Replace(selRangeText, cboReplace.Text));
                            _scintilla.GotoPosition(selRange.cpMin);
                        }
                        else
                        {
                            Scintilla.ReplaceSelection(rr.Replace(selRangeText, cboReplace.Text));
                        }
                    }
                }
                else
                {
                    var textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;

                    if (selRange.Equals(FindReplace.Find(selRange, textToFind, false)))
                    {
                        if (searchUp)
                        {
                            var textToReplace = rdoExtended.Checked ? FindReplace.Transform(cboReplace.Text) : cboReplace.Text;
                            _scintilla.SelectionStart = selRange.cpMin;
                            _scintilla.SelectionEnd = selRange.cpMax;
                            _scintilla.ReplaceSelection(textToReplace);

                            _scintilla.GotoPosition(selRange.cpMin);
                        }
                        else
                        {
                            var textToReplace = rdoExtended.Checked ? FindReplace.Transform(cboReplace.Text) : cboReplace.Text;
                            Scintilla.ReplaceSelection(textToReplace);
                        }
                    }
                }
            }

            return FindNext(searchUp, ref rr);
        }

        private void btnReplacePrevious_Click(object sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;
            lblStatus.ForeColor = _colorNormal;

            if (cboFind.Text == string.Empty)
            {
                return;
            }

            UpdateFindList(); //btnReplacePrevious_Click
            UpdateReplaceList(); //btnReplacePrevious_Click

            CharacterRange nextRange;

            try
            {
                nextRange = ReplaceNext(true);
            }
            catch (ArgumentException ex)
            {
                lblStatus.Text = _sErrorRegularExpression + ex.Message;
                lblStatus.ForeColor = _colorRed;
                return;
            }

            if (nextRange.cpMin == nextRange.cpMax)
            {
                lblStatus.Text = _sFindNoMatch.Replace("{text}", cboFind.Text);
                lblStatus.ForeColor = _colorRed;
            }
            else
            {
                if (nextRange.cpMin > _scintilla.AnchorPosition)
                {
                    if (chkInSelection.Checked)
                    {
                        lblStatus.Text = _sFindFirstMatchInSelection; //Search match wrapped to the beginning of the selection
                    }
                    else
                    {
                        lblStatus.Text = _sFindFirstMatchInFile; //Search match wrapped to the beginning of the document
                    }
                }

                lblStatus.ForeColor = _colorGreen;
                _scintilla.SetSel(nextRange.cpMin, nextRange.cpMax);
                MoveFormAwayFromSelection();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnHighlightAll_Click(object sender, EventArgs e)
        {
            CountOrHighlight(false, true, true);
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            CountOrHighlight(false, false, false);
        }

        private void CountOrHighlight(bool bMarkLine, bool bHighlight, bool bHightlight)
        {
            lblStatus.Text = string.Empty;
            lblStatus.ForeColor = _colorNormal;
            var sTextInFile = bHightlight ? _sCountQtyMarkInFile : _sCountQtyMatchInFile;
            var sTextInSelection = bHightlight ? _sCountQtyMarkInSelection : _sCountQtyMatchInSelection;

            if (cboFind.Text == string.Empty)
            {
                return;
            }

            btnClearHighlights_Click(null, null);
            var foundCount = 0;

            #region RegEx
            if (rdoRegularExpression.Checked)
            {
                Regex rr = null;

                try
                {
                    rr = new Regex(cboFind.Text, GetRegexOptions());
                }
                catch (ArgumentException ex)
                {
                    lblStatus.Text = _sErrorRegularExpression + ex.Message;
                    lblStatus.ForeColor = _colorRed;
                    return;
                }

                if (chkInSelection.Checked)
                {
                    if (_searchRange.cpMin == _searchRange.cpMax)
                    {
                        _searchRange = new CharacterRange(_scintilla.Selections[0].Start, _scintilla.Selections[0].End);
                    }

                    foundCount = FindReplace.FindAll(_searchRange, rr, bMarkLine, bHighlight).Count;
                }
                else
                {
                    _searchRange = new CharacterRange();
                    foundCount = FindReplace.FindAll(rr, bMarkLine, bHighlight).Count;
                }
            }
            #endregion

            #region Non-RegEx
            if (!rdoRegularExpression.Checked)
            {
                if (chkInSelection.Checked)
                {
                    if (_searchRange.cpMin == _searchRange.cpMax)
                    {
                        _searchRange = new CharacterRange(_scintilla.Selections[0].Start, _scintilla.Selections[0].End);
                    }

                    var textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;
                    foundCount = FindReplace.FindAll(_searchRange, textToFind, GetSearchFlags(), bMarkLine, bHighlight).Count;
                }
                else
                {
                    _searchRange = new CharacterRange();
                    var textToFind = rdoExtended.Checked ? FindReplace.Transform(cboFind.Text) : cboFind.Text;
                    foundCount = FindReplace.FindAll(textToFind, GetSearchFlags(), bMarkLine, bHighlight).Count;
                }
            }
            #endregion

            if (chkInSelection.Checked)
            {
                lblStatus.Text = sTextInSelection.Replace("{qty}", foundCount.ToString());
            }
            else
            {
                lblStatus.Text = sTextInFile.Replace("{qty}", foundCount.ToString());
            }

            lblStatus.ForeColor = _colorBlue;
        }

        private void btnHelp_Options_Click(object sender, EventArgs e)
        {
            var sObjectName = "";
            var btn = sender as C1.Win.C1Input.C1Button;
            var sMsg = MyGlobal.GetLanguageString("", "form", Name, "object", "chk" + btn.Name.Replace("btnHelp_", ""), "ToolTipText");

            foreach (Control obj in pnlStandardOptions.Controls)
            {
                if (obj.GetType().Name != "C1CheckBox")
                {
                    continue;
                }

                if (obj.Name.Equals("chk" + btn.Name.Replace("btnHelp_", "")))
                {
                    sObjectName = obj.Text;
                    break;
                }
            }

            if (string.IsNullOrEmpty(sObjectName))
            {
                foreach (Control obj in pnlRegexpOptions.Controls)
                {
                    if (obj.GetType().Name != "C1CheckBox")
                    {
                        continue;
                    }

                    if (obj.Name.Equals("chk" + btn.Name.Replace("btnHelp_", "")))
                    {
                        sObjectName = obj.Text;
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(sMsg))
            {
                sMsg = sObjectName;
            }

            var sText = Text + " - " + sObjectName;
            FindAndMoveMsgBox(Cursor.Position.X - 30, Cursor.Position.Y + 15, true, sText);
            MessageBox.Show(sMsg, sText, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cboFind_BeforeDropDownOpen(object sender, CancelEventArgs e)
        {
            LoadFindList("Editor"); //cboFind_BeforeDropDownOpen
        }

        private void cboReplace_BeforeDropDownOpen(object sender, CancelEventArgs e)
        {
            UpdateReplaceList(); //cboReplace_BeforeDropDownOpen
        }

        private void cboReplace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13 || string.IsNullOrEmpty(cboReplace.Text.Trim()))
            {
                return;
            }

            btnReplaceNext.Focus();
        }

        private void UpdateFindList() //判斷是否要更新「搜尋清單」
        {
            if (cboFind.Tag.ToString() == cboFind.Text)
            {
                return;
            }

            if (cboFind.Items.Count > 0 && cboFind.Text == cboFind.Items[0].ToString())
            {
                //搜尋的字串是第一個，不用更新
            }
            else
            {
                SaveFindList("Editor", cboFind.Text); //UpdateFindList
            }

            cboFind.Tag = cboFind.Text;
        }

        private void UpdateReplaceList() //判斷是否要更新「搜尋清單」
        {
            if (cboReplace.Tag.ToString() == cboReplace.Text)
            {
                return;
            }

            if (cboReplace.Items.Count > 0 && cboReplace.Text == cboReplace.Items[0].ToString())
            {
                //搜尋的字串是第一個，不用更新
            }
            else
            {
                SaveReplaceList("Editor", cboReplace.Text); //UpdateFindList
            }

            cboReplace.Tag = cboFind.Text;
        }

        private void LoadFindList(string sFun)
        {
            var i = 0;
            cboFind.Items.Clear();

            try
            {
                var dtRecent = DBCommon.ExecQuery("Select AttributeValue From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='FindList_" + sFun + "' Order By AttributeDate Desc");

                if (dtRecent.Rows.Count <= 0)
                {
                    return;
                }

                for (var iRow = 0; iRow < dtRecent.Rows.Count; iRow++)
                {
                    if (i > 20)
                    {
                        break;
                    }

                    cboFind.Items.Add(dtRecent.Rows[iRow]["AttributeValue"].ToString());

                    i++;
                }
            }
            catch (Exception)
            {
                //throw
            }
        }

        private void SaveFindList(string sFun, string sFindText)
        {
            //Save
            var dtRecent = DBCommon.ExecQuery("Select * From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='FindList_" + sFun + "' And AttributeValue='" + sFindText.Replace("'", "''") + "'");

            if (dtRecent.Rows.Count > 0)
            {
                DBCommon.ExecNonQuery("Update SystemConfig Set AttributeDate='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='FindList_" + sFun + "' And AttributeValue='" + sFindText.Replace("'", "''") + "'");
            }
            else
            {
                DBCommon.ExecNonQuery("Insert Into SystemConfig (DomainUser, MPID, AttributeKey, AttributeValue, AttributeDate) Values ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'FindList_" + sFun + "', '" + sFindText.Replace("'", "''") + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')");
            }

            //Reload
            LoadFindList(sFun); //SaveFindList
        }

        private void LoadReplaceList()
        {
            var i = 0;

            cboReplace.Items.Clear();

            try
            {
                var dtReplace = DBCommon.ExecQuery("Select AttributeValue From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='ReplaceList_Editor" + "' Order By AttributeDate Desc");

                if (dtReplace.Rows.Count <= 0)
                {
                    return;
                }

                for (var iRow = 0; iRow < dtReplace.Rows.Count; iRow++)
                {
                    if (i > 20)
                    {
                        break;
                    }

                    cboReplace.Items.Add(dtReplace.Rows[iRow]["AttributeValue"].ToString());

                    i++;
                }
            }
            catch (Exception)
            {
                //throw
            }
        }

        private void SaveReplaceList(string sFun, string sReplaceText)
        {
            //Save
            var dtRecent = DBCommon.ExecQuery("Select * From SystemConfig Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='ReplaceList_" + sFun + "' And AttributeValue='" + sReplaceText.Replace("'", "''") + "'");

            if (dtRecent.Rows.Count > 0)
            {
                DBCommon.ExecNonQuery("Update SystemConfig Set AttributeDate='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' Where DomainUser='" + MyGlobal.sDomainUser + "' And MPID=" + MyGlobal.sDBMotherPID + " And AttributeKey='ReplaceList_" + sFun + "' And AttributeValue='" + sReplaceText.Replace("'", "''") + "'");
            }
            else
            {
                DBCommon.ExecNonQuery("Insert Into SystemConfig (DomainUser, MPID, AttributeKey, AttributeValue, AttributeDate) Values ('" + MyGlobal.sDomainUser + "', " + MyGlobal.sDBMotherPID + ", 'ReplaceList_" + sFun + "', '" + sReplaceText.Replace("'", "''") + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')");
            }

            //Reload
            LoadReplaceList(); //SaveReplaceList
        }

        private void btnExchange_Click(object sender, EventArgs e)
        {
            var sTemp = cboFind.Text;
            cboFind.Text = cboReplace.Text;
            cboReplace.Text = sTemp;
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

        //從母表單傳遞資訊至指定的子表單
        private void timerMother2Child_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MyGlobal.sGlobalTemp5) && MyGlobal.sGlobalTemp5.StartsWith("CanPaste")) //判斷是否允許 Replace 動作
            {
                var bValue = MyGlobal.sGlobalTemp5.Equals("CanPasteY") ? true : false;
                btnExchange.Enabled = bValue;
                btnReplaceAll.Enabled = bValue;
                btnReplaceNext.Enabled = bValue;
                btnReplacePrevious.Enabled = bValue;
                lblReplaceWith.Enabled = bValue;
                cboReplace.Enabled = bValue;

                if (bValue && lblReplaceWith.Tag != null)
                {
                    SendMessage(cboReplace.Handle, EM_SETCUEBANNER, 0, lblReplaceWith.Tag.ToString());
                }

                MyGlobal.sGlobalTemp5 = "";
                return;
            }

            //是否為 Reload Localization 套用？
            if (string.IsNullOrEmpty(MyGlobal.sInfoFromReloadLocalization) || !MyGlobal.sInfoFromReloadLocalization.StartsWith("reloadlocalization`"))
            {
                return;
            }

            if (MyGlobal.sInfoFromReloadLocalization == "reloadlocalization`")
            {
                MyGlobal.sInfoFromReloadLocalization = "";
            }

            ApplyLocalizationSetting(); //timerMother2Child_Tick()
        }

        private void ApplyLocalizationSetting()
        {
            MyGlobal.ApplyLanguageInfo(this, false);

            var a = Assembly.GetExecutingAssembly();

            if (MyLibrary.bDarkMode)
            {
                C1.Win.C1Themes.C1ThemeController.ApplicationTheme = "VS2013Dark";
                c1ThemeController1.SetTheme(c1StatusBar1, "ExpressionDark");
                pnlStatus.BackgroundImage = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.panel_C1Status_Dark.png"));
                btnCount.Image = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.Count 24x24 new2 Dark.ico"));
            }
            else
            {
                pnlStatus.BackgroundImage = new Bitmap(a.GetManifestResourceStream(a.GetName().Name + ".Image.panel_C1Status.png"));
            }

            var sLangText = MyGlobal.GetLanguageString("Find...", "form", Name, "object", "cboFind", "ToolTipText");
            SendMessage(cboFind.Handle, EM_SETCUEBANNER, 0, sLangText);
            sLangText = MyGlobal.GetLanguageString("Replace...", "form", Name, "object", "cboReplace", "ToolTipText");
            SendMessage(cboReplace.Handle, EM_SETCUEBANNER, 0, sLangText);
            lblReplaceWith.Tag = sLangText;

            sLangText = MyGlobal.GetLanguageString("Count", "form", Name, "object", "btnCount", "ToolTipText");
            toolTip1.SetToolTip(btnCount, sLangText);
            sLangText = MyGlobal.GetLanguageString("Highlight All", "form", Name, "object", "btnHighlightAll", "ToolTipText");
            toolTip1.SetToolTip(btnHighlightAll, sLangText);
            sLangText = MyGlobal.GetLanguageString("Clear Highlights", "form", Name, "object", "btnClearHighlights", "ToolTipText");
            toolTip1.SetToolTip(btnClearHighlights, sLangText);
            sLangText = MyGlobal.GetLanguageString("Exchange", "form", Name, "object", "btnExchange", "ToolTipText");
            toolTip1.SetToolTip(btnExchange, sLangText);

            //載入搜尋記錄
            LoadFindList("Editor"); //Form_Load
            LoadReplaceList(); //Form_Load
            cboFind.Tag = "";
            cboReplace.Tag = "";

            var iWidth = -2;
            btnHelp_MatchCase.Location = new Point(chkMatchCase.Left + chkMatchCase.Width + iWidth, btnHelp_MatchCase.Top);
            btnHelp_WholeWord.Location = new Point(chkWholeWord.Left + chkWholeWord.Width + iWidth, btnHelp_WholeWord.Top);
            btnHelp_WordStart.Location = new Point(chkWordStart.Left + chkWordStart.Width + iWidth, btnHelp_WordStart.Top);
            btnHelp_Compiled.Location = new Point(chkCompiled.Left + chkCompiled.Width + iWidth, btnHelp_Compiled.Top);
            btnHelp_CultureInvariant.Location = new Point(chkCultureInvariant.Left + chkCultureInvariant.Width + iWidth, btnHelp_CultureInvariant.Top);
            btnHelp_EcmaScript.Location = new Point(chkEcmaScript.Left + chkEcmaScript.Width + iWidth, btnHelp_EcmaScript.Top);
            btnHelp_ExplicitCapture.Location = new Point(chkExplicitCapture.Left + chkExplicitCapture.Width + iWidth, btnHelp_ExplicitCapture.Top);
            btnHelp_IgnoreCase.Location = new Point(chkIgnoreCase.Left + chkIgnoreCase.Width + iWidth, btnHelp_IgnoreCase.Top);
            btnHelp_IgnorePatternWhitespace.Location = new Point(chkIgnorePatternWhitespace.Left + chkIgnorePatternWhitespace.Width + iWidth, btnHelp_IgnorePatternWhitespace.Top);
            btnHelp_Multiline.Location = new Point(chkMultiline.Left + chkMultiline.Width + iWidth, btnHelp_Multiline.Top);
            btnHelp_RightToLeft.Location = new Point(chkRightToLeft.Left + chkRightToLeft.Width + iWidth, btnHelp_RightToLeft.Top);
            btnHelp_SingleLine.Location = new Point(chkSingleLine.Left + chkSingleLine.Width + iWidth, btnHelp_SingleLine.Top);

            _sErrorRegularExpression = MyGlobal.GetLanguageString("Error in Regular Expression: ", "form", Name, "msg", "ErrorRegularExpression", "Text");
            _sFindNoMatch = MyGlobal.GetLanguageString("Find: Can't find the text \"{text}\"", "form", Name, "msg", "FindNoMatch", "Text");
            _sFindFirstMatchInFile = MyGlobal.GetLanguageString("Find: Found the 1st occurrence from the top. The end of the document has been reached in entire file", "form", Name, "msg", "FindFirstMatchInFile", "Text");
            _sFindFirstMatchInSelection = MyGlobal.GetLanguageString("Find: Found the 1st occurrence from the top. The end of the document has been reached in selection", "form", Name, "msg", "FindFirstMatchInSelection", "Text");
            _sCountQtyMatchInFile = MyGlobal.GetLanguageString("Count: {qty} matches in entire file", "form", Name, "msg", "CountQtyMatchInFile", "Text");
            _sCountQtyMatchInSelection = MyGlobal.GetLanguageString("Count: {qty} matches in selection", "form", Name, "msg", "CountQtyMatchInSelection", "Text");
            _sCountQtyMarkInFile = MyGlobal.GetLanguageString("Mark: {qty} matches in entire file", "form", Name, "msg", "CountQtyMarkInFile", "Text");
            _sCountQtyMarkInSelection = MyGlobal.GetLanguageString("Mark: {qty} matches in selection", "form", Name, "msg", "CountQtyMarkInSelection", "Text");
            _sReplaceNoMatch = MyGlobal.GetLanguageString("Replace: no occurrence was found", "form", Name, "msg", "ReplaceNoMatch", "Text");
            _sReplaceAllQtyReplacedInFile = MyGlobal.GetLanguageString("Replace All: {qty} occurrence(s) were replaced in entire file", "form", Name, "msg", "ReplaceAllQtyReplacedInFile", "Text");
            _sReplaceAllQtyReplacedInSelection = MyGlobal.GetLanguageString("Replace All: {qty} occurrence(s) were replaced in selection", "form", Name, "msg", "ReplaceAllQtyReplacedInSelection", "Text");
        }
    }
}