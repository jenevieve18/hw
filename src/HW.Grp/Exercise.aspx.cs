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
        //SqlExerciseRepository exerciseRepository = new SqlExerciseRepository();
        //SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
        //SqlUserRepository userRepository = new SqlUserRepository();

        protected int exerciseAreaID;
		protected string sortQueryString;
		protected int exerciseCategoryID;
		//protected IList<ExerciseCategoryLanguage> categories;
		//protected IList<ExerciseAreaLanguage> areas;
		//protected IList<HW.Core.Models.Exercise> exercises;
		protected int sponsorID;
		protected int sponsorAdminID;
		protected int sort;
		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);

        protected HW.Grp.WebService2.ExerciseArea[] areass;
        protected HW.Grp.WebService2.ExerciseCategoryLanguage[] categoriess;
        protected HW.Grp.WebService2.Exercise[] exercisess;

        public string AdditionalSortQuery {
			get { return (exerciseAreaID != 0 ? "&EAID=" + exerciseAreaID : "") + (exerciseCategoryID != 0 ? "&ECID=" + exerciseCategoryID : ""); }
		}

        public bool HasSelectedArea
        {
            get { return SelectedArea != null; }
        }

        //public ExerciseAreaLanguage SelectedArea
        //{
        //    get
        //    {
        //        foreach (var a in areas)
        //        {
        //            if (a.Area.Id == exerciseAreaID)
        //            {
        //                return a;
        //            }
        //        }
        //        return null;
        //    }
        //}

        public HW.Grp.WebService2.ExerciseArea SelectedArea
        {
            get
            {
                foreach (var a in areass)
                {
                    if (a.Id == exerciseAreaID)
                    {
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

        //public ExerciseCategoryLanguage SelectedCategory
        //{
        //    get
        //    {
        //        foreach (var c in categories)
        //        {
        //            if (c.Category.Id == exerciseCategoryID)
        //            {
        //                return c;
        //            }
        //        }
        //        return null;
        //    }
        //}

        public HW.Grp.WebService2.ExerciseCategoryLanguage SelectedCategory
        {
            get
            {
                foreach (var c in categoriess)
                {
                    if (c.Category.Id == exerciseCategoryID)
                    {
                        return c;
                    }
                }
                return null;
            }
        }

        //public void SaveAdminSession(int SponsorAdminSessionID, int ManagerFunction, DateTime date)
        //{
        //    sponsorRepository.SaveSponsorAdminSessionFunction(SponsorAdminSessionID, ManagerFunction, date);
        //}

        protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SponsorID"] == null, "default.aspx", true);
			
			sponsorID = ConvertHelper.ToInt32(Session["SponsorID"]);
			sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"], -1);
			
			//HtmlHelper.RedirectIf(!new SqlSponsorAdminRepository().SponsorAdminHasAccess(sponsorAdminID, ManagerFunction.Exercises), "default.aspx", true);

			//SaveAdminSession(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Exercises, DateTime.Now);

			sortQueryString = "&SORT=" + sort;
            
			
			//var userSession = userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
			//if (userSession != null) {
			//	lid = userSession.Lang;
			//}

			SetExercises(
				ConvertHelper.ToInt32(Request.QueryString["EAID"]),
				ConvertHelper.ToInt32(Request.QueryString["ECID"]),
				lid,
				ConvertHelper.ToInt32(Request.QueryString["SORT"])
			);
		}
		
		public void SetExercises(int areaID, int categoryID, int lid, int sort)
		{
			this.exerciseAreaID = areaID;
			this.exerciseCategoryID = categoryID;
			this.lid = lid;
			this.sort = sort;
            var service = new Grp.WebService2.Soap();

            //if (!IsPostBack)
            //{
            //    areas = exerciseRepository.FindAreas(areaID, lid - 1);
            //    categories = exerciseRepository.FindCategories(areaID, categoryID, lid - 1);
            //}
            //exercises = exerciseRepository.FindByAreaAndCategory(areaID, categoryID, lid - 1, sort);
            var token = Session["SecondToken"] != null ? Session["SecondToken"] : Session["Token"];
            if (!IsPostBack) 
            {
                
                areass = service.GetExerciseAreas(token.ToString(), areaID, lid, 20);
                categoriess = service.GetExerciseCategories(token.ToString(), areaID, categoryID, lid, 20);
            }
            exercisess = service.GetExercises(token.ToString(), areaID, categoryID, lid, sort, 20);
        }
	}
}