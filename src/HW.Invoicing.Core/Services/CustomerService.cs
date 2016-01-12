﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Services
{
    public class CustomerService
    {
        SqlCustomerRepository r;

        public CustomerService(SqlCustomerRepository r)
        {
            this.r = r;
        }

        public IList<Customer> FindSubscribersByCompany(int companyId)
        {
            var customers = r.FindSubscribersByCompany(companyId);
            AssignContacts(customers);
            return customers;
        }

        public IList<Customer> FindNonSubscribersByCompany(int companyId)
        {
            var customers = r.FindNonSubscribersByCompany(companyId);
            AssignContacts(customers);
            return customers;
        }

        public IList<Customer> FindDeletedCustomersByCompany(int companyId)
        {
            var customers = r.FindDeletedCustomersByCompany(companyId);
            AssignContacts(customers);
            return customers;
        }

        void AssignContacts(IList<Customer> customers)
        {
            foreach (var c in customers)
            {
                c.ContactPerson = r.ReadContact(c.ContactPerson.Id);
                c.Contacts = r.FindContacts(c.Id);
            }
        }
    }
}
