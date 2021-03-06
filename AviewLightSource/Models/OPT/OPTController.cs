using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

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
        SHORTCIRCUIT_PROTECTION = 2,
        /// <summary>
        /// 过压保护
        /// </summary>
        OVERVOLTAGE_PROTECTION = 3,
        /// <summary>
        /// 过流保护
        /// </summary>
        OVERCURRENT_PROTECTION = 4
    }
    public class OPTController : CSharp_OPTControllerAPI.OPTControllerAPI
    {
        /// <summary>
        /// 当前对象序列化保存路径
        /// </summary>
        [XmlIgnore]
        public string SavePath { get; set; }

        /// <summary>
        /// OPT光源控制器通道数
        /// </summary>
        [XmlIgnore]
        public int ChannelCount { get; set; }

        /// <summary>
        /// OPT光源控制器当前使用通道数
        /// </summary>
        [XmlIgnore]
        public int ChannelUsedCount { get; set; }

        /// <summary>
        /// OPT光源控制器连接地址，根据OPT光源控制器连接状态可为IP地址格式，SN序列号格式，COM串口号格式
        /// </summary>
        public string ConnectionAddress;

        /// <summary>
        /// OPT光源控制器连接状态
        /// </summary>
        [XmlIgnore]
        public bool IsConnected { get; set; } = false;

        /// <summary>
        /// OPT光源控制器连接方式
        /// </summary>
        public OPT_COMMUNICATION_MODEL Model { get; set; }

        /// <summary>
        /// OPT光源控制器通道数据集合
        /// </summary>     
        public ObservableCollection<OPTChannel> OPTChannelCollection { get; set; }


        public OPTController()
        {
            this.Model = OPT_COMMUNICATION_MODEL.SN; //默认通过SN码连接
            OPTChannelCollection = new ObservableCollection<OPTChannel>();
        }

        #region Public Methods

        /// <summary>
        /// 根据产品信息设置OPT光源控制器各通道亮度
        /// </summary>
        /// <param name="savePath"></param>
        /// <returns>
        /// 设置是否成功
        /// true:设置成功
        /// false:设置失败</returns>
        public bool SetIntensityByTypeName(string savePath)
        {
            OPTController opt = OPTControllerFactory.CreateInstance(savePath);
            if (opt == null)
            {
                return false;
            }
            SavePath = savePath;
            if (opt?.OPTChannelCollection == null || opt?.OPTChannelCollection.Count == 0)
                return false;

            OPTChannelCollection = opt.OPTChannelCollection;

            int ret = 0;
            try
            {
                for (int i = 0; i < ChannelCount; i++)
                {
                    ret = base.SetIntensity(opt.OPTChannelCollection[i].Channel, opt.OPTChannelCollection[i].Intensity);
                    if (ret != 0)
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ret == 0;
        }

        /// <summary>
        /// 获取当前OPT光源控制器各通道亮度
        /// </summary>
        public void ReadAllIntensity()      //Initial after this.Open function excuted
        {
            OPTChannelCollection.Clear();
            int result;
            foreach (var channelData in OPTChannelCollection)
            {
                result = base.TurnOnChannel(channelData.Channel);
                if (result == 0)
                {
                    channelData.OnOff = true;
                }
                int value = default;
                result = base.ReadIntensity(channelData.Channel, ref value);
                if (result == 0)
                {
                    channelData.Intensity = value;
                }
            }
        }
        /// <summary>
        /// 保存当前OPT光源控制器配置
        /// </summary>
        public void Save()
        {
            string objectStr = AviewLightSource.Serialize.XmlSeralizer.XMLSerialize(this);
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(SavePath))
            {
                sw.Write(objectStr);
            }
        }

        /// <summary>
        /// 打开当前OPT光源控制器所有通道
        /// </summary>
        /// <returns></returns>
        public bool TurnOnAllChannels()
        {
            return 0 == base.TurnOnChannel(0);
        }
        /// <summary>
        /// 关闭当前OPT光源控制器所有通道
        /// </summary>
        /// <returns></returns>
        public bool TurnOffAllChannels()
        {
            return 0 == base.TurnOffChannel(0);
        }
        /// <summary>
        /// 打开当前OPT光源控制器指定通道
        /// </summary>
        /// <param name="index">通道索引，从1开始</param>
        /// <returns></returns>
        public new bool TurnOnChannel(int index)
        {
            return 0 == base.TurnOnChannel(index);
        }
        /// <summary>
        /// 关闭当前OPT光源控制器指定通道
        /// </summary>
        /// <param name="index">通道索引，从1开始</param>
        /// <returns></returns>
        public new bool TurnOffChannel(int index)
        {
            return 0 == base.TurnOffChannel(index);
        }
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
            if (string.IsNullOrEmpty(this.ConnectionAddress))
            {
                throw new Exception("当前光源控制器连接地址为空，无法建立有效的连接");
            }
            switch (this.Model)
            {
                case OPT_COMMUNICATION_MODEL.COM:
                    ret = base.InitSerialPort(this.ConnectionAddress);
                    break;

                case OPT_COMMUNICATION_MODEL.IP:
                case OPT_COMMUNICATION_MODEL.SN:
                    ret = base.CreateEthernetConnectionBySN(this.ConnectionAddress);
                    break;
            }
            if (ret == 0)
            {
                IsConnected = true;
                int result;
                int count = default;
                result = GetControllerChannels(ref count);
                if (result == 0)
                {
                    ChannelCount = count;

                    for (int i = 0; i < ChannelCount; i++)
                    {
                        OPTChannel channelData = new OPTChannel()
                        {
                            Name = $"CH{i + 1}",
                            Channel = i + 1,
                        };
                        channelData.ChannelOnOffEvent += boolean =>
                        {
                            int returnRet;
                            if (boolean)
                            {
                                returnRet = base.TurnOnChannel(channelData.Channel);
                                if (returnRet == 0) return true;
                                else return false;
                            }
                            else
                            {
                                returnRet = base.TurnOffChannel(channelData.Channel);
                                if (returnRet == 0) return false;
                                else return true;
                            }
                        };
                        channelData.ChannelSetIntensityEvent += variable =>
                        {
                            int returnRet;
                            returnRet = base.SetIntensity(channelData.Channel, variable);
                            if (returnRet == 0)
                            {
                                return variable;
                            }
                            else
                            {
                                return 0;
                            }
                        };
                        OPTChannelCollection.Add(channelData);

                    }
                }

            }
        }

        #endregion Public Methods
    }
}
