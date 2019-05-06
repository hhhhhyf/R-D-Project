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
    public class XuanPenJi_ActualTimeController : Controller
    {
        //
        // GET: /XuanPenJi_YuHuan/
        IXuanPenJi_ActualTimeBLL XuanPenJi_ActualTimeBLL { get; set; }

        public ActionResult Index()
        {
            return View();
        }


        //得到高压旋喷机表格数据
        public ActionResult GetTableData(int page, int rows)
        {
            string json = XuanPenJi_ActualTimeBLL.GetTableData(page, rows);
            return Content(json);
        }

        //得到高压旋喷机搜索表格数据
        public ActionResult GetTableSearchData(int page, int rows, string projectName,
            string pileSite, string deviceCode, string startTime, string endTime)
        {
            string json = XuanPenJi_ActualTimeBLL.GetTableSearchData(page, rows, projectName, pileSite,
                deviceCode, startTime, endTime);
            return Content(json);
        }


        public ActionResult GetActualTimeData()
        {
            string json = XuanPenJi_ActualTimeBLL.GetActualTimeData();
            return Content(json);
        }
    }
}
