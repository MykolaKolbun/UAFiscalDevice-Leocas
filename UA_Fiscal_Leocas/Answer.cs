using System;

namespace UA_Fiscal_Leocas
{
    internal class Answer
    {
        public byte ans { get; set; }
        public UInt16 len { get; set; }
        public UInt16 sync { get; set; }
        public UInt16 status { get; set; }
        public UInt16 error { get; set; }
        public byte[] fiscDevData { get; set; }
        public byte CRC { get; set; }

        /// <summary>
        /// Обработка ответа от фискального
        /// </summary>
        /// <param name="syn">sync отправки</param>
        /// <param name="data">ответ от фискального</param>
        /// <returns></returns>
        public UInt32 GetAnswer(UInt16 syn, byte[] data)
        {
            error = 0;
            ans = 0;
            byte[] tempData = new byte[data.Length - 1];
            for (int k = 0; k < tempData.Length; k++)
                tempData[k] = data[k];
            CRC tempCRC = new CRC();
            CRC = data[data.Length - 1];
            if (CRC == (byte)tempCRC.ComputeCrc(ref tempData))
            {
                ans = data[0];
                len = BitConverter.ToUInt16(data, 1);
                sync = BitConverter.ToUInt16(data, 3);
                if (syn == sync)
                {
                    error = BitConverter.ToUInt16(data, 7);
                    status = BitConverter.ToUInt16(data, 5);
                    switch (ans)
                    {
                        case 0x00:
                            // check status
                            break;
                        case 0x01:
                            // check extended status
                            break;
                        case 0x02:
                            fiscDevData = getData(data, len);
                            break;
                        case 0x03:
                            // no data
                            break;
                        case 0x04:
                            break;
                    }
                }
                else
                {
                    error = 2;

                }
            }
            else
            {
                error = 1;
            }
            return error;
        }

        /// <summary>
        /// Получить данные из ответа
        /// </summary>
        /// <param name="data1">ответ от фискального</param>
        /// <param name="l">длина ответа</param>
        /// <returns></returns>
        private byte[] getData(byte[] data1, UInt16 l)
        {
            byte[] outData = new byte[l - 10];
            for (int i = 0; i < l - 10; i++)
            {
                outData[i] = data1[i + 9];
            }
            return outData;
        }
    }
}