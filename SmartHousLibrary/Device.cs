using SmartHousLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseLibrary
{
    public class Device:ISwitcher
    {
        public Guid Id { get;  set; }
        public string Name { get; set; }
        public bool IsOn { get; set; }

        public Device()
        {
            Id = Guid.NewGuid();
        }
        public void TurnOn()
        {
            IsOn = true;
        }

        public void TurnOff()
        {
            IsOn = false;
        }

    }
}