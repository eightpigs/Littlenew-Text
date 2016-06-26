using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Littlenew_Text.model
{
    /// <summary>
    /// 代码高亮的实体类
    /// </summary>
    class HightLight
    {
        /// <summary>
        /// 对应的语言
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Intro { get; set; }
        /// <summary>
        /// 高亮的关键字
        /// </summary>
        public List<HightLightKeyword> Keywords { get; set; }

        public HightLight() { }

        public HightLight(string Language, string Name, string Author, string Date, string Intro, List<HightLightKeyword> Keywords)
        {
            this.Language = Language;
            this.Name = Name;
            this.Author = Author;
            this.Date = Date;
            this.Intro = Intro;
            this.Keywords = Keywords;
        }
    }
}
