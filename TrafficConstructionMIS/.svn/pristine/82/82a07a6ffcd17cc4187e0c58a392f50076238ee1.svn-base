using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbModel;
using IDAL;
using Newtonsoft.Json;
using System.Linq.Expressions;
using IBLL;
using MyModel;
using MyTool;

namespace BLL
{
    public class ZhangLa1BLL:BaseBLL,IZhangLa1BLL
    {
        ITb_ZhangLa1_ProjectDAL Tb_ZhangLa1_ProjectDAL { get; set; }
        ITb_ZhangLa1_DeviceDAL Tb_ZhangLa1_DeviceDAL { get; set; }
        ITb_ZhangLa1_DataDAL Tb_ZhangLa1_DataDAL { get; set; }
        ITb_ZhangLa1_CurveDAL Tb_ZhangLa1_CurveDAL { get; set; }

        private string GetColor(Nullable<decimal> ExtendError, Nullable<int> BaoYaTime)
        {

            if (ExtendError != null && BaoYaTime != null)
            {

                if (Math.Abs((decimal)ExtendError) > 6 || BaoYaTime < 300)
                {
                    return "yellow";
                }
                return "white";
            }
            else
            {
                return "yellow";
            }
        }

        public string GetTableData(int pageIndex, int pageSize, int id)
        {
            int total = 0;
            List<ZhangLa1_Table> rows = new List<ZhangLa1_Table>();
            List<Tb_ZhangLa1_Device> devices = Tb_ZhangLa1_DeviceDAL.LoadEntities(r => r.ProjectId == id).ToList();
            Expression<Func<Tb_ZhangLa1_Data, bool>> pieceWhere=(r=>false);  //因为下面是用or拼接的piecewhere，所以赋初始值为false     
            foreach(Tb_ZhangLa1_Device device in devices)
            {
                pieceWhere=PredicateExtensions.Or(pieceWhere,r=>r.DeviceId==device.Id);   //使用or的piecewhere,实现在单张表中对某个字段的条件筛选的查询
            }
            List<Tb_ZhangLa1_Data> datas = Tb_ZhangLa1_DataDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total,
                pieceWhere, r => (DateTime)r.ZLDate, false).ToList();    
            foreach (Tb_ZhangLa1_Data data in datas)
            {
                ZhangLa1_Table row = new ZhangLa1_Table();
                row.Id = data.Id;
                row.LiangNo = data.LiangNo;
                row.KongNo = data.KongNo;
                row.ZhangLaCount = data.ZhangLaCount;
                row.LiangType = data.LiangType;
                row.ZLType = data.ZLType;
                row.Per100Kn = data.Per100Kn;
                row.Extend = data.Extend;
                row.RealExtend = data.RealExtend;
                row.ExtendError = data.ExtendError;
                row.BaoYaTime = data.BaoYaTime;
                row.ZLDate = data.ZLDate.ToString();
                row.Color = GetColor(data.ExtendError, data.BaoYaTime);
                rows.Add(row);
            }
            JqGridTable<ZhangLa1_Table> jqGridTable = new JqGridTable<ZhangLa1_Table>();
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }

        public string GetWarnTableData(int pageIndex, int pageSize, int id)
        {
            int total = 0;
            List<ZhangLa1_Table> rows = new List<ZhangLa1_Table>();
            List<Tb_ZhangLa1_Device> devices = Tb_ZhangLa1_DeviceDAL.LoadEntities(r => r.ProjectId == id).ToList();
            Expression<Func<Tb_ZhangLa1_Data, bool>> pieceWhere = (r => false);  //因为下面是用or拼接的piecewhere，所以赋初始值为false     
            foreach (Tb_ZhangLa1_Device device in devices)
            {

                pieceWhere = PredicateExtensions.Or(pieceWhere, (r => r.DeviceId == device.Id &&(r.ExtendError < -6||r.ExtendError > 6||r.BaoYaTime < 300)));   //使用or的piecewhere,实现在单张表中对某个字段的条件筛选的查询
            }
            List<Tb_ZhangLa1_Data> datas = Tb_ZhangLa1_DataDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total,
                pieceWhere, r => (DateTime)r.ZLDate, false).ToList();
            foreach (Tb_ZhangLa1_Data data in datas)
            {
                ZhangLa1_Table row = new ZhangLa1_Table();
                row.Id = data.Id;
                row.LiangNo = data.LiangNo;
                row.KongNo = data.KongNo;
                row.ZhangLaCount = data.ZhangLaCount;
                row.LiangType = data.LiangType;
                row.ZLType = data.ZLType;
                row.Per100Kn = data.Per100Kn;
                row.Extend = data.Extend;
                row.RealExtend = data.RealExtend;
                row.ExtendError = data.ExtendError;
                row.BaoYaTime = data.BaoYaTime;
                row.ZLDate = data.ZLDate.ToString();
                row.Color = GetColor(data.ExtendError, data.BaoYaTime);
                rows.Add(row);
            }
            JqGridTable<ZhangLa1_Table> jqGridTable = new JqGridTable<ZhangLa1_Table>();
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }


        public string GetTableSearchData(int pageIndex, int pageSize, int id, string liangNo, string startTime, string endTime)
        {
            int total = 0;
            List<ZhangLa1_Table> rows = new List<ZhangLa1_Table>();
            Expression<Func<Tb_ZhangLa1_Data, bool>> pieceWhere = (r => true);
            pieceWhere = PredicateExtensions.And(pieceWhere, r => r.Tb_ZhangLa1_Device.ProjectId == id); //区别于上面GetTabelData方法，这里直接用外键关系获得某个project对应的多个device在data表中的数据
            if (!"".Equals(liangNo) && liangNo != null)
            {
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.LiangNo == liangNo);
            }
            if (!"".Equals(startTime) && startTime != null)
            {
                DateTime start = Convert.ToDateTime(startTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.ZLDate >= start);
            }
            if (!"".Equals(endTime) && endTime != null)
            {
                DateTime end = Convert.ToDateTime(endTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.ZLDate <= end);
            }
            List<Tb_ZhangLa1_Data> datas=Tb_ZhangLa1_DataDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total,
               pieceWhere, r => (DateTime)r.ZLDate, false).ToList();
            foreach (Tb_ZhangLa1_Data data in datas)
            {
                ZhangLa1_Table row = new ZhangLa1_Table();
                row.Id = data.Id;
                row.LiangNo = data.LiangNo;
                row.KongNo = data.KongNo;
                row.ZhangLaCount = data.ZhangLaCount;
                row.LiangType = data.LiangType;
                row.ZLType = data.ZLType;
                row.Per100Kn = data.Per100Kn;
                row.Extend = data.Extend;
                row.RealExtend = data.RealExtend;
                row.ExtendError = data.ExtendError;
                row.BaoYaTime = data.BaoYaTime;
                row.ZLDate = data.ZLDate.ToString();
                row.Color = GetColor(data.ExtendError, data.BaoYaTime);
                rows.Add(row);
            }
            JqGridTable<ZhangLa1_Table> jqGridTable = new JqGridTable<ZhangLa1_Table>();
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }

        public void GetDetailData(int id,ZhangLa1_DetailData_BaseInfo baseInfo,ZhangLa1_DetailData_Ding ding1,
            ZhangLa1_DetailData_Ding ding2,ZhangLa1_DetailData_Ding ding3,ZhangLa1_DetailData_Ding ding4)
        {
            List<Tb_ZhangLa1_Data> data = Tb_ZhangLa1_DataDAL.LoadEntities(r => r.Id == id).ToList();

            baseInfo.LiangNo = data[0].LiangNo;
            baseInfo.KongNo = data[0].KongNo;
            baseInfo.ZhangLaCount = data[0].ZhangLaCount;
            baseInfo.LiangType = data[0].LiangType;
            baseInfo.ZLType = data[0].ZLType;
            baseInfo.InitKn = data[0].InitKn;
            baseInfo.Per100Kn = data[0].Per100Kn;
            baseInfo.HuiSuo = data[0].HuiSuo;
            baseInfo.Extend = data[0].Extend;
            baseInfo.RealExtend = data[0].RealExtend;
            baseInfo.ExtendError = data[0].ExtendError;
            baseInfo.ZLDate = data[0].ZLDate.ToString();
            baseInfo.BaoYaTime = data[0].BaoYaTime;
            baseInfo.DingHuiSuo = data[0].DingHuiSuo;
            baseInfo.JiaPianHuiSuo = data[0].JiaPianHuiSuo;
            baseInfo.ProjectId = data[0].Tb_ZhangLa1_Device.ProjectId;
            if (data[0].Gang1Flag == 1)
            {
                ding1.Flag = 1;
                ding1.Per10Kn = data[0].Per10Kn1;
                ding1.Per10Extend = data[0].Per10Extend1;
                ding1.Per20Kn = data[0].Per20Kn1;
                ding1.Per20Extend = data[0].Per20Extend1;
                ding1.Per50Kn = data[0].Per50Kn1;
                ding1.Per50Extend = data[0].Per50Extend1;
                ding1.Per100Kn = data[0].Per100Kn1;
                ding1.Per100Extend = data[0].Per100Extend1;
                ding1.Extend = data[0].Extend1;
                ding1.RealExtend = data[0].RealExtend1;
                ding1.ExtendError = data[0].ExtendError1;
            }
            else
            {
                ding1.Flag = 0;
            }
            if (data[0].Gang2Flag == 1)
            {
                ding2.Flag = 1;
                ding2.Per10Kn = data[0].Per10Kn2;
                ding2.Per10Extend = data[0].Per10Extend2;
                ding2.Per20Kn = data[0].Per20Kn2;
                ding2.Per20Extend = data[0].Per20Extend2;
                ding2.Per50Kn = data[0].Per50Kn2;
                ding2.Per50Extend = data[0].Per50Extend2;
                ding2.Per100Kn = data[0].Per100Kn2;
                ding2.Per100Extend = data[0].Per100Extend2;
                ding2.Extend = data[0].Extend2;
                ding2.RealExtend = data[0].RealExtend2;
                ding2.ExtendError = data[0].ExtendError2;
            }
            else
            {
                ding2.Flag = 0;
            }
            if (data[0].Gang3Flag == 1)
            {
                ding3.Flag = 1;
                ding3.Per10Kn = data[0].Per10Kn3;
                ding3.Per10Extend = data[0].Per10Extend3;
                ding3.Per20Kn = data[0].Per20Kn3;
                ding3.Per20Extend = data[0].Per20Extend3;
                ding3.Per50Kn = data[0].Per50Kn3;
                ding3.Per50Extend = data[0].Per50Extend3;
                ding3.Per100Kn = data[0].Per100Kn3;
                ding3.Per100Extend = data[0].Per100Extend3;
                ding3.Extend = data[0].Extend3;
                ding3.RealExtend = data[0].RealExtend3;
                ding3.ExtendError = data[0].ExtendError3;
            }
            else
            {
                ding3.Flag = 0;
            }
            if (data[0].Gang4Flag == 1)
            {
                ding4.Flag = 1;
                ding4.Per10Kn = data[0].Per10Kn4;
                ding4.Per10Extend = data[0].Per10Extend4;
                ding4.Per20Kn = data[0].Per20Kn4;
                ding4.Per20Extend = data[0].Per20Extend4;
                ding4.Per50Kn = data[0].Per50Kn4;
                ding4.Per50Extend = data[0].Per50Extend4;
                ding4.Per100Kn = data[0].Per100Kn4;
                ding4.Per100Extend = data[0].Per100Extend4;
                ding4.Extend = data[0].Extend4;
                ding4.RealExtend = data[0].RealExtend4;
                ding4.ExtendError = data[0].ExtendError4;
            }
            else
            {
                ding4.Flag = 0;
            }
        }

        public string GetCurveData(int id)
        {
            ZhangLa_CurveChart curve = new ZhangLa_CurveChart();
            List<Tb_ZhangLa1_Data> data = Tb_ZhangLa1_DataDAL.LoadEntities(r => r.Id == id).ToList();
            List<Tb_ZhangLa1_Curve> curvedatas = data[0].Tb_ZhangLa1_Curve.OrderBy(r => r.CurveTime).ToList();
            if (curvedatas.Count != 0)
            {

                foreach (Tb_ZhangLa1_Curve quxian in curvedatas)
                {
                    curve.DateTime.Add(quxian.CurveTime.ToString());
                    if (data[0].Gang1Flag == 1)
                    {
                        curve.Gang1Flag = 1;        //ZhangLa_CurveChart中统一将4个缸的flag置为0，然后根据每一条curve数据做判断，一旦flag为1则一直置为1
                        curve.Kn1.Add((double)quxian.Gang1Kn);
                        curve.Extend1.Add((double)quxian.Gang1Extend);
                        
                    }

                    if (data[0].Gang2Flag == 1)
                    {
                        curve.Gang2Flag = 1;
                        curve.Kn2.Add((double)quxian.Gang2Kn);
                        curve.Extend2.Add((double)quxian.Gang2Extend);
                    }
                    if (data[0].Gang3Flag == 1)
                    {
                        curve.Gang3Flag = 1;
                        curve.Kn3.Add((double)quxian.Gang3Kn);
                        curve.Extend3.Add((double)quxian.Gang3Extend);
                    }
                    if (data[0].Gang4Flag == 1)
                    {
                        curve.Gang4Flag = 1;
                        curve.Kn4.Add((double)quxian.Gang4Kn);
                        curve.Extend4.Add((double)quxian.Gang4Extend);
                    }
                }
            }
            string json = JsonConvert.SerializeObject(curve);
            return json;
        }



    }
}
