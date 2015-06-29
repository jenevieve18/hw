using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class InvoiceEdit : System.Web.UI.Page
    {
        protected Invoice invoice;
        SqlInvoiceRepository r = new SqlInvoiceRepository();
        protected int id;

        protected void Page_Load(object sender, EventArgs e)
        {
			HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            invoice = r.Read(id);
        }
    }
}