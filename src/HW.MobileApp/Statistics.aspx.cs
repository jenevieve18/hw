using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using System.Text.RegularExpressions;

namespace HW.MobileApp
{
    public partial class Statistics : System.Web.UI.Page
    {
        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.FormInstance formInstance;
        protected HWService.FormInstanceFeedback[] fifeedback;
        protected HWService.ExerciseInfo[] exercises;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Session["token"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            int language = int.Parse(Session["languageId"].ToString());
            string token = Session["token"].ToString();
            string formKey;

            if (Session["formKey"] == null){
                formKey = service.FormEnum(new HW.MobileApp.HWService.FormEnumRequest(token, language, 10)).FormEnumResult[0].formKey;
            }
            else formKey = Session["formKey"].ToString();

            string formInstanceKey;
            if (Session["formInstanceKey"] == null) formInstanceKey = "";
            else formInstanceKey = Session["formInstanceKey"].ToString();

            exercises = service.ExerciseEnum(new HWService.ExerciseEnumRequest(token,0,0,language,10)).ExerciseEnumResult;            
            
            formInstance = service.UserGetFormInstanceFeedback(token,formKey,formInstanceKey,language,10);
            fifeedback = formInstance.fiv;

        }

        protected string replaceExerciseTags(string actionPlan)
        {
            if (actionPlan == null || actionPlan == "") return null;
            
            string newString = "";
            for (int c = 0; c < actionPlan.Length ; c++)
            {

                if (c + 5 < actionPlan.Length - 5 & actionPlan.Substring(c, 5) == "<EXID")
                    {
                        int id = int.Parse(actionPlan.Substring(c + 5, 3));
                        newString += "<a href='ExercisesItem.aspx?varid=" + getExerciseVariant(id) + "' class='statlink'>" + getExerciseName(id) + "</a>";
                        c = c + 8;
                    }
                    else newString += actionPlan[c];
                
                
            }
          //  newString += actionPlan.Substring(actionPlan.Length - 5, 5);
            return newString;
        }

        protected string getExerciseName(int exid)
        { 
            foreach(HWService.ExerciseInfo exe in exercises)
            {
                if (exid == exe.exerciseID)
                    return exe.exercise;
            }

            return null;
        }

        protected string getExerciseVariant(int exid)
        {
            foreach (HWService.ExerciseInfo exe in exercises)
            {
                if (exid == exe.exerciseID)
                    return exe.exerciseVariant[0].exerciseVariantLangID+"" ;
            }

            return null;
        }

    }


}