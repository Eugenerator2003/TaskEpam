using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task4TcpIp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task4TcpIp.Tests
{
    [TestClass()]
    public class TaskTcpClientTests
    {
        [TestMethod()]
        public void StartTest()
        {
            TaskTcpClient client = new TaskTcpClient(8005, "127.0.0.1");
            TaskTcpListener listener = new TaskTcpListener(8005, "127.0.0.1");
            Random random = new Random(DateTime.Now.DayOfYear + DateTime.Now.Second);
            int rows = 20;
            int columns = rows + 1;
            double[,] matrix = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    matrix[i, j] = random.Next(1, 100);
                }
            }
            Task.Run(() => client.Start());
            Task.Run(() => listener.Start(matrix));

            while (!listener.IsCalculated) ;
            double[] expected = GaussMethod.Solve(matrix);
            double[] result = listener.GetResult();
            for(int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i], 0.0001);
            }
            
        }
    }
}