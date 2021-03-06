﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Helpers;

namespace HW.Invoicing
{
	public partial class CompanyEdit : System.Web.UI.Page
	{
		SqlCompanyRepository r = new SqlCompanyRepository();
		int id;
		protected Company company;
		protected string message;

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

			id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
			company = r.Read(id);
			if (!IsPostBack) {
				dropDownListInvoiceExporter.Items.Clear();
				int i = 0;
				foreach (var x in InvoiceExporterFactory.GetExporters()) {
//					dropDownListInvoiceExporter.Items.Add(new ListItem(x.Name, (i++).ToString()));
					dropDownListInvoiceExporter.Items.Add(new ListItem(x.Name, x.Id.ToString()));
				}

				if (company != null) {
					textBoxName.Text = company.Name;
					textBoxAddress.Text = company.Address;
					textBoxPhone.Text = company.Phone;
					textBoxEmail.Text = company.Email;
					textBoxWebsite.Text = company.Website;
					textBoxBankAccountNumber.Text = company.BankAccountNumber;
					textBoxTIN.Text = company.TIN;
					textBoxFinancialMonthStart.Text = company.FinancialMonthStart == null ? "" : company.FinancialMonthStart.Value.ToString("yyyy-MM-dd");
					textBoxFinancialMonthEnd.Text = company.FinancialMonthEnd == null ? "" : company.FinancialMonthEnd.Value.ToString("yyyy-MM-dd");
					checkBoxHasSubscriber.Checked = company.HasSubscriber;

					textBoxTerms.Text = company.Terms;

					textBoxAgreementPrefix.Text = company.AgreementPrefix;

					textBoxOrganizationNumber.Text = company.OrganizationNumber;

					textBoxAgreementEmailText.Text = company.AgreementEmailText;
					textBoxAgreementEmailSubject.Text = company.AgreementEmailSubject;

					textBoxAgreementSignedEmailText.Text = company.AgreementSignedEmailText;
					textBoxAgreementSignedEmailSubject.Text = company.AgreementSignedEmailSubject;

					textBoxInvoicePrefix.Text = company.InvoicePrefix;
					textBoxInvoiceLogoPercentage.Text = company.InvoiceLogoPercentage.ToString();
					dropDownListInvoiceExporter.SelectedValue = company.InvoiceExporter.ToString();
					textBoxInvoiceEmail.Text = company.InvoiceEmail;
					textBoxInvoiceEmailSubject.Text = company.InvoiceEmailSubject;
					textBoxInvoiceEmailText.Text = company.InvoiceEmailText;
					
					textBoxSubscriptionText.Text = company.SubscriptionText;
				} else {
					Response.Redirect("companies.aspx");
				}
			}
		}

		//protected void buttonSaveTerms_Click(object sender, EventArgs e)
		//{
		//    r.SaveTerms(textBoxTerms.Text, id);
		//    Response.Redirect("companies.aspx");
		//}

		//protected void buttonSaveAgreementEmailText_Click(object sender, EventArgs e)
		//{
		//    var c = new Company {
		//        AgreementEmailSubject = textBoxAgreementEmailSubject.Text,
		//        AgreementEmailText = textBoxAgreementEmailText.Text,
		//        AgreementSignedEmailSubject = textBoxAgreementSignedEmailSubject.Text,
		//        AgreementSignedEmailText = textBoxAgreementSignedEmailText.Text
		//    };
		//    r.SaveAgreementEmail(c, id);
		//    Response.Redirect("companies.aspx");
		//}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			string signature;
			if (fileUploadSignature.HasFile) {
				signature = fileUploadSignature.FileName;
				fileUploadSignature.SaveAs(Server.MapPath("~/uploads/" + signature));
			} else {
				signature = company.Signature;
			}
			string invoiceLogo;
			if (fileUploadInvoiceLogo.HasFile) {
				invoiceLogo = fileUploadInvoiceLogo.FileName;
				fileUploadInvoiceLogo.SaveAs(Server.MapPath("~/uploads/" + invoiceLogo));
			} else {
				invoiceLogo = company.InvoiceLogo;
			}
			string invoiceTemplate = "";
			//if (fileUploadInvoiceTemplate.HasFile)
			//{
			//    invoiceTemplate = fileUploadInvoiceTemplate.FileName;
			//    fileUploadInvoiceTemplate.SaveAs(Server.MapPath("~/uploads/" + invoiceTemplate));
			//}
			//else
			//{
			//    invoiceTemplate = company.InvoiceTemplate;
			//}
			string agreementTemplate;
			if (fileUploadAgreementTemplate.HasFile) {
				agreementTemplate = fileUploadAgreementTemplate.FileName;
				fileUploadAgreementTemplate.SaveAs(Server.MapPath("~/uploads/" + agreementTemplate));
			} else {
				agreementTemplate = company.AgreementTemplate;
			}
			var c = new Company {
				Name = textBoxName.Text,
				Address = textBoxAddress.Text,
				Phone = textBoxPhone.Text,
				Email = textBoxEmail.Text,
				Website = textBoxWebsite.Text,
				BankAccountNumber = textBoxBankAccountNumber.Text,
				TIN = textBoxTIN.Text,
				FinancialMonthStart = ConvertHelper.ToDateTime(textBoxFinancialMonthStart.Text),
				FinancialMonthEnd = ConvertHelper.ToDateTime(textBoxFinancialMonthEnd.Text),
				HasSubscriber = checkBoxHasSubscriber.Checked,
				Signature = signature,
				AgreementPrefix = textBoxAgreementPrefix.Text,
				OrganizationNumber = textBoxOrganizationNumber.Text,
				AgreementTemplate = agreementTemplate,

				InvoicePrefix = textBoxInvoicePrefix.Text,
				InvoiceLogo = invoiceLogo,
				InvoiceLogoPercentage = ConvertHelper.ToDouble(textBoxInvoiceLogoPercentage.Text),
				InvoiceTemplate = invoiceTemplate,
				InvoiceExporter = ConvertHelper.ToInt32(dropDownListInvoiceExporter.SelectedValue),
				InvoiceEmail = textBoxInvoiceEmail.Text,
				InvoiceEmailSubject = textBoxInvoiceEmailSubject.Text,
				InvoiceEmailText = textBoxInvoiceEmailText.Text,

				Terms = textBoxTerms.Text,

				AgreementEmailSubject = textBoxAgreementEmailSubject.Text,
				AgreementEmailText = textBoxAgreementEmailText.Text,
				AgreementSignedEmailSubject = textBoxAgreementSignedEmailSubject.Text,
				AgreementSignedEmailText = textBoxAgreementSignedEmailText.Text,

                SubscriptionText = textBoxSubscriptionText.Text
			};
			c.Validate();
			if (c.HasErrors) {
				message = c.Errors.ToHtmlUl();
			} else {
				r.Update(c, id);
				
				if (id == ConvertHelper.ToInt32(Session["CompanyId"])) {
					Session["CompanyName"] = c.Name;
				}
				
				Response.Redirect("companies.aspx");
			}
		}
	}
}