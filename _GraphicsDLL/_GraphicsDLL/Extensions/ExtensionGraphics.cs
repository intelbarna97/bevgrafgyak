using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _GraphicsDLL
{
    public static class ExtensionGraphics
    {
        public static void DrawPixel(this Graphics g, Pen pen, float x, float y)
        {
            g.DrawRectangle(pen, x, y, 0.5f, 0.5f);
        }
        public static void DrawPixel(this Graphics g, Color color, float x, float y)
        {
            g.DrawPixel(new Pen(color), x, y);
        }
        public static void DrawPixel(this Graphics g, Color color, PointF p)
        {
            g.DrawPixel(color, p.X, p.Y);
        }

        public static void DrawLineDDA(this Graphics g, Pen pen, float x1, float y1,
                                                                 float x2, float y2)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            float length = Math.Abs(dx);
            if (length < Math.Abs(dy))
                length = Math.Abs(dy);
            float incX = dx / length;
            float incY = dy / length;
            float x = x1;
            float y = y1;
            g.DrawPixel(pen, x, y);
            for (int i = 0; i < length; i++)
            {
                x += incX;
                y += incY;
                g.DrawPixel(pen, x, y);
            }
        }
        public static void DrawLineDDA(this Graphics g, Color color, float x1, float y1,
                                                                     float x2, float y2)
        {
            g.DrawLineDDA(new Pen(color), x1, y1, x2, y2);
        }
        public static void DrawLineDDAColor(this Graphics graphics, Color color1, Color color2, float x1, float y1,
                                                                                    float x2, float y2)
        {
            float r1, r2, g1, g2, b1, b2;
            Color color;
            r1 = color1.R;
            r2 = color2.R;
            g1 = color1.G;
            g2 = color2.G;
            b1 = color1.B;
            b2 = color2.B;
            float dx = x2 - x1;
            float dy = y2 - y1;
            float dr = r2 - r1;
            float dg = g2 - g1;
            float db = b2 - b1;
            float length = Math.Abs(dx);
            if (length < Math.Abs(dy))
                length = Math.Abs(dy);
            float incX = dx / length;
            float incY = dy / length;
            float incR = dr / length;
            float incG = dg / length;
            float incB = db / length;

            float x = x1;
            float y = y1;
            float r = r1;
            float g = g1;
            float b = b1;
            color = Color.FromArgb((int)r,(int)g,(int)b);
            graphics.DrawPixel(color, x, y);
            for (int i = 0; i < length; i++)
            {
                x += incX;
                y += incY;
                r += incR;
                g += incG;
                b += incB;

                color = Color.FromArgb((int)r,(int)g,(int)b);

                graphics.DrawPixel(color, x, y);
            }
        }
        public static void DrawLineMidPoint(this Graphics g, Color color, int x1, int y1,
                                                                          int x2, int y2)
        {

        }
        public static void DrawLineMidPoint(this Graphics g, Color color, float x1, float y1,
                                                                          float x2, float y2)
        {
            g.DrawLineMidPoint(color, (int)x1, (int)y1, (int)x2, (int)y1);
        }
    }
}
