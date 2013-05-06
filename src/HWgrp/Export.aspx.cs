//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HWgrp
{
	public partial class Export : System.Web.UI.Page
	{
//		IReportRepository reportRepository = AppContext.GetRepositoryFactory().CreateReportRepository();
//		IAnswerRepository answerRepository = AppContext.GetRepositoryFactory().CreateAnswerRepository();
//		IProjectRepository projectRepository = AppContext.GetRepositoryFactory().CreateProjectRepository();
//		IOptionRepository optionRepository = AppContext.GetRepositoryFactory().CreateOptionRepository();
//		IDepartmentRepository departmentRepository = AppContext.GetRepositoryFactory().CreateDepartmentRepository();
//		IIndexRepository indexRepository = AppContext.GetRepositoryFactory().CreateIndexRepository();
//		IQuestionRepository questionRepository = AppContext.GetRepositoryFactory().CreateQuestionRepository();
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
			int GB = (HttpContext.Current.Request.QueryString["GB"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["GB"].ToString()) : 0);
			int stdev = Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]);
			
			int fy = HttpContext.Current.Request.QueryString["FY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) : 0;
			int ty = HttpContext.Current.Request.QueryString["TY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) : 0;
			
			int langID = (HttpContext.Current.Request.QueryString["LangID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["LangID"]) : 0);

			int rpid = Convert.ToInt32(HttpContext.Current.Request.QueryString["RPID"]);
			int PRUID = Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]);
			
			int GRPNG = Convert.ToInt32(HttpContext.Current.Request.QueryString["GRPNG"]);
			int SPONS = Convert.ToInt32((HttpContext.Current.Request.QueryString["SAID"] != null ? HttpContext.Current.Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
			int SID = Convert.ToInt32((HttpContext.Current.Request.QueryString["SID"] != null ? HttpContext.Current.Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
			string GID = (HttpContext.Current.Request.QueryString["GID"] != null ? HttpContext.Current.Request.QueryString["GID"].ToString().Replace(" ", "") : "");
			string plot = HttpContext.Current.Request.QueryString["Plot"] != null ? HttpContext.Current.Request.QueryString["Plot"].ToString() : "LinePlot";
			string type = HttpContext.Current.Request.QueryString["TYPE"].ToString();
			
			bool hasGrouping = HttpContext.Current.Request.QueryString["GRPNG"] != null || HttpContext.Current.Request.QueryString["GRPNG"] != "0";
			string key = HttpContext.Current.Request.QueryString["AK"];
			
			object disabled = HttpContext.Current.Request.QueryString["DISABLED"];
			
			int point = HttpContext.Current.Request.QueryString["ExtraPoint"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ExtraPoint"]) : 0;
			
//			ReportPart r = reportRepository.ReadReportPart(rpid);
			ReportPart r = service.ReadReportPart(rpid, langID);
			
//			var exporter = ExportFactory.GetExporter(answerRepository, reportRepository, projectRepository, optionRepository, departmentRepository, questionRepository, indexRepository, type, HasAnswerKey, hasGrouping, disabled, Width, Height, Background, r, key);
			var exporter = ExportFactory.GetExporter(service, type, HasAnswerKey, hasGrouping, disabled, Width, Height, Background, r, key);
			Response.ContentType = exporter.Type;
			AddHeaderIf(exporter.HasContentDisposition, "content-disposition", exporter.ContentDisposition);
			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
//			Write(exporter.Export(GB, fy, ty, langID, rpid, PRUID, GRPNG, SPONS, SID, GID, plot, path, point));
//			Write(exporter.Export(GB, fy, ty, langID, PRUID, GRPNG, SPONS, SID, GID, plot, path, point));
			Write(exporter.Export(GB, fy, ty, langID, PRUID, GRPNG, SPONS, SID, GID, plot, path));
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
	}
}