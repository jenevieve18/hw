using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class CustomerPriceMoveUp : System.Web.UI.Page
    {
        SqlCustomerRepository r = new SqlCustomerRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            int sortOrder = ConvertHelper.ToInt32(Request.QueryString["SortOrder"]);
            int customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);

            var items = r.lalala(sortOrder, customerId);
            if (items.Count == 2)
            {
                r.Swap(items[0], items[1]);
            }

            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=customer-prices", customerId));
        }
    }
}