using System;
using System.Collections.Generic;
using System.IO;
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
	public partial class ExportAll : System.Web.UI.Page
	{
		protected IList<IReportPart> reportParts = null;
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
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int groupBy = (Request.QueryString["GB"] != null ? Convert.ToInt32(Request.QueryString["GB"].ToString()) : 0);
			
			int yearFrom = Request.QueryString["FY"] != null ? Convert.ToInt32(Request.QueryString["FY"]) : 0;
			int yearTo = Request.QueryString["TY"] != null ? Convert.ToInt32(Request.QueryString["TY"]) : 0;
			
			int monthFrom = ConvertHelper.ToInt32(Request.QueryString["FM"]);
			int monthTo = ConvertHelper.ToInt32(Request.QueryString["TM"]);
			
			int langID = (Request.QueryString["LangID"] != null ? Convert.ToInt32(Request.QueryString["LangID"]) : 0);

//			int rpid = Convert.ToInt32(Request.QueryString["RPID"]);
			string project = Request.QueryString["PRUID"];
			int projectRoundUnitID = ConvertHelper.ToInt32(project.Replace("SPRU", ""));
			
			int grouping = Convert.ToInt32(Request.QueryString["GRPNG"]);
			int sponsorAdminID = Convert.ToInt32((Request.QueryString["SAID"] != null ? Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
			int sponsorID = Convert.ToInt32((Request.QueryString["SID"] != null ? Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
			string departmentIDs = (Request.QueryString["GID"] != null ? Request.QueryString["GID"].ToString().Replace(" ", "") : "");
			int plot = ConvertHelper.ToInt32(Request.QueryString["PLOT"]);
            string type = StrHelper.Str3(Request.QueryString["TYPE"], "");
			
			bool hasGrouping = Request.QueryString["GRPNG"] != null || Request.QueryString["GRPNG"] != "0";
//			string key = Request.QueryString["AK"];
			
			ISponsor sponsor = service.ReadSponsor(sponsorID);
			reportParts = service.FindByProjectAndLanguage(projectRoundUnitID, langID);

			var exporter = ExportFactory.GetExporterAll(service, type, HasAnswerKey, hasGrouping, reportParts, Server.MapPath("HW template for Word.docx"));
			Response.ContentType = exporter.Type;
			
			AddHeaderIf(exporter.HasContentDisposition2, "content-disposition", exporter.ContentDisposition2);
			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
			
			exporter.UrlSet += delegate(object sender2, ReportPartEventArgs e2) { e2.Url = GetReportImageUrl(path, langID, yearFrom, yearTo, sponsorAdminID, sponsorID, groupBy, e2.ReportPart.Id, projectRoundUnitID, departmentIDs, grouping, plot, monthFrom, monthTo); };
			Write(exporter.ExportAll(langID, projectRoundUnitID, yearFrom, yearTo, groupBy, plot, grouping, sponsorAdminID, sponsorID, departmentIDs, sponsor.MinUserCountToDisclose, monthFrom, monthTo));
		}
		
		void Write(object obj)
		{
			if (obj is MemoryStream) {
				Response.BinaryWrite(((MemoryStream)obj).ToArray());
				Response.End();
			} else if (obj is string) {
				Response.Write((string)obj);
			}
		}
		
		void AddHeaderIf(bool condition, string name, string value)
		{
			if (condition) {
				Response.AddHeader(name, value);
			}
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