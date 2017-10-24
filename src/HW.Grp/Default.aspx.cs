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
using grpWS = HW.Grp.WebService;
using grpWS2 = HW.Grp.WebService2;
using System.IO;
using HW.Core.Util.Saml;
using System.Configuration;
using System.Net;

namespace HW.Grp
{
    public partial class Default : System.Web.UI.Page
    {
        SqlUserRepository userRepository = new SqlUserRepository();
        /*
		SqlManagerFunctionRepository functionRepo = new SqlManagerFunctionRepository();
		SqlUserRepository userRepository = new SqlUserRepository();
		
//		ISponsorRepository sponsorRepo;
//		INewsRepository newsRepo;
		
		SqlSponsorRepository sponsorRepo = new SqlSponsorRepository();
		SqlNewsRepository newsRepo = new SqlNewsRepository();
		*/
        protected string errorMessage = "";
        //		protected IList<AdminNews> adminNews; 

        protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);
        protected string imagePath = "";

        protected string ipAddress = "";

        //		public Default() : this(new SqlSponsorRepository(), new SqlNewsRepository())
        //		{
        //		}
        //		
        //		public Default(ISponsorRepository sponsorRepo, INewsRepository newsRepo)
        //		{
        //			this.sponsorRepo = sponsorRepo;
        //			this.newsRepo = newsRepo;
        //		}
        /*
        public void Index()
		{
			adminNews = newsRepo.FindTop3AdminNews();
		}
        */
        protected CultureInfo GetCultureInfo(int lid)
        {
            switch (lid)
            {
                case 1: return new CultureInfo("sv-SE");
                case 4: return new CultureInfo("de-DE");
                default: return new CultureInfo("en-US");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //lid = 2;
            var service = new grpWS.Soap();
            var service2 = new grpWS2.Soap();

            // tests.Text = "<i class='icon-circle-arrow-right'></i>&nbsp;" + R.Str(lid, "login.signinIDP", "Sign in using IdP") + "";
            var userSession = new grpWS.UserSession { HostAddress = Request.UserHostAddress, Agent = Request.UserAgent, Lang = ConvertHelper.ToInt32(Request.QueryString["lid"]) };
            service.SaveSessionIf(Request.QueryString["lid"] != null, userSession);
            
            if (Request.QueryString["r"] != null)
            {
                Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["r"]));
            }

            userSession = service.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
            if (userSession != null)
            {
                lid = userSession.Lang;
            }

            //Index();


            if (Session["IPAddress"] == null && Session["SponsorID"] == null)
            {
                ipAddress = Request.UserHostAddress;
                var soapService = new HW.Grp.WebService.Soap();
                var ipAddressResponse = soapService.GetCheckReturnIpAddress(ipAddress);
                if (ipAddressResponse.SponsorId != 0)
                {
                    Session["IPAddress"] = ipAddressResponse.RealmIdentifier;
                    Session["SponsorID"] = ipAddressResponse.SponsorId;
                    Session["RealmType"] = ipAddressResponse.RealmType;
                    Session["IdpUrl"] = ipAddressResponse.IdpUrl;
                    Session["UserKeyAttributeValue"] = ipAddressResponse.UserKeyAttributeValue;
                }

                else
                {
                    Session["IPAddress"] = ipAddressResponse.RealmIdentifier;

                }
            }

            if (Session["IPAddress"] != null)
            {
                
                if (Session["IPAddress"].ToString() == "Not RealmIdentifier")
                {
                    if (((Request.Form["ANV"] != null && Request.Form["ANV"] != "") && (Request.Form["LOS"] != null && Request.Form["LOS"] != "")) || Request.QueryString["SKEY"] != null || Request.QueryString["SAKEY"] != null)
                    {

                        string sponsorKey = Request.QueryString["SKEY"];
                        string sponsorAdminKey = Request.QueryString["SAKEY"];
                        string username = Request.Form["ANV"];
                        string password = Request.Form["LOS"];
                        string sa = Request.QueryString["SA"];
                        string said = sa != null ? (Session["SuperAdminID"] != null ? Session["SuperAdminID"].ToString() : "0") : "";

                        string firstUrl = "default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();

                        if (sponsorKey != null)
                        {
                        }
                        else
                        {
                            /// <summary>
                            /// Update Manager login process to call GRP-WS (ManagerLogin Webmethod) to login.
                            /// </summary>
                            var serviceResponse = service.ManagerLogin(username, password, 20);
                            if (serviceResponse.Token != null)
                            {
                                firstUrl = "default.aspx?Logout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();

                                Session["Name"] = serviceResponse.Name;
                                if (serviceResponse.SuperAdminId > 0)
                                {
                                    Session["SuperAdminToken"] = serviceResponse.Token;
                                    Session["SuperAdminID"] = serviceResponse.SuperAdminId;
                                    Response.Redirect("superadmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                                }
                                else
                                {
                                    Session["Token"] = serviceResponse.Token;
                                    Session["SponsorID"] = serviceResponse.Sponsor.Id;
                                    Session["SponsorAdminID"] = serviceResponse.Id;
                                    Session["Sponsor"] = serviceResponse.Sponsor.Name;
                                    Session["Anonymized"] = serviceResponse.Anonymized ? 1 : 0;
                                    Session["SeeUsers"] = serviceResponse.SeeUsers ? 1 : 0;
                                    Session["ReadOnly"] = serviceResponse.ReadOnly ? 1 : 0;

                                    /// <summary>
                                    /// Regular User or Using UN/PW
                                    /// </summary>

                                    if (HttpContext.Current.Session["Token"] != null)
                                    {
                                        try
                                        {
                                            var secondServiceResponse = service2.ManagerLogin(username, password, 20);
                                            if (secondServiceResponse.Token != null)
                                            {
                                                Session["SecondToken"] = secondServiceResponse.Token;
                                            }
                                        }
                                        catch (Exception)
                                        {

                                            // throw;
                                        }
                                    }

                                    if (Session["SponsorID"] != null)
                                    {
                                        //imagePath = "D:\\Projects\\HealthWatch\\HW\\src\\HW.Grp\\img\\Sponsor" + Session["SponsorID"].ToString();
                                        imagePath = Server.MapPath("~\\img\\Sponsor" + Session["SponsorID"].ToString());
                                        /// <summary>
                                        /// Check if Sponsor image directory exist
                                        /// if true then delete
                                        /// </summary>
                                        if (Directory.Exists(imagePath))
                                        {
                                            Directory.Delete(imagePath, true);
                                        }
                                    }

                                    //ManagerFunction firstFunction = functionRepo.ReadFirstFunctionBySponsorAdmin(serviceResponse.Id);
                                    //firstUrl = firstFunction.URL + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
                                    firstUrl = "org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
                                    Response.Redirect(firstUrl, true);
                                }
                            }
                            else
                            {
                                errorMessage = R.Str(lid, "login.invalid", "Invalid user name and password. Please try again.");
                            }
                        }
                    }

                    if (Session["SponsorID"] != null)
                    {
                        imagePath = Server.MapPath("~\\img\\Sponsor" + Session["SponsorID"].ToString());
                        /// <summary>
                        /// Check if Sponsor image directory exist
                        /// if true then delete during logout
                        /// </summary>
                        if (Directory.Exists(imagePath))
                        {
                            Directory.Delete(imagePath, true);
                        }
                    }

                    if (Session["SponsorAdminID"] != null && Request.QueryString["Logout"] != null)
                    {
                        if (Session["Token"] != null)
                        {
                            /// <summary>
                            /// Update Manager logout process to call GRP-WS (ManagerLogout Webmethod) for expiration of token.
                            /// </summary>
                            var logoutResponse = service.ManagerLogOut(Session["Token"].ToString());
                            if(Session["SecondToken"] != null)
                            {
                                var secondLogoutResponse = service2.ManagerLogOut(Session["SecondToken"].ToString());
                                Session.Remove("SecondToken");
                            }
                        }
                        Session.Remove("Token");
                        Session.Remove("SponsorID");
                        Session.Remove("SponsorAdminID");
                        Session.Remove("Sponsor");
                        Session.Remove("Anonymized");
                        Session.Remove("SeeUsers");
                        Session.Remove("ReadOnly");
                        Session.Remove("SponsorAdminSessionID");
                        Session.Remove("SponsorKey");
                        ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language='JavaScript'>window.close();</script>");
                    }
                    else if (Session["SuperAdminID"] != null || Request.QueryString["SuperLogout"] != null)
                    {
                        if (Session["SuperAdminToken"] != null)
                        {
                            /// <summary>
                            /// Update Manager logout process to call GRP-WS (ManagerLogout Webmethod) for expiration of token.
                            /// </summary>
                            var logoutResponse = service.ManagerLogOut(Session["SuperAdminToken"].ToString());
                        }
                        Session.Remove("SuperAdminToken");
                        Session.Remove("SuperAdminID");
                        ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language='JavaScript'>window.close();</script>");
                    }

                }

                else
                {

                   if (Request.QueryString["Logout"] != null)
                    {
                        if (Session["Token"] != null)
                        {
                            /// <summary>
                            /// Update Manager logout process to call GRP-WS (ManagerLogout Webmethod) for expiration of token.
                            /// </summary>
                            var logoutResponse = service.ManagerLogOut(Session["Token"].ToString());
                            if(Session["SecondToken"] != null)
                            {
                                var secondLogoutResponse = service2.ManagerLogOut(Session["SecondToken"].ToString());
                                Session.Remove("SecondToken");
                            }
                        }
                        Session.Remove("Token");
                        Session.Remove("SponsorAdminID");
                        Session.Remove("Sponsor");
                        Session.Remove("Anonymized");
                        Session.Remove("SeeUsers");
                        Session.Remove("ReadOnly");
                        Session.Remove("SponsorAdminSessionID");
                        Session.Remove("SponsorKey");
                        ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language='JavaScript'>window.close();</script>");
                    }
                    else if (Request.Form["SAMLResponse"] == null)
                    {
                        // create saml request to IDP
                        var samlEndPoint = Session["IdpUrl"].ToString();

                        var request = new AuthRequest(
                            ConfigurationManager.AppSettings["SAMLIssuer"].ToString(),
                            ConfigurationManager.AppSettings["SAMLAssertionURL"].ToString(),
                            samlEndPoint
                            );

                        string url = request.GetRedirectUrl(samlEndPoint);
                        Response.Redirect(url);
                    }
                    else if (Request.Form["SAMLResponse"] != null)
                    {
                        string firstUrl = "default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
                        var SAMLResponse = Request.Form["SAMLResponse"];
                        var sponsorID = Session["SponsorId"].ToString();
                        string userKey = Session["UserKeyAttributeValue"].ToString();
                        // Send response to GRP-WS for authentication.
                        // If token not null then proceed to org/specific page.
                        var serviceResponse = service.ConsumeSignedResponse(Convert.ToInt32(sponsorID), userKey , SAMLResponse, 20);
                        if (serviceResponse.Token != null)
                        {
                            firstUrl = "default.aspx?Logout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();

                            Session["Name"] = serviceResponse.Name;
                            if (serviceResponse.SuperAdminId > 0)
                            {
                                Session["SuperAdminToken"] = serviceResponse.Token;
                                Session["SuperAdminID"] = serviceResponse.SuperAdminId;
                                Response.Redirect("superadmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                            }
                            else
                            {
                                Session["Token"] = serviceResponse.Token;
                                Session["SponsorID"] = serviceResponse.Sponsor.Id;
                                Session["SponsorAdminID"] = serviceResponse.Id;
                                Session["Sponsor"] = serviceResponse.Sponsor.Name;
                                Session["Anonymized"] = serviceResponse.Anonymized ? 1 : 0;
                                Session["SeeUsers"] = serviceResponse.SeeUsers ? 1 : 0;
                                Session["ReadOnly"] = serviceResponse.ReadOnly ? 1 : 0;

                                /// <summary>
                                /// For SAML Second Login
                                /// </summary>
                                /// 

                                try
                                {
                                    var secondServiceResponse = service2.ConsumeSignedResponse(Convert.ToInt32(sponsorID), userKey, SAMLResponse, 20);
                                    if (secondServiceResponse.Token != null)
                                    {
                                        Session["SecondToken"] = secondServiceResponse.Token;
                                    }
                                }
                                catch (Exception)
                                {

                                    // throw;
                                }

                                if (Session["SponsorID"] != null)
                                {
                                    //imagePath = "D:\\Projects\\HealthWatch\\HW\\src\\HW.Grp\\img\\Sponsor" + Session["SponsorID"].ToString();
                                    imagePath = Server.MapPath("~\\img\\Sponsor" + Session["SponsorID"].ToString() + Session["SponsorAdminID"].ToString());
                                    /// <summary>
                                    /// Check if Sponsor image directory exist
                                    /// if true then delete
                                    /// </summary>
                                    if (Directory.Exists(imagePath))
                                    {
                                        Directory.Delete(imagePath, true);
                                    }
                                }

                                //ManagerFunction firstFunction = functionRepo.ReadFirstFunctionBySponsorAdmin(serviceResponse.Id);
                                //firstUrl = firstFunction.URL + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
                                firstUrl = "org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
                                Response.Redirect(firstUrl, true);
                            }
                        }
                        else
                        {
                            //form1.Visible = false;
                            //messageID.Text = "    User not found!!!";
                            ////errorMessage = R.Str(lid, "login.invalid", "Invalid user name and password. Please try again.");
                        }
                    }
                }
            }
            //tests.ServerClick += new EventHandler(RedirectPage);
        }
        protected void RedirectPage(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next());
        }
    }
}
