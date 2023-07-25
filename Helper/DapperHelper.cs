using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NativeMySql.API.Helper
{
    public class DapperHelper
    {
        private string connectionString;

        public DapperHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Get List of items using stored procedure with dynamic model
        public List<T> GetAll<T>(string storedProcedureName)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<T>(storedProcedureName, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        // Get List of items using stored procedure with dynamic model
        public T GetById<T>(string storedProcedureName, string parameterName, int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add(parameterName, id);

                return connection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Insert or Update data using stored procedure
        public int InsertOrUpdate<T>(string storedProcedureName, T model, Action<DynamicParameters> parameterBuilder)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                var parameters = new DynamicParameters();
                parameterBuilder(parameters);

                return connection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // Delete data using id
        public int Delete(string storedProcedureName, string parameterName, int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add(parameterName, id);
                return connection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
