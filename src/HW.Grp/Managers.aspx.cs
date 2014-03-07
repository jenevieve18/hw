using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class Managers : System.Web.UI.Page
	{
		protected IList<SponsorAdmin> sponsorAdmins;
		protected SqlManagerFunctionRepository managerRepository = new SqlManagerFunctionRepository();
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			sponsorRepository.SaveSponsorAdminSessionFunction(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Managers, DateTime.Now);
			int sponsorID = Convert.ToInt32(Session["SponsorID"]);
			bool delete = Request.QueryString["Delete"] != null;
			if (delete) {
				int sponsorAdminID = Convert.ToInt32(Request.QueryString["Delete"]);
				sponsorRepository.UpdateDeletedAdmin(sponsorID, sponsorAdminID);
			}
			if (sponsorID != 0) {
				labelManagers.Text = " <tr><td><B>Name</B>&nbsp;&nbsp;</td><td><b>Roles</b></td></tr>";
				int sponsorAdminID = Session["SponsorAdminID"] != null ? Convert.ToInt32(Session["SponsorAdminID"]) : -1;
				sponsorAdmins = sponsorRepository.FindAdminBySponsor(sponsorID, sponsorAdminID);
				foreach (var s in sponsorAdmins) {
					labelManagers.Text += "<TR><TD>" + (!s.ReadOnly ? "" : "<img src='img/locked.gif'/> ") + "<A HREF='managerSetup.aspx?SAID=" + s.Id.ToString() + "'>" + (s.Name == "" ? (s.Usr == "" ? "&gt; empty &lt;" : s.Usr) : s.Name) + "</A>&nbsp;&nbsp;</TD><TD>";
					int cx = 0;
					foreach (var f in managerRepository.FindBySponsorAdmin(s.Id)) {
						labelManagers.Text += (cx++ > 0 ? ", " : "") + f.Function;
					}
					labelManagers.Text += "</TD><TD><A HREF=\"javascript:if(confirm('Are you sure you want to delete this manager?')){location.href='managers.aspx?Delete=" + s.Id + "';}\"><img src='img/deltoolsmall.gif' border='0'/></TD></TR>";
				}
			} else {
				Response.Redirect("default.aspx", true);
			}
		}
	}
}