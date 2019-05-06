using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DbModel;
using MyModel;

namespace WebUI.Controllers
{
    public class WeiXin_APIController : Controller
    {
        //
        // GET: /WeiXin_API/
        IWeiXin_APIBLL WeiXin_APIBLL { set; get; }


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MixingPlantDetailPanel()
        {
            return View();
        }
        public ActionResult LabDetailPanel(int Id)
        {
            List<Tb_Lab_TestItem> labTestItem = new List<Tb_Lab_TestItem>();
            List<Tb_Lab_Test> labTest = new List<Tb_Lab_Test>();
            WeiXin_APIBLL.GetLabDetailData(Id, labTestItem, labTest);
            ViewBag.labTestItem = labTestItem;
            ViewBag.labTest = labTest;
            return View();
        }
        public ActionResult TensionDetailPanel(int id)
        {
            ZhangLa1_DetailData_BaseInfo baseInfo = new ZhangLa1_DetailData_BaseInfo();
            ZhangLa1_DetailData_Ding ding1 = new ZhangLa1_DetailData_Ding();
            ZhangLa1_DetailData_Ding ding2 = new ZhangLa1_DetailData_Ding();
            ZhangLa1_DetailData_Ding ding3 = new ZhangLa1_DetailData_Ding();
            ZhangLa1_DetailData_Ding ding4 = new ZhangLa1_DetailData_Ding();
            WeiXin_APIBLL.GetTensionDetailData(id, baseInfo, ding1, ding2, ding3, ding4);
            ViewBag.baseInfo = baseInfo;
            ViewBag.ding1 = ding1;
            ViewBag.ding2 = ding2;
            ViewBag.ding3 = ding3;
            ViewBag.ding4 = ding4;
            return View();
        }
        public ActionResult MudjackDetailPanel(int id)
        {
            return View();
        }

        public ActionResult EnvironmentPanel()
        {
            return View();
        }

        public ActionResult UserLogin(string userName, string password)
        {
            string json = WeiXin_APIBLL.CheckUserLogin(userName, password);
            return Content(json);
        }


        public ActionResult GetMixingPlantData(int pageIndex, int pageSize, string deviceFacld)
        {
            string json = WeiXin_APIBLL.GetMixingPlantData(pageIndex, pageSize, deviceFacld);
            return Content(json);
        }
        public ActionResult GetMixingPlantWarnData(int pageIndex, int pageSize, string deviceFacld)
        {
            string json = WeiXin_APIBLL.GetMixingPlantWarnData(pageIndex, pageSize, deviceFacld);
            return Content(json);
        }
        public ActionResult GetMixingPlantDetailData(string Id, string deviceFacld)
        {
            string json =  WeiXin_APIBLL.GetMixingPlantDetailData(Id, deviceFacld);
            return Content(json);
        }





        public ActionResult GetLabData(int pageIndex, int pageSize, int departmentId)
        {
            string json = WeiXin_APIBLL.GetLabData(pageIndex, pageSize, departmentId);
            return Content(json);
        }
     
        //潘承瑞，试验室曲线
        public ActionResult GetCurveData(int Id)
        {
            string json = WeiXin_APIBLL.GetCurveData(Id);
            return Content(json);
        }



        public ActionResult GetTensionData(int pageIndex, int pageSize, int id)
        {
            string json = WeiXin_APIBLL.GetTensionData(pageIndex, pageSize, id);
            return Content(json);
        }
        public ActionResult GetTensionCurveData(int id) 
        {
            string json = WeiXin_APIBLL.GetTensionCurveData(id);
            return Content(json);
        }
        //张拉
        public ActionResult GetTensionWarnData(int pageIndex, int pageSize, int id)
        {
            string json = WeiXin_APIBLL.GetTensionWarnData(pageIndex, pageSize, id);
            return Content(json);
        }


        //压浆
        public ActionResult GetMudjackData(int pageIndex, int pageSize, int id)
        {
            string json = WeiXin_APIBLL.GetMudjackData(pageIndex, pageSize, id);
            return Content(json);
        }
        //养护
        public ActionResult GetYangHuData(int pageIndex, int pageSize, string itemName)
        {
            string json = WeiXin_APIBLL.GetYangHuData(pageIndex, pageSize, itemName);
            return Content(json);
        }


        public ActionResult GetWenDuActualTimeData(int projectId)
        {
            string json = WeiXin_APIBLL.GetWenDuActualTimeData(projectId);
            return Content(json);
        }
        public ActionResult GetWenDuChartData(int projectId, string startTime, string endTime)
        {
            string json = WeiXin_APIBLL.GetWenDuChartData(projectId, startTime, endTime);
            return Content(json);
        }


        //上传位置信息时设备选择列表
        public ActionResult GetDeviceList()
        {
            string json = WeiXin_APIBLL.GetDeviceList();
            return Content(json);
        }
        //上传位置信息
        public ActionResult UploadLocationData(int id, string deviceCode, string type, float longitude, float latitude)
        {
            string json = WeiXin_APIBLL.SaveLocationData(id, deviceCode, type, longitude, latitude);
            return Content(json);
        }
    }
}
