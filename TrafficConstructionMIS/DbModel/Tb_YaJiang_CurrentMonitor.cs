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
    
    public partial class Tb_YaJiang_CurrentMonitor
    {
        public int Id { get; set; }
        public Nullable<int> DeviceNo { get; set; }
        public string DotId { get; set; }
        public Nullable<int> ConnState { get; set; }
        public string ZLState { get; set; }
        public string KongEx { get; set; }
        public string LiangType { get; set; }
        public string liangstr { get; set; }
        public Nullable<int> IType { get; set; }
        public Nullable<decimal> ShuiQ { get; set; }
        public Nullable<decimal> ShuiNiQ { get; set; }
        public Nullable<decimal> additiveQ { get; set; }
        public Nullable<decimal> AllQ { get; set; }
        public Nullable<int> JiaoBanTime { get; set; }
        public Nullable<decimal> EntrSpeed { get; set; }
        public Nullable<decimal> ExitSpeed { get; set; }
        public Nullable<decimal> EntrPree { get; set; }
        public Nullable<decimal> ExitPree { get; set; }
        public Nullable<int> Keeptime { get; set; }
        public Nullable<decimal> TotalSize { get; set; }
    }
}
