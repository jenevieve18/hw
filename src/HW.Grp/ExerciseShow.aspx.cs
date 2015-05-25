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
		protected int LID = 2;
		protected ExerciseVariantLanguage evl;
		protected int EVLID;
		protected int SID;

		ISponsorRepository sr;
		IExerciseRepository er;
		
		public ExerciseShow() : this(new SqlSponsorRepository(), new SqlExerciseRepository())
		{
		}
		
		public ExerciseShow(ISponsorRepository sr, IExerciseRepository er)
		{
			this.sr = sr;
			this.er = er;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			LID = ConvertHelper.ToInt32(Session["LID"], ConvertHelper.ToInt32(Request.QueryString["LID"], 2));

			int UID = 0, UPID = 0;
			if (HttpContext.Current.Request.QueryString["AUID"] != null) {
				UID = -Convert.ToInt32(HttpContext.Current.Request.QueryString["AUID"]);
				if (HttpContext.Current.Request.QueryString["SID"] != null) {
					SetSponsor(HW.Core.Helpers.ConvertHelper.ToInt32(Request.QueryString["SID"]));
				}
			} else if(HttpContext.Current.Session["UserID"] != null) {
				UID = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
				if (Convert.ToInt32(Application["SUPERSPONSOR" + Convert.ToInt32(Session["SponsorID"])]) > 0 && Application["SUPERSPONSORHEAD" + Convert.ToInt32(Session["SponsorID"]) + "LANG" + Convert.ToInt32(HttpContext.Current.Session["LID"])] != null) {
					headerText += " - " + Application["SUPERSPONSORHEAD" + Convert.ToInt32(Session["SponsorID"]) + "LANG" + Convert.ToInt32(Session["LID"])];
				}
				if (Convert.ToInt32(Application["SUPERSPONSOR" + Convert.ToInt32(Session["SponsorID"])]) > 0) {
					logos += "<img src='img/partner/" + Convert.ToInt32(Application["SUPERSPONSOR" + Convert.ToInt32(Session["SponsorID"])]) + ".gif'/>";
				}
			}
			if(HttpContext.Current.Session["UserProfileID"] != null) {
				UPID = Convert.ToInt32(HttpContext.Current.Session["UserProfileID"]);
			}

			EVLID = HW.Core.Helpers.ConvertHelper.ToInt32(Request.QueryString["ExerciseVariantLangID"]);
			if (UID == 0 || HttpContext.Current.Request.QueryString["ExerciseVariantLangID"] == null) {
				ClientScript.RegisterStartupScript(this.GetType(), "CLOSE_WINDOW", "<script language='JavaScript'>window.close();</script>");
			} else {
				Show(EVLID, UID, UPID);
			}
		}
		
		
		[WebMethod]
		public static string Save(string[] dataInputs, int sponsorID, int exerciseVariantLangID)
		{
			try {
				var r = new SqlSponsorRepository();
				r.SaveExerciseDataInputs(dataInputs, sponsorID, exerciseVariantLangID);
				return "Exercise Data Inputs Saved.";
			} catch (Exception ex) {
				LoggingService.Error(ex.Message);
				throw ex;
			}
		}
		
		[WebMethod]
		[ScriptMethod(UseHttpGet = true)]
		public static IList<object> Read(int sponsorID, int exerciseVariantLangID)
		{
			try {
				var r = new SqlSponsorRepository();
				var inputs = r.FindSponsorExerciseDataInputs(sponsorID, exerciseVariantLangID);
				var data = new List<object>();
				foreach (var i in inputs) {
					data.Add(new { content = i.Content });
				}
				return data;
			} catch (Exception ex) {
				LoggingService.Error(ex.Message);
				throw ex;
			}
		}
		
		public void Show(int ExerciseVariantLangID, int UID, int UPID)
		{
			if (!IsPostBack) {
				er.SaveStats(ExerciseVariantLangID, UID, UPID);
			}
			evl = er.ReadExerciseVariant(ExerciseVariantLangID);
			if (evl != null) {
				replacementHead = evl.Variant.Exercise.ReplacementHead;
				if (evl.Variant.Type.HasContent() && evl.File != null) {
					Response.Redirect("exercise/" + evl.File, true);
				} else {
					exercise.Controls.Add(GetExerciseTypeControl(evl));
				}
			}
		}
		
		public void SetSponsor(int SID)
		{
			this.SID = SID;
			var s = sr.ReadSponsor3(SID);
			if (s != null) {
				if (s.HasSuperSponsor) {
					logos += "<img src='img/partner/" + s.SuperSponsor.Id + ".gif'/>";
				}
				if (s.HasSuperSponsor && s.SuperSponsor.Languages[0].Header != "") {
					headerText += " - " + s.SuperSponsor.Languages[0].Header;
				}
			}
		}
		
		Control GetExerciseTypeControl(ExerciseVariantLanguage evl)
		{
			if (evl.Variant.Type.IsText()) {
				return new LiteralControl(
					string.Format(
						@"
<style type='text/css'>
    p {{
        margin:1em 0;
    }}
    ul, ol {{
        margin:0 2em;
    }}
</style>
<h1>{0}</h1>
{1}",
						evl.Variant.Exercise.Languages[0].ExerciseName,
						evl.Content
					)
				);
			} else {
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
	}
}