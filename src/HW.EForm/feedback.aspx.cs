using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
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
	/// Summary description for feedback.
	/// </summary>
	public class feedback : System.Web.UI.Page
	{
		protected Label FeedbackText;
		protected int projectID = 0;
		protected string surveyName = "";
		protected Label Lang;
		
		string aids = "", userKey = "", units = "", units2 = "", key1 = "", key2 = "", r = "", ud = "", name1 = "", name2 = "", t = "", rr = "", r1 = "", r2 = "", rnds1 = "", rnds2 = "", rnds = "", depts1 = "", depts2 = "";
		int surveyID = 0, projectRoundID = 0, LID = 1, rac = 10;
		bool unitCategorySet = false;

		OdbcDataReader rs;

		/// <summary>
		/// exhaustion
		/// </summary>
		/// <param name="surveyID"></param>
		/// <param name="LID"></param>
		/// <param name="FBR"></param>
		/// <param name="cx"></param>
		/// <param name="fn"></param>
		/// <param name="showN"></param>
		/// <param name="aids"></param>
		/// <param name="RAC"></param>
		/// <param name="rnds"></param>
		/// <param name="rnds1"></param>
		/// <param name="rnds2"></param>
		/// <param name="depts1"></param>
		/// <param name="depts2"></param>
		/// <param name="r1"></param>
		/// <param name="r2"></param>
		/// <param name="rr"></param>
		/// <param name="unitDesc"></param>
		/// <param name="showTotal"></param>
		/// <param name="BGCOLOR"></param>
		/// <param name="AID1"></param>
		/// <param name="AID1txt"></param>
		/// <param name="AID2"></param>
		/// <param name="AID2txt"></param>
		/// <param name="PRDESC"></param>
		/// <param name="projectRoundID"></param>
		/// <param name="units"></param>
		/// <param name="units2"></param>
		/// <param name="percent"></param>
		/// <param name="rnd"></param>
		/// <returns></returns>
		public static string hardcodedIdx1(int groupID, int surveyID, int LID, int FBR, ref int cx, string fn, bool showN, string aids, int RAC, string rnds, string rnds1, string rnds2, string depts1, string depts2, string r1, string r2, int rr, string unitDesc, bool showTotal, string BGCOLOR, int AID1, string AID1txt, int AID2, string AID2txt, string PRDESC, int projectRoundID, string units, string units2, bool percent, int rnd, int compare, string extraQS)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			OdbcDataReader rs2 = Db.recordSet("SELECT " +
				"s.SurveyID " +
				"FROM Survey s " +
				"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 380 " +
				"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 381 " +
				"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 382 " +
				"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 383 " +
				"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 384 " +
				"INNER JOIN SurveyQuestion sq6 ON sq6.SurveyID = s.SurveyID AND sq6.QuestionID = 459 " +
				"INNER JOIN SurveyQuestion sq7 ON sq7.SurveyID = s.SurveyID AND sq7.QuestionID = 460 " +
				"INNER JOIN SurveyQuestion sq8 ON sq8.SurveyID = s.SurveyID AND sq8.QuestionID = 461 " +
				"INNER JOIN SurveyQuestion sq9 ON sq9.SurveyID = s.SurveyID AND sq9.QuestionID = 462 " +
				"INNER JOIN SurveyQuestion sq10 ON sq10.SurveyID = s.SurveyID AND sq10.QuestionID = 463 " +
				"WHERE s.SurveyID = " + surveyID);
			if(rs2.Read())
			{
				sb.Append("<br class=\"noprint\"/><br/>");
				sb.Append("<div style=\"page-break-inside:avoid;\">");
				string area = "";
				switch(LID)
				{
					case 1: area = "Medelvärde utmattning"; break;
					case 2: area = "Mean value exhaustion"; break;
				}
				sb.Append("<div class=\"eform_area\"><p>" + area + "</p></div>");
				string url = "http" + (HttpContext.Current.Request.IsSecureConnection ? "s" : "") + "://" + HttpContext.Current.Request.Url.Host + "/feedbackImage.aspx?fn=" + fn + (groupID != 0 ? "&GroupID=" + groupID : "") + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RR=" + rr + "&" : "") + (units2 != "" ? "RRU=" + units2 + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "Rnd=" + rnd + "&R=" + projectRoundID + "&T=" + (compare == 2 ? 401 : 1) + "&U=" + units + (!percent ? "&Percent=0" : "") + extraQS;
				sb.Append("<div class=\"eform_ques\">");
				sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
				int width = 550, height = 320;
				sb.Append("<TR><TD><img width=\"" + width + "\" height=\"" + height + "\" src=\"" + url + "\"></TD></TR>");
				string desc = "";
				switch(LID)
				{
					case 1: desc = "Värden över 1.6 innebär tecken på hög stressnivå. Långvarig stress utan tillräcklig återhämtning kan leda till utbrändhet eller utmattningssyndrom. Det finns bra belägg för att en utbränningsprocess kan stoppas upp. Man kan ha hjälp av olika metoder, t ex kollegiala samtalsgrupper."; break;
					case 2: desc = "Values above 1.6 imply signs of high stress level."; break;
				}
				sb.Append("<TR><TD>" + desc + "</TD></TR>");
				sb.Append("</TABLE>");
				sb.Append("</div>");
				sb.Append("</div>");
				cx++;
				Db.sqlExecute("INSERT INTO FeedbackRunRow (FeedbackRunID,URL,Area,Header,Description,Width,Height) VALUES (" + FBR + ",'" + url.Replace("'","''") + "','" + area.Replace("'","''") + "',NULL," + (desc != "" ? "'" + desc.Replace("'","''") + "'" : "NULL") + "," + width + "," + height + ")");
			}
			rs2.Close();

			return sb.ToString();
		}

		/// <summary>
		/// burnout
		/// </summary>
		/// <param name="surveyID"></param>
		/// <param name="LID"></param>
		/// <param name="FBR"></param>
		/// <param name="cx"></param>
		/// <param name="fn"></param>
		/// <param name="showN"></param>
		/// <param name="aids"></param>
		/// <param name="RAC"></param>
		/// <param name="rnds"></param>
		/// <param name="rnds1"></param>
		/// <param name="rnds2"></param>
		/// <param name="depts1"></param>
		/// <param name="depts2"></param>
		/// <param name="r1"></param>
		/// <param name="r2"></param>
		/// <param name="rr"></param>
		/// <param name="unitDesc"></param>
		/// <param name="showTotal"></param>
		/// <param name="BGCOLOR"></param>
		/// <param name="AID1"></param>
		/// <param name="AID1txt"></param>
		/// <param name="AID2"></param>
		/// <param name="AID2txt"></param>
		/// <param name="PRDESC"></param>
		/// <param name="projectRoundID"></param>
		/// <param name="units"></param>
		/// <param name="units2"></param>
		/// <param name="percent"></param>
		/// <param name="rnd"></param>
		/// <returns></returns>
		public static string hardcodedIdx2(int groupID, int surveyID, int LID, int FBR, ref int cx, string fn, bool showN, string aids, int RAC, string rnds, string rnds1, string rnds2, string depts1, string depts2, string r1, string r2, int rr, string unitDesc, bool showTotal, string BGCOLOR, int AID1, string AID1txt, int AID2, string AID2txt, string PRDESC, int projectRoundID, string units, string units2, bool percent, int rnd, int compare, string extraQS)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			OdbcDataReader rs2 = Db.recordSet("SELECT " +
				"s.SurveyID " +
				"FROM Survey s " +
				"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 380 " +
				"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 381 " +
				"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 382 " +
				"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 383 " +
				"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 384 " +
				"INNER JOIN SurveyQuestion sq6 ON sq6.SurveyID = s.SurveyID AND sq6.QuestionID = 459 " +
				"INNER JOIN SurveyQuestion sq7 ON sq7.SurveyID = s.SurveyID AND sq7.QuestionID = 460 " +
				"INNER JOIN SurveyQuestion sq8 ON sq8.SurveyID = s.SurveyID AND sq8.QuestionID = 461 " +
				"INNER JOIN SurveyQuestion sq9 ON sq9.SurveyID = s.SurveyID AND sq9.QuestionID = 462 " +
				"INNER JOIN SurveyQuestion sq10 ON sq10.SurveyID = s.SurveyID AND sq10.QuestionID = 463 " +
				"WHERE s.SurveyID = " + surveyID);
			if(rs2.Read())
			{
				sb.Append("<br class=\"noprint\"/><br/>");
				sb.Append("<div style=\"page-break-inside:avoid;\">");
				string area = "";
				switch(LID)
				{
					case 1: area = "Andel med symptom på utbrändhet"; break;
					case 2: area = "Share with symptoms of burnout"; break;
				}
				sb.Append("<div class=\"eform_area\"><p>" + area + "</p></div>");
				string url = "http" + (HttpContext.Current.Request.IsSecureConnection ? "s" : "") + "://" + HttpContext.Current.Request.Url.Host + "/feedbackImage.aspx?fn=" + fn + (groupID != 0 ? "&GroupID=" + groupID : "") + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RR=" + rr + "&" : "") + (units2 != "" ? "RRU=" + units2 + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "Rnd=" + rnd + "&R=" + projectRoundID + "&T=" + (compare == 2 ? 402 : 2) + "&U=" + units + (!percent ? "&Percent=0" : "") + extraQS;
				sb.Append("<div class=\"eform_ques\">");
				sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
				int width = 550, height = 320;
				sb.Append("<TR><TD><img width=\"" + width + "\" height=\"" + height + "\" src=\"" + url + "\"></TD></TR>");
				string desc = "";
				switch(LID)
				{
					case 1: desc = "Kombinationen lågt engagemang (&lt;1.4) och hög grad av utmattning (&gt;1.6) innebär hög risk för utbrändhet."; break;
				}
				sb.Append("<TR><TD>" + desc + "</TD></TR>");
				sb.Append("</TABLE>");
				sb.Append("</div>");
				sb.Append("</div>");
				cx++;
				Db.sqlExecute("INSERT INTO FeedbackRunRow (FeedbackRunID,URL,Area,Header,Description,Width,Height) VALUES (" + FBR + ",'" + url.Replace("'","''") + "','" + area.Replace("'","''") + "',NULL," + (desc != "" ? "'" + desc.Replace("'","''") + "'" : "NULL") + "," + width + "," + height + ")");
			}
			rs2.Close();

			return sb.ToString();
		}
		
		/// <summary>
		/// engagement
		/// </summary>
		/// <param name="surveyID"></param>
		/// <param name="LID"></param>
		/// <param name="FBR"></param>
		/// <param name="cx"></param>
		/// <param name="fn"></param>
		/// <param name="showN"></param>
		/// <param name="aids"></param>
		/// <param name="RAC"></param>
		/// <param name="rnds"></param>
		/// <param name="rnds1"></param>
		/// <param name="rnds2"></param>
		/// <param name="depts1"></param>
		/// <param name="depts2"></param>
		/// <param name="r1"></param>
		/// <param name="r2"></param>
		/// <param name="rr"></param>
		/// <param name="unitDesc"></param>
		/// <param name="showTotal"></param>
		/// <param name="BGCOLOR"></param>
		/// <param name="AID1"></param>
		/// <param name="AID1txt"></param>
		/// <param name="AID2"></param>
		/// <param name="AID2txt"></param>
		/// <param name="PRDESC"></param>
		/// <param name="projectRoundID"></param>
		/// <param name="units"></param>
		/// <param name="units2"></param>
		/// <param name="percent"></param>
		/// <param name="rnd"></param>
		/// <returns></returns>
		public static string hardcodedIdx3(int groupID, int surveyID, int LID, int FBR, ref int cx, string fn, bool showN, string aids, int RAC, string rnds, string rnds1, string rnds2, string depts1, string depts2, string r1, string r2, int rr, string unitDesc, bool showTotal, string BGCOLOR, int AID1, string AID1txt, int AID2, string AID2txt, string PRDESC, int projectRoundID, string units, string units2, bool percent, int rnd, int compare, string extraQS)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			OdbcDataReader rs2 = Db.recordSet("SELECT " +
				"s.SurveyID " +
				"FROM Survey s " +
				"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 459 " +
				"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 460 " +
				"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 461 " +
				"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 462 " +
				"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 463 " +
				"WHERE s.SurveyID = " + surveyID);
			if(rs2.Read())
			{
				sb.Append("<br class=\"noprint\"/><br/>");
				sb.Append("<div style=\"page-break-inside:avoid;\">");
				string area = "";
				switch(LID)
				{
					case 1: area = "Medelvärde engagemang"; break;
					case 2: area = "Mean value engagement"; break;
				}
				sb.Append("<div class=\"eform_area\"><p>" + area + "</p></div>");
				string url = "http" + (HttpContext.Current.Request.IsSecureConnection ? "s" : "") + "://" + HttpContext.Current.Request.Url.Host + "/feedbackImage.aspx?fn=" + fn + (groupID != 0 ? "&GroupID=" + groupID : "") + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RR=" + rr + "&" : "") + (units2 != "" ? "RRU=" + units2 + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "Rnd=" + rnd + "&R=" + projectRoundID + "&T=" + (compare == 2 ? 403 : 3) + "&U=" + units + (!percent ? "&Percent=0" : "") + extraQS;
				sb.Append("<div class=\"eform_ques\">");
				sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
				int width = 550, height = 320;
				sb.Append("<TR><TD><img width=\"" + width + "\" height=\"" + height + "\" src=\"" + url + "\"></TD></TR>");
				string desc = "";
				switch(LID)
				{
					case 1: desc = "Höga värden är ett uttryck för att man trivs med sitt arbete, vilket i sin tur anses ha ett samband med kvalitet och produktivitet."; break;
				}
				sb.Append("<TR><TD>" + desc + "</TD></TR>");
				sb.Append("</TABLE>");
				sb.Append("</div>");
				sb.Append("</div>");
				cx++;
				Db.sqlExecute("INSERT INTO FeedbackRunRow (FeedbackRunID,URL,Area,Header,Description,Width,Height) VALUES (" + FBR + ",'" + url.Replace("'","''") + "','" + area.Replace("'","''") + "',NULL," + (desc != "" ? "'" + desc.Replace("'","''") + "'" : "NULL") + "," + width + "," + height + ")");
			}
			rs2.Close();

			return sb.ToString();
		}
		
		/// <summary>
		/// depression
		/// </summary>
		/// <param name="surveyID"></param>
		/// <param name="LID"></param>
		/// <param name="FBR"></param>
		/// <param name="cx"></param>
		/// <param name="fn"></param>
		/// <param name="showN"></param>
		/// <param name="aids"></param>
		/// <param name="RAC"></param>
		/// <param name="rnds"></param>
		/// <param name="rnds1"></param>
		/// <param name="rnds2"></param>
		/// <param name="depts1"></param>
		/// <param name="depts2"></param>
		/// <param name="r1"></param>
		/// <param name="r2"></param>
		/// <param name="rr"></param>
		/// <param name="unitDesc"></param>
		/// <param name="showTotal"></param>
		/// <param name="BGCOLOR"></param>
		/// <param name="AID1"></param>
		/// <param name="AID1txt"></param>
		/// <param name="AID2"></param>
		/// <param name="AID2txt"></param>
		/// <param name="PRDESC"></param>
		/// <param name="projectRoundID"></param>
		/// <param name="units"></param>
		/// <param name="units2"></param>
		/// <param name="percent"></param>
		/// <param name="rnd"></param>
		/// <returns></returns>
		public static string hardcodedIdx5(int groupID, int surveyID, int LID, int FBR, ref int cx, string fn, bool showN, string aids, int RAC, string rnds, string rnds1, string rnds2, string depts1, string depts2, string r1, string r2, int rr, string unitDesc, bool showTotal, string BGCOLOR, int AID1, string AID1txt, int AID2, string AID2txt, string PRDESC, int projectRoundID, string units, string units2, bool percent, int rnd, int compare, string extraQS)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			OdbcDataReader rs2 = Db.recordSet("SELECT " +
				"s.SurveyID " +
				"FROM Survey s " +
				"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 387 " +
				"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 388 " +
				"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 389 " +
				"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 390 " +
				"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 391 " +
				"INNER JOIN SurveyQuestion sq6 ON sq6.SurveyID = s.SurveyID AND sq6.QuestionID = 392 " +
				"INNER JOIN SurveyQuestion sq7 ON sq7.SurveyID = s.SurveyID AND sq7.QuestionID = 393 " +
				"WHERE s.SurveyID = " + surveyID);
			if(rs2.Read())
			{
				sb.Append("<br class=\"noprint\"/><br/>");
				sb.Append("<div style=\"page-break-inside:avoid;\">");
				string area = "";
				switch(LID)
				{
					case 1: area = "Andel med tecken på nedstämdhet"; break;
					case 2: area = "Share with signs of depression"; break;
				}
				sb.Append("<div class=\"eform_area\"><p>" + area + "</p></div>");
				string url = "http" + (HttpContext.Current.Request.IsSecureConnection ? "s" : "") + "://" + HttpContext.Current.Request.Url.Host + "/feedbackImage.aspx?fn=" + fn + (groupID != 0 ? "&GroupID=" + groupID : "") + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RR=" + rr + "&" : "") + (units2 != "" ? "RRU=" + units2 + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "Rnd=" + rnd + "&R=" + projectRoundID + "&T=" + (compare == 2 ? 405 : 5) + "&U=" + units + (!percent ? "&Percent=0" : "") + extraQS;
				sb.Append("<div class=\"eform_ques\">");
				sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
				int width = 550, height = 320;
				sb.Append("<TR><TD><img width=\"" + width + "\" height=\"" + height + "\" src=\"" + url + "\"></TD></TR>");
				string desc = "";
				sb.Append("</TABLE>");
				sb.Append("</div>");
				sb.Append("</div>");
				cx++;
				Db.sqlExecute("INSERT INTO FeedbackRunRow (FeedbackRunID,URL,Area,Header,Description,Width,Height) VALUES (" + FBR + ",'" + url.Replace("'","''") + "','" + area.Replace("'","''") + "',NULL," + (desc != "" ? "'" + desc.Replace("'","''") + "'" : "NULL") + "," + width + "," + height + ")");
			}
			rs2.Close();

			return sb.ToString();
		}
		
		/// <summary>
		/// long-term sick-leave
		/// </summary>
		/// <param name="surveyID"></param>
		/// <param name="LID"></param>
		/// <param name="FBR"></param>
		/// <param name="cx"></param>
		/// <param name="fn"></param>
		/// <param name="showN"></param>
		/// <param name="aids"></param>
		/// <param name="RAC"></param>
		/// <param name="rnds"></param>
		/// <param name="rnds1"></param>
		/// <param name="rnds2"></param>
		/// <param name="depts1"></param>
		/// <param name="depts2"></param>
		/// <param name="r1"></param>
		/// <param name="r2"></param>
		/// <param name="rr"></param>
		/// <param name="unitDesc"></param>
		/// <param name="showTotal"></param>
		/// <param name="BGCOLOR"></param>
		/// <param name="AID1"></param>
		/// <param name="AID1txt"></param>
		/// <param name="AID2"></param>
		/// <param name="AID2txt"></param>
		/// <param name="PRDESC"></param>
		/// <param name="projectRoundID"></param>
		/// <param name="units"></param>
		/// <param name="units2"></param>
		/// <param name="percent"></param>
		/// <param name="rnd"></param>
		/// <returns></returns>
		public static string hardcodedIdx6(int groupID, int surveyID, int LID, int FBR, ref int cx, string fn, bool showN, string aids, int RAC, string rnds, string rnds1, string rnds2, string depts1, string depts2, string r1, string r2, int rr, string unitDesc, bool showTotal, string BGCOLOR, int AID1, string AID1txt, int AID2, string AID2txt, string PRDESC, int projectRoundID, string units, string units2, bool percent, int rnd, int compare, string extraQS)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			OdbcDataReader rs2 = Db.recordSet("SELECT " +
				"s.SurveyID " +
				"FROM Survey s " +
				"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 380 " +
				"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 381 " +
				"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 382 " +
				"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 383 " +
				"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 384 " +
				"INNER JOIN SurveyQuestion sq6 ON sq6.SurveyID = s.SurveyID AND sq6.QuestionID = 401 " +
				"INNER JOIN SurveyQuestion sq7 ON sq7.SurveyID = s.SurveyID AND sq7.QuestionID = 402 " +
				"INNER JOIN SurveyQuestion sq8 ON sq8.SurveyID = s.SurveyID AND sq8.QuestionID = 403 " +
				"INNER JOIN SurveyQuestion sq9 ON sq9.SurveyID = s.SurveyID AND sq9.QuestionID = 404 " +
				"WHERE s.SurveyID = " + surveyID);
			if(rs2.Read())
			{
				sb.Append("<br class=\"noprint\"/><br/>");
				sb.Append("<div style=\"page-break-inside:avoid;\">");
				string area = "";
				switch(LID)
				{
					case 1: area = "Andel med ökad risk för långtidssjukfrånvaro"; break;
					case 2: area = "Share with increased risk of long-term sick-leave"; break;
				}
				sb.Append("<div class=\"eform_area\"><p>" + area + "</p></div>");
				string url = "http" + (HttpContext.Current.Request.IsSecureConnection ? "s" : "") + "://" + HttpContext.Current.Request.Url.Host + "/feedbackImage.aspx?fn=" + fn + (groupID != 0 ? "&GroupID=" + groupID : "") + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RR=" + rr + "&" : "") + (units2 != "" ? "RRU=" + units2 + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "Rnd=" + rnd + "&R=" + projectRoundID + "&T=" + (compare == 2 ? 406 : 6) + "&U=" + units + (!percent ? "&Percent=0" : "") + extraQS;
				sb.Append("<div class=\"eform_ques\">");
				sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
				int width = 550, height = 320;
				sb.Append("<TR><TD><img width=\"" + width + "\" height=\"" + height + "\" src=\"" + url + "\"></TD></TR>");
				string desc = "";
				switch(LID)
				{
					case 1: desc = "Andel med ett utmattningsvärde över 1.6 i kombination med ett indexvärde på prestationsbaserad självkänsla över 3.25."; break;
				}
				sb.Append("<TR><TD>" + desc + "</TD></TR>");
				sb.Append("</TABLE>");
				sb.Append("</div>");
				sb.Append("</div>");
				cx++;
				Db.sqlExecute("INSERT INTO FeedbackRunRow (FeedbackRunID,URL,Area,Header,Description,Width,Height) VALUES (" + FBR + ",'" + url.Replace("'","''") + "','" + area.Replace("'","''") + "',NULL," + (desc != "" ? "'" + desc.Replace("'","''") + "'" : "NULL") + "," + width + "," + height + ")");
			}
			rs2.Close();

			return sb.ToString();
		}
		public static string renderFeedback(int LID, int surveyID, string PRDESC, string PRDESC2, int projectRoundID, string units, bool unitCategorySet, int AID1, int AID2, string AID1txt, string AID2txt, string BGCOLOR)
		{
			return renderFeedback(LID,surveyID, PRDESC, PRDESC2, projectRoundID, units, unitCategorySet, AID1, AID2, AID1txt, AID2txt, BGCOLOR, true, "");
		}
		public static string renderFeedback(int LID, int surveyID, string PRDESC, string PRDESC2, int projectRoundID, string units, bool unitCategorySet, int AID1, int AID2, string AID1txt, string AID2txt, string BGCOLOR, bool showTotal, string unitDesc)
		{
			return renderFeedback(LID,surveyID, PRDESC, PRDESC2, projectRoundID, units, unitCategorySet, AID1, AID2, AID1txt, AID2txt, BGCOLOR, showTotal, unitDesc, "", "", 0, "");
		}
		public static string renderFeedback(int LID, int surveyID, string PRDESC, string PRDESC2, int projectRoundID, string units, bool unitCategorySet, int AID1, int AID2, string AID1txt, string AID2txt, string BGCOLOR, bool showTotal, string unitDesc, string r1, string r2, int rr, string units2)
		{
			return renderFeedback(LID,surveyID, PRDESC, PRDESC2, projectRoundID, units, unitCategorySet, AID1, AID2, AID1txt, AID2txt, BGCOLOR, showTotal, unitDesc, r1, r2, rr, units2, "", "", "");
		}
		public static string renderFeedback(int LID, int surveyID, string PRDESC, string PRDESC2, int projectRoundID, string units, bool unitCategorySet, int AID1, int AID2, string AID1txt, string AID2txt, string BGCOLOR, bool showTotal, string unitDesc, string r1, string r2, int rr, string units2, string rnds1, string rnds2, string rnds)
		{
			return renderFeedback(LID,surveyID, PRDESC, PRDESC2, projectRoundID, units, unitCategorySet, AID1, AID2, AID1txt, AID2txt, BGCOLOR, showTotal, unitDesc, r1, r2, rr, units2, rnds1, rnds2, rnds, 10);
		}
		public static string renderFeedback(int LID, int surveyID, string PRDESC, string PRDESC2, int projectRoundID, string units, bool unitCategorySet, int AID1, int AID2, string AID1txt, string AID2txt, string BGCOLOR, bool showTotal, string unitDesc, string r1, string r2, int rr, string units2, string rnds1, string rnds2, string rnds, int RAC)
		{
			return renderFeedback(LID,surveyID, PRDESC, PRDESC2, projectRoundID, units, unitCategorySet, AID1, AID2, AID1txt, AID2txt, BGCOLOR, showTotal, unitDesc, r1, r2, rr, units2, rnds1, rnds2, rnds, RAC, "", "");
		}
		public static string renderFeedback(int LID, int surveyID, string PRDESC, string PRDESC2, int projectRoundID, string units, bool unitCategorySet, int AID1, int AID2, string AID1txt, string AID2txt, string BGCOLOR, bool showTotal, string unitDesc, string r1, string r2, int rr, string units2, string rnds1, string rnds2, string rnds, int RAC, string depts1, string depts2)
		{
			return renderFeedback(LID,surveyID, PRDESC, PRDESC2, projectRoundID, units, unitCategorySet, AID1, AID2, AID1txt, AID2txt, BGCOLOR, showTotal, unitDesc, r1, r2, rr, units2, rnds1, rnds2, rnds, RAC, depts1, depts2, "", false, 0, true, 0, "");
		}
		public static string renderFeedback(int LID, int surveyID, string PRDESC, string PRDESC2, int projectRoundID, string units, bool unitCategorySet, int AID1, int AID2, string AID1txt, string AID2txt, string BGCOLOR, bool showTotal, string unitDesc, string r1, string r2, int rr, string units2, string rnds1, string rnds2, string rnds, int RAC, string depts1, string depts2, string extraQS)
		{
			return renderFeedback(LID,surveyID, PRDESC, PRDESC2, projectRoundID, units, unitCategorySet, AID1, AID2, AID1txt, AID2txt, BGCOLOR, showTotal, unitDesc, r1, r2, rr, units2, rnds1, rnds2, rnds, RAC, depts1, depts2, "", false, 0, true, 0, extraQS);
		}
		public static string renderFeedback(int LID, int surveyID, string PRDESC, string PRDESC2, int projectRoundID, string units, 
			bool unitCategorySet, int AID1, int AID2, string AID1txt, string AID2txt, string BGCOLOR, bool showTotal, string unitDesc, 
			string r1, string r2, int rr, string units2, string rnds1, string rnds2, string rnds, int RAC, string depts1, string depts2, 
			string aids, bool showN, int feedbackID, bool percent, int groupID, string extraQS)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			Random rnd = new Random(unchecked((int)DateTime.Now.Ticks));
			int FBR = 0;
			string fn = "";
			SqlDataReader r = Db.sqlRecordSet("SELECT TOP 1 FeedbackRunID FROM FeedbackRun ORDER BY FeedbackRunID DESC");
			if(r.Read())
			{
				FBR = r.GetInt32(0);
				fn = FBR.ToString();
			}
			else
			{
				fn = rnd.Next().ToString().Replace(".","").Replace(",","");
			}
			r.Close();
			if(System.IO.File.Exists(HttpContext.Current.Server.MapPath("report") + "\\" + fn + ".txt"))
			{
				fn = "";
			}

			string doubleQ = "";
			switch(LID)
			{
				case 1: doubleQ = "Hur nöjd eller missnöjd är du med det?"; break;
				case 2: doubleQ = "How satisfied or dissatisfied are you with it?"; break;
			}

			int cx = 0;

			OdbcDataReader rs;
//			
//			if(feedbackID == 0)
//			{
//				rs = Db.recordSet("SELECT FeedbackID FROM ProjectRound WHERE ProjectRoundID = " + projectRoundID);
//				if(rs.Read())
//				{
//					if(!rs.IsDBNull(0))
//					{
//						feedbackID = rs.GetInt32(0);
//					}
//				}
//				rs.Close();
//			}
			bool show3x3 = true;

			string path = LID + "_" + surveyID + "_" + projectRoundID + "_" + DateTime.UtcNow.Ticks;
			
			sb.Append("<!--FID:" + feedbackID + "-->");
			
			if(feedbackID != 0)
			{
				#region Feedback ID
				//bool seen1 = false, seen2 = false, seen3 = false, seen5 = false, seen6 = false;
				bool compare = false, noHardcodedIdxs = false; show3x3 = false;
				ArrayList seen = new ArrayList();
//				if(RAC > 7)
//					RAC = 7;

				rs = Db.recordSet("SELECT " +
					"fq.QuestionID, " +			// 0
					"qo.OptionID, " +
					"ISNULL(NULLIF(ql.ReportQuestion,''),ISNULL(sqlang.Question,ISNULL(ql.Question,i.Internal))), " +
					"ISNULL(ql.QuestionArea,'Index'), " +
					"o.OptionType, " +
					"i.Description, " +			// 5
					"fq.Additional, " +
					"q.Niner, " +
					"f.Compare, " +
					"(SELECT COUNT(*) FROM OptionComponents ocs WHERE ocs.OptionID = qo.OptionID AND ocs.NoOrderValue IS NULL), " +
					"fq.FeedbackQuestionID, " +	// 10
					"f.NoHardcodedIdxs, " +
					"fq.HardcodedIdx, " +
					"q.Grey, " +
					"q.LeftIsVas " +
					"FROM FeedbackQuestion fq " +
					"INNER JOIN Feedback f ON fq.FeedbackID = f.FeedbackID " +
					"LEFT OUTER JOIN Question q ON fq.QuestionID = q.QuestionID " +
					"LEFT OUTER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
					"LEFT OUTER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + LID + " " +
					"LEFT OUTER JOIN [Option] o ON qo.OptionID = o.OptionID " +
					"LEFT OUTER JOIN SurveyQuestion sq ON sq.QuestionID = q.QuestionID AND sq.SurveyID = " + surveyID + " " +
					"LEFT OUTER JOIN SurveyQuestionLang sqlang ON sq.SurveyQuestionID = sqlang.SurveyQuestionID AND sqlang.LangID = " + LID + " " +
					"LEFT OUTER JOIN Idx i ON fq.IdxID = i.IdxID " +
					"WHERE (sq.SurveyQuestionID IS NOT NULL OR i.IdxID IS NOT NULL OR fq.HardcodedIdx IS NOT NULL) AND fq.FeedbackID = " + feedbackID + " " +
					//"AND q.QuestionID NOT IN (311, 1629, 1630, 1631, 1632, 1633, 1634, 1635, 1636, 1637, 1638, 1639, 1640, 1641, 2455) " +
					"ORDER BY fq.HardcodedIdx, i.IdxID, sq.SortOrder");
				while(rs.Read())
				{
					compare = !rs.IsDBNull(8);
					noHardcodedIdxs = !rs.IsDBNull(11);

					if(!rs.IsDBNull(12))
					{
						if(noHardcodedIdxs && (!compare || rs.GetInt32(8) > 1))
						{
							switch(rs.GetInt32(12))
							{
								case 1:
									sb.Append(hardcodedIdx1(groupID,surveyID,LID,FBR, ref cx, fn, showN, aids, RAC, rnds, rnds1, rnds2, depts1, depts2, r1, r2, rr, unitDesc, showTotal, BGCOLOR, AID1, AID1txt, AID2, AID2txt, PRDESC, projectRoundID, units, units2, percent, rnd.Next(), (compare ? rs.GetInt32(8) : 0), extraQS));
									break;
								case 2:
									sb.Append(hardcodedIdx2(groupID,surveyID,LID,FBR, ref cx, fn, showN, aids, RAC, rnds, rnds1, rnds2, depts1, depts2, r1, r2, rr, unitDesc, showTotal, BGCOLOR, AID1, AID1txt, AID2, AID2txt, PRDESC, projectRoundID, units, units2, percent, rnd.Next(), (compare ? rs.GetInt32(8) : 0), extraQS));
									break;
								case 3:	
									sb.Append(hardcodedIdx3(groupID,surveyID,LID,FBR, ref cx, fn, showN, aids, RAC, rnds, rnds1, rnds2, depts1, depts2, r1, r2, rr, unitDesc, showTotal, BGCOLOR, AID1, AID1txt, AID2, AID2txt, PRDESC, projectRoundID, units, units2, percent, rnd.Next(), (compare ? rs.GetInt32(8) : 0), extraQS));
									break;
								case 5:
									sb.Append(hardcodedIdx5(groupID,surveyID,LID,FBR, ref cx, fn, showN, aids, RAC, rnds, rnds1, rnds2, depts1, depts2, r1, r2, rr, unitDesc, showTotal, BGCOLOR, AID1, AID1txt, AID2, AID2txt, PRDESC, projectRoundID, units, units2, percent, rnd.Next(), (compare ? rs.GetInt32(8) : 0), extraQS));
									break;
								case 6:
									sb.Append(hardcodedIdx6(groupID,surveyID,LID,FBR, ref cx, fn, showN, aids, RAC, rnds, rnds1, rnds2, depts1, depts2, r1, r2, rr, unitDesc, showTotal, BGCOLOR, AID1, AID1txt, AID2, AID2txt, PRDESC, projectRoundID, units, units2, percent, rnd.Next(), (compare ? rs.GetInt32(8) : 0), extraQS));
									break;
							}
						}
					}
					else
					{
						if(!compare && !rs.IsDBNull(7))
						{
							if(!seen.Contains(rs.GetInt32(0)))
							{
								show3x3 = true;

								#region Square
								bool G = !rs.IsDBNull(13);
//									rs.GetInt32(0) == 416 || 
//									rs.GetInt32(0) == 418 || 
//									rs.GetInt32(0) == 420 || 
//									rs.GetInt32(0) == 422 || 
//									rs.GetInt32(0) == 424 || 
//									rs.GetInt32(0) == 426 || 
//									rs.GetInt32(0) == 3846 ||
//									rs.GetInt32(0) == 3889 || 
//									rs.GetInt32(0) == 3903 || //
//									rs.GetInt32(0) == 3966 || //
//									rs.GetInt32(0) == 3935 || //
//									rs.GetInt32(0) == 3936 || //
//									rs.GetInt32(0) == 3937 || //
//									rs.GetInt32(0) == 3938 || //
//									rs.GetInt32(0) == 3939 || //
//									rs.GetInt32(0) == 3940 ||
//									rs.GetInt32(0) == 4060;

								// optionid 1080, 1079 (and not 117, 118)
								bool leftIsVas = !rs.IsDBNull(14);
//									rs.GetInt32(0) == 3935 || //
//									rs.GetInt32(0) == 3936 || //
//									rs.GetInt32(0) == 3937 || //
//									rs.GetInt32(0) == 3938 || //
//									rs.GetInt32(0) == 3939 || //
//									rs.GetInt32(0) == 3940;

								sb.Append("<br class=\"noprint\"/><br/>");
								sb.Append("<div style=\"page-break-inside:avoid;\">");
								// " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "
								sb.Append("<div class=\"eform_area\"><p>" + rs.GetString(3) + "</p></div>");
								sb.Append("<div class=\"eform_ques\">");
								sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");

								string url = "http" + (HttpContext.Current.Request.IsSecureConnection ? "s" : "") + "://" + HttpContext.Current.Request.Url.Host + "/feedbackImage.aspx?fn=" + fn + "&" + 
									(showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + 
									(rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + 
									(depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + 
									(rr != 0 ? "RR=" + rr + "&" : "") + (units2 != "" ? "RRU=" + units2 + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + 
									(!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + 
									(AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "" + (G ? "G=1&" : "") + 
									"Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=" + (leftIsVas ? 17 : 7) + "&Q=" + rs.GetInt32(0) + "&U=" + units + 
									(groupID != 0 ? "&GroupID=" + groupID : "") + 
									"&Q1=" + HttpContext.Current.Server.UrlEncode(rs.GetString(2)) + 
									"&Q2=" + HttpContext.Current.Server.UrlEncode(doubleQ) + (!percent ? "&Percent=0" : "") + extraQS;

								int width = 550, height = 480;
								sb.Append("<TR><TD><img width=\"" + width + "\" height=\"" + height + "\" src=\"" + url + "\"></TD></TR>");
								string desc = "";
								switch(LID)
								{
									case 1: desc = (showTotal && (units != "" || aids != "") || rnds2 != "" ? (PRDESC != "" ? PRDESC : (rnds2 != "" ? r2 : "Sjukhusets")).Replace("[x]","") + " värde inom parentes." + (rr != 0 ? " Förändring från " + r2 + " till " + r1 + "." : "") : ""); break;
									case 2: desc = (showTotal && (units != "" || aids != "") || rnds2 != "" ? (PRDESC != "" ? PRDESC : (rnds2 != "" ? r2 : "Hospital")).Replace("[x]","") + " value in parenthesis." + (rr != 0 ? " Changes from " + r2 + " to " + r1 + "." : "") : ""); break;
								}
								if(desc != "")
								{
									sb.Append("<TR><TD ALIGN=\"CENTER\">" + desc + "</TD></TR>");
								}
								sb.Append("</TABLE>");
								sb.Append("</div>");
								sb.Append("</div>");
								cx++;

								Db.sqlExecute("INSERT INTO FeedbackRunRow (FeedbackRunID,URL,Area,Header,Description,Width,Height,FeedbackQuestionID) VALUES (" + FBR + ",'" + url.Replace("'","''") + "','" + rs.GetString(3).Replace("'","''") + "',NULL," + (desc != "" ? "'" + desc.Replace("'","''") + "'" : "NULL") + "," + width + "," + height + "," + (rs.IsDBNull(10) ? "NULL" : rs.GetInt32(10).ToString()) + ")");
								#endregion
						
								seen.Add(rs.GetInt32(0));
							}
						}
						else
						{
							int width = 550, height = 320;//480;

							int optionType = (rs.IsDBNull(4) ? 0 : rs.GetInt32(4));
							if(compare)
							{
								height = 520;
								switch(optionType)
								{
									case 1:
									{
										if(rs.GetInt32(9) > 2)
										{
											// More than two option components, feedback with mean value
											if(rs.GetInt32(8) == 2)
											{
												optionType = 400;
											}
											else
											{
												optionType = 200;
											}
										}
										else
										{
											// Two or less option components, feedback only one option component in percentage
											if(rs.GetInt32(8) == 2)
											{
												optionType = 500;
											}
											else
											{
												optionType = 300;
											}
										}
										break;
									}
									case 9:
									{
										// VAS mean value
										if(rs.GetInt32(8) == 2)
										{
											optionType = 408;
										}
										else
										{
											optionType = 208;
										}
										break;
									}
								}
							}

						Again:
							sb.Append("<br class=\"noprint\"/><br/>");
							sb.Append("<div style=\"page-break-inside:avoid;\">");
							//" + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "
							sb.Append("<div class=\"eform_area\"><p>" + rs.GetString(3) + "</p></div>");
							sb.Append("<div class=\"eform_ques\">");
							sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
							sb.Append("<TR><TD><B>" + rs.GetString(2) + "</B></TD></TR>");
							//sb.Append("<TR><TD><img src=\"" + System.Configuration.ConfigurationSettings.AppSettings["InstanceURL"] + "/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID);
							switch(optionType)
							{
								case 1:
								case 3: 
								case 18: 
									height = 440; break;
									//							default: 
									//								height = 320; break;
							}
							string url = "http" + (HttpContext.Current.Request.IsSecureConnection ? "s" : "") + "://" + HttpContext.Current.Request.Url.Host + "/feedbackImage.aspx?fn=" + fn + "&" + 
								(showN ? "ShowN=1&" : "") + 
								(aids != "" ? "AIDS=" + aids + "&" : "") + 
								"RAC=" + RAC + "&" +
								"LID=" + LID + 
								(rnds != "" ? "&RNDS=" + rnds : "") + 
								(rnds1 != "" ? "&RNDS1=" + rnds1 : "") + 
								(rnds2 != "" ? "&RNDS2=" + rnds2 : "") + 
								(depts1 != "" ? "&DEPTS1=" + depts1 : "") + 
								(depts2 != "" ? "&DEPTS2=" + depts2 : "") + 
								"&R1=" + r1 + 
								"&R2=" + r2 + "&" + 
								(rr != 0 ? "RR=" + rr + "&" : "") + 
								(units2 != "" ? "RRU=" + units2 + "&" : "") + 
								(unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + 
								(!showTotal ? "ST=0&" : "") + 
								(BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + 
								(AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + 
								(AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + 
								(PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + 
								"Rnd=" + rnd.Next() + 
								"&R=" + projectRoundID + 
								(groupID != 0 ? "&GroupID=" + groupID : "") + 
								(!percent ? "&Percent=0" : "") + extraQS;
							if(optionType == 0)
							{
								url += "&Q=" + Math.Abs(rs.GetInt32(0)) + "&T=20";
							}
							else
							{
								switch(optionType)
								{
									case 1:	
										url += "&T=0"; break;
									case 2:	
									case 4:
										url += "&T=12"; break;
									case 9:	
										url += "&T=8"; break;
									case 3:
										url += "&T=13"; break;
									default:
										url += "&T=" + optionType; break;
								}
								url += "&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1);
							}
							if(!rs.IsDBNull(6))
							{
								url += "&VTL=" + rs.GetInt32(6);
							}
							url += "&U=" + units;

							sb.Append("<TR><TD><img width=\"" + width + "\" height=\"" + height + "\" src=\"" + url + "\"></TD></TR>");

							string desc = "";
							if(!rs.IsDBNull(5))
							{
								desc = rs.GetString(5);
							}
							if(desc != "")
							{
								sb.Append("<TR><TD>" + desc + "</TD></TR>");
							}
							sb.Append("</TABLE>");
							sb.Append("</div>");
							sb.Append("</div>");
							cx++;

							Db.sqlExecute("INSERT INTO FeedbackRunRow (FeedbackRunID,URL,Area,Header,Description,Width,Height,FeedbackQuestionID) VALUES (" + FBR + ",'" + url.Replace("'","''") + "','" + rs.GetString(3).Replace("'","''") + "','" + rs.GetString(2).Replace("'","''") + "'," + (desc != "" ? "'" + desc.Replace("'","''") + "'" : "NULL") + "," + width + "," + height + "," + (rs.IsDBNull(10) ? "NULL" : rs.GetInt32(10).ToString()) + ")");

							if(!rs.IsDBNull(6) && optionType == 9)
							{
								optionType = 18;
								goto Again;
							}
						}
					}
				}
				rs.Close();

				if(!compare && !noHardcodedIdxs)
				{
					sb.Append(hardcodedIdx1(groupID,surveyID,LID,FBR, ref cx, fn, showN, aids, RAC, rnds, rnds1, rnds2, depts1, depts2, r1, r2, rr, unitDesc, showTotal, BGCOLOR, AID1, AID1txt, AID2, AID2txt, PRDESC, projectRoundID, units, units2, percent, rnd.Next(),0, extraQS));
					sb.Append(hardcodedIdx2(groupID,surveyID,LID,FBR, ref cx, fn, showN, aids, RAC, rnds, rnds1, rnds2, depts1, depts2, r1, r2, rr, unitDesc, showTotal, BGCOLOR, AID1, AID1txt, AID2, AID2txt, PRDESC, projectRoundID, units, units2, percent, rnd.Next(),0, extraQS));
					sb.Append(hardcodedIdx3(groupID,surveyID,LID,FBR, ref cx, fn, showN, aids, RAC, rnds, rnds1, rnds2, depts1, depts2, r1, r2, rr, unitDesc, showTotal, BGCOLOR, AID1, AID1txt, AID2, AID2txt, PRDESC, projectRoundID, units, units2, percent, rnd.Next(),0, extraQS));
					sb.Append(hardcodedIdx5(groupID,surveyID,LID,FBR, ref cx, fn, showN, aids, RAC, rnds, rnds1, rnds2, depts1, depts2, r1, r2, rr, unitDesc, showTotal, BGCOLOR, AID1, AID1txt, AID2, AID2txt, PRDESC, projectRoundID, units, units2, percent, rnd.Next(),0, extraQS));
					sb.Append(hardcodedIdx6(groupID,surveyID,LID,FBR, ref cx, fn, showN, aids, RAC, rnds, rnds1, rnds2, depts1, depts2, r1, r2, rr, unitDesc, showTotal, BGCOLOR, AID1, AID1txt, AID2, AID2txt, PRDESC, projectRoundID, units, units2, percent, rnd.Next(),0, extraQS));
				}
				#endregion
			}
			else if(surveyID == 234)
			{
				#region 234
				bool seen1 = false, seen2 = false, seen3 = false, seen5 = false, seen6 = false; show3x3 = false;
				RAC = 7;

				rs = Db.recordSet("SELECT " +
					"sq.QuestionID, " +
					"qo.OptionID, " +
					"ql.Question, " +
					"ISNULL(ql.QuestionArea,'/.../'), " +
					"o.OptionType " +
					"FROM SurveyQuestion sq " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + LID + " " +
					"INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
					"WHERE sq.SurveyID = " + surveyID + " AND sq.QuestionID NOT IN (311, 1629, 1630, 1631, 1632, 1633, 1634, 1635, 1636, 1637, 1638, 1639, 1640, 1641, 2455) " +
					"ORDER BY sq.SortOrder");
				while(rs.Read())
				{
					if(rs.GetInt32(0) == 393 || rs.GetInt32(0) == 392 || rs.GetInt32(0) == 391 || rs.GetInt32(0) == 390 || rs.GetInt32(0) == 389 || rs.GetInt32(0) == 388 || rs.GetInt32(0) == 387 || rs.GetInt32(0) == 404 || rs.GetInt32(0) == 403 || rs.GetInt32(0) == 402 || rs.GetInt32(0) == 401 || rs.GetInt32(0) == 380 || rs.GetInt32(0) == 381 || rs.GetInt32(0) == 382 || rs.GetInt32(0) == 383 || rs.GetInt32(0) == 384 || rs.GetInt32(0) == 459 || rs.GetInt32(0) == 460 || rs.GetInt32(0) == 461 || rs.GetInt32(0) == 462 || rs.GetInt32(0) == 463)
					{
						if(!seen1)
						{
							seen1 = true;

							#region T=1
							OdbcDataReader rs2 = Db.recordSet("SELECT " +
								"s.SurveyID " +
								"FROM Survey s " +
								"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 380 " +
								"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 381 " +
								"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 382 " +
								"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 383 " +
								"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 384 " +
								"INNER JOIN SurveyQuestion sq6 ON sq6.SurveyID = s.SurveyID AND sq6.QuestionID = 459 " +
								"INNER JOIN SurveyQuestion sq7 ON sq7.SurveyID = s.SurveyID AND sq7.QuestionID = 460 " +
								"INNER JOIN SurveyQuestion sq8 ON sq8.SurveyID = s.SurveyID AND sq8.QuestionID = 461 " +
								"INNER JOIN SurveyQuestion sq9 ON sq9.SurveyID = s.SurveyID AND sq9.QuestionID = 462 " +
								"INNER JOIN SurveyQuestion sq10 ON sq10.SurveyID = s.SurveyID AND sq10.QuestionID = 463 " +
								"WHERE s.SurveyID = " + surveyID);
							if(rs2.Read())
							{
								sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
								switch(LID)
								{
									case 1: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Medelvärde utmattning</p></div>"); break;
									case 2: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Mean value exhaustion</p></div>"); break;
								}
								sb.Append("<div class=\"eform_ques\">");
								sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
								sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=1&U=" + units + (!percent ? "&Percent=0" : "") + "\"></TD></TR>");
								switch(LID)
								{
									case 1: sb.Append("<TR><TD>Värden över 1.6 innebär tecken på hög stressnivå. Långvarig stress utan tillräcklig återhämtning kan leda till utbrändhet eller utmattningssyndrom. Det finns bra belägg för att en utbränningsprocess kan stoppas upp. Man kan ha hjälp av olika metoder, t ex kollegiala samtalsgrupper.</TD></TR>"); break;
									case 2: sb.Append("<TR><TD>Values above 1.6 imply signs of high stress level.</TD></TR>"); break;
								}
								sb.Append("</TABLE>");
								sb.Append("</div>");
								cx++;
							}
							rs2.Close();
							#endregion
						}

						if(!seen2)
						{
							seen2 = true;

							#region T=2
							OdbcDataReader rs2 = Db.recordSet("SELECT " +
								"s.SurveyID " +
								"FROM Survey s " +
								"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 380 " +
								"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 381 " +
								"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 382 " +
								"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 383 " +
								"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 384 " +
								"INNER JOIN SurveyQuestion sq6 ON sq6.SurveyID = s.SurveyID AND sq6.QuestionID = 459 " +
								"INNER JOIN SurveyQuestion sq7 ON sq7.SurveyID = s.SurveyID AND sq7.QuestionID = 460 " +
								"INNER JOIN SurveyQuestion sq8 ON sq8.SurveyID = s.SurveyID AND sq8.QuestionID = 461 " +
								"INNER JOIN SurveyQuestion sq9 ON sq9.SurveyID = s.SurveyID AND sq9.QuestionID = 462 " +
								"INNER JOIN SurveyQuestion sq10 ON sq10.SurveyID = s.SurveyID AND sq10.QuestionID = 463 " +
								"WHERE s.SurveyID = " + surveyID);
							if(rs2.Read())
							{
								sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
								switch(LID)
								{
									case 1: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Andel med symptom på utbrändhet</p></div>"); break;
									case 2: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Share with symptoms of burnout</p></div>"); break;
								}
								sb.Append("<div class=\"eform_ques\">");
								sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
								sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=2&U=" + units + (!percent ? "&Percent=0" : "") + "\"></TD></TR>");
								switch(LID)
								{
									case 1: sb.Append("<TR><TD>Kombinationen lågt engagemang (&lt;1.4) och hög grad av utmattning (&gt;1.6) innebär hög risk för utbrändhet.</TD></TR>"); break;
								}
								sb.Append("</TABLE>");
								sb.Append("</div>");
								cx++;
							}
							rs2.Close();
							#endregion
						}

						if(!seen3)
						{
							seen3 = true;

							#region T=3
							OdbcDataReader rs2 = Db.recordSet("SELECT " +
								"s.SurveyID " +
								"FROM Survey s " +
								"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 459 " +
								"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 460 " +
								"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 461 " +
								"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 462 " +
								"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 463 " +
								"WHERE s.SurveyID = " + surveyID);
							if(rs2.Read())
							{
								sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
								switch(LID)
								{
									case 1: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Medelvärde engagemang</p></div>"); break;
									case 2: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Mean value engagement</p></div>"); break;
								}
								sb.Append("<div class=\"eform_ques\">");
								sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
								sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=3&U=" + units + (!percent ? "&Percent=0" : "") + "\"></TD></TR>");
								switch(LID)
								{
									case 1: sb.Append("<TR><TD>Höga värden är ett uttryck för att man trivs med sitt arbete, vilket i sin tur anses ha ett samband med kvalitet och produktivitet.</TD></TR>"); break;
								}
								sb.Append("</TABLE>");
								sb.Append("</div>");
								cx++;
							}
							rs2.Close();
							#endregion
						}

						if(!seen5)
						{
							seen5 = true;

							#region T=5
							OdbcDataReader rs2 = Db.recordSet("SELECT " +
								"s.SurveyID " +
								"FROM Survey s " +
								"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 387 " +
								"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 388 " +
								"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 389 " +
								"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 390 " +
								"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 391 " +
								"INNER JOIN SurveyQuestion sq6 ON sq6.SurveyID = s.SurveyID AND sq6.QuestionID = 392 " +
								"INNER JOIN SurveyQuestion sq7 ON sq7.SurveyID = s.SurveyID AND sq7.QuestionID = 393 " +
								"WHERE s.SurveyID = " + surveyID);
							if(rs2.Read())
							{
								sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
								switch(LID)
								{
									case 1: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Andel med tecken på nedstämdhet</p></div>"); break;
									case 2: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Share with signs of depression</p></div>"); break;
								}
								sb.Append("<div class=\"eform_ques\">");
								sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
								sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=5&U=" + units + (!percent ? "&Percent=0" : "") + "\"></TD></TR>");
								sb.Append("</TABLE>");
								sb.Append("</div>");
								cx++;
							}
							rs2.Close();
							#endregion
						}
						if(!seen6)
						{
							seen6 = true;

							#region T=6
							OdbcDataReader rs2 = Db.recordSet("SELECT " +
								"s.SurveyID " +
								"FROM Survey s " +
								"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 380 " +
								"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 381 " +
								"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 382 " +
								"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 383 " +
								"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 384 " +
								"INNER JOIN SurveyQuestion sq6 ON sq6.SurveyID = s.SurveyID AND sq6.QuestionID = 401 " +
								"INNER JOIN SurveyQuestion sq7 ON sq7.SurveyID = s.SurveyID AND sq7.QuestionID = 402 " +
								"INNER JOIN SurveyQuestion sq8 ON sq8.SurveyID = s.SurveyID AND sq8.QuestionID = 403 " +
								"INNER JOIN SurveyQuestion sq9 ON sq9.SurveyID = s.SurveyID AND sq9.QuestionID = 404 " +
								"WHERE s.SurveyID = " + surveyID);
							if(rs2.Read())
							{
								sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
								switch(LID)
								{
									case 1: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Andel med ökad risk för långtidssjukfrånvaro</p></div>"); break;
									case 2: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Share with increased risk of long-term sick-leave</p></div>"); break;
								}
								sb.Append("<div class=\"eform_ques\">");
								sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
								sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=6&U=" + units + (!percent ? "&Percent=0" : "") + "\"></TD></TR>");
								switch(LID)
								{
									case 1: sb.Append("<TR><TD>Andel med ett utmattningsvärde över 1.6 i kombination med ett indexvärde på prestationsbaserad självkänsla över 3.25.</TD></TR>"); break;
								}
								sb.Append("</TABLE>");
								sb.Append("</div>");
								cx++;
							}
							rs2.Close();
							#endregion
						}
					}
					else if(rs.GetInt32(0) == 406 || rs.GetInt32(0) == 408 || rs.GetInt32(0) == 410 || rs.GetInt32(0) == 412 || rs.GetInt32(0) == 414 || rs.GetInt32(0) == 801 || rs.GetInt32(0) == 3847 || rs.GetInt32(0) == 3848 || rs.GetInt32(0) == 3845 || rs.GetInt32(0) == 3846 || rs.GetInt32(0) == 416 || rs.GetInt32(0) == 418 || rs.GetInt32(0) == 420 || rs.GetInt32(0) == 422 || rs.GetInt32(0) == 424 || rs.GetInt32(0) == 426 || rs.GetInt32(0) == 428 || rs.GetInt32(0) == 430 || rs.GetInt32(0) == 432 || rs.GetInt32(0) == 434 || rs.GetInt32(0) == 436 || rs.GetInt32(0) == 438 || rs.GetInt32(0) == 440 || rs.GetInt32(0) == 442)
					{
						#region Square
						bool G = rs.GetInt32(0) == 3846 || rs.GetInt32(0) == 416 || rs.GetInt32(0) == 418 || rs.GetInt32(0) == 420 || rs.GetInt32(0) == 422 || rs.GetInt32(0) == 424 || rs.GetInt32(0) == 426;

						sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
						sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>" + rs.GetString(3) + "</p></div>");
						sb.Append("<div class=\"eform_ques\">");
						sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
						sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + 
							(showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + 
							(rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + 
							(depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + 
							(rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + 
							(!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + 
							(AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "" + (G ? "G=1&" : "") + 
							"Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=7&Q=" + rs.GetInt32(0) + "&U=" + units + 
							"&Q1=" + HttpContext.Current.Server.UrlEncode(rs.GetString(2)) + 
							"&Q2=" + HttpContext.Current.Server.UrlEncode(doubleQ) + (!percent ? "&Percent=0" : "") + "\"></TD></TR>");
						switch(LID)
						{
							case 1: sb.Append((showTotal && (units != "" || aids != "") || rnds2 != "" ? "<TR><TD ALIGN=\"CENTER\">" + (PRDESC != "" ? PRDESC : (rnds2 != "" ? r2 : "Sjukhusets")).Replace("[x]","") + " värde inom parentes." + (rr != 0 ? " Förändring från " + r2 + " till " + r1 + "." : "") + "</TD></TR>" : "")); break;
							case 2: sb.Append((showTotal && (units != "" || aids != "") || rnds2 != "" ? "<TR><TD ALIGN=\"CENTER\">" + (PRDESC != "" ? PRDESC : (rnds2 != "" ? r2 : "Hospital")).Replace("[x]","") + " value in parenthesis." + (rr != 0 ? " Changes from " + r2 + " to " + r1 + "." : "") + "</TD></TR>" : "")); break;
						}
						sb.Append("</TABLE>");
						sb.Append("</div>");
						cx++;
						#endregion
					}
					else
					{
						sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
						sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>" + rs.GetString(3) + "</p></div>");
						sb.Append("<div class=\"eform_ques\">");
						sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
						sb.Append("<TR><TD><B>" + rs.GetString(2) + "</B></TD></TR>");
						sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_").Replace("\"","_2_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + (!percent ? "&Percent=0" : ""));
						switch(rs.GetInt32(4))
						{
							case 1:	
								sb.Append("&T=0"); break;
							case 2:	
							case 4:
								sb.Append("&T=12"); break;
							case 9:	
								sb.Append("&T=8"); break;
							case 3:
								sb.Append("&T=13"); break;
						}	
						sb.Append("&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1) + "&U=" + units + "\"></TD></TR>");
						sb.Append("</TABLE>");
						sb.Append("</div>");
						cx++;
					}
				}
				rs.Close();
				#endregion
			}
			else
			{
				#region T=0
				rs = Db.recordSet("SELECT sq.QuestionID, qo.OptionID, ql.Question, ISNULL(ql.QuestionArea,'/.../') " +
					"FROM SurveyQuestion sq " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + LID + " " +
					"WHERE sq.QuestionID IN (" +
					"331," +
					"368," +
					"369," +
					"1617," +	//
					"1644," +	//
					"333," +
					"334," +
					"337," +
					"339," +
					"374," +
					//"375," +
					//"376," +
					//"377," +
					//"378," +
					//"379," +
					"459," +
					"380," +
					"460," +
					"381," +
					"382," +
					"461," +
					"462," +
					"383," +
					"384," +
					"463" +
					"" + (projectRoundID != 30 ? "," +
					"364," +	// Alkohol
					"370," +
					"1613," +
					"539," +
					"911" +
					"" : "") + 
					"" + (surveyID == 120 ? "" +	// SSM
					",208" +	// Alkohol följd
					",210" +	// Alkohol följd
					",213" + 	// Alkohol följd
					"" : "") +
					",1943" +
					",1944" +
					",1947" +
					",1948" +
					",1949" +
					",1950" +
					",1953" +
					",2023,2019,2020,2021,2025,2022,2024,2018,2017,2015,2016" +
					",3734" +

					") AND sq.SurveyID = " + surveyID + " " +
					"" + (projectRoundID == 194 ? "AND sq.QuestionID NOT IN (364) " : "") +
					"ORDER BY sq.SortOrder");
				while(rs.Read())
				{
					string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
					System.IO.FileStream fs = createStream(path,file);
					feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 0, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, rs.GetInt32(0), false, "", "", rs.GetInt32(1), false, 0, "",percent,0);
					fs.Close();

					sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
					sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>" + rs.GetString(3) + "</p></div>");
					sb.Append("<div class=\"eform_ques\">");
					sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
					sb.Append("<TR><TD><B>" + rs.GetString(2) + "</B></TD></TR>");
					//sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=0&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1) + "&U=" + units + "\"></TD></TR>");
					sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
					sb.Append("</TABLE>");
					sb.Append("</div>");
					//FeedbackText.Text += "<b style=\"font-size:16px;\">" + rs.GetString(2) + "</b><br/><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (aids != "" ? "AIDS=" + aids + "&" : "") + "R=" + projectRoundID + "&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1) + "&U=" + units + "\"><BR/><BR/>";
					cx++;
				}
				rs.Close();
				#endregion

				#region T=8
				rs = Db.recordSet("SELECT sq.QuestionID, qo.OptionID, ql.Question, ISNULL(ql.QuestionArea,'/.../') " +
					"FROM SurveyQuestion sq " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + LID + " " +
					"WHERE sq.QuestionID IN (728,729,730,731,732,733,734,735,736,737,738,739,740) AND sq.SurveyID = " + surveyID + " " +
					"ORDER BY sq.SortOrder");
				while(rs.Read())
				{
					string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
					System.IO.FileStream fs = createStream(path,file);
					feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 8, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, rs.GetInt32(0), false, "", "", rs.GetInt32(1), false, 0, "",percent,0);
					fs.Close();

					sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
					sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>" + rs.GetString(3) + "</p></div>");
					sb.Append("<div class=\"eform_ques\">");
					sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
					sb.Append("<TR><TD><B>" + rs.GetString(2) + "</B></TD></TR>");
					//sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=8&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1) + "&U=" + units + "\"></TD></TR>");
					sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
					sb.Append("</TABLE>");
					sb.Append("</div>");
					cx++;
				}
				rs.Close();
				#endregion

				#region T=1
				rs = Db.recordSet("SELECT " +
					"s.SurveyID " +
					"FROM Survey s " +
					"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 380 " +
					"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 381 " +
					"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 382 " +
					"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 383 " +
					"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 384 " +
					"INNER JOIN SurveyQuestion sq6 ON sq6.SurveyID = s.SurveyID AND sq6.QuestionID = 459 " +
					"INNER JOIN SurveyQuestion sq7 ON sq7.SurveyID = s.SurveyID AND sq7.QuestionID = 460 " +
					"INNER JOIN SurveyQuestion sq8 ON sq8.SurveyID = s.SurveyID AND sq8.QuestionID = 461 " +
					"INNER JOIN SurveyQuestion sq9 ON sq9.SurveyID = s.SurveyID AND sq9.QuestionID = 462 " +
					"INNER JOIN SurveyQuestion sq10 ON sq10.SurveyID = s.SurveyID AND sq10.QuestionID = 463 " +
					"WHERE s.SurveyID = " + surveyID);
				if(rs.Read())
				{
					string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
					System.IO.FileStream fs = createStream(path,file);
					feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 1, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, 0, false, "", "", 0, false, 0, "",percent,0);
					fs.Close();

					sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
					switch(LID)
					{
						case 1: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Medelvärde utmattning</p></div>"); break;
						case 2: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Mean value exhaustion</p></div>"); break;
					}
					sb.Append("<div class=\"eform_ques\">");
					sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
					//sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=1&U=" + units + "\"></TD></TR>");
					sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
					switch(LID)
					{
						case 1: sb.Append("<TR><TD>Värden över 1.6 innebär tecken på hög stressnivå. Långvarig stress utan tillräcklig återhämtning kan leda till utbrändhet eller utmattningssyndrom. Det finns bra belägg för att en utbränningsprocess kan stoppas upp. Man kan ha hjälp av olika metoder, t ex kollegiala samtalsgrupper.</TD></TR>"); break;
						case 2: sb.Append("<TR><TD>Values above 1.6 imply signs of high stress level.</TD></TR>"); break;
					}
					sb.Append("</TABLE>");
					sb.Append("</div>");
					cx++;
				}
				rs.Close();
				#endregion
				
				#region T=3
				rs = Db.recordSet("SELECT " +
					"s.SurveyID " +
					"FROM Survey s " +
					"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 459 " +
					"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 460 " +
					"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 461 " +
					"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 462 " +
					"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 463 " +
					"WHERE s.SurveyID = " + surveyID);
				if(rs.Read())
				{
					string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
					System.IO.FileStream fs = createStream(path,file);
					feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 3, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, 0, false, "", "", 0, false, 0, "",percent,0);
					fs.Close();

					sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
					switch(LID)
					{
						case 1: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Medelvärde engagemang</p></div>"); break;
						case 2: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Mean value engagement</p></div>"); break;
					}
					sb.Append("<div class=\"eform_ques\">");
					sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
					//sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=3&U=" + units + "\"></TD></TR>");
					sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
					switch(LID)
					{
						case 1: sb.Append("<TR><TD>Höga värden är ett uttryck för att man trivs med sitt arbete, vilket i sin tur anses ha ett samband med kvalitet och produktivitet.</TD></TR>"); break;
					}
					sb.Append("</TABLE>");
					sb.Append("</div>");
					cx++;
				}
				rs.Close();
				#endregion

				if(AID1 == 0 && AID2 == 0)
				{
					#region Endast gruppåterkoppling

					// T == 2
					rs = Db.recordSet("SELECT " +
						"s.SurveyID " +
						"FROM Survey s " +
						"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 380 " +
						"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 381 " +
						"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 382 " +
						"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 383 " +
						"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 384 " +
						"INNER JOIN SurveyQuestion sq6 ON sq6.SurveyID = s.SurveyID AND sq6.QuestionID = 459 " +
						"INNER JOIN SurveyQuestion sq7 ON sq7.SurveyID = s.SurveyID AND sq7.QuestionID = 460 " +
						"INNER JOIN SurveyQuestion sq8 ON sq8.SurveyID = s.SurveyID AND sq8.QuestionID = 461 " +
						"INNER JOIN SurveyQuestion sq9 ON sq9.SurveyID = s.SurveyID AND sq9.QuestionID = 462 " +
						"INNER JOIN SurveyQuestion sq10 ON sq10.SurveyID = s.SurveyID AND sq10.QuestionID = 463 " +
						"WHERE s.SurveyID = " + surveyID);
					if(rs.Read())
					{
						string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
						System.IO.FileStream fs = createStream(path,file);
						feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 2, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, 0, false, "", "", 0, false, 0, "",percent,0);
						fs.Close();

						sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
						switch(LID)
						{
							case 1: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Andel med symptom på utbrändhet</p></div>"); break;
							case 2: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Share with symptoms of burnout</p></div>"); break;
						}
						sb.Append("<div class=\"eform_ques\">");
						sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
						//sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=2&U=" + units + "\"></TD></TR>");
						sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
						switch(LID)
						{
							case 1: sb.Append("<TR><TD>Kombinationen lågt engagemang (&lt;1.4) och hög grad av utmattning (&gt;1.6) innebär hög risk för utbrändhet.</TD></TR>"); break;
						}
						sb.Append("</TABLE>");
						sb.Append("</div>");
						cx++;
					}
					rs.Close();

					rs = Db.recordSet("SELECT " +
						"s.SurveyID " +
						"FROM Survey s " +
						"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 380 " +
						"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 381 " +
						"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 382 " +
						"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 383 " +
						"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 384 " +
						"INNER JOIN SurveyQuestion sq6 ON sq6.SurveyID = s.SurveyID AND sq6.QuestionID = 401 " +
						"INNER JOIN SurveyQuestion sq7 ON sq7.SurveyID = s.SurveyID AND sq7.QuestionID = 402 " +
						"INNER JOIN SurveyQuestion sq8 ON sq8.SurveyID = s.SurveyID AND sq8.QuestionID = 403 " +
						"INNER JOIN SurveyQuestion sq9 ON sq9.SurveyID = s.SurveyID AND sq9.QuestionID = 404 " +
						"WHERE s.SurveyID = " + surveyID);
					if(rs.Read())
					{
						string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
						System.IO.FileStream fs = createStream(path,file);
						feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 6, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, 0, false, "", "", 0, false, 0, "",percent,0);
						fs.Close();

						sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
						switch(LID)
						{
							case 1: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Andel med ökad risk för långtidssjukfrånvaro</p></div>"); break;
							case 2: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Share with increased risk of long-term sick-leave</p></div>"); break;
						}
						sb.Append("<div class=\"eform_ques\">");
						sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
						//sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=6&U=" + units + "\"></TD></TR>");
						sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
						switch(LID)
						{
							case 1: sb.Append("<TR><TD>Andel med ett utmattningsvärde över 1.6 i kombination med ett indexvärde på prestationsbaserad självkänsla över 3.25.</TD></TR>"); break;
						}
						sb.Append("</TABLE>");
						sb.Append("</div>");
						cx++;
					}
					rs.Close();

					/*if(cx++ > 0)
						{
							FeedbackText.Text += "<br/><br style=\"page-break-before:always;\"/>";
						}
						FeedbackText.Text += "<div class=\"eform_area\"><p>Andel med ett D-värde > 2.6; symptom på utbrändhet</p></div>";
						FeedbackText.Text += "<div class=\"eform_ques\">";
						FeedbackText.Text += "<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">";
						FeedbackText.Text += "<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (aids != "" ? "AIDS=" + aids + "&" : "") + "R=" + projectRoundID + "&T=4&U=" + units + "\"></TD></TR>";
						FeedbackText.Text += "</TABLE>";
						FeedbackText.Text += "</div>";*/

					rs = Db.recordSet("SELECT " +
						"s.SurveyID " +
						"FROM Survey s " +
						"INNER JOIN SurveyQuestion sq1 ON sq1.SurveyID = s.SurveyID AND sq1.QuestionID = 387 " +
						"INNER JOIN SurveyQuestion sq2 ON sq2.SurveyID = s.SurveyID AND sq2.QuestionID = 388 " +
						"INNER JOIN SurveyQuestion sq3 ON sq3.SurveyID = s.SurveyID AND sq3.QuestionID = 389 " +
						"INNER JOIN SurveyQuestion sq4 ON sq4.SurveyID = s.SurveyID AND sq4.QuestionID = 390 " +
						"INNER JOIN SurveyQuestion sq5 ON sq5.SurveyID = s.SurveyID AND sq5.QuestionID = 391 " +
						"INNER JOIN SurveyQuestion sq6 ON sq6.SurveyID = s.SurveyID AND sq6.QuestionID = 392 " +
						"INNER JOIN SurveyQuestion sq7 ON sq7.SurveyID = s.SurveyID AND sq7.QuestionID = 393 " +
						"WHERE s.SurveyID = " + surveyID);
					if(rs.Read())
					{
						string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
						System.IO.FileStream fs = createStream(path,file);
						feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 5, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, 0, false, "", "", 0, false, 0, "",percent,0);
						fs.Close();

						sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
						switch(LID)
						{
							case 1: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Andel med tecken på nedstämdhet</p></div>"); break;
							case 2: sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>Share with signs of depression</p></div>"); break;
						}
						sb.Append("<div class=\"eform_ques\">");
						sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
						//sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=5&U=" + units + "\"></TD></TR>");
						sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
						sb.Append("</TABLE>");
						sb.Append("</div>");
						cx++;
					}
					rs.Close();
				
					#endregion
				}

				#region Square - Color marked T=7
				rs = Db.recordSet("SELECT " +
					"sq.QuestionID, " +
					"ql.Question, " +
					"ISNULL(ql.QuestionArea,'/.../') " +
					"FROM SurveyQuestion sq " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + LID + " " +
					"WHERE sq.QuestionID IN (406,408,410,412,414,801,3847,3848" +
					",3845" +
					",3846" +
					") AND sq.SurveyID = " + surveyID + " " +
					"ORDER BY sq.SortOrder");
				while(rs.Read())
				{
					bool G = rs.GetInt32(0) == 3846;

					string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
					System.IO.FileStream fs = createStream(path,file);
					feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 7, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, rs.GetInt32(0), G, rs.GetString(1), doubleQ, 0, false, 0,"",percent,0);
					fs.Close();

					sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
					sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>" + rs.GetString(2) + "</p></div>");
					sb.Append("<div class=\"eform_ques\">");
					sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
					sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
//					sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + 
//						(showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + 
//						(rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + 
//						(depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + 
//						(rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + 
//						(!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + 
//						(AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "" + (G ? "G=1&" : "") + 
//						"Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=7&Q=" + rs.GetInt32(0) + "&U=" + units + 
//						"&Q1=" + HttpContext.Current.Server.UrlEncode(rs.GetString(1)) + 
//						"&Q2=" + HttpContext.Current.Server.UrlEncode(doubleQ) + "\"></TD></TR>");
					switch(LID)
					{
						case 1: sb.Append((showTotal && (units != "" || aids != "") || rnds2 != "" ? "<TR><TD ALIGN=\"CENTER\">" + (PRDESC != "" ? PRDESC : (rnds2 != "" ? r2 : "Sjukhusets")).Replace("[x]","") + " värde inom parentes." + (rr != 0 ? " Förändring från " + r2 + " till " + r1 + "." : "") + "</TD></TR>" : "")); break;
						case 2: sb.Append((showTotal && (units != "" || aids != "") || rnds2 != "" ? "<TR><TD ALIGN=\"CENTER\">" + (PRDESC != "" ? PRDESC : (rnds2 != "" ? r2 : "Hospital")).Replace("[x]","") + " value in parenthesis." + (rr != 0 ? " Changes from " + r2 + " to " + r1 + "." : "") + "</TD></TR>" : "")); break;
					}
					sb.Append("</TABLE>");
					sb.Append("</div>");
					cx++;
				}
				rs.Close();
				#endregion

				#region T=8
				rs = Db.recordSet("SELECT sq.QuestionID, qo.OptionID, ql.Question, ISNULL(ql.QuestionArea,'/.../') " +
					"FROM SurveyQuestion sq " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + LID + " " +
					"WHERE sq.QuestionID IN (564,565,566,567,568,569) AND sq.SurveyID = " + surveyID + " " +
					"ORDER BY sq.SortOrder");
				while(rs.Read())
				{
					string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
					System.IO.FileStream fs = createStream(path,file);
					feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 8, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, rs.GetInt32(0), false, "", "", rs.GetInt32(1), false, 0, "",percent,0);
					fs.Close();

					sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
					sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>" + rs.GetString(3) + "</p></div>");
					sb.Append("<div class=\"eform_ques\">");
					sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
					switch(LID)
					{
						case 1: sb.Append("<TR><TD>Bedöm i vilken utsträckning följande påstående passar in på din närmaste chef:<BR><B>" + rs.GetString(2) + "</B></TD></TR>"); break;
						case 2: sb.Append("<TR><TD>Does your immediate superior...<BR><B>" + rs.GetString(2) + "</B></TD></TR>"); break;
					}
					sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
					//sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=8&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1) + "&U=" + units + "\"></TD></TR>");
					sb.Append("</TABLE>");
					sb.Append("</div>");
					cx++;
					//FeedbackText.Text += "<b style=\"font-size:16px;\">" + rs.GetString(2) + "</b><br/><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (aids != "" ? "AIDS=" + aids + "&" : "") + "R=" + projectRoundID + "&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1) + "&U=" + units + "\"><BR/><BR/>";
				}
				rs.Close();
				#endregion

				#region Square
				rs = Db.recordSet("SELECT sq.QuestionID, ql.Question, ISNULL(ql.QuestionArea,'/.../') " +
					"FROM SurveyQuestion sq " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + LID + " " +
					"WHERE sq.QuestionID IN (416,418,420,422,424,426,428,430,432,434,436,438,440,442" +
					") AND sq.SurveyID = " + surveyID + " " +
					"ORDER BY sq.SortOrder");
				while(rs.Read())
				{
					bool G = rs.GetInt32(0) == 416 || rs.GetInt32(0) == 418 || rs.GetInt32(0) == 420 || rs.GetInt32(0) == 422 || rs.GetInt32(0) == 424 || rs.GetInt32(0) == 426;

					string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
					System.IO.FileStream fs = createStream(path,file);
					feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 7, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, rs.GetInt32(0), G, rs.GetString(1), doubleQ, 0, false, 0,"",percent,0);
					fs.Close();

					sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
					sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>" + rs.GetString(2) + "</p></div>");
					sb.Append("<div class=\"eform_ques\">");
					sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
					sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
//					sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + 
//						(showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + 
//						(rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + 
//						(depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + 
//						(rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + 
//						(!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + 
//						(AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "" + (G ? "G=1&" : "") + 
//						"Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=7&Q=" + rs.GetInt32(0) + "&U=" + units + 
//						"&Q1=" + HttpContext.Current.Server.UrlEncode(rs.GetString(1)) + 
//						"&Q2=" + HttpContext.Current.Server.UrlEncode(doubleQ) + "\"></TD></TR>");
					switch(LID)
					{
						case 1: sb.Append((showTotal && (units != "" || aids != "") || rnds2 != "" ? "<TR><TD ALIGN=\"CENTER\">" + (PRDESC != "" ? PRDESC : (rnds2 != "" ? r2 : "Sjukhusets")).Replace("[x]","") + " värde inom parentes." + (rr != 0 ? " Förändring från " + r2 + " till " + r1 + "." : "") + "</TD></TR>" : "")); break;
						case 2: sb.Append((showTotal && (units != "" || aids != "") || rnds2 != "" ? "<TR><TD ALIGN=\"CENTER\">" + (PRDESC != "" ? PRDESC : (rnds2 != "" ? r2 : "Hospital")).Replace("[x]","") + " value in parenthesis." + (rr != 0 ? " Change from " + r2 + " to " + r1 + "." : "") + "</TD></TR>" : "")); break;
					}
					sb.Append("</TABLE>");
					sb.Append("</div>");
					cx++;
				}
				rs.Close();
				#endregion

				#region T=0
				rs = Db.recordSet("SELECT sq.QuestionID, qo.OptionID, ql.Question, ISNULL(ql.QuestionArea,'/.../') " +
					"FROM SurveyQuestion sq " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + LID + " " +
					"WHERE sq.QuestionID IN (" + (unitCategorySet ? "455,456,457,458," : "") + "446,447,448,449,537" +
					"" + (projectRoundID != 136 ? "" +
					// Mobbing
					",452,2447,2449,2450" +
					"" : "" +
					// ABB CR mobbing
					",2765,2767,2768,2769" +
					"") +
					// SSM likert
					",2454,2459,2617,2619,2621,2688,2623,2625,2627,2629,2590,2685,2687" +
					// Unionen likert
					",2820,2821,2822,2823,2824,2825,2826,2827,2828,2829,2830,2831,2832,2833,2834" +
					//",2470,2473" +
					") AND sq.SurveyID = " + surveyID + " " +
					"ORDER BY sq.SortOrder");
				while(rs.Read())
				{
					int u2c = (rs.GetInt32(0) == 455 || rs.GetInt32(0) == 456 || rs.GetInt32(0) == 457 || rs.GetInt32(0) == 458 ? 1 : 0);
					string u2 = (u2c != 0 ? (PRDESC2 != "" ? PRDESC2 : (PRDESC != "" ? PRDESC : "")) : "");

					string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
					System.IO.FileStream fs = createStream(path,file);
					feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 0, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, rs.GetInt32(0), false, "", "", rs.GetInt32(1), false, u2c, u2,percent,0);
					fs.Close();

					sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
					sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>" + rs.GetString(3) + "</p></div>");
					sb.Append("<div class=\"eform_ques\">");
					sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
					sb.Append("<TR><TD><B>" + rs.GetString(2) + "</B></TD></TR>");
					sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
					//sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=0&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1) + "&U=" + units + "" + (rs.GetInt32(0) == 455 || rs.GetInt32(0) == 456 || rs.GetInt32(0) == 457 || rs.GetInt32(0) == 458 ? "&U2C=1&U2=" + (PRDESC2 != "" ? PRDESC2.Replace("&","_0_").Replace("#","_1_") : (PRDESC != "" ? PRDESC.Replace("&","_0_").Replace("#","_1_") : "")) : "") + "\"></TD></TR>");
					sb.Append("</TABLE>");
					sb.Append("</div>");
					cx++;
					//FeedbackText.Text += "<b style=\"font-size:16px;\">" + rs.GetString(2) + "</b><br/><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (aids != "" ? "AIDS=" + aids + "&" : "") + "R=" + projectRoundID + "&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1) + "&U=" + units + "\"><BR/><BR/>";
				}
				rs.Close();
				#endregion

				#region T=8
				rs = Db.recordSet("SELECT sq.QuestionID, qo.OptionID, ql.Question, ISNULL(ql.QuestionArea,'/.../') " +
					"FROM SurveyQuestion sq " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + LID + " " +
					"WHERE sq.QuestionID IN (" +
					// SSM VAS
					"2596,2601,2606,2608,2614,2612" +
					//",2460,2462,2463,2465,2466,2467,2468,2469" +

					",3865,3866,3867,3868,3869,3870,3871,3872,3873,3874" +	// unionen extra

					") AND sq.SurveyID = " + surveyID + " " +
					"ORDER BY sq.SortOrder");
				while(rs.Read())
				{
					string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
					System.IO.FileStream fs = createStream(path,file);
					feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 8, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, rs.GetInt32(0), false, "", "", rs.GetInt32(1), false, 0, "",percent,0);
					fs.Close();

					sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
					sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>" + rs.GetString(3) + "</p></div>");
					sb.Append("<div class=\"eform_ques\">");
					sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
					sb.Append("<TR><TD><B>" + rs.GetString(2) + "</B></TD></TR>");
					sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
					//sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + (aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + (depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + (unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + (AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + (PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=8&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1) + "&U=" + units + "" + (rs.GetInt32(0) == 455 || rs.GetInt32(0) == 456 || rs.GetInt32(0) == 457 || rs.GetInt32(0) == 458 ? "&U2C=1&U2=" + (PRDESC2 != "" ? PRDESC2.Replace("&","_0_").Replace("#","_1_") : (PRDESC != "" ? PRDESC.Replace("&","_0_").Replace("#","_1_") : "")) : "") + "\"></TD></TR>");
					sb.Append("</TABLE>");
					sb.Append("</div>");
					cx++;
					//FeedbackText.Text += "<b style=\"font-size:16px;\">" + rs.GetString(2) + "</b><br/><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (aids != "" ? "AIDS=" + aids + "&" : "") + "R=" + projectRoundID + "&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1) + "&U=" + units + "\"><BR/><BR/>";
				}
				rs.Close();
				#endregion

				#region T=0
				rs = Db.recordSet("SELECT sq.QuestionID, qo.OptionID, ql.Question, ISNULL(ql.QuestionArea,'/.../') " +
					"FROM SurveyQuestion sq " +
					"INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
					"INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
					"INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + LID + " " +
					"WHERE sq.QuestionID IN (" +
					"3830" +
					",3831" +
					",3832" +
					",3833" +
					",3834" +
					",3835" +
					",3836" +
					",3837" +
					",3838" +
					",3839" +
					",3841" +
					",2843" +
					",3844" +
					") AND sq.SurveyID = " + surveyID + " " +
					"ORDER BY sq.SortOrder");
				while(rs.Read())
				{
					string file = rs.GetInt32(0) + "_" + DateTime.UtcNow.Ticks + ".gif";
					System.IO.FileStream fs = createStream(path,file);
					feedbackImage.exec(groupID,fs, LID, showN, fn, PRDESC, unitDesc, AID1, AID1txt, AID2, AID2txt, 0, projectRoundID, rr, r1, r2, rnds1, rnds2, depts1, depts2, rnds, aids, units, units2, RAC, showTotal, false, false, false, BGCOLOR, rs.GetInt32(0), false, "", "", rs.GetInt32(1), false, 0, "",percent,0);
					fs.Close();

					sb.Append("<br class=\"noprint\"/><br class=\"noprint\"/>");
					sb.Append("<div " + (cx > 0 && cx % 2 == 0 ? "style=\"page-break-before:always;\" " : "") + "class=\"eform_area\"><p>" + rs.GetString(3) + "</p></div>");
					sb.Append("<div class=\"eform_ques\">");
					sb.Append("<TABLE class=\"eform_ques_outer\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">");
					sb.Append("<TR><TD><B>" + rs.GetString(2) + "</B></TD></TR>");
					//sb.Append("<TR><TD><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (showN ? "ShowN=1&" : "") + 
					//	(aids != "" ? "AIDS=" + aids + "&" : "") + "RAC=" + RAC + "&LID=" + LID + (rnds != "" ? "&RNDS=" + rnds : "") + (rnds1 != "" ? "&RNDS1=" + rnds1 : "") + (rnds2 != "" ? "&RNDS2=" + rnds2 : "") + 
					//	(depts1 != "" ? "&DEPTS1=" + depts1 : "") + (depts2 != "" ? "&DEPTS2=" + depts2 : "") + "&R1=" + r1 + "&R2=" + r2 + "&" + (rr != 0 ? "RRU=" + units2 + "&RR=" + rr + "&" : "") + 
					//	(unitDesc != "" ? "UNITDESC=" + unitDesc.Replace("&","_0_").Replace("#","_1_") + "&" : "") + (!showTotal ? "ST=0&" : "") + (BGCOLOR != "" ? "BGCOLOR=" + BGCOLOR + "&" : "") + 
					//	(AID1 != 0 ? "AID1=" + AID1 + "&AID1txt=" + AID1txt + "&" : "") + (AID2 != 0 ? "AID2=" + AID2 + "&AID2txt=" + AID2txt + "&" : "") + 
					//	(PRDESC != "" ? "PRDESC=" + PRDESC.Replace("&","_0_").Replace("#","_1_") + "&" : "") + "Rnd=" + rnd.Next() + "&R=" + projectRoundID + "&T=0&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1) + 
					//	"&U=" + units + "\"></TD></TR>");
					sb.Append("<TR><TD><img src=\"/tmp/" + path + "/" + file + "\"></TD></TR>");
					sb.Append("</TABLE>");
					sb.Append("</div>");
					//FeedbackText.Text += "<b style=\"font-size:16px;\">" + rs.GetString(2) + "</b><br/><img src=\"/feedbackImage.aspx?fn=" + fn + "&" + (aids != "" ? "AIDS=" + aids + "&" : "") + "R=" + projectRoundID + "&Q=" + rs.GetInt32(0) + "&O=" + rs.GetInt32(1) + "&U=" + units + "\"><BR/><BR/>";
					cx++;
				}
				rs.Close();
				#endregion
			}

			return sb.ToString() + (show3x3 ? "<br class=\"noprint\"/><br class=\"noprint\"/><div class=\"noprint\"><a target=\"_blank\" href=\"downloadPDF.aspx?R=" + fn + ".txt\">3x3 questions - table</a></div>" : "");
		}
		private static System.IO.FileStream createStream(string path, string file)
		{
			string p = HttpContext.Current.Server.MapPath("tmp") + "\\" + path;
			if(!System.IO.Directory.Exists(p))
			{
				System.IO.Directory.CreateDirectory(p);
			}

			return (new System.IO.FileStream(p + "\\" + file, System.IO.FileMode.Create));
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			string FBRK = "";
			int userCount = 0, answerCount = 0;

			//HttpContext.Current.Request.ContentEncoding = System.Text.UTF8Encoding.UTF8;
			//HttpContext.Current.Response.Charset = "utf-8";
			//<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">

			if(!IsPostBack)
			{
				int groupID = (HttpContext.Current.Request.QueryString["GroupID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["GroupID"]) : 0);

				bool showTotal = (HttpContext.Current.Request.QueryString["ST"] == null || HttpContext.Current.Request.QueryString["ST"] != "0");

				int AID1 = (HttpContext.Current.Request.QueryString["AID1"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["AID1"]) : 0);
				int AID2 = (HttpContext.Current.Request.QueryString["AID2"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["AID2"]) : 0);
				string AID1txt = (HttpContext.Current.Request.QueryString["AID1txt"] != null ? HttpContext.Current.Request.QueryString["AID1txt"] : (AID1 != 0 ? "Mitt svar" : ""));
				string AID2txt = (HttpContext.Current.Request.QueryString["AID2txt"] != null ? HttpContext.Current.Request.QueryString["AID2txt"] : (AID2 != 0 ? "Mitt svar" : ""));

				rac = (HttpContext.Current.Request.QueryString["RAC"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["RAC"]) : 10);
				LID = (HttpContext.Current.Request.QueryString["LID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["LID"]) : 1);
				surveyID = (HttpContext.Current.Request.QueryString["SID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["SID"]) : 0);
				surveyName = (HttpContext.Current.Request.QueryString["SN"] != null ? HttpContext.Current.Request.QueryString["SN"] : "");
				userKey = (HttpContext.Current.Request.QueryString["K"] != null ? HttpContext.Current.Request.QueryString["K"] : "");
				r = (HttpContext.Current.Request.QueryString["R"] != null ? HttpContext.Current.Request.QueryString["R"] : "");
				rr = (HttpContext.Current.Request.QueryString["RR"] != null ? HttpContext.Current.Request.QueryString["RR"] : "0");
				r1 = (HttpContext.Current.Request.QueryString["R1"] != null ? HttpContext.Current.Request.QueryString["R1"] : "");
				r2 = (HttpContext.Current.Request.QueryString["R2"] != null ? HttpContext.Current.Request.QueryString["R2"] : "");

				rnds1 = (HttpContext.Current.Request.QueryString["RNDS1"] != null ? HttpContext.Current.Request.QueryString["RNDS1"] : "");
				rnds2 = (HttpContext.Current.Request.QueryString["RNDS2"] != null ? HttpContext.Current.Request.QueryString["RNDS2"] : "");
				depts1 = (HttpContext.Current.Request.QueryString["DEPTS1"] != null ? HttpContext.Current.Request.QueryString["DEPTS1"] : "");
				depts2 = (HttpContext.Current.Request.QueryString["DEPTS2"] != null ? HttpContext.Current.Request.QueryString["DEPTS2"] : "");
				rnds = (HttpContext.Current.Request.QueryString["RNDS"] != null ? HttpContext.Current.Request.QueryString["RNDS"] : "");

				aids = (HttpContext.Current.Request.QueryString["AIDS"] != null ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["AIDS"]) : "");

				units = (HttpContext.Current.Request.QueryString["U"] != null ? HttpContext.Current.Request.QueryString["U"] : "");
				units2 = (HttpContext.Current.Request.QueryString["RRU"] != null ? HttpContext.Current.Request.QueryString["RRU"] : "");
				t = (HttpContext.Current.Request.QueryString["T"] != null ? HttpContext.Current.Request.QueryString["T"] : "");
				ud = (HttpContext.Current.Request.QueryString["UD"] != null ? HttpContext.Current.Request.QueryString["UD"].ToString().Replace("_1_","#").Replace("_0_","&") : "");
				ud = HttpContext.Current.Server.HtmlDecode(ud);

				string BGCOLOR = (HttpContext.Current.Request.QueryString["BGCOLOR"] != null ? HttpContext.Current.Request.QueryString["BGCOLOR"] : "");

				if(HttpContext.Current.Request.QueryString["A"] != null)
				{
					name1 = "Org X";
					name2 = "Org X";
				}
				else if(HttpContext.Current.Request.QueryString["N"] != null)
				{
					// add htmldecode here and encode again on the querystrings above
					name1 = HttpContext.Current.Request.QueryString["N"].ToString().Replace("_1_","#").Replace("_0_","&");
					name2 = HttpContext.Current.Request.QueryString["N"].ToString().Replace("_1_","#").Replace("_0_","&");

					HttpContext.Current.Response.Write("<!--" + name1 + "-->");
				}
				else if(userKey.Length == 16 && t == "2" || r == "30")
				{
					name1 = "Karolinska Universitetssjukhuset[x]";
					name2 = "Karolinska Universitetssjukhuset[x], kliniska enheter";
					r1 = "2008";
					r2 = "2007";
				}
				else if(userKey.Length == 16 && t == "" || r == "20")
				{
					name1 = "KUS 2007";
					name2 = "KUS 2007, kliniska enheter";
				}
				if(rr != "0" && rr != "" && r1 != "" & r2 != "" && name1.IndexOf("[x]") < 0 && name2.IndexOf("[x]") < 0)
				{
					name1 += "[x]";
					name2 += "[x]";

					if(ud != "")
					{
						ud += "[x]";
					}
				}

				int surveyLangCount = 2, feedbackID = 0, compare = 0;

				string userQuery = "(SELECT COUNT(*) FROM ProjectRoundUser u WHERE u.ProjectRoundUnitID = QQ.ProjectRoundUnitID" + 
					(groupID != 0 ? " AND u.GroupID = " + groupID : "") + 
					"), ";
				string answerQuery = "(SELECT COUNT(*) FROM Answer aa" + 
					(groupID != 0 ? " INNER JOIN ProjectRoundUser u ON aa.ProjectRoundUserID = u.ProjectRoundUserID" : "") + 
					" WHERE aa.EndDT IS NOT NULL AND aa.ProjectRoundUnitID = QQ.ProjectRoundUnitID" + 
					(groupID != 0 ? " AND u.GroupID = " + groupID : "") + 
					") ";

				if(userKey.Length == 16)
				{
					#region Feedback on webbqps..chefkst level
					try
					{
						key1 = userKey.Substring(0,8);
						key2 = userKey.Substring(8);
						units = "";
						string sql = "";
						if(t == "")
						{
							sql = "SELECT " +
								"DISTINCT u.ProjectRoundUnitID, r.SurveyID, l.SurveyName, r.ProjectRoundID, r.ProjectID, u.UnitCategoryID, " +
								"(SELECT TOP 1 rl.SurveyName FROM ProjectRoundLang rl WHERE r.ProjectRoundID = rl.ProjectRoundID) " +
								"FROM webbqps..chefkst c " +
								"INNER JOIN webbqps..chefkst k ON c.PNR = k.PNR " +
								"INNER JOIN ProjectRoundUnit u ON CAST(k.KST AS VARCHAR(64)) = u.ID " +
								"INNER JOIN ProjectRoundUser s ON u.ProjectRoundUnitID = s.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound r ON u.ProjectRoundID = r.ProjectRoundID " +
								"LEFT OUTER JOIN ProjectRoundLang l ON r.ProjectRoundID = l.ProjectRoundID AND l.LangID = " + LID + " " +
								"INNER JOIN Answer a ON s.ProjectRoundUserID = a.ProjectRoundUserID " +
								"WHERE LEFT(CAST(c.UKEY AS VARCHAR(64)),8) = '" + key1.Replace("'","") + "' " +
								"AND LEFT(CAST(c.UKEY2 AS VARCHAR(64)),8) = '" + key2.Replace("'","") + "' " +
								"AND a.EndDT IS NOT NULL " +
								"AND s.Terminated IS NULL " +
								"AND s.NoSend IS NULL " +
								"AND u.Terminated IS NULL " +
								"AND u.ProjectRoundID = 20 " +
								"AND s.ProjectRoundID = 20 " +
								"AND r.ProjectRoundID = 20";
						}
						else if(t == "2")
						{
							sql = "SELECT " +
								"DISTINCT u.ProjectRoundUnitID, r.SurveyID, l.SurveyName, r.ProjectRoundID, r.ProjectID, u.UnitCategoryID, " +
								"(SELECT TOP 1 rl.SurveyName FROM ProjectRoundLang rl WHERE r.ProjectRoundID = rl.ProjectRoundID) " +
								"FROM webbQPS4..chefer c " +
								"INNER JOIN webbQPS4..chefKST k ON c.email = k.email " +
								"INNER JOIN ProjectRoundUnit u ON k.KST = u.ID " +
								"INNER JOIN ProjectRoundUser s ON u.ProjectRoundUnitID = s.ProjectRoundUnitID " +
								"INNER JOIN ProjectRound r ON u.ProjectRoundID = r.ProjectRoundID " +
								"LEFT OUTER JOIN ProjectRoundLang l ON r.ProjectRoundID = l.ProjectRoundID AND l.LangID = " + LID + " " +
								"INNER JOIN Answer a ON s.ProjectRoundUserID = a.ProjectRoundUserID " +
								"WHERE LEFT(CAST(c.UKEY AS VARCHAR(64)),8) = '" + key1.Replace("'","") + "' " +
								"AND LEFT(CAST(c.UKEY2 AS VARCHAR(64)),8) = '" + key2.Replace("'","") + "' " +
								"AND a.EndDT IS NOT NULL " +
								"AND s.Terminated IS NULL " +
								"AND s.NoSend IS NULL " +
								"AND u.Terminated IS NULL " +
								"AND u.ProjectRoundID = 30 " +
								"AND s.ProjectRoundID = 30 " +
								"AND r.ProjectRoundID = 30";
						}
						//HttpContext.Current.Response.Write(sql);
						rs = Db.recordSet(sql);
						if(rs.Read())
						{
							surveyID = rs.GetInt32(1);
							surveyName = (!rs.IsDBNull(2) ? rs.GetString(2) : rs.GetValue(6).ToString());
							projectRoundID = rs.GetInt32(3);
							projectID = rs.GetInt32(4);
							do
							{
								units += (units == "" ? "" : ",") + rs.GetInt32(0).ToString();
								if(!rs.IsDBNull(5))
								{
									unitCategorySet = true;
								}
							}
							while(rs.Read());
						}
						rs.Close();
						if(units != "")
						{
							rs = Db.recordSet("SELECT COUNT(*), COUNT(a.AnswerID) " +
								"FROM ProjectRoundUser s " +
								"LEFT OUTER JOIN Answer a ON s.ProjectRoundUserID = a.ProjectRoundUserID AND a.EndDT IS NOT NULL " +
								"WHERE s.Terminated IS NULL " +
								"AND s.NoSend IS NULL " +
								"AND s.ProjectRoundUnitID IN (" + units.Replace("'","") + ")");
							if(rs.Read() && (Convert.ToInt32(rs.GetValue(0)) < 8 || Math.Round(Convert.ToDouble(rs.GetValue(1))/Convert.ToDouble(rs.GetValue(0))*100,0) < 50))
							{
								units = "";
							}
							rs.Close();
						}
						if(units == "")
						{
							throw new Exception();
						}
					}
					catch(Exception ex)
					{
						HttpContext.Current.Response.Write(ex.Message);
						FeedbackText.Text = "Ej tillräckligt underlag";
					}

					if(units == "")
					{
						projectRoundID = 0;
					}
					#endregion
				}
				else if(r != "")
				{
					#region Standard

					if(r != "0")
					{
						string sql = "SELECT " +
							"DISTINCT " +
							"NULL, " +
							"r.SurveyID, " +
							"l.SurveyName, " +
							"r.ProjectRoundID, " +
							"r.ProjectID, " +
							"(SELECT COUNT(*) FROM SurveyLang sl WHERE sl.SurveyID = r.SurveyID) AS CX, " +
							"r.AdHocReportCompareWithParent, " +
							"r.FeedbackID, " +
							"f.Compare " +
							"FROM ProjectRound r " +
							"INNER JOIN ProjectRoundLang l ON r.ProjectRoundID = l.ProjectRoundID AND l.LangID = " + LID + " " +
							"LEFT OUTER JOIN Feedback f ON " + (HttpContext.Current.Request.QueryString["FeedbackID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FeedbackID"]).ToString() : "r.FeedbackID") + " = f.FeedbackID " +
							"WHERE r.ProjectRoundID = " + r;
						rs = Db.recordSet(sql);
						if(rs.Read())
						{
							surveyID = rs.GetInt32(1);
							surveyName = rs.GetString(2);
							projectRoundID = rs.GetInt32(3);
							projectID = rs.GetInt32(4);
							surveyLangCount = rs.GetInt32(5);
							if(HttpContext.Current.Request.QueryString["ST"] == null && units != "")
							{
								showTotal = rs.IsDBNull(6);
							}
							feedbackID = (rs.IsDBNull(7) ? 0 : rs.GetInt32(7));
							compare = (rs.IsDBNull(8) ? 0 : rs.GetInt32(8));
						}
						rs.Close();
					}
					else
					{
						// Compare rounds

						rs = Db.recordSet("SELECT " +
							"r.SurveyID, " +
							"l.SurveyName, " +
							"r.ProjectID, " +
							"(SELECT COUNT(*) FROM SurveyLang sl WHERE sl.SurveyID = r.SurveyID) AS CX, " +
							"r.AdHocReportCompareWithParent, " +
							"r.FeedbackID, " +
							"f.Compare " +
							"FROM ProjectRound r " +
							"INNER JOIN ProjectRoundLang l ON r.ProjectRoundID = l.ProjectRoundID AND l.LangID = " + LID + " " +
							"LEFT OUTER JOIN Feedback f ON " + (HttpContext.Current.Request.QueryString["FeedbackID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FeedbackID"]).ToString() : "r.FeedbackID") + " = f.FeedbackID " +
							"WHERE r.ProjectRoundID IN (" + rnds + ") ORDER BY r.ProjectRoundID DESC");
						if(rs.Read())
						{
							surveyID = rs.GetInt32(0);
							surveyName = rs.GetString(1);
							projectRoundID = -1;
							projectID = rs.GetInt32(2);
							surveyLangCount = rs.GetInt32(3);
							if(HttpContext.Current.Request.QueryString["ST"] == null && units != "")
							{
								showTotal = rs.IsDBNull(4);
							}
							feedbackID = (rs.IsDBNull(5) ? 0 : rs.GetInt32(5));
							compare = (rs.IsDBNull(6) ? 0 : rs.GetInt32(6));
						}
						rs.Close();
						
					}
					if(projectRoundID == -1 && compare > 0)
					{
						rs = Db.recordSet("SELECT " +
							"QQ.UserCount, " +
							userQuery +
							answerQuery +
							"FROM ProjectRoundUnit QQ " +
							"WHERE QQ.ProjectRoundID IN (" + rnds + ")");
						while(rs.Read())
						{
							userCount += (rs.IsDBNull(0) || rs.GetInt32(0) == 0 || groupID != 0 ? rs.GetInt32(1) : rs.GetInt32(0));
							answerCount += rs.GetInt32(2);
						}
						rs.Close();
					}
					else if(units != "" && compare > 0)
					{
						rs = Db.recordSet("SELECT " +
							"DISTINCT " +
							"QQ.ProjectRoundUnitID, " +
							"QQ.UserCount, " +
							userQuery +
							answerQuery +
							"FROM ProjectRoundUnit p " +
							"LEFT OUTER JOIN ProjectRoundUnit QQ ON LEFT(QQ.SortString,LEN(p.SortString)) = p.SortString " +
							"WHERE p.ProjectRoundID = " + r + " AND p.ProjectRoundUnitID IN (" + units.Replace("'","") + ")");
						while(rs.Read())
						{
							userCount += (rs.IsDBNull(1) || rs.GetInt32(1) == 0 || groupID != 0 ? rs.GetInt32(2) : rs.GetInt32(1));
							answerCount += rs.GetInt32(3);
						}
						rs.Close();
					}
					else if(units != "" && HttpContext.Current.Request.QueryString["AB"] != null)
					{
						rs = Db.recordSet("SELECT " +
							"DISTINCT " +
							"QQ.ProjectRoundUnitID, " +
							"QQ.UserCount, " +
							userQuery +
							answerQuery +
							"FROM ProjectRoundUnit p " +
							"LEFT OUTER JOIN ProjectRoundUnit QQ ON LEFT(QQ.SortString,LEN(p.SortString)) = p.SortString " +
							"WHERE p.ProjectRoundID = " + r + " AND p.ProjectRoundUnitID IN (" + units.Replace("'","") + ")");
						units = "";
						while(rs.Read())
						{
							units += (units == "" ? "" : ",") + rs.GetInt32(0).ToString();
							userCount += (rs.IsDBNull(1) || rs.GetInt32(1) == 0 || groupID != 0 ? rs.GetInt32(2) : rs.GetInt32(1));
							answerCount += rs.GetInt32(3);
						}
						rs.Close();
					}
					else if(units != "")
					{
						rs = Db.recordSet("SELECT " +
							"DISTINCT " +
							"QQ.ProjectRoundUnitID, " +
							"QQ.UserCount, " +
							userQuery +
							answerQuery +
							"FROM ProjectRoundUnit QQ " +
							"WHERE QQ.ProjectRoundID = " + r + " " +
							"AND QQ.ProjectRoundUnitID IN (" + units.Replace("'","") + ")");
						units = "";
						while(rs.Read())
						{
							units += (units == "" ? "" : ",") + rs.GetInt32(0).ToString();
							userCount += (rs.IsDBNull(1) || rs.GetInt32(1) == 0 || groupID != 0 ? rs.GetInt32(2) : rs.GetInt32(1));
							answerCount += rs.GetInt32(3);
						}
						rs.Close();
					}
					else
					{
						rs = Db.recordSet("SELECT " +
							"QQ.UserCount, " +
							userQuery +
							answerQuery +
							"FROM ProjectRoundUnit QQ " +
							"WHERE QQ.ProjectRoundID = " + r);
						while(rs.Read())
						{
							userCount += (rs.IsDBNull(0) || rs.GetInt32(0) == 0 || groupID != 0 ? rs.GetInt32(1) : rs.GetInt32(0));
							answerCount += rs.GetInt32(2);
						}
						rs.Close();
					}

					unitCategorySet = true;
					#endregion
				}
				else if(rnds1 != "")
				{
					if(rnds1 != "0")
					{
						rs = Db.recordSet("SELECT " +
							"QQ.UserCount, " +
							userQuery +
							answerQuery +
							"FROM ProjectRoundUnit QQ " +
							"WHERE QQ.ProjectRoundID IN (" + rnds1 + ")");
						while(rs.Read())
						{
							userCount += (rs.IsDBNull(0) || rs.GetInt32(0) == 0 || groupID != 0 ? rs.GetInt32(1) : rs.GetInt32(0));
							answerCount += rs.GetInt32(2);
						}
						rs.Close();
					}

					if(rnds2 != "" && rnds2 != "0")
					{
						int tmpUserCount = 0, tmpAnswerCount = 0;

						rs = Db.recordSet("SELECT " +
							"QQ.UserCount, " +
							userQuery +
							answerQuery +
							"FROM ProjectRoundUnit QQ " +
							"WHERE QQ.ProjectRoundID IN (" + rnds2 + ")");
						while(rs.Read())
						{
							tmpUserCount += (rs.IsDBNull(0) || rs.GetInt32(0) == 0 || groupID != 0 ? rs.GetInt32(1) : rs.GetInt32(0));
							tmpAnswerCount += rs.GetInt32(2);
						}
						rs.Close();

						if(userCount == 0 || tmpUserCount < userCount)
						{
							userCount = tmpUserCount;
							answerCount = tmpAnswerCount;
						}
					}
				}

				if(HttpContext.Current.Request.QueryString["FeedbackID"] != null)
				{
					feedbackID = Convert.ToInt32(HttpContext.Current.Request.QueryString["FeedbackID"]);
				}
				if(feedbackID != 0)
				{
					Db.sqlExecute("INSERT INTO FeedbackRun (FeedbackID,Total,Answer) VALUES (" + feedbackID + "," + userCount + "," + answerCount + ")");
				}
				else
				{
					Db.sqlExecute("INSERT INTO FeedbackRun (Total,Answer) VALUES (" + userCount + "," + answerCount + ")");
				}
				if(answerCount >= rac)
				{
					SqlDataReader rs2 = Db.sqlRecordSet("SELECT TOP 1 FeedbackRunKey FROM FeedbackRun ORDER BY FeedbackRunID DESC");
					if(rs2.Read())
					{
						FBRK = rs2.GetGuid(0).ToString();

						Lang.Text = 
							(feedbackID != 0 ? "<a href=\"http://export.eform.se/powerpointExport.aspx" + HttpContext.Current.Request.Url.Query + "&FBRK=" + FBRK + "&Host=" + HttpContext.Current.Request.Url.Host + "&Secure=" + (HttpContext.Current.Request.IsSecureConnection ? 1 : 0) + "\"><img src=\"submitImages/button_powerpoint.gif\" border=\"0\"/></a>" : "") + 
							(groupID == 0 && projectRoundID != 0 ? "<a href=\"http://export.eform.se/excelExport.aspx" + HttpContext.Current.Request.Url.Query + "&FBRK=" + FBRK + "&Host=" + HttpContext.Current.Request.Url.Host + "&Secure=" + (HttpContext.Current.Request.IsSecureConnection ? 1 : 0) + "\"><img src=\"submitImages/button_excel.gif\" border=\"0\"/></a>" : "");
					}
					rs2.Close();
				}
				if(surveyLangCount > 1)
				{
					string url = HttpContext.Current.Request.Url.PathAndQuery;
					switch(LID)
					{
						case 1: 
							url = url.Replace("?LID=1","?LID=2");
							if(url.IndexOf("?LID=2") < 0)
							{
								url = url.Replace("aspx?","aspx?LID=2&");
							}
							Lang.Text += "<a href=\"" + url + "\"><img src=\"submitImages/button_lang_1.gif\" border=\"0\"/></a>";
							break;
						case 2: 
							url = url.Replace("?LID=2","?LID=1");
							if(url.IndexOf("?LID=1") < 0)
							{
								url = url.Replace("aspx?","aspx?LID=1&");
							}
							Lang.Text += "<a href=\"" + url + "\"><img src=\"submitImages/button_lang_2.gif\" border=\"0\"/></a>";
							break;
					}
				}

				if(projectRoundID == 0 && HttpContext.Current.Request.QueryString["ALL"] == null && HttpContext.Current.Request.QueryString["RNDS1"] == null)
				{
					surveyName = "Ingen återkoppling!";
					FeedbackText.Text = "&nbsp;&nbsp;&nbsp;&nbsp;För få deltagare (&lt;8) alt. för få svarande (&lt;50%).";
				}
				else
				{
					if(rr != "0" && units != "")
					{
						string baseUnits = (HttpContext.Current.Request.QueryString["U"] != null ? HttpContext.Current.Request.QueryString["U"] : "");

						if(baseUnits != "")
						{
							units2 = "";
							rs = Db.recordSet("SELECT DISTINCT p3.ProjectRoundUnitID " +
								"FROM ProjectRoundUnit p " +
								"INNER JOIN ProjectRoundUnit p2 ON p.ID = p2.ID " +
								"INNER JOIN ProjectRoundUnit p3 ON LEFT(p3.SortString,LEN(p.SortString)) = p.SortString " +
								"WHERE p.ProjectRoundID = " + Convert.ToInt32(rr) + " " +
								"AND p2.ProjectRoundUnitID IN (" + baseUnits.Replace("'","") + ")");
							while(rs.Read())
							{
								units2 += (units2 == "" ? "" : ",") + rs.GetInt32(0).ToString();
							}
							rs.Close();
						}
					}
					FeedbackText.Text += renderFeedback(
						LID,
						surveyID,
						name1,
						name2,
						projectRoundID,
						units,
						unitCategorySet,
						AID1,
						AID2,
						AID1txt,
						AID2txt,
						BGCOLOR,
						showTotal, 
						ud,
						r1,
						r2,
						(rr != "" ? Convert.ToInt32(rr) : 0),
						units2,
						rnds1,
						rnds2,
						rnds,
						rac,
						depts1,
						depts2,
						aids,
						(HttpContext.Current.Request.QueryString["ShowN"] != null),
						feedbackID,
						(HttpContext.Current.Request.QueryString["Percent"] == null || Convert.ToInt32(HttpContext.Current.Request.QueryString["Percent"]) == 1),
						groupID,
						(HttpContext.Current.Request.QueryString["ExtraQS"] == null ? "" : HttpContext.Current.Request.QueryString["ExtraQS"].Replace("(","&").Replace(")","="))
						);
				}
			}
			if(HttpContext.Current.Request.QueryString["OnlyFBRK"] != null)
			{
				HttpContext.Current.Response.Clear();
				HttpContext.Current.Response.ClearHeaders();
				HttpContext.Current.Response.Charset = "UTF-8";
				HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
				HttpContext.Current.Response.ContentType = "text/plain";
				HttpContext.Current.Response.Write(FBRK + "," + (answerCount > userCount ? answerCount : userCount) + "," + answerCount);
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.End();
			}
		}

		protected string printSurveyName()
		{
			return surveyName;
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
