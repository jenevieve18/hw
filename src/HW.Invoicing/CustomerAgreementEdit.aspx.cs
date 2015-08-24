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

        protected void Page_Load(object sender, EventArgs e)
        {
            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            HtmlHelper.RedirectIf(customerId == 0, "customers.aspx");
            
            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            HtmlHelper.RedirectIf(id == 0, string.Format("customershow.aspx?Id={0}&SelectedTab=agreement", customerId));
            
            if (!IsPostBack)
            {
                var a = r.ReadAgreement(id);
                if (a != null)
                {
                    textBoxAgreementDate.Text = a.Date.Value.ToString("yyyy-MM-dd");
                    textBoxAgreementLecturer.Text = a.Lecturer;
                    textBoxAgreementRuntime.Text = a.Runtime;
                    textBoxAgreementLectureTitle.Text = a.LectureTitle;
                    textBoxAgreementLocation.Text = a.Location;
                    textBoxAgreementContact.Text = a.Contact;
                    textBoxAgreementMobile.Text = a.Mobile;
                    textBoxAgreementEmail.Text = a.Email;
                    textBoxAgreementCompensation.Text = a.Compensation;
                    textBoxAgreementPaymentTerms.Text = a.PaymentTerms;
                    textBoxAgreementBillingAddress.Text = a.BillingAddress;
                    textBoxAgreementOtherInformation.Text = a.OtherInformation;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var a = new CustomerAgreement
            {
                Date = ConvertHelper.ToDateTime(textBoxAgreementDate.Text),
                Lecturer = textBoxAgreementLecturer.Text,
                Runtime = textBoxAgreementRuntime.Text,
                LectureTitle = textBoxAgreementLectureTitle.Text,
                Location = textBoxAgreementLocation.Text,
                Contact = textBoxAgreementContact.Text,
                Mobile = textBoxAgreementMobile.Text,
                Email = textBoxAgreementEmail.Text,
                Compensation = textBoxAgreementCompensation.Text,
                PaymentTerms = textBoxAgreementPaymentTerms.Text,
                BillingAddress = textBoxAgreementBillingAddress.Text,
                OtherInformation = textBoxAgreementOtherInformation.Text,
            };
            r.UpdateAgreement(a, id);
            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=agreements", customerId));
        }
    }
}