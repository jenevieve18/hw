//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Pic = DocumentFormat.OpenXml.Drawing.Pictures;
using M = DocumentFormat.OpenXml.Math;
using Ovml = DocumentFormat.OpenXml.Vml.Office;
using V = DocumentFormat.OpenXml.Vml;
using HW.Core.Models;
using HW.Core.Services;

namespace HW.Core.Helpers
{
	public class WordDocumentExporter : IExporter
	{
		ReportPart r;
		IList<ReportPartLanguage> parts;
		ReportService service;
		
		public WordDocumentExporter(ReportPart r)
		{
			this.r = r;
		}
		
		public WordDocumentExporter(ReportService service, IList<ReportPartLanguage> parts)
		{
			this.service = service;
			this.parts = parts;
		}
		
		public string Type {
			get { return "application/octet-stream"; }
		}
		
		public bool HasContentDisposition {
			get { return ContentDisposition.Length > 0; }
		}
		
		public string ContentDisposition {
			get { return "attachment;filename=Report.docx;"; }
		}
		
		public object Export(int GB, int fy, int ty, int langID, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot,	string path, int distribution)
		{
			MemoryStream output = new MemoryStream();
			using (WordprocessingDocument package = WordprocessingDocument.Create(output, WordprocessingDocumentType.Document)) {
				string url = GetUrl(path, langID, fy, ty, SPONS, SID, GB, r.Id, PRUID, GID, GRPNG, plot, distribution);
				CreateParts(package, r.CurrentLanguage, url);
			}

			return output;
		}
		
		public object Export2(int GB, int fy, int ty, int langID, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int distribution)
		{
			MemoryStream output = new MemoryStream();
			using (WordprocessingDocument package = WordprocessingDocument.Create(output, WordprocessingDocumentType.Document)) {
				foreach (var r in parts) {
					string url = GetUrl(path, langID, fy, ty, SPONS, SID, GB, r.Id, PRUID, GID, GRPNG, plot, distribution);
					CreateParts(package, r, url);
				}
			}

			return output;
		}
		
		string GetUrl(string path, int langID, int fy, int ty, int SPONS, int SID, int GB, int rpid, int PRUID, string GID, int GRPNG, string plot, int distribution)
		{
			P p = new P(path, "reportImage.aspx");
			p.Q.Add("LangID", langID);
			p.Q.Add("FY", fy);
			p.Q.Add("TY", ty);
			p.Q.Add("SAID", SPONS);
			p.Q.Add("SID", SID);
			p.Q.Add("GB", GB);
			p.Q.Add("RPID", rpid);
			p.Q.Add("PRUID", PRUID);
			p.Q.Add("GID", GID);
			p.Q.Add("GRPNG", GRPNG);
			p.Q.Add("PLOT", plot);
			p.Q.Add("DIST", distribution);
			return p.ToString();
		}

		// Adds child parts and generates content of the specified part.
		private void CreateParts(WordprocessingDocument document, ReportPartLanguage r, string url)
		{
			ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
			GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

			MainDocumentPart mainDocumentPart1 = document.AddMainDocumentPart();
			GenerateMainDocumentPart1Content(mainDocumentPart1, r);

			WebSettingsPart webSettingsPart1 = mainDocumentPart1.AddNewPart<WebSettingsPart>("rId3");
			GenerateWebSettingsPart1Content(webSettingsPart1);

			DocumentSettingsPart documentSettingsPart1 = mainDocumentPart1.AddNewPart<DocumentSettingsPart>("rId2");
			GenerateDocumentSettingsPart1Content(documentSettingsPart1);

			StyleDefinitionsPart styleDefinitionsPart1 = mainDocumentPart1.AddNewPart<StyleDefinitionsPart>("rId1");
			GenerateStyleDefinitionsPart1Content(styleDefinitionsPart1);

			ThemePart themePart1 = mainDocumentPart1.AddNewPart<ThemePart>("rId6");
			GenerateThemePart1Content(themePart1);

			FontTablePart fontTablePart1 = mainDocumentPart1.AddNewPart<FontTablePart>("rId5");
			GenerateFontTablePart1Content(fontTablePart1);

			ImagePart imagePart1 = mainDocumentPart1.AddNewPart<ImagePart>("image/gif", "rId4");
			GenerateImagePart1Content(imagePart1, url);

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
			totalTime1.Text = "22";
			Ap.Pages pages1 = new Ap.Pages();
			pages1.Text = "1";
			Ap.Words words1 = new Ap.Words();
			words1.Text = "1";
			Ap.Characters characters1 = new Ap.Characters();
			characters1.Text = "9";
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
			vTLPSTR1.Text = "Title";

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
			charactersWithSpaces1.Text = "9";
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

			Paragraph paragraph1 = new Paragraph(){ RsidParagraphAddition = "0067381F", RsidRunAdditionDefault = "0067381F" };

			Run run1 = new Run();
			Text text1 = new Text();
//			text1.Text = "Subject";
			text1.Text = r.Subject;

			run1.Append(text1);

			paragraph1.Append(run1);

			Paragraph paragraph2 = new Paragraph(){ RsidParagraphAddition = "006D2E38", RsidRunAdditionDefault = "006D2E38" };

			Run run2 = new Run();

			RunProperties runProperties1 = new RunProperties();
			NoProof noProof1 = new NoProof();

			runProperties1.Append(noProof1);

			Drawing drawing1 = new Drawing();

			Wp.Inline inline1 = new Wp.Inline(){ DistanceFromTop = (UInt32Value)0U, DistanceFromBottom = (UInt32Value)0U, DistanceFromLeft = (UInt32Value)0U, DistanceFromRight = (UInt32Value)0U };
			Wp.Extent extent1 = new Wp.Extent(){ Cx = 5943600L, Cy = 6918325L };
			Wp.EffectExtent effectExtent1 = new Wp.EffectExtent(){ LeftEdge = 19050L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L };
			Wp.DocProperties docProperties1 = new Wp.DocProperties(){ Id = (UInt32Value)2U, Name = "Picture 1", Description = "test.gif" };

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
			Pic.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Pic.NonVisualDrawingProperties(){ Id = (UInt32Value)0U, Name = "test.gif" };
			Pic.NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties1 = new Pic.NonVisualPictureDrawingProperties();

			nonVisualPictureProperties1.Append(nonVisualDrawingProperties1);
			nonVisualPictureProperties1.Append(nonVisualPictureDrawingProperties1);

			Pic.BlipFill blipFill1 = new Pic.BlipFill();
			A.Blip blip1 = new A.Blip(){ Embed = "rId4" };

			A.Stretch stretch1 = new A.Stretch();
			A.FillRectangle fillRectangle1 = new A.FillRectangle();

			stretch1.Append(fillRectangle1);

			blipFill1.Append(blip1);
			blipFill1.Append(stretch1);

			Pic.ShapeProperties shapeProperties1 = new Pic.ShapeProperties();

			A.Transform2D transform2D1 = new A.Transform2D();
			A.Offset offset1 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents1 = new A.Extents(){ Cx = 5943600L, Cy = 6918325L };

			transform2D1.Append(offset1);
			transform2D1.Append(extents1);

			A.PresetGeometry presetGeometry1 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
			A.AdjustValueList adjustValueList1 = new A.AdjustValueList();

			presetGeometry1.Append(adjustValueList1);

			shapeProperties1.Append(transform2D1);
			shapeProperties1.Append(presetGeometry1);

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

			paragraph2.Append(run2);

			SectionProperties sectionProperties1 = new SectionProperties(){ RsidR = "006D2E38", RsidSect = "00E70BB7" };
			PageSize pageSize1 = new PageSize(){ Width = (UInt32Value)12240U, Height = (UInt32Value)15840U };
			PageMargin pageMargin1 = new PageMargin(){ Top = 1440, Right = (UInt32Value)1440U, Bottom = 1440, Left = (UInt32Value)1440U, Header = (UInt32Value)720U, Footer = (UInt32Value)720U, Gutter = (UInt32Value)0U };
			Columns columns1 = new Columns(){ Space = "720" };
			DocGrid docGrid1 = new DocGrid(){ LinePitch = 360 };

			sectionProperties1.Append(pageSize1);
			sectionProperties1.Append(pageMargin1);
			sectionProperties1.Append(columns1);
			sectionProperties1.Append(docGrid1);

			body1.Append(paragraph1);
			body1.Append(paragraph2);
			body1.Append(sectionProperties1);

			document1.Append(body1);

			mainDocumentPart1.Document = document1;
		}

		// Generates content of webSettingsPart1.
		private void GenerateWebSettingsPart1Content(WebSettingsPart webSettingsPart1)
		{
			WebSettings webSettings1 = new WebSettings();
			webSettings1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			webSettings1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
			OptimizeForBrowser optimizeForBrowser1 = new OptimizeForBrowser();

			webSettings1.Append(optimizeForBrowser1);

			webSettingsPart1.WebSettings = webSettings1;
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
			ProofState proofState1 = new ProofState(){ Spelling = ProofingStateValues.Clean, Grammar = ProofingStateValues.Clean };
			DefaultTabStop defaultTabStop1 = new DefaultTabStop(){ Val = 720 };
			CharacterSpacingControl characterSpacingControl1 = new CharacterSpacingControl(){ Val = CharacterSpacingValues.DoNotCompress };

			Compatibility compatibility1 = new Compatibility();
			UseFarEastLayout useFarEastLayout1 = new UseFarEastLayout();

			compatibility1.Append(useFarEastLayout1);

			Rsids rsids1 = new Rsids();
			RsidRoot rsidRoot1 = new RsidRoot(){ Val = "0067381F" };
			Rsid rsid1 = new Rsid(){ Val = "0067381F" };
			Rsid rsid2 = new Rsid(){ Val = "006D2E38" };
			Rsid rsid3 = new Rsid(){ Val = "008737FA" };
			Rsid rsid4 = new Rsid(){ Val = "00E70BB7" };

			rsids1.Append(rsidRoot1);
			rsids1.Append(rsid1);
			rsids1.Append(rsid2);
			rsids1.Append(rsid3);
			rsids1.Append(rsid4);

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

			ShapeDefaults shapeDefaults1 = new ShapeDefaults();
			Ovml.ShapeDefaults shapeDefaults2 = new Ovml.ShapeDefaults(){ Extension = V.ExtensionHandlingBehaviorValues.Edit, MaxShapeId = 3074 };

			Ovml.ShapeLayout shapeLayout1 = new Ovml.ShapeLayout(){ Extension = V.ExtensionHandlingBehaviorValues.Edit };
			Ovml.ShapeIdMap shapeIdMap1 = new Ovml.ShapeIdMap(){ Extension = V.ExtensionHandlingBehaviorValues.Edit, Data = "1" };

			shapeLayout1.Append(shapeIdMap1);

			shapeDefaults1.Append(shapeDefaults2);
			shapeDefaults1.Append(shapeLayout1);
			DecimalSymbol decimalSymbol1 = new DecimalSymbol(){ Val = "." };
			ListSeparator listSeparator1 = new ListSeparator(){ Val = "," };

			settings1.Append(zoom1);
			settings1.Append(proofState1);
			settings1.Append(defaultTabStop1);
			settings1.Append(characterSpacingControl1);
			settings1.Append(compatibility1);
			settings1.Append(rsids1);
			settings1.Append(mathProperties1);
			settings1.Append(themeFontLanguages1);
			settings1.Append(colorSchemeMapping1);
			settings1.Append(shapeDefaults1);
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
			RunFonts runFonts1 = new RunFonts(){ AsciiTheme = ThemeFontValues.MinorHighAnsi, HighAnsiTheme = ThemeFontValues.MinorHighAnsi, EastAsiaTheme = ThemeFontValues.MinorEastAsia, ComplexScriptTheme = ThemeFontValues.MinorBidi };
			FontSize fontSize1 = new FontSize(){ Val = "22" };
			FontSizeComplexScript fontSizeComplexScript1 = new FontSizeComplexScript(){ Val = "22" };
			Languages languages1 = new Languages(){ Val = "en-US", EastAsia = "en-US", Bidi = "ar-SA" };

			runPropertiesBaseStyle1.Append(runFonts1);
			runPropertiesBaseStyle1.Append(fontSize1);
			runPropertiesBaseStyle1.Append(fontSizeComplexScript1);
			runPropertiesBaseStyle1.Append(languages1);

			runPropertiesDefault1.Append(runPropertiesBaseStyle1);

			ParagraphPropertiesDefault paragraphPropertiesDefault1 = new ParagraphPropertiesDefault();

			ParagraphPropertiesBaseStyle paragraphPropertiesBaseStyle1 = new ParagraphPropertiesBaseStyle();
			SpacingBetweenLines spacingBetweenLines1 = new SpacingBetweenLines(){ After = "200", Line = "276", LineRule = LineSpacingRuleValues.Auto };

			paragraphPropertiesBaseStyle1.Append(spacingBetweenLines1);

			paragraphPropertiesDefault1.Append(paragraphPropertiesBaseStyle1);

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
			Rsid rsid5 = new Rsid(){ Val = "00E70BB7" };

			style1.Append(styleName1);
			style1.Append(primaryStyle1);
			style1.Append(rsid5);

			Style style2 = new Style(){ Type = StyleValues.Character, StyleId = "DefaultParagraphFont", Default = true };
			StyleName styleName2 = new StyleName(){ Val = "Default Paragraph Font" };
			UIPriority uIPriority1 = new UIPriority(){ Val = 1 };
			SemiHidden semiHidden1 = new SemiHidden();
			UnhideWhenUsed unhideWhenUsed1 = new UnhideWhenUsed();

			style2.Append(styleName2);
			style2.Append(uIPriority1);
			style2.Append(semiHidden1);
			style2.Append(unhideWhenUsed1);

			Style style3 = new Style(){ Type = StyleValues.Table, StyleId = "TableNormal", Default = true };
			StyleName styleName3 = new StyleName(){ Val = "Normal Table" };
			UIPriority uIPriority2 = new UIPriority(){ Val = 99 };
			SemiHidden semiHidden2 = new SemiHidden();
			UnhideWhenUsed unhideWhenUsed2 = new UnhideWhenUsed();
			PrimaryStyle primaryStyle2 = new PrimaryStyle();

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

			style3.Append(styleName3);
			style3.Append(uIPriority2);
			style3.Append(semiHidden2);
			style3.Append(unhideWhenUsed2);
			style3.Append(primaryStyle2);
			style3.Append(styleTableProperties1);

			Style style4 = new Style(){ Type = StyleValues.Numbering, StyleId = "NoList", Default = true };
			StyleName styleName4 = new StyleName(){ Val = "No List" };
			UIPriority uIPriority3 = new UIPriority(){ Val = 99 };
			SemiHidden semiHidden3 = new SemiHidden();
			UnhideWhenUsed unhideWhenUsed3 = new UnhideWhenUsed();

			style4.Append(styleName4);
			style4.Append(uIPriority3);
			style4.Append(semiHidden3);
			style4.Append(unhideWhenUsed3);

			Style style5 = new Style(){ Type = StyleValues.Character, StyleId = "Hyperlink" };
			StyleName styleName5 = new StyleName(){ Val = "Hyperlink" };
			BasedOn basedOn1 = new BasedOn(){ Val = "DefaultParagraphFont" };
			UIPriority uIPriority4 = new UIPriority(){ Val = 99 };
			UnhideWhenUsed unhideWhenUsed4 = new UnhideWhenUsed();
			Rsid rsid6 = new Rsid(){ Val = "0067381F" };

			StyleRunProperties styleRunProperties1 = new StyleRunProperties();
			Color color1 = new Color(){ Val = "0000FF", ThemeColor = ThemeColorValues.Hyperlink };
			Underline underline1 = new Underline(){ Val = UnderlineValues.Single };

			styleRunProperties1.Append(color1);
			styleRunProperties1.Append(underline1);

			style5.Append(styleName5);
			style5.Append(basedOn1);
			style5.Append(uIPriority4);
			style5.Append(unhideWhenUsed4);
			style5.Append(rsid6);
			style5.Append(styleRunProperties1);

			Style style6 = new Style(){ Type = StyleValues.Paragraph, StyleId = "BalloonText" };
			StyleName styleName6 = new StyleName(){ Val = "Balloon Text" };
			BasedOn basedOn2 = new BasedOn(){ Val = "Normal" };
			LinkedStyle linkedStyle1 = new LinkedStyle(){ Val = "BalloonTextChar" };
			UIPriority uIPriority5 = new UIPriority(){ Val = 99 };
			SemiHidden semiHidden4 = new SemiHidden();
			UnhideWhenUsed unhideWhenUsed5 = new UnhideWhenUsed();
			Rsid rsid7 = new Rsid(){ Val = "0067381F" };

			StyleParagraphProperties styleParagraphProperties1 = new StyleParagraphProperties();
			SpacingBetweenLines spacingBetweenLines2 = new SpacingBetweenLines(){ After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

			styleParagraphProperties1.Append(spacingBetweenLines2);

			StyleRunProperties styleRunProperties2 = new StyleRunProperties();
			RunFonts runFonts2 = new RunFonts(){ Ascii = "Tahoma", HighAnsi = "Tahoma", ComplexScript = "Tahoma" };
			FontSize fontSize2 = new FontSize(){ Val = "16" };
			FontSizeComplexScript fontSizeComplexScript2 = new FontSizeComplexScript(){ Val = "16" };

			styleRunProperties2.Append(runFonts2);
			styleRunProperties2.Append(fontSize2);
			styleRunProperties2.Append(fontSizeComplexScript2);

			style6.Append(styleName6);
			style6.Append(basedOn2);
			style6.Append(linkedStyle1);
			style6.Append(uIPriority5);
			style6.Append(semiHidden4);
			style6.Append(unhideWhenUsed5);
			style6.Append(rsid7);
			style6.Append(styleParagraphProperties1);
			style6.Append(styleRunProperties2);

			Style style7 = new Style(){ Type = StyleValues.Character, StyleId = "BalloonTextChar", CustomStyle = true };
			StyleName styleName7 = new StyleName(){ Val = "Balloon Text Char" };
			BasedOn basedOn3 = new BasedOn(){ Val = "DefaultParagraphFont" };
			LinkedStyle linkedStyle2 = new LinkedStyle(){ Val = "BalloonText" };
			UIPriority uIPriority6 = new UIPriority(){ Val = 99 };
			SemiHidden semiHidden5 = new SemiHidden();
			Rsid rsid8 = new Rsid(){ Val = "0067381F" };

			StyleRunProperties styleRunProperties3 = new StyleRunProperties();
			RunFonts runFonts3 = new RunFonts(){ Ascii = "Tahoma", HighAnsi = "Tahoma", ComplexScript = "Tahoma" };
			FontSize fontSize3 = new FontSize(){ Val = "16" };
			FontSizeComplexScript fontSizeComplexScript3 = new FontSizeComplexScript(){ Val = "16" };

			styleRunProperties3.Append(runFonts3);
			styleRunProperties3.Append(fontSize3);
			styleRunProperties3.Append(fontSizeComplexScript3);

			style7.Append(styleName7);
			style7.Append(basedOn3);
			style7.Append(linkedStyle2);
			style7.Append(uIPriority6);
			style7.Append(semiHidden5);
			style7.Append(rsid8);
			style7.Append(styleRunProperties3);

			styles1.Append(docDefaults1);
			styles1.Append(latentStyles1);
			styles1.Append(style1);
			styles1.Append(style2);
			styles1.Append(style3);
			styles1.Append(style4);
			styles1.Append(style5);
			styles1.Append(style6);
			styles1.Append(style7);

			styleDefinitionsPart1.Styles = styles1;
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

			A.Outline outline1 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

			A.SolidFill solidFill2 = new A.SolidFill();

			A.SchemeColor schemeColor8 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade4 = new A.Shade(){ Val = 95000 };
			A.SaturationModulation saturationModulation7 = new A.SaturationModulation(){ Val = 105000 };

			schemeColor8.Append(shade4);
			schemeColor8.Append(saturationModulation7);

			solidFill2.Append(schemeColor8);
			A.PresetDash presetDash1 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

			outline1.Append(solidFill2);
			outline1.Append(presetDash1);

			A.Outline outline2 = new A.Outline(){ Width = 25400, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

			A.SolidFill solidFill3 = new A.SolidFill();
			A.SchemeColor schemeColor9 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

			solidFill3.Append(schemeColor9);
			A.PresetDash presetDash2 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

			outline2.Append(solidFill3);
			outline2.Append(presetDash2);

			A.Outline outline3 = new A.Outline(){ Width = 38100, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

			A.SolidFill solidFill4 = new A.SolidFill();
			A.SchemeColor schemeColor10 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

			solidFill4.Append(schemeColor10);
			A.PresetDash presetDash3 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

			outline3.Append(solidFill4);
			outline3.Append(presetDash3);

			lineStyleList1.Append(outline1);
			lineStyleList1.Append(outline2);
			lineStyleList1.Append(outline3);

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

			Font font3 = new Font(){ Name = "Tahoma" };
			Panose1Number panose1Number3 = new Panose1Number(){ Val = "020B0604030504040204" };
			FontCharSet fontCharSet3 = new FontCharSet(){ Val = "00" };
			FontFamily fontFamily3 = new FontFamily(){ Val = FontFamilyValues.Swiss };
			NotTrueType notTrueType1 = new NotTrueType();
			Pitch pitch3 = new Pitch(){ Val = FontPitchValues.Variable };
			FontSignature fontSignature3 = new FontSignature(){ UnicodeSignature0 = "00000003", UnicodeSignature1 = "00000000", UnicodeSignature2 = "00000000", UnicodeSignature3 = "00000000", CodePageSignature0 = "00000001", CodePageSignature1 = "00000000" };

			font3.Append(panose1Number3);
			font3.Append(fontCharSet3);
			font3.Append(fontFamily3);
			font3.Append(notTrueType1);
			font3.Append(pitch3);
			font3.Append(fontSignature3);

			Font font4 = new Font(){ Name = "Cambria" };
			Panose1Number panose1Number4 = new Panose1Number(){ Val = "02040503050406030204" };
			FontCharSet fontCharSet4 = new FontCharSet(){ Val = "00" };
			FontFamily fontFamily4 = new FontFamily(){ Val = FontFamilyValues.Roman };
			Pitch pitch4 = new Pitch(){ Val = FontPitchValues.Variable };
			FontSignature fontSignature4 = new FontSignature(){ UnicodeSignature0 = "A00002EF", UnicodeSignature1 = "4000004B", UnicodeSignature2 = "00000000", UnicodeSignature3 = "00000000", CodePageSignature0 = "0000019F", CodePageSignature1 = "00000000" };

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

		// Generates content of imagePart1.
		private void GenerateImagePart1Content(ImagePart imagePart1, string url)
		{
			//        	System.IO.Stream data = GetBinaryDataStream(imagePart1Data);
			//        	imagePart1.FeedData(data);
			//        	data.Close();
			WebRequest req = WebRequest.Create(url);
			WebResponse response = req.GetResponse();
			Stream stream = response.GetResponseStream();
			imagePart1.FeedData(stream);
		}

		private void SetPackageProperties(OpenXmlPackage document)
		{
			document.PackageProperties.Creator = "Ian";
			document.PackageProperties.Title = "";
			document.PackageProperties.Subject = "";
			document.PackageProperties.Keywords = "";
			document.PackageProperties.Description = "";
			document.PackageProperties.Revision = "3";
			document.PackageProperties.Created = System.Xml.XmlConvert.ToDateTime("2013-03-25T07:20:00Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
			document.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime("2013-03-25T07:46:00Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
			document.PackageProperties.LastModifiedBy = "Ian";
		}

		#region Binary Data
		private string imagePart1Data = "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";

		private System.IO.Stream GetBinaryDataStream(string base64String)
		{
			return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
		}

		#endregion


	}
}
