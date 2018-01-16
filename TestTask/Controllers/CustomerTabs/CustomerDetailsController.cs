using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Domain.DbEntities;
using TestTask.Domain.DbServices.CustomerService;
using TestTask.WebUI.Components.ActionFilters;

namespace TestTask.WebUI.Controllers.CustomerTabs
{
    //[CustomerRedirectFilter]
    [Authorize(Roles = "Admin")]
    public class CustomerDetailsController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerDetailsController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Index(int customerId)
        {
            return View("~/Views/Admin/CustomerDetails/Index.cshtml", _customerService.GetCustomerById(customerId));
        }


        // Edit

        [HttpGet]
        public IActionResult EditCustomer(int customerId)
        {
            return View("~/Views/Admin/CustomerDetails/EditCustomer.cshtml", _customerService.GetCustomerById(customerId));
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
            return View("~/Views/Admin/CustomerDetails/EditCustomer.cshtml", customer);
        }


        [HttpGet]
        public IActionResult CreateCustomerContact(int customerId)
        {
            ViewBag.CustomerId = customerId;
            return View("~/Views/Admin/CustomerDetails/CreateCustomerContact.cshtml");
        }

        [HttpPost]
        public IActionResult CreateCustomerContact(Contact contact, int customerId)
        {
            HttpContext.Items["AllowCustomerRedirect"] = true;

            if (ModelState.IsValid)
            {
                var savingResult = _customerService.AddContact(customerId, contact);
                TempData["CustomerResult"] = savingResult ? "Contact was added successfully!" : "Error!";

                return RedirectToAction("Index", "CustomerDetails", new { customerId = customerId });
            }

            ViewBag.customerId = customerId;
            return View("~/Views/Admin/CustomerDetails/CreateCustomerContact.cshtml", contact);
        }

        // Remove

        public IActionResult RemoveCustomer(int customerId)
        {
            try
            {
                var deleteResult = _customerService.RemoveCustomerById(customerId);
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
