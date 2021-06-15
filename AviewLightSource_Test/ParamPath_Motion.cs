using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMN200_YH.Motion
{
    public static class ParamPath_Motion
    {
        //参数路径的配置文件，保存着Model下当前选择的目录
        static string readPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ParamPathSetting.ini");
        /// <summary>
        /// 文件夹
        /// </summary>
        public static string SelectedDirName { get; private set; } = "Default";
        static string FileName = "MotionParam.ini";
        /// <summary>
        /// 模板的根文件夹
        /// </summary>
        public static string ModelRoot => System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Model");

        /// <summary>
        /// 当前的文件夹
        /// </summary>
        public static string SelectedDirPath => System.IO.Path.Combine(ModelRoot, SelectedDirName);

        /// <summary>
        /// 当前保存的具体文件
        /// </summary>
        public static string CurFileFullPath => System.IO.Path.Combine(SelectedDirPath, FileName);
        static ParamPath_Motion()
        {

            CreateFilePath();
            

        }
        /// <summary>
        /// 文件夹为模板
        /// </summary>
        static void CreateFilePath()
        {


            string strDefaultMode = "defaultMode";
            if (System.IO.File.Exists(readPath))
            {
                string[] temp = System.IO.File.ReadLines(readPath).ToArray();
                if (temp.Length >= 0)
                {
                    SelectedDirName = temp[0];
                    if (System.IO.Directory.Exists(SelectedDirPath))
                    {
                        if (!System.IO.File.Exists(CurFileFullPath))
                        {
                            using (System.IO.File.Create(CurFileFullPath)) { };
                        }
                        return;
                    }



                }



            }
            SelectedDirName = strDefaultMode;
            if (!System.IO.Directory.Exists(SelectedDirPath))
            {
                System.IO.Directory.CreateDirectory(SelectedDirPath);

            }

            if (!System.IO.File.Exists(CurFileFullPath))
            {
                using (System.IO.File.Create(CurFileFullPath)) { };
            }
            SaveSelectedItem();
        }

        static void SaveSelectedItem()
        {
            System.IO.File.WriteAllText(readPath, SelectedDirName);
        }
        public static void ResetLoadFold(string str)
        {
            SelectedDirName = System.IO.Path.GetFileName(str);
            SaveSelectedItem();
        }


    }
}
