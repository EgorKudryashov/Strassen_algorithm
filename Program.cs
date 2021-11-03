using System;
using System.IO;

namespace Strassen_algorithm
{
    class Program
    {
        public static void PrintMatrix (int n, double[,] A)
        {
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Console.Write(A[i, j]);
                    Console.Write(' ');
                }
                Console.Write('\n');
            }
            Console.Write('\n');
        }

        public static int NewDim(int n)
        {
            int result = 1;
            while ((n >>= 1) != 0) result++;
            return (int)Math.Pow(2, result);
        }

        public static double[,] Addition2SquareMatrix(double[,] a, int n,  int N)
        {
            double[,] result = new double[N,N];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i,j] = a[i,j];
                }
            }
            return result;
        }
//**************************************************************************************
        public static void SplitMatrix (int n, double[,] a, double[,] a11, double[,] a12, double[,] a21, double[,] a22)
        {
            for (int i = 0; i < n; ++i)
            {
                for (int j=0; j<n; ++j)
                {
                    a11[i, j] = a[i, j];
                    a12[i, j] = a[i, j+n];
                    a21[i, j] = a[i+n, j];
                    a22[i, j] = a[i + n, j + n];
                }
            }
        }

        public static double[,] CollectMatrix(int n, double[,] a11, double[,] a12, double[,] a21, double[,] a22)
        {
            double[,] a = new double[n << 1, n << 1];

            for (int i = 0; i < n; ++i)
            {
                for (int j=0; j < n; ++j)
                {
                    a[i, j] = a11[i, j];
                    a[i, j + n] = a12[i, j];
                    a[i + n, j] = a21[i, j];
                    a[i + n, j + n] = a22[i, j];
                }
            }
            return a;
        }

//**************************************************************************************


        static (double[,], int) StrassenAlg (double[,] A, double[,] B, int n, int count)
        {
            if (n == 1)
            {
                return (BMO.Multiply(n, A, B), count+1);
            }

            n >>= 1; //Делим на 2

            double[,] a1 = new double[n,n];
            double[,] a2 = new double[n,n];
            double[,] a3 = new double[n,n];
            double[,] a4 = new double[n,n];

            double[,] b1 = new double[n,n];
            double[,] b2 = new double[n,n];
            double[,] b3 = new double[n,n];
            double[,] b4 = new double[n,n];

            SplitMatrix(n, A, a1, a2, a3, a4);
            SplitMatrix(n, B, b1, b2, b3, b4);

            double[,] P1, P2, P3, P4, P5, P6, P7;
            (P1, count) = StrassenAlg(BMO.Summation(n,a1, a4), BMO.Summation(n, b1, b4), n, count);
            (P2, count) = StrassenAlg(BMO.Summation(n, a3, a4), b1, n, count);
            (P3, count) = StrassenAlg(a1, BMO.Subtraction(n,b2, b4), n, count);
            (P4, count) = StrassenAlg(a4, BMO.Subtraction(n, b3, b1), n, count);
            (P5, count) = StrassenAlg(BMO.Summation(n, a1, a2), b4, n, count);
            (P6, count) = StrassenAlg(BMO.Subtraction(n, a3, a1), BMO.Summation(n,b1, b2), n, count);
            (P7, count) = StrassenAlg(BMO.Subtraction(n, a2, a4), BMO.Summation(n, b3, b4), n, count);

            double[,] C11 = BMO.Summation(n, BMO.Summation(n,P1, P4), BMO.Subtraction(n,P7, P5));
            double[,] C12 = BMO.Summation(n, P3, P5);
            double[,] C21 = BMO.Summation(n, P2, P4);
            double[,] C22 = BMO.Summation(n, BMO.Subtraction(n, P1, P2), BMO.Summation(n, P3, P6));

            return (CollectMatrix(n, C11, C12, C21, C22), count);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            StreamWriter inFile = new StreamWriter("C:\\Users\\Egor_Kudryashov\\Documents\\GitHub\\Strassen_algorithm\\Strassen_algorithm\\results.txt");
            for (int i = 0; i < 3; ++i)
            {
                int count = 0;
                int n = Generator.GenerateDim();
                inFile.Write(n);
                inFile.Write(' ');
                double[,] A = Generator.GenerateMatrix(n);
                double[,] B = Generator.GenerateMatrix(n);
                int N = NewDim(n);
                if (n != N / 2)
                {
                    A = Addition2SquareMatrix(A, n, N);
                    B = Addition2SquareMatrix(B, n, N);
                    n = N;
                }
                double[,] C;
                (C, count) = StrassenAlg(A, B, n, count);
                inFile.WriteLine(count);
                GC.Collect();
            }
            inFile.Close();

            //PrintMatrix(n, A);
            //PrintMatrix(n, B);
            //PrintMatrix(n, C);
            //PrintMatrix(n, BMO.Multiply(n, A, B));
            /*double[,] a1 = new double[n / 2, n / 2];
            double[,] a2 = new double[n / 2, n / 2];
            double[,] a3 = new double[n / 2, n / 2];
            double[,] a4 = new double[n / 2, n / 2];
            double[,] D = new double[n, n];
            SplitMatrix(n, A, a1, a2, a3, a4);
            D = CollectMatrix(n/2, a1, a2, a3, a4);
            PrintMatrix(n, D);*/
            //PrintMatrix(n, B);
            //PrintMatrix(n, C);
            //double[,] D = BMO.Multiply(n, A, B);
            //PrintMatrix(n, D);


        }
    }
}
