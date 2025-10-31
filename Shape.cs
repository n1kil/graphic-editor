using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphic_editor
{
    public abstract class Shape
    {
        private static int nextId = 1;

        public int Id { get; private set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public Color BorderColor { get; set; }
        public float BorderWidth { get; set; }
        public Color FillColor { get; set; }
        public bool IsSelected { get; set; }

        protected Shape(float x, float y, float width, float height)
        {
            Id = nextId++;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            BorderColor = Color.Black;
            BorderWidth = 2f;
            FillColor = Color.Transparent;
            IsSelected = false;
        }

        public abstract void Draw(Graphics graphics);
        public abstract bool ContainsPoint(float x, float y);

        public virtual void Move(float deltaX, float deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }

        public virtual void Resize(float newWidth, float newHeight)
        {
            Width = newWidth;
            Height = newHeight;
        }
    }
}
