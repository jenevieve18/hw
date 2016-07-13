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
	public partial class Export : System.Web.UI.Page
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
		
		bool HasAnswerKey {
			get { return Request.QueryString["AK"] != null; }
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int gb = (Request.QueryString["GB"] != null ? Convert.ToInt32(Request.QueryString["GB"].ToString()) : 0);
			int stdev = Convert.ToInt32(Request.QueryString["STDEV"]);
			
			int yearFrom = Request.QueryString["FY"] != null ? Convert.ToInt32(Request.QueryString["FY"]) : 0;
			int yearTo = Request.QueryString["TY"] != null ? Convert.ToInt32(Request.QueryString["TY"]) : 0;
			
			int monthFrom = ConvertHelper.ToInt32(Request.QueryString["FM"]);
			int monthTo = ConvertHelper.ToInt32(Request.QueryString["TM"]);
			
			int langID = (Request.QueryString["LangID"] != null ? Convert.ToInt32(Request.QueryString["LangID"]) : 0);

			int reportPartID = Convert.ToInt32(Request.QueryString["RPID"]);
            string project = Request.QueryString["PRUID"];
            int pruid = ConvertHelper.ToInt32(project.Replace("SPRU", ""));
			
			int grpng = Convert.ToInt32(Request.QueryString["GRPNG"]);
			int spons = Convert.ToInt32((Request.QueryString["SAID"] != null ? Request.QueryString["SAID"] : Session["SponsorAdminID"]));
			int sid = Convert.ToInt32((Request.QueryString["SID"] != null ? Request.QueryString["SID"] : Session["SponsorID"]));
			string gid = (Request.QueryString["GID"] != null ? Request.QueryString["GID"].ToString().Replace(" ", "") : "");
			int plot = ConvertHelper.ToInt32(Request.QueryString["Plot"]);
			string type = Request.QueryString["TYPE"].ToString();
			
			bool hasGrouping = Request.QueryString["GRPNG"] != null || Request.QueryString["GRPNG"] != "0";
//			string key = Request.QueryString["AK"];
			
			object disabled = Request.QueryString["DISABLED"];
			
			ISponsor sponsor = service.ReadSponsor(sid);
			ReportPart reportPart = service.ReadReportPart(reportPartID, langID);
			
			var exporter = ExportFactory.GetExporter(service, type, HasAnswerKey, hasGrouping, reportPart, Server.MapPath("HW template for Word.docx"));
			
			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = exporter.Type;
			HtmlHelper.AddHeaderIf(exporter.HasContentDisposition(reportPart.CurrentLanguage.Subject), "content-disposition", exporter.GetContentDisposition(reportPart.CurrentLanguage.Subject), Response);
			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
			
			string url = GetReportImageUrl(path, langID, yearFrom, yearTo, spons, sid, gb, reportPart.Id, pruid, gid, grpng, plot, monthFrom, monthTo);
			HtmlHelper.Write(exporter.Export(url, langID, pruid, yearFrom, yearTo, gb, plot, grpng, spons, sid, gid, sponsor.MinUserCountToDisclose, monthFrom, monthTo), Response);
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