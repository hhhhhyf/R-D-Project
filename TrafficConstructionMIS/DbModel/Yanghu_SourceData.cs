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
    
    public partial class Yanghu_SourceData
    {
        public int Id { get; set; }
        public string ItemId { get; set; }
        public string ProjectId { get; set; }
        public string FactoryCode { get; set; }
        public string BridgePart { get; set; }
        public string BeamType { get; set; }
        public string DeviceId { get; set; }
        public string TaiId { get; set; }
        public Nullable<double> Temperature { get; set; }
        public Nullable<double> Humidity { get; set; }
        public Nullable<double> Pressure { get; set; }
        public Nullable<int> IsManually { get; set; }
        public Nullable<double> ByTimes { get; set; }
        public Nullable<System.DateTime> CurrentTime { get; set; }
        public Nullable<int> ValveStatus { get; set; }
        public Nullable<int> YhStatus { get; set; }
    }
}