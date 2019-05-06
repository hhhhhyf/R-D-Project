using DbModel;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using System.Linq.Expressions;
using System.Web;
namespace BLL
{
    public class MainBLL : IMainBLL
    {
        ITb_Main_MenuDAL Tb_Main_MenuDAL { get; set; }

        public void GetMenu(List<Tb_Main_Menu> parentMenu, List<List<Tb_Main_Menu>> childMenu)
        {
            int total;
            Tb_Login user = (Tb_Login)HttpContext.Current.Session["TCMIS_User"];
            parentMenu.AddRange(Tb_Main_MenuDAL.LoadPageEntities<int>
                (1, Int16.MaxValue, out total, r => r.ParentId == -1, r => (int)r.Position, true).ToList());
            foreach (Tb_Main_Menu item in parentMenu)
            {
                childMenu.Add(Tb_Main_MenuDAL.LoadPageEntities<int>
                (1, Int16.MaxValue, out total, r => r.ParentId == item.Id, r => (int)r.Position, true).ToList());
            }
        }
    }
}
