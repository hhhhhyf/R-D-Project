using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel
{
    public class ZhangLa_DetailData_BaseInfo
    {
        public string KongEx { get; set; }
        public string LiangStr { get; set; }
        public string ZLTime { get; set; }
        public string LiangType { get; set; }
        public Nullable<decimal> Extend { get; set; }
        public Nullable<decimal> RealExtend { get; set; }
        public Nullable<decimal> ErrorRate { get; set; }
        public Nullable<decimal> Per100Press { get; set; }
        public string ZLType { get; set; }
        public Nullable<int> Kong { get; set; }
        public Nullable<int> InitPress { get; set; }
        public Nullable<int> HuiSuoL { get; set; }
        public Nullable<int> BaoYaTime { get; set; }
    }
}
