using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using TestApplication.Stores;
using TestApplication.Views;

namespace TestApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private NavigationStore _store;
        private Page? _currentView;

        public Page? CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainViewModel(NavigationStore store,LoginViewModel viewModel)
        {
            _store = store;
            _store.CurrentViewChanged += (page) => CurrentView = page;
            _store.OnCurrentViewChanged(new LoginView(viewModel));
        }

        //public void OnViewChanged()
        //{
        //    OnPropertyChanged(nameof(CurrentView));
        //}
    }
}
