using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using System.Windows.Controls;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using BaseHelper.Services;
using System.Data.Common;

namespace BaseHelper.ViewModels
{
    public class BaseHelperViewModel : ViewModelBase
    {
        
    
        public IRelayCommand? BtnCreateDB { get; set; }
        public IRelayCommand? BtnCreateTable { get ; set; }

        private List<object> originalTableData;
        private List<object> toSaveTableData;

        private ObservableCollection<object> tableData;
        public ObservableCollection<object> TableData { 
            get
            {
                return tableData;
            }
            set
            {
                if (value != tableData)
                {
                    tableData = value;
                    OnPropertyChanged(nameof(TableData));
                }             
            }
        }
        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { searchText = value;
                if (searchText != null)
                {

                }
                OnPropertyChanged(nameof(SearchText));

            }
        }


        private string? chosenDB;
        private string? chosenTable;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public string? ChosenDB
        {
            get => chosenDB;
            set
            {
                chosenDB = value;
                if (!string.IsNullOrWhiteSpace(chosenDB))
                {
                    TableNames = dataBaseService.ReadAllTableFromDB(chosenDB).ToList();
                }
                OnPropertyChanged("ChoosenDB");
            }
        }

        public string? ChosenTable
        {
            get => chosenTable;
            set
            {
                chosenTable = value;
                if (!string.IsNullOrWhiteSpace(chosenTable) && chosenDB != null)
                {
                    var columns = dataBaseService.GetColumnNames(chosenDB, chosenTable).ToArray();
                    var data = dataBaseService.ReadWholeTable(chosenDB, chosenTable).ToArray();
                    TableData=tableService.GetTable(columns, data);
                    OnPropertyChanged(nameof(ChosenTable));
                }
            }
        }

        private List<string> dbInfoCollection;

        public List<string> DbInfoCollection
        {
            get { return dbInfoCollection; }
            set { dbInfoCollection = value;
                OnPropertyChanged(nameof(DbInfoCollection));
            }
        }

        private List<string> tableNames;
        public List<string> TableNames
        {
            get { return tableNames; }
            set { tableNames = value;
                OnPropertyChanged(nameof(TableNames));
            }
        }

        public object SelectedItem
        {
            get {return _selectedItem;}
            set
            {
                _selectedItem = value;
                OnPropertyChanged("selectedItem");
            }
        }
        private object _selectedItem;
        private readonly IDataBaseService dataBaseService;
        private readonly ITableService tableService;

        public BaseHelperViewModel(IDataBaseService dataBaseService,ITableService tableService)
        {
            BtnCreateTable = new RelayCommand<object>(CreateTable);
            BtnCreateDB = new RelayCommand(CreateDB);
            this.dataBaseService = dataBaseService;
            this.tableService = tableService;
            DbInfoCollection = dataBaseService.ReadAllDatebases().ToList();
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
        
        //CreateTable = new CreateTableButtonCommand();
        TableNames = new List<string>(); //(List<string>)ReadTable.ReadExistingTableINDB("master");



        //_TableData = new ObservableCollection<object>(listTable);

        */
        }

        private void CreateTable(object? parameter)
        {
            if (parameter != null)
            {
                var dbName = parameter.ToString();
                string name = Interaction.InputBox("Write new Table Name", "CreateTable");
                if (!String.IsNullOrEmpty(dbName) && !String.IsNullOrEmpty(name))
                {
                    dataBaseService.CreateTable(name, dbName);
                    TableNames = dataBaseService.ReadAllTableFromDB(dbName).ToList();
                }
            }
        }
        private void CreateDB()
        {

            string name = Interaction.InputBox("Write new Base Name", "CreateBaseName");
            if (!String.IsNullOrEmpty(name))
            {
                dataBaseService.CreateDB(name);
                DbInfoCollection = dataBaseService.ReadAllDatebases().ToList();
            }
        }
 
        private void selectedItem_PropertyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Test");
        }
    }
}

