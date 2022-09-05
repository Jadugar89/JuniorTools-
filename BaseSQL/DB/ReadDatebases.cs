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
    internal static class  ReadDatebases
    {

        public static IEnumerable<string> ReadExistingDatebase(string connectionString)
        {
            List <string> list = new List<string>();
            string queryString = "Select * from sysdatabases;";
            var Result = new Query().CreateQuery(connectionString,queryString);
            
            foreach (var d in Result)
            {
                var tempArray= d.ToArray();
                list.Add(tempArray[0]);
            }
            return list;
        }

    }
}
