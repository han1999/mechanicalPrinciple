using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mechanicalPrinciple.Draw
{
    class MyDrawing
    {
        //public static void DrawX(Graphics g, string str, float x0, float y0, float x, float y)
        //{
        //    g.DrawLine(Form1.pen2, x0, y0, x, y);
        //    int i = 0;
        //    if (str == "left")
        //    {
        //        i = -20;
        //    }
        //    else if (str == "right")
        //    {
        //        i = 20;
        //    }
        //    PointF[] xPt = new PointF[3] { new PointF(x, y - 6), new PointF(x, y + 6), new PointF(x + i, y) };
        //    g.FillPolygon(new SolidBrush(Color.Black), xPt);
        //}

        //public static void DrawY(Graphics g, string str, float x0, float y0, float x, float y)
        //{
        //    g.DrawLine(Form1.pen2, x0, y0, x, y);
        //    int i = 0;
        //    if (str == "up")
        //    {
        //        i = -20;
        //    }
        //    else if (str == "down")
        //    {
        //        i = 20;
        //    }
        //    PointF[] yPt = new PointF[3] { new PointF(x - 6, y), new PointF(x + 6, y), new PointF(x, y + i) };
        //    g.FillPolygon(new SolidBrush(Color.Black), yPt);
        //}

        //public static void DrawXY(Graphics g, float x, float y)
        //{
        //    MyDrawing.DrawX(g, "right", x, y, x + 600, y);
        //    g.DrawString("X", Form1.font2, Form1.brush1, x + 600, y + 10);
        //    MyDrawing.DrawY(g, "up", x, y + 100, x, y - 100);
        //    for (int i = (int)(x + 100); i <= x + 500; i += 100)
        //    {
        //        g.DrawLine(Form1.pen1, i, y, i, y - 7);
        //        g.DrawString((int)(i / 100) - 1 + "a", Form1.font2, Form1.brush1, i - 7, y + 5);
        //    }
        //}

    }
}
