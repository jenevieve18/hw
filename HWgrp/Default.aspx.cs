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

namespace HWgrp
{
	public partial class Default : System.Web.UI.Page
	{
		IManagerFunctionRepository functionRepository = AppContext.GetRepositoryFactory().CreateManagerFunctionRepository();
		ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		
		public void Logout()
		{
			HttpContext.Current.Session.Remove("SponsorID");
			HttpContext.Current.Session.Remove("SponsorAdminID");
			HttpContext.Current.Session.Remove("Sponsor");
			HttpContext.Current.Session.Remove("Anonymized");
			HttpContext.Current.Session.Remove("SeeUsers");
			HttpContext.Current.Session.Remove("ReadOnly");
			
			HttpContext.Current.Session.Remove("SuperAdminID");
			
			ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language=\"JavaScript\">window.close();</script>");
		}
		
		public void Login(string skey, string sakey, string sa, string said, string anv, string los)
		{
			SponsorAdmin s = sponsorRepository.ReadSponsorAdmin(skey, sakey, sa, said, anv, los);
			if (s != null) {
				HttpContext.Current.Session["Name"] = s.Name;
				if (s.SuperAdmin) {
					HttpContext.Current.Session["SuperAdminID"] = s.SuperAdmin;
					HttpContext.Current.Response.Redirect("superadmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
				} else {
					HttpContext.Current.Session["SponsorAdminID"] = s.Id;
					HttpContext.Current.Session["SponsorID"] = s.Sponsor.Id;
					HttpContext.Current.Session["Sponsor"] = s.Sponsor.Name;
					HttpContext.Current.Session["Anonymized"] = s.Anonymized ? 1 : 0;
					HttpContext.Current.Session["SeeUsers"] = s.SeeUsers ? 1 : 0;
					HttpContext.Current.Session["ReadOnly"] = s.ReadOnly ? 1 : 0;
					
					string firstUrl = "default.aspx?Logout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
					ManagerFunction f = functionRepository.ReadFirstFunctionBySponsorAdmin(s.Id);
					if (f != null) {
						firstUrl = f.URL + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
					}
					HttpContext.Current.Response.Redirect(firstUrl, true);
				}
			}
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (HttpContext.Current.Request.Form["ANV"] != null && HttpContext.Current.Request.Form["LOS"] != null || HttpContext.Current.Request.QueryString["SKEY"] != null || HttpContext.Current.Request.QueryString["SAKEY"] != null) {
				string skey = HttpContext.Current.Request.QueryString["SKEY"];
				string sakey = HttpContext.Current.Request.QueryString["SAKEY"];
				string anv = HttpContext.Current.Request.Form["ANV"];
				string los = HttpContext.Current.Request.Form["LOS"];
				string sa = HttpContext.Current.Request.QueryString["SA"];
				string said = sa != null ? HttpContext.Current.Session["SuperAdminID"].ToString() : "";
				Login(skey, sakey, sa, said, anv, los);
			} else if (HttpContext.Current.Request.QueryString["Logout"] != null || HttpContext.Current.Request.QueryString["SuperLogout"] != null) {
				Logout();
			}
//			if (login && HttpContext.Current.Session["SponsorAdminID"] != null || HttpContext.Current.Request.QueryString["Logout"] != null) {
//				HttpContext.Current.Session.Remove("SponsorID");
//				HttpContext.Current.Session.Remove("SponsorAdminID");
//				HttpContext.Current.Session.Remove("Sponsor");
//				HttpContext.Current.Session.Remove("Anonymized");
//				HttpContext.Current.Session.Remove("SeeUsers");
//				HttpContext.Current.Session.Remove("ReadOnly");
//				ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language=\"JavaScript\">window.close();</script>");
//			} else if (login && HttpContext.Current.Session["SuperAdminID"] != null || HttpContext.Current.Request.QueryString["SuperLogout"] != null) {
//				HttpContext.Current.Session.Remove("SuperAdminID");
//
//				ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language=\"JavaScript\">window.close();</script>");
//			}
		}
	}
}