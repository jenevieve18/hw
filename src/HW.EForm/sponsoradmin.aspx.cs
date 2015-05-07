using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Odbc;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eform
{
	/// <summary>
	/// Summary description for sponsoradmin.
	/// </summary>
	public class sponsoradmin : System.Web.UI.Page
	{
		protected PlaceHolder LoginBox;
		protected TextBox Username;
		protected TextBox Password;
		protected Button Login;
		protected Button Logout;
		protected Label SearchResults;
		protected PlaceHolder LoggedIn;

		protected TextBox UserUsername;
		protected TextBox UserPassword;
		protected TextBox UserFullname;
		protected TextBox UserEmail;
		protected DropDownList UserSponsorID;
		protected Button CancelUser;
		protected Button DeleteUser;
		protected Button SaveUser;
		protected Button AddUser;
		protected PlaceHolder EditUser;

		protected TextBox Sponsor;
		protected PlaceHolder EditProject;
		protected DropDownList ProjectID;
		protected Button CancelProject;
		protected Button SaveProject;
		protected Button AddProject;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(HttpContext.Current.Session["SponsorSuperAdminID"] == null)
			{
				LoginBox.Visible = true;
				LoggedIn.Visible = false;

				Login.Click += new EventHandler(Login_Click);
			}
			else
			{
				Logout.Click += new EventHandler(Logout_Click);
				AddUser.Click += new EventHandler(AddUser_Click);
				CancelUser.Click += new EventHandler(CancelUser_Click);
				SaveUser.Click += new EventHandler(SaveUser_Click);
				DeleteUser.Click += new EventHandler(DeleteUser_Click);

				CancelProject.Click += new EventHandler(CancelProject_Click);
				SaveProject.Click += new EventHandler(SaveProject_Click);
				AddProject.Click += new EventHandler(AddProject_Click);

				LoginBox.Visible = false;
				LoggedIn.Visible = true;

				if(HttpContext.Current.Request.QueryString["SponsorAdminID"] != null && !IsPostBack)
				{
					EditUser.Visible = true;
					DeleteUser.Visible = true;
					OdbcDataReader rs = Db.recordSet("SELECT " +
						"a.Username, " +
						"a.Name, " +
						"a.Email, " +
						"a.SponsorID " +
						"FROM SponsorAdmin a " +
						"INNER JOIN SponsorSuperAdminSponsor ssas ON a.SponsorID = ssas.SponsorID " +
						"WHERE ssas.SponsorSuperAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorSuperAdminID"]) + " AND a.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SponsorAdminID"]));
					if(rs.Read())
					{
						UserUsername.Text = rs.GetString(0);
						UserFullname.Text = rs.GetString(1);
						UserEmail.Text = rs.GetString(2);
						UserSponsorID.SelectedValue = rs.GetInt32(3).ToString();
					}
					rs.Close();
					UserSponsorID.Enabled = false;
				}
				if(HttpContext.Current.Request.QueryString["SponsorID"] != null && !IsPostBack)
				{
					EditProject.Visible = true;
					OdbcDataReader rs = Db.recordSet("SELECT " +
						"a.Sponsor, " +
						"a.ProjectID " +
						"FROM Sponsor a " +
						"INNER JOIN SponsorSuperAdminSponsor ssas ON a.SponsorID = ssas.SponsorID " +
						"WHERE ssas.SponsorSuperAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorSuperAdminID"]) + " AND a.SponsorID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SponsorID"]));
					if(rs.Read())
					{
						Sponsor.Text = rs.GetString(0);
						ProjectID.SelectedValue = rs.GetInt32(1).ToString();
					}
					rs.Close();
					ProjectID.Enabled = false;
				}
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			if(HttpContext.Current.Session["SponsorSuperAdminID"] != null)
			{
				SearchResults.Text = "<h2>Projekt/administratörer</h2><ul>";

				OdbcDataReader rs = Db.recordSet("SELECT " +
					"s.SponsorID, " +
					"s.ProjectID, " +
					"s.Sponsor, " +
					"p.Internal, " +
					"(SELECT COUNT(*) FROM [User] u WHERE u.SponsorID = s.SponsorID) AS CX " +
					"FROM Sponsor s " +
					"INNER JOIN SponsorSuperAdminSponsor ssa ON s.SponsorID = ssa.SponsorID " +
					"INNER JOIN Project p ON s.ProjectID = p.ProjectID " +
					"WHERE ssa.SponsorSuperAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorSuperAdminID"]) + " " +
					"");
				while(rs.Read())
				{
					if(!IsPostBack)
					{
						UserSponsorID.Items.Add(new ListItem(rs.GetString(2),rs.GetInt32(0).ToString()));
					}

					SearchResults.Text += "<li><A HREF=\"sponsoradmin.aspx?SponsorID=" + rs.GetInt32(0) + "\">" + (rs.GetString(2) == "" ? "?" : rs.GetString(2)) + "</A> [Template: " + rs.GetString(3) + ", Subject count: " + rs.GetInt32(4) + "]<ul>";

					OdbcDataReader rs2 = Db.recordSet("SELECT " +
						"s.SponsorAdminID, " +
						"s.Name " +
						"FROM SponsorAdmin s " +
						"WHERE s.SponsorID = " + rs.GetInt32(0) + " " +
						"");
					while(rs2.Read())
					{
						SearchResults.Text += "<li><A HREF=\"sponsoradmin.aspx?SponsorAdminID=" + rs2.GetInt32(0) + "\">" + (rs2.GetString(1) == "" ? "?" : rs2.GetString(1)) + "</A></li>";
					}
					rs2.Close();

					SearchResults.Text += "</ul></li>";
				}
				rs.Close();

				SearchResults.Text += "</ul><hr/><h2>Projektmallar</h2><ul>";

				rs = Db.recordSet("SELECT DISTINCT " +
					"p.ProjectID, " +
					"p.Internal " +
					"FROM Sponsor s " +
					"INNER JOIN SponsorSuperAdminSponsor ssa ON s.SponsorID = ssa.SponsorID " +
					"INNER JOIN Project p ON s.ProjectID = p.ProjectID " +
					"WHERE ssa.SponsorSuperAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorSuperAdminID"]) + " " +
					"");
				while(rs.Read())
				{
					if(!IsPostBack)
					{
						ProjectID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
					}

					SearchResults.Text += "<li>" + (rs.GetString(1) == "" ? "?" : rs.GetString(1)) + "<ul>";

					OdbcDataReader rs2 = Db.recordSet("SELECT " +
						"p.ProjectRoundID, " +
						"p.Internal, " +
						"u.ProjectRoundUnitID, " +
						"u.UnitKey " +
						"FROM ProjectRound p " +
						"INNER JOIN ProjectRoundUnit u ON p.ProjectRoundID = u.ProjectRoundID " +
						"WHERE p.ProjectID = " + rs.GetInt32(0) + " " +
						"");
					while(rs2.Read())
					{
						SearchResults.Text += "<li><A HREF=\"JavaScript:void(window.open('submit.aspx?K=U" + rs2.GetGuid(3).ToString().Substring(0,8) + rs2.GetInt32(2).ToString() + "','',''));\">" + (rs2.GetString(1) == "" ? "?" : rs2.GetString(1)) + "</A></li>";
					}
					rs2.Close();

					SearchResults.Text += "</ul></li>";
				}
				rs.Close();

				SearchResults.Text += "</ul>";
			}
		}

		private void Login_Click(object sender, EventArgs e)
		{
			OdbcDataReader rs = Db.recordSet("SELECT " +
				"u.SponsorSuperAdminID " +			// 0
				"FROM SponsorSuperAdmin u " +
				"WHERE u.Username = '" + Username.Text.Trim().Replace("'","") + "' AND u.Password = '" + Db.HexHashMD5(Password.Text) + "'");
			if(rs.Read())
			{
				HttpContext.Current.Session["SponsorSuperAdminID"] = rs.GetInt32(0);
			}
			rs.Close();
			HttpContext.Current.Response.Redirect("sponsoradmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		private void Logout_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Session["SponsorSuperAdminID"] = null;
			HttpContext.Current.Response.Redirect("sponsoradmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}

		private void AddUser_Click(object sender, EventArgs e)
		{
			EditUser.Visible = true;
			UserUsername.Text = "";
			UserFullname.Text = "";
			UserEmail.Text = "";
			UserPassword.Text = "";
		}

		private void CancelUser_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Response.Redirect("sponsoradmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}

		private void SaveUser_Click(object sender, EventArgs e)
		{
			if(HttpContext.Current.Request.QueryString["SponsorAdminID"] != null)
			{
				Db.execute("UPDATE SponsorAdmin SET " +
					"Username = '" + UserUsername.Text.Replace("'","") + "', " +
					(UserPassword.Text != "" ? "Password = '" + Db.HexHashMD5(UserPassword.Text) + "', " : "") +
					"Name = '" + UserFullname.Text.Replace("'","") + "', " +
					"Email = '" + UserEmail.Text.Replace("'","") + "' " +
					"WHERE SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SponsorAdminID"]));
			}
			else
			{
				Db.execute("INSERT INTO SponsorAdmin (Username,Password,Name,Email,SponsorID) VALUES (" +
					"'" + UserUsername.Text.Replace("'","") + "', " +
					"'" + Db.HexHashMD5(UserPassword.Text) + "', " +
					"'" + UserFullname.Text.Replace("'","") + "', " +
					"'" + UserEmail.Text.Replace("'","") + "', " +
					Convert.ToInt32(UserSponsorID.SelectedValue) + " " +
					")");
			}

			HttpContext.Current.Response.Redirect("sponsoradmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}

		private void DeleteUser_Click(object sender, EventArgs e)
		{
			Db.execute("UPDATE SponsorAdmin SET SponsorID = -SponsorID WHERE SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SponsorAdminID"]));
			HttpContext.Current.Response.Redirect("sponsoradmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}

		private void CancelProject_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Response.Redirect("sponsoradmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}

		private void SaveProject_Click(object sender, EventArgs e)
		{
			if(HttpContext.Current.Request.QueryString["SponsorID"] != null)
			{
				Db.execute("UPDATE Sponsor SET " +
					"Sponsor = '" + Sponsor.Text.Replace("'","") + "' " +
					"WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SponsorID"]));
			}
			else
			{
				Db.execute("INSERT INTO Sponsor " +
					"(" +
					"[Sponsor]" +
					",[ProjectID]" +
					",[UserIdent1]" +
					",[UserIdent2]" +
					",[UserIdent3]" +
					",[UserCheck1]" +
					",[UserCheck2]" +
					",[UserCheck3]" +
					",[UserIdent4]" +
					",[UserIdent5]" +
					",[UserIdent6]" +
					",[UserIdent7]" +
					",[UserIdent8]" +
					",[UserIdent9]" +
					",[UserIdent10]" +
					",[ShowUserIdent1]" +
					",[ShowUserIdent2]" +
					",[ShowUserIdent3]" +
					",[ShowUserIdent4]" +
					",[ShowUserIdent5]" +
					",[ShowUserIdent6]" +
					",[ShowUserIdent7]" +
					",[ShowUserIdent8]" +
					",[ShowUserIdent9]" +
					",[ShowUserIdent10]" +
					",[ShowUserNr]" +
					",[UserNr]" +
					",[EmailIdent]" +
					",[FirstnameIdent]" +
					",[LastnameIdent]" +
					",[ShowUserCheck1]" +
					",[ShowUserCheck2]" +
					",[ShowUserCheck3]" +
					",[NoEmail]" +
					",[NoLogout]" +
					",[CustomReportTemplateID]" +
					",[FeedbackEmailFrom]" +
					",[FeedbackEmailSubject]" +
					",[FeedbackEmailBody]" +
					") SELECT " +
					"'" + Sponsor.Text.Replace("'","") + "'" +
					"," + Convert.ToInt32(ProjectID.SelectedValue) + " " +
					",s.[UserIdent1]" +
					",s.[UserIdent2]" +
					",s.[UserIdent3]" +
					",s.[UserCheck1]" +
					",s.[UserCheck2]" +
					",s.[UserCheck3]" +
					",s.[UserIdent4]" +
					",s.[UserIdent5]" +
					",s.[UserIdent6]" +
					",s.[UserIdent7]" +
					",s.[UserIdent8]" +
					",s.[UserIdent9]" +
					",s.[UserIdent10]" +
					",s.[ShowUserIdent1]" +
					",s.[ShowUserIdent2]" +
					",s.[ShowUserIdent3]" +
					",s.[ShowUserIdent4]" +
					",s.[ShowUserIdent5]" +
					",s.[ShowUserIdent6]" +
					",s.[ShowUserIdent7]" +
					",s.[ShowUserIdent8]" +
					",s.[ShowUserIdent9]" +
					",s.[ShowUserIdent10]" +
					",s.[ShowUserNr]" +
					",s.[UserNr]" +
					",s.[EmailIdent]" +
					",s.[FirstnameIdent]" +
					",s.[LastnameIdent]" +
					",s.[ShowUserCheck1]" +
					",s.[ShowUserCheck2]" +
					",s.[ShowUserCheck3]" +
					",s.[NoEmail]" +
					",s.[NoLogout]" +
					",s.[CustomReportTemplateID]" +
					",s.[FeedbackEmailFrom]" +
					",s.[FeedbackEmailSubject]" +
					",s.[FeedbackEmailBody] " +
					"FROM Sponsor s " +
					"INNER JOIN SponsorSuperAdmin ssa ON s.SponsorID = ssa.DefaultSponsorID " +
					"WHERE ssa.SponsorSuperAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorSuperAdminID"]));

				OdbcDataReader rs = Db.recordSet("SELECT " +
					"u.SponsorID " +			// 0
					"FROM Sponsor u " +
					"ORDER BY u.SponsorID DESC");
				if(rs.Read())
				{
					OdbcDataReader rs2 = Db.recordSet("SELECT " +
						"s.SponsorID " +			// 0
						"FROM Sponsor s " +
						"INNER JOIN SponsorSuperAdminSponsor ssa ON s.SponsorID = ssa.SponsorID " +
						"WHERE s.ProjectID = " + Convert.ToInt32(ProjectID.SelectedValue) + " AND ssa.SponsorSuperAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorSuperAdminID"]) + " " +
						"");
					if(rs2.Read())
					{
						Db.execute("INSERT INTO SponsorPRU (PRUID,NoLogout,SponsorID,NoSend) SELECT PRUID,NoLogout," + rs.GetInt32(0) + ",NoSend FROM SponsorPRU WHERE SponsorID = " + rs2.GetInt32(0));
						Db.execute("INSERT INTO SponsorAutoPRU (PRUID,SponsorID,Note) SELECT PRUID," + rs.GetInt32(0) + ",Note FROM SponsorAutoPRU WHERE SponsorID = " + rs2.GetInt32(0));
					}
					rs2.Close();
					Db.execute("INSERT INTO SponsorSuperAdminSponsor (SponsorSuperAdminID,SponsorID) VALUES (" + Convert.ToInt32(HttpContext.Current.Session["SponsorSuperAdminID"]) + "," + rs.GetInt32(0) + ")");
				}
				rs.Close();
			}

			HttpContext.Current.Response.Redirect("sponsoradmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
		}

		private void AddProject_Click(object sender, EventArgs e)
		{
			EditProject.Visible = true;
			Sponsor.Text = "";
		}
	}
}
