//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HW.Core.Models;

namespace HW.Core.Helpers
{
	public class IHGHtmlTable : HtmlTable
	{
		public IHGHtmlTable()
		{
			Style.Add("border", "0");
			Style.Add("border-collapse", "collapse");
			Style.Add("border-spacing", "0");
		}
	}
	
	public class IHGHtmlTableRow : HtmlTableRow
	{
		public IHGHtmlTableRow(params HtmlTableCell[] cells)
		{
			foreach (var c in cells) {
				Cells.Add(c);
			}
		}
		
		public void AddCell(IList<Control> cc)
		{
			foreach (var c in cc) {
				Cells.Add(new IHGHtmlTableCell(c));
			}
		}
		
		public void AddCell(Control c)
		{
			Cells.Add(new IHGHtmlTableCell(c));
		}
		
		public void AddCell(string text)
		{
			Cells.Add(new IHGHtmlTableCell(text));
		}
	}
	
	public class IHGHtmlTableCell : HtmlTableCell
	{
		public IHGHtmlTableCell(string text)
		{
			InnerText = text;
		}
		
		public IHGHtmlTableCell(IList<Control> cc)
		{
			foreach (var c in cc) {
				Controls.Add(c);
			}
		}
		
		public IHGHtmlTableCell(params Control[] cc) : this(new List<Control>(cc))
		{
		}
		
		public string FontSize {
			get { return Style["font-size"]; }
			set { Style["font-size"] = value; }
		}
	}
	
	public class IHGRadioButtonList : RadioButtonList
	{
		public IHGRadioButtonList(params string[] texts)
		{
			RepeatDirection = RepeatDirection.Horizontal;
			RepeatLayout = RepeatLayout.Flow;
			foreach (string t in texts) {
				Items.Add(new ListItem(t));
			}
		}
	}
	
	public class DepartmentListHtmlTable : HtmlTable
	{
		IList<Department> departments;
		
		public IList<Department> Departments {
			get { return departments; }
			set {
				departments = value;
				bool[] DX = new bool[8];
				foreach (var d in departments) {
					var r = new IHGHtmlTableRow();
					r.AddCell(new CheckBox { ID = "DID" + d.Id });
					r.AddCell(d.Name);
					
					int depth = d.Depth;
					DX[depth] = d.Siblings > 0;

					IList<Control> images = new List<Control>();
					for (int i = 1; i <= depth; i++) {
						images.Add(new HtmlImage { Src = string.Format("/img/{0}.gif", i == depth ? (DX[i] ? "T" : "L") : (DX[i] ? "I" : "null")), Width = 19, Height = 20 });
					}
					var t = new IHGHtmlTable();
					var rr = new IHGHtmlTableRow();
					rr.AddCell(images);
					rr.AddCell(d.Name);
					t.Rows.Add(rr);

					IHGHtmlTableCell c = new IHGHtmlTableCell(t);
					r.Cells.Add(c);
					
					Rows.Add(r);
				}
			}
		}
	}
}
