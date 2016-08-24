using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
using HW.EForm.Core.Services;

namespace HW.EForm2
{
    public partial class SurveyEdit : System.Web.UI.Page
    {
        protected Survey survey;
        SurveyService s = new SurveyService();

        protected void Page_Load(object sender, EventArgs e)
        {
            survey = s.ReadSurvey(ConvertHelper.ToInt32(Request.QueryString["SurveyID"]));
        }
    }
}