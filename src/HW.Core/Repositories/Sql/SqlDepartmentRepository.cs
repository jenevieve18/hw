using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlDepartmentRepository : BaseSqlRepository<Department>, IDepartmentRepository
	{
		public override void SaveOrUpdate(Department d)
		{
			string query = string.Format(
				@"
INSERT INTO Department (SponsorID, Department, ParentDepartmentID)
VALUES ({0}, '{1}', {2})",
				d.Sponsor.Id,
				d.Name,
				d.Parent == null ? "null" : d.Parent.Id.ToString()
			);
			Db.exec(query);
		}
		
		public void Save(Department d)
		{
			string query = string.Format(
				@"
INSERT INTO Department (SponsorID, Department, ParentDepartmentID)
VALUES ({0}, '{1}', {2})",
				d.Sponsor.Id,
				d.Name,
				d.Parent == null ? "null" : d.Parent.Id.ToString()
			);
			Db.exec(query);
		}
		
		public void SaveSponsorAdminDepartment(SponsorAdminDepartment d)
		{
			string query = string.Format(
				@"
INSERT INTO SponsorAdminDepartment (SponsorAdminID, DepartmentID)
VALUES ({0}, {1})",
				d.Id, d.Department.Id
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateDepartment(Department d)
		{
			string query = string.Format(
				@"
UPDATE Department SET Department = '{0}',
DepartmentShort = '{1}',
ParentDepartmentID = {2}
WHERE DepartmentID = {3}",
				d.Name,
				d.ShortName,
				d.Parent == null ? "null" : d.Parent.Id.ToString(),
				d.Id
			);
			Db.exec(query);
		}
		
		public void UpdateDepartment2(Department d)
		{
			string query = string.Format(
				@"
UPDATE Department SET DepartmentShort = '{0}',
SortOrder = {1}
WHERE DepartmentID = {2}",
				d.ShortName,
				d.SortOrder,
				d.Id
			);
			Db.exec(query);
		}

		public void UpdateDepartmentSortString(int sponsorID)
		{
			string query = string.Format(
				@"
UPDATE Department SET SortString = dbo.cf_departmentSortString(DepartmentID)
WHERE SponsorID = {0}",
				sponsorID
			);
			Db.exec(query);
		}
		
		public void DeleteSponsorAdminDepartment(int sponsorAdminID, int departmentID)
		{
			string query = string.Format(
				@"
DELETE FROM SponsorAdminDepartment
WHERE DepartmentID = {1} AND SponsorAdminID = {0}",
				sponsorAdminID,
				departmentID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public Department ReadBySponsor(int sponsorId)
		{
			string query = string.Format(
				@"
SELECT DepartmentID
FROM Department
WHERE SponsorID = {0} ORDER BY DepartmentID DESC",
				sponsorId
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					var d = new Department {
						Id = rs.GetInt32(0)
					};
					return d;
				}
			}
			return null;
		}
		
		public int GetLatestDepartmentID(int sponsorID)
		{
			string query = string.Format("SELECT DepartmentID FROM Department WHERE SponsorID = {0} ORDER BY DepartmentID DESC", sponsorID);
			int deptID = 0;
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					deptID = rs.GetInt32(0);
				}
			}
			return deptID;
		}
		
		public override Department Read(int id)
		{
			string query = string.Format(
				@"
SELECT dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
WHERE d.DepartmentID = {0}",
				id
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					var d = new Department {
						TreeName = rs.GetString(0)
					};
					return d;
				}
			}
			return null;
		}
		
		public Department ReadByIdAndSponsor(int departmentID, int sponsorID)
		{
			string query = string.Format(
				@"
SELECT d.SortString,
	d.ParentDepartmentID,
	d.Department,
	d.DepartmentShort
FROM Department d
WHERE d.SponsorID = " + sponsorID + " AND d.DepartmentID = " + departmentID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var d = new Department {
						SortString = rs.GetString(0),
//						Parent = new Department { Id = rs.GetInt32(1) },
						Parent = rs.IsDBNull(1) ? null : new Department { Id = rs.GetInt32(1) },
						Name = rs.GetString(2),
						ShortName = rs.GetString(3)
					};
					return d;
				}
			}
			return null;
		}
		
		public IList<SponsorAdminDepartment> a(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format(
					@"ISNULL(sad.DepartmentID, sa.SuperUser),
	d.DepartmentShort
FROM Department d
INNER JOIN SponsorAdmin sa ON sa.SponsorAdminID = {0}
LEFT OUTER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
AND sad.SponsorAdminID = {0} ",
					sponsorAdminID
				)
				: @"1,
	d.DepartmentShort
FROM Department d ";
			
			string query = string.Format(
				@"
SELECT d.Department,
	dbo.cf_departmentDepth(d.DepartmentID),
	d.DepartmentID,
	(
		SELECT COUNT(*) FROM Department x
		WHERE (x.ParentDepartmentID = d.ParentDepartmentID OR x.ParentDepartmentID IS NULL AND d.ParentDepartmentID IS NULL)
		AND d.SponsorID = x.SponsorID
		AND d.SortString < x.SortString
	),
	{1}
WHERE d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID,
				j
			);
			var departments = new List<SponsorAdminDepartment>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new SponsorAdminDepartment {
//						Admin = new SponsorAdmin { SuperUser = rs.GetBoolean(4) },
						Admin = new SponsorAdmin { SuperUser = !rs.IsDBNull(4) && rs.GetInt32(4) != 0 },
						Department = new Department {
							Name = rs.GetString(0),
							Depth = rs.GetInt32(1),
							Id = rs.GetInt32(2),
							Siblings = rs.GetInt32(3),
							ShortName = rs.GetString(5)
						}
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<SponsorAdminDepartment> b(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format(
					@"ISNULL(sad.DepartmentID, sa.SuperUser)
FROM Department d
INNER JOIN SponsorAdmin sa ON sa.SponsorAdminID = {0}
LEFT OUTER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
AND sad.SponsorAdminID = {0} ",
					sponsorAdminID)
				: @"1
FROM Department d ";
			
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	{1}
WHERE d.SponsorID = {0}",
				sponsorID,
				j
			);
			var departments = new List<SponsorAdminDepartment>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new SponsorAdminDepartment {
//						Admin = new SponsorAdmin { SuperUser = rs.GetBoolean(1) },
						Admin = new SponsorAdmin { SuperUser = !rs.IsDBNull(1) && rs.GetInt32(1) != 0 },
						Department = new Department {
							Id = rs.GetInt32(0)
						}
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindIn(string rndsd2)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	d.DepartmentID,
	d.SortString
FROM Department d
WHERE d.DepartmentID IN ({0})
ORDER BY d.SortString",
				rndsd2
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var d = new Department {
						Name = GetString(rs, 0),
						Id = GetInt32(rs, 1),
						SortString = GetString(rs, 2)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT DepartmentShort,
	DepartmentID
FROM Department
WHERE DepartmentShort IS NOT NULL
AND SponsorID = {0}",
				sponsorID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						ShortName = rs.GetString(0),
						Id = rs.GetInt32(1)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsor2(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	LEN(d.SortString),
	d.SortString
FROM Department d
WHERE d.SponsorID = {0}
ORDER BY LEN(d.SortString)",
				sponsorID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = rs.GetString(0),
						SortString = rs.GetString(2)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		// TODO: How about anonymized?
		public IList<Department> FindBySponsorWithSponsorAdminIn(int sponsorID, int sponsorAdminID, string GID)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	d.DepartmentID,
	d.SortString
FROM Department d
INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
WHERE sad.SponsorAdminID = {1}
AND d.SponsorID = {0}
AND (d.DepartmentID IN ({2}))
ORDER BY d.SortString",
				sponsorID,
				sponsorAdminID,
				GID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = GetString(rs, 0),
						Id = rs.GetInt32(1),
						SortString = GetString(rs, 2)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		// TODO: How about anonymized?
		public IList<Department> FindBySponsorOrderedBySortStringIn(int sponsorID, string GID)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	d.DepartmentID,
	DepartmentShort,
    SortString
FROM Department d
WHERE d.SponsorID = {0}
AND (d.DepartmentID IN ({1}))
ORDER BY d.SortString",
				sponsorID,
				GID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = GetString(rs, 0),
						Id = rs.GetInt32(1),
						ShortName = GetString(rs, 2),
						SortString = GetString(rs, 3)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		// TODO: How about anonymized?
		public IList<Department> FindBySponsorWithSponsorAdmin(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	d.DepartmentID,
	d.SortString
FROM Department d
INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
WHERE sad.SponsorAdminID = {1}
AND d.SponsorID = {0}
ORDER BY LEN(d.SortString)",
				sponsorID,
				sponsorAdminID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = GetString(rs, 0),
						Id = rs.GetInt32(1),
						SortString = GetString(rs, 2)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		// TODO: How about anonymized?
		public IList<Department> FindBySponsorOrderedBySortString(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	d.DepartmentID,
	DepartmentShort,
	SortString
FROM Department d
WHERE d.SponsorID = {0}
ORDER BY LEN(d.SortString)",
				sponsorID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = GetString(rs, 0),
						Id = rs.GetInt32(1),
						ShortName = GetString(rs, 2),
						SortString = GetString(rs, 3)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminOnTree(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format("INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{1}d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID,
				j
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Id = rs.GetInt32(0),
						TreeName = rs.GetString(1)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminAndTree(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format("INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{1}d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID,
				j
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Id = rs.GetInt32(0),
						TreeName = rs.GetString(1)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminSortStringAndTree(int sponsorID, string sortString, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format("INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	Department,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{3}d.SponsorID = {0}
AND LEFT(d.SortString,{2}) <> '{1}'
ORDER BY d.SortString",
				sponsorID,
				sortString,
				sortString.Length,
				j
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Id = rs.GetInt32(0),
						Name = rs.GetString(1),
						TreeName = rs.GetString(2)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminInDepth(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	d.DepartmentID,
	d.DepartmentShort,
	dbo.cf_departmentDepth(d.DepartmentID),
	(
		SELECT COUNT(*) FROM Department x
		{2}
		WHERE (x.ParentDepartmentID = d.ParentDepartmentID
			OR x.ParentDepartmentID IS NULL
			AND d.ParentDepartmentID IS NULL)
			AND d.SponsorID = x.SponsorID
			AND d.SortString < x.SortString
	)
FROM Department d
{3}
WHERE {1} d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID,
				(sponsorAdminID != -1 ? "sad.SponsorAdminID = " + sponsorAdminID + " AND " : ""),
				(sponsorAdminID != -1 ? "INNER JOIN SponsorAdminDepartment xx ON x.DepartmentID = xx.DepartmentID AND xx.SponsorAdminID = " + sponsorAdminID + " " : ""),
				(sponsorAdminID != -1 ? "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " : "")
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = rs.GetString(0),
						Id = rs.GetInt32(1),
						ShortName = rs.GetString(2),
						Depth = rs.GetInt32(3),
						Siblings = rs.GetInt32(4)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorInDepth(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT d.DepartmentAnonymized,
	d.DepartmentID,
	'',
	dbo.cf_departmentDepth(d.DepartmentID),
	(
		SELECT COUNT(*) FROM Department x
		WHERE (x.ParentDepartmentID = d.ParentDepartmentID
			OR x.ParentDepartmentID IS NULL
			AND d.ParentDepartmentID IS NULL)
			AND d.SponsorID = x.SponsorID
			AND d.SortString < x.SortString
	)
FROM Department d
WHERE d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = rs.GetString(0),
						Id = rs.GetInt32(1),
						ShortName = rs.GetString(2),
						Depth = rs.GetInt32(3)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
	}
}
