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
    public partial class CustomerAgreementShow : System.Web.UI.Page
    {
        SqlCompanyRepository r = new SqlCompanyRepository();
        protected Company company;
        SqlCustomerRepository cr = new SqlCustomerRepository();
        Customer customer;

        protected void Page_Load(object sender, EventArgs e)
        {
            company = r.Read(ConvertHelper.ToInt32(Request["Id"]));
            HtmlHelper.RedirectIf(company == null, "companies.aspx");

            customer = cr.Read(ConvertHelper.ToInt32(Request["CustomerId"]));
            
            if (customer != null)
            {
            }
        }

        protected void buttonNextClick(object sender, EventArgs e)
        {
            Session["TextBox1"] = TextBox1.Text;
            Session["TextBox2"] = TextBox2.Text;
            Session["TextBox3"] = TextBox3.Text;
            Session["TextBox4"] = TextBox4.Text;
            Session["TextBox5"] = TextBox5.Text;
            Session["TextBox6"] = TextBox6.Text;
            Session["TextBox7"] = TextBox7.Text;
            Session["TextBox8"] = TextBox8.Text;
            Session["TextBox9"] = TextBox9.Text;
            Session["TextBox10"] = TextBox10.Text;
            Session["TextBox11"] = TextBox11.Text;
            Session["TextBox12"] = TextBox12.Text;
            Session["TextBox13"] = TextBox13.Text;
            Session["TextBox14"] = TextBox14.Text;

            Response.Redirect(string.Format("companytermsaccepted.aspx?Id={0}", company.Id));
        }
    }
}