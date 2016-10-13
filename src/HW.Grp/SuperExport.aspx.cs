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

namespace HW.Grp
{
	public partial class SuperExport : System.Web.UI.Page
	{
		SqlReportRepository reportRepo = new SqlReportRepository();
		SqlUserRepository userRepo = new SqlUserRepository();
//		protected int lid = Language.ENGLISH;
		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SuperAdminID"] == null, "default.aspx", true);

//			int lid = ConvertHelper.ToInt32(Session["lid"], 2);
			var userSession = userRepo.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
			if (userSession != null) {
				lid = userSession.Lang;
			}
			int rpid = ConvertHelper.ToInt32(Request.QueryString["RPID"]);
			string type = StrHelper.Str3(Request.QueryString["TYPE"], "docx");
			
			var p = reportRepo.ReadReportPart(rpid, lid);
			
			var exporter = ExportFactory.GetSuperExporter(type, p, Server.MapPath("HW template for Word.docx"));
			exporter.CellWrite += delegate(object sender2, ExcelCellEventArgs e2) {
				e2.ExcelCell.Value = R.Str(lid, e2.ValueKey, "");
				e2.Writer.WriteCell(e2.ExcelCell);
			};
			
			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = exporter.Type;
//			HtmlHelper.AddHeaderIf(exporter.HasContentDisposition(p.CurrentLanguage.Subject), "content-disposition", exporter.GetContentDisposition(p.CurrentLanguage.Subject), Response);
			HtmlHelper.AddHeaderIf(exporter.HasContentDisposition(p.SelectedReportPartLang.Subject), "content-disposition", exporter.GetContentDisposition(p.SelectedReportPartLang.Subject), Response);
			
			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
			string url = path + GetReportImageUrl(p);
			
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
			
			HtmlHelper.Write(exporter.SuperExport(url, RNDS1, RNDS2, RNDSD1, RNDSD2, PID1, PID2, N, rpid, FDT, TDT, R1, R2, lid, plot), Response);
		}

		string GetReportImageUrl(ReportPart p)
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
		
//		void Write(object obj)
//		{
//			if (obj is MemoryStream) {
//				Response.BinaryWrite(((MemoryStream)obj).ToArray());
//				Response.End();
//			} else if (obj is string) {
//				Response.Write((string)obj);
//			}
//		}
//		
//		void AddHeaderIf(bool condition, string name, string value)
//		{
//			if (condition) {
//				Response.AddHeader(name, value);
//			}
//		}
	}
}