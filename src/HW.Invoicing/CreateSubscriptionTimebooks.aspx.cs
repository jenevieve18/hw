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
    public partial class CreateSubscriptionTimebooks : System.Web.UI.Page
    {
        protected IList<Customer> customers;
        SqlCustomerRepository r = new SqlCustomerRepository();

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

            var quantities = Request.Form.GetValues("subscription-quantities");
            var comments = Request.Form.GetValues("subscription-comments");
            var timebooks = new List<CustomerTimebook>();
            int i = 0;
            foreach (var c in customers)
            {
                timebooks.Add(
                    new CustomerTimebook
                    {
                        Customer = c,
                        Item = new Item { Id = c.SubscriptionItem.Id },
                        Quantity = ConvertHelper.ToInt32(quantities[i]),
                        Price = c.SubscriptionItem.Price,
                        VAT = 25,
                        Comments = comments[i]
                    }
                );
                i++;
            }
            r.SaveTimebooks(timebooks);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            customers = r.FindSubscribers();
            if (!IsPostBack)
            {
                var startDate = DateTime.Now;
                var endDate = DateTime.Now.AddMonths(1);
                textBoxStartDate.Text = startDate.ToString("yyyy-MM-dd");
                textBoxEndDate.Text = endDate.ToString("yyyy-MM-dd");

                textBoxQuantity.Text = ((endDate.Month - startDate.Month) + 12 * (endDate.Year - startDate.Year)).ToString();
            }
        }
    }
}