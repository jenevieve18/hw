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
			new SqlIndexRepository()
		);
		
		bool HasAnswerKey {
			get { return Request.QueryString["AK"] != null; }
		}
		
		bool HasWidth {
			get { return Request.QueryString["W"] != null; }
		}
		
		bool HasHeight {
			get { return Request.QueryString["H"] != null; }
		}
		
		bool HasBackground {
			get { return Request.QueryString["BG"] != null; }
		}
		
		int Width {
			get {
				if (HasWidth) {
					return Convert.ToInt32(Request.QueryString["W"]);
				} else {
					return 550;
				}
			}
		}
		
		int Height {
			get {
				if (HasHeight) {
					return Convert.ToInt32(Request.QueryString["H"]);
				} else {
					return 440;
				}
			}
		}
		
		string Background {
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
			bool stdev = (Request.QueryString["STDEV"] != null ? Convert.ToInt32(Request.QueryString["STDEV"]) == 1 : false);
			
			int fy = Request.QueryString["FY"] != null ? Convert.ToInt32(Request.QueryString["FY"]) : 0;
			int ty = Request.QueryString["TY"] != null ? Convert.ToInt32(Request.QueryString["TY"]) : 0;
			
			int langID = (Request.QueryString["LangID"] != null ? Convert.ToInt32(Request.QueryString["LangID"]) : 0);

			int rpid = Convert.ToInt32(Request.QueryString["RPID"]);
			int pruid = Convert.ToInt32(Request.QueryString["PRUID"]);
			
			bool hasGrouping = Request.QueryString["GRPNG"] != null || Request.QueryString["GRPNG"] != "0";
			
			int plot = ConvertHelper.ToInt32(Request.QueryString["Plot"]);
			string key = Request.QueryString["AK"];
			
			int grpng = Convert.ToInt32(Request.QueryString["GRPNG"]);
			int spons = Convert.ToInt32((Request.QueryString["SAID"] != null ? Request.QueryString["SAID"] : Session["SponsorAdminID"]));
			int sid = Convert.ToInt32((Request.QueryString["SID"] != null ? Request.QueryString["SID"] : Session["SponsorID"]));
			string gid = (Request.QueryString["GID"] != null ? Request.QueryString["GID"].ToString().Replace(" ", "") : "");
			
			object disabled = Request.QueryString["DISABLED"];
			
			int point = Request.QueryString["ExtraPoint"] != null ? Convert.ToInt32(Request.QueryString["ExtraPoint"]) : 0;
			
			ReportPart r = service.ReadReportPart(rpid, langID);

			var f = service.GetGraphFactory(HasAnswerKey);
			g = f.CreateGraph(key, r, langID, pruid, fy, ty, gb, hasGrouping, plot, Width, Height, Background, grpng, spons, sid, gid, disabled, point);
			g.render();
		}
	}
}