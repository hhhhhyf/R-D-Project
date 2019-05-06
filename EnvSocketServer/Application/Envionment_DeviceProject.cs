using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonLib;
using System.Text.RegularExpressions;

namespace EnvApplication
{
    public class Envionment_DeviceProject
    {
        //public int Id { get; set; } 这个自增不用填写
        public string DeviceCode { get; set; }
       
        public int? ProjectId { get; set; }//湿度       
       
    }
    
}