using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HWgrp___Old
{
	public partial class superstats : System.Web.UI.Page
	{
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			int cx = 0;
			SqlDataReader rs = Db.rs("SELECT " +
				"rp.ReportPartID, " +
				"rpl.Subject, " +
				"rpl.Header, " +
				"rpl.Footer, " +
				"rp.Type " +
				"FROM Report r " +
				"INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
				"INNER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = 1 " +
				"WHERE r.ReportID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["RID"]) + " " +
				"ORDER BY rp.SortOrder", "eFormSqlConnection");
			while (rs.Read())
			{
				StatsImg.Text += "<div" + (cx > 0 ? " style=\"page-break-before:always;\"" : "") + ">&nbsp;<br/>&nbsp;<br/></div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
				StatsImg.Text += "<tr class=\"noscreen\"><td align=\"center\" valign=\"middle\" background=\"img/top_healthWatch.jpg\" height=\"140\" style=\"font-size:24px;\">" + rs.GetString(1) + "</td></tr>";
				StatsImg.Text += "<tr class=\"noprint\"><td style=\"font-size:18px;\">" + rs.GetString(1) + "</td></tr>";

				if (!rs.IsDBNull(2) && rs.GetString(2) != "")
					StatsImg.Text += "<tr><td>" + rs.GetString(2).Replace("\r", "").Replace("\n", "<br/>") + "</td></tr>";

				StatsImg.Text += "<tr><td><img src=\"superReportImage.aspx?" +
					"N=" + (HttpContext.Current.Request.QueryString["N"] != null ? HttpContext.Current.Request.QueryString["N"] : "") + "&" +
					"FDT=" + (HttpContext.Current.Request.QueryString["FDT"] != null ? HttpContext.Current.Request.QueryString["FDT"] : "") + "&" +
					"TDT=" + (HttpContext.Current.Request.QueryString["TDT"] != null ? HttpContext.Current.Request.QueryString["TDT"] : "") + "&" +
					"RNDS1=" + (HttpContext.Current.Request.QueryString["RNDS1"] != null ? HttpContext.Current.Request.QueryString["RNDS1"] : "") + "&" +
					"RNDSD1=" + (HttpContext.Current.Request.QueryString["RNDSD1"] != null ? HttpContext.Current.Request.QueryString["RNDSD1"] : "") + "&" +
					"PID1=" + (HttpContext.Current.Request.QueryString["PID1"] != null ? HttpContext.Current.Request.QueryString["PID1"] : "") + "&" +
					"RNDS2=" + (HttpContext.Current.Request.QueryString["RNDS2"] != null ? HttpContext.Current.Request.QueryString["RNDS2"] : "") + "&" +
					"RNDSD2=" + (HttpContext.Current.Request.QueryString["RNDSD2"] != null ? HttpContext.Current.Request.QueryString["RNDSD2"] : "") + "&" +
					"PID2=" + (HttpContext.Current.Request.QueryString["PID2"] != null ? HttpContext.Current.Request.QueryString["PID2"] : "") + "&" +
					"R1=" + (HttpContext.Current.Request.QueryString["R1"] != null ? HttpContext.Current.Request.QueryString["R1"] : "") + "&" +
					"R2=" + (HttpContext.Current.Request.QueryString["R2"] != null ? HttpContext.Current.Request.QueryString["R2"] : "") + "&" +
					"RPID=" + rs.GetInt32(0) +
					"\"/></td></tr>";

				if (!rs.IsDBNull(3) && rs.GetString(3) != "")
					StatsImg.Text += "<tr><td>" + rs.GetString(3).Replace("\r", "").Replace("\n", "<br/>") + "</td></tr>";

				StatsImg.Text += "</table>";

				cx++;
			}
			rs.Close();
		}
	}
}