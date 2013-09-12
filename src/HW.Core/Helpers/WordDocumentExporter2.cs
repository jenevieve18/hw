//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using HW.Core.Models;
using HW.Core.Services;

using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Pic = DocumentFormat.OpenXml.Drawing.Pictures;
using V = DocumentFormat.OpenXml.Vml;
using Ovml = DocumentFormat.OpenXml.Vml.Office;
using M = DocumentFormat.OpenXml.Math;

namespace HW.Core.Helpers
{
	public class WordDocumentExporter2 : AbstractExporter
	{
		ReportPart r;
		IList<ReportPartLanguage> parts;
		ReportService service;
		
		public WordDocumentExporter2(ReportPart r)
		{
			this.r = r;
		}
		
		public WordDocumentExporter2(ReportService service, IList<ReportPartLanguage> parts)
		{
			this.service = service;
			this.parts = parts;
		}
		
		public override string Type {
			get { return "application/octet-stream"; }
		}
		
		public override string GetContentDisposition(string file)
		{
			return string.Format("attachment;filename=HealthWatch {0} {1}.docx;", file, DateTime.Now.ToString("yyyyMMdd"));
		}
		
		public override string ContentDisposition2 {
			get { return string.Format("attachment;filename=HealthWatch Survey {0}.docx;", DateTime.Now.ToString("yyyyMMdd")); }
		}
		
		public override object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, string plot,	string path)
		{
			MemoryStream output = new MemoryStream();
			using (WordprocessingDocument package = WordprocessingDocument.Create(output, WordprocessingDocumentType.Document)) {
				string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot);
				CreateParts(package, r.CurrentLanguage, url);
			}
			return output;
		}
		
		public override object Export2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, string plot, string path)
		{
			MemoryStream output = new MemoryStream();
			using (WordprocessingDocument package = WordprocessingDocument.Create(output, WordprocessingDocumentType.Document)) {
				foreach (var p in parts) {
					ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
					string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot);
					CreateParts(package, r.CurrentLanguage, url);
				}
			}

			return output;
		}

		// Adds child parts and generates content of the specified part.
		private void CreateParts(WordprocessingDocument document, ReportPartLanguage r, string url)
		{
			ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
			GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

			MainDocumentPart mainDocumentPart1 = document.AddMainDocumentPart();
			GenerateMainDocumentPart1Content(mainDocumentPart1, r);

			FontTablePart fontTablePart1 = mainDocumentPart1.AddNewPart<FontTablePart>("rId8");
			GenerateFontTablePart1Content(fontTablePart1);

			WebSettingsPart webSettingsPart1 = mainDocumentPart1.AddNewPart<WebSettingsPart>("rId3");
			GenerateWebSettingsPart1Content(webSettingsPart1);

			HeaderPart headerPart1 = mainDocumentPart1.AddNewPart<HeaderPart>("rId7");
			GenerateHeaderPart1Content(headerPart1);

			ImagePart imagePart1 = headerPart1.AddNewPart<ImagePart>("image/png", "rId2");
			GenerateImagePart1Content(imagePart1);

			ImagePart imagePart2 = headerPart1.AddNewPart<ImagePart>("image/png", "rId1");
			GenerateImagePart2Content(imagePart2);

			DocumentSettingsPart documentSettingsPart1 = mainDocumentPart1.AddNewPart<DocumentSettingsPart>("rId2");
			GenerateDocumentSettingsPart1Content(documentSettingsPart1);

			StyleDefinitionsPart styleDefinitionsPart1 = mainDocumentPart1.AddNewPart<StyleDefinitionsPart>("rId1");
			GenerateStyleDefinitionsPart1Content(styleDefinitionsPart1);

			ImagePart imagePart3 = mainDocumentPart1.AddNewPart<ImagePart>("image/gif", "rId6");
			GenerateImagePart3Content(imagePart3, url);

			EndnotesPart endnotesPart1 = mainDocumentPart1.AddNewPart<EndnotesPart>("rId5");
			GenerateEndnotesPart1Content(endnotesPart1);

			FootnotesPart footnotesPart1 = mainDocumentPart1.AddNewPart<FootnotesPart>("rId4");
			GenerateFootnotesPart1Content(footnotesPart1);

			ThemePart themePart1 = mainDocumentPart1.AddNewPart<ThemePart>("rId9");
			GenerateThemePart1Content(themePart1);

			SetPackageProperties(document);
		}

		// Generates content of extendedFilePropertiesPart1.
		private void GenerateExtendedFilePropertiesPart1Content(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
		{
			Ap.Properties properties1 = new Ap.Properties();
			properties1.AddNamespaceDeclaration("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
			Ap.Template template1 = new Ap.Template();
			template1.Text = "Normal.dotm";
			Ap.TotalTime totalTime1 = new Ap.TotalTime();
			totalTime1.Text = "3";
			Ap.Pages pages1 = new Ap.Pages();
			pages1.Text = "1";
			Ap.Words words1 = new Ap.Words();
			words1.Text = "1";
			Ap.Characters characters1 = new Ap.Characters();
			characters1.Text = "10";
			Ap.Application application1 = new Ap.Application();
			application1.Text = "Microsoft Office Word";
			Ap.DocumentSecurity documentSecurity1 = new Ap.DocumentSecurity();
			documentSecurity1.Text = "0";
			Ap.Lines lines1 = new Ap.Lines();
			lines1.Text = "1";
			Ap.Paragraphs paragraphs1 = new Ap.Paragraphs();
			paragraphs1.Text = "1";
			Ap.ScaleCrop scaleCrop1 = new Ap.ScaleCrop();
			scaleCrop1.Text = "false";

			Ap.HeadingPairs headingPairs1 = new Ap.HeadingPairs();

			Vt.VTVector vTVector1 = new Vt.VTVector(){ BaseType = Vt.VectorBaseValues.Variant, Size = (UInt32Value)2U };

			Vt.Variant variant1 = new Vt.Variant();
			Vt.VTLPSTR vTLPSTR1 = new Vt.VTLPSTR();
			vTLPSTR1.Text = "Rubrik";

			variant1.Append(vTLPSTR1);

			Vt.Variant variant2 = new Vt.Variant();
			Vt.VTInt32 vTInt321 = new Vt.VTInt32();
			vTInt321.Text = "1";

			variant2.Append(vTInt321);

			vTVector1.Append(variant1);
			vTVector1.Append(variant2);

			headingPairs1.Append(vTVector1);

			Ap.TitlesOfParts titlesOfParts1 = new Ap.TitlesOfParts();

			Vt.VTVector vTVector2 = new Vt.VTVector(){ BaseType = Vt.VectorBaseValues.Lpstr, Size = (UInt32Value)1U };
			Vt.VTLPSTR vTLPSTR2 = new Vt.VTLPSTR();
			vTLPSTR2.Text = "";

			vTVector2.Append(vTLPSTR2);

			titlesOfParts1.Append(vTVector2);
			Ap.Company company1 = new Ap.Company();
			company1.Text = "";
			Ap.LinksUpToDate linksUpToDate1 = new Ap.LinksUpToDate();
			linksUpToDate1.Text = "false";
			Ap.CharactersWithSpaces charactersWithSpaces1 = new Ap.CharactersWithSpaces();
			charactersWithSpaces1.Text = "10";
			Ap.SharedDocument sharedDocument1 = new Ap.SharedDocument();
			sharedDocument1.Text = "false";
			Ap.HyperlinksChanged hyperlinksChanged1 = new Ap.HyperlinksChanged();
			hyperlinksChanged1.Text = "false";
			Ap.ApplicationVersion applicationVersion1 = new Ap.ApplicationVersion();
			applicationVersion1.Text = "12.0000";

			properties1.Append(template1);
			properties1.Append(totalTime1);
			properties1.Append(pages1);
			properties1.Append(words1);
			properties1.Append(characters1);
			properties1.Append(application1);
			properties1.Append(documentSecurity1);
			properties1.Append(lines1);
			properties1.Append(paragraphs1);
			properties1.Append(scaleCrop1);
			properties1.Append(headingPairs1);
			properties1.Append(titlesOfParts1);
			properties1.Append(company1);
			properties1.Append(linksUpToDate1);
			properties1.Append(charactersWithSpaces1);
			properties1.Append(sharedDocument1);
			properties1.Append(hyperlinksChanged1);
			properties1.Append(applicationVersion1);

			extendedFilePropertiesPart1.Properties = properties1;
		}

		// Generates content of mainDocumentPart1.
		private void GenerateMainDocumentPart1Content(MainDocumentPart mainDocumentPart1, ReportPartLanguage r)
		{
			Document document1 = new Document();
			document1.AddNamespaceDeclaration("ve", "http://schemas.openxmlformats.org/markup-compatibility/2006");
			document1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
			document1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			document1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
			document1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
			document1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
			document1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
			document1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
			document1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");

			Body body1 = new Body();

			Paragraph paragraph1 = new Paragraph(){ RsidParagraphAddition = "00C471E5", RsidParagraphProperties = "00E424F4", RsidRunAdditionDefault = "00E424F4" };

			ParagraphProperties paragraphProperties1 = new ParagraphProperties();
			ParagraphStyleId paragraphStyleId1 = new ParagraphStyleId(){ Val = "Heading1" };

			paragraphProperties1.Append(paragraphStyleId1);

			Run run1 = new Run();
			Text text1 = new Text();
			//            text1.Text = "Heading";
			text1.Text = r.Subject;

			run1.Append(text1);

			paragraph1.Append(paragraphProperties1);
			paragraph1.Append(run1);
			Paragraph paragraph2 = new Paragraph(){ RsidParagraphAddition = "00E424F4", RsidRunAdditionDefault = "00E424F4" };

			Paragraph paragraph3 = new Paragraph(){ RsidParagraphAddition = "00F71168", RsidRunAdditionDefault = "00E424F4" };

			Run run2 = new Run();

			RunProperties runProperties1 = new RunProperties();
			NoProof noProof1 = new NoProof();
			Languages languages1 = new Languages(){ Val = "en-US" };

			runProperties1.Append(noProof1);
			runProperties1.Append(languages1);

			Drawing drawing1 = new Drawing();

			Wp.Inline inline1 = new Wp.Inline(){ DistanceFromTop = (UInt32Value)0U, DistanceFromBottom = (UInt32Value)0U, DistanceFromLeft = (UInt32Value)0U, DistanceFromRight = (UInt32Value)0U };
			Wp.Extent extent1 = new Wp.Extent(){ Cx = 5734050L, Cy = 2819400L };
			Wp.EffectExtent effectExtent1 = new Wp.EffectExtent(){ LeftEdge = 19050L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L };
			Wp.DocProperties docProperties1 = new Wp.DocProperties(){ Id = (UInt32Value)6U, Name = "Picture 6", Description = "http://localhost:3428/reportImage.aspx?LangID=1&FY=2012&TY=2013&SAID=514&SID=83&GB=7&RPID=14&RPLID=115&PRUID=2643&GRPNG=2&GID=0,923" };

			Wp.NonVisualGraphicFrameDrawingProperties nonVisualGraphicFrameDrawingProperties1 = new Wp.NonVisualGraphicFrameDrawingProperties();

			A.GraphicFrameLocks graphicFrameLocks1 = new A.GraphicFrameLocks(){ NoChangeAspect = true };
			graphicFrameLocks1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

			nonVisualGraphicFrameDrawingProperties1.Append(graphicFrameLocks1);

			A.Graphic graphic1 = new A.Graphic();
			graphic1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

			A.GraphicData graphicData1 = new A.GraphicData(){ Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" };

			Pic.Picture picture1 = new Pic.Picture();
			picture1.AddNamespaceDeclaration("pic", "http://schemas.openxmlformats.org/drawingml/2006/picture");

			Pic.NonVisualPictureProperties nonVisualPictureProperties1 = new Pic.NonVisualPictureProperties();
			Pic.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Pic.NonVisualDrawingProperties(){ Id = (UInt32Value)0U, Name = "Picture 6", Description = "http://localhost:3428/reportImage.aspx?LangID=1&FY=2012&TY=2013&SAID=514&SID=83&GB=7&RPID=14&RPLID=115&PRUID=2643&GRPNG=2&GID=0,923" };

			Pic.NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties1 = new Pic.NonVisualPictureDrawingProperties();
			A.PictureLocks pictureLocks1 = new A.PictureLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

			nonVisualPictureDrawingProperties1.Append(pictureLocks1);

			nonVisualPictureProperties1.Append(nonVisualDrawingProperties1);
			nonVisualPictureProperties1.Append(nonVisualPictureDrawingProperties1);

			Pic.BlipFill blipFill1 = new Pic.BlipFill();
			A.Blip blip1 = new A.Blip(){ Embed = "rId6" };
			A.SourceRectangle sourceRectangle1 = new A.SourceRectangle();

			A.Stretch stretch1 = new A.Stretch();
			A.FillRectangle fillRectangle1 = new A.FillRectangle();

			stretch1.Append(fillRectangle1);

			blipFill1.Append(blip1);
			blipFill1.Append(sourceRectangle1);
			blipFill1.Append(stretch1);

			Pic.ShapeProperties shapeProperties1 = new Pic.ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

			A.Transform2D transform2D1 = new A.Transform2D();
			A.Offset offset1 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents1 = new A.Extents(){ Cx = 5734050L, Cy = 2819400L };

			transform2D1.Append(offset1);
			transform2D1.Append(extents1);

			A.PresetGeometry presetGeometry1 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
			A.AdjustValueList adjustValueList1 = new A.AdjustValueList();

			presetGeometry1.Append(adjustValueList1);
			A.NoFill noFill1 = new A.NoFill();

			A.Outline outline1 = new A.Outline(){ Width = 9525 };
			A.NoFill noFill2 = new A.NoFill();
			A.Miter miter1 = new A.Miter(){ Limit = 800000 };
			A.HeadEnd headEnd1 = new A.HeadEnd();
			A.TailEnd tailEnd1 = new A.TailEnd();

			outline1.Append(noFill2);
			outline1.Append(miter1);
			outline1.Append(headEnd1);
			outline1.Append(tailEnd1);

			shapeProperties1.Append(transform2D1);
			shapeProperties1.Append(presetGeometry1);
			shapeProperties1.Append(noFill1);
			shapeProperties1.Append(outline1);

			picture1.Append(nonVisualPictureProperties1);
			picture1.Append(blipFill1);
			picture1.Append(shapeProperties1);

			graphicData1.Append(picture1);

			graphic1.Append(graphicData1);

			inline1.Append(extent1);
			inline1.Append(effectExtent1);
			inline1.Append(docProperties1);
			inline1.Append(nonVisualGraphicFrameDrawingProperties1);
			inline1.Append(graphic1);

			drawing1.Append(inline1);

			run2.Append(runProperties1);
			run2.Append(drawing1);

			paragraph3.Append(run2);

			SectionProperties sectionProperties1 = new SectionProperties(){ RsidR = "00F71168" };
			HeaderReference headerReference1 = new HeaderReference(){ Type = HeaderFooterValues.Default, Id = "rId7" };
			PageSize pageSize1 = new PageSize(){ Width = (UInt32Value)11906U, Height = (UInt32Value)16838U };
			PageMargin pageMargin1 = new PageMargin(){ Top = 1417, Right = (UInt32Value)1417U, Bottom = 1417, Left = (UInt32Value)1417U, Header = (UInt32Value)708U, Footer = (UInt32Value)708U, Gutter = (UInt32Value)0U };
			Columns columns1 = new Columns(){ Space = "708" };
			DocGrid docGrid1 = new DocGrid(){ LinePitch = 360 };

			sectionProperties1.Append(headerReference1);
			sectionProperties1.Append(pageSize1);
			sectionProperties1.Append(pageMargin1);
			sectionProperties1.Append(columns1);
			sectionProperties1.Append(docGrid1);

			body1.Append(paragraph1);
			body1.Append(paragraph2);
			body1.Append(paragraph3);
			body1.Append(sectionProperties1);

			document1.Append(body1);

			mainDocumentPart1.Document = document1;
		}

		// Generates content of fontTablePart1.
		private void GenerateFontTablePart1Content(FontTablePart fontTablePart1)
		{
			Fonts fonts1 = new Fonts();
			fonts1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			fonts1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

			Font font1 = new Font(){ Name = "Calibri" };
			Panose1Number panose1Number1 = new Panose1Number(){ Val = "020F0502020204030204" };
			FontCharSet fontCharSet1 = new FontCharSet(){ Val = "00" };
			FontFamily fontFamily1 = new FontFamily(){ Val = FontFamilyValues.Swiss };
			Pitch pitch1 = new Pitch(){ Val = FontPitchValues.Variable };
			FontSignature fontSignature1 = new FontSignature(){ UnicodeSignature0 = "E10002FF", UnicodeSignature1 = "4000ACFF", UnicodeSignature2 = "00000009", UnicodeSignature3 = "00000000", CodePageSignature0 = "0000019F", CodePageSignature1 = "00000000" };

			font1.Append(panose1Number1);
			font1.Append(fontCharSet1);
			font1.Append(fontFamily1);
			font1.Append(pitch1);
			font1.Append(fontSignature1);

			Font font2 = new Font(){ Name = "Times New Roman" };
			Panose1Number panose1Number2 = new Panose1Number(){ Val = "02020603050405020304" };
			FontCharSet fontCharSet2 = new FontCharSet(){ Val = "00" };
			FontFamily fontFamily2 = new FontFamily(){ Val = FontFamilyValues.Roman };
			Pitch pitch2 = new Pitch(){ Val = FontPitchValues.Variable };
			FontSignature fontSignature2 = new FontSignature(){ UnicodeSignature0 = "E0002AFF", UnicodeSignature1 = "C0007841", UnicodeSignature2 = "00000009", UnicodeSignature3 = "00000000", CodePageSignature0 = "000001FF", CodePageSignature1 = "00000000" };

			font2.Append(panose1Number2);
			font2.Append(fontCharSet2);
			font2.Append(fontFamily2);
			font2.Append(pitch2);
			font2.Append(fontSignature2);

			Font font3 = new Font(){ Name = "Cambria" };
			Panose1Number panose1Number3 = new Panose1Number(){ Val = "02040503050406030204" };
			FontCharSet fontCharSet3 = new FontCharSet(){ Val = "00" };
			FontFamily fontFamily3 = new FontFamily(){ Val = FontFamilyValues.Roman };
			Pitch pitch3 = new Pitch(){ Val = FontPitchValues.Variable };
			FontSignature fontSignature3 = new FontSignature(){ UnicodeSignature0 = "A00002EF", UnicodeSignature1 = "4000004B", UnicodeSignature2 = "00000000", UnicodeSignature3 = "00000000", CodePageSignature0 = "0000019F", CodePageSignature1 = "00000000" };

			font3.Append(panose1Number3);
			font3.Append(fontCharSet3);
			font3.Append(fontFamily3);
			font3.Append(pitch3);
			font3.Append(fontSignature3);

			Font font4 = new Font(){ Name = "Tahoma" };
			Panose1Number panose1Number4 = new Panose1Number(){ Val = "020B0604030504040204" };
			FontCharSet fontCharSet4 = new FontCharSet(){ Val = "00" };
			FontFamily fontFamily4 = new FontFamily(){ Val = FontFamilyValues.Swiss };
			Pitch pitch4 = new Pitch(){ Val = FontPitchValues.Variable };
			FontSignature fontSignature4 = new FontSignature(){ UnicodeSignature0 = "E1002EFF", UnicodeSignature1 = "C000605B", UnicodeSignature2 = "00000029", UnicodeSignature3 = "00000000", CodePageSignature0 = "000101FF", CodePageSignature1 = "00000000" };

			font4.Append(panose1Number4);
			font4.Append(fontCharSet4);
			font4.Append(fontFamily4);
			font4.Append(pitch4);
			font4.Append(fontSignature4);

			fonts1.Append(font1);
			fonts1.Append(font2);
			fonts1.Append(font3);
			fonts1.Append(font4);

			fontTablePart1.Fonts = fonts1;
		}

		// Generates content of webSettingsPart1.
		private void GenerateWebSettingsPart1Content(WebSettingsPart webSettingsPart1)
		{
			WebSettings webSettings1 = new WebSettings();
			webSettings1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			webSettings1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
			OptimizeForBrowser optimizeForBrowser1 = new OptimizeForBrowser();
			AllowPNG allowPNG1 = new AllowPNG();
			TargetScreenSize targetScreenSize1 = new TargetScreenSize(){ Val = TargetScreenSizeValues.Sz1024x768 };

			webSettings1.Append(optimizeForBrowser1);
			webSettings1.Append(allowPNG1);
			webSettings1.Append(targetScreenSize1);

			webSettingsPart1.WebSettings = webSettings1;
		}

		// Generates content of headerPart1.
		private void GenerateHeaderPart1Content(HeaderPart headerPart1)
		{
			Header header1 = new Header();
			header1.AddNamespaceDeclaration("ve", "http://schemas.openxmlformats.org/markup-compatibility/2006");
			header1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
			header1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			header1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
			header1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
			header1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
			header1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
			header1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
			header1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");

			Paragraph paragraph4 = new Paragraph(){ RsidParagraphAddition = "00F71168", RsidRunAdditionDefault = "008D5805" };

			ParagraphProperties paragraphProperties2 = new ParagraphProperties();
			ParagraphStyleId paragraphStyleId2 = new ParagraphStyleId(){ Val = "Header" };

			paragraphProperties2.Append(paragraphStyleId2);

			Run run3 = new Run();

			RunProperties runProperties2 = new RunProperties();
			NoProof noProof2 = new NoProof();

			runProperties2.Append(noProof2);

			Picture picture2 = new Picture();

			V.Shapetype shapetype1 = new V.Shapetype(){ Id = "_x0000_t202", CoordinateSize = "21600,21600", OptionalNumber = 202, EdgePath = "m,l,21600r21600,l21600,xe" };
			V.Stroke stroke1 = new V.Stroke(){ JoinStyle = V.StrokeJoinStyleValues.Miter };
			V.Path path1 = new V.Path(){ AllowGradientShape = true, ConnectionPointType = Ovml.ConnectValues.Rectangle };

			shapetype1.Append(stroke1);
			shapetype1.Append(path1);

			V.Shape shape1 = new V.Shape(){ Id = "_x0000_s2051", Style = "position:absolute;margin-left:356.9pt;margin-top:-14.7pt;width:153.5pt;height:60.2pt;z-index:251658240;visibility:visible;mso-height-percent:200;mso-height-percent:200;mso-width-relative:margin;mso-height-relative:margin", Stroked = false, Type = "#_x0000_t202", EncodedPackage = "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\n" };

			V.TextBox textBox1 = new V.TextBox(){ Style = "mso-fit-shape-to-text:t" };

			TextBoxContent textBoxContent1 = new TextBoxContent();

			Paragraph paragraph5 = new Paragraph(){ RsidParagraphAddition = "008D5805", RsidRunAdditionDefault = "00E424F4" };

			Run run4 = new Run();

			RunProperties runProperties3 = new RunProperties();
			NoProof noProof3 = new NoProof();
			Languages languages2 = new Languages(){ Val = "en-US" };

			runProperties3.Append(noProof3);
			runProperties3.Append(languages2);

			Drawing drawing2 = new Drawing();

			Wp.Inline inline2 = new Wp.Inline(){ DistanceFromTop = (UInt32Value)0U, DistanceFromBottom = (UInt32Value)0U, DistanceFromLeft = (UInt32Value)0U, DistanceFromRight = (UInt32Value)0U };
			Wp.Extent extent2 = new Wp.Extent(){ Cx = 1647825L, Cy = 523875L };
			Wp.EffectExtent effectExtent2 = new Wp.EffectExtent(){ LeftEdge = 19050L, TopEdge = 0L, RightEdge = 9525L, BottomEdge = 0L };
			Wp.DocProperties docProperties2 = new Wp.DocProperties(){ Id = (UInt32Value)2U, Name = "Picture 2", Description = "Ploppar små2" };

			Wp.NonVisualGraphicFrameDrawingProperties nonVisualGraphicFrameDrawingProperties2 = new Wp.NonVisualGraphicFrameDrawingProperties();

			A.GraphicFrameLocks graphicFrameLocks2 = new A.GraphicFrameLocks(){ NoChangeAspect = true };
			graphicFrameLocks2.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

			nonVisualGraphicFrameDrawingProperties2.Append(graphicFrameLocks2);

			A.Graphic graphic2 = new A.Graphic();
			graphic2.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

			A.GraphicData graphicData2 = new A.GraphicData(){ Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" };

			Pic.Picture picture3 = new Pic.Picture();
			picture3.AddNamespaceDeclaration("pic", "http://schemas.openxmlformats.org/drawingml/2006/picture");

			Pic.NonVisualPictureProperties nonVisualPictureProperties2 = new Pic.NonVisualPictureProperties();
			Pic.NonVisualDrawingProperties nonVisualDrawingProperties2 = new Pic.NonVisualDrawingProperties(){ Id = (UInt32Value)0U, Name = "Picture 2", Description = "Ploppar små2" };

			Pic.NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties2 = new Pic.NonVisualPictureDrawingProperties();
			A.PictureLocks pictureLocks2 = new A.PictureLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

			nonVisualPictureDrawingProperties2.Append(pictureLocks2);

			nonVisualPictureProperties2.Append(nonVisualDrawingProperties2);
			nonVisualPictureProperties2.Append(nonVisualPictureDrawingProperties2);

			Pic.BlipFill blipFill2 = new Pic.BlipFill();
			A.Blip blip2 = new A.Blip(){ Embed = "rId1" };
			A.SourceRectangle sourceRectangle2 = new A.SourceRectangle();

			A.Stretch stretch2 = new A.Stretch();
			A.FillRectangle fillRectangle2 = new A.FillRectangle();

			stretch2.Append(fillRectangle2);

			blipFill2.Append(blip2);
			blipFill2.Append(sourceRectangle2);
			blipFill2.Append(stretch2);

			Pic.ShapeProperties shapeProperties2 = new Pic.ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

			A.Transform2D transform2D2 = new A.Transform2D();
			A.Offset offset2 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents2 = new A.Extents(){ Cx = 1647825L, Cy = 523875L };

			transform2D2.Append(offset2);
			transform2D2.Append(extents2);

			A.PresetGeometry presetGeometry2 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
			A.AdjustValueList adjustValueList2 = new A.AdjustValueList();

			presetGeometry2.Append(adjustValueList2);
			A.NoFill noFill3 = new A.NoFill();

			A.Outline outline2 = new A.Outline(){ Width = 9525 };
			A.NoFill noFill4 = new A.NoFill();
			A.Miter miter2 = new A.Miter(){ Limit = 800000 };
			A.HeadEnd headEnd2 = new A.HeadEnd();
			A.TailEnd tailEnd2 = new A.TailEnd();

			outline2.Append(noFill4);
			outline2.Append(miter2);
			outline2.Append(headEnd2);
			outline2.Append(tailEnd2);

			shapeProperties2.Append(transform2D2);
			shapeProperties2.Append(presetGeometry2);
			shapeProperties2.Append(noFill3);
			shapeProperties2.Append(outline2);

			picture3.Append(nonVisualPictureProperties2);
			picture3.Append(blipFill2);
			picture3.Append(shapeProperties2);

			graphicData2.Append(picture3);

			graphic2.Append(graphicData2);

			inline2.Append(extent2);
			inline2.Append(effectExtent2);
			inline2.Append(docProperties2);
			inline2.Append(nonVisualGraphicFrameDrawingProperties2);
			inline2.Append(graphic2);

			drawing2.Append(inline2);

			run4.Append(runProperties3);
			run4.Append(drawing2);

			paragraph5.Append(run4);

			textBoxContent1.Append(paragraph5);

			textBox1.Append(textBoxContent1);

			shape1.Append(textBox1);

			picture2.Append(shapetype1);
			picture2.Append(shape1);

			run3.Append(runProperties2);
			run3.Append(picture2);

			Run run5 = new Run(){ RsidRunAddition = "00F71168" };

			RunProperties runProperties4 = new RunProperties();
			NoProof noProof4 = new NoProof();

			runProperties4.Append(noProof4);

			Picture picture4 = new Picture();

			V.Shape shape2 = new V.Shape(){ Id = "Textruta 2", Style = "position:absolute;margin-left:-56.45pt;margin-top:-16.25pt;width:108.6pt;height:58.2pt;z-index:251657216;visibility:visible;mso-width-relative:margin;mso-height-relative:margin", OptionalString = "_x0000_s2049", Stroked = false, Type = "#_x0000_t202", EncodedPackage = "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\nZXYueG1sUEsFBgAAAAAEAAQA8wAAAIgFAAAAAA==\n" };

			V.TextBox textBox2 = new V.TextBox(){ Style = "mso-next-textbox:#Textruta 2" };

			TextBoxContent textBoxContent2 = new TextBoxContent();

			Paragraph paragraph6 = new Paragraph(){ RsidParagraphAddition = "00F71168", RsidRunAdditionDefault = "00E424F4" };

			Run run6 = new Run();

			RunProperties runProperties5 = new RunProperties();
			NoProof noProof5 = new NoProof();
			Languages languages3 = new Languages(){ Val = "en-US" };

			runProperties5.Append(noProof5);
			runProperties5.Append(languages3);

			Drawing drawing3 = new Drawing();

			Wp.Inline inline3 = new Wp.Inline(){ DistanceFromTop = (UInt32Value)0U, DistanceFromBottom = (UInt32Value)0U, DistanceFromLeft = (UInt32Value)0U, DistanceFromRight = (UInt32Value)0U };
			Wp.Extent extent3 = new Wp.Extent(){ Cx = 857250L, Cy = 590550L };
			Wp.EffectExtent effectExtent3 = new Wp.EffectExtent(){ LeftEdge = 19050L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L };
			Wp.DocProperties docProperties3 = new Wp.DocProperties(){ Id = (UInt32Value)1U, Name = "Bildobjekt 1" };

			Wp.NonVisualGraphicFrameDrawingProperties nonVisualGraphicFrameDrawingProperties3 = new Wp.NonVisualGraphicFrameDrawingProperties();

			A.GraphicFrameLocks graphicFrameLocks3 = new A.GraphicFrameLocks(){ NoChangeAspect = true };
			graphicFrameLocks3.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

			nonVisualGraphicFrameDrawingProperties3.Append(graphicFrameLocks3);

			A.Graphic graphic3 = new A.Graphic();
			graphic3.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

			A.GraphicData graphicData3 = new A.GraphicData(){ Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" };

			Pic.Picture picture5 = new Pic.Picture();
			picture5.AddNamespaceDeclaration("pic", "http://schemas.openxmlformats.org/drawingml/2006/picture");

			Pic.NonVisualPictureProperties nonVisualPictureProperties3 = new Pic.NonVisualPictureProperties();
			Pic.NonVisualDrawingProperties nonVisualDrawingProperties3 = new Pic.NonVisualDrawingProperties(){ Id = (UInt32Value)0U, Name = "Bildobjekt 1" };

			Pic.NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties3 = new Pic.NonVisualPictureDrawingProperties();
			A.PictureLocks pictureLocks3 = new A.PictureLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

			nonVisualPictureDrawingProperties3.Append(pictureLocks3);

			nonVisualPictureProperties3.Append(nonVisualDrawingProperties3);
			nonVisualPictureProperties3.Append(nonVisualPictureDrawingProperties3);

			Pic.BlipFill blipFill3 = new Pic.BlipFill();
			A.Blip blip3 = new A.Blip(){ Embed = "rId2" };
			A.SourceRectangle sourceRectangle3 = new A.SourceRectangle();

			A.Stretch stretch3 = new A.Stretch();
			A.FillRectangle fillRectangle3 = new A.FillRectangle();

			stretch3.Append(fillRectangle3);

			blipFill3.Append(blip3);
			blipFill3.Append(sourceRectangle3);
			blipFill3.Append(stretch3);

			Pic.ShapeProperties shapeProperties3 = new Pic.ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

			A.Transform2D transform2D3 = new A.Transform2D();
			A.Offset offset3 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents3 = new A.Extents(){ Cx = 857250L, Cy = 590550L };

			transform2D3.Append(offset3);
			transform2D3.Append(extents3);

			A.PresetGeometry presetGeometry3 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
			A.AdjustValueList adjustValueList3 = new A.AdjustValueList();

			presetGeometry3.Append(adjustValueList3);
			A.NoFill noFill5 = new A.NoFill();

			A.Outline outline3 = new A.Outline(){ Width = 9525 };
			A.NoFill noFill6 = new A.NoFill();
			A.Miter miter3 = new A.Miter(){ Limit = 800000 };
			A.HeadEnd headEnd3 = new A.HeadEnd();
			A.TailEnd tailEnd3 = new A.TailEnd();

			outline3.Append(noFill6);
			outline3.Append(miter3);
			outline3.Append(headEnd3);
			outline3.Append(tailEnd3);

			shapeProperties3.Append(transform2D3);
			shapeProperties3.Append(presetGeometry3);
			shapeProperties3.Append(noFill5);
			shapeProperties3.Append(outline3);

			picture5.Append(nonVisualPictureProperties3);
			picture5.Append(blipFill3);
			picture5.Append(shapeProperties3);

			graphicData3.Append(picture5);

			graphic3.Append(graphicData3);

			inline3.Append(extent3);
			inline3.Append(effectExtent3);
			inline3.Append(docProperties3);
			inline3.Append(nonVisualGraphicFrameDrawingProperties3);
			inline3.Append(graphic3);

			drawing3.Append(inline3);

			run6.Append(runProperties5);
			run6.Append(drawing3);

			paragraph6.Append(run6);

			textBoxContent2.Append(paragraph6);

			textBox2.Append(textBoxContent2);

			shape2.Append(textBox2);

			picture4.Append(shape2);

			run5.Append(runProperties4);
			run5.Append(picture4);

			Run run7 = new Run();
			Text text2 = new Text(){ Space = SpaceProcessingModeValues.Preserve };
			text2.Text = "                                                            ";

			run7.Append(text2);

			paragraph4.Append(paragraphProperties2);
			paragraph4.Append(run3);
			paragraph4.Append(run5);
			paragraph4.Append(run7);

			Paragraph paragraph7 = new Paragraph(){ RsidParagraphAddition = "00F71168", RsidParagraphProperties = "008D5805", RsidRunAdditionDefault = "008D5805" };

			ParagraphProperties paragraphProperties3 = new ParagraphProperties();
			ParagraphStyleId paragraphStyleId3 = new ParagraphStyleId(){ Val = "Header" };
			Indentation indentation1 = new Indentation(){ FirstLine = "1304" };

			paragraphProperties3.Append(paragraphStyleId3);
			paragraphProperties3.Append(indentation1);

			Run run8 = new Run();
			Text text3 = new Text(){ Space = SpaceProcessingModeValues.Preserve };
			text3.Text = "                              ";

			run8.Append(text3);

			paragraph7.Append(paragraphProperties3);
			paragraph7.Append(run8);

			Paragraph paragraph8 = new Paragraph(){ RsidParagraphAddition = "00F71168", RsidRunAdditionDefault = "00F71168" };

			ParagraphProperties paragraphProperties4 = new ParagraphProperties();
			ParagraphStyleId paragraphStyleId4 = new ParagraphStyleId(){ Val = "Header" };

			paragraphProperties4.Append(paragraphStyleId4);

			paragraph8.Append(paragraphProperties4);

			Paragraph paragraph9 = new Paragraph(){ RsidParagraphAddition = "00F71168", RsidRunAdditionDefault = "00F71168" };

			ParagraphProperties paragraphProperties5 = new ParagraphProperties();
			ParagraphStyleId paragraphStyleId5 = new ParagraphStyleId(){ Val = "Header" };

			paragraphProperties5.Append(paragraphStyleId5);

			paragraph9.Append(paragraphProperties5);

			header1.Append(paragraph4);
			header1.Append(paragraph7);
			header1.Append(paragraph8);
			header1.Append(paragraph9);

			headerPart1.Header = header1;
		}

		// Generates content of imagePart1.
		private void GenerateImagePart1Content(ImagePart imagePart1)
		{
			System.IO.Stream data = GetBinaryDataStream(imagePart1Data);
			imagePart1.FeedData(data);
			data.Close();
		}

		// Generates content of imagePart2.
		private void GenerateImagePart2Content(ImagePart imagePart2)
		{
			System.IO.Stream data = GetBinaryDataStream(imagePart2Data);
			imagePart2.FeedData(data);
			data.Close();
		}

		// Generates content of documentSettingsPart1.
		private void GenerateDocumentSettingsPart1Content(DocumentSettingsPart documentSettingsPart1)
		{
			Settings settings1 = new Settings();
			settings1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
			settings1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			settings1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
			settings1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
			settings1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
			settings1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
			settings1.AddNamespaceDeclaration("sl", "http://schemas.openxmlformats.org/schemaLibrary/2006/main");
			Zoom zoom1 = new Zoom(){ Percent = "100" };
			DefaultTabStop defaultTabStop1 = new DefaultTabStop(){ Val = 1304 };
			HyphenationZone hyphenationZone1 = new HyphenationZone(){ Val = "425" };
			CharacterSpacingControl characterSpacingControl1 = new CharacterSpacingControl(){ Val = CharacterSpacingValues.DoNotCompress };

			HeaderShapeDefaults headerShapeDefaults1 = new HeaderShapeDefaults();
			Ovml.ShapeDefaults shapeDefaults1 = new Ovml.ShapeDefaults(){ Extension = V.ExtensionHandlingBehaviorValues.Edit, MaxShapeId = 3074 };

			Ovml.ShapeLayout shapeLayout1 = new Ovml.ShapeLayout(){ Extension = V.ExtensionHandlingBehaviorValues.Edit };
			Ovml.ShapeIdMap shapeIdMap1 = new Ovml.ShapeIdMap(){ Extension = V.ExtensionHandlingBehaviorValues.Edit, Data = "2" };

			shapeLayout1.Append(shapeIdMap1);

			headerShapeDefaults1.Append(shapeDefaults1);
			headerShapeDefaults1.Append(shapeLayout1);

			FootnoteDocumentWideProperties footnoteDocumentWideProperties1 = new FootnoteDocumentWideProperties();
			FootnoteSpecialReference footnoteSpecialReference1 = new FootnoteSpecialReference(){ Id = 0 };
			FootnoteSpecialReference footnoteSpecialReference2 = new FootnoteSpecialReference(){ Id = 1 };

			footnoteDocumentWideProperties1.Append(footnoteSpecialReference1);
			footnoteDocumentWideProperties1.Append(footnoteSpecialReference2);

			EndnoteDocumentWideProperties endnoteDocumentWideProperties1 = new EndnoteDocumentWideProperties();
			EndnoteSpecialReference endnoteSpecialReference1 = new EndnoteSpecialReference(){ Id = 0 };
			EndnoteSpecialReference endnoteSpecialReference2 = new EndnoteSpecialReference(){ Id = 1 };

			endnoteDocumentWideProperties1.Append(endnoteSpecialReference1);
			endnoteDocumentWideProperties1.Append(endnoteSpecialReference2);
			Compatibility compatibility1 = new Compatibility();

			Rsids rsids1 = new Rsids();
			RsidRoot rsidRoot1 = new RsidRoot(){ Val = "00F71168" };
			Rsid rsid1 = new Rsid(){ Val = "008B0A97" };
			Rsid rsid2 = new Rsid(){ Val = "008D5805" };
			Rsid rsid3 = new Rsid(){ Val = "00AB3B9F" };
			Rsid rsid4 = new Rsid(){ Val = "00C471E5" };
			Rsid rsid5 = new Rsid(){ Val = "00E424F4" };
			Rsid rsid6 = new Rsid(){ Val = "00ED2CCE" };
			Rsid rsid7 = new Rsid(){ Val = "00F71168" };

			rsids1.Append(rsidRoot1);
			rsids1.Append(rsid1);
			rsids1.Append(rsid2);
			rsids1.Append(rsid3);
			rsids1.Append(rsid4);
			rsids1.Append(rsid5);
			rsids1.Append(rsid6);
			rsids1.Append(rsid7);

			M.MathProperties mathProperties1 = new M.MathProperties();
			M.MathFont mathFont1 = new M.MathFont(){ Val = "Cambria Math" };
			M.BreakBinary breakBinary1 = new M.BreakBinary(){ Val = M.BreakBinaryOperatorValues.Before };
			M.BreakBinarySubtraction breakBinarySubtraction1 = new M.BreakBinarySubtraction(){ Val = M.BreakBinarySubtractionValues.MinusMinus };
			M.SmallFraction smallFraction1 = new M.SmallFraction(){ Val = M.BooleanValues.Off };
			M.DisplayDefaults displayDefaults1 = new M.DisplayDefaults();
			M.LeftMargin leftMargin1 = new M.LeftMargin(){ Val = (UInt32Value)0U };
			M.RightMargin rightMargin1 = new M.RightMargin(){ Val = (UInt32Value)0U };
			M.DefaultJustification defaultJustification1 = new M.DefaultJustification(){ Val = M.JustificationValues.CenterGroup };
			M.WrapIndent wrapIndent1 = new M.WrapIndent(){ Val = (UInt32Value)1440U };
			M.IntegralLimitLocation integralLimitLocation1 = new M.IntegralLimitLocation(){ Val = M.LimitLocationValues.SubscriptSuperscript };
			M.NaryLimitLocation naryLimitLocation1 = new M.NaryLimitLocation(){ Val = M.LimitLocationValues.UnderOver };

			mathProperties1.Append(mathFont1);
			mathProperties1.Append(breakBinary1);
			mathProperties1.Append(breakBinarySubtraction1);
			mathProperties1.Append(smallFraction1);
			mathProperties1.Append(displayDefaults1);
			mathProperties1.Append(leftMargin1);
			mathProperties1.Append(rightMargin1);
			mathProperties1.Append(defaultJustification1);
			mathProperties1.Append(wrapIndent1);
			mathProperties1.Append(integralLimitLocation1);
			mathProperties1.Append(naryLimitLocation1);
			ThemeFontLanguages themeFontLanguages1 = new ThemeFontLanguages(){ Val = "en-US" };
			ColorSchemeMapping colorSchemeMapping1 = new ColorSchemeMapping(){ Background1 = ColorSchemeIndexValues.Light1, Text1 = ColorSchemeIndexValues.Dark1, Background2 = ColorSchemeIndexValues.Light2, Text2 = ColorSchemeIndexValues.Dark2, Accent1 = ColorSchemeIndexValues.Accent1, Accent2 = ColorSchemeIndexValues.Accent2, Accent3 = ColorSchemeIndexValues.Accent3, Accent4 = ColorSchemeIndexValues.Accent4, Accent5 = ColorSchemeIndexValues.Accent5, Accent6 = ColorSchemeIndexValues.Accent6, Hyperlink = ColorSchemeIndexValues.Hyperlink, FollowedHyperlink = ColorSchemeIndexValues.FollowedHyperlink };

			ShapeDefaults shapeDefaults2 = new ShapeDefaults();
			Ovml.ShapeDefaults shapeDefaults3 = new Ovml.ShapeDefaults(){ Extension = V.ExtensionHandlingBehaviorValues.Edit, MaxShapeId = 3074 };

			Ovml.ShapeLayout shapeLayout2 = new Ovml.ShapeLayout(){ Extension = V.ExtensionHandlingBehaviorValues.Edit };
			Ovml.ShapeIdMap shapeIdMap2 = new Ovml.ShapeIdMap(){ Extension = V.ExtensionHandlingBehaviorValues.Edit, Data = "1" };

			shapeLayout2.Append(shapeIdMap2);

			shapeDefaults2.Append(shapeDefaults3);
			shapeDefaults2.Append(shapeLayout2);
			DecimalSymbol decimalSymbol1 = new DecimalSymbol(){ Val = "." };
			ListSeparator listSeparator1 = new ListSeparator(){ Val = "," };

			settings1.Append(zoom1);
			settings1.Append(defaultTabStop1);
			settings1.Append(hyphenationZone1);
			settings1.Append(characterSpacingControl1);
			settings1.Append(headerShapeDefaults1);
			settings1.Append(footnoteDocumentWideProperties1);
			settings1.Append(endnoteDocumentWideProperties1);
			settings1.Append(compatibility1);
			settings1.Append(rsids1);
			settings1.Append(mathProperties1);
			settings1.Append(themeFontLanguages1);
			settings1.Append(colorSchemeMapping1);
			settings1.Append(shapeDefaults2);
			settings1.Append(decimalSymbol1);
			settings1.Append(listSeparator1);

			documentSettingsPart1.Settings = settings1;
		}

		// Generates content of styleDefinitionsPart1.
		private void GenerateStyleDefinitionsPart1Content(StyleDefinitionsPart styleDefinitionsPart1)
		{
			Styles styles1 = new Styles();
			styles1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			styles1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

			DocDefaults docDefaults1 = new DocDefaults();

			RunPropertiesDefault runPropertiesDefault1 = new RunPropertiesDefault();

			RunPropertiesBaseStyle runPropertiesBaseStyle1 = new RunPropertiesBaseStyle();
			RunFonts runFonts1 = new RunFonts(){ Ascii = "Calibri", HighAnsi = "Calibri", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
			Languages languages4 = new Languages(){ Val = "en-US", EastAsia = "en-US", Bidi = "ar-SA" };

			runPropertiesBaseStyle1.Append(runFonts1);
			runPropertiesBaseStyle1.Append(languages4);

			runPropertiesDefault1.Append(runPropertiesBaseStyle1);
			ParagraphPropertiesDefault paragraphPropertiesDefault1 = new ParagraphPropertiesDefault();

			docDefaults1.Append(runPropertiesDefault1);
			docDefaults1.Append(paragraphPropertiesDefault1);

			LatentStyles latentStyles1 = new LatentStyles(){ DefaultLockedState = false, DefaultUiPriority = 99, DefaultSemiHidden = true, DefaultUnhideWhenUsed = true, DefaultPrimaryStyle = false, Count = 267 };
			LatentStyleExceptionInfo latentStyleExceptionInfo1 = new LatentStyleExceptionInfo(){ Name = "Normal", UiPriority = 0, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo2 = new LatentStyleExceptionInfo(){ Name = "heading 1", UiPriority = 9, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo3 = new LatentStyleExceptionInfo(){ Name = "heading 2", UiPriority = 9, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo4 = new LatentStyleExceptionInfo(){ Name = "heading 3", UiPriority = 9, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo5 = new LatentStyleExceptionInfo(){ Name = "heading 4", UiPriority = 9, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo6 = new LatentStyleExceptionInfo(){ Name = "heading 5", UiPriority = 9, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo7 = new LatentStyleExceptionInfo(){ Name = "heading 6", UiPriority = 9, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo8 = new LatentStyleExceptionInfo(){ Name = "heading 7", UiPriority = 9, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo9 = new LatentStyleExceptionInfo(){ Name = "heading 8", UiPriority = 9, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo10 = new LatentStyleExceptionInfo(){ Name = "heading 9", UiPriority = 9, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo11 = new LatentStyleExceptionInfo(){ Name = "toc 1", UiPriority = 39 };
			LatentStyleExceptionInfo latentStyleExceptionInfo12 = new LatentStyleExceptionInfo(){ Name = "toc 2", UiPriority = 39 };
			LatentStyleExceptionInfo latentStyleExceptionInfo13 = new LatentStyleExceptionInfo(){ Name = "toc 3", UiPriority = 39 };
			LatentStyleExceptionInfo latentStyleExceptionInfo14 = new LatentStyleExceptionInfo(){ Name = "toc 4", UiPriority = 39 };
			LatentStyleExceptionInfo latentStyleExceptionInfo15 = new LatentStyleExceptionInfo(){ Name = "toc 5", UiPriority = 39 };
			LatentStyleExceptionInfo latentStyleExceptionInfo16 = new LatentStyleExceptionInfo(){ Name = "toc 6", UiPriority = 39 };
			LatentStyleExceptionInfo latentStyleExceptionInfo17 = new LatentStyleExceptionInfo(){ Name = "toc 7", UiPriority = 39 };
			LatentStyleExceptionInfo latentStyleExceptionInfo18 = new LatentStyleExceptionInfo(){ Name = "toc 8", UiPriority = 39 };
			LatentStyleExceptionInfo latentStyleExceptionInfo19 = new LatentStyleExceptionInfo(){ Name = "toc 9", UiPriority = 39 };
			LatentStyleExceptionInfo latentStyleExceptionInfo20 = new LatentStyleExceptionInfo(){ Name = "caption", UiPriority = 35, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo21 = new LatentStyleExceptionInfo(){ Name = "Title", UiPriority = 10, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo22 = new LatentStyleExceptionInfo(){ Name = "Default Paragraph Font", UiPriority = 1 };
			LatentStyleExceptionInfo latentStyleExceptionInfo23 = new LatentStyleExceptionInfo(){ Name = "Subtitle", UiPriority = 11, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo24 = new LatentStyleExceptionInfo(){ Name = "Strong", UiPriority = 22, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo25 = new LatentStyleExceptionInfo(){ Name = "Emphasis", UiPriority = 20, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo26 = new LatentStyleExceptionInfo(){ Name = "Table Grid", UiPriority = 59, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo27 = new LatentStyleExceptionInfo(){ Name = "Placeholder Text", UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo28 = new LatentStyleExceptionInfo(){ Name = "No Spacing", UiPriority = 1, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo29 = new LatentStyleExceptionInfo(){ Name = "Light Shading", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo30 = new LatentStyleExceptionInfo(){ Name = "Light List", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo31 = new LatentStyleExceptionInfo(){ Name = "Light Grid", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo32 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 1", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo33 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 2", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo34 = new LatentStyleExceptionInfo(){ Name = "Medium List 1", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo35 = new LatentStyleExceptionInfo(){ Name = "Medium List 2", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo36 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 1", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo37 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 2", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo38 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 3", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo39 = new LatentStyleExceptionInfo(){ Name = "Dark List", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo40 = new LatentStyleExceptionInfo(){ Name = "Colorful Shading", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo41 = new LatentStyleExceptionInfo(){ Name = "Colorful List", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo42 = new LatentStyleExceptionInfo(){ Name = "Colorful Grid", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo43 = new LatentStyleExceptionInfo(){ Name = "Light Shading Accent 1", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo44 = new LatentStyleExceptionInfo(){ Name = "Light List Accent 1", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo45 = new LatentStyleExceptionInfo(){ Name = "Light Grid Accent 1", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo46 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 1 Accent 1", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo47 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 2 Accent 1", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo48 = new LatentStyleExceptionInfo(){ Name = "Medium List 1 Accent 1", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo49 = new LatentStyleExceptionInfo(){ Name = "Revision", UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo50 = new LatentStyleExceptionInfo(){ Name = "List Paragraph", UiPriority = 34, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo51 = new LatentStyleExceptionInfo(){ Name = "Quote", UiPriority = 29, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo52 = new LatentStyleExceptionInfo(){ Name = "Intense Quote", UiPriority = 30, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo53 = new LatentStyleExceptionInfo(){ Name = "Medium List 2 Accent 1", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo54 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 1 Accent 1", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo55 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 2 Accent 1", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo56 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 3 Accent 1", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo57 = new LatentStyleExceptionInfo(){ Name = "Dark List Accent 1", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo58 = new LatentStyleExceptionInfo(){ Name = "Colorful Shading Accent 1", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo59 = new LatentStyleExceptionInfo(){ Name = "Colorful List Accent 1", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo60 = new LatentStyleExceptionInfo(){ Name = "Colorful Grid Accent 1", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo61 = new LatentStyleExceptionInfo(){ Name = "Light Shading Accent 2", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo62 = new LatentStyleExceptionInfo(){ Name = "Light List Accent 2", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo63 = new LatentStyleExceptionInfo(){ Name = "Light Grid Accent 2", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo64 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 1 Accent 2", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo65 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 2 Accent 2", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo66 = new LatentStyleExceptionInfo(){ Name = "Medium List 1 Accent 2", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo67 = new LatentStyleExceptionInfo(){ Name = "Medium List 2 Accent 2", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo68 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 1 Accent 2", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo69 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 2 Accent 2", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo70 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 3 Accent 2", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo71 = new LatentStyleExceptionInfo(){ Name = "Dark List Accent 2", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo72 = new LatentStyleExceptionInfo(){ Name = "Colorful Shading Accent 2", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo73 = new LatentStyleExceptionInfo(){ Name = "Colorful List Accent 2", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo74 = new LatentStyleExceptionInfo(){ Name = "Colorful Grid Accent 2", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo75 = new LatentStyleExceptionInfo(){ Name = "Light Shading Accent 3", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo76 = new LatentStyleExceptionInfo(){ Name = "Light List Accent 3", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo77 = new LatentStyleExceptionInfo(){ Name = "Light Grid Accent 3", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo78 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 1 Accent 3", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo79 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 2 Accent 3", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo80 = new LatentStyleExceptionInfo(){ Name = "Medium List 1 Accent 3", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo81 = new LatentStyleExceptionInfo(){ Name = "Medium List 2 Accent 3", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo82 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 1 Accent 3", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo83 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 2 Accent 3", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo84 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 3 Accent 3", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo85 = new LatentStyleExceptionInfo(){ Name = "Dark List Accent 3", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo86 = new LatentStyleExceptionInfo(){ Name = "Colorful Shading Accent 3", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo87 = new LatentStyleExceptionInfo(){ Name = "Colorful List Accent 3", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo88 = new LatentStyleExceptionInfo(){ Name = "Colorful Grid Accent 3", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo89 = new LatentStyleExceptionInfo(){ Name = "Light Shading Accent 4", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo90 = new LatentStyleExceptionInfo(){ Name = "Light List Accent 4", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo91 = new LatentStyleExceptionInfo(){ Name = "Light Grid Accent 4", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo92 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 1 Accent 4", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo93 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 2 Accent 4", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo94 = new LatentStyleExceptionInfo(){ Name = "Medium List 1 Accent 4", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo95 = new LatentStyleExceptionInfo(){ Name = "Medium List 2 Accent 4", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo96 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 1 Accent 4", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo97 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 2 Accent 4", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo98 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 3 Accent 4", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo99 = new LatentStyleExceptionInfo(){ Name = "Dark List Accent 4", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo100 = new LatentStyleExceptionInfo(){ Name = "Colorful Shading Accent 4", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo101 = new LatentStyleExceptionInfo(){ Name = "Colorful List Accent 4", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo102 = new LatentStyleExceptionInfo(){ Name = "Colorful Grid Accent 4", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo103 = new LatentStyleExceptionInfo(){ Name = "Light Shading Accent 5", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo104 = new LatentStyleExceptionInfo(){ Name = "Light List Accent 5", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo105 = new LatentStyleExceptionInfo(){ Name = "Light Grid Accent 5", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo106 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 1 Accent 5", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo107 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 2 Accent 5", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo108 = new LatentStyleExceptionInfo(){ Name = "Medium List 1 Accent 5", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo109 = new LatentStyleExceptionInfo(){ Name = "Medium List 2 Accent 5", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo110 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 1 Accent 5", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo111 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 2 Accent 5", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo112 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 3 Accent 5", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo113 = new LatentStyleExceptionInfo(){ Name = "Dark List Accent 5", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo114 = new LatentStyleExceptionInfo(){ Name = "Colorful Shading Accent 5", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo115 = new LatentStyleExceptionInfo(){ Name = "Colorful List Accent 5", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo116 = new LatentStyleExceptionInfo(){ Name = "Colorful Grid Accent 5", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo117 = new LatentStyleExceptionInfo(){ Name = "Light Shading Accent 6", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo118 = new LatentStyleExceptionInfo(){ Name = "Light List Accent 6", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo119 = new LatentStyleExceptionInfo(){ Name = "Light Grid Accent 6", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo120 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 1 Accent 6", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo121 = new LatentStyleExceptionInfo(){ Name = "Medium Shading 2 Accent 6", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo122 = new LatentStyleExceptionInfo(){ Name = "Medium List 1 Accent 6", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo123 = new LatentStyleExceptionInfo(){ Name = "Medium List 2 Accent 6", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo124 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 1 Accent 6", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo125 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 2 Accent 6", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo126 = new LatentStyleExceptionInfo(){ Name = "Medium Grid 3 Accent 6", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo127 = new LatentStyleExceptionInfo(){ Name = "Dark List Accent 6", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo128 = new LatentStyleExceptionInfo(){ Name = "Colorful Shading Accent 6", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo129 = new LatentStyleExceptionInfo(){ Name = "Colorful List Accent 6", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo130 = new LatentStyleExceptionInfo(){ Name = "Colorful Grid Accent 6", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
			LatentStyleExceptionInfo latentStyleExceptionInfo131 = new LatentStyleExceptionInfo(){ Name = "Subtle Emphasis", UiPriority = 19, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo132 = new LatentStyleExceptionInfo(){ Name = "Intense Emphasis", UiPriority = 21, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo133 = new LatentStyleExceptionInfo(){ Name = "Subtle Reference", UiPriority = 31, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo134 = new LatentStyleExceptionInfo(){ Name = "Intense Reference", UiPriority = 32, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo135 = new LatentStyleExceptionInfo(){ Name = "Book Title", UiPriority = 33, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
			LatentStyleExceptionInfo latentStyleExceptionInfo136 = new LatentStyleExceptionInfo(){ Name = "Bibliography", UiPriority = 37 };
			LatentStyleExceptionInfo latentStyleExceptionInfo137 = new LatentStyleExceptionInfo(){ Name = "TOC Heading", UiPriority = 39, PrimaryStyle = true };

			latentStyles1.Append(latentStyleExceptionInfo1);
			latentStyles1.Append(latentStyleExceptionInfo2);
			latentStyles1.Append(latentStyleExceptionInfo3);
			latentStyles1.Append(latentStyleExceptionInfo4);
			latentStyles1.Append(latentStyleExceptionInfo5);
			latentStyles1.Append(latentStyleExceptionInfo6);
			latentStyles1.Append(latentStyleExceptionInfo7);
			latentStyles1.Append(latentStyleExceptionInfo8);
			latentStyles1.Append(latentStyleExceptionInfo9);
			latentStyles1.Append(latentStyleExceptionInfo10);
			latentStyles1.Append(latentStyleExceptionInfo11);
			latentStyles1.Append(latentStyleExceptionInfo12);
			latentStyles1.Append(latentStyleExceptionInfo13);
			latentStyles1.Append(latentStyleExceptionInfo14);
			latentStyles1.Append(latentStyleExceptionInfo15);
			latentStyles1.Append(latentStyleExceptionInfo16);
			latentStyles1.Append(latentStyleExceptionInfo17);
			latentStyles1.Append(latentStyleExceptionInfo18);
			latentStyles1.Append(latentStyleExceptionInfo19);
			latentStyles1.Append(latentStyleExceptionInfo20);
			latentStyles1.Append(latentStyleExceptionInfo21);
			latentStyles1.Append(latentStyleExceptionInfo22);
			latentStyles1.Append(latentStyleExceptionInfo23);
			latentStyles1.Append(latentStyleExceptionInfo24);
			latentStyles1.Append(latentStyleExceptionInfo25);
			latentStyles1.Append(latentStyleExceptionInfo26);
			latentStyles1.Append(latentStyleExceptionInfo27);
			latentStyles1.Append(latentStyleExceptionInfo28);
			latentStyles1.Append(latentStyleExceptionInfo29);
			latentStyles1.Append(latentStyleExceptionInfo30);
			latentStyles1.Append(latentStyleExceptionInfo31);
			latentStyles1.Append(latentStyleExceptionInfo32);
			latentStyles1.Append(latentStyleExceptionInfo33);
			latentStyles1.Append(latentStyleExceptionInfo34);
			latentStyles1.Append(latentStyleExceptionInfo35);
			latentStyles1.Append(latentStyleExceptionInfo36);
			latentStyles1.Append(latentStyleExceptionInfo37);
			latentStyles1.Append(latentStyleExceptionInfo38);
			latentStyles1.Append(latentStyleExceptionInfo39);
			latentStyles1.Append(latentStyleExceptionInfo40);
			latentStyles1.Append(latentStyleExceptionInfo41);
			latentStyles1.Append(latentStyleExceptionInfo42);
			latentStyles1.Append(latentStyleExceptionInfo43);
			latentStyles1.Append(latentStyleExceptionInfo44);
			latentStyles1.Append(latentStyleExceptionInfo45);
			latentStyles1.Append(latentStyleExceptionInfo46);
			latentStyles1.Append(latentStyleExceptionInfo47);
			latentStyles1.Append(latentStyleExceptionInfo48);
			latentStyles1.Append(latentStyleExceptionInfo49);
			latentStyles1.Append(latentStyleExceptionInfo50);
			latentStyles1.Append(latentStyleExceptionInfo51);
			latentStyles1.Append(latentStyleExceptionInfo52);
			latentStyles1.Append(latentStyleExceptionInfo53);
			latentStyles1.Append(latentStyleExceptionInfo54);
			latentStyles1.Append(latentStyleExceptionInfo55);
			latentStyles1.Append(latentStyleExceptionInfo56);
			latentStyles1.Append(latentStyleExceptionInfo57);
			latentStyles1.Append(latentStyleExceptionInfo58);
			latentStyles1.Append(latentStyleExceptionInfo59);
			latentStyles1.Append(latentStyleExceptionInfo60);
			latentStyles1.Append(latentStyleExceptionInfo61);
			latentStyles1.Append(latentStyleExceptionInfo62);
			latentStyles1.Append(latentStyleExceptionInfo63);
			latentStyles1.Append(latentStyleExceptionInfo64);
			latentStyles1.Append(latentStyleExceptionInfo65);
			latentStyles1.Append(latentStyleExceptionInfo66);
			latentStyles1.Append(latentStyleExceptionInfo67);
			latentStyles1.Append(latentStyleExceptionInfo68);
			latentStyles1.Append(latentStyleExceptionInfo69);
			latentStyles1.Append(latentStyleExceptionInfo70);
			latentStyles1.Append(latentStyleExceptionInfo71);
			latentStyles1.Append(latentStyleExceptionInfo72);
			latentStyles1.Append(latentStyleExceptionInfo73);
			latentStyles1.Append(latentStyleExceptionInfo74);
			latentStyles1.Append(latentStyleExceptionInfo75);
			latentStyles1.Append(latentStyleExceptionInfo76);
			latentStyles1.Append(latentStyleExceptionInfo77);
			latentStyles1.Append(latentStyleExceptionInfo78);
			latentStyles1.Append(latentStyleExceptionInfo79);
			latentStyles1.Append(latentStyleExceptionInfo80);
			latentStyles1.Append(latentStyleExceptionInfo81);
			latentStyles1.Append(latentStyleExceptionInfo82);
			latentStyles1.Append(latentStyleExceptionInfo83);
			latentStyles1.Append(latentStyleExceptionInfo84);
			latentStyles1.Append(latentStyleExceptionInfo85);
			latentStyles1.Append(latentStyleExceptionInfo86);
			latentStyles1.Append(latentStyleExceptionInfo87);
			latentStyles1.Append(latentStyleExceptionInfo88);
			latentStyles1.Append(latentStyleExceptionInfo89);
			latentStyles1.Append(latentStyleExceptionInfo90);
			latentStyles1.Append(latentStyleExceptionInfo91);
			latentStyles1.Append(latentStyleExceptionInfo92);
			latentStyles1.Append(latentStyleExceptionInfo93);
			latentStyles1.Append(latentStyleExceptionInfo94);
			latentStyles1.Append(latentStyleExceptionInfo95);
			latentStyles1.Append(latentStyleExceptionInfo96);
			latentStyles1.Append(latentStyleExceptionInfo97);
			latentStyles1.Append(latentStyleExceptionInfo98);
			latentStyles1.Append(latentStyleExceptionInfo99);
			latentStyles1.Append(latentStyleExceptionInfo100);
			latentStyles1.Append(latentStyleExceptionInfo101);
			latentStyles1.Append(latentStyleExceptionInfo102);
			latentStyles1.Append(latentStyleExceptionInfo103);
			latentStyles1.Append(latentStyleExceptionInfo104);
			latentStyles1.Append(latentStyleExceptionInfo105);
			latentStyles1.Append(latentStyleExceptionInfo106);
			latentStyles1.Append(latentStyleExceptionInfo107);
			latentStyles1.Append(latentStyleExceptionInfo108);
			latentStyles1.Append(latentStyleExceptionInfo109);
			latentStyles1.Append(latentStyleExceptionInfo110);
			latentStyles1.Append(latentStyleExceptionInfo111);
			latentStyles1.Append(latentStyleExceptionInfo112);
			latentStyles1.Append(latentStyleExceptionInfo113);
			latentStyles1.Append(latentStyleExceptionInfo114);
			latentStyles1.Append(latentStyleExceptionInfo115);
			latentStyles1.Append(latentStyleExceptionInfo116);
			latentStyles1.Append(latentStyleExceptionInfo117);
			latentStyles1.Append(latentStyleExceptionInfo118);
			latentStyles1.Append(latentStyleExceptionInfo119);
			latentStyles1.Append(latentStyleExceptionInfo120);
			latentStyles1.Append(latentStyleExceptionInfo121);
			latentStyles1.Append(latentStyleExceptionInfo122);
			latentStyles1.Append(latentStyleExceptionInfo123);
			latentStyles1.Append(latentStyleExceptionInfo124);
			latentStyles1.Append(latentStyleExceptionInfo125);
			latentStyles1.Append(latentStyleExceptionInfo126);
			latentStyles1.Append(latentStyleExceptionInfo127);
			latentStyles1.Append(latentStyleExceptionInfo128);
			latentStyles1.Append(latentStyleExceptionInfo129);
			latentStyles1.Append(latentStyleExceptionInfo130);
			latentStyles1.Append(latentStyleExceptionInfo131);
			latentStyles1.Append(latentStyleExceptionInfo132);
			latentStyles1.Append(latentStyleExceptionInfo133);
			latentStyles1.Append(latentStyleExceptionInfo134);
			latentStyles1.Append(latentStyleExceptionInfo135);
			latentStyles1.Append(latentStyleExceptionInfo136);
			latentStyles1.Append(latentStyleExceptionInfo137);

			Style style1 = new Style(){ Type = StyleValues.Paragraph, StyleId = "Normal", Default = true };
			StyleName styleName1 = new StyleName(){ Val = "Normal" };
			PrimaryStyle primaryStyle1 = new PrimaryStyle();

			StyleParagraphProperties styleParagraphProperties1 = new StyleParagraphProperties();
			SpacingBetweenLines spacingBetweenLines1 = new SpacingBetweenLines(){ After = "200", Line = "276", LineRule = LineSpacingRuleValues.Auto };

			styleParagraphProperties1.Append(spacingBetweenLines1);

			StyleRunProperties styleRunProperties1 = new StyleRunProperties();
			FontSize fontSize1 = new FontSize(){ Val = "22" };
			FontSizeComplexScript fontSizeComplexScript1 = new FontSizeComplexScript(){ Val = "22" };
			Languages languages5 = new Languages(){ Val = "sv-SE" };

			styleRunProperties1.Append(fontSize1);
			styleRunProperties1.Append(fontSizeComplexScript1);
			styleRunProperties1.Append(languages5);

			style1.Append(styleName1);
			style1.Append(primaryStyle1);
			style1.Append(styleParagraphProperties1);
			style1.Append(styleRunProperties1);

			Style style2 = new Style(){ Type = StyleValues.Paragraph, StyleId = "Heading1" };
			StyleName styleName2 = new StyleName(){ Val = "heading 1" };
			BasedOn basedOn1 = new BasedOn(){ Val = "Normal" };
			NextParagraphStyle nextParagraphStyle1 = new NextParagraphStyle(){ Val = "Normal" };
			LinkedStyle linkedStyle1 = new LinkedStyle(){ Val = "Heading1Char" };
			UIPriority uIPriority1 = new UIPriority(){ Val = 9 };
			PrimaryStyle primaryStyle2 = new PrimaryStyle();
			Rsid rsid8 = new Rsid(){ Val = "00E424F4" };

			StyleParagraphProperties styleParagraphProperties2 = new StyleParagraphProperties();
			KeepNext keepNext1 = new KeepNext();
			SpacingBetweenLines spacingBetweenLines2 = new SpacingBetweenLines(){ Before = "240", After = "60" };
			OutlineLevel outlineLevel1 = new OutlineLevel(){ Val = 0 };

			styleParagraphProperties2.Append(keepNext1);
			styleParagraphProperties2.Append(spacingBetweenLines2);
			styleParagraphProperties2.Append(outlineLevel1);

			StyleRunProperties styleRunProperties2 = new StyleRunProperties();
			RunFonts runFonts2 = new RunFonts(){ AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
			Bold bold1 = new Bold();
			BoldComplexScript boldComplexScript1 = new BoldComplexScript();
			Kern kern1 = new Kern(){ Val = (UInt32Value)32U };
			FontSize fontSize2 = new FontSize(){ Val = "32" };
			FontSizeComplexScript fontSizeComplexScript2 = new FontSizeComplexScript(){ Val = "32" };

			styleRunProperties2.Append(runFonts2);
			styleRunProperties2.Append(bold1);
			styleRunProperties2.Append(boldComplexScript1);
			styleRunProperties2.Append(kern1);
			styleRunProperties2.Append(fontSize2);
			styleRunProperties2.Append(fontSizeComplexScript2);

			style2.Append(styleName2);
			style2.Append(basedOn1);
			style2.Append(nextParagraphStyle1);
			style2.Append(linkedStyle1);
			style2.Append(uIPriority1);
			style2.Append(primaryStyle2);
			style2.Append(rsid8);
			style2.Append(styleParagraphProperties2);
			style2.Append(styleRunProperties2);

			Style style3 = new Style(){ Type = StyleValues.Character, StyleId = "DefaultParagraphFont", Default = true };
			StyleName styleName3 = new StyleName(){ Val = "Default Paragraph Font" };
			UIPriority uIPriority2 = new UIPriority(){ Val = 1 };
			SemiHidden semiHidden1 = new SemiHidden();
			UnhideWhenUsed unhideWhenUsed1 = new UnhideWhenUsed();

			style3.Append(styleName3);
			style3.Append(uIPriority2);
			style3.Append(semiHidden1);
			style3.Append(unhideWhenUsed1);

			Style style4 = new Style(){ Type = StyleValues.Table, StyleId = "TableNormal", Default = true };
			StyleName styleName4 = new StyleName(){ Val = "Normal Table" };
			UIPriority uIPriority3 = new UIPriority(){ Val = 99 };
			SemiHidden semiHidden2 = new SemiHidden();
			UnhideWhenUsed unhideWhenUsed2 = new UnhideWhenUsed();

			StyleTableProperties styleTableProperties1 = new StyleTableProperties();
			TableIndentation tableIndentation1 = new TableIndentation(){ Width = 0, Type = TableWidthUnitValues.Dxa };

			TableCellMarginDefault tableCellMarginDefault1 = new TableCellMarginDefault();
			TopMargin topMargin1 = new TopMargin(){ Width = "0", Type = TableWidthUnitValues.Dxa };
			TableCellLeftMargin tableCellLeftMargin1 = new TableCellLeftMargin(){ Width = 108, Type = TableWidthValues.Dxa };
			BottomMargin bottomMargin1 = new BottomMargin(){ Width = "0", Type = TableWidthUnitValues.Dxa };
			TableCellRightMargin tableCellRightMargin1 = new TableCellRightMargin(){ Width = 108, Type = TableWidthValues.Dxa };

			tableCellMarginDefault1.Append(topMargin1);
			tableCellMarginDefault1.Append(tableCellLeftMargin1);
			tableCellMarginDefault1.Append(bottomMargin1);
			tableCellMarginDefault1.Append(tableCellRightMargin1);

			styleTableProperties1.Append(tableIndentation1);
			styleTableProperties1.Append(tableCellMarginDefault1);

			style4.Append(styleName4);
			style4.Append(uIPriority3);
			style4.Append(semiHidden2);
			style4.Append(unhideWhenUsed2);
			style4.Append(styleTableProperties1);

			Style style5 = new Style(){ Type = StyleValues.Numbering, StyleId = "NoList", Default = true };
			StyleName styleName5 = new StyleName(){ Val = "No List" };
			UIPriority uIPriority4 = new UIPriority(){ Val = 99 };
			SemiHidden semiHidden3 = new SemiHidden();
			UnhideWhenUsed unhideWhenUsed3 = new UnhideWhenUsed();

			style5.Append(styleName5);
			style5.Append(uIPriority4);
			style5.Append(semiHidden3);
			style5.Append(unhideWhenUsed3);

			Style style6 = new Style(){ Type = StyleValues.Paragraph, StyleId = "Header" };
			StyleName styleName6 = new StyleName(){ Val = "header" };
			BasedOn basedOn2 = new BasedOn(){ Val = "Normal" };
			LinkedStyle linkedStyle2 = new LinkedStyle(){ Val = "HeaderChar" };
			UIPriority uIPriority5 = new UIPriority(){ Val = 99 };
			UnhideWhenUsed unhideWhenUsed4 = new UnhideWhenUsed();
			Rsid rsid9 = new Rsid(){ Val = "00F71168" };

			StyleParagraphProperties styleParagraphProperties3 = new StyleParagraphProperties();

			Tabs tabs1 = new Tabs();
			TabStop tabStop1 = new TabStop(){ Val = TabStopValues.Center, Position = 4536 };
			TabStop tabStop2 = new TabStop(){ Val = TabStopValues.Right, Position = 9072 };

			tabs1.Append(tabStop1);
			tabs1.Append(tabStop2);
			SpacingBetweenLines spacingBetweenLines3 = new SpacingBetweenLines(){ After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

			styleParagraphProperties3.Append(tabs1);
			styleParagraphProperties3.Append(spacingBetweenLines3);

			style6.Append(styleName6);
			style6.Append(basedOn2);
			style6.Append(linkedStyle2);
			style6.Append(uIPriority5);
			style6.Append(unhideWhenUsed4);
			style6.Append(rsid9);
			style6.Append(styleParagraphProperties3);

			Style style7 = new Style(){ Type = StyleValues.Character, StyleId = "HeaderChar", CustomStyle = true };
			StyleName styleName7 = new StyleName(){ Val = "Header Char" };
			BasedOn basedOn3 = new BasedOn(){ Val = "DefaultParagraphFont" };
			LinkedStyle linkedStyle3 = new LinkedStyle(){ Val = "Header" };
			UIPriority uIPriority6 = new UIPriority(){ Val = 99 };
			Rsid rsid10 = new Rsid(){ Val = "00F71168" };

			style7.Append(styleName7);
			style7.Append(basedOn3);
			style7.Append(linkedStyle3);
			style7.Append(uIPriority6);
			style7.Append(rsid10);

			Style style8 = new Style(){ Type = StyleValues.Paragraph, StyleId = "Footer" };
			StyleName styleName8 = new StyleName(){ Val = "footer" };
			BasedOn basedOn4 = new BasedOn(){ Val = "Normal" };
			LinkedStyle linkedStyle4 = new LinkedStyle(){ Val = "FooterChar" };
			UIPriority uIPriority7 = new UIPriority(){ Val = 99 };
			UnhideWhenUsed unhideWhenUsed5 = new UnhideWhenUsed();
			Rsid rsid11 = new Rsid(){ Val = "00F71168" };

			StyleParagraphProperties styleParagraphProperties4 = new StyleParagraphProperties();

			Tabs tabs2 = new Tabs();
			TabStop tabStop3 = new TabStop(){ Val = TabStopValues.Center, Position = 4536 };
			TabStop tabStop4 = new TabStop(){ Val = TabStopValues.Right, Position = 9072 };

			tabs2.Append(tabStop3);
			tabs2.Append(tabStop4);
			SpacingBetweenLines spacingBetweenLines4 = new SpacingBetweenLines(){ After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

			styleParagraphProperties4.Append(tabs2);
			styleParagraphProperties4.Append(spacingBetweenLines4);

			style8.Append(styleName8);
			style8.Append(basedOn4);
			style8.Append(linkedStyle4);
			style8.Append(uIPriority7);
			style8.Append(unhideWhenUsed5);
			style8.Append(rsid11);
			style8.Append(styleParagraphProperties4);

			Style style9 = new Style(){ Type = StyleValues.Character, StyleId = "FooterChar", CustomStyle = true };
			StyleName styleName9 = new StyleName(){ Val = "Footer Char" };
			BasedOn basedOn5 = new BasedOn(){ Val = "DefaultParagraphFont" };
			LinkedStyle linkedStyle5 = new LinkedStyle(){ Val = "Footer" };
			UIPriority uIPriority8 = new UIPriority(){ Val = 99 };
			Rsid rsid12 = new Rsid(){ Val = "00F71168" };

			style9.Append(styleName9);
			style9.Append(basedOn5);
			style9.Append(linkedStyle5);
			style9.Append(uIPriority8);
			style9.Append(rsid12);

			Style style10 = new Style(){ Type = StyleValues.Paragraph, StyleId = "BalloonText" };
			StyleName styleName10 = new StyleName(){ Val = "Balloon Text" };
			BasedOn basedOn6 = new BasedOn(){ Val = "Normal" };
			LinkedStyle linkedStyle6 = new LinkedStyle(){ Val = "BalloonTextChar" };
			UIPriority uIPriority9 = new UIPriority(){ Val = 99 };
			SemiHidden semiHidden4 = new SemiHidden();
			UnhideWhenUsed unhideWhenUsed6 = new UnhideWhenUsed();
			Rsid rsid13 = new Rsid(){ Val = "00F71168" };

			StyleParagraphProperties styleParagraphProperties5 = new StyleParagraphProperties();
			SpacingBetweenLines spacingBetweenLines5 = new SpacingBetweenLines(){ After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

			styleParagraphProperties5.Append(spacingBetweenLines5);

			StyleRunProperties styleRunProperties3 = new StyleRunProperties();
			RunFonts runFonts3 = new RunFonts(){ Ascii = "Tahoma", HighAnsi = "Tahoma", ComplexScript = "Tahoma" };
			FontSize fontSize3 = new FontSize(){ Val = "16" };
			FontSizeComplexScript fontSizeComplexScript3 = new FontSizeComplexScript(){ Val = "16" };

			styleRunProperties3.Append(runFonts3);
			styleRunProperties3.Append(fontSize3);
			styleRunProperties3.Append(fontSizeComplexScript3);

			style10.Append(styleName10);
			style10.Append(basedOn6);
			style10.Append(linkedStyle6);
			style10.Append(uIPriority9);
			style10.Append(semiHidden4);
			style10.Append(unhideWhenUsed6);
			style10.Append(rsid13);
			style10.Append(styleParagraphProperties5);
			style10.Append(styleRunProperties3);

			Style style11 = new Style(){ Type = StyleValues.Character, StyleId = "BalloonTextChar", CustomStyle = true };
			StyleName styleName11 = new StyleName(){ Val = "Balloon Text Char" };
			LinkedStyle linkedStyle7 = new LinkedStyle(){ Val = "BalloonText" };
			UIPriority uIPriority10 = new UIPriority(){ Val = 99 };
			SemiHidden semiHidden5 = new SemiHidden();
			Rsid rsid14 = new Rsid(){ Val = "00F71168" };

			StyleRunProperties styleRunProperties4 = new StyleRunProperties();
			RunFonts runFonts4 = new RunFonts(){ Ascii = "Tahoma", HighAnsi = "Tahoma", ComplexScript = "Tahoma" };
			FontSize fontSize4 = new FontSize(){ Val = "16" };
			FontSizeComplexScript fontSizeComplexScript4 = new FontSizeComplexScript(){ Val = "16" };

			styleRunProperties4.Append(runFonts4);
			styleRunProperties4.Append(fontSize4);
			styleRunProperties4.Append(fontSizeComplexScript4);

			style11.Append(styleName11);
			style11.Append(linkedStyle7);
			style11.Append(uIPriority10);
			style11.Append(semiHidden5);
			style11.Append(rsid14);
			style11.Append(styleRunProperties4);

			Style style12 = new Style(){ Type = StyleValues.Character, StyleId = "Heading1Char", CustomStyle = true };
			StyleName styleName12 = new StyleName(){ Val = "Heading 1 Char" };
			BasedOn basedOn7 = new BasedOn(){ Val = "DefaultParagraphFont" };
			LinkedStyle linkedStyle8 = new LinkedStyle(){ Val = "Heading1" };
			UIPriority uIPriority11 = new UIPriority(){ Val = 9 };
			Rsid rsid15 = new Rsid(){ Val = "00E424F4" };

			StyleRunProperties styleRunProperties5 = new StyleRunProperties();
			RunFonts runFonts5 = new RunFonts(){ AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
			Bold bold2 = new Bold();
			BoldComplexScript boldComplexScript2 = new BoldComplexScript();
			Kern kern2 = new Kern(){ Val = (UInt32Value)32U };
			FontSize fontSize5 = new FontSize(){ Val = "32" };
			FontSizeComplexScript fontSizeComplexScript5 = new FontSizeComplexScript(){ Val = "32" };
			Languages languages6 = new Languages(){ Val = "sv-SE" };

			styleRunProperties5.Append(runFonts5);
			styleRunProperties5.Append(bold2);
			styleRunProperties5.Append(boldComplexScript2);
			styleRunProperties5.Append(kern2);
			styleRunProperties5.Append(fontSize5);
			styleRunProperties5.Append(fontSizeComplexScript5);
			styleRunProperties5.Append(languages6);

			style12.Append(styleName12);
			style12.Append(basedOn7);
			style12.Append(linkedStyle8);
			style12.Append(uIPriority11);
			style12.Append(rsid15);
			style12.Append(styleRunProperties5);

			styles1.Append(docDefaults1);
			styles1.Append(latentStyles1);
			styles1.Append(style1);
			styles1.Append(style2);
			styles1.Append(style3);
			styles1.Append(style4);
			styles1.Append(style5);
			styles1.Append(style6);
			styles1.Append(style7);
			styles1.Append(style8);
			styles1.Append(style9);
			styles1.Append(style10);
			styles1.Append(style11);
			styles1.Append(style12);

			styleDefinitionsPart1.Styles = styles1;
		}

		// Generates content of imagePart3.
		private void GenerateImagePart3Content(ImagePart imagePart3, string url)
		{
			//            System.IO.Stream data = GetBinaryDataStream(imagePart3Data);
			//            imagePart3.FeedData(data);
			//            data.Close();
			WebRequest req = WebRequest.Create(url);
			WebResponse response = req.GetResponse();
			Stream stream = response.GetResponseStream();
			imagePart3.FeedData(stream);
		}

		// Generates content of endnotesPart1.
		private void GenerateEndnotesPart1Content(EndnotesPart endnotesPart1)
		{
			Endnotes endnotes1 = new Endnotes();
			endnotes1.AddNamespaceDeclaration("ve", "http://schemas.openxmlformats.org/markup-compatibility/2006");
			endnotes1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
			endnotes1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			endnotes1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
			endnotes1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
			endnotes1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
			endnotes1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
			endnotes1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
			endnotes1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");

			Endnote endnote1 = new Endnote(){ Type = FootnoteEndnoteValues.Separator, Id = 0 };

			Paragraph paragraph10 = new Paragraph(){ RsidParagraphAddition = "00AB3B9F", RsidParagraphProperties = "00F71168", RsidRunAdditionDefault = "00AB3B9F" };

			ParagraphProperties paragraphProperties6 = new ParagraphProperties();
			SpacingBetweenLines spacingBetweenLines6 = new SpacingBetweenLines(){ After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

			paragraphProperties6.Append(spacingBetweenLines6);

			Run run9 = new Run();
			SeparatorMark separatorMark1 = new SeparatorMark();

			run9.Append(separatorMark1);

			paragraph10.Append(paragraphProperties6);
			paragraph10.Append(run9);

			endnote1.Append(paragraph10);

			Endnote endnote2 = new Endnote(){ Type = FootnoteEndnoteValues.ContinuationSeparator, Id = 1 };

			Paragraph paragraph11 = new Paragraph(){ RsidParagraphAddition = "00AB3B9F", RsidParagraphProperties = "00F71168", RsidRunAdditionDefault = "00AB3B9F" };

			ParagraphProperties paragraphProperties7 = new ParagraphProperties();
			SpacingBetweenLines spacingBetweenLines7 = new SpacingBetweenLines(){ After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

			paragraphProperties7.Append(spacingBetweenLines7);

			Run run10 = new Run();
			ContinuationSeparatorMark continuationSeparatorMark1 = new ContinuationSeparatorMark();

			run10.Append(continuationSeparatorMark1);

			paragraph11.Append(paragraphProperties7);
			paragraph11.Append(run10);

			endnote2.Append(paragraph11);

			endnotes1.Append(endnote1);
			endnotes1.Append(endnote2);

			endnotesPart1.Endnotes = endnotes1;
		}

		// Generates content of footnotesPart1.
		private void GenerateFootnotesPart1Content(FootnotesPart footnotesPart1)
		{
			Footnotes footnotes1 = new Footnotes();
			footnotes1.AddNamespaceDeclaration("ve", "http://schemas.openxmlformats.org/markup-compatibility/2006");
			footnotes1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
			footnotes1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			footnotes1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
			footnotes1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
			footnotes1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
			footnotes1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
			footnotes1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
			footnotes1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");

			Footnote footnote1 = new Footnote(){ Type = FootnoteEndnoteValues.Separator, Id = 0 };

			Paragraph paragraph12 = new Paragraph(){ RsidParagraphAddition = "00AB3B9F", RsidParagraphProperties = "00F71168", RsidRunAdditionDefault = "00AB3B9F" };

			ParagraphProperties paragraphProperties8 = new ParagraphProperties();
			SpacingBetweenLines spacingBetweenLines8 = new SpacingBetweenLines(){ After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

			paragraphProperties8.Append(spacingBetweenLines8);

			Run run11 = new Run();
			SeparatorMark separatorMark2 = new SeparatorMark();

			run11.Append(separatorMark2);

			paragraph12.Append(paragraphProperties8);
			paragraph12.Append(run11);

			footnote1.Append(paragraph12);

			Footnote footnote2 = new Footnote(){ Type = FootnoteEndnoteValues.ContinuationSeparator, Id = 1 };

			Paragraph paragraph13 = new Paragraph(){ RsidParagraphAddition = "00AB3B9F", RsidParagraphProperties = "00F71168", RsidRunAdditionDefault = "00AB3B9F" };

			ParagraphProperties paragraphProperties9 = new ParagraphProperties();
			SpacingBetweenLines spacingBetweenLines9 = new SpacingBetweenLines(){ After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

			paragraphProperties9.Append(spacingBetweenLines9);

			Run run12 = new Run();
			ContinuationSeparatorMark continuationSeparatorMark2 = new ContinuationSeparatorMark();

			run12.Append(continuationSeparatorMark2);

			paragraph13.Append(paragraphProperties9);
			paragraph13.Append(run12);

			footnote2.Append(paragraph13);

			footnotes1.Append(footnote1);
			footnotes1.Append(footnote2);

			footnotesPart1.Footnotes = footnotes1;
		}

		// Generates content of themePart1.
		private void GenerateThemePart1Content(ThemePart themePart1)
		{
			A.Theme theme1 = new A.Theme(){ Name = "Office Theme" };
			theme1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

			A.ThemeElements themeElements1 = new A.ThemeElements();

			A.ColorScheme colorScheme1 = new A.ColorScheme(){ Name = "Office" };

			A.Dark1Color dark1Color1 = new A.Dark1Color();
			A.SystemColor systemColor1 = new A.SystemColor(){ Val = A.SystemColorValues.WindowText, LastColor = "000000" };

			dark1Color1.Append(systemColor1);

			A.Light1Color light1Color1 = new A.Light1Color();
			A.SystemColor systemColor2 = new A.SystemColor(){ Val = A.SystemColorValues.Window, LastColor = "FFFFFF" };

			light1Color1.Append(systemColor2);

			A.Dark2Color dark2Color1 = new A.Dark2Color();
			A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex(){ Val = "1F497D" };

			dark2Color1.Append(rgbColorModelHex1);

			A.Light2Color light2Color1 = new A.Light2Color();
			A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex(){ Val = "EEECE1" };

			light2Color1.Append(rgbColorModelHex2);

			A.Accent1Color accent1Color1 = new A.Accent1Color();
			A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex(){ Val = "4F81BD" };

			accent1Color1.Append(rgbColorModelHex3);

			A.Accent2Color accent2Color1 = new A.Accent2Color();
			A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex(){ Val = "C0504D" };

			accent2Color1.Append(rgbColorModelHex4);

			A.Accent3Color accent3Color1 = new A.Accent3Color();
			A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex(){ Val = "9BBB59" };

			accent3Color1.Append(rgbColorModelHex5);

			A.Accent4Color accent4Color1 = new A.Accent4Color();
			A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex(){ Val = "8064A2" };

			accent4Color1.Append(rgbColorModelHex6);

			A.Accent5Color accent5Color1 = new A.Accent5Color();
			A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex(){ Val = "4BACC6" };

			accent5Color1.Append(rgbColorModelHex7);

			A.Accent6Color accent6Color1 = new A.Accent6Color();
			A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex(){ Val = "F79646" };

			accent6Color1.Append(rgbColorModelHex8);

			A.Hyperlink hyperlink1 = new A.Hyperlink();
			A.RgbColorModelHex rgbColorModelHex9 = new A.RgbColorModelHex(){ Val = "0000FF" };

			hyperlink1.Append(rgbColorModelHex9);

			A.FollowedHyperlinkColor followedHyperlinkColor1 = new A.FollowedHyperlinkColor();
			A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex(){ Val = "800080" };

			followedHyperlinkColor1.Append(rgbColorModelHex10);

			colorScheme1.Append(dark1Color1);
			colorScheme1.Append(light1Color1);
			colorScheme1.Append(dark2Color1);
			colorScheme1.Append(light2Color1);
			colorScheme1.Append(accent1Color1);
			colorScheme1.Append(accent2Color1);
			colorScheme1.Append(accent3Color1);
			colorScheme1.Append(accent4Color1);
			colorScheme1.Append(accent5Color1);
			colorScheme1.Append(accent6Color1);
			colorScheme1.Append(hyperlink1);
			colorScheme1.Append(followedHyperlinkColor1);

			A.FontScheme fontScheme1 = new A.FontScheme(){ Name = "Office" };

			A.MajorFont majorFont1 = new A.MajorFont();
			A.LatinFont latinFont1 = new A.LatinFont(){ Typeface = "Cambria" };
			A.EastAsianFont eastAsianFont1 = new A.EastAsianFont(){ Typeface = "" };
			A.ComplexScriptFont complexScriptFont1 = new A.ComplexScriptFont(){ Typeface = "" };
			A.SupplementalFont supplementalFont1 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "ＭＳ ゴシック" };
			A.SupplementalFont supplementalFont2 = new A.SupplementalFont(){ Script = "Hang", Typeface = "맑은 고딕" };
			A.SupplementalFont supplementalFont3 = new A.SupplementalFont(){ Script = "Hans", Typeface = "宋体" };
			A.SupplementalFont supplementalFont4 = new A.SupplementalFont(){ Script = "Hant", Typeface = "新細明體" };
			A.SupplementalFont supplementalFont5 = new A.SupplementalFont(){ Script = "Arab", Typeface = "Times New Roman" };
			A.SupplementalFont supplementalFont6 = new A.SupplementalFont(){ Script = "Hebr", Typeface = "Times New Roman" };
			A.SupplementalFont supplementalFont7 = new A.SupplementalFont(){ Script = "Thai", Typeface = "Angsana New" };
			A.SupplementalFont supplementalFont8 = new A.SupplementalFont(){ Script = "Ethi", Typeface = "Nyala" };
			A.SupplementalFont supplementalFont9 = new A.SupplementalFont(){ Script = "Beng", Typeface = "Vrinda" };
			A.SupplementalFont supplementalFont10 = new A.SupplementalFont(){ Script = "Gujr", Typeface = "Shruti" };
			A.SupplementalFont supplementalFont11 = new A.SupplementalFont(){ Script = "Khmr", Typeface = "MoolBoran" };
			A.SupplementalFont supplementalFont12 = new A.SupplementalFont(){ Script = "Knda", Typeface = "Tunga" };
			A.SupplementalFont supplementalFont13 = new A.SupplementalFont(){ Script = "Guru", Typeface = "Raavi" };
			A.SupplementalFont supplementalFont14 = new A.SupplementalFont(){ Script = "Cans", Typeface = "Euphemia" };
			A.SupplementalFont supplementalFont15 = new A.SupplementalFont(){ Script = "Cher", Typeface = "Plantagenet Cherokee" };
			A.SupplementalFont supplementalFont16 = new A.SupplementalFont(){ Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
			A.SupplementalFont supplementalFont17 = new A.SupplementalFont(){ Script = "Tibt", Typeface = "Microsoft Himalaya" };
			A.SupplementalFont supplementalFont18 = new A.SupplementalFont(){ Script = "Thaa", Typeface = "MV Boli" };
			A.SupplementalFont supplementalFont19 = new A.SupplementalFont(){ Script = "Deva", Typeface = "Mangal" };
			A.SupplementalFont supplementalFont20 = new A.SupplementalFont(){ Script = "Telu", Typeface = "Gautami" };
			A.SupplementalFont supplementalFont21 = new A.SupplementalFont(){ Script = "Taml", Typeface = "Latha" };
			A.SupplementalFont supplementalFont22 = new A.SupplementalFont(){ Script = "Syrc", Typeface = "Estrangelo Edessa" };
			A.SupplementalFont supplementalFont23 = new A.SupplementalFont(){ Script = "Orya", Typeface = "Kalinga" };
			A.SupplementalFont supplementalFont24 = new A.SupplementalFont(){ Script = "Mlym", Typeface = "Kartika" };
			A.SupplementalFont supplementalFont25 = new A.SupplementalFont(){ Script = "Laoo", Typeface = "DokChampa" };
			A.SupplementalFont supplementalFont26 = new A.SupplementalFont(){ Script = "Sinh", Typeface = "Iskoola Pota" };
			A.SupplementalFont supplementalFont27 = new A.SupplementalFont(){ Script = "Mong", Typeface = "Mongolian Baiti" };
			A.SupplementalFont supplementalFont28 = new A.SupplementalFont(){ Script = "Viet", Typeface = "Times New Roman" };
			A.SupplementalFont supplementalFont29 = new A.SupplementalFont(){ Script = "Uigh", Typeface = "Microsoft Uighur" };

			majorFont1.Append(latinFont1);
			majorFont1.Append(eastAsianFont1);
			majorFont1.Append(complexScriptFont1);
			majorFont1.Append(supplementalFont1);
			majorFont1.Append(supplementalFont2);
			majorFont1.Append(supplementalFont3);
			majorFont1.Append(supplementalFont4);
			majorFont1.Append(supplementalFont5);
			majorFont1.Append(supplementalFont6);
			majorFont1.Append(supplementalFont7);
			majorFont1.Append(supplementalFont8);
			majorFont1.Append(supplementalFont9);
			majorFont1.Append(supplementalFont10);
			majorFont1.Append(supplementalFont11);
			majorFont1.Append(supplementalFont12);
			majorFont1.Append(supplementalFont13);
			majorFont1.Append(supplementalFont14);
			majorFont1.Append(supplementalFont15);
			majorFont1.Append(supplementalFont16);
			majorFont1.Append(supplementalFont17);
			majorFont1.Append(supplementalFont18);
			majorFont1.Append(supplementalFont19);
			majorFont1.Append(supplementalFont20);
			majorFont1.Append(supplementalFont21);
			majorFont1.Append(supplementalFont22);
			majorFont1.Append(supplementalFont23);
			majorFont1.Append(supplementalFont24);
			majorFont1.Append(supplementalFont25);
			majorFont1.Append(supplementalFont26);
			majorFont1.Append(supplementalFont27);
			majorFont1.Append(supplementalFont28);
			majorFont1.Append(supplementalFont29);

			A.MinorFont minorFont1 = new A.MinorFont();
			A.LatinFont latinFont2 = new A.LatinFont(){ Typeface = "Calibri" };
			A.EastAsianFont eastAsianFont2 = new A.EastAsianFont(){ Typeface = "" };
			A.ComplexScriptFont complexScriptFont2 = new A.ComplexScriptFont(){ Typeface = "" };
			A.SupplementalFont supplementalFont30 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "ＭＳ 明朝" };
			A.SupplementalFont supplementalFont31 = new A.SupplementalFont(){ Script = "Hang", Typeface = "맑은 고딕" };
			A.SupplementalFont supplementalFont32 = new A.SupplementalFont(){ Script = "Hans", Typeface = "宋体" };
			A.SupplementalFont supplementalFont33 = new A.SupplementalFont(){ Script = "Hant", Typeface = "新細明體" };
			A.SupplementalFont supplementalFont34 = new A.SupplementalFont(){ Script = "Arab", Typeface = "Arial" };
			A.SupplementalFont supplementalFont35 = new A.SupplementalFont(){ Script = "Hebr", Typeface = "Arial" };
			A.SupplementalFont supplementalFont36 = new A.SupplementalFont(){ Script = "Thai", Typeface = "Cordia New" };
			A.SupplementalFont supplementalFont37 = new A.SupplementalFont(){ Script = "Ethi", Typeface = "Nyala" };
			A.SupplementalFont supplementalFont38 = new A.SupplementalFont(){ Script = "Beng", Typeface = "Vrinda" };
			A.SupplementalFont supplementalFont39 = new A.SupplementalFont(){ Script = "Gujr", Typeface = "Shruti" };
			A.SupplementalFont supplementalFont40 = new A.SupplementalFont(){ Script = "Khmr", Typeface = "DaunPenh" };
			A.SupplementalFont supplementalFont41 = new A.SupplementalFont(){ Script = "Knda", Typeface = "Tunga" };
			A.SupplementalFont supplementalFont42 = new A.SupplementalFont(){ Script = "Guru", Typeface = "Raavi" };
			A.SupplementalFont supplementalFont43 = new A.SupplementalFont(){ Script = "Cans", Typeface = "Euphemia" };
			A.SupplementalFont supplementalFont44 = new A.SupplementalFont(){ Script = "Cher", Typeface = "Plantagenet Cherokee" };
			A.SupplementalFont supplementalFont45 = new A.SupplementalFont(){ Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
			A.SupplementalFont supplementalFont46 = new A.SupplementalFont(){ Script = "Tibt", Typeface = "Microsoft Himalaya" };
			A.SupplementalFont supplementalFont47 = new A.SupplementalFont(){ Script = "Thaa", Typeface = "MV Boli" };
			A.SupplementalFont supplementalFont48 = new A.SupplementalFont(){ Script = "Deva", Typeface = "Mangal" };
			A.SupplementalFont supplementalFont49 = new A.SupplementalFont(){ Script = "Telu", Typeface = "Gautami" };
			A.SupplementalFont supplementalFont50 = new A.SupplementalFont(){ Script = "Taml", Typeface = "Latha" };
			A.SupplementalFont supplementalFont51 = new A.SupplementalFont(){ Script = "Syrc", Typeface = "Estrangelo Edessa" };
			A.SupplementalFont supplementalFont52 = new A.SupplementalFont(){ Script = "Orya", Typeface = "Kalinga" };
			A.SupplementalFont supplementalFont53 = new A.SupplementalFont(){ Script = "Mlym", Typeface = "Kartika" };
			A.SupplementalFont supplementalFont54 = new A.SupplementalFont(){ Script = "Laoo", Typeface = "DokChampa" };
			A.SupplementalFont supplementalFont55 = new A.SupplementalFont(){ Script = "Sinh", Typeface = "Iskoola Pota" };
			A.SupplementalFont supplementalFont56 = new A.SupplementalFont(){ Script = "Mong", Typeface = "Mongolian Baiti" };
			A.SupplementalFont supplementalFont57 = new A.SupplementalFont(){ Script = "Viet", Typeface = "Arial" };
			A.SupplementalFont supplementalFont58 = new A.SupplementalFont(){ Script = "Uigh", Typeface = "Microsoft Uighur" };

			minorFont1.Append(latinFont2);
			minorFont1.Append(eastAsianFont2);
			minorFont1.Append(complexScriptFont2);
			minorFont1.Append(supplementalFont30);
			minorFont1.Append(supplementalFont31);
			minorFont1.Append(supplementalFont32);
			minorFont1.Append(supplementalFont33);
			minorFont1.Append(supplementalFont34);
			minorFont1.Append(supplementalFont35);
			minorFont1.Append(supplementalFont36);
			minorFont1.Append(supplementalFont37);
			minorFont1.Append(supplementalFont38);
			minorFont1.Append(supplementalFont39);
			minorFont1.Append(supplementalFont40);
			minorFont1.Append(supplementalFont41);
			minorFont1.Append(supplementalFont42);
			minorFont1.Append(supplementalFont43);
			minorFont1.Append(supplementalFont44);
			minorFont1.Append(supplementalFont45);
			minorFont1.Append(supplementalFont46);
			minorFont1.Append(supplementalFont47);
			minorFont1.Append(supplementalFont48);
			minorFont1.Append(supplementalFont49);
			minorFont1.Append(supplementalFont50);
			minorFont1.Append(supplementalFont51);
			minorFont1.Append(supplementalFont52);
			minorFont1.Append(supplementalFont53);
			minorFont1.Append(supplementalFont54);
			minorFont1.Append(supplementalFont55);
			minorFont1.Append(supplementalFont56);
			minorFont1.Append(supplementalFont57);
			minorFont1.Append(supplementalFont58);

			fontScheme1.Append(majorFont1);
			fontScheme1.Append(minorFont1);

			A.FormatScheme formatScheme1 = new A.FormatScheme(){ Name = "Office" };

			A.FillStyleList fillStyleList1 = new A.FillStyleList();

			A.SolidFill solidFill1 = new A.SolidFill();
			A.SchemeColor schemeColor1 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

			solidFill1.Append(schemeColor1);

			A.GradientFill gradientFill1 = new A.GradientFill(){ RotateWithShape = true };

			A.GradientStopList gradientStopList1 = new A.GradientStopList();

			A.GradientStop gradientStop1 = new A.GradientStop(){ Position = 0 };

			A.SchemeColor schemeColor2 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Tint tint1 = new A.Tint(){ Val = 50000 };
			A.SaturationModulation saturationModulation1 = new A.SaturationModulation(){ Val = 300000 };

			schemeColor2.Append(tint1);
			schemeColor2.Append(saturationModulation1);

			gradientStop1.Append(schemeColor2);

			A.GradientStop gradientStop2 = new A.GradientStop(){ Position = 35000 };

			A.SchemeColor schemeColor3 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Tint tint2 = new A.Tint(){ Val = 37000 };
			A.SaturationModulation saturationModulation2 = new A.SaturationModulation(){ Val = 300000 };

			schemeColor3.Append(tint2);
			schemeColor3.Append(saturationModulation2);

			gradientStop2.Append(schemeColor3);

			A.GradientStop gradientStop3 = new A.GradientStop(){ Position = 100000 };

			A.SchemeColor schemeColor4 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Tint tint3 = new A.Tint(){ Val = 15000 };
			A.SaturationModulation saturationModulation3 = new A.SaturationModulation(){ Val = 350000 };

			schemeColor4.Append(tint3);
			schemeColor4.Append(saturationModulation3);

			gradientStop3.Append(schemeColor4);

			gradientStopList1.Append(gradientStop1);
			gradientStopList1.Append(gradientStop2);
			gradientStopList1.Append(gradientStop3);
			A.LinearGradientFill linearGradientFill1 = new A.LinearGradientFill(){ Angle = 16200000, Scaled = true };

			gradientFill1.Append(gradientStopList1);
			gradientFill1.Append(linearGradientFill1);

			A.GradientFill gradientFill2 = new A.GradientFill(){ RotateWithShape = true };

			A.GradientStopList gradientStopList2 = new A.GradientStopList();

			A.GradientStop gradientStop4 = new A.GradientStop(){ Position = 0 };

			A.SchemeColor schemeColor5 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade1 = new A.Shade(){ Val = 51000 };
			A.SaturationModulation saturationModulation4 = new A.SaturationModulation(){ Val = 130000 };

			schemeColor5.Append(shade1);
			schemeColor5.Append(saturationModulation4);

			gradientStop4.Append(schemeColor5);

			A.GradientStop gradientStop5 = new A.GradientStop(){ Position = 80000 };

			A.SchemeColor schemeColor6 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade2 = new A.Shade(){ Val = 93000 };
			A.SaturationModulation saturationModulation5 = new A.SaturationModulation(){ Val = 130000 };

			schemeColor6.Append(shade2);
			schemeColor6.Append(saturationModulation5);

			gradientStop5.Append(schemeColor6);

			A.GradientStop gradientStop6 = new A.GradientStop(){ Position = 100000 };

			A.SchemeColor schemeColor7 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade3 = new A.Shade(){ Val = 94000 };
			A.SaturationModulation saturationModulation6 = new A.SaturationModulation(){ Val = 135000 };

			schemeColor7.Append(shade3);
			schemeColor7.Append(saturationModulation6);

			gradientStop6.Append(schemeColor7);

			gradientStopList2.Append(gradientStop4);
			gradientStopList2.Append(gradientStop5);
			gradientStopList2.Append(gradientStop6);
			A.LinearGradientFill linearGradientFill2 = new A.LinearGradientFill(){ Angle = 16200000, Scaled = false };

			gradientFill2.Append(gradientStopList2);
			gradientFill2.Append(linearGradientFill2);

			fillStyleList1.Append(solidFill1);
			fillStyleList1.Append(gradientFill1);
			fillStyleList1.Append(gradientFill2);

			A.LineStyleList lineStyleList1 = new A.LineStyleList();

			A.Outline outline4 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

			A.SolidFill solidFill2 = new A.SolidFill();

			A.SchemeColor schemeColor8 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade4 = new A.Shade(){ Val = 95000 };
			A.SaturationModulation saturationModulation7 = new A.SaturationModulation(){ Val = 105000 };

			schemeColor8.Append(shade4);
			schemeColor8.Append(saturationModulation7);

			solidFill2.Append(schemeColor8);
			A.PresetDash presetDash1 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

			outline4.Append(solidFill2);
			outline4.Append(presetDash1);

			A.Outline outline5 = new A.Outline(){ Width = 25400, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

			A.SolidFill solidFill3 = new A.SolidFill();
			A.SchemeColor schemeColor9 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

			solidFill3.Append(schemeColor9);
			A.PresetDash presetDash2 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

			outline5.Append(solidFill3);
			outline5.Append(presetDash2);

			A.Outline outline6 = new A.Outline(){ Width = 38100, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

			A.SolidFill solidFill4 = new A.SolidFill();
			A.SchemeColor schemeColor10 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

			solidFill4.Append(schemeColor10);
			A.PresetDash presetDash3 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

			outline6.Append(solidFill4);
			outline6.Append(presetDash3);

			lineStyleList1.Append(outline4);
			lineStyleList1.Append(outline5);
			lineStyleList1.Append(outline6);

			A.EffectStyleList effectStyleList1 = new A.EffectStyleList();

			A.EffectStyle effectStyle1 = new A.EffectStyle();

			A.EffectList effectList1 = new A.EffectList();

			A.OuterShadow outerShadow1 = new A.OuterShadow(){ BlurRadius = 40000L, Distance = 20000L, Direction = 5400000, RotateWithShape = false };

			A.RgbColorModelHex rgbColorModelHex11 = new A.RgbColorModelHex(){ Val = "000000" };
			A.Alpha alpha1 = new A.Alpha(){ Val = 38000 };

			rgbColorModelHex11.Append(alpha1);

			outerShadow1.Append(rgbColorModelHex11);

			effectList1.Append(outerShadow1);

			effectStyle1.Append(effectList1);

			A.EffectStyle effectStyle2 = new A.EffectStyle();

			A.EffectList effectList2 = new A.EffectList();

			A.OuterShadow outerShadow2 = new A.OuterShadow(){ BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

			A.RgbColorModelHex rgbColorModelHex12 = new A.RgbColorModelHex(){ Val = "000000" };
			A.Alpha alpha2 = new A.Alpha(){ Val = 35000 };

			rgbColorModelHex12.Append(alpha2);

			outerShadow2.Append(rgbColorModelHex12);

			effectList2.Append(outerShadow2);

			effectStyle2.Append(effectList2);

			A.EffectStyle effectStyle3 = new A.EffectStyle();

			A.EffectList effectList3 = new A.EffectList();

			A.OuterShadow outerShadow3 = new A.OuterShadow(){ BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

			A.RgbColorModelHex rgbColorModelHex13 = new A.RgbColorModelHex(){ Val = "000000" };
			A.Alpha alpha3 = new A.Alpha(){ Val = 35000 };

			rgbColorModelHex13.Append(alpha3);

			outerShadow3.Append(rgbColorModelHex13);

			effectList3.Append(outerShadow3);

			A.Scene3DType scene3DType1 = new A.Scene3DType();

			A.Camera camera1 = new A.Camera(){ Preset = A.PresetCameraValues.OrthographicFront };
			A.Rotation rotation1 = new A.Rotation(){ Latitude = 0, Longitude = 0, Revolution = 0 };

			camera1.Append(rotation1);

			A.LightRig lightRig1 = new A.LightRig(){ Rig = A.LightRigValues.ThreePoints, Direction = A.LightRigDirectionValues.Top };
			A.Rotation rotation2 = new A.Rotation(){ Latitude = 0, Longitude = 0, Revolution = 1200000 };

			lightRig1.Append(rotation2);

			scene3DType1.Append(camera1);
			scene3DType1.Append(lightRig1);

			A.Shape3DType shape3DType1 = new A.Shape3DType();
			A.BevelTop bevelTop1 = new A.BevelTop(){ Width = 63500L, Height = 25400L };

			shape3DType1.Append(bevelTop1);

			effectStyle3.Append(effectList3);
			effectStyle3.Append(scene3DType1);
			effectStyle3.Append(shape3DType1);

			effectStyleList1.Append(effectStyle1);
			effectStyleList1.Append(effectStyle2);
			effectStyleList1.Append(effectStyle3);

			A.BackgroundFillStyleList backgroundFillStyleList1 = new A.BackgroundFillStyleList();

			A.SolidFill solidFill5 = new A.SolidFill();
			A.SchemeColor schemeColor11 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

			solidFill5.Append(schemeColor11);

			A.GradientFill gradientFill3 = new A.GradientFill(){ RotateWithShape = true };

			A.GradientStopList gradientStopList3 = new A.GradientStopList();

			A.GradientStop gradientStop7 = new A.GradientStop(){ Position = 0 };

			A.SchemeColor schemeColor12 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Tint tint4 = new A.Tint(){ Val = 40000 };
			A.SaturationModulation saturationModulation8 = new A.SaturationModulation(){ Val = 350000 };

			schemeColor12.Append(tint4);
			schemeColor12.Append(saturationModulation8);

			gradientStop7.Append(schemeColor12);

			A.GradientStop gradientStop8 = new A.GradientStop(){ Position = 40000 };

			A.SchemeColor schemeColor13 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Tint tint5 = new A.Tint(){ Val = 45000 };
			A.Shade shade5 = new A.Shade(){ Val = 99000 };
			A.SaturationModulation saturationModulation9 = new A.SaturationModulation(){ Val = 350000 };

			schemeColor13.Append(tint5);
			schemeColor13.Append(shade5);
			schemeColor13.Append(saturationModulation9);

			gradientStop8.Append(schemeColor13);

			A.GradientStop gradientStop9 = new A.GradientStop(){ Position = 100000 };

			A.SchemeColor schemeColor14 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade6 = new A.Shade(){ Val = 20000 };
			A.SaturationModulation saturationModulation10 = new A.SaturationModulation(){ Val = 255000 };

			schemeColor14.Append(shade6);
			schemeColor14.Append(saturationModulation10);

			gradientStop9.Append(schemeColor14);

			gradientStopList3.Append(gradientStop7);
			gradientStopList3.Append(gradientStop8);
			gradientStopList3.Append(gradientStop9);

			A.PathGradientFill pathGradientFill1 = new A.PathGradientFill(){ Path = A.PathShadeValues.Circle };
			A.FillToRectangle fillToRectangle1 = new A.FillToRectangle(){ Left = 50000, Top = -80000, Right = 50000, Bottom = 180000 };

			pathGradientFill1.Append(fillToRectangle1);

			gradientFill3.Append(gradientStopList3);
			gradientFill3.Append(pathGradientFill1);

			A.GradientFill gradientFill4 = new A.GradientFill(){ RotateWithShape = true };

			A.GradientStopList gradientStopList4 = new A.GradientStopList();

			A.GradientStop gradientStop10 = new A.GradientStop(){ Position = 0 };

			A.SchemeColor schemeColor15 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Tint tint6 = new A.Tint(){ Val = 80000 };
			A.SaturationModulation saturationModulation11 = new A.SaturationModulation(){ Val = 300000 };

			schemeColor15.Append(tint6);
			schemeColor15.Append(saturationModulation11);

			gradientStop10.Append(schemeColor15);

			A.GradientStop gradientStop11 = new A.GradientStop(){ Position = 100000 };

			A.SchemeColor schemeColor16 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade7 = new A.Shade(){ Val = 30000 };
			A.SaturationModulation saturationModulation12 = new A.SaturationModulation(){ Val = 200000 };

			schemeColor16.Append(shade7);
			schemeColor16.Append(saturationModulation12);

			gradientStop11.Append(schemeColor16);

			gradientStopList4.Append(gradientStop10);
			gradientStopList4.Append(gradientStop11);

			A.PathGradientFill pathGradientFill2 = new A.PathGradientFill(){ Path = A.PathShadeValues.Circle };
			A.FillToRectangle fillToRectangle2 = new A.FillToRectangle(){ Left = 50000, Top = 50000, Right = 50000, Bottom = 50000 };

			pathGradientFill2.Append(fillToRectangle2);

			gradientFill4.Append(gradientStopList4);
			gradientFill4.Append(pathGradientFill2);

			backgroundFillStyleList1.Append(solidFill5);
			backgroundFillStyleList1.Append(gradientFill3);
			backgroundFillStyleList1.Append(gradientFill4);

			formatScheme1.Append(fillStyleList1);
			formatScheme1.Append(lineStyleList1);
			formatScheme1.Append(effectStyleList1);
			formatScheme1.Append(backgroundFillStyleList1);

			themeElements1.Append(colorScheme1);
			themeElements1.Append(fontScheme1);
			themeElements1.Append(formatScheme1);
			A.ObjectDefaults objectDefaults1 = new A.ObjectDefaults();
			A.ExtraColorSchemeList extraColorSchemeList1 = new A.ExtraColorSchemeList();

			theme1.Append(themeElements1);
			theme1.Append(objectDefaults1);
			theme1.Append(extraColorSchemeList1);

			themePart1.Theme = theme1;
		}

		private void SetPackageProperties(OpenXmlPackage document)
		{
			document.PackageProperties.Creator = "Karin Villaume";
			document.PackageProperties.Revision = "2";
			document.PackageProperties.Created = System.Xml.XmlConvert.ToDateTime("2013-07-23T10:16:00Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
			document.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime("2013-07-23T10:16:00Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
			document.PackageProperties.LastModifiedBy = "Ian";
		}

		#region Binary Data
		private string imagePart1Data = "iVBORw0KGgoAAAANSUhEUgAAANwAAACWCAYAAAC1meaLAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAZdEVYdFNvZnR3YXJlAEFkb2JlIEltYWdlUmVhZHlxyWU8AAADIGlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS4wLWMwNjAgNjEuMTM0Nzc3LCAyMDEwLzAyLzEyLTE3OjMyOjAwICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdFJlZj0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlUmVmIyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ1M1IFdpbmRvd3MiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6RjMzNUEzNzdDMzZFMTFFMEJERjNFREVDREJEMjJFOTYiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6RjMzNUEzNzhDMzZFMTFFMEJERjNFREVDREJEMjJFOTYiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDpGMzM1QTM3NUMzNkUxMUUwQkRGM0VERUNEQkQyMkU5NiIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDpGMzM1QTM3NkMzNkUxMUUwQkRGM0VERUNEQkQyMkU5NiIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVu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";

		private string imagePart2Data = "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";

		private string imagePart3Data = "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IiAiaHR0cDovL3d3dy53My5vcmcvVFIveGh0bWwxL0RURC94aHRtbDEtdHJhbnNpdGlvbmFsLmR0ZCI+DQoNCjxodG1sIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hodG1sIj4NCjxoZWFkPjx0aXRsZT4NCg0KPC90aXRsZT48L2hlYWQ+DQo8Ym9keT4NCiAgICA8Zm9ybSBuYW1lPSJmb3JtMSIgbWV0aG9kPSJwb3N0IiBhY3Rpb249InJlcG9ydEltYWdlLmFzcHg/TGFuZ0lEPTEmYW1wO0ZZPTIwMTImYW1wO1RZPTIwMTMmYW1wO1NBSUQ9NTE0JmFtcDtTSUQ9ODMmYW1wO0dCPTcmYW1wO1JQSUQ9MTQmYW1wO1JQTElEPTExNSZhbXA7UFJVSUQ9MjY0MyZhbXA7R1JQTkc9MiZhbXA7R0lEPTAlMmM5MjMiIGlkPSJmb3JtMSI+DQo8aW5wdXQgdHlwZT0iaGlkZGVuIiBuYW1lPSJfX1ZJRVdTVEFURSIgaWQ9Il9fVklFV1NUQVRFIiB2YWx1ZT0iL3dFUER3VUxMVEUyTVRZMk9EY3lNamxrWk5RdXc3ajA3dXJoYkFZK3Y0YzNVam9uMVFJdmJNczJkNS9EVElVUmdQNzEiIC8+DQoNCiAgICA8ZGl2Pg0KICAgIA0KICAgIDwvZGl2Pg0KICAgIDwvZm9ybT4NCjwvYm9keT4NCjwvaHRtbD4NCg==";

		private System.IO.Stream GetBinaryDataStream(string base64String)
		{
			return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
		}

		#endregion

	}
}
