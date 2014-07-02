using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{   
    //userGetProfileQuestion returns sortorder and not value
    public partial class ChangeProfile : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.Question[] profile;
        string token; 
        
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Default.aspx");

            token = Session["token"].ToString();
            int language = int.Parse(Session["languageID"].ToString());
            
            profile = service.ProfileQuestions(new HWService.ProfileQuestionsRequest(language,0)).ProfileQuestionsResult;
            
            if (!Page.IsPostBack)
            {
                populateDropDownLists();
                populateFormAnswers();
                
                dropDownListOccupation_SelectedIndexChanged(sender, e);
            }
            
        }


        public void populateFormAnswers()
        {
                HWService.UserInfo userinfo = service.UserGetInfo(token,10);
                dropDownListLanguage.SelectedIndex = (userinfo.languageID == 2)? 0:1;
                textBoxUsername.Text = userinfo.username;
                textBoxEmail.Text = userinfo.email;
                textBoxAlternateEmail.Text = userinfo.alternateEmail;

                string bday = service.UserGetProfileQuestion(4, token, 10);
                if (bday != "")
                {
                    birthYear.SelectedValue = bday.Substring(0, 4);
                    birthMonth.SelectedValue = bday.Substring(5, 2);
                    birthDay.SelectedValue = bday.Substring(8, 2);
                }
                rdbGender.SelectedValue = service.UserGetProfileQuestion(2, token, 10);
                rdbStatus.SelectedValue = service.UserGetProfileQuestion(7, token, 10);
                dropDownListOccupation.SelectedValue = service.UserGetProfileQuestion(9, token, 10);
                rdbOccupationType.SelectedValue = service.UserGetProfileQuestion(16, token, 10);
                dropDownListAnnualIncome.SelectedValue = service.UserGetProfileQuestion(8, token, 10);
                dropDownListEducation.SelectedValue = service.UserGetProfileQuestion(11, token, 10);
                dropDownListStudyArea.SelectedValue = service.UserGetProfileQuestion(10, token, 10);
                dropDownListIndustry.SelectedValue = service.UserGetProfileQuestion(5, token, 10);
                dropDownListJob.SelectedValue = service.UserGetProfileQuestion(6, token, 10);
                rdbManagerial.SelectedValue = service.UserGetProfileQuestion(19, token, 10);                
        }

        public void populateDropDownLists() {
            birthYear.DataSource = populateBirthYear();
            birthYear.DataBind();
            birthMonth.DataSource = populateBirthMonth();
            birthMonth.DataBind();
            birthDay.DataSource = populateBirthDay();
            birthDay.DataBind();
            
            foreach (var p in profile)
            {
                if (p.QuestionID == 9)
                {
                    dropDownListOccupation.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListOccupation.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
                else if (p.QuestionID == 8)
                {
                    dropDownListAnnualIncome.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListAnnualIncome.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
                else if (p.QuestionID == 11)
                {
                    dropDownListEducation.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListEducation.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
                else if (p.QuestionID == 5)
                {
                    dropDownListIndustry.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListIndustry.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
                else if (p.QuestionID == 6)
                {
                    dropDownListJob.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListJob.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
                else if (p.QuestionID == 10)
                {
                    dropDownListStudyArea.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListStudyArea.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }

            }
        }

        public List<string> populateBirthYear() {
            List<string> year = new List<string>();
            year.Add("YYYY");
            for (var i = 1900; i < DateTime.Now.Year; i++) 
                year.Add(i+"");
            return year;
        }

        public List<string> populateBirthMonth()
        {
            List<string> m = new List<string>();
            m.Add("MM");
            for (var i = 1; i <= 12; i++)
                m.Add(i.ToString().PadLeft(2, '0'));
            return m;
        }

        public List<string> populateBirthDay()
        {
            List<string> d = new List<string>();
            d.Add("DD");
            for (var i = 1; i <= 31; i++)
                d.Add(i.ToString().PadLeft(2,'0'));
            return d;
        }

        protected void dropDownListOccupation_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (dropDownListOccupation.SelectedValue == "375" || dropDownListOccupation.SelectedValue == "379")
            {
                divIndustry.Style["display"] = "block";
                divJob.Style["display"] = "block";
                divManagerial.Style["display"] = "block";
                divStudyArea.Style["display"] = "none"; 
            }
            else if (dropDownListOccupation.SelectedValue == "376")
            {
                divIndustry.Style["display"] = "none";
                divJob.Style["display"] = "none";
                divManagerial.Style["display"] = "none";
                divStudyArea.Style["display"] = "block"; 
            }
            else if (dropDownListOccupation.SelectedValue == "377")
            {
                divIndustry.Style["display"] = "none";
                divJob.Style["display"] = "block";
                divManagerial.Style["display"] = "none";
                divStudyArea.Style["display"] = "none"; 
            }
            else if (dropDownListOccupation.SelectedValue == "378") 
            {
                divIndustry.Style["display"] = "none";
                divJob.Style["display"] = "none";
                divManagerial.Style["display"] = "none";
                divStudyArea.Style["display"] = "none"; 
            }
            
        }
    }
}