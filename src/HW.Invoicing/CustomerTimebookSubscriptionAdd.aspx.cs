using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;
using HW.Core.Services;
using System.Globalization;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;

namespace HW.Invoicing
{
    public partial class CustomerTimebookSubscriptionAdd : System.Web.UI.Page
    {
        protected IList<Customer> customers;
        SqlCustomerRepository r = new SqlCustomerRepository();
        protected string message;
        protected DateTime startDate;
        protected DateTime endDate;
        protected decimal quantity;
        protected string generatedComments;
        int companyId;
        SqlCompanyRepository cr = new SqlCompanyRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
            

            var company = cr.Read(companyId);

            var now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            startDate = now;
            endDate = now.AddMonths(1).AddDays(-1);
            quantity = 1;

            customers = r.FindActiveSubscribersByCompany(companyId, startDate, endDate);
                        
//            string text = "Subscription fee for HealthWatch.se";
string text = StrHelper.Str(company.SubscriptionText == "", "Subscription fee for HealthWatch.se", company.SubscriptionText);
            generatedComments = text + " " + startDate.ToString("yyyy.MM.dd") + " - " + endDate.ToString("yyyy.MM.dd");

            if (!IsPostBack)
            {
                textBoxStartDate.Text = startDate.ToString("yyyy-MM-dd");
                textBoxEndDate.Text = endDate.ToString("yyyy-MM-dd");

                textBoxQuantity.Text = quantity.ToString();

                textBoxText.Text = text;
                textBoxComments.Text = generatedComments;
            }
        }

        protected void buttonClear_Click(object sender, EventArgs e)
        {
            r.ClearSubscriptionTimebooks(companyId);
            message = "<div class='alert alert-danger'>Subscription timebooks deleted.</div>";

            customers = r.FindActiveSubscribersByCompany(companyId, startDate, endDate);
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            //startDate = ConvertHelper.ToDateTime(textBoxStartDate.Text);
            //endDate = ConvertHelper.ToDateTime(textBoxEndDate.Text);
            
            cr.SaveSubscriptionText(textBoxText.Text, companyId);
            
            customers = r.FindActiveSubscribersByCompany(companyId, startDate, endDate);

            var ids = Request.Form.GetValues("subscription-id");
            var startDates = Request.Form.GetValues("subscription-start-date");
            var endDates = Request.Form.GetValues("subscription-end-date");
            var quantities = Request.Form.GetValues("subscription-quantities");
            var comments = Request.Form.GetValues("subscription-comments");
            var timebooks = new List<CustomerTimebook>();
            int i = 0;
            foreach (var c in customers)
            {
                var sDate = ConvertHelper.ToDateTime(startDates[i]);
                if (c.HasSubscription && !c.CantCreateTimebook(sDate))
                {
                    var t = new CustomerTimebook
                    {
                        Id = ConvertHelper.ToInt32(ids[i]),
                        Customer = c,
                        Item = new Item { Id = c.SubscriptionItem.Id },
                        Quantity = ConvertHelper.ToDecimal(quantities[i], new CultureInfo("en-US")),
                        Price = c.SubscriptionItem.Price,
                        VAT = 25,
                        Comments = comments[i],
                        IsSubscription = true,
                        SubscriptionStartDate = sDate,
                        SubscriptionEndDate = ConvertHelper.ToDateTime(endDates[i])
                    };
                    timebooks.Add(t);
                }
                i++;
            }
            r.SaveSubscriptionTimebooks(timebooks);
            message = "<div class='alert alert-success'>Saved! customer timebooks for subscription items are now saved.</div>";

            customers = r.FindActiveSubscribersByCompany(companyId, startDate, endDate);
        }

        //[WebMethod]
        //public static List<object> FindActiveSubscribersByCompany(int companyId, DateTime startDate, DateTime endDate)
        //{
        //    var generatedComments = string.Format("Subscription fee for HealthWatch.se {0} - {1}", startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));

        //    var r = new SqlCustomerRepository();
        //    var d = r.FindActiveSubscribersByCompany(companyId, startDate, endDate);
        //    var customers = new List<object>();
        //    foreach (var c in d) {
        //        var sDate = c.GetLatestSubscriptionTimebookStartDate(startDate);
        //        var eDate = c.GetLatestSubscriptionTimebookEndDate(endDate);
        //        var cc = new {
        //            id = c.Id,
        //            name = c.Name,
        //            subscriptionStartAndEndDate = c.GetSubscriptionStartAndEndDate(),
        //            subscriptionStartDate = c.GetSubscriptionStartDate(),
        //            subscriptionHasEndDate = c.SubscriptionHasEndDate,
        //            subscriptionEndDate = c.GetSubscriptionEndDate(),
        //            subscriptionTimebookEndDateLabel = c.GetLatestSubscriptionTimebookEndDateLabel(),
        //            subscriptionItem = c.SubscriptionItem.Name,
        //            subscriptionItemUnit = c.SubscriptionItem.Unit.Name,
        //            subscriptionItemPrice = c.SubscriptionItem.Price,
        //            latestSubscriptionTimebookId = c.GetLatestSubscriptionTimebookId(),
        //            latestSubscriptionTimebookStartDate = sDate.ToString("yyyy-MM-dd"),
        //            latestSubscriptionTimebookEndDate = eDate.ToString("yyyy-MM-dd"),
        //            latestSubscriptionTimebookQuantity = c.GetLatestSubscriptionTimebookQuantity(sDate, eDate),
        //            comments = c.GetLatestSubscriptionTimebookComments(sDate, eDate, generatedComments),
        //            subscriptionTimebookAvailability = c.GetSubscriptionTimebookAvailability(startDate)
        //        };
        //        customers.Add(cc);
        //    }
        //    return customers;
        //}

        //protected void buttonClear_Click(object sender, EventArgs e)
        //{
        //    r.ClearSubscriptionTimebooks();
        //    message = "<div class='alert alert-danger'>Subscription timebooks deleted.</div>";
            
        //    customers = r.FindActiveSubscribersByCompany(companyId, startDate, endDate);
        //}

        //protected void buttonSave_Click(object sender, EventArgs e)
        //{
        //    startDate = ConvertHelper.ToDateTime(textBoxStartDate.Text);
        //    endDate = ConvertHelper.ToDateTime(textBoxEndDate.Text);
        //    customers = r.FindActiveSubscribersByCompany(companyId, startDate, endDate);

        //    var ids = Request.Form.GetValues("subscription-id");
        //    var startDates = Request.Form.GetValues("subscription-start-date");
        //    var endDates = Request.Form.GetValues("subscription-end-date");
        //    var quantities = Request.Form.GetValues("subscription-quantities");
        //    var comments = Request.Form.GetValues("subscription-comments");
        //    var timebooks = new List<CustomerTimebook>();
        //    int i = 0;
        //    foreach (var c in customers)
        //    {
        //        var sDate = ConvertHelper.ToDateTime(startDates[i]);
        //        if (c.HasSubscription && !c.CantCreateTimebook(sDate))
        //        {
        //            var t = new CustomerTimebook
        //            {
        //                Id = ConvertHelper.ToInt32(ids[i]),
        //                Customer = c,
        //                Item = new Item { Id = c.SubscriptionItem.Id },
        //                Quantity = ConvertHelper.ToDecimal(quantities[i], new CultureInfo("en-US")),
        //                Price = c.SubscriptionItem.Price,
        //                VAT = 25,
        //                Comments = comments[i],
        //                IsSubscription = true,
        //                SubscriptionStartDate = sDate,
        //                SubscriptionEndDate = ConvertHelper.ToDateTime(endDates[i])
        //            };
        //            timebooks.Add(t);
        //        }
        //        i++;
        //    }
        //    r.SaveSubscriptionTimebooks(timebooks);
        //    message = "<div class='alert alert-success'>Saved! customer timebooks for subscription items are now saved.</div>";

        //    customers = r.FindActiveSubscribersByCompany(companyId, startDate, endDate);
        //}
    }
}