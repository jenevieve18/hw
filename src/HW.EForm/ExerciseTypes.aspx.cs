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
    public partial class ExerciseTypes : System.Web.UI.Page
    {
    	protected IList<ExerciseTypeLanguage> types;
    	IExerciseRepository r = AppContext.GetRepositoryFactory().CreateExerciseRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	types = r.FindTypeLanguages();
        }
    }
}