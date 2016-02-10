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
    public partial class plotType : System.Web.UI.Page
    {
    	SqlPlotTypeRepository r = new SqlPlotTypeRepository();
    	protected IList<PlotType> types;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	types = r.FindAll();
        }
    }
}