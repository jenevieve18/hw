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
    public partial class UserDelete : System.Web.UI.Page
    {
    	IUserRepository r;
    	
    	public UserDelete() : this(new SqlUserRepository())
    	{
    	}
    	
    	public UserDelete(IUserRepository r)
    	{
    		this.r = r;
    	}
    	
    	public void Delete(int id)
    	{
    		r.Delete(id);
    		Response.Redirect("users.aspx");
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	Delete(ConvertHelper.ToInt32(Request.QueryString["UserID"]));
        }
    }
}