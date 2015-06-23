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
using m = HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
	public partial class ItemEdit : System.Web.UI.Page
	{
		SqlItemRepository r = new SqlItemRepository();
        SqlUnitRepository ur = new SqlUnitRepository();
		
		public void Edit(int id)
		{
			if (IsPostBack) {
                var d = new Item
                {
                    Name = textBoxName.Text,
                    Description = textBoxDescription.Text,
                    Price = ConvertHelper.ToDecimal(textBoxPrice.Text),
                    Unit = new m.Unit { Id = ConvertHelper.ToInt32(dropDownListUnits.SelectedValue) },
                    Inactive = !checkBoxReactivate.Checked
                };
				r.Update(d, id);
				Response.Redirect("items.aspx");
			}
			var i = r.Read(id);
			if (i != null) {
				textBoxName.Text = i.Name;
				textBoxDescription.Text = i.Description;
                textBoxPrice.Text = i.Price.ToString();
                dropDownListUnits.SelectedValue = i.Unit.Id.ToString();
                checkBoxReactivate.Checked = !i.Inactive;
                placeHolderReactivate.Visible = i.Inactive;
			}
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
        	HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");
        	
			Edit(ConvertHelper.ToInt32(Request.QueryString["Id"]));
		}

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            dropDownListUnits.Items.Clear();
            foreach (var u in ur.FindAll())
            {
                var li = new ListItem(u.Name, u.Id.ToString());
                dropDownListUnits.Items.Add(li);
            }
        }

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			Edit(ConvertHelper.ToInt32(Request.QueryString["Id"]));
		}
	}
}