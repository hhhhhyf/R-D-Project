using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib
{
    public struct ConfigJson
    {
        public string serviceName { get; set; }

        //这里是数据库连接配置
        public string dbAddress { get; set; }  //属性的名字，必须与json格式字符串中的"key"值一样。
        public string dbPort { get; set; } //数据库端口
        public string dbInstance { get; set; }
        public string dbUserid { get; set; }
        public string dbPwd { get; set; }       
        
        //下面是socket配置
        public string socketIps { get; set; }  //客户端的ip地址，为空表示任何ip都可以接入，多个ip地址以逗号分开。
        public string socketPort { get; set; } //数据库端口  

        public string debugLevel { get; set; } //0不输出 1一般输出 2详细输出（包括控制台和Log）
    }
   
}
