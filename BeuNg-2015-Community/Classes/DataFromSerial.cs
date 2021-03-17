using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeuNg_2015_Community.Classes
{
    public class DataFromSerial
    {
        static string incomingData;
        static List<string> parsedData;
        static int numberOfParsedData;
        static int count = 0;

        //GETS DATA FROM SERIAL PORT AND RETURNS LIST OF PARSED DATA TO CALLER
        public static List<string> getDataFromSerialPort(string serialPort, int baudRate)
        {
            if(Form1.Self.serialPort1.IsOpen == false)
            {
                Form1.Self.serialPort1.PortName = serialPort;
                Form1.Self.serialPort1.BaudRate = baudRate;
                Form1.Self.serialPort1.Open();
            }

            incomingData = Form1.Self.serialPort1.ReadLine();
            parsedData = Parser.parseStringByComma(incomingData);
            numberOfParsedData = parsedData.Count;

            if (numberOfParsedData < 13)
            {
                while (count < 3)
                {
                    incomingData = incomingData = Form1.Self.serialPort1.ReadLine();
                    parsedData = Parser.parseStringByComma(incomingData);
                    numberOfParsedData = parsedData.Count;
                    count++;

                    if(numberOfParsedData == 13)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                return parsedData;
            }
            else
            {
                return parsedData;
            }
        }

        //String
        public static string getStringSerialData(string serialPort, int baudRate)
        {
            if (Form1.Self.serialPort1.IsOpen == false)
            {
                Form1.Self.serialPort1.PortName = serialPort;
                Form1.Self.serialPort1.BaudRate = baudRate;
                Form1.Self.serialPort1.Open();
            }

            incomingData = Form1.Self.serialPort1.ReadLine();

            return incomingData;
        }
        
        public static void releaseCommand()
        {
            //char rel = 'R';
            Form1.Self.serialPort1.Write("r");
        }
        
        public static void captureCommand()
        {
            Form1.Self.serialPort1.Write("c");
        }
    }
}
