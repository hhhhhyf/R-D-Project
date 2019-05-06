using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;

namespace WebUI.Controllers
{
    public class MixingPlant_StatisticController : Controller
    {
        //
        // GET: /MixingPlant_Statistic/
        IMixingPlant_StatisticBLL MixingPlant_StatisticBLL { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSelectOption(string deviceIds)
        {
            string json = MixingPlant_StatisticBLL.GetSelectOption(deviceIds);
            return Content(json);
        }

        public ActionResult GetPieStatistic(int deviceId, string month)
        {
            string json = MixingPlant_StatisticBLL.GetDeviceMonthDate(deviceId, month);
            return Content(json);
        }
    }
}
