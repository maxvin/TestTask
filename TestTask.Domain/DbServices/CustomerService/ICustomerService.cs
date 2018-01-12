﻿using System;
using System.Collections.Generic;
using System.Text;
using TestTask.Domain.DbEntities;

namespace TestTask.Domain.DbServices.CustomerService
{
    public interface ICustomerService
    {
        IList<Customer> GetAllCustomers();

        bool AddNewCustomer(Customer customer);

        bool RemoveCustomerById(int id);

        Customer GetInfoById(int id);
    }
}
