using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IYanghu_StatDataBLL
    {
        string GetTableData(int pageIndex, int pageSize, string itemName);
        string GetTableSearchData(int pageIndex, int pageSize, string itemName, string liangNo, string deviceId, string startTime, string endTime);
    }
}
