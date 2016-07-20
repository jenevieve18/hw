using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
using HW.Core.Services;

namespace HW.Grp
{
	public partial class MyExerciseShow : System.Web.UI.Page
	{
		protected string replacementHead = "";
		protected string headerText = "";
		protected string logos = "";
		protected int langId = 2;
		protected ExerciseVariantLanguage evl;
		protected int exerciseVariantLangId;
		protected int sponsorId;
		protected int sponsorAdminID;
		protected int sponsorAdminExerciseID;

		ExerciseService s = new ExerciseService();
		
		public MyExerciseShow()
		{
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int userId = ConvertHelper.ToInt32If(
				Request.QueryString["AUID"] != null,
				-Convert.ToInt32(Request.QueryString["AUID"]),
				ConvertHelper.ToInt32(Session["UserID"])
			);
			int sponsorId = ConvertHelper.ToInt32If(
				Request.QueryString["AUID"] != null,
				ConvertHelper.ToInt32(Request.QueryString["SID"]),
				ConvertHelper.ToInt32(Session["SponsorID"])
			);
			int superSponsorId = ConvertHelper.ToInt32(Application["SUPERSPONSOR" + sponsorId]);
			object superSponsorLang = Application["SUPERSPONSORHEAD" + sponsorId + "LANG" + langId];
			
			Index(
				ConvertHelper.ToInt32(Session["LID"], ConvertHelper.ToInt32(Request.QueryString["LID"], 2)),
				superSponsorId,
				superSponsorLang,
				sponsorId,
				ConvertHelper.ToInt32(Session["SponsorAdminID"]),
				ConvertHelper.ToInt32(Request.QueryString["SponsorAdminExerciseID"]),
				ConvertHelper.ToInt32(Request.QueryString["ExerciseVariantLangID"]),
				userId,
				Convert.ToInt32(Session["UserProfileID"])
			);
		}
		
		public void Index(int langId, int superSponsorId, object superSponsorLang, int sponsorId, int sponsorAdminID, int sponsorAdminExerciseID, int exerciseVariantLangId, int userId, int userProfileId)
		{
			this.sponsorAdminExerciseID = sponsorAdminExerciseID;
			if (userId < 0) {
				SetSponsor(s.ReadSponsor3(sponsorId));
			} else if (userId > 0) {
				if (superSponsorId > 0 && superSponsorLang != null) {
					headerText += " - " + superSponsorLang;
				}
				if (superSponsorId > 0) {
					logos += "<img src='img/partner/" + superSponsorId + ".gif'/>";
				}
			}

			if (userId == 0 || exerciseVariantLangId == 0) {
				CloseWindow();
			} else {
				if (!IsPostBack) {
					SaveStatistics(exerciseVariantLangId, userId, userProfileId);
				}
				Show(s.ReadSponsorAdminExercise(sponsorAdminExerciseID));
			}
		}
		
		public void CloseWindow()
		{
			ClientScript.RegisterStartupScript(this.GetType(), "CLOSE_WINDOW", "<script language='JavaScript'>window.close();</script>");
		}
		
		public void SaveStatistics(int exerciseVariantLangId, int userId, int userProfileId)
		{
			s.SaveStats(exerciseVariantLangId, userId, userProfileId);
		}

		public void Show(SponsorAdminExercise sae)
		{
			evl = sae.ExerciseVariantLanguage;
			if (evl != null) {
				replacementHead = evl.Variant.Exercise.ReplacementHead;
				if (evl.Variant.Type.HasContent() && evl.File != null) {
					Response.Redirect("exercise/" + evl.File, true);
				} else {
					exercise.Controls.Add(GetExerciseTypeControl(evl));
				}
			}
		}
		
		public void SetSponsor(Sponsor s)
		{
			if (s != null) {
				this.sponsorId = s.Id;
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
{2}
<h1>{0}</h1>
{1}",
						evl.Variant.Exercise.Languages[0].ExerciseName,
						evl.Content,
						evl.Variant.Exercise.Script
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