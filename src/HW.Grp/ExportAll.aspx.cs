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
		protected IList<ReportPartLanguage> reportParts = null;
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
			int gb = (Request.QueryString["GB"] != null ? Convert.ToInt32(Request.QueryString["GB"].ToString()) : 0);
//			int stdev = Convert.ToInt32(Request.QueryString["STDEV"]);
			
			int yearFrom = Request.QueryString["FY"] != null ? Convert.ToInt32(Request.QueryString["FY"]) : 0;
			int yearTo = Request.QueryString["TY"] != null ? Convert.ToInt32(Request.QueryString["TY"]) : 0;
			
			int monthFrom = ConvertHelper.ToInt32(Request.QueryString["FM"]);
			int monthTo = ConvertHelper.ToInt32(Request.QueryString["TM"]);
			
			int langID = (Request.QueryString["LangID"] != null ? Convert.ToInt32(Request.QueryString["LangID"]) : 0);

			int rpid = Convert.ToInt32(Request.QueryString["RPID"]);
			int pruid = Convert.ToInt32(Request.QueryString["PRUID"]);
			
			int grpng = Convert.ToInt32(Request.QueryString["GRPNG"]);
			int spons = Convert.ToInt32((Request.QueryString["SAID"] != null ? Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
			int sid = Convert.ToInt32((Request.QueryString["SID"] != null ? Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
			string gid = (Request.QueryString["GID"] != null ? Request.QueryString["GID"].ToString().Replace(" ", "") : "");
			int plot = ConvertHelper.ToInt32(Request.QueryString["PLOT"]);
			string type = Request.QueryString["TYPE"].ToString();
			
			bool hasGrouping = Request.QueryString["GRPNG"] != null || Request.QueryString["GRPNG"] != "0";
			string key = Request.QueryString["AK"];
			
			object disabled = Request.QueryString["DISABLED"];
			
			ISponsor s = service.ReadSponsor(sid);
			reportParts = service.FindByProjectAndLanguage(pruid, langID);

//			var exporter = ExportFactory.GetExporterAll(service, type, HasAnswerKey, hasGrouping, disabled, Width, Height, Background, reportParts, key, Server.MapPath("HW template for Word.docx"));
			var exporter = ExportFactory.GetExporterAll(service, type, HasAnswerKey, hasGrouping, reportParts, Server.MapPath("HW template for Word.docx"));
			Response.ContentType = exporter.Type;
			
			AddHeaderIf(exporter.HasContentDisposition2, "content-disposition", exporter.ContentDisposition2);
			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
//			Write(exporter.ExportAll(gb, yearFrom, yearTo, langID, pruid, grpng, spons, sid, gid, plot, path, s.MinUserCountToDisclose, monthFrom, monthTo));
			
			exporter.UrlSet += delegate(object sender2, ReportPartEventArgs e2) { e2.Url = GetReportImageUrl(path, langID, yearFrom, yearTo, spons, sid, gb, e2.ReportPart.Id, pruid, gid, grpng, plot, monthFrom, monthTo); };
			Write(exporter.ExportAll(langID, pruid, yearFrom, yearTo, gb, plot, grpng, spons, sid, gid, s.MinUserCountToDisclose, monthFrom, monthTo));
			
//			string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot, fm, tm);
//			string url = "";
//			Write(exporter.Export2(url, langID));
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