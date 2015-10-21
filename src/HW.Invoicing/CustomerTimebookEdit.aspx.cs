using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using System.Globalization;

namespace HW.Invoicing
{
    public partial class CustomerTimebookEdit : System.Web.UI.Page
    {
        SqlCustomerRepository r = new SqlCustomerRepository();
        SqlItemRepository ir = new SqlItemRepository();
        protected int customerId;
        protected int id;
        protected string message;
        IList<Item> items;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
            items = ir.FindAllWithCustomerItems(companyId, customerId);
            
            if (!IsPostBack)
            {
                var t = r.ReadTimebook(id);
                if (t != null)
                {
                    dropDownListTimebookItems.Items.Clear();
                    foreach (var i in items)
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

                    panelSubscriptionTimebook.Visible = t.IsSubscription;
                    panelTimebook.Visible = !panelSubscriptionTimebook.Visible;
                    if (t.IsSubscription)
                    {
                        textBoxSubscriptionTimebookStartDate.Text = t.SubscriptionStartDate.Value.ToString("yyyy-MM-dd");
                        textBoxSubscriptionTimebookEndDate.Text = t.SubscriptionEndDate.Value.ToString("yyyy-MM-dd");
                        textBoxSubscriptionTimebookQuantity.Text = t.Quantity.ToString();
                        textBoxSubscriptionTimebookComments.Text = t.Comments;
                    }
                    else
                    {
                        textBoxTimebookDate.Text = t.Date.Value.ToString("yyyy-MM-dd");
                        checkBoxTimebookDateHidden.Checked = t.DateHidden;
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
                else
                {
                    Response.Redirect("customers.aspx");
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            decimal quantity = panelSubscriptionTimebook.Visible
                ? ConvertHelper.ToDecimal(textBoxSubscriptionTimebookQuantity.Text)
                : ConvertHelper.ToDecimal(textBoxTimebookQty.Text);
            string comments = panelSubscriptionTimebook.Visible
                ? textBoxSubscriptionTimebookComments.Text
                : textBoxTimebookComments.Text;

            var t = new CustomerTimebook {
                Date = ConvertHelper.ToDateTime(textBoxTimebookDate.Text),
                DateHidden = checkBoxTimebookDateHidden.Checked,
                Department = textBoxTimebookDepartment.Text,
                Contact = new CustomerContact {
                    Id = ConvertHelper.ToInt32(dropDownListTimebookContacts.SelectedValue)
                },
                Item = new Item { Id = ConvertHelper.ToInt32(dropDownListTimebookItems.SelectedValue) },
                Quantity = quantity,
                Price = ConvertHelper.ToDecimal(textBoxTimebookPrice.Text),
                VAT = ConvertHelper.ToDecimal(textBoxTimebookVAT.Text),
                Consultant = textBoxTimebookConsultant.Text,
                Comments = comments,
                InternalComments = textBoxTimebookInternalComments.Text,
                Inactive = !checkBoxReactivate.Checked,
                
                SubscriptionStartDate = ConvertHelper.ToDateTime(textBoxSubscriptionTimebookStartDate.Text),
                SubscriptionEndDate = ConvertHelper.ToDateTime(textBoxSubscriptionTimebookEndDate.Text),
            };
            if (panelSubscriptionTimebook.Visible)
            {
                t.ValidateSubscription();
            }
            else
            {
                t.Validate();
            }
            if (!t.HasErrors)
            {
                if (panelSubscriptionTimebook.Visible)
                {
                    r.UpdateSubscriptionTimebook(t, id);
                }
                else
                {
                    r.UpdateTimebook(t, id);
                }
                Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=timebook", customerId));
            }
            else
            {
                message = t.Errors.ToHtmlUl();
                var selectedValue = dropDownListTimebookItems.SelectedValue;
                // HACK: This is for javascript when reflecting changes of the price and unit textboxes.
                dropDownListTimebookItems.Items.Clear();
                foreach (var i in items)
                {
                    var li = new ListItem(i.Name, i.Id.ToString());
                    li.Attributes.Add("data-price", i.Price.ToString());
                    li.Attributes.Add("data-unit", i.Unit.Name);
                    dropDownListTimebookItems.Items.Add(li);
                }
                dropDownListTimebookItems.SelectedValue = selectedValue;
            }
        }
    }
}