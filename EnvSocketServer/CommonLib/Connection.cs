using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib
{
    class Connection
    {
        //默认的
        public static string connectionString="Data Source=139.129.167.50,12345;Initial Catalog=YuHuan_TrafficConstructionMIS;uid=jtjsypt;pwd=Zjwlkj123";
        /// <summary>
        /// 从json来设置
        /// </summary>
        /// <param name="json"></param>
        public static void SetConnectionString(ConfigJson json)
        {          
            try
            {
                connectionString = @"server=" + json.dbAddress + "," + json.dbPort
                + ";database=" + json.dbInstance + ";uid=" + json.dbUserid + ";pwd=" + json.dbPwd;        
            }
            catch (Exception ex)
            {
                LogUtil.Log("读取数据库配置" + ex.Message);
            }
            finally
            {

            }
        }
    }
}
