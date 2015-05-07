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
    public partial class ItemDelete : System.Web.UI.Page
    {
    	IItemRepository r;
    	
    	public ItemDelete() : this(new SqlItemRepository())
    	{
    	}
    	
    	public ItemDelete(IItemRepository r)
    	{
    		this.r = r;
    	}
    	
    	public void Delete(int id)
    	{
    		r.Delete(id);
    		Response.Redirect("items.aspx");
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	Delete(ConvertHelper.ToInt32(Request.QueryString["ItemID"]));
        }
    }
}