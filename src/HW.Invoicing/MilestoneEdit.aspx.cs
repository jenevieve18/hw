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
    public partial class MilestoneEdit : System.Web.UI.Page
    {
    	SqlMilestoneRepository r = new SqlMilestoneRepository();
    	int id;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

        	id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            if (!IsPostBack)
            {
                var m = r.Read(id);
                if (m != null)
                {
                    textBoxName.Text = m.Name;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	var m = new Milestone {
                Name = textBoxName.Text
        	};
        	r.Update(m, id);
        	Response.Redirect("milestones.aspx");
        }
    }
}