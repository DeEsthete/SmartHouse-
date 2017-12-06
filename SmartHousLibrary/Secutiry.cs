using SmartHousLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouseLibrary
{
    public class Secutiry:User
    {
        public int CountAttempts { get; set; }

        public bool CheckPassword(string password)
        {
            if (Password == password)
            {
                return true;
            }
            else
            {
                CountAttempts++;
                return false;
            }
        }

       
    }
}
