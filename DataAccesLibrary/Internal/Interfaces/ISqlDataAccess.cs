using DataAccesLibrary.Internal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccesLibrary.Internal.Interfaces
{
    public interface ISqlDataAccess
    {
        void DeleteData<T>(string storedProcedure, T parameters, string connectinStringName);
        IEnumerable<T> GetAllData<T>(string storedProcedure, string connectinStringName);
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectinStringName);
        Task<T> LoadDataByParameterAsync<T>(string sql, string text, string connectinString);
        UserModel LoadUserByParameterAsync(string sql, string connectinString);
        int SaveData<T>(string storedProcedure, T parameters, string connectinStringName);
        void UpdateData<T>(string storedProcedure, T parameters, string connectinStringName);
        ServerResponseModel<T> ExecuteRawSql<T>(string sql, string connectinStringName);
        ServerResponseModel<T> QueryRawSql<T>(string sql, string connectinStringName);
        void GetId(string sql, string connectinStringName);
    }
}
