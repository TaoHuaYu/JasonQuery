using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace JasonLibrary.ColorPicker
{
    [DefaultEvent("ColorSelected")]
    public partial class ThemeColorPicker : UserControl
    {
        public int[] CustomColors { get; set; } = new int[0];

        private Color _c;
        private readonly Dictionary<string, Color> _dic;

        public delegate void colorSelected(object sender, ColorSelectedArg e);

        /// <summary>
        /// Occur when a color is selected.
        /// </summary>
        public event colorSelected ColorSelected;

        public delegate void moreColorWindowShow(object sender);

        /// <summary>
        /// Don't show pre-configure default Color Dialog
        /// </summary>
        public event moreColorWindowShow ShowCustomMoreColorWindow;

        public Color Color
        {
            get => _c;
            set
            {
                _c = value;

                if (ColorSelected == null)
                {
                    return;
                }

                var arg = new ColorSelectedArg(_c);
                ColorSelected(this, arg);
            }
        }

        public ThemeColorPicker()
        {
            InitializeComponent();

            _dic = new Dictionary<string, Color>
            {
                ["p01"] = Color.FromArgb(255, 255, 255),
                ["p02"] = Color.FromArgb(242, 242, 242),
                ["p03"] = Color.FromArgb(216, 216, 216),
                ["p04"] = Color.FromArgb(191, 191, 191),
                ["p05"] = Color.FromArgb(165, 165, 165),
                ["p06"] = Color.FromArgb(127, 127, 127),
                ["p11"] = Color.FromArgb(0, 0, 0),
                ["p12"] = Color.FromArgb(127, 127, 127),
                ["p13"] = Color.FromArgb(89, 89, 89),
                ["p14"] = Color.FromArgb(63, 63, 63),
                ["p15"] = Color.FromArgb(38, 38, 38),
                ["p16"] = Color.FromArgb(12, 12, 12),
                ["p21"] = Color.FromArgb(238, 236, 225),
                ["p22"] = Color.FromArgb(221, 217, 195),
                ["p23"] = Color.FromArgb(196, 189, 151),
                ["p24"] = Color.FromArgb(147, 137, 83),
                ["p25"] = Color.FromArgb(73, 68, 41),
                ["p26"] = Color.FromArgb(29, 27, 16),
                ["p31"] = Color.FromArgb(31, 73, 125),
                ["p32"] = Color.FromArgb(198, 217, 240),
                ["p33"] = Color.FromArgb(141, 179, 226),
                ["p34"] = Color.FromArgb(84, 141, 212),
                ["p35"] = Color.FromArgb(23, 54, 93),
                ["p36"] = Color.FromArgb(15, 36, 62),
                ["p41"] = Color.FromArgb(79, 129, 189),
                ["p42"] = Color.FromArgb(198, 217, 240),
                ["p43"] = Color.FromArgb(184, 204, 228),
                ["p44"] = Color.FromArgb(149, 179, 215),
                ["p45"] = Color.FromArgb(54, 96, 146),
                ["p46"] = Color.FromArgb(36, 64, 97),
                ["p51"] = Color.FromArgb(192, 80, 77),
                ["p52"] = Color.FromArgb(242, 220, 219),
                ["p53"] = Color.FromArgb(229, 185, 183),
                ["p54"] = Color.FromArgb(217, 150, 148),
                ["p55"] = Color.FromArgb(140, 51, 49),
                ["p56"] = Color.FromArgb(99, 36, 35),
                ["p61"] = Color.FromArgb(155, 187, 89),
                ["p62"] = Color.FromArgb(235, 241, 221),
                ["p63"] = Color.FromArgb(215, 227, 188),
                ["p64"] = Color.FromArgb(195, 214, 155),
                ["p65"] = Color.FromArgb(118, 146, 60),
                ["p66"] = Color.FromArgb(79, 97, 40),
                ["p71"] = Color.FromArgb(128, 100, 162),
                ["p72"] = Color.FromArgb(229, 224, 236),
                ["p73"] = Color.FromArgb(204, 193, 217),
                ["p74"] = Color.FromArgb(178, 162, 199),
                ["p75"] = Color.FromArgb(95, 73, 122),
                ["p76"] = Color.FromArgb(63, 49, 81),
                ["p81"] = Color.FromArgb(75, 172, 198),
                ["p82"] = Color.FromArgb(219, 238, 243),
                ["p83"] = Color.FromArgb(183, 221, 232),
                ["p84"] = Color.FromArgb(146, 205, 220),
                ["p85"] = Color.FromArgb(49, 133, 155),
                ["p86"] = Color.FromArgb(32, 88, 103),
                ["p91"] = Color.FromArgb(247, 150, 70),
                ["p92"] = Color.FromArgb(253, 234, 218),
                ["p93"] = Color.FromArgb(251, 213, 181),
                ["p94"] = Color.FromArgb(250, 192, 143),
                ["p95"] = Color.FromArgb(171, 82, 7),
                ["p96"] = Color.FromArgb(151, 72, 6),
                ["p07"] = Color.FromArgb(192,0,0),
                ["p17"] = Color.FromArgb(255,0,0),
                ["p27"] = Color.FromArgb(255,192,0),
                ["p37"] = Color.FromArgb(255,255,0),
                ["p47"] = Color.FromArgb(146,208,80),
                ["p57"] = Color.FromArgb(0,176,80),
                ["p67"] = Color.FromArgb(0,176,240),
                ["p77"] = Color.FromArgb(0,108,186),
                ["p87"] = Color.FromArgb(0,32,96),
                ["p97"] = Color.FromArgb(112,48,160)
            };

            SuspendLayout();

            for (var i = 1; i < 10; i++)
            {
                for (var j = 1; j < 8; j++)
                {
                    var p = new Panel();
                    p.Name = "p" + i + "" + j;
                    p.Size = new Size(13, 13);
                    var pt = Controls.Find("p" + (i - 1) + "" + j, false)[0].Location;
                    p.Location = new Point(pt.X + 4 + 13, pt.Y);
                    p.BackColor = Color.Transparent;
                    p.MouseClick += p_MouseClick;
                    p.Cursor = Cursors.Hand;
                    Controls.Add(p);
                }
            }

            ResumeLayout(false);
            PerformLayout();
        }

        private void p_MouseClick(object sender, MouseEventArgs e)
        {
            Color = _dic[((Control)sender).Name];
        }

        private void pnMoreColor_MouseClick(object sender, MouseEventArgs e)
        {
            if (ShowCustomMoreColorWindow != null)
            {
                ShowCustomMoreColorWindow(this);
            }
            else
            {
                ShowMoreColor();
            }
        }

        public virtual void ShowMoreColor()
        {
            var cd = new ColorDialog();
            cd.AllowFullOpen = true;
            cd.FullOpen = true;
            cd.Color = _c;
            cd.CustomColors = CustomColors;

            if (cd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Color = cd.Color;
            CustomColors = cd.CustomColors;
        }
    }
}