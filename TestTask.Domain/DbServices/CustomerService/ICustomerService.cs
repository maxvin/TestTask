using System;
using System.Collections.Generic;
using System.Text;
using TestTask.Domain.DbEntities;

namespace TestTask.Domain.DbServices.CustomerService
{
    public interface ICustomerService
    {
        IList<Customer> GetAllCustomers();

        bool AddNewCustomer(Customer customer);

        bool UpdateCustomer(Customer customer);

        bool RemoveCustomerById(int id);

        Customer GetCustomerById(int id);

        bool AddContact(int customerId, Contact contact);

        ICollection<Contact> GetCustomerContactsById(int customerId);

        void AddCustomerUser(int customerId, User user);

        void AddCustomerDepartment(int customerId, Department department);
    }
}
