using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4TcpIp
{
    /// <summary>
    /// Static class for parsing data from one- and two-dimensional arrays of double to string type and opposite.
    /// </summary>
    public static class DataParse
    {
        /// <summary>
        /// Converting from two-dimensional array of double to string.
        /// </summary>
        /// <param name="matrix">Two-dimensional array of double.</param>
        /// <returns>array converted to string.</returns>
        public static string MatrixDoubleToString(double[,] matrix)
        {
            int row = matrix.GetLength(0);
            StringBuilder info = new StringBuilder(row.ToString());
            info.Append(' ');
            foreach(double value in matrix)
            {
                info.Append(value.ToString());
                info.Append(' ');
            }
            info.Remove(info.Length - 1, 1);
            return info.ToString();

        }

        /// <summary>
        /// Converting from one-dimensional array to string.
        /// </summary>
        /// <param name="array">One-dimensiona array of double.</param>
        /// <returns>Array converted to string.</returns>
        public static string ArrayDoubleToString(double[] array)
        {
            StringBuilder info = new StringBuilder();
            foreach (double value in array)
            {
                info.Append(value.ToString());
                info.Append(' ');
            }
            info.Remove(info.Length - 1, 1);
            return info.ToString();
        }

        /// <summary>
        /// Converting from string to one-dimenstional array of double.
        /// </summary>
        /// <param name="line">String variable.</param>
        /// <returns>One-dimensional array.</returns>
        public static double[] StringToArrayDouble(string line)
        {
            List<double> values = new List<double>();
            foreach (string word in line.Split(' '))
            {
                if (Double.TryParse(word, out double value))
                {
                    values.Add(value);
                }
                else
                    throw new ArgumentException("Can't convert string to double array. Invalid data");
            }
            return values.ToArray();
        }
        
        public static (double[], double[]) GettedStringToArrays(string line)
        {
            List<double> values = new List<double>();
            foreach (string word in line.Split(' '))
            {
                if (Double.TryParse(word, out double value))
                {
                    values.Add(value);
                }
                else
                    if (word != "")
                        throw new ArgumentException("Can't convert string to double array. Invalid data");
            }
            int len = values.Count / 2;
            double[] headiingLine = new double[len];
            double[] currentLine = new double[len];
            for(int i = 0; i < len; i++)
            {
                headiingLine[i] = values[i];
                currentLine[i] = values[i + len];
            }
            return (headiingLine, currentLine);
        }

        /// <summary>
        /// Convering from string to two-dimensional array of double.
        /// </summary>
        /// <param name="line">String variable.</param>
        /// <returns>Two-dimensional array.</returns>
        public static double[,] StringToMatrixDouble(string line)
        {
            List<double> values = new List<double>();
            foreach (string word in line.Split(' '))
            {
                if (Double.TryParse(word, out double value))
                {
                    values.Add(value);
                }
                else
                    if (word != "")
                        throw new ArgumentException($"Can't convert string data to double type");
            }
            if (values.Count > 2)
            {
                int row = (int)values[0];
                double[,] result = new double[row, row + 1];
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < row + 1; j++)
                    {
                        result[i, j] = values[1 + i * (row + 1) + j];
                    }
                }
                return result;
            }
            else
                throw new ArgumentException("Can't parse from string to matrix of double");
        }
    }
}
