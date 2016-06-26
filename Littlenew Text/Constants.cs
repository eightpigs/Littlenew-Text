using LanguagePlugin;
using Littlenew_Text.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Littlenew_Text
{
    /// <summary>
    /// 常量信息
    /// </summary>
    class Constants
    {
        /// <summary>
        /// 语法高亮信息
        /// </summary>
        public static List<HightLight> HightLights = new List<HightLight>();

        /// <summary>
        /// 语法插件
        /// </summary>
        public static Dictionary<String, ArrayList> Plugins = new Dictionary<string, ArrayList>();
    }
}
