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
			new SqlSponsorRepository()
		);

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

		private void Page_Load(object sender, System.EventArgs e)
		{
			Response.ContentType = "image/gif";

			ExtendedGraph g = null;

			int gb = (Request.QueryString["GB"] != null ? Convert.ToInt32(Request.QueryString["GB"].ToString()) : 0);

			int yearFrom = Request.QueryString["FY"] != null ? Convert.ToInt32(Request.QueryString["FY"]) : 0;
			int yearTo = Request.QueryString["TY"] != null ? Convert.ToInt32(Request.QueryString["TY"]) : 0;

			int monthFrom = ConvertHelper.ToInt32(Request.QueryString["FM"]);
			int monthTo = ConvertHelper.ToInt32(Request.QueryString["TM"]);

			int langID = (Request.QueryString["LangID"] != null ? Convert.ToInt32(Request.QueryString["LangID"]) : 0);

//			int reportPartID = Convert.ToInt32(Request.QueryString["RPID"]);
			int sponsorProjectID = ConvertHelper.ToInt32(Request.QueryString["PRUID"].Replace("SP", ""));
//			int projectRoundUnitID = ConvertHelper.ToInt32(Request.QueryString["PRUID"]);

			// FIXME: This hasGrouping value is always true! Please check!
			bool hasGrouping = Request.QueryString["GRPNG"] != null || Request.QueryString["GRPNG"] != "0";
			//			bool hasGrouping = ConvertHelper.ToInt32(Request.QueryString["GRPNG"], 0) != 0;

			int plot = ConvertHelper.ToInt32(Request.QueryString["Plot"]);
			string key = Request.QueryString["AK"];

			int grpng = Convert.ToInt32(Request.QueryString["GRPNG"]);
			int sponsorAdminID = Convert.ToInt32((Request.QueryString["SAID"] != null ? Request.QueryString["SAID"] : Session["SponsorAdminID"]));
			int sid = Convert.ToInt32((Request.QueryString["SID"] != null ? Request.QueryString["SID"] : Session["SponsorID"]));
			string gid = (Request.QueryString["GID"] != null ? Request.QueryString["GID"].ToString().Replace(" ", "") : "");

			object disabled = Request.QueryString["DISABLED"];

			int point = Request.QueryString["ExtraPoint"] != null ? Convert.ToInt32(Request.QueryString["ExtraPoint"]) : 0;

			ISponsor s = service.ReadSponsor(sid);
//			ReportPart r = service.ReadReportPart(reportPartID, langID);
			SponsorProject r = new SqlMeasureRepository().ReadSponsorProject(sponsorProjectID);

//			var f = service.GetGraphFactory(HasAnswerKey);
			var f = new ForStepCount(new SqlAnswerRepository(), new SqlReportRepository(), new SqlProjectRepository(), new SqlOptionRepository(), new SqlIndexRepository(), new SqlQuestionRepository(), new SqlDepartmentRepository(), new SqlMeasureRepository());
//			g = f.CreateGraph(key, r, langID, projectRoundUnitID, yearFrom, yearTo, gb, hasGrouping, plot, Width, Height, Background, grpng, sponsorAdminID, sid, gid, disabled, point, s.MinUserCountToDisclose, monthFrom, monthTo);
//			g = f.CreateGraph(r, langID, projectRoundUnitID, yearFrom, yearTo, gb, hasGrouping, plot, grpng, sponsorAdminID, sid, gid, disabled, point, s.MinUserCountToDisclose, monthFrom, monthTo);
			g = f.CreateGraph(r, langID, yearFrom, yearTo, gb, hasGrouping, plot, grpng, sponsorAdminID, sid, gid, disabled, point, s.MinUserCountToDisclose, monthFrom, monthTo);
			g.render();
		}
	}
}