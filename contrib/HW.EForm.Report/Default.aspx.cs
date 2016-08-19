using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Services;

namespace HW.EForm.Report
{
    public partial class Default : System.Web.UI.Page
    {
    	UserService s = new UserService();
    	protected string errorMessage = "";
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
            	Login(textBoxEmail.Text, textBoxPassword.Text);
            }
        }
        
        public void Login(string name, string password)
        {
        	var m = s.ReadByEmailAndPassword(name, password);
        	if (m != null) {
        		Session["Email"] = m.Email;
        		Session["Password"] = m.Password;
        		
        		Response.Redirect("dashboard.aspx");
        	} else {
        		errorMessage = "Invalid user name or password. Please try again.";
        	}
        }
    }
}