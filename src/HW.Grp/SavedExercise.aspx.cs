using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.Grp
{
	public partial class SavedExercise : System.Web.UI.Page
	{
		SqlExerciseRepository exerciseRepo = new SqlExerciseRepository();
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		SqlUserRepository userRepository = new SqlUserRepository();
		SqlSponsorAdminRepository sponsorAdminRepo = new SqlSponsorAdminRepository();

		protected int exerciseAreaID;
		protected string sortQueryString;
		protected int exerciseCategoryID;
		protected IList<ExerciseCategoryLanguage> categories;
		protected IList<ExerciseAreaLanguage> areas;
		protected IList<SponsorAdminExercise> exercises;
		protected int sponsorID;
		protected int sponsorAdminID;
		protected int sort;
		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);

		public string AdditionalSortQuery
		{
			get { return (exerciseAreaID != 0 ? "&EAID=" + exerciseAreaID : "") + (exerciseCategoryID != 0 ? "&ECID=" + exerciseCategoryID : ""); }
		}

		public bool HasSelectedArea
		{
			get { return SelectedArea != null; }
		}

		public ExerciseAreaLanguage SelectedArea
		{
			get {
				foreach (var a in areas) {
					if (a.Area.Id == exerciseAreaID) {
						return a;
					}
				}
				return null;
			}
		}

		public bool HasSelectedCategory
		{
			get { return SelectedCategory != null; }
		}

		public ExerciseCategoryLanguage SelectedCategory
		{
			get {
				foreach (var c in categories) {
					if (c.Category.Id == exerciseCategoryID) {
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
		
		public void Delete(int sponsorAdminExerciseID)
		{
			if (sponsorAdminExerciseID > 0) {
				sponsorAdminRepo.DeleteSponsorAdminExercise(sponsorAdminExerciseID);
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SponsorID"] == null, "default.aspx", true);

			sponsorID = ConvertHelper.ToInt32(Session["SponsorID"]);
			sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"], -1);

			HtmlHelper.RedirectIf(!new SqlSponsorAdminRepository().SponsorAdminHasAccess(sponsorAdminID, ManagerFunction.Exercises), "default.aspx", true);

			SaveAdminSession(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Exercises, DateTime.Now);

			sortQueryString = "&SORT=" + sort;

			var userSession = userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
			if (userSession != null) {
				lid = userSession.Lang;
			}
			
			Delete(ConvertHelper.ToInt32(Request.QueryString["Delete"]));
			
			SetLanguage(userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent));
			
			Show(
//				exerciseRepo.FindBySponsorAdminExerciseHistory(lid - 1, sponsorAdminID),
				sponsorAdminRepo.FindBySponsorAdminExerciseHistory(lid - 1, sponsorAdminID),
				ConvertHelper.ToInt32(Request.QueryString["EAID"]),
				ConvertHelper.ToInt32(Request.QueryString["ECID"]),
				lid,
				ConvertHelper.ToInt32(Request.QueryString["SORT"])
			);
		}
		
		public void SetLanguage(UserSession userSession)
		{
			if (userSession != null) {
				this.lid = userSession.Lang;
			}
		}
		
		public void Show(IList<SponsorAdminExercise> exercises, int areaID, int categoryID, int lid, int sort)
		{
			this.exercises = exercises;

			this.exerciseAreaID = areaID;
			this.exerciseCategoryID = categoryID;
			this.lid = lid;
			this.sort = sort;

			if (!IsPostBack)
			{
				areas = exerciseRepo.FindAreas(areaID, lid - 1);
				categories = exerciseRepo.FindCategories(areaID, categoryID, lid - 1);
			}
		}

		//public void SetExercises(int areaID, int categoryID, int lid, int sort)
		//{
		//    this.exerciseAreaID = areaID;
		//    this.exerciseCategoryID = categoryID;
		//    this.lid = lid;
		//    this.sort = sort;

		//    if (!IsPostBack)
		//    {
		//        areas = exerciseRepository.FindAreas(areaID, lid - 1);
		//        categories = exerciseRepository.FindCategories(areaID, categoryID, lid - 1);
		//    }
		//    exercises = exerciseRepository.FindByAreaAndCategory(areaID, categoryID, lid - 1, sort);
		//}
	}
}