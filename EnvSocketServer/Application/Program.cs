using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace EnvApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Config.ReadConfig();
            //if (Config.config == null)
            //    return;

            //带参启动运行服务
            if (args.Length > 0) 
            {
                try
                {
                    ServiceBase[] serviceToRun = new ServiceBase[] { new EnvService() };
                    ServiceBase.Run(serviceToRun);
                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(@"D:\Log.txt", "\nService Start Error：" + DateTime.Now.ToString() + "\n" + ex.Message);
                }
            }
            //不带参启动配置程序
            else
            {
            StartLable:
                Console.WriteLine("请选择你要执行的操作——1：自动部署服务，2：安装服务，3：卸载服务，4：验证服务状态，5：退出");
                Console.WriteLine("————————————————————");
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.NumPad1 || key == ConsoleKey.D1)
                {
                    if (ServiceHelper.IsServiceExisted(Config.config.serviceName))
                    {
                        ServiceHelper.ConfigService(Config.config.serviceName, false);
                    }
                    if (!ServiceHelper.IsServiceExisted(Config.config.serviceName))
                    {
                        ServiceHelper.ConfigService(Config.config.serviceName, true);
                    }
                    ServiceHelper.StartService(Config.config.serviceName);
                    goto StartLable;
                }
                else if (key == ConsoleKey.NumPad2 || key == ConsoleKey.D2)
                {
                    if (!ServiceHelper.IsServiceExisted(Config.config.serviceName))
                    {
                        ServiceHelper.ConfigService(Config.config.serviceName, true);
                    }
                    else
                    {
                        Console.WriteLine("\n服务已存在......");
                    }
                    goto StartLable;
                }
                else if (key == ConsoleKey.NumPad3 || key == ConsoleKey.D3)
                {
                    if (ServiceHelper.IsServiceExisted(Config.config.serviceName))
                    {
                        ServiceHelper.ConfigService(Config.config.serviceName, false);
                    }
                    else
                    {
                        Console.WriteLine("\n服务不存在......");
                    }
                    goto StartLable;
                }
                else if (key == ConsoleKey.NumPad4 || key == ConsoleKey.D4)
                {
                    if (!ServiceHelper.IsServiceExisted(Config.config.serviceName))
                    {
                        Console.WriteLine("\n服务不存在......");
                    }
                    else
                    {
                        Console.WriteLine("\n服务状态：" + ServiceHelper.GetServiceStatus(Config.config.serviceName).ToString());
                    }
                    goto StartLable;
                }
                else if (key == ConsoleKey.NumPad5 || key == ConsoleKey.D5) { }
                else
                {
                    Console.WriteLine("\n请输入一个有效键！");
                    Console.WriteLine("————————————————————");
                    goto StartLable;
                }
            }
        }
    }
}
