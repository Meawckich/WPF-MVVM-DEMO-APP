using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestApplication.DataDTOs;
using TestApplication.DbContext;
using TestApplication.Excel;
using TestApplication.Models;
using TestApplication.ViewModels;

namespace TestApplication.Commands
{
    public class SaveToSqLCommand : CommandBase
    {
        private SqLiteDbContext _context;
        private DataViewModel _dataViewModel;
        private readonly List<string> _neededValuesTopColumn = new List<string>{ "C01","D01","F01"};
        private readonly List<string> _neededValuesLeftColumn = new List<string> { "E01", "G01", "H01" };
        public SaveToSqLCommand(SqLiteDbContext context, DataViewModel dataViewModel)
        {
            _context = context;
            _dataViewModel = dataViewModel;
        }
        public override void Execute(object? parameter)
        {
            if (_dataViewModel.Password != "12345")
            {
                MessageBox.Show("Введите правильный пароль базы данных");
                return;
            }
                

            List<ExcelCells> excels = new(); 
            var newEntities = _dataViewModel.ListItems as IEnumerable<ListViewDTO>;
            if (newEntities is null)
                return;

            newEntities = newEntities
                .Where(x => _neededValuesTopColumn.Contains(x.CellA!) || _neededValuesLeftColumn.Contains(x.CellA!))
                .Select(x => x);

            var data = _context.cells.ToList();
            _context.cells.RemoveRange(data);

            for (int i = 0; i < _neededValuesTopColumn.Count; i++)
            {
                for(int j = 0; j < _neededValuesLeftColumn.Count; j++)
                {
                    ExcelCells cell = new ExcelCells()
                    {
                        Cells = $"{_neededValuesTopColumn[i]}_{_neededValuesLeftColumn[j]}",
                        Value = Math.Round(Math.Pow(newEntities.ToList().Where(x => x.CellA == _neededValuesTopColumn[i]).Select(x => x.CellF).First() - newEntities.ToList().Where(x => x.CellA == _neededValuesLeftColumn[j]).Select(x => x.CellF).First(), 2.00), 2)
                    };
                    excels.Add(cell);
                }
            }




            _context.AddRange(excels);
            _context.SaveChanges();

            _dataViewModel.SqlListItems = new ObservableCollection<ExcelCells>(_context.cells.ToList());
            
        }
    }
}
