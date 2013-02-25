//	<file>
//		<license></license>
//		<owner name="Jens Pettersson" email=""/>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Repositories;

namespace HWgrp
{
	public partial class managers : System.Web.UI.Page
	{
		IManagerFunctionRepository managerRepository = AppContext.GetRepositoryFactory().CreateManagerFunctionRepository();
		ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
			bool delete = HttpContext.Current.Request.QueryString["Delete"] != null;
			if (delete) {
				int sponsorAdminID = Convert.ToInt32(HttpContext.Current.Request.QueryString["Delete"]);
				sponsorRepository.UpdateDeletedAdmin(sponsorID, sponsorAdminID);
			}
			if (sponsorID != 0) {
				Managers.Text = " <tr><td><B>Name</B>&nbsp;&nbsp;</td><td><b>Roles</b></td></tr>";
				int sponsorAdminID = HttpContext.Current.Session["SponsorAdminID"] != null ? Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) : -1;
				foreach (var s in sponsorRepository.FindAdminBySponsor(sponsorID, sponsorAdminID)) {
					Managers.Text += "<TR><TD>" + (s.ReadOnly ? "" : "<img src=\"img/locked.gif\"/> ") + "<A HREF=\"managerSetup.aspx?SAID=" + s.Id.ToString() + "\">" + (s.Name == "" ? (s.Usr == "" ? "&gt; empty &lt;" : s.Usr) : s.Name) + "</A>&nbsp;&nbsp;</TD><TD>";
					int cx = 0;
					foreach (var f in managerRepository.FindBySponsorAdmin(sponsorAdminID)) {
						Managers.Text += (cx++ > 0 ? ", " : "") + f.Function;
					}
					Managers.Text += "</TD><TD><A HREF=\"javascript:if(confirm('Are you sure you want to delete this manager?')){location.href='managers.aspx?Delete=" + s.Id + "';}\"><img src=\"img/deltoolsmall.gif\" border=\"0\"/></TD></TR>";
				}
			} else {
				HttpContext.Current.Response.Redirect("default.aspx", true);
			}
		}
	}
}