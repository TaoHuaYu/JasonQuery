using System;
using System.Drawing;
using System.Text;

namespace JasonLibrary.ColorPicker
{
    public class ColorSelectedArg : EventArgs
    {
        private Color _selectedColor;

        public Color Color => _selectedColor;

        public string HexColor { get; }

        public int R => _selectedColor.R;

        public int G => _selectedColor.R;

        public int B => _selectedColor.B;

        public ColorSelectedArg(Color selectedColor)
        {
            _selectedColor = selectedColor;
            var sb = new StringBuilder();

            sb.AppendFormat("#");
            sb.Append(BitConverter.ToString(new[] { _selectedColor.R }));
            sb.Append(BitConverter.ToString(new[] { _selectedColor.G }));
            sb.Append(BitConverter.ToString(new[] { _selectedColor.B }));
            HexColor = sb.ToString();
        }
    }
}