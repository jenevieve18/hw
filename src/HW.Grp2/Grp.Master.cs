using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Grp2
{
    public partial class Grp : System.Web.UI.MasterPage
    {
    	SqlManagerFunctionRepository repository = new SqlManagerFunctionRepository();
    	protected IList<ManagerFunction> functions;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	functions = repository.FindAll();
        }
    }
}