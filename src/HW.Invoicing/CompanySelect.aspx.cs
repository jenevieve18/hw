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
    public partial class CompanySelect : System.Web.UI.Page
    {
        SqlCompanyRepository r = new SqlCompanyRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            int userId = ConvertHelper.ToInt32(Session["UserId"]);
            r.UnselectByUser(userId);

            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            r.SelectCompany(id);
            var c = r.Read(id);
            Session["CompanyId"] = c.Id;
            Session["CompanyName"] = c.Name;
            Response.Redirect("dashboard.aspx");
        }
    }
}