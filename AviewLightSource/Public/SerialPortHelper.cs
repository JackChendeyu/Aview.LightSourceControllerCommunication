using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Aview.Public.Comunication.SerialPort
{
    class SerialPortHelper
    {
        /// <summary>
        /// 获取当前设备的串口名称集合
        /// </summary>
        /// <param name="sb">串口名称集合赋值对象</param>
        public static void GetCurrentPortNameCollection(StringBuilder sb)
        {
            sb.Clear();
            string[] portArray = System.IO.Ports.SerialPort.GetPortNames();
            if (portArray.Length == 0) return;

            for (int i = 0; i < portArray.Length-1; i++)
            {
                sb.Append(portArray[i]);
                sb.Append(",");
            }
            sb.Append(portArray[portArray.Length - 1]);
        }
    }
}
