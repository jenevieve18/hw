//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;

namespace HW.Core.Helpers
{
	public class PresentationDocumentExporter : IExporter
	{
		public string Type {
			get { return "application/octet-stream"; }
		}
		
		public bool HasContentDisposition {
			get { return ContentDisposition.Length > 0; }
		}
		
		public string ContentDisposition {
			get { return "attachment;filename=Report.pptx;"; }
		}
		
		public object Export(int GB, int fy, int ty, int langID, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot,	string path, int distribution)
		{
			MemoryStream output = new MemoryStream();
			using (PresentationDocument doc = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
				doc.AddPresentationPart();
				InsertNewSlide(doc, 1, "Hello world");
			}
			return output;
		}
		
		public object Export2(int GB, int fy, int ty, int langID, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int distribution)
		{
			throw new NotImplementedException();
		}
		
		public static void InsertNewSlide(PresentationDocument presentationDocument, int position, string slideTitle)
		{
			PresentationPart presentationPart = presentationDocument.PresentationPart;

			Slide slide = new Slide(new CommonSlideData(new ShapeTree()));

			// Construct the slide content.
			// Specify the non-visual properties of the new slide.
			NonVisualGroupShapeProperties nonVisualProperties = slide.CommonSlideData.ShapeTree.AppendChild(new NonVisualGroupShapeProperties());
			nonVisualProperties.NonVisualDrawingProperties = new NonVisualDrawingProperties() { Id = 1, Name = "" };
			nonVisualProperties.NonVisualGroupShapeDrawingProperties = new NonVisualGroupShapeDrawingProperties();
			nonVisualProperties.ApplicationNonVisualDrawingProperties = new ApplicationNonVisualDrawingProperties();

			// Specify the group shape properties of the new slide.
			slide.CommonSlideData.ShapeTree.AppendChild(new GroupShapeProperties());
		}
	}
}
