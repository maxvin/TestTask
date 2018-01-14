using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Domain.DbServices.CustomerService;

namespace TestTask.WebUI.Components.ViewComponents
{
    public class CustomerContactComponent : ViewComponent
    {
        private readonly ICustomerService _customerService;

        public CustomerContactComponent(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IViewComponentResult Invoke(int customerId)
        {
            var contacts = _customerService.GetCustomerContactsById(customerId);
            ViewBag.customerId = customerId;
            return View(contacts);
        }
    }
}
