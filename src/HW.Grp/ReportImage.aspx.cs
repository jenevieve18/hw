using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;

namespace HW.Grp
{
	public partial class ReportImage : System.Web.UI.Page
	{
		ReportService service = new ReportService(
			new SqlAnswerRepository(),
			new SqlReportRepository(),
			new SqlProjectRepository(),
			new SqlOptionRepository(),
			new SqlDepartmentRepository(),
			new SqlQuestionRepository(),
			new SqlIndexRepository(),
			new SqlSponsorRepository(),
			new SqlSponsorAdminRepository()
		);
		
//		bool HasAnswerKey {
//			get { return Request.QueryString["AK"] != null; }
//		}
//		
//		bool HasWidth {
//			get { return Request.QueryString["W"] != null; }
//		}
//		
//		bool HasHeight {
//			get { return Request.QueryString["H"] != null; }
//		}
//		
//		bool HasBackground {
//			get { return Request.QueryString["BG"] != null; }
//		}
//		
//		int Width {
//			get {
//				if (HasWidth) {
//					return Convert.ToInt32(Request.QueryString["W"]);
//				} else {
//					return 550;
//				}
//			}
//		}
//		
//		int Height {
//			get {
//				if (HasHeight) {
//					return Convert.ToInt32(Request.QueryString["H"]);
//				} else {
//					return 440;
//				}
//			}
//		}
//		
//		string Background {
//			get {
//				if (HasBackground) {
//					return "#" + Request.QueryString["BG"];
//				} else {
//					return "#EFEFEF";
//				}
//			}
//		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			Response.ContentType = "image/gif";

			int groupBy = ConvertHelper.ToInt32(Request.QueryString["GB"].ToString());
			
//			int yearFrom = ConvertHelper.ToInt32(Request.QueryString["FY"]);
//			int yearTo = ConvertHelper.ToInt32(Request.QueryString["TY"]);
//			
//			int monthFrom = ConvertHelper.ToInt32(Request.QueryString["FM"]);
//			int monthTo = ConvertHelper.ToInt32(Request.QueryString["TM"]);
			
			var dateFrom = new DateTime(ConvertHelper.ToInt32(Request.QueryString["FY"]), ConvertHelper.ToInt32(Request.QueryString["FM"]), 1);
			var dateTo = new DateTime(ConvertHelper.ToInt32(Request.QueryString["TY"]), ConvertHelper.ToInt32(Request.QueryString["TM"]), 1);
			
			int langID = ConvertHelper.ToInt32(Request.QueryString["LangID"]);

			int reportPartID = Convert.ToInt32(Request.QueryString["RPID"]);
            int projectRoundUnitID = ConvertHelper.ToInt32(Request.QueryString["PRUID"].Replace("SPRU", ""));
			
			// FIXME: This hasGrouping value is always true! Please check!
			bool hasGrouping = Request.QueryString["GRPNG"] != null || Request.QueryString["GRPNG"] != "0";
			
			int plot = ConvertHelper.ToInt32(Request.QueryString["Plot"]);
			string key = Request.QueryString["AK"];
			
			int grouping = ConvertHelper.ToInt32(Request.QueryString["GRPNG"]);
			int sponsorAdminID = ConvertHelper.ToInt32(Request.QueryString["SAID"], ConvertHelper.ToInt32(Session["SponsorAdminID"]));
			int sponsorID = ConvertHelper.ToInt32(Request.QueryString["SID"], ConvertHelper.ToInt32(Session["SponsorID"]));
			string departmentIDs = (Request.QueryString["GID"] != null ? Request.QueryString["GID"].ToString().Replace(" ", "") : "");
			
			object disabled = Request.QueryString["DISABLED"];
			
			int point = ConvertHelper.ToInt32(Request.QueryString["ExtraPoint"]);
			
			var reportService = new ReportService3();

			var reportPart = reportService.ReadReportPart(reportPartID);
			reportPart.SelectedReportPartLangID = langID;

//			var factory = service.GetGraphFactory(HasAnswerKey);
//			var graph = factory.CreateGraph(key, reportPart, langID, projectRoundUnitID, yearFrom, yearTo, groupBy, hasGrouping, plot, Width, Height, Background, grouping, sponsorAdminID, sponsorID, departmentIDs, disabled, point, sponsor.MinUserCountToDisclose, monthFrom, monthTo);
			
			var factory = new GroupStatsGraphFactory(new SqlAnswerRepository(), new SqlReportRepository(), new SqlProjectRepository(), new SqlOptionRepository(), new SqlIndexRepository(), new SqlQuestionRepository(), new SqlDepartmentRepository());
//			var graph = factory.CreateGraph(key, reportPart, langID, projectRoundUnitID, yearFrom, yearTo, groupBy, hasGrouping, plot, Width, Height, Background, grouping, sponsorAdminID, sponsorID, departmentIDs, disabled, point, sponsor.MinUserCountToDisclose, monthFrom, monthTo);
			
//			var dateFrom = new DateTime(yearFrom, monthFrom, 1);
//			var dateTo = new DateTime(yearTo, monthTo, 1);
			
			var projectRoundUnit = service.ReadProjectRoundUnit(projectRoundUnitID);
			var sponsorAdmin = service.ReadSponsorAdmin(sponsorAdminID);
			
            var sponsor = new SqlSponsorRepository().Read(sponsorID);
			var graph = factory.CreateGraph(reportPart, projectRoundUnit, langID, sponsorAdmin, sponsor, dateFrom, dateTo, groupBy, hasGrouping, plot, grouping, departmentIDs, point);
			
			graph.render();
		}
	}
}