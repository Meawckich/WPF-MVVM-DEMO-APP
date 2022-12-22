using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TestApplication.DataReaders;
using Application = Microsoft.Office.Interop.Excel.Application;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace TestApplication.Excel
{
    public class ExcelReader : IReader
    {
        private IEnumerable<string>? _cellsA;
        private IEnumerable<string>? _cellsF;
        private string? _path;

        public IEnumerable<string> CellsA
        {
            get { return _cellsA!; }
            set { _cellsA = value; }
        }

        public IEnumerable<string> CellsF
        {
            get { return _cellsF!; }
            set { _cellsF = value; }
        }
        public void GetPath()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog()
                {
                    Multiselect = false,
                    Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"
            };

                dialog.ShowDialog();

                _path = dialog.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        public void ReadData()
        {
            GetPath();
            try
            {
                Application excelApp = new Application();
                Workbook excelBook = excelApp.Workbooks.Open(_path);
                Worksheet excelSheet = excelBook.Sheets[1];
                Range excelRange = excelSheet.UsedRange;
                _cellsA = GetCells(excelRange,excelSheet, 1).ToList();
                _cellsF = GetCells(excelRange, excelSheet, 2).ToList();

                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex.Message);
            }
        }

        public IEnumerable<string> GetCells(Range range,Worksheet sheet, int columnId)
        {
            int rows = range.Rows.Count;

            for (int i = 1; i < rows; i++)
            {
                yield return sheet.Cells[i,columnId].Value == null ? string.Empty : sheet.Cells[i, columnId].Value.ToString();
            }
        }
    }
}
