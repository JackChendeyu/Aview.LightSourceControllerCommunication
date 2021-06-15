using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMN200_YH.Motion;
using AviewLightSource;

namespace WpfMN200_YH.Connect
{
    class ConnectFactory
    {        
        static ConnectFactory()
        {
            optLeft = OPTControllerFactory.CreateInstance(ParamPath_Motion.SelectedDirPath, "optLeft.json");
            //connectHelper.Add(SerialPort_ScanCode);
            //connectHelper.Add(SerialPort_ScanSocket);
            //connectHelper.Add(SerialPort_light);


            //WorkItemManagerHelper.workIterm.LoadedWorkItemEventHandler += () =>
            //{
            //    PowerData1.Read();
            //    PowerData2.Read();
            //    PowerData3.Read();
            //    PowerChannelFactory1.SetAllChannels();
            //    PowerChannelFactory2.SetAllChannels();
            //    PowerChannelFactory3.SetAllChannels();
            //};
        }
        static bool IsInitialed = false;

        public static OPTController optLeft ;

        /// <summary>
        /// 通讯对象管理器
        /// </summary>
        static readonly IConnectDLL.ConnectManagerHelper connectHelper = new IConnectDLL.ConnectManagerHelper();


        public static void Initial()
        {
            if (IsInitialed) return;
            IsInitialed = true;


            //=========================================
            connectHelper.Open();
            connectHelper.StartReLink();

        }

        public static void Close()
        {
            connectHelper.Close();
            
        }


      

    }
}
