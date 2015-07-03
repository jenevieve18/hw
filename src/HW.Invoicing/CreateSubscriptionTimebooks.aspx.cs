using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class CreateSubscriptionTimebooks : System.Web.UI.Page
    {
        protected IList<Customer> customers;
        SqlCustomerRepository r = new SqlCustomerRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            customers = r.FindSubscribed();
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