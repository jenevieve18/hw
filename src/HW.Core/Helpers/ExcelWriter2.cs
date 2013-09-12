//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Drawing;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace HW.Core.Helpers
{
	public class ExcelCell
	{
		public int Row { get; set; }
		public int Column { get; set; }
		public object Value { get; set; }
		public Color BackgroundColor { get; set; }
		public Color ForegroundColor { get; set; }
		public ExcelBorderStyle BorderStyle { get; set; }
		public int FontSize { get; set; }
		public bool Merge { get; set; }
		public bool AutoFit { get; set; }
		public ExcelHorizontalAlignment HorizontalAlignment { get; set; }
		public ExcelVerticalAlignment VerticalAlignment { get; set; }
		public bool FontBold { get; set; }
		
		public ExcelCell()
		{
			FontBold = false;
			Value = "";
			FontSize = 10;
			BackgroundColor = Color.White;
			ForegroundColor = Color.Black;
			BorderStyle = ExcelBorderStyle.None;
			HorizontalAlignment = ExcelHorizontalAlignment.Left;
			VerticalAlignment = ExcelVerticalAlignment.Bottom;
		}
	}
	
	public class ExcelWriter// : IExcelWriter
	{
		ExcelPackage p;
		ExcelWorksheet w;
		
		public ExcelWriter(Stream stream)
		{
			p = new ExcelPackage(stream);
			w = p.Workbook.Worksheets.Add("Sheet1");
			w.Cells.Style.Font.Size = 10;
			w.Cells.Style.Font.Name = "Arial";
		}
		
		public void WriteCell(int row, int col, object value)
		{
			WriteCell(row, col, value, ExcelBorderStyle.Thin);
		}
		
		public void WriteCell(int row, int col, object value, ExcelBorderStyle border)
		{
			WriteCell(row, col, value, Color.Black, Color.White, 10, border);
		}
		
		public void WriteCell(int row, int col, object value, Color bgColor, ExcelBorderStyle border)
		{
			WriteCell(row, col, value, Color.Black, bgColor, 10, border, false, false, 0);
		}
		
		public void WriteCell(int row, int col, object value, Color fgColor, Color bgColor, int size, ExcelBorderStyle border)
		{
			WriteCell(row, col, value, fgColor, bgColor, size, border, false, false, 0);
		}
		
		public void WriteCell(int row, int col, object value, Color fgColor, Color bgColor, int size, ExcelBorderStyle border, bool autoFit)
		{
			WriteCell(row, col, value, fgColor, bgColor, size, border, autoFit, false, 0);
		}
		
		public void WriteCell(int row, int col, object value, Color fgColor, Color bgColor, int size, ExcelBorderStyle border, bool autoFit, bool merge, int mergeCount)
		{
			ExcelRange c = w.Cells[row + 1, col + 1];
			c.Value = value;
			c.Style.Font.Size = size;
			c.Style.Font.Color.SetColor(fgColor);
			if (autoFit) {
				c.AutoFitColumns();
			}
			if (merge) {
				//w.Cells[row + 1, col + 1, row += 1, col + 1 + mergeCount].Merge = true;
			}
			
			var f = c.Style.Fill;
			f.PatternType = ExcelFillStyle.Solid;
			f.BackgroundColor.SetColor(bgColor);
			
			var b = c.Style.Border;
			b.Bottom.Style = b.Top.Style = b.Left.Style = b.Right.Style = border;
		}
		
		public void WriteCell(ExcelCell cell)
		{
			ExcelRange c = w.Cells[cell.Row + 1, cell.Column + 1];
			c.Value = cell.Value;
			c.Style.Font.Size = cell.FontSize;
			c.Style.Font.Color.SetColor(cell.ForegroundColor);
			c.Style.HorizontalAlignment = cell.HorizontalAlignment;
			c.Style.VerticalAlignment = cell.VerticalAlignment;
			c.Style.Font.Bold = cell.FontBold;
			if (cell.AutoFit) {
				c.AutoFitColumns();
			}
			if (cell.Merge) {
				//w.Cells[row + 1, col + 1, row += 1, col + 1 + mergeCount].Merge = true;
			}
			
			var f = c.Style.Fill;
			f.PatternType = ExcelFillStyle.Solid;
			f.BackgroundColor.SetColor(cell.BackgroundColor);
			
			var b = c.Style.Border;
			b.Bottom.Style = b.Top.Style = b.Left.Style = b.Right.Style = cell.BorderStyle;
		}
		
		public void Merge(int fromRow, int fromCol, int toRow, int toCol, ExcelBorderStyle border)
		{
			ExcelRange c = w.Cells[fromRow + 1, fromCol + 1, toRow + 1, toCol + 1];
			c.Merge = true;
			var b = c.Style.Border;
			b.Bottom.Style = b.Top.Style = b.Left.Style = b.Right.Style = border;
		}
		
		public void EndWrite()
		{
			p.Save();
		}
	}
}
