using BaseSQL.DB;
using BaseSQL._DbProviderFactory;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Data.Common;
using System.Data;
using Microsoft.Win32;

namespace BaseSQL
{
    public class SqlManager
    {
        public string connectionString { get; }

        public SqlManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<string> ReadAllTable(string DbName)
        {
             return TableManager.ReadExistingTableInDB(connectionString, DbName);
        }
        public IEnumerable<IEnumerable<string>> ReadWholeTable(string DbName, string TableName)
        {
            return TableManager.ReadWholeTable(connectionString, DbName, TableName);    
        }
        public bool CreateTable(string QueryString,string DbName)
        {
            return TableManager.CreateTable(connectionString, QueryString, DbName);
         }
        public IEnumerable<string> ReadAllDatebases()
        {
            return ReadDatebases.ReadExistingDatebase(connectionString);
        }
        public bool CreateDateBase(string DBName)
        {
            BaseBuilder baseBuilder = new BaseBuilder();
            return baseBuilder.CreateBase(connectionString, DBName);
        }
        public IEnumerable<string> GetColumnNames(string DBName, string TableName)
        {
            string queryString = $"SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('{TableName}') ";
            List<string> list = new List<string>();
            var Result = new Query().CreateQuery(connectionString, queryString, DBName);
            foreach (var d in Result)
            {
                var tempArray = d.ToArray();
                list.Add(tempArray[0]);
            }
            return list;
        }
        public bool AddRecord(string[] Values, string DBName, string TableName)
        {
            string[] ColumnNames = GetColumnNames(DBName, TableName).ToArray();
            if(ColumnNames.Length!= Values.Length)
            {
                return false;
            }
            StringBuilder StringQuery = new StringBuilder();
            StringQuery.Append($"INSERT INTO {TableName}(");
            StringQuery.AppendJoin(", ", ColumnNames);
            StringQuery.Append(") VALUES(");
            StringQuery.AppendJoin(", ", Values);
            StringQuery.Append(");");

            var Result = new Query().CreateQuery(connectionString, StringQuery.ToString(), DBName);
            return true;
        }
        public static void GetDataSources()
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);
            var test = DbProviderFactories.GetFactory("Microsoft.Data.SqlClient");

            var test2 = DbProviderFactories.GetProviderInvariantNames();

            DataTable table = DbProviderFactories.GetFactoryClasses();

            // Display each row and column value.
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    Console.WriteLine(row[column]);
                }
            }

            
        }

    }
}

