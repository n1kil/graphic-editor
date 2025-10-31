using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphic_editor
{
    public class Circle : Shape
    {
        public Circle(float x, float y, float width, float height) : base(x, y, width, height) { }

        public override void Draw(Graphics graphics)
        {
            using (var borderPen = new Pen(BorderColor, BorderWidth))
            using (var fillBrush = new SolidBrush(FillColor))
            {
                if (FillColor != Color.Transparent)
                {
                    graphics.FillEllipse(fillBrush, X, Y, Width, Height);
                }
                graphics.DrawEllipse(borderPen, X, Y, Width, Height);

                if (IsSelected)
                {
                    DrawSelection(graphics);
                }
            }
        }

        public override bool ContainsPoint(float x, float y)
        {
            float centerX = X + Width / 2;
            float centerY = Y + Height / 2;
            float radiusX = Width / 2;
            float radiusY = Height / 2;

            return Math.Pow((x - centerX) / radiusX, 2) + Math.Pow((y - centerY) / radiusY, 2) <= 1;
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
