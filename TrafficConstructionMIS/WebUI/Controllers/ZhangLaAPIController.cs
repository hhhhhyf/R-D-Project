using IBLL;
using log4net;
using MyTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ZhangLaAPIController : Controller
    {
        //
        // GET: /ZhangLaAPI/
        IZhangLaAPIBLL ZhangLaAPIBLL { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BiaoTouInfo(string data)
        {
            ILog logger = LogManager.GetLogger("logZhangLa");
            logger.Info(Base64.Decode(data));
            string json = ZhangLaAPIBLL.SaveBiaoTouInfo(data);
            return Content(json);
        }

        public ActionResult ZhangLaData(string data)
        {
            ILog logger = LogManager.GetLogger("logZhangLa");
            logger.Info(Base64.Decode(data));
            string json = ZhangLaAPIBLL.SaveZhangLaData(data);
            return Content(json);
        }

        public ActionResult ZhangLaQuXian(string data)
        {
            ILog logger = LogManager.GetLogger("logZhangLa");
            logger.Info(Base64.Decode(data));
            string json = ZhangLaAPIBLL.SaveZhangLaQuXian(data);
            return Content(json);
        }

        public ActionResult ZhangLaMonitor(string data)
        {
            ILog logger = LogManager.GetLogger("logZhangLa");
            logger.Info(Base64.Decode(data));
            string json = ZhangLaAPIBLL.SaveZhangLaMonitor(data);
            return Content(json);
        }
    }
}
