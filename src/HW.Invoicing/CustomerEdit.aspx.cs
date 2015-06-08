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
	public partial class CustomerEdit : System.Web.UI.Page
	{
		SqlCustomerRepository r = new SqlCustomerRepository();
		SqlItemRepository ir = new SqlItemRepository();
		protected IList<CustomerNotes> notes;
		protected IList<CustomerPrice> prices;
		protected IList<CustomerContact> contacts;
		
		public CustomerEdit()
		{
		}
		
		public void Edit(int id)
		{
			var c = r.Read(id);
			if (c != null) {
				textBoxName.Text = c.Name;
			}
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerID"]);
			if (!IsPostBack) {
				var c = r.Read(customerId);
				if (c != null) {
					textBoxName.Text = c.Name;
					textBoxAddress.Text = c.Address;
					textBoxEmail.Text = c.Email;
					textBoxPhone.Text = c.Phone;
				}
				foreach (var i in ir.FindAll()) {
					DropDownListItems.Items.Add(new ListItem(i.Name, i.Id.ToString()));
				}
			}
		}

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            int customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerID"]);
            notes = r.FindNotes(customerId);
            prices = r.FindPrices(customerId);
            contacts = r.FindContacts(customerId);
        }

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			var d = new Customer {
				Name = textBoxName.Text,
				Phone = textBoxPhone.Text,
				Email = textBoxEmail.Text
			};
            r.Update(d, ConvertHelper.ToInt32(Request.QueryString["CustomerID"]));
			Response.Redirect("customers.aspx");
		}

		protected void buttonSaveNotes_Click(object sender, EventArgs e)
		{
			var n = new CustomerNotes {
				Notes = textBoxNotes.Text,
				CreatedAt = DateTime.Now,
				CreatedBy = new User { Id = ConvertHelper.ToInt32(Session["UserId"]) }
			};
            r.SaveNotes(n, ConvertHelper.ToInt32(Request.QueryString["CustomerID"]));
		}

		protected void buttonSaveContact_Click(object sender, EventArgs e)
		{
			var c = new CustomerContact {
				Contact = textBoxContact.Text,
				Phone = textBoxPhone.Text,
				Mobile = textBoxMobile.Text,
				Email = textBoxEmail.Text
			};
            r.SaveContact(c, ConvertHelper.ToInt32(Request.QueryString["CustomerID"]));
		}

		protected void buttonSavePrice_Click(object sender, EventArgs e)
		{
			var p = new CustomerPrice {
				Item = new Item { Id = ConvertHelper.ToInt32(DropDownListItems.SelectedValue) },
				Price = ConvertHelper.ToDecimal(textBoxPrice.Text)
			};
            r.SavePrice(p, ConvertHelper.ToInt32(Request.QueryString["CustomerID"]));
		}

		protected void buttonSaveTimebook_Click(object sender, EventArgs e)
		{
			var p = new CustomerPrice {
				Item = new Item { Id = ConvertHelper.ToInt32(DropDownListItems.SelectedValue) },
				Price = ConvertHelper.ToDecimal(textBoxPrice.Text)
			};
            r.SavePrice(p, ConvertHelper.ToInt32(Request.QueryString["CustomerID"]));
		}
	}
}