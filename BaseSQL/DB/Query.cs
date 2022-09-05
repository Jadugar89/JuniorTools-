using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;

namespace BaseSQL.DB
{
    public class Query
    {

        
        public IEnumerable<IEnumerable<string>> CreateQuery(string ConString,string queryString)
        {
            return CreateQuery(ConString,queryString, String.Empty);
        }

        public IEnumerable<IEnumerable<string>> CreateQuery(string ConString,string queryString,string DBName)
        {
            var connectionString = ConString;
            List<IEnumerable<string>> Result = new List<IEnumerable<string>>();
            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);


                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();               
                    if (DBName != null && DBName!=String.Empty)
                    {
                        connection.ChangeDatabase(DBName);
                    }
                    

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var tempList = new List<string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if(reader[i].ToString() !=null)
                            {
                                tempList.Add(reader[i].ToString());
                            }
                            
                        }
                        Result.Add(tempList);
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    
                }
                Console.ReadLine();
            }
            return Result;
        }

        public bool CreateTable(string connectionString,string queryString,string DBName)
        {

            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);


                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    connection.ChangeDatabase(DBName);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}",
                            reader[0], reader[1], reader[2]);
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
                Console.ReadLine();
            }
            return true;
        }
        

    }
}
