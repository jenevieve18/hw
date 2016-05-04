using System;
using System.Collections.Generic;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing.Core.Services
{
	public class InvoiceService
	{
		SqlInvoiceRepository ir = new SqlInvoiceRepository();
		SqlCompanyRepository cr = new SqlCompanyRepository();
		SqlCustomerRepository ur = new SqlCustomerRepository();
		
		public InvoiceService(SqlCompanyRepository cr, SqlInvoiceRepository ir, SqlCustomerRepository ur)
		{
			this.cr = cr;
			this.ir = ir;
			this.ur = ur;
		}
		
		public Company ReadCompany(int companyId)
		{
			return cr.Read(companyId);
		}
		
		public void InvoiceExported(int invoiceId)
		{
			ir.Exported(invoiceId);
		}
		
		public void UpdateInvoice(Invoice invoice, int invoiceId)
		{
			ir.Update(invoice, invoiceId);
		}
		
		public IList<CustomerTimebook> FindOpenCustomerTimebooks(int customerId)
		{
			return ur.FindOpenTimebooks(customerId);
		}
		
		public Invoice ReadInvoice(int invoiceId)
		{
//			var invoice = ir.Read2(invoiceId);
//			if (invoice != null) {
//				invoice.CustomerContact = ur.ReadContact(invoice.CustomerContact.Id);
//				invoice.Customer.Contacts = ur.FindContacts(invoice.Customer.Id);
//				invoice.Timebooks = ir.FindTimebooks(invoiceId);
//			}
//			return invoice;
			return ReadInvoice(invoiceId, 0, "", "");
		}
		
		public Invoice ReadInvoice(int invoiceId, int companyId, string directory, string defaultInvoiceLogo)
		{
			var invoice = ir.Read2(invoiceId);
			if (invoice != null) {
				if (companyId > 0) {
					var company = cr.Read(companyId);
					invoice.Company = company;
					if (company.HasInvoiceLogo) {
						company.InvoiceLogo = string.Format("{0}/{1}", directory, company.InvoiceLogo);
					}
				}
				invoice.CustomerContact = ur.ReadContact(invoice.CustomerContact.Id);
				invoice.Customer.Contacts = ur.FindContacts(invoice.Customer.Id);
				invoice.Timebooks = ir.FindTimebooks(invoiceId);
			}
			return invoice;
		}
	}
}
