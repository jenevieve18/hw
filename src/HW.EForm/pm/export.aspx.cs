using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace eform
{
	/// <summary>
	/// Summary description for export.
	/// </summary>
	public class export : System.Web.UI.Page
	{
		protected DropDownList SurveyID;
		protected CheckBoxList ProjectRoundID;
		protected Button Execute;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				SqlDataReader rs = Db.sqlRecordSet("SELECT p.Internal, pr.Internal, pr.ProjectRoundID FROM ProjectRound pr INNER JOIN Project p ON pr.ProjectID = p.ProjectID ORDER BY p.Internal, pr.Internal");
				while(rs.Read())
				{
					ProjectRoundID.Items.Add(new ListItem(rs.GetString(0) + ", " + rs.GetString(1), rs.GetInt32(2).ToString()));
				}
				rs.Close();

				rs = Db.sqlRecordSet("SELECT s.Internal, s.SurveyID FROM Survey s ORDER BY s.Internal");
				while(rs.Read())
				{
					SurveyID.Items.Add(new ListItem(rs.GetString(0),rs.GetInt32(1).ToString()));
				}
				rs.Close();
			}

			Execute.Click += new EventHandler(Execute_Click);
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

		private void Execute_Click(object sender, EventArgs e)
		{
			string s = "0";
			SqlDataReader rs = Db.sqlRecordSet("SELECT p.Internal, pr.Internal, pr.ProjectRoundID FROM ProjectRound pr INNER JOIN Project p ON pr.ProjectID = p.ProjectID ORDER BY p.Internal, pr.Internal");
			while(rs.Read())
			{
				if(ProjectRoundID.Items.FindByValue(rs.GetInt32(2).ToString()).Selected)
				{
					s += "," + rs.GetInt32(2);
				}
			}
			rs.Close();

			Db.execExport(s,0,Convert.ToInt32(SurveyID.SelectedValue),1,0,0,0,0,0,0,false);
		}
	}
}
