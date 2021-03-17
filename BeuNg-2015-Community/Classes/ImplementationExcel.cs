using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeuNg_2015_Community.Classes
{
    public static class ImplementationExcel
    {
        public static void OpenFile()
        {
            Excel excel = new Excel(@"C:\Users\alakbar\Desktop\myexcel.xlsx", 1);
            MessageBox.Show(excel.ReadCells(0, 0));
        }

        public static void WriteData()
        {
            Excel excel = new Excel(@"C:\Users\asima\Desktop\myexcel.xlsx", 1);
            excel.WriteToCell(1, 1, "Salamlaaaaaaaaaaaaaaaaaaaaaaaarkkkkkkkkkkkkkkkkk");
            excel.Save();
            excel.Close();
        }
    }
}
