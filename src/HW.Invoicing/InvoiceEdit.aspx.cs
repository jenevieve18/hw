using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class InvoiceEdit : System.Web.UI.Page
    {
        protected Invoice invoice;
        SqlInvoiceRepository r = new SqlInvoiceRepository();
        SqlCompanyRepository cr = new SqlCompanyRepository();
        protected int id;

        protected void Page_Load(object sender, EventArgs e)
        {
			HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            if (!IsPostBack)
            {
                invoice = r.Read(id);
                if (invoice != null)
                {
                    labelInvoiceNumber.Text = invoice.Number;
                    labelInvoiceCustomerNumber.Text = invoice.Customer.Number;
                    textBoxInvoiceDate.Text = invoice.Date.Value.ToString("yyyy-MM-dd");
                    labelMaturityDate.Text = invoice.MaturityDate.Value.ToString("yyyy-MM-dd");
                    labelInvoiceCustomerAddress.Text = invoice.Customer.ToString().Replace("\n", "<br>");
                    labelInvoicePurchaseOrderNumber.Text = invoice.Customer.PurchaseOrderNumber;
                    labelInvoiceYourReferencePerson.Text = invoice.Customer.YourReferencePerson;
                    labelInvoiceOurReferencePerson.Text = invoice.Customer.OurReferencePerson;
                    textBoxInvoiceComments.Text = invoice.Comments;

                    var company = cr.Read(1);

                    labelCompanyName.Text = company.Name;
                    labelCompanyAddress.Text = company.Address;
                    labelCompanyBankAccountNumber.Text = company.BankAccountNumber;
                    labelCompanyPhone.Text = company.Phone;
                    labelCompanyTIN.Text = company.TIN;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var i = new Invoice
            {
                Date = ConvertHelper.ToDateTime(textBoxInvoiceDate.Text),
                MaturityDate = ConvertHelper.ToDateTime(labelMaturityDate.Text),
                Comments = textBoxInvoiceComments.Text
            };
            r.Update(i, id);
            Response.Redirect("invoices.aspx");
        }
    }
}