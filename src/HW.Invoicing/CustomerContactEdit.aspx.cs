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
    public partial class CustomerContactEdit : System.Web.UI.Page
    {
    	SqlCustomerRepository r = new SqlCustomerRepository();
        protected int id;
        int customerId;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            if (!IsPostBack)
            {
                var c = r.ReadContact(id);
                if (c != null)
                {
                    textBoxContact.Text = c.Contact;
                    textBoxPhone.Text = c.Phone;
                    textBoxMobile.Text = c.Mobile;
                    textBoxEmail.Text = c.Email;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var c = new CustomerContact {
                Contact = textBoxContact.Text,
                Phone = textBoxPhone.Text,
                Mobile = textBoxMobile.Text,
                Email = textBoxEmail.Text
            };
            r.UpdateContact(c, id);
            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=contact-persons", customerId));
        }
    }
}