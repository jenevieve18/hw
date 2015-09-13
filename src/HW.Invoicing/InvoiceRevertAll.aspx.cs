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
    public partial class InvoiceRevertAll : System.Web.UI.Page
    {
        protected string message;
        SqlInvoiceRepository r = new SqlInvoiceRepository();
        int companyId;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
        }

        protected void buttonReverAll_Click(object sender, EventArgs e)
        {
            r.RevertAll(companyId, ConvertHelper.ToInt32(textBoxInvoiceNumber.Text));
            Response.Redirect("invoices.aspx");
        }
    }
}