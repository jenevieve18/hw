using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlFeedbackTemplateRepository : BaseSqlRepository<FeedbackTemplate>
	{
		public SqlFeedbackTemplateRepository()
		{
		}
		
		public override void Save(FeedbackTemplate feedbackTemplate)
		{
			string query = @"
INSERT INTO FeedbackTemplate(
	FeedbackTemplateID, 
	FeedbackTemplate, 
	OrgPH, 
	DeptPH, 
	DatePH, 
	Slide, 
	DefaultSlide, 
	DefaultHeaderPH, 
	DefaultBottomPH, 
	BG, 
	DefaultImgPos, 
	CountSlide, 
	CountPH, 
	CountTxt, 
	NoFontScale
)
VALUES(
	@FeedbackTemplateID, 
	@FeedbackTemplate, 
	@OrgPH, 
	@DeptPH, 
	@DatePH, 
	@Slide, 
	@DefaultSlide, 
	@DefaultHeaderPH, 
	@DefaultBottomPH, 
	@BG, 
	@DefaultImgPos, 
	@CountSlide, 
	@CountPH, 
	@CountTxt, 
	@NoFontScale
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackTemplateID", feedbackTemplate.FeedbackTemplateID),
				new SqlParameter("@FeedbackTemplate", feedbackTemplate.FeedbackTemplateText),
				new SqlParameter("@OrgPH", feedbackTemplate.OrgPH),
				new SqlParameter("@DeptPH", feedbackTemplate.DeptPH),
				new SqlParameter("@DatePH", feedbackTemplate.DatePH),
				new SqlParameter("@Slide", feedbackTemplate.Slide),
				new SqlParameter("@DefaultSlide", feedbackTemplate.DefaultSlide),
				new SqlParameter("@DefaultHeaderPH", feedbackTemplate.DefaultHeaderPH),
				new SqlParameter("@DefaultBottomPH", feedbackTemplate.DefaultBottomPH),
				new SqlParameter("@BG", feedbackTemplate.BG),
				new SqlParameter("@DefaultImgPos", feedbackTemplate.DefaultImgPos),
				new SqlParameter("@CountSlide", feedbackTemplate.CountSlide),
				new SqlParameter("@CountPH", feedbackTemplate.CountPH),
				new SqlParameter("@CountTxt", feedbackTemplate.CountTxt),
				new SqlParameter("@NoFontScale", feedbackTemplate.NoFontScale)
			);
		}
		
		public override void Update(FeedbackTemplate feedbackTemplate, int id)
		{
			string query = @"
UPDATE FeedbackTemplate SET
	FeedbackTemplateID = @FeedbackTemplateID,
	FeedbackTemplate = @FeedbackTemplate,
	OrgPH = @OrgPH,
	DeptPH = @DeptPH,
	DatePH = @DatePH,
	Slide = @Slide,
	DefaultSlide = @DefaultSlide,
	DefaultHeaderPH = @DefaultHeaderPH,
	DefaultBottomPH = @DefaultBottomPH,
	BG = @BG,
	DefaultImgPos = @DefaultImgPos,
	CountSlide = @CountSlide,
	CountPH = @CountPH,
	CountTxt = @CountTxt,
	NoFontScale = @NoFontScale
WHERE FeedbackTemplateID = @FeedbackTemplateID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackTemplateID", feedbackTemplate.FeedbackTemplateID),
				new SqlParameter("@FeedbackTemplate", feedbackTemplate.FeedbackTemplateText),
				new SqlParameter("@OrgPH", feedbackTemplate.OrgPH),
				new SqlParameter("@DeptPH", feedbackTemplate.DeptPH),
				new SqlParameter("@DatePH", feedbackTemplate.DatePH),
				new SqlParameter("@Slide", feedbackTemplate.Slide),
				new SqlParameter("@DefaultSlide", feedbackTemplate.DefaultSlide),
				new SqlParameter("@DefaultHeaderPH", feedbackTemplate.DefaultHeaderPH),
				new SqlParameter("@DefaultBottomPH", feedbackTemplate.DefaultBottomPH),
				new SqlParameter("@BG", feedbackTemplate.BG),
				new SqlParameter("@DefaultImgPos", feedbackTemplate.DefaultImgPos),
				new SqlParameter("@CountSlide", feedbackTemplate.CountSlide),
				new SqlParameter("@CountPH", feedbackTemplate.CountPH),
				new SqlParameter("@CountTxt", feedbackTemplate.CountTxt),
				new SqlParameter("@NoFontScale", feedbackTemplate.NoFontScale)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM FeedbackTemplate
WHERE FeedbackTemplateID = @FeedbackTemplateID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@FeedbackTemplateID", id)
			);
		}
		
		public override FeedbackTemplate Read(int id)
		{
			string query = @"
SELECT 	FeedbackTemplateID, 
	FeedbackTemplate, 
	OrgPH, 
	DeptPH, 
	DatePH, 
	Slide, 
	DefaultSlide, 
	DefaultHeaderPH, 
	DefaultBottomPH, 
	BG, 
	DefaultImgPos, 
	CountSlide, 
	CountPH, 
	CountTxt, 
	NoFontScale
FROM FeedbackTemplate
WHERE FeedbackTemplateID = @FeedbackTemplateID";
			FeedbackTemplate feedbackTemplate = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@FeedbackTemplateID", id))) {
				if (rs.Read()) {
					feedbackTemplate = new FeedbackTemplate {
						FeedbackTemplateID = GetInt32(rs, 0),
						FeedbackTemplateText = GetString(rs, 1),
						OrgPH = GetString(rs, 2),
						DeptPH = GetString(rs, 3),
						DatePH = GetString(rs, 4),
						Slide = GetString(rs, 5),
						DefaultSlide = GetString(rs, 6),
						DefaultHeaderPH = GetString(rs, 7),
						DefaultBottomPH = GetString(rs, 8),
						BG = GetString(rs, 9),
						DefaultImgPos = GetInt32(rs, 10),
						CountSlide = GetString(rs, 11),
						CountPH = GetString(rs, 12),
						CountTxt = GetString(rs, 13),
						NoFontScale = GetInt32(rs, 14)
					};
				}
			}
			return feedbackTemplate;
		}
		
		public override IList<FeedbackTemplate> FindAll()
		{
			string query = @"
SELECT 	FeedbackTemplateID, 
	FeedbackTemplate, 
	OrgPH, 
	DeptPH, 
	DatePH, 
	Slide, 
	DefaultSlide, 
	DefaultHeaderPH, 
	DefaultBottomPH, 
	BG, 
	DefaultImgPos, 
	CountSlide, 
	CountPH, 
	CountTxt, 
	NoFontScale
FROM FeedbackTemplate";
			var feedbackTemplates = new List<FeedbackTemplate>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					feedbackTemplates.Add(new FeedbackTemplate {
						FeedbackTemplateID = GetInt32(rs, 0),
						FeedbackTemplateText = GetString(rs, 1),
						OrgPH = GetString(rs, 2),
						DeptPH = GetString(rs, 3),
						DatePH = GetString(rs, 4),
						Slide = GetString(rs, 5),
						DefaultSlide = GetString(rs, 6),
						DefaultHeaderPH = GetString(rs, 7),
						DefaultBottomPH = GetString(rs, 8),
						BG = GetString(rs, 9),
						DefaultImgPos = GetInt32(rs, 10),
						CountSlide = GetString(rs, 11),
						CountPH = GetString(rs, 12),
						CountTxt = GetString(rs, 13),
						NoFontScale = GetInt32(rs, 14)
					});
				}
			}
			return feedbackTemplates;
		}
	}
}
