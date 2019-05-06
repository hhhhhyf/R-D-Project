using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel
{
    public class YaJiang1_CurveChart
    {
        public List<string> DateTime = new List<string>();//日期
        //public Nullable<List<decimal>> InputPress = new List<decimal>();
        public List<Nullable<decimal>> InputPress = new List<decimal?>();//进浆压力
        public List<Nullable<decimal>> ZhenKongPress = new List<decimal?>();//真空压力
    }
}
