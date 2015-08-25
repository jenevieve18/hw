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
        int companyId;
        int customerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = ConvertHelper.ToInt32(Request["Id"]);
            companyId = ConvertHelper.ToInt32(Request.QueryString["CompanyId"]);
            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);

            agreement = r.ReadAgreement(id);
            HtmlHelper.RedirectIf(agreement == null, string.Format("customeragreementclosed.aspx?Id={0}&CompanyId={1}&CustomerId={2}", id, companyId, customerId));
            HtmlHelper.RedirectIf(agreement.IsClosed, string.Format("customeragreementclosed.aspx?Id={0}&CompanyId={1}&CustomerId={2}", id, companyId, customerId));

            company = cr.Read(companyId);

            customer = r.Read(customerId);

            bool isSession = ConvertHelper.ToInt32(Request.QueryString["Session"]) == 1;

            if (!IsPostBack)
            {
                if (agreement != null && company != null && customer != null)
                {
                    if (!isSession)
                    {
                        textBoxCustomerName.Text = customer.Name;
                        textBoxCustomerPostalAddress.Text = customer.PostalAddress;
                        textBoxCustomerNumber.Text = customer.Number;
                        textBoxCustomerInvoiceAddress.Text = customer.InvoiceAddress;
                        textBoxCustomerReferenceNumber.Text = customer.PurchaseOrderNumber;

                        textBoxAgreementDate.Text = agreement.Date.Value.ToString("yyyy-MM-dd");
                        textBoxAgreementRuntime.Text = agreement.Runtime;
                        textBoxAgreementLectureTitle.Text = agreement.LectureTitle;
                        textBoxAgreementLocation.Text = agreement.Location;
                        textBoxAgreementContact.Text = agreement.Contact;
                        textBoxAgreementMobile.Text = agreement.Mobile;
                        textBoxAgreementEmail.Text = agreement.Email;
                        textBoxAgreementCompensation.Text = agreement.Compensation;
                        textBoxAgreementOtherInformation.Text = agreement.OtherInformation;

                        //textBoxAgreementDateSigned.Text = agreement.DateSigned.Value.ToString("yyyy-MM-dd");
                        textBoxAgreementCustomerName.Text = agreement.CustomerName;
                        textBoxAgreementCustomerTitle.Text = agreement.CustomerTitle;
                        textBoxAgreementCustomerCompany.Text = agreement.CustomerCompany;
                    }
                    else
                    {
                        textBoxCustomerName.Text = Session["CustomerName"].ToString();
                        textBoxCustomerPostalAddress.Text = Session["CustomerPostalAddress"].ToString();
                        textBoxCustomerNumber.Text = Session["CustomerNumber"].ToString();
                        textBoxCustomerInvoiceAddress.Text = Session["CustomerInvoiceAddress"].ToString();
                        textBoxCustomerReferenceNumber.Text = Session["CustomerReferenceNumber"].ToString();
                        
                        textBoxAgreementDate.Text = Session["AgreementDate"].ToString();
                        textBoxAgreementRuntime.Text = Session["AgreementRuntime"].ToString();
                        textBoxAgreementLectureTitle.Text = Session["AgreementLectureTitle"].ToString();
                        textBoxAgreementLocation.Text = Session["AgreementLocation"].ToString();
                        textBoxAgreementContact.Text = Session["AgreementContact"].ToString();
                        textBoxAgreementMobile.Text = Session["AgreementMobile"].ToString();
                        textBoxAgreementEmail.Text = Session["AgreementEmail"].ToString();
                        textBoxAgreementCompensation.Text = Session["AgreementCompensation"].ToString();
                        textBoxAgreementOtherInformation.Text = Session["AgreementOtherInformation"].ToString();
                        agreement.PaymentTerms = Session["AgreementPaymentTerms"].ToString();
                        
                        textBoxAgreementDateSigned.Text = Session["AgreementDateSigned"].ToString();
                        textBoxAgreementCustomerName.Text = Session["AgreementCustomerName"].ToString();
                        textBoxAgreementCustomerTitle.Text = Session["AgreementCustomerTitle"].ToString();
                        textBoxAgreementCustomerCompany.Text = Session["AgreementCustomerCompany"].ToString();
                    }
                }
            }
        }

        protected void buttonNextClick(object sender, EventArgs e)
        {
            Session["CustomerName"] = textBoxCustomerName.Text;
            Session["CustomerPostalAddress"] = textBoxCustomerPostalAddress.Text;
            Session["CustomerNumber"] = textBoxCustomerNumber.Text;
            Session["CustomerInvoiceAddress"] = textBoxCustomerInvoiceAddress.Text;
            Session["CustomerReferenceNumber"] = textBoxCustomerReferenceNumber.Text;

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

            Session["AgreementDateSigned"] = textBoxAgreementDateSigned.Text;
            Session["AgreementCustomerName"] = textBoxAgreementCustomerName.Text;
            Session["AgreementCustomerTitle"] = textBoxAgreementCustomerTitle.Text;
            Session["AgreementCustomerCompany"] = textBoxAgreementCustomerCompany.Text;

            Response.Redirect(string.Format("customeragreementaccepted.aspx?Id={0}&CompanyId={1}&CustomerId={2}", id, company.Id, customer.Id));
        }
    }
}