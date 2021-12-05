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
    public class TaskTcpClient
    {
        private bool _isConnected = false;
        private bool _isEnded = false;
        private TcpClient client;
        private NetworkStream stream;

        List<string> dataString;

        /// <summary>
        /// Port of server which connected client.
        /// </summary>
        public int ServerPort { get; }

        /// <summary>
        /// Ip of server which connected client.
        /// </summary>
        public string ServerIp { get; }

        /// <summary>
        /// Delegate for processing data receiving from server.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        protected delegate double[] Calculate(double[,] matrix);
        /// <summary>
        /// Event for processing data receiving from server.
        /// </summary>
        protected event Calculate _calculationMethod;

        /// <summary>
        /// Constructor of TaskTcpClient.
        /// </summary>
        /// <param name="serverPort">Port of server which connected client.</param>
        /// <param name="serverIp">Ip of server which connected client.</param>
        public TaskTcpClient(int serverPort, string serverIp)
        {
            ServerPort = serverPort;
            ServerIp = serverIp;
            _calculationMethod += matrix => GaussMethod.Solve(matrix);
            dataString = new List<string>();
        }

        /// <summary>
        /// Starting to connection to server and receiving and processing data.
        /// </summary>
        public void Start()
        {
            while (!_isEnded)
            {
                if (!_isConnected)
                {
                    while (!_isConnected)
                    {
                        try
                        {
                            client = new TcpClient(ServerIp, ServerPort);
                            stream = client.GetStream();
                            _isConnected = true;
                        }
                        catch (Exception e)
                        {

                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    _ReceivingData();
                    if (this.dataString.Count > 0)
                    {
                        _SendingData();
                    }
                }
            }
        }

        /// <summary>
        /// Receiving data from server.
        /// </summary>
        private void _ReceivingData()
        { 
            byte[] data = new byte[8192];
            StringBuilder response = new StringBuilder();
            while (stream.DataAvailable)
            {
                int bytes = stream.Read(data, 0, data.Length);
                response.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            dataString.Add(response.ToString());
        }

        /// <summary>
        /// Processing and sending data to server.
        /// </summary>
        private void _SendingData()
        {
            string currentData = dataString[0];
            double[,] matrix = DataParse.StringToMatrixDouble(currentData);
            this.dataString.Remove(currentData);
            byte[] data = Encoding.Unicode.GetBytes(DataParse.ArrayDoubleToString(_calculationMethod.Invoke(matrix)));
            stream.Write(data, 0, data.Length);
            _isEnded = true;
        }
    }
}
