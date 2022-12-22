using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.Models
{
    public class ExcelCells
    {
        public int ExcellCellsId { get; set; } 
        public string? Cells { get; set; }
        public double Value { get; set; }

    }
}
