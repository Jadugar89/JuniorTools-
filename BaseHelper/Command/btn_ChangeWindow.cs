using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BaseHelper.User_Control;
using BaseHelper.ViewModels;
using BaseHelper.Views;

namespace BaseHelper.Command
{
    internal class btn_ChangeWindow : CommandBase
    {
        private NavigationViewModel _navigationViewModel;

        public btn_ChangeWindow(NavigationViewModel navigationViewModel)
        {
            _navigationViewModel = navigationViewModel;
        }
        public override void Execute(object? parameter)
        {
           
          
        }


    }
}
