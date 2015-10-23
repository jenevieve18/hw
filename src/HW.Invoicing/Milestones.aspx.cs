using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class Milestones : System.Web.UI.Page
    {
    	SqlMilestoneRepository r = new SqlMilestoneRepository();
    	protected IList<Milestone> milestones;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	milestones = r.FindAll();
        }
    }
}