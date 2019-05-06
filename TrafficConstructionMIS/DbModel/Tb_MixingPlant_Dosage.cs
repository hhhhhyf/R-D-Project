//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DbModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tb_MixingPlant_Dosage
    {
        public string Id { get; set; }
        public string DeviceFacId { get; set; }
        public string PieceId { get; set; }
        public string MaterialId { get; set; }
        public Nullable<decimal> Water { get; set; }
        public Nullable<decimal> RecAmnt { get; set; }
        public Nullable<decimal> PlanAmnt { get; set; }
        public Nullable<decimal> FacAmnt { get; set; }
        public Nullable<decimal> Deviation { get; set; }
        public Nullable<System.DateTime> OperationTime { get; set; }
    
        public virtual Tb_MixingPlant_Material Tb_MixingPlant_Material { get; set; }
        public virtual Tb_MixingPlant_Piece Tb_MixingPlant_Piece { get; set; }
    }
}