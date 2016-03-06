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

namespace HW.Grp
{
	public partial class SuperExport : System.Web.UI.Page
	{
		SqlReportRepository r = new SqlReportRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SuperAdminID"] == null, "default.aspx", true);

			int lid = ConvertHelper.ToInt32(Session["lid"], 2);
			int rpid = ConvertHelper.ToInt32(Request.QueryString["RPID"]);
			string type = StrHelper.Str3(Request.QueryString["TYPE"], "docx");
			
			var p = r.ReadReportPart(rpid, lid);
			var exporter = ExportFactory.GetSuperExporter(type, p, Server.MapPath("HW template for Word.docx"));
//			var exporter = new DocXExporter(p, Server.MapPath("HW template for Word.docx"));
			
			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = exporter.Type;
			AddHeaderIf(exporter.HasContentDisposition(p.CurrentLanguage.Subject), "content-disposition", exporter.GetContentDisposition(p.CurrentLanguage.Subject));
			
			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
			Write(exporter.SuperExport(path + GetReportImageUrl(p)));
		}

		string GetReportImageUrl(ReportPart p)
		{
			return string.Format(
				"superReportImage.aspx?N={0}&FDT={1}&TDT={2}&RNDS1={3}&RNDSD1={4}&PID1={5}&RNDS2={6}&RNDSD2={7}&PID2={8}&R1={9}&R2={10}&RPID={11}",
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
				p.Id
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