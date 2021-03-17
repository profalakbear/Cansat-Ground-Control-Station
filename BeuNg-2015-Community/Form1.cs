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
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace BeuNg_2015_Community
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        const string MAIN_DIR = "E:\\";
        const string TXT_PATH = MAIN_DIR + "\\Cansat\\degrees.txt";
        const string EXCEL_PATH = MAIN_DIR + "\\Cansat\\cansat.xlsx";
        Excel excelObject = new Excel(@"E:\docs\Cansat\YIS_OUTPUT\2591.xlsx", 1);
        public static Form1 Self;
        static string mySerialCommunicationPort;
        static int myBaudRate;
        static List<string> myParsedDataFromSerial;
        public static bool program_state = false;
        static int xDimension = 1;
        static int captureFlag = 0;
        static string captureCount = "";
        static bool vision = true;

        public Form1()
        {
            Self = this;
            InitializeComponent();
            Utilities.getAvailablePorts();
            IfNotExists.createAllIfNotExists(MAIN_DIR);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Stop();
        }


        //static void threadingCallback(object args)
        //{
        //    incomingSpeed = Services.getSpeed(myParsedDataFromSerial);
        //    incomingHeight = Services.getHeight(myParsedDataFromSerial);
        //    incomingPressure = Services.getPressure(myParsedDataFromSerial);
        //    incomingTemperature = Services.getTemperature(myParsedDataFromSerial);
        //    incomingHumidity = Services.getHumidity(myParsedDataFromSerial);
        //    incomingVoltage = Services.getVoltage(myParsedDataFromSerial);
        //}

        //Updating plotting charts in this method
        private void timer1_Tick(object sender, EventArgs e)
        {
            myParsedDataFromSerial = DataFromSerial.getDataFromSerialPort(mySerialCommunicationPort, myBaudRate);
            System.IO.File.AppendAllText(@"E:\\docs\\Cansat\\YIS_OUTPUT\\raw_telemetry.txt", String.Join(",",myParsedDataFromSerial));
            Services.push_back(myParsedDataFromSerial);

            TeamIDlbl.Text = Services.getTeamId();
            missionTime.Text = Services.getMissionTime();
            packetCountLbl.Text = Services.getPacketCount().ToString();
            chart2.Series["Hündürlük"].Points.AddY(Services.getHeight());
            chart4.Series["Təzyiq"].Points.AddY(Services.getPressure());
            chart3.Series["Temperatur"].Points.AddY(Services.getTemperature());
            circularProgressBar1.Value = (int)(Services.getVoltage() * 100.0f);
            circularProgressBar1.Text = string.Format("{0:0.0}\r\nV", Services.getVoltage());
            gpsTime.Text = Services.getGpsTime();
            Longtitude.Text = Services.getLongtitude().ToString();
            Latitude.Text = Services.getLatitude().ToString();
            GpsSatelliteCount.Text = Services.getGpsSatelliteCount();
            chart1.Series["Sürət"].Points.AddY(Services.getSpeed());
            fswState.Text = Services.getFsw();
            circularProgressBar2.Value = (int)(Services.getHumidity() * 100.0f);
            circularProgressBar2.Text = string.Format("{0:0.0}\r\n%", Services.getHumidity());
            System.IO.File.WriteAllText(@"E:\\docs\\Cansat\\YIS_OUTPUT\\degrees.txt",
                                        Services.getPitch() + " " +
                                        Services.getRoll() + " " +
                                        Services.getYaw()
                );
            captureLabel.Text = Services.getCapture();

            GMap.NET.PointLatLng point = new GMap.NET.PointLatLng(Services.getLatitude(), Services.getLongtitude());
            Bitmap mrkr = (Bitmap)Image.FromFile("img/marker.png");
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.blue_dot);
            GMapOverlay markers = new GMapOverlay("markers");
            markers.Markers.Add(marker);
            map.Overlays.Add(markers);
            if ( vision && Services.getLatitude() != 1000.0 && Services.getLongtitude() != 1000.0)
            {
                map.Position = new GMap.NET.PointLatLng(Services.getLatitude(), Services.getLongtitude());
                vision = false;
            }

            xDimension=Services.getPacketCount();
            excelObject.WriteToCell(xDimension, 0, Services.getTeamId());
            excelObject.WriteToCell(xDimension, 1, Services.getMissionTime());
            excelObject.WriteToCell(xDimension, 2, Services.getPacketCount().ToString());
            excelObject.WriteToCell(xDimension, 3, Services.getHeight().ToString());
            excelObject.WriteToCell(xDimension, 4, Services.getPressure().ToString());
            excelObject.WriteToCell(xDimension, 5, Services.getTemperature().ToString());
            excelObject.WriteToCell(xDimension, 6, Services.getVoltage().ToString());
            excelObject.WriteToCell(xDimension, 7, Services.getGpsTime());
            excelObject.WriteToCell(xDimension, 8, Services.getLatitude().ToString());
            excelObject.WriteToCell(xDimension, 9, Services.getLongtitude().ToString());
            excelObject.WriteToCell(xDimension, 10, Services.getGpsSatelliteCount());
            excelObject.WriteToCell(xDimension, 11, Services.getSpeed().ToString());
            excelObject.WriteToCell(xDimension, 12, Services.getFsw());
            excelObject.WriteToCell(xDimension, 13, Services.getHumidity().ToString());
            excelObject.WriteToCell(xDimension, 14, Services.getPitch());
            excelObject.WriteToCell(xDimension, 15, Services.getRoll());
            excelObject.WriteToCell(xDimension, 16, Services.getYaw());
            excelObject.WriteToCell(xDimension, 17, Services.getCapture());

            excelObject.Save();
            if (captureCount == Services.getCapture() && captureFlag == 0) {
                DataFromSerial.captureCommand();
                captureFlag = 1;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        //Start mission
        private void button7_Click(object sender, EventArgs e)
        {

            //System.Threading.Timer threadingTimer = new System.Threading.Timer(threadingCallback, 10, 1, 100);
            program_state = true;
            serialPort1.Close();

            try
            {
                if (comPort.SelectedItem != null || baudRate.SelectedItem != null)
                {
                    mySerialCommunicationPort = comPort.SelectedItem.ToString();
                    myBaudRate = Convert.ToInt32(baudRate.SelectedItem.ToString());
                    serialPort1.BaudRate = Convert.ToInt32(baudRate.SelectedItem.ToString());
                    serialPort1.PortName = comPort.SelectedItem.ToString();
                    serialPort1.Open();
                    serialPort1.Write("s");
                    myParsedDataFromSerial = DataFromSerial.getDataFromSerialPort(mySerialCommunicationPort, myBaudRate);
                    timer1.Enabled = true;
                    timer1.Start();

                    map.DragButton = MouseButtons.Left;
                    map.MapProvider = GMapProviders.GoogleMap;
                    //map.Position = new GMap.NET.PointLatLng(40.4093, 49.8671);
                    map.MinZoom = 5;
                    map.MaxZoom = 20;
                    map.Zoom = 15;
                    map.ShowCenter = false;

                    MessageBox.Show("Missiya Basladi");
                }
                else
                {
                    MessageBox.Show("Missiyani bawlatmaq ucun port ve baud reyti secin");
                }
            }
            catch (Exception ex)
            {
                program_state = false;
                MessageBox.Show(" Xeta bash verdi! " + ex.Message);
            }
        }

        //Refresh button
        private void button3_Click(object sender, EventArgs e)
        {
            Buttons.refreshButton();
        } 

        //Reset button
        private void button6_Click(object sender, EventArgs e)
        {
            Buttons.resetButton();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            if(program_state == true)
            {
                program_state = false;
                excelObject.Close();
                serialPort1.Write("i");
                MessageBox.Show("Missiyanı sonlandır əmri UĞURLU alındı.");
            }
            else
            {
                MessageBox.Show("Missiyani bawlatmamisiz");
            }
            
        }

        private void circularProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogsWindow lgs = new LogsWindow();
            lgs.Show();
            
        }

        private void button5_Click(object sender, EventArgs e)
        { 
            DataFromSerial.releaseCommand();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            captureCount = Services.getCapture();
            captureFlag = 0;
        }

        private void chart4_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
