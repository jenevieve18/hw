using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class Invoices : System.Web.UI.Page
    {
    	IInvoiceRepository r;
    	protected IList<Invoice> invoices;
    	
    	public Invoices() : this(new SqlInvoiceRepository())
    	{
    	}
    	
    	public Invoices(IInvoiceRepository r)
    	{
    		this.r = r;
    	}
    	
    	public void Index()
    	{
    		invoices = r.FindAll();
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
			HtmlHelper.RedirectIf(Session["UserId"] == null, "default.aspx");
			
        	Index();
        }
    }
}