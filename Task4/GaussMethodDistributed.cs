using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Task4TcpIp
{
    /// <summary>
    /// Class of distributed Gauss method solve.
    /// </summary>
    public class GaussMethodDistributed : GaussMethod
    {
        private double[][] _matrix;
        private double[] _solutions;
        private int _matrixRows;
        private int _numOfThreads;
        private int _currentLeadingLineIndex;
        private Queue<int>[] threadsQueue;

        /// <summary>
        /// Getting the solutions of the coefficients matrix by distributed Gauss method.
        /// </summary>
        /// <returns></returns>
        public double[] Solve()
        {
            if (_solutions != null)
                return _solutions;
            double[] solutions = new double[_matrixRows];
            for(int k = 1; k < _matrixRows; k++)
            {
                _currentLeadingLineIndex = k - 1;
                for(int j = k; j < _matrixRows; j++)
                {
                    for(int t = 0; t < _numOfThreads && j < _matrixRows; t++)
                    {
                        threadsQueue[t].Enqueue(j);
                        if(t + 1 != _numOfThreads)
                        {
                            j++;
                        }
                    }
                }
                _Calculate();
            }
            for (int i = _matrixRows - 1; i >= 0; i--)
            {
                solutions[i] = _matrix[i][_matrixRows] / _matrix[i][i];
                for (int c = _matrixRows - 1; c > i; c--)
                {
                    solutions[i] -= _matrix[i][c] * solutions[c] / _matrix[i][i];
                }
            }
            _solutions = solutions;
            return solutions;
        }

        /// <summary>
        /// Starting parallel calculation tasks.
        /// </summary>
        private void _Calculate()
        {
            while (threadsQueue.Any(elem => elem.Count > 0))
            {
                int start = threadsQueue[0].Dequeue();
                int end = start;
                for (int t = 1; t < _numOfThreads && threadsQueue.Any(elem => elem.Count > 0); t++)
                {
                    threadsQueue[t].Dequeue();
                    end++;
                };
                ParallelLoopResult result = Parallel.For(start, end + 1, (j) =>
                    {
                        double c = _matrix[j][_currentLeadingLineIndex] / _matrix[_currentLeadingLineIndex][_currentLeadingLineIndex];
                        for (int i = 0; i < _matrixRows + 1; i++)
                        {
                            _matrix[j][i] = _matrix[j][i] - c * _matrix[_currentLeadingLineIndex][i];
                        }
                    });
                while (!result.IsCompleted) ;
            }
        }

        /// <summary>
        /// Constructor of GauseMethodDistributed.
        /// </summary>
        /// <param name="matrix">The coefficients matrix.</param>
        /// <param name="numOfThreads">Number of calculation threads.</param>
        public GaussMethodDistributed(double[,] matrix, int numOfThreads)
        {
            _matrixRows = matrix.GetLength(0);
            _matrix = new double[_matrixRows][];
            for (int i = 0; i < _matrixRows; i++)
            {
                _matrix[i] = new double[_matrixRows + 1];

                for (int j = 0; j < _matrixRows + 1; j++)
                {
                    _matrix[i][j] = matrix[i, j];
                }
            }
            _numOfThreads = numOfThreads;
            threadsQueue = new Queue<int>[_numOfThreads];
            for(int i = 0; i < _numOfThreads; i++)
            {
                threadsQueue[i] = new Queue<int>();
            }
        }
    }
}
