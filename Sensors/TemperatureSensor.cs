using SmartHouseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensors
{
    public class TemperatureSensor:Device
    {
        public double Temperature { get; set; }
        public TemperatureSensor() : base()
        {
            
        }
    }
}
