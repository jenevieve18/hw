//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HW.Core
{
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
		
		public IHGHtmlTableCell(params Control[] cc)
		{
			foreach (var c in cc) {
				Controls.Add(c);
			}
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
