using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class Invoices : System.Web.UI.Page
    {
    	SqlInvoiceRepository r = new SqlInvoiceRepository();
    	protected IList<Invoice> invoices;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	invoices = r.FindAll();
        }
    }
}