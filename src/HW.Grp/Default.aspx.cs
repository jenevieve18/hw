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
using HW.Grp.WebService;

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
            var Service = new Soap();

			if (((Request.Form["ANV"] != null && Request.Form["ANV"] != "") && (Request.Form["LOS"] != null || Request.Form["LOS"] != "")) || Request.QueryString["SKEY"] != null || Request.QueryString["SAKEY"] != null) {
				string sponsorKey = Request.QueryString["SKEY"];
				string sponsorAdminKey = Request.QueryString["SAKEY"];
				string username = Request.Form["ANV"];
				string password = Request.Form["LOS"];
				string sa = Request.QueryString["SA"];
				string said = sa != null ? (Session["SuperAdminID"] != null ? Session["SuperAdminID"].ToString() : "0") : "";
                
                string firstUrl = "default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();

                var ServiceResponse = Service.ManagerLogin(username, password, 20);
                if(ServiceResponse.Token != null)
                {
                    firstUrl = "default.aspx?Logout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
                    login = true;
                    Session["Token"] = ServiceResponse.Token;
                    if (ServiceResponse.SuperAdminId > 0)
                    {
                        Session["SuperAdminID"] = ServiceResponse.SuperAdminId;
                        Response.Redirect("superadmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                    }
                    else
                    {
                        Session["SponsorID"] = ServiceResponse.Sponsor.Id;
                        Session["SponsorAdminID"] = ServiceResponse.Id;
                        Session["Sponsor"] = ServiceResponse.Sponsor.Name;
                        Session["Anonymized"] = ServiceResponse.Anonymized ? 1 : 0;
                        Session["SeeUsers"] = ServiceResponse.SeeUsers ? 1 : 0;
                        Session["ReadOnly"] = ServiceResponse.ReadOnly ? 1 : 0;

                        ManagerFunction firstFunction = functionRepo.ReadFirstFunctionBySponsorAdmin(ServiceResponse.Id);
                        firstUrl = firstFunction.URL + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
                        Response.Redirect(firstUrl, true);
                    }
                }
                Response.Redirect(firstUrl, true);


            }

            if ((Session["SponsorAdminID"] != null && Request.QueryString["Logout"] != null) || (Session["SuperAdminID"] != null && Request.QueryString["SuperLogout"] != null))
            {
                var LogoutResponse = Service.ManagerLogOut(Convert.ToInt32(Session["SponsorAdminID"]), Session["Token"].ToString());
                Session.Remove("Token");
                if (Session["SuperAdminID"] != null)
                {
                    Session.Remove("SuperAdminID");
                }
                else
                {
                    Session.Remove("SponsorID");
                    Session.Remove("SponsorAdminID");
                    Session.Remove("Sponsor");
                    Session.Remove("Anonymized");
                    Session.Remove("SeeUsers");
                    Session.Remove("ReadOnly");
                    Session.Remove("SponsorAdminSessionID");
                    Session.Remove("SponsorKey");
                }
                
                ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language='JavaScript'>window.close();</script>");
            }
        }
    }
}