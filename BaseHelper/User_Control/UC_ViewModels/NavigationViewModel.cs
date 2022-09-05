using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseHelper.Command;

namespace BaseHelper.User_Control
{
    internal class NavigationViewModel : INotifyPropertyChanged
    {
        public ICommand? ChangeWindow { get; set; }

        public NavigationViewModel()
        {
            
            ChangeWindow = new btn_ChangeWindow();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}
