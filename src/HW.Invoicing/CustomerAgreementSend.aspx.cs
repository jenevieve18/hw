﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;
using System.IO;
using HW.Invoicing.Core.Helpers;

namespace HW.Invoicing
{
	public partial class CustomerAgreementSend : System.Web.UI.Page
	{
		SqlCustomerRepository customerRepository = new SqlCustomerRepository();
		SqlCompanyRepository companyRepository = new SqlCompanyRepository();

		protected void Page_Load(object sender, EventArgs e)
		{
			int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
			int companyId = ConvertHelper.ToInt32(Request.QueryString["CompanyId"]);
			int customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);

			var agreement = customerRepository.ReadAgreement(id);
			var company = companyRepository.Read(companyId);
			if (agreement != null) {
				var body = company.AgreementSignedEmailText;
				try {
					var exporter = new HCGEAgreementExporter();
					string file = string.Format(Server.MapPath("~/uploads/{0} {1} {2}.pdf"), agreement.Id, agreement.Customer.Name, DateTime.Now.ToString("MMM yyyy"));
					string templateFileName = company.HasAgreementTemplate ? string.Format(Server.MapPath("~/uploads/{0}"), company.AgreementTemplate) : Server.MapPath(@"HCG Avtalsmall Latest.pdf");

					using (FileStream f = new FileStream(file, FileMode.Create, FileAccess.Write)) {
						MemoryStream s = exporter.Export(agreement, templateFileName, "calibri.ttf");
						s.WriteTo(f);
					}
					
					MailHelper.SendMail(
						"info@danhasson.se",
						agreement.Email,
						company.AgreementSignedEmailSubject,
						body,
						file
					);

					Session["Message"] = "<div class='alert alert-success'>Customer agreement sent to customer.</div>";
				} catch (Exception ex) {
					Session["Message"] = string.Format("<div class='alert alert-danger'>{0}</div>", ex.Message);
				}
				Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=agreements", customerId));
			}
		}
	}
}