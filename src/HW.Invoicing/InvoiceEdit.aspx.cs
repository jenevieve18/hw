using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
	public partial class InvoiceEdit : System.Web.UI.Page
	{
		protected Invoice invoice;
		SqlInvoiceRepository r = new SqlInvoiceRepository();
		SqlCompanyRepository cr = new SqlCompanyRepository();
		SqlCustomerRepository ur = new SqlCustomerRepository();
		protected int id;
		protected IList<CustomerTimebook> timebooks = new List<CustomerTimebook>();
		protected Company company;
		protected string message;

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

			id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
			company = cr.Read(ConvertHelper.ToInt32(Session["CompanyId"]));

			invoice = r.Read(id);
			if (!IsPostBack) {
				if (invoice != null) {
					labelInvoiceNumber.Text = invoice.Number;
					textBoxInvoiceDate.Text = invoice.Date.Value.ToString("yyyy-MM-dd");
					labelMaturityDate.Text = invoice.MaturityDate.Value.ToString("yyyy-MM-dd");
					labelInvoiceCustomerAddress.Text = invoice.Customer.ToString().Replace("\n", "<br>");
//					labelInvoicePurchaseOrderNumber.Text = invoice.Customer.GetPrimaryContactReferenceNumber();
//                    labelInvoiceYourReferencePerson.Text = invoice.Customer.GetPrimaryContactName();
//					labelInvoiceOurReferencePerson.Text = invoice.Customer.OurReferencePerson;
labelInvoicePurchaseOrderNumber.Text = invoice.GetContactReferenceNumber();
                    labelInvoiceYourReferencePerson.Text = invoice.GetContactName();
					labelInvoiceOurReferencePerson.Text = invoice.Customer.OurReferencePerson;
					textBoxInvoiceComments.Text = invoice.Comments;
					panelPurchaseOrderNumber.Visible = invoice.Customer.GetPrimaryContactReferenceNumber() != "";
				} else {
					Response.Redirect("invoices.aspx");
				}

				if (company != null) {
					labelCompanyName.Text = company.Name;
					labelCompanyAddress.Text = company.Address;
					labelCompanyBankAccountNumber.Text = company.BankAccountNumber;
					labelCompanyPhone.Text = company.Phone;
					labelCompanyTIN.Text = company.TIN;
				}
			}
			timebooks = ur.FindOpenTimebooks(invoice.Customer.Id);

			//if (invoice != null) {
			//    labelInvoiceNumber.Text = invoice.Number;
			//    textBoxInvoiceDate.Text = invoice.Date.Value.ToString("yyyy-MM-dd");
			//    labelMaturityDate.Text = invoice.MaturityDate.Value.ToString("yyyy-MM-dd");
			//    labelInvoiceCustomerAddress.Text = invoice.Customer.ToString().Replace("\n", "<br>");
			//    labelInvoicePurchaseOrderNumber.Text = invoice.Customer.PurchaseOrderNumber;
			//    labelInvoiceYourReferencePerson.Text = invoice.Customer.YourReferencePerson;
			//    labelInvoiceOurReferencePerson.Text = invoice.Customer.OurReferencePerson;
			//    textBoxInvoiceComments.Text = invoice.Comments;
			//} else {
			//    Response.Redirect("invoices.aspx");
			//}

			//timebooks = ur.FindOpenTimebooks(invoice.Customer.Id);
			//if (company != null) {
			//    labelCompanyName.Text = company.Name;
			//    labelCompanyAddress.Text = company.Address;
			//    labelCompanyBankAccountNumber.Text = company.BankAccountNumber;
			//    labelCompanyPhone.Text = company.Phone;
			//    labelCompanyTIN.Text = company.TIN;
			//}
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			var i = new Invoice
			{
				Date = ConvertHelper.ToDateTime(textBoxInvoiceDate.Text),
				MaturityDate = ConvertHelper.ToDateTime(labelMaturityDate.Text),
				Comments = textBoxInvoiceComments.Text
			};
			i.AddTimebook(Request.Form.GetValues("invoice-timebooks"), Request.Form.GetValues("invoice-timebooks-sortorder"));
			i.Validate();
			if (i.HasErrors)
			{
				message = i.Errors.ToHtmlUl();
			}
			else
			{
				r.Update(i, id);
				Response.Redirect("invoices.aspx");
			}
		}
	}
}