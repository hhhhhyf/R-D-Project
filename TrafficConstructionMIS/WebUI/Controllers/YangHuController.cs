using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;

namespace WebUI.Controllers
{
    public class YangHuController : Controller
    {
        //
        // GET: /YangHu/
        IYanghu_StatDataBLL Yanghu_StatDataBLL;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTableData(int page, int rows, string itemName)
        {
            string json = Yanghu_StatDataBLL.GetTableData(page, rows, itemName);
            return Content(json);
        }

        public ActionResult GetTableSearchData(int page, int rows, string itemName, string liangNo, string deviceId,string startTime, string endTime)
        {
            string json = Yanghu_StatDataBLL.GetTableSearchData(page, rows, itemName, liangNo, deviceId,startTime, endTime);
            return Content(json);
        }

    }
}
