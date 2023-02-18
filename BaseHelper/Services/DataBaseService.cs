using BaseSQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseHelper.Services
{
    public class DataBaseService : IDataBaseService
    {

        private SqlManager sqlManager;

        public DataBaseService(IConfiguration configuration)
        {
            sqlManager = new SqlManager(configuration.GetConnectionString("ConnectionString"));
        }

        public void CreateDB(string name)
        {
            sqlManager.CreateDateBase(name);
        }

        public void CreateTable(string dbName, string tableName)
        {
            tableName = tableName.Replace(" ", "_");
            string stringquery = $@"CREATE TABLE {tableName} ( 
                                    PersonID int,
                                    LastName varchar(255),
                                    FirstName varchar(255),
                                    Address varchar(255),
                                    City varchar(255) );";
            sqlManager.CreateTable(stringquery, dbName);
        }

        public IEnumerable<string> GetColumnNames(string dbName, string tableName)
        {
            return sqlManager.GetColumnNames(dbName, tableName);
        }

        public IEnumerable<IEnumerable<string>> ReadWholeTable(string dbName, string tableName)
        {
            return sqlManager.ReadWholeTable(dbName, tableName);
        }
        public IEnumerable<string> ReadAllTableFromDB(string dbName)
        {
            return sqlManager.ReadAllTable(dbName);
        }
        public IEnumerable<string> ReadAllDatebases()
        {
            return sqlManager.ReadAllDatebases();
        }
    }
}
