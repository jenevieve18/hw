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

        public IList<Customer> FindSubscribersByCompany(int companyId)
        {
            var customers = r.FindSubscribers(companyId);
            AssignContacts(customers);
            return customers;
        }

        public IList<Customer> FindNonSubscribersByCompany(int companyId)
        {
            var customers = r.FindNonSubscribers(companyId);
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
