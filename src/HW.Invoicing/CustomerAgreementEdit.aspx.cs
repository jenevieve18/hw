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
            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            HtmlHelper.RedirectIf(id == 0, string.Format("customershow.aspx?Id={0}&SelectedTab=agreement", customerId));

            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            HtmlHelper.RedirectIf(customerId == 0, "customers.aspx");
            
            if (!IsPostBack)
            {
                var a = r.ReadAgreement(id);
                if (a != null)
                {
                    textBoxAgreementLecturer.Text = a.Lecturer;
                    textBoxAgreementDate.Text = a.Date == null ? "" : a.Date.Value.ToString("yyyy-MM-dd");
                    textBoxAgreementLectureTitle.Text = a.LectureTitle;
                    textBoxAgreementContact.Text = a.Contact;
                    textBoxAgreementMobile.Text = a.Mobile;
                    textBoxAgreementEmail.Text = a.Email;
                    textBoxAgreementCompensation.Text = a.Compensation.ToString();
                    textBoxAgreementPaymentTerms.Text = a.PaymentTerms;
                    textBoxAgreementOtherInformation.Text = a.OtherInformation;

                    textBoxAgreementContactPlaceSigned.Text = a.ContactPlaceSigned;
                    textBoxAgreementContactDateSigned.Text = a.ContactDateSigned == null ? "" : a.ContactDateSigned.Value.ToString("yyyy-MM-dd");
                    textBoxAgreementContactName.Text = a.ContactName;
                    textBoxAgreementContactTitle.Text = a.ContactTitle;
                    textBoxAgreementContactCompany.Text = a.ContactCompany;

                    textBoxAgreementDateSigned.Text = a.DateSigned == null ? "" : a.DateSigned.Value.ToString("yyyy-MM-dd");

                    checkBoxClosed.Checked = a.IsClosed;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var a = new CustomerAgreement
            {
                Date = ConvertHelper.ToDateTime(textBoxAgreementDate.Text),
                Lecturer = textBoxAgreementLecturer.Text,
                LectureTitle = textBoxAgreementLectureTitle.Text,
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
            r.UpdateAgreement(a, id);
            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=agreements", customerId));
        }
    }
}