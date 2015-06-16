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
	public partial class CustomerAdd : System.Web.UI.Page
	{
		SqlCustomerRepository r = new SqlCustomerRepository();
		
		public CustomerAdd()
		{
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
        	//HtmlHelper.RedirectIf(Session["UserId"] == null, "default.aspx");
        	
			Add();
		}
		
		public void Add()
		{
			if (IsPostBack) {
				var c = new Customer {
                    Number = textBoxNumber.Text,
					Name = textBoxName.Text,
                    PostalAddress = textBoxPostalAddress.Text,
                    InvoiceAddress = textBoxInvoiceAddress.Text,
                    PurchaseOrderNumber = textBoxPurchaseOrderNumber.Text,
                    YourReferencePerson = textBoxYourReferencePerson.Text,
                    OurReferencePerson = textBoxOurReferencePerson.Text
				};
				r.Save(c);
				Response.Redirect("customers.aspx");
			}
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			Add();
		}
	}
}