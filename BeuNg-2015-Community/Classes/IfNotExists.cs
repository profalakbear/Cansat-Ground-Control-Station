using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeuNg_2015_Community.Classes
{
    public class IfNotExists
    {
        static string subdir = "\\docs\\Cansat\\YIS_OUTPUT\\";
        static string fullDirPath = "";
        const string txtFile = "degrees.txt";
        static string fullTxt = "";
        const string excelFile = "cansat.xlsx";
        static string fullExcel = "";

        public static void createAllIfNotExists(string main_dir)
        {
            fullDirPath = main_dir + subdir;

            //Create directory if not exists
            if (!Directory.Exists(fullDirPath))
            {
                Directory.CreateDirectory(fullDirPath);
                
                //Create excel file is not exists
                fullExcel = fullDirPath + excelFile;

                if (!File.Exists(fullExcel))
                {
                    Excel excel = new Excel(fullExcel, 1);  
                }
            }
        }

    }
}
