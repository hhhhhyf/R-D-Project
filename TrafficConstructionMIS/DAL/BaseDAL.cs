using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using System.Data.Entity.Core.Objects;
using System.Data;
namespace DAL
{
    public class BaseDAL<T> where T : class,new()
    {
        //  OAEntities Db = new OAEntities();
        
        /// <summary>
        /// 查询过滤
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            DbContext Db = DAL.DBContextFactory.CreateDbContext();
            //MergeOption.OverwriteChanges;
            return Db.Set<T>().Where<T>(whereLambda);//
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="totalCount">数据总条数（输出）</param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            DbContext Db = DAL.DBContextFactory.CreateDbContext();
            IQueryable<T> temp = null;
            if (whereLambda == null) temp = Db.Set<T>();
            else temp = Db.Set<T>().Where<T>(whereLambda);
            totalCount = temp.Count();
            if (isAsc)//升序
            {
                temp = temp.OrderBy<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            DbContext Db = DAL.DBContextFactory.CreateDbContext();
            Db.Entry<T>(entity).State = EntityState.Deleted;
            return true;
            //return Db.SaveChanges() > 0;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool EditEntity(T entity)
        {
            DbContext Db = DAL.DBContextFactory.CreateDbContext();
            Db.Entry<T>(entity).State = EntityState.Modified;
            //return Db.SaveChanges() > 0;
            return true;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            DbContext Db = DAL.DBContextFactory.CreateDbContext();
            Db.Set<T>().Add(entity);
            //Db.SaveChanges();
            return entity;

        }
    }
}
