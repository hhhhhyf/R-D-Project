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
    public class YaJiangDataAPIController : Controller
    {
        IYaJiang1APIBLL YaJiang1APIBLL { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult YaJiangData(string data)
        {
            ILog logger = LogManager.GetLogger("logYaJiang");
            logger.Info("YaJiang(Data):" + Base64.Decode(data));
            string json = YaJiang1APIBLL.SaveYaJiangData(data);
            return Content(json);
        }
        public ActionResult YaJiangQuXian(string data)
        {
            ILog logger = LogManager.GetLogger("logYaJiang");
            logger.Info("YaJiang1(QuXian):" + Base64.Decode(data));
            string json = YaJiang1APIBLL.SaveYaJiangQuXian(data);
            return Content(json);
        }

        public ActionResult YaJiangQuXianArray(string data)
        {
            ILog logger = LogManager.GetLogger("logYaJiang");
            logger.Info("YaJiang1(QuXian):" + Base64.Decode(data));
            string json = YaJiang1APIBLL.SaveYaJiangQuXianArray(data);
            return Content(json);
        }

    }
}
