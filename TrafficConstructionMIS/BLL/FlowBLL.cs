using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq.Expressions;
using DbModel;
using MyModel;
using IDAL;
using IBLL;
using MyTool;


namespace BLL
{
    public class FlowBLL:BaseBLL,IFlowBLL
    {
        ITb_Flow_PageDAL Tb_Flow_PageDAL { get; set; }
        ITb_Flow_StatisticsDAL Tb_Flow_StatisticsDAL { get; set;}
        ITb_Flow_VisitDAL Tb_Flow_VisitDAL{get;set;}
        /// <summary>
        /// 按页面id查询总访问量
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns>MyModel.Flow_Total</returns>
        public string GetTotalFlow(int pageId)
        {
            Flow_Total result = new Flow_Total();      //创建返回信息实例

            List<Tb_Flow_Statistics> pieces = Tb_Flow_StatisticsDAL.LoadEntities(r =>r.PageId==pageId).ToList();
            result.PageName = pieces[0].Tb_Flow_Page.PageName;                        //可以通过外键关联找到主键表中的字段
            result.PageId = (int)pieces[0].PageId;
            result.TotalCount = (int)pieces[0].TotalCount;
            string json = JsonConvert.SerializeObject(result);
            return json;
        }

        /// <summary>
        /// 按页面id和访问日期查询某个页面某一天的访问量
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="visitTime"></param>
        /// <returns>MyModel.Flow_Daily</returns>
        public string GetDailyFlow(int pageId, string visitTime)
        {
            Flow_Daily result = new Flow_Daily();         //创建返回日访问量信息实例

            Expression<Func<Tb_Flow_Visit, bool>> pieceWhere = t => true;
            if (!"".Equals(pageId) && pageId != null) pieceWhere = PredicateExtensions.And(pieceWhere, r => r.PageId == pageId);   //使用委托和Mytool创建多条件查询
            if (!"".Equals(visitTime) && visitTime != null)
            {
                DateTime visit_time = Convert.ToDateTime(visitTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.VisitTime == visit_time);
            }

            List<Tb_Flow_Visit> pieces_visit = Tb_Flow_VisitDAL.LoadEntities(pieceWhere).ToList();
            if (pieces_visit != null)
            {
                result.PageName = pieces_visit[0].Tb_Flow_Page.PageName;
                result.PageId = pieces_visit[0].Tb_Flow_Page.Id;
                result.Count = (int)pieces_visit[0].Count;
                result.VisitTime = Convert.ToString(pieces_visit[0].VisitTime);

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            else
            {
                result.PageName = pieces_visit[0].Tb_Flow_Page.PageName;
                result.PageId = pieces_visit[0].Tb_Flow_Page.Id;
                result.Count = 0;

                string json = JsonConvert.SerializeObject(result);
                return json;
            }
            
           
        }

        /// <summary>
        /// 访问某一页面时，增加该页面的总访问量和当日访问量
        /// </summary>
        /// <param name="pageId"></param>
        public void AddPageFlow(int pageId)
        {
            string visitTime=DateTime.Now.ToShortDateString();                //获取当前系统时间（格式yy-mm-dd)

            List<Tb_Flow_Statistics> piece_Statistics = Tb_Flow_StatisticsDAL.LoadEntities(r => r.Id == pageId).ToList();            //先将总访问量加一
            piece_Statistics[0].TotalCount += 1;
            SaveChanges();                                                                                                                                                         //保存

            Expression<Func<Tb_Flow_Visit, bool>> pieceWhere = t => true;                       //根据id和当前日期，在Tb_Flow_Visit中查询该页面在今天是否已有访问记录
            if (!"".Equals(pageId) && pageId != null)  pieceWhere = PredicateExtensions.And(pieceWhere, r => r.PageId== pageId);
            if (!"".Equals(visitTime) && visitTime != null)
            {
                DateTime visit_time = Convert.ToDateTime(visitTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.VisitTime == visit_time);
            }
            List<Tb_Flow_Visit> piece_visit = Tb_Flow_VisitDAL.LoadEntities(pieceWhere).ToList();

            if (piece_visit.Count!=0)                                                                         //若该页面在今天是否已有访问记录，直接将count+1
            {
                piece_visit[0].Count += 1;
                SaveChanges();
            }
            else                                                                                                    //若该页面在今天没有已有访问记录，则AddEntity(),将count置为1
            {
                Tb_Flow_Visit data = new Tb_Flow_Visit();
                data.PageId = pageId;
                data.Count = 1;
                data.VisitTime = Convert.ToDateTime(visitTime);
                Tb_Flow_Visit newVisit = Tb_Flow_VisitDAL.AddEntity(data);
                SaveChanges();
            }
        }
}
}
