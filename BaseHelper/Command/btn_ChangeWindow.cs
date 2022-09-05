using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BaseHelper.ViewModels;
using BaseHelper.Views;

namespace BaseHelper.Command
{
    internal class btn_ChangeWindow : CommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter!=null)
            {
                Window window = (Window)parameter;
                window.DataContext = new XmlReaderViewModel();

                if (int.TryParse(parameter.ToString(), out int value))
                {
                    if (value == 1)
                    {
                        MessageBox.Show("Zrobione 1");
                    }
                    else
                    {
                        MessageBox.Show("Zrobione else");
                    }
                }
            }
            

            

        }


    }
}
