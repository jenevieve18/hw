using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlManagerRepository : BaseSqlRepository<Manager>
	{
		public SqlManagerRepository()
		{
		}
		
		public override void Save(Manager manager)
		{
			string query = @"
INSERT INTO Manager(
	ManagerID, 
	Email, 
	Password, 
	Name, 
	Phone, 
	AddUser, 
	SeeAnswer, 
	ExpandAll, 
	UseExternalID, 
	SeeFeedback, 
	HasFeedback, 
	SeeUnit, 
	SeeTerminated, 
	SeeSurvey
)
VALUES(
	@ManagerID, 
	@Email, 
	@Password, 
	@Name, 
	@Phone, 
	@AddUser, 
	@SeeAnswer, 
	@ExpandAll, 
	@UseExternalID, 
	@SeeFeedback, 
	@HasFeedback, 
	@SeeUnit, 
	@SeeTerminated, 
	@SeeSurvey
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerID", manager.ManagerID),
				new SqlParameter("@Email", manager.Email),
				new SqlParameter("@Password", manager.Password),
				new SqlParameter("@Name", manager.Name),
				new SqlParameter("@Phone", manager.Phone),
				new SqlParameter("@AddUser", manager.AddUser),
				new SqlParameter("@SeeAnswer", manager.SeeAnswer),
				new SqlParameter("@ExpandAll", manager.ExpandAll),
				new SqlParameter("@UseExternalID", manager.UseExternalID),
				new SqlParameter("@SeeFeedback", manager.SeeFeedback),
				new SqlParameter("@HasFeedback", manager.HasFeedback),
				new SqlParameter("@SeeUnit", manager.SeeUnit),
				new SqlParameter("@SeeTerminated", manager.SeeTerminated),
				new SqlParameter("@SeeSurvey", manager.SeeSurvey)
			);
		}
		
		public override void Update(Manager manager, int id)
		{
			string query = @"
UPDATE Manager SET
	ManagerID = @ManagerID,
	Email = @Email,
	Password = @Password,
	Name = @Name,
	Phone = @Phone,
	AddUser = @AddUser,
	SeeAnswer = @SeeAnswer,
	ExpandAll = @ExpandAll,
	UseExternalID = @UseExternalID,
	SeeFeedback = @SeeFeedback,
	HasFeedback = @HasFeedback,
	SeeUnit = @SeeUnit,
	SeeTerminated = @SeeTerminated,
	SeeSurvey = @SeeSurvey
WHERE ManagerID = @ManagerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerID", manager.ManagerID),
				new SqlParameter("@Email", manager.Email),
				new SqlParameter("@Password", manager.Password),
				new SqlParameter("@Name", manager.Name),
				new SqlParameter("@Phone", manager.Phone),
				new SqlParameter("@AddUser", manager.AddUser),
				new SqlParameter("@SeeAnswer", manager.SeeAnswer),
				new SqlParameter("@ExpandAll", manager.ExpandAll),
				new SqlParameter("@UseExternalID", manager.UseExternalID),
				new SqlParameter("@SeeFeedback", manager.SeeFeedback),
				new SqlParameter("@HasFeedback", manager.HasFeedback),
				new SqlParameter("@SeeUnit", manager.SeeUnit),
				new SqlParameter("@SeeTerminated", manager.SeeTerminated),
				new SqlParameter("@SeeSurvey", manager.SeeSurvey)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Manager
WHERE ManagerID = @ManagerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerID", id)
			);
		}
		
		public override Manager Read(int id)
		{
			string query = @"
SELECT 	ManagerID, 
	Email, 
	Password, 
	Name, 
	Phone, 
	AddUser, 
	SeeAnswer, 
	ExpandAll, 
	UseExternalID, 
	SeeFeedback, 
	HasFeedback, 
	SeeUnit, 
	SeeTerminated, 
	SeeSurvey
FROM Manager
WHERE ManagerID = @ManagerID";
			Manager manager = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ManagerID", id))) {
				if (rs.Read()) {
					manager = new Manager {
						ManagerID = GetInt32(rs, 0),
						Email = GetString(rs, 1),
						Password = GetString(rs, 2),
						Name = GetString(rs, 3),
						Phone = GetString(rs, 4),
						AddUser = GetInt32(rs, 5),
						SeeAnswer = GetInt32(rs, 6),
						ExpandAll = GetInt32(rs, 7),
						UseExternalID = GetInt32(rs, 8),
						SeeFeedback = GetInt32(rs, 9),
						HasFeedback = GetInt32(rs, 10),
						SeeUnit = GetInt32(rs, 11),
						SeeTerminated = GetInt32(rs, 12),
						SeeSurvey = GetInt32(rs, 13)
					};
				}
			}
			return manager;
		}
		
		public Manager ReadByEmailAndPassword(string email, string password)
		{
			string query = @"
SELECT 	ManagerID, 
	Email, 
	Password, 
	Name, 
	Phone, 
	AddUser, 
	SeeAnswer, 
	ExpandAll, 
	UseExternalID, 
	SeeFeedback, 
	HasFeedback, 
	SeeUnit, 
	SeeTerminated, 
	SeeSurvey
FROM Manager
WHERE Email = @Email
AND Password = @Password";
			Manager manager = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@Email", email), new SqlParameter("@Password", password))) {
				if (rs.Read()) {
					manager = new Manager {
						ManagerID = GetInt32(rs, 0),
						Email = GetString(rs, 1),
						Password = GetString(rs, 2),
						Name = GetString(rs, 3),
						Phone = GetString(rs, 4),
						AddUser = GetInt32(rs, 5),
						SeeAnswer = GetInt32(rs, 6),
						ExpandAll = GetInt32(rs, 7),
						UseExternalID = GetInt32(rs, 8),
						SeeFeedback = GetInt32(rs, 9),
						HasFeedback = GetInt32(rs, 10),
						SeeUnit = GetInt32(rs, 11),
						SeeTerminated = GetInt32(rs, 12),
						SeeSurvey = GetInt32(rs, 13)
					};
				}
			}
			return manager;
		}
		
		public override IList<Manager> FindAll()
		{
			string query = @"
SELECT 	ManagerID, 
	Email, 
	Password, 
	Name, 
	Phone, 
	AddUser, 
	SeeAnswer, 
	ExpandAll, 
	UseExternalID, 
	SeeFeedback, 
	HasFeedback, 
	SeeUnit, 
	SeeTerminated, 
	SeeSurvey
FROM Manager";
			var managers = new List<Manager>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					managers.Add(new Manager {
						ManagerID = GetInt32(rs, 0),
						Email = GetString(rs, 1),
						Password = GetString(rs, 2),
						Name = GetString(rs, 3),
						Phone = GetString(rs, 4),
						AddUser = GetInt32(rs, 5),
						SeeAnswer = GetInt32(rs, 6),
						ExpandAll = GetInt32(rs, 7),
						UseExternalID = GetInt32(rs, 8),
						SeeFeedback = GetInt32(rs, 9),
						HasFeedback = GetInt32(rs, 10),
						SeeUnit = GetInt32(rs, 11),
						SeeTerminated = GetInt32(rs, 12),
						SeeSurvey = GetInt32(rs, 13)
					});
				}
			}
			return managers;
		}
	}
}
