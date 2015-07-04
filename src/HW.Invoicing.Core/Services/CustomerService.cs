using System;
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

        public IList<Customer> FindSubscribers()
        {
            var customers = r.FindSubscribers();
            AssignContacts(customers);
            return customers;
        }

        public IList<Customer> FindNonSubscribers()
        {
            var customers = r.FindNonSubscribers();
            AssignContacts(customers);
            return customers;
        }

        void AssignContacts(IList<Customer> customers)
        {
            foreach (var c in customers)
            {
                c.Contacts = r.FindContacts(c.Id);
            }
        }
    }
}
