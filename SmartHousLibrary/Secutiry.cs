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
        public  bool IsLoginRight { get; set; }
        public bool IsPasswordRight { get; set; }

        public Secutiry()
        {
            CountAttempts = 0;
            IsLoginRight = false;
            IsPasswordRight = false;
        }
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

        public bool CheckLogin(string login)
        {
            if (Login == login)
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
