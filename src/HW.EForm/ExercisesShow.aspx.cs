using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HW.EForm
{
    public partial class ExercisesShow : System.Web.UI.Page
    {
    	protected Exercise exercise;
    	IExerciseRepository r = AppContext.GetRepositoryFactory().CreateExerciseRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	exercise = r.Read(ConvertHelper.ToInt32(Request["ExerciseID"]));
        }
    }
}