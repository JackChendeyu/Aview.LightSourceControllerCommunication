using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviewLightSource
{
    public class OPTChannel : CSharp_OPTControllerAPI.OPTControllerAPI
    {
        public string Name { get; set; }
        public int Channel { get; set; }

        private bool _onOff;
        [Newtonsoft.Json.JsonIgnore]
        public bool OnOff
        {
            get => _onOff;
            set
            {
                int result;
                if (value)
                {
                    result = base.TurnOnChannel(this.Channel);
                    if (result == 0) _onOff = true;
                    else _onOff = false;
                }
                else
                {
                    result = base.TurnOffChannel(this.Channel);
                    if (result == 0) _onOff = false;
                    else _onOff = true;
                }
            }
        }
        private int _intensity;
        public int Intensity
        {
            get => _intensity;
            set
            {
                int result;
                result = base.SetIntensity(this.Channel, value);
                if (result == 0)
                {
                    _intensity = value;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
