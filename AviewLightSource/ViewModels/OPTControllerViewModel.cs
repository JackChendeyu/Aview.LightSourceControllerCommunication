//#define TEST
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MvvmFoundation.Wpf;
using AviewLightSource;

namespace AviewLightSource.ViewModels
{
    class OPTControllerViewModel : MvvmFoundation.Wpf.ObservableObject
    {
        //variable
        private OPTController _opt;
        private object _lockReportMsg = new object();

        //constant
        public const UInt32 MAX_CHANNEL_COUNT = 16;

        //event
        public Action DeviceHasOpened;

        #region Properties
        /// <summary>
        /// OPT光源控制器是否为IP连接方式
        /// </summary>
        public bool IsIPComunication
        {
            get => _opt.Model == OPT_COMMUNICATION_MODEL.IP;
            set
            {
                if(value)
                    _opt.Model = OPT_COMMUNICATION_MODEL.IP;
                RaisePropertyChanged(nameof(IsIPComunication));
            }
        }
        /// <summary>
        /// OPT光源控制器是否为SN连接方式
        /// </summary>
        public bool IsSNComunication
        {
            get => _opt.Model == OPT_COMMUNICATION_MODEL.SN;
            set
            {
                if(value)
                    _opt.Model = OPT_COMMUNICATION_MODEL.SN;
                RaisePropertyChanged(nameof(IsSNComunication));
            }
        }
        /// <summary>
        /// OPT光源控制器是否为COM连接方式
        /// </summary>
        public bool IsCOMComunication
        {
            get => _opt.Model == OPT_COMMUNICATION_MODEL.COM;
            set
            {
                if(value)
                    _opt.Model = OPT_COMMUNICATION_MODEL.COM;
                RaisePropertyChanged(nameof(IsCOMComunication));
            }
        }

        private List<string> _deviceList;
        /// <summary>
        /// 当前以太网下OPT光源控制器设备数量
        /// </summary>
        public List<string> DeviceList
        {
            get => _deviceList;
            set
            {
                _deviceList = value;
                RaisePropertyChanged(nameof(DeviceList));
            }
        }

        private string _info;
        /// <summary>
        /// 运行信息
        /// </summary>
        public string Info
        {
            get => _info;
            set
            {
                _info = value;
                RaisePropertyChanged(nameof(Info));
            }
        }

        /// <summary>
        /// 当前OPT光源控制器序列号
        /// </summary>
        public string ConnectionAddress
        {
            get => _opt?.ConnectionAddress;
            set
            {
                _opt.ConnectionAddress = value;
                RaisePropertyChanged(nameof(ConnectionAddress));
            }
        }

        #endregion //Properties
        /******************************************************************************/
        //Command

        //Search
        public RelayCommand CommandScan { get => new RelayCommand(Scan); }

        //Open
        public RelayCommand CommandOpen { get => new RelayCommand(Open, () => !_opt.IsConnected); }

        //Close
        public RelayCommand CommandClose { get => new RelayCommand(Close, () => _opt.IsConnected); }

        //Save
        public RelayCommand CommandSave { get => new RelayCommand(Save); }      
        /******************************************************************************/

        #region Constructors
        public OPTControllerViewModel()
        {
            DeviceList = new List<string>();
            _opt = new OPTController();
        }
        #endregion //Constructors


        #region Public Methods
        /// <summary>
        /// 保存当前配置
        /// </summary>
        public void Save()
        {
            _opt.Save();

        }
        /// <summary>
        /// 搜索以太网内OPT光源控制器设备
        /// </summary>
        public void Scan()
        {
            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();
            switch (_opt.Model)
            {
                case OPT_COMMUNICATION_MODEL.COM:
                    Aview.Public.Comunication.SerialPort.SerialPortHelper.GetCurrentPortNameCollection(sb);
                    break;

                case OPT_COMMUNICATION_MODEL.SN:
                case OPT_COMMUNICATION_MODEL.IP:
                    _opt.GetControllerListOnEthernet(sb);
                    break;
            }
            if (sb.Length > 0)
            {
                string[] deviceArray = sb?.ToString().Split(',');
                for (int i = 0; i < deviceArray?.Length; i++)
                {
                    list.Add(deviceArray[i]);
                }
                DeviceList = list;
            }           
        }
        /// <summary>
        /// 打开指定OPT光源控制器
        /// </summary>
        public void Open()
        {
#if TEST
            _opt.ChannelCount = 8;
            _opt.OPTChannelCollection = new ObservableCollection<OPTChannel>();
            for (int i = 0; i < _opt.ChannelCount; i++)
            {
                OPTChannel channel = new OPTChannel();
                channel.Name = $"CH{i + 1}";
                channel.Channel = i + 1;
                channel.OnOff = true;
                channel.Intensity = i * 10;
                _opt.OPTChannelCollection.Add(channel);
            }
            DeviceHasOpened?.Invoke();
#else
            _opt.Open();
            System.Diagnostics.Trace.Assert(_opt.IsConnected);
            if (_opt.IsConnected)
            {
                DeviceHasOpened?.Invoke();
                ReportMsg($"Address:{ConnectionAddress}");
            }         
#endif
        }

        /// <summary>
        /// 关闭指定OPT光源控制器
        /// </summary>
        public void Close()
        {
            _opt.Close();          
        }

        /// <summary>
        /// 设置当前OPT光源控制器对象
        /// </summary>
        /// <param name="opt">当前OPT光源控制器赋值对象</param>
        public void SetCurrentOPTObject(OPTController opt)
        {
            this._opt = opt;
            ReportMsg($"Address:{ConnectionAddress}");

        }

        /// <summary>
        /// 获取当前OPT光源控制器通道集合
        /// </summary>
        /// <returns>ObservableCollection<OPTChannel>集合，包含通道信息</returns>
        public ObservableCollection<OPTChannel> GetOPTChannelCollection()
        {
            return _opt.OPTChannelCollection;
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message">将要汇报的信息</param>
        public void ReportMsg(string message)
        {
            lock (_lockReportMsg)
            {
                Info = message;

            }
        }
        #endregion //Public Methods
    }
}
