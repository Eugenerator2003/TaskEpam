using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Task4TcpIp
{
    /// <summary>
    /// Class of Tcp-Ip client for solving system of linear equations.
    /// </summary>
    public class TaskTcpListener
    {
        int _ttl = 10;
        TcpListener listener;
        List<TcpClient> clients;
        bool[] commutationArray;
        private int _rows;
        private int _columns;
        private double[][] _matrix;
        private double[] _solutions;
        private bool _isCalculatingNow;
        private Queue<int>[] _rowsQueue;
        private int _currentLineIndex;

        /// <summary>
        /// Property of status of calculation the solutions. 
        /// </summary>
        public bool IsCalculated { get; private set; }

        /// <summary>
        /// Port of server.
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Ip of server.
        /// </summary>
        public string Ip { get; }

        /// <summary>
        /// Constructor of TaskTcpListener.
        /// </summary>
        /// <param name="port">Port of server.</param>
        /// <param name="ip">Ip of server.</param>
        public TaskTcpListener(int port, string ip)
        {
            Port = port;
            Ip = ip;
            IsCalculated = false;
            _isCalculatingNow = false;
            IPAddress ipAddr = IPAddress.Parse(Ip);
            listener = new TcpListener(ipAddr, Port);
            clients = new List<TcpClient>();
            listener.Start();
        }

        /// <summary>
        /// Starting the solving the matrix of system of linear equations with connected clients.
        /// </summary>
        /// <param name="matrix"></param>
        public void Start(double[,] matrix)
        {
            _SetMatrix(matrix);
            _Connect();
            _Calculate();            
        }

        /// <summary>
        /// Setting the given matrix.
        /// </summary>
        /// <param name="matrix">The given matrix.</param>
        private void _SetMatrix(double[,] matrix)
        {
            _rows = matrix.GetLength(0);
            _columns = matrix.GetLength(1);
            if (_rows + 1 != _columns)
            {
                throw new ArgumentException("Matrix of value not suitable for matrix of values of system of linear equations");
            }
            _matrix = new double[_rows][];
            _solutions = new double[_rows];
            for (int i = 0; i < _rows; i++)
            {
                _matrix[i] = new double[_columns];
                for (int j = 0; j < _columns; j++)
                {
                    _matrix[i][j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// Connection with clients.
        /// </summary>
        private void _Connect()
        {
            int failCount = 0;
            while(failCount < _ttl)
            {
                while(listener.Pending())
                {
                    clients.Add(listener.AcceptTcpClient());
                }
                if (clients.Count > 0)
                {
                    break;
                }
                else
                {
                    failCount++;
                    Thread.Sleep(125);
                }
            }
            if (failCount == _ttl)
                throw new Exception("Can't start solving. There no tcp client");
            commutationArray = new bool[clients.Count];
            _rowsQueue = new Queue<int>[clients.Count];
            for(int i = 0; i < commutationArray.Length; i++)
            {
                commutationArray[i] = true;
                _rowsQueue[i] = new Queue<int>();
            }
        }

        /// <summary>
        /// Calculation the solutions of matrix.
        /// </summary>
        private void _Calculate()
        {
            while (_currentLineIndex < _rows)
            {
                _SendData();
                _ReceiveData();
            }
            _SayEnd();
            for (int i = _rows - 1; i >= 0; i--)
            {
                _solutions[i] = _matrix[i][_rows] / _matrix[i][i];
                for (int c = _rows - 1; c > i; c--)
                {
                    _solutions[i] -= _matrix[i][c] * _solutions[c] / _matrix[i][i];
                }
            }
            IsCalculated = true;
        }

        /// <summary>
        /// Sending data to clients.
        /// </summary>
        private void _SendData()
        {
            if (commutationArray.All(elem => elem == true))
            {
                int countClients = 0;
                for(int i = _currentLineIndex + 1; i < _rows; i++)
                {
                    StringBuilder dataString = new StringBuilder(DataParse.ArrayDoubleToString(_matrix[_currentLineIndex]));
                    dataString.Append(";");
                    dataString.Append(DataParse.ArrayDoubleToString(_matrix[i]));
                    dataString.Append("|");
                    byte[] dataByte = Encoding.Unicode.GetBytes(dataString.ToString());
                    NetworkStream stream = clients[countClients].GetStream();
                    stream.Write(dataByte, 0, dataByte.Length);
                    _rowsQueue[countClients].Enqueue(i);
                    commutationArray[countClients] = false;
                    countClients++;
                    if (countClients == clients.Count)
                    {
                        countClients = 0;
                    }
                }
                _currentLineIndex++;
            }
        }

        /// <summary>
        /// Receiving data from clients.
        /// </summary>
        private void _ReceiveData()
        {
            while (commutationArray.Any(elem => elem == false))
            {
                for (int i = 0; i < commutationArray.Length; i++)
                {
                    if (!commutationArray[i] && clients[i].GetStream().DataAvailable)
                    {
                        byte[] dataByte = new byte[8192];
                        StringBuilder dataString = new StringBuilder();
                        NetworkStream stream = clients[i].GetStream();
                        while (stream.DataAvailable)
                        {
                            stream.Read(dataByte, 0, dataByte.Length);
                            dataString.Append(Encoding.Unicode.GetString(dataByte, 0, dataByte.Length));
                        }
                        dataString.Replace("\0", "");
                        string[] arraysString = dataString.ToString().Split('|');
                        foreach(string arrayString in arraysString)
                        {
                            if (arrayString.Length > 0)
                            {
                                _matrix[_rowsQueue[i].Dequeue()] = DataParse.StringToArrayDouble(arrayString);
                            }
                        }
                        commutationArray[i] = true;
                    }
                }
                Thread.Sleep(600);
            }
        }

        /// <summary>
        /// Notify clients to end the work.
        /// </summary>
        private void _SayEnd()
        {
            foreach(TcpClient client in clients)
            {
                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.Unicode.GetBytes("END");
                stream.Write(data, 0, data.Length);
            }
            listener.Stop();
        }

        /// <summary>
        /// Getting array of solutions of system.
        /// </summary>
        /// <returns>Array of solutions.</returns>
        public double[] GetResult()
        {
            if (!IsCalculated)
            {
                throw new Exception("Can't get array of solution. It's calculating now");
            }
            return _solutions;
        }

    }
}
