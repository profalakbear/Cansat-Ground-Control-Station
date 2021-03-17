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
        //Get available serial communication ports
        public static void getAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                Form1.Self.comPort.Items.Add(port);
            }
        }

        //Start Google map
        public void initializeMap()
        {
            //Dummy data
            string city = "Baku";
            string country = "Azerbaijan;";

            try
            {
                StringBuilder queryaddress = new StringBuilder();
                queryaddress.Append("http://maps.google.com/maps?q=");

                queryaddress.Append(city + "," + "+");
                queryaddress.Append(country + "," + "+");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }


    }
}
