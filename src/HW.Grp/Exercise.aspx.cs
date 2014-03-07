using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class Exercise : System.Web.UI.Page
	{
		protected int BX = 0;
		protected int LID = 0;
		protected int EAID;
		protected string sortQS;
		protected int ECID;
		protected SqlExerciseRepository exerciseRepository = new SqlExerciseRepository();
		protected SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		protected IList<ExerciseCategoryLanguage> categories;
		protected IList<ExerciseAreaLanguage> areas;
		protected IList<HW.Core.Models.Exercise> exercises;
		protected int sponsorID;
		protected int sponsorAdminID;
		int AX = 0;
		
		public bool HasSelectedArea {
			get { return SelectedArea != null; }
		}
		
		public ExerciseAreaLanguage SelectedArea {
			get {
				foreach (var a in areas) {
					if (a.Area.Id == EAID) {
						return a;
					}
				}
				return null;
			}
		}
		
		public IList<ExerciseAreaLanguage> Areas {
			get { return areas; }
		}
		
		public bool HasSelectedCategory {
			get { return SelectedCategory != null; }
		}
		
		public ExerciseCategoryLanguage SelectedCategory {
			get {
				foreach (var c in categories) {
					if (c.Category.Id == ECID) {
						return c;
					}
				}
				return null;
			}
		}
		
		public IList<ExerciseCategoryLanguage> Categories {
			get { return categories; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Convert.ToInt32(Session["SponsorID"]) == 0) {
				Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
			
			sponsorRepository.SaveSponsorAdminSessionFunction(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Exercises, DateTime.Now);
			if (Request.QueryString["LID"] != null) {
				LID = Convert.ToInt32(Request.QueryString["LID"]);
			}
			EAID = ConvertHelper.ToInt32(Request.QueryString["EAID"]);
			ECID = ConvertHelper.ToInt32(Request.QueryString["ECID"]);
			int SORT = ConvertHelper.ToInt32(Request.QueryString["SORT"]);
			sortQS = "&SORT=" + SORT;

			StringBuilder sb = new StringBuilder();
			int exerciseAreaID = 0;
			int exerciseID = 0;

			if (!IsPostBack) {
				if (EAID == 0) {
					switch (LID) {
							case 0: AreaID.Controls.Add(new LiteralControl("<dt><a href='javascript:;'><span>Visa alla</span></a></dt><dd><ul>")); break;
							case 1: AreaID.Controls.Add(new LiteralControl("<dt><a href='javascript:;'><span>Show all</span></a></dt><dd><ul>")); break;
					}
				}
				string s = "";
				areas = exerciseRepository.FindAreas(EAID, LID);
				foreach (var a in areas) {
					if (EAID == a.Area.Id) {
						AreaID.Controls.Add(new LiteralControl("<dt><a href='javascript:;'><span>" + a.AreaName + "</span></a></dt><dd><ul>"));
						switch (LID) {
								case 0: AreaID.Controls.Add(new LiteralControl("<li id='EAID0'><a href='exercise.aspx?EAID=0" + sortQS + "#filter'>Visa alla</a></li>")); break;
								case 1: AreaID.Controls.Add(new LiteralControl("<li id='EAID0'><a href='exercise.aspx?EAID=0" + sortQS + "#filter'>Show all</a></li>")); break;
						}
					} else {
						if (s != "") {
							AreaID.Controls.Add(new LiteralControl("<li" + s));
						}
						s = " id='EAID" + a.Area.Id + "'><a href='exercise.aspx?EAID=" + a.Area.Id + "" + sortQS + "#filter'>" + a.AreaName + "</a></li>";
					}
				}
				AreaID.Controls.Add(new LiteralControl("<li class='last'" + s));
				AreaID.Controls.Add(new LiteralControl("</ul></dd>"));

				if (ECID == 0) {
					switch (LID) {
						case 0:
							CategoryID.Controls.Add(new LiteralControl("<dt><a href='javascript:;'><span>Visa alla</span></a></dt><dd><ul>"));
							break;
						case 1:
							CategoryID.Controls.Add(new LiteralControl("<dt><a href='javascript:;'><span>Show all</span></a></dt><dd><ul>"));
							break;
					}
				}
				s = "";
				categories = exerciseRepository.FindCategories(EAID, ECID, LID);
				foreach (var c in categories) {
					if (ECID == c.Category.Id) {
						CategoryID.Controls.Add(new LiteralControl("<dt><a href='javascript:;'><span>" + c.CategoryName + "</span></a></dt><dd><ul>"));
						switch (LID) {
								case 0: CategoryID.Controls.Add(new LiteralControl("<li id='ECID0'><a href='exercise.aspx?ECID=0" + sortQS + (EAID != 0 ? "&EAID=" + EAID : "") + "#filter'>Visa alla</a></li>")); break;
								case 1: CategoryID.Controls.Add(new LiteralControl("<li id='ECID0'><a href='exercise.aspx?ECID=0" + sortQS + (EAID != 0 ? "&EAID=" + EAID : "") + "#filter'>Show all</a></li>")); break;
						}
					} else {
						if (s != "") {
							CategoryID.Controls.Add(new LiteralControl("<li" + s));
						}
						s = " id='ECID" + c.Category.Id + "'><a href='exercise.aspx?ECID=" + c.Category.Id + "" + sortQS + (EAID != 0 ? "&EAID=" + EAID : "") + "#filter'>" + c.CategoryName + "</a></li>";
					}
				}
				CategoryID.Controls.Add(new LiteralControl("<li class='last'" + s));
				CategoryID.Controls.Add(new LiteralControl("</ul></dd>"));
			}

			exercises = exerciseRepository.FindByAreaAndCategory(EAID, ECID, LID, SORT);
			foreach (var l in exercises) { // TODO:
				if (l.Id != exerciseID) {
					BX++;
					if (AX > 0) {
						sb.Append("</div><div class='bottom'>&nbsp;</div></div><!-- end .detail --> </div><!-- end .item -->");
					}

					sb.Append("<div class='item'><div class='overview'></div><div class='detail'>");

					sb.Append("<div class='image'>" + (l.Image != "" ? "<img src='" + l.Image + "' width='121' height='100'>" : "") + "</div>");

					// time
					if (l.CurrentLanguage.Time != "") {
						sb.Append("<div class='time'>" + l.CurrentLanguage.Time + "<span class='time-end'></span></div>");
					}

					// exercise
					sb.Append("<div class='descriptions'>" + l.CurrentArea.AreaName + (l.CurrentCategory.CategoryName == "" ? "" : " - " + l.CurrentCategory.CategoryName) + "</div><h2>" + l.CurrentLanguage.ExerciseName + "</h2>");

					// teaser
					if (l.CurrentLanguage.Teaser != "") {
						sb.Append("<p>" + l.CurrentLanguage.Teaser + "</p>");
					}
					sb.Append("<div>");
				}

				string path = ConfigurationManager.AppSettings["healthWatchURL"];
				sb.Append("<a class='sidearrow' href=\"JavaScript:void(window.open('" + path + "exerciseShow.aspx?SID=" + Convert.ToInt32(Session["SponsorID"]) + "&AUID=" + Math.Abs(Convert.ToInt32(Session["SponsorAdminID"])) + "&ExerciseVariantLangID=" + l.CurrentVariant.Id + "','EVLID" + l.CurrentVariant.Id + "','scrollbars=yes,resizable=yes,");

				if (l.CurrentVariant.ExerciseWindowX != 0) {
					sb.Append("width=650,height=580");
				} else {
					sb.Append("width=" + l.CurrentVariant.ExerciseWindowX + ",height=" + l.CurrentVariant.ExerciseWindowY);
				}
				sb.Append("'));\">" + l.CurrentType.TypeName + (l.CurrentType.SubTypeName != null && l.CurrentType.SubTypeName != "" ? " (" + l.CurrentType.SubTypeName + ")" : "") + "</a>");

				exerciseAreaID = l.CurrentArea.Id;
				exerciseID = l.Id;
				AX++;
			}

			if (AX > 0) {
				sb.Append("</div><div class='bottom'>&nbsp;</div></div><!-- end .detail --> </div><!-- end .item -->");
			}

			if (!IsPostBack) {
				ExerciseList.Controls.Add(new LiteralControl(sb.ToString()));
				
				string q = (EAID != 0 ? "&EAID=" + EAID : "") + (ECID != 0 ? "&ECID=" + ECID : "");

				switch (LID) {
					case 0:
						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 0 ? " class='active' href='javascript:;'" : " href='exercise.aspx?SORT=0" + q + "#filter'") + "><span>Slumpmässigt</span></a>"));
						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 1 ? " class='active' href='javascript:;'" : " href='exercise.aspx?SORT=1" + q + "#filter'") + "><span>Popularitet</span></a>"));
						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 2 ? " class='active' href='javascript:;'" : " href='exercise.aspx?SORT=2" + q + "#filter'") + "><span>Bokstavsordning</span></a>"));
						break;
					case 1:
						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 0 ? " class='active' href='javascript:;'" : " href='exercise.aspx?SORT=0" + q + "#filter'") + "><span>Random</span></a>"));
						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 1 ? " class='active' href='javascript:;'" : " href='exercise.aspx?SORT=1" + q + "#filter'") + "><span>Popularity</span></a>"));
						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 2 ? " class='active' href='javascript:;'" : " href='exercise.aspx?SORT=2" + q + "#filter'") + "><span>Alphabethical</span></a>"));
						break;
				}
			}
		}
	}
}