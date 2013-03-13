using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HWgrp.Web
{
	public partial class Stats : System.Web.UI.Page
	{
		protected IList<ReportPartLanguage> reportParts = null;
		protected IList<BaseModel> urlModels;
		IList<Department> departments;
		IList<SponsorBackgroundQuestion> questions;
		int sponsorID = 0;
		int sponsorAdminID = 0;
		ILanguageRepository langRepository = AppContext.GetRepositoryFactory().CreateLanguageRepository();
		IProjectRepository projRepository = AppContext.GetRepositoryFactory().CreateProjectRepository();
		ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		IDepartmentRepository departmentRepository = AppContext.GetRepositoryFactory().CreateDepartmentRepository();
		IReportRepository reportRepository = AppContext.GetRepositoryFactory().CreateReportRepository();
		
		public IList<SponsorProjectRoundUnitLanguage> Languages {
			set {
				int pruid = 0;
				foreach (var l in value) {
					dropDownLanguages.Items.Add(new ListItem(l.Language.Name, l.Language.Id.ToString()));
					if (l.Id == 2) {
						dropDownLanguages.SelectedValue = l.Language.Id.ToString();
					}
					pruid = l.SponsorProjectRoundUnit.ProjectRoundUnit.Id;
				}
			}
		}
		
		public IList<SponsorProjectRoundUnit> ProjectRoundUnits {
			set {
				foreach (var p in value) {
					dropDownListProjectRoundUnits.Items.Add(new ListItem(p.Navigation, p.ProjectRoundUnit.Id.ToString()));
				}
				dropDownGroupBy.SelectedValue = "7";
			}
		}
		
		public IList<SponsorBackgroundQuestion> BackgroundQuestions {
			set {
				this.questions = value;
				checkBoxQuestions.Items.Clear();
				foreach (var s in questions) {
					checkBoxQuestions.Items.Add(new ListItem(s.Question.Internal, s.Id.ToString()));
				}
				checkBoxQuestions.SelectedIndex = 0;
			}
		}
		
		public IList<Department> Departments {
			set {
				this.departments = value;
				tableDepartment.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(Session["Sponsor"].ToString()) { ColSpan = 3 }));
				tableDepartment.Departments = value;
			}
		}
		
		IList<BaseModel> SelectedDepartments {
			get {
				var selectedDepartments = new List<BaseModel>();
				foreach (var d in departments) {
					if (((CheckBox)tableDepartment.FindControl("DID" + d.Id)).Checked) {
						selectedDepartments.Add(d);
					}
				}
				return selectedDepartments;
			}
		}
		
		IList<BaseModel> SelectedQuestions {
			get {
				var selectedQuestions = new List<BaseModel>();
				foreach (var q in questions) {
//				foreach (var q in sponsorRepository.FindBySponsor(sponsorID)) {
					if (checkBoxQuestions.Items.FindByValue(q.Id.ToString()).Selected) {
						selectedQuestions.Add(q);
					}
				}
				return selectedQuestions;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["SponsorID"] != null) {
				sponsorID = Convert.ToInt32(Session["SponsorID"]);
				sponsorAdminID = Convert.ToInt32(Session["SponsorAdminID"]);
				
				if (IsPostBack) {
					BackgroundQuestions = sponsorRepository.FindBySponsor(sponsorID);
					Departments = departmentRepository.FindBySponsorWithSponsorAdminInDepth(sponsorID, sponsorAdminID);
				} else {
					Languages = langRepository.FindBySponsor(sponsorID);

					int selectedLangID = Convert.ToInt32(dropDownLanguages.SelectedValue);
					ProjectRoundUnits = sponsorRepository.FindBySponsorAndLanguage(sponsorID, selectedLangID);

					for (int i = 2005; i <= DateTime.Now.Year; i++) {
						dropDownYearFrom.Items.Add(new ListItem(i.ToString(), i.ToString()));
						dropDownYearTo.Items.Add(new ListItem(i.ToString(), i.ToString()));
					}
					dropDownYearFrom.SelectedValue = (DateTime.Now.Year - 1).ToString();
					dropDownYearTo.SelectedValue = DateTime.Now.Year.ToString();
				}
				Execute.Click += new EventHandler(Execute_Click);
			} else {
				Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
		}
		
//		public void SetReportPartLanguages(IList<ReportPartLanguage> reportParts, IList<BaseModel> urlModels)
//		{
//			this.reportParts = reportParts;
//			var selectedDepartments = SelectedDepartments;
//			string URL = GetURL(urlModels);
//			int cx = 0;
//
//			if (selectedDepartments.Count == 1) {
//				var allNone = new CheckBox() { ID = "selectAll" };
//
//				IHGRadioButtonList plotType = new IHGRadioButtonList();
//				plotType.ID = "graphTypes";
//				plotType.Items.Add(new ListItem("Line Plot", "LinePlot") { Selected = true });
//				plotType.Items.Add(new ListItem("Box Plot", "BoxPlot"));
//				StatsImg.Controls.Add(allNone);
//				StatsImg.Controls.Add(plotType);
//			} else {
//				StatsImg.Controls.Add(new LiteralControl(""));
//			}
//
//			foreach (var r in reportParts) {
//				if (cx == 0) {
//					StatsImg.Controls.Add(new LiteralControl("<div>&nbsp;<br>&nbsp;<br></div>"));
//				} else {
//					StatsImg.Controls.Add(new LiteralControl("<div style='page-break-before:always;'>&nbsp;<br>&nbsp;<br></div>"));
//				}
//
//				string imgID = "img" + cx;
//
//				HtmlTable table = new HtmlTable { Border = 0, CellSpacing = 0, CellPadding = 0 };
//				IHGHtmlTableCell headerCell = new IHGHtmlTableCell(r.Subject) { Align = "Center", VAlign = "Middle", Height = "140", FontSize = "24px" };
//				table.Rows.Add(new IHGHtmlTableRow(headerCell));
//
//				IHGHtmlTableCell subjectCell = new IHGHtmlTableCell() { FontSize = "18px" };
//				if (selectedDepartments.Count == 1) {
//					subjectCell.Controls.Add(new CheckBox() { ID = "chk" + cx });
//					subjectCell.Controls.Add(new LiteralControl(r.Subject));
//					subjectCell.Controls.Add(new HtmlInputHidden() { ID = "reportUrl", Value = GetReportImageUrl(r.ReportPart.Id, r.Id, "reportImage", URL) });
//					subjectCell.Controls.Add(new HtmlInputHidden() { ID = "exportPdfUrl", Value = GetReportImageUrl(r.ReportPart.Id, r.Id, "Export", URL + "&type=pdf") });
//					subjectCell.Controls.Add(new HtmlInputHidden() { ID = "exportCsvUrl", Value = GetReportImageUrl(r.ReportPart.Id, r.Id, "Export", URL + "&type=csv") });
//				} else {
//					subjectCell.Controls.Add(new LiteralControl(r.Subject));
//				}
//				table.Rows.Add(new IHGHtmlTableRow(subjectCell));
//
//				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(r.Header.Replace("\r", "").Replace("\n", "<br>"))));
//
//				var img = new HtmlImage { ID = imgID, Src = GetReportImageUrl(r.ReportPart.Id, r.Id, "reportImage", URL) };
//				img.Attributes.Add("class", "img");
//				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(img)));
//
//				var exportPdfAnchor = new HtmlAnchor { Target = "_blank", HRef = GetReportImageUrl(r.ReportPart.Id, r.Id, "Export", URL + "&type=pdf") };
//				exportPdfAnchor.Attributes.Add("class", "exportPdfAnchor");
//				exportPdfAnchor.Controls.Add(new HtmlImage { Src = "images/page_white_acrobat.png" });
//
//				var exportCsvAnchor= new HtmlAnchor { Target = "_blank", HRef = GetReportImageUrl(r.ReportPart.Id, r.Id, "Export", URL + "&type=csv") };
//				exportCsvAnchor.Attributes.Add("class", "exportCsvAnchor");
//				exportCsvAnchor.Controls.Add(new HtmlImage { Src = "images/page_white_excel.png" });
//
//				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(exportPdfAnchor, new LiteralControl("&nbsp;"), exportCsvAnchor)));
//
//				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(r.Footer.Replace("\r", "").Replace("\n", "<br>"))));
//				StatsImg.Controls.Add(table);
//				cx++;
//			}
//		}

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
		
		protected string GetReportImageUrl(int reportID, int reportPartLangID, string page, string URL)
		{
			string plotQuery = Request.QueryString["Plot"] != null ? "&Plot=" + Request["Plot"] : "";
			string reportImageUrl = string.Format(
				"{13}.aspx?LangID={0}&FY={1}&TY={2}&SAID={3}&SID={4}&{5}STDEV={6}&ExtraPoint={14}&GB={7}&RPID={8}&RPLID={15}&PRUID={9}{10}&GRPNG={11}{12}",
				Convert.ToInt32(dropDownLanguages.SelectedValue),
				dropDownYearFrom.SelectedValue,
				dropDownYearTo.SelectedValue,
				sponsorAdminID,
				sponsorID,
				Convert.ToInt32(Session["Anonymized"]) == 1 ? "Anonymized=1&" : "",
//				STDEV.Checked ? "1" : "0",
				"1",
				dropDownGroupBy.SelectedValue,
				reportID,
				dropDownListProjectRoundUnits.SelectedValue,
				URL,
				Convert.ToInt32(dropDownGrouping.SelectedValue),
				plotQuery,
				page,
				dropDownDistribution.SelectedValue,
				reportPartLangID
			);
			return reportImageUrl;
		}
		
		protected override void OnPreRender(EventArgs e)
		{
			Org.Visible = (dropDownGrouping.SelectedValue == "1" || dropDownGrouping.SelectedValue == "2");
			checkBoxQuestions.Visible = (dropDownGrouping.SelectedValue == "3");
		}
		
		protected string GetURL(IList<BaseModel> models)
		{
			string URL = "";
			foreach (var m in models) {
				URL += "," + m.Id;
			}
			URL = "&GID=0" + URL;
			return URL;
		}

		void Execute_Click(object sender, EventArgs e)
		{
			int selectedLangID = Convert.ToInt32(dropDownLanguages.SelectedValue);
			int selectedProjectRoundUnitID = Convert.ToInt32(dropDownListProjectRoundUnits.SelectedValue);
			int grouping = Convert.ToInt32(dropDownGrouping.SelectedValue);
			
			reportParts = reportRepository.FindByProjectAndLanguage(selectedProjectRoundUnitID, selectedLangID);
			urlModels = GetUrlModels(grouping);
//			SetReportPartLanguages(reportParts, GetUrlModels(grouping));
		}
	}
}