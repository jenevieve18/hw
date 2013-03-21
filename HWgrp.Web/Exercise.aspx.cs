using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Repositories;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HWgrp.Web
{
    public partial class Exercise : System.Web.UI.Page
    {
//        int AX = 0;
		protected int BX = 0;
		protected int LID = 0;
		protected IList<ExerciseAreaLanguage> areas;
		protected IList<ExerciseCategoryLanguage> categories;
		protected IList<HW.Core.Models.Exercise> exercises;
		IExerciseRepository exerciseRepository = AppContext.GetRepositoryFactory().CreateExerciseRepository();

		protected void Page_Load(object sender, EventArgs e)
		{
//			if (Convert.ToInt32(Session["SponsorID"]) == 0)
//			{
//				Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
//			}
			HtmlHelper.RedirectIf(Convert.ToInt32(Session["SponsorID"]) == 0, "default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next());
			
			if (Request.QueryString["LID"] != null)
			{
				LID = Convert.ToInt32(Request.QueryString["LID"]);
			}
			int EAID = (Request.QueryString["EAID"] != null ? Convert.ToInt32(Request.QueryString["EAID"]) : 0);
			int ECID = (Request.QueryString["ECID"] != null ? Convert.ToInt32(Request.QueryString["ECID"]) : 0);
			int SORT = (Request.QueryString["SORT"] != null ? Convert.ToInt32(Request.QueryString["SORT"]) : 0);
			string sortQS = "&SORT=" + SORT;

//			StringBuilder sb = new StringBuilder();
//			int rExerciseAreaID = 0, rExerciseID = 0;
//
			if (!IsPostBack)
			{
//				if (EAID == 0)
//				{
//					switch (LID)
//					{
//							case 0: AreaID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Visa alla</span></a></dt><dd><ul>")); break;
//							case 1: AreaID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Show all</span></a></dt><dd><ul>")); break;
//					}
//				}
//				string s = "";
				areas = exerciseRepository.FindAreas(EAID, LID);
//				foreach (var a in areas)
//				{
//					if (EAID == a.Area.Id)
//					{
//						AreaID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>" + a.AreaName + "</span></a></dt><dd><ul>"));
//						switch (LID)
//						{
//								case 0: AreaID.Controls.Add(new LiteralControl("<li id=\"EAID0\"><a href=\"exercise.aspx?EAID=0" + sortQS + "#filter\">Visa alla</a></li>")); break;
//								case 1: AreaID.Controls.Add(new LiteralControl("<li id=\"EAID0\"><a href=\"exercise.aspx?EAID=0" + sortQS + "#filter\">Show all</a></li>")); break;
//						}
//					}
//					else
//					{
//						if (s != "")
//						{
//							AreaID.Controls.Add(new LiteralControl("<li" + s));
//						}
//						s = " id=\"EAID" + a.Area.Id + "\"><a href=\"exercise.aspx?EAID=" + a.Area.Id + "" + sortQS + "#filter\">" + a.AreaName + "</a></li>";
//					}
//				}
//				AreaID.Controls.Add(new LiteralControl("<li class=\"last\"" + s));
//				AreaID.Controls.Add(new LiteralControl("</ul></dd>"));
//
//				if (ECID == 0)
//				{
//					switch (LID)
//					{
//						case 0:
//							CategoryID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Visa alla</span></a></dt><dd><ul>"));
//							break;
//						case 1:
//							CategoryID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Show all</span></a></dt><dd><ul>"));
//							break;
//					}
//				}
//				s = "";
				categories = exerciseRepository.FindCategories(EAID, ECID, LID);
//				foreach (var c in categories)
//				{
//					if (ECID == c.Category.Id)
//					{
//						CategoryID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>" + c.CategoryName + "</span></a></dt><dd><ul>"));
//						switch (LID)
//						{
//								case 0: CategoryID.Controls.Add(new LiteralControl("<li id=\"ECID0\"><a href=\"exercise.aspx?ECID=0" + sortQS + (EAID != 0 ? "&EAID=" + EAID : "") + "#filter\">Visa alla</a></li>")); break;
//								case 1: CategoryID.Controls.Add(new LiteralControl("<li id=\"ECID0\"><a href=\"exercise.aspx?ECID=0" + sortQS + (EAID != 0 ? "&EAID=" + EAID : "") + "#filter\">Show all</a></li>")); break;
//						}
//					}
//					else
//					{
//						if (s != "")
//						{
//							CategoryID.Controls.Add(new LiteralControl("<li" + s));
//						}
//						s = " id=\"ECID" + c.Category.Id + "\"><a href=\"exercise.aspx?ECID=" + c.Category.Id + "" + sortQS + (EAID != 0 ? "&EAID=" + EAID : "") + "#filter\">" + c.CategoryName + "</a></li>";
//					}
//					BulletedList1.Items.Add(new ListItem(HtmlHelper.Anchor(c.CategoryName, ""), c.Category.Id.ToString()));
//				}
//				CategoryID.Controls.Add(new LiteralControl("<li class=\"last\"" + s));
//				CategoryID.Controls.Add(new LiteralControl("</ul></dd>"));
			}

			exercises = exerciseRepository.FindByAreaAndCategory(EAID, ECID, LID, SORT);
//			foreach (var l in exercises) // TODO:
//			{
//				if (l.Id != rExerciseID)
//				{
//					BX++;
//					if (AX > 0)
//					{
//						sb.Append("</div><div class=\"bottom\">&nbsp;</div></div><!-- end .detail --> </div><!-- end .item -->");
//					}
//
//					sb.Append("<div class=\"item\"><div class=\"overview\"></div><div class=\"detail\">");
//
//					sb.Append("<div class=\"image\">" + (l.Image != "" ? "<img src=\"" + l.Image + "\" width=\"121\" height=\"100\">" : "") + "</div>");
//
//					// time
//					if (l.CurrentLanguage.Time != "")
//					{
//						sb.Append("<div class=\"time\">" + l.CurrentLanguage.Time + "<span class=\"time-end\"></span></div>");
//					}
//
//					// exercise
//					sb.Append("<div class=\"descriptions\">" + l.CurrentArea.AreaName + (l.CurrentCategory.CategoryName == "" ? "" : " - " + l.CurrentCategory.CategoryName) + "</div><h2>" + l.CurrentLanguage.ExerciseName + "</h2>");
//
//					// teaser
//					if (l.CurrentLanguage.Teaser != "")
//					{
//						sb.Append("<p>" + l.CurrentLanguage.Teaser + "</p>");
//					}
//					sb.Append("<div>");
//				}
//
////				sb.Append("<a class=\"sidearrow\" href=\"JavaScript:void(window.open('" + System.Configuration.ConfigurationSettings.AppSettings["healthWatchURL"] + "/exerciseShow.aspx?SID=" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "&AUID=" + Math.Abs(Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"])) + "&ExerciseVariantLangID=" + l.CurrentVariant.Id + "','EVLID" + l.CurrentVariant.Id + "','scrollbars=yes,resizable=yes,");
//				string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
//				sb.Append("<a class=\"sidearrow\" href=\"JavaScript:void(window.open('" + path + "exerciseShow.aspx?SID=" + Convert.ToInt32(Session["SponsorID"]) + "&AUID=" + Math.Abs(Convert.ToInt32(Session["SponsorAdminID"])) + "&ExerciseVariantLangID=" + l.CurrentVariant.Id + "','EVLID" + l.CurrentVariant.Id + "','scrollbars=yes,resizable=yes,");
//
//				if (l.CurrentVariant.ExerciseWindowX != 0)
//				{
//					sb.Append("width=650,height=580");
//				}
//				else
//				{
//					sb.Append("width=" + l.CurrentVariant.ExerciseWindowX + ",height=" + l.CurrentVariant.ExerciseWindowY);
//				}
//				sb.Append("'));\">" + l.CurrentType.TypeName + (l.CurrentType.SubTypeName != "" ? " (" + l.CurrentType.SubTypeName + ")" : "") + "</a>");
//
//				rExerciseAreaID = l.CurrentArea.Id;
//				rExerciseID = l.Id;
//				AX++;
//			}
//
//			if (AX > 0)
//			{
//				sb.Append("</div><div class=\"bottom\">&nbsp;</div></div><!-- end .detail --> </div><!-- end .item -->");
//			}


			if (!IsPostBack)
			{
//				ExerciseList.Controls.Add(new LiteralControl(sb.ToString()));
//
//				string q = (EAID != 0 ? "&EAID=" + EAID : "") + (ECID != 0 ? "&ECID=" + ECID : "");
//
//				switch (LID)
//				{
//					case 0:
//						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 0 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=0" + q + "#filter\"") + "><span>Slumpmässigt</span></a>"));
//						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 1 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=1" + q + "#filter\"") + "><span>Popularitet</span></a>"));
//						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 2 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=2" + q + "#filter\"") + "><span>Bokstavsordning</span></a>"));
//						break;
//					case 1:
//						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 0 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=0" + q + "#filter\"") + "><span>Random</span></a>"));
//						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 1 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=1" + q + "#filter\"") + "><span>Popularity</span></a>"));
//						Sort.Controls.Add(new LiteralControl("<a" + (SORT == 2 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=2" + q + "#filter\"") + "><span>Alphabethical</span></a>"));
//						break;
//				}
			}
		}
    }
}