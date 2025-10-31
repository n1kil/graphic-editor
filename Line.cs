using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphic_editor
{
    public class Line : Shape
    {
        public float EndX { get; set; }
        public float EndY { get; set; }

        public Line(float startX, float startY, float endX, float endY)
            : base(startX, startY, Math.Abs(endX - startX), Math.Abs(endY - startY))
        {
            EndX = endX;
            EndY = endY;
        }

        public override void Draw(Graphics graphics)
        {
            using (var linePen = new Pen(BorderColor, BorderWidth))
            {
                graphics.DrawLine(linePen, X, Y, EndX, EndY);

                if (IsSelected)
                {
                    DrawSelection(graphics);
                }
            }
        }

        public override bool ContainsPoint(float x, float y)
        {
            float distance = PointToLineDistance(x, y, X, Y, EndX, EndY);
            return distance <= BorderWidth + 5; 
        }

        private float PointToLineDistance(float px, float py, float x1, float y1, float x2, float y2)
        {
            float A = px - x1;
            float B = py - y1;
            float C = x2 - x1;
            float D = y2 - y1;

            float dot = A * C + B * D;
            float lenSq = C * C + D * D;
            float param = (lenSq != 0) ? dot / lenSq : -1;

            float xx, yy;

            if (param < 0)
            {
                xx = x1;
                yy = y1;
            }
            else if (param > 1)
            {
                xx = x2;
                yy = y2;
            }
            else
            {
                xx = x1 + param * C;
                yy = y1 + param * D;
            }

            float dx = px - xx;
            float dy = py - yy;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        private void DrawSelection(Graphics graphics)
        {
            using (var selectionPen = new Pen(Color.Blue, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                float minX = Math.Min(X, EndX);
                float minY = Math.Min(Y, EndY);
                float maxX = Math.Max(X, EndX);
                float maxY = Math.Max(Y, EndY);
                graphics.DrawRectangle(selectionPen, minX - 2, minY - 2, maxX - minX + 4, maxY - minY + 4);
            }
        }
    }
}
