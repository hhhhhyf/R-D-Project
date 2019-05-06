using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IEnvionment_StatisticBLL
    {
        string GetChartData(int projectId, string startTime, string endTime);
        string GetTableSearchData(int pageIndex, int pageSize, string startTime, string endTime);
    }
}
