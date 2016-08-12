using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Services;

namespace HW.ReportGenerator
{
    public partial class Default : System.Web.UI.Page
    {
    	UserService s = new UserService();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Response.Redirect("dashboard.aspx");
            }
        }
        
        public void Login(string name, string password)
        {
        	var u = s.ReadByNameAndPassword(name, password);
        	if (u != null) {
        		
        	} else {
        		Response.Redirect("dashboard.aspx");
        	}
        }
    }
}