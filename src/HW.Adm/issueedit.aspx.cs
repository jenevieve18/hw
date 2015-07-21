using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Models;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;

namespace HW.Adm
{
    public partial class issueedit : System.Web.UI.Page
    {
        SqlIssueRepository r = new SqlIssueRepository();
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            id = ConvertHelper.ToInt32(Request.QueryString["IssueID"]);
            if (!IsPostBack)
            {
                Issue i = r.Read(id);
                
                dropDownListStatus.Items.Clear();
                foreach (var s in Issue.GetStatuses())
                {
                    dropDownListStatus.Items.Add(new ListItem(s.Name, s.Id.ToString()));
                }
                if (i != null)
                {
                    textBoxTitle.Text = i.Title;
                    textBoxDescrption.Text = i.Description;
                    dropDownListStatus.SelectedValue = i.Status.ToString();
                }
            }
        }

        protected void buttonSaveClick(object sender, EventArgs e)
        {
            var i = new Issue
            {
                Title = textBoxTitle.Text,
                Description = textBoxDescrption.Text,
                Status = ConvertHelper.ToInt32(dropDownListStatus.SelectedValue)
            };
            r.Update(i, id);
            Response.Redirect("issue.aspx");
        }
    }
}