using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlDepartmentRepository : BaseSqlRepository<Department>
	{
		public SqlDepartmentRepository()
		{
		}
		
		public override void Save(Department department)
		{
			string query = @"
INSERT INTO Department(
	DepartmentID, 
	SponsorID, 
	Department, 
	ParentDepartmentID, 
	SortOrder, 
	SortString, 
	DepartmentShort, 
	DepartmentAnonymized, 
	PreviewExtendedSurveys, 
	MinUserCountToDisclose, 
	LoginDays, 
	LoginWeekday, 
	SortStringLength
)
VALUES(
	@DepartmentID, 
	@SponsorID, 
	@Department, 
	@ParentDepartmentID, 
	@SortOrder, 
	@SortString, 
	@DepartmentShort, 
	@DepartmentAnonymized, 
	@PreviewExtendedSurveys, 
	@MinUserCountToDisclose, 
	@LoginDays, 
	@LoginWeekday, 
	@SortStringLength
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@DepartmentID", department.DepartmentID),
				new SqlParameter("@SponsorID", department.SponsorID),
				new SqlParameter("@Department", department.DepartmentName),
				new SqlParameter("@ParentDepartmentID", department.ParentDepartmentID),
				new SqlParameter("@SortOrder", department.SortOrder),
				new SqlParameter("@SortString", department.SortString),
				new SqlParameter("@DepartmentShort", department.DepartmentShort),
				new SqlParameter("@DepartmentAnonymized", department.DepartmentAnonymized),
				new SqlParameter("@PreviewExtendedSurveys", department.PreviewExtendedSurveys),
				new SqlParameter("@MinUserCountToDisclose", department.MinUserCountToDisclose),
				new SqlParameter("@LoginDays", department.LoginDays),
				new SqlParameter("@LoginWeekday", department.LoginWeekday),
				new SqlParameter("@SortStringLength", department.SortStringLength)
			);
		}
		
		public override void Update(Department department, int id)
		{
			string query = @"
UPDATE Department SET
	DepartmentID = @DepartmentID,
	SponsorID = @SponsorID,
	Department = @Department,
	ParentDepartmentID = @ParentDepartmentID,
	SortOrder = @SortOrder,
	SortString = @SortString,
	DepartmentShort = @DepartmentShort,
	DepartmentAnonymized = @DepartmentAnonymized,
	PreviewExtendedSurveys = @PreviewExtendedSurveys,
	MinUserCountToDisclose = @MinUserCountToDisclose,
	LoginDays = @LoginDays,
	LoginWeekday = @LoginWeekday,
	SortStringLength = @SortStringLength
WHERE DepartmentID = @DepartmentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@DepartmentID", department.DepartmentID),
				new SqlParameter("@SponsorID", department.SponsorID),
				new SqlParameter("@Department", department.DepartmentName),
				new SqlParameter("@ParentDepartmentID", department.ParentDepartmentID),
				new SqlParameter("@SortOrder", department.SortOrder),
				new SqlParameter("@SortString", department.SortString),
				new SqlParameter("@DepartmentShort", department.DepartmentShort),
				new SqlParameter("@DepartmentAnonymized", department.DepartmentAnonymized),
				new SqlParameter("@PreviewExtendedSurveys", department.PreviewExtendedSurveys),
				new SqlParameter("@MinUserCountToDisclose", department.MinUserCountToDisclose),
				new SqlParameter("@LoginDays", department.LoginDays),
				new SqlParameter("@LoginWeekday", department.LoginWeekday),
				new SqlParameter("@SortStringLength", department.SortStringLength)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Department
WHERE DepartmentID = @DepartmentID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@DepartmentID", id)
			);
		}
		
		public override Department Read(int id)
		{
			string query = @"
SELECT 	DepartmentID, 
	SponsorID, 
	Department, 
	ParentDepartmentID, 
	SortOrder, 
	SortString, 
	DepartmentShort, 
	DepartmentAnonymized, 
	PreviewExtendedSurveys, 
	MinUserCountToDisclose, 
	LoginDays, 
	LoginWeekday, 
	SortStringLength
FROM Department
WHERE DepartmentID = @DepartmentID";
			Department department = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@DepartmentID", id))) {
				if (rs.Read()) {
					department = new Department {
						DepartmentID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						DepartmentName = GetString(rs, 2),
						ParentDepartmentID = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4),
						SortString = GetString(rs, 5),
						DepartmentShort = GetString(rs, 6),
						DepartmentAnonymized = GetString(rs, 7),
						PreviewExtendedSurveys = GetInt32(rs, 8),
						MinUserCountToDisclose = GetInt32(rs, 9),
						LoginDays = GetInt32(rs, 10),
						LoginWeekday = GetInt32(rs, 11),
						SortStringLength = GetInt32(rs, 12)
					};
				}
			}
			return department;
		}
		
		public override IList<Department> FindAll()
		{
			string query = @"
SELECT 	DepartmentID, 
	SponsorID, 
	Department, 
	ParentDepartmentID, 
	SortOrder, 
	SortString, 
	DepartmentShort, 
	DepartmentAnonymized, 
	PreviewExtendedSurveys, 
	MinUserCountToDisclose, 
	LoginDays, 
	LoginWeekday, 
	SortStringLength
FROM Department";
			var departments = new List<Department>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					departments.Add(new Department {
						DepartmentID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						DepartmentName = GetString(rs, 2),
						ParentDepartmentID = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4),
						SortString = GetString(rs, 5),
						DepartmentShort = GetString(rs, 6),
						DepartmentAnonymized = GetString(rs, 7),
						PreviewExtendedSurveys = GetInt32(rs, 8),
						MinUserCountToDisclose = GetInt32(rs, 9),
						LoginDays = GetInt32(rs, 10),
						LoginWeekday = GetInt32(rs, 11),
						SortStringLength = GetInt32(rs, 12)
					});
				}
			}
			return departments;
		}
	}
}
