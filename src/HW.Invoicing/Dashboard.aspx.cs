using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class Dashboard : System.Web.UI.Page
    {
    	IUserRepository r;
    	
    	public Dashboard() : this(new SqlUserRepository())
    	{
    	}
    	
    	public Dashboard(IUserRepository r)
    	{
    		this.r = r;
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");
        }
    }
}