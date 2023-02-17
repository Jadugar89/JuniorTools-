using BaseHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace BaseHelper.Services.Navigators
{
    public class Navigator : INavigator
    {
        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }
            set
            {
                currentViewModel?.Dispose();

                currentViewModel = value;
                StateChanged?.Invoke();
            }
        }

        public event Action StateChanged;

    }
}
