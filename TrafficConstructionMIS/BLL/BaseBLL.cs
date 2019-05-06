using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;
using DbModel;
namespace BLL
{
    public class BaseBLL
    {
        public YuHuan_TrafficConstructionMISEntities Db
        {
            get
            {
                return DBContextFactory.CreateDbContext();
            }
        }

        /// <summary>
        /// 一个业务中经常涉及到对多张操作，我们希望链接一次数据库，完成对张表数据的操作。提高性能。 工作单元模式。
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            int count = 0; 
            try 
            { 
                count = Db.SaveChanges();
            }
            catch (Exception e) { }
            return  count> 0;
        }
    }
}
