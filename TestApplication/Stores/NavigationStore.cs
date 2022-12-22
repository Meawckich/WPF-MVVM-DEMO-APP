using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestApplication.ViewModels;

namespace TestApplication.Stores
{
    public class NavigationStore
    {
        public event Action<Page>? CurrentViewChanged;
        public void OnCurrentViewChanged(Page page) => CurrentViewChanged?.Invoke(page);
    }
}
