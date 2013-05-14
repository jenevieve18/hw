using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HWgrp___Old
{
	public partial class exercise : System.Web.UI.Page
	{
		protected int AX = 0, BX = 0, LID = 0;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) == 0)
			{
				HttpContext.Current.Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
			if (HttpContext.Current.Request.QueryString["LID"] != null)
			{
				LID = Convert.ToInt32(HttpContext.Current.Request.QueryString["LID"]);
			}
			int EAID = (HttpContext.Current.Request.QueryString["EAID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["EAID"]) : 0);
			int ECID = (HttpContext.Current.Request.QueryString["ECID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ECID"]) : 0);
			int SORT = (HttpContext.Current.Request.QueryString["SORT"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["SORT"]) : 0);
			string sortQS = "&SORT=" + SORT;

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			SqlDataReader rs;
			int rExerciseAreaID = 0, rExerciseID = 0;

			if (!IsPostBack)
			{
				if (EAID == 0)
				{
					switch (LID)
					{
						case 0: AreaID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Visa alla</span></a></dt><dd><ul>")); break;
						case 1: AreaID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Show all</span></a></dt><dd><ul>")); break;
					}
				}
				string s = "";
				rs = Db.rs("SELECT " +
					 "eal.ExerciseArea, " +          // 0
					 "eal.ExerciseAreaID " +
					 "FROM [ExerciseArea] ea " +
					 "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID " +
					 "WHERE eal.Lang = " + LID + " " +
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
					 "AND e.RequiredUserLevel = 10 " +
					 "AND el.Lang = evl.Lang " +
					 "AND evl.Lang = etl.Lang " +
					 ") > 0 " +
					 "ORDER BY CASE eal.ExerciseAreaID WHEN " + EAID + " THEN NULL ELSE ea.ExerciseAreaSortOrder END");
				while (rs.Read())
				{
					if (EAID == rs.GetInt32(1))
					{
						AreaID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>" + rs.GetString(0) + "</span></a></dt><dd><ul>"));
						switch (LID)
						{
							case 0: AreaID.Controls.Add(new LiteralControl("<li id=\"EAID0\"><a href=\"exercise.aspx?EAID=0" + sortQS + "#filter\">Visa alla</a></li>")); break;
							case 1: AreaID.Controls.Add(new LiteralControl("<li id=\"EAID0\"><a href=\"exercise.aspx?EAID=0" + sortQS + "#filter\">Show all</a></li>")); break;
						}
					}
					else
					{
						if (s != "")
						{
							AreaID.Controls.Add(new LiteralControl("<li" + s));
						}
						s = " id=\"EAID" + rs.GetInt32(1) + "\"><a href=\"exercise.aspx?EAID=" + rs.GetInt32(1) + "" + sortQS + "#filter\">" + rs.GetString(0) + "</a></li>";
					}
				}
				rs.Close();
				AreaID.Controls.Add(new LiteralControl("<li class=\"last\"" + s));
				AreaID.Controls.Add(new LiteralControl("</ul></dd>"));

				if (ECID == 0)
				{
					switch (LID)
					{
						case 0:
							CategoryID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Visa alla</span></a></dt><dd><ul>"));
							break;
						case 1:
							CategoryID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Show all</span></a></dt><dd><ul>"));
							break;
					}
				}
				s = "";
				rs = Db.rs("SELECT " +
					 "eal.ExerciseCategory, " +          // 0
					 "eal.ExerciseCategoryID " +
					 "FROM [ExerciseCategory] ea " +
					 "INNER JOIN [ExerciseCategoryLang] eal ON ea.ExerciseCategoryID = eal.ExerciseCategoryID " +
					 "WHERE eal.Lang = " + LID + " " +
					 "AND (" +
						 "SELECT COUNT(*) " +
						 "FROM Exercise e " +
						 "INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID " +
						 "INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID " +
						 "INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID " +
						 "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
						 "INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID " +
						 "WHERE e.ExerciseCategoryID = ea.ExerciseCategoryID " +
						 (EAID != 0 ? "AND e.ExerciseAreaID = " + EAID + " " : "") +
						 "AND eal.Lang = el.Lang " +
						 "AND e.RequiredUserLevel = 10 " +
						 "AND el.Lang = evl.Lang " +
						 "AND evl.Lang = etl.Lang " +
					 ") > 0 " +
					 "ORDER BY CASE eal.ExerciseCategoryID WHEN " + ECID + " THEN NULL ELSE ea.ExerciseCategorySortOrder END");
				while (rs.Read())
				{
					if (ECID == rs.GetInt32(1))
					{
						CategoryID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>" + rs.GetString(0) + "</span></a></dt><dd><ul>"));
						switch (LID)
						{
							case 0: CategoryID.Controls.Add(new LiteralControl("<li id=\"ECID0\"><a href=\"exercise.aspx?ECID=0" + sortQS + (EAID != 0 ? "&EAID=" + EAID : "") + "#filter\">Visa alla</a></li>")); break;
							case 1: CategoryID.Controls.Add(new LiteralControl("<li id=\"ECID0\"><a href=\"exercise.aspx?ECID=0" + sortQS + (EAID != 0 ? "&EAID=" + EAID : "") + "#filter\">Show all</a></li>")); break;
						}
					}
					else
					{
						if (s != "")
						{
							CategoryID.Controls.Add(new LiteralControl("<li" + s));
						}
						s = " id=\"ECID" + rs.GetInt32(1) + "\"><a href=\"exercise.aspx?ECID=" + rs.GetInt32(1) + "" + sortQS + (EAID != 0 ? "&EAID=" + EAID : "") + "#filter\">" + rs.GetString(0) + "</a></li>";
					}
				}
				rs.Close();
				CategoryID.Controls.Add(new LiteralControl("<li class=\"last\"" + s));
				CategoryID.Controls.Add(new LiteralControl("</ul></dd>"));
			}

			rs = Db.rs("SELECT " +
					"el.New, " +                    // 0
					"NULL, " +
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
					"ecl.ExerciseCategory " +       // 19
					"FROM [ExerciseArea] ea " +
					"INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID " +
					"INNER JOIN [Exercise] e ON ea.ExerciseAreaID = e.ExerciseAreaID " +
					"INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID " +
					"INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID " +
					"INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID " +
					"INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
					"INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID " +
					"LEFT OUTER JOIN [ExerciseCategory] ec ON e.ExerciseCategoryID = ec.ExerciseCategoryID " +
					"LEFT OUTER JOIN [ExerciseCategoryLang] ecl ON ec.ExerciseCategoryID = ecl.ExerciseCategoryID AND ecl.Lang = eal.Lang " +
					"WHERE eal.Lang = el.Lang " +
					"AND e.RequiredUserLevel = 10 " +
					"AND el.Lang = evl.Lang " +
					"AND evl.Lang = etl.Lang " +
					"AND etl.Lang = " + LID + " " +
					(ECID != 0 ? "AND e.ExerciseCategoryID = " + ECID + " " : "") +
					(EAID != 0 ? "AND e.ExerciseAreaID = " + EAID + " " : "") +
					"ORDER BY " +
					(SORT == 1 ? "(SELECT COUNT(*) FROM ExerciseStats esX INNER JOIN ExerciseVariantLang evlX ON esX.ExerciseVariantLangID = evlX.ExerciseVariantLangID INNER JOIN ExerciseVariant evX ON evlX.ExerciseVariantID = evX.ExerciseVariantID WHERE evX.ExerciseID = e.ExerciseID) DESC, " : (SORT == 2 ? "el.Exercise ASC, " : "")) +
					"HASHBYTES('MD2',CAST(RAND(" + DateTime.Now.Second * DateTime.Now.Minute + ")*e.ExerciseID AS VARCHAR(16))) ASC, " +
					"et.ExerciseTypeSortOrder ASC");
			while (rs.Read())
			{
				if (rs.GetInt32(6) != rExerciseID)
				{
					BX++;
					if (AX > 0)
					{
						sb.Append("</div><div class=\"bottom\">&nbsp;</div></div><!-- end .detail --> </div><!-- end .item -->");
					}

					sb.Append("<div class=\"item\"><div class=\"overview\"></div><div class=\"detail\">");

					sb.Append("<div class=\"image\">" + (!rs.IsDBNull(5) && rs.GetString(5) != "" ? "<img src=\"" + rs.GetString(5) + "\" width=\"121\" height=\"100\">" : "") + "</div>");

					// time
					if (!rs.IsDBNull(9) && rs.GetString(9) != "")
					{
						sb.Append("<div class=\"time\">" + rs.GetString(9) + "<span class=\"time-end\"></span></div>");
					}

					// exercise
					sb.Append("<div class=\"descriptions\">" + rs.GetString(3) + (rs.IsDBNull(19) ? "" : " - " + rs.GetString(19)) + "</div><h2>" + rs.GetString(8) + "</h2>");

					// teaser
					if (!rs.IsDBNull(10) && rs.GetString(10) != "")
					{
						sb.Append("<p>" + rs.GetString(10) + "</p>");
					}
					sb.Append("<div>");
				}

				sb.Append("<a class=\"sidearrow\" href=\"JavaScript:void(window.open('" + System.Configuration.ConfigurationSettings.AppSettings["healthWatchURL"] + "/exerciseShow.aspx?SID=" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "&AUID=" + Math.Abs(Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"])) + "&ExerciseVariantLangID=" + rs.GetInt32(2) + "','EVLID" + rs.GetInt32(2) + "','scrollbars=yes,resizable=yes,");

				if (rs.IsDBNull(14))
				{
					sb.Append("width=650,height=580");
				}
				else
				{
					sb.Append("width=" + rs.GetInt32(14) + ",height=" + rs.GetInt32(15));
				}
				sb.Append("'));\">" + rs.GetString(17) + (!rs.IsDBNull(18) && rs.GetString(18) != "" ? " (" + rs.GetString(18) + ")" : "") + "</a>");

				rExerciseAreaID = rs.GetInt32(4);
				rExerciseID = rs.GetInt32(6);
				AX++;
			}
			rs.Close();

			if (AX > 0)
			{
				sb.Append("</div><div class=\"bottom\">&nbsp;</div></div><!-- end .detail --> </div><!-- end .item -->");
			}

			if (!IsPostBack)
			{
				ExerciseList.Controls.Add(new LiteralControl(sb.ToString()));

				string q = (EAID != 0 ? "&EAID=" + EAID : "") + (ECID != 0 ? "&ECID=" + ECID : "");

				switch (LID)
				{
					case 0:
						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 0 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=0" + q + "#filter\"") + "><span>Slumpmässigt</span></a>"));
						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 1 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=1" + q + "#filter\"") + "><span>Popularitet</span></a>"));
						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 2 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=2" + q + "#filter\"") + "><span>Bokstavsordning</span></a>"));
						break;
					case 1:
						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 0 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=0" + q + "#filter\"") + "><span>Random</span></a>"));
						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 1 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=1" + q + "#filter\"") + "><span>Popularity</span></a>"));
						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 2 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=2" + q + "#filter\"") + "><span>Alphabethical</span></a>"));
						break;
				}
			}
		}
	}
}