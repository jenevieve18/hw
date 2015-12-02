using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class CollaboratorEdit : System.Web.UI.Page
    {
        int id;
        SqlUserRepository r = new SqlUserRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            id = ConvertHelper.ToInt32(Request.QueryString["UserID"]);
            if (!IsPostBack)
            {
                var u = r.Read(id);
                if (u != null)
                {
                    textBoxUsername.Text = u.Username;
                    textBoxName.Text = u.Name;
                    //textBoxPassword.Text = u.Password;
                    //textBoxPassword.Attributes["value"] = u.Password;
                    textBoxColor.Text = u.Color;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var d = new User
            {
                Username = textBoxUsername.Text,
                Name = textBoxName.Text,
                Password = textBoxPassword.Text,
                Color = textBoxColor.Text
            };
            r.Update(d, id);
            Response.Redirect("users.aspx");
        }
    }
}