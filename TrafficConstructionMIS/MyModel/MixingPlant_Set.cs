using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModel
{
    public class MixingPlant_Set
    {
        public class Iterm{
            public string id;
            public string error;
        }
        public string deviceFacld;
        public List<Iterm> datas = new List<Iterm>(); 
    }
}
