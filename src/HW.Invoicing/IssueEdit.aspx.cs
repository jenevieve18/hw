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
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                var i = new Issue
                {
                    Title = textBoxTitle.Text,
                    Description = textBoxDescription.Text
                };
                r.Update(i, ConvertHelper.ToInt32(Request.QueryString["Id"]));
                Response.Redirect("issues.aspx");
            }
        }
    }
}