using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;

namespace WebUI.Controllers
{
    public class FlowController : Controller
    {
        //
        // GET: /Flow/
        IFlowBLL FlowBLL { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTotalFlow(int pageId)
        {
            string json = FlowBLL.GetTotalFlow(pageId);
            return Content(json);
        }

        public ActionResult GetDailyFlow(int pageId,string visitTime)
        {
            string json = FlowBLL.GetDailyFlow(pageId, visitTime);
            return Content(json);
        }

        public ActionResult AddPageFlow(int pageId)
        {
             FlowBLL.AddPageFlow(pageId);
             return Content("1");
        }
    }
}
