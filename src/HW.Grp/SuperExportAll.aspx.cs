using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Util.Exporters;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;

namespace HW.Grp
{
	public partial class SuperExportAll : System.Web.UI.Page
	{
		ReportService service = new ReportService();
		SqlReportRepository reportRepo = new SqlReportRepository();
		SqlUserRepository userRepo = new SqlUserRepository();
		
		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SuperAdminID"] == null, "default.aspx", true);

			var userSession = userRepo.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
			if (userSession != null) {
				lid = userSession.Lang;
			}
			string type = StrHelper.Str3(Request.QueryString["TYPE"], "docx");
			
			var parts = reportRepo.FindPartLanguagesByReport(Convert.ToInt32(Request.QueryString["RID"]), lid);
			
			var exporter = ExportFactory.GetSuperExporterAll(type, service, parts, Server.MapPath("HW template for Word.docx"));
			exporter.CellWrite += delegate(object sender2, ExcelCellEventArgs e2) {
				e2.ExcelCell.Value = R.Str(lid, e2.ValueKey, "");
				e2.Writer.WriteCell(e2.ExcelCell);
			};
			
			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = exporter.Type;
			HtmlHelper.AddHeaderIf(exporter.HasContentDisposition2, "content-disposition", exporter.ContentDisposition2, Response);
			
			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
			
			string N = StrHelper.Str3(Request.QueryString["N"], "");
			string FDT = StrHelper.Str3(Request.QueryString["FDT"], "");
			string TDT = StrHelper.Str3(Request.QueryString["TDT"], "");
			string RNDS1 = StrHelper.Str3(Request.QueryString["RNDS1"], "");
			string RNDSD1 = StrHelper.Str3(Request.QueryString["RNDSD1"], "");
			string PID1 = StrHelper.Str3(Request.QueryString["PID1"], "");
			string RNDS2 = StrHelper.Str3(Request.QueryString["RNDS2"], "");
			string RNDSD2 = StrHelper.Str3(Request.QueryString["RNDSD2"], "");
			string PID2 = StrHelper.Str3(Request.QueryString["PID2"], "");
			string R1 = StrHelper.Str3(Request.QueryString["R1"], "");
			string R2 = StrHelper.Str3(Request.QueryString["R2"], "");
			
			int plot = ConvertHelper.ToInt32(Request.QueryString["Plot"]);
			
//			Write(exporter.SuperExport(url, RNDS1, RNDS2, RNDSD1, RNDSD2, PID1, PID2, N, rpid, FDT, TDT, R1, R2, lid, plot));
			
			exporter.UrlSet += delegate(object sender2, ReportPartEventArgs e2) { e2.Url = path + GetSuperReportImageUrl(e2.ReportPart); };

//			Write(exporter.SuperExportAll(lid));
			HtmlHelper.Write(exporter.SuperExportAll(RNDS1, RNDS2, RNDSD1, RNDSD2, PID1, PID2, N, FDT, TDT, R1, R2, lid, plot), Response);
		}

		string GetSuperReportImageUrl(ReportPart p)
		{
			return string.Format(
				"superReportImage.aspx?N={0}&FDT={1}&TDT={2}&RNDS1={3}&RNDSD1={4}&PID1={5}&RNDS2={6}&RNDSD2={7}&PID2={8}&R1={9}&R2={10}&RPID={11}&Plot={12}",
				(StrHelper.Str3(Request.QueryString["N"], "")),
				(StrHelper.Str3(Request.QueryString["FDT"], "")),
				(StrHelper.Str3(Request.QueryString["TDT"], "")),
				(StrHelper.Str3(Request.QueryString["RNDS1"], "")),
				(StrHelper.Str3(Request.QueryString["RNDSD1"], "")),
				(StrHelper.Str3(Request.QueryString["PID1"], "")),
				(StrHelper.Str3(Request.QueryString["RNDS2"], "")),
				(StrHelper.Str3(Request.QueryString["RNDSD2"], "")),
				(StrHelper.Str3(Request.QueryString["PID2"], "")),
				(StrHelper.Str3(Request.QueryString["R1"], "")),
				(StrHelper.Str3(Request.QueryString["R2"], "")),
				p.Id,
				(StrHelper.Str3(Request.QueryString["Plot"], ""))
			);
		}
	}
}