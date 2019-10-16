using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mechanicalPrinciple.Coordinate
{
    class MyCoordinate
    {
        //符号判定函数
        public static int SNG(double va)
        {
            if (va > 0) return 1;
            else if (va == 0) return 0;
            else return -1;
        }

        public static int[] TranslateX(int[] X,int centerX)
        {
            int[] newX = new int[X.Length];
            for (int i=0; i<X.Length; i++)
            {
                newX[i] = X[i] + centerX;
            }
            return newX;
        }

        public static int[] TranslateX(double[] x, int centerX)
        {
            int[] x1 = new int[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                x1[i] = (int)x[i];
            }
            return TranslateX(x1, centerX);
        }
        public static int[] TranslateY(int[] Y,int centerY)
        {
            int[] newY = new int[Y.Length];
            for (int i=0; i<Y.Length; i++)
            {
                newY[i] = centerY - Y[i];
            }
            return newY;
        }

        public static int[] TranslateY(double[] y, int centerY)
        {
            int[] y1 = new int[y.Length];
            for (int i = 0; i < y.Length; i++)
            {
                y1[i] = (int)y[i];
            }
            return TranslateY(y1, centerY);
        }
        public static void TranslateXY(ref int[] X, ref int[] Y, int centerX, int centerY)
        {
            X = TranslateX(X, centerX);
            Y = TranslateY(Y, centerY);
        }
    }
}
