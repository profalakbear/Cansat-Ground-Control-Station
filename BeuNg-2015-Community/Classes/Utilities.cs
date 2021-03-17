using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeuNg_2015_Community
{
    public class Utilities
    {
        //GET AVAILABLE SERIAL COMMUNICATIONS PORT
        public static void getAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                Form1.Self.comPort.Items.Add(port);
            }
        }
    }
}
