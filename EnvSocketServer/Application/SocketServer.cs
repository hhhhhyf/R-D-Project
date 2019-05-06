using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using CommonLib;
using System.Text.RegularExpressions;

namespace EnvApplication
{
    class SocketServer
    {
        static int debug = 1;
        static string errorcode = "0";
        static Socket ReceiveSocket;
        static int MAX_CONN_COUT = 10;//设定最多有10个排队连接请求   
        public static List<Thread> m_ThreadList = new List<Thread>();
        static int MAX_RECEIVE_LENGTH = 18; //每次接收最大长度，这里对于温度和湿度因为是16进制，只接收14个字节
               

        public static void StartSocketServer()
        {
            //System init
            Envionment_Data.Init();
            //Gyxp_Stat.Init();

            //string testmsg1 = "??l?Y*N";
            ////string testmsg = "??l?Y*O`Y}S?OUOU?:`h";
            //string testmsg2 = "嶏l齓*N";
            //string tttt = "\u5d8f\u006c\u9f53\u002a\u004e";
            ////Encoding.GetEncoding("GB18030").GetString("嶏l齓*N");
            ////TransformCode tc = new TransformCode();
            //string rr = Util.Decode(testmsg1);
            //string rr2 = Util.Decode(testmsg2);
            //string rr3 = "\u8def\u6cfd\u592a";// tc.TransText(TransformCode.TransType.UnicodeToCN, "\u8def\u6cfd\u592a");
            IPAddress ip;
            if (string.IsNullOrEmpty(Config.config.socketIps))
                ip = IPAddress.Any;  // 侦听所有网络客户接口的客活动
            else
                IPAddress.TryParse(Config.config.socketIps,out ip);

            int port = 6118;
            if (!string.IsNullOrEmpty(Config.config.socketPort))
                int.TryParse(Config.config.socketPort, out port);
                                 

            int.TryParse(Config.config.debugLevel,out debug);
            ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//使用指定的地址簇协议、套接字类型和通信协议        ReceiveSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReuseAddress,true);  //有关套接字设置
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            ReceiveSocket.Bind(new IPEndPoint(ip, port)); //绑定IP地址和端口号
            ReceiveSocket.Listen(MAX_CONN_COUT);

            //DaemonThread dthread = new DaemonThread(ReceiveSocket);
            while (true)
            {
                Thread.Sleep(500);

                Socket socket = ReceiveSocket.Accept(); //阻塞
                LogUtil.Log("建立连接");
                if(debug > 1)
                {
                    //Console.WriteLine("建立连接,传送数据中，请不要关闭窗口");
                    DebugInfo.Write("建立连接!");                   
                }

                //通过Clientsoket发送数据  
                Thread myThread = new Thread(new ParameterizedThreadStart(ComWithClient));
                myThread.Start(socket);
                m_ThreadList.Add(myThread);
                //ComWithClient(socket);
            }
           
        }

        static void SendError(Socket socket, string err = "")
        {
            string ret = "0";
            if (string.IsNullOrEmpty(err))
                ret = errorcode;
            else
                ret = err;
            byte[] send = Encoding.Default.GetBytes(ret);//Encoding.ASCII.GetBytes("Success receive the message,send the back the message");
            socket.Send(send);
            LogUtil.Log("send：" + ret);
            if (debug > 1)
            {
                //Console.WriteLine("**" + System.DateTime.Now + "返回消息：" + ret);
                DebugInfo.Write("返回消息：" + ret);  
            }
        }

        static void ComWithClient(object obj)
        {
            Socket socket = (Socket)obj;
            if (obj == null)
            {
                LogUtil.Log("socket对象为空");
                if (debug > 1)
                {
                    //Console.WriteLine("**" + System.DateTime.Now + "socket对象为空");
                    DebugInfo.Write("socket对象为空");  
                }
                return;
            }

            byte[] receive = new byte[MAX_RECEIVE_LENGTH];
            string result = errorcode;
            while (true)
            {
                try
                {
                    Thread.Sleep(300);
                    //byte[] receive = new byte[200];
                    Int32 bytes = socket.Receive(receive, receive.Length, SocketFlags.None);
                    if (receive == null)
                    {
                        if (debug > 1)
                        {
                            //这个太多了，就不要记录了
                            //LogUtil.Log("接收到消息:null");
                            //Console.WriteLine("**" + System.DateTime.Now + "接收到消息null");
                        }
                        SendError(socket); //这个时候不需要返回? 需要返回，否则客户端认为连接断了，重新连接，线程会越来越多
                        continue; //特别注意：这里一定要用continue，都在只能收到一次
                    }

                    // string data = Encoding.ASCII.GetString(receive, 0, bytes);
                    string data = Encoding.Default.GetString(receive, 0, bytes);
                    //string data2 = Encoding.BigEndianUnicode.GetString(receive, 0, bytes);
                    //string data3 = Encoding.Unicode.GetString(receive, 0, bytes);
                    //data = "{Fg:0,Pn:嶏l齓*N,Pr:W12345,Dc:0001,Fl:000000,Dc:0000,Ot:2018-06-11 22:43:44}";
                    if (string.IsNullOrEmpty(data))
                    {
                        if (debug > 1)
                        {
                            //这个太多了，就不要记录了
                            // LogUtil.Log("接收到消息:null or empty");
                            //Console.WriteLine("**" + System.DateTime.Now + "接收到消息null or empty");
                        }
                        SendError(socket); //? 需要返回，否则客户端认为连接断了，重新连接，线程会越来越多
                        continue;
                    }                    

                    if (debug > 1)
                    {                        
                        DebugInfo.Write("接收到消息ASCII：" + data);                      
                        DebugInfo.Write("接收到消息Unicode：" + Encoding.Unicode.GetString(receive, 0, bytes));
                        DebugInfo.Write("接收到消息Unicode：" + CodingUtil.byteToHexStr(receive));
                    }

                    if (data.Trim().Equals("?")) //收到莫名其妙的？+空格，? 需要返回，否则客户端认为连接断了，重新连接，线程会越来越多
                    {
                        SendError(socket);
                        continue;
                    }

                    //判断是否心跳包：
                    string address = "";
                    if (Envionment_Data.IsHeartBeatPkg(receive, ref address))
                    {
                        //请求包例如：010300000002C40B  其中01表示设备号
                        if (!string.IsNullOrEmpty(address) && address.Length == 2)
                        {
                            if (Envionment_Data.CheckCodeMap != null 
                                && Envionment_Data.CheckCodeMap.Count > 0
                                && Envionment_Data.CheckCodeMap.ContainsKey(address))
                            {
                                string res = "";
                                Envionment_Data.CheckCodeMap.TryGetValue(address, out res);
                                res = address + res; //编号+后面的验证码
                                socket.Send(CodingUtil.strToToHexByte(res));

                                LogUtil.Log("recv Heart(16Hex):" + CodingUtil.byteToHexStr(receive).Substring(0, 4));
                                LogUtil.Log("send Heart Res:" + res);
                            }

                            
                            ////最后4位校验码，需要代码生成，这里暂时先写死几个
                            //string res = address + "0300000002C40B";//01
                            //if (address.Equals("02"))
                            //    res = address + "0300000002C438"; //02
                            //else if (address.Equals("03"))
                            //    res = address + "0300000002C5E9";
                            //else if (address.Equals("04"))
                            //    res = address + "0300000002C45E";
                            //else if (address.Equals("05"))
                            //    res = address + "0300000002C58F";
                            //else if (address.Equals("06"))
                            //    res = address + "0300000002C5BC";
                            //else if (address.Equals("07"))
                            //    res = address + "0300000002C46D";
                            //else if (address.Equals("08"))
                            //    res = address + "0300000002C492";
                            //else if (address.Equals("09"))
                            //    res = address + "0300000002C543";
                            //else if (address.Equals("0A"))
                            //    res = address + "0300000002C570";
                            //else if (address.Equals("0B"))
                            //    res = address + "0300000002C4A1";
                            //else
                            //    ;

                            //socket.Send(CodingUtil.strToToHexByte(res));
                            
                            //LogUtil.Log("recv Heart(16Hex):" + CodingUtil.byteToHexStr(receive).Substring(0,4));
                            //LogUtil.Log("send Heart Res:" + res);
                        }
                        
                    }
                    else //数据包
                    {                       
                        string[] items = new string[Envionment_Data.columnCount];
                        string hexData = CodingUtil.byteToHexStr(receive);
                        
                        if (debug > 1)
                        {
                            DebugInfo.Write("hexData：" + hexData + " length:" + hexData.Length);
                        }
                        if (!string.IsNullOrEmpty(hexData) && hexData.Length >= 18)
                        {
                            hexData = hexData.Substring(0, 18);//确认取前18个字符，因为可能是36个
                            int value0 = Convert.ToInt32(hexData.Substring(0,2), 16);
                            items[0] = "Dc:" + value0.ToString();
                            //温度和湿度要除以10
                            int value1 = Convert.ToInt32(hexData.Substring(6, 4), 16);                           
                            items[1] = "Te:" + (value1/10.0).ToString();
                            int value2 = Convert.ToInt32(hexData.Substring(10, 4), 16);
                            items[2] = "Hu:" + (value2 / 10.0).ToString();
                            items[3] = "Pr:" + ""; //这里先写死为空，后面从数据库里查询出来
                            LogUtil.Log("recv hexdata:" + hexData+"("+ items[0]+","+ items[1] + ","+ items[2] + ")");
                            result = Envionment_Data.DecodeAndInsert(items);
                        }
                       
                    }
                }
                catch (SocketException ex)
                {
                    //这里千万不能输出，因为远程关闭的话，这里会循环无数次打印
                    //LogUtil.Log("Socket错误：" + ex.Message);
                    //if (debug)
                    //{
                    //    Console.WriteLine("**" + System.DateTime.Now + "Socket错误：" + ex.Message);
                    //}
                    break; //add 2008.8.1 结束线程 好像没起作用
                }
                catch (Exception ex)
                {
                    LogUtil.Log("错误：" + ex.Message +" Stack:"+ ex.StackTrace);
                    if (debug > 1)
                    {
                        DebugInfo.Write("错误：" +ex.Message);
                    }
                }

            }//while

        }

    
    }
}
