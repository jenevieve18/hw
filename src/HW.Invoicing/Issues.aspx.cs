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
    public partial class Issues : System.Web.UI.Page
    {
    	SqlIssueRepository r = new SqlIssueRepository();
    	protected IList<Issue> issues;
    	protected Pager pager;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            int page = ConvertHelper.ToInt32(Request.QueryString["page"], 1);
            int pageSize = 25;
            int offset = (page - 1) * pageSize + 1;
        	issues = r.FindByOffset(offset, pageSize);
        	pager = new Pager(r.CountAllIssues(), page, pageSize);
        }
    }
}