using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModel;

namespace IBLL
{
    public interface IZhangLa1BLL
    {
        string GetTableData(int pageIndex, int pageSize, int id);
        string GetTableSearchData(int pageIndex, int pageSize, int id,
            string liangNo, string startTime, string endTime);
        void GetDetailData(int id, ZhangLa1_DetailData_BaseInfo baseInfo, ZhangLa1_DetailData_Ding ding1,
            ZhangLa1_DetailData_Ding ding2, ZhangLa1_DetailData_Ding ding3, ZhangLa1_DetailData_Ding ding4);
        string GetCurveData(int id);
        string GetWarnTableData(int pageIndex, int pageSize, int id);
    }
}
