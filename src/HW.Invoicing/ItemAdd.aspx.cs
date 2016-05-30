using System;
using System.Collections.Generic;
using System.Globalization;
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
        int companyId;
    	
    	public ItemAdd()
    	{
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
        }
        
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			dropDownListUnits.Items.Clear();
			foreach (var u in ur.FindByCompany(companyId)) {
				var li = new ListItem(u.Name, u.Id.ToString());
                dropDownListUnits.Items.Add(li);
			}
		}

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var i = new Item
            {
                Name = textBoxName.Text,
                Consultant = textBoxConsultant.Text,
                Description = textBoxDescription.Text,
                //Price = ConvertHelper.ToDecimal(textBoxPrice.Text),
                Price = ConvertHelper.ToDecimal(textBoxPrice.Text, 0, textBoxPrice.Text.IndexOf(",") >= 0 ? new CultureInfo("sv-SE") : new CultureInfo("en-US")),
                Unit = new m.Unit { Id = ConvertHelper.ToInt32(dropDownListUnits.SelectedValue) },
                Company = new Company { Id = ConvertHelper.ToInt32(Session["CompanyId"]) }
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