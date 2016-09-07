using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorExtendedSurveyDepartmentRepository : BaseSqlRepository<SponsorExtendedSurveyDepartment>
	{
		public SqlSponsorExtendedSurveyDepartmentRepository()
		{
		}
		
		public override void Save(SponsorExtendedSurveyDepartment sponsorExtendedSurveyDepartment)
		{
			string query = @"
INSERT INTO SponsorExtendedSurveyDepartment(
	SponsorExtendedSurveyDepartmentID, 
	SponsorExtendedSurveyID, 
	DepartmentID, 
	RequiredUserCount, 
	Hide, 
	Ext, 
	Answers, 
	Total
)
VALUES(
	@SponsorExtendedSurveyDepartmentID, 
	@SponsorExtendedSurveyID, 
	@DepartmentID, 
	@RequiredUserCount, 
	@Hide, 
	@Ext, 
	@Answers, 
	@Total
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExtendedSurveyDepartmentID", sponsorExtendedSurveyDepartment.SponsorExtendedSurveyDepartmentID),
				new SqlParameter("@SponsorExtendedSurveyID", sponsorExtendedSurveyDepartment.SponsorExtendedSurveyID),
				new SqlParameter("@DepartmentID", sponsorExtendedSurveyDepartment.DepartmentID),
				new SqlParameter("@RequiredUserCount", sponsorExtendedSurveyDepartment.RequiredUserCount),
				new SqlParameter("@Hide", sponsorExtendedSurveyDepartment.Hide),
				new SqlParameter("@Ext", sponsorExtendedSurveyDepartment.Ext),
				new SqlParameter("@Answers", sponsorExtendedSurveyDepartment.Answers),
				new SqlParameter("@Total", sponsorExtendedSurveyDepartment.Total)
			);
		}
		
		public override void Update(SponsorExtendedSurveyDepartment sponsorExtendedSurveyDepartment, int id)
		{
			string query = @"
UPDATE SponsorExtendedSurveyDepartment SET
	SponsorExtendedSurveyDepartmentID = @SponsorExtendedSurveyDepartmentID,
	SponsorExtendedSurveyID = @SponsorExtendedSurveyID,
	DepartmentID = @DepartmentID,
	RequiredUserCount = @RequiredUserCount,
	Hide = @Hide,
	Ext = @Ext,
	Answers = @Answers,
	Total = @Total
WHERE SponsorExtendedSurveyDepartmentID = @SponsorExtendedSurveyDepartmentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExtendedSurveyDepartmentID", sponsorExtendedSurveyDepartment.SponsorExtendedSurveyDepartmentID),
				new SqlParameter("@SponsorExtendedSurveyID", sponsorExtendedSurveyDepartment.SponsorExtendedSurveyID),
				new SqlParameter("@DepartmentID", sponsorExtendedSurveyDepartment.DepartmentID),
				new SqlParameter("@RequiredUserCount", sponsorExtendedSurveyDepartment.RequiredUserCount),
				new SqlParameter("@Hide", sponsorExtendedSurveyDepartment.Hide),
				new SqlParameter("@Ext", sponsorExtendedSurveyDepartment.Ext),
				new SqlParameter("@Answers", sponsorExtendedSurveyDepartment.Answers),
				new SqlParameter("@Total", sponsorExtendedSurveyDepartment.Total)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorExtendedSurveyDepartment
WHERE SponsorExtendedSurveyDepartmentID = @SponsorExtendedSurveyDepartmentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExtendedSurveyDepartmentID", id)
			);
		}
		
		public override SponsorExtendedSurveyDepartment Read(int id)
		{
			string query = @"
SELECT 	SponsorExtendedSurveyDepartmentID, 
	SponsorExtendedSurveyID, 
	DepartmentID, 
	RequiredUserCount, 
	Hide, 
	Ext, 
	Answers, 
	Total
FROM SponsorExtendedSurveyDepartment
WHERE SponsorExtendedSurveyDepartmentID = @SponsorExtendedSurveyDepartmentID";
			SponsorExtendedSurveyDepartment sponsorExtendedSurveyDepartment = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorExtendedSurveyDepartmentID", id))) {
				if (rs.Read()) {
					sponsorExtendedSurveyDepartment = new SponsorExtendedSurveyDepartment {
						SponsorExtendedSurveyDepartmentID = GetInt32(rs, 0),
						SponsorExtendedSurveyID = GetInt32(rs, 1),
						DepartmentID = GetInt32(rs, 2),
						RequiredUserCount = GetInt32(rs, 3),
						Hide = GetInt32(rs, 4),
						Ext = GetInt32(rs, 5),
						Answers = GetInt32(rs, 6),
						Total = GetInt32(rs, 7)
					};
				}
			}
			return sponsorExtendedSurveyDepartment;
		}
		
		public override IList<SponsorExtendedSurveyDepartment> FindAll()
		{
			string query = @"
SELECT 	SponsorExtendedSurveyDepartmentID, 
	SponsorExtendedSurveyID, 
	DepartmentID, 
	RequiredUserCount, 
	Hide, 
	Ext, 
	Answers, 
	Total
FROM SponsorExtendedSurveyDepartment";
			var sponsorExtendedSurveyDepartments = new List<SponsorExtendedSurveyDepartment>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorExtendedSurveyDepartments.Add(new SponsorExtendedSurveyDepartment {
						SponsorExtendedSurveyDepartmentID = GetInt32(rs, 0),
						SponsorExtendedSurveyID = GetInt32(rs, 1),
						DepartmentID = GetInt32(rs, 2),
						RequiredUserCount = GetInt32(rs, 3),
						Hide = GetInt32(rs, 4),
						Ext = GetInt32(rs, 5),
						Answers = GetInt32(rs, 6),
						Total = GetInt32(rs, 7)
					});
				}
			}
			return sponsorExtendedSurveyDepartments;
		}
	}
}
