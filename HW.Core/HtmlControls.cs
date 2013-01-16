//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HW.Core
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
}
