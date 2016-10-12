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
using HW.Core.Helpers;

namespace HW.Grp
{
	public partial class Default : System.Web.UI.Page
	{
		SqlManagerFunctionRepository functionRepo = new SqlManagerFunctionRepository();
		SqlUserRepository userRepository = new SqlUserRepository();
		
//		ISponsorRepository sponsorRepo;
//		INewsRepository newsRepo;
		
		SqlSponsorRepository sponsorRepo = new SqlSponsorRepository();
		SqlNewsRepository newsRepo = new SqlNewsRepository();
		
		protected string errorMessage = "";
		protected IList<AdminNews> adminNews;
		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);
		
//		public Default() : this(new SqlSponsorRepository(), new SqlNewsRepository())
//		{
//		}
//		
//		public Default(ISponsorRepository sponsorRepo, INewsRepository newsRepo)
//		{
//			this.sponsorRepo = sponsorRepo;
//			this.newsRepo = newsRepo;
//		}
		
		public void Index()
		{
			adminNews = newsRepo.FindTop3AdminNews();
		}

		protected CultureInfo GetCultureInfo(int lid)
		{
            switch (lid) {
                case 1: return new CultureInfo("sv-SE");
                case 4: return new CultureInfo("de-DE");
                default: return new CultureInfo("en-US");
            }
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			var userSession = new UserSession { HostAddress = Request.UserHostAddress, Agent = Request.UserAgent, Lang = ConvertHelper.ToInt32(Request.QueryString["lid"]) };
			userRepository.SaveSessionIf(Request.QueryString["lid"] != null, userSession);
			if (Request.QueryString["r"] != null) {
				Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["r"]));
			}
			userSession = userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
			if (userSession != null) {
				lid = userSession.Lang;
			}

			Index();
			
			bool login = false;
			SponsorAdmin sponsorAdmin = null;
			if (((Request.Form["ANV"] != null && Request.Form["ANV"] != "") && (Request.Form["LOS"] != null || Request.Form["LOS"] != "")) || Request.QueryString["SKEY"] != null || Request.QueryString["SAKEY"] != null) {
				string sponsorKey = Request.QueryString["SKEY"];
				string sponsorAdminKey = Request.QueryString["SAKEY"];
				string username = Request.Form["ANV"];
				string password = Request.Form["LOS"];
				string sa = Request.QueryString["SA"];
				string said = sa != null ? (Session["SuperAdminID"] != null ? Session["SuperAdminID"].ToString() : "0") : "";
				sponsorAdmin = sponsorRepo.ReadSponsorAdmin(sponsorKey, sponsorAdminKey, sa, said, username, password);
				if (sponsorAdmin != null) {
					login = true;
					sponsorRepo.SaveSponsorAdminSession(sponsorAdmin.Id, DateTime.Now);
					Session["SponsorAdminSessionID"] = sponsorRepo.ReadLastSponsorAdminSession();
					
					Session["Name"] = sponsorAdmin.Name;
					if (sponsorAdmin.SuperAdmin) {
						Session["SuperAdminID"] = sponsorAdmin.SuperAdminId;
					} else {
						Session["SponsorID"] = sponsorAdmin.Sponsor.Id;
						Session["SponsorAdminID"] = sponsorAdmin.Id;
						Session["Sponsor"] = sponsorAdmin.Sponsor.Name;
						Session["Anonymized"] = sponsorAdmin.Anonymized ? 1 : 0;
						Session["SeeUsers"] = sponsorAdmin.SeeUsers ? 1 : 0;
						Session["ReadOnly"] = sponsorAdmin.ReadOnly ? 1 : 0;

						SessionHelper.AddIf(sponsorKey != null, "SponsorKey", sponsorKey);
						SessionHelper.RemoveIf(sponsorKey == null, "SponsorKey");
					}
				} else {
					errorMessage = R.Str(lid, "login.invalid", "Invalid user name and password. Please try again.");
				}
			}
			if (login && (Session["SponsorAdminID"] != null || Session["SuperAdminID"] != null)) {
				string firstUrl = "default.aspx?Logout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
				ManagerFunction firstFunction = functionRepo.ReadFirstFunctionBySponsorAdmin(sponsorAdmin.Id);
				if (firstFunction != null) {
					firstUrl = firstFunction.URL + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
				}
				if (sponsorAdmin.SuperAdmin) {
					Response.Redirect("superadmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
				} else if (Session["SponsorID"] != null) {
					Response.Redirect(firstUrl, true);
				}
			} else if (login && Session["SponsorAdminID"] != null || Request.QueryString["Logout"] != null) {
				sponsorRepo.UpdateSponsorAdminSession(Convert.ToInt32(Session["SponsorAdminSessionID"]), DateTime.Now);
				
				Session.Remove("SponsorID");
				Session.Remove("SponsorAdminID");
				Session.Remove("Sponsor");
				Session.Remove("Anonymized");
				Session.Remove("SeeUsers");
				Session.Remove("ReadOnly");
				Session.Remove("SponsorAdminSessionID");
				Session.Remove("SponsorKey");
				ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language='JavaScript'>window.close();</script>");
			} else if (login && Session["SuperAdminID"] != null || Request.QueryString["SuperLogout"] != null) {
				Session.Remove("SuperAdminID");
				ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language='JavaScript'>window.close();</script>");
			}
		}
	}
}