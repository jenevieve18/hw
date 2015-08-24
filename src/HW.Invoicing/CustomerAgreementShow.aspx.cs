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
        SqlCompanyRepository cr = new SqlCompanyRepository();
        protected Company company;
        SqlCustomerRepository r = new SqlCustomerRepository();
        CustomerAgreement a;
        Customer customer;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = ConvertHelper.ToInt32(Request["Id"]);

            a = r.ReadAgreement(id);
            HtmlHelper.RedirectIf(a == null, "customers.aspx");

            company = cr.Read(ConvertHelper.ToInt32(Request.QueryString["CompanyId"]));

            customer = r.Read(ConvertHelper.ToInt32(Request["CustomerId"]));
            
            if (a != null)
            {
                textBoxAgreementLecturer.Text = a.Lecturer;
                textBoxAgreementDate.Text = a.Date.Value.ToString("yyyy-MM-dd");
                textBoxAgreementRuntime.Text = a.Runtime;
                textBoxAgreementLectureTitle.Text = a.LectureTitle;
                textBoxAgreementContact.Text = a.Contact;
                textBoxAgreementMobile.Text = a.Mobile;
                textBoxAgreementEmail.Text = a.Email;
                textBoxAgreementCompensation.Text = a.Compensation;
                textBoxAgreementPaymentTerms.Text = a.PaymentTerms;
                textBoxAgreementBillingAddress.Text = a.BillingAddress;
                textBoxAgreementOtherInformation.Text = a.OtherInformation;
            }
        }

        protected void buttonNextClick(object sender, EventArgs e)
        {
            Session["AgreementLecturer"] = textBoxAgreementLecturer.Text;
            Session["AgreementDate"] = textBoxAgreementDate.Text;
            Session["AgreementRuntime"] = textBoxAgreementRuntime.Text;
            Session["AgreementLectureTitle"] = textBoxAgreementLectureTitle.Text;
            Session["AgreementLocation"] = textBoxAgreementLocation.Text;
            Session["AgreementContact"] = textBoxAgreementContact.Text;
            Session["AgreementMobile"] = textBoxAgreementMobile.Text;
            Session["AgreementEmail"] = textBoxAgreementEmail.Text;
            Session["AgreementCompensation"] = textBoxAgreementCompensation.Text;
            Session["AgreementPaymentTerms"] = textBoxAgreementPaymentTerms.Text;
            Session["AgreementBillingAddress"] = textBoxAgreementBillingAddress.Text;
            Session["AgreementOtherInformation"] = textBoxAgreementOtherInformation.Text;

            Response.Redirect(string.Format("customeragreementaccepted.aspx?Id={0}&CompanyId={1}&CustomerId={2}", id, company.Id, customer.Id));
        }
    }
}