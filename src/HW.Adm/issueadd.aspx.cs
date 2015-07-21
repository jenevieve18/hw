using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Repositories.Sql;
using HW.Core.Models;

namespace HW.Adm
{
    public partial class issueadd : System.Web.UI.Page
    {
        SqlIssueRepository r = new SqlIssueRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void buttonSaveClick(object sender, EventArgs e)
        {
            var i = new Issue {
                Title = textBoxTitle.Text,
                Description = textBoxDescrption.Text
            };
            r.Save(i);
            Response.Redirect("issue.aspx");
        }
    }
}