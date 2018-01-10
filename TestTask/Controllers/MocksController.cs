using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Domain.DbEntities;
using TestTask.Domain.DbServices.DepartmentService;

namespace TestTask.WebUI.Controllers
{
    public class MocksController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public MocksController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
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
    }
}
