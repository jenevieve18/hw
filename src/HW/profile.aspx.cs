using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.FromHW;

namespace HW
{
    public partial class profile : System.Web.UI.Page
    {
        public bool guest = false;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (HttpContext.Current.Session["UserID"] == null)
            {
                HttpContext.Current.Response.Redirect("inactivity.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }

            #region Remove account
            if (!guest)
            {
                DeleteAccount.Controls.Add(new LiteralControl("<a href=\"javascript:if(confirm('"));
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1:
                        DeleteAccount.Controls.Add(new LiteralControl("Är du säker på att du vill ta bort ditt konto?"));
                        break;
                    case 2:
                        DeleteAccount.Controls.Add(new LiteralControl("Are you sure you want to delete your account?"));
                        break;
                }
                DeleteAccount.Controls.Add(new LiteralControl("')){location.href='login.aspx?Remove=Account'};\">"));
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1:
                        DeleteAccount.Controls.Add(new LiteralControl("Klicka här."));
                        break;
                    case 2:
                        DeleteAccount.Controls.Add(new LiteralControl("Klicka här"));
                        break;
                }
                DeleteAccount.Controls.Add(new LiteralControl("</a>"));
            }
            #endregion

            string functionScript = "";
            string startScript = "";
            string script = "";

            SqlDataReader rs = Db.rs("SELECT Username, Email, AltEmail FROM [User] WHERE UserID = " + HttpContext.Current.Session["UserID"]);
            if (rs.Read())
            {
                guest = (rs.GetString(0).IndexOf("AUTO_CREATED_GUEST") >= 0);

                #region Username
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:220px;\">"));
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1:
                        contents.Controls.Add(new LiteralControl((guest ? "Önskat användarnamn" : "Användarnamn")));
                        break;
                    case 2:
                        contents.Controls.Add(new LiteralControl((guest ? "Desired username" : "Username")));
                        break;
                }
                contents.Controls.Add(new LiteralControl("</div><div style=\"float:left;width:270px;\">"));

                TextBox username = new TextBox();
                username.Width = Unit.Pixel(250);
                username.CssClass = "regularTB";
                username.ID = "username";
                script = "chkTxt('username',5);";
                username.Attributes["onkeyup"] += script;
                startScript += script;
                if (!IsPostBack && !guest)
                {
                    username.Text = rs.GetString(0);
                }
                contents.Controls.Add(username);

                contents.Controls.Add(new LiteralControl("</div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;\"><IMG id=\"Starusername\" SRC=\"img/star.gif\"></div>"));
                #endregion

                #region Password
                contents.Controls.Add(new LiteralControl("<div style=\"clear:both;\"></div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:220px;\">"));
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1:
                        contents.Controls.Add(new LiteralControl((guest ? "Lösenord" : "Ev. nytt lösenord")));
                        break;
                    case 2:
                        contents.Controls.Add(new LiteralControl((guest ? "Password" : "Opt. new password")));
                        break;
                }
                contents.Controls.Add(new LiteralControl("</div><div style=\"float:left;width:270px;\">"));

                TextBox password = new TextBox();
                password.Width = Unit.Pixel(250);
                password.CssClass = "regularTB";
                password.ID = "password";
                password.TextMode = TextBoxMode.Password;
                if (guest)
                {
                    script = "chkTxt('password',5);";
                    password.Attributes["onkeyup"] += script;
                    startScript += script;
                }
                contents.Controls.Add(password);

                contents.Controls.Add(new LiteralControl("</div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;\">" + (guest ? "<IMG id=\"Starpassword\" SRC=\"img/star.gif\">" : "") + "</div>"));
                #endregion

                #region Email
                contents.Controls.Add(new LiteralControl("<div style=\"clear:both;\"></div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:220px;\">"));
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1:
                        contents.Controls.Add(new LiteralControl("E-postadress"));
                        break;
                    case 2:
                        contents.Controls.Add(new LiteralControl("Email address"));
                        break;
                }
                contents.Controls.Add(new LiteralControl("</div><div style=\"float:left;width:270px;\">"));

                TextBox email = new TextBox();
                email.Width = Unit.Pixel(250);
                email.CssClass = "regularTB";
                email.ID = "email";
                script = "chkEmail();";
                email.Attributes["onkeyup"] += script;
                startScript += script;
                if (!IsPostBack && !guest && !rs.IsDBNull(1))
                {
                    email.Text = rs.GetString(1);
                }
                contents.Controls.Add(email);

                contents.Controls.Add(new LiteralControl("</div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;\"><IMG id=\"Staremail\" SRC=\"img/star.gif\"></div>"));

                contents.Controls.Add(new LiteralControl("<div style=\"clear:both;\"></div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:220px;\">"));
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1:
                        contents.Controls.Add(new LiteralControl("Alternativ e-postadress"));
                        break;
                    case 2:
                        contents.Controls.Add(new LiteralControl("Alternate email address"));
                        break;
                }
                contents.Controls.Add(new LiteralControl("</div><div style=\"float:left;width:270px;\">"));

                TextBox aemail = new TextBox();
                aemail.Width = Unit.Pixel(250);
                aemail.CssClass = "regularTB";
                aemail.ID = "aemail";
                if (!IsPostBack && !guest && !rs.IsDBNull(2))
                {
                    aemail.Text = rs.GetString(2);
                }
                contents.Controls.Add(aemail);

                contents.Controls.Add(new LiteralControl("</div>"));
                #endregion
            }
            rs.Close();

            Db.renderBQ(true, guest, IsPostBack, contents, ref functionScript, ref startScript);

            #region Approve
            contents.Controls.Add(new LiteralControl("<div style=\"clear:both;\"></div>"));
            contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:490px;\">"));

            CheckBox cb = new CheckBox();
            cb.ID = "Approve";
            script = "chkChk('Approve');";
            cb.Attributes["onclick"] += script;
            startScript += script;
            contents.Controls.Add(cb);
            if (!IsPostBack && !guest)
            {
                ((CheckBox)contents.FindControl("Approve")).Checked = true;
            }

            contents.Controls.Add(new LiteralControl(""));
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1:
                    contents.Controls.Add(new LiteralControl("Jag accepterar tjänstens "));
                    break;
                case 2:
                    contents.Controls.Add(new LiteralControl("I accept the "));
                    break;
            }
            contents.Controls.Add(new LiteralControl("<a href=\"JavaScript:void(window.open('policy.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "','','width=600,height=600,scrollbars=yes'));\">"));
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1:
                    contents.Controls.Add(new LiteralControl("integritetspolicy & användarvillkor"));
                    break;
                case 2:
                    contents.Controls.Add(new LiteralControl("terms & conditions of the service"));
                    break;
            }
            contents.Controls.Add(new LiteralControl("</a>."));

            contents.Controls.Add(new LiteralControl("</div>"));

            contents.Controls.Add(new LiteralControl("<div style=\"float:left;\"><IMG id=\"StarApprove\" SRC=\"img/star.gif\"></div>"));
            #endregion

            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1:
                    submit.Text = "Spara";
                    break;
                case 2:
                    submit.Text = "Save";
                    break;
            }

            error.Controls.Clear();
            error.Controls.Add(new LiteralControl("<div style=\"clear:both;\">&nbsp;</div><div style=\"float:left;\"><IMG SRC=\"img/star.gif\">&nbsp;"));
            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1:
                    error.Controls.Add(new LiteralControl("Obligatoriskt."));
                    break;
                case 2:
                    error.Controls.Add(new LiteralControl("Required."));
                    break;
            }
            error.Controls.Add(new LiteralControl("</div>"));

            submit.Click += new EventHandler(submit_Click);

            ClientScript.RegisterStartupScript(this.GetType(), "START_SCRIPT", "<script language=\"JavaScript\">" + startScript + "</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "FN_SCRIPT", "<script language=\"JavaScript\">" + functionScript + "</script>");
        }

        private void submit_Click(object sender, EventArgs e)
        {
            string errorMsg = "";
            if (((TextBox)contents.FindControl("username")).Text.Length < 5)
            {
                errorMsg = "<SPAN STYLE=\"color:#CC0000;\">";
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1:
                        errorMsg += "Användarnamnet måste vara minst 5 tecken långt!";
                        break;
                    case 2:
                        errorMsg += "The username must be at least 5 characters long!";
                        break;
                }
                errorMsg += "</SPAN>";
            }
            else if ((guest || ((TextBox)contents.FindControl("password")).Text != "") && ((TextBox)contents.FindControl("password")).Text.Length < 5)
            {
                errorMsg = "<SPAN STYLE=\"color:#CC0000;\">";
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1:
                        errorMsg += "Lösenordet måste vara minst 5 tecken långt!";
                        break;
                    case 2:
                        errorMsg += "The password must be at least 5 characters long!";
                        break;
                }
                errorMsg += "</SPAN>";
            }
            else
            {
                string errorText = "";
                bool allForced = Db.checkForced(ref errorText, contents);

                if (!((CheckBox)contents.FindControl("Approve")).Checked)
                {
                    allForced = false;
                }

                if (allForced)
                {
                    bool valid = false;

                    SqlDataReader rs = Db.rs("SELECT " +
                            "COUNT(*) " +
                            "FROM [User] " +
                            "WHERE UserID <> " + HttpContext.Current.Session["UserID"] + " " +
                            "AND LOWER(Username) = '" + ((TextBox)contents.FindControl("username")).Text.Replace("'", "").ToLower() + "'");
                    if (rs.Read() && rs.GetInt32(0) == 0)
                    {
                        valid = true;
                    }
                    rs.Close();

                    if (valid)
                    {
                        Db.exec("UPDATE [User] SET " +
                            "Username = '" + ((TextBox)contents.FindControl("username")).Text.Replace("'", "") + "', " +
                            "Email = '" + ((TextBox)contents.FindControl("email")).Text.Replace("'", "") + "'," +
                            "AltEmail = '" + ((TextBox)contents.FindControl("aemail")).Text.Replace("'", "") + "'" + (((TextBox)contents.FindControl("password")).Text != "" ? ", " +
                            "Password = '" + Db.HashMD5(((TextBox)contents.FindControl("password")).Text.Trim()) + "'" : "") + " " +
                            "WHERE UserID = " + HttpContext.Current.Session["UserID"]);

                        Db.saveBQ(contents);

                        Db.loadUserSession(Convert.ToInt32(HttpContext.Current.Session["UserID"]));

                        HttpContext.Current.Response.Redirect("profile.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                    }
                    else
                    {
                        errorMsg = "<SPAN STYLE=\"color:#CC0000;\">";
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1:
                                errorMsg += "Detta användarnamn är upptaget!";
                                break;
                            case 2:
                                errorMsg += "This username is already taken!";
                                break;
                        }
                        errorMsg += "</SPAN> ";
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1:
                                errorMsg += "Vänligen prova ett annat";
                                break;
                            case 2:
                                errorMsg += "Please try a different one";
                                break;
                        }
                        errorMsg += ".";
                    }
                }
                else
                {
                    errorMsg = "<SPAN STYLE=\"color:#CC0000;\">";
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1:
                            errorMsg = (errorText != "" ? errorText : "Alla obligatoriska frågor måste besvaras.");
                            break;
                        case 2:
                            errorMsg = (errorText != "" ? errorText : "All required fields must be filled in.");
                            break;
                    }
                    errorMsg += "</SPAN>";
                }
            }

            error.Controls.Clear();
            error.Controls.Add(new LiteralControl("<div style=\"clear:both;\">&nbsp;</div><div style=\"float:left;\">" + errorMsg + "</div>"));
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}