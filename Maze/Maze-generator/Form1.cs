using System;
using System.Drawing;
using System.Windows.Forms;

namespace Maze_generator
{
    public sealed partial class Form1 : Form
    {
        private Maze _maze;

        public Form1()
        {
            InitializeComponent();

            const int initialWidth = 15, initialHeight = 15;
            _maze = new Maze(new Size(initialWidth, initialHeight));
            numericWidth.Value = initialWidth;
            numericHeight.Value = initialHeight;

            Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var pen = new Pen(Color.Black, 1);

            var cellWidth = (float) panel1.Width/_maze.Size.Width;
            var cellHeight = (float) panel1.Height/_maze.Size.Height;

            // Draw begin and end
            var brush = new SolidBrush(Color.Red);
            g.FillRectangle(brush, 0, 0, cellWidth, cellHeight);
            brush.Color = Color.DodgerBlue;
            g.FillRectangle(brush, panel1.Width - cellWidth, panel1.Height - cellHeight, cellWidth, cellHeight);

            foreach (var edge in _maze.Edges)
            {
                if (Math.Abs(edge.Cell1 - edge.Cell2) > 1)
                {
                    // Draw a horizontal line
                    var x = (float) Math.Max(edge.Cell1, edge.Cell2)%_maze.Size.Width*cellWidth;
                    var y = (float) Math.Floor((double) Math.Min(edge.Cell1, edge.Cell2)/_maze.Size.Width + 1)*
                            cellHeight;
                    g.DrawLine(pen, x, y, x + cellWidth, y);
                }
                else
                {
                    // Draw a vertical line
                    var x = (float) Math.Max(edge.Cell1, edge.Cell2)%_maze.Size.Width*cellWidth;
                    var y = (float) Math.Floor((double) Math.Min(edge.Cell1, edge.Cell2)/_maze.Size.Width)*cellHeight;
                    g.DrawLine(pen, x, y, x, y + cellHeight);
                }
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e) => Refresh();

        private void generateBtn_Click(object sender, EventArgs e)
        {
            _maze = new Maze(new Size((int) numericWidth.Value, (int) numericHeight.Value));
            Refresh();
        }
    }
}