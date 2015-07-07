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
    public partial class CustomerTimebookSubscriptionAdd : System.Web.UI.Page
    {
        protected IList<Customer> customers;
        SqlCustomerRepository r = new SqlCustomerRepository();
        protected string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            customers = r.FindActiveSubscribers();
            if (!IsPostBack)
            {
                var startDate = DateTime.Now;
                var endDate = DateTime.Now.AddMonths(1);
                textBoxStartDate.Text = startDate.ToString("yyyy-MM-dd");
                textBoxEndDate.Text = endDate.ToString("yyyy-MM-dd");

                textBoxQuantity.Text = ((endDate.Month - startDate.Month) + 12 * (endDate.Year - startDate.Year)).ToString();

                textBoxText.Text = "Subscription fee for HealthWatch.se";
                textBoxComments.Text = textBoxText.Text + " " + textBoxStartDate.Text.Replace('-', '.') + " - " + textBoxEndDate.Text.Replace('-', '.');
            }
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
                var timebooks = new List<CustomerTimebook>();
                int i = 0;
                foreach (var c in customers)
                {
                    if (c.HasSubscription && c.SubscriptionStartDate < startDate)
                    {
                        timebooks.Add(
                            new CustomerTimebook
                            {
                                Customer = c,
                                Item = new Item { Id = c.SubscriptionItem.Id },
                                Quantity = ConvertHelper.ToInt32(quantities[i]),
                                Price = c.SubscriptionItem.Price,
                                VAT = 25,
                                Comments = comments[i],
                                IsSubscription = true,
                                SubscriptionStartDate = ConvertHelper.ToDateTime(textBoxStartDate.Text),
                                SubscriptionEndDate = ConvertHelper.ToDateTime(textBoxEndDate.Text)
                            }
                        );
                    }
                    i++;
                }
                r.SaveSubscriptionTimebooks(timebooks);
                message = "<div class='alert alert-success'>Saved! customer timebooks for subscription items are now saved.</div>";
            }
        }
    }
}