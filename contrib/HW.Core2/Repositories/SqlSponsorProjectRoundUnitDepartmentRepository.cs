using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorProjectRoundUnitDepartmentRepository : BaseSqlRepository<SponsorProjectRoundUnitDepartment>
	{
		public SqlSponsorProjectRoundUnitDepartmentRepository()
		{
		}
		
		public override void Save(SponsorProjectRoundUnitDepartment sponsorProjectRoundUnitDepartment)
		{
			string query = @"
INSERT INTO SponsorProjectRoundUnitDepartment(
	SponsorProjectRoundUnitID, 
	ReportID, 
	DepartmentID, 
	SponsorProjectRoundUnitDepartmentID
)
VALUES(
	@SponsorProjectRoundUnitID, 
	@ReportID, 
	@DepartmentID, 
	@SponsorProjectRoundUnitDepartmentID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectRoundUnitID", sponsorProjectRoundUnitDepartment.SponsorProjectRoundUnitID),
				new SqlParameter("@ReportID", sponsorProjectRoundUnitDepartment.ReportID),
				new SqlParameter("@DepartmentID", sponsorProjectRoundUnitDepartment.DepartmentID),
				new SqlParameter("@SponsorProjectRoundUnitDepartmentID", sponsorProjectRoundUnitDepartment.SponsorProjectRoundUnitDepartmentID)
			);
		}
		
		public override void Update(SponsorProjectRoundUnitDepartment sponsorProjectRoundUnitDepartment, int id)
		{
			string query = @"
UPDATE SponsorProjectRoundUnitDepartment SET
	SponsorProjectRoundUnitID = @SponsorProjectRoundUnitID,
	ReportID = @ReportID,
	DepartmentID = @DepartmentID,
	SponsorProjectRoundUnitDepartmentID = @SponsorProjectRoundUnitDepartmentID
WHERE SponsorProjectRoundUnitDepartmentID = @SponsorProjectRoundUnitDepartmentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectRoundUnitID", sponsorProjectRoundUnitDepartment.SponsorProjectRoundUnitID),
				new SqlParameter("@ReportID", sponsorProjectRoundUnitDepartment.ReportID),
				new SqlParameter("@DepartmentID", sponsorProjectRoundUnitDepartment.DepartmentID),
				new SqlParameter("@SponsorProjectRoundUnitDepartmentID", sponsorProjectRoundUnitDepartment.SponsorProjectRoundUnitDepartmentID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorProjectRoundUnitDepartment
WHERE SponsorProjectRoundUnitDepartmentID = @SponsorProjectRoundUnitDepartmentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectRoundUnitDepartmentID", id)
			);
		}
		
		public override SponsorProjectRoundUnitDepartment Read(int id)
		{
			string query = @"
SELECT 	SponsorProjectRoundUnitID, 
	ReportID, 
	DepartmentID, 
	SponsorProjectRoundUnitDepartmentID
FROM SponsorProjectRoundUnitDepartment
WHERE SponsorProjectRoundUnitDepartmentID = @SponsorProjectRoundUnitDepartmentID";
			SponsorProjectRoundUnitDepartment sponsorProjectRoundUnitDepartment = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorProjectRoundUnitDepartmentID", id))) {
				if (rs.Read()) {
					sponsorProjectRoundUnitDepartment = new SponsorProjectRoundUnitDepartment {
						SponsorProjectRoundUnitID = GetInt32(rs, 0),
						ReportID = GetInt32(rs, 1),
						DepartmentID = GetInt32(rs, 2),
						SponsorProjectRoundUnitDepartmentID = GetInt32(rs, 3)
					};
				}
			}
			return sponsorProjectRoundUnitDepartment;
		}
		
		public override IList<SponsorProjectRoundUnitDepartment> FindAll()
		{
			string query = @"
SELECT 	SponsorProjectRoundUnitID, 
	ReportID, 
	DepartmentID, 
	SponsorProjectRoundUnitDepartmentID
FROM SponsorProjectRoundUnitDepartment";
			var sponsorProjectRoundUnitDepartments = new List<SponsorProjectRoundUnitDepartment>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorProjectRoundUnitDepartments.Add(new SponsorProjectRoundUnitDepartment {
						SponsorProjectRoundUnitID = GetInt32(rs, 0),
						ReportID = GetInt32(rs, 1),
						DepartmentID = GetInt32(rs, 2),
						SponsorProjectRoundUnitDepartmentID = GetInt32(rs, 3)
					});
				}
			}
			return sponsorProjectRoundUnitDepartments;
		}
	}
}
