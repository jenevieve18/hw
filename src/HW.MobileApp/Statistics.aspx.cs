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
        protected string compare;
        protected string timeframe;
        protected string viewlink;
        protected string rp0_selected;
        protected string rp1_selected;
        protected string rp2_selected;
        protected string rp3_selected;
        protected string chartlink;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");

            int language = int.Parse(Session["languageId"].ToString());
            string token = Session["token"].ToString();
            string formKey = "";

            foreach(var n in service.FormEnum(new HW.MobileApp.HWService.FormEnumRequest(token, language, 10)).FormEnumResult)
            {
                formKey = n.formKey;
                break;
            }  


            if(Request.QueryString["tf"] != null&&Request.QueryString["comp"] != null)
            {
                timeframe = Request.QueryString["tf"];
                compare = Request.QueryString["comp"];
                var fft = service.FormFeedbackTemplateEnum(new HWService.FormFeedbackTemplateEnumRequest(token,formKey,language,10)).FormFeedbackTemplateEnumResult;
                viewlink = "<a href='View.aspx?tf=" + timeframe + "&comp=" + compare + "' data-icon='check' rel='external'>View...</a>";
                rp0_selected = "";
                rp1_selected = "";
                rp2_selected = "";
                rp3_selected = "";
                if (!IsPostBack)
                {
                    if (Request.QueryString["rp0"] != null)
                    { rp0_selected = Request.QueryString["rp0"]; }
                    else { rp0_selected = "26"; }

                    if (Request.QueryString["rp1"] != null)
                    { rp1_selected = Request.QueryString["rp1"]; }
                    else { rp1_selected = "27"; }

                    if (Request.QueryString["rp2"] != null)
                    { rp2_selected = Request.QueryString["rp2"]; }
                    else rp2_selected = "0";

                    if (Request.QueryString["rp3"] != null)
                    { rp3_selected = Request.QueryString["rp3"]; }
                    else rp3_selected = "0";

                }

                rp0.Items.Add(new ListItem("None", "0"));
                rp1.Items.Add(new ListItem("None", "0"));
                rp2.Items.Add(new ListItem("None", "0"));
                rp3.Items.Add(new ListItem("None", "0"));

                foreach (var i in fft)
                {
                    if (i.feedbackTemplateID.ToString() != rp1_selected && i.feedbackTemplateID.ToString() != rp2_selected && i.feedbackTemplateID.ToString() != rp3_selected)
                        rp0.Items.Add(new ListItem(i.header,i.feedbackTemplateID.ToString()));
                    if (i.feedbackTemplateID.ToString() != rp0_selected && i.feedbackTemplateID.ToString() != rp2_selected && i.feedbackTemplateID.ToString() != rp3_selected)
                        rp1.Items.Add(new ListItem(i.header, i.feedbackTemplateID.ToString()));
                    if (i.feedbackTemplateID.ToString() != rp1_selected && i.feedbackTemplateID.ToString() != rp0_selected && i.feedbackTemplateID.ToString() != rp3_selected)
                        rp2.Items.Add(new ListItem(i.header, i.feedbackTemplateID.ToString()));
                    if (i.feedbackTemplateID.ToString() != rp1_selected && i.feedbackTemplateID.ToString() != rp2_selected && i.feedbackTemplateID.ToString() != rp0_selected)
                        rp3.Items.Add(new ListItem(i.header, i.feedbackTemplateID.ToString()));
                }
                if (!IsPostBack)
                {
                    rp0.SelectedValue = rp0_selected;
                    rp1.SelectedValue = rp1_selected;
                    rp2.SelectedValue = rp2_selected;
                    rp3.SelectedValue = rp3_selected;
                }
                
                var user = service.UserGetInfo(token, 10);
                
                string fdt = DateTime.Now.AddDays(1).ToString("yyMMdd");
                string tdt = "";
                string c = "1";
                if (compare == "Database") c = "2";
               
                if(timeframe == "Past week")
                {
                    tdt = DateTime.Now.AddDays(-7).ToString("yyMMdd");
                }
                else if(timeframe == "Past month")
                {
                    tdt = DateTime.Now.AddMonths(-1).ToString("yyMMdd");
                }
                else if(timeframe == "Past year")
                {
                    tdt = DateTime.Now.AddYears(-1).ToString("yyMMdd");
                }
                else if(timeframe == "Since first measure")
                {
                    foreach(var i in fft){
                       var s = service.UserGetFormFeedback(new HWService.UserGetFormFeedbackRequest(token, formKey, i.feedbackTemplateID, DateTime.Now.AddYears(-100), DateTime.Now.AddDays(1), language, 10)).UserGetFormFeedbackResult;
                       foreach (var x in s)
                       {
                           tdt = x.dateTime.ToString("yyMMdd");
                           break;
                       }
                       break;
                    }
                    //tdt = "100805";
                }

                chartlink = "https://test.healthwatch.se/lineChart.aspx?FDT="+tdt+"&TDT="+fdt+"&SID=0&LID="+language+"&UID="+user.userID+"&C="+c+"&RP0="+rp0_selected+"&RP1="+rp1_selected+"&RP2="+rp2_selected+"&RP3="+rp3_selected;
                
            }
            else
            {
                viewlink = "<a href='View.aspx?' data-icon='check' rel='external'>View...</a>";
                string formInstanceKey;
                if (Session["formInstanceKey"] == null) formInstanceKey = "";
                else formInstanceKey = Session["formInstanceKey"].ToString();


                if (Request.QueryString["fik"] != null)
                {
                    formInstanceKey = Request.QueryString["fik"];
                }
                exercises = service.ExerciseEnum(new HWService.ExerciseEnumRequest(token,0,0,language,10)).ExerciseEnumResult;            
                
                formInstance = service.UserGetFormInstanceFeedback(token,formKey,formInstanceKey,language,10);
                //Session.Add("latest",formInstance.dateTime.ToString("yyMMdd"));
                fifeedback = formInstance.fiv;
                if (fifeedback == null)
                    Response.Redirect("Dashboard.aspx");
            }
            
        }

        protected string replaceExerciseTags(string actionPlan)
        {
            if (actionPlan == null || actionPlan == "") return null;
            
            string newString = "";
            for (int c = 0; c < actionPlan.Length; c++)
            {
                if (c < actionPlan.Length - 5)
                {
                    if (actionPlan.Substring(c, 5) == "<EXID")
                    {
                        int id = int.Parse(actionPlan.Substring(c + 5, 3));
                        newString += "<a href='ExercisesItem.aspx?varid=" + getExerciseVariant(id) + "' class='statlink'>" + getExerciseName(id) + "</a>";
                        c = c + 8;
                    }
                    else newString += actionPlan[c];
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
                    return exe.exerciseVariant[0].exerciseVariantLangID + "" ;
            }

            return null;
        }

        protected void rp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string link = "Statistics.aspx?tf=" + timeframe + "&comp=" + compare;
            link += "&rp0=" + rp0.SelectedValue;
            link += "&rp1=" + rp1.SelectedValue;
            link += "&rp2=" + rp2.SelectedValue;
            link += "&rp3=" + rp3.SelectedValue;
            Response.Redirect(link);
        }

    }


}