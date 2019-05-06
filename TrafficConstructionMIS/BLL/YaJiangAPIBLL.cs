using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using MyTool;
using DbModel;
using Newtonsoft.Json;
using MyModel;

namespace BLL
{
    public class YaJiangAPIBLL : BaseBLL, IYaJiangAPIBLL
    {

        ITb_YaJiang_CurrentMonitorDAL Tb_YaJiang_CurrentMonitorDAL { get; set; }
        ITb_YaJiang_CurveDAL Tb_YaJiang_CurveDAL { get; set; }
        ITb_YaJiang_DataDAL Tb_YaJiang_DataDAL { get; set; }
        ITb_YaJiang_ProjectInfoDAL Tb_YaJiang_ProjectInfoDAL { get; set; }

        public string SaveBiaoTouInfo(string data)
        {
            string json = Base64.Decode(data);
            YaJiang_ReceiveData<Tb_YaJiang_ProjectInfo> YaJiangData = JsonConvert.DeserializeObject<YaJiang_ReceiveData<Tb_YaJiang_ProjectInfo>>(json);
            YaJiangData.Data.DeviceNo = YaJiangData.DeviceNo;
            Tb_YaJiang_ProjectInfo temp = Tb_YaJiang_ProjectInfoDAL.AddEntity(YaJiangData.Data);
            Dictionary<string, int> dict = new Dictionary<string, int>();
            if (SaveChanges())
            {
                dict.Add("result", 1);
                dict.Add("ProjectNo", temp.Id);
            }
            else
            {
                dict.Add("result", 0);
            }
            return JsonConvert.SerializeObject(dict);
        }

        public string SaveYaJiangData(string data)
        {
            string json = Base64.Decode(data);
            YaJiang_ReceiveData<Tb_YaJiang_Data> YaJiangData = JsonConvert.DeserializeObject<YaJiang_ReceiveData<Tb_YaJiang_Data>>(json);
            YaJiangData.Data.DeviceNo = YaJiangData.DeviceNo;
            Tb_YaJiang_Data temp = Tb_YaJiang_DataDAL.AddEntity(YaJiangData.Data);
            YaJiang_ReturnData rData = new YaJiang_ReturnData();
            if (SaveChanges())
            {
                rData.Liang = temp.LiangStr;
                rData.RecoNo = temp.Id;
                rData.Result = 1;
            }
            else
            {
                rData.Result = 0;
            }
            
            return JsonConvert.SerializeObject(rData);
        }

        public string SaveYaJiangQuXian(string data)
        {
            string json = Base64.Decode(data);
            YaJiang_ReceiveData<Tb_YaJiang_Curve> YaJiangData = JsonConvert.DeserializeObject<YaJiang_ReceiveData<Tb_YaJiang_Curve>>(json);
            YaJiangData.Data.DeviceNo = YaJiangData.DeviceNo;
            Tb_YaJiang_Curve temp = Tb_YaJiang_CurveDAL.AddEntity(YaJiangData.Data);
            
            Dictionary<string, int> dict = new Dictionary<string, int>();
            if (SaveChanges()) dict.Add("result", 1);
            else dict.Add("result", 0);  
            return JsonConvert.SerializeObject(dict);
        }

        public string SaveYaJiangMonitor(string data)
        {
            string json = Base64.Decode(data);
            YaJiang_ReceiveData<Tb_YaJiang_CurrentMonitor> YaJiangData = JsonConvert.DeserializeObject<YaJiang_ReceiveData<Tb_YaJiang_CurrentMonitor>>(json);
            YaJiangData.Data.DeviceNo = YaJiangData.DeviceNo;
            Tb_YaJiang_CurrentMonitor temp = Tb_YaJiang_CurrentMonitorDAL.AddEntity(YaJiangData.Data);
            Dictionary<string, int> dict = new Dictionary<string, int>();
            if (SaveChanges()) dict.Add("result", 1);
            else dict.Add("result", 0); 
            return JsonConvert.SerializeObject(dict);
        }
    }
}
