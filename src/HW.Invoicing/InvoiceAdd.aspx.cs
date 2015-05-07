using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class InvoiceAdd : System.Web.UI.Page
    {
    	IInvoiceRepository r;
    	
    	public InvoiceAdd() : this(new SqlInvoiceRepository())
    	{
    	}
    	
    	public InvoiceAdd(IInvoiceRepository r)
    	{
    		this.r = r;
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        
        public void Add()
        {
        	var i = new Invoice {
        		Date = DateTime.Now
        	};
        	r.Save(i);
        	Response.Redirect("invoices.aspx");
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	Add();
        }
    }
}