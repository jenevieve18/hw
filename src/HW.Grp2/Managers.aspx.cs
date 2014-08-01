using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Grp3
{
    public partial class Managers : System.Web.UI.Page
    {
    	SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
    	SqlManagerFunctionRepository managerFunctionRepository = new SqlManagerFunctionRepository();
    	protected IList<SponsorAdmin> admins = new List<SponsorAdmin>();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	admins = sponsorRepository.FindAdminBySponsor(83, -1);
        	foreach (var a in admins) {
        		a.Functions = managerFunctionRepository.FindBySponsorAdmin2(a.Id);
        	}
        }
    }
}