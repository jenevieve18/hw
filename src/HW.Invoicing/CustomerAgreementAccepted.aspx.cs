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
    public partial class CustomerAgreementAccepted : System.Web.UI.Page
    {
        SqlCompanyRepository r = new SqlCompanyRepository();
        protected Company company;

        protected void Page_Load(object sender, EventArgs e)
        {
            company = r.Read(ConvertHelper.ToInt32(Request["Id"]));
            if (company == null)
            {
                Response.Redirect("companies.aspx");
            }
        }

        protected void buttonBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("companyterms.aspx?Id={0}", company.Id));
        }

        protected void buttonNextClick(object sender, EventArgs e)
        {
            Db.sendMail("ian.escarro@gmail.com", "Your contract", "This is your contract!");
            Response.Redirect(string.Format("companytermsthanks.aspx?Id={0}", company.Id));
        }
    }
}