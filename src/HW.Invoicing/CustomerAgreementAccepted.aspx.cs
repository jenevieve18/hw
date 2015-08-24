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
        SqlCompanyRepository cr = new SqlCompanyRepository();
        protected Company company;
        protected CustomerAgreement agreement;
        SqlCustomerRepository r = new SqlCustomerRepository();
        Customer customer;
        int id;
        int customerId;
        int companyId;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            companyId = ConvertHelper.ToInt32(Request.QueryString["CompanyId"]);
            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            
            agreement = r.ReadAgreement(id);

            company = cr.Read(companyId);

            customer = r.Read(customerId);
        }

        protected void buttonBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("customeragreementshow.aspx?Id={0}&CompanyId={1}&CustomerId={2}", id));
        }

        protected void buttonNextClick(object sender, EventArgs e)
        {
            Db.sendMail("ian.escarro@gmail.com", "Your contract", "This is your contract!");
            Response.Redirect(string.Format("companytermsthanks.aspx?Id={0}", company.Id));
        }
    }
}