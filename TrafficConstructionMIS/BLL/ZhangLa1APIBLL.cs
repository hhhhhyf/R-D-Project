using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbModel;
using IDAL;
using MyTool;
using Newtonsoft.Json;
using System.Data.Entity;
using MyModel;
using IBLL;
namespace BLL
{
    public class ZhangLa1APIBLL : BaseBLL, IZhangLa1APIBLL
    {
        ITb_ZhangLa1_CurveDAL Tb_ZhangLa1_CurveDAL { get; set; }
        ITb_ZhangLa1_DataDAL Tb_ZhangLa1_DataDAL { get; set; }


        public string SaveZhangLaData(string data)
        {
            string json = Base64.Decode(data);
            Tb_ZhangLa1_Data zhangLaData = JsonConvert.DeserializeObject<Tb_ZhangLa1_Data>(json);
            Tb_ZhangLa1_Data temp = Tb_ZhangLa1_DataDAL.AddEntity(zhangLaData);
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

        public string SaveZhangLaQuXian(string data)
        {
            string json = Base64.Decode(data);
            Tb_ZhangLa1_Curve zhangLaData = JsonConvert.DeserializeObject<Tb_ZhangLa1_Curve>(json);
            Tb_ZhangLa1_Curve temp = Tb_ZhangLa1_CurveDAL.AddEntity(zhangLaData);
            Dictionary<string, int> dict = new Dictionary<string, int>();
            if (SaveChanges()) dict.Add("result", 1);
            else dict.Add("result", 0);
            return JsonConvert.SerializeObject(dict);
        }

        public string SaveZhangLaQuXianArray(string data)
        {
            string json = Base64.Decode(data);
            List<Tb_ZhangLa1_Curve> zhangLaDatas = JsonConvert.DeserializeObject<List<Tb_ZhangLa1_Curve>>(json);
            foreach(Tb_ZhangLa1_Curve zhangLaData in zhangLaDatas)
            {
                Tb_ZhangLa1_CurveDAL.AddEntity(zhangLaData);
            }
            
            Dictionary<string, int> dict = new Dictionary<string, int>();
            if (SaveChanges()) dict.Add("result", 1);
            else dict.Add("result", 0);
            return JsonConvert.SerializeObject(dict);
        }
    }
}
