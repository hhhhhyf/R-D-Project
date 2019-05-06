﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using DbModel;

namespace DAL
{
    /// <summary>
    /// 负责创建EF数据操作上下文实例，必须保证线程内唯一.
    /// </summary>
    public class DBContextFactory
    {
        //线程槽
        public static YuHuan_TrafficConstructionMISEntities CreateDbContext()
        {
            YuHuan_TrafficConstructionMISEntities dbContext = (YuHuan_TrafficConstructionMISEntities)CallContext.GetData("dbContext");
            if (dbContext == null)
            {
                dbContext = new YuHuan_TrafficConstructionMISEntities();
                CallContext.SetData("dbContext", dbContext);
            }
            return dbContext;
        }
    }
}
