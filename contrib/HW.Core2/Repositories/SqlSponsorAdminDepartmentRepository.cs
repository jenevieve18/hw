using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorAdminDepartmentRepository : BaseSqlRepository<SponsorAdminDepartment>
	{
		public SqlSponsorAdminDepartmentRepository()
		{
		}
		
		public override void Save(SponsorAdminDepartment sponsorAdminDepartment)
		{
			string query = @"
INSERT INTO SponsorAdminDepartment(
	SponsorAdminDepartmentID, 
	SponsorAdminID, 
	DepartmentID
)
VALUES(
	@SponsorAdminDepartmentID, 
	@SponsorAdminID, 
	@DepartmentID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminDepartmentID", sponsorAdminDepartment.SponsorAdminDepartmentID),
				new SqlParameter("@SponsorAdminID", sponsorAdminDepartment.SponsorAdminID),
				new SqlParameter("@DepartmentID", sponsorAdminDepartment.DepartmentID)
			);
		}
		
		public override void Update(SponsorAdminDepartment sponsorAdminDepartment, int id)
		{
			string query = @"
UPDATE SponsorAdminDepartment SET
	SponsorAdminDepartmentID = @SponsorAdminDepartmentID,
	SponsorAdminID = @SponsorAdminID,
	DepartmentID = @DepartmentID
WHERE SponsorAdminDepartmentID = @SponsorAdminDepartmentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminDepartmentID", sponsorAdminDepartment.SponsorAdminDepartmentID),
				new SqlParameter("@SponsorAdminID", sponsorAdminDepartment.SponsorAdminID),
				new SqlParameter("@DepartmentID", sponsorAdminDepartment.DepartmentID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorAdminDepartment
WHERE SponsorAdminDepartmentID = @SponsorAdminDepartmentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminDepartmentID", id)
			);
		}
		
		public override SponsorAdminDepartment Read(int id)
		{
			string query = @"
SELECT 	SponsorAdminDepartmentID, 
	SponsorAdminID, 
	DepartmentID
FROM SponsorAdminDepartment
WHERE SponsorAdminDepartmentID = @SponsorAdminDepartmentID";
			SponsorAdminDepartment sponsorAdminDepartment = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorAdminDepartmentID", id))) {
				if (rs.Read()) {
					sponsorAdminDepartment = new SponsorAdminDepartment {
						SponsorAdminDepartmentID = GetInt32(rs, 0),
						SponsorAdminID = GetInt32(rs, 1),
						DepartmentID = GetInt32(rs, 2)
					};
				}
			}
			return sponsorAdminDepartment;
		}
		
		public override IList<SponsorAdminDepartment> FindAll()
		{
			string query = @"
SELECT 	SponsorAdminDepartmentID, 
	SponsorAdminID, 
	DepartmentID
FROM SponsorAdminDepartment";
			var sponsorAdminDepartments = new List<SponsorAdminDepartment>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorAdminDepartments.Add(new SponsorAdminDepartment {
						SponsorAdminDepartmentID = GetInt32(rs, 0),
						SponsorAdminID = GetInt32(rs, 1),
						DepartmentID = GetInt32(rs, 2)
					});
				}
			}
			return sponsorAdminDepartments;
		}
	}
}
