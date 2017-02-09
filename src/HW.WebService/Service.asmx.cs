using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace HW.WebService
{
	[WebService(Namespace = "https://ws.healthwatch.se/", Description="HealthWatch.se web service. LanguageID 1:Swedish 2:English")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.Web.Script.Services.ScriptService]
	public class Service : System.Web.Services.WebService
	{
		public Service () {

			//Uncomment the following line if using designed components
			//InitializeComponent();
		}

		public struct SponsorInvite
		{
			public string email;
			public string userKey;
			public string invitationKey;
		}
		public struct Sponsor
		{
			public string sponsor;
			public string sponsorKey;
		}
		public struct Event
		{
			public DateTime time;
			public string description;
			public string result;
			public string formInstanceKey;
			public EventType type;
			public int eventID;
		}
		public struct Calendar
		{
			public DateTime date;
			public Mood mood;
			public string note;
			public Event[] events;
		}
		public struct Exercise
		{
			public string exerciseHeader;
			public string exerciseContent;
			public string exerciseTime;
			public string exerciseArea;
			public int exerciseAreaID;
		}
		public struct ExerciseArea
		{
			public string exerciseArea;
			public int exerciseAreaID;
		}
		public struct ExerciseVariant
		{
			public int exerciseVariantLangID;
			public string exerciseType;
		}
		public struct ExerciseInfo
		{
			public string exercise; // 8
			public int exerciseID; // 6
			public string exerciseTeaser; // 10
			public string exerciseTime; // 9
			public string exerciseArea; // 3
			public int exerciseAreaID; // 4
			public string exerciseImage; // 5
			public ExerciseVariant[] exerciseVariant; // 2
			public int popularity;
		}
		public struct UserInfo
		{
			public String username;
			public String email;
			public String alternateEmail;
			public int languageID;
			public int userID;
		}
		public struct UserData
		{
			public String token;
			public DateTime tokenExpires;
			public int languageID;
		}
		public struct DeviceID
		{
			public string registrationID;
			public bool inactive;
			public string phoneName;
		}
		public struct NewsCategory
		{
			public int newsCategoryID;
			public String newsCategory;
			public String newsCategoryAlias;
			public String newsCategoryImage;
			public int languageID;
			public int totalCount;
			public int lastXdaysCount;
		}
		public struct News
		{
			public int newsCategoryID;
			public String newsCategory;
			public String newsCategoryAlias;
			public String newsCategoryImage;
			public int languageID;
			public int newsID;
			public DateTime DT;
			public string headline;
			public string teaser;
			public string body;
			public string link;
		}
		public struct Answer
		{
			public int AnswerID;
			public int SortOrder;
			public String AnswerText;
			public int AnswerValue;
		}
		public struct Question
		{
			public int QuestionID;
			public int SortOrder;
//			public QuestionTypes QuestionType;
			QuestionTypes questionType;
			public QuestionTypes QuestionType {
				get {
					if (questionType == 0) {
						return QuestionTypes.SingleChoiceFewOptions;
					}
					return questionType;
				}
				set { questionType = value; }
			}
			public String QuestionText;
			public String MeasurementUnit;
			public bool Mandatory;
			public string DefaultValue;
			public int RequiredNumberOfCharacters;
			public int MaximumNumberOfCharacters;
			public Answer[] AnswerOptions;
			public VisibilityConditionOr[] VisibilityConditions;
			public int OptionID;
		}
		public struct VisibilityConditionOr
		{
			public int QuestionID;
			public int AnswerID;
		}
		public struct MeasureType
		{
			public string measureType;
			public int measureTypeID;
		}
		public struct MeasureCategory
		{
			public string measureCategory;
			public int measureCategoryID;
			public string measureType;
			public int measureTypeID;
		}
		public struct Measure
		{
			public string measure;
			public int measureID;
			public string measureCategory;
			public int measureCategoryID;
			public string moreInfo;
			public int componentCount;
			public MeasureComponent[] measureComponents;
		}
		public struct MeasureComponent
		{
			public string measureComponent;
			public int measureComponentID;
			public QuestionTypes questionType;
			public bool inherited;
			public bool hasAutoCalculateChildren;
			public bool isAutoCalculated;
			public int decimals;
			public string unit;
			public string autoCalculateScript;
			public string triggerScript;
			public string inheritedValue;
		}
		public struct Reminder
		{
			public int type;
			public int autoLoginLink;
			public int sendAtHour;
			public int regularity;
			public bool regularityDailyMonday;
			public bool regularityDailyTuesday;
			public bool regularityDailyWednesday;
			public bool regularityDailyThursday;
			public bool regularityDailyFriday;
			public bool regularityDailySaturday;
			public bool regularityDailySunday;
			public int regularityWeeklyDay;
			public int regularityWeeklyEvery;
			public int regularityMonthlyWeekNr;
			public int regularityMonthlyDay;
			public int regularityMonthlyEvery;
			public int inactivityCount;
			public int inactivityPeriod;
		}
		public struct UserMeasureComponent
		{
			public int MeasureComponentID;
			public string value;
		}
		public struct Form
		{
			public string formKey;
			public string form;
		}
		public struct FormQuestionAnswer
		{
			public int questionID;
			public int optionID;
			public string answer;
		}
		public struct FormInstance
		{
			public string formDisclaimer;
			public string formInstanceKey;
			public DateTime dateTime;
			public FormInstanceFeedback[] fiv;
		}
		public struct FormInstanceFeedback
		{
			public int feedbackTemplateID;
			public string header;
			public string value;
			public string analysis;
			public Rating rating;
			public string feedback;
			public string actionPlan;
			public string yellowLow;
			public string greenLow;
			public string greenHigh;
			public string yellowHigh;
		}
		public struct FormFeedbackTemplate
		{
			public int feedbackTemplateID;
			public string header;
		}
		public struct FormFeedback
		{
			public int feedbackTemplateID;
			public string header;
			public string formInstanceKey;
			public DateTime dateTime;
			public string value;
			public string profileValue;
			public string databaseValue;
		}
		public struct WordsOfWisdom
		{
			public string words;
			public string author;
		}
		public enum QuestionTypes { SingleChoiceFewOptions = 1, SingleChoiceManyOptions = 7, FreeText = 2, Numeric = 4, Date = 3, VAS = 9 };
		public enum Mood { NotSet = 0, DontKnow = 1, Happy = 2, Neutral = 3, Unhappy = 4 };
		public enum EventType { Form = 1, Measurement = 2, Exercise = 3 };
		public enum Rating { NotKnown = 0, Unhealthy = 1, Warning = 2, Healthy = 3 };

		[WebMethod(CacheDuration = 10 * 60, Description = "Todays words of wisdom.")]
		public WordsOfWisdom TodaysWordsOfWisdom(int languageID)
		{
			SqlDataReader r = rs("SELECT wl.Wise, wl.WiseBy FROM WiseLang wl INNER JOIN Wise w ON wl.WiseID = w.WiseID WHERE w.LastShown = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND wl.LangID = " + languageID);
			if (!r.Read())
			{
				r.Close();
				r = rs("SELECT TOP 1 wl.Wise, wl.WiseBy, w.WiseID FROM WiseLang wl INNER JOIN Wise w ON wl.WiseID = w.WiseID WHERE wl.LangID = " + languageID + " ORDER BY w.LastShown ASC");
				r.Read();
				exec("UPDATE Wise SET LastShown = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE WiseID = " + r.GetInt32(2));
			}

			WordsOfWisdom w = new WordsOfWisdom();
			w.words = r.GetString(0);
			w.author = r.GetString(1);

			return w;
		}
		[WebMethod(Description = "Get user form feedback over time for specific template. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public FormFeedback[] UserGetFormFeedback(string token,string formKey,int formFeedbackTemplateID,DateTime fromDateTime,DateTime toDateTime,int languageID,int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int projectRoundUnitID = 0, projectRoundUserID = 0;
				SqlDataReader r = rs("SELECT " +
				                     "spru.ProjectRoundUnitID, " +
				                     "upru.ProjectRoundUserID " +
				                     "FROM [User] u " +
				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
				                     "INNER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
				                     "WHERE u.UserID = " + userID + " " +
				                     "AND REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') = '" + formKey.Replace("'", "") + "'");
				if (r.Read())
				{
					projectRoundUnitID = r.GetInt32(0);
					projectRoundUserID = r.GetInt32(1);
				}
				r.Close();
				int QID = 0, OID = 0; string header = "";
				#region Get header and question/option id
				r = rs("SELECT " +
				       "wqo.QuestionID, " +
				       "wqo.OptionID, " +
				       "wqol.FeedbackHeader " +
				       "FROM Report r " +
				       "INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
				       "INNER JOIN ReportPartComponent rpc ON rp.ReportPartID = rpc.ReportPartID " +
				       "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
				       "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + languageID + " " +
				       "WHERE rpc.ReportPartID = " + formFeedbackTemplateID, "eFormSqlConnection");
				if(r.Read())
				{
					QID = r.GetInt32(0);
					OID = r.GetInt32(1);
					header = r.GetString(2);
				}
				r.Close();
				#endregion
				int cx = execIntScal("SELECT " +
				                     "COUNT(*) " +
				                     "FROM Answer a " +
				                     "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +
				                     //"INNER JOIN healthWatch..UserProjectRoundUser h ON ha.ProjectRoundUserID = h.ProjectRoundUserID " +
				                     "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
				                     "AND av.ValueInt IS NOT NULL " +
				                     "AND av.DeletedSessionID IS NULL " +
				                     "AND av.QuestionID = " + QID + " " +
				                     "AND av.OptionID = " + OID + " " +
				                     "WHERE a.EndDT IS NOT NULL " +
				                     "AND ha.ProjectRoundUserID = " + projectRoundUserID + " " +
				                     "AND a.EndDT >= '" + fromDateTime.ToString("yyyy-MM-dd") + "' " +
				                     "AND a.EndDT <= '" + toDateTime.ToString("yyyy-MM-dd") + "'", "eFormSqlConnection");
				FormFeedback[] ret = new FormFeedback[cx];
				cx = 0;
				int db = execIntScal("SELECT AVG(av.ValueInt) FROM Answer a INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
				                     "WHERE a.EndDT IS NOT NULL " +
				                     "AND av.ValueInt IS NOT NULL " +
				                     "AND av.DeletedSessionID IS NULL " +
				                     "AND av.QuestionID = " + QID + " " +
				                     "AND av.OptionID = " + OID + " " +
				                     "AND a.EndDT >= '" + fromDateTime.ToString("yyyy-MM-dd") + "' " +
				                     "AND a.EndDT <= '" + toDateTime.ToString("yyyy-MM-dd") + "'", "eFormSqlConnection");
				r = rs("SELECT " +
				       "av.ValueInt, " +
				       "a.EndDT, " +
				       "REPLACE(CONVERT(VARCHAR(255),ha.AnswerKey),'-','') " +
				       "FROM Answer a " +
				       "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +
				       //"INNER JOIN healthWatch..UserProjectRoundUser h ON ha.ProjectRoundUserID = h.ProjectRoundUserID " +
				       "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
				       "AND av.ValueInt IS NOT NULL " +
				       "AND av.DeletedSessionID IS NULL " +
				       "AND av.QuestionID = " + QID + " " +
				       "AND av.OptionID = " + OID + " " +
				       "WHERE a.EndDT IS NOT NULL " +
				       "AND ha.ProjectRoundUserID = " + projectRoundUserID + " " +
				       "AND a.EndDT >= '" + fromDateTime.ToString("yyyy-MM-dd") + "' " +
				       "AND a.EndDT <= '" + toDateTime.ToString("yyyy-MM-dd") + "' " +
				       "ORDER BY a.EndDT ASC", "eFormSqlConnection");
				while (r.Read())
				{
					ret[cx].dateTime = r.GetDateTime(1);
					ret[cx].value = r.GetInt32(0).ToString();
					ret[cx].databaseValue = db.ToString();
					ret[cx].profileValue = db.ToString();
					ret[cx].feedbackTemplateID = formFeedbackTemplateID;
					ret[cx].formInstanceKey = r.GetString(2);
					ret[cx].header = header;
					cx++;
				}
				r.Close();

				return ret;
			}
			return (new FormFeedback[0]);
		}
		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerate form feedback templates. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public FormFeedbackTemplate[] FormFeedbackTemplateEnum(string token,string formKey,int languageID,int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int projectRoundUnitID = 0, projectRoundUserID = 0;
				SqlDataReader r = rs("SELECT " +
				                     "spru.ProjectRoundUnitID, " +
				                     "upru.ProjectRoundUserID, " +
				                     "u.Email, " +
				                     "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') " +
				                     "FROM [User] u " +
				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
				                     "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
				                     "WHERE u.UserID = " + userID);
				while (r.Read())
				{
					if (formKey == r.GetString(3))
					{
						projectRoundUnitID = r.GetInt32(0);
						if (r.IsDBNull(1))
						{
							projectRoundUserID = createSurveyUser(userID, r.GetInt32(0), r.GetString(2));
						}
						else
						{
							projectRoundUserID = r.GetInt32(1);
						}
					}
				}
				r.Close();

				int cx = execIntScal("SELECT " +
				                     "COUNT(*) " +
				                     "FROM ProjectRoundUnit pru " +
				                     "INNER JOIN Report r ON dbo.cf_unitIndividualReportID(pru.ProjectRoundUnitID) = r.ReportID " +
				                     "LEFT OUTER JOIN ReportLang rl ON r.ReportID = rl.ReportID AND rl.LangID = " + languageID + " " +
				                     "LEFT OUTER JOIN ReportPart rp ON rp.ReportID = r.ReportID AND rp.Type = 8 " +
				                     "LEFT OUTER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = " + languageID + " " +
				                     "WHERE " +
				                     "pru.ProjectRoundUnitID = " + projectRoundUnitID, "eFormSqlConnection");

				FormFeedbackTemplate[] fft = new FormFeedbackTemplate[cx];
				cx = 0;
				string sql = "SELECT " +
					"rl.Feedback, " +
					"rpl.Subject, " +
					"rpl.AltText, " +
					"rp.ReportPartID " +
					"FROM ProjectRoundUnit pru " +
					"INNER JOIN Report r ON dbo.cf_unitIndividualReportID(pru.ProjectRoundUnitID) = r.ReportID " +
					"LEFT OUTER JOIN ReportLang rl ON r.ReportID = rl.ReportID AND rl.LangID = " + languageID + " " +
					"LEFT OUTER JOIN ReportPart rp ON rp.ReportID = r.ReportID AND rp.Type = 8 " +
					"LEFT OUTER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = " + languageID + " " +
					"WHERE " +
					"pru.ProjectRoundUnitID = " + projectRoundUnitID + " " +
					"ORDER BY rp.SortOrder";
				r = rs(sql, "eFormSqlConnection");
				while (r.Read())
				{
					fft[cx].feedbackTemplateID = r.GetInt32(3);
					fft[cx].header = r.IsDBNull(1) ? "" : r.GetString(1);
					cx++;
				}
				r.Close();

				return fft;
			}
			return (new FormFeedbackTemplate[0]);
		}
		[WebMethod(Description = "Get user form feedback. Leave formInstanceKey blank for latest instance. Tag EXID in actionPlan is reference to ExerciseID, should be replaced with hyperlink. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public FormInstance UserGetFormInstanceFeedback(string token,string formKey,string formInstanceKey,int languageID,int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int projectRoundUnitID = 0, projectRoundUserID = 0;
				SqlDataReader r = rs("SELECT " +
				                     "spru.ProjectRoundUnitID, " +
				                     "upru.ProjectRoundUserID, " +
				                     "u.Email, " +
				                     "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') " +
				                     "FROM [User] u " +
				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
				                     "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
				                     "WHERE u.UserID = " + userID);
				while (r.Read())
				{
					if (formKey == r.GetString(3))
					{
						projectRoundUnitID = r.GetInt32(0);
						if (r.IsDBNull(1))
						{
							projectRoundUserID = createSurveyUser(userID, r.GetInt32(0), r.GetString(2));
						}
						else
						{
							projectRoundUserID = r.GetInt32(1);
						}
					}
				}
				r.Close();
				if (formInstanceKey == "")
				{
					r = rs("SELECT " +
					       "TOP 1 " +
					       "REPLACE(CONVERT(VARCHAR(255),uprua.AnswerKey),'-','') " +
					       "FROM [User] u " +
					       "INNER JOIN UserProjectRoundUser upru ON u.UserID = upru.UserID " +
					       "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
					       "INNER JOIN eform..Answer a ON uprua.AnswerID = a.AnswerID " +
					       "INNER JOIN eform..ProjectRound pr ON a.ProjectRoundID = pr.ProjectRoundID " +
					       "INNER JOIN eform..ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
					       "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
					       "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID AND spru.SurveyID = ISNULL(NULLIF(pru.SurveyID,0),pr.SurveyID) " +
					       "WHERE u.UserID = " + userID + " " +
					       "AND REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') = '" + formKey.Replace("'","") + "' " +
					       "ORDER BY uprua.DT DESC, uprua.AnswerID DESC");
					if (r.Read())
					{
						formInstanceKey = r.GetString(0);
					}
					r.Close();
				}
				if (formInstanceKey != "" && projectRoundUnitID != 0)
				{
					FormInstance fi = new FormInstance();
					fi.formInstanceKey = formInstanceKey;

					#region Specific measurement
					string sql = "SELECT " +
						"rl.Feedback, " +
						"rpl.Subject, " +
						"rpl.AltText, " +
						"rp.ReportPartID, " +
						"(SELECT COUNT(*) FROM ReportPart rp2 WHERE rp2.ReportID = r.ReportID) AS CX " +
						"FROM ProjectRoundUnit pru " +
						"INNER JOIN Report r ON dbo.cf_unitIndividualReportID(pru.ProjectRoundUnitID) = r.ReportID " +
						"LEFT OUTER JOIN ReportLang rl ON r.ReportID = rl.ReportID AND rl.LangID = " + languageID + " " +
						"LEFT OUTER JOIN ReportPart rp ON rp.ReportID = r.ReportID AND rp.Type = 8 " +
						"LEFT OUTER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = " + languageID + " " +
						"WHERE " +
						"pru.ProjectRoundUnitID = " + projectRoundUnitID + " " +
						"ORDER BY rp.SortOrder";
					r = rs(sql, "eFormSqlConnection");
					if (r.Read())
					{
						fi.formDisclaimer = r.IsDBNull(0) ? "" : r.GetString(0);
						FormInstanceFeedback[] fiv = new FormInstanceFeedback[r.GetInt32(4)];
						int cx = 0;
						do
						{
							fiv[cx].feedbackTemplateID = r.GetInt32(3);

							SqlDataReader r2 = rs("SELECT " +
							                      "rpc.WeightedQuestionOptionID, " +	// 0
							                      "wqol.WeightedQuestionOption, " +
							                      "wqo.TargetVal, " +
							                      "wqo.YellowLow, " +
							                      "wqo.GreenLow, " +
							                      "wqo.GreenHigh, " +					// 5
							                      "wqo.YellowHigh, " +
							                      "wqo.QuestionID, " +
							                      "wqo.OptionID, " +
							                      "wqol.FeedbackHeader, " +
							                      "wqol.Feedback," +                  // 10
							                      "wqol.FeedbackRedLow," +
							                      "wqol.FeedbackYellowLow," +
							                      "wqol.FeedbackGreen," +
							                      "wqol.FeedbackYellowHigh," +
							                      "wqol.FeedbackRedHigh," +           // 15
							                      "wqol.ActionRedLow," +
							                      "wqol.ActionYellowLow," +
							                      "wqol.ActionGreen," +
							                      "wqol.ActionYellowHigh," +
							                      "wqol.ActionRedHigh " +             // 20
							                      "FROM Report r " +
							                      "INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
							                      "INNER JOIN ReportPartComponent rpc ON rp.ReportPartID = rpc.ReportPartID " +
							                      "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
							                      "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + languageID + " " +
							                      "WHERE rpc.ReportPartID = " + r.GetInt32(3) + " " +
							                      "ORDER BY rp.SortOrder, rpc.SortOrder", "eFormSqlConnection");
							while (r2.Read())
							{
								fiv[cx].header = r2.GetString(9);
								if (!r2.IsDBNull(10) && r2.GetString(10) != "")
								{
									fiv[cx].analysis = r2.GetString(10);
								}
								fiv[cx].rating = Rating.NotKnown;

								SqlDataReader r3 = rs("SELECT TOP 1 " +
								                      "av.ValueInt, " +
								                      "a.EndDT, " +
								                      "a.AnswerID " +
								                      "FROM Answer a " +
								                      "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +
								                      "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
								                      "AND av.DeletedSessionID IS NULL " +
								                      "AND av.QuestionID = " + r2.GetInt32(7) + " " +
								                      "AND av.OptionID = " + r2.GetInt32(8) + " " +
								                      "WHERE a.EndDT IS NOT NULL " +
								                      "AND ha.AnswerKey = '" + formInstanceKey + "' " +
								                      "ORDER BY a.EndDT DESC", "eFormSqlConnection");
								if (r3.Read())
								{
									fi.dateTime = r3.GetDateTime(1);
									fiv[cx].value = r3.GetInt32(0).ToString();
									if (!r2.IsDBNull(3))
										fiv[cx].yellowLow = r2.GetInt32(3).ToString();
									if (!r2.IsDBNull(4))
										fiv[cx].greenLow = r2.GetInt32(4).ToString();
									if (!r2.IsDBNull(5))
										fiv[cx].greenHigh = r2.GetInt32(5).ToString();
									if (!r2.IsDBNull(6))
										fiv[cx].yellowHigh = r2.GetInt32(6).ToString();

									if (!r3.IsDBNull(0))
									{
										bool hasColor = false;
										int levelID = 0;

										#region Levels
										if (!r2.IsDBNull(3))
										{
											hasColor = true;
											if (r2.GetInt32(3) >= 0 && r2.GetInt32(3) <= 100)
											{

												if (r3.GetInt32(0) >= r2.GetInt32(3))
												{
													levelID = 2;
												}
											}
										}
										if (!r2.IsDBNull(4))
										{
											hasColor = true;
											if (r2.GetInt32(4) >= 0 && r2.GetInt32(4) <= 100)
											{

												if (r3.GetInt32(0) >= r2.GetInt32(4))
												{
													levelID = 3;
												}
											}
										}
										if (!r2.IsDBNull(5))
										{
											hasColor = true;
											if (r2.GetInt32(5) >= 0 && r2.GetInt32(5) <= 100)
											{

												if (r3.GetInt32(0) >= r2.GetInt32(5))
												{
													levelID = 4;
												}
											}
										}
										if (!r2.IsDBNull(6))
										{
											hasColor = true;
											if (r2.GetInt32(6) >= 0 && r2.GetInt32(6) <= 100)
											{

												if (r3.GetInt32(0) >= r2.GetInt32(6))
												{
													levelID = 5;
												}
											}
										}
										if (levelID == 0)
										{
											if (hasColor)
											{
												levelID = 1;
											}
										}
										#endregion
										switch (levelID)
										{
												case 1: fiv[cx].rating = Rating.Unhealthy; break;
												case 2: fiv[cx].rating = Rating.Warning; break;
												case 3: fiv[cx].rating = Rating.Healthy; break;
												case 4: fiv[cx].rating = Rating.Warning; break;
												case 5: fiv[cx].rating = Rating.Unhealthy; break;
										}
										if (levelID != 0)
										{
											if (!r2.IsDBNull(feedbackIdx(levelID)) && r2.GetString(feedbackIdx(levelID)) != "")
											{
												fiv[cx].feedback = r2.GetString(feedbackIdx(levelID));
											}
											if (!r2.IsDBNull(actionIdx(levelID)) && r2.GetString(actionIdx(levelID)) != "")
											{
												fiv[cx].actionPlan = r2.GetString(actionIdx(levelID));
											}
										}
									}

								}
								r3.Close();
							}
							r2.Close();

							cx++;
						}
						while (r.Read());

						fi.fiv = fiv;
					}
					r.Close();
					#endregion

					return fi;
				}
			}
			return (new FormInstance());
		}
		[WebMethod(Description = "Set user form, question by question. Leave formInstanceKey blank on first answer. Returns formInstanceKey if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public string UserSetFormInstanceQuestion(string token,string formKey,string formInstanceKey,int questionID,int optionID,string answer,int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int projectRoundUnitID = 0, projectRoundUserID = 0;
				SqlDataReader r = rs("SELECT " +
				                     "spru.ProjectRoundUnitID, " +
				                     "upru.ProjectRoundUserID, " +
				                     "u.Email, " +
				                     "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') " +
				                     "FROM [User] u " +
				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
				                     "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
				                     "WHERE u.UserID = " + userID);
				while (r.Read())
				{
					if (formKey == r.GetString(3))
					{
						projectRoundUnitID = r.GetInt32(0);
						if (r.IsDBNull(1))
						{
							projectRoundUserID = createSurveyUser(userID, r.GetInt32(0), r.GetString(2));
						}
						else
						{
							projectRoundUserID = r.GetInt32(1);
						}
					}
				}
				r.Close();
				#region Fetch ProjectRoundID
				int projectRoundID = execIntScal("SELECT " +
				                                 "pru.ProjectRoundID " +
				                                 "FROM ProjectRoundUser u " +
				                                 "INNER JOIN ProjectRoundUnit pru ON u.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
				                                 "WHERE u.ProjectRoundUserID = " + projectRoundUserID, "eFormSqlConnection");
				#endregion
				if (projectRoundID != 0 && projectRoundUnitID != 0)
				{
					bool first = false;
					int answerID = 0;
					if (formInstanceKey == "")
					{
						#region Create new AnswerID
						exec("INSERT INTO Answer (EndDT,ProjectRoundID, ProjectRoundUnitID, ProjectRoundUserID, ExtendedFirst) VALUES (GETDATE()," + projectRoundID + "," + projectRoundUnitID + "," + projectRoundUserID + "," + (first ? "1" : "NULL") + ")", "eFormSqlConnection");
						r = rs("SELECT TOP 1 AnswerID, REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-','') FROM Answer WHERE ProjectRoundUserID = " + projectRoundUserID + " ORDER BY AnswerID DESC", "eFormSqlConnection");
						if (r.Read())
						{
							answerID = r.GetInt32(0);
							formInstanceKey = r.GetString(1);
						}
						r.Close();
						exec("INSERT INTO UserProjectRoundUserAnswer (ProjectRoundUserID, AnswerKey, UserProfileID, AnswerID) VALUES (" + projectRoundUserID + ",'" + formInstanceKey.Replace("'","") + "'," + execIntScal("SELECT UserProfileID FROM [User] WHERE UserID = " + userID) + "," + answerID + ")");
					}
					else
					{
						answerID = execIntScal("SELECT AnswerID FROM Answer WHERE REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-','') = '" + formInstanceKey.Replace("'","") + "' AND ProjectRoundUserID = " + projectRoundUserID, "eFormSqlConnection");
					}
					#endregion
					if (answerID != 0)
					{
						int sessionID = 0;
						int optionType = execIntScal("SELECT o.OptionType FROM [Option] o INNER JOIN QuestionOption qo ON o.OptionID = qo.OptionID WHERE qo.QuestionID = " + questionID + " AND o.OptionID = " + optionID, "eFormSqlConnection");
						if (optionType != 0)
						{
							exec("UPDATE AnswerValue SET DeletedSessionID = " + sessionID + " WHERE AnswerID = " + answerID + " AND QuestionID = " + questionID + " AND OptionID = " + optionID + " AND DeletedSessionID IS NULL", "eFormSqlConnection");
						}
						#region Save new value
						switch (optionType)
						{
							case 1:
								{
									try
									{
										if (execIntScal("SELECT COUNT(*) FROM OptionComponents ocs WHERE ocs.OptionID = " + optionID + " AND ocs.OptionComponentID = " + Convert.ToInt32(answer), "eFormSqlConnection") == 0)
										{
											throw (new Exception());
										}
										exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueInt,CreatedSessionID) VALUES (" + answerID + "," + questionID + "," + optionID + "," + Convert.ToInt32(answer) + "," + sessionID + ")", "eFormSqlConnection");
									}
									catch (Exception) { formInstanceKey = ""; }
									break;
								}
							case 2:
								{
									exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueText,CreatedSessionID) VALUES (" + answerID + "," + questionID + "," + optionID + ",'" + answer.Replace("'", "''") + "'," + sessionID + ")", "eFormSqlConnection");
									break;
								}
							case 3:
								{
									goto case 1;
								}
							case 4:
								{
									try
									{
										decimal newValIns = Convert.ToDecimal(answer);
										exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueDecimal,CreatedSessionID) VALUES (" + answerID + "," + questionID + "," + optionID + "," + Convert.ToDecimal(answer).ToString().Replace(",", ".") + "," + sessionID + ")", "eFormSqlConnection");
									}
									catch (Exception) { formInstanceKey = ""; }
									break;
								}
							case 9:
								{
									try
									{
										int v = Convert.ToInt32(answer);
										if (v < 0 || v > 100)
											throw (new Exception());
										exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueInt,CreatedSessionID) VALUES (" + answerID + "," + questionID + "," + optionID + "," + Convert.ToInt32(answer) + "," + sessionID + ")", "eFormSqlConnection");
									}
									catch (Exception) { formInstanceKey = ""; }
									break;
								}
							default:
								formInstanceKey = "";
								break;
						}
						#endregion
					}
					else
					{
						formInstanceKey = "";
					}
				}
				return formInstanceKey;
			}
			return "";
		}
		[WebMethod(Description = "Set user form. Returns formInstanceKey if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public string UserSetFormInstance(string token,string formKey,FormQuestionAnswer[] fqa,int expirationMinutes)
		{
			string answerKey = "";

			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int projectRoundUnitID = 0, projectRoundUserID = 0;
				SqlDataReader r = rs("SELECT " +
				                     "spru.ProjectRoundUnitID, " +
				                     "upru.ProjectRoundUserID, " +
				                     "u.Email, " +
				                     "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') " +
				                     "FROM [User] u " +
				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
				                     "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
				                     "WHERE u.UserID = " + userID);
				while (r.Read())
				{
					if (formKey == r.GetString(3))
					{
						projectRoundUnitID = r.GetInt32(0);
						if (r.IsDBNull(1))
						{
							projectRoundUserID = createSurveyUser(userID, r.GetInt32(0), r.GetString(2));
						}
						else
						{
							projectRoundUserID = r.GetInt32(1);
						}
					}
				}
				r.Close();
				#region Fetch ProjectRoundID
				int projectRoundID = execIntScal("SELECT " +
				                                 "pru.ProjectRoundID " +
				                                 "FROM ProjectRoundUser u " +
				                                 "INNER JOIN ProjectRoundUnit pru ON u.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
				                                 "WHERE u.ProjectRoundUserID = " + projectRoundUserID, "eFormSqlConnection");
				#endregion
				if (projectRoundID != 0 && projectRoundUnitID != 0)
				{
					bool first = false;
					int answerID = 0;
					#region Create new AnswerID
					exec("INSERT INTO Answer (EndDT, ProjectRoundID, ProjectRoundUnitID, ProjectRoundUserID, ExtendedFirst) VALUES (GETDATE()," + projectRoundID + "," + projectRoundUnitID + "," + projectRoundUserID + "," + (first ? "1" : "NULL") + ")", "eFormSqlConnection");
					r = rs("SELECT TOP 1 AnswerID, REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-','') FROM Answer WHERE ProjectRoundUserID = " + projectRoundUserID + " ORDER BY AnswerID DESC", "eFormSqlConnection");
					if (r.Read())
					{
						answerID = r.GetInt32(0);
						answerKey = r.GetString(1);
					}
					r.Close();
					exec("INSERT INTO UserProjectRoundUserAnswer (ProjectRoundUserID, AnswerKey, UserProfileID, AnswerID) VALUES (" + projectRoundUserID + ",'" + answerKey.Replace("'", "") + "'," + execIntScal("SELECT UserProfileID FROM [User] WHERE UserID = " + userID) + "," + answerID + ")");
					#endregion
					if (answerID != 0)
					{
						foreach (FormQuestionAnswer f in fqa)
						{
							int sessionID = 0;
							int optionType = execIntScal("SELECT o.OptionType FROM [Option] o INNER JOIN QuestionOption qo ON o.OptionID = qo.OptionID WHERE qo.QuestionID = " + f.questionID + " AND o.OptionID = " + f.optionID, "eFormSqlConnection");
							#region Save new value
							switch (optionType)
							{
								case 1:
									{
										try
										{
											if (execIntScal("SELECT COUNT(*) FROM OptionComponents ocs WHERE ocs.OptionID = " + f.optionID + " AND ocs.OptionComponentID = " + Convert.ToInt32(f.answer), "eFormSqlConnection") == 0)
											{
												throw (new Exception());
											}
											exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueInt,CreatedSessionID) VALUES (" + answerID + "," + f.questionID + "," + f.optionID + "," + Convert.ToInt32(f.answer) + "," + sessionID + ")", "eFormSqlConnection");
										}
										catch (Exception) { answerKey = ""; }
										break;
									}
								case 2:
									{
										exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueText,CreatedSessionID) VALUES (" + answerID + "," + f.questionID + "," + f.optionID + ",'" + f.answer.Replace("'", "''") + "'," + sessionID + ")", "eFormSqlConnection");
										break;
									}
								case 3:
									{
										goto case 1;
									}
								case 4:
									{
										try
										{
											decimal newValIns = Convert.ToDecimal(f.answer);
											exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueDecimal,CreatedSessionID) VALUES (" + answerID + "," + f.questionID + "," + f.optionID + "," + Convert.ToDecimal(f.answer).ToString().Replace(",", ".") + "," + sessionID + ")", "eFormSqlConnection");
										}
										catch (Exception) { answerKey = ""; }
										break;
									}
								case 9:
									{
										try
										{
											int v = Convert.ToInt32(f.answer);
											if (v < 0 || v > 100)
												throw (new Exception());
											exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueInt,CreatedSessionID) VALUES (" + answerID + "," + f.questionID + "," + f.optionID + "," + Convert.ToInt32(f.answer) + "," + sessionID + ")", "eFormSqlConnection");
										}
										catch (Exception) { answerKey = ""; }
										break;
									}
								default:
									answerKey = "";
									break;
							}
							#endregion
						}
					}
				}
				return answerKey;
			}
			return answerKey;
		}
		[WebMethod(Description = "Deletes user form. Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public bool UserDeleteFormInstance(string token,string formInstanceKey,int eventID,int expirationMinutes)
		{
			int ret = 0;

			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				ret = exec("UPDATE UserProjectRoundUserAnswer SET " +
				           "ProjectRoundUserID = -ABS(ProjectRoundUserID), " +
				           "UserProfileID = -ABS(UserProfileID) " +
				           "WHERE AnswerKey = '" + formInstanceKey.Replace("'", "") + "' " +
				           "AND UserProjectRoundUserAnswerID = " + eventID);
			}
			return (ret > 0);
		}
		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates form questions. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public Question[] FormQuestionEnum(string token, int languageID, string formKey, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int projectRoundUnitID = 0, projectRoundUserID = 0;
				SqlDataReader r = rs("SELECT " +
				                     "spru.ProjectRoundUnitID, " +
				                     "upru.ProjectRoundUserID, " +
				                     "u.Email, " +
				                     "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
				                     "ISNULL(sprul.Nav,spru.Nav) AS Nav " +
				                     "FROM [User] u " +
				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
				                     "LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID AND sprul.LangID = " + languageID + " " +
				                     "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
				                     "WHERE u.UserID = " + userID);
				while (r.Read())
				{
					if (formKey == r.GetString(3))
					{
						projectRoundUnitID = r.GetInt32(0);
						if (r.IsDBNull(1))
						{
							projectRoundUserID = createSurveyUser(userID, r.GetInt32(0), r.GetString(2));
						}
						else
						{
							projectRoundUserID = r.GetInt32(1);
						}
					}
				}
				r.Close();
				bool first = false;
				#region Fetch SurveyID
				int surveyID = execIntScal("SELECT " +
				                           "dbo.cf_unitSurveyID(u.ProjectRoundUnitID) " +
				                           "FROM ProjectRoundUser u " +
				                           "INNER JOIN ProjectRoundUnit pru ON u.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
				                           "WHERE u.ProjectRoundUserID = " + projectRoundUserID, "eFormSqlConnection");
				#endregion

				int cx = execIntScal("SELECT " +
				                     "COUNT(DISTINCT sq.SurveyQuestionID) " +
				                     "FROM SurveyQuestion sq " +
				                     "INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
				                     "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
				                     "WHERE q.Box = 0 " +
				                     (!first ? "AND (sq.ExtendedFirst IS NULL OR sq.ExtendedFirst = 0) " : "") +
				                     "AND sq.SurveyID = " + surveyID, "eFormSqlConnection");

				Question[] ret = new Question[cx];

				cx = 0;
				r = rs("SELECT " +
				       "sq.SurveyQuestionID, " +	// 0
				       "sq.OptionsPlacement, " +	// 1
				       "q.FontFamily, " +			// 2
				       "ISNULL(sq.FontSize,q.FontSize), " +			// 3
				       "q.FontDecoration, " +		// 4
				       "q.FontColor, " +			// 5
				       "q.Underlined, " +			// 6
				       "ISNULL(sql.Question,ql.Question) AS Question, " +			// 7
				       "q.QuestionID, " +			// 8
				       "q.Box, " +					// 9
				       "sq.NoCount, " +			// 10
				       "sq.RestartCount, " +		// 11
				       "(SELECT COUNT(*) FROM SurveyQuestionOption sqo WHERE sqo.OptionPlacement = 1 AND sqo.SurveyQuestionID = sq.SurveyQuestionID), " +	// 12
				       "(SELECT COUNT(*) FROM SurveyQuestionOption sqo INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID WHERE qo.Hide = 1 AND sqo.SurveyQuestionID = sq.SurveyQuestionID), " +	// 13
				       "sq.NoBreak, " +			// 14
				       "sq.BreakAfterQuestion, " +	// 15
				       "s.FlipFlopBg, " +			// 16
				       "s.TwoColumns, " +          // 17
				       "(SELECT COUNT(*) FROM SurveyQuestionOption sqo WHERE sqo.SurveyQuestionID = sq.SurveyQuestionID) " +	// 18
				       "FROM Survey s " +
				       "INNER JOIN SurveyQuestion sq ON s.SurveyID = sq.SurveyID " +
				       "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
				       "INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + languageID + " " +
				       "LEFT OUTER JOIN SurveyQuestionLang sql ON sq.SurveyQuestionID = sql.SurveyQuestionID AND sql.LangID = ql.LangID " +
				       "WHERE q.Box = 0 AND s.SurveyID = " + surveyID + " " +
				       (!first ? "AND (sq.ExtendedFirst IS NULL OR sq.ExtendedFirst = 0) " : "") +
				       "ORDER BY sq.SortOrder", "eFormSqlConnection");
				while (r.Read())
				{
					if (r.GetInt32(18) > 0)
					{
						SqlDataReader r2 = rs("SELECT " +
						                      "qo.QuestionID, " +			// 0
						                      "qo.OptionID, " +			// 1
						                      "sqo.OptionPlacement, " +	// 2
						                      "o.OptionType, " +			// 3
						                      "o.Width, " +				// 4
						                      "ISNULL(sqo.Height,o.Height), " +				// 5
						                      "sqo.Forced, " +			// 6
						                      "o.InnerWidth, " +			// 7
						                      "qo.Hide, " +				// 8
						                      "o.BgColor, " +				// 9
						                      "(SELECT COUNT(*) FROM OptionComponents ocs WHERE ocs.OptionID = o.OptionID) " +    // 10
						                      "FROM SurveyQuestionOption sqo " +
						                      "INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " +
						                      "INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
						                      "WHERE sqo.SurveyQuestionID = " + r.GetInt32(0) + " " +
						                      "ORDER BY sqo.SortOrder", "eFormSqlConnection");
						if (r2.Read())
						{
							ret[cx].QuestionID = r.GetInt32(8);
							ret[cx].SortOrder = (cx + 1);
							ret[cx].QuestionText = (!r.IsDBNull(7) ? r.GetString(7) : "");

							ret[cx].OptionID = r2.GetInt32(1);
							ret[cx].QuestionType = (QuestionTypes)r2.GetInt32(3);

							if (r2.GetInt32(3) == 1 || r2.GetInt32(3) == 9)
							{
								Answer[] a = new Answer[r2.GetInt32(10)];
								int dx = 0;

								SqlDataReader r3 = rs("SELECT " +
								                      "ocs.OptionComponentID, " +
								                      "ocl.Text " +
								                      "FROM OptionComponents ocs " +
								                      "INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
								                      "INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + languageID + " " +
								                      "WHERE ocs.OptionID = " + r2.GetInt32(1) + " " +
								                      "ORDER BY ocs.SortOrder", "eFormSqlConnection");
								while (r3.Read())
								{
									a[dx].AnswerText = (!r3.IsDBNull(1) ? r3.GetString(1) : "");
									switch (r2.GetInt32(3))
									{
										case 1:
											a[dx].AnswerID = r3.GetInt32(0);
											a[dx].SortOrder = dx;
											break;
										case 9:
											a[dx].SortOrder = dx;
											a[dx].AnswerValue = (dx * (100 / (r2.GetInt32(10) - 1)));
											break;
									}
									dx++;
								}
								r3.Close();

								ret[cx].AnswerOptions = a;
							}

							cx++;
						}
						r2.Close();
					}
				}
				r.Close();

				return ret;
			}
			return (new Question[0]);
		}
		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates forms. Note that form keys are not static (same form can have different key for different users). Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public Form[] FormEnum(string token, int languageID, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int formCount = execIntScal("SELECT " +
				                            "COUNT(*) " +
				                            "FROM [User] u " +
				                            "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				                            "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
				                            "LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID AND sprul.LangID = " + languageID + " " +
				                            "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
				                            "WHERE u.UserID = " + userID);

				Form[] ret = new Form[formCount];
				int cx = 0;
				SqlDataReader r = rs("SELECT " +
				                     "spru.ProjectRoundUnitID, " +
				                     "upru.ProjectRoundUserID, " +
				                     "u.Email, " +
				                     "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
				                     "ISNULL(sprul.Nav,spru.Nav) AS Nav " +
				                     "FROM [User] u " +
				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
				                     "LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID AND sprul.LangID = " + languageID + " " +
				                     "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
				                     "WHERE u.UserID = " + userID + " " +
				                     "ORDER BY spru.SortOrder");
				while (r.Read())
				{
					if (r.IsDBNull(1))
					{
						createSurveyUser(userID, r.GetInt32(0), r.GetString(2));
					}
					ret[cx].formKey = r.GetString(3);
					ret[cx].form = r.GetString(4);
					cx++;
				}
				r.Close();

				return ret;
			}
			return (new Form[0]);
		}
		[WebMethod(Description = "Delete measure. Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public bool UserDeleteMeasure(string token,int eventID,int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				if(exec("UPDATE UserMeasure SET DeletedDT = GETDATE() WHERE UserID = " + userID + " AND UserMeasureID = " + eventID) > 0)
					return true;
			}
			return false;
		}
		[WebMethod(Description = "Set measure having one or two components (use component ID = 0 if no secondary component). Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public bool UserSetMeasureParameterized(string token,DateTime dateTime,int measureID,int measureComponentID_1,string value_1,int measureComponentID_2,string value_2,int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				bool ok = true;
				int userProfileID = execIntScal("SELECT UserProfileID FROM [User] WHERE UserID = " + userID);
				#region Create new UserMeasure
				exec("INSERT INTO UserMeasure (" +
				     "UserID, " +
				     "CreatedDT, " +
				     "DT," +
				     "UserProfileID" +
				     ") VALUES (" +
				     "" + userID + "," +
				     "GETDATE()," +
				     "'" + dateTime.ToString("yyyy-MM-dd HH:mm") + "'," +
				     "" + userProfileID + "" +
				     ")");
				#endregion
				#region Fetch new UserMeasure
				int userMeasureID = execIntScal("SELECT TOP 1 " +
				                                "UserMeasureID " +
				                                "FROM UserMeasure " +
				                                "WHERE UserID = " + userID + " " +
				                                "ORDER BY UserMeasureID DESC");
				#endregion
				if (measureComponentID_1 != 0)
				{
					int questionType = execIntScal("SELECT " +
					                               "mc.Type " +
					                               "FROM MeasureComponent mc " +
					                               "WHERE mc.MeasureID = " + measureID + " AND mc.MeasureComponentID = " + measureComponentID_1);
					switch (questionType)
					{
						case 4:
							try
							{
								Convert.ToDecimal(value_1.Replace("'", "").Replace(".", ","));
								exec("INSERT INTO UserMeasureComponent (" +
								     "UserMeasureID, " +
								     "MeasureComponentID, " +
								     "ValDec" +
								     ") VALUES (" +
								     "" + userMeasureID + "," +
								     "" + measureComponentID_1 + "," +
								     "" + value_1.Replace("'", "").Replace(",", ".") +
								     ")");
							}
							catch (Exception) { ok = false; }
							break;
						default:
							ok = false; break;
					}
				}
				if (measureComponentID_2 != 0)
				{
					int questionType = execIntScal("SELECT " +
					                               "mc.Type " +
					                               "FROM MeasureComponent mc " +
					                               "WHERE mc.MeasureID = " + measureID + " AND mc.MeasureComponentID = " + measureComponentID_2);
					switch (questionType)
					{
						case 4:
							try
							{
								Convert.ToDecimal(value_2.Replace("'", "").Replace(".", ","));
								exec("INSERT INTO UserMeasureComponent (" +
								     "UserMeasureID, " +
								     "MeasureComponentID, " +
								     "ValDec" +
								     ") VALUES (" +
								     "" + userMeasureID + "," +
								     "" + measureComponentID_2 + "," +
								     "" + value_2.Replace("'", "").Replace(",", ".") +
								     ")");
							}
							catch (Exception) { ok = false; }
							break;
						default:
							ok = false; break;
					}
				}
				return ok;
			}
			return false;
		}
		[WebMethod(Description = "Set measure. Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public bool UserSetMeasure(string token,DateTime dateTime,int measureID,UserMeasureComponent[] umc,int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				bool ok = true;
				int userProfileID = execIntScal("SELECT UserProfileID FROM [User] WHERE UserID = " + userID);
				#region Create new UserMeasure
				exec("INSERT INTO UserMeasure (" +
				     "UserID, " +
				     "CreatedDT, " +
				     "DT," +
				     "UserProfileID" +
				     ") VALUES (" +
				     "" + userID + "," +
				     "GETDATE()," +
				     "'" + dateTime.ToString("yyyy-MM-dd HH:mm") + "'," +
				     "" + userProfileID + "" +
				     ")");
				#endregion
				#region Fetch new UserMeasure
				int userMeasureID = execIntScal("SELECT TOP 1 " +
				                                "UserMeasureID " +
				                                "FROM UserMeasure " +
				                                "WHERE UserID = " + userID + " " +
				                                "ORDER BY UserMeasureID DESC");
				#endregion
				foreach(UserMeasureComponent a in umc)
				{
					int questionType = execIntScal("SELECT " +
					                               "mc.Type " +
					                               "FROM MeasureComponent mc " +
					                               "WHERE mc.MeasureID = " + measureID + " AND mc.MeasureComponentID = " + a.MeasureComponentID);
					switch (questionType)
					{
						case 4:
							try
							{
								Convert.ToDecimal(a.value.Replace("'", "").Replace(".", ","));
								exec("INSERT INTO UserMeasureComponent (" +
								     "UserMeasureID, " +
								     "MeasureComponentID, " +
								     "ValDec" +
								     ") VALUES (" +
								     "" + userMeasureID + "," +
								     "" + a.MeasureComponentID + "," +
								     "" + a.value.Replace("'", "").Replace(",", ".") +
								     ")");
							}
							catch (Exception) { ok = false; }
							break;
						default:
							ok = false; break;
					}
				}
				return ok;
			}
			return false;
		}
		[WebMethod(Description = "Get user reminder. " +
		           "Type 0 = Never, 1 = Regularly, 2 = Inactivity. " +
		           "AutoLoginLink 0 = No, 1 = Constant, 2 = ExpireWhenUsed. " +
		           "SendAtHour = hour of day to send reminder, expressed as integer between 6-22. " +
		           "Regularity 1 = Daily, 2 = Weekly, 3 = Monthly. " +
		           "RegularityDailyMonday...Sunday = false/true. " +
		           "RegularityWeeklyDay 1 = Monday, ..., 7 = Sunday. " +
		           "RegularityWeeklyEvery 1 = Every week, 2 = Every other week, 3 = Every third week. " +
		           "RegularityMonthlyWeekNr 1 = First, 2 = Second, 3 = Third, 4 = Fourth. " +
		           "RegularityMonthlyDay 1 = Monday, ..., 7 = Sunday. " +
		           "RegularityMonthlyEvery 1 = Every, 2 = Every other, 3 = Every third, 6 = Every sixth. " +
		           "InactivityCount = number of days/weeks/months depending on period, expressed as integer between 1-6. " +
		           "InactivityPeriod 1 = Day, 7 = Week, 30 = Month. " +
		           "Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public Reminder UserGetReminder(string token,int expirationMinutes)
		{
			Reminder ret = new Reminder();

			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				SqlDataReader r = rs("SELECT " +
				                     "u.Username, " +		// 0
				                     "s.LoginDays, " +		// 1
				                     "u.Reminder, " +		// 2
				                     "u.ReminderType, " +	// 3
				                     "u.ReminderLink, " +	// 4
				                     "u.ReminderSettings, " +// 5
				                     "u.Email " +			// 6
				                     "FROM [User] u " +
				                     "LEFT OUTER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				                     "WHERE u.UserID = " + userID);
				if (r.Read())
				{
					if (!r.IsDBNull(2))
					{
						ret.type = (r.GetInt32(2) == 0 || r.IsDBNull(3) ? 0 : r.GetInt32(3));
						ret.autoLoginLink = (r.IsDBNull(4) ? 0 : r.GetInt32(4));

						string[] settings = (r.IsDBNull(5) ? "" : r.GetString(5)).Split(':');

						switch (r.IsDBNull(3) ? 0 : r.GetInt32(3))
						{
							case 1:
								ret.sendAtHour = Convert.ToInt32(settings[0]);
								ret.regularity = Convert.ToInt32(settings[1]);
								switch (Convert.ToInt32(settings[1]))
								{
									case 1:
										string[] days = settings[2].Split(',');
										foreach (string day in days)
										{
											switch (Convert.ToInt32(day))
											{
													case 1: ret.regularityDailyMonday = true; break;
													case 2: ret.regularityDailyTuesday = true; break;
													case 3: ret.regularityDailyWednesday = true; break;
													case 4: ret.regularityDailyThursday = true; break;
													case 5: ret.regularityDailyFriday = true; break;
													case 6: ret.regularityDailySaturday = true; break;
													case 7: ret.regularityDailySunday = true; break;
											}
										}
										break;
									case 2:
										ret.regularityWeeklyDay = Convert.ToInt32(settings[2]);
										ret.regularityWeeklyEvery = Convert.ToInt32(settings[3]);
										break;
									case 3:
										ret.regularityMonthlyWeekNr = Convert.ToInt32(settings[2]);
										ret.regularityMonthlyDay = Convert.ToInt32(settings[3]);
										ret.regularityMonthlyEvery = Convert.ToInt32(settings[4]);
										break;
								}
								break;
							case 2:
								ret.sendAtHour = Convert.ToInt32(settings[0]);
								ret.inactivityCount = Convert.ToInt32(settings[1]);
								ret.inactivityPeriod = Convert.ToInt32(settings[2]);
								break;
						}
					}
				}
				r.Close();
			}
			return ret;
		}
		[WebMethod(Description = "Set user reminder. Returns true if successful. " +
		           "Type 0 = Never, 1 = Regularly, 2 = Inactivity. " +
		           "AutoLoginLink 0 = No, 1 = Constant, 2 = ExpireWhenUsed. " +
		           "SendAtHour = hour of day to send reminder, expressed as integer between 6-22. " +
		           "Regularity 1 = Daily, 2 = Weekly, 3 = Monthly. " +
		           "RegularityDailyMonday...Sunday = false/true. " +
		           "RegularityWeeklyDay 1 = Monday, ..., 7 = Sunday. " +
		           "RegularityWeeklyEvery 1 = Every week, 2 = Every other week, 3 = Every third week. " +
		           "RegularityMonthlyWeekNr 1 = First, 2 = Second, 3 = Third, 4 = Fourth. " +
		           "RegularityMonthlyDay 1 = Monday, ..., 7 = Sunday. " +
		           "RegularityMonthlyEvery 1 = Every, 2 = Every other, 3 = Every third, 6 = Every sixth. " +
		           "InactivityCount = number of days/weeks/months depending on period, expressed as integer between 1-6. " +
		           "InactivityPeriod 1 = Day, 7 = Week, 30 = Month. " +
		           "Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public bool UserSetReminder(string token,Reminder reminder,int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				if (reminder.type == 0)
				{
					exec("UPDATE [User] SET " +
					     "Reminder = 0, " +
					     "ReminderLink = " + reminder.autoLoginLink + ", " +
					     "ReminderType = " + reminder.type + ", " +
					     "ReminderNextSend = NULL " +
					     "WHERE UserID = " + userID);
				}
				else
				{
					string settings = reminder.sendAtHour.ToString();
					switch (reminder.type)
					{
						case 1:
							settings += ":" + reminder.regularity.ToString();
							switch (reminder.regularity)
							{
								case 1:
									string days = "";
									if (reminder.regularityDailyMonday) days += (days != "" ? "," : "") + "1";
									if (reminder.regularityDailyTuesday) days += (days != "" ? "," : "") + "2";
									if (reminder.regularityDailyWednesday) days += (days != "" ? "," : "") + "3";
									if (reminder.regularityDailyThursday) days += (days != "" ? "," : "") + "4";
									if (reminder.regularityDailyFriday) days += (days != "" ? "," : "") + "5";
									if (reminder.regularityDailySaturday) days += (days != "" ? "," : "") + "6";
									if (reminder.regularityDailySunday) days += (days != "" ? "," : "") + "7";
									settings += ":" + (days != "" ? days : "1");
									break;
								case 2:
									settings += ":" + reminder.regularityWeeklyDay.ToString();
									settings += ":" + reminder.regularityWeeklyEvery.ToString();
									break;
								case 3:
									settings += ":" + reminder.regularityMonthlyWeekNr.ToString();
									settings += ":" + reminder.regularityMonthlyDay.ToString();
									settings += ":" + reminder.regularityMonthlyEvery.ToString();
									break;
							}
							break;
						case 2:
							settings += ":" + reminder.inactivityCount.ToString();
							settings += ":" + reminder.inactivityPeriod.ToString();
							break;

					}
					exec("UPDATE [User] SET " +
					     (reminder.autoLoginLink == 2 ? "UserKey = NEWID(), " : "") +
					     "Reminder = 1, " +
					     "ReminderLink = " + reminder.autoLoginLink + ", " +
					     "ReminderType = " + reminder.type + ", " +
					     "ReminderSettings = '" + settings.Replace("'", "") + "', " +
					     "ReminderNextSend = '" + nextReminderSend(reminder.type, settings.Split(':'), DateTime.Now, DateTime.Now) + "' " +
					     "WHERE UserID = " + userID);
				}
				return true;
			}
			return false;
		}
		[WebMethod(Description = "Set user reminder. Returns true if successful. " +
		           "Type 0 = Never, 1 = Regularly, 2 = Inactivity. " +
		           "AutoLoginLink 0 = No, 1 = Constant, 2 = ExpireWhenUsed. " +
		           "SendAtHour = hour of day to send reminder, expressed as integer between 6-22. " +
		           "Regularity 1 = Daily, 2 = Weekly, 3 = Monthly. " +
		           "RegularityDailyMonday...Sunday = false/true. " +
		           "RegularityWeeklyDay 1 = Monday, ..., 7 = Sunday. " +
		           "RegularityWeeklyEvery 1 = Every week, 2 = Every other week, 3 = Every third week. " +
		           "RegularityMonthlyWeekNr 1 = First, 2 = Second, 3 = Third, 4 = Fourth. " +
		           "RegularityMonthlyDay 1 = Monday, ..., 7 = Sunday. " +
		           "RegularityMonthlyEvery 1 = Every, 2 = Every other, 3 = Every third, 6 = Every sixth. " +
		           "InactivityCount = number of days/weeks/months depending on period, expressed as integer between 1-6. " +
		           "InactivityPeriod 1 = Day, 7 = Week, 30 = Month. " +
		           "Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public bool UserSetReminderParameterized(string token, int type, int autoLoginLink, int sendAtHour, int regularity,bool regularityDailyMonday,bool regularityDailyTuesday,bool regularityDailyWednesday,bool regularityDailyThursday,bool regularityDailyFriday,bool regularityDailySaturday,bool regularityDailySunday,int regularityWeeklyDay,int regularityWeeklyEvery,int regularityMonthlyWeekNr,int regularityMonthlyDay,int regularityMonthlyEvery,int inactivityCount,int inactivityPeriod,int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				if (type == 0)
				{
					exec("UPDATE [User] SET " +
					     "Reminder = 0, " +
					     "ReminderLink = " + autoLoginLink + ", " +
					     "ReminderType = " + type + ", " +
					     "ReminderNextSend = NULL " +
					     "WHERE UserID = " + userID);
				}
				else
				{
					string settings = sendAtHour.ToString();
					switch (type)
					{
						case 1:
							settings += ":" + regularity.ToString();
							switch (regularity)
							{
								case 1:
									string days = "";
									if (regularityDailyMonday) days += (days != "" ? "," : "") + "1";
									if (regularityDailyTuesday) days += (days != "" ? "," : "") + "2";
									if (regularityDailyWednesday) days += (days != "" ? "," : "") + "3";
									if (regularityDailyThursday) days += (days != "" ? "," : "") + "4";
									if (regularityDailyFriday) days += (days != "" ? "," : "") + "5";
									if (regularityDailySaturday) days += (days != "" ? "," : "") + "6";
									if (regularityDailySunday) days += (days != "" ? "," : "") + "7";
									settings += ":" + (days != "" ? days : "1");
									break;
								case 2:
									settings += ":" + regularityWeeklyDay.ToString();
									settings += ":" + regularityWeeklyEvery.ToString();
									break;
								case 3:
									settings += ":" + regularityMonthlyWeekNr.ToString();
									settings += ":" + regularityMonthlyDay.ToString();
									settings += ":" + regularityMonthlyEvery.ToString();
									break;
							}
							break;
						case 2:
							settings += ":" + inactivityCount.ToString();
							settings += ":" + inactivityPeriod.ToString();
							break;

					}
					exec("UPDATE [User] SET " +
					     (autoLoginLink == 2 ? "UserKey = NEWID(), " : "") +
					     "Reminder = 1, " +
					     "ReminderLink = " + autoLoginLink + ", " +
					     "ReminderType = " + type + ", " +
					     "ReminderSettings = '" + settings.Replace("'", "") + "', " +
					     "ReminderNextSend = '" + nextReminderSend(type, settings.Split(':'), DateTime.Now, DateTime.Now) + "' " +
					     "WHERE UserID = " + userID);
				}
				return true;
			}
			return false;
		}
		[WebMethod(Description = "Enumerates measurements. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public Measure[] MeasureEnum(string token, int measureCategoryID, int languageID, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int cx = execIntScal("SELECT COUNT(*) " +
				                     "FROM Measure m " +
				                     "INNER JOIN MeasureCategory mc ON m.MeasureCategoryID = mc.MeasureCategoryID " +
				                     "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + languageID + " " +
				                     "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
				                     "WHERE mc.SPRUID IS NULL " + (measureCategoryID != 0 ? "AND m.MeasureCategoryID = " + measureCategoryID + " " : "") +
				                     "");
				Measure[] ret = new Measure[cx];
				cx = 0;

				SqlDataReader r = rs("SELECT " +
				                     "m.MeasureID, " +
				                     "ISNULL(ml.Measure,m.Measure), " +
				                     "(SELECT COUNT(*) FROM MeasureComponent mc WHERE mc.MeasureID = m.MeasureID), " +
				                     "m.MoreInfo, " +
				                     "mc.MeasureCategoryID, " +
				                     "ISNULL(mcl.MeasureCategory,mc.MeasureCategory) " +
				                     "FROM Measure m " +
				                     "INNER JOIN MeasureCategory mc ON m.MeasureCategoryID = mc.MeasureCategoryID " +
				                     "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + languageID + " " +
				                     "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
				                     "WHERE mc.SPRUID IS NULL " + (measureCategoryID != 0 ? "AND m.MeasureCategoryID = " + measureCategoryID + " " : "") +
				                     "ORDER BY m.MeasureCategoryID, m.SortOrder");
				while (r.Read())
				{
					ret[cx].measureID = r.GetInt32(0);
					ret[cx].measure = r.GetString(1);
					ret[cx].componentCount = r.GetInt32(2);
					if (!r.IsDBNull(3))
					{
						ret[cx].moreInfo = r.GetString(3);
					}
					ret[cx].measureCategoryID = r.GetInt32(4);
					ret[cx].measureCategory = r.GetString(5);

					MeasureComponent[] mc = new MeasureComponent[r.GetInt32(2)];

					int dx = 0;
					SqlDataReader r2 = rs("SELECT " +
					                      "mc.MeasureComponentID, " +
					                      "ISNULL(mcl.MeasureComponent,mc.MeasureComponent), " +
					                      "mc.Type, " +
					                      "ISNULL(mcl.Unit,mc.Unit), " +
					                      "mc.Inherit, " +
					                      "mc.AutoScript, " +            // 5
					                      "(SELECT COUNT(*) FROM MeasureComponentPart mcp WHERE mcp.MeasureComponentPart = mc.MeasureComponentID), " +
					                      "mc.Decimals " +
					                      "FROM MeasureComponent mc " +
					                      "LEFT OUTER JOIN MeasureComponentLang mcl ON mc.MeasureComponentID = mcl.MeasureComponentID AND mcl.LangID = " + languageID + " " +
					                      "WHERE mc.MeasureID = " + r.GetInt32(0) + " " +
					                      "ORDER BY mc.SortOrder");
					while (r2.Read())
					{
						mc[dx].measureComponentID = r2.GetInt32(0);
						mc[dx].measureComponent = r2.GetString(1);
						mc[dx].questionType = (QuestionTypes)r2.GetInt32(2);
						if (!r2.IsDBNull(3))
						{
							mc[dx].unit = r2.GetString(3);
						}
						mc[dx].inherited = (!r2.IsDBNull(4) && r2.GetInt32(4) == 1);
						if (r2.GetInt32(4) == 1)
						{
							string val = "";
							SqlDataReader r3 = rs("SELECT TOP 1 c.ValDec FROM UserMeasureComponent c INNER JOIN UserMeasure m ON c.UserMeasureID = m.UserMeasureID WHERE c.MeasureComponentID = " + r2.GetInt32(0) + " AND m.UserID = " + userID + " AND m.DeletedDT IS NULL ORDER BY m.DT DESC");
							if (r3.Read())
							{
								val += Math.Round(r3.GetDecimal(0), r2.GetInt32(7));
							}
							r3.Close();
							mc[dx].inheritedValue = val;
						}
						mc[dx].hasAutoCalculateChildren = (r2.GetInt32(6) > 0);
						mc[dx].isAutoCalculated = (!r2.IsDBNull(5));
						if (!r2.IsDBNull(5))
						{
							string scr = "";
							SqlDataReader r3 = rs("SELECT p.MeasureComponentPart, c.MeasureID FROM MeasureComponentPart p INNER JOIN MeasureComponent c ON p.MeasureComponentPart = c.MeasureComponentID WHERE p.MeasureComponentID = " + r2.GetInt32(0) + " ORDER BY p.SortOrder");
							while (r3.Read())
							{
								scr += (scr != "" ? "," : "") + "'M" + r3.GetInt32(1) + "C" + r3.GetInt32(0) + "'";
							}
							r3.Close();
							mc[dx].autoCalculateScript = "function MCPS" + r2.GetInt32(0) + "(){" + r2.GetString(5) + "}function MCP" + r2.GetInt32(0) + "(){document.forms[0].M" + r.GetInt32(0) + "C" + r2.GetInt32(0) + ".value=MCPS" + r2.GetInt32(0) + "(" + scr + ");}";
						}
						mc[dx].decimals = r2.GetInt32(7);
						string auto = "";
						if (r2.GetInt32(6) != 0)
						{
							SqlDataReader r3 = rs("SELECT MeasureComponentID FROM MeasureComponentPart WHERE MeasureComponentPart = " + r2.GetInt32(0));
							while (r3.Read())
							{
								auto += "MCP" + r3.GetInt32(0) + "();";
							}
							r3.Close();
						}
						if (!r2.IsDBNull(5))
						{
							auto += "MCP" + r2.GetInt32(0) + "();";
						}
						if (auto != "")
						{
							mc[dx].triggerScript = auto;
						}
						dx++;
					}
					r2.Close();

					ret[cx].measureComponents = mc;

					cx++;
				}
				r.Close();

				return ret;
			}
			return (new Measure[0]);
		}
		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates measurement categories. Set measureTypeID = 0 for all. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public MeasureCategory[] MeasureCategoryEnum(string token, int measureTypeID, int languageID, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int sponsorID = execIntScal("SELECT SponsorID FROM [User] WHERE UserID = " + userID);

				int cx = execIntScal("SELECT SUM(tmp.CX) FROM (" +
				                     "SELECT COUNT(*) AS CX " +
				                     "FROM MeasureCategory mc " +
				                     "INNER JOIN MeasureType mt ON mc.MeasureTypeID = mt.MeasureTypeID " +
				                     "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
				                     "LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + languageID + " " +
				                     "WHERE mc.SPRUID IS NULL " +
				                     (measureTypeID != 0 ? "AND mc.MeasureTypeID = " + measureTypeID + " " : "") +
				                     //"UNION ALL " +
				                     //"SELECT COUNT(*) AS CX " +
				                     //"FROM MeasureCategory mc " +
				                     //"INNER JOIN MeasureType mt ON mc.MeasureTypeID = mt.MeasureTypeID " +
				                     //"INNER JOIN SponsorProjectRoundUnit spru ON mc.SPRUID = spru.SponsorProjectRoundUnitID AND spru.SponsorID = " + sponsorID + " " +
				                     //"LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
				                     //"LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + languageID + " " +
				                     //(measureTypeID != 0 ? "WHERE mc.MeasureTypeID = " + measureTypeID + " " : "") +
				                     ") tmp");
				MeasureCategory[] ret = new MeasureCategory[cx];
				cx = 0;

				SqlDataReader r = rs("SELECT " +
				                     "mc.MeasureCategoryID, " +       // 0
				                     "ISNULL(mcl.MeasureCategory,mc.MeasureCategory), " +
				                     "NULL, " +
				                     "mc.SortOrder AS SO, " +
				                     "mt.MeasureTypeID, " +
				                     "ISNULL(mtl.MeasureType,mt.MeasureType) " +
				                     "FROM MeasureCategory mc " +
				                     "INNER JOIN MeasureType mt ON mc.MeasureTypeID = mt.MeasureTypeID " +
				                     "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
				                     "LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + languageID + " " +
				                     "WHERE mc.SPRUID IS NULL " +
				                     (measureTypeID != 0 ? "AND mc.MeasureTypeID = " + measureTypeID + " " : "") +
				                     //"UNION ALL " +
				                     //"SELECT " +
				                     //"mc.MeasureCategoryID, " +       // 0
				                     //"ISNULL(mcl.MeasureCategory,mc.MeasureCategory), " +
				                     //"REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
				                     //"mc.SortOrder AS SO, " +
				                     //"mt.MeasureTypeID, " +
				                     //"ISNULL(mtl.MeasureType,mt.MeasureType) " +
				                     //"FROM MeasureCategory mc " +
				                     //"INNER JOIN MeasureType mt ON mc.MeasureTypeID = mt.MeasureTypeID " +
				                     //"INNER JOIN SponsorProjectRoundUnit spru ON mc.SPRUID = spru.SponsorProjectRoundUnitID AND spru.SponsorID = " + sponsorID + " " +
				                     //"LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
				                     //"LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + languageID + " " +
				                     //(measureTypeID != 0 ? "WHERE mc.MeasureTypeID = " + measureTypeID + " " : "") +
				                     "ORDER BY SO");
				while (r.Read())
				{
					ret[cx].measureCategoryID = r.GetInt32(0);
					ret[cx].measureCategory = r.GetString(1);
					ret[cx].measureTypeID = r.GetInt32(4);
					ret[cx].measureType = r.GetString(5);
					cx++;
				}
				r.Close();

				return ret;
			}
			return (new MeasureCategory[0]);
		}
		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates measurement types. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public MeasureType[] MeasureTypeEnum(string token, int languageID, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int cx = execIntScal("SELECT COUNT(*) " +
				                     "FROM MeasureType mt " +
				                     "LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + languageID + " " +
				                     "WHERE Active = 1 " +
				                     "AND (SELECT COUNT(*) FROM MeasureCategory mc WHERE mt.MeasureTypeID = mc.MeasureTypeID AND mc.SPRUID IS NULL) > 0 " +
				                     "");
				MeasureType[] ret = new MeasureType[cx];
				cx = 0;

				SqlDataReader r = rs("SELECT " +
				                     "mt.MeasureTypeID, " +       // 0
				                     "ISNULL(mtl.MeasureType,mt.MeasureType) " +
				                     "FROM MeasureType mt " +
				                     "LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + languageID + " " +
				                     "WHERE Active = 1 " +
				                     "AND (SELECT COUNT(*) FROM MeasureCategory mc WHERE mt.MeasureTypeID = mc.MeasureTypeID AND mc.SPRUID IS NULL) > 0 " +
				                     "ORDER BY mt.SortOrder");
				while (r.Read())
				{
					ret[cx].measureTypeID = r.GetInt32(0);
					ret[cx].measureType = r.GetString(1);
					cx++;
				}
				r.Close();

				return ret;
			}
			return (new MeasureType[0]);
		}
		[WebMethod(Description = "Set or update a calendar entry. Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public bool CalendarUpdate(string token, Mood mood, string note, DateTime date, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				bool oldNoteIdentical = false;
				SqlDataReader r = rs("SELECT " +
				                     "DiaryID, " +
				                     "DiaryNote, " +
				                     "Mood " +
				                     "FROM Diary " +
				                     "WHERE DeletedDT IS NULL " +
				                     "AND DiaryDate = '" + date.ToString("yyyy-MM-dd") + "' " +
				                     "AND UserID = " + userID);
				if (r.Read())
				{
					if ((r.IsDBNull(1) ? "" : r.GetString(1)) != note || (r.IsDBNull(2) ? 0 : r.GetInt32(2)) != (int)mood)
					{
						exec("UPDATE Diary SET DeletedDT = GETDATE() WHERE DiaryID = " + r.GetInt32(0));
					}
					else
					{
						oldNoteIdentical = true;
					}
				}
				r.Close();
				if ((note != "" || mood != 0) && !oldNoteIdentical)
				{
					exec("INSERT INTO Diary (DiaryNote, DiaryDate, UserID, Mood) VALUES ('" + note.Replace("'", "''") + "','" + date.ToString("yyyy-MM-dd") + "'," + userID + "," + (int)mood + ")");
				}
				return true;
			}
			return false;
		}
		[WebMethod(Description = "Gets calendar info. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public Calendar[] CalendarEnum(string token, DateTime fromDT, DateTime toDT, int languageID, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int cx = execIntScal("SELECT COUNT(DISTINCT tmp.M) FROM (" +
				                     "SELECT " +
				                     "dbo.cf_yearMonthDay(um.DT) AS M " +
				                     "FROM UserMeasure um " +
				                     "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
				                     "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
				                     "INNER JOIN Measure m ON mc.MeasureID = m.MeasureID " +
				                     "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + languageID + " " +
				                     "WHERE mc.ShowInList = 1 AND um.DeletedDT IS NULL AND um.UserID = " + userID + " " +
				                     "AND dbo.cf_yearMonthDay(um.DT) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
				                     "AND dbo.cf_yearMonthDay(um.DT) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +

				                     "UNION ALL " +

				                     "SELECT " +
				                     "dbo.cf_yearMonthDay(es.DateTime) AS M " +
				                     "FROM ExerciseStats es " +
				                     "INNER JOIN ExerciseVariantLang evl ON es.ExerciseVariantLangID = evl.ExerciseVariantLangID " +
				                     "INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
				                     "INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
				                     "LEFT OUTER JOIN ExerciseLang el ON e.ExerciseID = el.ExerciseID AND el.Lang = " + (languageID - 1) + " " +
				                     "WHERE es.UserID = " + userID + " " +
				                     "AND dbo.cf_yearMonthDay(es.DateTime) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
				                     "AND dbo.cf_yearMonthDay(es.DateTime) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +

				                     "UNION ALL " +

				                     "SELECT " +
				                     "dbo.cf_yearMonthDay(uprua.DT) AS M " +
				                     "FROM UserProjectRoundUser upru " +
				                     "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
				                     "INNER JOIN [User] u ON upru.UserID = u.UserID " +
				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				                     "INNER JOIN SponsorProjectRoundUnit spru ON upru.ProjectRoundUnitID = spru.ProjectRoundUnitID AND s.SponsorID = spru.SponsorID " +
				                     "INNER JOIN MeasureCategory mc ON spru.SponsorProjectRoundUnitID = mc.SPRUID " +
				                     "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
				                     "WHERE upru.UserID = " + userID + " " +
				                     "AND dbo.cf_yearMonthDay(uprua.DT) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
				                     "AND dbo.cf_yearMonthDay(uprua.DT) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +

				                     "UNION ALL " +

				                     "SELECT " +
				                     "dbo.cf_yearMonthDay(d.DiaryDate) AS M " +
				                     "FROM Diary d " +
				                     "WHERE d.UserID = " + userID + " " +
				                     "AND dbo.cf_yearMonthDay(d.DiaryDate) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
				                     "AND dbo.cf_yearMonthDay(d.DiaryDate) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +
				                     ") tmp");

				Calendar[] ret = new Calendar[cx];
				cx = 0;

				DateTime oldDT = DateTime.MinValue;
				SqlDataReader r = rs("SELECT DISTINCT " +
				                     "dbo.cf_yearMonthDay(um.DT) AS M " +
				                     "FROM UserMeasure um " +
				                     "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
				                     "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
				                     "INNER JOIN Measure m ON mc.MeasureID = m.MeasureID " +
				                     "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + languageID + " " +
				                     "WHERE mc.ShowInList = 1 AND um.DeletedDT IS NULL AND um.UserID = " + userID + " " +
				                     "AND dbo.cf_yearMonthDay(um.DT) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
				                     "AND dbo.cf_yearMonthDay(um.DT) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +

				                     "UNION ALL " +

				                     "SELECT DISTINCT " +
				                     "dbo.cf_yearMonthDay(es.DateTime) AS M " +
				                     "FROM ExerciseStats es " +
				                     "INNER JOIN ExerciseVariantLang evl ON es.ExerciseVariantLangID = evl.ExerciseVariantLangID " +
				                     "INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
				                     "INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
				                     "LEFT OUTER JOIN ExerciseLang el ON e.ExerciseID = el.ExerciseID AND el.Lang = " + (languageID - 1) + " " +
				                     "WHERE es.UserID = " + userID + " " +
				                     "AND dbo.cf_yearMonthDay(es.DateTime) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
				                     "AND dbo.cf_yearMonthDay(es.DateTime) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +

				                     "UNION ALL " +

				                     "SELECT DISTINCT " +
				                     "dbo.cf_yearMonthDay(uprua.DT) AS M " +
				                     "FROM UserProjectRoundUser upru " +
				                     "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
				                     "INNER JOIN [User] u ON upru.UserID = u.UserID " +
				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
				                     "INNER JOIN SponsorProjectRoundUnit spru ON upru.ProjectRoundUnitID = spru.ProjectRoundUnitID AND s.SponsorID = spru.SponsorID " +
				                     "INNER JOIN MeasureCategory mc ON spru.SponsorProjectRoundUnitID = mc.SPRUID " +
				                     "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
				                     "WHERE upru.UserID = " + userID + " " +
				                     "AND dbo.cf_yearMonthDay(uprua.DT) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
				                     "AND dbo.cf_yearMonthDay(uprua.DT) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +

				                     "UNION ALL " +

				                     "SELECT DISTINCT " +
				                     "dbo.cf_yearMonthDay(d.DiaryDate) AS M " +
				                     "FROM Diary d " +
				                     "WHERE d.UserID = " + userID + " " +
				                     "AND dbo.cf_yearMonthDay(d.DiaryDate) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
				                     "AND dbo.cf_yearMonthDay(d.DiaryDate) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +

				                     "ORDER BY M DESC");
				while (r.Read())
				{
					DateTime dt = DateTime.ParseExact(r.GetString(0), "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
					if (dt != oldDT)
					{
						ret[cx].date = dt;

						SqlDataReader r2 = rs("SELECT " +
						                      "DiaryNote, " +
						                      "Mood " +
						                      "FROM Diary " +
						                      "WHERE DeletedDT IS NULL " +
						                      "AND UserID = " + userID + " " +
						                      "AND DiaryDate = '" + dt.ToString("yyyy-MM-dd") + "'");
						while (r2.Read())
						{
							if (!r2.IsDBNull(0) && r2.GetString(0) != "")
							{
								ret[cx].note = r2.GetString(0);
							}
							if (!r2.IsDBNull(1) && r2.GetInt32(1) != 0)
							{
								ret[cx].mood = (Mood)r2.GetInt32(1);
							}
						}
						r2.Close();

						#region Events
						int eventCX = execIntScal("SELECT COUNT(*) FROM (" +
						                          "SELECT DISTINCT " +
						                          "um.UserMeasureID AS A1, " +
						                          "dbo.cf_hourMinute(um.DT) AS M, " +
						                          "ISNULL(ml.Measure,m.Measure) AS A2, " +
						                          "m.SortOrder AS S, " +
						                          "(SELECT COUNT(*) FROM MeasureComponent x WHERE x.MeasureID = m.MeasureID) AS A3, " +
						                          "m.MeasureID AS A4, " +
						                          "NULL AS A5, " +
						                          "NULL AS A6 " +
						                          "FROM UserMeasure um " +
						                          "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
						                          "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
						                          "INNER JOIN Measure m ON mc.MeasureID = m.MeasureID " +
						                          "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + languageID + " " +
						                          "WHERE mc.ShowInList = 1 AND um.DeletedDT IS NULL AND um.UserID = " + userID + " " +
						                          "AND dbo.cf_yearMonthDay(um.DT) = '" + dt.ToString("yyyy-MM-dd") + "' " +

						                          "UNION ALL " +

						                          "SELECT DISTINCT " +
						                          "NULL AS A1, " +
						                          "dbo.cf_hourMinute(es.DateTime) AS M, " +
						                          "el.Exercise AS A2, " +
						                          "e.ExerciseSortOrder+1000 AS S, " +
						                          "NULL AS A3, " +
						                          "NULL AS A4, " +
						                          "NULL AS A5, " +
						                          "NULL AS A6 " +
						                          "FROM ExerciseStats es " +
						                          "INNER JOIN ExerciseVariantLang evl ON es.ExerciseVariantLangID = evl.ExerciseVariantLangID " +
						                          "INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
						                          "INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
						                          "LEFT OUTER JOIN ExerciseLang el ON e.ExerciseID = el.ExerciseID AND el.Lang = " + (languageID - 1) + " " +
						                          "WHERE es.UserID = " + userID + " " +
						                          "AND dbo.cf_yearMonthDay(es.DateTime) = '" + dt.ToString("yyyy-MM-dd") + "' " +

						                          "UNION ALL " +

						                          "SELECT DISTINCT " +
						                          "NULL AS A1, " +
						                          "dbo.cf_hourMinute(uprua.DT) AS M, " +
						                          "ISNULL(mcl.MeasureCategory,mc.MeasureCategory) AS A2, " +
						                          "mc.SortOrder+500 AS S, " +
						                          "NULL AS A3, " +
						                          "NULL AS A4, " +
						                          "uprua.AnswerKey AS A5, " +
						                          "uprua.UserProjectRoundUserAnswerID AS A6 " +
						                          "FROM UserProjectRoundUser upru " +
						                          "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
						                          "INNER JOIN [User] u ON upru.UserID = u.UserID " +
						                          "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
						                          "INNER JOIN SponsorProjectRoundUnit spru ON upru.ProjectRoundUnitID = spru.ProjectRoundUnitID AND s.SponsorID = spru.SponsorID " +
						                          "INNER JOIN MeasureCategory mc ON spru.SponsorProjectRoundUnitID = mc.SPRUID " +
						                          "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
						                          "WHERE upru.UserID = " + userID + " " +
						                          "AND dbo.cf_yearMonthDay(uprua.DT) = '" + dt.ToString("yyyy-MM-dd") + "' " +
						                          
						                          ") tmp");

						if (eventCX > 0)
						{
							Event[] events = new Event[eventCX];

							eventCX = 0;

							r2 = rs("SELECT DISTINCT " +
							        "um.UserMeasureID, " +
							        "dbo.cf_hourMinute(um.DT) AS M, " +
							        "ISNULL(ml.Measure,m.Measure), " +
							        "m.SortOrder AS S, " +
							        "(SELECT COUNT(*) FROM MeasureComponent x WHERE x.MeasureID = m.MeasureID), " +
							        "m.MeasureID, " +
							        "NULL, " +
							        "NULL " +
							        "FROM UserMeasure um " +
							        "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
							        "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
							        "INNER JOIN Measure m ON mc.MeasureID = m.MeasureID " +
							        "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + languageID + " " +
							        "WHERE mc.ShowInList = 1 AND um.DeletedDT IS NULL AND um.UserID = " + userID + " " +
							        "AND dbo.cf_yearMonthDay(um.DT) = '" + dt.ToString("yyyy-MM-dd") + "' " +

							        "UNION ALL " +

							        "SELECT DISTINCT " +
							        "NULL, " +
							        "dbo.cf_hourMinute(es.DateTime) AS M, " +
							        "el.Exercise, " +
							        "e.ExerciseSortOrder+1000 AS S, " +
							        "NULL, " +
							        "NULL, " +
							        "NULL, " +
							        "NULL " +
							        "FROM ExerciseStats es " +
							        "INNER JOIN ExerciseVariantLang evl ON es.ExerciseVariantLangID = evl.ExerciseVariantLangID " +
							        "INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
							        "INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
							        "LEFT OUTER JOIN ExerciseLang el ON e.ExerciseID = el.ExerciseID AND el.Lang = " + (languageID - 1) + " " +
							        "WHERE es.UserID = " + userID + " " +
							        "AND dbo.cf_yearMonthDay(es.DateTime) = '" + dt.ToString("yyyy-MM-dd") + "' " +

							        "UNION ALL " +

							        "SELECT DISTINCT " +
							        "NULL, " +
							        "dbo.cf_hourMinute(uprua.DT) AS M, " +
							        "ISNULL(mcl.MeasureCategory,mc.MeasureCategory), " +
							        "mc.SortOrder+500 AS S, " +
							        "NULL, " +
							        "NULL, " +
							        "uprua.AnswerKey, " +
							        "uprua.UserProjectRoundUserAnswerID " +
							        "FROM UserProjectRoundUser upru " +
							        "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
							        "INNER JOIN [User] u ON upru.UserID = u.UserID " +
							        "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
							        "INNER JOIN SponsorProjectRoundUnit spru ON upru.ProjectRoundUnitID = spru.ProjectRoundUnitID AND s.SponsorID = spru.SponsorID " +
							        "INNER JOIN MeasureCategory mc ON spru.SponsorProjectRoundUnitID = mc.SPRUID " +
							        "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
							        "WHERE upru.UserID = " + userID + " " +
							        "AND dbo.cf_yearMonthDay(uprua.DT) = '" + dt.ToString("yyyy-MM-dd") + "' " +

							        "ORDER BY M, S");
							while (r2.Read())
							{
								DateTime eventDT = DateTime.ParseExact(r.GetString(0) + " " + r2.GetString(1), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.CurrentCulture);
								events[eventCX].time = eventDT;
								events[eventCX].description = r2.GetString(2);

								if (!r2.IsDBNull(6) && !r2.IsDBNull(7))
								{
									events[eventCX].type = EventType.Form;
									events[eventCX].formInstanceKey = r2.GetString(6);
									events[eventCX].eventID = r2.GetInt32(7);
								}
								else if (!r2.IsDBNull(0))
								{
									events[eventCX].type = EventType.Measurement;
									events[eventCX].eventID = r2.GetInt32(0);

									int dx = 0; string res = "";
									SqlDataReader r3 = rs("SELECT " +
									                      "umc.ValInt, " +
									                      "umc.ValDec, " +
									                      "umc.ValTxt, " +
									                      "ISNULL(mcl.MeasureComponent,mc.MeasureComponent), " +
									                      "ISNULL(mcl.Unit,mc.Unit), " +
									                      "mc.Type, " +
									                      "mc.Decimals, " +
									                      "mc.ShowUnitInList " +
									                      "FROM UserMeasure um " +
									                      "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
									                      "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
									                      "LEFT OUTER JOIN MeasureComponentLang mcl ON mc.MeasureComponentID = mcl.MeasureComponentID AND mcl.LangID = " + languageID + " " +
									                      "WHERE mc.ShowInList = 1 AND um.UserMeasureID = " + r2.GetInt32(0) + " " +
									                      "ORDER BY mc.SortOrder");
									while (r3.Read())
									{
										if (dx++ > 0)
										{
											res += " / ";
										}
										switch (r3.GetInt32(5))
										{
												case 4: res += Math.Round(r3.GetDecimal(1), r3.GetInt32(6)).ToString() + (r3.GetInt32(7) == 1 ? r3.GetString(4) : ""); break;
										}

									}
									r3.Close();

									events[eventCX].result = res;
								}
								else
								{
									events[eventCX].type = EventType.Exercise;
								}

								//if (edit)
								//{
								//    if (!rs.IsDBNull(0))
								//    {
								//        //actsBox.InnerHtml += "" +
								//        //    "<IMG ONCLICK=\"actG('" + rs.GetInt32(0) + "'," + ld[rs.GetInt32(5)] + ",'0');\" STYLE=\"cursor:pointer;cursor:hand;\" ALT=\"Idag\" SRC=\"img/graphIcon3.gif\" BORDER=\"0\">" +
								//        //    "&nbsp;" +
								//        //    "<IMG ONCLICK=\"actG('" + rs.GetInt32(0) + "'," + ld[rs.GetInt32(5)] + ",'1');\" STYLE=\"cursor:pointer;cursor:hand;\" ALT=\"Över tid\" SRC=\"img/graphIcon2.gif\" BORDER=\"0\">" +
								//        //    "&nbsp;" +
								//        sb.Append("<a class=\"remove\" href=\"javascript:if(confirm('");
								//        switch (LID)
								//        {
								//            case 1:
								//                sb.Append("Är du säker på att du vill ta bort detta värde?");
								//                break;
								//            case 2:
								//                sb.Append("Are you sure you want to remove this value?");
								//                break;
								//        }
								//        sb.Append("')){document.forms[0].DeleteUMID.value='" + rs.GetInt32(0) + "';__doPostBack('','');}\"></a>");
								//    }
								//    else if (!rs.IsDBNull(6))
								//    {
								//        //actsBox.InnerHtml += "" +
								//        //    "<A HREF=\"statistics.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&AK=" + rs.GetString(6) + "\" STYLE=\"cursor:pointer;cursor:hand;\"><IMG BORDER=\"0\" SRC=\"img/graphIcon2.gif\" BORDER=\"0\"></A>" +
								//        //    "&nbsp;" +
								//        sb.Append("<a class=\"remove\" href=\"javascript:if(confirm('");
								//        switch (LID)
								//        {
								//            case 1:
								//                sb.Append("Är du säker på att du vill ta bort denna mätning?");
								//                break;
								//            case 2:
								//                sb.Append("Are you sure you want to remove this measurement?");
								//                break;
								//        }
								//        sb.Append("')){document.forms[0].DeleteUPRUA.value='" + rs.GetInt32(7) + ":" + rs.GetString(6) + "';__doPostBack('','');}\"></a>");
								//    }
								//}
								////actsBox.InnerHtml += "</TD></TR>";
								//if (!rs.IsDBNull(6))
								//{
								//    sb.Append("<a href=\"statistics.aspx?AK=" + rs.GetString(6) + "\" class=\"statstoggle\"></a>");
								//}
								//sb.Append("</div>");
								//}
								//while (rs.Read());

								eventCX++;
							}
							r2.Close();

							ret[cx].events = events;
						}
						#endregion

						oldDT = dt;
						cx++;
					}
				}
				r.Close();

				return ret;
			}
			return (new Calendar[0]);
		}
		[WebMethod(Description = "Gets exercise info and increments exercise count for user. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public Exercise ExerciseExec(string token, int exerciseVariantLangID, int expirationMinutes)
		{
			Exercise ret = new Exercise();
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				SqlDataReader r = rs("SELECT " +
				                     "el.Exercise, " +
				                     "evl.ExerciseFile, " +
				                     "et.ExerciseTypeID, " +
				                     "evl.ExerciseContent, " +
				                     "e.PrintOnBottom, " +
				                     "e.ReplacementHead, " +
				                     "eal.ExerciseArea, " +
				                     "eal.ExerciseAreaID, " +
				                     "el.ExerciseTime " +
				                     "FROM [ExerciseVariantLang] evl " +
				                     "INNER JOIN [ExerciseVariant] ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
				                     "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
				                     "INNER JOIN [ExerciseLang] el ON ev.ExerciseID = el.ExerciseID AND el.Lang = evl.Lang " +
				                     "INNER JOIN [Exercise] e ON el.ExerciseID = e.ExerciseID " +
				                     "INNER JOIN [ExerciseArea] ea ON ea.ExerciseAreaID = e.ExerciseAreaID " +
				                     "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID AND eal.Lang = evl.Lang " +
				                     "WHERE evl.ExerciseVariantLangID = " + exerciseVariantLangID);
				if (r.Read())
				{
					ret.exerciseHeader = r.GetString(0);
					ret.exerciseContent = r.GetString(3);
					ret.exerciseArea = r.GetString(6);
					ret.exerciseAreaID = r.GetInt32(7);
					ret.exerciseTime = r.GetString(8);

					exec("INSERT INTO [ExerciseStats] (" +
					     "ExerciseVariantLangID, " +
					     "UserID, " +
					     "UserProfileID" +
					     ") VALUES (" +
					     "" + exerciseVariantLangID + "," +
					     "" + userID + "," +
					     "" + execIntScal("SELECT UserProfileID FROM [User] WHERE UserID = " + userID) + "" +
					     ")");
				}
				r.Close();
			}
			return ret;
		}
		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates exercises. Set ExerciseAreaID = 0 to show all. Type 0 = All, 1 = Containing short exercises, 2 = Containing long exercises. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public ExerciseInfo[] ExerciseEnum(string token, int exerciseAreaID, int type, int languageID, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int cx = execIntScal("SELECT COUNT(*) " +
				                     "FROM [ExerciseArea] ea " +
				                     "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID " +
				                     "INNER JOIN [Exercise] e ON ea.ExerciseAreaID = e.ExerciseAreaID " +
				                     "INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID " +
				                     "INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID " +
				                     "INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID " +
				                     "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
				                     "INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID " +
				                     "WHERE eal.Lang = el.Lang " +
				                     "AND e.RequiredUserLevel = 0 " +
				                     "AND el.Lang = evl.Lang " +
				                     "AND evl.Lang = etl.Lang " +
				                     "AND etl.Lang = " + (languageID - 1) + " " +
				                     (type != 0 ? "AND e.Minutes " + (type == 1 ? "<= 15" : "> 15") + " " : "") +
				                     (exerciseAreaID != 0 ? "AND e.ExerciseAreaID = " + exerciseAreaID + " " : "") +
				                     "AND et.ExerciseTypeID = 1");
				ExerciseInfo[] ret = new ExerciseInfo[cx];
				cx = 0;
				SqlDataReader r = rs("SELECT " +
				                     "el.New, " +                    // 0
				                     "NULL, " +
				                     //"(" +
				                     //    "SELECT COUNT(*) FROM [ExerciseVariantLang] evlTmp " +
				                     //    "INNER JOIN [ExerciseVariant] evTmp ON evlTmp.ExerciseVariantID = evTmp.ExerciseVariantID " +
				                     //    "WHERE evTmp.ExerciseTypeID >= 3 " +
				                     //    "AND evTmp.ExerciseTypeID <= 4 " +
				                     //    "AND Lang = evl.Lang " +
				                     //    "AND evTmp.ExerciseID = ev.ExerciseID" +
				                     //") AS VariantCount, " +         // 1
				                     "evl.ExerciseVariantLangID, " + // 2
				                     "eal.ExerciseArea, " +          // 3
				                     "eal.ExerciseAreaID, " +        // 4
				                     "e.ExerciseImg, " +             // 5
				                     "e.ExerciseID, " +              // 6
				                     "ea.ExerciseAreaImg, " +        // 7
				                     "el.Exercise, " +               // 8
				                     "el.ExerciseTime, " +           // 9
				                     "el.ExerciseTeaser, " +         // 10
				                     "evl.ExerciseFile, " +          // 11
				                     "evl.ExerciseFileSize, " +      // 12
				                     "evl.ExerciseContent, " +       // 13
				                     "evl.ExerciseWindowX, " +       // 14
				                     "evl.ExerciseWindowY, " +       // 15
				                     "et.ExerciseTypeID, " +         // 16
				                     "etl.ExerciseType, " +          // 17
				                     "etl.ExerciseSubtype, " +       // 18
				                     "(SELECT COUNT(*) FROM ExerciseStats esX INNER JOIN ExerciseVariantLang evlX ON esX.ExerciseVariantLangID = evlX.ExerciseVariantLangID INNER JOIN ExerciseVariant evX ON evlX.ExerciseVariantID = evX.ExerciseVariantID WHERE evX.ExerciseID = e.ExerciseID) AS CX " +  // 19
				                     "FROM [ExerciseArea] ea " +
				                     "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID " +
				                     "INNER JOIN [Exercise] e ON ea.ExerciseAreaID = e.ExerciseAreaID " +
				                     "INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID " +
				                     "INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID " +
				                     "INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID " +
				                     "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
				                     "INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID " +
				                     "WHERE eal.Lang = el.Lang " +
				                     "AND e.RequiredUserLevel = 0 " +
				                     "AND el.Lang = evl.Lang " +
				                     "AND evl.Lang = etl.Lang " +
				                     "AND etl.Lang = " + (languageID - 1) + " " +
				                     (type != 0 ? "AND e.Minutes " + (type == 1 ? "<= 15" : "> 15") + " " : "") +
				                     (exerciseAreaID != 0 ? "AND e.ExerciseAreaID = " + exerciseAreaID + " " : "") +
				                     "AND et.ExerciseTypeID = 1 " +
				                     "ORDER BY " +
				                     "ea.ExerciseAreaSortOrder ASC, " +
				                     "e.ExerciseSortOrder ASC, " +
				                     "el.Exercise ASC, " +
				                     "et.ExerciseTypeSortOrder ASC");
				while (r.Read())
				{
					ret[cx].exercise = r.GetString(8);
					ret[cx].exerciseID = r.GetInt32(6);
					ret[cx].exerciseTeaser = r.GetString(10);
					ret[cx].exerciseTime = r.GetString(9);
					ret[cx].exerciseArea = r.GetString(3);
					ret[cx].exerciseAreaID = r.GetInt32(4);
					ret[cx].popularity = r.GetInt32(19);
					if (!r.IsDBNull(5))
					{
						ret[cx].exerciseImage = "https://www.healthwatch.se/" + r.GetString(5);
					}

					ExerciseVariant[] tmp = new ExerciseVariant[1];
					tmp[0].exerciseVariantLangID = r.GetInt32(2);
					tmp[0].exerciseType = r.GetString(17);
					ret[cx].exerciseVariant = tmp;

					cx++;
				}
				r.Close();

				return ret;
			}
			return (new ExerciseInfo[0]);
		}
		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates exercise areas. Type 0 = All, 1 = Containing short exercises, 2 = Containing long exercises. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public ExerciseArea[] ExerciseAreaEnum(string token, int type, int languageID, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int cx = execIntScal("SELECT COUNT(*) " +
				                     "FROM [ExerciseArea] ea " +
				                     "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID " +
				                     "WHERE eal.Lang = " + (languageID - 1) + " " +
				                     "AND (" +
				                     "SELECT COUNT(*) " +
				                     "FROM Exercise e " +
				                     "INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID " +
				                     "INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID " +
				                     "INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID " +
				                     "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
				                     "INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID " +
				                     "WHERE e.ExerciseAreaID = ea.ExerciseAreaID " +
				                     "AND eal.Lang = el.Lang " +
				                     "AND e.RequiredUserLevel = 0 " +
				                     "AND el.Lang = evl.Lang " +
				                     "AND evl.Lang = etl.Lang " +
				                     ") > 0");
				ExerciseArea[] ret = new ExerciseArea[cx];
				cx = 0;
				SqlDataReader r = rs("SELECT " +
				                     "eal.ExerciseArea, " +          // 0
				                     "eal.ExerciseAreaID " +
				                     "FROM [ExerciseArea] ea " +
				                     "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID " +
				                     "WHERE eal.Lang = " + (languageID - 1) + " " +
				                     "AND (" +
				                     "SELECT COUNT(*) " +
				                     "FROM Exercise e " +
				                     "INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID " +
				                     "INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID " +
				                     "INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID " +
				                     "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
				                     "INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID " +
				                     "WHERE e.ExerciseAreaID = ea.ExerciseAreaID " +
				                     "AND eal.Lang = el.Lang " +
				                     "AND e.RequiredUserLevel = 0 " +
				                     "AND el.Lang = evl.Lang " +
				                     "AND evl.Lang = etl.Lang " +
				                     ") > 0 " +
				                     "ORDER BY ea.ExerciseAreaSortOrder");
				while (r.Read())
				{
					ret[cx].exerciseArea = r.GetString(0);
					ret[cx].exerciseAreaID = r.GetInt32(1);
					cx++;
				}
				r.Close();

				return ret;
			}
			return (new ExerciseArea[0]);
		}
		[WebMethod(Description="Validates a username and password combination and, if there is a match, returns a user data object including token with a variable lifetime (max 20 minutes).")]
		public UserData UserLogin(string username, string password, int expirationMinutes)
		{
			UserData ud = new UserData();

			SqlDataReader r = rs("SELECT u.UserID, u.LID FROM [User] u WHERE u.Username = '" + username.Replace("'", "") + "' AND u.Password = '" + HashMD5(password.Trim()) + "'");
			if (r.Read())
			{
				ud = getUserToken(r.GetInt32(0),r.GetInt32(1),expirationMinutes);
			}
			r.Close();

			return ud;
		}
		[WebMethod(Description = "Update the user language. Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public bool UserUpdateLanguage(string token, int languageID, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				return (exec("UPDATE [User] SET LID = " + languageID + " WHERE UserID = " + userID) > 0);
			}
			return false;
		}
		[WebMethod(Description = "Extend the user token. Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public bool UserExtendToken(string token, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			return (userID != 0);
		}
		[WebMethod(Description = "Expire token. Returns true if successful.")]
		public bool UserLogout(string token)
		{
			return (exec("UPDATE UserToken SET Expires = GETDATE() WHERE UserToken = '" + token.Replace("'", "''") + "'") > 0);
		}

		[WebMethod(Description = "Report issue. Returns true if successful.")]
		public bool ReportIssue(string token, int expirationMinutes, string title, string description)
		{
			/*
         [IssueID] [int] NULL,
         [IssueDate] [smalldatetime] NULL,
         [Title] [varchar](255) NULL,
         [Description] [text] NULL,
         [UserID] [int] NULL,
			 */
			int userID = getUserIdFromToken(token, expirationMinutes);

			string t = title;
			if(t.Length > 250)
			{
				t = t.Substring(0,250);
			}
			string d = description;
			if (d.Length > 64000)
			{
				d = d.Substring(0, 64000);
			}
			bool success = false;
			try
			{
				exec("INSERT INTO [Issue] (IssueDate,Title,Description,UserID) VALUES (GETDATE(),'" + t.Replace("'", "''") + "','" + d.Replace("'", "''") + "'," + userID + ")");
				success = true;
			}
			catch (Exception) { }

			success = false;
			try
			{
//				sendMail("info@healthwatch.se", "support@healthwatch.se", d, "Issue report: " + t);
				HW.Core.Helpers.SmtpHelper.Send("info@healthwatch.se", "support@healthwatch.se", d, "Issue report: " + t);
				success = true;
			}
			catch (Exception) { }

//			return true;
			return success;
		}

		[WebMethod(Description = "Provide email address to request a password reset link to be sent by email. Returns false only if malformed email address or email server is unavailable, otherwise true.")]
		public bool UserResetPassword(string email, int languageID)
		{
			return sendPasswordReminder(email, languageID);
		}
		[WebMethod(CacheDuration = 10*60, Description = "Enumerates news categories in specified language.")]
		public NewsCategory[] NewsCategories(int lastXdays, int languageID, bool includeEnglishNews)
		{
			int newsCategories = execIntScal("SELECT COUNT(*) FROM NewsCategory nc INNER JOIN NewsCategoryLang ncl ON nc.NewsCategoryID = ncl.NewsCategoryID AND ncl.LangID = " + languageID,"newsSqlConnection");
			NewsCategory[] ncs = new NewsCategory[newsCategories];

			int cx = 0;
			SqlDataReader r = rs("SELECT " +
			                     "nc.NewsCategoryShort, " +
			                     "ncl.NewsCategory, " +
			                     "nc.NewsCategoryID, " +
			                     "ncl.LangID, " +
			                     "(SELECT COUNT(*) FROM News n WHERE n.Published IS NOT NULL AND n.Published <= GETDATE() AND n.Deleted IS NULL AND n.NewsCategoryID = nc.NewsCategoryID AND n.LinkLangID IN (" + (languageID - 1) + (includeEnglishNews ? ",1" : "") + ")), " +
			                     "(SELECT COUNT(*) FROM News n WHERE n.Published IS NOT NULL AND n.Published <= GETDATE() AND n.Deleted IS NULL AND n.NewsCategoryID = nc.NewsCategoryID AND n.LinkLangID IN (" + (languageID - 1) + (includeEnglishNews ? ",1" : "") + ") AND GETDATE() <= DATEADD(day," + lastXdays + ",n.DT)) " +
			                     "FROM NewsCategory nc INNER JOIN NewsCategoryLang ncl ON nc.NewsCategoryID = ncl.NewsCategoryID AND ncl.LangID = " + languageID + " ORDER BY ncl.NewsCategory", "newsSqlConnection");
			while (r.Read())
			{
				ncs[cx].newsCategoryAlias = r.GetString(0);
				ncs[cx].newsCategory = r.GetString(1);
				ncs[cx].newsCategoryID = r.GetInt32(2);
				ncs[cx].languageID = r.GetInt32(3);
				ncs[cx].totalCount = r.GetInt32(4);
				ncs[cx].lastXdaysCount = r.GetInt32(5);
				ncs[cx].newsCategoryImage = "https://www.healthwatch.se/includes/resources/article_" + r.GetString(0) + "_50x50.gif";
				cx++;
			}
			r.Close();

			return ncs;
		}
		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates news in specified category (0 for all or no category) with specified language. LastXdays includes only news published last X days, 0 to disable. StartOffset skips news up to that number. TopX only enumerates that many articles, starting at StartOffset, 0 to disable.")]
		public News[] NewsEnum(int lastXdays, int startOffset, int topX, int languageID, bool includeEnglishNews, int newsCategoryID)
		{
			int news = execIntScal("SELECT " +
			                       "COUNT(*) " +
			                       "FROM News n " +
			                       "WHERE n.Published IS NOT NULL " +
			                       "AND n.Published <= GETDATE() " +
			                       "AND n.Deleted IS NULL " +
			                       (newsCategoryID != 0 ? "AND n.NewsCategoryID = " + newsCategoryID + " " : "") +
			                       "AND n.LinkLangID IN (" + (languageID - 1) + (includeEnglishNews ? ",1" : "") + ") " +
			                       (lastXdays != 0 ? "AND GETDATE() <= DATEADD(day," + lastXdays + ",n.DT) " : "") +
			                       "", "newsSqlConnection");
			news = news - startOffset;
			if (topX > 0)
			{
				news = Math.Min(news, topX);
			}
			news = Math.Max(news, 0);

			News[] n = new News[news];

			int cx = 0, bx = 1;
			SqlDataReader r = rs("SELECT " +
			                     (topX != 0 ? "TOP " + (startOffset + topX) + " " : "") +
			                     "nc.NewsCategoryShort, " +
			                     "ncl.NewsCategory, " +
			                     "nc.NewsCategoryID, " +
			                     "n.LinkLangID, " +
			                     "n.NewsID, " +
			                     "n.HeadlineShort, " +
			                     "n.DT, " +
			                     "n.Headline, " +
			                     "n.Teaser, " +
			                     "n.Body " +
			                     "FROM News n " +
			                     "LEFT OUTER JOIN NewsCategory nc ON n.NewsCategoryID = nc.NewsCategoryID " +
			                     "LEFT OUTER JOIN NewsCategoryLang ncl ON nc.NewsCategoryID = ncl.NewsCategoryID AND ncl.LangID = " + languageID + " " +
			                     "WHERE n.Published IS NOT NULL " +
			                     "AND n.Published <= GETDATE() " +
			                     "AND n.Deleted IS NULL " +
			                     (newsCategoryID != 0 ? "AND n.NewsCategoryID = " + newsCategoryID + " " : "") +
			                     "AND n.LinkLangID IN (" + (languageID - 1) + (includeEnglishNews ? ",1" : "") + ") " +
			                     (lastXdays != 0 ? "AND GETDATE() <= DATEADD(day," + lastXdays + ",n.DT) " : "") +
			                     "ORDER BY n.DT DESC, n.NewsID DESC" +
			                     "", "newsSqlConnection");
			while (r.Read())
			{
				if (bx > startOffset && (topX == 0 || cx < topX))
				{
					n[cx].newsCategoryAlias = (r.IsDBNull(0) ? "" : r.GetString(0));
					n[cx].newsCategory = (r.IsDBNull(1) ? "" : r.GetString(1));
					n[cx].newsCategoryID = (r.IsDBNull(2) ? 0 : r.GetInt32(2));
					n[cx].languageID = (r.GetInt32(3) + 1);
					n[cx].newsCategoryImage = (r.IsDBNull(0) ? "" : "https://www.healthwatch.se/includes/resources/article_" + r.GetString(0) + "_50x50.gif");

					n[cx].newsID = r.GetInt32(4);
					n[cx].link = "https://www.healthwatch.se/news/" + (!r.IsDBNull(0) ? r.GetString(0) + "/" : "") + r.GetString(5);
					n[cx].DT = r.GetDateTime(6);
					n[cx].headline = r.GetString(7);
					n[cx].teaser = r.GetString(8);
					n[cx].body = r.GetString(9);
					cx++;
				}
				bx++;
			}
			r.Close();

			return n;
		}
		[WebMethod(CacheDuration = 10 * 60, Description = "Shows details for a news article.")]
		public News NewsDetail(int newsID, int languageID)
		{
			News n = new News();

			SqlDataReader r = rs("SELECT " +
			                     "nc.NewsCategoryShort, " +
			                     "ncl.NewsCategory, " +
			                     "nc.NewsCategoryID, " +
			                     "n.LinkLangID, " +
			                     "n.NewsID, " +
			                     "n.HeadlineShort, " +
			                     "n.DT, " +
			                     "n.Headline, " +
			                     "n.Teaser, " +
			                     "n.Body " +
			                     "FROM News n " +
			                     "LEFT OUTER JOIN NewsCategory nc ON n.NewsCategoryID = nc.NewsCategoryID " +
			                     "LEFT OUTER JOIN NewsCategoryLang ncl ON nc.NewsCategoryID = ncl.NewsCategoryID AND ncl.LangID = " + languageID + " " +
			                     "WHERE n.Published IS NOT NULL " +
			                     "AND n.Published <= GETDATE() " +
			                     "AND n.Deleted IS NULL " +
			                     "AND n.NewsID = " + newsID + " " +
			                     "", "newsSqlConnection");
			if (r.Read())
			{
				n.newsCategoryAlias = (r.IsDBNull(0) ? "" : r.GetString(0));
				n.newsCategory = (r.IsDBNull(1) ? "" : r.GetString(1));
				n.newsCategoryID = (r.IsDBNull(2) ? 0 : r.GetInt32(2));
				n.languageID = (r.GetInt32(3) + 1);
				n.newsCategoryImage = (r.IsDBNull(0) ? "" : "https://www.healthwatch.se/includes/resources/article_" + r.GetString(0) + "_50x50.gif");

				n.newsID = r.GetInt32(4);
				n.link = "https://www.healthwatch.se/news/" + (!r.IsDBNull(0) ? r.GetString(0) + "/" : "") + r.GetString(5);
				n.DT = r.GetDateTime(6);
				n.headline = r.GetString(7);
				n.teaser = r.GetString(8);
				n.body = r.GetString(9);
			}
			r.Close();

			return n;
		}
		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates profile questions in specified language. Sponsor should be 0 if not known.")]
		public Question[] ProfileQuestions(int languageID, int sponsorID)
		{
			int sortOrder = 0;
			int qCount = execIntScal("SELECT COUNT(*) " +
			                         "FROM Sponsor s " +
			                         "INNER JOIN SponsorBQ sbq ON s.SponsorID = sbq.SponsorID " +
			                         "INNER JOIN BQ ON BQ.BQID = sbq.BQID " +
			                         "INNER JOIN BQLang ON BQ.BQID = BQLang.BQID AND BQLang.LangID = " + languageID + " " +
			                         "WHERE sbq.Hidden = 0 AND s.SponsorID = " + (sponsorID == 0 ? 1 : sponsorID));

			Question[] Qs = new Question[qCount];
			int cx = 0;

			SqlDataReader r = rs("SELECT " +
			                     "BQ.BQID, " +           // 0
			                     "BQLang.BQ, " +         // 1
			                     "BQ.Type, " +           // 2
			                     "sbq.Forced, " +        // 3
			                     "BQ.ReqLength, " +      // 4
			                     "BQ.DefaultVal, " +     // 5
			                     "BQ.MaxLength, " +      // 6
			                     "(" +
			                     "SELECT " +
			                     "COUNT(*) FROM " +
			                     "BQVisibility v2 " +
			                     "INNER JOIN SponsorBQ b2 ON v2.BQID = b2.BQID AND b2.SponsorID = s.SponsorID " +
			                     "WHERE b2.Hidden = 0 AND v2.ChildBQID = BQ.BQID" +
			                     "), " +                  // 7 - Number of parent questions
			                     "BQ.MeasurementUnit " +  // 8
			                     "FROM Sponsor s " +
			                     "INNER JOIN SponsorBQ sbq ON s.SponsorID = sbq.SponsorID " +
			                     "INNER JOIN BQ ON BQ.BQID = sbq.BQID " +
			                     "INNER JOIN BQLang ON BQ.BQID = BQLang.BQID AND BQLang.LangID = " + languageID + " " +
			                     "WHERE sbq.Hidden = 0 AND s.SponsorID = " + (sponsorID == 0 ? 1 : sponsorID) + " " +
			                     "ORDER BY sbq.SortOrder");
			while (r.Read())
			{
				Qs[cx].SortOrder = (++sortOrder);
				Qs[cx].QuestionID = r.GetInt32(0);
				Qs[cx].QuestionText = r.GetString(1);
				Qs[cx].QuestionType = (QuestionTypes)r.GetInt32(2);
				Qs[cx].Mandatory = (!r.IsDBNull(3) && r.GetInt32(3) == 1);
				Qs[cx].RequiredNumberOfCharacters = (!r.IsDBNull(4) ? r.GetInt32(4) : (r.GetInt32(2) == 3 ? 10 : 0));
				Qs[cx].DefaultValue = (r.IsDBNull(5) ? "" : r.GetString(5));
				Qs[cx].MaximumNumberOfCharacters = (!r.IsDBNull(6) ? r.GetInt32(6) : (r.GetInt32(2) == 3 ? 10 : 255));
				Qs[cx].MeasurementUnit = (r.IsDBNull(8) ? "" : r.GetString(8));

				if (r.GetInt32(7) > 0)
				{
					int vcCount = execIntScal("SELECT COUNT(*) " +
					                          "FROM BQVisibility v " +
					                          "INNER JOIN BQ ON v.BQID = BQ.BQID " +
					                          "INNER JOIN SponsorBQ b ON v.BQID = b.BQID AND b.SponsorID = " + (sponsorID == 0 ? 1 : sponsorID) + " " +
					                          "WHERE b.Hidden = 0 AND v.ChildBQID = " + r.GetInt32(0));

					VisibilityConditionOr[] VCs = new VisibilityConditionOr[vcCount];
					int rx = 0;

					SqlDataReader r2 = rs("SELECT " +
					                      "v.BQID, " +
					                      "v.BAID " +
					                      "FROM BQVisibility v " +
					                      "INNER JOIN BQ ON v.BQID = BQ.BQID " +
					                      "INNER JOIN SponsorBQ b ON v.BQID = b.BQID AND b.SponsorID = " + (sponsorID == 0 ? 1 : sponsorID) + " " +
					                      "WHERE b.Hidden = 0 AND v.ChildBQID = " + r.GetInt32(0));
					while (r2.Read())
					{
						VCs[rx].QuestionID = r2.GetInt32(0);
						VCs[rx].AnswerID = r2.GetInt32(1);

						rx++;
					}
					r2.Close();

					Qs[cx].VisibilityConditions = VCs;
				}

				if (r.GetInt32(2) == 1 || r.GetInt32(2) == 7)
				{
					int aSortOrder = 0;
					int aCount = execIntScal("SELECT COUNT(*) " +
					                         "FROM BA " +
					                         "INNER JOIN BALang ON BA.BAID = BALang.BAID AND BALang.LangID = " + languageID + " " +
					                         "WHERE BA.BQID = " + r.GetInt32(0));

					Answer[] A = new Answer[aCount];
					int ax = 0;

					SqlDataReader r2 = rs("SELECT " +
					                      "BA.BAID, " +
					                      "ISNULL(BALang.BA,BA.Internal), " +
					                      "BA.Value " +
					                      "FROM BA " +
					                      "LEFT OUTER JOIN BALang ON BA.BAID = BALang.BAID AND BALang.LangID = " + languageID + " " +
					                      "WHERE BA.BQID = " + r.GetInt32(0) + " " +
					                      "ORDER BY BA.SortOrder");
					while (r2.Read())
					{
						A[ax].AnswerID = r2.GetInt32(0);
						A[ax].SortOrder = (++aSortOrder);
						A[ax].AnswerText = r2.GetString(1);
						A[ax].AnswerValue = (r2.IsDBNull(2) ? r2.GetInt32(0) : r2.GetInt32(2));

						ax++;
					}
					r2.Close();

					Qs[cx].AnswerOptions = A;
				}

				cx++;
			}
			r.Close();

			return Qs;
		}
		[WebMethod(Description = "Creates user. Username and password must be at least five characters. Alternate email is optional. Sponsor and department should be 0 if not known. Returns token if successful, blank if username is too short or already taken, password is too short, policy not accepted or malformed email-address.")]
		public UserData UserCreate(string username, string password, string email, string alternateEmail, bool acceptPolicy, int languageID, int sponsorID, int departmentID, int expirationMinutes)
		{
			if ((departmentID == 0 || execIntScal("SELECT SponsorID FROM Department WHERE DepartmentID = " + departmentID) == sponsorID) && execIntScal("SELECT COUNT(*) FROM [User] WHERE LOWER(Username) = '" + username.Replace("'","").ToLower() + "'") == 0 && username.Length >= 5 && password.Length >= 5 && email != "" && isEmail(email) && (alternateEmail == "" || isEmail(alternateEmail)) && acceptPolicy)
			{
				int userID = execIntScal("INSERT INTO [User] (" +
				                         "Username, " +
				                         "Email, " +
				                         "Password, " +
				                         "SponsorID, " +
				                         "DepartmentID, " +
				                         "LID, " +
				                         "AltEmail" +
				                         ") OUTPUT INSERTED.UserID VALUES (" +
				                         "'" + username.Replace("'", "") + "'," +
				                         "'" + email.Replace("'", "") + "'," +
				                         "'" + HashMD5(password.Trim()) + "'," +
				                         "" + (sponsorID == 0 ? 1 : sponsorID) + "," +
				                         "" + (departmentID == 0 ? "NULL" : departmentID.ToString()) + "," +
				                         "" + languageID + "," +
				                         "" + (alternateEmail != "" ? "'" + alternateEmail.Replace("'", "") + "'" : "NULL") + "" +
				                         ")");

				int userProfileID = execIntScal("INSERT INTO UserProfile (UserID, SponsorID, DepartmentID) OUTPUT INSERTED.UserProfileID VALUES (" + userID + "," + (sponsorID == 0 ? 1 : sponsorID) + "," + (departmentID == 0 ? "NULL" : departmentID.ToString()) + ")");
				exec("UPDATE [User] SET UserProfileID = " + userProfileID + " WHERE UserID = " + userID);

				return getUserToken(userID, languageID, expirationMinutes);
			}

			return (new UserData());
		}
		[WebMethod(Description = "Updates user info. Username must be at least five characters. Alternate email is optional. Returns true if successful, false if token invalid/expired, username is too short or already taken or malformed email-address. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public bool UserUpdateInfo(string username, string email, string alternateEmail, string token, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (
				userID != 0
				&&
				execIntScal("SELECT COUNT(*) FROM [User] WHERE UserID <> " + userID + " AND LOWER(Username) = '" + username.Replace("'", "").ToLower() + "'") == 0
				&&
				username.Length >= 5
				&&
				email != ""
				&&
				isEmail(email)
				&&
				(alternateEmail == "" || isEmail(alternateEmail)))
			{
				return (exec("UPDATE [User] SET " +
				             "Username = '" + username.Replace("'", "") + "', " +
				             "Email = '" + email.Replace("'", "") + "', " +
				             "AltEmail = " + (alternateEmail != "" ? "'" + alternateEmail.Replace("'", "") + "'" : "NULL") + " " +
				             "WHERE UserID = " + userID) > 0);
			}

			return false;
		}
		[WebMethod(Description = "Gets user info. Returns username, email, alternate email and language or blank if token invalid/expired. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public UserInfo UserGetInfo(string token, int expirationMinutes)
		{
			UserInfo ui = new UserInfo();

			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				SqlDataReader r = rs("SELECT Username, Email, AltEmail, LID FROM [User] WHERE UserID = " + userID);
				if (r.Read())
				{
					ui.username = (r.IsDBNull(0) ? "" : r.GetString(0));
					ui.email = (r.IsDBNull(1) ? "" : r.GetString(1));
					ui.alternateEmail = (r.IsDBNull(2) ? "" : r.GetString(2));
					ui.languageID = (r.IsDBNull(3) ? 0 : r.GetInt32(3));
					ui.userID = userID;
				}
				r.Close();
			}

			return ui;
		}
		[WebMethod(Description = "Updates user password. Returns true if successful, false if token invalid/expired or password is too short. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
		public bool UserUpdatePassword(string password, string token, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0 && password.Length >= 5)
			{
				return (exec("UPDATE [User] SET " +
				             "Password = '" + HashMD5(password.Trim()) + "' " +
				             "WHERE UserID = " + userID) > 0);
			}

			return false;
		}
		[WebMethod(Description = "Gets a profile question answer of a user. Returns blank if token invalid/expired or no answer exist.")]
		public string UserGetProfileQuestion(int questionID, string token, int expirationMinutes)
		{
			string ret = "";

			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				SqlDataReader r = rs("SELECT BQ.Type, w.ValueInt, w.ValueText, w.ValueDate " +
				                     "FROM [UserProfileBQ] w " +
				                     "INNER JOIN BQ ON w.BQID = BQ.BQID " +
				                     "INNER JOIN [User] u ON w.UserProfileID = u.UserProfileID " +
				                     "WHERE w.BQID = " + questionID + " AND u.UserID = " + userID);
				if (r.Read())
				{
					switch (r.GetInt32(0))
					{
						case 1:
						case 4:
						case 7:
							ret = (r.IsDBNull(1) ? "" : r.GetInt32(1).ToString()); break;
						case 2:
							ret = (r.IsDBNull(2) ? "" : r.GetString(2)); break;
						case 3:
							ret = (r.IsDBNull(3) ? "" : r.GetDateTime(3).ToString("yyyy-MM-dd")); break;
					}
				}
				r.Close();
			}

			return ret;
		}
		[WebMethod(Description = "Internal function UserKeyGetProfileQuestion.")]
		public string UserKeyGetProfileQuestion(int questionID, string userKey)
		{
			string ret = "";

			int userID = execIntScal("SELECT UserID FROM [User] WHERE UserKey = '" + userKey.Replace("'","") + "'");
			if (userID != 0)
			{
				SqlDataReader r = rs("SELECT BQ.Type, w.ValueInt, w.ValueText, w.ValueDate " +
				                     "FROM [UserProfileBQ] w " +
				                     "INNER JOIN BQ ON w.BQID = BQ.BQID " +
				                     "INNER JOIN [User] u ON w.UserProfileID = u.UserProfileID " +
				                     "WHERE w.BQID = " + questionID + " AND u.UserID = " + userID);
				if (r.Read())
				{
					switch (r.GetInt32(0))
					{
						case 1:
						case 4:
						case 7:
							ret = (r.IsDBNull(1) ? "" : r.GetInt32(1).ToString()); break;
						case 2:
							ret = (r.IsDBNull(2) ? "" : r.GetString(2)); break;
						case 3:
							ret = (r.IsDBNull(3) ? "" : r.GetDateTime(3).ToString("yyyy-MM-dd")); break;
					}
				}
				r.Close();
			}

			return ret;
		}
		[WebMethod(Description = "Sets or updates a profile question of a user. Returns false if token invalid/expired or malformed answer else true.")]
		public bool UserSetProfileQuestion(int questionID, string answer, string token, int expirationMinutes)
		{
			bool res = false;

			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0)
			{
				int userProfileID = execIntScal("SELECT UserProfileID FROM [User] WHERE UserID = " + userID);
				int typeID = execIntScal("SELECT Type FROM BQ WHERE BQID = " + questionID);
				switch (typeID)
				{
					case 1:
					case 7:
						try
						{
							int answerID = Convert.ToInt32(answer);
							if (execIntScal("SELECT BQID FROM BA WHERE BAID = " + answerID) == questionID)
							{
								int oldAnswerID = execIntScal("SELECT TOP 1 ValueInt FROM UserProfileBQ WHERE BQID = " + questionID + " AND UserProfileID = " + userProfileID);
								if(oldAnswerID != 0 && oldAnswerID != answerID)
								{
									userProfileID = duplicateUserProfile(userID, userProfileID, questionID);
									oldAnswerID = 0;
								}
								if (oldAnswerID == 0)
								{
									exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + userProfileID + "," + questionID + "," + answerID + ")");
								}
								updateProfileComparison(userProfileID);
								res = true;
							}
						}
						catch (Exception) { }
						break;
					case 2:
						{
							string oldAnswer = execStrScal("SELECT TOP 1 ValueText FROM UserProfileBQ WHERE BQID = " + questionID + " AND UserProfileID = " + userProfileID);
							if (oldAnswer != "" && oldAnswer != answer)
							{
								userProfileID = duplicateUserProfile(userID, userProfileID, questionID);
								oldAnswer = "";
							}
							if (oldAnswer == "")
							{
								exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueText) VALUES (" + userProfileID + "," + questionID + ",'" + answer.Replace("'", "''") + "')");
							}
							res = true;
						}
						break;
					case 3:
						{
							try
							{
								DateTime answerDT = DateTime.ParseExact(answer, "yyyy-MM-dd", (new System.Globalization.CultureInfo("en-US")));
								DateTime oldAnswerDT = execDateScal("SELECT TOP 1 ValueDate FROM UserProfileBQ WHERE BQID = " + questionID + " AND UserProfileID = " + userProfileID);
								if (oldAnswerDT != DateTime.MinValue && oldAnswerDT != answerDT)
								{
									userProfileID = duplicateUserProfile(userID, userProfileID, questionID);
									oldAnswerDT = DateTime.MinValue;
								}
								if (oldAnswerDT == DateTime.MinValue)
								{
									exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueDate) VALUES (" + userProfileID + "," + questionID + ",'" + answerDT.ToString("yyyy-MM-dd") + "')");
								}
								res = true;
							}
							catch (Exception) { }
						}
						break;
					case 4:
						{
							try
							{
								int answerInt = Convert.ToInt32(answer);
								int oldAnswerInt = execIntScal("SELECT TOP 1 ValueInt FROM UserProfileBQ WHERE BQID = " + questionID + " AND UserProfileID = " + userProfileID, int.MinValue);
//								if (oldAnswerInt != 0 && oldAnswerInt != answerInt)
								if (oldAnswerInt != int.MinValue && oldAnswerInt != answerInt)
								{
									userProfileID = duplicateUserProfile(userID, userProfileID, questionID);
									oldAnswerInt = 0;
								}
								if (oldAnswerInt == 0)
								{
									exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + userProfileID + "," + questionID + "," + answerInt + ")");
								}
								res = true;
							}
							catch (Exception) { }
						}
						break;
				}
			}
			return res;
		}
		[WebMethod(Description = "Internal function SponsorInvites.")]
		public SponsorInvite[] SponsorInvites(string sponsorKey)
		{
			int sponsorID = 0;

			if (sponsorKey.Length == 36)
			{
				try
				{
					SqlDataReader r = rs("SELECT SponsorID FROM Sponsor WHERE SponsorKey = '" + sponsorKey.Replace("'","") + "'"); //LEFT(CAST(SponsorKey AS VARCHAR(64)),8) = '" + sponsorKey.Substring(0, 8).Replace("'", "") + "' AND SponsorID = " + Convert.ToInt32(sponsorKey.Substring(8)));
					if (r.Read())
					{
						sponsorID = r.GetInt32(0);
					}
					r.Close();
				}
				catch (Exception) { }
			}
			if (sponsorID != 0)
			{
				SponsorInvite[] si = new SponsorInvite[execIntScal("SELECT COUNT(*) FROM SponsorInvite WHERE Email <> '' AND SponsorID = " + sponsorID)];
				
				int cx = 0;
				SqlDataReader r = rs("SELECT si.Email, u.UserKey, si.InvitationKey FROM SponsorInvite si LEFT OUTER JOIN [User] u ON si.UserID = u.UserID WHERE si.Email <> '' AND si.SponsorID = " + sponsorID);
				while (r.Read())
				{
					si[cx].email = r.GetString(0);
					si[cx].userKey = (r.IsDBNull(1) ? "" : r.GetGuid(1).ToString());
					si[cx].invitationKey = r.GetGuid(2).ToString();
					cx++;
				}
				r.Close();

				return si;
			}
			return (new SponsorInvite[0]);
		}
		[WebMethod(Description = "Internal function SuperSponsorSponsors.")]
		public Sponsor[] SuperSponsorSponsors(string superSponsorKey)
		{
			Sponsor[] s = new Sponsor[0];

			if (superSponsorKey.Length == 36)
			{
				try
				{
					s = new Sponsor[execIntScal("SELECT COUNT(*) FROM SuperSponsor ss INNER JOIN Sponsor s ON ss.SuperSponsorID = s.SuperSponsorID WHERE ss.SuperSponsorKey = '" + superSponsorKey.Replace("'","") + "'")];

					int cx = 0;
					SqlDataReader r = rs("SELECT s.Sponsor, s.SponsorKey FROM SuperSponsor ss INNER JOIN Sponsor s ON ss.SuperSponsorID = s.SuperSponsorID WHERE ss.SuperSponsorKey = '" + superSponsorKey.Replace("'","") + "'");
					while (r.Read())
					{
						s[cx].sponsor = r.GetString(0);
						s[cx].sponsorKey = r.GetSqlGuid(1).ToString();
						cx++;
					}
					r.Close();
				}
				catch (Exception) { }
			}
			return s;
		}
		[WebMethod(Description = "Internal function InvitationKeyHasTest.")]
		public bool InvitationKeyHasTest(int testID, string invitationKey)
		{
			return (execIntScal("SELECT COUNT(*) FROM [SponsorInvite] si INNER JOIN SponsorInviteTest sit ON si.SponsorInviteID = sit.SponsorInviteID AND sit.TestID = " + testID + " WHERE InvitationKey = '" + invitationKey.Replace("'", "") + "'") > 0);
		}
		[WebMethod(Description = "Internal function InvitationKeySetTest.")]
		public bool InvitationKeySetTest(int testID, string invitationKey, DateTime DT, string testData, int testTypeID)
		{
			bool ret = false;

			int sponsorInviteID = execIntScal("SELECT SponsorInviteID FROM [SponsorInvite] WHERE InvitationKey = '" + invitationKey.Replace("'", "") + "'");
			if (sponsorInviteID != 0)
			{
				exec("INSERT INTO SponsorInviteTest (DT,TestData,TestID,SponsorInviteID,TestTypeID) VALUES (" +
				     "'" + DT.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
				     "'" + testData.Replace("'", "''") + "'," +
				     "" + testID + "," +
				     "" + sponsorInviteID + "," +
				     "" + testTypeID + "" +
				     ")");
				ret = true;
			}

			return ret;
		}

		[WebMethod(Description = "Saves the user device registration ID. Updates the values if there is a record for the device ID on that user. Returns false if the device ID is already in the database.")]
		public bool UserSaveRegistrationID(string registrationID, string phoneName, bool inactive, string token, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			bool isNewValue = false;
			if (userID != 0) {
				SqlDataReader r = rs(
					"SELECT 1 " +
					"FROM UserRegistrationID " +
					"WHERE UserID = " + userID + " " +
					"AND RegistrationID = '" + registrationID.Replace("'", "") + "'"
				);
				if (!r.Read()) {
					isNewValue = true;
				}
				if (isNewValue) {
					exec(
						"INSERT INTO UserRegistrationID(UserID, RegistrationID, Inactive, PhoneName)" +
						"VALUES(" + userID + ", '" + registrationID.Replace("'", "") + "', " + (inactive ? 1 : 0) + ", '" + phoneName.Replace("'", "") + "')"
					);
				} else {
					exec(
						"UPDATE UserRegistrationID SET Inactive = " + (inactive ? 1 : 0) + " " +
						"WHERE UserID = " + userID + ""
					);
					exec(
						"UPDATE UserRegistrationID SET PhoneName = '" + phoneName.Replace("'", "") + "' " +
						"WHERE UserID = " + userID + " " +
						"AND RegistrationID = '" + registrationID.Replace("'", "") + "'"
					);
				}
				r.Close();
			}
			return isNewValue;
		}
		
		[WebMethod(Description = "Gets the user device registration ID. Returns the DeviceID object if the registration device ID for user is in the database.")]
		public DeviceID UserGetRegistrationID(string registrationID, string token, int expirationMinutes)
		{
			DeviceID deviceID = new DeviceID();
			int userID = getUserIdFromToken(token, expirationMinutes);
			if (userID != 0) {
				SqlDataReader r = rs(
					"SELECT UserRegistrationID, UserID, RegistrationID, Inactive, PhoneName " +
					"FROM dbo.UserRegistrationID " +
					"WHERE UserID = " + userID + " " +
					"AND RegistrationID = '" + registrationID.Replace("'", "") + "'"
				);
				if (r.Read()) {
					deviceID = new DeviceID {
						registrationID = r.IsDBNull(2) ? "" : r.GetString(2),
						inactive = r.IsDBNull(3) ? false : r.GetInt32(3) == 1,
						phoneName = r.IsDBNull(4) ? "" : r.GetString(4)
					};
				}
				r.Close();
			}
			return deviceID;
		}

		[WebMethod(Description = "Removes the user device registration ID. Returns false if the device ID is not found in the database.")]
		public bool UserRemoveRegistrationID(string registrationID, string token, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			bool registrationIDFound = false;
			if (userID != 0) {
				SqlDataReader r = rs(
					"SELECT UserRegistrationID " +
					"FROM dbo.UserRegistrationID " +
					"WHERE RegistrationID = '" + registrationID.Replace("'", "") + "' " +
					""
//					"AND UserID = " + userID
				);
				if (r.Read()) {
					registrationIDFound = true;
				}
				if (registrationIDFound) {
					exec(
						"DELETE FROM dbo.UserRegistrationID " +
						"WHERE RegistrationID = '" + registrationID.Replace("'", "") + "' " +
						""
//						"AND UserID = " + userID
					);
				}
				r.Close();
			}
			return registrationIDFound;
		}

		[WebMethod(Description = "Gets the UserKey and updates a new value into the database. Returns 0 if UserKey is not found. This will only update a new UserKey if the User's ReminderLink is 2.")]
		public bool UserSetUsedUserKey(string userKey, string token, int expirationMinutes)
		{
			int userID = getUserIdFromToken(token, expirationMinutes);
			bool validKey = false;
			if (userID != 0) {
				userKey = (userKey.Length >= 12 ? userKey.Substring(0, 12) : userKey).Replace("'", "").ToLower();
				SqlDataReader r = rs(
					"SELECT " +
					"u.UserID, " +
					"u.ReminderLink " +
					"FROM [User] u " +
					"WHERE u.UserID = " + userID + " " +
					"AND LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)) = '" + userKey + "'"
				);
				if (r.Read()) {
					userID = r.GetInt32(0);
					if (r.GetInt32(1) == 2) {
						exec("UPDATE [User] SET UserKey = NEWID() WHERE UserID = " + userID);
					}
					validKey = true;
				}
				r.Close();
			}
			return validKey;
		}
		
		[WebMethod(Description="Validates a user key and, if there is a match, returns a user data object including token with a variable lifetime (max 20 minutes).")]
		public UserData UserLoginWithKey(string userKey, int expirationMinutes)
		{
			UserData ud = new UserData();

			SqlDataReader r = rs(
				"SELECT u.UserID, u.LID " +
				"FROM [User] u " +
				"WHERE u.UserID = " + toInt32(subString(userKey, 12), 0).ToString() + " " +
				"AND LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)) = '" + subString(userKey, 0, 12).Replace("'", "").ToLower() + "'"
			);
			if (r.Read())
			{
				ud = getUserToken(r.GetInt32(0),r.GetInt32(1),expirationMinutes);
			}
			r.Close();

			return ud;
		}
		private string subString(string str, int startIndex)
		{
			try {
				return str.Substring(startIndex);
			} catch {
				return "";
			}
		}
		private string subString(string str, int startIndex, int length)
		{
			try {
				return str.Substring(startIndex, length);
			} catch {
				return "";
			}
		}
		private int toInt32(object v, int d)
		{
			try {
				return Convert.ToInt32(v);
			} catch {
				return d;
			}
		}
		private void updateProfileComparison(int userProfileID)
		{
			string comparison = "";
			string comparisonInsert = "";

			SqlDataReader r = rs("SELECT " +
			                     "sbq.BQID, " +           // 0
			                     "upbq.ValueInt " +
			                     "FROM UserProfile up " +
			                     "INNER JOIN SponsorBQ sbq ON up.SponsorID = sbq.SponsorID " +
			                     "INNER JOIN BQ ON BQ.BQID = sbq.BQID " +
			                     "INNER JOIN UserProfileBQ upbq ON up.UserProfileID = upbq.UserProfileID AND upbq.BQID = BQ.BQID " +
			                     "WHERE BQ.Type IN (1,7) AND BQ.Comparison = 1 AND up.UserProfileID = " + userProfileID + " ORDER BY BQ.BQID");
			while (r.Read())
			{
				if (!r.IsDBNull(1))
				{
					string val = r.GetInt32(1).ToString();
					comparison += val;
					comparisonInsert += (comparisonInsert != "" ? "¤" : "") + "INSERT INTO ProfileComparisonBQ (ProfileComparisonID,BQID,ValueInt) VALUES ([x]," + r.GetInt32(0) + "," + val + ")";
				}
			}
			r.Close();

			string hash = HashMD5(comparison);
			int profileComparisonID = execIntScal("SELECT ProfileComparisonID FROM ProfileComparison WHERE Hash = '" + hash + "'");
			if (profileComparisonID == 0)
			{
				profileComparisonID = execIntScal("INSERT INTO ProfileComparison (Hash) OUTPUT INSERTED.ProfileComparisonID VALUES ('" + hash + "')");
				if (comparisonInsert != "")
				{
					if (comparisonInsert.IndexOf('¤') >= 0)
					{
						foreach (string s in comparisonInsert.Split('¤'))
						{
							exec(s.Replace("[x]", profileComparisonID.ToString()));
						}
					}
					else
					{
						exec(comparisonInsert.Replace("[x]", profileComparisonID.ToString()));
					}
				}
			}
			exec("UPDATE UserProfile SET ProfileComparisonID = " + profileComparisonID + " WHERE UserProfileID = " + userProfileID);
		}
		private int duplicateUserProfile(int userID, int userProfileID, int excludeQuestionID)
		{
			int newUserProfileID = execIntScal("INSERT INTO UserProfile (UserID, SponsorID, DepartmentID) OUTPUT INSERTED.UserProfileID SELECT UserID, SponsorID, DepartmentID FROM UserProfile WHERE UserProfileID = " + userProfileID);
			exec("UPDATE [User] SET UserProfileID = " + newUserProfileID + " WHERE UserID = " + userID);

			SqlDataReader r = rs("SELECT " +
			                     "sbq.BQID, " +           // 0
			                     "BQ.Type, " +
			                     "upbq.ValueInt, " +
			                     "upbq.ValueText, " +
			                     "upbq.ValueDate " +
			                     "FROM UserProfile up " +
			                     "INNER JOIN SponsorBQ sbq ON up.SponsorID = sbq.SponsorID " +
			                     "INNER JOIN BQ ON BQ.BQID = sbq.BQID " +
			                     "INNER JOIN UserProfileBQ upbq ON up.UserProfileID = upbq.UserProfileID AND upbq.BQID = BQ.BQID " +
			                     "WHERE BQ.BQID <> " + excludeQuestionID + " AND up.UserProfileID = " + userProfileID);
			while (r.Read())
			{
				switch (r.GetInt32(1))
				{
					case 1:
					case 4:
					case 7:
						{
							if (!r.IsDBNull(2))
							{
								exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + newUserProfileID + "," + r.GetInt32(0) + "," + r.GetInt32(2) + ")");
							}
						}
						break;
					case 2:
						{
							if (!r.IsDBNull(3))
							{
								exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueText) VALUES (" + newUserProfileID + "," + r.GetInt32(0) + ",'" + r.GetString(3).Replace("'", "''") + "')");
							}
						}
						break;
					case 3:
						{
							if (!r.IsDBNull(4))
							{
								exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueDate) VALUES (" + newUserProfileID + "," + r.GetInt32(0) + ",'" + r.GetDateTime(4).ToString("yyyy-MM-dd") + "')");
							}
						}
						break;
				}
			}
			r.Close();

			updateProfileComparison(newUserProfileID);

			return newUserProfileID;
		}
		private SqlDataReader rs(string sqlString)
		{
			return rs(sqlString, "SqlConnection");
		}
		private SqlDataReader rs(string sqlString, string con)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
			SqlDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
			return dataReader;
		}
		private string HashMD5(string str)
		{
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] hashByteArray = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes("HW" + str + "HW"));
			string hash = "";
			for (int i = 0; i < hashByteArray.Length; i++)
				hash += hashByteArray[i];
			return hash;
		}
		private int exec(string sqlString)
		{
			return exec(sqlString, "SqlConnection");
		}
		private int exec(string sqlString, string con)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
			int ret = dataCommand.ExecuteNonQuery();
			dataConnection.Close();
			dataConnection.Dispose();
			return ret;
		}
		private string execStrScal(string sqlString)
		{
			return execStrScal(sqlString, "SqlConnection");
		}
		private string execStrScal(string sqlString, string con)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
			object o = dataCommand.ExecuteScalar();
			string ret = "";
			if(o != null)
				ret = o.ToString();
			dataConnection.Close();
			dataConnection.Dispose();
			return ret;
		}
		private int execIntScal(string sqlString)
		{
			return execIntScal(sqlString, "SqlConnection");
		}
		private int execIntScal(string sqlString, string con)
		{
			return execIntScal(sqlString, con, 0);
		}
		private int execIntScal(string sqlString, int defaultValue)
		{
			return execIntScal(sqlString, "SqlConnection", defaultValue);
		}
		private int execIntScal(string sqlString, string con, int defaultValue)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
			object o = dataCommand.ExecuteScalar();
//			int ret = 0;
			int ret = defaultValue;
			if(o != null)
				ret = Convert.ToInt32(o.ToString());
			dataConnection.Close();
			dataConnection.Dispose();
			return ret;
		}
		private DateTime execDateScal(string sqlString)
		{
			return execDateScal(sqlString, "SqlConnection");
		}
		private DateTime execDateScal(string sqlString, string con)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
			object o = dataCommand.ExecuteScalar();
			DateTime ret = DateTime.MinValue;
			if (o != null)
				ret = Convert.ToDateTime(o.ToString());
			dataConnection.Close();
			dataConnection.Dispose();
			return ret;
		}
//		private bool sendMail(string from, string email, string body, string subject)
//		{
//			bool success = false;
//			try
//			{
//				System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);
//				System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(from,email,subject,body);
//				smtp.Send(mail);
//				success = true;
//			}
//			catch (Exception) { }
//			return success;
//		}
		private bool isEmail(string inputEmail)
		{
			string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
				@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
				@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
			System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(strRegex);
			if (re.IsMatch(inputEmail))
				return true;
			else
				return false;
		}

		private bool sendPasswordReminder(string email, int languageID)
		{
			bool success = false;

			if (email != "" && isEmail(email))
			{
				success = true;

				SqlDataReader r = rs("SELECT TOP 100 UserID, Email, Username, LEFT(REPLACE(CONVERT(VARCHAR(255),UserKey),'-',''),8) FROM [User] WHERE Email = '" + email.ToString().Replace("'", "") + "'");
				while (r.Read())
				{
					switch (languageID)
					{
						case 1:
//							success = sendMail("support@healthwatch.se", r.GetString(1),
							success = HW.Core.Helpers.SmtpHelper.Send("support@healthwatch.se", r.GetString(1),
							                                          "Nytt lösenord",
							                                          "Hej." +
							                                          "\r\n\r\n" +
							                                          "En begäran om nytt lösenord till ditt konto med användarnamn \"" + r.GetString(2) + "\" har inkommit. Om du begärt detta, klicka på länken nedan för att ange ett nytt lösenord." +
							                                          "\r\n\r\n" +
							                                          "https://www.healthwatch.se/password.aspx?NL=1&K=" + r.GetString(3) + r.GetInt32(0) + "");
							break;
						case 2:
//							success = sendMail("support@healthwatch.se", r.GetString(1),
							success = HW.Core.Helpers.SmtpHelper.Send("support@healthwatch.se", r.GetString(1),
							                                          "New password",
							                                          "Hi." +
							                                          "\r\n\r\n" +
							                                          "A request for new password for your account with username \"" + r.GetString(2) + "\" has arrived. If you made this request, click the link below to set a new password." +
							                                          "\r\n\r\n" +
							                                          "https://www.healthwatch.se/password.aspx?NL=1&K=" + r.GetString(3) + r.GetInt32(0) + "");
							break;
					}
				}
				r.Close();
			}

			return success;
		}
		private int getUserIdFromToken(string token, int expirationMinutes)
		{
			int userID = 0;
			if (token.Length == 36)
			{
				userID = execIntScal("SELECT UserID FROM UserToken WHERE UserToken = '" + token.Replace("'", "") + "' AND GETDATE() < Expires");
			}
			if (userID != 0 && expirationMinutes > 0)
			{
				exec("UPDATE UserToken SET Expires = DATEADD(minute," + Math.Min(expirationMinutes, 20) + ",GETDATE()) WHERE UserToken = '" + token.Replace("'", "''") + "'");
			}
			return userID;
		}
		private UserData getUserToken(int userID, int languageID, int expirationMinutes)
		{
			UserData ud = new UserData();
			ud.languageID = languageID;
			ud.tokenExpires = DateTime.Now.AddMinutes(Math.Min(expirationMinutes, 20));
			int sessionID = execIntScal("INSERT INTO Session (DT,UserAgent,UserID,IP,AutoEnded) OUTPUT INSERTED.SessionID VALUES (GETDATE(),'App'," + userID + ",'127.0.0.1',1)");
			ud.token = execStrScal("INSERT INTO UserToken (UserID, Expires,SessionID) OUTPUT INSERTED.UserToken VALUES (" + userID + ",DATEADD(minute," + Math.Min(expirationMinutes, 20) + ",GETDATE())," + sessionID + ")");

			return ud;
		}
		private string nextReminderSend(int type, string[] settings, DateTime lastLogin, DateTime lastSend)
		{
			DateTime nextPossibleReminderSend = lastSend.Date.AddHours(Convert.ToInt32(settings[0]));
			while (nextPossibleReminderSend <= DateTime.Now.AddMinutes(30))
			{
				nextPossibleReminderSend = nextPossibleReminderSend.AddDays(1);
			}
			DateTime nextReminderSend = nextPossibleReminderSend.AddYears(10);

			try
			{
				switch (type)
				{
					case 1:
						System.DayOfWeek[] dayOfWeek = { System.DayOfWeek.Monday, System.DayOfWeek.Tuesday, System.DayOfWeek.Wednesday, System.DayOfWeek.Thursday, System.DayOfWeek.Friday, System.DayOfWeek.Saturday, System.DayOfWeek.Sunday };

						switch (Convert.ToInt32(settings[1]))
						{
							case 1:
								#region Weekday
								{
									string[] days = settings[2].Split(',');
									foreach (string day in days)
									{
										DateTime tmp = nextPossibleReminderSend;
										while (tmp.DayOfWeek != dayOfWeek[Convert.ToInt32(day) - 1])
										{
											tmp = tmp.AddDays(1);
										}
										if (tmp < nextReminderSend)
										{
											nextReminderSend = tmp;
										}
									}
									break;
								}
								#endregion
							case 2:
								#region Week
								{
									nextReminderSend = nextPossibleReminderSend.AddDays(7 * (Convert.ToInt32(settings[3]) - 1));
									while (nextReminderSend.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[2]) - 1])
									{
										nextReminderSend = nextReminderSend.AddDays(1);
									}
									break;
								}
								#endregion
							case 3:
								#region Month
								{
									DateTime tmp = nextPossibleReminderSend.AddDays(-nextPossibleReminderSend.Day);
									int i = 0;
									while (tmp.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[3]) - 1] || i != Convert.ToInt32(settings[2]))
									{
										tmp = tmp.AddDays(1);
										if (tmp.DayOfWeek == dayOfWeek[Convert.ToInt32(settings[3]) - 1])
										{
											i++;
										}
									}
									nextReminderSend = nextPossibleReminderSend.AddMonths((Convert.ToInt32(settings[4]) - 1));
									if (tmp < nextPossibleReminderSend)
									{
										// Has allready occurred this month
										nextReminderSend = nextReminderSend.AddMonths(1);
									}
									nextReminderSend = nextReminderSend.AddDays(-nextReminderSend.Day);
									i = 0;
									while (nextReminderSend.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[3]) - 1] || i != Convert.ToInt32(settings[2]))
									{
										nextReminderSend = nextReminderSend.AddDays(1);
										if (nextReminderSend.DayOfWeek == dayOfWeek[Convert.ToInt32(settings[3]) - 1])
										{
											i++;
										}
									}
									break;
								}
								#endregion
						}
						break;
					case 2:
						nextReminderSend = lastLogin.Date.AddHours(Convert.ToInt32(settings[0])).AddDays(Convert.ToInt32(settings[1]) * Convert.ToInt32(settings[2]));
						while (nextReminderSend < nextPossibleReminderSend)
						{
							nextReminderSend = nextReminderSend.AddDays(7);
						}
						break;
				}
			}
			catch (Exception)
			{
				nextReminderSend = nextPossibleReminderSend.AddYears(10);
			}

			return nextReminderSend.ToString("yyyy-MM-dd HH:mm");
		}
		private int createSurveyUser(int userID, int untID, string eml)
		{
			int usrID = 0;

			SqlDataReader r = rs("SELECT ProjectRoundID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + untID, "eFormSqlConnection");
			if (r.Read())
			{
				exec("INSERT INTO ProjectRoundUser (ProjectRoundID,ProjectRoundUnitID,Email) VALUES (" + r.GetInt32(0) + "," + untID + ",'" + eml.Replace("'", "") + "')", "eFormSqlConnection");
				r.Close();
				r = rs("SELECT ProjectRoundUserID FROM [ProjectRoundUser] WHERE ProjectRoundUnitID=" + untID + " AND Email = '" + eml.Replace("'", "") + "' ORDER BY ProjectRoundUserID DESC", "eFormSqlConnection");
				if (r.Read())
				{
					usrID = r.GetInt32(0);
					exec("INSERT INTO UserProjectRoundUser (UserID, ProjectRoundUnitID, ProjectRoundUserID) VALUES (" + userID + "," + untID + "," + usrID + ")");
				}
			}
			r.Close();

			return usrID;
		}
		private int feedbackIdx(int level)
		{
			return 10 + level;
		}
		private int actionIdx(int level)
		{
			return 15 + level;
		}
		
//		public struct Event
//		{
//			public DateTime time;
//			public string description;
//			public string result;
//			public string formInstanceKey;
//			public EventType type;
//			public int eventID;
//		}
//
//		public struct Calendar
//		{
//			public DateTime date;
//			public Mood mood;
//			public string note;
//			public Event[] events;
//		}
//
//		public struct Exercise
//		{
//			public string exerciseHeader;
//			public string exerciseContent;
//			public string exerciseTime;
//			public string exerciseArea;
//			public int exerciseAreaID;
//		}
//
//		public struct ExerciseArea
//		{
//			public string exerciseArea;
//			public int exerciseAreaID;
//		}
//
//		public struct ExerciseVariant
//		{
//			public int exerciseVariantLangID;
//			public string exerciseType;
//		}
//
//		public struct ExerciseInfo
//		{
//			public string exercise; // 8
//			public int exerciseID; // 6
//			public string exerciseTeaser; // 10
//			public string exerciseTime; // 9
//			public string exerciseArea; // 3
//			public int exerciseAreaID; // 4
//			public string exerciseImage; // 5
//			public ExerciseVariant[] exerciseVariant; // 2
//			public int popularity;
//		}
//
//		public struct UserInfo
//		{
//			public String username;
//			public String email;
//			public String alternateEmail;
//			public int languageID;
//		}
//
//		public struct UserData
//		{
//			public String token;
//			public DateTime tokenExpires;
//			public int languageID;
//		}
//
//		public struct NewsCategory
//		{
//			public int newsCategoryID;
//			public String newsCategory;
//			public String newsCategoryAlias;
//			public String newsCategoryImage;
//			public int languageID;
//			public int totalCount;
//			public int lastXdaysCount;
//		}
//
//		public struct News
//		{
//			public int newsCategoryID;
//			public String newsCategory;
//			public String newsCategoryAlias;
//			public String newsCategoryImage;
//			public int languageID;
//			public int newsID;
//			public DateTime DT;
//			public string headline;
//			public string teaser;
//			public string body;
//			public string link;
//		}
//
//		public struct Answer
//		{
//			public int AnswerID;
//			public int SortOrder;
//			public String AnswerText;
//			public int AnswerValue;
//		}
//
//		public struct Question
//		{
//			public int QuestionID;
//			public int SortOrder;
//			public QuestionTypes QuestionType;
//			public String QuestionText;
//			public String MeasurementUnit;
//			public bool Mandatory;
//			public string DefaultValue;
//			public int RequiredNumberOfCharacters;
//			public int MaximumNumberOfCharacters;
//			public Answer[] AnswerOptions;
//			public VisibilityConditionOr[] VisibilityConditions;
//			public int OptionID;
//		}
//
//		public struct VisibilityConditionOr
//		{
//			public int QuestionID;
//			public int AnswerID;
//		}
//
//		public struct MeasureType
//		{
//			public string measureType;
//			public int measureTypeID;
//		}
//
//		public struct MeasureCategory
//		{
//			public string measureCategory;
//			public int measureCategoryID;
//			public string measureType;
//			public int measureTypeID;
//		}
//		public struct Measure
//		{
//			public string measure;
//			public int measureID;
//			public string measureCategory;
//			public int measureCategoryID;
//			public string moreInfo;
//			public int componentCount;
//			public MeasureComponent[] measureComponents;
//		}
//
//		public struct MeasureComponent
//		{
//			public string measureComponent;
//			public int measureComponentID;
//			public QuestionTypes questionType;
//			public bool inherited;
//			public bool hasAutoCalculateChildren;
//			public bool isAutoCalculated;
//			public int decimals;
//			public string unit;
//			public string autoCalculateScript;
//			public string triggerScript;
//			public string inheritedValue;
//		}
//
//		public struct Reminder
//		{
//			public int type;
//			public int autoLoginLink;
//			public int sendAtHour;
//			public int regularity;
//			public bool regularityDailyMonday;
//			public bool regularityDailyTuesday;
//			public bool regularityDailyWednesday;
//			public bool regularityDailyThursday;
//			public bool regularityDailyFriday;
//			public bool regularityDailySaturday;
//			public bool regularityDailySunday;
//			public int regularityWeeklyDay;
//			public int regularityWeeklyEvery;
//			public int regularityMonthlyWeekNr;
//			public int regularityMonthlyDay;
//			public int regularityMonthlyEvery;
//			public int inactivityCount;
//			public int inactivityPeriod;
//		}
//
//		public struct UserMeasureComponent
//		{
//			public int MeasureComponentID;
//			public string value;
//		}
//
//		public struct Form
//		{
//			public string formKey;
//			public string form;
//		}
//
//		public struct FormQuestionAnswer
//		{
//			public int questionID;
//			public int optionID;
//			public string answer;
//		}
//
//		public struct FormInstance
//		{
//			public string formDisclaimer;
//			public string formInstanceKey;
//			public DateTime dateTime;
//			public FormInstanceFeedback[] fiv;
//		}
//
//		public struct FormInstanceFeedback
//		{
//			public int feedbackTemplateID;
//			public string header;
//			public string value;
//			public string analysis;
//			public Rating rating;
//			public string feedback;
//			public string actionPlan;
//			public string yellowLow;
//			public string greenLow;
//			public string greenHigh;
//			public string yellowHigh;
//		}
//
//		public struct FormFeedbackTemplate
//		{
//			public int feedbackTemplateID;
//			public string header;
//		}
//
//		public struct FormFeedback
//		{
//			public int feedbackTemplateID;
//			public string header;
//			public string formInstanceKey;
//			public DateTime dateTime;
//			public string value;
//			public string profileValue;
//			public string databaseValue;
//		}
//
		////		public struct WordsOfWisdom
		////		{
		////			public string words;
		////			public string author;
		////		}
		////
//		public enum QuestionTypes { SingleChoiceFewOptions = 1, SingleChoiceManyOptions = 7, FreeText = 2, Numeric = 4, Date = 3, VAS = 9 };
//		public enum Mood { NotSet = 0, DontKnow = 1, Happy = 2, Neutral = 3, Unhappy = 4 };
//		public enum EventType { Form = 1, Measurement = 2, Exercise = 3 };
//		public enum Rating { NotKnown = 0, Unhealthy = 1, Warning = 2, Healthy = 3 };
//
//		[WebMethod(CacheDuration = 10 * 60, Description = "Todays words of wisdom.")]
//		public WordsOfWisdom TodaysWordsOfWisdom(int languageID)
//		{
//			SqlDataReader r = rs("SELECT wl.Wise, wl.WiseBy FROM WiseLang wl INNER JOIN Wise w ON wl.WiseID = w.WiseID WHERE w.LastShown = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND wl.LangID = " + languageID);
//			if (!r.Read()) {
//				r.Close();
//				r = rs("SELECT TOP 1 wl.Wise, wl.WiseBy, w.WiseID FROM WiseLang wl INNER JOIN Wise w ON wl.WiseID = w.WiseID WHERE wl.LangID = " + languageID + " ORDER BY w.LastShown ASC");
//				r.Read();
//				exec("UPDATE Wise SET LastShown = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE WiseID = " + r.GetInt32(2));
//			}
//
//			WordsOfWisdom w = new WordsOfWisdom();
//			w.words = r.GetString(0);
//			w.author = r.GetString(1);
//
//			return w;
//		}
//
//		[WebMethod(Description = "Get user form feedback over time for specific template. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public FormFeedback[] UserGetFormFeedback(string token,string formKey,int formFeedbackTemplateID,DateTime fromDateTime,DateTime toDateTime,int languageID,int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int projectRoundUnitID = 0, projectRoundUserID = 0;
//				SqlDataReader r = rs("SELECT " +
//				                     "spru.ProjectRoundUnitID, " +
//				                     "upru.ProjectRoundUserID " +
//				                     "FROM [User] u " +
//				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
//				                     "INNER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
//				                     "WHERE u.UserID = " + userID + " " +
//				                     "AND REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') = '" + formKey.Replace("'", "") + "'");
//				if (r.Read()) {
//					projectRoundUnitID = r.GetInt32(0);
//					projectRoundUserID = r.GetInt32(1);
//				}
//				r.Close();
//				int QID = 0, OID = 0; string header = "";
//				#region Get header and question/option id
//				r = rs("SELECT " +
//				       "wqo.QuestionID, " +
//				       "wqo.OptionID, " +
//				       "wqol.FeedbackHeader " +
//				       "FROM Report r " +
//				       "INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
//				       "INNER JOIN ReportPartComponent rpc ON rp.ReportPartID = rpc.ReportPartID " +
//				       "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
//				       "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + languageID + " " +
//				       "WHERE rpc.ReportPartID = " + formFeedbackTemplateID, "eFormSqlConnection");
//				if(r.Read()) {
//					QID = r.GetInt32(0);
//					OID = r.GetInt32(1);
//					header = r.GetString(2);
//				}
//				r.Close();
//				#endregion
//				int cx = execIntScal("SELECT " +
//				                     "COUNT(*) " +
//				                     "FROM Answer a " +
//				                     "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +
//				                     //"INNER JOIN healthWatch..UserProjectRoundUser h ON ha.ProjectRoundUserID = h.ProjectRoundUserID " +
//				                     "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
//				                     "AND av.ValueInt IS NOT NULL " +
//				                     "AND av.DeletedSessionID IS NULL " +
//				                     "AND av.QuestionID = " + QID + " " +
//				                     "AND av.OptionID = " + OID + " " +
//				                     "WHERE a.EndDT IS NOT NULL " +
//				                     "AND ha.ProjectRoundUserID = " + projectRoundUserID + " " +
//				                     "AND a.EndDT >= '" + fromDateTime.ToString("yyyy-MM-dd") + "' " +
//				                     "AND a.EndDT <= '" + toDateTime.ToString("yyyy-MM-dd") + "'", "eFormSqlConnection");
//				FormFeedback[] ret = new FormFeedback[cx];
//				cx = 0;
//				int db = execIntScal("SELECT AVG(av.ValueInt) FROM Answer a INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
//				                     "WHERE a.EndDT IS NOT NULL " +
//				                     "AND av.ValueInt IS NOT NULL " +
//				                     "AND av.DeletedSessionID IS NULL " +
//				                     "AND av.QuestionID = " + QID + " " +
//				                     "AND av.OptionID = " + OID + " " +
//				                     "AND a.EndDT >= '" + fromDateTime.ToString("yyyy-MM-dd") + "' " +
//				                     "AND a.EndDT <= '" + toDateTime.ToString("yyyy-MM-dd") + "'", "eFormSqlConnection");
//				r = rs("SELECT " +
//				       "av.ValueInt, " +
//				       "a.EndDT, " +
//				       "REPLACE(CONVERT(VARCHAR(255),ha.AnswerKey),'-','') " +
//				       "FROM Answer a " +
//				       "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +
//				       //"INNER JOIN healthWatch..UserProjectRoundUser h ON ha.ProjectRoundUserID = h.ProjectRoundUserID " +
//				       "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
//				       "AND av.ValueInt IS NOT NULL " +
//				       "AND av.DeletedSessionID IS NULL " +
//				       "AND av.QuestionID = " + QID + " " +
//				       "AND av.OptionID = " + OID + " " +
//				       "WHERE a.EndDT IS NOT NULL " +
//				       "AND ha.ProjectRoundUserID = " + projectRoundUserID + " " +
//				       "AND a.EndDT >= '" + fromDateTime.ToString("yyyy-MM-dd") + "' " +
//				       "AND a.EndDT <= '" + toDateTime.ToString("yyyy-MM-dd") + "' " +
//				       "ORDER BY a.EndDT ASC", "eFormSqlConnection");
//				while (r.Read()) {
//					ret[cx].dateTime = r.GetDateTime(1);
//					ret[cx].value = r.GetInt32(0).ToString();
//					ret[cx].databaseValue = db.ToString();
//					ret[cx].profileValue = db.ToString();
//					ret[cx].feedbackTemplateID = formFeedbackTemplateID;
//					ret[cx].formInstanceKey = r.GetString(2);
//					ret[cx].header = header;
//					cx++;
//				}
//				r.Close();
//
//				return ret;
//			}
//			return (new FormFeedback[0]);
//		}
//
//		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerate form feedback templates. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public FormFeedbackTemplate[] FormFeedbackTemplateEnum(string token,string formKey,int languageID,int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int projectRoundUnitID = 0, projectRoundUserID = 0;
//				SqlDataReader r = rs("SELECT " +
//				                     "spru.ProjectRoundUnitID, " +
//				                     "upru.ProjectRoundUserID, " +
//				                     "u.Email, " +
//				                     "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') " +
//				                     "FROM [User] u " +
//				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
//				                     "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
//				                     "WHERE u.UserID = " + userID);
//				while (r.Read()) {
//					if (formKey == r.GetString(3)) {
//						projectRoundUnitID = r.GetInt32(0);
//						if (r.IsDBNull(1)) {
//							projectRoundUserID = createSurveyUser(userID, r.GetInt32(0), r.GetString(2));
//						} else {
//							projectRoundUserID = r.GetInt32(1);
//						}
//					}
//				}
//				r.Close();
//
//				int cx = execIntScal("SELECT " +
//				                     "COUNT(*) " +
//				                     "FROM ProjectRoundUnit pru " +
//				                     "INNER JOIN Report r ON dbo.cf_unitIndividualReportID(pru.ProjectRoundUnitID) = r.ReportID " +
//				                     "LEFT OUTER JOIN ReportLang rl ON r.ReportID = rl.ReportID AND rl.LangID = " + languageID + " " +
//				                     "LEFT OUTER JOIN ReportPart rp ON rp.ReportID = r.ReportID AND rp.Type = 8 " +
//				                     "LEFT OUTER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = " + languageID + " " +
//				                     "WHERE " +
//				                     "pru.ProjectRoundUnitID = " + projectRoundUnitID, "eFormSqlConnection");
//
//				FormFeedbackTemplate[] fft = new FormFeedbackTemplate[cx];
//				cx = 0;
//				string sql = "SELECT " +
//					"rl.Feedback, " +
//					"rpl.Subject, " +
//					"rpl.AltText, " +
//					"rp.ReportPartID " +
//					"FROM ProjectRoundUnit pru " +
//					"INNER JOIN Report r ON dbo.cf_unitIndividualReportID(pru.ProjectRoundUnitID) = r.ReportID " +
//					"LEFT OUTER JOIN ReportLang rl ON r.ReportID = rl.ReportID AND rl.LangID = " + languageID + " " +
//					"LEFT OUTER JOIN ReportPart rp ON rp.ReportID = r.ReportID AND rp.Type = 8 " +
//					"LEFT OUTER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = " + languageID + " " +
//					"WHERE " +
//					"pru.ProjectRoundUnitID = " + projectRoundUnitID + " " +
//					"ORDER BY rp.SortOrder";
//				r = rs(sql, "eFormSqlConnection");
//				while (r.Read()) {
//					fft[cx].feedbackTemplateID = r.GetInt32(3);
//					fft[cx].header = r.GetString(1);
//					cx++;
//				}
//				r.Close();
//
//				return fft;
//			}
//			return (new FormFeedbackTemplate[0]);
//		}
//
//		[WebMethod(Description = "Get user form feedback. Leave formInstanceKey blank for latest instance. Tag EXID in actionPlan is reference to ExerciseID, should be replaced with hyperlink. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public FormInstance UserGetFormInstanceFeedback(string token,string formKey,string formInstanceKey,int languageID,int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int projectRoundUnitID = 0, projectRoundUserID = 0;
//				SqlDataReader r = rs("SELECT " +
//				                     "spru.ProjectRoundUnitID, " +
//				                     "upru.ProjectRoundUserID, " +
//				                     "u.Email, " +
//				                     "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') " +
//				                     "FROM [User] u " +
//				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
//				                     "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
//				                     "WHERE u.UserID = " + userID);
//				while (r.Read()) {
//					if (formKey == r.GetString(3)) {
//						projectRoundUnitID = r.GetInt32(0);
//						if (r.IsDBNull(1)) {
//							projectRoundUserID = createSurveyUser(userID, r.GetInt32(0), r.GetString(2));
//						} else {
//							projectRoundUserID = r.GetInt32(1);
//						}
//					}
//				}
//				r.Close();
//				if (formInstanceKey == "") {
//					r = rs("SELECT " +
//					       "TOP 1 " +
//					       "REPLACE(CONVERT(VARCHAR(255),uprua.AnswerKey),'-','') " +
//					       "FROM [User] u " +
//					       "INNER JOIN UserProjectRoundUser upru ON u.UserID = upru.UserID " +
//					       "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
//					       "INNER JOIN eform..Answer a ON uprua.AnswerID = a.AnswerID " +
//					       "INNER JOIN eform..ProjectRound pr ON a.ProjectRoundID = pr.ProjectRoundID " +
//					       "INNER JOIN eform..ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
//					       "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//					       "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID AND spru.SurveyID = ISNULL(NULLIF(pru.SurveyID,0),pr.SurveyID) " +
//					       "WHERE u.UserID = " + userID + " " +
//					       "AND REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') = '" + formKey.Replace("'","") + "' " +
//					       "ORDER BY uprua.DT DESC, uprua.AnswerID DESC");
//					if (r.Read()) {
//						formInstanceKey = r.GetString(0);
//					}
//					r.Close();
//				}
//				if (formInstanceKey != "" && projectRoundUnitID != 0) {
//					FormInstance fi = new FormInstance();
//					fi.formInstanceKey = formInstanceKey;
//
//					#region Specific measurement
//					string sql = "SELECT " +
//						"rl.Feedback, " +
//						"rpl.Subject, " +
//						"rpl.AltText, " +
//						"rp.ReportPartID, " +
//						"(SELECT COUNT(*) FROM ReportPart rp2 WHERE rp2.ReportID = r.ReportID) AS CX " +
//						"FROM ProjectRoundUnit pru " +
//						"INNER JOIN Report r ON dbo.cf_unitIndividualReportID(pru.ProjectRoundUnitID) = r.ReportID " +
//						"LEFT OUTER JOIN ReportLang rl ON r.ReportID = rl.ReportID AND rl.LangID = " + languageID + " " +
//						"LEFT OUTER JOIN ReportPart rp ON rp.ReportID = r.ReportID AND rp.Type = 8 " +
//						"LEFT OUTER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = " + languageID + " " +
//						"WHERE " +
//						"pru.ProjectRoundUnitID = " + projectRoundUnitID + " " +
//						"ORDER BY rp.SortOrder";
//					r = rs(sql, "eFormSqlConnection");
//					if (r.Read()) {
//						fi.formDisclaimer = r.GetString(0);
//						FormInstanceFeedback[] fiv = new FormInstanceFeedback[r.GetInt32(4)];
//						int cx = 0;
//						do {
//							fiv[cx].feedbackTemplateID = r.GetInt32(3);
//
//							SqlDataReader r2 = rs("SELECT " +
//							                      "rpc.WeightedQuestionOptionID, " +	// 0
//							                      "wqol.WeightedQuestionOption, " +
//							                      "wqo.TargetVal, " +
//							                      "wqo.YellowLow, " +
//							                      "wqo.GreenLow, " +
//							                      "wqo.GreenHigh, " +					// 5
//							                      "wqo.YellowHigh, " +
//							                      "wqo.QuestionID, " +
//							                      "wqo.OptionID, " +
//							                      "wqol.FeedbackHeader, " +
//							                      "wqol.Feedback," +                  // 10
//							                      "wqol.FeedbackRedLow," +
//							                      "wqol.FeedbackYellowLow," +
//							                      "wqol.FeedbackGreen," +
//							                      "wqol.FeedbackYellowHigh," +
//							                      "wqol.FeedbackRedHigh," +           // 15
//							                      "wqol.ActionRedLow," +
//							                      "wqol.ActionYellowLow," +
//							                      "wqol.ActionGreen," +
//							                      "wqol.ActionYellowHigh," +
//							                      "wqol.ActionRedHigh " +             // 20
//							                      "FROM Report r " +
//							                      "INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
//							                      "INNER JOIN ReportPartComponent rpc ON rp.ReportPartID = rpc.ReportPartID " +
//							                      "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
//							                      "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + languageID + " " +
//							                      "WHERE rpc.ReportPartID = " + r.GetInt32(3) + " " +
//							                      "ORDER BY rp.SortOrder, rpc.SortOrder", "eFormSqlConnection");
//							while (r2.Read()) {
//								fiv[cx].header = r2.GetString(9);
//								if (!r2.IsDBNull(10) && r2.GetString(10) != "") {
//									fiv[cx].analysis = r2.GetString(10);
//								}
//								fiv[cx].rating = Rating.NotKnown;
//
//								SqlDataReader r3 = rs("SELECT TOP 1 " +
//								                      "av.ValueInt, " +
//								                      "a.EndDT, " +
//								                      "a.AnswerID " +
//								                      "FROM Answer a " +
//								                      "INNER JOIN healthWatch..UserProjectRoundUserAnswer ha ON a.AnswerID = ha.AnswerID AND ha.ProjectRoundUserID = a.ProjectRoundUserID " +
//								                      "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
//								                      "AND av.DeletedSessionID IS NULL " +
//								                      "AND av.QuestionID = " + r2.GetInt32(7) + " " +
//								                      "AND av.OptionID = " + r2.GetInt32(8) + " " +
//								                      "WHERE a.EndDT IS NOT NULL " +
//								                      "AND ha.AnswerKey = '" + formInstanceKey + "' " +
//								                      "ORDER BY a.EndDT DESC", "eFormSqlConnection");
//								if (r3.Read()) {
//									fi.dateTime = r3.GetDateTime(1);
//									fiv[cx].value = r3.GetInt32(0).ToString();
//									if (!r2.IsDBNull(3))
//										fiv[cx].yellowLow = r2.GetInt32(3).ToString();
//									if (!r2.IsDBNull(4))
//										fiv[cx].greenLow = r2.GetInt32(4).ToString();
//									if (!r2.IsDBNull(5))
//										fiv[cx].greenHigh = r2.GetInt32(5).ToString();
//									if (!r2.IsDBNull(6))
//										fiv[cx].yellowHigh = r2.GetInt32(6).ToString();
//
//									if (!r3.IsDBNull(0)) {
//										bool hasColor = false;
//										int levelID = 0;
//
//										#region Levels
//										if (!r2.IsDBNull(3)) {
//											hasColor = true;
//											if (r2.GetInt32(3) >= 0 && r2.GetInt32(3) <= 100) {
//
//												if (r3.GetInt32(0) >= r2.GetInt32(3)) {
//													levelID = 2;
//												}
//											}
//										}
//										if (!r2.IsDBNull(4)) {
//											hasColor = true;
//											if (r2.GetInt32(4) >= 0 && r2.GetInt32(4) <= 100) {
//												if (r3.GetInt32(0) >= r2.GetInt32(4)) {
//													levelID = 3;
//												}
//											}
//										}
//										if (!r2.IsDBNull(5)) {
//											hasColor = true;
//											if (r2.GetInt32(5) >= 0 && r2.GetInt32(5) <= 100) {
//												if (r3.GetInt32(0) >= r2.GetInt32(5)) {
//													levelID = 4;
//												}
//											}
//										}
//										if (!r2.IsDBNull(6)) {
//											hasColor = true;
//											if (r2.GetInt32(6) >= 0 && r2.GetInt32(6) <= 100) {
//												if (r3.GetInt32(0) >= r2.GetInt32(6)) {
//													levelID = 5;
//												}
//											}
//										}
//										if (levelID == 0) {
//											if (hasColor) {
//												levelID = 1;
//											}
//										}
//										#endregion
//										switch (levelID) {
//												case 1: fiv[cx].rating = Rating.Unhealthy; break;
//												case 2: fiv[cx].rating = Rating.Warning; break;
//												case 3: fiv[cx].rating = Rating.Healthy; break;
//												case 4: fiv[cx].rating = Rating.Warning; break;
//												case 5: fiv[cx].rating = Rating.Unhealthy; break;
//										}
//										if (levelID != 0) {
//											if (!r2.IsDBNull(feedbackIdx(levelID)) && r2.GetString(feedbackIdx(levelID)) != "") {
//												fiv[cx].feedback = r2.GetString(feedbackIdx(levelID));
//											}
//											if (!r2.IsDBNull(actionIdx(levelID)) && r2.GetString(actionIdx(levelID)) != "") {
//												fiv[cx].actionPlan = r2.GetString(actionIdx(levelID));
//											}
//										}
//									}
//
//								}
//								r3.Close();
//							}
//							r2.Close();
//
//							cx++;
//						} while (r.Read());
//
//						fi.fiv = fiv;
//					}
//					r.Close();
//					#endregion
//
//					return fi;
//				}
//			}
//			return (new FormInstance());
//		}
//
//		[WebMethod(Description = "Set user form, question by question. Leave formInstanceKey blank on first answer. Returns formInstanceKey if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public string UserSetFormInstanceQuestion(string token,string formKey,string formInstanceKey,int questionID,int optionID,string answer,int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int projectRoundUnitID = 0, projectRoundUserID = 0;
//				SqlDataReader r = rs("SELECT " +
//				                     "spru.ProjectRoundUnitID, " +
//				                     "upru.ProjectRoundUserID, " +
//				                     "u.Email, " +
//				                     "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') " +
//				                     "FROM [User] u " +
//				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
//				                     "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
//				                     "WHERE u.UserID = " + userID);
//				while (r.Read()) {
//					if (formKey == r.GetString(3)) {
//						projectRoundUnitID = r.GetInt32(0);
//						if (r.IsDBNull(1)) {
//							projectRoundUserID = createSurveyUser(userID, r.GetInt32(0), r.GetString(2));
//						} else {
//							projectRoundUserID = r.GetInt32(1);
//						}
//					}
//				}
//				r.Close();
//				#region Fetch ProjectRoundID
//				int projectRoundID = execIntScal("SELECT " +
//				                                 "pru.ProjectRoundID " +
//				                                 "FROM ProjectRoundUser u " +
//				                                 "INNER JOIN ProjectRoundUnit pru ON u.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
//				                                 "WHERE u.ProjectRoundUserID = " + projectRoundUserID, "eFormSqlConnection");
//				#endregion
//				if (projectRoundID != 0 && projectRoundUnitID != 0) {
//					bool first = false;
//					int answerID = 0;
//					if (formInstanceKey == "") {
//						#region Create new AnswerID
//						exec("INSERT INTO Answer (EndDT,ProjectRoundID, ProjectRoundUnitID, ProjectRoundUserID, ExtendedFirst) VALUES (GETDATE()," + projectRoundID + "," + projectRoundUnitID + "," + projectRoundUserID + "," + (first ? "1" : "NULL") + ")", "eFormSqlConnection");
//						r = rs("SELECT TOP 1 AnswerID, REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-','') FROM Answer WHERE ProjectRoundUserID = " + projectRoundUserID + " ORDER BY AnswerID DESC", "eFormSqlConnection");
//						if (r.Read()) {
//							answerID = r.GetInt32(0);
//							formInstanceKey = r.GetString(1);
//						}
//						r.Close();
//						exec("INSERT INTO UserProjectRoundUserAnswer (ProjectRoundUserID, AnswerKey, UserProfileID, AnswerID) VALUES (" + projectRoundUserID + ",'" + formInstanceKey.Replace("'","") + "'," + execIntScal("SELECT UserProfileID FROM [User] WHERE UserID = " + userID) + "," + answerID + ")");
//					} else {
//						answerID = execIntScal("SELECT AnswerID FROM Answer WHERE REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-','') = '" + formInstanceKey.Replace("'","") + "' AND ProjectRoundUserID = " + projectRoundUserID, "eFormSqlConnection");
//					}
//					#endregion
//					if (answerID != 0) {
//						int sessionID = 0;
//						int optionType = execIntScal("SELECT o.OptionType FROM [Option] o INNER JOIN QuestionOption qo ON o.OptionID = qo.OptionID WHERE qo.QuestionID = " + questionID + " AND o.OptionID = " + optionID, "eFormSqlConnection");
//						if (optionType != 0) {
//							exec("UPDATE AnswerValue SET DeletedSessionID = " + sessionID + " WHERE AnswerID = " + answerID + " AND QuestionID = " + questionID + " AND OptionID = " + optionID + " AND DeletedSessionID IS NULL", "eFormSqlConnection");
//						}
//						#region Save new value
//						switch (optionType) {
//							case 1:
//								{
//									try {
//										if (execIntScal("SELECT COUNT(*) FROM OptionComponents ocs WHERE ocs.OptionID = " + optionID + " AND ocs.OptionComponentID = " + Convert.ToInt32(answer), "eFormSqlConnection") == 0) {
//											throw (new Exception());
//										}
//										exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueInt,CreatedSessionID) VALUES (" + answerID + "," + questionID + "," + optionID + "," + Convert.ToInt32(answer) + "," + sessionID + ")", "eFormSqlConnection");
//									} catch (Exception) { formInstanceKey = ""; }
//									break;
//								}
//							case 2:
//								{
//									exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueText,CreatedSessionID) VALUES (" + answerID + "," + questionID + "," + optionID + ",'" + answer.Replace("'", "''") + "'," + sessionID + ")", "eFormSqlConnection");
//									break;
//								}
//							case 3:
//								{
//									goto case 1;
//								}
//							case 4:
//								{
//									try {
//										decimal newValIns = Convert.ToDecimal(answer);
//										exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueDecimal,CreatedSessionID) VALUES (" + answerID + "," + questionID + "," + optionID + "," + Convert.ToDecimal(answer).ToString().Replace(",", ".") + "," + sessionID + ")", "eFormSqlConnection");
//									} catch (Exception) { formInstanceKey = ""; }
//									break;
//								}
//							case 9:
//								{
//									try {
//										int v = Convert.ToInt32(answer);
//										if (v < 0 || v > 100)
//											throw (new Exception());
//										exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueInt,CreatedSessionID) VALUES (" + answerID + "," + questionID + "," + optionID + "," + Convert.ToInt32(answer) + "," + sessionID + ")", "eFormSqlConnection");
//									} catch (Exception) { formInstanceKey = ""; }
//									break;
//								}
//							default:
//								formInstanceKey = "";
//								break;
//						}
//						#endregion
//					} else {
//						formInstanceKey = "";
//					}
//				}
//				return formInstanceKey;
//			}
//			return "";
//		}
//
//		[WebMethod(Description = "Set user form. Returns formInstanceKey if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public string UserSetFormInstance(string token,string formKey,FormQuestionAnswer[] fqa,int expirationMinutes)
//		{
//			string answerKey = "";
//
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int projectRoundUnitID = 0, projectRoundUserID = 0;
//				SqlDataReader r = rs("SELECT " +
//				                     "spru.ProjectRoundUnitID, " +
//				                     "upru.ProjectRoundUserID, " +
//				                     "u.Email, " +
//				                     "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-','') " +
//				                     "FROM [User] u " +
//				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
//				                     "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
//				                     "WHERE u.UserID = " + userID);
//				while (r.Read()) {
//					if (formKey == r.GetString(3)) {
//						projectRoundUnitID = r.GetInt32(0);
//						if (r.IsDBNull(1)) {
//							projectRoundUserID = createSurveyUser(userID, r.GetInt32(0), r.GetString(2));
//						} else {
//							projectRoundUserID = r.GetInt32(1);
//						}
//					}
//				}
//				r.Close();
//				#region Fetch ProjectRoundID
//				int projectRoundID = execIntScal("SELECT " +
//				                                 "pru.ProjectRoundID " +
//				                                 "FROM ProjectRoundUser u " +
//				                                 "INNER JOIN ProjectRoundUnit pru ON u.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
//				                                 "WHERE u.ProjectRoundUserID = " + projectRoundUserID, "eFormSqlConnection");
//				#endregion
//				if (projectRoundID != 0 && projectRoundUnitID != 0) {
//					bool first = false;
//					int answerID = 0;
//					#region Create new AnswerID
//					exec("INSERT INTO Answer (EndDT, ProjectRoundID, ProjectRoundUnitID, ProjectRoundUserID, ExtendedFirst) VALUES (GETDATE()," + projectRoundID + "," + projectRoundUnitID + "," + projectRoundUserID + "," + (first ? "1" : "NULL") + ")", "eFormSqlConnection");
//					r = rs("SELECT TOP 1 AnswerID, REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-','') FROM Answer WHERE ProjectRoundUserID = " + projectRoundUserID + " ORDER BY AnswerID DESC", "eFormSqlConnection");
//					if (r.Read()) {
//						answerID = r.GetInt32(0);
//						answerKey = r.GetString(1);
//					}
//					r.Close();
//					exec("INSERT INTO UserProjectRoundUserAnswer (ProjectRoundUserID, AnswerKey, UserProfileID, AnswerID) VALUES (" + projectRoundUserID + ",'" + answerKey.Replace("'", "") + "'," + execIntScal("SELECT UserProfileID FROM [User] WHERE UserID = " + userID) + "," + answerID + ")");
//					#endregion
//					if (answerID != 0) {
//						foreach (FormQuestionAnswer f in fqa) {
//							int sessionID = 0;
//							int optionType = execIntScal("SELECT o.OptionType FROM [Option] o INNER JOIN QuestionOption qo ON o.OptionID = qo.OptionID WHERE qo.QuestionID = " + f.questionID + " AND o.OptionID = " + f.optionID, "eFormSqlConnection");
//							#region Save new value
//							switch (optionType) {
//								case 1:
//									{
//										try {
//											if (execIntScal("SELECT COUNT(*) FROM OptionComponents ocs WHERE ocs.OptionID = " + f.optionID + " AND ocs.OptionComponentID = " + Convert.ToInt32(f.answer), "eFormSqlConnection") == 0) {
//												throw (new Exception());
//											}
//											exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueInt,CreatedSessionID) VALUES (" + answerID + "," + f.questionID + "," + f.optionID + "," + Convert.ToInt32(f.answer) + "," + sessionID + ")", "eFormSqlConnection");
//										} catch (Exception) { answerKey = ""; }
//										break;
//									}
//								case 2:
//									{
//										exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueText,CreatedSessionID) VALUES (" + answerID + "," + f.questionID + "," + f.optionID + ",'" + f.answer.Replace("'", "''") + "'," + sessionID + ")", "eFormSqlConnection");
//										break;
//									}
//								case 3:
//									{
//										goto case 1;
//									}
//								case 4:
//									{
//										try {
//											decimal newValIns = Convert.ToDecimal(f.answer);
//											exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueDecimal,CreatedSessionID) VALUES (" + answerID + "," + f.questionID + "," + f.optionID + "," + Convert.ToDecimal(f.answer).ToString().Replace(",", ".") + "," + sessionID + ")", "eFormSqlConnection");
//										} catch (Exception) { answerKey = ""; }
//										break;
//									}
//								case 9:
//									{
//										try {
//											int v = Convert.ToInt32(f.answer);
//											if (v < 0 || v > 100)
//												throw (new Exception());
//											exec("INSERT INTO AnswerValue (AnswerID,QuestionID,OptionID,ValueInt,CreatedSessionID) VALUES (" + answerID + "," + f.questionID + "," + f.optionID + "," + Convert.ToInt32(f.answer) + "," + sessionID + ")", "eFormSqlConnection");
//										} catch (Exception) { answerKey = ""; }
//										break;
//									}
//								default:
//									answerKey = "";
//									break;
//							}
//							#endregion
//						}
//					}
//				}
//				return answerKey;
//			}
//			return answerKey;
//		}
//
//		[WebMethod(Description = "Deletes user form. Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public bool UserDeleteFormInstance(string token,string formInstanceKey,int eventID,int expirationMinutes)
//		{
//			int ret = 0;
//
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				ret = exec("UPDATE UserProjectRoundUserAnswer SET " +
//				           "ProjectRoundUserID = -ABS(ProjectRoundUserID), " +
//				           "UserProfileID = -ABS(UserProfileID) " +
//				           "WHERE AnswerKey = '" + formInstanceKey.Replace("'", "") + "' " +
//				           "AND UserProjectRoundUserAnswerID = " + eventID);
//			}
//			return (ret > 0);
//		}
//
//		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates form questions. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public Question[] FormQuestionEnum(string token, int languageID, string formKey, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int projectRoundUnitID = 0, projectRoundUserID = 0;
//				SqlDataReader r = rs("SELECT " +
//				                     "spru.ProjectRoundUnitID, " +
//				                     "upru.ProjectRoundUserID, " +
//				                     "u.Email, " +
//				                     "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
//				                     "ISNULL(sprul.Nav,spru.Nav) AS Nav " +
//				                     "FROM [User] u " +
//				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
//				                     "LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID AND sprul.LangID = " + languageID + " " +
//				                     "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
//				                     "WHERE u.UserID = " + userID);
//				while (r.Read()) {
//					if (formKey == r.GetString(3)) {
//						projectRoundUnitID = r.GetInt32(0);
//						if (r.IsDBNull(1)) {
//							projectRoundUserID = createSurveyUser(userID, r.GetInt32(0), r.GetString(2));
//						} else {
//							projectRoundUserID = r.GetInt32(1);
//						}
//					}
//				}
//				r.Close();
//				bool first = false;
//				#region Fetch SurveyID
//				int surveyID = execIntScal("SELECT " +
//				                           "dbo.cf_unitSurveyID(u.ProjectRoundUnitID) " +
//				                           "FROM ProjectRoundUser u " +
//				                           "INNER JOIN ProjectRoundUnit pru ON u.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
//				                           "WHERE u.ProjectRoundUserID = " + projectRoundUserID, "eFormSqlConnection");
//				#endregion
//
//				int cx = execIntScal("SELECT " +
//				                     "COUNT(DISTINCT sq.SurveyQuestionID) " +
//				                     "FROM SurveyQuestion sq " +
//				                     "INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
//				                     "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
//				                     "WHERE q.Box = 0 " +
//				                     (!first ? "AND (sq.ExtendedFirst IS NULL OR sq.ExtendedFirst = 0) " : "") +
//				                     "AND sq.SurveyID = " + surveyID, "eFormSqlConnection");
//
//				Question[] ret = new Question[cx];
//
//				cx = 0;
//				r = rs("SELECT " +
//				       "sq.SurveyQuestionID, " +	// 0
//				       "sq.OptionsPlacement, " +	// 1
//				       "q.FontFamily, " +			// 2
//				       "ISNULL(sq.FontSize,q.FontSize), " +			// 3
//				       "q.FontDecoration, " +		// 4
//				       "q.FontColor, " +			// 5
//				       "q.Underlined, " +			// 6
//				       "ISNULL(sql.Question,ql.Question) AS Question, " +			// 7
//				       "q.QuestionID, " +			// 8
//				       "q.Box, " +					// 9
//				       "sq.NoCount, " +			// 10
//				       "sq.RestartCount, " +		// 11
//				       "(SELECT COUNT(*) FROM SurveyQuestionOption sqo WHERE sqo.OptionPlacement = 1 AND sqo.SurveyQuestionID = sq.SurveyQuestionID), " +	// 12
//				       "(SELECT COUNT(*) FROM SurveyQuestionOption sqo INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID WHERE qo.Hide = 1 AND sqo.SurveyQuestionID = sq.SurveyQuestionID), " +	// 13
//				       "sq.NoBreak, " +			// 14
//				       "sq.BreakAfterQuestion, " +	// 15
//				       "s.FlipFlopBg, " +			// 16
//				       "s.TwoColumns, " +          // 17
//				       "(SELECT COUNT(*) FROM SurveyQuestionOption sqo WHERE sqo.SurveyQuestionID = sq.SurveyQuestionID) " +	// 18
//				       "FROM Survey s " +
//				       "INNER JOIN SurveyQuestion sq ON s.SurveyID = sq.SurveyID " +
//				       "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
//				       "INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + languageID + " " +
//				       "LEFT OUTER JOIN SurveyQuestionLang sql ON sq.SurveyQuestionID = sql.SurveyQuestionID AND sql.LangID = ql.LangID " +
//				       "WHERE q.Box = 0 AND s.SurveyID = " + surveyID + " " +
//				       (!first ? "AND (sq.ExtendedFirst IS NULL OR sq.ExtendedFirst = 0) " : "") +
//				       "ORDER BY sq.SortOrder", "eFormSqlConnection");
//				while (r.Read()) {
//					if (r.GetInt32(18) > 0) {
//						SqlDataReader r2 = rs("SELECT " +
//						                      "qo.QuestionID, " +			// 0
//						                      "qo.OptionID, " +			// 1
//						                      "sqo.OptionPlacement, " +	// 2
//						                      "o.OptionType, " +			// 3
//						                      "o.Width, " +				// 4
//						                      "ISNULL(sqo.Height,o.Height), " +				// 5
//						                      "sqo.Forced, " +			// 6
//						                      "o.InnerWidth, " +			// 7
//						                      "qo.Hide, " +				// 8
//						                      "o.BgColor, " +				// 9
//						                      "(SELECT COUNT(*) FROM OptionComponents ocs WHERE ocs.OptionID = o.OptionID) " +    // 10
//						                      "FROM SurveyQuestionOption sqo " +
//						                      "INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " +
//						                      "INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
//						                      "WHERE sqo.SurveyQuestionID = " + r.GetInt32(0) + " " +
//						                      "ORDER BY sqo.SortOrder", "eFormSqlConnection");
//						if (r2.Read()) {
//							ret[cx].QuestionID = r.GetInt32(8);
//							ret[cx].SortOrder = (cx + 1);
//							ret[cx].QuestionText = (!r.IsDBNull(7) ? r.GetString(7) : "");
//
//							ret[cx].OptionID = r2.GetInt32(1);
//							ret[cx].QuestionType = (QuestionTypes)r2.GetInt32(3);
//
//							if (r2.GetInt32(3) == 1 || r2.GetInt32(3) == 9) {
//								Answer[] a = new Answer[r2.GetInt32(10)];
//								int dx = 0;
//
//								SqlDataReader r3 = rs("SELECT " +
//								                      "ocs.OptionComponentID, " +
//								                      "ocl.Text " +
//								                      "FROM OptionComponents ocs " +
//								                      "INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
//								                      "INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + languageID + " " +
//								                      "WHERE ocs.OptionID = " + r2.GetInt32(1) + " " +
//								                      "ORDER BY ocs.SortOrder", "eFormSqlConnection");
//								while (r3.Read()) {
//									a[dx].AnswerText = (!r3.IsDBNull(1) ? r3.GetString(1) : "");
//									switch (r2.GetInt32(3)) {
//										case 1:
//											a[dx].AnswerID = r3.GetInt32(0);
//											a[dx].SortOrder = dx;
//											break;
//										case 9:
//											a[dx].SortOrder = dx;
//											a[dx].AnswerValue = (dx * (100 / (r2.GetInt32(10) - 1)));
//											break;
//									}
//									dx++;
//								}
//								r3.Close();
//
//								ret[cx].AnswerOptions = a;
//							}
//
//							cx++;
//						}
//						r2.Close();
//					}
//				}
//				r.Close();
//
//				return ret;
//			}
//			return (new Question[0]);
//		}
//
//		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates forms. Note that form keys are not static (same form can have different key for different users). Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public Form[] FormEnum(string token, int languageID, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int formCount = execIntScal("SELECT " +
//				                            "COUNT(*) " +
//				                            "FROM [User] u " +
//				                            "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//				                            "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
//				                            "LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID AND sprul.LangID = " + languageID + " " +
//				                            "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
//				                            "WHERE u.UserID = " + userID);
//
//				Form[] ret = new Form[formCount];
//				int cx = 0;
//				SqlDataReader r = rs("SELECT " +
//				                     "spru.ProjectRoundUnitID, " +
//				                     "upru.ProjectRoundUserID, " +
//				                     "u.Email, " +
//				                     "REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
//				                     "ISNULL(sprul.Nav,spru.Nav) AS Nav " +
//				                     "FROM [User] u " +
//				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//				                     "INNER JOIN SponsorProjectRoundUnit spru ON s.SponsorID = spru.SponsorID " +
//				                     "LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID AND sprul.LangID = " + languageID + " " +
//				                     "LEFT OUTER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = u.UserID " +
//				                     "WHERE u.UserID = " + userID + " " +
//				                     "ORDER BY spru.SortOrder");
//				while (r.Read()) {
//					if (r.IsDBNull(1)) {
//						createSurveyUser(userID, r.GetInt32(0), r.GetString(2));
//					}
//					ret[cx].formKey = r.GetString(3);
//					ret[cx].form = r.GetString(4);
//					cx++;
//				}
//				r.Close();
//
//				return ret;
//			}
//			return (new Form[0]);
//		}
//
//		[WebMethod(Description = "Delete measure. Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public bool UserDeleteMeasure(string token,int eventID,int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				if(exec("UPDATE UserMeasure SET DeletedDT = GETDATE() WHERE UserID = " + userID + " AND UserMeasureID = " + eventID) > 0)
//					return true;
//			}
//			return false;
//		}
//
//		[WebMethod(Description = "Set measure having one or two components (use component ID = 0 if no secondary component). Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public bool UserSetMeasureParameterized(string token,DateTime dateTime,int measureID,int measureComponentID_1,string value_1,int measureComponentID_2,string value_2,int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				bool ok = true;
//				int userProfileID = execIntScal("SELECT UserProfileID FROM [User] WHERE UserID = " + userID);
//				#region Create new UserMeasure
//				exec("INSERT INTO UserMeasure (" +
//				     "UserID, " +
//				     "CreatedDT, " +
//				     "DT," +
//				     "UserProfileID" +
//				     ") VALUES (" +
//				     "" + userID + "," +
//				     "GETDATE()," +
//				     "'" + dateTime.ToString("yyyy-MM-dd HH:mm") + "'," +
//				     "" + userProfileID + "" +
//				     ")");
//				#endregion
//				#region Fetch new UserMeasure
//				int userMeasureID = execIntScal("SELECT TOP 1 " +
//				                                "UserMeasureID " +
//				                                "FROM UserMeasure " +
//				                                "WHERE UserID = " + userID + " " +
//				                                "ORDER BY UserMeasureID DESC");
//				#endregion
//				if (measureComponentID_1 != 0) {
//					int questionType = execIntScal("SELECT " +
//					                               "mc.Type " +
//					                               "FROM MeasureComponent mc " +
//					                               "WHERE mc.MeasureID = " + measureID + " AND mc.MeasureComponentID = " + measureComponentID_1);
//					switch (questionType) {
//						case 4:
//							try {
//								Convert.ToDecimal(value_1.Replace("'", "").Replace(".", ","));
//								exec("INSERT INTO UserMeasureComponent (" +
//								     "UserMeasureID, " +
//								     "MeasureComponentID, " +
//								     "ValDec" +
//								     ") VALUES (" +
//								     "" + userMeasureID + "," +
//								     "" + measureComponentID_1 + "," +
//								     "" + value_1.Replace("'", "").Replace(",", ".") +
//								     ")");
//							} catch (Exception) { ok = false; }
//							break;
//						default:
//							ok = false; break;
//					}
//				}
//				if (measureComponentID_2 != 0) {
//					int questionType = execIntScal("SELECT " +
//					                               "mc.Type " +
//					                               "FROM MeasureComponent mc " +
//					                               "WHERE mc.MeasureID = " + measureID + " AND mc.MeasureComponentID = " + measureComponentID_2);
//					switch (questionType) {
//						case 4:
//							try {
//								Convert.ToDecimal(value_2.Replace("'", "").Replace(".", ","));
//								exec("INSERT INTO UserMeasureComponent (" +
//								     "UserMeasureID, " +
//								     "MeasureComponentID, " +
//								     "ValDec" +
//								     ") VALUES (" +
//								     "" + userMeasureID + "," +
//								     "" + measureComponentID_2 + "," +
//								     "" + value_2.Replace("'", "").Replace(",", ".") +
//								     ")");
//							} catch (Exception) { ok = false; }
//							break;
//						default:
//							ok = false; break;
//					}
//				}
//				return ok;
//			}
//			return false;
//		}
//
//		[WebMethod(Description = "Set measure. Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public bool UserSetMeasure(string token,DateTime dateTime,int measureID,UserMeasureComponent[] umc,int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				bool ok = true;
//				int userProfileID = execIntScal("SELECT UserProfileID FROM [User] WHERE UserID = " + userID);
//				#region Create new UserMeasure
//				exec("INSERT INTO UserMeasure (" +
//				     "UserID, " +
//				     "CreatedDT, " +
//				     "DT," +
//				     "UserProfileID" +
//				     ") VALUES (" +
//				     "" + userID + "," +
//				     "GETDATE()," +
//				     "'" + dateTime.ToString("yyyy-MM-dd HH:mm") + "'," +
//				     "" + userProfileID + "" +
//				     ")");
//				#endregion
//				#region Fetch new UserMeasure
//				int userMeasureID = execIntScal("SELECT TOP 1 " +
//				                                "UserMeasureID " +
//				                                "FROM UserMeasure " +
//				                                "WHERE UserID = " + userID + " " +
//				                                "ORDER BY UserMeasureID DESC");
//				#endregion
//				foreach(UserMeasureComponent a in umc) {
//					int questionType = execIntScal("SELECT " +
//					                               "mc.Type " +
//					                               "FROM MeasureComponent mc " +
//					                               "WHERE mc.MeasureID = " + measureID + " AND mc.MeasureComponentID = " + a.MeasureComponentID);
//					switch (questionType) {
//						case 4:
//							try {
//								Convert.ToDecimal(a.value.Replace("'", "").Replace(".", ","));
//								exec("INSERT INTO UserMeasureComponent (" +
//								     "UserMeasureID, " +
//								     "MeasureComponentID, " +
//								     "ValDec" +
//								     ") VALUES (" +
//								     "" + userMeasureID + "," +
//								     "" + a.MeasureComponentID + "," +
//								     "" + a.value.Replace("'", "").Replace(",", ".") +
//								     ")");
//							} catch (Exception) { ok = false; }
//							break;
//						default:
//							ok = false; break;
//					}
//				}
//				return ok;
//			}
//			return false;
//		}
//
//		[WebMethod(Description = "Get user reminder. " +
//		           "Type 0 = Never, 1 = Regularly, 2 = Inactivity. " +
//		           "AutoLoginLink 0 = No, 1 = Constant, 2 = ExpireWhenUsed. " +
//		           "SendAtHour = hour of day to send reminder, expressed as integer between 6-22. " +
//		           "Regularity 1 = Daily, 2 = Weekly, 3 = Monthly. " +
//		           "RegularityDailyMonday...Sunday = false/true. " +
//		           "RegularityWeeklyDay 1 = Monday, ..., 7 = Sunday. " +
//		           "RegularityWeeklyEvery 1 = Every week, 2 = Every other week, 3 = Every third week. " +
//		           "RegularityMonthlyWeekNr 1 = First, 2 = Second, 3 = Third, 4 = Fourth. " +
//		           "RegularityMonthlyDay 1 = Monday, ..., 7 = Sunday. " +
//		           "RegularityMonthlyEvery 1 = Every, 2 = Every other, 3 = Every third, 6 = Every sixth. " +
//		           "InactivityCount = number of days/weeks/months depending on period, expressed as integer between 1-6. " +
//		           "InactivityPeriod 1 = Day, 7 = Week, 30 = Month. " +
//		           "Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public Reminder UserGetReminder(string token,int expirationMinutes)
//		{
//			Reminder ret = new Reminder();
//
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				SqlDataReader r = rs("SELECT " +
//				                     "u.Username, " +		// 0
//				                     "s.LoginDays, " +		// 1
//				                     "u.Reminder, " +		// 2
//				                     "u.ReminderType, " +	// 3
//				                     "u.ReminderLink, " +	// 4
//				                     "u.ReminderSettings, " +// 5
//				                     "u.Email " +			// 6
//				                     "FROM [User] u " +
//				                     "LEFT OUTER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//				                     "WHERE u.UserID = " + userID);
//				if (r.Read()) {
//					if (!r.IsDBNull(2)) {
//						ret.type = (r.GetInt32(2) == 0 || r.IsDBNull(3) ? 0 : r.GetInt32(3));
//						ret.autoLoginLink = (r.IsDBNull(4) ? 0 : r.GetInt32(4));
//
//						string[] settings = (r.IsDBNull(5) ? "" : r.GetString(5)).Split(':');
//
//						switch (r.IsDBNull(3) ? 0 : r.GetInt32(3)) {
//							case 1:
//								ret.sendAtHour = Convert.ToInt32(settings[0]);
//								ret.regularity = Convert.ToInt32(settings[1]);
//								switch (Convert.ToInt32(settings[1])) {
//									case 1:
//										string[] days = settings[2].Split(',');
//										foreach (string day in days) {
//											switch (Convert.ToInt32(day)) {
//													case 1: ret.regularityDailyMonday = true; break;
//													case 2: ret.regularityDailyTuesday = true; break;
//													case 3: ret.regularityDailyWednesday = true; break;
//													case 4: ret.regularityDailyThursday = true; break;
//													case 5: ret.regularityDailyFriday = true; break;
//													case 6: ret.regularityDailySaturday = true; break;
//													case 7: ret.regularityDailySunday = true; break;
//											}
//										}
//										break;
//									case 2:
//										ret.regularityWeeklyDay = Convert.ToInt32(settings[2]);
//										ret.regularityWeeklyEvery = Convert.ToInt32(settings[3]);
//										break;
//									case 3:
//										ret.regularityMonthlyWeekNr = Convert.ToInt32(settings[2]);
//										ret.regularityMonthlyDay = Convert.ToInt32(settings[3]);
//										ret.regularityMonthlyEvery = Convert.ToInt32(settings[4]);
//										break;
//								}
//								break;
//							case 2:
//								ret.sendAtHour = Convert.ToInt32(settings[0]);
//								ret.inactivityCount = Convert.ToInt32(settings[1]);
//								ret.inactivityPeriod = Convert.ToInt32(settings[2]);
//								break;
//						}
//					}
//				}
//				r.Close();
//			}
//			return ret;
//		}
//
//		[WebMethod(Description = "Set user reminder. Returns true if successful. " +
//		           "Type 0 = Never, 1 = Regularly, 2 = Inactivity. " +
//		           "AutoLoginLink 0 = No, 1 = Constant, 2 = ExpireWhenUsed. " +
//		           "SendAtHour = hour of day to send reminder, expressed as integer between 6-22. " +
//		           "Regularity 1 = Daily, 2 = Weekly, 3 = Monthly. " +
//		           "RegularityDailyMonday...Sunday = false/true. " +
//		           "RegularityWeeklyDay 1 = Monday, ..., 7 = Sunday. " +
//		           "RegularityWeeklyEvery 1 = Every week, 2 = Every other week, 3 = Every third week. " +
//		           "RegularityMonthlyWeekNr 1 = First, 2 = Second, 3 = Third, 4 = Fourth. " +
//		           "RegularityMonthlyDay 1 = Monday, ..., 7 = Sunday. " +
//		           "RegularityMonthlyEvery 1 = Every, 2 = Every other, 3 = Every third, 6 = Every sixth. " +
//		           "InactivityCount = number of days/weeks/months depending on period, expressed as integer between 1-6. " +
//		           "InactivityPeriod 1 = Day, 7 = Week, 30 = Month. " +
//		           "Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public bool UserSetReminder(string token,Reminder reminder,int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				if (reminder.type == 0) {
//					exec("UPDATE [User] SET " +
//					     "Reminder = 0, " +
//					     "ReminderLink = " + reminder.autoLoginLink + ", " +
//					     "ReminderType = " + reminder.type + ", " +
//					     "ReminderNextSend = NULL " +
//					     "WHERE UserID = " + userID);
//				} else {
//					string settings = reminder.sendAtHour.ToString();
//					switch (reminder.type) {
//						case 1:
//							settings += ":" + reminder.regularity.ToString();
//							switch (reminder.regularity) {
//								case 1:
//									string days = "";
//									if (reminder.regularityDailyMonday) days += (days != "" ? "," : "") + "1";
//									if (reminder.regularityDailyTuesday) days += (days != "" ? "," : "") + "2";
//									if (reminder.regularityDailyWednesday) days += (days != "" ? "," : "") + "3";
//									if (reminder.regularityDailyThursday) days += (days != "" ? "," : "") + "4";
//									if (reminder.regularityDailyFriday) days += (days != "" ? "," : "") + "5";
//									if (reminder.regularityDailySaturday) days += (days != "" ? "," : "") + "6";
//									if (reminder.regularityDailySunday) days += (days != "" ? "," : "") + "7";
//									settings += ":" + (days != "" ? days : "1");
//									break;
//								case 2:
//									settings += ":" + reminder.regularityWeeklyDay.ToString();
//									settings += ":" + reminder.regularityWeeklyEvery.ToString();
//									break;
//								case 3:
//									settings += ":" + reminder.regularityMonthlyWeekNr.ToString();
//									settings += ":" + reminder.regularityMonthlyDay.ToString();
//									settings += ":" + reminder.regularityMonthlyEvery.ToString();
//									break;
//							}
//							break;
//						case 2:
//							settings += ":" + reminder.inactivityCount.ToString();
//							settings += ":" + reminder.inactivityPeriod.ToString();
//							break;
//
//					}
//					exec("UPDATE [User] SET " +
//					     (reminder.autoLoginLink == 2 ? "UserKey = NEWID(), " : "") +
//					     "Reminder = 1, " +
//					     "ReminderLink = " + reminder.autoLoginLink + ", " +
//					     "ReminderType = " + reminder.type + ", " +
//					     "ReminderSettings = '" + settings.Replace("'", "") + "', " +
//					     "ReminderNextSend = '" + nextReminderSend(reminder.type, settings.Split(':'), DateTime.Now, DateTime.Now) + "' " +
//					     "WHERE UserID = " + userID);
//				}
//				return true;
//			}
//			return false;
//		}
//
//		[WebMethod(Description = "Set user reminder. Returns true if successful. " +
//		           "Type 0 = Never, 1 = Regularly, 2 = Inactivity. " +
//		           "AutoLoginLink 0 = No, 1 = Constant, 2 = ExpireWhenUsed. " +
//		           "SendAtHour = hour of day to send reminder, expressed as integer between 6-22. " +
//		           "Regularity 1 = Daily, 2 = Weekly, 3 = Monthly. " +
//		           "RegularityDailyMonday...Sunday = false/true. " +
//		           "RegularityWeeklyDay 1 = Monday, ..., 7 = Sunday. " +
//		           "RegularityWeeklyEvery 1 = Every week, 2 = Every other week, 3 = Every third week. " +
//		           "RegularityMonthlyWeekNr 1 = First, 2 = Second, 3 = Third, 4 = Fourth. " +
//		           "RegularityMonthlyDay 1 = Monday, ..., 7 = Sunday. " +
//		           "RegularityMonthlyEvery 1 = Every, 2 = Every other, 3 = Every third, 6 = Every sixth. " +
//		           "InactivityCount = number of days/weeks/months depending on period, expressed as integer between 1-6. " +
//		           "InactivityPeriod 1 = Day, 7 = Week, 30 = Month. " +
//		           "Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public bool UserSetReminderParameterized(string token, int type, int autoLoginLink, int sendAtHour, int regularity,bool regularityDailyMonday,bool regularityDailyTuesday,bool regularityDailyWednesday,bool regularityDailyThursday,bool regularityDailyFriday,bool regularityDailySaturday,bool regularityDailySunday,int regularityWeeklyDay,int regularityWeeklyEvery,int regularityMonthlyWeekNr,int regularityMonthlyDay,int regularityMonthlyEvery,int inactivityCount,int inactivityPeriod,int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				if (type == 0) {
//					exec("UPDATE [User] SET " +
//					     "Reminder = 0, " +
//					     "ReminderLink = " + autoLoginLink + ", " +
//					     "ReminderType = " + type + ", " +
//					     "ReminderNextSend = NULL " +
//					     "WHERE UserID = " + userID);
//				} else {
//					string settings = sendAtHour.ToString();
//					switch (type) {
//						case 1:
//							settings += ":" + regularity.ToString();
//							switch (regularity) {
//								case 1:
//									string days = "";
//									if (regularityDailyMonday) days += (days != "" ? "," : "") + "1";
//									if (regularityDailyTuesday) days += (days != "" ? "," : "") + "2";
//									if (regularityDailyWednesday) days += (days != "" ? "," : "") + "3";
//									if (regularityDailyThursday) days += (days != "" ? "," : "") + "4";
//									if (regularityDailyFriday) days += (days != "" ? "," : "") + "5";
//									if (regularityDailySaturday) days += (days != "" ? "," : "") + "6";
//									if (regularityDailySunday) days += (days != "" ? "," : "") + "7";
//									settings += ":" + (days != "" ? days : "1");
//									break;
//								case 2:
//									settings += ":" + regularityWeeklyDay.ToString();
//									settings += ":" + regularityWeeklyEvery.ToString();
//									break;
//								case 3:
//									settings += ":" + regularityMonthlyWeekNr.ToString();
//									settings += ":" + regularityMonthlyDay.ToString();
//									settings += ":" + regularityMonthlyEvery.ToString();
//									break;
//							}
//							break;
//						case 2:
//							settings += ":" + inactivityCount.ToString();
//							settings += ":" + inactivityPeriod.ToString();
//							break;
//
//					}
//					exec("UPDATE [User] SET " +
//					     (autoLoginLink == 2 ? "UserKey = NEWID(), " : "") +
//					     "Reminder = 1, " +
//					     "ReminderLink = " + autoLoginLink + ", " +
//					     "ReminderType = " + type + ", " +
//					     "ReminderSettings = '" + settings.Replace("'", "") + "', " +
//					     "ReminderNextSend = '" + nextReminderSend(type, settings.Split(':'), DateTime.Now, DateTime.Now) + "' " +
//					     "WHERE UserID = " + userID);
//				}
//				return true;
//			}
//			return false;
//		}
//
//		[WebMethod(Description = "Enumerates measurements. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public Measure[] MeasureEnum(string token, int measureCategoryID, int languageID, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int cx = execIntScal("SELECT COUNT(*) " +
//				                     "FROM Measure m " +
//				                     "INNER JOIN MeasureCategory mc ON m.MeasureCategoryID = mc.MeasureCategoryID " +
//				                     "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + languageID + " " +
//				                     "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
//				                     "WHERE mc.SPRUID IS NULL " + (measureCategoryID != 0 ? "AND m.MeasureCategoryID = " + measureCategoryID + " " : "") +
//				                     "");
//				Measure[] ret = new Measure[cx];
//				cx = 0;
//
//				SqlDataReader r = rs("SELECT " +
//				                     "m.MeasureID, " +
//				                     "ISNULL(ml.Measure,m.Measure), " +
//				                     "(SELECT COUNT(*) FROM MeasureComponent mc WHERE mc.MeasureID = m.MeasureID), " +
//				                     "m.MoreInfo, " +
//				                     "mc.MeasureCategoryID, " +
//				                     "ISNULL(mcl.MeasureCategory,mc.MeasureCategory) " +
//				                     "FROM Measure m " +
//				                     "INNER JOIN MeasureCategory mc ON m.MeasureCategoryID = mc.MeasureCategoryID " +
//				                     "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + languageID + " " +
//				                     "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
//				                     "WHERE mc.SPRUID IS NULL " + (measureCategoryID != 0 ? "AND m.MeasureCategoryID = " + measureCategoryID + " " : "") +
//				                     "ORDER BY m.MeasureCategoryID, m.SortOrder");
//				while (r.Read()) {
//					ret[cx].measureID = r.GetInt32(0);
//					ret[cx].measure = r.GetString(1);
//					ret[cx].componentCount = r.GetInt32(2);
//					if (!r.IsDBNull(3)) {
//						ret[cx].moreInfo = r.GetString(3);
//					}
//					ret[cx].measureCategoryID = r.GetInt32(4);
//					ret[cx].measureCategory = r.GetString(5);
//
//					MeasureComponent[] mc = new MeasureComponent[r.GetInt32(2)];
//
//					int dx = 0;
//					SqlDataReader r2 = rs("SELECT " +
//					                      "mc.MeasureComponentID, " +
//					                      "ISNULL(mcl.MeasureComponent,mc.MeasureComponent), " +
//					                      "mc.Type, " +
//					                      "ISNULL(mcl.Unit,mc.Unit), " +
//					                      "mc.Inherit, " +
//					                      "mc.AutoScript, " +            // 5
//					                      "(SELECT COUNT(*) FROM MeasureComponentPart mcp WHERE mcp.MeasureComponentPart = mc.MeasureComponentID), " +
//					                      "mc.Decimals " +
//					                      "FROM MeasureComponent mc " +
//					                      "LEFT OUTER JOIN MeasureComponentLang mcl ON mc.MeasureComponentID = mcl.MeasureComponentID AND mcl.LangID = " + languageID + " " +
//					                      "WHERE mc.MeasureID = " + r.GetInt32(0) + " " +
//					                      "ORDER BY mc.SortOrder");
//					while (r2.Read()) {
//						mc[dx].measureComponentID = r2.GetInt32(0);
//						mc[dx].measureComponent = r2.GetString(1);
//						mc[dx].questionType = (QuestionTypes)r2.GetInt32(2);
//						if (!r2.IsDBNull(3)) {
//							mc[dx].unit = r2.GetString(3);
//						}
//						mc[dx].inherited = (!r2.IsDBNull(4) && r2.GetInt32(4) == 1);
//						if (r2.GetInt32(4) == 1) {
//							string val = "";
//							SqlDataReader r3 = rs("SELECT TOP 1 c.ValDec FROM UserMeasureComponent c INNER JOIN UserMeasure m ON c.UserMeasureID = m.UserMeasureID WHERE c.MeasureComponentID = " + r2.GetInt32(0) + " AND m.UserID = " + userID + " AND m.DeletedDT IS NULL ORDER BY m.DT DESC");
//							if (r3.Read()) {
//								val += Math.Round(r3.GetDecimal(0), r2.GetInt32(7));
//							}
//							r3.Close();
//							mc[dx].inheritedValue = val;
//						}
//						mc[dx].hasAutoCalculateChildren = (r2.GetInt32(6) > 0);
//						mc[dx].isAutoCalculated = (!r2.IsDBNull(5));
//						if (!r2.IsDBNull(5)) {
//							string scr = "";
//							SqlDataReader r3 = rs("SELECT p.MeasureComponentPart, c.MeasureID FROM MeasureComponentPart p INNER JOIN MeasureComponent c ON p.MeasureComponentPart = c.MeasureComponentID WHERE p.MeasureComponentID = " + r2.GetInt32(0) + " ORDER BY p.SortOrder");
//							while (r3.Read()) {
//								scr += (scr != "" ? "," : "") + "'M" + r3.GetInt32(1) + "C" + r3.GetInt32(0) + "'";
//							}
//							r3.Close();
//							mc[dx].autoCalculateScript = "function MCPS" + r2.GetInt32(0) + "(){" + r2.GetString(5) + "}function MCP" + r2.GetInt32(0) + "(){document.forms[0].M" + r.GetInt32(0) + "C" + r2.GetInt32(0) + ".value=MCPS" + r2.GetInt32(0) + "(" + scr + ");}";
//						}
//						mc[dx].decimals = r2.GetInt32(7);
//						string auto = "";
//						if (r2.GetInt32(6) != 0) {
//							SqlDataReader r3 = rs("SELECT MeasureComponentID FROM MeasureComponentPart WHERE MeasureComponentPart = " + r2.GetInt32(0));
//							while (r3.Read()) {
//								auto += "MCP" + r3.GetInt32(0) + "();";
//							}
//							r3.Close();
//						}
//						if (!r2.IsDBNull(5)) {
//							auto += "MCP" + r2.GetInt32(0) + "();";
//						}
//						if (auto != "") {
//							mc[dx].triggerScript = auto;
//						}
//						dx++;
//					}
//					r2.Close();
//
//					ret[cx].measureComponents = mc;
//
//					cx++;
//				}
//				r.Close();
//
//				return ret;
//			}
//			return (new Measure[0]);
//		}
//
//		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates measurement categories. Set measureTypeID = 0 for all. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public MeasureCategory[] MeasureCategoryEnum(string token, int measureTypeID, int languageID, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int sponsorID = execIntScal("SELECT SponsorID FROM [User] WHERE UserID = " + userID);
//
//				int cx = execIntScal("SELECT SUM(tmp.CX) FROM (" +
//				                     "SELECT COUNT(*) AS CX " +
//				                     "FROM MeasureCategory mc " +
//				                     "INNER JOIN MeasureType mt ON mc.MeasureTypeID = mt.MeasureTypeID " +
//				                     "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
//				                     "LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + languageID + " " +
//				                     "WHERE mc.SPRUID IS NULL " +
//				                     (measureTypeID != 0 ? "AND mc.MeasureTypeID = " + measureTypeID + " " : "") +
//				                     //"UNION ALL " +
//				                     //"SELECT COUNT(*) AS CX " +
//				                     //"FROM MeasureCategory mc " +
//				                     //"INNER JOIN MeasureType mt ON mc.MeasureTypeID = mt.MeasureTypeID " +
//				                     //"INNER JOIN SponsorProjectRoundUnit spru ON mc.SPRUID = spru.SponsorProjectRoundUnitID AND spru.SponsorID = " + sponsorID + " " +
//				                     //"LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
//				                     //"LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + languageID + " " +
//				                     //(measureTypeID != 0 ? "WHERE mc.MeasureTypeID = " + measureTypeID + " " : "") +
//				                     ") tmp");
//				MeasureCategory[] ret = new MeasureCategory[cx];
//				cx = 0;
//
//				SqlDataReader r = rs("SELECT " +
//				                     "mc.MeasureCategoryID, " +       // 0
//				                     "ISNULL(mcl.MeasureCategory,mc.MeasureCategory), " +
//				                     "NULL, " +
//				                     "mc.SortOrder AS SO, " +
//				                     "mt.MeasureTypeID, " +
//				                     "ISNULL(mtl.MeasureType,mt.MeasureType) " +
//				                     "FROM MeasureCategory mc " +
//				                     "INNER JOIN MeasureType mt ON mc.MeasureTypeID = mt.MeasureTypeID " +
//				                     "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
//				                     "LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + languageID + " " +
//				                     "WHERE mc.SPRUID IS NULL " +
//				                     (measureTypeID != 0 ? "AND mc.MeasureTypeID = " + measureTypeID + " " : "") +
//				                     //"UNION ALL " +
//				                     //"SELECT " +
//				                     //"mc.MeasureCategoryID, " +       // 0
//				                     //"ISNULL(mcl.MeasureCategory,mc.MeasureCategory), " +
//				                     //"REPLACE(CONVERT(VARCHAR(255),spru.SurveyKey),'-',''), " +
//				                     //"mc.SortOrder AS SO, " +
//				                     //"mt.MeasureTypeID, " +
//				                     //"ISNULL(mtl.MeasureType,mt.MeasureType) " +
//				                     //"FROM MeasureCategory mc " +
//				                     //"INNER JOIN MeasureType mt ON mc.MeasureTypeID = mt.MeasureTypeID " +
//				                     //"INNER JOIN SponsorProjectRoundUnit spru ON mc.SPRUID = spru.SponsorProjectRoundUnitID AND spru.SponsorID = " + sponsorID + " " +
//				                     //"LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
//				                     //"LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + languageID + " " +
//				                     //(measureTypeID != 0 ? "WHERE mc.MeasureTypeID = " + measureTypeID + " " : "") +
//				                     "ORDER BY SO");
//				while (r.Read()) {
//					ret[cx].measureCategoryID = r.GetInt32(0);
//					ret[cx].measureCategory = r.GetString(1);
//					ret[cx].measureTypeID = r.GetInt32(4);
//					ret[cx].measureType = r.GetString(5);
//					cx++;
//				}
//				r.Close();
//
//				return ret;
//			}
//			return (new MeasureCategory[0]);
//		}
//
//		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates measurement types. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public MeasureType[] MeasureTypeEnum(string token, int languageID, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int cx = execIntScal("SELECT COUNT(*) " +
//				                     "FROM MeasureType mt " +
//				                     "LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + languageID + " " +
//				                     "WHERE Active = 1 " +
//				                     "AND (SELECT COUNT(*) FROM MeasureCategory mc WHERE mt.MeasureTypeID = mc.MeasureTypeID AND mc.SPRUID IS NULL) > 0 " +
//				                     "");
//				MeasureType[] ret = new MeasureType[cx];
//				cx = 0;
//
//				SqlDataReader r = rs("SELECT " +
//				                     "mt.MeasureTypeID, " +       // 0
//				                     "ISNULL(mtl.MeasureType,mt.MeasureType) " +
//				                     "FROM MeasureType mt " +
//				                     "LEFT OUTER JOIN MeasureTypeLang mtl ON mt.MeasureTypeID = mtl.MeasureTypeID AND mtl.LangID = " + languageID + " " +
//				                     "WHERE Active = 1 " +
//				                     "AND (SELECT COUNT(*) FROM MeasureCategory mc WHERE mt.MeasureTypeID = mc.MeasureTypeID AND mc.SPRUID IS NULL) > 0 " +
//				                     "ORDER BY mt.SortOrder");
//				while (r.Read()) {
//					ret[cx].measureTypeID = r.GetInt32(0);
//					ret[cx].measureType = r.GetString(1);
//					cx++;
//				}
//				r.Close();
//
//				return ret;
//			}
//			return (new MeasureType[0]);
//		}
//
//		[WebMethod(Description = "Set or update a calendar entry. Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public bool CalendarUpdate(string token, Mood mood, string note, DateTime date, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				bool oldNoteIdentical = false;
//				SqlDataReader r = rs("SELECT " +
//				                     "DiaryID, " +
//				                     "DiaryNote, " +
//				                     "Mood " +
//				                     "FROM Diary " +
//				                     "WHERE DeletedDT IS NULL " +
//				                     "AND DiaryDate = '" + date.ToString("yyyy-MM-dd") + "' " +
//				                     "AND UserID = " + userID);
//				if (r.Read()) {
//					if ((r.IsDBNull(1) ? "" : r.GetString(1)) != note || (r.IsDBNull(2) ? 0 : r.GetInt32(2)) != (int)mood) {
//						exec("UPDATE Diary SET DeletedDT = GETDATE() WHERE DiaryID = " + r.GetInt32(0));
//					} else {
//						oldNoteIdentical = true;
//					}
//				}
//				r.Close();
//				if ((note != "" || mood != 0) && !oldNoteIdentical) {
//					exec("INSERT INTO Diary (DiaryNote, DiaryDate, UserID, Mood) VALUES ('" + note.Replace("'", "''") + "','" + date.ToString("yyyy-MM-dd") + "'," + userID + "," + (int)mood + ")");
//				}
//				return true;
//			}
//			return false;
//		}
//
//		[WebMethod(Description = "Gets calendar info. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public Calendar[] CalendarEnum(string token, DateTime fromDT, DateTime toDT, int languageID, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int cx = execIntScal("SELECT COUNT(DISTINCT tmp.M) FROM (" +
//				                     "SELECT " +
//				                     "dbo.cf_yearMonthDay(um.DT) AS M " +
//				                     "FROM UserMeasure um " +
//				                     "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
//				                     "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
//				                     "INNER JOIN Measure m ON mc.MeasureID = m.MeasureID " +
//				                     "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + languageID + " " +
//				                     "WHERE mc.ShowInList = 1 AND um.DeletedDT IS NULL AND um.UserID = " + userID + " " +
//				                     "AND dbo.cf_yearMonthDay(um.DT) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
//				                     "AND dbo.cf_yearMonthDay(um.DT) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +
//
//				                     "UNION ALL " +
//
//				                     "SELECT " +
//				                     "dbo.cf_yearMonthDay(es.DateTime) AS M " +
//				                     "FROM ExerciseStats es " +
//				                     "INNER JOIN ExerciseVariantLang evl ON es.ExerciseVariantLangID = evl.ExerciseVariantLangID " +
//				                     "INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
//				                     "INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
//				                     "LEFT OUTER JOIN ExerciseLang el ON e.ExerciseID = el.ExerciseID AND el.Lang = " + (languageID - 1) + " " +
//				                     "WHERE es.UserID = " + userID + " " +
//				                     "AND dbo.cf_yearMonthDay(es.DateTime) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
//				                     "AND dbo.cf_yearMonthDay(es.DateTime) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +
//
//				                     "UNION ALL " +
//
//				                     "SELECT " +
//				                     "dbo.cf_yearMonthDay(uprua.DT) AS M " +
//				                     "FROM UserProjectRoundUser upru " +
//				                     "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
//				                     "INNER JOIN [User] u ON upru.UserID = u.UserID " +
//				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//				                     "INNER JOIN SponsorProjectRoundUnit spru ON upru.ProjectRoundUnitID = spru.ProjectRoundUnitID AND s.SponsorID = spru.SponsorID " +
//				                     "INNER JOIN MeasureCategory mc ON spru.SponsorProjectRoundUnitID = mc.SPRUID " +
//				                     "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
//				                     "WHERE upru.UserID = " + userID + " " +
//				                     "AND dbo.cf_yearMonthDay(uprua.DT) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
//				                     "AND dbo.cf_yearMonthDay(uprua.DT) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +
//
//				                     "UNION ALL " +
//
//				                     "SELECT " +
//				                     "dbo.cf_yearMonthDay(d.DiaryDate) AS M " +
//				                     "FROM Diary d " +
//				                     "WHERE d.UserID = " + userID + " " +
//				                     "AND dbo.cf_yearMonthDay(d.DiaryDate) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
//				                     "AND dbo.cf_yearMonthDay(d.DiaryDate) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +
//				                     ") tmp");
//
//				Calendar[] ret = new Calendar[cx];
//				cx = 0;
//
//				DateTime oldDT = DateTime.MinValue;
//				SqlDataReader r = rs("SELECT DISTINCT " +
//				                     "dbo.cf_yearMonthDay(um.DT) AS M " +
//				                     "FROM UserMeasure um " +
//				                     "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
//				                     "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
//				                     "INNER JOIN Measure m ON mc.MeasureID = m.MeasureID " +
//				                     "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + languageID + " " +
//				                     "WHERE mc.ShowInList = 1 AND um.DeletedDT IS NULL AND um.UserID = " + userID + " " +
//				                     "AND dbo.cf_yearMonthDay(um.DT) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
//				                     "AND dbo.cf_yearMonthDay(um.DT) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +
//
//				                     "UNION ALL " +
//
//				                     "SELECT DISTINCT " +
//				                     "dbo.cf_yearMonthDay(es.DateTime) AS M " +
//				                     "FROM ExerciseStats es " +
//				                     "INNER JOIN ExerciseVariantLang evl ON es.ExerciseVariantLangID = evl.ExerciseVariantLangID " +
//				                     "INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
//				                     "INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
//				                     "LEFT OUTER JOIN ExerciseLang el ON e.ExerciseID = el.ExerciseID AND el.Lang = " + (languageID - 1) + " " +
//				                     "WHERE es.UserID = " + userID + " " +
//				                     "AND dbo.cf_yearMonthDay(es.DateTime) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
//				                     "AND dbo.cf_yearMonthDay(es.DateTime) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +
//
//				                     "UNION ALL " +
//
//				                     "SELECT DISTINCT " +
//				                     "dbo.cf_yearMonthDay(uprua.DT) AS M " +
//				                     "FROM UserProjectRoundUser upru " +
//				                     "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
//				                     "INNER JOIN [User] u ON upru.UserID = u.UserID " +
//				                     "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//				                     "INNER JOIN SponsorProjectRoundUnit spru ON upru.ProjectRoundUnitID = spru.ProjectRoundUnitID AND s.SponsorID = spru.SponsorID " +
//				                     "INNER JOIN MeasureCategory mc ON spru.SponsorProjectRoundUnitID = mc.SPRUID " +
//				                     "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
//				                     "WHERE upru.UserID = " + userID + " " +
//				                     "AND dbo.cf_yearMonthDay(uprua.DT) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
//				                     "AND dbo.cf_yearMonthDay(uprua.DT) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +
//
//				                     "UNION ALL " +
//
//				                     "SELECT DISTINCT " +
//				                     "dbo.cf_yearMonthDay(d.DiaryDate) AS M " +
//				                     "FROM Diary d " +
//				                     "WHERE d.UserID = " + userID + " " +
//				                     "AND dbo.cf_yearMonthDay(d.DiaryDate) >= '" + fromDT.ToString("yyyy-MM-dd") + "' " +
//				                     "AND dbo.cf_yearMonthDay(d.DiaryDate) <= '" + toDT.ToString("yyyy-MM-dd") + "' " +
//
//				                     "ORDER BY M DESC");
//				while (r.Read()) {
//					DateTime dt = DateTime.ParseExact(r.GetString(0), "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
//					if (dt != oldDT) {
//						ret[cx].date = dt;
//
//						SqlDataReader r2 = rs("SELECT " +
//						                      "DiaryNote, " +
//						                      "Mood " +
//						                      "FROM Diary " +
//						                      "WHERE DeletedDT IS NULL " +
//						                      "AND UserID = " + userID + " " +
//						                      "AND DiaryDate = '" + dt.ToString("yyyy-MM-dd") + "'");
//						while (r2.Read()) {
//							if (!r2.IsDBNull(0) && r2.GetString(0) != "") {
//								ret[cx].note = r2.GetString(0);
//							}
//							if (!r2.IsDBNull(1) && r2.GetInt32(1) != 0) {
//								ret[cx].mood = (Mood)r2.GetInt32(1);
//							}
//						}
//						r2.Close();
//
//						#region Events
//						int eventCX = execIntScal("SELECT COUNT(*) FROM (" +
//						                          "SELECT DISTINCT " +
//						                          "um.UserMeasureID AS A1, " +
//						                          "dbo.cf_hourMinute(um.DT) AS M, " +
//						                          "ISNULL(ml.Measure,m.Measure) AS A2, " +
//						                          "m.SortOrder AS S, " +
//						                          "(SELECT COUNT(*) FROM MeasureComponent x WHERE x.MeasureID = m.MeasureID) AS A3, " +
//						                          "m.MeasureID AS A4, " +
//						                          "NULL AS A5, " +
//						                          "NULL AS A6 " +
//						                          "FROM UserMeasure um " +
//						                          "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
//						                          "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
//						                          "INNER JOIN Measure m ON mc.MeasureID = m.MeasureID " +
//						                          "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + languageID + " " +
//						                          "WHERE mc.ShowInList = 1 AND um.DeletedDT IS NULL AND um.UserID = " + userID + " " +
//						                          "AND dbo.cf_yearMonthDay(um.DT) = '" + dt.ToString("yyyy-MM-dd") + "' " +
//
//						                          "UNION ALL " +
//
//						                          "SELECT DISTINCT " +
//						                          "NULL AS A1, " +
//						                          "dbo.cf_hourMinute(es.DateTime) AS M, " +
//						                          "el.Exercise AS A2, " +
//						                          "e.ExerciseSortOrder+1000 AS S, " +
//						                          "NULL AS A3, " +
//						                          "NULL AS A4, " +
//						                          "NULL AS A5, " +
//						                          "NULL AS A6 " +
//						                          "FROM ExerciseStats es " +
//						                          "INNER JOIN ExerciseVariantLang evl ON es.ExerciseVariantLangID = evl.ExerciseVariantLangID " +
//						                          "INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
//						                          "INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
//						                          "LEFT OUTER JOIN ExerciseLang el ON e.ExerciseID = el.ExerciseID AND el.Lang = " + (languageID - 1) + " " +
//						                          "WHERE es.UserID = " + userID + " " +
//						                          "AND dbo.cf_yearMonthDay(es.DateTime) = '" + dt.ToString("yyyy-MM-dd") + "' " +
//
//						                          "UNION ALL " +
//
//						                          "SELECT DISTINCT " +
//						                          "NULL AS A1, " +
//						                          "dbo.cf_hourMinute(uprua.DT) AS M, " +
//						                          "ISNULL(mcl.MeasureCategory,mc.MeasureCategory) AS A2, " +
//						                          "mc.SortOrder+500 AS S, " +
//						                          "NULL AS A3, " +
//						                          "NULL AS A4, " +
//						                          "uprua.AnswerKey AS A5, " +
//						                          "uprua.UserProjectRoundUserAnswerID AS A6 " +
//						                          "FROM UserProjectRoundUser upru " +
//						                          "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
//						                          "INNER JOIN [User] u ON upru.UserID = u.UserID " +
//						                          "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//						                          "INNER JOIN SponsorProjectRoundUnit spru ON upru.ProjectRoundUnitID = spru.ProjectRoundUnitID AND s.SponsorID = spru.SponsorID " +
//						                          "INNER JOIN MeasureCategory mc ON spru.SponsorProjectRoundUnitID = mc.SPRUID " +
//						                          "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
//						                          "WHERE upru.UserID = " + userID + " " +
//						                          "AND dbo.cf_yearMonthDay(uprua.DT) = '" + dt.ToString("yyyy-MM-dd") + "' " +
//
//						                          ") tmp");
//
//						if (eventCX > 0) {
//							Event[] events = new Event[eventCX];
//
//							eventCX = 0;
//
//							r2 = rs("SELECT DISTINCT " +
//							        "um.UserMeasureID, " +
//							        "dbo.cf_hourMinute(um.DT) AS M, " +
//							        "ISNULL(ml.Measure,m.Measure), " +
//							        "m.SortOrder AS S, " +
//							        "(SELECT COUNT(*) FROM MeasureComponent x WHERE x.MeasureID = m.MeasureID), " +
//							        "m.MeasureID, " +
//							        "NULL, " +
//							        "NULL " +
//							        "FROM UserMeasure um " +
//							        "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
//							        "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
//							        "INNER JOIN Measure m ON mc.MeasureID = m.MeasureID " +
//							        "LEFT OUTER JOIN MeasureLang ml ON m.MeasureID = ml.MeasureID AND ml.LangID = " + languageID + " " +
//							        "WHERE mc.ShowInList = 1 AND um.DeletedDT IS NULL AND um.UserID = " + userID + " " +
//							        "AND dbo.cf_yearMonthDay(um.DT) = '" + dt.ToString("yyyy-MM-dd") + "' " +
//
//							        "UNION ALL " +
//
//							        "SELECT DISTINCT " +
//							        "NULL, " +
//							        "dbo.cf_hourMinute(es.DateTime) AS M, " +
//							        "el.Exercise, " +
//							        "e.ExerciseSortOrder+1000 AS S, " +
//							        "NULL, " +
//							        "NULL, " +
//							        "NULL, " +
//							        "NULL " +
//							        "FROM ExerciseStats es " +
//							        "INNER JOIN ExerciseVariantLang evl ON es.ExerciseVariantLangID = evl.ExerciseVariantLangID " +
//							        "INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
//							        "INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
//							        "LEFT OUTER JOIN ExerciseLang el ON e.ExerciseID = el.ExerciseID AND el.Lang = " + (languageID - 1) + " " +
//							        "WHERE es.UserID = " + userID + " " +
//							        "AND dbo.cf_yearMonthDay(es.DateTime) = '" + dt.ToString("yyyy-MM-dd") + "' " +
//
//							        "UNION ALL " +
//
//							        "SELECT DISTINCT " +
//							        "NULL, " +
//							        "dbo.cf_hourMinute(uprua.DT) AS M, " +
//							        "ISNULL(mcl.MeasureCategory,mc.MeasureCategory), " +
//							        "mc.SortOrder+500 AS S, " +
//							        "NULL, " +
//							        "NULL, " +
//							        "uprua.AnswerKey, " +
//							        "uprua.UserProjectRoundUserAnswerID " +
//							        "FROM UserProjectRoundUser upru " +
//							        "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
//							        "INNER JOIN [User] u ON upru.UserID = u.UserID " +
//							        "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
//							        "INNER JOIN SponsorProjectRoundUnit spru ON upru.ProjectRoundUnitID = spru.ProjectRoundUnitID AND s.SponsorID = spru.SponsorID " +
//							        "INNER JOIN MeasureCategory mc ON spru.SponsorProjectRoundUnitID = mc.SPRUID " +
//							        "LEFT OUTER JOIN MeasureCategoryLang mcl ON mc.MeasureCategoryID = mcl.MeasureCategoryID AND mcl.LangID = " + languageID + " " +
//							        "WHERE upru.UserID = " + userID + " " +
//							        "AND dbo.cf_yearMonthDay(uprua.DT) = '" + dt.ToString("yyyy-MM-dd") + "' " +
//
//							        "ORDER BY M, S");
//							while (r2.Read()) {
//								DateTime eventDT = DateTime.ParseExact(r.GetString(0) + " " + r2.GetString(1), "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.CurrentCulture);
//								events[eventCX].time = eventDT;
//								events[eventCX].description = r2.GetString(2);
//
//								if (!r2.IsDBNull(6) && !r2.IsDBNull(7)) {
//									events[eventCX].type = EventType.Form;
//									events[eventCX].formInstanceKey = r2.GetString(6);
//									events[eventCX].eventID = r2.GetInt32(7);
//								} else if (!r2.IsDBNull(0)) {
//									events[eventCX].type = EventType.Measurement;
//									events[eventCX].eventID = r2.GetInt32(0);
//
//									int dx = 0; string res = "";
//									SqlDataReader r3 = rs("SELECT " +
//									                      "umc.ValInt, " +
//									                      "umc.ValDec, " +
//									                      "umc.ValTxt, " +
//									                      "ISNULL(mcl.MeasureComponent,mc.MeasureComponent), " +
//									                      "ISNULL(mcl.Unit,mc.Unit), " +
//									                      "mc.Type, " +
//									                      "mc.Decimals, " +
//									                      "mc.ShowUnitInList " +
//									                      "FROM UserMeasure um " +
//									                      "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
//									                      "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
//									                      "LEFT OUTER JOIN MeasureComponentLang mcl ON mc.MeasureComponentID = mcl.MeasureComponentID AND mcl.LangID = " + languageID + " " +
//									                      "WHERE mc.ShowInList = 1 AND um.UserMeasureID = " + r2.GetInt32(0) + " " +
//									                      "ORDER BY mc.SortOrder");
//									while (r3.Read()) {
//										if (dx++ > 0) {
//											res += " / ";
//										}
//										switch (r3.GetInt32(5)) {
//												case 4: res += Math.Round(r3.GetDecimal(1), r3.GetInt32(6)).ToString() + (r3.GetInt32(7) == 1 ? r3.GetString(4) : ""); break;
//										}
//
//									}
//									r3.Close();
//
//									events[eventCX].result = res;
//								} else {
//									events[eventCX].type = EventType.Exercise;
//								}
//
//								//if (edit)
//								//{
//								//    if (!rs.IsDBNull(0))
//								//    {
//								//        //actsBox.InnerHtml += "" +
//								//        //    "<IMG ONCLICK=\"actG('" + rs.GetInt32(0) + "'," + ld[rs.GetInt32(5)] + ",'0');\" STYLE=\"cursor:pointer;cursor:hand;\" ALT=\"Idag\" SRC=\"img/graphIcon3.gif\" BORDER=\"0\">" +
//								//        //    "&nbsp;" +
//								//        //    "<IMG ONCLICK=\"actG('" + rs.GetInt32(0) + "'," + ld[rs.GetInt32(5)] + ",'1');\" STYLE=\"cursor:pointer;cursor:hand;\" ALT=\"Över tid\" SRC=\"img/graphIcon2.gif\" BORDER=\"0\">" +
//								//        //    "&nbsp;" +
//								//        sb.Append("<a class=\"remove\" href=\"javascript:if(confirm('");
//								//        switch (LID)
//								//        {
//								//            case 1:
//								//                sb.Append("Är du säker på att du vill ta bort detta värde?");
//								//                break;
//								//            case 2:
//								//                sb.Append("Are you sure you want to remove this value?");
//								//                break;
//								//        }
//								//        sb.Append("')){document.forms[0].DeleteUMID.value='" + rs.GetInt32(0) + "';__doPostBack('','');}\"></a>");
//								//    }
//								//    else if (!rs.IsDBNull(6))
//								//    {
//								//        //actsBox.InnerHtml += "" +
//								//        //    "<A HREF=\"statistics.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&AK=" + rs.GetString(6) + "\" STYLE=\"cursor:pointer;cursor:hand;\"><IMG BORDER=\"0\" SRC=\"img/graphIcon2.gif\" BORDER=\"0\"></A>" +
//								//        //    "&nbsp;" +
//								//        sb.Append("<a class=\"remove\" href=\"javascript:if(confirm('");
//								//        switch (LID)
//								//        {
//								//            case 1:
//								//                sb.Append("Är du säker på att du vill ta bort denna mätning?");
//								//                break;
//								//            case 2:
//								//                sb.Append("Are you sure you want to remove this measurement?");
//								//                break;
//								//        }
//								//        sb.Append("')){document.forms[0].DeleteUPRUA.value='" + rs.GetInt32(7) + ":" + rs.GetString(6) + "';__doPostBack('','');}\"></a>");
//								//    }
//								//}
//								////actsBox.InnerHtml += "</TD></TR>";
//								//if (!rs.IsDBNull(6))
//								//{
//								//    sb.Append("<a href=\"statistics.aspx?AK=" + rs.GetString(6) + "\" class=\"statstoggle\"></a>");
//								//}
//								//sb.Append("</div>");
//								//}
//								//while (rs.Read());
//
//								eventCX++;
//							}
//							r2.Close();
//
//							ret[cx].events = events;
//						}
//						#endregion
//
//						oldDT = dt;
//						cx++;
//					}
//				}
//				r.Close();
//
//				return ret;
//			}
//			return (new Calendar[0]);
//		}
//
//		[WebMethod(Description = "Gets exercise info and increments exercise count for user. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public Exercise ExerciseExec(string token, int exerciseVariantLangID, int expirationMinutes)
//		{
//			Exercise ret = new Exercise();
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				SqlDataReader r = rs("SELECT " +
//				                     "el.Exercise, " +
//				                     "evl.ExerciseFile, " +
//				                     "et.ExerciseTypeID, " +
//				                     "evl.ExerciseContent, " +
//				                     "e.PrintOnBottom, " +
//				                     "e.ReplacementHead, " +
//				                     "eal.ExerciseArea, " +
//				                     "eal.ExerciseAreaID, " +
//				                     "el.ExerciseTime " +
//				                     "FROM [ExerciseVariantLang] evl " +
//				                     "INNER JOIN [ExerciseVariant] ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
//				                     "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
//				                     "INNER JOIN [ExerciseLang] el ON ev.ExerciseID = el.ExerciseID AND el.Lang = evl.Lang " +
//				                     "INNER JOIN [Exercise] e ON el.ExerciseID = e.ExerciseID " +
//				                     "INNER JOIN [ExerciseArea] ea ON ea.ExerciseAreaID = e.ExerciseAreaID " +
//				                     "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID AND eal.Lang = evl.Lang " +
//				                     "WHERE evl.ExerciseVariantLangID = " + exerciseVariantLangID);
//				if (r.Read()) {
//					ret.exerciseHeader = r.GetString(0);
//					ret.exerciseContent = r.GetString(3);
//					ret.exerciseArea = r.GetString(6);
//					ret.exerciseAreaID = r.GetInt32(7);
//					ret.exerciseTime = r.GetString(8);
//
//					exec("INSERT INTO [ExerciseStats] (" +
//					     "ExerciseVariantLangID, " +
//					     "UserID, " +
//					     "UserProfileID" +
//					     ") VALUES (" +
//					     "" + exerciseVariantLangID + "," +
//					     "" + userID + "," +
//					     "" + execIntScal("SELECT UserProfileID FROM [User] WHERE UserID = " + userID) + "" +
//					     ")");
//				}
//				r.Close();
//			}
//			return ret;
//		}
//
//		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates exercises. Set ExerciseAreaID = 0 to show all. Type 0 = All, 1 = Containing short exercises, 2 = Containing long exercises. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public ExerciseInfo[] ExerciseEnum(string token, int exerciseAreaID, int type, int languageID, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int cx = execIntScal("SELECT COUNT(*) " +
//				                     "FROM [ExerciseArea] ea " +
//				                     "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID " +
//				                     "INNER JOIN [Exercise] e ON ea.ExerciseAreaID = e.ExerciseAreaID " +
//				                     "INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID " +
//				                     "INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID " +
//				                     "INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID " +
//				                     "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
//				                     "INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID " +
//				                     "WHERE eal.Lang = el.Lang " +
//				                     "AND e.RequiredUserLevel = 0 " +
//				                     "AND el.Lang = evl.Lang " +
//				                     "AND evl.Lang = etl.Lang " +
//				                     "AND etl.Lang = " + (languageID - 1) + " " +
//				                     (type != 0 ? "AND e.Minutes " + (type == 1 ? "<= 15" : "> 15") + " " : "") +
//				                     (exerciseAreaID != 0 ? "AND e.ExerciseAreaID = " + exerciseAreaID + " " : "") +
//				                     "AND et.ExerciseTypeID = 1");
//				ExerciseInfo[] ret = new ExerciseInfo[cx];
//				cx = 0;
//				SqlDataReader r = rs("SELECT " +
//				                     "el.New, " +                    // 0
//				                     "NULL, " +
//				                     //"(" +
//				                     //    "SELECT COUNT(*) FROM [ExerciseVariantLang] evlTmp " +
//				                     //    "INNER JOIN [ExerciseVariant] evTmp ON evlTmp.ExerciseVariantID = evTmp.ExerciseVariantID " +
//				                     //    "WHERE evTmp.ExerciseTypeID >= 3 " +
//				                     //    "AND evTmp.ExerciseTypeID <= 4 " +
//				                     //    "AND Lang = evl.Lang " +
//				                     //    "AND evTmp.ExerciseID = ev.ExerciseID" +
//				                     //") AS VariantCount, " +         // 1
//				                     "evl.ExerciseVariantLangID, " + // 2
//				                     "eal.ExerciseArea, " +          // 3
//				                     "eal.ExerciseAreaID, " +        // 4
//				                     "e.ExerciseImg, " +             // 5
//				                     "e.ExerciseID, " +              // 6
//				                     "ea.ExerciseAreaImg, " +        // 7
//				                     "el.Exercise, " +               // 8
//				                     "el.ExerciseTime, " +           // 9
//				                     "el.ExerciseTeaser, " +         // 10
//				                     "evl.ExerciseFile, " +          // 11
//				                     "evl.ExerciseFileSize, " +      // 12
//				                     "evl.ExerciseContent, " +       // 13
//				                     "evl.ExerciseWindowX, " +       // 14
//				                     "evl.ExerciseWindowY, " +       // 15
//				                     "et.ExerciseTypeID, " +         // 16
//				                     "etl.ExerciseType, " +          // 17
//				                     "etl.ExerciseSubtype, " +       // 18
//				                     "(SELECT COUNT(*) FROM ExerciseStats esX INNER JOIN ExerciseVariantLang evlX ON esX.ExerciseVariantLangID = evlX.ExerciseVariantLangID INNER JOIN ExerciseVariant evX ON evlX.ExerciseVariantID = evX.ExerciseVariantID WHERE evX.ExerciseID = e.ExerciseID) AS CX " +  // 19
//				                     "FROM [ExerciseArea] ea " +
//				                     "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID " +
//				                     "INNER JOIN [Exercise] e ON ea.ExerciseAreaID = e.ExerciseAreaID " +
//				                     "INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID " +
//				                     "INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID " +
//				                     "INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID " +
//				                     "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
//				                     "INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID " +
//				                     "WHERE eal.Lang = el.Lang " +
//				                     "AND e.RequiredUserLevel = 0 " +
//				                     "AND el.Lang = evl.Lang " +
//				                     "AND evl.Lang = etl.Lang " +
//				                     "AND etl.Lang = " + (languageID - 1) + " " +
//				                     (type != 0 ? "AND e.Minutes " + (type == 1 ? "<= 15" : "> 15") + " " : "") +
//				                     (exerciseAreaID != 0 ? "AND e.ExerciseAreaID = " + exerciseAreaID + " " : "") +
//				                     "AND et.ExerciseTypeID = 1 " +
//				                     "ORDER BY " +
//				                     "ea.ExerciseAreaSortOrder ASC, " +
//				                     "e.ExerciseSortOrder ASC, " +
//				                     "el.Exercise ASC, " +
//				                     "et.ExerciseTypeSortOrder ASC");
//				while (r.Read()) {
//					ret[cx].exercise = r.GetString(8);
//					ret[cx].exerciseID = r.GetInt32(6);
//					ret[cx].exerciseTeaser = r.GetString(10);
//					ret[cx].exerciseTime = r.GetString(9);
//					ret[cx].exerciseArea = r.GetString(3);
//					ret[cx].exerciseAreaID = r.GetInt32(4);
//					ret[cx].popularity = r.GetInt32(19);
//					if (!r.IsDBNull(5)) {
//						ret[cx].exerciseImage = "https://www.healthwatch.se/" + r.GetString(5);
//					}
//
//					ExerciseVariant[] tmp = new ExerciseVariant[1];
//					tmp[0].exerciseVariantLangID = r.GetInt32(2);
//					tmp[0].exerciseType = r.GetString(17);
//					ret[cx].exerciseVariant = tmp;
//
//					cx++;
//				}
//				r.Close();
//
//				return ret;
//			}
//			return (new ExerciseInfo[0]);
//		}
//
//		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates exercise areas. Type 0 = All, 1 = Containing short exercises, 2 = Containing long exercises. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public ExerciseArea[] ExerciseAreaEnum(string token, int type, int languageID, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int cx = execIntScal("SELECT COUNT(*) " +
//				                     "FROM [ExerciseArea] ea " +
//				                     "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID " +
//				                     "WHERE eal.Lang = " + (languageID - 1) + " " +
//				                     "AND (" +
//				                     "SELECT COUNT(*) " +
//				                     "FROM Exercise e " +
//				                     "INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID " +
//				                     "INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID " +
//				                     "INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID " +
//				                     "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
//				                     "INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID " +
//				                     "WHERE e.ExerciseAreaID = ea.ExerciseAreaID " +
//				                     "AND eal.Lang = el.Lang " +
//				                     "AND e.RequiredUserLevel = 0 " +
//				                     "AND el.Lang = evl.Lang " +
//				                     "AND evl.Lang = etl.Lang " +
//				                     ") > 0");
//				ExerciseArea[] ret = new ExerciseArea[cx];
//				cx = 0;
//				SqlDataReader r = rs("SELECT " +
//				                     "eal.ExerciseArea, " +          // 0
//				                     "eal.ExerciseAreaID " +
//				                     "FROM [ExerciseArea] ea " +
//				                     "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID " +
//				                     "WHERE eal.Lang = " + (languageID - 1) + " " +
//				                     "AND (" +
//				                     "SELECT COUNT(*) " +
//				                     "FROM Exercise e " +
//				                     "INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID " +
//				                     "INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID " +
//				                     "INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID " +
//				                     "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
//				                     "INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID " +
//				                     "WHERE e.ExerciseAreaID = ea.ExerciseAreaID " +
//				                     "AND eal.Lang = el.Lang " +
//				                     "AND e.RequiredUserLevel = 0 " +
//				                     "AND el.Lang = evl.Lang " +
//				                     "AND evl.Lang = etl.Lang " +
//				                     ") > 0 " +
//				                     "ORDER BY ea.ExerciseAreaSortOrder");
//				while (r.Read()) {
//					ret[cx].exerciseArea = r.GetString(0);
//					ret[cx].exerciseAreaID = r.GetInt32(1);
//					cx++;
//				}
//				r.Close();
//
//				return ret;
//			}
//			return (new ExerciseArea[0]);
//		}
//
//		[WebMethod(Description="Validates a username and password combination and, if there is a match, returns a user data object including token with a variable lifetime (max 20 minutes).")]
//		public UserData UserLogin(string username, string password, int expirationMinutes)
//		{
//			UserData ud = new UserData();
//			SqlDataReader r = rs("SELECT u.UserID, u.LID FROM [User] u WHERE u.Username = '" + username.Replace("'", "") + "' AND u.Password = '" + HashMD5(password.Trim()) + "'");
//			if (r.Read()) {
//				ud = getUserToken(r.GetInt32(0),r.GetInt32(1),expirationMinutes);
//			}
//			r.Close();
//			return ud;
//		}
//
//		[WebMethod(Description = "Update the user language. Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public bool UserUpdateLanguage(string token, int languageID, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				return (exec("UPDATE [User] SET LID = " + languageID + " WHERE UserID = " + userID) > 0);
//			}
//			return false;
//		}
//
//		[WebMethod(Description = "Extend the user token. Returns true if successful. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public bool UserExtendToken(string token, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			return (userID != 0);
//		}
//
//		[WebMethod(Description = "Expire token. Returns true if successful.")]
//		public bool UserLogout(string token)
//		{
//			return (exec("UPDATE UserToken SET Expires = GETDATE() WHERE UserToken = '" + token.Replace("'", "''") + "'") > 0);
//		}
//
//		[WebMethod(Description = "Provide email address to request a password reset link to be sent by email. Returns false only if malformed email address or email server is unavailable, otherwise true.")]
//		public bool UserResetPassword(string email, int languageID)
//		{
//			return sendPasswordReminder(email, languageID);
//		}
//
//		[WebMethod(CacheDuration = 10*60, Description = "Enumerates news categories in specified language.")]
//		public NewsCategory[] NewsCategories(int lastXdays, int languageID, bool includeEnglishNews)
//		{
//			int newsCategories = execIntScal("SELECT COUNT(*) FROM NewsCategory nc INNER JOIN NewsCategoryLang ncl ON nc.NewsCategoryID = ncl.NewsCategoryID AND ncl.LangID = " + languageID,"newsSqlConnection");
//			NewsCategory[] ncs = new NewsCategory[newsCategories];
//
//			int cx = 0;
//			string query = string.Format(
//				"SELECT " +
//				"nc.NewsCategoryShort, " +
//				"ncl.NewsCategory, " +
//				"nc.NewsCategoryID, " +
//				"ncl.LangID, " +
//				"(SELECT COUNT(*) FROM News n WHERE n.Published IS NOT NULL AND n.Published <= GETDATE() AND n.Deleted IS NULL AND n.NewsCategoryID = nc.NewsCategoryID AND n.LinkLangID IN (" + (languageID - 1) + (includeEnglishNews ? ",1" : "") + ")), " +
//				"(SELECT COUNT(*) FROM News n WHERE n.Published IS NOT NULL AND n.Published <= GETDATE() AND n.Deleted IS NULL AND n.NewsCategoryID = nc.NewsCategoryID AND n.LinkLangID IN (" + (languageID - 1) + (includeEnglishNews ? ",1" : "") + ") AND GETDATE() <= DATEADD(day," + lastXdays + ",n.DT)) " +
//				"FROM NewsCategory nc INNER JOIN NewsCategoryLang ncl ON nc.NewsCategoryID = ncl.NewsCategoryID AND ncl.LangID = " + languageID + " ORDER BY ncl.NewsCategory"
//			);
//			SqlDataReader r = rs(query, "newsSqlConnection");
//			while (r.Read()) {
//				ncs[cx].newsCategoryAlias = r.GetString(0);
//				ncs[cx].newsCategory = r.GetString(1);
//				ncs[cx].newsCategoryID = r.GetInt32(2);
//				ncs[cx].languageID = r.GetInt32(3);
//				ncs[cx].totalCount = r.GetInt32(4);
//				ncs[cx].lastXdaysCount = r.GetInt32(5);
//				ncs[cx].newsCategoryImage = "https://www.healthwatch.se/includes/resources/article_" + r.GetString(0) + "_50x50.gif";
//				cx++;
//			}
//			r.Close();
//
//			return ncs;
//		}
//
//		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates news in specified category (0 for all or no category) with specified language. LastXdays includes only news published last X days, 0 to disable. StartOffset skips news up to that number. TopX only enumerates that many articles, starting at StartOffset, 0 to disable.")]
//		public News[] NewsEnum(int lastXdays, int startOffset, int topX, int languageID, bool includeEnglishNews, int newsCategoryID)
//		{
//			int news = execIntScal("SELECT " +
//			                       "COUNT(*) " +
//			                       "FROM News n " +
//			                       "WHERE n.Published IS NOT NULL " +
//			                       "AND n.Published <= GETDATE() " +
//			                       "AND n.Deleted IS NULL " +
//			                       (newsCategoryID != 0 ? "AND n.NewsCategoryID = " + newsCategoryID + " " : "") +
//			                       "AND n.LinkLangID IN (" + (languageID - 1) + (includeEnglishNews ? ",1" : "") + ") " +
//			                       (lastXdays != 0 ? "AND GETDATE() <= DATEADD(day," + lastXdays + ",n.DT) " : "") +
//			                       "", "newsSqlConnection");
//			news = news - startOffset;
//			if (topX > 0) {
//				news = Math.Min(news, topX);
//			}
//			news = Math.Max(news, 0);
//
//			News[] n = new News[news];
//
//			int cx = 0, bx = 1;
//			SqlDataReader r = rs("SELECT " +
//			                     (topX != 0 ? "TOP " + (startOffset + topX) + " " : "") +
//			                     "nc.NewsCategoryShort, " +
//			                     "ncl.NewsCategory, " +
//			                     "nc.NewsCategoryID, " +
//			                     "n.LinkLangID, " +
//			                     "n.NewsID, " +
//			                     "n.HeadlineShort, " +
//			                     "n.DT, " +
//			                     "n.Headline, " +
//			                     "n.Teaser, " +
//			                     "n.Body " +
//			                     "FROM News n " +
//			                     "LEFT OUTER JOIN NewsCategory nc ON n.NewsCategoryID = nc.NewsCategoryID " +
//			                     "LEFT OUTER JOIN NewsCategoryLang ncl ON nc.NewsCategoryID = ncl.NewsCategoryID AND ncl.LangID = " + languageID + " " +
//			                     "WHERE n.Published IS NOT NULL " +
//			                     "AND n.Published <= GETDATE() " +
//			                     "AND n.Deleted IS NULL " +
//			                     (newsCategoryID != 0 ? "AND n.NewsCategoryID = " + newsCategoryID + " " : "") +
//			                     "AND n.LinkLangID IN (" + (languageID - 1) + (includeEnglishNews ? ",1" : "") + ") " +
//			                     (lastXdays != 0 ? "AND GETDATE() <= DATEADD(day," + lastXdays + ",n.DT) " : "") +
//			                     "ORDER BY n.DT DESC, n.NewsID DESC" +
//			                     "", "newsSqlConnection");
//			while (r.Read()) {
//				if (bx > startOffset && (topX == 0 || cx < topX)) {
//					n[cx].newsCategoryAlias = (r.IsDBNull(0) ? "" : r.GetString(0));
//					n[cx].newsCategory = (r.IsDBNull(1) ? "" : r.GetString(1));
//					n[cx].newsCategoryID = (r.IsDBNull(2) ? 0 : r.GetInt32(2));
//					n[cx].languageID = (r.GetInt32(3) + 1);
//					n[cx].newsCategoryImage = (r.IsDBNull(0) ? "" : "https://www.healthwatch.se/includes/resources/article_" + r.GetString(0) + "_50x50.gif");
//
//					n[cx].newsID = r.GetInt32(4);
//					n[cx].link = "https://www.healthwatch.se/news/" + (!r.IsDBNull(0) ? r.GetString(0) + "/" : "") + r.GetString(5);
//					n[cx].DT = r.GetDateTime(6);
//					n[cx].headline = r.GetString(7);
//					n[cx].teaser = r.GetString(8);
//					n[cx].body = r.GetString(9);
//					cx++;
//				}
//				bx++;
//			}
//			r.Close();
//
//			return n;
//		}
//
//		[WebMethod(CacheDuration = 10 * 60, Description = "Shows details for a news article.")]
//		public News NewsDetail(int newsID, int languageID)
//		{
//			News n = new News();
//
//			SqlDataReader r = rs("SELECT " +
//			                     "nc.NewsCategoryShort, " +
//			                     "ncl.NewsCategory, " +
//			                     "nc.NewsCategoryID, " +
//			                     "n.LinkLangID, " +
//			                     "n.NewsID, " +
//			                     "n.HeadlineShort, " +
//			                     "n.DT, " +
//			                     "n.Headline, " +
//			                     "n.Teaser, " +
//			                     "n.Body " +
//			                     "FROM News n " +
//			                     "LEFT OUTER JOIN NewsCategory nc ON n.NewsCategoryID = nc.NewsCategoryID " +
//			                     "LEFT OUTER JOIN NewsCategoryLang ncl ON nc.NewsCategoryID = ncl.NewsCategoryID AND ncl.LangID = " + languageID + " " +
//			                     "WHERE n.Published IS NOT NULL " +
//			                     "AND n.Published <= GETDATE() " +
//			                     "AND n.Deleted IS NULL " +
//			                     "AND n.NewsID = " + newsID + " " +
//			                     "", "newsSqlConnection");
//			if (r.Read()) {
//				n.newsCategoryAlias = (r.IsDBNull(0) ? "" : r.GetString(0));
//				n.newsCategory = (r.IsDBNull(1) ? "" : r.GetString(1));
//				n.newsCategoryID = (r.IsDBNull(2) ? 0 : r.GetInt32(2));
//				n.languageID = (r.GetInt32(3) + 1);
//				n.newsCategoryImage = (r.IsDBNull(0) ? "" : "https://www.healthwatch.se/includes/resources/article_" + r.GetString(0) + "_50x50.gif");
//
//				n.newsID = r.GetInt32(4);
//				n.link = "https://www.healthwatch.se/news/" + (!r.IsDBNull(0) ? r.GetString(0) + "/" : "") + r.GetString(5);
//				n.DT = r.GetDateTime(6);
//				n.headline = r.GetString(7);
//				n.teaser = r.GetString(8);
//				n.body = r.GetString(9);
//			}
//			r.Close();
//
//			return n;
//		}
//
//		[WebMethod(CacheDuration = 10 * 60, Description = "Enumerates profile questions in specified language. Sponsor should be 0 if not known.")]
//		public Question[] ProfileQuestions(int languageID, int sponsorID)
//		{
//			int sortOrder = 0;
//			int qCount = execIntScal("SELECT COUNT(*) " +
//			                         "FROM Sponsor s " +
//			                         "INNER JOIN SponsorBQ sbq ON s.SponsorID = sbq.SponsorID " +
//			                         "INNER JOIN BQ ON BQ.BQID = sbq.BQID " +
//			                         "INNER JOIN BQLang ON BQ.BQID = BQLang.BQID AND BQLang.LangID = " + languageID + " " +
//			                         "WHERE sbq.Hidden = 0 AND s.SponsorID = " + (sponsorID == 0 ? 1 : sponsorID));
//
//			Question[] Qs = new Question[qCount];
//			int cx = 0;
//
//			SqlDataReader r = rs("SELECT " +
//			                     "BQ.BQID, " +           // 0
//			                     "BQLang.BQ, " +         // 1
//			                     "BQ.Type, " +           // 2
//			                     "sbq.Forced, " +        // 3
//			                     "BQ.ReqLength, " +      // 4
//			                     "BQ.DefaultVal, " +     // 5
//			                     "BQ.MaxLength, " +      // 6
//			                     "(" +
//			                     "SELECT " +
//			                     "COUNT(*) FROM " +
//			                     "BQVisibility v2 " +
//			                     "INNER JOIN SponsorBQ b2 ON v2.BQID = b2.BQID AND b2.SponsorID = s.SponsorID " +
//			                     "WHERE b2.Hidden = 0 AND v2.ChildBQID = BQ.BQID" +
//			                     "), " +                  // 7 - Number of parent questions
//			                     "BQ.MeasurementUnit " +  // 8
//			                     "FROM Sponsor s " +
//			                     "INNER JOIN SponsorBQ sbq ON s.SponsorID = sbq.SponsorID " +
//			                     "INNER JOIN BQ ON BQ.BQID = sbq.BQID " +
//			                     "INNER JOIN BQLang ON BQ.BQID = BQLang.BQID AND BQLang.LangID = " + languageID + " " +
//			                     "WHERE sbq.Hidden = 0 AND s.SponsorID = " + (sponsorID == 0 ? 1 : sponsorID) + " " +
//			                     "ORDER BY sbq.SortOrder");
//			while (r.Read()) {
//				Qs[cx].SortOrder = (++sortOrder);
//				Qs[cx].QuestionID = r.GetInt32(0);
//				Qs[cx].QuestionText = r.GetString(1);
//				Qs[cx].QuestionType = (QuestionTypes)r.GetInt32(2);
//				Qs[cx].Mandatory = (!r.IsDBNull(3) && r.GetInt32(3) == 1);
//				Qs[cx].RequiredNumberOfCharacters = (!r.IsDBNull(4) ? r.GetInt32(4) : (r.GetInt32(2) == 3 ? 10 : 0));
//				Qs[cx].DefaultValue = (r.IsDBNull(5) ? "" : r.GetString(5));
//				Qs[cx].MaximumNumberOfCharacters = (!r.IsDBNull(6) ? r.GetInt32(6) : (r.GetInt32(2) == 3 ? 10 : 255));
//				Qs[cx].MeasurementUnit = (r.IsDBNull(8) ? "" : r.GetString(8));
//
//				if (r.GetInt32(7) > 0) {
//					int vcCount = execIntScal("SELECT COUNT(*) " +
//					                          "FROM BQVisibility v " +
//					                          "INNER JOIN BQ ON v.BQID = BQ.BQID " +
//					                          "INNER JOIN SponsorBQ b ON v.BQID = b.BQID AND b.SponsorID = " + (sponsorID == 0 ? 1 : sponsorID) + " " +
//					                          "WHERE b.Hidden = 0 AND v.ChildBQID = " + r.GetInt32(0));
//
//					VisibilityConditionOr[] VCs = new VisibilityConditionOr[vcCount];
//					int rx = 0;
//
//					SqlDataReader r2 = rs("SELECT " +
//					                      "v.BQID, " +
//					                      "v.BAID " +
//					                      "FROM BQVisibility v " +
//					                      "INNER JOIN BQ ON v.BQID = BQ.BQID " +
//					                      "INNER JOIN SponsorBQ b ON v.BQID = b.BQID AND b.SponsorID = " + (sponsorID == 0 ? 1 : sponsorID) + " " +
//					                      "WHERE b.Hidden = 0 AND v.ChildBQID = " + r.GetInt32(0));
//					while (r2.Read()) {
//						VCs[rx].QuestionID = r2.GetInt32(0);
//						VCs[rx].AnswerID = r2.GetInt32(1);
//
//						rx++;
//					}
//					r2.Close();
//
//					Qs[cx].VisibilityConditions = VCs;
//				}
//
//				if (r.GetInt32(2) == 1 || r.GetInt32(2) == 7) {
//					int aSortOrder = 0;
//					int aCount = execIntScal("SELECT COUNT(*) " +
//					                         "FROM BA " +
//					                         "INNER JOIN BALang ON BA.BAID = BALang.BAID AND BALang.LangID = " + languageID + " " +
//					                         "WHERE BA.BQID = " + r.GetInt32(0));
//
//					Answer[] A = new Answer[aCount];
//					int ax = 0;
//
//					SqlDataReader r2 = rs("SELECT " +
//					                      "BA.BAID, " +
//					                      "BALang.BA " +
//					                      "FROM BA " +
//					                      "INNER JOIN BALang ON BA.BAID = BALang.BAID AND BALang.LangID = " + languageID + " " +
//					                      "WHERE BA.BQID = " + r.GetInt32(0) + " " +
//					                      "ORDER BY BA.Value, BALang.BA");
//					while (r2.Read()) {
//						A[ax].AnswerID = r2.GetInt32(0);
//						A[ax].SortOrder = (++aSortOrder);
//						A[ax].AnswerText = r2.GetString(1);
//
//						ax++;
//					}
//					r2.Close();
//
//					Qs[cx].AnswerOptions = A;
//				}
//
//				cx++;
//			}
//			r.Close();
//
//			return Qs;
//		}
//
//		[WebMethod(Description = "Creates user. Username and password must be at least five characters. Alternate email is optional. Sponsor and department should be 0 if not known. Returns token if successful, blank if username is too short or already taken, password is too short, policy not accepted or malformed email-address.")]
//		public UserData UserCreate(string username, string password, string email, string alternateEmail, bool acceptPolicy, int languageID, int sponsorID, int departmentID, int expirationMinutes)
//		{
//			if ((departmentID == 0 || execIntScal("SELECT SponsorID FROM Department WHERE DepartmentID = " + departmentID) == sponsorID) && execIntScal("SELECT COUNT(*) FROM [User] WHERE LOWER(Username) = '" + username.Replace("'","").ToLower() + "'") == 0 && username.Length >= 5 && password.Length >= 5 && email != "" && isEmail(email) && (alternateEmail == "" || isEmail(alternateEmail)) && acceptPolicy) {
//				int userID = execIntScal("INSERT INTO [User] (" +
//				                         "Username, " +
//				                         "Email, " +
//				                         "Password, " +
//				                         "SponsorID, " +
//				                         "DepartmentID, " +
//				                         "LID, " +
//				                         "AltEmail" +
//				                         ") OUTPUT INSERTED.UserID VALUES (" +
//				                         "'" + username.Replace("'", "") + "'," +
//				                         "'" + email.Replace("'", "") + "'," +
//				                         "'" + HashMD5(password.Trim()) + "'," +
//				                         "" + (sponsorID == 0 ? 1 : sponsorID) + "," +
//				                         "" + (departmentID == 0 ? "NULL" : departmentID.ToString()) + "," +
//				                         "" + languageID + "," +
//				                         "" + (alternateEmail != "" ? "'" + alternateEmail.Replace("'", "") + "'" : "NULL") + "" +
//				                         ")");
//
//				int userProfileID = execIntScal("INSERT INTO UserProfile (UserID, SponsorID, DepartmentID) OUTPUT INSERTED.UserProfileID VALUES (" + userID + "," + (sponsorID == 0 ? 1 : sponsorID) + "," + (departmentID == 0 ? "NULL" : departmentID.ToString()) + ")");
//				exec("UPDATE [User] SET UserProfileID = " + userProfileID + " WHERE UserID = " + userID);
//
//				return getUserToken(userID, languageID, expirationMinutes);
//			}
//
//			return (new UserData());
//		}
//
//		[WebMethod(Description = "Updates user info. Username must be at least five characters. Alternate email is optional. Returns true if successful, false if token invalid/expired, username is too short or already taken or malformed email-address. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public bool UserUpdateInfo(string username, string email, string alternateEmail, string token, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (
//				userID != 0
//				&&
//				execIntScal("SELECT COUNT(*) FROM [User] WHERE UserID <> " + userID + " AND LOWER(Username) = '" + username.Replace("'", "").ToLower() + "'") == 0
//				&&
//				username.Length >= 5
//				&&
//				email != ""
//				&&
//				isEmail(email)
//				&&
//				(alternateEmail == "" || isEmail(alternateEmail)))
//			{
//				return (exec("UPDATE [User] SET " +
//				             "Username = '" + username.Replace("'", "") + "', " +
//				             "Email = '" + email.Replace("'", "") + "', " +
//				             "AltEmail = " + (alternateEmail != "" ? "'" + alternateEmail.Replace("'", "") + "'" : "NULL") + " " +
//				             "WHERE UserID = " + userID) > 0);
//			}
//
//			return false;
//		}
//
//		[WebMethod(Description = "Gets user info. Returns username, email, alternate email and language or blank if token invalid/expired. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public UserInfo UserGetInfo(string token, int expirationMinutes)
//		{
//			UserInfo ui = new UserInfo();
//
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				SqlDataReader r = rs("SELECT Username, Email, AltEmail, LID FROM [User] WHERE UserID = " + userID);
//				if (r.Read()) {
//					ui.username = (r.IsDBNull(0) ? "" : r.GetString(0));
//					ui.email = (r.IsDBNull(1) ? "" : r.GetString(1));
//					ui.alternateEmail = (r.IsDBNull(2) ? "" : r.GetString(2));
//					ui.languageID = (r.IsDBNull(3) ? 0 : r.GetInt32(3));
//				}
//				r.Close();
//			}
//
//			return ui;
//		}
//
//		[WebMethod(Description = "Updates user password. Returns true if successful, false if token invalid/expired or password is too short. Expiration minutes greater than 0 extends the token expiration by that many minutes from now.")]
//		public bool UserUpdatePassword(string password, string token, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0 && password.Length >= 5) {
//				return (exec("UPDATE [User] SET " +
//				             "Password = '" + HashMD5(password.Trim()) + "' " +
//				             "WHERE UserID = " + userID) > 0);
//			}
//
//			return false;
//		}
//
//		[WebMethod(Description = "Gets a profile question answer of a user. Returns blank if token invalid/expired or no answer exist.")]
//		public string UserGetProfileQuestion(int questionID, string token, int expirationMinutes)
//		{
//			string ret = "";
//
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				SqlDataReader r = rs("SELECT BQ.Type, w.ValueInt, w.ValueText, w.ValueDate " +
//				                     "FROM [UserProfileBQ] w " +
//				                     "INNER JOIN BQ ON w.BQID = BQ.BQID " +
//				                     "INNER JOIN [User] u ON w.UserProfileID = u.UserProfileID " +
//				                     "WHERE w.BQID = " + questionID + " AND u.UserID = " + userID);
//				if (r.Read()) {
//					switch (r.GetInt32(0)) {
//						case 1:
//						case 4:
//						case 7:
//							ret = (r.IsDBNull(1) ? "" : r.GetInt32(1).ToString()); break;
//						case 2:
//							ret = (r.IsDBNull(2) ? "" : r.GetString(2)); break;
//						case 3:
//							ret = (r.IsDBNull(3) ? "" : r.GetDateTime(3).ToString("yyyy-MM-dd")); break;
//					}
//				}
//				r.Close();
//			}
//
//			return ret;
//		}
//
//		[WebMethod(Description = "Sets or updates a profile question of a user. Returns false if token invalid/expired or malformed answer else true.")]
//		public bool UserSetProfileQuestion(int questionID, string answer, string token, int expirationMinutes)
//		{
//			bool res = false;
//
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			if (userID != 0) {
//				int userProfileID = execIntScal("SELECT UserProfileID FROM [User] WHERE UserID = " + userID);
//				int typeID = execIntScal("SELECT Type FROM BQ WHERE BQID = " + questionID);
//				switch (typeID) {
//					case 1:
//					case 7:
//						try {
//							int answerID = Convert.ToInt32(answer);
//							if (execIntScal("SELECT BQID FROM BA WHERE BAID = " + answerID) == questionID) {
//								int oldAnswerID = execIntScal("SELECT TOP 1 ValueInt FROM UserProfileBQ WHERE BQID = " + questionID + " AND UserProfileID = " + userProfileID);
//								if(oldAnswerID != 0 && oldAnswerID != answerID) {
//									userProfileID = duplicateUserProfile(userID, userProfileID, questionID);
//									oldAnswerID = 0;
//								}
//								if (oldAnswerID == 0) {
//									exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + userProfileID + "," + questionID + "," + answerID + ")");
//								}
//								updateProfileComparison(userProfileID);
//								res = true;
//							}
//						} catch (Exception) { }
//						break;
//					case 2:
//						{
//							string oldAnswer = execStrScal("SELECT TOP 1 ValueText FROM UserProfileBQ WHERE BQID = " + questionID + " AND UserProfileID = " + userProfileID);
//							if (oldAnswer != "" && oldAnswer != answer) {
//								userProfileID = duplicateUserProfile(userID, userProfileID, questionID);
//								oldAnswer = "";
//							}
//							if (oldAnswer == "") {
//								exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueText) VALUES (" + userProfileID + "," + questionID + ",'" + answer.Replace("'", "''") + "')");
//							}
//							res = true;
//						}
//						break;
//					case 3:
//						{
//							try {
//								DateTime answerDT = DateTime.ParseExact(answer, "yyyy-MM-dd", (new System.Globalization.CultureInfo("en-US")));
//								DateTime oldAnswerDT = execDateScal("SELECT TOP 1 ValueDate FROM UserProfileBQ WHERE BQID = " + questionID + " AND UserProfileID = " + userProfileID);
//								if (oldAnswerDT != DateTime.MinValue && oldAnswerDT != answerDT) {
//									userProfileID = duplicateUserProfile(userID, userProfileID, questionID);
//									oldAnswerDT = DateTime.MinValue;
//								}
//								if (oldAnswerDT == DateTime.MinValue) {
//									exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueDate) VALUES (" + userProfileID + "," + questionID + ",'" + answerDT.ToString("yyyy-MM-dd") + "')");
//								}
//								res = true;
//							} catch (Exception) { }
//						}
//						break;
//					case 4:
//						{
//							try {
//								int answerInt = Convert.ToInt32(answer);
//								int oldAnswerInt = execIntScal("SELECT TOP 1 ValueInt FROM UserProfileBQ WHERE BQID = " + questionID + " AND UserProfileID = " + userProfileID);
//								if (oldAnswerInt != 0 && oldAnswerInt != answerInt) {
//									userProfileID = duplicateUserProfile(userID, userProfileID, questionID);
//									oldAnswerInt = 0;
//								}
//								if (oldAnswerInt == 0) {
//									exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + userProfileID + "," + questionID + "," + answerInt + ")");
//								}
//								res = true;
//							} catch (Exception) { }
//						}
//						break;
//				}
//			}
//			return res;
//		}
//
//		[WebMethod(Description = "Saves the user device registration ID. Returns false if the device ID is already in the database.")]
//		public bool UserSaveRegistrationID(string registrationID, string token, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			bool isNewValue = false;
//			if (userID != 0) {
//				SqlDataReader r = rs(
//					"SELECT UserRegistrationID, UserID, RegistrationID " +
//					"FROM dbo.UserRegistrationID " +
//					"WHERE UserID = " + userID + " " +
//					"AND RegistrationID = '" + registrationID + "'"
//				);
//				if (!r.Read()) {
//					isNewValue = true;
//				}
//				if (isNewValue) {
//					exec(
//						"INSERT INTO dbo.UserRegistrationID(UserID, RegistrationID)" +
//						"VALUES(" + userID + ", '" + registrationID.Replace("'", "") + "')"
//					);
//				}
//				r.Close();
//			}
//			return isNewValue;
//		}
//
//		[WebMethod(Description = "Gets the UserKey and updates a new value into the database. Returns 0 if UserKey is not found. This will only update a new UserKey if the User's ReminderLink is 2.")]
//		public bool UserSetUsedUserKey(string userKey, string token, int expirationMinutes)
//		{
//			int userID = getUserIdFromToken(token, expirationMinutes);
//			bool validKey = false;
//			if (userID != 0) {
//				userKey = (userKey.Length == 12 ? userKey.Substring(0, 12) : userKey).Replace("'", "").ToLower();
//				SqlDataReader r = rs(
//					"SELECT " +
//					"u.UserID, " +
//					"u.ReminderLink " +
//					"FROM [User] u " +
//					"WHERE u.UserID = " + userID + " " +
//					"AND LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)) = '" + userKey + "'"
//				);
//				if (r.Read()) {
//					userID = r.GetInt32(0);
//					if (r.GetInt32(1) == 2) {
//						exec("UPDATE [User] SET UserKey = NEWID() WHERE UserID = " + userID);
//					}
//					validKey = true;
//				}
//				r.Close();
//			}
//			return validKey;
//		}
//
//		[WebMethod(Description = "")]
//		public string HelloWorld()
//		{
//			return "Hello you son of a shit!";
//		}
//
//		private void updateProfileComparison(int userProfileID)
//		{
//			string comparison = "";
//			string comparisonInsert = "";
//
//			SqlDataReader r = rs("SELECT " +
//			                     "sbq.BQID, " +           // 0
//			                     "upbq.ValueInt " +
//			                     "FROM UserProfile up " +
//			                     "INNER JOIN SponsorBQ sbq ON up.SponsorID = sbq.SponsorID " +
//			                     "INNER JOIN BQ ON BQ.BQID = sbq.BQID " +
//			                     "INNER JOIN UserProfileBQ upbq ON up.UserProfileID = upbq.UserProfileID AND upbq.BQID = BQ.BQID " +
//			                     "WHERE BQ.Type IN (1,7) AND BQ.Comparison = 1 AND up.UserProfileID = " + userProfileID + " ORDER BY BQ.BQID");
//			while (r.Read()) {
//				if (!r.IsDBNull(1)) {
//					string val = r.GetInt32(1).ToString();
//					comparison += val;
//					comparisonInsert += (comparisonInsert != "" ? "¤" : "") + "INSERT INTO ProfileComparisonBQ (ProfileComparisonID,BQID,ValueInt) VALUES ([x]," + r.GetInt32(0) + "," + val + ")";
//				}
//			}
//			r.Close();
//
//			string hash = HashMD5(comparison);
//			int profileComparisonID = execIntScal("SELECT ProfileComparisonID FROM ProfileComparison WHERE Hash = '" + hash + "'");
//			if (profileComparisonID == 0) {
//				profileComparisonID = execIntScal("INSERT INTO ProfileComparison (Hash) OUTPUT INSERTED.ProfileComparisonID VALUES ('" + hash + "')");
//				if (comparisonInsert != "") {
//					if (comparisonInsert.IndexOf('¤') >= 0) {
//						foreach (string s in comparisonInsert.Split('¤')) {
//							exec(s.Replace("[x]", profileComparisonID.ToString()));
//						}
//					} else {
//						exec(comparisonInsert.Replace("[x]", profileComparisonID.ToString()));
//					}
//				}
//			}
//			exec("UPDATE UserProfile SET ProfileComparisonID = " + profileComparisonID + " WHERE UserProfileID = " + userProfileID);
//		}
//
//		private int duplicateUserProfile(int userID, int userProfileID, int excludeQuestionID)
//		{
//			int newUserProfileID = execIntScal("INSERT INTO UserProfile (UserID, SponsorID, DepartmentID) OUTPUT INSERTED.UserProfileID SELECT UserID, SponsorID, DepartmentID FROM UserProfile WHERE UserProfileID = " + userProfileID);
//			exec("UPDATE [User] SET UserProfileID = " + newUserProfileID + " WHERE UserID = " + userID);
//
//			SqlDataReader r = rs("SELECT " +
//			                     "sbq.BQID, " +           // 0
//			                     "BQ.Type, " +
//			                     "upbq.ValueInt, " +
//			                     "upbq.ValueText, " +
//			                     "upbq.ValueDate " +
//			                     "FROM UserProfile up " +
//			                     "INNER JOIN SponsorBQ sbq ON up.SponsorID = sbq.SponsorID " +
//			                     "INNER JOIN BQ ON BQ.BQID = sbq.BQID " +
//			                     "INNER JOIN UserProfileBQ upbq ON up.UserProfileID = upbq.UserProfileID AND upbq.BQID = BQ.BQID " +
//			                     "WHERE BQ.BQID <> " + excludeQuestionID + " AND up.UserProfileID = " + userProfileID);
//			while (r.Read()) {
//				switch (r.GetInt32(1)) {
//					case 1:
//					case 4:
//					case 7:
//						{
//							if (!r.IsDBNull(2)) {
//								exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + newUserProfileID + "," + r.GetInt32(0) + "," + r.GetInt32(2) + ")");
//							}
//						}
//						break;
//					case 2:
//						{
//							if (!r.IsDBNull(3)) {
//								exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueText) VALUES (" + newUserProfileID + "," + r.GetInt32(0) + ",'" + r.GetString(3).Replace("'", "''") + "')");
//							}
//						}
//						break;
//					case 3:
//						{
//							if (!r.IsDBNull(4)) {
//								exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueDate) VALUES (" + newUserProfileID + "," + r.GetInt32(0) + ",'" + r.GetDateTime(4).ToString("yyyy-MM-dd") + "')");
//							}
//						}
//						break;
//				}
//			}
//			r.Close();
//
//			updateProfileComparison(newUserProfileID);
//
//			return newUserProfileID;
//		}
//
//		private SqlDataReader rs(string sqlString)
//		{
//			return rs(sqlString, "SqlConnection");
//		}
//
//		private SqlDataReader rs(string sqlString, string con)
//		{
//			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
//			dataConnection.Open();
//			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
//			SqlDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
//			return dataReader;
//		}
//
//		private string HashMD5(string str)
//		{
//			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
//			byte[] hashByteArray = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes("HW" + str + "HW"));
//			string hash = "";
//			for (int i = 0; i < hashByteArray.Length; i++) {
//				hash += hashByteArray[i];
//			}
//			return hash;
//		}
//
//		private int exec(string sqlString)
//		{
//			return exec(sqlString, "SqlConnection");
//		}
//
//		private int exec(string sqlString, string con)
//		{
//			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
//			dataConnection.Open();
//			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
//			int ret = dataCommand.ExecuteNonQuery();
//			dataConnection.Close();
//			dataConnection.Dispose();
//			return ret;
//		}
//
//		private string execStrScal(string sqlString)
//		{
//			return execStrScal(sqlString, "SqlConnection");
//		}
//
//		private string execStrScal(string sqlString, string con)
//		{
//			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
//			dataConnection.Open();
//			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
//			object o = dataCommand.ExecuteScalar();
//			string ret = "";
//			if(o != null) {
//				ret = o.ToString();
//			}
//			dataConnection.Close();
//			dataConnection.Dispose();
//			return ret;
//		}
//
//		private int execIntScal(string sqlString)
//		{
//			return execIntScal(sqlString, "SqlConnection");
//		}
//
//		private int execIntScal(string sqlString, string con)
//		{
//			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
//			dataConnection.Open();
//			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
//			object o = dataCommand.ExecuteScalar();
//			int ret = 0;
//			if(o != null) {
//				ret = Convert.ToInt32(o.ToString());
//			}
//			dataConnection.Close();
//			dataConnection.Dispose();
//			return ret;
//		}
//
//		private DateTime execDateScal(string sqlString)
//		{
//			return execDateScal(sqlString, "SqlConnection");
//		}
//
//		private DateTime execDateScal(string sqlString, string con)
//		{
//			SqlConnection dataConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[con].ConnectionString);
//			dataConnection.Open();
//			SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
//			object o = dataCommand.ExecuteScalar();
//			DateTime ret = DateTime.MinValue;
//			if (o != null) {
//				ret = Convert.ToDateTime(o.ToString());
//			}
//			dataConnection.Close();
//			dataConnection.Dispose();
//			return ret;
//		}
//
//		private bool sendMail(string from, string email, string body, string subject)
//		{
//			bool success = false;
//			try {
//				System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);
//				System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(from,email,subject,body);
//				smtp.Send(mail);
//				success = true;
//			} catch (Exception) { }
//			return success;
//		}
//
//		private bool isEmail(string inputEmail)
//		{
//			string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
//				@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
//				@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
//			System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(strRegex);
//			if (re.IsMatch(inputEmail)) {
//				return true;
//			} else {
//				return false;
//			}
//		}
//
//		private bool sendPasswordReminder(string email, int languageID)
//		{
//			bool success = false;
//
//			if (email != "" && isEmail(email)) {
//				success = true;
//
//				SqlDataReader r = rs("SELECT TOP 100 UserID, Email, Username, LEFT(REPLACE(CONVERT(VARCHAR(255),UserKey),'-',''),8) FROM [User] WHERE Email = '" + email.ToString().Replace("'", "") + "'");
//				while (r.Read()) {
//					switch (languageID) {
//						case 1:
//							success = sendMail("support@healthwatch.se", r.GetString(1),
//							                   "Hej." +
//							                   "\r\n\r\n" +
//							                   "En begäran om nytt lösenord till ditt konto med användarnamn \"" + r.GetString(2) + "\" har inkommit. Om du begärt detta, klicka på länken nedan för att ange ett nytt lösenord." +
//							                   "\r\n\r\n" +
//							                   "https://www.healthwatch.se/password.aspx?NL=1&K=" + r.GetString(3) + r.GetInt32(0) + "",
//							                   "Nytt lösenord");
//							break;
//						case 2:
//							success = sendMail("support@healthwatch.se", r.GetString(1),
//							                   "Hi." +
//							                   "\r\n\r\n" +
//							                   "A request for new password for your account with username \"" + r.GetString(2) + "\" has arrived. If you made this request, click the link below to set a new password." +
//							                   "\r\n\r\n" +
//							                   "https://www.healthwatch.se/password.aspx?NL=1&K=" + r.GetString(3) + r.GetInt32(0) + "",
//							                   "New password");
//							break;
//					}
//				}
//				r.Close();
//			}
//
//			return success;
//		}
//
//		private int getUserIdFromToken(string token, int expirationMinutes)
//		{
//			int userID = execIntScal("SELECT UserID FROM UserToken WHERE UserToken = '" + token.Replace("'", "''") + "' AND GETDATE() < Expires");
//			if (userID != 0 && expirationMinutes > 0) {
//				exec("UPDATE UserToken SET Expires = DATEADD(minute," + Math.Min(expirationMinutes, 20) + ",GETDATE()) WHERE UserToken = '" + token.Replace("'", "''") + "'");
//			}
//			return userID;
//		}
//
//		private UserData getUserToken(int userID, int languageID, int expirationMinutes)
//		{
//			UserData ud = new UserData();
//			ud.languageID = languageID;
//			ud.tokenExpires = DateTime.Now.AddMinutes(Math.Min(expirationMinutes, 20));
//			int sessionID = execIntScal("INSERT INTO Session (DT,UserAgent,UserID,IP,AutoEnded) OUTPUT INSERTED.SessionID VALUES (GETDATE(),'App'," + userID + ",'127.0.0.1',1)");
//			ud.token = execStrScal("INSERT INTO UserToken (UserID, Expires,SessionID) OUTPUT INSERTED.UserToken VALUES (" + userID + ",DATEADD(minute," + Math.Min(expirationMinutes, 20) + ",GETDATE())," + sessionID + ")");
//
//			return ud;
//		}
//
//		private string nextReminderSend(int type, string[] settings, DateTime lastLogin, DateTime lastSend)
//		{
//			DateTime nextPossibleReminderSend = lastSend.Date.AddHours(Convert.ToInt32(settings[0]));
//			while (nextPossibleReminderSend <= DateTime.Now.AddMinutes(30)) {
//				nextPossibleReminderSend = nextPossibleReminderSend.AddDays(1);
//			}
//			DateTime nextReminderSend = nextPossibleReminderSend.AddYears(10);
//
//			try {
//				switch (type) {
//					case 1:
//						System.DayOfWeek[] dayOfWeek = { System.DayOfWeek.Monday, System.DayOfWeek.Tuesday, System.DayOfWeek.Wednesday, System.DayOfWeek.Thursday, System.DayOfWeek.Friday, System.DayOfWeek.Saturday, System.DayOfWeek.Sunday };
//
//						switch (Convert.ToInt32(settings[1])) {
//							case 1:
//								#region Weekday
//								{
//									string[] days = settings[2].Split(',');
//									foreach (string day in days)
//									{
//										DateTime tmp = nextPossibleReminderSend;
//										while (tmp.DayOfWeek != dayOfWeek[Convert.ToInt32(day) - 1])
//										{
//											tmp = tmp.AddDays(1);
//										}
//										if (tmp < nextReminderSend)
//										{
//											nextReminderSend = tmp;
//										}
//									}
//									break;
//								}
//								#endregion
//							case 2:
//								#region Week
//								{
//									nextReminderSend = nextPossibleReminderSend.AddDays(7 * (Convert.ToInt32(settings[3]) - 1));
//									while (nextReminderSend.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[2]) - 1])
//									{
//										nextReminderSend = nextReminderSend.AddDays(1);
//									}
//									break;
//								}
//								#endregion
//							case 3:
//								#region Month
//								{
//									DateTime tmp = nextPossibleReminderSend.AddDays(-nextPossibleReminderSend.Day);
//									int i = 0;
//									while (tmp.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[3]) - 1] || i != Convert.ToInt32(settings[2]))
//									{
//										tmp = tmp.AddDays(1);
//										if (tmp.DayOfWeek == dayOfWeek[Convert.ToInt32(settings[3]) - 1])
//										{
//											i++;
//										}
//									}
//									nextReminderSend = nextPossibleReminderSend.AddMonths((Convert.ToInt32(settings[4]) - 1));
//									if (tmp < nextPossibleReminderSend)
//									{
//										// Has allready occurred this month
//										nextReminderSend = nextReminderSend.AddMonths(1);
//									}
//									nextReminderSend = nextReminderSend.AddDays(-nextReminderSend.Day);
//									i = 0;
//									while (nextReminderSend.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[3]) - 1] || i != Convert.ToInt32(settings[2]))
//									{
//										nextReminderSend = nextReminderSend.AddDays(1);
//										if (nextReminderSend.DayOfWeek == dayOfWeek[Convert.ToInt32(settings[3]) - 1])
//										{
//											i++;
//										}
//									}
//									break;
//								}
//								#endregion
//						}
//						break;
//					case 2:
//						nextReminderSend = lastLogin.Date.AddHours(Convert.ToInt32(settings[0])).AddDays(Convert.ToInt32(settings[1]) * Convert.ToInt32(settings[2]));
//						while (nextReminderSend < nextPossibleReminderSend)
//						{
//							nextReminderSend = nextReminderSend.AddDays(7);
//						}
//						break;
//				}
//			} catch (Exception) {
//				nextReminderSend = nextPossibleReminderSend.AddYears(10);
//			}
//
//			return nextReminderSend.ToString("yyyy-MM-dd HH:mm");
//		}
//
//		private int createSurveyUser(int userID, int untID, string eml)
//		{
//			int usrID = 0;
//
//			SqlDataReader r = rs("SELECT ProjectRoundID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + untID, "eFormSqlConnection");
//			if (r.Read()) {
//				exec("INSERT INTO ProjectRoundUser (ProjectRoundID,ProjectRoundUnitID,Email) VALUES (" + r.GetInt32(0) + "," + untID + ",'" + eml.Replace("'", "") + "')", "eFormSqlConnection");
//				r.Close();
//				r = rs("SELECT ProjectRoundUserID FROM [ProjectRoundUser] WHERE ProjectRoundUnitID=" + untID + " AND Email = '" + eml.Replace("'", "") + "' ORDER BY ProjectRoundUserID DESC", "eFormSqlConnection");
//				if (r.Read()) {
//					usrID = r.GetInt32(0);
//					exec("INSERT INTO UserProjectRoundUser (UserID, ProjectRoundUnitID, ProjectRoundUserID) VALUES (" + userID + "," + untID + "," + usrID + ")");
//				}
//			}
//			r.Close();
//
//			return usrID;
//		}
//
//		private int feedbackIdx(int level)
//		{
//			return 10 + level;
//		}
//
//		private int actionIdx(int level)
//		{
//			return 15 + level;
//		}
	}
}
