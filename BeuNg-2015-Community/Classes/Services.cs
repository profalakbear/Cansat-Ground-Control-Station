using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using BeuNg_2015_Community.Classes;
using System.IO;

namespace BeuNg_2015_Community.Classes
{
    public class Services
    {
        static string teamID = "2591";
        static string missionTime = "00:00:00";
        static int missionTime_second = -1;
        static int packetCount = 0;
        static float height = 0;
        static float pressure = 0;
        static float temperature = 0;
        static float voltage = 0;
        static string gpstime ="00.00.00.00:00";
        static float longtitude = 1000;
        static float latitude = 1000;
        static float speed = 0;
        static string gpsSatteliteCount = "0";
        static string fswStage = "0";
        static float humidity = 0;
        static string pitch = "0";
        static string roll = "0";
        static string yaw = "0";
        static string capture = "0";
        static string[] myData;

        public static void push_back(List<string> parsedDataFromMain)
        {
            myData = parsedDataFromMain.ToArray();
            int i = 0;
            bool packetFlag = false, missionTimeFlag = false;
            while (myData.Length > i)  
            {
                if (myData[i][0] == '<' &&
                    myData[i][myData[i].Length - 1] == '>' ||
                    myData[i][myData[i].Length - 1] == 13 &&
                    myData[i][1] > 'a'  &&
                    myData[i][1] < 's'
                    )
                {
                    float float_temp;
                    int int_temp, int_temp2;
                    switch (myData[i].Substring(1, 1))
                    {
                        case "a":   // #1 Team ID
                            teamID = myData[i].Substring(2).TrimEnd('>')=="2591" ? myData[i].Substring(2).TrimEnd('>'):"2591";
                            break;
                        case "b":   // #2 Mission Time
                            missionTime = myData[i].Substring(2).TrimEnd('>');
                            missionTimeFlag = true;

                            string[] temp_arr = missionTime.Split(':');
                            int.TryParse(temp_arr[0], out int_temp);
                            int.TryParse(temp_arr[1], out int_temp2);
                            int_temp2 += int_temp * 60;
                            if (int_temp2 != missionTime_second + 1)
                                missionTimeFlag = false;
                            else
                                missionTime_second = int_temp2;
                            break;
                        case "c":   // #3 Packet count
                            if (int.TryParse(myData[i].Substring(2).TrimEnd('>'), out int_temp))
                            {
                                packetCount = int_temp;
                                packetFlag = true;
                            }
                            break;
                        case "d":   // #4 Height
                            if (float.TryParse(myData[i].Substring(2).TrimEnd('>'), out float_temp))
                                height = float_temp;
                            break;
                        case "e":   // #5 Pressure
                            if (float.TryParse(myData[i].Substring(2).TrimEnd('>'), out float_temp))
                                pressure = float_temp;
                            break;
                        case "f":   // #6 Temperature
                            if (float.TryParse(myData[i].Substring(2).TrimEnd('>'), out float_temp))
                                temperature = float_temp;
                            break;
                        case "g":   // #7 Voltage
                            if (float.TryParse(myData[i].Substring(2).TrimEnd('>'), out float_temp))
                                voltage = float_temp;
                            break;
                        case "h":   // #8 GPS Time
                            gpstime = myData[i].Substring(2).TrimEnd('>');
                            break;
                        case "i":   // #9 Latitude
                            if (float.TryParse(myData[i].Substring(2).TrimEnd('>'), out float_temp))
                                latitude = float_temp;
                            break;
                        case "j":   // #10 Longtitude
                            if (float.TryParse(myData[i].Substring(2).TrimEnd('>'), out float_temp))
                                longtitude = float_temp;
                            break;
                        case "k":   // #11 Speed
                            if (float.TryParse( myData[i].Substring(2).TrimEnd('>'), out float_temp))
                                speed = float_temp;
                            break;
                        case "l":   // #12 GPS Satellite count
                            gpsSatteliteCount = myData[i].Substring(2).TrimEnd('>');
                            break;
                        case "m":   // #13 FSW Stage
                            fswStage = myData[i].Substring(2).TrimEnd('>');
                            break;  
                        case "n":   // #14 Humidity
                            if (float.TryParse(myData[i].Substring(2).TrimEnd('>'), out float_temp))
                                humidity = float_temp;
                            break;
                        case "o":   // #15 Pitch
                            pitch = myData[i].Substring(2).TrimEnd('>');
                            break;
                        case "p":   // #16 Roll 
                            roll = myData[i].Substring(2).TrimEnd('>');
                            break;
                        case "q":   // #17 Yaw
                            yaw = myData[i].Substring(2).TrimEnd('>');
                            break;
                        case "r":   // #18 Capture
                            //MessageBox.Show(((int)(myData[17][myData[17].Length - 1])).ToString());
                            capture = myData[i].Substring(2).TrimEnd('>', '\r');
                            break;
                    }
                }
                i++;
            }

            if (!packetFlag)
                packetCount++;
            if (!missionTimeFlag)
            {
                missionTime_second++;
                //MessageBox.Show(temp.ToString());
                missionTime =   (missionTime_second/60<10 ? "0":"") + 
                                (missionTime_second/60).ToString() + ":" + 
                                (missionTime_second%60<10 ? "0":"") +
                                (missionTime_second%60).ToString();
            }
        }

        //GET TEAM ID #1
        public static string getTeamId()
        {
            return teamID;
        }

        //GET MISSION TIME #2
        public static string getMissionTime()
        {
            return missionTime;
        }

        //GET PACKET COUNT #3
        public static int getPacketCount()
        {
            return packetCount;
        }

        //GET HEIGHT #4
        public static float getHeight()
        {
            return height;
        }

        //GET PRESSURE #5
        public static float getPressure()
        {
            return pressure;
        }

        //GET TEMPERATURE #6
        public static float getTemperature()
        {
            return temperature;
        }

        //GET VOLTAGE #7
        public static float getVoltage()
        {
            return voltage;
        }

        //GET GPS TIME #8
        public static string getGpsTime()
        {
            return gpstime;
        }

        //GET LONGTITUDE #9
        public static float getLongtitude()
        {
            return longtitude;
        }

        //GET LENGTITUDE #10
        public static float getLatitude()
        {
            return latitude;
        }

        //GET SPEED #11
        public static float getSpeed()
        {
            return speed;
        }

        //GET GPS SATTELITE COUNT #12
        public static string getGpsSatelliteCount()
        {
            return gpsSatteliteCount;
        }

        //GET FLIGHT SOFTWARE STAGE #13
        public static string getFsw()
        {
            return fswStage;
        }

        //GET HUMIDITY #14
        public static float getHumidity()
        {
            return humidity;
        }

        //GET PITCH #15
        public static string getPitch()
        {
            return pitch;
        }

        //GET ROLL #16
        public static string getRoll()
        {
            return roll;
        }

        //GET YAW #17
        public static string getYaw()
        {
            return yaw;
        }
        
        //GET Capture count #18
        public static string getCapture()
        {
            return capture;
        }
    }
}