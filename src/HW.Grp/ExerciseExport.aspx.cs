using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Util.Exporters;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;

namespace HW.Grp
{
	public partial class ExerciseExport : System.Web.UI.Page
	{
		PdfExerciseExporter exporter = new PdfExerciseExporter();
        ExerciseService service = new ExerciseService();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int sponsorAdminExerciseId = ConvertHelper.ToInt32(Request.QueryString["SponsorAdminExerciseID"]);
			var sae = service.ReadSponsorAdminExercise(sponsorAdminExerciseId);
//			var evl = sae.ExerciseVariantLanguage;

			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = exporter.Type;
			
			HtmlHelper.AddHeaderIf(exporter.HasContentDisposition(""), "content-disposition", exporter.GetContentDisposition(""), Response);

			string logo = Server.MapPath("~/img/hwlogosmall.gif");
			
			int sponsorId = Convert.ToInt32(Session["SponsorID"]);
			string sponsorLogo = "";
			var s = service.ReadSponsor3(sponsorId);
			if (s != null) {
				if (s.HasSuperSponsor) {
					sponsorLogo = Server.MapPath("~/img/partner/" + s.SuperSponsor.Id + ".gif");
				}
			}
			
//			HtmlHelper.Write(exporter.Export(evl, logo, sponsorLogo), Response);
//			HtmlHelper.Write(Export(evl, logo, sponsorLogo), Response);
			HtmlHelper.Write(Export(sae, logo, sponsorLogo), Response);
		}
		
//		public MemoryStream Export(ExerciseVariantLanguage evl, string logo, string sponsorLogo)
//		{
//			return exporter.Export(evl, logo, sponsorLogo);
//		}
		
		public MemoryStream Export(SponsorAdminExercise sae, string logo, string sponsorLogo)
		{
			return exporter.Export(sae, logo, sponsorLogo);
		}
	}
}