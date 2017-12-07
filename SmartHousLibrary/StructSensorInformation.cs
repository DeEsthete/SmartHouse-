using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHousLibrary
{
    public struct StructSensorInformation
    {
        public double Temperature { get; set; }
        public bool IsMooving { get; set; }
        public bool IsOpen { get; set; }
        public int Watt { get; set; }

        public StructSensorInformation(int temperature, bool isMooving, bool isOpen, int watt)
        {
            Temperature = temperature;
            IsMooving = isMooving;
            IsOpen = isOpen;
            Watt = watt;
        }
    }
}
