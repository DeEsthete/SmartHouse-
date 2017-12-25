using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartHousLibrary
{
    public class Logic
    {

        public  void Ardu(bool isOn,string ip)
        { 
            WebRequest request;
            if (isOn)
            {
                request = WebRequest.Create( "http://"+ip+"/$1");
            }
            else
            {
                 request = WebRequest.Create(
                    "http://10.3.6.80/$2");

            }
         
            request.Credentials = CredentialCache.DefaultCredentials;
          
            WebResponse response = request.GetResponse();
            
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            
            Stream dataStream = response.GetResponseStream();
            
            StreamReader reader = new StreamReader(dataStream);
           
            string responseFromServer = reader.ReadToEnd();
           
            Console.WriteLine(responseFromServer);

            reader.Close();
            response.Close();
        }
    }
}
