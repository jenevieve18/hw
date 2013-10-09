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
			get { return HttpContext.Current.Request.QueryString["AK"] != null; }
		}
		
		bool HasWidth {
			get { return HttpContext.Current.Request.QueryString["W"] != null; }
		}
		
		bool HasHeight {
			get { return HttpContext.Current.Request.QueryString["H"] != null; }
		}
		
		bool HasBackground {
			get { return HttpContext.Current.Request.QueryString["BG"] != null; }
		}
		
		int Width {
			get {
				if (HasWidth) {
					return Convert.ToInt32(HttpContext.Current.Request.QueryString["W"]);
				} else {
					return 550;
				}
			}
		}
		
		int Height {
			get {
				if (HasHeight) {
					return Convert.ToInt32(HttpContext.Current.Request.QueryString["H"]);
				} else {
					return 440;
				}
			}
		}
		
		string Background {
			get {
				if (HasBackground) {
					return "#" + HttpContext.Current.Request.QueryString["BG"];
				} else {
					return "#EFEFEF";
				}
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
            Response.ContentType = "image/gif";

			ExtendedGraph g = null;

			int GB = (HttpContext.Current.Request.QueryString["GB"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["GB"].ToString()) : 0);
			bool stdev = (HttpContext.Current.Request.QueryString["STDEV"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 : false);
			
			int fy = HttpContext.Current.Request.QueryString["FY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) : 0;
			int ty = HttpContext.Current.Request.QueryString["TY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) : 0;
			
			int langID = (HttpContext.Current.Request.QueryString["LangID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["LangID"]) : 0);

			int rpid = Convert.ToInt32(HttpContext.Current.Request.QueryString["RPID"]);
			int PRUID = Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]);
			
			bool hasGrouping = HttpContext.Current.Request.QueryString["GRPNG"] != null || HttpContext.Current.Request.QueryString["GRPNG"] != "0";
			
			string plot = HttpContext.Current.Request.QueryString["Plot"] != null ? HttpContext.Current.Request.QueryString["Plot"] : "";
			string key = HttpContext.Current.Request.QueryString["AK"];
			
			int GRPNG = Convert.ToInt32(HttpContext.Current.Request.QueryString["GRPNG"]);
			int SPONS = Convert.ToInt32((HttpContext.Current.Request.QueryString["SAID"] != null ? HttpContext.Current.Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
			int SID = Convert.ToInt32((HttpContext.Current.Request.QueryString["SID"] != null ? HttpContext.Current.Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
			string GID = (HttpContext.Current.Request.QueryString["GID"] != null ? HttpContext.Current.Request.QueryString["GID"].ToString().Replace(" ", "") : "");
			
			object disabled = HttpContext.Current.Request.QueryString["DISABLED"];
			
			int point = HttpContext.Current.Request.QueryString["ExtraPoint"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ExtraPoint"]) : 0;
			
			ReportPart r = service.ReadReportPart(rpid, langID);

			var f = service.GetGraphFactory(HasAnswerKey);
			g = f.CreateGraph(key, r, langID, PRUID, fy, ty, GB, hasGrouping, plot, Width, Height, Background, GRPNG, SPONS, SID, GID, disabled, point);
			g.render();
		}
	}
}