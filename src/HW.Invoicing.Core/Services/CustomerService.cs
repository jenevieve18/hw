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
		SqlCustomerRepository cr;
		SqlItemRepository ir;

		public CustomerService(SqlCustomerRepository cr, SqlItemRepository ir)
		{
			this.cr = cr;
			this.ir = ir;
		}

		public IList<Customer> FindSubscribersByCompany(int companyId)
		{
			var customers = cr.FindSubscribersByCompany(companyId);
			AssignContacts(customers);
			return customers;
		}

		public IList<Customer> FindNonSubscribersByCompany(int companyId)
		{
			var customers = cr.FindNonSubscribersByCompany(companyId);
			AssignContacts(customers);
			return customers;
		}

		public IList<Customer> FindDeletedCustomersByCompany(int companyId)
		{
			var customers = cr.FindDeletedCustomersByCompany(companyId);
			AssignContacts(customers);
			return customers;
		}

		void AssignContacts(IList<Customer> customers)
		{
			foreach (var c in customers) {
//				c.ContactPerson = cr.ReadContact(c.ContactPerson.Id);
				c.Contacts = cr.FindContacts(c.Id);
			}
		}
		
		public void UpdateCustomerContact(CustomerContact c, int id)
		{
			cr.UpdateContact(c, id);
		}
		
		public void UpdateCustomerAgreement(CustomerAgreement a, int id)
		{
			cr.UpdateAgreement(a, id);
		}
		
		public void UpdateCustomer2(Customer c, int id)
		{
			cr.Update2(c, id);
		}
		
		public CustomerAgreement ReadCustomerAgreement(int customerAgreementId)
		{
			return cr.ReadAgreement(customerAgreementId);
		}
		
		public Customer ReadCustomer(int id)
		{
			var c = cr.Read2(id);
			if (c != null) {
				c.Contacts = cr.FindContacts(id);
				if (c.HasSubscription) {
					c.SubscriptionItem = ir.Read2(c.SubscriptionItem.Id);
				}
			}
			return c;
		}
		
		public void MakePrimary(CustomerContact contact, int customerId)
		{
			var contacts = cr.FindContacts(customerId);
			foreach (var c in contacts) {
				if (c.Id != contact.Id) {
					
				}
			}
		}
	}
}
