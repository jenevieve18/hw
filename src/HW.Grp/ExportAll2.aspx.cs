using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Helpers.Exporters;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;

namespace HW.Grp
{
	public partial class ExportAll2 : System.Web.UI.Page
	{
		IList<IReportPart> reportParts = new List<IReportPart>();
//		ReportService service = new ReportService(
//			new SqlAnswerRepository(),
//			new SqlReportRepository(),
//			new SqlProjectRepository(),
//			new SqlOptionRepository(),
//			new SqlDepartmentRepository(),
//			new SqlQuestionRepository(),
//			new SqlIndexRepository(),
//			new SqlSponsorRepository(),
//			new SqlSponsorAdminRepository()
//		);
		ReportService service = new ReportService();
//		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);

		bool HasAnswerKey
		{
			get { return Request.QueryString["AK"] != null; }
		}

		bool HasWidth
		{
			get { return Request.QueryString["W"] != null; }
		}

		bool HasHeight
		{
			get { return Request.QueryString["H"] != null; }
		}

		bool HasBackground
		{
			get { return Request.QueryString["BG"] != null; }
		}

		int Width
		{
			get {
				if (HasWidth) {
					return Convert.ToInt32(Request.QueryString["W"]);
				} else {
					return 550;
				}
			}
		}

		int Height
		{
			get {
				if (HasHeight) {
					return Convert.ToInt32(Request.QueryString["H"]);
				} else {
					return 440;
				}
			}
		}

		string Background
		{
			get {
				if (HasBackground) {
					return "#" + Request.QueryString["BG"];
				} else {
					return "#EFEFEF";
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			int groupBy = (Request.QueryString["GB"] != null ? Convert.ToInt32(Request.QueryString["GB"].ToString()) : 0);

//			int yearFrom = Request.QueryString["FY"] != null ? Convert.ToInt32(Request.QueryString["FY"]) : 0;
//			int yearTo = Request.QueryString["TY"] != null ? Convert.ToInt32(Request.QueryString["TY"]) : 0;
//
//			int monthFrom = ConvertHelper.ToInt32(Request.QueryString["FM"]);
//			int monthTo = ConvertHelper.ToInt32(Request.QueryString["TM"]);
			
			var dateFrom = new DateTime(ConvertHelper.ToInt32(Request.QueryString["FY"]), ConvertHelper.ToInt32(Request.QueryString["FM"]), 1);
			var dateTo = new DateTime(ConvertHelper.ToInt32(Request.QueryString["TY"]), ConvertHelper.ToInt32(Request.QueryString["TM"]), 1);

			int langID = (Request.QueryString["LangID"] != null ? Convert.ToInt32(Request.QueryString["LangID"]) : 0);

			int projectRoundUnitID = ConvertHelper.ToInt32(Request.QueryString["PRUID"]);

			int grouping = Convert.ToInt32(Request.QueryString["GRPNG"]);
			int sponsorAdminID = Convert.ToInt32((Request.QueryString["SAID"] != null ? Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
			int sponsorID = Convert.ToInt32((Request.QueryString["SID"] != null ? Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
			string departmentIDs = (Request.QueryString["GID"] != null ? Request.QueryString["GID"].ToString().Replace(" ", "") : "");
			int plot = ConvertHelper.ToInt32(Request.QueryString["PLOT"]);
			string type = StrHelper.Str3(Request.QueryString["TYPE"], "");

			bool hasGrouping = Request.QueryString["GRPNG"] != null || Request.QueryString["GRPNG"] != "0";

			IAdmin sponsor = service.ReadSponsor(sponsorID);
//			reportParts = service.FindByProjectAndLanguage(pruid, langID);
			
			string project = Request.QueryString["PRUID"];
			int sponsorProjectID = ConvertHelper.ToInt32(project.Replace("SP", ""));
//			var measureRepository = new SqlMeasureRepository();
//			var sponsorProject = measureRepository.ReadSponsorProject(sponsorProjectID);
			
			var sponsorProjectRepo = new SqlSponsorProjectRepository();
			var sponsorProject = sponsorProjectRepo.Read(sponsorProjectID);
			reportParts.Add(sponsorProject);
			
			ProjectRoundUnit projectRoundUnit = service.ReadProjectRoundUnit(projectRoundUnitID);
			var sponsorAdmin = service.ReadSponsorAdmin(sponsorAdminID);

			var exporter = ExportFactory.GetExporterAll(service, type, HasAnswerKey, hasGrouping, reportParts, Server.MapPath("HW template for Word.docx"));
			exporter.CellWrite += delegate(object sender2, ExcelCellEventArgs e2) {
//				e2.ExcelCell.Value = R.Str(lid, e2.ValueKey, "");
				e2.ExcelCell.Value = R.Str(langID, e2.ValueKey, "");
				e2.Writer.WriteCell(e2.ExcelCell);
			};
			
			Response.ContentType = exporter.Type;

			HtmlHelper.AddHeaderIf(exporter.HasContentDisposition2, "content-disposition", exporter.ContentDisposition2, Response);
			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;

//			exporter.UrlSet += delegate(object sender2, ReportPartEventArgs e2) { e2.Url = GetReportImageUrl(path, langID, yearFrom, yearTo, sponsorAdminID, sponsorID, groupBy, e2.ReportPart.Id, project, departmentIDs, grouping, plot, monthFrom, monthTo); };
//			HtmlHelper.Write(exporter.ExportAll(langID, projectRoundUnitID, yearFrom, yearTo, groupBy, plot, grouping, sponsorAdminID, sponsorID, departmentIDs, s.MinUserCountToDisclose, monthFrom, monthTo), Response);

			exporter.UrlSet += delegate(object sender2, ReportPartEventArgs e2) {
				e2.Url = GetReportImageUrl(path, langID, dateFrom.Year, dateTo.Year, sponsorAdminID, sponsorID, groupBy, e2.ReportPart.Id, project, departmentIDs, grouping, plot, dateFrom.Month, dateTo.Month);
			};
			HtmlHelper.Write(exporter.ExportAll(langID, projectRoundUnit, dateFrom, dateTo, groupBy, plot, grouping, sponsorAdmin, sponsor as Sponsor, departmentIDs), Response);
		}

//		void Write(object obj)
//		{
//			if (obj is MemoryStream) {
//				Response.BinaryWrite(((MemoryStream)obj).ToArray());
//				Response.End();
//			} else if (obj is string) {
//				Response.Write((string)obj);
//			}
//		}
//
//		void AddHeaderIf(bool condition, string name, string value)
//		{
//			if (condition) {
//				Response.AddHeader(name, value);
//			}
//		}

		string GetReportImageUrl(string path, int langID, int yearFrom, int yearTo, int sponsorAdminID, int sponsorID, int groupBy, int reportPartID, string projectRoundUnitID, string departmentIDs, int grouping, int plot, int monthFrom, int monthTo)
		{
			P p = new P(path, "reportImage2.aspx");
			p.Q.Add("LangID", langID);
			p.Q.Add("FY", yearFrom);
			p.Q.Add("TY", yearTo);
			p.Q.Add("FM", monthFrom);
			p.Q.Add("TM", monthTo);
			p.Q.Add("SAID", sponsorAdminID);
			p.Q.Add("SID", sponsorID);
			p.Q.Add("GB", groupBy);
			p.Q.Add("RPID", reportPartID);
			p.Q.Add("PRUID", projectRoundUnitID);
			p.Q.Add("GID", departmentIDs);
			p.Q.Add("GRPNG", grouping);
			p.Q.Add("PLOT", plot);
			return p.ToString();
		}
		
//		IList<IReportPart> GetReportParts(string project)
//		{
//			var parts = new List<IReportPart>();
//			if (project.Contains("SPRU")) {
//				int selectedProjectRoundUnitID = ConvertHelper.ToInt32(project.Replace("SPRU", ""));
//				int selectedDepartmentID = departments[0].Id;
//				var reportParts = reportRepository.FindByProjectAndLanguage2(selectedProjectRoundUnitID, lid, selectedDepartmentID);
//				if (reportParts.Count <= 0) {
//					reportParts = reportRepository.FindByProjectAndLanguage(selectedProjectRoundUnitID, lid);
//				}
//				parts.AddRange(reportParts);
//			} else {
//				int sponsorProjectID = ConvertHelper.ToInt32(project.Replace("SP", ""));
//				var sponsorProject = measureRepository.ReadSponsorProject(sponsorProjectID);
//				parts.Add(sponsorProject);
//			}
//			return parts;
//		}
	}
}