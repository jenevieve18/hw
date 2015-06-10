using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using m = HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class Units : System.Web.UI.Page
    {
    	SqlUnitRepository r = new SqlUnitRepository();
    	protected IList<m.Unit> units;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	units = r.FindAll();
        }
    }
}