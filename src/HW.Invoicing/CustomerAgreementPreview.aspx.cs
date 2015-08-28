﻿using System;
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
    public partial class CustomerAgreementPreview : System.Web.UI.Page
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
            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            companyId = ConvertHelper.ToInt32(Request.QueryString["CompanyId"]);
            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);

            HtmlHelper.RedirectIf(Session["CustomerName"] == null, string.Format("customeragreementshow.aspx?Id={0}&CompanyId={1}&CustomerId={2}", id, companyId, customerId));

            agreement = r.ReadAgreement(id);

            company = cr.Read(companyId);

            customer = r.Read(customerId);

            labelCustomerName.Text = Session["CustomerName"].ToString();
            labelCustomerPostalAddress.Text = Session["CustomerPostalAddress"].ToString();
            labelCustomerNumber.Text = Session["CustomerNumber"].ToString();
            labelCustomerInvoiceAddress.Text = Session["CustomerInvoiceAddress"].ToString();
            labelCustomerReferenceNumber.Text = Session["CustomerReferenceNumber"].ToString();

            labelCompanyName.Text = company.ToString().Replace("\n", "<br>");

            labelAgreementLecturer.Text = agreement.Lecturer;
            labelAgreementLectureTitle.Text = Session["AgreementLectureTitle"].ToString();

            labelAgreementContact.Text = Session["AgreementContact"].ToString();
            labelAgreementMobile.Text = Session["AgreementMobile"].ToString();
            labelAgreementEmail.Text = Session["AgreementEmail"].ToString();
            labelAgreementCompensation.Text = ConvertHelper.ToDecimal(Session["AgreementCompensation"]).ToString("### ##0.00");
            labelAgreementOtherInformation.Text = Session["AgreementOtherInformation"].ToString().Replace("\n", "<br>");
            labelPaymentTerms.Text = Session["AgreementPaymentTerms"].ToString();

            labelAgreementContactPlaceSigned.Text = Session["AgreementContactPlaceSigned"].ToString();
            labelAgreementContactDateSigned.Text = Session["AgreementContactDateSigned"].ToString();
            labelAgreementContactName.Text = Session["AgreementContactName"].ToString();
            labelAgreementContactTitle.Text = Session["AgreementContactTitle"].ToString();
            labelAgreementContactCompany.Text = Session["AgreementContactCompany"].ToString();

            labelAgreementDateSigned.Text = agreement.DateSigned.Value.ToString("yyyy-MM-dd");
        }

        protected void buttonBackClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("customeragreementshow.aspx?Id={0}&CompanyId={1}&CustomerId={2}&Session=1", id, companyId, customerId));
        }

        protected void buttonNextClick(object sender, EventArgs e)
        {
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
                Id = agreement.Id,
                Date = agreement.Date,
                Lecturer = agreement.Lecturer,
                LectureTitle = Session["AgreementLectureTitle"].ToString(),

                Contact = Session["AgreementContact"].ToString(),
                Mobile = Session["AgreementMobile"].ToString(),
                Email = Session["AgreementEmail"].ToString(),
                Compensation = ConvertHelper.ToDecimal(Session["AgreementCompensation"]),
                PaymentTerms = agreement.PaymentTerms,
                OtherInformation = Session["AgreementOtherInformation"].ToString(),
                
                ContactPlaceSigned = Session["AgreementContactPlaceSigned"].ToString(),
                ContactDateSigned = ConvertHelper.ToDateTime(Session["AgreementContactDateSigned"].ToString()),
                ContactName = Session["AgreementContactName"].ToString(),
                ContactTitle = Session["AgreementContactTitle"].ToString(),
                ContactCompany = Session["AgreementContactCompany"].ToString(),

                IsClosed = true,
                DateTimeAndPlaces = Session["AgreementDateTimeAndPlaces"] as List<CustomerAgreementDateTimeAndPlace>
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