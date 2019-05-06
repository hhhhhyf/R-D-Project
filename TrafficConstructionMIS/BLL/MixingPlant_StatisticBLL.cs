using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using DbModel;
using MyModel;
using Newtonsoft.Json;
using MyTool;
using System.Linq.Expressions;

namespace BLL
{
    public class MixingPlant_StatisticBLL:BaseBLL,IMixingPlant_StatisticBLL
    {
        ITb_MixingPlant_DeviceDAL Tb_MixingPlant_DeviceDAL { get; set; }
        ITb_MixingPlant_PieceDAL Tb_MixingPlant_PieceDAL { get; set; }


        /// <summary>
        /// 统计一个月中每天的盘方量之和
        /// </summary>
        /// <param name="deviceId">搅拌站id</param>
        /// <param name="month">查询月份</param>
        /// <returns>搅拌站的名称，一个月每一天的日期和每日的盘方量</returns>
        public string GetDeviceMonthDate(int deviceId, string month)
        {
            //DateTime mon_dt=DateTime.Parse(Convert.ToDateTime(month.Trim()).ToString("yyyy-MM"));
            DateTime mon_dt = Convert.ToDateTime(month);                             //前端传过来的是字符串形式，转换为日期形式
            List<MixingPlant_StatisticChart> monthData = new List<MixingPlant_StatisticChart>();          //创建返回统计信息的实例

            Expression<Func<Tb_MixingPlant_Piece, bool>> pieceWhere = t => true;                //创建多条件查询

            if (!"".Equals(deviceId) && deviceId != null)
            {
                List<Tb_MixingPlant_Device> device = Tb_MixingPlant_DeviceDAL.LoadEntities(r => r.Id == deviceId).ToList();     //根据deviceId在device表中查找
                string requestDevice = device[0].DeviceFacId;                              //获取device的名称，去piece表中查找该搅拌站下的所有记录
                if (!"".Equals(month) && month != null)                                     //用前台传来的month生成日期查询范围
                {
                    DateTime startMonth = mon_dt.AddDays(1 - mon_dt.Day);                                         
                    DateTime endMonth = startMonth.AddMonths(1);
                    pieceWhere = PredicateExtensions.And(pieceWhere, r => r.DeviceFacId == requestDevice);
                    pieceWhere = PredicateExtensions.And(pieceWhere, r => r.BldTimStart >= startMonth);
                    pieceWhere = PredicateExtensions.And(pieceWhere, r => r.BldTimEnd <= endMonth);
                    List<Tb_MixingPlant_Piece> pieces = Tb_MixingPlant_PieceDAL.LoadEntities(pieceWhere).ToList();                   //获取deviceid下对应月份所有的数据

                    
                    for (DateTime day = startMonth; day < endMonth; day = day.AddDays(1))                //按月份的每一天对上面获得的list进行遍历
                    {
                        float dayPieAmnt = 0;
                        List<DateTime> date = new List<DateTime>();
                        DateTime dayLastMont=day.AddDays(1);
                        MixingPlant_StatisticChart dayData = new MixingPlant_StatisticChart();
                        
                        //pieceWhere = PredicateExtensions.And(pieceWhere, r => r.DeviceFacId == requestDevice);
                        //pieceWhere=PredicateExtensions.And(pieceWhere,r=>r.BldTimStart>=day);
                        //pieceWhere = PredicateExtensions.And(pieceWhere, r => r.BldTimEnd < dayLastMont);
                        List<Tb_MixingPlant_Piece> everydays = pieces.Where(r => r.BldTimStart >= day && r.BldTimEnd < dayLastMont).ToList();   //List类型的实例可以用lambada表达式
                        if (everydays.Count != 0)    //如果某一天有记录则统计所有记录的盘方量之和
                        {
                            foreach (Tb_MixingPlant_Piece everyday in everydays)
                            {
                                dayPieAmnt = dayPieAmnt + (float)everyday.PieAmnt;
                                
                            }

                            dayData.SumPieAmount = (float)Math.Round(dayPieAmnt, 2);      //四舍五入，保留两位小数
                        }
                        else
                        {
                            
                            dayData.SumPieAmount = 0;
                        }
                        dayData.DeviceId = requestDevice;
                        dayData.Date = day.ToLongDateString();
                        monthData.Add(dayData);
                    }

                }       
            }
            string json = JsonConvert.SerializeObject(monthData);
            return json;
        }

        /// <summary>
        /// 获取下拉框中的待选搅拌站
        /// </summary>
        /// <param name="deviceIds"></param>
        /// <returns>返回一个字典，key为DeviceFacId，供前端</returns>
        public string GetSelectOption(string deviceIds)
        {
            List<Tb_MixingPlant_Device> devices = new List<Tb_MixingPlant_Device>();
            List<Tb_MixingPlant_Device> search_device = new List<Tb_MixingPlant_Device>();
            string[] arrdeviceIds = deviceIds.Split(',');
            List<int> device = new List<int>();
            
            foreach (string str in arrdeviceIds)
            {
                int num;
                num = Convert.ToInt32(str);
                device.Add(num);
            }
            //for (int i = 0; i < device.Count; i++)
            foreach(int i in device)
            {
                Tb_MixingPlant_Device Device = new Tb_MixingPlant_Device();
                //search_device = Tb_MixingPlant_DeviceDAL.LoadEntities(r => r.Id == device[i]).ToList();    //在这里数组的下标运算放在了linq表达式中,这种情况也是不被允许的。
                search_device = Tb_MixingPlant_DeviceDAL.LoadEntities(r => i==r.Id).ToList();            
                Device.Id = search_device[0].Id;
                Device.DeviceFacId = search_device[0].DeviceFacId;
                devices.Add(Device);
            }
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("DeviceFacId", devices);                                                           //返回
            string json = JsonConvert.SerializeObject(dict);
            return json;
        }
    }
}
