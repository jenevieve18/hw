using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class CustomerAgreementSend : System.Web.UI.Page
    {
        SqlCustomerRepository r = new SqlCustomerRepository();
        SqlCompanyRepository cr = new SqlCompanyRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            int companyId = ConvertHelper.ToInt32(Request.QueryString["CompanyId"]);
            int customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);

            var agreement = r.ReadAgreement(id);
            var company = cr.Read(companyId);
            if (agreement != null)
            {
                var body = company.AgreementEmailText;
                Db.sendMail(
                    company.Email,
                    agreement.Email,
                    company.AgreementEmailSubject,
                    body
                );

                Session["Message"] = "Customer agreement sent to customer.";
                Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=agreements", customerId));
            }
        }
    }
}