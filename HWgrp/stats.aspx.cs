//	<file>
//		<license></license>
//		<owner name="Jens Pettersson" email=""/>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HW.Core;

namespace HWgrp
{
	public partial class stats : System.Web.UI.Page
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
					LangID.Items.Add(new ListItem(l.Language.Name, l.Language.Id.ToString()));
					if (l.Id == 2) {
						LangID.SelectedValue = l.Language.Id.ToString();
					}
					pruid = l.SponsorProjectRoundUnit.ProjectRoundUnit.Id;
				}
			}
		}
		
		public IList<SponsorProjectRoundUnit> ProjectRoundUnits {
			set {
				foreach (var p in value) {
					ProjectRoundUnitID.Items.Add(new ListItem(p.Navigation, p.ProjectRoundUnit.Id.ToString()));
				}
				GroupBy.SelectedValue = "7";
			}
		}
		
		public IList<SponsorBackgroundQuestion> BackgroundQuestions {
			set {
				this.questions = value;
				foreach (var s in questions) {
					BQ.Items.Add(new ListItem(s.Question.Internal, s.Id.ToString()));
				}
				BQ.SelectedIndex = 0;
			}
		}
		
		public IList<Department> Departments {
			set {
				this.departments = value;
				Org.Controls.Add(new LiteralControl("<br>"));
				IHGHtmlTable table = new IHGHtmlTable { Border = 0, CellSpacing = 0, CellPadding = 0 };
				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(HttpContext.Current.Session["Sponsor"].ToString()) { ColSpan = 3 }));
				bool[] DX = new bool[8];
				foreach (var d in departments) {
					IHGHtmlTableRow row = new IHGHtmlTableRow(new IHGHtmlTableCell(new CheckBox { ID = "DID" + d.Id }), new IHGHtmlTableCell(d.Name));
					
					int depth = d.Depth;
					DX[depth] = d.Siblings > 0;

					IList<Control> images = new List<Control>();
					for (int i = 1; i <= depth; i++) {
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
		}
		
		IList<BaseModel> SelectedDepartments {
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

		protected void Page_Load(object sender, EventArgs e)
		{
			sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
			sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
			
			if (sponsorID != 0) {
				if (!IsPostBack) {
					Languages = langRepository.FindBySponsor(sponsorID);

					int selectedLangID = Convert.ToInt32(LangID.SelectedValue);
					ProjectRoundUnits = sponsorRepository.FindBySponsorAndLanguage(sponsorID, selectedLangID);

					for (int i = 2005; i <= DateTime.Now.Year; i++) {
						FromYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
						ToYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
					}
					FromYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
					ToYear.SelectedValue = DateTime.Now.Year.ToString();
					
					BackgroundQuestions = sponsorRepository.FindBySponsor(sponsorID);
				}
				Departments = departmentRepository.FindBySponsorWithSponsorAdminInDepth(sponsorID, sponsorAdminID);
			} else {
				HttpContext.Current.Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
			Execute.Click += new EventHandler(Execute_Click);
		}
		
		public void SetReportPartLanguages(IList<ReportPartLanguage> reportParts, IList<BaseModel> urlModels)
		{
			this.reportParts = reportParts;
			this.urlModels = urlModels;
			var selectedDepartments = SelectedDepartments;
			string URL = GetURL(urlModels);
			int cx = 0;
			
			if (selectedDepartments.Count == 1) {
				var allNone = new CheckBox() { ID = "selectAll" };
				
				IHGRadioButtonList plotType = new IHGRadioButtonList();
				plotType.ID = "graphTypes";
				plotType.Items.Add(new ListItem("Line Plot", "LinePlot") { Selected = true });
				plotType.Items.Add(new ListItem("Box Plot", "BoxPlot"));
				StatsImg.Controls.Add(allNone);
				StatsImg.Controls.Add(plotType);
			} else {
				StatsImg.Controls.Add(new LiteralControl(""));
			}
			
			foreach (var r in reportParts) {
				if (cx == 0) {
					StatsImg.Controls.Add(new LiteralControl("<div>&nbsp;<br>&nbsp;<br></div>"));
				} else {
					StatsImg.Controls.Add(new LiteralControl("<div style='page-break-before:always;'>&nbsp;<br>&nbsp;<br></div>"));
				}
				
				string imgID = "img" + cx;

				HtmlTable table = new HtmlTable { Border = 0, CellSpacing = 0, CellPadding = 0 };
				IHGHtmlTableCell headerCell = new IHGHtmlTableCell(r.Subject) { Align = "Center", VAlign = "Middle", Height = "140", FontSize = "24px" };
				table.Rows.Add(new IHGHtmlTableRow(headerCell));
				
				IHGHtmlTableCell subjectCell = new IHGHtmlTableCell() { FontSize = "18px" };
				if (selectedDepartments.Count == 1) {
					subjectCell.Controls.Add(new CheckBox() { ID = "chk" + cx });
					subjectCell.Controls.Add(new LiteralControl(r.Subject));
					subjectCell.Controls.Add(new HtmlInputHidden() { ID = "reportUrl", Value = GetReportImageUrl(r.ReportPart.Id, "reportImage", URL) });
					subjectCell.Controls.Add(new HtmlInputHidden() { ID = "exportPdfUrl", Value = GetReportImageUrl(r.ReportPart.Id, "Export", URL + "&type=pdf") });
					subjectCell.Controls.Add(new HtmlInputHidden() { ID = "exportCsvUrl", Value = GetReportImageUrl(r.ReportPart.Id, "Export", URL + "&type=csv") });
				} else {
					subjectCell.Controls.Add(new LiteralControl(r.Subject));
				}
				table.Rows.Add(new IHGHtmlTableRow(subjectCell));

				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(r.Header.Replace("\r", "").Replace("\n", "<br>"))));
				
				var img = new HtmlImage { ID = imgID, Src = GetReportImageUrl(r.ReportPart.Id, "reportImage", URL) };
				img.Attributes.Add("class", "img");
				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(img)));
				
				var exportPdfAnchor = new HtmlAnchor { Target = "_blank", HRef = GetReportImageUrl(r.ReportPart.Id, "Export", URL + "&type=pdf") };
				exportPdfAnchor.Attributes.Add("class", "exportPdfAnchor");
				exportPdfAnchor.Controls.Add(new HtmlImage { Src = "images/page_white_acrobat.png" });
				
				var exportCsvAnchor= new HtmlAnchor { Target = "_blank", HRef = GetReportImageUrl(r.ReportPart.Id, "Export", URL + "&type=csv") };
				exportCsvAnchor.Attributes.Add("class", "exportCsvAnchor");
				exportCsvAnchor.Controls.Add(new HtmlImage { Src = "images/page_white_excel.png" });
				
				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(exportPdfAnchor, new LiteralControl("&nbsp;"), exportCsvAnchor)));
				
				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(r.Footer.Replace("\r", "").Replace("\n", "<br>"))));
				StatsImg.Controls.Add(table);
				cx++;
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
		
		protected string GetReportImageUrl(int reportID, string page, string URL)
		{
			string plotQuery = HttpContext.Current.Request.QueryString["Plot"] != null ? "&Plot=" + HttpContext.Current.Request["Plot"] : "";
			string reportImageUrl = string.Format(
				"{13}.aspx?LangID={0}&FY={1}&TY={2}&SAID={3}&SID={4}&{5}STDEV={6}&GB={7}&RPID={8}&PRUID={9}{10}&GRPNG={11}{12}",
				Convert.ToInt32(LangID.SelectedValue),
				FromYear.SelectedValue,
				ToYear.SelectedValue,
				sponsorAdminID,
				sponsorID,
				Convert.ToInt32(HttpContext.Current.Session["Anonymized"]) == 1 ? "Anonymized=1&" : "",
				STDEV.Checked ? "1" : "0",
				GroupBy.SelectedValue,
				reportID,
				ProjectRoundUnitID.SelectedValue,
				URL,
				Convert.ToInt32(Grouping.SelectedValue),
				plotQuery,
				page
			);
			return reportImageUrl;
		}
		
		protected override void OnPreRender(EventArgs e)
		{
			Org.Visible = (Grouping.SelectedValue == "1" || Grouping.SelectedValue == "2");
			BQ.Visible = (Grouping.SelectedValue == "3");
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
			int selectedLangID = Convert.ToInt32(LangID.SelectedValue);
			int selectedProjectRoundUnitID = Convert.ToInt32(ProjectRoundUnitID.SelectedValue);
			int grouping = Convert.ToInt32(Grouping.SelectedValue);
			
			var reportParts = reportRepository.FindByProjectAndLanguage(selectedProjectRoundUnitID, selectedLangID);
			SetReportPartLanguages(reportParts, GetUrlModels(grouping));
		}
	}
}