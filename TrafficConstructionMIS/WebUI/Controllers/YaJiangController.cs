using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using MyModel;

namespace WebUI.Controllers
{
    public class YaJiangController : Controller
    {
        IYaJiangBLL YaJiangBLL { get; set; }
        //
        // GET: /YaJiang/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTableData(int page, int rows, int id)             //jqgrid自动传的参数：page(页号)，rows(一页的行数)，这里的参数名称要和前端一致，所以一定要按照jqgrid的规定写
        {
            string json = YaJiangBLL.GetTableData(page, rows, id);
            return Content(json);
        }

        public ActionResult GetTableSearchData(int page, int rows, int id, string liangStr, string startTime, string endTime)
        {
            string json = YaJiangBLL.GetTableSearchData(page, rows, id, liangStr, startTime, endTime);
            return Content(json);
        }

        public ActionResult GetDetailChart(int id)
        {
            YaJiang_DetailChart baseInfo = new YaJiang_DetailChart();
            YaJiangBLL.GetDetailChart(id, baseInfo);
            ViewBag.baseInfo = baseInfo;
            return View();
        }

        public ActionResult CurveChartPanel(int id)
        {
            return View();
        }

        public ActionResult CurveData(int id)
        {
            string json = YaJiangBLL.GetCurveData(id);
            return Content(json);
        }
    }
}
