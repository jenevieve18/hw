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
	public class managerSetup : System.Web.UI.Page
	{
		protected TextBox Name;
		protected TextBox Email;
		protected TextBox Password;
		protected TextBox Phone;
		protected PlaceHolder ProjectRound;
		protected DropDownList ProjectRoundID;
		protected CheckBox ShowCompleteOrg;
		protected CheckBoxList ProjectRoundUnitID;
		protected Button Save;

		int managerID = 0;
		int projectRoundIDx = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
			managerID = (HttpContext.Current.Request.QueryString["ManagerID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ManagerID"].ToString()) : 0);
			projectRoundIDx = (HttpContext.Current.Request.QueryString["ProjectRoundID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ProjectRoundID"].ToString()) : 0);

			Save.Click += new EventHandler(Save_Click);
			ProjectRoundID.SelectedIndexChanged += new EventHandler(ProjectRoundID_SelectedIndexChanged);

			OdbcDataReader rs;

			if(!IsPostBack)
			{
				if(managerID != 0)
				{
					rs = Db.recordSet("SELECT m.Name, m.Phone, m.Email, m.Password FROM Manager m WHERE m.ManagerID = " + managerID);
					if(rs.Read())
					{
						Name.Text = rs.GetString(0);
						Phone.Text = rs.GetString(1);
						Email.Text = rs.GetString(2);
						Password.Text = rs.GetString(3);
					}
					rs.Close();

					if(projectRoundIDx == 0)
					{
						rs = Db.recordSet("SELECT TOP 1 ProjectRoundID, ShowAllUnits FROM ManagerProjectRound WHERE ManagerID = " + managerID);
						if(rs.Read())
						{
							projectRoundIDx = rs.GetInt32(0);
							ProjectRoundID.SelectedValue = projectRoundIDx.ToString();
							ShowCompleteOrg.Checked = !rs.IsDBNull(1);
						}
						rs.Close();
					}
					else
					{
						rs = Db.recordSet("SELECT ProjectRoundID, ShowAllUnits FROM ManagerProjectRound WHERE ProjectRoundID = " + projectRoundIDx + " AND ManagerID = " + managerID);
						if(rs.Read())
						{
							projectRoundIDx = rs.GetInt32(0);
							ProjectRoundID.SelectedValue = projectRoundIDx.ToString();
							ShowCompleteOrg.Checked = !rs.IsDBNull(1);
						}
						rs.Close();
					}
				}

				int cx = 0;
				rs = Db.recordSet("SELECT pr.ProjectRoundID, p.Internal, pr.Internal FROM Project p INNER JOIN ProjectRound pr ON p.ProjectID = pr.ProjectID ORDER BY p.Internal, pr.Internal");
				while(rs.Read())
				{
					ProjectRoundID.Items.Add(new ListItem(rs.GetString(1) + " » " + rs.GetString(2), rs.GetInt32(0).ToString()));
					if(projectRoundIDx == 0 && cx++ == 0)
					{
						projectRoundIDx = rs.GetInt32(0);
					}
				}
				rs.Close();
				if(projectRoundIDx != 0)
				{
					ProjectRoundID.SelectedValue = projectRoundIDx.ToString();
				}

				populateRound();
			}

			if(managerID != 0)
			{
				ProjectRound.Visible = true;
			}
		}
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);
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

		private void Save_Click(object sender, EventArgs e)
		{
			save();
		}

		private void save()
		{
			if(managerID == 0)
			{
				OdbcDataReader rs = Db.recordSet("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO Manager (Name,Email,Password,Phone) VALUES ('" + Name.Text.Replace("'","''") + "','" + Email.Text.Replace("'","''") + "','" + Password.Text.Replace("'","''") + "','" + Phone.Text.Replace("'","''") + "');SELECT ManagerID FROM [Manager] ORDER BY ManagerID DESC;COMMIT;");
				if(rs.Read())
				{
					managerID = rs.GetInt32(0);
				}
				rs.Close();
				HttpContext.Current.Response.Redirect("managerSetup.aspx?ManagerID=" + managerID,true);
			}
			else
			{
				Db.execute("UPDATE Manager SET Name = '" + Name.Text.Replace("'","''") + "', Email = '" + Email.Text.Replace("'","''") + "', Password = '" + Password.Text.Replace("'","''") + "', Phone = '" + Phone.Text.Replace("'","''") + "' WHERE ManagerID = " + managerID);
				
				bool hasUnits = false;
				
				Db.execute("DELETE FROM ManagerProjectRoundUnit WHERE ManagerID = " + managerID + " AND ProjectRoundID = " + projectRoundIDx);
				OdbcDataReader rs = Db.recordSet("SELECT ProjectRoundUnitID FROM ProjectRoundUnit WHERE ProjectRoundID = " + projectRoundIDx);
				while(rs.Read())
				{
					if(ProjectRoundUnitID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected)
					{
						hasUnits = true;
						Db.execute("INSERT INTO ManagerProjectRoundUnit (ManagerID, ProjectRoundID, ProjectRoundUnitID) VALUES (" + managerID + "," + projectRoundIDx + "," + rs.GetInt32(0) + ")");
					}
				}
				rs.Close();
				if(hasUnits)
				{
					rs = Db.recordSet("SELECT ManagerProjectRoundID FROM ManagerProjectRound WHERE ManagerID = " + managerID + " AND ProjectRoundID = " + projectRoundIDx);
					if(rs.Read())
					{
						Db.execute("UPDATE ManagerProjectRound SET ShowAllUnits = " + (ShowCompleteOrg.Checked ? "1" : "NULL") + " WHERE ManagerProjectRoundID = " + rs.GetInt32(0));
					}
					else
					{
						Db.execute("INSERT INTO ManagerProjectRound (ManagerID,ProjectRoundID,ShowAllUnits) VALUES (" + managerID + "," + projectRoundIDx + "," + (ShowCompleteOrg.Checked ? "1" : "NULL") + ")");
					}
					rs.Close();
				}
				else
				{
					Db.execute("DELETE FROM ManagerProjectRound WHERE ManagerID = " + managerID + " AND ProjectRoundID = " + projectRoundIDx);
				}
				HttpContext.Current.Response.Redirect("managerSetup.aspx?ManagerID=" + managerID + "&ProjectRoundID=" +  Convert.ToInt32(ProjectRoundID.SelectedValue), true);
			}
		}

		private void populateRound()
		{
			ProjectRoundUnitID.Items.Clear();

			OdbcDataReader rs = Db.recordSet("SELECT ProjectRoundUnitID, dbo.cf_projectUnitTree(ProjectRoundUnitID,' » ') FROM ProjectRoundUnit WHERE ProjectRoundID = " + projectRoundIDx + " ORDER BY SortString");
			while(rs.Read())
			{
				ProjectRoundUnitID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
			}
			rs.Close();

			rs = Db.recordSet("SELECT ProjectRoundUnitID FROM ManagerProjectRoundUnit WHERE ManagerID = " + managerID + " AND ProjectRoundID = " + projectRoundIDx);
			while(rs.Read())
			{
				if(ProjectRoundUnitID.Items.FindByValue(rs.GetInt32(0).ToString()) != null)
				{
					ProjectRoundUnitID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected = true;
				}
			}
			rs.Close();
		}

		private void ProjectRoundID_SelectedIndexChanged(object sender, EventArgs e)
		{
			save();
		}
	}

}
