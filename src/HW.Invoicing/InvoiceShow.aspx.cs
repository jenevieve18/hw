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
    public partial class InvoiceShow : System.Web.UI.Page
    {
        SqlInvoiceRepository invoiceRepository = new SqlInvoiceRepository();
        SqlCompanyRepository companyRepository = new SqlCompanyRepository();
        int id;
        protected Invoice invoice;
        protected Company company;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            
            invoice = invoiceRepository.Read(id);
            if (invoice != null)
            {
                labelInvoiceNumber.Text = invoice.Number;
                labelInvoiceDate.Text = invoice.Date.Value.ToString("yyyy-MM-dd");
                labelMaturityDate.Text = invoice.MaturityDate.Value.ToString("yyyy-MM-dd");
                labelInvoiceCustomerAddress.Text = invoice.Customer.ToString().Replace("\n", "<br>");
                labelInvoicePurchaseOrderNumber.Text = invoice.Customer.PurchaseOrderNumber;
                labelInvoiceYourReferencePerson.Text = invoice.Customer != null && invoice.Customer.ContactPerson != null ? invoice.Customer.ContactPerson.Name : "";
                labelInvoiceOurReferencePerson.Text = invoice.Customer.OurReferencePerson;
                panelPurchaseOrder.Visible = invoice.Customer.PurchaseOrderNumber != "";
            }
            else
            {
                Response.Redirect("invoices.aspx");
            }

            int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
            company = companyRepository.Read(companyId);
            if (company != null)
            {
                labelCompanyName.Text = company.Name;
                labelCompanyAddress.Text = company.Address;
                labelCompanyBankAccountNumber.Text = company.BankAccountNumber;
                labelCompanyPhone.Text = company.Phone;
                labelCompanyTIN.Text = company.TIN;
            }
        }

        protected void labelInvoiceCustomerNumber_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("customershow.aspx?Id={}&SelectedTab=customer-info", invoice.Customer.Id));
        }
    }
}