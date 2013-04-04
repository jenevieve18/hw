//	<file>
//		<license></license>
//		<owner name="Jens Pettersson" email=""/>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HW.Core;
using HW.Core.Repositories;

namespace HWgrp
{
	public partial class superstats : System.Web.UI.Page
	{
		IReportRepository reportRepository = AppContext.GetRepositoryFactory().CreateReportRepository();
		
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			int cx = 0;
			int reportID = Convert.ToInt32(HttpContext.Current.Request.QueryString["RID"]);
			foreach (var l in reportRepository.FindPartLanguagesByReport(reportID))
			{
				StatsImg.Text += "<div" + (cx > 0 ? " style=\"page-break-before:always;\"" : "") + ">&nbsp;<br/>&nbsp;<br/></div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
				StatsImg.Text += "<tr class=\"noscreen\"><td align=\"center\" valign=\"middle\" background=\"img/top_healthWatch.jpg\" height=\"140\" style=\"font-size:24px;\">" + l.Subject + "</td></tr>";
				StatsImg.Text += "<tr class=\"noprint\"><td style=\"font-size:18px;\">" + l.Subject + "</td></tr>";

				if (l.Header != "") {
					StatsImg.Text += "<tr><td>" + l.Header.Replace("\r", "").Replace("\n", "<br/>") + "</td></tr>";
				}
				
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
					"RPID=" + l.ReportPart.Id +
					"\"/></td></tr>";

				if (l.Footer != "") {
					StatsImg.Text += "<tr><td>" + l.Footer.Replace("\r", "").Replace("\n", "<br/>") + "</td></tr>";
				}

				StatsImg.Text += "</table>";

				cx++;
			}
		}
	}
}