using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml;
using HW.Core.Models;
using A = DocumentFormat.OpenXml.Drawing;
using P14 = DocumentFormat.OpenXml.Office2010.PowerPoint;
using Ds = DocumentFormat.OpenXml.CustomXmlDataProperties;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;

namespace HW.Core.Util.Exporters
{
	public class GeneratedClass2
    {
        public void CreateParts(PresentationDocument document, IList<IReportPart> parts)
        {
            ThumbnailPart thumbnailPart1 = document.AddNewPart<ThumbnailPart>("image/jpeg", "rId2");
            GenerateThumbnailPart1Content(thumbnailPart1);

            PresentationPart presentationPart1 = document.AddPresentationPart();
            GeneratePresentationPart1Content(presentationPart1, parts);

            ViewPropertiesPart viewPropertiesPart1 = presentationPart1.AddNewPart<ViewPropertiesPart>("rId8");
            GenerateViewPropertiesPart1Content(viewPropertiesPart1);

            SlideMasterPart slideMasterPart1 = presentationPart1.AddNewPart<SlideMasterPart>("rId3");
            GenerateSlideMasterPart1Content(slideMasterPart1);

            ThemePart themePart1 = slideMasterPart1.AddNewPart<ThemePart>("rId3");
            GenerateThemePart1Content(themePart1);

            SlideLayoutPart slideLayoutPart1 = slideMasterPart1.AddNewPart<SlideLayoutPart>("rId2");
            GenerateSlideLayoutPart1Content(slideLayoutPart1);

            ImagePart imagePart1 = slideLayoutPart1.AddNewPart<ImagePart>("image/png", "rId3");
            GenerateImagePart1Content(imagePart1);

            ImagePart imagePart2 = slideLayoutPart1.AddNewPart<ImagePart>("image/png", "rId2");
            GenerateImagePart2Content(imagePart2);

            slideLayoutPart1.AddPart(slideMasterPart1, "rId1");

            SlideLayoutPart slideLayoutPart2 = slideMasterPart1.AddNewPart<SlideLayoutPart>("rId1");
            GenerateSlideLayoutPart2Content(slideLayoutPart2);

            slideLayoutPart2.AddPart(imagePart2, "rId2");

            slideLayoutPart2.AddPart(slideMasterPart1, "rId1");

            PresentationPropertiesPart presentationPropertiesPart1 = presentationPart1.AddNewPart<PresentationPropertiesPart>("rId7");
            GeneratePresentationPropertiesPart1Content(presentationPropertiesPart1);

            SlideMasterPart slideMasterPart2 = presentationPart1.AddNewPart<SlideMasterPart>("rId2");
            GenerateSlideMasterPart2Content(slideMasterPart2);

            ThemePart themePart2 = slideMasterPart2.AddNewPart<ThemePart>("rId2");
            GenerateThemePart2Content(themePart2);

            SlideLayoutPart slideLayoutPart3 = slideMasterPart2.AddNewPart<SlideLayoutPart>("rId1");
            GenerateSlideLayoutPart3Content(slideLayoutPart3);

            slideLayoutPart3.AddPart(slideMasterPart2, "rId1");

            CustomXmlPart customXmlPart1 = presentationPart1.AddNewPart<CustomXmlPart>("application/xml", "rId1");
            GenerateCustomXmlPart1Content(customXmlPart1);

            CustomXmlPropertiesPart customXmlPropertiesPart1 = customXmlPart1.AddNewPart<CustomXmlPropertiesPart>("rId1");
            GenerateCustomXmlPropertiesPart1Content(customXmlPropertiesPart1);

            NotesMasterPart notesMasterPart1 = presentationPart1.AddNewPart<NotesMasterPart>("rId6");
            GenerateNotesMasterPart1Content(notesMasterPart1);

            ThemePart themePart3 = notesMasterPart1.AddNewPart<ThemePart>("rId1");
            GenerateThemePart3Content(themePart3);
            
            UInt32Value slideID = 282;

            foreach (var p in parts) {
//	            SlidePart slidePart1 = presentationPart1.AddNewPart<SlidePart>("rId5");
            	SlidePart slidePart1 = presentationPart1.AddNewPart<SlidePart>("rId" + slideID);
	            GenerateSlidePart1Content(p.Subject, slidePart1);
	            
//	            ImagePart imagePart2 = slideLayoutPart1.AddNewPart<ImagePart>("image/png", "rId2");
//            	GenerateImagePart2Content(imagePart2);
            	
            	ReportPartEventArgs e = new ReportPartEventArgs(p.ReportPart);
            	OnUrlSet(e);
            	
            	ImagePart imagePart3 = slidePart1.AddNewPart<ImagePart>("image/gif", "rId2");
	            GenerateImagePart3Content(imagePart3, e.Url);
	
	            slidePart1.AddPart(slideLayoutPart1, "rId1");
	            slideID++;
            }

//            ImagePart imagePart3 = slidePart1.AddNewPart<ImagePart>("image/gif", "rId2");
//            GenerateImagePart3Content(imagePart3);
//
//            slidePart1.AddPart(slideLayoutPart1, "rId1");

            TableStylesPart tableStylesPart1 = presentationPart1.AddNewPart<TableStylesPart>("rId10");
            GenerateTableStylesPart1Content(tableStylesPart1);

            SlidePart slidePart2 = presentationPart1.AddNewPart<SlidePart>("rId4");
            GenerateSlidePart2Content(slidePart2);

            ImagePart imagePart4 = slidePart2.AddNewPart<ImagePart>("image/png", "rId3");
            GenerateImagePart4Content(imagePart4);

            slidePart2.AddPart(imagePart2, "rId2");

            slidePart2.AddPart(slideLayoutPart3, "rId1");

            presentationPart1.AddPart(themePart2, "rId9");

            ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId4");
            GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

            SetPackageProperties(document);
        }
        
		public event EventHandler<ReportPartEventArgs> UrlSet;
		
		protected virtual void OnUrlSet(ReportPartEventArgs e)
		{
			if (UrlSet != null) {
				UrlSet(this, e);
			}
		}

        // Generates content of thumbnailPart1.
        private void GenerateThumbnailPart1Content(ThumbnailPart thumbnailPart1)
        {
            System.IO.Stream data = GetBinaryDataStream(thumbnailPart1Data);
            thumbnailPart1.FeedData(data);
            data.Close();
        }

        // Generates content of presentationPart1.
        private void GeneratePresentationPart1Content(PresentationPart presentationPart1, IList<IReportPart> parts)
        {
            Presentation presentation1 = new Presentation(){ SaveSubsetFonts = true };
            presentation1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            presentation1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            presentation1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            SlideMasterIdList slideMasterIdList1 = new SlideMasterIdList();
            SlideMasterId slideMasterId1 = new SlideMasterId(){ Id = (UInt32Value)2147483648U, RelationshipId = "rId2" };
            SlideMasterId slideMasterId2 = new SlideMasterId(){ Id = (UInt32Value)2147483672U, RelationshipId = "rId3" };

            slideMasterIdList1.Append(slideMasterId1);
            slideMasterIdList1.Append(slideMasterId2);

            NotesMasterIdList notesMasterIdList1 = new NotesMasterIdList();
            NotesMasterId notesMasterId1 = new NotesMasterId(){ Id = "rId6" };

            notesMasterIdList1.Append(notesMasterId1);

            SlideIdList slideIdList1 = new SlideIdList();
            SlideId slideId1 = new SlideId(){ Id = (UInt32Value)258U, RelationshipId = "rId4" };
//            SlideId slideId2 = new SlideId(){ Id = (UInt32Value)282U, RelationshipId = "rId5" };

            slideIdList1.Append(slideId1);
            
            UInt32Value slideID = 282;
			foreach (var p in parts) {
				SlideId slideId2 = new SlideId(){ Id = (UInt32Value)slideID, RelationshipId = "rId" + slideID };
				slideID++;
				slideIdList1.Append(slideId2);
			}
            
//            slideIdList1.Append(slideId2);
            SlideSize slideSize1 = new SlideSize(){ Cx = 9144000, Cy = 6858000, Type = SlideSizeValues.Screen4x3 };
            NotesSize notesSize1 = new NotesSize(){ Cx = 6797675, Cy = 9874250 };

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

            PresentationExtensionList presentationExtensionList1 = new PresentationExtensionList();

            PresentationExtension presentationExtension1 = new PresentationExtension(){ Uri = "{EFAFB233-063F-42B5-8137-9DF3F51BA10A}" };

            OpenXmlUnknownElement openXmlUnknownElement1 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<p15:sldGuideLst xmlns:p15=\"http://schemas.microsoft.com/office/powerpoint/2012/main\"><p15:guide id=\"1\" orient=\"horz\" pos=\"3838\"><p15:clr><a:srgbClr val=\"A4A3A4\" xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\" /></p15:clr></p15:guide><p15:guide id=\"2\" pos=\"930\"><p15:clr><a:srgbClr val=\"A4A3A4\" xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\" /></p15:clr></p15:guide></p15:sldGuideLst>");

            presentationExtension1.Append(openXmlUnknownElement1);

            presentationExtensionList1.Append(presentationExtension1);

            presentation1.Append(slideMasterIdList1);
            presentation1.Append(notesMasterIdList1);
            presentation1.Append(slideIdList1);
            presentation1.Append(slideSize1);
            presentation1.Append(notesSize1);
            presentation1.Append(defaultTextStyle1);
            presentation1.Append(presentationExtensionList1);

            presentationPart1.Presentation = presentation1;
        }

        // Generates content of viewPropertiesPart1.
        private void GenerateViewPropertiesPart1Content(ViewPropertiesPart viewPropertiesPart1)
        {
            ViewProperties viewProperties1 = new ViewProperties();
            viewProperties1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            viewProperties1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            viewProperties1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            NormalViewProperties normalViewProperties1 = new NormalViewProperties(){ ShowOutlineIcons = false };
            RestoredLeft restoredLeft1 = new RestoredLeft(){ Size = 20584 };
            RestoredTop restoredTop1 = new RestoredTop(){ Size = 94649 };

            normalViewProperties1.Append(restoredLeft1);
            normalViewProperties1.Append(restoredTop1);

            SlideViewProperties slideViewProperties1 = new SlideViewProperties();

            CommonSlideViewProperties commonSlideViewProperties1 = new CommonSlideViewProperties();

            CommonViewProperties commonViewProperties1 = new CommonViewProperties(){ VariableScale = true };

            ScaleFactor scaleFactor1 = new ScaleFactor();
            A.ScaleX scaleX1 = new A.ScaleX(){ Numerator = 82, Denominator = 100 };
            A.ScaleY scaleY1 = new A.ScaleY(){ Numerator = 82, Denominator = 100 };

            scaleFactor1.Append(scaleX1);
            scaleFactor1.Append(scaleY1);
            Origin origin1 = new Origin(){ X = -78L, Y = -108L };

            commonViewProperties1.Append(scaleFactor1);
            commonViewProperties1.Append(origin1);

            GuideList guideList1 = new GuideList();
            Guide guide1 = new Guide(){ Orientation = DirectionValues.Horizontal, Position = 3838 };
            Guide guide2 = new Guide(){ Position = 930 };

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

        // Generates content of slideMasterPart1.
        private void GenerateSlideMasterPart1Content(SlideMasterPart slideMasterPart1)
        {
            SlideMaster slideMaster1 = new SlideMaster(){ Preserve = true };
            slideMaster1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            slideMaster1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            slideMaster1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            CommonSlideData commonSlideData1 = new CommonSlideData();

            Background background1 = new Background();

            BackgroundStyleReference backgroundStyleReference1 = new BackgroundStyleReference(){ Index = (UInt32Value)1001U };
            A.SchemeColor schemeColor10 = new A.SchemeColor(){ Val = A.SchemeColorValues.Background1 };

            backgroundStyleReference1.Append(schemeColor10);

            background1.Append(backgroundStyleReference1);

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
            NonVisualDrawingProperties nonVisualDrawingProperties2 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Platshållare för rubrik 1" };

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

            A.Transform2D transform2D1 = new A.Transform2D();
            A.Offset offset2 = new A.Offset(){ X = 457200L, Y = 274638L };
            A.Extents extents2 = new A.Extents(){ Cx = 8229600L, Cy = 1143000L };

            transform2D1.Append(offset2);
            transform2D1.Append(extents2);

            A.PresetGeometry presetGeometry1 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();

            presetGeometry1.Append(adjustValueList1);

            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(presetGeometry1);

            TextBody textBody1 = new TextBody();

            A.BodyProperties bodyProperties1 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };
            A.NormalAutoFit normalAutoFit1 = new A.NormalAutoFit();

            bodyProperties1.Append(normalAutoFit1);
            A.ListStyle listStyle1 = new A.ListStyle();

            A.Paragraph paragraph1 = new A.Paragraph();

            A.Run run1 = new A.Run();
            A.RunProperties runProperties1 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text1 = new A.Text();
            text1.Text = "Klicka här för att ändra format";

            run1.Append(runProperties1);
            run1.Append(text1);

            paragraph1.Append(run1);

            textBody1.Append(bodyProperties1);
            textBody1.Append(listStyle1);
            textBody1.Append(paragraph1);

            shape1.Append(nonVisualShapeProperties1);
            shape1.Append(shapeProperties1);
            shape1.Append(textBody1);

            Shape shape2 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties2 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties3 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Platshållare för text 2" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties2 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks2 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties2.Append(shapeLocks2);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties3 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape2 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Index = (UInt32Value)1U };

            applicationNonVisualDrawingProperties3.Append(placeholderShape2);

            nonVisualShapeProperties2.Append(nonVisualDrawingProperties3);
            nonVisualShapeProperties2.Append(nonVisualShapeDrawingProperties2);
            nonVisualShapeProperties2.Append(applicationNonVisualDrawingProperties3);

            ShapeProperties shapeProperties2 = new ShapeProperties();

            A.Transform2D transform2D2 = new A.Transform2D();
            A.Offset offset3 = new A.Offset(){ X = 457200L, Y = 1600200L };
            A.Extents extents3 = new A.Extents(){ Cx = 8229600L, Cy = 4525963L };

            transform2D2.Append(offset3);
            transform2D2.Append(extents3);

            A.PresetGeometry presetGeometry2 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList2 = new A.AdjustValueList();

            presetGeometry2.Append(adjustValueList2);

            shapeProperties2.Append(transform2D2);
            shapeProperties2.Append(presetGeometry2);

            TextBody textBody2 = new TextBody();

            A.BodyProperties bodyProperties2 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false };
            A.NormalAutoFit normalAutoFit2 = new A.NormalAutoFit();

            bodyProperties2.Append(normalAutoFit2);
            A.ListStyle listStyle2 = new A.ListStyle();

            A.Paragraph paragraph2 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties1 = new A.ParagraphProperties(){ Level = 0 };

            A.Run run2 = new A.Run();
            A.RunProperties runProperties2 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text2 = new A.Text();
            text2.Text = "Klicka här för att ändra format på bakgrundstexten";

            run2.Append(runProperties2);
            run2.Append(text2);

            paragraph2.Append(paragraphProperties1);
            paragraph2.Append(run2);

            A.Paragraph paragraph3 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties2 = new A.ParagraphProperties(){ Level = 1 };

            A.Run run3 = new A.Run();
            A.RunProperties runProperties3 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text3 = new A.Text();
            text3.Text = "Nivå två";

            run3.Append(runProperties3);
            run3.Append(text3);

            paragraph3.Append(paragraphProperties2);
            paragraph3.Append(run3);

            A.Paragraph paragraph4 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties3 = new A.ParagraphProperties(){ Level = 2 };

            A.Run run4 = new A.Run();
            A.RunProperties runProperties4 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text4 = new A.Text();
            text4.Text = "Nivå tre";

            run4.Append(runProperties4);
            run4.Append(text4);

            paragraph4.Append(paragraphProperties3);
            paragraph4.Append(run4);

            A.Paragraph paragraph5 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties4 = new A.ParagraphProperties(){ Level = 3 };

            A.Run run5 = new A.Run();
            A.RunProperties runProperties5 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text5 = new A.Text();
            text5.Text = "Nivå fyra";

            run5.Append(runProperties5);
            run5.Append(text5);

            paragraph5.Append(paragraphProperties4);
            paragraph5.Append(run5);

            A.Paragraph paragraph6 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties5 = new A.ParagraphProperties(){ Level = 4 };

            A.Run run6 = new A.Run();
            A.RunProperties runProperties6 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text6 = new A.Text();
            text6.Text = "Nivå fem";

            run6.Append(runProperties6);
            run6.Append(text6);

            paragraph6.Append(paragraphProperties5);
            paragraph6.Append(run6);

            textBody2.Append(bodyProperties2);
            textBody2.Append(listStyle2);
            textBody2.Append(paragraph2);
            textBody2.Append(paragraph3);
            textBody2.Append(paragraph4);
            textBody2.Append(paragraph5);
            textBody2.Append(paragraph6);

            shape2.Append(nonVisualShapeProperties2);
            shape2.Append(shapeProperties2);
            shape2.Append(textBody2);

            Shape shape3 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties3 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties4 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Platshållare för datum 3" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties3 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks3 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties3.Append(shapeLocks3);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties4 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape3 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)2U };

            applicationNonVisualDrawingProperties4.Append(placeholderShape3);

            nonVisualShapeProperties3.Append(nonVisualDrawingProperties4);
            nonVisualShapeProperties3.Append(nonVisualShapeDrawingProperties3);
            nonVisualShapeProperties3.Append(applicationNonVisualDrawingProperties4);

            ShapeProperties shapeProperties3 = new ShapeProperties();

            A.Transform2D transform2D3 = new A.Transform2D();
            A.Offset offset4 = new A.Offset(){ X = 457200L, Y = 6356350L };
            A.Extents extents4 = new A.Extents(){ Cx = 2133600L, Cy = 365125L };

            transform2D3.Append(offset4);
            transform2D3.Append(extents4);

            A.PresetGeometry presetGeometry3 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList3 = new A.AdjustValueList();

            presetGeometry3.Append(adjustValueList3);

            shapeProperties3.Append(transform2D3);
            shapeProperties3.Append(presetGeometry3);

            TextBody textBody3 = new TextBody();
            A.BodyProperties bodyProperties3 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };

            A.ListStyle listStyle3 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties2 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Left };

            A.DefaultRunProperties defaultRunProperties11 = new A.DefaultRunProperties(){ FontSize = 1200 };

            A.SolidFill solidFill10 = new A.SolidFill();

            A.SchemeColor schemeColor11 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint1 = new A.Tint(){ Val = 75000 };

            schemeColor11.Append(tint1);

            solidFill10.Append(schemeColor11);

            defaultRunProperties11.Append(solidFill10);

            level1ParagraphProperties2.Append(defaultRunProperties11);

            listStyle3.Append(level1ParagraphProperties2);

            A.Paragraph paragraph7 = new A.Paragraph();

            A.Field field1 = new A.Field(){ Id = "{D8B05ACB-391D-42A6-B02F-D1BCC6363F82}", Type = "datetimeFigureOut" };
            A.RunProperties runProperties7 = new A.RunProperties(){ Language = "sv-SE", SmartTagClean = false };
            A.ParagraphProperties paragraphProperties6 = new A.ParagraphProperties();
            A.Text text7 = new A.Text();
            text7.Text = "2016-10-21";

            field1.Append(runProperties7);
            field1.Append(paragraphProperties6);
            field1.Append(text7);
            A.EndParagraphRunProperties endParagraphRunProperties1 = new A.EndParagraphRunProperties(){ Language = "sv-SE", Dirty = false };

            paragraph7.Append(field1);
            paragraph7.Append(endParagraphRunProperties1);

            textBody3.Append(bodyProperties3);
            textBody3.Append(listStyle3);
            textBody3.Append(paragraph7);

            shape3.Append(nonVisualShapeProperties3);
            shape3.Append(shapeProperties3);
            shape3.Append(textBody3);

            Shape shape4 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties4 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties5 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Platshållare för sidfot 4" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties4 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks4 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties4.Append(shapeLocks4);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties5 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape4 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)3U };

            applicationNonVisualDrawingProperties5.Append(placeholderShape4);

            nonVisualShapeProperties4.Append(nonVisualDrawingProperties5);
            nonVisualShapeProperties4.Append(nonVisualShapeDrawingProperties4);
            nonVisualShapeProperties4.Append(applicationNonVisualDrawingProperties5);

            ShapeProperties shapeProperties4 = new ShapeProperties();

            A.Transform2D transform2D4 = new A.Transform2D();
            A.Offset offset5 = new A.Offset(){ X = 3124200L, Y = 6356350L };
            A.Extents extents5 = new A.Extents(){ Cx = 2895600L, Cy = 365125L };

            transform2D4.Append(offset5);
            transform2D4.Append(extents5);

            A.PresetGeometry presetGeometry4 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList4 = new A.AdjustValueList();

            presetGeometry4.Append(adjustValueList4);

            shapeProperties4.Append(transform2D4);
            shapeProperties4.Append(presetGeometry4);

            TextBody textBody4 = new TextBody();
            A.BodyProperties bodyProperties4 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };

            A.ListStyle listStyle4 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties3 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Center };

            A.DefaultRunProperties defaultRunProperties12 = new A.DefaultRunProperties(){ FontSize = 1200 };

            A.SolidFill solidFill11 = new A.SolidFill();

            A.SchemeColor schemeColor12 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint2 = new A.Tint(){ Val = 75000 };

            schemeColor12.Append(tint2);

            solidFill11.Append(schemeColor12);

            defaultRunProperties12.Append(solidFill11);

            level1ParagraphProperties3.Append(defaultRunProperties12);

            listStyle4.Append(level1ParagraphProperties3);

            A.Paragraph paragraph8 = new A.Paragraph();
            A.EndParagraphRunProperties endParagraphRunProperties2 = new A.EndParagraphRunProperties(){ Language = "sv-SE" };

            paragraph8.Append(endParagraphRunProperties2);

            textBody4.Append(bodyProperties4);
            textBody4.Append(listStyle4);
            textBody4.Append(paragraph8);

            shape4.Append(nonVisualShapeProperties4);
            shape4.Append(shapeProperties4);
            shape4.Append(textBody4);

            Shape shape5 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties5 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties6 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Platshållare för bildnummer 5" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties5 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks5 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties5.Append(shapeLocks5);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties6 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape5 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)4U };

            applicationNonVisualDrawingProperties6.Append(placeholderShape5);

            nonVisualShapeProperties5.Append(nonVisualDrawingProperties6);
            nonVisualShapeProperties5.Append(nonVisualShapeDrawingProperties5);
            nonVisualShapeProperties5.Append(applicationNonVisualDrawingProperties6);

            ShapeProperties shapeProperties5 = new ShapeProperties();

            A.Transform2D transform2D5 = new A.Transform2D();
            A.Offset offset6 = new A.Offset(){ X = 6553200L, Y = 6356350L };
            A.Extents extents6 = new A.Extents(){ Cx = 2133600L, Cy = 365125L };

            transform2D5.Append(offset6);
            transform2D5.Append(extents6);

            A.PresetGeometry presetGeometry5 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList5 = new A.AdjustValueList();

            presetGeometry5.Append(adjustValueList5);

            shapeProperties5.Append(transform2D5);
            shapeProperties5.Append(presetGeometry5);

            TextBody textBody5 = new TextBody();
            A.BodyProperties bodyProperties5 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };

            A.ListStyle listStyle5 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties4 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Right };

            A.DefaultRunProperties defaultRunProperties13 = new A.DefaultRunProperties(){ FontSize = 1200 };

            A.SolidFill solidFill12 = new A.SolidFill();

            A.SchemeColor schemeColor13 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint3 = new A.Tint(){ Val = 75000 };

            schemeColor13.Append(tint3);

            solidFill12.Append(schemeColor13);

            defaultRunProperties13.Append(solidFill12);

            level1ParagraphProperties4.Append(defaultRunProperties13);

            listStyle5.Append(level1ParagraphProperties4);

            A.Paragraph paragraph9 = new A.Paragraph();

            A.Field field2 = new A.Field(){ Id = "{28C31C57-8128-4495-8BED-DE23F8D413CB}", Type = "slidenum" };
            A.RunProperties runProperties8 = new A.RunProperties(){ Language = "sv-SE", SmartTagClean = false };
            A.ParagraphProperties paragraphProperties7 = new A.ParagraphProperties();
            A.Text text8 = new A.Text();
            text8.Text = "‹#›";

            field2.Append(runProperties8);
            field2.Append(paragraphProperties7);
            field2.Append(text8);
            A.EndParagraphRunProperties endParagraphRunProperties3 = new A.EndParagraphRunProperties(){ Language = "sv-SE" };

            paragraph9.Append(field2);
            paragraph9.Append(endParagraphRunProperties3);

            textBody5.Append(bodyProperties5);
            textBody5.Append(listStyle5);
            textBody5.Append(paragraph9);

            shape5.Append(nonVisualShapeProperties5);
            shape5.Append(shapeProperties5);
            shape5.Append(textBody5);

            shapeTree1.Append(nonVisualGroupShapeProperties1);
            shapeTree1.Append(groupShapeProperties1);
            shapeTree1.Append(shape1);
            shapeTree1.Append(shape2);
            shapeTree1.Append(shape3);
            shapeTree1.Append(shape4);
            shapeTree1.Append(shape5);

            CommonSlideDataExtensionList commonSlideDataExtensionList1 = new CommonSlideDataExtensionList();

            CommonSlideDataExtension commonSlideDataExtension1 = new CommonSlideDataExtension(){ Uri = "{BB962C8B-B14F-4D97-AF65-F5344CB8AC3E}" };

            P14.CreationId creationId1 = new P14.CreationId(){ Val = (UInt32Value)1552228110U };
            creationId1.AddNamespaceDeclaration("p14", "http://schemas.microsoft.com/office/powerpoint/2010/main");

            commonSlideDataExtension1.Append(creationId1);

            commonSlideDataExtensionList1.Append(commonSlideDataExtension1);

            commonSlideData1.Append(background1);
            commonSlideData1.Append(shapeTree1);
            commonSlideData1.Append(commonSlideDataExtensionList1);
            ColorMap colorMap1 = new ColorMap(){ Background1 = A.ColorSchemeIndexValues.Light1, Text1 = A.ColorSchemeIndexValues.Dark1, Background2 = A.ColorSchemeIndexValues.Light2, Text2 = A.ColorSchemeIndexValues.Dark2, Accent1 = A.ColorSchemeIndexValues.Accent1, Accent2 = A.ColorSchemeIndexValues.Accent2, Accent3 = A.ColorSchemeIndexValues.Accent3, Accent4 = A.ColorSchemeIndexValues.Accent4, Accent5 = A.ColorSchemeIndexValues.Accent5, Accent6 = A.ColorSchemeIndexValues.Accent6, Hyperlink = A.ColorSchemeIndexValues.Hyperlink, FollowedHyperlink = A.ColorSchemeIndexValues.FollowedHyperlink };

            SlideLayoutIdList slideLayoutIdList1 = new SlideLayoutIdList();
            SlideLayoutId slideLayoutId1 = new SlideLayoutId(){ Id = (UInt32Value)2147483677U, RelationshipId = "rId1" };
            SlideLayoutId slideLayoutId2 = new SlideLayoutId(){ Id = (UInt32Value)2147483678U, RelationshipId = "rId2" };

            slideLayoutIdList1.Append(slideLayoutId1);
            slideLayoutIdList1.Append(slideLayoutId2);

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
            A.DefaultRunProperties defaultRunProperties24 = new A.DefaultRunProperties(){ Language = "sv-SE" };

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

            slideMaster1.Append(commonSlideData1);
            slideMaster1.Append(colorMap1);
            slideMaster1.Append(slideLayoutIdList1);
            slideMaster1.Append(textStyles1);

            slideMasterPart1.SlideMaster = slideMaster1;
        }

        // Generates content of themePart1.
        private void GenerateThemePart1Content(ThemePart themePart1)
        {
            A.Theme theme1 = new A.Theme(){ Name = "1_Anpassad formgivning" };
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
            A.SupplementalFont supplementalFont30 = new A.SupplementalFont(){ Script = "Geor", Typeface = "Sylfaen" };

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
            majorFont1.Append(supplementalFont30);

            A.MinorFont minorFont1 = new A.MinorFont();
            A.LatinFont latinFont30 = new A.LatinFont(){ Typeface = "Calibri" };
            A.EastAsianFont eastAsianFont30 = new A.EastAsianFont(){ Typeface = "" };
            A.ComplexScriptFont complexScriptFont30 = new A.ComplexScriptFont(){ Typeface = "" };
            A.SupplementalFont supplementalFont31 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
            A.SupplementalFont supplementalFont32 = new A.SupplementalFont(){ Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont33 = new A.SupplementalFont(){ Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont34 = new A.SupplementalFont(){ Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont35 = new A.SupplementalFont(){ Script = "Arab", Typeface = "Arial" };
            A.SupplementalFont supplementalFont36 = new A.SupplementalFont(){ Script = "Hebr", Typeface = "Arial" };
            A.SupplementalFont supplementalFont37 = new A.SupplementalFont(){ Script = "Thai", Typeface = "Cordia New" };
            A.SupplementalFont supplementalFont38 = new A.SupplementalFont(){ Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont39 = new A.SupplementalFont(){ Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont40 = new A.SupplementalFont(){ Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont41 = new A.SupplementalFont(){ Script = "Khmr", Typeface = "DaunPenh" };
            A.SupplementalFont supplementalFont42 = new A.SupplementalFont(){ Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont43 = new A.SupplementalFont(){ Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont44 = new A.SupplementalFont(){ Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont45 = new A.SupplementalFont(){ Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont46 = new A.SupplementalFont(){ Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont47 = new A.SupplementalFont(){ Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont48 = new A.SupplementalFont(){ Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont49 = new A.SupplementalFont(){ Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont50 = new A.SupplementalFont(){ Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont51 = new A.SupplementalFont(){ Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont52 = new A.SupplementalFont(){ Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont53 = new A.SupplementalFont(){ Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont54 = new A.SupplementalFont(){ Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont55 = new A.SupplementalFont(){ Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont56 = new A.SupplementalFont(){ Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont57 = new A.SupplementalFont(){ Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont58 = new A.SupplementalFont(){ Script = "Viet", Typeface = "Arial" };
            A.SupplementalFont supplementalFont59 = new A.SupplementalFont(){ Script = "Uigh", Typeface = "Microsoft Uighur" };
            A.SupplementalFont supplementalFont60 = new A.SupplementalFont(){ Script = "Geor", Typeface = "Sylfaen" };

            minorFont1.Append(latinFont30);
            minorFont1.Append(eastAsianFont30);
            minorFont1.Append(complexScriptFont30);
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
            minorFont1.Append(supplementalFont59);
            minorFont1.Append(supplementalFont60);

            fontScheme1.Append(majorFont1);
            fontScheme1.Append(minorFont1);

            A.FormatScheme formatScheme1 = new A.FormatScheme(){ Name = "Office" };

            A.FillStyleList fillStyleList1 = new A.FillStyleList();

            A.SolidFill solidFill32 = new A.SolidFill();
            A.SchemeColor schemeColor33 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill32.Append(schemeColor33);

            A.GradientFill gradientFill1 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList1 = new A.GradientStopList();

            A.GradientStop gradientStop1 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor34 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint4 = new A.Tint(){ Val = 50000 };
            A.SaturationModulation saturationModulation1 = new A.SaturationModulation(){ Val = 300000 };

            schemeColor34.Append(tint4);
            schemeColor34.Append(saturationModulation1);

            gradientStop1.Append(schemeColor34);

            A.GradientStop gradientStop2 = new A.GradientStop(){ Position = 35000 };

            A.SchemeColor schemeColor35 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint5 = new A.Tint(){ Val = 37000 };
            A.SaturationModulation saturationModulation2 = new A.SaturationModulation(){ Val = 300000 };

            schemeColor35.Append(tint5);
            schemeColor35.Append(saturationModulation2);

            gradientStop2.Append(schemeColor35);

            A.GradientStop gradientStop3 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor36 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint6 = new A.Tint(){ Val = 15000 };
            A.SaturationModulation saturationModulation3 = new A.SaturationModulation(){ Val = 350000 };

            schemeColor36.Append(tint6);
            schemeColor36.Append(saturationModulation3);

            gradientStop3.Append(schemeColor36);

            gradientStopList1.Append(gradientStop1);
            gradientStopList1.Append(gradientStop2);
            gradientStopList1.Append(gradientStop3);
            A.LinearGradientFill linearGradientFill1 = new A.LinearGradientFill(){ Angle = 16200000, Scaled = true };

            gradientFill1.Append(gradientStopList1);
            gradientFill1.Append(linearGradientFill1);

            A.GradientFill gradientFill2 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList2 = new A.GradientStopList();

            A.GradientStop gradientStop4 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor37 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade1 = new A.Shade(){ Val = 51000 };
            A.SaturationModulation saturationModulation4 = new A.SaturationModulation(){ Val = 130000 };

            schemeColor37.Append(shade1);
            schemeColor37.Append(saturationModulation4);

            gradientStop4.Append(schemeColor37);

            A.GradientStop gradientStop5 = new A.GradientStop(){ Position = 80000 };

            A.SchemeColor schemeColor38 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade2 = new A.Shade(){ Val = 93000 };
            A.SaturationModulation saturationModulation5 = new A.SaturationModulation(){ Val = 130000 };

            schemeColor38.Append(shade2);
            schemeColor38.Append(saturationModulation5);

            gradientStop5.Append(schemeColor38);

            A.GradientStop gradientStop6 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor39 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade3 = new A.Shade(){ Val = 94000 };
            A.SaturationModulation saturationModulation6 = new A.SaturationModulation(){ Val = 135000 };

            schemeColor39.Append(shade3);
            schemeColor39.Append(saturationModulation6);

            gradientStop6.Append(schemeColor39);

            gradientStopList2.Append(gradientStop4);
            gradientStopList2.Append(gradientStop5);
            gradientStopList2.Append(gradientStop6);
            A.LinearGradientFill linearGradientFill2 = new A.LinearGradientFill(){ Angle = 16200000, Scaled = false };

            gradientFill2.Append(gradientStopList2);
            gradientFill2.Append(linearGradientFill2);

            fillStyleList1.Append(solidFill32);
            fillStyleList1.Append(gradientFill1);
            fillStyleList1.Append(gradientFill2);

            A.LineStyleList lineStyleList1 = new A.LineStyleList();

            A.Outline outline1 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill33 = new A.SolidFill();

            A.SchemeColor schemeColor40 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade4 = new A.Shade(){ Val = 95000 };
            A.SaturationModulation saturationModulation7 = new A.SaturationModulation(){ Val = 105000 };

            schemeColor40.Append(shade4);
            schemeColor40.Append(saturationModulation7);

            solidFill33.Append(schemeColor40);
            A.PresetDash presetDash1 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

            outline1.Append(solidFill33);
            outline1.Append(presetDash1);

            A.Outline outline2 = new A.Outline(){ Width = 25400, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill34 = new A.SolidFill();
            A.SchemeColor schemeColor41 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill34.Append(schemeColor41);
            A.PresetDash presetDash2 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

            outline2.Append(solidFill34);
            outline2.Append(presetDash2);

            A.Outline outline3 = new A.Outline(){ Width = 38100, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill35 = new A.SolidFill();
            A.SchemeColor schemeColor42 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill35.Append(schemeColor42);
            A.PresetDash presetDash3 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

            outline3.Append(solidFill35);
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

            A.SolidFill solidFill36 = new A.SolidFill();
            A.SchemeColor schemeColor43 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill36.Append(schemeColor43);

            A.GradientFill gradientFill3 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList3 = new A.GradientStopList();

            A.GradientStop gradientStop7 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor44 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint7 = new A.Tint(){ Val = 40000 };
            A.SaturationModulation saturationModulation8 = new A.SaturationModulation(){ Val = 350000 };

            schemeColor44.Append(tint7);
            schemeColor44.Append(saturationModulation8);

            gradientStop7.Append(schemeColor44);

            A.GradientStop gradientStop8 = new A.GradientStop(){ Position = 40000 };

            A.SchemeColor schemeColor45 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint8 = new A.Tint(){ Val = 45000 };
            A.Shade shade5 = new A.Shade(){ Val = 99000 };
            A.SaturationModulation saturationModulation9 = new A.SaturationModulation(){ Val = 350000 };

            schemeColor45.Append(tint8);
            schemeColor45.Append(shade5);
            schemeColor45.Append(saturationModulation9);

            gradientStop8.Append(schemeColor45);

            A.GradientStop gradientStop9 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor46 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade6 = new A.Shade(){ Val = 20000 };
            A.SaturationModulation saturationModulation10 = new A.SaturationModulation(){ Val = 255000 };

            schemeColor46.Append(shade6);
            schemeColor46.Append(saturationModulation10);

            gradientStop9.Append(schemeColor46);

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

            A.SchemeColor schemeColor47 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint9 = new A.Tint(){ Val = 80000 };
            A.SaturationModulation saturationModulation11 = new A.SaturationModulation(){ Val = 300000 };

            schemeColor47.Append(tint9);
            schemeColor47.Append(saturationModulation11);

            gradientStop10.Append(schemeColor47);

            A.GradientStop gradientStop11 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor48 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade7 = new A.Shade(){ Val = 30000 };
            A.SaturationModulation saturationModulation12 = new A.SaturationModulation(){ Val = 200000 };

            schemeColor48.Append(shade7);
            schemeColor48.Append(saturationModulation12);

            gradientStop11.Append(schemeColor48);

            gradientStopList4.Append(gradientStop10);
            gradientStopList4.Append(gradientStop11);

            A.PathGradientFill pathGradientFill2 = new A.PathGradientFill(){ Path = A.PathShadeValues.Circle };
            A.FillToRectangle fillToRectangle2 = new A.FillToRectangle(){ Left = 50000, Top = 50000, Right = 50000, Bottom = 50000 };

            pathGradientFill2.Append(fillToRectangle2);

            gradientFill4.Append(gradientStopList4);
            gradientFill4.Append(pathGradientFill2);

            backgroundFillStyleList1.Append(solidFill36);
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

        // Generates content of slideLayoutPart1.
        private void GenerateSlideLayoutPart1Content(SlideLayoutPart slideLayoutPart1)
        {
            SlideLayout slideLayout1 = new SlideLayout(){ UserDrawn = true };
            slideLayout1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            slideLayout1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            slideLayout1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            CommonSlideData commonSlideData2 = new CommonSlideData(){ Name = "Rubrik och text" };

            ShapeTree shapeTree2 = new ShapeTree();

            NonVisualGroupShapeProperties nonVisualGroupShapeProperties2 = new NonVisualGroupShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties7 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
            NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties2 = new NonVisualGroupShapeDrawingProperties();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties7 = new ApplicationNonVisualDrawingProperties();

            nonVisualGroupShapeProperties2.Append(nonVisualDrawingProperties7);
            nonVisualGroupShapeProperties2.Append(nonVisualGroupShapeDrawingProperties2);
            nonVisualGroupShapeProperties2.Append(applicationNonVisualDrawingProperties7);

            GroupShapeProperties groupShapeProperties2 = new GroupShapeProperties();

            A.TransformGroup transformGroup2 = new A.TransformGroup();
            A.Offset offset7 = new A.Offset(){ X = 0L, Y = 0L };
            A.Extents extents7 = new A.Extents(){ Cx = 0L, Cy = 0L };
            A.ChildOffset childOffset2 = new A.ChildOffset(){ X = 0L, Y = 0L };
            A.ChildExtents childExtents2 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

            transformGroup2.Append(offset7);
            transformGroup2.Append(extents7);
            transformGroup2.Append(childOffset2);
            transformGroup2.Append(childExtents2);

            groupShapeProperties2.Append(transformGroup2);

            Picture picture1 = new Picture();

            NonVisualPictureProperties nonVisualPictureProperties1 = new NonVisualPictureProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties8 = new NonVisualDrawingProperties(){ Id = (UInt32Value)12U, Name = "Picture 3", Description = "C:\\Documents and Settings\\Antti Salmi\\Desktop\\hw_ppt_imgs\\hw_logo.png" };

            NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties1 = new NonVisualPictureDrawingProperties();
            A.PictureLocks pictureLocks1 = new A.PictureLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

            nonVisualPictureDrawingProperties1.Append(pictureLocks1);
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties8 = new ApplicationNonVisualDrawingProperties(){ UserDrawn = true };

            nonVisualPictureProperties1.Append(nonVisualDrawingProperties8);
            nonVisualPictureProperties1.Append(nonVisualPictureDrawingProperties1);
            nonVisualPictureProperties1.Append(applicationNonVisualDrawingProperties8);

            BlipFill blipFill1 = new BlipFill();
            A.Blip blip1 = new A.Blip(){ Embed = "rId2", CompressionState = A.BlipCompressionValues.Print };
            A.SourceRectangle sourceRectangle1 = new A.SourceRectangle();

            A.Stretch stretch1 = new A.Stretch();
            A.FillRectangle fillRectangle1 = new A.FillRectangle();

            stretch1.Append(fillRectangle1);

            blipFill1.Append(blip1);
            blipFill1.Append(sourceRectangle1);
            blipFill1.Append(stretch1);

            ShapeProperties shapeProperties6 = new ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D6 = new A.Transform2D();
            A.Offset offset8 = new A.Offset(){ X = 285720L, Y = 6325486L };
            A.Extents extents8 = new A.Extents(){ Cx = 571504L, Cy = 389662L };

            transform2D6.Append(offset8);
            transform2D6.Append(extents8);

            A.PresetGeometry presetGeometry6 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList6 = new A.AdjustValueList();

            presetGeometry6.Append(adjustValueList6);
            A.NoFill noFill1 = new A.NoFill();

            shapeProperties6.Append(transform2D6);
            shapeProperties6.Append(presetGeometry6);
            shapeProperties6.Append(noFill1);

            picture1.Append(nonVisualPictureProperties1);
            picture1.Append(blipFill1);
            picture1.Append(shapeProperties6);

            Shape shape6 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties6 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties9 = new NonVisualDrawingProperties(){ Id = (UInt32Value)13U, Name = "Rectangle 10" };
            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties6 = new NonVisualShapeDrawingProperties();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties9 = new ApplicationNonVisualDrawingProperties(){ UserDrawn = true };

            nonVisualShapeProperties6.Append(nonVisualDrawingProperties9);
            nonVisualShapeProperties6.Append(nonVisualShapeDrawingProperties6);
            nonVisualShapeProperties6.Append(applicationNonVisualDrawingProperties9);

            ShapeProperties shapeProperties7 = new ShapeProperties();

            A.Transform2D transform2D7 = new A.Transform2D();
            A.Offset offset9 = new A.Offset(){ X = 142844L, Y = 6143644L };
            A.Extents extents9 = new A.Extents(){ Cx = 8858312L, Cy = 45719L };

            transform2D7.Append(offset9);
            transform2D7.Append(extents9);

            A.PresetGeometry presetGeometry7 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList7 = new A.AdjustValueList();

            presetGeometry7.Append(adjustValueList7);

            A.SolidFill solidFill37 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex14 = new A.RgbColorModelHex(){ Val = "ECECEC" };

            solidFill37.Append(rgbColorModelHex14);

            A.Outline outline4 = new A.Outline();
            A.NoFill noFill2 = new A.NoFill();

            outline4.Append(noFill2);

            shapeProperties7.Append(transform2D7);
            shapeProperties7.Append(presetGeometry7);
            shapeProperties7.Append(solidFill37);
            shapeProperties7.Append(outline4);

            ShapeStyle shapeStyle1 = new ShapeStyle();

            A.LineReference lineReference1 = new A.LineReference(){ Index = (UInt32Value)2U };

            A.SchemeColor schemeColor49 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };
            A.Shade shade8 = new A.Shade(){ Val = 50000 };

            schemeColor49.Append(shade8);

            lineReference1.Append(schemeColor49);

            A.FillReference fillReference1 = new A.FillReference(){ Index = (UInt32Value)1U };
            A.SchemeColor schemeColor50 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };

            fillReference1.Append(schemeColor50);

            A.EffectReference effectReference1 = new A.EffectReference(){ Index = (UInt32Value)0U };
            A.SchemeColor schemeColor51 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };

            effectReference1.Append(schemeColor51);

            A.FontReference fontReference1 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };
            A.SchemeColor schemeColor52 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            fontReference1.Append(schemeColor52);

            shapeStyle1.Append(lineReference1);
            shapeStyle1.Append(fillReference1);
            shapeStyle1.Append(effectReference1);
            shapeStyle1.Append(fontReference1);

            TextBody textBody6 = new TextBody();
            A.BodyProperties bodyProperties6 = new A.BodyProperties(){ RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };
            A.ListStyle listStyle6 = new A.ListStyle();

            A.Paragraph paragraph10 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties8 = new A.ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Center };
            A.EndParagraphRunProperties endParagraphRunProperties4 = new A.EndParagraphRunProperties(){ Language = "en-GB" };

            paragraph10.Append(paragraphProperties8);
            paragraph10.Append(endParagraphRunProperties4);

            textBody6.Append(bodyProperties6);
            textBody6.Append(listStyle6);
            textBody6.Append(paragraph10);

            shape6.Append(nonVisualShapeProperties6);
            shape6.Append(shapeProperties7);
            shape6.Append(shapeStyle1);
            shape6.Append(textBody6);

            Shape shape7 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties7 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties10 = new NonVisualDrawingProperties(){ Id = (UInt32Value)15U, Name = "TextBox 14" };
            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties7 = new NonVisualShapeDrawingProperties(){ TextBox = true };
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties10 = new ApplicationNonVisualDrawingProperties(){ UserDrawn = true };

            nonVisualShapeProperties7.Append(nonVisualDrawingProperties10);
            nonVisualShapeProperties7.Append(nonVisualShapeDrawingProperties7);
            nonVisualShapeProperties7.Append(applicationNonVisualDrawingProperties10);

            ShapeProperties shapeProperties8 = new ShapeProperties();

            A.Transform2D transform2D8 = new A.Transform2D();
            A.Offset offset10 = new A.Offset(){ X = 6572264L, Y = 6396334L };
            A.Extents extents10 = new A.Extents(){ Cx = 2357454L, Cy = 215444L };

            transform2D8.Append(offset10);
            transform2D8.Append(extents10);

            A.PresetGeometry presetGeometry8 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList8 = new A.AdjustValueList();

            presetGeometry8.Append(adjustValueList8);
            A.NoFill noFill3 = new A.NoFill();

            shapeProperties8.Append(transform2D8);
            shapeProperties8.Append(presetGeometry8);
            shapeProperties8.Append(noFill3);

            TextBody textBody7 = new TextBody();

            A.BodyProperties bodyProperties7 = new A.BodyProperties(){ Wrap = A.TextWrappingValues.Square, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Top };
            A.ShapeAutoFit shapeAutoFit1 = new A.ShapeAutoFit();

            bodyProperties7.Append(shapeAutoFit1);
            A.ListStyle listStyle7 = new A.ListStyle();

            A.Paragraph paragraph11 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties9 = new A.ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Right };

            A.Run run7 = new A.Run();

            A.RunProperties runProperties9 = new A.RunProperties(){ Language = "fi-FI", FontSize = 800, Dirty = false, SmartTagClean = false };

            A.SolidFill solidFill38 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex15 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill38.Append(rgbColorModelHex15);
            A.LatinFont latinFont31 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.ComplexScriptFont complexScriptFont31 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            runProperties9.Append(solidFill38);
            runProperties9.Append(latinFont31);
            runProperties9.Append(complexScriptFont31);
            A.Text text9 = new A.Text();
            text9.Text = "Page ";

            run7.Append(runProperties9);
            run7.Append(text9);

            A.Field field3 = new A.Field(){ Id = "{02752316-B598-4E7D-8A69-5E514050A2A8}", Type = "slidenum" };

            A.RunProperties runProperties10 = new A.RunProperties(){ Language = "fi-FI", FontSize = 800, SmartTagClean = false };

            A.SolidFill solidFill39 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex16 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill39.Append(rgbColorModelHex16);
            A.LatinFont latinFont32 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.ComplexScriptFont complexScriptFont32 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            runProperties10.Append(solidFill39);
            runProperties10.Append(latinFont32);
            runProperties10.Append(complexScriptFont32);
            A.ParagraphProperties paragraphProperties10 = new A.ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Right };
            A.Text text10 = new A.Text();
            text10.Text = "‹#›";

            field3.Append(runProperties10);
            field3.Append(paragraphProperties10);
            field3.Append(text10);

            A.EndParagraphRunProperties endParagraphRunProperties5 = new A.EndParagraphRunProperties(){ Language = "en-GB", FontSize = 800, Dirty = false, SmartTagClean = false };

            A.SolidFill solidFill40 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex17 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill40.Append(rgbColorModelHex17);
            A.LatinFont latinFont33 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.ComplexScriptFont complexScriptFont33 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            endParagraphRunProperties5.Append(solidFill40);
            endParagraphRunProperties5.Append(latinFont33);
            endParagraphRunProperties5.Append(complexScriptFont33);

            paragraph11.Append(paragraphProperties9);
            paragraph11.Append(run7);
            paragraph11.Append(field3);
            paragraph11.Append(endParagraphRunProperties5);

            textBody7.Append(bodyProperties7);
            textBody7.Append(listStyle7);
            textBody7.Append(paragraph11);

            shape7.Append(nonVisualShapeProperties7);
            shape7.Append(shapeProperties8);
            shape7.Append(textBody7);

            Picture picture2 = new Picture();

            NonVisualPictureProperties nonVisualPictureProperties2 = new NonVisualPictureProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties11 = new NonVisualDrawingProperties(){ Id = (UInt32Value)17U, Name = "Picture 2", Description = "C:\\Documents and Settings\\Antti Salmi\\Desktop\\hw_ppt_imgs\\figures2.png" };

            NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties2 = new NonVisualPictureDrawingProperties();
            A.PictureLocks pictureLocks2 = new A.PictureLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

            nonVisualPictureDrawingProperties2.Append(pictureLocks2);
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties11 = new ApplicationNonVisualDrawingProperties(){ UserDrawn = true };

            nonVisualPictureProperties2.Append(nonVisualDrawingProperties11);
            nonVisualPictureProperties2.Append(nonVisualPictureDrawingProperties2);
            nonVisualPictureProperties2.Append(applicationNonVisualDrawingProperties11);

            BlipFill blipFill2 = new BlipFill();
            A.Blip blip2 = new A.Blip(){ Embed = "rId3", CompressionState = A.BlipCompressionValues.Print };
            A.SourceRectangle sourceRectangle2 = new A.SourceRectangle();

            A.Stretch stretch2 = new A.Stretch();
            A.FillRectangle fillRectangle2 = new A.FillRectangle();

            stretch2.Append(fillRectangle2);

            blipFill2.Append(blip2);
            blipFill2.Append(sourceRectangle2);
            blipFill2.Append(stretch2);

            ShapeProperties shapeProperties9 = new ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D9 = new A.Transform2D();
            A.Offset offset11 = new A.Offset(){ X = 6140047L, Y = 428604L };
            A.Extents extents11 = new A.Extents(){ Cx = 2475327L, Cy = 785818L };

            transform2D9.Append(offset11);
            transform2D9.Append(extents11);

            A.PresetGeometry presetGeometry9 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList9 = new A.AdjustValueList();

            presetGeometry9.Append(adjustValueList9);
            A.NoFill noFill4 = new A.NoFill();

            shapeProperties9.Append(transform2D9);
            shapeProperties9.Append(presetGeometry9);
            shapeProperties9.Append(noFill4);

            picture2.Append(nonVisualPictureProperties2);
            picture2.Append(blipFill2);
            picture2.Append(shapeProperties9);

            Shape shape8 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties8 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties12 = new NonVisualDrawingProperties(){ Id = (UInt32Value)19U, Name = "Platshållare för text 18" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties8 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks6 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties8.Append(shapeLocks6);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties12 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape6 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)10U, HasCustomPrompt = true };

            applicationNonVisualDrawingProperties12.Append(placeholderShape6);

            nonVisualShapeProperties8.Append(nonVisualDrawingProperties12);
            nonVisualShapeProperties8.Append(nonVisualShapeDrawingProperties8);
            nonVisualShapeProperties8.Append(applicationNonVisualDrawingProperties12);

            ShapeProperties shapeProperties10 = new ShapeProperties();

            A.Transform2D transform2D10 = new A.Transform2D();
            A.Offset offset12 = new A.Offset(){ X = 500063L, Y = 476672L };
            A.Extents extents12 = new A.Extents(){ Cx = 7816850L, Cy = 912813L };

            transform2D10.Append(offset12);
            transform2D10.Append(extents12);

            shapeProperties10.Append(transform2D10);

            TextBody textBody8 = new TextBody();

            A.BodyProperties bodyProperties8 = new A.BodyProperties();
            A.NormalAutoFit normalAutoFit3 = new A.NormalAutoFit();

            bodyProperties8.Append(normalAutoFit3);

            A.ListStyle listStyle8 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties8 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Indent = 0 };
            A.NoBullet noBullet2 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties34 = new A.DefaultRunProperties(){ FontSize = 3000, Bold = true };

            A.SolidFill solidFill41 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex18 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill41.Append(rgbColorModelHex18);
            A.LatinFont latinFont34 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.ComplexScriptFont complexScriptFont34 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            defaultRunProperties34.Append(solidFill41);
            defaultRunProperties34.Append(latinFont34);
            defaultRunProperties34.Append(complexScriptFont34);

            level1ParagraphProperties8.Append(noBullet2);
            level1ParagraphProperties8.Append(defaultRunProperties34);

            listStyle8.Append(level1ParagraphProperties8);

            A.Paragraph paragraph12 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties11 = new A.ParagraphProperties(){ Level = 0 };

            A.Run run8 = new A.Run();
            A.RunProperties runProperties11 = new A.RunProperties(){ Language = "sv-SE", Dirty = false, SmartTagClean = false };
            A.Text text11 = new A.Text();
            text11.Text = "Rubrik";

            run8.Append(runProperties11);
            run8.Append(text11);
            A.EndParagraphRunProperties endParagraphRunProperties6 = new A.EndParagraphRunProperties(){ Language = "sv-SE", Dirty = false };

            paragraph12.Append(paragraphProperties11);
            paragraph12.Append(run8);
            paragraph12.Append(endParagraphRunProperties6);

            textBody8.Append(bodyProperties8);
            textBody8.Append(listStyle8);
            textBody8.Append(paragraph12);

            shape8.Append(nonVisualShapeProperties8);
            shape8.Append(shapeProperties10);
            shape8.Append(textBody8);

            Shape shape9 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties9 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties13 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Platshållare för text 2" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties9 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks7 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties9.Append(shapeLocks7);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties13 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape7 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

            applicationNonVisualDrawingProperties13.Append(placeholderShape7);

            nonVisualShapeProperties9.Append(nonVisualDrawingProperties13);
            nonVisualShapeProperties9.Append(nonVisualShapeDrawingProperties9);
            nonVisualShapeProperties9.Append(applicationNonVisualDrawingProperties13);

            ShapeProperties shapeProperties11 = new ShapeProperties();

            A.Transform2D transform2D11 = new A.Transform2D();
            A.Offset offset13 = new A.Offset(){ X = 501650L, Y = 1557338L };
            A.Extents extents13 = new A.Extents(){ Cx = 7777163L, Cy = 4392612L };

            transform2D11.Append(offset13);
            transform2D11.Append(extents13);

            shapeProperties11.Append(transform2D11);

            TextBody textBody9 = new TextBody();

            A.BodyProperties bodyProperties9 = new A.BodyProperties();
            A.NormalAutoFit normalAutoFit4 = new A.NormalAutoFit();

            bodyProperties9.Append(normalAutoFit4);

            A.ListStyle listStyle9 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties9 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Indent = 0 };
            A.NoBullet noBullet3 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties35 = new A.DefaultRunProperties(){ FontSize = 2200 };

            A.SolidFill solidFill42 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex19 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill42.Append(rgbColorModelHex19);

            defaultRunProperties35.Append(solidFill42);

            level1ParagraphProperties9.Append(noBullet3);
            level1ParagraphProperties9.Append(defaultRunProperties35);

            A.Level2ParagraphProperties level2ParagraphProperties4 = new A.Level2ParagraphProperties();

            A.DefaultRunProperties defaultRunProperties36 = new A.DefaultRunProperties(){ FontSize = 2000 };

            A.SolidFill solidFill43 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex20 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill43.Append(rgbColorModelHex20);

            defaultRunProperties36.Append(solidFill43);

            level2ParagraphProperties4.Append(defaultRunProperties36);

            A.Level3ParagraphProperties level3ParagraphProperties4 = new A.Level3ParagraphProperties();

            A.DefaultRunProperties defaultRunProperties37 = new A.DefaultRunProperties(){ FontSize = 2000 };

            A.SolidFill solidFill44 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex21 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill44.Append(rgbColorModelHex21);

            defaultRunProperties37.Append(solidFill44);

            level3ParagraphProperties4.Append(defaultRunProperties37);

            A.Level4ParagraphProperties level4ParagraphProperties4 = new A.Level4ParagraphProperties();

            A.DefaultRunProperties defaultRunProperties38 = new A.DefaultRunProperties(){ FontSize = 2000 };

            A.SolidFill solidFill45 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex22 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill45.Append(rgbColorModelHex22);

            defaultRunProperties38.Append(solidFill45);

            level4ParagraphProperties4.Append(defaultRunProperties38);

            A.Level5ParagraphProperties level5ParagraphProperties4 = new A.Level5ParagraphProperties();

            A.DefaultRunProperties defaultRunProperties39 = new A.DefaultRunProperties(){ FontSize = 2000 };

            A.SolidFill solidFill46 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex23 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill46.Append(rgbColorModelHex23);

            defaultRunProperties39.Append(solidFill46);

            level5ParagraphProperties4.Append(defaultRunProperties39);

            listStyle9.Append(level1ParagraphProperties9);
            listStyle9.Append(level2ParagraphProperties4);
            listStyle9.Append(level3ParagraphProperties4);
            listStyle9.Append(level4ParagraphProperties4);
            listStyle9.Append(level5ParagraphProperties4);

            A.Paragraph paragraph13 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties12 = new A.ParagraphProperties(){ Level = 0 };

            A.Run run9 = new A.Run();
            A.RunProperties runProperties12 = new A.RunProperties(){ Language = "sv-SE", Dirty = false, SmartTagClean = false };
            A.Text text12 = new A.Text();
            text12.Text = "Klicka här för att ändra format på ";

            run9.Append(runProperties12);
            run9.Append(text12);

            A.Run run10 = new A.Run();
            A.RunProperties runProperties13 = new A.RunProperties(){ Language = "sv-SE", Dirty = false, SpellingError = true, SmartTagClean = false };
            A.Text text13 = new A.Text();
            text13.Text = "bakgrundstexte";

            run10.Append(runProperties13);
            run10.Append(text13);
            A.EndParagraphRunProperties endParagraphRunProperties7 = new A.EndParagraphRunProperties(){ Language = "sv-SE", Dirty = false, SmartTagClean = false };

            paragraph13.Append(paragraphProperties12);
            paragraph13.Append(run9);
            paragraph13.Append(run10);
            paragraph13.Append(endParagraphRunProperties7);

            textBody9.Append(bodyProperties9);
            textBody9.Append(listStyle9);
            textBody9.Append(paragraph13);

            shape9.Append(nonVisualShapeProperties9);
            shape9.Append(shapeProperties11);
            shape9.Append(textBody9);

            shapeTree2.Append(nonVisualGroupShapeProperties2);
            shapeTree2.Append(groupShapeProperties2);
            shapeTree2.Append(picture1);
            shapeTree2.Append(shape6);
            shapeTree2.Append(shape7);
            shapeTree2.Append(picture2);
            shapeTree2.Append(shape8);
            shapeTree2.Append(shape9);

            CommonSlideDataExtensionList commonSlideDataExtensionList2 = new CommonSlideDataExtensionList();

            CommonSlideDataExtension commonSlideDataExtension2 = new CommonSlideDataExtension(){ Uri = "{BB962C8B-B14F-4D97-AF65-F5344CB8AC3E}" };

            P14.CreationId creationId2 = new P14.CreationId(){ Val = (UInt32Value)1446454562U };
            creationId2.AddNamespaceDeclaration("p14", "http://schemas.microsoft.com/office/powerpoint/2010/main");

            commonSlideDataExtension2.Append(creationId2);

            commonSlideDataExtensionList2.Append(commonSlideDataExtension2);

            commonSlideData2.Append(shapeTree2);
            commonSlideData2.Append(commonSlideDataExtensionList2);

            ColorMapOverride colorMapOverride1 = new ColorMapOverride();
            A.MasterColorMapping masterColorMapping1 = new A.MasterColorMapping();

            colorMapOverride1.Append(masterColorMapping1);

            slideLayout1.Append(commonSlideData2);
            slideLayout1.Append(colorMapOverride1);

            slideLayoutPart1.SlideLayout = slideLayout1;
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

        // Generates content of slideLayoutPart2.
        private void GenerateSlideLayoutPart2Content(SlideLayoutPart slideLayoutPart2)
        {
            SlideLayout slideLayout2 = new SlideLayout(){ Preserve = true, UserDrawn = true };
            slideLayout2.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            slideLayout2.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            slideLayout2.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            CommonSlideData commonSlideData3 = new CommonSlideData(){ Name = "Jämförelse" };

            ShapeTree shapeTree3 = new ShapeTree();

            NonVisualGroupShapeProperties nonVisualGroupShapeProperties3 = new NonVisualGroupShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties14 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
            NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties3 = new NonVisualGroupShapeDrawingProperties();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties14 = new ApplicationNonVisualDrawingProperties();

            nonVisualGroupShapeProperties3.Append(nonVisualDrawingProperties14);
            nonVisualGroupShapeProperties3.Append(nonVisualGroupShapeDrawingProperties3);
            nonVisualGroupShapeProperties3.Append(applicationNonVisualDrawingProperties14);

            GroupShapeProperties groupShapeProperties3 = new GroupShapeProperties();

            A.TransformGroup transformGroup3 = new A.TransformGroup();
            A.Offset offset14 = new A.Offset(){ X = 0L, Y = 0L };
            A.Extents extents14 = new A.Extents(){ Cx = 0L, Cy = 0L };
            A.ChildOffset childOffset3 = new A.ChildOffset(){ X = 0L, Y = 0L };
            A.ChildExtents childExtents3 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

            transformGroup3.Append(offset14);
            transformGroup3.Append(extents14);
            transformGroup3.Append(childOffset3);
            transformGroup3.Append(childExtents3);

            groupShapeProperties3.Append(transformGroup3);

            Shape shape10 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties10 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties15 = new NonVisualDrawingProperties(){ Id = (UInt32Value)10U, Name = "Rectangle 11" };
            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties10 = new NonVisualShapeDrawingProperties();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties15 = new ApplicationNonVisualDrawingProperties(){ UserDrawn = true };

            nonVisualShapeProperties10.Append(nonVisualDrawingProperties15);
            nonVisualShapeProperties10.Append(nonVisualShapeDrawingProperties10);
            nonVisualShapeProperties10.Append(applicationNonVisualDrawingProperties15);

            ShapeProperties shapeProperties12 = new ShapeProperties();

            A.Transform2D transform2D12 = new A.Transform2D();
            A.Offset offset15 = new A.Offset(){ X = 142844L, Y = 142852L };
            A.Extents extents15 = new A.Extents(){ Cx = 8858312L, Cy = 6000792L };

            transform2D12.Append(offset15);
            transform2D12.Append(extents15);

            A.PresetGeometry presetGeometry10 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList10 = new A.AdjustValueList();

            presetGeometry10.Append(adjustValueList10);

            A.SolidFill solidFill47 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex24 = new A.RgbColorModelHex(){ Val = "F6F6F6" };

            solidFill47.Append(rgbColorModelHex24);

            A.Outline outline5 = new A.Outline();
            A.NoFill noFill5 = new A.NoFill();

            outline5.Append(noFill5);

            shapeProperties12.Append(transform2D12);
            shapeProperties12.Append(presetGeometry10);
            shapeProperties12.Append(solidFill47);
            shapeProperties12.Append(outline5);

            ShapeStyle shapeStyle2 = new ShapeStyle();

            A.LineReference lineReference2 = new A.LineReference(){ Index = (UInt32Value)2U };

            A.SchemeColor schemeColor53 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };
            A.Shade shade9 = new A.Shade(){ Val = 50000 };

            schemeColor53.Append(shade9);

            lineReference2.Append(schemeColor53);

            A.FillReference fillReference2 = new A.FillReference(){ Index = (UInt32Value)1U };
            A.SchemeColor schemeColor54 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };

            fillReference2.Append(schemeColor54);

            A.EffectReference effectReference2 = new A.EffectReference(){ Index = (UInt32Value)0U };
            A.SchemeColor schemeColor55 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };

            effectReference2.Append(schemeColor55);

            A.FontReference fontReference2 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };
            A.SchemeColor schemeColor56 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            fontReference2.Append(schemeColor56);

            shapeStyle2.Append(lineReference2);
            shapeStyle2.Append(fillReference2);
            shapeStyle2.Append(effectReference2);
            shapeStyle2.Append(fontReference2);

            TextBody textBody10 = new TextBody();
            A.BodyProperties bodyProperties10 = new A.BodyProperties(){ RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };
            A.ListStyle listStyle10 = new A.ListStyle();

            A.Paragraph paragraph14 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties13 = new A.ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Center };
            A.EndParagraphRunProperties endParagraphRunProperties8 = new A.EndParagraphRunProperties(){ Language = "en-GB", Dirty = false };

            paragraph14.Append(paragraphProperties13);
            paragraph14.Append(endParagraphRunProperties8);

            textBody10.Append(bodyProperties10);
            textBody10.Append(listStyle10);
            textBody10.Append(paragraph14);

            shape10.Append(nonVisualShapeProperties10);
            shape10.Append(shapeProperties12);
            shape10.Append(shapeStyle2);
            shape10.Append(textBody10);

            Picture picture3 = new Picture();

            NonVisualPictureProperties nonVisualPictureProperties3 = new NonVisualPictureProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties16 = new NonVisualDrawingProperties(){ Id = (UInt32Value)11U, Name = "Picture 3", Description = "C:\\Documents and Settings\\Antti Salmi\\Desktop\\hw_ppt_imgs\\hw_logo.png" };

            NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties3 = new NonVisualPictureDrawingProperties();
            A.PictureLocks pictureLocks3 = new A.PictureLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

            nonVisualPictureDrawingProperties3.Append(pictureLocks3);
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties16 = new ApplicationNonVisualDrawingProperties(){ UserDrawn = true };

            nonVisualPictureProperties3.Append(nonVisualDrawingProperties16);
            nonVisualPictureProperties3.Append(nonVisualPictureDrawingProperties3);
            nonVisualPictureProperties3.Append(applicationNonVisualDrawingProperties16);

            BlipFill blipFill3 = new BlipFill();
            A.Blip blip3 = new A.Blip(){ Embed = "rId2" };
            A.SourceRectangle sourceRectangle3 = new A.SourceRectangle();

            A.Stretch stretch3 = new A.Stretch();
            A.FillRectangle fillRectangle3 = new A.FillRectangle();

            stretch3.Append(fillRectangle3);

            blipFill3.Append(blip3);
            blipFill3.Append(sourceRectangle3);
            blipFill3.Append(stretch3);

            ShapeProperties shapeProperties13 = new ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D13 = new A.Transform2D();
            A.Offset offset16 = new A.Offset(){ X = 285720L, Y = 6325486L };
            A.Extents extents16 = new A.Extents(){ Cx = 571504L, Cy = 389662L };

            transform2D13.Append(offset16);
            transform2D13.Append(extents16);

            A.PresetGeometry presetGeometry11 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList11 = new A.AdjustValueList();

            presetGeometry11.Append(adjustValueList11);
            A.NoFill noFill6 = new A.NoFill();

            shapeProperties13.Append(transform2D13);
            shapeProperties13.Append(presetGeometry11);
            shapeProperties13.Append(noFill6);

            picture3.Append(nonVisualPictureProperties3);
            picture3.Append(blipFill3);
            picture3.Append(shapeProperties13);

            Shape shape11 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties11 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties17 = new NonVisualDrawingProperties(){ Id = (UInt32Value)12U, Name = "Rectangle 10" };
            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties11 = new NonVisualShapeDrawingProperties();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties17 = new ApplicationNonVisualDrawingProperties(){ UserDrawn = true };

            nonVisualShapeProperties11.Append(nonVisualDrawingProperties17);
            nonVisualShapeProperties11.Append(nonVisualShapeDrawingProperties11);
            nonVisualShapeProperties11.Append(applicationNonVisualDrawingProperties17);

            ShapeProperties shapeProperties14 = new ShapeProperties();

            A.Transform2D transform2D14 = new A.Transform2D();
            A.Offset offset17 = new A.Offset(){ X = 142844L, Y = 6143644L };
            A.Extents extents17 = new A.Extents(){ Cx = 8858312L, Cy = 45719L };

            transform2D14.Append(offset17);
            transform2D14.Append(extents17);

            A.PresetGeometry presetGeometry12 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList12 = new A.AdjustValueList();

            presetGeometry12.Append(adjustValueList12);

            A.SolidFill solidFill48 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex25 = new A.RgbColorModelHex(){ Val = "ECECEC" };

            solidFill48.Append(rgbColorModelHex25);

            A.Outline outline6 = new A.Outline();
            A.NoFill noFill7 = new A.NoFill();

            outline6.Append(noFill7);

            shapeProperties14.Append(transform2D14);
            shapeProperties14.Append(presetGeometry12);
            shapeProperties14.Append(solidFill48);
            shapeProperties14.Append(outline6);

            ShapeStyle shapeStyle3 = new ShapeStyle();

            A.LineReference lineReference3 = new A.LineReference(){ Index = (UInt32Value)2U };

            A.SchemeColor schemeColor57 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };
            A.Shade shade10 = new A.Shade(){ Val = 50000 };

            schemeColor57.Append(shade10);

            lineReference3.Append(schemeColor57);

            A.FillReference fillReference3 = new A.FillReference(){ Index = (UInt32Value)1U };
            A.SchemeColor schemeColor58 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };

            fillReference3.Append(schemeColor58);

            A.EffectReference effectReference3 = new A.EffectReference(){ Index = (UInt32Value)0U };
            A.SchemeColor schemeColor59 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };

            effectReference3.Append(schemeColor59);

            A.FontReference fontReference3 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };
            A.SchemeColor schemeColor60 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            fontReference3.Append(schemeColor60);

            shapeStyle3.Append(lineReference3);
            shapeStyle3.Append(fillReference3);
            shapeStyle3.Append(effectReference3);
            shapeStyle3.Append(fontReference3);

            TextBody textBody11 = new TextBody();
            A.BodyProperties bodyProperties11 = new A.BodyProperties(){ RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };
            A.ListStyle listStyle11 = new A.ListStyle();

            A.Paragraph paragraph15 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties14 = new A.ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Center };
            A.EndParagraphRunProperties endParagraphRunProperties9 = new A.EndParagraphRunProperties(){ Language = "en-GB" };

            paragraph15.Append(paragraphProperties14);
            paragraph15.Append(endParagraphRunProperties9);

            textBody11.Append(bodyProperties11);
            textBody11.Append(listStyle11);
            textBody11.Append(paragraph15);

            shape11.Append(nonVisualShapeProperties11);
            shape11.Append(shapeProperties14);
            shape11.Append(shapeStyle3);
            shape11.Append(textBody11);

            Shape shape12 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties12 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties18 = new NonVisualDrawingProperties(){ Id = (UInt32Value)14U, Name = "TextBox 14" };
            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties12 = new NonVisualShapeDrawingProperties(){ TextBox = true };
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties18 = new ApplicationNonVisualDrawingProperties(){ UserDrawn = true };

            nonVisualShapeProperties12.Append(nonVisualDrawingProperties18);
            nonVisualShapeProperties12.Append(nonVisualShapeDrawingProperties12);
            nonVisualShapeProperties12.Append(applicationNonVisualDrawingProperties18);

            ShapeProperties shapeProperties15 = new ShapeProperties();

            A.Transform2D transform2D15 = new A.Transform2D();
            A.Offset offset18 = new A.Offset(){ X = 6572264L, Y = 6396334L };
            A.Extents extents18 = new A.Extents(){ Cx = 2357454L, Cy = 215444L };

            transform2D15.Append(offset18);
            transform2D15.Append(extents18);

            A.PresetGeometry presetGeometry13 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList13 = new A.AdjustValueList();

            presetGeometry13.Append(adjustValueList13);
            A.NoFill noFill8 = new A.NoFill();

            shapeProperties15.Append(transform2D15);
            shapeProperties15.Append(presetGeometry13);
            shapeProperties15.Append(noFill8);

            TextBody textBody12 = new TextBody();

            A.BodyProperties bodyProperties12 = new A.BodyProperties(){ Wrap = A.TextWrappingValues.Square, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Top };
            A.ShapeAutoFit shapeAutoFit2 = new A.ShapeAutoFit();

            bodyProperties12.Append(shapeAutoFit2);
            A.ListStyle listStyle12 = new A.ListStyle();

            A.Paragraph paragraph16 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties15 = new A.ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Right };

            A.Run run11 = new A.Run();

            A.RunProperties runProperties14 = new A.RunProperties(){ Language = "fi-FI", FontSize = 800, Dirty = false };

            A.SolidFill solidFill49 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex26 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill49.Append(rgbColorModelHex26);
            A.LatinFont latinFont35 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.ComplexScriptFont complexScriptFont35 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            runProperties14.Append(solidFill49);
            runProperties14.Append(latinFont35);
            runProperties14.Append(complexScriptFont35);
            A.Text text14 = new A.Text();
            text14.Text = "Page ";

            run11.Append(runProperties14);
            run11.Append(text14);

            A.Field field4 = new A.Field(){ Id = "{57CB01F7-8DCA-46B5-851A-98264788B0A4}", Type = "slidenum" };

            A.RunProperties runProperties15 = new A.RunProperties(){ Language = "fi-FI", FontSize = 800, SmartTagClean = false };

            A.SolidFill solidFill50 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex27 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill50.Append(rgbColorModelHex27);
            A.LatinFont latinFont36 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.ComplexScriptFont complexScriptFont36 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            runProperties15.Append(solidFill50);
            runProperties15.Append(latinFont36);
            runProperties15.Append(complexScriptFont36);
            A.ParagraphProperties paragraphProperties16 = new A.ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Right };
            A.Text text15 = new A.Text();
            text15.Text = "‹#›";

            field4.Append(runProperties15);
            field4.Append(paragraphProperties16);
            field4.Append(text15);

            A.EndParagraphRunProperties endParagraphRunProperties10 = new A.EndParagraphRunProperties(){ Language = "en-GB", FontSize = 800, Dirty = false };

            A.SolidFill solidFill51 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex28 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill51.Append(rgbColorModelHex28);
            A.LatinFont latinFont37 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.ComplexScriptFont complexScriptFont37 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            endParagraphRunProperties10.Append(solidFill51);
            endParagraphRunProperties10.Append(latinFont37);
            endParagraphRunProperties10.Append(complexScriptFont37);

            paragraph16.Append(paragraphProperties15);
            paragraph16.Append(run11);
            paragraph16.Append(field4);
            paragraph16.Append(endParagraphRunProperties10);

            textBody12.Append(bodyProperties12);
            textBody12.Append(listStyle12);
            textBody12.Append(paragraph16);

            shape12.Append(nonVisualShapeProperties12);
            shape12.Append(shapeProperties15);
            shape12.Append(textBody12);

            Shape shape13 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties13 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties19 = new NonVisualDrawingProperties(){ Id = (UInt32Value)23U, Name = "Platshållare för text 22" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties13 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks8 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties13.Append(shapeLocks8);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties19 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape8 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)10U, HasCustomPrompt = true };

            applicationNonVisualDrawingProperties19.Append(placeholderShape8);

            nonVisualShapeProperties13.Append(nonVisualDrawingProperties19);
            nonVisualShapeProperties13.Append(nonVisualShapeDrawingProperties13);
            nonVisualShapeProperties13.Append(applicationNonVisualDrawingProperties19);

            ShapeProperties shapeProperties16 = new ShapeProperties();

            A.Transform2D transform2D16 = new A.Transform2D();
            A.Offset offset19 = new A.Offset(){ X = 500063L, Y = 475580L };
            A.Extents extents19 = new A.Extents(){ Cx = 7816850L, Cy = 865188L };

            transform2D16.Append(offset19);
            transform2D16.Append(extents19);

            shapeProperties16.Append(transform2D16);

            TextBody textBody13 = new TextBody();

            A.BodyProperties bodyProperties13 = new A.BodyProperties();
            A.NormalAutoFit normalAutoFit5 = new A.NormalAutoFit();

            bodyProperties13.Append(normalAutoFit5);

            A.ListStyle listStyle13 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties10 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Indent = 0 };
            A.NoBullet noBullet4 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties40 = new A.DefaultRunProperties(){ FontSize = 3000, Bold = true };

            A.SolidFill solidFill52 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex29 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill52.Append(rgbColorModelHex29);
            A.LatinFont latinFont38 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.ComplexScriptFont complexScriptFont38 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            defaultRunProperties40.Append(solidFill52);
            defaultRunProperties40.Append(latinFont38);
            defaultRunProperties40.Append(complexScriptFont38);

            level1ParagraphProperties10.Append(noBullet4);
            level1ParagraphProperties10.Append(defaultRunProperties40);

            listStyle13.Append(level1ParagraphProperties10);

            A.Paragraph paragraph17 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties17 = new A.ParagraphProperties(){ Level = 0 };

            A.Run run12 = new A.Run();
            A.RunProperties runProperties16 = new A.RunProperties(){ Language = "sv-SE", Dirty = false };
            A.Text text16 = new A.Text();
            text16.Text = "Rubrik";

            run12.Append(runProperties16);
            run12.Append(text16);

            paragraph17.Append(paragraphProperties17);
            paragraph17.Append(run12);

            textBody13.Append(bodyProperties13);
            textBody13.Append(listStyle13);
            textBody13.Append(paragraph17);

            shape13.Append(nonVisualShapeProperties13);
            shape13.Append(shapeProperties16);
            shape13.Append(textBody13);

            Shape shape14 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties14 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties20 = new NonVisualDrawingProperties(){ Id = (UInt32Value)25U, Name = "Platshållare för text 24" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties14 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks9 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties14.Append(shapeLocks9);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties20 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape9 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U, HasCustomPrompt = true };

            applicationNonVisualDrawingProperties20.Append(placeholderShape9);

            nonVisualShapeProperties14.Append(nonVisualDrawingProperties20);
            nonVisualShapeProperties14.Append(nonVisualShapeDrawingProperties14);
            nonVisualShapeProperties14.Append(applicationNonVisualDrawingProperties20);

            ShapeProperties shapeProperties17 = new ShapeProperties();

            A.Transform2D transform2D17 = new A.Transform2D();
            A.Offset offset20 = new A.Offset(){ X = 500063L, Y = 4470946L };
            A.Extents extents20 = new A.Extents(){ Cx = 8001027L, Cy = 830262L };

            transform2D17.Append(offset20);
            transform2D17.Append(extents20);

            shapeProperties17.Append(transform2D17);

            TextBody textBody14 = new TextBody();

            A.BodyProperties bodyProperties14 = new A.BodyProperties();
            A.NormalAutoFit normalAutoFit6 = new A.NormalAutoFit();

            bodyProperties14.Append(normalAutoFit6);

            A.ListStyle listStyle14 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties11 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Indent = 0 };
            A.NoBullet noBullet5 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties41 = new A.DefaultRunProperties(){ FontSize = 1200 };

            A.SolidFill solidFill53 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex30 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill53.Append(rgbColorModelHex30);
            A.LatinFont latinFont39 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.ComplexScriptFont complexScriptFont39 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            defaultRunProperties41.Append(solidFill53);
            defaultRunProperties41.Append(latinFont39);
            defaultRunProperties41.Append(complexScriptFont39);

            level1ParagraphProperties11.Append(noBullet5);
            level1ParagraphProperties11.Append(defaultRunProperties41);

            listStyle14.Append(level1ParagraphProperties11);

            A.Paragraph paragraph18 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties18 = new A.ParagraphProperties(){ Level = 0 };

            A.Run run13 = new A.Run();
            A.RunProperties runProperties17 = new A.RunProperties(){ Language = "sv-SE", Dirty = false };
            A.Text text17 = new A.Text();
            text17.Text = "Text";

            run13.Append(runProperties17);
            run13.Append(text17);

            paragraph18.Append(paragraphProperties18);
            paragraph18.Append(run13);

            textBody14.Append(bodyProperties14);
            textBody14.Append(listStyle14);
            textBody14.Append(paragraph18);

            shape14.Append(nonVisualShapeProperties14);
            shape14.Append(shapeProperties17);
            shape14.Append(textBody14);

            Shape shape15 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties15 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties21 = new NonVisualDrawingProperties(){ Id = (UInt32Value)22U, Name = "Platshållare för bild 21" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties15 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks10 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties15.Append(shapeLocks10);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties21 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape10 = new PlaceholderShape(){ Type = PlaceholderValues.Picture, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

            applicationNonVisualDrawingProperties21.Append(placeholderShape10);

            nonVisualShapeProperties15.Append(nonVisualDrawingProperties21);
            nonVisualShapeProperties15.Append(nonVisualShapeDrawingProperties15);
            nonVisualShapeProperties15.Append(applicationNonVisualDrawingProperties21);

            ShapeProperties shapeProperties18 = new ShapeProperties();

            A.Transform2D transform2D18 = new A.Transform2D();
            A.Offset offset21 = new A.Offset(){ X = 513264L, Y = 1484784L };
            A.Extents extents21 = new A.Extents(){ Cx = 7992888L, Cy = 2850654L };

            transform2D18.Append(offset21);
            transform2D18.Append(extents21);

            shapeProperties18.Append(transform2D18);

            TextBody textBody15 = new TextBody();
            A.BodyProperties bodyProperties15 = new A.BodyProperties();
            A.ListStyle listStyle15 = new A.ListStyle();

            A.Paragraph paragraph19 = new A.Paragraph();
            A.EndParagraphRunProperties endParagraphRunProperties11 = new A.EndParagraphRunProperties(){ Language = "sv-SE" };

            paragraph19.Append(endParagraphRunProperties11);

            textBody15.Append(bodyProperties15);
            textBody15.Append(listStyle15);
            textBody15.Append(paragraph19);

            shape15.Append(nonVisualShapeProperties15);
            shape15.Append(shapeProperties18);
            shape15.Append(textBody15);

            shapeTree3.Append(nonVisualGroupShapeProperties3);
            shapeTree3.Append(groupShapeProperties3);
            shapeTree3.Append(shape10);
            shapeTree3.Append(picture3);
            shapeTree3.Append(shape11);
            shapeTree3.Append(shape12);
            shapeTree3.Append(shape13);
            shapeTree3.Append(shape14);
            shapeTree3.Append(shape15);

            CommonSlideDataExtensionList commonSlideDataExtensionList3 = new CommonSlideDataExtensionList();

            CommonSlideDataExtension commonSlideDataExtension3 = new CommonSlideDataExtension(){ Uri = "{BB962C8B-B14F-4D97-AF65-F5344CB8AC3E}" };

            P14.CreationId creationId3 = new P14.CreationId(){ Val = (UInt32Value)3166449003U };
            creationId3.AddNamespaceDeclaration("p14", "http://schemas.microsoft.com/office/powerpoint/2010/main");

            commonSlideDataExtension3.Append(creationId3);

            commonSlideDataExtensionList3.Append(commonSlideDataExtension3);

            commonSlideData3.Append(shapeTree3);
            commonSlideData3.Append(commonSlideDataExtensionList3);

            ColorMapOverride colorMapOverride2 = new ColorMapOverride();
            A.MasterColorMapping masterColorMapping2 = new A.MasterColorMapping();

            colorMapOverride2.Append(masterColorMapping2);

            slideLayout2.Append(commonSlideData3);
            slideLayout2.Append(colorMapOverride2);

            slideLayoutPart2.SlideLayout = slideLayout2;
        }

        // Generates content of presentationPropertiesPart1.
        private void GeneratePresentationPropertiesPart1Content(PresentationPropertiesPart presentationPropertiesPart1)
        {
            PresentationProperties presentationProperties1 = new PresentationProperties();
            presentationProperties1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            presentationProperties1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            presentationProperties1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            ColorMostRecentlyUsed colorMostRecentlyUsed1 = new ColorMostRecentlyUsed();
            A.RgbColorModelHex rgbColorModelHex31 = new A.RgbColorModelHex(){ Val = "0084B5" };
            A.RgbColorModelHex rgbColorModelHex32 = new A.RgbColorModelHex(){ Val = "F6F6F6" };
            A.RgbColorModelHex rgbColorModelHex33 = new A.RgbColorModelHex(){ Val = "ECECEC" };
            A.RgbColorModelHex rgbColorModelHex34 = new A.RgbColorModelHex(){ Val = "BDD143" };
            A.RgbColorModelHex rgbColorModelHex35 = new A.RgbColorModelHex(){ Val = "F18805" };
            A.RgbColorModelHex rgbColorModelHex36 = new A.RgbColorModelHex(){ Val = "DD0068" };

            colorMostRecentlyUsed1.Append(rgbColorModelHex31);
            colorMostRecentlyUsed1.Append(rgbColorModelHex32);
            colorMostRecentlyUsed1.Append(rgbColorModelHex33);
            colorMostRecentlyUsed1.Append(rgbColorModelHex34);
            colorMostRecentlyUsed1.Append(rgbColorModelHex35);
            colorMostRecentlyUsed1.Append(rgbColorModelHex36);

            PresentationPropertiesExtensionList presentationPropertiesExtensionList1 = new PresentationPropertiesExtensionList();

            PresentationPropertiesExtension presentationPropertiesExtension1 = new PresentationPropertiesExtension(){ Uri = "{E76CE94A-603C-4142-B9EB-6D1370010A27}" };

            P14.DiscardImageEditData discardImageEditData1 = new P14.DiscardImageEditData(){ Val = false };
            discardImageEditData1.AddNamespaceDeclaration("p14", "http://schemas.microsoft.com/office/powerpoint/2010/main");

            presentationPropertiesExtension1.Append(discardImageEditData1);

            PresentationPropertiesExtension presentationPropertiesExtension2 = new PresentationPropertiesExtension(){ Uri = "{D31A062A-798A-4329-ABDD-BBA856620510}" };

            P14.DefaultImageDpi defaultImageDpi1 = new P14.DefaultImageDpi(){ Val = (UInt32Value)220U };
            defaultImageDpi1.AddNamespaceDeclaration("p14", "http://schemas.microsoft.com/office/powerpoint/2010/main");

            presentationPropertiesExtension2.Append(defaultImageDpi1);

            PresentationPropertiesExtension presentationPropertiesExtension3 = new PresentationPropertiesExtension(){ Uri = "{FD5EFAAD-0ECE-453E-9831-46B23BE46B34}" };

            OpenXmlUnknownElement openXmlUnknownElement2 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<p15:chartTrackingRefBased xmlns:p15=\"http://schemas.microsoft.com/office/powerpoint/2012/main\" val=\"0\" />");

            presentationPropertiesExtension3.Append(openXmlUnknownElement2);

            presentationPropertiesExtensionList1.Append(presentationPropertiesExtension1);
            presentationPropertiesExtensionList1.Append(presentationPropertiesExtension2);
            presentationPropertiesExtensionList1.Append(presentationPropertiesExtension3);

            presentationProperties1.Append(colorMostRecentlyUsed1);
            presentationProperties1.Append(presentationPropertiesExtensionList1);

            presentationPropertiesPart1.PresentationProperties = presentationProperties1;
        }

        // Generates content of slideMasterPart2.
        private void GenerateSlideMasterPart2Content(SlideMasterPart slideMasterPart2)
        {
            SlideMaster slideMaster2 = new SlideMaster();
            slideMaster2.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            slideMaster2.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            slideMaster2.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            CommonSlideData commonSlideData4 = new CommonSlideData();

            Background background2 = new Background();

            BackgroundStyleReference backgroundStyleReference2 = new BackgroundStyleReference(){ Index = (UInt32Value)1001U };
            A.SchemeColor schemeColor61 = new A.SchemeColor(){ Val = A.SchemeColorValues.Background1 };

            backgroundStyleReference2.Append(schemeColor61);

            background2.Append(backgroundStyleReference2);

            ShapeTree shapeTree4 = new ShapeTree();

            NonVisualGroupShapeProperties nonVisualGroupShapeProperties4 = new NonVisualGroupShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties22 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
            NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties4 = new NonVisualGroupShapeDrawingProperties();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties22 = new ApplicationNonVisualDrawingProperties();

            nonVisualGroupShapeProperties4.Append(nonVisualDrawingProperties22);
            nonVisualGroupShapeProperties4.Append(nonVisualGroupShapeDrawingProperties4);
            nonVisualGroupShapeProperties4.Append(applicationNonVisualDrawingProperties22);

            GroupShapeProperties groupShapeProperties4 = new GroupShapeProperties();

            A.TransformGroup transformGroup4 = new A.TransformGroup();
            A.Offset offset22 = new A.Offset(){ X = 0L, Y = 0L };
            A.Extents extents22 = new A.Extents(){ Cx = 0L, Cy = 0L };
            A.ChildOffset childOffset4 = new A.ChildOffset(){ X = 0L, Y = 0L };
            A.ChildExtents childExtents4 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

            transformGroup4.Append(offset22);
            transformGroup4.Append(extents22);
            transformGroup4.Append(childOffset4);
            transformGroup4.Append(childExtents4);

            groupShapeProperties4.Append(transformGroup4);

            Shape shape16 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties16 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties23 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title Placeholder 1" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties16 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks11 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties16.Append(shapeLocks11);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties23 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape11 = new PlaceholderShape(){ Type = PlaceholderValues.Title };

            applicationNonVisualDrawingProperties23.Append(placeholderShape11);

            nonVisualShapeProperties16.Append(nonVisualDrawingProperties23);
            nonVisualShapeProperties16.Append(nonVisualShapeDrawingProperties16);
            nonVisualShapeProperties16.Append(applicationNonVisualDrawingProperties23);

            ShapeProperties shapeProperties19 = new ShapeProperties();

            A.Transform2D transform2D19 = new A.Transform2D();
            A.Offset offset23 = new A.Offset(){ X = 457200L, Y = 274638L };
            A.Extents extents23 = new A.Extents(){ Cx = 8229600L, Cy = 1143000L };

            transform2D19.Append(offset23);
            transform2D19.Append(extents23);

            A.PresetGeometry presetGeometry14 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList14 = new A.AdjustValueList();

            presetGeometry14.Append(adjustValueList14);

            shapeProperties19.Append(transform2D19);
            shapeProperties19.Append(presetGeometry14);

            TextBody textBody16 = new TextBody();

            A.BodyProperties bodyProperties16 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };
            A.NormalAutoFit normalAutoFit7 = new A.NormalAutoFit();

            bodyProperties16.Append(normalAutoFit7);
            A.ListStyle listStyle16 = new A.ListStyle();

            A.Paragraph paragraph20 = new A.Paragraph();

            A.Run run14 = new A.Run();
            A.RunProperties runProperties18 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text18 = new A.Text();
            text18.Text = "Klicka här för att ändra format";

            run14.Append(runProperties18);
            run14.Append(text18);
            A.EndParagraphRunProperties endParagraphRunProperties12 = new A.EndParagraphRunProperties(){ Language = "en-GB" };

            paragraph20.Append(run14);
            paragraph20.Append(endParagraphRunProperties12);

            textBody16.Append(bodyProperties16);
            textBody16.Append(listStyle16);
            textBody16.Append(paragraph20);

            shape16.Append(nonVisualShapeProperties16);
            shape16.Append(shapeProperties19);
            shape16.Append(textBody16);

            Shape shape17 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties17 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties24 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Text Placeholder 2" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties17 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks12 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties17.Append(shapeLocks12);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties24 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape12 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Index = (UInt32Value)1U };

            applicationNonVisualDrawingProperties24.Append(placeholderShape12);

            nonVisualShapeProperties17.Append(nonVisualDrawingProperties24);
            nonVisualShapeProperties17.Append(nonVisualShapeDrawingProperties17);
            nonVisualShapeProperties17.Append(applicationNonVisualDrawingProperties24);

            ShapeProperties shapeProperties20 = new ShapeProperties();

            A.Transform2D transform2D20 = new A.Transform2D();
            A.Offset offset24 = new A.Offset(){ X = 457200L, Y = 1600200L };
            A.Extents extents24 = new A.Extents(){ Cx = 8229600L, Cy = 4525963L };

            transform2D20.Append(offset24);
            transform2D20.Append(extents24);

            A.PresetGeometry presetGeometry15 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList15 = new A.AdjustValueList();

            presetGeometry15.Append(adjustValueList15);

            shapeProperties20.Append(transform2D20);
            shapeProperties20.Append(presetGeometry15);

            TextBody textBody17 = new TextBody();

            A.BodyProperties bodyProperties17 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false };
            A.NormalAutoFit normalAutoFit8 = new A.NormalAutoFit();

            bodyProperties17.Append(normalAutoFit8);
            A.ListStyle listStyle17 = new A.ListStyle();

            A.Paragraph paragraph21 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties19 = new A.ParagraphProperties(){ Level = 0 };

            A.Run run15 = new A.Run();
            A.RunProperties runProperties19 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text19 = new A.Text();
            text19.Text = "Klicka här för att ändra format på bakgrundstexten";

            run15.Append(runProperties19);
            run15.Append(text19);

            paragraph21.Append(paragraphProperties19);
            paragraph21.Append(run15);

            A.Paragraph paragraph22 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties20 = new A.ParagraphProperties(){ Level = 1 };

            A.Run run16 = new A.Run();
            A.RunProperties runProperties20 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text20 = new A.Text();
            text20.Text = "Nivå två";

            run16.Append(runProperties20);
            run16.Append(text20);

            paragraph22.Append(paragraphProperties20);
            paragraph22.Append(run16);

            A.Paragraph paragraph23 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties21 = new A.ParagraphProperties(){ Level = 2 };

            A.Run run17 = new A.Run();
            A.RunProperties runProperties21 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text21 = new A.Text();
            text21.Text = "Nivå tre";

            run17.Append(runProperties21);
            run17.Append(text21);

            paragraph23.Append(paragraphProperties21);
            paragraph23.Append(run17);

            A.Paragraph paragraph24 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties22 = new A.ParagraphProperties(){ Level = 3 };

            A.Run run18 = new A.Run();
            A.RunProperties runProperties22 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text22 = new A.Text();
            text22.Text = "Nivå fyra";

            run18.Append(runProperties22);
            run18.Append(text22);

            paragraph24.Append(paragraphProperties22);
            paragraph24.Append(run18);

            A.Paragraph paragraph25 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties23 = new A.ParagraphProperties(){ Level = 4 };

            A.Run run19 = new A.Run();
            A.RunProperties runProperties23 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text23 = new A.Text();
            text23.Text = "Nivå fem";

            run19.Append(runProperties23);
            run19.Append(text23);
            A.EndParagraphRunProperties endParagraphRunProperties13 = new A.EndParagraphRunProperties(){ Language = "en-GB" };

            paragraph25.Append(paragraphProperties23);
            paragraph25.Append(run19);
            paragraph25.Append(endParagraphRunProperties13);

            textBody17.Append(bodyProperties17);
            textBody17.Append(listStyle17);
            textBody17.Append(paragraph21);
            textBody17.Append(paragraph22);
            textBody17.Append(paragraph23);
            textBody17.Append(paragraph24);
            textBody17.Append(paragraph25);

            shape17.Append(nonVisualShapeProperties17);
            shape17.Append(shapeProperties20);
            shape17.Append(textBody17);

            Shape shape18 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties18 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties25 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Date Placeholder 3" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties18 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks13 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties18.Append(shapeLocks13);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties25 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape13 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)2U };

            applicationNonVisualDrawingProperties25.Append(placeholderShape13);

            nonVisualShapeProperties18.Append(nonVisualDrawingProperties25);
            nonVisualShapeProperties18.Append(nonVisualShapeDrawingProperties18);
            nonVisualShapeProperties18.Append(applicationNonVisualDrawingProperties25);

            ShapeProperties shapeProperties21 = new ShapeProperties();

            A.Transform2D transform2D21 = new A.Transform2D();
            A.Offset offset25 = new A.Offset(){ X = 457200L, Y = 6356350L };
            A.Extents extents25 = new A.Extents(){ Cx = 2133600L, Cy = 365125L };

            transform2D21.Append(offset25);
            transform2D21.Append(extents25);

            A.PresetGeometry presetGeometry16 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList16 = new A.AdjustValueList();

            presetGeometry16.Append(adjustValueList16);

            shapeProperties21.Append(transform2D21);
            shapeProperties21.Append(presetGeometry16);

            TextBody textBody18 = new TextBody();
            A.BodyProperties bodyProperties18 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };

            A.ListStyle listStyle18 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties12 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Left };

            A.DefaultRunProperties defaultRunProperties42 = new A.DefaultRunProperties(){ FontSize = 1200 };

            A.SolidFill solidFill54 = new A.SolidFill();

            A.SchemeColor schemeColor62 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint10 = new A.Tint(){ Val = 75000 };

            schemeColor62.Append(tint10);

            solidFill54.Append(schemeColor62);

            defaultRunProperties42.Append(solidFill54);

            level1ParagraphProperties12.Append(defaultRunProperties42);

            listStyle18.Append(level1ParagraphProperties12);

            A.Paragraph paragraph26 = new A.Paragraph();

            A.Field field5 = new A.Field(){ Id = "{9770A701-F095-403F-9DCF-B1CCDA9CEBEC}", Type = "datetimeFigureOut" };
            A.RunProperties runProperties24 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
            A.ParagraphProperties paragraphProperties24 = new A.ParagraphProperties();
            A.Text text24 = new A.Text();
            text24.Text = "10/21/2016";

            field5.Append(runProperties24);
            field5.Append(paragraphProperties24);
            field5.Append(text24);
            A.EndParagraphRunProperties endParagraphRunProperties14 = new A.EndParagraphRunProperties(){ Language = "en-GB" };

            paragraph26.Append(field5);
            paragraph26.Append(endParagraphRunProperties14);

            textBody18.Append(bodyProperties18);
            textBody18.Append(listStyle18);
            textBody18.Append(paragraph26);

            shape18.Append(nonVisualShapeProperties18);
            shape18.Append(shapeProperties21);
            shape18.Append(textBody18);

            Shape shape19 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties19 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties26 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Footer Placeholder 4" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties19 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks14 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties19.Append(shapeLocks14);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties26 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape14 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)3U };

            applicationNonVisualDrawingProperties26.Append(placeholderShape14);

            nonVisualShapeProperties19.Append(nonVisualDrawingProperties26);
            nonVisualShapeProperties19.Append(nonVisualShapeDrawingProperties19);
            nonVisualShapeProperties19.Append(applicationNonVisualDrawingProperties26);

            ShapeProperties shapeProperties22 = new ShapeProperties();

            A.Transform2D transform2D22 = new A.Transform2D();
            A.Offset offset26 = new A.Offset(){ X = 3124200L, Y = 6356350L };
            A.Extents extents26 = new A.Extents(){ Cx = 2895600L, Cy = 365125L };

            transform2D22.Append(offset26);
            transform2D22.Append(extents26);

            A.PresetGeometry presetGeometry17 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList17 = new A.AdjustValueList();

            presetGeometry17.Append(adjustValueList17);

            shapeProperties22.Append(transform2D22);
            shapeProperties22.Append(presetGeometry17);

            TextBody textBody19 = new TextBody();
            A.BodyProperties bodyProperties19 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };

            A.ListStyle listStyle19 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties13 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Center };

            A.DefaultRunProperties defaultRunProperties43 = new A.DefaultRunProperties(){ FontSize = 1200 };

            A.SolidFill solidFill55 = new A.SolidFill();

            A.SchemeColor schemeColor63 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint11 = new A.Tint(){ Val = 75000 };

            schemeColor63.Append(tint11);

            solidFill55.Append(schemeColor63);

            defaultRunProperties43.Append(solidFill55);

            level1ParagraphProperties13.Append(defaultRunProperties43);

            listStyle19.Append(level1ParagraphProperties13);

            A.Paragraph paragraph27 = new A.Paragraph();
            A.EndParagraphRunProperties endParagraphRunProperties15 = new A.EndParagraphRunProperties(){ Language = "en-GB" };

            paragraph27.Append(endParagraphRunProperties15);

            textBody19.Append(bodyProperties19);
            textBody19.Append(listStyle19);
            textBody19.Append(paragraph27);

            shape19.Append(nonVisualShapeProperties19);
            shape19.Append(shapeProperties22);
            shape19.Append(textBody19);

            Shape shape20 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties20 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties27 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Slide Number Placeholder 5" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties20 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks15 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties20.Append(shapeLocks15);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties27 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape15 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)4U };

            applicationNonVisualDrawingProperties27.Append(placeholderShape15);

            nonVisualShapeProperties20.Append(nonVisualDrawingProperties27);
            nonVisualShapeProperties20.Append(nonVisualShapeDrawingProperties20);
            nonVisualShapeProperties20.Append(applicationNonVisualDrawingProperties27);

            ShapeProperties shapeProperties23 = new ShapeProperties();

            A.Transform2D transform2D23 = new A.Transform2D();
            A.Offset offset27 = new A.Offset(){ X = 6553200L, Y = 6356350L };
            A.Extents extents27 = new A.Extents(){ Cx = 2133600L, Cy = 365125L };

            transform2D23.Append(offset27);
            transform2D23.Append(extents27);

            A.PresetGeometry presetGeometry18 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList18 = new A.AdjustValueList();

            presetGeometry18.Append(adjustValueList18);

            shapeProperties23.Append(transform2D23);
            shapeProperties23.Append(presetGeometry18);

            TextBody textBody20 = new TextBody();
            A.BodyProperties bodyProperties20 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };

            A.ListStyle listStyle20 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties14 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Right };

            A.DefaultRunProperties defaultRunProperties44 = new A.DefaultRunProperties(){ FontSize = 1200 };

            A.SolidFill solidFill56 = new A.SolidFill();

            A.SchemeColor schemeColor64 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint12 = new A.Tint(){ Val = 75000 };

            schemeColor64.Append(tint12);

            solidFill56.Append(schemeColor64);

            defaultRunProperties44.Append(solidFill56);

            level1ParagraphProperties14.Append(defaultRunProperties44);

            listStyle20.Append(level1ParagraphProperties14);

            A.Paragraph paragraph28 = new A.Paragraph();

            A.Field field6 = new A.Field(){ Id = "{335A3C46-0F61-4459-889B-E48E8905434F}", Type = "slidenum" };
            A.RunProperties runProperties25 = new A.RunProperties(){ Language = "en-GB", SmartTagClean = false };
            A.ParagraphProperties paragraphProperties25 = new A.ParagraphProperties();
            A.Text text25 = new A.Text();
            text25.Text = "‹#›";

            field6.Append(runProperties25);
            field6.Append(paragraphProperties25);
            field6.Append(text25);
            A.EndParagraphRunProperties endParagraphRunProperties16 = new A.EndParagraphRunProperties(){ Language = "en-GB" };

            paragraph28.Append(field6);
            paragraph28.Append(endParagraphRunProperties16);

            textBody20.Append(bodyProperties20);
            textBody20.Append(listStyle20);
            textBody20.Append(paragraph28);

            shape20.Append(nonVisualShapeProperties20);
            shape20.Append(shapeProperties23);
            shape20.Append(textBody20);

            shapeTree4.Append(nonVisualGroupShapeProperties4);
            shapeTree4.Append(groupShapeProperties4);
            shapeTree4.Append(shape16);
            shapeTree4.Append(shape17);
            shapeTree4.Append(shape18);
            shapeTree4.Append(shape19);
            shapeTree4.Append(shape20);

            commonSlideData4.Append(background2);
            commonSlideData4.Append(shapeTree4);
            ColorMap colorMap2 = new ColorMap(){ Background1 = A.ColorSchemeIndexValues.Light1, Text1 = A.ColorSchemeIndexValues.Dark1, Background2 = A.ColorSchemeIndexValues.Light2, Text2 = A.ColorSchemeIndexValues.Dark2, Accent1 = A.ColorSchemeIndexValues.Accent1, Accent2 = A.ColorSchemeIndexValues.Accent2, Accent3 = A.ColorSchemeIndexValues.Accent3, Accent4 = A.ColorSchemeIndexValues.Accent4, Accent5 = A.ColorSchemeIndexValues.Accent5, Accent6 = A.ColorSchemeIndexValues.Accent6, Hyperlink = A.ColorSchemeIndexValues.Hyperlink, FollowedHyperlink = A.ColorSchemeIndexValues.FollowedHyperlink };

            SlideLayoutIdList slideLayoutIdList2 = new SlideLayoutIdList();
            SlideLayoutId slideLayoutId3 = new SlideLayoutId(){ Id = (UInt32Value)2147483649U, RelationshipId = "rId1" };

            slideLayoutIdList2.Append(slideLayoutId3);

            TextStyles textStyles2 = new TextStyles();

            TitleStyle titleStyle2 = new TitleStyle();

            A.Level1ParagraphProperties level1ParagraphProperties15 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Center, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.SpaceBefore spaceBefore11 = new A.SpaceBefore();
            A.SpacingPercent spacingPercent11 = new A.SpacingPercent(){ Val = 0 };

            spaceBefore11.Append(spacingPercent11);
            A.NoBullet noBullet6 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties45 = new A.DefaultRunProperties(){ FontSize = 4400, Kerning = 1200 };

            A.SolidFill solidFill57 = new A.SolidFill();
            A.SchemeColor schemeColor65 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill57.Append(schemeColor65);
            A.LatinFont latinFont40 = new A.LatinFont(){ Typeface = "+mj-lt" };
            A.EastAsianFont eastAsianFont31 = new A.EastAsianFont(){ Typeface = "+mj-ea" };
            A.ComplexScriptFont complexScriptFont40 = new A.ComplexScriptFont(){ Typeface = "+mj-cs" };

            defaultRunProperties45.Append(solidFill57);
            defaultRunProperties45.Append(latinFont40);
            defaultRunProperties45.Append(eastAsianFont31);
            defaultRunProperties45.Append(complexScriptFont40);

            level1ParagraphProperties15.Append(spaceBefore11);
            level1ParagraphProperties15.Append(noBullet6);
            level1ParagraphProperties15.Append(defaultRunProperties45);

            titleStyle2.Append(level1ParagraphProperties15);

            BodyStyle bodyStyle2 = new BodyStyle();

            A.Level1ParagraphProperties level1ParagraphProperties16 = new A.Level1ParagraphProperties(){ LeftMargin = 342900, Indent = -342900, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.SpaceBefore spaceBefore12 = new A.SpaceBefore();
            A.SpacingPercent spacingPercent12 = new A.SpacingPercent(){ Val = 20000 };

            spaceBefore12.Append(spacingPercent12);
            A.BulletFont bulletFont10 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.CharacterBullet characterBullet10 = new A.CharacterBullet(){ Char = "•" };

            A.DefaultRunProperties defaultRunProperties46 = new A.DefaultRunProperties(){ FontSize = 3200, Kerning = 1200 };

            A.SolidFill solidFill58 = new A.SolidFill();
            A.SchemeColor schemeColor66 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill58.Append(schemeColor66);
            A.LatinFont latinFont41 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont32 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont41 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties46.Append(solidFill58);
            defaultRunProperties46.Append(latinFont41);
            defaultRunProperties46.Append(eastAsianFont32);
            defaultRunProperties46.Append(complexScriptFont41);

            level1ParagraphProperties16.Append(spaceBefore12);
            level1ParagraphProperties16.Append(bulletFont10);
            level1ParagraphProperties16.Append(characterBullet10);
            level1ParagraphProperties16.Append(defaultRunProperties46);

            A.Level2ParagraphProperties level2ParagraphProperties5 = new A.Level2ParagraphProperties(){ LeftMargin = 742950, Indent = -285750, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.SpaceBefore spaceBefore13 = new A.SpaceBefore();
            A.SpacingPercent spacingPercent13 = new A.SpacingPercent(){ Val = 20000 };

            spaceBefore13.Append(spacingPercent13);
            A.BulletFont bulletFont11 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.CharacterBullet characterBullet11 = new A.CharacterBullet(){ Char = "–" };

            A.DefaultRunProperties defaultRunProperties47 = new A.DefaultRunProperties(){ FontSize = 2800, Kerning = 1200 };

            A.SolidFill solidFill59 = new A.SolidFill();
            A.SchemeColor schemeColor67 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill59.Append(schemeColor67);
            A.LatinFont latinFont42 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont33 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont42 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties47.Append(solidFill59);
            defaultRunProperties47.Append(latinFont42);
            defaultRunProperties47.Append(eastAsianFont33);
            defaultRunProperties47.Append(complexScriptFont42);

            level2ParagraphProperties5.Append(spaceBefore13);
            level2ParagraphProperties5.Append(bulletFont11);
            level2ParagraphProperties5.Append(characterBullet11);
            level2ParagraphProperties5.Append(defaultRunProperties47);

            A.Level3ParagraphProperties level3ParagraphProperties5 = new A.Level3ParagraphProperties(){ LeftMargin = 1143000, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.SpaceBefore spaceBefore14 = new A.SpaceBefore();
            A.SpacingPercent spacingPercent14 = new A.SpacingPercent(){ Val = 20000 };

            spaceBefore14.Append(spacingPercent14);
            A.BulletFont bulletFont12 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.CharacterBullet characterBullet12 = new A.CharacterBullet(){ Char = "•" };

            A.DefaultRunProperties defaultRunProperties48 = new A.DefaultRunProperties(){ FontSize = 2400, Kerning = 1200 };

            A.SolidFill solidFill60 = new A.SolidFill();
            A.SchemeColor schemeColor68 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill60.Append(schemeColor68);
            A.LatinFont latinFont43 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont34 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont43 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties48.Append(solidFill60);
            defaultRunProperties48.Append(latinFont43);
            defaultRunProperties48.Append(eastAsianFont34);
            defaultRunProperties48.Append(complexScriptFont43);

            level3ParagraphProperties5.Append(spaceBefore14);
            level3ParagraphProperties5.Append(bulletFont12);
            level3ParagraphProperties5.Append(characterBullet12);
            level3ParagraphProperties5.Append(defaultRunProperties48);

            A.Level4ParagraphProperties level4ParagraphProperties5 = new A.Level4ParagraphProperties(){ LeftMargin = 1600200, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.SpaceBefore spaceBefore15 = new A.SpaceBefore();
            A.SpacingPercent spacingPercent15 = new A.SpacingPercent(){ Val = 20000 };

            spaceBefore15.Append(spacingPercent15);
            A.BulletFont bulletFont13 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.CharacterBullet characterBullet13 = new A.CharacterBullet(){ Char = "–" };

            A.DefaultRunProperties defaultRunProperties49 = new A.DefaultRunProperties(){ FontSize = 2000, Kerning = 1200 };

            A.SolidFill solidFill61 = new A.SolidFill();
            A.SchemeColor schemeColor69 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill61.Append(schemeColor69);
            A.LatinFont latinFont44 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont35 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont44 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties49.Append(solidFill61);
            defaultRunProperties49.Append(latinFont44);
            defaultRunProperties49.Append(eastAsianFont35);
            defaultRunProperties49.Append(complexScriptFont44);

            level4ParagraphProperties5.Append(spaceBefore15);
            level4ParagraphProperties5.Append(bulletFont13);
            level4ParagraphProperties5.Append(characterBullet13);
            level4ParagraphProperties5.Append(defaultRunProperties49);

            A.Level5ParagraphProperties level5ParagraphProperties5 = new A.Level5ParagraphProperties(){ LeftMargin = 2057400, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.SpaceBefore spaceBefore16 = new A.SpaceBefore();
            A.SpacingPercent spacingPercent16 = new A.SpacingPercent(){ Val = 20000 };

            spaceBefore16.Append(spacingPercent16);
            A.BulletFont bulletFont14 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.CharacterBullet characterBullet14 = new A.CharacterBullet(){ Char = "»" };

            A.DefaultRunProperties defaultRunProperties50 = new A.DefaultRunProperties(){ FontSize = 2000, Kerning = 1200 };

            A.SolidFill solidFill62 = new A.SolidFill();
            A.SchemeColor schemeColor70 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill62.Append(schemeColor70);
            A.LatinFont latinFont45 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont36 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont45 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties50.Append(solidFill62);
            defaultRunProperties50.Append(latinFont45);
            defaultRunProperties50.Append(eastAsianFont36);
            defaultRunProperties50.Append(complexScriptFont45);

            level5ParagraphProperties5.Append(spaceBefore16);
            level5ParagraphProperties5.Append(bulletFont14);
            level5ParagraphProperties5.Append(characterBullet14);
            level5ParagraphProperties5.Append(defaultRunProperties50);

            A.Level6ParagraphProperties level6ParagraphProperties4 = new A.Level6ParagraphProperties(){ LeftMargin = 2514600, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.SpaceBefore spaceBefore17 = new A.SpaceBefore();
            A.SpacingPercent spacingPercent17 = new A.SpacingPercent(){ Val = 20000 };

            spaceBefore17.Append(spacingPercent17);
            A.BulletFont bulletFont15 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.CharacterBullet characterBullet15 = new A.CharacterBullet(){ Char = "•" };

            A.DefaultRunProperties defaultRunProperties51 = new A.DefaultRunProperties(){ FontSize = 2000, Kerning = 1200 };

            A.SolidFill solidFill63 = new A.SolidFill();
            A.SchemeColor schemeColor71 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill63.Append(schemeColor71);
            A.LatinFont latinFont46 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont37 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont46 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties51.Append(solidFill63);
            defaultRunProperties51.Append(latinFont46);
            defaultRunProperties51.Append(eastAsianFont37);
            defaultRunProperties51.Append(complexScriptFont46);

            level6ParagraphProperties4.Append(spaceBefore17);
            level6ParagraphProperties4.Append(bulletFont15);
            level6ParagraphProperties4.Append(characterBullet15);
            level6ParagraphProperties4.Append(defaultRunProperties51);

            A.Level7ParagraphProperties level7ParagraphProperties4 = new A.Level7ParagraphProperties(){ LeftMargin = 2971800, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.SpaceBefore spaceBefore18 = new A.SpaceBefore();
            A.SpacingPercent spacingPercent18 = new A.SpacingPercent(){ Val = 20000 };

            spaceBefore18.Append(spacingPercent18);
            A.BulletFont bulletFont16 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.CharacterBullet characterBullet16 = new A.CharacterBullet(){ Char = "•" };

            A.DefaultRunProperties defaultRunProperties52 = new A.DefaultRunProperties(){ FontSize = 2000, Kerning = 1200 };

            A.SolidFill solidFill64 = new A.SolidFill();
            A.SchemeColor schemeColor72 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill64.Append(schemeColor72);
            A.LatinFont latinFont47 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont38 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont47 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties52.Append(solidFill64);
            defaultRunProperties52.Append(latinFont47);
            defaultRunProperties52.Append(eastAsianFont38);
            defaultRunProperties52.Append(complexScriptFont47);

            level7ParagraphProperties4.Append(spaceBefore18);
            level7ParagraphProperties4.Append(bulletFont16);
            level7ParagraphProperties4.Append(characterBullet16);
            level7ParagraphProperties4.Append(defaultRunProperties52);

            A.Level8ParagraphProperties level8ParagraphProperties4 = new A.Level8ParagraphProperties(){ LeftMargin = 3429000, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.SpaceBefore spaceBefore19 = new A.SpaceBefore();
            A.SpacingPercent spacingPercent19 = new A.SpacingPercent(){ Val = 20000 };

            spaceBefore19.Append(spacingPercent19);
            A.BulletFont bulletFont17 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.CharacterBullet characterBullet17 = new A.CharacterBullet(){ Char = "•" };

            A.DefaultRunProperties defaultRunProperties53 = new A.DefaultRunProperties(){ FontSize = 2000, Kerning = 1200 };

            A.SolidFill solidFill65 = new A.SolidFill();
            A.SchemeColor schemeColor73 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill65.Append(schemeColor73);
            A.LatinFont latinFont48 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont39 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont48 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties53.Append(solidFill65);
            defaultRunProperties53.Append(latinFont48);
            defaultRunProperties53.Append(eastAsianFont39);
            defaultRunProperties53.Append(complexScriptFont48);

            level8ParagraphProperties4.Append(spaceBefore19);
            level8ParagraphProperties4.Append(bulletFont17);
            level8ParagraphProperties4.Append(characterBullet17);
            level8ParagraphProperties4.Append(defaultRunProperties53);

            A.Level9ParagraphProperties level9ParagraphProperties4 = new A.Level9ParagraphProperties(){ LeftMargin = 3886200, Indent = -228600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.SpaceBefore spaceBefore20 = new A.SpaceBefore();
            A.SpacingPercent spacingPercent20 = new A.SpacingPercent(){ Val = 20000 };

            spaceBefore20.Append(spacingPercent20);
            A.BulletFont bulletFont18 = new A.BulletFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.CharacterBullet characterBullet18 = new A.CharacterBullet(){ Char = "•" };

            A.DefaultRunProperties defaultRunProperties54 = new A.DefaultRunProperties(){ FontSize = 2000, Kerning = 1200 };

            A.SolidFill solidFill66 = new A.SolidFill();
            A.SchemeColor schemeColor74 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill66.Append(schemeColor74);
            A.LatinFont latinFont49 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont40 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont49 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties54.Append(solidFill66);
            defaultRunProperties54.Append(latinFont49);
            defaultRunProperties54.Append(eastAsianFont40);
            defaultRunProperties54.Append(complexScriptFont49);

            level9ParagraphProperties4.Append(spaceBefore20);
            level9ParagraphProperties4.Append(bulletFont18);
            level9ParagraphProperties4.Append(characterBullet18);
            level9ParagraphProperties4.Append(defaultRunProperties54);

            bodyStyle2.Append(level1ParagraphProperties16);
            bodyStyle2.Append(level2ParagraphProperties5);
            bodyStyle2.Append(level3ParagraphProperties5);
            bodyStyle2.Append(level4ParagraphProperties5);
            bodyStyle2.Append(level5ParagraphProperties5);
            bodyStyle2.Append(level6ParagraphProperties4);
            bodyStyle2.Append(level7ParagraphProperties4);
            bodyStyle2.Append(level8ParagraphProperties4);
            bodyStyle2.Append(level9ParagraphProperties4);

            OtherStyle otherStyle2 = new OtherStyle();

            A.DefaultParagraphProperties defaultParagraphProperties3 = new A.DefaultParagraphProperties();
            A.DefaultRunProperties defaultRunProperties55 = new A.DefaultRunProperties(){ Language = "en-US" };

            defaultParagraphProperties3.Append(defaultRunProperties55);

            A.Level1ParagraphProperties level1ParagraphProperties17 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties56 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

            A.SolidFill solidFill67 = new A.SolidFill();
            A.SchemeColor schemeColor75 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill67.Append(schemeColor75);
            A.LatinFont latinFont50 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont41 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont50 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties56.Append(solidFill67);
            defaultRunProperties56.Append(latinFont50);
            defaultRunProperties56.Append(eastAsianFont41);
            defaultRunProperties56.Append(complexScriptFont50);

            level1ParagraphProperties17.Append(defaultRunProperties56);

            A.Level2ParagraphProperties level2ParagraphProperties6 = new A.Level2ParagraphProperties(){ LeftMargin = 457200, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties57 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

            A.SolidFill solidFill68 = new A.SolidFill();
            A.SchemeColor schemeColor76 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill68.Append(schemeColor76);
            A.LatinFont latinFont51 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont42 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont51 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties57.Append(solidFill68);
            defaultRunProperties57.Append(latinFont51);
            defaultRunProperties57.Append(eastAsianFont42);
            defaultRunProperties57.Append(complexScriptFont51);

            level2ParagraphProperties6.Append(defaultRunProperties57);

            A.Level3ParagraphProperties level3ParagraphProperties6 = new A.Level3ParagraphProperties(){ LeftMargin = 914400, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties58 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

            A.SolidFill solidFill69 = new A.SolidFill();
            A.SchemeColor schemeColor77 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill69.Append(schemeColor77);
            A.LatinFont latinFont52 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont43 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont52 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties58.Append(solidFill69);
            defaultRunProperties58.Append(latinFont52);
            defaultRunProperties58.Append(eastAsianFont43);
            defaultRunProperties58.Append(complexScriptFont52);

            level3ParagraphProperties6.Append(defaultRunProperties58);

            A.Level4ParagraphProperties level4ParagraphProperties6 = new A.Level4ParagraphProperties(){ LeftMargin = 1371600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties59 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

            A.SolidFill solidFill70 = new A.SolidFill();
            A.SchemeColor schemeColor78 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill70.Append(schemeColor78);
            A.LatinFont latinFont53 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont44 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont53 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties59.Append(solidFill70);
            defaultRunProperties59.Append(latinFont53);
            defaultRunProperties59.Append(eastAsianFont44);
            defaultRunProperties59.Append(complexScriptFont53);

            level4ParagraphProperties6.Append(defaultRunProperties59);

            A.Level5ParagraphProperties level5ParagraphProperties6 = new A.Level5ParagraphProperties(){ LeftMargin = 1828800, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties60 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

            A.SolidFill solidFill71 = new A.SolidFill();
            A.SchemeColor schemeColor79 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill71.Append(schemeColor79);
            A.LatinFont latinFont54 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont45 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont54 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties60.Append(solidFill71);
            defaultRunProperties60.Append(latinFont54);
            defaultRunProperties60.Append(eastAsianFont45);
            defaultRunProperties60.Append(complexScriptFont54);

            level5ParagraphProperties6.Append(defaultRunProperties60);

            A.Level6ParagraphProperties level6ParagraphProperties5 = new A.Level6ParagraphProperties(){ LeftMargin = 2286000, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties61 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

            A.SolidFill solidFill72 = new A.SolidFill();
            A.SchemeColor schemeColor80 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill72.Append(schemeColor80);
            A.LatinFont latinFont55 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont46 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont55 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties61.Append(solidFill72);
            defaultRunProperties61.Append(latinFont55);
            defaultRunProperties61.Append(eastAsianFont46);
            defaultRunProperties61.Append(complexScriptFont55);

            level6ParagraphProperties5.Append(defaultRunProperties61);

            A.Level7ParagraphProperties level7ParagraphProperties5 = new A.Level7ParagraphProperties(){ LeftMargin = 2743200, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties62 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

            A.SolidFill solidFill73 = new A.SolidFill();
            A.SchemeColor schemeColor81 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill73.Append(schemeColor81);
            A.LatinFont latinFont56 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont47 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont56 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties62.Append(solidFill73);
            defaultRunProperties62.Append(latinFont56);
            defaultRunProperties62.Append(eastAsianFont47);
            defaultRunProperties62.Append(complexScriptFont56);

            level7ParagraphProperties5.Append(defaultRunProperties62);

            A.Level8ParagraphProperties level8ParagraphProperties5 = new A.Level8ParagraphProperties(){ LeftMargin = 3200400, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties63 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

            A.SolidFill solidFill74 = new A.SolidFill();
            A.SchemeColor schemeColor82 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill74.Append(schemeColor82);
            A.LatinFont latinFont57 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont48 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont57 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties63.Append(solidFill74);
            defaultRunProperties63.Append(latinFont57);
            defaultRunProperties63.Append(eastAsianFont48);
            defaultRunProperties63.Append(complexScriptFont57);

            level8ParagraphProperties5.Append(defaultRunProperties63);

            A.Level9ParagraphProperties level9ParagraphProperties5 = new A.Level9ParagraphProperties(){ LeftMargin = 3657600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties64 = new A.DefaultRunProperties(){ FontSize = 1800, Kerning = 1200 };

            A.SolidFill solidFill75 = new A.SolidFill();
            A.SchemeColor schemeColor83 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill75.Append(schemeColor83);
            A.LatinFont latinFont58 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont49 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont58 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties64.Append(solidFill75);
            defaultRunProperties64.Append(latinFont58);
            defaultRunProperties64.Append(eastAsianFont49);
            defaultRunProperties64.Append(complexScriptFont58);

            level9ParagraphProperties5.Append(defaultRunProperties64);

            otherStyle2.Append(defaultParagraphProperties3);
            otherStyle2.Append(level1ParagraphProperties17);
            otherStyle2.Append(level2ParagraphProperties6);
            otherStyle2.Append(level3ParagraphProperties6);
            otherStyle2.Append(level4ParagraphProperties6);
            otherStyle2.Append(level5ParagraphProperties6);
            otherStyle2.Append(level6ParagraphProperties5);
            otherStyle2.Append(level7ParagraphProperties5);
            otherStyle2.Append(level8ParagraphProperties5);
            otherStyle2.Append(level9ParagraphProperties5);

            textStyles2.Append(titleStyle2);
            textStyles2.Append(bodyStyle2);
            textStyles2.Append(otherStyle2);

            slideMaster2.Append(commonSlideData4);
            slideMaster2.Append(colorMap2);
            slideMaster2.Append(slideLayoutIdList2);
            slideMaster2.Append(textStyles2);

            slideMasterPart2.SlideMaster = slideMaster2;
        }

        // Generates content of themePart2.
        private void GenerateThemePart2Content(ThemePart themePart2)
        {
            A.Theme theme2 = new A.Theme(){ Name = "WebbQPS" };
            theme2.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            A.ThemeElements themeElements2 = new A.ThemeElements();

            A.ColorScheme colorScheme2 = new A.ColorScheme(){ Name = "Office" };

            A.Dark1Color dark1Color2 = new A.Dark1Color();
            A.SystemColor systemColor3 = new A.SystemColor(){ Val = A.SystemColorValues.WindowText, LastColor = "000000" };

            dark1Color2.Append(systemColor3);

            A.Light1Color light1Color2 = new A.Light1Color();
            A.SystemColor systemColor4 = new A.SystemColor(){ Val = A.SystemColorValues.Window, LastColor = "FFFFFF" };

            light1Color2.Append(systemColor4);

            A.Dark2Color dark2Color2 = new A.Dark2Color();
            A.RgbColorModelHex rgbColorModelHex37 = new A.RgbColorModelHex(){ Val = "1F497D" };

            dark2Color2.Append(rgbColorModelHex37);

            A.Light2Color light2Color2 = new A.Light2Color();
            A.RgbColorModelHex rgbColorModelHex38 = new A.RgbColorModelHex(){ Val = "EEECE1" };

            light2Color2.Append(rgbColorModelHex38);

            A.Accent1Color accent1Color2 = new A.Accent1Color();
            A.RgbColorModelHex rgbColorModelHex39 = new A.RgbColorModelHex(){ Val = "4F81BD" };

            accent1Color2.Append(rgbColorModelHex39);

            A.Accent2Color accent2Color2 = new A.Accent2Color();
            A.RgbColorModelHex rgbColorModelHex40 = new A.RgbColorModelHex(){ Val = "C0504D" };

            accent2Color2.Append(rgbColorModelHex40);

            A.Accent3Color accent3Color2 = new A.Accent3Color();
            A.RgbColorModelHex rgbColorModelHex41 = new A.RgbColorModelHex(){ Val = "9BBB59" };

            accent3Color2.Append(rgbColorModelHex41);

            A.Accent4Color accent4Color2 = new A.Accent4Color();
            A.RgbColorModelHex rgbColorModelHex42 = new A.RgbColorModelHex(){ Val = "8064A2" };

            accent4Color2.Append(rgbColorModelHex42);

            A.Accent5Color accent5Color2 = new A.Accent5Color();
            A.RgbColorModelHex rgbColorModelHex43 = new A.RgbColorModelHex(){ Val = "4BACC6" };

            accent5Color2.Append(rgbColorModelHex43);

            A.Accent6Color accent6Color2 = new A.Accent6Color();
            A.RgbColorModelHex rgbColorModelHex44 = new A.RgbColorModelHex(){ Val = "F79646" };

            accent6Color2.Append(rgbColorModelHex44);

            A.Hyperlink hyperlink2 = new A.Hyperlink();
            A.RgbColorModelHex rgbColorModelHex45 = new A.RgbColorModelHex(){ Val = "0000FF" };

            hyperlink2.Append(rgbColorModelHex45);

            A.FollowedHyperlinkColor followedHyperlinkColor2 = new A.FollowedHyperlinkColor();
            A.RgbColorModelHex rgbColorModelHex46 = new A.RgbColorModelHex(){ Val = "800080" };

            followedHyperlinkColor2.Append(rgbColorModelHex46);

            colorScheme2.Append(dark1Color2);
            colorScheme2.Append(light1Color2);
            colorScheme2.Append(dark2Color2);
            colorScheme2.Append(light2Color2);
            colorScheme2.Append(accent1Color2);
            colorScheme2.Append(accent2Color2);
            colorScheme2.Append(accent3Color2);
            colorScheme2.Append(accent4Color2);
            colorScheme2.Append(accent5Color2);
            colorScheme2.Append(accent6Color2);
            colorScheme2.Append(hyperlink2);
            colorScheme2.Append(followedHyperlinkColor2);

            A.FontScheme fontScheme2 = new A.FontScheme(){ Name = "Office" };

            A.MajorFont majorFont2 = new A.MajorFont();
            A.LatinFont latinFont59 = new A.LatinFont(){ Typeface = "Calibri" };
            A.EastAsianFont eastAsianFont50 = new A.EastAsianFont(){ Typeface = "" };
            A.ComplexScriptFont complexScriptFont59 = new A.ComplexScriptFont(){ Typeface = "" };
            A.SupplementalFont supplementalFont61 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
            A.SupplementalFont supplementalFont62 = new A.SupplementalFont(){ Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont63 = new A.SupplementalFont(){ Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont64 = new A.SupplementalFont(){ Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont65 = new A.SupplementalFont(){ Script = "Arab", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont66 = new A.SupplementalFont(){ Script = "Hebr", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont67 = new A.SupplementalFont(){ Script = "Thai", Typeface = "Angsana New" };
            A.SupplementalFont supplementalFont68 = new A.SupplementalFont(){ Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont69 = new A.SupplementalFont(){ Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont70 = new A.SupplementalFont(){ Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont71 = new A.SupplementalFont(){ Script = "Khmr", Typeface = "MoolBoran" };
            A.SupplementalFont supplementalFont72 = new A.SupplementalFont(){ Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont73 = new A.SupplementalFont(){ Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont74 = new A.SupplementalFont(){ Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont75 = new A.SupplementalFont(){ Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont76 = new A.SupplementalFont(){ Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont77 = new A.SupplementalFont(){ Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont78 = new A.SupplementalFont(){ Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont79 = new A.SupplementalFont(){ Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont80 = new A.SupplementalFont(){ Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont81 = new A.SupplementalFont(){ Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont82 = new A.SupplementalFont(){ Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont83 = new A.SupplementalFont(){ Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont84 = new A.SupplementalFont(){ Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont85 = new A.SupplementalFont(){ Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont86 = new A.SupplementalFont(){ Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont87 = new A.SupplementalFont(){ Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont88 = new A.SupplementalFont(){ Script = "Viet", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont89 = new A.SupplementalFont(){ Script = "Uigh", Typeface = "Microsoft Uighur" };

            majorFont2.Append(latinFont59);
            majorFont2.Append(eastAsianFont50);
            majorFont2.Append(complexScriptFont59);
            majorFont2.Append(supplementalFont61);
            majorFont2.Append(supplementalFont62);
            majorFont2.Append(supplementalFont63);
            majorFont2.Append(supplementalFont64);
            majorFont2.Append(supplementalFont65);
            majorFont2.Append(supplementalFont66);
            majorFont2.Append(supplementalFont67);
            majorFont2.Append(supplementalFont68);
            majorFont2.Append(supplementalFont69);
            majorFont2.Append(supplementalFont70);
            majorFont2.Append(supplementalFont71);
            majorFont2.Append(supplementalFont72);
            majorFont2.Append(supplementalFont73);
            majorFont2.Append(supplementalFont74);
            majorFont2.Append(supplementalFont75);
            majorFont2.Append(supplementalFont76);
            majorFont2.Append(supplementalFont77);
            majorFont2.Append(supplementalFont78);
            majorFont2.Append(supplementalFont79);
            majorFont2.Append(supplementalFont80);
            majorFont2.Append(supplementalFont81);
            majorFont2.Append(supplementalFont82);
            majorFont2.Append(supplementalFont83);
            majorFont2.Append(supplementalFont84);
            majorFont2.Append(supplementalFont85);
            majorFont2.Append(supplementalFont86);
            majorFont2.Append(supplementalFont87);
            majorFont2.Append(supplementalFont88);
            majorFont2.Append(supplementalFont89);

            A.MinorFont minorFont2 = new A.MinorFont();
            A.LatinFont latinFont60 = new A.LatinFont(){ Typeface = "Calibri" };
            A.EastAsianFont eastAsianFont51 = new A.EastAsianFont(){ Typeface = "" };
            A.ComplexScriptFont complexScriptFont60 = new A.ComplexScriptFont(){ Typeface = "" };
            A.SupplementalFont supplementalFont90 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
            A.SupplementalFont supplementalFont91 = new A.SupplementalFont(){ Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont92 = new A.SupplementalFont(){ Script = "Hans", Typeface = "宋体" };
            A.SupplementalFont supplementalFont93 = new A.SupplementalFont(){ Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont94 = new A.SupplementalFont(){ Script = "Arab", Typeface = "Arial" };
            A.SupplementalFont supplementalFont95 = new A.SupplementalFont(){ Script = "Hebr", Typeface = "Arial" };
            A.SupplementalFont supplementalFont96 = new A.SupplementalFont(){ Script = "Thai", Typeface = "Cordia New" };
            A.SupplementalFont supplementalFont97 = new A.SupplementalFont(){ Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont98 = new A.SupplementalFont(){ Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont99 = new A.SupplementalFont(){ Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont100 = new A.SupplementalFont(){ Script = "Khmr", Typeface = "DaunPenh" };
            A.SupplementalFont supplementalFont101 = new A.SupplementalFont(){ Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont102 = new A.SupplementalFont(){ Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont103 = new A.SupplementalFont(){ Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont104 = new A.SupplementalFont(){ Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont105 = new A.SupplementalFont(){ Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont106 = new A.SupplementalFont(){ Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont107 = new A.SupplementalFont(){ Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont108 = new A.SupplementalFont(){ Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont109 = new A.SupplementalFont(){ Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont110 = new A.SupplementalFont(){ Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont111 = new A.SupplementalFont(){ Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont112 = new A.SupplementalFont(){ Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont113 = new A.SupplementalFont(){ Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont114 = new A.SupplementalFont(){ Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont115 = new A.SupplementalFont(){ Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont116 = new A.SupplementalFont(){ Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont117 = new A.SupplementalFont(){ Script = "Viet", Typeface = "Arial" };
            A.SupplementalFont supplementalFont118 = new A.SupplementalFont(){ Script = "Uigh", Typeface = "Microsoft Uighur" };

            minorFont2.Append(latinFont60);
            minorFont2.Append(eastAsianFont51);
            minorFont2.Append(complexScriptFont60);
            minorFont2.Append(supplementalFont90);
            minorFont2.Append(supplementalFont91);
            minorFont2.Append(supplementalFont92);
            minorFont2.Append(supplementalFont93);
            minorFont2.Append(supplementalFont94);
            minorFont2.Append(supplementalFont95);
            minorFont2.Append(supplementalFont96);
            minorFont2.Append(supplementalFont97);
            minorFont2.Append(supplementalFont98);
            minorFont2.Append(supplementalFont99);
            minorFont2.Append(supplementalFont100);
            minorFont2.Append(supplementalFont101);
            minorFont2.Append(supplementalFont102);
            minorFont2.Append(supplementalFont103);
            minorFont2.Append(supplementalFont104);
            minorFont2.Append(supplementalFont105);
            minorFont2.Append(supplementalFont106);
            minorFont2.Append(supplementalFont107);
            minorFont2.Append(supplementalFont108);
            minorFont2.Append(supplementalFont109);
            minorFont2.Append(supplementalFont110);
            minorFont2.Append(supplementalFont111);
            minorFont2.Append(supplementalFont112);
            minorFont2.Append(supplementalFont113);
            minorFont2.Append(supplementalFont114);
            minorFont2.Append(supplementalFont115);
            minorFont2.Append(supplementalFont116);
            minorFont2.Append(supplementalFont117);
            minorFont2.Append(supplementalFont118);

            fontScheme2.Append(majorFont2);
            fontScheme2.Append(minorFont2);

            A.FormatScheme formatScheme2 = new A.FormatScheme(){ Name = "Office" };

            A.FillStyleList fillStyleList2 = new A.FillStyleList();

            A.SolidFill solidFill76 = new A.SolidFill();
            A.SchemeColor schemeColor84 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill76.Append(schemeColor84);

            A.GradientFill gradientFill5 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList5 = new A.GradientStopList();

            A.GradientStop gradientStop12 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor85 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint13 = new A.Tint(){ Val = 50000 };
            A.SaturationModulation saturationModulation13 = new A.SaturationModulation(){ Val = 300000 };

            schemeColor85.Append(tint13);
            schemeColor85.Append(saturationModulation13);

            gradientStop12.Append(schemeColor85);

            A.GradientStop gradientStop13 = new A.GradientStop(){ Position = 35000 };

            A.SchemeColor schemeColor86 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint14 = new A.Tint(){ Val = 37000 };
            A.SaturationModulation saturationModulation14 = new A.SaturationModulation(){ Val = 300000 };

            schemeColor86.Append(tint14);
            schemeColor86.Append(saturationModulation14);

            gradientStop13.Append(schemeColor86);

            A.GradientStop gradientStop14 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor87 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint15 = new A.Tint(){ Val = 15000 };
            A.SaturationModulation saturationModulation15 = new A.SaturationModulation(){ Val = 350000 };

            schemeColor87.Append(tint15);
            schemeColor87.Append(saturationModulation15);

            gradientStop14.Append(schemeColor87);

            gradientStopList5.Append(gradientStop12);
            gradientStopList5.Append(gradientStop13);
            gradientStopList5.Append(gradientStop14);
            A.LinearGradientFill linearGradientFill3 = new A.LinearGradientFill(){ Angle = 16200000, Scaled = true };

            gradientFill5.Append(gradientStopList5);
            gradientFill5.Append(linearGradientFill3);

            A.GradientFill gradientFill6 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList6 = new A.GradientStopList();

            A.GradientStop gradientStop15 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor88 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade11 = new A.Shade(){ Val = 51000 };
            A.SaturationModulation saturationModulation16 = new A.SaturationModulation(){ Val = 130000 };

            schemeColor88.Append(shade11);
            schemeColor88.Append(saturationModulation16);

            gradientStop15.Append(schemeColor88);

            A.GradientStop gradientStop16 = new A.GradientStop(){ Position = 80000 };

            A.SchemeColor schemeColor89 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade12 = new A.Shade(){ Val = 93000 };
            A.SaturationModulation saturationModulation17 = new A.SaturationModulation(){ Val = 130000 };

            schemeColor89.Append(shade12);
            schemeColor89.Append(saturationModulation17);

            gradientStop16.Append(schemeColor89);

            A.GradientStop gradientStop17 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor90 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade13 = new A.Shade(){ Val = 94000 };
            A.SaturationModulation saturationModulation18 = new A.SaturationModulation(){ Val = 135000 };

            schemeColor90.Append(shade13);
            schemeColor90.Append(saturationModulation18);

            gradientStop17.Append(schemeColor90);

            gradientStopList6.Append(gradientStop15);
            gradientStopList6.Append(gradientStop16);
            gradientStopList6.Append(gradientStop17);
            A.LinearGradientFill linearGradientFill4 = new A.LinearGradientFill(){ Angle = 16200000, Scaled = false };

            gradientFill6.Append(gradientStopList6);
            gradientFill6.Append(linearGradientFill4);

            fillStyleList2.Append(solidFill76);
            fillStyleList2.Append(gradientFill5);
            fillStyleList2.Append(gradientFill6);

            A.LineStyleList lineStyleList2 = new A.LineStyleList();

            A.Outline outline7 = new A.Outline(){ Width = 9525, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill77 = new A.SolidFill();

            A.SchemeColor schemeColor91 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade14 = new A.Shade(){ Val = 95000 };
            A.SaturationModulation saturationModulation19 = new A.SaturationModulation(){ Val = 105000 };

            schemeColor91.Append(shade14);
            schemeColor91.Append(saturationModulation19);

            solidFill77.Append(schemeColor91);
            A.PresetDash presetDash4 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

            outline7.Append(solidFill77);
            outline7.Append(presetDash4);

            A.Outline outline8 = new A.Outline(){ Width = 25400, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill78 = new A.SolidFill();
            A.SchemeColor schemeColor92 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill78.Append(schemeColor92);
            A.PresetDash presetDash5 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

            outline8.Append(solidFill78);
            outline8.Append(presetDash5);

            A.Outline outline9 = new A.Outline(){ Width = 38100, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill79 = new A.SolidFill();
            A.SchemeColor schemeColor93 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill79.Append(schemeColor93);
            A.PresetDash presetDash6 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };

            outline9.Append(solidFill79);
            outline9.Append(presetDash6);

            lineStyleList2.Append(outline7);
            lineStyleList2.Append(outline8);
            lineStyleList2.Append(outline9);

            A.EffectStyleList effectStyleList2 = new A.EffectStyleList();

            A.EffectStyle effectStyle4 = new A.EffectStyle();

            A.EffectList effectList4 = new A.EffectList();

            A.OuterShadow outerShadow4 = new A.OuterShadow(){ BlurRadius = 40000L, Distance = 20000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex47 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha4 = new A.Alpha(){ Val = 38000 };

            rgbColorModelHex47.Append(alpha4);

            outerShadow4.Append(rgbColorModelHex47);

            effectList4.Append(outerShadow4);

            effectStyle4.Append(effectList4);

            A.EffectStyle effectStyle5 = new A.EffectStyle();

            A.EffectList effectList5 = new A.EffectList();

            A.OuterShadow outerShadow5 = new A.OuterShadow(){ BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex48 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha5 = new A.Alpha(){ Val = 35000 };

            rgbColorModelHex48.Append(alpha5);

            outerShadow5.Append(rgbColorModelHex48);

            effectList5.Append(outerShadow5);

            effectStyle5.Append(effectList5);

            A.EffectStyle effectStyle6 = new A.EffectStyle();

            A.EffectList effectList6 = new A.EffectList();

            A.OuterShadow outerShadow6 = new A.OuterShadow(){ BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex49 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha6 = new A.Alpha(){ Val = 35000 };

            rgbColorModelHex49.Append(alpha6);

            outerShadow6.Append(rgbColorModelHex49);

            effectList6.Append(outerShadow6);

            A.Scene3DType scene3DType2 = new A.Scene3DType();

            A.Camera camera2 = new A.Camera(){ Preset = A.PresetCameraValues.OrthographicFront };
            A.Rotation rotation3 = new A.Rotation(){ Latitude = 0, Longitude = 0, Revolution = 0 };

            camera2.Append(rotation3);

            A.LightRig lightRig2 = new A.LightRig(){ Rig = A.LightRigValues.ThreePoints, Direction = A.LightRigDirectionValues.Top };
            A.Rotation rotation4 = new A.Rotation(){ Latitude = 0, Longitude = 0, Revolution = 1200000 };

            lightRig2.Append(rotation4);

            scene3DType2.Append(camera2);
            scene3DType2.Append(lightRig2);

            A.Shape3DType shape3DType2 = new A.Shape3DType();
            A.BevelTop bevelTop2 = new A.BevelTop(){ Width = 63500L, Height = 25400L };

            shape3DType2.Append(bevelTop2);

            effectStyle6.Append(effectList6);
            effectStyle6.Append(scene3DType2);
            effectStyle6.Append(shape3DType2);

            effectStyleList2.Append(effectStyle4);
            effectStyleList2.Append(effectStyle5);
            effectStyleList2.Append(effectStyle6);

            A.BackgroundFillStyleList backgroundFillStyleList2 = new A.BackgroundFillStyleList();

            A.SolidFill solidFill80 = new A.SolidFill();
            A.SchemeColor schemeColor94 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill80.Append(schemeColor94);

            A.GradientFill gradientFill7 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList7 = new A.GradientStopList();

            A.GradientStop gradientStop18 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor95 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint16 = new A.Tint(){ Val = 40000 };
            A.SaturationModulation saturationModulation20 = new A.SaturationModulation(){ Val = 350000 };

            schemeColor95.Append(tint16);
            schemeColor95.Append(saturationModulation20);

            gradientStop18.Append(schemeColor95);

            A.GradientStop gradientStop19 = new A.GradientStop(){ Position = 40000 };

            A.SchemeColor schemeColor96 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint17 = new A.Tint(){ Val = 45000 };
            A.Shade shade15 = new A.Shade(){ Val = 99000 };
            A.SaturationModulation saturationModulation21 = new A.SaturationModulation(){ Val = 350000 };

            schemeColor96.Append(tint17);
            schemeColor96.Append(shade15);
            schemeColor96.Append(saturationModulation21);

            gradientStop19.Append(schemeColor96);

            A.GradientStop gradientStop20 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor97 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade16 = new A.Shade(){ Val = 20000 };
            A.SaturationModulation saturationModulation22 = new A.SaturationModulation(){ Val = 255000 };

            schemeColor97.Append(shade16);
            schemeColor97.Append(saturationModulation22);

            gradientStop20.Append(schemeColor97);

            gradientStopList7.Append(gradientStop18);
            gradientStopList7.Append(gradientStop19);
            gradientStopList7.Append(gradientStop20);

            A.PathGradientFill pathGradientFill3 = new A.PathGradientFill(){ Path = A.PathShadeValues.Circle };
            A.FillToRectangle fillToRectangle3 = new A.FillToRectangle(){ Left = 50000, Top = -80000, Right = 50000, Bottom = 180000 };

            pathGradientFill3.Append(fillToRectangle3);

            gradientFill7.Append(gradientStopList7);
            gradientFill7.Append(pathGradientFill3);

            A.GradientFill gradientFill8 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList8 = new A.GradientStopList();

            A.GradientStop gradientStop21 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor98 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint18 = new A.Tint(){ Val = 80000 };
            A.SaturationModulation saturationModulation23 = new A.SaturationModulation(){ Val = 300000 };

            schemeColor98.Append(tint18);
            schemeColor98.Append(saturationModulation23);

            gradientStop21.Append(schemeColor98);

            A.GradientStop gradientStop22 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor99 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade17 = new A.Shade(){ Val = 30000 };
            A.SaturationModulation saturationModulation24 = new A.SaturationModulation(){ Val = 200000 };

            schemeColor99.Append(shade17);
            schemeColor99.Append(saturationModulation24);

            gradientStop22.Append(schemeColor99);

            gradientStopList8.Append(gradientStop21);
            gradientStopList8.Append(gradientStop22);

            A.PathGradientFill pathGradientFill4 = new A.PathGradientFill(){ Path = A.PathShadeValues.Circle };
            A.FillToRectangle fillToRectangle4 = new A.FillToRectangle(){ Left = 50000, Top = 50000, Right = 50000, Bottom = 50000 };

            pathGradientFill4.Append(fillToRectangle4);

            gradientFill8.Append(gradientStopList8);
            gradientFill8.Append(pathGradientFill4);

            backgroundFillStyleList2.Append(solidFill80);
            backgroundFillStyleList2.Append(gradientFill7);
            backgroundFillStyleList2.Append(gradientFill8);

            formatScheme2.Append(fillStyleList2);
            formatScheme2.Append(lineStyleList2);
            formatScheme2.Append(effectStyleList2);
            formatScheme2.Append(backgroundFillStyleList2);

            themeElements2.Append(colorScheme2);
            themeElements2.Append(fontScheme2);
            themeElements2.Append(formatScheme2);
            A.ObjectDefaults objectDefaults2 = new A.ObjectDefaults();
            A.ExtraColorSchemeList extraColorSchemeList2 = new A.ExtraColorSchemeList();

            theme2.Append(themeElements2);
            theme2.Append(objectDefaults2);
            theme2.Append(extraColorSchemeList2);

            themePart2.Theme = theme2;
        }

        // Generates content of slideLayoutPart3.
        private void GenerateSlideLayoutPart3Content(SlideLayoutPart slideLayoutPart3)
        {
            SlideLayout slideLayout3 = new SlideLayout(){ Type = SlideLayoutValues.Title, Preserve = true };
            slideLayout3.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            slideLayout3.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            slideLayout3.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            CommonSlideData commonSlideData5 = new CommonSlideData(){ Name = "Rubrikbild" };

            ShapeTree shapeTree5 = new ShapeTree();

            NonVisualGroupShapeProperties nonVisualGroupShapeProperties5 = new NonVisualGroupShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties28 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
            NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties5 = new NonVisualGroupShapeDrawingProperties();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties28 = new ApplicationNonVisualDrawingProperties();

            nonVisualGroupShapeProperties5.Append(nonVisualDrawingProperties28);
            nonVisualGroupShapeProperties5.Append(nonVisualGroupShapeDrawingProperties5);
            nonVisualGroupShapeProperties5.Append(applicationNonVisualDrawingProperties28);

            GroupShapeProperties groupShapeProperties5 = new GroupShapeProperties();

            A.TransformGroup transformGroup5 = new A.TransformGroup();
            A.Offset offset28 = new A.Offset(){ X = 0L, Y = 0L };
            A.Extents extents28 = new A.Extents(){ Cx = 0L, Cy = 0L };
            A.ChildOffset childOffset5 = new A.ChildOffset(){ X = 0L, Y = 0L };
            A.ChildExtents childExtents5 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

            transformGroup5.Append(offset28);
            transformGroup5.Append(extents28);
            transformGroup5.Append(childOffset5);
            transformGroup5.Append(childExtents5);

            groupShapeProperties5.Append(transformGroup5);

            Shape shape21 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties21 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties29 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title 1" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties21 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks16 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties21.Append(shapeLocks16);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties29 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape16 = new PlaceholderShape(){ Type = PlaceholderValues.CenteredTitle };

            applicationNonVisualDrawingProperties29.Append(placeholderShape16);

            nonVisualShapeProperties21.Append(nonVisualDrawingProperties29);
            nonVisualShapeProperties21.Append(nonVisualShapeDrawingProperties21);
            nonVisualShapeProperties21.Append(applicationNonVisualDrawingProperties29);

            ShapeProperties shapeProperties24 = new ShapeProperties();

            A.Transform2D transform2D24 = new A.Transform2D();
            A.Offset offset29 = new A.Offset(){ X = 685800L, Y = 2130425L };
            A.Extents extents29 = new A.Extents(){ Cx = 7772400L, Cy = 1470025L };

            transform2D24.Append(offset29);
            transform2D24.Append(extents29);

            shapeProperties24.Append(transform2D24);

            TextBody textBody21 = new TextBody();
            A.BodyProperties bodyProperties21 = new A.BodyProperties();
            A.ListStyle listStyle21 = new A.ListStyle();

            A.Paragraph paragraph29 = new A.Paragraph();

            A.Run run20 = new A.Run();
            A.RunProperties runProperties26 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text26 = new A.Text();
            text26.Text = "Klicka här för att ändra format";

            run20.Append(runProperties26);
            run20.Append(text26);
            A.EndParagraphRunProperties endParagraphRunProperties17 = new A.EndParagraphRunProperties(){ Language = "en-GB" };

            paragraph29.Append(run20);
            paragraph29.Append(endParagraphRunProperties17);

            textBody21.Append(bodyProperties21);
            textBody21.Append(listStyle21);
            textBody21.Append(paragraph29);

            shape21.Append(nonVisualShapeProperties21);
            shape21.Append(shapeProperties24);
            shape21.Append(textBody21);

            Shape shape22 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties22 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties30 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Subtitle 2" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties22 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks17 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties22.Append(shapeLocks17);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties30 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape17 = new PlaceholderShape(){ Type = PlaceholderValues.SubTitle, Index = (UInt32Value)1U };

            applicationNonVisualDrawingProperties30.Append(placeholderShape17);

            nonVisualShapeProperties22.Append(nonVisualDrawingProperties30);
            nonVisualShapeProperties22.Append(nonVisualShapeDrawingProperties22);
            nonVisualShapeProperties22.Append(applicationNonVisualDrawingProperties30);

            ShapeProperties shapeProperties25 = new ShapeProperties();

            A.Transform2D transform2D25 = new A.Transform2D();
            A.Offset offset30 = new A.Offset(){ X = 1371600L, Y = 3886200L };
            A.Extents extents30 = new A.Extents(){ Cx = 6400800L, Cy = 1752600L };

            transform2D25.Append(offset30);
            transform2D25.Append(extents30);

            shapeProperties25.Append(transform2D25);

            TextBody textBody22 = new TextBody();
            A.BodyProperties bodyProperties22 = new A.BodyProperties();

            A.ListStyle listStyle22 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties18 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
            A.NoBullet noBullet7 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties65 = new A.DefaultRunProperties();

            A.SolidFill solidFill81 = new A.SolidFill();

            A.SchemeColor schemeColor100 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint19 = new A.Tint(){ Val = 75000 };

            schemeColor100.Append(tint19);

            solidFill81.Append(schemeColor100);

            defaultRunProperties65.Append(solidFill81);

            level1ParagraphProperties18.Append(noBullet7);
            level1ParagraphProperties18.Append(defaultRunProperties65);

            A.Level2ParagraphProperties level2ParagraphProperties7 = new A.Level2ParagraphProperties(){ LeftMargin = 457200, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
            A.NoBullet noBullet8 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties66 = new A.DefaultRunProperties();

            A.SolidFill solidFill82 = new A.SolidFill();

            A.SchemeColor schemeColor101 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint20 = new A.Tint(){ Val = 75000 };

            schemeColor101.Append(tint20);

            solidFill82.Append(schemeColor101);

            defaultRunProperties66.Append(solidFill82);

            level2ParagraphProperties7.Append(noBullet8);
            level2ParagraphProperties7.Append(defaultRunProperties66);

            A.Level3ParagraphProperties level3ParagraphProperties7 = new A.Level3ParagraphProperties(){ LeftMargin = 914400, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
            A.NoBullet noBullet9 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties67 = new A.DefaultRunProperties();

            A.SolidFill solidFill83 = new A.SolidFill();

            A.SchemeColor schemeColor102 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint21 = new A.Tint(){ Val = 75000 };

            schemeColor102.Append(tint21);

            solidFill83.Append(schemeColor102);

            defaultRunProperties67.Append(solidFill83);

            level3ParagraphProperties7.Append(noBullet9);
            level3ParagraphProperties7.Append(defaultRunProperties67);

            A.Level4ParagraphProperties level4ParagraphProperties7 = new A.Level4ParagraphProperties(){ LeftMargin = 1371600, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
            A.NoBullet noBullet10 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties68 = new A.DefaultRunProperties();

            A.SolidFill solidFill84 = new A.SolidFill();

            A.SchemeColor schemeColor103 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint22 = new A.Tint(){ Val = 75000 };

            schemeColor103.Append(tint22);

            solidFill84.Append(schemeColor103);

            defaultRunProperties68.Append(solidFill84);

            level4ParagraphProperties7.Append(noBullet10);
            level4ParagraphProperties7.Append(defaultRunProperties68);

            A.Level5ParagraphProperties level5ParagraphProperties7 = new A.Level5ParagraphProperties(){ LeftMargin = 1828800, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
            A.NoBullet noBullet11 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties69 = new A.DefaultRunProperties();

            A.SolidFill solidFill85 = new A.SolidFill();

            A.SchemeColor schemeColor104 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint23 = new A.Tint(){ Val = 75000 };

            schemeColor104.Append(tint23);

            solidFill85.Append(schemeColor104);

            defaultRunProperties69.Append(solidFill85);

            level5ParagraphProperties7.Append(noBullet11);
            level5ParagraphProperties7.Append(defaultRunProperties69);

            A.Level6ParagraphProperties level6ParagraphProperties6 = new A.Level6ParagraphProperties(){ LeftMargin = 2286000, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
            A.NoBullet noBullet12 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties70 = new A.DefaultRunProperties();

            A.SolidFill solidFill86 = new A.SolidFill();

            A.SchemeColor schemeColor105 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint24 = new A.Tint(){ Val = 75000 };

            schemeColor105.Append(tint24);

            solidFill86.Append(schemeColor105);

            defaultRunProperties70.Append(solidFill86);

            level6ParagraphProperties6.Append(noBullet12);
            level6ParagraphProperties6.Append(defaultRunProperties70);

            A.Level7ParagraphProperties level7ParagraphProperties6 = new A.Level7ParagraphProperties(){ LeftMargin = 2743200, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
            A.NoBullet noBullet13 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties71 = new A.DefaultRunProperties();

            A.SolidFill solidFill87 = new A.SolidFill();

            A.SchemeColor schemeColor106 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint25 = new A.Tint(){ Val = 75000 };

            schemeColor106.Append(tint25);

            solidFill87.Append(schemeColor106);

            defaultRunProperties71.Append(solidFill87);

            level7ParagraphProperties6.Append(noBullet13);
            level7ParagraphProperties6.Append(defaultRunProperties71);

            A.Level8ParagraphProperties level8ParagraphProperties6 = new A.Level8ParagraphProperties(){ LeftMargin = 3200400, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
            A.NoBullet noBullet14 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties72 = new A.DefaultRunProperties();

            A.SolidFill solidFill88 = new A.SolidFill();

            A.SchemeColor schemeColor107 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint26 = new A.Tint(){ Val = 75000 };

            schemeColor107.Append(tint26);

            solidFill88.Append(schemeColor107);

            defaultRunProperties72.Append(solidFill88);

            level8ParagraphProperties6.Append(noBullet14);
            level8ParagraphProperties6.Append(defaultRunProperties72);

            A.Level9ParagraphProperties level9ParagraphProperties6 = new A.Level9ParagraphProperties(){ LeftMargin = 3657600, Indent = 0, Alignment = A.TextAlignmentTypeValues.Center };
            A.NoBullet noBullet15 = new A.NoBullet();

            A.DefaultRunProperties defaultRunProperties73 = new A.DefaultRunProperties();

            A.SolidFill solidFill89 = new A.SolidFill();

            A.SchemeColor schemeColor108 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };
            A.Tint tint27 = new A.Tint(){ Val = 75000 };

            schemeColor108.Append(tint27);

            solidFill89.Append(schemeColor108);

            defaultRunProperties73.Append(solidFill89);

            level9ParagraphProperties6.Append(noBullet15);
            level9ParagraphProperties6.Append(defaultRunProperties73);

            listStyle22.Append(level1ParagraphProperties18);
            listStyle22.Append(level2ParagraphProperties7);
            listStyle22.Append(level3ParagraphProperties7);
            listStyle22.Append(level4ParagraphProperties7);
            listStyle22.Append(level5ParagraphProperties7);
            listStyle22.Append(level6ParagraphProperties6);
            listStyle22.Append(level7ParagraphProperties6);
            listStyle22.Append(level8ParagraphProperties6);
            listStyle22.Append(level9ParagraphProperties6);

            A.Paragraph paragraph30 = new A.Paragraph();

            A.Run run21 = new A.Run();
            A.RunProperties runProperties27 = new A.RunProperties(){ Language = "sv-SE" };
            A.Text text27 = new A.Text();
            text27.Text = "Klicka här för att ändra format på underrubrik i bakgrunden";

            run21.Append(runProperties27);
            run21.Append(text27);
            A.EndParagraphRunProperties endParagraphRunProperties18 = new A.EndParagraphRunProperties(){ Language = "en-GB" };

            paragraph30.Append(run21);
            paragraph30.Append(endParagraphRunProperties18);

            textBody22.Append(bodyProperties22);
            textBody22.Append(listStyle22);
            textBody22.Append(paragraph30);

            shape22.Append(nonVisualShapeProperties22);
            shape22.Append(shapeProperties25);
            shape22.Append(textBody22);

            Shape shape23 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties23 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties31 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Date Placeholder 3" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties23 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks18 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties23.Append(shapeLocks18);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties31 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape18 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Size = PlaceholderSizeValues.Half, Index = (UInt32Value)10U };

            applicationNonVisualDrawingProperties31.Append(placeholderShape18);

            nonVisualShapeProperties23.Append(nonVisualDrawingProperties31);
            nonVisualShapeProperties23.Append(nonVisualShapeDrawingProperties23);
            nonVisualShapeProperties23.Append(applicationNonVisualDrawingProperties31);
            ShapeProperties shapeProperties26 = new ShapeProperties();

            TextBody textBody23 = new TextBody();
            A.BodyProperties bodyProperties23 = new A.BodyProperties();
            A.ListStyle listStyle23 = new A.ListStyle();

            A.Paragraph paragraph31 = new A.Paragraph();

            A.Field field7 = new A.Field(){ Id = "{9770A701-F095-403F-9DCF-B1CCDA9CEBEC}", Type = "datetimeFigureOut" };
            A.RunProperties runProperties28 = new A.RunProperties(){ Language = "en-US", SmartTagClean = false };
            A.ParagraphProperties paragraphProperties26 = new A.ParagraphProperties();
            A.Text text28 = new A.Text();
            text28.Text = "10/21/2016";

            field7.Append(runProperties28);
            field7.Append(paragraphProperties26);
            field7.Append(text28);
            A.EndParagraphRunProperties endParagraphRunProperties19 = new A.EndParagraphRunProperties(){ Language = "en-GB" };

            paragraph31.Append(field7);
            paragraph31.Append(endParagraphRunProperties19);

            textBody23.Append(bodyProperties23);
            textBody23.Append(listStyle23);
            textBody23.Append(paragraph31);

            shape23.Append(nonVisualShapeProperties23);
            shape23.Append(shapeProperties26);
            shape23.Append(textBody23);

            Shape shape24 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties24 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties32 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Footer Placeholder 4" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties24 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks19 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties24.Append(shapeLocks19);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties32 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape19 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)11U };

            applicationNonVisualDrawingProperties32.Append(placeholderShape19);

            nonVisualShapeProperties24.Append(nonVisualDrawingProperties32);
            nonVisualShapeProperties24.Append(nonVisualShapeDrawingProperties24);
            nonVisualShapeProperties24.Append(applicationNonVisualDrawingProperties32);
            ShapeProperties shapeProperties27 = new ShapeProperties();

            TextBody textBody24 = new TextBody();
            A.BodyProperties bodyProperties24 = new A.BodyProperties();
            A.ListStyle listStyle24 = new A.ListStyle();

            A.Paragraph paragraph32 = new A.Paragraph();
            A.EndParagraphRunProperties endParagraphRunProperties20 = new A.EndParagraphRunProperties(){ Language = "en-GB" };

            paragraph32.Append(endParagraphRunProperties20);

            textBody24.Append(bodyProperties24);
            textBody24.Append(listStyle24);
            textBody24.Append(paragraph32);

            shape24.Append(nonVisualShapeProperties24);
            shape24.Append(shapeProperties27);
            shape24.Append(textBody24);

            Shape shape25 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties25 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties33 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Slide Number Placeholder 5" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties25 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks20 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties25.Append(shapeLocks20);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties33 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape20 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)12U };

            applicationNonVisualDrawingProperties33.Append(placeholderShape20);

            nonVisualShapeProperties25.Append(nonVisualDrawingProperties33);
            nonVisualShapeProperties25.Append(nonVisualShapeDrawingProperties25);
            nonVisualShapeProperties25.Append(applicationNonVisualDrawingProperties33);
            ShapeProperties shapeProperties28 = new ShapeProperties();

            TextBody textBody25 = new TextBody();
            A.BodyProperties bodyProperties25 = new A.BodyProperties();
            A.ListStyle listStyle25 = new A.ListStyle();

            A.Paragraph paragraph33 = new A.Paragraph();

            A.Field field8 = new A.Field(){ Id = "{335A3C46-0F61-4459-889B-E48E8905434F}", Type = "slidenum" };
            A.RunProperties runProperties29 = new A.RunProperties(){ Language = "en-GB", SmartTagClean = false };
            A.ParagraphProperties paragraphProperties27 = new A.ParagraphProperties();
            A.Text text29 = new A.Text();
            text29.Text = "‹#›";

            field8.Append(runProperties29);
            field8.Append(paragraphProperties27);
            field8.Append(text29);
            A.EndParagraphRunProperties endParagraphRunProperties21 = new A.EndParagraphRunProperties(){ Language = "en-GB" };

            paragraph33.Append(field8);
            paragraph33.Append(endParagraphRunProperties21);

            textBody25.Append(bodyProperties25);
            textBody25.Append(listStyle25);
            textBody25.Append(paragraph33);

            shape25.Append(nonVisualShapeProperties25);
            shape25.Append(shapeProperties28);
            shape25.Append(textBody25);

            shapeTree5.Append(nonVisualGroupShapeProperties5);
            shapeTree5.Append(groupShapeProperties5);
            shapeTree5.Append(shape21);
            shapeTree5.Append(shape22);
            shapeTree5.Append(shape23);
            shapeTree5.Append(shape24);
            shapeTree5.Append(shape25);

            commonSlideData5.Append(shapeTree5);

            ColorMapOverride colorMapOverride3 = new ColorMapOverride();
            A.MasterColorMapping masterColorMapping3 = new A.MasterColorMapping();

            colorMapOverride3.Append(masterColorMapping3);

            slideLayout3.Append(commonSlideData5);
            slideLayout3.Append(colorMapOverride3);

            slideLayoutPart3.SlideLayout = slideLayout3;
        }

        // Generates content of customXmlPart1.
        private void GenerateCustomXmlPart1Content(CustomXmlPart customXmlPart1)
        {
            System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(customXmlPart1.GetStream(System.IO.FileMode.Create), System.Text.Encoding.UTF8);
            writer.WriteRaw("<tns:customPropertyEditors xmlns:tns=\"http://schemas.microsoft.com/office/2006/customDocumentInformationPanel\"><tns:showOnOpen>true</tns:showOnOpen><tns:defaultPropertyEditorNamespace>Standard properties</tns:defaultPropertyEditorNamespace></tns:customPropertyEditors>");
            writer.Flush();
            writer.Close();
        }

        // Generates content of customXmlPropertiesPart1.
        private void GenerateCustomXmlPropertiesPart1Content(CustomXmlPropertiesPart customXmlPropertiesPart1)
        {
            Ds.DataStoreItem dataStoreItem1 = new Ds.DataStoreItem(){ ItemId = "{1058D893-C8BA-44FB-9AE9-195E2D99B7A8}" };
            dataStoreItem1.AddNamespaceDeclaration("ds", "http://schemas.openxmlformats.org/officeDocument/2006/customXml");

            Ds.SchemaReferences schemaReferences1 = new Ds.SchemaReferences();
            Ds.SchemaReference schemaReference1 = new Ds.SchemaReference(){ Uri = "http://schemas.microsoft.com/office/2006/customDocumentInformationPanel" };

            schemaReferences1.Append(schemaReference1);

            dataStoreItem1.Append(schemaReferences1);

            customXmlPropertiesPart1.DataStoreItem = dataStoreItem1;
        }

        // Generates content of notesMasterPart1.
        private void GenerateNotesMasterPart1Content(NotesMasterPart notesMasterPart1)
        {
            NotesMaster notesMaster1 = new NotesMaster();
            notesMaster1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            notesMaster1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            notesMaster1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            CommonSlideData commonSlideData6 = new CommonSlideData();

            Background background3 = new Background();

            BackgroundStyleReference backgroundStyleReference3 = new BackgroundStyleReference(){ Index = (UInt32Value)1001U };
            A.SchemeColor schemeColor109 = new A.SchemeColor(){ Val = A.SchemeColorValues.Background1 };

            backgroundStyleReference3.Append(schemeColor109);

            background3.Append(backgroundStyleReference3);

            ShapeTree shapeTree6 = new ShapeTree();

            NonVisualGroupShapeProperties nonVisualGroupShapeProperties6 = new NonVisualGroupShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties34 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
            NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties6 = new NonVisualGroupShapeDrawingProperties();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties34 = new ApplicationNonVisualDrawingProperties();

            nonVisualGroupShapeProperties6.Append(nonVisualDrawingProperties34);
            nonVisualGroupShapeProperties6.Append(nonVisualGroupShapeDrawingProperties6);
            nonVisualGroupShapeProperties6.Append(applicationNonVisualDrawingProperties34);

            GroupShapeProperties groupShapeProperties6 = new GroupShapeProperties();

            A.TransformGroup transformGroup6 = new A.TransformGroup();
            A.Offset offset31 = new A.Offset(){ X = 0L, Y = 0L };
            A.Extents extents31 = new A.Extents(){ Cx = 0L, Cy = 0L };
            A.ChildOffset childOffset6 = new A.ChildOffset(){ X = 0L, Y = 0L };
            A.ChildExtents childExtents6 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

            transformGroup6.Append(offset31);
            transformGroup6.Append(extents31);
            transformGroup6.Append(childOffset6);
            transformGroup6.Append(childExtents6);

            groupShapeProperties6.Append(transformGroup6);

            Shape shape26 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties26 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties35 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Platshållare för sidhuvud 1" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties26 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks21 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties26.Append(shapeLocks21);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties35 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape21 = new PlaceholderShape(){ Type = PlaceholderValues.Header, Size = PlaceholderSizeValues.Quarter };

            applicationNonVisualDrawingProperties35.Append(placeholderShape21);

            nonVisualShapeProperties26.Append(nonVisualDrawingProperties35);
            nonVisualShapeProperties26.Append(nonVisualShapeDrawingProperties26);
            nonVisualShapeProperties26.Append(applicationNonVisualDrawingProperties35);

            ShapeProperties shapeProperties29 = new ShapeProperties();

            A.Transform2D transform2D26 = new A.Transform2D();
            A.Offset offset32 = new A.Offset(){ X = 0L, Y = 0L };
            A.Extents extents32 = new A.Extents(){ Cx = 2946400L, Cy = 495300L };

            transform2D26.Append(offset32);
            transform2D26.Append(extents32);

            A.PresetGeometry presetGeometry19 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList19 = new A.AdjustValueList();

            presetGeometry19.Append(adjustValueList19);

            shapeProperties29.Append(transform2D26);
            shapeProperties29.Append(presetGeometry19);

            TextBody textBody26 = new TextBody();
            A.BodyProperties bodyProperties26 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false };

            A.ListStyle listStyle26 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties19 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Left };
            A.DefaultRunProperties defaultRunProperties74 = new A.DefaultRunProperties(){ FontSize = 1200 };

            level1ParagraphProperties19.Append(defaultRunProperties74);

            listStyle26.Append(level1ParagraphProperties19);

            A.Paragraph paragraph34 = new A.Paragraph();
            A.EndParagraphRunProperties endParagraphRunProperties22 = new A.EndParagraphRunProperties(){ Language = "sv-SE" };

            paragraph34.Append(endParagraphRunProperties22);

            textBody26.Append(bodyProperties26);
            textBody26.Append(listStyle26);
            textBody26.Append(paragraph34);

            shape26.Append(nonVisualShapeProperties26);
            shape26.Append(shapeProperties29);
            shape26.Append(textBody26);

            Shape shape27 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties27 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties36 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Platshållare för datum 2" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties27 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks22 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties27.Append(shapeLocks22);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties36 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape22 = new PlaceholderShape(){ Type = PlaceholderValues.DateAndTime, Index = (UInt32Value)1U };

            applicationNonVisualDrawingProperties36.Append(placeholderShape22);

            nonVisualShapeProperties27.Append(nonVisualDrawingProperties36);
            nonVisualShapeProperties27.Append(nonVisualShapeDrawingProperties27);
            nonVisualShapeProperties27.Append(applicationNonVisualDrawingProperties36);

            ShapeProperties shapeProperties30 = new ShapeProperties();

            A.Transform2D transform2D27 = new A.Transform2D();
            A.Offset offset33 = new A.Offset(){ X = 3849688L, Y = 0L };
            A.Extents extents33 = new A.Extents(){ Cx = 2946400L, Cy = 495300L };

            transform2D27.Append(offset33);
            transform2D27.Append(extents33);

            A.PresetGeometry presetGeometry20 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList20 = new A.AdjustValueList();

            presetGeometry20.Append(adjustValueList20);

            shapeProperties30.Append(transform2D27);
            shapeProperties30.Append(presetGeometry20);

            TextBody textBody27 = new TextBody();
            A.BodyProperties bodyProperties27 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false };

            A.ListStyle listStyle27 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties20 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Right };
            A.DefaultRunProperties defaultRunProperties75 = new A.DefaultRunProperties(){ FontSize = 1200 };

            level1ParagraphProperties20.Append(defaultRunProperties75);

            listStyle27.Append(level1ParagraphProperties20);

            A.Paragraph paragraph35 = new A.Paragraph();

            A.Field field9 = new A.Field(){ Id = "{36E6A815-89B9-B94A-8051-92DBFC9E49C2}", Type = "datetimeFigureOut" };
            A.RunProperties runProperties30 = new A.RunProperties(){ Language = "sv-SE", SmartTagClean = false };
            A.ParagraphProperties paragraphProperties28 = new A.ParagraphProperties();
            A.Text text30 = new A.Text();
            text30.Text = "2016-10-21";

            field9.Append(runProperties30);
            field9.Append(paragraphProperties28);
            field9.Append(text30);
            A.EndParagraphRunProperties endParagraphRunProperties23 = new A.EndParagraphRunProperties(){ Language = "sv-SE" };

            paragraph35.Append(field9);
            paragraph35.Append(endParagraphRunProperties23);

            textBody27.Append(bodyProperties27);
            textBody27.Append(listStyle27);
            textBody27.Append(paragraph35);

            shape27.Append(nonVisualShapeProperties27);
            shape27.Append(shapeProperties30);
            shape27.Append(textBody27);

            Shape shape28 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties28 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties37 = new NonVisualDrawingProperties(){ Id = (UInt32Value)4U, Name = "Platshållare för bildobjekt 3" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties28 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks23 = new A.ShapeLocks(){ NoGrouping = true, NoRotation = true, NoChangeAspect = true };

            nonVisualShapeDrawingProperties28.Append(shapeLocks23);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties37 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape23 = new PlaceholderShape(){ Type = PlaceholderValues.SlideImage, Index = (UInt32Value)2U };

            applicationNonVisualDrawingProperties37.Append(placeholderShape23);

            nonVisualShapeProperties28.Append(nonVisualDrawingProperties37);
            nonVisualShapeProperties28.Append(nonVisualShapeDrawingProperties28);
            nonVisualShapeProperties28.Append(applicationNonVisualDrawingProperties37);

            ShapeProperties shapeProperties31 = new ShapeProperties();

            A.Transform2D transform2D28 = new A.Transform2D();
            A.Offset offset34 = new A.Offset(){ X = 1177925L, Y = 1235075L };
            A.Extents extents34 = new A.Extents(){ Cx = 4441825L, Cy = 3332163L };

            transform2D28.Append(offset34);
            transform2D28.Append(extents34);

            A.PresetGeometry presetGeometry21 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList21 = new A.AdjustValueList();

            presetGeometry21.Append(adjustValueList21);
            A.NoFill noFill9 = new A.NoFill();

            A.Outline outline10 = new A.Outline(){ Width = 12700 };

            A.SolidFill solidFill90 = new A.SolidFill();
            A.PresetColor presetColor1 = new A.PresetColor(){ Val = A.PresetColorValues.Black };

            solidFill90.Append(presetColor1);

            outline10.Append(solidFill90);

            shapeProperties31.Append(transform2D28);
            shapeProperties31.Append(presetGeometry21);
            shapeProperties31.Append(noFill9);
            shapeProperties31.Append(outline10);

            TextBody textBody28 = new TextBody();
            A.BodyProperties bodyProperties28 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Center };
            A.ListStyle listStyle28 = new A.ListStyle();

            A.Paragraph paragraph36 = new A.Paragraph();
            A.EndParagraphRunProperties endParagraphRunProperties24 = new A.EndParagraphRunProperties(){ Language = "sv-SE" };

            paragraph36.Append(endParagraphRunProperties24);

            textBody28.Append(bodyProperties28);
            textBody28.Append(listStyle28);
            textBody28.Append(paragraph36);

            shape28.Append(nonVisualShapeProperties28);
            shape28.Append(shapeProperties31);
            shape28.Append(textBody28);

            Shape shape29 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties29 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties38 = new NonVisualDrawingProperties(){ Id = (UInt32Value)5U, Name = "Platshållare för anteckningar 4" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties29 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks24 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties29.Append(shapeLocks24);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties38 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape24 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)3U };

            applicationNonVisualDrawingProperties38.Append(placeholderShape24);

            nonVisualShapeProperties29.Append(nonVisualDrawingProperties38);
            nonVisualShapeProperties29.Append(nonVisualShapeDrawingProperties29);
            nonVisualShapeProperties29.Append(applicationNonVisualDrawingProperties38);

            ShapeProperties shapeProperties32 = new ShapeProperties();

            A.Transform2D transform2D29 = new A.Transform2D();
            A.Offset offset35 = new A.Offset(){ X = 679450L, Y = 4751388L };
            A.Extents extents35 = new A.Extents(){ Cx = 5438775L, Cy = 3889375L };

            transform2D29.Append(offset35);
            transform2D29.Append(extents35);

            A.PresetGeometry presetGeometry22 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList22 = new A.AdjustValueList();

            presetGeometry22.Append(adjustValueList22);

            shapeProperties32.Append(transform2D29);
            shapeProperties32.Append(presetGeometry22);

            TextBody textBody29 = new TextBody();
            A.BodyProperties bodyProperties29 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false };
            A.ListStyle listStyle29 = new A.ListStyle();

            A.Paragraph paragraph37 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties29 = new A.ParagraphProperties(){ Level = 0 };

            A.Run run22 = new A.Run();
            A.RunProperties runProperties31 = new A.RunProperties(){ Language = "sv-SE", SmartTagClean = false };
            A.Text text31 = new A.Text();
            text31.Text = "Klicka här för att ändra format på bakgrundstexten";

            run22.Append(runProperties31);
            run22.Append(text31);

            paragraph37.Append(paragraphProperties29);
            paragraph37.Append(run22);

            A.Paragraph paragraph38 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties30 = new A.ParagraphProperties(){ Level = 1 };

            A.Run run23 = new A.Run();
            A.RunProperties runProperties32 = new A.RunProperties(){ Language = "sv-SE", SmartTagClean = false };
            A.Text text32 = new A.Text();
            text32.Text = "Nivå två";

            run23.Append(runProperties32);
            run23.Append(text32);

            paragraph38.Append(paragraphProperties30);
            paragraph38.Append(run23);

            A.Paragraph paragraph39 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties31 = new A.ParagraphProperties(){ Level = 2 };

            A.Run run24 = new A.Run();
            A.RunProperties runProperties33 = new A.RunProperties(){ Language = "sv-SE", SmartTagClean = false };
            A.Text text33 = new A.Text();
            text33.Text = "Nivå tre";

            run24.Append(runProperties33);
            run24.Append(text33);

            paragraph39.Append(paragraphProperties31);
            paragraph39.Append(run24);

            A.Paragraph paragraph40 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties32 = new A.ParagraphProperties(){ Level = 3 };

            A.Run run25 = new A.Run();
            A.RunProperties runProperties34 = new A.RunProperties(){ Language = "sv-SE", SmartTagClean = false };
            A.Text text34 = new A.Text();
            text34.Text = "Nivå fyra";

            run25.Append(runProperties34);
            run25.Append(text34);

            paragraph40.Append(paragraphProperties32);
            paragraph40.Append(run25);

            A.Paragraph paragraph41 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties33 = new A.ParagraphProperties(){ Level = 4 };

            A.Run run26 = new A.Run();
            A.RunProperties runProperties35 = new A.RunProperties(){ Language = "sv-SE", SmartTagClean = false };
            A.Text text35 = new A.Text();
            text35.Text = "Nivå fem";

            run26.Append(runProperties35);
            run26.Append(text35);
            A.EndParagraphRunProperties endParagraphRunProperties25 = new A.EndParagraphRunProperties(){ Language = "sv-SE" };

            paragraph41.Append(paragraphProperties33);
            paragraph41.Append(run26);
            paragraph41.Append(endParagraphRunProperties25);

            textBody29.Append(bodyProperties29);
            textBody29.Append(listStyle29);
            textBody29.Append(paragraph37);
            textBody29.Append(paragraph38);
            textBody29.Append(paragraph39);
            textBody29.Append(paragraph40);
            textBody29.Append(paragraph41);

            shape29.Append(nonVisualShapeProperties29);
            shape29.Append(shapeProperties32);
            shape29.Append(textBody29);

            Shape shape30 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties30 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties39 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Platshållare för sidfot 5" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties30 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks25 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties30.Append(shapeLocks25);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties39 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape25 = new PlaceholderShape(){ Type = PlaceholderValues.Footer, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)4U };

            applicationNonVisualDrawingProperties39.Append(placeholderShape25);

            nonVisualShapeProperties30.Append(nonVisualDrawingProperties39);
            nonVisualShapeProperties30.Append(nonVisualShapeDrawingProperties30);
            nonVisualShapeProperties30.Append(applicationNonVisualDrawingProperties39);

            ShapeProperties shapeProperties33 = new ShapeProperties();

            A.Transform2D transform2D30 = new A.Transform2D();
            A.Offset offset36 = new A.Offset(){ X = 0L, Y = 9378950L };
            A.Extents extents36 = new A.Extents(){ Cx = 2946400L, Cy = 495300L };

            transform2D30.Append(offset36);
            transform2D30.Append(extents36);

            A.PresetGeometry presetGeometry23 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList23 = new A.AdjustValueList();

            presetGeometry23.Append(adjustValueList23);

            shapeProperties33.Append(transform2D30);
            shapeProperties33.Append(presetGeometry23);

            TextBody textBody30 = new TextBody();
            A.BodyProperties bodyProperties30 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Bottom };

            A.ListStyle listStyle30 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties21 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Left };
            A.DefaultRunProperties defaultRunProperties76 = new A.DefaultRunProperties(){ FontSize = 1200 };

            level1ParagraphProperties21.Append(defaultRunProperties76);

            listStyle30.Append(level1ParagraphProperties21);

            A.Paragraph paragraph42 = new A.Paragraph();
            A.EndParagraphRunProperties endParagraphRunProperties26 = new A.EndParagraphRunProperties(){ Language = "sv-SE" };

            paragraph42.Append(endParagraphRunProperties26);

            textBody30.Append(bodyProperties30);
            textBody30.Append(listStyle30);
            textBody30.Append(paragraph42);

            shape30.Append(nonVisualShapeProperties30);
            shape30.Append(shapeProperties33);
            shape30.Append(textBody30);

            Shape shape31 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties31 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties40 = new NonVisualDrawingProperties(){ Id = (UInt32Value)7U, Name = "Platshållare för bildnummer 6" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties31 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks26 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties31.Append(shapeLocks26);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties40 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape26 = new PlaceholderShape(){ Type = PlaceholderValues.SlideNumber, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)5U };

            applicationNonVisualDrawingProperties40.Append(placeholderShape26);

            nonVisualShapeProperties31.Append(nonVisualDrawingProperties40);
            nonVisualShapeProperties31.Append(nonVisualShapeDrawingProperties31);
            nonVisualShapeProperties31.Append(applicationNonVisualDrawingProperties40);

            ShapeProperties shapeProperties34 = new ShapeProperties();

            A.Transform2D transform2D31 = new A.Transform2D();
            A.Offset offset37 = new A.Offset(){ X = 3849688L, Y = 9378950L };
            A.Extents extents37 = new A.Extents(){ Cx = 2946400L, Cy = 495300L };

            transform2D31.Append(offset37);
            transform2D31.Append(extents37);

            A.PresetGeometry presetGeometry24 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList24 = new A.AdjustValueList();

            presetGeometry24.Append(adjustValueList24);

            shapeProperties34.Append(transform2D31);
            shapeProperties34.Append(presetGeometry24);

            TextBody textBody31 = new TextBody();
            A.BodyProperties bodyProperties31 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Bottom };

            A.ListStyle listStyle31 = new A.ListStyle();

            A.Level1ParagraphProperties level1ParagraphProperties22 = new A.Level1ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Right };
            A.DefaultRunProperties defaultRunProperties77 = new A.DefaultRunProperties(){ FontSize = 1200 };

            level1ParagraphProperties22.Append(defaultRunProperties77);

            listStyle31.Append(level1ParagraphProperties22);

            A.Paragraph paragraph43 = new A.Paragraph();

            A.Field field10 = new A.Field(){ Id = "{5C5CF5D1-AEF7-4349-B983-1F5C3276214A}", Type = "slidenum" };
            A.RunProperties runProperties36 = new A.RunProperties(){ Language = "sv-SE", SmartTagClean = false };
            A.ParagraphProperties paragraphProperties34 = new A.ParagraphProperties();
            A.Text text36 = new A.Text();
            text36.Text = "‹#›";

            field10.Append(runProperties36);
            field10.Append(paragraphProperties34);
            field10.Append(text36);
            A.EndParagraphRunProperties endParagraphRunProperties27 = new A.EndParagraphRunProperties(){ Language = "sv-SE" };

            paragraph43.Append(field10);
            paragraph43.Append(endParagraphRunProperties27);

            textBody31.Append(bodyProperties31);
            textBody31.Append(listStyle31);
            textBody31.Append(paragraph43);

            shape31.Append(nonVisualShapeProperties31);
            shape31.Append(shapeProperties34);
            shape31.Append(textBody31);

            shapeTree6.Append(nonVisualGroupShapeProperties6);
            shapeTree6.Append(groupShapeProperties6);
            shapeTree6.Append(shape26);
            shapeTree6.Append(shape27);
            shapeTree6.Append(shape28);
            shapeTree6.Append(shape29);
            shapeTree6.Append(shape30);
            shapeTree6.Append(shape31);

            CommonSlideDataExtensionList commonSlideDataExtensionList4 = new CommonSlideDataExtensionList();

            CommonSlideDataExtension commonSlideDataExtension4 = new CommonSlideDataExtension(){ Uri = "{BB962C8B-B14F-4D97-AF65-F5344CB8AC3E}" };

            P14.CreationId creationId4 = new P14.CreationId(){ Val = (UInt32Value)1898320203U };
            creationId4.AddNamespaceDeclaration("p14", "http://schemas.microsoft.com/office/powerpoint/2010/main");

            commonSlideDataExtension4.Append(creationId4);

            commonSlideDataExtensionList4.Append(commonSlideDataExtension4);

            commonSlideData6.Append(background3);
            commonSlideData6.Append(shapeTree6);
            commonSlideData6.Append(commonSlideDataExtensionList4);
            ColorMap colorMap3 = new ColorMap(){ Background1 = A.ColorSchemeIndexValues.Light1, Text1 = A.ColorSchemeIndexValues.Dark1, Background2 = A.ColorSchemeIndexValues.Light2, Text2 = A.ColorSchemeIndexValues.Dark2, Accent1 = A.ColorSchemeIndexValues.Accent1, Accent2 = A.ColorSchemeIndexValues.Accent2, Accent3 = A.ColorSchemeIndexValues.Accent3, Accent4 = A.ColorSchemeIndexValues.Accent4, Accent5 = A.ColorSchemeIndexValues.Accent5, Accent6 = A.ColorSchemeIndexValues.Accent6, Hyperlink = A.ColorSchemeIndexValues.Hyperlink, FollowedHyperlink = A.ColorSchemeIndexValues.FollowedHyperlink };

            NotesStyle notesStyle1 = new NotesStyle();

            A.Level1ParagraphProperties level1ParagraphProperties23 = new A.Level1ParagraphProperties(){ LeftMargin = 0, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties78 = new A.DefaultRunProperties(){ FontSize = 1200, Kerning = 1200 };

            A.SolidFill solidFill91 = new A.SolidFill();
            A.SchemeColor schemeColor110 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill91.Append(schemeColor110);
            A.LatinFont latinFont61 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont52 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont61 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties78.Append(solidFill91);
            defaultRunProperties78.Append(latinFont61);
            defaultRunProperties78.Append(eastAsianFont52);
            defaultRunProperties78.Append(complexScriptFont61);

            level1ParagraphProperties23.Append(defaultRunProperties78);

            A.Level2ParagraphProperties level2ParagraphProperties8 = new A.Level2ParagraphProperties(){ LeftMargin = 457200, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties79 = new A.DefaultRunProperties(){ FontSize = 1200, Kerning = 1200 };

            A.SolidFill solidFill92 = new A.SolidFill();
            A.SchemeColor schemeColor111 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill92.Append(schemeColor111);
            A.LatinFont latinFont62 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont53 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont62 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties79.Append(solidFill92);
            defaultRunProperties79.Append(latinFont62);
            defaultRunProperties79.Append(eastAsianFont53);
            defaultRunProperties79.Append(complexScriptFont62);

            level2ParagraphProperties8.Append(defaultRunProperties79);

            A.Level3ParagraphProperties level3ParagraphProperties8 = new A.Level3ParagraphProperties(){ LeftMargin = 914400, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties80 = new A.DefaultRunProperties(){ FontSize = 1200, Kerning = 1200 };

            A.SolidFill solidFill93 = new A.SolidFill();
            A.SchemeColor schemeColor112 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill93.Append(schemeColor112);
            A.LatinFont latinFont63 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont54 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont63 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties80.Append(solidFill93);
            defaultRunProperties80.Append(latinFont63);
            defaultRunProperties80.Append(eastAsianFont54);
            defaultRunProperties80.Append(complexScriptFont63);

            level3ParagraphProperties8.Append(defaultRunProperties80);

            A.Level4ParagraphProperties level4ParagraphProperties8 = new A.Level4ParagraphProperties(){ LeftMargin = 1371600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties81 = new A.DefaultRunProperties(){ FontSize = 1200, Kerning = 1200 };

            A.SolidFill solidFill94 = new A.SolidFill();
            A.SchemeColor schemeColor113 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill94.Append(schemeColor113);
            A.LatinFont latinFont64 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont55 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont64 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties81.Append(solidFill94);
            defaultRunProperties81.Append(latinFont64);
            defaultRunProperties81.Append(eastAsianFont55);
            defaultRunProperties81.Append(complexScriptFont64);

            level4ParagraphProperties8.Append(defaultRunProperties81);

            A.Level5ParagraphProperties level5ParagraphProperties8 = new A.Level5ParagraphProperties(){ LeftMargin = 1828800, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties82 = new A.DefaultRunProperties(){ FontSize = 1200, Kerning = 1200 };

            A.SolidFill solidFill95 = new A.SolidFill();
            A.SchemeColor schemeColor114 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill95.Append(schemeColor114);
            A.LatinFont latinFont65 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont56 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont65 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties82.Append(solidFill95);
            defaultRunProperties82.Append(latinFont65);
            defaultRunProperties82.Append(eastAsianFont56);
            defaultRunProperties82.Append(complexScriptFont65);

            level5ParagraphProperties8.Append(defaultRunProperties82);

            A.Level6ParagraphProperties level6ParagraphProperties7 = new A.Level6ParagraphProperties(){ LeftMargin = 2286000, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties83 = new A.DefaultRunProperties(){ FontSize = 1200, Kerning = 1200 };

            A.SolidFill solidFill96 = new A.SolidFill();
            A.SchemeColor schemeColor115 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill96.Append(schemeColor115);
            A.LatinFont latinFont66 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont57 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont66 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties83.Append(solidFill96);
            defaultRunProperties83.Append(latinFont66);
            defaultRunProperties83.Append(eastAsianFont57);
            defaultRunProperties83.Append(complexScriptFont66);

            level6ParagraphProperties7.Append(defaultRunProperties83);

            A.Level7ParagraphProperties level7ParagraphProperties7 = new A.Level7ParagraphProperties(){ LeftMargin = 2743200, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties84 = new A.DefaultRunProperties(){ FontSize = 1200, Kerning = 1200 };

            A.SolidFill solidFill97 = new A.SolidFill();
            A.SchemeColor schemeColor116 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill97.Append(schemeColor116);
            A.LatinFont latinFont67 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont58 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont67 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties84.Append(solidFill97);
            defaultRunProperties84.Append(latinFont67);
            defaultRunProperties84.Append(eastAsianFont58);
            defaultRunProperties84.Append(complexScriptFont67);

            level7ParagraphProperties7.Append(defaultRunProperties84);

            A.Level8ParagraphProperties level8ParagraphProperties7 = new A.Level8ParagraphProperties(){ LeftMargin = 3200400, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties85 = new A.DefaultRunProperties(){ FontSize = 1200, Kerning = 1200 };

            A.SolidFill solidFill98 = new A.SolidFill();
            A.SchemeColor schemeColor117 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill98.Append(schemeColor117);
            A.LatinFont latinFont68 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont59 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont68 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties85.Append(solidFill98);
            defaultRunProperties85.Append(latinFont68);
            defaultRunProperties85.Append(eastAsianFont59);
            defaultRunProperties85.Append(complexScriptFont68);

            level8ParagraphProperties7.Append(defaultRunProperties85);

            A.Level9ParagraphProperties level9ParagraphProperties7 = new A.Level9ParagraphProperties(){ LeftMargin = 3657600, Alignment = A.TextAlignmentTypeValues.Left, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, LatinLineBreak = false, Height = true };

            A.DefaultRunProperties defaultRunProperties86 = new A.DefaultRunProperties(){ FontSize = 1200, Kerning = 1200 };

            A.SolidFill solidFill99 = new A.SolidFill();
            A.SchemeColor schemeColor118 = new A.SchemeColor(){ Val = A.SchemeColorValues.Text1 };

            solidFill99.Append(schemeColor118);
            A.LatinFont latinFont69 = new A.LatinFont(){ Typeface = "+mn-lt" };
            A.EastAsianFont eastAsianFont60 = new A.EastAsianFont(){ Typeface = "+mn-ea" };
            A.ComplexScriptFont complexScriptFont69 = new A.ComplexScriptFont(){ Typeface = "+mn-cs" };

            defaultRunProperties86.Append(solidFill99);
            defaultRunProperties86.Append(latinFont69);
            defaultRunProperties86.Append(eastAsianFont60);
            defaultRunProperties86.Append(complexScriptFont69);

            level9ParagraphProperties7.Append(defaultRunProperties86);

            notesStyle1.Append(level1ParagraphProperties23);
            notesStyle1.Append(level2ParagraphProperties8);
            notesStyle1.Append(level3ParagraphProperties8);
            notesStyle1.Append(level4ParagraphProperties8);
            notesStyle1.Append(level5ParagraphProperties8);
            notesStyle1.Append(level6ParagraphProperties7);
            notesStyle1.Append(level7ParagraphProperties7);
            notesStyle1.Append(level8ParagraphProperties7);
            notesStyle1.Append(level9ParagraphProperties7);

            notesMaster1.Append(commonSlideData6);
            notesMaster1.Append(colorMap3);
            notesMaster1.Append(notesStyle1);

            notesMasterPart1.NotesMaster = notesMaster1;
        }

        // Generates content of themePart3.
        private void GenerateThemePart3Content(ThemePart themePart3)
        {
            A.Theme theme3 = new A.Theme(){ Name = "Office-tema" };
            theme3.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            A.ThemeElements themeElements3 = new A.ThemeElements();

            A.ColorScheme colorScheme3 = new A.ColorScheme(){ Name = "Office" };

            A.Dark1Color dark1Color3 = new A.Dark1Color();
            A.SystemColor systemColor5 = new A.SystemColor(){ Val = A.SystemColorValues.WindowText, LastColor = "000000" };

            dark1Color3.Append(systemColor5);

            A.Light1Color light1Color3 = new A.Light1Color();
            A.SystemColor systemColor6 = new A.SystemColor(){ Val = A.SystemColorValues.Window, LastColor = "FFFFFF" };

            light1Color3.Append(systemColor6);

            A.Dark2Color dark2Color3 = new A.Dark2Color();
            A.RgbColorModelHex rgbColorModelHex50 = new A.RgbColorModelHex(){ Val = "44546A" };

            dark2Color3.Append(rgbColorModelHex50);

            A.Light2Color light2Color3 = new A.Light2Color();
            A.RgbColorModelHex rgbColorModelHex51 = new A.RgbColorModelHex(){ Val = "E7E6E6" };

            light2Color3.Append(rgbColorModelHex51);

            A.Accent1Color accent1Color3 = new A.Accent1Color();
            A.RgbColorModelHex rgbColorModelHex52 = new A.RgbColorModelHex(){ Val = "5B9BD5" };

            accent1Color3.Append(rgbColorModelHex52);

            A.Accent2Color accent2Color3 = new A.Accent2Color();
            A.RgbColorModelHex rgbColorModelHex53 = new A.RgbColorModelHex(){ Val = "ED7D31" };

            accent2Color3.Append(rgbColorModelHex53);

            A.Accent3Color accent3Color3 = new A.Accent3Color();
            A.RgbColorModelHex rgbColorModelHex54 = new A.RgbColorModelHex(){ Val = "A5A5A5" };

            accent3Color3.Append(rgbColorModelHex54);

            A.Accent4Color accent4Color3 = new A.Accent4Color();
            A.RgbColorModelHex rgbColorModelHex55 = new A.RgbColorModelHex(){ Val = "FFC000" };

            accent4Color3.Append(rgbColorModelHex55);

            A.Accent5Color accent5Color3 = new A.Accent5Color();
            A.RgbColorModelHex rgbColorModelHex56 = new A.RgbColorModelHex(){ Val = "4472C4" };

            accent5Color3.Append(rgbColorModelHex56);

            A.Accent6Color accent6Color3 = new A.Accent6Color();
            A.RgbColorModelHex rgbColorModelHex57 = new A.RgbColorModelHex(){ Val = "70AD47" };

            accent6Color3.Append(rgbColorModelHex57);

            A.Hyperlink hyperlink3 = new A.Hyperlink();
            A.RgbColorModelHex rgbColorModelHex58 = new A.RgbColorModelHex(){ Val = "0563C1" };

            hyperlink3.Append(rgbColorModelHex58);

            A.FollowedHyperlinkColor followedHyperlinkColor3 = new A.FollowedHyperlinkColor();
            A.RgbColorModelHex rgbColorModelHex59 = new A.RgbColorModelHex(){ Val = "954F72" };

            followedHyperlinkColor3.Append(rgbColorModelHex59);

            colorScheme3.Append(dark1Color3);
            colorScheme3.Append(light1Color3);
            colorScheme3.Append(dark2Color3);
            colorScheme3.Append(light2Color3);
            colorScheme3.Append(accent1Color3);
            colorScheme3.Append(accent2Color3);
            colorScheme3.Append(accent3Color3);
            colorScheme3.Append(accent4Color3);
            colorScheme3.Append(accent5Color3);
            colorScheme3.Append(accent6Color3);
            colorScheme3.Append(hyperlink3);
            colorScheme3.Append(followedHyperlinkColor3);

            A.FontScheme fontScheme3 = new A.FontScheme(){ Name = "Office" };

            A.MajorFont majorFont3 = new A.MajorFont();
            A.LatinFont latinFont70 = new A.LatinFont(){ Typeface = "Calibri Light" };
            A.EastAsianFont eastAsianFont61 = new A.EastAsianFont(){ Typeface = "" };
            A.ComplexScriptFont complexScriptFont70 = new A.ComplexScriptFont(){ Typeface = "" };
            A.SupplementalFont supplementalFont119 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "Yu Gothic Light" };
            A.SupplementalFont supplementalFont120 = new A.SupplementalFont(){ Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont121 = new A.SupplementalFont(){ Script = "Hans", Typeface = "DengXian Light" };
            A.SupplementalFont supplementalFont122 = new A.SupplementalFont(){ Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont123 = new A.SupplementalFont(){ Script = "Arab", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont124 = new A.SupplementalFont(){ Script = "Hebr", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont125 = new A.SupplementalFont(){ Script = "Thai", Typeface = "Angsana New" };
            A.SupplementalFont supplementalFont126 = new A.SupplementalFont(){ Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont127 = new A.SupplementalFont(){ Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont128 = new A.SupplementalFont(){ Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont129 = new A.SupplementalFont(){ Script = "Khmr", Typeface = "MoolBoran" };
            A.SupplementalFont supplementalFont130 = new A.SupplementalFont(){ Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont131 = new A.SupplementalFont(){ Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont132 = new A.SupplementalFont(){ Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont133 = new A.SupplementalFont(){ Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont134 = new A.SupplementalFont(){ Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont135 = new A.SupplementalFont(){ Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont136 = new A.SupplementalFont(){ Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont137 = new A.SupplementalFont(){ Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont138 = new A.SupplementalFont(){ Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont139 = new A.SupplementalFont(){ Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont140 = new A.SupplementalFont(){ Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont141 = new A.SupplementalFont(){ Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont142 = new A.SupplementalFont(){ Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont143 = new A.SupplementalFont(){ Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont144 = new A.SupplementalFont(){ Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont145 = new A.SupplementalFont(){ Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont146 = new A.SupplementalFont(){ Script = "Viet", Typeface = "Times New Roman" };
            A.SupplementalFont supplementalFont147 = new A.SupplementalFont(){ Script = "Uigh", Typeface = "Microsoft Uighur" };
            A.SupplementalFont supplementalFont148 = new A.SupplementalFont(){ Script = "Geor", Typeface = "Sylfaen" };

            majorFont3.Append(latinFont70);
            majorFont3.Append(eastAsianFont61);
            majorFont3.Append(complexScriptFont70);
            majorFont3.Append(supplementalFont119);
            majorFont3.Append(supplementalFont120);
            majorFont3.Append(supplementalFont121);
            majorFont3.Append(supplementalFont122);
            majorFont3.Append(supplementalFont123);
            majorFont3.Append(supplementalFont124);
            majorFont3.Append(supplementalFont125);
            majorFont3.Append(supplementalFont126);
            majorFont3.Append(supplementalFont127);
            majorFont3.Append(supplementalFont128);
            majorFont3.Append(supplementalFont129);
            majorFont3.Append(supplementalFont130);
            majorFont3.Append(supplementalFont131);
            majorFont3.Append(supplementalFont132);
            majorFont3.Append(supplementalFont133);
            majorFont3.Append(supplementalFont134);
            majorFont3.Append(supplementalFont135);
            majorFont3.Append(supplementalFont136);
            majorFont3.Append(supplementalFont137);
            majorFont3.Append(supplementalFont138);
            majorFont3.Append(supplementalFont139);
            majorFont3.Append(supplementalFont140);
            majorFont3.Append(supplementalFont141);
            majorFont3.Append(supplementalFont142);
            majorFont3.Append(supplementalFont143);
            majorFont3.Append(supplementalFont144);
            majorFont3.Append(supplementalFont145);
            majorFont3.Append(supplementalFont146);
            majorFont3.Append(supplementalFont147);
            majorFont3.Append(supplementalFont148);

            A.MinorFont minorFont3 = new A.MinorFont();
            A.LatinFont latinFont71 = new A.LatinFont(){ Typeface = "Calibri" };
            A.EastAsianFont eastAsianFont62 = new A.EastAsianFont(){ Typeface = "" };
            A.ComplexScriptFont complexScriptFont71 = new A.ComplexScriptFont(){ Typeface = "" };
            A.SupplementalFont supplementalFont149 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "Yu Gothic" };
            A.SupplementalFont supplementalFont150 = new A.SupplementalFont(){ Script = "Hang", Typeface = "맑은 고딕" };
            A.SupplementalFont supplementalFont151 = new A.SupplementalFont(){ Script = "Hans", Typeface = "DengXian" };
            A.SupplementalFont supplementalFont152 = new A.SupplementalFont(){ Script = "Hant", Typeface = "新細明體" };
            A.SupplementalFont supplementalFont153 = new A.SupplementalFont(){ Script = "Arab", Typeface = "Arial" };
            A.SupplementalFont supplementalFont154 = new A.SupplementalFont(){ Script = "Hebr", Typeface = "Arial" };
            A.SupplementalFont supplementalFont155 = new A.SupplementalFont(){ Script = "Thai", Typeface = "Cordia New" };
            A.SupplementalFont supplementalFont156 = new A.SupplementalFont(){ Script = "Ethi", Typeface = "Nyala" };
            A.SupplementalFont supplementalFont157 = new A.SupplementalFont(){ Script = "Beng", Typeface = "Vrinda" };
            A.SupplementalFont supplementalFont158 = new A.SupplementalFont(){ Script = "Gujr", Typeface = "Shruti" };
            A.SupplementalFont supplementalFont159 = new A.SupplementalFont(){ Script = "Khmr", Typeface = "DaunPenh" };
            A.SupplementalFont supplementalFont160 = new A.SupplementalFont(){ Script = "Knda", Typeface = "Tunga" };
            A.SupplementalFont supplementalFont161 = new A.SupplementalFont(){ Script = "Guru", Typeface = "Raavi" };
            A.SupplementalFont supplementalFont162 = new A.SupplementalFont(){ Script = "Cans", Typeface = "Euphemia" };
            A.SupplementalFont supplementalFont163 = new A.SupplementalFont(){ Script = "Cher", Typeface = "Plantagenet Cherokee" };
            A.SupplementalFont supplementalFont164 = new A.SupplementalFont(){ Script = "Yiii", Typeface = "Microsoft Yi Baiti" };
            A.SupplementalFont supplementalFont165 = new A.SupplementalFont(){ Script = "Tibt", Typeface = "Microsoft Himalaya" };
            A.SupplementalFont supplementalFont166 = new A.SupplementalFont(){ Script = "Thaa", Typeface = "MV Boli" };
            A.SupplementalFont supplementalFont167 = new A.SupplementalFont(){ Script = "Deva", Typeface = "Mangal" };
            A.SupplementalFont supplementalFont168 = new A.SupplementalFont(){ Script = "Telu", Typeface = "Gautami" };
            A.SupplementalFont supplementalFont169 = new A.SupplementalFont(){ Script = "Taml", Typeface = "Latha" };
            A.SupplementalFont supplementalFont170 = new A.SupplementalFont(){ Script = "Syrc", Typeface = "Estrangelo Edessa" };
            A.SupplementalFont supplementalFont171 = new A.SupplementalFont(){ Script = "Orya", Typeface = "Kalinga" };
            A.SupplementalFont supplementalFont172 = new A.SupplementalFont(){ Script = "Mlym", Typeface = "Kartika" };
            A.SupplementalFont supplementalFont173 = new A.SupplementalFont(){ Script = "Laoo", Typeface = "DokChampa" };
            A.SupplementalFont supplementalFont174 = new A.SupplementalFont(){ Script = "Sinh", Typeface = "Iskoola Pota" };
            A.SupplementalFont supplementalFont175 = new A.SupplementalFont(){ Script = "Mong", Typeface = "Mongolian Baiti" };
            A.SupplementalFont supplementalFont176 = new A.SupplementalFont(){ Script = "Viet", Typeface = "Arial" };
            A.SupplementalFont supplementalFont177 = new A.SupplementalFont(){ Script = "Uigh", Typeface = "Microsoft Uighur" };
            A.SupplementalFont supplementalFont178 = new A.SupplementalFont(){ Script = "Geor", Typeface = "Sylfaen" };

            minorFont3.Append(latinFont71);
            minorFont3.Append(eastAsianFont62);
            minorFont3.Append(complexScriptFont71);
            minorFont3.Append(supplementalFont149);
            minorFont3.Append(supplementalFont150);
            minorFont3.Append(supplementalFont151);
            minorFont3.Append(supplementalFont152);
            minorFont3.Append(supplementalFont153);
            minorFont3.Append(supplementalFont154);
            minorFont3.Append(supplementalFont155);
            minorFont3.Append(supplementalFont156);
            minorFont3.Append(supplementalFont157);
            minorFont3.Append(supplementalFont158);
            minorFont3.Append(supplementalFont159);
            minorFont3.Append(supplementalFont160);
            minorFont3.Append(supplementalFont161);
            minorFont3.Append(supplementalFont162);
            minorFont3.Append(supplementalFont163);
            minorFont3.Append(supplementalFont164);
            minorFont3.Append(supplementalFont165);
            minorFont3.Append(supplementalFont166);
            minorFont3.Append(supplementalFont167);
            minorFont3.Append(supplementalFont168);
            minorFont3.Append(supplementalFont169);
            minorFont3.Append(supplementalFont170);
            minorFont3.Append(supplementalFont171);
            minorFont3.Append(supplementalFont172);
            minorFont3.Append(supplementalFont173);
            minorFont3.Append(supplementalFont174);
            minorFont3.Append(supplementalFont175);
            minorFont3.Append(supplementalFont176);
            minorFont3.Append(supplementalFont177);
            minorFont3.Append(supplementalFont178);

            fontScheme3.Append(majorFont3);
            fontScheme3.Append(minorFont3);

            A.FormatScheme formatScheme3 = new A.FormatScheme(){ Name = "Office" };

            A.FillStyleList fillStyleList3 = new A.FillStyleList();

            A.SolidFill solidFill100 = new A.SolidFill();
            A.SchemeColor schemeColor119 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill100.Append(schemeColor119);

            A.GradientFill gradientFill9 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList9 = new A.GradientStopList();

            A.GradientStop gradientStop23 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor120 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.LuminanceModulation luminanceModulation1 = new A.LuminanceModulation(){ Val = 110000 };
            A.SaturationModulation saturationModulation25 = new A.SaturationModulation(){ Val = 105000 };
            A.Tint tint28 = new A.Tint(){ Val = 67000 };

            schemeColor120.Append(luminanceModulation1);
            schemeColor120.Append(saturationModulation25);
            schemeColor120.Append(tint28);

            gradientStop23.Append(schemeColor120);

            A.GradientStop gradientStop24 = new A.GradientStop(){ Position = 50000 };

            A.SchemeColor schemeColor121 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.LuminanceModulation luminanceModulation2 = new A.LuminanceModulation(){ Val = 105000 };
            A.SaturationModulation saturationModulation26 = new A.SaturationModulation(){ Val = 103000 };
            A.Tint tint29 = new A.Tint(){ Val = 73000 };

            schemeColor121.Append(luminanceModulation2);
            schemeColor121.Append(saturationModulation26);
            schemeColor121.Append(tint29);

            gradientStop24.Append(schemeColor121);

            A.GradientStop gradientStop25 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor122 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.LuminanceModulation luminanceModulation3 = new A.LuminanceModulation(){ Val = 105000 };
            A.SaturationModulation saturationModulation27 = new A.SaturationModulation(){ Val = 109000 };
            A.Tint tint30 = new A.Tint(){ Val = 81000 };

            schemeColor122.Append(luminanceModulation3);
            schemeColor122.Append(saturationModulation27);
            schemeColor122.Append(tint30);

            gradientStop25.Append(schemeColor122);

            gradientStopList9.Append(gradientStop23);
            gradientStopList9.Append(gradientStop24);
            gradientStopList9.Append(gradientStop25);
            A.LinearGradientFill linearGradientFill5 = new A.LinearGradientFill(){ Angle = 5400000, Scaled = false };

            gradientFill9.Append(gradientStopList9);
            gradientFill9.Append(linearGradientFill5);

            A.GradientFill gradientFill10 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList10 = new A.GradientStopList();

            A.GradientStop gradientStop26 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor123 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.SaturationModulation saturationModulation28 = new A.SaturationModulation(){ Val = 103000 };
            A.LuminanceModulation luminanceModulation4 = new A.LuminanceModulation(){ Val = 102000 };
            A.Tint tint31 = new A.Tint(){ Val = 94000 };

            schemeColor123.Append(saturationModulation28);
            schemeColor123.Append(luminanceModulation4);
            schemeColor123.Append(tint31);

            gradientStop26.Append(schemeColor123);

            A.GradientStop gradientStop27 = new A.GradientStop(){ Position = 50000 };

            A.SchemeColor schemeColor124 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.SaturationModulation saturationModulation29 = new A.SaturationModulation(){ Val = 110000 };
            A.LuminanceModulation luminanceModulation5 = new A.LuminanceModulation(){ Val = 100000 };
            A.Shade shade18 = new A.Shade(){ Val = 100000 };

            schemeColor124.Append(saturationModulation29);
            schemeColor124.Append(luminanceModulation5);
            schemeColor124.Append(shade18);

            gradientStop27.Append(schemeColor124);

            A.GradientStop gradientStop28 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor125 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.LuminanceModulation luminanceModulation6 = new A.LuminanceModulation(){ Val = 99000 };
            A.SaturationModulation saturationModulation30 = new A.SaturationModulation(){ Val = 120000 };
            A.Shade shade19 = new A.Shade(){ Val = 78000 };

            schemeColor125.Append(luminanceModulation6);
            schemeColor125.Append(saturationModulation30);
            schemeColor125.Append(shade19);

            gradientStop28.Append(schemeColor125);

            gradientStopList10.Append(gradientStop26);
            gradientStopList10.Append(gradientStop27);
            gradientStopList10.Append(gradientStop28);
            A.LinearGradientFill linearGradientFill6 = new A.LinearGradientFill(){ Angle = 5400000, Scaled = false };

            gradientFill10.Append(gradientStopList10);
            gradientFill10.Append(linearGradientFill6);

            fillStyleList3.Append(solidFill100);
            fillStyleList3.Append(gradientFill9);
            fillStyleList3.Append(gradientFill10);

            A.LineStyleList lineStyleList3 = new A.LineStyleList();

            A.Outline outline11 = new A.Outline(){ Width = 6350, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill101 = new A.SolidFill();
            A.SchemeColor schemeColor126 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill101.Append(schemeColor126);
            A.PresetDash presetDash7 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };
            A.Miter miter1 = new A.Miter(){ Limit = 800000 };

            outline11.Append(solidFill101);
            outline11.Append(presetDash7);
            outline11.Append(miter1);

            A.Outline outline12 = new A.Outline(){ Width = 12700, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill102 = new A.SolidFill();
            A.SchemeColor schemeColor127 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill102.Append(schemeColor127);
            A.PresetDash presetDash8 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };
            A.Miter miter2 = new A.Miter(){ Limit = 800000 };

            outline12.Append(solidFill102);
            outline12.Append(presetDash8);
            outline12.Append(miter2);

            A.Outline outline13 = new A.Outline(){ Width = 19050, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill103 = new A.SolidFill();
            A.SchemeColor schemeColor128 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill103.Append(schemeColor128);
            A.PresetDash presetDash9 = new A.PresetDash(){ Val = A.PresetLineDashValues.Solid };
            A.Miter miter3 = new A.Miter(){ Limit = 800000 };

            outline13.Append(solidFill103);
            outline13.Append(presetDash9);
            outline13.Append(miter3);

            lineStyleList3.Append(outline11);
            lineStyleList3.Append(outline12);
            lineStyleList3.Append(outline13);

            A.EffectStyleList effectStyleList3 = new A.EffectStyleList();

            A.EffectStyle effectStyle7 = new A.EffectStyle();
            A.EffectList effectList7 = new A.EffectList();

            effectStyle7.Append(effectList7);

            A.EffectStyle effectStyle8 = new A.EffectStyle();
            A.EffectList effectList8 = new A.EffectList();

            effectStyle8.Append(effectList8);

            A.EffectStyle effectStyle9 = new A.EffectStyle();

            A.EffectList effectList9 = new A.EffectList();

            A.OuterShadow outerShadow7 = new A.OuterShadow(){ BlurRadius = 57150L, Distance = 19050L, Direction = 5400000, Alignment = A.RectangleAlignmentValues.Center, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex60 = new A.RgbColorModelHex(){ Val = "000000" };
            A.Alpha alpha7 = new A.Alpha(){ Val = 63000 };

            rgbColorModelHex60.Append(alpha7);

            outerShadow7.Append(rgbColorModelHex60);

            effectList9.Append(outerShadow7);

            effectStyle9.Append(effectList9);

            effectStyleList3.Append(effectStyle7);
            effectStyleList3.Append(effectStyle8);
            effectStyleList3.Append(effectStyle9);

            A.BackgroundFillStyleList backgroundFillStyleList3 = new A.BackgroundFillStyleList();

            A.SolidFill solidFill104 = new A.SolidFill();
            A.SchemeColor schemeColor129 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };

            solidFill104.Append(schemeColor129);

            A.SolidFill solidFill105 = new A.SolidFill();

            A.SchemeColor schemeColor130 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint32 = new A.Tint(){ Val = 95000 };
            A.SaturationModulation saturationModulation31 = new A.SaturationModulation(){ Val = 170000 };

            schemeColor130.Append(tint32);
            schemeColor130.Append(saturationModulation31);

            solidFill105.Append(schemeColor130);

            A.GradientFill gradientFill11 = new A.GradientFill(){ RotateWithShape = true };

            A.GradientStopList gradientStopList11 = new A.GradientStopList();

            A.GradientStop gradientStop29 = new A.GradientStop(){ Position = 0 };

            A.SchemeColor schemeColor131 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint33 = new A.Tint(){ Val = 93000 };
            A.SaturationModulation saturationModulation32 = new A.SaturationModulation(){ Val = 150000 };
            A.Shade shade20 = new A.Shade(){ Val = 98000 };
            A.LuminanceModulation luminanceModulation7 = new A.LuminanceModulation(){ Val = 102000 };

            schemeColor131.Append(tint33);
            schemeColor131.Append(saturationModulation32);
            schemeColor131.Append(shade20);
            schemeColor131.Append(luminanceModulation7);

            gradientStop29.Append(schemeColor131);

            A.GradientStop gradientStop30 = new A.GradientStop(){ Position = 50000 };

            A.SchemeColor schemeColor132 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Tint tint34 = new A.Tint(){ Val = 98000 };
            A.SaturationModulation saturationModulation33 = new A.SaturationModulation(){ Val = 130000 };
            A.Shade shade21 = new A.Shade(){ Val = 90000 };
            A.LuminanceModulation luminanceModulation8 = new A.LuminanceModulation(){ Val = 103000 };

            schemeColor132.Append(tint34);
            schemeColor132.Append(saturationModulation33);
            schemeColor132.Append(shade21);
            schemeColor132.Append(luminanceModulation8);

            gradientStop30.Append(schemeColor132);

            A.GradientStop gradientStop31 = new A.GradientStop(){ Position = 100000 };

            A.SchemeColor schemeColor133 = new A.SchemeColor(){ Val = A.SchemeColorValues.PhColor };
            A.Shade shade22 = new A.Shade(){ Val = 63000 };
            A.SaturationModulation saturationModulation34 = new A.SaturationModulation(){ Val = 120000 };

            schemeColor133.Append(shade22);
            schemeColor133.Append(saturationModulation34);

            gradientStop31.Append(schemeColor133);

            gradientStopList11.Append(gradientStop29);
            gradientStopList11.Append(gradientStop30);
            gradientStopList11.Append(gradientStop31);
            A.LinearGradientFill linearGradientFill7 = new A.LinearGradientFill(){ Angle = 5400000, Scaled = false };

            gradientFill11.Append(gradientStopList11);
            gradientFill11.Append(linearGradientFill7);

            backgroundFillStyleList3.Append(solidFill104);
            backgroundFillStyleList3.Append(solidFill105);
            backgroundFillStyleList3.Append(gradientFill11);

            formatScheme3.Append(fillStyleList3);
            formatScheme3.Append(lineStyleList3);
            formatScheme3.Append(effectStyleList3);
            formatScheme3.Append(backgroundFillStyleList3);

            themeElements3.Append(colorScheme3);
            themeElements3.Append(fontScheme3);
            themeElements3.Append(formatScheme3);
            A.ObjectDefaults objectDefaults3 = new A.ObjectDefaults();
            A.ExtraColorSchemeList extraColorSchemeList3 = new A.ExtraColorSchemeList();

            A.ExtensionList extensionList1 = new A.ExtensionList();

            A.Extension extension1 = new A.Extension(){ Uri = "{05A4C25C-085E-4340-85A3-A5531E510DB2}" };

            OpenXmlUnknownElement openXmlUnknownElement3 = OpenXmlUnknownElement.CreateOpenXmlUnknownElement("<thm15:themeFamily xmlns:thm15=\"http://schemas.microsoft.com/office/thememl/2012/main\" name=\"Office Theme\" id=\"{62F939B6-93AF-4DB8-9C6B-D6C7DFDC589F}\" vid=\"{4A3C46E8-61CC-4603-A589-7422A47A8E4A}\" />");

            extension1.Append(openXmlUnknownElement3);

            extensionList1.Append(extension1);

            theme3.Append(themeElements3);
            theme3.Append(objectDefaults3);
            theme3.Append(extraColorSchemeList3);
            theme3.Append(extensionList1);

            themePart3.Theme = theme3;
        }

        // Generates content of slidePart1.
        private void GenerateSlidePart1Content(string text, SlidePart slidePart1)
        {
            Slide slide1 = new Slide();
            slide1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            slide1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            slide1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            CommonSlideData commonSlideData7 = new CommonSlideData();

            ShapeTree shapeTree7 = new ShapeTree();

            NonVisualGroupShapeProperties nonVisualGroupShapeProperties7 = new NonVisualGroupShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties41 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
            NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties7 = new NonVisualGroupShapeDrawingProperties();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties41 = new ApplicationNonVisualDrawingProperties();

            nonVisualGroupShapeProperties7.Append(nonVisualDrawingProperties41);
            nonVisualGroupShapeProperties7.Append(nonVisualGroupShapeDrawingProperties7);
            nonVisualGroupShapeProperties7.Append(applicationNonVisualDrawingProperties41);

            GroupShapeProperties groupShapeProperties7 = new GroupShapeProperties();

            A.TransformGroup transformGroup7 = new A.TransformGroup();
            A.Offset offset38 = new A.Offset(){ X = 0L, Y = 0L };
            A.Extents extents38 = new A.Extents(){ Cx = 0L, Cy = 0L };
            A.ChildOffset childOffset7 = new A.ChildOffset(){ X = 0L, Y = 0L };
            A.ChildExtents childExtents7 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

            transformGroup7.Append(offset38);
            transformGroup7.Append(extents38);
            transformGroup7.Append(childOffset7);
            transformGroup7.Append(childExtents7);

            groupShapeProperties7.Append(transformGroup7);

            Shape shape32 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties32 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties42 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Platshållare för text 1" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties32 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks27 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties32.Append(shapeLocks27);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties42 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape27 = new PlaceholderShape(){ Type = PlaceholderValues.Body, Size = PlaceholderSizeValues.Quarter, Index = (UInt32Value)10U };

            applicationNonVisualDrawingProperties42.Append(placeholderShape27);

            nonVisualShapeProperties32.Append(nonVisualDrawingProperties42);
            nonVisualShapeProperties32.Append(nonVisualShapeDrawingProperties32);
            nonVisualShapeProperties32.Append(applicationNonVisualDrawingProperties42);
            ShapeProperties shapeProperties35 = new ShapeProperties();

            TextBody textBody32 = new TextBody();
            A.BodyProperties bodyProperties32 = new A.BodyProperties();
            A.ListStyle listStyle32 = new A.ListStyle();

            A.Paragraph paragraph44 = new A.Paragraph();

            A.Run run27 = new A.Run();
            A.RunProperties runProperties37 = new A.RunProperties(){ Language = "sv-SE", Dirty = false, SmartTagClean = false };
            A.Text text37 = new A.Text();
            text37.Text = text;
//            text37.Text = "HME";

            run27.Append(runProperties37);
            run27.Append(text37);
            A.EndParagraphRunProperties endParagraphRunProperties28 = new A.EndParagraphRunProperties(){ Language = "sv-SE", Dirty = false };

            paragraph44.Append(run27);
            paragraph44.Append(endParagraphRunProperties28);

            textBody32.Append(bodyProperties32);
            textBody32.Append(listStyle32);
            textBody32.Append(paragraph44);

            shape32.Append(nonVisualShapeProperties32);
            shape32.Append(shapeProperties35);
            shape32.Append(textBody32);

            Shape shape33 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties33 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties43 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1026U, Name = "AutoShape 2", Description = "http://localhost:3428/reportImage.aspx?LangID=1&FY=2012&TY=2013&SAID=514&SID=83&GB=7&RPID=15&RPLID=119&PRUID=2643&GRPNG=2&GID=0,923&PLOT=docx" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties33 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks28 = new A.ShapeLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

            nonVisualShapeDrawingProperties33.Append(shapeLocks28);
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties43 = new ApplicationNonVisualDrawingProperties();

            nonVisualShapeProperties33.Append(nonVisualDrawingProperties43);
            nonVisualShapeProperties33.Append(nonVisualShapeDrawingProperties33);
            nonVisualShapeProperties33.Append(applicationNonVisualDrawingProperties43);

            ShapeProperties shapeProperties36 = new ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D32 = new A.Transform2D();
            A.Offset offset39 = new A.Offset(){ X = 155575L, Y = -144463L };
            A.Extents extents39 = new A.Extents(){ Cx = 304800L, Cy = 304801L };

            transform2D32.Append(offset39);
            transform2D32.Append(extents39);

            A.PresetGeometry presetGeometry25 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList25 = new A.AdjustValueList();

            presetGeometry25.Append(adjustValueList25);
            A.NoFill noFill10 = new A.NoFill();

            shapeProperties36.Append(transform2D32);
            shapeProperties36.Append(presetGeometry25);
            shapeProperties36.Append(noFill10);

            TextBody textBody33 = new TextBody();

            A.BodyProperties bodyProperties33 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, Wrap = A.TextWrappingValues.Square, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, ColumnCount = 1, Anchor = A.TextAnchoringTypeValues.Top, AnchorCenter = false, CompatibleLineSpacing = true };

            A.PresetTextWrap presetTextWrap1 = new A.PresetTextWrap(){ Preset = A.TextShapeValues.TextNoShape };
            A.AdjustValueList adjustValueList26 = new A.AdjustValueList();

            presetTextWrap1.Append(adjustValueList26);

            bodyProperties33.Append(presetTextWrap1);
            A.ListStyle listStyle33 = new A.ListStyle();

            A.Paragraph paragraph45 = new A.Paragraph();
            A.EndParagraphRunProperties endParagraphRunProperties29 = new A.EndParagraphRunProperties(){ Language = "en-US" };

            paragraph45.Append(endParagraphRunProperties29);

            textBody33.Append(bodyProperties33);
            textBody33.Append(listStyle33);
            textBody33.Append(paragraph45);

            shape33.Append(nonVisualShapeProperties33);
            shape33.Append(shapeProperties36);
            shape33.Append(textBody33);

            Shape shape34 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties34 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties44 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1028U, Name = "AutoShape 4", Description = "http://localhost:3428/reportImage.aspx?LangID=1&FY=2012&TY=2013&SAID=514&SID=83&GB=7&RPID=15&RPLID=119&PRUID=2643&GRPNG=2&GID=0,923&PLOT=docx" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties34 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks29 = new A.ShapeLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

            nonVisualShapeDrawingProperties34.Append(shapeLocks29);
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties44 = new ApplicationNonVisualDrawingProperties();

            nonVisualShapeProperties34.Append(nonVisualDrawingProperties44);
            nonVisualShapeProperties34.Append(nonVisualShapeDrawingProperties34);
            nonVisualShapeProperties34.Append(applicationNonVisualDrawingProperties44);

            ShapeProperties shapeProperties37 = new ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D33 = new A.Transform2D();
            A.Offset offset40 = new A.Offset(){ X = 155575L, Y = -144463L };
            A.Extents extents40 = new A.Extents(){ Cx = 304800L, Cy = 304801L };

            transform2D33.Append(offset40);
            transform2D33.Append(extents40);

            A.PresetGeometry presetGeometry26 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList27 = new A.AdjustValueList();

            presetGeometry26.Append(adjustValueList27);
            A.NoFill noFill11 = new A.NoFill();

            shapeProperties37.Append(transform2D33);
            shapeProperties37.Append(presetGeometry26);
            shapeProperties37.Append(noFill11);

            TextBody textBody34 = new TextBody();

            A.BodyProperties bodyProperties34 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, Wrap = A.TextWrappingValues.Square, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, ColumnCount = 1, Anchor = A.TextAnchoringTypeValues.Top, AnchorCenter = false, CompatibleLineSpacing = true };

            A.PresetTextWrap presetTextWrap2 = new A.PresetTextWrap(){ Preset = A.TextShapeValues.TextNoShape };
            A.AdjustValueList adjustValueList28 = new A.AdjustValueList();

            presetTextWrap2.Append(adjustValueList28);

            bodyProperties34.Append(presetTextWrap2);
            A.ListStyle listStyle34 = new A.ListStyle();

            A.Paragraph paragraph46 = new A.Paragraph();
            A.EndParagraphRunProperties endParagraphRunProperties30 = new A.EndParagraphRunProperties(){ Language = "en-US" };

            paragraph46.Append(endParagraphRunProperties30);

            textBody34.Append(bodyProperties34);
            textBody34.Append(listStyle34);
            textBody34.Append(paragraph46);

            shape34.Append(nonVisualShapeProperties34);
            shape34.Append(shapeProperties37);
            shape34.Append(textBody34);

            Picture picture4 = new Picture();

            NonVisualPictureProperties nonVisualPictureProperties4 = new NonVisualPictureProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties45 = new NonVisualDrawingProperties(){ Id = (UInt32Value)6U, Name = "Picture 5", Description = "Untitled.png" };

            NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties4 = new NonVisualPictureDrawingProperties();
            A.PictureLocks pictureLocks4 = new A.PictureLocks(){ NoChangeAspect = true };

            nonVisualPictureDrawingProperties4.Append(pictureLocks4);
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties45 = new ApplicationNonVisualDrawingProperties();

            nonVisualPictureProperties4.Append(nonVisualDrawingProperties45);
            nonVisualPictureProperties4.Append(nonVisualPictureDrawingProperties4);
            nonVisualPictureProperties4.Append(applicationNonVisualDrawingProperties45);

            BlipFill blipFill4 = new BlipFill();
            A.Blip blip4 = new A.Blip(){ Embed = "rId2", CompressionState = A.BlipCompressionValues.Print };

            A.Stretch stretch4 = new A.Stretch();
            A.FillRectangle fillRectangle4 = new A.FillRectangle();

            stretch4.Append(fillRectangle4);

            blipFill4.Append(blip4);
            blipFill4.Append(stretch4);

            ShapeProperties shapeProperties38 = new ShapeProperties();

            A.Transform2D transform2D34 = new A.Transform2D();
            A.Offset offset41 = new A.Offset(){ X = 537567L, Y = 1631197L };
            A.Extents extents41 = new A.Extents(){ Cx = 7768233L, Cy = 3819019L };

            transform2D34.Append(offset41);
            transform2D34.Append(extents41);

            A.PresetGeometry presetGeometry27 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList29 = new A.AdjustValueList();

            presetGeometry27.Append(adjustValueList29);

            shapeProperties38.Append(transform2D34);
            shapeProperties38.Append(presetGeometry27);

            picture4.Append(nonVisualPictureProperties4);
            picture4.Append(blipFill4);
            picture4.Append(shapeProperties38);

            shapeTree7.Append(nonVisualGroupShapeProperties7);
            shapeTree7.Append(groupShapeProperties7);
            shapeTree7.Append(shape32);
            shapeTree7.Append(shape33);
            shapeTree7.Append(shape34);
            shapeTree7.Append(picture4);

            CommonSlideDataExtensionList commonSlideDataExtensionList5 = new CommonSlideDataExtensionList();

            CommonSlideDataExtension commonSlideDataExtension5 = new CommonSlideDataExtension(){ Uri = "{BB962C8B-B14F-4D97-AF65-F5344CB8AC3E}" };

            P14.CreationId creationId5 = new P14.CreationId(){ Val = (UInt32Value)1046939494U };
            creationId5.AddNamespaceDeclaration("p14", "http://schemas.microsoft.com/office/powerpoint/2010/main");

            commonSlideDataExtension5.Append(creationId5);

            commonSlideDataExtensionList5.Append(commonSlideDataExtension5);

            commonSlideData7.Append(shapeTree7);
            commonSlideData7.Append(commonSlideDataExtensionList5);

            ColorMapOverride colorMapOverride4 = new ColorMapOverride();
            A.MasterColorMapping masterColorMapping4 = new A.MasterColorMapping();

            colorMapOverride4.Append(masterColorMapping4);

            Timing timing1 = new Timing();

            TimeNodeList timeNodeList1 = new TimeNodeList();

            ParallelTimeNode parallelTimeNode1 = new ParallelTimeNode();
            CommonTimeNode commonTimeNode1 = new CommonTimeNode(){ Id = (UInt32Value)1U, Duration = "indefinite", Restart = TimeNodeRestartValues.Never, NodeType = TimeNodeValues.TmingRoot };

            parallelTimeNode1.Append(commonTimeNode1);

            timeNodeList1.Append(parallelTimeNode1);

            timing1.Append(timeNodeList1);

            slide1.Append(commonSlideData7);
            slide1.Append(colorMapOverride4);
            slide1.Append(timing1);

            slidePart1.Slide = slide1;
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

        // Generates content of tableStylesPart1.
        private void GenerateTableStylesPart1Content(TableStylesPart tableStylesPart1)
        {
            A.TableStyleList tableStyleList1 = new A.TableStyleList(){ Default = "{5C22544A-7EE6-4342-B048-85BDC9FD1C3A}" };
            tableStyleList1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

            A.TableStyleEntry tableStyleEntry1 = new A.TableStyleEntry(){ StyleId = "{5C22544A-7EE6-4342-B048-85BDC9FD1C3A}", StyleName = "Mellanmörkt format 2 - Dekorfärg 1" };

            A.WholeTable wholeTable1 = new A.WholeTable();

            A.TableCellTextStyle tableCellTextStyle1 = new A.TableCellTextStyle();

            A.FontReference fontReference4 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };
            A.PresetColor presetColor2 = new A.PresetColor(){ Val = A.PresetColorValues.Black };

            fontReference4.Append(presetColor2);
            A.SchemeColor schemeColor134 = new A.SchemeColor(){ Val = A.SchemeColorValues.Dark1 };

            tableCellTextStyle1.Append(fontReference4);
            tableCellTextStyle1.Append(schemeColor134);

            A.TableCellStyle tableCellStyle1 = new A.TableCellStyle();

            A.TableCellBorders tableCellBorders1 = new A.TableCellBorders();

            A.LeftBorder leftBorder1 = new A.LeftBorder();

            A.Outline outline14 = new A.Outline(){ Width = 12700, CompoundLineType = A.CompoundLineValues.Single };

            A.SolidFill solidFill106 = new A.SolidFill();
            A.SchemeColor schemeColor135 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            solidFill106.Append(schemeColor135);

            outline14.Append(solidFill106);

            leftBorder1.Append(outline14);

            A.RightBorder rightBorder1 = new A.RightBorder();

            A.Outline outline15 = new A.Outline(){ Width = 12700, CompoundLineType = A.CompoundLineValues.Single };

            A.SolidFill solidFill107 = new A.SolidFill();
            A.SchemeColor schemeColor136 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            solidFill107.Append(schemeColor136);

            outline15.Append(solidFill107);

            rightBorder1.Append(outline15);

            A.TopBorder topBorder1 = new A.TopBorder();

            A.Outline outline16 = new A.Outline(){ Width = 12700, CompoundLineType = A.CompoundLineValues.Single };

            A.SolidFill solidFill108 = new A.SolidFill();
            A.SchemeColor schemeColor137 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            solidFill108.Append(schemeColor137);

            outline16.Append(solidFill108);

            topBorder1.Append(outline16);

            A.BottomBorder bottomBorder1 = new A.BottomBorder();

            A.Outline outline17 = new A.Outline(){ Width = 12700, CompoundLineType = A.CompoundLineValues.Single };

            A.SolidFill solidFill109 = new A.SolidFill();
            A.SchemeColor schemeColor138 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            solidFill109.Append(schemeColor138);

            outline17.Append(solidFill109);

            bottomBorder1.Append(outline17);

            A.InsideHorizontalBorder insideHorizontalBorder1 = new A.InsideHorizontalBorder();

            A.Outline outline18 = new A.Outline(){ Width = 12700, CompoundLineType = A.CompoundLineValues.Single };

            A.SolidFill solidFill110 = new A.SolidFill();
            A.SchemeColor schemeColor139 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            solidFill110.Append(schemeColor139);

            outline18.Append(solidFill110);

            insideHorizontalBorder1.Append(outline18);

            A.InsideVerticalBorder insideVerticalBorder1 = new A.InsideVerticalBorder();

            A.Outline outline19 = new A.Outline(){ Width = 12700, CompoundLineType = A.CompoundLineValues.Single };

            A.SolidFill solidFill111 = new A.SolidFill();
            A.SchemeColor schemeColor140 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            solidFill111.Append(schemeColor140);

            outline19.Append(solidFill111);

            insideVerticalBorder1.Append(outline19);

            tableCellBorders1.Append(leftBorder1);
            tableCellBorders1.Append(rightBorder1);
            tableCellBorders1.Append(topBorder1);
            tableCellBorders1.Append(bottomBorder1);
            tableCellBorders1.Append(insideHorizontalBorder1);
            tableCellBorders1.Append(insideVerticalBorder1);

            A.FillProperties fillProperties1 = new A.FillProperties();

            A.SolidFill solidFill112 = new A.SolidFill();

            A.SchemeColor schemeColor141 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };
            A.Tint tint35 = new A.Tint(){ Val = 20000 };

            schemeColor141.Append(tint35);

            solidFill112.Append(schemeColor141);

            fillProperties1.Append(solidFill112);

            tableCellStyle1.Append(tableCellBorders1);
            tableCellStyle1.Append(fillProperties1);

            wholeTable1.Append(tableCellTextStyle1);
            wholeTable1.Append(tableCellStyle1);

            A.Band1Horizontal band1Horizontal1 = new A.Band1Horizontal();

            A.TableCellStyle tableCellStyle2 = new A.TableCellStyle();
            A.TableCellBorders tableCellBorders2 = new A.TableCellBorders();

            A.FillProperties fillProperties2 = new A.FillProperties();

            A.SolidFill solidFill113 = new A.SolidFill();

            A.SchemeColor schemeColor142 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };
            A.Tint tint36 = new A.Tint(){ Val = 40000 };

            schemeColor142.Append(tint36);

            solidFill113.Append(schemeColor142);

            fillProperties2.Append(solidFill113);

            tableCellStyle2.Append(tableCellBorders2);
            tableCellStyle2.Append(fillProperties2);

            band1Horizontal1.Append(tableCellStyle2);

            A.Band2Horizontal band2Horizontal1 = new A.Band2Horizontal();

            A.TableCellStyle tableCellStyle3 = new A.TableCellStyle();
            A.TableCellBorders tableCellBorders3 = new A.TableCellBorders();

            tableCellStyle3.Append(tableCellBorders3);

            band2Horizontal1.Append(tableCellStyle3);

            A.Band1Vertical band1Vertical1 = new A.Band1Vertical();

            A.TableCellStyle tableCellStyle4 = new A.TableCellStyle();
            A.TableCellBorders tableCellBorders4 = new A.TableCellBorders();

            A.FillProperties fillProperties3 = new A.FillProperties();

            A.SolidFill solidFill114 = new A.SolidFill();

            A.SchemeColor schemeColor143 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };
            A.Tint tint37 = new A.Tint(){ Val = 40000 };

            schemeColor143.Append(tint37);

            solidFill114.Append(schemeColor143);

            fillProperties3.Append(solidFill114);

            tableCellStyle4.Append(tableCellBorders4);
            tableCellStyle4.Append(fillProperties3);

            band1Vertical1.Append(tableCellStyle4);

            A.Band2Vertical band2Vertical1 = new A.Band2Vertical();

            A.TableCellStyle tableCellStyle5 = new A.TableCellStyle();
            A.TableCellBorders tableCellBorders5 = new A.TableCellBorders();

            tableCellStyle5.Append(tableCellBorders5);

            band2Vertical1.Append(tableCellStyle5);

            A.LastColumn lastColumn1 = new A.LastColumn();

            A.TableCellTextStyle tableCellTextStyle2 = new A.TableCellTextStyle(){ Bold = A.BooleanStyleValues.On };

            A.FontReference fontReference5 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };
            A.PresetColor presetColor3 = new A.PresetColor(){ Val = A.PresetColorValues.Black };

            fontReference5.Append(presetColor3);
            A.SchemeColor schemeColor144 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            tableCellTextStyle2.Append(fontReference5);
            tableCellTextStyle2.Append(schemeColor144);

            A.TableCellStyle tableCellStyle6 = new A.TableCellStyle();
            A.TableCellBorders tableCellBorders6 = new A.TableCellBorders();

            A.FillProperties fillProperties4 = new A.FillProperties();

            A.SolidFill solidFill115 = new A.SolidFill();
            A.SchemeColor schemeColor145 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };

            solidFill115.Append(schemeColor145);

            fillProperties4.Append(solidFill115);

            tableCellStyle6.Append(tableCellBorders6);
            tableCellStyle6.Append(fillProperties4);

            lastColumn1.Append(tableCellTextStyle2);
            lastColumn1.Append(tableCellStyle6);

            A.FirstColumn firstColumn1 = new A.FirstColumn();

            A.TableCellTextStyle tableCellTextStyle3 = new A.TableCellTextStyle(){ Bold = A.BooleanStyleValues.On };

            A.FontReference fontReference6 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };
            A.PresetColor presetColor4 = new A.PresetColor(){ Val = A.PresetColorValues.Black };

            fontReference6.Append(presetColor4);
            A.SchemeColor schemeColor146 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            tableCellTextStyle3.Append(fontReference6);
            tableCellTextStyle3.Append(schemeColor146);

            A.TableCellStyle tableCellStyle7 = new A.TableCellStyle();
            A.TableCellBorders tableCellBorders7 = new A.TableCellBorders();

            A.FillProperties fillProperties5 = new A.FillProperties();

            A.SolidFill solidFill116 = new A.SolidFill();
            A.SchemeColor schemeColor147 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };

            solidFill116.Append(schemeColor147);

            fillProperties5.Append(solidFill116);

            tableCellStyle7.Append(tableCellBorders7);
            tableCellStyle7.Append(fillProperties5);

            firstColumn1.Append(tableCellTextStyle3);
            firstColumn1.Append(tableCellStyle7);

            A.LastRow lastRow1 = new A.LastRow();

            A.TableCellTextStyle tableCellTextStyle4 = new A.TableCellTextStyle(){ Bold = A.BooleanStyleValues.On };

            A.FontReference fontReference7 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };
            A.PresetColor presetColor5 = new A.PresetColor(){ Val = A.PresetColorValues.Black };

            fontReference7.Append(presetColor5);
            A.SchemeColor schemeColor148 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            tableCellTextStyle4.Append(fontReference7);
            tableCellTextStyle4.Append(schemeColor148);

            A.TableCellStyle tableCellStyle8 = new A.TableCellStyle();

            A.TableCellBorders tableCellBorders8 = new A.TableCellBorders();

            A.TopBorder topBorder2 = new A.TopBorder();

            A.Outline outline20 = new A.Outline(){ Width = 38100, CompoundLineType = A.CompoundLineValues.Single };

            A.SolidFill solidFill117 = new A.SolidFill();
            A.SchemeColor schemeColor149 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            solidFill117.Append(schemeColor149);

            outline20.Append(solidFill117);

            topBorder2.Append(outline20);

            tableCellBorders8.Append(topBorder2);

            A.FillProperties fillProperties6 = new A.FillProperties();

            A.SolidFill solidFill118 = new A.SolidFill();
            A.SchemeColor schemeColor150 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };

            solidFill118.Append(schemeColor150);

            fillProperties6.Append(solidFill118);

            tableCellStyle8.Append(tableCellBorders8);
            tableCellStyle8.Append(fillProperties6);

            lastRow1.Append(tableCellTextStyle4);
            lastRow1.Append(tableCellStyle8);

            A.FirstRow firstRow1 = new A.FirstRow();

            A.TableCellTextStyle tableCellTextStyle5 = new A.TableCellTextStyle(){ Bold = A.BooleanStyleValues.On };

            A.FontReference fontReference8 = new A.FontReference(){ Index = A.FontCollectionIndexValues.Minor };
            A.PresetColor presetColor6 = new A.PresetColor(){ Val = A.PresetColorValues.Black };

            fontReference8.Append(presetColor6);
            A.SchemeColor schemeColor151 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            tableCellTextStyle5.Append(fontReference8);
            tableCellTextStyle5.Append(schemeColor151);

            A.TableCellStyle tableCellStyle9 = new A.TableCellStyle();

            A.TableCellBorders tableCellBorders9 = new A.TableCellBorders();

            A.BottomBorder bottomBorder2 = new A.BottomBorder();

            A.Outline outline21 = new A.Outline(){ Width = 38100, CompoundLineType = A.CompoundLineValues.Single };

            A.SolidFill solidFill119 = new A.SolidFill();
            A.SchemeColor schemeColor152 = new A.SchemeColor(){ Val = A.SchemeColorValues.Light1 };

            solidFill119.Append(schemeColor152);

            outline21.Append(solidFill119);

            bottomBorder2.Append(outline21);

            tableCellBorders9.Append(bottomBorder2);

            A.FillProperties fillProperties7 = new A.FillProperties();

            A.SolidFill solidFill120 = new A.SolidFill();
            A.SchemeColor schemeColor153 = new A.SchemeColor(){ Val = A.SchemeColorValues.Accent1 };

            solidFill120.Append(schemeColor153);

            fillProperties7.Append(solidFill120);

            tableCellStyle9.Append(tableCellBorders9);
            tableCellStyle9.Append(fillProperties7);

            firstRow1.Append(tableCellTextStyle5);
            firstRow1.Append(tableCellStyle9);

            tableStyleEntry1.Append(wholeTable1);
            tableStyleEntry1.Append(band1Horizontal1);
            tableStyleEntry1.Append(band2Horizontal1);
            tableStyleEntry1.Append(band1Vertical1);
            tableStyleEntry1.Append(band2Vertical1);
            tableStyleEntry1.Append(lastColumn1);
            tableStyleEntry1.Append(firstColumn1);
            tableStyleEntry1.Append(lastRow1);
            tableStyleEntry1.Append(firstRow1);

            tableStyleList1.Append(tableStyleEntry1);

            tableStylesPart1.TableStyleList = tableStyleList1;
        }

        // Generates content of slidePart2.
        private void GenerateSlidePart2Content(SlidePart slidePart2)
        {
            Slide slide2 = new Slide();
            slide2.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            slide2.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            slide2.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            CommonSlideData commonSlideData8 = new CommonSlideData();

            ShapeTree shapeTree8 = new ShapeTree();

            NonVisualGroupShapeProperties nonVisualGroupShapeProperties8 = new NonVisualGroupShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties46 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1U, Name = "" };
            NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties8 = new NonVisualGroupShapeDrawingProperties();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties46 = new ApplicationNonVisualDrawingProperties();

            nonVisualGroupShapeProperties8.Append(nonVisualDrawingProperties46);
            nonVisualGroupShapeProperties8.Append(nonVisualGroupShapeDrawingProperties8);
            nonVisualGroupShapeProperties8.Append(applicationNonVisualDrawingProperties46);

            GroupShapeProperties groupShapeProperties8 = new GroupShapeProperties();

            A.TransformGroup transformGroup8 = new A.TransformGroup();
            A.Offset offset42 = new A.Offset(){ X = 0L, Y = 0L };
            A.Extents extents42 = new A.Extents(){ Cx = 0L, Cy = 0L };
            A.ChildOffset childOffset8 = new A.ChildOffset(){ X = 0L, Y = 0L };
            A.ChildExtents childExtents8 = new A.ChildExtents(){ Cx = 0L, Cy = 0L };

            transformGroup8.Append(offset42);
            transformGroup8.Append(extents42);
            transformGroup8.Append(childOffset8);
            transformGroup8.Append(childExtents8);

            groupShapeProperties8.Append(transformGroup8);

            Shape shape35 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties35 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties47 = new NonVisualDrawingProperties(){ Id = (UInt32Value)2U, Name = "Title 1" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties35 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks30 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties35.Append(shapeLocks30);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties47 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape28 = new PlaceholderShape(){ Type = PlaceholderValues.CenteredTitle };

            applicationNonVisualDrawingProperties47.Append(placeholderShape28);

            nonVisualShapeProperties35.Append(nonVisualDrawingProperties47);
            nonVisualShapeProperties35.Append(nonVisualShapeDrawingProperties35);
            nonVisualShapeProperties35.Append(applicationNonVisualDrawingProperties47);

            ShapeProperties shapeProperties39 = new ShapeProperties();

            A.Transform2D transform2D35 = new A.Transform2D();
            A.Offset offset43 = new A.Offset(){ X = 500034L, Y = 2504866L };
            A.Extents extents43 = new A.Extents(){ Cx = 8072438L, Cy = 1155699L };

            transform2D35.Append(offset43);
            transform2D35.Append(extents43);

            shapeProperties39.Append(transform2D35);

            TextBody textBody35 = new TextBody();

            A.BodyProperties bodyProperties35 = new A.BodyProperties(){ Anchor = A.TextAnchoringTypeValues.Bottom, AnchorCenter = false };
            A.NormalAutoFit normalAutoFit9 = new A.NormalAutoFit();

            bodyProperties35.Append(normalAutoFit9);
            A.ListStyle listStyle35 = new A.ListStyle();

            A.Paragraph paragraph47 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties35 = new A.ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Left };

            A.EndParagraphRunProperties endParagraphRunProperties31 = new A.EndParagraphRunProperties(){ Language = "sv-SE", FontSize = 3400, Bold = true, Dirty = false };

            A.SolidFill solidFill121 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex61 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill121.Append(rgbColorModelHex61);
            A.LatinFont latinFont72 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.ComplexScriptFont complexScriptFont72 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            endParagraphRunProperties31.Append(solidFill121);
            endParagraphRunProperties31.Append(latinFont72);
            endParagraphRunProperties31.Append(complexScriptFont72);

            paragraph47.Append(paragraphProperties35);
            paragraph47.Append(endParagraphRunProperties31);

            textBody35.Append(bodyProperties35);
            textBody35.Append(listStyle35);
            textBody35.Append(paragraph47);

            shape35.Append(nonVisualShapeProperties35);
            shape35.Append(shapeProperties39);
            shape35.Append(textBody35);

            Shape shape36 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties36 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties48 = new NonVisualDrawingProperties(){ Id = (UInt32Value)3U, Name = "Subtitle 2" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties36 = new NonVisualShapeDrawingProperties();
            A.ShapeLocks shapeLocks31 = new A.ShapeLocks(){ NoGrouping = true };

            nonVisualShapeDrawingProperties36.Append(shapeLocks31);

            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties48 = new ApplicationNonVisualDrawingProperties();
            PlaceholderShape placeholderShape29 = new PlaceholderShape(){ Type = PlaceholderValues.SubTitle, Index = (UInt32Value)1U };

            applicationNonVisualDrawingProperties48.Append(placeholderShape29);

            nonVisualShapeProperties36.Append(nonVisualDrawingProperties48);
            nonVisualShapeProperties36.Append(nonVisualShapeDrawingProperties36);
            nonVisualShapeProperties36.Append(applicationNonVisualDrawingProperties48);

            ShapeProperties shapeProperties40 = new ShapeProperties();

            A.Transform2D transform2D36 = new A.Transform2D();
            A.Offset offset44 = new A.Offset(){ X = 500034L, Y = 3933626L };
            A.Extents extents44 = new A.Extents(){ Cx = 8143932L, Cy = 2519710L };

            transform2D36.Append(offset44);
            transform2D36.Append(extents44);

            shapeProperties40.Append(transform2D36);

            TextBody textBody36 = new TextBody();

            A.BodyProperties bodyProperties36 = new A.BodyProperties(){ Anchor = A.TextAnchoringTypeValues.Top };
            A.NormalAutoFit normalAutoFit10 = new A.NormalAutoFit();

            bodyProperties36.Append(normalAutoFit10);
            A.ListStyle listStyle36 = new A.ListStyle();

            A.Paragraph paragraph48 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties36 = new A.ParagraphProperties(){ Alignment = A.TextAlignmentTypeValues.Left };

            A.EndParagraphRunProperties endParagraphRunProperties32 = new A.EndParagraphRunProperties(){ Language = "sv-SE", FontSize = 2100, Dirty = false };

            A.SolidFill solidFill122 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex62 = new A.RgbColorModelHex(){ Val = "0084B5" };

            solidFill122.Append(rgbColorModelHex62);
            A.LatinFont latinFont73 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.ComplexScriptFont complexScriptFont73 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            endParagraphRunProperties32.Append(solidFill122);
            endParagraphRunProperties32.Append(latinFont73);
            endParagraphRunProperties32.Append(complexScriptFont73);

            paragraph48.Append(paragraphProperties36);
            paragraph48.Append(endParagraphRunProperties32);

            textBody36.Append(bodyProperties36);
            textBody36.Append(listStyle36);
            textBody36.Append(paragraph48);

            shape36.Append(nonVisualShapeProperties36);
            shape36.Append(shapeProperties40);
            shape36.Append(textBody36);

            Shape shape37 = new Shape();

            NonVisualShapeProperties nonVisualShapeProperties37 = new NonVisualShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties49 = new NonVisualDrawingProperties(){ Id = (UInt32Value)8U, Name = "Title 1" };

            NonVisualShapeDrawingProperties nonVisualShapeDrawingProperties37 = new NonVisualShapeDrawingProperties(){ TextBox = true };
            A.ShapeLocks shapeLocks32 = new A.ShapeLocks();

            nonVisualShapeDrawingProperties37.Append(shapeLocks32);
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties49 = new ApplicationNonVisualDrawingProperties();

            nonVisualShapeProperties37.Append(nonVisualDrawingProperties49);
            nonVisualShapeProperties37.Append(nonVisualShapeDrawingProperties37);
            nonVisualShapeProperties37.Append(applicationNonVisualDrawingProperties49);

            ShapeProperties shapeProperties41 = new ShapeProperties();

            A.Transform2D transform2D37 = new A.Transform2D();
            A.Offset offset45 = new A.Offset(){ X = 5429256L, Y = 357166L };
            A.Extents extents45 = new A.Extents(){ Cx = 3214710L, Cy = 357190L };

            transform2D37.Append(offset45);
            transform2D37.Append(extents45);

            A.PresetGeometry presetGeometry28 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList30 = new A.AdjustValueList();

            presetGeometry28.Append(adjustValueList30);

            shapeProperties41.Append(transform2D37);
            shapeProperties41.Append(presetGeometry28);

            TextBody textBody37 = new TextBody();

            A.BodyProperties bodyProperties37 = new A.BodyProperties(){ Vertical = A.TextVerticalValues.Horizontal, LeftInset = 91440, TopInset = 45720, RightInset = 91440, BottomInset = 45720, RightToLeftColumns = false, Anchor = A.TextAnchoringTypeValues.Top };
            A.NormalAutoFit normalAutoFit11 = new A.NormalAutoFit();

            bodyProperties37.Append(normalAutoFit11);
            A.ListStyle listStyle37 = new A.ListStyle();

            A.Paragraph paragraph49 = new A.Paragraph();

            A.ParagraphProperties paragraphProperties37 = new A.ParagraphProperties(){ LeftMargin = 0, RightMargin = 0, Level = 0, Indent = 0, Alignment = A.TextAlignmentTypeValues.Right, DefaultTabSize = 914400, RightToLeft = false, EastAsianLineBreak = true, FontAlignment = A.TextFontAlignmentValues.Automatic, LatinLineBreak = false, Height = true };

            A.LineSpacing lineSpacing1 = new A.LineSpacing();
            A.SpacingPercent spacingPercent21 = new A.SpacingPercent(){ Val = 100000 };

            lineSpacing1.Append(spacingPercent21);

            A.SpaceBefore spaceBefore21 = new A.SpaceBefore();
            A.SpacingPercent spacingPercent22 = new A.SpacingPercent(){ Val = 0 };

            spaceBefore21.Append(spacingPercent22);

            A.SpaceAfter spaceAfter1 = new A.SpaceAfter();
            A.SpacingPoints spacingPoints1 = new A.SpacingPoints(){ Val = 0 };

            spaceAfter1.Append(spacingPoints1);
            A.BulletColorText bulletColorText1 = new A.BulletColorText();
            A.BulletSizeText bulletSizeText1 = new A.BulletSizeText();
            A.BulletFontText bulletFontText1 = new A.BulletFontText();
            A.NoBullet noBullet16 = new A.NoBullet();
            A.TabStopList tabStopList1 = new A.TabStopList();
            A.DefaultRunProperties defaultRunProperties87 = new A.DefaultRunProperties();

            paragraphProperties37.Append(lineSpacing1);
            paragraphProperties37.Append(spaceBefore21);
            paragraphProperties37.Append(spaceAfter1);
            paragraphProperties37.Append(bulletColorText1);
            paragraphProperties37.Append(bulletSizeText1);
            paragraphProperties37.Append(bulletFontText1);
            paragraphProperties37.Append(noBullet16);
            paragraphProperties37.Append(tabStopList1);
            paragraphProperties37.Append(defaultRunProperties87);

            A.Run run28 = new A.Run();

            A.RunProperties runProperties38 = new A.RunProperties(){ Kumimoji = false, Language = "sv-SE", FontSize = 1200, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Kerning = 1200, Capital = A.TextCapsValues.None, Spacing = 0, NormalizeHeight = false, Baseline = 0 };

            A.Outline outline22 = new A.Outline();
            A.NoFill noFill12 = new A.NoFill();

            outline22.Append(noFill12);

            A.SolidFill solidFill123 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex63 = new A.RgbColorModelHex(){ Val = "DD0068" };

            solidFill123.Append(rgbColorModelHex63);
            A.EffectList effectList10 = new A.EffectList();
            A.UnderlineFollowsText underlineFollowsText1 = new A.UnderlineFollowsText();
            A.UnderlineFillText underlineFillText1 = new A.UnderlineFillText();
            A.LatinFont latinFont74 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.EastAsianFont eastAsianFont63 = new A.EastAsianFont(){ Typeface = "+mj-ea" };
            A.ComplexScriptFont complexScriptFont74 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            runProperties38.Append(outline22);
            runProperties38.Append(solidFill123);
            runProperties38.Append(effectList10);
            runProperties38.Append(underlineFollowsText1);
            runProperties38.Append(underlineFillText1);
            runProperties38.Append(latinFont74);
            runProperties38.Append(eastAsianFont63);
            runProperties38.Append(complexScriptFont74);
            A.Text text38 = new A.Text();
            text38.Text = "Stressa rätt. ";

            run28.Append(runProperties38);
            run28.Append(text38);

            A.Run run29 = new A.Run();

            A.RunProperties runProperties39 = new A.RunProperties(){ Kumimoji = false, Language = "sv-SE", FontSize = 1200, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Kerning = 1200, Capital = A.TextCapsValues.None, Spacing = 0, NormalizeHeight = false, Baseline = 0 };

            A.Outline outline23 = new A.Outline();
            A.NoFill noFill13 = new A.NoFill();

            outline23.Append(noFill13);

            A.SolidFill solidFill124 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex64 = new A.RgbColorModelHex(){ Val = "F18805" };

            solidFill124.Append(rgbColorModelHex64);
            A.EffectList effectList11 = new A.EffectList();
            A.UnderlineFollowsText underlineFollowsText2 = new A.UnderlineFollowsText();
            A.UnderlineFillText underlineFillText2 = new A.UnderlineFillText();
            A.LatinFont latinFont75 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.EastAsianFont eastAsianFont64 = new A.EastAsianFont(){ Typeface = "+mj-ea" };
            A.ComplexScriptFont complexScriptFont75 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            runProperties39.Append(outline23);
            runProperties39.Append(solidFill124);
            runProperties39.Append(effectList11);
            runProperties39.Append(underlineFollowsText2);
            runProperties39.Append(underlineFillText2);
            runProperties39.Append(latinFont75);
            runProperties39.Append(eastAsianFont64);
            runProperties39.Append(complexScriptFont75);
            A.Text text39 = new A.Text();
            text39.Text = "Orka mer. ";

            run29.Append(runProperties39);
            run29.Append(text39);

            A.Run run30 = new A.Run();

            A.RunProperties runProperties40 = new A.RunProperties(){ Kumimoji = false, Language = "sv-SE", FontSize = 1200, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Kerning = 1200, Capital = A.TextCapsValues.None, Spacing = 0, NormalizeHeight = false, Baseline = 0 };

            A.Outline outline24 = new A.Outline();
            A.NoFill noFill14 = new A.NoFill();

            outline24.Append(noFill14);

            A.SolidFill solidFill125 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex65 = new A.RgbColorModelHex(){ Val = "BDD143" };

            solidFill125.Append(rgbColorModelHex65);
            A.EffectList effectList12 = new A.EffectList();
            A.UnderlineFollowsText underlineFollowsText3 = new A.UnderlineFollowsText();
            A.UnderlineFillText underlineFillText3 = new A.UnderlineFillText();
            A.LatinFont latinFont76 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.EastAsianFont eastAsianFont65 = new A.EastAsianFont(){ Typeface = "+mj-ea" };
            A.ComplexScriptFont complexScriptFont76 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            runProperties40.Append(outline24);
            runProperties40.Append(solidFill125);
            runProperties40.Append(effectList12);
            runProperties40.Append(underlineFollowsText3);
            runProperties40.Append(underlineFillText3);
            runProperties40.Append(latinFont76);
            runProperties40.Append(eastAsianFont65);
            runProperties40.Append(complexScriptFont76);
            A.Text text40 = new A.Text();
            text40.Text = "Njut av";

            run30.Append(runProperties40);
            run30.Append(text40);

            A.Run run31 = new A.Run();

            A.RunProperties runProperties41 = new A.RunProperties(){ Kumimoji = false, Language = "sv-SE", FontSize = 1200, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Kerning = 1200, Capital = A.TextCapsValues.None, Spacing = 0, NormalizeHeight = false };

            A.Outline outline25 = new A.Outline();
            A.NoFill noFill15 = new A.NoFill();

            outline25.Append(noFill15);

            A.SolidFill solidFill126 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex66 = new A.RgbColorModelHex(){ Val = "BDD143" };

            solidFill126.Append(rgbColorModelHex66);
            A.EffectList effectList13 = new A.EffectList();
            A.UnderlineFollowsText underlineFollowsText4 = new A.UnderlineFollowsText();
            A.UnderlineFillText underlineFillText4 = new A.UnderlineFillText();
            A.LatinFont latinFont77 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.EastAsianFont eastAsianFont66 = new A.EastAsianFont(){ Typeface = "+mj-ea" };
            A.ComplexScriptFont complexScriptFont77 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            runProperties41.Append(outline25);
            runProperties41.Append(solidFill126);
            runProperties41.Append(effectList13);
            runProperties41.Append(underlineFollowsText4);
            runProperties41.Append(underlineFillText4);
            runProperties41.Append(latinFont77);
            runProperties41.Append(eastAsianFont66);
            runProperties41.Append(complexScriptFont77);
            A.Text text41 = new A.Text();
            text41.Text = " livet.";

            run31.Append(runProperties41);
            run31.Append(text41);

            A.EndParagraphRunProperties endParagraphRunProperties33 = new A.EndParagraphRunProperties(){ Kumimoji = false, Language = "sv-SE", FontSize = 1200, Italic = false, Underline = A.TextUnderlineValues.None, Strike = A.TextStrikeValues.NoStrike, Kerning = 1200, Capital = A.TextCapsValues.None, Spacing = 0, NormalizeHeight = false, Baseline = 0 };

            A.Outline outline26 = new A.Outline();
            A.NoFill noFill16 = new A.NoFill();

            outline26.Append(noFill16);

            A.SolidFill solidFill127 = new A.SolidFill();
            A.RgbColorModelHex rgbColorModelHex67 = new A.RgbColorModelHex(){ Val = "BDD143" };

            solidFill127.Append(rgbColorModelHex67);
            A.EffectList effectList14 = new A.EffectList();
            A.UnderlineFollowsText underlineFollowsText5 = new A.UnderlineFollowsText();
            A.UnderlineFillText underlineFillText5 = new A.UnderlineFillText();
            A.LatinFont latinFont78 = new A.LatinFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };
            A.EastAsianFont eastAsianFont67 = new A.EastAsianFont(){ Typeface = "+mj-ea" };
            A.ComplexScriptFont complexScriptFont78 = new A.ComplexScriptFont(){ Typeface = "Arial", PitchFamily = 34, CharacterSet = 0 };

            endParagraphRunProperties33.Append(outline26);
            endParagraphRunProperties33.Append(solidFill127);
            endParagraphRunProperties33.Append(effectList14);
            endParagraphRunProperties33.Append(underlineFollowsText5);
            endParagraphRunProperties33.Append(underlineFillText5);
            endParagraphRunProperties33.Append(latinFont78);
            endParagraphRunProperties33.Append(eastAsianFont67);
            endParagraphRunProperties33.Append(complexScriptFont78);

            paragraph49.Append(paragraphProperties37);
            paragraph49.Append(run28);
            paragraph49.Append(run29);
            paragraph49.Append(run30);
            paragraph49.Append(run31);
            paragraph49.Append(endParagraphRunProperties33);

            textBody37.Append(bodyProperties37);
            textBody37.Append(listStyle37);
            textBody37.Append(paragraph49);

            shape37.Append(nonVisualShapeProperties37);
            shape37.Append(shapeProperties41);
            shape37.Append(textBody37);

            Picture picture5 = new Picture();

            NonVisualPictureProperties nonVisualPictureProperties5 = new NonVisualPictureProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties50 = new NonVisualDrawingProperties(){ Id = (UInt32Value)1027U, Name = "Picture 3", Description = "C:\\Documents and Settings\\Antti Salmi\\Desktop\\hw_ppt_imgs\\hw_logo.png" };

            NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties5 = new NonVisualPictureDrawingProperties();
            A.PictureLocks pictureLocks5 = new A.PictureLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

            nonVisualPictureDrawingProperties5.Append(pictureLocks5);
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties50 = new ApplicationNonVisualDrawingProperties();

            nonVisualPictureProperties5.Append(nonVisualDrawingProperties50);
            nonVisualPictureProperties5.Append(nonVisualPictureDrawingProperties5);
            nonVisualPictureProperties5.Append(applicationNonVisualDrawingProperties50);

            BlipFill blipFill5 = new BlipFill();
            A.Blip blip5 = new A.Blip(){ Embed = "rId2" };
            A.SourceRectangle sourceRectangle4 = new A.SourceRectangle();

            A.Stretch stretch5 = new A.Stretch();
            A.FillRectangle fillRectangle5 = new A.FillRectangle();

            stretch5.Append(fillRectangle5);

            blipFill5.Append(blip5);
            blipFill5.Append(sourceRectangle4);
            blipFill5.Append(stretch5);

            ShapeProperties shapeProperties42 = new ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D38 = new A.Transform2D();
            A.Offset offset46 = new A.Offset(){ X = 571472L, Y = 357166L };
            A.Extents extents46 = new A.Extents(){ Cx = 1047750L, Cy = 714375L };

            transform2D38.Append(offset46);
            transform2D38.Append(extents46);

            A.PresetGeometry presetGeometry29 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList31 = new A.AdjustValueList();

            presetGeometry29.Append(adjustValueList31);
            A.NoFill noFill17 = new A.NoFill();

            shapeProperties42.Append(transform2D38);
            shapeProperties42.Append(presetGeometry29);
            shapeProperties42.Append(noFill17);

            picture5.Append(nonVisualPictureProperties5);
            picture5.Append(blipFill5);
            picture5.Append(shapeProperties42);

            Picture picture6 = new Picture();

            NonVisualPictureProperties nonVisualPictureProperties6 = new NonVisualPictureProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties51 = new NonVisualDrawingProperties(){ Id = (UInt32Value)7U, Name = "Picture 2", Description = "C:\\Documents and Settings\\Antti Salmi\\Desktop\\hw_ppt_imgs\\figures.png" };

            NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties6 = new NonVisualPictureDrawingProperties();
            A.PictureLocks pictureLocks6 = new A.PictureLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

            nonVisualPictureDrawingProperties6.Append(pictureLocks6);
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties51 = new ApplicationNonVisualDrawingProperties();

            nonVisualPictureProperties6.Append(nonVisualDrawingProperties51);
            nonVisualPictureProperties6.Append(nonVisualPictureDrawingProperties6);
            nonVisualPictureProperties6.Append(applicationNonVisualDrawingProperties51);

            BlipFill blipFill6 = new BlipFill();
            A.Blip blip6 = new A.Blip(){ Embed = "rId3" };
            A.SourceRectangle sourceRectangle5 = new A.SourceRectangle();

            A.Stretch stretch6 = new A.Stretch();
            A.FillRectangle fillRectangle6 = new A.FillRectangle();

            stretch6.Append(fillRectangle6);

            blipFill6.Append(blip6);
            blipFill6.Append(sourceRectangle5);
            blipFill6.Append(stretch6);

            ShapeProperties shapeProperties43 = new ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D39 = new A.Transform2D();
            A.Offset offset47 = new A.Offset(){ X = 571472L, Y = 1285860L };
            A.Extents extents47 = new A.Extents(){ Cx = 8001000L, Cy = 1104900L };

            transform2D39.Append(offset47);
            transform2D39.Append(extents47);

            A.PresetGeometry presetGeometry30 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList32 = new A.AdjustValueList();

            presetGeometry30.Append(adjustValueList32);
            A.NoFill noFill18 = new A.NoFill();

            shapeProperties43.Append(transform2D39);
            shapeProperties43.Append(presetGeometry30);
            shapeProperties43.Append(noFill18);

            picture6.Append(nonVisualPictureProperties6);
            picture6.Append(blipFill6);
            picture6.Append(shapeProperties43);

            shapeTree8.Append(nonVisualGroupShapeProperties8);
            shapeTree8.Append(groupShapeProperties8);
            shapeTree8.Append(shape35);
            shapeTree8.Append(shape36);
            shapeTree8.Append(shape37);
            shapeTree8.Append(picture5);
            shapeTree8.Append(picture6);

            commonSlideData8.Append(shapeTree8);

            ColorMapOverride colorMapOverride5 = new ColorMapOverride();
            A.MasterColorMapping masterColorMapping5 = new A.MasterColorMapping();

            colorMapOverride5.Append(masterColorMapping5);

            slide2.Append(commonSlideData8);
            slide2.Append(colorMapOverride5);

            slidePart2.Slide = slide2;
        }

        // Generates content of imagePart4.
        private void GenerateImagePart4Content(ImagePart imagePart4)
        {
            System.IO.Stream data = GetBinaryDataStream(imagePart4Data);
            imagePart4.FeedData(data);
            data.Close();
        }

        // Generates content of extendedFilePropertiesPart1.
        private void GenerateExtendedFilePropertiesPart1Content(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
        {
            Ap.Properties properties1 = new Ap.Properties();
            properties1.AddNamespaceDeclaration("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
            Ap.Template template1 = new Ap.Template();
            template1.Text = "WebbQPS";
            Ap.TotalTime totalTime1 = new Ap.TotalTime();
            totalTime1.Text = "136";
            Ap.Words words1 = new Ap.Words();
            words1.Text = "11";
            Ap.Application application1 = new Ap.Application();
            application1.Text = "Microsoft Macintosh PowerPoint";
            Ap.PresentationFormat presentationFormat1 = new Ap.PresentationFormat();
            presentationFormat1.Text = "On-screen Show (4:3)";
            Ap.Paragraphs paragraphs1 = new Ap.Paragraphs();
            paragraphs1.Text = "2";
            Ap.Slides slides1 = new Ap.Slides();
            slides1.Text = "2";
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
            vTInt321.Text = "2";

            variant2.Append(vTInt321);

            Vt.Variant variant3 = new Vt.Variant();
            Vt.VTLPSTR vTLPSTR2 = new Vt.VTLPSTR();
            vTLPSTR2.Text = "Slide Titles";

            variant3.Append(vTLPSTR2);

            Vt.Variant variant4 = new Vt.Variant();
            Vt.VTInt32 vTInt322 = new Vt.VTInt32();
            vTInt322.Text = "2";

            variant4.Append(vTInt322);

            vTVector1.Append(variant1);
            vTVector1.Append(variant2);
            vTVector1.Append(variant3);
            vTVector1.Append(variant4);

            headingPairs1.Append(vTVector1);

            Ap.TitlesOfParts titlesOfParts1 = new Ap.TitlesOfParts();

            Vt.VTVector vTVector2 = new Vt.VTVector(){ BaseType = Vt.VectorBaseValues.Lpstr, Size = (UInt32Value)4U };
            Vt.VTLPSTR vTLPSTR3 = new Vt.VTLPSTR();
            vTLPSTR3.Text = "WebbQPS";
            Vt.VTLPSTR vTLPSTR4 = new Vt.VTLPSTR();
            vTLPSTR4.Text = "1_Anpassad formgivning";
            Vt.VTLPSTR vTLPSTR5 = new Vt.VTLPSTR();
            vTLPSTR5.Text = "Slide 1";
            Vt.VTLPSTR vTLPSTR6 = new Vt.VTLPSTR();
            vTLPSTR6.Text = "Slide 2";

            vTVector2.Append(vTLPSTR3);
            vTVector2.Append(vTLPSTR4);
            vTVector2.Append(vTLPSTR5);
            vTVector2.Append(vTLPSTR6);

            titlesOfParts1.Append(vTVector2);
            Ap.Company company1 = new Ap.Company();
            company1.Text = "Karolinska Institutet, Fyfa";
            Ap.LinksUpToDate linksUpToDate1 = new Ap.LinksUpToDate();
            linksUpToDate1.Text = "false";
            Ap.SharedDocument sharedDocument1 = new Ap.SharedDocument();
            sharedDocument1.Text = "false";
            Ap.HyperlinksChanged hyperlinksChanged1 = new Ap.HyperlinksChanged();
            hyperlinksChanged1.Text = "false";
            Ap.ApplicationVersion applicationVersion1 = new Ap.ApplicationVersion();
            applicationVersion1.Text = "12.0000";

            properties1.Append(template1);
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
            properties1.Append(company1);
            properties1.Append(linksUpToDate1);
            properties1.Append(sharedDocument1);
            properties1.Append(hyperlinksChanged1);
            properties1.Append(applicationVersion1);

            extendedFilePropertiesPart1.Properties = properties1;
        }

        private void SetPackageProperties(OpenXmlPackage document)
        {
            document.PackageProperties.Creator = "";
            document.PackageProperties.Title = "Title_text";
            document.PackageProperties.Revision = "31";
            document.PackageProperties.Created = System.Xml.XmlConvert.ToDateTime("2013-06-12T11:42:54Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
            document.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime("2016-10-21T14:59:31Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
            document.PackageProperties.LastModifiedBy = "Ian";
            document.PackageProperties.LastPrinted = System.Xml.XmlConvert.ToDateTime("2011-08-17T06:16:56Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
        }

        #region Binary Data
        private string thumbnailPart1Data = "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YEBXS48VStLHlJnkVmB/aGiurD47GYWM4YbFV6EJ6yjSqzhFu1r2i0ua2nMrStpex+kcFeMPit4cYHMMs4B8ReM+DsuzWTqZhgOHeIs0ynCYmvKnGi8VLD4PE0qUMb7KEKSxtOMMXGnGNONZQikuW8E+CPB/w28J6B4D8AeGtF8HeDPC2mwaR4d8M+HdPttK0bR9OtwfKtbGxtI44IU3M8srBTJPPJLcTvJPLJI3U0UVzNuTcpNuTbbbbbbbu229W29W3q2fnuIxGIxmIr4vF162KxWKrVcRicTiKs6+IxGIrzlVrV69arKVStWrVJSqVatSUp1JylOcnJtsooopGIUUUUAFFFFABRRRQAUUUUAUbrS9NvpYJ73T7G8ntSWtZrq0guJbdjjJgkljd4iSASUK5IB6gVeooqI0qcJVJwpwhOq4yqzjCMZVJRioxdSSSc3GKUYuTbUUktFYAoooqwCiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooA//ZA+BWZwNxAgAAHgAAAMEAAADAAAYAgIFJBQAAAABM+z4yAAAAAAAAAAAAAAAAoF9nA5hcZwNMAgAAUAAAAB4AAAAAAAIAIP0NBQAAAAAkd/4xAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=";

        private string imagePart1Data = "iVBORw0KGgoAAAANSUhEUgAAAfgAAACgCAYAAADggn5RAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjExNTg3OUM1QzM3MzExRTA5MkRDRkE4NUJGQ0U2Q0I4IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjExNTg3OUM2QzM3MzExRTA5MkRDRkE4NUJGQ0U2Q0I4Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6MTE1ODc5QzNDMzczMTFFMDkyRENGQTg1QkZDRTZDQjgiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6MTE1ODc5QzRDMzczMTFFMDkyRENGQTg1QkZDRTZDQjgiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz7y4i1mAAAllUlEQVR42uydC3AV9b3H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";

        private string imagePart2Data = "iVBORw0KGgoAAAANSUhEUgAAANwAAACWCAYAAAC1meaLAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOkYzMzVBMzc3QzM2RTExRTBCREYzRURFQ0RCRDIyRTk2IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOkYzMzVBMzc4QzM2RTExRTBCREYzRURFQ0RCRDIyRTk2Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6RjMzNUEzNzVDMzZFMTFFMEJERjNFREVDREJEMjJFOTYiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6RjMzNUEzNzZDMzZFMTFFMEJERjNFREVDREJEMjJFOTYiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz41jbLkAAARx0lEQVR42uxdS1LjvBYW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";

        private string imagePart3Data = "R0lGODlhfwO4AfcAAAAAAAAASAAAdEgAAHQAAEgASEgAdHQASHQAdEh0dABInAB0v0hInHRInEic4HS//5xIAL90AJxISJxIdL+/dOCcSOCcdP+/dJzg/7////+oqMz/u+DgnP/gnP/+vv//v8zMzOD/////4P///wAAAAAAAAAAAAAAAAAAAAAAMwAAZgAAmQAAzAAA/wAzAAAzMwAzZgAzmQAzzAAz/wBmAABmMwBmZgBmmQBmzABm/wCZAACZMwCZZgCZmQCZzACZ/wDMAADMMwDMZgDMmQDMzADM/wD/AAD/MwD/ZgD/mQD/zAD//zMAADMAMzMAZjMAmTMAzDMA/zMzADMzMzMzZjMzmTMzzDMz/zNmADNmMzNmZjNmmTNmzDNm/zOZADOZMzOZZjOZmTOZzDOZ/zPMADPMMzPMZjPMmTPMzDPM/zP/ADP/MzP/ZjP/mTP/zDP//2YAAGYAM2YAZmYAmWYAzGYA/2YzAGYzM2YzZmYzmWYzzGYz/2ZmAGZmM2ZmZmZmmWZmzGZm/2aZAGaZM2aZZmaZmWaZzGaZ/2bMAGbMM2bMZmbMmWbMzGbM/2b/AGb/M2b/Zmb/mWb/zGb//5kAAJkAM5kAZpkAmZkAzJkA/5kzAJkzM5kzZpkzmZkzzJkz/5lmAJlmM5lmZplmmZlmzJlm/5mZAJmZM5mZZpmZmZmZzJmZ/5nMAJnMM5nMZpnMmZnMzJnM/5n/AJn/M5n/Zpn/mZn/zJn//8wAAMwAM8wAZswAmcwAzMwA/8wzAMwzM8wzZswzmcwzzMwz/8xmAMxmM8xmZsxmmcxmzMxm/8yZAMyZM8yZZsyZmcyZzMyZ/8zMAMzMM8zMZszMmczMzMzM/8z/AMz/M8z/Zsz/mcz/zMz///8AAP8AM/8AZv8Amf8AzP8A//8zAP8zM/8zZv8zmf8zzP8z//9mAP9mM/9mZv9mmf9mzP9m//+ZAP+ZM/+ZZv+Zmf+ZzP+Z///MAP/MM//MZv/Mmf/MzP/M////AP//M///Zv//mf//zAAAACH5BAEAACQALAAAAAB/A7gBAAisAEcIHEiwoMGDCBMqXMiwocOHECNKnEixosWLGDNq3Mixo8ePIEOKHEmypMmTKFOqXMmypcuXMGPKnEmzps2bOHPq3Mmzp8+fQIMKHUq0qNGjSJMqXcq0qdOnUKNKnUq1qtWrWLNq3cq1q9evYMOKHUu2rNmzaNOqXcu2rdu3cOPKnUu3rt27ePPq3cu3r9+/gAMLHky4sOHDiBMrXsy4sePHkCNLnky5suXLmEcza97MubPnz6BDix5NurTp06hTq17NurXr17Bjy55Nu7bt27hz697Nu7fv38CDCx9OvLjx48iTK1/OvLnz59CjS59Ovbr16zfYs2vfzr279+/gw4sfT768+fPo06tfz769+/fw48ufT7++/fv48+vfz7+///8ABijggAQWaOCBUggmqOCCPgHA4IMQWgXAhBFWaOFSDmZ44YYcAuXgCB9+2OGIJN4kYokopgjTiSq26KJJLL4o44wcxUjjjThKZGOOPPaI0I4+BukjkEIWeSORRib/6SKSSjZZIpNORrkhlFJWCSGVVmYp3wcNhCDQBRM+8GWYP2pp5n4VAKCAlyJIgEEHDITQ5ptxHoTlmXimB6eXe87Zp5t25ikofXuO8CedcgJq0J2DNgpeoYceaicIlFZq6aWYZqrpppx26umnoIYq6qiklmrqqaimquqqrLbq6quwxirrrLTWauutuOaq60yQxumnr4oWBMAGxBZr7LHIJqvsssw26+yz0EYr7bTUVmvttdhmq+223Hbr7bfghivuuOSWa+656KabLk2F/pooonaqK++89NZr77345qvvvvz26++/AAdc7ExpkgkmAGKOcHDCiwrs8MMQRyzxxBRXFWzxxRhnjGHGHHfs8ccghyzyyCSXfIfsxianrPLKLLfs8sswS4xyzDTXbPPNOOes88gz7+zzz0AHLfTQRDvbc9FIJ6300kw3/fDRTkct9dRUV231slBfrfXWXHftNctZfy322GSXbTa+YZ+t9tpst+02s2m/LffcdNc9ddx256333nzHjHffgAcu+OAT/0344Ygnrvi5hi/u+OOQR96rbOOSV2755YNTjvnmnHfOdk4Lj4nwj56Xbvrpa+PUgQEZrJ6Bu/GiLvvstFON0wULjODunLHX7vvvwP+sOusjRPCApA0Hr/zyzIONUwQTBoAo74vqav312Gev/fbcd+/99+CHL/745JdvqU4iTNA6sBj03vz78Mdv8U4V5A578vLnr//+/OaU5pqiY5iw+EfAAhqQcUoZ1gEXyMAGWktzDoygBPkHwQla8ILLWasgBjfIQdRpsIMgDKHlPijCEpoQcSQ8oQpXqLcU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";

        private string imagePart4Data = "iVBORw0KGgoAAAANSUhEUgAABpAAAADoCAYAAADogqdXAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjdDNDkxNTQzQzM3MTExRTBCOEI5Qjg2MzdGNjU0RDhBIiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjdDNDkxNTQ0QzM3MTExRTBCOEI5Qjg2MzdGNjU0RDhBIj4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6N0M0OTE1NDFDMzcxMTFFMEI4QjlCODYzN0Y2NTREOEEiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6N0M0OTE1NDJDMzcxMTFFMEI4QjlCODYzN0Y2NTREOEEiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz7fCqmXAABlr0lEQVR42uzdDWxc9Z3v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";

        private System.IO.Stream GetBinaryDataStream(string base64String)
        {
            return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
        }

        #endregion

    }

}
