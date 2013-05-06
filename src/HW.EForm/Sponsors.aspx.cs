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
	public partial class Sponsors : System.Web.UI.Page
	{
		ISponsorRepository r = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		protected IList<Sponsor> sponsors;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			sponsors = r.FindAll();
		}
	}
}