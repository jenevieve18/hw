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
		SqlInvoiceRepository ir = new SqlInvoiceRepository();
		SqlCompanyRepository cr = new SqlCompanyRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

			int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
			var invoice = ir.Read(id);
            HtmlHelper.RedirectIf(invoice == null, "invoices.aspx");
			
			int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
			var company = cr.Read(companyId);
			
			invoice.Company = company;
			if (company.HasInvoiceLogo) {
				company.InvoiceLogo = string.Format(Server.MapPath("~/uploads/{0}"), company.InvoiceLogo);
			} else {
				company.InvoiceLogo = Server.MapPath("~/img/ihg.png");
			}
			
			try {
				string file = string.Format(Server.MapPath("~/uploads/{0} {1} {2}.pdf"), invoice.Id, invoice.Customer.Name, DateTime.Now.ToString("MMM yyyy"));
				string templateFileName = company.HasAgreementTemplate ? string.Format(Server.MapPath("~/uploads/{0}"), company.AgreementTemplate) : Server.MapPath(@"HCG Avtalsmall Latest.pdf");

				using (FileStream fStream = new FileStream(file, FileMode.Create, FileAccess.Write)) {
					var exporter = InvoiceExporterFactory.GetExporter2(company.InvoiceExporter);
					var exported = exporter.Export(invoice, templateFileName, Server.MapPath(@"arial.ttf"), false);
					exported.WriteTo(fStream);
				}
				
				MailHelper.SendMail(
                    company.InvoiceEmail,
                    invoice.Customer.InvoiceEmail,
					company.InvoiceEmailSubject,
					company.InvoiceEmailText,
					file
				);
			} catch (Exception ex) {
			}
		}
	}
}