using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AviewLightSource
{
    public class OPTControllerFactory
    {
        static OPTControllerFactory()
        {

        }
        public static OPTController CreateInstance(string filePath, string fileName)
        {
            string fullPath = System.IO.Path.Combine(filePath, fileName);
            return CreateInstance(fullPath);
        }

        public static OPTController CreateInstance(string fullPath)
        {          
            OPTController opt;
            try
            {
                string str;
                using (System.IO.FileStream fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(fs))
                    {
                        str = sr.ReadToEnd();
                    }
                }
                opt = AviewLightSource.Serialize.XmlSeralizer.DeXMLSerialize<OPTController>(str);
            }
            catch (Exception ex)
            {
                return new OPTController() { SavePath = fullPath };
            }
            opt.SavePath = fullPath;
            return opt;
        }
    }
}
