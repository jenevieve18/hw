using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class CustomerAgreementEdit : System.Web.UI.Page
    {
        SqlCustomerRepository r = new SqlCustomerRepository();
        protected int customerId;
        int id;
        protected CustomerAgreement agreement;
        protected string message;

        protected List<CustomerAgreementDateTimeAndPlace> dateTimeAndPlaces = new List<CustomerAgreementDateTimeAndPlace>();

        protected void Page_Load(object sender, EventArgs e)
        {
            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            HtmlHelper.RedirectIf(id == 0, string.Format("customershow.aspx?Id={0}&SelectedTab=agreement", customerId));

            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            HtmlHelper.RedirectIf(customerId == 0, "customers.aspx");
            
            if (!IsPostBack)
            {
                agreement = r.ReadAgreement(id);
                if (agreement != null)
                {
                    textBoxAgreementLecturer.Text = agreement.Lecturer;
                    textBoxAgreementDate.Text = agreement.Date == null ? "" : agreement.Date.Value.ToString("yyyy-MM-dd");
                    textBoxAgreementLectureTitle.Text = agreement.LectureTitle;
                    textBoxAgreementContact.Text = agreement.Contact;
                    textBoxAgreementMobile.Text = agreement.Mobile;
                    textBoxAgreementEmail.Text = agreement.Email;
                    textBoxAgreementCompensation.Text = agreement.Compensation.ToString();
                    textBoxAgreementPaymentTerms.Text = agreement.PaymentTerms;
                    textBoxAgreementOtherInformation.Text = agreement.OtherInformation;

                    textBoxAgreementContactPlaceSigned.Text = agreement.ContactPlaceSigned;
                    textBoxAgreementContactDateSigned.Text = agreement.ContactDateSigned == null ? "" : agreement.ContactDateSigned.Value.ToString("yyyy-MM-dd");
                    textBoxAgreementContactName.Text = agreement.ContactName;
                    textBoxAgreementContactTitle.Text = agreement.ContactTitle;
                    textBoxAgreementContactCompany.Text = agreement.ContactCompany;

                    textBoxAgreementDateSigned.Text = agreement.DateSigned == null ? "" : agreement.DateSigned.Value.ToString("yyyy-MM-dd");

                    checkBoxClosed.Checked = agreement.IsClosed;

                    dateTimeAndPlaces = agreement.DateTimeAndPlaces;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var dates = Request.Form.GetValues("agreement-date");
            var timeFroms = Request.Form.GetValues("agreement-timefrom");
            var timeTos = Request.Form.GetValues("agreement-timeto");
            var addresses = Request.Form.GetValues("agreement-address");
            int i = 0;
            dateTimeAndPlaces = new List<CustomerAgreementDateTimeAndPlace>();
            if (dates != null)
            {
                foreach (var d in dates)
                {
                    var dt = new CustomerAgreementDateTimeAndPlace
                    {
                        Date = ConvertHelper.ToDateTime(d),
                        TimeFrom = timeFroms[i],
                        TimeTo = timeTos[i],
                        Address = addresses[i]
                    };
                    dateTimeAndPlaces.Add(dt);
                    i++;
                }
            }

            var a = new CustomerAgreement
            {
                Date = ConvertHelper.ToDateTime(textBoxAgreementDate.Text),
                Lecturer = textBoxAgreementLecturer.Text,
                LectureTitle = textBoxAgreementLectureTitle.Text,

                DateTimeAndPlaces = dateTimeAndPlaces,

                Contact = textBoxAgreementContact.Text,
                Mobile = textBoxAgreementMobile.Text,
                Email = textBoxAgreementEmail.Text,
                Compensation = ConvertHelper.ToDecimal(textBoxAgreementCompensation.Text),
                PaymentTerms = textBoxAgreementPaymentTerms.Text,
                OtherInformation = textBoxAgreementOtherInformation.Text,
                
                ContactPlaceSigned = textBoxAgreementContactPlaceSigned.Text,
                ContactDateSigned = ConvertHelper.ToDateTime(textBoxAgreementContactDateSigned.Text),
                ContactName = textBoxAgreementContactName.Text,
                ContactTitle = textBoxAgreementContactTitle.Text,
                ContactCompany = textBoxAgreementContactCompany.Text,
                
                DateSigned = ConvertHelper.ToDateTime(textBoxAgreementDateSigned.Text),
                
                IsClosed = checkBoxClosed.Checked
            };
            a.Validate();
            if (a.HasErrors)
            {
                message = a.Errors.ToHtmlUl();
            }
            else
            {
                r.UpdateAgreement(a, id);
                Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=agreements", customerId));
            }
        }
    }
}