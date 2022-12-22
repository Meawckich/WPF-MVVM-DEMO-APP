using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace TestApplication.DataReaders
{
    public interface IReader
    {
        void ReadData();
        void GetPath();
        IEnumerable<string> GetCells(Range range, Worksheet sheet, int id);
    }
}
