/*
 * Created by SharpDevelop.
 * User: ultra
 * Date: 8/19/2013
 * Time: 3:07 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using NUnit.Framework;

namespace HW.Tests
{
	[TestFixture]
	public class Test1
	{
		[Test]
		public void TestMethod()
		{
			Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

			if (xlApp == null)
			{
				Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
				return;
			}
			xlApp.Visible = true;

			Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
			Worksheet ws = (Worksheet)wb.Worksheets[1];

			if (ws == null)
			{
				Console.WriteLine("Worksheet could not be created. Check that your office installation and project references are correct.");
			}

			// Select the Excel cells, in the range c1 to c7 in the worksheet.
			Range aRange = ws.get_Range("C1", "C7");

			if (aRange == null)
			{
				Console.WriteLine("Could not get a range. Check to be sure you have the correct versions of the office DLLs.");
			}

			// Fill the cells in the C1 to C7 range of the worksheet with the number 6.
//			Object[] args = new Object[1];
//			args[0] = 6;
//			aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);
			
			// Change the cells in the C1 to C7 range of the worksheet to the number 8.
			aRange.Value2 = 8;
		}
	}
}
