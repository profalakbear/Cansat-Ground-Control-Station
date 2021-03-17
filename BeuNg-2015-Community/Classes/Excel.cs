using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace BeuNg_2015_Community.Classes
{
    public class Excel
    {
        string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;

        public Excel(string path, int Sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[Sheet];
        }

        public string ReadCells(int row, int col)
        {
            row++;
            col++;

            if (ws.Cells[row, col].Value2 != null)
            {
                return ws.Cells[row, col].Value2;
            }
            else
            {
                return "";
            }
        }

        public void WriteToCell(int row, int col, string my_string)
        {
            row++;
            col++;

            ws.Cells[row, col].Value2 = my_string;
        }

        public void Save()
        {
            wb.Save();
        }

        public void SaveAs(string path)
        {
            wb.SaveAs(path);
        }

        public void Close()
        {
            wb.Close();
        }
    }
}