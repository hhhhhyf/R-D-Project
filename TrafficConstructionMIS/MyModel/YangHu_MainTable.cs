using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel
{
    public class YangHu_MainTable
    {
        public int Id { get; set; }
        public string FactoryName { get; set; }
        public string BridgePart { get; set; }
        public string BeamType { get; set; }
        public string TaiId { get; set; }
        public Nullable<double> ByTimes { get; set; }
        public string StartTime { get; set; }
        public Nullable<int> PengCount { get; set; }
        public Nullable<double> PengMinutes { get; set; }

        public string DeviceId { get; set; }
        public string RunMethod { get; set; }
        public Nullable<double> Temperature { get; set; }
        public Nullable<double> Humidity { get; set; }
        public Nullable<double> Pressure { get; set; }
    }
}
