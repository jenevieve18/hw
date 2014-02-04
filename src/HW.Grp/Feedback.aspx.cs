using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HW.Core.Helpers;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class Feedback : System.Web.UI.Page
	{
//		ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();
//		IAnswerRepository answerRepository = AppContext.GetRepositoryFactory().CreateAnswerRepository();
//		IProjectRepository projectRepository = AppContext.GetRepositoryFactory().CreateProjectRepository();
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		SqlAnswerRepository answerRepository = new SqlAnswerRepository();
		SqlProjectRepository projectRepository = new SqlProjectRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			bool showIndividuals = (HttpContext.Current.Request.QueryString["ShowIndividuals"] != null);
			int sponsorID = (HttpContext.Current.Request.QueryString["SponsorID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["SponsorID"]) : 0);
			if (sponsorID == 0) {
				sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
			}
//			SqlDataReader rs = Db.rs("SELECT " +
//			                         "ses.ProjectRoundID, " +                // 0
//			                         "ses.Internal, " +                      // 1
//			                         "ses.RoundText, " +                     // 2
//			                         "ses.IndividualFeedbackEmailSubject, " +// 3
//			                         "ses.IndividualFeedbackEmailBody " +    // 4
//			                         "FROM SponsorExtendedSurvey ses " +
//			                         "WHERE ses.SponsorID = " + sponsorID + " " +
//			                         "ORDER BY ses.SponsorExtendedSurveyID");
//			while (rs.Read())
			foreach (var s in sponsorRepository.FindExtendedSurveysBySponsor(sponsorID)) {
				ArrayList l = new ArrayList();
				int cx = 0;

//				List.Text += "<br/><br/><B>" + (rs.IsDBNull(1) ? "" : rs.GetString(1)) + (rs.IsDBNull(2) ? "" : (rs.IsDBNull(1) ? "" : ", ") + rs.GetString(2)) + "</B>";
				List.Text += "<br/><br/><B>" + s.Internal + (s.RoundText == "" ? "" : ", " + s.RoundText) + "</B>";

				#region burnout
				List.Text += "<br/><br/><i>Utbrändhet</i> ";
//				SqlDataReader rs2 = Db.rs("SELECT " +
//				                          "av1.ValueInt, " +
//				                          "av2.ValueInt, " +
//				                          "av3.ValueInt, " +
//				                          "av4.ValueInt, " +
//				                          "av5.ValueInt, " +
//				                          "av13.ValueInt, " +
//				                          "av14.ValueInt, " +
//				                          "av15.ValueInt, " +
//				                          "av16.ValueInt, " +
//				                          "u.Email " +
//				                          "FROM ProjectRoundUser u " +
//				                          "INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
//				                          "INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 380 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL " +
//				                          "INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 381 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL " +
//				                          "INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 382 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL " +
//				                          "INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 383 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL " +
//				                          "INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 384 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL " +
//				                          "INNER JOIN AnswerValue av13 ON a.AnswerID = av13.AnswerID AND av13.QuestionID = 401 AND av13.OptionID = 116 AND av13.DeletedSessionID IS NULL " +
//				                          "INNER JOIN AnswerValue av14 ON a.AnswerID = av14.AnswerID AND av14.QuestionID = 402 AND av14.OptionID = 116 AND av14.DeletedSessionID IS NULL " +
//				                          "INNER JOIN AnswerValue av15 ON a.AnswerID = av15.AnswerID AND av15.QuestionID = 403 AND av15.OptionID = 116 AND av15.DeletedSessionID IS NULL " +
//				                          "INNER JOIN AnswerValue av16 ON a.AnswerID = av16.AnswerID AND av16.QuestionID = 404 AND av16.OptionID = 116 AND av16.DeletedSessionID IS NULL " +
//				                          "WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + rs.GetInt32(0), "eFormSqlConnection");
//				while (rs2.Read())
				foreach (var a in answerRepository.FindByProjectRound(s.ProjectRound.Id)) {
					bool pbs = false;
					float scoreN = 0;
					for (int i = 5; i < 9; i++) {
//						switch (rs2.GetInt32(i))
						switch (a.Values[i].ValueInt) {
								case 367: scoreN += 5; break;
								case 363: scoreN += 4; break;
								case 368: scoreN += 3; break;
								case 369: scoreN += 2; break;
								case 361: scoreN += 1; break;
						}
					}
					if (scoreN / 4 > 3.25) {
						pbs = true;
					}

					scoreN = 0;
					for (int i = 0; i < 5; i++) {
						if (i == 0 || i == 1 || i == 4) {
//							switch (rs2.GetInt32(i))
							switch (a.Values[i].ValueInt) {
									case 361: scoreN += 1; break;
									case 362: scoreN += 2; break;
									case 363: scoreN += 3; break;
									case 360: scoreN += 4; break;
							}
						} else {
//							switch (rs2.GetInt32(i))
							switch (a.Values[i].ValueInt) {
									case 361: scoreN += 4; break;
									case 362: scoreN += 3; break;
									case 363: scoreN += 2; break;
									case 360: scoreN += 1; break;
							}
						}
					}

					scoreN = scoreN / 5;
					if (scoreN > 2.6) {
						//if (pbs)
						//{
//						if (!l.Contains(rs2.GetString(9))) { l.Add(rs2.GetString(9)); }
						if (!l.Contains(a.ProjectRoundUser.Email)) {
//							l.Add(rs2.GetString(9));
							l.Add(a.ProjectRoundUser.Email);
						}
						if (showIndividuals) {
//							List.Text += "<br/>" + rs2.GetString(9);
							List.Text += "<br/>" + a.ProjectRoundUser.Email;
						} else {
							cx++;
						}
						//}
					}
				}
//				rs2.Close();
				if (!showIndividuals) {
					List.Text += cx;
				}
				cx = 0;
				#endregion

				#region Depression
				List.Text += "<br/><br/><I>Depression</i> ";
//				rs2 = Db.rs("SELECT " +
//				            "av1.ValueInt, " +
//				            "av2.ValueInt, " +
//				            "av3.ValueInt, " +
//				            "av4.ValueInt, " +
//				            "av5.ValueInt, " +
//				            "av6.ValueInt, " +
//				            "av7.ValueInt, " +
//				            "u.Email " +
//				            "FROM ProjectRoundUser u " +
//				            "INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
//				            "INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 387 AND av1.OptionID = 115 AND av1.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 388 AND av2.OptionID = 115 AND av2.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 389 AND av3.OptionID = 115 AND av3.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 390 AND av4.OptionID = 115 AND av4.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 391 AND av5.OptionID = 115 AND av5.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av6 ON a.AnswerID = av6.AnswerID AND av6.QuestionID = 392 AND av6.OptionID = 115 AND av6.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av7 ON a.AnswerID = av7.AnswerID AND av7.QuestionID = 393 AND av7.OptionID = 122 AND av7.DeletedSessionID IS NULL " +
//				            "WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + rs.GetInt32(0), "eFormSqlConnection");
//				while (rs2.Read())
				foreach (var a in answerRepository.x(s.ProjectRound.Id)) {
					bool depr = false;
					for (int i = 0; i < 6; i++) {
//						switch (rs2.GetInt32(i))
						switch (a.Values[i].ValueInt) {
								case 364: depr = true; break;
								case 365: depr = true; break;
						}
					}
//					if (rs2.GetInt32(6) == 294 && depr)
					if (a.Values[6].ValueInt == 294 && depr) {
//						if (!l.Contains(rs2.GetString(7))) { l.Add(rs2.GetString(7)); }
						if (!l.Contains(a.ProjectRoundUser.Email)) {
//							l.Add(rs2.GetString(7));
							l.Add(a.ProjectRoundUser.Email);
						}
						if (showIndividuals) {
//							List.Text += "<br/>" + rs2.GetString(7);
							List.Text += "<br/>" + a.ProjectRoundUser.Email;
						} else {
							cx++;
						}
					}
				}
//				rs2.Close();
				if (!showIndividuals) {
					List.Text += cx;
				}
				cx = 0;
				#endregion

				#region OLBI (disabled)
				/*
            List.Text += "<br/><br/><I>OLBI</i> ";
            rs2 = Db.rs("SELECT " +
                        "av1.ValueInt, " +
                        "av2.ValueInt, " +
                        "av3.ValueInt, " +
                        "av4.ValueInt, " +
                        "av5.ValueInt, " +
                        "av11.ValueInt, " +
                        "av12.ValueInt, " +
                        "av13.ValueInt, " +
                        "av14.ValueInt, " +
                        "av15.ValueInt, " +
                        "u.Email " +
                        "FROM ProjectRoundUser u " +
                        "INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
                        "INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 380 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL " +
                        "INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 381 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL " +
                        "INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 382 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL " +
                        "INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 383 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL " +
                        "INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 384 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL " +
                        "INNER JOIN AnswerValue av11 ON a.AnswerID = av11.AnswerID AND av11.QuestionID = 459 AND av11.OptionID = 114 AND av11.DeletedSessionID IS NULL " +
                        "INNER JOIN AnswerValue av12 ON a.AnswerID = av12.AnswerID AND av12.QuestionID = 460 AND av12.OptionID = 114 AND av12.DeletedSessionID IS NULL " +
                        "INNER JOIN AnswerValue av13 ON a.AnswerID = av13.AnswerID AND av13.QuestionID = 461 AND av13.OptionID = 114 AND av13.DeletedSessionID IS NULL " +
                        "INNER JOIN AnswerValue av14 ON a.AnswerID = av14.AnswerID AND av14.QuestionID = 462 AND av14.OptionID = 114 AND av14.DeletedSessionID IS NULL " +
                        "INNER JOIN AnswerValue av15 ON a.AnswerID = av15.AnswerID AND av15.QuestionID = 463 AND av15.OptionID = 114 AND av15.DeletedSessionID IS NULL " +
                        "WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + rs.GetInt32(0), "eFormSqlConnection");
            while (rs2.Read())
            {
                float scoreN = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (i == 0 || i == 1 || i == 4)
                    {
                        switch (rs2.GetInt32(i))
                        {
                            case 361: scoreN += 1; break;
                            case 362: scoreN += 2; break;
                            case 363: scoreN += 3; break;
                            case 360: scoreN += 4; break;
                        }
                    }
                    else
                    {
                        switch (rs2.GetInt32(i))
                        {
                            case 361: scoreN += 4; break;
                            case 362: scoreN += 3; break;
                            case 363: scoreN += 2; break;
                            case 360: scoreN += 1; break;
                        }
                    }
                }
                scoreN = scoreN / 5;

                float scoreD = 0;
                for (int i = 5; i < 10; i++)
                {
                    if (i == 6 || i == 7 || i == 8)
                    {
                        switch (rs2.GetInt32(i))
                        {
                            case 361: scoreD += 1; break;
                            case 362: scoreD += 2; break;
                            case 363: scoreD += 3; break;
                            case 360: scoreD += 4; break;
                        }
                    }
                    else
                    {
                        switch (rs2.GetInt32(i))
                        {
                            case 361: scoreD += 4; break;
                            case 362: scoreD += 3; break;
                            case 363: scoreD += 2; break;
                            case 360: scoreD += 1; break;
                        }
                    }
                }
                scoreD = scoreD / 5;

                if (scoreN > 2.6 && scoreD > 2.6)
                {
                    if (!l.Contains(rs2.GetString(10))) { l.Add(rs2.GetString(10)); }
                    if (showIndividuals)
                    {
                        List.Text += "<br/>" + rs2.GetString(10);
                    }
                    else
                    {
                        cx++;
                    }
                }
            }
            rs2.Close();
            if (!showIndividuals)
            {
                List.Text += cx;
            }
            cx = 0;
				 */
				#endregion

				#region Sömn
				List.Text += "<br/><br/><I>Sömnproblem</i> ";
//				rs2 = Db.rs("SELECT " +
//				            "av1.ValueInt, " +
//				            "u.Email " +
//				            "FROM ProjectRoundUser u " +
//				            "INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
//				            "INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 374 AND av1.OptionID = 86 AND av1.DeletedSessionID IS NULL " +
//				            "WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + rs.GetInt32(0), "eFormSqlConnection");
//				while (rs2.Read())
				foreach (var a in answerRepository.FindByRoundQuestionAndOption(s.ProjectRound.Id, 374, 86))
				{
//					if (rs2.GetInt32(0) == 313 || rs2.GetInt32(0) == 314)
					if (a.Values[0].ValueInt == 313 || a.Values[0].ValueInt == 314)
					{
//						if (!l.Contains(rs2.GetString(1))) { l.Add(rs2.GetString(1)); }
						if (!l.Contains(a.ProjectRoundUser.Email)) {
							l.Add(a.ProjectRoundUser.Email);
						}
						if (showIndividuals)
						{
							List.Text += "<br/>" + a.ProjectRoundUser.Email;
						}
						else
						{
							cx++;
						}
					}
				}
//				rs2.Close();
				if (!showIndividuals)
				{
					List.Text += cx;
				}
				cx = 0;
				#endregion

				#region SRH
				List.Text += "<br/><br/><I>Självskattad hälsa</i> ";
//				rs2 = Db.rs("SELECT " +
//				            "av1.ValueInt, " +
//				            "u.Email " +
//				            "FROM ProjectRoundUser u " +
//				            "INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
//				            "INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 331 AND av1.OptionID = 98 AND av1.DeletedSessionID IS NULL " +
//				            "WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + rs.GetInt32(0), "eFormSqlConnection");
//				while (rs2.Read())
				foreach (var a in answerRepository.FindByRoundQuestionAndOption(s.ProjectRound.Id, 331, 98)) {
//					if (rs2.GetInt32(0) == 316 || rs2.GetInt32(0) == 317)
					if (a.Values[0].ValueInt == 316 || a.Values[0].ValueInt == 317) {
//						if (!l.Contains(rs2.GetString(1))) { l.Add(rs2.GetString(1)); }
						if (!l.Contains(a.ProjectRoundUser.Email)) {
//							l.Add(rs2.GetString(1));
							l.Add(a.ProjectRoundUser.Email);
						}
						if (showIndividuals) {
//							List.Text += "<br/>" + rs2.GetString(1);
							List.Text += "<br/>" + a.ProjectRoundUser.Email;
						} else {
							cx++;
						}
					}
				}
//				rs2.Close();
				if (!showIndividuals) {
					List.Text += cx;
				}
				cx = 0;
				#endregion

				#region Rygg
				List.Text += "<br/><br/><I>Rygg</i> ";
//				rs2 = Db.rs("SELECT " +
//				            "av1.ValueInt, " +
//				            "av2.ValueInt, " +
//				            "av3.ValueInt, " +
//				            "av4.ValueInt, " +
//				            "u.Email " +
//				            "FROM ProjectRoundUser u " +
//				            "INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
//				            "INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 337 AND av1.OptionID = 90 AND av1.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 341 AND av2.OptionID = 90 AND av2.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 342 AND av3.OptionID = 90 AND av3.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 343 AND av4.OptionID = 90 AND av4.DeletedSessionID IS NULL " +
//				            "WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + rs.GetInt32(0), "eFormSqlConnection");
//				while (rs2.Read())
				foreach (var a in answerRepository.a(s.ProjectRound.Id)) {
//					if (rs2.GetInt32(0) == 294 && (rs2.GetInt32(1) == 294 || rs2.GetInt32(2) == 294 || rs2.GetInt32(3) == 294))
					if (a.Values[0].ValueInt == 294 && (a.Values[1].ValueInt == 294 || a.Values[2].ValueInt == 294 || a.Values[3].ValueInt == 294)) {
//						if (!l.Contains(rs2.GetString(4))) { l.Add(rs2.GetString(4)); }
						if (!l.Contains(a.ProjectRoundUser.Email)) {
//							l.Add(rs2.GetString(4));
							l.Add(a.ProjectRoundUser.Email);
						}
						if (showIndividuals) {
//							List.Text += "<br/>" + rs2.GetString(4);
							List.Text += "<br/>" + a.ProjectRoundUser.Email;
						} else {
							cx++;
						}
					}
				}
//				rs2.Close();
				if (!showIndividuals) {
					List.Text += cx;
				}
				cx = 0;
				#endregion

				#region Nacke
				List.Text += "<br/><br/><I>Nacke</i> ";
//				rs2 = Db.rs("SELECT " +
//				            "av1.ValueInt, " +
//				            "av2.ValueInt, " +
//				            "av3.ValueInt, " +
//				            "av4.ValueInt, " +
//				            "u.Email " +
//				            "FROM ProjectRoundUser u " +
//				            "INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
//				            "INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 339 AND av1.OptionID = 90 AND av1.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 346 AND av2.OptionID = 90 AND av2.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 347 AND av3.OptionID = 90 AND av3.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 348 AND av4.OptionID = 90 AND av4.DeletedSessionID IS NULL " +
//				            "WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + rs.GetInt32(0), "eFormSqlConnection");
//				while (rs2.Read())
				foreach (var a in answerRepository.b(s.ProjectRound.Id)) {
//					if (rs2.GetInt32(0) == 294 && (rs2.GetInt32(1) == 294 || rs2.GetInt32(2) == 294 || rs2.GetInt32(3) == 294))
					if (a.Values[0].ValueInt == 294 && (a.Values[1].ValueInt == 294 || a.Values[2].ValueInt == 294 || a.Values[3].ValueInt == 294)) {
//						if (!l.Contains(rs2.GetString(4))) { l.Add(rs2.GetString(4)); }
						if (!l.Contains(a.ProjectRoundUser.Email)) {
//							l.Add(rs2.GetString(4));
							l.Add(a.ProjectRoundUser.Email);
						}
						if (showIndividuals) {
//							List.Text += "<br/>" + rs2.GetString(4);
							List.Text += "<br/>" + a.ProjectRoundUser.Email;
						} else {
							cx++;
						}
					}
				}
//				rs2.Close();
				if (!showIndividuals) {
					List.Text += cx;
				}
				cx = 0;
				#endregion

				#region Röker
				List.Text += "<br/><br/><I>Röker</i> ";
//				rs2 = Db.rs("SELECT " +
//				            "av1.ValueInt, " +
//				            "u.Email " +
//				            "FROM ProjectRoundUser u " +
//				            "INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
//				            "INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 370 AND av1.OptionID = 90 AND av1.DeletedSessionID IS NULL " +
//				            "WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + rs.GetInt32(0), "eFormSqlConnection");
//				while (rs2.Read())
				foreach (var a in answerRepository.FindByRoundQuestionAndOption(s.ProjectRound.Id, 370, 90)) {
//					if (rs2.GetInt32(0) == 294)
					if (a.Values[0].ValueInt == 294) {
//						if (!l.Contains(rs2.GetString(1))) { l.Add(rs2.GetString(1)); }
						if (!l.Contains(a.ProjectRoundUser.Email)) {
//							l.Add(rs2.GetString(1));
							l.Add(a.ProjectRoundUser.Email);
						}
						if (showIndividuals) {
//							List.Text += "<br/>" + rs2.GetString(1);
							List.Text += "<br/>" + a.ProjectRoundUser.Email;
						} else {
							cx++;
						}
					}
				}
//				rs2.Close();
				if (!showIndividuals) {
					List.Text += cx;
				}
				cx = 0;
				#endregion

				#region Hosta
				List.Text += "<br/><br/><I>Hosta</i> ";
//				rs2 = Db.rs("SELECT " +
//				            "av1.ValueInt, " +
//				            "u.Email " +
//				            "FROM ProjectRoundUser u " +
//				            "INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
//				            "INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 349 AND av1.OptionID = 90 AND av1.DeletedSessionID IS NULL " +
//				            "WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + rs.GetInt32(0), "eFormSqlConnection");
//				while (rs2.Read())
				foreach (var a in answerRepository.FindByRoundQuestionAndOption(s.ProjectRound.Id, 349, 90)) {
//					if (rs2.GetInt32(0) == 294)
					if (a.Values[0].ValueInt == 294) {
//						if (!l.Contains(rs2.GetString(1))) { l.Add(rs2.GetString(1)); }
						if (!l.Contains(a.ProjectRoundUser.Email)) {
//							l.Add(rs2.GetString(1));
							l.Add(a.ProjectRoundUser.Email);
						}
						if (showIndividuals) {
//							List.Text += "<br/>" + rs2.GetString(1);
							List.Text += "<br/>" + a.ProjectRoundUser.Email;
						} else {
							cx++;
						}
					}
				}
//				rs2.Close();
				if (!showIndividuals) {
					List.Text += cx;
				}
				cx = 0;
				#endregion

				#region Pip
				List.Text += "<br/><br/><I>Pip</i> ";
//				rs2 = Db.rs("SELECT " +
//				            "av1.ValueInt, " +
//				            "u.Email " +
//				            "FROM ProjectRoundUser u " +
//				            "INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
//				            "INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 350 AND av1.OptionID = 90 AND av1.DeletedSessionID IS NULL " +
//				            "WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + rs.GetInt32(0), "eFormSqlConnection");
//				while (rs2.Read())
				foreach (var a in answerRepository.FindByRoundQuestionAndOption(s.ProjectRound.Id, 350, 90)) {
//					if (rs2.GetInt32(0) == 294)
					if (a.Values[0].ValueInt == 294) {
//						if (!l.Contains(rs2.GetString(1))) { l.Add(rs2.GetString(1)); }
						if (!l.Contains(a.ProjectRoundUser.Email)) {
//							l.Add(rs2.GetString(1));
							l.Add(a.ProjectRoundUser.Email);
						}
						if (showIndividuals) {
//							List.Text += "<br/>" + rs2.GetString(1);
							List.Text += "<br/>" + a.ProjectRoundUser.Email;
						} else {
							cx++;
						}
					}
				}
//				rs2.Close();
				if (!showIndividuals) {
					List.Text += cx;
				}
				cx = 0;
				#endregion

				#region Alkohol
				List.Text += "<br/><br/><I>Alkohol</i> ";
//				rs2 = Db.rs("SELECT " +
//				            "av1.ValueInt, " +
//				            "av2.ValueInt, " +
//				            "av3.ValueInt, " +
//				            "av4.ValueInt, " +
//				            "u.Email " +
//				            "FROM ProjectRoundUser u " +
//				            "INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
//				            "INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 364 AND av1.OptionID = 108 AND av1.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 208 AND av2.OptionID = 41 AND av2.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 210 AND av3.OptionID = 41 AND av3.DeletedSessionID IS NULL " +
//				            "INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 213 AND av4.OptionID = 42 AND av4.DeletedSessionID IS NULL " +
//				            "WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + rs.GetInt32(0), "eFormSqlConnection");
//				while (rs2.Read())
				foreach (var a in answerRepository.f(s.ProjectRound.Id)) {
					if (
//						(rs2.GetInt32(0) == 340 || rs2.GetInt32(0) == 341)
						(a.Values[0].ValueInt == 340 || a.Values[0].ValueInt == 341)
						&&
						(
//							rs2.GetInt32(1) == 133 ||
//							rs2.GetInt32(1) == 134 ||
//							rs2.GetInt32(1) == 135 ||
//							rs2.GetInt32(2) == 133 ||
//							rs2.GetInt32(2) == 134 ||
//							rs2.GetInt32(2) == 135 ||
//							rs2.GetInt32(3) == 138
							a.Values[1].ValueInt == 133 ||
							a.Values[1].ValueInt == 134 ||
							a.Values[1].ValueInt == 135 ||
							a.Values[2].ValueInt == 133 ||
							a.Values[2].ValueInt == 134 ||
							a.Values[3].ValueInt == 135 ||
							a.Values[3].ValueInt == 138
						)
					)
					{
//						if (!l.Contains(rs2.GetString(4))) { l.Add(rs2.GetString(4)); }
						if (!l.Contains(a.ProjectRoundUser.Email)) {
//							l.Add(rs2.GetString(4));
							l.Add(a.ProjectRoundUser.Email);
						}
						if (showIndividuals) {
//							List.Text += "<br/>" + rs2.GetString(4);
							List.Text += "<br/>" + a.ProjectRoundUser.Email;
						} else {
							cx++;
						}
					}
				}
//				rs2.Close();
				if (!showIndividuals) {
					List.Text += cx;
				}
				cx = 0;
				#endregion

				#region Diabetes
				List.Text += "<br/><br/><I>Diabetes</i> ";
//				string sql = "SELECT " +
//					"av1.ValueDecimal, " +  // age
//					"av2.ValueInt, " +      // gender
//					"av3.ValueDecimal, " +  // waist
//					"av4.ValueInt, " +      // motion 1
//					"av5.ValueInt, " +      // motion 2
//					"av6.ValueInt, " +      // veggies
//					"av7.ValueInt, " +      // bp 1
//					"av8.ValueInt, " +      // bp 2
//					"av9.ValueInt, " +      // bs
//					"av10.ValueInt, " +      // ?
//					"av11.ValueDecimal, " +      // weight
//					"av12.ValueDecimal, " +      // height
//					"u.Email " +
//					"FROM ProjectRoundUser u " +
//					"INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID " +
//					"INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 311 AND av1.OptionID = 81 AND av1.DeletedSessionID IS NULL " +
//					"INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 310 AND av2.OptionID = 79 AND av2.DeletedSessionID IS NULL " +
//					"INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 538 AND av3.OptionID = 82 AND av3.DeletedSessionID IS NULL " +
//					"INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 368 AND av4.OptionID = 109 AND av4.DeletedSessionID IS NULL " +
//					"INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 369 AND av5.OptionID = 110 AND av5.DeletedSessionID IS NULL " +
//					"INNER JOIN AnswerValue av6 ON a.AnswerID = av6.AnswerID AND av6.QuestionID = 539 AND av6.OptionID = 134 AND av6.DeletedSessionID IS NULL " +
//					"LEFT OUTER JOIN AnswerValue av7 ON a.AnswerID = av7.AnswerID AND av7.QuestionID = 352 AND av7.OptionID = 104 AND av7.DeletedSessionID IS NULL " +
//					"LEFT OUTER JOIN AnswerValue av8 ON a.AnswerID = av8.AnswerID AND av8.QuestionID = 356 AND av8.OptionID = 90 AND av8.DeletedSessionID IS NULL " +
//					"LEFT OUTER JOIN AnswerValue av9 ON a.AnswerID = av9.AnswerID AND av9.QuestionID = 638 AND av9.OptionID = 90 AND av9.DeletedSessionID IS NULL " +
//					"INNER JOIN AnswerValue av10 ON a.AnswerID = av10.AnswerID AND av10.QuestionID = 639 AND av10.OptionID = 137 AND av10.DeletedSessionID IS NULL " +
//					"INNER JOIN AnswerValue av11 ON a.AnswerID = av11.AnswerID AND av11.QuestionID = 314 AND av11.OptionID = 83 AND av11.DeletedSessionID IS NULL " +
//					"INNER JOIN AnswerValue av12 ON a.AnswerID = av12.AnswerID AND av12.QuestionID = 313 AND av12.OptionID = 82 AND av12.DeletedSessionID IS NULL " +
//					"WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = " + rs.GetInt32(0);
				//List.Text += sql;
//				rs2 = Db.rs(sql, "eFormSqlConnection");
//				while (rs2.Read())
				foreach (var a in answerRepository.g(s.ProjectRound.Id)) {
					int Qscore = 0;

//					decimal Qbmi = rs2.GetDecimal(10) / ((rs2.GetDecimal(11) / 100) * (rs2.GetDecimal(11) / 100));
					decimal Qbmi = a.Values[10].ValueDecimal / ((a.Values[11].ValueDecimal / 100) * (a.Values[11].ValueDecimal / 100));
					if (Qbmi > 30) {
						Qscore += 3;
					} else if (Qbmi >= 25) {
						Qscore += 1;
					}

//					if (rs2.GetDecimal(0) >= 65) { Qscore += 4; } else if (rs2.GetDecimal(0) >= 55) { Qscore += 3; } else if (rs2.GetDecimal(0) >= 45) { Qscore += 2; }
					if (a.Values[0].ValueDecimal >= 65) {
						Qscore += 4;
					} else if (a.Values[0].ValueDecimal >= 55) {
						Qscore += 3;
					} else if (a.Values[0].ValueDecimal >= 45) {
						Qscore += 2;
					}

					//List.Text += "<br/>" + Qbmi + ":" + rs2.GetDecimal(0);

//					if (rs2.GetInt32(1) == 255 && rs2.GetDecimal(2) > 88 || rs2.GetInt32(1) != 255 && rs2.GetDecimal(2) > 102) { Qscore += 4; } else if (rs2.GetInt32(1) == 255 && rs2.GetDecimal(2) >= 80 || rs2.GetInt32(1) != 255 && rs2.GetDecimal(2) >= 94) { Qscore += 3; }
					if (a.Values[1].ValueInt == 255 && a.Values[2].ValueDecimal > 88 || a.Values[1].ValueInt != 255 && a.Values[2].ValueDecimal > 102) {
						Qscore += 4;
					} else if (a.Values[1].ValueInt == 255 && a.Values[2].ValueDecimal >= 80 || a.Values[1].ValueInt != 255 && a.Values[2].ValueDecimal >= 94) {
						Qscore += 3;
					}

//					if ((rs2.GetInt32(3) == 342 && (rs2.GetInt32(4) == 322 || rs2.GetInt32(4) == 346)) || (rs2.GetInt32(3) == 343 && rs2.GetInt32(4) == 322)) { Qscore += 2; }
					if ((a.Values[3].ValueInt == 342 && (a.Values[4].ValueInt == 322 || a.Values[4].ValueInt == 346)) || (a.Values[3].ValueInt == 343 && a.Values[4].ValueInt == 322)) {
						Qscore += 2;
					}

//					if (rs2.GetInt32(5) == 417) { Qscore += 1; }
					if (a.Values[5].ValueInt == 417) {
						Qscore += 1;
					}

//					if (!rs2.IsDBNull(6) && rs2.GetInt32(6) == 294 && !rs2.IsDBNull(7) && rs2.GetInt32(7) == 294) { Qscore += 2; }
					if (a.Values[6].ValueInt == 294 && a.Values[7].ValueInt == 294) {
						Qscore += 2;
					}

//					if (!rs2.IsDBNull(8) && rs2.GetInt32(8) == 294) { Qscore += 5; }
					if (a.Values[8].ValueInt == 294) {
						Qscore += 5;
					}

//					if (rs2.GetInt32(9) == 428) { Qscore += 3; } else if (rs2.GetInt32(9) == 429) { Qscore += 5; }
					if (a.Values[9].ValueInt == 428) {
						Qscore += 3;
					} else if (a.Values[9].ValueInt == 429) {
						Qscore += 5;
					}

					if (Qscore >= 15) {
//						if (!l.Contains(rs2.GetString(12))) { l.Add(rs2.GetString(12)); }
						if (!l.Contains(a.ProjectRoundUser.Email)) {
							l.Add(a.ProjectRoundUser.Email	);
						}
						if (showIndividuals) {
//							List.Text += "<br/>" + rs2.GetString(12);
							List.Text += "<br/>" + a.ProjectRoundUser.Email;
						} else {
							cx++;
						}
					}
				}
//				rs2.Close();
				if (!showIndividuals) {
					List.Text += cx;
				}
				cx = 0;
				#endregion

				List.Text += "<br/><br/><I>Totalt unika</i> ";

				if (showIndividuals) {
					foreach (string x in l) {
//						if (!rs.IsDBNull(3) && !rs.IsDBNull(4))
						if (s.IndividualFeedbackEmailSubject != "" && s.IndividualFeedbackEmailBody != "") {
							if (x == (HttpContext.Current.Request.QueryString["Send"] != null ? HttpContext.Current.Request.QueryString["Send"].ToString() : "")) {
								if (Db.isEmail(x)) {
//									Db.sendMail("info@healthwatch.se", x, rs.GetString(4), rs.GetString(3));
//									Db.sendMail("info@healthwatch.se", x, s.IndividualFeedbackEmailBody, s.IndividualFeedbackEmailSubject);
									Db.sendMail("info@healthwatch.se", x, s.IndividualFeedbackEmailSubject, s.IndividualFeedbackEmailBody);
								}
							}
//							List.Text += "<br/><A HREF=\"feedback.aspx?PRID=" + rs.GetInt32(0) + "&Send=" + x + "\"/>" + x + "</A>";
							List.Text += "<br/><A HREF=\"feedback.aspx?PRID=" + s.ProjectRound.Id + "&Send=" + x + "\"/>" + x + "</A>";
						} else {
							List.Text += "<br/>" + x;
						}
					}
				} else {
					List.Text += l.Count;
				}

//				rs2 = Db.rs("SELECT " +
//				            "pru.Email, " +
//				            "opl.Text " +
//				            "FROM ProjectRoundUser pru " +
//				            "INNER JOIN Answer a ON pru.ProjectRoundUserID = a.ProjectRoundUserID " +
//				            "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND (av.QuestionID = 2473 OR av.QuestionID = 3470) AND (av.OptionID = 421 OR av.OptionID = 989) AND (av.ValueInt = 1262 OR av.ValueInt = 2623) " +
//				            "LEFT OUTER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 2588 AND av2.OptionID = 711 " +
//				            "LEFT OUTER JOIN OptionComponentLang opl ON av2.ValueInt = opl.OptionComponentID AND opl.LangID = 1 " +
//				            "WHERE a.EndDT IS NOT NULL AND pru.ProjectRoundID = " + rs.GetInt32(0) + " " +
//				            "ORDER BY pru.Email", "eFormSqlConnection");
				var u = answerRepository.ReadByProjectRound(s.ProjectRound.Id);
//				if (rs2.Read())
				if (u != null) {
					List.Text += "<br/><br/><I>Intervju</i><table border=\"1\"><tr><td>Email</td><td>Chef</td></tr>";
//					do
					foreach (var v in u.Values) {
//						List.Text += "<tr><td>" + rs2.GetString(0) + "</td><td>" + (rs2.IsDBNull(1) ? "" : rs2.GetString(1)) + "</td></tr>";
						List.Text += "<tr><td>" + u.ProjectRoundUser.Email + "</td><td>" + v.Option.CurrentComponent.Text + "</td></tr>";
					}
//					while (rs2.Read());
					List.Text += "</table>";
				}
//				rs2.Close();
			}
//			rs.Close();
		}
	}
}