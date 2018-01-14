using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Domain.DbEntities;
using TestTask.Domain.DbServices.DepartmentService;
using TestTask.Domain.DbServices.UserService;

namespace TestTask.WebUI.Controllers
{
    public class MocksController : Controller
    {
        private readonly IDepartmentService _departmentService;

        private readonly IUserDbService _userService;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public MocksController(IDepartmentService departmentService, 
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserDbService userService)
        {
            _departmentService = departmentService;
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
        }

        public ContentResult MockDepartments()
        {
            var results = new List<Boolean>();
            foreach(var ittr in Enumerable.Range(1, 15))
            {
                var EfSaveResult = _departmentService.AddDepartment(new Department
                {
                    Name = $"Mocked name {ittr}",
                    Address = $"Mocked address {ittr}"
                });
            }

            return Content($"Mocking is {results.Any(res => res != false)}");
        }


        public async Task<ContentResult> AddAdminRole()
        {
            var user = _userService.GetUserInfoByUserName("Admin");
            var result = await _roleManager.CreateAsync(new IdentityRole { Id = "Admin", Name = "Admin" });
            var roleResult = await _userManager.AddToRoleAsync(user, "Admin");

            return Content("Successfull ${result}");
        }
    }
}
