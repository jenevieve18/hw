using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using HW.Core.Helpers;

namespace HW.Grp
{
	public partial class SuperStats : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			int cx = 0;
			string query = string.Format(
				@"
SELECT rp.ReportPartID, 
	rpl.Subject,
	rpl.Header,
	rpl.Footer,
	rp.Type
FROM Report r 
INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID 
INNER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = 1 
WHERE r.ReportID = {0} 
ORDER BY rp.SortOrder",
				Convert.ToInt32(Request.QueryString["RID"])
			);
			SqlDataReader rs = Db.rs(query, "eFormSqlConnection");
			while (rs.Read()) {
				StatsImg.Text += "<div" + (cx > 0 ? " style=\"page-break-before:always;\"" : "") + ">&nbsp;<br/>&nbsp;<br/></div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
				StatsImg.Text += "<tr class=\"noscreen\"><td align=\"center\" valign=\"middle\" background=\"img/top_healthWatch.jpg\" height=\"140\" style=\"font-size:24px;\">" + rs.GetString(1) + "</td></tr>";
				StatsImg.Text += "<tr class=\"noprint\"><td style=\"font-size:18px;\">" + rs.GetString(1) + "</td></tr>";

				if (!rs.IsDBNull(2) && rs.GetString(2) != "") {
					StatsImg.Text += "<tr><td>" + rs.GetString(2).Replace("\r", "").Replace("\n", "<br/>") + "</td></tr>";
				}

				StatsImg.Text += "<tr><td><img src=\"superReportImage.aspx?" +
					"N=" + (Request.QueryString["N"] != null ? Request.QueryString["N"] : "") + "&" +
					"FDT=" + (Request.QueryString["FDT"] != null ? Request.QueryString["FDT"] : "") + "&" +
					"TDT=" + (Request.QueryString["TDT"] != null ? Request.QueryString["TDT"] : "") + "&" +
					"RNDS1=" + (Request.QueryString["RNDS1"] != null ? Request.QueryString["RNDS1"] : "") + "&" +
					"RNDSD1=" + (Request.QueryString["RNDSD1"] != null ? Request.QueryString["RNDSD1"] : "") + "&" +
					"PID1=" + (Request.QueryString["PID1"] != null ? Request.QueryString["PID1"] : "") + "&" +
					"RNDS2=" + (Request.QueryString["RNDS2"] != null ? Request.QueryString["RNDS2"] : "") + "&" +
					"RNDSD2=" + (Request.QueryString["RNDSD2"] != null ? Request.QueryString["RNDSD2"] : "") + "&" +
					"PID2=" + (Request.QueryString["PID2"] != null ? Request.QueryString["PID2"] : "") + "&" +
					"R1=" + (Request.QueryString["R1"] != null ? Request.QueryString["R1"] : "") + "&" +
					"R2=" + (Request.QueryString["R2"] != null ? Request.QueryString["R2"] : "") + "&" +
					"RPID=" + rs.GetInt32(0) +
					"\"/></td></tr>";

				if (!rs.IsDBNull(3) && rs.GetString(3) != "") {
					StatsImg.Text += "<tr><td>" + rs.GetString(3).Replace("\r", "").Replace("\n", "<br/>") + "</td></tr>";
				}

				StatsImg.Text += "</table>";

				cx++;
			}
			rs.Close();
		}
	}
}