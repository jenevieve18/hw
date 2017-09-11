using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Util.Exporters;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using System.Configuration;

namespace HW.Grp
{
	public partial class ExportAll : System.Web.UI.Page
	{
		protected IList<IReportPart> reportParts = null;
		ReportService service = new ReportService();
		
		bool HasAnswerKey {
			get { return Request.QueryString["AK"] != null; }
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{

            string token = (Request.QueryString["Token"] != null ? Request.QueryString["Token"] : "");

            if (token.Length != 36)
            {
                return;
            }

            int groupBy = (Request.QueryString["GB"] != null ? Convert.ToInt32(Request.QueryString["GB"].ToString()) : 0);

            int fromYear = Convert.ToInt32(Request.QueryString["FY"]);
            int fromMonth = Convert.ToInt32(Request.QueryString["FM"]);
            int toYear = Convert.ToInt32(Request.QueryString["TY"]);
            int toMonth = Convert.ToInt32(Request.QueryString["TM"]);
			
			int langID = (Request.QueryString["LangID"] != null ? Convert.ToInt32(Request.QueryString["LangID"]) : 0);

//			int rpid = Convert.ToInt32(Request.QueryString["RPID"]);
			string project = Request.QueryString["PRUID"];
			//int projectRoundUnitID = ConvertHelper.ToInt32(project.Replace("SPRU", ""));
			
			int grouping = Convert.ToInt32(Request.QueryString["GRPNG"]);
			int sponsorAdminID = Convert.ToInt32((Request.QueryString["SAID"] != null ? Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
			int sponsorID = Convert.ToInt32((Request.QueryString["SID"] != null ? Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
			string departmentIDs = (Request.QueryString["GID"] != null ? Request.QueryString["GID"].ToString().Replace(" ", "") : "");
			int plot = ConvertHelper.ToInt32(Request.QueryString["PLOT"]);
            string type = StrHelper.Str3(Request.QueryString["TYPE"], "");
			
			bool hasGrouping = Request.QueryString["GRPNG"] != null || Request.QueryString["GRPNG"] != "0";

            var grpWSUrl = ConfigurationManager.AppSettings["grpWSUrl"].ToString();

            var grpWS = new HW.Grp.WebService.Soap();

            var responseWS = grpWS.ExportAll(token, groupBy, fromYear, toYear, fromMonth, toMonth, langID, project, grouping, sponsorAdminID, sponsorID, departmentIDs, plot, type, HasAnswerKey, grpWSUrl, 20);
            var objectData = new MemoryStream(Convert.FromBase64String(responseWS.Base64StreamData));
            var content = responseWS.Content;

            byte[] bytes = objectData.ToArray();
            objectData.Close();


            Response.Clear();
            Response.ContentType = responseWS.ContentType;
            Response.AddHeader("content-disposition", content);
            Response.BinaryWrite(bytes);
            Response.End();


            //IAdmin sponsor = service.ReadSponsor(sponsorID);
            //reportParts = service.FindByProjectAndLanguage(projectRoundUnitID, langID);

            //ProjectRoundUnit projectRoundUnit = service.ReadProjectRoundUnit(projectRoundUnitID);
            //var sponsorAdmin = service.ReadSponsorAdmin(sponsorAdminID);

            //var exporter = ExportFactory.GetExporterAll(service, type, HasAnswerKey, hasGrouping, reportParts, Server.MapPath("HW template for Word.docx"));
            //exporter.CellWrite += delegate(object sender2, ExcelCellEventArgs e2) {
            //	e2.ExcelCell.Value = R.Str(langID, e2.ValueKey, "");
            //	e2.Writer.WriteCell(e2.ExcelCell);
            //};

            //Response.ContentType = exporter.Type;

            //HtmlHelper.AddHeaderIf(exporter.HasContentDisposition2, "content-disposition", exporter.ContentDisposition2, Response);
            //string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;

            //exporter.UrlSet += delegate(object sender2, ReportPartEventArgs e2) { e2.Url = GetReportImageUrl(path, langID, dateFrom.Year, dateTo.Year, sponsorAdminID, sponsorID, groupBy, e2.ReportPart.Id, projectRoundUnitID, departmentIDs, grouping, plot, dateFrom.Month, dateTo.Month); };
            //HtmlHelper.Write(exporter.ExportAll(langID, projectRoundUnit, dateFrom, dateTo, groupBy, plot, grouping, sponsorAdmin, sponsor as Sponsor, departmentIDs), Response);
        }
		
		//string GetReportImageUrl(string path, int langID, int yearFrom, int yearTo, int sposorAdminID, int sponsorID, int groupBy, int reportPartID, int projectRoundUnitID, string departmentIDs, int grouping, int plot, int monthFrom, int monthTo)
		//{
		//	P p = new P(path, "reportImage.aspx");
		//	p.Q.Add("LangID", langID);
		//	p.Q.Add("FY", yearFrom);
		//	p.Q.Add("TY", yearTo);
		//	p.Q.Add("FM", monthFrom);
		//	p.Q.Add("TM", monthTo);
		//	p.Q.Add("SAID", sposorAdminID);
		//	p.Q.Add("SID", sponsorID);
		//	p.Q.Add("GB", groupBy);
		//	p.Q.Add("RPID", reportPartID);
		//	p.Q.Add("PRUID", projectRoundUnitID);
		//	p.Q.Add("GID", departmentIDs);
		//	p.Q.Add("GRPNG", grouping);
		//	p.Q.Add("PLOT", plot);
		//	return p.ToString();
		//}
	}
}