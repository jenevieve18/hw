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
    public partial class ItemAdd : System.Web.UI.Page
    {
    	SqlItemRepository ir = new SqlItemRepository();
    	SqlUnitRepository ur = new SqlUnitRepository();
        protected string message;
    	
    	public ItemAdd()
    	{
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));
        }
        
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			dropDownListUnits.Items.Clear();
			foreach (var u in ur.FindAll()) {
				var li = new ListItem(u.Name, u.Id.ToString());
                dropDownListUnits.Items.Add(li);
			}
		}

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var i = new Item
            {
                Name = textBoxName.Text,
                Description = textBoxDescription.Text,
                Price = ConvertHelper.ToDecimal(textBoxPrice.Text),
                Unit = new m.Unit { Id = ConvertHelper.ToInt32(dropDownListUnits.SelectedValue) }
            };
            i.Validate();
            if (!i.HasErrors)
            {
                ir.Save(i);
                Response.Redirect("items.aspx");
            }
            else
            {
                message = i.Errors.ToHtmlUl();
            }
        }
    }
}