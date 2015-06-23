using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;
using m = HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class UnitAdd : System.Web.UI.Page
    {
    	SqlUnitRepository r = new SqlUnitRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");
        }

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			var u = new m.Unit {
				Name = textBoxName.Text
			};
			r.Save(u);
			Response.Redirect("units.aspx");
		}
    }
}