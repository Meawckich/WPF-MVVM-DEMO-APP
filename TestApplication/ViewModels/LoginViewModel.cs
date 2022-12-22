using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Input;
using TestApplication.Commands;
using TestApplication.DbContext;
using TestApplication.Stores;
using TestApplication.Views;

namespace TestApplication.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private NavigationStore _navstore;
        private string? _login;
        private string? _password;
        private SnackbarMessageQueue? _errorMessage;
        public ICommand? NavigationCommand { get; }
        public string? Login
        {
            get => _login;
            set
            {
                _login= value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string? Password
        {
            get => _password;
            set
            {
                _password= value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public SnackbarMessageQueue? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public LoginViewModel(NavigationStore store,DataViewModel dataViewModel, SqLiteDbContext context)
        {
            _navstore = store;
            _errorMessage = new SnackbarMessageQueue();
            NavigationCommand = new NavigateToDataCommand(_navstore, dataViewModel, this, context);

        }
    }
}
