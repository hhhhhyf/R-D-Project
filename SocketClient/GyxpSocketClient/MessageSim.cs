using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyxpSocketClient
{
    class MessageSim
    {
        public static List<string>[] data;
        public static List<string>[] dataV1;

        public static void initMsgV1()
        {   //A5 3次 17分钟   A4:1, 26 A3:3,13  A2： 1,8
            dataV1 = new List<string>[5];
            dataV1[0] = new List<string>(){ @"{93a74d468cb94a1fb6025749c69da971,bf350b4984e94b28a0208773021574cd,Zjwl,Yh,0003,7,1,1,4,2,2,2
[{2018-12-21 14:58:53,0,UYG,,1,1|   ",
                "2018-12-21 14:58:57,0,,,0,1|   ",
                "2018-12-21 14:59:02,0,,,1,1|                                                                                                                                               ",
                "2018-12-21 14:59:07,0,,,1,1|             ",
                "2018-12-21 14:59:12,0,,,0,0|           ",
                "2018-12-21 14:59:17,0,,,1,0}]}          "
                };
            dataV1[1] = new List<string>(){ @"{93a74d468cb94a1fb6025749c69da971,bf350b4984e94b28a0208773021574cd,Zjwl,Yh,0003,7,1,1,4,2,2,2
[{2018-12-21 15:02:53,0,UYG,,1,1|   ",
                "2018-12-21 15:02:43,0,,,1,1|   ",
                "2018-12-21 15:02:02,0,,,1,1|                                                                                                                                               ",
                "2018-12-21 15:02:07,0,,,0,0|             ",
                "2018-12-21 15:02:12,0,,,0,0|           ",
                "2018-12-21 15:02:17,0,,,1,1}]}          "
                };
            dataV1[2] = new List<string>(){ @"{93a74d468cb94a1fb6025749c69da971,bf350b4984e94b28a0208773021574cd,Zjwl,Yh,0003,7,1,1,4,2,2,2
[{2018-12-21 15:05:53,0,UYG,,1,1|   ",
                "2018-12-21 15:02:58,0,,,1,1|   ",
                "2018-12-21 15:02:32,0,,,1,1|                                                                                                                                               ",
                "2018-12-21 15:05:07,0,,,0,1|             ",
                "2018-12-21 15:05:12,0,,,0,0|           ",
                "2018-12-21 15:05:17,0,,,0,1}]}          "
                };
            dataV1[3] = new List<string>(){ @"{93a74d468cb94a1fb6025749c69da971,bf350b4984e94b28a0208773021574cd,Zjwl,Yh,0003,7,1,1,4,2,2,2
[{2018-12-21 15:08:53,0,UYG,,0,1|   ",
                "2018-12-21 15:08:57,0,,,1,1|   ",
                "2018-12-21 15:02:47,0,,,1,1|                                                                                                                                               ",
                "2018-12-21 15:08:07,0,,,1,1|             ",
                "2018-12-21 15:08:12,0,,,0,0|           ",
                "2018-12-21 15:08:17,0,,,0,1}]}          "
                };
            dataV1[4] = new List<string>(){ @"{93a74d468cb94a1fb6025749c69da971,bf350b4984e94b28a0208773021574cd,Zjwl,Yh,0003,7,1,1,4,2,2,2
[{2018-12-21 15:10:53,0,UYG,,1,1|   ",
                "2018-12-21 15:10:57,0,,,0,1|   ",
                "2018-12-21 15:10:02,0,,,1,1|                                                                                                                                               ",
                "2018-12-21 15:10:07,0,,,1,1|             ",
                "2018-12-21 15:10:12,0,,,0,0|           ",
                "2018-12-21 15:10:17,0,,,0,1}]}          "
                };
        }
        public static void initMsgV2()
        {   //A5 3次 17分钟   A4:1, 26 A3:3,13  A2： 1,8
            data = new List<string>[10];
            data[0] = new List<string>(){ @"{bb93d839e6284473b6598f93473c44c4, 26f9dbe38cf947958e7ea922f78f9b6a, Zjwl, 0011, 7, 167, 517, 001, 1,
[2018-12-24 10:26:48, _UJUJUJUJUJUJUJUJUJUJ_,T2-5, A6,0,0|",
                "2018-12-24 10:26:50,_UJUJUJUJUJUJUJUJUJUJ_,T1-12,A5,0,1 |           ",
                "2018-12-24 10:26:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-13 ,A4,1,1|            ",
                "2018-12-24 10:26:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-19 ,A3,1,1|            ",
                "2018-12-24 10:26:53,_UJUJUJUJUJUJUJUJUJUJ_,T1-16 ,A2,0,1|          ",
                "2018-12-24 10:26:56,_UJUJUJUJUJUJUJUJUJUJ_,T1-14,A1,0,0]}            "
                };

            data[1] = new List<string>(){ @"{bb93d839e6284473b6598f93473c44c4, 26f9dbe38cf947958e7ea922f78f9b6a, Zjwl, 0011, 7, 167, 517, 001, 1,
[2018-12-24 10:28:48, _UJUJUJUJUJUJUJUJUJUJ_,T2-5, A6,1,0|",
                "2018-12-24 10:28:50,_UJUJUJUJUJUJUJUJUJUJ_,T1-12,A5,1,1 |           ",
                "2018-12-24 10:28:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-13 ,A4,1,1|            ",
                "2018-12-24 10:28:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-19 ,A3,0,1|            ",
                "2018-12-24 10:28:53,_UJUJUJUJUJUJUJUJUJUJ_,T1-16 ,A2,0,1|          ",
                "2018-12-24 10:28:56,_UJUJUJUJUJUJUJUJUJUJ_,T1-14,A1,0,0]}            "
                };
            data[2] = new List<string>(){ @"{bb93d839e6284473b6598f93473c44c4, 26f9dbe38cf947958e7ea922f78f9b6a, Zjwl, 0011, 7, 167, 517, 001, 1,
[2018-12-24 10:33:48, _UJUJUJUJUJUJUJUJUJUJ_,T2-5, A6,0,0|",
                "2018-12-24 10:33:50,_UJUJUJUJUJUJUJUJUJUJ_,T1-12,A5,0,1 |           ",
                "2018-12-24 10:33:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-13 ,A4,1,1|            ",
                "2018-12-24 10:33:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-19 ,A3,0,1|            ",
                "2018-12-24 10:33:53,_UJUJUJUJUJUJUJUJUJUJ_,T1-16 ,A2,1,1|          ",
                "2018-12-24 10:33:56,_UJUJUJUJUJUJUJUJUJUJ_,T1-14,A1,0,0]}            "
                };
            data[3] = new List<string>(){ @"{bb93d839e6284473b6598f93473c44c4, 26f9dbe38cf947958e7ea922f78f9b6a, Zjwl, 0011, 7, 167, 517, 001, 1,
[2018-12-24 10:37:48, _UJUJUJUJUJUJUJUJUJUJ_,T2-5, A6,0,0|",
                "2018-12-24 10:37:50,_UJUJUJUJUJUJUJUJUJUJ_,T1-12,A5,1,1 |           ",
                "2018-12-24 10:37:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-13 ,A4,1,1|            ",
                "2018-12-24 10:37:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-19 ,A3,1,1|            ",
                "2018-12-24 10:37:53,_UJUJUJUJUJUJUJUJUJUJ_,T1-16 ,A2,1,1|          ",
                "2018-12-24 10:37:56,_UJUJUJUJUJUJUJUJUJUJ_,T1-14,A1,0,0]}            "
                };

            data[4] = new List<string>(){ @"{bb93d839e6284473b6598f93473c44c4, 26f9dbe38cf947958e7ea922f78f9b6a, Zjwl, 0011, 7, 167, 517, 001, 1,
[2018-12-24 10:41:48, _UJUJUJUJUJUJUJUJUJUJ_,T2-5, A6,1,0|",
                "2018-12-24 10:41:50,_UJUJUJUJUJUJUJUJUJUJ_,T1-12,A5,1,1 |           ",
                "2018-12-24 10:41:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-13 ,A4,1,1|            ",
                "2018-12-24 10:41:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-19 ,A3,0,1|            ",
                "2018-12-24 10:41:53,_UJUJUJUJUJUJUJUJUJUJ_,T1-16 ,A2,0,1|          ",
                "2018-12-24 10:41:56,_UJUJUJUJUJUJUJUJUJUJ_,T1-14,A1,0,0]}            "
                };
            data[5] = new List<string>(){ @"{bb93d839e6284473b6598f93473c44c4, 26f9dbe38cf947958e7ea922f78f9b6a, Zjwl, 0011, 7, 167, 517, 001, 1,
[2018-12-24 10:44:48, _UJUJUJUJUJUJUJUJUJUJ_,T2-5, A6,1,0|",
                "2018-12-24 10:44:50,_UJUJUJUJUJUJUJUJUJUJ_,T1-12,A5,0,1 |           ",
                "2018-12-24 10:44:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-13 ,A4,1,1|            ",
                "2018-12-24 10:44:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-19 ,A3,1,1|            ",
                "2018-12-24 10:44:53,_UJUJUJUJUJUJUJUJUJUJ_,T1-16 ,A2,0,1|          ",
                "2018-12-24 10:44:56,_UJUJUJUJUJUJUJUJUJUJ_,T1-14,A1,0,0]}            "
                };
            data[6] = new List<string>(){ @"{bb93d839e6284473b6598f93473c44c4, 26f9dbe38cf947958e7ea922f78f9b6a, Zjwl, 0011, 7, 167, 517, 001, 1,
[2018-12-24 10:51:48, _UJUJUJUJUJUJUJUJUJUJ_,T2-5, A6,1,0|",
                "2018-12-24 10:51:50,_UJUJUJUJUJUJUJUJUJUJ_,T1-12,A5,1,1 |           ",
                "2018-12-24 10:51:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-13 ,A4,1,1|            ",
                "2018-12-24 10:51:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-19 ,A3,0,1|            ",
                "2018-12-24 10:51:53,_UJUJUJUJUJUJUJUJUJUJ_,T1-16 ,A2,0,1|          ",
                "2018-12-24 10:51:56,_UJUJUJUJUJUJUJUJUJUJ_,T1-14,A1,0,0]}            "
                };
            data[7] = new List<string>(){ @"{bb93d839e6284473b6598f93473c44c4, 26f9dbe38cf947958e7ea922f78f9b6a, Zjwl, 0011, 7, 167, 517, 001, 1,
[2018-12-24 10:52:48, _UJUJUJUJUJUJUJUJUJUJ_,T2-5, A6,1,0|",
                "2018-12-24 10:52:50,_UJUJUJUJUJUJUJUJUJUJ_,T1-12,A5,1,1 |           ",
                "2018-12-24 10:52:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-13 ,A4,1,0|            ",
                "2018-12-24 10:52:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-19 ,A3,0,1|            ",
                "2018-12-24 10:52:53,_UJUJUJUJUJUJUJUJUJUJ_,T1-16 ,A2,0,0|          ",
                "2018-12-24 10:52:56,_UJUJUJUJUJUJUJUJUJUJ_,T1-14,A1,0,0]}            "
                };
            data[8] = new List<string>(){ @"{bb93d839e6284473b6598f93473c44c4, 26f9dbe38cf947958e7ea922f78f9b6a, Zjwl, 0011, 7, 167, 517, 001, 1,
[2018-12-24 10:54:48, _UJUJUJUJUJUJUJUJUJUJ_,T2-5, A6,1,0|",
                "2018-12-24 10:54:50,_UJUJUJUJUJUJUJUJUJUJ_,T1-12,A5,1,1 |           ",
                "2018-12-24 10:54:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-13 ,A4,1,1|            ",
                "2018-12-24 10:54:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-19 ,A3,0,1|            ",
                "2018-12-24 10:54:53,_UJUJUJUJUJUJUJUJUJUJ_,T1-16 ,A2,0,1|          ",
                "2018-12-24 10:54:56,_UJUJUJUJUJUJUJUJUJUJ_,T1-14,A1,0,0]}            "
                };
            data[9] = new List<string>(){ @"{bb93d839e6284473b6598f93473c44c4, 26f9dbe38cf947958e7ea922f78f9b6a, Zjwl, 0011, 7, 167, 517, 001, 1,
[2018-12-24 10:56:48, _UJUJUJUJUJUJUJUJUJUJ_,T2-5, A6,1,0|",
                "2018-12-24 10:56:50,_UJUJUJUJUJUJUJUJUJUJ_,T1-12,A5,1,1 |           ",
                "2018-12-24 10:56:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-13 ,A4,1,1|            ",
                "2018-12-24 10:56:52,_UJUJUJUJUJUJUJUJUJUJ_,T1-19 ,A3,0,1|            ",
                "2018-12-24 10:56:53,_UJUJUJUJUJUJUJUJUJUJ_,T1-16 ,A2,0,0|          ",
                "2018-12-24 10:56:56,_UJUJUJUJUJUJUJUJUJUJ_,T1-14,A1,0,0]}            "
                };

        }
        /// <summary>
        /// 产生模拟消息
        /// </summary>
        /// <param name="seq">指定消息</param>
        /// <returns></returns>
        public static string CreateGyxpMsg(int seq)
        {
            int NUM = 5;
            string msg = "{Fg:0,Pn:嶏l齓*N,Pc:Whelm4444444,Dc:0001,Fl:777,Pr:888,Ot:2018-06-11 22:43:44}";
            if (seq % NUM == 0)
                msg = "{Fg:0,Pn:嶏l齓*N,Pc:Whelm4444444,Dc:0001,Fl:777,Pr:888,Ot:2018-06-11 22:43:44}";
            else if (seq % NUM == 1)
                msg = "{Dc:d002}";
            else if (seq % NUM == 2)
                msg = "?";
            else if (seq % NUM == 4)
                msg = " ";
            else
                msg = "xxxx";
            return msg;
        }

        /// <summary>
        /// 产生模拟消息
        /// </summary>
        /// <param name="seq">指定消息</param>
        /// <returns></returns>
        public static string CreateEnvionmentMsg(int seq)
        {
            string msg = "4001";
            if (seq % 2 == 1)
                msg = "010304011303528B07";
            else
                msg = "4001";
            /*
            if (count % 2 == 1)
                msg = "090300000002C543";//"010304011303528B07";
            else
                msg = "4009";// "4001";
                */
            return msg;
        }

        /// <summary>
        /// 产生模拟消息
        /// </summary>
        /// <param name="seq">指定消息</param>
        /// <returns></returns>
        public static string CreateYanghuMsg(int seq)
        {
            List<string>[] inputData;
            //inputData = data;
            inputData = dataV1;
            int NUM = 6;
            //List<String> msgList = new List<string>(NUM)
            //下面消息实际复制出来，可能带有空格，这里真实模拟
            if (seq != 0 && seq%NUM ==0) //不是第一条，后面重新开始完整的一条
                System.Threading.Thread.Sleep(200);
            else
                System.Threading.Thread.Sleep(100);
            string msg = "";
            int msgNo = seq / NUM; //发完整的第几条;
            if (msgNo == 9)
                ; //use  to stop
            if(msgNo < inputData.Length)
                msg = inputData[msgNo][seq % NUM];
           
            return msg;
        }
    }
}
