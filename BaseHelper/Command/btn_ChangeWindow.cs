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
            if (parameter!=null)
            {


                if (int.TryParse(parameter.ToString(), out int value))
                {
                    if (value == 1)
                    {
                        _navigationViewModel.CurrentView = new BaseHelperViewModel();

                    }
                    else
                    {
                        _navigationViewModel.CurrentView = new XmlReaderViewModel();
                    }
                }
            }
          
        }


    }
}
