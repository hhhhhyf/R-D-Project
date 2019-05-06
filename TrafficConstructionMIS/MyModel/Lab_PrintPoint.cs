using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel
{
    /// <summary>
    /// 装载经过坐标转换以后的各条曲线中点的坐标
    /// </summary>
    public class Lab_PrintPoint
    {
        public int Lab_CurveType = 0;//获取曲线类型
        public int curveNum;//获取一个id下有条数据
        public List<int> x_Arrary1 = new List<int>();//点的x坐标值
        public List<int> x_Arrary2 = new List<int>();
        public List<int> x_Arrary3 = new List<int>();
        public List<int> y_Arrary1 = new List<int>();
        public List<int> y_Arrary2 = new List<int>();
        public List<int> y_Arrary3 = new List<int>();

        /// <summary>
        /// 曲线3——水泥抗压有6条曲线
        /// </summary>
        public List<int> x_Arrary4 = new List<int>();
        public List<int> x_Arrary5 = new List<int>();
        public List<int> x_Arrary6 = new List<int>();
        public List<int> y_Arrary4 = new List<int>();
        public List<int> y_Arrary5 = new List<int>();
        public List<int> y_Arrary6 = new List<int>();
    }
}
