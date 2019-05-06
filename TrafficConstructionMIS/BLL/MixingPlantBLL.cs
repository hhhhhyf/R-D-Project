using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using DbModel;
using MyModel;
using Newtonsoft.Json;
using MyTool;
using System.Linq.Expressions;
namespace BLL
{
    public class MixingPlantBLL : BaseBLL, IMixingPlantBLL
    {
        ITb_MixingPlant_ProduceDAL Tb_MixingPlant_ProduceDAL { get; set;}
        ITb_MixingPlant_PieceDAL Tb_MixingPlant_PieceDAL { get; set; }
        ITb_MixingPlant_DosageDAL Tb_MixingPlant_DosageDAL { get; set; }
        ITb_MixingPlant_MaterialDAL Tb_MixingPlant_MaterialDAL { get; set; }
        ITb_MixingPlant_DeviceDAL Tb_MixingPlant_DeviceDAL { get; set; }

        private bool IsNumberic(string oText)
        {
            try
            {
                int var1 = Convert.ToInt32(oText);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string DevicePacIdConvert(string deviceFacld)
        {
            if (IsNumberic(deviceFacld))
            {
                int Id = Int32.Parse(deviceFacld);
                List<Tb_MixingPlant_Device> devices = Tb_MixingPlant_DeviceDAL.LoadEntities(r => r.Id == Id).ToList();
               return devices[0].DeviceFacId;
            }
            return deviceFacld;
        }

        public string GetTableData(int pageIndex, int pageSize, string deviceFacld)
        {
            int total = 0;
            deviceFacld = DevicePacIdConvert(deviceFacld);
            JqGridTable<MixingPlant_MainTable> jqGridTable = new JqGridTable<MixingPlant_MainTable>();
            List<MixingPlant_MainTable> rows = new List<MixingPlant_MainTable>();
            List<Tb_MixingPlant_Piece> pieces = Tb_MixingPlant_PieceDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total, r => r.DeviceFacId == deviceFacld, r => (DateTime)r.BldTimStart, false).ToList();
            
            foreach (Tb_MixingPlant_Piece piece in pieces)
            {
                MixingPlant_MainTable table = new MixingPlant_MainTable();
                table.Customer = piece.Tb_MixingPlant_Produce.Customer;
                table.ProjectName = piece.Tb_MixingPlant_Produce.ProjectName;
                table.ConsPos = piece.Tb_MixingPlant_Produce.ConsPos;
                table.BetLev = piece.Tb_MixingPlant_Produce.BetLev;
                table.Id = piece.Id;
                table.BldTimStart = piece.BldTimStart.ToString();
                table.BldTimEnd = piece.BldTimEnd.ToString();
                if (piece.BldTim != null) table.BldTim = (int)piece.BldTim;
                if (piece.PieAmnt != null) table.PieAmnt = (decimal)piece.PieAmnt;
                table.Color = "black";
                //告警代码
                //告警代码
                if (piece.DosageClass == 2) table.Color = "yellow";
                else if (piece.DosageClass == 3) table.Color = "red";
                else table.Color = "black";
                rows.Add(table);
            }
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total/pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }
        public string GetWarnTableData(int pageIndex, int pageSize, string deviceFacld)
        {
            int total = 0;
            deviceFacld = DevicePacIdConvert(deviceFacld);
            JqGridTable<MixingPlant_MainTable> jqGridTable = new JqGridTable<MixingPlant_MainTable>();
            List<MixingPlant_MainTable> rows = new List<MixingPlant_MainTable>();
            Expression<Func<Tb_MixingPlant_Piece, bool>> pieceWhere = t => t.DeviceFacId == deviceFacld;
            pieceWhere = PredicateExtensions.And(pieceWhere, r =>r.DosageClass > 1);
            List<Tb_MixingPlant_Piece> pieces = Tb_MixingPlant_PieceDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total, pieceWhere, r => (DateTime)r.BldTimStart, false).ToList();
            foreach (Tb_MixingPlant_Piece piece in pieces)
            {
                MixingPlant_MainTable table = new MixingPlant_MainTable();
                table.Customer = piece.Tb_MixingPlant_Produce.Customer;
                table.ProjectName = piece.Tb_MixingPlant_Produce.ProjectName;
                table.ConsPos = piece.Tb_MixingPlant_Produce.ConsPos;
                table.BetLev = piece.Tb_MixingPlant_Produce.BetLev;
                table.Id = piece.Id;
                table.BldTimStart = piece.BldTimStart.ToString();
                table.BldTimEnd = piece.BldTimEnd.ToString();
                if (piece.BldTim != null) table.BldTim = (int)piece.BldTim;
                if (piece.PieAmnt != null) table.PieAmnt = (decimal)piece.PieAmnt;
                //告警代码
                if (piece.DosageClass == 2) table.Color = "yellow";
                else if (piece.DosageClass == 3) table.Color = "red";
                else table.Color = "black";

                rows.Add(table);
            }
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }


        public string GetTableSearchData(int pageIndex, int pageSize, string deviceFacld, string customer,
            string projectName, string consPos, string startTime, string endTime)
        {
            int total = 0;
            deviceFacld = DevicePacIdConvert(deviceFacld);
            JqGridTable<MixingPlant_MainTable> jqGridTable = new JqGridTable<MixingPlant_MainTable>();
            List<MixingPlant_MainTable> rows = new List<MixingPlant_MainTable>();

            Expression<Func<Tb_MixingPlant_Piece, bool>> pieceWhere = t => t.DeviceFacId == deviceFacld;
            if (!"".Equals(startTime) && startTime != null)
            {
                DateTime bldTimStart = Convert.ToDateTime(startTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.BldTimStart >= bldTimStart);
            }
            if (!"".Equals(endTime) && endTime != null)
            {
                DateTime bldTimStart1 = Convert.ToDateTime(endTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.BldTimStart <= bldTimStart1);
            }
            if (!"".Equals(customer) && customer != null) pieceWhere = PredicateExtensions.And(pieceWhere, r => r.Tb_MixingPlant_Produce.Customer == customer);
            if (!"".Equals(projectName) && projectName != null) pieceWhere = PredicateExtensions.And(pieceWhere, r => r.Tb_MixingPlant_Produce.ProjectName == projectName);
            if (!"".Equals(consPos) && consPos != null) pieceWhere = PredicateExtensions.And(pieceWhere, r => r.Tb_MixingPlant_Produce.ConsPos == consPos);
            List<Tb_MixingPlant_Piece> pieces = Tb_MixingPlant_PieceDAL.LoadPageEntities<DateTime>(pageIndex, pageSize,
                out total, pieceWhere, r => (DateTime)r.BldTimStart, false).ToList();
            foreach (Tb_MixingPlant_Piece piece in pieces)
            {
                MixingPlant_MainTable table = new MixingPlant_MainTable();
                table.Customer = piece.Tb_MixingPlant_Produce.Customer;
                table.ProjectName = piece.Tb_MixingPlant_Produce.ProjectName;
                table.ConsPos = piece.Tb_MixingPlant_Produce.ConsPos;
                table.BetLev = piece.Tb_MixingPlant_Produce.BetLev;
                table.Id = piece.Id;
                table.BldTimStart = piece.BldTimStart.ToString();
                table.BldTimEnd = piece.BldTimEnd.ToString();
                if (piece.BldTim != null) table.BldTim = (int)piece.BldTim;
                if (piece.PieAmnt != null) table.PieAmnt = (decimal)piece.PieAmnt;
                table.Color = "black";
                //告警代码
                if (piece.DosageClass == 2) table.Color = "yellow";
                else if (piece.DosageClass == 3) table.Color = "red";
                else table.Color = "black";
                ////告警代码
                //foreach (Tb_MixingPlant_Dosage dosage in piece.Tb_MixingPlant_Dosage)
                //{
                //    decimal deviation = 0;
                //    if (dosage.Deviation != null) deviation = Math.Abs((decimal)dosage.Deviation);
                //    if (deviation < 5)
                //    {
                //        if ("".Equals(table.Color) || table.Color == null)
                //        {
                //            table.Color = "black";
                //        }
                //    }
                //    else if (deviation >= 5 && deviation < 10)
                //    {
                //        if ("".Equals(table.Color) || table.Color == null || "black".Equals(table.Color))
                //        {
                //            table.Color = "yellow";
                //        }
                //    }
                //    else
                //    {
                //        table.Color = "red";
                //    }
                //}

                rows.Add(table);
            }
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }


        public string GetDosageTableData(string Id, string deviceFacld)
        {
            deviceFacld = DevicePacIdConvert(deviceFacld);
            List<Tb_MixingPlant_Dosage> tb_MixingPlant_Dosages = 
                Tb_MixingPlant_DosageDAL.LoadEntities(r => (r.PieceId == Id && r.DeviceFacId == deviceFacld)).ToList();
            List<MixingPlant_DosageTable> rows = new List<MixingPlant_DosageTable>(); 
            foreach (Tb_MixingPlant_Dosage dosage in tb_MixingPlant_Dosages)
            {
                MixingPlant_DosageTable dosageTable = new MixingPlant_DosageTable();
                Tb_MixingPlant_Material material = Tb_MixingPlant_MaterialDAL.LoadEntities(r => r.Id == dosage.MaterialId).FirstOrDefault();
                if (material != null) dosageTable.Name = material.Name;
                if(dosage.Deviation != null) dosageTable.Deviation = (decimal)dosage.Deviation;
                if (dosage.FacAmnt != null) dosageTable.FacAmnt = (decimal)dosage.FacAmnt;
                if (dosage.PlanAmnt != null) dosageTable.PlanAmnt = (decimal)dosage.PlanAmnt;
                if (dosage.RecAmnt != null) dosageTable.RecAmnt = (decimal)dosage.RecAmnt;
                if (dosage.Water != null) dosageTable.Water = (decimal)dosage.Water;
                if (material.PError != null) dosageTable.PError = (decimal)material.PError;
                rows.Add(dosageTable);
            }
            JqGridTable<MixingPlant_DosageTable> jqGridTable = new JqGridTable<MixingPlant_DosageTable>();
            jqGridTable.page = 1;
            jqGridTable.records = rows.Count;
            jqGridTable.rows = rows;
            jqGridTable.total = 1;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }


        public List<Tb_MixingPlant_Material> GetMaterials(string deviceFacld)
        {
            deviceFacld = DevicePacIdConvert(deviceFacld);
            return Tb_MixingPlant_MaterialDAL.LoadEntities(r => r.DeviceFacId == deviceFacld).ToList();
        }

        public string SaveErrorSet(string data) 
        {
            MixingPlant_Set set = JsonConvert.DeserializeObject<MixingPlant_Set>(data);

            string deviceFacld = DevicePacIdConvert(set.deviceFacld);

            foreach (MixingPlant_Set.Iterm item in set.datas)
            {
                Tb_MixingPlant_Material material = Tb_MixingPlant_MaterialDAL.LoadEntities
                    (r => (r.Id == item.id) && r.DeviceFacId == deviceFacld).FirstOrDefault();
                if (item.error.Length == 0) material.PError = 0;
                else material.PError = Decimal.Parse(item.error);
            }
            SaveChanges();
            return "1";
        }
    }
}
