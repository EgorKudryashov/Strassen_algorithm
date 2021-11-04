using System;

namespace Strassen_algorithm
{
    class Generator
    {
        public static int GenerateDim()
        {
            Random random = new Random();
            int n = 32+random.Next()%992;
            return n;
        }

        public static double[,] GenerateMatrix(int n)
        {
            Random rand = new Random();
            double[,] A = new double[n, n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    A[i, j] = ((rand.Next()%5 == 0) ? -1 : 1) * rand.Next() % 100;
                }
            }

            return A;
        }

    }
}
