using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sql;
using System.Windows;



namespace BaseSQL.DB
{
    public class BaseBuilder
    {

        public bool CreateBase(string connectionString,string Name)
        {
            string queryString = $"CREATE DATABASE {Name}; ";
           

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
