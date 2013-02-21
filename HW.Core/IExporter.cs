//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HW.Core
{
	public class ExportFactory
	{
		public static readonly string Pdf = "pdf";
		public static readonly string Csv = "csv";
		
		public static IExporter GetExporter(IAnswerRepository answerRepository, IReportRepository reportRepository, IProjectRepository projectRepository, IOptionRepository optionRepository, IDepartmentRepository departmentRepository, IQuestionRepository questionRepository, IIndexRepository indexRepository, string type, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, ReportPart r, string key)
		{
			if (type == Pdf) {
				return new PdfExporter();
			} else if (type == Csv) {
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
		
		object Export(int GB, int fy, int ty, int langID, int rpid, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int point);
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
			get { return "attachment;filename=Report.csv"; }
		}
		
		public object Export(int GB, int fy, int ty, int langID, int rpid, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int point)
		{
			var f = GraphFactory.CreateFactory(hasAnswerKey, answerRepository, reportRepository, projectRepository, optionRepository, departmentRepository, questionRepository, indexRepository);
			return f.CreateGraph2(key, rpid, langID, PRUID, r.Type, fy, ty, r.Components.Count, r.RequiredAnswerCount, r.Option.Id, r.Question.Id, GB, hasGrouping, plot, width, height, background, GRPNG, SPONS, SID, GID, disabled, point);
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
		
		public object Export(int GB, int fy, int ty, int langID, int rpid, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int point)
		{
			Document doc = new Document();
			var output = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(doc, output);
			doc.Open();
			
			string url = string.Format(
				@"{12}reportImage.aspx?LangID={0}&FY={1}&TY={2}&SAID={3}&SID={4}&STDEV={5}&ExtraPoint={13}&GB={6}&RPID={7}&PRUID={8}&GID={9}&GRPNG={10}&Plot={11}",
				langID,
				fy,
				ty,
				SPONS,
				SID,
				0, // TODO: No use for standard deviation. Current use is extra point or "distribution".
				GB,
				rpid,
				PRUID,
				GID,
				GRPNG,
				plot,
				path,
				point
			);
			iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(url));
			jpg.ScaleToFit(500f, 500f);
			doc.Add(jpg);
			doc.Close();
			return output;
		}
	}
}
