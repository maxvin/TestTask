using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTask.Domain.DbEntities;


namespace TestTask.Domain.DbServices.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _appDbContext;

        public DepartmentService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IList<Department> GetDepartments()
        {
            return _appDbContext.Departments.ToList();
        }

        public IList<Department> GetDepartments(int count)
        {
            throw new NotImplementedException();
        }

        public bool AddDepartment(Department department)
        {
            try
            {
                _appDbContext.Departments.Add(department);
                _appDbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public IList<Department> GetCustomerDepartments(int customerId)
        {
            return _appDbContext.Customers.Include(e => e.Departments).First(e => e.Id == customerId).Departments.ToList();
        }
    }
}
