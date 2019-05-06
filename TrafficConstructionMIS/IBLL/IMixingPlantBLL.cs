using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbModel;
namespace IBLL
{
    public interface IMixingPlantBLL
    {
        string GetTableData(int pageIndex, int pageSize, string deviceFacld);
        string GetWarnTableData(int pageIndex, int pageSize, string deviceFacld);
        string GetTableSearchData(int pageIndex, int pageSize, string deviceFacld, string customer,
        string projectName, string consPos, string startTime, string endTime);
        string GetDosageTableData(string Id, string deviceFacld);
        List<Tb_MixingPlant_Material> GetMaterials(string deviceFacld);
        string SaveErrorSet(string data);
    }
}
