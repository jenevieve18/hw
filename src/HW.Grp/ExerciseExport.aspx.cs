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
		ExerciseExporter exporter = new ExerciseExporter();
		SqlExerciseRepository r = new SqlExerciseRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int variantID = ConvertHelper.ToInt32(Request.QueryString["ExerciseVariantLangID"]);
			
//			var ex = r.ReadExerciseVariant(variantID);
			int sponsorAdminExerciseID = ConvertHelper.ToInt32(Request.QueryString["SponsorAdminExerciseID"]);
			var sae = r.ReadSponsorAdminExercise(sponsorAdminExerciseID);
			var ex = sae.ExerciseVariantLanguage;
			exporter.Export(ex);

			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = exporter.Type;
//			AddHeaderIf(exporter.HasContentDisposition(reportPart.CurrentLanguage.Subject), "content-disposition", exporter.GetContentDisposition(reportPart.CurrentLanguage.Subject));
			AddHeaderIf(exporter.HasContentDisposition(""), "content-disposition", exporter.GetContentDisposition(""));
			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;

//			string url = GetReportImageUrl(path, langID, yearFrom, yearTo, spons, sid, gb, reportPart.Id, pruid, gid, grpng, plot, monthFrom, monthTo);
//			Write(exporter.Export(url, langID, pruid, yearFrom, yearTo, gb, plot, grpng, spons, sid, gid, sponsor.MinUserCountToDisclose, monthFrom, monthTo));
			Write(exporter.Export(ex));
		}
		
		void Write(object obj)
		{
			if (obj is MemoryStream) {
				Response.BinaryWrite(((MemoryStream)obj).ToArray());
				Response.End();
			} else if (obj is string) {
				Response.Write((string)obj);
			}
		}
		
		void AddHeaderIf(bool condition, string name, string value)
		{
			if (condition) {
				Response.AddHeader(name, value);
			}
		}
	}
}