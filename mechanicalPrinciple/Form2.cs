using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mechanicalPrinciple.PublicElement;
using mechanicalPrinciple.Coordinate;
using System.Threading;
using mechanicalPrinciple.Threads;

namespace mechanicalPrinciple
{
    public partial class Form2 : Form
    {
        public static int step = 0;
        public static int status = 0;
        public static int isBegin = 0;
        public static int isPause = 0;
        public static int isAgain=0;

        public static double speed = 0.5;

        double rr = 5;
        Point[] points1, points2;
        int centerX = 1300 / 2, centerY = 700 / 2;
        int newE;
        int newR0; 

        Thread myThread = new Thread(new ThreadStart(MyThread.DrawThread));
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            label4.Text = "(" + e.X.ToString() + "," + e.Y.ToString() + ")";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out rr) && double.TryParse(textBox2.Text, out speed))
            {
                isBegin = 1;
                isAgain = 0;
                isPause = 0;
                button2.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = false;
                Form1.form2.Invalidate();
                status = -1;
            }else
            {
                MessageBox.Show("请检查左边数据！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isPause == 0)
            {
                isPause = 1;
                button2.Text = "暂停";
            }else
            {
                isPause = 0;
                button2.Text = "继续";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            isBegin = 0;
            isPause = 0;
            button2.Text = "暂停";
            button2.Enabled = false;
            button3.Enabled = false;
            button1.Enabled = true;
            isAgain = 1;
            step = 0;
            Form1.form2.Invalidate();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            button3.PerformClick();
            Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        public void Form2_Paint(object sender, PaintEventArgs e)
        {
                Graphics g = e.Graphics;
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                if (status == 0 || status==-1)
                {
                    double[] x = new double[370];
                    double[] y = new double[370];
                    double[] dx = new double[370];
                    double[] dy = new double[370];
                    double[] x1 = new double[370];
                    double[] y1 = new double[370];
                    double s0 = Math.Sqrt(Common.r0 * Common.r0 - Common.e * Common.e);

                    for (int i = 0; i <= 360; i++)
                    {
                        double ss = s0 + Common.s[i];
                        double d = i * Math.PI / 180;
                        double sd = Math.Sin(d);
                        double cd = Math.Cos(d);
                        x[i] = ss * sd + Common.e * cd;
                        y[i] = ss * cd - Common.e * sd;
                        double dss = Common.ds[i] - Common.e;
                        dx[i] = dss * sd + ss * cd;
                        dy[i] = dss * cd - ss * sd;
                        double q;
                        if (Math.Abs(dy[i]) > 1E-10)
                        {
                            q = Math.Atan(dx[i] / (-dy[i]));
                            q = q - (MyCoordinate.SNG(-dy[i]) - 1) * Math.PI / 2;
                        }
                        else
                        {
                            q = Math.PI / 2;
                            q = q - (MyCoordinate.SNG(-dy[i]) - 1) * Math.PI / 2;
                        }

                        x1[i] = x[i] - rr * Math.Cos(q);
                        y1[i] = y[i] - rr * Math.Sin(q);
                    }
                    double zoomMax = 250;
                    double max1 = 0;
                    for (int i = 0; i <= 360; i++)
                    {
                        if (x[i] > max1) max1 = x[i];
                        if (y[i] > max1) max1 = y[i];
                    }
                    double scale = zoomMax / max1;
                    for (int i = 0; i <= 360; i++)
                    {
                        x[i] *= scale;
                        y[i] *= scale;
                        x1[i] *= scale;
                        y1[i] *= scale;
                    }

                    //int centerX = 1300 / 2, centerY = 700 / 2;
                    int[] newX = MyCoordinate.TranslateX(x, centerX);
                    int[] newY = MyCoordinate.TranslateY(y, centerY);
                    int[] newX1 = MyCoordinate.TranslateX(x1, centerX);
                    int[] newY1 = MyCoordinate.TranslateY(y1, centerY);
                    points1 = new Point[361];
                    points2 = new Point[361];
                    for (int i = 0; i <= 360; i++)
                    {
                        points1[i] = new Point(newX[i], newY[i]);
                        points2[i] = new Point(newX1[i], newY1[i]);
                    }
                    newE = (int)(Common.e * scale);
                    newR0 = (int)(Common.r0 * scale);
                    //g.DrawEllipse(Common.pen1, centerX - newE, centerY - newE, 2 * newE, 2 * newE);
                    //g.DrawEllipse(Common.pen1, centerX - newR0, centerY - newR0, 2 * newR0, 2 * newR0);
                    if (status!=-1) myThread.Start();
                    status = 1;
                }
                g.DrawEllipse(Common.pen1, centerX - newE, centerY - newE, 2 * newE, 2 * newE);
                g.DrawEllipse(Common.pen1, centerX - newR0, centerY - newR0, 2 * newR0, 2 * newR0);
                if (step < 2 * 360) g.DrawLine(Common.pen1, centerX, centerY, points1[step % 360].X, points1[step % 360].Y);
                else g.DrawLine(Common.pen1, centerX, centerY, points2[step % 360].X, points2[step % 360].Y);
            g.DrawString("理论轮廓线各点参数："+
                "\ns[" + step%360   + "]=" + Common.s[step%360] +
                "\nv[" + step%360 + "]=" + Common.ds[step%360]  +
                "\na[" + step%360 + "]=" + Common.d2s[step%360] +
                "\n\n在转角为 "+Common.maxDegree+" 度时取得最大压力角" +
                "\n最大压力角（度）："+Common.aMax, new Font("宋体", 11, FontStyle.Bold), new SolidBrush(Color.Black),
                50, 200);
            int remarkX = 575, remarkY = 130;
            string stage = "";
            if (step <= 360) stage = "理论轮廓线";
            else if (step >= 360 && step <= 360 * 2) stage = "小滚子";
            else stage = "实际轮廓线";
            g.DrawString(stage, new Font("宋体", 20, FontStyle.Bold), new SolidBrush(Color.Blue), remarkX, remarkY-40);
            if (step % 360 <= Common.pushEnd)
            {
                g.DrawString("升程" +
                    " ("+Common.pushEnd+"度）", new Font("宋体", 15, FontStyle.Bold), new SolidBrush(Color.Red), remarkX, remarkY);
            }else if (step%360>=Common.pushEnd && step % 360 <= Common.backBegin)
            {
                g.DrawString("远休止" +
                    "("+(Common.backBegin-Common.pushEnd)+"度）", new Font("宋体", 15, FontStyle.Bold), new SolidBrush(Color.Red), remarkX, remarkY);
            }else if (step%360>=Common.backBegin && step % 360 <= Common.backEnd)
            {
                g.DrawString("回程" +
                    " (" + (Common.backEnd-Common.backBegin) + "度）", new Font("宋体", 15, FontStyle.Bold), new SolidBrush(Color.Red), remarkX, remarkY);
            }else {
                g.DrawString("近休止" +
                    " (" + (Common.nearEnd-Common.nearBegin)+ "度）", new Font("宋体", 15, FontStyle.Bold), new SolidBrush(Color.Red), remarkX, remarkY);
            }
            if (step >= Common.pushBegin)
            {
                g.DrawLine(Common.pen1, centerX, centerY, points1[Common.pushBegin].X, points1[Common.pushBegin].Y);
            }
            if (step  >= Common.pushEnd)
            {
                g.DrawLine(Common.pen2, centerX, centerY, points1[Common.pushEnd].X, points1[Common.pushEnd].Y);
            }
            if (step  >= Common.backBegin)
            {
                g.DrawLine(Common.pen3, centerX, centerY, points1[Common.backBegin].X, points1[Common.backBegin].Y);
            }
            if (step >= Common.backEnd)
            {
                g.DrawLine(Common.pen4, centerX, centerY, points1[Common.backEnd].X, points1[Common.backEnd].Y);
            }
            for (int i = 0; i < step; i++)
                {
                    if (i < 360)
                    {
                        g.DrawLine(Common.pen1, points1[i], points1[i + 1]);
                        //g.DrawLine(Common.pen1, centerX, centerY, points1[i + 1].X, points1[i + 1].Y);
                    }
                    else if (i >= 360 && i < 360 * 2)
                    {
                        g.DrawEllipse(Common.pen1, (float)(points1[i % 360].X - rr), (float)(points1[i % 360].Y - rr), 2 * (float)rr, 2 * (float)rr);
                        i++;
                    }
                    else if (i >= 360 * 2 && i < 360 * 3)
                    {
                        g.DrawLine(Common.pen1, points2[i % 360], points2[i % 360 + 1]);
                        //g.DrawLine(Common.pen1, centerX, centerY, points1[i%360 + 1].X, points1[i%360 + 1].Y);
                    }
                }
        }
            //else if (status == 1)
            //{
            //    g.DrawLine(Common.pen1, points1[step], points1[step + 1]);
                
            //}else if (status == 2)
            //{
            //    g.DrawEllipse(Common.pen1, (float)(points1[step].X - rr), (float)(points1[step].Y - rr), 2 * (float)rr, 2 * (float)rr);
            //}else if (status == 3)
            //{
            //    g.DrawLine(Common.pen1, points2[step], points2[step + 1]);
            //}
            //for (int i = 0; i < 360; i++)
            //{
            //    g.DrawLine(Common.pen1, points1[i], points1[i + 1]);
            //    System.Threading.Thread.Sleep(20);
            //}
            //for (int i = 0; i < 360; i += 2)
            //{
            //g.DrawEllipse(Common.pen1, (float)(newX[i] - rr), (float)(newY[i] - rr), 2 * (float)rr, 2 * (float)rr);
            //    System.Threading.Thread.Sleep(20);
            //}
            //Application.DoEvents();
            //for (int i = 0; i < 360; i++)
            //{
            //g.DrawLine(Common.pen1, points2[i], points2[i + 1]);
            //    System.Threading.Thread.Sleep(20);
            //}
            //g.DrawString("okkk", new Font("宋体", 10, FontStyle.Regular), new SolidBrush(Color.Black), 20, 20);
    }
}