using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using DbModel;
using MyModel;
using Newtonsoft.Json;
using System.Web;

namespace BLL
{
    public class LoginBLL:BaseBLL,ILoginBLL
    {
        ITb_LoginDAL Tb_LoginDAL { get; set; }
 
        public string CheckUserLogin(string userName,string password)
        {
            Tb_Login record = Tb_LoginDAL.LoadEntities(r => r.UserName == userName).ToList().FirstOrDefault();//Tb_Login——DbModel中
            Dictionary<string, string> map = new Dictionary<string, string>();
            if (record == null) map.Add("result", "error");
            else
            {
                if (record.Password == password)
                {
                    HttpContext.Current.Session["TCMIS_User"] = record;
                    map.Add("result", "right");
                    if (record.UserRight == 1)
                    {
                        map.Add("role", "管理员");
                    }
                    else if (record.UserRight == 0)
                    {
                        map.Add("role", "质监站");
                    }
                    else map.Add("role", "用户");
                }
                else
                {
                    map.Add("result", "error");
                }
            }
            string json = JsonConvert.SerializeObject(map);
            return json;
        }

    }
}
