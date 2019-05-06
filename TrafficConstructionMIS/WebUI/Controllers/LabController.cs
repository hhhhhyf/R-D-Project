using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using DbModel;


namespace WebUI.Controllers
{
    public class LabController : Controller
    {
        //
        // GET: /Lab/
        ILabBLL LabBLL { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSelectOption()
        {
            string json = LabBLL.GetSelectOption();
            return Content(json);
        }

        public ActionResult GetTableData(int page, int rows, int departmentId)
        {
            string json = LabBLL.GetTableData(page, rows, departmentId);
            return Content(json);
        }

        public ActionResult GetTableSearchData(int page, int rows, int departmentId, int testItemId, string testNo, string startTime, string endTime)
        {
            string json = LabBLL.GetTableSearchData(page, rows, departmentId, testItemId, testNo, startTime, endTime);
            return Content(json);
        }

        public ActionResult DetailDataPanel(int Id)
        {
            //string json = LabBLL.GetDetailData(Id);
            //return Content(json);
            List<Tb_Lab_TestItem> labTestItem = new List<Tb_Lab_TestItem>();
            List<Tb_Lab_Test> labTest = new List<Tb_Lab_Test>();
            LabBLL.GetDetailData(Id, labTestItem, labTest);
            ViewBag.labTestItem = labTestItem;
            ViewBag.labTest = labTest;
            return View();

        }

        public ActionResult GetCurveData(int id)
        {
            string json = LabBLL.GetCurveData(id);
            return Content(json);

        }

        //public ActionResult CurveChartPanel(int id)
        //{
        //    return View();
        //}

        //打印
        public void Print(int Id)
        {
            string path=LabBLL.Print(Id);
            string[] strs = path.Split('/');
            Response.ContentType = "application/x-doc-compressed";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + strs[strs.Length - 1]);
            //指定编码 防止中文文件名乱码  
            Response.HeaderEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.TransmitFile(path);
        }


    }
}
