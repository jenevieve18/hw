using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlFeedbackTemplatePageRepository : BaseSqlRepository<FeedbackTemplatePage>
	{
		public SqlFeedbackTemplatePageRepository()
		{
		}
		
		public override void Save(FeedbackTemplatePage feedbackTemplatePage)
		{
			string query = @"
INSERT INTO FeedbackTemplatePage(
	FeedbackTemplatePageID, 
	FeedbackTemplateID, 
	Slide, 
	HeaderPH, 
	BottomPH, 
	ImgPos, 
	Description, 
	DoubleImg
)
VALUES(
	@FeedbackTemplatePageID, 
	@FeedbackTemplateID, 
	@Slide, 
	@HeaderPH, 
	@BottomPH, 
	@ImgPos, 
	@Description, 
	@DoubleImg
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackTemplatePageID", feedbackTemplatePage.FeedbackTemplatePageID),
				new SqlParameter("@FeedbackTemplateID", feedbackTemplatePage.FeedbackTemplateID),
				new SqlParameter("@Slide", feedbackTemplatePage.Slide),
				new SqlParameter("@HeaderPH", feedbackTemplatePage.HeaderPH),
				new SqlParameter("@BottomPH", feedbackTemplatePage.BottomPH),
				new SqlParameter("@ImgPos", feedbackTemplatePage.ImgPos),
				new SqlParameter("@Description", feedbackTemplatePage.Description),
				new SqlParameter("@DoubleImg", feedbackTemplatePage.DoubleImg)
			);
		}
		
		public override void Update(FeedbackTemplatePage feedbackTemplatePage, int id)
		{
			string query = @"
UPDATE FeedbackTemplatePage SET
	FeedbackTemplatePageID = @FeedbackTemplatePageID,
	FeedbackTemplateID = @FeedbackTemplateID,
	Slide = @Slide,
	HeaderPH = @HeaderPH,
	BottomPH = @BottomPH,
	ImgPos = @ImgPos,
	Description = @Description,
	DoubleImg = @DoubleImg
WHERE FeedbackTemplatePageID = @FeedbackTemplatePageID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackTemplatePageID", feedbackTemplatePage.FeedbackTemplatePageID),
				new SqlParameter("@FeedbackTemplateID", feedbackTemplatePage.FeedbackTemplateID),
				new SqlParameter("@Slide", feedbackTemplatePage.Slide),
				new SqlParameter("@HeaderPH", feedbackTemplatePage.HeaderPH),
				new SqlParameter("@BottomPH", feedbackTemplatePage.BottomPH),
				new SqlParameter("@ImgPos", feedbackTemplatePage.ImgPos),
				new SqlParameter("@Description", feedbackTemplatePage.Description),
				new SqlParameter("@DoubleImg", feedbackTemplatePage.DoubleImg)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM FeedbackTemplatePage
WHERE FeedbackTemplatePageID = @FeedbackTemplatePageID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackTemplatePageID", id)
			);
		}
		
		public override FeedbackTemplatePage Read(int id)
		{
			string query = @"
SELECT 	FeedbackTemplatePageID, 
	FeedbackTemplateID, 
	Slide, 
	HeaderPH, 
	BottomPH, 
	ImgPos, 
	Description, 
	DoubleImg
FROM FeedbackTemplatePage
WHERE FeedbackTemplatePageID = @FeedbackTemplatePageID";
			FeedbackTemplatePage feedbackTemplatePage = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@FeedbackTemplatePageID", id))) {
				if (rs.Read()) {
					feedbackTemplatePage = new FeedbackTemplatePage {
						FeedbackTemplatePageID = GetInt32(rs, 0),
						FeedbackTemplateID = GetInt32(rs, 1),
						Slide = GetString(rs, 2),
						HeaderPH = GetString(rs, 3),
						BottomPH = GetString(rs, 4),
						ImgPos = GetInt32(rs, 5),
						Description = GetString(rs, 6),
						DoubleImg = GetInt32(rs, 7)
					};
				}
			}
			return feedbackTemplatePage;
		}
		
		public override IList<FeedbackTemplatePage> FindAll()
		{
			string query = @"
SELECT 	FeedbackTemplatePageID, 
	FeedbackTemplateID, 
	Slide, 
	HeaderPH, 
	BottomPH, 
	ImgPos, 
	Description, 
	DoubleImg
FROM FeedbackTemplatePage";
			var feedbackTemplatePages = new List<FeedbackTemplatePage>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					feedbackTemplatePages.Add(new FeedbackTemplatePage {
						FeedbackTemplatePageID = GetInt32(rs, 0),
						FeedbackTemplateID = GetInt32(rs, 1),
						Slide = GetString(rs, 2),
						HeaderPH = GetString(rs, 3),
						BottomPH = GetString(rs, 4),
						ImgPos = GetInt32(rs, 5),
						Description = GetString(rs, 6),
						DoubleImg = GetInt32(rs, 7)
					});
				}
			}
			return feedbackTemplatePages;
		}
	}
}
