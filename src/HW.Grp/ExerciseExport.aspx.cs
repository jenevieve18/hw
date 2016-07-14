using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class ExerciseExport : System.Web.UI.Page
	{
		PdfExerciseExporter exporter = new PdfExerciseExporter();
		SqlExerciseRepository er = new SqlExerciseRepository();
		SqlSponsorRepository sr = new SqlSponsorRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int sponsorAdminExerciseId = ConvertHelper.ToInt32(Request.QueryString["SponsorAdminExerciseID"]);
			var sae = er.ReadSponsorAdminExercise(sponsorAdminExerciseId);
			var evl = er.ReadExerciseVariant(sae.ExerciseVariantLanguage.Id);

			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = exporter.Type;
			
			HtmlHelper.AddHeaderIf(exporter.HasContentDisposition(""), "content-disposition", exporter.GetContentDisposition(""), Response);

			string logo = Server.MapPath("~/img/hwlogosmall.gif");
			
			int sponsorId = Convert.ToInt32(Session["SponsorID"]);
			string sponsorLogo = "";
			var s = sr.ReadSponsor3(sponsorId);
			if (s != null) {
				if (s.HasSuperSponsor) {
					sponsorLogo = Server.MapPath("~/img/partner/" + s.SuperSponsor.Id + ".gif");
				}
			}
			
			HtmlHelper.Write(exporter.Export(evl, logo, sponsorLogo), Response);
		}
	}
}