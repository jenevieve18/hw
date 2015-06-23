using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using m = HW.Invoicing.Core.Models;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class UnitEdit : System.Web.UI.Page
    {
        SqlUnitRepository r = new SqlUnitRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            if (!IsPostBack) {
                var u = r.Read(id);
                if (u != null)
                {
                    textBoxName.Text = u.Name;
                }
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var u = new m.Unit
            {
                Name = textBoxName.Text
            };
            r.Update(u, ConvertHelper.ToInt32(Request.QueryString["Id"]));
            Response.Redirect("units.aspx");
        }
    }
}