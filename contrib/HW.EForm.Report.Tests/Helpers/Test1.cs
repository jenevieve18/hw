// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Helpers
{
	[TestFixture]
	public class Test1
	{
		[Test]
		public void TestMethod()
		{
			using (WordprocessingDocument package = WordprocessingDocument.Create("test.docx", WordprocessingDocumentType.Document))
			{
				package.AddMainDocumentPart();

				package.MainDocumentPart.Document =
					new DocumentFormat.OpenXml.Wordprocessing.Document(
						new DocumentFormat.OpenXml.Wordprocessing.Body(
							new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
								new DocumentFormat.OpenXml.Wordprocessing.Run(
									new DocumentFormat.OpenXml.Wordprocessing.Text("Hello World!")))));

				package.MainDocumentPart.Document.Save();
			}
		}
	}
}
