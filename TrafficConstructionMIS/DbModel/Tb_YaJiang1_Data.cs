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
    
    public partial class Tb_YaJiang1_Data
    {
        public Tb_YaJiang1_Data()
        {
            this.Tb_YaJiang1_Curve = new HashSet<Tb_YaJiang1_Curve>();
        }
    
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string LiangType { get; set; }
        public string LiangNo { get; set; }
        public string KongNo { get; set; }
        public Nullable<decimal> SetShuiJiao { get; set; }
        public Nullable<decimal> RealShuiJiao { get; set; }
        public Nullable<decimal> SetPress { get; set; }
        public Nullable<decimal> RealPress { get; set; }
        public Nullable<decimal> SetZhenKongDu { get; set; }
        public Nullable<decimal> RealZhenKongDu { get; set; }
        public Nullable<int> SetJiaoBanTime { get; set; }
        public Nullable<int> RealJiaoBanTime { get; set; }
        public Nullable<int> SetWenYaTime { get; set; }
        public Nullable<int> RealWenYaTime { get; set; }
        public Nullable<int> SetCycleTime { get; set; }
        public Nullable<int> RealCycleTime { get; set; }
        public string InputKong1 { get; set; }
        public string InputKong2 { get; set; }
        public string OutputKong1 { get; set; }
        public string OutputKong2 { get; set; }
        public Nullable<System.DateTime> YaJiangDate { get; set; }
    
        public virtual ICollection<Tb_YaJiang1_Curve> Tb_YaJiang1_Curve { get; set; }
        public virtual Tb_YaJiang1_Device Tb_YaJiang1_Device { get; set; }
    }
}