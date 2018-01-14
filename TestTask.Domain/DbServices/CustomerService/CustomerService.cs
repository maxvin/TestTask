using System;
using System.Collections.Generic;
using System.Text;
using TestTask.Domain.DbEntities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Domain.DbServices.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _appDbContext;

        public CustomerService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
