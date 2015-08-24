using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class CompanyEdit : System.Web.UI.Page
    {
        SqlCompanyRepository r = new SqlCompanyRepository();
        int id;
        protected Company company;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            company = r.Read(id);
            if (!IsPostBack)
            {
                if (company != null)
                {
                    textBoxName.Text = company.Name;
                    textBoxAddress.Text = company.Address;
                    textBoxPhone.Text = company.Phone;
                    textBoxEmail.Text = company.Email;
                    textBoxBankAccountNumber.Text = company.BankAccountNumber;
                    textBoxTIN.Text = company.TIN;
                    textBoxFinancialMonthStart.Text = company.FinancialMonthStart.Value.ToString("yyyy-MM-dd");
                    textBoxFinancialMonthEnd.Text = company.FinancialMonthEnd.Value.ToString("yyyy-MM-dd");
                    textBoxInvoicePrefix.Text = company.InvoicePrefix;
                    checkBoxHasSubscriber.Checked = company.HasSubscriber;

                    textBoxTerms.Text = company.Terms;

                    textBoxAgreementEmailText.Text = company.AgreementEmailText;
                    textBoxAgreementEmailSubject.Text = company.AgreementEmailSubject;
                }
                else
                {
                    Response.Redirect("companies.aspx");
                }
            }
        }

        protected void buttonSaveTerms_Click(object sender, EventArgs e)
        {
            r.SaveTerms(textBoxTerms.Text, id);
            Response.Redirect("companies.aspx");
        }

        protected void buttonSaveAgreementEmailText_Click(object sender, EventArgs e)
        {
            var c = new Company {
                AgreementEmailSubject = textBoxAgreementEmailSubject.Text,
                AgreementEmailText = textBoxAgreementEmailText.Text
            };
            r.SaveAgreementEmail(c, id);
            Response.Redirect("companies.aspx");
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            string signature;
            if (fileUploadSignature.HasFile)
            {
                signature = fileUploadSignature.FileName;
                fileUploadSignature.SaveAs(Server.MapPath("~/uploads/" + signature));
            }
            else
            {
                signature = company.Signature;
            }
            string logo;
            if (fileUploadInvoiceLogo.HasFile)
            {
                logo = fileUploadInvoiceLogo.FileName;
                fileUploadInvoiceLogo.SaveAs(Server.MapPath("~/uploads/" + logo));
            }
            else
            {
                logo = company.InvoiceLogo;
            }
            string template;
            if (fileUploadInvoiceTemplate.HasFile)
            {
                template = fileUploadInvoiceTemplate.FileName;
                fileUploadInvoiceTemplate.SaveAs(Server.MapPath("~/uploads/" + template));
            }
            else
            {
                template = company.InvoiceTemplate;
            }
            var c = new Company {
                Name = textBoxName.Text,
                Address = textBoxAddress.Text,
                Phone = textBoxPhone.Text,
                Email = textBoxEmail.Text,
                BankAccountNumber = textBoxBankAccountNumber.Text,
                TIN = textBoxTIN.Text,
                FinancialMonthStart = ConvertHelper.ToDateTime(textBoxFinancialMonthStart.Text),
                FinancialMonthEnd = ConvertHelper.ToDateTime(textBoxFinancialMonthEnd.Text),
                InvoicePrefix = textBoxInvoicePrefix.Text,
                HasSubscriber = checkBoxHasSubscriber.Checked,
                InvoiceLogo = logo,
                InvoiceTemplate = template,
                Signature = signature
            };
            r.Update(c, id);
            Response.Redirect("companies.aspx");
        }
    }
}