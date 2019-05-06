using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel
{
    public class Lab_CurveChart
    {
        public int Lab_CurveType = 0;//获取曲线类型
        public int curveNum;//获取一个id下有条数据
        public int curve1Flag = 0;//下面三条主要为前台js书写便利
        public int curve2Flag = 0;
        public int curve3Flag = 0;
        public List<string> DateTime1 = new List<string>();
        public List<string> DateTime2 = new List<string>();
        public List<string> DateTime3 = new List<string>();
        public List<double> Force1 = new List<double>();
        public List<double> Force2 = new List<double>();
        public List<double> Force3 = new List<double>();

        public int curve4Flag = 0;
        public int curve5Flag = 0;
        public int curve6Flag = 0;
        public List<string> DateTime4 = new List<string>();
        public List<string> DateTime5 = new List<string>();
        public List<string> DateTime6 = new List<string>();
        public List<double> Force4 = new List<double>();
        public List<double> Force5 = new List<double>();
        public List<double> Force6 = new List<double>();
    }
}
