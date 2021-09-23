using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UA_Fiscal_Leocas
{
    class Network
    {
        #region Fields
        /// <summary>
        /// Делегат для события "Данные получены".
        /// </summary>
        /// <param name="data">Принятые данные (массив байтов)</param>
        /// <param name="len">Длина масива принятых данных</param>
        public delegate void Received(byte[] data);

        TcpClient tcpClient = new TcpClient();
        NetworkStream stream;
        //Socket sender;
        #endregion

        #region Methods
        public int Connect(string ip, int port)
        {
            int connected = 3;
            if (!tcpClient.Connected)
            {

                try
                {
                    tcpClient.Connect(ip, port);
                    tcpClient.ReceiveBufferSize = 1024;
                    connected = 0;
                }
                catch
                {
                    connected = 3;
                }
            }
            else
                connected = 0;
            return connected;
        }

        public void Send(byte[] data)
        {
            stream = tcpClient.GetStream();
            stream.Write(data, 0, data.Length);
            var buffer = new byte[3];
            var dataBuffer = new byte[1001];
            do
            {
                stream.Read(dataBuffer, 0, 1000);
            }
            while (stream.DataAvailable);
            short len = BitConverter.ToInt16(dataBuffer, 1);
            var outData = new byte[len];
            for (int d = 0; d < len; d++)
            {
                outData[d] = dataBuffer[d];
            }
            this.OnRecievedEvent(outData);
        }

        /// <summary>
        /// Закрыть соединение с TCP-сервером
        /// </summary>
        /// <returns></returns>
        public int Close()
        {
            try
            {
                LingerOption lingerOption = new LingerOption(true, 0);
                tcpClient.LingerState = lingerOption;
                tcpClient.GetStream().Close();
                tcpClient.Close();
                tcpClient = null;
            }
            catch
            { }
            return 0;
        }
        #endregion

        #region Events
        /// <summary>
        /// Событие "Данные получены".
        /// </summary>
        public event Received ReceivedEvent;

        /// <summary>
        /// Сонструктор события "Данные получены".
        /// </summary>
        /// <param name="data">Принятые данные (массив байтов)</param>
        /// <param name="len">Длина масива принятых данных</param>
        private void OnRecievedEvent(byte[] data)
        {
            if (this.ReceivedEvent != null)
                this.ReceivedEvent(data);
        }
        #endregion
    }
}
