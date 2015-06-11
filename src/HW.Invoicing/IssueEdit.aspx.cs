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

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            if (!IsPostBack)
            {
                var i = r.Read(id);
                if (i != null)
                {
                    textBoxTitle.Text = i.Title;
                    textBoxDescription.Text = i.Description;
                }
                dropDownListStatus.Items.Clear();
                foreach (var s in new[] { new { id = 1, name = "Open" }, new { id = 2, name = "Fixed" } })
                {
                    dropDownListStatus.Items.Add(new ListItem(s.name, s.id.ToString()));
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
                    Status = ConvertHelper.ToInt32(dropDownListStatus.SelectedValue)
                };
                r.Update(i, ConvertHelper.ToInt32(Request.QueryString["Id"]));
                Response.Redirect("issues.aspx");
            }
        }
    }
}