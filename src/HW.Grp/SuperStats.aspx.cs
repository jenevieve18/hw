﻿using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class SuperStats : System.Web.UI.Page
	{
		SqlReportRepository reportRepository = new SqlReportRepository();
        protected int lid;
		
		protected void Page_Load(object sender, EventArgs e)
        {
            lid = ConvertHelper.ToInt32(Session["lid"], 1);
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			int cx = 0;
			foreach (var p in reportRepository.FindPartLanguagesByReport(Convert.ToInt32(Request.QueryString["RID"]))) {
				StatsImg.Text += string.Format(
					@"
<div{0}>&nbsp;<br/>&nbsp;<br/></div>
<table border='0' cellspacing='0' cellpadding='0'>",
					(cx > 0 ? " style='page-break-before:always;'" : "")
				);
				StatsImg.Text += string.Format(
					@"
	<tr class='noscreen'>
		<td align='center' valign='middle' background='img/top_healthWatch.jpg' height='140' style='font-size:24px;'>{0}</td>
	</tr>",
					p.Subject
				);
				StatsImg.Text += string.Format(
					@"
	<tr class='noprint'>
		<td style='font-size:18px;'>{0}</td>
	</tr>",
					p.Subject
				);
				if (p.Header != "") {
					StatsImg.Text += string.Format(
						@"
	<tr>
		<td>{0}</td>
	</tr>",
						p.Header.Replace("\r", "").Replace("\n", "<br/>")
					);
				}

				StatsImg.Text += string.Format(
					@"
	<tr>
		<td>
			<img src='superReportImage.aspx?N={0}&FDT={1}&TDT={2}&RNDS1={3}&RNDSD1={4}&PID1={5}&RNDS2={6}&RNDSD2={7}&PID2={8}&R1={9}&R2={10}&RPID={11}'/>
		</td>
	</tr>",
					(Request.QueryString["N"] != null ? Request.QueryString["N"] : ""),
					(Request.QueryString["FDT"] != null ? Request.QueryString["FDT"] : ""),
					(Request.QueryString["TDT"] != null ? Request.QueryString["TDT"] : ""),
					(Request.QueryString["RNDS1"] != null ? Request.QueryString["RNDS1"] : ""),
					(Request.QueryString["RNDSD1"] != null ? Request.QueryString["RNDSD1"] : ""),
					(Request.QueryString["PID1"] != null ? Request.QueryString["PID1"] : ""),
					(Request.QueryString["RNDS2"] != null ? Request.QueryString["RNDS2"] : ""),
					(Request.QueryString["RNDSD2"] != null ? Request.QueryString["RNDSD2"] : ""),
					(Request.QueryString["PID2"] != null ? Request.QueryString["PID2"] : ""),
					(Request.QueryString["R1"] != null ? Request.QueryString["R1"] : ""),
					(Request.QueryString["R2"] != null ? Request.QueryString["R2"] : ""),
					p.ReportPart.Id
				);

				if (p.Footer != "") {
					StatsImg.Text += string.Format(
						@"
	<tr>
		<td>{0}</td>
	</tr>",
						p.Footer.Replace("\r", "").Replace("\n", "<br/>")
					);
				}

				StatsImg.Text += @"
</table>";
				cx++;
			}
		}
	}
}