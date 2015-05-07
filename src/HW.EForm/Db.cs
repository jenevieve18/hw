using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Web;

namespace eform
{
	public class Db
	{
		public static string webReqGet(string url)
		{
			StringBuilder sb  = new StringBuilder();

			try
			{
				// Create the request object
				System.Net.HttpWebRequest req = System.Net.HttpWebRequest.Create(url) as System.Net.HttpWebRequest;

				byte[] byteArray = new byte[1024];

				// Set request method to use
				req.Method="GET";

				// Get the response
				HttpWebResponse WebResponse = (HttpWebResponse) req.GetResponse();
			
				// We will read data via the response stream
				Stream StreamReader = WebResponse.GetResponseStream();

				// Used to build entire input
				string tempString = null;
				int count = 0;

				do
				{
					// Fill the buffer with data
					count = StreamReader.Read(byteArray, 0, byteArray.Length);

					// Make sure we read some data
					if (count != 0)
					{
						// translate from bytes to UTF8 text
						tempString = Encoding.UTF8.GetString(byteArray, 0, count);

						// continue building the string
						sb.Append(tempString);
					}
				}
				while (count > 0); // Any more data to read?

				// Clean up
				WebResponse.Close();
				StreamReader.Close();
				req = null;
			}
			catch(Exception){}

			return sb.ToString();
		}

		public static string RemoveHTMLTags(string sHtml)
		{
			const string REGEX_REMOVE_TAGS = @"(<[a-z]+[^>]*>)|(</[a-z\d]+>)";
			return Regex.Replace(sHtml, REGEX_REMOVE_TAGS, " ", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
		}
		public static string trunc(string txt, int len)
		{
			txt = txt.Replace("\t"," ").Replace("\r\n"," ").Replace("\r"," ").Replace("\n"," ");
			if(txt.Length > len)
				return txt.Substring(0,len-3) + "...";
			else
				return txt;
		}
		public static void execExport(int rPRID, int rComparePRID, int rExportSurveyID, int rExportLangID, int rManager, int rAll, int rBase, int rExtended, int rToScreen)
		{
			execExport(rPRID,rComparePRID,rExportSurveyID,rExportLangID,rManager,rAll,rBase,rExtended,rToScreen,0,false);
		}
		public static void execExport(int rPRID, int rComparePRID, int rExportSurveyID, int rExportLangID, int rManager, int rAll, int rBase, int rExtended, int rToScreen, int sponsorID, bool IDS)
		{
			execExport(rPRID.ToString(),rComparePRID,rExportSurveyID,rExportLangID,rManager,rAll,rBase,rExtended,rToScreen,sponsorID,IDS);
		}
		public static void execExport(string rPRID, int rComparePRID, int rExportSurveyID, int rExportLangID, int rManager, int rAll, int rBase, int rExtended, int rToScreen, int sponsorID, bool IDS)
		{
			#region EXPORT

			System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

			System.IO.FileStream fs = null;
			System.IO.StreamWriter f = null;
			string fname = HttpContext.Current.Server.MapPath("tmp/" + DateTime.Now.Ticks + ".sps");

			if(rToScreen == 0)
			{
				fs = new System.IO.FileStream(fname, System.IO.FileMode.Create);
				f = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
			}

			string def = "", syn = "";
			bool nextShouldBreak = true;
			int caseCounter = 0, recordCount = 0, ax = 0;

			//recordCount++; 

			string rowDelim = "\n";				// "\r\n";
			//string defDelim = rowDelim + "/" + recordCount + " ";

			#region SPSS header
			SqlDataReader rs3;
			//if(rBase == 0)
			//{
			if(sponsorID != 0)
			{
				rs3 = Db.sqlRecordSet("SELECT " +
					"UserIdent1, " +	// 0
					"UserIdent2, " +	// 1
					"UserIdent3, " +	// 2
					"UserIdent4, " +	// 3
					"UserIdent5, " +	// 4
					"UserIdent6, " +	// 5
					"UserIdent7, " +	// 6
					"UserIdent8, " +	// 7
					"UserIdent9, " +	// 8
					"UserIdent10, " +	// 9
					"UserCheck1, " +	// 10
					"UserCheck2, " +	// 11
					"UserCheck3, " +	// 12
					"UserNr " +			// 13
					"FROM Sponsor WHERE SponsorID = " + sponsorID);
				if(rs3.Read())
				{
					caseCounter++;
					def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
						"V" + (caseCounter) + "(F6.0)";
					syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserID)." + rowDelim;
					syn += "VARIABLE LABELS sysUserID 'User ID'." + rowDelim;
					nextShouldBreak = false;

					if(!rs3.IsDBNull(13))
					{
						caseCounter++;
						def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
							"V" + (caseCounter) + "(F6.0)";
						syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserNr)." + rowDelim;
						syn += "VARIABLE LABELS sysUserNr '" + rs3.GetString(13) + "'." + rowDelim;
						nextShouldBreak = false;
					}
					if(IDS)
					{
						for(int i=0; i<=9; i++)
						{
							if(!rs3.IsDBNull(i))
							{
								nextShouldBreak = true;
								caseCounter++;
								def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
									"V" + (caseCounter) + "(A250)";
								syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserIdent" + i + ")." + rowDelim;
								syn += "VARIABLE LABELS sysUserIdent" + i + " '" + rs3.GetString(i) + "'." + rowDelim;
								nextShouldBreak = true;
							}
						}
						for(int i=10; i<=12; i++)
						{
							if(!rs3.IsDBNull(i))
							{
								caseCounter++;
								def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
									"V" + (caseCounter) + "(F6.0)";
								syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserCheck" + (i-9) + ")." + rowDelim;
								syn += "VARIABLE LABELS sysUserCheck" + (i-9) + " '" + rs3.GetString(i) + "'." + rowDelim;
								syn += "VALUE LABELS sysUserCheck" + (i-9) + "";
								ax = 0;
								SqlDataReader rs4 = Db.sqlRecordSet("" +
									"SELECT " +
									"SponsorUserCheckID, " +
									"Txt " +
									"FROM SponsorUserCheck " +
									"WHERE SponsorID = " + sponsorID + " AND UserCheckNr = " + (i-9));
								while(rs4.Read())
								{
									ax += rs4.GetString(1).Length;
									if(ax > 115)
									{
										ax = 0;
										syn += rowDelim;
									}
									syn += " " + rs4.GetInt32(0) + " '" + trunc(rs4.GetString(1),115) + "'";
								}
								rs4.Close();
								syn += "." + rowDelim;
								nextShouldBreak = false;
							}
						}
					}
					nextShouldBreak = true;
						caseCounter++;
						def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
							"V" + (caseCounter) + "(A250)";
						syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysNote)." + rowDelim;
						syn += "VARIABLE LABELS sysNote 'Notering'." + rowDelim;
						nextShouldBreak = true;
				}
				rs3.Close();
			}

			caseCounter++;
			def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
				"V" + (caseCounter) + "(F8.0)";
			syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUser)." + rowDelim;
			syn += "VARIABLE LABELS sysUser 'User identification'." + rowDelim;
			nextShouldBreak = false;
			//}

			caseCounter++;
			def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
				"V" + (caseCounter) + "(F8.0)";
			syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysRoundDescription)." + rowDelim;
			syn += "VARIABLE LABELS sysRoundDescription 'Round description'." + rowDelim;
			syn += "VALUE LABELS sysRoundDescription";
			rs3 = Db.sqlRecordSet("SELECT " +
				"pr.ProjectRoundID, " +
				"pr.Internal " +
				"FROM ProjectRound pr " +
				"WHERE pr.ProjectRoundID IN (" + rPRID + ")");
			while(rs3.Read())
			{
				syn += " " + rs3.GetInt32(0) + " '" + trunc(rs3.GetString(1),115) + "'";
			}
			rs3.Close();
			syn += "." + rowDelim;
			nextShouldBreak = false;

			caseCounter++;
			def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
				"V" + (caseCounter) + "(F6.0)";
			syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUnit)." + rowDelim;
			syn += "VARIABLE LABELS sysUnit 'Unit'." + rowDelim;
			syn += "VALUE LABELS sysUnit";
			rs3 = Db.sqlRecordSet("SELECT " +
				"DISTINCT x.ID " +
				"FROM ProjectRoundUnit x " +
				"WHERE x.ID NOT IN (SELECT DISTINCT y.ID FROM [Unit] y) " +
				"AND (x.ProjectRoundID IN (" + rPRID + ")" + (rComparePRID != 0? " OR x.ProjectRoundID = " + rComparePRID : "") + ")");
			while(rs3.Read())
			{
				Db.execute("INSERT INTO [Unit] (ID) VALUES ('" + rs3.GetString(0).Replace("'","''") + "')");
			}
			rs3.Close();
			ax = 0;
			rs3 = Db.sqlRecordSet("SELECT DISTINCT y.UnitID, x.ID FROM Unit y INNER JOIN ProjectRoundUnit x ON y.ID = x.ID WHERE x.ProjectRoundID IN (" + rPRID + ")" + (rComparePRID != 0 ? " OR x.ProjectRoundID = " + rComparePRID : ""));
			while(rs3.Read())
			{
				ax += rs3.GetString(1).Length;
				if(ax > 115)
				{
					ax = 0;
					syn += rowDelim;
				}
				syn += " " + rs3.GetInt32(0) + " '" + trunc(rs3.GetString(1),115) + "'";
			}
			rs3.Close();
			syn += "." + rowDelim;
			nextShouldBreak = false;

			//if(rBase == 0)
			//{
			caseCounter++;
			def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
				"V" + (caseCounter) + "(F1.0)";
			syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysRound)." + rowDelim;
			syn += "VARIABLE LABELS sysRound 'Round'." + rowDelim;
			nextShouldBreak = false;

			caseCounter++;
			def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
				"V" + (caseCounter) + "(F3.0)";
			syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysCase)." + rowDelim;
			syn += "VARIABLE LABELS sysCase 'User case counter'." + rowDelim;
			nextShouldBreak = false;
			//}

			caseCounter++;
			def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
				"V" + (caseCounter) + "(DATETIME17.0)";
			syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysStart)." + rowDelim;
			syn += "VARIABLE LABELS sysStart 'Start Date Time'." + rowDelim;
			nextShouldBreak = false;

			caseCounter++;
			def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
				"V" + (caseCounter) + "(DATETIME17.0)";
			syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysEnd)." + rowDelim;
			syn += "VARIABLE LABELS sysEnd 'End Date Time'." + rowDelim;
			nextShouldBreak = false;

			caseCounter++;
			def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
				"V" + (caseCounter) + "(F8.0)";
			syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExternal)." + rowDelim;
			syn += "VARIABLE LABELS sysExternal 'External ID'." + rowDelim;
			nextShouldBreak = false;

//			if(rBase == 0)
//			{
				nextShouldBreak = true;

				caseCounter++;
				def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
					"V" + (caseCounter) + "(F1.0)";
				syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExt)." + rowDelim;
				syn += "VARIABLE LABELS sysExt 'Extended Survey'." + rowDelim;
				syn += "VALUE LABELS sysExt 0 'No' 1 'Yes'." + rowDelim;
				nextShouldBreak = false;

				caseCounter++;
				def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
					"V" + (caseCounter) + "(F1.0)";
				syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExtTag)." + rowDelim;
				syn += "VARIABLE LABELS sysExtTag 'Extended'." + rowDelim;
				syn += "VALUE LABELS sysExtTag 0 'No' 1 'Yes'." + rowDelim;
				nextShouldBreak = false;

				caseCounter++;
				def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
					"V" + (caseCounter) + "(F3.0)";
				syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysGroup)." + rowDelim;
				syn += "VARIABLE LABELS sysGroup 'Group'." + rowDelim;
				syn += "VALUE LABELS sysGroup";
				rs3 = Db.sqlRecordSet("SELECT DISTINCT g.GroupID, g.GroupDesc FROM [Group] g INNER JOIN ProjectRoundUser x ON g.GroupID = x.GroupID WHERE x.ProjectRoundID IN (" + rPRID + ")" + (rComparePRID != 0 ? " OR x.ProjectRoundID = " + rComparePRID : ""));
				while(rs3.Read())
				{
					syn += " " + rs3.GetInt32(0) + " '" + rs3.GetString(1) + "'";
				}
				rs3.Close();
				syn += "." + rowDelim;
				nextShouldBreak = false;

				//caseCounter++;
				//def += defBeginDelim + "V" + (caseCounter) + "(F8.0)" + defEndDelim;
				//syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExtID)." + rowDelim;
				//syn += "VARIABLE LABELS sysExtra 'External ID'." + rowDelim;

				caseCounter++;
				def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
					"V" + (caseCounter) + "(F2.0)";
				syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysSndCt)." + rowDelim;
				syn += "VARIABLE LABELS sysSndCt 'Invitation send count'." + rowDelim;
				nextShouldBreak = false;

				caseCounter++;
				def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
					"V" + (caseCounter) + "(F2.0)";
				syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysRemCt)." + rowDelim;
				syn += "VARIABLE LABELS sysRemCt 'Reminder send count'." + rowDelim;
				nextShouldBreak = false;

				caseCounter++;
				def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
					"V" + (caseCounter) + "(F1.0)";
				syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysNoSnd)." + rowDelim;
				syn += "VARIABLE LABELS sysNoSnd 'Unsubscribed for further reminders'." + rowDelim;
				syn += "VALUE LABELS sysNoSnd 0 'No' 1 'Yes'." + rowDelim;
				nextShouldBreak = false;

				caseCounter++;
				def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
					"V" + (caseCounter) + "(F1.0)";
				syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserTerm)." + rowDelim;
				syn += "VARIABLE LABELS sysUserTerm 'User terminated/withdrawn'." + rowDelim;
				syn += "VALUE LABELS sysUserTerm 0 'No' 1 'Yes'." + rowDelim;
				nextShouldBreak = false;

				caseCounter++;
				def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
					"V" + (caseCounter) + "(F1.0)";
				syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUnitTerm)." + rowDelim;
				syn += "VARIABLE LABELS sysUnitTerm 'Unit terminated/withdrawn'." + rowDelim;
				syn += "VALUE LABELS sysUnitTerm 0 'No' 1 'Yes'." + rowDelim;
				nextShouldBreak = true;

				caseCounter++;
				def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
					"V" + (caseCounter) + "(A250)";
				syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExtra)." + rowDelim;
				syn += "VARIABLE LABELS sysExtra 'Extra info'." + rowDelim;
				nextShouldBreak = true;

				caseCounter++;
				def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
					"V" + (caseCounter) + "(A250)";
				syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysManager)." + rowDelim;
				syn += "VARIABLE LABELS sysManager 'Manager for listed units'." + rowDelim;
				nextShouldBreak = true;
				
				caseCounter++;
				def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
					"V" + (caseCounter) + "(A250)";
				syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysEmail)." + rowDelim;
				syn += "VARIABLE LABELS sysEmail 'Email'." + rowDelim;
				nextShouldBreak = true;
//			}
			#endregion

			StringBuilder querySelect = new StringBuilder();
			StringBuilder queryJoin = new StringBuilder();
			string varTypes = "", varAttrs = "", varPositions = "", varBreaks = "";

			int varCount = 0, queries = 1, varPerRecord = 0, colsPerRecord = 0, varPos = 0, queryDivide = 40;
			string SQL = "SELECT " +
				"sq.QuestionID, " +			// 0
				"qo.OptionID, " +			// 1
				"o.OptionType, " +			// 2
				"q.Variablename, " +		// 3
				"o.Variablename, " +		// 4
				"ql.Question, " +			// 5
				"(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), " +	// 6
				"(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID), " +							// 7
				"sq.SortOrder, " +			// 8
				"sqo.SortOrder, " +			// 9
				"(SELECT COUNT(*) FROM [ProjectRoundUserQO] x INNER JOIN [ProjectRoundUser] y ON x.ProjectRoundUserID = y.ProjectRoundUserID WHERE (y.ProjectRoundID IN (" + rPRID + ")" + (rComparePRID != 0 ? " OR y.ProjectRoundID = " + rComparePRID : "") + ") AND x.QuestionID = sq.QuestionID AND x.OptionID = qo.OptionID) " +	// 10
				"FROM SurveyQuestion sq " +
				"INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " + 
				"INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " + 
				"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " + 
				"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
				"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID " +
				"WHERE sq.SurveyID = " + rExportSurveyID + " " +
				"AND ql.LangID = " + rExportLangID + " " +
				"ORDER BY sq.SortOrder, sqo.SortOrder";
			rs3 = Db.sqlRecordSet(SQL);
			while(rs3.Read())
			{
				#region Header
				string var = (rs3.IsDBNull(3) || rs3.GetString(3).Trim() == "" ? "Q" + rs3.GetInt32(0).ToString() : rs3.GetString(3)).Replace(" ","_").Replace("-","_").Replace("/","_");
				string desc = (rs3.IsDBNull(5) || rs3.GetString(5).Trim() == "" ? var : rs3.GetString(5));
				if(rs3.GetInt32(6) > 1)
				{
					var += (rs3.IsDBNull(4) || rs3.GetString(4).Trim() == "" ? "A" + rs3.GetInt32(1).ToString() : rs3.GetString(4));
					desc += var;
				}
				desc = trunc(RemoveHTMLTags(desc),230);

				syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + ")." + rowDelim;
				syn += "VARIABLE LABELS " + var + " '" + desc + "'." + rowDelim;

				string defDelim = " ";
				if(varCount == 0 || varPerRecord > 15 || colsPerRecord > 235 || rs3.GetInt32(2) == 2)
				{
					recordCount++;
					varPerRecord = 0;
					colsPerRecord = 0;
					defDelim = rowDelim + "/" + recordCount + " ";
					varBreaks += (varBreaks != "" ? "," : "") + "1";
				}
				else
				{
					varBreaks += (varBreaks != "" ? "," : "") + "0";
				}

				switch(rs3.GetInt32(2))
				{
					case 0:
						bool zone = rs3.GetInt32(2) == 9 && rs3.GetInt32(7) > 2;
						syn += "VALUE LABELS " + var + (zone ? "Zone" : "");
						int cx = 0;
						ax = 0;
						SqlDataReader rs4 = Db.sqlRecordSet("SELECT " +
							"ocl.Text, " +
							"oc.ExportValue " +
							"FROM OptionComponents oc " +
							"INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + rExportLangID + " " +
							"WHERE oc.OptionID = " + rs3.GetInt32(1) + " " +
							"ORDER BY oc.SortOrder");
						while(rs4.Read())
						{
							string s = trunc(RemoveHTMLTags(HttpContext.Current.Server.HtmlDecode(rs4.GetString(0))),115);
							ax += s.Length;
							if(ax > 115)
							{
								ax = 0;
								syn += rowDelim;
							}
							syn += " " + (zone ? (cx++) : rs4.GetInt32(1)) + " '" + s + "'";
						}
						rs4.Close();
						syn += "." + rowDelim;
						break;
					case 1:	
						varPos = 3;
						colsPerRecord += varPos;
						varPerRecord ++;
						varPositions += (varPositions != "" ? "," : "") + varPos;
						def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".0)";
						goto case 0;
					case 2: 
						varPos = (Math.Min(250,Db.sqlGetInt32("SELECT MAX(LEN(CAST(ValueText AS VARCHAR(255)))) FROM AnswerValue WHERE QuestionID = " + rs3.GetInt32(0) + " AND OptionID = " + rs3.GetInt32(1))+16));
						colsPerRecord += 250;//varPos;
						varPerRecord ++;
						varPositions += (varPositions != "" ? "," : "") + 250;//varPos;
						def += defDelim + "V" + (caseCounter) + "(A" + varPos + ")";
						break;
					case 3: 
						goto case 1;
					case 4:
						varPos = 8;
						colsPerRecord += varPos;
						varPerRecord ++;
						varPositions += (varPositions != "" ? "," : "") + varPos;
						def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".2)"; 
						break; 
					case 9:
						varPos = 3;
						colsPerRecord += varPos;
						varPerRecord ++;
						varPositions += (varPositions != "" ? "," : "") + varPos;

						if(rs3.GetInt32(7) > 2)
						{
							def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".0)";

							defDelim = " ";
							colsPerRecord += varPos;
							varPerRecord ++;
								
							syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + "Zone)." + rowDelim;
							syn += "VARIABLE LABELS " + var + "Zone '" + desc + "'." + rowDelim;
						}

						def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".0)"; 
						goto case 0;
				}
				#endregion

				if(varCount != 0 && varCount % queryDivide == 0)
				{
					querySelect.Append("¤");
					queryJoin.Append("¤");
					queries++;
				}
				varCount++;
				switch(rs3.GetInt32(2))
				{
					case 0:
						#region Link
						querySelect.Append(",op" + varCount + ".ExportValue AS var" + varCount + " ");
						queryJoin.Append("LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
							"av" + varCount + ".AnswerID = a.AnswerID " +
							"AND " +
							"av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
							"AND " +
							"av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
							"AND " +
							"av" + varCount + ".DeletedSessionID IS NULL " +
							(rs3.GetInt32(10) > 0 ?
							"LEFT OUTER JOIN ProjectRoundUserQO pruqo" + varCount + " ON " +
							"pruqo" + varCount + ".ProjectRoundUserID = usr.ProjectRoundUserID " +
							"AND " +
							"pruqo" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
							"AND " +
							"pruqo" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
							"LEFT OUTER JOIN OptionComponents op" + varCount + " ON " +
							"op" + varCount + ".OptionID = ISNULL(av" + varCount + ".OptionID,pruqo" + varCount + ".OptionID) " +
							"AND " +
							"op" + varCount + ".OptionComponentID = ISNULL(av" + varCount + ".ValueInt,CAST(pruqo" + varCount + ".Answer AS INT)) " +
							""
							:
							"LEFT OUTER JOIN OptionComponents op" + varCount + " ON " +
							"op" + varCount + ".OptionID = av" + varCount + ".OptionID " +
							"AND " +
							"op" + varCount + ".OptionComponentID = av" + varCount + ".ValueInt " +
							""));
						#endregion
						varTypes += (varTypes != "" ? "," : "") + "1";
						varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);
						break;
					case 1:
						goto case 0;
					case 2:
						#region Freetext
						if(rs3.GetInt32(10) > 0)
						{
							querySelect.Append(",ISNULL(av" + varCount + ".ValueText,pruqo" + varCount + ".Answer) AS var" + varCount + " ");
						}
						else
						{
							querySelect.Append(",av" + varCount + ".ValueText AS var" + varCount + " ");
						}
						queryJoin.Append("LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
							"av" + varCount + ".AnswerID = a.AnswerID " +
							"AND " +
							"av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
							"AND " +
							"av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
							"AND " +
							"av" + varCount + ".DeletedSessionID IS NULL " +
							(rs3.GetInt32(10) > 0 ?
							"LEFT OUTER JOIN ProjectRoundUserQO pruqo" + varCount + " ON " +
							"pruqo" + varCount + ".ProjectRoundUserID = usr.ProjectRoundUserID " +
							"AND " +
							"pruqo" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
							"AND " +
							"pruqo" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " : ""));
						#endregion
						varTypes += (varTypes != "" ? "," : "") + "2";
						varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);
						break;
					case 3:
						goto case 0;
					case 4:
						#region Decimal
						if(rs3.GetInt32(10) > 0)
						{
							querySelect.Append(",ISNULL(av" + varCount + ".ValueDecimal,CAST(pruqo" + varCount + ".Answer AS DECIMAL)) AS var" + varCount + " ");
						}
						else
						{
							querySelect.Append(",av" + varCount + ".ValueDecimal AS var" + varCount + " ");
						}
						queryJoin.Append("LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
							"av" + varCount + ".AnswerID = a.AnswerID " +
							"AND " +
							"av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
							"AND " +
							"av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
							"AND " +
							"av" + varCount + ".DeletedSessionID IS NULL " +
							(rs3.GetInt32(10) > 0 ?
							"LEFT OUTER JOIN ProjectRoundUserQO pruqo" + varCount + " ON " +
							"pruqo" + varCount + ".ProjectRoundUserID = usr.ProjectRoundUserID " +
							"AND " +
							"pruqo" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
							"AND " +
							"pruqo" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " : ""));
						#endregion
						varTypes += (varTypes != "" ? "," : "") + "4";
						varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);
						break;
					case 9:
						#region VAS
						if(rs3.GetInt32(10) > 0)
						{
							querySelect.Append(",ISNULL(av" + varCount + ".ValueInt,CAST(pruqo" + varCount + ".Answer AS INT)) AS var" + varCount + " ");
						}
						else
						{
							querySelect.Append(",av" + varCount + ".ValueInt AS var" + varCount + " ");
						}
						queryJoin.Append("LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
							"av" + varCount + ".AnswerID = a.AnswerID " +
							"AND " +
							"av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
							"AND " +
							"av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
							"AND " +
							"av" + varCount + ".DeletedSessionID IS NULL " +
							(rs3.GetInt32(10) > 0 ?
							"LEFT OUTER JOIN ProjectRoundUserQO pruqo" + varCount + " ON " +
							"pruqo" + varCount + ".ProjectRoundUserID = usr.ProjectRoundUserID " +
							"AND " +
							"pruqo" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
							"AND " +
							"pruqo" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " : ""));
						#endregion
						varTypes += (varTypes != "" ? "," : "") + "9";
						varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);
						break;
				}

				if(rManager != 0 && (rs3.GetInt32(2) == 1 || rs3.GetInt32(2) == 3 || rs3.GetInt32(2) == 4 || rs3.GetInt32(2) == 9))
				{
					#region Manager
					syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + "_v)." + rowDelim;
					syn += "VARIABLE LABELS " + var + "_v '" + desc + " / User mean value'." + rowDelim;

					defDelim = " ";
					if(varPerRecord > 15 || colsPerRecord > 15*15)
					{
						recordCount++;
						varPerRecord = 0;
						colsPerRecord = 0;
						defDelim = rowDelim + "/" + recordCount + " ";
						varBreaks += (varBreaks != "" ? "," : "") + "1";
					}
					else
					{
						varBreaks += (varBreaks != "" ? "," : "") + "0";
					}

					varPos = 8;
					colsPerRecord += varPos;
					varPerRecord ++;
					varPositions += (varPositions != "" ? "," : "") + varPos;
					def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".2)";

					if(varCount % queryDivide == 0)
					{
						querySelect.Append("¤");
						queryJoin.Append("¤");
						queries++;
					}
					varCount++;
					querySelect.Append(", tmp" + varCount + ".var" + varCount + " ");
					#region Queries
					switch(rs3.GetInt32(2))
					{
						case 0:
							#region Link
							queryJoin.Append("LEFT OUTER JOIN " +
								"(" +
								"SELECT AVG(CAST(op" + varCount + ".ExportValue AS DECIMAL)) AS var" + varCount + ", prum" + varCount + ".ProjectRoundUserID, uuX" + varCount + ".ProjectRoundID " +
								"FROM ProjectRoundUnitManager prum" + varCount + " " +
								"INNER JOIN ProjectRoundUnit prumU" + varCount + " ON prum" + varCount + ".ProjectRoundUnitID = prumU" + varCount + ".ProjectRoundUnitID " +
								"INNER JOIN ProjectRoundUnit uuX" + varCount + " ON (" +
								"uuX" + varCount + ".ProjectRoundUnitID = prumU" + varCount + ".ProjectRoundUnitID " +
								"AND " +
								"uuX" + varCount + ".ProjectRoundID IN (" + rPRID + ") " +
								(rComparePRID != 0 ? 
								"OR " +
								"uuX" + varCount + ".ID = prumU" + varCount + ".ID " +
								"AND " +
								"uuX" + varCount + ".ProjectRoundID = " + rComparePRID + " " +
								"" : "") +
								") " +
								"INNER JOIN ProjectRoundUser uX" + varCount + " ON uX" + varCount + ".ProjectRoundUnitID = uuX" + varCount + ".ProjectRoundUnitID " +
								"LEFT OUTER JOIN Answer aX" + varCount + " ON uX" + varCount + ".ProjectRoundUserID = aX" + varCount + ".ProjectRoundUserID " + (rAll != 0 ? "" : "AND aX" + varCount + ".EndDT IS NOT NULL ") +
								"LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
								"av" + varCount + ".AnswerID = aX" + varCount + ".AnswerID AND " +
								"av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " AND " +
								"av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " AND " +
								"av" + varCount + ".DeletedSessionID IS NULL " +
								(rs3.GetInt32(10) > 0 ?
								"LEFT OUTER JOIN ProjectRoundUserQO pruqo" + varCount + " ON " +
								"pruqo" + varCount + ".ProjectRoundUserID = uX" + varCount + ".ProjectRoundUserID AND " +
								"pruqo" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " AND " +
								"pruqo" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
								"LEFT OUTER JOIN OptionComponents op" + varCount + " ON " +
								"op" + varCount + ".OptionID = ISNULL(av" + varCount + ".OptionID,pruqo" + varCount + ".OptionID) AND " +
								"op" + varCount + ".OptionComponentID = ISNULL(av" + varCount + ".ValueInt,CAST(pruqo" + varCount + ".Answer AS INT)) " +
								""
								:
								"LEFT OUTER JOIN OptionComponents op" + varCount + " ON " +
								"op" + varCount + ".OptionID = av" + varCount + ".OptionID AND " +
								"op" + varCount + ".OptionComponentID = av" + varCount + ".ValueInt " +
								"") +
								"GROUP BY prum" + varCount + ".ProjectRoundUserID, uuX" + varCount + ".ProjectRoundID) tmp" + varCount + " ON tmp" + varCount + ".ProjectRoundID = usr.ProjectRoundID AND tmp" + varCount + ".ProjectRoundUserID = ISNULL(usrCurrent.ProjectRoundUserID,usr.ProjectRoundUserID) ");
							#endregion
							break;
						case 1:
							goto case 0;
						case 3:
							goto case 0;
						case 4:
							#region Decimal
							queryJoin.Append("LEFT OUTER JOIN " +
								"(SELECT " +
								(rs3.GetInt32(10) > 0 ? 
								"AVG(ISNULL(av" + varCount + ".ValueDecimal,CAST(pruqo" + varCount + ".Answer AS DECIMAL))) AS var" + varCount + " "
								:
								"AVG(av" + varCount + ".ValueDecimal) AS var" + varCount + " "
								) +
								", prum" + varCount + ".ProjectRoundUserID, uuX" + varCount + ".ProjectRoundID " +
								"FROM ProjectRoundUnitManager prum" + varCount + " " +
								"INNER JOIN ProjectRoundUnit prumU" + varCount + " ON prum" + varCount + ".ProjectRoundUnitID = prumU" + varCount + ".ProjectRoundUnitID " +
								"INNER JOIN ProjectRoundUnit uuX" + varCount + " ON (" +
								"uuX" + varCount + ".ProjectRoundUnitID = prumU" + varCount + ".ProjectRoundUnitID " +
								"AND " +
								"uuX" + varCount + ".ProjectRoundID IN (" + rPRID + ") " +
								(rComparePRID != 0 ? 
								"OR " +
								"uuX" + varCount + ".ID = prumU" + varCount + ".ID " +
								"AND " +
								"uuX" + varCount + ".ProjectRoundID = " + rComparePRID + " " +
								"" : "") +
								") " +
								"INNER JOIN ProjectRoundUser uX" + varCount + " ON uX" + varCount + ".ProjectRoundUnitID = uuX" + varCount + ".ProjectRoundUnitID " +
								"LEFT OUTER JOIN Answer aX" + varCount + " ON uX" + varCount + ".ProjectRoundUserID = aX" + varCount + ".ProjectRoundUserID " + (rAll != 0 ? "" : "AND aX" + varCount + ".EndDT IS NOT NULL ") +
								"LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
								"av" + varCount + ".AnswerID = aX" + varCount + ".AnswerID AND " +
								"av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " AND " +
								"av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " AND " +
								"av" + varCount + ".DeletedSessionID IS NULL " +
								(rs3.GetInt32(10) > 0 ?
								"LEFT OUTER JOIN ProjectRoundUserQO pruqo" + varCount + " ON " +
								"pruqo" + varCount + ".ProjectRoundUserID = uX" + varCount + ".ProjectRoundUserID AND " +
								"pruqo" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " AND " +
								"pruqo" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
								"" : "") +
								"GROUP BY prum" + varCount + ".ProjectRoundUserID, uuX" + varCount + ".ProjectRoundID) tmp" + varCount + " ON tmp" + varCount + ".ProjectRoundID = usr.ProjectRoundID AND tmp" + varCount + ".ProjectRoundUserID = ISNULL(usrCurrent.ProjectRoundUserID,usr.ProjectRoundUserID) ");
							#endregion
							break;
						case 9:
							#region VAS
							queryJoin.Append("LEFT OUTER JOIN " +
								"(SELECT " +
								(rs3.GetInt32(10) > 0 ? 
								"AVG(CAST(ISNULL(av" + varCount + ".ValueInt,pruqo" + varCount + ".Answer) AS DECIMAL)) AS var" + varCount + " "
								:
								"AVG(CAST(av" + varCount + ".ValueInt AS DECIMAL)) AS var" + varCount + " "
								) +
								", prum" + varCount + ".ProjectRoundUserID, uuX" + varCount + ".ProjectRoundID " +
								"FROM ProjectRoundUnitManager prum" + varCount + " " +
								"INNER JOIN ProjectRoundUnit prumU" + varCount + " ON prum" + varCount + ".ProjectRoundUnitID = prumU" + varCount + ".ProjectRoundUnitID " +
								"INNER JOIN ProjectRoundUnit uuX" + varCount + " ON (" +
								"uuX" + varCount + ".ProjectRoundUnitID = prumU" + varCount + ".ProjectRoundUnitID " +
								"AND " +
								"uuX" + varCount + ".ProjectRoundID IN (" + rPRID + ") " +
								(rComparePRID != 0 ? 
								"OR " +
								"uuX" + varCount + ".ID = prumU" + varCount + ".ID " +
								"AND " +
								"uuX" + varCount + ".ProjectRoundID = " + rComparePRID + " " +
								"" : "") +
								") " +
								"INNER JOIN ProjectRoundUser uX" + varCount + " ON uX" + varCount + ".ProjectRoundUnitID = uuX" + varCount + ".ProjectRoundUnitID " +
								"LEFT OUTER JOIN Answer aX" + varCount + " ON uX" + varCount + ".ProjectRoundUserID = aX" + varCount + ".ProjectRoundUserID " + (rAll != 0 ? "" : "AND aX" + varCount + ".EndDT IS NOT NULL ") +
								"LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
								"av" + varCount + ".AnswerID = aX" + varCount + ".AnswerID AND " +
								"av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " AND " +
								"av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " AND " +
								"av" + varCount + ".DeletedSessionID IS NULL " +
								(rs3.GetInt32(10) > 0 ?
								"LEFT OUTER JOIN ProjectRoundUserQO pruqo" + varCount + " ON " +
								"pruqo" + varCount + ".ProjectRoundUserID = uX" + varCount + ".ProjectRoundUserID AND " +
								"pruqo" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " AND " +
								"pruqo" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
								"" : "") +
								"GROUP BY prum" + varCount + ".ProjectRoundUserID, uuX" + varCount + ".ProjectRoundID) tmp" + varCount + " ON tmp" + varCount + ".ProjectRoundID = usr.ProjectRoundID AND tmp" + varCount + ".ProjectRoundUserID = ISNULL(usrCurrent.ProjectRoundUserID,usr.ProjectRoundUserID) ");
							#endregion
							break;
					}
					#endregion
					varTypes += (varTypes != "" ? "," : "") + "4";
					varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);

					syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + "_c)." + rowDelim;
					syn += "VARIABLE LABELS " + var + "_c '" + desc + " / User count'." + rowDelim;

					defDelim = " ";
					if(varPerRecord > 15 || colsPerRecord > 15*15)
					{
						recordCount++;
						varPerRecord = 0;
						colsPerRecord = 0;
						defDelim = rowDelim + "/" + recordCount + " ";
						varBreaks += (varBreaks != "" ? "," : "") + "1";
					}
					else
					{
						varBreaks += (varBreaks != "" ? "," : "") + "0";
					}

					varPos = 6;
					colsPerRecord += varPos;
					varPerRecord ++;
					varPositions += (varPositions != "" ? "," : "") + varPos;
					def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".0)";

					if(varCount % queryDivide == 0)
					{
						querySelect.Append("¤");
						queryJoin.Append("¤");
						queries++;
					}
					varCount++;
					querySelect.Append(", tmp" + varCount + ".var" + (varCount) + " ");
					#region Queries
					switch(rs3.GetInt32(2))
					{
						case 0:
							#region Link
							queryJoin.Append("LEFT OUTER JOIN " +
								"(" +
								"SELECT COUNT(op" + varCount + ".ExportValue) AS var" + (varCount) + ", prum" + varCount + ".ProjectRoundUserID, uuX" + varCount + ".ProjectRoundID " +
								"FROM ProjectRoundUnitManager prum" + varCount + " " +
								"INNER JOIN ProjectRoundUnit prumU" + varCount + " ON prum" + varCount + ".ProjectRoundUnitID = prumU" + varCount + ".ProjectRoundUnitID " +
								"INNER JOIN ProjectRoundUnit uuX" + varCount + " ON (" +
								"uuX" + varCount + ".ProjectRoundUnitID = prumU" + varCount + ".ProjectRoundUnitID " +
								"AND " +
								"uuX" + varCount + ".ProjectRoundID IN (" + rPRID + ") " +
								(rComparePRID != 0 ? 
								"OR " +
								"uuX" + varCount + ".ID = prumU" + varCount + ".ID " +
								"AND " +
								"uuX" + varCount + ".ProjectRoundID = " + rComparePRID + " " +
								"" : "") +
								") " +
								"INNER JOIN ProjectRoundUser uX" + varCount + " ON uX" + varCount + ".ProjectRoundUnitID = uuX" + varCount + ".ProjectRoundUnitID " +
								"LEFT OUTER JOIN Answer aX" + varCount + " ON uX" + varCount + ".ProjectRoundUserID = aX" + varCount + ".ProjectRoundUserID " + (rAll != 0 ? "" : "AND aX" + varCount + ".EndDT IS NOT NULL ") +
								"LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
								"av" + varCount + ".AnswerID = aX" + varCount + ".AnswerID AND " +
								"av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " AND " +
								"av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " AND " +
								"av" + varCount + ".DeletedSessionID IS NULL " +
								(rs3.GetInt32(10) > 0 ?
								"LEFT OUTER JOIN ProjectRoundUserQO pruqo" + varCount + " ON " +
								"pruqo" + varCount + ".ProjectRoundUserID = uX" + varCount + ".ProjectRoundUserID AND " +
								"pruqo" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " AND " +
								"pruqo" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
								"LEFT OUTER JOIN OptionComponents op" + varCount + " ON " +
								"op" + varCount + ".OptionID = ISNULL(av" + varCount + ".OptionID,pruqo" + varCount + ".OptionID) AND " +
								"op" + varCount + ".OptionComponentID = ISNULL(av" + varCount + ".ValueInt,CAST(pruqo" + varCount + ".Answer AS INT)) " +
								""
								:
								"LEFT OUTER JOIN OptionComponents op" + varCount + " ON " +
								"op" + varCount + ".OptionID = av" + varCount + ".OptionID AND " +
								"op" + varCount + ".OptionComponentID = av" + varCount + ".ValueInt " +
								"") +
								"GROUP BY prum" + varCount + ".ProjectRoundUserID, uuX" + varCount + ".ProjectRoundID) tmp" + varCount + " ON tmp" + varCount + ".ProjectRoundID = usr.ProjectRoundID AND tmp" + varCount + ".ProjectRoundUserID = ISNULL(usrCurrent.ProjectRoundUserID,usr.ProjectRoundUserID) ");
							#endregion
							break;
						case 1:
							goto case 0;
						case 3:
							goto case 0;
						case 4:
							#region Decimal
							queryJoin.Append("LEFT OUTER JOIN " +
								"(SELECT " +
								(rs3.GetInt32(10) > 0 ? 
								"COUNT(ISNULL(av" + varCount + ".ValueDecimal,pruqo" + varCount + ".Answer)) AS var" + (varCount)
								:
								"COUNT(av" + varCount + ".ValueDecimal) AS var" + (varCount)
								) +
								", prum" + varCount + ".ProjectRoundUserID, uuX" + varCount + ".ProjectRoundID " +
								"FROM ProjectRoundUnitManager prum" + varCount + " " +
								"INNER JOIN ProjectRoundUnit prumU" + varCount + " ON prum" + varCount + ".ProjectRoundUnitID = prumU" + varCount + ".ProjectRoundUnitID " +
								"INNER JOIN ProjectRoundUnit uuX" + varCount + " ON (" +
								"uuX" + varCount + ".ProjectRoundUnitID = prumU" + varCount + ".ProjectRoundUnitID " +
								"AND " +
								"uuX" + varCount + ".ProjectRoundID IN (" + rPRID + ") " +
								(rComparePRID != 0 ? 
								"OR " +
								"uuX" + varCount + ".ID = prumU" + varCount + ".ID " +
								"AND " +
								"uuX" + varCount + ".ProjectRoundID = " + rComparePRID + " " +
								"" : "") +
								") " +
								"INNER JOIN ProjectRoundUser uX" + varCount + " ON uX" + varCount + ".ProjectRoundUnitID = uuX" + varCount + ".ProjectRoundUnitID " +
								"LEFT OUTER JOIN Answer aX" + varCount + " ON uX" + varCount + ".ProjectRoundUserID = aX" + varCount + ".ProjectRoundUserID " + (rAll != 0 ? "" : "AND aX" + varCount + ".EndDT IS NOT NULL ") +
								"LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
								"av" + varCount + ".AnswerID = aX" + varCount + ".AnswerID AND " +
								"av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " AND " +
								"av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " AND " +
								"av" + varCount + ".DeletedSessionID IS NULL " +
								(rs3.GetInt32(10) > 0 ?
								"LEFT OUTER JOIN ProjectRoundUserQO pruqo" + varCount + " ON " +
								"pruqo" + varCount + ".ProjectRoundUserID = uX" + varCount + ".ProjectRoundUserID AND " +
								"pruqo" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " AND " +
								"pruqo" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
								"" : "") +
								"GROUP BY prum" + varCount + ".ProjectRoundUserID, uuX" + varCount + ".ProjectRoundID) tmp" + varCount + " ON tmp" + varCount + ".ProjectRoundID = usr.ProjectRoundID AND tmp" + varCount + ".ProjectRoundUserID = ISNULL(usrCurrent.ProjectRoundUserID,usr.ProjectRoundUserID) ");
							#endregion
							break;
						case 9:
							#region VAS
							queryJoin.Append("LEFT OUTER JOIN " +
								"(SELECT " +
								(rs3.GetInt32(10) > 0 ? 
								"COUNT(ISNULL(av" + varCount + ".ValueInt,pruqo" + varCount + ".Answer)) AS var" + (varCount)
								:
								"COUNT(av" + varCount + ".ValueInt) AS var" + (varCount)
								) +
								", prum" + varCount + ".ProjectRoundUserID, uuX" + varCount + ".ProjectRoundID " +
								"FROM ProjectRoundUnitManager prum" + varCount + " " +
								"INNER JOIN ProjectRoundUnit prumU" + varCount + " ON prum" + varCount + ".ProjectRoundUnitID = prumU" + varCount + ".ProjectRoundUnitID " +
								"INNER JOIN ProjectRoundUnit uuX" + varCount + " ON (" +
								"uuX" + varCount + ".ProjectRoundUnitID = prumU" + varCount + ".ProjectRoundUnitID " +
								"AND " +
								"uuX" + varCount + ".ProjectRoundID IN (" + rPRID + ") " +
								(rComparePRID != 0 ? 
								"OR " +
								"uuX" + varCount + ".ID = prumU" + varCount + ".ID " +
								"AND " +
								"uuX" + varCount + ".ProjectRoundID = " + rComparePRID + " " +
								"" : "") +
								") " +
								"INNER JOIN ProjectRoundUser uX" + varCount + " ON uX" + varCount + ".ProjectRoundUnitID = uuX" + varCount + ".ProjectRoundUnitID " +
								"LEFT OUTER JOIN Answer aX" + varCount + " ON uX" + varCount + ".ProjectRoundUserID = aX" + varCount + ".ProjectRoundUserID " + (rAll != 0 ? "" : "AND aX" + varCount + ".EndDT IS NOT NULL ") +
								"LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
								"av" + varCount + ".AnswerID = aX" + varCount + ".AnswerID AND " +
								"av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " AND " +
								"av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " AND " +
								"av" + varCount + ".DeletedSessionID IS NULL " +
								(rs3.GetInt32(10) > 0 ?
								"LEFT OUTER JOIN ProjectRoundUserQO pruqo" + varCount + " ON " +
								"pruqo" + varCount + ".ProjectRoundUserID = uX" + varCount + ".ProjectRoundUserID AND " +
								"pruqo" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " AND " +
								"pruqo" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
								"" : "") +
								"GROUP BY prum" + varCount + ".ProjectRoundUserID, uuX" + varCount + ".ProjectRoundID) tmp" + varCount + " ON tmp" + varCount + ".ProjectRoundID = usr.ProjectRoundID AND tmp" + varCount + ".ProjectRoundUserID = ISNULL(usrCurrent.ProjectRoundUserID,usr.ProjectRoundUserID) ");
							#endregion
							break;
					}
					#endregion
					varTypes += (varTypes != "" ? "," : "") + "9";
					varAttrs += (varAttrs != "" ? "," : "") + "1";
					#endregion
				}
			}
			rs3.Close();
			string[] varType = varTypes.Split(','), varAttr = varAttrs.Split(','), varPosition = varPositions.Split(','), varBreak = varBreaks.Split(',');

			//string[] queryResult = new string[queries];
			ArrayList queryResult = new ArrayList();

			int caseCount = 0;
			for(int q=0; q<queries; q++)
			{
				StringBuilder output = new StringBuilder();

				int userID = 0, userCaseCounter = 0;
				#region User base
				if(rBase == 0)
				{
					SQL = "SELECT DISTINCT " +
						"ISNULL(usrCurrent.ProjectRoundUserID,usr.ProjectRoundUserID) AS PRUID, " +
						"q.UnitID, " +
						"usr.ProjectRoundID, " +
						"a.StartDT, " +
						"a.EndDT, " +
						"usr.Extended, " +			// 5
						"usr.ExtendedTag, " +
						"usr.GroupID, " +
						"usr.Extra, " +
						"usr.ExternalID, " +
						"usr.SendCount, " +			// 10
						"usr.ReminderCount, " +
						"usr.NoSend, " +
						"usr.Terminated, " +
						"u.Terminated, " +
						"(SELECT COUNT(*) FROM ProjectRoundUnitManager x WHERE x.ProjectRoundUserID = ISNULL(usrCurrent.ProjectRoundUserID,usr.ProjectRoundUserID)), " +		// 15
						"usr.Email, " +
						"a.AnswerID " +
						querySelect.ToString().Split('¤')[q] +
						"FROM ProjectRoundUser usr " +
						(sponsorID != 0 ? "" +
						"INNER JOIN UserProjectRoundUser xUPRU ON xUPRU.ProjectRoundUserID = usr.ProjectRoundUserID " +
						"INNER JOIN [User] xU ON xUPRU.UserID = xU.UserID AND xU.SponsorID = " + sponsorID + " " +
						"" : "") +
						"INNER JOIN ProjectRoundUnit u ON usr.ProjectRoundUnitID = u.ProjectRoundUnitID " +
						"INNER JOIN [Unit] q ON u.ID = q.ID " +
						"LEFT OUTER JOIN ProjectRoundUser usrCurrent ON usrCurrent.ProjectRoundID IN (" + rPRID + ") " +
						"AND (usr.ProjectRoundUserID = usrCurrent.ProjectRoundUserID" + (rComparePRID != 0 ? " OR usr.ExternalID = usrCurrent.ExternalID OR usr.Email = usrCurrent.Email" : "") + ") " +
						"LEFT OUTER JOIN Answer a ON usr.ProjectRoundUserID = a.ProjectRoundUserID " + (rAll != 0 ? "" : "AND a.EndDT IS NOT NULL ") +
						queryJoin.ToString().Split('¤')[q] +
						"WHERE (usr.ProjectRoundID IN (" + rPRID + ")" + (rComparePRID != 0 ? " OR usr.ProjectRoundID = " + rComparePRID : "") + ") " +
						(rExtended != 0 ? "AND ISNULL(usrCurrent.Extended,usr.Extended) IS NOT NULL AND ISNULL(usrCurrent.Extended,usr.Extended) = 1 " : "") +
						"ORDER BY " +
						//"u.ID, " +
						"ISNULL(usrCurrent.ProjectRoundUserID,usr.ProjectRoundUserID), " +
						"usr.ProjectRoundID, " +
						"a.AnswerID";
				}
				else if(rBase == 2)
				{
					SQL = "SELECT DISTINCT " +
						"ISNULL(usrCurrent.ProjectRoundUserID,usr.ProjectRoundUserID) AS PRUID, " +
						"q.UnitID, " +
						"usr.ProjectRoundID, " +
						"a.StartDT, " +
						"a.EndDT, " +
						"usr.Extended, " +			// 5
						"usr.ExtendedTag, " +
						"usr.GroupID, " +
						"usr.Extra, " +
						"usr.ExternalID, " +
						"usr.SendCount, " +			// 10
						"usr.ReminderCount, " +
						"usr.NoSend, " +
						"usr.Terminated, " +
						"u.Terminated, " +
						"(SELECT COUNT(*) FROM ProjectRoundUnitManager x WHERE x.ProjectRoundUserID = ISNULL(usrCurrent.ProjectRoundUserID,usr.ProjectRoundUserID)), " +		// 15
						"usr.Email, " +
						"a.AnswerID " +
						querySelect.ToString().Split('¤')[q] +
						"FROM ProjectRoundUser usr " +
						(sponsorID != 0 ? "" +
						"INNER JOIN UserProjectRoundUser xUPRU ON xUPRU.ProjectRoundUserID = usr.ProjectRoundUserID " +
						"INNER JOIN [User] xU ON xUPRU.UserID = xU.UserID AND xU.SponsorID = " + sponsorID + " " +
						"" : "") +
						"INNER JOIN ProjectRoundUnit u ON usr.ProjectRoundUnitID = u.ProjectRoundUnitID " +
						"INNER JOIN [Unit] q ON u.ID = q.ID " +
						"LEFT OUTER JOIN ProjectRoundUser usrCurrent ON usrCurrent.ProjectRoundID IN (" + rPRID + ") " +
						"AND (usr.ProjectRoundUserID = usrCurrent.ProjectRoundUserID" + (rComparePRID != 0 ? " OR usr.ExternalID = usrCurrent.ExternalID OR usr.Email = usrCurrent.Email" : "") + ") " +
						"INNER JOIN Answer a ON usr.ProjectRoundUserID = a.ProjectRoundUserID " + (rAll != 0 ? "" : "AND a.EndDT IS NOT NULL ") +
						queryJoin.ToString().Split('¤')[q] +
						"WHERE u.SurveyID = " + rExportSurveyID + " AND (usr.ProjectRoundID IN (" + rPRID + ")" + (rComparePRID != 0 ? " OR usr.ProjectRoundID = " + rComparePRID : "") + ") " +
						(rExtended != 0 ? "AND ISNULL(usrCurrent.Extended,usr.Extended) IS NOT NULL AND ISNULL(usrCurrent.Extended,usr.Extended) = 1 " : "") +
						"ORDER BY " +
						//"u.ID, " +
						"ISNULL(usrCurrent.ProjectRoundUserID,usr.ProjectRoundUserID), " +
						"usr.ProjectRoundID, " +
						"a.AnswerID";
				}
				else
				{
					SQL = "SELECT DISTINCT " +
						"-a.AnswerID AS PRUID, " +
						"q.UnitID, " +
						"-a.AnswerID, " +
						"a.StartDT, " +
						"a.EndDT, " +
						"usr.Extended, " +			// 5
						"usr.ExtendedTag, " +
						"usr.GroupID, " +
						"usr.Extra, " +
						"usr.ExternalID, " +
						"usr.SendCount, " +			// 10
						"usr.ReminderCount, " +
						"usr.NoSend, " +
						"usr.Terminated, " +
						"u.Terminated, " +
						"0, " +		// 15
						"usr.Email, " +
						"a.AnswerID " +
						querySelect.ToString().Split('¤')[q] +
						"FROM Answer a " +
						"INNER JOIN ProjectRoundUnit u ON a.ProjectRoundUnitID = u.ProjectRoundUnitID " +
						"INNER JOIN [Unit] q ON u.ID = q.ID " +
						"LEFT OUTER JOIN ProjectRoundUser usr ON a.ProjectRoundUserID = usr.ProjectRoundUserID " +
						queryJoin.ToString().Split('¤')[q] +
						"WHERE (a.ProjectRoundID IN (" + rPRID + ")" + (rComparePRID != 0 ? " OR usr.ProjectRoundID = " + rComparePRID : "") + ") " +
						(rAll != 0 ? "" : "AND a.EndDT IS NOT NULL ") +
						"ORDER BY " +
						"a.AnswerID";
				}
//				HttpContext.Current.Response.Write(SQL);
//				if(q < queries-1)
//					continue;
//				HttpContext.Current.Response.End();
				SqlDataReader rs = Db.sqlRecordSet(SQL);
				while(rs.Read())
				{
					if(userID != 0)
					{
						output.Append("¤");
					}

					if(userID == 0 || userID != rs.GetInt32(0))
					{
						userCaseCounter = 0;
						userID = rs.GetInt32(0);
					}
					userCaseCounter++;

					if(q == 0)
					{
						int round = 1;
						if(rComparePRID != 0 && rs.GetInt32(2).ToString() == rPRID)
						{
							round = 2;
						}

						caseCount++;

						string s = "";
						if(rs.GetInt32(15) != 0)
						{
							rs3 = Db.sqlRecordSet("SELECT u.ID FROM ProjectRoundUnitManager x INNER JOIN ProjectRoundUnit u ON x.ProjectRoundUnitID = u.ProjectRoundUnitID WHERE x.ProjectRoundUserID = " + rs.GetInt32(0));
							while(rs3.Read())
							{
								s += (s != "" ? "," : "") + rs3.GetString(0);
							}
							rs3.Close();
						}

						nextShouldBreak = false;
						if(rBase == 0 && sponsorID != 0)
						{
							rs3 = Db.sqlRecordSet("SELECT " +
								"s.UserIdent1, " +	// 0
								"s.UserIdent2, " +	// 1
								"s.UserIdent3, " +	// 2
								"s.UserIdent4, " +	// 3
								"s.UserIdent5, " +	// 4
								"s.UserIdent6, " +	// 5
								"s.UserIdent7, " +	// 6
								"s.UserIdent8, " +	// 7
								"s.UserIdent9, " +	// 8
								"s.UserIdent10, " +	// 9
								"s.UserCheck1, " +	// 10
								"s.UserCheck2, " +	// 11
								"s.UserCheck3, " +	// 12
								"s.UserNr, " +		// 13
								"u.UserIdent1, " +	// 14
								"u.UserIdent2, " +	// 15
								"u.UserIdent3, " +	// 16
								"u.UserIdent4, " +	// 17
								"u.UserIdent5, " +	// 18
								"u.UserIdent6, " +	// 19
								"u.UserIdent7, " +	// 20
								"u.UserIdent8, " +	// 21
								"u.UserIdent9, " +	// 22
								"u.UserIdent10, " +	// 23
								"u.UserCheck1, " +	// 24
								"u.UserCheck2, " +	// 25
								"u.UserCheck3, " +	// 26
								"u.UserNr, " +		// 27
								"u.UserID, " +		// 28
								"x.Note " +			// 29
								"FROM Sponsor s " +
								"LEFT OUTER JOIN UserProjectRoundUser x ON x.ProjectRoundUserID = " + rs.GetInt32(0) + " " +
								"LEFT OUTER JOIN [User] u ON x.UserID = u.UserID " +
								"WHERE s.SponsorID = " + sponsorID);
							if(rs3.Read())
							{
								output.Append((nextShouldBreak ? rowDelim : "") + (rs3.IsDBNull(28) ? 0 : rs3.GetInt32(28)).ToString().PadLeft(6,' ')); nextShouldBreak = false;
								if(!rs3.IsDBNull(13))
								{
									output.Append((nextShouldBreak ? rowDelim : "") + (rs3.IsDBNull(27) ? 0 : rs3.GetInt32(27)).ToString().PadLeft(6,' ')); nextShouldBreak = false;
								}
								if(IDS)
								{
									for(int i=0; i<=9; i++)
									{
										if(!rs3.IsDBNull(i))
										{
											nextShouldBreak = true;
											output.Append((nextShouldBreak ? rowDelim : "") + (rs3.IsDBNull(i+14) ? "" : rs3.GetString(i+14))); nextShouldBreak = true;
										}
									}
									for(int i=10; i<=12; i++)
									{
										if(!rs3.IsDBNull(i))
										{
											output.Append((nextShouldBreak ? rowDelim : "") + (rs3.IsDBNull(i+14) ? 0 : rs3.GetInt32(i+14)).ToString().PadLeft(6,' ')); nextShouldBreak = false;
										}
									}
								}
								nextShouldBreak = true;
								string note = (rs3.IsDBNull(29) ? "" : rs3.GetString(29)).Replace("\r\n"," ").Replace("\r","").Replace("\n","").Replace("\t","");
								if(note.Length > 250)
								{
									note = note.Substring(0,250);
								}
								output.Append((nextShouldBreak ? rowDelim : "") + note); nextShouldBreak = true;
							}
							rs3.Close();
						}

						output.Append("" +
							(nextShouldBreak ? rowDelim : "") + 
							//(rBase == 0 ? 
							userID.ToString().PadRight(8,' ') +
							//: "") + 
							rs.GetInt32(2).ToString().PadRight(8,' ') + 
							rs.GetInt32(1).ToString().PadRight(6,' ') + 
							//(rBase == 0 ? 
							round + 
							userCaseCounter.ToString().PadRight(3,' ') +
							//: "" )+ 
							(rs.IsDBNull(3) ? "".PadRight(17,' ') : rs.GetDateTime(3).ToString("dd-MMM-yyyy HH:mm")) + 
							(rs.IsDBNull(4) ? "".PadRight(17,' ') : rs.GetDateTime(4).ToString("dd-MMM-yyyy HH:mm")) + 
							(rs.IsDBNull(9) ? "" : rs.GetInt64(9).ToString()) + 
							//(rBase == 0 ?
							rowDelim +
							(rs.IsDBNull(5) ? 0 : rs.GetInt32(5)) + 
							(rs.IsDBNull(6) ? 0 : rs.GetInt32(6)) + 
							(rs.IsDBNull(7) ? "".PadRight(3,' ') : rs.GetInt32(7).ToString().PadRight(3,' ')) + 
							(rs.IsDBNull(10) ? "0".ToString().PadRight(2,' ') : rs.GetInt32(10).ToString().PadRight(2,' ')) + 
							(rs.IsDBNull(11) ? "0".ToString().PadRight(2,' ') : rs.GetInt32(11).ToString().PadRight(2,' ')) + 
							(rs.IsDBNull(12) ? 0 : rs.GetInt32(12)) + 
							(rs.IsDBNull(13) ? 0 : 1) + 
							(rs.IsDBNull(14) ? 0 : 1) +
							rowDelim +
							(rs.IsDBNull(8) ? "" : rs.GetString(8)) +
							rowDelim +
							s +
							rowDelim +
							(rs.IsDBNull(16) ? "" : rs.GetString(16))
							//: "")
							);
					}

					for(int i=q*queryDivide; i<Math.Min((q+1)*queryDivide,varCount); i++)
					{
						if(varBreak[i] == "1")
						{
							output.Append(rowDelim);
						}

						int pos = i+18-q*queryDivide;

						switch(Convert.ToInt32(varType[i]))
						{
							case 1:	
								output.Append((rs.IsDBNull(pos) ? "" : rs.GetInt32(pos).ToString()).PadRight(Convert.ToInt32(varPosition[i]),' ')); 
								break;
							case 2: 
								if(rToScreen != 0)
								{
									HttpContext.Current.Response.Write((rs.IsDBNull(pos) ? "" : rs.GetString(pos)) + "\t");
								}
								output.Append(trunc((rs.IsDBNull(pos) ? "" : rs.GetString(pos)),230)); 
								break;
							case 3:
								goto case 1;
							case 4: 
								output.Append((rs.IsDBNull(pos) ? "" : rs.GetDecimal(pos).ToString("N2").Replace(".","").Replace(",","")).PadRight(Convert.ToInt32(varPosition[i]),' ')); 
								break;
							case 9:	
								output.Append((rs.IsDBNull(pos) ? "" : rs.GetInt32(pos).ToString()).PadRight(Convert.ToInt32(varPosition[i]),' ')); 
								if(Convert.ToInt32(varAttr[i]) > 2)
								{
									if(!rs.IsDBNull(pos))
									{
										int cx = 0;
										for(int ii=0; ii<Convert.ToInt32(varAttr[i]); ii++)
										{
											if(rs.GetInt32(pos) >= 100/Convert.ToInt32(varAttr[i])*ii)
											{
												cx = ii;
											}
										}
										output.Append(cx.ToString().PadRight(Convert.ToInt32(varPosition[i]),' '));
									}
									else
									{
										output.Append("".PadRight(Convert.ToInt32(varPosition[i]),' '));
									}
								}
								break;
						}
					}
					if(rToScreen != 0)
					{
						HttpContext.Current.Response.Write("\r\n");
					}
				}
				rs.Close();
				#endregion

				queryResult.Add(output.ToString().Split('¤'));
			}

			StringBuilder mergedOutput = new StringBuilder();
			for(int p=0; p<caseCount; p++)
			{
				for(int m=0; m<queries; m++)
				{
					if(queryResult.Count > m && ((string[])queryResult[m]).Length > p)
					{
						mergedOutput.Append(((string[])queryResult[m])[p]);
					}
				}
				mergedOutput.Append(rowDelim);
			}
			
			if(rToScreen == 0)
			{
				f.Write("DATA LIST FIXED RECORDS=" + recordCount + def + "." + rowDelim + "BEGIN DATA" + rowDelim + mergedOutput.ToString() + "END DATA." + rowDelim + syn + rowDelim);
				f.Close();
				fs.Close();

				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				//HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
				HttpContext.Current.Response.Charset = "UTF-8";
				HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
				HttpContext.Current.Response.ContentType = "text/x-spss-syntax";
				HttpContext.Current.Response.AddHeader("content-disposition","attachment; filename=" + DateTime.Now.Ticks + ".sps");
				HttpContext.Current.Response.WriteFile(fname);
			}
			HttpContext.Current.Response.Flush();
			HttpContext.Current.Response.End();

			#endregion
		}
		
		public static string printNav()
		{
			string ret = "";
			ret += "<table width=\"100%\" height=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"text\">";
			ret += "<tr><td width=\"15\"><img src=\"../img/null.gif\" width=\"2\" height=\"15\"></td><td width=\"170\" colspan=\"2\">&nbsp;</td></tr>";
			ret += "<tr><td background=\"../img/nav/redBgr.gif\"><img src=\"../img/null.gif\" width=\"1\" height=\"6\"></td><td background=\"../img/nav/redBgr.gif\"><img src=\"../img/null.gif\" width=\"115\" height=\"1\"></td><td class=\"navTopR\"><img src=\"../img/null.gif\" width=\"30\" height=\"1\"></td></tr>";
			
			OdbcDataReader rs = recordSet("SELECT NavText, NavURL FROM Nav ORDER BY SortOrder");
			while(rs.Read())
			{
				ret += "<tr><td colspan=\"3\" bgcolor=\"#DBDBDB\"><img src=\"../img/null.gif\" width=\"1\" height=\"1\"></td></tr>";
				ret += "<tr><td width=\"15\" class=\"navBg\">&nbsp;</td><td width=\"165\" colspan=\"2\" bgcolor=\"#E6E6E6\" class=\"navBg\"><img src=\"../img/arrow_topNavN.gif\" width=\"5\" height=\"7\">&nbsp;<a href=\"" + rs.GetString(1) + "\">" + rs.GetString(0) + "</a></td></tr>";
			}
			rs.Close();

			ret += "<tr><td width=\"15\" height=\"100%\" bgcolor=\"#E6E6E6\">&nbsp;</td><td width=\"165\" height=\"100%\" colspan=\"2\" bgcolor=\"#E6E6E6\">&nbsp;</td></tr>";
			ret += "</table>";

			return ret;
		}
		
		public static void execute(string sqlString) 
		{
			OdbcConnection dataConnection = new OdbcConnection(ConfigurationSettings.AppSettings["SqlConnection"]);
			dataConnection.Open();
			OdbcCommand dataCommand = new OdbcCommand(sqlString.Replace("\\","\\\\"), dataConnection);
			dataCommand.CommandTimeout = 900;
			dataCommand.ExecuteNonQuery();
			dataConnection.Close();
			dataConnection.Dispose();
		}
		public static void sqlExecute(string sqlString) 
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationSettings.AppSettings["SqlClientConnection"]);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString.Replace("\\","\\\\"), dataConnection);
			dataCommand.CommandTimeout = 900;
			dataCommand.ExecuteNonQuery();
			dataConnection.Close();
			dataConnection.Dispose();
		}
		
		public static OdbcDataReader recordSet(string sqlString)
		{
			OdbcConnection dataConnection = new OdbcConnection(ConfigurationSettings.AppSettings["SqlConnection"]);
			dataConnection.Open();
			OdbcCommand dataCommand = new OdbcCommand(sqlString.Replace("\\","\\\\"), dataConnection);
			dataCommand.CommandTimeout = 900;
			OdbcDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
			return dataReader;
		}
		public static SqlDataReader sqlRecordSet(string sqlString)
		{
			SqlConnection dataConnection = new SqlConnection(ConfigurationSettings.AppSettings["SqlClientConnection"]);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString.Replace("\\","\\\\"), dataConnection);
			dataCommand.CommandTimeout = 900;
			SqlDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
			return dataReader;
		}

		public static int getInt32(string sqlString)
		{
			int returnValue = 0;
			OdbcConnection dataConnection = new OdbcConnection(ConfigurationSettings.AppSettings["SqlConnection"]);
			dataConnection.Open();
			OdbcCommand dataCommand = new OdbcCommand(sqlString.Replace("\\","\\\\"), dataConnection);
			dataCommand.CommandTimeout = 900;
			OdbcDataReader dataReader = dataCommand.ExecuteReader();
			if(dataReader.Read())
				if(!dataReader.IsDBNull(0))
					returnValue = Convert.ToInt32(dataReader.GetValue(0));
			dataReader.Close();
			dataConnection.Close();
			dataConnection.Dispose();
			return returnValue;
		}
		public static int sqlGetInt32(string sqlString)
		{
			int returnValue = 0;
			SqlConnection dataConnection = new SqlConnection(ConfigurationSettings.AppSettings["SqlClientConnection"]);
			dataConnection.Open();
			SqlCommand dataCommand = new SqlCommand(sqlString.Replace("\\","\\\\"), dataConnection);
			dataCommand.CommandTimeout = 900;
			SqlDataReader dataReader = dataCommand.ExecuteReader();
			if(dataReader.Read())
				if(!dataReader.IsDBNull(0))
					returnValue = Convert.ToInt32(dataReader.GetValue(0));
			dataReader.Close();
			dataConnection.Close();
			dataConnection.Dispose();
			return returnValue;
		}

		public static void execute(string sqlString, string conn) 
		{
			OdbcConnection dataConnection = new OdbcConnection(conn);
			dataConnection.Open();
			OdbcCommand dataCommand = new OdbcCommand(sqlString.Replace("\\","\\\\"), dataConnection);
			dataCommand.CommandTimeout = 900;
			dataCommand.ExecuteNonQuery();
			dataConnection.Close();
			dataConnection.Dispose();
		}
		
		public static OdbcDataReader recordSet(string sqlString, string conn)
		{
			OdbcConnection dataConnection = new OdbcConnection(conn);
			dataConnection.Open();
			OdbcCommand dataCommand = new OdbcCommand(sqlString.Replace("\\","\\\\"), dataConnection);
			dataCommand.CommandTimeout = 900;
			OdbcDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
			return dataReader;
		}

		public static string HexHashMD5(string str)
		{
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] hashByteArray = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
			string hash = "";
			for(int i = 0; i < hashByteArray.Length; i++)
				hash += Convert.ToInt32(hashByteArray[i]).ToString("X").ToLower();
			return hash;
		}
		public static string HashMD5(string str)
		{
			return HexHashMD5("EF0RM" + str + "eform");
		}
	}
}
