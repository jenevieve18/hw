using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class Reminders : System.Web.UI.Page
	{
		IDepartmentRepository departmentRepository;
		IList<Department> departments;
		int lid;
		
		public Reminders() : this(new SqlDepartmentRepository())
		{
		}
		
		public Reminders(IDepartmentRepository departmentRepository)
		{
			this.departmentRepository = departmentRepository;
		}
		
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			buttonSave.Text = R.Str(lid, "save", "Save");
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SponsorID"] == null, "default.aspx", true);
			
			HtmlHelper.RedirectIf(!new SqlSponsorAdminRepository().SponsorAdminHasAccess(ConvertHelper.ToInt32(Session["SponsorAdminID"]), ManagerFunction.Reminders), "default.aspx", true);
			
			lid = ConvertHelper.ToInt32(Session["lid"], 2);
			
			Index(ConvertHelper.ToInt32(Session["SponsorID"]), ConvertHelper.ToInt32(Session["SponsorAdminID"]));
		}
		
		public void Index(int sponsorID, int sponsorAdminID)
		{
			departments = departmentRepository.FindBySponsorWithSponsorAdminInDepth(sponsorID, sponsorAdminID);

			Org.Controls.Add(new LiteralControl("<br>"));
			
			IHGHtmlTable table = new IHGHtmlTable { Border = 0, CellSpacing = 0, CellPadding = 3 };

            var r = new IHGHtmlTableRow(
                new IHGHtmlTableCell(R.Str(lid, "reminder.interval", "Reminder interval"), true),
                new IHGHtmlTableCell(R.Str(lid, "reminder.check", "System checks"), true),
                new IHGHtmlTableCell(R.Str(lid, "unit.id", "Unit ID"), true),
                new IHGHtmlTableCell(R.Str(lid, "unit", "Unit"), true)
            );
            r.Attributes.Add("style", "border-bottom:1px solid #333333;");
            table.Rows.Add(r);

            table.Rows.Add(
                new IHGHtmlTableRow(
                    new IHGHtmlTableCell(Session["Sponsor"].ToString()) { ColSpan = 3 }
                )
            );
            
            Dictionary<int, bool> DX = new Dictionary<int, bool>();

            int j = 0;
            
            Dictionary<string, string> loginDays = new Dictionary<string, string>();
            loginDays.Add("NULL", R.Str(lid, "week.same", "< same as parent >"));
            loginDays.Add("1", R.Str(lid, "day.everyday", "every day"));
            loginDays.Add("7", R.Str(lid, "week", "week"));
            loginDays.Add("14", R.Str(lid, "week.two", "2 weeks"));
            loginDays.Add("30", R.Str(lid, "month", "month"));
            loginDays.Add("90", R.Str(lid, "month.three", "3 months"));
            loginDays.Add("180", R.Str(lid, "month.six", "6 months"));

            Dictionary<string, string> loginWeekDays = new Dictionary<string, string>();
            loginWeekDays.Add("NULL", R.Str(lid, "week.same", "< same as parent >"));
            loginWeekDays.Add("-1", R.Str(lid, "week.disabled", "< disabled >"));
            loginWeekDays.Add("0", R.Str(lid, "week.everyday", "< every day >"));
            loginWeekDays.Add("1", R.Str(lid, "week.monday", "Monday"));
            loginWeekDays.Add("2", R.Str(lid, "week.tuesday", "Tuesday"));
            loginWeekDays.Add("3", R.Str(lid, "week.wednesday", "Wednesday"));
            loginWeekDays.Add("4", R.Str(lid, "week.thursday", "Thursday"));
            loginWeekDays.Add("5", R.Str(lid, "week.friday", "Friday"));

			foreach (var d in departments) {
                IHGHtmlTable boxes = new IHGHtmlTable() { Width = "100%" };
                if (j > 0)
                {
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
                    boxes.Rows.Add(
                        new IHGHtmlTableRow(
                            new IHGHtmlTableCell(ld),
                            new IHGHtmlTableCell(lw)
                        )
                    );
                }
                else
                {
                    boxes.Rows.Add(
                        new IHGHtmlTableRow(
                            new IHGHtmlTableCell(loginDays[d.LoginDays.ToString()]) { Width = "50%" },
                            new IHGHtmlTableCell(loginWeekDays[d.LoginWeekDay.ToString()])
                        )
                    );
                }
                j++;

                IHGHtmlTableRow row = new IHGHtmlTableRow(
                    new IHGHtmlTableCell(boxes) { ColSpan = 2 },
                    new IHGHtmlTableCell(d.ShortName.ToString())
                );
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
        	Save();
        }
        
        public void Save()
        {
        	foreach (var d in departments) {
        		var ld = ((DropDownList)Org.FindControl("LDID" + d.Id)).SelectedValue;
        		var lw = ((DropDownList)Org.FindControl("LWID" + d.Id)).SelectedValue;
        		departmentRepository.UpdateLoginSettings(ld, lw, d.Id);
        	}
        }
	}
}