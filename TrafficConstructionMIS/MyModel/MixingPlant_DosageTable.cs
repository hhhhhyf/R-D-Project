using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel
{
    public class MixingPlant_DosageTable
    {
        public string Name { get; set; }
        public decimal Water { get; set; }
        /// <summary>
        /// 配方值
        /// </summary>
        public decimal RecAmnt { get; set; }
        /// <summary>
        /// 设定值
        /// </summary>
        public decimal PlanAmnt { get; set; }
        /// <summary>
        /// 完成值
        /// </summary>
        public decimal FacAmnt { get; set; }

        public Nullable<decimal> PError { get; set; }
        /// <summary>
        /// 误差
        /// </summary>
        public decimal Deviation { get; set; }
    }
}
