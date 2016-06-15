using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Repositories.Sql;

namespace HW.Adm
{
    public partial class managerFunc : System.Web.UI.Page
    {
    	protected SqlManagerFunctionRepository repo = new SqlManagerFunctionRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}