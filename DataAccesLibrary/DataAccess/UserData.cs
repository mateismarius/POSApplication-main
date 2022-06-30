using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.DataAccess
{
    public class UserData: IUserData
    {
        private readonly ISqlDataAccess sqlDataAccess;

        public UserData(ISqlDataAccess _sqlDataAccess)
        {
            sqlDataAccess = _sqlDataAccess;
        }
        public ServerResponseModel<UserModel> AddNewUser(string sql)
        {
            return sqlDataAccess.ExecuteRawSql<UserModel>(sql, "retailApp");
        }

        public ServerResponseModel<UserModel> GetUsersList()
        {
            string query = "SELECT * FROM [dbo].[User]";
            return sqlDataAccess.QueryRawSql<UserModel>(query, "retailApp");
        }

        public ServerResponseModel<UserModel> GetUserByName(string first, string last)
        {
            string query = $"SELECT * FROM [dbo].[User] WHERE FirstName='{first}' AND LastName='{last}'";
            return sqlDataAccess.QueryRawSql<UserModel>(query, "retailApp");
        }

        public ServerResponseModel<UserModel> LogInUser(string sql)
        {
            return sqlDataAccess.ExecuteRawSql<UserModel>(sql, "retailApp");
        }
    }
}
