using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _GraphicsDLL;

namespace _GraphicsWF
{
    public partial class Form1 : Form
    {
        Graphics g;
        List<PointF> p = new List<PointF>();
        int grabbed = -1;
        Color color1 = Color.Red;
        Color color2 = Color.Blue;

        public Form1()
        {
            InitializeComponent();            
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            int colorSplitInc = 0;


            float r1, r2, g1, g2, b1, b2;
            r1 = color1.R;
            r2 = color2.R;
            g1 = color1.G;
            g2 = color2.G;
            b1 = color1.B;
            b2 = color2.B;


            float dr = r2 - r1;
            float dg = g2 - g1;
            float db = b2 - b1;

            float incR = 0;
            float incG = 0;
            float incB = 0;


            if (p.Count > 1)
            {
                incR = dr / p.Count;
                incG = dg / p.Count;
                incB = db / p.Count;
            }
            float colorSplitRed = r1;
            float colorSplitGreen = g1;
            float colorSplitBlue = b1;

            for (int i = 0; i < p.Count - 1; i++)
            {
                if (i == p.Count - 2)
                {
                    g.DrawLineDDAColor(Color.FromArgb((int)colorSplitRed, (int)colorSplitGreen, (int)colorSplitBlue), Color.FromArgb((int)r2, (int)g2, (int)b2), p[i].X, p[i].Y, p[i + 1].X, p[i + 1].Y);
                }
                else
                g.DrawLineDDAColor(Color.FromArgb((int)colorSplitRed, (int)colorSplitGreen, (int)colorSplitBlue), Color.FromArgb((int)(colorSplitRed + incR), (int)(colorSplitGreen + incG),(int) (colorSplitBlue + incB)), p[i].X, p[i].Y, p[i + 1].X, p[i + 1].Y);
                colorSplitRed += incR;
                colorSplitGreen += incG;
                colorSplitBlue += incB;
            
            }

            for (int i = 0; i < p.Count; i++)
            {
                g.FillRectangle(new SolidBrush(canvas.BackColor), p[i].X - ExtensionPointF.GRAB_DISTANCE,
                                                                  p[i].Y - ExtensionPointF.GRAB_DISTANCE,
                                                                  2 * ExtensionPointF.GRAB_DISTANCE,
                                                                  2 * ExtensionPointF.GRAB_DISTANCE);
                g.DrawRectangle(Pens.Black, p[i].X - ExtensionPointF.GRAB_DISTANCE,
                                            p[i].Y - ExtensionPointF.GRAB_DISTANCE,
                                            2 * ExtensionPointF.GRAB_DISTANCE,
                                            2 * ExtensionPointF.GRAB_DISTANCE);
            }
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    for (int i = 0; i < p.Count; i++)
                    {
                        if (p[i].IsGrabbedBy(e.Location))
                            grabbed = i;
                    }
                    if (grabbed == -1)
                    {
                        p.Add(new PointF(e.X, e.Y));
                        grabbed = p.Count - 1;
                        canvas.Invalidate();
                    }
                    break;
                case MouseButtons.Right:
                    break;
                default:
                    break;
            }
        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    grabbed = -1;
                    break;
                case MouseButtons.Right:
                    break;
                default:
                    break;
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (grabbed != -1)
            {
                p[grabbed] = new PointF(e.X, e.Y);
                canvas.Invalidate();
            }
        }
       
    }
}
