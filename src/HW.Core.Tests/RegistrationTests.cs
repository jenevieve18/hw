// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using HW.Core.FromHW;
using NUnit.Framework;

namespace HW.Core.Tests
{
	[TestFixture]
	public class RegistrationTests
	{
		[Test]
		public void TestMethod()
		{
			var session = new Dictionary<string, object>();
			session["UserID"] = null;
			//lalala(session, "D322060F10971");
			lalala(session, "84A0D0B510981");
		}
		
		void lalala(Dictionary<string, object> session, string invite)
		{
			if (Convert.ToInt32(session["UserID"]) != 0) {
				Db.exec("UPDATE [Session] SET EndDT = GETDATE() WHERE EndDT IS NULL AND SessionID = " + Convert.ToInt32(session["SessionID"]));
//				session.Abandon();
			}
			session["SponsorInviteID"] = -1;
			string url = "/register.aspx";
			try {
//				string invite = HttpContext.Current.Request.RawUrl.Substring(HttpContext.Current.Request.RawUrl.IndexOf("/i/") + 3);
				SqlDataReader rs = Db.rs("SELECT " +
				                         "i.SponsorInviteID, " +     // 0
				                         "i.SponsorID, " +
				                         "i.DepartmentID, " +
				                         "LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8), " +
				                         "i.Email, " +
				                         "s.LID, " +                 // 5
				                         "s.InfoText, " +
				                         "s.ConsentText, " +
				                         "s.ForceLID " +
				                         "FROM SponsorInvite i " +
				                         "INNER JOIN Sponsor s ON i.SponsorID = s.SponsorID " +
				                         "WHERE i.SponsorInviteID = " + Convert.ToInt32(invite.Substring(8)) + " " +
				                         "AND LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),i.InvitationKey),'-',''),8)) = '" + invite.Substring(0, 8).Replace("'", "").ToLower() + "'");
				if (rs.Read())
				{
					if (!rs.IsDBNull(5) || !rs.IsDBNull(8))
					{
						session["LID"] = (rs.IsDBNull(8) ? rs.GetInt32(5) : rs.GetInt32(8));
//						HttpContext.Current.Response.Cookies["HW"]["LID"] = (rs.IsDBNull(8) ? rs.GetInt32(5) : rs.GetInt32(8)).ToString();
//						HttpContext.Current.Response.Cookies["HW"].Expires = DateTime.Now.AddYears(1);
					}
					if (!rs.IsDBNull(8))
					{
						session["ForceLID"] = rs.GetInt32(8);
					}
					session["SponsorInviteID"] = rs.GetInt32(0);
					Db.loadSponsor(rs.GetInt32(1).ToString(), rs.GetString(3));
					session["DepartmentID"] = (rs.IsDBNull(2) ? "NULL" : rs.GetInt32(2).ToString());
					session["Email"] = (rs.IsDBNull(4) ? "" : rs.GetString(4));
					if (!rs.IsDBNull(6))
					{
						url = "/sponsorInformation.aspx";
					}
					else if (!rs.IsDBNull(7))
					{
						url = "/sponsorConsent.aspx";
					}
				}
				rs.Close();
			}
			catch (Exception) { }
//			HttpContext.Current.Response.Redirect(url + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			Process.Start("http://localhost:47375" + url + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next());
		}
	}
}
