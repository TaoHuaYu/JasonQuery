using System;
using System.Drawing;
using System.Windows.Forms;

namespace JasonLibrary.ColorPicker
{
    public partial class ThemeColorPickerWindow : Form
    {
        private Color _c;

        public enum Action
        {
            HideWindow,
            CloseWindow,
            DoNothing
        }

        bool preventClose;

        public Action ActionAfterColorSelected { get; set; } = Action.CloseWindow;

        public Action ActionAfterLostFocus { get; set; } = Action.CloseWindow;

        public int[] CustomColors
        {
            get => themeColorPicker1.CustomColors;
            set => themeColorPicker1.CustomColors = value;
        }

        public Color Color
        {
            get => _c;
            set
            {
                _c = value;

                if (ColorSelected != null)
                {
                    ColorSelectedArg arg = new ColorSelectedArg(_c);
                    ColorSelected(this, arg);
                }

                switch (ActionAfterColorSelected)
                {
                    case Action.HideWindow:
                        Visible = false;
                        break;
                    case Action.CloseWindow:
                        Close();
                        break;
                    case Action.DoNothing:
                        break;
                }
            }
        }

        public delegate void colorSelected(object sender, ColorSelectedArg e);

        /// <summary>
        /// Occur when a color is selected.
        /// </summary>
        public event colorSelected ColorSelected;

        /// <summary>
        /// Create a new window for ThemeColorPicker.
        /// </summary>
        /// <param name="startLocation">The starting position on screen. Note: This is not location in Form.</param>
        /// <param name="borderStyle">How the border should displayed.</param>
        /// <param name="actionAfterColorSelected">The form action of 0o-.</param>
        /// <param name="actionAfterLostFocus"></param>
        public ThemeColorPickerWindow(Point startLocation, FormBorderStyle borderStyle, Action actionAfterColorSelected, Action actionAfterLostFocus)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = startLocation;
            FormBorderStyle = borderStyle;
            ActionAfterColorSelected = actionAfterColorSelected;
            ActionAfterLostFocus = actionAfterLostFocus;
            LostFocus += ThemeColorPickerWindow_LostFocus;
            Deactivate += ThemeColorPickerWindow_Deactivate;
            themeColorPicker1.ShowCustomMoreColorWindow += themeColorPicker1_ShowCustomMoreColorWindow;
        }

        private void themeColorPicker1_ShowCustomMoreColorWindow(object sender)
        {
            preventClose = true;
            var cd = new ColorDialog();
            cd.AllowFullOpen = true;
            cd.FullOpen = true;
            cd.Color = _c;
            cd.CustomColors = themeColorPicker1.CustomColors;

            if (cd.ShowDialog() == DialogResult.OK)
            {
                Color = cd.Color;
                themeColorPicker1.CustomColors = cd.CustomColors;
            }

            preventClose = false;
        }

        private void ThemeColorPickerWindow_Deactivate(object sender, EventArgs e)
        {
            if (preventClose)
                return;

            switch (ActionAfterLostFocus)
            {
                case Action.HideWindow:
                    Visible = false;
                    break;
                case Action.CloseWindow:
                    Close();
                    break;
            }
        }

        private void ThemeColorPickerWindow_LostFocus(object sender, EventArgs e)
        {
            switch (ActionAfterLostFocus)
            {
                case Action.HideWindow:
                    Visible = false;
                    break;
                case Action.CloseWindow:
                    Close();
                    break;
            }
        }

        private void themeColorPicker1_ColorSelected(object sender, ColorSelectedArg e)
        {
            Color = e.Color;
        }
    }
}