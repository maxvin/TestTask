using Microsoft.AspNetCore.Authorization;
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
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;

        private readonly ICustomerService _customerService;

        public DepartmentsController(IDepartmentService departmentService, ICustomerService customerService)
        {
            _departmentService = departmentService;
            _customerService  = customerService;

        }

        public IActionResult Index(int customerId)
        {
            ViewBag.CustomerId = customerId;
            return View("~/Views/Admin/Departments/Index.cshtml", _departmentService.GetCustomerDepartments(customerId));
        }

        [HttpGet]
        public IActionResult CreateDepartment(int customerId)
        {
            ViewBag.customerId = customerId;
            return View("~/Views/Admin/Departments/CreateDepartment.cshtml");
        }


        [HttpPost]
        public IActionResult CreateDepartment(Department department, int customerId)
        {
            if (ModelState.IsValid)
            {
                _customerService.AddCustomerDepartment(customerId, department);
                ViewBag.customerId = customerId;

                return RedirectToAction("index", "departments");
            }
            ViewBag.customerId = customerId;
            return View("~/Views/Admin/Departments/CreateDepartment.cshtml", department);
        }

    }
}
