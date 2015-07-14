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

        protected void Page_Load(object sender, EventArgs e)
        {
            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            var c = r.Read(id);
            if (!IsPostBack)
            {
                if (c != null)
                {
                    textBoxName.Text = c.Name;
                    textBoxAddress.Text = c.Address;
                    textBoxPhone.Text = c.Phone;
                    textBoxBankAccountNumber.Text = c.BankAccountNumber;
                    textBoxTIN.Text = c.TIN;
                    textBoxFinancialMonthStart.Text = c.FinancialMonthStart.Value.ToString("yyyy-MM-dd");
                    textBoxFinancialMonthEnd.Text = c.FinancialMonthEnd.Value.ToString("yyyy-MM-dd");
                    textBoxInvoicePrefix.Text = c.InvoicePrefix;
                    checkBoxHasSubscriber.Checked = c.HasSubscriber;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var c = new Company {
                Name = textBoxName.Text,
                Address = textBoxAddress.Text,
                Phone = textBoxPhone.Text,
                BankAccountNumber = textBoxBankAccountNumber.Text,
                TIN = textBoxTIN.Text,
                FinancialMonthStart = ConvertHelper.ToDateTime(textBoxFinancialMonthStart.Text),
                FinancialMonthEnd = ConvertHelper.ToDateTime(textBoxFinancialMonthEnd.Text),
                InvoicePrefix = textBoxInvoicePrefix.Text,
                HasSubscriber = checkBoxHasSubscriber.Checked
            };
            r.Update(c, id);
            Response.Redirect("companies.aspx");
        }
    }
}