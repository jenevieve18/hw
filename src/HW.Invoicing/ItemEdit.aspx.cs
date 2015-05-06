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
	public partial class ItemEdit : System.Web.UI.Page
	{
		SqlItemRepository r = new SqlItemRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack) {
				var i = r.Read(ConvertHelper.ToInt32(Request.QueryString["ItemID"]));
				if (i != null) {
					textBoxName.Text = i.Name;
					textBoxDescription.Text = i.Description;
				}
			}
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			var i = new Item {
				Name = textBoxName.Text,
				Description = textBoxDescription.Text
			};
			r.Update(i, ConvertHelper.ToInt32(Request.QueryString["ItemID"]));
			Response.Redirect("items.aspx");
		}
	}
}