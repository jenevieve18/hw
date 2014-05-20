using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class Default : System.Web.UI.Page
    {
    	HWService.ServiceSoap service = new HWService.ServiceSoapClient();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	if (IsPostBack) {
        		var u = service.UserLogin("Usr514", "Pa514", 1);
        		Response.Redirect("Dashboard.aspx");
        	}
        }
    }
}