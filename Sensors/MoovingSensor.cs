using SmartHouseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    public class MoovingSensor:Device
    {
        public bool IsMooving { get; set; }
        public MoovingSensor():base()
        {

        }
    }
}
