using BaseHelper.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseHelper.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<BaseHelperViewModel> baseHelperViewModel;
        private readonly CreateViewModel<XmlReaderViewModel> xmlReaderViewModel;
        private readonly CreateViewModel<WeatherViewModel> weatherViewModel;

        public ViewModelFactory(CreateViewModel<BaseHelperViewModel> baseHelperViewModel,
                                CreateViewModel<XmlReaderViewModel> xmlReaderViewModel,
                                CreateViewModel<WeatherViewModel> weatherViewModel
            )
        {
            this.baseHelperViewModel = baseHelperViewModel;
            this.xmlReaderViewModel = xmlReaderViewModel;
            this.weatherViewModel = weatherViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.BaseHelper:
                    return baseHelperViewModel();
                case ViewType.XmlReader:
                    return xmlReaderViewModel();
                case ViewType.Weather:
                    return weatherViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", nameof(ViewType));
            }
        }
    }
}
