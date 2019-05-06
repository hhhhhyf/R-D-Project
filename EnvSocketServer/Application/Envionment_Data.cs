using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonLib;
using System.Text.RegularExpressions;

namespace EnvApplication
{
    public class Envionment_Data
    {
        //public int Id { get; set; } 这个自增不用填写
        public string DeviceCode { get; set; }
        public double? Temperature { get; set; }//温度
        public double? Humidity { get; set; }//湿度

        public int? ProjectId { get; set; }//湿度
        public DateTime? UploadTime { get; set; }

        public static Dictionary<string, string> ColumnMap;

        public static int columnCount = 4;

        public static Dictionary<string, string> CheckCodeMap;

        public static void Init()
        {
            ColumnMap = new Dictionary<string, string>() 
            {               
                {"Dc","DeviceCode"},
                {"Te","Temperature"},
                {"Hu","Humidity"},
                {"Pr","ProjectId"}
               //UploadTime //不需要
            };

            CheckCodeMap = new Dictionary<string, string>()
            {
                {"01","0300000002C40B"},
                {"02","0300000002C438"},
                {"03","0300000002C5E9"},
                {"04","0300000002C45E"},
                {"05","0300000002C58F"},
                {"06","0300000002C5BC"},
                {"07","0300000002C46D"},
                {"08","0300000002C492"},
                {"09","0300000002C543"},
                {"0A","0300000002C570"},
                {"0B","0300000002C4A1"},
                {"0C","0300000002C516"},
                {"0D","0300000002C4C7"},
                {"0E","0300000002C4F4"},
                {"0F","0300000002C525"},
                {"10","0300000002C74A"},
                {"11","0300000002C69B"},
                {"12","0300000002C6A8"},
                {"13","0300000002C779"},
                {"14","0300000002C6CE"}
            };
        }

        /// <summary>
        /// 心跳包格式：十六进制4001
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsHeartBeatPkg(string data)
        {
            bool ret = false;
            if (string.IsNullOrEmpty(data))
                return ret;
            //必须包含{Dc: 并且是开头
            string keystr = "{Dc:";
            if (!(data.Contains(keystr) && data.IndexOf(keystr) == 0))
                return ret;
            //: 冒号只出现一次：
            if (Regex.Matches(data, ":").Count == 1)
                return true;

            return ret;
        }

        /// <summary>
        /// 心跳包格式：十六进制4001,其中40是心跳包标志，01是设备地址
        /// </summary>
        /// <param name="recv"></param>
        /// <returns></returns>
        public static bool IsHeartBeatPkg(byte[] recv, ref string address)
        {
            bool ret = false;
            if (recv == null || recv.Length == 0)
                return ret;
            string result = CodingUtil.byteToHexStr(recv);
            if (string.IsNullOrEmpty(result) || result.Length <4)
                return ret;
            //是4001
            string keystr = "40";
            string heartFlag = result.Substring(0, 2);
            if (heartFlag.Equals(keystr))
            {
                address = result.Substring(2, 2);
                return true;
            }
            
            return ret;
        }

        public static string DecodeAndInsert(string[] items)
        {
            string result = "0";

            Dictionary<string, string> keyvalue = new Dictionary<string, string>();
            for (int i = 0; i < items.Length; i++)
            {
                if (!string.IsNullOrEmpty(items[i]) && items[i].Contains(":"))
                {
                    string[] kv = items[i].Split(':');

                    //
                    //string s2 = "2Ab刘";
                    string res = ""; //初始为空
                    if(kv.Length > 1) //即有值
                        res = kv[1].Trim();
                   

                    if (ColumnMap != null && ColumnMap.ContainsKey(kv[0].Trim()))
                    {
                        if (!keyvalue.ContainsKey(ColumnMap[kv[0].Trim()]))
                            keyvalue.Add(ColumnMap[kv[0].Trim()], res);
                    }
                    else
                    {
                        if (!keyvalue.ContainsKey(kv[0].Trim()))
                            keyvalue.Add(kv[0].Trim(), res);

                    }
                   
                }
            }

            if (keyvalue.Count > 0)
            {
                Envionment_Data value = new Envionment_Data();               
               
                if (keyvalue.ContainsKey("DeviceCode"))
                    value.DeviceCode = keyvalue["DeviceCode"];
                if (keyvalue.ContainsKey("Temperature"))
                {
                    double d;
                    if (Double.TryParse(keyvalue["Temperature"], out d))
                        value.Temperature = d;//温度                    
                }
                if (keyvalue.ContainsKey("Humidity"))
                {
                    double d;
                    if (Double.TryParse(keyvalue["Humidity"], out d))
                        value.Humidity = d;//湿度              
                }

                if (keyvalue.ContainsKey("ProjectId"))
                {
                    int d;
                    if (string.IsNullOrEmpty(keyvalue["ProjectId"]))
                        value.ProjectId = null;
                    else
                    {
                        if (Int32.TryParse(keyvalue["ProjectId"], out d))
                            value.ProjectId = d;//归属的项目Id
                    }

                    //这里最终还是要从数据库表中根据设备号读取项目id，上面的不用
                    //
                    List<Envionment_DeviceProject> prjList = DbHelper.Query<Envionment_DeviceProject, string>(value.DeviceCode, "DeviceCode");
                    if (prjList != null && prjList.Count == 1) //只有一个，说明找到了
                    {
                        value.ProjectId = prjList[0].ProjectId;
                    }
                }



                value.UploadTime = System.DateTime.Now;

                if (!(string.IsNullOrEmpty(value.DeviceCode) && value.Temperature == null && value.Humidity == null))
                {
                    bool addRes = DbHelper.Insert<Envionment_Data>(value);
                    if (addRes)
                        result = "1";
                }
                result = "1";//at last 1
            }


            return result;
        }
    }
    
}