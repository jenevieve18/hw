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
        protected CustomerTimebook timebook;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
            items = ir.FindAllWithCustomerItems(companyId, customerId);
            
            if (!IsPostBack)
            {
                timebook = r.ReadTimebook(id);
                if (timebook != null)
                {
                    dropDownListTimebookItems.Items.Clear();
                    dropDownListSubscriptionTimebookItems.Items.Clear();
                    foreach (var i in items)
                    {
                        var li = new ListItem(i.Name, i.Id.ToString());
                        li.Attributes.Add("data-id", i.Id.ToString());
                        li.Attributes.Add("data-price", i.Price.ToString());
                        li.Attributes.Add("data-unit", i.Unit.Name);
                        dropDownListTimebookItems.Items.Add(li);

                        li = new ListItem(i.Name, i.Id.ToString());
                        li.Attributes.Add("data-id", i.Id.ToString());
                        li.Attributes.Add("data-price", i.Price.ToString());
                        li.Attributes.Add("data-unit", i.Unit.Name);
                        dropDownListSubscriptionTimebookItems.Items.Add(li);
                    }

                    dropDownListTimebookContacts.Items.Clear();
                    foreach (var c in r.FindContacts(customerId))
                    {
                        dropDownListTimebookContacts.Items.Add(new ListItem(c.Name, c.Id.ToString()));
                    }

                    panelSubscriptionTimebook.Visible = timebook.IsSubscription;
                    panelTimebook.Visible = !panelSubscriptionTimebook.Visible;
                    //Panel2.Visible = Panel1.Visible = !timebook.IsHeader;
                    if (timebook.IsSubscription)
                    {
                        textBoxSubscriptionTimebookStartDate.Text = timebook.SubscriptionStartDate.Value.ToString("yyyy-MM-dd");
                        textBoxSubscriptionTimebookEndDate.Text = timebook.SubscriptionEndDate.Value.ToString("yyyy-MM-dd");
                        dropDownListSubscriptionTimebookItems.SelectedValue = timebook.Item.Id.ToString();
                        labelSubscriptionTimebookUnit.Text = timebook.Item.Unit.Name;
                        textBoxSubscriptionTimebookQty.Text = timebook.Quantity.ToString();
                        textBoxSubscriptionTimebookPrice.Text = timebook.Price.ToString();
                        textBoxSubscriptionTimebookComments.Text = timebook.Comments;
                    //}
                    //else if (timebook.IsHeader)
                    //{
                        textBoxTimebookDate.Text = timebook.Date.Value.ToString("yyyy-MM-dd");
                        textBoxTimebookComments.Text = timebook.Comments;
                    }
                    else
                    {
                        textBoxTimebookDate.Text = timebook.Date.Value.ToString("yyyy-MM-dd");
                        checkBoxTimebookDateHidden.Checked = timebook.DateHidden;
                        textBoxTimebookDepartment.Text = timebook.Department;
                        dropDownListTimebookContacts.SelectedValue = timebook.Contact.Id.ToString();
                        dropDownListTimebookItems.SelectedValue = timebook.Item.Id.ToString();
                        labelTimebookUnit.Text = timebook.Item.Unit.Name;
                        textBoxTimebookQty.Text = timebook.Quantity.ToString();
                        textBoxTimebookPrice.Text = timebook.Price.ToString();
                        textBoxTimebookVAT.Text = timebook.VAT.ToString();
                        textBoxTimebookConsultant.Text = timebook.Consultant;
                        textBoxTimebookComments.Text = timebook.Comments;
                        textBoxTimebookInternalComments.Text = timebook.InternalComments;
                    }
                    checkBoxReactivate.Checked = !timebook.Inactive;
                    placeHolderReactivate.Visible = timebook.Inactive;
                    checkBoxTimebookIsHeader.Checked = timebook.IsHeader;
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
                ? ConvertHelper.ToDecimal(textBoxSubscriptionTimebookQty.Text)
                : ConvertHelper.ToDecimal(textBoxTimebookQty.Text);
            string comments = panelSubscriptionTimebook.Visible
                ? textBoxSubscriptionTimebookComments.Text
                : textBoxTimebookComments.Text;
            decimal price = panelSubscriptionTimebook.Visible
                ? ConvertHelper.ToDecimal(textBoxSubscriptionTimebookPrice.Text)
                : ConvertHelper.ToDecimal(textBoxTimebookPrice.Text);

            var t = new CustomerTimebook {
                Date = ConvertHelper.ToDateTime(textBoxTimebookDate.Text),
                DateHidden = checkBoxTimebookDateHidden.Checked,
                Department = textBoxTimebookDepartment.Text,
                Contact = new CustomerContact {
                    Id = ConvertHelper.ToInt32(dropDownListTimebookContacts.SelectedValue)
                },
                Item = new Item { Id = ConvertHelper.ToInt32(dropDownListTimebookItems.SelectedValue) },
                Quantity = quantity,
                //Price = ConvertHelper.ToDecimal(textBoxTimebookPrice.Text),
                Price = price,
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
            else if (Panel2.Visible)
            {
                t.Validate();
            }
            else
            {
                t.ValidateSubscription();
            }
            if (!t.HasErrors)
            {
                if (panelSubscriptionTimebook.Visible)
                {
                    r.UpdateSubscriptionTimebook(t, id);
                }
                //else
                //{
                //    r.UpdateTimebook(t, id);
                //}
                else if (Panel2.Visible)
                {
                    r.UpdateTimebook(t, id);
                }
                else
                {
                    r.UpdateHeaderTimebook(t, id);
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