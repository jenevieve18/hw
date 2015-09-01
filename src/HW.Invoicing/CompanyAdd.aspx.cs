﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class CompanyAdd : System.Web.UI.Page
    {
        SqlCompanyRepository r = new SqlCompanyRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            if (!IsPostBack)
            {
                textBoxFinancialMonthStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
                textBoxFinancialMonthEnd.Text = DateTime.Now.AddYears(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            string signature = "";
            if (fileUploadSignature.HasFile)
            {
                signature = fileUploadSignature.FileName;
                fileUploadSignature.SaveAs(Server.MapPath("~/uploads/" + signature));
            }
            string logo = "";
            if (fileUploadInvoiceLogo.HasFile)
            {
                logo = fileUploadInvoiceLogo.FileName;
                fileUploadInvoiceLogo.SaveAs(Server.MapPath("~/uploads/" + logo));
            }
            string template = "";
            if (fileUploadInvoiceTemplate.HasFile)
            {
                template = fileUploadInvoiceTemplate.FileName;
                fileUploadInvoiceTemplate.SaveAs(Server.MapPath("~/uploads/" + template));
            }
            var c = new Company {
                Name = textBoxName.Text,
                Address = textBoxAddress.Text,
                Phone = textBoxPhone.Text,
                BankAccountNumber = textBoxBankAccountNumber.Text,
                TIN = textBoxTIN.Text,
                FinancialMonthStart = ConvertHelper.ToDateTime(textBoxFinancialMonthStart.Text),
                FinancialMonthEnd = ConvertHelper.ToDateTime(textBoxFinancialMonthEnd.Text),
                User = new User { Id = ConvertHelper.ToInt32(Session["UserId"]) },
                InvoicePrefix = textBoxInvoicePrefix.Text,
                HasSubscriber = checkBoxHasSubscriber.Checked,
                InvoiceLogo = logo,
                InvoiceTemplate = template,
                Signature = signature,
                AgreementPrefix = textBoxAgreementPrefix.Text,
                OrganizationNumber = textBoxOrganizationNumber.Text,

                AgreementEmailSubject = textBoxAgreementEmailSubject.Text,
                AgreementEmailText = textBoxAgreementEmailText.Text,
                AgreementSignedEmailSubject = textBoxAgreementSignedEmailSubject.Text,
                AgreementSignedEmailText = textBoxAgreementSignedEmailText.Text
            };
            r.Save(c);
            Response.Redirect("companies.aspx");
        }
    }
}