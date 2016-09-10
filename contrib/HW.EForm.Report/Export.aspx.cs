using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Repositories;
using HW.EForm.Core.Services;

namespace HW.EForm.Report
{
	public partial class Export : System.Web.UI.Page
	{
//		FeedbackService s = new FeedbackService(new SqlFeedbackRepository(),
//		                                        new SqlFeedbackQuestionRepository(),
//		                                        new SqlQuestionRepository(),
//		                                        new SqlQuestionOptionRepository(),
//		                                        new SqlQuestionLangRepository(),
//		                                        new SqlWeightedQuestionOptionRepository(),
//		                                        new SqlOptionRepository(),
//		                                        new SqlOptionComponentsRepository(),
//		                                        new SqlOptionComponentRepository(),
//		                                        new SqlOptionComponentLangRepository(),
//		                                        new SqlProjectRoundUnitRepository(),
//		                                        new SqlAnswerValueRepository());
		FeedbackService s = ServiceFactory.CreateFeedbackService();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int feedbackID = ConvertHelper.ToInt32(Request.QueryString["FeedbackID"]);
			int projectRoundID = ConvertHelper.ToInt32(Request.QueryString["ProjectRoundID"]);
			int projectRoundUnitID = ConvertHelper.ToInt32(Request.QueryString["ProjectRoundUnitID"]);
			int langID = 1;
			
			var feedback = s.ReadFeedbackWithAnswers(feedbackID, projectRoundID, new int[] { projectRoundUnitID }, langID);
			
//			var exporter = new PromasExporter();
//
//			Response.ClearHeaders();
//			Response.ClearContent();
//			Response.ContentType = exporter.Type;
//			HtmlHelper.AddHeaderIf(exporter.HasContentDisposition("test"), "content-disposition", exporter.GetContentDisposition("test"), Response);
//			string path = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
//
//			HtmlHelper.Write(exporter.Export(feedback), Response);
		}
	}
}