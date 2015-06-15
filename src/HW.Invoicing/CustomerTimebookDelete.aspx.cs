using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class CustomerTimebookDelete : System.Web.UI.Page
    {
        SqlCustomerRepository r = new SqlCustomerRepository();
        int id;
        int customerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            r.DeleteTimebook(id);
            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=timebook", customerId));
        }
    }
}