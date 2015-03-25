using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Services;

namespace HW.Core.Repositories.Sql
{
	public class SqlDepartmentRepository : BaseSqlRepository<Department>//, IDepartmentRepository
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
		
		public Department Save2(int sponsorID, string name, string shortName)
		{
			string query = string.Format(
				@"
SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRAN;
INSERT INTO Department (SponsorID, Department, DepartmentShort)
VALUES ({0}, '{1}', '{2}');
SELECT DepartmentID FROM [Department] WHERE SponsorID={0} AND DepartmentShort = '{2}' ORDER BY DepartmentID DESC;
COMMIT;",
				sponsorID,
				name,
				shortName
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					return new Department {
						Id = GetInt32(rs, 0)
					};
				}
			}
			return null;
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
		
		public void Update3(int sponsorID, int deleteDepartmentID)
		{
			string query = string.Format("UPDATE Department SET SponsorID = -ABS(SponsorID) WHERE SponsorID = " + sponsorID + " AND DepartmentID = " + deleteDepartmentID);
			Db.exec(query);
			query = string.Format("UPDATE SponsorAdminDepartment SET DepartmentID = -ABS(DepartmentID) WHERE DepartmentID = " + deleteDepartmentID);
			Db.exec(query);
			query = string.Format("UPDATE Department SET SortString = dbo.cf_departmentSortString(DepartmentID) WHERE SponsorID = " + sponsorID + "");
			Db.exec(query);
		}
		
		public void UpdateLoginSettings(string loginDays, string loginWeekDay, int departmentID)
		{
			string query = string.Format(
				@"
UPDATE Department SET LoginDays = {0},
LoginWeekDay = {1}
WHERE DepartmentID = {2}",
				loginDays,
				loginWeekDay,
				departmentID
			);
			Db.exec(query);
		}
		
		public void Update(int deptID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
UPDATE Department SET SortOrder = {0}
WHERE DepartmentID = {0}",
				deptID
			);
			Db.exec(query);

			if (sponsorAdminID != -1) {
				query = string.Format(
					@"
INSERT INTO SponsorAdminDepartment (SponsorAdminID, DepartmentID)
VALUES ({0}, {1})",
					sponsorAdminID,
					deptID
				);
				Db.exec(query);
			}
		}
		
		public void Update2(string parentDepartmentID, int deptID)
		{
			string query = string.Format(
				@"
UPDATE Department SET ParentDepartmentID = {0}
WHERE DepartmentID = {1}",
				parentDepartmentID,
				deptID
			);
			Db.exec(query);
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
			string query = string.Format(
				@"
SELECT DepartmentID
FROM Department
WHERE SponsorID = {0} ORDER BY DepartmentID DESC",
				sponsorID
			);
			int deptID = 0;
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					deptID = rs.GetInt32(0);
				}
			}
			return deptID;
		}
		
		public Department Read(int bqID, int deptID)
		{
			string query = string.Format(
				@"
SELECT AVG(DATEDIFF(year, upbq.ValueDate, GETDATE())),
	COUNT(upbq.ValueDate)
FROM Department d
INNER JOIN Department sid ON LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND sid.SponsorID = d.SponsorID
INNER JOIN SponsorInvite si ON sid.DepartmentID = si.DepartmentID
INNER JOIN [User] u ON si.UserID = u.UserID
INNER JOIN UserProfileBQ upbq ON u.UserProfileID = upbq.UserProfileID AND upbq.BQID = {0}
WHERE d.DepartmentID = {1}",
				bqID,
				deptID
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					return new Department {
						Average = GetDouble(rs, 0),
						Count = GetInt32(rs, 1)
					};
				}
			}
			return null;
		}
		
		public Department Read(string shortName, int sponsorID)
		{
			string query = string.Format(
				@"
SELECT DepartmentID
FROM Department
WHERE DepartmentShort = '{0}' AND SponsorID = {1}",
				shortName,
				sponsorID
			);
			using (SqlDataReader rs = Db.rs(query)) {
				if (rs.Read()) {
					return new Department {
						Id = GetInt32(rs, 0)
					};
				}
			}
			return null;
		}
		
		public Department ReadWithReminder2(int id)
		{
			string query = string.Format(
				@"
WITH SelectRecursiveDepartment(DepartmentID, LoginWeekday, Level) AS (
	SELECT DepartmentID, LoginWeekday, 0 AS Level
	FROM Department
	WHERE ParentDepartmentID IS NULL
	OR LoginWeekday IS NOT NULL
	UNION ALL
	SELECT d.DepartmentID, q.LoginWeekday, Level + 1 as Level
	FROM Department d
	INNER JOIN SelectRecursiveDepartment q ON d.ParentDepartmentID = q.DepartmentID
)
SELECT TOP 1 LoginWeekday
FROM SelectRecursiveDepartment
WHERE DepartmentID = @DepartmentID"
			);
			Department d = new Department();
			using (SqlDataReader rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@DepartmentID", id))) {
				if (rs.Read()) {
					d.LoginWeekDay = GetInt32(rs, 0, -666);
				}
			}
			
			query = string.Format(
				@"
WITH SelectRecursiveDepartment(DepartmentID, LoginDays, Level) AS (
	SELECT DepartmentID, LoginDays, 0 AS Level
	FROM Department
	WHERE ParentDepartmentID IS NULL
	OR LoginDays IS NOT NULL
	UNION ALL
	SELECT d.DepartmentID, q.LoginDays, Level + 1 as Level
	FROM Department d
	INNER JOIN SelectRecursiveDepartment q ON d.ParentDepartmentID = q.DepartmentID
)
SELECT TOP 1 LoginDays
FROM SelectRecursiveDepartment
WHERE DepartmentID = @DepartmentID"
			);
			using (SqlDataReader rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@DepartmentID", id))) {
				if (rs.Read()) {
					d.LoginDays = GetInt32(rs, 0, -1);
				}
			}
			
			return d;
		}
		
		public Department ReadWithReminder(int id)
		{
			string query = string.Format(
				@"
SELECT DepartmentID, Department, ParentDepartmentID, LoginDays, LoginWeekday, Level
FROM FindDepartmentWithReminder(@DepartmentID)",
				id
			);
			Department d = null;
			using (SqlDataReader rs = ExecuteReader(query, "healthWatchSqlConnection", new SqlParameter("@DepartmentID", id))) {
				if (rs.Read()) {
					d = new Department {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Parent = new Department { Id = GetInt32(rs, 2) },
						LoginDays = GetInt32(rs, 3, -1),
						LoginWeekDay = GetInt32(rs, 4, -1)
					};
				}
			}
			return d;
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
WHERE d.SponsorID = {0}
AND d.DepartmentID = {1}",
				sponsorID,
				departmentID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var d = new Department {
						SortString = rs.GetString(0),
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
		
//		public IList<Department> FindBySponsor(int sponsorID)
//		{
//			string query = string.Format(
//				@"
		//SELECT DepartmentID,
//	dbo.cf_departmentTree(DepartmentID,' &gt; '),
//	DepartmentShort
		//FROM Department
		//WHERE DepartmentShort IS NOT NULL
		//AND SponsorID = {0}",
//				sponsorID
//			);
//			var departments = new List<Department>();
//			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
//				while (rs.Read()) {
//					var d = new Department {
//						Id = GetInt32(rs, 0),
//						TreeName = GetString(rs, 1),
//						ShortName = GetString(rs, 2)
//					};
//					departments.Add(d);
//				}
//			}
//			return departments;
//		}
		
		public IList<Department> lalala(int sponsorAdminID, string ESselect, string ESjoin, int sponsorID)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	dbo.cf_departmentDepth(d.DepartmentID),
	d.DepartmentID,
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		WHERE si.DepartmentID = d.DepartmentID AND si.SponsorID = d.SponsorID
	),
	(
		SELECT COUNT(*)
		FROM SponsorInvite si INNER JOIN [User] u ON si.UserID = u.UserID
		WHERE si.DepartmentID = d.DepartmentID AND si.SponsorID = d.SponsorID
	),
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		WHERE si.DepartmentID = d.DepartmentID AND si.SponsorID = d.SponsorID AND si.Sent IS NOT NULL
	),
	(
		SELECT COUNT(*) FROM Department x
		{0}
		WHERE (x.ParentDepartmentID = d.ParentDepartmentID OR x.ParentDepartmentID IS NULL AND d.ParentDepartmentID IS NULL)
		AND d.SponsorID = x.SponsorID
		AND d.SortString < x.SortString
	),
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID
		WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID
	),
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		INNER JOIN [User] u ON si.UserID = u.UserID
		INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID
		WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID
	),
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID
		WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID AND si.Sent IS NOT NULL
	),
	(
		SELECT MIN(si.Sent)
		FROM SponsorInvite si
		INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID
		WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID AND si.Sent IS NOT NULL
	),
	d.DepartmentShort
	{1},
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		INNER JOIN [User] u ON si.UserID = u.UserID
		INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID
		WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString
		AND si.SponsorID = d.SponsorID
		AND si.StoppedReason IS NULL
		AND si.UserID IS NOT NULL
	),
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		INNER JOIN [User] u ON si.UserID = u.UserID
		WHERE si.DepartmentID = d.DepartmentID
		AND si.StoppedReason IS NULL
	),
	d.MinUserCountToDisclose
FROM Department d
INNER JOIN Sponsor s ON d.SponsorID = s.SponsorID
{2}
{3}
d.SponsorID = {4} ORDER BY d.SortString",
				(sponsorAdminID != -1 ? "INNER JOIN SponsorAdminDepartment xx ON x.DepartmentID = xx.DepartmentID AND xx.SponsorAdminID = " + sponsorAdminID + " " : ""),
				ESselect,
				ESjoin,
				(sponsorAdminID != -1 ? "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + sponsorAdminID + " AND " : "WHERE "),
				sponsorID
			);
			IList<Department> departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var d = new Department {
						Name = GetString(rs, 0),
						Depth = GetInt32(rs, 1),
						Id = GetInt32(rs, 2)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> c(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{0} d.SponsorID = {1}
ORDER BY d.SortString",
				(sponsorAdminID != -1 ? "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + sponsorAdminID + " AND " : "WHERE "),
				sponsorID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var d = new Department {
						Id = GetInt32(rs, 0),
						TreeName = GetString(rs, 1)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public List<Department> FindSponsorWithSponsorAdminOnTree(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{0} d.SponsorID = {1}
ORDER BY d.SortString",
				(sponsorAdminID != -1 ? "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + sponsorAdminID + " AND " : "WHERE "),
				sponsorID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var d = new Department {
						Id = GetInt32(rs, 0),
						TreeName = GetString(rs, 1)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public List<Department> FindSponsorWithSponsorAdminOnTree(int sponsorID, int sponsorAdminID, string sortString)
		{
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{0} d.SponsorID = {1}
AND LEFT(d.SortString,{2}) <> '{3}'
ORDER BY d.SortString",
				(sponsorAdminID != -1 ? "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + sponsorAdminID + " AND " : "WHERE "),
				sponsorID,
				sortString.Length,
				sortString
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var d = new Department {
						Id = GetInt32(rs, 0),
						TreeName = GetString(rs, 1)
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
SELECT DepartmentShort,
	DepartmentID
FROM Department
WHERE DepartmentShort IS NOT NULL
AND SponsorID = {0}",
				sponsorID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					var d = new Department {
						ShortName = GetString(rs, 0),
						Id = GetInt32(rs, 1)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsor(int sponsorID, string units)
		{
			string query = string.Format(
				@"
SELECT dbo.cf_DepartmentTree(DepartmentID,' » '),
	DepartmentShort FROM
Department WHERE SponsorID = {0}
AND DepartmentShort IN ({1})",
				sponsorID,
				units
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query)) {
				while (rs.Read()) {
					departments.Add(new Department { TreeName = GetString(rs, 0), ShortName = GetString(rs, 1) });
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	d.Department,
	dbo.cf_departmentTree(DepartmentID,' &gt; '),
	d.SortString,
	d.MinUserCountToDisclose
FROM Department d
WHERE d.SponsorID = {0}
ORDER BY dbo.cf_departmentSortString(d.DepartmentID)",
				sponsorID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						TreeName = GetString(rs, 2),
						SortString = GetString(rs, 3),
						MinUserCountToDisclose = GetInt32(rs, 4, 10)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminIn(int sponsorID, int sponsorAdminID, string gid, int sponsorMinUserCountToDisclose)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	d.DepartmentID,
	d.SortString,
	d.MinUserCountToDisclose
FROM Department d
INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
WHERE sad.SponsorAdminID = {1}
AND d.SponsorID = {0}
AND (d.DepartmentID IN ({2}))
ORDER BY d.SortString",
				sponsorID,
				sponsorAdminID,
				gid
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = GetString(rs, 0),
						Id = rs.GetInt32(1),
						SortString = GetString(rs, 2),
						MinUserCountToDisclose = GetInt32(rs, 3, sponsorMinUserCountToDisclose)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorOrderedBySortStringIn(int sponsorID, string gid, int sponsorMinUserCountToDisclose)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	d.DepartmentID,
	DepartmentShort,
    SortString,
	d.MinUserCountToDisclose
FROM Department d
WHERE d.SponsorID = {0}
AND (d.DepartmentID IN ({1}))
ORDER BY d.SortString",
				sponsorID,
				gid
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = GetString(rs, 0),
						Id = rs.GetInt32(1),
						ShortName = GetString(rs, 2),
						SortString = GetString(rs, 3),
						MinUserCountToDisclose = GetInt32(rs, 4, sponsorMinUserCountToDisclose)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdmin(int sponsorID, int sponsorAdminID, int sponsorMinUserCountToDisclose)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	d.DepartmentID,
	d.SortString,
	d.MinUserCountToDisclose
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
						SortString = GetString(rs, 2),
						MinUserCountToDisclose = GetInt32(rs, 3, sponsorMinUserCountToDisclose)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorOrderedBySortString(int sponsorID, int sponsorMinUserCountToDisclose)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	d.DepartmentID,
	DepartmentShort,
	SortString,
	d.MinUserCountToDisclose
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
						SortString = GetString(rs, 3),
						MinUserCountToDisclose = GetInt32(rs, 4, sponsorMinUserCountToDisclose)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
//		public IList<Department> FindBySponsorWithSponsorAdminOnTree(int sponsorID, int sponsorAdminID)
//		{
//			string j = sponsorAdminID != -1
//				? string.Format("INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
//				: "WHERE ";
//			string query = string.Format(
//				@"
		//SELECT d.DepartmentID,
//	dbo.cf_departmentTree(d.DepartmentID,' » ')
		//FROM Department d
		//{1}d.SponsorID = {0}
		//ORDER BY d.SortString",
//				sponsorID,
//				j
//			);
//			var departments = new List<Department>();
//			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
//				while (rs.Read()) {
//					var d = new Department {
//						Id = rs.GetInt32(0),
//						TreeName = rs.GetString(1)
//					};
//					departments.Add(d);
//				}
//			}
//			return departments;
//		}
//
//		public IList<Department> FindBySponsorWithSponsorAdminAndTree(int sponsorID, int sponsorAdminID)
//		{
//			string j = sponsorAdminID != -1
//				? string.Format("INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
//				: "WHERE ";
//			string query = string.Format(
//				@"
		//SELECT d.DepartmentID,
//	dbo.cf_departmentTree(d.DepartmentID,' » ')
		//FROM Department d
		//{1}d.SponsorID = {0}
		//ORDER BY d.SortString",
//				sponsorID,
//				j
//			);
//			var departments = new List<Department>();
//			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
//				while (rs.Read()) {
//					var d = new Department {
//						Id = rs.GetInt32(0),
//						TreeName = rs.GetString(1)
//					};
//					departments.Add(d);
//				}
//			}
//			return departments;
//		}
//
//		public IList<Department> FindBySponsorWithSponsorAdminSortStringAndTree(int sponsorID, string sortString, int sponsorAdminID)
//		{
//			string j = sponsorAdminID != -1
//				? string.Format("INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
//				: "WHERE ";
//			string query = string.Format(
//				@"
		//SELECT d.DepartmentID,
//	Department,
//	dbo.cf_departmentTree(d.DepartmentID,' » ')
		//FROM Department d
		//{3}d.SponsorID = {0}
		//AND LEFT(d.SortString,{2}) <> '{1}'
		//ORDER BY d.SortString",
//				sponsorID,
//				sortString,
//				sortString.Length,
//				j
//			);
//			var departments = new List<Department>();
//			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
//				while (rs.Read()) {
//					var d = new Department {
//						Id = rs.GetInt32(0),
//						Name = rs.GetString(1),
//						TreeName = rs.GetString(2)
//					};
//					departments.Add(d);
//				}
//			}
//			return departments;
//		}
		
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
	),
	d.LoginDays,
	d.LoginWeekDay
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
						Name = GetString(rs, 0),
						Id = GetInt32(rs, 1),
						ShortName = GetString(rs, 2),
						Depth = GetInt32(rs, 3),
						Siblings = GetInt32(rs, 4),
						LoginDays = GetInt32(rs, 5, -666),
						LoginWeekDay = GetInt32(rs, 6, -666)
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
