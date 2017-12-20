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

        //public void SendMessage()
        //{
        //    const string accountSid = "ACa3a0aa93822680c0df0f954abfaa5010";
        //    const string authToken = "660eefc858c608383d38fac4633279d3";
        //    TwilioClient.Init(accountSid, authToken);
        //    var mediaUrl = new List<System.Uri>()
        //{
        //    new Uri( "https://climacons.herokuapp.com/clear.png" )
        //};
        //    var to = new PhoneNumber("+77779561787");
        //    var message = MessageResource.Create(
        //      to,
        //      from: new PhoneNumber("+18317049551"),
        //      body: Password,
        //      mediaUrl: mediaUrl);
        //    Console.WriteLine(message.Sid);
        //}
    }
}
