using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Services;

namespace HW.EForm.Report
{
	public partial class Export : System.Web.UI.Page
	{
		FeedbackService s = new FeedbackService();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int feedbackID = ConvertHelper.ToInt32(Request.QueryString["FeedbackID"]);
			int projectRoundID = ConvertHelper.ToInt32(Request.QueryString["ProjectRoundID"]);
			int projectRoundUnitID = ConvertHelper.ToInt32(Request.QueryString["ProjectRoundUnitID"]);
			
			var feedback = s.ReadFeedback2(feedbackID, projectRoundID, new int[] { projectRoundUnitID });
			
			var exporter = new PromasExporter();
			
			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = exporter.Type;
			HtmlHelper.AddHeaderIf(exporter.HasContentDisposition("test"), "content-disposition", exporter.GetContentDisposition("test"), Response);
			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;

			HtmlHelper.Write(exporter.Export(feedback), Response);
		}
	}
}