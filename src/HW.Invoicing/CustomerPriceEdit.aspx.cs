using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class CustomerPriceEdit : System.Web.UI.Page
    {
        SqlCustomerRepository r = new SqlCustomerRepository();
        SqlItemRepository ir = new SqlItemRepository();
        protected int id;
        protected int customerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            if (!IsPostBack)
            {
                dropDownListItems.Items.Clear();
                foreach (var i in ir.FindAll())
                {
                    dropDownListItems.Items.Add(new ListItem(i.Name, i.Id.ToString()));
                }
                var c = r.ReadItem(id);
                if (c != null)
                {
                    dropDownListItems.SelectedValue = c.Item.Id.ToString();
                    textBoxItemPrice.Text = c.Price.ToString();
                    checkBoxReactivate.Checked = !c.Inactive;
                    placeHolderReactivate.Visible = c.Inactive;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var c = new CustomerItem
            {
                Item = new Item { Id = ConvertHelper.ToInt32(dropDownListItems.SelectedValue) },
                Price = ConvertHelper.ToDecimal(textBoxItemPrice.Text),
                Inactive = !checkBoxReactivate.Checked
            };
            r.UpdateItem(c, id);
            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=customer-prices", customerId));
        }
    }
}