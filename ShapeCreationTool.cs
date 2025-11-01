using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphic_editor
{
    public class ShapeCreationTool : Tool
    {
        private ShapeManager shapeManager;
        private ShapeFactory shapeFactory;
        private string shapeType;
        private float startX, startY;

        public ShapeCreationTool(ShapeManager manager, ShapeFactory factory)
        {
            shapeManager = manager;
            shapeFactory = factory;
        }

        public void SetShapeType(string type)
        {
            shapeType = type;
        }

        public override void Activate()
        {
            isEditing = true;
        }

        public override void Deactivate()
        {
            isEditing = false;
            currentShape = null;
        }

        public override void HandleMouseDown(MouseEventArgs e)
        {
            startX = e.X;
            startY = e.Y;

            currentShape = shapeFactory.CreateShape(shapeType, startX, startY, 0, 0);
        }

        public override void HandleMouseMove(MouseEventArgs e)
        {
            if (currentShape != null)
            {
                float width = e.X - startX;
                float height = e.Y - startY;

                if (currentShape is Line line)
                {
                    line.EndX = e.X;
                    line.EndY = e.Y;
                }
                else
                {
                    currentShape.Width = Math.Abs(width);
                    currentShape.Height = Math.Abs(height);

                    
                    if (width < 0) currentShape.X = e.X;
                    if (height < 0) currentShape.Y = e.Y;
                }
            }
        }

        public override void HandleMouseUp(MouseEventArgs e)
        {
            if (currentShape != null)
            {
                shapeManager.AddShape(currentShape);
                currentShape = null;
            }
        }
    }
}
