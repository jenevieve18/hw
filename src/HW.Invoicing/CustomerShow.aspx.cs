using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class CustomerShow : System.Web.UI.Page
    {
        SqlCustomerRepository r = new SqlCustomerRepository();
        SqlItemRepository ir = new SqlItemRepository();
        protected IList<CustomerNotes> notes;
        protected IList<CustomerItem> prices;
        protected IList<CustomerContact> contacts;
        protected IList<Item> timebookItems;
        protected IList<Item> items;
        protected IList<CustomerTimebook> timebooks;
    	protected int id;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            if (!IsPostBack)
            {
                var c = r.Read(id);
                labelCustomer.Text = c.Name;

                labelCustomerNumber.Text = textBoxCustomerNumber.Text = c.Number;
            }
        }

        protected void buttonSaveTimebook_Click(object sender, EventArgs e)
        {
            var t = new CustomerTimebook
            {
                Date = ConvertHelper.ToDateTime(textBoxTimebookDate.Text),
                Department = textBoxTimebookDepartment.Text,
                Contact = new CustomerContact { Id = ConvertHelper.ToInt32(dropDownListTimebookContacts.SelectedValue) },
                Item = new Item { Id = ConvertHelper.ToInt32(dropDownListTimebookItems.SelectedValue) },
                Quantity = ConvertHelper.ToDecimal(textBoxTimebookQty.Text),
                Price = ConvertHelper.ToDecimal(textBoxTimebookPrice.Text),
                Consultant = textBoxTimebookConsultant.Text,
                Comments = textBoxTimebookComments.Text
            };
            r.SaveTimebook(t, ConvertHelper.ToInt32(Request.QueryString["Id"]));
        }

        protected void buttonSaveNotes_Click(object sender, EventArgs e)
        {
            var t = new CustomerNotes
            {
                Notes = textBoxNotes.Text,
                CreatedBy = new User { Id = ConvertHelper.ToInt32(Session["UserId"]) },
                CreatedAt = DateTime.Now,
            };
            r.SaveNotes(t, ConvertHelper.ToInt32(Request.QueryString["Id"]));
        }

        protected void buttonSaveContact_Click(object sender, EventArgs e)
        {
            var t = new CustomerContact
            {
                Contact = textBoxContact.Text,
                Phone = textBoxContactPhone.Text,
                Mobile = textBoxContactMobile.Text,
                Email = textBoxContactEmail.Text
            };
            r.SaveContact(t, ConvertHelper.ToInt32(Request.QueryString["Id"]));
        }

        protected void buttonSaveItem_Click(object sender, EventArgs e)
        {
            var t = new CustomerItem
            {
                Item = new Item { Id = ConvertHelper.ToInt32(dropDownListItems.SelectedValue) },
                Price = ConvertHelper.ToInt32(textBoxItemPrice.Text)
            };
            r.SaveItem(t, ConvertHelper.ToInt32(Request.QueryString["Id"]));
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            notes = r.FindNotes(id);
            prices = r.FindItems(id);
            contacts = r.FindContacts(id);
            timebooks = r.FindTimebooks(id);
            timebookItems = ir.FindAllWithCustomerItems();
            items = ir.FindAll();

            dropDownListItems.Items.Clear();
            foreach (var i in items)
            {
                dropDownListItems.Items.Add(new ListItem(i.Name, i.Id.ToString()));
            }

            dropDownListTimebookItems.Items.Clear();
            foreach (var i in timebookItems)
            {
                var li = new ListItem(i.Name, i.Id.ToString());
                li.Attributes.Add("data-price", i.Price.ToString());
                li.Attributes.Add("data-unit", i.Unit.Name);
                dropDownListTimebookItems.Items.Add(li);
            }

            dropDownListTimebookContacts.Items.Clear();
            foreach (var c in contacts)
            {
                dropDownListTimebookContacts.Items.Add(new ListItem(c.Contact, c.Id.ToString()));
            }
            
            foreach (var t in new[] { new { id = 1, name = "Primary" }, new { id = 2, name = "Secondary" }, new { id = 3, name = "Other" }}) {
            	var li = new ListItem(t.name, t.id.ToString());
            	li.Attributes.Add("class", "radio-inline");
            	radioButtonListContactType.Items.Add(li);
            }
        }
    }
}