using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace CommonLib
{
    public class Config
    {
        //下面是获取应用程序所在目录的上一级目录,注意这里依旧无法使用System.Environment.CurrentDirectory（获取和设置当前目录(该进程从中启动的目录)的完全限定目录）来正确找到目录，使用此路径会指向C：/windows目录
        public static string dataPath = (new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)).Parent.FullName;//获取程序的基目录。
        public static string configFile =  dataPath + "\\data\\config.txt";
        public static ConfigJson config;

        public static ConfigJson ReadConfig()
        {
            //private string  ConnectionStringLW = "server=192.168.1.1,1430(端口号) ;database=DB;uid=sa;pwd=123456"    //数据库连接字符串
            //string dir = System.Environment.CurrentDirectory;
            //string sLine = "";
            string strConfig = "";
            //ArrayList LineList = new ArrayList();
            ConfigJson json = new ConfigJson();
            StreamReader objReader = null;
            if (System.IO.File.Exists(Config.configFile))
            {
                try
                {
                    objReader = new StreamReader(Config.configFile);
                    strConfig = objReader.ReadToEnd();
                    //while (sLine != null)
                    //{
                    //    sLine = objReader.ReadLine();
                    //    if (sLine != null && !sLine.Equals(""))
                    //        LineList.Add(sLine);
                    //}
                }
                catch (Exception ex)
                {
                    LogUtil.Log("读取数据库配置文件" + Config.configFile + "失败" + ex.Message);
                }
                finally
                {
                    objReader.Close();
                }
            }

            //if (LineList == null || LineList.Count == 0)
            //    return json;

            if (string.IsNullOrEmpty(strConfig))
                return json;

            //string json = Jsonstr("D:\\json\\jsonmy1.txt");//Jsonstr函数读取json数据的文本txt          　　　　　　 
            JavaScriptSerializer js = new JavaScriptSerializer();   //实例化一个能够序列化数据的类
            //这里直接用LineList[0] 是因为只有一行
            //json = js.Deserialize<ConfigJson>(LineList[0].ToString());    //将json数据转化为对象类型并赋值给list
            json = js.Deserialize<ConfigJson>(strConfig);
            config = json;
            return json;
        }
    }
}
