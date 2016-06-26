using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Littlenew_Text.model
{
    /// <summary>
    /// 代码高亮的关键字
    /// </summary>
    class HightLightKeyword
    {
        /// <summary>
        /// 关键字名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 关键字颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 关键字字体大小
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 关键字字体
        /// </summary>
        public string FontFamily { get; set; }

        public HightLightKeyword() { }

        HightLightKeyword(string Name ,string Color ,int Size ,string FontFamily)
        {
            this.Name = Name;
            this.Color = Color;
            this.Size = Size;
            this.FontFamily = FontFamily;
        }
    }
}
