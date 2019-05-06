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
    
    public partial class Yanghu_StatData
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ProjectName { get; set; }
        public string FactoryName { get; set; }
        public string BridgePart { get; set; }
        public string BeamType { get; set; }
        public string DeviceId { get; set; }
        public string TaiId { get; set; }
        public Nullable<double> Temperature { get; set; }
        public Nullable<double> Humidity { get; set; }
        public Nullable<double> Pressure { get; set; }
        public Nullable<int> IsManually { get; set; }
        public Nullable<double> ByTimes { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<int> PengCount { get; set; }
        public Nullable<double> PengMinutes { get; set; }
        public Nullable<int> YhStatus { get; set; }
        public Nullable<int> LastValveStatus { get; set; }
        public Nullable<System.DateTime> LastTime { get; set; }
    }
}
