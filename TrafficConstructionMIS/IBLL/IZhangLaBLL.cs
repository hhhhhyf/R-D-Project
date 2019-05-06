using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModel;
namespace IBLL
{
    public interface IZhangLaBLL
    {
        string GetTableData(int pageIndex, int pageSize, int Id);
        string GetTableSearchData(int pageIndex, int pageSize, int Id,
            string liangStr, string startTime, string endTime);
        void GetDetailData(int id, ZhangLa_DetailData_BaseInfo baseInfo, ZhangLa_DetailData_Ding ding1,
            ZhangLa_DetailData_Ding ding2, ZhangLa_DetailData_Ding ding3, ZhangLa_DetailData_Ding ding4);
        string GetCurveData(int id);
    }
}
