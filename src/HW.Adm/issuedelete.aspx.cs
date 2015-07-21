using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Repositories.Sql;
using HW.Core.Helpers;

namespace HW.Adm
{
    public partial class issuedelete : System.Web.UI.Page
    {
        SqlIssueRepository r = new SqlIssueRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = ConvertHelper.ToInt32(Request.QueryString["IssueID"]);
            r.Delete(id);
            Response.Redirect("issue.aspx");
        }
    }
}