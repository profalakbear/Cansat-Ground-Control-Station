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

        public static void refreshbutton()
        {
            countCombo = Form1.Self.comPort.Items.Count;

            if (countCombo == null || countCombo == 0)
            {
                Utilities.getAvailablePorts();
            }
        }
    }
}
