using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class Default : System.Web.UI.Page
    {
        SqlUserRepository r = new SqlUserRepository();
        protected string errorMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            var u = r.ReadByNameAndPassword(textBoxName.Text, textBoxPassword.Text);
            if (u != null) {
                Response.Redirect("dashboard.aspx");
            } else {
                errorMessage = "Invalid user name or password, please try again!";
            }
        }
    }
}