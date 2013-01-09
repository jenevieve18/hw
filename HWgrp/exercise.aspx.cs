//	<file>
//		<license></license>
//		<owner name="Jens Pettersson" email=""/>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;

namespace HWgrp
{
	public partial class exercise : System.Web.UI.Page
	{
		protected int AX = 0, BX = 0, LID = 0;
		IExerciseRepository exerciseRepository = AppContext.GetRepositoryFactory().CreateExerciseRepository();

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
				foreach (var a in exerciseRepository.FindAreas(EAID, LID))
				{
					if (EAID == a.Area.Id)
					{
						AreaID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>" + a.AreaName + "</span></a></dt><dd><ul>"));
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
						s = " id=\"EAID" + a.Area.Id + "\"><a href=\"exercise.aspx?EAID=" + a.Area.Id + "" + sortQS + "#filter\">" + a.AreaName + "</a></li>";
					}
				}
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
				foreach (var c in exerciseRepository.FindCategories(EAID, ECID, LID))
				{
					if (ECID == c.Category.Id)
					{
						CategoryID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>" + c.CategoryName + "</span></a></dt><dd><ul>"));
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
						s = " id=\"ECID" + c.Category.Id + "\"><a href=\"exercise.aspx?ECID=" + c.Category.Id + "" + sortQS + (EAID != 0 ? "&EAID=" + EAID : "") + "#filter\">" + c.CategoryName + "</a></li>";
					}
				}
				CategoryID.Controls.Add(new LiteralControl("<li class=\"last\"" + s));
				CategoryID.Controls.Add(new LiteralControl("</ul></dd>"));
			}

			foreach (var l in exerciseRepository.FindByAreaAndCategory(EAID, ECID, LID, SORT)) // TODO:
			{
				if (l.Id != rExerciseID)
				{
					BX++;
					if (AX > 0)
					{
						sb.Append("</div><div class=\"bottom\">&nbsp;</div></div><!-- end .detail --> </div><!-- end .item -->");
					}

					sb.Append("<div class=\"item\"><div class=\"overview\"></div><div class=\"detail\">");

					sb.Append("<div class=\"image\">" + (l.Image != "" ? "<img src=\"" + l.Image + "\" width=\"121\" height=\"100\">" : "") + "</div>");

					// time
					if (l.CurrentLanguage.Time != "")
					{
						sb.Append("<div class=\"time\">" + l.CurrentLanguage.Time + "<span class=\"time-end\"></span></div>");
					}

					// exercise
					sb.Append("<div class=\"descriptions\">" + l.CurrentArea.AreaName + (l.CurrentCategory.CategoryName == "" ? "" : " - " + l.CurrentCategory.CategoryName) + "</div><h2>" + l.CurrentLanguage.ExerciseName + "</h2>");

					// teaser
					if (l.CurrentLanguage.Teaser != "")
					{
						sb.Append("<p>" + l.CurrentLanguage.Teaser + "</p>");
					}
					sb.Append("<div>");
				}

				sb.Append("<a class=\"sidearrow\" href=\"JavaScript:void(window.open('" + System.Configuration.ConfigurationSettings.AppSettings["healthWatchURL"] + "/exerciseShow.aspx?SID=" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "&AUID=" + Math.Abs(Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"])) + "&ExerciseVariantLangID=" + l.CurrentVariant.Id + "','EVLID" + l.CurrentVariant.Id + "','scrollbars=yes,resizable=yes,");

				if (l.CurrentVariant.ExerciseWindowX != 0)
				{
					sb.Append("width=650,height=580");
				}
				else
				{
					sb.Append("width=" + l.CurrentVariant.ExerciseWindowX + ",height=" + l.CurrentVariant.ExerciseWindowY);
				}
				sb.Append("'));\">" + l.CurrentType.TypeName + (l.CurrentType.SubTypeName != "" ? " (" + l.CurrentType.SubTypeName + ")" : "") + "</a>");

				rExerciseAreaID = l.CurrentArea.Id;
				rExerciseID = l.Id;
				AX++;
			}

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