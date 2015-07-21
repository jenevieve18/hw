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
                var i = r.Read(id);
                if (i != null)
                {
                    textBoxTitle.Text = i.Title;
                    textBoxDescrption.Text = i.Description;
                }
            }
        }

        protected void buttonSaveClick(object sender, EventArgs e)
        {
            var i = new Issue
            {
                Title = textBoxTitle.Text,
                Description = textBoxDescrption.Text
            };
            r.Update(i, id);
            Response.Redirect("issue.aspx");
        }
    }
}