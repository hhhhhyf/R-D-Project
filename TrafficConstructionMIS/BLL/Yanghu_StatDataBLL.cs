using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBLL;
using DbModel;
using IDAL;
using Newtonsoft.Json;
using System.Linq.Expressions;
using MyModel;
using MyTool;
namespace BLL
{
    public class Yanghu_StatDataBLL : BaseBLL, IYanghu_StatDataBLL
    {
        IYanghu_StatDataDAL Yanghu_StatDataDAL;
        public string GetTableData(int pageIndex, int pageSize, string itemName)
        {
            int total = 0;
            List<YangHu_MainTable> rows = new List<YangHu_MainTable>(); 
            List <Yanghu_StatData> datas = Yanghu_StatDataDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total,
                r => r.ItemName == itemName && r.PengCount !=0 && r.PengMinutes != 0, r => (DateTime)r.StartTime, false).ToList();    //外键
            foreach (Yanghu_StatData data in datas)
            {
                YangHu_MainTable row = new YangHu_MainTable();
                row.Id = data.Id;
                row.FactoryName = data.FactoryName;
                row.BeamType = data.BeamType;
                row.BridgePart = data.BridgePart;
                row.ByTimes = data.ByTimes;
                row.PengCount = data.PengCount;
                row.TaiId = data.TaiId;
                row.StartTime = data.StartTime.ToString();
                if (data.PengMinutes != null) row.PengMinutes = Math.Round((double)data.PengMinutes, 2);

                row.DeviceId = data.DeviceId;
                if(data.IsManually == 0) row.RunMethod = "自动";
                if (data.IsManually == 1) row.RunMethod = "手动";
                row.Temperature = data.Temperature;
                row.Humidity = data.Humidity;
                row.Pressure = data.Pressure;
                rows.Add(row);
            }

            JqGridTable<YangHu_MainTable> jqGridTable = new JqGridTable<YangHu_MainTable>();
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }

        public string GetTableSearchData(int pageIndex, int pageSize, string itemName, string liangNo, string deviceId, string startTime, string endTime)
        {
            int total = 0;
            List<YangHu_MainTable> rows = new List<YangHu_MainTable>();
            Expression<Func<Yanghu_StatData, bool>> pieceWhere = t => t.ItemName == itemName&& t.PengCount !=0 && t.PengMinutes != 0;
            if (!"".Equals(liangNo) && liangNo != null)
            {
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.BeamType == liangNo);
            }
            if (!"".Equals(startTime) && startTime != null)
            {
                DateTime start = Convert.ToDateTime(startTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.StartTime >= start);
            }
            if (!"".Equals(endTime) && endTime != null)
            {
                DateTime end = Convert.ToDateTime(endTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.StartTime <= end);
            }

            if (!"".Equals(deviceId) && deviceId != null)
            {
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.DeviceId == deviceId);
            }
            


            List<Yanghu_StatData> datas = Yanghu_StatDataDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total,
                pieceWhere, r => (DateTime)r.StartTime, false).ToList();

            foreach (Yanghu_StatData data in datas)
            {
                YangHu_MainTable row = new YangHu_MainTable();
                row.Id = data.Id;
                row.FactoryName = data.FactoryName;
                row.BeamType = data.BeamType;
                row.BridgePart = data.BridgePart;
                row.ByTimes = data.ByTimes;
                row.PengCount = data.PengCount;
                row.TaiId = data.TaiId;
                row.StartTime = data.StartTime.ToString();
                if (data.PengMinutes != null) row.PengMinutes = Math.Round((double)data.PengMinutes, 2);
                row.DeviceId = data.DeviceId;
                if (data.IsManually == 0) row.RunMethod = "自动";
                if (data.IsManually == 1) row.RunMethod = "手动";
                row.Temperature = data.Temperature;
                row.Humidity = data.Humidity;
                row.Pressure = data.Pressure;
                rows.Add(row);
                rows.Add(row);
            }


            JqGridTable<YangHu_MainTable> jqGridTable = new JqGridTable<YangHu_MainTable>();
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }
    }
}
