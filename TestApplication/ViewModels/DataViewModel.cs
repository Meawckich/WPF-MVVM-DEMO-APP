using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TestApplication.Commands;
using TestApplication.DataDTOs;
using TestApplication.DbContext;
using TestApplication.Excel;
using TestApplication.Models;
using TestApplication.Stores;

namespace TestApplication.ViewModels
{
    public class DataViewModel : ViewModelBase
    {
        private string? _password;
        private ObservableCollection<ListViewDTO>? _listItems;
        private ObservableCollection<ExcelCells>? _sqlListItems;
        public ObservableCollection<ListViewDTO>? ListItems
        {
            get => _listItems;
            set
            {
                _listItems = value;
                OnPropertyChanged(nameof(ListItems));
            }
        }
        public ObservableCollection<ExcelCells>? SqlListItems
        {
            get => _sqlListItems;
            set
            {
                _sqlListItems = value;
                OnPropertyChanged(nameof(SqlListItems));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public ICommand? OpenDialog { get; }
        public ICommand? SaveToSql { get; }

        public DataViewModel(ExcelReader? reader, SqLiteDbContext context)
        {
            OpenDialog = new OpenDialogCommand(reader!, this);
            SaveToSql = new SaveToSqLCommand(context, this);
        }

    }
}
