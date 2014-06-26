using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class ExercisesList : System.Web.UI.Page
    {
        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.ExerciseArea[] exerciseArea;
        protected HWService.ExerciseInfo[] exercises;

        protected string token = "";
        protected int lang = 2;
        protected int sort = 0;
        protected int areaid = 0;
        protected string areaname = "";
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Default.aspx");
            token = Session["token"].ToString();

            HtmlHelper.RedirectIf(Request.QueryString["exaid"] == null || Request.QueryString["sort"]==null, "Default.aspx");

            int lang = int.Parse(Session["languageId"].ToString());

            areaid = int.Parse(Request.QueryString["exaid"]);
            sort = int.Parse(Request.QueryString["sort"]);

            exerciseArea = service.ExerciseAreaEnum(new HWService.ExerciseAreaEnumRequest(token, 0, lang, 10)).ExerciseAreaEnumResult;
            exercises = service.ExerciseEnum(new HWService.ExerciseEnumRequest(token, areaid, 0, lang, 10)).ExerciseEnumResult;

            if (areaid == 0) areaname = "Exercises";
            else
            areaname = exerciseArea[areaid - 1].exerciseArea;

            if (sort == 1)
            {
                exercises = exercises.OrderByDescending(item => int.Parse(item.popularity.ToString())).ToArray<HWService.ExerciseInfo>();
            }
            if (sort == 2)
            {
                exercises = exercises.OrderBy(item => item.exercise.ToString()).ToArray<HWService.ExerciseInfo>();
            }

            
        }

        
    }
}