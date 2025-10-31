using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphic_editor
{
    public class RectangleShape : Shape
    {
        public RectangleShape(float x, float y, float width, float height)
            : base(x, y, width, height) { }

        public override void Draw(Graphics graphics)
        {
            using (var borderPen = new Pen(BorderColor, BorderWidth))
            using (var fillBrush = new SolidBrush(FillColor))
            {
                if (FillColor != Color.Transparent)
                {
                    graphics.FillRectangle(fillBrush, X, Y, Width, Height);
                }
                graphics.DrawRectangle(borderPen, X, Y, Width, Height);

                if (IsSelected)
                {
                    DrawSelection(graphics);
                }
            }
        }

        public override bool ContainsPoint(float x, float y)
        {
            return x >= X && x <= X + Width && y >= Y && y <= Y + Height;
        }

        private void DrawSelection(Graphics graphics)
        {
            using (var selectionPen = new Pen(Color.Blue, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                graphics.DrawRectangle(selectionPen, X - 2, Y - 2, Width + 4, Height + 4);
            }
        }
    }

}
