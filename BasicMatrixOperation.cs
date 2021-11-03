using System;

namespace Strassen_algorithm
{
    class BMO
    {

        public static double[,] Multiply(int n, double[,] a, double[,] b)
        {
            double[,] c = new double[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    double sum = 0;
                    for (int k = 0; k < n; ++k)
                    {
                        sum += a[i, k] * b[k, j];
                    }
                    c[i, j] = sum;
                }
            }
            return c;
        }

        public static double[,] Summation(int n, double[,] a, double[,] b)
        {
            double[,] c = new double[n,n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    c[i,j] = a[i,j] + b[i,j];
                }
            }
            return c;
        }

        public static double[,] Subtraction(int n, double[,] a, double[,] b)
        {
            double[,] c = new double[n,n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    c[i,j] = a[i,j] - b[i,j];
                }
            }
            return c;
        }
    }
}
