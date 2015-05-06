using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace healthWatch
{
	/// <summary>
	/// Summary description for register.
	/// </summary>
	public partial class register : System.Web.UI.Page
	{
        int sponsorInviteID = 0;
        string[,] lang;

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (HttpContext.Current.Request.QueryString["Consent"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["Consent"]) == 1)
            {
                HttpContext.Current.Session["SponsorInviteConsent"] = "1";
            }
            if (HttpContext.Current.Request.QueryString["SendPass"] != null)
            {
                Db.sendPasswordReminder("", Convert.ToInt32(HttpContext.Current.Request.QueryString["SendPass"]),false);
            }
            //string changeLang = "<a class=\"subheaderLink\" HREF=\"/home.aspx?LID=";
            //switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            //{
            //    case 1: { changeLang += "2"; break; }
            //    case 2: { changeLang += "1"; break; }
            //}
            //changeLang += "&Goto=register.aspx&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"><img src=\"/img/arrow1.gif\" width=\"8\" height=\"7\" border=\"0\" />";
            //switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            //{
            //    case 1: { changeLang += "In english"; break; }
            //    case 2: { changeLang += "På svenska"; break; }
            //}
            //changeLang += "</a>";
            //ChangeLang.Text = changeLang;

            lang = new string[2,27] { 
                { 
                    "E-postadressen", 
                    "finns redan i HealthWatch databas.", 
                    "Jag vill skapa ett nytt konto", 
                    "Jag vill använda mitt befintliga konto, med användarnamn", 
                    "", 
                    "Lösenord till markerat konto ovan", 
                    "Felaktigt lösenord!", 
                    "Nästa", 
                    "Du är redan inloggad.", 
                    "Länken som du använde för att komma till denna sida är felaktig eller har redan använts!<br/>Om du använt länken och skapat ett konto kan du logga in på",
                    "startsidan",
                    "Önskat användarnamn",
                    "minst 5 tecken",
                    "Lösenord",
                    "E-postadress",
                    "Jag accepterar tjänstens",
                    "integritetspolicy & användarvillkor",
                    "Slutför",
                    "Obligatoriskt",
                    "Användarnamnet måste vara minst 5 tecken långt!",
                    "Lösenordet måste vara minst 5 tecken långt!",
                    "Detta användarnamn finns redan registrerat!",
                    "Vänligen prova ett annat",
                    "Alla obligatoriska frågor måste besvaras",
                    "Om du glömt ditt lösenord, ",
                    "klicka här för att få ett mail med återställningsinstruktioner.",
                    "Alternativ e-postadress"
                }, { 
                    "The email address", 
                    "already exist in the HealthWatch database.", 
                    "I want to create a new account", 
                    "I want to use my existing account, with username", 
                    "", 
                    "Password to the checked account above", 
                    "Incorrect password!", 
                    "Next", 
                    "You are already logged in.", 
                    "The link you followed to this page is incorrect or has already been used!<br/>If you have used the link to created an account you can log in on", 
                    "the start page",
                    "Desired username",
                    "at least 5 characters",
                    "Password",
                    "Email address",
                    "I accept the",
                    "terms & conditions of the service",
                    "Submit",
                    "Required",
                    "The username must be at least 5 characters",
                    "The password must be at least 5 characters",
                    "This username is already used!",
                    "Please try a different one",
                    "All required fields must be filled out",
                    "If you've forgotten your password, ",
                    "click here and we'll send you an email with recovery instructions.",
                    "Alternate email address"
                } 
            };

            if (Convert.ToInt32(HttpContext.Current.Session["UserID"]) != 0)
            {
                sponsorInviteID = -2;
            }
            else
            {
                sponsorInviteID = Convert.ToInt32(HttpContext.Current.Session["SponsorInviteID"]);

                if (sponsorInviteID > 0)
                {
                    SqlDataReader rs = Db.rs("SELECT SponsorInviteID FROM SponsorInvite WHERE UserID IS NULL AND SponsorInviteID = " + sponsorInviteID);
                    if (!rs.Read())
                    {
                        sponsorInviteID = -1;
                    }
                    rs.Close();

                    if (sponsorInviteID == -1)
                    {
                        HttpContext.Current.Session["SponsorInviteID"] = -1;
                        HttpContext.Current.Session["SponsorID"] = 1;
                        HttpContext.Current.Session["DepartmentID"] = "NULL";
                        HttpContext.Current.Response.Redirect("/register.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                    }
                }
            }

            string sponsorInvitedEmailAddressExistBuffer = "";

            if (sponsorInviteID > 0 && HttpContext.Current.Session["NoCheckEmailDupe"] == null)
            {
                SqlDataReader rs = Db.rs("SELECT " +
                    "u.UserID, " +
                    "u.Username, " +
                    "u.SponsorID, " +
                    "s.Sponsor " +
                    "FROM [User] u " +
                    "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
                    "WHERE u.Email = '" + HttpContext.Current.Session["Email"].ToString().Replace("'", "''") + "' " +
                    "AND (u.SponsorID IS NULL OR u.SponsorID <> " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + ")");
                if (rs.Read())
                {
                    int dupeAction = (HttpContext.Current.Request.Form["DupeAction"] == null ? -1 : Convert.ToInt32(HttpContext.Current.Request.Form["DupeAction"]));
                    sponsorInvitedEmailAddressExistBuffer += "" +
                        lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 0] + " \"" + HttpContext.Current.Session["Email"] + "\" " + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 1] +
                        "<br/>";
                    do
                    {
                        if (dupeAction == -1) { dupeAction = rs.GetInt32(0); }

                        sponsorInvitedEmailAddressExistBuffer += "" +
                            "<br/><input type=\"radio\" onclick=\"document.getElementById('DupePasswordBox').style.display='';\" name=\"DupeAction\" value=\"" + rs.GetInt32(0) + "\"" + 
                            (dupeAction == rs.GetInt32(0) ? " checked" : "") + "> " + lang[Convert.ToInt32(HttpContext.Current.Session["LID"])-1,3] + " \"" + rs.GetString(1) + "\"<!-- (" + rs.GetString(3) + ")-->.";
                    }
                    while (rs.Read());

                    sponsorInvitedEmailAddressExistBuffer += "<br/><input type=\"radio\" onclick=\"document.getElementById('DupePasswordBox').style.display='none';\" name=\"DupeAction\" value=\"0\"" +
                        (dupeAction == 0 ? " checked" : "") +
                        "> " + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 2] + "<!-- (" + HttpContext.Current.Application["SPN" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"])] + ")-->.";

                    sponsorInvitedEmailAddressExistBuffer += "<div" + (dupeAction != 0 ? "" : " style=\"display:none\"") + " id=\"DupePasswordBox\"><br/>" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 5] + " <input type=\"password\" name=\"DupePassword\"/>" + (IsPostBack ? "&nbsp;<span style=\"color:#cc0000;\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 6] + "</span>" : "") + "<br/><br/>" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 24] + "<a href=\"javascript:void(location.href='register.aspx?SendPass='+selectedDupeAction());\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 25] + "</a></div><br/>";
                }
                rs.Close();
            }

            next.Visible = false;
            submit.Visible = true;
            if(sponsorInvitedEmailAddressExistBuffer != "")
            {
                next.Visible = true;
                submit.Visible = false;
                contents.Controls.Add(new LiteralControl(sponsorInvitedEmailAddressExistBuffer));

                next.Text = lang[Convert.ToInt32(HttpContext.Current.Session["LID"])-1,7];
                next.Click += new EventHandler(next_Click);
            }
            else if (sponsorInviteID == -2)
            {
                error.Controls.Clear();
                error.Controls.Add(new LiteralControl("<span STYLE=\"color:#CC0000;\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"])-1,8] + "</span>"));
            } 
            else if (sponsorInviteID == -1)
            {
                error.Controls.Clear();
                error.Controls.Add(new LiteralControl("<span STYLE=\"color:#CC0000;\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"])-1,9] + " <A HREF=\"home.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"])-1,10] + "</A>.</span>"));
                HttpContext.Current.Session["SponsorInviteID"] = null;
            }
            else
            {
                string functionScript = "";
                string startScript = "";
                string script = "";

                if (HttpContext.Current.Request.QueryString["Login"] != null && HttpContext.Current.Request.QueryString["Login"].ToString() == "Guest")
                {
                    int userID = Db.createAccount("AUTO_CREATED_GUEST_" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), "", "AUTO_CREATED_PASS_" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), 1, "NULL", 0, "");
                    Db.checkAndLogin(userID);
                }
                //contents.Controls.Add(new LiteralControl("<div style=\"clear:float;\">"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:220px;\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 11] + "</div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:270px;\">"));

                TextBox username = new TextBox();
                username.Width = Unit.Pixel(250);
                username.CssClass = "regularTB";
                username.ID = "username";
                script = "chkTxt('username',5);";
                username.Attributes["onkeyup"] += script;
                startScript += script;
                contents.Controls.Add(username);

                contents.Controls.Add(new LiteralControl("</div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;\"><IMG id=\"Starusername\" SRC=\"img/star.gif\"><!--&nbsp;(" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 12] + ")--></div>"));
                //contents.Controls.Add(new LiteralControl("</div>"));

                contents.Controls.Add(new LiteralControl("<div style=\"clear:both;\"></div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:220px;\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 13] + "</div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:270px;\">"));

                TextBox password = new TextBox();
                password.Width = Unit.Pixel(250);
                password.CssClass = "regularTB";
                password.ID = "password";
                password.TextMode = TextBoxMode.Password;
                script = "chkTxt('password',5);";
                password.Attributes["onkeyup"] += script;
                startScript += script;
                contents.Controls.Add(password);

                contents.Controls.Add(new LiteralControl("</div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;\"><IMG id=\"Starpassword\" SRC=\"img/star.gif\"><!--&nbsp;(" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 12] + ")--></div>"));
                //contents.Controls.Add(new LiteralControl("</div>"));

                contents.Controls.Add(new LiteralControl("<div style=\"clear:both;\"></div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:220px;\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 14] + "</div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:270px;\">"));

                TextBox email = new TextBox();
                email.Width = Unit.Pixel(250);
                email.CssClass = "regularTB";
                email.ID = "email";
                script = "chkEmail();";
                email.Attributes["onkeyup"] += script;
                startScript += script;
                if (!IsPostBack)
                {
                    email.Text = (HttpContext.Current.Session["Email"] != null ? HttpContext.Current.Session["Email"].ToString() : "");
                }
                contents.Controls.Add(email);

                contents.Controls.Add(new LiteralControl("</div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;\"><IMG id=\"Staremail\" SRC=\"img/star.gif\"></div>"));

                contents.Controls.Add(new LiteralControl("<div style=\"clear:both;\"></div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:220px;\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 26] + "</div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:270px;\">"));

                TextBox aemail = new TextBox();
                aemail.Width = Unit.Pixel(250);
                aemail.CssClass = "regularTB";
                aemail.ID = "aemail";
                contents.Controls.Add(aemail);

                contents.Controls.Add(new LiteralControl("</div>"));
                //contents.Controls.Add(new LiteralControl("</div>"));

                //contents.Controls.Add(new LiteralControl("<TABLE><TR><TD>"));

                Db.renderBQ(false, false, IsPostBack, contents, ref functionScript, ref startScript);

                //contents.Controls.Add(new LiteralControl("</TD></TR></TABLE>"));

                contents.Controls.Add(new LiteralControl("<div style=\"clear:both;\"></div>"));
                contents.Controls.Add(new LiteralControl("<div style=\"float:left;width:490px;\">"));

                CheckBox cb = new CheckBox();
                cb.ID = "Approve";
                script = "chkChk('Approve');";
                cb.Attributes["onclick"] += script;
                startScript += script;
                contents.Controls.Add(cb);

                contents.Controls.Add(new LiteralControl("" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 15] + " <A class=\"lnk\" HREF=\"JavaScript:void(window.open('policy.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "','','width=600,height=600,scrollbars=yes'));\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 16] + "</A>."));
                contents.Controls.Add(new LiteralControl("</div>"));

                contents.Controls.Add(new LiteralControl("<div style=\"float:left;\"><IMG id=\"StarApprove\" SRC=\"img/star.gif\"></div>"));

                //contents.Controls.Add(new LiteralControl("<div style=\"clear:both;float:right;\">"));

                //Button submit = new Button();
                //submit.ID = "submit";
                submit.Text = "" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 17] + "";
                //contents.Controls.Add(submit);

                //contents.Controls.Add(new LiteralControl("</div>"));

                error.Controls.Clear();
                error.Controls.Add(new LiteralControl("<div style=\"clear:both;\">&nbsp;</div><div style=\"float:left;\"><IMG SRC=\"img/star.gif\"> " + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 18] + ".</div>"));

                submit.Click += new EventHandler(submit_Click);

                ClientScript.RegisterStartupScript(this.GetType(),"START_SCRIPT", "<script language=\"JavaScript\">" + startScript + "</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "FN_SCRIPT", "<script language=\"JavaScript\">" + functionScript + "</script>");
            }
        }

        void next_Click(object sender, EventArgs e)
        {
            bool redirect = false;

            if (HttpContext.Current.Request.Form["DupeAction"] != null)
            {
                int userID = Convert.ToInt32(HttpContext.Current.Request.Form["DupeAction"]);
                if (userID == 0)
                {
                    HttpContext.Current.Session["NoCheckEmailDupe"] = 1;
                    redirect = true;
                }
                else
                {
                    SqlDataReader rs = Db.rs("SELECT UserID FROM [User] " +
                        "WHERE Email = '" + HttpContext.Current.Session["Email"].ToString().Replace("'", "''") + "' " +
                        "AND " +
                        "(" +
                        "SponsorID IS NULL " +
                        "OR " +
                        "SponsorID <> " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "" +
                        ") " +
                        "AND UserID = " + userID + " " +
                        "AND Password = '" + Db.HashMD5(HttpContext.Current.Request.Form["DupePassword"].ToString().Trim()) + "'");
                    if (rs.Read())
                    {
                        redirect = true;
                    }
                    rs.Close();

                    if (redirect)
                    {
                        #region Update profile
                        int profileID = 0;

                        SqlDataReader rs2 = Db.rs("SELECT " +
                            "u.UserProfileID, " +
                            "up.ProfileComparisonID " +
                            "FROM [User] u " +
                            "INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID " +
                            "WHERE u.UserID = " + userID);
                        if (rs2.Read())
                        {
                            #region Create new profile
                            Db.exec("INSERT INTO UserProfile (UserID,SponsorID,DepartmentID,ProfileComparisonID,Created) VALUES (" + userID + "," + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "," + (HttpContext.Current.Session["DepartmentID"] != null ? HttpContext.Current.Session["DepartmentID"].ToString() : "NULL") + "," + rs2.GetInt32(1) + ",GETDATE())");
                            SqlDataReader rs3 = Db.rs("SELECT TOP 1 UserProfileID FROM UserProfile WHERE UserID = " + userID + " ORDER BY UserProfileID DESC");
                            if (rs3.Read())
                            {
                                profileID = rs3.GetInt32(0);
                            }
                            rs3.Close();
                            #endregion

                            rs = Db.rs("SELECT " +
                                "sbq.BQID, " +           // 0
                                "bq.Type, " +            // 1
                                "sbq.SponsorBQID, " +    // 2
                                "sbq.Hidden, " +         // 3
                                "b.ValueInt, " +         // 4
                                "b.ValueText, " +        // 5
                                "b.ValueDate " +         // 6
                                "FROM Sponsor s " +
                                "INNER JOIN SponsorBQ sbq ON s.SponsorID = sbq.SponsorID " +
                                "INNER JOIN BQ ON BQ.BQID = sbq.BQID " +
                                "LEFT OUTER JOIN UserProfileBQ b ON b.BQID = BQ.BQID AND b.UserProfileID = " + rs2.GetInt32(0) + " " +
                                "WHERE s.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
                            while (rs.Read())
                            {
                                if(!rs.IsDBNull(3) && rs.GetInt32(3) == 1 && Convert.ToInt32(HttpContext.Current.Session["SponsorInviteID"]) > 0)
                                {
                                    rs3 = Db.rs("SELECT " +
                                        "BAID, " +
                                        "ValueInt, " +
                                        "ValueDate, " +
                                        "ValueText " +
                                        "FROM SponsorInviteBQ " +
                                        "WHERE SponsorInviteID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorInviteID"]) + " " +
                                        "AND BQID = " + rs.GetInt32(0));
                                    if (rs3.Read())
                                    {
                                        if (!rs3.IsDBNull(0))
                                        {
                                            Db.exec("INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueInt) VALUES (" + profileID + "," + rs.GetInt32(0) + "," + rs3.GetInt32(0) + ")");
                                        }
                                        if (!rs3.IsDBNull(1))
                                        {
                                            Db.exec("INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueInt) VALUES (" + profileID + "," + rs.GetInt32(0) + "," + rs3.GetInt32(1) + ")");
                                        }
                                        if (!rs3.IsDBNull(2))
                                        {
                                            Db.exec("INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueDate) VALUES (" + profileID + "," + rs.GetInt32(0) + ",'" + rs3.GetDateTime(2).ToString("yyyy-MM-dd") + "')");
                                        }
                                        if (!rs3.IsDBNull(3))
                                        {
                                            Db.exec("INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueText) VALUES (" + profileID + "," + rs.GetInt32(0) + ",'" + rs3.GetString(3).Replace("'","''") + "')");
                                        }
                                    }
                                    rs3.Close();
                                }
                                else if(!rs.IsDBNull(4) || !rs.IsDBNull(5) || !rs.IsDBNull(6))
                                {
                                    Db.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt,ValueText,ValueDate) VALUES (" + profileID + "," + rs.GetInt32(0) + "," +
                                        (rs.IsDBNull(4) ? "NULL" : rs.GetInt32(4).ToString()) + "," +
                                        (rs.IsDBNull(5) ? "NULL" : "'" + rs.GetString(5).Replace("'", "") + "'") + "," +
                                        (rs.IsDBNull(6) ? "NULL" : "'" + rs.GetDateTime(6).ToString("yyyy-MM-dd") + "'") +
                                        ")");
                                }
                            }
                            rs.Close();

                            Db.exec("UPDATE [User] SET " +
                                "UserProfileID = " + profileID + ", " +
                                "DepartmentID = " + (HttpContext.Current.Session["DepartmentID"] != null ? HttpContext.Current.Session["DepartmentID"].ToString() : "NULL") + ", " +
                                "SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
                                "WHERE UserID = " + userID);
                        }
                        rs2.Close();
                        #endregion

                        if (Convert.ToInt32(HttpContext.Current.Session["SponsorInviteID"]) > 0)
                        {
                            Db.exec("UPDATE SponsorInvite SET " +
                                "UserID = " + userID + ", " +
                                "Consent = " + (HttpContext.Current.Session["SponsorInviteConsent"] != null ? "GETDATE()" : "NULL") + " " +
                                "WHERE SponsorInviteID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorInviteID"]));
                        }
                        HttpContext.Current.Session["SponsorInviteID"] = null;

                        Db.checkAndLogin(userID);
                    }
                }
                if (redirect)
                {
                    HttpContext.Current.Response.Redirect("register.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                }
            }
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

		private void submit_Click(object sender, EventArgs e)
		{
			string errorMsg = "";
			if(((TextBox)contents.FindControl("username")).Text.Length < 5)
			{
                errorMsg = "<SPAN STYLE=\"color:#CC0000;\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 19] + "</SPAN>";
			}
			else if(((TextBox)contents.FindControl("password")).Text.Length < 5)
			{
                errorMsg = "<SPAN STYLE=\"color:#CC0000;\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 20] + "</SPAN>";
			}
			else
			{
                string errorText = "";
                bool allForced = Db.checkForced(ref errorText, contents);
				
				if(!((CheckBox)contents.FindControl("Approve")).Checked)
				{
					allForced = false;
				}

				if(allForced)
				{
					bool valid = false;

					SqlDataReader rs = Db.rs("SELECT COUNT(*) FROM [User] WHERE LOWER(Username) = '" + ((TextBox)contents.FindControl("username")).Text.Replace("'","").ToLower() + "'");
					if(rs.Read() && rs.GetInt32(0) == 0)
					{
						valid = true;
					}
					rs.Close();

					if(valid)
					{
						int userID = Db.createAccount(
                            ((TextBox)contents.FindControl("username")).Text.Replace("'",""),
                            ((TextBox)contents.FindControl("email")).Text.Replace("'",""),
                            ((TextBox)contents.FindControl("password")).Text,
                            Convert.ToInt32(HttpContext.Current.Session["SponsorID"]),
                            (HttpContext.Current.Session["DepartmentID"] != null ? HttpContext.Current.Session["DepartmentID"].ToString() : "NULL"),
                            0,
                            ((TextBox)contents.FindControl("aemail")).Text.Replace("'",""));
                        Db.saveBQ(contents);

                        if (Convert.ToInt32(HttpContext.Current.Session["SponsorInviteID"]) > 0)
                        {
                            Db.exec("UPDATE SponsorInvite SET " +
                                "UserID = " + userID + ", Consent = " + (HttpContext.Current.Session["SponsorInviteConsent"] != null ? "GETDATE()" : "NULL") + " " +
                                "WHERE SponsorInviteID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorInviteID"]));
                        }
                        HttpContext.Current.Session["SponsorInviteID"] = null;

                        Db.checkAndLogin(userID);
					}
					else
					{
                        errorMsg = "<SPAN STYLE=\"color:#CC0000;\">" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 21] + "</SPAN> " + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 22] + ".";
					}
				}
				else
				{
                    errorMsg = "<SPAN STYLE=\"color:#CC0000;\">" + (errorText != "" ? errorText : "" + lang[Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1, 23] + ".") + "</SPAN>";
				}
			}

            error.Controls.Clear();
            error.Controls.Add(new LiteralControl("<div style=\"clear:both;\">&nbsp;</div><div style=\"float:left;\">" + errorMsg + "</div>"));
		}
	}
}
