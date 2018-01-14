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

        [HttpGet]
        public IActionResult CreateCustomerContact(int customerId)
        {
            ViewBag.CustomerId = customerId;
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomerContact(Contact contact, int customerId)
        {
            if (ModelState.IsValid)
            {
                var savingResult = _customerService.AddContact(customerId, contact);
                TempData["CustomerResult"] = savingResult ? "Contact was added successfully!" : "Error!";
                return RedirectToAction("GetCustomerInformation", "Admin", new { id = customerId });
            }

            ViewBag.CustomerId = customerId;
            return View(contact);
        }


        [HttpGet]
        public IActionResult CreateUser()
        {
            ViewBag.DepartmentsList = new SelectList(_departmentService.GetDepartments().ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterUserViewModel registerModel)
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

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                if (result.Succeeded) return RedirectToAction("Site", "Index");
            }

            ViewBag.DepartmentsList = new SelectList(departments.ToList(), "Id", "Name"); ;
            return View(registerModel);

        }



        // Edit

        [HttpGet]
        public IActionResult EditCustomer(int id)
        {
            return View(_customerService.GetCustomerById(id));
        }

        [HttpPost]
        public IActionResult EditCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var savingResult = _customerService.UpdateCustomer(customer);
                TempData["CustomerResult"] = savingResult ? "Customer was edited successfully!" : "Error!";
                return RedirectToAction("Index", "Admin");
            }
            return View(customer);
        }

        // Remove

        public IActionResult RemoveCustomer(int id)
        {
            try
            {
                var deleteResult = _customerService.RemoveCustomerById(id);
                TempData["CustomerResult"] = deleteResult ? "Customer was removed successfully!" : "Error!";
                return RedirectToAction("Index", "Admin");
            }
            catch
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        // Info
        [HttpGet]
        public IActionResult GetCustomerInformation(int id)
        {
            return View(_customerService.GetCustomerById(id));
        }

    }
}