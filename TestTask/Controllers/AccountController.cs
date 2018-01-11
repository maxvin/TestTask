using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TestTask.Domain.DbEntities;
using TestTask.Domain.DbEntities.AccountEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Domain;
using TestTask.Domain.DbServices.UserService;
using TestTask.Domain.DbServices.DepartmentService;

namespace TestTask.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserDbService _userDbService;

        private readonly IDepartmentService _departmentService;

        public AccountController(IUserDbService userDbService,
            IDepartmentService departmentService)
        {
            _userDbService = userDbService;
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.DepartmentsList = new SelectList(_departmentService.GetDepartments().ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerModel) {

            var departments = _departmentService.GetDepartments();

            if (ModelState.IsValid)
            {
                var registerDepartment = departments.First(e=>e.Id == registerModel.DepartmentId);
                User user = new User { UserName = registerModel.UserName,
                    Department = registerDepartment,
                    Email = registerModel.Email,
                    Mobile = registerModel.Mobile,
                    Name = registerModel.UserName
                };
                var result = await _userDbService.AddUser(user, registerModel.Password);

                if (result.Succeeded) return RedirectToAction("Site", "Index");
            }

            ViewBag.DepartmentsList = new SelectList(departments.ToList(), "Id", "Name"); ;
            return View(registerModel);

        }

    }
}
