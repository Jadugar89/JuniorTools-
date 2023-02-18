using System.Collections.Generic;

namespace BaseHelper.Services
{
    public interface IDataBaseService
    {
        void CreateDB(string name);
        void CreateTable(string dbName, string tableName);
        IEnumerable<string> GetColumnNames(string dbName, string tableName);
        IEnumerable<string> ReadAllDatebases();
        IEnumerable<string> ReadAllTableFromDB(string dbName);
        IEnumerable<IEnumerable<string>> ReadWholeTable(string dbName, string tableName);
    }
}