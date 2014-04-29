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
	/// Summary description for findrisk.
	/// </summary>
	public class findrisk : System.Web.UI.Page
	{
		protected Label res;

		private void Page_Load(object sender, System.EventArgs e)
		{
			OdbcDataReader rs = Db.recordSet("" +
				"SELECT " +
				"s.ProjectRoundUserID, " +
				"ISNULL(av1.ValueInt,0), " +
				"ISNULL(av2.ValueDecimal,0), " +
				"ISNULL(av3.ValueDecimal,0), " +
				"ISNULL(av4.ValueDecimal,0), " +
				"ISNULL(av5.ValueInt,0), " +
				"ISNULL(av6.ValueDecimal,0), " +
				"ISNULL(av7.ValueInt,0), " +
				"ISNULL(av8.ValueInt,0), " +
				"ISNULL(av9.ValueInt,0), " +
				"ISNULL(av10.ValueInt,0), " +
				"ISNULL(av11.ValueInt,0), " +
				"ISNULL(av12.ValueInt,0), " +
				"ISNULL(av13.ValueInt,0), " +
				"a.AnswerID, " +
				"a.EndDT, " +
				"u.Terminated, " +
				"s.Terminated " +
				"FROM ProjectRoundUser s " +
				"INNER JOIN ProjectRound p ON p.ProjectRoundID = s.ProjectRoundID " +
				"INNER JOIN ProjectRoundUnit u ON s.ProjectRoundUnitID = u.ProjectRoundUnitID " +
				"LEFT OUTER JOIN Answer a ON a.ProjectRoundUserID = s.ProjectRoundUserID " +
				"LEFT OUTER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.DeletedSessionID IS NULL AND av1.ValueInt IS NOT NULL AND av1.QuestionID = 354 AND av1.OptionID = 104 " +		// diabetes
				"LEFT OUTER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.DeletedSessionID IS NULL AND av2.ValueDecimal IS NOT NULL AND av2.QuestionID = 314 AND av2.OptionID = 83 " +	// weight
				"LEFT OUTER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.DeletedSessionID IS NULL AND av3.ValueDecimal IS NOT NULL AND av3.QuestionID = 313 AND av3.OptionID = 82 " +	// height
				"LEFT OUTER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.DeletedSessionID IS NULL AND av4.ValueDecimal IS NOT NULL AND av4.QuestionID = 538 AND av4.OptionID = 82 " +	// waist
				"LEFT OUTER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.DeletedSessionID IS NULL AND av5.ValueInt IS NOT NULL AND av5.QuestionID = 310 AND av5.OptionID = 79 " +	// gender
				"LEFT OUTER JOIN AnswerValue av6 ON a.AnswerID = av6.AnswerID AND av6.DeletedSessionID IS NULL AND av6.ValueDecimal IS NOT NULL AND av6.QuestionID = 311 AND av6.OptionID = 81 " +	// age
				"LEFT OUTER JOIN AnswerValue av7 ON a.AnswerID = av7.AnswerID AND av7.DeletedSessionID IS NULL AND av7.ValueInt IS NOT NULL AND av7.QuestionID = 368 AND av7.OptionID = 109 " +	// act1
				"LEFT OUTER JOIN AnswerValue av8 ON a.AnswerID = av8.AnswerID AND av8.DeletedSessionID IS NULL AND av8.ValueInt IS NOT NULL AND av8.QuestionID = 369 AND av8.OptionID = 110 " +	// act2
				"LEFT OUTER JOIN AnswerValue av9 ON a.AnswerID = av9.AnswerID AND av9.DeletedSessionID IS NULL AND av9.ValueInt IS NOT NULL AND av9.QuestionID = 539 AND av9.OptionID = 134 " +	// veggies
				"LEFT OUTER JOIN AnswerValue av10 ON a.AnswerID = av10.AnswerID AND av10.DeletedSessionID IS NULL AND av10.ValueInt IS NOT NULL AND av10.QuestionID = 352 AND av10.OptionID = 104 " +	// bp1
				"LEFT OUTER JOIN AnswerValue av11 ON a.AnswerID = av11.AnswerID AND av11.DeletedSessionID IS NULL AND av11.ValueInt IS NOT NULL AND av11.QuestionID = 356 AND av11.OptionID = 90 " +	// bp2
				"LEFT OUTER JOIN AnswerValue av12 ON a.AnswerID = av12.AnswerID AND av12.DeletedSessionID IS NULL AND av12.ValueInt IS NOT NULL AND av12.QuestionID = 638 AND av12.OptionID = 90 " +	// blodsocker
				"LEFT OUTER JOIN AnswerValue av13 ON a.AnswerID = av13.AnswerID AND av13.DeletedSessionID IS NULL AND av13.ValueInt IS NOT NULL AND av13.QuestionID = 639 AND av13.OptionID = 137 " +	// släkt
				"WHERE p.ProjectRoundID = 20");
			while(rs.Read())
			{
				int Qscore = 0;
				bool inStudy = (rs.IsDBNull(16) && rs.IsDBNull(17));

				#region Findrisk
				if(!rs.IsDBNull(14))
				{
					bool incomplete = (rs.GetInt32(1) == 0 || rs.GetDecimal(2) == 0 || rs.GetDecimal(3) == 0 || rs.GetDecimal(4) == 0 ||
						rs.GetInt32(5) == 0 || rs.GetDecimal(6) == 0 || rs.GetInt32(7) == 0 || rs.GetInt32(8) == 0 ||
						rs.GetInt32(9) == 0 || rs.GetInt32(10) == 0 || rs.GetInt32(12) == 0 || rs.GetInt32(13) == 0);

					int Qage = Convert.ToInt32(rs.GetDecimal(6));
					bool Qfemale = (rs.GetInt32(5) == 255);
		
					decimal Qbmi = 0;
					if(rs.GetDecimal(3) != 0)
					{
						Qbmi = rs.GetDecimal(2) / (rs.GetDecimal(3)/100 * rs.GetDecimal(3)/100);
					}
					decimal Qwaist = rs.GetDecimal(4);
					bool Qnomotion = false;
					if(rs.GetInt32(7) == 342 && (rs.GetInt32(8) == 322 || rs.GetInt32(8) == 346))
					{
						Qnomotion = true;
					}
					if(rs.GetInt32(7) == 343 && rs.GetInt32(8) == 322)
					{
						Qnomotion = true;
					}
					bool Qnoveggies = rs.GetInt32(9) == 417;
					bool Qbp = rs.GetInt32(10) == 294 && rs.GetInt32(11) == 294;
					bool Qbs = rs.GetInt32(12) == 294;

					if(rs.GetInt32(13) == 428)
						Qscore += 3;
					if(rs.GetInt32(13) == 429)
						Qscore += 5;

					if(Qage >= 65)
					{
						Qscore += 4;
					}
					else if(Qage >= 55)
					{
						Qscore += 3;
					}
					else if(Qage >= 45)
					{
						Qscore += 2;
					}

					if(Qbmi > 30)
					{
						Qscore += 3;
					}
					else if(Qbmi >= 25)
					{
						Qscore += 1;
					}
		
					if(Qfemale && Qwaist > 88 || !Qfemale && Qwaist > 102)
					{
						Qscore += 4;
					}
					else if(Qfemale && Qwaist >= 80 || !Qfemale && Qwaist >= 94)
					{
						Qscore += 3;
					}

					if(Qnomotion)
					{
						Qscore += 2;
					}
					if(Qnoveggies)
					{
						Qscore += 1;
					}
					if(Qbp)
					{
						Qscore += 2;
					}
					if(Qbs)
					{
						Qscore += 5;
					}

					// UserID
					// InStudy
					// Findrisk
					// Diabetes
					// Complete
					// Submitted

					res.Text += rs.GetInt32(0) + "\t" + (inStudy ? 1 : 0) + "\t" + Qscore + "\t" + (rs.GetInt32(1) == 295 || rs.GetInt32(1) == 334 ? 0 : (rs.GetInt32(1) == 0 ? -1 : 1)) + "\t" + (incomplete ? 0 : 1) + "\t" + (rs.IsDBNull(14) ? 0 : 1) + "\r\n";
				}
				else
				{
					res.Text += rs.GetInt32(0) + "\t" + (inStudy ? 1 : 0) + "\t\t-1\t0\t0\r\n";
				}
				#endregion
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
	}
}
