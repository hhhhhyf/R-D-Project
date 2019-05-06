using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnvApplication
{
    class DaemonThread : Object
    {
        private Thread m_thread;
        private Socket m_socketServer;

        public DaemonThread(Socket socketServer)
        {
            m_socketServer = socketServer;
            m_thread = new Thread(DaemonThreadStart);
            m_thread.Start();
        }

        public void DaemonThreadStart()
        {
            if(m_thread.IsAlive)
                LogUtil.Log("start DaemonThread:Alive");
            else
                LogUtil.Log("start DaemonThread:not Alive:");
            while (m_thread.IsAlive)
            {                
                //日志自动改名
                //LogUtil.CheckLogFileName();
                LogUtil.Log("thread count:" + SocketServer.m_ThreadList.Count.ToString());
                if (SocketServer.m_ThreadList.Count >= 12) //这里暂时判断一下
                {                    
                    for (int i = SocketServer.m_ThreadList.Count/2; i >= 0; i--) //删除前面一半线程，可能后面要删除，用倒序
                    {
                        if (!m_thread.IsAlive)
                        {
                            LogUtil.Log("DaemonThread:not Alive");
                            break;
                        }
                        try
                        {
                            LogUtil.Log("Try to abort thread");
                            //if ((DateTime.Now - userTokenArray[i].ActiveDateTime).Milliseconds > m_asyncSocketServer.SocketTimeOutMS) //超时Socket断开
                            //{
                            //    lock (userTokenArray[i])
                            //    {
                            //        m_asyncSocketServer.CloseClientSocket(userTokenArray[i]);
                            //    }
                            //}

                            if (!SocketServer.m_ThreadList[i].IsAlive)
                            {
                                SocketServer.m_ThreadList[i].Abort();
                                LogUtil.Log("abort线程" + i + "成功！");
                                SocketServer.m_ThreadList[i].Join();
                                LogUtil.Log("join线程" + i + "成功！");
                                SocketServer.m_ThreadList.Remove(SocketServer.m_ThreadList[i]);
                                LogUtil.Log("删除线程"+i+"成功！");
                            }
                            else
                            {
                                LogUtil.Log("线程" + i + "Not Alive");
                            }
                        }
                        catch (Exception E)
                        {
                            LogUtil.Log("删除线程失败！" + E.Message);
                        }
                    }
                }

                for (int i = 0; i < 60 * 1000 / 10; i++) //每分钟检测一次
                {
                    if (!m_thread.IsAlive)
                        break;
                    Thread.Sleep(10);
                }
            }
        }

        public void Close()
        {
            m_thread.Abort();
            m_thread.Join();
        }
    }
}
