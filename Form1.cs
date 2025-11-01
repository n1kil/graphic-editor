using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphic_editor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetSize();
            InitializeTools();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private class ArrayPoints
        {
            private int index = 0;
            private Point[] points;

            public ArrayPoints(int size)
            {
                if(size <= 0)
                {
                    size = 2;
                }
                points = new Point[size];
            }

            public void SetPoint(int x, int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }


            public void ResetPoints()
            {
                index = 0;
            }

            public int GetCountOfPoints()
            {
                return index;
            }

            public Point[] GetPoints()
            {
                return points;
            }
        }

        private ArrayPoints arrayPoints = new ArrayPoints(2);

        Bitmap map = new Bitmap(100, 100);
        Graphics graphics;
        Pen pen = new Pen(Color.Black, 3f);

        private Color previousColor;

        Bitmap shapesLayer;
        Graphics shapesGraphics;

        private ShapeManager shapeManager = new ShapeManager();
        private ShapeFactory shapeFactory = new ShapeFactory();
        private Tool currentTool;
        private SelectionTool selectionTool;
        private ShapeCreationTool shapeCreationTool;
        private string currentShapeType = "rectangle";
        private bool isDrawingShapes = false;

        private void InitializeTools()
        {
            selectionTool = new SelectionTool(shapeManager);
            shapeCreationTool = new ShapeCreationTool(shapeManager, shapeFactory);
        }
        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);

            shapesLayer = new Bitmap(rectangle.Width, rectangle.Height);
            shapesGraphics = Graphics.FromImage(shapesLayer);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            shapesGraphics.Clear(Color.Transparent);
            shapeManager.Clear();
            pictureBox1.Image = map;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
            previousColor = pen.Color;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK) 
            { 
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private bool isMouse = false;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;

            if (isDrawingShapes && currentTool != null)
            {
                currentTool.HandleMouseDown(e);
            }
            else
            {
                arrayPoints.SetPoint(e.X, e.Y);
            }

            RefreshCanvas();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;

            if (isDrawingShapes && currentTool != null)
            {
                currentTool.HandleMouseUp(e);
            }
            else
            {
                arrayPoints.ResetPoints();
            }

            RefreshCanvas();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouse)
            {
                return;
            }

            if (isDrawingShapes && currentTool != null)
            {
                currentTool.HandleMouseMove(e);
            }
            else
            {
                arrayPoints.SetPoint(e.X, e.Y);
                if (arrayPoints.GetCountOfPoints() >= 2)
                {
                    graphics.DrawLines(pen, arrayPoints.GetPoints());
                    pictureBox1.Image = map;
                    arrayPoints.SetPoint(e.X, e.Y);
                }
            }

            RefreshCanvas();
        }

        private void RefreshCanvas()
        {
            
            var tempBitmap = new Bitmap(map);
            using (var tempGraphics = Graphics.FromImage(tempBitmap))
            {
                
                shapeManager.DrawAllShapes(tempGraphics);
            }

            pictureBox1.Image = tempBitmap;
        }

        
        private void EnableShapeDrawing(string shapeType)
        {
            isDrawingShapes = true;
            currentShapeType = shapeType;
            shapeCreationTool.SetShapeType(shapeType);
            currentTool = shapeCreationTool;
            currentTool.Activate();
        }

        private void EnableSelectionTool()
        {
            isDrawingShapes = true;
            currentTool = selectionTool;
            currentTool.Activate();
        }

        private void EnableFreeDrawing()
        {
            isDrawingShapes = false;
            if (currentTool != null)
            {
                currentTool.Deactivate();
                currentTool = null;
            }
        }



        private void button3click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            if(isDrawingShapes != false)
            {
                EnableFreeDrawing();
            }
            previousColor = pen.Color;
            pen.Color = pictureBox1.BackColor;
        }

     

        private void button19_Click(object sender, EventArgs e)
        {
            pen.Color = previousColor;
            EnableFreeDrawing();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            EnableShapeDrawing("rectangle");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            EnableSelectionTool();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            EnableShapeDrawing("circle");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            EnableShapeDrawing("line");
        }
    }
}
