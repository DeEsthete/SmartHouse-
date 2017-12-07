using SmartHouseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHousLibrary
{
    public class Scenario : Device
    {
        public Device device { set; get; }
        public bool IsOn { set; get; }
        public DateTime time { set; get; }
    }
}