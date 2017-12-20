using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHousLibrary
{
    public class Logic
    {
        //SerialPort serialPort1 = new SerialPort("serialPort1", 9600);

        public void Ardu(bool on)
        {
            SerialPort port = new SerialPort("COM6", 9600);
            port.Open();
            if (on == true)
            {
                port.WriteLine("1");
            }
            else
            {
                port.WriteLine("0");
            }
            port.Close();
        }
    }
}
