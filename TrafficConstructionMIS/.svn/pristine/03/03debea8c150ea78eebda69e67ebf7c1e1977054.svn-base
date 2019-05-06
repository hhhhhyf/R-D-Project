using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using log4net;
using MyTool;

namespace WebUI.Controllers
{
    public class ZhangLaDataAPIController : Controller
    {
        //
        // GET: /ZhangLaAPI/
        IZhangLa1APIBLL ZhangLa1APIBLL { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ZhangLaData(string data)
        {
            ILog logger = LogManager.GetLogger("logZhangLa");
            logger.Info("ZhangLa1(Data):"+Base64.Decode(data));
            string json = ZhangLa1APIBLL.SaveZhangLaData(data);
            return Content(json);
        }

        public ActionResult ZhangLaQuXian(string data)
        {
            ILog logger = LogManager.GetLogger("logZhangLa");
            logger.Info("ZhangLa1(QuXian):" + Base64.Decode(data));
            string json = ZhangLa1APIBLL.SaveZhangLaQuXian(data);
            return Content(json);
        }

        public ActionResult ZhangLaQuXianArray(string data)
        {
            ILog logger = LogManager.GetLogger("logZhangLa");
            logger.Info("ZhangLa1(QuXianArray):" + Base64.Decode(data));
            string json = ZhangLa1APIBLL.SaveZhangLaQuXianArray(data);
            return Content(json);
        }
    }
}
