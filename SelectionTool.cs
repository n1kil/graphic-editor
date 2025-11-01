using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphic_editor
{
    public class SelectionTool : Tool
    {
        private ShapeManager shapeManager;
        private bool isDragging;
        private float offsetX, offsetY;

        public SelectionTool(ShapeManager manager)
        {
            shapeManager = manager;
        }

        public override void Activate()
        {
            isEditing = true;
        }

        public override void Deactivate()
        {
            isEditing = false;
            if (currentShape != null)
            {
                currentShape.IsSelected = false;
                currentShape = null;
            }
        }

        public override void HandleMouseDown(MouseEventArgs e)
        {
            var shape = shapeManager.GetShapeAtPoint(e.X, e.Y);

            if (shape != null)
            {
                if (currentShape != null && currentShape != shape)
                {
                    currentShape.IsSelected = false;
                }

                currentShape = shape;
                currentShape.IsSelected = true;
                isDragging = true;

                offsetX = e.X - currentShape.X;
                offsetY = e.Y - currentShape.Y;
            }
            else
            {
                if (currentShape != null)
                {
                    currentShape.IsSelected = false;
                    currentShape = null;
                }
            }
        }

        public override void HandleMouseMove(MouseEventArgs e)
        {
            if (isDragging && currentShape != null)
            {
                currentShape.X = e.X - offsetX;
                currentShape.Y = e.Y - offsetY;
            }
        }

        public override void HandleMouseUp(MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
