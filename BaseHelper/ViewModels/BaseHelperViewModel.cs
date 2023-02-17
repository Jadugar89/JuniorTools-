using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseHelper.Command;
using BaseHelper.Models;
using BaseSQL;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using ClassMaker;
using System.Windows.Controls;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

namespace BaseHelper.ViewModels
{
    public class BaseHelperViewModel : ViewModelBase
    {
        
    
        public IRelayCommand? BtnCreateDB { get; set; }
        public IRelayCommand? BtnCreateTable { get ; set; }
        private ObservableCollection<object> _tableData;
        public ObservableCollection<object> _TableData { 
            get
            {
                return _tableData;
            }
            set
            {
                if (value != _tableData)
                {
                    _tableData = value;
                    OnPropertyChanged("_TableData");
                }             
            }
        }

        private string? _choosenDB;
        private string? _choosenTable;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public string? ChoosenDB
        {
            get => _choosenDB;
            set
            {
                _choosenDB = value;
                
                if (!string.IsNullOrWhiteSpace(_choosenDB))
                {
                    TableNames = new Sql_Manager(ConStirng).ReadAllTable(_choosenDB).ToList();
                }
               
                OnPropertyChanged("ChoosenDB");

            }
        }

        public string? ChoosenTable
        {
            get => _choosenTable;
            set
            {
                _choosenTable = value;
                if (!string.IsNullOrWhiteSpace(_choosenTable)  && _choosenDB!=null)
                {
                    var Columns = new Sql_Manager(ConStirng).GetColumnNames(_choosenDB, _choosenTable).ToArray();
                    ManagerClassMaker classMaker = new ManagerClassMaker();
                    Type dynType =classMaker.CreateTypeFromTable(Columns);

                    var listTable = new List<object>(); //classMaker.CreateList(dynType);
                    
                    var data = new Sql_Manager(ConStirng).ReadWholeTable(_choosenDB, _choosenTable).ToArray();
                    foreach(var row in data)
                    {
                       var test= classMaker.AddDatatoFields(dynType, row.ToArray());
                        listTable.Add(test);    
                    }
                    _TableData = new ObservableCollection<object>(listTable);
                    OnPropertyChanged("_TableData");
                    
                }
            }

        }
        public List<string> DBinfoCollection { get; set; }
        public List<string> TableNames { get; set; }
        public object SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {

                _selectedItem = value;
                OnPropertyChanged("selectedItem");
            }
        }
        private object _selectedItem;


        public BaseHelperViewModel()
        {
            BtnCreateTable = new RelayCommand<object>(CreateTable);
            BtnCreateDB = new RelayCommand(CreateDB);
            /*
             var Columns = new Sql_Manager(ConStirng).GetColumnNames("Restauracje", "Dobre").ToArray();
             ManagerClassMaker classMaker = new ManagerClassMaker();
             Type dynType = classMaker.CreateTypeFromTable(Columns);

             var listTable = new List<object>(); 

             var data = new Sql_Manager(ConStirng).ReadWholeTable("Restauracje", "Dobre").ToArray();
             foreach (var row in data)
             {
                 var test = classMaker.AddDatatoFields(dynType, row.ToArray());
                 listTable.Add(test);
             }


             
             Sql_Manager sql_Manager = new Sql_Manager(ConStirng);
             DBinfoCollection = sql_Manager.ReadAllDatebases().ToList();
             //CreateTable = new CreateTableButtonCommand();
             TableNames = new List<string>(); //(List<string>)ReadTable.ReadExistingTableINDB("master");



             //_TableData = new ObservableCollection<object>(listTable);

             */
        }

        private void CreateTableFunc(object? parameter)
        {
            if (parameter != null)
            {

                string name = Interaction.InputBox("Write new Table Name", "CreateTable");
                if (name != null)
                {
                    name = name.Replace(" ", "_");
                    string stringquery = $@"CREATE TABLE {name} ( 
                                    PersonID int,
                                    LastName varchar(255),
                                    FirstName varchar(255),
                                    Address varchar(255),
                                    City varchar(255) );";
                    new Sql_Manager(ConStirng).CreateTable(stringquery,parameter.ToString());
                }
            }
        }
        private void CreateDB()
        {

            string name = Interaction.InputBox("Write new Base Name", "CreateBaseName");
            if (name != null)
            {
                Sql_Manager sql_Manager = new Sql_Manager(ConfigurationManager.ConnectionStrings["NameOfConnectionStringInConfig"].ConnectionString);
                sql_Manager.CreateDateBase(name);
            }
        }
 
        private void selectedItem_PropertyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Test");
        }
    }
}

