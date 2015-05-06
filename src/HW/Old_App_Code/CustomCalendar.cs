using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

[assembly: TagPrefix("healthWatch","HW")]

namespace healthWatch
{
	[DefaultEvent("SelectionChanged"),
	ToolboxData("<{0}:CustomCalendar runat=\"server\"></{0}:CustomCalendar>")]
	public class CustomCalendar : System.Web.UI.WebControls.Calendar, IPostBackEventHandler
	{
		#region Public properties
		[Bindable(true), 
		Category("Behavior"), 
		Description("Whether nonselectable days are included in week and month selections."), 
		DefaultValue(false)]
		public Boolean SelectAllInRange
		{
			get
			{
				object o = this.ViewState["SelectAllInRange"];
				if (o != null)
					return Boolean.Parse(o.ToString());
				else
					return false;
			}
			set { this.ViewState["SelectAllInRange"] = value.ToString(); }
		}

		[Bindable(true),
		Category("Appearance"),
		Description("Show key in header."),
		DefaultValue(false)]
		public Boolean ShowKey
		{
			get
			{
				object o = this.ViewState["ShowKey"];
				if (o != null)
					return Boolean.Parse(o.ToString());
				else
					return false;
			}
			set { this.ViewState["ShowKey"] = value.ToString(); }
		}

		[Bindable(true),
		Category("Appearance"),
		Description("True if title attributes are added to navigation and selector links."),
		DefaultValue(false)]
		public Boolean ShowLinkTitles
		{
			get
			{
				object o = this.ViewState["ShowLinkTitles"];
				if (o != null)
					return Boolean.Parse(o.ToString());
				else
					return false;
			}
			set { this.ViewState["ShowLinkTitles"] = value.ToString(); }
		}

		[Bindable(true),
		Category("Appearance"),
		Description("Dates displayed as bold."),
		DefaultValue("")]
		public string BoldDates
		{
			get
			{
				object o = this.ViewState["BoldDates"];
				if (o != null)
					return o.ToString();
				else
					return "";
			}
			set { this.ViewState["BoldDates"] = value; }
		}

		[Bindable(true),
		Category("Misc"),
		Description("The maximum month that can be displayed."),
		DefaultValue("")]
		public DateTime MaxVisibleDate
		{
			get
			{
				object o = this.ViewState["MaxVisibleDate"];
				if (o != null)
					return DateTime.Parse(o.ToString());
				else
					return DateTime.MinValue;
			}
			set { this.ViewState["MaxVisibleDate"] = value.ToString(); }
		}

		[Bindable(true),
		Category("Misc"),
		Description("The minumum month that can be displayed."),
		DefaultValue("")]
		public DateTime MinVisibleDate
		{
			get
			{
				object o = this.ViewState["MinVisibleDate"];
				if (o != null)
					return DateTime.Parse(o.ToString());
				else
					return DateTime.MinValue;
			}
			set { this.ViewState["MinVisibleDate"] = value.ToString(); }
		}
		#endregion

		#region Private properties

		private DateTime TargetDate
		{
			get
			{
				if (this.VisibleDate == DateTime.MinValue)
					return this.TodaysDate;
				else
					return this.VisibleDate;
			}
		}

		private static readonly DateTime DayCountBaseDate = new DateTime(2000, 1, 1);
		#endregion

		public CustomCalendar() : base()
		{}

		#region Control rendering
		/// <summary> 
		/// This member overrides <see cref="System.Web.UI.Control.Render"/>.
		/// </summary>
		protected override void Render(HtmlTextWriter output)
		{
			// Create the main table.
			Table table = new Table();
			table.CellPadding = this.CellPadding;
			table.CellSpacing = this.CellSpacing;
			if (this.ShowGridLines)
				table.GridLines = GridLines.Both;
			else
				table.GridLines = GridLines.None;

			// If ShowTitle is true, add a row with the calendar title.
			if (this.ShowTitle)
			{
				// Create a one-cell table row.
				TableRow row = new TableRow();
				TableCell cell = new TableCell();
				if (this.HasWeekSelectors(this.SelectionMode))
					cell.ColumnSpan = 8;
				else
					cell.ColumnSpan = 7;

				// Apply styling.
				cell.MergeStyle(this.TitleStyle);

				// Add the title table to the cell.
				cell.Controls.Add(this.TitleTable());
				row.Cells.Add(cell);

				// Add it to the table.
				table.Rows.Add(row);
			}

			// If ShowDayHeader is true, add a row with the days header.
			if (this.ShowDayHeader)
				table.Rows.Add(DaysHeaderTableRow());

			// Find the first date that will be visible on the calendar.
			DateTime date = this.GetFirstCalendarDate();

			// Create a list for storing nonselectable dates.
			ArrayList nonselectableDates = new ArrayList();

			// Add rows for the dates (six rows are always displayed).
			for (int i = 0; i < 6; i++)
			{
				TableRow row = new TableRow();

				// Create a week selector, if needed.
				if (this.HasWeekSelectors(this.SelectionMode))
				{
					TableCell cell = new TableCell();
					cell.HorizontalAlign = HorizontalAlign.Center;
					cell.MergeStyle(this.SelectorStyle);

					if (this.Enabled)
					{
						// Create the post back link.
						HtmlAnchor anchor = new HtmlAnchor();
						string arg = String.Format("R{0}07", this.DayCountFromDate(date));
						anchor.HRef = this.Page.GetPostBackClientHyperlink(this, arg);

						// If ShowLinkTitles is true, add a title.
						if (this.ShowLinkTitles)
							anchor.Attributes.Add("title",
								String.Format("Select the week starting {0}", date.ToString("D")));

						anchor.Controls.Add(new LiteralControl(this.SelectWeekText));

						// Add a color style to the anchor if it is explicitly
						// set.
						if (!this.SelectorStyle.ForeColor.IsEmpty)
							anchor.Attributes.Add("style", String.Format("color:{0}", this.SelectorStyle.ForeColor.Name));

						cell.Controls.Add(anchor);
					}
					else
						cell.Controls.Add(new LiteralControl(this.SelectWeekText));

					row.Cells.Add(cell);
				}

				// Add the days (there are always seven days per row).
				for (int j = 0; j < 7; j++)
				{
					// Create a CalendarDay and a TableCell for the date.
					CalendarDay day = this.Day(date);
					TableCell cell = this.Cell(day);

					// Raise the OnDayRender event.
					this.OnDayRender(cell, day);

					// If the day was marked nonselectable, add it to the list.
					if (!day.IsSelectable)
						nonselectableDates.Add(day.Date.ToShortDateString());

					// If the day is selectable, and the selection mode allows
					// it, convert the text to a link with post back.
					if (this.Enabled && day.IsSelectable &&
						this.SelectionMode != CalendarSelectionMode.None)
					{
						try
						{
							// Create the post back link.
							HtmlAnchor anchor = new HtmlAnchor();
							string arg = this.DayCountFromDate(date).ToString();
							anchor.HRef = this.Page.GetPostBackClientHyperlink(this, arg);

							// If ShowLinkTitles is true, add a title.
							if (this.ShowLinkTitles)
								anchor.Attributes.Add("title",
									String.Format("Select {0}", day.Date.ToString("D")));
							
							// Copy the existing text.
							anchor.Controls.Add(new LiteralControl(((LiteralControl) cell.Controls[0]).Text));

							// Add a color style to the anchor if it is
							// explicitly set. Note that the style precedence
							// follows that of the base Calendar control.
							/*string s = "";
							if (!this.DayStyle.ForeColor.IsEmpty)
								s = this.DayStyle.ForeColor.Name;
							if (day.IsWeekend && !this.WeekendDayStyle.ForeColor.IsEmpty)
								s = this.WeekendDayStyle.ForeColor.Name;
							if (day.IsOtherMonth && !this.OtherMonthDayStyle.ForeColor.IsEmpty)
								s = this.OtherMonthDayStyle.ForeColor.Name;
							if (day.IsToday && !this.TodayDayStyle.ForeColor.IsEmpty)
								s = this.TodayDayStyle.ForeColor.Name;
							if (this.SelectedDates.Contains(day.Date) && !this.SelectedDayStyle.ForeColor.IsEmpty)
								s = this.SelectedDayStyle.ForeColor.Name;
							if (s.Length > 0)
								anchor.Attributes.Add("style", String.Format("color:{0}", s));*/
							
							if(this.SelectedDates.Contains(day.Date) || this.BoldDates.IndexOf(day.Date.ToString("yyyy-MM-dd")) != -1)
								anchor.Attributes.Add("style", "text-decoration:none; font-weight:bold");
							else
								anchor.Attributes.Add("style", "text-decoration:none");

							// Replace the literal control in the cell with
							// the anchor.
							cell.Controls.RemoveAt(0);
							cell.Controls.AddAt(0, anchor);
						}
						catch (Exception)
						{}
					}

					// Add the cell to the current table row.
					row.Cells.Add(cell);

					// Bump the date.
					date = date.AddDays(1);
				}

				// Add the row.
				table.Rows.Add(row);
			}

			// Save the list of nonselectable dates.
			if (nonselectableDates.Count > 0)
				this.SaveNonselectableDates(nonselectableDates);

			// Apply styling.
			this.AddAttributesToRender(output);

			// Render the table.
			table.RenderControl(output);
		}

		// ====================================================================
		// Helper functions for rendering the control.
		// ====================================================================

		//
		// Generates a Table control for the calendar title.
		//
		private Table TitleTable()
		{
			// Create a table row.
			TableRow row = new TableRow();
			TableCell cell;
			HtmlAnchor anchor;
			DateTime date;
			string text;

			bool isLocked = false;

			// Add a table cell with the previous month.
			if (this.ShowNextPrevMonth)
			{
				cell = new TableCell();
				cell.MergeStyle(this.NextPrevStyle);
				cell.Style.Add("width", "15%");

				// Find the first of the previous month, needed for post back
				// processing.
				try
				{
					date = new DateTime(this.TargetDate.Year, this.TargetDate.Month, 1).AddMonths(-1);
				}
				catch (Exception)
				{
					date = this.TargetDate;
				}
				isLocked = (this.MinVisibleDate.Date == this.TargetDate.Date && this.MaxVisibleDate.Date == this.TargetDate.Date);

				// Get the previous month text.
				if (this.NextPrevFormat == NextPrevFormat.CustomText)
					text = this.PrevMonthText;
				else
				{
					if (this.NextPrevFormat == NextPrevFormat.ShortMonth)
						text = date.ToString("MMM");
					else
						text = date.ToString("MMMM");
				}

				if (this.Enabled)
				{
					// If a minimum visible month is set, check if the
					// previous month falls before it.
					if (this.MinVisibleDate != DateTime.MinValue && new DateTime(this.MinVisibleDate.Year, this.MinVisibleDate.Month, 1) > date)
					{
						// Yes, the previous month is out of range so just add
						// a non-breaking space.
						cell.Controls.Add(new LiteralControl("&nbsp;"));
					}
					else
					{
						// No, create the post back link.
						anchor = new HtmlAnchor();
						anchor.Attributes.Add("style", "text-decoration:none");
						string arg = String.Format("V{0}", this.DayCountFromDate(date));
						anchor.HRef = this.Page.GetPostBackClientHyperlink(this, arg);

						// If ShowLinkTitles is true, add a title.
						if (this.ShowLinkTitles)
							anchor.Attributes.Add("title", String.Format("View {0}", date.ToString("Y")));

						anchor.Controls.Add(new LiteralControl(text));

						// Add a color style to the anchor if it is explicitly
						// set.
						if (!this.NextPrevStyle.ForeColor.IsEmpty)
							anchor.Attributes.Add("style", String.Format("color:{0}", this.NextPrevStyle.ForeColor.Name));

						if (this.NextPrevStyle.CssClass != null)
							anchor.Attributes.Add("class", this.NextPrevStyle.CssClass);

						// Add the link to the cell.
						cell.Controls.Add(anchor);
					}
				}
				else
					cell.Controls.Add(new LiteralControl(text));

				row.Cells.Add(cell);
			}

			// Add a table cell for the title text.
			cell = new TableCell();
			if(this.ShowKey)
			{
				cell.Controls.Add(new LiteralControl("<img src=\"img/lock_on.gif\"/>"));
			}
			cell.HorizontalAlign = HorizontalAlign.Center;
			if (this.ShowNextPrevMonth)
				cell.Style.Add("width", "70%");
			if (this.TitleFormat == TitleFormat.Month)
				cell.Text = this.TargetDate.ToString("MMMM");
			else
			{
				//cell.Text = this.TargetDate.ToString("y").Replace(", ", " ");
				
				//cell.Controls.Add(new LiteralControl(this.TargetDate.ToString("MMMM") + "&nbsp;"));
				
				DropDownList m = new DropDownList();
				m.CssClass = this.TitleStyle.CssClass;
				m.ID = "CalendarMonth";
				m.Attributes["onchange"] = this.Page.GetPostBackEventReference(this,"[xxx]").Replace("'[xxx]'","this.options[this.selectedIndex].value");
				m.Style.Add("background",ColorTranslator.ToHtml(this.TitleStyle.BackColor));
				for(int i = 1; i <= 12; i++)
				{
					string arg = "V" + this.DayCountFromDate(new DateTime(this.TargetDate.Year, i, 1)).ToString();
					if(!isLocked || this.TargetDate.Month == i)
					{
						m.Items.Add(new ListItem(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i-1],arg));
					}
					if(this.TargetDate.Month == i)
					{
						m.SelectedValue = arg;
					}
				}
				cell.Controls.Add(m);

				DropDownList y = new DropDownList();
				y.CssClass = this.TitleStyle.CssClass;
				y.ID = "CalendarYear";
				y.Attributes["onchange"] = this.Page.GetPostBackEventReference(this,"[xxx]").Replace("'[xxx]'","this.options[this.selectedIndex].value");
				y.Style.Add("background",ColorTranslator.ToHtml(this.TitleStyle.BackColor));
				for(int i = 2006; i < DateTime.Today.Year+10; i++)
				{
					string arg = "V" + this.DayCountFromDate(new DateTime(i, this.TargetDate.Month, 1)).ToString();
					if(!isLocked || this.TargetDate.Year == i)
					{
						y.Items.Add(new ListItem(i.ToString(),arg));
					}
					if(this.TargetDate.Year == i)
					{
						y.SelectedValue = arg;
					}
				}
				cell.Controls.Add(y);
			}
			if (this.TitleStyle.CssClass != null)
				cell.CssClass = this.TitleStyle.CssClass;
			row.Cells.Add(cell);

			// Add a table cell for the next month.
			if (this.ShowNextPrevMonth)
			{
				cell = new TableCell();
				cell.HorizontalAlign = HorizontalAlign.Right;
				cell.MergeStyle(this.NextPrevStyle);
				cell.Style.Add("width", "15%");

				// Find the first of the next month, needed for post back
				// processing.
				try
				{
					date = new DateTime(this.TargetDate.Year, this.TargetDate.Month, 1).AddMonths(1);
				}
				catch (Exception)
				{
					date = this.TargetDate;
				}

				// Get the next month text.
				if (this.NextPrevFormat == NextPrevFormat.CustomText)
					text = this.NextMonthText;
				else
				{
					if (this.NextPrevFormat == NextPrevFormat.ShortMonth)
						text = date.ToString("MMM");
					else
						text = date.ToString("MMMM");
				}

				if (this.Enabled)
				{
					// If a maximum visible month is set, check if the next
					// month falls after it.
					if (this.MaxVisibleDate != DateTime.MinValue &&
						new DateTime(this.MaxVisibleDate.Year, this.MaxVisibleDate.Month, 1) < date)
					{
						// Yes, the next month is out of range so just add a
						// non-breaking space.
						cell.Controls.Add(new LiteralControl("&nbsp;"));
					}
					else
					{
						// No, create the post back link.
						anchor = new HtmlAnchor();
						anchor.Attributes.Add("style", "text-decoration:none");
						string arg = String.Format("V{0}", this.DayCountFromDate(date));
						anchor.HRef = this.Page.GetPostBackClientHyperlink(this, arg);

						// If ShowLinkTitles is true, add a title.
						if (this.ShowLinkTitles)
							anchor.Attributes.Add("title",
								String.Format("View {0}", date.ToString("Y")));

						anchor.Controls.Add(new LiteralControl(text));

						// Add a color style to the anchor if it is explicitly
						// set.
						if (!this.NextPrevStyle.ForeColor.IsEmpty)
							anchor.Attributes.Add("style", String.Format("color:{0}", this.NextPrevStyle.ForeColor.Name));

						if (this.NextPrevStyle.CssClass != null)
							anchor.Attributes.Add("class", this.NextPrevStyle.CssClass);

						// Add the link to the cell.
						cell.Controls.Add(anchor);
					}
				}
				else
					cell.Controls.Add(new LiteralControl(text));

				row.Cells.Add(cell);
			}

			// Create the table and add the title row to it.
			Table table = new Table();
			table.CellPadding = 0;
			table.CellSpacing = 0;
			table.Attributes.Add("style", "width:100%;");
			table.Rows.Add(row);

			return table;
		}

		//
		// Generates a TableRow control for the calendar days header.
		//
		private TableRow DaysHeaderTableRow()
		{
			// Create the table row.
			TableRow row = new TableRow();

			// Create an array of days.
			DayOfWeek[] days = {
								   DayOfWeek.Sunday,
								   DayOfWeek.Monday,
								   DayOfWeek.Tuesday,
								   DayOfWeek.Wednesday,
								   DayOfWeek.Thursday,
								   DayOfWeek.Friday,
								   DayOfWeek.Saturday
							   };

			// Adjust the array to get the specified starting day at the first index.
			DayOfWeek first = this.GetFirstDayOfWeek();
			while(days[0] != first)
			{
				DayOfWeek temp = days[0];
				for (int i = 0; i < days.Length - 1; i++)
					days[i] = days[i + 1];
				days[days.Length - 1] = temp;
			}

			// Add a month selector column, if needed.
			if (this.HasWeekSelectors(this.SelectionMode))
			{
				TableCell cell = new TableCell();
				cell.HorizontalAlign = HorizontalAlign.Center;

				// If months are selectable, create the selector.
				if (this.SelectionMode == CalendarSelectionMode.DayWeekMonth)
				{
					// Find the first of the month.
					DateTime date = new DateTime(this.TargetDate.Year, this.TargetDate.Month, 1);

					// Use the selector style.
					cell.MergeStyle(this.SelectorStyle);

					// Create the post back link.
					if (this.Enabled)
					{
						HtmlAnchor anchor = new HtmlAnchor();
						string arg = String.Format("R{0}{1}",
							this.DayCountFromDate(date),
							DateTime.DaysInMonth(date.Year, date.Month));
						anchor.HRef = this.Page.GetPostBackClientHyperlink(this, arg);

						// If ShowLinkTitles is true, add a title.
						if (this.ShowLinkTitles)
							anchor.Attributes.Add("title",
								String.Format("Select the month of {0}", date.ToString("Y")));

						anchor.Controls.Add(new LiteralControl(this.SelectMonthText));

						// Add a color style to the anchor if it is explicitly
						// set.
						if (!this.SelectorStyle.ForeColor.IsEmpty)
							anchor.Attributes.Add("style", String.Format("color:{0}", this.SelectorStyle.ForeColor.Name));

						cell.Controls.Add(anchor);
					}
					else
						cell.Controls.Add(new LiteralControl(this.SelectMonthText));
				}
				else
					// Use the day header style.
					cell.CssClass = this.DayHeaderStyle.CssClass;

				row.Cells.Add(cell);
			}

			// Add the day names to the header.
			foreach (System.DayOfWeek day in days)
				row.Cells.Add(this.DayHeaderTableCell(day));

			return row;
		}

		//
		// Returns a table cell containing a day name for the calendar day
		// header.
		//
		private TableCell DayHeaderTableCell(System.DayOfWeek dayOfWeek)
		{
			// Generate the day name text based on the specified format.
			string s;
			if (this.DayNameFormat == DayNameFormat.Short)
				s = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[(int) dayOfWeek];
			else
			{
				s = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int) dayOfWeek];
				if (this.DayNameFormat == DayNameFormat.FirstTwoLetters)
					s = s.Substring(0, 2);
				if (this.DayNameFormat == DayNameFormat.FirstLetter)
					s = s.Substring(0, 1);
			}

			// Create the cell, set the style and the text.
			TableCell cell = new TableCell();
			cell.HorizontalAlign = HorizontalAlign.Center;
			cell.MergeStyle(this.DayHeaderStyle);
			cell.Text = s;

			return cell;
		}

		//
		// Determines the first day of the week based on the FirstDayOfWeek
		// property setting.
		//
		private System.DayOfWeek GetFirstDayOfWeek()
		{
			// If the default value is specifed, use the system default.
			if (this.FirstDayOfWeek == FirstDayOfWeek.Default)
				return CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
			else
				return (DayOfWeek) this.FirstDayOfWeek;
		}

		//
		// Returns the date that should appear in the first day cell of the
		// calendar display.
		//
		private DateTime GetFirstCalendarDate()
		{
			// Start with the first of the month.
			DateTime date = new DateTime(this.TargetDate.Year, this.TargetDate.Month, 1);

			// While that day does not fall on the first day of the week, move back.
			DayOfWeek firstDay = this.GetFirstDayOfWeek();
			while(date.DayOfWeek != firstDay)
				date = date.AddDays(-1);

			return date;
		}

		//
		// Creates a CalendarDay instance for the given date.
		//
		// Note: This object is included in the DayRenderEventArgs passed to
		// the DayRender event handler.
		//
		private CalendarDay Day(DateTime date)
		{
			CalendarDay day = new CalendarDay(
				date,
				date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday,
				date == this.TodaysDate,
				date == this.SelectedDate,
				!(date.Month == this.TargetDate.Month && date.Year == this.TargetDate.Year),
				date.Day.ToString());

			// Default the day to selectable.
			if(this.MaxVisibleDate != DateTime.MinValue || this.MinVisibleDate != DateTime.MinValue)
			{
				if(day.Date > this.MaxVisibleDate.Date || day.Date < this.MinVisibleDate.Date)
					day.IsSelectable = false;
				else
					day.IsSelectable = true;
			}
			else
				day.IsSelectable = true;

			return day;
		}

		//
		// Creates a TableCell control for the given calendar day.
		//
		// Note: This object is included in the DayRenderEventArgs passed to
		// the DayRender event handler.
		//
		private TableCell Cell(CalendarDay day)
		{
			TableCell cell = new TableCell();
			cell.HorizontalAlign = HorizontalAlign.Center;
			if (this.HasWeekSelectors(this.SelectionMode))
				cell.Attributes.Add("style", "width:12%");
			else
				cell.Attributes.Add("style", "width:14%");

			// Add styling based on day flags.
			// Note:
			//   - Styles are applied per the precedence order used by the
			//     base Calendar control.
			//   - For CssClass, multiple class names may be added.
			StringBuilder sb = new StringBuilder();
			if (this.SelectedDates.Contains(day.Date))
			{
				cell.MergeStyle(this.SelectedDayStyle);
				sb.AppendFormat(" {0}", this.SelectedDayStyle.CssClass);
			}
			if (day.IsToday)
			{
				cell.MergeStyle(this.TodayDayStyle);
				sb.AppendFormat(" {0}", this.TodayDayStyle.CssClass);
			}
			if (day.IsOtherMonth)
			{
				cell.MergeStyle(this.OtherMonthDayStyle);
				sb.AppendFormat(" {0}", this.OtherMonthDayStyle.CssClass);
			}
			if (day.IsWeekend)
			{
				cell.MergeStyle(this.WeekendDayStyle);
				sb.AppendFormat(" {0}", this.WeekendDayStyle.CssClass);
			}
			cell.MergeStyle(this.DayStyle);
			sb.AppendFormat(" {0}", this.DayStyle.CssClass);
			string s = sb.ToString().Trim();
			if (s.Length > 0)
				cell.CssClass = s;

			// Add a literal control to the cell using the day number for the
			// text.
			cell.Controls.Add(new LiteralControl(day.DayNumberText));

			return cell;
		}

		//
		// Returns true if the selection mode includes week selectors.
		//
		private new bool HasWeekSelectors(CalendarSelectionMode selectionMode)
		{
			if (selectionMode == CalendarSelectionMode.DayWeek ||
				selectionMode == CalendarSelectionMode.DayWeekMonth)
				return true;
			else
				return false;
		}
		#endregion

		#region Post back event handling
		// ====================================================================
		// Functions for converting between DateTime and day count values.
		// ====================================================================

		//
		// Returns the number of days between the given DateTime value and the
		// base date.
		//
		private int DayCountFromDate(DateTime date)
		{
			return ((TimeSpan) (date - CustomCalendar.DayCountBaseDate)).Days;
		}

		//
		// Returns a DateTime value equal to the base date plus the given number
		// of days.
		//
		private DateTime DateFromDayCount(int dayCount)
		{
			return CustomCalendar.DayCountBaseDate.AddDays(dayCount);
		}

		// ====================================================================
		// Functions to save and load the nonselectable dates list.
		//
		// Note: A hidden form field is used to store this data rather than the
		// view state because the nonselectable dates are not known until after
		// the DayRender event has been raised for each day as the control is
		// rendered.
		//
		// To minimize the amount of data stored in that field, the dates are
		// represented as day count values.
		// ====================================================================

		//
		// Saves a list of dates to the hidden form field.
		//
		private void SaveNonselectableDates(ArrayList dates)
		{
			// Build a string array by converting each date to a day count
			// value.
			string[] list = new string[dates.Count];
			for (int i = 0; i < list.Length; i++)
				list[i] = this.DayCountFromDate(DateTime.Parse(dates[i].ToString())).ToString();

			// Get the hidden field name.
			string fieldName  = this.GetHiddenFieldName();

			// For the field value, create a comma-separated list from the day
			// count values.
			string fieldValue = HttpUtility.HtmlAttributeEncode(String.Join(",", list));

			// Add the hidden form field to the page.
			this.Page.RegisterHiddenField(fieldName, fieldValue);
		}

		//
		// Returns a list of dates stored in the hidden form field.
		//
		private ArrayList LoadNonselectableDates()
		{
			// Create an empty list.
			ArrayList dates = new ArrayList();

			// Get the value stored in the hidden form field.
			string fieldName  = this.GetHiddenFieldName();
			string fieldValue = this.Page.Request.Form[fieldName];

			// If no dates were stored, return the empty list.
			if (fieldValue == null)
				return dates;

			// Extract the individual day count values.
			string[] list = fieldValue.Split(',');

			// Convert those values to dates and store them in an array list.
			foreach (string s in list)
				dates.Add(this.DateFromDayCount(Int32.Parse(s)));

			return dates;
		}

		//
		// Returns the name of the hidden field used to store nonselectable
		// dates on the form.
		//
		private string GetHiddenFieldName()
		{
			// Create a unique field name.
			return String.Format("{0}_NonselectableDates", this.ClientID);
		}

		// ====================================================================
		// Implementation of the IPostBackEventHandler.RaisePostBackEvent
		// event handler.
		// ====================================================================

		/// <summary>
		/// Handles a post back event targeted at the control.
		/// </summary>
		/// <param name="eventArgument">
		/// A <see cref="System.String"/> representing the event argument passed to the handler.
		/// </param>
		public void RaisePostBackEvent(string eventArgument)
		{
			// Was the post back initiated by a previous or next month link?
			if (eventArgument.StartsWith("V"))
			{
				try
				{
					// Save the current visible date.
					DateTime previousDate = this.TargetDate;

					// Extract the day count from the argument and use it to
					// change the visible date.
					int d = Int32.Parse(eventArgument.Substring(1));
					this.VisibleDate = this.DateFromDayCount(d);

					// Raise the VisibleMonthChanged event.
					OnVisibleMonthChanged(this.VisibleDate, previousDate);
				}
				catch (Exception)
				{}
				return;
			}

			// Was the post back initiated by a month or week selector link?
			if (eventArgument.StartsWith("R"))
			{
				try
				{
					// Extract the day count and number of days from the
					// argument.
					int d = Int32.Parse(eventArgument.Substring(1, eventArgument.Length - 3));
					int n = Int32.Parse(eventArgument.Substring(eventArgument.Length - 2));

					// Get the starting date.
					DateTime date = this.DateFromDayCount(d);

					// Reset the selected dates collection to include all the
					// dates in the given range.
					this.SelectedDates.Clear();
					this.SelectedDates.SelectRange(date, date.AddDays(n - 1));

					// If SelectAllInRange is false, remove any dates found
					// in the nonselectable date list.
					if (!this.SelectAllInRange)
					{
						ArrayList nonselectableDates = this.LoadNonselectableDates();
						foreach(DateTime badDate in nonselectableDates)
							this.SelectedDates.Remove(badDate);
					}

					// Raise the SelectionChanged event.
					OnSelectionChanged();
				}
				catch (Exception)
				{}
				return;
			}

			// The post back must have been initiated by a calendar day link.
			try
			{
				// Get the day count from the argument.
				int d = Int32.Parse(eventArgument);

				// Reset the selected dates collection to include only the
				// newly selected date.
				this.SelectedDates.Clear();
				this.VisibleDate = this.DateFromDayCount(d);
				this.SelectedDates.Add(this.DateFromDayCount(d));

				// Raise the SelectionChanged event.
				OnSelectionChanged();
			}
			catch (Exception)
			{}
		}
		#endregion
	}
}
