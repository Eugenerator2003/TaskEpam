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
        private int _ttl = 30;
        private bool _isConnected = false;
        private bool _isEnded = false;
        private TcpClient client;
        private NetworkStream stream;

        List<string> data;

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
            data = new List<string>();
        }

        /// <summary>
        /// Starting to connection to server and receiving and processing data.
        /// </summary>
        public void Start()
        {
            _Connect();
            _Calculate();
        }

        /// <summary>
        /// Connection with the listener.
        /// </summary>
        private void _Connect()
        {
            int failCount = 0;
            while(failCount < _ttl)
            {
                try
                {
                    client = new TcpClient(ServerIp, ServerPort);
                    stream = client.GetStream();
                    _isEnded = false;
                    _isConnected = true;
                }
                catch
                {
                    failCount++;
                    if (failCount == _ttl)
                        throw new Exception("Can't connect to listener");
                }
            }
        }

        /// <summary>
        /// Processing the data from listener.
        /// </summary>
        private void _Calculate()
        {
            while(!_isEnded)
            {
                _ReceiveData();
                _SendData();
            }
        }

        /// <summary>
        /// Receiving data from listener.
        /// </summary>
        private void _ReceiveData()
        {
            StringBuilder dataString = new StringBuilder();
            byte[] dataByte = new byte[8192];
            while(stream.DataAvailable)
            {
                stream.Read(dataByte, 0, dataByte.Length);
                dataString.Append(Encoding.Unicode.GetString(dataByte, 0, dataByte.Length));
            }
            dataString.Replace("\0", "");
            if (dataString.Length > 0)
            {
                if (dataString.ToString() != "END")
                {
                    foreach (string line in dataString.ToString().Split('|'))
                    {
                        if (line.Contains(';'))
                        {
                            data.Add(line);
                        }
                    }
                }
                else
                {
                    _isEnded = true;
                    stream.Dispose();
                    client.Close();
                }
            }
        }

        /// <summary>
        /// Processing receiving data and sending result to listener.
        /// </summary>
        private void _SendData()
        {
            if (!_isEnded && data.Count > 0)
            {
                foreach(string dataString in data)
                {
                    string[] arraysString = dataString.Split(';');
                    double[] leadingArray = DataParse.StringToArrayDouble(arraysString[0]);
                    double[] currentLine = DataParse.StringToArrayDouble(arraysString[1]);
                    double[] result = _calculationMethod.Invoke(leadingArray, currentLine);
                    StringBuilder dataStringBuilder = new StringBuilder(DataParse.ArrayDoubleToString(result));
                    dataStringBuilder.Append('|');
                    byte[] dataByte = Encoding.Unicode.GetBytes(dataStringBuilder.ToString());
                    stream.Write(dataByte, 0, dataByte.Length);
                }
                data.Clear();
            }
        }
    }
}
