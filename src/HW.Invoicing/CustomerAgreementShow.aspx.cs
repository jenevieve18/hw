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
        protected CustomerAgreement agreement;
        Customer customer;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = ConvertHelper.ToInt32(Request["Id"]);

            agreement = r.ReadAgreement(id);
            HtmlHelper.RedirectIf(agreement == null, "customers.aspx");

            company = cr.Read(ConvertHelper.ToInt32(Request.QueryString["CompanyId"]));

            customer = r.Read(ConvertHelper.ToInt32(Request["CustomerId"]));
            
            if (agreement != null && company != null && customer != null)
            {
                textBoxCustomerName.Text = customer.Name;
                textBoxCustomerAddress.Text = customer.Address;
                textBoxCustomerNumber.Text = customer.Number;
                textBoxCustomerInvoiceAddress.Text = customer.InvoiceAddress;
                textBoxCustomerReferenceNumber.Text = customer.PurchaseOrderNumber;

                textBoxAgreementDate.Text = agreement.Date.Value.ToString("yyyy-MM-dd");
                textBoxAgreementRuntime.Text = agreement.Runtime;
                textBoxAgreementLectureTitle.Text = agreement.LectureTitle;
                textBoxAgreementContact.Text = agreement.Contact;
                textBoxAgreementMobile.Text = agreement.Mobile;
                textBoxAgreementEmail.Text = agreement.Email;
                textBoxAgreementCompensation.Text = agreement.Compensation;
                textBoxAgreementOtherInformation.Text = agreement.OtherInformation;
            }
        }

        protected void buttonNextClick(object sender, EventArgs e)
        {
            Session["AgreementDate"] = textBoxAgreementDate.Text;
            Session["AgreementRuntime"] = textBoxAgreementRuntime.Text;
            Session["AgreementLectureTitle"] = textBoxAgreementLectureTitle.Text;
            Session["AgreementLocation"] = textBoxAgreementLocation.Text;
            Session["AgreementContact"] = textBoxAgreementContact.Text;
            Session["AgreementMobile"] = textBoxAgreementMobile.Text;
            Session["AgreementEmail"] = textBoxAgreementEmail.Text;
            Session["AgreementCompensation"] = textBoxAgreementCompensation.Text;
            Session["AgreementOtherInformation"] = textBoxAgreementOtherInformation.Text;
            Session["AgreementPaymentTerms"] = agreement.PaymentTerms;

            Response.Redirect(string.Format("customeragreementaccepted.aspx?Id={0}&CompanyId={1}&CustomerId={2}", id, company.Id, customer.Id));
        }
    }
}