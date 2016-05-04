using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Services;

namespace HW.Invoicing
{
	public partial class InvoiceExport : System.Web.UI.Page
	{
		InvoiceService service = new InvoiceService(new SqlCompanyRepository(), new SqlInvoiceRepository(), new SqlCustomerRepository());

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

			int invoiceId = ConvertHelper.ToInt32(Request.QueryString["Id"]);

			service.InvoiceExported(invoiceId);

			int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
			
			var invoice = service.ReadInvoice(invoiceId, companyId, Server.MapPath("~/uploads"), Server.MapPath("~/img/ihg.png"));

			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;

//			string file = string.Format("{0} {1} {2} {3}", invoice.Number, invoice.Customer != null ? invoice.Customer.Name : "", invoice.Customer != null && invoice.Customer.ContactPerson != null ? invoice.Customer.ContactPerson.Name : "", DateTime.Now.ToString("MMM yyyy"));
			string file = string.Format("{0} {1} {2} {3}", invoice.Number, invoice.Customer != null ? invoice.Customer.Name : "", invoice.Customer != null && invoice.CustomerContact != null ? invoice.CustomerContact.Name : "", DateTime.Now.ToString("MMM yyyy"));
			Response.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}.pdf\";", file));

//			var exporter = InvoiceExporterFactory.GetExporter(invoice.Customer.Company.InvoiceExporter);
			var exporter = InvoiceExporterFactory.GetExporter(invoice.Company.InvoiceExporter);

			var exported = exporter.Export(invoice);
			exported.WriteTo(Response.OutputStream);

			Response.Flush();
			Response.Close();
			Response.End();
		}
	}
}