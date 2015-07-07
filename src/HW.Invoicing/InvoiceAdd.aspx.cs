using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class InvoiceAdd : System.Web.UI.Page
    {
        SqlCustomerRepository cr = new SqlCustomerRepository();
        SqlCompanyRepository or = new SqlCompanyRepository();
        SqlInvoiceRepository ir = new SqlInvoiceRepository();
        SqlItemRepository tr = new SqlItemRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            if (!IsPostBack)
            {
                dropDownListCustomer.Items.Clear();
                foreach (var c in cr.FindAll())
                {
                    dropDownListCustomer.Items.Add(new ListItem(c.Name, c.Id.ToString()));
                }
                dropDownListItem.Items.Clear();
                foreach (var i in tr.FindAll())
                {
                    dropDownListItem.Items.Add(new ListItem(i.Name, i.Id.ToString()));
                }

                labelInvoiceNumber.Text = string.Format("IHGF-{0}", ir.GetLatestInvoiceNumber().ToString("000"));

                var company = or.Read(1);
                if (company != null)
                {
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
            var i = new Invoice {
                Number = labelInvoiceNumber.Text,
                Comments = textBoxComments.Text
            };
            ir.Save(i);
        }
    }
}