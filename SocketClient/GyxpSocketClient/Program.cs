using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GyxpSocketClient
{
    class Program
    {
        static Socket ClientSocket;
        static void Main(string[] args)
        {
            MessageSim.initMsgV1();
            MessageSim.initMsgV2();
            String IP = "127.0.0.1";
            int port = 6118;
            //String IP = "139.129.167.50";
            //int port =11111 ;

            try
            {
                IPAddress ip = IPAddress.Parse(IP);  //将IP地址字符串转换成IPAddress实例
                ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//使用指定的地址簇协议、套接字类型和通信协议
                IPEndPoint endPoint = new IPEndPoint(ip, port); // 用指定的ip和端口号初始化IPEndPoint实例
                ClientSocket.Connect(endPoint);  //与远程主机建立连接
                int count = 0;
                while (true)
                {
                    //string msg = "ProjectName:UJUJUJUJUJUJUJUJUJUJ,PileSite:pile1,DeviceCode:dev1,Flow:133.5,Pressure:1333345,OperateTime:2018-06-03 14:20:30";
                    //string msg = "Fg:1,Pn:台州,Pc:pile1,Dc:dev1,Fl:133.5,Pr:1333345,Ot:2018-06-03 14:20:30";
                    //string msg = "Pn:台州,Pc:pile1,Dc:dev1,Lo:3.44,Tt:3456888,Tf:673345.3,Td:34555.6,Et:2018-06-03 14:20:30";
                    //string msg = "{Fg:0,pn:UJUJUJUJUJUJUJUJUJUJ,Pc:W12345,Dc:0001,Fl:000000,Pr:0000,Ot:2018-06-11 22:43:44}   {pn:UJUJUJUJUJUJUJUJUJUJ,Pc:W12345,Dc:0001,Lo:003000,Tt:000000,Tf:000000,Td:0000,Et:2018-06-11 22:43:46}";
                    //string msg = "{Fg:0,Pn:UJUJUJUJUJUJUJUJUJUJ,Pr:W12345,Dc:0001,Fl:000000,Dc:0000,Ot:2018-06-11 22:43:44}";
                    //string msg = "{Fg:0,Pn:嶏l齓*N,Pc:Whelm4444444,Dc:0001,Fl:777,Pr:888,Ot:2018-06-11 22:43:44}";
                    //string msg = "{Fg:0,Pn:嶏l齓*,5,6,8abcN,Pc:Whelm5555,Dc:0001,Fl:777,Pr:888,Ot:2018-06-11 22:43:44}";
                    //string msg = "{Pn:台州123,abcN,d,Pc:pile1,Dc:dev1,Lo:3.44,Tt:3456888,Tf:673345.3,Td:34555.6,Et:2018-06-03 14:20:30}";
                    //string msg = "{Pn:台州123,Pc:pile1,Dc:dev1,Lo:3.44,Tt:3456888,Tf:673345.3,Td:34555.6,Et:2018-06-03 14:20:30}";
                    //heart beat

                    string msg = "";
                    int msgType = 2; //1.高压旋喷 2.环境（温度湿度） 3.养护信息
                    switch (msgType)
                    {
                        case 1:
                            msg = MessageSim.CreateGyxpMsg(count);
                            //msg = "{Pn:台州123,Pc:pile1,Dc:dev1,Lo:3.44,Tt:3456888,Tf:673345.3,Td:34555.6,Et:2018-06-03 14:20:30}";
                            break;
                        case 2:
                            msg = MessageSim.CreateEnvionmentMsg(count);
                            break;
                        case 3:
                            msg = MessageSim.CreateYanghuMsg(count);
                            break;
                        default:
                            break;
                    }
                    if (!string.IsNullOrEmpty(msg))
                    {
                        byte[] message = UTF8Encoding.UTF8.GetBytes(msg);
                        ClientSocket.Send(message);
                        Console.WriteLine("发送消息为:" + msg);
                    }

                    //int length = ClientSocket.Receive(receive);  // length 接收字节数组长度
                    //Console.WriteLine("接收消息为（UTF8）：" + UTF8Encoding.UTF8.GetString(receive));
                    //Console.WriteLine("开始发送消息");

                    //if (env == false)
                    //{
                    //    //byte[] message = Encoding.ASCII.GetBytes("Connect the Server");  //通信时实际发送的是字节数组，所以要将发送消息转换字节

                    //    byte[] message = UTF8Encoding.UTF8.GetBytes(msg);
                    //    ClientSocket.Send(message);
                    //    Console.WriteLine("发送消息为（UTF8）:" + UTF8Encoding.UTF8.GetString(message));

                    //    byte[] receive = new byte[200];
                    //    int length = ClientSocket.Receive(receive);  // length 接收字节数组长度
                    //    Console.WriteLine("接收消息为（UTF8）：" + UTF8Encoding.UTF8.GetString(receive));
                    //    //Thread.Sleep(10000);
                    //}
                    //else
                    //{
                    //    byte[] message = strToToHexByte(msg);//Encoding.Default.GetBytes(msg);
                    //    ClientSocket.Send(message);
                    //    Console.WriteLine("发送消息为（16进制byte）:" + message + ":"+msg);

                    //    byte[] receive = new byte[200];
                    //    int length = ClientSocket.Receive(receive);  // length 接收字节数组长度
                    //    string res = byteToHexStr(receive);
                    //    Console.WriteLine("接收消息为（16进制byte）：" + receive + ":"+ res);
                    //}

                    Thread.Sleep(1000);

                    count++;
                }
                ClientSocket.Close();  //关闭连接
            }
            catch (SocketException ex)
            { }
            catch(Exception e)
            {
            }
        }


        private static string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符
            {
                result += Convert.ToString(b[i], 16);
            }
            return result;
        }

        private static string HexStringToString(string hs, Encoding encode)
        {
            string strTemp = "";
            byte[] b = new byte[hs.Length / 2];
            for (int i = 0; i < hs.Length / 2; i++)
            {
                strTemp = hs.Substring(i * 2, 2);
                b[i] = Convert.ToByte(strTemp, 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }

        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }


    }
}
