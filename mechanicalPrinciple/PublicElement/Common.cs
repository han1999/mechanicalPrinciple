using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mechanicalPrinciple.PublicElement
{
    class Common
    {
        const double PI = Math.PI;
        public static double[] s = new double[370];
        public static double[] ds = new double[370];
        public static double[] d2s = new double[370];
        public static int way = 0;
        public static int pushBegin, pushEnd, farBegin, farEnd, backBegin, backEnd, nearBegin, nearEnd;
        public static double h, e, angel, r0;
        public static double aMax;
        public static int maxDegree;

        public static Pen pen1 = new Pen(Color.Black, 2);
        public static Pen pen2 = new Pen(Color.Red, 2);
        public static Pen pen3 = new Pen(Color.Blue, 2);
        public static Pen pen4 = new Pen(Color.Green, 2);


        public static void Delay(int mm)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now)
            {
                Application.DoEvents();
            }
            return;
        }

        public static void CalculateS()
        {
            for (int i = 0; i < 361; i++)
            {
                if (way == 0)
                {
                    return;
                }
                else if (way == 1)
                {
                    if (i>=pushBegin && i <= pushEnd)
                    {
                        double Len = pushEnd - pushBegin;
                        double x = i / Len;
                        s[i] = h * x;
                        double dt0 = Len * PI / 180;
                        ds[i] = h / dt0;
                        d2s[i] = 0;
                    }
                    if (i>=farBegin && i < farEnd)
                    {
                        s[i] = h;
                        ds[i] = 0;
                        d2s[i] = 0;
       
                    }
                    if (i>=backBegin && i <=backEnd)
                    {
                        double temp = backEnd - i;
                        double tempLen = backEnd - backBegin;
                        double tempX = temp / tempLen;
                        s[i] = h * tempX;
                        double dt0 = tempLen * PI / 180;
                        ds[i] = -(h / dt0);
                        d2s[i] = 0;
                    }
                    if (i>=nearBegin && i <= nearEnd)
                    {
                        s[i] = 0;
                        ds[i] = 0;
                        d2s[i] = 0;
                    }
                }else if (way == 2)
                {
                    if (i >= pushBegin && i <= pushEnd/2)
                    {
                        double Len = pushEnd - pushBegin;
                        double x = i / Len;
                        s[i] = 2*h * x*x;
                        double dt0 = Len * PI / 180;
                        ds[i] = 4 * h * i * PI / 180 / (dt0 * dt0);
                        d2s[i] = 4 * h / (dt0 * dt0);

                    }
                    if (i>=pushEnd/2 && i < farBegin)
                    {
                        double Len = pushEnd - pushBegin;
                        double x = i / Len;
                        double dt0 = Len * PI / 180;
                        s[i] = h - 2 * h * (dt0 - i * PI / 180) * (dt0 - i * PI / 180) / (dt0 * dt0);
                        ds[i] = 4 * h * (dt0 - i * PI / 180) / (dt0 * dt0);
                        d2s[i] = -4 * h / (dt0 * dt0);
                    }
                    if (i >= farBegin && i < farEnd)
                    {
                        s[i] = h;
                        ds[i] = 0;
                        d2s[i] = 0;
                    }
                    if (i >= backBegin && i <= backBegin + (backEnd - backBegin) / 2)
                    {
                        double temp = backEnd - i;
                        double tempLen = backEnd - backBegin;
                        double tempX = temp / tempLen;
                        double dt0 = tempLen * PI / 180;
                        s[i] = h - 2 * h * (dt0 - temp * PI / 180) * (dt0 - temp * PI / 180) / (dt0 * dt0);
                        ds[i] = -4 * h * (dt0 - temp * PI / 180) / (dt0 * dt0);
                        d2s[i] = -4 * h / (dt0 * dt0);
                    }
                    if (i >= backBegin + (backEnd - backBegin) / 2 && i <= backEnd)
                    {
                        double temp = backEnd - i;
                        double tempLen = backEnd - backBegin;
                        double tempX = temp / tempLen;
                        s[i] = 2 * h * tempX * tempX;
                        double dt0 = tempLen * PI / 180;
                        ds[i] = -(4 * h * temp * PI / 180 / (dt0 * dt0));
                        d2s[i] = 4 * h / (dt0 * dt0);
                    }
                    if (i >= nearBegin && i <= nearEnd)
                    {
                        s[i] = 0;
                        ds[i] = 0;
                        d2s[i] = 0;
                    }
                }else if (way == 3)
                {
                    if (i >= pushBegin && i <= pushEnd)
                    {
                        double Len = pushEnd - pushBegin;
                        double x = i / Len;
                        s[i] = h / 2 * (1 - Math.Cos(PI * x));
                        double dt0 = Len * PI / 180;
                        ds[i] = PI * h / (2 * dt0) * Math.Sin(PI * x);
                        d2s[i] = PI * PI * h / (2 * dt0 * dt0) * Math.Cos(PI * x);
                    }
                    if (i >= farBegin && i < farEnd)
                    {
                        s[i] = h;
                        ds[i] = 0;
                        d2s[i] = 0;
                    }
                    if (i >= backBegin && i <= backEnd)
                    {
                        double temp = backEnd - i;
                        double tempLen = backEnd - backBegin;
                        double tempX = temp / tempLen;
                        s[i] = h / 2 * (1 - Math.Cos(PI * tempX));
                        double dt0 = tempLen * PI / 180;
                        ds[i] =-( PI * h / (2 * dt0) * Math.Sin(PI * tempX));
                        d2s[i] = PI * PI * h / (2 * dt0 * dt0) * Math.Cos(PI * tempX);
                    }
                    if (i >= nearBegin && i <= nearEnd)
                    {
                        s[i] = 0;
                        ds[i] = 0;
                        d2s[i] = 0;
                    }
                }else if (way == 4) {
                    if (i >= pushBegin && i <= pushEnd)
                    {
                        double Len = pushEnd - pushBegin;
                        double x = i / Len;
                        double dt0 = Len * PI / 180;
                        s[i] = h * (x - 1 / (2 * PI) * Math.Sin(2 * PI * x));
                        ds[i] = h / dt0 * (1 - Math.Cos(2 * PI * x));
                        d2s[i] = 2 * PI * h * Math.Sin(2 * PI * x) / (dt0 * dt0);
                    }
                    if (i >= farBegin && i < farEnd)
                    {
                        s[i] = h;
                        ds[i] = 0;
                        d2s[i] = 0;
                    }
                    if (i >= backBegin && i <= backEnd)
                    {
                        double temp = backEnd - i;
                        double tempLen = backEnd - backBegin;
                        double tempX = temp / tempLen;
                        double dt0 = tempLen * PI / 180;
                        s[i] = h * (tempX - 1 / (2 * PI) * Math.Sin(2 * PI * tempX));
                        ds[i] = -h / dt0 * (1 - Math.Cos(2 * PI * tempX));
                        d2s[i] = 2 * PI * h * Math.Sin(2 * PI * tempX) / (dt0 * dt0);
                    }
                    if (i >= nearBegin && i <= nearEnd)
                    {
                        s[i] = 0;
                        ds[i] = 0;
                        d2s[i] = 0;
                    }
                }
            }
        }

    }
}
