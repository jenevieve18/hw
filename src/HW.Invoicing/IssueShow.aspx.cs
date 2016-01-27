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
	public partial class IssueShow : System.Web.UI.Page
	{
		SqlIssueRepository r = new SqlIssueRepository();
		protected Issue issue;
		protected string message;
		protected int id;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["CompanyId"] == null, "issues.aspx");
			
			id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
			issue = r.Read(id);
			HtmlHelper.RedirectIf(issue == null, "issues.aspx");
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			var d = new IssueComment
			{
				Comments = textBoxComments.Text
			};
			d.Validate();
			if (!d.HasErrors)
			{
				r.SaveComment(d, id, ConvertHelper.ToInt32(Session["UserId"]));
				Response.Redirect("issueshow.aspx?Id=" + id);
			}
			else
			{
				message = d.Errors.ToHtmlUl();
			}
		}
	}
}