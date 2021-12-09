using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace Task4TcpIp
{
    /// <summary>
    /// Class for solve system of linear equations by Gausse method.
    /// </summary>
    public class GaussMethod
    {
        /// <summary>
        /// Delegate of processing rows of the coefficients matrix.
        /// </summary>
        /// <param name="leadingLine">Leading row.</param>
        /// <param name="currentLine">Current row.</param>
        /// <returns></returns>
        public delegate double[] CalculationMethod(double[] leadingLine, double[] currentLine);

        /// <summary>
        /// Getting the solutions of the coefficients matrix by Gauss method.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static double[] Solve(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            double[][] _matrix = new double[rows][];
            for(int i = 0; i < rows; i++)
            {
                _matrix[i] = new double[rows + 1];
                for(int j = 0; j < rows + 1; j++)
                {
                    _matrix[i][j] = matrix[i, j];
                }
            }

            for (int k = 1; k < rows; k++)
            {
                for (int j = k; j < rows; j++)
                {
                    double c = _matrix[j][k - 1] / _matrix[k - 1][k - 1];
                    for(int i = 0; i < rows + 1; i++)
                    {
                        _matrix[j][i] -= c * _matrix[k - 1][i];
                    }
                }
            }

            double[] solutions = new double[rows];
            for (int i = rows - 1; i >= 0; i--)
            {
                solutions[i] = _matrix[i][rows] / _matrix[i][i];
                for (int c = rows - 1; c > i; c--)
                {
                    solutions[i] -= _matrix[i][c] * solutions[c] / _matrix[i][i];
                }
            }

            return solutions;
        }

        /// <summary>
        /// Processing rows of the coefficients matrix.
        /// </summary>
        /// <param name="leadingLine">Leading row.</param>
        /// <param name="currentLine">Current row.</param>
        /// <returns></returns>
        public static double[] Calculate(double[] leadingLine, double[] currentLine)
        {
            int index = 0;
            double m;
            try
            {
                while (index < leadingLine.Length && leadingLine[index] == 0) index++;
                m = currentLine[index] / leadingLine[index];
            }
            catch
            {
                throw new Exception(DataParse.ArrayDoubleToString(leadingLine) + ";" + DataParse.ArrayDoubleToString(currentLine));
            }
            for(int i = 0; i < leadingLine.Length; i++)
            {
                currentLine[i] -= m * leadingLine[i];
            }
            return currentLine;
        }
    }
}
