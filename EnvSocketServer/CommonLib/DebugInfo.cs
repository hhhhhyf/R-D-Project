using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonLib
{
    public class DebugInfo
    {
        public static void Write(string content)
        {
            //日志文件会存在windows服务程序目录下 （System.Environment.CurrentDirectory使用该方法无法获取到正确的目录？）
            string dir = AppDomain.CurrentDomain.BaseDirectory;//获取该服务所在的程序安装目录路径
            
            FileStream fs;
            StreamWriter sw;
            try
            {
                DateTime now = DateTime.Now;
                fs = new FileStream(dir + "//DebugInfo.txt", FileMode.Append);
                sw = new StreamWriter(fs);
                //开始写入
                sw.Write("\r\n**" + now.ToString() + "** " + content);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            { }
            finally
            {
            }

        }
    }
}