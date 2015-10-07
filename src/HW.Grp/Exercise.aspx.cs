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
		IExerciseRepository exerciseRepository;
		ISponsorRepository sponsorRepository;
		
//		protected int BX = 0;
		protected int EAID;
		protected string sortQS;
		protected int ECID;
		protected IList<ExerciseCategoryLanguage> categories;
		protected IList<ExerciseAreaLanguage> areas;
		protected IList<HW.Core.Models.Exercise> exercises;
		protected int sponsorID;
		protected int sponsorAdminID;
//		int AX = 0;
		protected int lid;
		protected int SORTX;
		
		public Exercise() : this(new SqlSponsorRepository(), new SqlExerciseRepository())
		{
		}
		
		public Exercise(ISponsorRepository sponsorRepository, IExerciseRepository exerciseRepository)
		{
			this.sponsorRepository = sponsorRepository;
			this.exerciseRepository = exerciseRepository;
		}
		
		public string AdditionalSortQuery {
			get { return (EAID != 0 ? "&EAID=" + EAID : "") + (ECID != 0 ? "&ECID=" + ECID : ""); }
		}
		
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
		
		public void SaveAdminSession(int SponsorAdminSessionID, int ManagerFunction, DateTime date)
		{
			sponsorRepository.SaveSponsorAdminSessionFunction(SponsorAdminSessionID, ManagerFunction, date);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SponsorID"] == null, "default.aspx", true);
			
			sponsorID = ConvertHelper.ToInt32(Session["SponsorID"]);
			sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"], -1);
			
			HtmlHelper.RedirectIf(!new SqlSponsorAdminRepository().SponsorAdminHasAccess(sponsorAdminID, ManagerFunction.Exercises), "default.aspx", true);

			SaveAdminSession(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Exercises, DateTime.Now);

			sortQS = "&SORT=" + SORTX;

			Index(
				ConvertHelper.ToInt32(Request.QueryString["EAID"]),
				ConvertHelper.ToInt32(Request.QueryString["ECID"]),
				ConvertHelper.ToInt32(Session["lid"], 2),
				ConvertHelper.ToInt32(Request.QueryString["SORT"])
			);
		}
		
		public void Index(int areaID, int categoryID, int lid, int sort)
		{
			this.EAID = areaID;
			this.ECID = categoryID;
			this.lid = lid;
			this.SORTX = sort;
			
			if (!IsPostBack) {
				areas = exerciseRepository.FindAreas(areaID, lid - 1);
				categories = exerciseRepository.FindCategories(areaID, categoryID, lid - 1);
			}
			exercises = exerciseRepository.FindByAreaAndCategory(areaID, categoryID, lid - 1, sort);
		}
	}
}