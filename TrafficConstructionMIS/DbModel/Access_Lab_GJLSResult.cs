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
    
    public partial class Access_Lab_GJLSResult
    {
        public int Id { get; set; }
        public string ZuHao { get; set; }
        public string ShunXu { get; set; }
        public string Shape { get; set; }
        public string Tester { get; set; }
        public System.DateTime TestDate { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> MaxLoad { get; set; }
        public string Curvedata { get; set; }
    }
}
