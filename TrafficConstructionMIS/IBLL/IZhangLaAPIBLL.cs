using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IZhangLaAPIBLL
    {
        string SaveBiaoTouInfo(string data);
        string SaveZhangLaData(string data);
        string SaveZhangLaQuXian(string data);
        string SaveZhangLaMonitor(string data);
    }
}
