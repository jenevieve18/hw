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
using System.Text;
using System.Net;
using System.IO;

namespace HW.Grp
{
	public partial class Stats : System.Web.UI.Page
	{

        Grp.WebService.Soap service = new WebService.Soap();

        SqlSponsorRepository sponsorRepo = new SqlSponsorRepository();
		SqlSponsorProjectRepository sponsorProjectRepo = new SqlSponsorProjectRepository();
		SqlSponsorBQRepository sponsorBQRepo = new SqlSponsorBQRepository();
		SqlSponsorProjectRoundUnitRepository sponsorProjectRoundUnitRepo = new SqlSponsorProjectRoundUnitRepository();
		
		SqlUserRepository userRepository = new SqlUserRepository();
		
		SqlProjectRepository projRepository = new SqlProjectRepository();
		
		SqlDepartmentRepository departmentRepository = new SqlDepartmentRepository();
		
		SqlReportRepository reportRepository = new SqlReportRepository();
		SqlPlotTypeRepository plotRepository = new SqlPlotTypeRepository();
		
		IList<Department> departments;
		IList<SponsorBackgroundQuestion> questions;
		int sponsorID = 0;
		int sponsorAdminID = 0;
		
		protected IList<IReportPart> reportParts = null;
		protected IList<BaseModel> urlModels;
		protected IList<PlotTypeLanguage> plotTypes = new List<PlotTypeLanguage>();
		
		protected DateTime startDate;
		protected DateTime endDate;
		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);
		
		protected Sponsor sponsor;

		public IList<SponsorProjectRoundUnit> SponsorProjectRoundUnits {
			set {
				ProjectRoundUnitID.Items.Clear();
				foreach (var p in value) {
					ProjectRoundUnitID.Items.Add(new ListItem(p.Navigation, "SPRU" + p.ProjectRoundUnit.Id.ToString()));
				}
				GroupBy.SelectedValue = "7";
			}
		}
		
		public IList<SponsorProject> SponsorProjects {
			set {
				foreach (var p in value) {
					ProjectRoundUnitID.Items.Add(new ListItem(p.Subject, "SP" + p.Id.ToString()));
				}
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

        protected int SelectedBackgroundQuestions
        {
            get
            {
                return BQ.Items.Cast<ListItem>().Where(li => li.Selected).ToList().Count();
            }
        }
		
		public int GetSponsorDefaultPlotType(int defaultPlotType, bool forSingleSeries, int grouping)
		{
			if (grouping == 3 && defaultPlotType > 3) {
				return 0;
			} else if (grouping <= 2 && !forSingleSeries) {
				return 0;
			} else {
				return defaultPlotType;
			}
		}
		
		public IList<Department> Departments {
			set {
				this.departments = value;
				Org.Controls.Add(new LiteralControl("<br>"));
				IHGHtmlTable table = new IHGHtmlTable { Border = 0, CellSpacing = 0, CellPadding = 0 };
				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(Session["Sponsor"].ToString()) { ColSpan = 3 }));
				Dictionary<int, bool> DX = new Dictionary<int, bool>();
				foreach (var d in departments) {
					IHGHtmlTableRow row = new IHGHtmlTableRow(new IHGHtmlTableCell(new CheckBox { ID = "DID" + d.Id }), new IHGHtmlTableCell(d.ShortName));
					
					int depth = d.Depth;
					DX[depth] = d.Siblings > 0;

					IList<Control> images = new List<Control>();
					for (int i = 1; i <= depth; i++) {
						if (!DX.ContainsKey(i)) {
							DX[i] = false;
						}
						images.Add(new HtmlImage { Src = string.Format("assets/theme1/img/{0}.gif", i == depth ? (DX[i] ? "T" : "L") : (DX[i] ? "I" : "null")), Width = 19, Height = 20 });
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
//				foreach (var q in sponsorRepo.FindBySponsor(sponsorID)) {
				foreach (var q in sponsorBQRepo.FindBySponsor(sponsorID)) {
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
		
		public void SetReportPartLanguages(IList<IReportPart> reportParts, IList<BaseModel> urlModels)
		{
			this.reportParts = reportParts;
			this.urlModels = urlModels;
			var selectedDepartments = SelectedDepartments;
		}

		protected string GetLang(int lid)
		{
            switch (lid)
            {
                case 1: return "sv";
                case 4: return "de";
                default: return "en";
            }
		}

		protected CultureInfo GetCultureInfo(int lid)
		{
            switch (lid)
            {
                case 1: return new CultureInfo("sv-SE");
                case 4: return new CultureInfo("de-DE");
                default: return new CultureInfo("en-US");
            }
		}
		
		public void SaveAdminSession(int SponsorAdminSessionID, int ManagerFunction, DateTime date)
		{
			sponsorRepo.SaveSponsorAdminSessionFunction(SponsorAdminSessionID, ManagerFunction, date);
		}
		
		public void Index(int sponsorID, int sponsorAdminID)
		{
			if (sponsorID != 0) {
				if (!IsPostBack) {
					
					startDate = ToDate(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
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
					Grouping.Items.Add(new ListItem(R.Str(lid, "users.none", "< none >"), "0"));
					Grouping.Items.Add(new ListItem(R.Str(lid, "users.unit", "Users on unit"), "1"));
					Grouping.Items.Add(new ListItem(R.Str(lid, "users.unit.subunit", "Users on unit+subunits"), "2"));
					Grouping.Items.Add(new ListItem(R.Str(lid, "background.variable", "Background variable"), "3"));

                    SponsorProjectRoundUnits = sponsorRepo.FindBySponsorAndLanguage(sponsorID, lid);

                    SponsorProjects = sponsorProjectRepo.FindSponsorProjects(sponsorID);

					BackgroundQuestions = sponsorBQRepo.FindBySponsor(sponsorID);
				} else {
					startDate = GetDateFromString(Request.Form["startDate"]);
					endDate = GetDateFromString(Request.Form["endDate"]);
				}
				Departments = departmentRepository.FindBySponsorWithSponsorAdminInDepth(sponsorID, sponsorAdminID);
			} else {
				Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
		}

		DateTime ToDate(int year, int month, int day)
		{
			DateTime d;
			try {
				d = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
			} catch {
				d = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day - 1);
			}
			return d;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
			sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);

            HtmlHelper.RedirectIf(!new SqlSponsorAdminRepository().SponsorAdminHasAccess(sponsorAdminID, ManagerFunction.Statistics), "default.aspx", true);

            sponsor = sponsorRepo.Read(sponsorID);

            var userSession = userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
            if (userSession != null)
            {
                lid = userSession.Lang;
            }


            plotTypes = plotRepository.FindByLanguage(lid);
			
			SaveAdminSession(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Statistics, DateTime.Now);
			
			Index(sponsorID, sponsorAdminID);

            if(reportParts == null)
            {
                StatisticImage.Text = "";
            }

			Execute.Click += new EventHandler(ExecuteClick);
			ProjectRoundUnitID.SelectedIndexChanged += new EventHandler(ProjectRoundUnitSelectedIndexChanged);
		}

		void ProjectRoundUnitSelectedIndexChanged(object sender, EventArgs e)
		{
            if (ProjectRoundUnitID.SelectedValue.Contains("SPRU")) {
            	int pruID = ConvertHelper.ToInt32(ProjectRoundUnitID.SelectedValue.Replace("SPRU", ""));
            	var sponsorProjectRoundUnit = sponsorProjectRoundUnitRepo.ReadByProjectRoundUnit(pruID);
            	if (sponsorProjectRoundUnit != null && sponsorProjectRoundUnit.DefaultAggregation != 0) {
            		GroupBy.SelectedValue = sponsorProjectRoundUnit.DefaultAggregation.ToString();
            	} else {
                    GroupBy.SelectedValue = "7";
                }
            }
		}
		
		DateTime GetDateFromString(string s)
		{
			DateTime d = DateTime.Now;
			try {
				d = DateTime.ParseExact(s, "yyyy MMM", GetCultureInfo(lid));
			} catch {}
			return d;
		}
		
		protected string GetReportImageUrlForSponsorProject(int reportPartLangID, Q additionalQuery)
		{
            var p = GetPage2("reportImage2.aspx", reportPartLangID);
			p.Add(additionalQuery);
			return p.ToString();
		}

		protected string GetReportImageUrlForReportPart(int reportID, int reportPartLangID, Q additionalQuery)
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
		
		protected string GetExportAllUrl2(string type, Q additionalQuery)
		{
			var p = GetPage("ExportAll2.aspx", 0, 0);
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

        P GetPage2(string page, int reportPartLangID)
		{
			P p = new P(page);
			p.Q.Add("LangID", lid);
			p.Q.Add("FY", startDate.Year);
			p.Q.Add("TY", endDate.Year);
			p.Q.Add("FM", startDate.Month);
			p.Q.Add("TM", endDate.Month);
			p.Q.Add("SAID", sponsorAdminID);
			p.Q.Add("SID", sponsorID);
			p.Q.AddIf(Convert.ToInt32(Session["Anonymized"]) == 1, "Anonymized", 1);
			p.Q.Add("GB", GroupBy.SelectedValue);
//			p.Q.Add("RPID", reportID);
			p.Q.Add("RPLID", reportPartLangID);
			p.Q.Add("PRUID", ProjectRoundUnitID.SelectedValue);
			p.Q.Add("GRPNG", Grouping.SelectedValue);
			return p;
		}
		
        P GetPage(string page, int reportID, int reportPartLangID)
		{
			P p = new P(page);
			p.Q.Add("LangID", lid);
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
			return p;
		}

		void ExecuteClick(object sender, EventArgs e)
		{
			int grouping = Convert.ToInt32(Grouping.SelectedValue);
			
			if (departments.Count > 0) {
				var reportParts = GetReportParts(ProjectRoundUnitID.SelectedValue);
				SetReportPartLanguages(reportParts, GetUrlModels(grouping));
			}

            /// <summary>
            /// Generate HTML File to display in Statistic image.
            /// </summary>
            String imageBuilder = "";
            
            if (reportParts != null && reportParts.Count > 0)
            {
                Q additionalQuery = GetGID(urlModels);
                bool forSingleSeries = (SelectedDepartments.Count <= 1 && (Grouping.SelectedValue == "1" || Grouping.SelectedValue == "2")) || Grouping.SelectedValue == "0";
                imageBuilder += "<div class=\"report-parts\">";
                if (reportParts[0] is ReportPartLang)
                {
                imageBuilder += "<div class=\"action\">";
                imageBuilder += "<div class=\"chart-descriptions\" title=" + R.Str(lid, "chart.description", "Chart Descriptions") + ">";
                imageBuilder += "<div>";
                    foreach (var p in plotTypes)
                    {
                        imageBuilder += "<div>&nbsp;<br /></div>";
                        imageBuilder += "<div class=\"report -part\">";
                        imageBuilder += "<div class=\"report -part-subject\">";
                        imageBuilder += "<span>" + p.ShortName + " - " + p.Name  + "</span>";
                        imageBuilder += "<span class=\"toggle toggle-right toggle-active\"></span></div>";
                        imageBuilder += "<div class=\"report-part-header\">" + p.Description + "</div></div>";
                    };
                    imageBuilder += "</div>";
                    imageBuilder += "</div>";
                    imageBuilder += "<span class=\"small\">" + R.Str(lid, "graphs.change.all", "Change all graphs to:") + "</span>";
                    imageBuilder += "<select class=\"plot-types small\">";
                    foreach (var p in plotTypes)
                    { 
                        if (!p.SupportsMultipleSeries && !forSingleSeries) { } 
                        else
                           {
                                imageBuilder += "<option value = " + p.PlotType.Id.ToString() + (p.PlotType.Id == sponsor.DefaultPlotType ? "selected" : "" )+">" + p.ShortName + "</ option >";
                           }
                    };
                    imageBuilder += "</select>";
                    imageBuilder += "<span class=\"chart-descriptions-info\"></span>";
                    imageBuilder += "<span class=\"small\">" + R.Str(lid, "graphs.export.all", "Export all graphs to:") + "</span>";
                    imageBuilder += "<span class=\"button white small export\">";
                    string exportAllDocXUrl = GetExportAllUrl("docx", additionalQuery);
                    imageBuilder += "<span class=\"hidden hidden-exportall-docx-url\">" + exportAllDocXUrl + "</span>";
                    imageBuilder += HtmlHelper.Anchor("docx", exportAllDocXUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='exportall-docx-url' target='_blank'");
                    imageBuilder += "</span>";
                    imageBuilder += "<span class=\"button white small export\">";
                    string exportAllPptxUrl = GetExportAllUrl("pptx", additionalQuery);
                    imageBuilder += "<span class=\"hidden hidden-exportall-pptx-url\">" + exportAllPptxUrl + "</span>";
                    imageBuilder += HtmlHelper.Anchor("pptx", exportAllPptxUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='exportall-pptx-url' target='_blank'");
                    imageBuilder += "</span>";
                    string exportAllXlsUrl = GetExportAllUrl("xls", additionalQuery);
                    imageBuilder += "<span class=\"button white small export\">";
                    imageBuilder += "<span class=\"hidden hidden-exportall-xls-url\">" + exportAllXlsUrl + "</span>";
                    imageBuilder += HtmlHelper.Anchor("xls", exportAllXlsUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='exportall-xls-url' target='_blank'");
                    imageBuilder += "</span>";
                    imageBuilder += "<span class=\"button white small export\">";
                    imageBuilder += HtmlHelper.Anchor(R.Str(lid, "xls.verbose", "xls verbose"), exportAllXlsUrl + "&PLOT=" + PlotType.Verbose, "class='exportall-xls-verbose-url' target='_blank'");
                    imageBuilder += "</span>";
                    imageBuilder += "</div>";
                }
                else
                {
                    imageBuilder += "<div class=\"action\">";
                    imageBuilder += "<div class=\"chart-descriptions\" title=" + R.Str(lid, "chart.description", "Chart Descriptions") + ">";
                    imageBuilder += "<div>";
                        foreach (var p in plotTypes)
                        {
                            imageBuilder += "<div>&nbsp;<br />";
                            imageBuilder += "</div>";
                            imageBuilder += "<div class=\"report-part\">";
                            imageBuilder += "<div class=\"report-part-subject\">";
                            imageBuilder += "<span>" + p.ShortName + " - " + p.Name + "</span>";
                            imageBuilder += "<span class=\"toggle toggle-right toggle-active\"></span>";
                            imageBuilder += "</div>";
                            imageBuilder += "<div class=\"report-part-header\">" + p.Description + "</div>";
                            imageBuilder += "</div>";
                        };
                    imageBuilder += "</div>";
                    imageBuilder += "</div>";
                    imageBuilder += "<span class=\"small\">" + R.Str(lid, "graphs.change.all", "Change all graphs to:") + "</span>";
                    imageBuilder += "<select class=\"plot-types small\">";
                    var xxx = lid == 1 ? new PlotTypeLanguage { PlotType = new PlotType { Id = 1 }, ShortName = "Linje", SupportsMultipleSeries = true } :
                                   new PlotTypeLanguage { PlotType = new PlotType { Id = 1 }, ShortName = "Line", SupportsMultipleSeries = true };
                    foreach (var p in new PlotTypeLanguage[] { xxx })
                       { 
                    if (!p.SupportsMultipleSeries && !forSingleSeries) { }
                    else
                       {
                            imageBuilder += "<option value = " + p.PlotType.Id.ToString() + (p.PlotType.Id == sponsor.DefaultPlotType ? "selected" : "") + ">" + p.ShortName + "</option>";
                       } 
                    };
                    imageBuilder += "</select>";
                    imageBuilder += "<span class=\"chart-descriptions-info\"></span>";
                    imageBuilder += "<span class=\"small\">" + R.Str(lid, "graphs.export.all", "Export all graphs to:") + "</span>";
                    imageBuilder += "<span class=\"button white small export\">";
                    string exportAllDocXUrl = GetExportAllUrl2("docx", additionalQuery);
                    imageBuilder += "<span class=\"hidden hidden-exportall-docx-url\">" + exportAllDocXUrl + "</span>";
                    imageBuilder += HtmlHelper.Anchor("docx", exportAllDocXUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='exportall-docx-url' target='_blank'");
                    imageBuilder += "</span>";
                    imageBuilder += "<span class=\"button white small export\">";
                    string exportAllPptxUrl = GetExportAllUrl2("pptx", additionalQuery);
                    imageBuilder += "<span class=\"hidden hidden-exportall-pptx-url\">" + exportAllPptxUrl + "</span>";
                    imageBuilder += HtmlHelper.Anchor("pptx", exportAllPptxUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='exportall-pptx-url' target='_blank'");
                    imageBuilder += "</span>";
                    string exportAllXlsUrl = GetExportAllUrl2("xls", additionalQuery);
                    imageBuilder += "<span class=\"button white small export\">";
                    imageBuilder += "<span class=\"hidden hidden-exportall-xls-url\">" + exportAllXlsUrl + "</span>";
                    imageBuilder += HtmlHelper.Anchor("xls", exportAllXlsUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='exportall-xls-url' target='_blank'");
                    imageBuilder += "</span>";
                    imageBuilder += "<span class=\"button white small export\">";
                    imageBuilder += HtmlHelper.Anchor(R.Str(lid, "xls.verbose", "xls verbose"), exportAllXlsUrl + "&PLOT=" + PlotType.Verbose, "class='exportall-xls-verbose-url' target='_blank'");
                    imageBuilder += "</span>";
                    imageBuilder += "</div>";
                }

                var selectedGID = "0";
                foreach (var gid in urlModels)
                {
                    selectedGID += "," + gid.Id.ToString();
                }
                /// <summary>
                /// Initializing GRP-WS and call GetReportImageUrl webmethod.
                /// </summary>
                /// <returns>List of Statistic Image URLs</returns>
                var soapService = new Grp.WebService.Soap();
                var soapResponse = soapService.GetReportImageUrl(
                    Session["Token"].ToString(),
                    startDate,
                    endDate,
                    lid,
                    sponsorAdminID,
                    sponsorID,
                    Convert.ToInt32(GroupBy.SelectedValue),
                    ProjectRoundUnitID.SelectedValue,
                    Convert.ToInt32(Grouping.SelectedValue),
                    Convert.ToInt32(Session["Anonymize"]),
                    selectedGID,
                    20);

                var imagePath = Server.MapPath("~\\img\\Sponsor" + sponsorID);
                
                Directory.CreateDirectory(imagePath);

                /// <summary>
                /// Display image to Statistic page.
                /// </summary>
                foreach (var r in reportParts)
                {
                    imageBuilder += "<div> &nbsp;<br/>";
                    imageBuilder += "</div>";
                    imageBuilder += "<div class=\"report-part\">";
                    imageBuilder += "<div class=\"hidden selected-plot-type\">" + PlotType.Line + "</div>";
                    imageBuilder += "<div class=\"report-part-subject\">";
                    imageBuilder += "<span>" + r.Subject + "</span>";
                    imageBuilder += "<span class=\"toggle toggle-right toggle-active\"></span>";
                    imageBuilder += "</div>";
                    imageBuilder += "<div class=\"report-part-header\">" + r.Header  + "</div>";
                    imageBuilder += "<div class=\"report-part-content\">";
                    if (r is ReportPartLang)
                    {
                        /// <summary>
                        /// Generate image filepath + filename
                        /// </summary>
                        var saveImage = imagePath + "\\" + Session["Token"].ToString() + r.ReportPart.Id.ToString() + ".png";
                        var imageSource = "img/Sponsor" + sponsorID + "/" + Session["Token"].ToString() + r.ReportPart.Id.ToString() + ".png";
                        var imageUrl = soapResponse.Where(url => url.Id == r.ReportPart.Id).Select(url => url.Url).ToList()[0] + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue));

                        /// <summary>
                        /// Download and save the image file to GRP
                        /// </summary>
                        WebClient webClient = new WebClient();
                        webClient.DownloadFile(imageUrl, saveImage);
                        webClient.Dispose();
                        
                        imageBuilder += "<span class=\"hidden hidden-image-url\">" + imageUrl + "</span>";
                        imageBuilder += "<img class=\"report-part-graph\" src=" + imageSource + " alt=\"\"/>";
                        imageBuilder += "<div class=\"action\">";
                        imageBuilder += "<span class=\"small\">" + R.Str(lid, "graphs.change", "Change this graph to:") + "</span>";
                        imageBuilder += "<select class=\"plot-types small\">";
                            foreach (var p in plotTypes)
                               { 
                            if (!p.SupportsMultipleSeries && !forSingleSeries) { } 
                            else
                                {
                                imageBuilder += "<option value = " + p.PlotType.Id.ToString() +  (p.PlotType.Id == sponsor.DefaultPlotType ? "selected" : "") + ">" + p.ShortName + "</ option >";
                                } 
                            };
                        imageBuilder += "</select>";
                        imageBuilder += "<span class=\"small\">" + R.Str(lid, "graphs.export", "Export this graph to:") + "</span>";
                        imageBuilder += "<span class=\"button white small export\">";
                        string exportDocXUrl = GetExportUrl(r.ReportPart.Id, r.Id, "docx", additionalQuery);
                        imageBuilder += "<span class=\"hidden hidden-export-docx-url\">" + exportDocXUrl + "</span>";
                        imageBuilder += HtmlHelper.Anchor("docx", exportDocXUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='export-docx-url' target='_blank'");
                        imageBuilder += "</span>";
                        imageBuilder += "<span class=\"button white small export\">";
                        string exportPptXUrl = GetExportUrl(r.ReportPart.Id, r.Id, "pptx", additionalQuery);
                        imageBuilder += "<span class=\"hidden hidden-export-pptx-url\">"+ exportPptXUrl + "</span>";
                        imageBuilder += HtmlHelper.Anchor("pptx", exportPptXUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='export-pptx-url' target='_blank'");
                        imageBuilder += "</span>";
                        string exportXlsUrl = GetExportUrl(r.ReportPart.Id, r.Id, "xls", additionalQuery);
                        imageBuilder += "<span class=\"button white small export\">";
                        imageBuilder += "<span class=\"hidden hidden-export-xls-url\">" + exportXlsUrl + "</span>";
                        imageBuilder += HtmlHelper.Anchor("xls", exportXlsUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='export-xls-url' target='_blank'");
                        imageBuilder += "</span>";
                        imageBuilder += "<span class=\"button white small export\">";
                        imageBuilder += HtmlHelper.Anchor(R.Str(lid, "xls.verbose", "xls verbose"), exportXlsUrl + "&PLOT=" + PlotType.Verbose, "class='export-xls-verbose-url' target='_blank'");
                        imageBuilder += "</span>";
                        imageBuilder += "</div>";
                    }
                    else
                    {
                        imageBuilder += "<span class=\"hidden hidden-image-url\">" + soapResponse.Where(url => url.Id == r.ReportPart.Id).Select(url => url.Url).ToList()[0] + "</span>";
                        imageBuilder += "<img class=\"report-part-graph\" src=" + soapResponse.Where(url => url.Id == r.ReportPart.Id).Select(url => url.Url).ToList()[0] + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)) + " alt=\"\" />";
                        imageBuilder += "<div class=\"action\">";
                        imageBuilder += "<span class=\"small\">" + R.Str(lid, "graphs.change", "Change this graph to:") + "</span>";
                        imageBuilder += "<select class=\"plot-types small\">";
                            var xxx = lid == 1 ? new PlotTypeLanguage { PlotType = new PlotType { Id = 1 }, ShortName = "Linje", SupportsMultipleSeries = true } :
                                               new PlotTypeLanguage { PlotType = new PlotType { Id = 1 }, ShortName = "Line", SupportsMultipleSeries = true }; 
                            foreach (var p in new PlotTypeLanguage[] { xxx })
                               { 
                            if (!p.SupportsMultipleSeries && !forSingleSeries) { }
                            else
                               {
                                imageBuilder += "<option value = " + p.PlotType.Id.ToString() + (p.PlotType.Id == sponsor.DefaultPlotType ? "selected" : "" ) + ">" + p.ShortName + "</option>";
                               }
                            };
                        imageBuilder += "</select>";
                        imageBuilder += "<span class=\"small\">" + R.Str(lid, "graphs.export", "Export this graph to:") + "</span>";
                        imageBuilder += "<span class=\"button white small export\">";
                        string exportAllDocXUrl = GetExportAllUrl2("docx", additionalQuery);
                        imageBuilder += "<span class=\"hidden hidden-export-docx-url\">" + exportAllDocXUrl + "</span>";
                        imageBuilder += HtmlHelper.Anchor("docx", exportAllDocXUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='export-docx-url' target='_blank'");
                        imageBuilder += "</span>";
                        imageBuilder += "<span class=\"button white small export\">";
                        string exportAllPptxUrl = GetExportAllUrl2("pptx", additionalQuery);
                        imageBuilder += "<span class=\"hidden hidden-export-pptx-url\">" + exportAllPptxUrl + "</span>";
                        imageBuilder += HtmlHelper.Anchor("pptx", exportAllPptxUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='export-pptx-url' target='_blank'");
                        imageBuilder += "</span>";
                        string exportAllXlsUrl = GetExportAllUrl2("xls", additionalQuery);
                        imageBuilder += "<span class=\"button white small export\">";
                        imageBuilder += "<span class=\"hidden hidden-export-xls-url\">" + exportAllXlsUrl + "</span>";
                        imageBuilder += HtmlHelper.Anchor("xls", exportAllXlsUrl + "&Plot=" + GetSponsorDefaultPlotType(sponsor.DefaultPlotType, forSingleSeries, ConvertHelper.ToInt32(Grouping.SelectedValue)), "class='export-xls-url' target='_blank'");
                        imageBuilder += "</span>";
                        imageBuilder += "<span class=\"button white small export\">";
                        imageBuilder += HtmlHelper.Anchor(R.Str(lid, "xls.verbose", "xls verbose"), exportAllXlsUrl + "&PLOT=" + PlotType.Verbose, "class='exportall-xls-verbose-url' target='_blank'");
                        imageBuilder += "</span>";
                        imageBuilder += "</div>";
                    }
                    imageBuilder += "</div>";
                    imageBuilder += "<div class=\"report -part-bottom\">&nbsp;</div>";
                    imageBuilder += "</div>";
                }
            }
            imageBuilder += "</div>";
            StatisticImage.Text = imageBuilder;
        }

        IList<IReportPart> GetReportParts(string project)
		{
			var parts = new List<IReportPart>();
			if (project.Contains("SPRU")) {
				int selectedProjectRoundUnitID = ConvertHelper.ToInt32(project.Replace("SPRU", ""));
				int selectedDepartmentID = departments[0].Id;
				var reportParts = reportRepository.FindByProjectAndLanguage2(selectedProjectRoundUnitID, lid, selectedDepartmentID);
				if (reportParts.Count <= 0) {
					reportParts = reportRepository.FindByProjectAndLanguage(selectedProjectRoundUnitID, lid);
				}
				parts.AddRange(reportParts);
			} else {
				int sponsorProjectID = ConvertHelper.ToInt32(project.Replace("SP", ""));
				var sponsorProject = sponsorProjectRepo.Read(sponsorProjectID);
				parts.Add(sponsorProject);
			}
			return parts;
		}
	}
}