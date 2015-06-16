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

        public IList<Customer> FindAll()
        {
            var customers = r.FindAll();
            foreach (var c in customers)
            {
                c.Contacts = r.FindContacts(c.Id);
            }
            return customers;
        }
    }
}
