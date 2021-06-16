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
        private object _lockGetPropertyIntensity = new object();
        private object _lockSetPropertyIntensity = new object();
        private object _lockSetPropertyOnOff = new object();
        private object _lockReportMsg = new object();

        //constant
        public const UInt32 MAX_CHANNEL_COUNT = 16;

        //event
        public Action DeviceHasOpened;

        #region Properties

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
        public string Info
        {
            get => _info;
            set
            {
                _info = value;
                RaisePropertyChanged(nameof(Info));
            }
        }

        public string SN
        {
            get => _opt?.SN;
            set
            {
                _opt.SN = value;
                RaisePropertyChanged(nameof(SN));
            }
        }
        /******************************************************************************/
        #endregion //Properties

        public RelayCommand CommandScan { get => new RelayCommand(Scan); }
        public RelayCommand CommandOpen { get => new RelayCommand(Open, () => !_opt.IsConnected); }
        public RelayCommand CommandClose { get => new RelayCommand(Close, () => _opt.IsConnected); }
        public RelayCommand CommandSave { get => new RelayCommand(Save); }

        #region Constructors
        public OPTControllerViewModel()
        {
            DeviceList = new List<string>();
            _opt = new OPTController();


        }
        #endregion //Constructors

        #region Private Methods
        private int GetPropertyChannelIntensity(string propertyName)
        {
            lock (_lockGetPropertyIntensity)
            {
                //if (_opt == null || _opt.Channels == null) return 0;
                //int index = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(propertyName, @"[^0-9]+", ""));
                //if (index > _opt.ChannelCount) return 0;
                //return _opt.Channels[index - 1].intensity;
            }
            return 0;
        }
        private void SetPropertyIntensity(string propertyName, int intensity)
        {
//            lock (_lockSetPropertyIntensity)
//            {
//                int index = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(propertyName, @"[^0-9]+", ""));
//#if TEST
//                _opt.Channels[index - 1].intensity = intensity;
//#else
//            int result;
//            result = _opt.SetIntensity(index, intensity);
//            if (result == 0)
//            {
//                _opt.Channels[index - 1].intensity = intensity;
//            }
//            else
//            {
//                return;
//            }
//#endif
//            }

        }
        private void SetPropertyOnOff(int index, bool channelOnOff, ref bool channelState)
        {
//            lock (_lockSetPropertyOnOff)
//            {
//#if TEST
//                if (channelOnOff)
//                {
//                    channelState = true;
//                }
//                else
//                {
//                    channelState = false;
//                }
//#else
//            int result;
//            if (channelOnOff)
//            {
//                result = _opt.TurnOnChannel(index);
//                if (result == 0) channelState = true;
//                else channelState = false;
//            }
//            else
//            {
//                result = _opt.TurnOffChannel(index);
//                if (result == 0) channelState = false;
//                else channelState = true;
//            }
//#endif
//            }

        }
#endregion //Private Methods



#region Public Methods
        public void Save()
        {
            _opt.Save();

        }
        public void Scan()
        {
            List<string> list = new List<string>();

            StringBuilder sb = new StringBuilder();
            _opt.GetControllerListOnEthernet(sb);
            string[] deviceArray = sb?.ToString().Split(',');
            for (int i = 0; i < deviceArray?.Length; i++)
            {
                list.Add(deviceArray[i]);
            }
            DeviceList = list;
        }
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

            //System.Diagnostics.Trace.Assert(_opt.IsConnected);
            if (_opt.IsConnected)
            {
                int ret;
                int count = default;
                ret = _opt.GetControllerChannels(ref count);
                if (ret == 0)
                {
                    _opt.ChannelCount = count;
                    _opt.ReadAllIntensity();
                    DeviceHasOpened?.Invoke();
                }
            }
            //for (int i = 0; i < _opt.ChannelCount; i++)
            //{
            //    _opt.Channels[i].channel = i + 1;
            //    System.Reflection.PropertyInfo propertyIntensity = this.GetType().GetProperty($"Channel{i + 1}Intensity", typeof(int));
            //    propertyIntensity.SetValue(this, _opt.Channels == null ? 0 : _opt.Channels[i].intensity);
            //    System.Reflection.PropertyInfo propertyEnable = this.GetType().GetProperty($"Channel{i + 1}Enable", typeof(bool));
            //    propertyEnable.SetValue(this, i + 1 <= _opt.ChannelCount ? true : false);
            //    System.Reflection.PropertyInfo propertyOnOff = this.GetType().GetProperty($"Channel{i + 1}OnOff", typeof(bool));
            //    propertyOnOff.SetValue(this, true);
            //}
#endif

        }

        public void Close()
        {
            _opt.Close();
            //for (int i = 0; i < _opt.ChannelCount; i++)
            //{             
            //    System.Reflection.PropertyInfo propertyEnable = this.GetType().GetProperty($"Channel{i + 1}Enable", typeof(bool));
            //    propertyEnable.SetValue(this, false);

            //}
        }

        public void SetCurrentOPTObject(OPTController opt)
        {
            this._opt = opt;
            //for (int i = 0; i < _opt.ChannelCount; i++)
            //{
            //    _opt.Channels[i].channel = i + 1;
            //    System.Reflection.PropertyInfo propertyIntensity = this.GetType().GetProperty($"Channel{i + 1}Intensity", typeof(int));
            //    propertyIntensity.SetValue(this, _opt.Channels == null ? 0 : _opt.Channels[i].intensity);
            //    System.Reflection.PropertyInfo propertyEnable = this.GetType().GetProperty($"Channel{i + 1}Enable", typeof(bool));
            //    propertyEnable.SetValue(this, i + 1 <= _opt.ChannelCount ? true : false);
            //    System.Reflection.PropertyInfo propertyOnOff = this.GetType().GetProperty($"Channel{i + 1}OnOff", typeof(bool));
            //    propertyOnOff.SetValue(this, true);
            //}
        }
        public ObservableCollection<OPTChannel> GetOPTChannelCollection()
        {
            return _opt.OPTChannelCollection;
        }

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
