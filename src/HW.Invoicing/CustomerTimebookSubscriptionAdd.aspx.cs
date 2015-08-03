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

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
            customers = r.FindActiveSubscribersByCompany(companyId);

            var now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            startDate = now;
            endDate = now.AddMonths(1).AddDays(-1);
            quantity = 1;
            string text = "Subscription fee for HealthWatch.se";
            generatedComments = text + " " + startDate.ToString("yyyy.MM.dd") + " - " + endDate.ToString("yyyy.MM.dd");

            if (!IsPostBack)
            {
                textBoxStartDate.Text = startDate.ToString("yyyy-MM-dd");
                textBoxEndDate.Text = endDate.ToString("yyyy-MM-dd");

                textBoxQuantity.Text = quantity.ToString(); //quantity.ToString("0.00") ;

                textBoxText.Text = text;
                textBoxComments.Text = generatedComments;
            }
        }

        protected void buttonClear_Click(object sender, EventArgs e)
        {
            r.ClearSubscriptionTimebooks();
            message = "<div class='alert alert-danger'>Subscription timebooks deleted.</div>";
            
            customers = r.FindActiveSubscribersByCompany(companyId);
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            /*startDate = ConvertHelper.ToDateTime(textBoxStartDate.Text);
            if (r.HasSubscriptionTimebookWithDate(startDate))
            {
                message = "<div class='alert alert-danger'>Invalid subscription timebook. Start date selected is in the database. Please check and try again.</div>";
            }
            else
            {*/
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
                    //if (c.HasSubscription && !c.CantCreateTimebook(sDate))
                    if (c.HasSubscription)
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
            //}

            customers = r.FindActiveSubscribersByCompany(companyId);
        }
    }
}