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
    public class YaJiangBLL:BaseBLL,IYaJiangBLL
    {
        ITb_YaJiang_ProjectDAL Tb_YaJiang_ProjectDAL { get; set; }                     //这里的名字   Tb_YaJiang_ProjectDAL   一定要和spring中的name保持一致
        ITb_YaJiang_ProjectInfoDAL Tb_YaJiang_ProjectInfoDAL { get; set; }
        ITb_YaJiang_DataDAL Tb_YaJiang_DataDAL { get; set; }

        public string GetTableData(int pageIndex, int pageSize, int id)
        {
            int total = 0;
            List<Tb_YaJiang_Project> searchConditions = Tb_YaJiang_ProjectDAL.LoadEntities(r => r.Id == id).ToList();
            string projectName = searchConditions[0].ProjectName;
            string dotId = searchConditions[0].DotId;
            
            List<YaJiang_Main> datas = new List<YaJiang_Main>();
            List<Tb_YaJiang_Data> datas_all = Tb_YaJiang_DataDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total,
                r => r.Tb_YaJiang_ProjectInfo.ProjectName.Equals(projectName) && r.Tb_YaJiang_ProjectInfo.DotId.Equals(dotId), r => (DateTime)r.ZLTime, false).ToList();    //外键
            foreach (Tb_YaJiang_Data i in datas_all)
            {
                YaJiang_Main data = new YaJiang_Main();
                data.Id = i.Id;
                data.LiangStr = i.LiangStr;
                data.KongEX = i.KongEx;
                data.PressSX = i.PressSX;
                data.PressXX = i.PressXX;
                data.ZhenKongPree = i.ZhenKongPree;
                data.JiaoBanTime = i.JiaoBanTime;
                data.Keeptime = i.Keeptime;
                data.CycleTime = i.CycleTime;
                data.ZLTime = i.ZLTime.ToString();
                data.EndTime = i.EndTime.ToString();
                datas.Add(data);
            }
            JqGridTable<YaJiang_Main> jqGridTable = new JqGridTable<YaJiang_Main>();
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = datas;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }

        public string GetTableSearchData(int pageIndex, int pageSize, int id, string liangStr, string startTime, string endTime)
        {
            int total = 0;
            List<Tb_YaJiang_Project> searchCondition = Tb_YaJiang_ProjectDAL.LoadEntities(r => r.Id == id).ToList();
            string projectName = searchCondition[0].ProjectName;
            string dotId = searchCondition[0].DotId;
            List<YaJiang_Main> datas = new List<YaJiang_Main>();
            Expression<Func<Tb_YaJiang_Data, bool>> pieceWhere = t => t.Tb_YaJiang_ProjectInfo.ProjectName == projectName && t.Tb_YaJiang_ProjectInfo.DotId == dotId;
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
            List<Tb_YaJiang_Data> datas_all = Tb_YaJiang_DataDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total,
                pieceWhere, r => (DateTime)r.ZLTime, false).ToList();
            foreach (Tb_YaJiang_Data i in datas_all)
            {
                YaJiang_Main data = new YaJiang_Main();
                data.Id = i.Id;
                data.LiangStr = i.LiangStr;
                data.KongEX = i.KongEx;
                data.PressSX = i.PressSX;
                data.PressXX = i.PressXX;
                data.ZhenKongPree = i.ZhenKongPree;
                data.JiaoBanTime = i.JiaoBanTime;
                data.Keeptime = i.Keeptime;
                data.CycleTime = i.CycleTime;
                datas.Add(data);
            }
            JqGridTable<YaJiang_Main> jqGridTable = new JqGridTable<YaJiang_Main>();
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = datas;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
            
        }

        public void GetDetailChart(int id,YaJiang_DetailChart baseInfo)
        {
            List<Tb_YaJiang_Data> data = Tb_YaJiang_DataDAL.LoadEntities(r => r.Id == id).ToList();

            baseInfo.Id = data[0].Id;
            baseInfo.DeviceNo = data[0].DeviceNo;
            baseInfo.LiangType = data[0].LiangType;
            baseInfo.LiangStr = data[0].LiangStr;
            baseInfo.KongEx = data[0].KongEx;
            baseInfo.DotId = data[0].DotId;
            baseInfo.ProjectNo = data[0].ProjectNo;
            baseInfo.ZLTime = data[0].ZLTime;
            baseInfo.EndTime = data[0].EndTime;
            baseInfo.ZLNum = data[0].ZLNum;
            baseInfo.Kong = data[0].Kong;
            baseInfo.ZLType = data[0].ZLType;
            baseInfo.CycleTime = data[0].CycleTime;
            baseInfo.ShuiQ = data[0].ShuiQ;
            baseInfo.ShuiNiQ = data[0].ShuiNiQ;
            baseInfo.PressSX = data[0].PressSX;
            baseInfo.PressXX = data[0].PressXX;
            baseInfo.Keeptime = data[0].Keeptime;
            baseInfo.EnterPree = data[0].EntrPree;
            baseInfo.JiaoBanTime = data[0].JiaoBanTime;
            baseInfo.ZhenKongPree = data[0].ZhenKongPree;
        }


        public string GetCurveData(int id)
        {
            YaJiang_CurveChart curve = new YaJiang_CurveChart();
            List<Tb_YaJiang_Data> data = Tb_YaJiang_DataDAL.LoadEntities(r => r.Id == id).ToList();
            List<Tb_YaJiang_Curve> curvedatas = data[0].Tb_YaJiang_Curve.ToList();
            if (curvedatas.Count != 0)
            {
                List<YaJiang_QuXianDataAry> lists = JsonConvert.DeserializeObject<List<YaJiang_QuXianDataAry>>(curvedatas[curvedatas.Count - 1].DataAry);
                //目的是为了处理某些厂家数据格式传递成  yyyy:mm:dd hh:mm:ss 00:00:00不能转化的问题
                foreach (YaJiang_QuXianDataAry t in lists)
                {
                    string[] s = t.RecordTime.Split(' ');
                    t.RecordTime = s[0] + ' ' + s[1];
                }
                lists = lists.OrderBy(r => DateTime.Parse(r.RecordTime)).ToList();
                foreach (YaJiang_QuXianDataAry quxian in lists)
                {
                    curve.DateTime.Add(quxian.RecordTime);
                    curve.Pressure.Add((double)quxian.In_Pressure);
                }
            }
            string json = JsonConvert.SerializeObject(curve);
            return json;
        }
    }
}
