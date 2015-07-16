using System;
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
            var c = new Company {
                Name = textBoxName.Text,
                Address = textBoxAddress.Text,
                Phone = textBoxPhone.Text,
                BankAccountNumber = textBoxBankAccountNumber.Text,
                TIN = textBoxTIN.Text,
                FinancialMonthStart = ConvertHelper.ToDateTime(textBoxFinancialMonthStart.Text),
                FinancialMonthEnd = ConvertHelper.ToDateTime(textBoxFinancialMonthEnd.Text),
                HasSubscriber = checkBoxHasSubscriber.Checked
            };
            r.Save(c);
            Response.Redirect("companies.aspx");
        }
    }
}