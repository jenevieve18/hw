using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Services;

namespace HW.Grp
{
	public partial class ReportImage : System.Web.UI.Page
	{
		ReportService service = new ReportService(
			AppContext.GetRepositoryFactory().CreateAnswerRepository(),
			AppContext.GetRepositoryFactory().CreateReportRepository(),
			AppContext.GetRepositoryFactory().CreateProjectRepository(),
			AppContext.GetRepositoryFactory().CreateOptionRepository(),
			AppContext.GetRepositoryFactory().CreateDepartmentRepository(),
			AppContext.GetRepositoryFactory().CreateQuestionRepository(),
			AppContext.GetRepositoryFactory().CreateIndexRepository()
			
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

			int GB = (Request.QueryString["GB"] != null ? Convert.ToInt32(Request.QueryString["GB"].ToString()) : 0);
			bool stdev = (Request.QueryString["STDEV"] != null ? Convert.ToInt32(Request.QueryString["STDEV"]) == 1 : false);
			
			int fy = Request.QueryString["FY"] != null ? Convert.ToInt32(Request.QueryString["FY"]) : 0;
			int ty = Request.QueryString["TY"] != null ? Convert.ToInt32(Request.QueryString["TY"]) : 0;
			
			int langID = (Request.QueryString["LangID"] != null ? Convert.ToInt32(Request.QueryString["LangID"]) : 0);

			int rpid = Convert.ToInt32(Request.QueryString["RPID"]);
			int PRUID = Convert.ToInt32(Request.QueryString["PRUID"]);
			
			bool hasGrouping = Request.QueryString["GRPNG"] != null || Request.QueryString["GRPNG"] != "0";
			
			string plot = Request.QueryString["Plot"] != null ? Request.QueryString["Plot"] : "";
			string key = Request.QueryString["AK"];
			
			int GRPNG = Convert.ToInt32(Request.QueryString["GRPNG"]);
			int SPONS = Convert.ToInt32((Request.QueryString["SAID"] != null ? Request.QueryString["SAID"] : Session["SponsorAdminID"]));
			int SID = Convert.ToInt32((Request.QueryString["SID"] != null ? Request.QueryString["SID"] : Session["SponsorID"]));
			string GID = (Request.QueryString["GID"] != null ? Request.QueryString["GID"].ToString().Replace(" ", "") : "");
			
			object disabled = Request.QueryString["DISABLED"];
			
			int point = Request.QueryString["ExtraPoint"] != null ? Convert.ToInt32(Request.QueryString["ExtraPoint"]) : 0;
			
			ReportPart r = service.ReadReportPart(rpid, langID);

			var f = service.GetGraphFactory(HasAnswerKey);
			g = f.CreateGraph(key, r, langID, PRUID, fy, ty, GB, hasGrouping, plot, Width, Height, Background, GRPNG, SPONS, SID, GID, disabled, point);
			g.render();
		}
	}
}