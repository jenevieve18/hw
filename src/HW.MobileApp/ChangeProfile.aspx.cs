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
        protected string policylink;
        protected int language;
        HWService.Question[] profileQ;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");

            token = Session["token"].ToString();
            language = int.Parse(Session["languageID"].ToString());

            if (IsPostBack)
            {
                if (!(String.IsNullOrEmpty(textBoxPassword.Text.Trim())))
                {
                    textBoxPassword.Attributes["value"] = textBoxPassword.Text;
                }
                if (!(String.IsNullOrEmpty(textBoxConfirmPassword.Text.Trim())))
                {
                    textBoxConfirmPassword.Attributes["value"] = textBoxConfirmPassword.Text;
                }
            }

            language = int.Parse(dropDownListLanguage.SelectedValue);
            profileQ = service.ProfileQuestions(new HWService.ProfileQuestionsRequest(language, 0)).ProfileQuestionsResult;
            policylink = "href='termsEnglish.html'";
            if (language == 1) policylink = "href='termsSwedish.html'";
            populateForm();
            if (!Page.IsPostBack) { 
                dropDownListOccupation_SelectedIndexChanged(sender, e);
                rdbMangerialYes_CheckedChanged(sender, e);
            }
            
        }

        public HWService.Question getProfileQuestion(int id)
        {
            foreach (var p in profileQ)
            {
                if (id == p.QuestionID)
                    return p;
            }
            return null;
        }

        public void populateDDLs()
        {
            foreach (var p in profileQ)
            {
                if (p.QuestionID == 9)
                {
                    dropDownListOccupation.Items.Clear();
                    dropDownListOccupation.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListOccupation.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
                else if (p.QuestionID == 5)
                {
                    dropDownListIndustry.Items.Clear();
                    dropDownListIndustry.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListIndustry.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
                else if (p.QuestionID == 6)
                {
                    dropDownListJob.Items.Clear();
                    dropDownListJob.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListJob.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
                else if (p.QuestionID == 10)
                {
                    dropDownListStudyArea.Items.Clear();
                    dropDownListStudyArea.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListStudyArea.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
                else if (p.QuestionID == 8)
                {
                    dropDownListAnnualIncome.Items.Clear();
                    dropDownListAnnualIncome.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListAnnualIncome.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
                else if (p.QuestionID == 11)
                {
                    dropDownListEducation.Items.Clear();
                    dropDownListEducation.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListEducation.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
                else if (p.QuestionID == 20)
                {
                    dropDownListSubordinates.Items.Clear();
                    dropDownListSubordinates.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListSubordinates.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
                else if (p.QuestionID == 13)
                {
                    dropDownListCoffee.Items.Clear();
                    dropDownListCoffee.Items.Add(new ListItem("Choose one...", "0"));
                    foreach (var a in p.AnswerOptions)
                    {
                        dropDownListCoffee.Items.Add(new ListItem(a.AnswerText, a.AnswerID.ToString()));
                    }
                }
            }
        }

        public void populateForm()
        {
            if (!Page.IsPostBack)
            {
                birthYear.DataSource = Enumerable.Range(1900, DateTime.Now.Year);
                birthYear.DataBind();
                birthMonth.DataSource = Enumerable.Range(1, 12).Select(i => i.ToString("D2"));
                birthMonth.DataBind();
                birthDay.DataSource = Enumerable.Range(1, 31).Select(i => i.ToString("D2"));
                birthDay.DataBind();
                populateDDLs();
                populateFormAnswers();
                cbTerms.Checked = true;

                

            }

            var x = getProfileQuestion(4);
            if (x != null)
                lblBirth.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
            x = getProfileQuestion(2);
            if (x != null)
            {
                lblGender.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
                foreach (var i in x.AnswerOptions)
                {
                    if (i.AnswerID == 2) rdbGenderFemale.Text = i.AnswerText;
                    else rdbGenderMale.Text = i.AnswerText;
                }
            }
            x = getProfileQuestion(7);
            if (x != null)
            {
                lblStatus.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
                foreach (var i in x.AnswerOptions)
                {
                    if (i.AnswerID == 369) rdbStatusMarried.Text = i.AnswerText;
                    else rdbStatusSingle.Text = i.AnswerText;
                }
            }
            x = getProfileQuestion(9);
            if (x != null)
                lblOccupation.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
            x = getProfileQuestion(16);
            if (x != null)
            {
                lblOccupationType.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
                foreach (var i in x.AnswerOptions)
                {
                    if (i.AnswerID == 405) rdbOccupationTypeFull.Text = i.AnswerText;
                    else rdbOccupationTypePart.Text = i.AnswerText;
                }
            }
            x = getProfileQuestion(5);
            if (x != null)
                LblIndustry.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
            x = getProfileQuestion(6);
            if (x != null)
                LblJob.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
            x = getProfileQuestion(19);
            if (x != null)
            {
                LblManage.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
                foreach (var i in x.AnswerOptions)
                {
                    if (i.AnswerID == 412) rdbMangerialYes.Text = i.AnswerText;
                    else rdbMangerialNo.Text = i.AnswerText;
                }
            }
            x = getProfileQuestion(10);
            if (x != null)
                LblStudyArea.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
            x = getProfileQuestion(8);
            if (x != null)
                lblIncome.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
            x = getProfileQuestion(11);
            if (x != null)
                lblEducation.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
            x = getProfileQuestion(20);
            if (x != null)
                lblSubordinates.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
            x = getProfileQuestion(13);
            if (x != null)
                lblCoffee.Text = x.QuestionText + ((x.Mandatory) ? "<span style='color:red;'>*</span>" : "");
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

        protected void dropDownListLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateDDLs();
        }

        protected void rdbMangerialYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMangerialYes.Checked)
                divSubordinates.Style["display"] = "block";
            else
                divSubordinates.Style["display"] = "none";
        }

        protected void saveChangesBtn_Click(object sender, EventArgs e)
        {   
            Boolean flag1 = false;
            Boolean flag2 = true;
            Boolean flag3 = false;

            var gender = "";
            var status = "";
            var occupationtype = "";
            var managerial = "";

            if (rdbGenderMale.Checked) gender = "1";
            else if (rdbGenderFemale.Checked) gender = "2";

            if (rdbStatusMarried.Checked) status = "369";
            else if (rdbStatusSingle.Checked) status = "370";

            if (rdbOccupationTypeFull.Checked) occupationtype = "405";
            else if (rdbOccupationTypePart.Checked) occupationtype = "406";

            if (rdbMangerialYes.Checked) managerial = "413";
            else if (rdbMangerialNo.Checked) managerial = "412";

            
            if (service.UserUpdateInfo(textBoxUsername.Text, textBoxEmail.Text, textBoxAlternateEmail.Text, token, 10)) flag1 = true;
            if (textBoxPassword.Text != ""&&textBoxConfirmPassword.Text!="" && textBoxConfirmPassword.Text == textBoxPassword.Text)
            { 
                if(!service.UserUpdatePassword(textBoxPassword.Text,token,10) )flag2=false;
            }
            if (service.UserSetProfileQuestion(4,birthYear.SelectedValue+"-"+birthMonth.SelectedValue+"-"+birthDay.SelectedValue,token,10)&&
                service.UserSetProfileQuestion(2,gender,token,10)&&
                service.UserSetProfileQuestion(7,status,token,10)&&
                service.UserSetProfileQuestion(9,dropDownListOccupation.SelectedValue,token,10)&&
                service.UserSetProfileQuestion(8,dropDownListAnnualIncome.SelectedValue,token,10)&&
                service.UserSetProfileQuestion(11,dropDownListEducation.SelectedValue,token,10)&&
                cbTerms.Checked)
            { flag3 = true; }

            service.UserSetProfileQuestion(16,occupationtype,token,10);
            service.UserSetProfileQuestion(19,managerial,token,10);
            service.UserSetProfileQuestion(10,dropDownListStudyArea.SelectedValue,token,10);
            service.UserSetProfileQuestion(5,dropDownListIndustry.SelectedValue,token,10);
            service.UserSetProfileQuestion(6,dropDownListJob.SelectedValue,token,10);
            service.UserSetProfileQuestion(20,dropDownListSubordinates.SelectedValue,token,10);
            service.UserSetProfileQuestion(13, dropDownListCoffee.SelectedValue, token, 10);
            service.UserUpdateLanguage(token, int.Parse(dropDownListLanguage.SelectedValue), 10);
            Session["languageID"] = int.Parse(dropDownListLanguage.SelectedValue);

            if(flag1 && flag2 && flag3)
                Response.Redirect("Settings.aspx");
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
                if (service.UserGetProfileQuestion(2, token, 10) == "1")
                    rdbGenderMale.Checked = true;
                else rdbGenderFemale.Checked = true;

                if (service.UserGetProfileQuestion(7, token, 10) == "369")
                    rdbStatusMarried.Checked = true;
                else rdbStatusSingle.Checked = true;

                if (service.UserGetProfileQuestion(16, token, 10) == "405")
                    rdbOccupationTypeFull.Checked = true;
                else rdbOccupationTypePart.Checked = true;

                if (service.UserGetProfileQuestion(19, token, 10) == "413")
                    rdbMangerialYes.Checked = true;
                else rdbMangerialNo.Checked = true;
            
                dropDownListOccupation.SelectedValue = service.UserGetProfileQuestion(9, token, 10);
                dropDownListAnnualIncome.SelectedValue = service.UserGetProfileQuestion(8, token, 10);
                dropDownListEducation.SelectedValue = service.UserGetProfileQuestion(11, token, 10);
                dropDownListStudyArea.SelectedValue = service.UserGetProfileQuestion(10, token, 10);
                dropDownListIndustry.SelectedValue = service.UserGetProfileQuestion(5, token, 10);
                dropDownListJob.SelectedValue = service.UserGetProfileQuestion(6, token, 10);
                dropDownListCoffee.SelectedValue = service.UserGetProfileQuestion(13, token, 10);
                dropDownListSubordinates.SelectedValue = service.UserGetProfileQuestion(20, token, 10);              
        }

        
    }
}