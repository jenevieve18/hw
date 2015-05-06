using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;

namespace healthWatch 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			//Db.exec("UPDATE [Session] SET EndDT = GETDATE() WHERE EndDT IS NULL");
			Db.reloadApplication();
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{
            if (HttpContext.Current.Request.IsSecureConnection && HttpContext.Current.Request.Url.Host.IndexOf("healthwatch.se") < 0)
            {
                HttpContext.Current.Response.Redirect("http://" + HttpContext.Current.Request.Url.Host, true);
            }
            else if ((HttpContext.Current.Request.Url.Host != "healthwatch.se" || !HttpContext.Current.Request.IsSecureConnection) && HttpContext.Current.Request.Url.Host.IndexOf("localhost") < 0 && HttpContext.Current.Request.Url.Host.IndexOf("dev.") < 0 && HttpContext.Current.Request.Url.Host.IndexOf("new.") < 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Status = "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location", HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.Host, "healthwatch.se").Replace("http://","https://"));
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                //HttpContext.Current.Response.Redirect("https://www.healthwatch.se" + HttpContext.Current.Request.RawUrl, true);
            }

            if (HttpContext.Current.Request.Cookies["HW"] != null && HttpContext.Current.Request.Cookies["HW"]["LID"] != null && (Convert.ToInt32("0" + HttpContext.Current.Request.Cookies["HW"]["LID"]) == 1 || Convert.ToInt32("0" + HttpContext.Current.Request.Cookies["HW"]["LID"]) == 2))
            {
                HttpContext.Current.Session["LID"] = Convert.ToInt32("0" + HttpContext.Current.Request.Cookies["HW"]["LID"]);
            }
            else
            {
                HttpContext.Current.Session["LID"] = 1;
            }
            HttpContext.Current.Response.Cookies["HW"]["LID"] = Convert.ToInt32(HttpContext.Current.Session["LID"]).ToString();
            HttpContext.Current.Response.Cookies["HW"].Expires = DateTime.Now.AddYears(1);

            if (HttpContext.Current.Request.QueryString["SPK"] != null && HttpContext.Current.Request.QueryString["SPK"].ToString().Length >= 9)
            {
                string SPK = HttpContext.Current.Request.QueryString["SPK"].ToString();

                Db.loadSponsor(SPK.Substring(8), SPK.Substring(0, 8));
            }
            if(HttpContext.Current.Session["SponsorID"] == null)
            {
                HttpContext.Current.Session["SponsorID"] = 1;
            }
			HttpContext.Current.Session["HomeURL"] = "/";

            string referrer = (HttpContext.Current.Request.UrlReferrer != null && HttpContext.Current.Request.UrlReferrer.ToString() != "" ? HttpContext.Current.Request.UrlReferrer.ToString() : "");
            if (referrer.Length > 512)
            {
                referrer = referrer.Substring(0, 512);
            }
            HttpContext.Current.Session["ReferrerURL"] = referrer;
			string agent = (HttpContext.Current.Request.UserAgent != null && HttpContext.Current.Request.UserAgent.ToString() != "" ? HttpContext.Current.Request.UserAgent.ToString() : "");
            if (agent.Length > 512)
            {
                agent = agent.Substring(0, 512);
            }
            string ip = (HttpContext.Current.Request.UserHostAddress != null && HttpContext.Current.Request.UserHostAddress.ToString() != "" ? HttpContext.Current.Request.UserHostAddress.ToString() : "");
            if (ip.Length > 16)
            {
                ip = ip.Substring(0, 16);
            }

			string site = HttpContext.Current.Request.Url.Host;
			if (site.Length > 32)
			{
				site = site.Substring(0, 32);
			}

            Db.exec("INSERT INTO Session (IP, Referrer, UserAgent, Site) VALUES (" + (ip == "" ? "NULL" : "'" + ip.Replace("'", "''") + "'") + "," + (referrer == "" ? "NULL" : "'" + referrer.Replace("'", "''") + "'") + "," + (agent == "" ? "NULL" : "'" + agent.Replace("'", "''") + "'") + ",'" + site.Replace("'","") + "')");
			SqlDataReader rs = Db.rs("SELECT TOP 1 " +
                "SessionID " +
                "FROM Session " +
                "WHERE IP " + (ip == "" ? "IS NULL" : "= '" + ip.Replace("'", "''") + "'") + " " +
                "AND UserAgent " + (agent == "" ? "IS NULL" : "= '" + agent.Replace("'", "''") + "'") + " " +
                "ORDER BY SessionID DESC");
			if(rs.Read())
			{
				HttpContext.Current.Session["SessionID"] = rs.GetInt32(0);
			}
			rs.Close();
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{
            try
            {
                new ErrorHandling(
                    Server.GetLastError(), 
                    HttpContext.Current.Session, 
                    HttpContext.Current.Request);
            }
            catch (Exception) { }
		}

		protected void Session_End(Object sender, EventArgs e)
		{
            int? SID = (int?)Session["SessionID"];
			if (SID != null)
			{
				Db.exec("UPDATE [Session] SET EndDT = GETDATE(), AutoEnded = 1 WHERE EndDT IS NULL AND SessionID = " + SID);
			}
		}

		protected void Application_End(Object sender, EventArgs e)
		{
			//Db.exec("UPDATE [Session] SET EndDT = GETDATE() WHERE EndDT IS NULL");
		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

