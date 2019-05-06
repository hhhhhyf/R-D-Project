using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
namespace WebUI.Controllers
{
    /// <summary>
    /// 胥智杰
    /// 搅拌站控制器
    /// </summary>
    public class MixingPlantController : Controller
    {
        //
        // GET: /MixingPlant/
        IMixingPlantBLL MixingPlantBLL { get; set;}
        //主页面
        public ActionResult Index()
        {
            return View();
        }

        //误差图面板
        public ActionResult DeviationChartPanel(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        //材料表面板
        public ActionResult DosagePanel(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        //误差设置面板
        public ActionResult SetPanel(string deviceFacld)
        {
            
            ViewBag.materials = MixingPlantBLL.GetMaterials(deviceFacld);
            return View();
        }

        //得到搅拌表格数据
        public ActionResult GetTableData(int page, int rows, string deviceFacld)
        {
            string json = MixingPlantBLL.GetTableData(page, rows, deviceFacld);
            return Content(json);
        }
        //得到搅拌表搜索数据
        public ActionResult GetTableSearchData(int page, int rows, string deviceFacld, string customer,
            string projectName, string consPos, string startTime, string endTime)
        {
            string json = MixingPlantBLL.GetTableSearchData(page, rows,deviceFacld, customer, projectName, 
                consPos, startTime, endTime);
            return Content(json);
        }


        //得到材料表数据 （误差图的数据也来源于此函数）
        public ActionResult GetDosageTableData(string Id, string deviceFacld)
        {
            string json = MixingPlantBLL.GetDosageTableData(Id, deviceFacld);
            return Content(json);
        }

        public ActionResult SaveErrorSet(string data)
        {
            string result = MixingPlantBLL.SaveErrorSet(data);
            return Content(result);
        }
    }
}
