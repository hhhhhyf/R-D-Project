using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class EnvironmentController : Controller
    {
        //
        // GET: /Environment/

        IEnvionment_ActualTimeBLL Envionment_ActualTimeBLL { get; set; }
        IEnvionment_StatisticBLL Envionment_StatisticBLL { get; set; }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetActualTimeData(int projectId)
        {
            string json = Envionment_ActualTimeBLL.GetActualTimeData(projectId);
            return Content(json);
        }

        public ActionResult GetChartData(int projectId, string startTime, string endTime)
        {
            string json = Envionment_StatisticBLL.GetChartData(projectId, startTime, endTime);
            return Content(json);
        }

    }
}
