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
using HW.Core.Repositories;
using HW.Core.Services;

namespace HWgrp.Web
{
	public partial class ExportAll : System.Web.UI.Page
	{
		protected IList<ReportPartLanguage> reportParts = null;
		ReportService service = new ReportService(
			AppContext.GetRepositoryFactory().CreateAnswerRepository(),
			AppContext.GetRepositoryFactory().CreateReportRepository(),
			AppContext.GetRepositoryFactory().CreateProjectRepository(),
			AppContext.GetRepositoryFactory().CreateOptionRepository(),
			AppContext.GetRepositoryFactory().CreateDepartmentRepository(),
			AppContext.GetRepositoryFactory().CreateQuestionRepository(),
			AppContext.GetRepositoryFactory().CreateIndexRepository()
			
		);
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int GB = (Request.QueryString["GB"] != null ? Convert.ToInt32(Request.QueryString["GB"].ToString()) : 0);
			int stdev = Convert.ToInt32(Request.QueryString["STDEV"]);
			
			int fy = Request.QueryString["FY"] != null ? Convert.ToInt32(Request.QueryString["FY"]) : 0;
			int ty = Request.QueryString["TY"] != null ? Convert.ToInt32(Request.QueryString["TY"]) : 0;
			
			int langID = (Request.QueryString["LangID"] != null ? Convert.ToInt32(Request.QueryString["LangID"]) : 0);

			int rpid = Convert.ToInt32(Request.QueryString["RPID"]);
			int PRUID = Convert.ToInt32(Request.QueryString["PRUID"]);
			
			int GRPNG = Convert.ToInt32(Request.QueryString["GRPNG"]);
			int SPONS = Convert.ToInt32((Request.QueryString["SAID"] != null ? Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
			int SID = Convert.ToInt32((Request.QueryString["SID"] != null ? Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
			string GID = (Request.QueryString["GID"] != null ? Request.QueryString["GID"].ToString().Replace(" ", "") : "");
			string plot = Request.QueryString["PLOT"] != null ? Request.QueryString["PLOT"].ToString() : "LinePlot";
			string type = Request.QueryString["type"].ToString();
			
			bool hasGrouping = Request.QueryString["GRPNG"] != null || Request.QueryString["GRPNG"] != "0";
			string key = Request.QueryString["AK"];
			
			object disabled = Request.QueryString["DISABLED"];
			
			int distribution = Request.QueryString["DIST"] != null ? Convert.ToInt32(Request.QueryString["DIST"]) : 0;
			
			reportParts = service.FindByProjectAndLanguage(PRUID, langID);
			
			var exporter = ExportFactory.GetExporter2(service, type, HasAnswerKey, hasGrouping, disabled, Width, Height, Background, reportParts, key);
			Response.ContentType = exporter.Type;
			AddHeaderIf(exporter.HasContentDisposition, "content-disposition", exporter.ContentDisposition);
			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
			Write(exporter.Export2(GB, fy, ty, langID, PRUID, GRPNG, SPONS, SID, GID, plot, path, distribution));
		}
		
		void Write(object obj)
		{
			if (obj is MemoryStream) {
				Response.BinaryWrite(((MemoryStream)obj).ToArray());
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
	}
}