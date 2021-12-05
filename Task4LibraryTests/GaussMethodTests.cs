using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task4TcpIp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4TcpIp.Tests
{
    [TestClass()]
    public class GaussMethodTests
    {
        [TestMethod()]
        public void SolveTest1()
        {
            double[,] matrix = new double[,] { { 4, 1, 2, 3 }, { -2, 3, 1, 10 }, { -2, 4, 1, 7 } };
            double[] expected = new double[] { -4, -3, 11 };
            double[] result = GaussMethod.Solve(matrix);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i], 0.0001);
            }
        }

        [TestMethod()]
        public void SolveTest2()
        {
            double[,] matrix = new double[,] { { 1, 5, -2, 1, 3 }, { 0, 2, -3, 2, 5 }, { 3, 1, -1, 1, 6 }, { 4, -2, -1, 2, 11 } };
            double[] expected = new double[] { 1, 0, 1, 4 };
            double[] result = GaussMethod.Solve(matrix);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i], 0.0001);
            }
        }
    }
}