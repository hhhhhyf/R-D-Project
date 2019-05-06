using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel
{
    public class Envionment_StatisticTable
    {
        public string ProjectName { get; set; }
        public string BiaoDuan { get; set; }
        public string DeviceCode { get; set; }
        public string UploadTime { get; set; }
        public Nullable<double> Temperature { get; set; }
        public Nullable<double> Humidity { get; set; }
    }
}
