using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Repositories.Sql;
using HW.Core.Helpers;

namespace HW.Adm
{
    public partial class wisedelete : System.Web.UI.Page
    {
        SqlWiseRepository r = new SqlWiseRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = ConvertHelper.ToInt32(Request.QueryString["WiseID"]);
            r.Delete(id);
            Response.Redirect("wise.aspx");
        }
    }
}