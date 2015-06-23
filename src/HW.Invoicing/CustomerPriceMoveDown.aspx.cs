using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class CustomerPriceMoveDown : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            int customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=customer-prices", customerId));
        }
    }
}