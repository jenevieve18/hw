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
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            notes = r.FindNotes(id);
            prices = r.FindItems(id);
            contacts = r.FindContacts(id);
            timebooks = r.FindTimebooks(id);
            items = ir.FindAllWithCustomerItems();
            foreach (var i in items)
            {
                var li = new ListItem(i.Name, i.Id.ToString());
                li.Attributes.Add("data-price", i.Price.ToString());
                li.Attributes.Add("data-unit", i.Unit.Name);
                dropDownListTimebookItems.Items.Add(li);
            }
        }
    }
}