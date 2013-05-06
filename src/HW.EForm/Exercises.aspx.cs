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
	public partial class Exercises : System.Web.UI.Page
	{
		IExerciseRepository r = AppContext.GetRepositoryFactory().CreateExerciseRepository();
		protected IList<Exercise> exercises;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			exercises = r.FindAll();
		}
	}
}