using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Littlenew_Text.utils
{
    /// <summary>
    /// 文件操作
    /// </summary>
    class FileUtils
    {
        public static string readText(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            return reader.ReadToEnd();
        }
    }
}
