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
    public partial class InvoiceRevert : System.Web.UI.Page
    {
        SqlInvoiceRepository r = new SqlInvoiceRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            var i = r.Read(id);
            r.Revert(i);
            Response.Redirect("invoices.aspx");
        }
    }
}