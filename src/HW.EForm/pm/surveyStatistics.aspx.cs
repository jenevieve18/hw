using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace eform
{
	/// <summary>
	/// Summary description for surveyStatistics.
	/// </summary>
	public class surveyStatistics : System.Web.UI.Page
	{
		protected Label Res;

		private string components(int QID, int OID, int projectID, int langID, int year, int total, bool text)
		{
			string res = "";

			string sql = "SELECT ocs.ExportValue, LTRIM(RTRIM(CAST(ocl.Text AS CHAR(8000)))), COUNT(av.AnswerValue),ocs.SortOrder " +
				"FROM OptionComponents ocs " +
				"INNER JOIN OptionComponentLang ocl ON ocs.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + langID + " " +
				"LEFT OUTER JOIN (AnswerValue av " +
				"INNER JOIN Answer a ON a.EndDT IS NOT NULL AND av.AnswerID = a.AnswerID AND YEAR(a.EndDT) = " + year + " " +
				"INNER JOIN ProjectRoundUser pru ON a.ProjectRoundUserID = pru.ProjectRoundUserID " +
				"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID AND pr.ProjectID = " + projectID + " " +
				") ON av.QuestionID = " + QID + " AND ocs.OptionID = av.OptionID AND ocs.OptionComponentID = av.ValueInt " +
				"WHERE ocs.OptionID = " + OID + " " +
				"GROUP BY ocs.ExportValue, LTRIM(RTRIM(CAST(ocl.Text AS CHAR(8000)))),ocs.SortOrder";
			//HttpContext.Current.Response.Write(sql);
			//HttpContext.Current.Response.End();
			SqlDataReader rs = Db.sqlRecordSet(sql);
			while(rs.Read())
			{
				res += "<br/>" + (text ? "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + rs.GetInt32(0) + "=" + Db.RemoveHTMLTags(rs.GetString(1)) : (total == 0 ? "0" : Math.Round((double)rs.GetInt32(2)/(double)total*100).ToString() + "%") + "");
			}
			rs.Close();

			return res;
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			int surveyID = 95, projectID = 13, langID = 1;

			string questionIDOptionID = ""; bool first = true;
			SortedList al = new SortedList(); int cx = 0;
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			Res.Text = "<TABLE BORDER=\"1\"><TR>" +
				//"<TD>ID</TD>" +
				"<TD>Question</TD>";

			string sql = "SELECT " +
				"sq.QuestionID, " +							// 0
				"qo.OptionID, " +							// 1
				"LTRIM(RTRIM(CAST(ISNULL(sqlang.Question,ql.Question) AS CHAR(8000)))), " +	// 2
				"sq.SortOrder, " +							// 3
				"sqo.SortOrder, " +							// 4
				"YEAR(a.EndDT), " +							// 5
				"AVG(CAST(ocs.ExportValue AS DECIMAL)), " +	// 6
				"COUNT(*) " +								// 7
				"FROM SurveyQuestion sq " +
				"INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
				"INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " +
				"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
				"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
				"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + langID + " " +
				"INNER JOIN AnswerValue av ON av.QuestionID = sq.QuestionID AND av.OptionID = qo.OptionID AND av.DeletedSessionID IS NULL " +
				"INNER JOIN OptionComponents ocs ON ocs.OptionID = o.OptionID AND av.ValueInt = ocs.OptionComponentID " +
				"INNER JOIN Answer a ON av.AnswerID = a.AnswerID " +
				"INNER JOIN ProjectRoundUser pru ON a.ProjectRoundUserID = pru.ProjectRoundUserID " +
				"INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
				"LEFT OUTER JOIN SurveyQuestionLang sqlang ON sq.SurveyQuestionID = sqlang.SurveyQuestionID AND sqlang.LangID = " + langID + " " +
				"WHERE o.OptionType = 1 AND pr.ProjectID = " + projectID + " AND sq.SurveyID = " + surveyID + " AND a.EndDT IS NOT NULL " +
				"GROUP BY sq.QuestionID, qo.OptionID, LTRIM(RTRIM(CAST(ISNULL(sqlang.Question,ql.Question) AS CHAR(8000)))), sq.SortOrder, sqo.SortOrder, YEAR(a.EndDT) " +
				"ORDER BY sq.SortOrder, sqo.SortOrder, YEAR(a.EndDT)";
			//HttpContext.Current.Response.Write(sql);
			SqlDataReader rs = Db.sqlRecordSet(sql);
			while(rs.Read())
			{
				if(questionIDOptionID != rs.GetInt32(0).ToString() + ":" + rs.GetInt32(1).ToString())
				{
					if(questionIDOptionID != "" && first)
					{
						first = false;
					}
					questionIDOptionID = rs.GetInt32(0).ToString() + ":" + rs.GetInt32(1).ToString();
					sb.Append("</TR>\r\n<TR>" +
						//"<TD>" + questionIDOptionID + "</TD>" +
						"<TD>" + rs.GetString(2) + components(rs.GetInt32(0),rs.GetInt32(1),projectID,langID,0,0,true) + "</TD>");
					cx = 0;
				}
				if(first) { al.Add(cx,rs.GetInt32(5)); }

				if(al.ContainsValue(rs.GetInt32(5)))
				{
					if(first) { Res.Text += "<TD>" + rs.GetInt32(5) + "</TD>"; }
					while(al.ContainsKey(cx) && Convert.ToInt32(al[cx]) != rs.GetInt32(5))
					{
						sb.Append("<TD></TD>");
						cx++;
					}
					if(Convert.ToInt32(al[cx]) == rs.GetInt32(5))
					{
						sb.Append("<TD VALIGN=\"BOTTOM\"><nobr>avg=" + Math.Round(Convert.ToDouble(rs.GetValue(6)),2) + " (n=" + rs.GetInt32(7) + ")" + components(rs.GetInt32(0),rs.GetInt32(1),projectID,langID,rs.GetInt32(5),rs.GetInt32(7),false) + "</nobr></TD>");
					}
				}
				cx++;
			}
			rs.Close();

			Res.Text += sb.ToString() + "</TR></TABLE>";
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
	}
}
