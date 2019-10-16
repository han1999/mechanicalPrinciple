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
using mechanicalPrinciple.Export;

namespace mechanicalPrinciple
{
    public partial class Form1 : Form
    {
        public static Form2 form2 = new Form2();
        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            Common.way = 1;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            Common.way = 2;
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            Common.way = 3;
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            Common.way = 4;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = textBox2.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = textBox4.Text;   
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = textBox3.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox8.Text = textBox5.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = textBox6.Text;
        }

        private void groupBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textBox5.Text = textBox8.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out Common.pushBegin) && int.TryParse(textBox2.Text,out Common.pushEnd) &&
                int.TryParse(textBox4.Text, out Common.farBegin) && int .TryParse(textBox3.Text, out Common.farEnd) &&
                int.TryParse(textBox6.Text, out Common.backBegin) && int.TryParse(textBox5.Text, out Common.backEnd) &&
                int.TryParse(textBox8.Text, out Common.nearBegin) && int.TryParse(textBox7.Text, out Common.nearEnd) &&
                double.TryParse(textBox9.Text, out Common.h) && double.TryParse(textBox12.Text, out Common.e) &&
                double.TryParse(textBox10.Text, out Common.angel) && double.TryParse(textBox11.Text, out Common.r0))
            {
                //MessageBox.Show("正在计算中")
                Common.CalculateS();
                MyExport.ExportToExcel();

            }else
            {
                MessageBox.Show("数据不完整或有误！ 请检查。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out Common.pushBegin) && int.TryParse(textBox2.Text, out Common.pushEnd) &&
                int.TryParse(textBox4.Text, out Common.farBegin) && int.TryParse(textBox3.Text, out Common.farEnd) &&
                int.TryParse(textBox6.Text, out Common.backBegin) && int.TryParse(textBox5.Text, out Common.backEnd) &&
                int.TryParse(textBox8.Text, out Common.nearBegin) && int.TryParse(textBox7.Text, out Common.nearEnd) &&
                double.TryParse(textBox9.Text, out Common.h) && double.TryParse(textBox12.Text, out Common.e) &&
                double.TryParse(textBox10.Text, out Common.angel))
            {
                Common.CalculateS();
                int i =(int)Common.e+1;
                double aMax = 0;
                for (i=(int)(Common.e+1); i<1000; i++)
                {
                    aMax = 0;
                    double s0 = Math.Sqrt(i * i - Common.e * Common.e);
                    for (int j=0; j<Common.pushEnd; j++)
                    {
                        if (Common.s[j] + s0 == 0) continue;
                        double al = Math.Atan((Common.ds[j] - Common.e) / (Common.s[j] + s0));
                        if (al > aMax)
                        {
                            aMax = al;
                        }
                    }
                    if (aMax < Common.angel * Math.PI / 180)
                    {
                        break;
                    }
                }
                if (i == 1000)
                {
                    MessageBox.Show("Too big! 换参数！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //do
                //{
                //    double aMax = 0;
                //    i += 1;
                //    for (int j=0; j<=Common.pushEnd; j++)
                //    {
                //        double s0 = Math.Sqrt(i * i - Common.e * Common.e);
                //        if (Common.s[j] + s0 == 0) continue;
                //        double al = Math.Atan((Common.ds[j] -Common.e) / (Common.s[j] + s0));
                //        if (al > aMax)
                //        {
                //            aMax = al;
                //        }
                //    }
                //    if (i == 500)
                //    {
                //        MessageBox.Show("too big!");
                //        return;
                //    }
                //}while (aMax > Common.angel * Math.PI/180) ;
                //do
                //{
                //    i -= 1;
                //    if (i < Common.e) break;
                //    for (int j = 0; j <= Common.pushEnd; j++)
                //    {
                //        //			d=PI*f/dt0;
                //        //			ydgl();
                //        double s0 = Math.Sqrt(i * i - Common.e * Common.e);
                //        if (Common.s[j] + s0 == 0) continue;
                //        double al = Math.Atan((Common.ds[j] - Common.e) / (Common.s[j] + s0));
                //        if (al > aMax)
                //        {
                //            aMax = al;
                //        }
                //    }
                //} while ((aMax > Common.angel * Math.PI / 180) || (aMax <= (Common.angel-0.5) * Math.PI / 180));
                label5.Text = "基圆半径至少为： " + i;
                textBox11.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out Common.pushBegin) && int.TryParse(textBox2.Text, out Common.pushEnd) &&
               int.TryParse(textBox4.Text, out Common.farBegin) && int.TryParse(textBox3.Text, out Common.farEnd) &&
               int.TryParse(textBox6.Text, out Common.backBegin) && int.TryParse(textBox5.Text, out Common.backEnd) &&
               int.TryParse(textBox8.Text, out Common.nearBegin) && int.TryParse(textBox7.Text, out Common.nearEnd) &&
               double.TryParse(textBox9.Text, out Common.h) && double.TryParse(textBox12.Text, out Common.e) &&
               double.TryParse(textBox10.Text, out Common.angel) && double.TryParse(textBox11.Text, out Common.r0))
            {
                //MessageBox.Show("正在计算中")
                Common.CalculateS();
                //MessageBox.Show("导出过程可能需要十几秒时间，稍安勿躁！", "小提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Export.MyExport.ExportToExcel();
                Common.aMax = 0;
                    double s0 = Math.Sqrt(Common.r0 * Common.r0 - Common.e * Common.e);
                    for (int j = 0; j <= Common.pushEnd; j++)
                    {
                        if (Common.s[j] + s0 == 0) continue;
                        double al = Math.Atan((Common.ds[j] - Common.e) / (Common.s[j] + s0));
                        if (al > Common.aMax)
                        {
                            Common.aMax = al;
                            Common.maxDegree = j;
                        }
                    }
                Common.aMax *= 180 / Math.PI;
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("数据不完整或有误！ 请检查。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void 导出到excelTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.PerformClick();
        }

        private void 帮助HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请查看 https://github.com/han1999/mechanicalPrinciple", "贴士", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
