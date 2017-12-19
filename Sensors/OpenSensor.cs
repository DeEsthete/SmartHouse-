using SmartHouseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    public class OpenSensor:Device
    {
        public bool IsOpen { get; set; }
        public OpenSensor():base()
        {

        }
    }
}
