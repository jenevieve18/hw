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
        protected List<CustomerAgreementDateTimeAndPlace> dateTimeAndPlaces = new List<CustomerAgreementDateTimeAndPlace>();

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

                        labelCompanyName.Text = company.ToString().Replace("\n", "<br>");

                        textBoxAgreementLectureTitle.Text = agreement.LectureTitle;

                        // Customer agreement datetime and places

                        textBoxAgreementContact.Text = agreement.Contact;
                        textBoxAgreementMobile.Text = agreement.Mobile;
                        textBoxAgreementEmail.Text = agreement.Email;
                        textBoxAgreementCompensation.Text = agreement.Compensation;
                        textBoxAgreementOtherInformation.Text = agreement.OtherInformation;

                        labelPaymentTerms.Text = agreement.PaymentTerms;

                        textBoxAgreementPlaceSigned.Text = agreement.PlaceSigned;
                        labelAgreementDate.Text = agreement.Date.Value.ToString("yyyy-MM-dd");
                        textBoxAgreementContactName.Text = agreement.ContactName;
                        textBoxAgreementContactTitle.Text = agreement.ContactTitle;
                        textBoxAgreementContactCompany.Text = agreement.ContactCompany;
                    }
                    else
                    {
                        textBoxCustomerName.Text = Session["CustomerName"].ToString();
                        textBoxCustomerPostalAddress.Text = Session["CustomerPostalAddress"].ToString();
                        textBoxCustomerNumber.Text = Session["CustomerNumber"].ToString();
                        textBoxCustomerInvoiceAddress.Text = Session["CustomerInvoiceAddress"].ToString();
                        textBoxCustomerReferenceNumber.Text = Session["CustomerReferenceNumber"].ToString();

                        labelCompanyName.Text = company.ToString().Replace("\n", "<br>");
                        
                        textBoxAgreementLectureTitle.Text = Session["AgreementLectureTitle"].ToString();

                        // Customer agreement datetime and places

                        dateTimeAndPlaces = Session["AgreementDateTimeAndPlaces"] as List<CustomerAgreementDateTimeAndPlace>;

                        textBoxAgreementContact.Text = Session["AgreementContact"].ToString();
                        textBoxAgreementMobile.Text = Session["AgreementMobile"].ToString();
                        textBoxAgreementEmail.Text = Session["AgreementEmail"].ToString();
                        textBoxAgreementCompensation.Text = Session["AgreementCompensation"].ToString();
                        textBoxAgreementOtherInformation.Text = Session["AgreementOtherInformation"].ToString();

                        labelPaymentTerms.Text = Session["AgreementPaymentTerms"].ToString();

                        textBoxAgreementPlaceSigned.Text = Session["AgreementPlaceSigned"].ToString();
                        textBoxAgreementDateSigned.Text = Session["AgreementDateSigned"].ToString();
                        //labelAgreementDate.Text = Session["AgreementDate"].ToString();
                        labelAgreementDate.Text = agreement.Date.Value.ToString("yyyy-MM-dd");
                        textBoxAgreementContactName.Text = Session["AgreementContactName"].ToString();
                        textBoxAgreementContactTitle.Text = Session["AgreementContactTitle"].ToString();
                        textBoxAgreementContactCompany.Text = Session["AgreementContactCompany"].ToString();
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

            Session["AgreementLectureTitle"] = textBoxAgreementLectureTitle.Text;

            var dates = Request.Form.GetValues("agreement-date");
            var timeFroms = Request.Form.GetValues("agreement-timefrom");
            var timeTos = Request.Form.GetValues("agreement-timeto");
            var addresses = Request.Form.GetValues("agreement-address");
            int i = 0;
            List<CustomerAgreementDateTimeAndPlace> dateTimeAndPlaces = new List<CustomerAgreementDateTimeAndPlace>();
            if (dates != null)
            {
                foreach (var d in dates)
                {
                    var dt = new CustomerAgreementDateTimeAndPlace
                    {
                        Date = d,
                        TimeFrom = timeFroms[i],
                        TimeTo = timeFroms[i],
                        Address = addresses[i]
                    };
                    dateTimeAndPlaces.Add(dt);
                    i++;
                }
            }
            Session["AgreementDateTimeAndPlaces"] = dateTimeAndPlaces;

            Session["AgreementContact"] = textBoxAgreementContact.Text;
            Session["AgreementMobile"] = textBoxAgreementMobile.Text;
            Session["AgreementEmail"] = textBoxAgreementEmail.Text;
            Session["AgreementCompensation"] = textBoxAgreementCompensation.Text;
            Session["AgreementOtherInformation"] = textBoxAgreementOtherInformation.Text;
            Session["AgreementPaymentTerms"] = agreement.PaymentTerms;

            Session["AgreementPlaceSigned"] = textBoxAgreementPlaceSigned.Text;
            Session["AgreementDateSigned"] = textBoxAgreementDateSigned.Text;
            Session["AgreementContactName"] = textBoxAgreementContactName.Text;
            Session["AgreementContactTitle"] = textBoxAgreementContactTitle.Text;
            Session["AgreementContactCompany"] = textBoxAgreementContactCompany.Text;

            Response.Redirect(string.Format("customeragreementaccepted.aspx?Id={0}&CompanyId={1}&CustomerId={2}", id, company.Id, customer.Id));
        }
    }
}