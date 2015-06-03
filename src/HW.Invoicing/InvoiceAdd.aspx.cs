using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class InvoiceAdd : System.Web.UI.Page
    {
    	IInvoiceRepository ir;
    	ICustomerRepository cr;
        protected IList<Item> items;
        IItemRepository jr;
    	
    	public InvoiceAdd() : this(new SqlInvoiceRepository(), new SqlCustomerRepository(), new SqlItemRepository())
    	{
    	}
    	
    	public InvoiceAdd(IInvoiceRepository ir, ICustomerRepository cr, IItemRepository jr)
    	{
    		this.ir = ir;
    		this.cr = cr;
            this.jr = jr;
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	if (!IsPostBack) {
        		foreach (var c in cr.FindAll()) {
        			dropDownListCustomer.Items.Add(new ListItem(c.Name, c.Id.ToString()));
        		}
                items = jr.FindAll();
        	}
        }
        
        public void Add()
        {
            var i = new Invoice
            {
                Date = DateTime.Now,
                Customer = new Customer { Id = ConvertHelper.ToInt32(dropDownListCustomer.SelectedValue) }
            };
        	ir.Save(i);
        	Response.Redirect("invoices.aspx");
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	Add();
        }
    }
}