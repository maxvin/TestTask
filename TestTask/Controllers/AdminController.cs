using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Domain.DbEntities;
using TestTask.Domain.DbServices.CustomerService;
using TestTask.Domain.DbEntities.AccountEntities;
using TestTask.Domain.DbServices.DepartmentService;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTask.Domain.DbServices.UserService;

namespace TestTask.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IDepartmentService _departmentService;
        private readonly IUserDbService _userDbService;


        public AdminController(ICustomerService customerService,
            IDepartmentService departmentService,
            IUserDbService userDbService)
        {
            _customerService = customerService;
            _departmentService = departmentService;
            _userDbService = userDbService;
        }


        public IActionResult Index()
        {
            return View(_customerService.GetAllCustomers());
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var savingResult = _customerService.AddNewCustomer(customer);
                TempData["CustomerResult"] = savingResult ? "Customer was added successfully!" : "Error!";
                return RedirectToAction("Index", "Admin");
            }
            return View(customer);
        }

    }
}