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
        static List<string> mySensorData;

        //STRING PARSER FOR COMMA
        public static List<string> parseStringByComma(string dataParsedByComma)
        {
            mySensorData = dataParsedByComma.Split(',').ToList<string>();
            return mySensorData;
        }
    }
}

