using System;
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

namespace HW.Invoicing
{
    public partial class CustomerShow : System.Web.UI.Page
    {
        SqlCustomerRepository r = new SqlCustomerRepository();
        SqlItemRepository ir = new SqlItemRepository();
        SqlInvoiceRepository vr = new SqlInvoiceRepository();
        SqlCompanyRepository cr = new SqlCompanyRepository();
        SqlLanguageRepository lr = new SqlLanguageRepository();
        protected IList<CustomerNotes> notes;
        protected IList<CustomerItem> prices;
        protected IList<CustomerContact> contacts;
        protected IList<Item> timebookItems;
        protected IList<Item> items;
        protected IList<CustomerTimebook> timebooks;
        protected IList<Language> languages;
    	protected int id;
        protected string selectedTab;
        protected Customer customer;
        protected int companyId;
        protected Company company;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

        	id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            companyId = ConvertHelper.ToInt32(Session["CompanyId"], 1);
            selectedTab = Request.QueryString["SelectedTab"] == null ? "notes" : Request.QueryString["SelectedTab"];
            if (!IsPostBack)
            {
                customer = r.Read(id);

                if (customer != null)
                {
                    labelCustomer.Text = customer.Name;
                    labelCustomerNumber.Text = textBoxCustomerNumber.Text = customer.Number;

                    labelPostalAddress.Text = customer.PostalAddress.Replace("\n", "<br>");
                    textBoxPostalAddress.Text = customer.PostalAddress;

                    labelInvoiceAddress.Text = customer.InvoiceAddress.Replace("\n", "<br>");
                    textBoxInvoiceAddress.Text = customer.InvoiceAddress;

                    labelPurchaseOrderNumber.Text = textBoxPurchaseOrderNumber.Text = customer.PurchaseOrderNumber;
                    labelYourReferencePerson.Text = textBoxYourReferencePerson.Text = customer.YourReferencePerson;
                    labelOurReferencePerson.Text = textBoxOurReferencePerson.Text = customer.OurReferencePerson;
                    labelPhone.Text = textBoxPhone.Text = customer.Phone;
                    labelEmail.Text = textBoxEmail.Text = customer.Email;
                    labelLanguage.Text = customer.Language.Name;
                    
                    labelCustomerNumber.Font.Strikeout = labelInvoiceAddress.Font.Strikeout =
                        labelPostalAddress.Font.Strikeout = labelPurchaseOrderNumber.Font.Strikeout =
                        labelYourReferencePerson.Font.Strikeout = labelOurReferencePerson.Font.Strikeout =
                        labelEmail.Font.Strikeout = labelPhone.Font.Strikeout =
                        labelLanguage.Font.Strikeout = customer.Inactive;
                    
                    labelInvoiceCustomerNumber.Text = customer.Number;
                    textBoxInvoiceDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    labelMaturityDate.Text = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
                    labelInvoiceCustomerAddress.Text = customer.InvoiceAddress.Replace("\n", "<br>");
                    labelInvoiceNumber.Text = "IHG-001";
                    labelInvoiceOurReferencePerson.Text = customer.OurReferencePerson;
                    labelInvoicePurchaseOrderNumber.Text = customer.PurchaseOrderNumber;
                    labelInvoiceYourReferencePerson.Text = customer.YourReferencePerson;

                    textBoxTimebookDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    textBoxTimebookVAT.Text = 25.ToString();
                }

                company = cr.Read(companyId);

                if (company != null)
                {
                    labelCompanyName.Text = company.Name;
                    labelCompanyAddress.Text = company.Address;
                    labelCompanyPhone.Text = company.Phone;
                    labelCompanyBankAccountNumber.Text = company.BankAccountNumber;
                    labelCompanyTIN.Text = company.TIN;
                }
            }
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

        protected void buttonSaveTimebook_Click(object sender, EventArgs e)
        {
            var t = new CustomerTimebook
            {
                Date = ConvertHelper.ToDateTime(textBoxTimebookDate.Text),
                Department = textBoxTimebookDepartment.Text,
                Contact = new CustomerContact { Id = ConvertHelper.ToInt32(dropDownListTimebookContacts.SelectedValue) },
                Item = new Item { Id = ConvertHelper.ToInt32(dropDownListTimebookItems.SelectedValue) },
                Quantity = ConvertHelper.ToDecimal(textBoxTimebookQty.Text),
                Price = ConvertHelper.ToDecimal(textBoxTimebookPrice.Text),
                Consultant = textBoxTimebookConsultant.Text,
                VAT = ConvertHelper.ToDecimal(textBoxTimebookVAT.Text),
                Comments = textBoxTimebookComments.Text,
                InternalComments = textBoxTimebookInternalComments.Text
            };
            r.SaveTimebook(t, ConvertHelper.ToInt32(Request.QueryString["Id"]));
            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=timebook", id));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static int GetLatestInvoiceNumber()
        {
            return new SqlInvoiceRepository().GetLatestInvoiceNumber();
        }

        protected void buttonSaveInvoice_Click(object sender, EventArgs e)
        {
            int n = vr.GetLatestInvoiceNumber();
            var i = new Invoice
            {
                Id = n,
                Number = string.Format("IHGF-{0}", n.ToString("000")),
                //Date = DateTime.Now,
                Date = ConvertHelper.ToDateTime(textBoxInvoiceDate.Text),
                MaturityDate = ConvertHelper.ToDateTime(labelMaturityDate.Text),
                Customer = new Customer { Id = id },
                Comments = textBoxInvoiceComments.Text
            };
            string[] timebooks = Request.Form.GetValues("invoice-timebooks");
            foreach (var t in timebooks)
            {
                i.AddTimebook(ConvertHelper.ToInt32(t));
            }
            vr.Save(i);
            Response.Redirect(string.Format("invoices.aspx"));
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var c = new Customer
            {
                Number = textBoxCustomerNumber.Text,
                PostalAddress = textBoxPostalAddress.Text,
                InvoiceAddress = textBoxInvoiceAddress.Text,
                PurchaseOrderNumber = textBoxPurchaseOrderNumber.Text,
                YourReferencePerson = textBoxYourReferencePerson.Text,
                OurReferencePerson = textBoxOurReferencePerson.Text,
                Phone = textBoxPhone.Text,
                Email = textBoxEmail.Text,
                Language = new Language { Id = ConvertHelper.ToInt32(dropDownListLanguage.SelectedValue) }
            };
            r.Update(c, ConvertHelper.ToInt32(Request.QueryString["Id"]));
            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=customer-info", id));
        }

        protected void buttonSaveNotes_Click(object sender, EventArgs e)
        {
            var t = new CustomerNotes
            {
                Notes = textBoxNotes.Text,
                CreatedBy = new User { Id = ConvertHelper.ToInt32(Session["UserId"]) },
                CreatedAt = DateTime.Now,
            };
            r.SaveNotes(t, ConvertHelper.ToInt32(Request.QueryString["Id"]));
            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=notes", id));
        }

        protected void buttonSaveContact_Click(object sender, EventArgs e)
        {
            var t = new CustomerContact
            {
                Contact = textBoxContact.Text,
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
            var t = new CustomerItem
            {
                Item = new Item { Id = ConvertHelper.ToInt32(dropDownListItems.SelectedValue) },
                Price = ConvertHelper.ToInt32(textBoxItemPrice.Text)
            };
            r.SaveItem(t, ConvertHelper.ToInt32(Request.QueryString["Id"]));
            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=customer-prices", id));
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            notes = r.FindNotes(id);
            prices = r.FindItems(id);
            contacts = r.FindContacts(id);
            timebooks = r.FindTimebooks(id);
            timebookItems = ir.FindAllWithCustomerItems(id);
            items = ir.FindAll();
            languages = lr.FindAll();

            dropDownListItems.Items.Clear();
            foreach (var i in items)
            {
                dropDownListItems.Items.Add(new ListItem(i.Name, i.Id.ToString()));
            }

            dropDownListTimebookItems.Items.Clear();
            foreach (var i in timebookItems)
            {
                var li = new ListItem(i.Name, i.Id.ToString());
                li.Attributes.Add("data-price", i.Price.ToString());
                li.Attributes.Add("data-unit", i.Unit.Name);
                dropDownListTimebookItems.Items.Add(li);
            }

            dropDownListTimebookContacts.Items.Clear();
            foreach (var c in contacts)
            {
                dropDownListTimebookContacts.Items.Add(new ListItem(c.Contact, c.Id.ToString()));
            }

            dropDownListLanguage.Items.Clear();
            foreach (var l in languages)
            {
                dropDownListLanguage.Items.Add(new ListItem(l.Name, l.Id.ToString()));
            }

            dropDownListCurrency.Items.Clear();
            foreach (var c in Customer.GetCurrencies())
            {
                dropDownListCurrency.Items.Add(new ListItem(c.ToString(), c.Id.ToString()));
            }

            if (customer != null)
            {
                customer.Contacts = contacts;
                dropDownListLanguage.SelectedValue = customer.Language.Id.ToString();

                foreach (var t in new[] { new { id = 1, name = "Primary" }, new { id = 2, name = "Secondary" }, new { id = 3, name = "Other" } })
                {
                    if ((t.id == 1 && customer.HasPrimaryContacts) || (t.id == 2 && customer.HasSecondaryContacts))
                    {
                        continue;
                    }
                    var li = new ListItem(t.name, t.id.ToString());
                    li.Attributes.Add("class", "radio-inline");
                    radioButtonListContactType.Items.Add(li);
                }
                if (!customer.HasPrimaryContacts)
                {
                    radioButtonListContactType.SelectedValue = 1.ToString();
                } else if (!customer.HasSecondaryContacts) {
                    radioButtonListContactType.SelectedValue = 2.ToString();
                } else {
                    radioButtonListContactType.SelectedValue = 3.ToString();
                }
            }
        }
    }
}