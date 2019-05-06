using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbModel;
using MyModel;

namespace IBLL
{
    public interface ILabBLL
    {
        string GetTableData(int pageIndex, int pageSize,int departmentId);
        string GetTableSearchData(int pageIndex, int pageSize, int departmentId, int testItemId, string testNo, string startTime, string endTime);
        string GetSelectOption();
        void GetDetailData(int Id, List<Tb_Lab_TestItem> labTestItem,List<Tb_Lab_Test> labTest);
        string GetCurveData(int id);
        string Print(int Id);
        string PrintChartPoints(Lab_CurveChart jsonData, int max_length, int maxForce);
    }
}
