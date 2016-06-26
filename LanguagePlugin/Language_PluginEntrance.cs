using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguagePlugin
{
    /// <summary>
    /// 语法高亮的插件入口
    /// </summary>
    public interface Language_PluginEntrance
    {
        /// <summary>
        /// 语法高亮器入口
        /// </summary>
        void Highlighter();
    }
}
