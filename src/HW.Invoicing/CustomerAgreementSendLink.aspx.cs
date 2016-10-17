using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;
using System.Configuration;

namespace HW.Invoicing
{
    public partial class CustomerAgreementSendLink : System.Web.UI.Page
    {
        SqlCustomerRepository r = new SqlCustomerRepository();
        SqlCompanyRepository cr = new SqlCompanyRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            int customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            int companyId = ConvertHelper.ToInt32(Request.QueryString["CompanyId"]);
            
            var agreement = r.ReadAgreement(id);
            var company = cr.Read(companyId);

            if (agreement != null)
            {
                try
                {
                    string body = company.AgreementEmailText;
                    body = body.Replace(
                        "{LINK}",
                        string.Format(
                            "{0}customeragreementshow.aspx?Id={1}&CompanyId={2}&CustomerId={3}",
                            ConfigurationManager.AppSettings["InvoiceUrl"],
                            id,
                            companyId,
                            customerId
                        )
                    );
                    Db.sendMail2(
                        "info@danhasson.se",
                        agreement.Email,
                        company.AgreementEmailSubject,
                        body
                    );

                    Session["Message"] = "<div class='alert alert-success'>Customer agreement link was sent to the customer.</div>";
                }
                catch (Exception ex)
                {
                    Session["Message"] = string.Format("<div class='alert alert-danger'>{0}</div>", ex.Message);
                }
            }

            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=agreements", customerId));
        }
    }
}