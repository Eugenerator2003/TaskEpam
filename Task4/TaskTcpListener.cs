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
        private double[,] _matrix;
        private double[] _result;
        private bool _isCalculatingNow;

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
                    _rows = matrix.GetLength(0);
                    _columns = matrix.GetLength(1);
                    _matrix = matrix;
                    if (_rows + 1 != _columns)
                    {
                        throw new ArgumentException("Matrix of value not suitable for matrix of values of system of linear equations");
                    }

                    while (server.Pending())
                    {
                        clients.Add(server.AcceptTcpClient());
                    }
                    commutationArray = new bool[clients.Count];
                    for (int i = 0; i < commutationArray.Length; i++)
                    {
                        commutationArray[i] = true;
                    }

                    if (clients.Count > 0)
                    {
                        _isSend = true;
                        _isCalculatingNow = true;
                    }
                }
                _SendingData();
                _ReceivingData();
            }
        }

        /// <summary>
        /// Sending data from connected clients.
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
            for(int i = 0; i < commutationArray.Length; i++)
            {
                if (commutationArray[i])
                {
                    NetworkStream stream = clients[i].GetStream();
                    byte[] data = Encoding.Unicode.GetBytes(DataParse.MatrixDoubleToString(_matrix));
                    stream.Write(data, 0, data.Length);
                    commutationArray[i] = false;
                }
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
                        _result = DataParse.StringToArrayDouble(dataString.ToString());
                        commutationArray[i] = true;
                        IsCalculated = true;
                        _isCalculatingNow = false;
                    }
                }
            }
        }

        /// <summary>
        /// Getting array of solutions of system.
        /// </summary>
        /// <returns>Array of solutions.</returns>
        public double[] GetResult() => _result;

    }
}
