using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            oldstates = new bool[250 * 250];
            newstates = new bool[250 * 250];
            Randomize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Step();
            Invalidate();
        }

        int rows = 250;
        int columns = 250;
        bool[] oldstates;
        bool[] newstates;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            float cellWidth = 2;
            float cellHeight = 2;

            Graphics g = e.Graphics;
            SolidBrush coloralive = new SolidBrush(Color.Yellow); // initialize brushes
            SolidBrush colordead = new SolidBrush(Color.Black);
            
            g.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, Width, Height)); // draw pixel

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (oldstates[x + y * columns])
                    {
                        g.FillRectangle(coloralive, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                    }
                    else
                    {
                        g.FillRectangle(colordead, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                    }
                }
            }
        }

        void Randomize()
        {
            Random r = new Random();

            for (int a = 0; a < oldstates.Length; a++)
            {
                if (r.NextDouble() < 0.25)
                    oldstates[a] = true;
                else
                    oldstates[a] = false;
            }
        }

        void Step()
        {
            int neighbors;
            int index;
            bool alive;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    neighbors = getNeighbors(x, y);
                    index = x + y * columns;
                    alive = oldstates[index];

                    if ((alive && (neighbors == 2 || neighbors == 3)) || (!alive && neighbors == 3))
                        newstates[index] = true;
                    else
                        newstates[index] = false;

                   
                }
            }
            newstates.CopyTo(oldstates, 0);
        }


        private int getNeighbors(int x, int y)
        {
            int neighborCount = 0;

            /* N  */
            if ((y > 0) && oldstates[x + (y - 1) * columns]) { neighborCount += 1; }
            /* E  */
            if ((x > 0) && oldstates[(x - 1) + y * columns]) { neighborCount += 1; }
            /* S  */
            if ((y + 1 < rows) && oldstates[x + (y + 1) * columns]) { neighborCount += 1; }
            /* W  */
            if ((x + 1 < columns) && oldstates[(x + 1) + y * columns]) { neighborCount += 1; }

            /* NW */
            if ((x > 0 && y > 0) && oldstates[x - 1 + (y - 1) * columns])
            { neighborCount += 1; }
            /* NE */
            if ((x + 1 < columns && y > 0) && oldstates[x + 1 + (y - 1) * columns])
            { neighborCount += 1; }
            /* SE */
            if ((x > 0 && y + 1 < rows) && oldstates[x - 1 + (y + 1) * columns])
            { neighborCount += 1; }
            /* SW */
            if ((x + 1 < columns && y + 1 < rows) && oldstates[x + 1 + (y + 1) * columns])
            { neighborCount += 1; }

            return neighborCount;
        }
    }
}
