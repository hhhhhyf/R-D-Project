using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModel;
using IBLL;
using DbModel;
using IDAL;
using MyTool;
using Newtonsoft.Json;


namespace BLL
{
    public class YaJiang1APIBLL : BaseBLL, IYaJiang1APIBLL
    {
        ITb_YaJiang1_CurveDAL Tb_YaJiang1_CurveDAL { get; set; }
        ITb_YaJiang1_DataDAL Tb_YaJiang1_DataDAL { get; set; }

        public string SaveYaJiangData(string data)
        {
            string json = Base64.Decode(data);
            Tb_YaJiang1_Data yaJiangData = JsonConvert.DeserializeObject<Tb_YaJiang1_Data>(json);
            Tb_YaJiang1_Data temp = Tb_YaJiang1_DataDAL.AddEntity(yaJiangData);
            ZhangLa_ReturnData rData = new ZhangLa_ReturnData();
            if (SaveChanges())
            {
                rData.Liang = temp.LiangNo;
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
            Tb_YaJiang1_Curve yaJiangData = JsonConvert.DeserializeObject<Tb_YaJiang1_Curve>(json);
            Tb_YaJiang1_Curve temp = Tb_YaJiang1_CurveDAL.AddEntity(yaJiangData);
            Dictionary<string, int> dict = new Dictionary<string, int>();
            if (SaveChanges()) dict.Add("result", 1);
            else dict.Add("result", 0);
            return JsonConvert.SerializeObject(dict);
        }


        public string SaveYaJiangQuXianArray(string data)
        {
            string json = Base64.Decode(data);
            List<Tb_YaJiang1_Curve> yaJiangDatas = JsonConvert.DeserializeObject < List<Tb_YaJiang1_Curve>>(json);
            foreach (Tb_YaJiang1_Curve yaJiangData in yaJiangDatas)
            {
                Tb_YaJiang1_CurveDAL.AddEntity(yaJiangData);
            }
            Dictionary<string, int> dict = new Dictionary<string, int>();
            if (SaveChanges()) dict.Add("result", 1);
            else dict.Add("result", 0);
            return JsonConvert.SerializeObject(dict);
        }
    }
}
