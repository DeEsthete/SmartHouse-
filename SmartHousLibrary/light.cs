using SmartHousLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseLibrary
{
    public class light:Device, IDimmer
    {
        private int percent;

        int IDimmer.DimmerUp()
        {
            return ++percent;
        }

        int IDimmer.DimmerDown()
        {
            return --percent;
        }
    }
}
