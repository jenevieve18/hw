using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Services;

namespace HW.Invoicing
{
	public partial class CustomerContactEdit : System.Web.UI.Page
	{
//		SqlCustomerRepository r = new SqlCustomerRepository();
		CustomerService service = new CustomerService(new SqlCustomerRepository(), new SqlItemRepository());
		protected int id;
		protected int customerId;
		protected string message;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

			id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
			customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
			if (!IsPostBack)
			{
//				var customer = r.Read(customerId);
				var customer = service.ReadCustomer(customerId);
				HtmlHelper.RedirectIf(customer == null, "customers.aspx");

//				customer.Contacts = r.FindContacts(customerId);

				radioButtonListContactType.Items.Clear();
				foreach (var t in new[] { new { id = 1, name = "Primary" }, new { id = 2, name = "Secondary" }, new { id = 3, name = "Other" } })
				{
//					if ((t.id == 1 && customer.HasPrimaryContacts && customer.FirstPrimaryContact.Id != id) || (t.id == 2 && customer.HasSecondaryContacts && customer.SecondaryContact.Id != id))
					if ((t.id == 1 && customer.HasPrimaryContact && customer.PrimaryContact.Id != id) || (t.id == 2 && customer.HasSecondaryContact && customer.SecondaryContact.Id != id))
					{
						continue;
					}
					var li = new ListItem(t.name, t.id.ToString());
					li.Attributes.Add("class", "radio-inline");
					radioButtonListContactType.Items.Add(li);
				}
				
//				var c = r.ReadContact(id);
				var c = customer.GetCustomerContact(id);
				if (c != null)
				{
					textBoxContact.Text = c.Name;
					textBoxContactPurchaseOrderNumber.Text = c.PurchaseOrderNumber;
					textBoxContactTitle.Text = c.Title;
					textBoxPhone.Text = c.Phone;
					textBoxMobile.Text = c.Mobile;
					textBoxEmail.Text = c.Email;
					radioButtonListContactType.SelectedValue = c.Type.ToString();
					checkBoxReactivate.Checked = !c.Inactive;
					placeHolderReactivate.Visible = c.Inactive;
				}
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			var c = new CustomerContact {
				Name = textBoxContact.Text,
				PurchaseOrderNumber = textBoxContactPurchaseOrderNumber.Text,
				Title = textBoxContactTitle.Text,
				Phone = textBoxPhone.Text,
				Mobile = textBoxMobile.Text,
				Email = textBoxEmail.Text,
				Inactive = !checkBoxReactivate.Checked,
				Type = ConvertHelper.ToInt32(radioButtonListContactType.SelectedValue)
			};
			c.Validate();
			if (!c.HasErrors)
			{
//				r.UpdateContact(c, id);
				service.UpdateCustomerContact(c, id);
				Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=contact-persons", customerId));
			}
			else
			{
				message = c.Errors.ToHtmlUl();
			}
		}
	}
}