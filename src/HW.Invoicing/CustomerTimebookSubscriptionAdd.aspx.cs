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

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
            customers = r.FindActiveSubscribersByCompany(companyId);
            if (!IsPostBack)
            {
                var now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                startDate = now;
                var endDate = now.AddMonths(1).AddDays(-1);
                textBoxStartDate.Text = startDate.ToString("yyyy-MM-dd");
                textBoxEndDate.Text = endDate.ToString("yyyy-MM-dd");

                textBoxQuantity.Text = ((endDate.Month - startDate.Month) + 12 * (endDate.Year - startDate.Year)).ToString();

                textBoxText.Text = "Subscription fee for HealthWatch.se";
                textBoxComments.Text = textBoxText.Text + " " + textBoxStartDate.Text.Replace('-', '.') + " - " + textBoxEndDate.Text.Replace('-', '.');
            }
        }

        protected void buttonClear_Click(object sender, EventArgs e)
        {
            r.ClearSubscriptionTimebooks();
            message = "<div class='alert alert-danger'>Subscription timebooks deleted.</div>";
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var startDate = ConvertHelper.ToDateTime(textBoxStartDate.Text);
            if (r.HasSubscriptionTimebookWithDate(startDate))
            {
                message = "<div class='alert alert-danger'>Invalid subscription timebook. Start date selected is in the database. Please check and try again.</div>";
            }
            else
            {
                var quantities = Request.Form.GetValues("subscription-quantities");
                var comments = Request.Form.GetValues("subscription-comments");
                var startDates = Request.Form.GetValues("subscription-start-date");
                var endDates = Request.Form.GetValues("subscription-end-date");
                var timebooks = new List<CustomerTimebook>();
                int i = 0;
                foreach (var c in customers)
                {
                    var sDate = ConvertHelper.ToDateTime(startDates[i]);
                    if (c.HasSubscription && c.SubscriptionStartDate <= sDate)
                    {
                        var t = new CustomerTimebook
                        {
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
            }
        }
    }
}