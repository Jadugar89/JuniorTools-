using BaseHelper.Enum;
using BaseHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseHelper.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
