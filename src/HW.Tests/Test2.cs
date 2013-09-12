/*
 * Created by SharpDevelop.
 * User: ultra
 * Date: 8/21/2013
 * Time: 7:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace HW.Tests
{
	[TestFixture]
	public class Test2
	{
		[Test]
		public void TestMethod()
		{
			string file = @"C:\Downloads\test.xlsx";
			FileInfo newFile = new FileInfo(file);
			if (newFile.Exists)
			{
				newFile.Delete();  // ensures we create a new workbook
				newFile = new FileInfo(file);
			}
			using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inventory");
                //Add the headers
                worksheet.Cells[1, 1].Value = "ID";
//                worksheet.Cells[1, 2].Value = "hälsa";
//                worksheet.Cells[1, 3].Value = "Quantity";
//                worksheet.Cells[1, 4].Value = "Price";
//                worksheet.Cells[1, 5].Value = "Value";
//
//                //Add some items...
//                worksheet.Cells["A2"].Value = 12001;
//                worksheet.Cells["B2"].Value = "Nails";
//                worksheet.Cells["C2"].Value = 37;
//                worksheet.Cells["D2"].Value = 3.99;
//
//                worksheet.Cells["A3"].Value = 12002;
//                worksheet.Cells["B3"].Value = "Hammer";
//                worksheet.Cells["C3"].Value = 5;
//                worksheet.Cells["D3"].Value = 12.10;
//
//                worksheet.Cells["A4"].Value = 12003;
//                worksheet.Cells["B4"].Value = "Saw";
//                worksheet.Cells["C4"].Value = 12;
//                worksheet.Cells["D4"].Value = 15.37;
//
//                //Add a formula for the value-column
//                worksheet.Cells["E2:E4"].Formula = "C2*D2";
//
//                //Ok now format the values;
//                using (var range = worksheet.Cells[1, 1, 1, 5]) 
//                {
//                    range.Style.Font.Bold = true;
//                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
//                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
//                    range.Style.Font.Color.SetColor(Color.White);
//                }
//
//                worksheet.Cells["A5:E5"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
//                worksheet.Cells["A5:E5"].Style.Font.Bold = true;
//
//                worksheet.Cells[5, 3, 5, 5].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(2,3,4,3).Address);
//                worksheet.Cells["C2:C5"].Style.Numberformat.Format = "#,##0";
//                worksheet.Cells["D2:E5"].Style.Numberformat.Format = "#,##0.00";
//
//                //Create an autofilter for the range
//                worksheet.Cells["A1:E4"].AutoFilter = true;
//
//                worksheet.Cells["A2:A4"].Style.Numberformat.Format = "@";   //Format as text
//                worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells
//
//                // lets set the header text 
//                worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Inventory";
//                // add the page number to the footer plus the total number of pages
//                worksheet.HeaderFooter.OddFooter.RightAlignedText =
//                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
//                // add the sheet name to the footer
//                worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
//                // add the file path to the footer
//                worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;
//
//                worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
//                worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];
//
//                // Change the sheet view to show it in page layout mode
//                worksheet.View.PageLayoutView = true;
//
//                // set some document properties
//                package.Workbook.Properties.Title = "Invertory";
//                package.Workbook.Properties.Author = "Jan Källman";
//                package.Workbook.Properties.Comments = "This sample demonstrates how to create an Excel 2007 workbook using EPPlus";
//
//                // set some extended property values
//                package.Workbook.Properties.Company = "AdventureWorks Inc.";
//
//                // set some custom property values
//                package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
//                package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");
                // save our new workbook and we are done!
                package.Save();

            }
		}
	}
}
