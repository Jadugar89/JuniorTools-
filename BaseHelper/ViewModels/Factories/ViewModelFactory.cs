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

        public ViewModelFactory(CreateViewModel<BaseHelperViewModel> baseHelperViewModel,
                                CreateViewModel<XmlReaderViewModel> xmlReaderViewModel){
            this.baseHelperViewModel = baseHelperViewModel;
            this.xmlReaderViewModel = xmlReaderViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.BaseHelper:
                    return baseHelperViewModel();
                case ViewType.XmlReader:
                    return xmlReaderViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }
        }
    }
}
