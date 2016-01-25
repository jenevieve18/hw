using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
	public partial class InvoiceSend : System.Web.UI.Page
	{
		SqlInvoiceRepository invoiceRepository = new SqlInvoiceRepository();
		SqlCompanyRepository companyRepository = new SqlCompanyRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

			int invoiceId = ConvertHelper.ToInt32(Request.QueryString["Id"]);
			var invoice = invoiceRepository.Read(invoiceId);
            HtmlHelper.RedirectIf(invoice == null, "invoices.aspx");
			
			int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
			var company = companyRepository.Read(companyId);
			
			invoice.Company = company;
			if (company.HasInvoiceLogo) {
				company.InvoiceLogo = string.Format(Server.MapPath("~/uploads/{0}"), company.InvoiceLogo);
			} else {
				company.InvoiceLogo = Server.MapPath("~/img/ihg.png");
			}
			
			try {
				string file = string.Format(Server.MapPath("~/uploads/{0} {1} {2}.pdf"), invoice.Id, invoice.Customer.Name, DateTime.Now.ToString("MMM yyyy"));

				using (FileStream fStream = new FileStream(file, FileMode.Create, FileAccess.Write)) {
					var exporter = InvoiceExporterFactory.GetExporter2(company.InvoiceExporter);
					var exported = exporter.Export(invoice);
					exported.WriteTo(fStream);
				}
				
				MailHelper.SendMail(
                    company.InvoiceEmail,
                    invoice.Customer.InvoiceEmail,
					company.InvoiceEmailSubject,
					company.InvoiceEmailText,
					file
                );

                Session["Message"] = "Invoice sent!";
			} catch (Exception ex) {
                Session["Message"] = ex.Message;
			}
            Response.Redirect("invoices.aspx");
		}
	}
}