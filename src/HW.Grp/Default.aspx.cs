using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class Default : System.Web.UI.Page
	{
		protected string errorMessage = "";
		SqlManagerFunctionRepository functionRepository = new SqlManagerFunctionRepository();
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			bool superAdmin = false;
			bool login = false;
			SponsorAdmin s = null;
			if ((Request.Form["ANV"] != null && Request.Form["LOS"] != null) || Request.QueryString["SKEY"] != null || Request.QueryString["SAKEY"] != null) {
				login = true;
				string skey = Request.QueryString["SKEY"];
				string sakey = Request.QueryString["SAKEY"];
				string anv = Request.Form["ANV"];
				string los = Request.Form["LOS"];
				string sa = Request.QueryString["SA"];
				string said = sa != null ? (Session["SuperAdminID"] != null ? Session["SuperAdminID"].ToString() : "0") : "";
				s = sponsorRepository.ReadSponsorAdmin(skey, sakey, sa, said, anv, los);
				if (s != null) {
					Session["Name"] = s.Name;
					if (s.SuperAdmin) {
						Session["SuperAdminID"] = s.SuperAdminId;
						superAdmin = true;
					} else {
						Session["SponsorID"] = s.Sponsor.Id;
						Session["SponsorAdminID"] = s.Id;
						Session["Sponsor"] = s.Sponsor.Name;
						Session["Anonymized"] = s.Anonymized ? 1 : 0;
						Session["SeeUsers"] = s.SeeUsers ? 1 : 0;
						Session["ReadOnly"] = s.ReadOnly ? 1 : 0;
					}
				}
			}
			if (login && (Session["SponsorAdminID"] != null || Session["SuperAdminID"] != null)) {
				string firstUrl = "default.aspx?Logout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
				ManagerFunction f = functionRepository.ReadFirstFunctionBySponsorAdmin(s.Id);
				if (f != null) {
					firstUrl = f.URL + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
				}
				if (superAdmin) {
					Response.Redirect("superadmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
				} else if (Session["SponsorID"] != null) {
					Response.Redirect(firstUrl, true);
				}
			} else if (login && Session["SponsorAdminID"] != null || Request.QueryString["Logout"] != null) {
				Session.Remove("SponsorID");
				Session.Remove("SponsorAdminID");
				Session.Remove("Sponsor");
				Session.Remove("Anonymized");
				Session.Remove("SeeUsers");
				Session.Remove("ReadOnly");
				ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language=\"JavaScript\">window.close();</script>");
			} else if (login && Session["SuperAdminID"] != null || Request.QueryString["SuperLogout"] != null) {
				Session.Remove("SuperAdminID");
				ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language=\"JavaScript\">window.close();</script>");
			}
		}
	}
}