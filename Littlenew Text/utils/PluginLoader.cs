using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Littlenew_Text.utils
{
    class PluginLoader
    {
        /// <summary>
        /// 载入所有插件
        /// </summary>
        public static void loadPlugins(string path , string interfaceName)
        {
            //获取插件目录(plugins)下所有文件
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (file.ToUpper().EndsWith(".DLL"))
                {
                    try
                    {
                        //载入dll
                        Assembly ab = Assembly.LoadFrom(file);
                        Type[] types = ab.GetTypes();
                        foreach (Type t in types)
                        {
                            //如果某些类实现了预定义的接口，则认为该类适配与主程序(是主程序的插件)
                            if (t.GetInterface(interfaceName) != null)
                            {
                                // 如果没有加载过该插件信息, 先实例化插件容器
                                if(!Constants.Plugins.ContainsKey(interfaceName))
                                {
                                    Constants.Plugins[interfaceName] = new ArrayList();
                                }
                                // 将插件添加到对应的插件容器内
                                Constants.Plugins[interfaceName].Add(ab.CreateInstance(t.FullName));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("加载插件错误.");
                    }
                }
            }
        }


        public static void ExecPluginsMethod(string methodName, Object obj)
        {
            Type t = obj.GetType();

            MethodInfo method = t.GetMethod(methodName);

            method.Invoke(obj, null);
        }
    }
}
