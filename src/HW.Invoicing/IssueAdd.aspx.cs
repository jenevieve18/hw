using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class IssueAdd : System.Web.UI.Page
    {
    	SqlIssueRepository r = new SqlIssueRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        }

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			if (IsPostBack) {
				var i = new Issue {
					Title = textBoxTitle.Text,
					Description = textBoxDescription.Text
				};
				r.Save(i);
				Response.Redirect("issues.aspx");
			}
		}
    }
}