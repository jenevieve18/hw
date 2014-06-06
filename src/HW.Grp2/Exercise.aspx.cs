using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Grp2
{
	public partial class Exercise : System.Web.UI.Page
	{
		SqlExerciseRepository exerciseRepository = new SqlExerciseRepository();
		protected IList<ExerciseAreaLanguage> areas;
		protected IList<ExerciseCategoryLanguage> categories;
		protected IList<HW.Core.Models.Exercise> exercises;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			areas = exerciseRepository.FindAreas(0, 1);
			categories = exerciseRepository.FindCategories(0, 0, 1);
			exercises = exerciseRepository.FindByAreaAndCategory(1, 1, 1, 1);
		}
	}
}