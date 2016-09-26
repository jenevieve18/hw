using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlProjectRoundUserRepository : BaseSqlRepository<ProjectRoundUser>
	{
		public SqlProjectRoundUserRepository()
		{
		}
		
		public override void Save(ProjectRoundUser projectRoundUser)
		{
			string query = @"
INSERT INTO ProjectRoundUser(
	ProjectRoundUserID, 
	ProjectRoundID, 
	ProjectRoundUnitID, 
	UserKey, 
	Email, 
	LastSent, 
	SendCount, 
	ReminderCount, 
	UserCategoryID, 
	Name, 
	Created, 
	Extended, 
	Extra, 
	ExternalID, 
	NoSend, 
	Terminated, 
	FollowupSendCount, 
	GroupID, 
	ExtendedTag
)
VALUES(
	@ProjectRoundUserID, 
	@ProjectRoundID, 
	@ProjectRoundUnitID, 
	@UserKey, 
	@Email, 
	@LastSent, 
	@SendCount, 
	@ReminderCount, 
	@UserCategoryID, 
	@Name, 
	@Created, 
	@Extended, 
	@Extra, 
	@ExternalID, 
	@NoSend, 
	@Terminated, 
	@FollowupSendCount, 
	@GroupID, 
	@ExtendedTag
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUserID", projectRoundUser.ProjectRoundUserID),
				new SqlParameter("@ProjectRoundID", projectRoundUser.ProjectRoundID),
				new SqlParameter("@ProjectRoundUnitID", projectRoundUser.ProjectRoundUnitID),
				new SqlParameter("@UserKey", projectRoundUser.UserKey),
				new SqlParameter("@Email", projectRoundUser.Email),
				new SqlParameter("@LastSent", projectRoundUser.LastSent),
				new SqlParameter("@SendCount", projectRoundUser.SendCount),
				new SqlParameter("@ReminderCount", projectRoundUser.ReminderCount),
				new SqlParameter("@UserCategoryID", projectRoundUser.UserCategoryID),
				new SqlParameter("@Name", projectRoundUser.Name),
				new SqlParameter("@Created", projectRoundUser.Created),
				new SqlParameter("@Extended", projectRoundUser.Extended),
				new SqlParameter("@Extra", projectRoundUser.Extra),
				new SqlParameter("@ExternalID", projectRoundUser.ExternalID),
				new SqlParameter("@NoSend", projectRoundUser.NoSend),
				new SqlParameter("@Terminated", projectRoundUser.Terminated),
				new SqlParameter("@FollowupSendCount", projectRoundUser.FollowupSendCount),
				new SqlParameter("@GroupID", projectRoundUser.GroupID),
				new SqlParameter("@ExtendedTag", projectRoundUser.ExtendedTag)
			);
		}
		
		public override void Update(ProjectRoundUser projectRoundUser, int id)
		{
			string query = @"
UPDATE ProjectRoundUser SET
	ProjectRoundUserID = @ProjectRoundUserID,
	ProjectRoundID = @ProjectRoundID,
	ProjectRoundUnitID = @ProjectRoundUnitID,
	UserKey = @UserKey,
	Email = @Email,
	LastSent = @LastSent,
	SendCount = @SendCount,
	ReminderCount = @ReminderCount,
	UserCategoryID = @UserCategoryID,
	Name = @Name,
	Created = @Created,
	Extended = @Extended,
	Extra = @Extra,
	ExternalID = @ExternalID,
	NoSend = @NoSend,
	Terminated = @Terminated,
	FollowupSendCount = @FollowupSendCount,
	GroupID = @GroupID,
	ExtendedTag = @ExtendedTag
WHERE ProjectRoundUserID = @ProjectRoundUserID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUserID", projectRoundUser.ProjectRoundUserID),
				new SqlParameter("@ProjectRoundID", projectRoundUser.ProjectRoundID),
				new SqlParameter("@ProjectRoundUnitID", projectRoundUser.ProjectRoundUnitID),
				new SqlParameter("@UserKey", projectRoundUser.UserKey),
				new SqlParameter("@Email", projectRoundUser.Email),
				new SqlParameter("@LastSent", projectRoundUser.LastSent),
				new SqlParameter("@SendCount", projectRoundUser.SendCount),
				new SqlParameter("@ReminderCount", projectRoundUser.ReminderCount),
				new SqlParameter("@UserCategoryID", projectRoundUser.UserCategoryID),
				new SqlParameter("@Name", projectRoundUser.Name),
				new SqlParameter("@Created", projectRoundUser.Created),
				new SqlParameter("@Extended", projectRoundUser.Extended),
				new SqlParameter("@Extra", projectRoundUser.Extra),
				new SqlParameter("@ExternalID", projectRoundUser.ExternalID),
				new SqlParameter("@NoSend", projectRoundUser.NoSend),
				new SqlParameter("@Terminated", projectRoundUser.Terminated),
				new SqlParameter("@FollowupSendCount", projectRoundUser.FollowupSendCount),
				new SqlParameter("@GroupID", projectRoundUser.GroupID),
				new SqlParameter("@ExtendedTag", projectRoundUser.ExtendedTag)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProjectRoundUser
WHERE ProjectRoundUserID = @ProjectRoundUserID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUserID", id)
			);
		}
		
		public override ProjectRoundUser Read(int id)
		{
			string query = @"
SELECT 	ProjectRoundUserID, 
	ProjectRoundID, 
	ProjectRoundUnitID, 
	UserKey, 
	Email, 
	LastSent, 
	SendCount, 
	ReminderCount, 
	UserCategoryID, 
	Name, 
	Created, 
	Extended, 
	Extra, 
	ExternalID, 
	NoSend, 
	Terminated, 
	FollowupSendCount, 
	GroupID, 
	ExtendedTag
FROM ProjectRoundUser
WHERE ProjectRoundUserID = @ProjectRoundUserID";
			ProjectRoundUser projectRoundUser = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectRoundUserID", id))) {
				if (rs.Read()) {
					projectRoundUser = new ProjectRoundUser {
						ProjectRoundUserID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						ProjectRoundUnitID = GetInt32(rs, 2),
						UserKey = GetGuid(rs, 3),
						Email = GetString(rs, 4),
						LastSent = GetDateTime(rs, 5),
						SendCount = GetInt32(rs, 6),
						ReminderCount = GetInt32(rs, 7),
						UserCategoryID = GetInt32(rs, 8),
						Name = GetString(rs, 9),
						Created = GetDateTime(rs, 10),
						Extended = GetInt32(rs, 11),
						Extra = GetString(rs, 12),
						ExternalID = GetInt32(rs, 13),
						NoSend = GetInt32(rs, 14),
						Terminated = GetInt32(rs, 15),
						FollowupSendCount = GetInt32(rs, 16),
						GroupID = GetInt32(rs, 17),
						ExtendedTag = GetInt32(rs, 18)
					};
				}
			}
			return projectRoundUser;
		}
		
		public override IList<ProjectRoundUser> FindAll()
		{
			string query = @"
SELECT 	ProjectRoundUserID, 
	ProjectRoundID, 
	ProjectRoundUnitID, 
	UserKey, 
	Email, 
	LastSent, 
	SendCount, 
	ReminderCount, 
	UserCategoryID, 
	Name, 
	Created, 
	Extended, 
	Extra, 
	ExternalID, 
	NoSend, 
	Terminated, 
	FollowupSendCount, 
	GroupID, 
	ExtendedTag
FROM ProjectRoundUser";
			var projectRoundUsers = new List<ProjectRoundUser>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					projectRoundUsers.Add(new ProjectRoundUser {
						ProjectRoundUserID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						ProjectRoundUnitID = GetInt32(rs, 2),
						UserKey = GetGuid(rs, 3),
						Email = GetString(rs, 4),
						LastSent = GetDateTime(rs, 5),
						SendCount = GetInt32(rs, 6),
						ReminderCount = GetInt32(rs, 7),
						UserCategoryID = GetInt32(rs, 8),
						Name = GetString(rs, 9),
						Created = GetDateTime(rs, 10),
						Extended = GetInt32(rs, 11),
						Extra = GetString(rs, 12),
						ExternalID = GetInt32(rs, 13),
						NoSend = GetInt32(rs, 14),
						Terminated = GetInt32(rs, 15),
						FollowupSendCount = GetInt32(rs, 16),
						GroupID = GetInt32(rs, 17),
						ExtendedTag = GetInt32(rs, 18)
					});
				}
			}
			return projectRoundUsers;
		}
	}
}
