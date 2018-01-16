using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Domain.DbEntities;
using TestTask.Domain.DbEntities.AccountEntities;
using TestTask.Domain.DbServices.CustomerService;
using TestTask.Domain.DbServices.DepartmentService;
using TestTask.Domain.DbServices.UserService;
using TestTask.WebUI.Components.ActionFilters;

namespace TestTask.WebUI.Controllers.CustomerTabs
{
    //[CustomerRedirectFilter]
    [Authorize(Roles = "Admin")]
    public class UsersInfoController : Controller
    {
        private readonly IUserDbService _userDbService;

        private readonly IDepartmentService _departmentService;

        private readonly ICustomerService _customerService;

        private readonly UserManager<User> _userManager;

        public UsersInfoController(IUserDbService userDbService, 
            IDepartmentService departmentService,
            ICustomerService customerService,
            UserManager<User> userManager
            )
        {
            _userDbService = userDbService;
            _departmentService = departmentService;
            _customerService = customerService;
            _userManager = userManager;
        }

        public IActionResult Index(int customerId)
        {
            ViewBag.CustomerId = customerId;
            return View("~/Views/Admin/UserInfoPage/Index.cshtml", _userDbService.GetUsersByCustomerId(customerId));
        }

        [HttpGet]
        public IActionResult CreateUser(int customerId)
        {
            ViewBag.customerId = customerId;
            ViewBag.DepartmentsList = new SelectList(_departmentService.GetCustomerDepartments(customerId), "Id", "Name");
            return View("~/Views/Admin/UserInfoPage/CreateUser.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterUserViewModel registerModel, int customerId)
        {
            var departments = _departmentService.GetDepartments();

            if (ModelState.IsValid)
            {
                var registerDepartment = departments.First(e => e.Id == registerModel.DepartmentId);
                User user = new User
                {
                    UserName = registerModel.UserName,
                    Department = registerDepartment,
                    Email = registerModel.Email,
                    Mobile = registerModel.Mobile,
                    Name = registerModel.UserName
                };
                var result = await _userDbService.AddUser(user, registerModel.Password);

                var updatedUserInfo = await _userManager.FindByNameAsync(registerModel.UserName);         
                _customerService.AddCustomerUser(customerId, updatedUserInfo);


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                if (result.Succeeded) return RedirectToAction("index", "userinfo", new { customerId = customerId });
            }

            ViewBag.DepartmentsList = new SelectList(departments.ToList(), "Id", "Name");
            return View("~/Views/Admin/UserInfoPage/CreateUser.cshtml", registerModel);
        }
    }
}
