using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class CustomerNotesEdit : System.Web.UI.Page
    {
        SqlCustomerRepository r = new SqlCustomerRepository();
        protected int customerId;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
            if (!IsPostBack)
            {
                var n = r.ReadNote(id);
                if (n != null)
                {
                    textBoxNotes.Text = n.Notes;
                    checkBoxReactivate.Checked = !n.Inactive;
                    placeHolderReactivate.Visible = n.Inactive;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var n = new CustomerNotes
            {
                Notes = textBoxNotes.Text,
                Inactive = !checkBoxReactivate.Checked
            };
            r.UpdateNotes(n, id);
            Response.Redirect(string.Format("customershow.aspx?Id={0}&SelectedTab=notes", customerId));
        }
    }
}