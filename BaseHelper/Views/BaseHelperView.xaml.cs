using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BaseHelper.ViewModels;

namespace BaseHelper.Views
{
    /// <summary>
    /// Logika interakcji dla klasy BaseHelper.xaml
    /// </summary>
    public partial class BaseHelperView : Window
    {
        public BaseHelperView()
        {
            InitializeComponent();
            DataContext = new BaseHelperViewModel();
        }

    }
}
