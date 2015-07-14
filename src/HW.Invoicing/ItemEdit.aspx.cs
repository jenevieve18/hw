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
        protected string message;
		
		protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));
        	
            if (!IsPostBack)
            {
                var i = r.Read(ConvertHelper.ToInt32(Request.QueryString["Id"]));
                if (i != null)
                {
                    textBoxName.Text = i.Name;
                    textBoxDescription.Text = i.Description;
                    textBoxPrice.Text = i.Price.ToString();
                    dropDownListUnits.SelectedValue = i.Unit.Id.ToString();
                    checkBoxReactivate.Checked = !i.Inactive;
                    placeHolderReactivate.Visible = i.Inactive;
                
                    dropDownListUnits.Items.Clear();
                    foreach (var u in ur.FindByCompany(ConvertHelper.ToInt32(Session["CompanyId"])))
                    {
                        var li = new ListItem(u.Name, u.Id.ToString());
                        dropDownListUnits.Items.Add(li);
                    }
                    dropDownListUnits.SelectedValue = i.Unit.Id.ToString();
                }
            }
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
            var d = new Item
            {
                Name = textBoxName.Text,
                Description = textBoxDescription.Text,
                Price = ConvertHelper.ToDecimal(textBoxPrice.Text),
                Unit = new m.Unit { Id = ConvertHelper.ToInt32(dropDownListUnits.SelectedValue) },
                Inactive = !checkBoxReactivate.Checked
            };
            d.Validate();
            if (!d.HasErrors)
            {
                r.Update(d, ConvertHelper.ToInt32(Request.QueryString["Id"]));
                Response.Redirect("items.aspx");
            }
            else
            {
                message = d.Errors.ToHtmlUl();
            }
		}
	}
}