using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseHelper.Command;
using BaseHelper.ViewModels;

namespace BaseHelper.User_Control
{
    public class NavigationViewModel : INotifyPropertyChanged
    {
        public ICommand? ChangeWindow { get; set; }
        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; 
                OnPropertyChanged("CurrentView");
            }
        }


        public NavigationViewModel()
        {
            ChangeWindow = new btn_ChangeWindow(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
