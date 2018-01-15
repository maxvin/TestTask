using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTask.Domain.DbEntities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Domain.DbServices.UserService
{
    public class UserDbService : IUserDbService
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserDbService(ApplicationDbContext appDbContext,
            UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _appDbContext = appDbContext;
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
            return _appDbContext.Users.FirstOrDefault(e=>e.UserName == userName);
        }

        public List<User> GetUsersByCustomerId(int customerId)
        {
            return _appDbContext.Customers.Include(e => e.Users).First(e => e.Id == customerId).Users.ToList();
        }
    }
}
