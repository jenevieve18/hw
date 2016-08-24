// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

namespace HW.EForm.Core.Helpers
{
	public class PromasExporter
	{
		PromasTemplateGeneratedClass gc = new PromasTemplateGeneratedClass();
		
		public string Type {
			get { return "application/vnd.openxmlformats-officedocument.presentationml.presentation"; }
		}
		
		public PromasExporter()
		{
		}
		
		public bool HasContentDisposition(string file)
		{
			return GetContentDisposition(file).Length > 0;
		}
		
		public string GetContentDisposition(string file)
		{
			return string.Format("attachment;filename=\"HealthWatch {0} {1}.pptx\";", file, DateTime.Now.ToString("yyyyMMdd"));
		}
		
//		public string ContentDisposition2 {
//			get { return string.Format("attachment;filename=\"HealthWatch Survey {0}.pptx\";", DateTime.Now.ToString("yyyyMMdd")); }
//		}
		
//		public object Export(string url, int langID, int pruid, int fy, int ty, int gb, int plot, int grpng, int spons, int sid, string gid, int sponsorMinUserCountToDisclose, int fm, int tm)
		public object Export()
		{
			MemoryStream output = new MemoryStream();
			using (PresentationDocument document = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
//				gc.UrlSet += delegate(object sender, ReportPartEventArgs e) {
//					e.Url = url;
//					OnUrlSet(e);
//				};
//				gc.CreateParts(document, new List<IReportPart>(new ReportPartLanguage[] { r.CurrentLanguage }));
				gc.CreateParts(document);
			}
			return output;
		}
	}
}