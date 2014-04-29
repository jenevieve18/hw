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
	/// Summary description for feedbackSetup.
	/// </summary>
	public class feedbackSetup : System.Web.UI.Page
	{
		protected Button Save;
		protected DropDownList SurveyID;
		protected CheckBoxList QuestionID;

		private void Page_Load(object sender, System.EventArgs e)
		{
            OdbcDataReader rs;
			
			if(!IsPostBack)
			{
				rs = Db.recordSet("SELECT SurveyID, Internal FROM Survey");
				while(rs.Read())
				{
					SurveyID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();

				populateQuestions();
			}

			SurveyID.SelectedIndexChanged += new EventHandler(SurveyID_SelectedIndexChanged);
			Save.Click += new EventHandler(Save_Click);
		}

		private void populateQuestions()
		{
			int f = (HttpContext.Current.Request.QueryString["FeedbackID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FeedbackID"]) : 0);

			QuestionID.Items.Clear();

			OdbcDataReader rs = Db.recordSet("SELECT q.Internal, q.QuestionID, q.Variablename, qc.QuestionContainer, f.FeedbackQuestionID " +
				"FROM Question q " +
				"INNER JOIN SurveyQuestion sq ON q.QuestionID = sq.QuestionID " +
				"LEFT OUTER JOIN QuestionContainer qc ON q.QuestionContainerID = qc.QuestionContainerID " +
				"LEFT OUTER JOIN FeedbackQuestion f ON q.QuestionID = f.QuestionID AND f.FeedbackID = " + f + " " +
				"WHERE sq.SurveyID = " + Convert.ToInt32(SurveyID.SelectedValue) + " AND (SELECT COUNT(*) FROM QuestionOption qo WHERE qo.QuestionID = q.QuestionID) > 0 ORDER BY sq.SortOrder");
			while(rs.Read())
			{
				QuestionID.Items.Add(new ListItem(rs.GetInt32(1) + " / " + (!rs.IsDBNull(3) ? rs.GetString(3) + "/" : "") + (!rs.IsDBNull(2) && rs.GetString(2) != "" ? "[" + rs.GetString(2) + "] " : "") + rs.GetString(0),rs.GetInt32(1).ToString()));

				if(!rs.IsDBNull(4))
				{
					QuestionID.Items.FindByValue(rs.GetInt32(1).ToString()).Selected = true;
				}
			}
			rs.Close();
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

		private void SurveyID_SelectedIndexChanged(object sender, EventArgs e)
		{
			populateQuestions();
		}

		private void Save_Click(object sender, EventArgs e)
		{
			int f = (HttpContext.Current.Request.QueryString["FeedbackID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FeedbackID"]) : 0);

			if(f != 0)
			{
				OdbcDataReader rs = Db.recordSet("SELECT DISTINCT q.QuestionID, f.FeedbackQuestionID " +
					"FROM Question q " +
					"INNER JOIN SurveyQuestion sq ON q.QuestionID = sq.QuestionID " +
					"INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
					"LEFT OUTER JOIN FeedbackQuestion f ON q.QuestionID = f.QuestionID AND f.FeedbackID = " + f + " " +
					"WHERE sq.SurveyID = " + Convert.ToInt32(SurveyID.SelectedValue));
				while(rs.Read())
				{
					if(QuestionID.Items.FindByValue(rs.GetInt32(0).ToString()).Selected)
					{
						if(rs.IsDBNull(1))
						{
							Db.execute("INSERT INTO FeedbackQuestion (FeedbackID,QuestionID) VALUES (" + f + "," + rs.GetInt32(0) + ")");
						}
					}
					else
					{
						if(!rs.IsDBNull(1))
						{
							Db.execute("UPDATE FeedbackQuestion SET FeedbackID = -ABS(FeedbackID) WHERE FeedbackQuestionID = " + rs.GetInt32(1));
						}
					}
				}
				rs.Close();
			}
		}
	}
}
