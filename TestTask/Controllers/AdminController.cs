using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Domain.DbEntities;
using TestTask.Domain.DbServices.CustomerService;

namespace TestTask.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICustomerService _customerService;
        public AdminController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        public IActionResult Index()
        {
            return View(_customerService.GetAllCustomers());
        }

        // Add

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var savingResult = _customerService.AddNewCustomer(customer);
                TempData["CustomerResult"] = savingResult ? "Customer was added successfully!" : "Error!";
                return RedirectToAction("Index", "Admin");
            }
            return View(customer);
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
                var savingResult = _customerService.AddNewCustomer(customer);
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

    }
}