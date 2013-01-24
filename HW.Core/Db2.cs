using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using HW.Core;

/// <summary>
/// Summary description for Db
/// </summary>
public class Db2
{
	public static SqlDataReader rs(string sqlString)
	{
		return rs(sqlString, "SqlConnection");
	}
	
	public static SqlDataReader rs(string sqlString, string con)
	{
		SqlConnection dataConnection = new SqlConnection(ConfigurationSettings.AppSettings[con]);
		dataConnection.Open();
		SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
		SqlDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
		return dataReader;
	}

	public static void exec(string sqlString)
	{
		exec(sqlString, "SqlConnection");
	}
	
	public static void exec(string sqlString, string con)
	{
		SqlConnection dataConnection = new SqlConnection(ConfigurationSettings.AppSettings[con]);
		dataConnection.Open();
		SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
		dataCommand.ExecuteNonQuery();
		dataConnection.Close();
		dataConnection.Dispose();
	}

//	public static string header()
//	{
//		StringBuilder sb = new StringBuilder();
//
//		sb.Append("<title>HealthWatch.se / Group admin</title>");
//		//sb.Append("<link href=\"main.css?V=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\" rel=\"stylesheet\" type=\"text/css\">");
//		sb.Append("<meta http-equiv=\"Pragma\" content=\"no-cache\">");
//		sb.Append("<meta http-equiv=\"Expires\" content=\"-1\">");
//		sb.Append("<meta name=\"Robots\" content=\"noarchive\">");
//		sb.Append("<script language=\"JavaScript\">window.history.forward(1);</script>");
//
//		sb.Append("<meta charset=\"utf-8\"/>");
//		sb.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">");
//		sb.Append("<!--<meta name=\"viewport\" content=\"width=device-width; initial-scale=1.0; maximum-scale=1.0;\">-->");
//		//sb.Append("<link rel=\"shortcut icon\" href=\"favicon.ico\">");
//		//sb.Append("<link rel=\"apple-touch-icon\" href=\"apple-touch-icon.png\">");
//		sb.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"includes/css/960.css\">");
//		sb.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"includes/css/site.css\" />");
//		sb.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"includes/css/admin.css\">");
//		sb.Append("<link type=\"text/css\" href=\"includes/ui/css/ui-lightness/jquery-ui-1.8.11.custom.css\" rel=\"Stylesheet\" />");
//		sb.Append("<script type=\"text/javascript\" src=\"includes/ui/js/jquery-1.5.1.min.js\"></script>");
//		sb.Append("<script type=\"text/javascript\" src=\"includes/ui/js/jquery-ui-1.8.11.custom.min.js\"></script>");
//		sb.Append("<script type=\"text/javascript\">");
//		sb.Append("$(document).ready(function() {");
//		sb.Append("var descriptionS = $(\"#submenu .description\").html();");
//		sb.Append("$(\"#submenu a\").mouseover(function() {");
//		sb.Append("$(\"#submenu .description\").html($(this).html());");
//		sb.Append("$(\"#submenu .active\").css('background-position', 'center -80px');");
//		sb.Append("});");
//		sb.Append("$(\"#submenu a\").mouseout(function () {");
//		sb.Append("$(\"#submenu .description\").html(descriptionS);");
//		sb.Append("$(\"#submenu .active\").css('background-position', 'center -120px');");
//		sb.Append("});");
//		sb.Append("});");
//		sb.Append("</script>");
//
//		return sb.ToString();
//	}
//
//	public static bool sendMail(string email, string body, string subject)
//	{
//		return sendMail("reminder@healthwatch.se", email, body, subject);
//	}
//	public static bool sendMail(string from, string email, string body, string subject)
//	{
//		bool success = false;
//		try
//		{
//			System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
//			System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
//			mail.To = email;
//			mail.From = from;
//			mail.Subject = subject;
//			mail.Body = body;
//			System.Web.Mail.SmtpMail.Send(mail);
//			success = true;
//		}
//		catch (Exception) { }
//		return success;
//	}
//
//	public static bool sendInvitation(int sponsorInviteID, string email, string body, string subject, string key)
//	{
//		bool success = false;
//
//		if (Db.isEmail(email))
//		{
//			try
//			{
//				if (body.IndexOf("<LINK/>") >= 0)
//				{
//					body = body.Replace("<LINK/>", "" + System.Configuration.ConfigurationSettings.AppSettings["healthWatchURL"] + "/i/" + key + sponsorInviteID.ToString());
//				}
//				else
//				{
//					body += "\r\n\r\n" + "" + System.Configuration.ConfigurationSettings.AppSettings["healthWatchURL"] + "/i/" + key + sponsorInviteID.ToString();
//				}
//				System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
//				System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
//				mail.To = email;
//				mail.From = "info@healthwatch.se";
//				mail.Subject = subject;
//				mail.Body = body;
//				System.Web.Mail.SmtpMail.Send(mail);
//
//				Db.exec("UPDATE SponsorInvite SET Sent = GETDATE() WHERE SponsorInviteID = " + sponsorInviteID);
//
//				success = true;
//			}
//			catch (Exception) { }
//		}
//
//		return success;
//	}
//
//	public static string bottom()
//	{
//		return "";// "</div></div>";
//	}
//	
	static ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();

	static IManagerFunctionRepository managerFunctionRepository = AppContext.GetRepositoryFactory().CreateManagerFunctionRepository();

	public static string nav()
	{
		StringBuilder sb = new StringBuilder();

		sb.Append("<div class=\"headergroup grid_16\">");
		sb.Append("<div class=\"grid_3 alpha\">");
		sb.Append("<img src=\"img/hwlogo.png\" width=\"186\" height=\"126\" alt=\"HealthWatch group administrator\">");
		sb.Append("</div>");
		sb.Append("<div class=\"grid_8 omega p2\">");
		if (HttpContext.Current.Session["SponsorID"] != null)
		{
//			SqlDataReader r = Db.rs("SELECT " +
//			                        "s.Sponsor, " +
//			                        "ss.SuperSponsorID, " +
//			                        "ssl.Header " +
//			                        "FROM Sponsor s " +
//			                        "LEFT OUTER JOIN SuperSponsor ss ON s.SuperSponsorID = ss.SuperSponsorID " +
//			                        "LEFT OUTER JOIN SuperSponsorLang ssl ON ss.SuperSponsorID = ssl.SuperSponsorID AND ssl.LangID = 1 " +
//			                        "WHERE s.SponsorID = " + HttpContext.Current.Session["SponsorID"]);
			int sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
			var s = sponsorRepository.X(sponsorID);
//			if (r.Read())
			if (s != null)
			{
				sb.Append("HealthWatch.se");
//				if (!r.IsDBNull(2) && r.GetString(0).Replace(" ", "").IndexOf(r.GetString(2).Replace(" ", "")) < 0)
				var l = s.SuperSponsor.Languages[0];
				if (l != null && l.Header != null && s.Name.Replace(" ", "").IndexOf(l.Header.Replace(" ", "")) < 0)
				{
//					sb.Append(" - " + r.GetString(2));
					sb.Append(" - " + l.Header);
				}
				sb.Append("<br />");
				sb.Append("<span style=\"font-size:14px;\">Group administration");
//				sb.Append(" - " + r.GetString(0));
				sb.Append(" - " + s.Name);
				sb.Append("</span><br />");
//				if (!r.IsDBNull(1))
				if (s.SuperSponsor != null)
				{
//					sb.Append("<img src=\"" + System.Configuration.ConfigurationSettings.AppSettings["healthWatchURL"] + "/img/partner/" + r.GetInt32(1) + ".gif\"/>");
					sb.Append("<img src=\"" + ConfigurationSettings.AppSettings["healthWatchURL"] + "/img/partner/" + s.SuperSponsor.Id + ".gif\"/>");
				}
			}
			else
			{
				sb.Append("HealthWatch.se");
				sb.Append("<br />");
				sb.Append("Group administration");
				sb.Append("<br />");
			}
//			r.Close();
		}
		else
		{
			sb.Append("HealthWatch.se");
			sb.Append("<br />");
			sb.Append("Group administration");
			sb.Append("<br />");
		}
		//sb.Append("<a href=\"#\" class=\"regular\"> På Svenska</a>");
		sb.Append("</div>");

		//string ret = "" +
		//    //"<!--SAID:" + HttpContext.Current.Session["SponsorAdminID"] + ",SID:" + HttpContext.Current.Session["SponsorID"] + "-->" +
		//    "<div id=\"main\">";

		//ret += "<table class=\"noprint\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
		//ret += "<tr>";
		//ret += "<td><img src=\"img/null.gif\" width=\"150\" height=\"125\" border=\"0\"" +
		//    //"USEMAP=\"#top\"><MAP NAME=\"top\"><AREA SHAPE=\"poly\" COORDS=\"11,14, 11,90, 181,88, 179,16\" HREF=\"default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"></MAP" +
		//    "></td>";
		//ret += "<td><img src=\"img/null.gif\" width=\"25\" height=\"1\"></td>";
		//ret += "<td valign=\"top\"><br/>";
		if (HttpContext.Current.Request.Url.AbsolutePath.IndexOf("super") < 0 && HttpContext.Current.Session["SponsorID"] != null || HttpContext.Current.Request.Url.AbsolutePath.IndexOf("super") >= 0 && HttpContext.Current.Session["SuperAdminID"] != null)
		{
			sb.Append("<div class=\"logincontainer grid_5 alpha omega\">");
			sb.Append("<div class=\"gears\">");
			sb.Append("<span>" + HttpContext.Current.Session["Name"] + "</span><span style=\"color:white;\">" + (HttpContext.Current.Session["SponsorAdminID"] != null ? HttpContext.Current.Session["SponsorAdminID"].ToString() : "") + "</span><br />");
			sb.Append("<a href=\"settings.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\" class=\"regular\">Settings</a>&nbsp;&nbsp;");
			if (HttpContext.Current.Request.Url.AbsolutePath.IndexOf("super") < 0 && HttpContext.Current.Session["SponsorID"] != null)
			{
				sb.Append("<a href=\"default.aspx?Logout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\" class=\"regular\">Log out</a>");
			}
			else if (HttpContext.Current.Request.Url.AbsolutePath.IndexOf("super") >= 0 && HttpContext.Current.Session["SuperAdminID"] != null)
			{
				sb.Append("<a href=\"default.aspx?SuperLogout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\" class=\"regular\">Log out</a>");
				//ret += "<A class=\"unli\" HREF=\"default.aspx?SuperLogout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><img src=\"img/logout.gif\" border=\"0\"/>Log out</A><br/>";
			}
			sb.Append("</div>");
			sb.Append("</div>");

			sb.Append("<div id=\"submenu\" class=\"grid_16 alpha\">");
			if (HttpContext.Current.Request.Url.AbsolutePath.IndexOf("super") < 0 && HttpContext.Current.Session["SponsorID"] != null)
			{
				string desc = "";
//				SqlDataReader r = Db.rs("SELECT " +
//				                        "mf.ManagerFunction, " +
//				                        "mf.URL, " +
//				                        "mf.Expl " +
//				                        "FROM ManagerFunction mf " +
//				                        (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//				                         "INNER JOIN SponsorAdminFunction s ON s.ManagerFunctionID = mf.ManagerFunctionID " +
//				                         "WHERE s.SponsorAdminID = " + HttpContext.Current.Session["SponsorAdminID"] : ""));
//				while (r.Read())
				int sponsorAdminID = HttpContext.Current.Session["SponsorAdminID"] != null ? Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) : -1;
				foreach (var f in managerFunctionRepository.FindBySponsorAdmin(sponsorAdminID))
				{
//					bool active = HttpContext.Current.Request.Url.AbsolutePath.IndexOf(r.GetString(1)) >= 0;
					bool active = HttpContext.Current.Request.Url.AbsolutePath.IndexOf(f.URL) >= 0;

//					sb.Append("<a title=\"" + r.GetString(2) + "\" " + (active ? "class=\"active\"" : "") + " href=\"" + r.GetString(1) + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + r.GetString(0) + "</a>");
					sb.Append("<a title=\"" + f.Expl + "\" " + (active ? "class=\"active\"" : "") + " href=\"" + f.URL + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + f.Function + "</a>");
					
					if (active)
					{
//						desc = r.GetString(2);
						desc = f.Expl;
					}
				}
//				r.Close();
				sb.Append("<div class=\"description\">" + desc + "</div>");
				//ret += "<A class=\"unli\" HREF=\"default.aspx?Logout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><img src=\"img/logout.gif\" border=\"0\"/>Log out</A><br/>";
			}
			sb.Append("</div>");
		}
		else
		{
			sb.Append("<br/><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
			sb.Append("<tr>");
			sb.Append("<td>Username&nbsp;</td>");
			sb.Append("<td><input type=\"text\" name=\"ANV\">&nbsp;</td>");
			sb.Append("</tr>");
			sb.Append("<tr>");
			sb.Append("<td>Password&nbsp;</td>");
			sb.Append("<td><input type=\"password\" name=\"LOS\">&nbsp;</td>");
			sb.Append("<td><input type=\"submit\" value=\"OK\"></td>");
			sb.Append("</tr>");
			sb.Append("</table>");
		}

		sb.Append("</div> <!-- end .headergroup -->");

		//ret += "</td>";
		//ret += "</tr>";
		//ret += "</table>";
		
		//ret += "<div id=\"container\">";
		return sb.ToString();
	}

//	public static bool isEmail(string inputEmail)
//	{
//		string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
//			@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
//			@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
//		Regex re = new Regex(strRegex);
//		if (re.IsMatch(inputEmail))
//			return true;
//		else
//			return false;
//	}
}
