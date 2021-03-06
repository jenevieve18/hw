﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Util.Exporters;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using System.Configuration;

namespace HW.Grp
{
	public partial class Export : System.Web.UI.Page
	{
		ReportService service = new ReportService();
//		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);
		
		bool HasAnswerKey {
			get { return Request.QueryString["AK"] != null; }
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
            string token = (Request.QueryString["Token"] != null ? Request.QueryString["Token"] : "");

            if (token.Length != 36)
            {
                return;
            }

            int groupBy = (Request.QueryString["GB"] != null ? Convert.ToInt32(Request.QueryString["GB"].ToString()) : 0);

            int fromYear = Convert.ToInt32(Request.QueryString["FY"]);
            int fromMonth = Convert.ToInt32(Request.QueryString["FM"]);
            int toYear = Convert.ToInt32(Request.QueryString["TY"]);
            int toMonth = Convert.ToInt32(Request.QueryString["TM"]);

            int langID = (Request.QueryString["LangID"] != null ? Convert.ToInt32(Request.QueryString["LangID"]) : 0);

            int reportPartID = Convert.ToInt32(Request.QueryString["RPID"]);
            string project = Request.QueryString["PRUID"];

            int grouping = Convert.ToInt32(Request.QueryString["GRPNG"]);
            int sponsorAdminID = Convert.ToInt32((Request.QueryString["SAID"] != null ? Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
            int sponsorID = Convert.ToInt32((Request.QueryString["SID"] != null ? Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
            string departmentIDs = (Request.QueryString["GID"] != null ? Request.QueryString["GID"].ToString().Replace(" ", "") : "");
            int plot = ConvertHelper.ToInt32(Request.QueryString["PLOT"]);
            string type = StrHelper.Str3(Request.QueryString["TYPE"], "");

            bool hasGrouping = Request.QueryString["GRPNG"] != null || Request.QueryString["GRPNG"] != "0";
            //			string key = Request.QueryString["AK"];

            object disabled = Request.QueryString["DISABLED"];

            var grpWSUrl = ConfigurationManager.AppSettings["grpWSUrl"].ToString();

            var grpWS = new HW.Grp.WebService.Soap();

            var responseWS = grpWS.Export(token, groupBy, fromYear, toYear, fromMonth, toMonth, langID, project, grouping, sponsorAdminID, sponsorID, departmentIDs, reportPartID, plot, type, HasAnswerKey, grpWSUrl, 20);
            var objectData = new MemoryStream(Convert.FromBase64String(responseWS.Base64StreamData));
            var content = responseWS.Content;

            byte[] bytes = objectData.ToArray();
            objectData.Close();


            Response.Clear();
            Response.ContentType = responseWS.ContentType;
            Response.AddHeader("content-disposition", content);
            Response.BinaryWrite(bytes);
            Response.End();

            ////////////			IAdmin sponsor = service.ReadSponsor(sponsorID);
            //////////////			ReportPart reportPart = service.ReadReportPart(reportPartID, langID);

            //////////////			var reportService = new ReportService3();
            //////////////			var reportPart = reportService.ReadReportPart(reportPartID);
            ////////////			var reportPart = service.ReadReportPart(reportPartID);
            ////////////            reportPart.SelectedReportPartLangID = langID;

            ////////////			ProjectRoundUnit projectRoundUnit = service.ReadProjectRoundUnit(projectRoundUnitID);
            ////////////			var sponsorAdmin = service.ReadSponsorAdmin(sponsorAdminID);

            ////////////            var exporter = ExportFactory.GetExporter(service, type, HasAnswerKey, hasGrouping, reportPart, Server.MapPath("HW template for Word.docx"));
            ////////////			exporter.CellWrite += delegate(object sender2, ExcelCellEventArgs e2) {
            //////////////				e2.ExcelCell.Value = R.Str(lid, e2.ValueKey, "");
            ////////////				e2.ExcelCell.Value = R.Str(langID, e2.ValueKey, "");
            ////////////				e2.Writer.WriteCell(e2.ExcelCell);
            ////////////			};

            ////////////			Response.ClearHeaders();
            ////////////			Response.ClearContent();
            ////////////			Response.ContentType = exporter.Type;
            ////////////            HtmlHelper.AddHeaderIf(exporter.HasContentDisposition(reportPart.SelectedReportPartLang.Subject), "content-disposition", exporter.GetContentDisposition(reportPart.SelectedReportPartLang.Subject), Response);
            ////////////			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;

            //////////////			string url = GetReportImageUrl(path, langID, yearFrom, yearTo, sponsorAdminID, sponsorID, groupBy, reportPart.Id, projectRoundUnitID, departmentIDs, grouping, plot, monthFrom, monthTo);
            //////////////			HtmlHelper.Write(exporter.Export(url, langID, projectRoundUnitID, yearFrom, yearTo, groupBy, plot, grouping, sponsorAdminID, sponsorID, departmentIDs, sponsor.MinUserCountToDisclose, monthFrom, monthTo), Response);

            ////////////			string url = GetReportImageUrl(path, langID, dateFrom.Year, dateTo.Year, sponsorAdminID, sponsorID, groupBy, reportPart.Id, projectRoundUnitID, departmentIDs, grouping, plot, dateFrom.Month, dateTo.Month);
            ////////////			HtmlHelper.Write(exporter.Export(url, langID, projectRoundUnit, dateFrom, dateTo, groupBy, plot, grouping, sponsorAdmin, sponsor as Sponsor, departmentIDs), Response);
        }
		
		string GetReportImageUrl(string path, int langID, int yearFrom, int yearTo, int spons, int sid, int gb, int reportPartID, int projectRoundUnitID, string gid, int grpng, int plot, int monthFrom, int monthTo)
		{
			P p = new P(path, "reportImage.aspx");
			p.Q.Add("LangID", langID);
			p.Q.Add("FY", yearFrom);
			p.Q.Add("TY", yearTo);
			p.Q.Add("FM", monthFrom);
			p.Q.Add("TM", monthTo);
			p.Q.Add("SAID", spons);
			p.Q.Add("SID", sid);
			p.Q.Add("GB", gb);
			p.Q.Add("RPID", reportPartID);
			p.Q.Add("PRUID", projectRoundUnitID);
			p.Q.Add("GID", gid);
			p.Q.Add("GRPNG", grpng);
			p.Q.Add("PLOT", plot);
			return p.ToString();
		}
	}
}