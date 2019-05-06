using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using DbModel;
using MyModel;
using IDAL;
using Newtonsoft.Json;
namespace BLL
{
    public class WeiXin_APIBLL : BaseBLL, IWeiXin_APIBLL
    {
        ILoginBLL LoginBLL { set; get; }
        IMixingPlantBLL MixingPlantBLL { set; get; }
        ILabBLL LabBLL { set; get; }
        IZhangLa1BLL ZhangLa1BLL { set; get; }
        IYaJiang1BLL YaJiang1BLL { set; get; }
        IYanghu_StatDataBLL Yanghu_StatDataBLL { set; get; }
        IEnvionment_StatisticBLL Envionment_StatisticBLL { set; get; }
        IEnvionment_ActualTimeBLL Envionment_ActualTimeBLL { set; get; }

        ITb_MixingPlant_DeviceDAL Tb_MixingPlant_DeviceDAL { set; get; }
        ITb_Lab_DepartmentDAL Tb_Lab_DepartmentDAL { set; get; }
        IEnvionment_DeviceProjectDAL Envionment_DeviceProjectDAL { set; get; }
        ITb_ZhangLa1_DeviceDAL Tb_ZhangLa1_DeviceDAL { set; get; }
        ITb_YaJiang1_DeviceDAL Tb_YaJiang1_DeviceDAL { set; get; }

        public string CheckUserLogin(string userName, string password)
        {
            return LoginBLL.CheckUserLogin(userName, password);
        }


        public string GetMixingPlantData(int pageIndex, int pageSize, string deviceFacld)
        {
            if ("1".Equals(deviceFacld)) deviceFacld = "HZS120混凝土搅拌站";
            else if ("2".Equals(deviceFacld)) deviceFacld = "HZS120混凝土搅拌站-2#站";
            return MixingPlantBLL.GetTableData(pageIndex, pageSize, deviceFacld);
        }
        public string GetMixingPlantWarnData(int pageIndex, int pageSize, string deviceFacld) 
        {
            if ("1".Equals(deviceFacld)) deviceFacld = "HZS120混凝土搅拌站";
            else if ("2".Equals(deviceFacld)) deviceFacld = "HZS120混凝土搅拌站-2#站";
            return MixingPlantBLL.GetWarnTableData(pageIndex , pageSize, deviceFacld);
        }
        public string GetMixingPlantDetailData(string Id, string deviceFacld)
        {
            if ("1".Equals(deviceFacld)) deviceFacld = "HZS120混凝土搅拌站";
            else if ("2".Equals(deviceFacld)) deviceFacld = "HZS120混凝土搅拌站-2#站";
            return MixingPlantBLL.GetDosageTableData(Id, deviceFacld);
        }




        public string GetLabData(int pageIndex, int pageSize, int departmentId)
        {
            return LabBLL.GetTableData(pageIndex, pageSize, departmentId);
        }
        public void GetLabDetailData(int Id, List<Tb_Lab_TestItem> labTestItem, List<Tb_Lab_Test> labTest)
        {
            LabBLL.GetDetailData(Id, labTestItem, labTest);
        }
        /// <summary>
        /// 潘承瑞，共四类曲线，1-4分别为钢筋拉伸（3条）、水泥抗折（3条）、水泥抗压（6条）、混凝土抗压（3条）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Lab_CurveChart</returns>
        public string GetCurveData(int Id)
        {
            return LabBLL.GetCurveData(Id);
        }


        public string GetTensionData(int pageIndex, int pageSize, int id)
        {
            return ZhangLa1BLL.GetTableData(pageIndex, pageSize, id);
        }
        public void GetTensionDetailData(int id, ZhangLa1_DetailData_BaseInfo baseInfo, ZhangLa1_DetailData_Ding ding1,
    ZhangLa1_DetailData_Ding ding2, ZhangLa1_DetailData_Ding ding3, ZhangLa1_DetailData_Ding ding4)
        {
            ZhangLa1BLL.GetDetailData(id, baseInfo, ding1, ding2, ding3, ding4);
        }

        public string GetTensionWarnData(int pageIndex, int pageSize, int id)
        {
            string json = ZhangLa1BLL.GetWarnTableData(pageIndex, pageSize, id);
            return json;
        }



        public string GetTensionCurveData(int id)
        {
            return ZhangLa1BLL.GetCurveData(id);
        }






        public string GetMudjackData(int pageIndex, int pageSize, int id)
        {
            return YaJiang1BLL.GetTableData(pageIndex, pageSize, id);
        }
        public void GetMudjackDetailData()
        {

        }


        public string GetYangHuData(int pageIndex, int pageSize, string itemName)
        {
            return Yanghu_StatDataBLL.GetTableData(pageIndex, pageSize, itemName);
        }


        public string GetWenDuActualTimeData(int projectId)
        {
            return Envionment_ActualTimeBLL.GetActualTimeData(projectId);
        }

        public string GetWenDuChartData(int projectId, string startTime, string endTime)
        {
            return Envionment_StatisticBLL.GetChartData(projectId, startTime, endTime);
        }
        //System.Data.Entity.Spatial.DbGeometry
        public string GetDeviceList()
        {
            List<Tb_MixingPlant_Device> mixingPlantDevices = Tb_MixingPlant_DeviceDAL.LoadEntities(r => true).ToList();
            List<Tb_Lab_Department> labDevices = Tb_Lab_DepartmentDAL.LoadEntities(r => true).ToList();
            List<Envionment_DeviceProject> environmentDevices = Envionment_DeviceProjectDAL.LoadEntities(r => true).ToList();
            List<Tb_ZhangLa1_Device> zhangLaDevices = Tb_ZhangLa1_DeviceDAL.LoadEntities(r => true).ToList();
            List<Tb_YaJiang1_Device> yajiangDevices = Tb_YaJiang1_DeviceDAL.LoadEntities(r => true).ToList();


            Dictionary<string, List<WeiXin_API_Device>> dict = new Dictionary<string, List<WeiXin_API_Device>>();

            List<WeiXin_API_Device> mps = new List<WeiXin_API_Device>();
            foreach (Tb_MixingPlant_Device mp in mixingPlantDevices)
            {
                if (mp.Name != null && mp.Name != "")
                {
                    WeiXin_API_Device device = new WeiXin_API_Device();
                    device.Id = mp.Id;
                    device.Name = mp.Name;
                    device.Type = "MixingPlant";
                    device.Code = mp.DeviceCode;
                    if (mp.geom != null)
                    {
                        string[] texts = mp.geom.AsText().Split(' ');
                        device.Longitude = float.Parse(texts[1].Replace("(", ""));
                        device.Latitude = float.Parse(texts[2].Replace(")", ""));
                    }
                    mps.Add(device);
                }
            }

            List<WeiXin_API_Device> labs = new List<WeiXin_API_Device>();
            foreach (Tb_Lab_Department lab in labDevices)
            {
                if (lab.DepartmentName != null && lab.DepartmentName != "")
                {
                    WeiXin_API_Device device = new WeiXin_API_Device();
                    device.Id = lab.Id;
                    device.Name = lab.DepartmentName;
                    device.Type = "Lab";
                    device.Code = lab.DepartmentCode;
                    if (lab.geom != null)
                    {
                        string[] texts = lab.geom.AsText().Split(' ');
                        device.Longitude = float.Parse(texts[1].Replace("(", ""));
                        device.Latitude = float.Parse(texts[2].Replace(")", ""));
                    }
                    labs.Add(device);
                }
            }

            List<WeiXin_API_Device> envs = new List<WeiXin_API_Device>();
            foreach (Envionment_DeviceProject env in environmentDevices)
            {
                if (env.DeviceName != null && env.DeviceName != "")
                {
                    WeiXin_API_Device device = new WeiXin_API_Device();
                    device.Id = env.Id;
                    device.Name = env.DeviceName;
                    device.Type = "WenShiDu";
                    device.Code = env.DeviceCode;
                    if (env.geom != null)
                    {
                        string[] texts = env.geom.AsText().Split(' ');
                        device.Longitude = float.Parse(texts[1].Replace("(", ""));
                        device.Latitude = float.Parse(texts[2].Replace(")", ""));
                    }
                    envs.Add(device);
                }
            }

            List<WeiXin_API_Device> zhanglas = new List<WeiXin_API_Device>();
            foreach (Tb_ZhangLa1_Device zhangla in zhangLaDevices)
            {
                if (zhangla.DeviceName != null && zhangla.DeviceName != "")
                {
                    WeiXin_API_Device device = new WeiXin_API_Device();
                    device.Name = zhangla.DeviceName;
                    device.Type = "ZhangLa";
                    device.Code = zhangla.Id;
                    if (zhangla.geom != null)
                    {
                        string[] texts = zhangla.geom.AsText().Split(' ');
                        device.Longitude = float.Parse(texts[1].Replace("(", ""));
                        device.Latitude = float.Parse(texts[2].Replace(")", ""));
                    }
                    zhanglas.Add(device);
                }
            }
            List<WeiXin_API_Device> yajiangs = new List<WeiXin_API_Device>();
            foreach (Tb_YaJiang1_Device yajiang in yajiangDevices)
            {
                if (yajiang.DeviceName != null && yajiang.DeviceName != "")
                {
                    WeiXin_API_Device device = new WeiXin_API_Device();
                    device.Name = yajiang.DeviceName;
                    device.Type = "YaJiang";
                    device.Code = yajiang.Id;
                    if (yajiang.geom != null)
                    {
                        string[] texts = yajiang.geom.AsText().Split(' ');
                        device.Longitude = float.Parse(texts[1].Replace("(", ""));
                        device.Latitude = float.Parse(texts[2].Replace(")", ""));
                    }
                    yajiangs.Add(device);
                }
            }


            dict.Add("MixingPlant", mps);
            dict.Add("Lab", labs);
            dict.Add("WenShiDu", envs);
            dict.Add("ZhangLa", zhanglas);
            dict.Add("YaJiang", yajiangs);
            string json = JsonConvert.SerializeObject(dict);
            return json;
        }

        public string SaveLocationData(int id,string deviceCode, string type, float longitude, float latitude)
        {
            
            System.Data.Entity.Spatial.DbGeometry position = System.Data.Entity.Spatial.DbGeometry.FromText("POINT(" + longitude + " " + latitude + ")");
           
            if (type.Equals("MixingPlant"))
            {
                Tb_MixingPlant_Device mixingPlantDevice = Tb_MixingPlant_DeviceDAL.LoadEntities(r => r.Id == id).FirstOrDefault();
                mixingPlantDevice.geom = position;
            }
            else if (type.Equals("Lab"))
            {
                Tb_Lab_Department lab = Tb_Lab_DepartmentDAL.LoadEntities(r => r.Id == id).FirstOrDefault();
                lab.geom = position;
            }
            else if (type.Equals("WenShiDu"))
            {
                Envionment_DeviceProject env = Envionment_DeviceProjectDAL.LoadEntities(r => r.Id == id).FirstOrDefault();
                env.geom = position;
            }
            else if (type.Equals("ZhangLa"))
            {
                Tb_ZhangLa1_Device zhangLa = Tb_ZhangLa1_DeviceDAL.LoadEntities(r => r.Id == deviceCode).FirstOrDefault();
                zhangLa.geom = position;
            }
            else if (type.Equals("YaJiang"))
            {
                Tb_YaJiang1_Device yajiang = Tb_YaJiang1_DeviceDAL.LoadEntities(r => r.Id == deviceCode).FirstOrDefault();
                yajiang.geom = position;
            }
            
            if (SaveChanges())
            {
                return "success";
            }
            else
            {
                return "error";
            }
        }
    }
}
