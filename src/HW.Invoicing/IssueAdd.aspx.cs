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
    public partial class IssueAdd : System.Web.UI.Page
    {
    	SqlIssueRepository r = new SqlIssueRepository();
        SqlMilestoneRepository mr = new SqlMilestoneRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));
            if (!IsPostBack)
            {
                dropDownListMilestone.Items.Clear();
                foreach (var m in mr.FindAll())
                {
                    dropDownListMilestone.Items.Add(new ListItem(m.Name, m.Id.ToString()));
                }
                dropDownListPriority.Items.Clear();
                foreach (var p in Priority.GetPriorities())
                {
                    dropDownListPriority.Items.Add(new ListItem(p.Name, p.Id.ToString()));
                }
                dropDownListPriority.SelectedValue = 3.ToString();
            }
        }

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			if (IsPostBack) {
                var i = new Issue
                {
                    Title = textBoxTitle.Text,
                    Description = textBoxDescription.Text,
                    Milestone = new Milestone { Id = ConvertHelper.ToInt32(dropDownListMilestone.SelectedValue) },
                    Priority = new Priority { Id = ConvertHelper.ToInt32(dropDownListPriority.SelectedValue) }
                };
				r.Save(i);
				Response.Redirect("issues.aspx");
			}
		}
    }
}