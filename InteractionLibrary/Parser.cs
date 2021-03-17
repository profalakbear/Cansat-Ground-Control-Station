using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeuNg_2015_Community
{
    public class Parser
    {

        static string teamID;
        static string missionTime;
        static string packetCount;
        static string height;
        static string pressure;
        static string temperature;
        static string voltage;
        static string gpstime;
        static string longtitude;
        static string lengtitude;
        static string speed;
        static string gpsSatteliteCount;
        static string fswLevel;

        public static List<string> parseStringByComma(string dataParsedByComma)
        {
            List<string> mySensorData = dataParsedByComma.Split(',').ToList<string>();
            return mySensorData;
        }

        public static void parseStringByBracket(List<string> dataParsedByBracket)
        {
            string[] my_data = dataParsedByBracket.ToArray();
            int count = 0;

            foreach(string item in my_data)
            {
                my_data[count] = item.Substring(1).TrimEnd('>');
                count++;
            }

            teamID = my_data[0];
            missionTime = my_data[1];
            packetCount = my_data[2];
            height = my_data[3];
            pressure = my_data[4];
            temperature = my_data[5];
            voltage = my_data[6];
            gpstime = my_data[7];
            longtitude = my_data[8];
            lengtitude = my_data[9];
            speed = my_data[10];
            gpsSatteliteCount = my_data[11];
            fswLevel = my_data[12];
            MessageBox.Show(teamID + " //// " + missionTime);
        }
    }
}

