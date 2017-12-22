
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseLibrary
{
    public class Room:Device
    {
        List<Device> Device { get; set; }

        public Room()
        {
            Device = new List<Device>();
        }
    }
}
