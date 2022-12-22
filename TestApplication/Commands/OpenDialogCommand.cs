using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using TestApplication.DataDTOs;
using TestApplication.Excel;
using TestApplication.ViewModels;
using Microsoft.Office.Interop.Excel;

namespace TestApplication.Commands
{
    public class OpenDialogCommand : CommandBase
    {
        private ExcelReader _excelReader;
        private DataViewModel _dataViewModel;
        public OpenDialogCommand(ExcelReader reader, DataViewModel dataViewModel)
        {
            _excelReader = reader;
            _dataViewModel = dataViewModel;
        }
        public override void Execute(object? parameter)
        {
            _excelReader.ReadData();

            try
            {
                IEnumerable<ListViewDTO> list = from a in _excelReader!.CellsA
                                                join f in _excelReader!.CellsF on _excelReader.CellsA.ToList().IndexOf(a) equals _excelReader.CellsF.ToList().IndexOf(f)
                                                select new ListViewDTO { CellA = a, CellF = Convert.ToDouble(f) };

                _dataViewModel.ListItems = new ObservableCollection<ListViewDTO>(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
