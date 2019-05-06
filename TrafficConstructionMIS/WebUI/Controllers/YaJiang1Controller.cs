using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using MyModel;

namespace WebUI.Controllers
{
    public class YaJiang1Controller : Controller
    {
        //
        // GET: /YaJiang1/
        IYaJiang1BLL YaJiang1BLL { set; get; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTableData(int page, int rows, int id)             //jqgrid自动传的参数：page(页号)，rows(一页的行数)，这里的参数名称要和前端一致，所以一定要按照jqgrid的规定写
        {
            string json = YaJiang1BLL.GetTableData(page, rows, id);
            return Content(json);
        }

        public ActionResult GetTableSearchData(int page, int rows, int id, string liangNo, string startTime, string endTime)
        {
            string json = YaJiang1BLL.GetTableSearchData(page, rows, id, liangNo, startTime, endTime);
            return Content(json);
        }

        /// <summary>
        /// 获取压浆详情面板的第二个面板信息——预制梁信息
        /// </summary>
        /// <param name="id">项目id</param>
        /// <returns></returns>
        public ActionResult GetDetailChart1(int id)
        {
            YaJiang1_BaseInfo1 baseInfo = new YaJiang1_BaseInfo1();
            YaJiang1BLL.GetDetailChart1(id, baseInfo);
            ViewBag.baseInfo = baseInfo;
            return View();
        }

        /// <summary>
        /// 获取压浆详情面板的第三个面板信息——压浆设定值和结果值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetDetailChart2(int id)
        {
            YaJiang1_BaseInfo2 baseInfo = new YaJiang1_BaseInfo2();
            YaJiang1BLL.GetDetailChart2(id, baseInfo);
            ViewBag.baseInfo = baseInfo;
            return View();
        }

        /// <summary>
        /// 获取压浆详情面板的第一个面板信息——压力值-时间曲线图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CurveChartPanel(int id)
        {
            return View();
        }

        public ActionResult GetCurveData(int id)
        {
            string json = YaJiang1BLL.GetCurveData(id);
            return Content(json);
        }
    }
}
