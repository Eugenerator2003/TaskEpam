using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4TcpIp
{
    /// <summary>
    /// Class for solve system of linear equations by Gausse method.
    /// </summary>
    public class GaussMethod
    {

        /// <summary>
        /// Solving the marix of system of linear equations by Gausse method.
        /// </summary>
        /// <param name="matrix">Matrix of values.</param>
        /// <returns>Array of solutions.</returns>
        public static double[] Solve(double[,] matrix)
        {
            int rows = matrix.GetLength(0); 
            double[,] matrixCopy = new double[rows, rows + 1];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < rows + 1; j++)
                    matrixCopy[i, j] = matrix[i, j];

            for (int k = 0; k < rows; k++)
            {
                for (int i = 0; i < rows + 1; i++) 
                    matrixCopy[k, i] = matrixCopy[k, i] / matrix[k, k];
                for (int i = k + 1; i < rows; i++)
                {
                    double K = matrixCopy[i, k] / matrixCopy[k, k];
                    for (int j = 0; j < rows + 1; j++)
                        matrixCopy[i, j] = matrixCopy[i, j] - matrixCopy[k, j] * K;
                }
                for (int i = 0; i < rows; i++) 
                    for (int j = 0; j < rows + 1; j++)
                        matrix[i, j] = matrixCopy[i, j];
            }

            for (int k = rows - 1; k > -1; k--)
            {
                for (int i = rows; i > -1; i--)
                    matrixCopy[k, i] = matrixCopy[k, i] / matrix[k, k];
                for (int i = k - 1; i > -1; i--) 
                {
                    double K = matrixCopy[i, k] / matrixCopy[k, k];
                    for (int j = rows; j > -1; j--) 
                        matrixCopy[i, j] = matrixCopy[i, j] - matrixCopy[k, j] * K;
                }
            }

            double[] solutions = new double[rows];
            for (int i = 0; i < rows; i++)
                solutions[i] = matrixCopy[i, rows];

            return solutions;
        }
    }
}
