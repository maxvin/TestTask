using System;
using System.Collections.Generic;
using System.Text;
using TestTask.Domain.DbEntities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TestTask.Domain.DbServices.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _appDbContext;

        private readonly UserManager<User> _userManager;


        public CustomerService(ApplicationDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public bool AddContact(int customerId, Contact contact)
        {
            using (var appContect = _appDbContext)
            {
                try
                {
                    var customer = appContect.Customers.Include(e=>e.Contacts).First(e => e.Id == customerId);
                    customer.Contacts.Add(contact);
                    _appDbContext.Update(customer);
                    _appDbContext.SaveChanges();
                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }

            }
        }

        public void AddCustomerDepartment(int customerId, Department department)
        {
            var customer = _appDbContext.Customers.Include(e => e.Departments).First(e => e.Id == customerId);
            customer.Departments.Add(department);
            _appDbContext.Update(customer);
            _appDbContext.SaveChanges();
        }

        public void AddCustomerUser(int customerId, User user)
        {
            var customer = _appDbContext.Customers.Include(e => e.Users).First(e => e.Id == customerId);

            customer.Users.Add(user);
            _appDbContext.Update(customer);
            _appDbContext.SaveChanges();
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

        public ICollection<Contact> GetCustomerContactsById(int customerId)
        {
            return _appDbContext.Customers.Include(e => e.Contacts).First(e => e.Id == customerId).Contacts.ToList();
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

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                _appDbContext.Update(customer);
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
