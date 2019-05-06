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
    /// <summary>
    /// 潘承瑞 旋喷机汇总数据——业务逻辑层
    /// </summary>
    public class XuanPenJi_StatisticBLL:BaseBLL,IXuanPenJi_StatisticBLL
    {
        IGyxp_StatDAL Gyxp_StatDAL { get; set; }

        /// <summary>
        /// 显示所有数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public string GetTableData(int pageIndex, int pageSize)
        {
            int total = 0;
            JqGridTable<XuanPenJi_StatisticTable> jqGridTable = new JqGridTable<XuanPenJi_StatisticTable>();
            List<XuanPenJi_StatisticTable> rows = new List<XuanPenJi_StatisticTable>();
            List<Gyxp_Stat> pieces = Gyxp_StatDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total, r => true, r => (DateTime)r.EndTime, false).ToList();

            foreach (Gyxp_Stat piece in pieces)
            {
                XuanPenJi_StatisticTable table = new XuanPenJi_StatisticTable();
                table.ProjectName = piece.ProjectName;
                table.PileSite = piece.PileSite;
                table.DeviceCode = piece.DeviceCode;
                if (piece.Luo != null)  table.Luo = (float)piece.Luo;
                if (piece.TotalTime != null) table.TotalTime = (int)piece.TotalTime/60;
                if (piece.TotalFlow != null) table.TotalFlow = (float)piece.TotalFlow;
                if (piece.TotalDust != null) table.TotalDust = (float)piece.TotalDust;
                table.EndTime = piece.EndTime.ToString();
                table.Id = (int)piece.Id;

                rows.Add(table);
            }

            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }

        /// <summary>
        /// 根据检索条件展示相应的数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="projectName"></param>
        /// <param name="pileSite"></param>
        /// <param name="deviceCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public string GetTableSearchData(int pageIndex, int pageSize, string projectName, string pileSite, string deviceCode, string startTime, string endTime)
        {
            int total = 0;
            JqGridTable<XuanPenJi_StatisticTable> jqGridTable = new JqGridTable<XuanPenJi_StatisticTable>();
            List<XuanPenJi_StatisticTable> rows = new List<XuanPenJi_StatisticTable>();

            Expression<Func<Gyxp_Stat, bool>> pieceWhere = t => true;
            if (!"".Equals(startTime) && startTime != null)
            {
                DateTime operateTimeStart = Convert.ToDateTime(startTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.EndTime >= operateTimeStart);
            }
            if (!"".Equals(endTime) && endTime != null)
            {
                DateTime operateTimeEnd = Convert.ToDateTime(endTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.EndTime <= operateTimeEnd);
            }
            if (!"".Equals(projectName) && projectName != null) pieceWhere = PredicateExtensions.And(pieceWhere, r => r.ProjectName == projectName);
            if (!"".Equals(pileSite) && pileSite != null) pieceWhere = PredicateExtensions.And(pieceWhere, r => r.PileSite == pileSite);
            if (!"".Equals(deviceCode) && deviceCode != null) pieceWhere = PredicateExtensions.And(pieceWhere, r => r.DeviceCode == deviceCode);
            List<Gyxp_Stat> pieces = Gyxp_StatDAL.LoadPageEntities<DateTime>(pageIndex, pageSize,
                out total, pieceWhere, r => (DateTime)r.EndTime, false).ToList();
            foreach (Gyxp_Stat piece in pieces)
            {
                XuanPenJi_StatisticTable table = new XuanPenJi_StatisticTable();
                table.ProjectName = piece.ProjectName;
                table.PileSite = piece.PileSite;
                table.DeviceCode = piece.DeviceCode;
                if (piece.Luo != null) table.Luo = (float)piece.Luo;
                if (piece.TotalTime != null) table.TotalTime = (int)piece.TotalTime/60;
                if (piece.TotalFlow != null) table.TotalFlow = (float)piece.TotalFlow;
                if (piece.TotalDust != null) table.TotalDust = (float)piece.TotalDust;
                table.EndTime = piece.EndTime.ToString();

                rows.Add(table);
            }
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
