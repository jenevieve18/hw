using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;

namespace HW.Adm
{
    public partial class managerFuncDelete : System.Web.UI.Page
    {
        SqlManagerFunctionRepository r = new SqlManagerFunctionRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = ConvertHelper.ToInt32(Request.QueryString["ManagerFunctionID"]);
            r.Delete(id);
            Response.Redirect("managerFunc.aspx");
        }
    }
}