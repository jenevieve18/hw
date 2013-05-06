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
					checkBoxQuestions.Items.Add(new ListItem(s.BackgroundQuestion.Internal, s.Id.ToString()));
				}
				checkBoxQuestions.SelectedIndex = 0;
			}
		}
		
		public IList<Department> Departments {
			set {
				this.departments = value;
				tableDepartments.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(Session["Sponsor"].ToString()) { ColSpan = 3 }));
				tableDepartments.Departments = value;
			}
		}
		
		protected IList<BaseModel> SelectedDepartments {
			get {
				var selectedDepartments = new List<BaseModel>();
				foreach (var d in departments) {
					if (((CheckBox)tableDepartments.FindControl("DID" + d.Id)).Checked) {
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
					if (checkBoxQuestions.Items.FindByValue(q.Id.ToString()).Selected) {
						selectedQuestions.Add(q);
					}
				}
				return selectedQuestions;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SponsorID"] == null, "default.aspx", true);
			
			sponsorID = Convert.ToInt32(Session["SponsorID"]);
			sponsorAdminID = Convert.ToInt32(Session["SponsorAdminID"]);
			
			if (!IsPostBack) {
				Languages = langRepository.FindBySponsor(sponsorID);

				int selectedLangID = Convert.ToInt32(dropDownLanguages.SelectedValue);
				ProjectRoundUnits = sponsorRepository.FindBySponsorAndLanguage(sponsorID, selectedLangID);

				for (int i = 2005; i <= DateTime.Now.Year; i++) {
					dropDownYearFrom.Items.Add(new ListItem(i.ToString(), i.ToString()));
					dropDownYearTo.Items.Add(new ListItem(i.ToString(), i.ToString()));
				}
				dropDownYearFrom.SelectedValue = (DateTime.Now.Year - 1).ToString();
				dropDownYearTo.SelectedValue = DateTime.Now.Year.ToString();
				BackgroundQuestions = sponsorRepository.FindBySponsor(sponsorID);
			} else {
				Departments = departmentRepository.FindBySponsorWithSponsorAdminInDepth(sponsorID, sponsorAdminID);
			}
			Execute.Click += new EventHandler(Execute_Click);
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
		
		protected string GetReportImageUrl(int reportID, int reportPartLangID, Q additionalQuery)
		{
			var p = GetPage("reportImage.aspx", reportID, reportPartLangID);
			p.Add(additionalQuery);
			return p.ToString();
		}
		
		protected string GetExportUrl(int reportID, int reportPartID, string type, Q additionalQuery)
		{
			var p = GetPage("Export.aspx", reportID, reportPartID);
			p.Q.Add("TYPE", type);
			p.Add(additionalQuery);
			return p.ToString();
		}
		
		protected string GetExportAllUrl(string type, Q additionalQuery)
		{
			var p = GetPage("ExportAll.aspx", 0, 0);
			p.Q.Add("TYPE", type);
			p.Add(additionalQuery);
			return p.ToString();
		}
		
		P GetPage(string page, int reportID, int reportPartLangID)
		{
			P p = new P(page);
			p.Q.Add("LangID", dropDownLanguages.SelectedValue);
			p.Q.Add("FY", dropDownYearFrom.SelectedValue);
			p.Q.Add("TY", dropDownYearTo.SelectedValue);
			p.Q.Add("SAID", sponsorAdminID);
			p.Q.Add("SID", sponsorID);
			p.Q.AddIf(Convert.ToInt32(Session["Anonymized"]) == 1, "Anonymized", 1);
			p.Q.Add("DIST", dropDownDistribution.SelectedValue);
			p.Q.Add("GB", dropDownGroupBy.SelectedValue);
			p.Q.Add("RPID", reportID);
			p.Q.Add("RPLID", reportPartLangID);
			p.Q.Add("PRUID", dropDownListProjectRoundUnits.SelectedValue);
			p.Q.Add("GRPNG", dropDownGrouping.SelectedValue);
			p.Q.AddIf(Request.QueryString["PLOT"] != null, "PLOT", Request.QueryString["PLOT"]);
			return p;
		}
		
		protected override void OnPreRender(EventArgs e)
		{
			tableDepartments.Visible = Org.Visible = (dropDownGrouping.SelectedValue == "1" || dropDownGrouping.SelectedValue == "2");
			checkBoxQuestions.Visible = (dropDownGrouping.SelectedValue == "3");
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

		void Execute_Click(object sender, EventArgs e)
		{
			int selectedLangID = Convert.ToInt32(dropDownLanguages.SelectedValue);
			int selectedProjectRoundUnitID = Convert.ToInt32(dropDownListProjectRoundUnits.SelectedValue);
			int grouping = Convert.ToInt32(dropDownGrouping.SelectedValue);
			
			reportParts = reportRepository.FindByProjectAndLanguage(selectedProjectRoundUnitID, selectedLangID);
			urlModels = GetUrlModels(grouping);
		}
	}
}