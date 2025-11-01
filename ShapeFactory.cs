using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphic_editor
{
    public class ShapeFactory
    {
        public Shape CreateShape(string shapeType, float x, float y, float width, float height)
        {
            switch (shapeType.ToLower())
            {
                case "rectangle":
                    return new RectangleShape(x, y, width, height);
                case "circle":
                    return new Circle(x, y, width, height);
                case "line":
                    return new Line(x, y, x + width, y + height);
                default:
                    throw new ArgumentException();
            }
        }
    }
}
