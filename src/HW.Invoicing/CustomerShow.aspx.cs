﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;
using System.Web.Services;
using System.Web.Script.Services;
using System.Globalization;

namespace HW.Invoicing
{
	public partial class CustomerShow : System.Web.UI.Page
	{
		SqlCustomerRepository r = new SqlCustomerRepository();
		SqlItemRepository ir = new SqlItemRepository();
		SqlInvoiceRepository vr = new SqlInvoiceRepository();
		SqlCompanyRepository cr = new SqlCompanyRepository();
		protected IList<CustomerNotes> notes;
		protected IList<CustomerItem> prices;
		protected IList<CustomerContact> contacts;
		protected IList<Item> timebookItems;
		protected IList<Item> items;
		protected IList<CustomerTimebook> timebooks;
		protected IList<CustomerAgreement> agreements;
		protected IList<Language> languages;
		protected int id;
		protected string selectedTab;
		protected Customer customer;
		protected int companyId;
		protected Company company;
		protected string message;
		protected Pager pager;

		[WebMethod]
		public static string UpdateTimebookConsultant(string consultant, int id)
		{
			var d = new SqlCustomerRepository();
			d.UpdateTimebookConsultant(consultant, id);
			return consultant;
		}

		[WebMethod]
		public static string UpdateTimebookComments(string comments, int id)
		{
			var d = new SqlCustomerRepository();
			d.UpdateTimebookComments(comments, id);
			return comments;
		}

		[WebMethod(EnableSession = true)]
		//[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
		public static int GetLatestInvoiceNumber(int customerID)
		{
			var ir = new SqlInvoiceRepository();
			var cr = new SqlCustomerRepository();
			
			int companyId = ConvertHelper.ToInt32(HttpContext.Current.Session["CompanyId"]);
			var customer = cr.Read(customerID);
			if (customer.Company.Id != companyId) {
				return -1;
			} else {
				return ir.GetLatestInvoiceNumber(companyId);
			}
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));
			

			id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
			companyId = ConvertHelper.ToInt32(Session["CompanyId"], 1);

			selectedTab = Request.QueryString["SelectedTab"] == null ? "notes" : Request.QueryString["SelectedTab"];

			if (Session["Message"] != null) {
				message = Session["Message"].ToString();
				Session.Remove("Message");
			}

			customer = r.Read(id);

			HtmlHelper.RedirectIf(customer == null, "customers.aspx");
			HtmlHelper.RedirectIf(customer.Company.Id != companyId, "customers.aspx");

			if (!IsPostBack) {
				if (customer == null) {
					Response.Redirect("customers.aspx");
				} else {
//					if (customer.ContactPerson != null && customer.ContactPerson.Id > 0) {
//						customer.ContactPerson = r.ReadContact(customer.ContactPerson.Id);
//					}

					labelCustomer.Text = customer.Name;

					// Customer Info Panel
					labelCustomerName.Text = textBoxCustomerName.Text = customer.Name;
					labelCustomerNumber.Text = textBoxCustomerNumber.Text = customer.Number;

					labelPostalAddress.Text = customer.PostalAddress.Replace("\n", "<br>");
					textBoxPostalAddress.Text = customer.PostalAddress;

					labelInvoiceAddress.Text = customer.InvoiceAddress.Replace("\n", "<br>");
					textBoxInvoiceAddress.Text = customer.InvoiceAddress;
					labelInvoiceEmail.Text = textBoxInvoiceEmail.Text = customer.InvoiceEmail;
					labelInvoiceEmailCC.Text = textBoxInvoiceEmailCC.Text = customer.InvoiceEmailCC;

					//labelYourReferencePerson.Text = customer.ContactPerson != null ? customer.ContactPerson.Name : "";
					labelOurReferencePerson.Text = textBoxOurReferencePerson.Text = customer.OurReferencePerson;
					labelPhone.Text = textBoxPhone.Text = customer.Phone;
					labelEmail.Text = textBoxEmail.Text = customer.Email;
					labelLanguage.Text = customer.Language.Name;
					labelCurrency.Text = customer.Currency.ToString();
					
					labelCustomerNumber.Font.Strikeout = labelInvoiceAddress.Font.Strikeout =
						labelPostalAddress.Font.Strikeout =
						labelOurReferencePerson.Font.Strikeout =
						labelEmail.Font.Strikeout = labelPhone.Font.Strikeout =
						labelLanguage.Font.Strikeout = labelCurrency.Font.Strikeout = customer.IsInactive;
					
					// Timebook Panel
					textBoxTimebookDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
					textBoxTimebookQty.Text = 1.ToString();
					textBoxTimebookVAT.Text = 25.ToString();
					
					// Invoice Panel
					labelInvoiceCustomerNumber.Text = customer.Number;
					textBoxInvoiceDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
					labelMaturityDate.Text = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
					labelInvoiceCustomerAddress.Text = customer.InvoiceAddress.Replace("\n", "<br>");
					labelInvoiceNumber.Text = "IHG-001";
					labelInvoiceOurReferencePerson.Text = customer.OurReferencePerson;
					labelInvoicePurchaseOrderNumber.Text = customer.GetPrimaryContactReferenceNumber();
					labelInvoiceYourReferencePerson.Text = customer.GetPrimaryContactName();
					panelPurchaseOrderNumber.Visible = customer.GetPrimaryContactReferenceNumber() != "";
					
					labelInvoiceYourReferencePerson.Visible = !customer.HasSecondaryContact;
//					dropDownListInvoiceYourReferencePerson.Visible = customer.HasPrimaryContact;
					dropDownListInvoiceYourReferencePerson.Visible = customer.HasSecondaryContact;

					// Subscription Panel
					checkBoxSubscribe.Checked = customer.HasSubscription;
					if (customer.HasSubscription) {
						textBoxSubscriptionStartDate.Text = customer.SubscriptionStartDate.Value.ToString("yyyy-MM-dd");
						checkBoxSubscriptionHasEndDate.Checked = customer.SubscriptionHasEndDate;
						if (customer.SubscriptionHasEndDate) {
							textBoxSubscriptionEndDate.Text = customer.SubscriptionEndDate.Value.ToString("yyyy-MM-dd");
						}
					} else {
						textBoxSubscriptionStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
					}

					// Agreement
					textBoxAgreementDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
					textBoxAgreementPaymentTerms.Text = "30 dagar";
					
					
				}
			}

			company = cr.Read(companyId);
			if (company != null) {
				labelCompanyName.Text = company.Name;
				labelCompanyAddress.Text = company.Address;
				labelCompanyPhone.Text = company.Phone;
				labelCompanyBankAccountNumber.Text = company.BankAccountNumber;
				labelCompanyTIN.Text = company.TIN;
			}
		}

		protected void buttonSaveSubscription_Click(object sender, EventArgs e)
		{
			var c = new Customer {
				HasSubscription = checkBoxSubscribe.Checked,
				SubscriptionItem = new Item { Id = ConvertHelper.ToInt32(dropDownListSubscriptionItem.SelectedValue) },
				SubscriptionStartDate = ConvertHelper.ToDateTime(textBoxSubscriptionStartDate.Text),
				SubscriptionEndDate = ConvertHelper.ToDateTime(textBoxSubscriptionEndDate.Text),
				SubscriptionHasEndDate = checkBoxSubscriptionHasEndDate.Checked
			};
			r.UpdateSubscription(c, id);
			Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=subscription", id));
		}

		protected void buttonDeactivate_Click(object sender, EventArgs e)
		{
			r.Deactivate(ConvertHelper.ToInt32(Request.QueryString["Id"]));
			Response.Redirect(string.Format("customers.aspx"));
		}

		protected void buttonReactivate_Click(object sender, EventArgs e)
		{
			r.Reactivate(id);
			Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=customer-info", id));
		}

		protected void buttonDelete_Click(object sender, EventArgs e)
		{
			r.Delete(id);
			Response.Redirect("customers.aspx");
		}

		protected void buttonUndelete_Click(object sender, EventArgs e)
		{
			r.Undelete(id);
			Response.Redirect("customers.aspx");
		}

		protected void buttonSaveTimebook_Click(object sender, EventArgs e)
		{
			CustomerTimebook t;
			if (!checkBoxTimebookIsHeader.Checked) {
				t = new CustomerTimebook {
					Date = ConvertHelper.ToDateTime(textBoxTimebookDate.Text),
					DateHidden = checkBoxTimebookDateHidden.Checked,
					Department = textBoxTimebookDepartment.Text,
					Contact = new CustomerContact { Id = ConvertHelper.ToInt32(dropDownListTimebookContacts.SelectedValue) },
					Item = new Item { Id = ConvertHelper.ToInt32(dropDownListTimebookItems.SelectedValue) },
					Quantity = ConvertHelper.ToDecimal(textBoxTimebookQty.Text),
					//Price = ConvertHelper.ToDecimal(textBoxTimebookPrice.Text),
					Price = ConvertHelper.ToDecimal(textBoxTimebookPrice.Text, 0, textBoxTimebookPrice.Text.IndexOf(",") >= 0 ? new CultureInfo("sv-SE") : new CultureInfo("en-US")),
					Consultant = textBoxTimebookConsultant.Text,
					VAT = ConvertHelper.ToDecimal(textBoxTimebookVAT.Text),
					Comments = textBoxTimebookComments.Text,
					InternalComments = textBoxTimebookInternalComments.Text
				};
				r.SaveTimebook(t, ConvertHelper.ToInt32(Request.QueryString["Id"]));
			} else {
				t = new CustomerTimebook {
					Date = ConvertHelper.ToDateTime(textBoxTimebookDate.Text),
					IsHeader = checkBoxTimebookIsHeader.Checked,
					Comments = textBoxTimebookComments.Text
				};
				r.SaveHeaderTimebook(t, ConvertHelper.ToInt32(Request.QueryString["Id"]));
			}
			Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=timebook", id));
		}

		protected void buttonSaveInvoice_Click(object sender, EventArgs e)
		{
			int n = vr.GetLatestInvoiceNumber(companyId);
			CustomerContact customerContact = null;
			if (labelInvoiceYourReferencePerson.Visible) {
//				customerContact = customer.PrimaryContact;
				customerContact = customer.PrimaryContact != null ? customer.PrimaryContact : null;
			} else {
				customerContact = new CustomerContact { Id = ConvertHelper.ToInt32(dropDownListInvoiceYourReferencePerson.SelectedValue) };
			}
			var i = new Invoice
			{
				Id = n,
				Number = string.Format("{0}-{1}", company.InvoicePrefix, n.ToString("000")),
				Date = ConvertHelper.ToDateTime(textBoxInvoiceDate.Text),
				MaturityDate = ConvertHelper.ToDateTime(labelMaturityDate.Text),
				Customer = new Customer {
					Id = id,
					Company = new Company { Id = companyId }
				},
				Comments = textBoxInvoiceComments.Text,
//				CustomerContact = customer.PrimaryContact != null ? customer.PrimaryContact : null,
				CustomerContact = customerContact,
//				YourReferencePerson = labelInvoiceYourReferencePerson.Text,
				OurReferencePerson = labelInvoiceOurReferencePerson.Text
//				PurchaseOrderNumber = labelInvoicePurchaseOrderNumber.Text
			};
			i.AddTimebook(Request.Form.GetValues("invoice-timebooks"), Request.Form.GetValues("invoice-timebooks-sortorder"));
			vr.Save(i);
			Response.Redirect(string.Format("invoices.aspx"));
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			var c = new Customer {
				Name = textBoxCustomerName.Text,
				Number = textBoxCustomerNumber.Text,
				PostalAddress = textBoxPostalAddress.Text,
				InvoiceAddress = textBoxInvoiceAddress.Text,
				InvoiceEmail = textBoxInvoiceEmail.Text,
				InvoiceEmailCC = textBoxInvoiceEmailCC.Text,
				//ContactPerson = new CustomerContact { Id = ConvertHelper.ToInt32(dropDownListYourReferencePerson.SelectedValue) },
				OurReferencePerson = textBoxOurReferencePerson.Text,
				Phone = textBoxPhone.Text,
				Email = textBoxEmail.Text,
				Language = new Language { Id = ConvertHelper.ToInt32(dropDownListLanguage.SelectedValue) },
				Currency = new Currency { Id = ConvertHelper.ToInt32(dropDownListCurrency.SelectedValue) }
			};
			r.Update(c, ConvertHelper.ToInt32(Request.QueryString["Id"]));
			Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=customer-info", id));
		}

		protected void buttonSaveNotes_Click(object sender, EventArgs e)
		{
			var t = new CustomerNotes {
				Notes = textBoxNotes.Text,
				CreatedBy = new User { Id = ConvertHelper.ToInt32(Session["UserId"]) },
				CreatedAt = DateTime.Now,
			};
			r.SaveNotes(t, ConvertHelper.ToInt32(Request.QueryString["Id"]));
			Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=notes", id));
		}

		protected void buttonSaveContact_Click(object sender, EventArgs e)
		{
			var t = new CustomerContact {
				Name = textBoxContact.Text,
				PurchaseOrderNumber = textBoxContactPurchaseOrderNumber.Text,
				Title = textBoxContactTitle.Text,
				Phone = textBoxContactPhone.Text,
				Mobile = textBoxContactMobile.Text,
				Email = textBoxContactEmail.Text,
				Type = ConvertHelper.ToInt32(radioButtonListContactType.SelectedValue)
			};
			r.SaveContact(t, ConvertHelper.ToInt32(Request.QueryString["Id"]));
			Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=contact-persons", id));
		}

		protected void buttonSaveItem_Click(object sender, EventArgs e)
		{
			var t = new CustomerItem {
				Item = new Item { Id = ConvertHelper.ToInt32(dropDownListItems.SelectedValue) },
				//Price = ConvertHelper.ToDecimal(textBoxItemPrice.Text)
				Price = ConvertHelper.ToDecimal(textBoxItemPrice.Text, 0, textBoxItemPrice.Text.IndexOf(",") >= 0 ? new CultureInfo("sv-SE") : new CultureInfo("en-US")),
			};
			r.SaveItem(t, ConvertHelper.ToInt32(Request.QueryString["Id"]));
			Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=customer-prices", id));
		}

		protected void buttonSaveAgreement_Click(object sender, EventArgs e)
		{
			var t = new CustomerAgreement {
				Date = ConvertHelper.ToDateTime(textBoxAgreementDate.Text),
				Lecturer = textBoxAgreementLecturer.Text,
				LectureTitle = textBoxAgreementLectureTitle.Text,
				Contact = textBoxAgreementContact.Text,
				Mobile = textBoxAgreementMobile.Text,
				Email = textBoxAgreementEmail.Text,
				Compensation = ConvertHelper.ToDecimal(textBoxAgreementCompensation.Text),
				PaymentTerms = textBoxAgreementPaymentTerms.Text,
				BillingAddress = textBoxAgreementBillingAddress.Text,
				OtherInformation = textBoxAgreementOtherInformation.Text,
			};
			r.SaveAgreement(t, id);
			Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=agreements", id));
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			
			notes = r.FindNotes(id);
			prices = r.FindItems(id);
			contacts = r.FindContacts(id);
			timebooks = r.FindTimebooks(id);
			timebookItems = ir.FindAllWithCustomerItems(companyId, id);
			items = ir.FindByCompany(companyId);
			agreements = r.FindAgreements(id);
			
//			int page = ConvertHelper.ToInt32(Request.QueryString["page"], 1);
			//            int pageSize = 5;
			//            int offset = (page - 1) * pageSize + 1;
			//        	timebooks = r.FindTimebooksByOffset(id, offset, pageSize);
//			pager = new Pager(r.CountAllTimebooks(id), page, pageSize);

			dropDownListSubscriptionItem.Items.Clear();
			foreach (var i in items) {
				dropDownListSubscriptionItem.Items.Add(new ListItem(i.Name, i.Id.ToString()));
			}

			dropDownListItems.Items.Clear();
			foreach (var i in items) {
				dropDownListItems.Items.Add(new ListItem(i.ToString(), i.Id.ToString()));
			}

			dropDownListTimebookItems.Items.Clear();
			foreach (var i in timebookItems) {
				var li = new ListItem(i.Name, i.Id.ToString());
				li.Attributes.Add("data-price", i.Price.ToString());
				li.Attributes.Add("data-unit", i.Unit.Name);
				li.Attributes.Add("data-consultant", i.Consultant);
				dropDownListTimebookItems.Items.Add(li);
			}

//			dropDownListYourReferencePerson.Items.Clear();
//			foreach (var c in contacts) {
//				dropDownListYourReferencePerson.Items.Add(new ListItem(c.ToString(), c.Id.ToString()));
//			}
//
			dropDownListInvoiceYourReferencePerson.Items.Clear();
			foreach (var c in contacts) {
				var li = new ListItem(c.Name, c.Id.ToString());
				li.Attributes.Add("data-purchase-order-number", c.PurchaseOrderNumber);
				dropDownListInvoiceYourReferencePerson.Items.Add(li);
			}
			
			dropDownListTimebookContacts.Items.Clear();
			foreach (var c in contacts) {
				dropDownListTimebookContacts.Items.Add(new ListItem(c.Name, c.Id.ToString()));
			}

			dropDownListLanguage.Items.Clear();
			foreach (var l in Language.GetLanguages()) {
				dropDownListLanguage.Items.Add(new ListItem(l.Name, l.Id.ToString()));
			}

			dropDownListCurrency.Items.Clear();
			foreach (var c in Currency.GetCurrencies()) {
				dropDownListCurrency.Items.Add(new ListItem(c.ToString(), c.Id.ToString()));
			}

			if (customer != null) {
				customer.Contacts = contacts;
				dropDownListLanguage.SelectedValue = customer.Language.Id.ToString();
				dropDownListCurrency.SelectedValue = customer.Currency.Id.ToString();
//				dropDownListYourReferencePerson.SelectedValue = customer.ContactPerson != null ? customer.ContactPerson.Id.ToString() : "";
				if (customer.SubscriptionItem != null) {
					dropDownListSubscriptionItem.SelectedValue = customer.SubscriptionItem.Id.ToString();
				}

				foreach (var t in new[] { new { id = 1, name = "Primary" }, new { id = 2, name = "Secondary" }, new { id = 3, name = "Other" } }) {
					if ((t.id == 1 && customer.HasPrimaryContact) || (t.id == 2 && customer.HasSecondaryContact)) {
						continue;
					}
					var li = new ListItem(t.name, t.id.ToString());
					li.Attributes.Add("class", "radio-inline");
					radioButtonListContactType.Items.Add(li);
				}
				if (!customer.HasPrimaryContact) {
					radioButtonListContactType.SelectedValue = 1.ToString();
				} else if (!customer.HasSecondaryContact) {
					radioButtonListContactType.SelectedValue = 2.ToString();
				} else {
					radioButtonListContactType.SelectedValue = 3.ToString();
				}
			}
		}
	}
}