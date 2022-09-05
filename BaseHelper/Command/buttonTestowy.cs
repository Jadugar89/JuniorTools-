using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BaseHelper.Command
{
    internal class buttonTestowy : CommandBase
    {
        
        

        public override void Execute(object? parameter)
        {
            MessageBox.Show("dziala testowy");
        }
    }
}
