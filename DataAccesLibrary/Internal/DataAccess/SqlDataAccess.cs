using Dapper;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Abstractions;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.Internal.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfigurationManager config;
       

        public SqlDataAccess(IConfigurationManager _configuration)
        {
            config = _configuration;
        }

        // get connection string by name from app.config 
        public string GetConnectionString(string name)
        {
            return config.ConnectionStrings[name].ConnectionString;
        }

        // get all data acoording to specific model from database and returns number of rows affected
        public IEnumerable<T> GetAllData<T>(string storedProcedure, string connectinStringName)
        {
            string connectionString = GetConnectionString(connectinStringName);
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                IEnumerable<T> rows = dbConnection.Query<T>(storedProcedure,
                commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }

        // get data from database by parameter
        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectinStringName)
        {
            string connectionString = GetConnectionString(connectinStringName);
            using (IDbConnection dbConnection= new SqlConnection(connectionString))
            {
                List<T> rows = dbConnection.Query<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }
        // insert data into database
        public int SaveData<T>(string storedProcedure, T parameters, string connectinStringName)
        {
            string connectionString = GetConnectionString(connectinStringName);
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                int rowsAffected = dbConnection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return rowsAffected;
            }
            
        }

        public void UpdateData<T>(string storedProcedure, T parameters, string connectinStringName)
        {
            string connectionString = GetConnectionString(connectinStringName);
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void GetId(string sql, string connectinStringName)
        {
            string connectionString = GetConnectionString(connectinStringName);
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
               dbConnection.Execute(sql);
            }
        }

        public void DeleteData<T>(string storedProcedure, T parameters, string connectinStringName)
        {
            string connectionString = GetConnectionString(connectinStringName);
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> LoadDataByParameterAsync<T>(string sql, string text, string connectinString)
        {
            string connectionString = GetConnectionString(connectinString);
            using (IDbConnection dbconnection = new SqlConnection(connectionString))
            {
                var result = await dbconnection.QueryFirstAsync<T>(sql, text);
                return result;
            }
        }

        public UserModel LoadUserByParameterAsync(string sql, string connectinString)
        {
            string connectionString = GetConnectionString(connectinString);
            using (IDbConnection dbconnection = new SqlConnection(connectionString))
            {
                var result = dbconnection.QueryFirst<UserModel>(sql);
                return result;
            }
        }

        public ServerResponseModel<T> ExecuteRawSql<T>(string sql, string connectinStringName)
        {
            ServerResponseModel<T> response = new ServerResponseModel<T>();
            string connectionString = GetConnectionString(connectinStringName);
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                try
                {
                    response.Count = dbConnection.Execute(sql);
                }

                catch (Exception ex)
                {
                    response.Error = ex.Message;
                }
            }
            return response;
        }

        public ServerResponseModel<T> QueryRawSql<T>(string sql, string connectinStringName)
        {
            ServerResponseModel<T> response = new ServerResponseModel<T>();
            string connectionString = GetConnectionString(connectinStringName);
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();
                try
                {
                    response.Data = dbConnection.Query<T>(sql);
                }

                catch (Exception ex)
                {
                    response.Error = ex.Message;
                }
            }
            return response;
        }

       

    }
}
