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
		SqlCustomerRepository customerRepository = new SqlCustomerRepository();
		SqlItemRepository itemRepository = new SqlItemRepository();
		SqlCompanyRepository companyRepository = new SqlCompanyRepository();
		int companyId;
		protected string message;
		protected Company company;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

			companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
			company = companyRepository.Read(companyId);

			if (!IsPostBack) {
				dropDownListLanguage.Items.Clear();
				foreach (var l in Language.GetLanguages()) {
					dropDownListLanguage.Items.Add(new ListItem(l.Name, l.Id.ToString()));
				}
				dropDownListCurrency.Items.Clear();
				foreach (var c in Currency.GetCurrencies()) {
					dropDownListCurrency.Items.Add(new ListItem(c.ToString(), c.Id.ToString()));
				}

				var items = itemRepository.FindByCompany(companyId);

				dropDownListSubscriptionItem.Items.Clear();
				foreach (var i in items) {
					dropDownListSubscriptionItem.Items.Add(new ListItem(i.Name, i.Id.ToString()));
				}
			}
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			var customer = new Customer {
				Number = textBoxNumber.Text,
				Name = textBoxName.Text,
				PostalAddress = textBoxPostalAddress.Text,
				InvoiceAddress = textBoxInvoiceAddress.Text,
				InvoiceEmail = textBoxInvoiceEmail.Text,
				InvoiceEmailCC = textBoxInvoiceEmailCC.Text,
				OurReferencePerson = textBoxOurReferencePerson.Text,
				Phone = textBoxPhone.Text,
				Email = textBoxEmail.Text,
				Language = new Language { Id = ConvertHelper.ToInt32(dropDownListLanguage.SelectedValue) },
				Currency = new Currency { Id = ConvertHelper.ToInt32(dropDownListCurrency.SelectedValue) },
				Company = new Company { Id = ConvertHelper.ToInt32(Session["CompanyId"]) },
				HasSubscription = checkBoxSubscribe.Checked,
				SubscriptionItem = new Item { Id = ConvertHelper.ToInt32(dropDownListSubscriptionItem.SelectedValue) },
				SubscriptionStartDate = ConvertHelper.ToDateTime(textBoxSubscriptionStartDate.Text),
				SubscriptionHasEndDate = checkBoxSubscriptionHasEndDate.Checked,
				SubscriptionEndDate = ConvertHelper.ToDateTime(textBoxSubscriptionEndDate.Text)
			};
			customer.Validate();
			if (!customer.HasErrors) {
				customerRepository.Save(customer);
				Response.Redirect("customers.aspx");
			} else {
				message = customer.Errors.ToHtmlUl();
			}
		}
	}
}