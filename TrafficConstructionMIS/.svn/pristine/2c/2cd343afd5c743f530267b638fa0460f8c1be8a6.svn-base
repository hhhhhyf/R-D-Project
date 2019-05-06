using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using MyTool;
using MyModel;
using DbModel;
using IBLL;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace BLL
{
    public class ZhangLaBLL : BaseBLL, IZhangLaBLL
    {
        ITb_ZhangLa_CurrentMonitorDAL Tb_ZhangLa_CurrentMonitorDAL { get; set; }
        ITb_ZhangLa_CurveDAL Tb_ZhangLa_CurveDAL { get; set; }
        ITb_ZhangLa_DataDAL Tb_ZhangLa_DataDAL { get; set; }
        ITb_ZhangLa_ProjectInfoDAL Tb_ZhangLa_ProjectInfoDAL { get; set; }
        ITb_ZhangLa_ProjectDAL Tb_ZhangLa_ProjectDAL { get; set; }
        private Nullable<decimal> GetExtendError(Tb_ZhangLa_Data data)
        {
            Nullable<decimal> error = 0;
            Nullable<decimal> real = GetRealExtend(data);
            Nullable<decimal> lilun = GetExtend(data);
            if (lilun != null)
            {
                error = (real - lilun) / lilun;
                error = Math.Round((decimal)error * 100, 2);
            }
            return error;
        }
        
        private Nullable<decimal> GetExtend(Tb_ZhangLa_Data data)
        {
            Nullable<decimal> extend = 0;
            if (data.Gang1Flag == 1)
            {
                extend += PassDecimal(data.Extend);
            }
            if (data.Gang2Flag == 1)
            {
                extend += PassDecimal(data.Extend2);
            }
            if (data.Gang3Flag == 1)
            {
                extend += PassDecimal(data.Extend3);
            }
            if (data.Gang4Flag == 1)
            {
                extend += PassDecimal(data.Extend4);
            }
            return extend;
        }
        private Nullable<decimal> GetRealExtend(Tb_ZhangLa_Data data)
        {
            Nullable<decimal> extend = 0;
            if (data.Gang1Flag == 1)
            {
                extend += PassDecimal(data.RealExtend1);
            }
            if (data.Gang2Flag == 1)
            {
                extend += PassDecimal(data.RealExtend2);
            }
            if (data.Gang3Flag == 1)
            {
                extend += PassDecimal(data.RealExtend3);
            }
            if (data.Gang4Flag == 1)
            {
                extend += PassDecimal(data.RealExtend4);
            }
            return extend;
        }
        private Nullable<int> GetBaoYaTime(Tb_ZhangLa_Data data)
        {
            Nullable<int> time = 0;
            if (data.Gang1Flag == 1)
            {
                time = data.BaoYaTime1;
            }
            else if (data.Gang2Flag == 1)
            {
                time = data.BaoYaTime2;
            }
            else if (data.Gang3Flag == 1)
            {
                time = data.BaoYaTime3;
            }
            else if (data.Gang4Flag == 1)
            {
                time = data.BaoYaTime4;
            }
            return time;
        }
        public string GetTableData(int pageIndex, int pageSize, int id)
        {
            int total = 0;
            List<Tb_ZhangLa_Project> project = Tb_ZhangLa_ProjectDAL.LoadEntities(r => r.Id == id).ToList();
            string projectName = project[0].ProjectName;
            string dotId = project[0].DotId;
            List<ZhangLa_Table> rows = new List<ZhangLa_Table>();
            List<Tb_ZhangLa_Data> datas = Tb_ZhangLa_DataDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total,
            r => r.Tb_ZhangLa_ProjectInfo.ProjectName == projectName && r.Tb_ZhangLa_ProjectInfo.DotId == dotId, r => (DateTime)r.ZLTime, false).ToList();
            foreach (Tb_ZhangLa_Data data in datas)
            {
                ZhangLa_Table row = new ZhangLa_Table();
                row.LiangStr = data.LiangStr;
                row.KongEx = data.KongEx;
                row.LiangType = data.LiangType;
                row.BaoYaTime = GetBaoYaTime(data);
                row.ErrorRate = GetExtendError(data);
                row.Extend = GetExtend(data);
                row.RealExtend = GetRealExtend(data);
                row.Per100Press = data.Per100Press;
                row.Id = data.Id;
                row.ZLTime = data.ZLTime.ToString();
                row.ZLType = data.ZLType;
                row.Kong = data.Kong;
                rows.Add(row);
            }
            JqGridTable<ZhangLa_Table> jqGridTable = new JqGridTable<ZhangLa_Table>();
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }

        public string GetTableSearchData(int pageIndex, int pageSize, int id,
            string liangStr, string startTime, string endTime)
        {
            int total = 0;
            List<Tb_ZhangLa_Project> project = Tb_ZhangLa_ProjectDAL.LoadEntities(r => r.Id == id).ToList();
            string projectName = project[0].ProjectName;
            string dotId = project[0].DotId;
            List<ZhangLa_Table> rows = new List<ZhangLa_Table>();
            Expression<Func<Tb_ZhangLa_Data, bool>> pieceWhere = t => t.Tb_ZhangLa_ProjectInfo.ProjectName == projectName &&t.Tb_ZhangLa_ProjectInfo.DotId == dotId;
            if (!"".Equals(startTime) && startTime != null)
            {
                DateTime start = Convert.ToDateTime(startTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.ZLTime >= start);
            }
            if (!"".Equals(endTime) && endTime != null)
            {
                DateTime end = Convert.ToDateTime(endTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.ZLTime <= end);
            }
            if (!"".Equals(liangStr) && liangStr != null)
            {
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.LiangStr == liangStr);
            }
            List<Tb_ZhangLa_Data> datas = Tb_ZhangLa_DataDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total,
                pieceWhere, r => (DateTime)r.ZLTime, false).ToList();
            foreach (Tb_ZhangLa_Data data in datas)
            {
                ZhangLa_Table row = new ZhangLa_Table();
                row.LiangStr = data.LiangStr;
                row.KongEx = data.KongEx;
                row.LiangType = data.LiangType;
                row.BaoYaTime = GetBaoYaTime(data);
                row.ErrorRate = GetExtendError(data);
                row.Extend = GetExtend(data);
                row.RealExtend = GetRealExtend(data);
                row.Per100Press = data.Per100Press;
                row.Id = data.Id;
                row.ZLTime = data.ZLTime.ToString();
                row.ZLType = data.ZLType;
                row.Kong = data.Kong;
                rows.Add(row);
            }
            
            JqGridTable<ZhangLa_Table> jqGridTable = new JqGridTable<ZhangLa_Table>();
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }

        public void GetDetailData(int id, ZhangLa_DetailData_BaseInfo baseInfo, ZhangLa_DetailData_Ding ding1,
            ZhangLa_DetailData_Ding ding2, ZhangLa_DetailData_Ding ding3, ZhangLa_DetailData_Ding ding4)
        {
            List<Tb_ZhangLa_Data> data = Tb_ZhangLa_DataDAL.LoadEntities(r => r.Id == id).ToList();

            baseInfo.ErrorRate = GetExtendError(data[0]);
            baseInfo.InitPress = data[0].InitPress;
            baseInfo.Kong = data[0].Kong;
            baseInfo.KongEx = data[0].KongEx;
            baseInfo.LiangStr = data[0].LiangStr;
            baseInfo.LiangType = data[0].LiangType;
            baseInfo.Per100Press = data[0].Per100Press;
            baseInfo.ZLTime = data[0].ZLTime.ToString();
            baseInfo.ZLType = data[0].ZLType;
            baseInfo.HuiSuoL = data[0].HuiSuoL;
            baseInfo.Extend =GetExtend(data[0]);
            baseInfo.ErrorRate = GetExtendError(data[0]);
            baseInfo.RealExtend = GetRealExtend(data[0]);
            baseInfo.BaoYaTime = GetBaoYaTime(data[0]);
            if (data[0].Gang1Flag == 1)
            {
                ding1.BaoYaTime = data[0].BaoYaTime1;
                ding1.Extend = data[0].Extend;
                ding1.ExtendError = GetError((decimal)PassDecimal(data[0].Extend),
                    (decimal)PassDecimal(data[0].RealExtend1));
                ding1.Flag = 1;
                ding1.Per100Extend = data[0].Per100Extend1;
                ding1.Per100Mpa = data[0].Per100Mpa1;
                ding1.Per20Extend = data[0].Per20Extend1;
                ding1.Per20Mpa = data[0].Per20Mpa1;
                ding1.Per50Extend = data[0].Per50Extend1;
                ding1.Per50Mpa = data[0].Per50Mpa1;
                ding1.Per10Extend = data[0].Per10Extend1;
                ding1.Per10Mpa = data[0].Per10Mpa1;
                ding1.RealExtend = data[0].RealExtend1;
            }
            else
            {
                ding1.Flag = 0;
            }

            if (data[0].Gang2Flag == 1)
            {
                ding2.BaoYaTime = data[0].BaoYaTime2;
                ding2.Extend = data[0].Extend2;
                ding2.ExtendError = GetError((decimal)PassDecimal(data[0].Extend2),
                    (decimal)PassDecimal(data[0].RealExtend2));
                ding2.Flag = 1;
                ding2.Per100Extend = data[0].Per100Extend2;
                ding2.Per100Mpa = data[0].Per100Mpa2;
                ding2.Per50Extend = data[0].Per50Extend2;
                ding2.Per50Mpa = data[0].Per50Mpa2;
                ding2.Per20Extend = data[0].Per20Extend2;
                ding2.Per20Mpa = data[0].Per20Mpa2;
                ding2.Per10Extend = data[0].Per10Extend2;
                ding2.Per10Mpa = data[0].Per10Mpa2;
                ding2.RealExtend = data[0].RealExtend2;
            }
            else
            {
                ding2.Flag = 0;
            }

            if (data[0].Gang3Flag == 1)
            {
                ding3.BaoYaTime = data[0].BaoYaTime3;
                ding3.Extend = data[0].Extend3;
                ding3.ExtendError = GetError((decimal)PassDecimal(data[0].Extend3),
                    (decimal)PassDecimal(data[0].RealExtend3));
                ding3.Flag = 1;
                ding3.Per100Extend = data[0].Per100Extend3;
                ding3.Per100Mpa = data[0].Per100Mpa3;
                ding3.Per50Extend = data[0].Per50Extend3;
                ding3.Per50Mpa = data[0].Per50Mpa3;
                ding3.Per20Extend = data[0].Per20Extend3;
                ding3.Per20Mpa = data[0].Per20Mpa3;
                ding3.Per10Extend = data[0].Per10Extend1;
                ding3.Per10Mpa = data[0].Per10Mpa3;
                ding3.RealExtend = data[0].RealExtend3;
            }
            else
            {
                ding3.Flag = 0;
            }

            if (data[0].Gang4Flag == 1)
            {
                ding4.BaoYaTime = data[0].BaoYaTime4;
                ding4.Extend = data[0].Extend4;
                ding4.ExtendError = GetError((decimal)PassDecimal(data[0].Extend4),
                    (decimal)PassDecimal(data[0].RealExtend4));
                ding4.Flag = 1;
                ding4.Per100Extend = data[0].Per100Extend4;
                ding4.Per100Mpa = data[0].Per100Mpa4;
                ding4.Per50Extend = data[0].Per50Extend4;
                ding4.Per50Mpa = data[0].Per50Mpa4;
                ding4.Per20Extend = data[0].Per20Extend4;
                ding4.Per20Mpa = data[0].Per20Mpa4;
                ding4.Per10Extend = data[0].Per10Extend4;
                ding4.Per10Mpa = data[0].Per10Mpa4;
                ding4.RealExtend = data[0].RealExtend4;
            }
            else
            {
                ding4.Flag = 0;
            }

        }



        public string GetCurveData(int id)
        {
            ZhangLa_CurveChart curve = new ZhangLa_CurveChart();
            List<Tb_ZhangLa_Data> data = Tb_ZhangLa_DataDAL.LoadEntities(r => r.Id == id).ToList();
            List<Tb_ZhangLa_Curve> curvedatas = data[0].Tb_ZhangLa_Curve.ToList();
            if (curvedatas.Count != 0)
            {
                List<ZhangLa_QuXianDataAry> lists = JsonConvert.DeserializeObject<List<ZhangLa_QuXianDataAry>>(curvedatas[curvedatas.Count - 1].DataAry);
                lists = lists.OrderBy(r => DateTime.Parse(r.RecordTime)).ToList();
                foreach (ZhangLa_QuXianDataAry quxian in lists)
                {
                    curve.DateTime.Add(quxian.RecordTime);
                    if (data[0].Gang1Flag == 1)
                    {
                        curve.Gang1Flag = 1;
                        curve.Kn1.Add( Math.Round(quxian.Gang1Press, 2));
                        curve.Extend1.Add( Math.Round(quxian.Gang1Extend, 2));
                    }

                    if (data[0].Gang2Flag == 1)
                    {
                        curve.Gang2Flag = 1;
                        curve.Kn2.Add( Math.Round(quxian.Gang2Press, 2));
                        curve.Extend2.Add( Math.Round(quxian.Gang2Extend, 2));
                    }
                    if (data[0].Gang3Flag == 1)
                    {
                        curve.Gang3Flag = 1;
                        curve.Kn3.Add( Math.Round(quxian.Gang3Press, 2));
                        curve.Extend3.Add( Math.Round(quxian.Gang3Extend, 2));
                    }
                    if (data[0].Gang4Flag == 1)
                    {
                        curve.Gang4Flag = 1;
                        curve.Kn4.Add( Math.Round(quxian.Gang4Press, 2));
                        curve.Extend4.Add( Math.Round(quxian.Gang4Extend, 2));
                    }

                    
                }
            }
            string json = JsonConvert.SerializeObject(curve);
            return json;
        }
        
        private Nullable<decimal> GetError(decimal lilun, decimal real)
        {
            decimal error = 0;
            if (lilun != 0)
            {
                error = (real - lilun) / lilun;
                error = Math.Round((decimal)error * 100, 2);
            }
            return error;
        }
        
        private Nullable<decimal> PassDecimal(Nullable<decimal> t)
        {
            if (t == null) return 0;
            return t;
        }
        
        private int PassInt(Nullable<int> t)
        {
            if (t == null) return 0;
            return (int)t;
        }


    }
}
