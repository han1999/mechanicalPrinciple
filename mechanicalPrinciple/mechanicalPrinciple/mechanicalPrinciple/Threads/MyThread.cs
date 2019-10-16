using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mechanicalPrinciple;
using System.Threading;
using System.Drawing;

namespace mechanicalPrinciple.Threads
{
    class MyThread
    {
        public static void DrawThread()
        {
            while (true)
            {
                if (Form2.isAgain == 1)
                {
                    Form2.step = 0;
                }
                if (Form2.isBegin == 1)
                {
                    if (Form2.isPause != 1)
                    {

                        Form2.step++;
                        //if (Form2.step >= 360 && Form2.status == 1)
                        //{
                        //    Form2.status = 2;
                        //    Form2.step = 0;
                        //}
                        //else if (Form2.step >= 360 && Form2.status == 2)
                        //{
                        //    Form2.status = 3;
                        //    Form2.step = 0;
                        //}
                        //else if (Form2.step >= 360 && Form2.status == 3)
                        //{
                        //    Form2.status = 4;
                        //    Form2.step = 0;
                        //}
                        //if (Form2.step == 360 * 3)
                        // {
                        //     Form2.step = 0;
                        // }
                    }
                }
                if (Form2.step >= 3 * 360 )
                {
                    Form2.step = 3 * 360 ;
                }
                Form1.form2.Invalidate();
                Thread.Sleep((int)(Math.PI/(Form2.speed*180)*1000));
            }
        }
    }
}
