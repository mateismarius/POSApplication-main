using DataAccesLibrary.Internal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.Internal.Interfaces
{
    public interface IUserData
    {
        ServerResponseModel<UserModel> AddNewUser(string sql);
        ServerResponseModel<UserModel> GetUsersList();
        ServerResponseModel<UserModel> LogInUser(string sql);
        ServerResponseModel<UserModel> GetUserByName(string first, string last);
    }
}
