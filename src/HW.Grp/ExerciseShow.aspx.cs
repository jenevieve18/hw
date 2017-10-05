using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using Newtonsoft.Json;

namespace HW.Grp
{
	public partial class ExerciseShow : System.Web.UI.Page
	{
		protected string replacementHead = "";
		protected string headerText = "";
		protected string logos = "";
		protected int langId = 2;
		protected ExerciseVariantLanguage evl;
		protected int exerciseVariantLangId;
		protected int sponsorId;
		protected int sponsorAdminId;

        SqlSponsorRepository sr = new SqlSponsorRepository();
		SqlExerciseRepository er = new SqlExerciseRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
            var service = new WebService.Soap();
            var service2 = new WebService2.Soap();

            langId = ConvertHelper.ToInt32(Session["LID"], ConvertHelper.ToInt32(Request.QueryString["LID"], 2));
			sponsorId = ConvertHelper.ToInt32(Request.QueryString["SID"]);
			sponsorAdminId = ConvertHelper.ToInt32(Session["SponsorAdminID"]);

			int userId = 0;
			int userProfileId = 0;
			if (HttpContext.Current.Request.QueryString["AUID"] != null) {
				userId = -Convert.ToInt32(HttpContext.Current.Request.QueryString["AUID"]);
				if (HttpContext.Current.Request.QueryString["SID"] != null) {
                    //					SetSponsor(HW.Core.Helpers.ConvertHelper.ToInt32(Request.QueryString["SID"]));
                    SetSponsor(sponsorId);

                    //var sponsor = service2.GetSponsor(Session["Token"].ToString(), sponsorId, 20);
                    //if (sponsor != null)
                    //{
                    //    SetSponsor(sponsorId);
                    //}
                }
			} else if(HttpContext.Current.Session["UserID"] != null) {
				userId = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
				if (Convert.ToInt32(Application["SUPERSPONSOR" + Convert.ToInt32(Session["SponsorID"])]) > 0 && Application["SUPERSPONSORHEAD" + Convert.ToInt32(Session["SponsorID"]) + "LANG" + Convert.ToInt32(HttpContext.Current.Session["LID"])] != null) {
					headerText += " - " + Application["SUPERSPONSORHEAD" + Convert.ToInt32(Session["SponsorID"]) + "LANG" + Convert.ToInt32(Session["LID"])];
				}
				if (Convert.ToInt32(Application["SUPERSPONSOR" + Convert.ToInt32(Session["SponsorID"])]) > 0) {
                    logos += "<img src='img/partner/" + Convert.ToInt32(Application["SUPERSPONSOR" + Convert.ToInt32(Session["SponsorID"])]) + ".gif'/>";
				}
			}
			if(HttpContext.Current.Session["UserProfileID"] != null) {
				userProfileId = Convert.ToInt32(HttpContext.Current.Session["UserProfileID"]);
			}

			exerciseVariantLangId = HW.Core.Helpers.ConvertHelper.ToInt32(Request.QueryString["ExerciseVariantLangID"]);
			if (userId == 0 || HttpContext.Current.Request.QueryString["ExerciseVariantLangID"] == null) {
				ClientScript.RegisterStartupScript(this.GetType(), "CLOSE_WINDOW", "<script language='JavaScript'>window.close();</script>");
			} else {
				if (!IsPostBack) {
                    //er.SaveStats(exerciseVariantLangId, userId, userProfileId);  INSERT EXERCISE STATS
                }
                //Show(er.ReadExerciseVariant(exerciseVariantLangId));
                var exercises = service2.GetExercise(Session["Token"].ToString(), exerciseVariantLangId, 20);
                Show(exercises);
            }
		}

        //		[WebMethod]
        //		public static string SaveOrUpdateSponsorAdminExercise(string[] dataInputs, int sponsorAdminID, int exerciseVariantLangID, int sponsorAdminExerciseID)
        //		{
        //			try {
        //				var r = new SqlSponsorRepository();
        //				if (sponsorAdminExerciseID <= 0) {
        //					r.SaveSponsorAdminExercise(dataInputs, sponsorAdminID, exerciseVariantLangID);
        //				} else {
        //					r.UpdateSponsorAdminExercise(dataInputs, sponsorAdminExerciseID);
        //				}
        //				return "Exercise data for this manager is saved.";
        //			} catch (Exception ex) {
        //				throw ex;
        //			}
        //		}
        //
        //		[WebMethod]
        //		public static string SaveOrUpdateSponsorAdminExercise2(SponsorAdminExerciseDataInput[] dataInputs, int sponsorAdminID, int exerciseVariantLangID, int sponsorAdminExerciseID)
        //		{
        //			try {
        //				var r = new SqlSponsorRepository();
        //				if (sponsorAdminExerciseID <= 0) {
        //					r.SaveSponsorAdminExercise2(dataInputs, sponsorAdminID, exerciseVariantLangID);
        //				} else {
        //					r.UpdateSponsorAdminExercise2(dataInputs, sponsorAdminExerciseID);
        //				}
        //				return "Exercise data for this manager is saved.";
        //			} catch (Exception ex) {
        //				throw ex;
        //			}
        //		}
        //
        //		[WebMethod]
        //		public static string SaveOrUpdateSponsorAdminExercise3(SponsorAdminExerciseDataInput[] dataInputs, int sponsorAdminID, int exerciseVariantLangID, int sponsorAdminExerciseID)
        //		{
        //			try {
        //				var r = new SqlSponsorRepository();
        //				if (sponsorAdminExerciseID <= 0) {
        //					r.SaveSponsorAdminExercise3(dataInputs, sponsorAdminID, exerciseVariantLangID);
        //				} else {
        //					r.UpdateSponsorAdminExercise3(dataInputs, sponsorAdminExerciseID);
        //				}
        //				return "Exercise data for this manager is saved.";
        //			} catch (Exception ex) {
        //				throw ex;
        //			}
        //		}
        //		
        //		[WebMethod]
        //		[ScriptMethod(UseHttpGet = true)]
        //		public static IList<object> FindSponsorAdminExerciseDataInputs(int sponsorAdminExerciseID)
        //		{
        //			try {
        //				var r = new SqlSponsorRepository();
        //				var inputs = r.FindSponsorAdminExerciseDataInputs(sponsorAdminExerciseID);
        //				var data = new List<object>();
        //				foreach (var i in inputs) {
        //					data.Add(
        //						new {
        //							ValueText = i.ValueText,
        //							ValueInt = i.ValueInt,
        //							Type = i.Type
        //						}
        //					);
        //				}
        //				return data;
        //			} catch (Exception ex) {
        //				throw ex;
        //			}
        //		}
        //		
        //		[WebMethod]
        //		[ScriptMethod(UseHttpGet = true)]
        //		public static IList<object> FindSponsorAdminExerciseDataInputs2(int sponsorAdminExerciseID)
        //		{
        //			try {
        //				var r = new SqlSponsorRepository();
        //				var inputs = r.FindSponsorAdminExerciseDataInputs2(sponsorAdminExerciseID);
        //				var data = new List<object>();
        //				foreach (var i in inputs) {
        //					var d = new {
        //                        Id = i.Id,
        //						ValueText = i.ValueText,
        //						ValueInt = i.ValueInt,
        //						Type = i.Type,
        //                        Components = new List<object>()
        //					};
        //                    foreach (var c in i.Components)
        //                    {
        //                        d.Components.Add(new { Id = c.Id, ValueText = c.ValueText, ValueInt = c.ValueInt });
        //                    }
        //					data.Add(d);
        //				}
        //				return data;
        //			} catch (Exception ex) {
        //				throw ex;
        //			}
        //		}

        public void Show(WebService2.ExerciseItem evl)
        {
            if (evl != null)
            {
                replacementHead = evl.ReplacementHead;
                if (evl.HasContent && evl.File != null)
                {
                    Response.Redirect("exercise/" + evl.File, true);
                }
                else
                {
                    exercise.Controls.Add(GetExerciseTypeControl(evl));
                }
            }
        }

        //      public void Show(ExerciseVariantLanguage evl)
        //{
        //	if (evl != null) {
        //		replacementHead = evl.Variant.Exercise.ReplacementHead;
        //		if (evl.Variant.Type.HasContent() && evl.File != null) {
        //			Response.Redirect("exercise/" + evl.File, true);
        //		} else {
        //			exercise.Controls.Add(GetExerciseTypeControl(evl));
        //		}
        //	}
        //}

        public void SetSponsor(int sponsorId)
        {
            var service = new WebService2.Soap();
            var result = service.GetSuperSponsorData(Session["Token"].ToString(), sponsorId, 20);
            if (result != null)
            {
                if (result.HasSuperSponsor)
                {
                    logos += "<img src='img/partner/" + result.SuperSponsorID + ".gif'/>";
                }
                if (result.HasSuperSponsor && result.Header != "")
                {
                    headerText += " - " + result.Header;
                }
            }
        }

        //public void SetSponsor(Sponsor s)
        //{
        //    if (s != null)
        //    {
        //        if (s.HasSuperSponsor)
        //        {
        //            logos += "<img src='img/partner/" + s.SuperSponsor.Id + ".gif'/>";
        //        }
        //        if (s.HasSuperSponsor && s.SuperSponsor.Languages[0].Header != "")
        //        {
        //            headerText += " - " + s.SuperSponsor.Languages[0].Header;
        //        }
        //    }
        //}

        Control GetExerciseTypeControl(WebService2.ExerciseItem evl)
        {
            if (evl.IsText)
            {
                return new LiteralControl(
                    string.Format(
                        @"
<!--<style type='text/css'>
    p {{
        margin:1em 0;
    }}
    ul, ol {{
        margin:0 2em;
    }}
</style>-->
<h1>{0}</h1>
{1}
<script>
{2}
</script>",
                        evl.ExerciseName,
                        evl.Content,
                        evl.Script
                    )
                );
            }
            else
            {
                return new LiteralControl(
                    string.Format(
                        @"
<script type=""text/javascript"">
    AC_FL_RunContent( 'codebase','https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0','width','550','height','400','src','exercise/{0}','quality','high','pluginspage','https://www.macromedia.com/go/getflashplayer','movie','exercise/{0}');
</script>
<noscript>
    <object classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"" width=""550"" height=""400"">
        <param name=""movie"" value=""exercise/{1}"" />
        <param name=""quality"" value=""high"" />
        <embed src=""exercise/{1}"" quality=""high"" pluginspage=""https://www.macromedia.com/go/getflashplayer"" type=""application/x-shockwave-flash"" width=""550"" height=""400""></embed>
    </object>
</noscript>",
                        evl.File.Replace(".swf", ""),
                        evl.File
                    )
                );
            }
        }


//        Control GetExerciseTypeControl(ExerciseVariantLanguage evl)
//		{
//			if (evl.Variant.Type.IsText()) {
//				return new LiteralControl(
//					string.Format(
//						@"
//<!--<style type='text/css'>
//    p {{
//        margin:1em 0;
//    }}
//    ul, ol {{
//        margin:0 2em;
//    }}
//</style>-->
//<h1>{0}</h1>
//{1}
//<script>
//{2}
//</script>",
//						evl.Variant.Exercise.Languages[0].ExerciseName,
//						evl.Content,
//						evl.Variant.Exercise.Script
//					)
//				);
//			} else {
//				return new LiteralControl(
//					string.Format(
//						@"
//<script type=""text/javascript"">
//    AC_FL_RunContent( 'codebase','https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0','width','550','height','400','src','exercise/{0}','quality','high','pluginspage','https://www.macromedia.com/go/getflashplayer','movie','exercise/{0}');
//</script>
//<noscript>
//    <object classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"" width=""550"" height=""400"">
//        <param name=""movie"" value=""exercise/{1}"" />
//        <param name=""quality"" value=""high"" />
//        <embed src=""exercise/{1}"" quality=""high"" pluginspage=""https://www.macromedia.com/go/getflashplayer"" type=""application/x-shockwave-flash"" width=""550"" height=""400""></embed>
//    </object>
//</noscript>",
//						evl.File.Replace(".swf", ""),
//						evl.File
//					)
//				);
//			}
//		}
	}
}