using DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModel;

namespace IBLL
{
    public interface IWeiXin_APIBLL
    {
        string CheckUserLogin(string userName, string password);

        string GetMixingPlantData(int pageIndex, int pageSize, string deviceFacld);
        string GetMixingPlantWarnData(int pageIndex, int pageSize, string deviceFacld);
        string GetMixingPlantDetailData(string Id, string deviceFacld);

        string GetLabData(int pageIndex, int pageSize, int departmentId);
        void GetLabDetailData(int Id, List<Tb_Lab_TestItem> labTestItem, List<Tb_Lab_Test> labTest);

        string GetTensionData(int pageIndex, int pageSize, int id);
        void GetTensionDetailData(int id, ZhangLa1_DetailData_BaseInfo baseInfo, ZhangLa1_DetailData_Ding ding1,
    ZhangLa1_DetailData_Ding ding2, ZhangLa1_DetailData_Ding ding3, ZhangLa1_DetailData_Ding ding4);
        string GetTensionCurveData(int id);

        string GetMudjackData(int pageIndex, int pageSize, int id);
        string GetTensionWarnData(int pageIndex, int pageSize, int id);

        string GetYangHuData(int pageIndex, int pageSize, string itemName);

        string GetWenDuActualTimeData(int projectId);
        string GetWenDuChartData(int projectId, string startTime, string endTime);


        string GetCurveData(int Id);//试验室曲线
        string GetDeviceList();
        string SaveLocationData(int id, string deviceCode, string type, float longitude, float latitude);
    }
}
