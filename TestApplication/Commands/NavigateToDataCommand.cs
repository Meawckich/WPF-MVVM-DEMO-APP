using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using TestApplication.DbContext;
using TestApplication.Stores;
using TestApplication.ViewModels;
using TestApplication.Views;
using DataView = TestApplication.Views.DataView;

namespace TestApplication.Commands
{
    class NavigateToDataCommand : CommandBase
    {
        private NavigationStore _store;
        private DataViewModel _viewModel;
        private LoginViewModel _loginViewModel;
        private SqLiteDbContext _context;
        public NavigateToDataCommand(NavigationStore store , DataViewModel viewModel, LoginViewModel loginViewModel, SqLiteDbContext context)
        {
            _store = store;
            _viewModel= viewModel;
            _loginViewModel = loginViewModel;
            _context = context;
        }
        public override void Execute(object? parameter)
        {
            if (_loginViewModel.Login is null || _loginViewModel.Login == string.Empty)
            {
                _loginViewModel.ErrorMessage?.Enqueue("Заполните логин", null, null, null, false, true,
                    TimeSpan.FromSeconds(2));
                return;
            }

            if (_loginViewModel.Password is null || _loginViewModel.Password == string.Empty)
            {
                _loginViewModel.ErrorMessage?.Enqueue("Заполните пароль", null, null, null, false, true,
                    TimeSpan.FromSeconds(2));
                return;
            }

            var findUser = _context.users.FirstOrDefault(x => x.Login == _loginViewModel.Login && x.PassWord == _loginViewModel.Password);

            if (findUser == null) 
            {
                _loginViewModel.ErrorMessage?.Enqueue("Пользователя не существует", null, null, null, false, true,
                    TimeSpan.FromSeconds(2));
                return;
            }
            _store.OnCurrentViewChanged(new DataView(_viewModel));
        }
    }
}
   

