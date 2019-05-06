using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbModel;
using IDAL;
using IBLL;
using Newtonsoft.Json;
using MyModel;
using MyTool;
using System.Linq.Expressions;

namespace BLL
{
    /// <summary>
    /// 潘承瑞 旋喷机实时数据——业务逻辑层
    /// </summary>
    public class XuanPenJi_ActualTimeBLL:BaseBLL,IXuanPenJi_ActualTimeBLL
    {
        IGyxpDAL GyxpDAL{ get; set; }

        /// <summary>
        /// 显示所有数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public string GetTableData(int pageIndex,int pageSize)
        {
            int total=0;
            JqGridTable<XuanPenJi_ActualTimeTable> jqGridTable = new JqGridTable<XuanPenJi_ActualTimeTable>();
            List<XuanPenJi_ActualTimeTable> rows = new List<XuanPenJi_ActualTimeTable>();
            List<Gyxp> xuanpens = GyxpDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total, r => true, r => (DateTime)r.OperateTime, false).ToList();

            foreach (Gyxp xuanpen in xuanpens)
            {
                XuanPenJi_ActualTimeTable table = new XuanPenJi_ActualTimeTable();
                table.ProjectName = xuanpen.ProjectName;
                table.PileSite = xuanpen.PileSite;
                table.DeviceCode = xuanpen.DeviceCode;
                if (xuanpen.Flow != null) table.Flow = (float)xuanpen.Flow;
                if (xuanpen.Pressure != null) table.Pressure = (float)xuanpen.Pressure;
                table.OperateTime = xuanpen.OperateTime.ToString();
                table.Id = xuanpen.Id;
                table.Flag = xuanpen.Flag;

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
        public string GetTableSearchData(int pageIndex, int pageSize, string projectName,
            string pileSite, string deviceCode, string startTime, string endTime)
        {
            int total = 0;
            JqGridTable<XuanPenJi_ActualTimeTable> jqGridTable=new JqGridTable<XuanPenJi_ActualTimeTable>();
            List<XuanPenJi_ActualTimeTable> rows=new List<XuanPenJi_ActualTimeTable>();

            Expression<Func<Gyxp,bool>> pieceWhere=t=>true;
            if(!"".Equals(startTime) && startTime != null)
            {
                DateTime operateTimeStart=Convert.ToDateTime(startTime);
                pieceWhere=PredicateExtensions.And(pieceWhere,r=>r.OperateTime>=operateTimeStart);
            }
            if(!"".Equals(endTime)&&endTime!=null)
            {
                DateTime operateTimeEnd=Convert.ToDateTime(endTime);
                pieceWhere=PredicateExtensions.And(pieceWhere,r=>r.OperateTime<=operateTimeEnd);
            }
            if(!"".Equals(projectName)&&projectName!=null) pieceWhere=PredicateExtensions.And(pieceWhere,r=>r.ProjectName==projectName);
            if (!"".Equals(pileSite) && pileSite != null) pieceWhere = PredicateExtensions.And(pieceWhere, r => r.PileSite == pileSite);
            if (!"".Equals(deviceCode) && deviceCode != null) pieceWhere = PredicateExtensions.And(pieceWhere, r => r.DeviceCode == deviceCode);
            List<Gyxp> pieces = GyxpDAL.LoadPageEntities<DateTime>(pageIndex, pageSize,
                out total, pieceWhere, r => (DateTime)r.OperateTime, false).ToList();
            foreach (Gyxp piece in pieces)
            {
                XuanPenJi_ActualTimeTable table = new XuanPenJi_ActualTimeTable();
                table.ProjectName = piece.ProjectName;
                table.PileSite = piece.PileSite;
                table.DeviceCode = piece.DeviceCode;
                if (piece.Flow != null) table.Flow = (float)piece.Flow;
                if (piece.Pressure != null) table.Pressure = (float)piece.Pressure;
                table.OperateTime = piece.OperateTime.ToString();

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


        
        public string GetActualTimeData()
        {
            int total;
            List<Gyxp> xuanpens = GyxpDAL.LoadPageEntities<DateTime>(1, 20, out total, r => true, r => (DateTime)r.OperateTime, false).ToList();
            //已完成的无效桩编号
            var pileSites = xuanpens.Where(r => r.Flag.Equals("0")).GroupBy(r => r.PileSite).Select(r => new { PileSite = r.Key }).ToList();
            
            List<XuanPenJi_ActualTimeData> datas = new List<XuanPenJi_ActualTimeData>();
            foreach (Gyxp xuanpen in xuanpens)
            {
                bool jump = false;
                for (int i = 0; i < pileSites.Count; i++ )
                {
                    if (xuanpen.PileSite.Equals(pileSites[i].PileSite))
                    {
                        jump = true;
                        break;
                    }
                }
                if (jump) continue;
                bool have = false;
                foreach (XuanPenJi_ActualTimeData data in datas)
                {
                    if (data.PileSite.Equals(xuanpen.PileSite))
                    {
                        have = true;
                        break;
                    }
                }
                if(!have)
                {
                    XuanPenJi_ActualTimeData data = new XuanPenJi_ActualTimeData();
                    data.PileSite = xuanpen.PileSite;
                    data.DeviceCode = xuanpen.DeviceCode;
                    data.ProjectName = xuanpen.ProjectName;
                    data.OperateTime = xuanpen.OperateTime.ToString();
                    if(xuanpen.Pressure != null) data.Pressure = (double)xuanpen.Pressure;
                    if(xuanpen.Flow != null) data.Flow = (double)xuanpen.Flow;
                    
                   // List<Gyxp> gyxps = GyxpDAL.LoadPageEntities<DateTime>(1, Int32.MaxValue, out total, r => r.PileSite.Equals(xuanpen.PileSite), r => (DateTime)r.OperateTime, false).ToList();
                    //gyxps.GroupBy(r => r.PileSite).Select(r => new { PileSite = r.Key,  TotalFlow = r.Sum(i => i.Flow), TotalDust = r.Sum(i => i.Pressure)}).ToList();
                    datas.Add(data);
                }
            }
            return JsonConvert.SerializeObject(datas);
        }
    }
}
