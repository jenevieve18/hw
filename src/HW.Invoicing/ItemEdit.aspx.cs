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
	public partial class ItemEdit : System.Web.UI.Page
	{
		IItemRepository r;
		
		public ItemEdit() : this(new SqlItemRepository())
		{
		}
		
		public ItemEdit(IItemRepository r)
		{
			this.r = r;
		}
		
		public void Edit(int id)
		{
			if (IsPostBack) {
				var d = new Item {
					Name = textBoxName.Text,
					Description = textBoxDescription.Text,
                    Price = ConvertHelper.ToDecimal(textBoxPrice.Text)
				};
				r.Update(d, id);
				Response.Redirect("items.aspx");
			}
			var i = r.Read(id);
			if (i != null) {
				textBoxName.Text = i.Name;
				textBoxDescription.Text = i.Description;
                textBoxPrice.Text = i.Price.ToString();
			}
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			Edit(ConvertHelper.ToInt32(Request.QueryString["ItemID"]));
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			Edit(ConvertHelper.ToInt32(Request.QueryString["ItemID"]));
		}
	}
}