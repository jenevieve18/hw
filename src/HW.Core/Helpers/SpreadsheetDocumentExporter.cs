using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using DocumentFormat.OpenXml.Packaging;
using HW.Core.Models;
using HW.Core.Services;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;

namespace HW.Core.Helpers
{
	public class SpreadsheetDocumentExporter : AbstractExporter
	{
		ReportPart r;
		IList<ReportPartLanguage> parts;
		ReportService service;
		
		public SpreadsheetDocumentExporter(ReportPart r)
		{
			this.r = r;
		}
		
		public SpreadsheetDocumentExporter(ReportService service, IList<ReportPartLanguage> parts)
		{
			this.service = service;
			this.parts = parts;
		}
		
		public override string Type {
			get { return "application/octet-stream"; }
		}
		
		public override string GetContentDisposition(string file)
		{
			return string.Format("attachment;filename=HealthWatch {0} {1}.xlsx;", file, DateTime.Now.ToString("yyyyMMdd"));
		}
		
		public override string ContentDisposition2 {
			get { return string.Format("attachment;filename=HealthWatch Survey {0}.xlsx;", DateTime.Now.ToString("yyyyMMdd")); }
		}
		
		public override object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path)
		{
			MemoryStream output = new MemoryStream();
			using (SpreadsheetDocument  package = SpreadsheetDocument.Create(output, SpreadsheetDocumentType.Workbook)) {
				string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot);
				CreateParts(package, r.CurrentLanguage, url);
			}
			return output;
		}
		
		public override object Export2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path)
		{
			throw new NotImplementedException();
		}
		
		private void CreateParts(SpreadsheetDocument document, ReportPartLanguage r, string url)
		{
			ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
			GenerateExtendedFilePropertiesPart1Content(extendedFilePropertiesPart1);

			WorkbookPart workbookPart1 = document.AddWorkbookPart();
			GenerateWorkbookPart1Content(workbookPart1);

			WorksheetPart worksheetPart1 = workbookPart1.AddNewPart<WorksheetPart>("rId3");
			GenerateWorksheetPart1Content(worksheetPart1);

			WorksheetPart worksheetPart2 = workbookPart1.AddNewPart<WorksheetPart>("rId2");
			GenerateWorksheetPart2Content(worksheetPart2);

			WorksheetPart worksheetPart3 = workbookPart1.AddNewPart<WorksheetPart>("rId1");
			GenerateWorksheetPart3Content(worksheetPart3);

			DrawingsPart drawingsPart1 = worksheetPart3.AddNewPart<DrawingsPart>("rId1");
			GenerateDrawingsPart1Content(drawingsPart1);

			ImagePart imagePart1 = drawingsPart1.AddNewPart<ImagePart>("image/gif", "rId1");
			GenerateImagePart1Content(imagePart1, url);

			WorkbookStylesPart workbookStylesPart1 = workbookPart1.AddNewPart<WorkbookStylesPart>("rId5");
			GenerateWorkbookStylesPart1Content(workbookStylesPart1);

			ThemePart themePart1 = workbookPart1.AddNewPart<ThemePart>("rId4");
			GenerateThemePart1Content(themePart1);

			SetPackageProperties(document);
		}

		// Generates content of extendedFilePropertiesPart1.
		private void GenerateExtendedFilePropertiesPart1Content(ExtendedFilePropertiesPart extendedFilePropertiesPart1)
		{
			Ap.Properties properties1 = new Ap.Properties();
			properties1.AddNamespaceDeclaration("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
			Ap.Application application1 = new Ap.Application();
			application1.Text = "Microsoft Excel";
			Ap.DocumentSecurity documentSecurity1 = new Ap.DocumentSecurity();
			documentSecurity1.Text = "0";
			Ap.ScaleCrop scaleCrop1 = new Ap.ScaleCrop();
			scaleCrop1.Text = "false";

			Ap.HeadingPairs headingPairs1 = new Ap.HeadingPairs();

			Vt.VTVector vTVector1 = new Vt.VTVector(){ BaseType = Vt.VectorBaseValues.Variant, Size = (UInt32Value)2U };

			Vt.Variant variant1 = new Vt.Variant();
			Vt.VTLPSTR vTLPSTR1 = new Vt.VTLPSTR();
			vTLPSTR1.Text = "Worksheets";

			variant1.Append(vTLPSTR1);

			Vt.Variant variant2 = new Vt.Variant();
			Vt.VTInt32 vTInt321 = new Vt.VTInt32();
			vTInt321.Text = "3";

			variant2.Append(vTInt321);

			vTVector1.Append(variant1);
			vTVector1.Append(variant2);

			headingPairs1.Append(vTVector1);

			Ap.TitlesOfParts titlesOfParts1 = new Ap.TitlesOfParts();

			Vt.VTVector vTVector2 = new Vt.VTVector(){ BaseType = Vt.VectorBaseValues.Lpstr, Size = (UInt32Value)3U };
			Vt.VTLPSTR vTLPSTR2 = new Vt.VTLPSTR();
			vTLPSTR2.Text = "Sheet1";
			Vt.VTLPSTR vTLPSTR3 = new Vt.VTLPSTR();
			vTLPSTR3.Text = "Sheet2";
			Vt.VTLPSTR vTLPSTR4 = new Vt.VTLPSTR();
			vTLPSTR4.Text = "Sheet3";

			vTVector2.Append(vTLPSTR2);
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

			properties1.Append(application1);
			properties1.Append(documentSecurity1);
			properties1.Append(scaleCrop1);
			properties1.Append(headingPairs1);
			properties1.Append(titlesOfParts1);
			properties1.Append(linksUpToDate1);
			properties1.Append(sharedDocument1);
			properties1.Append(hyperlinksChanged1);
			properties1.Append(applicationVersion1);

			extendedFilePropertiesPart1.Properties = properties1;
		}

		// Generates content of workbookPart1.
		private void GenerateWorkbookPart1Content(WorkbookPart workbookPart1)
		{
			Workbook workbook1 = new Workbook();
			workbook1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			FileVersion fileVersion1 = new FileVersion(){ ApplicationName = "xl", LastEdited = "4", LowestEdited = "4", BuildVersion = "4505" };
			WorkbookProperties workbookProperties1 = new WorkbookProperties(){ DefaultThemeVersion = (UInt32Value)124226U };

			BookViews bookViews1 = new BookViews();
			WorkbookView workbookView1 = new WorkbookView(){ XWindow = 120, YWindow = 30, WindowWidth = (UInt32Value)15255U, WindowHeight = (UInt32Value)5640U };

			bookViews1.Append(workbookView1);

			Sheets sheets1 = new Sheets();
			Sheet sheet1 = new Sheet(){ Name = "Sheet1", SheetId = (UInt32Value)1U, Id = "rId1" };
			Sheet sheet2 = new Sheet(){ Name = "Sheet2", SheetId = (UInt32Value)2U, Id = "rId2" };
			Sheet sheet3 = new Sheet(){ Name = "Sheet3", SheetId = (UInt32Value)3U, Id = "rId3" };

			sheets1.Append(sheet1);
			sheets1.Append(sheet2);
			sheets1.Append(sheet3);
			CalculationProperties calculationProperties1 = new CalculationProperties(){ CalculationId = (UInt32Value)124519U };

			workbook1.Append(fileVersion1);
			workbook1.Append(workbookProperties1);
			workbook1.Append(bookViews1);
			workbook1.Append(sheets1);
			workbook1.Append(calculationProperties1);

			workbookPart1.Workbook = workbook1;
		}

		// Generates content of worksheetPart1.
		private void GenerateWorksheetPart1Content(WorksheetPart worksheetPart1)
		{
			Worksheet worksheet1 = new Worksheet();
			worksheet1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			SheetDimension sheetDimension1 = new SheetDimension(){ Reference = "A1" };

			SheetViews sheetViews1 = new SheetViews();
			SheetView sheetView1 = new SheetView(){ WorkbookViewId = (UInt32Value)0U };

			sheetViews1.Append(sheetView1);
			SheetFormatProperties sheetFormatProperties1 = new SheetFormatProperties(){ DefaultRowHeight = 15D };
			SheetData sheetData1 = new SheetData();
			PageMargins pageMargins1 = new PageMargins(){ Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };

			worksheet1.Append(sheetDimension1);
			worksheet1.Append(sheetViews1);
			worksheet1.Append(sheetFormatProperties1);
			worksheet1.Append(sheetData1);
			worksheet1.Append(pageMargins1);

			worksheetPart1.Worksheet = worksheet1;
		}

		// Generates content of worksheetPart2.
		private void GenerateWorksheetPart2Content(WorksheetPart worksheetPart2)
		{
			Worksheet worksheet2 = new Worksheet();
			worksheet2.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			SheetDimension sheetDimension2 = new SheetDimension(){ Reference = "A1" };

			SheetViews sheetViews2 = new SheetViews();
			SheetView sheetView2 = new SheetView(){ WorkbookViewId = (UInt32Value)0U };

			sheetViews2.Append(sheetView2);
			SheetFormatProperties sheetFormatProperties2 = new SheetFormatProperties(){ DefaultRowHeight = 15D };
			SheetData sheetData2 = new SheetData();
			PageMargins pageMargins2 = new PageMargins(){ Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };

			worksheet2.Append(sheetDimension2);
			worksheet2.Append(sheetViews2);
			worksheet2.Append(sheetFormatProperties2);
			worksheet2.Append(sheetData2);
			worksheet2.Append(pageMargins2);

			worksheetPart2.Worksheet = worksheet2;
		}

		// Generates content of worksheetPart3.
		private void GenerateWorksheetPart3Content(WorksheetPart worksheetPart3)
		{
			Worksheet worksheet3 = new Worksheet();
			worksheet3.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			SheetDimension sheetDimension3 = new SheetDimension(){ Reference = "A1" };

			SheetViews sheetViews3 = new SheetViews();

			SheetView sheetView3 = new SheetView(){ TabSelected = true, ZoomScale = (UInt32Value)70U, ZoomScaleNormal = (UInt32Value)70U, WorkbookViewId = (UInt32Value)0U };
			Selection selection1 = new Selection(){ ActiveCell = "Q15", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "Q15" } };

			sheetView3.Append(selection1);

			sheetViews3.Append(sheetView3);
			SheetFormatProperties sheetFormatProperties3 = new SheetFormatProperties(){ DefaultRowHeight = 15D };
			SheetData sheetData3 = new SheetData();
			PageMargins pageMargins3 = new PageMargins(){ Left = 0.7D, Right = 0.7D, Top = 0.75D, Bottom = 0.75D, Header = 0.3D, Footer = 0.3D };
			Drawing drawing1 = new Drawing(){ Id = "rId1" };

			worksheet3.Append(sheetDimension3);
			worksheet3.Append(sheetViews3);
			worksheet3.Append(sheetFormatProperties3);
			worksheet3.Append(sheetData3);
			worksheet3.Append(pageMargins3);
			worksheet3.Append(drawing1);

			worksheetPart3.Worksheet = worksheet3;
		}

		// Generates content of drawingsPart1.
		private void GenerateDrawingsPart1Content(DrawingsPart drawingsPart1)
		{
			Xdr.WorksheetDrawing worksheetDrawing1 = new Xdr.WorksheetDrawing();
			worksheetDrawing1.AddNamespaceDeclaration("xdr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
			worksheetDrawing1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");

			Xdr.TwoCellAnchor twoCellAnchor1 = new Xdr.TwoCellAnchor(){ EditAs = Xdr.EditAsValues.OneCell };

			Xdr.FromMarker fromMarker1 = new Xdr.FromMarker();
			Xdr.ColumnId columnId1 = new Xdr.ColumnId();
			columnId1.Text = "1";
			Xdr.ColumnOffset columnOffset1 = new Xdr.ColumnOffset();
			columnOffset1.Text = "0";
			Xdr.RowId rowId1 = new Xdr.RowId();
			rowId1.Text = "2";
			Xdr.RowOffset rowOffset1 = new Xdr.RowOffset();
			rowOffset1.Text = "0";

			fromMarker1.Append(columnId1);
			fromMarker1.Append(columnOffset1);
			fromMarker1.Append(rowId1);
			fromMarker1.Append(rowOffset1);

			Xdr.ToMarker toMarker1 = new Xdr.ToMarker();
			Xdr.ColumnId columnId2 = new Xdr.ColumnId();
			columnId2.Text = "14";
			Xdr.ColumnOffset columnOffset2 = new Xdr.ColumnOffset();
			columnOffset2.Text = "600075";
			Xdr.RowId rowId2 = new Xdr.RowId();
			rowId2.Text = "24";
			Xdr.RowOffset rowOffset2 = new Xdr.RowOffset();
			rowOffset2.Text = "0";

			toMarker1.Append(columnId2);
			toMarker1.Append(columnOffset2);
			toMarker1.Append(rowId2);
			toMarker1.Append(rowOffset2);

			Xdr.Picture picture1 = new Xdr.Picture();

			Xdr.NonVisualPictureProperties nonVisualPictureProperties1 = new Xdr.NonVisualPictureProperties();
			Xdr.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Xdr.NonVisualDrawingProperties(){ Id = (UInt32Value)1025U, Name = "Picture 1", Description = "http://dev-grp.healthwatch.se/reportImage.aspx?LangID=1&FY=2012&TY=2013&SAID=514&SID=83&GB=7&RPID=14&RPLID=115&PRUID=2643&GRPNG=2&GID=0,923&PLOT=Export" };

			Xdr.NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties1 = new Xdr.NonVisualPictureDrawingProperties();
			A.PictureLocks pictureLocks1 = new A.PictureLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

			nonVisualPictureDrawingProperties1.Append(pictureLocks1);

			nonVisualPictureProperties1.Append(nonVisualDrawingProperties1);
			nonVisualPictureProperties1.Append(nonVisualPictureDrawingProperties1);

			Xdr.BlipFill blipFill1 = new Xdr.BlipFill();

			A.Blip blip1 = new A.Blip(){ Embed = "rId1" };
			blip1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			A.SourceRectangle sourceRectangle1 = new A.SourceRectangle();

			A.Stretch stretch1 = new A.Stretch();
			A.FillRectangle fillRectangle1 = new A.FillRectangle();

			stretch1.Append(fillRectangle1);

			blipFill1.Append(blip1);
			blipFill1.Append(sourceRectangle1);
			blipFill1.Append(stretch1);

			Xdr.ShapeProperties shapeProperties1 = new Xdr.ShapeProperties(){ BlackWhiteMode = A.BlackWhiteModeValues.Auto };

			A.Transform2D transform2D1 = new A.Transform2D();
			A.Offset offset1 = new A.Offset(){ X = 609600L, Y = 381000L };
			A.Extents extents1 = new A.Extents(){ Cx = 8524875L, Cy = 4191000L };

			transform2D1.Append(offset1);
			transform2D1.Append(extents1);

			A.PresetGeometry presetGeometry1 = new A.PresetGeometry(){ Preset = A.ShapeTypeValues.Rectangle };
			A.AdjustValueList adjustValueList1 = new A.AdjustValueList();

			presetGeometry1.Append(adjustValueList1);
			A.NoFill noFill1 = new A.NoFill();

			shapeProperties1.Append(transform2D1);
			shapeProperties1.Append(presetGeometry1);
			shapeProperties1.Append(noFill1);

			picture1.Append(nonVisualPictureProperties1);
			picture1.Append(blipFill1);
			picture1.Append(shapeProperties1);
			Xdr.ClientData clientData1 = new Xdr.ClientData();

			twoCellAnchor1.Append(fromMarker1);
			twoCellAnchor1.Append(toMarker1);
			twoCellAnchor1.Append(picture1);
			twoCellAnchor1.Append(clientData1);

			worksheetDrawing1.Append(twoCellAnchor1);

			drawingsPart1.WorksheetDrawing = worksheetDrawing1;
		}

		// Generates content of imagePart1.
		private void GenerateImagePart1Content(ImagePart imagePart1, string url)
		{
//			System.IO.Stream data = GetBinaryDataStream(imagePart1Data);
//			imagePart1.FeedData(data);
//			data.Close();
			WebRequest req = WebRequest.Create(url);
			WebResponse response = req.GetResponse();
			Stream stream = response.GetResponseStream();
			imagePart1.FeedData(stream);
		}

		// Generates content of workbookStylesPart1.
		private void GenerateWorkbookStylesPart1Content(WorkbookStylesPart workbookStylesPart1)
		{
			Stylesheet stylesheet1 = new Stylesheet();

			Fonts fonts1 = new Fonts(){ Count = (UInt32Value)1U };

			Font font1 = new Font();
			FontSize fontSize1 = new FontSize(){ Val = 11D };
			Color color1 = new Color(){ Theme = (UInt32Value)1U };
			FontName fontName1 = new FontName(){ Val = "Calibri" };
			FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering(){ Val = 2 };
			FontScheme fontScheme1 = new FontScheme(){ Val = FontSchemeValues.Minor };

			font1.Append(fontSize1);
			font1.Append(color1);
			font1.Append(fontName1);
			font1.Append(fontFamilyNumbering1);
			font1.Append(fontScheme1);

			fonts1.Append(font1);

			Fills fills1 = new Fills(){ Count = (UInt32Value)2U };

			Fill fill1 = new Fill();
			PatternFill patternFill1 = new PatternFill(){ PatternType = PatternValues.None };

			fill1.Append(patternFill1);

			Fill fill2 = new Fill();
			PatternFill patternFill2 = new PatternFill(){ PatternType = PatternValues.Gray125 };

			fill2.Append(patternFill2);

			fills1.Append(fill1);
			fills1.Append(fill2);

			Borders borders1 = new Borders(){ Count = (UInt32Value)1U };

			Border border1 = new Border();
			LeftBorder leftBorder1 = new LeftBorder();
			RightBorder rightBorder1 = new RightBorder();
			TopBorder topBorder1 = new TopBorder();
			BottomBorder bottomBorder1 = new BottomBorder();
			DiagonalBorder diagonalBorder1 = new DiagonalBorder();

			border1.Append(leftBorder1);
			border1.Append(rightBorder1);
			border1.Append(topBorder1);
			border1.Append(bottomBorder1);
			border1.Append(diagonalBorder1);

			borders1.Append(border1);

			CellStyleFormats cellStyleFormats1 = new CellStyleFormats(){ Count = (UInt32Value)1U };
			CellFormat cellFormat1 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

			cellStyleFormats1.Append(cellFormat1);

			CellFormats cellFormats1 = new CellFormats(){ Count = (UInt32Value)1U };
			CellFormat cellFormat2 = new CellFormat(){ NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };

			cellFormats1.Append(cellFormat2);

			CellStyles cellStyles1 = new CellStyles(){ Count = (UInt32Value)1U };
			CellStyle cellStyle1 = new CellStyle(){ Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

			cellStyles1.Append(cellStyle1);
			DifferentialFormats differentialFormats1 = new DifferentialFormats(){ Count = (UInt32Value)0U };
			TableStyles tableStyles1 = new TableStyles(){ Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium9", DefaultPivotStyle = "PivotStyleLight16" };

			stylesheet1.Append(fonts1);
			stylesheet1.Append(fills1);
			stylesheet1.Append(borders1);
			stylesheet1.Append(cellStyleFormats1);
			stylesheet1.Append(cellFormats1);
			stylesheet1.Append(cellStyles1);
			stylesheet1.Append(differentialFormats1);
			stylesheet1.Append(tableStyles1);

			workbookStylesPart1.Stylesheet = stylesheet1;
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

			A.FontScheme fontScheme2 = new A.FontScheme(){ Name = "Office" };

			A.MajorFont majorFont1 = new A.MajorFont();
			A.LatinFont latinFont1 = new A.LatinFont(){ Typeface = "Cambria" };
			A.EastAsianFont eastAsianFont1 = new A.EastAsianFont(){ Typeface = "" };
			A.ComplexScriptFont complexScriptFont1 = new A.ComplexScriptFont(){ Typeface = "" };
			A.SupplementalFont supplementalFont1 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
			A.SupplementalFont supplementalFont2 = new A.SupplementalFont(){ Script = "Hang", Typeface = "맑은 고딕" };
			A.SupplementalFont supplementalFont3 = new A.SupplementalFont(){ Script = "Hans", Typeface = "宋体" };
			A.SupplementalFont supplementalFont4 = new A.SupplementalFont(){ Script = "Hant", Typeface = "新細明體" };
			A.SupplementalFont supplementalFont5 = new A.SupplementalFont(){ Script = "Arab", Typeface = "Times New Roman" };
			A.SupplementalFont supplementalFont6 = new A.SupplementalFont(){ Script = "Hebr", Typeface = "Times New Roman" };
			A.SupplementalFont supplementalFont7 = new A.SupplementalFont(){ Script = "Thai", Typeface = "Tahoma" };
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
			A.SupplementalFont supplementalFont30 = new A.SupplementalFont(){ Script = "Jpan", Typeface = "ＭＳ Ｐゴシック" };
			A.SupplementalFont supplementalFont31 = new A.SupplementalFont(){ Script = "Hang", Typeface = "맑은 고딕" };
			A.SupplementalFont supplementalFont32 = new A.SupplementalFont(){ Script = "Hans", Typeface = "宋体" };
			A.SupplementalFont supplementalFont33 = new A.SupplementalFont(){ Script = "Hant", Typeface = "新細明體" };
			A.SupplementalFont supplementalFont34 = new A.SupplementalFont(){ Script = "Arab", Typeface = "Arial" };
			A.SupplementalFont supplementalFont35 = new A.SupplementalFont(){ Script = "Hebr", Typeface = "Arial" };
			A.SupplementalFont supplementalFont36 = new A.SupplementalFont(){ Script = "Thai", Typeface = "Tahoma" };
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

			fontScheme2.Append(majorFont1);
			fontScheme2.Append(minorFont1);

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
			themeElements1.Append(fontScheme2);
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
			document.PackageProperties.Creator = "Ian";
			document.PackageProperties.Created = System.Xml.XmlConvert.ToDateTime("2013-06-14T05:13:51Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
			document.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime("2013-06-14T05:15:03Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
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
