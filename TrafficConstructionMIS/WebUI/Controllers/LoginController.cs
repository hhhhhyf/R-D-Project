using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        ILoginBLL LoginBLL { set; get; }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckUserLogin(string userName, string password)
        {
            string json = LoginBLL.CheckUserLogin(userName, password);
            return Content(json);
        }


    }
}
