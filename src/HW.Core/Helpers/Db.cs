﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using HW.Core.Repositories.Sql;

namespace HW.Core.Helpers
{
	public class Db
	{
        public static int createProjectRoundUnit(int parentProjectRoundUnitID, string name, int SID, int individualReportID, int reportID)
        {
            int ID = 0;

            SqlDataReader rs = Db.rs("SELECT ProjectRoundID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + parentProjectRoundUnitID, "eFormSqlConnection");
            if (rs.Read())
            {
                Db.exec("INSERT INTO ProjectRoundUnit (" +
                    "UserCount," +
                    "LangID," +
                    "SurveyID," +
                    "ProjectRoundID," +
                    "Unit," +
                    "ParentProjectRoundUnitID," +
                    "IndividualReportID," +
                    "ReportID" +
                    ") VALUES (" +
                    "0," +
                    "0," +
                    "" + SID + "," +
                    "" + rs.GetInt32(0) + "," +
                    "'" + name.Replace("'", "''") + "'," +
                    "" + parentProjectRoundUnitID + "," +
                    "" + individualReportID + "," +
                    "" + reportID + "" +
                    ")", "eFormSqlConnection");
                SqlDataReader rs2 = Db.rs("SELECT ProjectRoundUnitID FROM [ProjectRoundUnit] " +
                    "WHERE ProjectRoundID=" + rs.GetInt32(0) + " AND Unit = '" + name.Replace("'", "''") + "' " +
                    "ORDER BY ProjectRoundUnitID DESC", "eFormSqlConnection");
                if (rs2.Read())
                {
                    ID = rs2.GetInt32(0);
                }
                rs2.Close();
            }
            rs.Close();

            Db.exec("UPDATE ProjectRoundUnit " +
                "SET ID = dbo.cf_unitExtID(" + ID + ",dbo.cf_unitDepth(" + ID + "),''), " +
                "SortOrder = " + ID + " " +
                "WHERE ProjectRoundUnitID = " + ID, "eFormSqlConnection");

            Db.exec("UPDATE ProjectRoundUnit SET SortString = dbo.cf_unitSortString(ProjectRoundUnitID) WHERE ProjectRoundUnitID = " + ID, "eFormSqlConnection");

            return ID;
        }

        public static int getInt32(string sqlString)
        {
            return getInt32(sqlString, "SqlConnection");
        }

        public static int getInt32(string sqlString, string con)
        {
            int returnValue = 0;
            SqlConnection dataConnection = new SqlConnection(ConfigurationSettings.AppSettings[con]);
            dataConnection.Open();
            SqlCommand dataCommand = new SqlCommand(sqlString.Replace("\\", "\\\\"), dataConnection);
            dataCommand.CommandTimeout = 900;
            SqlDataReader dataReader = dataCommand.ExecuteReader();
            if (dataReader.Read())
                if (!dataReader.IsDBNull(0))
                    returnValue = Convert.ToInt32(dataReader.GetValue(0));
            dataReader.Close();
            dataConnection.Close();
            dataConnection.Dispose();
            return returnValue;
        }

        public static string HashMD5(string str)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashByteArray = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes("HW" + str + "HW"));
            string hash = "";
            for (int i = 0; i < hashByteArray.Length; i++)
                hash += hashByteArray[i];
            return hash;
        }

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

		public static string header()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("<title>HealthWatch.se / Group admin</title>");
			//sb.Append("<link href=\"main.css?V=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\" rel=\"stylesheet\" type=\"text/css\">");
			sb.Append("<meta http-equiv=\"Pragma\" content=\"no-cache\">");
			sb.Append("<meta http-equiv=\"Expires\" content=\"-1\">");
			sb.Append("<meta name=\"Robots\" content=\"noarchive\">");
			sb.Append("<script language=\"JavaScript\">window.history.forward(1);</script>");

			sb.Append("<meta charset=\"utf-8\"/>");
			sb.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">");
			sb.Append("<!--<meta name=\"viewport\" content=\"width=device-width; initial-scale=1.0; maximum-scale=1.0;\">-->");
			//sb.Append("<link rel=\"shortcut icon\" href=\"favicon.ico\">");
			//sb.Append("<link rel=\"apple-touch-icon\" href=\"apple-touch-icon.png\">");
			sb.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"includes/css/960.css\">");
			sb.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"includes/css/site.css\" />");
			sb.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"includes/css/admin.css\">");
			sb.Append("<link type=\"text/css\" href=\"includes/ui/css/ui-lightness/jquery-ui-1.8.11.custom.css\" rel=\"Stylesheet\" />");
			sb.Append("<script type=\"text/javascript\" src=\"includes/ui/js/jquery-1.5.1.min.js\"></script>");
			sb.Append("<script type=\"text/javascript\" src=\"includes/ui/js/jquery-ui-1.8.11.custom.min.js\"></script>");
			sb.Append("<script type=\"text/javascript\">");
			sb.Append("$(document).ready(function() {");
			sb.Append("var descriptionS = $(\"#submenu .description\").html();");
			sb.Append("$(\"#submenu a\").mouseover(function() {");
			sb.Append("$(\"#submenu .description\").html($(this).html());");
			sb.Append("$(\"#submenu .active\").css('background-position', 'center -80px');");
			sb.Append("});");
			sb.Append("$(\"#submenu a\").mouseout(function () {");
			sb.Append("$(\"#submenu .description\").html(descriptionS);");
			sb.Append("$(\"#submenu .active\").css('background-position', 'center -120px');");
			sb.Append("});");
			sb.Append("});");
			sb.Append("</script>");

			return sb.ToString();
		}
		
		public static string header2()
		{
			string ret = "<TITLE>HealthWatch</TITLE>";
			ret += "<link href=\"main.css?V=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\" rel=\"stylesheet\" type=\"text/css\">";
			ret += "<meta http-equiv=\"Pragma\" content=\"no-cache\">";
			ret += "<meta http-equiv=\"Expires\" content=\"-1\">";
			ret += "<meta name=\"Robots\" content=\"noarchive\">";
			ret += "<script language=\"JavaScript\">window.history.forward(1);</script>";

			return ret;
		}

		public static bool sendMail(string email, string body, string subject)
		{
			return sendMail("reminder@healthwatch.se", email, body, subject);
		}
		
		public static bool sendMail(string from, string email, string body, string subject)
		{
			bool success = false;
			try {
//				System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
//				System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
//				mail.To = email;
//				mail.From = from;
//				mail.Subject = subject;
//				mail.Body = body;
//				System.Web.Mail.SmtpMail.Send(mail);
//				success = true;
				string server = ConfigurationSettings.AppSettings["SmtpServer"];
				System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(from, email, subject, body);
				System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(server);
				client.Send(mail);
				success = true;
			} catch (Exception) {
			}
			return success;
		}

		public static bool sendInvitation(int sponsorInviteID, string email, string body, string subject, string key)
		{
			bool success = false;

			if (Db.isEmail(email))
			{
				try {
					if (body.IndexOf("<LINK/>") >= 0) {
//						string path = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
						string path = ConfigurationSettings.AppSettings["healthWatchURL"];
						body = body.Replace("<LINK/>", "" + path + "i/" + key + sponsorInviteID.ToString());
					} else {
//						string path = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
						string path = ConfigurationSettings.AppSettings["healthWatchURL"];
						body += "\r\n\r\n" + "" + path + "i/" + key + sponsorInviteID.ToString();
					}
//					System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
//					System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
//					mail.To = email;
//					mail.From = "info@healthwatch.se";
//					mail.Subject = subject;
//					mail.Body = body;
//					System.Web.Mail.SmtpMail.Send(mail);
					string server = ConfigurationSettings.AppSettings["SmtpServer"];
					System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage("info@healthwatch.se", email, subject, body);
					System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(server);
					client.Send(mail);

					Db.exec("UPDATE SponsorInvite SET Sent = GETDATE() WHERE SponsorInviteID = " + sponsorInviteID);

					success = true;
				} catch (Exception) {
				}
			}
			return success;
		}

		public static string bottom()
		{
			return ""; // "</div></div>";
		}
		
		static SqlManagerFunctionRepository managerFunctionRepository = new SqlManagerFunctionRepository();
		
		public static string nav2()
		{
			SqlDataReader r;

			string ret = "<div id=\"main\">";

			ret += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
			ret += "<tr>";
			ret += "<td><img src=\"img/null.gif\" width=\"150\" height=\"125\" border=\"0\" USEMAP=\"#top\"><MAP NAME=\"top\"><AREA SHAPE=\"poly\" COORDS=\"11,14, 11,90, 181,88, 179,16\" HREF=\"default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"></MAP></td>";
			ret += "<td><img src=\"img/null.gif\" width=\"25\" height=\"1\"></td>";
			ret += "<td>" +
				"<A class=\"unli\" HREF=\"news.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">News</A><br/>" +
				"<A class=\"unli\" HREF=\"rss.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">News setup</A><br/>" +
				"<A class=\"unli\" HREF=\"sponsor.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Sponsors</A><br/>" +
				"<A class=\"unli\" HREF=\"grpUser.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Sponsor managers</A><br/>" +
				"<A class=\"unli\" HREF=\"sponsorStats.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Sponsor statistics</A><br/>" +
				"<A class=\"unli\" HREF=\"superAdmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Super managers</A><br/>" +
				"</td>";
			ret += "<td><img src=\"img/null.gif\" width=\"25\" height=\"1\"></td>";
			ret += "<td>" +
				"<A class=\"unli\" HREF=\"bq.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Background questions</A><br/>" +
				"<A class=\"unli\" HREF=\"messages.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Messages</A><br/>" +
				"<A class=\"unli\" HREF=\"export.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Data export</A><br/>" +
				"<A class=\"unli\" HREF=\"stats.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Usage statistics</A><br/>" +
				"<A class=\"unli\" HREF=\"tx.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">File uploads</A><br/>" +
				"<br/>" +
				"</td>";
			ret += "<td><img src=\"img/null.gif\" width=\"25\" height=\"1\"></td>";
			ret += "<td>" +
				"<A class=\"unli\" HREF=\"exercise.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Exercises</A><br/>" +
				"<A class=\"unli\" HREF=\"users.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Users</A><br/>" +
				"<A class=\"unli\" HREF=\"extendedSurvey.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Extended survey statistics</A><br/>" +
				"<A class=\"unli\" HREF=\"survey.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Survey statistics</A><br/>" +
				"<A class=\"unli\" HREF=\"wise.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Words of wisdom</A><br/>" +
				"<br/>" +
				"</td>";
			ret += "</tr>";
			ret += "</table>";
		   
			ret += "<div id=\"container\">";

			return ret;
		}

		public static string nav()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("<div class=\"headergroup grid_16\">");
			sb.Append("<div class=\"grid_3 alpha\">");
			sb.Append("<img src=\"images/hwlogo.png\" width=\"186\" height=\"126\" alt=\"Hwlogo\">");
			sb.Append("</div>");
			sb.Append("<div class=\"grid_8 omega p2\">");
			sb.Append("HealthWatch.se");
			sb.Append("<br />");
			sb.Append("Group Admin");
			sb.Append("<br />");
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
			if (HttpContext.Current.Request.Url.AbsolutePath.IndexOf("super") < 0 && HttpContext.Current.Session["SponsorID"] != null || HttpContext.Current.Request.Url.AbsolutePath.IndexOf("super") >= 0 && HttpContext.Current.Session["SuperAdminID"] != null) {
				sb.Append("<div class=\"logincontainer grid_5 alpha omega\">");
				sb.Append("<div class=\"gears\">");
				sb.Append("<span>" + HttpContext.Current.Session["Name"] + "</span><br />");
				if (HttpContext.Current.Request.Url.AbsolutePath.IndexOf("super") < 0 && HttpContext.Current.Session["SponsorID"] != null) {
					sb.Append("<a href=\"default.aspx?Logout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\" class=\"regular\">Log out</a>");
				} else if (HttpContext.Current.Request.Url.AbsolutePath.IndexOf("super") >= 0 && HttpContext.Current.Session["SuperAdminID"] != null) {
					sb.Append("<a href=\"default.aspx?SuperLogout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\" class=\"regular\">Log out</a>");
					//ret += "<A class=\"unli\" HREF=\"default.aspx?SuperLogout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><img src=\"img/logout.gif\" border=\"0\"/>Log out</A><br/>";
				}
				sb.Append("</div>");
				sb.Append("</div>");

				if (HttpContext.Current.Request.Url.AbsolutePath.IndexOf("super") < 0 && HttpContext.Current.Session["SponsorID"] != null) {
					sb.Append("<div id=\"submenu\" class=\"grid_16 alpha\">");
					string desc = "";
//					SqlDataReader r = Db.rs("SELECT " +
//					                        "mf.ManagerFunction, " +
//					                        "mf.URL, " +
//					                        "mf.Expl " +
//					                        "FROM ManagerFunction mf " +
//					                        (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
//					                         "INNER JOIN SponsorAdminFunction s ON s.ManagerFunctionID = mf.ManagerFunctionID " +
//					                         "WHERE s.SponsorAdminID = " + HttpContext.Current.Session["SponsorAdminID"] : ""));
//					while (r.Read())
					int sponsorAdminID = HttpContext.Current.Session["SponsorAdminID"] != null ? Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) : -1;
					foreach (var f in managerFunctionRepository.FindBySponsorAdmin(sponsorAdminID)) {
//						bool active = HttpContext.Current.Request.Url.AbsolutePath.IndexOf(r.GetString(1)) >= 0;
						bool active = HttpContext.Current.Request.Url.AbsolutePath.IndexOf(f.URL) >= 0;

//						sb.Append("<a title=\"" + r.GetString(2) + "\" " + (active ? "class=\"active\"" : "") + " href=\"" + r.GetString(1) + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + r.GetString(0) + "</a>");
						sb.Append("<a title=\"" + f.Expl + "\" " + (active ? "class=\"active\"" : "") + " href=\"" + f.URL + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + f.Function + "</a>");
						
						if (active) {
//							desc = r.GetString(2);
							desc = f.Expl;
						}
					}
//					r.Close();
					sb.Append("<div class=\"description\">" + desc + "</div>");
					sb.Append("</div>");
					//ret += "<A class=\"unli\" HREF=\"default.aspx?Logout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><img src=\"img/logout.gif\" border=\"0\"/>Log out</A><br/>";
				}
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

		public static bool isEmail(string inputEmail)
		{
			string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
				@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
				@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
			Regex re = new Regex(strRegex);
			if (re.IsMatch(inputEmail)) {
				return true;
			} else {
				return false;
			}
		}
	}
}
