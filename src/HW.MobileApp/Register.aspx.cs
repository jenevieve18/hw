using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class Register : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        string token;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void loginBtn_Click(object sender, EventArgs e) 
        {


            if (Session["token"] != null)
            {
                HttpCookie cToken = new HttpCookie("token");
                cToken.Value = Session["token"].ToString();
                cToken.Expires = DateTime.Now.AddMonths(5);
                Response.Cookies.Add(cToken);

                Response.Cookies["splash"].Value = null;
                Response.Cookies["splash"].Expires = DateTime.Now.AddDays(-1);

                Response.Redirect("ChangeProfile.aspx");
            }
            
        }

        protected void createBtn_Click(object sender, EventArgs e) 
        {
            bool flag1 = true;
            bool flag2 = true;
            bool flag3 = true;
            bool flag4 = true;
            bool flag5 = true;
            bool flag6 = true;

            labelMessage.Text = "";
            if (textBoxUsername.Text == "")
            {
                labelMessage.Text = labelMessage.Text + ((labelMessage.Text == "") ? "" : "<br>") + "* " + lblUsername.Text;
                flag1 = false;
            }
            if (textBoxPassword.Text == "")
            {
                labelMessage.Text = labelMessage.Text + ((labelMessage.Text == "") ? "" : "<br>") + "* " + lblPassword.Text;
                flag2 = false;
            }
            if (textBoxConfirmPassword.Text == "")
            {
                labelMessage.Text = labelMessage.Text + ((labelMessage.Text == "") ? "" : "<br>") + "* " + lblConfirmPassword.Text;
                flag3 = false;
            }
            if (!textBoxPassword.Text.Equals( textBoxConfirmPassword.Text))
            {
                labelMessage.Text = labelMessage.Text + ((labelMessage.Text == "") ? "" : "<br>") + "* " + lblPassword.Text + " & " + lblConfirmPassword.Text;
                flag4 = false;
            }
            if (textBoxEmail.Text == "")
            {
                labelMessage.Text = labelMessage.Text + ((labelMessage.Text == "") ? "" : "<br>") + "* " + lblEmail.Text;
                flag5 = false;
            }
            if (!cbTerms.Checked)
            {
                labelMessage.Text = labelMessage.Text + ((labelMessage.Text == "") ? "" : "<br>") + "* " + "Accept Terms & Conditions";
                flag6 = false;
            }

            if (flag1 && flag2 && flag3 && flag4 && flag5 && flag6)
            {
                token = service.UserCreate(textBoxUsername.Text, textBoxPassword.Text, textBoxEmail.Text, textBoxAlternateEmail.Text, cbTerms.Checked, int.Parse(dropDownListLanguage.SelectedValue), 0, 0, 10).token;
                if (token != "" && token !=null)
                {
                    //goto welcome page
                    Session.Add("token", token);
                    Session.Add("languageId", dropDownListLanguage.SelectedValue);
                    Response.Redirect("Register.aspx#welcome");
                }
                else labelMessage.Text = "Username maybe already be taken or Malformed Email.";
            }
            
        }

        protected void dropDownListLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropDownListLanguage.SelectedValue == "1"){
                lblLanguage.Text = "Språk";
                lblUsername.Text = "Användarnamn<span class='req'>*</span>";
                lblPassword.Text = "lösenord<span class='req'>*</span>";
                lblConfirmPassword.Text = "bekräfta lösenord<span class='req'>*</span>";
                cbTerms.Text = "jag accepterar";
                lblHeader.Text = "kontouppgifter";
            }
            else if (dropDownListLanguage.SelectedValue == "2"){
                lblLanguage.Text = "Language";
                lblUsername.Text = "Username<span class='req'>*</span>";
                lblPassword.Text = "Password<span class='req'>*</span>";
                lblConfirmPassword.Text = "Confirm Password<span class='req'>*</span>";
                cbTerms.Text = "I accept";
                lblHeader.Text = "Account Details";
            }
        }
    }
}