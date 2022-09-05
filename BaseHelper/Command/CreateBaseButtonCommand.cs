using BaseSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualBasic;
using System.Configuration;

namespace BaseHelper.Command
{
    public class CreateBaseButtonCommand : ICommand
    {
        

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }
        public void Execute(object? parameter)
        {
            
            string name = Interaction.InputBox("Write new Base Name", "CreateBaseName");
            if (name != null)
            {
                Sql_Manager sql_Manager = new Sql_Manager(ConfigurationManager.ConnectionStrings["NameOfConnectionStringInConfig"].ConnectionString);
                sql_Manager.CreateDateBase(name);
            }

        }
    }
}
