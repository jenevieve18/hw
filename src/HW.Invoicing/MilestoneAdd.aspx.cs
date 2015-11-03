using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class MilestoneAdd : System.Web.UI.Page
    {
    	SqlMilestoneRepository r = new SqlMilestoneRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	var m = new Milestone {
        		Name = textBoxName.Text
        	};
        	r.Save(m);
        	Response.Redirect("milestones.aspx");
        }
    }
}