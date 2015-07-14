using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;

namespace HW.Invoicing
{
	public partial class CustomerAdd : System.Web.UI.Page
	{
		SqlCustomerRepository r = new SqlCustomerRepository();
        SqlLanguageRepository lr = new SqlLanguageRepository();
        SqlItemRepository ir = new SqlItemRepository();
        int companyId;
        protected string message;
		
		protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            companyId = ConvertHelper.ToInt32(Session["CompanyId"]);

            if (!IsPostBack)
            {
                dropDownListLanguage.Items.Clear();
                foreach (var l in lr.FindAll())
                {
                    dropDownListLanguage.Items.Add(new ListItem(l.Name, l.Id.ToString()));
                }

                var items = ir.FindByCompany(companyId);

                dropDownListSubscriptionItem.Items.Clear();
                foreach (var i in items)
                {
                    dropDownListSubscriptionItem.Items.Add(new ListItem(i.Name, i.Id.ToString()));
                }
            }
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
            var c = new Customer
            {
                Number = textBoxNumber.Text,
                Name = textBoxName.Text,
                PostalAddress = textBoxPostalAddress.Text,
                InvoiceAddress = textBoxInvoiceAddress.Text,
                PurchaseOrderNumber = textBoxPurchaseOrderNumber.Text,
                YourReferencePerson = textBoxYourReferencePerson.Text,
                OurReferencePerson = textBoxOurReferencePerson.Text,
                Phone = textBoxPhone.Text,
                Email = textBoxEmail.Text,
                Language = new Language { Id = ConvertHelper.ToInt32(dropDownListLanguage.SelectedValue) },
                Company = new Company { Id = ConvertHelper.ToInt32(Session["CompanyId"]) },
                HasSubscription = checkBoxSubscribe.Checked,
                SubscriptionItem = new Item { Id = ConvertHelper.ToInt32(dropDownListSubscriptionItem.SelectedValue) },
                SubscriptionStartDate = ConvertHelper.ToDateTime(textBoxSubscriptionStartDate.Text),
                SubscriptionHasEndDate = checkBoxSubscriptionHasEndDate.Checked,
                SubscriptionEndDate = ConvertHelper.ToDateTime(textBoxSubscriptionEndDate.Text)
            };
            c.Validate();
            if (!c.HasErrors)
            {
                r.Save(c);
                Response.Redirect("customers.aspx");
            }
            else
            {
                message = c.Errors.ToHtmlUl();
            }
		}
	}
}