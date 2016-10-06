using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;

namespace HW.Grp
{
	public partial class ReportImage2 : System.Web.UI.Page
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

//		bool HasAnswerKey
//		{
//			get { return Request.QueryString["AK"] != null; }
//		}
//
//		bool HasWidth
//		{
//			get { return Request.QueryString["W"] != null; }
//		}
//
//		bool HasHeight
//		{
//			get { return Request.QueryString["H"] != null; }
//		}
//
//		bool HasBackground
//		{
//			get { return Request.QueryString["BG"] != null; }
//		}
//
//		int Width
//		{
//			get {
//				if (HasWidth) {
//					return Convert.ToInt32(Request.QueryString["W"]);
//				} else {
//					return 550;
//				}
//			}
//		}
//
//		int Height
//		{
//			get {
//				if (HasHeight) {
//					return Convert.ToInt32(Request.QueryString["H"]);
//				} else {
//					return 440;
//				}
//			}
//		}
//
//		string Background
//		{
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

			ExtendedGraph graph = null;

			try {
				int groupBy = (Request.QueryString["GB"] != null ? Convert.ToInt32(Request.QueryString["GB"].ToString()) : 0);

//				int yearFrom = Request.QueryString["FY"] != null ? Convert.ToInt32(Request.QueryString["FY"]) : 0;
//				int yearTo = Request.QueryString["TY"] != null ? Convert.ToInt32(Request.QueryString["TY"]) : 0;
//
//				int monthFrom = ConvertHelper.ToInt32(Request.QueryString["FM"]);
//				int monthTo = ConvertHelper.ToInt32(Request.QueryString["TM"]);
				
				var dateFrom = new DateTime(ConvertHelper.ToInt32(Request.QueryString["FY"]), ConvertHelper.ToInt32(Request.QueryString["FM"]), 1);
				var dateTo = new DateTime(ConvertHelper.ToInt32(Request.QueryString["TY"]), ConvertHelper.ToInt32(Request.QueryString["TM"]), 1);

				int langID = (Request.QueryString["LangID"] != null ? Convert.ToInt32(Request.QueryString["LangID"]) : 0);

				int sponsorProjectID = ConvertHelper.ToInt32(Request.QueryString["PRUID"].Replace("SP", ""));

				// FIXME: This hasGrouping value is always true! Please check!
				bool hasGrouping = Request.QueryString["GRPNG"] != null || Request.QueryString["GRPNG"] != "0";

				int plot = ConvertHelper.ToInt32(Request.QueryString["Plot"]);
//				string key = Request.QueryString["AK"];

				int grouping = Convert.ToInt32(Request.QueryString["GRPNG"]);
				int sponsorAdminID = Convert.ToInt32((Request.QueryString["SAID"] != null ? Request.QueryString["SAID"] : Session["SponsorAdminID"]));
				int sponsorID = Convert.ToInt32((Request.QueryString["SID"] != null ? Request.QueryString["SID"] : Session["SponsorID"]));
				string departmentIDs = (Request.QueryString["GID"] != null ? Request.QueryString["GID"].ToString().Replace(" ", "") : "");

				object disabled = Request.QueryString["DISABLED"];

				int point = Request.QueryString["ExtraPoint"] != null ? Convert.ToInt32(Request.QueryString["ExtraPoint"]) : 0;

				IAdmin sponsor = service.ReadSponsor(sponsorID);
//				SponsorProject project = new SqlMeasureRepository().ReadSponsorProject(sponsorProjectID);
				SponsorProject project = new SqlSponsorProjectRepository().Read(sponsorProjectID);
				
				var sponsorAdmin = service.ReadSponsorAdmin(sponsorAdminID);
				
				var factory = new ForStepCount(new SqlAnswerRepository(), new SqlReportRepository(), new SqlProjectRepository(), new SqlOptionRepository(), new SqlIndexRepository(), new SqlQuestionRepository(), new SqlDepartmentRepository(), new SqlMeasureRepository());
//				graph = factory.CreateGraph(project, langID, yearFrom, yearTo, groupBy, hasGrouping, plot, grouping, sponsorAdminID, sponsorID, departmentIDs, disabled, point, sponsor.MinUserCountToDisclose, monthFrom, monthTo);
				graph = factory.CreateGraph(project, langID, dateFrom, dateTo, groupBy, hasGrouping, plot, grouping, sponsorAdmin, sponsor as Sponsor, departmentIDs, disabled, point);
			} catch (NotSupportedException) {
				graph = new ExtendedGraph(895, 440, "#FFFFFF");
				graph.SetMinMax(0, 100);
				graph.DrawBackgroundFromIndex(new BaseIndex());
				graph.DrawComputingSteps(null, 0);
				int bx = 0;
				graph.Explanations.Add(
					new Explanation {
						Description = "Background variable graph for this project is not yet supported.",
						Color = bx + 0,
						Right = bx == 0 ? false : true,
						Box = bx == 0 ? true : false,
						HasAxis = bx == 0 ? false : true
					}
				);
				graph.Draw();
			}
			graph.render();
		}
	}
}