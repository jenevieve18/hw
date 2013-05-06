using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HW.EForm
{
	public partial class SponsorsShow : System.Web.UI.Page
	{
		ISponsorRepository r = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		IProjectRepository pr = AppContext.GetRepositoryFactory().CreateProjectRepository();
		protected Sponsor sponsor;
		protected IList<ProjectRoundUnit> units;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int id = Convert.ToInt32(Request["SponsorID"]);
			sponsor = r.Read(id);
			units = pr.FindAllProjectRoundUnits();
		}
	}
}