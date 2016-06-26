using Littlenew_Text.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Littlenew_Text.utils
{
    /// <summary>
    /// 程序启动初始化类
    /// </summary>
    class Init
    {
        /// <summary>
        /// 加载语法高亮信息
        /// </summary>
        public static void LoadHightLight()
        {
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory() + "/Config/HightLight/");
            if (dir.Exists)
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    string text = FileUtils.readText(file.FullName);
                    HightLight hightlight = JsonConvert.DeserializeObject<HightLight>(text);
                    Constants.HightLights.Add(hightlight);
                }
            }
        }
    }
}
