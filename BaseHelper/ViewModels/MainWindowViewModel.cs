using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BaseHelper.Command;
using BaseHelper.User_Control;

namespace BaseHelper.ViewModels
{

    public class MainWindowViewModel :ViewModelBase
    {
        private ViewModelBase _currentView;

        public ViewModelBase CurentView
        {
            get { return _currentView; }
            set { _currentView = value; }
        }

        public ViewModelBase? CurrentView { set; get; }

        public MainWindowViewModel()
        {
            CurrentView = new BaseHelperViewModel();

        }
    }


}
