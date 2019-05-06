using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;

namespace WebUI.Controllers
{
    /// <summary>
    /// 潘承瑞 旋喷机实时数据——控制器
    /// </summary>
    public class XuanPenJi_StatisticController : Controller
    {
        //
        // GET: /XuanPenJi_Stat/
        IXuanPenJi_StatisticBLL XuanPenJi_StatisticBLL { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChartPanel()
        {
            return View();
        }


        //得到高压旋喷机总表格数据
        public ActionResult GetTableData(int page, int rows)
        {
            string json = XuanPenJi_StatisticBLL.GetTableData(page, rows);
            return Content(json);
            // return Content("");
        }

        //得到高压旋喷机搜索表格数据
        public ActionResult GetTableSearchData(int page, int rows, string projectName,
            string pileSite, string deviceCode, string startTime, string endTime)
        {
            string json = XuanPenJi_StatisticBLL.GetTableSearchData(page, rows, projectName, pileSite,
                deviceCode, startTime, endTime);
            return Content(json);
        }
    }
}
