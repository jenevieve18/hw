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
		protected DropDownList FeedbackTemplateID;
		protected PlaceHolder QuestionID;
		protected Label FeedbackID;
		protected TextBox Feedback;

		int f = 0;

		private void Page_Load(object sender, System.EventArgs e)
		{
            OdbcDataReader rs;
			
			f = (HttpContext.Current.Request.QueryString["FeedbackID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FeedbackID"]) : 0);

			if(!IsPostBack)
			{
				if(HttpContext.Current.Request.QueryString["CopyFeedbackID"] != null)
				{
					Db.execute("INSERT INTO Feedback (Feedback, SurveyID, FeedbackTemplateID, Compare, NoHardcodedIdxs) SELECT Feedback, SurveyID, FeedbackTemplateID, Compare, NoHardcodedIdxs FROM Feedback WHERE FeedbackID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["CopyFeedbackID"]));
					f = Db.getInt32("SELECT TOP 1 FeedbackID FROM Feedback ORDER BY FeedbackID DESC");
					Db.execute("INSERT INTO FeedbackQuestion (FeedbackID, QuestionID, Additional, FeedbackTemplatePageID, IdxID, HardcodedIdx) SELECT " + f + ", QuestionID, Additional, FeedbackTemplatePageID, IdxID, HardcodedIdx FROM FeedbackQuestion WHERE FeedbackID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["CopyFeedbackID"]));
				}

				rs = Db.recordSet("SELECT FeedbackTemplateID, FeedbackTemplate FROM FeedbackTemplate");
				while(rs.Read())
				{
					FeedbackTemplateID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();
				rs = Db.recordSet("SELECT SurveyID, Internal FROM Survey");
				while(rs.Read())
				{
					SurveyID.Items.Add(new ListItem(rs.GetInt32(0).ToString().PadLeft(3,'0') + " " + rs.GetString(1),rs.GetInt32(0).ToString()));
				}
				rs.Close();

				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				sb.Append("<table>");
				rs = Db.recordSet("SELECT FeedbackID, Feedback, SurveyID, FeedbackTemplateID FROM Feedback");
				while(rs.Read())
				{
					sb.Append("<tr class=\"hov\"><td>&bull; " +
						"<a href=\"feedbackSetup.aspx?FeedbackID=" + rs.GetInt32(0) + "\"" + (f == rs.GetInt32(0) ? " style=\"font-weight:bold;\"" : "") + ">" + 
						rs.GetString(1) + 
						"</a></td><td>[<a href=\"feedbackSetup.aspx?CopyFeedbackID=" + rs.GetInt32(0) + "\">copy</a>]</td></tr>");
					if(f == rs.GetInt32(0))
					{
						Feedback.Text = rs.GetString(1);
						if(!rs.IsDBNull(2))
						{
							SurveyID.SelectedValue =  rs.GetInt32(2).ToString();
						}
						FeedbackTemplateID.SelectedValue = rs.GetInt32(3).ToString();
					}
				}
				rs.Close();
				sb.Append("</table>");
				FeedbackID.Text += sb.ToString();

				populateQuestions(true);
			}
			else
			{
				populateQuestions(false);
			}

			SurveyID.SelectedIndexChanged += new EventHandler(SurveyID_SelectedIndexChanged);
			FeedbackTemplateID.SelectedIndexChanged += new EventHandler(FeedbackTemplateID_SelectedIndexChanged);
			Save.Click += new EventHandler(Save_Click);
		}

		private void populateQuestions(bool pop)
		{
			OdbcDataReader rs = Db.recordSet("SELECT q.Internal, q.QuestionID, q.Variablename, qc.QuestionContainer, f.FeedbackQuestionID, f.FeedbackTemplatePageID, ql.Question " +
				"FROM Question q " +
				"INNER JOIN SurveyQuestion sq ON q.QuestionID = sq.QuestionID " +
				"LEFT OUTER JOIN QuestionContainer qc ON q.QuestionContainerID = qc.QuestionContainerID " +
				"LEFT OUTER JOIN FeedbackQuestion f ON q.QuestionID = f.QuestionID AND f.FeedbackID = " + f + " " +
				"LEFT OUTER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = 1 " +
				"WHERE sq.SurveyID = " + Convert.ToInt32(SurveyID.SelectedValue) + " AND (SELECT COUNT(*) FROM QuestionOption qo WHERE qo.QuestionID = q.QuestionID) > 0 ORDER BY sq.SortOrder");
			while(rs.Read())
			{
				QuestionID.Controls.Add(new LiteralControl("<tr><td>"));

				CheckBox CB = new CheckBox();
				CB.EnableViewState = true;
				CB.ID = "Q" + rs.GetInt32(1);
				CB.Text = rs.GetInt32(1) + " / " + (!rs.IsDBNull(3) ? rs.GetString(3) + "/" : "") + (!rs.IsDBNull(2) && rs.GetString(2) != "" ? "[" + rs.GetString(2) + "] " : "") + rs.GetString(0);

				if(pop && !rs.IsDBNull(4))
				{
					CB.Checked = true;
				}

				QuestionID.Controls.Add(CB);
				QuestionID.Controls.Add(new LiteralControl("</td><td>"));
				DropDownList dl = new DropDownList();
				dl.EnableViewState = true;
				dl.ID = "QFTP" + rs.GetInt32(1);
				dl.Items.Add(new ListItem("< default >","0"));
				OdbcDataReader rs2 = Db.recordSet("SELECT FeedbackTemplatePageID, Description FROM FeedbackTemplatePage WHERE FeedbackTemplateID = " + Convert.ToInt32(FeedbackTemplateID.SelectedValue));
				while(rs2.Read())
				{
					dl.Items.Add(new ListItem(rs2.GetString(1),rs2.GetInt32(0).ToString()));
				}
				rs2.Close();
				
				if(pop && !rs.IsDBNull(5))
				{
					dl.Items.FindByValue(rs.GetInt32(5).ToString()).Selected = true;
				}

				QuestionID.Controls.Add(dl);

				QuestionID.Controls.Add(new LiteralControl("<span title=\"" + (!rs.IsDBNull(6) ? rs.GetString(6).Replace("\"","") : "") + "\">?</span></td></tr>"));
			}
			rs.Close();

			QuestionID.Controls.Add(new LiteralControl("<tr><td colspan=\"2\"><hr/></td></tr>"));

			rs = Db.recordSet("SELECT i.Internal, i.IdxID, NULL, NULL, f.FeedbackQuestionID, f.FeedbackTemplatePageID, NULL " +
				"FROM Idx i " +
				"LEFT OUTER JOIN FeedbackQuestion f ON i.IdxID = f.IdxID AND f.FeedbackID = " + f);
			while(rs.Read())
			{
				QuestionID.Controls.Add(new LiteralControl("<tr><td>"));

				CheckBox CB = new CheckBox();
				CB.EnableViewState = true;
				CB.ID = "I" + rs.GetInt32(1);
				CB.Text = "INDEX: " + rs.GetInt32(1) + " / " + rs.GetString(0);

				if(pop && !rs.IsDBNull(4))
				{
					CB.Checked = true;
				}

				QuestionID.Controls.Add(CB);
				QuestionID.Controls.Add(new LiteralControl("</td><td>"));
				DropDownList dl = new DropDownList();
				dl.EnableViewState = true;
				dl.ID = "IFTP" + rs.GetInt32(1);
				dl.Items.Add(new ListItem("< default >","0"));
				OdbcDataReader rs2 = Db.recordSet("SELECT FeedbackTemplatePageID, Description FROM FeedbackTemplatePage WHERE FeedbackTemplateID = " + Convert.ToInt32(FeedbackTemplateID.SelectedValue));
				while(rs2.Read())
				{
					dl.Items.Add(new ListItem(rs2.GetString(1),rs2.GetInt32(0).ToString()));
				}
				rs2.Close();
				
				if(pop && !rs.IsDBNull(5))
				{
					dl.Items.FindByValue(rs.GetInt32(5).ToString()).Selected = true;
				}

				QuestionID.Controls.Add(dl);

				QuestionID.Controls.Add(new LiteralControl("</td></tr>"));
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
			//populateQuestions(false);
		}

		private void Save_Click(object sender, EventArgs e)
		{
			int f = (HttpContext.Current.Request.QueryString["FeedbackID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FeedbackID"]) : 0);

			if(f != 0)
			{
				Db.execute("UPDATE Feedback SET Feedback = '" + Feedback.Text.Replace("'","") + "', FeedbackTemplateID = " + Convert.ToInt32(FeedbackTemplateID.SelectedValue) + ", SurveyID = " + Convert.ToInt32(SurveyID.SelectedValue) + " WHERE FeedbackID = " + f);

				OdbcDataReader rs = Db.recordSet("SELECT DISTINCT q.QuestionID, f.FeedbackQuestionID " +
					"FROM Question q " +
					"INNER JOIN SurveyQuestion sq ON q.QuestionID = sq.QuestionID " +
					"INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
					"LEFT OUTER JOIN FeedbackQuestion f ON q.QuestionID = f.QuestionID AND f.FeedbackID = " + f + " " +
					"WHERE sq.SurveyID = " + Convert.ToInt32(SurveyID.SelectedValue));
				while(rs.Read())
				{
					if(((CheckBox)QuestionID.FindControl("Q" + rs.GetInt32(0))).Checked)
					{
						string s = ((DropDownList)QuestionID.FindControl("QFTP" + rs.GetInt32(0))).SelectedValue;
						if(rs.IsDBNull(1))
						{
							Db.execute("INSERT INTO FeedbackQuestion (FeedbackID,QuestionID,FeedbackTemplatePageID) VALUES (" + f + "," + rs.GetInt32(0) + "," + (s == "0" ? "NULL" : s) + ")");
						}
						else
						{
							Db.execute("UPDATE FeedbackQuestion SET FeedbackTemplatePageID = " + (s == "0" ? "NULL" : s) + " WHERE FeedbackID = " + f + " AND QuestionID = " + rs.GetInt32(0));
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

				rs = Db.recordSet("SELECT DISTINCT i.IdxID, f.FeedbackQuestionID " +
					"FROM Idx i " +
					"LEFT OUTER JOIN FeedbackQuestion f ON i.IdxID = f.IdxID AND f.FeedbackID = " + f);
				while(rs.Read())
				{
					if(((CheckBox)QuestionID.FindControl("I" + rs.GetInt32(0))).Checked)
					{
						string s = ((DropDownList)QuestionID.FindControl("IFTP" + rs.GetInt32(0))).SelectedValue;
						if(rs.IsDBNull(1))
						{
							Db.execute("INSERT INTO FeedbackQuestion (FeedbackID,QuestionID,FeedbackTemplatePageID,IdxID) VALUES (" + f + ",-" + rs.GetInt32(0) + "," + (s == "0" ? "NULL" : s) + "," + rs.GetInt32(0) + ")");
						}
						else
						{
							Db.execute("UPDATE FeedbackQuestion SET FeedbackTemplatePageID = " + (s == "0" ? "NULL" : s) + " WHERE FeedbackID = " + f + " AND QuestionID = " + rs.GetInt32(0));
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

		private void FeedbackTemplateID_SelectedIndexChanged(object sender, EventArgs e)
		{
			//populateQuestions(false);
		}
	}
}
