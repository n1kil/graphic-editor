using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphic_editor
{
    public abstract class AbstractShape
    {
        public Point Position { get; set; }
        public Size Size { get; set; }
        public Color FillColor { get; set; } = Color.Blue;
        public Color StrokeColor { get; set; } = Color.Black;
        public float StrokeThickness { get; set; } = 2f;
        public bool IsSelected { get; set; }

        public abstract void Draw(Graphics graphics);
        public abstract bool Contains(Point point);
    }
}
