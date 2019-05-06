using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel
{
    public class ZhangLa1_DetailData_Ding
    {
        public Nullable<int> Flag { get; set; }
        public Nullable<decimal> Per10Kn { get; set; } //10%控制应力(KN)
        public Nullable<decimal> Per10Extend { get; set; }//10%伸长量(mm)
        public Nullable<decimal> Per20Kn { get; set; }//20%控制应力(KN)
        public Nullable<decimal> Per20Extend { get; set; }//20%伸长量(mm)
        public Nullable<decimal> Per50Kn { get; set; }//50%控制应力(KN)
        public Nullable<decimal> Per50Extend { get; set; }//50%伸长量(mm)
        public Nullable<decimal> Per100Kn { get; set; }//100%控制应力(KN)
        public Nullable<decimal> Per100Extend { get; set; }//100%伸长量(mm)
        public Nullable<decimal> Extend { get; set; }//理论伸长量(mm)
        public Nullable<decimal> RealExtend { get; set; }//真实伸长量(mm)
        public Nullable<decimal> ExtendError { get; set; }//相对误差率(%)
    }
}
