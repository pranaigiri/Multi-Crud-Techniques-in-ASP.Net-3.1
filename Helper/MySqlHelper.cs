using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using NativeMySql.API.Models;
using System.Data;

namespace NativeMySql.API.Helper
{
    public class MySqlHelper
    {


        private string connectionString;

        public MySqlHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Get List of items using stored procedure with dynamic model
        public List<T> GetAll<T>(string storedProcedureName, Func<MySqlDataReader, T> mapFunction)
        {
            List<T> items = new List<T>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T item = mapFunction(reader);
                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }

        // Get List of items using stored procedure with dynamic model
        public T GetById<T>(string storedProcedureName, string parameterName, int id, Func<MySqlDataReader, T> mapFunction)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(parameterName, id); // Assuming your stored procedure expects a parameter for the user ID.

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return mapFunction(reader);
                        }
                    }
                }
            }

            // Return null if user not found or any other appropriate action.
            return default(T);
        }



        // Insert data using stored procedure
        public int InsertOrUpdate<T>(string storedProcedureName, T model, Action<MySqlCommand> parameterBuilder)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Call the parameterBuilder action to set any additional parameters needed for the stored procedure.
                    parameterBuilder(command);

                    return command.ExecuteNonQuery();
                }
            }
        }

        //Delete data using id
        public int Delete(string storedProcedureName, string parameterName, int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(parameterName, id);

                    return command.ExecuteNonQuery();
                }
            }
        }





    }

}
