using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTask.Domain.DbEntities;

namespace TestTask.Domain.DbServices.UserService
{
    public class UserDbService : IUserDbService
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly ApplicationIdentityContext _appIdentityDbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserDbService(ApplicationDbContext appDbContext,
            ApplicationIdentityContext appIdentityDbContext,
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _appDbContext = appDbContext;
            _appIdentityDbContext = appIdentityDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public User GetUserInfoById(int userId)
        {
            throw new NotImplementedException();
        }

        public User GetUserInfoByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
