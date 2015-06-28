using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class CustomerTimebookEdit : System.Web.UI.Page
    {
        SqlCustomerRepository r = new SqlCustomerRepository();
        SqlItemRepository ir = new SqlItemRepository();
        protected int customerId;
        protected int id;
        protected string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            if (!IsPostBack)
            {
                dropDownListTimebookItems.Items.Clear();
                foreach (var i in ir.FindAllWithCustomerItems(customerId))
                {
                    var li = new ListItem(i.Name, i.Id.ToString());
                    li.Attributes.Add("data-price", i.Price.ToString());
                    li.Attributes.Add("data-unit", i.Unit.Name);
                    dropDownListTimebookItems.Items.Add(li);
                }

                dropDownListTimebookContacts.Items.Clear();
                foreach (var c in r.FindContacts(customerId))
                {
                    dropDownListTimebookContacts.Items.Add(new ListItem(c.Contact, c.Id.ToString()));
                }

                var t = r.ReadTimebook(id);
                if (t != null)
                {
                    textBoxTimebookDate.Text = t.Date.Value.ToString("yyyy-MM-dd");
                    textBoxTimebookDepartment.Text = t.Department;
                    dropDownListTimebookContacts.SelectedValue = t.Contact.Id.ToString();
                    dropDownListTimebookItems.SelectedValue = t.Item.Id.ToString();
                    textBoxTimebookQty.Text = t.Quantity.ToString();
                    textBoxTimebookPrice.Text = t.Price.ToString();
                    textBoxTimebookVAT.Text = t.VAT.ToString();
                    textBoxTimebookConsultant.Text = t.Consultant;
                    textBoxTimebookComments.Text = t.Comments;
                    textBoxTimebookInternalComments.Text = t.InternalComments;
                    checkBoxReactivate.Checked = !t.Inactive;
                    placeHolderReactivate.Visible = t.Inactive;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var t = new CustomerTimebook {
                Date = ConvertHelper.ToDateTime(textBoxTimebookDate.Text),
                Department = textBoxTimebookDepartment.Text,
                Contact = new CustomerContact { Id = ConvertHelper.ToInt32(dropDownListTimebookContacts.SelectedValue) },
                Item = new Item { Id = ConvertHelper.ToInt32(dropDownListTimebookItems.SelectedValue) },
                Quantity = ConvertHelper.ToDecimal(textBoxTimebookQty.Text),
                Price = ConvertHelper.ToDecimal(textBoxTimebookPrice.Text),
                VAT = ConvertHelper.ToDecimal(textBoxTimebookVAT.Text),
                Consultant = textBoxTimebookConsultant.Text,
                Comments = textBoxTimebookComments.Text,
                InternalComments = textBoxTimebookInternalComments.Text,
                Inactive = !checkBoxReactivate.Checked
            };
            t.Validate();
            if (!t.HasErrors)
            {
                r.UpdateTimebook(t, id);
                Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=timebook", customerId));
            }
            else
            {
                message = t.Errors.ToHtmlUl();
            }
        }
    }
}