using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using MyModel;

namespace WebUI.Controllers
{
    public class ZhangLa1Controller : Controller
    {
        //
        // GET: /ZhangLa1/
        IZhangLa1BLL ZhangLa1BLL { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTableData(int page,int rows,int id)
        {
            string json = ZhangLa1BLL.GetTableData(page, rows, id);
            return Content(json);
        }

        public ActionResult GetTableSearchData(int page, int rows, int id, string liangNo, string startTime, string endTime)
        {
            string json = ZhangLa1BLL.GetTableSearchData(page, rows, id, liangNo, startTime, endTime);
            return Content(json);
        }

        public ActionResult DetailDataPanel(int id)
        {
            ZhangLa1_DetailData_BaseInfo baseInfo = new ZhangLa1_DetailData_BaseInfo();
            ZhangLa1_DetailData_Ding ding1 = new ZhangLa1_DetailData_Ding();
            ZhangLa1_DetailData_Ding ding2 = new ZhangLa1_DetailData_Ding();
            ZhangLa1_DetailData_Ding ding3 = new ZhangLa1_DetailData_Ding();
            ZhangLa1_DetailData_Ding ding4 = new ZhangLa1_DetailData_Ding();
            ZhangLa1BLL.GetDetailData(id, baseInfo, ding1, ding2, ding3, ding4);
            ViewBag.baseInfo = baseInfo;
            ViewBag.ding1 = ding1;
            ViewBag.ding2 = ding2;
            ViewBag.ding3 = ding3;
            ViewBag.ding4 = ding4;
            return View();
        }

        public ActionResult CurveData(int id)
        {
            string json = ZhangLa1BLL.GetCurveData(id);
            return Content(json);
        }

        public ActionResult CurveChartPanel(int id)
        {
            return View();
        }
    }
}
