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
				//CreateParts(doc);
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

		// Adds child parts and generates content of the specified part.
		/*private void CreateParts(PresentationDocument document)
		{
			ThumbnailPart thumbnailPart1 = document.AddNewPart<ThumbnailPart>("image/jpeg", "rId2");
			GenerateThumbnailPart1Content(thumbnailPart1);

			PresentationPart presentationPart1 = document.AddPresentationPart();
			GeneratePresentationPart1Content(presentationPart1);

			PresentationPropertiesPart presentationPropertiesPart1 = presentationPart1.AddNewPart<PresentationPropertiesPart>("rId3");
			GeneratePresentationPropertiesPart1Content(presentationPropertiesPart1);

			SlidePart slidePart1 = presentationPart1.AddNewPart<SlidePart>("rId2");
			GenerateSlidePart1Content(slidePart1);

			ImagePart imagePart1 = slidePart1.AddNewPart<ImagePart>("image/gif", "rId2");
			GenerateImagePart1Content(imagePart1);

			SlideLayoutPart slideLayoutPart1 = slidePart1.AddNewPart<SlideLayoutPart>("rId1");
			GenerateSlideLayoutPart1Content(slideLayoutPart1);

			SlideMasterPart slideMasterPart1 = slideLayoutPart1.AddNewPart<SlideMasterPart>("rId1");
			GenerateSlideMasterPart1Content(slideMasterPart1);

			SlideLayoutPart slideLayoutPart2 = slideMasterPart1.AddNewPart<SlideLayoutPart>("rId8");
			GenerateSlideLayoutPart2Content(slideLayoutPart2);

			slideLayoutPart2.AddPart(slideMasterPart1, "rId1");

			SlideLayoutPart slideLayoutPart3 = slideMasterPart1.AddNewPart<SlideLayoutPart>("rId3");
			GenerateSlideLayoutPart3Content(slideLayoutPart3);

			slideLayoutPart3.AddPart(slideMasterPart1, "rId1");

			SlideLayoutPart slideLayoutPart4 = slideMasterPart1.AddNewPart<SlideLayoutPart>("rId7");
			GenerateSlideLayoutPart4Content(slideLayoutPart4);

			slideLayoutPart4.AddPart(slideMasterPart1, "rId1");

			ThemePart themePart1 = slideMasterPart1.AddNewPart<ThemePart>("rId12");
			GenerateThemePart1Content(themePart1);

			slideMasterPart1.AddPart(slideLayoutPart1, "rId2");

			SlideLayoutPart slideLayoutPart5 = slideMasterPart1.AddNewPart<SlideLayoutPart>("rId1");
			GenerateSlideLayoutPart5Content(slideLayoutPart5);

			slideLayoutPart5.AddPart(slideMasterPart1, "rId1");

			SlideLayoutPart slideLayoutPart6 = slideMasterPart1.AddNewPart<SlideLayoutPart>("rId6");
			GenerateSlideLayoutPart6Content(slideLayoutPart6);

			slideLayoutPart6.AddPart(slideMasterPart1, "rId1");

			SlideLayoutPart slideLayoutPart7 = slideMasterPart1.AddNewPart<SlideLayoutPart>("rId11");
			GenerateSlideLayoutPart7Content(slideLayoutPart7);

			slideLayoutPart7.AddPart(slideMasterPart1, "rId1");

			SlideLayoutPart slideLayoutPart8 = slideMasterPart1.AddNewPart<SlideLayoutPart>("rId5");
			GenerateSlideLayoutPart8Content(slideLayoutPart8);

			slideLayoutPart8.AddPart(slideMasterPart1, "rId1");

			SlideLayoutPart slideLayoutPart9 = slideMasterPart1.AddNewPart<SlideLayoutPart>("rId10");
			GenerateSlideLayoutPart9Content(slideLayoutPart9);

			slideLayoutPart9.AddPart(slideMasterPart1, "rId1");

			SlideLayoutPart slideLayoutPart10 = slideMasterPart1.AddNewPart<SlideLayoutPart>("rId4");
			GenerateSlideLayoutPart10Content(slideLayoutPart10);

			slideLayoutPart10.AddPart(slideMasterPart1, "rId1");

			SlideLayoutPart slideLayoutPart11 = slideMasterPart1.AddNewPart<SlideLayoutPart>("rId9");
			GenerateSlideLayoutPart11Content(slideLayoutPart11);

			slideLayoutPart11.AddPart(slideMasterPart1, "rId1");

			presentationPart1.AddPart(slideMasterPart1, "rId1");

			TableStylesPart tableStylesPart1 = presentationPart1.AddNewPart<TableStylesPart>("rId6");
			GenerateTableStylesPart1Content(tableStylesPart1);

			presentationPart1.AddPart(themePart1, "rId5");

			ViewPropertiesPart viewPropertiesPart1 = presentationPart1.AddNewPart<ViewPropertiesPart>("rId4");
			GenerateViewPropertiesPart1Content(viewPropertiesPart1);

			ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId4");
			GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

			SetPackageProperties(document);
		}

		// Generates content of thumbnailPart1.
		private void GenerateThumbnailPart1Content(ThumbnailPart thumbnailPart1)
		{
			System.IO.Stream data = GetBinaryDataStream(thumbnailPart1Data);
			thumbnailPart1.FeedData(data);
			data.Close();
		}

		// Generates content of presentationPart1.
		private void GeneratePresentationPart1Content(PresentationPart presentationPart1)
		{
			Presentation presentation1 = new Presentation(){ SaveSubsetFonts = true };
			presentation1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			presentation1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			presentation1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			SlideMasterIdList slideMasterIdList1 = new SlideMasterIdList();
			SlideMasterId slideMasterId1 = new SlideMasterId(){ Id = (UInt32Value)2147483648U, RelationshipId = "rId1" };

			slideMasterIdList1.Append(slideMasterId1);

			SlideIdList slideIdList1 = new SlideIdList();
			SlideId slideId1 = new SlideId(){ Id = (UInt32Value)258U, RelationshipId = "rId2" };

			slideIdList1.Append(slideId1);
			SlideSize slideSize1 = new SlideSize(){ Cx = 9144000, Cy = 6858000, Type = SlideSizeValues.Screen4x3 };
			NotesSize notesSize1 = new NotesSize(){ Cx = 6858000, Cy = 9144000 };

			DefaultTextStyle defaultTextStyle1 = new DefaultTextStyle();

			A.DefaultParagraphProperties defaultParagraphProperties1 = new A.DefaultParagraphProperties();
			A.DefaultRunProperties defaultRunProperties1 = new A.DefaultRunProperties(){ Language = "en-US" };

			defaultParagraphProperties1.Append(defaultRunProperties1);

			A.Level1ParagraphProperties level1ParagraphProperties1 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties2 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill1 = new A.SolidFill();
			A.SchemeColor schemeColor1 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill1.Append(schemeColor1);
			A.LatinFont latinFont1 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont1 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont1 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties2.Append(solidFill1);
			defaultRunProperties2.Append(latinFont1);
			defaultRunProperties2.Append(eastAsianFont1);
			defaultRunProperties2.Append(complexScriptFont1);

			level1ParagraphProperties1.Append(defaultRunProperties2);

			A.Level2ParagraphProperties level2ParagraphProperties1 = new A.Level2ParagraphProperties(){ LeftMargin = 457200, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties3 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill2 = new A.SolidFill();
			A.SchemeColor schemeColor2 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill2.Append(schemeColor2);
			A.LatinFont latinFont2 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont2 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont2 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties3.Append(solidFill2);
			defaultRunProperties3.Append(latinFont2);
			defaultRunProperties3.Append(eastAsianFont2);
			defaultRunProperties3.Append(complexScriptFont2);

			level2ParagraphProperties1.Append(defaultRunProperties3);

			A.Level3ParagraphProperties level3ParagraphProperties1 = new A.Level3ParagraphProperties(){ LeftMargin = 914400, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties4 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill3 = new A.SolidFill();
			A.SchemeColor schemeColor3 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill3.Append(schemeColor3);
			A.LatinFont latinFont3 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont3 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont3 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties4.Append(solidFill3);
			defaultRunProperties4.Append(latinFont3);
			defaultRunProperties4.Append(eastAsianFont3);
			defaultRunProperties4.Append(complexScriptFont3);

			level3ParagraphProperties1.Append(defaultRunProperties4);

			A.Level4ParagraphProperties level4ParagraphProperties1 = new A.Level4ParagraphProperties(){ LeftMargin = 1371600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties5 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill4 = new A.SolidFill();
			A.SchemeColor schemeColor4 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill4.Append(schemeColor4);
			A.LatinFont latinFont4 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont4 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont4 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties5.Append(solidFill4);
			defaultRunProperties5.Append(latinFont4);
			defaultRunProperties5.Append(eastAsianFont4);
			defaultRunProperties5.Append(complexScriptFont4);

			level4ParagraphProperties1.Append(defaultRunProperties5);

			A.Level5ParagraphProperties level5ParagraphProperties1 = new A.Level5ParagraphProperties(){ LeftMargin = 1828800, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties6 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill5 = new A.SolidFill();
			A.SchemeColor schemeColor5 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill5.Append(schemeColor5);
			A.LatinFont latinFont5 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont5 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont5 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties6.Append(solidFill5);
			defaultRunProperties6.Append(latinFont5);
			defaultRunProperties6.Append(eastAsianFont5);
			defaultRunProperties6.Append(complexScriptFont5);

			level5ParagraphProperties1.Append(defaultRunProperties6);

			A.Level6ParagraphProperties level6ParagraphProperties1 = new A.Level6ParagraphProperties(){ LeftMargin = 2286000, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties7 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill6 = new A.SolidFill();
			A.SchemeColor schemeColor6 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill6.Append(schemeColor6);
			A.LatinFont latinFont6 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont6 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont6 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties7.Append(solidFill6);
			defaultRunProperties7.Append(latinFont6);
			defaultRunProperties7.Append(eastAsianFont6);
			defaultRunProperties7.Append(complexScriptFont6);

			level6ParagraphProperties1.Append(defaultRunProperties7);

			A.Level7ParagraphProperties level7ParagraphProperties1 = new A.Level7ParagraphProperties(){ LeftMargin = 2743200, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties8 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill7 = new A.SolidFill();
			A.SchemeColor schemeColor7 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill7.Append(schemeColor7);
			A.LatinFont latinFont7 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont7 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont7 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties8.Append(solidFill7);
			defaultRunProperties8.Append(latinFont7);
			defaultRunProperties8.Append(eastAsianFont7);
			defaultRunProperties8.Append(complexScriptFont7);

			level7ParagraphProperties1.Append(defaultRunProperties8);

			A.Level8ParagraphProperties level8ParagraphProperties1 = new A.Level8ParagraphProperties(){ LeftMargin = 3200400, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties9 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill8 = new A.SolidFill();
			A.SchemeColor schemeColor8 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill8.Append(schemeColor8);
			A.LatinFont latinFont8 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont8 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont8 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties9.Append(solidFill8);
			defaultRunProperties9.Append(latinFont8);
			defaultRunProperties9.Append(eastAsianFont8);
			defaultRunProperties9.Append(complexScriptFont8);

			level8ParagraphProperties1.Append(defaultRunProperties9);

			A.Level9ParagraphProperties level9ParagraphProperties1 = new A.Level9ParagraphProperties(){ LeftMargin = 3657600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties10 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill9 = new A.SolidFill();
			A.SchemeColor schemeColor9 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill9.Append(schemeColor9);
			A.LatinFont latinFont9 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont9 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont9 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties10.Append(solidFill9);
			defaultRunProperties10.Append(latinFont9);
			defaultRunProperties10.Append(eastAsianFont9);
			defaultRunProperties10.Append(complexScriptFont9);

			level9ParagraphProperties1.Append(defaultRunProperties10);

			defaultTextStyle1.Append(defaultParagraphProperties1);
			defaultTextStyle1.Append(level1ParagraphProperties1);
			defaultTextStyle1.Append(level2ParagraphProperties1);
			defaultTextStyle1.Append(level3ParagraphProperties1);
			defaultTextStyle1.Append(level4ParagraphProperties1);
			defaultTextStyle1.Append(level5ParagraphProperties1);
			defaultTextStyle1.Append(level6ParagraphProperties1);
			defaultTextStyle1.Append(level7ParagraphProperties1);
			defaultTextStyle1.Append(level8ParagraphProperties1);
			defaultTextStyle1.Append(level9ParagraphProperties1);

			presentation1.Append(slideMasterIdList1);
			presentation1.Append(slideIdList1);
			presentation1.Append(slideSize1);
			presentation1.Append(notesSize1);
			presentation1.Append(defaultTextStyle1);

			presentationPart1.Presentation = presentation1;
		}

		// Generates content of presentationPropertiesPart1.
		private void GeneratePresentationPropertiesPart1Content(PresentationPropertiesPart presentationPropertiesPart1)
		{
			PresentationProperties presentationProperties1 = new PresentationProperties();
			presentationProperties1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			presentationProperties1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			presentationProperties1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			presentationPropertiesPart1.PresentationProperties = presentationProperties1;
		}

		// Generates content of slidePart1.
		private void GenerateSlidePart1Content(SlidePart slidePart1)
		{
			Slide slide1 = new Slide();
			slide1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slide1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slide1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData1 = new CommonSlideData();

			ShapeTree shapeTree1 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties1 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties1 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties1 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties1 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties1.Append(nonVisualDrawingProperties1);
			nonVisualGroupShapeProperties1.Append(nonVisualGroupShapeDrawingProperties1);
			nonVisualGroupShapeProperties1.Append(applicationNonVisualDrawingProperties1);

			GroupShapeProperties groupShapeProperties1 = new GroupShapeProperties();

			A.TransformGroup transformGroup1 = new A.TransformGroup();
			A.Offset offset1 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents1 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset1 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents1 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup1.Append(offset1);
			transformGroup1.Append(extents1);
			transformGroup1.Append(childOffset1);
			transformGroup1.Append(childExtents1);

			groupShapeProperties1.Append(transformGroup1);

			Shape shape1 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties1 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties2 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties1 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks1 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties1.Append(shapeLocks1);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties2 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape1 = new PlaceholderShape(){ Type = PlaceholderValues.Title };

			applicationNonVisualDrawingProperties2.Append(placeholderShape1);

			nonVisualShapeProperties1.Append(nonVisualDrawingProperties2);
			nonVisualShapeProperties1.Append(nonVisualShapeDrawingProperties1);
			nonVisualShapeProperties1.Append(applicationNonVisualDrawingProperties2);
			ShapeProperties shapeProperties1 = new ShapeProperties();

			TextBody textBody1 = new TextBody();
			A.BodyProperties bodyProperties1 = new A.BodyProperties();
			A.ListStyle listStyle1 = new A.ListStyle();

			A.Paragraph paragraph1 = new A.Paragraph();

			A.Run run1 = new A.Run();
			A.RunProperties runProperties1 = new A.RunProperties(){ Language = "en-US", Dirty = false, SmartTagClean = false };
			A.Text text1 = new A.Text();
			text1.Text = "Subject";

			run1.Append(runProperties1);
			run1.Append(text1);
			A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties(){ Language = "en-US", Dirty = false };

			paragraph1.Append(run1);
			paragraph1.Append(endParagraphRunProperties1);

			textBody1.Append(bodyProperties1);
			textBody1.Append(listStyle1);
			textBody1.Append(paragraph1);

			shape1.Append(nonVisualShapeProperties1);
			shape1.Append(shapeProperties1);
			shape1.Append(textBody1);

			Shape shape2 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties2 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties3 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Content Placeholder 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties2 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks2 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties2.Append(shapeLocks2);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties3 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape2 = new PlaceholderShape(){ Index = (UInt32Value)1U };

			applicationNonVisualDrawingProperties3.Append(placeholderShape2);

			nonVisualShapeProperties2.Append(nonVisualDrawingProperties3);
			nonVisualShapeProperties2.Append(nonVisualShapeDrawingProperties2);
			nonVisualShapeProperties2.Append(applicationNonVisualDrawingProperties3);
			ShapeProperties shapeProperties2 = new ShapeProperties();

			TextBody textBody2 = new TextBody();
			A.BodyProperties bodyProperties2 = new A.BodyProperties();
			A.ListStyle listStyle2 = new A.ListStyle();

			A.Paragraph paragraph2 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties(){ Language = "en-US", Dirty = false };

			paragraph2.Append(endParagraphRunProperties2);

			textBody2.Append(bodyProperties2);
			textBody2.Append(listStyle2);
			textBody2.Append(paragraph2);

			shape2.Append(nonVisualShapeProperties2);
			shape2.Append(shapeProperties2);
			shape2.Append(textBody2);

			Picture picture1 = new Picture();

			NonVisualPictureProperties nonVisualPictureProperties1 = new NonVisualPictureProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties4 = new NonVisualDrawingProperties(){ Id = (UInt32Value)10242U, Name = "Picture 2", Description = "http://localhost:1878/reportImage.aspx?LangID=1&FY=2012&TY=2013&SAID=0&SID=1&Anonymized=1&DIST=0&GB=7&RPID=0&RPLID=0&PRUID=1&GRPNG=2&GID=0,9" };

			NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties1 = new NonVisualPictureDrawingProperties();
			A.PictureLocks pictureLocks1 = new A.PictureLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

			nonVisualPictureDrawingProperties1.Append(pictureLocks1);
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties4 = new ApplicationNonVisualDrawingProperties();

			nonVisualPictureProperties1.Append(nonVisualDrawingProperties4);
			nonVisualPictureProperties1.Append(nonVisualPictureDrawingProperties1);
			nonVisualPictureProperties1.Append(applicationNonVisualDrawingProperties4);

			BlipFill blipFill1 = new BlipFill();
			A.Blip blip1 = new A.Blip(){ Embed = "rId2" };
			A.SourceRectangle sourceRectangle1 = new A.SourceRectangle();

			A.Stretch stretch1 = new A.Stretch();
			A.FillRectangle fillRectangle1 = new A.FillRectangle();

			stretch1.Append(fillRectangle1);

			blipFill1.Append(blip1);
			blipFill1.Append(sourceRectangle1);
			blipFill1.Append(stretch1);

			ShapeProperties shapeProperties3 = new ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

			A.Transform2D transform2D1 = new A.Transform2D();
			A.Offset offset2 = new A.Offset(){ X = 304800L, Y = 1524000L };
			A.Extents extents2 = new A.Extents(){ Cx = 8524875L, Cy = 4191001L };

			transform2D1.Append(offset2);
			transform2D1.Append(extents2);

			A.PresetGeometry presetGeometry1 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
			A.AdjustValueList adjustValueList1 = new A.AdjustValueList();

			presetGeometry1.Append(adjustValueList1);
			A.NoFill noFill1 = new A.NoFill();

			shapeProperties3.Append(transform2D1);
			shapeProperties3.Append(presetGeometry1);
			shapeProperties3.Append(noFill1);

			picture1.Append(nonVisualPictureProperties1);
			picture1.Append(blipFill1);
			picture1.Append(shapeProperties3);

			shapeTree1.Append(nonVisualGroupShapeProperties1);
			shapeTree1.Append(groupShapeProperties1);
			shapeTree1.Append(shape1);
			shapeTree1.Append(shape2);
			shapeTree1.Append(picture1);

			commonSlideData1.Append(shapeTree1);

			ColorMapOverride colorMapOverride1 = new ColorMapOverride();
			A.MasterColorMapping masterColorMapping1 = new A.MasterColorMapping();

			colorMapOverride1.Append(masterColorMapping1);

			slide1.Append(commonSlideData1);
			slide1.Append(colorMapOverride1);

			slidePart1.Slide = slide1;
		}

		// Generates content of imagePart1.
		private void GenerateImagePart1Content(ImagePart imagePart1)
		{
			System.IO.Stream data = GetBinaryDataStream(imagePart1Data);
			imagePart1.FeedData(data);
			data.Close();
		}

		// Generates content of slideLayoutPart1.
		private void GenerateSlideLayoutPart1Content(SlideLayoutPart slideLayoutPart1)
		{
			SlideLayout slideLayout1 = new SlideLayout(){ Type = SlideLayoutValues.Object, Preserve = true };
			slideLayout1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slideLayout1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slideLayout1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData2 = new CommonSlideData(){ Name = "Title and Content" };

			ShapeTree shapeTree2 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties2 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties5 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties2 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties5 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties2.Append(nonVisualDrawingProperties5);
			nonVisualGroupShapeProperties2.Append(nonVisualGroupShapeDrawingProperties2);
			nonVisualGroupShapeProperties2.Append(applicationNonVisualDrawingProperties5);

			GroupShapeProperties groupShapeProperties2 = new GroupShapeProperties();

			A.TransformGroup transformGroup2 = new A.TransformGroup();
			A.Offset offset3 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents3 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset2 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents2 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup2.Append(offset3);
			transformGroup2.Append(extents3);
			transformGroup2.Append(childOffset2);
			transformGroup2.Append(childExtents2);

			groupShapeProperties2.Append(transformGroup2);

			Shape shape3 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties3 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties6 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties3 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks3 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties3.Append(shapeLocks3);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties6 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape3 = new PlaceholderShape(){ Type = PlaceholderValues.Title };

			applicationNonVisualDrawingProperties6.Append(placeholderShape3);

			nonVisualShapeProperties3.Append(nonVisualDrawingProperties6);
			nonVisualShapeProperties3.Append(nonVisualShapeDrawingProperties3);
			nonVisualShapeProperties3.Append(applicationNonVisualDrawingProperties6);
			ShapeProperties shapeProperties4 = new ShapeProperties();

			TextBody textBody3 = new TextBody();
			A.BodyProperties bodyProperties3 = new A.BodyProperties();
			A.ListStyle listStyle3 = new A.ListStyle();

			A.Paragraph paragraph3 = new A.Paragraph();

			A.Run run2 = new A.Run();
			A.RunProperties runProperties2 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text2 = new A.Text();
			text2.Text = "Click to edit Master title style";

			run2.Append(runProperties2);
			run2.Append(text2);
			A.EndParagraphRunProperties endParagraphRunProperties3 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph3.Append(run2);
			paragraph3.Append(endParagraphRunProperties3);

			textBody3.Append(bodyProperties3);
			textBody3.Append(listStyle3);
			textBody3.Append(paragraph3);

			shape3.Append(nonVisualShapeProperties3);
			shape3.Append(shapeProperties4);
			shape3.Append(textBody3);

			Shape shape4 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties4 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties7 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Content Placeholder 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties4 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks4 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties4.Append(shapeLocks4);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties7 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape4 = new PlaceholderShape(){ Index = (UInt32Value)1U };

			applicationNonVisualDrawingProperties7.Append(placeholderShape4);

			nonVisualShapeProperties4.Append(nonVisualDrawingProperties7);
			nonVisualShapeProperties4.Append(nonVisualShapeDrawingProperties4);
			nonVisualShapeProperties4.Append(applicationNonVisualDrawingProperties7);
			ShapeProperties shapeProperties5 = new ShapeProperties();

			TextBody textBody4 = new TextBody();
			A.BodyProperties bodyProperties4 = new A.BodyProperties();
			A.ListStyle listStyle4 = new A.ListStyle();

			A.Paragraph paragraph4 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties1 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run3 = new A.Run();
			A.RunProperties runProperties3 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text3 = new A.Text();
			text3.Text = "Click to edit Master text styles";

			run3.Append(runProperties3);
			run3.Append(text3);

			paragraph4.Append(paragraphProperties1);
			paragraph4.Append(run3);

			A.Paragraph paragraph5 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties2 = new A.ParagraphProperties(){ Level = 1 };

			A.Run run4 = new A.Run();
			A.RunProperties runProperties4 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text4 = new A.Text();
			text4.Text = "Second level";

			run4.Append(runProperties4);
			run4.Append(text4);

			paragraph5.Append(paragraphProperties2);
			paragraph5.Append(run4);

			A.Paragraph paragraph6 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties3 = new A.ParagraphProperties(){ Level = 2 };

			A.Run run5 = new A.Run();
			A.RunProperties runProperties5 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text5 = new A.Text();
			text5.Text = "Third level";

			run5.Append(runProperties5);
			run5.Append(text5);

			paragraph6.Append(paragraphProperties3);
			paragraph6.Append(run5);

			A.Paragraph paragraph7 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties4 = new A.ParagraphProperties(){ Level = 3 };

			A.Run run6 = new A.Run();
			A.RunProperties runProperties6 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text6 = new A.Text();
			text6.Text = "Fourth level";

			run6.Append(runProperties6);
			run6.Append(text6);

			paragraph7.Append(paragraphProperties4);
			paragraph7.Append(run6);

			A.Paragraph paragraph8 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties5 = new A.ParagraphProperties(){ Level = 4 };

			A.Run run7 = new A.Run();
			A.RunProperties runProperties7 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text7 = new A.Text();
			text7.Text = "Fifth level";

			run7.Append(runProperties7);
			run7.Append(text7);
			A.EndParagraphRunProperties endParagraphRunProperties4 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph8.Append(paragraphProperties5);
			paragraph8.Append(run7);
			paragraph8.Append(endParagraphRunProperties4);

			textBody4.Append(bodyProperties4);
			textBody4.Append(listStyle4);
			textBody4.Append(paragraph4);
			textBody4.Append(paragraph5);
			textBody4.Append(paragraph6);
			textBody4.Append(paragraph7);
			textBody4.Append(paragraph8);

			shape4.Append(nonVisualShapeProperties4);
			shape4.Append(shapeProperties5);
			shape4.Append(textBody4);

			Shape shape5 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties5 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties8 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Date Placeholder 3" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties5 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks5 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties5.Append(shapeLocks5);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties8 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape5 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)10U };

			applicationNonVisualDrawingProperties8.Append(placeholderShape5);

			nonVisualShapeProperties5.Append(nonVisualDrawingProperties8);
			nonVisualShapeProperties5.Append(nonVisualShapeDrawingProperties5);
			nonVisualShapeProperties5.Append(applicationNonVisualDrawingProperties8);
			ShapeProperties shapeProperties6 = new ShapeProperties();

			TextBody textBody5 = new TextBody();
			A.BodyProperties bodyProperties5 = new A.BodyProperties();
			A.ListStyle listStyle5 = new A.ListStyle();

			A.Paragraph paragraph9 = new A.Paragraph();

			A.Field field1 = new A.Field(){ Id = "{6A870D7B-DCE0-46E8-B448-2F0E4E8E28D1}", Type = "datetimeFigureOut" };
			A.RunProperties runProperties8 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text8 = new A.Text();
			text8.Text = "3/28/2013";

			field1.Append(runProperties8);
			field1.Append(text8);
			A.EndParagraphRunProperties endParagraphRunProperties5 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph9.Append(field1);
			paragraph9.Append(endParagraphRunProperties5);

			textBody5.Append(bodyProperties5);
			textBody5.Append(listStyle5);
			textBody5.Append(paragraph9);

			shape5.Append(nonVisualShapeProperties5);
			shape5.Append(shapeProperties6);
			shape5.Append(textBody5);

			Shape shape6 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties6 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties9 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Footer Placeholder 4" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties6 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks6 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties6.Append(shapeLocks6);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties9 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape6 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

			applicationNonVisualDrawingProperties9.Append(placeholderShape6);

			nonVisualShapeProperties6.Append(nonVisualDrawingProperties9);
			nonVisualShapeProperties6.Append(nonVisualShapeDrawingProperties6);
			nonVisualShapeProperties6.Append(applicationNonVisualDrawingProperties9);
			ShapeProperties shapeProperties7 = new ShapeProperties();

			TextBody textBody6 = new TextBody();
			A.BodyProperties bodyProperties6 = new A.BodyProperties();
			A.ListStyle listStyle6 = new A.ListStyle();

			A.Paragraph paragraph10 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties6 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph10.Append(endParagraphRunProperties6);

			textBody6.Append(bodyProperties6);
			textBody6.Append(listStyle6);
			textBody6.Append(paragraph10);

			shape6.Append(nonVisualShapeProperties6);
			shape6.Append(shapeProperties7);
			shape6.Append(textBody6);

			Shape shape7 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties7 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties10 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Slide Number Placeholder 5" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties7 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks7 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties7.Append(shapeLocks7);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties10 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape7 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

			applicationNonVisualDrawingProperties10.Append(placeholderShape7);

			nonVisualShapeProperties7.Append(nonVisualDrawingProperties10);
			nonVisualShapeProperties7.Append(nonVisualShapeDrawingProperties7);
			nonVisualShapeProperties7.Append(applicationNonVisualDrawingProperties10);
			ShapeProperties shapeProperties8 = new ShapeProperties();

			TextBody textBody7 = new TextBody();
			A.BodyProperties bodyProperties7 = new A.BodyProperties();
			A.ListStyle listStyle7 = new A.ListStyle();

			A.Paragraph paragraph11 = new A.Paragraph();

			A.Field field2 = new A.Field(){ Id = "{AFBBC1CF-DB04-427F-9A30-B7E1FB44F3B5}", Type = "slidenum" };
			A.RunProperties runProperties9 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text9 = new A.Text();
			text9.Text = "‹#›";

			field2.Append(runProperties9);
			field2.Append(text9);
			A.EndParagraphRunProperties endParagraphRunProperties7 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph11.Append(field2);
			paragraph11.Append(endParagraphRunProperties7);

			textBody7.Append(bodyProperties7);
			textBody7.Append(listStyle7);
			textBody7.Append(paragraph11);

			shape7.Append(nonVisualShapeProperties7);
			shape7.Append(shapeProperties8);
			shape7.Append(textBody7);

			shapeTree2.Append(nonVisualGroupShapeProperties2);
			shapeTree2.Append(groupShapeProperties2);
			shapeTree2.Append(shape3);
			shapeTree2.Append(shape4);
			shapeTree2.Append(shape5);
			shapeTree2.Append(shape6);
			shapeTree2.Append(shape7);

			commonSlideData2.Append(shapeTree2);

			ColorMapOverride colorMapOverride2 = new ColorMapOverride();
			A.MasterColorMapping masterColorMapping2 = new A.MasterColorMapping();

			colorMapOverride2.Append(masterColorMapping2);

			slideLayout1.Append(commonSlideData2);
			slideLayout1.Append(colorMapOverride2);

			slideLayoutPart1.SlideLayout = slideLayout1;
		}

		// Generates content of slideMasterPart1.
		private void GenerateSlideMasterPart1Content(SlideMasterPart slideMasterPart1)
		{
			SlideMaster slideMaster1 = new SlideMaster();
			slideMaster1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slideMaster1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slideMaster1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData3 = new CommonSlideData();

			Background background1 = new Background();

			BackgroundStyleReference backgroundStyleReference1 = new BackgroundStyleReference(){ Index = (UInt32Value)1001U };
			A.SchemeColor schemeColor10 = new A.SchemeColor(){ Val = A.SchemeColorValues.Background1 };

			backgroundStyleReference1.Append(schemeColor10);

			background1.Append(backgroundStyleReference1);

			ShapeTree shapeTree3 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties3 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties11 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties3 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties11 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties3.Append(nonVisualDrawingProperties11);
			nonVisualGroupShapeProperties3.Append(nonVisualGroupShapeDrawingProperties3);
			nonVisualGroupShapeProperties3.Append(applicationNonVisualDrawingProperties11);

			GroupShapeProperties groupShapeProperties3 = new GroupShapeProperties();

			A.TransformGroup transformGroup3 = new A.TransformGroup();
			A.Offset offset4 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents4 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset3 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents3 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup3.Append(offset4);
			transformGroup3.Append(extents4);
			transformGroup3.Append(childOffset3);
			transformGroup3.Append(childExtents3);

			groupShapeProperties3.Append(transformGroup3);

			Shape shape8 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties8 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties12 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title Placeholder 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties8 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks8 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties8.Append(shapeLocks8);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties12 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape8 = new PlaceholderShape(){ Type = PlaceholderValues.Title };

			applicationNonVisualDrawingProperties12.Append(placeholderShape8);

			nonVisualShapeProperties8.Append(nonVisualDrawingProperties12);
			nonVisualShapeProperties8.Append(nonVisualShapeDrawingProperties8);
			nonVisualShapeProperties8.Append(applicationNonVisualDrawingProperties12);

			ShapeProperties shapeProperties9 = new ShapeProperties();

			A.Transform2D transform2D2 = new A.Transform2D();
			A.Offset offset5 = new A.Offset(){ X = 457200L, Y = 274638L };
			A.Extents extents5 = new A.Extents(){ Cx = 8229600L, Cy = 1143000L };

			transform2D2.Append(offset5);
			transform2D2.Append(extents5);

			A.PresetGeometry presetGeometry2 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
			A.AdjustValueList adjustValueList2 = new A.AdjustValueList();

			presetGeometry2.Append(adjustValueList2);

			shapeProperties9.Append(transform2D2);
			shapeProperties9.Append(presetGeometry2);

			TextBody textBody8 = new TextBody();

			A.BodyProperties bodyProperties8 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };
			A.NormalAutoFit normalAutoFit1 = new A.NormalAutoFit();

			bodyProperties8.Append(normalAutoFit1);
			A.ListStyle listStyle8 = new A.ListStyle();

			A.Paragraph paragraph12 = new A.Paragraph();

			A.Run run8 = new A.Run();
			A.RunProperties runProperties10 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text10 = new A.Text();
			text10.Text = "Click to edit Master title style";

			run8.Append(runProperties10);
			run8.Append(text10);
			A.EndParagraphRunProperties endParagraphRunProperties8 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph12.Append(run8);
			paragraph12.Append(endParagraphRunProperties8);

			textBody8.Append(bodyProperties8);
			textBody8.Append(listStyle8);
			textBody8.Append(paragraph12);

			shape8.Append(nonVisualShapeProperties8);
			shape8.Append(shapeProperties9);
			shape8.Append(textBody8);

			Shape shape9 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties9 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties13 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Text Placeholder 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties9 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks9 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties9.Append(shapeLocks9);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties13 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape9 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Index = (UInt32Value)1U };

			applicationNonVisualDrawingProperties13.Append(placeholderShape9);

			nonVisualShapeProperties9.Append(nonVisualDrawingProperties13);
			nonVisualShapeProperties9.Append(nonVisualShapeDrawingProperties9);
			nonVisualShapeProperties9.Append(applicationNonVisualDrawingProperties13);

			ShapeProperties shapeProperties10 = new ShapeProperties();

			A.Transform2D transform2D3 = new A.Transform2D();
			A.Offset offset6 = new A.Offset(){ X = 457200L, Y = 1600200L };
			A.Extents extents6 = new A.Extents(){ Cx = 8229600L, Cy = 4525963L };

			transform2D3.Append(offset6);
			transform2D3.Append(extents6);

			A.PresetGeometry presetGeometry3 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
			A.AdjustValueList adjustValueList3 = new A.AdjustValueList();

			presetGeometry3.Append(adjustValueList3);

			shapeProperties10.Append(transform2D3);
			shapeProperties10.Append(presetGeometry3);

			TextBody textBody9 = new TextBody();

			A.BodyProperties bodyProperties9 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false };
			A.NormalAutoFit normalAutoFit2 = new A.NormalAutoFit();

			bodyProperties9.Append(normalAutoFit2);
			A.ListStyle listStyle9 = new A.ListStyle();

			A.Paragraph paragraph13 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties6 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run9 = new A.Run();
			A.RunProperties runProperties11 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text11 = new A.Text();
			text11.Text = "Click to edit Master text styles";

			run9.Append(runProperties11);
			run9.Append(text11);

			paragraph13.Append(paragraphProperties6);
			paragraph13.Append(run9);

			A.Paragraph paragraph14 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties7 = new A.ParagraphProperties(){ Level = 1 };

			A.Run run10 = new A.Run();
			A.RunProperties runProperties12 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text12 = new A.Text();
			text12.Text = "Second level";

			run10.Append(runProperties12);
			run10.Append(text12);

			paragraph14.Append(paragraphProperties7);
			paragraph14.Append(run10);

			A.Paragraph paragraph15 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties8 = new A.ParagraphProperties(){ Level = 2 };

			A.Run run11 = new A.Run();
			A.RunProperties runProperties13 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text13 = new A.Text();
			text13.Text = "Third level";

			run11.Append(runProperties13);
			run11.Append(text13);

			paragraph15.Append(paragraphProperties8);
			paragraph15.Append(run11);

			A.Paragraph paragraph16 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties9 = new A.ParagraphProperties(){ Level = 3 };

			A.Run run12 = new A.Run();
			A.RunProperties runProperties14 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text14 = new A.Text();
			text14.Text = "Fourth level";

			run12.Append(runProperties14);
			run12.Append(text14);

			paragraph16.Append(paragraphProperties9);
			paragraph16.Append(run12);

			A.Paragraph paragraph17 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties10 = new A.ParagraphProperties(){ Level = 4 };

			A.Run run13 = new A.Run();
			A.RunProperties runProperties15 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text15 = new A.Text();
			text15.Text = "Fifth level";

			run13.Append(runProperties15);
			run13.Append(text15);
			A.EndParagraphRunProperties endParagraphRunProperties9 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph17.Append(paragraphProperties10);
			paragraph17.Append(run13);
			paragraph17.Append(endParagraphRunProperties9);

			textBody9.Append(bodyProperties9);
			textBody9.Append(listStyle9);
			textBody9.Append(paragraph13);
			textBody9.Append(paragraph14);
			textBody9.Append(paragraph15);
			textBody9.Append(paragraph16);
			textBody9.Append(paragraph17);

			shape9.Append(nonVisualShapeProperties9);
			shape9.Append(shapeProperties10);
			shape9.Append(textBody9);

			Shape shape10 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties10 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties14 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Date Placeholder 3" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties10 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks10 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties10.Append(shapeLocks10);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties14 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape10 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)2U };

			applicationNonVisualDrawingProperties14.Append(placeholderShape10);

			nonVisualShapeProperties10.Append(nonVisualDrawingProperties14);
			nonVisualShapeProperties10.Append(nonVisualShapeDrawingProperties10);
			nonVisualShapeProperties10.Append(applicationNonVisualDrawingProperties14);

			ShapeProperties shapeProperties11 = new ShapeProperties();

			A.Transform2D transform2D4 = new A.Transform2D();
			A.Offset offset7 = new A.Offset(){ X = 457200L, Y = 6356350L };
			A.Extents extents7 = new A.Extents(){ Cx = 2133600L, Cy = 365125L };

			transform2D4.Append(offset7);
			transform2D4.Append(extents7);

			A.PresetGeometry presetGeometry4 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
			A.AdjustValueList adjustValueList4 = new A.AdjustValueList();

			presetGeometry4.Append(adjustValueList4);

			shapeProperties11.Append(transform2D4);
			shapeProperties11.Append(presetGeometry4);

			TextBody textBody10 = new TextBody();
			A.BodyProperties bodyProperties10 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };

			A.ListStyle listStyle10 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties2 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Left };

			A.DefaultRunProperties defaultRunProperties11 = new A.DefaultRunProperties(){ FontSize = 1200 };

			A.SolidFill solidFill10 = new A.SolidFill();

			A.SchemeColor schemeColor11 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint1 = new A.Tint(){ Val = 75000 };

			schemeColor11.Append(tint1);

			solidFill10.Append(schemeColor11);

			defaultRunProperties11.Append(solidFill10);

			level1ParagraphProperties2.Append(defaultRunProperties11);

			listStyle10.Append(level1ParagraphProperties2);

			A.Paragraph paragraph18 = new A.Paragraph();

			A.Field field3 = new A.Field(){ Id = "{6A870D7B-DCE0-46E8-B448-2F0E4E8E28D1}", Type = "datetimeFigureOut" };
			A.RunProperties runProperties16 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text16 = new A.Text();
			text16.Text = "3/28/2013";

			field3.Append(runProperties16);
			field3.Append(text16);
			A.EndParagraphRunProperties endParagraphRunProperties10 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph18.Append(field3);
			paragraph18.Append(endParagraphRunProperties10);

			textBody10.Append(bodyProperties10);
			textBody10.Append(listStyle10);
			textBody10.Append(paragraph18);

			shape10.Append(nonVisualShapeProperties10);
			shape10.Append(shapeProperties11);
			shape10.Append(textBody10);

			Shape shape11 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties11 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties15 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Footer Placeholder 4" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties11 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks11 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties11.Append(shapeLocks11);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties15 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape11 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)3U };

			applicationNonVisualDrawingProperties15.Append(placeholderShape11);

			nonVisualShapeProperties11.Append(nonVisualDrawingProperties15);
			nonVisualShapeProperties11.Append(nonVisualShapeDrawingProperties11);
			nonVisualShapeProperties11.Append(applicationNonVisualDrawingProperties15);

			ShapeProperties shapeProperties12 = new ShapeProperties();

			A.Transform2D transform2D5 = new A.Transform2D();
			A.Offset offset8 = new A.Offset(){ X = 3124200L, Y = 6356350L };
			A.Extents extents8 = new A.Extents(){ Cx = 2895600L, Cy = 365125L };

			transform2D5.Append(offset8);
			transform2D5.Append(extents8);

			A.PresetGeometry presetGeometry5 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
			A.AdjustValueList adjustValueList5 = new A.AdjustValueList();

			presetGeometry5.Append(adjustValueList5);

			shapeProperties12.Append(transform2D5);
			shapeProperties12.Append(presetGeometry5);

			TextBody textBody11 = new TextBody();
			A.BodyProperties bodyProperties11 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };

			A.ListStyle listStyle11 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties3 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Center };

			A.DefaultRunProperties defaultRunProperties12 = new A.DefaultRunProperties(){ FontSize = 1200 };

			A.SolidFill solidFill11 = new A.SolidFill();

			A.SchemeColor schemeColor12 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint2 = new A.Tint(){ Val = 75000 };

			schemeColor12.Append(tint2);

			solidFill11.Append(schemeColor12);

			defaultRunProperties12.Append(solidFill11);

			level1ParagraphProperties3.Append(defaultRunProperties12);

			listStyle11.Append(level1ParagraphProperties3);

			A.Paragraph paragraph19 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties11 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph19.Append(endParagraphRunProperties11);

			textBody11.Append(bodyProperties11);
			textBody11.Append(listStyle11);
			textBody11.Append(paragraph19);

			shape11.Append(nonVisualShapeProperties11);
			shape11.Append(shapeProperties12);
			shape11.Append(textBody11);

			Shape shape12 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties12 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties16 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Slide Number Placeholder 5" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties12 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks12 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties12.Append(shapeLocks12);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties16 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape12 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)4U };

			applicationNonVisualDrawingProperties16.Append(placeholderShape12);

			nonVisualShapeProperties12.Append(nonVisualDrawingProperties16);
			nonVisualShapeProperties12.Append(nonVisualShapeDrawingProperties12);
			nonVisualShapeProperties12.Append(applicationNonVisualDrawingProperties16);

			ShapeProperties shapeProperties13 = new ShapeProperties();

			A.Transform2D transform2D6 = new A.Transform2D();
			A.Offset offset9 = new A.Offset(){ X = 6553200L, Y = 6356350L };
			A.Extents extents9 = new A.Extents(){ Cx = 2133600L, Cy = 365125L };

			transform2D6.Append(offset9);
			transform2D6.Append(extents9);

			A.PresetGeometry presetGeometry6 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
			A.AdjustValueList adjustValueList6 = new A.AdjustValueList();

			presetGeometry6.Append(adjustValueList6);

			shapeProperties13.Append(transform2D6);
			shapeProperties13.Append(presetGeometry6);

			TextBody textBody12 = new TextBody();
			A.BodyProperties bodyProperties12 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };

			A.ListStyle listStyle12 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties4 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Right };

			A.DefaultRunProperties defaultRunProperties13 = new A.DefaultRunProperties(){ FontSize = 1200 };

			A.SolidFill solidFill12 = new A.SolidFill();

			A.SchemeColor schemeColor13 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint3 = new A.Tint(){ Val = 75000 };

			schemeColor13.Append(tint3);

			solidFill12.Append(schemeColor13);

			defaultRunProperties13.Append(solidFill12);

			level1ParagraphProperties4.Append(defaultRunProperties13);

			listStyle12.Append(level1ParagraphProperties4);

			A.Paragraph paragraph20 = new A.Paragraph();

			A.Field field4 = new A.Field(){ Id = "{AFBBC1CF-DB04-427F-9A30-B7E1FB44F3B5}", Type = "slidenum" };
			A.RunProperties runProperties17 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text17 = new A.Text();
			text17.Text = "‹#›";

			field4.Append(runProperties17);
			field4.Append(text17);
			A.EndParagraphRunProperties endParagraphRunProperties12 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph20.Append(field4);
			paragraph20.Append(endParagraphRunProperties12);

			textBody12.Append(bodyProperties12);
			textBody12.Append(listStyle12);
			textBody12.Append(paragraph20);

			shape12.Append(nonVisualShapeProperties12);
			shape12.Append(shapeProperties13);
			shape12.Append(textBody12);

			shapeTree3.Append(nonVisualGroupShapeProperties3);
			shapeTree3.Append(groupShapeProperties3);
			shapeTree3.Append(shape8);
			shapeTree3.Append(shape9);
			shapeTree3.Append(shape10);
			shapeTree3.Append(shape11);
			shapeTree3.Append(shape12);

			commonSlideData3.Append(background1);
			commonSlideData3.Append(shapeTree3);
			ColorMap colorMap1 = new ColorMap(){ Background1 = A.ColorSchemeIndexValues.Light1, Text1 = A.ColorSchemeIndexValues.Dark1, Background2 = A.ColorSchemeIndexValues.Light2, Text2 = A.ColorSchemeIndexValues.Dark2, Accent1 = A.ColorSchemeIndexValues.Accent1, Accent2 = A.ColorSchemeIndexValues.Accent2, Accent3 = A.ColorSchemeIndexValues.Accent3, Accent4 = A.ColorSchemeIndexValues.Accent4, Accent5 = A.ColorSchemeIndexValues.Accent5, Accent6 = A.ColorSchemeIndexValues.Accent6, Hyperlink = A.ColorSchemeIndexValues.Hyperlink, FollowedHyperlink = A.ColorSchemeIndexValues.FollowedHyperlink };

			SlideLayoutIdList slideLayoutIdList1 = new SlideLayoutIdList();
			SlideLayoutId slideLayoutId1 = new SlideLayoutId(){ Id = (UInt32Value)2147483649U, RelationshipId = "rId1" };
			SlideLayoutId slideLayoutId2 = new SlideLayoutId(){ Id = (UInt32Value)2147483650U, RelationshipId = "rId2" };
			SlideLayoutId slideLayoutId3 = new SlideLayoutId(){ Id = (UInt32Value)2147483651U, RelationshipId = "rId3" };
			SlideLayoutId slideLayoutId4 = new SlideLayoutId(){ Id = (UInt32Value)2147483652U, RelationshipId = "rId4" };
			SlideLayoutId slideLayoutId5 = new SlideLayoutId(){ Id = (UInt32Value)2147483653U, RelationshipId = "rId5" };
			SlideLayoutId slideLayoutId6 = new SlideLayoutId(){ Id = (UInt32Value)2147483654U, RelationshipId = "rId6" };
			SlideLayoutId slideLayoutId7 = new SlideLayoutId(){ Id = (UInt32Value)2147483655U, RelationshipId = "rId7" };
			SlideLayoutId slideLayoutId8 = new SlideLayoutId(){ Id = (UInt32Value)2147483656U, RelationshipId = "rId8" };
			SlideLayoutId slideLayoutId9 = new SlideLayoutId(){ Id = (UInt32Value)2147483657U, RelationshipId = "rId9" };
			SlideLayoutId slideLayoutId10 = new SlideLayoutId(){ Id = (UInt32Value)2147483658U, RelationshipId = "rId10" };
			SlideLayoutId slideLayoutId11 = new SlideLayoutId(){ Id = (UInt32Value)2147483659U, RelationshipId = "rId11" };

			slideLayoutIdList1.Append(slideLayoutId1);
			slideLayoutIdList1.Append(slideLayoutId2);
			slideLayoutIdList1.Append(slideLayoutId3);
			slideLayoutIdList1.Append(slideLayoutId4);
			slideLayoutIdList1.Append(slideLayoutId5);
			slideLayoutIdList1.Append(slideLayoutId6);
			slideLayoutIdList1.Append(slideLayoutId7);
			slideLayoutIdList1.Append(slideLayoutId8);
			slideLayoutIdList1.Append(slideLayoutId9);
			slideLayoutIdList1.Append(slideLayoutId10);
			slideLayoutIdList1.Append(slideLayoutId11);

			TextStyles textStyles1 = new TextStyles();

			TitleStyle titleStyle1 = new TitleStyle();

			A.Level1ParagraphProperties level1ParagraphProperties5 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Center, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.SpaceBefore spaceBefore1 = new A.SpaceBefore();
			A.SpacingPercent spacingPercent1 = new A.SpacingPercent(){ Val = 0 };

			spaceBefore1.Append(spacingPercent1);
			A.NoBullet noBullet1 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties14 = new A.DefaultRunProperties(){ FontSize = 4400, Kerning = 1200 };

			A.SolidFill solidFill13 = new A.SolidFill();
			A.SchemeColor schemeColor14 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill13.Append(schemeColor14);
			A.LatinFont latinFont10 = new A.LatinFont(){ Typeface = "+mj-lt" };
			A.EastAsianFont eastAsianFont10 = new A.EastAsianFont(){ Typeface = "+mj-ea" };
			A.ComplexScriptFont complexScriptFont10 = new A.ComplexScriptFont(){ Typeface = "+mj-cs" };

			defaultRunProperties14.Append(solidFill13);
			defaultRunProperties14.Append(latinFont10);
			defaultRunProperties14.Append(eastAsianFont10);
			defaultRunProperties14.Append(complexScriptFont10);

			level1ParagraphProperties5.Append(spaceBefore1);
			level1ParagraphProperties5.Append(noBullet1);
			level1ParagraphProperties5.Append(defaultRunProperties14);

			titleStyle1.Append(level1ParagraphProperties5);

			BodyStyle bodyStyle1 = new BodyStyle();

			A.Level1ParagraphProperties level1ParagraphProperties6 = new A.Level1ParagraphProperties(){ LeftMargin = 342900, Indent = -342900, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.SpaceBefore spaceBefore2 = new A.SpaceBefore();
			A.SpacingPercent spacingPercent2 = new A.SpacingPercent(){ Val = 20000 };

			spaceBefore2.Append(spacingPercent2);
			A.BulletFont bulletFont1 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
			A.CharacterBullet characterBullet1 = new A.CharacterBullet(){ Char = "•" };

			A.DefaultRunProperties defaultRunProperties15 = new A.DefaultRunProperties(){ FontSize = 3200, Kerning = 1200 };

			A.SolidFill solidFill14 = new A.SolidFill();
			A.SchemeColor schemeColor15 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill14.Append(schemeColor15);
			A.LatinFont latinFont11 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont11 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont11 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties15.Append(solidFill14);
			defaultRunProperties15.Append(latinFont11);
			defaultRunProperties15.Append(eastAsianFont11);
			defaultRunProperties15.Append(complexScriptFont11);

			level1ParagraphProperties6.Append(spaceBefore2);
			level1ParagraphProperties6.Append(bulletFont1);
			level1ParagraphProperties6.Append(characterBullet1);
			level1ParagraphProperties6.Append(defaultRunProperties15);

			A.Level2ParagraphProperties level2ParagraphProperties2 = new A.Level2ParagraphProperties(){ LeftMargin = 742950, Indent = -285750, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.SpaceBefore spaceBefore3 = new A.SpaceBefore();
			A.SpacingPercent spacingPercent3 = new A.SpacingPercent(){ Val = 20000 };

			spaceBefore3.Append(spacingPercent3);
			A.BulletFont bulletFont2 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
			A.CharacterBullet characterBullet2 = new A.CharacterBullet(){ Char = "–" };

			A.DefaultRunProperties defaultRunProperties16 = new A.DefaultRunProperties(){ FontSize = 2800, Kerning = 1200 };

			A.SolidFill solidFill15 = new A.SolidFill();
			A.SchemeColor schemeColor16 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill15.Append(schemeColor16);
			A.LatinFont latinFont12 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont12 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont12 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties16.Append(solidFill15);
			defaultRunProperties16.Append(latinFont12);
			defaultRunProperties16.Append(eastAsianFont12);
			defaultRunProperties16.Append(complexScriptFont12);

			level2ParagraphProperties2.Append(spaceBefore3);
			level2ParagraphProperties2.Append(bulletFont2);
			level2ParagraphProperties2.Append(characterBullet2);
			level2ParagraphProperties2.Append(defaultRunProperties16);

			A.Level3ParagraphProperties level3ParagraphProperties2 = new A.Level3ParagraphProperties(){ LeftMargin = 1143000, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.SpaceBefore spaceBefore4 = new A.SpaceBefore();
			A.SpacingPercent spacingPercent4 = new A.SpacingPercent(){ Val = 20000 };

			spaceBefore4.Append(spacingPercent4);
			A.BulletFont bulletFont3 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
			A.CharacterBullet characterBullet3 = new A.CharacterBullet(){ Char = "•" };

			A.DefaultRunProperties defaultRunProperties17 = new A.DefaultRunProperties(){ FontSize = 2400, Kerning = 1200 };

			A.SolidFill solidFill16 = new A.SolidFill();
			A.SchemeColor schemeColor17 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill16.Append(schemeColor17);
			A.LatinFont latinFont13 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont13 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont13 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties17.Append(solidFill16);
			defaultRunProperties17.Append(latinFont13);
			defaultRunProperties17.Append(eastAsianFont13);
			defaultRunProperties17.Append(complexScriptFont13);

			level3ParagraphProperties2.Append(spaceBefore4);
			level3ParagraphProperties2.Append(bulletFont3);
			level3ParagraphProperties2.Append(characterBullet3);
			level3ParagraphProperties2.Append(defaultRunProperties17);

			A.Level4ParagraphProperties level4ParagraphProperties2 = new A.Level4ParagraphProperties(){ LeftMargin = 1600200, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.SpaceBefore spaceBefore5 = new A.SpaceBefore();
			A.SpacingPercent spacingPercent5 = new A.SpacingPercent(){ Val = 20000 };

			spaceBefore5.Append(spacingPercent5);
			A.BulletFont bulletFont4 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
			A.CharacterBullet characterBullet4 = new A.CharacterBullet(){ Char = "–" };

			A.DefaultRunProperties defaultRunProperties18 = new A.DefaultRunProperties(){ FontSize = 2000, Kerning = 1200 };

			A.SolidFill solidFill17 = new A.SolidFill();
			A.SchemeColor schemeColor18 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill17.Append(schemeColor18);
			A.LatinFont latinFont14 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont14 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont14 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties18.Append(solidFill17);
			defaultRunProperties18.Append(latinFont14);
			defaultRunProperties18.Append(eastAsianFont14);
			defaultRunProperties18.Append(complexScriptFont14);

			level4ParagraphProperties2.Append(spaceBefore5);
			level4ParagraphProperties2.Append(bulletFont4);
			level4ParagraphProperties2.Append(characterBullet4);
			level4ParagraphProperties2.Append(defaultRunProperties18);

			A.Level5ParagraphProperties level5ParagraphProperties2 = new A.Level5ParagraphProperties(){ LeftMargin = 2057400, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.SpaceBefore spaceBefore6 = new A.SpaceBefore();
			A.SpacingPercent spacingPercent6 = new A.SpacingPercent(){ Val = 20000 };

			spaceBefore6.Append(spacingPercent6);
			A.BulletFont bulletFont5 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
			A.CharacterBullet characterBullet5 = new A.CharacterBullet(){ Char = "»" };

			A.DefaultRunProperties defaultRunProperties19 = new A.DefaultRunProperties(){ FontSize = 2000, Kerning = 1200 };

			A.SolidFill solidFill18 = new A.SolidFill();
			A.SchemeColor schemeColor19 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill18.Append(schemeColor19);
			A.LatinFont latinFont15 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont15 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont15 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties19.Append(solidFill18);
			defaultRunProperties19.Append(latinFont15);
			defaultRunProperties19.Append(eastAsianFont15);
			defaultRunProperties19.Append(complexScriptFont15);

			level5ParagraphProperties2.Append(spaceBefore6);
			level5ParagraphProperties2.Append(bulletFont5);
			level5ParagraphProperties2.Append(characterBullet5);
			level5ParagraphProperties2.Append(defaultRunProperties19);

			A.Level6ParagraphProperties level6ParagraphProperties2 = new A.Level6ParagraphProperties(){ LeftMargin = 2514600, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.SpaceBefore spaceBefore7 = new A.SpaceBefore();
			A.SpacingPercent spacingPercent7 = new A.SpacingPercent(){ Val = 20000 };

			spaceBefore7.Append(spacingPercent7);
			A.BulletFont bulletFont6 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
			A.CharacterBullet characterBullet6 = new A.CharacterBullet(){ Char = "•" };

			A.DefaultRunProperties defaultRunProperties20 = new A.DefaultRunProperties(){ FontSize = 2000, Kerning = 1200 };

			A.SolidFill solidFill19 = new A.SolidFill();
			A.SchemeColor schemeColor20 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill19.Append(schemeColor20);
			A.LatinFont latinFont16 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont16 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont16 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties20.Append(solidFill19);
			defaultRunProperties20.Append(latinFont16);
			defaultRunProperties20.Append(eastAsianFont16);
			defaultRunProperties20.Append(complexScriptFont16);

			level6ParagraphProperties2.Append(spaceBefore7);
			level6ParagraphProperties2.Append(bulletFont6);
			level6ParagraphProperties2.Append(characterBullet6);
			level6ParagraphProperties2.Append(defaultRunProperties20);

			A.Level7ParagraphProperties level7ParagraphProperties2 = new A.Level7ParagraphProperties(){ LeftMargin = 2971800, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.SpaceBefore spaceBefore8 = new A.SpaceBefore();
			A.SpacingPercent spacingPercent8 = new A.SpacingPercent(){ Val = 20000 };

			spaceBefore8.Append(spacingPercent8);
			A.BulletFont bulletFont7 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
			A.CharacterBullet characterBullet7 = new A.CharacterBullet(){ Char = "•" };

			A.DefaultRunProperties defaultRunProperties21 = new A.DefaultRunProperties(){ FontSize = 2000, Kerning = 1200 };

			A.SolidFill solidFill20 = new A.SolidFill();
			A.SchemeColor schemeColor21 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill20.Append(schemeColor21);
			A.LatinFont latinFont17 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont17 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont17 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties21.Append(solidFill20);
			defaultRunProperties21.Append(latinFont17);
			defaultRunProperties21.Append(eastAsianFont17);
			defaultRunProperties21.Append(complexScriptFont17);

			level7ParagraphProperties2.Append(spaceBefore8);
			level7ParagraphProperties2.Append(bulletFont7);
			level7ParagraphProperties2.Append(characterBullet7);
			level7ParagraphProperties2.Append(defaultRunProperties21);

			A.Level8ParagraphProperties level8ParagraphProperties2 = new A.Level8ParagraphProperties(){ LeftMargin = 3429000, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.SpaceBefore spaceBefore9 = new A.SpaceBefore();
			A.SpacingPercent spacingPercent9 = new A.SpacingPercent(){ Val = 20000 };

			spaceBefore9.Append(spacingPercent9);
			A.BulletFont bulletFont8 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
			A.CharacterBullet characterBullet8 = new A.CharacterBullet(){ Char = "•" };

			A.DefaultRunProperties defaultRunProperties22 = new A.DefaultRunProperties(){ FontSize = 2000, Kerning = 1200 };

			A.SolidFill solidFill21 = new A.SolidFill();
			A.SchemeColor schemeColor22 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill21.Append(schemeColor22);
			A.LatinFont latinFont18 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont18 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont18 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties22.Append(solidFill21);
			defaultRunProperties22.Append(latinFont18);
			defaultRunProperties22.Append(eastAsianFont18);
			defaultRunProperties22.Append(complexScriptFont18);

			level8ParagraphProperties2.Append(spaceBefore9);
			level8ParagraphProperties2.Append(bulletFont8);
			level8ParagraphProperties2.Append(characterBullet8);
			level8ParagraphProperties2.Append(defaultRunProperties22);

			A.Level9ParagraphProperties level9ParagraphProperties2 = new A.Level9ParagraphProperties(){ LeftMargin = 3886200, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.SpaceBefore spaceBefore10 = new A.SpaceBefore();
			A.SpacingPercent spacingPercent10 = new A.SpacingPercent(){ Val = 20000 };

			spaceBefore10.Append(spacingPercent10);
			A.BulletFont bulletFont9 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
			A.CharacterBullet characterBullet9 = new A.CharacterBullet(){ Char = "•" };

			A.DefaultRunProperties defaultRunProperties23 = new A.DefaultRunProperties(){ FontSize = 2000, Kerning = 1200 };

			A.SolidFill solidFill22 = new A.SolidFill();
			A.SchemeColor schemeColor23 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill22.Append(schemeColor23);
			A.LatinFont latinFont19 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont19 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont19 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties23.Append(solidFill22);
			defaultRunProperties23.Append(latinFont19);
			defaultRunProperties23.Append(eastAsianFont19);
			defaultRunProperties23.Append(complexScriptFont19);

			level9ParagraphProperties2.Append(spaceBefore10);
			level9ParagraphProperties2.Append(bulletFont9);
			level9ParagraphProperties2.Append(characterBullet9);
			level9ParagraphProperties2.Append(defaultRunProperties23);

			bodyStyle1.Append(level1ParagraphProperties6);
			bodyStyle1.Append(level2ParagraphProperties2);
			bodyStyle1.Append(level3ParagraphProperties2);
			bodyStyle1.Append(level4ParagraphProperties2);
			bodyStyle1.Append(level5ParagraphProperties2);
			bodyStyle1.Append(level6ParagraphProperties2);
			bodyStyle1.Append(level7ParagraphProperties2);
			bodyStyle1.Append(level8ParagraphProperties2);
			bodyStyle1.Append(level9ParagraphProperties2);

			OtherStyle otherStyle1 = new OtherStyle();

			A.DefaultParagraphProperties defaultParagraphProperties2 = new A.DefaultParagraphProperties();
			A.DefaultRunProperties defaultRunProperties24 = new A.DefaultRunProperties(){ Language = "en-US" };

			defaultParagraphProperties2.Append(defaultRunProperties24);

			A.Level1ParagraphProperties level1ParagraphProperties7 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties25 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill23 = new A.SolidFill();
			A.SchemeColor schemeColor24 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill23.Append(schemeColor24);
			A.LatinFont latinFont20 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont20 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont20 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties25.Append(solidFill23);
			defaultRunProperties25.Append(latinFont20);
			defaultRunProperties25.Append(eastAsianFont20);
			defaultRunProperties25.Append(complexScriptFont20);

			level1ParagraphProperties7.Append(defaultRunProperties25);

			A.Level2ParagraphProperties level2ParagraphProperties3 = new A.Level2ParagraphProperties(){ LeftMargin = 457200, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties26 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill24 = new A.SolidFill();
			A.SchemeColor schemeColor25 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill24.Append(schemeColor25);
			A.LatinFont latinFont21 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont21 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont21 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties26.Append(solidFill24);
			defaultRunProperties26.Append(latinFont21);
			defaultRunProperties26.Append(eastAsianFont21);
			defaultRunProperties26.Append(complexScriptFont21);

			level2ParagraphProperties3.Append(defaultRunProperties26);

			A.Level3ParagraphProperties level3ParagraphProperties3 = new A.Level3ParagraphProperties(){ LeftMargin = 914400, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties27 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill25 = new A.SolidFill();
			A.SchemeColor schemeColor26 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill25.Append(schemeColor26);
			A.LatinFont latinFont22 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont22 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont22 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties27.Append(solidFill25);
			defaultRunProperties27.Append(latinFont22);
			defaultRunProperties27.Append(eastAsianFont22);
			defaultRunProperties27.Append(complexScriptFont22);

			level3ParagraphProperties3.Append(defaultRunProperties27);

			A.Level4ParagraphProperties level4ParagraphProperties3 = new A.Level4ParagraphProperties(){ LeftMargin = 1371600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties28 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill26 = new A.SolidFill();
			A.SchemeColor schemeColor27 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill26.Append(schemeColor27);
			A.LatinFont latinFont23 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont23 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont23 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties28.Append(solidFill26);
			defaultRunProperties28.Append(latinFont23);
			defaultRunProperties28.Append(eastAsianFont23);
			defaultRunProperties28.Append(complexScriptFont23);

			level4ParagraphProperties3.Append(defaultRunProperties28);

			A.Level5ParagraphProperties level5ParagraphProperties3 = new A.Level5ParagraphProperties(){ LeftMargin = 1828800, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties29 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill27 = new A.SolidFill();
			A.SchemeColor schemeColor28 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill27.Append(schemeColor28);
			A.LatinFont latinFont24 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont24 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont24 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties29.Append(solidFill27);
			defaultRunProperties29.Append(latinFont24);
			defaultRunProperties29.Append(eastAsianFont24);
			defaultRunProperties29.Append(complexScriptFont24);

			level5ParagraphProperties3.Append(defaultRunProperties29);

			A.Level6ParagraphProperties level6ParagraphProperties3 = new A.Level6ParagraphProperties(){ LeftMargin = 2286000, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties30 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill28 = new A.SolidFill();
			A.SchemeColor schemeColor29 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill28.Append(schemeColor29);
			A.LatinFont latinFont25 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont25 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont25 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties30.Append(solidFill28);
			defaultRunProperties30.Append(latinFont25);
			defaultRunProperties30.Append(eastAsianFont25);
			defaultRunProperties30.Append(complexScriptFont25);

			level6ParagraphProperties3.Append(defaultRunProperties30);

			A.Level7ParagraphProperties level7ParagraphProperties3 = new A.Level7ParagraphProperties(){ LeftMargin = 2743200, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties31 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill29 = new A.SolidFill();
			A.SchemeColor schemeColor30 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill29.Append(schemeColor30);
			A.LatinFont latinFont26 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont26 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont26 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties31.Append(solidFill29);
			defaultRunProperties31.Append(latinFont26);
			defaultRunProperties31.Append(eastAsianFont26);
			defaultRunProperties31.Append(complexScriptFont26);

			level7ParagraphProperties3.Append(defaultRunProperties31);

			A.Level8ParagraphProperties level8ParagraphProperties3 = new A.Level8ParagraphProperties(){ LeftMargin = 3200400, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties32 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill30 = new A.SolidFill();
			A.SchemeColor schemeColor31 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill30.Append(schemeColor31);
			A.LatinFont latinFont27 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont27 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont27 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties32.Append(solidFill30);
			defaultRunProperties32.Append(latinFont27);
			defaultRunProperties32.Append(eastAsianFont27);
			defaultRunProperties32.Append(complexScriptFont27);

			level8ParagraphProperties3.Append(defaultRunProperties32);

			A.Level9ParagraphProperties level9ParagraphProperties3 = new A.Level9ParagraphProperties(){ LeftMargin = 3657600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

			A.DefaultRunProperties defaultRunProperties33 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

			A.SolidFill solidFill31 = new A.SolidFill();
			A.SchemeColor schemeColor32 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

			solidFill31.Append(schemeColor32);
			A.LatinFont latinFont28 = new A.LatinFont(){ Typeface = "+mn-lt" };
			A.EastAsianFont eastAsianFont28 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
			A.ComplexScriptFont complexScriptFont28 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

			defaultRunProperties33.Append(solidFill31);
			defaultRunProperties33.Append(latinFont28);
			defaultRunProperties33.Append(eastAsianFont28);
			defaultRunProperties33.Append(complexScriptFont28);

			level9ParagraphProperties3.Append(defaultRunProperties33);

			otherStyle1.Append(defaultParagraphProperties2);
			otherStyle1.Append(level1ParagraphProperties7);
			otherStyle1.Append(level2ParagraphProperties3);
			otherStyle1.Append(level3ParagraphProperties3);
			otherStyle1.Append(level4ParagraphProperties3);
			otherStyle1.Append(level5ParagraphProperties3);
			otherStyle1.Append(level6ParagraphProperties3);
			otherStyle1.Append(level7ParagraphProperties3);
			otherStyle1.Append(level8ParagraphProperties3);
			otherStyle1.Append(level9ParagraphProperties3);

			textStyles1.Append(titleStyle1);
			textStyles1.Append(bodyStyle1);
			textStyles1.Append(otherStyle1);

			slideMaster1.Append(commonSlideData3);
			slideMaster1.Append(colorMap1);
			slideMaster1.Append(slideLayoutIdList1);
			slideMaster1.Append(textStyles1);

			slideMasterPart1.SlideMaster = slideMaster1;
		}

		// Generates content of slideLayoutPart2.
		private void GenerateSlideLayoutPart2Content(SlideLayoutPart slideLayoutPart2)
		{
			SlideLayout slideLayout2 = new SlideLayout(){ Type = SlideLayoutValues.ObjectText, Preserve = true };
			slideLayout2.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slideLayout2.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slideLayout2.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData4 = new CommonSlideData(){ Name = "Content with Caption" };

			ShapeTree shapeTree4 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties4 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties17 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties4 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties17 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties4.Append(nonVisualDrawingProperties17);
			nonVisualGroupShapeProperties4.Append(nonVisualGroupShapeDrawingProperties4);
			nonVisualGroupShapeProperties4.Append(applicationNonVisualDrawingProperties17);

			GroupShapeProperties groupShapeProperties4 = new GroupShapeProperties();

			A.TransformGroup transformGroup4 = new A.TransformGroup();
			A.Offset offset10 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents10 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset4 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents4 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup4.Append(offset10);
			transformGroup4.Append(extents10);
			transformGroup4.Append(childOffset4);
			transformGroup4.Append(childExtents4);

			groupShapeProperties4.Append(transformGroup4);

			Shape shape13 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties13 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties18 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties13 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks13 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties13.Append(shapeLocks13);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties18 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape13 = new PlaceholderShape(){ Type = PlaceholderValues.Title };

			applicationNonVisualDrawingProperties18.Append(placeholderShape13);

			nonVisualShapeProperties13.Append(nonVisualDrawingProperties18);
			nonVisualShapeProperties13.Append(nonVisualShapeDrawingProperties13);
			nonVisualShapeProperties13.Append(applicationNonVisualDrawingProperties18);

			ShapeProperties shapeProperties14 = new ShapeProperties();

			A.Transform2D transform2D7 = new A.Transform2D();
			A.Offset offset11 = new A.Offset(){ X = 457200L, Y = 273050L };
			A.Extents extents11 = new A.Extents(){ Cx = 3008313L, Cy = 1162050L };

			transform2D7.Append(offset11);
			transform2D7.Append(extents11);

			shapeProperties14.Append(transform2D7);

			TextBody textBody13 = new TextBody();
			A.BodyProperties bodyProperties13 = new A.BodyProperties(){ Anchor = A.TextAnchoringTypeValues.Bottom };

			A.ListStyle listStyle13 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties8 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Left };
			A.DefaultRunProperties defaultRunProperties34 = new A.DefaultRunProperties(){ FontSize = 2000, Bold = true };

			level1ParagraphProperties8.Append(defaultRunProperties34);

			listStyle13.Append(level1ParagraphProperties8);

			A.Paragraph paragraph21 = new A.Paragraph();

			A.Run run14 = new A.Run();
			A.RunProperties runProperties18 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text18 = new A.Text();
			text18.Text = "Click to edit Master title style";

			run14.Append(runProperties18);
			run14.Append(text18);
			A.EndParagraphRunProperties endParagraphRunProperties13 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph21.Append(run14);
			paragraph21.Append(endParagraphRunProperties13);

			textBody13.Append(bodyProperties13);
			textBody13.Append(listStyle13);
			textBody13.Append(paragraph21);

			shape13.Append(nonVisualShapeProperties13);
			shape13.Append(shapeProperties14);
			shape13.Append(textBody13);

			Shape shape14 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties14 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties19 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Content Placeholder 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties14 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks14 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties14.Append(shapeLocks14);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties19 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape14 = new PlaceholderShape(){ Index = (UInt32Value)1U };

			applicationNonVisualDrawingProperties19.Append(placeholderShape14);

			nonVisualShapeProperties14.Append(nonVisualDrawingProperties19);
			nonVisualShapeProperties14.Append(nonVisualShapeDrawingProperties14);
			nonVisualShapeProperties14.Append(applicationNonVisualDrawingProperties19);

			ShapeProperties shapeProperties15 = new ShapeProperties();

			A.Transform2D transform2D8 = new A.Transform2D();
			A.Offset offset12 = new A.Offset(){ X = 3575050L, Y = 273050L };
			A.Extents extents12 = new A.Extents(){ Cx = 5111750L, Cy = 5853113L };

			transform2D8.Append(offset12);
			transform2D8.Append(extents12);

			shapeProperties15.Append(transform2D8);

			TextBody textBody14 = new TextBody();
			A.BodyProperties bodyProperties14 = new A.BodyProperties();

			A.ListStyle listStyle14 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties9 = new A.Level1ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties35 = new A.DefaultRunProperties(){ FontSize = 3200 };

			level1ParagraphProperties9.Append(defaultRunProperties35);

			A.Level2ParagraphProperties level2ParagraphProperties4 = new A.Level2ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties36 = new A.DefaultRunProperties(){ FontSize = 2800 };

			level2ParagraphProperties4.Append(defaultRunProperties36);

			A.Level3ParagraphProperties level3ParagraphProperties4 = new A.Level3ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties37 = new A.DefaultRunProperties(){ FontSize = 2400 };

			level3ParagraphProperties4.Append(defaultRunProperties37);

			A.Level4ParagraphProperties level4ParagraphProperties4 = new A.Level4ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties38 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level4ParagraphProperties4.Append(defaultRunProperties38);

			A.Level5ParagraphProperties level5ParagraphProperties4 = new A.Level5ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties39 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level5ParagraphProperties4.Append(defaultRunProperties39);

			A.Level6ParagraphProperties level6ParagraphProperties4 = new A.Level6ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties40 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level6ParagraphProperties4.Append(defaultRunProperties40);

			A.Level7ParagraphProperties level7ParagraphProperties4 = new A.Level7ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties41 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level7ParagraphProperties4.Append(defaultRunProperties41);

			A.Level8ParagraphProperties level8ParagraphProperties4 = new A.Level8ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties42 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level8ParagraphProperties4.Append(defaultRunProperties42);

			A.Level9ParagraphProperties level9ParagraphProperties4 = new A.Level9ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties43 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level9ParagraphProperties4.Append(defaultRunProperties43);

			listStyle14.Append(level1ParagraphProperties9);
			listStyle14.Append(level2ParagraphProperties4);
			listStyle14.Append(level3ParagraphProperties4);
			listStyle14.Append(level4ParagraphProperties4);
			listStyle14.Append(level5ParagraphProperties4);
			listStyle14.Append(level6ParagraphProperties4);
			listStyle14.Append(level7ParagraphProperties4);
			listStyle14.Append(level8ParagraphProperties4);
			listStyle14.Append(level9ParagraphProperties4);

			A.Paragraph paragraph22 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties11 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run15 = new A.Run();
			A.RunProperties runProperties19 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text19 = new A.Text();
			text19.Text = "Click to edit Master text styles";

			run15.Append(runProperties19);
			run15.Append(text19);

			paragraph22.Append(paragraphProperties11);
			paragraph22.Append(run15);

			A.Paragraph paragraph23 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties12 = new A.ParagraphProperties(){ Level = 1 };

			A.Run run16 = new A.Run();
			A.RunProperties runProperties20 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text20 = new A.Text();
			text20.Text = "Second level";

			run16.Append(runProperties20);
			run16.Append(text20);

			paragraph23.Append(paragraphProperties12);
			paragraph23.Append(run16);

			A.Paragraph paragraph24 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties13 = new A.ParagraphProperties(){ Level = 2 };

			A.Run run17 = new A.Run();
			A.RunProperties runProperties21 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text21 = new A.Text();
			text21.Text = "Third level";

			run17.Append(runProperties21);
			run17.Append(text21);

			paragraph24.Append(paragraphProperties13);
			paragraph24.Append(run17);

			A.Paragraph paragraph25 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties14 = new A.ParagraphProperties(){ Level = 3 };

			A.Run run18 = new A.Run();
			A.RunProperties runProperties22 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text22 = new A.Text();
			text22.Text = "Fourth level";

			run18.Append(runProperties22);
			run18.Append(text22);

			paragraph25.Append(paragraphProperties14);
			paragraph25.Append(run18);

			A.Paragraph paragraph26 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties15 = new A.ParagraphProperties(){ Level = 4 };

			A.Run run19 = new A.Run();
			A.RunProperties runProperties23 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text23 = new A.Text();
			text23.Text = "Fifth level";

			run19.Append(runProperties23);
			run19.Append(text23);
			A.EndParagraphRunProperties endParagraphRunProperties14 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph26.Append(paragraphProperties15);
			paragraph26.Append(run19);
			paragraph26.Append(endParagraphRunProperties14);

			textBody14.Append(bodyProperties14);
			textBody14.Append(listStyle14);
			textBody14.Append(paragraph22);
			textBody14.Append(paragraph23);
			textBody14.Append(paragraph24);
			textBody14.Append(paragraph25);
			textBody14.Append(paragraph26);

			shape14.Append(nonVisualShapeProperties14);
			shape14.Append(shapeProperties15);
			shape14.Append(textBody14);

			Shape shape15 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties15 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties20 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Text Placeholder 3" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties15 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks15 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties15.Append(shapeLocks15);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties20 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape15 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)2U };

			applicationNonVisualDrawingProperties20.Append(placeholderShape15);

			nonVisualShapeProperties15.Append(nonVisualDrawingProperties20);
			nonVisualShapeProperties15.Append(nonVisualShapeDrawingProperties15);
			nonVisualShapeProperties15.Append(applicationNonVisualDrawingProperties20);

			ShapeProperties shapeProperties16 = new ShapeProperties();

			A.Transform2D transform2D9 = new A.Transform2D();
			A.Offset offset13 = new A.Offset(){ X = 457200L, Y = 1435100L };
			A.Extents extents13 = new A.Extents(){ Cx = 3008313L, Cy = 4691063L };

			transform2D9.Append(offset13);
			transform2D9.Append(extents13);

			shapeProperties16.Append(transform2D9);

			TextBody textBody15 = new TextBody();
			A.BodyProperties bodyProperties15 = new A.BodyProperties();

			A.ListStyle listStyle15 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties10 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Indent = 0 };
			A.NoBullet noBullet2 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties44 = new A.DefaultRunProperties(){ FontSize = 1400 };

			level1ParagraphProperties10.Append(noBullet2);
			level1ParagraphProperties10.Append(defaultRunProperties44);

			A.Level2ParagraphProperties level2ParagraphProperties5 = new A.Level2ParagraphProperties(){ LeftMargin = 457200, Indent = 0 };
			A.NoBullet noBullet3 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties45 = new A.DefaultRunProperties(){ FontSize = 1200 };

			level2ParagraphProperties5.Append(noBullet3);
			level2ParagraphProperties5.Append(defaultRunProperties45);

			A.Level3ParagraphProperties level3ParagraphProperties5 = new A.Level3ParagraphProperties(){ LeftMargin = 914400, Indent = 0 };
			A.NoBullet noBullet4 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties46 = new A.DefaultRunProperties(){ FontSize = 1000 };

			level3ParagraphProperties5.Append(noBullet4);
			level3ParagraphProperties5.Append(defaultRunProperties46);

			A.Level4ParagraphProperties level4ParagraphProperties5 = new A.Level4ParagraphProperties(){ LeftMargin = 1371600, Indent = 0 };
			A.NoBullet noBullet5 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties47 = new A.DefaultRunProperties(){ FontSize = 900 };

			level4ParagraphProperties5.Append(noBullet5);
			level4ParagraphProperties5.Append(defaultRunProperties47);

			A.Level5ParagraphProperties level5ParagraphProperties5 = new A.Level5ParagraphProperties(){ LeftMargin = 1828800, Indent = 0 };
			A.NoBullet noBullet6 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties48 = new A.DefaultRunProperties(){ FontSize = 900 };

			level5ParagraphProperties5.Append(noBullet6);
			level5ParagraphProperties5.Append(defaultRunProperties48);

			A.Level6ParagraphProperties level6ParagraphProperties5 = new A.Level6ParagraphProperties(){ LeftMargin = 2286000, Indent = 0 };
			A.NoBullet noBullet7 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties49 = new A.DefaultRunProperties(){ FontSize = 900 };

			level6ParagraphProperties5.Append(noBullet7);
			level6ParagraphProperties5.Append(defaultRunProperties49);

			A.Level7ParagraphProperties level7ParagraphProperties5 = new A.Level7ParagraphProperties(){ LeftMargin = 2743200, Indent = 0 };
			A.NoBullet noBullet8 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties50 = new A.DefaultRunProperties(){ FontSize = 900 };

			level7ParagraphProperties5.Append(noBullet8);
			level7ParagraphProperties5.Append(defaultRunProperties50);

			A.Level8ParagraphProperties level8ParagraphProperties5 = new A.Level8ParagraphProperties(){ LeftMargin = 3200400, Indent = 0 };
			A.NoBullet noBullet9 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties51 = new A.DefaultRunProperties(){ FontSize = 900 };

			level8ParagraphProperties5.Append(noBullet9);
			level8ParagraphProperties5.Append(defaultRunProperties51);

			A.Level9ParagraphProperties level9ParagraphProperties5 = new A.Level9ParagraphProperties(){ LeftMargin = 3657600, Indent = 0 };
			A.NoBullet noBullet10 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties52 = new A.DefaultRunProperties(){ FontSize = 900 };

			level9ParagraphProperties5.Append(noBullet10);
			level9ParagraphProperties5.Append(defaultRunProperties52);

			listStyle15.Append(level1ParagraphProperties10);
			listStyle15.Append(level2ParagraphProperties5);
			listStyle15.Append(level3ParagraphProperties5);
			listStyle15.Append(level4ParagraphProperties5);
			listStyle15.Append(level5ParagraphProperties5);
			listStyle15.Append(level6ParagraphProperties5);
			listStyle15.Append(level7ParagraphProperties5);
			listStyle15.Append(level8ParagraphProperties5);
			listStyle15.Append(level9ParagraphProperties5);

			A.Paragraph paragraph27 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties16 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run20 = new A.Run();
			A.RunProperties runProperties24 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text24 = new A.Text();
			text24.Text = "Click to edit Master text styles";

			run20.Append(runProperties24);
			run20.Append(text24);

			paragraph27.Append(paragraphProperties16);
			paragraph27.Append(run20);

			textBody15.Append(bodyProperties15);
			textBody15.Append(listStyle15);
			textBody15.Append(paragraph27);

			shape15.Append(nonVisualShapeProperties15);
			shape15.Append(shapeProperties16);
			shape15.Append(textBody15);

			Shape shape16 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties16 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties21 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Date Placeholder 4" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties16 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks16 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties16.Append(shapeLocks16);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties21 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape16 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)10U };

			applicationNonVisualDrawingProperties21.Append(placeholderShape16);

			nonVisualShapeProperties16.Append(nonVisualDrawingProperties21);
			nonVisualShapeProperties16.Append(nonVisualShapeDrawingProperties16);
			nonVisualShapeProperties16.Append(applicationNonVisualDrawingProperties21);
			ShapeProperties shapeProperties17 = new ShapeProperties();

			TextBody textBody16 = new TextBody();
			A.BodyProperties bodyProperties16 = new A.BodyProperties();
			A.ListStyle listStyle16 = new A.ListStyle();

			A.Paragraph paragraph28 = new A.Paragraph();

			A.Field field5 = new A.Field(){ Id = "{6A870D7B-DCE0-46E8-B448-2F0E4E8E28D1}", Type = "datetimeFigureOut" };
			A.RunProperties runProperties25 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text25 = new A.Text();
			text25.Text = "3/28/2013";

			field5.Append(runProperties25);
			field5.Append(text25);
			A.EndParagraphRunProperties endParagraphRunProperties15 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph28.Append(field5);
			paragraph28.Append(endParagraphRunProperties15);

			textBody16.Append(bodyProperties16);
			textBody16.Append(listStyle16);
			textBody16.Append(paragraph28);

			shape16.Append(nonVisualShapeProperties16);
			shape16.Append(shapeProperties17);
			shape16.Append(textBody16);

			Shape shape17 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties17 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties22 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Footer Placeholder 5" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties17 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks17 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties17.Append(shapeLocks17);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties22 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape17 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

			applicationNonVisualDrawingProperties22.Append(placeholderShape17);

			nonVisualShapeProperties17.Append(nonVisualDrawingProperties22);
			nonVisualShapeProperties17.Append(nonVisualShapeDrawingProperties17);
			nonVisualShapeProperties17.Append(applicationNonVisualDrawingProperties22);
			ShapeProperties shapeProperties18 = new ShapeProperties();

			TextBody textBody17 = new TextBody();
			A.BodyProperties bodyProperties17 = new A.BodyProperties();
			A.ListStyle listStyle17 = new A.ListStyle();

			A.Paragraph paragraph29 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties16 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph29.Append(endParagraphRunProperties16);

			textBody17.Append(bodyProperties17);
			textBody17.Append(listStyle17);
			textBody17.Append(paragraph29);

			shape17.Append(nonVisualShapeProperties17);
			shape17.Append(shapeProperties18);
			shape17.Append(textBody17);

			Shape shape18 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties18 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties23 = new NonVisualDrawingProperties(){ Id = (UInt32Value)7U, Name = "Slide Number Placeholder 6" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties18 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks18 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties18.Append(shapeLocks18);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties23 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape18 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

			applicationNonVisualDrawingProperties23.Append(placeholderShape18);

			nonVisualShapeProperties18.Append(nonVisualDrawingProperties23);
			nonVisualShapeProperties18.Append(nonVisualShapeDrawingProperties18);
			nonVisualShapeProperties18.Append(applicationNonVisualDrawingProperties23);
			ShapeProperties shapeProperties19 = new ShapeProperties();

			TextBody textBody18 = new TextBody();
			A.BodyProperties bodyProperties18 = new A.BodyProperties();
			A.ListStyle listStyle18 = new A.ListStyle();

			A.Paragraph paragraph30 = new A.Paragraph();

			A.Field field6 = new A.Field(){ Id = "{AFBBC1CF-DB04-427F-9A30-B7E1FB44F3B5}", Type = "slidenum" };
			A.RunProperties runProperties26 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text26 = new A.Text();
			text26.Text = "‹#›";

			field6.Append(runProperties26);
			field6.Append(text26);
			A.EndParagraphRunProperties endParagraphRunProperties17 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph30.Append(field6);
			paragraph30.Append(endParagraphRunProperties17);

			textBody18.Append(bodyProperties18);
			textBody18.Append(listStyle18);
			textBody18.Append(paragraph30);

			shape18.Append(nonVisualShapeProperties18);
			shape18.Append(shapeProperties19);
			shape18.Append(textBody18);

			shapeTree4.Append(nonVisualGroupShapeProperties4);
			shapeTree4.Append(groupShapeProperties4);
			shapeTree4.Append(shape13);
			shapeTree4.Append(shape14);
			shapeTree4.Append(shape15);
			shapeTree4.Append(shape16);
			shapeTree4.Append(shape17);
			shapeTree4.Append(shape18);

			commonSlideData4.Append(shapeTree4);

			ColorMapOverride colorMapOverride3 = new ColorMapOverride();
			A.MasterColorMapping masterColorMapping3 = new A.MasterColorMapping();

			colorMapOverride3.Append(masterColorMapping3);

			slideLayout2.Append(commonSlideData4);
			slideLayout2.Append(colorMapOverride3);

			slideLayoutPart2.SlideLayout = slideLayout2;
		}

		// Generates content of slideLayoutPart3.
		private void GenerateSlideLayoutPart3Content(SlideLayoutPart slideLayoutPart3)
		{
			SlideLayout slideLayout3 = new SlideLayout(){ Type = SlideLayoutValues.SectionHeader, Preserve = true };
			slideLayout3.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slideLayout3.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slideLayout3.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData5 = new CommonSlideData(){ Name = "Section Header" };

			ShapeTree shapeTree5 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties5 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties24 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties5 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties24 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties5.Append(nonVisualDrawingProperties24);
			nonVisualGroupShapeProperties5.Append(nonVisualGroupShapeDrawingProperties5);
			nonVisualGroupShapeProperties5.Append(applicationNonVisualDrawingProperties24);

			GroupShapeProperties groupShapeProperties5 = new GroupShapeProperties();

			A.TransformGroup transformGroup5 = new A.TransformGroup();
			A.Offset offset14 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents14 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset5 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents5 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup5.Append(offset14);
			transformGroup5.Append(extents14);
			transformGroup5.Append(childOffset5);
			transformGroup5.Append(childExtents5);

			groupShapeProperties5.Append(transformGroup5);

			Shape shape19 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties19 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties25 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties19 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks19 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties19.Append(shapeLocks19);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties25 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape19 = new PlaceholderShape(){ Type = PlaceholderValues.Title };

			applicationNonVisualDrawingProperties25.Append(placeholderShape19);

			nonVisualShapeProperties19.Append(nonVisualDrawingProperties25);
			nonVisualShapeProperties19.Append(nonVisualShapeDrawingProperties19);
			nonVisualShapeProperties19.Append(applicationNonVisualDrawingProperties25);

			ShapeProperties shapeProperties20 = new ShapeProperties();

			A.Transform2D transform2D10 = new A.Transform2D();
			A.Offset offset15 = new A.Offset(){ X = 722313L, Y = 4406900L };
			A.Extents extents15 = new A.Extents(){ Cx = 7772400L, Cy = 1362075L };

			transform2D10.Append(offset15);
			transform2D10.Append(extents15);

			shapeProperties20.Append(transform2D10);

			TextBody textBody19 = new TextBody();
			A.BodyProperties bodyProperties19 = new A.BodyProperties(){ Anchor = A.TextAnchoringTypeValues.Top };

			A.ListStyle listStyle19 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties11 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Left };
			A.DefaultRunProperties defaultRunProperties53 = new A.DefaultRunProperties(){ FontSize = 4000, Bold = true, Capital = A.TextCapsValues.All };

			level1ParagraphProperties11.Append(defaultRunProperties53);

			listStyle19.Append(level1ParagraphProperties11);

			A.Paragraph paragraph31 = new A.Paragraph();

			A.Run run21 = new A.Run();
			A.RunProperties runProperties27 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text27 = new A.Text();
			text27.Text = "Click to edit Master title style";

			run21.Append(runProperties27);
			run21.Append(text27);
			A.EndParagraphRunProperties endParagraphRunProperties18 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph31.Append(run21);
			paragraph31.Append(endParagraphRunProperties18);

			textBody19.Append(bodyProperties19);
			textBody19.Append(listStyle19);
			textBody19.Append(paragraph31);

			shape19.Append(nonVisualShapeProperties19);
			shape19.Append(shapeProperties20);
			shape19.Append(textBody19);

			Shape shape20 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties20 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties26 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Text Placeholder 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties20 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks20 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties20.Append(shapeLocks20);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties26 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape20 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Index = (UInt32Value)1U };

			applicationNonVisualDrawingProperties26.Append(placeholderShape20);

			nonVisualShapeProperties20.Append(nonVisualDrawingProperties26);
			nonVisualShapeProperties20.Append(nonVisualShapeDrawingProperties20);
			nonVisualShapeProperties20.Append(applicationNonVisualDrawingProperties26);

			ShapeProperties shapeProperties21 = new ShapeProperties();

			A.Transform2D transform2D11 = new A.Transform2D();
			A.Offset offset16 = new A.Offset(){ X = 722313L, Y = 2906713L };
			A.Extents extents16 = new A.Extents(){ Cx = 7772400L, Cy = 1500187L };

			transform2D11.Append(offset16);
			transform2D11.Append(extents16);

			shapeProperties21.Append(transform2D11);

			TextBody textBody20 = new TextBody();
			A.BodyProperties bodyProperties20 = new A.BodyProperties(){ Anchor = A.TextAnchoringTypeValues.Bottom };

			A.ListStyle listStyle20 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties12 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Indent = 0 };
			A.NoBullet noBullet11 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties54 = new A.DefaultRunProperties(){ FontSize = 2000 };

			A.SolidFill solidFill32 = new A.SolidFill();

			A.SchemeColor schemeColor33 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint4 = new A.Tint(){ Val = 75000 };

			schemeColor33.Append(tint4);

			solidFill32.Append(schemeColor33);

			defaultRunProperties54.Append(solidFill32);

			level1ParagraphProperties12.Append(noBullet11);
			level1ParagraphProperties12.Append(defaultRunProperties54);

			A.Level2ParagraphProperties level2ParagraphProperties6 = new A.Level2ParagraphProperties(){ LeftMargin = 457200, Indent = 0 };
			A.NoBullet noBullet12 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties55 = new A.DefaultRunProperties(){ FontSize = 1800 };

			A.SolidFill solidFill33 = new A.SolidFill();

			A.SchemeColor schemeColor34 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint5 = new A.Tint(){ Val = 75000 };

			schemeColor34.Append(tint5);

			solidFill33.Append(schemeColor34);

			defaultRunProperties55.Append(solidFill33);

			level2ParagraphProperties6.Append(noBullet12);
			level2ParagraphProperties6.Append(defaultRunProperties55);

			A.Level3ParagraphProperties level3ParagraphProperties6 = new A.Level3ParagraphProperties(){ LeftMargin = 914400, Indent = 0 };
			A.NoBullet noBullet13 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties56 = new A.DefaultRunProperties(){ FontSize = 1600 };

			A.SolidFill solidFill34 = new A.SolidFill();

			A.SchemeColor schemeColor35 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint6 = new A.Tint(){ Val = 75000 };

			schemeColor35.Append(tint6);

			solidFill34.Append(schemeColor35);

			defaultRunProperties56.Append(solidFill34);

			level3ParagraphProperties6.Append(noBullet13);
			level3ParagraphProperties6.Append(defaultRunProperties56);

			A.Level4ParagraphProperties level4ParagraphProperties6 = new A.Level4ParagraphProperties(){ LeftMargin = 1371600, Indent = 0 };
			A.NoBullet noBullet14 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties57 = new A.DefaultRunProperties(){ FontSize = 1400 };

			A.SolidFill solidFill35 = new A.SolidFill();

			A.SchemeColor schemeColor36 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint7 = new A.Tint(){ Val = 75000 };

			schemeColor36.Append(tint7);

			solidFill35.Append(schemeColor36);

			defaultRunProperties57.Append(solidFill35);

			level4ParagraphProperties6.Append(noBullet14);
			level4ParagraphProperties6.Append(defaultRunProperties57);

			A.Level5ParagraphProperties level5ParagraphProperties6 = new A.Level5ParagraphProperties(){ LeftMargin = 1828800, Indent = 0 };
			A.NoBullet noBullet15 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties58 = new A.DefaultRunProperties(){ FontSize = 1400 };

			A.SolidFill solidFill36 = new A.SolidFill();

			A.SchemeColor schemeColor37 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint8 = new A.Tint(){ Val = 75000 };

			schemeColor37.Append(tint8);

			solidFill36.Append(schemeColor37);

			defaultRunProperties58.Append(solidFill36);

			level5ParagraphProperties6.Append(noBullet15);
			level5ParagraphProperties6.Append(defaultRunProperties58);

			A.Level6ParagraphProperties level6ParagraphProperties6 = new A.Level6ParagraphProperties(){ LeftMargin = 2286000, Indent = 0 };
			A.NoBullet noBullet16 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties59 = new A.DefaultRunProperties(){ FontSize = 1400 };

			A.SolidFill solidFill37 = new A.SolidFill();

			A.SchemeColor schemeColor38 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint9 = new A.Tint(){ Val = 75000 };

			schemeColor38.Append(tint9);

			solidFill37.Append(schemeColor38);

			defaultRunProperties59.Append(solidFill37);

			level6ParagraphProperties6.Append(noBullet16);
			level6ParagraphProperties6.Append(defaultRunProperties59);

			A.Level7ParagraphProperties level7ParagraphProperties6 = new A.Level7ParagraphProperties(){ LeftMargin = 2743200, Indent = 0 };
			A.NoBullet noBullet17 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties60 = new A.DefaultRunProperties(){ FontSize = 1400 };

			A.SolidFill solidFill38 = new A.SolidFill();

			A.SchemeColor schemeColor39 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint10 = new A.Tint(){ Val = 75000 };

			schemeColor39.Append(tint10);

			solidFill38.Append(schemeColor39);

			defaultRunProperties60.Append(solidFill38);

			level7ParagraphProperties6.Append(noBullet17);
			level7ParagraphProperties6.Append(defaultRunProperties60);

			A.Level8ParagraphProperties level8ParagraphProperties6 = new A.Level8ParagraphProperties(){ LeftMargin = 3200400, Indent = 0 };
			A.NoBullet noBullet18 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties61 = new A.DefaultRunProperties(){ FontSize = 1400 };

			A.SolidFill solidFill39 = new A.SolidFill();

			A.SchemeColor schemeColor40 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint11 = new A.Tint(){ Val = 75000 };

			schemeColor40.Append(tint11);

			solidFill39.Append(schemeColor40);

			defaultRunProperties61.Append(solidFill39);

			level8ParagraphProperties6.Append(noBullet18);
			level8ParagraphProperties6.Append(defaultRunProperties61);

			A.Level9ParagraphProperties level9ParagraphProperties6 = new A.Level9ParagraphProperties(){ LeftMargin = 3657600, Indent = 0 };
			A.NoBullet noBullet19 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties62 = new A.DefaultRunProperties(){ FontSize = 1400 };

			A.SolidFill solidFill40 = new A.SolidFill();

			A.SchemeColor schemeColor41 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint12 = new A.Tint(){ Val = 75000 };

			schemeColor41.Append(tint12);

			solidFill40.Append(schemeColor41);

			defaultRunProperties62.Append(solidFill40);

			level9ParagraphProperties6.Append(noBullet19);
			level9ParagraphProperties6.Append(defaultRunProperties62);

			listStyle20.Append(level1ParagraphProperties12);
			listStyle20.Append(level2ParagraphProperties6);
			listStyle20.Append(level3ParagraphProperties6);
			listStyle20.Append(level4ParagraphProperties6);
			listStyle20.Append(level5ParagraphProperties6);
			listStyle20.Append(level6ParagraphProperties6);
			listStyle20.Append(level7ParagraphProperties6);
			listStyle20.Append(level8ParagraphProperties6);
			listStyle20.Append(level9ParagraphProperties6);

			A.Paragraph paragraph32 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties17 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run22 = new A.Run();
			A.RunProperties runProperties28 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text28 = new A.Text();
			text28.Text = "Click to edit Master text styles";

			run22.Append(runProperties28);
			run22.Append(text28);

			paragraph32.Append(paragraphProperties17);
			paragraph32.Append(run22);

			textBody20.Append(bodyProperties20);
			textBody20.Append(listStyle20);
			textBody20.Append(paragraph32);

			shape20.Append(nonVisualShapeProperties20);
			shape20.Append(shapeProperties21);
			shape20.Append(textBody20);

			Shape shape21 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties21 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties27 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Date Placeholder 3" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties21 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks21 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties21.Append(shapeLocks21);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties27 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape21 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)10U };

			applicationNonVisualDrawingProperties27.Append(placeholderShape21);

			nonVisualShapeProperties21.Append(nonVisualDrawingProperties27);
			nonVisualShapeProperties21.Append(nonVisualShapeDrawingProperties21);
			nonVisualShapeProperties21.Append(applicationNonVisualDrawingProperties27);
			ShapeProperties shapeProperties22 = new ShapeProperties();

			TextBody textBody21 = new TextBody();
			A.BodyProperties bodyProperties21 = new A.BodyProperties();
			A.ListStyle listStyle21 = new A.ListStyle();

			A.Paragraph paragraph33 = new A.Paragraph();

			A.Field field7 = new A.Field(){ Id = "{6A870D7B-DCE0-46E8-B448-2F0E4E8E28D1}", Type = "datetimeFigureOut" };
			A.RunProperties runProperties29 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text29 = new A.Text();
			text29.Text = "3/28/2013";

			field7.Append(runProperties29);
			field7.Append(text29);
			A.EndParagraphRunProperties endParagraphRunProperties19 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph33.Append(field7);
			paragraph33.Append(endParagraphRunProperties19);

			textBody21.Append(bodyProperties21);
			textBody21.Append(listStyle21);
			textBody21.Append(paragraph33);

			shape21.Append(nonVisualShapeProperties21);
			shape21.Append(shapeProperties22);
			shape21.Append(textBody21);

			Shape shape22 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties22 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties28 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Footer Placeholder 4" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties22 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks22 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties22.Append(shapeLocks22);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties28 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape22 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

			applicationNonVisualDrawingProperties28.Append(placeholderShape22);

			nonVisualShapeProperties22.Append(nonVisualDrawingProperties28);
			nonVisualShapeProperties22.Append(nonVisualShapeDrawingProperties22);
			nonVisualShapeProperties22.Append(applicationNonVisualDrawingProperties28);
			ShapeProperties shapeProperties23 = new ShapeProperties();

			TextBody textBody22 = new TextBody();
			A.BodyProperties bodyProperties22 = new A.BodyProperties();
			A.ListStyle listStyle22 = new A.ListStyle();

			A.Paragraph paragraph34 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties20 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph34.Append(endParagraphRunProperties20);

			textBody22.Append(bodyProperties22);
			textBody22.Append(listStyle22);
			textBody22.Append(paragraph34);

			shape22.Append(nonVisualShapeProperties22);
			shape22.Append(shapeProperties23);
			shape22.Append(textBody22);

			Shape shape23 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties23 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties29 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Slide Number Placeholder 5" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties23 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks23 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties23.Append(shapeLocks23);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties29 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape23 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

			applicationNonVisualDrawingProperties29.Append(placeholderShape23);

			nonVisualShapeProperties23.Append(nonVisualDrawingProperties29);
			nonVisualShapeProperties23.Append(nonVisualShapeDrawingProperties23);
			nonVisualShapeProperties23.Append(applicationNonVisualDrawingProperties29);
			ShapeProperties shapeProperties24 = new ShapeProperties();

			TextBody textBody23 = new TextBody();
			A.BodyProperties bodyProperties23 = new A.BodyProperties();
			A.ListStyle listStyle23 = new A.ListStyle();

			A.Paragraph paragraph35 = new A.Paragraph();

			A.Field field8 = new A.Field(){ Id = "{AFBBC1CF-DB04-427F-9A30-B7E1FB44F3B5}", Type = "slidenum" };
			A.RunProperties runProperties30 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text30 = new A.Text();
			text30.Text = "‹#›";

			field8.Append(runProperties30);
			field8.Append(text30);
			A.EndParagraphRunProperties endParagraphRunProperties21 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph35.Append(field8);
			paragraph35.Append(endParagraphRunProperties21);

			textBody23.Append(bodyProperties23);
			textBody23.Append(listStyle23);
			textBody23.Append(paragraph35);

			shape23.Append(nonVisualShapeProperties23);
			shape23.Append(shapeProperties24);
			shape23.Append(textBody23);

			shapeTree5.Append(nonVisualGroupShapeProperties5);
			shapeTree5.Append(groupShapeProperties5);
			shapeTree5.Append(shape19);
			shapeTree5.Append(shape20);
			shapeTree5.Append(shape21);
			shapeTree5.Append(shape22);
			shapeTree5.Append(shape23);

			commonSlideData5.Append(shapeTree5);

			ColorMapOverride colorMapOverride4 = new ColorMapOverride();
			A.MasterColorMapping masterColorMapping4 = new A.MasterColorMapping();

			colorMapOverride4.Append(masterColorMapping4);

			slideLayout3.Append(commonSlideData5);
			slideLayout3.Append(colorMapOverride4);

			slideLayoutPart3.SlideLayout = slideLayout3;
		}

		// Generates content of slideLayoutPart4.
		private void GenerateSlideLayoutPart4Content(SlideLayoutPart slideLayoutPart4)
		{
			SlideLayout slideLayout4 = new SlideLayout(){ Type = SlideLayoutValues.Blank, Preserve = true };
			slideLayout4.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slideLayout4.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slideLayout4.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData6 = new CommonSlideData(){ Name = "Blank" };

			ShapeTree shapeTree6 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties6 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties30 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties6 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties30 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties6.Append(nonVisualDrawingProperties30);
			nonVisualGroupShapeProperties6.Append(nonVisualGroupShapeDrawingProperties6);
			nonVisualGroupShapeProperties6.Append(applicationNonVisualDrawingProperties30);

			GroupShapeProperties groupShapeProperties6 = new GroupShapeProperties();

			A.TransformGroup transformGroup6 = new A.TransformGroup();
			A.Offset offset17 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents17 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset6 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents6 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup6.Append(offset17);
			transformGroup6.Append(extents17);
			transformGroup6.Append(childOffset6);
			transformGroup6.Append(childExtents6);

			groupShapeProperties6.Append(transformGroup6);

			Shape shape24 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties24 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties31 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Date Placeholder 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties24 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks24 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties24.Append(shapeLocks24);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties31 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape24 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)10U };

			applicationNonVisualDrawingProperties31.Append(placeholderShape24);

			nonVisualShapeProperties24.Append(nonVisualDrawingProperties31);
			nonVisualShapeProperties24.Append(nonVisualShapeDrawingProperties24);
			nonVisualShapeProperties24.Append(applicationNonVisualDrawingProperties31);
			ShapeProperties shapeProperties25 = new ShapeProperties();

			TextBody textBody24 = new TextBody();
			A.BodyProperties bodyProperties24 = new A.BodyProperties();
			A.ListStyle listStyle24 = new A.ListStyle();

			A.Paragraph paragraph36 = new A.Paragraph();

			A.Field field9 = new A.Field(){ Id = "{6A870D7B-DCE0-46E8-B448-2F0E4E8E28D1}", Type = "datetimeFigureOut" };
			A.RunProperties runProperties31 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text31 = new A.Text();
			text31.Text = "3/28/2013";

			field9.Append(runProperties31);
			field9.Append(text31);
			A.EndParagraphRunProperties endParagraphRunProperties22 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph36.Append(field9);
			paragraph36.Append(endParagraphRunProperties22);

			textBody24.Append(bodyProperties24);
			textBody24.Append(listStyle24);
			textBody24.Append(paragraph36);

			shape24.Append(nonVisualShapeProperties24);
			shape24.Append(shapeProperties25);
			shape24.Append(textBody24);

			Shape shape25 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties25 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties32 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Footer Placeholder 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties25 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks25 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties25.Append(shapeLocks25);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties32 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape25 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

			applicationNonVisualDrawingProperties32.Append(placeholderShape25);

			nonVisualShapeProperties25.Append(nonVisualDrawingProperties32);
			nonVisualShapeProperties25.Append(nonVisualShapeDrawingProperties25);
			nonVisualShapeProperties25.Append(applicationNonVisualDrawingProperties32);
			ShapeProperties shapeProperties26 = new ShapeProperties();

			TextBody textBody25 = new TextBody();
			A.BodyProperties bodyProperties25 = new A.BodyProperties();
			A.ListStyle listStyle25 = new A.ListStyle();

			A.Paragraph paragraph37 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties23 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph37.Append(endParagraphRunProperties23);

			textBody25.Append(bodyProperties25);
			textBody25.Append(listStyle25);
			textBody25.Append(paragraph37);

			shape25.Append(nonVisualShapeProperties25);
			shape25.Append(shapeProperties26);
			shape25.Append(textBody25);

			Shape shape26 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties26 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties33 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Slide Number Placeholder 3" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties26 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks26 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties26.Append(shapeLocks26);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties33 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape26 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

			applicationNonVisualDrawingProperties33.Append(placeholderShape26);

			nonVisualShapeProperties26.Append(nonVisualDrawingProperties33);
			nonVisualShapeProperties26.Append(nonVisualShapeDrawingProperties26);
			nonVisualShapeProperties26.Append(applicationNonVisualDrawingProperties33);
			ShapeProperties shapeProperties27 = new ShapeProperties();

			TextBody textBody26 = new TextBody();
			A.BodyProperties bodyProperties26 = new A.BodyProperties();
			A.ListStyle listStyle26 = new A.ListStyle();

			A.Paragraph paragraph38 = new A.Paragraph();

			A.Field field10 = new A.Field(){ Id = "{AFBBC1CF-DB04-427F-9A30-B7E1FB44F3B5}", Type = "slidenum" };
			A.RunProperties runProperties32 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text32 = new A.Text();
			text32.Text = "‹#›";

			field10.Append(runProperties32);
			field10.Append(text32);
			A.EndParagraphRunProperties endParagraphRunProperties24 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph38.Append(field10);
			paragraph38.Append(endParagraphRunProperties24);

			textBody26.Append(bodyProperties26);
			textBody26.Append(listStyle26);
			textBody26.Append(paragraph38);

			shape26.Append(nonVisualShapeProperties26);
			shape26.Append(shapeProperties27);
			shape26.Append(textBody26);

			shapeTree6.Append(nonVisualGroupShapeProperties6);
			shapeTree6.Append(groupShapeProperties6);
			shapeTree6.Append(shape24);
			shapeTree6.Append(shape25);
			shapeTree6.Append(shape26);

			commonSlideData6.Append(shapeTree6);

			ColorMapOverride colorMapOverride5 = new ColorMapOverride();
			A.MasterColorMapping masterColorMapping5 = new A.MasterColorMapping();

			colorMapOverride5.Append(masterColorMapping5);

			slideLayout4.Append(commonSlideData6);
			slideLayout4.Append(colorMapOverride5);

			slideLayoutPart4.SlideLayout = slideLayout4;
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
			A.LatinFont latinFont29 = new A.LatinFont(){ Typeface = "Calibri" };
			A.EastAsianFont eastAsianFont29 = new A.EastAsianFont(){ Typeface = "" };
			A.ComplexScriptFont complexScriptFont29 = new A.ComplexScriptFont(){ Typeface = "" };
			A.SupplementalFont supplementalFont1 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
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

			majorFont1.Append(latinFont29);
			majorFont1.Append(eastAsianFont29);
			majorFont1.Append(complexScriptFont29);
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
			A.LatinFont latinFont30 = new A.LatinFont(){ Typeface = "Calibri" };
			A.EastAsianFont eastAsianFont30 = new A.EastAsianFont(){ Typeface = "" };
			A.ComplexScriptFont complexScriptFont30 = new A.ComplexScriptFont(){ Typeface = "" };
			A.SupplementalFont supplementalFont30 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
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

			minorFont1.Append(latinFont30);
			minorFont1.Append(eastAsianFont30);
			minorFont1.Append(complexScriptFont30);
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

			A.SolidFill solidFill41 = new A.SolidFill();
			A.SchemeColor schemeColor42 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

			solidFill41.Append(schemeColor42);

			A.GradientFill gradientFill1 = new A.GradientFill(){ RotateWithShape = true };

			A.GradientStopList gradientStopList1 = new A.GradientStopList();

			A.GradientStop gradientStop1 = new A.GradientStop(){ Position = 0 };

			A.SchemeColor schemeColor43 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Tint tint13 = new A.Tint(){ Val = 50000 };
			A.SaturationModulation saturationModulation1 = new A.SaturationModulation(){ Val = 300000 };

			schemeColor43.Append(tint13);
			schemeColor43.Append(saturationModulation1);

			gradientStop1.Append(schemeColor43);

			A.GradientStop gradientStop2 = new A.GradientStop(){ Position = 35000 };

			A.SchemeColor schemeColor44 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Tint tint14 = new A.Tint(){ Val = 37000 };
			A.SaturationModulation saturationModulation2 = new A.SaturationModulation(){ Val = 300000 };

			schemeColor44.Append(tint14);
			schemeColor44.Append(saturationModulation2);

			gradientStop2.Append(schemeColor44);

			A.GradientStop gradientStop3 = new A.GradientStop(){ Position = 100000 };

			A.SchemeColor schemeColor45 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Tint tint15 = new A.Tint(){ Val = 15000 };
			A.SaturationModulation saturationModulation3 = new A.SaturationModulation(){ Val = 350000 };

			schemeColor45.Append(tint15);
			schemeColor45.Append(saturationModulation3);

			gradientStop3.Append(schemeColor45);

			gradientStopList1.Append(gradientStop1);
			gradientStopList1.Append(gradientStop2);
			gradientStopList1.Append(gradientStop3);
			A.LinearGradientFill linearGradientFill1 = new A.LinearGradientFill(){ Angle = 16200000, Scaled = true };

			gradientFill1.Append(gradientStopList1);
			gradientFill1.Append(linearGradientFill1);

			A.GradientFill gradientFill2 = new A.GradientFill(){ RotateWithShape = true };

			A.GradientStopList gradientStopList2 = new A.GradientStopList();

			A.GradientStop gradientStop4 = new A.GradientStop(){ Position = 0 };

			A.SchemeColor schemeColor46 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade1 = new A.Shade(){ Val = 51000 };
			A.SaturationModulation saturationModulation4 = new A.SaturationModulation(){ Val = 130000 };

			schemeColor46.Append(shade1);
			schemeColor46.Append(saturationModulation4);

			gradientStop4.Append(schemeColor46);

			A.GradientStop gradientStop5 = new A.GradientStop(){ Position = 80000 };

			A.SchemeColor schemeColor47 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade2 = new A.Shade(){ Val = 93000 };
			A.SaturationModulation saturationModulation5 = new A.SaturationModulation(){ Val = 130000 };

			schemeColor47.Append(shade2);
			schemeColor47.Append(saturationModulation5);

			gradientStop5.Append(schemeColor47);

			A.GradientStop gradientStop6 = new A.GradientStop(){ Position = 100000 };

			A.SchemeColor schemeColor48 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade3 = new A.Shade(){ Val = 94000 };
			A.SaturationModulation saturationModulation6 = new A.SaturationModulation(){ Val = 135000 };

			schemeColor48.Append(shade3);
			schemeColor48.Append(saturationModulation6);

			gradientStop6.Append(schemeColor48);

			gradientStopList2.Append(gradientStop4);
			gradientStopList2.Append(gradientStop5);
			gradientStopList2.Append(gradientStop6);
			A.LinearGradientFill linearGradientFill2 = new A.LinearGradientFill(){ Angle = 16200000, Scaled = false };

			gradientFill2.Append(gradientStopList2);
			gradientFill2.Append(linearGradientFill2);

			fillStyleList1.Append(solidFill41);
			fillStyleList1.Append(gradientFill1);
			fillStyleList1.Append(gradientFill2);

			A.LineStyleList lineStyleList1 = new A.LineStyleList();

			A.Outline outline1 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

			A.SolidFill solidFill42 = new A.SolidFill();

			A.SchemeColor schemeColor49 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade4 = new A.Shade(){ Val = 95000 };
			A.SaturationModulation saturationModulation7 = new A.SaturationModulation(){ Val = 105000 };

			schemeColor49.Append(shade4);
			schemeColor49.Append(saturationModulation7);

			solidFill42.Append(schemeColor49);
			A.PresetDash presetDash1 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

			outline1.Append(solidFill42);
			outline1.Append(presetDash1);

			A.Outline outline2 = new A.Outline(){ Width = 25400, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

			A.SolidFill solidFill43 = new A.SolidFill();
			A.SchemeColor schemeColor50 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

			solidFill43.Append(schemeColor50);
			A.PresetDash presetDash2 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

			outline2.Append(solidFill43);
			outline2.Append(presetDash2);

			A.Outline outline3 = new A.Outline(){ Width = 38100, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

			A.SolidFill solidFill44 = new A.SolidFill();
			A.SchemeColor schemeColor51 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

			solidFill44.Append(schemeColor51);
			A.PresetDash presetDash3 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

			outline3.Append(solidFill44);
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

			A.SolidFill solidFill45 = new A.SolidFill();
			A.SchemeColor schemeColor52 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

			solidFill45.Append(schemeColor52);

			A.GradientFill gradientFill3 = new A.GradientFill(){ RotateWithShape = true };

			A.GradientStopList gradientStopList3 = new A.GradientStopList();

			A.GradientStop gradientStop7 = new A.GradientStop(){ Position = 0 };

			A.SchemeColor schemeColor53 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Tint tint16 = new A.Tint(){ Val = 40000 };
			A.SaturationModulation saturationModulation8 = new A.SaturationModulation(){ Val = 350000 };

			schemeColor53.Append(tint16);
			schemeColor53.Append(saturationModulation8);

			gradientStop7.Append(schemeColor53);

			A.GradientStop gradientStop8 = new A.GradientStop(){ Position = 40000 };

			A.SchemeColor schemeColor54 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Tint tint17 = new A.Tint(){ Val = 45000 };
			A.Shade shade5 = new A.Shade(){ Val = 99000 };
			A.SaturationModulation saturationModulation9 = new A.SaturationModulation(){ Val = 350000 };

			schemeColor54.Append(tint17);
			schemeColor54.Append(shade5);
			schemeColor54.Append(saturationModulation9);

			gradientStop8.Append(schemeColor54);

			A.GradientStop gradientStop9 = new A.GradientStop(){ Position = 100000 };

			A.SchemeColor schemeColor55 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade6 = new A.Shade(){ Val = 20000 };
			A.SaturationModulation saturationModulation10 = new A.SaturationModulation(){ Val = 255000 };

			schemeColor55.Append(shade6);
			schemeColor55.Append(saturationModulation10);

			gradientStop9.Append(schemeColor55);

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

			A.SchemeColor schemeColor56 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Tint tint18 = new A.Tint(){ Val = 80000 };
			A.SaturationModulation saturationModulation11 = new A.SaturationModulation(){ Val = 300000 };

			schemeColor56.Append(tint18);
			schemeColor56.Append(saturationModulation11);

			gradientStop10.Append(schemeColor56);

			A.GradientStop gradientStop11 = new A.GradientStop(){ Position = 100000 };

			A.SchemeColor schemeColor57 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
			A.Shade shade7 = new A.Shade(){ Val = 30000 };
			A.SaturationModulation saturationModulation12 = new A.SaturationModulation(){ Val = 200000 };

			schemeColor57.Append(shade7);
			schemeColor57.Append(saturationModulation12);

			gradientStop11.Append(schemeColor57);

			gradientStopList4.Append(gradientStop10);
			gradientStopList4.Append(gradientStop11);

			A.PathGradientFill pathGradientFill2 = new A.PathGradientFill(){ Path = A.PathShadeValues.Circle };
			A.FillToRectangle fillToRectangle2 = new A.FillToRectangle(){ Left = 50000, Top = 50000, Right = 50000, Bottom = 50000 };

			pathGradientFill2.Append(fillToRectangle2);

			gradientFill4.Append(gradientStopList4);
			gradientFill4.Append(pathGradientFill2);

			backgroundFillStyleList1.Append(solidFill45);
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

		// Generates content of slideLayoutPart5.
		private void GenerateSlideLayoutPart5Content(SlideLayoutPart slideLayoutPart5)
		{
			SlideLayout slideLayout5 = new SlideLayout(){ Type = SlideLayoutValues.Title, Preserve = true };
			slideLayout5.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slideLayout5.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slideLayout5.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData7 = new CommonSlideData(){ Name = "Title Slide" };

			ShapeTree shapeTree7 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties7 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties34 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties7 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties34 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties7.Append(nonVisualDrawingProperties34);
			nonVisualGroupShapeProperties7.Append(nonVisualGroupShapeDrawingProperties7);
			nonVisualGroupShapeProperties7.Append(applicationNonVisualDrawingProperties34);

			GroupShapeProperties groupShapeProperties7 = new GroupShapeProperties();

			A.TransformGroup transformGroup7 = new A.TransformGroup();
			A.Offset offset18 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents18 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset7 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents7 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup7.Append(offset18);
			transformGroup7.Append(extents18);
			transformGroup7.Append(childOffset7);
			transformGroup7.Append(childExtents7);

			groupShapeProperties7.Append(transformGroup7);

			Shape shape27 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties27 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties35 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties27 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks27 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties27.Append(shapeLocks27);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties35 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape27 = new PlaceholderShape(){ Type = PlaceholderValues.CenteredTitle };

			applicationNonVisualDrawingProperties35.Append(placeholderShape27);

			nonVisualShapeProperties27.Append(nonVisualDrawingProperties35);
			nonVisualShapeProperties27.Append(nonVisualShapeDrawingProperties27);
			nonVisualShapeProperties27.Append(applicationNonVisualDrawingProperties35);

			ShapeProperties shapeProperties28 = new ShapeProperties();

			A.Transform2D transform2D12 = new A.Transform2D();
			A.Offset offset19 = new A.Offset(){ X = 685800L, Y = 2130425L };
			A.Extents extents19 = new A.Extents(){ Cx = 7772400L, Cy = 1470025L };

			transform2D12.Append(offset19);
			transform2D12.Append(extents19);

			shapeProperties28.Append(transform2D12);

			TextBody textBody27 = new TextBody();
			A.BodyProperties bodyProperties27 = new A.BodyProperties();
			A.ListStyle listStyle27 = new A.ListStyle();

			A.Paragraph paragraph39 = new A.Paragraph();

			A.Run run23 = new A.Run();
			A.RunProperties runProperties33 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text33 = new A.Text();
			text33.Text = "Click to edit Master title style";

			run23.Append(runProperties33);
			run23.Append(text33);
			A.EndParagraphRunProperties endParagraphRunProperties25 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph39.Append(run23);
			paragraph39.Append(endParagraphRunProperties25);

			textBody27.Append(bodyProperties27);
			textBody27.Append(listStyle27);
			textBody27.Append(paragraph39);

			shape27.Append(nonVisualShapeProperties27);
			shape27.Append(shapeProperties28);
			shape27.Append(textBody27);

			Shape shape28 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties28 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties36 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Subtitle 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties28 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks28 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties28.Append(shapeLocks28);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties36 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape28 = new PlaceholderShape(){ Type = PlaceholderValues.SubTitle, Index = (UInt32Value)1U };

			applicationNonVisualDrawingProperties36.Append(placeholderShape28);

			nonVisualShapeProperties28.Append(nonVisualDrawingProperties36);
			nonVisualShapeProperties28.Append(nonVisualShapeDrawingProperties28);
			nonVisualShapeProperties28.Append(applicationNonVisualDrawingProperties36);

			ShapeProperties shapeProperties29 = new ShapeProperties();

			A.Transform2D transform2D13 = new A.Transform2D();
			A.Offset offset20 = new A.Offset(){ X = 1371600L, Y = 3886200L };
			A.Extents extents20 = new A.Extents(){ Cx = 6400800L, Cy = 1752600L };

			transform2D13.Append(offset20);
			transform2D13.Append(extents20);

			shapeProperties29.Append(transform2D13);

			TextBody textBody28 = new TextBody();
			A.BodyProperties bodyProperties28 = new A.BodyProperties();

			A.ListStyle listStyle28 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties13 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
			A.NoBullet noBullet20 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties63 = new A.DefaultRunProperties();

			A.SolidFill solidFill46 = new A.SolidFill();

			A.SchemeColor schemeColor58 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint19 = new A.Tint(){ Val = 75000 };

			schemeColor58.Append(tint19);

			solidFill46.Append(schemeColor58);

			defaultRunProperties63.Append(solidFill46);

			level1ParagraphProperties13.Append(noBullet20);
			level1ParagraphProperties13.Append(defaultRunProperties63);

			A.Level2ParagraphProperties level2ParagraphProperties7 = new A.Level2ParagraphProperties(){ LeftMargin = 457200, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
			A.NoBullet noBullet21 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties64 = new A.DefaultRunProperties();

			A.SolidFill solidFill47 = new A.SolidFill();

			A.SchemeColor schemeColor59 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint20 = new A.Tint(){ Val = 75000 };

			schemeColor59.Append(tint20);

			solidFill47.Append(schemeColor59);

			defaultRunProperties64.Append(solidFill47);

			level2ParagraphProperties7.Append(noBullet21);
			level2ParagraphProperties7.Append(defaultRunProperties64);

			A.Level3ParagraphProperties level3ParagraphProperties7 = new A.Level3ParagraphProperties(){ LeftMargin = 914400, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
			A.NoBullet noBullet22 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties65 = new A.DefaultRunProperties();

			A.SolidFill solidFill48 = new A.SolidFill();

			A.SchemeColor schemeColor60 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint21 = new A.Tint(){ Val = 75000 };

			schemeColor60.Append(tint21);

			solidFill48.Append(schemeColor60);

			defaultRunProperties65.Append(solidFill48);

			level3ParagraphProperties7.Append(noBullet22);
			level3ParagraphProperties7.Append(defaultRunProperties65);

			A.Level4ParagraphProperties level4ParagraphProperties7 = new A.Level4ParagraphProperties(){ LeftMargin = 1371600, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
			A.NoBullet noBullet23 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties66 = new A.DefaultRunProperties();

			A.SolidFill solidFill49 = new A.SolidFill();

			A.SchemeColor schemeColor61 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint22 = new A.Tint(){ Val = 75000 };

			schemeColor61.Append(tint22);

			solidFill49.Append(schemeColor61);

			defaultRunProperties66.Append(solidFill49);

			level4ParagraphProperties7.Append(noBullet23);
			level4ParagraphProperties7.Append(defaultRunProperties66);

			A.Level5ParagraphProperties level5ParagraphProperties7 = new A.Level5ParagraphProperties(){ LeftMargin = 1828800, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
			A.NoBullet noBullet24 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties67 = new A.DefaultRunProperties();

			A.SolidFill solidFill50 = new A.SolidFill();

			A.SchemeColor schemeColor62 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint23 = new A.Tint(){ Val = 75000 };

			schemeColor62.Append(tint23);

			solidFill50.Append(schemeColor62);

			defaultRunProperties67.Append(solidFill50);

			level5ParagraphProperties7.Append(noBullet24);
			level5ParagraphProperties7.Append(defaultRunProperties67);

			A.Level6ParagraphProperties level6ParagraphProperties7 = new A.Level6ParagraphProperties(){ LeftMargin = 2286000, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
			A.NoBullet noBullet25 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties68 = new A.DefaultRunProperties();

			A.SolidFill solidFill51 = new A.SolidFill();

			A.SchemeColor schemeColor63 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint24 = new A.Tint(){ Val = 75000 };

			schemeColor63.Append(tint24);

			solidFill51.Append(schemeColor63);

			defaultRunProperties68.Append(solidFill51);

			level6ParagraphProperties7.Append(noBullet25);
			level6ParagraphProperties7.Append(defaultRunProperties68);

			A.Level7ParagraphProperties level7ParagraphProperties7 = new A.Level7ParagraphProperties(){ LeftMargin = 2743200, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
			A.NoBullet noBullet26 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties69 = new A.DefaultRunProperties();

			A.SolidFill solidFill52 = new A.SolidFill();

			A.SchemeColor schemeColor64 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint25 = new A.Tint(){ Val = 75000 };

			schemeColor64.Append(tint25);

			solidFill52.Append(schemeColor64);

			defaultRunProperties69.Append(solidFill52);

			level7ParagraphProperties7.Append(noBullet26);
			level7ParagraphProperties7.Append(defaultRunProperties69);

			A.Level8ParagraphProperties level8ParagraphProperties7 = new A.Level8ParagraphProperties(){ LeftMargin = 3200400, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
			A.NoBullet noBullet27 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties70 = new A.DefaultRunProperties();

			A.SolidFill solidFill53 = new A.SolidFill();

			A.SchemeColor schemeColor65 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint26 = new A.Tint(){ Val = 75000 };

			schemeColor65.Append(tint26);

			solidFill53.Append(schemeColor65);

			defaultRunProperties70.Append(solidFill53);

			level8ParagraphProperties7.Append(noBullet27);
			level8ParagraphProperties7.Append(defaultRunProperties70);

			A.Level9ParagraphProperties level9ParagraphProperties7 = new A.Level9ParagraphProperties(){ LeftMargin = 3657600, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
			A.NoBullet noBullet28 = new A.NoBullet();

			A.DefaultRunProperties defaultRunProperties71 = new A.DefaultRunProperties();

			A.SolidFill solidFill54 = new A.SolidFill();

			A.SchemeColor schemeColor66 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
			A.Tint tint27 = new A.Tint(){ Val = 75000 };

			schemeColor66.Append(tint27);

			solidFill54.Append(schemeColor66);

			defaultRunProperties71.Append(solidFill54);

			level9ParagraphProperties7.Append(noBullet28);
			level9ParagraphProperties7.Append(defaultRunProperties71);

			listStyle28.Append(level1ParagraphProperties13);
			listStyle28.Append(level2ParagraphProperties7);
			listStyle28.Append(level3ParagraphProperties7);
			listStyle28.Append(level4ParagraphProperties7);
			listStyle28.Append(level5ParagraphProperties7);
			listStyle28.Append(level6ParagraphProperties7);
			listStyle28.Append(level7ParagraphProperties7);
			listStyle28.Append(level8ParagraphProperties7);
			listStyle28.Append(level9ParagraphProperties7);

			A.Paragraph paragraph40 = new A.Paragraph();

			A.Run run24 = new A.Run();
			A.RunProperties runProperties34 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text34 = new A.Text();
			text34.Text = "Click to edit Master subtitle style";

			run24.Append(runProperties34);
			run24.Append(text34);
			A.EndParagraphRunProperties endParagraphRunProperties26 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph40.Append(run24);
			paragraph40.Append(endParagraphRunProperties26);

			textBody28.Append(bodyProperties28);
			textBody28.Append(listStyle28);
			textBody28.Append(paragraph40);

			shape28.Append(nonVisualShapeProperties28);
			shape28.Append(shapeProperties29);
			shape28.Append(textBody28);

			Shape shape29 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties29 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties37 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Date Placeholder 3" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties29 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks29 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties29.Append(shapeLocks29);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties37 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape29 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)10U };

			applicationNonVisualDrawingProperties37.Append(placeholderShape29);

			nonVisualShapeProperties29.Append(nonVisualDrawingProperties37);
			nonVisualShapeProperties29.Append(nonVisualShapeDrawingProperties29);
			nonVisualShapeProperties29.Append(applicationNonVisualDrawingProperties37);
			ShapeProperties shapeProperties30 = new ShapeProperties();

			TextBody textBody29 = new TextBody();
			A.BodyProperties bodyProperties29 = new A.BodyProperties();
			A.ListStyle listStyle29 = new A.ListStyle();

			A.Paragraph paragraph41 = new A.Paragraph();

			A.Field field11 = new A.Field(){ Id = "{6A870D7B-DCE0-46E8-B448-2F0E4E8E28D1}", Type = "datetimeFigureOut" };
			A.RunProperties runProperties35 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text35 = new A.Text();
			text35.Text = "3/28/2013";

			field11.Append(runProperties35);
			field11.Append(text35);
			A.EndParagraphRunProperties endParagraphRunProperties27 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph41.Append(field11);
			paragraph41.Append(endParagraphRunProperties27);

			textBody29.Append(bodyProperties29);
			textBody29.Append(listStyle29);
			textBody29.Append(paragraph41);

			shape29.Append(nonVisualShapeProperties29);
			shape29.Append(shapeProperties30);
			shape29.Append(textBody29);

			Shape shape30 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties30 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties38 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Footer Placeholder 4" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties30 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks30 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties30.Append(shapeLocks30);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties38 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape30 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

			applicationNonVisualDrawingProperties38.Append(placeholderShape30);

			nonVisualShapeProperties30.Append(nonVisualDrawingProperties38);
			nonVisualShapeProperties30.Append(nonVisualShapeDrawingProperties30);
			nonVisualShapeProperties30.Append(applicationNonVisualDrawingProperties38);
			ShapeProperties shapeProperties31 = new ShapeProperties();

			TextBody textBody30 = new TextBody();
			A.BodyProperties bodyProperties30 = new A.BodyProperties();
			A.ListStyle listStyle30 = new A.ListStyle();

			A.Paragraph paragraph42 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties28 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph42.Append(endParagraphRunProperties28);

			textBody30.Append(bodyProperties30);
			textBody30.Append(listStyle30);
			textBody30.Append(paragraph42);

			shape30.Append(nonVisualShapeProperties30);
			shape30.Append(shapeProperties31);
			shape30.Append(textBody30);

			Shape shape31 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties31 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties39 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Slide Number Placeholder 5" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties31 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks31 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties31.Append(shapeLocks31);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties39 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape31 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

			applicationNonVisualDrawingProperties39.Append(placeholderShape31);

			nonVisualShapeProperties31.Append(nonVisualDrawingProperties39);
			nonVisualShapeProperties31.Append(nonVisualShapeDrawingProperties31);
			nonVisualShapeProperties31.Append(applicationNonVisualDrawingProperties39);
			ShapeProperties shapeProperties32 = new ShapeProperties();

			TextBody textBody31 = new TextBody();
			A.BodyProperties bodyProperties31 = new A.BodyProperties();
			A.ListStyle listStyle31 = new A.ListStyle();

			A.Paragraph paragraph43 = new A.Paragraph();

			A.Field field12 = new A.Field(){ Id = "{AFBBC1CF-DB04-427F-9A30-B7E1FB44F3B5}", Type = "slidenum" };
			A.RunProperties runProperties36 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text36 = new A.Text();
			text36.Text = "‹#›";

			field12.Append(runProperties36);
			field12.Append(text36);
			A.EndParagraphRunProperties endParagraphRunProperties29 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph43.Append(field12);
			paragraph43.Append(endParagraphRunProperties29);

			textBody31.Append(bodyProperties31);
			textBody31.Append(listStyle31);
			textBody31.Append(paragraph43);

			shape31.Append(nonVisualShapeProperties31);
			shape31.Append(shapeProperties32);
			shape31.Append(textBody31);

			shapeTree7.Append(nonVisualGroupShapeProperties7);
			shapeTree7.Append(groupShapeProperties7);
			shapeTree7.Append(shape27);
			shapeTree7.Append(shape28);
			shapeTree7.Append(shape29);
			shapeTree7.Append(shape30);
			shapeTree7.Append(shape31);

			commonSlideData7.Append(shapeTree7);

			ColorMapOverride colorMapOverride6 = new ColorMapOverride();
			A.MasterColorMapping masterColorMapping6 = new A.MasterColorMapping();

			colorMapOverride6.Append(masterColorMapping6);

			slideLayout5.Append(commonSlideData7);
			slideLayout5.Append(colorMapOverride6);

			slideLayoutPart5.SlideLayout = slideLayout5;
		}

		// Generates content of slideLayoutPart6.
		private void GenerateSlideLayoutPart6Content(SlideLayoutPart slideLayoutPart6)
		{
			SlideLayout slideLayout6 = new SlideLayout(){ Type = SlideLayoutValues.TitleOnly, Preserve = true };
			slideLayout6.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slideLayout6.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slideLayout6.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData8 = new CommonSlideData(){ Name = "Title Only" };

			ShapeTree shapeTree8 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties8 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties40 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties8 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties40 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties8.Append(nonVisualDrawingProperties40);
			nonVisualGroupShapeProperties8.Append(nonVisualGroupShapeDrawingProperties8);
			nonVisualGroupShapeProperties8.Append(applicationNonVisualDrawingProperties40);

			GroupShapeProperties groupShapeProperties8 = new GroupShapeProperties();

			A.TransformGroup transformGroup8 = new A.TransformGroup();
			A.Offset offset21 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents21 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset8 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents8 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup8.Append(offset21);
			transformGroup8.Append(extents21);
			transformGroup8.Append(childOffset8);
			transformGroup8.Append(childExtents8);

			groupShapeProperties8.Append(transformGroup8);

			Shape shape32 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties32 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties41 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties32 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks32 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties32.Append(shapeLocks32);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties41 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape32 = new PlaceholderShape(){ Type = PlaceholderValues.Title };

			applicationNonVisualDrawingProperties41.Append(placeholderShape32);

			nonVisualShapeProperties32.Append(nonVisualDrawingProperties41);
			nonVisualShapeProperties32.Append(nonVisualShapeDrawingProperties32);
			nonVisualShapeProperties32.Append(applicationNonVisualDrawingProperties41);
			ShapeProperties shapeProperties33 = new ShapeProperties();

			TextBody textBody32 = new TextBody();
			A.BodyProperties bodyProperties32 = new A.BodyProperties();
			A.ListStyle listStyle32 = new A.ListStyle();

			A.Paragraph paragraph44 = new A.Paragraph();

			A.Run run25 = new A.Run();
			A.RunProperties runProperties37 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text37 = new A.Text();
			text37.Text = "Click to edit Master title style";

			run25.Append(runProperties37);
			run25.Append(text37);
			A.EndParagraphRunProperties endParagraphRunProperties30 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph44.Append(run25);
			paragraph44.Append(endParagraphRunProperties30);

			textBody32.Append(bodyProperties32);
			textBody32.Append(listStyle32);
			textBody32.Append(paragraph44);

			shape32.Append(nonVisualShapeProperties32);
			shape32.Append(shapeProperties33);
			shape32.Append(textBody32);

			Shape shape33 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties33 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties42 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Date Placeholder 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties33 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks33 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties33.Append(shapeLocks33);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties42 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape33 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)10U };

			applicationNonVisualDrawingProperties42.Append(placeholderShape33);

			nonVisualShapeProperties33.Append(nonVisualDrawingProperties42);
			nonVisualShapeProperties33.Append(nonVisualShapeDrawingProperties33);
			nonVisualShapeProperties33.Append(applicationNonVisualDrawingProperties42);
			ShapeProperties shapeProperties34 = new ShapeProperties();

			TextBody textBody33 = new TextBody();
			A.BodyProperties bodyProperties33 = new A.BodyProperties();
			A.ListStyle listStyle33 = new A.ListStyle();

			A.Paragraph paragraph45 = new A.Paragraph();

			A.Field field13 = new A.Field(){ Id = "{6A870D7B-DCE0-46E8-B448-2F0E4E8E28D1}", Type = "datetimeFigureOut" };
			A.RunProperties runProperties38 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text38 = new A.Text();
			text38.Text = "3/28/2013";

			field13.Append(runProperties38);
			field13.Append(text38);
			A.EndParagraphRunProperties endParagraphRunProperties31 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph45.Append(field13);
			paragraph45.Append(endParagraphRunProperties31);

			textBody33.Append(bodyProperties33);
			textBody33.Append(listStyle33);
			textBody33.Append(paragraph45);

			shape33.Append(nonVisualShapeProperties33);
			shape33.Append(shapeProperties34);
			shape33.Append(textBody33);

			Shape shape34 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties34 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties43 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Footer Placeholder 3" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties34 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks34 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties34.Append(shapeLocks34);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties43 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape34 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

			applicationNonVisualDrawingProperties43.Append(placeholderShape34);

			nonVisualShapeProperties34.Append(nonVisualDrawingProperties43);
			nonVisualShapeProperties34.Append(nonVisualShapeDrawingProperties34);
			nonVisualShapeProperties34.Append(applicationNonVisualDrawingProperties43);
			ShapeProperties shapeProperties35 = new ShapeProperties();

			TextBody textBody34 = new TextBody();
			A.BodyProperties bodyProperties34 = new A.BodyProperties();
			A.ListStyle listStyle34 = new A.ListStyle();

			A.Paragraph paragraph46 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties32 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph46.Append(endParagraphRunProperties32);

			textBody34.Append(bodyProperties34);
			textBody34.Append(listStyle34);
			textBody34.Append(paragraph46);

			shape34.Append(nonVisualShapeProperties34);
			shape34.Append(shapeProperties35);
			shape34.Append(textBody34);

			Shape shape35 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties35 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties44 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Slide Number Placeholder 4" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties35 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks35 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties35.Append(shapeLocks35);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties44 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape35 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

			applicationNonVisualDrawingProperties44.Append(placeholderShape35);

			nonVisualShapeProperties35.Append(nonVisualDrawingProperties44);
			nonVisualShapeProperties35.Append(nonVisualShapeDrawingProperties35);
			nonVisualShapeProperties35.Append(applicationNonVisualDrawingProperties44);
			ShapeProperties shapeProperties36 = new ShapeProperties();

			TextBody textBody35 = new TextBody();
			A.BodyProperties bodyProperties35 = new A.BodyProperties();
			A.ListStyle listStyle35 = new A.ListStyle();

			A.Paragraph paragraph47 = new A.Paragraph();

			A.Field field14 = new A.Field(){ Id = "{AFBBC1CF-DB04-427F-9A30-B7E1FB44F3B5}", Type = "slidenum" };
			A.RunProperties runProperties39 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text39 = new A.Text();
			text39.Text = "‹#›";

			field14.Append(runProperties39);
			field14.Append(text39);
			A.EndParagraphRunProperties endParagraphRunProperties33 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph47.Append(field14);
			paragraph47.Append(endParagraphRunProperties33);

			textBody35.Append(bodyProperties35);
			textBody35.Append(listStyle35);
			textBody35.Append(paragraph47);

			shape35.Append(nonVisualShapeProperties35);
			shape35.Append(shapeProperties36);
			shape35.Append(textBody35);

			shapeTree8.Append(nonVisualGroupShapeProperties8);
			shapeTree8.Append(groupShapeProperties8);
			shapeTree8.Append(shape32);
			shapeTree8.Append(shape33);
			shapeTree8.Append(shape34);
			shapeTree8.Append(shape35);

			commonSlideData8.Append(shapeTree8);

			ColorMapOverride colorMapOverride7 = new ColorMapOverride();
			A.MasterColorMapping masterColorMapping7 = new A.MasterColorMapping();

			colorMapOverride7.Append(masterColorMapping7);

			slideLayout6.Append(commonSlideData8);
			slideLayout6.Append(colorMapOverride7);

			slideLayoutPart6.SlideLayout = slideLayout6;
		}

		// Generates content of slideLayoutPart7.
		private void GenerateSlideLayoutPart7Content(SlideLayoutPart slideLayoutPart7)
		{
			SlideLayout slideLayout7 = new SlideLayout(){ Type = SlideLayoutValues.VerticalTitleAndText, Preserve = true };
			slideLayout7.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slideLayout7.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slideLayout7.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData9 = new CommonSlideData(){ Name = "Vertical Title and Text" };

			ShapeTree shapeTree9 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties9 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties45 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties9 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties45 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties9.Append(nonVisualDrawingProperties45);
			nonVisualGroupShapeProperties9.Append(nonVisualGroupShapeDrawingProperties9);
			nonVisualGroupShapeProperties9.Append(applicationNonVisualDrawingProperties45);

			GroupShapeProperties groupShapeProperties9 = new GroupShapeProperties();

			A.TransformGroup transformGroup9 = new A.TransformGroup();
			A.Offset offset22 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents22 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset9 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents9 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup9.Append(offset22);
			transformGroup9.Append(extents22);
			transformGroup9.Append(childOffset9);
			transformGroup9.Append(childExtents9);

			groupShapeProperties9.Append(transformGroup9);

			Shape shape36 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties36 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties46 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Vertical Title 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties36 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks36 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties36.Append(shapeLocks36);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties46 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape36 = new PlaceholderShape(){ Type = PlaceholderValues.Title, Orientation = DirectionValues.Vertical };

			applicationNonVisualDrawingProperties46.Append(placeholderShape36);

			nonVisualShapeProperties36.Append(nonVisualDrawingProperties46);
			nonVisualShapeProperties36.Append(nonVisualShapeDrawingProperties36);
			nonVisualShapeProperties36.Append(applicationNonVisualDrawingProperties46);

			ShapeProperties shapeProperties37 = new ShapeProperties();

			A.Transform2D transform2D14 = new A.Transform2D();
			A.Offset offset23 = new A.Offset(){ X = 6629400L, Y = 274638L };
			A.Extents extents23 = new A.Extents(){ Cx = 2057400L, Cy = 5851525L };

			transform2D14.Append(offset23);
			transform2D14.Append(extents23);

			shapeProperties37.Append(transform2D14);

			TextBody textBody36 = new TextBody();
			A.BodyProperties bodyProperties36 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.EastAsianVetical };
			A.ListStyle listStyle36 = new A.ListStyle();

			A.Paragraph paragraph48 = new A.Paragraph();

			A.Run run26 = new A.Run();
			A.RunProperties runProperties40 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text40 = new A.Text();
			text40.Text = "Click to edit Master title style";

			run26.Append(runProperties40);
			run26.Append(text40);
			A.EndParagraphRunProperties endParagraphRunProperties34 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph48.Append(run26);
			paragraph48.Append(endParagraphRunProperties34);

			textBody36.Append(bodyProperties36);
			textBody36.Append(listStyle36);
			textBody36.Append(paragraph48);

			shape36.Append(nonVisualShapeProperties36);
			shape36.Append(shapeProperties37);
			shape36.Append(textBody36);

			Shape shape37 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties37 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties47 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Vertical Text Placeholder 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties37 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks37 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties37.Append(shapeLocks37);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties47 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape37 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Orientation = DirectionValues.Vertical, Index = (UInt32Value)1U };

			applicationNonVisualDrawingProperties47.Append(placeholderShape37);

			nonVisualShapeProperties37.Append(nonVisualDrawingProperties47);
			nonVisualShapeProperties37.Append(nonVisualShapeDrawingProperties37);
			nonVisualShapeProperties37.Append(applicationNonVisualDrawingProperties47);

			ShapeProperties shapeProperties38 = new ShapeProperties();

			A.Transform2D transform2D15 = new A.Transform2D();
			A.Offset offset24 = new A.Offset(){ X = 457200L, Y = 274638L };
			A.Extents extents24 = new A.Extents(){ Cx = 6019800L, Cy = 5851525L };

			transform2D15.Append(offset24);
			transform2D15.Append(extents24);

			shapeProperties38.Append(transform2D15);

			TextBody textBody37 = new TextBody();
			A.BodyProperties bodyProperties37 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.EastAsianVetical };
			A.ListStyle listStyle37 = new A.ListStyle();

			A.Paragraph paragraph49 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties18 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run27 = new A.Run();
			A.RunProperties runProperties41 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text41 = new A.Text();
			text41.Text = "Click to edit Master text styles";

			run27.Append(runProperties41);
			run27.Append(text41);

			paragraph49.Append(paragraphProperties18);
			paragraph49.Append(run27);

			A.Paragraph paragraph50 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties19 = new A.ParagraphProperties(){ Level = 1 };

			A.Run run28 = new A.Run();
			A.RunProperties runProperties42 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text42 = new A.Text();
			text42.Text = "Second level";

			run28.Append(runProperties42);
			run28.Append(text42);

			paragraph50.Append(paragraphProperties19);
			paragraph50.Append(run28);

			A.Paragraph paragraph51 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties20 = new A.ParagraphProperties(){ Level = 2 };

			A.Run run29 = new A.Run();
			A.RunProperties runProperties43 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text43 = new A.Text();
			text43.Text = "Third level";

			run29.Append(runProperties43);
			run29.Append(text43);

			paragraph51.Append(paragraphProperties20);
			paragraph51.Append(run29);

			A.Paragraph paragraph52 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties21 = new A.ParagraphProperties(){ Level = 3 };

			A.Run run30 = new A.Run();
			A.RunProperties runProperties44 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text44 = new A.Text();
			text44.Text = "Fourth level";

			run30.Append(runProperties44);
			run30.Append(text44);

			paragraph52.Append(paragraphProperties21);
			paragraph52.Append(run30);

			A.Paragraph paragraph53 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties22 = new A.ParagraphProperties(){ Level = 4 };

			A.Run run31 = new A.Run();
			A.RunProperties runProperties45 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text45 = new A.Text();
			text45.Text = "Fifth level";

			run31.Append(runProperties45);
			run31.Append(text45);
			A.EndParagraphRunProperties endParagraphRunProperties35 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph53.Append(paragraphProperties22);
			paragraph53.Append(run31);
			paragraph53.Append(endParagraphRunProperties35);

			textBody37.Append(bodyProperties37);
			textBody37.Append(listStyle37);
			textBody37.Append(paragraph49);
			textBody37.Append(paragraph50);
			textBody37.Append(paragraph51);
			textBody37.Append(paragraph52);
			textBody37.Append(paragraph53);

			shape37.Append(nonVisualShapeProperties37);
			shape37.Append(shapeProperties38);
			shape37.Append(textBody37);

			Shape shape38 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties38 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties48 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Date Placeholder 3" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties38 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks38 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties38.Append(shapeLocks38);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties48 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape38 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)10U };

			applicationNonVisualDrawingProperties48.Append(placeholderShape38);

			nonVisualShapeProperties38.Append(nonVisualDrawingProperties48);
			nonVisualShapeProperties38.Append(nonVisualShapeDrawingProperties38);
			nonVisualShapeProperties38.Append(applicationNonVisualDrawingProperties48);
			ShapeProperties shapeProperties39 = new ShapeProperties();

			TextBody textBody38 = new TextBody();
			A.BodyProperties bodyProperties38 = new A.BodyProperties();
			A.ListStyle listStyle38 = new A.ListStyle();

			A.Paragraph paragraph54 = new A.Paragraph();

			A.Field field15 = new A.Field(){ Id = "{6A870D7B-DCE0-46E8-B448-2F0E4E8E28D1}", Type = "datetimeFigureOut" };
			A.RunProperties runProperties46 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text46 = new A.Text();
			text46.Text = "3/28/2013";

			field15.Append(runProperties46);
			field15.Append(text46);
			A.EndParagraphRunProperties endParagraphRunProperties36 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph54.Append(field15);
			paragraph54.Append(endParagraphRunProperties36);

			textBody38.Append(bodyProperties38);
			textBody38.Append(listStyle38);
			textBody38.Append(paragraph54);

			shape38.Append(nonVisualShapeProperties38);
			shape38.Append(shapeProperties39);
			shape38.Append(textBody38);

			Shape shape39 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties39 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties49 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Footer Placeholder 4" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties39 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks39 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties39.Append(shapeLocks39);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties49 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape39 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

			applicationNonVisualDrawingProperties49.Append(placeholderShape39);

			nonVisualShapeProperties39.Append(nonVisualDrawingProperties49);
			nonVisualShapeProperties39.Append(nonVisualShapeDrawingProperties39);
			nonVisualShapeProperties39.Append(applicationNonVisualDrawingProperties49);
			ShapeProperties shapeProperties40 = new ShapeProperties();

			TextBody textBody39 = new TextBody();
			A.BodyProperties bodyProperties39 = new A.BodyProperties();
			A.ListStyle listStyle39 = new A.ListStyle();

			A.Paragraph paragraph55 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties37 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph55.Append(endParagraphRunProperties37);

			textBody39.Append(bodyProperties39);
			textBody39.Append(listStyle39);
			textBody39.Append(paragraph55);

			shape39.Append(nonVisualShapeProperties39);
			shape39.Append(shapeProperties40);
			shape39.Append(textBody39);

			Shape shape40 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties40 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties50 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Slide Number Placeholder 5" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties40 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks40 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties40.Append(shapeLocks40);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties50 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape40 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

			applicationNonVisualDrawingProperties50.Append(placeholderShape40);

			nonVisualShapeProperties40.Append(nonVisualDrawingProperties50);
			nonVisualShapeProperties40.Append(nonVisualShapeDrawingProperties40);
			nonVisualShapeProperties40.Append(applicationNonVisualDrawingProperties50);
			ShapeProperties shapeProperties41 = new ShapeProperties();

			TextBody textBody40 = new TextBody();
			A.BodyProperties bodyProperties40 = new A.BodyProperties();
			A.ListStyle listStyle40 = new A.ListStyle();

			A.Paragraph paragraph56 = new A.Paragraph();

			A.Field field16 = new A.Field(){ Id = "{AFBBC1CF-DB04-427F-9A30-B7E1FB44F3B5}", Type = "slidenum" };
			A.RunProperties runProperties47 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text47 = new A.Text();
			text47.Text = "‹#›";

			field16.Append(runProperties47);
			field16.Append(text47);
			A.EndParagraphRunProperties endParagraphRunProperties38 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph56.Append(field16);
			paragraph56.Append(endParagraphRunProperties38);

			textBody40.Append(bodyProperties40);
			textBody40.Append(listStyle40);
			textBody40.Append(paragraph56);

			shape40.Append(nonVisualShapeProperties40);
			shape40.Append(shapeProperties41);
			shape40.Append(textBody40);

			shapeTree9.Append(nonVisualGroupShapeProperties9);
			shapeTree9.Append(groupShapeProperties9);
			shapeTree9.Append(shape36);
			shapeTree9.Append(shape37);
			shapeTree9.Append(shape38);
			shapeTree9.Append(shape39);
			shapeTree9.Append(shape40);

			commonSlideData9.Append(shapeTree9);

			ColorMapOverride colorMapOverride8 = new ColorMapOverride();
			A.MasterColorMapping masterColorMapping8 = new A.MasterColorMapping();

			colorMapOverride8.Append(masterColorMapping8);

			slideLayout7.Append(commonSlideData9);
			slideLayout7.Append(colorMapOverride8);

			slideLayoutPart7.SlideLayout = slideLayout7;
		}

		// Generates content of slideLayoutPart8.
		private void GenerateSlideLayoutPart8Content(SlideLayoutPart slideLayoutPart8)
		{
			SlideLayout slideLayout8 = new SlideLayout(){ Type = SlideLayoutValues.TwoTextAndTwoObjects, Preserve = true };
			slideLayout8.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slideLayout8.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slideLayout8.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData10 = new CommonSlideData(){ Name = "Comparison" };

			ShapeTree shapeTree10 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties10 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties51 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties10 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties51 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties10.Append(nonVisualDrawingProperties51);
			nonVisualGroupShapeProperties10.Append(nonVisualGroupShapeDrawingProperties10);
			nonVisualGroupShapeProperties10.Append(applicationNonVisualDrawingProperties51);

			GroupShapeProperties groupShapeProperties10 = new GroupShapeProperties();

			A.TransformGroup transformGroup10 = new A.TransformGroup();
			A.Offset offset25 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents25 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset10 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents10 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup10.Append(offset25);
			transformGroup10.Append(extents25);
			transformGroup10.Append(childOffset10);
			transformGroup10.Append(childExtents10);

			groupShapeProperties10.Append(transformGroup10);

			Shape shape41 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties41 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties52 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties41 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks41 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties41.Append(shapeLocks41);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties52 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape41 = new PlaceholderShape(){ Type = PlaceholderValues.Title };

			applicationNonVisualDrawingProperties52.Append(placeholderShape41);

			nonVisualShapeProperties41.Append(nonVisualDrawingProperties52);
			nonVisualShapeProperties41.Append(nonVisualShapeDrawingProperties41);
			nonVisualShapeProperties41.Append(applicationNonVisualDrawingProperties52);
			ShapeProperties shapeProperties42 = new ShapeProperties();

			TextBody textBody41 = new TextBody();
			A.BodyProperties bodyProperties41 = new A.BodyProperties();

			A.ListStyle listStyle41 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties14 = new A.Level1ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties72 = new A.DefaultRunProperties();

			level1ParagraphProperties14.Append(defaultRunProperties72);

			listStyle41.Append(level1ParagraphProperties14);

			A.Paragraph paragraph57 = new A.Paragraph();

			A.Run run32 = new A.Run();
			A.RunProperties runProperties48 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text48 = new A.Text();
			text48.Text = "Click to edit Master title style";

			run32.Append(runProperties48);
			run32.Append(text48);
			A.EndParagraphRunProperties endParagraphRunProperties39 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph57.Append(run32);
			paragraph57.Append(endParagraphRunProperties39);

			textBody41.Append(bodyProperties41);
			textBody41.Append(listStyle41);
			textBody41.Append(paragraph57);

			shape41.Append(nonVisualShapeProperties41);
			shape41.Append(shapeProperties42);
			shape41.Append(textBody41);

			Shape shape42 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties42 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties53 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Text Placeholder 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties42 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks42 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties42.Append(shapeLocks42);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties53 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape42 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Index = (UInt32Value)1U };

			applicationNonVisualDrawingProperties53.Append(placeholderShape42);

			nonVisualShapeProperties42.Append(nonVisualDrawingProperties53);
			nonVisualShapeProperties42.Append(nonVisualShapeDrawingProperties42);
			nonVisualShapeProperties42.Append(applicationNonVisualDrawingProperties53);

			ShapeProperties shapeProperties43 = new ShapeProperties();

			A.Transform2D transform2D16 = new A.Transform2D();
			A.Offset offset26 = new A.Offset(){ X = 457200L, Y = 1535113L };
			A.Extents extents26 = new A.Extents(){ Cx = 4040188L, Cy = 639762L };

			transform2D16.Append(offset26);
			transform2D16.Append(extents26);

			shapeProperties43.Append(transform2D16);

			TextBody textBody42 = new TextBody();
			A.BodyProperties bodyProperties42 = new A.BodyProperties(){ Anchor = A.TextAnchoringTypeValues.Bottom };

			A.ListStyle listStyle42 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties15 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Indent = 0 };
			A.NoBullet noBullet29 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties73 = new A.DefaultRunProperties(){ FontSize = 2400, Bold = true };

			level1ParagraphProperties15.Append(noBullet29);
			level1ParagraphProperties15.Append(defaultRunProperties73);

			A.Level2ParagraphProperties level2ParagraphProperties8 = new A.Level2ParagraphProperties(){ LeftMargin = 457200, Indent = 0 };
			A.NoBullet noBullet30 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties74 = new A.DefaultRunProperties(){ FontSize = 2000, Bold = true };

			level2ParagraphProperties8.Append(noBullet30);
			level2ParagraphProperties8.Append(defaultRunProperties74);

			A.Level3ParagraphProperties level3ParagraphProperties8 = new A.Level3ParagraphProperties(){ LeftMargin = 914400, Indent = 0 };
			A.NoBullet noBullet31 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties75 = new A.DefaultRunProperties(){ FontSize = 1800, Bold = true };

			level3ParagraphProperties8.Append(noBullet31);
			level3ParagraphProperties8.Append(defaultRunProperties75);

			A.Level4ParagraphProperties level4ParagraphProperties8 = new A.Level4ParagraphProperties(){ LeftMargin = 1371600, Indent = 0 };
			A.NoBullet noBullet32 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties76 = new A.DefaultRunProperties(){ FontSize = 1600, Bold = true };

			level4ParagraphProperties8.Append(noBullet32);
			level4ParagraphProperties8.Append(defaultRunProperties76);

			A.Level5ParagraphProperties level5ParagraphProperties8 = new A.Level5ParagraphProperties(){ LeftMargin = 1828800, Indent = 0 };
			A.NoBullet noBullet33 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties77 = new A.DefaultRunProperties(){ FontSize = 1600, Bold = true };

			level5ParagraphProperties8.Append(noBullet33);
			level5ParagraphProperties8.Append(defaultRunProperties77);

			A.Level6ParagraphProperties level6ParagraphProperties8 = new A.Level6ParagraphProperties(){ LeftMargin = 2286000, Indent = 0 };
			A.NoBullet noBullet34 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties78 = new A.DefaultRunProperties(){ FontSize = 1600, Bold = true };

			level6ParagraphProperties8.Append(noBullet34);
			level6ParagraphProperties8.Append(defaultRunProperties78);

			A.Level7ParagraphProperties level7ParagraphProperties8 = new A.Level7ParagraphProperties(){ LeftMargin = 2743200, Indent = 0 };
			A.NoBullet noBullet35 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties79 = new A.DefaultRunProperties(){ FontSize = 1600, Bold = true };

			level7ParagraphProperties8.Append(noBullet35);
			level7ParagraphProperties8.Append(defaultRunProperties79);

			A.Level8ParagraphProperties level8ParagraphProperties8 = new A.Level8ParagraphProperties(){ LeftMargin = 3200400, Indent = 0 };
			A.NoBullet noBullet36 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties80 = new A.DefaultRunProperties(){ FontSize = 1600, Bold = true };

			level8ParagraphProperties8.Append(noBullet36);
			level8ParagraphProperties8.Append(defaultRunProperties80);

			A.Level9ParagraphProperties level9ParagraphProperties8 = new A.Level9ParagraphProperties(){ LeftMargin = 3657600, Indent = 0 };
			A.NoBullet noBullet37 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties81 = new A.DefaultRunProperties(){ FontSize = 1600, Bold = true };

			level9ParagraphProperties8.Append(noBullet37);
			level9ParagraphProperties8.Append(defaultRunProperties81);

			listStyle42.Append(level1ParagraphProperties15);
			listStyle42.Append(level2ParagraphProperties8);
			listStyle42.Append(level3ParagraphProperties8);
			listStyle42.Append(level4ParagraphProperties8);
			listStyle42.Append(level5ParagraphProperties8);
			listStyle42.Append(level6ParagraphProperties8);
			listStyle42.Append(level7ParagraphProperties8);
			listStyle42.Append(level8ParagraphProperties8);
			listStyle42.Append(level9ParagraphProperties8);

			A.Paragraph paragraph58 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties23 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run33 = new A.Run();
			A.RunProperties runProperties49 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text49 = new A.Text();
			text49.Text = "Click to edit Master text styles";

			run33.Append(runProperties49);
			run33.Append(text49);

			paragraph58.Append(paragraphProperties23);
			paragraph58.Append(run33);

			textBody42.Append(bodyProperties42);
			textBody42.Append(listStyle42);
			textBody42.Append(paragraph58);

			shape42.Append(nonVisualShapeProperties42);
			shape42.Append(shapeProperties43);
			shape42.Append(textBody42);

			Shape shape43 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties43 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties54 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Content Placeholder 3" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties43 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks43 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties43.Append(shapeLocks43);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties54 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape43 = new PlaceholderShape(){ Size = PlaceholderSizeValues.Half, Index = (UInt32Value)2U };

			applicationNonVisualDrawingProperties54.Append(placeholderShape43);

			nonVisualShapeProperties43.Append(nonVisualDrawingProperties54);
			nonVisualShapeProperties43.Append(nonVisualShapeDrawingProperties43);
			nonVisualShapeProperties43.Append(applicationNonVisualDrawingProperties54);

			ShapeProperties shapeProperties44 = new ShapeProperties();

			A.Transform2D transform2D17 = new A.Transform2D();
			A.Offset offset27 = new A.Offset(){ X = 457200L, Y = 2174875L };
			A.Extents extents27 = new A.Extents(){ Cx = 4040188L, Cy = 3951288L };

			transform2D17.Append(offset27);
			transform2D17.Append(extents27);

			shapeProperties44.Append(transform2D17);

			TextBody textBody43 = new TextBody();
			A.BodyProperties bodyProperties43 = new A.BodyProperties();

			A.ListStyle listStyle43 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties16 = new A.Level1ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties82 = new A.DefaultRunProperties(){ FontSize = 2400 };

			level1ParagraphProperties16.Append(defaultRunProperties82);

			A.Level2ParagraphProperties level2ParagraphProperties9 = new A.Level2ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties83 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level2ParagraphProperties9.Append(defaultRunProperties83);

			A.Level3ParagraphProperties level3ParagraphProperties9 = new A.Level3ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties84 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level3ParagraphProperties9.Append(defaultRunProperties84);

			A.Level4ParagraphProperties level4ParagraphProperties9 = new A.Level4ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties85 = new A.DefaultRunProperties(){ FontSize = 1600 };

			level4ParagraphProperties9.Append(defaultRunProperties85);

			A.Level5ParagraphProperties level5ParagraphProperties9 = new A.Level5ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties86 = new A.DefaultRunProperties(){ FontSize = 1600 };

			level5ParagraphProperties9.Append(defaultRunProperties86);

			A.Level6ParagraphProperties level6ParagraphProperties9 = new A.Level6ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties87 = new A.DefaultRunProperties(){ FontSize = 1600 };

			level6ParagraphProperties9.Append(defaultRunProperties87);

			A.Level7ParagraphProperties level7ParagraphProperties9 = new A.Level7ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties88 = new A.DefaultRunProperties(){ FontSize = 1600 };

			level7ParagraphProperties9.Append(defaultRunProperties88);

			A.Level8ParagraphProperties level8ParagraphProperties9 = new A.Level8ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties89 = new A.DefaultRunProperties(){ FontSize = 1600 };

			level8ParagraphProperties9.Append(defaultRunProperties89);

			A.Level9ParagraphProperties level9ParagraphProperties9 = new A.Level9ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties90 = new A.DefaultRunProperties(){ FontSize = 1600 };

			level9ParagraphProperties9.Append(defaultRunProperties90);

			listStyle43.Append(level1ParagraphProperties16);
			listStyle43.Append(level2ParagraphProperties9);
			listStyle43.Append(level3ParagraphProperties9);
			listStyle43.Append(level4ParagraphProperties9);
			listStyle43.Append(level5ParagraphProperties9);
			listStyle43.Append(level6ParagraphProperties9);
			listStyle43.Append(level7ParagraphProperties9);
			listStyle43.Append(level8ParagraphProperties9);
			listStyle43.Append(level9ParagraphProperties9);

			A.Paragraph paragraph59 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties24 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run34 = new A.Run();
			A.RunProperties runProperties50 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text50 = new A.Text();
			text50.Text = "Click to edit Master text styles";

			run34.Append(runProperties50);
			run34.Append(text50);

			paragraph59.Append(paragraphProperties24);
			paragraph59.Append(run34);

			A.Paragraph paragraph60 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties25 = new A.ParagraphProperties(){ Level = 1 };

			A.Run run35 = new A.Run();
			A.RunProperties runProperties51 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text51 = new A.Text();
			text51.Text = "Second level";

			run35.Append(runProperties51);
			run35.Append(text51);

			paragraph60.Append(paragraphProperties25);
			paragraph60.Append(run35);

			A.Paragraph paragraph61 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties26 = new A.ParagraphProperties(){ Level = 2 };

			A.Run run36 = new A.Run();
			A.RunProperties runProperties52 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text52 = new A.Text();
			text52.Text = "Third level";

			run36.Append(runProperties52);
			run36.Append(text52);

			paragraph61.Append(paragraphProperties26);
			paragraph61.Append(run36);

			A.Paragraph paragraph62 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties27 = new A.ParagraphProperties(){ Level = 3 };

			A.Run run37 = new A.Run();
			A.RunProperties runProperties53 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text53 = new A.Text();
			text53.Text = "Fourth level";

			run37.Append(runProperties53);
			run37.Append(text53);

			paragraph62.Append(paragraphProperties27);
			paragraph62.Append(run37);

			A.Paragraph paragraph63 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties28 = new A.ParagraphProperties(){ Level = 4 };

			A.Run run38 = new A.Run();
			A.RunProperties runProperties54 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text54 = new A.Text();
			text54.Text = "Fifth level";

			run38.Append(runProperties54);
			run38.Append(text54);
			A.EndParagraphRunProperties endParagraphRunProperties40 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph63.Append(paragraphProperties28);
			paragraph63.Append(run38);
			paragraph63.Append(endParagraphRunProperties40);

			textBody43.Append(bodyProperties43);
			textBody43.Append(listStyle43);
			textBody43.Append(paragraph59);
			textBody43.Append(paragraph60);
			textBody43.Append(paragraph61);
			textBody43.Append(paragraph62);
			textBody43.Append(paragraph63);

			shape43.Append(nonVisualShapeProperties43);
			shape43.Append(shapeProperties44);
			shape43.Append(textBody43);

			Shape shape44 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties44 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties55 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Text Placeholder 4" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties44 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks44 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties44.Append(shapeLocks44);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties55 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape44 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)3U };

			applicationNonVisualDrawingProperties55.Append(placeholderShape44);

			nonVisualShapeProperties44.Append(nonVisualDrawingProperties55);
			nonVisualShapeProperties44.Append(nonVisualShapeDrawingProperties44);
			nonVisualShapeProperties44.Append(applicationNonVisualDrawingProperties55);

			ShapeProperties shapeProperties45 = new ShapeProperties();

			A.Transform2D transform2D18 = new A.Transform2D();
			A.Offset offset28 = new A.Offset(){ X = 4645025L, Y = 1535113L };
			A.Extents extents28 = new A.Extents(){ Cx = 4041775L, Cy = 639762L };

			transform2D18.Append(offset28);
			transform2D18.Append(extents28);

			shapeProperties45.Append(transform2D18);

			TextBody textBody44 = new TextBody();
			A.BodyProperties bodyProperties44 = new A.BodyProperties(){ Anchor = A.TextAnchoringTypeValues.Bottom };

			A.ListStyle listStyle44 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties17 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Indent = 0 };
			A.NoBullet noBullet38 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties91 = new A.DefaultRunProperties(){ FontSize = 2400, Bold = true };

			level1ParagraphProperties17.Append(noBullet38);
			level1ParagraphProperties17.Append(defaultRunProperties91);

			A.Level2ParagraphProperties level2ParagraphProperties10 = new A.Level2ParagraphProperties(){ LeftMargin = 457200, Indent = 0 };
			A.NoBullet noBullet39 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties92 = new A.DefaultRunProperties(){ FontSize = 2000, Bold = true };

			level2ParagraphProperties10.Append(noBullet39);
			level2ParagraphProperties10.Append(defaultRunProperties92);

			A.Level3ParagraphProperties level3ParagraphProperties10 = new A.Level3ParagraphProperties(){ LeftMargin = 914400, Indent = 0 };
			A.NoBullet noBullet40 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties93 = new A.DefaultRunProperties(){ FontSize = 1800, Bold = true };

			level3ParagraphProperties10.Append(noBullet40);
			level3ParagraphProperties10.Append(defaultRunProperties93);

			A.Level4ParagraphProperties level4ParagraphProperties10 = new A.Level4ParagraphProperties(){ LeftMargin = 1371600, Indent = 0 };
			A.NoBullet noBullet41 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties94 = new A.DefaultRunProperties(){ FontSize = 1600, Bold = true };

			level4ParagraphProperties10.Append(noBullet41);
			level4ParagraphProperties10.Append(defaultRunProperties94);

			A.Level5ParagraphProperties level5ParagraphProperties10 = new A.Level5ParagraphProperties(){ LeftMargin = 1828800, Indent = 0 };
			A.NoBullet noBullet42 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties95 = new A.DefaultRunProperties(){ FontSize = 1600, Bold = true };

			level5ParagraphProperties10.Append(noBullet42);
			level5ParagraphProperties10.Append(defaultRunProperties95);

			A.Level6ParagraphProperties level6ParagraphProperties10 = new A.Level6ParagraphProperties(){ LeftMargin = 2286000, Indent = 0 };
			A.NoBullet noBullet43 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties96 = new A.DefaultRunProperties(){ FontSize = 1600, Bold = true };

			level6ParagraphProperties10.Append(noBullet43);
			level6ParagraphProperties10.Append(defaultRunProperties96);

			A.Level7ParagraphProperties level7ParagraphProperties10 = new A.Level7ParagraphProperties(){ LeftMargin = 2743200, Indent = 0 };
			A.NoBullet noBullet44 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties97 = new A.DefaultRunProperties(){ FontSize = 1600, Bold = true };

			level7ParagraphProperties10.Append(noBullet44);
			level7ParagraphProperties10.Append(defaultRunProperties97);

			A.Level8ParagraphProperties level8ParagraphProperties10 = new A.Level8ParagraphProperties(){ LeftMargin = 3200400, Indent = 0 };
			A.NoBullet noBullet45 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties98 = new A.DefaultRunProperties(){ FontSize = 1600, Bold = true };

			level8ParagraphProperties10.Append(noBullet45);
			level8ParagraphProperties10.Append(defaultRunProperties98);

			A.Level9ParagraphProperties level9ParagraphProperties10 = new A.Level9ParagraphProperties(){ LeftMargin = 3657600, Indent = 0 };
			A.NoBullet noBullet46 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties99 = new A.DefaultRunProperties(){ FontSize = 1600, Bold = true };

			level9ParagraphProperties10.Append(noBullet46);
			level9ParagraphProperties10.Append(defaultRunProperties99);

			listStyle44.Append(level1ParagraphProperties17);
			listStyle44.Append(level2ParagraphProperties10);
			listStyle44.Append(level3ParagraphProperties10);
			listStyle44.Append(level4ParagraphProperties10);
			listStyle44.Append(level5ParagraphProperties10);
			listStyle44.Append(level6ParagraphProperties10);
			listStyle44.Append(level7ParagraphProperties10);
			listStyle44.Append(level8ParagraphProperties10);
			listStyle44.Append(level9ParagraphProperties10);

			A.Paragraph paragraph64 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties29 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run39 = new A.Run();
			A.RunProperties runProperties55 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text55 = new A.Text();
			text55.Text = "Click to edit Master text styles";

			run39.Append(runProperties55);
			run39.Append(text55);

			paragraph64.Append(paragraphProperties29);
			paragraph64.Append(run39);

			textBody44.Append(bodyProperties44);
			textBody44.Append(listStyle44);
			textBody44.Append(paragraph64);

			shape44.Append(nonVisualShapeProperties44);
			shape44.Append(shapeProperties45);
			shape44.Append(textBody44);

			Shape shape45 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties45 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties56 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Content Placeholder 5" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties45 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks45 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties45.Append(shapeLocks45);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties56 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape45 = new PlaceholderShape(){ Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)4U };

			applicationNonVisualDrawingProperties56.Append(placeholderShape45);

			nonVisualShapeProperties45.Append(nonVisualDrawingProperties56);
			nonVisualShapeProperties45.Append(nonVisualShapeDrawingProperties45);
			nonVisualShapeProperties45.Append(applicationNonVisualDrawingProperties56);

			ShapeProperties shapeProperties46 = new ShapeProperties();

			A.Transform2D transform2D19 = new A.Transform2D();
			A.Offset offset29 = new A.Offset(){ X = 4645025L, Y = 2174875L };
			A.Extents extents29 = new A.Extents(){ Cx = 4041775L, Cy = 3951288L };

			transform2D19.Append(offset29);
			transform2D19.Append(extents29);

			shapeProperties46.Append(transform2D19);

			TextBody textBody45 = new TextBody();
			A.BodyProperties bodyProperties45 = new A.BodyProperties();

			A.ListStyle listStyle45 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties18 = new A.Level1ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties100 = new A.DefaultRunProperties(){ FontSize = 2400 };

			level1ParagraphProperties18.Append(defaultRunProperties100);

			A.Level2ParagraphProperties level2ParagraphProperties11 = new A.Level2ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties101 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level2ParagraphProperties11.Append(defaultRunProperties101);

			A.Level3ParagraphProperties level3ParagraphProperties11 = new A.Level3ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties102 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level3ParagraphProperties11.Append(defaultRunProperties102);

			A.Level4ParagraphProperties level4ParagraphProperties11 = new A.Level4ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties103 = new A.DefaultRunProperties(){ FontSize = 1600 };

			level4ParagraphProperties11.Append(defaultRunProperties103);

			A.Level5ParagraphProperties level5ParagraphProperties11 = new A.Level5ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties104 = new A.DefaultRunProperties(){ FontSize = 1600 };

			level5ParagraphProperties11.Append(defaultRunProperties104);

			A.Level6ParagraphProperties level6ParagraphProperties11 = new A.Level6ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties105 = new A.DefaultRunProperties(){ FontSize = 1600 };

			level6ParagraphProperties11.Append(defaultRunProperties105);

			A.Level7ParagraphProperties level7ParagraphProperties11 = new A.Level7ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties106 = new A.DefaultRunProperties(){ FontSize = 1600 };

			level7ParagraphProperties11.Append(defaultRunProperties106);

			A.Level8ParagraphProperties level8ParagraphProperties11 = new A.Level8ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties107 = new A.DefaultRunProperties(){ FontSize = 1600 };

			level8ParagraphProperties11.Append(defaultRunProperties107);

			A.Level9ParagraphProperties level9ParagraphProperties11 = new A.Level9ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties108 = new A.DefaultRunProperties(){ FontSize = 1600 };

			level9ParagraphProperties11.Append(defaultRunProperties108);

			listStyle45.Append(level1ParagraphProperties18);
			listStyle45.Append(level2ParagraphProperties11);
			listStyle45.Append(level3ParagraphProperties11);
			listStyle45.Append(level4ParagraphProperties11);
			listStyle45.Append(level5ParagraphProperties11);
			listStyle45.Append(level6ParagraphProperties11);
			listStyle45.Append(level7ParagraphProperties11);
			listStyle45.Append(level8ParagraphProperties11);
			listStyle45.Append(level9ParagraphProperties11);

			A.Paragraph paragraph65 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties30 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run40 = new A.Run();
			A.RunProperties runProperties56 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text56 = new A.Text();
			text56.Text = "Click to edit Master text styles";

			run40.Append(runProperties56);
			run40.Append(text56);

			paragraph65.Append(paragraphProperties30);
			paragraph65.Append(run40);

			A.Paragraph paragraph66 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties31 = new A.ParagraphProperties(){ Level = 1 };

			A.Run run41 = new A.Run();
			A.RunProperties runProperties57 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text57 = new A.Text();
			text57.Text = "Second level";

			run41.Append(runProperties57);
			run41.Append(text57);

			paragraph66.Append(paragraphProperties31);
			paragraph66.Append(run41);

			A.Paragraph paragraph67 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties32 = new A.ParagraphProperties(){ Level = 2 };

			A.Run run42 = new A.Run();
			A.RunProperties runProperties58 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text58 = new A.Text();
			text58.Text = "Third level";

			run42.Append(runProperties58);
			run42.Append(text58);

			paragraph67.Append(paragraphProperties32);
			paragraph67.Append(run42);

			A.Paragraph paragraph68 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties33 = new A.ParagraphProperties(){ Level = 3 };

			A.Run run43 = new A.Run();
			A.RunProperties runProperties59 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text59 = new A.Text();
			text59.Text = "Fourth level";

			run43.Append(runProperties59);
			run43.Append(text59);

			paragraph68.Append(paragraphProperties33);
			paragraph68.Append(run43);

			A.Paragraph paragraph69 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties34 = new A.ParagraphProperties(){ Level = 4 };

			A.Run run44 = new A.Run();
			A.RunProperties runProperties60 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text60 = new A.Text();
			text60.Text = "Fifth level";

			run44.Append(runProperties60);
			run44.Append(text60);
			A.EndParagraphRunProperties endParagraphRunProperties41 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph69.Append(paragraphProperties34);
			paragraph69.Append(run44);
			paragraph69.Append(endParagraphRunProperties41);

			textBody45.Append(bodyProperties45);
			textBody45.Append(listStyle45);
			textBody45.Append(paragraph65);
			textBody45.Append(paragraph66);
			textBody45.Append(paragraph67);
			textBody45.Append(paragraph68);
			textBody45.Append(paragraph69);

			shape45.Append(nonVisualShapeProperties45);
			shape45.Append(shapeProperties46);
			shape45.Append(textBody45);

			Shape shape46 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties46 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties57 = new NonVisualDrawingProperties(){ Id = (UInt32Value)7U, Name = "Date Placeholder 6" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties46 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks46 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties46.Append(shapeLocks46);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties57 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape46 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)10U };

			applicationNonVisualDrawingProperties57.Append(placeholderShape46);

			nonVisualShapeProperties46.Append(nonVisualDrawingProperties57);
			nonVisualShapeProperties46.Append(nonVisualShapeDrawingProperties46);
			nonVisualShapeProperties46.Append(applicationNonVisualDrawingProperties57);
			ShapeProperties shapeProperties47 = new ShapeProperties();

			TextBody textBody46 = new TextBody();
			A.BodyProperties bodyProperties46 = new A.BodyProperties();
			A.ListStyle listStyle46 = new A.ListStyle();

			A.Paragraph paragraph70 = new A.Paragraph();

			A.Field field17 = new A.Field(){ Id = "{6A870D7B-DCE0-46E8-B448-2F0E4E8E28D1}", Type = "datetimeFigureOut" };
			A.RunProperties runProperties61 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text61 = new A.Text();
			text61.Text = "3/28/2013";

			field17.Append(runProperties61);
			field17.Append(text61);
			A.EndParagraphRunProperties endParagraphRunProperties42 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph70.Append(field17);
			paragraph70.Append(endParagraphRunProperties42);

			textBody46.Append(bodyProperties46);
			textBody46.Append(listStyle46);
			textBody46.Append(paragraph70);

			shape46.Append(nonVisualShapeProperties46);
			shape46.Append(shapeProperties47);
			shape46.Append(textBody46);

			Shape shape47 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties47 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties58 = new NonVisualDrawingProperties(){ Id = (UInt32Value)8U, Name = "Footer Placeholder 7" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties47 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks47 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties47.Append(shapeLocks47);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties58 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape47 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

			applicationNonVisualDrawingProperties58.Append(placeholderShape47);

			nonVisualShapeProperties47.Append(nonVisualDrawingProperties58);
			nonVisualShapeProperties47.Append(nonVisualShapeDrawingProperties47);
			nonVisualShapeProperties47.Append(applicationNonVisualDrawingProperties58);
			ShapeProperties shapeProperties48 = new ShapeProperties();

			TextBody textBody47 = new TextBody();
			A.BodyProperties bodyProperties47 = new A.BodyProperties();
			A.ListStyle listStyle47 = new A.ListStyle();

			A.Paragraph paragraph71 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties43 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph71.Append(endParagraphRunProperties43);

			textBody47.Append(bodyProperties47);
			textBody47.Append(listStyle47);
			textBody47.Append(paragraph71);

			shape47.Append(nonVisualShapeProperties47);
			shape47.Append(shapeProperties48);
			shape47.Append(textBody47);

			Shape shape48 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties48 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties59 = new NonVisualDrawingProperties(){ Id = (UInt32Value)9U, Name = "Slide Number Placeholder 8" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties48 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks48 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties48.Append(shapeLocks48);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties59 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape48 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

			applicationNonVisualDrawingProperties59.Append(placeholderShape48);

			nonVisualShapeProperties48.Append(nonVisualDrawingProperties59);
			nonVisualShapeProperties48.Append(nonVisualShapeDrawingProperties48);
			nonVisualShapeProperties48.Append(applicationNonVisualDrawingProperties59);
			ShapeProperties shapeProperties49 = new ShapeProperties();

			TextBody textBody48 = new TextBody();
			A.BodyProperties bodyProperties48 = new A.BodyProperties();
			A.ListStyle listStyle48 = new A.ListStyle();

			A.Paragraph paragraph72 = new A.Paragraph();

			A.Field field18 = new A.Field(){ Id = "{AFBBC1CF-DB04-427F-9A30-B7E1FB44F3B5}", Type = "slidenum" };
			A.RunProperties runProperties62 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text62 = new A.Text();
			text62.Text = "‹#›";

			field18.Append(runProperties62);
			field18.Append(text62);
			A.EndParagraphRunProperties endParagraphRunProperties44 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph72.Append(field18);
			paragraph72.Append(endParagraphRunProperties44);

			textBody48.Append(bodyProperties48);
			textBody48.Append(listStyle48);
			textBody48.Append(paragraph72);

			shape48.Append(nonVisualShapeProperties48);
			shape48.Append(shapeProperties49);
			shape48.Append(textBody48);

			shapeTree10.Append(nonVisualGroupShapeProperties10);
			shapeTree10.Append(groupShapeProperties10);
			shapeTree10.Append(shape41);
			shapeTree10.Append(shape42);
			shapeTree10.Append(shape43);
			shapeTree10.Append(shape44);
			shapeTree10.Append(shape45);
			shapeTree10.Append(shape46);
			shapeTree10.Append(shape47);
			shapeTree10.Append(shape48);

			commonSlideData10.Append(shapeTree10);

			ColorMapOverride colorMapOverride9 = new ColorMapOverride();
			A.MasterColorMapping masterColorMapping9 = new A.MasterColorMapping();

			colorMapOverride9.Append(masterColorMapping9);

			slideLayout8.Append(commonSlideData10);
			slideLayout8.Append(colorMapOverride9);

			slideLayoutPart8.SlideLayout = slideLayout8;
		}

		// Generates content of slideLayoutPart9.
		private void GenerateSlideLayoutPart9Content(SlideLayoutPart slideLayoutPart9)
		{
			SlideLayout slideLayout9 = new SlideLayout(){ Type = SlideLayoutValues.VerticalText, Preserve = true };
			slideLayout9.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slideLayout9.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slideLayout9.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData11 = new CommonSlideData(){ Name = "Title and Vertical Text" };

			ShapeTree shapeTree11 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties11 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties60 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties11 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties60 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties11.Append(nonVisualDrawingProperties60);
			nonVisualGroupShapeProperties11.Append(nonVisualGroupShapeDrawingProperties11);
			nonVisualGroupShapeProperties11.Append(applicationNonVisualDrawingProperties60);

			GroupShapeProperties groupShapeProperties11 = new GroupShapeProperties();

			A.TransformGroup transformGroup11 = new A.TransformGroup();
			A.Offset offset30 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents30 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset11 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents11 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup11.Append(offset30);
			transformGroup11.Append(extents30);
			transformGroup11.Append(childOffset11);
			transformGroup11.Append(childExtents11);

			groupShapeProperties11.Append(transformGroup11);

			Shape shape49 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties49 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties61 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties49 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks49 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties49.Append(shapeLocks49);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties61 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape49 = new PlaceholderShape(){ Type = PlaceholderValues.Title };

			applicationNonVisualDrawingProperties61.Append(placeholderShape49);

			nonVisualShapeProperties49.Append(nonVisualDrawingProperties61);
			nonVisualShapeProperties49.Append(nonVisualShapeDrawingProperties49);
			nonVisualShapeProperties49.Append(applicationNonVisualDrawingProperties61);
			ShapeProperties shapeProperties50 = new ShapeProperties();

			TextBody textBody49 = new TextBody();
			A.BodyProperties bodyProperties49 = new A.BodyProperties();
			A.ListStyle listStyle49 = new A.ListStyle();

			A.Paragraph paragraph73 = new A.Paragraph();

			A.Run run45 = new A.Run();
			A.RunProperties runProperties63 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text63 = new A.Text();
			text63.Text = "Click to edit Master title style";

			run45.Append(runProperties63);
			run45.Append(text63);
			A.EndParagraphRunProperties endParagraphRunProperties45 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph73.Append(run45);
			paragraph73.Append(endParagraphRunProperties45);

			textBody49.Append(bodyProperties49);
			textBody49.Append(listStyle49);
			textBody49.Append(paragraph73);

			shape49.Append(nonVisualShapeProperties49);
			shape49.Append(shapeProperties50);
			shape49.Append(textBody49);

			Shape shape50 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties50 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties62 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Vertical Text Placeholder 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties50 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks50 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties50.Append(shapeLocks50);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties62 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape50 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Orientation = DirectionValues.Vertical, Index = (UInt32Value)1U };

			applicationNonVisualDrawingProperties62.Append(placeholderShape50);

			nonVisualShapeProperties50.Append(nonVisualDrawingProperties62);
			nonVisualShapeProperties50.Append(nonVisualShapeDrawingProperties50);
			nonVisualShapeProperties50.Append(applicationNonVisualDrawingProperties62);
			ShapeProperties shapeProperties51 = new ShapeProperties();

			TextBody textBody50 = new TextBody();
			A.BodyProperties bodyProperties50 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.EastAsianVetical };
			A.ListStyle listStyle50 = new A.ListStyle();

			A.Paragraph paragraph74 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties35 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run46 = new A.Run();
			A.RunProperties runProperties64 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text64 = new A.Text();
			text64.Text = "Click to edit Master text styles";

			run46.Append(runProperties64);
			run46.Append(text64);

			paragraph74.Append(paragraphProperties35);
			paragraph74.Append(run46);

			A.Paragraph paragraph75 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties36 = new A.ParagraphProperties(){ Level = 1 };

			A.Run run47 = new A.Run();
			A.RunProperties runProperties65 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text65 = new A.Text();
			text65.Text = "Second level";

			run47.Append(runProperties65);
			run47.Append(text65);

			paragraph75.Append(paragraphProperties36);
			paragraph75.Append(run47);

			A.Paragraph paragraph76 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties37 = new A.ParagraphProperties(){ Level = 2 };

			A.Run run48 = new A.Run();
			A.RunProperties runProperties66 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text66 = new A.Text();
			text66.Text = "Third level";

			run48.Append(runProperties66);
			run48.Append(text66);

			paragraph76.Append(paragraphProperties37);
			paragraph76.Append(run48);

			A.Paragraph paragraph77 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties38 = new A.ParagraphProperties(){ Level = 3 };

			A.Run run49 = new A.Run();
			A.RunProperties runProperties67 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text67 = new A.Text();
			text67.Text = "Fourth level";

			run49.Append(runProperties67);
			run49.Append(text67);

			paragraph77.Append(paragraphProperties38);
			paragraph77.Append(run49);

			A.Paragraph paragraph78 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties39 = new A.ParagraphProperties(){ Level = 4 };

			A.Run run50 = new A.Run();
			A.RunProperties runProperties68 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text68 = new A.Text();
			text68.Text = "Fifth level";

			run50.Append(runProperties68);
			run50.Append(text68);
			A.EndParagraphRunProperties endParagraphRunProperties46 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph78.Append(paragraphProperties39);
			paragraph78.Append(run50);
			paragraph78.Append(endParagraphRunProperties46);

			textBody50.Append(bodyProperties50);
			textBody50.Append(listStyle50);
			textBody50.Append(paragraph74);
			textBody50.Append(paragraph75);
			textBody50.Append(paragraph76);
			textBody50.Append(paragraph77);
			textBody50.Append(paragraph78);

			shape50.Append(nonVisualShapeProperties50);
			shape50.Append(shapeProperties51);
			shape50.Append(textBody50);

			Shape shape51 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties51 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties63 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Date Placeholder 3" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties51 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks51 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties51.Append(shapeLocks51);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties63 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape51 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)10U };

			applicationNonVisualDrawingProperties63.Append(placeholderShape51);

			nonVisualShapeProperties51.Append(nonVisualDrawingProperties63);
			nonVisualShapeProperties51.Append(nonVisualShapeDrawingProperties51);
			nonVisualShapeProperties51.Append(applicationNonVisualDrawingProperties63);
			ShapeProperties shapeProperties52 = new ShapeProperties();

			TextBody textBody51 = new TextBody();
			A.BodyProperties bodyProperties51 = new A.BodyProperties();
			A.ListStyle listStyle51 = new A.ListStyle();

			A.Paragraph paragraph79 = new A.Paragraph();

			A.Field field19 = new A.Field(){ Id = "{6A870D7B-DCE0-46E8-B448-2F0E4E8E28D1}", Type = "datetimeFigureOut" };
			A.RunProperties runProperties69 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text69 = new A.Text();
			text69.Text = "3/28/2013";

			field19.Append(runProperties69);
			field19.Append(text69);
			A.EndParagraphRunProperties endParagraphRunProperties47 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph79.Append(field19);
			paragraph79.Append(endParagraphRunProperties47);

			textBody51.Append(bodyProperties51);
			textBody51.Append(listStyle51);
			textBody51.Append(paragraph79);

			shape51.Append(nonVisualShapeProperties51);
			shape51.Append(shapeProperties52);
			shape51.Append(textBody51);

			Shape shape52 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties52 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties64 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Footer Placeholder 4" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties52 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks52 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties52.Append(shapeLocks52);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties64 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape52 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

			applicationNonVisualDrawingProperties64.Append(placeholderShape52);

			nonVisualShapeProperties52.Append(nonVisualDrawingProperties64);
			nonVisualShapeProperties52.Append(nonVisualShapeDrawingProperties52);
			nonVisualShapeProperties52.Append(applicationNonVisualDrawingProperties64);
			ShapeProperties shapeProperties53 = new ShapeProperties();

			TextBody textBody52 = new TextBody();
			A.BodyProperties bodyProperties52 = new A.BodyProperties();
			A.ListStyle listStyle52 = new A.ListStyle();

			A.Paragraph paragraph80 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties48 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph80.Append(endParagraphRunProperties48);

			textBody52.Append(bodyProperties52);
			textBody52.Append(listStyle52);
			textBody52.Append(paragraph80);

			shape52.Append(nonVisualShapeProperties52);
			shape52.Append(shapeProperties53);
			shape52.Append(textBody52);

			Shape shape53 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties53 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties65 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Slide Number Placeholder 5" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties53 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks53 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties53.Append(shapeLocks53);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties65 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape53 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

			applicationNonVisualDrawingProperties65.Append(placeholderShape53);

			nonVisualShapeProperties53.Append(nonVisualDrawingProperties65);
			nonVisualShapeProperties53.Append(nonVisualShapeDrawingProperties53);
			nonVisualShapeProperties53.Append(applicationNonVisualDrawingProperties65);
			ShapeProperties shapeProperties54 = new ShapeProperties();

			TextBody textBody53 = new TextBody();
			A.BodyProperties bodyProperties53 = new A.BodyProperties();
			A.ListStyle listStyle53 = new A.ListStyle();

			A.Paragraph paragraph81 = new A.Paragraph();

			A.Field field20 = new A.Field(){ Id = "{AFBBC1CF-DB04-427F-9A30-B7E1FB44F3B5}", Type = "slidenum" };
			A.RunProperties runProperties70 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text70 = new A.Text();
			text70.Text = "‹#›";

			field20.Append(runProperties70);
			field20.Append(text70);
			A.EndParagraphRunProperties endParagraphRunProperties49 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph81.Append(field20);
			paragraph81.Append(endParagraphRunProperties49);

			textBody53.Append(bodyProperties53);
			textBody53.Append(listStyle53);
			textBody53.Append(paragraph81);

			shape53.Append(nonVisualShapeProperties53);
			shape53.Append(shapeProperties54);
			shape53.Append(textBody53);

			shapeTree11.Append(nonVisualGroupShapeProperties11);
			shapeTree11.Append(groupShapeProperties11);
			shapeTree11.Append(shape49);
			shapeTree11.Append(shape50);
			shapeTree11.Append(shape51);
			shapeTree11.Append(shape52);
			shapeTree11.Append(shape53);

			commonSlideData11.Append(shapeTree11);

			ColorMapOverride colorMapOverride10 = new ColorMapOverride();
			A.MasterColorMapping masterColorMapping10 = new A.MasterColorMapping();

			colorMapOverride10.Append(masterColorMapping10);

			slideLayout9.Append(commonSlideData11);
			slideLayout9.Append(colorMapOverride10);

			slideLayoutPart9.SlideLayout = slideLayout9;
		}

		// Generates content of slideLayoutPart10.
		private void GenerateSlideLayoutPart10Content(SlideLayoutPart slideLayoutPart10)
		{
			SlideLayout slideLayout10 = new SlideLayout(){ Type = SlideLayoutValues.TwoObjects, Preserve = true };
			slideLayout10.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slideLayout10.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slideLayout10.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData12 = new CommonSlideData(){ Name = "Two Content" };

			ShapeTree shapeTree12 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties12 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties66 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties12 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties66 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties12.Append(nonVisualDrawingProperties66);
			nonVisualGroupShapeProperties12.Append(nonVisualGroupShapeDrawingProperties12);
			nonVisualGroupShapeProperties12.Append(applicationNonVisualDrawingProperties66);

			GroupShapeProperties groupShapeProperties12 = new GroupShapeProperties();

			A.TransformGroup transformGroup12 = new A.TransformGroup();
			A.Offset offset31 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents31 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset12 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents12 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup12.Append(offset31);
			transformGroup12.Append(extents31);
			transformGroup12.Append(childOffset12);
			transformGroup12.Append(childExtents12);

			groupShapeProperties12.Append(transformGroup12);

			Shape shape54 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties54 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties67 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties54 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks54 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties54.Append(shapeLocks54);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties67 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape54 = new PlaceholderShape(){ Type = PlaceholderValues.Title };

			applicationNonVisualDrawingProperties67.Append(placeholderShape54);

			nonVisualShapeProperties54.Append(nonVisualDrawingProperties67);
			nonVisualShapeProperties54.Append(nonVisualShapeDrawingProperties54);
			nonVisualShapeProperties54.Append(applicationNonVisualDrawingProperties67);
			ShapeProperties shapeProperties55 = new ShapeProperties();

			TextBody textBody54 = new TextBody();
			A.BodyProperties bodyProperties54 = new A.BodyProperties();
			A.ListStyle listStyle54 = new A.ListStyle();

			A.Paragraph paragraph82 = new A.Paragraph();

			A.Run run51 = new A.Run();
			A.RunProperties runProperties71 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text71 = new A.Text();
			text71.Text = "Click to edit Master title style";

			run51.Append(runProperties71);
			run51.Append(text71);
			A.EndParagraphRunProperties endParagraphRunProperties50 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph82.Append(run51);
			paragraph82.Append(endParagraphRunProperties50);

			textBody54.Append(bodyProperties54);
			textBody54.Append(listStyle54);
			textBody54.Append(paragraph82);

			shape54.Append(nonVisualShapeProperties54);
			shape54.Append(shapeProperties55);
			shape54.Append(textBody54);

			Shape shape55 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties55 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties68 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Content Placeholder 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties55 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks55 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties55.Append(shapeLocks55);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties68 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape55 = new PlaceholderShape(){ Size = PlaceholderSizeValues.Half, Index = (UInt32Value)1U };

			applicationNonVisualDrawingProperties68.Append(placeholderShape55);

			nonVisualShapeProperties55.Append(nonVisualDrawingProperties68);
			nonVisualShapeProperties55.Append(nonVisualShapeDrawingProperties55);
			nonVisualShapeProperties55.Append(applicationNonVisualDrawingProperties68);

			ShapeProperties shapeProperties56 = new ShapeProperties();

			A.Transform2D transform2D20 = new A.Transform2D();
			A.Offset offset32 = new A.Offset(){ X = 457200L, Y = 1600200L };
			A.Extents extents32 = new A.Extents(){ Cx = 4038600L, Cy = 4525963L };

			transform2D20.Append(offset32);
			transform2D20.Append(extents32);

			shapeProperties56.Append(transform2D20);

			TextBody textBody55 = new TextBody();
			A.BodyProperties bodyProperties55 = new A.BodyProperties();

			A.ListStyle listStyle55 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties19 = new A.Level1ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties109 = new A.DefaultRunProperties(){ FontSize = 2800 };

			level1ParagraphProperties19.Append(defaultRunProperties109);

			A.Level2ParagraphProperties level2ParagraphProperties12 = new A.Level2ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties110 = new A.DefaultRunProperties(){ FontSize = 2400 };

			level2ParagraphProperties12.Append(defaultRunProperties110);

			A.Level3ParagraphProperties level3ParagraphProperties12 = new A.Level3ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties111 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level3ParagraphProperties12.Append(defaultRunProperties111);

			A.Level4ParagraphProperties level4ParagraphProperties12 = new A.Level4ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties112 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level4ParagraphProperties12.Append(defaultRunProperties112);

			A.Level5ParagraphProperties level5ParagraphProperties12 = new A.Level5ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties113 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level5ParagraphProperties12.Append(defaultRunProperties113);

			A.Level6ParagraphProperties level6ParagraphProperties12 = new A.Level6ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties114 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level6ParagraphProperties12.Append(defaultRunProperties114);

			A.Level7ParagraphProperties level7ParagraphProperties12 = new A.Level7ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties115 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level7ParagraphProperties12.Append(defaultRunProperties115);

			A.Level8ParagraphProperties level8ParagraphProperties12 = new A.Level8ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties116 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level8ParagraphProperties12.Append(defaultRunProperties116);

			A.Level9ParagraphProperties level9ParagraphProperties12 = new A.Level9ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties117 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level9ParagraphProperties12.Append(defaultRunProperties117);

			listStyle55.Append(level1ParagraphProperties19);
			listStyle55.Append(level2ParagraphProperties12);
			listStyle55.Append(level3ParagraphProperties12);
			listStyle55.Append(level4ParagraphProperties12);
			listStyle55.Append(level5ParagraphProperties12);
			listStyle55.Append(level6ParagraphProperties12);
			listStyle55.Append(level7ParagraphProperties12);
			listStyle55.Append(level8ParagraphProperties12);
			listStyle55.Append(level9ParagraphProperties12);

			A.Paragraph paragraph83 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties40 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run52 = new A.Run();
			A.RunProperties runProperties72 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text72 = new A.Text();
			text72.Text = "Click to edit Master text styles";

			run52.Append(runProperties72);
			run52.Append(text72);

			paragraph83.Append(paragraphProperties40);
			paragraph83.Append(run52);

			A.Paragraph paragraph84 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties41 = new A.ParagraphProperties(){ Level = 1 };

			A.Run run53 = new A.Run();
			A.RunProperties runProperties73 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text73 = new A.Text();
			text73.Text = "Second level";

			run53.Append(runProperties73);
			run53.Append(text73);

			paragraph84.Append(paragraphProperties41);
			paragraph84.Append(run53);

			A.Paragraph paragraph85 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties42 = new A.ParagraphProperties(){ Level = 2 };

			A.Run run54 = new A.Run();
			A.RunProperties runProperties74 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text74 = new A.Text();
			text74.Text = "Third level";

			run54.Append(runProperties74);
			run54.Append(text74);

			paragraph85.Append(paragraphProperties42);
			paragraph85.Append(run54);

			A.Paragraph paragraph86 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties43 = new A.ParagraphProperties(){ Level = 3 };

			A.Run run55 = new A.Run();
			A.RunProperties runProperties75 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text75 = new A.Text();
			text75.Text = "Fourth level";

			run55.Append(runProperties75);
			run55.Append(text75);

			paragraph86.Append(paragraphProperties43);
			paragraph86.Append(run55);

			A.Paragraph paragraph87 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties44 = new A.ParagraphProperties(){ Level = 4 };

			A.Run run56 = new A.Run();
			A.RunProperties runProperties76 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text76 = new A.Text();
			text76.Text = "Fifth level";

			run56.Append(runProperties76);
			run56.Append(text76);
			A.EndParagraphRunProperties endParagraphRunProperties51 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph87.Append(paragraphProperties44);
			paragraph87.Append(run56);
			paragraph87.Append(endParagraphRunProperties51);

			textBody55.Append(bodyProperties55);
			textBody55.Append(listStyle55);
			textBody55.Append(paragraph83);
			textBody55.Append(paragraph84);
			textBody55.Append(paragraph85);
			textBody55.Append(paragraph86);
			textBody55.Append(paragraph87);

			shape55.Append(nonVisualShapeProperties55);
			shape55.Append(shapeProperties56);
			shape55.Append(textBody55);

			Shape shape56 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties56 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties69 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Content Placeholder 3" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties56 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks56 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties56.Append(shapeLocks56);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties69 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape56 = new PlaceholderShape(){ Size = PlaceholderSizeValues.Half, Index = (UInt32Value)2U };

			applicationNonVisualDrawingProperties69.Append(placeholderShape56);

			nonVisualShapeProperties56.Append(nonVisualDrawingProperties69);
			nonVisualShapeProperties56.Append(nonVisualShapeDrawingProperties56);
			nonVisualShapeProperties56.Append(applicationNonVisualDrawingProperties69);

			ShapeProperties shapeProperties57 = new ShapeProperties();

			A.Transform2D transform2D21 = new A.Transform2D();
			A.Offset offset33 = new A.Offset(){ X = 4648200L, Y = 1600200L };
			A.Extents extents33 = new A.Extents(){ Cx = 4038600L, Cy = 4525963L };

			transform2D21.Append(offset33);
			transform2D21.Append(extents33);

			shapeProperties57.Append(transform2D21);

			TextBody textBody56 = new TextBody();
			A.BodyProperties bodyProperties56 = new A.BodyProperties();

			A.ListStyle listStyle56 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties20 = new A.Level1ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties118 = new A.DefaultRunProperties(){ FontSize = 2800 };

			level1ParagraphProperties20.Append(defaultRunProperties118);

			A.Level2ParagraphProperties level2ParagraphProperties13 = new A.Level2ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties119 = new A.DefaultRunProperties(){ FontSize = 2400 };

			level2ParagraphProperties13.Append(defaultRunProperties119);

			A.Level3ParagraphProperties level3ParagraphProperties13 = new A.Level3ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties120 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level3ParagraphProperties13.Append(defaultRunProperties120);

			A.Level4ParagraphProperties level4ParagraphProperties13 = new A.Level4ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties121 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level4ParagraphProperties13.Append(defaultRunProperties121);

			A.Level5ParagraphProperties level5ParagraphProperties13 = new A.Level5ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties122 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level5ParagraphProperties13.Append(defaultRunProperties122);

			A.Level6ParagraphProperties level6ParagraphProperties13 = new A.Level6ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties123 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level6ParagraphProperties13.Append(defaultRunProperties123);

			A.Level7ParagraphProperties level7ParagraphProperties13 = new A.Level7ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties124 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level7ParagraphProperties13.Append(defaultRunProperties124);

			A.Level8ParagraphProperties level8ParagraphProperties13 = new A.Level8ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties125 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level8ParagraphProperties13.Append(defaultRunProperties125);

			A.Level9ParagraphProperties level9ParagraphProperties13 = new A.Level9ParagraphProperties();
			A.DefaultRunProperties defaultRunProperties126 = new A.DefaultRunProperties(){ FontSize = 1800 };

			level9ParagraphProperties13.Append(defaultRunProperties126);

			listStyle56.Append(level1ParagraphProperties20);
			listStyle56.Append(level2ParagraphProperties13);
			listStyle56.Append(level3ParagraphProperties13);
			listStyle56.Append(level4ParagraphProperties13);
			listStyle56.Append(level5ParagraphProperties13);
			listStyle56.Append(level6ParagraphProperties13);
			listStyle56.Append(level7ParagraphProperties13);
			listStyle56.Append(level8ParagraphProperties13);
			listStyle56.Append(level9ParagraphProperties13);

			A.Paragraph paragraph88 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties45 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run57 = new A.Run();
			A.RunProperties runProperties77 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text77 = new A.Text();
			text77.Text = "Click to edit Master text styles";

			run57.Append(runProperties77);
			run57.Append(text77);

			paragraph88.Append(paragraphProperties45);
			paragraph88.Append(run57);

			A.Paragraph paragraph89 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties46 = new A.ParagraphProperties(){ Level = 1 };

			A.Run run58 = new A.Run();
			A.RunProperties runProperties78 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text78 = new A.Text();
			text78.Text = "Second level";

			run58.Append(runProperties78);
			run58.Append(text78);

			paragraph89.Append(paragraphProperties46);
			paragraph89.Append(run58);

			A.Paragraph paragraph90 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties47 = new A.ParagraphProperties(){ Level = 2 };

			A.Run run59 = new A.Run();
			A.RunProperties runProperties79 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text79 = new A.Text();
			text79.Text = "Third level";

			run59.Append(runProperties79);
			run59.Append(text79);

			paragraph90.Append(paragraphProperties47);
			paragraph90.Append(run59);

			A.Paragraph paragraph91 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties48 = new A.ParagraphProperties(){ Level = 3 };

			A.Run run60 = new A.Run();
			A.RunProperties runProperties80 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text80 = new A.Text();
			text80.Text = "Fourth level";

			run60.Append(runProperties80);
			run60.Append(text80);

			paragraph91.Append(paragraphProperties48);
			paragraph91.Append(run60);

			A.Paragraph paragraph92 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties49 = new A.ParagraphProperties(){ Level = 4 };

			A.Run run61 = new A.Run();
			A.RunProperties runProperties81 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text81 = new A.Text();
			text81.Text = "Fifth level";

			run61.Append(runProperties81);
			run61.Append(text81);
			A.EndParagraphRunProperties endParagraphRunProperties52 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph92.Append(paragraphProperties49);
			paragraph92.Append(run61);
			paragraph92.Append(endParagraphRunProperties52);

			textBody56.Append(bodyProperties56);
			textBody56.Append(listStyle56);
			textBody56.Append(paragraph88);
			textBody56.Append(paragraph89);
			textBody56.Append(paragraph90);
			textBody56.Append(paragraph91);
			textBody56.Append(paragraph92);

			shape56.Append(nonVisualShapeProperties56);
			shape56.Append(shapeProperties57);
			shape56.Append(textBody56);

			Shape shape57 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties57 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties70 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Date Placeholder 4" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties57 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks57 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties57.Append(shapeLocks57);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties70 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape57 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)10U };

			applicationNonVisualDrawingProperties70.Append(placeholderShape57);

			nonVisualShapeProperties57.Append(nonVisualDrawingProperties70);
			nonVisualShapeProperties57.Append(nonVisualShapeDrawingProperties57);
			nonVisualShapeProperties57.Append(applicationNonVisualDrawingProperties70);
			ShapeProperties shapeProperties58 = new ShapeProperties();

			TextBody textBody57 = new TextBody();
			A.BodyProperties bodyProperties57 = new A.BodyProperties();
			A.ListStyle listStyle57 = new A.ListStyle();

			A.Paragraph paragraph93 = new A.Paragraph();

			A.Field field21 = new A.Field(){ Id = "{6A870D7B-DCE0-46E8-B448-2F0E4E8E28D1}", Type = "datetimeFigureOut" };
			A.RunProperties runProperties82 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text82 = new A.Text();
			text82.Text = "3/28/2013";

			field21.Append(runProperties82);
			field21.Append(text82);
			A.EndParagraphRunProperties endParagraphRunProperties53 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph93.Append(field21);
			paragraph93.Append(endParagraphRunProperties53);

			textBody57.Append(bodyProperties57);
			textBody57.Append(listStyle57);
			textBody57.Append(paragraph93);

			shape57.Append(nonVisualShapeProperties57);
			shape57.Append(shapeProperties58);
			shape57.Append(textBody57);

			Shape shape58 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties58 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties71 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Footer Placeholder 5" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties58 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks58 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties58.Append(shapeLocks58);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties71 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape58 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

			applicationNonVisualDrawingProperties71.Append(placeholderShape58);

			nonVisualShapeProperties58.Append(nonVisualDrawingProperties71);
			nonVisualShapeProperties58.Append(nonVisualShapeDrawingProperties58);
			nonVisualShapeProperties58.Append(applicationNonVisualDrawingProperties71);
			ShapeProperties shapeProperties59 = new ShapeProperties();

			TextBody textBody58 = new TextBody();
			A.BodyProperties bodyProperties58 = new A.BodyProperties();
			A.ListStyle listStyle58 = new A.ListStyle();

			A.Paragraph paragraph94 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties54 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph94.Append(endParagraphRunProperties54);

			textBody58.Append(bodyProperties58);
			textBody58.Append(listStyle58);
			textBody58.Append(paragraph94);

			shape58.Append(nonVisualShapeProperties58);
			shape58.Append(shapeProperties59);
			shape58.Append(textBody58);

			Shape shape59 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties59 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties72 = new NonVisualDrawingProperties(){ Id = (UInt32Value)7U, Name = "Slide Number Placeholder 6" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties59 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks59 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties59.Append(shapeLocks59);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties72 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape59 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

			applicationNonVisualDrawingProperties72.Append(placeholderShape59);

			nonVisualShapeProperties59.Append(nonVisualDrawingProperties72);
			nonVisualShapeProperties59.Append(nonVisualShapeDrawingProperties59);
			nonVisualShapeProperties59.Append(applicationNonVisualDrawingProperties72);
			ShapeProperties shapeProperties60 = new ShapeProperties();

			TextBody textBody59 = new TextBody();
			A.BodyProperties bodyProperties59 = new A.BodyProperties();
			A.ListStyle listStyle59 = new A.ListStyle();

			A.Paragraph paragraph95 = new A.Paragraph();

			A.Field field22 = new A.Field(){ Id = "{AFBBC1CF-DB04-427F-9A30-B7E1FB44F3B5}", Type = "slidenum" };
			A.RunProperties runProperties83 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text83 = new A.Text();
			text83.Text = "‹#›";

			field22.Append(runProperties83);
			field22.Append(text83);
			A.EndParagraphRunProperties endParagraphRunProperties55 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph95.Append(field22);
			paragraph95.Append(endParagraphRunProperties55);

			textBody59.Append(bodyProperties59);
			textBody59.Append(listStyle59);
			textBody59.Append(paragraph95);

			shape59.Append(nonVisualShapeProperties59);
			shape59.Append(shapeProperties60);
			shape59.Append(textBody59);

			shapeTree12.Append(nonVisualGroupShapeProperties12);
			shapeTree12.Append(groupShapeProperties12);
			shapeTree12.Append(shape54);
			shapeTree12.Append(shape55);
			shapeTree12.Append(shape56);
			shapeTree12.Append(shape57);
			shapeTree12.Append(shape58);
			shapeTree12.Append(shape59);

			commonSlideData12.Append(shapeTree12);

			ColorMapOverride colorMapOverride11 = new ColorMapOverride();
			A.MasterColorMapping masterColorMapping11 = new A.MasterColorMapping();

			colorMapOverride11.Append(masterColorMapping11);

			slideLayout10.Append(commonSlideData12);
			slideLayout10.Append(colorMapOverride11);

			slideLayoutPart10.SlideLayout = slideLayout10;
		}

		// Generates content of slideLayoutPart11.
		private void GenerateSlideLayoutPart11Content(SlideLayoutPart slideLayoutPart11)
		{
			SlideLayout slideLayout11 = new SlideLayout(){ Type = SlideLayoutValues.PictureText, Preserve = true };
			slideLayout11.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			slideLayout11.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			slideLayout11.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			CommonSlideData commonSlideData13 = new CommonSlideData(){ Name = "Picture with Caption" };

			ShapeTree shapeTree13 = new ShapeTree();

			NonVisualGroupShapeProperties nonVisualGroupShapeProperties13 = new NonVisualGroupShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties73 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
			NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties13 = new NonVisualGroupShapeDrawingProperties();
			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties73 = new ApplicationNonVisualDrawingProperties();

			nonVisualGroupShapeProperties13.Append(nonVisualDrawingProperties73);
			nonVisualGroupShapeProperties13.Append(nonVisualGroupShapeDrawingProperties13);
			nonVisualGroupShapeProperties13.Append(applicationNonVisualDrawingProperties73);

			GroupShapeProperties groupShapeProperties13 = new GroupShapeProperties();

			A.TransformGroup transformGroup13 = new A.TransformGroup();
			A.Offset offset34 = new A.Offset(){ X = 0L, Y = 0L };
			A.Extents extents34 = new A.Extents(){ Cx = 0L, Cy = 0L };
			A.ChildOffset childOffset13 = new A.ChildOffset(){ X = 0L, Y = 0L };
			A.ChildExtents childExtents13 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

			transformGroup13.Append(offset34);
			transformGroup13.Append(extents34);
			transformGroup13.Append(childOffset13);
			transformGroup13.Append(childExtents13);

			groupShapeProperties13.Append(transformGroup13);

			Shape shape60 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties60 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties74 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title 1" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties60 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks60 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties60.Append(shapeLocks60);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties74 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape60 = new PlaceholderShape(){ Type = PlaceholderValues.Title };

			applicationNonVisualDrawingProperties74.Append(placeholderShape60);

			nonVisualShapeProperties60.Append(nonVisualDrawingProperties74);
			nonVisualShapeProperties60.Append(nonVisualShapeDrawingProperties60);
			nonVisualShapeProperties60.Append(applicationNonVisualDrawingProperties74);

			ShapeProperties shapeProperties61 = new ShapeProperties();

			A.Transform2D transform2D22 = new A.Transform2D();
			A.Offset offset35 = new A.Offset(){ X = 1792288L, Y = 4800600L };
			A.Extents extents35 = new A.Extents(){ Cx = 5486400L, Cy = 566738L };

			transform2D22.Append(offset35);
			transform2D22.Append(extents35);

			shapeProperties61.Append(transform2D22);

			TextBody textBody60 = new TextBody();
			A.BodyProperties bodyProperties60 = new A.BodyProperties(){ Anchor = A.TextAnchoringTypeValues.Bottom };

			A.ListStyle listStyle60 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties21 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Left };
			A.DefaultRunProperties defaultRunProperties127 = new A.DefaultRunProperties(){ FontSize = 2000, Bold = true };

			level1ParagraphProperties21.Append(defaultRunProperties127);

			listStyle60.Append(level1ParagraphProperties21);

			A.Paragraph paragraph96 = new A.Paragraph();

			A.Run run62 = new A.Run();
			A.RunProperties runProperties84 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text84 = new A.Text();
			text84.Text = "Click to edit Master title style";

			run62.Append(runProperties84);
			run62.Append(text84);
			A.EndParagraphRunProperties endParagraphRunProperties56 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph96.Append(run62);
			paragraph96.Append(endParagraphRunProperties56);

			textBody60.Append(bodyProperties60);
			textBody60.Append(listStyle60);
			textBody60.Append(paragraph96);

			shape60.Append(nonVisualShapeProperties60);
			shape60.Append(shapeProperties61);
			shape60.Append(textBody60);

			Shape shape61 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties61 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties75 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Picture Placeholder 2" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties61 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks61 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties61.Append(shapeLocks61);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties75 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape61 = new PlaceholderShape(){ Type = PlaceholderValues.Picture, Index = (UInt32Value)1U };

			applicationNonVisualDrawingProperties75.Append(placeholderShape61);

			nonVisualShapeProperties61.Append(nonVisualDrawingProperties75);
			nonVisualShapeProperties61.Append(nonVisualShapeDrawingProperties61);
			nonVisualShapeProperties61.Append(applicationNonVisualDrawingProperties75);

			ShapeProperties shapeProperties62 = new ShapeProperties();

			A.Transform2D transform2D23 = new A.Transform2D();
			A.Offset offset36 = new A.Offset(){ X = 1792288L, Y = 612775L };
			A.Extents extents36 = new A.Extents(){ Cx = 5486400L, Cy = 4114800L };

			transform2D23.Append(offset36);
			transform2D23.Append(extents36);

			shapeProperties62.Append(transform2D23);

			TextBody textBody61 = new TextBody();
			A.BodyProperties bodyProperties61 = new A.BodyProperties();

			A.ListStyle listStyle61 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties22 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Indent = 0 };
			A.NoBullet noBullet47 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties128 = new A.DefaultRunProperties(){ FontSize = 3200 };

			level1ParagraphProperties22.Append(noBullet47);
			level1ParagraphProperties22.Append(defaultRunProperties128);

			A.Level2ParagraphProperties level2ParagraphProperties14 = new A.Level2ParagraphProperties(){ LeftMargin = 457200, Indent = 0 };
			A.NoBullet noBullet48 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties129 = new A.DefaultRunProperties(){ FontSize = 2800 };

			level2ParagraphProperties14.Append(noBullet48);
			level2ParagraphProperties14.Append(defaultRunProperties129);

			A.Level3ParagraphProperties level3ParagraphProperties14 = new A.Level3ParagraphProperties(){ LeftMargin = 914400, Indent = 0 };
			A.NoBullet noBullet49 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties130 = new A.DefaultRunProperties(){ FontSize = 2400 };

			level3ParagraphProperties14.Append(noBullet49);
			level3ParagraphProperties14.Append(defaultRunProperties130);

			A.Level4ParagraphProperties level4ParagraphProperties14 = new A.Level4ParagraphProperties(){ LeftMargin = 1371600, Indent = 0 };
			A.NoBullet noBullet50 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties131 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level4ParagraphProperties14.Append(noBullet50);
			level4ParagraphProperties14.Append(defaultRunProperties131);

			A.Level5ParagraphProperties level5ParagraphProperties14 = new A.Level5ParagraphProperties(){ LeftMargin = 1828800, Indent = 0 };
			A.NoBullet noBullet51 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties132 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level5ParagraphProperties14.Append(noBullet51);
			level5ParagraphProperties14.Append(defaultRunProperties132);

			A.Level6ParagraphProperties level6ParagraphProperties14 = new A.Level6ParagraphProperties(){ LeftMargin = 2286000, Indent = 0 };
			A.NoBullet noBullet52 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties133 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level6ParagraphProperties14.Append(noBullet52);
			level6ParagraphProperties14.Append(defaultRunProperties133);

			A.Level7ParagraphProperties level7ParagraphProperties14 = new A.Level7ParagraphProperties(){ LeftMargin = 2743200, Indent = 0 };
			A.NoBullet noBullet53 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties134 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level7ParagraphProperties14.Append(noBullet53);
			level7ParagraphProperties14.Append(defaultRunProperties134);

			A.Level8ParagraphProperties level8ParagraphProperties14 = new A.Level8ParagraphProperties(){ LeftMargin = 3200400, Indent = 0 };
			A.NoBullet noBullet54 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties135 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level8ParagraphProperties14.Append(noBullet54);
			level8ParagraphProperties14.Append(defaultRunProperties135);

			A.Level9ParagraphProperties level9ParagraphProperties14 = new A.Level9ParagraphProperties(){ LeftMargin = 3657600, Indent = 0 };
			A.NoBullet noBullet55 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties136 = new A.DefaultRunProperties(){ FontSize = 2000 };

			level9ParagraphProperties14.Append(noBullet55);
			level9ParagraphProperties14.Append(defaultRunProperties136);

			listStyle61.Append(level1ParagraphProperties22);
			listStyle61.Append(level2ParagraphProperties14);
			listStyle61.Append(level3ParagraphProperties14);
			listStyle61.Append(level4ParagraphProperties14);
			listStyle61.Append(level5ParagraphProperties14);
			listStyle61.Append(level6ParagraphProperties14);
			listStyle61.Append(level7ParagraphProperties14);
			listStyle61.Append(level8ParagraphProperties14);
			listStyle61.Append(level9ParagraphProperties14);

			A.Paragraph paragraph97 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties57 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph97.Append(endParagraphRunProperties57);

			textBody61.Append(bodyProperties61);
			textBody61.Append(listStyle61);
			textBody61.Append(paragraph97);

			shape61.Append(nonVisualShapeProperties61);
			shape61.Append(shapeProperties62);
			shape61.Append(textBody61);

			Shape shape62 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties62 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties76 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Text Placeholder 3" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties62 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks62 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties62.Append(shapeLocks62);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties76 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape62 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)2U };

			applicationNonVisualDrawingProperties76.Append(placeholderShape62);

			nonVisualShapeProperties62.Append(nonVisualDrawingProperties76);
			nonVisualShapeProperties62.Append(nonVisualShapeDrawingProperties62);
			nonVisualShapeProperties62.Append(applicationNonVisualDrawingProperties76);

			ShapeProperties shapeProperties63 = new ShapeProperties();

			A.Transform2D transform2D24 = new A.Transform2D();
			A.Offset offset37 = new A.Offset(){ X = 1792288L, Y = 5367338L };
			A.Extents extents37 = new A.Extents(){ Cx = 5486400L, Cy = 804862L };

			transform2D24.Append(offset37);
			transform2D24.Append(extents37);

			shapeProperties63.Append(transform2D24);

			TextBody textBody62 = new TextBody();
			A.BodyProperties bodyProperties62 = new A.BodyProperties();

			A.ListStyle listStyle62 = new A.ListStyle();

			A.Level1ParagraphProperties level1ParagraphProperties23 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Indent = 0 };
			A.NoBullet noBullet56 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties137 = new A.DefaultRunProperties(){ FontSize = 1400 };

			level1ParagraphProperties23.Append(noBullet56);
			level1ParagraphProperties23.Append(defaultRunProperties137);

			A.Level2ParagraphProperties level2ParagraphProperties15 = new A.Level2ParagraphProperties(){ LeftMargin = 457200, Indent = 0 };
			A.NoBullet noBullet57 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties138 = new A.DefaultRunProperties(){ FontSize = 1200 };

			level2ParagraphProperties15.Append(noBullet57);
			level2ParagraphProperties15.Append(defaultRunProperties138);

			A.Level3ParagraphProperties level3ParagraphProperties15 = new A.Level3ParagraphProperties(){ LeftMargin = 914400, Indent = 0 };
			A.NoBullet noBullet58 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties139 = new A.DefaultRunProperties(){ FontSize = 1000 };

			level3ParagraphProperties15.Append(noBullet58);
			level3ParagraphProperties15.Append(defaultRunProperties139);

			A.Level4ParagraphProperties level4ParagraphProperties15 = new A.Level4ParagraphProperties(){ LeftMargin = 1371600, Indent = 0 };
			A.NoBullet noBullet59 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties140 = new A.DefaultRunProperties(){ FontSize = 900 };

			level4ParagraphProperties15.Append(noBullet59);
			level4ParagraphProperties15.Append(defaultRunProperties140);

			A.Level5ParagraphProperties level5ParagraphProperties15 = new A.Level5ParagraphProperties(){ LeftMargin = 1828800, Indent = 0 };
			A.NoBullet noBullet60 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties141 = new A.DefaultRunProperties(){ FontSize = 900 };

			level5ParagraphProperties15.Append(noBullet60);
			level5ParagraphProperties15.Append(defaultRunProperties141);

			A.Level6ParagraphProperties level6ParagraphProperties15 = new A.Level6ParagraphProperties(){ LeftMargin = 2286000, Indent = 0 };
			A.NoBullet noBullet61 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties142 = new A.DefaultRunProperties(){ FontSize = 900 };

			level6ParagraphProperties15.Append(noBullet61);
			level6ParagraphProperties15.Append(defaultRunProperties142);

			A.Level7ParagraphProperties level7ParagraphProperties15 = new A.Level7ParagraphProperties(){ LeftMargin = 2743200, Indent = 0 };
			A.NoBullet noBullet62 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties143 = new A.DefaultRunProperties(){ FontSize = 900 };

			level7ParagraphProperties15.Append(noBullet62);
			level7ParagraphProperties15.Append(defaultRunProperties143);

			A.Level8ParagraphProperties level8ParagraphProperties15 = new A.Level8ParagraphProperties(){ LeftMargin = 3200400, Indent = 0 };
			A.NoBullet noBullet63 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties144 = new A.DefaultRunProperties(){ FontSize = 900 };

			level8ParagraphProperties15.Append(noBullet63);
			level8ParagraphProperties15.Append(defaultRunProperties144);

			A.Level9ParagraphProperties level9ParagraphProperties15 = new A.Level9ParagraphProperties(){ LeftMargin = 3657600, Indent = 0 };
			A.NoBullet noBullet64 = new A.NoBullet();
			A.DefaultRunProperties defaultRunProperties145 = new A.DefaultRunProperties(){ FontSize = 900 };

			level9ParagraphProperties15.Append(noBullet64);
			level9ParagraphProperties15.Append(defaultRunProperties145);

			listStyle62.Append(level1ParagraphProperties23);
			listStyle62.Append(level2ParagraphProperties15);
			listStyle62.Append(level3ParagraphProperties15);
			listStyle62.Append(level4ParagraphProperties15);
			listStyle62.Append(level5ParagraphProperties15);
			listStyle62.Append(level6ParagraphProperties15);
			listStyle62.Append(level7ParagraphProperties15);
			listStyle62.Append(level8ParagraphProperties15);
			listStyle62.Append(level9ParagraphProperties15);

			A.Paragraph paragraph98 = new A.Paragraph();
			A.ParagraphProperties paragraphProperties50 = new A.ParagraphProperties(){ Level = 0 };

			A.Run run63 = new A.Run();
			A.RunProperties runProperties85 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text85 = new A.Text();
			text85.Text = "Click to edit Master text styles";

			run63.Append(runProperties85);
			run63.Append(text85);

			paragraph98.Append(paragraphProperties50);
			paragraph98.Append(run63);

			textBody62.Append(bodyProperties62);
			textBody62.Append(listStyle62);
			textBody62.Append(paragraph98);

			shape62.Append(nonVisualShapeProperties62);
			shape62.Append(shapeProperties63);
			shape62.Append(textBody62);

			Shape shape63 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties63 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties77 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Date Placeholder 4" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties63 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks63 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties63.Append(shapeLocks63);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties77 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape63 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)10U };

			applicationNonVisualDrawingProperties77.Append(placeholderShape63);

			nonVisualShapeProperties63.Append(nonVisualDrawingProperties77);
			nonVisualShapeProperties63.Append(nonVisualShapeDrawingProperties63);
			nonVisualShapeProperties63.Append(applicationNonVisualDrawingProperties77);
			ShapeProperties shapeProperties64 = new ShapeProperties();

			TextBody textBody63 = new TextBody();
			A.BodyProperties bodyProperties63 = new A.BodyProperties();
			A.ListStyle listStyle63 = new A.ListStyle();

			A.Paragraph paragraph99 = new A.Paragraph();

			A.Field field23 = new A.Field(){ Id = "{6A870D7B-DCE0-46E8-B448-2F0E4E8E28D1}", Type = "datetimeFigureOut" };
			A.RunProperties runProperties86 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text86 = new A.Text();
			text86.Text = "3/28/2013";

			field23.Append(runProperties86);
			field23.Append(text86);
			A.EndParagraphRunProperties endParagraphRunProperties58 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph99.Append(field23);
			paragraph99.Append(endParagraphRunProperties58);

			textBody63.Append(bodyProperties63);
			textBody63.Append(listStyle63);
			textBody63.Append(paragraph99);

			shape63.Append(nonVisualShapeProperties63);
			shape63.Append(shapeProperties64);
			shape63.Append(textBody63);

			Shape shape64 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties64 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties78 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Footer Placeholder 5" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties64 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks64 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties64.Append(shapeLocks64);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties78 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape64 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

			applicationNonVisualDrawingProperties78.Append(placeholderShape64);

			nonVisualShapeProperties64.Append(nonVisualDrawingProperties78);
			nonVisualShapeProperties64.Append(nonVisualShapeDrawingProperties64);
			nonVisualShapeProperties64.Append(applicationNonVisualDrawingProperties78);
			ShapeProperties shapeProperties65 = new ShapeProperties();

			TextBody textBody64 = new TextBody();
			A.BodyProperties bodyProperties64 = new A.BodyProperties();
			A.ListStyle listStyle64 = new A.ListStyle();

			A.Paragraph paragraph100 = new A.Paragraph();
			A.EndParagraphRunProperties endParagraphRunProperties59 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph100.Append(endParagraphRunProperties59);

			textBody64.Append(bodyProperties64);
			textBody64.Append(listStyle64);
			textBody64.Append(paragraph100);

			shape64.Append(nonVisualShapeProperties64);
			shape64.Append(shapeProperties65);
			shape64.Append(textBody64);

			Shape shape65 = new Shape();

			NonVisualShapeProperties nonVisualShapeProperties65 = new NonVisualShapeProperties();
			NonVisualDrawingProperties nonVisualDrawingProperties79 = new NonVisualDrawingProperties(){ Id = (UInt32Value)7U, Name = "Slide Number Placeholder 6" };

			NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties65 = new NonVisualShapeDrawingProperties();
			A.ShapeLocks shapeLocks65 = new A.ShapeLocks(){ NoGrouping = true };

			nonVisualShapeDrawingProperties65.Append(shapeLocks65);

			ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties79 = new ApplicationNonVisualDrawingProperties();
			PlaceholderShape placeholderShape65 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

			applicationNonVisualDrawingProperties79.Append(placeholderShape65);

			nonVisualShapeProperties65.Append(nonVisualDrawingProperties79);
			nonVisualShapeProperties65.Append(nonVisualShapeDrawingProperties65);
			nonVisualShapeProperties65.Append(applicationNonVisualDrawingProperties79);
			ShapeProperties shapeProperties66 = new ShapeProperties();

			TextBody textBody65 = new TextBody();
			A.BodyProperties bodyProperties65 = new A.BodyProperties();
			A.ListStyle listStyle65 = new A.ListStyle();

			A.Paragraph paragraph101 = new A.Paragraph();

			A.Field field24 = new A.Field(){ Id = "{AFBBC1CF-DB04-427F-9A30-B7E1FB44F3B5}", Type = "slidenum" };
			A.RunProperties runProperties87 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
			A.Text text87 = new A.Text();
			text87.Text = "‹#›";

			field24.Append(runProperties87);
			field24.Append(text87);
			A.EndParagraphRunProperties endParagraphRunProperties60 = new A.EndParagraphRunProperties(){ Language = "en-US" };

			paragraph101.Append(field24);
			paragraph101.Append(endParagraphRunProperties60);

			textBody65.Append(bodyProperties65);
			textBody65.Append(listStyle65);
			textBody65.Append(paragraph101);

			shape65.Append(nonVisualShapeProperties65);
			shape65.Append(shapeProperties66);
			shape65.Append(textBody65);

			shapeTree13.Append(nonVisualGroupShapeProperties13);
			shapeTree13.Append(groupShapeProperties13);
			shapeTree13.Append(shape60);
			shapeTree13.Append(shape61);
			shapeTree13.Append(shape62);
			shapeTree13.Append(shape63);
			shapeTree13.Append(shape64);
			shapeTree13.Append(shape65);

			commonSlideData13.Append(shapeTree13);

			ColorMapOverride colorMapOverride12 = new ColorMapOverride();
			A.MasterColorMapping masterColorMapping12 = new A.MasterColorMapping();

			colorMapOverride12.Append(masterColorMapping12);

			slideLayout11.Append(commonSlideData13);
			slideLayout11.Append(colorMapOverride12);

			slideLayoutPart11.SlideLayout = slideLayout11;
		}

		// Generates content of tableStylesPart1.
		private void GenerateTableStylesPart1Content(TableStylesPart tableStylesPart1)
		{
			A.TableStyleList tableStyleList1 = new A.TableStyleList(){ Default = "{5C22544A-7EE6-4342-B048-85BDC9FD1C3A}" };
			tableStyleList1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

			tableStylesPart1.TableStyleList = tableStyleList1;
		}

		// Generates content of viewPropertiesPart1.
		private void GenerateViewPropertiesPart1Content(ViewPropertiesPart viewPropertiesPart1)
		{
			ViewProperties viewProperties1 = new ViewProperties();
			viewProperties1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			viewProperties1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			viewProperties1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

			NormalViewProperties normalViewProperties1 = new NormalViewProperties(){ ShowOutlineIcons = false };
			RestoredLeft restoredLeft1 = new RestoredLeft(){ Size = 15620 };
			RestoredTop restoredTop1 = new RestoredTop(){ Size = 94660 };

			normalViewProperties1.Append(restoredLeft1);
			normalViewProperties1.Append(restoredTop1);

			SlideViewProperties slideViewProperties1 = new SlideViewProperties();

			CommonSlideViewProperties commonSlideViewProperties1 = new CommonSlideViewProperties();

			CommonViewProperties commonViewProperties1 = new CommonViewProperties(){ VariableScale = true };

			ScaleFactor scaleFactor1 = new ScaleFactor();
			A.ScaleX scaleX1 = new A.ScaleX(){ Numerator = 49, Denominator = 100 };
			A.ScaleY scaleY1 = new A.ScaleY(){ Numerator = 49, Denominator = 100 };

			scaleFactor1.Append(scaleX1);
			scaleFactor1.Append(scaleY1);
			Origin origin1 = new Origin(){ X = -588L, Y = -84L };

			commonViewProperties1.Append(scaleFactor1);
			commonViewProperties1.Append(origin1);

			GuideList guideList1 = new GuideList();
			Guide guide1 = new Guide(){ Orientation = DirectionValues.Horizontal, Position = 2160 };
			Guide guide2 = new Guide(){ Position = 2880 };

			guideList1.Append(guide1);
			guideList1.Append(guide2);

			commonSlideViewProperties1.Append(commonViewProperties1);
			commonSlideViewProperties1.Append(guideList1);

			slideViewProperties1.Append(commonSlideViewProperties1);

			NotesTextViewProperties notesTextViewProperties1 = new NotesTextViewProperties();

			CommonViewProperties commonViewProperties2 = new CommonViewProperties();

			ScaleFactor scaleFactor2 = new ScaleFactor();
			A.ScaleX scaleX2 = new A.ScaleX(){ Numerator = 100, Denominator = 100 };
			A.ScaleY scaleY2 = new A.ScaleY(){ Numerator = 100, Denominator = 100 };

			scaleFactor2.Append(scaleX2);
			scaleFactor2.Append(scaleY2);
			Origin origin2 = new Origin(){ X = 0L, Y = 0L };

			commonViewProperties2.Append(scaleFactor2);
			commonViewProperties2.Append(origin2);

			notesTextViewProperties1.Append(commonViewProperties2);
			GridSpacing gridSpacing1 = new GridSpacing(){ Cx = 78028800L, Cy = 78028800L };

			viewProperties1.Append(normalViewProperties1);
			viewProperties1.Append(slideViewProperties1);
			viewProperties1.Append(notesTextViewProperties1);
			viewProperties1.Append(gridSpacing1);

			viewPropertiesPart1.ViewProperties = viewProperties1;
		}

		// Generates content of extendedFilePropertiesPart1.
		private void GenerateExtendedFilePropertiesPart1Content(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
		{
			Ap.Properties properties1 = new Ap.Properties();
			properties1.AddNamespaceDeclaration("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
			Ap.TotalTime totalTime1 = new Ap.TotalTime();
			totalTime1.Text = "2";
			Ap.Words words1 = new Ap.Words();
			words1.Text = "1";
			Ap.Application application1 = new Ap.Application();
			application1.Text = "Microsoft Office PowerPoint";
			Ap.PresentationFormat presentationFormat1 = new Ap.PresentationFormat();
			presentationFormat1.Text = "On-screen Show (4:3)";
			Ap.Paragraphs paragraphs1 = new Ap.Paragraphs();
			paragraphs1.Text = "1";
			Ap.Slides slides1 = new Ap.Slides();
			slides1.Text = "1";
			Ap.Notes notes1 = new Ap.Notes();
			notes1.Text = "0";
			Ap.HiddenSlides hiddenSlides1 = new Ap.HiddenSlides();
			hiddenSlides1.Text = "0";
			Ap.MultimediaClips multimediaClips1 = new Ap.MultimediaClips();
			multimediaClips1.Text = "0";
			Ap.ScaleCrop scaleCrop1 = new Ap.ScaleCrop();
			scaleCrop1.Text = "false";

			Ap.HeadingPairs headingPairs1 = new Ap.HeadingPairs();

			Vt.VTVector vTVector1 = new Vt.VTVector(){ BaseType = Vt.VectorBaseValues.Variant, Size = (UInt32Value)4U };

			Vt.Variant variant1 = new Vt.Variant();
			Vt.VTLPSTR vTLPSTR1 = new Vt.VTLPSTR();
			vTLPSTR1.Text = "Theme";

			variant1.Append(vTLPSTR1);

			Vt.Variant variant2 = new Vt.Variant();
			Vt.VTInt32 vTInt321 = new Vt.VTInt32();
			vTInt321.Text = "1";

			variant2.Append(vTInt321);

			Vt.Variant variant3 = new Vt.Variant();
			Vt.VTLPSTR vTLPSTR2 = new Vt.VTLPSTR();
			vTLPSTR2.Text = "Slide Titles";

			variant3.Append(vTLPSTR2);

			Vt.Variant variant4 = new Vt.Variant();
			Vt.VTInt32 vTInt322 = new Vt.VTInt32();
			vTInt322.Text = "1";

			variant4.Append(vTInt322);

			vTVector1.Append(variant1);
			vTVector1.Append(variant2);
			vTVector1.Append(variant3);
			vTVector1.Append(variant4);

			headingPairs1.Append(vTVector1);

			Ap.TitlesOfParts titlesOfParts1 = new Ap.TitlesOfParts();

			Vt.VTVector vTVector2 = new Vt.VTVector(){ BaseType = Vt.VectorBaseValues.Lpstr, Size = (UInt32Value)2U };
			Vt.VTLPSTR vTLPSTR3 = new Vt.VTLPSTR();
			vTLPSTR3.Text = "Office Theme";
			Vt.VTLPSTR vTLPSTR4 = new Vt.VTLPSTR();
			vTLPSTR4.Text = "Subject";

			vTVector2.Append(vTLPSTR3);
			vTVector2.Append(vTLPSTR4);

			titlesOfParts1.Append(vTVector2);
			Ap.LinksUpToDate linksUpToDate1 = new Ap.LinksUpToDate();
			linksUpToDate1.Text = "false";
			Ap.SharedDocument sharedDocument1 = new Ap.SharedDocument();
			sharedDocument1.Text = "false";
			Ap.HyperlinksChanged hyperlinksChanged1 = new Ap.HyperlinksChanged();
			hyperlinksChanged1.Text = "false";
			Ap.ApplicationVersion applicationVersion1 = new Ap.ApplicationVersion();
			applicationVersion1.Text = "12.0000";

			properties1.Append(totalTime1);
			properties1.Append(words1);
			properties1.Append(application1);
			properties1.Append(presentationFormat1);
			properties1.Append(paragraphs1);
			properties1.Append(slides1);
			properties1.Append(notes1);
			properties1.Append(hiddenSlides1);
			properties1.Append(multimediaClips1);
			properties1.Append(scaleCrop1);
			properties1.Append(headingPairs1);
			properties1.Append(titlesOfParts1);
			properties1.Append(linksUpToDate1);
			properties1.Append(sharedDocument1);
			properties1.Append(hyperlinksChanged1);
			properties1.Append(applicationVersion1);

			extendedFilePropertiesPart1.Properties = properties1;
		}

		private void SetPackageProperties(OpenXmlPackage document)
		{
			document.PackageProperties.Creator = "Ian";
			document.PackageProperties.Title = "Subject";
			document.PackageProperties.Revision = "1";
			document.PackageProperties.Created = System.Xml.XmlConvert.ToDateTime("2013-03-28T12:19:08Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
			document.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime("2013-03-28T12:21:49Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
			document.PackageProperties.LastModifiedBy = "Ian";
		}

		#region Binary Data
		private string thumbnailPart1Data = "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AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";

		private string imagePart1Data = "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";

		private System.IO.Stream GetBinaryDataStream(string base64String)
		{
			return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
		}

		#endregion
*/
	}
}
