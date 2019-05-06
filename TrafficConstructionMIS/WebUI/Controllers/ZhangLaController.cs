using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyModel;
namespace WebUI.Controllers
{
    public class ZhangLaController : Controller
    {
        //
        // GET: /ZhangLa/
        IZhangLaBLL ZhangLaBLL { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTableData(int page, int rows, int id)
        {
            string json = ZhangLaBLL.GetTableData(page, rows, id);
            return Content(json);
        }

        public ActionResult GetTableSearchData(int page, int rows, int id,
            string liangStr, string startTime, string endTime)
        {
            string json = ZhangLaBLL.GetTableSearchData(page, rows, id, liangStr, startTime, endTime);
            return Content(json);
        }
        public ActionResult DetailDataPanel(int id)
        {
            ZhangLa_DetailData_BaseInfo baseInfo = new ZhangLa_DetailData_BaseInfo();
            ZhangLa_DetailData_Ding ding1 = new ZhangLa_DetailData_Ding();
            ZhangLa_DetailData_Ding ding2 = new ZhangLa_DetailData_Ding();
            ZhangLa_DetailData_Ding ding3 = new ZhangLa_DetailData_Ding();
            ZhangLa_DetailData_Ding ding4 = new ZhangLa_DetailData_Ding();
            ZhangLaBLL.GetDetailData(id, baseInfo, ding1, ding2, ding3, ding4);
            ViewBag.baseInfo = baseInfo;
            ViewBag.ding1 = ding1;
            ViewBag.ding2 = ding2;
            ViewBag.ding3 = ding3;
            ViewBag.ding4 = ding4;
            return View();
        }
        public ActionResult CurveData(int id)
        {
            string json = ZhangLaBLL.GetCurveData(id);
            return Content(json);
        }


        public ActionResult CurveChartPanel(int id)
        {
            return View();
        }
    }
}
