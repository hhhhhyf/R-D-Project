using DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
namespace WebUI.Controllers
{
    public class MainController : Controller
    {
        
        IMainBLL MainBLL { set; get; }
        public ActionResult Index()
        {
            List<Tb_Main_Menu> parentMenu = new List<Tb_Main_Menu>();
            List<List<Tb_Main_Menu>> childMenu = new List<List<Tb_Main_Menu>>();
            MainBLL.GetMenu(parentMenu, childMenu);
            ViewBag.parentMenu = parentMenu;
            ViewBag.childMenu = childMenu;
            ViewBag.user = Session["TCMIS_User"];
            return View();
        }
        public ActionResult Map()
        {
            return View();
        }
        public ActionResult Index1()
        {
            return View();
        }
    }
}
