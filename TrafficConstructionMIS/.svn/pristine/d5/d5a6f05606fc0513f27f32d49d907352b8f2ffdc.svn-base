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
    public class YaJiangAPIController : Controller
    {
        //
        // GET: /YaJiangAPI/
        IYaJiangAPIBLL YaJiangAPIBLL { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BiaoTouInfo(string data)
        {
            ILog logger = LogManager.GetLogger("logYaJiang");
            logger.Info(Base64.Decode(data));
            string json = YaJiangAPIBLL.SaveBiaoTouInfo(data);
            return Content(json);
        }

        public ActionResult YaJiangData(string data)
        {
            ILog logger = LogManager.GetLogger("logYaJiang");
            logger.Info(Base64.Decode(data));
            string json = YaJiangAPIBLL.SaveYaJiangData(data);
            return Content(json);
        }

        public ActionResult YaJiangQuXian(string data)
        {
            ILog logger = LogManager.GetLogger("logYaJiang");
            logger.Info(Base64.Decode(data));
            string json = YaJiangAPIBLL.SaveYaJiangQuXian(data);
            return Content(json);
        }

        public ActionResult YaJiangMonitor(string data)
        {
            ILog logger = LogManager.GetLogger("logYaJiang");
            logger.Info(Base64.Decode(data));
            string json = YaJiangAPIBLL.SaveYaJiangMonitor(data);
            return Content(json);
        }

    }
}
