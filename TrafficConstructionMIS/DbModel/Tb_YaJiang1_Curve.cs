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
    
    public partial class Tb_YaJiang1_Curve
    {
        public int Id { get; set; }
        public Nullable<int> YJDataId { get; set; }
        public Nullable<decimal> InputPress { get; set; }
        public Nullable<decimal> ZhenKongPress { get; set; }
        public Nullable<System.DateTime> CurveDate { get; set; }
    
        public virtual Tb_YaJiang1_Data Tb_YaJiang1_Data { get; set; }
    }
}