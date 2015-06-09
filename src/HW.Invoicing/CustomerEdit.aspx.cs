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
		protected IList<Item> items;
		protected IList<CustomerTimebook> timebooks;
		protected int id;
		
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
			HtmlHelper.RedirectIf(Session["UserId"] == null, "default.aspx");
			
			id = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
			if (!IsPostBack) {
				var c = r.Read(id);
				if (c != null) {
                    labelCustomer.Text = c.Name;
					textBoxNumber.Text = c.Number;
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

//			int customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerID"]);
			notes = r.FindNotes(id);
			prices = r.FindPrices(id);
			contacts = r.FindContacts(id);
			timebooks = r.FindTimebooks(id);
			items = ir.FindAllWithCustomerPrices();

			DropDownListContacts.Items.Clear();
			foreach (var c in contacts) {
				DropDownListContacts.Items.Add(new ListItem(c.Contact, c.Id.ToString()));
			}
			DropDownListTimebookItems.Items.Clear();
			foreach (var i in items) {
				var li = new ListItem(i.Name, i.Id.ToString());
				li.Attributes.Add("data-price", i.Price.ToString());
				DropDownListTimebookItems.Items.Add(li);
			}
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			var d = new Customer {
				Number = textBoxNumber.Text,
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
				Phone = textBoxContactPhone.Text,
				Mobile = textBoxContactMobile.Text,
				Email = textBoxContactEmail.Text
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
			var p = new CustomerTimebook {
				Contact = new CustomerContact { Id = ConvertHelper.ToInt32(DropDownListContacts.SelectedValue) },
				Item = new Item { Id = ConvertHelper.ToInt32(DropDownListItems.SelectedValue) },
				Quantity = ConvertHelper.ToDecimal(textBoxTime.Text),
				Price = ConvertHelper.ToDecimal(textBoxTimebookPrice.Text),
				Consultant = textBoxConsultant.Text,
				Comments = textBoxComments.Text
			};
			r.SaveTimebook(p, ConvertHelper.ToInt32(Request.QueryString["CustomerID"]));
		}
	}
}