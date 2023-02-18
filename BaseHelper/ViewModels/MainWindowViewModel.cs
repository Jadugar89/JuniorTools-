using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BaseHelper.ViewModels.Factories;
using Microsoft.Toolkit.Mvvm.Input;
using BaseHelper.Enum;

namespace BaseHelper.ViewModels
{

    public class MainWindowViewModel :ViewModelBase
    {
        private readonly IViewModelFactory viewModelFactory;
        private ViewModelBase currentView;
        private ViewType currentViewType;
        public ViewModelBase CurrentView
        {
            get { return currentView; }
            set { currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public IRelayCommand BtnSwitchView { get; set; }

        public MainWindowViewModel(IViewModelFactory viewModelFactory)
        {
            
            this.viewModelFactory = viewModelFactory;
            this.CurrentView = viewModelFactory.CreateViewModel(ViewType.BaseHelper);
            this.currentViewType = ViewType.BaseHelper;
            BtnSwitchView = new RelayCommand<ViewType>(SwitchView);
        }

        private void SwitchView(ViewType viewType)
        {
            if (currentViewType != viewType)
            {
                this.CurrentView = this.viewModelFactory.CreateViewModel(viewType);
                this.currentViewType = viewType;
            }
        }
    }


}
