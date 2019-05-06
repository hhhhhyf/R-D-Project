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
    public class YaJiang1BLL:BaseBLL,IYaJiang1BLL
    {
        ITb_YaJiang1_ProjectDAL Tb_YaJiang1_ProjectDAL { get; set; }
        ITb_YaJiang1_DeviceDAL Tb_YaJiang1_DeviceDAL { get; set; }
        ITb_YaJiang1_DataDAL Tb_YaJiang1_DataDAL { get; set; }
        ITb_YaJiang1_CurveDAL Tb_YaJiang1_CurveDAL { get; set; }



        public string GetTableData(int pageIndex, int pageSize, int id)
        {
            int total = 0;
            List<YaJiang1_Main> datas = new List<YaJiang1_Main>();

            List<Tb_YaJiang1_Data> datas_all = Tb_YaJiang1_DataDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total,
                r => r.Tb_YaJiang1_Device.ProjectId == id, r => (DateTime)r.YaJiangDate, false).ToList();

            foreach (Tb_YaJiang1_Data i in datas_all)
            {
                YaJiang1_Main data = new YaJiang1_Main();
                data.Id = i.Id;
                data.Vender = i.Tb_YaJiang1_Device.Vender;
                data.LiangNo = i.LiangNo;
                data.KongNo = i.KongNo;
                data.RealShuiJiao = i.RealShuiJiao;
                data.RealPress = i.RealPress;
                data.RealZhenKongDu = i.RealZhenKongDu;
                data.RealJiaoBanTime = i.RealJiaoBanTime;
                data.RealWenYaTime = i.RealWenYaTime;
                data.YaJiangDate = i.YaJiangDate.ToString();
                datas.Add(data);
            }
            JqGridTable<YaJiang1_Main> jqGridTable = new JqGridTable<YaJiang1_Main>();
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = datas;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }

        public string GetTableSearchData(int pageIndex, int pageSize, int id, string liangNo, string startTime, string endTime)
        {
            int total = 0;
            List<YaJiang1_Main> rows = new List<YaJiang1_Main>();
            Expression<Func<Tb_YaJiang1_Data, bool>> pieceWhere = (r => true);
            pieceWhere = PredicateExtensions.And(pieceWhere, r => r.Tb_YaJiang1_Device.ProjectId == id);
            if (!"".Equals(liangNo) && liangNo != null)
            {
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.LiangNo == liangNo);
            }
            if (!"".Equals(startTime) && startTime != null)
            {
                DateTime start = Convert.ToDateTime(startTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.YaJiangDate >= start);
            }
            if (!"".Equals(endTime) && endTime != null)
            {
                DateTime end = Convert.ToDateTime(endTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.YaJiangDate <= end);
            }
            List<Tb_YaJiang1_Data> datas = Tb_YaJiang1_DataDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total,
                pieceWhere, r => (DateTime)r.YaJiangDate, false).ToList();

            foreach (Tb_YaJiang1_Data i in datas)
            {
                YaJiang1_Main data = new YaJiang1_Main();
                data.Id = i.Id;
                data.Vender = i.Tb_YaJiang1_Device.Vender;
                data.LiangNo = i.LiangNo;
                data.KongNo = i.KongNo;
                data.RealShuiJiao = i.RealShuiJiao;
                data.RealPress = i.RealPress;
                data.RealZhenKongDu = i.RealZhenKongDu;
                data.RealJiaoBanTime = i.RealJiaoBanTime;
                data.RealWenYaTime = i.RealWenYaTime;
                data.YaJiangDate = i.YaJiangDate.ToString();
                rows.Add(data);
            }
            JqGridTable<YaJiang1_Main> jqGridTable = new JqGridTable<YaJiang1_Main>();
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }

        public void GetDetailChart1(int id,YaJiang1_BaseInfo1 baseInfo)
        {
            List<Tb_YaJiang1_Data> data= Tb_YaJiang1_DataDAL.LoadEntities(r => r.Id == id).ToList();

            baseInfo.Vender = data[0].Tb_YaJiang1_Device.Vender;
            baseInfo.LiangNo = data[0].LiangNo;
            baseInfo.InputKong1 = data[0].InputKong1;
            baseInfo.InputKong2 = data[0].InputKong2;
            baseInfo.OutputKong1 = data[0].OutputKong1;
            baseInfo.OutputKong2 = data[0].OutputKong2;
        }

        public void GetDetailChart2(int id,YaJiang1_BaseInfo2 baseInfo)
        {
            List<Tb_YaJiang1_Data> data = Tb_YaJiang1_DataDAL.LoadEntities(r => r.Id == id).ToList();

            baseInfo.SetShuiJiao = data[0].SetShuiJiao;
            baseInfo.RealShuiJiao = data[0].RealShuiJiao;
            baseInfo.SetPress = data[0].SetPress;
            baseInfo.RealPress = data[0].RealPress;
            baseInfo.SetZhenKongDu = data[0].SetZhenKongDu;
            baseInfo.RealZhenKongDu = data[0].RealZhenKongDu;
            baseInfo.SetJiaoBanTime = data[0].SetJiaoBanTime;
            baseInfo.RealJiaoBanTime = data[0].RealJiaoBanTime;
            baseInfo.SetWenYaTime = data[0].SetWenYaTime;
            baseInfo.RealWenYaTime = data[0].RealWenYaTime;
            baseInfo.SetCycleTime = data[0].SetCycleTime;
            baseInfo.RealCycleTime = data[0].RealCycleTime;
        }

        public string GetCurveData(int id)
        {
            YaJiang1_CurveChart curve = new YaJiang1_CurveChart();
            //List<Tb_YaJiang1_Data> data = Tb_YaJiang1_DataDAL.LoadEntities(r => r.Id == id).ToList();
            List<Tb_YaJiang1_Curve> curvedatas = Tb_YaJiang1_CurveDAL.LoadEntities(r=>r.YJDataId==id).OrderBy(r => r.CurveDate).ToList();
            if (curvedatas.Count != 0)
            {
                foreach (Tb_YaJiang1_Curve quxian in curvedatas)
                {
                    curve.DateTime.Add(quxian.CurveDate.ToString());
                    curve.InputPress.Add(quxian.InputPress);
                    curve.ZhenKongPress.Add(quxian.ZhenKongPress);
                }
            }
            string json = JsonConvert.SerializeObject(curve);
            return json;

        }
    }
}
