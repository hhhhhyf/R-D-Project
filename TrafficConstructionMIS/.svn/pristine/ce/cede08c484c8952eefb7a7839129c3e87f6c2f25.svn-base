using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class Envionment_ActualTimeController : Controller
    {
        //
        // GET: /Envionment_ActualTime/
        IEnvionment_ActualTimeBLL Envionment_ActualTimeBLL { get; set;}

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetActualTimeData(int projectId)
        {
            string json = Envionment_ActualTimeBLL.GetActualTimeData(projectId);
            return Content(json);
        }

    }
}
