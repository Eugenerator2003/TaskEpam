using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task4TcpIp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Task4TcpIp.Tests
{
    [TestClass()]
    public class GaussMethodTests
    {
        [TestMethod()]
        public void SolveTest()
        {
            double[,] matrix = new double[,] { { 4, 1, 2, 3 }, { -2, 3, 1, 10 }, { -2, 4, 1, 7 } };
            double[] expected = new double[] { -4, -3, 11 };
            double[] result = GaussMethod.Solve(matrix);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i], 0.0001);
            }
        }

        [DataTestMethod()]
        [DataRow(500, 2, 3, 4, 8)]
        [DataRow(1000, 2, 3, 4, 8)]
        [DataRow(1500, 2, 3, 4, 8)]
        [DataRow(2000, 2, 3, 4, 8)]
        public void DistributedValidationTest(int rows, params int[] numsOfThreads)
        {
            Random random = new Random();
            double[,] matrix = new double[rows, rows + 1];
            for(int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows + 1; j++)
                {
                    matrix[i,j] = 1000 * random.NextDouble();
                }
            }
            Stopwatch stopwatchLinear = Stopwatch.StartNew();
            double[] resultLinear = GaussMethod.Solve(matrix);
            stopwatchLinear.Stop();
            Console.WriteLine($"Linear: {stopwatchLinear.ElapsedMilliseconds} ms");
            
            foreach (int threadsCount in numsOfThreads)
            {
                Stopwatch stopwatchDistribute = Stopwatch.StartNew();
                GaussMethodDistributed distributed = new GaussMethodDistributed(matrix, threadsCount);
                double[] resultDistribute = distributed.Solve();
                stopwatchDistribute.Stop();
                Console.WriteLine($"Distributed ({threadsCount} threads): {stopwatchDistribute.ElapsedMilliseconds} ms");
                for (int i = 0; i < rows; i++)
                {
                    Assert.AreEqual(resultLinear[i], resultDistribute[i], 0.00001);
                }
            }
        }
    }
}