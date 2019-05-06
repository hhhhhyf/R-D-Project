using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using IDAL;
using MyModel;
using DbModel;
using Newtonsoft.Json;
using System.Linq.Expressions;
using MyTool;

namespace BLL
{
    public class Envionment_StatisticBLL : BaseBLL, IEnvionment_StatisticBLL
    {
        IEnvionment_DataDAL Envionment_DataDAL { get; set; }
        IEnvionment_ProjectInfoDAL Envionment_ProjectInfoDAL { get; set; }

        public string GetTableData(int pageIndex, int pageSize,int projectId)
        {
            int total = 0;
            JqGridTable<Envionment_StatisticTable> jqGridTable = new JqGridTable<Envionment_StatisticTable>();
            List<Envionment_StatisticTable> rows = new List<Envionment_StatisticTable>();
            List<Envionment_ProjectInfo> pieces1 = Envionment_ProjectInfoDAL.LoadEntities(r => r.Id == projectId).ToList();
            List<Envionment_Data> pieces2 = Envionment_DataDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total,r=>r.ProjectId==projectId,r=>(DateTime)r.UploadTime,false).ToList();

            foreach (Envionment_Data piece in pieces2)
            {
                Envionment_StatisticTable table = new Envionment_StatisticTable();
                table.Temperature = (float)piece.Temperature;
                table.Humidity = (float)piece.Humidity;
                table.UploadTime = piece.UploadTime.ToString();
                table.ProjectName = pieces1[0].ProjectName;
                rows.Add(table);
            }
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0)  jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;


        }

        public string GetChartData(int projectId, string startTime, string endTime)
        {
            int total = 0;
            List<Envionment_StatisticTable> rows = new List<Envionment_StatisticTable>();
            Expression<Func<Envionment_Data, bool>> dataWhere = t => t.ProjectId == projectId;
            if (!"".Equals(startTime) && startTime != null)
            {
                DateTime bldTimStart = Convert.ToDateTime(startTime);
                dataWhere = PredicateExtensions.And(dataWhere, r => r.UploadTime >= bldTimStart);
            }
            if (!"".Equals(endTime) && endTime != null)
            {
                DateTime bldTimEnd = Convert.ToDateTime(endTime);
                dataWhere = PredicateExtensions.And(dataWhere, r => r.UploadTime < bldTimEnd);
            }
            List<Envionment_Data> datas = Envionment_DataDAL.LoadPageEntities<DateTime>(1, Int16.MaxValue, out total, dataWhere, r => (DateTime)r.UploadTime, true).ToList();

            foreach (Envionment_Data data in datas)
            {
                Envionment_StatisticTable table = new Envionment_StatisticTable();
                table.Temperature = data.Temperature;
                table.Humidity = data.Humidity;
                table.UploadTime = data.UploadTime.ToString();
                table.ProjectName = data.Envionment_ProjectInfo.ProjectName;
                table.DeviceCode = data.DeviceCode;
                rows.Add(table);
            }
            string json = JsonConvert.SerializeObject(rows);
            return json;
        }

        public string GetTableSearchData(int pageIndex, int pageSize, string startTime, string endTime)
        {
            throw new NotImplementedException();
        }
    }
}
