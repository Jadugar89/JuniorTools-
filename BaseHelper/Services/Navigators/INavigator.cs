using BaseHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BaseHelper.Services.Navigators
{
    public enum ViewType
    {
     BaseHelper,
     XmlReader,
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
