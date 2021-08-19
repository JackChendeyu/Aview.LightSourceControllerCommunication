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
            optLeft = OPTControllerFactory.CreateInstance(ParamPath_Motion.SelectedDirPath, "optLeft.txt");

        }
        static bool IsInitialed = false;

        public static OPTController optLeft ;


      

    }
}
