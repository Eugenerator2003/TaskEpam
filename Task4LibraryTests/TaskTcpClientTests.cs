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
    public class TaskTcpListenerTests
    {
        [DataTestMethod()]
        [DataRow(5, 2)]
        [DataRow(6, 3)]
        public void SolveTest(int matrixRows, int numOfClients)
        {
            TaskTcpListener listener = new TaskTcpListener(8005, "127.0.0.1");
            Random random = new Random();
            int columns = matrixRows + 1;
            double[,] matrix = new double[matrixRows, columns];
            for (int i = 0; i < matrixRows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    matrix[i, j] = random.Next(1, 100);
                }
            }
            for(int i = 0; i < numOfClients; i++)
            {
                Task.Run(() => {
                    TaskTcpClient client = new TaskTcpClient(8005, "127.0.0.1", GaussMethod.Calculate);
                    client.Start();
                });
            }   
            listener.Start(matrix);
            while (!listener.IsCalculated) ;
            double[] expected = (new GaussMethodDistributed(matrix, 4)).Solve();
            double[] result = listener.GetResult();
            for(int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i], 0.0001);
            }
            
        }
    }
}