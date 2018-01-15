using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTask.Domain.DbEntities;

namespace TestTask.Domain.DbServices.UserService
{
    public interface IUserDbService
    {
        Task<IdentityResult> AddUser(User user, string password);

        User GetUserInfoByUserName(string userName);

        User GetUserInfoById(int userId);

        List<User> GetUsersByCustomerId(int customerId);
    }
}
