#define TEST
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #region Properties
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

        public int Channel1Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel1Intensity));

            }
        }
        public int Channel2Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel2Intensity));
            }
        }
        public int Channel3Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel3Intensity));
            }
        }
        public int Channel4Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel4Intensity));
            }
        }
        public int Channel5Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel5Intensity));
            }
        }
        public int Channel6Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel6Intensity));
            }
        }
        public int Channel7Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel7Intensity));
            }
        }
        public int Channel8Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel8Intensity));
            }
        }
        public int Channel9Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {

                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel9Intensity));
            }
        }
        public int Channel10Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel10Intensity));
            }
        }
        public int Channel11Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel11Intensity));
            }
        }
        public int Channel12Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel12Intensity));
            }
        }
        public int Channel13Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel13Intensity));
            }
        }
        public int Channel14Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel14Intensity));
            }
        }
        public int Channel15Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel15Intensity));
            }
        }
        public int Channel16Intensity
        {
            get
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                return GetPropertyChannelIntensity(info);
            }
            set
            {
                var info = System.Reflection.MethodBase.GetCurrentMethod().Name;
                SetPropertyIntensity(info, value);
                RaisePropertyChanged(nameof(Channel16Intensity));
            }
        }

        private bool _channel1Enable;
        public bool Channel1Enable
        {
            get { return _opt == null ? false : _channel1Enable; }
            set { _channel1Enable = value; RaisePropertyChanged(nameof(Channel1Enable)); }
        }
        private bool _channel2Enable;
        public bool Channel2Enable
        {
            get { return _opt == null ? false : _channel2Enable; }
            set { _channel2Enable = value; RaisePropertyChanged(nameof(Channel2Enable)); }
        }
        private bool _channel3Enable;
        public bool Channel3Enable
        {
            get { return _opt == null ? false : _channel3Enable; }
            set { _channel3Enable = value; RaisePropertyChanged(nameof(Channel3Enable)); }
        }
        private bool _channel4Enable;
        public bool Channel4Enable
        {
            get { return _opt == null ? false : _channel4Enable; }
            set { _channel4Enable = value; RaisePropertyChanged(nameof(Channel4Enable)); }
        }
        private bool _channel5Enable;
        public bool Channel5Enable
        {
            get { return _opt == null ? false : _channel5Enable; }
            set { _channel5Enable = value; RaisePropertyChanged(nameof(Channel5Enable)); }
        }
        private bool _channel6Enable;
        public bool Channel6Enable
        {
            get { return _opt == null ? false : _channel6Enable; }
            set { _channel6Enable = value; RaisePropertyChanged(nameof(Channel6Enable)); }
        }
        private bool _channel7Enable;
        public bool Channel7Enable
        {
            get { return _opt == null ? false : _channel7Enable; }
            set { _channel7Enable = value; RaisePropertyChanged(nameof(Channel7Enable)); }
        }
        private bool _channel8Enable;
        public bool Channel8Enable
        {
            get { return _opt == null ? false : _channel8Enable; }
            set { _channel8Enable = value; RaisePropertyChanged(nameof(Channel8Enable)); }
        }
        private bool _channel9Enable;
        public bool Channel9Enable
        {
            get { return _opt == null ? false : _channel9Enable; }
            set { _channel9Enable = value; RaisePropertyChanged(nameof(Channel9Enable)); }
        }
        private bool _channel10Enable;
        public bool Channel10Enable
        {
            get { return _opt == null ? false : _channel10Enable; }
            set { _channel10Enable = value; RaisePropertyChanged(nameof(Channel10Enable)); }
        }
        private bool _channel11Enable;
        public bool Channel11Enable
        {
            get { return _opt == null ? false : _channel11Enable; }
            set { _channel11Enable = value; RaisePropertyChanged(nameof(Channel11Enable)); }
        }
        private bool _channel12Enable;
        public bool Channel12Enable
        {
            get { return _opt == null ? false : _channel12Enable; }
            set { _channel12Enable = value; RaisePropertyChanged(nameof(Channel12Enable)); }
        }
        private bool _channel13Enable;
        public bool Channel13Enable
        {
            get { return _opt == null ? false : _channel13Enable; }
            set { _channel13Enable = value; RaisePropertyChanged(nameof(Channel13Enable)); }
        }
        private bool _channel14Enable;
        public bool Channel14Enable
        {
            get { return _opt == null ? false : _channel14Enable; }
            set { _channel14Enable = value; RaisePropertyChanged(nameof(Channel14Enable)); }
        }
        private bool _channel15Enable;
        public bool Channel15Enable
        {
            get { return _opt == null ? false : _channel15Enable; }
            set { _channel15Enable = value; RaisePropertyChanged(nameof(Channel15Enable)); }
        }
        private bool _channel16Enable;
        public bool Channel16Enable
        {
            get { return _opt == null ? false : _channel16Enable; }
            set { _channel16Enable = value; RaisePropertyChanged(nameof(Channel16Enable)); }
        }




        private bool _channel1OnOff;
        public bool Channel1OnOff
        {
            get => _channel1OnOff;
            set
            {
                SetPropertyOnOff(1, value, ref _channel1OnOff);
                RaisePropertyChanged(nameof(Channel1OnOff));
            }
        }

        private bool _channel2OnOff;
        public bool Channel2OnOff
        {
            get => _channel2OnOff;
            set
            {
                SetPropertyOnOff(2, value, ref _channel2OnOff);
                RaisePropertyChanged(nameof(Channel2OnOff));
            }
        }
        private bool _channel3OnOff;
        public bool Channel3OnOff
        {
            get => _channel3OnOff;
            set
            {
                SetPropertyOnOff(3, value, ref _channel3OnOff);
                RaisePropertyChanged(nameof(Channel3OnOff));
            }
        }
        private bool _channel4OnOff;
        public bool Channel4OnOff
        {
            get => _channel4OnOff;
            set
            {
                SetPropertyOnOff(4, value, ref _channel4OnOff);
                RaisePropertyChanged(nameof(Channel4OnOff));
            }
        }
        private bool _channel5OnOff;
        public bool Channel5OnOff
        {
            get => _channel5OnOff;
            set
            {
                SetPropertyOnOff(5, value, ref _channel5OnOff);
                RaisePropertyChanged(nameof(Channel5OnOff));
            }
        }
        private bool _channel6OnOff;
        public bool Channel6OnOff
        {
            get => _channel6OnOff;
            set
            {
                SetPropertyOnOff(6, value, ref _channel6OnOff);
                RaisePropertyChanged(nameof(Channel6OnOff));
            }
        }
        private bool _channel7OnOff;
        public bool Channel7OnOff
        {
            get => _channel7OnOff;
            set
            {
                SetPropertyOnOff(7, value, ref _channel7OnOff);
                RaisePropertyChanged(nameof(Channel7OnOff));
            }
        }
        private bool _channel8OnOff;
        public bool Channel8OnOff
        {
            get => _channel8OnOff;
            set
            {
                SetPropertyOnOff(8, value, ref _channel8OnOff);
                RaisePropertyChanged(nameof(Channel8OnOff));
            }
        }
        private bool _channel9OnOff;
        public bool Channel9OnOff
        {
            get => _channel9OnOff;
            set
            {
                SetPropertyOnOff(9, value, ref _channel9OnOff);
                RaisePropertyChanged(nameof(Channel9OnOff));
            }
        }
        private bool _channel10OnOff;
        public bool Channel10OnOff
        {
            get => _channel10OnOff;
            set
            {
                SetPropertyOnOff(10, value, ref _channel10OnOff);
                RaisePropertyChanged(nameof(Channel10OnOff));
            }
        }
        private bool _channel11OnOff;
        public bool Channel11OnOff
        {
            get => _channel11OnOff;
            set
            {
                SetPropertyOnOff(11, value, ref _channel11OnOff);
                RaisePropertyChanged(nameof(Channel11OnOff));
            }
        }
        private bool _channel12OnOff;
        public bool Channel12OnOff
        {
            get => _channel12OnOff;
            set
            {
                SetPropertyOnOff(12, value, ref _channel12OnOff);
                RaisePropertyChanged(nameof(Channel12OnOff));
            }
        }
        private bool _channel13OnOff;
        public bool Channel13OnOff
        {
            get => _channel13OnOff;
            set
            {
                SetPropertyOnOff(13, value, ref _channel13OnOff);
                RaisePropertyChanged(nameof(Channel13OnOff));
            }
        }
        private bool _channel14OnOff;
        public bool Channel14OnOff
        {
            get => _channel14OnOff;
            set
            {
                SetPropertyOnOff(14, value, ref _channel14OnOff);
                RaisePropertyChanged(nameof(Channel14OnOff));
            }
        }
        private bool _channel15OnOff;
        public bool Channel15OnOff
        {
            get => _channel15OnOff;
            set
            {
                SetPropertyOnOff(15, value, ref _channel15OnOff);
                RaisePropertyChanged(nameof(Channel15OnOff));
            }
        }
        private bool _channel16OnOff;
        public bool Channel16OnOff
        {
            get => _channel16OnOff;
            set
            {
                SetPropertyOnOff(16, value, ref _channel16OnOff);
                RaisePropertyChanged(nameof(Channel16OnOff));
            }
        }
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
                if (_opt == null || _opt.Channels == null) return 0;
                int index = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(propertyName, @"[^0-9]+", ""));
                return _opt.Channels[index - 1].intensity;
            }
            
        }
        private void SetPropertyIntensity(string propertyName, int intensity)
        {
            lock (_lockSetPropertyIntensity)
            {
                int index = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(propertyName, @"[^0-9]+", ""));
#if TEST
                _opt.Channels[index - 1].intensity = intensity;
#else
            int result;
            result = _opt.SetIntensity(index, intensity);
            if (result == 0)
            {
                _opt.Channels[index - 1].intensity = intensity;
            }
            else
            {
                return;
            }
#endif
            }

        }
        private void SetPropertyOnOff(int index, bool channelOnOff, ref bool channelState)
        {
            lock (_lockSetPropertyOnOff)
            {
#if TEST
                if (channelOnOff)
                {
                    channelState = true;
                }
                else
                {
                    channelState = false;
                }
#else
            int result;
            if (channelOnOff)
            {
                result = _opt.TurnOnChannel(index);
                if (result == 0) channelState = true;
                else channelState = false;
            }
            else
            {
                result = _opt.TurnOffChannel(index);
                if (result == 0) channelState = false;
                else channelState = true;
            }
#endif
            }

        }
#endregion //Private Methods

#region Public Methods
        public void Save()
        {
            _opt.Save();

        }
        public void Scan()
        {
            StringBuilder sb = null;
            _opt.GetControllerListOnEthernet(sb);
            string[] deviceArray = sb?.ToString().Split(',');
            for (int i = 0; i < deviceArray?.Length; i++)
            {
                DeviceList.Add(deviceArray[i]);
            }
        }
        public void Open()
        {
#if TEST
            _opt.ChannelCount = 16;
            _opt.Channels = new CSharp_OPTControllerAPI.OPTControllerAPI.IntensityItem[_opt.ChannelCount];
#endif
            _opt.Open();
            System.Diagnostics.Trace.Assert(_opt.IsConnected);
            if (_opt.IsConnected)
            {               
                _opt.ReadAllIntensity();
            }

            for (int i = 0; i < _opt.ChannelCount; i++)
            {
                _opt.Channels[i].channel = i + 1;
                System.Reflection.PropertyInfo propertyIntensity = this.GetType().GetProperty($"Channel{i + 1}Intensity", typeof(int));
                propertyIntensity.SetValue(this, _opt.Channels == null ? 0 : _opt.Channels[i].intensity);
                System.Reflection.PropertyInfo propertyEnable = this.GetType().GetProperty($"Channel{i + 1}Enable", typeof(bool));
                propertyEnable.SetValue(this, i + 1 <= _opt.ChannelCount ? true : false);
            }
        }

        public void Close()
        {
            _opt.Close();
        }

        public void SetCurrentOPTObject(OPTController opt)
        {
            this._opt = opt;
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
