﻿using System;
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
                var body = company.AgreementSignedEmailText;

                try
                {
                    Db.sendMail(
                        "info@danhasson.se",
                        agreement.Email,
                        company.AgreementSignedEmailSubject,
                        body
                    );
                    //MailHelper.SendMail(
                    //    company.Email,
                    //    agreement.Email,
                    //    company.AgreementEmailSubject,
                    //    company.AgreementEmailText,
                    //    body
                    //);

                    Session["Message"] = "<div class='alert alert-success'>Customer agreement sent to customer.</div>";
                }
                catch (Exception ex)
                {
                    Session["Message"] = string.Format("<div class='alert alert-danger'>{0}</div>", ex.Message);
                }
                Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=agreements", customerId));
            }
        }
    }
}