using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel
{
    /// <summary>
    /// 潘承瑞  旋喷机实时数据表
    /// </summary>主要将DataTime类型数据转换为string类型，以免前端显示格式错误
    public class XuanPenJi_ActualTimeTable
    {
        public int Id { get; set; }

        /// <summary>
        /// 开始结束标志
        /// </summary>
        public string Flag { get; set; }

        public string UploadTime { get; set; }

        public string ProjectName { get; set; }

        /// <summary>
        /// 桩号
        /// </summary>
        public string PileSite { get; set; }

        public string DeviceCode { get; set; }

        /// <summary>
        /// 流量
        /// </summary>
        public float Flow { get; set; }

        /// <summary>
        /// 压力
        /// </summary>
        public float Pressure { get; set; }

        public string OperateTime { get; set; }
    }
}
