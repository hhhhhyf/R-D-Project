using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class Envionment_StatisticController : Controller
    {
        //
        // GET: /Envionment_Statistic/
        IEnvionment_StatisticBLL Envionment_StatisticBLL { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetChartData(int projectId, string startTime, string endTime)
        {
            string json = Envionment_StatisticBLL.GetChartData(projectId, startTime, endTime);
            return Content(json);
        }

    }
}
