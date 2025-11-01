using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphic_editor
{
    public class ShapeManager
    {
        private List<Shape> shapes;
        private Shape selectedShape;

        public ShapeManager()
        {
            shapes = new List<Shape>();
        }

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
        }

        public void RemoveShape(Shape shape)
        {
            shapes.Remove(shape);
        }

        public Shape GetShapeAtPoint(float x, float y)
        {
            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                if (shapes[i].ContainsPoint(x, y))
                {
                    return shapes[i];
                }
            }
            return null;
        }

        public void DrawAllShapes(Graphics graphics)
        {
            foreach (var shape in shapes)
            {
                shape.Draw(graphics);
            }
        }

        public void Clear()
        {
            shapes.Clear();
            selectedShape = null;
        }

        public List<Shape> Shapes => shapes;
        public Shape SelectedShape
        {
            get => selectedShape;
            set
            {
                if (selectedShape != null)
                    selectedShape.IsSelected = false;

                selectedShape = value;

                if (selectedShape != null)
                    selectedShape.IsSelected = true;
            }
        }
    }
}
