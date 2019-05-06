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
    public class Envionment_ActualTimeBLL:BaseBLL,IEnvionment_ActualTimeBLL
    { 
        IEnvionment_DataDAL Envionment_DataDAL{get;set;}
        IEnvionment_ProjectInfoDAL Envionment_ProjectInfoDAL { get; set; }



        public string GetActualTimeData(int projectId)
        {
            int total;
            List<Envionment_ProjectInfo> projectInfo = Envionment_ProjectInfoDAL.LoadEntities(r => r.Id == projectId).ToList();
            List<Envionment_Data> edata = Envionment_DataDAL.LoadPageEntities<DateTime>(1, 1, out total, r => r.ProjectId == projectId, r => (DateTime)r.UploadTime, false).ToList();
            Envionment_ActualTimeData actualTimeData = new Envionment_ActualTimeData();
            DateTime currentTime= System.DateTime.Now;
            TimeSpan ts = GetTimePeriod(currentTime, (DateTime)edata[0].UploadTime);
            if (ts.TotalMinutes > 10) actualTimeData.State = 0;
            else actualTimeData.State = 1;
            actualTimeData.ProjectName = projectInfo[0].ProjectName;
            actualTimeData.Temperature = edata[0].Temperature;
            actualTimeData.DeviceCode = edata[0].DeviceCode;
            actualTimeData.UploadTime = edata[0].UploadTime.ToString();
            actualTimeData.BiaoDuan = projectInfo[0].BiaoDuan;
            actualTimeData.Humidity = edata[0].Humidity;
            return JsonConvert.SerializeObject(actualTimeData);
        }

        private TimeSpan GetTimePeriod(DateTime dt1, DateTime dt2)
        {
            return dt1.Subtract(dt2);
        } 
    }
}
