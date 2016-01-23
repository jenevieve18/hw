using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
	public partial class CustomerAgreementDownload : System.Web.UI.Page
	{
		SqlCustomerRepository r = new SqlCustomerRepository();
		SqlCompanyRepository cr = new SqlCompanyRepository();

		protected void Page_Load(object sender, EventArgs e)
		{
			int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
			int companyId = ConvertHelper.ToInt32(Request.QueryString["CompanyId"]);
			int customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
			
			var company = cr.Read(companyId);
			
			var agreement = r.ReadAgreement(id);
//			if (agreement != null) {
//				string fileName = "test.pdf";
//				using (FileStream f = new FileStream(fileName, FileMode.Create, FileAccess.Write)) {
//					MemoryStream s = hcg.Export(a, @"HCG Avtal HCGE-PDF form example without comments.pdf", "calibri.ttf");
//					s.WriteTo(f);
//				}
//			}
			
			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;

			string file = string.Format("{0} {1} {2}", agreement.Id, agreement.Customer.Name, DateTime.Now.ToString("MMM yyyy"));
			Response.AddHeader("content-disposition", string.Format("attachment;filename=\"{0}.pdf\";", file));

//			string templateFileName = company.HasAgreementTemplate ? string.Format(Server.MapPath("~/uploads/{0}"), company.AgreementTemplate) : Server.MapPath(@"HCG Avtal HCGE-PDF form example without comments.pdf");
			string templateFileName = company.HasAgreementTemplate ? string.Format(Server.MapPath("~/uploads/{0}"), company.AgreementTemplate) : Server.MapPath(@"HCG Avtalsmall Latest.pdf");
			
			var exporter = new HCGEAgreementExporter();
			
			var exported = exporter.Export(agreement, templateFileName, Server.MapPath(@"calibri.ttf"));
			exported.WriteTo(Response.OutputStream);

			Response.Flush();
			Response.Close();
			Response.End();
		}
	}
}