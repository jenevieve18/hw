using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HW.Grp
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
		
		protected override void OnPreRender(EventArgs e)
		{
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
		
		P GetPage(string page, int reportID, int reportPartLangID)
		{
			P p = new P(page);
			p.Q.Add("LangID", LangID.SelectedValue);
			p.Q.Add("FY", FromYear.SelectedValue);
			p.Q.Add("TY", ToYear.SelectedValue);
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
			int selectedLangID = Convert.ToInt32(LangID.SelectedValue);
			int selectedProjectRoundUnitID = Convert.ToInt32(ProjectRoundUnitID.SelectedValue);
			int grouping = Convert.ToInt32(Grouping.SelectedValue);
			
			var reportParts = reportRepository.FindByProjectAndLanguage(selectedProjectRoundUnitID, selectedLangID);
			SetReportPartLanguages(reportParts, GetUrlModels(grouping));
		}
	}
}