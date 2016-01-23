using System;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing.Core.Services
{
	public class InvoiceService
	{
		SqlInvoiceRepository ir = new SqlInvoiceRepository();
		SqlCompanyRepository cr = new SqlCompanyRepository();
		
		public InvoiceService(SqlCompanyRepository cr, SqlInvoiceRepository ir)
		{
			this.cr = cr;
			this.ir = ir;
		}
		
		public void InvoiceExported(int invoiceId)
		{
			ir.Exported(invoiceId);
		}
		
		public Invoice ReadInvoice(int invoiceId, int companyId, string directory, string defaultInvoiceLogo)
		{
			var invoice = ir.Read(invoiceId);
			var company = cr.Read(companyId);
			invoice.Company = company;
			invoice.Company = company;
			if (company.HasInvoiceLogo)
			{
				company.InvoiceLogo = string.Format("{0}/{1}", directory, company.InvoiceLogo);
			}
//			else
//			{
//				company.InvoiceLogo = defaultInvoiceLogo;
//			}
			return invoice;
		}
	}
}
