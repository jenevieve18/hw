using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using HW.Core.Helpers;
using System.Configuration;

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
            HtmlHelper.RedirectIf(Session["CustomerName"] == null, "default.aspx");

            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            companyId = ConvertHelper.ToInt32(Request.QueryString["CompanyId"]);
            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            
            agreement = r.ReadAgreement(id);

            company = cr.Read(companyId);

            customer = r.Read(customerId);
        }

        protected void buttonBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("customeragreementshow.aspx?Id={0}&CompanyId={1}&CustomerId={2}", id, companyId, customerId));
        }

        protected void buttonNextClick(object sender, EventArgs e)
        {
            //Db.sendMail("ian.escarro@gmail.com", "Your contract", "This is your contract!");

            var c = new Customer
            {
                Name = Session["CustomerName"].ToString(),
                PostalAddress = Session["CustomerPostalAddress"].ToString(),
                Number = Session["CustomerNumber"].ToString(),
                InvoiceAddress = Session["CustomerInvoiceAddress"].ToString(),
                PurchaseOrderNumber = Session["CustomerReferenceNumber"].ToString()
            };
            r.Update2(c, customerId);

            var a = new CustomerAgreement
            {
                Lecturer = agreement.Lecturer,
                Date = ConvertHelper.ToDateTime(Session["AgreementDate"].ToString()),
                Runtime = Session["AgreementRuntime"].ToString(),
                LectureTitle = Session["AgreementLectureTitle"].ToString(),
                Location = Session["AgreementLocation"].ToString(),
                Contact = Session["AgreementContact"].ToString(),
                Mobile = Session["AgreementMobile"].ToString(),
                Email = Session["AgreementEmail"].ToString(),
                Compensation = Session["AgreementCompensation"].ToString(),
                PaymentTerms = agreement.PaymentTerms,
                OtherInformation = Session["AgreementOtherInformation"].ToString(),
                DateSigned = ConvertHelper.ToDateTime(Session["AgreementDateSigned"].ToString()),
                CustomerName = Session["AgreementCustomerName"].ToString(),
                CustomerTitle = Session["AgreementCustomerTitle"].ToString(),
                CustomerCompany = Session["AgreementCustomerCompany"].ToString()
            };
            r.UpdateAgreement(a, id);

            Db.sendMail(
                company.Email,
                string.Format("Customer Agreement for {0} is updated", c.Name),
                string.Format(@"Updated customer agreement.

Please visit this link to review the agreement!

{0}customershow.aspx?Id={1}&SelectedTab=agreements

Yours,
InvoicingSystem", ConfigurationManager.AppSettings["InvoiceURL"], customerId)
            );

            Response.Redirect(string.Format("customeragreementthanks.aspx?Id={0}&CompanyId={1}&CustomerId={2}", id, companyId, customerId));
        }
    }
}