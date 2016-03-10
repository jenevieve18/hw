using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class SuperExportAll : System.Web.UI.Page
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
    	SqlReportRepository r = new SqlReportRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SuperAdminID"] == null, "default.aspx", true);

			int lid = ConvertHelper.ToInt32(Session["lid"], 2);
			string type = StrHelper.Str3(Request.QueryString["TYPE"], "docx");
			
//			var parts = r.FindPartLanguagesByReport(Convert.ToInt32(Request.QueryString["RID"]));
			var parts = r.FindPartLanguagesByReport(Convert.ToInt32(Request.QueryString["RID"]), lid);
//			var exporter = ExportFactory.GetSuperExporterAll(service, parts, type, Server.MapPath("HW template for Word.docx"));
			var exporter = ExportFactory.GetSuperExporterAll(type, service, parts, Server.MapPath("HW template for Word.docx"));
			
			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = exporter.Type;
			AddHeaderIf(exporter.HasContentDisposition2, "content-disposition", exporter.ContentDisposition2);
			
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
			Write(exporter.SuperExportAll(RNDS1, RNDS2, RNDSD1, RNDSD2, PID1, PID2, N, FDT, TDT, R1, R2, lid, plot));
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
    }
}