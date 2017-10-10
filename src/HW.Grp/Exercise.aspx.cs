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
using System.Net;

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

        //protected HW.Grp.WebService2.ExerciseArea[] areass;
        //protected HW.Grp.WebService2.ExerciseCategoryLanguage[] categoriess;
        //protected HW.Grp.WebService2.Exercise[] exercisess;

        protected AreasData[] areaData;
        protected CategoryData[] categoryData;
        protected ExerciseData[] exerciseData;

        public string AdditionalSortQuery {
			get { return (exerciseAreaID != 0 ? "&EAID=" + exerciseAreaID : "") + (exerciseCategoryID != 0 ? "&ECID=" + exerciseCategoryID : ""); }
		}

        public bool HasSelectedArea
        {
            get { return SelectedArea != null; }
        }

        
        public AreasData SelectedArea
        {
            get
            {
                foreach (var a in areaData)
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


        public CategoryData SelectedCategory
        {
            get
            {
                foreach (var c in categoryData)
                {
                    if (c.Id == exerciseCategoryID)
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

            var service = new WebService.Soap();
            var service2 = new WebService2.Soap();
            var token = Session["SecondToken"] != null ? Session["SecondToken"] : Session["Token"];
            int check = 0;
            try
            {
                var client = new WebClient();
                var getService2URL = service2.Url;
                var download = client.DownloadData(getService2URL);
            }
            catch (Exception esd)
            {
                check = 1;
            }

            dynamic areass = null;
            dynamic categoriess = null;
            dynamic exercisess = null;
            if (!IsPostBack)
            {
                if (check == 0)
                {
                    areass = service2.GetExerciseAreas(token.ToString(), areaID, lid, 20);
                    categoriess = service2.GetExerciseCategories(token.ToString(), areaID, categoryID, lid, 20);
                    exercisess = service2.GetExercises(token.ToString(), areaID, categoryID, lid, sort, 20);
                }
                else
                {
                    areass = service.GetExerciseAreas(token.ToString(), areaID, lid, 20);
                    categoriess = service.GetExerciseCategories(token.ToString(), areaID, categoryID, lid, 20);
                    exercisess = service.GetExercises(token.ToString(), areaID, categoryID, lid, sort, 20);
                }

                if (areass != null)
                {
                    areaData = new AreasData[areass.Length];
                    for (int x = 0; x <= areass.Length - 1; x++)
                    {
                        var area = new AreasData
                        {
                            Id = areass[x].Id,
                            AreaName = areass[x].AreaName
                        };

                        areaData[x] = area;
                    }
                }
                
                if (categoriess != null)
                {
                    categoryData = new CategoryData[categoriess.Length];
                    for (int x = 0; x <= categoriess.Length - 1; x++)
                    {
                        var category = new CategoryData();
                        category.Id = categoriess[x].Category.Id;
                        category.CategoryName = categoriess[x].CategoryName;

                        categoryData[x] = category;
                    }
                }
            }
            if (exercisess != null)
            {
                exerciseData = new ExerciseData[exercisess.Length];
                for (int x = 0; x <= exercisess.Length - 1; x++)
                {
                    var exercise = new ExerciseData();
                    exercise.AreaCategoryName = exercisess[x].AreaCategoryName;
                    exercise.ExerciseVariantId = exercisess[x].ExerciseVariantId;
                    exercise.Id = exercisess[x].Id;
                    exercise.Image = exercisess[x].Image;
                    exercise.Name = exercisess[x].Name;
                    exercise.Teaser = exercisess[x].Teaser;
                    exercise.Time = exercisess[x].Time;

                    exerciseData[x] = exercise;
                }
            }

        }

        public class AreasData
        {
            public int Id { get; set; }
            public string AreaName { get; set; }
        }

        public class CategoryData
        {
            public int Id { get; set; }
            public string CategoryName { get; set; }
        }

        public class ExerciseData
        {
           public string AreaCategoryName { get; set; }
           public int ExerciseVariantId { get; set; }
           public int Id { get; set; }
           public string Image { get; set; }
           public string Name { get; set; }
           public string Teaser { get; set; }
           public string Time { get; set; }
        }     
	}
}