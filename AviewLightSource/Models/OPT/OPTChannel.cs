using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AviewLightSource
{
    public class OPTChannel 
    {
        /// <summary>
        /// 通道打开关闭事件
        /// </summary>
        [XmlIgnore]
        public Func<bool,bool> ChannelOnOffEvent;

        /// <summary>
        /// 通道设置亮度值事件
        /// </summary>
        [XmlIgnore]
        public Func<int, int> ChannelSetIntensityEvent;

        /// <summary>
        /// 通道名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 通道索引
        /// </summary>
        public int Channel { get; set; }

        private bool _onOff;
        [XmlIgnore]
        /// <summary>
        /// 通道状态
        /// </summary>     
        public bool OnOff
        {
            get => _onOff;
            set
            {
                if (ChannelOnOffEvent == null) _onOff = false;
                else _onOff = ChannelOnOffEvent(value);
            }
        }
        private int _intensity;
        /// <summary>
        /// 通道亮度值
        /// </summary>
        public int Intensity
        {
            get => _intensity;
            set
            {
                if (ChannelSetIntensityEvent == null) _intensity = value;
                else _intensity = ChannelSetIntensityEvent(value);
                
            }
        }
    }
}
