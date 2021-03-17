using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeuNg_2015_Community
{
    public class Buttons
    {
        static int countCombo;

        //Refresh button Click Event
        public static void refreshButton()
        {
            countCombo = Form1.Self.comPort.Items.Count;


            if(Form1.program_state == false)
            {
                if (countCombo == 0)
                {
                    Utilities.getAvailablePorts();
                }
            }
            else
            {
                MessageBox.Show("Program calishan zaman yenilemek olmaz.", 
                                "XEBERDARLIQ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Reset button Click Event
        public static void resetButton()
        {
            if(Form1.program_state == false)
            {
                Form1.Self.serialPort1.Close();
                Form1.Self.comPort.SelectedIndex = -1;
                Form1.Self.baudRate.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Program calishan zaman sifirlama olmaz.",
                                "XEBERDARLIQ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
