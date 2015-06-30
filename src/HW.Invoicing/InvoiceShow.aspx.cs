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
        SqlInvoiceRepository ir = new SqlInvoiceRepository();
        SqlCompanyRepository cr = new SqlCompanyRepository();
        int id;
        protected Invoice invoice;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            invoice = ir.Read(id);
            if (invoice != null)
            {
                labelInvoiceNumber.Text = invoice.Number;
                labelInvoiceCustomerNumber.Text = invoice.Customer.Number;
                //labelInvoiceCustomerAddress.Text = invoice.Customer.InvoiceAddress.Replace("\n", "<br>");
                labelInvoiceCustomerAddress.Text = invoice.Customer.ToString().Replace("\n", "<br>");
                labelInvoicePurchaseOrderNumber.Text = invoice.Customer.PurchaseOrderNumber;
                labelInvoiceYourReferencePerson.Text = invoice.Customer.YourReferencePerson;
                labelInvoiceOurReferencePerson.Text = invoice.Customer.OurReferencePerson;
                labelInvoiceComments.Text = invoice.Comments;

                var company = cr.Read(1);

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

        protected void buttonPrint_Click(object sender, EventArgs e)
        {
        }
    }
}