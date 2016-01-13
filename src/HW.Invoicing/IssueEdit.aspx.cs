using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class IssueEdit : System.Web.UI.Page
    {
        SqlIssueRepository r = new SqlIssueRepository();
        SqlMilestoneRepository mr = new SqlMilestoneRepository();
        protected Issue issue;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
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
                dropDownListStatus.Items.Clear();
                foreach (var s in Issue.GetStatuses())
                {
                    dropDownListStatus.Items.Add(new ListItem(s.Name, s.Id.ToString()));
                }
                issue = r.Read(id);
                if (issue != null)
                {
                    issue.Comments = r.FindComments(id);

                    textBoxTitle.Text = issue.Title;
                    textBoxDescription.Text = issue.Description;
                    dropDownListMilestone.SelectedValue = issue.Milestone.Id.ToString();
                    dropDownListPriority.SelectedValue = issue.Priority.Id.ToString();
                    dropDownListStatus.SelectedValue = issue.Status.ToString();
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                var i = new Issue
                {
                    Title = textBoxTitle.Text,
                    Description = textBoxDescription.Text,
                    Milestone = new Milestone { Id = ConvertHelper.ToInt32(dropDownListMilestone.SelectedValue) },
                    Priority = new Priority { Id = ConvertHelper.ToInt32(dropDownListPriority.SelectedValue) },
                    Status = ConvertHelper.ToInt32(dropDownListStatus.SelectedValue)
                };
                r.Update(i, ConvertHelper.ToInt32(Request.QueryString["Id"]));
                Response.Redirect("issues.aspx");
            }
        }
    }
}