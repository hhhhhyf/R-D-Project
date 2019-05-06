using CommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnvApplication
{
    partial class EnvService : ServiceBase
    {
        Thread thread;
        public EnvService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            thread = new Thread(new ThreadStart(SocketServer.StartSocketServer));//启用另外一个线程来处理业务.否则 OnStart方法执行不完.服务无法进行停止启动操作.
            thread.Start();
            //SocketServer.StartSocketServer();
            LogUtil.Log("启动服务！");
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            thread.Abort();
            LogUtil.Log("停止服务！");
        }
    }
}
