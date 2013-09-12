using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HW.EForm
{
    public partial class ExerciseCategoriesEdit : System.Web.UI.Page
    {
    	IExerciseRepository r = AppContext.GetRepositoryFactory().CreateExerciseRepository();
    	protected ExerciseCategoryLanguage category;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request["ExerciseCategoryLangID"]);
            category = r.ReadCategoryLanguage(id);
        }
    }
}