using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using MyTool;
using MyModel;
using DbModel;
using Newtonsoft.Json;
using System.Data.Entity;

namespace BLL
{
    public class ZhangLaAPIBLL : BaseBLL, IZhangLaAPIBLL
    {
        ITb_ZhangLa_CurrentMonitorDAL Tb_ZhangLa_CurrentMonitorDAL{ get; set;}
        ITb_ZhangLa_CurveDAL Tb_ZhangLa_CurveDAL { get; set; }
        ITb_ZhangLa_DataDAL Tb_ZhangLa_DataDAL { get; set; }
        ITb_ZhangLa_ProjectInfoDAL Tb_ZhangLa_ProjectInfoDAL { get; set; }

        public string SaveBiaoTouInfo(string data)
        {
            string json = Base64.Decode(data);
            ZhangLa_ReceiveData<Tb_ZhangLa_ProjectInfo> zhangLaData = JsonConvert.DeserializeObject<ZhangLa_ReceiveData<Tb_ZhangLa_ProjectInfo>>(json);
            zhangLaData.Data.DeviceNo = zhangLaData.DeviceNo;
            Tb_ZhangLa_ProjectInfo temp = Tb_ZhangLa_ProjectInfoDAL.AddEntity(zhangLaData.Data);
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

        public string SaveZhangLaData(string data)
        {
            string json = Base64.Decode(data);
            ZhangLa_ReceiveData<Tb_ZhangLa_Data> zhangLaData = JsonConvert.DeserializeObject<ZhangLa_ReceiveData<Tb_ZhangLa_Data>>(json);
            zhangLaData.Data.DeviceNo = zhangLaData.DeviceNo;
            Tb_ZhangLa_Data temp = Tb_ZhangLa_DataDAL.AddEntity(zhangLaData.Data);
            ZhangLa_ReturnData rData = new ZhangLa_ReturnData();
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

        public string SaveZhangLaQuXian(string data)
        {
            string json = Base64.Decode(data);
            ZhangLa_ReceiveData<Tb_ZhangLa_Curve> zhangLaData = JsonConvert.DeserializeObject<ZhangLa_ReceiveData<Tb_ZhangLa_Curve>>(json);
            zhangLaData.Data.DeviceNo = zhangLaData.DeviceNo;
            Tb_ZhangLa_Curve temp = Tb_ZhangLa_CurveDAL.AddEntity(zhangLaData.Data);
            Dictionary<string, int> dict = new Dictionary<string, int>();
            if (SaveChanges()) dict.Add("result", 1);
            else dict.Add("result", 0);  
            return JsonConvert.SerializeObject(dict);
        }

        public string SaveZhangLaMonitor(string data)
        {
            string json = Base64.Decode(data);
            ZhangLa_ReceiveData<Tb_ZhangLa_CurrentMonitor> zhangLaData = JsonConvert.DeserializeObject<ZhangLa_ReceiveData<Tb_ZhangLa_CurrentMonitor>>(json);
            zhangLaData.Data.DeviceNo = zhangLaData.DeviceNo;
            Tb_ZhangLa_CurrentMonitor temp = Tb_ZhangLa_CurrentMonitorDAL.AddEntity(zhangLaData.Data);
            Dictionary<string, int> dict = new Dictionary<string, int>();
            if (SaveChanges()) dict.Add("result", 1);
            else dict.Add("result", 0); 
            return JsonConvert.SerializeObject(dict);
        }
    }
}
