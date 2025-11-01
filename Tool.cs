using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphic_editor
{
    public abstract class Tool
    {

        protected Shape currentShape;
        protected bool isEditing;

        public abstract void Activate();
        public abstract void Deactivate();
        public abstract void HandleMouseDown(MouseEventArgs e);
        public abstract void HandleMouseMove(MouseEventArgs e);
        public abstract void HandleMouseUp(MouseEventArgs e);
    }
}
