// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using HW.EForm.Core.Models;

namespace HW.EForm.Core.Helpers
{
	public class PromasExporter
	{
		GeneratedCode.GeneratedClass gc = new GeneratedCode.GeneratedClass();
		
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
		
		public object Export(Feedback feedback)
		{
			MemoryStream output = new MemoryStream();
			using (PresentationDocument document = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
				gc.CreateParts(document, feedback);
			}
			return output;
		}
	}
}