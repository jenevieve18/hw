using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Services;

namespace HW.Invoicing
{
	public partial class InvoiceEdit : System.Web.UI.Page
	{
		protected Invoice invoice;
		protected int id;
		protected IList<CustomerTimebook> timebooks = new List<CustomerTimebook>();
		protected Company company;
		protected string message;
		InvoiceService service = new InvoiceService(new SqlCompanyRepository(), new SqlInvoiceRepository(), new SqlCustomerRepository());

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

			id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
			company = service.ReadCompany(ConvertHelper.ToInt32(Session["CompanyId"]));

			invoice = service.ReadInvoice(id);
			if (!IsPostBack) {
				HtmlHelper.RedirectIf(invoice == null, "invoices.aspx");
				
				labelInvoiceNumber.Text = invoice.Number;
				textBoxInvoiceDate.Text = invoice.Date.Value.ToString("yyyy-MM-dd");
				labelMaturityDate.Text = invoice.MaturityDate.Value.ToString("yyyy-MM-dd");
				labelInvoiceCustomerAddress.Text = invoice.Customer.ToString().Replace("\n", "<br>");
				
				labelInvoicePurchaseOrderNumber.Text = invoice.GetContactReferenceNumber();
				labelInvoiceYourReferencePerson.Text = invoice.GetContactName();
				labelInvoiceOurReferencePerson.Text = invoice.OurReferencePerson;
				textBoxInvoiceComments.Text = invoice.Comments;
				panelPurchaseOrderNumber.Visible = invoice.GetContactReferenceNumber() != "";
				
				labelInvoiceYourReferencePerson.Visible = !invoice.Customer.HasSecondaryContact;

                foreach (var c in invoice.Customer.Contacts)
                {
                    var li = new ListItem(c.Name, c.Id.ToString());
                    li.Attributes.Add("data-purchase-order-number", c.PurchaseOrderNumber);
					dropDownListInvoiceYourReferencePerson.Items.Add(li);
				}
                dropDownListInvoiceYourReferencePerson.SelectedValue = invoice.CustomerContact.Id.ToString();

				if (company != null) {
					labelCompanyName.Text = company.Name;
					labelCompanyAddress.Text = company.Address;
					labelCompanyBankAccountNumber.Text = company.BankAccountNumber;
					labelCompanyPhone.Text = company.Phone;
					labelCompanyTIN.Text = company.TIN;
				}
			}
			timebooks = service.FindOpenCustomerTimebooks(invoice.Customer.Id);
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			int selectedCustomerContact;
			if (labelInvoiceYourReferencePerson.Visible) {
				selectedCustomerContact = invoice.CustomerContact.Id;
			} else {
				selectedCustomerContact = ConvertHelper.ToInt32(dropDownListInvoiceYourReferencePerson.SelectedValue);
			}
			
			var newInvoice = new Invoice {
				Date = ConvertHelper.ToDateTime(textBoxInvoiceDate.Text),
				MaturityDate = ConvertHelper.ToDateTime(labelMaturityDate.Text),
				CustomerContact = new CustomerContact { Id = selectedCustomerContact },
				Comments = textBoxInvoiceComments.Text
			};
			newInvoice.AddTimebook(Request.Form.GetValues("invoice-timebooks"), Request.Form.GetValues("invoice-timebooks-sortorder"));
			newInvoice.Validate();
			if (newInvoice.HasErrors) {
				message = invoice.Errors.ToHtmlUl();
			} else {
				service.UpdateInvoice(newInvoice, id);
				Response.Redirect("invoices.aspx");
			}
		}
	}
}