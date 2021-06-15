using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviewLightSource
{
    //public struct ChannelItem
    //{
    //    public int Index;
    //    public int Intensity;
    //    public OPT_CHANNEL_STATE State;
    //}
    /// <summary>
    /// OPT光源连接方式
    /// </summary>
    public enum OPT_COMMUNICATION_MODEL
    {
        /// <summary>
        /// COM串口连结
        /// </summary>
        COM = 0,
        /// <summary>
        /// 通过OPT设备SN码连接
        /// </summary>
        SN = 1,
        /// <summary>
        /// 通过OPT设备以太网IP地址连接
        /// </summary>
        IP = 2
    }

    /// <summary>
    /// OPT控制器通道状态
    /// </summary>
    public enum OPT_CHANNEL_STATE
    {
        /// <summary>
        /// 已连接光源
        /// </summary>
        LIGHTSOURCE_CONNECTED = 0,
        /// <summary>
        /// 未连接光源
        /// </summary>
        LIGHTSOURCE_DISCONNECTED = 1,
        /// <summary>
        /// 短路保护
        /// </summary>
        SHORT_CIRCUIT_PROTECTION = 2,
        /// <summary>
        /// 过压保护
        /// </summary>
        OVER_VOLTAGE_PROTECTION = 3,
        /// <summary>
        /// 过流保护
        /// </summary>
        OVER_CURRENT_PROTECTION = 4
    }
    public class OPTController : CSharp_OPTControllerAPI.OPTControllerAPI, IConnectDLL.IConnectBase
    {
        public string SavePath { get; set; }
        /// <summary>
        /// OPT光源控制器通道数
        /// </summary>
        public int ChannelCount { get; set; }

        /// <summary>
        /// OPT光源控制器当前使用通道数
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public int ChannelUsedCount { get; set; }
        /// <summary>
        /// OPT光源控制器IP地址
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// OPT光源控制器序列号
        /// </summary>
        public string SN { get; set; }

        //public string SerialPortName { get; set; }

        /// <summary>
        /// OPT光源控制器连接状态
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public bool IsConnected { get; set; } = false;

        /// <summary>
        /// OPT光源控制器连接方式
        /// </summary>
        public OPT_COMMUNICATION_MODEL Model { get; set; }
        /// <summary>
        /// OPT光源控制器通道亮度值集合
        /// </summary>
        public IntensityItem[] Channels { get; set; }

        #region Public Members

        public bool SetIntensityByTypeName()
        {
            if (Channels == null)
                return false;
            int ret = 0;
            for (int i = 0; i < ChannelCount; i++)
            {
                ret = base.SetIntensity(Channels[i].channel, Channels[i].intensity);
                if (ret != 0)
                    return false;
            }

            return ret == 0;
        }

        public void ReadAllIntensity()
        {
            Channels = new IntensityItem[ChannelCount];
            for (int i = 0; i < Channels.Length; i++)
            {
                Channels[i].channel = i + 1;
                base.ReadIntensity(Channels[i].channel, ref Channels[i].intensity);
            }
        }
        public void Save()
        {         
            string objectStr = Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(SavePath))
            {
                sw.Write(objectStr);
            }
        }
        #endregion Public Members

        #region IConnectDLL.IConnectBase Members
        /// <summary>
        /// 断开连接OPT光源控制器
        /// </summary>
        public void Close()
        {
            if (!IsConnected) return;
            int ret = default;
            switch (this.Model)
            {
                case OPT_COMMUNICATION_MODEL.COM:
                    ret = base.ReleaseSerialPort();
                    break;

                case OPT_COMMUNICATION_MODEL.IP:
                case OPT_COMMUNICATION_MODEL.SN:

                    ret = base.DestroyEthernetConnect();
                    break;
            }
            if (ret == 0)
            {
                IsConnected = false;
            }
        }
        /// <summary>
        /// 连接OPT光源控制器
        /// </summary>
        public void Open()
        {
            int ret = default;
            switch (this.Model)
            {
                case OPT_COMMUNICATION_MODEL.COM:
                    ret = -1;
                    break;

                case OPT_COMMUNICATION_MODEL.IP:
                    if (string.IsNullOrEmpty(this.IPAddress))
                        throw new Exception($"{nameof(this.IPAddress)} is null or empty");
                    ret = base.CreateEthernetConnectionByIP(this.IPAddress);
                    break;

                case OPT_COMMUNICATION_MODEL.SN:
                    if (string.IsNullOrEmpty(this.SN))
                        throw new Exception($"{nameof(this.SN)} is null or empty");
                    ret = base.CreateEthernetConnectionBySN(SN);
                    break;
            }
            if (ret == 0)
            {
                IsConnected = true;
                int count = default;
                ret = base.GetControllerChannels(ref count);
                if (ret == 0)
                {
                    ChannelCount = count;
                    base.TurnOffChannel(0);
                    System.Threading.Thread.Sleep(500);
                    base.TurnOnChannel(0);

                }
            }
        }
        #endregion IConnectDLL.IConnectBase Members
    }
}
