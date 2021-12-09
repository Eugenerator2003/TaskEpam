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
        /// Event for processing data receiving from server.
        /// </summary>
        public event GaussMethod.CalculationMethod _calculationMethod;

        /// <summary>
        /// Constructor of TaskTcpClient.
        /// </summary>
        /// <param name="serverPort">Port of server which connected client.</param>
        /// <param name="serverIp">Ip of server which connected client.</param>
        /// <param name="method">Method of calculation</param>
        public TaskTcpClient(int serverPort, string serverIp, GaussMethod.CalculationMethod method)
        {
            ServerPort = serverPort;
            ServerIp = serverIp;
            _calculationMethod += method;
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
                }
                else
                {
                    _ReceivingData();
                    if (this.dataString.Count > 0)
                    {
                        _SendingData();
                    }
                }
                System.Threading.Thread.Sleep(500);
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
                System.Threading.Thread.Sleep(100);
            }
            if (response.Length > 0)
            {
                foreach(string values in response.ToString().Split(';'))
                {
                    dataString.Add(values);
                }
            }
        }

        /// <summary>
        /// Processing and sending data to server.
        /// </summary>
        private void _SendingData()
        {
            while (dataString.Count > 0)
            {
                if (dataString[0] == "END")
                {
                    _isEnded = true;
                    break;
                }
                string currentStringData = dataString[0];
                (double[], double[]) dataValues = DataParse.GettedStringToArrays(currentStringData);
                this.dataString.Remove(currentStringData);
                double[] result = _calculationMethod.Invoke(dataValues.Item1, dataValues.Item2);
                byte[] data = Encoding.Unicode.GetBytes(DataParse.ArrayDoubleToString(result) + ";");
                
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
            }
            //byte[] data = Encoding.Unicode.GetBytes(DataParse.ArrayDoubleToString(_calculationMethod.Invoke(matrix)));
            //stream.Write(data, 0, data.Length);
        }
    }
}
