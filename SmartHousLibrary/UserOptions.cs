using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHousLibrary
{
    public class UserOptions
    {
        public double TemperatureLimit { get; set; }
        public int WattLimit { get; set; }
        public UserOptions(int temperature, int watt)
        {
            TemperatureLimit = temperature;
            WattLimit = watt;
        }
    }
}
