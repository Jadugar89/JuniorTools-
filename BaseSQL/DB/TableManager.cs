using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BaseSQL.DB
{
    internal static class TableManager
    {

        public static IEnumerable<string> ReadExistingTableInDB(string connectionString,string DBName)
        {
            List <string> list = new List<string>();
            string queryString = $"Select Table_Name from {DBName}.INFORMATION_SCHEMA.TABLES;";
            var Result = new Query().CreateQuery(connectionString,queryString);
            foreach (var row in Result)
            {
                list.Add(row.First());
            }

           
            return list;
        }
        public static bool CreateTable(string ConString, string QueryString, string DBName)
        {
            Query query = new Query();
            query.CreateQuery(ConString, QueryString ,DBName);
            return true;
        }
        public static IEnumerable<IEnumerable<string>> ReadWholeTable(string ConString, string DBName, string TableName)
        {
            string queryString = $"Select * from {TableName};";
            return new Query().CreateQuery(ConString, queryString,DBName);
        }


    }
}
