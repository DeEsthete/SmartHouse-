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
        //SerialPort serialPort1 = new SerialPort("serialPort1", 9600);

        //public void Ardu(bool on)
        //{
        //    SerialPort port = new SerialPort("COM6", 9600);
        //    port.Open();
        //    if (on == true)
        //    {
        //        port.WriteLine("1");
        //    }
        //    else
        //    {
        //        port.WriteLine("2");
        //    }
        //    port.Close();
        //}

        public  void Ardu(bool isOn)
        { 
            WebRequest request;
            if (isOn)
            {
                request = WebRequest.Create(
                 "http://10.3.6.80/$1");
            }
            else
            {
                 request = WebRequest.Create(
                    "http://10.3.6.80/$2");

            }
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams and the response.
            reader.Close();
            response.Close();
        }
    }
}
