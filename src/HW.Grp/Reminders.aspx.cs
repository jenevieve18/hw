﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class Reminders : System.Web.UI.Page
	{
		SqlDepartmentRepository departmentRepository = new SqlDepartmentRepository();
		IList<Department> departments;
		int lid;
		
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			buttonSave.Text = R.Str(lid, "save", "Save");
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SponsorID"] == null, "default.aspx", true);
			
			int sponsorID = ConvertHelper.ToInt32(Session["SponsorID"]);
			int sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"]);
			lid = ConvertHelper.ToInt32(Session["lid"], 1);
			
			Org.Controls.Add(new LiteralControl("<br>"));
			IHGHtmlTable table = new IHGHtmlTable { Border = 0, CellSpacing = 0, CellPadding = 0 };
			table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(Session["Sponsor"].ToString()) { ColSpan = 3 }));
			Dictionary<int, bool> DX = new Dictionary<int, bool>();

			departments = departmentRepository.FindBySponsorWithSponsorAdminInDepth(sponsorID, sponsorAdminID);
			foreach (var d in departments) {
                IHGHtmlTable boxes = new IHGHtmlTable();
                var ld = new DropDownList { ID = "LDID" + d.Id };
                ld.Items.Add(new ListItem(R.Str(lid, "week.same", "< same as parent >"), "NULL"));
                ld.Items.Add(new ListItem(R.Str(lid, "day.everyday", "every day"), "1"));
                ld.Items.Add(new ListItem(R.Str(lid, "week", "week"), "7"));
                ld.Items.Add(new ListItem(R.Str(lid, "week.two", "2 weeks"), "14"));
                ld.Items.Add(new ListItem(R.Str(lid, "month", "month"), "30"));
                ld.Items.Add(new ListItem(R.Str(lid, "month.three", "3 months"), "90"));
                ld.Items.Add(new ListItem(R.Str(lid, "month.six", "6 months"), "180"));
                
                var lw = new DropDownList { ID = "LWID" + d.Id };
                lw.Items.Add(new ListItem(R.Str(lid, "week.same", "< same as parent >"), "NULL"));
                lw.Items.Add(new ListItem(R.Str(lid, "week.disabled", "< disabled >"), "-1"));
                lw.Items.Add(new ListItem(R.Str(lid, "week.everyday", "< every day >"), "0"));
                lw.Items.Add(new ListItem(R.Str(lid, "week.monday", "Monday"), "1"));
                lw.Items.Add(new ListItem(R.Str(lid, "week.tuesday", "Tuesday"), "2"));
                lw.Items.Add(new ListItem(R.Str(lid, "week.wednesday", "Wednesday"), "3"));
                lw.Items.Add(new ListItem(R.Str(lid, "week.thursday", "Thursday"), "4"));
                lw.Items.Add(new ListItem(R.Str(lid, "week.friday", "Friday"), "5"));

                ld.SelectedValue = d.LoginDays.ToString();
                lw.SelectedValue = d.LoginWeekDay.ToString();
                boxes.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(ld), new IHGHtmlTableCell(lw)));
                
                IHGHtmlTableRow row = new IHGHtmlTableRow(new IHGHtmlTableCell(boxes), new IHGHtmlTableCell(d.Id.ToString()));
				int depth = d.Depth;
				DX[depth] = d.Siblings > 0;

				IList<Control> images = new List<Control>();
				for (int i = 1; i <= depth; i++) {
					if (!DX.ContainsKey(i)) {
						DX[i] = false;
					}
					images.Add(new HtmlImage { Src = string.Format("img/{0}.gif", i == depth ? (DX[i] ? "T" : "L") : (DX[i] ? "I" : "null")), Width = 19, Height = 20 });
				}
				IHGHtmlTable imagesTable = new IHGHtmlTable { Border = 0, CellSpacing = 0, CellPadding = 0 };
				imagesTable.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(images), new IHGHtmlTableCell(d.Name)));
				
				IHGHtmlTableCell imageCell = new IHGHtmlTableCell(imagesTable);
				row.Cells.Add(imageCell);
				table.Rows.Add(row);
			}
			Org.Controls.Add(table);
		}

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	foreach (var d in departments) {
        		var ld = ((DropDownList)Org.FindControl("LDID" + d.Id)).SelectedValue;
        		var lw = ((DropDownList)Org.FindControl("LWID" + d.Id)).SelectedValue;
        		departmentRepository.UpdateLoginSettings(ld, lw, d.Id);
        	}
        }
	}
}