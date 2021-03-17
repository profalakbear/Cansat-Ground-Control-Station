using BeuNg_2015_Community.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeuNg_2015_Community
{
    public partial class LogsWindow : Form
    {
        public static LogsWindow Self;
        public static string log_data;
        public LogsWindow()
        {
            InitializeComponent();
            Self = this;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lgs_timer_Tick(object sender, EventArgs e)
        {
            int b_rate;
            string s_port;
            b_rate = Convert.ToInt32(Form1.Self.baudRate.SelectedItem);
            s_port = Form1.Self.comPort.SelectedItem.ToString();

            log_data = DataFromSerial.getStringSerialData(s_port, b_rate);

            listBox1.Items.Add(log_data);
        }
    }
}
