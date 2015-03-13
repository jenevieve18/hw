using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class Stats : System.Web.UI.Page
	{
		protected IList<ReportPartLanguage> reportParts = null;
		protected IList<BaseModel> urlModels;
		protected IList<PlotTypeLanguage> plotTypes = new List<PlotTypeLanguage>();
		IList<Department> departments;
		IList<SponsorBackgroundQuestion> questions;
		int sponsorID = 0;
		int sponsorAdminID = 0;
//		SqlLanguageRepository langRepository = new SqlLanguageRepository();
		SqlProjectRepository projRepository = new SqlProjectRepository();
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		SqlDepartmentRepository departmentRepository = new SqlDepartmentRepository();
		SqlReportRepository reportRepository = new SqlReportRepository();
		SqlPlotTypeRepository plotRepository = new SqlPlotTypeRepository();
        protected int lid;
        protected DateTime startDate;
        protected DateTime endDate;

//		public IList<SponsorProjectRoundUnitLanguage> Languages {
//			set {
//				int pruid = 0;
//				foreach (var l in value) {
//					LangID.Items.Add(new ListItem(l.Language.Name, l.Language.Id.ToString()));
//					if (l.Id == 2) {
//						LangID.SelectedValue = l.Language.Id.ToString();
//					}
//					pruid = l.SponsorProjectRoundUnit.ProjectRoundUnit.Id;
//				}
//			}
//		}
		
		public IList<SponsorProjectRoundUnit> ProjectRoundUnits {
			set {
        		ProjectRoundUnitID.Items.Clear();
				foreach (var p in value) {
					ProjectRoundUnitID.Items.Add(new ListItem(p.Navigation, p.ProjectRoundUnit.Id.ToString()));
				}
				GroupBy.SelectedValue = "7";
			}
		}
		
		public IList<SponsorBackgroundQuestion> BackgroundQuestions {
			set {
				this.questions = value;
				BQ.Items.Clear();
				foreach (var s in questions) {
					BQ.Items.Add(new ListItem(s.BackgroundQuestion.Internal, s.Id.ToString()));
				}
				BQ.SelectedIndex = 0;
			}
		}
		
		public IList<Department> Departments {
			set {
				this.departments = value;
				Org.Controls.Add(new LiteralControl("<br>"));
				IHGHtmlTable table = new IHGHtmlTable { Border = 0, CellSpacing = 0, CellPadding = 0 };
				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(Session["Sponsor"].ToString()) { ColSpan = 3 }));
//				bool[] DX = new bool[8];
				Dictionary<int, bool> DX = new Dictionary<int, bool>();
				foreach (var d in departments) {
					//IHGHtmlTableRow row = new IHGHtmlTableRow(new IHGHtmlTableCell(new CheckBox { ID = "DID" + d.Id }), new IHGHtmlTableCell(d.Name));
                    IHGHtmlTableRow row = new IHGHtmlTableRow(new IHGHtmlTableCell(new CheckBox { ID = "DID" + d.Id }), new IHGHtmlTableCell(d.ShortName.ToString()));
					
					int depth = d.Depth;
//					if (!DX.ContainsKey(depth)) {
//						DX[depth] = d.Siblings > 0;
//					}
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
			get {
				return departments;
			}
		}
		
		protected IList<BaseModel> SelectedDepartments {
			get {
				var selectedDepartments = new List<BaseModel>();
//				var departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID);
				foreach (var d in departments) {
					if (((CheckBox)Org.FindControl("DID" + d.Id.ToString())).Checked) {
						selectedDepartments.Add(d);
					}
				}
				return selectedDepartments;
			}
		}
		
		IList<BaseModel> SelectedQuestions {
			get {
				var selectedQuestions = new List<BaseModel>();
//				foreach (var q in questions) {
				foreach (var q in sponsorRepository.FindBySponsor(sponsorID)) {
					if (BQ.Items.FindByValue(q.Id.ToString()).Selected) {
						selectedQuestions.Add(q);
					}
				}
				return selectedQuestions;
			}
		}

		IList<BaseModel> GetUrlModels(int grouping)
		{
			switch (grouping) {
				case 1:
				case 2:
					return SelectedDepartments;
				case 3:
					return SelectedQuestions;
				default:
					return new List<BaseModel>();
			}
		}
		
		public void SetReportPartLanguages(IList<ReportPartLanguage> reportParts, IList<BaseModel> urlModels)
		{
			this.reportParts = reportParts;
			this.urlModels = urlModels;
			var selectedDepartments = SelectedDepartments;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
            sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
            lid = ConvertHelper.ToInt32(Session["lid"], 1);
			
//			plotTypes = plotRepository.FindAll();
            plotTypes = plotRepository.FindByLanguage(lid);
			
			sponsorRepository.SaveSponsorAdminSessionFunction(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Statistics, DateTime.Now);
			if (sponsorID != 0) {
				if (!IsPostBack) {
					
//					startDate = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day).ToString("yyyy MMM");
//					endDate = DateTime.Now.ToString("yyyy MMM");
					startDate = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
					endDate = DateTime.Now;
					
					GroupBy.Items.Clear();
					GroupBy.Items.Add(new ListItem(R.Str(lid, "week.one", "One week"), "1"));
					GroupBy.Items.Add(new ListItem(R.Str(lid, "week.two.even", "Two weeks, start with even"), "7"));
					GroupBy.Items.Add(new ListItem(R.Str(lid, "week.two.odd", "Two weeks, start with odd"), "2"));
					GroupBy.Items.Add(new ListItem(R.Str(lid, "month.one", "One month"), "3"));
					GroupBy.Items.Add(new ListItem(R.Str(lid, "month.three", "Three months"), "4"));
					GroupBy.Items.Add(new ListItem(R.Str(lid, "month.six", "Six months"), "5"));
					GroupBy.Items.Add(new ListItem(R.Str(lid, "year.one", "One year"), "6"));
					
					Grouping.Items.Clear();
					Grouping.Items.Add(new ListItem(R.Str(lid, "", "< none >"), "0"));
					Grouping.Items.Add(new ListItem(R.Str(lid, "users.unit", "Users on unit"), "1"));
					Grouping.Items.Add(new ListItem(R.Str(lid, "users.unit.subunit", "Users on unit+subunits"), "2"));
					Grouping.Items.Add(new ListItem(R.Str(lid, "background.variable", "Background variable"), "3"));
					
//					Languages = langRepository.FindBySponsor(sponsorID);

//					int selectedLangID = Convert.ToInt32(LangID.SelectedValue);
//					ProjectRoundUnits = sponsorRepository.FindBySponsorAndLanguage(sponsorID, selectedLangID);
					ProjectRoundUnits = sponsorRepository.FindBySponsorAndLanguage(sponsorID, lid);

//					FromYear.Items.Clear();
//					ToYear.Items.Clear();
//					for (int i = 2005; i <= DateTime.Now.Year; i++) {
//						FromYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
//						ToYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
//					}
//					FromYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
//					ToYear.SelectedValue = DateTime.Now.Year.ToString();
					
					BackgroundQuestions = sponsorRepository.FindBySponsor(sponsorID);
				} else {
//					startDate = Request.Form["startDate"];
//					endDate = Request.Form["endDate"];
					startDate = GetDateFromString(Request.Form["startDate"]);
					endDate = GetDateFromString(Request.Form["endDate"]);
				}
				Departments = departmentRepository.FindBySponsorWithSponsorAdminInDepth(sponsorID, sponsorAdminID);
			} else {
				Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
			Execute.Click += new EventHandler(Execute_Click);
		}
		
		DateTime GetDateFromString(string s)
		{
			DateTime d = DateTime.Now;
			try {
				d = DateTime.ParseExact(s, "yyyy MMM", CultureInfo.InvariantCulture);
			} catch {}
			return d;
		}
		
//		protected string GetReportImageUrl(int reportID, int reportPartLangID, Q additionalQuery, int startYear, int endYear)
		protected string GetReportImageUrl(int reportID, int reportPartLangID, Q additionalQuery)
		{
//			var p = GetPage("reportImage.aspx", reportID, reportPartLangID, startYear, endYear);
			var p = GetPage("reportImage.aspx", reportID, reportPartLangID);
			p.Add(additionalQuery);
			return p.ToString();
		}
		
//		protected string GetExportUrl(int reportID, int reportPartID, string type, Q additionalQuery, int startYear, int endYear)
		protected string GetExportUrl(int reportID, int reportPartID, string type, Q additionalQuery)
		{
//			var p = GetPage("Export.aspx", reportID, reportPartID, startYear, endYear);
			var p = GetPage("Export.aspx", reportID, reportPartID);
			p.Q.Add("TYPE", type);
			p.Add(additionalQuery);
			return p.ToString();
		}
		
//		protected string GetExportAllUrl(string type, Q additionalQuery, int startYear, int endYear)
		protected string GetExportAllUrl(string type, Q additionalQuery)
		{
//			var p = GetPage("ExportAll.aspx", 0, 0, startYear, endYear);
			var p = GetPage("ExportAll.aspx", 0, 0);
			p.Q.Add("TYPE", type);
			p.Add(additionalQuery);
			return p.ToString();
		}
		
		protected override void OnPreRender(EventArgs e)
        {
            Execute.Text = R.Str(lid, "execute", "Execute");

			Org.Visible = (Grouping.SelectedValue == "1" || Grouping.SelectedValue == "2");
			BQ.Visible = (Grouping.SelectedValue == "3");
		}
		
		protected string GetURL(IList<BaseModel> models)
		{
			string url = "";
			foreach (var m in models) {
				url += "," + m.Id;
			}
			url = "&GID=0" + url;
			return url;
		}
		
		protected Q GetGID(IList<BaseModel> models)
		{
			string v = "";
			foreach (var m in models) {
				v += "," + m.Id;
			}
			var q = new Q();
			q.Add("GID", "0" + v);
			return q;
		}
		
//		P GetPage(string page, int reportID, int reportPartLangID, int startYear, int endYear)
		P GetPage(string page, int reportID, int reportPartLangID)
		{
			P p = new P(page);
//			p.Q.Add("LangID", LangID.SelectedValue);
			p.Q.Add("LangID", lid);
//			p.Q.Add("FY", FromYear.SelectedValue);
//			p.Q.Add("TY", ToYear.SelectedValue);
			p.Q.Add("FY", startDate.Year);
			p.Q.Add("TY", endDate.Year);
			p.Q.Add("FM", startDate.Month);
			p.Q.Add("TM", endDate.Month);
			p.Q.Add("SAID", sponsorAdminID);
			p.Q.Add("SID", sponsorID);
			p.Q.AddIf(Convert.ToInt32(Session["Anonymized"]) == 1, "Anonymized", 1);
			p.Q.Add("GB", GroupBy.SelectedValue);
			p.Q.Add("RPID", reportID);
			p.Q.Add("RPLID", reportPartLangID);
			p.Q.Add("PRUID", ProjectRoundUnitID.SelectedValue);
			p.Q.Add("GRPNG", Grouping.SelectedValue);
			p.Q.AddIf(Request.QueryString["PLOT"] != null, "PLOT", Request.QueryString["PLOT"]);
			return p;
		}

		void Execute_Click(object sender, EventArgs e)
		{
//			int selectedLangID = Convert.ToInt32(LangID.SelectedValue);
			int selectedProjectRoundUnitID = Convert.ToInt32(ProjectRoundUnitID.SelectedValue);
			int grouping = Convert.ToInt32(Grouping.SelectedValue);
			
//			plotTypes = plotRepository.FindByLanguage(selectedLangID);
//			plotTypes = plotRepository.FindByLanguage(lid);
			
			int selectedDepartmentID = departments[0].Id;
//			var reportParts = reportRepository.FindByProjectAndLanguage2(selectedProjectRoundUnitID, selectedLangID, selectedDepartmentID);
			var reportParts = reportRepository.FindByProjectAndLanguage2(selectedProjectRoundUnitID, lid, selectedDepartmentID);
			if (reportParts.Count <= 0) {
//				reportParts = reportRepository.FindByProjectAndLanguage(selectedProjectRoundUnitID, selectedLangID);
				reportParts = reportRepository.FindByProjectAndLanguage(selectedProjectRoundUnitID, lid);
			}
			SetReportPartLanguages(reportParts, GetUrlModels(grouping));
		}
	}
}