using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Models;
using HW.EForm.Core.Services;

namespace HW.EForm2
{
    public partial class Surveys : System.Web.UI.Page
    {
        protected IList<Survey> surveys;
        SurveyService s = new SurveyService();

        protected void Page_Load(object sender, EventArgs e)
        {
            surveys = s.FindAllSurveys();
        }
    }
}