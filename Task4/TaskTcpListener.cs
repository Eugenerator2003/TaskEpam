using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Task4TcpIp
{
    /// <summary>
    /// Class of Tcp-Ip client for solving system of linear equations.
    /// </summary>
    public class TaskTcpListener
    {
        TcpListener server;
        List<TcpClient> clients;
        bool[] commutationArray;
        private bool _isSend;
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
            _isSend = false;
            IPAddress ipAddr = IPAddress.Parse(Ip);
            server = new TcpListener(ipAddr, Port);
            clients = new List<TcpClient>();
            server.Start();
        }

        /// <summary>
        /// Renewing connection with clients.
        /// </summary>
        public void RenewConnection()
        {
            if (!_isCalculatingNow)
            {
                while (server.Pending())
                {
                    clients.Add(server.AcceptTcpClient());
                }
                commutationArray = new bool[clients.Count];
                for (int i = 0; i < commutationArray.Length; i++)
                {
                    commutationArray[i] = true;
                }
            }
            else
                throw new Exception("Server can't renew connections. It's calculating now");
        }

        /// <summary>
        /// Starting the solving the matrix of system of linear equations with connected clients.
        /// </summary>
        /// <param name="matrix"></param>
        public void Start(double[,] matrix)
        {
            
            IsCalculated = false;
            while (!IsCalculated)
            {
                if (!_isSend)
                {
                    while (server.Pending())
                    {
                        clients.Add(server.AcceptTcpClient());
                    }
                    if (clients.Count > 0)
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
                        _isSend = true;
                        _isCalculatingNow = true;
                        commutationArray = new bool[clients.Count];
                        for (int i = 0; i < commutationArray.Length; i++)
                        {
                            commutationArray[i] = true;
                        }
                        _rowsQueue = new Queue<int>[clients.Count];
                        for (int i = 0; i < commutationArray.Length; i++)
                        {
                            _rowsQueue[i] = new Queue<int>();
                        }
                        _currentLineIndex = 1;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                    }
                }
                else
                {
                    _SendingData();
                    System.Threading.Thread.Sleep(500);
                    _ReceivingData();
                }
            }
        }

        /// <summary>
        /// Sending data to connected clients.
        /// </summary>
        private void _SendingData()
        {
            foreach(bool flag in commutationArray)
            {
                if (!flag)
                {
                    return;
                }
            }
            int clientIndex = 0;
            for(int j = _currentLineIndex; j < _rows; j++)
            {
                StringBuilder info = new StringBuilder();
                info.Append(DataParse.ArrayDoubleToString(_matrix[j - 1]));
                info.Append(' ');
                info.Append(DataParse.ArrayDoubleToString(_matrix[j]));
                info.Append(';');
                NetworkStream stream = clients[clientIndex].GetStream();
                byte[] data = Encoding.Unicode.GetBytes(info.ToString());
                stream.Write(data, 0, data.Length);
                _rowsQueue[clientIndex].Enqueue(j);
                clientIndex++;
                if (clientIndex == clients.Count)
                    clientIndex = 0;
            }
            for(int i = 0; i < commutationArray.Length; i++)
            {
                commutationArray[i] = false;
            }
            _currentLineIndex++;
            if (_currentLineIndex == _matrix.Length)
            {
                for (int i = _rows - 1; i >= 0; i--)
                {
                    _solutions[i] = _matrix[i][_rows] / _matrix[i][i];
                    for (int c = _rows - 1; c > i; c--)
                    {
                        _solutions[i] -= _matrix[i][c] * _solutions[c] / _matrix[i][i];
                    }
                }
                _isCalculatingNow = false;
                IsCalculated = true;
                for(int i = 0; i < commutationArray.Length; i++)
                {
                    commutationArray[i] = true;
                    NetworkStream stream = clients[i].GetStream();
                    byte[] data = Encoding.Unicode.GetBytes("END");
                    stream.Write(data, 0, data.Length);
                }
                clients.Clear();
                commutationArray = null;
                _rowsQueue = null;
            }
        }

        /// <summary>
        /// Receiving data to connected clients.
        /// </summary>
        private void _ReceivingData()
        {
            for(int i = 0; i < commutationArray.Length; i++)
            {
                if (!commutationArray[i])
                {
                    NetworkStream stream = clients[i].GetStream();
                    if (stream.DataAvailable)
                    {
                        byte[] data = new byte[8192];
                        StringBuilder dataString = new StringBuilder();
                        do
                        {
                            int bytes = stream.Read(data, 0, data.Length);
                            dataString.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (stream.DataAvailable);
                        List<string> results = new List<string>(dataString.ToString().Split(';'));
                        foreach(string result in results)
                        {
                            _matrix[_rowsQueue[i].Dequeue()] = DataParse.StringToArrayDouble(result);
                        }
                        if (_rowsQueue[i].Count == 0)
                        {
                            commutationArray[i] = true;
                        }    
                    }
                }
            }
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
