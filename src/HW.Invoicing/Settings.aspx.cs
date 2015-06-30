using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class Settings : System.Web.UI.Page
    {
        SqlCompanyRepository r = new SqlCompanyRepository();
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

            id = ConvertHelper.ToInt32(Session["CompanyId"], 1);
            if (!IsPostBack)
            {
                var c = r.Read(id);
                if (c != null)
                {
                    textBoxName.Text = c.Name;
                    textBoxAddress.Text = c.Address;
                    textBoxPhone.Text = c.Phone;
                    textBoxBankAccountNumber.Text = c.BankAccountNumber;
                    textBoxTIN.Text = c.TIN;
                    if (c.FinancialMonthStart != null)
                    {
                        textBoxFinancialMonthStart.Text = c.FinancialMonthStart.Value.ToString("MMMM dd");
                    }
                    if (c.FinancialMonthEnd != null)
                    {
                        textBoxFinancialMonthEnd.Text = c.FinancialMonthEnd.Value.ToString("MMMM dd");
                    }
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
                FinancialMonthStart = ConvertHelper.ToDateTime(textBoxFinancialMonthStart.Text, DateTime.Now, "MMMM dd"),
                FinancialMonthEnd = ConvertHelper.ToDateTime(textBoxFinancialMonthEnd.Text, DateTime.Now, "MMMM dd"),
            };
            r.Update(c, id);
        }
    }
}