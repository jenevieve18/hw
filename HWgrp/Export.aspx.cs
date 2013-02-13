using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HWgrp
{
	public partial class Export : System.Web.UI.Page
	{
		IReportRepository reportRepository = AppContext.GetRepositoryFactory().CreateReportRepository();
		IAnswerRepository answerRepository = AppContext.GetRepositoryFactory().CreateAnswerRepository();
		IProjectRepository projectRepository = AppContext.GetRepositoryFactory().CreateProjectRepository();
		IOptionRepository optionRepository = AppContext.GetRepositoryFactory().CreateOptionRepository();
		IDepartmentRepository departmentRepository = AppContext.GetRepositoryFactory().CreateDepartmentRepository();
		IIndexRepository indexRepository = AppContext.GetRepositoryFactory().CreateIndexRepository();
		IQuestionRepository questionRepository = AppContext.GetRepositoryFactory().CreateQuestionRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int GB = (HttpContext.Current.Request.QueryString["GB"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["GB"].ToString()) : 0);
//			bool stdev = (HttpContext.Current.Request.QueryString["STDEV"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 : false);
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
			string type = HttpContext.Current.Request.QueryString["type"].ToString();
			
			bool hasGrouping = HttpContext.Current.Request.QueryString["GRPNG"] != null || HttpContext.Current.Request.QueryString["GRPNG"] != "0";
			string key = HttpContext.Current.Request.QueryString["AK"];
			
			object disabled = HttpContext.Current.Request.QueryString["DISABLED"];
			
			ReportPart r = reportRepository.ReadReportPart(rpid);
			
			var exporter = ExportFactory.GetExporter(answerRepository, reportRepository, projectRepository, optionRepository, departmentRepository, questionRepository, indexRepository, type, HasAnswerKey, hasGrouping, disabled, Width, Height, Background, r, key);
			Response.ContentType = exporter.Type;
			AddHeaderIf(exporter.HasContentDisposition, "content-disposition", exporter.ContentDisposition);
			Write(exporter.Export(GB, stdev, fy, ty, langID, rpid, PRUID, GRPNG, SPONS, SID, GID, plot, Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath));
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
	
	public class ExportFactory
	{
		public static IExporter GetExporter(IAnswerRepository answerRepository, IReportRepository reportRepository, IProjectRepository projectRepository, IOptionRepository optionRepository, IDepartmentRepository departmentRepository, IQuestionRepository questionRepository, IIndexRepository indexRepository, string type, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, ReportPart r, string key)
		{
			if (type == "pdf") {
				return new PdfExporter();
			} else if (type == "csv") {
				return new CsvExporter(answerRepository, reportRepository, projectRepository, optionRepository, departmentRepository, questionRepository, indexRepository, hasAnswerKey, hasGrouping, disabled, width, height, background, r, key);
			} else {
				throw new NotSupportedException();
			}
		}
	}
	
	public interface IExporter
	{
		string Type { get; }
		
		bool HasContentDisposition { get; }
		
		string ContentDisposition { get; }
		
		object Export(int GB, int stdev, int fy, int ty, int langID, int rpid, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path);
	}
	
	public class CsvExporter : IExporter
	{
		IAnswerRepository answerRepository;
		IReportRepository reportRepository;
		IProjectRepository projectRepository;
		IOptionRepository optionRepository;
		IDepartmentRepository departmentRepository;
		IQuestionRepository questionRepository;
		IIndexRepository indexRepository;
		bool hasAnswerKey;
		bool hasGrouping;
		object disabled;
		int width;
		int height;
		string background;
		ReportPart r;
		string key;
		
		public CsvExporter(IAnswerRepository answerRepository, IReportRepository reportRepository, IProjectRepository projectRepository, IOptionRepository optionRepository, IDepartmentRepository departmentRepository, IQuestionRepository questionRepository, IIndexRepository indexRepository, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, ReportPart r, string key)
		{
			this.answerRepository = answerRepository;
			this.reportRepository = reportRepository;
			this.projectRepository = projectRepository;
			this.optionRepository = optionRepository;
			this.departmentRepository = departmentRepository;
			this.questionRepository = questionRepository;
			this.indexRepository = indexRepository;
			this.hasAnswerKey = hasAnswerKey;
			this.hasGrouping = hasGrouping;
			this.disabled = disabled;
			this.width = width;
			this.height = height;
			this.background = background;
			this.r = r;
			this.key = key;
		}
		
		public string Type {
			get { return "text/csv"; }
		}
		
		public bool HasContentDisposition {
			get { return ContentDisposition.Length > 0; }
		}
		
		public string ContentDisposition {
			get { return "attachment;filename=test.csv"; }
		}
		
		public object Export(int GB, int stdev, int fy, int ty, int langID, int rpid, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path)
		{
//			StringBuilder s = new StringBuilder();
//			for (int i = 0; i < 10; i++) {
//				for (int j = 0; j < 10; j++) {
//					s.Append((j * i).ToString());
//					s.Append(",");
//				}
//				s.Append(Environment.NewLine);
//			}
//			
			var f = GraphFactory.CreateFactory(hasAnswerKey, answerRepository, reportRepository, projectRepository, optionRepository, departmentRepository, questionRepository, indexRepository);
//			var g = f.CreateGraph(key, rpid, langID, PRUID, r.Type, fy, ty, r.Components.Count, r.RequiredAnswerCount, r.Option.Id, r.Question.Id, GB, stdev == 1, hasGrouping, plot, width, height, background, GRPNG, SPONS, SID, GID, disabled);
			return f.CreateGraph2(key, rpid, langID, PRUID, r.Type, fy, ty, r.Components.Count, r.RequiredAnswerCount, r.Option.Id, r.Question.Id, GB, stdev == 1, hasGrouping, plot, width, height, background, GRPNG, SPONS, SID, GID, disabled);
//			g.render();
			
//			return s.ToString();
		}
	}
	
	public class PdfExporter : IExporter
	{
		public string Type {
			get { return "application/pdf"; }
		}
		
		public bool HasContentDisposition {
			get { return ContentDisposition.Length > 0; }
		}
		
		public string ContentDisposition {
			get { return ""; }
		}
		
		public object Export(int GB, int stdev, int fy, int ty, int langID, int rpid, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path)
		{
			Document doc = new Document();
			var output = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(doc, output);
			doc.Open();
			
			string url = string.Format(
				@"{12}reportImage.aspx?LangID={0}&FY={1}&TY={2}&SAID={3}&SID={4}&STDEV={5}&GB={6}&RPID={7}&PRUID={8}&GID={9}&GRPNG={10}&Plot={11}",
				langID,
				fy,
				ty,
				SPONS,
				SID,
				stdev,
				GB,
				rpid,
				PRUID,
				GID,
				GRPNG,
				plot,
				path
			);
			iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(url));
			jpg.ScaleToFit(500f, 500f);
			doc.Add(jpg);
			doc.Close();
			return output;
		}
	}
}