using System;
using System.Collections.Generic;
using System.Text;
using TestTask.Domain.DbEntities;
using System.Linq;

namespace TestTask.Domain.DbServices.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _appDbContext;

        public CustomerService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool AddNewCustomer(Customer customer)
        {
            try
            {
                _appDbContext.Customers.Add(customer);
                _appDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IList<Customer> GetAllCustomers()
        {
            return _appDbContext.Customers.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _appDbContext.Customers.FirstOrDefault(e => e.Id == id);
        }

        public bool RemoveCustomerById(int id)
        {
            try
            {
                var customer = _appDbContext.Customers.FirstOrDefault(e => e.Id == id);
                _appDbContext.Customers.Remove(customer);
                _appDbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
