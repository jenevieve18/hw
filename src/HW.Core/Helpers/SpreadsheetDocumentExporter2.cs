using System;
using System.Collections.Generic;
using System.IO;
using HW.Core.Models;
using HW.Core.Services;

using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;

namespace HW.Core.Helpers
{
	public class SpreadsheetDocumentExporter2 : AbstractExporter
	{
		ReportPart r;
		IList<ReportPartLanguage> parts;
		ReportService service;
		
		public SpreadsheetDocumentExporter2(ReportPart r)
		{
			this.r = r;
		}
		
		public SpreadsheetDocumentExporter2(ReportService service, IList<ReportPartLanguage> parts)
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
		
//		public override object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm)
//		{
//			MemoryStream output = new MemoryStream();
//			using (SpreadsheetDocument  package = SpreadsheetDocument.Create(output, SpreadsheetDocumentType.Workbook)) {
//				string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot, fm, tm);
//				CreateParts(package, r.CurrentLanguage, url);
//			}
//			return output;
//		}
//		
//		public override object ExportAll(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm)
//		{
//			throw new NotImplementedException();
//		}
		
//		public override object Export(string url)
		public override object Export(string url, int langID, int pruid, int fy, int ty, int gb, int plot, int grpng, int spons, int sid, string gid, int sponsorMinUserCountToDisclose, int fm, int tm)
		{
			MemoryStream output = new MemoryStream();
			using (SpreadsheetDocument  package = SpreadsheetDocument.Create(output, SpreadsheetDocumentType.Workbook)) {
//				string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot, fm, tm);
				CreateParts(package, r.CurrentLanguage, url);
			}
			return output;
		}
		
//		public override object ExportAll(int langID)
		public override object ExportAll(int langID, int pruid, int fy, int ty, int gb, int plot, int grpng, int spons, int sid, string gid, int sponsorMinUserCountToDisclose, int fm, int tm)
		{
			throw new NotImplementedException();
		}
		
		public override object SuperExport(string url)
		{
			throw new NotImplementedException();
		}
		
		public override object SuperExportAll(int langID)
		{
			throw new NotImplementedException();
		}

        // Adds child parts and generates content of the specified part.
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
            GenerateImagePart1Content(imagePart1);

            SharedStringTablePart sharedStringTablePart1 = workbookPart1.AddNewPart<SharedStringTablePart>("rId6");
            GenerateSharedStringTablePart1Content(sharedStringTablePart1);

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
            FileVersion fileVersion1 = new FileVersion(){ ApplicationName = "xl", LastEdited = "4", LowestEdited = "4", BuildVersion = "4507" };
            WorkbookProperties workbookProperties1 = new WorkbookProperties(){ FilterPrivacy = true, DefaultThemeVersion = (UInt32Value)124226U };

            BookViews bookViews1 = new BookViews();
            WorkbookView workbookView1 = new WorkbookView(){ XWindow = 240, YWindow = 105, WindowWidth = (UInt32Value)14805U, WindowHeight = (UInt32Value)8010U };

            bookViews1.Append(workbookView1);

            Sheets sheets1 = new Sheets();
            Sheet sheet1 = new Sheet(){ Name = "Sheet1", SheetId = (UInt32Value)1U, Id = "rId1" };
            Sheet sheet2 = new Sheet(){ Name = "Sheet2", SheetId = (UInt32Value)2U, Id = "rId2" };
            Sheet sheet3 = new Sheet(){ Name = "Sheet3", SheetId = (UInt32Value)3U, Id = "rId3" };

            sheets1.Append(sheet1);
            sheets1.Append(sheet2);
            sheets1.Append(sheet3);
            CalculationProperties calculationProperties1 = new CalculationProperties(){ CalculationId = (UInt32Value)125725U };
            FileRecoveryProperties fileRecoveryProperties1 = new FileRecoveryProperties(){ RepairLoad = true };

            workbook1.Append(fileVersion1);
            workbook1.Append(workbookProperties1);
            workbook1.Append(bookViews1);
            workbook1.Append(sheets1);
            workbook1.Append(calculationProperties1);
            workbook1.Append(fileRecoveryProperties1);

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

            SheetView sheetView3 = new SheetView(){ TabSelected = true, WorkbookViewId = (UInt32Value)0U };
            Selection selection1 = new Selection(){ ActiveCell = "B8", SequenceOfReferences = new ListValue<StringValue>() { InnerText = "B8" } };

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
            columnId1.Text = "6";
            Xdr.ColumnOffset columnOffset1 = new Xdr.ColumnOffset();
            columnOffset1.Text = "95250";
            Xdr.RowId rowId1 = new Xdr.RowId();
            rowId1.Text = "1";
            Xdr.RowOffset rowOffset1 = new Xdr.RowOffset();
            rowOffset1.Text = "28575";

            fromMarker1.Append(columnId1);
            fromMarker1.Append(columnOffset1);
            fromMarker1.Append(rowId1);
            fromMarker1.Append(rowOffset1);

            Xdr.ToMarker toMarker1 = new Xdr.ToMarker();
            Xdr.ColumnId columnId2 = new Xdr.ColumnId();
            columnId2.Text = "20";
            Xdr.ColumnOffset columnOffset2 = new Xdr.ColumnOffset();
            columnOffset2.Text = "85725";
            Xdr.RowId rowId2 = new Xdr.RowId();
            rowId2.Text = "23";
            Xdr.RowOffset rowOffset2 = new Xdr.RowOffset();
            rowOffset2.Text = "28575";

            toMarker1.Append(columnId2);
            toMarker1.Append(columnOffset2);
            toMarker1.Append(rowId2);
            toMarker1.Append(rowOffset2);

            Xdr.Picture picture1 = new Xdr.Picture();

            Xdr.NonVisualPictureProperties nonVisualPictureProperties1 = new Xdr.NonVisualPictureProperties();
            Xdr.NonVisualDrawingProperties nonVisualDrawingProperties1 = new Xdr.NonVisualDrawingProperties(){ Id = (UInt32Value)1025U, Name = "Picture 1", Description = "http://dev-grp.healthwatch.se/reportImage.aspx?LangID=1&FY=2012&TY=2013&SAID=514&SID=83&GB=7&RPID=14&RPLID=115&PRUID=2643&GRPNG=2&GID=0,923&PLOT=pptx" };

            Xdr.NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties1 = new Xdr.NonVisualPictureDrawingProperties();
            A.PictureLocks pictureLocks1 = new A.PictureLocks(){ NoChangeAspect = true, NoChangeArrowheads = true };

            nonVisualPictureDrawingProperties1.Append(pictureLocks1);

            nonVisualPictureProperties1.Append(nonVisualDrawingProperties1);
            nonVisualPictureProperties1.Append(nonVisualPictureDrawingProperties1);

            Xdr.BlipFill blipFill1 = new Xdr.BlipFill();

            A.Blip blip1 = new A.Blip(){ Embed = "rId1", CompressionState = A.BlipCompressionValues.Print };
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
            A.Offset offset1 = new A.Offset(){ X = 3752850L, Y = 219075L };
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
        private void GenerateImagePart1Content(ImagePart imagePart1)
        {
            System.IO.Stream data = GetBinaryDataStream(imagePart1Data);
            imagePart1.FeedData(data);
            data.Close();
        }

        // Generates content of sharedStringTablePart1.
        private void GenerateSharedStringTablePart1Content(SharedStringTablePart sharedStringTablePart1)
        {
            SharedStringTable sharedStringTable1 = new SharedStringTable();

            sharedStringTablePart1.SharedStringTable = sharedStringTable1;
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
            document.PackageProperties.Creator = "";
            document.PackageProperties.Created = System.Xml.XmlConvert.ToDateTime("2006-09-16T00:00:00Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
            document.PackageProperties.Modified = System.Xml.XmlConvert.ToDateTime("2013-08-14T13:06:12Z", System.Xml.XmlDateTimeSerializationMode.RoundtripKind);
            document.PackageProperties.LastModifiedBy = "";
        }

        #region Binary Data
        private string imagePart1Data = "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aHRtbDEtdHJhbnNpdGlvbmFsLmR0ZCI+DQoNCjxodG1sIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hodG1sIj4NCjxoZWFkPjx0aXRsZT4NCg0KPC90aXRsZT48L2hlYWQ+DQo8Ym9keT4NCiAgICA8Zm9ybSBuYW1lPSJmb3JtMSIgbWV0aG9kPSJwb3N0IiBhY3Rpb249InJlcG9ydEltYWdlLmFzcHg/TGFuZ0lEPTEmYW1wO0ZZPTIwMTImYW1wO1RZPTIwMTMmYW1wO1NBSUQ9NTE0JmFtcDtTSUQ9ODMmYW1wO0dCPTcmYW1wO1JQSUQ9MTQmYW1wO1JQTElEPTExNSZhbXA7UFJVSUQ9MjY0MyZhbXA7R1JQTkc9MiZhbXA7R0lEPTAlMmM5MjMmYW1wO1BMT1Q9cHB0eCIgaWQ9ImZvcm0xIj4NCjxpbnB1dCB0eXBlPSJoaWRkZW4iIG5hbWU9Il9fVklFV1NUQVRFIiBpZD0iX19WSUVXU1RBVEUiIHZhbHVlPSIvd0VQRHdVTExURTJNVFkyT0RjeU1qbGtaQ3dZRDFWMnhVb0dCanpiKzdJWlhTU0FSSEhxaUNVQjhOLzhjTmlwUEtESCIgLz4NCg0KICAgIDxkaXY+DQogICAgDQogICAgPC9kaXY+DQogICAgPC9mb3JtPg0KPC9ib2R5Pg0KPC9odG1sPg0K";

        private System.IO.Stream GetBinaryDataStream(string base64String)
        {
            return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
        }

        #endregion


	}
}
